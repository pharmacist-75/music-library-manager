using MusicLibraryManager.Models.MusicBrainz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MusicLibraryManager.Services;

#nullable enable
internal sealed class MusicBrainzCacheService
{
    private readonly string _cachePath;
    private readonly Dictionary<string, CacheEntry> _cache;
    private readonly object _syncRoot = new();

    public MusicBrainzCacheService()
    {
        _cachePath = Path.Combine(AppContext.BaseDirectory,"musicbrainz-cache.json");

        _cache = File.Exists(_cachePath)
            ? JsonSerializer.Deserialize<Dictionary<string, CacheEntry>>(File.ReadAllText(_cachePath)) ?? []
            : [];
    }

    public RecordingSearchResult? GetRecordings(string artist, string title)
    {
        return Get<RecordingSearchResult>($"recording:{Normalize(artist)}:{Normalize(title)}");
    }

    public void StoreRecordings(string artist, string title, RecordingSearchResult result)
    {
        Store($"recording:{Normalize(artist)}:{Normalize(title)}", result);
    }

    public T? Get<T>(string key) where T : class
    {
        lock (_syncRoot)
        {
            return _cache.TryGetValue(key, out CacheEntry? entry)
                ? JsonSerializer.Deserialize<T>(entry.Json)
                : null;
        }
    }

    public void Store<T>(string key, T value)
    {
        lock (_syncRoot)
        {
            _cache[key] = new CacheEntry
            {
                CachedAt = DateTime.UtcNow,
                Json = JsonSerializer.Serialize(value)
            };

            Save();
        }
    }

    public ReleaseDetails? GetRelease(string releaseId)
    {
        return Get<ReleaseDetails>($"release:{releaseId.Trim().ToLowerInvariant()}");
    }

    public void StoreRelease(string releaseId, ReleaseDetails release)
    {
        Store($"release:{releaseId.Trim().ToLowerInvariant()}", release);
    }

    private void Save()
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        string temporaryPath = _cachePath + ".tmp";

        File.WriteAllText(temporaryPath, JsonSerializer.Serialize(_cache, options));

        File.Move(temporaryPath, _cachePath, true);
    }

    private static string Normalize(string value)
    {
        return value.Trim().ToUpperInvariant();
    }
}