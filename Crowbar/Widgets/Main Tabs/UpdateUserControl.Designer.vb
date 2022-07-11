<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UpdateUserControl
	Inherits BaseUserControl

	'UserControl overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.UpdateUserControlFillPanel = New Crowbar.PanelEx()
		Me.CheckForUpdateGroupBox = New Crowbar.GroupBoxEx()
		Me.Panel1 = New Crowbar.PanelEx()
		Me.ChangelogTextBox = New Crowbar.RichTextBoxEx()
		Me.CurrentVersionLabel = New System.Windows.Forms.Label()
		Me.CheckForUpdateTopPanel = New Crowbar.PanelEx()
		Me.CheckForUpdateButton = New Crowbar.ButtonEx()
		Me.CheckForUpdateTextBox = New Crowbar.RichTextBoxEx()
		Me.CheckForUpdateProgressBar = New Crowbar.ProgressBarEx()
		Me.CancelCheckButton = New Crowbar.ButtonEx()
		Me.DownloadGroupBox = New Crowbar.GroupBoxEx()
		Me.DownloadFolderTextBox = New Crowbar.RichTextBoxEx()
		Me.DownloadProgressBarEx = New Crowbar.ProgressBarEx()
		Me.BrowseForDownloadFolderButton = New Crowbar.ButtonEx()
		Me.DownloadFolderLabel = New System.Windows.Forms.Label()
		Me.GotoDownloadFileButton = New Crowbar.ButtonEx()
		Me.CancelDownloadButton = New Crowbar.ButtonEx()
		Me.DownloadButton = New Crowbar.ButtonEx()
		Me.UpdateGroupBox = New Crowbar.GroupBoxEx()
		Me.CancelUpdateButton = New Crowbar.ButtonEx()
		Me.BrowseForUpdateFolderButton = New Crowbar.ButtonEx()
		Me.UpdateFolderTextBox = New Crowbar.RichTextBoxEx()
		Me.UpdateProgressBarEx = New Crowbar.ProgressBarEx()
		Me.UpdateButton = New Crowbar.ButtonEx()
		Me.UpdateToNewPathCheckBox = New System.Windows.Forms.CheckBox()
		Me.UpdateCopySettingsCheckBox = New System.Windows.Forms.CheckBox()
		Me.UpdateUserControlFillPanel.SuspendLayout()
		Me.CheckForUpdateGroupBox.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.CheckForUpdateTopPanel.SuspendLayout()
		Me.DownloadGroupBox.SuspendLayout()
		Me.UpdateGroupBox.SuspendLayout()
		Me.SuspendLayout()
		'
		'UpdateUserControlFillPanel
		'
		Me.UpdateUserControlFillPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.UpdateUserControlFillPanel.Controls.Add(Me.CheckForUpdateGroupBox)
		Me.UpdateUserControlFillPanel.Controls.Add(Me.DownloadGroupBox)
		Me.UpdateUserControlFillPanel.Controls.Add(Me.UpdateGroupBox)
		Me.UpdateUserControlFillPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.UpdateUserControlFillPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.UpdateUserControlFillPanel.Location = New System.Drawing.Point(0, 0)
		Me.UpdateUserControlFillPanel.Name = "UpdateUserControlFillPanel"
		Me.UpdateUserControlFillPanel.SelectedIndex = -1
		Me.UpdateUserControlFillPanel.SelectedValue = Nothing
		Me.UpdateUserControlFillPanel.Size = New System.Drawing.Size(776, 536)
		Me.UpdateUserControlFillPanel.TabIndex = 17
		'
		'CheckForUpdateGroupBox
		'
		Me.CheckForUpdateGroupBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.CheckForUpdateGroupBox.Controls.Add(Me.Panel1)
		Me.CheckForUpdateGroupBox.Controls.Add(Me.CurrentVersionLabel)
		Me.CheckForUpdateGroupBox.Controls.Add(Me.CheckForUpdateTopPanel)
		Me.CheckForUpdateGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.CheckForUpdateGroupBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.CheckForUpdateGroupBox.IsReadOnly = False
		Me.CheckForUpdateGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.CheckForUpdateGroupBox.Name = "CheckForUpdateGroupBox"
		Me.CheckForUpdateGroupBox.SelectedValue = Nothing
		Me.CheckForUpdateGroupBox.Size = New System.Drawing.Size(776, 365)
		Me.CheckForUpdateGroupBox.TabIndex = 14
		Me.CheckForUpdateGroupBox.TabStop = False
		Me.CheckForUpdateGroupBox.Text = "Check for Update - Check for latest version and get changelog"
		'
		'Panel1
		'
		Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.Panel1.Controls.Add(Me.ChangelogTextBox)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.Panel1.Location = New System.Drawing.Point(3, 44)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
		Me.Panel1.SelectedIndex = -1
		Me.Panel1.SelectedValue = Nothing
		Me.Panel1.Size = New System.Drawing.Size(770, 318)
		Me.Panel1.TabIndex = 16
		'
		'ChangelogTextBox
		'
		Me.ChangelogTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.ChangelogTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.ChangelogTextBox.CueBannerText = ""
		Me.ChangelogTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ChangelogTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.ChangelogTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.ChangelogTextBox.Location = New System.Drawing.Point(3, 3)
		Me.ChangelogTextBox.Name = "ChangelogTextBox"
		Me.ChangelogTextBox.ReadOnly = True
		Me.ChangelogTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.ChangelogTextBox.Size = New System.Drawing.Size(764, 312)
		Me.ChangelogTextBox.TabIndex = 6
		Me.ChangelogTextBox.Text = ""
		Me.ChangelogTextBox.WordWrap = False
		'
		'CurrentVersionLabel
		'
		Me.CurrentVersionLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CurrentVersionLabel.AutoSize = True
		Me.CurrentVersionLabel.Location = New System.Drawing.Point(659, 0)
		Me.CurrentVersionLabel.Name = "CurrentVersionLabel"
		Me.CurrentVersionLabel.Size = New System.Drawing.Size(114, 13)
		Me.CurrentVersionLabel.TabIndex = 14
		Me.CurrentVersionLabel.Text = "Current Version: 0.00"
		'
		'CheckForUpdateTopPanel
		'
		Me.CheckForUpdateTopPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.CheckForUpdateTopPanel.Controls.Add(Me.CheckForUpdateButton)
		Me.CheckForUpdateTopPanel.Controls.Add(Me.CheckForUpdateTextBox)
		Me.CheckForUpdateTopPanel.Controls.Add(Me.CheckForUpdateProgressBar)
		Me.CheckForUpdateTopPanel.Controls.Add(Me.CancelCheckButton)
		Me.CheckForUpdateTopPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.CheckForUpdateTopPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.CheckForUpdateTopPanel.Location = New System.Drawing.Point(3, 18)
		Me.CheckForUpdateTopPanel.Name = "CheckForUpdateTopPanel"
		Me.CheckForUpdateTopPanel.SelectedIndex = -1
		Me.CheckForUpdateTopPanel.SelectedValue = Nothing
		Me.CheckForUpdateTopPanel.Size = New System.Drawing.Size(770, 26)
		Me.CheckForUpdateTopPanel.TabIndex = 15
		'
		'CheckForUpdateButton
		'
		Me.CheckForUpdateButton.Location = New System.Drawing.Point(3, 0)
		Me.CheckForUpdateButton.Name = "CheckForUpdateButton"
		Me.CheckForUpdateButton.Size = New System.Drawing.Size(69, 23)
		Me.CheckForUpdateButton.TabIndex = 1
		Me.CheckForUpdateButton.Text = "Check"
		Me.CheckForUpdateButton.UseVisualStyleBackColor = True
		'
		'CheckForUpdateTextBox
		'
		Me.CheckForUpdateTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CheckForUpdateTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.CheckForUpdateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.CheckForUpdateTextBox.CueBannerText = ""
		Me.CheckForUpdateTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CheckForUpdateTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.CheckForUpdateTextBox.Location = New System.Drawing.Point(78, 0)
		Me.CheckForUpdateTextBox.Multiline = False
		Me.CheckForUpdateTextBox.Name = "CheckForUpdateTextBox"
		Me.CheckForUpdateTextBox.ReadOnly = True
		Me.CheckForUpdateTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.CheckForUpdateTextBox.Size = New System.Drawing.Size(614, 22)
		Me.CheckForUpdateTextBox.TabIndex = 9
		Me.CheckForUpdateTextBox.Text = "[not checked yet]"
		'
		'CheckForUpdateProgressBar
		'
		Me.CheckForUpdateProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CheckForUpdateProgressBar.Location = New System.Drawing.Point(78, 0)
		Me.CheckForUpdateProgressBar.Name = "CheckForUpdateProgressBar"
		Me.CheckForUpdateProgressBar.Size = New System.Drawing.Size(614, 22)
		Me.CheckForUpdateProgressBar.TabIndex = 10
		Me.CheckForUpdateProgressBar.Visible = False
		'
		'CancelCheckButton
		'
		Me.CancelCheckButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CancelCheckButton.Enabled = False
		Me.CancelCheckButton.Location = New System.Drawing.Point(698, 0)
		Me.CancelCheckButton.Name = "CancelCheckButton"
		Me.CancelCheckButton.Size = New System.Drawing.Size(69, 23)
		Me.CancelCheckButton.TabIndex = 11
		Me.CancelCheckButton.Text = "Cancel"
		Me.CancelCheckButton.UseVisualStyleBackColor = True
		'
		'DownloadGroupBox
		'
		Me.DownloadGroupBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.DownloadGroupBox.Controls.Add(Me.DownloadFolderTextBox)
		Me.DownloadGroupBox.Controls.Add(Me.DownloadProgressBarEx)
		Me.DownloadGroupBox.Controls.Add(Me.BrowseForDownloadFolderButton)
		Me.DownloadGroupBox.Controls.Add(Me.DownloadFolderLabel)
		Me.DownloadGroupBox.Controls.Add(Me.GotoDownloadFileButton)
		Me.DownloadGroupBox.Controls.Add(Me.CancelDownloadButton)
		Me.DownloadGroupBox.Controls.Add(Me.DownloadButton)
		Me.DownloadGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.DownloadGroupBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.DownloadGroupBox.IsReadOnly = False
		Me.DownloadGroupBox.Location = New System.Drawing.Point(0, 365)
		Me.DownloadGroupBox.Name = "DownloadGroupBox"
		Me.DownloadGroupBox.SelectedValue = Nothing
		Me.DownloadGroupBox.Size = New System.Drawing.Size(776, 76)
		Me.DownloadGroupBox.TabIndex = 8
		Me.DownloadGroupBox.TabStop = False
		Me.DownloadGroupBox.Text = "Download - Download the new version (compressed file) for manually updating"
		'
		'DownloadFolderTextBox
		'
		Me.DownloadFolderTextBox.AllowDrop = True
		Me.DownloadFolderTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DownloadFolderTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.DownloadFolderTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.DownloadFolderTextBox.CueBannerText = ""
		Me.DownloadFolderTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.DownloadFolderTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.DownloadFolderTextBox.Location = New System.Drawing.Point(107, 15)
		Me.DownloadFolderTextBox.Multiline = False
		Me.DownloadFolderTextBox.Name = "DownloadFolderTextBox"
		Me.DownloadFolderTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.DownloadFolderTextBox.Size = New System.Drawing.Size(582, 22)
		Me.DownloadFolderTextBox.TabIndex = 7
		Me.DownloadFolderTextBox.Text = ""
		'
		'DownloadProgressBarEx
		'
		Me.DownloadProgressBarEx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DownloadProgressBarEx.Location = New System.Drawing.Point(87, 44)
		Me.DownloadProgressBarEx.Name = "DownloadProgressBarEx"
		Me.DownloadProgressBarEx.Size = New System.Drawing.Size(521, 23)
		Me.DownloadProgressBarEx.TabIndex = 6
		'
		'BrowseForDownloadFolderButton
		'
		Me.BrowseForDownloadFolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForDownloadFolderButton.Location = New System.Drawing.Point(695, 15)
		Me.BrowseForDownloadFolderButton.Name = "BrowseForDownloadFolderButton"
		Me.BrowseForDownloadFolderButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForDownloadFolderButton.TabIndex = 8
		Me.BrowseForDownloadFolderButton.Text = "Browse..."
		Me.BrowseForDownloadFolderButton.UseVisualStyleBackColor = True
		'
		'DownloadFolderLabel
		'
		Me.DownloadFolderLabel.AutoSize = True
		Me.DownloadFolderLabel.Location = New System.Drawing.Point(3, 20)
		Me.DownloadFolderLabel.Name = "DownloadFolderLabel"
		Me.DownloadFolderLabel.Size = New System.Drawing.Size(98, 13)
		Me.DownloadFolderLabel.TabIndex = 9
		Me.DownloadFolderLabel.Text = "Download folder:"
		'
		'GotoDownloadFileButton
		'
		Me.GotoDownloadFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoDownloadFileButton.Enabled = False
		Me.GotoDownloadFileButton.Location = New System.Drawing.Point(695, 44)
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
		Me.CancelDownloadButton.Location = New System.Drawing.Point(614, 44)
		Me.CancelDownloadButton.Name = "CancelDownloadButton"
		Me.CancelDownloadButton.Size = New System.Drawing.Size(75, 23)
		Me.CancelDownloadButton.TabIndex = 12
		Me.CancelDownloadButton.Text = "Cancel"
		Me.CancelDownloadButton.UseVisualStyleBackColor = True
		'
		'DownloadButton
		'
		Me.DownloadButton.Location = New System.Drawing.Point(6, 44)
		Me.DownloadButton.Name = "DownloadButton"
		Me.DownloadButton.Size = New System.Drawing.Size(75, 23)
		Me.DownloadButton.TabIndex = 2
		Me.DownloadButton.Text = "Download"
		Me.DownloadButton.UseVisualStyleBackColor = True
		'
		'UpdateGroupBox
		'
		Me.UpdateGroupBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.UpdateGroupBox.Controls.Add(Me.CancelUpdateButton)
		Me.UpdateGroupBox.Controls.Add(Me.BrowseForUpdateFolderButton)
		Me.UpdateGroupBox.Controls.Add(Me.UpdateFolderTextBox)
		Me.UpdateGroupBox.Controls.Add(Me.UpdateProgressBarEx)
		Me.UpdateGroupBox.Controls.Add(Me.UpdateButton)
		Me.UpdateGroupBox.Controls.Add(Me.UpdateToNewPathCheckBox)
		Me.UpdateGroupBox.Controls.Add(Me.UpdateCopySettingsCheckBox)
		Me.UpdateGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.UpdateGroupBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.UpdateGroupBox.IsReadOnly = False
		Me.UpdateGroupBox.Location = New System.Drawing.Point(0, 441)
		Me.UpdateGroupBox.Name = "UpdateGroupBox"
		Me.UpdateGroupBox.SelectedValue = Nothing
		Me.UpdateGroupBox.Size = New System.Drawing.Size(776, 95)
		Me.UpdateGroupBox.TabIndex = 7
		Me.UpdateGroupBox.TabStop = False
		Me.UpdateGroupBox.Text = "Update - Update current version to latest version - Crowbar will close and reopen" &
	""
		'
		'CancelUpdateButton
		'
		Me.CancelUpdateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CancelUpdateButton.Enabled = False
		Me.CancelUpdateButton.Location = New System.Drawing.Point(695, 66)
		Me.CancelUpdateButton.Name = "CancelUpdateButton"
		Me.CancelUpdateButton.Size = New System.Drawing.Size(75, 23)
		Me.CancelUpdateButton.TabIndex = 13
		Me.CancelUpdateButton.Text = "Cancel"
		Me.CancelUpdateButton.UseVisualStyleBackColor = True
		'
		'BrowseForUpdateFolderButton
		'
		Me.BrowseForUpdateFolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForUpdateFolderButton.Location = New System.Drawing.Point(695, 16)
		Me.BrowseForUpdateFolderButton.Name = "BrowseForUpdateFolderButton"
		Me.BrowseForUpdateFolderButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForUpdateFolderButton.TabIndex = 10
		Me.BrowseForUpdateFolderButton.Text = "Browse..."
		Me.BrowseForUpdateFolderButton.UseVisualStyleBackColor = True
		'
		'UpdateFolderTextBox
		'
		Me.UpdateFolderTextBox.AllowDrop = True
		Me.UpdateFolderTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UpdateFolderTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.UpdateFolderTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.UpdateFolderTextBox.CueBannerText = ""
		Me.UpdateFolderTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.UpdateFolderTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.UpdateFolderTextBox.Location = New System.Drawing.Point(266, 16)
		Me.UpdateFolderTextBox.Multiline = False
		Me.UpdateFolderTextBox.Name = "UpdateFolderTextBox"
		Me.UpdateFolderTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.UpdateFolderTextBox.Size = New System.Drawing.Size(423, 22)
		Me.UpdateFolderTextBox.TabIndex = 9
		Me.UpdateFolderTextBox.Text = ""
		'
		'UpdateProgressBarEx
		'
		Me.UpdateProgressBarEx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UpdateProgressBarEx.Location = New System.Drawing.Point(87, 66)
		Me.UpdateProgressBarEx.Name = "UpdateProgressBarEx"
		Me.UpdateProgressBarEx.Size = New System.Drawing.Size(602, 23)
		Me.UpdateProgressBarEx.TabIndex = 5
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
		'UpdateToNewPathCheckBox
		'
		Me.UpdateToNewPathCheckBox.AutoSize = True
		Me.UpdateToNewPathCheckBox.Location = New System.Drawing.Point(6, 20)
		Me.UpdateToNewPathCheckBox.Name = "UpdateToNewPathCheckBox"
		Me.UpdateToNewPathCheckBox.Size = New System.Drawing.Size(254, 17)
		Me.UpdateToNewPathCheckBox.TabIndex = 4
		Me.UpdateToNewPathCheckBox.Text = "Update to new folder (keep current version):"
		Me.UpdateToNewPathCheckBox.UseVisualStyleBackColor = True
		'
		'UpdateCopySettingsCheckBox
		'
		Me.UpdateCopySettingsCheckBox.AutoSize = True
		Me.UpdateCopySettingsCheckBox.Checked = True
		Me.UpdateCopySettingsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.UpdateCopySettingsCheckBox.Location = New System.Drawing.Point(6, 43)
		Me.UpdateCopySettingsCheckBox.Name = "UpdateCopySettingsCheckBox"
		Me.UpdateCopySettingsCheckBox.Size = New System.Drawing.Size(282, 17)
		Me.UpdateCopySettingsCheckBox.TabIndex = 3
		Me.UpdateCopySettingsCheckBox.Text = "Copy settings from current version to new version"
		Me.UpdateCopySettingsCheckBox.UseVisualStyleBackColor = True
		'
		'UpdateUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.UpdateUserControlFillPanel)
		Me.Name = "UpdateUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.UpdateUserControlFillPanel.ResumeLayout(False)
		Me.CheckForUpdateGroupBox.ResumeLayout(False)
		Me.CheckForUpdateGroupBox.PerformLayout()
		Me.Panel1.ResumeLayout(False)
		Me.CheckForUpdateTopPanel.ResumeLayout(False)
		Me.DownloadGroupBox.ResumeLayout(False)
		Me.DownloadGroupBox.PerformLayout()
		Me.UpdateGroupBox.ResumeLayout(False)
		Me.UpdateGroupBox.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents UpdateButton As ButtonEx
	Friend WithEvents CheckForUpdateButton As ButtonEx
	Friend WithEvents DownloadButton As ButtonEx
	Friend WithEvents UpdateCopySettingsCheckBox As CheckBox
	Friend WithEvents UpdateToNewPathCheckBox As CheckBox
	Friend WithEvents ChangelogTextBox As RichTextBoxEx
	Friend WithEvents UpdateGroupBox As GroupBoxEx
	Friend WithEvents UpdateProgressBarEx As ProgressBarEx
	Friend WithEvents DownloadGroupBox As GroupBoxEx
	Friend WithEvents DownloadProgressBarEx As ProgressBarEx
	Friend WithEvents DownloadFolderLabel As Label
	Friend WithEvents BrowseForDownloadFolderButton As ButtonEx
	Friend WithEvents DownloadFolderTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents BrowseForUpdateFolderButton As ButtonEx
	Friend WithEvents UpdateFolderTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CheckForUpdateTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CheckForUpdateProgressBar As ProgressBarEx
	Friend WithEvents CancelUpdateButton As ButtonEx
	Friend WithEvents CancelDownloadButton As ButtonEx
	Friend WithEvents CancelCheckButton As ButtonEx
	Friend WithEvents CheckForUpdateGroupBox As GroupBoxEx
	Friend WithEvents CurrentVersionLabel As Label
	Friend WithEvents GotoDownloadFileButton As ButtonEx
	Friend WithEvents UpdateUserControlFillPanel As PanelEx
	Friend WithEvents CheckForUpdateTopPanel As PanelEx
	Friend WithEvents Panel1 As PanelEx
End Class
