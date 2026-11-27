using System.Collections.Generic;

namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class BulkMatchResult
{
    public List<ReleaseMatchResult> Releases { get; init; } = [];
    public List<string> AmbiguousFiles { get; init; } = [];
    public List<string> UnmatchedFiles { get; init; } = [];
    public List<string> UnprocessedAlbumGroups { get; init; } = [];
    public bool IsComplete => UnprocessedAlbumGroups.Count == 0;
}