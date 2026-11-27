using MusicLibraryManager.Models;
using MusicLibraryManager.Models.MusicBrainz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicLibraryManager.Services;

#nullable enable
internal sealed class MusicBrainzReleaseMatcher
{
    public ReleaseMatchResult Match(ReleaseDetails release, IReadOnlyCollection<LocalMusicTrack> localTracks)
    {
        ReleaseMatchResult result = new()
        {
            ReleaseId = release.Id,
            Album = release.Title,
            TotalTracks = release.Media.Sum(medium => medium.Tracks.Count)
        };
        
        bool isLiveAlbum = release.ReleaseGroup?.SecondaryTypes.Contains("Live", StringComparer.OrdinalIgnoreCase) == true;

        HashSet<string> artists = release.ArtistCredits
            .Select(credit => Normalize(credit.Name ?? credit.Artist?.Name))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        List<LocalMusicTrack> candidates = localTracks
            .Where(track => artists.Contains(Normalize(track.Artist)))
            .ToList();

        List<(ReleaseMedium Medium, ReleaseTrack Track)> releaseTracks = release.Media
            .SelectMany(medium => medium.Tracks.Select(track => (Medium: medium, Track: track)))
            .ToList();

        foreach ((ReleaseMedium medium, ReleaseTrack track) in releaseTracks)
        {
            string originalTitle = track.Title ?? track.Recording?.Title ?? "";
            (string cleanTitle, _, bool trackIsLive) = MusicTitleParser.Parse(originalTitle);

            bool isLiveRecording = isLiveAlbum || trackIsLive;
            string title = Normalize(cleanTitle);

            List<LocalMusicTrack> matches = candidates
                .Where(candidate => Normalize(candidate.Title) == title
                    && candidate.IsLiveVersion == isLiveRecording)
                .ToList();

            // Skip ambiguous matches instead of choosing arbitrarily.
            int matchingTrackCount = releaseTracks.Count(item =>
            {
                string itemTitle = item.Track.Title ?? item.Track.Recording?.Title ?? "";
                (string cleanItemTitle, _, bool itemIsLive) = MusicTitleParser.Parse(itemTitle);

                return Normalize(cleanItemTitle) == title
                    && (isLiveAlbum || itemIsLive) == isLiveRecording;
            });

            if (matches.Count != 1 || matchingTrackCount != 1)
            {
                continue;
            }

            LocalMusicTrack localTrack = matches[0];

            int? durationDifference = localTrack.LengthMilliseconds.HasValue && track.Length.HasValue
                ? Math.Abs(localTrack.LengthMilliseconds.Value - track.Length.Value)
                : null;

            TrackMatch match = new()
            {
                FilePath = localTrack.FilePath,
                RecordingId = track.Recording?.Id ?? "",
                TrackId = track.Id,
                Title = track.Title ?? "",
                DiscNumber = medium.Position,
                TrackNumber = track.Position,
                DurationDifferenceMilliseconds = durationDifference
            };

            result.Matches.Add(match);
        }

        return result;
    }

    private static string Normalize(string? value)
    {
        return (value ?? "")
            .Normalize(NormalizationForm.FormKC)
            .Replace('’', '\'')
            .Replace('‘', '\'')
            .Replace('`', '\'')
            .Replace('–', '-')
            .Replace('—', '-')
            .Replace('‑', '-')
            .Replace("&", "and")
            .Trim()
            .ToUpperInvariant();
    }
}
