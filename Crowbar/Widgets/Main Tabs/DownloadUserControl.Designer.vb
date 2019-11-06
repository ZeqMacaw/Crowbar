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
		Me.ConvertToExpectedFileOrFolderCheckBox = New Crowbar.CheckBoxEx()
		Me.OptionsUseDefaultsButton = New System.Windows.Forms.Button()
		Me.ReplaceSpacesWithUnderscoresCheckBox = New Crowbar.CheckBoxEx()
		Me.AppendDateTimeCheckBox = New Crowbar.CheckBoxEx()
		Me.PrependTitleCheckBox = New Crowbar.CheckBoxEx()
		Me.UseIdCheckBox = New Crowbar.CheckBoxEx()
		Me.ExampleOutputFileNameLabel = New System.Windows.Forms.Label()
		Me.ExampleOutputFileNameTextBox = New Crowbar.TextBoxEx()
		Me.CancelDownloadButton = New System.Windows.Forms.Button()
		Me.DownloadProgressBar = New Crowbar.ProgressBarEx()
		Me.OpenWorkshopPageButton = New System.Windows.Forms.Button()
		Me.DocumentsOutputPathTextBox = New Crowbar.TextBoxEx()
		Me.DownloadedItemTextBox = New Crowbar.TextBoxEx()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.DownloadedItemButton = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.OptionsGroupBox.SuspendLayout()
		Me.SuspendLayout()
		'
		'ItemIdTextBox
		'
		Me.ItemIdTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemIdTextBox.CueBannerText = ""
		Me.ItemIdTextBox.Location = New System.Drawing.Point(87, 3)
		Me.ItemIdTextBox.Name = "ItemIdTextBox"
		Me.ItemIdTextBox.Size = New System.Drawing.Size(643, 21)
		Me.ItemIdTextBox.TabIndex = 1
		'
		'DownloadButton
		'
		Me.DownloadButton.Location = New System.Drawing.Point(3, 261)
		Me.DownloadButton.Name = "DownloadButton"
		Me.DownloadButton.Size = New System.Drawing.Size(120, 23)
		Me.DownloadButton.TabIndex = 10
		Me.DownloadButton.Text = "Download"
		Me.DownloadButton.UseVisualStyleBackColor = True
		'
		'LogTextBox
		'
		Me.LogTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.LogTextBox.CueBannerText = ""
		Me.LogTextBox.HideSelection = False
		Me.LogTextBox.Location = New System.Drawing.Point(3, 290)
		Me.LogTextBox.Name = "LogTextBox"
		Me.LogTextBox.ReadOnly = True
		Me.LogTextBox.Size = New System.Drawing.Size(776, 229)
		Me.LogTextBox.TabIndex = 13
		Me.LogTextBox.Text = ""
		'
		'ItemIdOrLinkLabel
		'
		Me.ItemIdOrLinkLabel.AutoSize = True
		Me.ItemIdOrLinkLabel.Location = New System.Drawing.Point(3, 6)
		Me.ItemIdOrLinkLabel.Name = "ItemIdOrLinkLabel"
		Me.ItemIdOrLinkLabel.Size = New System.Drawing.Size(78, 13)
		Me.ItemIdOrLinkLabel.TabIndex = 0
		Me.ItemIdOrLinkLabel.Text = "Item ID or link:"
		'
		'OuputToLabel
		'
		Me.OuputToLabel.AutoSize = True
		Me.OuputToLabel.Location = New System.Drawing.Point(3, 33)
		Me.OuputToLabel.Name = "OuputToLabel"
		Me.OuputToLabel.Size = New System.Drawing.Size(58, 13)
		Me.OuputToLabel.TabIndex = 3
		Me.OuputToLabel.Text = "Output to:"
		'
		'OutputPathComboBox
		'
		Me.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.OutputPathComboBox.FormattingEnabled = True
		Me.OutputPathComboBox.Location = New System.Drawing.Point(87, 30)
		Me.OutputPathComboBox.Name = "OutputPathComboBox"
		Me.OutputPathComboBox.Size = New System.Drawing.Size(140, 21)
		Me.OutputPathComboBox.TabIndex = 4
		'
		'OutputPathTextBox
		'
		Me.OutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputPathTextBox.CueBannerText = ""
		Me.OutputPathTextBox.Location = New System.Drawing.Point(233, 30)
		Me.OutputPathTextBox.Name = "OutputPathTextBox"
		Me.OutputPathTextBox.Size = New System.Drawing.Size(427, 21)
		Me.OutputPathTextBox.TabIndex = 5
		'
		'GotoOutputPathButton
		'
		Me.GotoOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoOutputPathButton.Location = New System.Drawing.Point(736, 30)
		Me.GotoOutputPathButton.Name = "GotoOutputPathButton"
		Me.GotoOutputPathButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoOutputPathButton.TabIndex = 8
		Me.GotoOutputPathButton.Text = "Goto"
		Me.GotoOutputPathButton.UseVisualStyleBackColor = True
		'
		'BrowseForOutputPathButton
		'
		Me.BrowseForOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForOutputPathButton.Location = New System.Drawing.Point(666, 30)
		Me.BrowseForOutputPathButton.Name = "BrowseForOutputPathButton"
		Me.BrowseForOutputPathButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForOutputPathButton.TabIndex = 7
		Me.BrowseForOutputPathButton.Text = "Browse..."
		Me.BrowseForOutputPathButton.UseVisualStyleBackColor = True
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OptionsGroupBox.Controls.Add(Me.ConvertToExpectedFileOrFolderCheckBox)
		Me.OptionsGroupBox.Controls.Add(Me.OptionsUseDefaultsButton)
		Me.OptionsGroupBox.Controls.Add(Me.ReplaceSpacesWithUnderscoresCheckBox)
		Me.OptionsGroupBox.Controls.Add(Me.AppendDateTimeCheckBox)
		Me.OptionsGroupBox.Controls.Add(Me.PrependTitleCheckBox)
		Me.OptionsGroupBox.Controls.Add(Me.UseIdCheckBox)
		Me.OptionsGroupBox.Controls.Add(Me.ExampleOutputFileNameLabel)
		Me.OptionsGroupBox.Controls.Add(Me.ExampleOutputFileNameTextBox)
		Me.OptionsGroupBox.IsReadOnly = False
		Me.OptionsGroupBox.Location = New System.Drawing.Point(3, 57)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.SelectedValue = Nothing
		Me.OptionsGroupBox.Size = New System.Drawing.Size(776, 198)
		Me.OptionsGroupBox.TabIndex = 9
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Output File Name Options"
		'
		'ConvertToExpectedFileOrFolderCheckBox
		'
		Me.ConvertToExpectedFileOrFolderCheckBox.AutoSize = True
		Me.ConvertToExpectedFileOrFolderCheckBox.IsReadOnly = False
		Me.ConvertToExpectedFileOrFolderCheckBox.Location = New System.Drawing.Point(230, 20)
		Me.ConvertToExpectedFileOrFolderCheckBox.Name = "ConvertToExpectedFileOrFolderCheckBox"
		Me.ConvertToExpectedFileOrFolderCheckBox.Size = New System.Drawing.Size(187, 17)
		Me.ConvertToExpectedFileOrFolderCheckBox.TabIndex = 7
		Me.ConvertToExpectedFileOrFolderCheckBox.Text = "Convert to expected file or folder"
		Me.ToolTip1.SetToolTip(Me.ConvertToExpectedFileOrFolderCheckBox, "Example: Garry's Mod uses compressed GMA (LZMA) instead of GMA.")
		Me.ConvertToExpectedFileOrFolderCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsUseDefaultsButton
		'
		Me.OptionsUseDefaultsButton.Location = New System.Drawing.Point(6, 112)
		Me.OptionsUseDefaultsButton.Name = "OptionsUseDefaultsButton"
		Me.OptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.OptionsUseDefaultsButton.TabIndex = 4
		Me.OptionsUseDefaultsButton.Text = "Use Defaults"
		Me.OptionsUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'ReplaceSpacesWithUnderscoresCheckBox
		'
		Me.ReplaceSpacesWithUnderscoresCheckBox.AutoSize = True
		Me.ReplaceSpacesWithUnderscoresCheckBox.IsReadOnly = False
		Me.ReplaceSpacesWithUnderscoresCheckBox.Location = New System.Drawing.Point(6, 89)
		Me.ReplaceSpacesWithUnderscoresCheckBox.Name = "ReplaceSpacesWithUnderscoresCheckBox"
		Me.ReplaceSpacesWithUnderscoresCheckBox.Size = New System.Drawing.Size(185, 17)
		Me.ReplaceSpacesWithUnderscoresCheckBox.TabIndex = 3
		Me.ReplaceSpacesWithUnderscoresCheckBox.Text = "Replace spaces with underscores"
		Me.ReplaceSpacesWithUnderscoresCheckBox.UseVisualStyleBackColor = True
		'
		'AppendDateTimeCheckBox
		'
		Me.AppendDateTimeCheckBox.AutoSize = True
		Me.AppendDateTimeCheckBox.IsReadOnly = False
		Me.AppendDateTimeCheckBox.Location = New System.Drawing.Point(6, 66)
		Me.AppendDateTimeCheckBox.Name = "AppendDateTimeCheckBox"
		Me.AppendDateTimeCheckBox.Size = New System.Drawing.Size(191, 17)
		Me.AppendDateTimeCheckBox.TabIndex = 2
		Me.AppendDateTimeCheckBox.Text = "Append the item update date-time"
		Me.AppendDateTimeCheckBox.UseVisualStyleBackColor = True
		'
		'PrependTitleCheckBox
		'
		Me.PrependTitleCheckBox.AutoSize = True
		Me.PrependTitleCheckBox.IsReadOnly = False
		Me.PrependTitleCheckBox.Location = New System.Drawing.Point(6, 43)
		Me.PrependTitleCheckBox.Name = "PrependTitleCheckBox"
		Me.PrependTitleCheckBox.Size = New System.Drawing.Size(110, 17)
		Me.PrependTitleCheckBox.TabIndex = 1
		Me.PrependTitleCheckBox.Text = "Prepend item title"
		Me.PrependTitleCheckBox.UseVisualStyleBackColor = True
		'
		'UseIdCheckBox
		'
		Me.UseIdCheckBox.AutoSize = True
		Me.UseIdCheckBox.IsReadOnly = False
		Me.UseIdCheckBox.Location = New System.Drawing.Point(6, 20)
		Me.UseIdCheckBox.Name = "UseIdCheckBox"
		Me.UseIdCheckBox.Size = New System.Drawing.Size(190, 17)
		Me.UseIdCheckBox.TabIndex = 0
		Me.UseIdCheckBox.Text = "Use item ID instead of given name"
		Me.UseIdCheckBox.UseVisualStyleBackColor = True
		'
		'ExampleOutputFileNameLabel
		'
		Me.ExampleOutputFileNameLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.ExampleOutputFileNameLabel.AutoSize = True
		Me.ExampleOutputFileNameLabel.Location = New System.Drawing.Point(6, 155)
		Me.ExampleOutputFileNameLabel.Name = "ExampleOutputFileNameLabel"
		Me.ExampleOutputFileNameLabel.Size = New System.Drawing.Size(132, 13)
		Me.ExampleOutputFileNameLabel.TabIndex = 5
		Me.ExampleOutputFileNameLabel.Text = "Example output file name:"
		'
		'ExampleOutputFileNameTextBox
		'
		Me.ExampleOutputFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ExampleOutputFileNameTextBox.CueBannerText = ""
		Me.ExampleOutputFileNameTextBox.Location = New System.Drawing.Point(6, 171)
		Me.ExampleOutputFileNameTextBox.Name = "ExampleOutputFileNameTextBox"
		Me.ExampleOutputFileNameTextBox.ReadOnly = True
		Me.ExampleOutputFileNameTextBox.Size = New System.Drawing.Size(761, 21)
		Me.ExampleOutputFileNameTextBox.TabIndex = 6
		'
		'CancelDownloadButton
		'
		Me.CancelDownloadButton.Enabled = False
		Me.CancelDownloadButton.Location = New System.Drawing.Point(129, 261)
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
		Me.DownloadProgressBar.Location = New System.Drawing.Point(255, 261)
		Me.DownloadProgressBar.Name = "DownloadProgressBar"
		Me.DownloadProgressBar.Size = New System.Drawing.Size(524, 23)
		Me.DownloadProgressBar.TabIndex = 12
		'
		'OpenWorkshopPageButton
		'
		Me.OpenWorkshopPageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OpenWorkshopPageButton.Location = New System.Drawing.Point(736, 1)
		Me.OpenWorkshopPageButton.Name = "OpenWorkshopPageButton"
		Me.OpenWorkshopPageButton.Size = New System.Drawing.Size(43, 23)
		Me.OpenWorkshopPageButton.TabIndex = 2
		Me.OpenWorkshopPageButton.Text = "Open"
		Me.OpenWorkshopPageButton.UseVisualStyleBackColor = True
		'
		'DocumentsOutputPathTextBox
		'
		Me.DocumentsOutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DocumentsOutputPathTextBox.CueBannerText = ""
		Me.DocumentsOutputPathTextBox.Location = New System.Drawing.Point(233, 30)
		Me.DocumentsOutputPathTextBox.Name = "DocumentsOutputPathTextBox"
		Me.DocumentsOutputPathTextBox.ReadOnly = True
		Me.DocumentsOutputPathTextBox.Size = New System.Drawing.Size(427, 21)
		Me.DocumentsOutputPathTextBox.TabIndex = 6
		'
		'DownloadedItemTextBox
		'
		Me.DownloadedItemTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DownloadedItemTextBox.CueBannerText = ""
		Me.DownloadedItemTextBox.Location = New System.Drawing.Point(79, 525)
		Me.DownloadedItemTextBox.Name = "DownloadedItemTextBox"
		Me.DownloadedItemTextBox.ReadOnly = True
		Me.DownloadedItemTextBox.Size = New System.Drawing.Size(651, 21)
		Me.DownloadedItemTextBox.TabIndex = 15
		'
		'Label1
		'
		Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 528)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(70, 13)
		Me.Label1.TabIndex = 14
		Me.Label1.Text = "Downloaded:"
		'
		'DownloadedItemButton
		'
		Me.DownloadedItemButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DownloadedItemButton.Location = New System.Drawing.Point(736, 523)
		Me.DownloadedItemButton.Name = "DownloadedItemButton"
		Me.DownloadedItemButton.Size = New System.Drawing.Size(43, 23)
		Me.DownloadedItemButton.TabIndex = 16
		Me.DownloadedItemButton.Text = "Goto"
		Me.DownloadedItemButton.UseVisualStyleBackColor = True
		'
		'DownloadUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.DownloadedItemButton)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.DownloadedItemTextBox)
		Me.Controls.Add(Me.ItemIdOrLinkLabel)
		Me.Controls.Add(Me.OpenWorkshopPageButton)
		Me.Controls.Add(Me.BrowseForOutputPathButton)
		Me.Controls.Add(Me.GotoOutputPathButton)
		Me.Controls.Add(Me.OutputPathTextBox)
		Me.Controls.Add(Me.DocumentsOutputPathTextBox)
		Me.Controls.Add(Me.OutputPathComboBox)
		Me.Controls.Add(Me.OuputToLabel)
		Me.Controls.Add(Me.ItemIdTextBox)
		Me.Controls.Add(Me.OptionsGroupBox)
		Me.Controls.Add(Me.DownloadButton)
		Me.Controls.Add(Me.CancelDownloadButton)
		Me.Controls.Add(Me.DownloadProgressBar)
		Me.Controls.Add(Me.LogTextBox)
		Me.Name = "DownloadUserControl"
		Me.Size = New System.Drawing.Size(784, 546)
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.OptionsGroupBox.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

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
	Friend WithEvents Label1 As Label
	Friend WithEvents DownloadedItemButton As Button
	Friend WithEvents ExampleOutputFileNameTextBox As TextBoxEx
	Friend WithEvents ConvertToExpectedFileOrFolderCheckBox As CheckBoxEx
	Friend WithEvents ToolTip1 As ToolTip
End Class
