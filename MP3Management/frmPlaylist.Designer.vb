<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlaylist
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
        Me.labPlaylist = New System.Windows.Forms.Label()
        Me.cmbPlaylist = New System.Windows.Forms.ComboBox()
        Me.dgvSongs = New System.Windows.Forms.DataGridView()
        Me.colFileName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colArtist = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAlbum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.chkMerge = New System.Windows.Forms.CheckBox()
        Me.labFileNotFould = New System.Windows.Forms.Label()
        Me.labDuplicatedEntry = New System.Windows.Forms.Label()
        Me.labPreviouslySaved = New System.Windows.Forms.Label()
        Me.labNewEntry = New System.Windows.Forms.Label()
        Me.cmdValidatePlaylists = New System.Windows.Forms.Button()
        CType(Me.dgvSongs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labPlaylist
        '
        Me.labPlaylist.AutoSize = True
        Me.labPlaylist.Location = New System.Drawing.Point(12, 15)
        Me.labPlaylist.Name = "labPlaylist"
        Me.labPlaylist.Size = New System.Drawing.Size(39, 13)
        Me.labPlaylist.TabIndex = 0
        Me.labPlaylist.Text = "Playlist"
        '
        'cmbPlaylist
        '
        Me.cmbPlaylist.FormattingEnabled = True
        Me.cmbPlaylist.Location = New System.Drawing.Point(98, 12)
        Me.cmbPlaylist.Name = "cmbPlaylist"
        Me.cmbPlaylist.Size = New System.Drawing.Size(235, 21)
        Me.cmbPlaylist.Sorted = True
        Me.cmbPlaylist.TabIndex = 1
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
        Me.dgvSongs.Size = New System.Drawing.Size(1094, 303)
        Me.dgvSongs.TabIndex = 2
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
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.Location = New System.Drawing.Point(1031, 10)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 23)
        Me.cmdOk.TabIndex = 3
        Me.cmdOk.Text = "OK"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(940, 10)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 4
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'chkMerge
        '
        Me.chkMerge.AutoSize = True
        Me.chkMerge.Location = New System.Drawing.Point(339, 14)
        Me.chkMerge.Name = "chkMerge"
        Me.chkMerge.Size = New System.Drawing.Size(149, 17)
        Me.chkMerge.TabIndex = 5
        Me.chkMerge.Text = "merge with existing playlist"
        Me.chkMerge.UseVisualStyleBackColor = True
        '
        'labFileNotFould
        '
        Me.labFileNotFould.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.labFileNotFould.AutoSize = True
        Me.labFileNotFould.ForeColor = System.Drawing.Color.LightGray
        Me.labFileNotFould.Location = New System.Drawing.Point(356, 361)
        Me.labFileNotFould.Name = "labFileNotFould"
        Me.labFileNotFould.Size = New System.Drawing.Size(92, 13)
        Me.labFileNotFould.TabIndex = 6
        Me.labFileNotFould.Text = "MP3-File not fould"
        '
        'labDuplicatedEntry
        '
        Me.labDuplicatedEntry.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.labDuplicatedEntry.AutoSize = True
        Me.labDuplicatedEntry.ForeColor = System.Drawing.Color.Red
        Me.labDuplicatedEntry.Location = New System.Drawing.Point(268, 361)
        Me.labDuplicatedEntry.Name = "labDuplicatedEntry"
        Me.labDuplicatedEntry.Size = New System.Drawing.Size(82, 13)
        Me.labDuplicatedEntry.TabIndex = 7
        Me.labDuplicatedEntry.Text = "duplicated entry"
        '
        'labPreviouslySaved
        '
        Me.labPreviouslySaved.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.labPreviouslySaved.AutoSize = True
        Me.labPreviouslySaved.ForeColor = System.Drawing.Color.Blue
        Me.labPreviouslySaved.Location = New System.Drawing.Point(111, 361)
        Me.labPreviouslySaved.Name = "labPreviouslySaved"
        Me.labPreviouslySaved.Size = New System.Drawing.Size(151, 13)
        Me.labPreviouslySaved.TabIndex = 8
        Me.labPreviouslySaved.Text = "previously saved to this playlist"
        '
        'labNewEntry
        '
        Me.labNewEntry.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.labNewEntry.AutoSize = True
        Me.labNewEntry.ForeColor = System.Drawing.Color.Green
        Me.labNewEntry.Location = New System.Drawing.Point(12, 361)
        Me.labNewEntry.Name = "labNewEntry"
        Me.labNewEntry.Size = New System.Drawing.Size(93, 13)
        Me.labNewEntry.TabIndex = 9
        Me.labNewEntry.Text = "New song to save"
        '
        'cmdValidatePlaylists
        '
        Me.cmdValidatePlaylists.Location = New System.Drawing.Point(640, 10)
        Me.cmdValidatePlaylists.Name = "cmdValidatePlaylists"
        Me.cmdValidatePlaylists.Size = New System.Drawing.Size(115, 23)
        Me.cmdValidatePlaylists.TabIndex = 14
        Me.cmdValidatePlaylists.Text = "Validate Playlists"
        Me.cmdValidatePlaylists.UseVisualStyleBackColor = True
        '
        'frmPlaylist
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(1118, 392)
        Me.Controls.Add(Me.cmdValidatePlaylists)
        Me.Controls.Add(Me.labNewEntry)
        Me.Controls.Add(Me.labPreviouslySaved)
        Me.Controls.Add(Me.labDuplicatedEntry)
        Me.Controls.Add(Me.labFileNotFould)
        Me.Controls.Add(Me.chkMerge)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.dgvSongs)
        Me.Controls.Add(Me.cmbPlaylist)
        Me.Controls.Add(Me.labPlaylist)
        Me.Name = "frmPlaylist"
        Me.Text = "Playlist"
        CType(Me.dgvSongs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents labPlaylist As System.Windows.Forms.Label
    Friend WithEvents cmbPlaylist As System.Windows.Forms.ComboBox
    Friend WithEvents dgvSongs As System.Windows.Forms.DataGridView
    Friend WithEvents colFileName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colArtist As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAlbum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents chkMerge As System.Windows.Forms.CheckBox
    Friend WithEvents labFileNotFould As System.Windows.Forms.Label
    Friend WithEvents labDuplicatedEntry As System.Windows.Forms.Label
    Friend WithEvents labPreviouslySaved As System.Windows.Forms.Label
    Friend WithEvents labNewEntry As System.Windows.Forms.Label
    Friend WithEvents cmdValidatePlaylists As System.Windows.Forms.Button
End Class
