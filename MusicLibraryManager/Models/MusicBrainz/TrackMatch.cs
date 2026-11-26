namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class TrackMatch
{
    public required string FilePath { get; init; }
    public required string RecordingId { get; init; }
    public required string TrackId { get; init; }
    public required string Title { get; init; }
    public int DiscNumber { get; init; }
    public int TrackNumber { get; init; }
    public int? DurationDifferenceMilliseconds { get; init; }
}