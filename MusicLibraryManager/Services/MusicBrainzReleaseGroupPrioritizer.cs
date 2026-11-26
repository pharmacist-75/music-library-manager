using MusicLibraryManager.Models.MusicBrainz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicLibraryManager.Services;

internal sealed class MusicBrainzReleaseGroupPrioritizer
{
    public List<ReleaseGroup> Prioritize(IEnumerable<ReleaseGroup> releaseGroups)
    {
        return releaseGroups
            .OrderBy(GetPriority)
            .ThenBy(group => group.Title, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public int GetPriority(ReleaseGroup group)
    {
        if (!string.Equals(group.PrimaryType, "Album", StringComparison.OrdinalIgnoreCase))
        {
            return 5;
        }

        List<string> types = group.SecondaryTypes ?? [];

        if (types.Count == 0)
        {
            return 0;
        }

        if (types.Contains("Live", StringComparer.OrdinalIgnoreCase))
        {
            return 2;
        }

        if (types.Contains("Compilation", StringComparer.OrdinalIgnoreCase))
        {
            return 3;
        }

        if (types.Contains("Demo", StringComparer.OrdinalIgnoreCase))
        {
            return 4;
        }

        return 1;
    }
}