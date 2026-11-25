using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class ReleaseGroup
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("primary-type")]
    public string? PrimaryType { get; set; }

    [JsonPropertyName("secondary-types")]
    public List<string> SecondaryTypes { get; set; } = [];
}
