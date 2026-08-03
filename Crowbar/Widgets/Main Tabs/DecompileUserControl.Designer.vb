<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DecompileUserControl
	Inherits BaseUserControl

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.DebugInfoCheckBox = New Crowbar.CheckBoxEx()
		Me.LogFileCheckBox = New Crowbar.CheckBoxEx()
		Me.DeclareSequenceQciCheckBox = New Crowbar.CheckBoxEx()
		Me.FormatForStricterImportersCheckBox = New Crowbar.CheckBoxEx()
		Me.UseMixedCaseForKeywordsCheckBox = New Crowbar.CheckBoxEx()
		Me.RemovePathFromMaterialFileNamesCheckBox = New Crowbar.CheckBoxEx()
		Me.UseNonValveUvConversionCheckBox = New Crowbar.CheckBoxEx()
		Me.OverrideMdlVersionLabel = New Crowbar.LabelEx()
		Me.OverrideMdlVersionComboUserControl = New Crowbar.ComboUserControl()
		Me.PrefixMeshFileNamesWithModelNameCheckBox = New Crowbar.CheckBoxEx()
		Me.Panel2 = New Crowbar.PanelEx()
		Me.Label1 = New Crowbar.LabelEx()
		Me.DecompileInputComboUserControl = New Crowbar.ComboUserControl()
		Me.MdlPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.BrowseForMdlPathFolderOrFileNameButton = New Crowbar.ButtonEx()
		Me.GotoMdlButton = New Crowbar.ButtonEx()
		Me.GotoOutputPathButton = New Crowbar.ButtonEx()
		Me.BrowseForOutputPathButton = New Crowbar.ButtonEx()
		Me.OutputPathTextBox = New Crowbar.RichTextBoxEx()
		Me.OutputSubfolderTextBox = New Crowbar.RichTextBoxEx()
		Me.OutputPathComboUserControl = New Crowbar.ComboUserControl()
		Me.Label3 = New Crowbar.LabelEx()
		Me.UseDefaultOutputSubfolderButton = New Crowbar.ButtonEx()
		Me.Options_LogSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.DecompileOptionsPanel = New Crowbar.PanelEx()
		Me.ReCreateFilesGroupBox = New Crowbar.GroupBoxEx()
		Me.Panel1 = New Crowbar.PanelEx()
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox = New Crowbar.CheckBoxEx()
		Me.SkinFamilyOnSingleLineCheckBox = New Crowbar.CheckBoxEx()
		Me.TextureBmpFilesCheckBox = New Crowbar.CheckBoxEx()
		Me.DecompileOptionsUseDefaultsButton = New Crowbar.ButtonEx()
		Me.ModelFormatComboUserControl = New Crowbar.ComboUserControl()
		Me.Label2 = New Crowbar.LabelEx()
		Me.IncludeDefineBoneLinesCheckBox = New Crowbar.CheckBoxEx()
		Me.GroupIntoQciFilesCheckBox = New Crowbar.CheckBoxEx()
		Me.PlaceInAnimsSubfolderCheckBox = New Crowbar.CheckBoxEx()
		Me.LodMeshSmdFilesCheckBox = New Crowbar.CheckBoxEx()
		Me.ProceduralBonesVrdFileCheckBox = New Crowbar.CheckBoxEx()
		Me.BoneAnimationSmdFilesCheckBox = New Crowbar.CheckBoxEx()
		Me.VertexAnimationVtaFileCheckBox = New Crowbar.CheckBoxEx()
		Me.PhysicsMeshSmdFileCheckBox = New Crowbar.CheckBoxEx()
		Me.ReferenceMeshSmdFileCheckBox = New Crowbar.CheckBoxEx()
		Me.QcFileCheckBox = New Crowbar.CheckBoxEx()
		Me.OptionsGroupBox = New Crowbar.GroupBoxEx()
		Me.Panel3 = New Crowbar.PanelEx()
		Me.FolderForEachModelCheckBox = New Crowbar.CheckBoxEx()
		Me.DecompilerLogTextBox = New Crowbar.RichTextBoxEx()
		Me.DecompileButtonsPanel = New Crowbar.PanelEx()
		Me.DecompileButton = New Crowbar.ButtonEx()
		Me.SkipCurrentModelButton = New Crowbar.ButtonEx()
		Me.CancelDecompileButton = New Crowbar.ButtonEx()
		Me.UseAllInCompileButton = New Crowbar.ButtonEx()
		Me.Panel4 = New Crowbar.PanelEx()
		Me.DecompiledFilesComboUserControl = New Crowbar.ComboUserControl()
		Me.UseInEditButton = New Crowbar.ButtonEx()
		Me.UseInCompileButton = New Crowbar.ButtonEx()
		Me.GotoDecompiledFileButton = New Crowbar.ButtonEx()
		Me.Panel2.SuspendLayout()
		CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Options_LogSplitContainer.Panel1.SuspendLayout()
		Me.Options_LogSplitContainer.Panel2.SuspendLayout()
		Me.Options_LogSplitContainer.SuspendLayout()
		Me.DecompileOptionsPanel.SuspendLayout()
		Me.ReCreateFilesGroupBox.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		Me.Panel3.SuspendLayout()
		Me.DecompileButtonsPanel.SuspendLayout()
		Me.Panel4.SuspendLayout()
		Me.SuspendLayout()
		'
		'DebugInfoCheckBox
		'
		Me.DebugInfoCheckBox.AutoSize = True
		Me.DebugInfoCheckBox.IsReadOnly = False
		Me.DebugInfoCheckBox.Location = New System.Drawing.Point(3, 118)
		Me.DebugInfoCheckBox.Name = "DebugInfoCheckBox"
		Me.DebugInfoCheckBox.Size = New System.Drawing.Size(207, 17)
		Me.DebugInfoCheckBox.TabIndex = 14
		Me.DebugInfoCheckBox.Text = "Decompile-info comments and files"
		Me.ToolTip1.SetToolTip(Me.DebugInfoCheckBox, "Write comments and extra files that include decompile info useful in debugging.")
		Me.DebugInfoCheckBox.UseVisualStyleBackColor = True
		'
		'LogFileCheckBox
		'
		Me.LogFileCheckBox.AutoSize = True
		Me.LogFileCheckBox.IsReadOnly = False
		Me.LogFileCheckBox.Location = New System.Drawing.Point(3, 95)
		Me.LogFileCheckBox.Name = "LogFileCheckBox"
		Me.LogFileCheckBox.Size = New System.Drawing.Size(116, 17)
		Me.LogFileCheckBox.TabIndex = 13
		Me.LogFileCheckBox.Text = "Write log to a file"
		Me.ToolTip1.SetToolTip(Me.LogFileCheckBox, "Write the decompile log to a file.")
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'DeclareSequenceQciCheckBox
		'
		Me.DeclareSequenceQciCheckBox.AutoSize = True
		Me.DeclareSequenceQciCheckBox.IsReadOnly = False
		Me.DeclareSequenceQciCheckBox.Location = New System.Drawing.Point(3, 164)
		Me.DeclareSequenceQciCheckBox.Name = "DeclareSequenceQciCheckBox"
		Me.DeclareSequenceQciCheckBox.Size = New System.Drawing.Size(160, 17)
		Me.DeclareSequenceQciCheckBox.TabIndex = 40
		Me.DeclareSequenceQciCheckBox.Text = "$DeclareSequence QCI file"
		Me.ToolTip1.SetToolTip(Me.DeclareSequenceQciCheckBox, "Write a QCI file with a $DeclareSequence line for each sequence in the MDL file. " &
		"Useful for getting sequences in correct order for multiplayer.")
		Me.DeclareSequenceQciCheckBox.UseVisualStyleBackColor = True
		'
		'FormatForStricterImportersCheckBox
		'
		Me.FormatForStricterImportersCheckBox.AutoSize = True
		Me.FormatForStricterImportersCheckBox.IsReadOnly = False
		Me.FormatForStricterImportersCheckBox.Location = New System.Drawing.Point(3, 49)
		Me.FormatForStricterImportersCheckBox.Name = "FormatForStricterImportersCheckBox"
		Me.FormatForStricterImportersCheckBox.Size = New System.Drawing.Size(170, 17)
		Me.FormatForStricterImportersCheckBox.TabIndex = 12
		Me.FormatForStricterImportersCheckBox.Text = "Format for stricter importers"
		Me.ToolTip1.SetToolTip(Me.FormatForStricterImportersCheckBox, "Write decompiled files in a format that some importers expect, but is not as easy" &
		" to read.")
		Me.FormatForStricterImportersCheckBox.UseVisualStyleBackColor = True
		'
		'UseMixedCaseForKeywordsCheckBox
		'
		Me.UseMixedCaseForKeywordsCheckBox.AutoSize = True
		Me.UseMixedCaseForKeywordsCheckBox.IsReadOnly = False
		Me.UseMixedCaseForKeywordsCheckBox.Location = New System.Drawing.Point(20, 95)
		Me.UseMixedCaseForKeywordsCheckBox.Name = "UseMixedCaseForKeywordsCheckBox"
		Me.UseMixedCaseForKeywordsCheckBox.Size = New System.Drawing.Size(217, 17)
		Me.UseMixedCaseForKeywordsCheckBox.TabIndex = 42
		Me.UseMixedCaseForKeywordsCheckBox.Text = "Use MixedCase for keywords (Source)"
		Me.ToolTip1.SetToolTip(Me.UseMixedCaseForKeywordsCheckBox, "$CommandLikeThis instead of $commandlikethis")
		Me.UseMixedCaseForKeywordsCheckBox.UseVisualStyleBackColor = True
		'
		'RemovePathFromMaterialFileNamesCheckBox
		'
		Me.RemovePathFromMaterialFileNamesCheckBox.AutoSize = True
		Me.RemovePathFromMaterialFileNamesCheckBox.IsReadOnly = False
		Me.RemovePathFromMaterialFileNamesCheckBox.Location = New System.Drawing.Point(20, 141)
		Me.RemovePathFromMaterialFileNamesCheckBox.Name = "RemovePathFromMaterialFileNamesCheckBox"
		Me.RemovePathFromMaterialFileNamesCheckBox.Size = New System.Drawing.Size(219, 17)
		Me.RemovePathFromMaterialFileNamesCheckBox.TabIndex = 41
		Me.RemovePathFromMaterialFileNamesCheckBox.Text = "Remove path from material file names"
		Me.ToolTip1.SetToolTip(Me.RemovePathFromMaterialFileNamesCheckBox, "Write only the file name in the SMD, even if a path was stored. This might cause " &
		"problem with $CDMaterials in QC file.")
		Me.RemovePathFromMaterialFileNamesCheckBox.UseVisualStyleBackColor = True
		'
		'UseNonValveUvConversionCheckBox
		'
		Me.UseNonValveUvConversionCheckBox.AutoSize = True
		Me.UseNonValveUvConversionCheckBox.IsReadOnly = False
		Me.UseNonValveUvConversionCheckBox.Location = New System.Drawing.Point(20, 164)
		Me.UseNonValveUvConversionCheckBox.Name = "UseNonValveUvConversionCheckBox"
		Me.UseNonValveUvConversionCheckBox.Size = New System.Drawing.Size(245, 17)
		Me.UseNonValveUvConversionCheckBox.TabIndex = 44
		Me.UseNonValveUvConversionCheckBox.Text = "Use non-Valve UV conversion (GoldSource)"
		Me.ToolTip1.SetToolTip(Me.UseNonValveUvConversionCheckBox, "[ u=s/width ] and [ v=1-(t/height) ] instead of Valve's [ u=s/(width-1) ] and [ v" &
		"=1-(t/(height-1)) ]")
		Me.UseNonValveUvConversionCheckBox.UseVisualStyleBackColor = True
		'
		'OverrideMdlVersionLabel
		'
		Me.OverrideMdlVersionLabel.AutoSize = True
		Me.OverrideMdlVersionLabel.Location = New System.Drawing.Point(3, 202)
		Me.OverrideMdlVersionLabel.Name = "OverrideMdlVersionLabel"
		Me.OverrideMdlVersionLabel.Size = New System.Drawing.Size(120, 13)
		Me.OverrideMdlVersionLabel.TabIndex = 46
		Me.OverrideMdlVersionLabel.Text = "Override MDL version:"
		Me.ToolTip1.SetToolTip(Me.OverrideMdlVersionLabel, "Decompile based on this selected version instead of what is stored in MDL file.")
		'
		'OverrideMdlVersionComboUserControl
		'
		Me.OverrideMdlVersionComboUserControl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.OverrideMdlVersionComboUserControl.IsReadOnly = False
		Me.OverrideMdlVersionComboUserControl.Location = New System.Drawing.Point(123, 199)
		Me.OverrideMdlVersionComboUserControl.MaxDropDownItems = 30
		Me.OverrideMdlVersionComboUserControl.MultipleInputsIsAllowed = True
		Me.OverrideMdlVersionComboUserControl.Name = "OverrideMdlVersionComboUserControl"
		Me.OverrideMdlVersionComboUserControl.Size = New System.Drawing.Size(110, 23)
		Me.OverrideMdlVersionComboUserControl.TabIndex = 45
		Me.OverrideMdlVersionComboUserControl.TextHistoryIsKept = False
		Me.OverrideMdlVersionComboUserControl.TextHistoryMaxSize = 15
		Me.OverrideMdlVersionComboUserControl.TextIsPathFileNames = False
		Me.ToolTip1.SetToolTip(Me.OverrideMdlVersionComboUserControl, "Decompile based on this selected version instead of what is stored in MDL file.")
		Me.OverrideMdlVersionComboUserControl.ValueMember = ""
		'
		'PrefixMeshFileNamesWithModelNameCheckBox
		'
		Me.PrefixMeshFileNamesWithModelNameCheckBox.AutoSize = True
		Me.PrefixMeshFileNamesWithModelNameCheckBox.IsReadOnly = False
		Me.PrefixMeshFileNamesWithModelNameCheckBox.Location = New System.Drawing.Point(3, 26)
		Me.PrefixMeshFileNamesWithModelNameCheckBox.Name = "PrefixMeshFileNamesWithModelNameCheckBox"
		Me.PrefixMeshFileNamesWithModelNameCheckBox.Size = New System.Drawing.Size(231, 17)
		Me.PrefixMeshFileNamesWithModelNameCheckBox.TabIndex = 47
		Me.PrefixMeshFileNamesWithModelNameCheckBox.Text = "Prefix mesh file names with model name"
		Me.ToolTip1.SetToolTip(Me.PrefixMeshFileNamesWithModelNameCheckBox, "Avoid file name conflicts.")
		Me.PrefixMeshFileNamesWithModelNameCheckBox.UseVisualStyleBackColor = True
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.DecompileInputComboUserControl)
		Me.Panel2.Controls.Add(Me.MdlPathFileNameTextBox)
		Me.Panel2.Controls.Add(Me.BrowseForMdlPathFolderOrFileNameButton)
		Me.Panel2.Controls.Add(Me.GotoMdlButton)
		Me.Panel2.Controls.Add(Me.GotoOutputPathButton)
		Me.Panel2.Controls.Add(Me.BrowseForOutputPathButton)
		Me.Panel2.Controls.Add(Me.OutputPathTextBox)
		Me.Panel2.Controls.Add(Me.OutputSubfolderTextBox)
		Me.Panel2.Controls.Add(Me.OutputPathComboUserControl)
		Me.Panel2.Controls.Add(Me.Label3)
		Me.Panel2.Controls.Add(Me.UseDefaultOutputSubfolderButton)
		Me.Panel2.Controls.Add(Me.Options_LogSplitContainer)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.SelectedIndex = -1
		Me.Panel2.SelectedValue = Nothing
		Me.Panel2.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.TabIndex = 8
		'
		'Label1
		'
		Me.Label1.Location = New System.Drawing.Point(3, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(64, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "MDL input:"
		'
		'DecompileInputComboUserControl
		'
		Me.DecompileInputComboUserControl.DataSource = Nothing
		Me.DecompileInputComboUserControl.DisplayMember = ""
		Me.DecompileInputComboUserControl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.DecompileInputComboUserControl.IsReadOnly = False
		Me.DecompileInputComboUserControl.Location = New System.Drawing.Point(73, 4)
		Me.DecompileInputComboUserControl.MaxDropDownItems = 30
		Me.DecompileInputComboUserControl.MultipleInputsIsAllowed = True
		Me.DecompileInputComboUserControl.Name = "DecompileInputComboUserControl"
		Me.DecompileInputComboUserControl.SelectedIndex = -1
		Me.DecompileInputComboUserControl.SelectedValue = ""
		Me.DecompileInputComboUserControl.Size = New System.Drawing.Size(140, 23)
		Me.DecompileInputComboUserControl.TabIndex = 1
		Me.DecompileInputComboUserControl.TextHistoryIsKept = False
		Me.DecompileInputComboUserControl.TextHistoryMaxSize = 15
		Me.DecompileInputComboUserControl.TextIsPathFileNames = False
		Me.DecompileInputComboUserControl.ValueMember = ""
		'
		'MdlPathFileNameTextBox
		'
		Me.MdlPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MdlPathFileNameTextBox.CueBannerText = ""
		Me.MdlPathFileNameTextBox.DetectUrls = False
		Me.MdlPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.MdlPathFileNameTextBox.Location = New System.Drawing.Point(219, 4)
		Me.MdlPathFileNameTextBox.Multiline = False
		Me.MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox"
		Me.MdlPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.MdlPathFileNameTextBox.Size = New System.Drawing.Size(435, 22)
		Me.MdlPathFileNameTextBox.TabIndex = 1
		Me.MdlPathFileNameTextBox.Text = ""
		Me.MdlPathFileNameTextBox.WordWrap = False
		'
		'BrowseForMdlPathFolderOrFileNameButton
		'
		Me.BrowseForMdlPathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForMdlPathFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForMdlPathFolderOrFileNameButton.Name = "BrowseForMdlPathFolderOrFileNameButton"
		Me.BrowseForMdlPathFolderOrFileNameButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForMdlPathFolderOrFileNameButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.BrowseForMdlPathFolderOrFileNameButton.TabIndex = 2
		Me.BrowseForMdlPathFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForMdlPathFolderOrFileNameButton.UseVisualStyleBackColor = False
		'
		'GotoMdlButton
		'
		Me.GotoMdlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoMdlButton.Location = New System.Drawing.Point(730, 3)
		Me.GotoMdlButton.Name = "GotoMdlButton"
		Me.GotoMdlButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoMdlButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.GotoMdlButton.TabIndex = 3
		Me.GotoMdlButton.Text = "Goto"
		Me.GotoMdlButton.UseVisualStyleBackColor = True
		'
		'GotoOutputPathButton
		'
		Me.GotoOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoOutputPathButton.Location = New System.Drawing.Point(730, 32)
		Me.GotoOutputPathButton.Name = "GotoOutputPathButton"
		Me.GotoOutputPathButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoOutputPathButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.GotoOutputPathButton.TabIndex = 18
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
		Me.BrowseForOutputPathButton.TabIndex = 17
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
		Me.OutputPathTextBox.Location = New System.Drawing.Point(219, 33)
		Me.OutputPathTextBox.Multiline = False
		Me.OutputPathTextBox.Name = "OutputPathTextBox"
		Me.OutputPathTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.OutputPathTextBox.Size = New System.Drawing.Size(435, 22)
		Me.OutputPathTextBox.TabIndex = 16
		Me.OutputPathTextBox.Text = ""
		Me.OutputPathTextBox.WordWrap = False
		'
		'OutputSubfolderTextBox
		'
		Me.OutputSubfolderTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputSubfolderTextBox.CueBannerText = ""
		Me.OutputSubfolderTextBox.DetectUrls = False
		Me.OutputSubfolderTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.OutputSubfolderTextBox.Location = New System.Drawing.Point(219, 33)
		Me.OutputSubfolderTextBox.Multiline = False
		Me.OutputSubfolderTextBox.Name = "OutputSubfolderTextBox"
		Me.OutputSubfolderTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.OutputSubfolderTextBox.Size = New System.Drawing.Size(435, 22)
		Me.OutputSubfolderTextBox.TabIndex = 20
		Me.OutputSubfolderTextBox.Text = ""
		Me.OutputSubfolderTextBox.Visible = False
		Me.OutputSubfolderTextBox.WordWrap = False
		'
		'OutputPathComboUserControl
		'
		Me.OutputPathComboUserControl.DataSource = Nothing
		Me.OutputPathComboUserControl.DisplayMember = ""
		Me.OutputPathComboUserControl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.OutputPathComboUserControl.IsReadOnly = False
		Me.OutputPathComboUserControl.Location = New System.Drawing.Point(73, 33)
		Me.OutputPathComboUserControl.MaxDropDownItems = 30
		Me.OutputPathComboUserControl.MultipleInputsIsAllowed = True
		Me.OutputPathComboUserControl.Name = "OutputPathComboUserControl"
		Me.OutputPathComboUserControl.SelectedIndex = -1
		Me.OutputPathComboUserControl.SelectedValue = ""
		Me.OutputPathComboUserControl.Size = New System.Drawing.Size(140, 23)
		Me.OutputPathComboUserControl.TabIndex = 14
		Me.OutputPathComboUserControl.TextHistoryIsKept = False
		Me.OutputPathComboUserControl.TextHistoryMaxSize = 15
		Me.OutputPathComboUserControl.TextIsPathFileNames = False
		Me.OutputPathComboUserControl.ValueMember = ""
		'
		'Label3
		'
		Me.Label3.Location = New System.Drawing.Point(3, 37)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(62, 13)
		Me.Label3.TabIndex = 13
		Me.Label3.Text = "Output to:"
		'
		'UseDefaultOutputSubfolderButton
		'
		Me.UseDefaultOutputSubfolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseDefaultOutputSubfolderButton.Location = New System.Drawing.Point(660, 32)
		Me.UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton"
		Me.UseDefaultOutputSubfolderButton.Size = New System.Drawing.Size(113, 23)
		Me.UseDefaultOutputSubfolderButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.UseDefaultOutputSubfolderButton.TabIndex = 19
		Me.UseDefaultOutputSubfolderButton.Text = "Use Default"
		Me.UseDefaultOutputSubfolderButton.UseVisualStyleBackColor = True
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
		Me.Options_LogSplitContainer.Panel1.Controls.Add(Me.DecompileOptionsPanel)
		Me.Options_LogSplitContainer.Panel1MinSize = 45
		'
		'Options_LogSplitContainer.Panel2
		'
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.DecompilerLogTextBox)
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.DecompileButtonsPanel)
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.Panel4)
		Me.Options_LogSplitContainer.Panel2MinSize = 45
		Me.Options_LogSplitContainer.Size = New System.Drawing.Size(770, 472)
		Me.Options_LogSplitContainer.SplitterDistance = 250
		Me.Options_LogSplitContainer.TabIndex = 12
		'
		'DecompileOptionsPanel
		'
		Me.DecompileOptionsPanel.Controls.Add(Me.ReCreateFilesGroupBox)
		Me.DecompileOptionsPanel.Controls.Add(Me.OptionsGroupBox)
		Me.DecompileOptionsPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DecompileOptionsPanel.Location = New System.Drawing.Point(0, 0)
		Me.DecompileOptionsPanel.Name = "DecompileOptionsPanel"
		Me.DecompileOptionsPanel.SelectedIndex = -1
		Me.DecompileOptionsPanel.SelectedValue = Nothing
		Me.DecompileOptionsPanel.Size = New System.Drawing.Size(770, 250)
		Me.DecompileOptionsPanel.TabIndex = 8
		'
		'ReCreateFilesGroupBox
		'
		Me.ReCreateFilesGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.ReCreateFilesGroupBox.Controls.Add(Me.Panel1)
		Me.ReCreateFilesGroupBox.IsReadOnly = False
		Me.ReCreateFilesGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.ReCreateFilesGroupBox.Name = "ReCreateFilesGroupBox"
		Me.ReCreateFilesGroupBox.SelectedValue = Nothing
		Me.ReCreateFilesGroupBox.Size = New System.Drawing.Size(520, 247)
		Me.ReCreateFilesGroupBox.TabIndex = 0
		Me.ReCreateFilesGroupBox.TabStop = False
		Me.ReCreateFilesGroupBox.Text = "Re-Create Files"
		'
		'Panel1
		'
		Me.Panel1.AutoScroll = True
		Me.Panel1.Controls.Add(Me.UseNonValveUvConversionCheckBox)
		Me.Panel1.Controls.Add(Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox)
		Me.Panel1.Controls.Add(Me.UseMixedCaseForKeywordsCheckBox)
		Me.Panel1.Controls.Add(Me.RemovePathFromMaterialFileNamesCheckBox)
		Me.Panel1.Controls.Add(Me.SkinFamilyOnSingleLineCheckBox)
		Me.Panel1.Controls.Add(Me.TextureBmpFilesCheckBox)
		Me.Panel1.Controls.Add(Me.DecompileOptionsUseDefaultsButton)
		Me.Panel1.Controls.Add(Me.ModelFormatComboUserControl)
		Me.Panel1.Controls.Add(Me.Label2)
		Me.Panel1.Controls.Add(Me.IncludeDefineBoneLinesCheckBox)
		Me.Panel1.Controls.Add(Me.GroupIntoQciFilesCheckBox)
		Me.Panel1.Controls.Add(Me.PlaceInAnimsSubfolderCheckBox)
		Me.Panel1.Controls.Add(Me.LodMeshSmdFilesCheckBox)
		Me.Panel1.Controls.Add(Me.ProceduralBonesVrdFileCheckBox)
		Me.Panel1.Controls.Add(Me.BoneAnimationSmdFilesCheckBox)
		Me.Panel1.Controls.Add(Me.VertexAnimationVtaFileCheckBox)
		Me.Panel1.Controls.Add(Me.PhysicsMeshSmdFileCheckBox)
		Me.Panel1.Controls.Add(Me.ReferenceMeshSmdFileCheckBox)
		Me.Panel1.Controls.Add(Me.QcFileCheckBox)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(3, 18)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.SelectedIndex = -1
		Me.Panel1.SelectedValue = Nothing
		Me.Panel1.Size = New System.Drawing.Size(514, 226)
		Me.Panel1.TabIndex = 11
		'
		'OnlyChangedMaterialsInTextureGroupLinesCheckBox
		'
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.AutoSize = True
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.IsReadOnly = False
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Location = New System.Drawing.Point(20, 49)
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Name = "OnlyChangedMaterialsInTextureGroupLinesCheckBox"
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Size = New System.Drawing.Size(264, 17)
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.TabIndex = 43
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Text = "Only changed materials in $texturegroup lines"
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.UseVisualStyleBackColor = True
		'
		'SkinFamilyOnSingleLineCheckBox
		'
		Me.SkinFamilyOnSingleLineCheckBox.AutoSize = True
		Me.SkinFamilyOnSingleLineCheckBox.IsReadOnly = False
		Me.SkinFamilyOnSingleLineCheckBox.Location = New System.Drawing.Point(20, 26)
		Me.SkinFamilyOnSingleLineCheckBox.Name = "SkinFamilyOnSingleLineCheckBox"
		Me.SkinFamilyOnSingleLineCheckBox.Size = New System.Drawing.Size(258, 17)
		Me.SkinFamilyOnSingleLineCheckBox.TabIndex = 39
		Me.SkinFamilyOnSingleLineCheckBox.Text = "Each $texturegroup skin-family on single line"
		Me.SkinFamilyOnSingleLineCheckBox.UseVisualStyleBackColor = True
		'
		'TextureBmpFilesCheckBox
		'
		Me.TextureBmpFilesCheckBox.AutoSize = True
		Me.TextureBmpFilesCheckBox.IsReadOnly = False
		Me.TextureBmpFilesCheckBox.Location = New System.Drawing.Point(318, 3)
		Me.TextureBmpFilesCheckBox.Name = "TextureBmpFilesCheckBox"
		Me.TextureBmpFilesCheckBox.Size = New System.Drawing.Size(181, 17)
		Me.TextureBmpFilesCheckBox.TabIndex = 38
		Me.TextureBmpFilesCheckBox.Text = "Texture BMP files (GoldSource)"
		Me.TextureBmpFilesCheckBox.UseVisualStyleBackColor = True
		'
		'DecompileOptionsUseDefaultsButton
		'
		Me.DecompileOptionsUseDefaultsButton.Location = New System.Drawing.Point(213, 197)
		Me.DecompileOptionsUseDefaultsButton.Name = "DecompileOptionsUseDefaultsButton"
		Me.DecompileOptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.DecompileOptionsUseDefaultsButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.DecompileOptionsUseDefaultsButton.TabIndex = 37
		Me.DecompileOptionsUseDefaultsButton.Text = "Use Defaults"
		Me.DecompileOptionsUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'ModelFormatComboUserControl
		'
		Me.ModelFormatComboUserControl.DataSource = Nothing
		Me.ModelFormatComboUserControl.DisplayMember = ""
		Me.ModelFormatComboUserControl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.ModelFormatComboUserControl.IsReadOnly = False
		Me.ModelFormatComboUserControl.Location = New System.Drawing.Point(80, 199)
		Me.ModelFormatComboUserControl.MaxDropDownItems = 30
		Me.ModelFormatComboUserControl.MultipleInputsIsAllowed = True
		Me.ModelFormatComboUserControl.Name = "ModelFormatComboUserControl"
		Me.ModelFormatComboUserControl.SelectedIndex = -1
		Me.ModelFormatComboUserControl.SelectedValue = ""
		Me.ModelFormatComboUserControl.Size = New System.Drawing.Size(125, 23)
		Me.ModelFormatComboUserControl.TabIndex = 15
		Me.ModelFormatComboUserControl.TextHistoryIsKept = False
		Me.ModelFormatComboUserControl.TextHistoryMaxSize = 15
		Me.ModelFormatComboUserControl.TextIsPathFileNames = False
		Me.ModelFormatComboUserControl.ValueMember = ""
		Me.ModelFormatComboUserControl.Visible = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(3, 202)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(80, 13)
		Me.Label2.TabIndex = 23
		Me.Label2.Text = "Model format:"
		Me.Label2.Visible = False
		'
		'IncludeDefineBoneLinesCheckBox
		'
		Me.IncludeDefineBoneLinesCheckBox.AutoSize = True
		Me.IncludeDefineBoneLinesCheckBox.IsReadOnly = False
		Me.IncludeDefineBoneLinesCheckBox.Location = New System.Drawing.Point(20, 72)
		Me.IncludeDefineBoneLinesCheckBox.Name = "IncludeDefineBoneLinesCheckBox"
		Me.IncludeDefineBoneLinesCheckBox.Size = New System.Drawing.Size(286, 17)
		Me.IncludeDefineBoneLinesCheckBox.TabIndex = 2
		Me.IncludeDefineBoneLinesCheckBox.Text = "Include $definebone lines (typical for view models)"
		Me.IncludeDefineBoneLinesCheckBox.UseVisualStyleBackColor = True
		'
		'GroupIntoQciFilesCheckBox
		'
		Me.GroupIntoQciFilesCheckBox.AutoSize = True
		Me.GroupIntoQciFilesCheckBox.IsReadOnly = False
		Me.GroupIntoQciFilesCheckBox.Location = New System.Drawing.Point(80, 3)
		Me.GroupIntoQciFilesCheckBox.Name = "GroupIntoQciFilesCheckBox"
		Me.GroupIntoQciFilesCheckBox.Size = New System.Drawing.Size(128, 17)
		Me.GroupIntoQciFilesCheckBox.TabIndex = 1
		Me.GroupIntoQciFilesCheckBox.Text = "Group into QCI files"
		Me.GroupIntoQciFilesCheckBox.UseVisualStyleBackColor = True
		'
		'PlaceInAnimsSubfolderCheckBox
		'
		Me.PlaceInAnimsSubfolderCheckBox.AutoSize = True
		Me.PlaceInAnimsSubfolderCheckBox.IsReadOnly = False
		Me.PlaceInAnimsSubfolderCheckBox.Location = New System.Drawing.Point(335, 141)
		Me.PlaceInAnimsSubfolderCheckBox.Name = "PlaceInAnimsSubfolderCheckBox"
		Me.PlaceInAnimsSubfolderCheckBox.Size = New System.Drawing.Size(159, 17)
		Me.PlaceInAnimsSubfolderCheckBox.TabIndex = 9
		Me.PlaceInAnimsSubfolderCheckBox.Text = "Place in ""anims"" subfolder"
		Me.PlaceInAnimsSubfolderCheckBox.UseVisualStyleBackColor = True
		'
		'LodMeshSmdFilesCheckBox
		'
		Me.LodMeshSmdFilesCheckBox.AutoSize = True
		Me.LodMeshSmdFilesCheckBox.IsReadOnly = False
		Me.LodMeshSmdFilesCheckBox.Location = New System.Drawing.Point(318, 26)
		Me.LodMeshSmdFilesCheckBox.Name = "LodMeshSmdFilesCheckBox"
		Me.LodMeshSmdFilesCheckBox.Size = New System.Drawing.Size(129, 17)
		Me.LodMeshSmdFilesCheckBox.TabIndex = 5
		Me.LodMeshSmdFilesCheckBox.Text = "LOD mesh SMD files"
		Me.LodMeshSmdFilesCheckBox.UseVisualStyleBackColor = True
		'
		'ProceduralBonesVrdFileCheckBox
		'
		Me.ProceduralBonesVrdFileCheckBox.AutoSize = True
		Me.ProceduralBonesVrdFileCheckBox.IsReadOnly = False
		Me.ProceduralBonesVrdFileCheckBox.Location = New System.Drawing.Point(318, 95)
		Me.ProceduralBonesVrdFileCheckBox.Name = "ProceduralBonesVrdFileCheckBox"
		Me.ProceduralBonesVrdFileCheckBox.Size = New System.Drawing.Size(160, 17)
		Me.ProceduralBonesVrdFileCheckBox.TabIndex = 10
		Me.ProceduralBonesVrdFileCheckBox.Text = "Procedural bones VRD file"
		Me.ProceduralBonesVrdFileCheckBox.UseVisualStyleBackColor = True
		'
		'BoneAnimationSmdFilesCheckBox
		'
		Me.BoneAnimationSmdFilesCheckBox.AutoSize = True
		Me.BoneAnimationSmdFilesCheckBox.IsReadOnly = False
		Me.BoneAnimationSmdFilesCheckBox.Location = New System.Drawing.Point(318, 118)
		Me.BoneAnimationSmdFilesCheckBox.Name = "BoneAnimationSmdFilesCheckBox"
		Me.BoneAnimationSmdFilesCheckBox.Size = New System.Drawing.Size(159, 17)
		Me.BoneAnimationSmdFilesCheckBox.TabIndex = 8
		Me.BoneAnimationSmdFilesCheckBox.Text = "Bone animation SMD files"
		Me.BoneAnimationSmdFilesCheckBox.UseVisualStyleBackColor = True
		'
		'VertexAnimationVtaFileCheckBox
		'
		Me.VertexAnimationVtaFileCheckBox.AutoSize = True
		Me.VertexAnimationVtaFileCheckBox.IsReadOnly = False
		Me.VertexAnimationVtaFileCheckBox.Location = New System.Drawing.Point(318, 72)
		Me.VertexAnimationVtaFileCheckBox.Name = "VertexAnimationVtaFileCheckBox"
		Me.VertexAnimationVtaFileCheckBox.Size = New System.Drawing.Size(190, 17)
		Me.VertexAnimationVtaFileCheckBox.TabIndex = 7
		Me.VertexAnimationVtaFileCheckBox.Text = "Vertex animation VTA file (flexes)"
		Me.VertexAnimationVtaFileCheckBox.UseVisualStyleBackColor = True
		'
		'PhysicsMeshSmdFileCheckBox
		'
		Me.PhysicsMeshSmdFileCheckBox.AutoSize = True
		Me.PhysicsMeshSmdFileCheckBox.IsReadOnly = False
		Me.PhysicsMeshSmdFileCheckBox.Location = New System.Drawing.Point(318, 49)
		Me.PhysicsMeshSmdFileCheckBox.Name = "PhysicsMeshSmdFileCheckBox"
		Me.PhysicsMeshSmdFileCheckBox.Size = New System.Drawing.Size(138, 17)
		Me.PhysicsMeshSmdFileCheckBox.TabIndex = 6
		Me.PhysicsMeshSmdFileCheckBox.Text = "Physics mesh SMD file"
		Me.PhysicsMeshSmdFileCheckBox.UseVisualStyleBackColor = True
		'
		'ReferenceMeshSmdFileCheckBox
		'
		Me.ReferenceMeshSmdFileCheckBox.AutoSize = True
		Me.ReferenceMeshSmdFileCheckBox.IsReadOnly = False
		Me.ReferenceMeshSmdFileCheckBox.Location = New System.Drawing.Point(3, 118)
		Me.ReferenceMeshSmdFileCheckBox.Name = "ReferenceMeshSmdFileCheckBox"
		Me.ReferenceMeshSmdFileCheckBox.Size = New System.Drawing.Size(153, 17)
		Me.ReferenceMeshSmdFileCheckBox.TabIndex = 3
		Me.ReferenceMeshSmdFileCheckBox.Text = "Reference mesh SMD file"
		Me.ReferenceMeshSmdFileCheckBox.UseVisualStyleBackColor = True
		'
		'QcFileCheckBox
		'
		Me.QcFileCheckBox.AutoSize = True
		Me.QcFileCheckBox.IsReadOnly = False
		Me.QcFileCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.QcFileCheckBox.Name = "QcFileCheckBox"
		Me.QcFileCheckBox.Size = New System.Drawing.Size(60, 17)
		Me.QcFileCheckBox.TabIndex = 0
		Me.QcFileCheckBox.Text = "QC file"
		Me.QcFileCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OptionsGroupBox.Controls.Add(Me.Panel3)
		Me.OptionsGroupBox.IsReadOnly = False
		Me.OptionsGroupBox.Location = New System.Drawing.Point(526, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.SelectedValue = Nothing
		Me.OptionsGroupBox.Size = New System.Drawing.Size(244, 247)
		Me.OptionsGroupBox.TabIndex = 6
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'Panel3
		'
		Me.Panel3.AutoScroll = True
		Me.Panel3.Controls.Add(Me.PrefixMeshFileNamesWithModelNameCheckBox)
		Me.Panel3.Controls.Add(Me.OverrideMdlVersionLabel)
		Me.Panel3.Controls.Add(Me.OverrideMdlVersionComboUserControl)
		Me.Panel3.Controls.Add(Me.FolderForEachModelCheckBox)
		Me.Panel3.Controls.Add(Me.DebugInfoCheckBox)
		Me.Panel3.Controls.Add(Me.LogFileCheckBox)
		Me.Panel3.Controls.Add(Me.DeclareSequenceQciCheckBox)
		Me.Panel3.Controls.Add(Me.FormatForStricterImportersCheckBox)
		Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel3.Location = New System.Drawing.Point(3, 18)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.SelectedIndex = -1
		Me.Panel3.SelectedValue = Nothing
		Me.Panel3.Size = New System.Drawing.Size(238, 226)
		Me.Panel3.TabIndex = 0
		'
		'FolderForEachModelCheckBox
		'
		Me.FolderForEachModelCheckBox.AutoSize = True
		Me.FolderForEachModelCheckBox.IsReadOnly = False
		Me.FolderForEachModelCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.FolderForEachModelCheckBox.Name = "FolderForEachModelCheckBox"
		Me.FolderForEachModelCheckBox.Size = New System.Drawing.Size(139, 17)
		Me.FolderForEachModelCheckBox.TabIndex = 11
		Me.FolderForEachModelCheckBox.Text = "Folder for each model"
		Me.FolderForEachModelCheckBox.UseVisualStyleBackColor = True
		'
		'DecompilerLogTextBox
		'
		Me.DecompilerLogTextBox.CueBannerText = ""
		Me.DecompilerLogTextBox.DetectUrls = False
		Me.DecompilerLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DecompilerLogTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.DecompilerLogTextBox.HideSelection = False
		Me.DecompilerLogTextBox.Location = New System.Drawing.Point(0, 26)
		Me.DecompilerLogTextBox.Name = "DecompilerLogTextBox"
		Me.DecompilerLogTextBox.ReadOnly = True
		Me.DecompilerLogTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.DecompilerLogTextBox.Size = New System.Drawing.Size(770, 166)
		Me.DecompilerLogTextBox.TabIndex = 0
		Me.DecompilerLogTextBox.Text = ""
		Me.DecompilerLogTextBox.WordWrap = False
		'
		'DecompileButtonsPanel
		'
		Me.DecompileButtonsPanel.Controls.Add(Me.DecompileButton)
		Me.DecompileButtonsPanel.Controls.Add(Me.SkipCurrentModelButton)
		Me.DecompileButtonsPanel.Controls.Add(Me.CancelDecompileButton)
		Me.DecompileButtonsPanel.Controls.Add(Me.UseAllInCompileButton)
		Me.DecompileButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.DecompileButtonsPanel.Location = New System.Drawing.Point(0, 0)
		Me.DecompileButtonsPanel.Name = "DecompileButtonsPanel"
		Me.DecompileButtonsPanel.SelectedIndex = -1
		Me.DecompileButtonsPanel.SelectedValue = Nothing
		Me.DecompileButtonsPanel.Size = New System.Drawing.Size(770, 26)
		Me.DecompileButtonsPanel.TabIndex = 7
		'
		'DecompileButton
		'
		Me.DecompileButton.Location = New System.Drawing.Point(0, 0)
		Me.DecompileButton.Name = "DecompileButton"
		Me.DecompileButton.Size = New System.Drawing.Size(120, 23)
		Me.DecompileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.DecompileButton.TabIndex = 2
		Me.DecompileButton.Text = "&Decompile"
		Me.DecompileButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentModelButton
		'
		Me.SkipCurrentModelButton.Enabled = False
		Me.SkipCurrentModelButton.Location = New System.Drawing.Point(126, 0)
		Me.SkipCurrentModelButton.Name = "SkipCurrentModelButton"
		Me.SkipCurrentModelButton.Size = New System.Drawing.Size(120, 23)
		Me.SkipCurrentModelButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.SkipCurrentModelButton.TabIndex = 3
		Me.SkipCurrentModelButton.Text = "Skip Current Model"
		Me.SkipCurrentModelButton.UseVisualStyleBackColor = True
		'
		'CancelDecompileButton
		'
		Me.CancelDecompileButton.Enabled = False
		Me.CancelDecompileButton.Location = New System.Drawing.Point(252, 0)
		Me.CancelDecompileButton.Name = "CancelDecompileButton"
		Me.CancelDecompileButton.Size = New System.Drawing.Size(120, 23)
		Me.CancelDecompileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.CancelDecompileButton.TabIndex = 4
		Me.CancelDecompileButton.Text = "Cancel Decompile"
		Me.CancelDecompileButton.UseVisualStyleBackColor = True
		'
		'UseAllInCompileButton
		'
		Me.UseAllInCompileButton.Enabled = False
		Me.UseAllInCompileButton.Location = New System.Drawing.Point(378, 0)
		Me.UseAllInCompileButton.Name = "UseAllInCompileButton"
		Me.UseAllInCompileButton.Size = New System.Drawing.Size(120, 23)
		Me.UseAllInCompileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.UseAllInCompileButton.TabIndex = 5
		Me.UseAllInCompileButton.Text = "Use All in Compile"
		Me.UseAllInCompileButton.UseVisualStyleBackColor = True
		'
		'Panel4
		'
		Me.Panel4.Controls.Add(Me.DecompiledFilesComboUserControl)
		Me.Panel4.Controls.Add(Me.UseInEditButton)
		Me.Panel4.Controls.Add(Me.UseInCompileButton)
		Me.Panel4.Controls.Add(Me.GotoDecompiledFileButton)
		Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.Panel4.Location = New System.Drawing.Point(0, 192)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.SelectedIndex = -1
		Me.Panel4.SelectedValue = Nothing
		Me.Panel4.Size = New System.Drawing.Size(770, 26)
		Me.Panel4.TabIndex = 8
		'
		'DecompiledFilesComboUserControl
		'
		Me.DecompiledFilesComboUserControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DecompiledFilesComboUserControl.DataSource = Nothing
		Me.DecompiledFilesComboUserControl.DisplayMember = ""
		Me.DecompiledFilesComboUserControl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.DecompiledFilesComboUserControl.IsReadOnly = False
		Me.DecompiledFilesComboUserControl.Location = New System.Drawing.Point(0, 4)
		Me.DecompiledFilesComboUserControl.MaxDropDownItems = 30
		Me.DecompiledFilesComboUserControl.MultipleInputsIsAllowed = True
		Me.DecompiledFilesComboUserControl.Name = "DecompiledFilesComboUserControl"
		Me.DecompiledFilesComboUserControl.SelectedIndex = -1
		Me.DecompiledFilesComboUserControl.SelectedValue = ""
		Me.DecompiledFilesComboUserControl.Size = New System.Drawing.Size(621, 23)
		Me.DecompiledFilesComboUserControl.TabIndex = 1
		Me.DecompiledFilesComboUserControl.TextHistoryIsKept = False
		Me.DecompiledFilesComboUserControl.TextHistoryMaxSize = 15
		Me.DecompiledFilesComboUserControl.TextIsPathFileNames = False
		Me.DecompiledFilesComboUserControl.ValueMember = ""
		'
		'UseInEditButton
		'
		Me.UseInEditButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInEditButton.Enabled = False
		Me.UseInEditButton.Location = New System.Drawing.Point(551, 3)
		Me.UseInEditButton.Name = "UseInEditButton"
		Me.UseInEditButton.Size = New System.Drawing.Size(72, 23)
		Me.UseInEditButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.UseInEditButton.TabIndex = 2
		Me.UseInEditButton.Text = "Use in Edit"
		Me.UseInEditButton.UseVisualStyleBackColor = True
		Me.UseInEditButton.Visible = False
		'
		'UseInCompileButton
		'
		Me.UseInCompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInCompileButton.Enabled = False
		Me.UseInCompileButton.Location = New System.Drawing.Point(627, 3)
		Me.UseInCompileButton.Name = "UseInCompileButton"
		Me.UseInCompileButton.Size = New System.Drawing.Size(94, 23)
		Me.UseInCompileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.UseInCompileButton.TabIndex = 3
		Me.UseInCompileButton.Text = "Use in Compile"
		Me.UseInCompileButton.UseVisualStyleBackColor = True
		'
		'GotoDecompiledFileButton
		'
		Me.GotoDecompiledFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoDecompiledFileButton.Location = New System.Drawing.Point(727, 3)
		Me.GotoDecompiledFileButton.Name = "GotoDecompiledFileButton"
		Me.GotoDecompiledFileButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoDecompiledFileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.GotoDecompiledFileButton.TabIndex = 4
		Me.GotoDecompiledFileButton.Text = "Goto"
		Me.GotoDecompiledFileButton.UseVisualStyleBackColor = True
		'
		'DecompileUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel2)
		Me.Name = "DecompileUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.ResumeLayout(False)
		Me.Options_LogSplitContainer.Panel1.ResumeLayout(False)
		Me.Options_LogSplitContainer.Panel2.ResumeLayout(False)
		CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Options_LogSplitContainer.ResumeLayout(False)
		Me.DecompileOptionsPanel.ResumeLayout(False)
		Me.ReCreateFilesGroupBox.ResumeLayout(False)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.Panel3.ResumeLayout(False)
		Me.Panel3.PerformLayout()
		Me.DecompileButtonsPanel.ResumeLayout(False)
		Me.Panel4.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents DecompileButton As ButtonEx
	Friend WithEvents MdlPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents BrowseForMdlPathFolderOrFileNameButton As ButtonEx
	Friend WithEvents Label1 As Crowbar.LabelEx
	Friend WithEvents ReCreateFilesGroupBox As GroupBoxEx
	Friend WithEvents Panel1 As PanelEx
	Friend WithEvents LodMeshSmdFilesCheckBox As CheckBoxEx
	Friend WithEvents ProceduralBonesVrdFileCheckBox As CheckBoxEx
	Friend WithEvents BoneAnimationSmdFilesCheckBox As CheckBoxEx
	Friend WithEvents VertexAnimationVtaFileCheckBox As CheckBoxEx
	Friend WithEvents PhysicsMeshSmdFileCheckBox As CheckBoxEx
	Friend WithEvents DebugInfoCheckBox As CheckBoxEx
	Friend WithEvents ReferenceMeshSmdFileCheckBox As CheckBoxEx
	Friend WithEvents QcFileCheckBox As CheckBoxEx
	Friend WithEvents Panel2 As PanelEx
	Friend WithEvents DecompilerLogTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CancelDecompileButton As ButtonEx
	Friend WithEvents SkipCurrentModelButton As ButtonEx
	Friend WithEvents DecompileInputComboUserControl As ComboUserControl
	Friend WithEvents Options_LogSplitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents Label2 As Crowbar.LabelEx
	Friend WithEvents ModelFormatComboUserControl As ComboUserControl
	Friend WithEvents FormatForStricterImportersCheckBox As CheckBoxEx
	Friend WithEvents GroupIntoQciFilesCheckBox As CheckBoxEx
	Friend WithEvents PlaceInAnimsSubfolderCheckBox As CheckBoxEx
	Friend WithEvents LogFileCheckBox As CheckBoxEx
	Friend WithEvents GotoMdlButton As ButtonEx
	Friend WithEvents IncludeDefineBoneLinesCheckBox As CheckBoxEx
	Friend WithEvents FolderForEachModelCheckBox As CheckBoxEx
	Friend WithEvents DecompiledFilesComboUserControl As ComboUserControl
	Friend WithEvents GotoDecompiledFileButton As ButtonEx
	Friend WithEvents UseInEditButton As ButtonEx
	Friend WithEvents UseInCompileButton As ButtonEx
	Friend WithEvents UseAllInCompileButton As ButtonEx
	Friend WithEvents DecompileOptionsUseDefaultsButton As ButtonEx
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents TextureBmpFilesCheckBox As CheckBoxEx
	Friend WithEvents SkinFamilyOnSingleLineCheckBox As CheckBoxEx
	Friend WithEvents DeclareSequenceQciCheckBox As CheckBoxEx
	Friend WithEvents RemovePathFromMaterialFileNamesCheckBox As CheckBoxEx
	Friend WithEvents UseMixedCaseForKeywordsCheckBox As CheckBoxEx
	Friend WithEvents GotoOutputPathButton As ButtonEx
	Friend WithEvents BrowseForOutputPathButton As ButtonEx
	Friend WithEvents OutputPathTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents OutputPathComboUserControl As ComboUserControl
	Friend WithEvents Label3 As Crowbar.LabelEx
	Friend WithEvents UseDefaultOutputSubfolderButton As ButtonEx
	Friend WithEvents OptionsGroupBox As GroupBoxEx
	Friend WithEvents Panel3 As PanelEx
	Friend WithEvents OnlyChangedMaterialsInTextureGroupLinesCheckBox As CheckBoxEx
	Friend WithEvents OutputSubfolderTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents UseNonValveUvConversionCheckBox As CheckBoxEx
	Friend WithEvents OverrideMdlVersionLabel As Crowbar.LabelEx
	Friend WithEvents OverrideMdlVersionComboUserControl As ComboUserControl
	Friend WithEvents PrefixMeshFileNamesWithModelNameCheckBox As CheckBoxEx
	Friend WithEvents DecompileButtonsPanel As PanelEx
	Friend WithEvents DecompileOptionsPanel As PanelEx
	Friend WithEvents Panel4 As PanelEx
End Class
