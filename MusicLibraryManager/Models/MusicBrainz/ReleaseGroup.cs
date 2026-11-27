using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

#nullable enable
public sealed class ReleaseGroup
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("primary-type")]
    public string? PrimaryType { get; set; }

    [JsonPropertyName("secondary-types")]
    public List<string> SecondaryTypes { get; set; } = [];

    [JsonPropertyName("first-release-date")]
    public string? FirstReleaseDate { get; set; }
}
