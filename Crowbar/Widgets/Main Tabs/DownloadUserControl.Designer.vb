<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DownloadUserControl
    Inherits BaseUserControl

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.ItemIdTextBox = New Crowbar.TextBoxEx()
		Me.DownloadButton = New System.Windows.Forms.Button()
		Me.LogTextBox = New Crowbar.RichTextBoxEx()
		Me.ItemIdOrLinkLabel = New System.Windows.Forms.Label()
		Me.OuputToLabel = New System.Windows.Forms.Label()
		Me.OutputPathComboBox = New System.Windows.Forms.ComboBox()
		Me.OutputPathTextBox = New Crowbar.TextBoxEx()
		Me.GotoOutputPathButton = New System.Windows.Forms.Button()
		Me.BrowseForOutputPathButton = New System.Windows.Forms.Button()
		Me.OptionsGroupBox = New Crowbar.GroupBoxEx()
		Me.OptionsGroupBoxFillPanel = New System.Windows.Forms.Panel()
		Me.UseIdCheckBox = New Crowbar.CheckBoxEx()
		Me.PrependTitleCheckBox = New Crowbar.CheckBoxEx()
		Me.AppendDateTimeCheckBox = New Crowbar.CheckBoxEx()
		Me.ReplaceSpacesWithUnderscoresCheckBox = New Crowbar.CheckBoxEx()
		Me.OptionsUseDefaultsButton = New System.Windows.Forms.Button()
		Me.ConvertToExpectedFileOrFolderCheckBox = New Crowbar.CheckBoxEx()
		Me.ExampleOutputFileNameLabel = New System.Windows.Forms.Label()
		Me.ExampleOutputFileNameTextBox = New Crowbar.TextBoxEx()
		Me.CancelDownloadButton = New System.Windows.Forms.Button()
		Me.DownloadProgressBar = New Crowbar.ProgressBarEx()
		Me.OpenWorkshopPageButton = New System.Windows.Forms.Button()
		Me.DocumentsOutputPathTextBox = New Crowbar.TextBoxEx()
		Me.DownloadedItemTextBox = New Crowbar.TextBoxEx()
		Me.DownloadedLabel = New System.Windows.Forms.Label()
		Me.GotoDownloadedItemButton = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.DownloadUserControlFillPanel = New System.Windows.Forms.Panel()
		Me.Options_LogSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.DownloadButtonsPanel = New System.Windows.Forms.Panel()
		Me.PostDownloadPanel = New System.Windows.Forms.Panel()
		Me.UseInUnpackButton = New System.Windows.Forms.Button()
		Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.OptionsGroupBox.SuspendLayout()
		Me.OptionsGroupBoxFillPanel.SuspendLayout()
		Me.DownloadUserControlFillPanel.SuspendLayout()
		CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Options_LogSplitContainer.Panel1.SuspendLayout()
		Me.Options_LogSplitContainer.Panel2.SuspendLayout()
		Me.Options_LogSplitContainer.SuspendLayout()
		Me.DownloadButtonsPanel.SuspendLayout()
		Me.PostDownloadPanel.SuspendLayout()
		Me.SuspendLayout()
		'
		'ItemIdTextBox
		'
		Me.ItemIdTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemIdTextBox.CueBannerText = ""
		Me.ItemIdTextBox.Location = New System.Drawing.Point(87, 3)
		Me.ItemIdTextBox.Name = "ItemIdTextBox"
		Me.ItemIdTextBox.Size = New System.Drawing.Size(616, 22)
		Me.ItemIdTextBox.TabIndex = 1
		'
		'DownloadButton
		'
		Me.DownloadButton.Location = New System.Drawing.Point(0, 0)
		Me.DownloadButton.Name = "DownloadButton"
		Me.DownloadButton.Size = New System.Drawing.Size(120, 23)
		Me.DownloadButton.TabIndex = 10
		Me.DownloadButton.Text = "Download"
		Me.DownloadButton.UseVisualStyleBackColor = True
		'
		'LogTextBox
		'
		Me.LogTextBox.CueBannerText = ""
		Me.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LogTextBox.HideSelection = False
		Me.LogTextBox.Location = New System.Drawing.Point(0, 26)
		Me.LogTextBox.Name = "LogTextBox"
		Me.LogTextBox.ReadOnly = True
		Me.LogTextBox.Size = New System.Drawing.Size(770, 226)
		Me.LogTextBox.TabIndex = 13
		Me.LogTextBox.Text = ""
		'
		'ItemIdOrLinkLabel
		'
		Me.ItemIdOrLinkLabel.AutoSize = True
		Me.ItemIdOrLinkLabel.Location = New System.Drawing.Point(3, 8)
		Me.ItemIdOrLinkLabel.Name = "ItemIdOrLinkLabel"
		Me.ItemIdOrLinkLabel.Size = New System.Drawing.Size(82, 13)
		Me.ItemIdOrLinkLabel.TabIndex = 0
		Me.ItemIdOrLinkLabel.Text = "Item ID or link:"
		'
		'OuputToLabel
		'
		Me.OuputToLabel.AutoSize = True
		Me.OuputToLabel.Location = New System.Drawing.Point(3, 37)
		Me.OuputToLabel.Name = "OuputToLabel"
		Me.OuputToLabel.Size = New System.Drawing.Size(62, 13)
		Me.OuputToLabel.TabIndex = 3
		Me.OuputToLabel.Text = "Output to:"
		'
		'OutputPathComboBox
		'
		Me.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.OutputPathComboBox.FormattingEnabled = True
		Me.OutputPathComboBox.Location = New System.Drawing.Point(87, 33)
		Me.OutputPathComboBox.Name = "OutputPathComboBox"
		Me.OutputPathComboBox.Size = New System.Drawing.Size(140, 21)
		Me.OutputPathComboBox.TabIndex = 4
		'
		'OutputPathTextBox
		'
		Me.OutputPathTextBox.AllowDrop = True
		Me.OutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputPathTextBox.CueBannerText = ""
		Me.OutputPathTextBox.Location = New System.Drawing.Point(233, 32)
		Me.OutputPathTextBox.Name = "OutputPathTextBox"
		Me.OutputPathTextBox.Size = New System.Drawing.Size(421, 22)
		Me.OutputPathTextBox.TabIndex = 5
		'
		'GotoOutputPathButton
		'
		Me.GotoOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoOutputPathButton.Location = New System.Drawing.Point(730, 32)
		Me.GotoOutputPathButton.Name = "GotoOutputPathButton"
		Me.GotoOutputPathButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoOutputPathButton.TabIndex = 8
		Me.GotoOutputPathButton.Text = "Goto"
		Me.GotoOutputPathButton.UseVisualStyleBackColor = True
		'
		'BrowseForOutputPathButton
		'
		Me.BrowseForOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForOutputPathButton.Location = New System.Drawing.Point(660, 32)
		Me.BrowseForOutputPathButton.Name = "BrowseForOutputPathButton"
		Me.BrowseForOutputPathButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForOutputPathButton.TabIndex = 7
		Me.BrowseForOutputPathButton.Text = "Browse..."
		Me.BrowseForOutputPathButton.UseVisualStyleBackColor = True
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Controls.Add(Me.OptionsGroupBoxFillPanel)
		Me.OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsGroupBox.IsReadOnly = False
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.SelectedValue = Nothing
		Me.OptionsGroupBox.Size = New System.Drawing.Size(770, 193)
		Me.OptionsGroupBox.TabIndex = 9
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Output File Name Options"
		'
		'OptionsGroupBoxFillPanel
		'
		Me.OptionsGroupBoxFillPanel.AutoScroll = True
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.UseIdCheckBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.PrependTitleCheckBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.AppendDateTimeCheckBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.ReplaceSpacesWithUnderscoresCheckBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.OptionsUseDefaultsButton)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.ConvertToExpectedFileOrFolderCheckBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.ExampleOutputFileNameLabel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.ExampleOutputFileNameTextBox)
		Me.OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsGroupBoxFillPanel.Location = New System.Drawing.Point(3, 18)
		Me.OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel"
		Me.OptionsGroupBoxFillPanel.Size = New System.Drawing.Size(764, 172)
		Me.OptionsGroupBoxFillPanel.TabIndex = 8
		'
		'UseIdCheckBox
		'
		Me.UseIdCheckBox.AutoSize = True
		Me.UseIdCheckBox.IsReadOnly = False
		Me.UseIdCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.UseIdCheckBox.Name = "UseIdCheckBox"
		Me.UseIdCheckBox.Size = New System.Drawing.Size(201, 17)
		Me.UseIdCheckBox.TabIndex = 0
		Me.UseIdCheckBox.Text = "Use item ID instead of given name"
		Me.UseIdCheckBox.UseVisualStyleBackColor = True
		'
		'PrependTitleCheckBox
		'
		Me.PrependTitleCheckBox.AutoSize = True
		Me.PrependTitleCheckBox.IsReadOnly = False
		Me.PrependTitleCheckBox.Location = New System.Drawing.Point(3, 26)
		Me.PrependTitleCheckBox.Name = "PrependTitleCheckBox"
		Me.PrependTitleCheckBox.Size = New System.Drawing.Size(117, 17)
		Me.PrependTitleCheckBox.TabIndex = 1
		Me.PrependTitleCheckBox.Text = "Prepend item title"
		Me.PrependTitleCheckBox.UseVisualStyleBackColor = True
		'
		'AppendDateTimeCheckBox
		'
		Me.AppendDateTimeCheckBox.AutoSize = True
		Me.AppendDateTimeCheckBox.IsReadOnly = False
		Me.AppendDateTimeCheckBox.Location = New System.Drawing.Point(3, 49)
		Me.AppendDateTimeCheckBox.Name = "AppendDateTimeCheckBox"
		Me.AppendDateTimeCheckBox.Size = New System.Drawing.Size(204, 17)
		Me.AppendDateTimeCheckBox.TabIndex = 2
		Me.AppendDateTimeCheckBox.Text = "Append the item update date-time"
		Me.AppendDateTimeCheckBox.UseVisualStyleBackColor = True
		'
		'ReplaceSpacesWithUnderscoresCheckBox
		'
		Me.ReplaceSpacesWithUnderscoresCheckBox.AutoSize = True
		Me.ReplaceSpacesWithUnderscoresCheckBox.IsReadOnly = False
		Me.ReplaceSpacesWithUnderscoresCheckBox.Location = New System.Drawing.Point(3, 72)
		Me.ReplaceSpacesWithUnderscoresCheckBox.Name = "ReplaceSpacesWithUnderscoresCheckBox"
		Me.ReplaceSpacesWithUnderscoresCheckBox.Size = New System.Drawing.Size(195, 17)
		Me.ReplaceSpacesWithUnderscoresCheckBox.TabIndex = 3
		Me.ReplaceSpacesWithUnderscoresCheckBox.Text = "Replace spaces with underscores"
		Me.ReplaceSpacesWithUnderscoresCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsUseDefaultsButton
		'
		Me.OptionsUseDefaultsButton.Location = New System.Drawing.Point(3, 95)
		Me.OptionsUseDefaultsButton.Name = "OptionsUseDefaultsButton"
		Me.OptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.OptionsUseDefaultsButton.TabIndex = 4
		Me.OptionsUseDefaultsButton.Text = "Use Defaults"
		Me.OptionsUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'ConvertToExpectedFileOrFolderCheckBox
		'
		Me.ConvertToExpectedFileOrFolderCheckBox.AutoSize = True
		Me.ConvertToExpectedFileOrFolderCheckBox.IsReadOnly = False
		Me.ConvertToExpectedFileOrFolderCheckBox.Location = New System.Drawing.Point(230, 3)
		Me.ConvertToExpectedFileOrFolderCheckBox.Name = "ConvertToExpectedFileOrFolderCheckBox"
		Me.ConvertToExpectedFileOrFolderCheckBox.Size = New System.Drawing.Size(196, 17)
		Me.ConvertToExpectedFileOrFolderCheckBox.TabIndex = 7
		Me.ConvertToExpectedFileOrFolderCheckBox.Text = "Convert to expected file or folder"
		Me.ToolTip1.SetToolTip(Me.ConvertToExpectedFileOrFolderCheckBox, "Example: Garry's Mod uses compressed GMA (LZMA) instead of GMA.")
		Me.ConvertToExpectedFileOrFolderCheckBox.UseVisualStyleBackColor = True
		'
		'ExampleOutputFileNameLabel
		'
		Me.ExampleOutputFileNameLabel.AutoSize = True
		Me.ExampleOutputFileNameLabel.Location = New System.Drawing.Point(3, 131)
		Me.ExampleOutputFileNameLabel.Name = "ExampleOutputFileNameLabel"
		Me.ExampleOutputFileNameLabel.Size = New System.Drawing.Size(141, 13)
		Me.ExampleOutputFileNameLabel.TabIndex = 5
		Me.ExampleOutputFileNameLabel.Text = "Example output file name:"
		'
		'ExampleOutputFileNameTextBox
		'
		Me.ExampleOutputFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ExampleOutputFileNameTextBox.CueBannerText = ""
		Me.ExampleOutputFileNameTextBox.Location = New System.Drawing.Point(3, 147)
		Me.ExampleOutputFileNameTextBox.Name = "ExampleOutputFileNameTextBox"
		Me.ExampleOutputFileNameTextBox.ReadOnly = True
		Me.ExampleOutputFileNameTextBox.Size = New System.Drawing.Size(758, 22)
		Me.ExampleOutputFileNameTextBox.TabIndex = 6
		'
		'CancelDownloadButton
		'
		Me.CancelDownloadButton.Enabled = False
		Me.CancelDownloadButton.Location = New System.Drawing.Point(126, 0)
		Me.CancelDownloadButton.Name = "CancelDownloadButton"
		Me.CancelDownloadButton.Size = New System.Drawing.Size(120, 23)
		Me.CancelDownloadButton.TabIndex = 11
		Me.CancelDownloadButton.Text = "Cancel Download"
		Me.CancelDownloadButton.UseVisualStyleBackColor = True
		'
		'DownloadProgressBar
		'
		Me.DownloadProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DownloadProgressBar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.DownloadProgressBar.Location = New System.Drawing.Point(252, 0)
		Me.DownloadProgressBar.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
		Me.DownloadProgressBar.Name = "DownloadProgressBar"
		Me.DownloadProgressBar.Size = New System.Drawing.Size(515, 23)
		Me.DownloadProgressBar.TabIndex = 12
		'
		'OpenWorkshopPageButton
		'
		Me.OpenWorkshopPageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OpenWorkshopPageButton.Location = New System.Drawing.Point(709, 3)
		Me.OpenWorkshopPageButton.Name = "OpenWorkshopPageButton"
		Me.OpenWorkshopPageButton.Size = New System.Drawing.Size(64, 23)
		Me.OpenWorkshopPageButton.TabIndex = 2
		Me.OpenWorkshopPageButton.Text = "Open"
		Me.OpenWorkshopPageButton.UseVisualStyleBackColor = True
		'
		'DocumentsOutputPathTextBox
		'
		Me.DocumentsOutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DocumentsOutputPathTextBox.CueBannerText = ""
		Me.DocumentsOutputPathTextBox.Location = New System.Drawing.Point(233, 32)
		Me.DocumentsOutputPathTextBox.Name = "DocumentsOutputPathTextBox"
		Me.DocumentsOutputPathTextBox.ReadOnly = True
		Me.DocumentsOutputPathTextBox.Size = New System.Drawing.Size(421, 22)
		Me.DocumentsOutputPathTextBox.TabIndex = 6
		'
		'DownloadedItemTextBox
		'
		Me.DownloadedItemTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DownloadedItemTextBox.CueBannerText = ""
		Me.DownloadedItemTextBox.Location = New System.Drawing.Point(83, 3)
		Me.DownloadedItemTextBox.Name = "DownloadedItemTextBox"
		Me.DownloadedItemTextBox.ReadOnly = True
		Me.DownloadedItemTextBox.Size = New System.Drawing.Size(542, 22)
		Me.DownloadedItemTextBox.TabIndex = 15
		'
		'DownloadedLabel
		'
		Me.DownloadedLabel.AutoSize = True
		Me.DownloadedLabel.Location = New System.Drawing.Point(0, 8)
		Me.DownloadedLabel.Name = "DownloadedLabel"
		Me.DownloadedLabel.Size = New System.Drawing.Size(77, 13)
		Me.DownloadedLabel.TabIndex = 14
		Me.DownloadedLabel.Text = "Downloaded:"
		'
		'GotoDownloadedItemButton
		'
		Me.GotoDownloadedItemButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoDownloadedItemButton.Location = New System.Drawing.Point(727, 3)
		Me.GotoDownloadedItemButton.Name = "GotoDownloadedItemButton"
		Me.GotoDownloadedItemButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoDownloadedItemButton.TabIndex = 16
		Me.GotoDownloadedItemButton.Text = "Goto"
		Me.GotoDownloadedItemButton.UseVisualStyleBackColor = True
		'
		'DownloadUserControlFillPanel
		'
		Me.DownloadUserControlFillPanel.Controls.Add(Me.ItemIdOrLinkLabel)
		Me.DownloadUserControlFillPanel.Controls.Add(Me.ItemIdTextBox)
		Me.DownloadUserControlFillPanel.Controls.Add(Me.OpenWorkshopPageButton)
		Me.DownloadUserControlFillPanel.Controls.Add(Me.OuputToLabel)
		Me.DownloadUserControlFillPanel.Controls.Add(Me.OutputPathComboBox)
		Me.DownloadUserControlFillPanel.Controls.Add(Me.OutputPathTextBox)
		Me.DownloadUserControlFillPanel.Controls.Add(Me.DocumentsOutputPathTextBox)
		Me.DownloadUserControlFillPanel.Controls.Add(Me.BrowseForOutputPathButton)
		Me.DownloadUserControlFillPanel.Controls.Add(Me.GotoOutputPathButton)
		Me.DownloadUserControlFillPanel.Controls.Add(Me.Options_LogSplitContainer)
		Me.DownloadUserControlFillPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DownloadUserControlFillPanel.Location = New System.Drawing.Point(0, 0)
		Me.DownloadUserControlFillPanel.Name = "DownloadUserControlFillPanel"
		Me.DownloadUserControlFillPanel.Size = New System.Drawing.Size(776, 536)
		Me.DownloadUserControlFillPanel.TabIndex = 17
		'
		'Options_LogSplitContainer
		'
		Me.Options_LogSplitContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Options_LogSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.Options_LogSplitContainer.Location = New System.Drawing.Point(3, 61)
		Me.Options_LogSplitContainer.Name = "Options_LogSplitContainer"
		Me.Options_LogSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'Options_LogSplitContainer.Panel1
		'
		Me.Options_LogSplitContainer.Panel1.Controls.Add(Me.OptionsGroupBox)
		'
		'Options_LogSplitContainer.Panel2
		'
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.LogTextBox)
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.DownloadButtonsPanel)
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.PostDownloadPanel)
		Me.Options_LogSplitContainer.Size = New System.Drawing.Size(770, 475)
		Me.Options_LogSplitContainer.SplitterDistance = 193
		Me.Options_LogSplitContainer.TabIndex = 17
		'
		'DownloadButtonsPanel
		'
		Me.DownloadButtonsPanel.Controls.Add(Me.DownloadButton)
		Me.DownloadButtonsPanel.Controls.Add(Me.CancelDownloadButton)
		Me.DownloadButtonsPanel.Controls.Add(Me.DownloadProgressBar)
		Me.DownloadButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.DownloadButtonsPanel.Location = New System.Drawing.Point(0, 0)
		Me.DownloadButtonsPanel.Name = "DownloadButtonsPanel"
		Me.DownloadButtonsPanel.Size = New System.Drawing.Size(770, 26)
		Me.DownloadButtonsPanel.TabIndex = 19
		'
		'PostDownloadPanel
		'
		Me.PostDownloadPanel.Controls.Add(Me.UseInUnpackButton)
		Me.PostDownloadPanel.Controls.Add(Me.DownloadedLabel)
		Me.PostDownloadPanel.Controls.Add(Me.DownloadedItemTextBox)
		Me.PostDownloadPanel.Controls.Add(Me.GotoDownloadedItemButton)
		Me.PostDownloadPanel.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PostDownloadPanel.Location = New System.Drawing.Point(0, 252)
		Me.PostDownloadPanel.Name = "PostDownloadPanel"
		Me.PostDownloadPanel.Size = New System.Drawing.Size(770, 26)
		Me.PostDownloadPanel.TabIndex = 18
		'
		'UseInUnpackButton
		'
		Me.UseInUnpackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInUnpackButton.Location = New System.Drawing.Point(631, 3)
		Me.UseInUnpackButton.Name = "UseInUnpackButton"
		Me.UseInUnpackButton.Size = New System.Drawing.Size(90, 23)
		Me.UseInUnpackButton.TabIndex = 17
		Me.UseInUnpackButton.Text = "Use In Unpack"
		Me.UseInUnpackButton.UseVisualStyleBackColor = True
		'
		'Timer1
		'
		Me.Timer1.Interval = 1000
		'
		'DownloadUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.DownloadUserControlFillPanel)
		Me.Name = "DownloadUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.OptionsGroupBoxFillPanel.ResumeLayout(False)
		Me.OptionsGroupBoxFillPanel.PerformLayout()
		Me.DownloadUserControlFillPanel.ResumeLayout(False)
		Me.DownloadUserControlFillPanel.PerformLayout()
		Me.Options_LogSplitContainer.Panel1.ResumeLayout(False)
		Me.Options_LogSplitContainer.Panel2.ResumeLayout(False)
		CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Options_LogSplitContainer.ResumeLayout(False)
		Me.DownloadButtonsPanel.ResumeLayout(False)
		Me.PostDownloadPanel.ResumeLayout(False)
		Me.PostDownloadPanel.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents ItemIdTextBox As TextBoxEx
	Friend WithEvents DownloadButton As Button
	Friend WithEvents LogTextBox As RichTextBoxEx
	Friend WithEvents ItemIdOrLinkLabel As Label
	Friend WithEvents OuputToLabel As Label
	Friend WithEvents OutputPathComboBox As ComboBox
	Friend WithEvents OutputPathTextBox As TextBoxEx
	Friend WithEvents GotoOutputPathButton As Button
	Friend WithEvents BrowseForOutputPathButton As Button
	Friend WithEvents OptionsGroupBox As GroupBoxEx
	Friend WithEvents CancelDownloadButton As Button
	Friend WithEvents ExampleOutputFileNameLabel As Label
	Friend WithEvents AppendDateTimeCheckBox As CheckBoxEx
	Friend WithEvents PrependTitleCheckBox As CheckBoxEx
	Friend WithEvents UseIdCheckBox As CheckBoxEx
	Friend WithEvents ReplaceSpacesWithUnderscoresCheckBox As CheckBoxEx
	Friend WithEvents OptionsUseDefaultsButton As Button
	Friend WithEvents DownloadProgressBar As ProgressBarEx
	Friend WithEvents OpenWorkshopPageButton As Button
	Friend WithEvents DocumentsOutputPathTextBox As TextBoxEx
	Friend WithEvents DownloadedItemTextBox As TextBoxEx
	Friend WithEvents DownloadedLabel As Label
	Friend WithEvents GotoDownloadedItemButton As Button
	Friend WithEvents ExampleOutputFileNameTextBox As TextBoxEx
	Friend WithEvents ConvertToExpectedFileOrFolderCheckBox As CheckBoxEx
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents DownloadUserControlFillPanel As Panel
	Friend WithEvents Timer1 As Timer
	Friend WithEvents Options_LogSplitContainer As SplitContainer
	Friend WithEvents PostDownloadPanel As Panel
	Friend WithEvents DownloadButtonsPanel As Panel
	Friend WithEvents OptionsGroupBoxFillPanel As Panel
	Friend WithEvents UseInUnpackButton As Button
End Class
