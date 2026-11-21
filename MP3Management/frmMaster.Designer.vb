<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaster
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdCreateCSV = New System.Windows.Forms.Button()
        Me.labDrive = New System.Windows.Forms.Label()
        Me.labPlaylistFolder = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.txtPlaylistFolder = New System.Windows.Forms.TextBox()
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.txtDriveLetter = New System.Windows.Forms.TextBox()
        Me.dgvSongs = New System.Windows.Forms.DataGridView()
        Me.colFileName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colArtist = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAlbum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdLoad = New System.Windows.Forms.Button()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.labFilter = New System.Windows.Forms.Label()
        Me.cmdShowUnclear = New System.Windows.Forms.Button()
        Me.optV1 = New System.Windows.Forms.RadioButton()
        Me.optV2 = New System.Windows.Forms.RadioButton()
        Me.txtFolder = New System.Windows.Forms.TextBox()
        CType(Me.dgvSongs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Location = New System.Drawing.Point(396, 25)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(75, 23)
        Me.cmdUpdate.TabIndex = 0
        Me.cmdUpdate.Text = "Update"
        Me.cmdUpdate.UseVisualStyleBackColor = True
        '
        'cmdCreateCSV
        '
        Me.cmdCreateCSV.Location = New System.Drawing.Point(297, 25)
        Me.cmdCreateCSV.Name = "cmdCreateCSV"
        Me.cmdCreateCSV.Size = New System.Drawing.Size(93, 23)
        Me.cmdCreateCSV.TabIndex = 2
        Me.cmdCreateCSV.Text = "Create CSV"
        Me.cmdCreateCSV.UseVisualStyleBackColor = True
        '
        'labDrive
        '
        Me.labDrive.AutoSize = True
        Me.labDrive.Location = New System.Drawing.Point(12, 9)
        Me.labDrive.Name = "labDrive"
        Me.labDrive.Size = New System.Drawing.Size(35, 13)
        Me.labDrive.TabIndex = 3
        Me.labDrive.Text = "Drive:"
        '
        'labPlaylistFolder
        '
        Me.labPlaylistFolder.AutoSize = True
        Me.labPlaylistFolder.Location = New System.Drawing.Point(12, 31)
        Me.labPlaylistFolder.Name = "labPlaylistFolder"
        Me.labPlaylistFolder.Size = New System.Drawing.Size(74, 13)
        Me.labPlaylistFolder.TabIndex = 4
        Me.labPlaylistFolder.Text = "Playlist Folder:"
        '
        'txtPlaylistFolder
        '
        Me.txtPlaylistFolder.Location = New System.Drawing.Point(92, 28)
        Me.txtPlaylistFolder.Name = "txtPlaylistFolder"
        Me.txtPlaylistFolder.Size = New System.Drawing.Size(148, 20)
        Me.txtPlaylistFolder.TabIndex = 5
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(246, 26)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(25, 23)
        Me.cmdBrowse.TabIndex = 6
        Me.cmdBrowse.Text = "..."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'txtDriveLetter
        '
        Me.txtDriveLetter.Location = New System.Drawing.Point(92, 6)
        Me.txtDriveLetter.Name = "txtDriveLetter"
        Me.txtDriveLetter.Size = New System.Drawing.Size(26, 20)
        Me.txtDriveLetter.TabIndex = 7
        '
        'dgvSongs
        '
        Me.dgvSongs.AllowUserToAddRows = False
        Me.dgvSongs.AllowUserToResizeRows = False
        Me.dgvSongs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSongs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSongs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colFileName, Me.colArtist, Me.colTitle, Me.colAlbum})
        Me.dgvSongs.Location = New System.Drawing.Point(12, 55)
        Me.dgvSongs.Name = "dgvSongs"
        Me.dgvSongs.Size = New System.Drawing.Size(1094, 309)
        Me.dgvSongs.TabIndex = 8
        '
        'colFileName
        '
        Me.colFileName.HeaderText = "FileName"
        Me.colFileName.Name = "colFileName"
        Me.colFileName.Width = 300
        '
        'colArtist
        '
        Me.colArtist.HeaderText = "Artist"
        Me.colArtist.Name = "colArtist"
        Me.colArtist.Width = 200
        '
        'colTitle
        '
        Me.colTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTitle.HeaderText = "Title"
        Me.colTitle.Name = "colTitle"
        '
        'colAlbum
        '
        Me.colAlbum.HeaderText = "Album"
        Me.colAlbum.Name = "colAlbum"
        Me.colAlbum.Width = 200
        '
        'cmdLoad
        '
        Me.cmdLoad.Location = New System.Drawing.Point(567, 26)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(75, 23)
        Me.cmdLoad.TabIndex = 9
        Me.cmdLoad.Text = "Load"
        Me.cmdLoad.UseVisualStyleBackColor = True
        '
        'labStatus
        '
        Me.labStatus.AutoSize = True
        Me.labStatus.Location = New System.Drawing.Point(564, 56)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Size = New System.Drawing.Size(37, 13)
        Me.labStatus.TabIndex = 10
        Me.labStatus.Text = "Status"
        Me.labStatus.Visible = False
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(689, 28)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(89, 20)
        Me.txtFilter.TabIndex = 12
        Me.txtFilter.Text = "*.mp3"
        '
        'labFilter
        '
        Me.labFilter.AutoSize = True
        Me.labFilter.Location = New System.Drawing.Point(651, 31)
        Me.labFilter.Name = "labFilter"
        Me.labFilter.Size = New System.Drawing.Size(32, 13)
        Me.labFilter.TabIndex = 11
        Me.labFilter.Text = "Filter:"
        '
        'cmdShowUnclear
        '
        Me.cmdShowUnclear.Location = New System.Drawing.Point(477, 26)
        Me.cmdShowUnclear.Name = "cmdShowUnclear"
        Me.cmdShowUnclear.Size = New System.Drawing.Size(84, 23)
        Me.cmdShowUnclear.TabIndex = 13
        Me.cmdShowUnclear.Text = "Show Unclear"
        Me.cmdShowUnclear.UseVisualStyleBackColor = True
        '
        'optV1
        '
        Me.optV1.AutoSize = True
        Me.optV1.Location = New System.Drawing.Point(567, 9)
        Me.optV1.Name = "optV1"
        Me.optV1.Size = New System.Drawing.Size(38, 17)
        Me.optV1.TabIndex = 14
        Me.optV1.Text = "V1"
        Me.optV1.UseVisualStyleBackColor = True
        '
        'optV2
        '
        Me.optV2.AutoSize = True
        Me.optV2.Checked = True
        Me.optV2.Location = New System.Drawing.Point(611, 9)
        Me.optV2.Name = "optV2"
        Me.optV2.Size = New System.Drawing.Size(38, 17)
        Me.optV2.TabIndex = 15
        Me.optV2.TabStop = True
        Me.optV2.Text = "V2"
        Me.optV2.UseVisualStyleBackColor = True
        '
        'txtFolder
        '
        Me.txtFolder.Location = New System.Drawing.Point(124, 6)
        Me.txtFolder.Name = "txtFolder"
        Me.txtFolder.Size = New System.Drawing.Size(116, 20)
        Me.txtFolder.TabIndex = 16
        '
        'frmMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1118, 392)
        Me.Controls.Add(Me.txtFolder)
        Me.Controls.Add(Me.optV2)
        Me.Controls.Add(Me.optV1)
        Me.Controls.Add(Me.cmdShowUnclear)
        Me.Controls.Add(Me.txtFilter)
        Me.Controls.Add(Me.labFilter)
        Me.Controls.Add(Me.labStatus)
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.dgvSongs)
        Me.Controls.Add(Me.txtDriveLetter)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Controls.Add(Me.txtPlaylistFolder)
        Me.Controls.Add(Me.labPlaylistFolder)
        Me.Controls.Add(Me.labDrive)
        Me.Controls.Add(Me.cmdCreateCSV)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Name = "frmMaster"
        Me.Text = "MP3 Management"
        CType(Me.dgvSongs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents cmdCreateCSV As System.Windows.Forms.Button
    Friend WithEvents labDrive As System.Windows.Forms.Label
    Friend WithEvents labPlaylistFolder As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txtPlaylistFolder As System.Windows.Forms.TextBox
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents txtDriveLetter As System.Windows.Forms.TextBox
    Friend WithEvents dgvSongs As System.Windows.Forms.DataGridView
    Friend WithEvents colFileName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colArtist As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAlbum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
    Friend WithEvents labStatus As System.Windows.Forms.Label
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents labFilter As System.Windows.Forms.Label
    Friend WithEvents cmdShowUnclear As System.Windows.Forms.Button
    Friend WithEvents optV1 As System.Windows.Forms.RadioButton
    Friend WithEvents optV2 As System.Windows.Forms.RadioButton
    Friend WithEvents txtFolder As System.Windows.Forms.TextBox

End Class
