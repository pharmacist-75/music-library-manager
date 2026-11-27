using MusicLibraryManager.Models;
using MusicLibraryManager.Models.MusicBrainz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MusicLibraryManager.Services;

#nullable enable
internal sealed class MusicBrainzLibraryAnalyzer
{
    private readonly MusicBrainzService _musicBrainz;
    private readonly MusicBrainzCacheService _cache;
    private readonly MusicBrainzReleaseGroupPrioritizer _prioritizer = new();
    private readonly MusicBrainzReleaseSelector _selector = new();
    private readonly MusicBrainzBulkMatcher _matcher = new();

    public MusicBrainzLibraryAnalyzer(MusicBrainzService musicBrainz, MusicBrainzCacheService cache)
    {
        _musicBrainz = musicBrainz;
        _cache = cache;
    }

    public async Task<BulkMatchResult> AnalyzeAsync(string artistName, IReadOnlyCollection<LocalMusicTrack> tracks, IProgress<string>? progress = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(artistName))
        {
            throw new ArgumentException("Artist name is required.", nameof(artistName));
        }

        progress?.Report($"Finding artist: {artistName}");

        ArtistSearchResult? artistSearch = _cache.GetArtists(artistName);

        if (artistSearch is null)
        {
            artistSearch = await _musicBrainz.SearchArtistsAsync(artistName, cancellationToken);
            _cache.StoreArtists(artistName, artistSearch);
        }

