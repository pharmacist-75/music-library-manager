using MusicLibraryManager.Models.MusicBrainz;
using System;
using System.IO;
using System.Net;
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

        return await GetMusicBrainzAsync<RecordingSearchResult>(url, cancellationToken);
    }

    public async Task<ReleaseDetails> GetReleaseAsync(string releaseId, CancellationToken cancellationToken = default)
    {
        string url = $"https://musicbrainz.org/ws/2/release/{Uri.EscapeDataString(releaseId)}?inc=recordings+artist-credits+release-groups&fmt=json";

        return await GetMusicBrainzAsync<ReleaseDetails>(url, cancellationToken);
    }

    public async Task<ArtistSearchResult> SearchArtistsAsync(string name, CancellationToken cancellationToken = default)
    {
        string query = $"artist:\"{name.Replace("\"", "\\\"")}\"";
        string url = $"https://musicbrainz.org/ws/2/artist/?query={Uri.EscapeDataString(query)}&fmt=json&limit=10";

        return await GetMusicBrainzAsync<ArtistSearchResult>(url, cancellationToken);
    }

    public async Task<ReleaseGroupSearchResult> GetAlbumGroupsAsync(string artistId, int offset = 0, CancellationToken cancellationToken = default)
    {
        string url = $"https://musicbrainz.org/ws/2/release-group/?artist={Uri.EscapeDataString(artistId)}&type=album&limit=100&offset={offset}&fmt=json";

        return await GetMusicBrainzAsync<ReleaseGroupSearchResult>(url, cancellationToken);
    }

    private async Task<T> GetMusicBrainzAsync<T>(string url, CancellationToken cancellationToken) where T : class
    {
        await _rateLimiter.WaitAsync(cancellationToken);

        try
        {
            for (int attempt = 0; attempt < 4; attempt++)
            {
                TimeSpan remaining = TimeSpan.FromMilliseconds(1100) - (DateTime.UtcNow - _lastRequest);

                if (remaining > TimeSpan.Zero)
                {
                    await Task.Delay(remaining, cancellationToken);
                }

                _lastRequest = DateTime.UtcNow;

                using HttpResponseMessage response = await _client.GetAsync(url, cancellationToken);

                if (response.StatusCode == HttpStatusCode.ServiceUnavailable && attempt < 3)
                {
                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt + 1)), cancellationToken);
                    continue;
                }

                response.EnsureSuccessStatusCode();

                await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                T? result = await JsonSerializer.DeserializeAsync<T>(stream, cancellationToken: cancellationToken);

                return result ?? throw new JsonException("MusicBrainz returned an empty response.");
            }

            throw new InvalidOperationException("MusicBrainz request failed.");
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
