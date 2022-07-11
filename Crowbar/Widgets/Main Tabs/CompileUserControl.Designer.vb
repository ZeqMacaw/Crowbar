<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CompileUserControl
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
		Me.CompilerOptionsTextBox = New Crowbar.RichTextBoxEx()
		Me.GameSetupComboBox = New Crowbar.ComboBoxEx()
		Me.FolderForEachModelCheckBox = New System.Windows.Forms.CheckBox()
		Me.SourceEngineLogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionNoP4CheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionVerboseCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesWriteQciFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.DirectCompilerOptionsLabel = New System.Windows.Forms.Label()
		Me.DirectCompilerOptionsTextBox = New Crowbar.RichTextBoxEx()
		Me.BrowseForQcPathFolderOrFileNameButton = New Crowbar.ButtonEx()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.EditGameSetupButton = New Crowbar.ButtonEx()
		Me.GameSetupLabel = New System.Windows.Forms.Label()
		Me.CompileButton = New Crowbar.ButtonEx()
		Me.Panel1 = New Crowbar.PanelEx()
		Me.QcPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.OutputPathTextBox = New Crowbar.RichTextBoxEx()
		Me.GameModelsOutputPathTextBox = New Crowbar.RichTextBoxEx()
		Me.OutputSubfolderTextBox = New Crowbar.RichTextBoxEx()
		Me.GotoOutputPathButton = New Crowbar.ButtonEx()
		Me.BrowseForOutputPathButton = New Crowbar.ButtonEx()
		Me.OutputPathComboBox = New Crowbar.ComboBoxEx()
		Me.CompileComboBox = New Crowbar.ComboBoxEx()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.GotoQcButton = New Crowbar.ButtonEx()
		Me.Options_LogSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.OptionsGroupBox = New Crowbar.GroupBoxEx()
		Me.OptionsGroupBoxFillPanel = New Crowbar.PanelEx()
		Me.GameSetupPanel = New Crowbar.PanelEx()
		Me.CompilerOptionsSourceEnginePanel = New Crowbar.PanelEx()
		Me.DefineBonesGroupBox = New Crowbar.GroupBoxEx()
		Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompileOptionsSourceEngineUseDefaultsButton = New Crowbar.ButtonEx()
		Me.CompilerOptionsGoldSourceEnginePanel = New Crowbar.PanelEx()
		Me.GoldSourceEngineLogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton = New Crowbar.ButtonEx()
		Me.CompilerOptionsTextBoxMinScrollPanel = New Crowbar.PanelEx()
		Me.CompileLogRichTextBox = New Crowbar.RichTextBoxEx()
		Me.CompileButtonsPanel = New Crowbar.PanelEx()
		Me.SkipCurrentModelButton = New Crowbar.ButtonEx()
		Me.CancelCompileButton = New Crowbar.ButtonEx()
		Me.UseAllInPackButton = New Crowbar.ButtonEx()
		Me.PostCompilePanel = New Crowbar.PanelEx()
		Me.CompiledFilesComboBox = New Crowbar.ComboBoxEx()
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
		Me.GameSetupPanel.SuspendLayout()
		Me.CompilerOptionsSourceEnginePanel.SuspendLayout()
		Me.DefineBonesGroupBox.SuspendLayout()
		Me.CompilerOptionsGoldSourceEnginePanel.SuspendLayout()
		Me.CompileButtonsPanel.SuspendLayout()
		Me.PostCompilePanel.SuspendLayout()
		Me.SuspendLayout()
		'
		'CompilerOptionsTextBox
		'
		Me.CompilerOptionsTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerOptionsTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.CompilerOptionsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.CompilerOptionsTextBox.CueBannerText = ""
		Me.CompilerOptionsTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CompilerOptionsTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.CompilerOptionsTextBox.Location = New System.Drawing.Point(0, 169)
		Me.CompilerOptionsTextBox.Name = "CompilerOptionsTextBox"
		Me.CompilerOptionsTextBox.ReadOnly = True
		Me.CompilerOptionsTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.CompilerOptionsTextBox.Size = New System.Drawing.Size(764, 37)
		Me.CompilerOptionsTextBox.TabIndex = 15
		Me.CompilerOptionsTextBox.Text = ""
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
		Me.GameSetupComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.IsReadOnly = False
		Me.GameSetupComboBox.Location = New System.Drawing.Point(192, 1)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(476, 23)
		Me.GameSetupComboBox.TabIndex = 1
		'
		'FolderForEachModelCheckBox
		'
		Me.FolderForEachModelCheckBox.AutoSize = True
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
		Me.CompilerOptionDefineBonesCheckBox.Location = New System.Drawing.Point(179, 4)
		Me.CompilerOptionDefineBonesCheckBox.Name = "CompilerOptionDefineBonesCheckBox"
		Me.CompilerOptionDefineBonesCheckBox.Size = New System.Drawing.Size(91, 17)
		Me.CompilerOptionDefineBonesCheckBox.TabIndex = 7
		Me.CompilerOptionDefineBonesCheckBox.Text = "DefineBones"
		Me.CompilerOptionDefineBonesCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionNoP4CheckBox
		'
		Me.CompilerOptionNoP4CheckBox.AutoSize = True
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
		Me.CompilerOptionDefineBonesWriteQciFileCheckBox.Location = New System.Drawing.Point(6, 22)
		Me.CompilerOptionDefineBonesWriteQciFileCheckBox.Name = "CompilerOptionDefineBonesWriteQciFileCheckBox"
		Me.CompilerOptionDefineBonesWriteQciFileCheckBox.Size = New System.Drawing.Size(97, 17)
		Me.CompilerOptionDefineBonesWriteQciFileCheckBox.TabIndex = 8
		Me.CompilerOptionDefineBonesWriteQciFileCheckBox.Text = "Write QCI file:"
		Me.CompilerOptionDefineBonesWriteQciFileCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionDefineBonesFileNameTextBox
		'
		Me.CompilerOptionDefineBonesFileNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.CompilerOptionDefineBonesFileNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.CompilerOptionDefineBonesFileNameTextBox.CueBannerText = ""
		Me.CompilerOptionDefineBonesFileNameTextBox.Enabled = False
		Me.CompilerOptionDefineBonesFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CompilerOptionDefineBonesFileNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
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
		Me.DirectCompilerOptionsLabel.Size = New System.Drawing.Size(764, 13)
		Me.DirectCompilerOptionsLabel.TabIndex = 13
		Me.DirectCompilerOptionsLabel.Text = "Direct entry of command-line options (in case they are not included above):"
		'
		'DirectCompilerOptionsTextBox
		'
		Me.DirectCompilerOptionsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DirectCompilerOptionsTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.DirectCompilerOptionsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.DirectCompilerOptionsTextBox.CueBannerText = ""
		Me.DirectCompilerOptionsTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.DirectCompilerOptionsTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.DirectCompilerOptionsTextBox.Location = New System.Drawing.Point(0, 141)
		Me.DirectCompilerOptionsTextBox.Multiline = False
		Me.DirectCompilerOptionsTextBox.Name = "DirectCompilerOptionsTextBox"
		Me.DirectCompilerOptionsTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.DirectCompilerOptionsTextBox.Size = New System.Drawing.Size(764, 22)
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
		'EditGameSetupButton
		'
		Me.EditGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.EditGameSetupButton.Location = New System.Drawing.Point(674, 0)
		Me.EditGameSetupButton.Name = "EditGameSetupButton"
		Me.EditGameSetupButton.Size = New System.Drawing.Size(90, 23)
		Me.EditGameSetupButton.TabIndex = 2
		Me.EditGameSetupButton.Text = "Set Up Games"
		Me.EditGameSetupButton.UseVisualStyleBackColor = True
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
		Me.CompileButton.TabIndex = 1
		Me.CompileButton.Text = "&Compile DefineBones"
		Me.CompileButton.UseVisualStyleBackColor = True
		'
		'Panel1
		'
		Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.Panel1.Controls.Add(Me.QcPathFileNameTextBox)
		Me.Panel1.Controls.Add(Me.OutputPathTextBox)
		Me.Panel1.Controls.Add(Me.GameModelsOutputPathTextBox)
		Me.Panel1.Controls.Add(Me.OutputSubfolderTextBox)
		Me.Panel1.Controls.Add(Me.GotoOutputPathButton)
		Me.Panel1.Controls.Add(Me.BrowseForOutputPathButton)
		Me.Panel1.Controls.Add(Me.OutputPathComboBox)
		Me.Panel1.Controls.Add(Me.CompileComboBox)
		Me.Panel1.Controls.Add(Me.Label1)
		Me.Panel1.Controls.Add(Me.GotoQcButton)
		Me.Panel1.Controls.Add(Me.Label6)
		Me.Panel1.Controls.Add(Me.BrowseForQcPathFolderOrFileNameButton)
		Me.Panel1.Controls.Add(Me.Options_LogSplitContainer)
		Me.Panel1.Controls.Add(Me.UseDefaultOutputSubfolderButton)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
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
		Me.QcPathFileNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.QcPathFileNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.QcPathFileNameTextBox.CueBannerText = ""
		Me.QcPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.QcPathFileNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
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
		Me.OutputPathTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.OutputPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.OutputPathTextBox.CueBannerText = ""
		Me.OutputPathTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.OutputPathTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
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
		Me.GameModelsOutputPathTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.GameModelsOutputPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.GameModelsOutputPathTextBox.CueBannerText = ""
		Me.GameModelsOutputPathTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GameModelsOutputPathTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
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
		Me.OutputSubfolderTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.OutputSubfolderTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.OutputSubfolderTextBox.CueBannerText = ""
		Me.OutputSubfolderTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.OutputSubfolderTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
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
		Me.BrowseForOutputPathButton.TabIndex = 10
		Me.BrowseForOutputPathButton.Text = "Browse..."
		Me.BrowseForOutputPathButton.UseVisualStyleBackColor = True
		'
		'OutputPathComboBox
		'
		Me.OutputPathComboBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
		Me.OutputPathComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.OutputPathComboBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.OutputPathComboBox.FormattingEnabled = True
		Me.OutputPathComboBox.IsReadOnly = False
		Me.OutputPathComboBox.Location = New System.Drawing.Point(63, 33)
		Me.OutputPathComboBox.Name = "OutputPathComboBox"
		Me.OutputPathComboBox.Size = New System.Drawing.Size(140, 23)
		Me.OutputPathComboBox.TabIndex = 6
		'
		'CompileComboBox
		'
		Me.CompileComboBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
		Me.CompileComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.CompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CompileComboBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.CompileComboBox.FormattingEnabled = True
		Me.CompileComboBox.IsReadOnly = False
		Me.CompileComboBox.Location = New System.Drawing.Point(63, 4)
		Me.CompileComboBox.Name = "CompileComboBox"
		Me.CompileComboBox.Size = New System.Drawing.Size(140, 23)
		Me.CompileComboBox.TabIndex = 1
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
		Me.OptionsGroupBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.OptionsGroupBox.Controls.Add(Me.OptionsGroupBoxFillPanel)
		Me.OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsGroupBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
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
		Me.OptionsGroupBoxFillPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.GameSetupPanel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.CompilerOptionsSourceEnginePanel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.CompilerOptionsGoldSourceEnginePanel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.DirectCompilerOptionsLabel)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.DirectCompilerOptionsTextBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.CompilerOptionsTextBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.CompilerOptionsTextBoxMinScrollPanel)
		Me.OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsGroupBoxFillPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.OptionsGroupBoxFillPanel.Location = New System.Drawing.Point(3, 18)
		Me.OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel"
		Me.OptionsGroupBoxFillPanel.SelectedIndex = -1
		Me.OptionsGroupBoxFillPanel.SelectedValue = Nothing
		Me.OptionsGroupBoxFillPanel.Size = New System.Drawing.Size(764, 209)
		Me.OptionsGroupBoxFillPanel.TabIndex = 0
		'
		'GameSetupPanel
		'
		Me.GameSetupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.GameSetupPanel.Controls.Add(Me.GameSetupLabel)
		Me.GameSetupPanel.Controls.Add(Me.GameSetupComboBox)
		Me.GameSetupPanel.Controls.Add(Me.EditGameSetupButton)
		Me.GameSetupPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.GameSetupPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.GameSetupPanel.Location = New System.Drawing.Point(0, 0)
		Me.GameSetupPanel.Name = "GameSetupPanel"
		Me.GameSetupPanel.SelectedIndex = -1
		Me.GameSetupPanel.SelectedValue = Nothing
		Me.GameSetupPanel.Size = New System.Drawing.Size(764, 26)
		Me.GameSetupPanel.TabIndex = 40
		'
		'CompilerOptionsSourceEnginePanel
		'
		Me.CompilerOptionsSourceEnginePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerOptionsSourceEnginePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionDefineBonesCheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.DefineBonesGroupBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.SourceEngineLogFileCheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionVerboseCheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionNoP4CheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.FolderForEachModelCheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompileOptionsSourceEngineUseDefaultsButton)
		Me.CompilerOptionsSourceEnginePanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.CompilerOptionsSourceEnginePanel.Location = New System.Drawing.Point(0, 24)
		Me.CompilerOptionsSourceEnginePanel.Name = "CompilerOptionsSourceEnginePanel"
		Me.CompilerOptionsSourceEnginePanel.SelectedIndex = -1
		Me.CompilerOptionsSourceEnginePanel.SelectedValue = Nothing
		Me.CompilerOptionsSourceEnginePanel.Size = New System.Drawing.Size(764, 100)
		Me.CompilerOptionsSourceEnginePanel.TabIndex = 38
		'
		'DefineBonesGroupBox
		'
		Me.DefineBonesGroupBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.DefineBonesGroupBox.Controls.Add(Me.CompilerOptionDefineBonesFileNameTextBox)
		Me.DefineBonesGroupBox.Controls.Add(Me.CompilerOptionDefineBonesModifyQcFileCheckBox)
		Me.DefineBonesGroupBox.Controls.Add(Me.CompilerOptionDefineBonesOverwriteQciFileCheckBox)
		Me.DefineBonesGroupBox.Controls.Add(Me.CompilerOptionDefineBonesWriteQciFileCheckBox)
		Me.DefineBonesGroupBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
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
		Me.CompileOptionsSourceEngineUseDefaultsButton.Location = New System.Drawing.Point(674, 68)
		Me.CompileOptionsSourceEngineUseDefaultsButton.Name = "CompileOptionsSourceEngineUseDefaultsButton"
		Me.CompileOptionsSourceEngineUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.CompileOptionsSourceEngineUseDefaultsButton.TabIndex = 12
		Me.CompileOptionsSourceEngineUseDefaultsButton.Text = "Use Defaults"
		Me.ToolTip1.SetToolTip(Me.CompileOptionsSourceEngineUseDefaultsButton, "Set the compiler options back to default settings")
		Me.CompileOptionsSourceEngineUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'CompilerOptionsGoldSourceEnginePanel
		'
		Me.CompilerOptionsGoldSourceEnginePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerOptionsGoldSourceEnginePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.CompilerOptionsGoldSourceEnginePanel.Controls.Add(Me.GoldSourceEngineLogFileCheckBox)
		Me.CompilerOptionsGoldSourceEnginePanel.Controls.Add(Me.CompileOptionsGoldSourceEngineUseDefaultsButton)
		Me.CompilerOptionsGoldSourceEnginePanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.CompilerOptionsGoldSourceEnginePanel.Location = New System.Drawing.Point(0, 24)
		Me.CompilerOptionsGoldSourceEnginePanel.Name = "CompilerOptionsGoldSourceEnginePanel"
		Me.CompilerOptionsGoldSourceEnginePanel.SelectedIndex = -1
		Me.CompilerOptionsGoldSourceEnginePanel.SelectedValue = Nothing
		Me.CompilerOptionsGoldSourceEnginePanel.Size = New System.Drawing.Size(764, 100)
		Me.CompilerOptionsGoldSourceEnginePanel.TabIndex = 13
		'
		'GoldSourceEngineLogFileCheckBox
		'
		Me.GoldSourceEngineLogFileCheckBox.AutoSize = True
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
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Location = New System.Drawing.Point(674, 68)
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Name = "CompileOptionsGoldSourceEngineUseDefaultsButton"
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.TabIndex = 13
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Text = "Use Defaults"
		Me.ToolTip1.SetToolTip(Me.CompileOptionsGoldSourceEngineUseDefaultsButton, "Set the compiler options back to default settings")
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'CompilerOptionsTextBoxMinScrollPanel
		'
		Me.CompilerOptionsTextBoxMinScrollPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.CompilerOptionsTextBoxMinScrollPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.CompilerOptionsTextBoxMinScrollPanel.Location = New System.Drawing.Point(0, 169)
		Me.CompilerOptionsTextBoxMinScrollPanel.Name = "CompilerOptionsTextBoxMinScrollPanel"
		Me.CompilerOptionsTextBoxMinScrollPanel.SelectedIndex = -1
		Me.CompilerOptionsTextBoxMinScrollPanel.SelectedValue = Nothing
		Me.CompilerOptionsTextBoxMinScrollPanel.Size = New System.Drawing.Size(764, 37)
		Me.CompilerOptionsTextBoxMinScrollPanel.TabIndex = 41
		'
		'CompileLogRichTextBox
		'
		Me.CompileLogRichTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.CompileLogRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.CompileLogRichTextBox.CueBannerText = ""
		Me.CompileLogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.CompileLogRichTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CompileLogRichTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
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
		Me.CompileButtonsPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.CompileButtonsPanel.Controls.Add(Me.CompileButton)
		Me.CompileButtonsPanel.Controls.Add(Me.SkipCurrentModelButton)
		Me.CompileButtonsPanel.Controls.Add(Me.CancelCompileButton)
		Me.CompileButtonsPanel.Controls.Add(Me.UseAllInPackButton)
		Me.CompileButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.CompileButtonsPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
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
		Me.UseAllInPackButton.TabIndex = 4
		Me.UseAllInPackButton.Text = "Use All in Pack"
		Me.UseAllInPackButton.UseVisualStyleBackColor = True
		Me.UseAllInPackButton.Visible = False
		'
		'PostCompilePanel
		'
		Me.PostCompilePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.PostCompilePanel.Controls.Add(Me.CompiledFilesComboBox)
		Me.PostCompilePanel.Controls.Add(Me.UseInViewButton)
		Me.PostCompilePanel.Controls.Add(Me.RecompileButton)
		Me.PostCompilePanel.Controls.Add(Me.UseInPackButton)
		Me.PostCompilePanel.Controls.Add(Me.GotoCompiledMdlButton)
		Me.PostCompilePanel.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PostCompilePanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.PostCompilePanel.Location = New System.Drawing.Point(0, 212)
		Me.PostCompilePanel.Name = "PostCompilePanel"
		Me.PostCompilePanel.SelectedIndex = -1
		Me.PostCompilePanel.SelectedValue = Nothing
		Me.PostCompilePanel.Size = New System.Drawing.Size(770, 26)
		Me.PostCompilePanel.TabIndex = 40
		'
		'CompiledFilesComboBox
		'
		Me.CompiledFilesComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompiledFilesComboBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
		Me.CompiledFilesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.CompiledFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CompiledFilesComboBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.CompiledFilesComboBox.FormattingEnabled = True
		Me.CompiledFilesComboBox.IsReadOnly = False
		Me.CompiledFilesComboBox.Location = New System.Drawing.Point(0, 4)
		Me.CompiledFilesComboBox.Name = "CompiledFilesComboBox"
		Me.CompiledFilesComboBox.Size = New System.Drawing.Size(559, 23)
		Me.CompiledFilesComboBox.TabIndex = 1
		'
		'UseInViewButton
		'
		Me.UseInViewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInViewButton.Enabled = False
		Me.UseInViewButton.Location = New System.Drawing.Point(565, 3)
		Me.UseInViewButton.Name = "UseInViewButton"
		Me.UseInViewButton.Size = New System.Drawing.Size(75, 23)
		Me.UseInViewButton.TabIndex = 2
		Me.UseInViewButton.Text = "Use in View"
		Me.UseInViewButton.UseVisualStyleBackColor = True
		'
		'RecompileButton
		'
		Me.RecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.RecompileButton.Enabled = False
		Me.RecompileButton.Location = New System.Drawing.Point(646, 3)
		Me.RecompileButton.Name = "RecompileButton"
		Me.RecompileButton.Size = New System.Drawing.Size(75, 23)
		Me.RecompileButton.TabIndex = 5
		Me.RecompileButton.Text = "Recompile"
		Me.RecompileButton.UseVisualStyleBackColor = True
		'
		'UseInPackButton
		'
		Me.UseInPackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInPackButton.Enabled = False
		Me.UseInPackButton.Location = New System.Drawing.Point(646, 3)
		Me.UseInPackButton.Name = "UseInPackButton"
		Me.UseInPackButton.Size = New System.Drawing.Size(75, 23)
		Me.UseInPackButton.TabIndex = 3
		Me.UseInPackButton.Text = "Use in Pack"
		Me.UseInPackButton.UseVisualStyleBackColor = True
		Me.UseInPackButton.Visible = False
		'
		'GotoCompiledMdlButton
		'
		Me.GotoCompiledMdlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoCompiledMdlButton.Location = New System.Drawing.Point(727, 3)
		Me.GotoCompiledMdlButton.Name = "GotoCompiledMdlButton"
		Me.GotoCompiledMdlButton.Size = New System.Drawing.Size(43, 23)
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
		Me.GameSetupPanel.ResumeLayout(False)
		Me.CompilerOptionsSourceEnginePanel.ResumeLayout(False)
		Me.CompilerOptionsSourceEnginePanel.PerformLayout()
		Me.DefineBonesGroupBox.ResumeLayout(False)
		Me.DefineBonesGroupBox.PerformLayout()
		Me.CompilerOptionsGoldSourceEnginePanel.ResumeLayout(False)
		Me.CompilerOptionsGoldSourceEnginePanel.PerformLayout()
		Me.CompileButtonsPanel.ResumeLayout(False)
		Me.PostCompilePanel.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents CompilerOptionsTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents GameSetupComboBox As ComboBoxEx
	Friend WithEvents BrowseForQcPathFolderOrFileNameButton As ButtonEx
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents EditGameSetupButton As ButtonEx
	Friend WithEvents GameSetupLabel As System.Windows.Forms.Label
	Friend WithEvents CompileButton As ButtonEx
	Friend WithEvents CompilerOptionNoP4CheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionVerboseCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents DirectCompilerOptionsLabel As System.Windows.Forms.Label
	Friend WithEvents DirectCompilerOptionsTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents Options_LogSplitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents CompileLogRichTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CancelCompileButton As ButtonEx
	Friend WithEvents SkipCurrentModelButton As ButtonEx
	Friend WithEvents CompileComboBox As ComboBoxEx
	Friend WithEvents RecompileButton As ButtonEx
	Friend WithEvents CompiledFilesComboBox As ComboBoxEx
	Friend WithEvents GotoQcButton As ButtonEx
	Friend WithEvents GotoCompiledMdlButton As ButtonEx
	Friend WithEvents Panel1 As PanelEx
	Friend WithEvents SourceEngineLogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsGroupBox As GroupBoxEx
	Friend WithEvents OptionsGroupBoxFillPanel As PanelEx
	Friend WithEvents UseInViewButton As ButtonEx
	Friend WithEvents UseInPackButton As ButtonEx
	Friend WithEvents UseAllInPackButton As ButtonEx
	Friend WithEvents FolderForEachModelCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionDefineBonesFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CompilerOptionDefineBonesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionDefineBonesWriteQciFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionDefineBonesModifyQcFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents CompileOptionsSourceEngineUseDefaultsButton As ButtonEx
	Friend WithEvents GotoOutputPathButton As ButtonEx
	Friend WithEvents BrowseForOutputPathButton As ButtonEx
	Friend WithEvents UseDefaultOutputSubfolderButton As ButtonEx
	Friend WithEvents OutputPathTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents OutputPathComboBox As ComboBoxEx
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents GameModelsOutputPathTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CompilerOptionsSourceEnginePanel As PanelEx
	Friend WithEvents CompilerOptionsGoldSourceEnginePanel As PanelEx
	Friend WithEvents GoldSourceEngineLogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompileOptionsGoldSourceEngineUseDefaultsButton As ButtonEx
	Friend WithEvents OutputSubfolderTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents QcPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents DefineBonesGroupBox As GroupBoxEx
	Friend WithEvents CompilerOptionDefineBonesOverwriteQciFileCheckBox As CheckBox
	Friend WithEvents GameSetupPanel As PanelEx
	Friend WithEvents CompileButtonsPanel As PanelEx
	Friend WithEvents CompilerOptionsTextBoxMinScrollPanel As PanelEx
	Friend WithEvents PostCompilePanel As PanelEx
End Class
