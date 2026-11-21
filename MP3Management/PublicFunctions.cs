Imports System.ComponentModel
Imports IdSharp.Tagging.ID3v1
Imports IdSharp.Tagging.ID3v2
Imports System.IO

Module PublicFunctions
    Public DriveLetter As String
    Public PlaylistFolder As DirectoryInfo
    Public DroppedFiles() As String

    Function NNInt(ByVal v As Object) As Integer

        If IsDBNull(v) OrElse v.ToString = "" OrElse IsNothing(v) Then
            Return 0
        Else
            Return CInt(v)
        End If

    End Function

    Function NNStr(ByVal v As Object) As String

        If IsDBNull(v) OrElse IsNothing(v) Then
            Return ""
        Else
            Return CStr(v)
        End If

    End Function

    Function ListToString(ByVal bl As BindingList(Of Frames.IUnsynchronizedText), seperator As String) As String
        Dim st As Frames.IUnsynchronizedText
        Dim result As String = ""

        For Each st In bl
            result += st.Text.ToString() & seperator
        Next

        Return result.TrimEnd(seperator).Replace(Environment.NewLine, "")

    End Function

    Function ListToString(ByVal bl As BindingList(Of Frames.IComments), seperator As String) As String
        Dim st As Frames.IComments
        Dim result As String = ""

        For Each st In bl
            result += st.Value.ToString() & seperator
        Next

        Return result.TrimEnd(seperator).Replace(Environment.NewLine, "")

    End Function

    Sub AddRowToGridview(ByRef gridView As DataGridView, ByVal mp3File As String, ByVal foreColor As Color, priorityVersion As String)
        Dim id3v1 As New ID3v1Tag
        Dim id3v2 As New ID3v2Tag
        Dim row As DataGridViewRow
        Dim cells(3) As DataGridViewCell

        cells(0) = New DataGridViewTextBoxCell()
        cells(0).Value = mp3File
        cells(0).Style.ForeColor = foreColor

        cells(1) = New DataGridViewTextBoxCell()
        cells(1).Style.ForeColor = foreColor

        cells(2) = New DataGridViewTextBoxCell()
        cells(2).Style.ForeColor = foreColor

        cells(3) = New DataGridViewTextBoxCell()
        cells(3).Style.ForeColor = foreColor

        If File.Exists(mp3File) Then
            Try
                id3v1 = New ID3v1Tag(mp3File)
            Catch ex As Exception
                Beep()
            End Try

            Try
                id3v2 = New ID3v2Tag(mp3File)
            Catch ex As Exception
                Beep()
            End Try
        End If

        If priorityVersion = "V1" Then
            Try
                cells(1).Value = id3v1.Artist
                cells(2).Value = id3v1.Title
                cells(3).Value = id3v1.Album
            Catch ex As Exception

                id3v1 = New ID3v1Tag()
                id3v1.Artist = id3v2.Artist
                id3v1.Title = id3v2.Title
                id3v1.Album = id3v2.Album
                id3v1.Save(mp3File)

                cells(1).Value = id3v1.Artist
                cells(2).Value = id3v1.Title
                cells(3).Value = id3v1.Album
                cells(1).Style.ForeColor = Color.Green
                cells(2).Style.ForeColor = Color.Green
                cells(3).Style.ForeColor = Color.Green
            End Try
        Else
            Try
                cells(1).Value = id3v2.Artist
                cells(2).Value = id3v2.Title
                cells(3).Value = id3v2.Album
            Catch ex As Exception

                id3v2 = New ID3v2Tag()
                id3v2.Artist = id3v1.Artist
                id3v2.Title = id3v1.Title
                id3v2.Album = id3v1.Album
                id3v2.Save(mp3File)

                cells(1).Value = id3v2.Artist
                cells(2).Value = id3v2.Title
                cells(3).Value = id3v2.Album
                cells(1).Style.ForeColor = Color.Green
                cells(2).Style.ForeColor = Color.Green
                cells(3).Style.ForeColor = Color.Green
            End Try
        End If

        row = New DataGridViewRow()
        row.Cells.AddRange(cells)

        gridView.Rows.Add(row)

    End Sub

End Module
