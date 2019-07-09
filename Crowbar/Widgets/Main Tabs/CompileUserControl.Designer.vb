<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompileUserControl
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
		Me.CompilerOptionsTextBox = New System.Windows.Forms.TextBox()
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox()
		Me.FolderForEachModelCheckBox = New System.Windows.Forms.CheckBox()
		Me.SourceEngineLogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionNoP4CheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionVerboseCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesCreateFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.CompilerOptionDefineBonesFileNameLabel = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.DirectCompilerOptionsTextBox = New System.Windows.Forms.TextBox()
		Me.BrowseForQcPathFolderOrFileNameButton = New System.Windows.Forms.Button()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.EditGameSetupButton = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.CompileButton = New System.Windows.Forms.Button()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.QcPathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.GameModelsOutputPathTextBox = New Crowbar.TextBoxEx()
		Me.OutputPathTextBox = New Crowbar.TextBoxEx()
		Me.OutputSubfolderTextBox = New Crowbar.TextBoxEx()
		Me.GotoOutputPathButton = New System.Windows.Forms.Button()
		Me.BrowseForOutputPathButton = New System.Windows.Forms.Button()
		Me.OutputPathComboBox = New System.Windows.Forms.ComboBox()
		Me.CompileComboBox = New System.Windows.Forms.ComboBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.GotoQcButton = New System.Windows.Forms.Button()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.CompilerOptionsGoldSourceEnginePanel = New System.Windows.Forms.Panel()
		Me.GoldSourceEngineLogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton = New System.Windows.Forms.Button()
		Me.CompilerOptionsSourceEnginePanel = New System.Windows.Forms.Panel()
		Me.CompileOptionsSourceEngineUseDefaultsButton = New System.Windows.Forms.Button()
		Me.CompileOptionsSizerLabel = New System.Windows.Forms.Label()
		Me.SkipCurrentModelButton = New System.Windows.Forms.Button()
		Me.CancelCompileButton = New System.Windows.Forms.Button()
		Me.UseAllInPackButton = New System.Windows.Forms.Button()
		Me.UseInViewButton = New System.Windows.Forms.Button()
		Me.CompileLogRichTextBox = New Crowbar.RichTextBoxEx()
		Me.CompiledFilesComboBox = New System.Windows.Forms.ComboBox()
		Me.GotoCompiledMdlButton = New System.Windows.Forms.Button()
		Me.RecompileButton = New System.Windows.Forms.Button()
		Me.UseInPackButton = New System.Windows.Forms.Button()
		Me.UseDefaultOutputSubfolderButton = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.Panel1.SuspendLayout()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		Me.Panel2.SuspendLayout()
		Me.CompilerOptionsGoldSourceEnginePanel.SuspendLayout()
		Me.CompilerOptionsSourceEnginePanel.SuspendLayout()
		Me.SuspendLayout()
		'
		'CompilerOptionsTextBox
		'
		Me.CompilerOptionsTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerOptionsTextBox.Location = New System.Drawing.Point(3, 175)
		Me.CompilerOptionsTextBox.Multiline = True
		Me.CompilerOptionsTextBox.Name = "CompilerOptionsTextBox"
		Me.CompilerOptionsTextBox.ReadOnly = True
		Me.CompilerOptionsTextBox.Size = New System.Drawing.Size(758, 45)
		Me.CompilerOptionsTextBox.TabIndex = 15
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(195, 1)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(470, 21)
		Me.GameSetupComboBox.TabIndex = 1
		'
		'FolderForEachModelCheckBox
		'
		Me.FolderForEachModelCheckBox.AutoSize = True
		Me.FolderForEachModelCheckBox.Location = New System.Drawing.Point(502, 74)
		Me.FolderForEachModelCheckBox.Name = "FolderForEachModelCheckBox"
		Me.FolderForEachModelCheckBox.Size = New System.Drawing.Size(130, 17)
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
		Me.SourceEngineLogFileCheckBox.Size = New System.Drawing.Size(108, 17)
		Me.SourceEngineLogFileCheckBox.TabIndex = 4
		Me.SourceEngineLogFileCheckBox.Text = "Write log to a file"
		Me.ToolTip1.SetToolTip(Me.SourceEngineLogFileCheckBox, "Write compile log to a file (in same folder as QC file).")
		Me.SourceEngineLogFileCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionDefineBonesCheckBox
		'
		Me.CompilerOptionDefineBonesCheckBox.AutoSize = True
		Me.CompilerOptionDefineBonesCheckBox.Location = New System.Drawing.Point(179, 3)
		Me.CompilerOptionDefineBonesCheckBox.Name = "CompilerOptionDefineBonesCheckBox"
		Me.CompilerOptionDefineBonesCheckBox.Size = New System.Drawing.Size(89, 17)
		Me.CompilerOptionDefineBonesCheckBox.TabIndex = 7
		Me.CompilerOptionDefineBonesCheckBox.Text = "Define Bones"
		Me.CompilerOptionDefineBonesCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionNoP4CheckBox
		'
		Me.CompilerOptionNoP4CheckBox.AutoSize = True
		Me.CompilerOptionNoP4CheckBox.Location = New System.Drawing.Point(6, 26)
		Me.CompilerOptionNoP4CheckBox.Name = "CompilerOptionNoP4CheckBox"
		Me.CompilerOptionNoP4CheckBox.Size = New System.Drawing.Size(54, 17)
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
		Me.CompilerOptionVerboseCheckBox.Size = New System.Drawing.Size(65, 17)
		Me.CompilerOptionVerboseCheckBox.TabIndex = 6
		Me.CompilerOptionVerboseCheckBox.Text = "Verbose"
		Me.ToolTip1.SetToolTip(Me.CompilerOptionVerboseCheckBox, "Write more info in compile log.")
		Me.CompilerOptionVerboseCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionDefineBonesModifyQcFileCheckBox
		'
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.AutoSize = True
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Enabled = False
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Location = New System.Drawing.Point(213, 72)
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Name = "CompilerOptionDefineBonesModifyQcFileCheckBox"
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Size = New System.Drawing.Size(226, 17)
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.TabIndex = 11
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Text = "Put in QC file: $include ""<QCI file name>"""
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionDefineBonesCreateFileCheckBox
		'
		Me.CompilerOptionDefineBonesCreateFileCheckBox.AutoSize = True
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Enabled = False
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Location = New System.Drawing.Point(196, 26)
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Name = "CompilerOptionDefineBonesCreateFileCheckBox"
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Size = New System.Drawing.Size(225, 17)
		Me.CompilerOptionDefineBonesCreateFileCheckBox.TabIndex = 8
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Text = "Create QCI file (in same folder as QC file)"
		Me.CompilerOptionDefineBonesCreateFileCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionDefineBonesFileNameTextBox
		'
		Me.CompilerOptionDefineBonesFileNameTextBox.Enabled = False
		Me.CompilerOptionDefineBonesFileNameTextBox.Location = New System.Drawing.Point(292, 47)
		Me.CompilerOptionDefineBonesFileNameTextBox.Name = "CompilerOptionDefineBonesFileNameTextBox"
		Me.CompilerOptionDefineBonesFileNameTextBox.Size = New System.Drawing.Size(139, 21)
		Me.CompilerOptionDefineBonesFileNameTextBox.TabIndex = 10
		'
		'CompilerOptionDefineBonesFileNameLabel
		'
		Me.CompilerOptionDefineBonesFileNameLabel.AutoSize = True
		Me.CompilerOptionDefineBonesFileNameLabel.Enabled = False
		Me.CompilerOptionDefineBonesFileNameLabel.Location = New System.Drawing.Point(213, 50)
		Me.CompilerOptionDefineBonesFileNameLabel.Name = "CompilerOptionDefineBonesFileNameLabel"
		Me.CompilerOptionDefineBonesFileNameLabel.Size = New System.Drawing.Size(76, 13)
		Me.CompilerOptionDefineBonesFileNameLabel.TabIndex = 9
		Me.CompilerOptionDefineBonesFileNameLabel.Text = "QCI file name:"
		'
		'Label4
		'
		Me.Label4.Location = New System.Drawing.Point(3, 133)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(393, 13)
		Me.Label4.TabIndex = 13
		Me.Label4.Text = "Direct entry of command-line options (in case they are not included above):"
		'
		'DirectCompilerOptionsTextBox
		'
		Me.DirectCompilerOptionsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DirectCompilerOptionsTextBox.Location = New System.Drawing.Point(3, 149)
		Me.DirectCompilerOptionsTextBox.Name = "DirectCompilerOptionsTextBox"
		Me.DirectCompilerOptionsTextBox.Size = New System.Drawing.Size(758, 21)
		Me.DirectCompilerOptionsTextBox.TabIndex = 14
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
		Me.EditGameSetupButton.Location = New System.Drawing.Point(671, 0)
		Me.EditGameSetupButton.Name = "EditGameSetupButton"
		Me.EditGameSetupButton.Size = New System.Drawing.Size(90, 23)
		Me.EditGameSetupButton.TabIndex = 2
		Me.EditGameSetupButton.Text = "Set Up Games"
		Me.EditGameSetupButton.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.Location = New System.Drawing.Point(3, 5)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(186, 13)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Game that has the model compiler:"
		'
		'CompileButton
		'
		Me.CompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CompileButton.Location = New System.Drawing.Point(0, 249)
		Me.CompileButton.Name = "CompileButton"
		Me.CompileButton.Size = New System.Drawing.Size(120, 23)
		Me.CompileButton.TabIndex = 1
		Me.CompileButton.Text = "Compile"
		Me.CompileButton.UseVisualStyleBackColor = True
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.QcPathFileNameTextBox)
		Me.Panel1.Controls.Add(Me.GameModelsOutputPathTextBox)
		Me.Panel1.Controls.Add(Me.OutputPathTextBox)
		Me.Panel1.Controls.Add(Me.OutputSubfolderTextBox)
		Me.Panel1.Controls.Add(Me.GotoOutputPathButton)
		Me.Panel1.Controls.Add(Me.BrowseForOutputPathButton)
		Me.Panel1.Controls.Add(Me.OutputPathComboBox)
		Me.Panel1.Controls.Add(Me.CompileComboBox)
		Me.Panel1.Controls.Add(Me.Label1)
		Me.Panel1.Controls.Add(Me.GotoQcButton)
		Me.Panel1.Controls.Add(Me.Label6)
		Me.Panel1.Controls.Add(Me.BrowseForQcPathFolderOrFileNameButton)
		Me.Panel1.Controls.Add(Me.SplitContainer1)
		Me.Panel1.Controls.Add(Me.UseDefaultOutputSubfolderButton)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(776, 536)
		Me.Panel1.TabIndex = 15
		'
		'QcPathFileNameTextBox
		'
		Me.QcPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.QcPathFileNameTextBox.Location = New System.Drawing.Point(209, 7)
		Me.QcPathFileNameTextBox.Name = "QcPathFileNameTextBox"
		Me.QcPathFileNameTextBox.Size = New System.Drawing.Size(445, 21)
		Me.QcPathFileNameTextBox.TabIndex = 22
		'
		'GameModelsOutputPathTextBox
		'
		Me.GameModelsOutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameModelsOutputPathTextBox.Location = New System.Drawing.Point(209, 34)
		Me.GameModelsOutputPathTextBox.Name = "GameModelsOutputPathTextBox"
		Me.GameModelsOutputPathTextBox.ReadOnly = True
		Me.GameModelsOutputPathTextBox.Size = New System.Drawing.Size(445, 21)
		Me.GameModelsOutputPathTextBox.TabIndex = 8
		'
		'OutputPathTextBox
		'
		Me.OutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputPathTextBox.Location = New System.Drawing.Point(209, 34)
		Me.OutputPathTextBox.Name = "OutputPathTextBox"
		Me.OutputPathTextBox.Size = New System.Drawing.Size(445, 21)
		Me.OutputPathTextBox.TabIndex = 9
		'
		'OutputSubfolderTextBox
		'
		Me.OutputSubfolderTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputSubfolderTextBox.Location = New System.Drawing.Point(209, 34)
		Me.OutputSubfolderTextBox.Name = "OutputSubfolderTextBox"
		Me.OutputSubfolderTextBox.Size = New System.Drawing.Size(445, 21)
		Me.OutputSubfolderTextBox.TabIndex = 21
		Me.OutputSubfolderTextBox.Visible = False
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
		Me.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.OutputPathComboBox.FormattingEnabled = True
		Me.OutputPathComboBox.Location = New System.Drawing.Point(63, 33)
		Me.OutputPathComboBox.Name = "OutputPathComboBox"
		Me.OutputPathComboBox.Size = New System.Drawing.Size(140, 21)
		Me.OutputPathComboBox.TabIndex = 6
		'
		'CompileComboBox
		'
		Me.CompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CompileComboBox.FormattingEnabled = True
		Me.CompileComboBox.Location = New System.Drawing.Point(63, 4)
		Me.CompileComboBox.Name = "CompileComboBox"
		Me.CompileComboBox.Size = New System.Drawing.Size(140, 21)
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
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer1.Location = New System.Drawing.Point(3, 61)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.OptionsGroupBox)
		Me.SplitContainer1.Panel1.Controls.Add(Me.CompileButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.SkipCurrentModelButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.CancelCompileButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.UseAllInPackButton)
		Me.SplitContainer1.Panel1MinSize = 90
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInViewButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.CompileLogRichTextBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.CompiledFilesComboBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.GotoCompiledMdlButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.RecompileButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInPackButton)
		Me.SplitContainer1.Panel2MinSize = 90
		Me.SplitContainer1.Size = New System.Drawing.Size(770, 472)
		Me.SplitContainer1.SplitterDistance = 275
		Me.SplitContainer1.TabIndex = 16
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OptionsGroupBox.Controls.Add(Me.Panel2)
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.Size = New System.Drawing.Size(770, 243)
		Me.OptionsGroupBox.TabIndex = 0
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'Panel2
		'
		Me.Panel2.AutoScroll = True
		Me.Panel2.Controls.Add(Me.CompilerOptionsGoldSourceEnginePanel)
		Me.Panel2.Controls.Add(Me.CompilerOptionsSourceEnginePanel)
		Me.Panel2.Controls.Add(Me.Label4)
		Me.Panel2.Controls.Add(Me.DirectCompilerOptionsTextBox)
		Me.Panel2.Controls.Add(Me.Label3)
		Me.Panel2.Controls.Add(Me.GameSetupComboBox)
		Me.Panel2.Controls.Add(Me.EditGameSetupButton)
		Me.Panel2.Controls.Add(Me.CompilerOptionsTextBox)
		Me.Panel2.Controls.Add(Me.CompileOptionsSizerLabel)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(3, 17)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(764, 223)
		Me.Panel2.TabIndex = 0
		'
		'CompilerOptionsGoldSourceEnginePanel
		'
		Me.CompilerOptionsGoldSourceEnginePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerOptionsGoldSourceEnginePanel.Controls.Add(Me.GoldSourceEngineLogFileCheckBox)
		Me.CompilerOptionsGoldSourceEnginePanel.Controls.Add(Me.CompileOptionsGoldSourceEngineUseDefaultsButton)
		Me.CompilerOptionsGoldSourceEnginePanel.Location = New System.Drawing.Point(0, 22)
		Me.CompilerOptionsGoldSourceEnginePanel.Name = "CompilerOptionsGoldSourceEnginePanel"
		Me.CompilerOptionsGoldSourceEnginePanel.Size = New System.Drawing.Size(764, 108)
		Me.CompilerOptionsGoldSourceEnginePanel.TabIndex = 13
		'
		'GoldSourceEngineLogFileCheckBox
		'
		Me.GoldSourceEngineLogFileCheckBox.Location = New System.Drawing.Point(6, 3)
		Me.GoldSourceEngineLogFileCheckBox.Name = "GoldSourceEngineLogFileCheckBox"
		Me.GoldSourceEngineLogFileCheckBox.Size = New System.Drawing.Size(108, 17)
		Me.GoldSourceEngineLogFileCheckBox.TabIndex = 14
		Me.GoldSourceEngineLogFileCheckBox.Text = "Write log to a file"
		Me.ToolTip1.SetToolTip(Me.GoldSourceEngineLogFileCheckBox, "Write compile log to a file (in same folder as QC file).")
		Me.GoldSourceEngineLogFileCheckBox.UseVisualStyleBackColor = True
		'
		'CompileOptionsGoldSourceEngineUseDefaultsButton
		'
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Location = New System.Drawing.Point(671, 68)
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Name = "CompileOptionsGoldSourceEngineUseDefaultsButton"
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.TabIndex = 13
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.Text = "Use Defaults"
		Me.ToolTip1.SetToolTip(Me.CompileOptionsGoldSourceEngineUseDefaultsButton, "Set the compiler options back to default settings")
		Me.CompileOptionsGoldSourceEngineUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'CompilerOptionsSourceEnginePanel
		'
		Me.CompilerOptionsSourceEnginePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.SourceEngineLogFileCheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionDefineBonesModifyQcFileCheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionDefineBonesCheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionVerboseCheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionDefineBonesFileNameTextBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionNoP4CheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionDefineBonesFileNameLabel)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.FolderForEachModelCheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompilerOptionDefineBonesCreateFileCheckBox)
		Me.CompilerOptionsSourceEnginePanel.Controls.Add(Me.CompileOptionsSourceEngineUseDefaultsButton)
		Me.CompilerOptionsSourceEnginePanel.Location = New System.Drawing.Point(0, 22)
		Me.CompilerOptionsSourceEnginePanel.Name = "CompilerOptionsSourceEnginePanel"
		Me.CompilerOptionsSourceEnginePanel.Size = New System.Drawing.Size(764, 108)
		Me.CompilerOptionsSourceEnginePanel.TabIndex = 38
		'
		'CompileOptionsSourceEngineUseDefaultsButton
		'
		Me.CompileOptionsSourceEngineUseDefaultsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompileOptionsSourceEngineUseDefaultsButton.Location = New System.Drawing.Point(671, 68)
		Me.CompileOptionsSourceEngineUseDefaultsButton.Name = "CompileOptionsSourceEngineUseDefaultsButton"
		Me.CompileOptionsSourceEngineUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.CompileOptionsSourceEngineUseDefaultsButton.TabIndex = 12
		Me.CompileOptionsSourceEngineUseDefaultsButton.Text = "Use Defaults"
		Me.ToolTip1.SetToolTip(Me.CompileOptionsSourceEngineUseDefaultsButton, "Set the compiler options back to default settings")
		Me.CompileOptionsSourceEngineUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'CompileOptionsSizerLabel
		'
		Me.CompileOptionsSizerLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompileOptionsSizerLabel.Enabled = False
		Me.CompileOptionsSizerLabel.Location = New System.Drawing.Point(3, 175)
		Me.CompileOptionsSizerLabel.Name = "CompileOptionsSizerLabel"
		Me.CompileOptionsSizerLabel.Size = New System.Drawing.Size(761, 47)
		Me.CompileOptionsSizerLabel.TabIndex = 37
		'
		'SkipCurrentModelButton
		'
		Me.SkipCurrentModelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentModelButton.Enabled = False
		Me.SkipCurrentModelButton.Location = New System.Drawing.Point(126, 249)
		Me.SkipCurrentModelButton.Name = "SkipCurrentModelButton"
		Me.SkipCurrentModelButton.Size = New System.Drawing.Size(120, 23)
		Me.SkipCurrentModelButton.TabIndex = 2
		Me.SkipCurrentModelButton.Text = "Skip Current Model"
		Me.SkipCurrentModelButton.UseVisualStyleBackColor = True
		'
		'CancelCompileButton
		'
		Me.CancelCompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CancelCompileButton.Enabled = False
		Me.CancelCompileButton.Location = New System.Drawing.Point(252, 249)
		Me.CancelCompileButton.Name = "CancelCompileButton"
		Me.CancelCompileButton.Size = New System.Drawing.Size(120, 23)
		Me.CancelCompileButton.TabIndex = 3
		Me.CancelCompileButton.Text = "Cancel Compile"
		Me.CancelCompileButton.UseVisualStyleBackColor = True
		'
		'UseAllInPackButton
		'
		Me.UseAllInPackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UseAllInPackButton.Enabled = False
		Me.UseAllInPackButton.Location = New System.Drawing.Point(378, 249)
		Me.UseAllInPackButton.Name = "UseAllInPackButton"
		Me.UseAllInPackButton.Size = New System.Drawing.Size(120, 23)
		Me.UseAllInPackButton.TabIndex = 4
		Me.UseAllInPackButton.Text = "Use All in Pack"
		Me.UseAllInPackButton.UseVisualStyleBackColor = True
		Me.UseAllInPackButton.Visible = False
		'
		'UseInViewButton
		'
		Me.UseInViewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInViewButton.Enabled = False
		Me.UseInViewButton.Location = New System.Drawing.Point(565, 170)
		Me.UseInViewButton.Name = "UseInViewButton"
		Me.UseInViewButton.Size = New System.Drawing.Size(75, 23)
		Me.UseInViewButton.TabIndex = 2
		Me.UseInViewButton.Text = "Use in View"
		Me.UseInViewButton.UseVisualStyleBackColor = True
		'
		'CompileLogRichTextBox
		'
		Me.CompileLogRichTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompileLogRichTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CompileLogRichTextBox.HideSelection = False
		Me.CompileLogRichTextBox.Location = New System.Drawing.Point(0, 0)
		Me.CompileLogRichTextBox.Name = "CompileLogRichTextBox"
		Me.CompileLogRichTextBox.ReadOnly = True
		Me.CompileLogRichTextBox.Size = New System.Drawing.Size(770, 164)
		Me.CompileLogRichTextBox.TabIndex = 0
		Me.CompileLogRichTextBox.Text = ""
		Me.CompileLogRichTextBox.WordWrap = False
		'
		'CompiledFilesComboBox
		'
		Me.CompiledFilesComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompiledFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CompiledFilesComboBox.FormattingEnabled = True
		Me.CompiledFilesComboBox.Location = New System.Drawing.Point(0, 171)
		Me.CompiledFilesComboBox.Name = "CompiledFilesComboBox"
		Me.CompiledFilesComboBox.Size = New System.Drawing.Size(559, 21)
		Me.CompiledFilesComboBox.TabIndex = 1
		'
		'GotoCompiledMdlButton
		'
		Me.GotoCompiledMdlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoCompiledMdlButton.Location = New System.Drawing.Point(727, 170)
		Me.GotoCompiledMdlButton.Name = "GotoCompiledMdlButton"
		Me.GotoCompiledMdlButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoCompiledMdlButton.TabIndex = 4
		Me.GotoCompiledMdlButton.Text = "Goto"
		Me.GotoCompiledMdlButton.UseVisualStyleBackColor = True
		'
		'RecompileButton
		'
		Me.RecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.RecompileButton.Enabled = False
		Me.RecompileButton.Location = New System.Drawing.Point(646, 170)
		Me.RecompileButton.Name = "RecompileButton"
		Me.RecompileButton.Size = New System.Drawing.Size(75, 23)
		Me.RecompileButton.TabIndex = 5
		Me.RecompileButton.Text = "Recompile"
		Me.RecompileButton.UseVisualStyleBackColor = True
		'
		'UseInPackButton
		'
		Me.UseInPackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInPackButton.Enabled = False
		Me.UseInPackButton.Location = New System.Drawing.Point(646, 170)
		Me.UseInPackButton.Name = "UseInPackButton"
		Me.UseInPackButton.Size = New System.Drawing.Size(75, 23)
		Me.UseInPackButton.TabIndex = 3
		Me.UseInPackButton.Text = "Use in Pack"
		Me.UseInPackButton.UseVisualStyleBackColor = True
		Me.UseInPackButton.Visible = False
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
		Me.Panel1.PerformLayout()
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.CompilerOptionsGoldSourceEnginePanel.ResumeLayout(False)
		Me.CompilerOptionsSourceEnginePanel.ResumeLayout(False)
		Me.CompilerOptionsSourceEnginePanel.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents CompilerOptionsTextBox As System.Windows.Forms.TextBox
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents BrowseForQcPathFolderOrFileNameButton As System.Windows.Forms.Button
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents EditGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents CompileButton As System.Windows.Forms.Button
	Friend WithEvents CompilerOptionNoP4CheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionVerboseCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents DirectCompilerOptionsTextBox As System.Windows.Forms.TextBox
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents CompileLogRichTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CancelCompileButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentModelButton As System.Windows.Forms.Button
	Friend WithEvents CompileComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents RecompileButton As System.Windows.Forms.Button
	Friend WithEvents CompiledFilesComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoQcButton As System.Windows.Forms.Button
	Friend WithEvents GotoCompiledMdlButton As System.Windows.Forms.Button
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents SourceEngineLogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents UseInViewButton As System.Windows.Forms.Button
	Friend WithEvents UseInPackButton As System.Windows.Forms.Button
	Friend WithEvents UseAllInPackButton As System.Windows.Forms.Button
	Friend WithEvents FolderForEachModelCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionDefineBonesFileNameLabel As System.Windows.Forms.Label
	Friend WithEvents CompilerOptionDefineBonesFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents CompilerOptionDefineBonesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionDefineBonesCreateFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionDefineBonesModifyQcFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents CompileOptionsSourceEngineUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents GotoOutputPathButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForOutputPathButton As System.Windows.Forms.Button
	Friend WithEvents UseDefaultOutputSubfolderButton As System.Windows.Forms.Button
	Friend WithEvents OutputPathTextBox As Crowbar.TextBoxEx
	Friend WithEvents OutputPathComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents CompileOptionsSizerLabel As System.Windows.Forms.Label
	Friend WithEvents GameModelsOutputPathTextBox As Crowbar.TextBoxEx
	Friend WithEvents CompilerOptionsSourceEnginePanel As System.Windows.Forms.Panel
	Friend WithEvents CompilerOptionsGoldSourceEnginePanel As System.Windows.Forms.Panel
	Friend WithEvents GoldSourceEngineLogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompileOptionsGoldSourceEngineUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents OutputSubfolderTextBox As Crowbar.TextBoxEx
	Friend WithEvents QcPathFileNameTextBox As Crowbar.TextBoxEx

End Class
