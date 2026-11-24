using IdSharp.Tagging.ID3v1;
using IdSharp.Tagging.ID3v2;
using IdSharp.Tagging.ID3v2.Frames;
using System;
using System.ComponentModel;
using System.Diagnostics;
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

    public static string ListToString(BindingList<IUnsynchronizedText> bl, string seperator)
    {
        string result = "";

        foreach (IUnsynchronizedText st in bl)
        {
            result += st.Text.ToString() + seperator;
        }

        return result.TrimEnd(seperator.ToCharArray()).Replace(Environment.NewLine, "");
    }

    public static string ListToString(BindingList<IComments> bl, string seperator)
    {
        string result = "";

        foreach (IComments st in bl)
        {
            result += st.Value.ToString() + seperator;
        }

        return result.TrimEnd(seperator.ToCharArray()).Replace(Environment.NewLine, "");
    }

    public static void AddRowToGridview(ref DataGridView gridView, string mp3File, Color foreColor, string priorityVersion)
    {
        ID3v1Tag id3v1 = new();
        ID3v2Tag id3v2 = new();
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

        if (File.Exists(mp3File))
        {
            try
            {
                id3v1 = new ID3v1Tag(mp3File);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading ID3v1 tag from file: {mp3File}: {ex.Message}");
            }

            try
            {
                id3v2 = new ID3v2Tag(mp3File);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading ID3v2 tag from file: {mp3File}: {ex.Message}");
            }
        }

        if (priorityVersion == "V1")
        {
            try
            {
                cells[1].Value = id3v1.Artist;
                cells[2].Value = id3v1.Title;
                cells[3].Value = id3v1.Album;
            }
            catch (Exception)
            {
                id3v1 = new ID3v1Tag
                {
                    Artist = id3v2.Artist,
                    Title = id3v2.Title,
                    Album = id3v2.Album
                };
                id3v1.Save(mp3File);

                cells[1].Value = id3v1.Artist;
                cells[2].Value = id3v1.Title;
                cells[3].Value = id3v1.Album;
                cells[1].Style.ForeColor = Color.Green;
                cells[2].Style.ForeColor = Color.Green;
                cells[3].Style.ForeColor = Color.Green;
            }
        }
        else
        {
            try
            {
                cells[1].Value = id3v2.Artist;
                cells[2].Value = id3v2.Title;
                cells[3].Value = id3v2.Album;
            }
            catch (Exception)
            {
                id3v2 = new ID3v2Tag
                {
                    Artist = id3v1.Artist,
                    Title = id3v1.Title,
                    Album = id3v1.Album
                };
                id3v2.Save(mp3File);

                cells[1].Value = id3v2.Artist;
                cells[2].Value = id3v2.Title;
                cells[3].Value = id3v2.Album;
                cells[1].Style.ForeColor = Color.Green;
                cells[2].Style.ForeColor = Color.Green;
                cells[3].Style.ForeColor = Color.Green;
            }
        }

        row = new DataGridViewRow();
        row.Cells.AddRange(cells);

        gridView.Rows.Add(row);
    }
}
