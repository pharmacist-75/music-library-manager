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
        List<ReleaseMatchResult> results = releases
            .GroupBy(release => release.Id, StringComparer.OrdinalIgnoreCase)
            .Select(group => _releaseMatcher.Match(group.First(), localTracks))
            .Where(result => result.MatchedTracks > 0)
            .OrderByDescending(result => result.MatchedTracks)
            .ThenByDescending(result => result.Coverage)
            .ToList();

        Dictionary<string, int> matchCounts = results
            .SelectMany(result => result.Matches)
            .GroupBy(match => match.FilePath, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.Count(), StringComparer.OrdinalIgnoreCase);

        List<string> ambiguousFiles = matchCounts
            .Where(entry => entry.Value > 1)
            .Select(entry => entry.Key)
            .ToList();

        List<string> unmatchedFiles = localTracks
            .Where(track => !matchCounts.ContainsKey(track.FilePath))
            .Select(track => track.FilePath)
            .ToList();

        return new BulkMatchResult
        {
            Releases = results,
            AmbiguousFiles = ambiguousFiles,
            UnmatchedFiles = unmatchedFiles
        };
    }
}
