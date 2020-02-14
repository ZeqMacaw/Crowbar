<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateUserControl
	Inherits BaseUserControl

	'UserControl overrides dispose to clean up the component list.
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
		Me.components = New System.ComponentModel.Container()
		Me.UpdateButton = New System.Windows.Forms.Button()
		Me.CheckForUpdateButton = New System.Windows.Forms.Button()
		Me.DownloadButton = New System.Windows.Forms.Button()
		Me.UpdateCopySettingsCheckBox = New System.Windows.Forms.CheckBox()
		Me.UpdateToNewPathCheckBox = New System.Windows.Forms.CheckBox()
		Me.ChangelogTextBox = New Crowbar.RichTextBoxEx()
		Me.GroupBoxEx1 = New Crowbar.GroupBoxEx()
		Me.CancelUpdateButton = New System.Windows.Forms.Button()
		Me.BrowseForUpdateFolderButton = New System.Windows.Forms.Button()
		Me.UpdateFolderTextBox = New Crowbar.TextBoxEx()
		Me.UpdateProgressBarEx = New Crowbar.ProgressBarEx()
		Me.GroupBoxEx2 = New Crowbar.GroupBoxEx()
		Me.GotoDownloadFileButton = New System.Windows.Forms.Button()
		Me.CancelDownloadButton = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.BrowseForDownloadFolderButton = New System.Windows.Forms.Button()
		Me.DownloadFolderTextBox = New Crowbar.TextBoxEx()
		Me.DownloadProgressBarEx = New Crowbar.ProgressBarEx()
		Me.CheckForUpdateTextBox = New Crowbar.TextBoxEx()
		Me.CheckForUpdateProgressBar = New Crowbar.ProgressBarEx()
		Me.CancelCheckButton = New System.Windows.Forms.Button()
		Me.GroupBoxEx3 = New Crowbar.GroupBoxEx()
		Me.CurrentVersionLabel = New System.Windows.Forms.Label()
		Me.GroupBoxEx1.SuspendLayout()
		Me.GroupBoxEx2.SuspendLayout()
		Me.GroupBoxEx3.SuspendLayout()
		Me.SuspendLayout()
		'
		'UpdateButton
		'
		Me.UpdateButton.Location = New System.Drawing.Point(6, 66)
		Me.UpdateButton.Name = "UpdateButton"
		Me.UpdateButton.Size = New System.Drawing.Size(75, 23)
		Me.UpdateButton.TabIndex = 0
		Me.UpdateButton.Text = "Update"
		Me.UpdateButton.UseVisualStyleBackColor = True
		'
		'CheckForUpdateButton
		'
		Me.CheckForUpdateButton.Location = New System.Drawing.Point(6, 20)
		Me.CheckForUpdateButton.Name = "CheckForUpdateButton"
		Me.CheckForUpdateButton.Size = New System.Drawing.Size(75, 23)
		Me.CheckForUpdateButton.TabIndex = 1
		Me.CheckForUpdateButton.Text = "Check"
		Me.CheckForUpdateButton.UseVisualStyleBackColor = True
		'
		'DownloadButton
		'
		Me.DownloadButton.Location = New System.Drawing.Point(6, 47)
		Me.DownloadButton.Name = "DownloadButton"
		Me.DownloadButton.Size = New System.Drawing.Size(75, 23)
		Me.DownloadButton.TabIndex = 2
		Me.DownloadButton.Text = "Download"
		Me.DownloadButton.UseVisualStyleBackColor = True
		'
		'UpdateCopySettingsCheckBox
		'
		Me.UpdateCopySettingsCheckBox.AutoSize = True
		Me.UpdateCopySettingsCheckBox.Checked = True
		Me.UpdateCopySettingsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.UpdateCopySettingsCheckBox.Location = New System.Drawing.Point(6, 43)
		Me.UpdateCopySettingsCheckBox.Name = "UpdateCopySettingsCheckBox"
		Me.UpdateCopySettingsCheckBox.Size = New System.Drawing.Size(267, 17)
		Me.UpdateCopySettingsCheckBox.TabIndex = 3
		Me.UpdateCopySettingsCheckBox.Text = "Copy settings from current version to new version"
		Me.UpdateCopySettingsCheckBox.UseVisualStyleBackColor = True
		'
		'UpdateToNewPathCheckBox
		'
		Me.UpdateToNewPathCheckBox.AutoSize = True
		Me.UpdateToNewPathCheckBox.Location = New System.Drawing.Point(6, 20)
		Me.UpdateToNewPathCheckBox.Name = "UpdateToNewPathCheckBox"
		Me.UpdateToNewPathCheckBox.Size = New System.Drawing.Size(242, 17)
		Me.UpdateToNewPathCheckBox.TabIndex = 4
		Me.UpdateToNewPathCheckBox.Text = "Update to new folder (keep current version):"
		Me.UpdateToNewPathCheckBox.UseVisualStyleBackColor = True
		'
		'ChangelogTextBox
		'
		Me.ChangelogTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ChangelogTextBox.CueBannerText = ""
		Me.ChangelogTextBox.Location = New System.Drawing.Point(6, 49)
		Me.ChangelogTextBox.Name = "ChangelogTextBox"
		Me.ChangelogTextBox.Size = New System.Drawing.Size(758, 292)
		Me.ChangelogTextBox.TabIndex = 6
		Me.ChangelogTextBox.Text = ""
		'
		'GroupBoxEx1
		'
		Me.GroupBoxEx1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBoxEx1.Controls.Add(Me.CancelUpdateButton)
		Me.GroupBoxEx1.Controls.Add(Me.BrowseForUpdateFolderButton)
		Me.GroupBoxEx1.Controls.Add(Me.UpdateFolderTextBox)
		Me.GroupBoxEx1.Controls.Add(Me.UpdateProgressBarEx)
		Me.GroupBoxEx1.Controls.Add(Me.UpdateButton)
		Me.GroupBoxEx1.Controls.Add(Me.UpdateToNewPathCheckBox)
		Me.GroupBoxEx1.Controls.Add(Me.UpdateCopySettingsCheckBox)
		Me.GroupBoxEx1.IsReadOnly = False
		Me.GroupBoxEx1.Location = New System.Drawing.Point(3, 438)
		Me.GroupBoxEx1.Name = "GroupBoxEx1"
		Me.GroupBoxEx1.SelectedValue = Nothing
		Me.GroupBoxEx1.Size = New System.Drawing.Size(770, 95)
		Me.GroupBoxEx1.TabIndex = 7
		Me.GroupBoxEx1.TabStop = False
		Me.GroupBoxEx1.Text = "Update - Update current version to latest version - Crowbar will close and reopen" &
	""
		'
		'CancelUpdateButton
		'
		Me.CancelUpdateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CancelUpdateButton.Enabled = False
		Me.CancelUpdateButton.Location = New System.Drawing.Point(689, 66)
		Me.CancelUpdateButton.Name = "CancelUpdateButton"
		Me.CancelUpdateButton.Size = New System.Drawing.Size(75, 23)
		Me.CancelUpdateButton.TabIndex = 13
		Me.CancelUpdateButton.Text = "Cancel"
		Me.CancelUpdateButton.UseVisualStyleBackColor = True
		'
		'BrowseForUpdateFolderButton
		'
		Me.BrowseForUpdateFolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForUpdateFolderButton.Location = New System.Drawing.Point(689, 16)
		Me.BrowseForUpdateFolderButton.Name = "BrowseForUpdateFolderButton"
		Me.BrowseForUpdateFolderButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForUpdateFolderButton.TabIndex = 10
		Me.BrowseForUpdateFolderButton.Text = "Browse..."
		Me.BrowseForUpdateFolderButton.UseVisualStyleBackColor = True
		'
		'UpdateFolderTextBox
		'
		Me.UpdateFolderTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UpdateFolderTextBox.CueBannerText = ""
		Me.UpdateFolderTextBox.Location = New System.Drawing.Point(254, 18)
		Me.UpdateFolderTextBox.Name = "UpdateFolderTextBox"
		Me.UpdateFolderTextBox.Size = New System.Drawing.Size(429, 21)
		Me.UpdateFolderTextBox.TabIndex = 9
		'
		'UpdateProgressBarEx
		'
		Me.UpdateProgressBarEx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UpdateProgressBarEx.Location = New System.Drawing.Point(87, 66)
		Me.UpdateProgressBarEx.Name = "UpdateProgressBarEx"
		Me.UpdateProgressBarEx.Size = New System.Drawing.Size(596, 23)
		Me.UpdateProgressBarEx.TabIndex = 5
		'
		'GroupBoxEx2
		'
		Me.GroupBoxEx2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBoxEx2.Controls.Add(Me.GotoDownloadFileButton)
		Me.GroupBoxEx2.Controls.Add(Me.CancelDownloadButton)
		Me.GroupBoxEx2.Controls.Add(Me.Label2)
		Me.GroupBoxEx2.Controls.Add(Me.BrowseForDownloadFolderButton)
		Me.GroupBoxEx2.Controls.Add(Me.DownloadFolderTextBox)
		Me.GroupBoxEx2.Controls.Add(Me.DownloadProgressBarEx)
		Me.GroupBoxEx2.Controls.Add(Me.DownloadButton)
		Me.GroupBoxEx2.IsReadOnly = False
		Me.GroupBoxEx2.Location = New System.Drawing.Point(3, 356)
		Me.GroupBoxEx2.Name = "GroupBoxEx2"
		Me.GroupBoxEx2.SelectedValue = Nothing
		Me.GroupBoxEx2.Size = New System.Drawing.Size(770, 76)
		Me.GroupBoxEx2.TabIndex = 8
		Me.GroupBoxEx2.TabStop = False
		Me.GroupBoxEx2.Text = "Download - Download the new version (compressed file) for manually updating"
		'
		'GotoDownloadFileButton
		'
		Me.GotoDownloadFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoDownloadFileButton.Enabled = False
		Me.GotoDownloadFileButton.Location = New System.Drawing.Point(689, 47)
		Me.GotoDownloadFileButton.Name = "GotoDownloadFileButton"
		Me.GotoDownloadFileButton.Size = New System.Drawing.Size(75, 23)
		Me.GotoDownloadFileButton.TabIndex = 13
		Me.GotoDownloadFileButton.Text = "Goto"
		Me.GotoDownloadFileButton.UseVisualStyleBackColor = True
		'
		'CancelDownloadButton
		'
		Me.CancelDownloadButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CancelDownloadButton.Enabled = False
		Me.CancelDownloadButton.Location = New System.Drawing.Point(608, 47)
		Me.CancelDownloadButton.Name = "CancelDownloadButton"
		Me.CancelDownloadButton.Size = New System.Drawing.Size(75, 23)
		Me.CancelDownloadButton.TabIndex = 12
		Me.CancelDownloadButton.Text = "Cancel"
		Me.CancelDownloadButton.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(6, 23)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(89, 13)
		Me.Label2.TabIndex = 9
		Me.Label2.Text = "Download folder:"
		'
		'BrowseForDownloadFolderButton
		'
		Me.BrowseForDownloadFolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForDownloadFolderButton.Location = New System.Drawing.Point(689, 18)
		Me.BrowseForDownloadFolderButton.Name = "BrowseForDownloadFolderButton"
		Me.BrowseForDownloadFolderButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForDownloadFolderButton.TabIndex = 8
		Me.BrowseForDownloadFolderButton.Text = "Browse..."
		Me.BrowseForDownloadFolderButton.UseVisualStyleBackColor = True
		'
		'DownloadFolderTextBox
		'
		Me.DownloadFolderTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DownloadFolderTextBox.CueBannerText = ""
		Me.DownloadFolderTextBox.Location = New System.Drawing.Point(101, 20)
		Me.DownloadFolderTextBox.Name = "DownloadFolderTextBox"
		Me.DownloadFolderTextBox.Size = New System.Drawing.Size(582, 21)
		Me.DownloadFolderTextBox.TabIndex = 7
		'
		'DownloadProgressBarEx
		'
		Me.DownloadProgressBarEx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DownloadProgressBarEx.Location = New System.Drawing.Point(87, 47)
		Me.DownloadProgressBarEx.Name = "DownloadProgressBarEx"
		Me.DownloadProgressBarEx.Size = New System.Drawing.Size(515, 23)
		Me.DownloadProgressBarEx.TabIndex = 6
		'
		'CheckForUpdateTextBox
		'
		Me.CheckForUpdateTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CheckForUpdateTextBox.CueBannerText = ""
		Me.CheckForUpdateTextBox.Location = New System.Drawing.Point(87, 20)
		Me.CheckForUpdateTextBox.Name = "CheckForUpdateTextBox"
		Me.CheckForUpdateTextBox.ReadOnly = True
		Me.CheckForUpdateTextBox.Size = New System.Drawing.Size(596, 21)
		Me.CheckForUpdateTextBox.TabIndex = 9
		Me.CheckForUpdateTextBox.Text = "[not checked yet]"
		'
		'CheckForUpdateProgressBar
		'
		Me.CheckForUpdateProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CheckForUpdateProgressBar.Location = New System.Drawing.Point(87, 20)
		Me.CheckForUpdateProgressBar.Name = "CheckForUpdateProgressBar"
		Me.CheckForUpdateProgressBar.Size = New System.Drawing.Size(596, 23)
		Me.CheckForUpdateProgressBar.TabIndex = 10
		Me.CheckForUpdateProgressBar.Visible = False
		'
		'CancelCheckButton
		'
		Me.CancelCheckButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CancelCheckButton.Enabled = False
		Me.CancelCheckButton.Location = New System.Drawing.Point(689, 20)
		Me.CancelCheckButton.Name = "CancelCheckButton"
		Me.CancelCheckButton.Size = New System.Drawing.Size(75, 23)
		Me.CancelCheckButton.TabIndex = 11
		Me.CancelCheckButton.Text = "Cancel"
		Me.CancelCheckButton.UseVisualStyleBackColor = True
		'
		'GroupBoxEx3
		'
		Me.GroupBoxEx3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBoxEx3.Controls.Add(Me.CurrentVersionLabel)
		Me.GroupBoxEx3.Controls.Add(Me.CheckForUpdateButton)
		Me.GroupBoxEx3.Controls.Add(Me.CheckForUpdateTextBox)
		Me.GroupBoxEx3.Controls.Add(Me.CheckForUpdateProgressBar)
		Me.GroupBoxEx3.Controls.Add(Me.ChangelogTextBox)
		Me.GroupBoxEx3.Controls.Add(Me.CancelCheckButton)
		Me.GroupBoxEx3.IsReadOnly = False
		Me.GroupBoxEx3.Location = New System.Drawing.Point(3, 3)
		Me.GroupBoxEx3.Name = "GroupBoxEx3"
		Me.GroupBoxEx3.SelectedValue = Nothing
		Me.GroupBoxEx3.Size = New System.Drawing.Size(770, 347)
		Me.GroupBoxEx3.TabIndex = 14
		Me.GroupBoxEx3.TabStop = False
		Me.GroupBoxEx3.Text = "Check for Update - Check for latest version and get changelog"
		'
		'CurrentVersionLabel
		'
		Me.CurrentVersionLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CurrentVersionLabel.AutoSize = True
		Me.CurrentVersionLabel.Location = New System.Drawing.Point(653, 0)
		Me.CurrentVersionLabel.Name = "CurrentVersionLabel"
		Me.CurrentVersionLabel.Size = New System.Drawing.Size(111, 13)
		Me.CurrentVersionLabel.TabIndex = 14
		Me.CurrentVersionLabel.Text = "Current Version: 0.00"
		'
		'UpdateUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.GroupBoxEx3)
		Me.Controls.Add(Me.GroupBoxEx2)
		Me.Controls.Add(Me.GroupBoxEx1)
		Me.Name = "UpdateUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.GroupBoxEx1.ResumeLayout(False)
		Me.GroupBoxEx1.PerformLayout()
		Me.GroupBoxEx2.ResumeLayout(False)
		Me.GroupBoxEx2.PerformLayout()
		Me.GroupBoxEx3.ResumeLayout(False)
		Me.GroupBoxEx3.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents UpdateButton As Button
	Friend WithEvents CheckForUpdateButton As Button
	Friend WithEvents DownloadButton As Button
	Friend WithEvents UpdateCopySettingsCheckBox As CheckBox
	Friend WithEvents UpdateToNewPathCheckBox As CheckBox
	Friend WithEvents ChangelogTextBox As RichTextBoxEx
	Friend WithEvents GroupBoxEx1 As GroupBoxEx
	Friend WithEvents UpdateProgressBarEx As ProgressBarEx
	Friend WithEvents GroupBoxEx2 As GroupBoxEx
	Friend WithEvents DownloadProgressBarEx As ProgressBarEx
	Friend WithEvents Label2 As Label
	Friend WithEvents BrowseForDownloadFolderButton As Button
	Friend WithEvents DownloadFolderTextBox As TextBoxEx
	Friend WithEvents BrowseForUpdateFolderButton As Button
	Friend WithEvents UpdateFolderTextBox As TextBoxEx
	Friend WithEvents CheckForUpdateTextBox As TextBoxEx
	Friend WithEvents CheckForUpdateProgressBar As ProgressBarEx
	Friend WithEvents CancelUpdateButton As Button
	Friend WithEvents CancelDownloadButton As Button
	Friend WithEvents CancelCheckButton As Button
	Friend WithEvents GroupBoxEx3 As GroupBoxEx
	Friend WithEvents CurrentVersionLabel As Label
	Friend WithEvents GotoDownloadFileButton As Button
End Class