        List<Artist> artists = artistSearch.Artists
            .Where(artist => string.Equals(artist.Name, artistName, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(artist => artist.Score)
            .ToList();

        if (artists.Count == 0)
        {
            throw new InvalidOperationException($"Artist not found: {artistName}");
        }

        if (artists.Count > 1 && artists[0].Score == artists[1].Score)
        {
            throw new InvalidOperationException($"Ambiguous artist: {artistName}. Manual selection required.");
        }

        foreach (Artist artist in artistSearch.Artists)
        {
            Debug.WriteLine($"{artist.Name} | {artist.Id} | Score: {artist.Score} | {artist.Disambiguation}");
        }

        Artist selectedArtist = artists[0]; List<ReleaseGroup> albumGroups = [];

        for (int offset = 0; ; offset += 100)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ReleaseGroupSearchResult? page = _cache.GetAlbumGroups(selectedArtist.Id, offset);

            if (page is null)
            {
                page = await _musicBrainz.GetAlbumGroupsAsync(selectedArtist.Id, offset, cancellationToken);
                _cache.StoreAlbumGroups(selectedArtist.Id, offset, page);
            }

            albumGroups.AddRange(page.ReleaseGroups);

            if (page.ReleaseGroups.Count == 0 || offset + page.ReleaseGroups.Count >= page.Count)
            {
                break;
            }
        }

        List<ReleaseGroup> studioAlbums = _prioritizer.Prioritize(albumGroups)
            .Where(group => _prioritizer.GetPriority(group) == 0)
            .ToList();

        List<ReleaseDetails> releases = [];
        List<ReleaseDetails> cachedReleases = _cache.GetCachedReleases();

        HashSet<string> studioAlbumIds = studioAlbums
            .Select(group => group.Id)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        releases.AddRange(cachedReleases
            .Where(release => release.ReleaseGroup is not null && studioAlbumIds.Contains(release.ReleaseGroup.Id)));

        HashSet<string> cachedGroups = new(StringComparer.OrdinalIgnoreCase);
        List<string> unprocessedAlbums = [];

        // Evaluate all cached album tracklists before making new API requests.
        foreach (ReleaseGroup group in studioAlbums)
        {
            ReleaseBrowseResult? editions = _cache.GetReleases(group.Id, 0);

            if (editions is null)
            {
                continue;
            }

            List<(ReleaseDetails Details, int Matches)> candidates = editions.Releases
                .Select(edition => _cache.GetRelease(edition.Id))
                .OfType<ReleaseDetails>()
                .Select(details => (Details: details, Matches: _matcher.Match([details], tracks).Releases.FirstOrDefault()?.MatchedTracks ?? 0))
                .OrderByDescending(candidate => candidate.Matches)
                .ToList();

            if (candidates.Count == 0)
            {
                continue;
            }

            releases.Add(candidates[0].Details);

            if (editions.Releases.Count == editions.Count)
            {
                cachedGroups.Add(group.Id);
            }

            progress?.Report($"Cached album: {group.Title} ({candidates[0].Matches} matches)");
        }

        foreach (ReleaseGroup group in studioAlbums)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ReleaseGroup? details = _cache.GetReleaseGroup(group.Id);

            if (details is not null)
            {
                continue;
            }

            try
            {
                details = await _musicBrainz.GetReleaseGroupAsync(group.Id, cancellationToken);
                _cache.StoreReleaseGroup(details);
            }
            catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                progress?.Report("Release-group dates unavailable. Continuing with cached data.");
                break;
            }
        }

        // Fetch missing album data only after cached candidates have been evaluated.

        try
        {
            await FindBetterEditionAsync(releases, tracks, cancellationToken);
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            BulkMatchResult partialResult = _matcher.Match(releases, tracks);
            partialResult.UnprocessedAlbumGroups.Add("Edition lookup");
            partialResult.UnprocessedAlbumGroups.AddRange(studioAlbums.Where(group => !cachedGroups.Contains(group.Id)).Select(group => group.Title));
            return partialResult;
        }

        List<ReleaseGroup> remainingGroups = studioAlbums
            .Where(group => !cachedGroups.Contains(group.Id))
            .ToList();

        for (int index = 0; index < remainingGroups.Count; index++)
        {
            ReleaseGroup group = remainingGroups[index];
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                progress?.Report($"Analyzing album: {group.Title}");

                ReleaseBrowseResult? editions = _cache.GetReleases(group.Id, 0);

                if (editions is null)
                {
                    editions = await _musicBrainz.GetReleasesAsync(group.Id, cancellationToken: cancellationToken);
                    _cache.StoreReleases(group.Id, 0, editions);
                }

                if (editions.Releases.Count < editions.Count)
                {
                    throw new InvalidOperationException($"Edition list is incomplete: {group.Title}");
                }

                Release? selected = _selector.Select(editions.Releases);

                if (selected is null)
                {
                    continue;
                }

                ReleaseDetails? details = _cache.GetRelease(selected.Id);

                if (details is null)
                {
                    details = await _musicBrainz.GetReleaseAsync(selected.Id, cancellationToken);
                    _cache.StoreRelease(selected.Id, details);
                }

                releases.Add(details);
            }
            catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                unprocessedAlbums.AddRange(remainingGroups.Skip(index).Select(album => album.Title));
                progress?.Report("MusicBrainz unavailable. Showing cached results.");
                break;
            }
        }

        BulkMatchResult result = _matcher.Match(releases, tracks);
        result.UnprocessedAlbumGroups.AddRange(unprocessedAlbums);
        return result;
    }

    private async Task FindBetterEditionAsync(List<ReleaseDetails> releases, IReadOnlyCollection<LocalMusicTrack> tracks, CancellationToken cancellationToken)
    {
        MusicBrainzReleaseMatcher matcher = new();

        ReleaseDetails? bestRelease = releases
            .OrderByDescending(release => matcher.Match(release, tracks).MatchedTracks)
            .FirstOrDefault();

        if (bestRelease?.ReleaseGroup is null)
        {
            return;
        }

        ReleaseMatchResult bestMatch = matcher.Match(bestRelease, tracks);

        HashSet<string> matchedFiles = bestMatch.Matches
            .Select(match => match.FilePath)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        List<LocalMusicTrack> missingTracks = tracks
            .Where(track => !matchedFiles.Contains(track.FilePath))
            .ToList();

        if (missingTracks.Count == 0)
        {
            return;
        }

        Dictionary<string, (Release Release, int Count)> candidates = new(StringComparer.OrdinalIgnoreCase);

        foreach (LocalMusicTrack track in missingTracks)
        {
            cancellationToken.ThrowIfCancellationRequested();

            RecordingSearchResult? search = _cache.GetRecordings(track.Artist, track.Title);

            if (search is null)
            {
                search = await _musicBrainz.SearchRecordingsAsync(track.Artist, track.Title, cancellationToken);
                _cache.StoreRecordings(track.Artist, track.Title, search);
            }

            Dictionary<string, Release> matchingReleases = search.Recordings
                .SelectMany(recording => recording.Releases)
                .Where(release => string.Equals(release.ReleaseGroup?.Id, bestRelease.ReleaseGroup.Id, StringComparison.OrdinalIgnoreCase))
                .GroupBy(release => release.Id, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);

            foreach ((string releaseId, Release release) in matchingReleases)
            {
                int count = candidates.TryGetValue(releaseId, out (Release Release, int Count) existing)
                    ? existing.Count
                    : 0;

                candidates[releaseId] = (release, count + 1);
            }
        }

        List<Release> orderedCandidates = candidates.Values
            .OrderByDescending(candidate => candidate.Count)
            .ThenBy(candidate => candidate.Release.Date is null || candidate.Release.Date.Length < 4 ? "9999" : candidate.Release.Date)
            .Select(candidate => candidate.Release)
            .ToList();

        foreach (Release candidate in orderedCandidates)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ReleaseDetails? details = _cache.GetRelease(candidate.Id);

            if (details is null)
            {
                details = await _musicBrainz.GetReleaseAsync(candidate.Id, cancellationToken);
                _cache.StoreRelease(candidate.Id, details);
            }

            ReleaseMatchResult match = matcher.Match(details, tracks);

            if (match.MatchedTracks > bestMatch.MatchedTracks)
            {
                bestRelease = details;
                bestMatch = match;
            }

            if (bestMatch.MatchedTracks == tracks.Count)
            {
                break;
            }
        }

        string releaseGroupId = bestRelease.ReleaseGroup.Id;

        releases.RemoveAll(release => string.Equals(release.ReleaseGroup?.Id, releaseGroupId, StringComparison.OrdinalIgnoreCase));
        releases.Add(bestRelease);
    }
}
