using MusicLibraryManager.Models;
using MusicLibraryManager.Models.MusicBrainz;
using System;
using System.Collections.Generic;
using System.Linq;

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
            string title = Normalize(track.Title ?? track.Recording?.Title);

            List<LocalMusicTrack> matches = candidates
                .Where(candidate => Normalize(candidate.Title) == title)
                .ToList();

            // Skip ambiguous matches instead of choosing arbitrarily.
            if (matches.Count != 1 || releaseTracks.Count(item => Normalize(item.Track.Title ?? item.Track.Recording?.Title) == title) != 1)
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
        return (value ?? "").Trim().ToUpperInvariant();
    }
}
