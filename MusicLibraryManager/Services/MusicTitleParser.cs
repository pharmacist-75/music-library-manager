using System.Text.RegularExpressions;

namespace MusicLibraryManager.Services;

internal static class MusicTitleParser
{
    private static readonly Regex LiveSuffix = new(
        @"\s*[\(\[](?<version>Live\b[^\)\]]*)[\)\]]\s*$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static (string Title, string? VersionInfo, bool IsLiveVersion) Parse(string title)
    {
        Match match = LiveSuffix.Match(title);

        if (!match.Success)
        {
            return (title.Trim(), null, false);
        }

        string cleanTitle = title[..match.Index].Trim();
        string versionInfo = match.Groups["version"].Value.Trim();

        return (cleanTitle, versionInfo, true);
    }
}