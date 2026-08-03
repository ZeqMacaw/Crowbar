<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CompileUserControl
	Inherits BaseUserControl

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.CompilerOptionsTextBox = New Crowbar.RichTextBoxEx()
        Me.GameSetupComboUserControl = New Crowbar.ComboUserControl()
        Me.FolderForEachModelCheckBox = New Crowbar.CheckBoxEx()
        Me.SourceEngineLogFileCheckBox = New Crowbar.CheckBoxEx()
        Me.CompilerOptionDefineBonesCheckBox = New Crowbar.CheckBoxEx()
        Me.CompilerOptionNoP4CheckBox = New Crowbar.CheckBoxEx()
        Me.CompilerOptionVerboseCheckBox = New Crowbar.CheckBoxEx()
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox = New Crowbar.CheckBoxEx()
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox = New Crowbar.CheckBoxEx()
        Me.CompilerOptionDefineBonesFileNameTextBox = New Crowbar.RichTextBoxEx()
        Me.DirectCompilerOptionsLabel = New Crowbar.LabelEx()
        Me.DirectCompilerOptionsTextBox = New Crowbar.RichTextBoxEx()
        Me.BrowseForQcPathFolderOrFileNameButton = New Crowbar.ButtonEx()
        Me.Label6 = New Crowbar.LabelEx()
        Me.SetUpGamesButton = New Crowbar.ButtonEx()
        Me.GameSetupLabel = New Crowbar.LabelEx()
        Me.CompileButton = New Crowbar.ButtonEx()
        Me.Panel1 = New Crowbar.PanelEx()
        Me.QcPathFileNameTextBox = New Crowbar.RichTextBoxEx()
        Me.OutputPathTextBox = New Crowbar.RichTextBoxEx()
        Me.GameModelsOutputPathTextBox = New Crowbar.RichTextBoxEx()
        Me.OutputSubfolderTextBox = New Crowbar.RichTextBoxEx()
        Me.GotoOutputPathButton = New Crowbar.ButtonEx()
        Me.BrowseForOutputPathButton = New Crowbar.ButtonEx()
        Me.OutputPathComboUserControl = New Crowbar.ComboUserControl()
        Me.CompileComboUserControl = New Crowbar.ComboUserControl()
        Me.Label1 = New Crowbar.LabelEx()
        Me.GotoQcButton = New Crowbar.ButtonEx()
        Me.Options_LogSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.OptionsGroupBox = New Crowbar.GroupBoxEx()
        Me.OptionsGroupBoxFillPanel = New Crowbar.PanelEx()
        Me.CompilerOptionsSourceEnginePanel = New Crowbar.PanelEx()
        Me.DefineBonesGroupBox = New Crowbar.GroupBoxEx()
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox = New Crowbar.CheckBoxEx()
        Me.CompileOptionsSourceEngineUseDefaultsButton = New Crowbar.ButtonEx()
        Me.CompilerOptionsGoldSourceEnginePanel = New Crowbar.PanelEx()
        Me.GoldSourceEngineLogFileCheckBox = New Crowbar.CheckBoxEx()
        Me.CompileOptionsGoldSourceEngineUseDefaultsButton = New Crowbar.ButtonEx()
        Me.GameSetupPanel = New Crowbar.PanelEx()
        Me.PanelEx1 = New Crowbar.PanelEx()
        Me.PanelEx2 = New Crowbar.PanelEx()
        Me.CompilerOptionsTextBoxMinScrollPanel = New Crowbar.PanelEx()
        Me.CompileLogRichTextBox = New Crowbar.RichTextBoxEx()
        Me.CompileButtonsPanel = New Crowbar.PanelEx()
        Me.SkipCurrentModelButton = New Crowbar.ButtonEx()
        Me.CancelCompileButton = New Crowbar.ButtonEx()
        Me.UseAllInPackButton = New Crowbar.ButtonEx()
        Me.PostCompilePanel = New Crowbar.PanelEx()
        Me.CompiledFilesComboUserControl = New Crowbar.ComboUserControl()
        Me.UseInViewButton = New Crowbar.ButtonEx()
        Me.RecompileButton = New Crowbar.ButtonEx()
        Me.UseInPackButton = New Crowbar.ButtonEx()
        Me.GotoCompiledMdlButton = New Crowbar.ButtonEx()
        Me.UseDefaultOutputSubfolderButton = New Crowbar.ButtonEx()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Options_LogSplitContainer.Panel1.SuspendLayout()
        Me.Options_LogSplitContainer.Panel2.SuspendLayout()
        Me.Options_LogSplitContainer.SuspendLayout()
        Me.OptionsGroupBox.SuspendLayout()
        Me.OptionsGroupBoxFillPanel.SuspendLayout()
        Me.CompilerOptionsSourceEnginePanel.SuspendLayout()
        Me.DefineBonesGroupBox.SuspendLayout()
        Me.CompilerOptionsGoldSourceEnginePanel.SuspendLayout()
        Me.GameSetupPanel.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        Me.CompileButtonsPanel.SuspendLayout()
        Me.PostCompilePanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'CompilerOptionsTextBox
        '
        Me.CompilerOptionsTextBox.CueBannerText = ""
        Me.CompilerOptionsTextBox.DetectUrls = False
        Me.CompilerOptionsTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CompilerOptionsTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.CompilerOptionsTextBox.Location = New System.Drawing.Point(0, 0)
        Me.CompilerOptionsTextBox.MinimumSize = New System.Drawing.Size(0, 37)
        Me.CompilerOptionsTextBox.Name = "CompilerOptionsTextBox"
        Me.CompilerOptionsTextBox.ReadOnly = True
        Me.CompilerOptionsTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.CompilerOptionsTextBox.Size = New System.Drawing.Size(749, 105)
        Me.CompilerOptionsTextBox.TabIndex = 15
        Me.CompilerOptionsTextBox.Text = ""
        '
        'GameSetupComboBox
        '
        Me.GameSetupComboUserControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GameSetupComboUserControl.IsReadOnly = False
        Me.GameSetupComboUserControl.Location = New System.Drawing.Point(192, 1)
        Me.GameSetupComboUserControl.Name = "GameSetupComboBox"
        Me.GameSetupComboUserControl.Size = New System.Drawing.Size(473, 23)
        Me.GameSetupComboUserControl.TabIndex = 1
        '
        'FolderForEachModelCheckBox
        '
        Me.FolderForEachModelCheckBox.AutoSize = True
        Me.FolderForEachModelCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.FolderForEachModelCheckBox.IsReadOnly = False
        Me.FolderForEachModelCheckBox.Location = New System.Drawing.Point(502, 74)
        Me.FolderForEachModelCheckBox.Name = "FolderForEachModelCheckBox"
        Me.FolderForEachModelCheckBox.Size = New System.Drawing.Size(139, 17)
        Me.FolderForEachModelCheckBox.TabIndex = 3
        Me.FolderForEachModelCheckBox.Text = "Folder for each model"
        Me.FolderForEachModelCheckBox.UseVisualStyleBackColor = True
        Me.FolderForEachModelCheckBox.Visible = False
        '
        'SourceEngineLogFileCheckBox
        '
        Me.SourceEngineLogFileCheckBox.AutoSize = True
        Me.SourceEngineLogFileCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.SourceEngineLogFileCheckBox.IsReadOnly = False
        Me.SourceEngineLogFileCheckBox.Location = New System.Drawing.Point(6, 3)
        Me.SourceEngineLogFileCheckBox.Name = "SourceEngineLogFileCheckBox"
        Me.SourceEngineLogFileCheckBox.Size = New System.Drawing.Size(116, 17)
        Me.SourceEngineLogFileCheckBox.TabIndex = 4
        Me.SourceEngineLogFileCheckBox.Text = "Write log to a file"
        Me.ToolTip1.SetToolTip(Me.SourceEngineLogFileCheckBox, "Write compile log to a file (in same folder as QC file).")
        Me.SourceEngineLogFileCheckBox.UseVisualStyleBackColor = True
        '
        'CompilerOptionDefineBonesCheckBox
        '
        Me.CompilerOptionDefineBonesCheckBox.AutoSize = True
        Me.CompilerOptionDefineBonesCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.CompilerOptionDefineBonesCheckBox.IsReadOnly = False
        Me.CompilerOptionDefineBonesCheckBox.Location = New System.Drawing.Point(179, 4)
        Me.CompilerOptionDefineBonesCheckBox.Name = "CompilerOptionDefineBonesCheckBox"
        Me.CompilerOptionDefineBonesCheckBox.Size = New System.Drawing.Size(92, 17)
        Me.CompilerOptionDefineBonesCheckBox.TabIndex = 7
        Me.CompilerOptionDefineBonesCheckBox.Text = "DefineBones"
        Me.CompilerOptionDefineBonesCheckBox.UseVisualStyleBackColor = True
        '
        'CompilerOptionNoP4CheckBox
        '
        Me.CompilerOptionNoP4CheckBox.AutoSize = True
        Me.CompilerOptionNoP4CheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.CompilerOptionNoP4CheckBox.IsReadOnly = False
        Me.CompilerOptionNoP4CheckBox.Location = New System.Drawing.Point(6, 26)
        Me.CompilerOptionNoP4CheckBox.Name = "CompilerOptionNoP4CheckBox"
        Me.CompilerOptionNoP4CheckBox.Size = New System.Drawing.Size(56, 17)
        Me.CompilerOptionNoP4CheckBox.TabIndex = 5
        Me.CompilerOptionNoP4CheckBox.Text = "No P4"
        Me.ToolTip1.SetToolTip(Me.CompilerOptionNoP4CheckBox, "No Perforce integration (modders do not usually have Perforce software).")
        Me.CompilerOptionNoP4CheckBox.UseVisualStyleBackColor = True
        '
        'CompilerOptionVerboseCheckBox
        '
        Me.CompilerOptionVerboseCheckBox.AutoSize = True
        Me.CompilerOptionVerboseCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.CompilerOptionVerboseCheckBox.IsReadOnly = False
        Me.CompilerOptionVerboseCheckBox.Location = New System.Drawing.Point(6, 49)
        Me.CompilerOptionVerboseCheckBox.Name = "CompilerOptionVerboseCheckBox"
        Me.CompilerOptionVerboseCheckBox.Size = New System.Drawing.Size(67, 17)
        Me.CompilerOptionVerboseCheckBox.TabIndex = 6
        Me.CompilerOptionVerboseCheckBox.Text = "Verbose"
        Me.ToolTip1.SetToolTip(Me.CompilerOptionVerboseCheckBox, "Write more info in compile log.")
        Me.CompilerOptionVerboseCheckBox.UseVisualStyleBackColor = True
        '
        'CompilerOptionDefineBonesModifyQcFileCheckBox
        '
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox.AutoSize = True
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Enabled = False
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox.IsReadOnly = False
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Location = New System.Drawing.Point(19, 65)
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Name = "CompilerOptionDefineBonesModifyQcFileCheckBox"
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Size = New System.Drawing.Size(238, 17)
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox.TabIndex = 11
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Text = "Put in QC file: $include ""<QCI file name>"""
        Me.CompilerOptionDefineBonesModifyQcFileCheckBox.UseVisualStyleBackColor = True
        '
        'CompilerOptionDefineBonesWriteQciFileCheckBox
        '
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox.AutoSize = True
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox.Enabled = False
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox.IsReadOnly = False
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox.Location = New System.Drawing.Point(6, 22)
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox.Name = "CompilerOptionDefineBonesWriteQciFileCheckBox"
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox.Size = New System.Drawing.Size(97, 17)
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox.TabIndex = 8
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox.Text = "Write QCI file:"
        Me.CompilerOptionDefineBonesWriteQciFileCheckBox.UseVisualStyleBackColor = True
        '
        'CompilerOptionDefineBonesFileNameTextBox
        '
        Me.CompilerOptionDefineBonesFileNameTextBox.CueBannerText = ""
        Me.CompilerOptionDefineBonesFileNameTextBox.DetectUrls = False
        Me.CompilerOptionDefineBonesFileNameTextBox.Enabled = False
        Me.CompilerOptionDefineBonesFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.CompilerOptionDefineBonesFileNameTextBox.Location = New System.Drawing.Point(109, 18)
        Me.CompilerOptionDefineBonesFileNameTextBox.Multiline = False
        Me.CompilerOptionDefineBonesFileNameTextBox.Name = "CompilerOptionDefineBonesFileNameTextBox"
        Me.CompilerOptionDefineBonesFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.CompilerOptionDefineBonesFileNameTextBox.Size = New System.Drawing.Size(140, 22)
        Me.CompilerOptionDefineBonesFileNameTextBox.TabIndex = 10
        Me.CompilerOptionDefineBonesFileNameTextBox.Text = ""
        Me.CompilerOptionDefineBonesFileNameTextBox.WordWrap = False
        '
        'DirectCompilerOptionsLabel
        '
        Me.DirectCompilerOptionsLabel.Location = New System.Drawing.Point(0, 125)
        Me.DirectCompilerOptionsLabel.Name = "DirectCompilerOptionsLabel"
        Me.DirectCompilerOptionsLabel.Size = New System.Drawing.Size(761, 13)
        Me.DirectCompilerOptionsLabel.TabIndex = 13
        Me.DirectCompilerOptionsLabel.Text = "Direct entry of command-line options (in case they are not included above):"
        '
        'DirectCompilerOptionsTextBox
        '
        Me.DirectCompilerOptionsTextBox.CueBannerText = ""
        Me.DirectCompilerOptionsTextBox.DetectUrls = False
        Me.DirectCompilerOptionsTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DirectCompilerOptionsTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.DirectCompilerOptionsTextBox.Location = New System.Drawing.Point(0, 0)
        Me.DirectCompilerOptionsTextBox.Multiline = False
        Me.DirectCompilerOptionsTextBox.Name = "DirectCompilerOptionsTextBox"
        Me.DirectCompilerOptionsTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.DirectCompilerOptionsTextBox.Size = New System.Drawing.Size(749, 22)
        Me.DirectCompilerOptionsTextBox.TabIndex = 14
        Me.DirectCompilerOptionsTextBox.Text = ""
        Me.DirectCompilerOptionsTextBox.WordWrap = False
        '
        'BrowseForQcPathFolderOrFileNameButton
        '
        Me.BrowseForQcPathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BrowseForQcPathFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
        Me.BrowseForQcPathFolderOrFileNameButton.Name = "BrowseForQcPathFolderOrFileNameButton"
        Me.BrowseForQcPathFolderOrFileNameButton.Size = New System.Drawing.Size(64, 23)
        Me.BrowseForQcPathFolderOrFileNameButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.BrowseForQcPathFolderOrFileNameButton.TabIndex = 3
        Me.BrowseForQcPathFolderOrFileNameButton.Text = "Browse..."
        Me.BrowseForQcPathFolderOrFileNameButton.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(3, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "QC input:"
        '
        'SetUpGamesButton
        '
        Me.SetUpGamesButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SetUpGamesButton.Location = New System.Drawing.Point(671, 0)
        Me.SetUpGamesButton.Name = "SetUpGamesButton"
        Me.SetUpGamesButton.Size = New System.Drawing.Size(90, 23)
        Me.SetUpGamesButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.SetUpGamesButton.TabIndex = 2
        Me.SetUpGamesButton.Text = "Set Up Games"
        Me.SetUpGamesButton.UseVisualStyleBackColor = True
        '
        'GameSetupLabel
        '
        Me.GameSetupLabel.Location = New System.Drawing.Point(0, 5)
        Me.GameSetupLabel.Name = "GameSetupLabel"
        Me.GameSetupLabel.Size = New System.Drawing.Size(186, 13)
        Me.GameSetupLabel.TabIndex = 0
        Me.GameSetupLabel.Text = "Game that has the model compiler:"
        '
        'CompileButton
        '
        Me.CompileButton.Location = New System.Drawing.Point(0, 0)
        Me.CompileButton.Name = "CompileButton"
        Me.CompileButton.Size = New System.Drawing.Size(125, 23)
        Me.CompileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.CompileButton.TabIndex = 1
        Me.CompileButton.Text = "&Compile DefineBones"
        Me.CompileButton.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.QcPathFileNameTextBox)
        Me.Panel1.Controls.Add(Me.OutputPathTextBox)
        Me.Panel1.Controls.Add(Me.GameModelsOutputPathTextBox)
        Me.Panel1.Controls.Add(Me.OutputSubfolderTextBox)
        Me.Panel1.Controls.Add(Me.GotoOutputPathButton)
        Me.Panel1.Controls.Add(Me.BrowseForOutputPathButton)
        Me.Panel1.Controls.Add(Me.OutputPathComboUserControl)
        Me.Panel1.Controls.Add(Me.CompileComboUserControl)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.GotoQcButton)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.BrowseForQcPathFolderOrFileNameButton)
        Me.Panel1.Controls.Add(Me.Options_LogSplitContainer)
        Me.Panel1.Controls.Add(Me.UseDefaultOutputSubfolderButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.SelectedIndex = -1
        Me.Panel1.SelectedValue = Nothing
        Me.Panel1.Size = New System.Drawing.Size(776, 536)
        Me.Panel1.TabIndex = 15
        '
        'QcPathFileNameTextBox
        '
        Me.QcPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QcPathFileNameTextBox.CueBannerText = ""
        Me.QcPathFileNameTextBox.DetectUrls = False
        Me.QcPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.QcPathFileNameTextBox.Location = New System.Drawing.Point(209, 4)
        Me.QcPathFileNameTextBox.Multiline = False
        Me.QcPathFileNameTextBox.Name = "QcPathFileNameTextBox"
        Me.QcPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.QcPathFileNameTextBox.Size = New System.Drawing.Size(445, 22)
        Me.QcPathFileNameTextBox.TabIndex = 22
        Me.QcPathFileNameTextBox.Text = ""
        Me.QcPathFileNameTextBox.WordWrap = False
        '
        'OutputPathTextBox
        '
        Me.OutputPathTextBox.AllowDrop = True
        Me.OutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OutputPathTextBox.CueBannerText = ""
        Me.OutputPathTextBox.DetectUrls = False
        Me.OutputPathTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.OutputPathTextBox.Location = New System.Drawing.Point(209, 33)
        Me.OutputPathTextBox.Multiline = False
        Me.OutputPathTextBox.Name = "OutputPathTextBox"
        Me.OutputPathTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.OutputPathTextBox.Size = New System.Drawing.Size(445, 22)
        Me.OutputPathTextBox.TabIndex = 9
        Me.OutputPathTextBox.Text = ""
        Me.OutputPathTextBox.WordWrap = False
        '
        'GameModelsOutputPathTextBox
        '
        Me.GameModelsOutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GameModelsOutputPathTextBox.CueBannerText = ""
        Me.GameModelsOutputPathTextBox.DetectUrls = False
        Me.GameModelsOutputPathTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.GameModelsOutputPathTextBox.Location = New System.Drawing.Point(209, 32)
        Me.GameModelsOutputPathTextBox.Multiline = False
        Me.GameModelsOutputPathTextBox.Name = "GameModelsOutputPathTextBox"
        Me.GameModelsOutputPathTextBox.ReadOnly = True
        Me.GameModelsOutputPathTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.GameModelsOutputPathTextBox.Size = New System.Drawing.Size(445, 22)
        Me.GameModelsOutputPathTextBox.TabIndex = 8
        Me.GameModelsOutputPathTextBox.Text = ""
        Me.GameModelsOutputPathTextBox.WordWrap = False
        '
        'OutputSubfolderTextBox
        '
        Me.OutputSubfolderTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OutputSubfolderTextBox.CueBannerText = ""
        Me.OutputSubfolderTextBox.DetectUrls = False
        Me.OutputSubfolderTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.OutputSubfolderTextBox.Location = New System.Drawing.Point(209, 32)
        Me.OutputSubfolderTextBox.Multiline = False
        Me.OutputSubfolderTextBox.Name = "OutputSubfolderTextBox"
        Me.OutputSubfolderTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.OutputSubfolderTextBox.Size = New System.Drawing.Size(445, 22)
        Me.OutputSubfolderTextBox.TabIndex = 21
        Me.OutputSubfolderTextBox.Text = ""
        Me.OutputSubfolderTextBox.Visible = False
        Me.OutputSubfolderTextBox.WordWrap = False
        '
        'GotoOutputPathButton
        '
        Me.GotoOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GotoOutputPathButton.Location = New System.Drawing.Point(730, 32)
        Me.GotoOutputPathButton.Name = "GotoOutputPathButton"
        Me.GotoOutputPathButton.Size = New System.Drawing.Size(43, 23)
        Me.GotoOutputPathButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.GotoOutputPathButton.TabIndex = 11
        Me.GotoOutputPathButton.Text = "Goto"
        Me.GotoOutputPathButton.UseVisualStyleBackColor = True
        '
        'BrowseForOutputPathButton
        '
        Me.BrowseForOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BrowseForOutputPathButton.Enabled = False
        Me.BrowseForOutputPathButton.Location = New System.Drawing.Point(660, 32)
        Me.BrowseForOutputPathButton.Name = "BrowseForOutputPathButton"
        Me.BrowseForOutputPathButton.Size = New System.Drawing.Size(64, 23)
        Me.BrowseForOutputPathButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.BrowseForOutputPathButton.TabIndex = 10
        Me.BrowseForOutputPathButton.Text = "Browse..."
        Me.BrowseForOutputPathButton.UseVisualStyleBackColor = True
        '
        'OutputPathComboBox
        '
        Me.OutputPathComboUserControl.IsReadOnly = False
        Me.OutputPathComboUserControl.Location = New System.Drawing.Point(63, 33)
        Me.OutputPathComboUserControl.Name = "OutputPathComboBox"
        Me.OutputPathComboUserControl.Size = New System.Drawing.Size(140, 23)
        Me.OutputPathComboUserControl.TabIndex = 6
        '
        'CompileComboBox
        '
        Me.CompileComboUserControl.IsReadOnly = False
        Me.CompileComboUserControl.Location = New System.Drawing.Point(63, 4)
        Me.CompileComboUserControl.Name = "CompileComboBox"
        Me.CompileComboUserControl.Size = New System.Drawing.Size(140, 23)
        Me.CompileComboUserControl.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(3, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Output to:"
        '
        'GotoQcButton
        '
        Me.GotoQcButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GotoQcButton.Location = New System.Drawing.Point(730, 3)
        Me.GotoQcButton.Name = "GotoQcButton"
        Me.GotoQcButton.Size = New System.Drawing.Size(43, 23)
        Me.GotoQcButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.GotoQcButton.TabIndex = 4
        Me.GotoQcButton.Text = "Goto"
        Me.GotoQcButton.UseVisualStyleBackColor = True
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
        Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.CompileLogRichTextBox)
        Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.CompileButtonsPanel)
        Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.PostCompilePanel)
        Me.Options_LogSplitContainer.Panel2MinSize = 45
        Me.Options_LogSplitContainer.Size = New System.Drawing.Size(770, 472)
        Me.Options_LogSplitContainer.SplitterDistance = 230
        Me.Options_LogSplitContainer.TabIndex = 16
        '
        'OptionsGroupBox
        '
        Me.OptionsGroupBox.Controls.Add(Me.OptionsGroupBoxFillPanel)
        Me.OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OptionsGroupBox.IsReadOnly = False
        Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
        Me.OptionsGroupBox.Name = "OptionsGroupBox"
        Me.OptionsGroupBox.SelectedValue = Nothing
        Me.OptionsGroupBox.Size = New System.Drawing.Size(770, 230)
        Me.OptionsGroupBox.TabIndex = 0
        Me.OptionsGroupBox.TabStop = False
        Me.OptionsGroupBox.Text = "Options"
        '
        'OptionsGroupBoxFillPanel
        '
        Me.OptionsGroupBoxFillPanel.AutoScroll = True
        Me.OptionsGroupBoxFillPanel.Controls.Add(Me.CompilerOptionsSourceEnginePanel)
        Me.OptionsGroupBoxFillPanel.Controls.Add(Me.CompilerOptionsGoldSourceEnginePanel)
        Me.OptionsGroupBoxFillPanel.Controls.Add(Me.GameSetupPanel)
        Me.OptionsGroupBoxFillPanel.Controls.Add(Me.DirectCompilerOptionsLabel)
        Me.OptionsGroupBoxFillPanel.Controls.Add(Me.PanelEx1)
        Me.OptionsGroupBoxFillPanel.Controls.Add(Me.PanelEx2)
        Me.OptionsGroupBoxFillPanel.Controls.Add(Me.CompilerOptionsTextBoxMinScrollPanel)
        Me.OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OptionsGroupBoxFillPanel.Location = New System.Drawing.Point(3, 18)
        Me.OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel"
        Me.OptionsGroupBoxFillPanel.SelectedIndex = -1
        Me.OptionsGroupBoxFillPanel.SelectedValue = Nothing
        Me.OptionsGroupBoxFillPanel.Size = New System.Drawing.Size(764, 209)
        Me.OptionsGroupBoxFillPanel.TabIndex = 0
        '
        'CompilerOptionsSourceEnginePanel
        '
        Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionDefineBonesCheckBox)
        Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.DefineBonesGroupBox)
        Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.SourceEngineLogFileCheckBox)
        Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionVerboseCheckBox)
        Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionNoP4CheckBox)
        Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.FolderForEachModelCheckBox)
        Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompileOptionsSourceEngineUseDefaultsButton)
        Me.CompilerOptionsSourceEnginePanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.CompilerOptionsSourceEnginePanel.Location = New System.Drawing.Point(0, 126)
        Me.CompilerOptionsSourceEnginePanel.Name = "CompilerOptionsSourceEnginePanel"
        Me.CompilerOptionsSourceEnginePanel.SelectedIndex = -1
        Me.CompilerOptionsSourceEnginePanel.SelectedValue = Nothing
        Me.CompilerOptionsSourceEnginePanel.Size = New System.Drawing.Size(761, 100)
        Me.CompilerOptionsSourceEnginePanel.TabIndex = 38
        '
        'DefineBonesGroupBox
        '
        Me.DefineBonesGroupBox.Controls.Add(Me.CompilerOptionDefineBonesFileNameTextBox)
        Me.DefineBonesGroupBox.Controls.Add(Me.CompilerOptionDefineBonesModifyQcFileCheckBox)
        Me.DefineBonesGroupBox.Controls.Add(Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox)
        Me.DefineBonesGroupBox.Controls.Add(Me.CompilerOptionDefineBonesWriteQciFileCheckBox)
        Me.DefineBonesGroupBox.IsReadOnly = False
        Me.DefineBonesGroupBox.Location = New System.Drawing.Point(173, 3)
        Me.DefineBonesGroupBox.Name = "DefineBonesGroupBox"
        Me.DefineBonesGroupBox.SelectedValue = Nothing
        Me.DefineBonesGroupBox.Size = New System.Drawing.Size(259, 95)
        Me.DefineBonesGroupBox.TabIndex = 14
        Me.DefineBonesGroupBox.TabStop = False
        '
        'CompilerOptionDefineBonesOverwriteQciFileCheckBox
        '
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox.AutoSize = True
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Enabled = False
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox.IsReadOnly = False
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Location = New System.Drawing.Point(19, 45)
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Name = "CompilerOptionDefineBonesOverwriteQciFileCheckBox"
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Size = New System.Drawing.Size(116, 17)
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox.TabIndex = 13
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Text = "Overwrite QCI file"
        Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox.UseVisualStyleBackColor = True
        '
        'CompileOptionsSourceEngineUseDefaultsButton
        '
        Me.CompileOptionsSourceEngineUseDefaultsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CompileOptionsSourceEngineUseDefaultsButton.Location = New System.Drawing.Point(671, 64)
        Me.CompileOptionsSourceEngineUseDefaultsButton.Name = "CompileOptionsSourceEngineUseDefaultsButton"
        Me.CompileOptionsSourceEngineUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
        Me.CompileOptionsSourceEngineUseDefaultsButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.CompileOptionsSourceEngineUseDefaultsButton.TabIndex = 12
        Me.CompileOptionsSourceEngineUseDefaultsButton.Text = "Use Defaults"
        Me.ToolTip1.SetToolTip(Me.CompileOptionsSourceEngineUseDefaultsButton, "Set the compiler options back to default settings")
        Me.CompileOptionsSourceEngineUseDefaultsButton.UseVisualStyleBackColor = True
        '
        'CompilerOptionsGoldSourceEnginePanel
        '
        Me.CompilerOptionsGoldSourceEnginePanel.Controls.Add(Me.GoldSourceEngineLogFileCheckBox)
        Me.CompilerOptionsGoldSourceEnginePanel.Controls.Add(Me.CompileOptionsGoldSourceEngineUseDefaultsButton)
        Me.CompilerOptionsGoldSourceEnginePanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.CompilerOptionsGoldSourceEnginePanel.Location = New System.Drawing.Point(0, 26)
        Me.CompilerOptionsGoldSourceEnginePanel.Name = "CompilerOptionsGoldSourceEnginePanel"
        Me.CompilerOptionsGoldSourceEnginePanel.SelectedIndex = -1
        Me.CompilerOptionsGoldSourceEnginePanel.SelectedValue = Nothing
        Me.CompilerOptionsGoldSourceEnginePanel.Size = New System.Drawing.Size(761, 100)
        Me.CompilerOptionsGoldSourceEnginePanel.TabIndex = 13
        '
        'GoldSourceEngineLogFileCheckBox
        '
        Me.GoldSourceEngineLogFileCheckBox.AutoSize = True
        Me.GoldSourceEngineLogFileCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.GoldSourceEngineLogFileCheckBox.IsReadOnly = False
        Me.GoldSourceEngineLogFileCheckBox.Location = New System.Drawing.Point(6, 3)
        Me.GoldSourceEngineLogFileCheckBox.Name = "GoldSourceEngineLogFileCheckBox"
        Me.GoldSourceEngineLogFileCheckBox.Size = New System.Drawing.Size(116, 17)
        Me.GoldSourceEngineLogFileCheckBox.TabIndex = 14
        Me.GoldSourceEngineLogFileCheckBox.Text = "Write log to a file"
        Me.ToolTip1.SetToolTip(Me.GoldSourceEngineLogFileCheckBox, "Write compile log to a file (in same folder as QC file).")
        Me.GoldSourceEngineLogFileCheckBox.UseVisualStyleBackColor = True
        '
        'CompileOptionsGoldSourceEngineUseDefaultsButton
        '
        Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Location = New System.Drawing.Point(653, 68)
        Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Name = "CompileOptionsGoldSourceEngineUseDefaultsButton"
        Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
        Me.CompileOptionsGoldSourceEngineUseDefaultsButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.CompileOptionsGoldSourceEngineUseDefaultsButton.TabIndex = 13
        Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Text = "Use Defaults"
        Me.ToolTip1.SetToolTip(Me.CompileOptionsGoldSourceEngineUseDefaultsButton, "Set the compiler options back to default settings")
        Me.CompileOptionsGoldSourceEngineUseDefaultsButton.UseVisualStyleBackColor = True
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
        Me.GameSetupPanel.Size = New System.Drawing.Size(761, 26)
        Me.GameSetupPanel.TabIndex = 40
        '
        'PanelEx1
        '
        Me.PanelEx1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelEx1.Controls.Add(Me.DirectCompilerOptionsTextBox)
        Me.PanelEx1.Location = New System.Drawing.Point(0, 141)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.SelectedIndex = -1
        Me.PanelEx1.SelectedValue = Nothing
        Me.PanelEx1.Size = New System.Drawing.Size(749, 22)
        Me.PanelEx1.TabIndex = 42
        '
        'PanelEx2
        '
        Me.PanelEx2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelEx2.Controls.Add(Me.CompilerOptionsTextBox)
        Me.PanelEx2.Location = New System.Drawing.Point(0, 169)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.SelectedIndex = -1
        Me.PanelEx2.SelectedValue = Nothing
        Me.PanelEx2.Size = New System.Drawing.Size(749, 105)
        Me.PanelEx2.TabIndex = 15
        '
        'CompilerOptionsTextBoxMinScrollPanel
        '
        Me.CompilerOptionsTextBoxMinScrollPanel.AutoScroll = True
        Me.CompilerOptionsTextBoxMinScrollPanel.Location = New System.Drawing.Point(0, 169)
        Me.CompilerOptionsTextBoxMinScrollPanel.Name = "CompilerOptionsTextBoxMinScrollPanel"
        Me.CompilerOptionsTextBoxMinScrollPanel.SelectedIndex = -1
        Me.CompilerOptionsTextBoxMinScrollPanel.SelectedValue = Nothing
        Me.CompilerOptionsTextBoxMinScrollPanel.Size = New System.Drawing.Size(761, 37)
        Me.CompilerOptionsTextBoxMinScrollPanel.TabIndex = 41
        '
        'CompileLogRichTextBox
        '
        Me.CompileLogRichTextBox.CueBannerText = ""
        Me.CompileLogRichTextBox.DetectUrls = False
        Me.CompileLogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CompileLogRichTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.CompileLogRichTextBox.HideSelection = False
        Me.CompileLogRichTextBox.Location = New System.Drawing.Point(0, 26)
        Me.CompileLogRichTextBox.Name = "CompileLogRichTextBox"
        Me.CompileLogRichTextBox.ReadOnly = True
        Me.CompileLogRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.CompileLogRichTextBox.Size = New System.Drawing.Size(770, 186)
        Me.CompileLogRichTextBox.TabIndex = 0
        Me.CompileLogRichTextBox.Text = ""
        Me.CompileLogRichTextBox.WordWrap = False
        '
        'CompileButtonsPanel
        '
        Me.CompileButtonsPanel.Controls.Add(Me.CompileButton)
        Me.CompileButtonsPanel.Controls.Add(Me.SkipCurrentModelButton)
        Me.CompileButtonsPanel.Controls.Add(Me.CancelCompileButton)
        Me.CompileButtonsPanel.Controls.Add(Me.UseAllInPackButton)
        Me.CompileButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.CompileButtonsPanel.Location = New System.Drawing.Point(0, 0)
        Me.CompileButtonsPanel.Name = "CompileButtonsPanel"
        Me.CompileButtonsPanel.SelectedIndex = -1
        Me.CompileButtonsPanel.SelectedValue = Nothing
        Me.CompileButtonsPanel.Size = New System.Drawing.Size(770, 26)
        Me.CompileButtonsPanel.TabIndex = 39
        '
        'SkipCurrentModelButton
        '
        Me.SkipCurrentModelButton.Enabled = False
        Me.SkipCurrentModelButton.Location = New System.Drawing.Point(131, 0)
        Me.SkipCurrentModelButton.Name = "SkipCurrentModelButton"
        Me.SkipCurrentModelButton.Size = New System.Drawing.Size(120, 23)
        Me.SkipCurrentModelButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.SkipCurrentModelButton.TabIndex = 2
        Me.SkipCurrentModelButton.Text = "Skip Current Model"
        Me.SkipCurrentModelButton.UseVisualStyleBackColor = True
        '
        'CancelCompileButton
        '
        Me.CancelCompileButton.Enabled = False
        Me.CancelCompileButton.Location = New System.Drawing.Point(257, 0)
        Me.CancelCompileButton.Name = "CancelCompileButton"
        Me.CancelCompileButton.Size = New System.Drawing.Size(120, 23)
        Me.CancelCompileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.CancelCompileButton.TabIndex = 3
        Me.CancelCompileButton.Text = "Cancel Compile"
        Me.CancelCompileButton.UseVisualStyleBackColor = True
        '
        'UseAllInPackButton
        '
        Me.UseAllInPackButton.Enabled = False
        Me.UseAllInPackButton.Location = New System.Drawing.Point(383, 0)
        Me.UseAllInPackButton.Name = "UseAllInPackButton"
        Me.UseAllInPackButton.Size = New System.Drawing.Size(120, 23)
        Me.UseAllInPackButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.UseAllInPackButton.TabIndex = 4
        Me.UseAllInPackButton.Text = "Use All in Pack"
        Me.UseAllInPackButton.UseVisualStyleBackColor = True
        Me.UseAllInPackButton.Visible = False
        '
        'PostCompilePanel
        '
        Me.PostCompilePanel.Controls.Add(Me.CompiledFilesComboUserControl)
        Me.PostCompilePanel.Controls.Add(Me.UseInViewButton)
        Me.PostCompilePanel.Controls.Add(Me.RecompileButton)
        Me.PostCompilePanel.Controls.Add(Me.UseInPackButton)
        Me.PostCompilePanel.Controls.Add(Me.GotoCompiledMdlButton)
        Me.PostCompilePanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PostCompilePanel.Location = New System.Drawing.Point(0, 212)
        Me.PostCompilePanel.Name = "PostCompilePanel"
        Me.PostCompilePanel.SelectedIndex = -1
        Me.PostCompilePanel.SelectedValue = Nothing
        Me.PostCompilePanel.Size = New System.Drawing.Size(770, 26)
        Me.PostCompilePanel.TabIndex = 40
        '
        'CompiledFilesComboBox
        '
        Me.CompiledFilesComboUserControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CompiledFilesComboUserControl.IsReadOnly = False
        Me.CompiledFilesComboUserControl.Location = New System.Drawing.Point(0, 4)
        Me.CompiledFilesComboUserControl.Name = "CompiledFilesComboBox"
        Me.CompiledFilesComboUserControl.Size = New System.Drawing.Size(542, 23)
        Me.CompiledFilesComboUserControl.TabIndex = 1
        '
        'UseInViewButton
        '
        Me.UseInViewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UseInViewButton.Enabled = False
        Me.UseInViewButton.Location = New System.Drawing.Point(548, 3)
        Me.UseInViewButton.Name = "UseInViewButton"
        Me.UseInViewButton.Size = New System.Drawing.Size(75, 23)
        Me.UseInViewButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.UseInViewButton.TabIndex = 2
        Me.UseInViewButton.Text = "Use in View"
        Me.UseInViewButton.UseVisualStyleBackColor = True
        '
        'RecompileButton
        '
        Me.RecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RecompileButton.Enabled = False
        Me.RecompileButton.Location = New System.Drawing.Point(629, 3)
        Me.RecompileButton.Name = "RecompileButton"
        Me.RecompileButton.Size = New System.Drawing.Size(75, 23)
        Me.RecompileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.RecompileButton.TabIndex = 5
        Me.RecompileButton.Text = "Recompile"
        Me.RecompileButton.UseVisualStyleBackColor = True
        '
        'UseInPackButton
        '
        Me.UseInPackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UseInPackButton.Enabled = False
        Me.UseInPackButton.Location = New System.Drawing.Point(629, 3)
        Me.UseInPackButton.Name = "UseInPackButton"
        Me.UseInPackButton.Size = New System.Drawing.Size(75, 23)
        Me.UseInPackButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.UseInPackButton.TabIndex = 3
        Me.UseInPackButton.Text = "Use in Pack"
        Me.UseInPackButton.UseVisualStyleBackColor = True
        Me.UseInPackButton.Visible = False
        '
        'GotoCompiledMdlButton
        '
        Me.GotoCompiledMdlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GotoCompiledMdlButton.Location = New System.Drawing.Point(710, 3)
        Me.GotoCompiledMdlButton.Name = "GotoCompiledMdlButton"
        Me.GotoCompiledMdlButton.Size = New System.Drawing.Size(43, 23)
        Me.GotoCompiledMdlButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.GotoCompiledMdlButton.TabIndex = 4
        Me.GotoCompiledMdlButton.Text = "Goto"
        Me.GotoCompiledMdlButton.UseVisualStyleBackColor = True
        '
        'UseDefaultOutputSubfolderButton
        '
        Me.UseDefaultOutputSubfolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UseDefaultOutputSubfolderButton.Location = New System.Drawing.Point(660, 32)
        Me.UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton"
        Me.UseDefaultOutputSubfolderButton.Size = New System.Drawing.Size(113, 23)
        Me.UseDefaultOutputSubfolderButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
        Me.UseDefaultOutputSubfolderButton.TabIndex = 12
        Me.UseDefaultOutputSubfolderButton.Text = "Use Default"
        Me.UseDefaultOutputSubfolderButton.UseVisualStyleBackColor = True
        '
        'CompileUserControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "CompileUserControl"
        Me.Size = New System.Drawing.Size(776, 536)
        Me.Panel1.ResumeLayout(False)
        Me.Options_LogSplitContainer.Panel1.ResumeLayout(False)
        Me.Options_LogSplitContainer.Panel2.ResumeLayout(False)
        CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Options_LogSplitContainer.ResumeLayout(False)
        Me.OptionsGroupBox.ResumeLayout(False)
        Me.OptionsGroupBoxFillPanel.ResumeLayout(False)
        Me.CompilerOptionsSourceEnginePanel.ResumeLayout(False)
        Me.CompilerOptionsSourceEnginePanel.PerformLayout()
        Me.DefineBonesGroupBox.ResumeLayout(False)
        Me.DefineBonesGroupBox.PerformLayout()
        Me.CompilerOptionsGoldSourceEnginePanel.ResumeLayout(False)
        Me.CompilerOptionsGoldSourceEnginePanel.PerformLayout()
        Me.GameSetupPanel.ResumeLayout(False)
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        Me.CompileButtonsPanel.ResumeLayout(False)
        Me.PostCompilePanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CompilerOptionsTextBox As Crowbar.RichTextBoxEx
    Friend WithEvents GameSetupComboUserControl As ComboUserControl
    Friend WithEvents BrowseForQcPathFolderOrFileNameButton As ButtonEx
	Friend WithEvents Label6 As Crowbar.LabelEx
	Friend WithEvents SetUpGamesButton As ButtonEx
	Friend WithEvents GameSetupLabel As Crowbar.LabelEx
	Friend WithEvents CompileButton As ButtonEx
	Friend WithEvents CompilerOptionNoP4CheckBox As CheckBoxEx
	Friend WithEvents CompilerOptionVerboseCheckBox As CheckBoxEx
	Friend WithEvents DirectCompilerOptionsLabel As Crowbar.LabelEx
	Friend WithEvents DirectCompilerOptionsTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents Options_LogSplitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents CompileLogRichTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CancelCompileButton As ButtonEx
	Friend WithEvents SkipCurrentModelButton As ButtonEx
    Friend WithEvents CompileComboUserControl As ComboUserControl
    Friend WithEvents RecompileButton As ButtonEx
    Friend WithEvents CompiledFilesComboUserControl As ComboUserControl
    Friend WithEvents GotoQcButton As ButtonEx
	Friend WithEvents GotoCompiledMdlButton As ButtonEx
	Friend WithEvents Panel1 As PanelEx
	Friend WithEvents SourceEngineLogFileCheckBox As CheckBoxEx
	Friend WithEvents OptionsGroupBox As GroupBoxEx
	Friend WithEvents OptionsGroupBoxFillPanel As PanelEx
	Friend WithEvents UseInViewButton As ButtonEx
	Friend WithEvents UseInPackButton As ButtonEx
	Friend WithEvents UseAllInPackButton As ButtonEx
	Friend WithEvents FolderForEachModelCheckBox As CheckBoxEx
	Friend WithEvents CompilerOptionDefineBonesFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CompilerOptionDefineBonesCheckBox As CheckBoxEx
	Friend WithEvents CompilerOptionDefineBonesWriteQciFileCheckBox As CheckBoxEx
	Friend WithEvents CompilerOptionDefineBonesModifyQcFileCheckBox As CheckBoxEx
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents CompileOptionsSourceEngineUseDefaultsButton As ButtonEx
	Friend WithEvents GotoOutputPathButton As ButtonEx
	Friend WithEvents BrowseForOutputPathButton As ButtonEx
	Friend WithEvents UseDefaultOutputSubfolderButton As ButtonEx
	Friend WithEvents OutputPathTextBox As Crowbar.RichTextBoxEx
    Friend WithEvents OutputPathComboUserControl As ComboUserControl
    Friend WithEvents Label1 As Crowbar.LabelEx
	Friend WithEvents GameModelsOutputPathTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CompilerOptionsSourceEnginePanel As PanelEx
	Friend WithEvents CompilerOptionsGoldSourceEnginePanel As PanelEx
	Friend WithEvents GoldSourceEngineLogFileCheckBox As CheckBoxEx
	Friend WithEvents CompileOptionsGoldSourceEngineUseDefaultsButton As ButtonEx
	Friend WithEvents OutputSubfolderTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents QcPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents DefineBonesGroupBox As GroupBoxEx
	Friend WithEvents CompilerOptionDefineBonesOverwriteQciFileCheckBox As CheckBoxEx
	Friend WithEvents GameSetupPanel As PanelEx
	Friend WithEvents CompileButtonsPanel As PanelEx
	Friend WithEvents CompilerOptionsTextBoxMinScrollPanel As PanelEx
	Friend WithEvents PostCompilePanel As PanelEx
	Friend WithEvents PanelEx1 As PanelEx
	Friend WithEvents PanelEx2 As PanelEx
End Class
