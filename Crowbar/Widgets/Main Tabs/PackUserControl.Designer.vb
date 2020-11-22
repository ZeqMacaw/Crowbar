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
		Me.Options_LogSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
		Me.OptionsGroupBoxFillPanel = New System.Windows.Forms.Panel()
		Me.PackerOptionsPanel = New System.Windows.Forms.Panel()
		Me.MultiFileVpkCheckBox = New System.Windows.Forms.CheckBox()
		Me.PackOptionsUseDefaultsButton = New System.Windows.Forms.Button()
		Me.LogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox()
		Me.SetUpGamesButton = New System.Windows.Forms.Button()
		Me.GmaPanel = New System.Windows.Forms.Panel()
		Me.GmaTitleTextBox = New Crowbar.TextBoxEx()
		Me.GmaTitleLabel = New System.Windows.Forms.Label()
		Me.GmaGarrysModTagsUserControl = New Crowbar.GarrysModTagsUserControl()
		Me.DirectPackerOptionsLabel = New System.Windows.Forms.Label()
		Me.DirectPackerOptionsTextBox = New System.Windows.Forms.TextBox()
		Me.PackerOptionsTextBox = New System.Windows.Forms.TextBox()
		Me.PackerOptionsTextBoxMinScrollPanel = New System.Windows.Forms.Panel()
		Me.LogRichTextBox = New Crowbar.RichTextBoxEx()
		Me.PackButtonsPanel = New System.Windows.Forms.Panel()
		Me.PackButton = New System.Windows.Forms.Button()
		Me.SkipCurrentFolderButton = New System.Windows.Forms.Button()
		Me.CancelPackButton = New System.Windows.Forms.Button()
		Me.UseAllInPublishButton = New System.Windows.Forms.Button()
		Me.PostPackPanel = New System.Windows.Forms.Panel()
		Me.PackedFilesComboBox = New System.Windows.Forms.ComboBox()
		Me.UseInPublishButton = New System.Windows.Forms.Button()
		Me.GotoPackedFileButton = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.Panel1.SuspendLayout()
		CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Options_LogSplitContainer.Panel1.SuspendLayout()
		Me.Options_LogSplitContainer.Panel2.SuspendLayout()
		Me.Options_LogSplitContainer.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		Me.OptionsGroupBoxFillPanel.SuspendLayout()
		Me.PackerOptionsPanel.SuspendLayout()
		Me.GmaPanel.SuspendLayout()
		Me.PackButtonsPanel.SuspendLayout()
		Me.PostPackPanel.SuspendLayout()
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
		Me.Panel1.Controls.Add(Me.Options_LogSplitContainer)
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
		Me.OutputPathTextBox.AllowDrop = True
		Me.OutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputPathTextBox.CueBannerText = ""
		Me.OutputPathTextBox.Location = New System.Drawing.Point(223, 32)
		Me.OutputPathTextBox.Name = "OutputPathTextBox"
		Me.OutputPathTextBox.Size = New System.Drawing.Size(431, 22)
		Me.OutputPathTextBox.TabIndex = 25
		'
		'OutputParentPathTextBox
		'
		Me.OutputParentPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputParentPathTextBox.CueBannerText = ""
		Me.OutputParentPathTextBox.Location = New System.Drawing.Point(223, 32)
		Me.OutputParentPathTextBox.Name = "OutputParentPathTextBox"
		Me.OutputParentPathTextBox.ReadOnly = True
		Me.OutputParentPathTextBox.Size = New System.Drawing.Size(431, 22)
		Me.OutputParentPathTextBox.TabIndex = 24
		Me.OutputParentPathTextBox.Visible = False
		'
		'OutputPathComboBox
		'
		Me.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.OutputPathComboBox.FormattingEnabled = True
		Me.OutputPathComboBox.Location = New System.Drawing.Point(77, 33)
		Me.OutputPathComboBox.Name = "OutputPathComboBox"
		Me.OutputPathComboBox.Size = New System.Drawing.Size(140, 21)
		Me.OutputPathComboBox.TabIndex = 23
		'
		'InputComboBox
		'
		Me.InputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.InputComboBox.FormattingEnabled = True
		Me.InputComboBox.Location = New System.Drawing.Point(77, 4)
		Me.InputComboBox.Name = "InputComboBox"
		Me.InputComboBox.Size = New System.Drawing.Size(140, 21)
		Me.InputComboBox.TabIndex = 0
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 37)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(62, 13)
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
		Me.Label6.Size = New System.Drawing.Size(74, 13)
		Me.Label6.TabIndex = 17
		Me.Label6.Text = "Folder input:"
		'
		'InputPathFileNameTextBox
		'
		Me.InputPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.InputPathFileNameTextBox.CueBannerText = ""
		Me.InputPathFileNameTextBox.Location = New System.Drawing.Point(223, 3)
		Me.InputPathFileNameTextBox.Name = "InputPathFileNameTextBox"
		Me.InputPathFileNameTextBox.Size = New System.Drawing.Size(431, 22)
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
		Me.Options_LogSplitContainer.Panel1MinSize = 45
		'
		'Options_LogSplitContainer.Panel2
		'
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.LogRichTextBox)
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.PackButtonsPanel)
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.PostPackPanel)
		Me.Options_LogSplitContainer.Panel2MinSize = 45
		Me.Options_LogSplitContainer.Size = New System.Drawing.Size(770, 472)
		Me.Options_LogSplitContainer.SplitterDistance = 260
		Me.Options_LogSplitContainer.TabIndex = 29
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Controls.Add(Me.OptionsGroupBoxFillPanel)
		Me.OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.Size = New System.Drawing.Size(770, 260)
		Me.OptionsGroupBox.TabIndex = 0
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'OptionsGroupBoxFillPanel
		'
		Me.OptionsGroupBoxFillPanel.AutoScroll = True
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.PackerOptionsPanel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.DirectPackerOptionsLabel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.DirectPackerOptionsTextBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.PackerOptionsTextBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.PackerOptionsTextBoxMinScrollPanel)
		Me.OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsGroupBoxFillPanel.Location = New System.Drawing.Point(3, 18)
		Me.OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel"
		Me.OptionsGroupBoxFillPanel.Size = New System.Drawing.Size(764, 239)
		Me.OptionsGroupBoxFillPanel.TabIndex = 19
		'
		'PackerOptionsPanel
		'
		Me.PackerOptionsPanel.AutoScroll = True
		Me.PackerOptionsPanel.Controls.Add(Me.MultiFileVpkCheckBox)
		Me.PackerOptionsPanel.Controls.Add(Me.PackOptionsUseDefaultsButton)
		Me.PackerOptionsPanel.Controls.Add(Me.LogFileCheckBox)
		Me.PackerOptionsPanel.Controls.Add(Me.Label3)
		Me.PackerOptionsPanel.Controls.Add(Me.GameSetupComboBox)
		Me.PackerOptionsPanel.Controls.Add(Me.SetUpGamesButton)
		Me.PackerOptionsPanel.Controls.Add(Me.GmaPanel)
		Me.PackerOptionsPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.PackerOptionsPanel.Location = New System.Drawing.Point(0, 0)
		Me.PackerOptionsPanel.Name = "PackerOptionsPanel"
		Me.PackerOptionsPanel.Size = New System.Drawing.Size(764, 153)
		Me.PackerOptionsPanel.TabIndex = 0
		'
		'MultiFileVpkCheckBox
		'
		Me.MultiFileVpkCheckBox.AutoSize = True
		Me.MultiFileVpkCheckBox.Location = New System.Drawing.Point(6, 51)
		Me.MultiFileVpkCheckBox.Name = "MultiFileVpkCheckBox"
		Me.MultiFileVpkCheckBox.Size = New System.Drawing.Size(125, 17)
		Me.MultiFileVpkCheckBox.TabIndex = 13
		Me.MultiFileVpkCheckBox.Text = "Write multi-file VPK"
		Me.MultiFileVpkCheckBox.UseVisualStyleBackColor = True
		Me.MultiFileVpkCheckBox.Visible = False
		'
		'PackOptionsUseDefaultsButton
		'
		Me.PackOptionsUseDefaultsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackOptionsUseDefaultsButton.Location = New System.Drawing.Point(674, 127)
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
		Me.LogFileCheckBox.Size = New System.Drawing.Size(116, 17)
		Me.LogFileCheckBox.TabIndex = 4
		Me.LogFileCheckBox.Text = "Write log to a file"
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(0, 5)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(165, 13)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Game that has the packer tool:"
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(171, 1)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(497, 21)
		Me.GameSetupComboBox.TabIndex = 1
		'
		'SetUpGamesButton
		'
		Me.SetUpGamesButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SetUpGamesButton.Location = New System.Drawing.Point(674, 0)
		Me.SetUpGamesButton.Name = "SetUpGamesButton"
		Me.SetUpGamesButton.Size = New System.Drawing.Size(90, 23)
		Me.SetUpGamesButton.TabIndex = 2
		Me.SetUpGamesButton.Text = "Set Up Games"
		Me.SetUpGamesButton.UseVisualStyleBackColor = True
		'
		'GmaPanel
		'
		Me.GmaPanel.Controls.Add(Me.GmaTitleTextBox)
		Me.GmaPanel.Controls.Add(Me.GmaTitleLabel)
		Me.GmaPanel.Controls.Add(Me.GmaGarrysModTagsUserControl)
		Me.GmaPanel.Location = New System.Drawing.Point(217, 29)
		Me.GmaPanel.Name = "GmaPanel"
		Me.GmaPanel.Size = New System.Drawing.Size(423, 122)
		Me.GmaPanel.TabIndex = 0
		'
		'GmaTitleTextBox
		'
		Me.GmaTitleTextBox.CueBannerText = ""
		Me.GmaTitleTextBox.Location = New System.Drawing.Point(42, 1)
		Me.GmaTitleTextBox.Name = "GmaTitleTextBox"
		Me.GmaTitleTextBox.Size = New System.Drawing.Size(317, 22)
		Me.GmaTitleTextBox.TabIndex = 14
		'
		'GmaTitleLabel
		'
		Me.GmaTitleLabel.AutoSize = True
		Me.GmaTitleLabel.Location = New System.Drawing.Point(3, 6)
		Me.GmaTitleLabel.Name = "GmaTitleLabel"
		Me.GmaTitleLabel.Size = New System.Drawing.Size(32, 13)
		Me.GmaTitleLabel.TabIndex = 4
		Me.GmaTitleLabel.Text = "Title:"
		'
		'GmaGarrysModTagsUserControl
		'
		Me.GmaGarrysModTagsUserControl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GmaGarrysModTagsUserControl.Location = New System.Drawing.Point(0, 28)
		Me.GmaGarrysModTagsUserControl.Name = "GmaGarrysModTagsUserControl"
		Me.GmaGarrysModTagsUserControl.Orientation = Crowbar.AppEnums.OrientationType.Horizontal
		Me.GmaGarrysModTagsUserControl.Size = New System.Drawing.Size(362, 94)
		Me.GmaGarrysModTagsUserControl.TabIndex = 15
		'
		'DirectPackerOptionsLabel
		'
		Me.DirectPackerOptionsLabel.Location = New System.Drawing.Point(0, 154)
		Me.DirectPackerOptionsLabel.Name = "DirectPackerOptionsLabel"
		Me.DirectPackerOptionsLabel.Size = New System.Drawing.Size(764, 13)
		Me.DirectPackerOptionsLabel.TabIndex = 16
		Me.DirectPackerOptionsLabel.Text = "Direct entry of command-line options (in case they are not included above):"
		'
		'DirectPackerOptionsTextBox
		'
		Me.DirectPackerOptionsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DirectPackerOptionsTextBox.Location = New System.Drawing.Point(0, 170)
		Me.DirectPackerOptionsTextBox.Name = "DirectPackerOptionsTextBox"
		Me.DirectPackerOptionsTextBox.Size = New System.Drawing.Size(764, 22)
		Me.DirectPackerOptionsTextBox.TabIndex = 17
		'
		'PackerOptionsTextBox
		'
		Me.PackerOptionsTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackerOptionsTextBox.Location = New System.Drawing.Point(0, 198)
		Me.PackerOptionsTextBox.Multiline = True
		Me.PackerOptionsTextBox.Name = "PackerOptionsTextBox"
		Me.PackerOptionsTextBox.ReadOnly = True
		Me.PackerOptionsTextBox.Size = New System.Drawing.Size(764, 37)
		Me.PackerOptionsTextBox.TabIndex = 18
		'
		'PackerOptionsTextBoxMinScrollPanel
		'
		Me.PackerOptionsTextBoxMinScrollPanel.Location = New System.Drawing.Point(0, 198)
		Me.PackerOptionsTextBoxMinScrollPanel.Name = "PackerOptionsTextBoxMinScrollPanel"
		Me.PackerOptionsTextBoxMinScrollPanel.Size = New System.Drawing.Size(764, 37)
		Me.PackerOptionsTextBoxMinScrollPanel.TabIndex = 42
		'
		'LogRichTextBox
		'
		Me.LogRichTextBox.CueBannerText = ""
		Me.LogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LogRichTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LogRichTextBox.HideSelection = False
		Me.LogRichTextBox.Location = New System.Drawing.Point(0, 26)
		Me.LogRichTextBox.Name = "LogRichTextBox"
		Me.LogRichTextBox.ReadOnly = True
		Me.LogRichTextBox.Size = New System.Drawing.Size(770, 156)
		Me.LogRichTextBox.TabIndex = 0
		Me.LogRichTextBox.Text = ""
		Me.LogRichTextBox.WordWrap = False
		'
		'PackButtonsPanel
		'
		Me.PackButtonsPanel.Controls.Add(Me.PackButton)
		Me.PackButtonsPanel.Controls.Add(Me.SkipCurrentFolderButton)
		Me.PackButtonsPanel.Controls.Add(Me.CancelPackButton)
		Me.PackButtonsPanel.Controls.Add(Me.UseAllInPublishButton)
		Me.PackButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.PackButtonsPanel.Location = New System.Drawing.Point(0, 0)
		Me.PackButtonsPanel.Name = "PackButtonsPanel"
		Me.PackButtonsPanel.Size = New System.Drawing.Size(770, 26)
		Me.PackButtonsPanel.TabIndex = 5
		'
		'PackButton
		'
		Me.PackButton.Location = New System.Drawing.Point(0, 0)
		Me.PackButton.Name = "PackButton"
		Me.PackButton.Size = New System.Drawing.Size(120, 23)
		Me.PackButton.TabIndex = 1
		Me.PackButton.Text = "Pack"
		Me.PackButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentFolderButton
		'
		Me.SkipCurrentFolderButton.Enabled = False
		Me.SkipCurrentFolderButton.Location = New System.Drawing.Point(126, 0)
		Me.SkipCurrentFolderButton.Name = "SkipCurrentFolderButton"
		Me.SkipCurrentFolderButton.Size = New System.Drawing.Size(120, 23)
		Me.SkipCurrentFolderButton.TabIndex = 2
		Me.SkipCurrentFolderButton.Text = "Skip Current Folder"
		Me.SkipCurrentFolderButton.UseVisualStyleBackColor = True
		'
		'CancelPackButton
		'
		Me.CancelPackButton.Enabled = False
		Me.CancelPackButton.Location = New System.Drawing.Point(252, 0)
		Me.CancelPackButton.Name = "CancelPackButton"
		Me.CancelPackButton.Size = New System.Drawing.Size(120, 23)
		Me.CancelPackButton.TabIndex = 3
		Me.CancelPackButton.Text = "Cancel Pack"
		Me.CancelPackButton.UseVisualStyleBackColor = True
		'
		'UseAllInPublishButton
		'
		Me.UseAllInPublishButton.Enabled = False
		Me.UseAllInPublishButton.Location = New System.Drawing.Point(378, 0)
		Me.UseAllInPublishButton.Name = "UseAllInPublishButton"
		Me.UseAllInPublishButton.Size = New System.Drawing.Size(120, 23)
		Me.UseAllInPublishButton.TabIndex = 4
		Me.UseAllInPublishButton.Text = "Use All in Publish"
		Me.UseAllInPublishButton.UseVisualStyleBackColor = True
		Me.UseAllInPublishButton.Visible = False
		'
		'PostPackPanel
		'
		Me.PostPackPanel.Controls.Add(Me.PackedFilesComboBox)
		Me.PostPackPanel.Controls.Add(Me.UseInPublishButton)
		Me.PostPackPanel.Controls.Add(Me.GotoPackedFileButton)
		Me.PostPackPanel.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PostPackPanel.Location = New System.Drawing.Point(0, 182)
		Me.PostPackPanel.Name = "PostPackPanel"
		Me.PostPackPanel.Size = New System.Drawing.Size(770, 26)
		Me.PostPackPanel.TabIndex = 6
		'
		'PackedFilesComboBox
		'
		Me.PackedFilesComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackedFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.PackedFilesComboBox.FormattingEnabled = True
		Me.PackedFilesComboBox.Location = New System.Drawing.Point(0, 4)
		Me.PackedFilesComboBox.Name = "PackedFilesComboBox"
		Me.PackedFilesComboBox.Size = New System.Drawing.Size(721, 21)
		Me.PackedFilesComboBox.TabIndex = 1
		'
		'UseInPublishButton
		'
		Me.UseInPublishButton.Enabled = False
		Me.UseInPublishButton.Location = New System.Drawing.Point(632, 3)
		Me.UseInPublishButton.Name = "UseInPublishButton"
		Me.UseInPublishButton.Size = New System.Drawing.Size(89, 23)
		Me.UseInPublishButton.TabIndex = 3
		Me.UseInPublishButton.Text = "Use in Publish"
		Me.UseInPublishButton.UseVisualStyleBackColor = True
		Me.UseInPublishButton.Visible = False
		'
		'GotoPackedFileButton
		'
		Me.GotoPackedFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoPackedFileButton.Location = New System.Drawing.Point(727, 3)
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
		Me.Options_LogSplitContainer.Panel1.ResumeLayout(False)
		Me.Options_LogSplitContainer.Panel2.ResumeLayout(False)
		CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Options_LogSplitContainer.ResumeLayout(False)
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.OptionsGroupBoxFillPanel.ResumeLayout(False)
		Me.OptionsGroupBoxFillPanel.PerformLayout()
		Me.PackerOptionsPanel.ResumeLayout(False)
		Me.PackerOptionsPanel.PerformLayout()
		Me.GmaPanel.ResumeLayout(False)
		Me.GmaPanel.PerformLayout()
		Me.PackButtonsPanel.ResumeLayout(False)
		Me.PostPackPanel.ResumeLayout(False)
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
	Friend WithEvents Options_LogSplitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents UseAllInPublishButton As System.Windows.Forms.Button
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents PackerOptionsPanel As System.Windows.Forms.Panel
	Friend WithEvents PackOptionsUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents LogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents SetUpGamesButton As System.Windows.Forms.Button
	Friend WithEvents CancelPackButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentFolderButton As System.Windows.Forms.Button
	Friend WithEvents PackButton As System.Windows.Forms.Button
	Friend WithEvents UseInPublishButton As System.Windows.Forms.Button
	Friend WithEvents LogRichTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents PackedFilesComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoPackedFileButton As System.Windows.Forms.Button
	Friend WithEvents DirectPackerOptionsLabel As Label
	Friend WithEvents DirectPackerOptionsTextBox As TextBox
	Friend WithEvents PackerOptionsTextBox As TextBox
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents MultiFileVpkCheckBox As CheckBox
	Friend WithEvents PackButtonsPanel As Panel
	Friend WithEvents PostPackPanel As Panel
	Friend WithEvents OptionsGroupBoxFillPanel As Panel
	Friend WithEvents PackerOptionsTextBoxMinScrollPanel As Panel
	Friend WithEvents GmaTitleLabel As Label
	Friend WithEvents GmaTitleTextBox As TextBoxEx
	Friend WithEvents GmaGarrysModTagsUserControl As GarrysModTagsUserControl
	Friend WithEvents GmaPanel As Panel
End Class
