using MusicLibraryManager.Models.MusicBrainz;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MusicLibraryManager.Services;

#nullable enable
internal sealed class MusicBrainzService : IDisposable
{
    private readonly HttpClient _client = new();
    private readonly SemaphoreSlim _rateLimiter = new(1, 1);
    private DateTime _lastRequest = DateTime.MinValue;

    public MusicBrainzService()
    {
        _client.DefaultRequestHeaders.UserAgent.ParseAdd("MusicLibraryManager/1.4.0 (https://github.com/pharmacist-75/music-library-manager)");
    }

    public async Task<RecordingSearchResult> SearchRecordingsAsync(string artist, string title, CancellationToken cancellationToken = default)
    {
        string query = $"artist:\"{artist}\" AND recording:\"{title}\"";

        string url = $"https://musicbrainz.org/ws/2/recording/?query={Uri.EscapeDataString(query)}&fmt=json&limit=25";

        await _rateLimiter.WaitAsync(cancellationToken);

        try
        {
            TimeSpan remaining = TimeSpan.FromSeconds(1) - (DateTime.UtcNow - _lastRequest);

            if (remaining > TimeSpan.Zero)
            {
                await Task.Delay(remaining, cancellationToken);
            }

            _lastRequest = DateTime.UtcNow;

            using HttpResponseMessage response = await _client.GetAsync(url, cancellationToken);

            response.EnsureSuccessStatusCode();

            await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            RecordingSearchResult? result = await JsonSerializer.DeserializeAsync<RecordingSearchResult>(stream, cancellationToken: cancellationToken);

            return result ?? new RecordingSearchResult();
        }
        finally
        {
            _rateLimiter.Release();
        }
    }

    public async Task<ReleaseDetails> GetReleaseAsync(string releaseId, CancellationToken cancellationToken = default)
    {
        string url = $"https://musicbrainz.org/ws/2/release/{Uri.EscapeDataString(releaseId)}?inc=recordings+artist-credits+release-groups&fmt=json";

        await _rateLimiter.WaitAsync(cancellationToken);

        try
        {
            TimeSpan remaining = TimeSpan.FromSeconds(1) - (DateTime.UtcNow - _lastRequest);

            if (remaining > TimeSpan.Zero)
            {
                await Task.Delay(remaining, cancellationToken);
            }

            _lastRequest = DateTime.UtcNow;

            using HttpResponseMessage response = await _client.GetAsync(url, cancellationToken);

            response.EnsureSuccessStatusCode();

            await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            ReleaseDetails? release = await JsonSerializer.DeserializeAsync<ReleaseDetails>(stream, cancellationToken: cancellationToken);

            return release ?? throw new JsonException("MusicBrainz returned an empty release.");
        }
        finally
        {
            _rateLimiter.Release();
        }
    }

    public void Dispose()
    {
        _client.Dispose();
        _rateLimiter.Dispose();
    }
}
