<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PackUserControl
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
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.GotoOutputPathButton = New System.Windows.Forms.Button()
		Me.BrowseForOutputPathButton = New System.Windows.Forms.Button()
		Me.OutputPathTextBox = New Crowbar.TextBoxEx()
		Me.OutputParentPathTextBox = New Crowbar.TextBoxEx()
		Me.OutputPathComboBox = New System.Windows.Forms.ComboBox()
		Me.InputComboBox = New System.Windows.Forms.ComboBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.GotoInputPathButton = New System.Windows.Forms.Button()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.InputPathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.BrowseForInputFolderOrFileNameButton = New System.Windows.Forms.Button()
		Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
		Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.GmaTitleLabel = New System.Windows.Forms.Label()
		Me.GmaTitleTextBox = New Crowbar.TextBoxEx()
		Me.MultiFileVpkCheckBox = New System.Windows.Forms.CheckBox()
		Me.PackOptionsUseDefaultsButton = New System.Windows.Forms.Button()
		Me.LogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox()
		Me.SetUpGamesButton = New System.Windows.Forms.Button()
		Me.DirectPackerOptionsLabel = New System.Windows.Forms.Label()
		Me.DirectPackerOptionsTextBox = New System.Windows.Forms.TextBox()
		Me.PackerOptionsTextBox = New System.Windows.Forms.TextBox()
		Me.PackerOptionsSizerLabel = New System.Windows.Forms.Label()
		Me.PackButton = New System.Windows.Forms.Button()
		Me.SkipCurrentFolderButton = New System.Windows.Forms.Button()
		Me.CancelPackButton = New System.Windows.Forms.Button()
		Me.UseAllInPublishButton = New System.Windows.Forms.Button()
		Me.LogRichTextBox = New Crowbar.RichTextBoxEx()
		Me.UseInReleaseButton = New System.Windows.Forms.Button()
		Me.PackedFilesComboBox = New System.Windows.Forms.ComboBox()
		Me.GotoPackedFileButton = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.Panel1.SuspendLayout()
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer2.Panel1.SuspendLayout()
		Me.SplitContainer2.Panel2.SuspendLayout()
		Me.SplitContainer2.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		Me.Panel2.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.GotoOutputPathButton)
		Me.Panel1.Controls.Add(Me.BrowseForOutputPathButton)
		Me.Panel1.Controls.Add(Me.OutputPathTextBox)
		Me.Panel1.Controls.Add(Me.OutputParentPathTextBox)
		Me.Panel1.Controls.Add(Me.OutputPathComboBox)
		Me.Panel1.Controls.Add(Me.InputComboBox)
		Me.Panel1.Controls.Add(Me.Label1)
		Me.Panel1.Controls.Add(Me.GotoInputPathButton)
		Me.Panel1.Controls.Add(Me.Label6)
		Me.Panel1.Controls.Add(Me.InputPathFileNameTextBox)
		Me.Panel1.Controls.Add(Me.BrowseForInputFolderOrFileNameButton)
		Me.Panel1.Controls.Add(Me.SplitContainer2)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(776, 536)
		Me.Panel1.TabIndex = 3
		'
		'GotoOutputPathButton
		'
		Me.GotoOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoOutputPathButton.Location = New System.Drawing.Point(730, 32)
		Me.GotoOutputPathButton.Name = "GotoOutputPathButton"
		Me.GotoOutputPathButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoOutputPathButton.TabIndex = 27
		Me.GotoOutputPathButton.Text = "Goto"
		Me.GotoOutputPathButton.UseVisualStyleBackColor = True
		'
		'BrowseForOutputPathButton
		'
		Me.BrowseForOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForOutputPathButton.Location = New System.Drawing.Point(660, 32)
		Me.BrowseForOutputPathButton.Name = "BrowseForOutputPathButton"
		Me.BrowseForOutputPathButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForOutputPathButton.TabIndex = 26
		Me.BrowseForOutputPathButton.Text = "Browse..."
		Me.BrowseForOutputPathButton.UseVisualStyleBackColor = True
		'
		'OutputPathTextBox
		'
		Me.OutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputPathTextBox.CueBannerText = ""
		Me.OutputPathTextBox.Location = New System.Drawing.Point(223, 34)
		Me.OutputPathTextBox.Name = "OutputPathTextBox"
		Me.OutputPathTextBox.Size = New System.Drawing.Size(431, 21)
		Me.OutputPathTextBox.TabIndex = 25
		'
		'OutputParentPathTextBox
		'
		Me.OutputParentPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputParentPathTextBox.CueBannerText = ""
		Me.OutputParentPathTextBox.Location = New System.Drawing.Point(223, 34)
		Me.OutputParentPathTextBox.Name = "OutputParentPathTextBox"
		Me.OutputParentPathTextBox.ReadOnly = True
		Me.OutputParentPathTextBox.Size = New System.Drawing.Size(423, 21)
		Me.OutputParentPathTextBox.TabIndex = 24
		Me.OutputParentPathTextBox.Visible = False
		'
		'OutputPathComboBox
		'
		Me.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.OutputPathComboBox.FormattingEnabled = True
		Me.OutputPathComboBox.Location = New System.Drawing.Point(77, 34)
		Me.OutputPathComboBox.Name = "OutputPathComboBox"
		Me.OutputPathComboBox.Size = New System.Drawing.Size(140, 21)
		Me.OutputPathComboBox.TabIndex = 23
		'
		'InputComboBox
		'
		Me.InputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.InputComboBox.FormattingEnabled = True
		Me.InputComboBox.Location = New System.Drawing.Point(77, 5)
		Me.InputComboBox.Name = "InputComboBox"
		Me.InputComboBox.Size = New System.Drawing.Size(140, 21)
		Me.InputComboBox.TabIndex = 0
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 37)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(58, 13)
		Me.Label1.TabIndex = 22
		Me.Label1.Text = "Output to:"
		'
		'GotoInputPathButton
		'
		Me.GotoInputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoInputPathButton.Location = New System.Drawing.Point(730, 3)
		Me.GotoInputPathButton.Name = "GotoInputPathButton"
		Me.GotoInputPathButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoInputPathButton.TabIndex = 21
		Me.GotoInputPathButton.Text = "Goto"
		Me.GotoInputPathButton.UseVisualStyleBackColor = True
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(3, 8)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(68, 13)
		Me.Label6.TabIndex = 17
		Me.Label6.Text = "Folder input:"
		'
		'InputPathFileNameTextBox
		'
		Me.InputPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.InputPathFileNameTextBox.CueBannerText = ""
		Me.InputPathFileNameTextBox.Location = New System.Drawing.Point(223, 5)
		Me.InputPathFileNameTextBox.Name = "InputPathFileNameTextBox"
		Me.InputPathFileNameTextBox.Size = New System.Drawing.Size(431, 21)
		Me.InputPathFileNameTextBox.TabIndex = 1
		'
		'BrowseForInputFolderOrFileNameButton
		'
		Me.BrowseForInputFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForInputFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForInputFolderOrFileNameButton.Name = "BrowseForInputFolderOrFileNameButton"
		Me.BrowseForInputFolderOrFileNameButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForInputFolderOrFileNameButton.TabIndex = 20
		Me.BrowseForInputFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForInputFolderOrFileNameButton.UseVisualStyleBackColor = True
		'
		'SplitContainer2
		'
		Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer2.Location = New System.Drawing.Point(3, 61)
		Me.SplitContainer2.Name = "SplitContainer2"
		Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer2.Panel1
		'
		Me.SplitContainer2.Panel1.Controls.Add(Me.OptionsGroupBox)
		Me.SplitContainer2.Panel1.Controls.Add(Me.PackButton)
		Me.SplitContainer2.Panel1.Controls.Add(Me.SkipCurrentFolderButton)
		Me.SplitContainer2.Panel1.Controls.Add(Me.CancelPackButton)
		Me.SplitContainer2.Panel1.Controls.Add(Me.UseAllInPublishButton)
		Me.SplitContainer2.Panel1MinSize = 90
		'
		'SplitContainer2.Panel2
		'
		Me.SplitContainer2.Panel2.Controls.Add(Me.LogRichTextBox)
		Me.SplitContainer2.Panel2.Controls.Add(Me.UseInReleaseButton)
		Me.SplitContainer2.Panel2.Controls.Add(Me.PackedFilesComboBox)
		Me.SplitContainer2.Panel2.Controls.Add(Me.GotoPackedFileButton)
		Me.SplitContainer2.Panel2MinSize = 90
		Me.SplitContainer2.Size = New System.Drawing.Size(770, 472)
		Me.SplitContainer2.SplitterDistance = 296
		Me.SplitContainer2.TabIndex = 29
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OptionsGroupBox.Controls.Add(Me.Panel2)
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.Size = New System.Drawing.Size(770, 265)
		Me.OptionsGroupBox.TabIndex = 0
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'Panel2
		'
		Me.Panel2.AutoScroll = True
		Me.Panel2.Controls.Add(Me.GmaTitleLabel)
		Me.Panel2.Controls.Add(Me.GmaTitleTextBox)
		Me.Panel2.Controls.Add(Me.MultiFileVpkCheckBox)
		Me.Panel2.Controls.Add(Me.PackOptionsUseDefaultsButton)
		Me.Panel2.Controls.Add(Me.LogFileCheckBox)
		Me.Panel2.Controls.Add(Me.Label3)
		Me.Panel2.Controls.Add(Me.GameSetupComboBox)
		Me.Panel2.Controls.Add(Me.SetUpGamesButton)
		Me.Panel2.Controls.Add(Me.DirectPackerOptionsLabel)
		Me.Panel2.Controls.Add(Me.DirectPackerOptionsTextBox)
		Me.Panel2.Controls.Add(Me.PackerOptionsTextBox)
		Me.Panel2.Controls.Add(Me.PackerOptionsSizerLabel)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(3, 17)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(764, 245)
		Me.Panel2.TabIndex = 0
		'
		'GmaTitleLabel
		'
		Me.GmaTitleLabel.AutoSize = True
		Me.GmaTitleLabel.Location = New System.Drawing.Point(182, 31)
		Me.GmaTitleLabel.Name = "GmaTitleLabel"
		Me.GmaTitleLabel.Size = New System.Drawing.Size(31, 13)
		Me.GmaTitleLabel.TabIndex = 27
		Me.GmaTitleLabel.Text = "Title:"
		Me.GmaTitleLabel.Visible = False
		'
		'GmaTitleTextBox
		'
		Me.GmaTitleTextBox.CueBannerText = ""
		Me.GmaTitleTextBox.Location = New System.Drawing.Point(223, 28)
		Me.GmaTitleTextBox.Name = "GmaTitleTextBox"
		Me.GmaTitleTextBox.Size = New System.Drawing.Size(315, 21)
		Me.GmaTitleTextBox.TabIndex = 26
		Me.GmaTitleTextBox.Visible = False
		'
		'MultiFileVpkCheckBox
		'
		Me.MultiFileVpkCheckBox.AutoSize = True
		Me.MultiFileVpkCheckBox.Location = New System.Drawing.Point(6, 51)
		Me.MultiFileVpkCheckBox.Name = "MultiFileVpkCheckBox"
		Me.MultiFileVpkCheckBox.Size = New System.Drawing.Size(116, 17)
		Me.MultiFileVpkCheckBox.TabIndex = 13
		Me.MultiFileVpkCheckBox.Text = "Write multi-file VPK"
		Me.MultiFileVpkCheckBox.UseVisualStyleBackColor = True
		Me.MultiFileVpkCheckBox.Visible = False
		'
		'PackOptionsUseDefaultsButton
		'
		Me.PackOptionsUseDefaultsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackOptionsUseDefaultsButton.Location = New System.Drawing.Point(671, 92)
		Me.PackOptionsUseDefaultsButton.Name = "PackOptionsUseDefaultsButton"
		Me.PackOptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.PackOptionsUseDefaultsButton.TabIndex = 12
		Me.PackOptionsUseDefaultsButton.Text = "Use Defaults"
		Me.PackOptionsUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'LogFileCheckBox
		'
		Me.LogFileCheckBox.AutoSize = True
		Me.LogFileCheckBox.Location = New System.Drawing.Point(6, 28)
		Me.LogFileCheckBox.Name = "LogFileCheckBox"
		Me.LogFileCheckBox.Size = New System.Drawing.Size(108, 17)
		Me.LogFileCheckBox.TabIndex = 4
		Me.LogFileCheckBox.Text = "Write log to a file"
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(3, 5)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(156, 13)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Game that has the packer tool:"
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(165, 1)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(500, 21)
		Me.GameSetupComboBox.TabIndex = 1
		'
		'SetUpGamesButton
		'
		Me.SetUpGamesButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SetUpGamesButton.Location = New System.Drawing.Point(671, 0)
		Me.SetUpGamesButton.Name = "SetUpGamesButton"
		Me.SetUpGamesButton.Size = New System.Drawing.Size(90, 23)
		Me.SetUpGamesButton.TabIndex = 2
		Me.SetUpGamesButton.Text = "Set Up Games"
		Me.SetUpGamesButton.UseVisualStyleBackColor = True
		'
		'DirectPackerOptionsLabel
		'
		Me.DirectPackerOptionsLabel.Location = New System.Drawing.Point(3, 155)
		Me.DirectPackerOptionsLabel.Name = "DirectPackerOptionsLabel"
		Me.DirectPackerOptionsLabel.Size = New System.Drawing.Size(393, 13)
		Me.DirectPackerOptionsLabel.TabIndex = 16
		Me.DirectPackerOptionsLabel.Text = "Direct entry of command-line options (in case they are not included above):"
		'
		'DirectPackerOptionsTextBox
		'
		Me.DirectPackerOptionsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DirectPackerOptionsTextBox.Location = New System.Drawing.Point(3, 171)
		Me.DirectPackerOptionsTextBox.Name = "DirectPackerOptionsTextBox"
		Me.DirectPackerOptionsTextBox.Size = New System.Drawing.Size(758, 21)
		Me.DirectPackerOptionsTextBox.TabIndex = 17
		'
		'PackerOptionsTextBox
		'
		Me.PackerOptionsTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackerOptionsTextBox.Location = New System.Drawing.Point(3, 198)
		Me.PackerOptionsTextBox.Multiline = True
		Me.PackerOptionsTextBox.Name = "PackerOptionsTextBox"
		Me.PackerOptionsTextBox.ReadOnly = True
		Me.PackerOptionsTextBox.Size = New System.Drawing.Size(758, 44)
		Me.PackerOptionsTextBox.TabIndex = 18
		'
		'PackerOptionsSizerLabel
		'
		Me.PackerOptionsSizerLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackerOptionsSizerLabel.Enabled = False
		Me.PackerOptionsSizerLabel.Location = New System.Drawing.Point(3, 198)
		Me.PackerOptionsSizerLabel.Name = "PackerOptionsSizerLabel"
		Me.PackerOptionsSizerLabel.Size = New System.Drawing.Size(742, 44)
		Me.PackerOptionsSizerLabel.TabIndex = 38
		'
		'PackButton
		'
		Me.PackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.PackButton.Location = New System.Drawing.Point(0, 271)
		Me.PackButton.Name = "PackButton"
		Me.PackButton.Size = New System.Drawing.Size(110, 23)
		Me.PackButton.TabIndex = 1
		Me.PackButton.Text = "Pack"
		Me.PackButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentFolderButton
		'
		Me.SkipCurrentFolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentFolderButton.Enabled = False
		Me.SkipCurrentFolderButton.Location = New System.Drawing.Point(116, 271)
		Me.SkipCurrentFolderButton.Name = "SkipCurrentFolderButton"
		Me.SkipCurrentFolderButton.Size = New System.Drawing.Size(110, 23)
		Me.SkipCurrentFolderButton.TabIndex = 2
		Me.SkipCurrentFolderButton.Text = "Skip Current Folder"
		Me.SkipCurrentFolderButton.UseVisualStyleBackColor = True
		'
		'CancelPackButton
		'
		Me.CancelPackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CancelPackButton.Enabled = False
		Me.CancelPackButton.Location = New System.Drawing.Point(232, 271)
		Me.CancelPackButton.Name = "CancelPackButton"
		Me.CancelPackButton.Size = New System.Drawing.Size(110, 23)
		Me.CancelPackButton.TabIndex = 3
		Me.CancelPackButton.Text = "Cancel Pack"
		Me.CancelPackButton.UseVisualStyleBackColor = True
		'
		'UseAllInPublishButton
		'
		Me.UseAllInPublishButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UseAllInPublishButton.Enabled = False
		Me.UseAllInPublishButton.Location = New System.Drawing.Point(348, 271)
		Me.UseAllInPublishButton.Name = "UseAllInPublishButton"
		Me.UseAllInPublishButton.Size = New System.Drawing.Size(110, 23)
		Me.UseAllInPublishButton.TabIndex = 4
		Me.UseAllInPublishButton.Text = "Use All in Publish"
		Me.UseAllInPublishButton.UseVisualStyleBackColor = True
		Me.UseAllInPublishButton.Visible = False
		'
		'LogRichTextBox
		'
		Me.LogRichTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.LogRichTextBox.CueBannerText = ""
		Me.LogRichTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LogRichTextBox.HideSelection = False
		Me.LogRichTextBox.Location = New System.Drawing.Point(0, 0)
		Me.LogRichTextBox.Name = "LogRichTextBox"
		Me.LogRichTextBox.ReadOnly = True
		Me.LogRichTextBox.Size = New System.Drawing.Size(770, 143)
		Me.LogRichTextBox.TabIndex = 0
		Me.LogRichTextBox.Text = ""
		Me.LogRichTextBox.WordWrap = False
		'
		'UseInReleaseButton
		'
		Me.UseInReleaseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInReleaseButton.Enabled = False
		Me.UseInReleaseButton.Location = New System.Drawing.Point(632, 149)
		Me.UseInReleaseButton.Name = "UseInReleaseButton"
		Me.UseInReleaseButton.Size = New System.Drawing.Size(89, 23)
		Me.UseInReleaseButton.TabIndex = 3
		Me.UseInReleaseButton.Text = "Use in Release"
		Me.UseInReleaseButton.UseVisualStyleBackColor = True
		Me.UseInReleaseButton.Visible = False
		'
		'PackedFilesComboBox
		'
		Me.PackedFilesComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackedFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.PackedFilesComboBox.FormattingEnabled = True
		Me.PackedFilesComboBox.Location = New System.Drawing.Point(0, 150)
		Me.PackedFilesComboBox.Name = "PackedFilesComboBox"
		Me.PackedFilesComboBox.Size = New System.Drawing.Size(721, 21)
		Me.PackedFilesComboBox.TabIndex = 1
		'
		'GotoPackedFileButton
		'
		Me.GotoPackedFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoPackedFileButton.Location = New System.Drawing.Point(727, 149)
		Me.GotoPackedFileButton.Name = "GotoPackedFileButton"
		Me.GotoPackedFileButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoPackedFileButton.TabIndex = 4
		Me.GotoPackedFileButton.Text = "Goto"
		Me.GotoPackedFileButton.UseVisualStyleBackColor = True
		'
		'PackUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Name = "PackUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.SplitContainer2.Panel1.ResumeLayout(False)
		Me.SplitContainer2.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer2.ResumeLayout(False)
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents OutputParentPathTextBox As Crowbar.TextBoxEx
	Friend WithEvents GotoOutputPathButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForOutputPathButton As System.Windows.Forms.Button
	Friend WithEvents OutputPathTextBox As Crowbar.TextBoxEx
	Friend WithEvents OutputPathComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents InputComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents GotoInputPathButton As System.Windows.Forms.Button
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents InputPathFileNameTextBox As Crowbar.TextBoxEx
	Friend WithEvents BrowseForInputFolderOrFileNameButton As System.Windows.Forms.Button
	Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
	Friend WithEvents UseAllInPublishButton As System.Windows.Forms.Button
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents PackOptionsUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents LogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents SetUpGamesButton As System.Windows.Forms.Button
	Friend WithEvents CancelPackButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentFolderButton As System.Windows.Forms.Button
	Friend WithEvents PackButton As System.Windows.Forms.Button
	Friend WithEvents UseInReleaseButton As System.Windows.Forms.Button
	Friend WithEvents LogRichTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents PackedFilesComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoPackedFileButton As System.Windows.Forms.Button
	Friend WithEvents DirectPackerOptionsLabel As Label
	Friend WithEvents DirectPackerOptionsTextBox As TextBox
	Friend WithEvents PackerOptionsTextBox As TextBox
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents MultiFileVpkCheckBox As CheckBox
	Friend WithEvents GmaTitleTextBox As TextBoxEx
	Friend WithEvents GmaTitleLabel As Label
	Friend WithEvents PackerOptionsSizerLabel As Label
End Class
