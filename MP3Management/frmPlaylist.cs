Imports System.IO
Imports System.Text

Public Class frmPlaylist

    Private Sub frmPlaylist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim availablePlayLists As FileInfo()
        Dim file As String

        availablePlayLists = playlistFolder.GetFiles("*.m3u")

        cmbPlaylist.Items.AddRange(availablePlayLists)
        cmbPlaylist.Items.Add("<newPlayList.m3u>")

        cmbPlaylist.SelectedText = "<newPlayList.m3u>"
        chkMerge.Visible = False

        For Each file In droppedFiles
            AddRowToGridview(dgvSongs, file, Color.Green, "V1")
        Next

    End Sub
    
    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click

        Me.Dispose()

    End Sub

    Private Sub cmdOk_Click(sender As Object, e As EventArgs) Handles cmdOk.Click
        Dim playList As String
        Dim sw As StreamWriter
        Dim row As DataGridViewRow
        Dim mp3File As String
        Dim skip As Boolean

        If cmbPlaylist.Text = "<newPlayList.m3u>" Then
            playList = InputBox("Name of PlayList: ")
            If Not playList.ToLower().EndsWith(".m3u") Then
                playList += ".m3u"
            End If
        Else
            playList = cmbPlaylist.Text
        End If

        If playList <> "" Then
            sw = New StreamWriter(PlaylistFolder.FullName & playList, False)
            For Each row In dgvSongs.Rows
                If chkMerge.Checked Then
                    skip = (row.Cells(0).Style.ForeColor = Color.Red)
                    skip = skip Or (row.Cells(0).Style.ForeColor = Color.LightGray)
                Else
                    skip = (row.Cells(0).Style.ForeColor <> Color.Green)
                End If
                If Not skip Then
                    mp3File = ".." & row.Cells(0).Value.Substring(2)
                    sw.WriteLine(mp3File)
                End If
            Next
            sw.Close()
        End If

        Me.Dispose()

    End Sub

    Private Sub cmbPlaylist_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlaylist.SelectedIndexChanged

        chkMerge.Checked = False
        If File.Exists(playlistFolder.FullName & cmbPlaylist.Text) Then
            chkMerge.Checked = True
        End If

        chkMerge.Visible = (cmbPlaylist.Text <> "<newPlayList.m3u>")
        
    End Sub

    Private Sub chkMerge_CheckedChanged(sender As Object, e As EventArgs) Handles chkMerge.CheckedChanged
        Dim sr As StreamReader
        Dim mp3File As String
        Dim rowColor As Color
        Dim row As DataGridViewRow
        Dim rowRemoved As Boolean

        If chkMerge.Checked = False Then
            Do
                rowRemoved = False
                For Each row In dgvSongs.Rows
                    If row.Cells(0).Style.ForeColor <> Color.Green Then
                        dgvSongs.Rows.Remove(row)
                        rowRemoved = True
                        Exit For
                    End If
                Next
            Loop While rowRemoved
        Else
            sr = New StreamReader(PlaylistFolder.FullName & cmbPlaylist.Text)
            While sr.Peek() >= 0
                mp3File = sr.ReadLine()

                mp3File = DriveLetter & mp3File.Substring(2)

                If File.Exists(mp3File) Then
                    If DroppedFiles.Contains(mp3File) Then
                        rowColor = Color.Red
                    Else
                        rowColor = Color.Blue
                    End If
                Else
                    rowColor = Color.LightGray
                End If
                AddRowToGridview(dgvSongs, mp3File, rowColor, "V1")
            End While
            sr.Close()
        End If

    End Sub

    Private Sub cmdValidatePlaylists_Click(sender As Object, e As EventArgs) Handles cmdValidatePlaylists.Click
        Dim availablePlayLists As FileInfo()
        Dim playList As FileInfo
        Dim sr As StreamReader
        Dim mp3File As FileInfo
        Dim fileName As String

        availablePlayLists = PlaylistFolder.GetFiles("*.m3u")

        For Each playList In availablePlayLists
            sr = New StreamReader(playList.FullName)
            While sr.Peek() >= 0
                fileName = sr.ReadLine().Replace("..\", DriveLetter & "\")
                mp3File = New FileInfo(fileName)
                If Not mp3File.Exists Then
                    Debug.Print(playList.Name & " missing file " & mp3File.FullName)
                End If
            End While
            sr.Close()
        Next

        Debug.Print("finished.")

    End Sub

End Class