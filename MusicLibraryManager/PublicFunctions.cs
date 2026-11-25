using MusicLibraryManager.Enums;
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

    public static void AddRowToGridview(DataGridView gridView, FileInfo mp3File, Color foreColor)
    {
        DataGridViewRow row;
        DataGridViewCell[] cells = new DataGridViewCell[13];

        cells[0] = new DataGridViewTextBoxCell
        {
            Value = mp3File.Name
        };
        cells[0].Style.ForeColor = foreColor;

        for(int i = 1; i < 13; i++)
        {
            cells[i] = new DataGridViewTextBoxCell();
            cells[i].Style.ForeColor = foreColor;
        }

        MusicMetadata musicMetadata = MusicMetadataService.LoadMetadata(mp3File.FullName);

        cells[(int)DataGridColumns.Directory].Value = mp3File.DirectoryName;
        cells[(int)DataGridColumns.Filename].Value = mp3File.Name;
        cells[(int)DataGridColumns.Artist].Value = musicMetadata.Artist;
        cells[(int)DataGridColumns.Title].Value = musicMetadata.Title;
        cells[(int)DataGridColumns.Album].Value = musicMetadata.Album;
        cells[(int)DataGridColumns.AlbumArtist].Value = musicMetadata.AlbumArtist;
        cells[(int)DataGridColumns.TrackNumber].Value = musicMetadata.TrackNumber;
        cells[(int)DataGridColumns.DiscNumber].Value = musicMetadata.DiscNumber;
        cells[(int)DataGridColumns.Year].Value = musicMetadata.Year;
        cells[(int)DataGridColumns.Genre].Value = musicMetadata.Genre;
        cells[(int)DataGridColumns.Comments].Value = ListToString(musicMetadata.Comments, "; ");
        cells[(int)DataGridColumns.Length].Value = FormatTrackLength(musicMetadata.LengthMilliseconds);
        cells[(int)DataGridColumns.Lyrics].Value = ListToString(musicMetadata.Lyrics, "; ");

        row = new DataGridViewRow();
        row.Cells.AddRange(cells);

        gridView.Rows.Add(row);
    }

    private static string FormatTrackLength(int? lengthInMilliseconds)
    {
        if (lengthInMilliseconds is null)
        {
            return string.Empty;
        }

        TimeSpan length = TimeSpan.FromMilliseconds(lengthInMilliseconds.Value);

        return length.ToString(@"h\:mm\:ss");
    }

    public static void InitializeColumns(DataGridView gridView)
    {
        GridColumnDefinition[] columns =
        [
            new GridColumnDefinition { Name = "Folder", Header = "Folder", Width = 100, Visible = false },
            new GridColumnDefinition { Name = "FileName", Header = "File name", Width = 300, Visible = true },
            new GridColumnDefinition { Name = "Title", Header = "Title", Width = 200, Visible = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill },
            new GridColumnDefinition { Name = "Artist", Header = "Artist", Width = 200, Visible = true },
            new GridColumnDefinition { Name = "Album", Header = "Album", Width = 200, Visible = true },
            new GridColumnDefinition { Name = "AlbumArtist", Header = "Album Artist", Width = 200, Visible = true },
            new GridColumnDefinition { Name = "TrackNumber", Header = "Track", Width = 70, Visible = false },
            new GridColumnDefinition { Name = "DiscNumber", Header = "Disc", Width = 70, Visible = false },
            new GridColumnDefinition { Name = "Year", Header = "Year", Width = 70, Visible = false },
            new GridColumnDefinition { Name = "Genre", Header = "Genre", Width = 150, Visible = false },
            new GridColumnDefinition { Name = "Comments", Header = "Comments", Width = 200, Visible = false },
            new GridColumnDefinition { Name = "Length", Header = "Length", Width = 100, Visible = false },
            new GridColumnDefinition { Name = "Lyrics", Header = "Lyrics", Width = 200, Visible = false }
        ];

        foreach (GridColumnDefinition column in columns)
        {
            DataGridViewTextBoxColumn gridColumn = new()
            {
                Name = column.Name,
                HeaderText = column.Header,
                Width = column.Width,
                Visible = column.Visible,
                AutoSizeMode = column.AutoSizeMode
            };

            gridView.Columns.Add(gridColumn);
        }
    }
}
