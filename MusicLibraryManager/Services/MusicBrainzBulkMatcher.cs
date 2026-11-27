using MusicLibraryManager.Models;
using MusicLibraryManager.Models.MusicBrainz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicLibraryManager.Services;

internal sealed class MusicBrainzBulkMatcher
{
    private readonly MusicBrainzReleaseMatcher _releaseMatcher = new();

    public BulkMatchResult Match(IReadOnlyCollection<ReleaseDetails> releases, IReadOnlyCollection<LocalMusicTrack> localTracks)
    {
        List<(ReleaseDetails Release, ReleaseMatchResult Match)> editions = releases
            .Where(release => release.ReleaseGroup is not null)
            .GroupBy(release => release.Id, StringComparer.OrdinalIgnoreCase)
            .Select(group => group.First())
            .Select(release => (Release: release, Match: _releaseMatcher.Match(release, localTracks)))
            .Where(candidate => candidate.Match.MatchedTracks > 0)
            .ToList();

        MusicBrainzCacheService cache = new();

        Dictionary<string, int?> albumYears = editions
            .Select(candidate => candidate.Release.ReleaseGroup!.Id)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                groupId => groupId,
                groupId => GetReleaseYear(cache.GetReleaseGroup(groupId)?.FirstReleaseDate),
                StringComparer.OrdinalIgnoreCase);

        Dictionary<string, List<TrackMatch>> selectedMatches = new(StringComparer.OrdinalIgnoreCase);
        List<string> ambiguousFiles = [];
        List<string> unmatchedFiles = [];

        foreach (LocalMusicTrack file in localTracks)
        {
            List<(ReleaseDetails Release, ReleaseMatchResult Match)> candidates = editions
                .Where(candidate => candidate.Match.Matches.Any(match => string.Equals(match.FilePath, file.FilePath, StringComparison.OrdinalIgnoreCase)))
                .Where(candidate => IsSuitableAlbum(candidate.Release, file.IsLiveVersion))
                .ToList();

            List<IGrouping<string, (ReleaseDetails Release, ReleaseMatchResult Match)>> groups = candidates
                .GroupBy(candidate => candidate.Release.ReleaseGroup!.Id, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (groups.Count == 0)
            {
                unmatchedFiles.Add(file.FilePath);
                continue;
            }

            int? earliestYear = groups
                .Select(group => albumYears[group.Key])
                .Where(year => year.HasValue)
                .Select(year => year!.Value)
                .Cast<int?>()
                .Min();

            List<IGrouping<string, (ReleaseDetails Release, ReleaseMatchResult Match)>> bestGroups = groups
                .Where(group => !earliestYear.HasValue || albumYears[group.Key] == earliestYear || !albumYears[group.Key].HasValue)
                .ToList();

            if (bestGroups.Count > 1)
            {
                ambiguousFiles.Add(file.FilePath);
            }

            foreach (IGrouping<string, (ReleaseDetails Release, ReleaseMatchResult Match)> group in bestGroups)
            {
                (ReleaseDetails Release, ReleaseMatchResult Match) edition = group
                    .OrderByDescending(candidate => candidate.Match.MatchedTracks)
                    .ThenBy(candidate => GetReleaseYear(candidate.Release.Date) ?? int.MaxValue)
                    .First();

                TrackMatch track = edition.Match.Matches.First(match => string.Equals(match.FilePath, file.FilePath, StringComparison.OrdinalIgnoreCase));

                if (!selectedMatches.TryGetValue(edition.Release.Id, out List<TrackMatch>? matches))
                {
                    matches = [];
                    selectedMatches.Add(edition.Release.Id, matches);
                }

                matches.Add(track);
            }
        }

        List<ReleaseMatchResult> results = editions
            .Where(candidate => selectedMatches.ContainsKey(candidate.Release.Id))
            .Select(candidate => new ReleaseMatchResult
            {
                ReleaseId = candidate.Release.Id,
                Album = candidate.Release.ReleaseGroup!.Title,
                TotalTracks = candidate.Match.TotalTracks,
                Matches = selectedMatches[candidate.Release.Id]
            })
            .OrderBy(result => result.Album)
            .ToList();

        return new BulkMatchResult
        {
            Releases = results,
            AmbiguousFiles = ambiguousFiles,
            UnmatchedFiles = unmatchedFiles
        };
    }

    private static bool IsSuitableAlbum(ReleaseDetails release, bool isLiveVersion)
    {
        ReleaseGroup? group = release.ReleaseGroup;

        if (group is null || !string.Equals(group.PrimaryType, "Album", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        bool isLiveAlbum = group.SecondaryTypes.Contains("Live", StringComparer.OrdinalIgnoreCase);

        if (isLiveVersion)
        {
            return isLiveAlbum;
        }

        return group.SecondaryTypes.Count == 0;
    }

    private static int? GetReleaseYear(string? date)
    {
        return date is not null && date.Length >= 4 && int.TryParse(date[..4], out int year)
            ? year
            : null;
    }
}
