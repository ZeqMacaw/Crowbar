<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetUpGamesUserControl
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
		Me.AddLibraryPathButton = New System.Windows.Forms.Button()
		Me.DeleteLibraryPathButton = New System.Windows.Forms.Button()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.BrowseForSteamAppPathFileNameButton = New System.Windows.Forms.Button()
		Me.SteamAppPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.AddGameSetupButton = New System.Windows.Forms.Button()
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox()
		Me.GameGroupBox = New System.Windows.Forms.GroupBox()
		Me.EngineLabel = New System.Windows.Forms.Label()
		Me.EngineComboBox = New System.Windows.Forms.ComboBox()
		Me.CreateModelsFolderTreeButton = New System.Windows.Forms.Button()
		Me.BrowseForMappingToolPathFileNameButton = New System.Windows.Forms.Button()
		Me.MappingToolPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.MappingToolLabel = New System.Windows.Forms.Label()
		Me.GameAppOptionsTextBox = New System.Windows.Forms.TextBox()
		Me.ExecutableOptionsLabel = New System.Windows.Forms.Label()
		Me.ClearGameAppOptionsButton = New System.Windows.Forms.Button()
		Me.BrowseForGameAppPathFileNameButton = New System.Windows.Forms.Button()
		Me.GameAppPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.ExecutableLabel = New System.Windows.Forms.Label()
		Me.PackerLabel = New System.Windows.Forms.Label()
		Me.BrowseForUnpackerPathFileNameButton = New System.Windows.Forms.Button()
		Me.PackerPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.ModelViewerLabel = New System.Windows.Forms.Label()
		Me.BrowseForViewerPathFileNameButton = New System.Windows.Forms.Button()
		Me.ViewerPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.CloneGameSetupButton = New System.Windows.Forms.Button()
		Me.GameNameTextBox = New Crowbar.TextBoxEx()
		Me.NameLabel = New System.Windows.Forms.Label()
		Me.DeleteGameSetupButton = New System.Windows.Forms.Button()
		Me.BrowseForGamePathFileNameButton = New System.Windows.Forms.Button()
		Me.GamePathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.ModelCompilerLabel = New System.Windows.Forms.Label()
		Me.BrowseForCompilerPathFileNameButton = New System.Windows.Forms.Button()
		Me.CompilerPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.GamePathLabel = New System.Windows.Forms.Label()
		Me.GoBackButton = New System.Windows.Forms.Button()
		Me.SteamLibraryPathsDataGridView = New Crowbar.MacroDataGridView()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.GameGroupBox.SuspendLayout()
		CType(Me.SteamLibraryPathsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'AddLibraryPathButton
		'
		Me.AddLibraryPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.AddLibraryPathButton.Location = New System.Drawing.Point(613, 412)
		Me.AddLibraryPathButton.Name = "AddLibraryPathButton"
		Me.AddLibraryPathButton.Size = New System.Drawing.Size(75, 23)
		Me.AddLibraryPathButton.TabIndex = 51
		Me.AddLibraryPathButton.Text = "Add Macro"
		Me.AddLibraryPathButton.UseVisualStyleBackColor = True
		'
		'DeleteLibraryPathButton
		'
		Me.DeleteLibraryPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DeleteLibraryPathButton.Location = New System.Drawing.Point(613, 441)
		Me.DeleteLibraryPathButton.Name = "DeleteLibraryPathButton"
		Me.DeleteLibraryPathButton.Size = New System.Drawing.Size(75, 50)
		Me.DeleteLibraryPathButton.TabIndex = 50
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
		Me.BrowseForSteamAppPathFileNameButton.Location = New System.Drawing.Point(613, 364)
		Me.BrowseForSteamAppPathFileNameButton.Name = "BrowseForSteamAppPathFileNameButton"
		Me.BrowseForSteamAppPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForSteamAppPathFileNameButton.TabIndex = 47
		Me.BrowseForSteamAppPathFileNameButton.Text = "Browse..."
		Me.BrowseForSteamAppPathFileNameButton.UseVisualStyleBackColor = True
		'
		'SteamAppPathFileNameTextBox
		'
		Me.SteamAppPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SteamAppPathFileNameTextBox.Location = New System.Drawing.Point(3, 364)
		Me.SteamAppPathFileNameTextBox.Name = "SteamAppPathFileNameTextBox"
		Me.SteamAppPathFileNameTextBox.Size = New System.Drawing.Size(604, 22)
		Me.SteamAppPathFileNameTextBox.TabIndex = 46
		'
		'AddGameSetupButton
		'
		Me.AddGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.AddGameSetupButton.Location = New System.Drawing.Point(706, 3)
		Me.AddGameSetupButton.Name = "AddGameSetupButton"
		Me.AddGameSetupButton.Size = New System.Drawing.Size(75, 23)
		Me.AddGameSetupButton.TabIndex = 43
		Me.AddGameSetupButton.Text = "Add"
		Me.AddGameSetupButton.UseVisualStyleBackColor = True
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(3, 4)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(697, 21)
		Me.GameSetupComboBox.TabIndex = 42
		'
		'GameGroupBox
		'
		Me.GameGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameGroupBox.Controls.Add(Me.EngineLabel)
		Me.GameGroupBox.Controls.Add(Me.EngineComboBox)
		Me.GameGroupBox.Controls.Add(Me.CreateModelsFolderTreeButton)
		Me.GameGroupBox.Controls.Add(Me.BrowseForMappingToolPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.MappingToolPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.MappingToolLabel)
		Me.GameGroupBox.Controls.Add(Me.GameAppOptionsTextBox)
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
		Me.GameGroupBox.Location = New System.Drawing.Point(3, 32)
		Me.GameGroupBox.Name = "GameGroupBox"
		Me.GameGroupBox.Size = New System.Drawing.Size(778, 304)
		Me.GameGroupBox.TabIndex = 44
		Me.GameGroupBox.TabStop = False
		Me.GameGroupBox.Text = "Game Setup"
		'
		'EngineLabel
		'
		Me.EngineLabel.AutoSize = True
		Me.EngineLabel.Location = New System.Drawing.Point(6, 49)
		Me.EngineLabel.Name = "EngineLabel"
		Me.EngineLabel.Size = New System.Drawing.Size(46, 13)
		Me.EngineLabel.TabIndex = 43
		Me.EngineLabel.Text = "Engine:"
		'
		'EngineComboBox
		'
		Me.EngineComboBox.FormattingEnabled = True
		Me.EngineComboBox.Items.AddRange(New Object() {"GoldSource", "Source"})
		Me.EngineComboBox.Location = New System.Drawing.Point(55, 45)
		Me.EngineComboBox.Name = "EngineComboBox"
		Me.EngineComboBox.Size = New System.Drawing.Size(121, 21)
		Me.EngineComboBox.TabIndex = 42
		'
		'CreateModelsFolderTreeButton
		'
		Me.CreateModelsFolderTreeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CreateModelsFolderTreeButton.Location = New System.Drawing.Point(502, 275)
		Me.CreateModelsFolderTreeButton.Name = "CreateModelsFolderTreeButton"
		Me.CreateModelsFolderTreeButton.Size = New System.Drawing.Size(270, 23)
		Me.CreateModelsFolderTreeButton.TabIndex = 40
		Me.CreateModelsFolderTreeButton.Text = "Create ""models"" folder tree from this game's VPKs"
		Me.ToolTip1.SetToolTip(Me.CreateModelsFolderTreeButton, "Use this so HLMV can view models found in VPKs.")
		Me.CreateModelsFolderTreeButton.UseVisualStyleBackColor = True
		'
		'BrowseForMappingToolPathFileNameButton
		'
		Me.BrowseForMappingToolPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForMappingToolPathFileNameButton.Location = New System.Drawing.Point(697, 217)
		Me.BrowseForMappingToolPathFileNameButton.Name = "BrowseForMappingToolPathFileNameButton"
		Me.BrowseForMappingToolPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForMappingToolPathFileNameButton.TabIndex = 39
		Me.BrowseForMappingToolPathFileNameButton.Text = "Browse..."
		Me.BrowseForMappingToolPathFileNameButton.UseVisualStyleBackColor = True
		'
		'MappingToolPathFileNameTextBox
		'
		Me.MappingToolPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MappingToolPathFileNameTextBox.Location = New System.Drawing.Point(102, 217)
		Me.MappingToolPathFileNameTextBox.Name = "MappingToolPathFileNameTextBox"
		Me.MappingToolPathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.MappingToolPathFileNameTextBox.TabIndex = 38
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
		'GameAppOptionsTextBox
		'
		Me.GameAppOptionsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameAppOptionsTextBox.Location = New System.Drawing.Point(112, 101)
		Me.GameAppOptionsTextBox.Name = "GameAppOptionsTextBox"
		Me.GameAppOptionsTextBox.Size = New System.Drawing.Size(579, 22)
		Me.GameAppOptionsTextBox.TabIndex = 32
		'
		'ExecutableOptionsLabel
		'
		Me.ExecutableOptionsLabel.AutoSize = True
		Me.ExecutableOptionsLabel.Location = New System.Drawing.Point(6, 106)
		Me.ExecutableOptionsLabel.Name = "ExecutableOptionsLabel"
		Me.ExecutableOptionsLabel.Size = New System.Drawing.Size(108, 13)
		Me.ExecutableOptionsLabel.TabIndex = 31
		Me.ExecutableOptionsLabel.Text = "Executable options:"
		'
		'ClearGameAppOptionsButton
		'
		Me.ClearGameAppOptionsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ClearGameAppOptionsButton.Location = New System.Drawing.Point(697, 101)
		Me.ClearGameAppOptionsButton.Name = "ClearGameAppOptionsButton"
		Me.ClearGameAppOptionsButton.Size = New System.Drawing.Size(75, 23)
		Me.ClearGameAppOptionsButton.TabIndex = 33
		Me.ClearGameAppOptionsButton.Text = "Clear"
		Me.ClearGameAppOptionsButton.UseVisualStyleBackColor = True
		'
		'BrowseForGameAppPathFileNameButton
		'
		Me.BrowseForGameAppPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForGameAppPathFileNameButton.Location = New System.Drawing.Point(697, 72)
		Me.BrowseForGameAppPathFileNameButton.Name = "BrowseForGameAppPathFileNameButton"
		Me.BrowseForGameAppPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForGameAppPathFileNameButton.TabIndex = 30
		Me.BrowseForGameAppPathFileNameButton.Text = "Browse..."
		Me.BrowseForGameAppPathFileNameButton.UseVisualStyleBackColor = True
		'
		'GameAppPathFileNameTextBox
		'
		Me.GameAppPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameAppPathFileNameTextBox.Location = New System.Drawing.Point(112, 72)
		Me.GameAppPathFileNameTextBox.Name = "GameAppPathFileNameTextBox"
		Me.GameAppPathFileNameTextBox.Size = New System.Drawing.Size(579, 22)
		Me.GameAppPathFileNameTextBox.TabIndex = 29
		'
		'ExecutableLabel
		'
		Me.ExecutableLabel.AutoSize = True
		Me.ExecutableLabel.Location = New System.Drawing.Point(6, 77)
		Me.ExecutableLabel.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.ExecutableLabel.Name = "ExecutableLabel"
		Me.ExecutableLabel.Size = New System.Drawing.Size(99, 13)
		Me.ExecutableLabel.TabIndex = 28
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
		Me.BrowseForUnpackerPathFileNameButton.Location = New System.Drawing.Point(697, 246)
		Me.BrowseForUnpackerPathFileNameButton.Name = "BrowseForUnpackerPathFileNameButton"
		Me.BrowseForUnpackerPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForUnpackerPathFileNameButton.TabIndex = 18
		Me.BrowseForUnpackerPathFileNameButton.Text = "Browse..."
		Me.BrowseForUnpackerPathFileNameButton.UseVisualStyleBackColor = True
		'
		'PackerPathFileNameTextBox
		'
		Me.PackerPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackerPathFileNameTextBox.Location = New System.Drawing.Point(102, 246)
		Me.PackerPathFileNameTextBox.Name = "PackerPathFileNameTextBox"
		Me.PackerPathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.PackerPathFileNameTextBox.TabIndex = 17
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
		Me.BrowseForViewerPathFileNameButton.Location = New System.Drawing.Point(697, 188)
		Me.BrowseForViewerPathFileNameButton.Name = "BrowseForViewerPathFileNameButton"
		Me.BrowseForViewerPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForViewerPathFileNameButton.TabIndex = 15
		Me.BrowseForViewerPathFileNameButton.Text = "Browse..."
		Me.BrowseForViewerPathFileNameButton.UseVisualStyleBackColor = True
		'
		'ViewerPathFileNameTextBox
		'
		Me.ViewerPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ViewerPathFileNameTextBox.Location = New System.Drawing.Point(102, 188)
		Me.ViewerPathFileNameTextBox.Name = "ViewerPathFileNameTextBox"
		Me.ViewerPathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.ViewerPathFileNameTextBox.TabIndex = 14
		'
		'CloneGameSetupButton
		'
		Me.CloneGameSetupButton.Location = New System.Drawing.Point(6, 275)
		Me.CloneGameSetupButton.Name = "CloneGameSetupButton"
		Me.CloneGameSetupButton.Size = New System.Drawing.Size(75, 23)
		Me.CloneGameSetupButton.TabIndex = 12
		Me.CloneGameSetupButton.Text = "Clone"
		Me.CloneGameSetupButton.UseVisualStyleBackColor = True
		'
		'GameNameTextBox
		'
		Me.GameNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameNameTextBox.CueBannerText = ""
		Me.GameNameTextBox.Location = New System.Drawing.Point(55, 19)
		Me.GameNameTextBox.Name = "GameNameTextBox"
		Me.GameNameTextBox.Size = New System.Drawing.Size(717, 22)
		Me.GameNameTextBox.TabIndex = 1
		'
		'NameLabel
		'
		Me.NameLabel.AutoSize = True
		Me.NameLabel.Location = New System.Drawing.Point(6, 24)
		Me.NameLabel.Name = "NameLabel"
		Me.NameLabel.Size = New System.Drawing.Size(39, 13)
		Me.NameLabel.TabIndex = 0
		Me.NameLabel.Text = "Name:"
		'
		'DeleteGameSetupButton
		'
		Me.DeleteGameSetupButton.Location = New System.Drawing.Point(87, 275)
		Me.DeleteGameSetupButton.Name = "DeleteGameSetupButton"
		Me.DeleteGameSetupButton.Size = New System.Drawing.Size(75, 23)
		Me.DeleteGameSetupButton.TabIndex = 8
		Me.DeleteGameSetupButton.Text = "Delete"
		Me.DeleteGameSetupButton.UseVisualStyleBackColor = True
		'
		'BrowseForGamePathFileNameButton
		'
		Me.BrowseForGamePathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForGamePathFileNameButton.Location = New System.Drawing.Point(697, 130)
		Me.BrowseForGamePathFileNameButton.Name = "BrowseForGamePathFileNameButton"
		Me.BrowseForGamePathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForGamePathFileNameButton.TabIndex = 4
		Me.BrowseForGamePathFileNameButton.Text = "Browse..."
		Me.BrowseForGamePathFileNameButton.UseVisualStyleBackColor = True
		'
		'GamePathFileNameTextBox
		'
		Me.GamePathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GamePathFileNameTextBox.Location = New System.Drawing.Point(102, 130)
		Me.GamePathFileNameTextBox.Name = "GamePathFileNameTextBox"
		Me.GamePathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.GamePathFileNameTextBox.TabIndex = 3
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
		Me.BrowseForCompilerPathFileNameButton.Location = New System.Drawing.Point(697, 159)
		Me.BrowseForCompilerPathFileNameButton.Name = "BrowseForCompilerPathFileNameButton"
		Me.BrowseForCompilerPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForCompilerPathFileNameButton.TabIndex = 7
		Me.BrowseForCompilerPathFileNameButton.Text = "Browse..."
		Me.BrowseForCompilerPathFileNameButton.UseVisualStyleBackColor = True
		'
		'CompilerPathFileNameTextBox
		'
		Me.CompilerPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerPathFileNameTextBox.Location = New System.Drawing.Point(102, 159)
		Me.CompilerPathFileNameTextBox.Name = "CompilerPathFileNameTextBox"
		Me.CompilerPathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.CompilerPathFileNameTextBox.TabIndex = 6
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
		Me.GoBackButton.Enabled = False
		Me.GoBackButton.Location = New System.Drawing.Point(706, 520)
		Me.GoBackButton.Name = "GoBackButton"
		Me.GoBackButton.Size = New System.Drawing.Size(75, 23)
		Me.GoBackButton.TabIndex = 52
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
		Me.SteamLibraryPathsDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlDark
		Me.SteamLibraryPathsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
		Me.SteamLibraryPathsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.SteamLibraryPathsDataGridView.Location = New System.Drawing.Point(3, 412)
		Me.SteamLibraryPathsDataGridView.MultiSelect = False
		Me.SteamLibraryPathsDataGridView.Name = "SteamLibraryPathsDataGridView"
		Me.SteamLibraryPathsDataGridView.RowHeadersVisible = False
		Me.SteamLibraryPathsDataGridView.RowHeadersWidth = 25
		Me.SteamLibraryPathsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
		Me.SteamLibraryPathsDataGridView.Size = New System.Drawing.Size(604, 131)
		Me.SteamLibraryPathsDataGridView.TabIndex = 49
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.GoBackButton)
		Me.Panel1.Controls.Add(Me.GameSetupComboBox)
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
		Me.Panel1.TabIndex = 17
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
		CType(Me.SteamLibraryPathsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents AddLibraryPathButton As System.Windows.Forms.Button
	Friend WithEvents DeleteLibraryPathButton As System.Windows.Forms.Button
	Friend WithEvents SteamLibraryPathsDataGridView As Crowbar.MacroDataGridView
	Friend WithEvents Label11 As System.Windows.Forms.Label
	Friend WithEvents Label10 As System.Windows.Forms.Label
	Friend WithEvents BrowseForSteamAppPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents SteamAppPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents AddGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GameGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents CreateModelsFolderTreeButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForMappingToolPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents MappingToolPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents MappingToolLabel As System.Windows.Forms.Label
	Friend WithEvents GameAppOptionsTextBox As System.Windows.Forms.TextBox
	Friend WithEvents ExecutableOptionsLabel As System.Windows.Forms.Label
	Friend WithEvents ClearGameAppOptionsButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForGameAppPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents GameAppPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents ExecutableLabel As System.Windows.Forms.Label
	Friend WithEvents PackerLabel As System.Windows.Forms.Label
	Friend WithEvents BrowseForUnpackerPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents PackerPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents ModelViewerLabel As System.Windows.Forms.Label
	Friend WithEvents BrowseForViewerPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents ViewerPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents CloneGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents GameNameTextBox As Crowbar.TextBoxEx
	Friend WithEvents NameLabel As System.Windows.Forms.Label
	Friend WithEvents DeleteGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForGamePathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents GamePathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents ModelCompilerLabel As System.Windows.Forms.Label
	Friend WithEvents BrowseForCompilerPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents CompilerPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents GamePathLabel As System.Windows.Forms.Label
	Friend WithEvents EngineComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents EngineLabel As System.Windows.Forms.Label
	Friend WithEvents GoBackButton As System.Windows.Forms.Button
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents Panel1 As Panel
End Class
