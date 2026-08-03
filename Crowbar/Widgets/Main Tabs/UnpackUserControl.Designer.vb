<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UnpackUserControl
	Inherits BaseUserControl

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.LogFileCheckBox = New Crowbar.CheckBoxEx()
		Me.RefreshListingButton = New Crowbar.ButtonEx()
		Me.Panel2 = New Crowbar.PanelEx()
		Me.OutputSamePathTextBox = New Crowbar.RichTextBoxEx()
		Me.GameModelsOutputPathTextBox = New Crowbar.RichTextBoxEx()
		Me.UnpackComboBox = New Crowbar.ComboUserControl()
		Me.GotoOutputPathButton = New Crowbar.ButtonEx()
		Me.BrowseForOutputPathButton = New Crowbar.ButtonEx()
		Me.OutputPathTextBox = New Crowbar.RichTextBoxEx()
		Me.OutputSubfolderTextBox = New Crowbar.RichTextBoxEx()
		Me.OutputPathComboBox = New Crowbar.ComboUserControl()
		Me.Label2 = New Crowbar.LabelEx()
		Me.UseDefaultOutputSubfolderButton = New Crowbar.ButtonEx()
		Me.PackagesLabel = New Crowbar.LabelEx()
		Me.PackagePathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.BrowseForPackagePathFolderOrFileNameButton = New Crowbar.ButtonEx()
		Me.GotoPackageButton = New Crowbar.ButtonEx()
		Me.Options_LogSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
		Me.ContentsGroupBox = New Crowbar.GroupBoxEx()
		Me.ContentsGroupBoxFillPanel = New Crowbar.PanelEx()
		Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
		Me.PackageTreeView = New Crowbar.TreeViewEx()
		Me.PackageDataGridView = New Crowbar.DataGridViewEx()
		Me.Panel3 = New Crowbar.PanelEx()
		Me.SelectionPathTextBox = New Crowbar.RichTextBoxEx()
		Me.ToolStrip1 = New Crowbar.ToolStripEx()
		Me.FindToolStripTextBox = New Crowbar.ToolStripSpringTextBoxEx()
		Me.FindToolStripButton = New Crowbar.ToolStripButtonEx()
		Me.ToolStripSeparator1 = New Crowbar.ToolStripSeparatorEx()
		Me.FilesSelectedCountToolStripLabel = New Crowbar.ToolStripLabelEx()
		Me.ToolStripSeparator3 = New Crowbar.ToolStripSeparatorEx()
		Me.SizeSelectedTotalToolStripLabel = New Crowbar.ToolStripLabelEx()
		Me.ContentsMinScrollerPanel = New Crowbar.PanelEx()
		Me.OptionsGroupBox = New Crowbar.GroupBoxEx()
		Me.OptionsGroupBoxFillPanel = New Crowbar.PanelEx()
		Me.KeepFullPathCheckBox = New Crowbar.CheckBoxEx()
		Me.FolderForEachPackageCheckBox = New Crowbar.CheckBoxEx()
		Me.Label3 = New Crowbar.LabelEx()
		Me.EditGameSetupButton = New Crowbar.ButtonEx()
		Me.GameSetupComboBox = New Crowbar.ComboUserControl()
		Me.SelectAllModelsAndMaterialsFoldersCheckBox = New Crowbar.CheckBoxEx()
		Me.UnpackOptionsUseDefaultsButton = New Crowbar.ButtonEx()
		Me.UnpackerLogTextBox = New Crowbar.RichTextBoxEx()
		Me.UnpackButtonsPanel = New Crowbar.PanelEx()
		Me.UnpackButton = New Crowbar.ButtonEx()
		Me.SkipCurrentPackageButton = New Crowbar.ButtonEx()
		Me.CancelUnpackButton = New Crowbar.ButtonEx()
		Me.UseAllInDecompileButton = New Crowbar.ButtonEx()
		Me.PostUnpackPanel = New Crowbar.PanelEx()
		Me.UnpackedFilesComboBox = New Crowbar.ComboUserControl()
		Me.UseInPreviewButton = New Crowbar.ButtonEx()
		Me.UseInDecompileButton = New Crowbar.ButtonEx()
		Me.GotoUnpackedFileButton = New Crowbar.ButtonEx()
		Me.Panel2.SuspendLayout()
		CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Options_LogSplitContainer.Panel1.SuspendLayout()
		Me.Options_LogSplitContainer.Panel2.SuspendLayout()
		Me.Options_LogSplitContainer.SuspendLayout()
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer2.Panel1.SuspendLayout()
		Me.SplitContainer2.Panel2.SuspendLayout()
		Me.SplitContainer2.SuspendLayout()
		Me.ContentsGroupBox.SuspendLayout()
		Me.ContentsGroupBoxFillPanel.SuspendLayout()
		CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer3.Panel1.SuspendLayout()
		Me.SplitContainer3.Panel2.SuspendLayout()
		Me.SplitContainer3.SuspendLayout()
		Me.Panel3.SuspendLayout()
		Me.ToolStrip1.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		Me.OptionsGroupBoxFillPanel.SuspendLayout()
		Me.UnpackButtonsPanel.SuspendLayout()
		Me.PostUnpackPanel.SuspendLayout()
		Me.SuspendLayout()
		'
		'ImageList1
		'
		Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
		Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
		Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
		'
		'LogFileCheckBox
		'
		Me.LogFileCheckBox.AutoSize = True
		Me.LogFileCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.LogFileCheckBox.IsReadOnly = False
		Me.LogFileCheckBox.Location = New System.Drawing.Point(3, 72)
		Me.LogFileCheckBox.Name = "LogFileCheckBox"
		Me.LogFileCheckBox.Size = New System.Drawing.Size(116, 17)
		Me.LogFileCheckBox.TabIndex = 5
		Me.LogFileCheckBox.Text = "Write log to a file"
		Me.ToolTip1.SetToolTip(Me.LogFileCheckBox, "Write unpack log to a file.")
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'RefreshListingButton
		'
		Me.RefreshListingButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.RefreshListingButton.ButtonCanBeFocused = True
		Me.RefreshListingButton.Image = Global.Crowbar.My.Resources.Resources.Refresh
		Me.RefreshListingButton.Location = New System.Drawing.Point(631, 4)
		Me.RefreshListingButton.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.RefreshListingButton.Name = "RefreshListingButton"
		Me.RefreshListingButton.Padding = New System.Windows.Forms.Padding(0, 0, 1, 2)
		Me.RefreshListingButton.Size = New System.Drawing.Size(23, 22)
		Me.RefreshListingButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.RefreshListingButton.TabIndex = 37
		Me.ToolTip1.SetToolTip(Me.RefreshListingButton, "Refresh Package Listing")
		Me.RefreshListingButton.UseVisualStyleBackColor = True
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.RefreshListingButton)
		Me.Panel2.Controls.Add(Me.OutputSamePathTextBox)
		Me.Panel2.Controls.Add(Me.GameModelsOutputPathTextBox)
		Me.Panel2.Controls.Add(Me.UnpackComboBox)
		Me.Panel2.Controls.Add(Me.GotoOutputPathButton)
		Me.Panel2.Controls.Add(Me.BrowseForOutputPathButton)
		Me.Panel2.Controls.Add(Me.OutputPathTextBox)
		Me.Panel2.Controls.Add(Me.OutputSubfolderTextBox)
		Me.Panel2.Controls.Add(Me.OutputPathComboBox)
		Me.Panel2.Controls.Add(Me.Label2)
		Me.Panel2.Controls.Add(Me.UseDefaultOutputSubfolderButton)
		Me.Panel2.Controls.Add(Me.PackagesLabel)
		Me.Panel2.Controls.Add(Me.PackagePathFileNameTextBox)
		Me.Panel2.Controls.Add(Me.BrowseForPackagePathFolderOrFileNameButton)
		Me.Panel2.Controls.Add(Me.GotoPackageButton)
		Me.Panel2.Controls.Add(Me.Options_LogSplitContainer)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.TabIndex = 0
		'
		'OutputSamePathTextBox
		'
		Me.OutputSamePathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputSamePathTextBox.CueBannerText = ""
		Me.OutputSamePathTextBox.DetectUrls = False
		Me.OutputSamePathTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.OutputSamePathTextBox.Location = New System.Drawing.Point(209, 33)
		Me.OutputSamePathTextBox.Multiline = False
		Me.OutputSamePathTextBox.Name = "OutputSamePathTextBox"
		Me.OutputSamePathTextBox.ReadOnly = True
		Me.OutputSamePathTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.OutputSamePathTextBox.SelectionEnabled = True
		Me.OutputSamePathTextBox.Size = New System.Drawing.Size(445, 22)
		Me.OutputSamePathTextBox.TabIndex = 26
		Me.OutputSamePathTextBox.Text = ""
		Me.OutputSamePathTextBox.WordWrap = False
		'
		'GameModelsOutputPathTextBox
		'
		Me.GameModelsOutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameModelsOutputPathTextBox.CueBannerText = ""
		Me.GameModelsOutputPathTextBox.DetectUrls = False
		Me.GameModelsOutputPathTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GameModelsOutputPathTextBox.Location = New System.Drawing.Point(209, 33)
		Me.GameModelsOutputPathTextBox.Multiline = False
		Me.GameModelsOutputPathTextBox.Name = "GameModelsOutputPathTextBox"
		Me.GameModelsOutputPathTextBox.ReadOnly = True
		Me.GameModelsOutputPathTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.GameModelsOutputPathTextBox.SelectionEnabled = True
		Me.GameModelsOutputPathTextBox.Size = New System.Drawing.Size(445, 22)
		Me.GameModelsOutputPathTextBox.TabIndex = 15
		Me.GameModelsOutputPathTextBox.Text = ""
		'
		'UnpackComboBox
		'
		Me.UnpackComboBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.UnpackComboBox.IsReadOnly = False
		Me.UnpackComboBox.Location = New System.Drawing.Point(71, 4)
		Me.UnpackComboBox.MaxDropDownItems = 30
		Me.UnpackComboBox.MultipleInputsIsAllowed = True
		Me.UnpackComboBox.Name = "UnpackComboBox"
		Me.UnpackComboBox.Size = New System.Drawing.Size(132, 23)
		Me.UnpackComboBox.TabIndex = 1
		Me.UnpackComboBox.TextHistoryIsKept = False
		Me.UnpackComboBox.TextHistoryMaxSize = 15
		Me.UnpackComboBox.TextIsPathFileNames = False
		'
		'GotoOutputPathButton
		'
		Me.GotoOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoOutputPathButton.ButtonCanBeFocused = True
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
		Me.BrowseForOutputPathButton.ButtonCanBeFocused = True
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
		Me.OutputPathTextBox.Location = New System.Drawing.Point(209, 33)
		Me.OutputPathTextBox.Multiline = False
		Me.OutputPathTextBox.Name = "OutputPathTextBox"
		Me.OutputPathTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.OutputPathTextBox.SelectionEnabled = True
		Me.OutputPathTextBox.Size = New System.Drawing.Size(445, 22)
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
		Me.OutputSubfolderTextBox.Location = New System.Drawing.Point(209, 33)
		Me.OutputSubfolderTextBox.Multiline = False
		Me.OutputSubfolderTextBox.Name = "OutputSubfolderTextBox"
		Me.OutputSubfolderTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.OutputSubfolderTextBox.SelectionEnabled = True
		Me.OutputSubfolderTextBox.Size = New System.Drawing.Size(445, 22)
		Me.OutputSubfolderTextBox.TabIndex = 22
		Me.OutputSubfolderTextBox.Text = ""
		Me.OutputSubfolderTextBox.Visible = False
		'
		'OutputPathComboBox
		'
		Me.OutputPathComboBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.OutputPathComboBox.IsReadOnly = False
		Me.OutputPathComboBox.Location = New System.Drawing.Point(71, 33)
		Me.OutputPathComboBox.MaxDropDownItems = 30
		Me.OutputPathComboBox.MultipleInputsIsAllowed = True
		Me.OutputPathComboBox.Name = "OutputPathComboBox"
		Me.OutputPathComboBox.Size = New System.Drawing.Size(132, 23)
		Me.OutputPathComboBox.TabIndex = 14
		Me.OutputPathComboBox.TextHistoryIsKept = False
		Me.OutputPathComboBox.TextHistoryMaxSize = 15
		Me.OutputPathComboBox.TextIsPathFileNames = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(3, 37)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(62, 13)
		Me.Label2.TabIndex = 13
		Me.Label2.Text = "Output to:"
		'
		'UseDefaultOutputSubfolderButton
		'
		Me.UseDefaultOutputSubfolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseDefaultOutputSubfolderButton.ButtonCanBeFocused = True
		Me.UseDefaultOutputSubfolderButton.Location = New System.Drawing.Point(660, 32)
		Me.UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton"
		Me.UseDefaultOutputSubfolderButton.Size = New System.Drawing.Size(113, 23)
		Me.UseDefaultOutputSubfolderButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.UseDefaultOutputSubfolderButton.TabIndex = 19
		Me.UseDefaultOutputSubfolderButton.Text = "Use Default"
		Me.UseDefaultOutputSubfolderButton.UseVisualStyleBackColor = True
		'
		'PackagesLabel
		'
		Me.PackagesLabel.AutoSize = True
		Me.PackagesLabel.Location = New System.Drawing.Point(3, 8)
		Me.PackagesLabel.Name = "PackagesLabel"
		Me.PackagesLabel.Size = New System.Drawing.Size(57, 13)
		Me.PackagesLabel.TabIndex = 1
		Me.PackagesLabel.Text = "Packages:"
		'
		'PackagePathFileNameTextBox
		'
		Me.PackagePathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PackagePathFileNameTextBox.CueBannerText = ""
		Me.PackagePathFileNameTextBox.DetectUrls = False
		Me.PackagePathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.PackagePathFileNameTextBox.Location = New System.Drawing.Point(209, 4)
		Me.PackagePathFileNameTextBox.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
		Me.PackagePathFileNameTextBox.Multiline = False
		Me.PackagePathFileNameTextBox.Name = "PackagePathFileNameTextBox"
		Me.PackagePathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.PackagePathFileNameTextBox.SelectionEnabled = True
		Me.PackagePathFileNameTextBox.Size = New System.Drawing.Size(422, 22)
		Me.PackagePathFileNameTextBox.TabIndex = 2
		Me.PackagePathFileNameTextBox.Text = ""
		Me.PackagePathFileNameTextBox.WordWrap = False
		'
		'BrowseForPackagePathFolderOrFileNameButton
		'
		Me.BrowseForPackagePathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForPackagePathFolderOrFileNameButton.ButtonCanBeFocused = True
		Me.BrowseForPackagePathFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForPackagePathFolderOrFileNameButton.Name = "BrowseForPackagePathFolderOrFileNameButton"
		Me.BrowseForPackagePathFolderOrFileNameButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForPackagePathFolderOrFileNameButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.BrowseForPackagePathFolderOrFileNameButton.TabIndex = 3
		Me.BrowseForPackagePathFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForPackagePathFolderOrFileNameButton.UseVisualStyleBackColor = True
		'
		'GotoPackageButton
		'
		Me.GotoPackageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoPackageButton.ButtonCanBeFocused = True
		Me.GotoPackageButton.Location = New System.Drawing.Point(730, 3)
		Me.GotoPackageButton.Name = "GotoPackageButton"
		Me.GotoPackageButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoPackageButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.GotoPackageButton.TabIndex = 4
		Me.GotoPackageButton.Text = "Goto"
		Me.GotoPackageButton.UseVisualStyleBackColor = True
		'
		'Options_LogSplitContainer
		'
		Me.Options_LogSplitContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Options_LogSplitContainer.Location = New System.Drawing.Point(3, 61)
		Me.Options_LogSplitContainer.Name = "Options_LogSplitContainer"
		Me.Options_LogSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'Options_LogSplitContainer.Panel1
		'
		Me.Options_LogSplitContainer.Panel1.Controls.Add(Me.SplitContainer2)
		Me.Options_LogSplitContainer.Panel1MinSize = 45
		'
		'Options_LogSplitContainer.Panel2
		'
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.UnpackerLogTextBox)
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.UnpackButtonsPanel)
		Me.Options_LogSplitContainer.Panel2.Controls.Add(Me.PostUnpackPanel)
		Me.Options_LogSplitContainer.Panel2MinSize = 90
		Me.Options_LogSplitContainer.Size = New System.Drawing.Size(770, 472)
		Me.Options_LogSplitContainer.SplitterDistance = 347
		Me.Options_LogSplitContainer.TabIndex = 6
		'
		'SplitContainer2
		'
		Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
		Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
		Me.SplitContainer2.Name = "SplitContainer2"
		'
		'SplitContainer2.Panel1
		'
		Me.SplitContainer2.Panel1.Controls.Add(Me.ContentsGroupBox)
		'
		'SplitContainer2.Panel2
		'
		Me.SplitContainer2.Panel2.Controls.Add(Me.OptionsGroupBox)
		Me.SplitContainer2.Size = New System.Drawing.Size(770, 347)
		Me.SplitContainer2.SplitterDistance = 544
		Me.SplitContainer2.SplitterWidth = 6
		Me.SplitContainer2.TabIndex = 0
		'
		'ContentsGroupBox
		'
		Me.ContentsGroupBox.Controls.Add(Me.ContentsGroupBoxFillPanel)
		Me.ContentsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ContentsGroupBox.IsReadOnly = False
		Me.ContentsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.ContentsGroupBox.Name = "ContentsGroupBox"
		Me.ContentsGroupBox.SelectedValue = Nothing
		Me.ContentsGroupBox.Size = New System.Drawing.Size(544, 347)
		Me.ContentsGroupBox.TabIndex = 0
		Me.ContentsGroupBox.TabStop = False
		Me.ContentsGroupBox.Text = "Contents of package"
		'
		'ContentsGroupBoxFillPanel
		'
		Me.ContentsGroupBoxFillPanel.AutoScroll = True
		Me.ContentsGroupBoxFillPanel.Controls.Add(Me.SplitContainer3)
		Me.ContentsGroupBoxFillPanel.Controls.Add(Me.Panel3)
		Me.ContentsGroupBoxFillPanel.Controls.Add(Me.ToolStrip1)
		Me.ContentsGroupBoxFillPanel.Controls.Add(Me.ContentsMinScrollerPanel)
		Me.ContentsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ContentsGroupBoxFillPanel.Location = New System.Drawing.Point(3, 18)
		Me.ContentsGroupBoxFillPanel.Name = "ContentsGroupBoxFillPanel"
		Me.ContentsGroupBoxFillPanel.Size = New System.Drawing.Size(538, 326)
		Me.ContentsGroupBoxFillPanel.TabIndex = 12
		'
		'SplitContainer3
		'
		Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer3.Location = New System.Drawing.Point(0, 22)
		Me.SplitContainer3.Name = "SplitContainer3"
		'
		'SplitContainer3.Panel1
		'
		Me.SplitContainer3.Panel1.Controls.Add(Me.PackageTreeView)
		'
		'SplitContainer3.Panel2
		'
		Me.SplitContainer3.Panel2.Controls.Add(Me.PackageDataGridView)
		Me.SplitContainer3.Size = New System.Drawing.Size(538, 279)
		Me.SplitContainer3.SplitterDistance = 250
		Me.SplitContainer3.TabIndex = 6
		'
		'PackageTreeView
		'
		Me.PackageTreeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PackageTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll
		Me.PackageTreeView.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.PackageTreeView.HideSelection = False
		Me.PackageTreeView.ImageIndex = 0
		Me.PackageTreeView.ImageList = Me.ImageList1
		Me.PackageTreeView.Location = New System.Drawing.Point(3, 3)
		Me.PackageTreeView.Name = "PackageTreeView"
		Me.PackageTreeView.SelectedImageIndex = 0
		Me.PackageTreeView.Size = New System.Drawing.Size(244, 273)
		Me.PackageTreeView.TabIndex = 0
		'
		'PackageDataGridView
		'
		Me.PackageDataGridView.AllowUserToOrderColumns = True
		Me.PackageDataGridView.AllowUserToResizeColumns = True
		Me.PackageDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.PackageDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
		Me.PackageDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.PackageDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PackageDataGridView.EnableHeadersVisualStyles = False
		Me.PackageDataGridView.ReadOnly = True
		Me.PackageDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
		Me.PackageDataGridView.Location = New System.Drawing.Point(0, 0)
		Me.PackageDataGridView.Name = "PackageDataGridView"
		Me.PackageDataGridView.ShowCellErrors = False
		Me.PackageDataGridView.ShowRowErrors = False
		Me.PackageDataGridView.Size = New System.Drawing.Size(284, 279)
		Me.PackageDataGridView.TabIndex = 1
		'
		'Panel3
		'
		Me.Panel3.Controls.Add(Me.SelectionPathTextBox)
		Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel3.Location = New System.Drawing.Point(0, 0)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(538, 22)
		Me.Panel3.TabIndex = 11
		'
		'SelectionPathTextBox
		'
		Me.SelectionPathTextBox.CueBannerText = ""
		Me.SelectionPathTextBox.DetectUrls = False
		Me.SelectionPathTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SelectionPathTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.SelectionPathTextBox.Location = New System.Drawing.Point(0, 0)
		Me.SelectionPathTextBox.Multiline = False
		Me.SelectionPathTextBox.Name = "SelectionPathTextBox"
		Me.SelectionPathTextBox.ReadOnly = True
		Me.SelectionPathTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.SelectionPathTextBox.SelectionEnabled = True
		Me.SelectionPathTextBox.Size = New System.Drawing.Size(538, 22)
		Me.SelectionPathTextBox.TabIndex = 1
		Me.SelectionPathTextBox.Text = ""
		Me.SelectionPathTextBox.WordWrap = False
		'
		'ToolStrip1
		'
		Me.ToolStrip1.CanOverflow = False
		Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FindToolStripTextBox, Me.FindToolStripButton, Me.ToolStripSeparator1, Me.FilesSelectedCountToolStripLabel, Me.ToolStripSeparator3, Me.SizeSelectedTotalToolStripLabel})
		Me.ToolStrip1.Location = New System.Drawing.Point(0, 301)
		Me.ToolStrip1.Name = "ToolStrip1"
		Me.ToolStrip1.Size = New System.Drawing.Size(538, 25)
		Me.ToolStrip1.Stretch = True
		Me.ToolStrip1.TabIndex = 10
		Me.ToolStrip1.Text = "ToolStrip1"
		'
		'FindToolStripTextBox
		'
		Me.FindToolStripTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.FindToolStripTextBox.Name = "FindToolStripTextBox"
		Me.FindToolStripTextBox.Size = New System.Drawing.Size(408, 22)
		Me.FindToolStripTextBox.ToolTipText = "Text to find"
		'
		'FindToolStripButton
		'
		Me.FindToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.FindToolStripButton.Image = Global.Crowbar.My.Resources.Resources.Find
		Me.FindToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.FindToolStripButton.Name = "FindToolStripButton"
		Me.FindToolStripButton.RightToLeftAutoMirrorImage = True
		Me.FindToolStripButton.Size = New System.Drawing.Size(23, 22)
		Me.FindToolStripButton.Text = "Find"
		Me.FindToolStripButton.ToolTipText = "Find"
		'
		'ToolStripSeparator1
		'
		Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
		Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
		'
		'FilesSelectedCountToolStripLabel
		'
		Me.FilesSelectedCountToolStripLabel.Name = "FilesSelectedCountToolStripLabel"
		Me.FilesSelectedCountToolStripLabel.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.FilesSelectedCountToolStripLabel.Size = New System.Drawing.Size(40, 22)
		Me.FilesSelectedCountToolStripLabel.Text = "0 / 0"
		Me.FilesSelectedCountToolStripLabel.ToolTipText = "Selected item count / Total item count"
		'
		'ToolStripSeparator3
		'
		Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
		Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
		'
		'SizeSelectedTotalToolStripLabel
		'
		Me.SizeSelectedTotalToolStripLabel.Name = "SizeSelectedTotalToolStripLabel"
		Me.SizeSelectedTotalToolStripLabel.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.SizeSelectedTotalToolStripLabel.Size = New System.Drawing.Size(23, 22)
		Me.SizeSelectedTotalToolStripLabel.Text = "0"
		Me.SizeSelectedTotalToolStripLabel.ToolTipText = "Byte count of selected items"
		'
		'ContentsMinScrollerPanel
		'
		Me.ContentsMinScrollerPanel.Location = New System.Drawing.Point(0, 0)
		Me.ContentsMinScrollerPanel.Name = "ContentsMinScrollerPanel"
		Me.ContentsMinScrollerPanel.Size = New System.Drawing.Size(250, 110)
		Me.ContentsMinScrollerPanel.TabIndex = 12
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Controls.Add(Me.OptionsGroupBoxFillPanel)
		Me.OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsGroupBox.IsReadOnly = False
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.SelectedValue = Nothing
		Me.OptionsGroupBox.Size = New System.Drawing.Size(220, 347)
		Me.OptionsGroupBox.TabIndex = 0
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'OptionsGroupBoxFillPanel
		'
		Me.OptionsGroupBoxFillPanel.AutoScroll = True
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.KeepFullPathCheckBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.FolderForEachPackageCheckBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.Label3)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.EditGameSetupButton)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.GameSetupComboBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.SelectAllModelsAndMaterialsFoldersCheckBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.LogFileCheckBox)
		Me.OptionsGroupBoxFillPanel.Controls.Add(Me.UnpackOptionsUseDefaultsButton)
		Me.OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsGroupBoxFillPanel.Location = New System.Drawing.Point(3, 18)
		Me.OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel"
		Me.OptionsGroupBoxFillPanel.Size = New System.Drawing.Size(214, 326)
		Me.OptionsGroupBoxFillPanel.TabIndex = 0
		'
		'KeepFullPathCheckBox
		'
		Me.KeepFullPathCheckBox.AutoSize = True
		Me.KeepFullPathCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.KeepFullPathCheckBox.IsReadOnly = False
		Me.KeepFullPathCheckBox.Location = New System.Drawing.Point(3, 26)
		Me.KeepFullPathCheckBox.Name = "KeepFullPathCheckBox"
		Me.KeepFullPathCheckBox.Size = New System.Drawing.Size(98, 17)
		Me.KeepFullPathCheckBox.TabIndex = 13
		Me.KeepFullPathCheckBox.Text = "Keep full path"
		Me.KeepFullPathCheckBox.UseVisualStyleBackColor = True
		'
		'FolderForEachPackageCheckBox
		'
		Me.FolderForEachPackageCheckBox.AutoSize = True
		Me.FolderForEachPackageCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.FolderForEachPackageCheckBox.IsReadOnly = False
		Me.FolderForEachPackageCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.FolderForEachPackageCheckBox.Name = "FolderForEachPackageCheckBox"
		Me.FolderForEachPackageCheckBox.Size = New System.Drawing.Size(150, 17)
		Me.FolderForEachPackageCheckBox.TabIndex = 12
		Me.FolderForEachPackageCheckBox.Text = "Folder for each package"
		Me.FolderForEachPackageCheckBox.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(3, 239)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(155, 13)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Game that has the unpacker:"
		Me.Label3.Visible = False
		'
		'EditGameSetupButton
		'
		Me.EditGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.EditGameSetupButton.ButtonCanBeFocused = True
		Me.EditGameSetupButton.Location = New System.Drawing.Point(8113, 229)
		Me.EditGameSetupButton.Name = "EditGameSetupButton"
		Me.EditGameSetupButton.Size = New System.Drawing.Size(90, 23)
		Me.EditGameSetupButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.EditGameSetupButton.TabIndex = 1
		Me.EditGameSetupButton.Text = "Set Up Games"
		Me.EditGameSetupButton.UseVisualStyleBackColor = True
		Me.EditGameSetupButton.Visible = False
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.GameSetupComboBox.IsReadOnly = False
		Me.GameSetupComboBox.Location = New System.Drawing.Point(3, 255)
		Me.GameSetupComboBox.MaxDropDownItems = 30
		Me.GameSetupComboBox.MultipleInputsIsAllowed = True
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(8200, 23)
		Me.GameSetupComboBox.TabIndex = 2
		Me.GameSetupComboBox.TextHistoryIsKept = False
		Me.GameSetupComboBox.TextHistoryMaxSize = 15
		Me.GameSetupComboBox.TextIsPathFileNames = False
		Me.GameSetupComboBox.Visible = False
		'
		'SelectAllModelsAndMaterialsFoldersCheckBox
		'
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.AutoSize = True
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.IsReadOnly = False
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Location = New System.Drawing.Point(33, 180)
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Name = "SelectAllModelsAndMaterialsFoldersCheckBox"
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Size = New System.Drawing.Size(238, 17)
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.TabIndex = 4
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Text = "Select all ""models"" and ""materials"" folders"
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.UseVisualStyleBackColor = True
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Visible = False
		'
		'UnpackOptionsUseDefaultsButton
		'
		Me.UnpackOptionsUseDefaultsButton.ButtonCanBeFocused = True
		Me.UnpackOptionsUseDefaultsButton.Location = New System.Drawing.Point(33, 203)
		Me.UnpackOptionsUseDefaultsButton.Name = "UnpackOptionsUseDefaultsButton"
		Me.UnpackOptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.UnpackOptionsUseDefaultsButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.UnpackOptionsUseDefaultsButton.TabIndex = 6
		Me.UnpackOptionsUseDefaultsButton.Text = "Use Defaults"
		Me.UnpackOptionsUseDefaultsButton.UseVisualStyleBackColor = True
		Me.UnpackOptionsUseDefaultsButton.Visible = False
		'
		'UnpackerLogTextBox
		'
		Me.UnpackerLogTextBox.CueBannerText = ""
		Me.UnpackerLogTextBox.DetectUrls = False
		Me.UnpackerLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.UnpackerLogTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.UnpackerLogTextBox.HideSelection = False
		Me.UnpackerLogTextBox.Location = New System.Drawing.Point(0, 26)
		Me.UnpackerLogTextBox.Name = "UnpackerLogTextBox"
		Me.UnpackerLogTextBox.ReadOnly = True
		Me.UnpackerLogTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.UnpackerLogTextBox.SelectionEnabled = True
		Me.UnpackerLogTextBox.Size = New System.Drawing.Size(770, 69)
		Me.UnpackerLogTextBox.TabIndex = 0
		Me.UnpackerLogTextBox.Text = ""
		Me.UnpackerLogTextBox.WordWrap = False
		'
		'UnpackButtonsPanel
		'
		Me.UnpackButtonsPanel.Controls.Add(Me.UnpackButton)
		Me.UnpackButtonsPanel.Controls.Add(Me.SkipCurrentPackageButton)
		Me.UnpackButtonsPanel.Controls.Add(Me.CancelUnpackButton)
		Me.UnpackButtonsPanel.Controls.Add(Me.UseAllInDecompileButton)
		Me.UnpackButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.UnpackButtonsPanel.Location = New System.Drawing.Point(0, 0)
		Me.UnpackButtonsPanel.Name = "UnpackButtonsPanel"
		Me.UnpackButtonsPanel.Size = New System.Drawing.Size(770, 26)
		Me.UnpackButtonsPanel.TabIndex = 1
		'
		'UnpackButton
		'
		Me.UnpackButton.ButtonCanBeFocused = True
		Me.UnpackButton.Enabled = False
		Me.UnpackButton.Location = New System.Drawing.Point(0, 0)
		Me.UnpackButton.Name = "UnpackButton"
		Me.UnpackButton.Size = New System.Drawing.Size(120, 23)
		Me.UnpackButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.UnpackButton.TabIndex = 2
		Me.UnpackButton.Text = "Unpack"
		Me.UnpackButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentPackageButton
		'
		Me.SkipCurrentPackageButton.ButtonCanBeFocused = True
		Me.SkipCurrentPackageButton.Enabled = False
		Me.SkipCurrentPackageButton.Location = New System.Drawing.Point(126, 0)
		Me.SkipCurrentPackageButton.Name = "SkipCurrentPackageButton"
		Me.SkipCurrentPackageButton.Size = New System.Drawing.Size(120, 23)
		Me.SkipCurrentPackageButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.SkipCurrentPackageButton.TabIndex = 3
		Me.SkipCurrentPackageButton.Text = "Skip Current Package"
		Me.SkipCurrentPackageButton.UseVisualStyleBackColor = True
		'
		'CancelUnpackButton
		'
		Me.CancelUnpackButton.ButtonCanBeFocused = True
		Me.CancelUnpackButton.Enabled = False
		Me.CancelUnpackButton.Location = New System.Drawing.Point(252, 0)
		Me.CancelUnpackButton.Name = "CancelUnpackButton"
		Me.CancelUnpackButton.Size = New System.Drawing.Size(120, 23)
		Me.CancelUnpackButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.CancelUnpackButton.TabIndex = 4
		Me.CancelUnpackButton.Text = "Cancel Unpack"
		Me.CancelUnpackButton.UseVisualStyleBackColor = True
		'
		'UseAllInDecompileButton
		'
		Me.UseAllInDecompileButton.ButtonCanBeFocused = True
		Me.UseAllInDecompileButton.Enabled = False
		Me.UseAllInDecompileButton.Location = New System.Drawing.Point(378, 0)
		Me.UseAllInDecompileButton.Name = "UseAllInDecompileButton"
		Me.UseAllInDecompileButton.Size = New System.Drawing.Size(120, 23)
		Me.UseAllInDecompileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.UseAllInDecompileButton.TabIndex = 5
		Me.UseAllInDecompileButton.Text = "Use All in Decompile"
		Me.UseAllInDecompileButton.UseVisualStyleBackColor = True
		'
		'PostUnpackPanel
		'
		Me.PostUnpackPanel.Controls.Add(Me.UnpackedFilesComboBox)
		Me.PostUnpackPanel.Controls.Add(Me.UseInPreviewButton)
		Me.PostUnpackPanel.Controls.Add(Me.UseInDecompileButton)
		Me.PostUnpackPanel.Controls.Add(Me.GotoUnpackedFileButton)
		Me.PostUnpackPanel.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PostUnpackPanel.Location = New System.Drawing.Point(0, 95)
		Me.PostUnpackPanel.Name = "PostUnpackPanel"
		Me.PostUnpackPanel.Size = New System.Drawing.Size(770, 26)
		Me.PostUnpackPanel.TabIndex = 5
		'
		'UnpackedFilesComboBox
		'
		Me.UnpackedFilesComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UnpackedFilesComboBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.UnpackedFilesComboBox.IsReadOnly = False
		Me.UnpackedFilesComboBox.Location = New System.Drawing.Point(0, 4)
		Me.UnpackedFilesComboBox.MaxDropDownItems = 30
		Me.UnpackedFilesComboBox.MultipleInputsIsAllowed = True
		Me.UnpackedFilesComboBox.Name = "UnpackedFilesComboBox"
		Me.UnpackedFilesComboBox.Size = New System.Drawing.Size(512, 23)
		Me.UnpackedFilesComboBox.TabIndex = 1
		Me.UnpackedFilesComboBox.TextHistoryIsKept = False
		Me.UnpackedFilesComboBox.TextHistoryMaxSize = 15
		Me.UnpackedFilesComboBox.TextIsPathFileNames = False
		'
		'UseInPreviewButton
		'
		Me.UseInPreviewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInPreviewButton.ButtonCanBeFocused = True
		Me.UseInPreviewButton.Enabled = False
		Me.UseInPreviewButton.Location = New System.Drawing.Point(518, 3)
		Me.UseInPreviewButton.Name = "UseInPreviewButton"
		Me.UseInPreviewButton.Size = New System.Drawing.Size(91, 23)
		Me.UseInPreviewButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.UseInPreviewButton.TabIndex = 2
		Me.UseInPreviewButton.Text = "Use in Preview"
		Me.UseInPreviewButton.UseVisualStyleBackColor = True
		'
		'UseInDecompileButton
		'
		Me.UseInDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInDecompileButton.ButtonCanBeFocused = True
		Me.UseInDecompileButton.Enabled = False
		Me.UseInDecompileButton.Location = New System.Drawing.Point(615, 3)
		Me.UseInDecompileButton.Name = "UseInDecompileButton"
		Me.UseInDecompileButton.Size = New System.Drawing.Size(106, 23)
		Me.UseInDecompileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.UseInDecompileButton.TabIndex = 3
		Me.UseInDecompileButton.Text = "Use in Decompile"
		Me.UseInDecompileButton.UseVisualStyleBackColor = True
		'
		'GotoUnpackedFileButton
		'
		Me.GotoUnpackedFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoUnpackedFileButton.ButtonCanBeFocused = True
		Me.GotoUnpackedFileButton.Location = New System.Drawing.Point(727, 3)
		Me.GotoUnpackedFileButton.Name = "GotoUnpackedFileButton"
		Me.GotoUnpackedFileButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoUnpackedFileButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.GotoUnpackedFileButton.TabIndex = 4
		Me.GotoUnpackedFileButton.Text = "Goto"
		Me.GotoUnpackedFileButton.UseVisualStyleBackColor = True
		'
		'UnpackUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel2)
		Me.Name = "UnpackUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.Options_LogSplitContainer.Panel1.ResumeLayout(False)
		Me.Options_LogSplitContainer.Panel2.ResumeLayout(False)
		CType(Me.Options_LogSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Options_LogSplitContainer.ResumeLayout(False)
		Me.SplitContainer2.Panel1.ResumeLayout(False)
		Me.SplitContainer2.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer2.ResumeLayout(False)
		Me.ContentsGroupBox.ResumeLayout(False)
		Me.ContentsGroupBoxFillPanel.ResumeLayout(False)
		Me.ContentsGroupBoxFillPanel.PerformLayout()
		Me.SplitContainer3.Panel1.ResumeLayout(False)
		Me.SplitContainer3.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer3.ResumeLayout(False)
		Me.Panel3.ResumeLayout(False)
		Me.ToolStrip1.ResumeLayout(False)
		Me.ToolStrip1.PerformLayout()
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.OptionsGroupBoxFillPanel.ResumeLayout(False)
		Me.OptionsGroupBoxFillPanel.PerformLayout()
		Me.UnpackButtonsPanel.ResumeLayout(False)
		Me.PostUnpackPanel.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents Panel2 As PanelEx
	Friend WithEvents GotoPackageButton As ButtonEx
	Friend WithEvents PackagesLabel As Crowbar.LabelEx
	Friend WithEvents BrowseForPackagePathFolderOrFileNameButton As ButtonEx
	Friend WithEvents PackagePathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents Options_LogSplitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents UseAllInDecompileButton As ButtonEx
	Friend WithEvents UnpackComboBox As ComboUserControl
	Friend WithEvents CancelUnpackButton As ButtonEx
	Friend WithEvents SkipCurrentPackageButton As ButtonEx
	Friend WithEvents UnpackButton As ButtonEx
	Friend WithEvents OptionsGroupBox As GroupBoxEx
	Friend WithEvents UseInDecompileButton As ButtonEx
	Friend WithEvents UseInPreviewButton As ButtonEx
	Friend WithEvents UnpackerLogTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents UnpackedFilesComboBox As ComboUserControl
	Friend WithEvents GotoUnpackedFileButton As ButtonEx
	Friend WithEvents ContentsGroupBox As GroupBoxEx
	Friend WithEvents PackageTreeView As Crowbar.TreeViewEx
	Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
	Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
	Friend WithEvents UnpackOptionsUseDefaultsButton As ButtonEx
	Friend WithEvents SelectionPathTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents SelectAllModelsAndMaterialsFoldersCheckBox As CheckBoxEx
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents Label3 As Crowbar.LabelEx
	Friend WithEvents GameSetupComboBox As ComboUserControl
	Friend WithEvents EditGameSetupButton As ButtonEx
	Friend WithEvents LogFileCheckBox As CheckBoxEx
	Friend WithEvents OptionsGroupBoxFillPanel As PanelEx
	Friend WithEvents PackageDataGridView As DataGridViewEx
	Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
	Friend WithEvents GameModelsOutputPathTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents GotoOutputPathButton As ButtonEx
	Friend WithEvents BrowseForOutputPathButton As ButtonEx
	Friend WithEvents OutputPathTextBox As Crowbar.RichTextBoxEx
    Friend WithEvents OutputPathComboBox As ComboUserControl
    Friend WithEvents Label2 As Crowbar.LabelEx
	Friend WithEvents UseDefaultOutputSubfolderButton As ButtonEx
	Friend WithEvents OutputSubfolderTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents OutputSamePathTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents FolderForEachPackageCheckBox As CheckBoxEx
	Friend WithEvents KeepFullPathCheckBox As CheckBoxEx
	Friend WithEvents PostUnpackPanel As PanelEx
	Friend WithEvents UnpackButtonsPanel As PanelEx
	Friend WithEvents Panel3 As PanelEx
	Friend WithEvents ContentsGroupBoxFillPanel As PanelEx
	Friend WithEvents ContentsMinScrollerPanel As PanelEx
	Friend WithEvents ToolStrip1 As ToolStripEx
	Friend WithEvents FindToolStripTextBox As ToolStripSpringTextBoxEx
	Friend WithEvents FindToolStripButton As ToolStripButtonEx
	Friend WithEvents ToolStripSeparator1 As ToolStripSeparatorEx
	Friend WithEvents FilesSelectedCountToolStripLabel As ToolStripLabelEx
	Friend WithEvents ToolStripSeparator3 As ToolStripSeparatorEx
	Friend WithEvents SizeSelectedTotalToolStripLabel As ToolStripLabelEx
	Friend WithEvents RefreshListingButton As ButtonEx
End Class
