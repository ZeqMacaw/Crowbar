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
		Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.AddLibraryPathButton = New Crowbar.ButtonEx()
		Me.DeleteLibraryPathButton = New Crowbar.ButtonEx()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.BrowseForSteamAppPathFileNameButton = New Crowbar.ButtonEx()
		Me.SteamAppPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.AddGameSetupButton = New Crowbar.ButtonEx()
		Me.GameSetupComboBox = New Crowbar.ComboBoxEx()
		Me.GameGroupBox = New Crowbar.GroupBoxEx()
		Me.EngineLabel = New System.Windows.Forms.Label()
		Me.EngineComboBox = New Crowbar.ComboBoxEx()
		Me.CreateModelsFolderTreeButton = New Crowbar.ButtonEx()
		Me.BrowseForMappingToolPathFileNameButton = New Crowbar.ButtonEx()
		Me.MappingToolPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.MappingToolLabel = New System.Windows.Forms.Label()
		Me.ExecutableOptionsLabel = New System.Windows.Forms.Label()
		Me.ClearGameAppOptionsButton = New Crowbar.ButtonEx()
		Me.BrowseForGameAppPathFileNameButton = New Crowbar.ButtonEx()
		Me.GameAppPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.ExecutableLabel = New System.Windows.Forms.Label()
		Me.PackerLabel = New System.Windows.Forms.Label()
		Me.BrowseForUnpackerPathFileNameButton = New Crowbar.ButtonEx()
		Me.PackerPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.ModelViewerLabel = New System.Windows.Forms.Label()
		Me.BrowseForViewerPathFileNameButton = New Crowbar.ButtonEx()
		Me.ViewerPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.CloneGameSetupButton = New Crowbar.ButtonEx()
		Me.GameNameTextBox = New Crowbar.RichTextBoxEx()
		Me.NameLabel = New System.Windows.Forms.Label()
		Me.DeleteGameSetupButton = New Crowbar.ButtonEx()
		Me.BrowseForGamePathFileNameButton = New Crowbar.ButtonEx()
		Me.GamePathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.ModelCompilerLabel = New System.Windows.Forms.Label()
		Me.BrowseForCompilerPathFileNameButton = New Crowbar.ButtonEx()
		Me.CompilerPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.GamePathLabel = New System.Windows.Forms.Label()
		Me.GameAppOptionsTextBox = New Crowbar.RichTextBoxEx()
		Me.GoBackButton = New Crowbar.ButtonEx()
		Me.SteamLibraryPathsDataGridView = New Crowbar.MacroDataGridView()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.Panel1 = New Crowbar.PanelEx()
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel = New System.Windows.Forms.Panel()
		Me.GameGroupBox.SuspendLayout()
		CType(Me.SteamLibraryPathsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel1.SuspendLayout()
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel.SuspendLayout()
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
		Me.SteamAppPathFileNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.SteamAppPathFileNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.SteamAppPathFileNameTextBox.CueBannerText = ""
		Me.SteamAppPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.SteamAppPathFileNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.SteamAppPathFileNameTextBox.Location = New System.Drawing.Point(3, 365)
		Me.SteamAppPathFileNameTextBox.Multiline = False
		Me.SteamAppPathFileNameTextBox.Name = "SteamAppPathFileNameTextBox"
		Me.SteamAppPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.SteamAppPathFileNameTextBox.Size = New System.Drawing.Size(604, 22)
		Me.SteamAppPathFileNameTextBox.TabIndex = 46
		Me.SteamAppPathFileNameTextBox.Text = ""
		Me.SteamAppPathFileNameTextBox.WordWrap = False
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
		Me.GameSetupComboBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
		Me.GameSetupComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.IsReadOnly = False
		Me.GameSetupComboBox.Location = New System.Drawing.Point(3, 3)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(697, 23)
		Me.GameSetupComboBox.TabIndex = 42
		'
		'GameGroupBox
		'
		Me.GameGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameGroupBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.GameGroupBox.Controls.Add(Me.EngineLabel)
		Me.GameGroupBox.Controls.Add(Me.EngineComboBox)
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
		Me.GameGroupBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.GameGroupBox.IsReadOnly = False
		Me.GameGroupBox.Location = New System.Drawing.Point(3, 32)
		Me.GameGroupBox.Name = "GameGroupBox"
		Me.GameGroupBox.SelectedValue = Nothing
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
		Me.EngineComboBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
		Me.EngineComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.EngineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.EngineComboBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.EngineComboBox.FormattingEnabled = True
		Me.EngineComboBox.IsReadOnly = False
		Me.EngineComboBox.Items.AddRange(New Object() {"GoldSource", "Source"})
		Me.EngineComboBox.Location = New System.Drawing.Point(55, 45)
		Me.EngineComboBox.Name = "EngineComboBox"
		Me.EngineComboBox.Size = New System.Drawing.Size(121, 23)
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
		Me.MappingToolPathFileNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.MappingToolPathFileNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.MappingToolPathFileNameTextBox.CueBannerText = ""
		Me.MappingToolPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.MappingToolPathFileNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.MappingToolPathFileNameTextBox.Location = New System.Drawing.Point(102, 218)
		Me.MappingToolPathFileNameTextBox.Multiline = False
		Me.MappingToolPathFileNameTextBox.Name = "MappingToolPathFileNameTextBox"
		Me.MappingToolPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.MappingToolPathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.MappingToolPathFileNameTextBox.TabIndex = 38
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
		Me.GameAppPathFileNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.GameAppPathFileNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.GameAppPathFileNameTextBox.CueBannerText = ""
		Me.GameAppPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GameAppPathFileNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.GameAppPathFileNameTextBox.Location = New System.Drawing.Point(112, 73)
		Me.GameAppPathFileNameTextBox.Multiline = False
		Me.GameAppPathFileNameTextBox.Name = "GameAppPathFileNameTextBox"
		Me.GameAppPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.GameAppPathFileNameTextBox.Size = New System.Drawing.Size(579, 22)
		Me.GameAppPathFileNameTextBox.TabIndex = 29
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
		Me.PackerPathFileNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.PackerPathFileNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.PackerPathFileNameTextBox.CueBannerText = ""
		Me.PackerPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.PackerPathFileNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.PackerPathFileNameTextBox.Location = New System.Drawing.Point(102, 247)
		Me.PackerPathFileNameTextBox.Multiline = False
		Me.PackerPathFileNameTextBox.Name = "PackerPathFileNameTextBox"
		Me.PackerPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
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
		Me.ViewerPathFileNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.ViewerPathFileNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.ViewerPathFileNameTextBox.CueBannerText = ""
		Me.ViewerPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.ViewerPathFileNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.ViewerPathFileNameTextBox.Location = New System.Drawing.Point(102, 189)
		Me.ViewerPathFileNameTextBox.Multiline = False
		Me.ViewerPathFileNameTextBox.Name = "ViewerPathFileNameTextBox"
		Me.ViewerPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.ViewerPathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.ViewerPathFileNameTextBox.TabIndex = 14
		Me.ViewerPathFileNameTextBox.Text = ""
		Me.ViewerPathFileNameTextBox.WordWrap = False
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
		Me.GameNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.GameNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.GameNameTextBox.CueBannerText = ""
		Me.GameNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GameNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.GameNameTextBox.Location = New System.Drawing.Point(55, 17)
		Me.GameNameTextBox.Multiline = False
		Me.GameNameTextBox.Name = "GameNameTextBox"
		Me.GameNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.GameNameTextBox.Size = New System.Drawing.Size(717, 22)
		Me.GameNameTextBox.TabIndex = 1
		Me.GameNameTextBox.Text = ""
		Me.GameNameTextBox.WordWrap = False
		'
		'NameLabel
		'
		Me.NameLabel.AutoSize = True
		Me.NameLabel.Location = New System.Drawing.Point(6, 22)
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
		Me.GamePathFileNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.GamePathFileNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.GamePathFileNameTextBox.CueBannerText = ""
		Me.GamePathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GamePathFileNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.GamePathFileNameTextBox.Location = New System.Drawing.Point(102, 131)
		Me.GamePathFileNameTextBox.Multiline = False
		Me.GamePathFileNameTextBox.Name = "GamePathFileNameTextBox"
		Me.GamePathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.GamePathFileNameTextBox.Size = New System.Drawing.Size(589, 22)
		Me.GamePathFileNameTextBox.TabIndex = 3
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
		Me.CompilerPathFileNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.CompilerPathFileNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.CompilerPathFileNameTextBox.CueBannerText = ""
		Me.CompilerPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CompilerPathFileNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.CompilerPathFileNameTextBox.Location = New System.Drawing.Point(102, 160)
		Me.CompilerPathFileNameTextBox.Multiline = False
		Me.CompilerPathFileNameTextBox.Name = "CompilerPathFileNameTextBox"
		Me.CompilerPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
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
		'GameAppOptionsTextBox
		'
		Me.GameAppOptionsTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.GameAppOptionsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.GameAppOptionsTextBox.CueBannerText = ""
		Me.GameAppOptionsTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.GameAppOptionsTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GameAppOptionsTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.GameAppOptionsTextBox.Location = New System.Drawing.Point(0, 0)
		Me.GameAppOptionsTextBox.Multiline = False
		Me.GameAppOptionsTextBox.Name = "GameAppOptionsTextBox"
		Me.GameAppOptionsTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.GameAppOptionsTextBox.Size = New System.Drawing.Size(579, 22)
		Me.GameAppOptionsTextBox.TabIndex = 32
		Me.GameAppOptionsTextBox.Text = ""
		Me.GameAppOptionsTextBox.WordWrap = False
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
		Me.SteamLibraryPathsDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.SteamLibraryPathsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.SteamLibraryPathsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
		Me.SteamLibraryPathsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
		DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(124, Byte), Integer), CType(CType(16, Byte), Integer))
		DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.SteamLibraryPathsDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
		Me.SteamLibraryPathsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(124, Byte), Integer), CType(CType(16, Byte), Integer))
		DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.SteamLibraryPathsDataGridView.DefaultCellStyle = DataGridViewCellStyle5
		Me.SteamLibraryPathsDataGridView.EnableHeadersVisualStyles = False
		Me.SteamLibraryPathsDataGridView.GridColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
		Me.SteamLibraryPathsDataGridView.Location = New System.Drawing.Point(3, 412)
		Me.SteamLibraryPathsDataGridView.MultiSelect = False
		Me.SteamLibraryPathsDataGridView.Name = "SteamLibraryPathsDataGridView"
		Me.SteamLibraryPathsDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
		DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(124, Byte), Integer), CType(CType(16, Byte), Integer))
		DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.SteamLibraryPathsDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
		Me.SteamLibraryPathsDataGridView.RowHeadersVisible = False
		Me.SteamLibraryPathsDataGridView.RowHeadersWidth = 25
		Me.SteamLibraryPathsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
		Me.SteamLibraryPathsDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.SteamLibraryPathsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
		Me.SteamLibraryPathsDataGridView.Size = New System.Drawing.Size(604, 131)
		Me.SteamLibraryPathsDataGridView.TabIndex = 49
		'
		'Panel1
		'
		Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
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
		Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.SelectedIndex = -1
		Me.Panel1.SelectedValue = Nothing
		Me.Panel1.Size = New System.Drawing.Size(784, 546)
		Me.Panel1.TabIndex = 17
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
		Me.GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents AddLibraryPathButton As ButtonEx
	Friend WithEvents DeleteLibraryPathButton As ButtonEx
	Friend WithEvents SteamLibraryPathsDataGridView As Crowbar.MacroDataGridView
	Friend WithEvents Label11 As System.Windows.Forms.Label
	Friend WithEvents Label10 As System.Windows.Forms.Label
	Friend WithEvents BrowseForSteamAppPathFileNameButton As ButtonEx
	Friend WithEvents SteamAppPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents AddGameSetupButton As ButtonEx
	Friend WithEvents GameSetupComboBox As ComboBoxEx
	Friend WithEvents GameGroupBox As GroupBoxEx
	Friend WithEvents CreateModelsFolderTreeButton As ButtonEx
	Friend WithEvents BrowseForMappingToolPathFileNameButton As ButtonEx
	Friend WithEvents MappingToolPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents MappingToolLabel As System.Windows.Forms.Label
	Friend WithEvents GameAppOptionsTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents ExecutableOptionsLabel As System.Windows.Forms.Label
	Friend WithEvents ClearGameAppOptionsButton As ButtonEx
	Friend WithEvents BrowseForGameAppPathFileNameButton As ButtonEx
	Friend WithEvents GameAppPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents ExecutableLabel As System.Windows.Forms.Label
	Friend WithEvents PackerLabel As System.Windows.Forms.Label
	Friend WithEvents BrowseForUnpackerPathFileNameButton As ButtonEx
	Friend WithEvents PackerPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents ModelViewerLabel As System.Windows.Forms.Label
	Friend WithEvents BrowseForViewerPathFileNameButton As ButtonEx
	Friend WithEvents ViewerPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CloneGameSetupButton As ButtonEx
	Friend WithEvents GameNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents NameLabel As System.Windows.Forms.Label
	Friend WithEvents DeleteGameSetupButton As ButtonEx
	Friend WithEvents BrowseForGamePathFileNameButton As ButtonEx
	Friend WithEvents GamePathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents ModelCompilerLabel As System.Windows.Forms.Label
	Friend WithEvents BrowseForCompilerPathFileNameButton As ButtonEx
	Friend WithEvents CompilerPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents GamePathLabel As System.Windows.Forms.Label
	Friend WithEvents EngineComboBox As ComboBoxEx
	Friend WithEvents EngineLabel As System.Windows.Forms.Label
	Friend WithEvents GoBackButton As ButtonEx
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents Panel1 As PanelEx
	Friend WithEvents GameAppOptionsRichTextBoxExIncorrectTextSpacingWorkaroundPanel As Panel
End Class
