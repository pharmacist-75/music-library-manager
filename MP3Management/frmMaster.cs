Imports System.IO
Imports System.Text
Imports System.ComponentModel
Imports IdSharp.Tagging.ID3v1
Imports IdSharp.Tagging.ID3v2

Public Class frmMaster
    Private _action As String
    Const MaxLengthArtistV1 As Integer = 30
    
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim files As FileInfo()
        'Dim fi As FileInfo
        'Dim parts() As String
        'Dim newFileName As String

        'files = New DirectoryInfo("I:\").GetFiles("*Rock'n'roll Realschule*.mp3")
        'For Each fi In files
        '    parts = fi.Name.Split("-")


        '    newFileName = parts(0).Trim() & " - " & parts(3).Trim().Replace(".mp3", " (Mtv Unplugged).mp3")
        '    Debug.Print(fi.FullName.Replace(fi.Name, newFileName))
        '    If fi.FullName.Replace(fi.Name, newFileName) <> fi.FullName Then
        '        Try
        '            fi.MoveTo(fi.FullName.Replace(fi.Name, newFileName))
        '        Catch ex As Exception
        '            Beep()
        '        End Try
        '    End If
        'Next

        AllowDrop = True

        txtDriveLetter.Text = GetSetting("MP3Management", "Settings", "DriveLetter", Application.StartupPath.Substring(0, 2))
        DriveLetter = txtDriveLetter.Text

        txtPlaylistFolder.Text = GetSetting("MP3Management", "Settings", "PlaylistFolder", "")
        If txtPlaylistFolder.Text <> "" Then
            PlaylistFolder = New DirectoryInfo(txtPlaylistFolder.Text)
        End If

    End Sub

    Private Sub Form1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop

        DroppedFiles = e.Data.GetData(DataFormats.FileDrop)

        frmPlaylist.Show()

    End Sub

    Private Sub Form1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        Dim row As DataGridViewRow
        Dim id3v1 As ID3v1Tag
        Dim id3v2 As ID3v2Tag
        Dim updateArtist As Boolean
        Dim updateTitle As Boolean
        Dim updateAlbum As Boolean

        For Each row In dgvSongs.Rows
            updateArtist = ((row.Cells(1).Style.ForeColor = Color.Red OrElse row.Cells(1).Style.ForeColor = Color.Blue) AndAlso Not IsNothing(row.Cells(1).Value))
            updateTitle = ((row.Cells(2).Style.ForeColor = Color.Red OrElse row.Cells(2).Style.ForeColor = Color.Blue) AndAlso Not IsNothing(row.Cells(2).Value))
            updateAlbum = ((row.Cells(3).Style.ForeColor = Color.Red OrElse row.Cells(3).Style.ForeColor = Color.Blue) AndAlso Not IsNothing(row.Cells(3).Value))

            If updateAlbum Or updateArtist Or updateTitle Then
                If optV1.Checked Then
                    id3v1 = New ID3v1Tag(row.Cells(0).Value.ToString())

                    If updateArtist Then
                        id3v1.Artist = row.Cells(1).Value.ToString()
                    End If
                    If updateTitle Then
                        id3v1.Title = row.Cells(2).Value.ToString()
                    End If
                    If updateAlbum Then
                        id3v1.Album = row.Cells(3).Value.ToString()
                    End If
                    id3v1.Save(row.Cells(0).Value.ToString())
                Else
                    id3v2 = New ID3v2Tag(row.Cells(0).Value.ToString())

                    If updateArtist Then
                        id3v2.Artist = row.Cells(1).Value.ToString()
                    End If
                    If updateTitle Then
                        id3v2.Title = row.Cells(2).Value.ToString()
                    End If
                    If updateAlbum Then
                        id3v2.Album = row.Cells(3).Value.ToString()
                    End If
                    id3v2.Save(row.Cells(0).Value.ToString())
                End If
            End If
        Next

        MessageBox.Show("Update done!")

        Select Case _action
            Case "LOAD"
                cmdLoad_Click(Me, New EventArgs())
            Case "SHOW_UNCLEAR"
                cmdShowUnclear_Click(Me, New EventArgs())
        End Select

    End Sub

    Private Sub cmdCreateCSV_Click(sender As Object, e As EventArgs) Handles cmdCreateCSV.Click
        Dim mp3Files() As FileInfo
        Dim mp3File As FileInfo
        Dim id3 As ID3v2Tag
        Dim sw As StreamWriter
        Dim length As TimeSpan

        mp3Files = New DirectoryInfo(txtDriveLetter.Text).GetFiles("*.MP3")

        sw = New StreamWriter(txtDriveLetter.Text & "\ID3_Tags_2.CSV", False, Encoding.UTF8)
        sw.WriteLine("Filename;Album;Artist;Comment;Disc;Genre;Length;Lyrics;PaddingSize;Song;Title;Track;Year")
        For Each mp3File In mp3Files
            Try
                id3 = New ID3v2Tag(mp3File.FullName)

                sw.Write(mp3File.FullName)
                sw.Write(";" & NNStr(id3.Album))
                sw.Write(";" & NNStr(id3.Artist).Replace(Environment.NewLine, ""))
                sw.Write(";" & ListToString(id3.CommentsList, " "))
                sw.Write(";" & NNStr(id3.DiscNumber).Replace(Environment.NewLine, ""))
                sw.Write(";" & NNStr(id3.Genre).Replace(Environment.NewLine, ""))
                If Not IsNothing(id3.LengthMilliseconds) Then
                    length = New TimeSpan(0, 0, 0, 0, NNInt(id3.LengthMilliseconds))
                    sw.Write(";" & length.Hours & ":" & length.Minutes & ":" & length.Seconds & ":" & length.Milliseconds)
                Else
                    sw.Write(";")
                End If
                sw.Write(";" & ListToString(id3.UnsynchronizedLyricsList, " "))
                sw.Write(";") ' & id3.PaddingSize.ToString())
                sw.Write(";" & NNStr(id3.Title).Replace(Environment.NewLine, ""))
                sw.Write(";" & NNStr(id3.Title).Replace(Environment.NewLine, ""))
                sw.Write(";" & NNStr(id3.TrackNumber).Replace(Environment.NewLine, ""))
                sw.Write(";" & NNStr(id3.Year))
            Catch ex As Exception
                sw.Write(";M I S S I N G  T A G")
            Finally
                sw.WriteLine()
            End Try
        Next
        sw.Close()

    End Sub

    Private Sub cmdBrowse_Click(sender As Object, e As EventArgs) Handles cmdBrowse.Click
        Dim dr As DialogResult

        FolderBrowserDialog1.SelectedPath = txtDriveLetter.Text & "\PLAYLIST\"
        dr = FolderBrowserDialog1.ShowDialog()

        If dr = DialogResult.OK Then
            txtPlaylistFolder.Text = FolderBrowserDialog1.SelectedPath & "\"

            SaveSetting("MP3Management", "Settings", "PlaylistFolder", txtPlaylistFolder.Text)

            If txtPlaylistFolder.Text <> "" Then
                PlaylistFolder = New DirectoryInfo(txtPlaylistFolder.Text)
            End If
        End If

    End Sub

    Private Sub txtDriveLetter_TextChanged(sender As Object, e As EventArgs) Handles txtDriveLetter.TextChanged
        DriveLetter = txtDriveLetter.Text
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Dim mp3Files As FileInfo()
        Dim mp3File As FileInfo

        dgvSongs.Rows.Clear()

        mp3Files = New DirectoryInfo(DriveLetter & "\" & txtFolder.Text).GetFiles(txtFilter.Text)

        dgvSongs.Visible = False
        labStatus.Visible = True

        For Each mp3File In mp3Files
            labStatus.Text = mp3File.FullName & "..."
            Application.DoEvents()

            If optV1.Checked Then
                AddRowToGridview(dgvSongs, mp3File.FullName, Color.Black, "V1")
            Else
                AddRowToGridview(dgvSongs, mp3File.FullName, Color.Black, "V2")
            End If
        Next

        dgvSongs.Sort(dgvSongs.Columns(0), ListSortDirection.Ascending)

        labStatus.Visible = False
        dgvSongs.Visible = True
        _action = "LOAD"

    End Sub

    Private Sub dgvSongs_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSongs.CellDoubleClick
        Dim parts As String()
        Dim mp3File As FileInfo
        Dim artist As String
        Dim i As Integer
        Dim myDelims As String() = New String() {" - "}
        Dim newFileName As String

        If e.ColumnIndex = 0 Then
            mp3File = New FileInfo(dgvSongs.Rows(e.RowIndex).Cells(0).Value)
            parts = mp3File.Name.Replace(mp3File.Extension, "").Split(myDelims, StringSplitOptions.None)

            artist = ""
            If parts.Length > 2 Then
                For i = 0 To parts.Length - 2
                    artist += parts(i) & "-"
                Next
                artist = artist.Trim("-")
            Else
                artist = parts(0).Trim()
            End If

            dgvSongs.Rows(e.RowIndex).Cells(1).Value = artist
            dgvSongs.Rows(e.RowIndex).Cells(1).Style.ForeColor = Color.Red
            dgvSongs.Rows(e.RowIndex).Cells(2).Value = parts(parts.Length - 1).Trim()
            dgvSongs.Rows(e.RowIndex).Cells(2).Style.ForeColor = Color.Red
        End If

        If e.ColumnIndex = 1 Or e.ColumnIndex = 2 Then
            mp3File = New FileInfo(dgvSongs.Rows(e.RowIndex).Cells(0).Value)

            newFileName = dgvSongs.Rows(e.RowIndex).Cells(1).Value
            newFileName &= " - "
            newFileName &= dgvSongs.Rows(e.RowIndex).Cells(2).Value
            newFileName &= ".mp3"

            mp3File.MoveTo(mp3File.DirectoryName & "\" & newFileName.Replace("?", "").Replace("/", ""))

            dgvSongs.Rows(e.RowIndex).Cells(0).Value = mp3File.FullName
        End If

    End Sub

    Private Sub dgvSongs_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSongs.CellEndEdit

        dgvSongs.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red

    End Sub

    Private Sub cmdShowUnclear_Click(sender As Object, e As EventArgs) Handles cmdShowUnclear.Click
        Dim mp3Files As FileInfo()
        Dim mp3File As FileInfo
        Dim id3v2 As ID3v2Tag
        Dim id3v1 As ID3v1Tag
        Dim parts As String()
        Dim fileNameArtist As String
        Dim tagArtist As String
        Dim i As Integer
        Dim version As String
        Dim selectedVersion As String

        dgvSongs.Rows.Clear()

        mp3Files = New DirectoryInfo(DriveLetter & "\" & txtFolder.Text).GetFiles(txtFilter.Text)

        dgvSongs.Visible = False
        labStatus.Visible = True

        If optV1.Checked Then
            selectedVersion = "V1"
        Else
            selectedVersion = "V2"
        End If

        For Each mp3File In mp3Files
            labStatus.Text = mp3File.FullName & "..."
            Application.DoEvents()

            Dim myDelims As String() = New String() {" - "}

            parts = mp3File.Name.Replace(mp3File.Extension, "").Split(myDelims, StringSplitOptions.None)

            tagArtist = ""
            version = ""
            If optV1.Checked Then
                id3v1 = New ID3v1Tag(mp3File.FullName)
                If Not IsNothing(id3v1.Title) Then
                    tagArtist = id3v1.Title.ToUpper()
                    version = "V1"
                Else
                    id3v2 = New ID3v2Tag(mp3File.FullName)
                    If Not IsNothing(id3v2.Title) Then
                        tagArtist = id3v2.Title.ToUpper()
                        version = "V2"
                    End If
                End If
            Else
                id3v2 = New ID3v2Tag(mp3File.FullName)
                If Not IsNothing(id3v2.Title) Then
                    tagArtist = id3v2.Title.ToUpper()
                    version = "V2"
                Else
                    id3v1 = New ID3v1Tag(mp3File.FullName)
                    If Not IsNothing(id3v1.Title) Then
                        tagArtist = id3v1.Title.ToUpper()
                        version = "V1"
                    End If
                End If
            End If

            fileNameArtist = ""
            'If parts.Length > 2 Then
            '    For i = 0 To parts.Length - 2
            '        fileNameArtist += parts(i) & "-"
            '    Next
            '    fileNameArtist = fileNameArtist.Trim("-")
            'Else
            'fileNameArtist = parts(0).Trim()
            fileNameArtist = parts(parts.Length - 1).Trim()
            'End If

            If version = "" Then
                AddRowToGridview(dgvSongs, mp3File.FullName, Color.Red, selectedVersion)
            Else
                If version <> selectedVersion Then
                    AddRowToGridview(dgvSongs, mp3File.FullName, Color.Blue, version)
                Else
                    If selectedVersion = "V1" AndAlso fileNameArtist.Trim().Length > maxLengthArtistV1 Then
                        fileNameArtist = fileNameArtist.Trim().Substring(0, maxLengthArtistV1)
                    End If
                    If fileNameArtist.Trim().ToUpper() <> tagArtist Then
                        'If fileNameArtist.Trim() <> tagArtist Then
                        AddRowToGridview(dgvSongs, mp3File.FullName, Color.Black, version)
                    End If
                End If
            End If
        Next

        dgvSongs.Sort(dgvSongs.Columns(0), ListSortDirection.Ascending)

        labStatus.Visible = False
        dgvSongs.Visible = True
        _action = "SHOW_UNCLEAR"

    End Sub

End Class
