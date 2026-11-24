using MusicLibraryManager.Models;
using MusicLibraryManager.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

internal static class PublicFunctions
{
    public static string MusicLibraryFolder;
    public static DirectoryInfo PlaylistFolder;
    public static string[] DroppedFiles;

    public static int NNInt(object v)
    {
        if (Convert.IsDBNull(v) || v.ToString() == "" || v == null)
        {
            return 0;
        }
        else
        {
            return Convert.ToInt32(v);
        }
    }

    public static string NNStr(object v)
    {
        if (Convert.IsDBNull(v) || v == null)
        {
            return "";
        }
        else
        {
            return Convert.ToString(v);
        }
    }

    public static string ListToString(List<MusicComment> comments, string seperator)
    {
        string result = "";

        foreach (MusicComment comment in comments)
        {
            result += comment.Value.ToString() + seperator;
        }

        return result.TrimEnd(seperator.ToCharArray()).Replace(Environment.NewLine, "");
    }

    public static string ListToString(List<MusicLyrics> lyrics, string seperator)
    {
        string result = "";

        foreach (MusicLyrics lyric in lyrics)
        {
            result += lyric.Text.ToString() + seperator;
        }

        return result.TrimEnd(seperator.ToCharArray()).Replace(Environment.NewLine, "");
    }

    public static void AddRowToGridview(DataGridView gridView, string mp3File, Color foreColor)
    {
        DataGridViewRow row;
        DataGridViewCell[] cells = new DataGridViewCell[4];

        cells[0] = new DataGridViewTextBoxCell
        {
            Value = mp3File
        };
        cells[0].Style.ForeColor = foreColor;

        cells[1] = new DataGridViewTextBoxCell();
        cells[1].Style.ForeColor = foreColor;

        cells[2] = new DataGridViewTextBoxCell();
        cells[2].Style.ForeColor = foreColor;

        cells[3] = new DataGridViewTextBoxCell();
        cells[3].Style.ForeColor = foreColor;

        MusicMetadata musicMetadata = MusicMetadataService.LoadMetadata(mp3File);

        cells[1].Value = musicMetadata.Artist;
        cells[2].Value = musicMetadata.Title;
        cells[3].Value = musicMetadata.Album;

        row = new DataGridViewRow();
        row.Cells.AddRange(cells);

        gridView.Rows.Add(row);
    }
}
