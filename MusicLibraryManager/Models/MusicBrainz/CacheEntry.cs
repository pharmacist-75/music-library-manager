using System;

namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class CacheEntry
{
    public DateTime CachedAt { get; set; }
    public string Json { get; set; } = "";
}