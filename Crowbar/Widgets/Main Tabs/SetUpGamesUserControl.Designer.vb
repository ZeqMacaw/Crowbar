<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetUpGamesUserControl
	Inherits BaseUserControl

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.AddLibraryPathButton = New Crowbar.ButtonEx()
		Me.DeleteLibraryPathButton = New Crowbar.ButtonEx()
		Me.Label11 = New Crowbar.LabelEx()
		Me.Label10 = New Crowbar.LabelEx()
		Me.BrowseForSteamAppPathFileNameButton = New Crowbar.ButtonEx()
		Me.SteamAppPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.AddGameSetupButton = New Crowbar.ButtonEx()
		Me.GameGroupBox = New Crowbar.GroupBoxEx()
		Me.EngineLabel = New Crowbar.LabelEx()
		Me.EngineComboUserControl = New Crowbar.ComboUserControl()
		Me.CreateModelsFolderTreeButton = New Crowbar.ButtonEx()
		Me.BrowseForMappingToolPathFileNameButton = New Crowbar.ButtonEx()
		Me.MappingToolPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.MappingToolLabel = New Crowbar.LabelEx()
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel = New System.Windows.Forms.Panel()
		Me.GameAppOptionsTextBox = New Crowbar.RichTextBoxEx()
		Me.ExecutableOptionsLabel = New Crowbar.LabelEx()
		Me.ClearGameAppOptionsButton = New Crowbar.ButtonEx()
		Me.BrowseForGameAppPathFileNameButton = New Crowbar.ButtonEx()
		Me.GameAppPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.ExecutableLabel = New Crowbar.LabelEx()
		Me.PackerLabel = New Crowbar.LabelEx()
		Me.BrowseForUnpackerPathFileNameButton = New Crowbar.ButtonEx()
		Me.PackerPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.ModelViewerLabel = New Crowbar.LabelEx()
		Me.BrowseForViewerPathFileNameButton = New Crowbar.ButtonEx()
		Me.ViewerPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.CloneGameSetupButton = New Crowbar.ButtonEx()
		Me.GameNameTextBox = New Crowbar.RichTextBoxEx()
		Me.NameLabel = New Crowbar.LabelEx()
		Me.DeleteGameSetupButton = New Crowbar.ButtonEx()
		Me.BrowseForGamePathFileNameButton = New Crowbar.ButtonEx()
		Me.GamePathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.ModelCompilerLabel = New Crowbar.LabelEx()
		Me.BrowseForCompilerPathFileNameButton = New Crowbar.ButtonEx()
		Me.CompilerPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.GamePathLabel = New Crowbar.LabelEx()
		Me.GoBackButton = New Crowbar.ButtonEx()
		Me.SteamLibraryPathsDataGridView = New Crowbar.MacroDataGridView()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.Panel1 = New Crowbar.PanelEx()
		Me.GameSetupComboUserControl = New Crowbar.ComboUserControl()
		Me.GameGroupBox.SuspendLayout()
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel.SuspendLayout()
		CType(Me.SteamLibraryPathsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'AddLibraryPathButton
		'
		Me.AddLibraryPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.AddLibraryPathButton.ButtonCanBeFocused = True
		Me.AddLibraryPathButton.Location = New System.Drawing.Point(613, 412)
		Me.AddLibraryPathButton.Name = "AddLibraryPathButton"
		Me.AddLibraryPathButton.Size = New System.Drawing.Size(75, 23)
		Me.AddLibraryPathButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.AddLibraryPathButton.TabIndex = 5
		Me.AddLibraryPathButton.Text = "Add Macro"
		Me.AddLibraryPathButton.UseVisualStyleBackColor = True
		'
		'DeleteLibraryPathButton
		'
		Me.DeleteLibraryPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DeleteLibraryPathButton.ButtonCanBeFocused = True
		Me.DeleteLibraryPathButton.Location = New System.Drawing.Point(613, 441)
		Me.DeleteLibraryPathButton.Name = "DeleteLibraryPathButton"
		Me.DeleteLibraryPathButton.Size = New System.Drawing.Size(75, 50)
		Me.DeleteLibraryPathButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.DeleteLibraryPathButton.TabIndex = 6
		Me.DeleteLibraryPathButton.Text = "Delete Last Macro If Not Used"
		Me.DeleteLibraryPathButton.UseVisualStyleBackColor = True
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Location = New System.Drawing.Point(3, 396)
		Me.Label11.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(573, 13)
		Me.Label11.TabIndex = 48
		Me.Label11.Text = "Steam Library folders (<library#> macros for placing at start of fields above; ri" &
	"ght-click a macro for commands):"
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Location = New System.Drawing.Point(3, 348)
		Me.Label10.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(314, 13)
		Me.Label10.TabIndex = 45
		Me.Label10.Text = "Steam executable (steam.exe) [Used for ""Run Game"" button]:"
		'
		'BrowseForSteamAppPathFileNameButton
		'
		Me.BrowseForSteamAppPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForSteamAppPathFileNameButton.ButtonCanBeFocused = True
		Me.BrowseForSteamAppPathFileNameButton.Location = New System.Drawing.Point(613, 364)
		Me.BrowseForSteamAppPathFileNameButton.Name = "BrowseForSteamAppPathFileNameButton"
		Me.BrowseForSteamAppPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForSteamAppPathFileNameButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.BrowseForSteamAppPathFileNameButton.TabIndex = 3
		Me.BrowseForSteamAppPathFileNameButton.Text = "Browse..."
		Me.BrowseForSteamAppPathFileNameButton.UseVisualStyleBackColor = True
		'
		'SteamAppPathFileNameTextBox
		'
		Me.SteamAppPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SteamAppPathFileNameTextBox.CueBannerText = ""
		Me.SteamAppPathFileNameTextBox.DetectUrls = False
		Me.SteamAppPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.SteamAppPathFileNameTextBox.Location = New System.Drawing.Point(3, 365)
		Me.SteamAppPathFileNameTextBox.Multiline = False
		Me.SteamAppPathFileNameTextBox.Name = "SteamAppPathFileNameTextBox"
		Me.SteamAppPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.SteamAppPathFileNameTextBox.SelectionEnabled = True
		Me.SteamAppPathFileNameTextBox.Size = New System.Drawing.Size(604, 22)
		Me.SteamAppPathFileNameTextBox.TabIndex = 2
		Me.SteamAppPathFileNameTextBox.Text = ""
		Me.SteamAppPathFileNameTextBox.WordWrap = False
		'
		'AddGameSetupButton
		'
		Me.AddGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.AddGameSetupButton.ButtonCanBeFocused = True
		Me.AddGameSetupButton.Location = New System.Drawing.Point(706, 3)
		Me.AddGameSetupButton.Name = "AddGameSetupButton"
		Me.AddGameSetupButton.Size = New System.Drawing.Size(75, 22)
		Me.AddGameSetupButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.AddGameSetupButton.TabIndex = 1
		Me.AddGameSetupButton.Text = "Add"
		Me.AddGameSetupButton.UseVisualStyleBackColor = True
		'
		'GameGroupBox
		'
		Me.GameGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameGroupBox.Controls.Add(Me.EngineLabel)
		Me.GameGroupBox.Controls.Add(Me.EngineComboUserControl)
		Me.GameGroupBox.Controls.Add(Me.CreateModelsFolderTreeButton)
		Me.GameGroupBox.Controls.Add(Me.BrowseForMappingToolPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.MappingToolPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.MappingToolLabel)
		Me.GameGroupBox.Controls.Add(Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel)
		Me.GameGroupBox.Controls.Add(Me.ExecutableOptionsLabel)
		Me.GameGroupBox.Controls.Add(Me.ClearGameAppOptionsButton)
		Me.GameGroupBox.Controls.Add(Me.BrowseForGameAppPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.GameAppPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.ExecutableLabel)
		Me.GameGroupBox.Controls.Add(Me.PackerLabel)
		Me.GameGroupBox.Controls.Add(Me.BrowseForUnpackerPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.PackerPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.ModelViewerLabel)
		Me.GameGroupBox.Controls.Add(Me.BrowseForViewerPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.ViewerPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.CloneGameSetupButton)
		Me.GameGroupBox.Controls.Add(Me.GameNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.NameLabel)
		Me.GameGroupBox.Controls.Add(Me.DeleteGameSetupButton)
		Me.GameGroupBox.Controls.Add(Me.BrowseForGamePathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.GamePathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.ModelCompilerLabel)
		Me.GameGroupBox.Controls.Add(Me.BrowseForCompilerPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.CompilerPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.GamePathLabel)
		Me.GameGroupBox.IsReadOnly = False
		Me.GameGroupBox.Location = New System.Drawing.Point(3, 32)
		Me.GameGroupBox.Name = "GameGroupBox"
		Me.GameGroupBox.SelectedValue = Nothing
		Me.GameGroupBox.Size = New System.Drawing.Size(778, 304)
		Me.GameGroupBox.TabIndex = 3
		Me.GameGroupBox.TabStop = False
		Me.GameGroupBox.Text = "Game Setup"
		'
		'EngineLabel
		'
		Me.EngineLabel.AutoSize = True
		Me.EngineLabel.Location = New System.Drawing.Point(6, 49)
		Me.EngineLabel.Name = "EngineLabel"
		Me.EngineLabel.Size = New System.Drawing.Size(46, 13)
		Me.EngineLabel.TabIndex = 6
		Me.EngineLabel.Text = "Engine:"
		'
		'EngineComboUserControl
		'
		Me.EngineComboUserControl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.EngineComboUserControl.IsReadOnly = False
		Me.EngineComboUserControl.Location = New System.Drawing.Point(55, 45)
		Me.EngineComboUserControl.MaxDropDownItems = 30
		Me.EngineComboUserControl.MultipleInputsIsAllowed = True
		Me.EngineComboUserControl.Name = "EngineComboUserControl"
		Me.EngineComboUserControl.Size = New System.Drawing.Size(121, 23)
		Me.EngineComboUserControl.TabIndex = 7
		Me.EngineComboUserControl.TextHistoryIsKept = False
		Me.EngineComboUserControl.TextHistoryMaxSize = 15
		Me.EngineComboUserControl.TextIsPathFileNames = False
		'
		'CreateModelsFolderTreeButton
		'
		Me.CreateModelsFolderTreeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CreateModelsFolderTreeButton.ButtonCanBeFocused = True
		Me.CreateModelsFolderTreeButton.Location = New System.Drawing.Point(502, 275)
		Me.CreateModelsFolderTreeButton.Name = "CreateModelsFolderTreeButton"
		Me.CreateModelsFolderTreeButton.Size = New System.Drawing.Size(270, 23)
		Me.CreateModelsFolderTreeButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.CreateModelsFolderTreeButton.TabIndex = 15
		Me.CreateModelsFolderTreeButton.Text = "Create ""models"" folder tree from this game's VPKs"
		Me.ToolTip1.SetToolTip(Me.CreateModelsFolderTreeButton, "Use this so HLMV can view models found in VPKs.")
		Me.CreateModelsFolderTreeButton.UseVisualStyleBackColor = True
		'
		'BrowseForMappingToolPathFileNameButton
		'
		Me.BrowseForMappingToolPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForMappingToolPathFileNameButton.ButtonCanBeFocused = True
		Me.BrowseForMappingToolPathFileNameButton.Location = New System.Drawing.Point(697, 217)
		Me.BrowseForMappingToolPathFileNameButton.Name = "BrowseForMappingToolPathFileNameButton"
		Me.BrowseForMappingToolPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForMappingToolPathFileNameButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.BrowseForMappingToolPathFileNameButton.TabIndex = 11
		Me.BrowseForMappingToolPathFileNameButton.Text = "Browse..."
		Me.BrowseForMappingToolPathFileNameButton.UseVisualStyleBackColor = True
		'
		'MappingToolPathFileNameTextBox
		'
		Me.MappingToolPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MappingToolPathFileNameTextBox.CueBannerText = ""
		Me.MappingToolPathFileNameTextBox.DetectUrls = False
		Me.MappingToolPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.MappingToolPathFileNameTextBox.Location = New System.Drawing.Point(102, 218)
		Me.MappingToolPathFileNameTextBox.Multiline = False
		Me.MappingToolPathFileNameTextBox.Name = "MappingToolPathFileNameTextBox"
		Me.MappingToolPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.MappingToolPathFileNameTextBox.SelectionEnabled = True
		Me.MappingToolPathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.MappingToolPathFileNameTextBox.TabIndex = 10
		Me.MappingToolPathFileNameTextBox.Text = ""
		Me.MappingToolPathFileNameTextBox.WordWrap = False
		'
		'MappingToolLabel
		'
		Me.MappingToolLabel.AutoSize = True
		Me.MappingToolLabel.Location = New System.Drawing.Point(6, 222)
		Me.MappingToolLabel.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.MappingToolLabel.Name = "MappingToolLabel"
		Me.MappingToolLabel.Size = New System.Drawing.Size(81, 13)
		Me.MappingToolLabel.TabIndex = 37
		Me.MappingToolLabel.Text = "Mapping tool:"
		'
		'GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel
		'
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel.Controls.Add(Me.GameAppOptionsTextBox)
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel.Location = New System.Drawing.Point(112, 102)
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel.Name = "GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel"
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel.Size = New System.Drawing.Size(579, 22)
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel.TabIndex = 44
		'
		'GameAppOptionsTextBox
		'
		Me.GameAppOptionsTextBox.CueBannerText = ""
		Me.GameAppOptionsTextBox.DetectUrls = False
		Me.GameAppOptionsTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.GameAppOptionsTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GameAppOptionsTextBox.Location = New System.Drawing.Point(0, 0)
		Me.GameAppOptionsTextBox.Multiline = False
		Me.GameAppOptionsTextBox.Name = "GameAppOptionsTextBox"
		Me.GameAppOptionsTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.GameAppOptionsTextBox.SelectionEnabled = True
		Me.GameAppOptionsTextBox.Size = New System.Drawing.Size(579, 22)
		Me.GameAppOptionsTextBox.TabIndex = 12
		Me.GameAppOptionsTextBox.Text = ""
		Me.GameAppOptionsTextBox.WordWrap = False
		'
		'ExecutableOptionsLabel
		'
		Me.ExecutableOptionsLabel.AutoSize = True
		Me.ExecutableOptionsLabel.Location = New System.Drawing.Point(6, 106)
		Me.ExecutableOptionsLabel.Name = "ExecutableOptionsLabel"
		Me.ExecutableOptionsLabel.Size = New System.Drawing.Size(108, 13)
		Me.ExecutableOptionsLabel.TabIndex = 11
		Me.ExecutableOptionsLabel.Text = "Executable options:"
		'
		'ClearGameAppOptionsButton
		'
		Me.ClearGameAppOptionsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ClearGameAppOptionsButton.ButtonCanBeFocused = True
		Me.ClearGameAppOptionsButton.Location = New System.Drawing.Point(697, 101)
		Me.ClearGameAppOptionsButton.Name = "ClearGameAppOptionsButton"
		Me.ClearGameAppOptionsButton.Size = New System.Drawing.Size(75, 23)
		Me.ClearGameAppOptionsButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.ClearGameAppOptionsButton.TabIndex = 13
		Me.ClearGameAppOptionsButton.Text = "Clear"
		Me.ClearGameAppOptionsButton.UseVisualStyleBackColor = True
		'
		'BrowseForGameAppPathFileNameButton
		'
		Me.BrowseForGameAppPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForGameAppPathFileNameButton.ButtonCanBeFocused = True
		Me.BrowseForGameAppPathFileNameButton.Location = New System.Drawing.Point(697, 72)
		Me.BrowseForGameAppPathFileNameButton.Name = "BrowseForGameAppPathFileNameButton"
		Me.BrowseForGameAppPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForGameAppPathFileNameButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.BrowseForGameAppPathFileNameButton.TabIndex = 10
		Me.BrowseForGameAppPathFileNameButton.Text = "Browse..."
		Me.BrowseForGameAppPathFileNameButton.UseVisualStyleBackColor = True
		'
		'GameAppPathFileNameTextBox
		'
		Me.GameAppPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameAppPathFileNameTextBox.CueBannerText = ""
		Me.GameAppPathFileNameTextBox.DetectUrls = False
		Me.GameAppPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GameAppPathFileNameTextBox.Location = New System.Drawing.Point(112, 73)
		Me.GameAppPathFileNameTextBox.Multiline = False
		Me.GameAppPathFileNameTextBox.Name = "GameAppPathFileNameTextBox"
		Me.GameAppPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.GameAppPathFileNameTextBox.SelectionEnabled = True
		Me.GameAppPathFileNameTextBox.Size = New System.Drawing.Size(579, 22)
		Me.GameAppPathFileNameTextBox.TabIndex = 9
		Me.GameAppPathFileNameTextBox.Text = ""
		Me.GameAppPathFileNameTextBox.WordWrap = False
		'
		'ExecutableLabel
		'
		Me.ExecutableLabel.AutoSize = True
		Me.ExecutableLabel.Location = New System.Drawing.Point(6, 77)
		Me.ExecutableLabel.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.ExecutableLabel.Name = "ExecutableLabel"
		Me.ExecutableLabel.Size = New System.Drawing.Size(99, 13)
		Me.ExecutableLabel.TabIndex = 8
		Me.ExecutableLabel.Text = "Executable (*.exe):"
		'
		'PackerLabel
		'
		Me.PackerLabel.AutoSize = True
		Me.PackerLabel.Location = New System.Drawing.Point(6, 251)
		Me.PackerLabel.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.PackerLabel.Name = "PackerLabel"
		Me.PackerLabel.Size = New System.Drawing.Size(67, 13)
		Me.PackerLabel.TabIndex = 16
		Me.PackerLabel.Text = "Packer tool:"
		'
		'BrowseForUnpackerPathFileNameButton
		'
		Me.BrowseForUnpackerPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForUnpackerPathFileNameButton.ButtonCanBeFocused = True
		Me.BrowseForUnpackerPathFileNameButton.Location = New System.Drawing.Point(697, 246)
		Me.BrowseForUnpackerPathFileNameButton.Name = "BrowseForUnpackerPathFileNameButton"
		Me.BrowseForUnpackerPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForUnpackerPathFileNameButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.BrowseForUnpackerPathFileNameButton.TabIndex = 12
		Me.BrowseForUnpackerPathFileNameButton.Text = "Browse..."
		Me.BrowseForUnpackerPathFileNameButton.UseVisualStyleBackColor = True
		'
		'PackerPathFileNameTextBox
		'
		Me.PackerPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackerPathFileNameTextBox.CueBannerText = ""
		Me.PackerPathFileNameTextBox.DetectUrls = False
		Me.PackerPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.PackerPathFileNameTextBox.Location = New System.Drawing.Point(102, 247)
		Me.PackerPathFileNameTextBox.Multiline = False
		Me.PackerPathFileNameTextBox.Name = "PackerPathFileNameTextBox"
		Me.PackerPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.PackerPathFileNameTextBox.SelectionEnabled = True
		Me.PackerPathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.PackerPathFileNameTextBox.TabIndex = 17
		Me.PackerPathFileNameTextBox.Text = ""
		Me.PackerPathFileNameTextBox.WordWrap = False
		'
		'ModelViewerLabel
		'
		Me.ModelViewerLabel.AutoSize = True
		Me.ModelViewerLabel.Location = New System.Drawing.Point(6, 193)
		Me.ModelViewerLabel.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.ModelViewerLabel.Name = "ModelViewerLabel"
		Me.ModelViewerLabel.Size = New System.Drawing.Size(79, 13)
		Me.ModelViewerLabel.TabIndex = 13
		Me.ModelViewerLabel.Text = "Model viewer:"
		'
		'BrowseForViewerPathFileNameButton
		'
		Me.BrowseForViewerPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForViewerPathFileNameButton.ButtonCanBeFocused = True
		Me.BrowseForViewerPathFileNameButton.Location = New System.Drawing.Point(697, 188)
		Me.BrowseForViewerPathFileNameButton.Name = "BrowseForViewerPathFileNameButton"
		Me.BrowseForViewerPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForViewerPathFileNameButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.BrowseForViewerPathFileNameButton.TabIndex = 9
		Me.BrowseForViewerPathFileNameButton.Text = "Browse..."
		Me.BrowseForViewerPathFileNameButton.UseVisualStyleBackColor = True
		'
		'ViewerPathFileNameTextBox
		'
		Me.ViewerPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ViewerPathFileNameTextBox.CueBannerText = ""
		Me.ViewerPathFileNameTextBox.DetectUrls = False
		Me.ViewerPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.ViewerPathFileNameTextBox.Location = New System.Drawing.Point(102, 189)
		Me.ViewerPathFileNameTextBox.Multiline = False
		Me.ViewerPathFileNameTextBox.Name = "ViewerPathFileNameTextBox"
		Me.ViewerPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.ViewerPathFileNameTextBox.SelectionEnabled = True
		Me.ViewerPathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.ViewerPathFileNameTextBox.TabIndex = 8
		Me.ViewerPathFileNameTextBox.Text = ""
		Me.ViewerPathFileNameTextBox.WordWrap = False
		'
		'CloneGameSetupButton
		'
		Me.CloneGameSetupButton.ButtonCanBeFocused = True
		Me.CloneGameSetupButton.Location = New System.Drawing.Point(6, 275)
		Me.CloneGameSetupButton.Name = "CloneGameSetupButton"
		Me.CloneGameSetupButton.Size = New System.Drawing.Size(75, 23)
		Me.CloneGameSetupButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.CloneGameSetupButton.TabIndex = 13
		Me.CloneGameSetupButton.Text = "Clone"
		Me.CloneGameSetupButton.UseVisualStyleBackColor = True
		'
		'GameNameTextBox
		'
		Me.GameNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameNameTextBox.CueBannerText = ""
		Me.GameNameTextBox.DetectUrls = False
		Me.GameNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GameNameTextBox.Location = New System.Drawing.Point(55, 17)
		Me.GameNameTextBox.Multiline = False
		Me.GameNameTextBox.Name = "GameNameTextBox"
		Me.GameNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.GameNameTextBox.SelectionEnabled = True
		Me.GameNameTextBox.Size = New System.Drawing.Size(717, 22)
		Me.GameNameTextBox.TabIndex = 5
		Me.GameNameTextBox.Text = ""
		Me.GameNameTextBox.WordWrap = False
		'
		'NameLabel
		'
		Me.NameLabel.AutoSize = True
		Me.NameLabel.Location = New System.Drawing.Point(6, 22)
		Me.NameLabel.Name = "NameLabel"
		Me.NameLabel.Size = New System.Drawing.Size(39, 13)
		Me.NameLabel.TabIndex = 4
		Me.NameLabel.Text = "Name:"
		'
		'DeleteGameSetupButton
		'
		Me.DeleteGameSetupButton.ButtonCanBeFocused = True
		Me.DeleteGameSetupButton.Location = New System.Drawing.Point(87, 275)
		Me.DeleteGameSetupButton.Name = "DeleteGameSetupButton"
		Me.DeleteGameSetupButton.Size = New System.Drawing.Size(75, 23)
		Me.DeleteGameSetupButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.DeleteGameSetupButton.TabIndex = 14
		Me.DeleteGameSetupButton.Text = "Delete"
		Me.DeleteGameSetupButton.UseVisualStyleBackColor = True
		'
		'BrowseForGamePathFileNameButton
		'
		Me.BrowseForGamePathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForGamePathFileNameButton.ButtonCanBeFocused = True
		Me.BrowseForGamePathFileNameButton.Location = New System.Drawing.Point(697, 130)
		Me.BrowseForGamePathFileNameButton.Name = "BrowseForGamePathFileNameButton"
		Me.BrowseForGamePathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForGamePathFileNameButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.BrowseForGamePathFileNameButton.TabIndex = 5
		Me.BrowseForGamePathFileNameButton.Text = "Browse..."
		Me.BrowseForGamePathFileNameButton.UseVisualStyleBackColor = True
		'
		'GamePathFileNameTextBox
		'
		Me.GamePathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GamePathFileNameTextBox.CueBannerText = ""
		Me.GamePathFileNameTextBox.DetectUrls = False
		Me.GamePathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GamePathFileNameTextBox.Location = New System.Drawing.Point(102, 131)
		Me.GamePathFileNameTextBox.Multiline = False
		Me.GamePathFileNameTextBox.Name = "GamePathFileNameTextBox"
		Me.GamePathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.GamePathFileNameTextBox.SelectionEnabled = True
		Me.GamePathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.GamePathFileNameTextBox.TabIndex = 4
		Me.GamePathFileNameTextBox.Text = ""
		Me.GamePathFileNameTextBox.WordWrap = False
		'
		'ModelCompilerLabel
		'
		Me.ModelCompilerLabel.AutoSize = True
		Me.ModelCompilerLabel.Location = New System.Drawing.Point(6, 164)
		Me.ModelCompilerLabel.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.ModelCompilerLabel.Name = "ModelCompilerLabel"
		Me.ModelCompilerLabel.Size = New System.Drawing.Size(90, 13)
		Me.ModelCompilerLabel.TabIndex = 5
		Me.ModelCompilerLabel.Text = "Model compiler:"
		'
		'BrowseForCompilerPathFileNameButton
		'
		Me.BrowseForCompilerPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForCompilerPathFileNameButton.ButtonCanBeFocused = True
		Me.BrowseForCompilerPathFileNameButton.Location = New System.Drawing.Point(697, 159)
		Me.BrowseForCompilerPathFileNameButton.Name = "BrowseForCompilerPathFileNameButton"
		Me.BrowseForCompilerPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForCompilerPathFileNameButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.BrowseForCompilerPathFileNameButton.TabIndex = 7
		Me.BrowseForCompilerPathFileNameButton.Text = "Browse..."
		Me.BrowseForCompilerPathFileNameButton.UseVisualStyleBackColor = True
		'
		'CompilerPathFileNameTextBox
		'
		Me.CompilerPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerPathFileNameTextBox.CueBannerText = ""
		Me.CompilerPathFileNameTextBox.DetectUrls = False
		Me.CompilerPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CompilerPathFileNameTextBox.Location = New System.Drawing.Point(102, 160)
		Me.CompilerPathFileNameTextBox.Multiline = False
		Me.CompilerPathFileNameTextBox.Name = "CompilerPathFileNameTextBox"
		Me.CompilerPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.CompilerPathFileNameTextBox.SelectionEnabled = True
		Me.CompilerPathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.CompilerPathFileNameTextBox.TabIndex = 6
		Me.CompilerPathFileNameTextBox.Text = ""
		Me.CompilerPathFileNameTextBox.WordWrap = False
		'
		'GamePathLabel
		'
		Me.GamePathLabel.AutoSize = True
		Me.GamePathLabel.Location = New System.Drawing.Point(6, 135)
		Me.GamePathLabel.Name = "GamePathLabel"
		Me.GamePathLabel.Size = New System.Drawing.Size(76, 13)
		Me.GamePathLabel.TabIndex = 2
		Me.GamePathLabel.Text = "GameInfo.txt:"
		'
		'GoBackButton
		'
		Me.GoBackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GoBackButton.ButtonCanBeFocused = True
		Me.GoBackButton.Enabled = False
		Me.GoBackButton.Location = New System.Drawing.Point(706, 520)
		Me.GoBackButton.Name = "GoBackButton"
		Me.GoBackButton.Size = New System.Drawing.Size(75, 23)
		Me.GoBackButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.GoBackButton.TabIndex = 7
		Me.GoBackButton.Text = "Go Back"
		Me.GoBackButton.UseVisualStyleBackColor = True
		'
		'SteamLibraryPathsDataGridView
		'
		Me.SteamLibraryPathsDataGridView.AllowUserToAddRows = False
		Me.SteamLibraryPathsDataGridView.AllowUserToDeleteRows = False
		Me.SteamLibraryPathsDataGridView.AllowUserToResizeRows = False
		Me.SteamLibraryPathsDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SteamLibraryPathsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
		Me.SteamLibraryPathsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.SteamLibraryPathsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
		Me.SteamLibraryPathsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.SteamLibraryPathsDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.SteamLibraryPathsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.SteamLibraryPathsDataGridView.EnableHeadersVisualStyles = False
		Me.SteamLibraryPathsDataGridView.Location = New System.Drawing.Point(3, 412)
		Me.SteamLibraryPathsDataGridView.MultiSelect = False
		Me.SteamLibraryPathsDataGridView.Name = "SteamLibraryPathsDataGridView"
		Me.SteamLibraryPathsDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.SteamLibraryPathsDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
		Me.SteamLibraryPathsDataGridView.RowHeadersVisible = False
		Me.SteamLibraryPathsDataGridView.RowHeadersWidth = 25
		Me.SteamLibraryPathsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
		Me.SteamLibraryPathsDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.SteamLibraryPathsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
		Me.SteamLibraryPathsDataGridView.Size = New System.Drawing.Size(604, 131)
		Me.SteamLibraryPathsDataGridView.TabIndex = 4
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.GameSetupComboUserControl)
		Me.Panel1.Controls.Add(Me.GoBackButton)
		Me.Panel1.Controls.Add(Me.AddLibraryPathButton)
		Me.Panel1.Controls.Add(Me.DeleteLibraryPathButton)
		Me.Panel1.Controls.Add(Me.SteamLibraryPathsDataGridView)
		Me.Panel1.Controls.Add(Me.Label11)
		Me.Panel1.Controls.Add(Me.Label10)
		Me.Panel1.Controls.Add(Me.BrowseForSteamAppPathFileNameButton)
		Me.Panel1.Controls.Add(Me.SteamAppPathFileNameTextBox)
		Me.Panel1.Controls.Add(Me.AddGameSetupButton)
		Me.Panel1.Controls.Add(Me.GameGroupBox)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(784, 546)
		Me.Panel1.TabIndex = 0
		'
		'GameSetupComboUserControl
		'
		Me.GameSetupComboUserControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboUserControl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GameSetupComboUserControl.IsReadOnly = True
		Me.GameSetupComboUserControl.Location = New System.Drawing.Point(3, 3)
		Me.GameSetupComboUserControl.MaxDropDownItems = 30
		Me.GameSetupComboUserControl.MultipleInputsIsAllowed = False
		Me.GameSetupComboUserControl.Name = "GameSetupComboUserControl"
		Me.GameSetupComboUserControl.Size = New System.Drawing.Size(697, 22)
		Me.GameSetupComboUserControl.TabIndex = 0
		Me.GameSetupComboUserControl.TextHistoryIsKept = False
		Me.GameSetupComboUserControl.TextHistoryMaxSize = 15
		Me.GameSetupComboUserControl.TextIsPathFileNames = False
		'
		'SetUpGamesUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Name = "SetUpGamesUserControl"
		Me.Size = New System.Drawing.Size(784, 546)
		Me.GameGroupBox.ResumeLayout(False)
		Me.GameGroupBox.PerformLayout()
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel.ResumeLayout(False)
		CType(Me.SteamLibraryPathsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents AddLibraryPathButton As ButtonEx
	Friend WithEvents DeleteLibraryPathButton As ButtonEx
	Friend WithEvents SteamLibraryPathsDataGridView As Crowbar.MacroDataGridView
	Friend WithEvents Label11 As Crowbar.LabelEx
	Friend WithEvents Label10 As Crowbar.LabelEx
	Friend WithEvents BrowseForSteamAppPathFileNameButton As ButtonEx
	Friend WithEvents SteamAppPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents AddGameSetupButton As ButtonEx
	Friend WithEvents GameGroupBox As GroupBoxEx
	Friend WithEvents CreateModelsFolderTreeButton As ButtonEx
	Friend WithEvents BrowseForMappingToolPathFileNameButton As ButtonEx
	Friend WithEvents MappingToolPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents MappingToolLabel As Crowbar.LabelEx
	Friend WithEvents GameAppOptionsTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents ExecutableOptionsLabel As Crowbar.LabelEx
	Friend WithEvents ClearGameAppOptionsButton As ButtonEx
	Friend WithEvents BrowseForGameAppPathFileNameButton As ButtonEx
	Friend WithEvents GameAppPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents ExecutableLabel As Crowbar.LabelEx
	Friend WithEvents PackerLabel As Crowbar.LabelEx
	Friend WithEvents BrowseForUnpackerPathFileNameButton As ButtonEx
	Friend WithEvents PackerPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents ModelViewerLabel As Crowbar.LabelEx
	Friend WithEvents BrowseForViewerPathFileNameButton As ButtonEx
	Friend WithEvents ViewerPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CloneGameSetupButton As ButtonEx
	Friend WithEvents GameNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents NameLabel As Crowbar.LabelEx
	Friend WithEvents DeleteGameSetupButton As ButtonEx
	Friend WithEvents BrowseForGamePathFileNameButton As ButtonEx
	Friend WithEvents GamePathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents ModelCompilerLabel As Crowbar.LabelEx
	Friend WithEvents BrowseForCompilerPathFileNameButton As ButtonEx
	Friend WithEvents CompilerPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents GamePathLabel As Crowbar.LabelEx
	Friend WithEvents EngineComboUserControl As ComboUserControl
	Friend WithEvents EngineLabel As Crowbar.LabelEx
	Friend WithEvents GoBackButton As ButtonEx
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents Panel1 As PanelEx
	Friend WithEvents GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel As Panel
	Friend WithEvents GameSetupComboUserControl As ComboUserControl
End Class
