namespace MusicLibraryManager.Models;

public sealed class LocalMusicTrack
{
    public required string FilePath { get; init; }
    public required string Artist { get; init; }
    public required string Title { get; init; }
    public int? LengthMilliseconds { get; init; }
}