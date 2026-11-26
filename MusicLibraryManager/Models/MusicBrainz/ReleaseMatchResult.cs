using System.Collections.Generic;

namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class ReleaseMatchResult
{
    public required string ReleaseId { get; init; }
    public required string Album { get; init; }
    public int TotalTracks { get; init; }
    public List<TrackMatch> Matches { get; init; } = [];

    public int MatchedTracks => Matches.Count;
    public double Coverage => TotalTracks == 0 ? 0 : (double)MatchedTracks / TotalTracks;
}