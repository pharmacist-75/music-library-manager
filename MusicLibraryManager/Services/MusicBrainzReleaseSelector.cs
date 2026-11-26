using MusicLibraryManager.Models.MusicBrainz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicLibraryManager.Services;

#nullable enable
internal sealed class MusicBrainzReleaseSelector
{
    public Release? Select(IEnumerable<Release> releases)
    {
        return releases
            .Where(release => !string.IsNullOrWhiteSpace(release.Id))
            .OrderBy(release => string.Equals(release.Status, "Official", StringComparison.OrdinalIgnoreCase) ? 0 : 1)
            .ThenBy(release => string.IsNullOrWhiteSpace(release.Date) ? 1 : 0)
            .ThenBy(release => release.Date, StringComparer.Ordinal)
            .ThenBy(release => release.Id, StringComparer.Ordinal)
            .FirstOrDefault();
    }
}
