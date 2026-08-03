<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PackUserControl
	Inherits BaseUserControl

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.Panel1 = New Crowbar.PanelEx()
		Me.GotoOutputPathButton = New Crowbar.ButtonEx()
		Me.BrowseForOutputPathButton = New Crowbar.ButtonEx()
		Me.OutputPathTextBox = New Crowbar.RichTextBoxEx()
		Me.OutputParentPathTextBox = New Crowbar.RichTextBoxEx()
		Me.OutputPathComboUserControl = New Crowbar.ComboUserControl()
		Me.InputComboUserControl = New Crowbar.ComboUserControl()
		Me.Label1 = New Crowbar.LabelEx()
		Me.GotoInputPathButton = New Crowbar.ButtonEx()
		Me.Label6 = New Crowbar.LabelEx()
		Me.InputPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.BrowseForInputFolderOrFileNameButton = New Crowbar.ButtonEx()
		Me.Options_LogSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.OptionsGroupBox = New Crowbar.GroupBoxEx()
		Me.OptionsGroupBoxFillPanel = New Crowbar.PanelEx()
		Me.PackOptionsPanel = New Crowbar.PanelEx()
		Me.MultiFileVpkCheckBox = New Crowbar.CheckBoxEx()
		Me.PackOptionsUseDefaultsButton = New Crowbar.ButtonEx()
		Me.LogFileCheckBox = New Crowbar.CheckBoxEx()
		Me.GmaPanel = New Crowbar.PanelEx()
		Me.GmaTitleTextBox = New Crowbar.RichTextBoxEx()
		Me.GmaTitleLabel = New Crowbar.LabelEx()
		Me.GmaGarrysModTagsUserControl = New Crowbar.GarrysModTagsUserControl()
		Me.GameSetupPanel = New Crowbar.PanelEx()
		Me.GameSetupLabel = New Crowbar.LabelEx()
		Me.GameSetupComboUserControl = New Crowbar.ComboUserControl()
		Me.SetUpGamesButton = New Crowbar.ButtonEx()
		Me.DirectPackOptionsLabel = New Crowbar.LabelEx()
		Me.DirectPackOptionsPanel = New Crowbar.PanelEx()
		Me.DirectPackOptionsTextBox = New Crowbar.RichTextBoxEx()
		Me.PackOptionsTextPanel = New Crowbar.PanelEx()
		Me.PackOptionsTextBox = New Crowbar.RichTextBoxEx()
		Me.PackOptionsTextBoxMinScrollPanel = New Crowbar.PanelEx()
		Me.LogRichTextBox = New Crowbar.RichTextBoxEx()
		Me.PackButtonsPanel = New Crowbar.PanelEx()
		Me.PackButton = New Crowbar.ButtonEx()
		Me.SkipCurrentFolderButton = New Crowbar.ButtonEx()
		Me.CancelPackButton = New Crowbar.ButtonEx()
		Me.UseAllInPublishButton = New Crowbar.ButtonEx()
		Me.PostPackPanel = New Crowbar.PanelEx()
		Me.PackedFilesComboUserControl = New Crowbar.ComboUserControl()
		Me.UseInPublishButton = New Crowbar.ButtonEx()
		Me.GotoPackedFileButton = New Crowbar.ButtonEx()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.Panel1.SuspendLayout()
		CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Options_LogSplitContainer.Panel1.SuspendLayout()
		Me.Options_LogSplitContainer.Panel2.SuspendLayout()
		Me.Options_LogSplitContainer.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		Me.OptionsGroupBoxFillPanel.SuspendLayout()
		Me.PackOptionsPanel.SuspendLayout()
		Me.GmaPanel.SuspendLayout()
		Me.GameSetupPanel.SuspendLayout()
		Me.DirectPackOptionsPanel.SuspendLayout()
		Me.PackOptionsTextPanel.SuspendLayout()
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
		Me.Panel1.Controls.Add(Me.OutputPathComboUserControl)
		Me.Panel1.Controls.Add(Me.InputComboUserControl)
		Me.Panel1.Controls.Add(Me.Label1)
		Me.Panel1.Controls.Add(Me.GotoInputPathButton)
		Me.Panel1.Controls.Add(Me.Label6)
		Me.Panel1.Controls.Add(Me.InputPathFileNameTextBox)
		Me.Panel1.Controls.Add(Me.BrowseForInputFolderOrFileNameButton)
		Me.Panel1.Controls.Add(Me.Options_LogSplitContainer)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.SelectedIndex = -1
		Me.Panel1.SelectedValue = Nothing
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
		Me.OutputPathTextBox.DetectUrls = False
		Me.OutputPathTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.OutputPathTextBox.Location = New System.Drawing.Point(223, 32)
		Me.OutputPathTextBox.Multiline = False
		Me.OutputPathTextBox.Name = "OutputPathTextBox"
		Me.OutputPathTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.OutputPathTextBox.Size = New System.Drawing.Size(431, 22)
		Me.OutputPathTextBox.TabIndex = 25
		Me.OutputPathTextBox.Text = ""
		'
		'OutputParentPathTextBox
		'
		Me.OutputParentPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputParentPathTextBox.CueBannerText = ""
		Me.OutputParentPathTextBox.DetectUrls = False
		Me.OutputParentPathTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.OutputParentPathTextBox.Location = New System.Drawing.Point(223, 32)
		Me.OutputParentPathTextBox.Multiline = False
		Me.OutputParentPathTextBox.Name = "OutputParentPathTextBox"
		Me.OutputParentPathTextBox.ReadOnly = True
		Me.OutputParentPathTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.OutputParentPathTextBox.Size = New System.Drawing.Size(431, 22)
		Me.OutputParentPathTextBox.TabIndex = 24
		Me.OutputParentPathTextBox.Text = ""
		Me.OutputParentPathTextBox.Visible = False
		'
		'OutputPathComboBox
		'
		Me.OutputPathComboUserControl.IsReadOnly = False
		Me.OutputPathComboUserControl.Location = New System.Drawing.Point(77, 33)
		Me.OutputPathComboUserControl.Name = "OutputPathComboBox"
		Me.OutputPathComboUserControl.Size = New System.Drawing.Size(140, 23)
		Me.OutputPathComboUserControl.TabIndex = 23
		'
		'InputComboBox
		'
		Me.InputComboUserControl.IsReadOnly = False
		Me.InputComboUserControl.Location = New System.Drawing.Point(77, 4)
		Me.InputComboUserControl.Name = "InputComboBox"
		Me.InputComboUserControl.Size = New System.Drawing.Size(140, 23)
		Me.InputComboUserControl.TabIndex = 0
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
		Me.InputPathFileNameTextBox.DetectUrls = False
		Me.InputPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.InputPathFileNameTextBox.Location = New System.Drawing.Point(223, 3)
		Me.InputPathFileNameTextBox.Multiline = False
		Me.InputPathFileNameTextBox.Name = "InputPathFileNameTextBox"
		Me.InputPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.InputPathFileNameTextBox.Size = New System.Drawing.Size(431, 22)
		Me.InputPathFileNameTextBox.TabIndex = 1
		Me.InputPathFileNameTextBox.Text = ""
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
		Me.OptionsGroupBox.IsReadOnly = False
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.SelectedValue = Nothing
		Me.OptionsGroupBox.Size = New System.Drawing.Size(770, 260)
		Me.OptionsGroupBox.TabIndex = 0
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'OptionsGroupBoxFillPanel
		'
		Me.OptionsGroupBoxFillPanel.AutoScroll = True
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.PackOptionsPanel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.DirectPackOptionsLabel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.DirectPackOptionsPanel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.PackOptionsTextPanel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.PackOptionsTextBoxMinScrollPanel)
		Me.OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsGroupBoxFillPanel.Location = New System.Drawing.Point(3, 18)
		Me.OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel"
		Me.OptionsGroupBoxFillPanel.SelectedIndex = -1
		Me.OptionsGroupBoxFillPanel.SelectedValue = Nothing
		Me.OptionsGroupBoxFillPanel.Size = New System.Drawing.Size(764, 239)
		Me.OptionsGroupBoxFillPanel.TabIndex = 19
		'
		'PackOptionsPanel
		'
		Me.PackOptionsPanel.Controls.Add(Me.MultiFileVpkCheckBox)
		Me.PackOptionsPanel.Controls.Add(Me.PackOptionsUseDefaultsButton)
		Me.PackOptionsPanel.Controls.Add(Me.LogFileCheckBox)
		Me.PackOptionsPanel.Controls.Add(Me.GmaPanel)
		Me.PackOptionsPanel.Controls.Add(Me.GameSetupPanel)
		Me.PackOptionsPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.PackOptionsPanel.Location = New System.Drawing.Point(0, 0)
		Me.PackOptionsPanel.Name = "PackOptionsPanel"
		Me.PackOptionsPanel.SelectedIndex = -1
		Me.PackOptionsPanel.SelectedValue = Nothing
		Me.PackOptionsPanel.Size = New System.Drawing.Size(764, 153)
		Me.PackOptionsPanel.TabIndex = 0
		'
		'MultiFileVpkCheckBox
		'
		Me.MultiFileVpkCheckBox.AutoSize = True
		Me.MultiFileVpkCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.MultiFileVpkCheckBox.IsReadOnly = False
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
		Me.LogFileCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.LogFileCheckBox.IsReadOnly = False
		Me.LogFileCheckBox.Location = New System.Drawing.Point(6, 28)
		Me.LogFileCheckBox.Name = "LogFileCheckBox"
		Me.LogFileCheckBox.Size = New System.Drawing.Size(116, 17)
		Me.LogFileCheckBox.TabIndex = 4
		Me.LogFileCheckBox.Text = "Write log to a file"
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'GmaPanel
		'
		Me.GmaPanel.Controls.Add(Me.GmaTitleTextBox)
		Me.GmaPanel.Controls.Add(Me.GmaTitleLabel)
		Me.GmaPanel.Controls.Add(Me.GmaGarrysModTagsUserControl)
		Me.GmaPanel.Location = New System.Drawing.Point(217, 29)
		Me.GmaPanel.Name = "GmaPanel"
		Me.GmaPanel.SelectedIndex = -1
		Me.GmaPanel.SelectedValue = Nothing
		Me.GmaPanel.Size = New System.Drawing.Size(423, 122)
		Me.GmaPanel.TabIndex = 0
		'
		'GmaTitleTextBox
		'
		Me.GmaTitleTextBox.CueBannerText = ""
		Me.GmaTitleTextBox.DetectUrls = False
		Me.GmaTitleTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GmaTitleTextBox.Location = New System.Drawing.Point(42, 1)
		Me.GmaTitleTextBox.Multiline = False
		Me.GmaTitleTextBox.Name = "GmaTitleTextBox"
		Me.GmaTitleTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.GmaTitleTextBox.Size = New System.Drawing.Size(317, 22)
		Me.GmaTitleTextBox.TabIndex = 14
		Me.GmaTitleTextBox.Text = ""
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
		'GameSetupPanel
		'
		Me.GameSetupPanel.Controls.Add(Me.GameSetupLabel)
		Me.GameSetupPanel.Controls.Add(Me.GameSetupComboUserControl)
		Me.GameSetupPanel.Controls.Add(Me.SetUpGamesButton)
		Me.GameSetupPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.GameSetupPanel.Location = New System.Drawing.Point(0, 0)
		Me.GameSetupPanel.Name = "GameSetupPanel"
		Me.GameSetupPanel.SelectedIndex = -1
		Me.GameSetupPanel.SelectedValue = Nothing
		Me.GameSetupPanel.Size = New System.Drawing.Size(764, 26)
		Me.GameSetupPanel.TabIndex = 14
		'
		'GameSetupLabel
		'
		Me.GameSetupLabel.AutoSize = True
		Me.GameSetupLabel.Location = New System.Drawing.Point(0, 5)
		Me.GameSetupLabel.Name = "GameSetupLabel"
		Me.GameSetupLabel.Size = New System.Drawing.Size(165, 13)
		Me.GameSetupLabel.TabIndex = 0
		Me.GameSetupLabel.Text = "Game that has the packer tool:"
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboUserControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboUserControl.IsReadOnly = False
		Me.GameSetupComboUserControl.Location = New System.Drawing.Point(171, 1)
		Me.GameSetupComboUserControl.Name = "GameSetupComboBox"
		Me.GameSetupComboUserControl.Size = New System.Drawing.Size(497, 23)
		Me.GameSetupComboUserControl.TabIndex = 1
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
		'DirectPackOptionsLabel
		'
		Me.DirectPackOptionsLabel.Location = New System.Drawing.Point(0, 154)
		Me.DirectPackOptionsLabel.Name = "DirectPackOptionsLabel"
		Me.DirectPackOptionsLabel.Size = New System.Drawing.Size(761, 13)
		Me.DirectPackOptionsLabel.TabIndex = 16
		Me.DirectPackOptionsLabel.Text = "Direct entry of command-line options (in case they are not included above):"
		'
		'DirectPackOptionsPanel
		'
		Me.DirectPackOptionsPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DirectPackOptionsPanel.Controls.Add(Me.DirectPackOptionsTextBox)
		Me.DirectPackOptionsPanel.Location = New System.Drawing.Point(0, 170)
		Me.DirectPackOptionsPanel.Name = "DirectPackOptionsPanel"
		Me.DirectPackOptionsPanel.SelectedIndex = -1
		Me.DirectPackOptionsPanel.SelectedValue = Nothing
		Me.DirectPackOptionsPanel.Size = New System.Drawing.Size(761, 22)
		Me.DirectPackOptionsPanel.TabIndex = 43
		'
		'DirectPackOptionsTextBox
		'
		Me.DirectPackOptionsTextBox.CueBannerText = ""
		Me.DirectPackOptionsTextBox.DetectUrls = False
		Me.DirectPackOptionsTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DirectPackOptionsTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.DirectPackOptionsTextBox.Location = New System.Drawing.Point(0, 0)
		Me.DirectPackOptionsTextBox.Multiline = False
		Me.DirectPackOptionsTextBox.Name = "DirectPackOptionsTextBox"
		Me.DirectPackOptionsTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.DirectPackOptionsTextBox.Size = New System.Drawing.Size(761, 22)
		Me.DirectPackOptionsTextBox.TabIndex = 17
		Me.DirectPackOptionsTextBox.Text = ""
		Me.DirectPackOptionsTextBox.WordWrap = False
		'
		'PackOptionsTextPanel
		'
		Me.PackOptionsTextPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackOptionsTextPanel.Controls.Add(Me.PackOptionsTextBox)
		Me.PackOptionsTextPanel.Location = New System.Drawing.Point(0, 198)
		Me.PackOptionsTextPanel.Name = "PackOptionsTextPanel"
		Me.PackOptionsTextPanel.SelectedIndex = -1
		Me.PackOptionsTextPanel.SelectedValue = Nothing
		Me.PackOptionsTextPanel.Size = New System.Drawing.Size(761, 37)
		Me.PackOptionsTextPanel.TabIndex = 8
		'
		'PackOptionsTextBox
		'
		Me.PackOptionsTextBox.CueBannerText = ""
		Me.PackOptionsTextBox.DetectUrls = False
		Me.PackOptionsTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PackOptionsTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.PackOptionsTextBox.Location = New System.Drawing.Point(0, 0)
		Me.PackOptionsTextBox.Name = "PackOptionsTextBox"
		Me.PackOptionsTextBox.ReadOnly = True
		Me.PackOptionsTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.PackOptionsTextBox.Size = New System.Drawing.Size(761, 37)
		Me.PackOptionsTextBox.TabIndex = 18
		Me.PackOptionsTextBox.Text = ""
		'
		'PackOptionsTextBoxMinScrollPanel
		'
		Me.PackOptionsTextBoxMinScrollPanel.Location = New System.Drawing.Point(0, 198)
		Me.PackOptionsTextBoxMinScrollPanel.Name = "PackOptionsTextBoxMinScrollPanel"
		Me.PackOptionsTextBoxMinScrollPanel.SelectedIndex = -1
		Me.PackOptionsTextBoxMinScrollPanel.SelectedValue = Nothing
		Me.PackOptionsTextBoxMinScrollPanel.Size = New System.Drawing.Size(761, 37)
		Me.PackOptionsTextBoxMinScrollPanel.TabIndex = 42
		'
		'LogRichTextBox
		'
		Me.LogRichTextBox.CueBannerText = ""
		Me.LogRichTextBox.DetectUrls = False
		Me.LogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LogRichTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.LogRichTextBox.HideSelection = False
		Me.LogRichTextBox.Location = New System.Drawing.Point(0, 26)
		Me.LogRichTextBox.Name = "LogRichTextBox"
		Me.LogRichTextBox.ReadOnly = True
		Me.LogRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
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
		Me.PackButtonsPanel.SelectedIndex = -1
		Me.PackButtonsPanel.SelectedValue = Nothing
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
		Me.PostPackPanel.Controls.Add(Me.PackedFilesComboUserControl)
		Me.PostPackPanel.Controls.Add(Me.UseInPublishButton)
		Me.PostPackPanel.Controls.Add(Me.GotoPackedFileButton)
		Me.PostPackPanel.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PostPackPanel.Location = New System.Drawing.Point(0, 182)
		Me.PostPackPanel.Name = "PostPackPanel"
		Me.PostPackPanel.SelectedIndex = -1
		Me.PostPackPanel.SelectedValue = Nothing
		Me.PostPackPanel.Size = New System.Drawing.Size(770, 26)
		Me.PostPackPanel.TabIndex = 6
		'
		'PackedFilesComboBox
		'
		Me.PackedFilesComboUserControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackedFilesComboUserControl.IsReadOnly = False
		Me.PackedFilesComboUserControl.Location = New System.Drawing.Point(0, 4)
		Me.PackedFilesComboUserControl.Name = "PackedFilesComboBox"
		Me.PackedFilesComboUserControl.Size = New System.Drawing.Size(721, 23)
		Me.PackedFilesComboUserControl.TabIndex = 1
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
		Me.PackOptionsPanel.ResumeLayout(False)
		Me.PackOptionsPanel.PerformLayout()
		Me.GmaPanel.ResumeLayout(False)
		Me.GmaPanel.PerformLayout()
		Me.GameSetupPanel.ResumeLayout(False)
		Me.GameSetupPanel.PerformLayout()
		Me.DirectPackOptionsPanel.ResumeLayout(False)
		Me.PackOptionsTextPanel.ResumeLayout(False)
		Me.PackButtonsPanel.ResumeLayout(False)
		Me.PostPackPanel.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents Panel1 As PanelEx
	Friend WithEvents OutputParentPathTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents GotoOutputPathButton As ButtonEx
	Friend WithEvents BrowseForOutputPathButton As ButtonEx
	Friend WithEvents OutputPathTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents OutputPathComboUserControl As ComboUserControl
	Friend WithEvents InputComboUserControl As ComboUserControl
	Friend WithEvents Label1 As Crowbar.LabelEx
	Friend WithEvents GotoInputPathButton As ButtonEx
	Friend WithEvents Label6 As Crowbar.LabelEx
	Friend WithEvents InputPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents BrowseForInputFolderOrFileNameButton As ButtonEx
	Friend WithEvents Options_LogSplitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents UseAllInPublishButton As ButtonEx
	Friend WithEvents OptionsGroupBox As GroupBoxEx
	Friend WithEvents PackOptionsPanel As PanelEx
	Friend WithEvents PackOptionsUseDefaultsButton As ButtonEx
	Friend WithEvents LogFileCheckBox As CheckBoxEx
	Friend WithEvents GameSetupLabel As Crowbar.LabelEx
	Friend WithEvents GameSetupComboUserControl As ComboUserControl
	Friend WithEvents SetUpGamesButton As ButtonEx
	Friend WithEvents CancelPackButton As ButtonEx
	Friend WithEvents SkipCurrentFolderButton As ButtonEx
	Friend WithEvents PackButton As ButtonEx
	Friend WithEvents UseInPublishButton As ButtonEx
	Friend WithEvents LogRichTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents PackedFilesComboUserControl As ComboUserControl
	Friend WithEvents GotoPackedFileButton As ButtonEx
	Friend WithEvents DirectPackOptionsLabel As LabelEx
	Friend WithEvents DirectPackOptionsTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents PackOptionsTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents MultiFileVpkCheckBox As CheckBoxEx
	Friend WithEvents GmaTitleLabel As LabelEx
	Friend WithEvents GmaTitleTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents GmaGarrysModTagsUserControl As GarrysModTagsUserControl
	Friend WithEvents PackButtonsPanel As PanelEx
	Friend WithEvents PostPackPanel As PanelEx
	Friend WithEvents OptionsGroupBoxFillPanel As PanelEx
	Friend WithEvents PackOptionsTextBoxMinScrollPanel As PanelEx
	Friend WithEvents GmaPanel As PanelEx
	Friend WithEvents DirectPackOptionsPanel As PanelEx
	Friend WithEvents PackOptionsTextPanel As PanelEx
	Friend WithEvents GameSetupPanel As PanelEx
End Class
