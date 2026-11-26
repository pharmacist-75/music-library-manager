using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class ReleaseDetails
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("artist-credit")]
    public List<ArtistCredit> ArtistCredits { get; set; } = [];

    [JsonPropertyName("release-group")]
    public ReleaseGroup? ReleaseGroup { get; set; }

    [JsonPropertyName("media")]
    public List<ReleaseMedium> Media { get; set; } = [];
}