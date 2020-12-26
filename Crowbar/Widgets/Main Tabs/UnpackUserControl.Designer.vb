<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UnpackUserControl
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
		Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.LogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.OutputSamePathTextBox = New Crowbar.TextBoxEx()
		Me.GameModelsOutputPathTextBox = New Crowbar.TextBoxEx()
		Me.UnpackComboBox = New System.Windows.Forms.ComboBox()
		Me.GotoOutputPathButton = New System.Windows.Forms.Button()
		Me.BrowseForOutputPathButton = New System.Windows.Forms.Button()
		Me.OutputPathTextBox = New Crowbar.TextBoxEx()
		Me.OutputSubfolderTextBox = New Crowbar.TextBoxEx()
		Me.OutputPathComboBox = New System.Windows.Forms.ComboBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.UseDefaultOutputSubfolderButton = New System.Windows.Forms.Button()
		Me.PackagesLabel = New System.Windows.Forms.Label()
		Me.PackagePathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.BrowseForPackagePathFolderOrFileNameButton = New System.Windows.Forms.Button()
		Me.GotoPackageButton = New System.Windows.Forms.Button()
		Me.Options_LogSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
		Me.ContentsGroupBox = New System.Windows.Forms.GroupBox()
		Me.ContentsGroupBoxFillPanel = New System.Windows.Forms.Panel()
		Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
		Me.PackageTreeView = New Crowbar.TreeViewEx()
		Me.PackageListView = New System.Windows.Forms.ListView()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.SelectionPathTextBox = New System.Windows.Forms.TextBox()
		Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.SearchToolStripComboBox = New System.Windows.Forms.ToolStripComboBox()
		Me.FindToolStripTextBox = New Crowbar.ToolStripSpringTextBox()
		Me.FindToolStripButton = New System.Windows.Forms.ToolStripButton()
		Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.FilesSelectedCountToolStripLabel = New System.Windows.Forms.ToolStripLabel()
		Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
		Me.SizeSelectedTotalToolStripLabel = New System.Windows.Forms.ToolStripLabel()
		Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
		Me.RefreshListingToolStripButton = New System.Windows.Forms.ToolStripButton()
		Me.ContentsMinScrollerPanel = New System.Windows.Forms.Panel()
		Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
		Me.OptionsGroupBoxFillPanel = New System.Windows.Forms.Panel()
		Me.KeepFullPathCheckBox = New System.Windows.Forms.CheckBox()
		Me.FolderForEachPackageCheckBox = New System.Windows.Forms.CheckBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.EditGameSetupButton = New System.Windows.Forms.Button()
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox()
		Me.SelectAllModelsAndMaterialsFoldersCheckBox = New System.Windows.Forms.CheckBox()
		Me.UnpackOptionsUseDefaultsButton = New System.Windows.Forms.Button()
		Me.UnpackerLogTextBox = New Crowbar.RichTextBoxEx()
		Me.UnpackButtonsPanel = New System.Windows.Forms.Panel()
		Me.UnpackButton = New System.Windows.Forms.Button()
		Me.SkipCurrentPackageButton = New System.Windows.Forms.Button()
		Me.CancelUnpackButton = New System.Windows.Forms.Button()
		Me.UseAllInDecompileButton = New System.Windows.Forms.Button()
		Me.PostUnpackPanel = New System.Windows.Forms.Panel()
		Me.UnpackedFilesComboBox = New System.Windows.Forms.ComboBox()
		Me.UseInPreviewButton = New System.Windows.Forms.Button()
		Me.UseInDecompileButton = New System.Windows.Forms.Button()
		Me.GotoUnpackedFileButton = New System.Windows.Forms.Button()
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
		Me.LogFileCheckBox.Location = New System.Drawing.Point(3, 72)
		Me.LogFileCheckBox.Name = "LogFileCheckBox"
		Me.LogFileCheckBox.Size = New System.Drawing.Size(116, 17)
		Me.LogFileCheckBox.TabIndex = 5
		Me.LogFileCheckBox.Text = "Write log to a file"
		Me.ToolTip1.SetToolTip(Me.LogFileCheckBox, "Write unpack log to a file.")
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'Panel2
		'
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
		Me.OutputSamePathTextBox.Location = New System.Drawing.Point(209, 32)
		Me.OutputSamePathTextBox.Name = "OutputSamePathTextBox"
		Me.OutputSamePathTextBox.ReadOnly = True
		Me.OutputSamePathTextBox.Size = New System.Drawing.Size(445, 22)
		Me.OutputSamePathTextBox.TabIndex = 26
		'
		'GameModelsOutputPathTextBox
		'
		Me.GameModelsOutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameModelsOutputPathTextBox.CueBannerText = ""
		Me.GameModelsOutputPathTextBox.Location = New System.Drawing.Point(209, 32)
		Me.GameModelsOutputPathTextBox.Name = "GameModelsOutputPathTextBox"
		Me.GameModelsOutputPathTextBox.ReadOnly = True
		Me.GameModelsOutputPathTextBox.Size = New System.Drawing.Size(445, 22)
		Me.GameModelsOutputPathTextBox.TabIndex = 15
		'
		'UnpackComboBox
		'
		Me.UnpackComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.UnpackComboBox.FormattingEnabled = True
		Me.UnpackComboBox.Location = New System.Drawing.Point(71, 4)
		Me.UnpackComboBox.Name = "UnpackComboBox"
		Me.UnpackComboBox.Size = New System.Drawing.Size(132, 21)
		Me.UnpackComboBox.TabIndex = 1
		'
		'GotoOutputPathButton
		'
		Me.GotoOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoOutputPathButton.Location = New System.Drawing.Point(730, 32)
		Me.GotoOutputPathButton.Name = "GotoOutputPathButton"
		Me.GotoOutputPathButton.Size = New System.Drawing.Size(43, 23)
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
		Me.OutputPathTextBox.Location = New System.Drawing.Point(209, 32)
		Me.OutputPathTextBox.Name = "OutputPathTextBox"
		Me.OutputPathTextBox.Size = New System.Drawing.Size(445, 22)
		Me.OutputPathTextBox.TabIndex = 16
		'
		'OutputSubfolderTextBox
		'
		Me.OutputSubfolderTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputSubfolderTextBox.CueBannerText = ""
		Me.OutputSubfolderTextBox.Location = New System.Drawing.Point(209, 32)
		Me.OutputSubfolderTextBox.Name = "OutputSubfolderTextBox"
		Me.OutputSubfolderTextBox.Size = New System.Drawing.Size(445, 22)
		Me.OutputSubfolderTextBox.TabIndex = 22
		Me.OutputSubfolderTextBox.Visible = False
		'
		'OutputPathComboBox
		'
		Me.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.OutputPathComboBox.FormattingEnabled = True
		Me.OutputPathComboBox.Location = New System.Drawing.Point(71, 33)
		Me.OutputPathComboBox.Name = "OutputPathComboBox"
		Me.OutputPathComboBox.Size = New System.Drawing.Size(132, 21)
		Me.OutputPathComboBox.TabIndex = 14
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
		Me.UseDefaultOutputSubfolderButton.Location = New System.Drawing.Point(660, 32)
		Me.UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton"
		Me.UseDefaultOutputSubfolderButton.Size = New System.Drawing.Size(113, 23)
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
		Me.PackagePathFileNameTextBox.Location = New System.Drawing.Point(209, 3)
		Me.PackagePathFileNameTextBox.Name = "PackagePathFileNameTextBox"
		Me.PackagePathFileNameTextBox.Size = New System.Drawing.Size(445, 22)
		Me.PackagePathFileNameTextBox.TabIndex = 2
		'
		'BrowseForPackagePathFolderOrFileNameButton
		'
		Me.BrowseForPackagePathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForPackagePathFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForPackagePathFolderOrFileNameButton.Name = "BrowseForPackagePathFolderOrFileNameButton"
		Me.BrowseForPackagePathFolderOrFileNameButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForPackagePathFolderOrFileNameButton.TabIndex = 3
		Me.BrowseForPackagePathFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForPackagePathFolderOrFileNameButton.UseVisualStyleBackColor = True
		'
		'GotoPackageButton
		'
		Me.GotoPackageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoPackageButton.Location = New System.Drawing.Point(730, 3)
		Me.GotoPackageButton.Name = "GotoPackageButton"
		Me.GotoPackageButton.Size = New System.Drawing.Size(43, 23)
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
		Me.SplitContainer2.SplitterDistance = 600
		Me.SplitContainer2.SplitterWidth = 6
		Me.SplitContainer2.TabIndex = 0
		'
		'ContentsGroupBox
		'
		Me.ContentsGroupBox.Controls.Add(Me.ContentsGroupBoxFillPanel)
		Me.ContentsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ContentsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.ContentsGroupBox.Name = "ContentsGroupBox"
		Me.ContentsGroupBox.Size = New System.Drawing.Size(600, 347)
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
		Me.ContentsGroupBoxFillPanel.Size = New System.Drawing.Size(594, 326)
		Me.ContentsGroupBoxFillPanel.TabIndex = 12
		'
		'SplitContainer3
		'
		Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer3.Location = New System.Drawing.Point(0, 26)
		Me.SplitContainer3.Name = "SplitContainer3"
		'
		'SplitContainer3.Panel1
		'
		Me.SplitContainer3.Panel1.Controls.Add(Me.PackageTreeView)
		'
		'SplitContainer3.Panel2
		'
		Me.SplitContainer3.Panel2.Controls.Add(Me.PackageListView)
		Me.SplitContainer3.Size = New System.Drawing.Size(594, 275)
		Me.SplitContainer3.SplitterDistance = 250
		Me.SplitContainer3.TabIndex = 6
		'
		'PackageTreeView
		'
		Me.PackageTreeView.BackColor = System.Drawing.SystemColors.Control
		Me.PackageTreeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PackageTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
		Me.PackageTreeView.HideSelection = False
		Me.PackageTreeView.ImageIndex = 0
		Me.PackageTreeView.ImageList = Me.ImageList1
		Me.PackageTreeView.Location = New System.Drawing.Point(0, 0)
		Me.PackageTreeView.Name = "PackageTreeView"
		Me.PackageTreeView.SelectedImageIndex = 0
		Me.PackageTreeView.Size = New System.Drawing.Size(250, 275)
		Me.PackageTreeView.TabIndex = 0
		'
		'PackageListView
		'
		Me.PackageListView.AllowColumnReorder = True
		Me.PackageListView.BackColor = System.Drawing.SystemColors.Control
		Me.PackageListView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PackageListView.HideSelection = False
		Me.PackageListView.Location = New System.Drawing.Point(0, 0)
		Me.PackageListView.Name = "PackageListView"
		Me.PackageListView.ShowGroups = False
		Me.PackageListView.Size = New System.Drawing.Size(340, 275)
		Me.PackageListView.SmallImageList = Me.ImageList1
		Me.PackageListView.Sorting = System.Windows.Forms.SortOrder.Ascending
		Me.PackageListView.TabIndex = 1
		Me.PackageListView.UseCompatibleStateImageBehavior = False
		Me.PackageListView.View = System.Windows.Forms.View.Details
		'
		'Panel3
		'
		Me.Panel3.Controls.Add(Me.SelectionPathTextBox)
		Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel3.Location = New System.Drawing.Point(0, 0)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(594, 26)
		Me.Panel3.TabIndex = 11
		'
		'SelectionPathTextBox
		'
		Me.SelectionPathTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SelectionPathTextBox.Location = New System.Drawing.Point(0, 0)
		Me.SelectionPathTextBox.Name = "SelectionPathTextBox"
		Me.SelectionPathTextBox.ReadOnly = True
		Me.SelectionPathTextBox.Size = New System.Drawing.Size(594, 22)
		Me.SelectionPathTextBox.TabIndex = 1
		'
		'ToolStrip1
		'
		Me.ToolStrip1.CanOverflow = False
		Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SearchToolStripComboBox, Me.FindToolStripTextBox, Me.FindToolStripButton, Me.ToolStripSeparator1, Me.FilesSelectedCountToolStripLabel, Me.ToolStripSeparator3, Me.SizeSelectedTotalToolStripLabel, Me.ToolStripSeparator2, Me.RefreshListingToolStripButton})
		Me.ToolStrip1.Location = New System.Drawing.Point(0, 301)
		Me.ToolStrip1.Name = "ToolStrip1"
		Me.ToolStrip1.Size = New System.Drawing.Size(594, 25)
		Me.ToolStrip1.Stretch = True
		Me.ToolStrip1.TabIndex = 10
		Me.ToolStrip1.Text = "ToolStrip1"
		'
		'SearchToolStripComboBox
		'
		Me.SearchToolStripComboBox.Name = "SearchToolStripComboBox"
		Me.SearchToolStripComboBox.Size = New System.Drawing.Size(121, 25)
		Me.SearchToolStripComboBox.ToolTipText = "What to search"
		'
		'FindToolStripTextBox
		'
		Me.FindToolStripTextBox.Name = "FindToolStripTextBox"
		Me.FindToolStripTextBox.Size = New System.Drawing.Size(336, 25)
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
		Me.FilesSelectedCountToolStripLabel.Size = New System.Drawing.Size(24, 22)
		Me.FilesSelectedCountToolStripLabel.Text = "0/0"
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
		Me.SizeSelectedTotalToolStripLabel.Size = New System.Drawing.Size(13, 22)
		Me.SizeSelectedTotalToolStripLabel.Text = "0"
		Me.SizeSelectedTotalToolStripLabel.ToolTipText = "Byte count of selected items"
		'
		'ToolStripSeparator2
		'
		Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
		Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
		'
		'RefreshListingToolStripButton
		'
		Me.RefreshListingToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.RefreshListingToolStripButton.Image = Global.Crowbar.My.Resources.Resources.Refresh
		Me.RefreshListingToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.RefreshListingToolStripButton.Name = "RefreshListingToolStripButton"
		Me.RefreshListingToolStripButton.Size = New System.Drawing.Size(23, 22)
		Me.RefreshListingToolStripButton.Text = "Refresh"
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
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.Size = New System.Drawing.Size(164, 347)
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
		Me.OptionsGroupBoxFillPanel.Size = New System.Drawing.Size(158, 326)
		Me.OptionsGroupBoxFillPanel.TabIndex = 0
		'
		'KeepFullPathCheckBox
		'
		Me.KeepFullPathCheckBox.AutoSize = True
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
		Me.EditGameSetupButton.Location = New System.Drawing.Point(6445, 229)
		Me.EditGameSetupButton.Name = "EditGameSetupButton"
		Me.EditGameSetupButton.Size = New System.Drawing.Size(90, 23)
		Me.EditGameSetupButton.TabIndex = 1
		Me.EditGameSetupButton.Text = "Set Up Games"
		Me.EditGameSetupButton.UseVisualStyleBackColor = True
		Me.EditGameSetupButton.Visible = False
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(3, 255)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(6532, 21)
		Me.GameSetupComboBox.TabIndex = 2
		Me.GameSetupComboBox.Visible = False
		'
		'SelectAllModelsAndMaterialsFoldersCheckBox
		'
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.AutoSize = True
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
		Me.UnpackOptionsUseDefaultsButton.Location = New System.Drawing.Point(33, 203)
		Me.UnpackOptionsUseDefaultsButton.Name = "UnpackOptionsUseDefaultsButton"
		Me.UnpackOptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.UnpackOptionsUseDefaultsButton.TabIndex = 6
		Me.UnpackOptionsUseDefaultsButton.Text = "Use Defaults"
		Me.UnpackOptionsUseDefaultsButton.UseVisualStyleBackColor = True
		Me.UnpackOptionsUseDefaultsButton.Visible = False
		'
		'UnpackerLogTextBox
		'
		Me.UnpackerLogTextBox.CueBannerText = ""
		Me.UnpackerLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.UnpackerLogTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.UnpackerLogTextBox.HideSelection = False
		Me.UnpackerLogTextBox.Location = New System.Drawing.Point(0, 26)
		Me.UnpackerLogTextBox.Name = "UnpackerLogTextBox"
		Me.UnpackerLogTextBox.ReadOnly = True
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
		Me.UnpackButton.Enabled = False
		Me.UnpackButton.Location = New System.Drawing.Point(0, 0)
		Me.UnpackButton.Name = "UnpackButton"
		Me.UnpackButton.Size = New System.Drawing.Size(120, 23)
		Me.UnpackButton.TabIndex = 2
		Me.UnpackButton.Text = "Unpack"
		Me.UnpackButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentPackageButton
		'
		Me.SkipCurrentPackageButton.Enabled = False
		Me.SkipCurrentPackageButton.Location = New System.Drawing.Point(126, 0)
		Me.SkipCurrentPackageButton.Name = "SkipCurrentPackageButton"
		Me.SkipCurrentPackageButton.Size = New System.Drawing.Size(120, 23)
		Me.SkipCurrentPackageButton.TabIndex = 3
		Me.SkipCurrentPackageButton.Text = "Skip Current Package"
		Me.SkipCurrentPackageButton.UseVisualStyleBackColor = True
		'
		'CancelUnpackButton
		'
		Me.CancelUnpackButton.Enabled = False
		Me.CancelUnpackButton.Location = New System.Drawing.Point(252, 0)
		Me.CancelUnpackButton.Name = "CancelUnpackButton"
		Me.CancelUnpackButton.Size = New System.Drawing.Size(120, 23)
		Me.CancelUnpackButton.TabIndex = 4
		Me.CancelUnpackButton.Text = "Cancel Unpack"
		Me.CancelUnpackButton.UseVisualStyleBackColor = True
		'
		'UseAllInDecompileButton
		'
		Me.UseAllInDecompileButton.Enabled = False
		Me.UseAllInDecompileButton.Location = New System.Drawing.Point(378, 0)
		Me.UseAllInDecompileButton.Name = "UseAllInDecompileButton"
		Me.UseAllInDecompileButton.Size = New System.Drawing.Size(120, 23)
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
		Me.UnpackedFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.UnpackedFilesComboBox.FormattingEnabled = True
		Me.UnpackedFilesComboBox.Location = New System.Drawing.Point(0, 4)
		Me.UnpackedFilesComboBox.Name = "UnpackedFilesComboBox"
		Me.UnpackedFilesComboBox.Size = New System.Drawing.Size(512, 21)
		Me.UnpackedFilesComboBox.TabIndex = 1
		'
		'UseInPreviewButton
		'
		Me.UseInPreviewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInPreviewButton.Enabled = False
		Me.UseInPreviewButton.Location = New System.Drawing.Point(518, 3)
		Me.UseInPreviewButton.Name = "UseInPreviewButton"
		Me.UseInPreviewButton.Size = New System.Drawing.Size(91, 23)
		Me.UseInPreviewButton.TabIndex = 2
		Me.UseInPreviewButton.Text = "Use in Preview"
		Me.UseInPreviewButton.UseVisualStyleBackColor = True
		'
		'UseInDecompileButton
		'
		Me.UseInDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInDecompileButton.Enabled = False
		Me.UseInDecompileButton.Location = New System.Drawing.Point(615, 3)
		Me.UseInDecompileButton.Name = "UseInDecompileButton"
		Me.UseInDecompileButton.Size = New System.Drawing.Size(106, 23)
		Me.UseInDecompileButton.TabIndex = 3
		Me.UseInDecompileButton.Text = "Use in Decompile"
		Me.UseInDecompileButton.UseVisualStyleBackColor = True
		'
		'GotoUnpackedFileButton
		'
		Me.GotoUnpackedFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoUnpackedFileButton.Location = New System.Drawing.Point(727, 3)
		Me.GotoUnpackedFileButton.Name = "GotoUnpackedFileButton"
		Me.GotoUnpackedFileButton.Size = New System.Drawing.Size(43, 23)
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
		Me.Panel3.PerformLayout()
		Me.ToolStrip1.ResumeLayout(False)
		Me.ToolStrip1.PerformLayout()
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.OptionsGroupBoxFillPanel.ResumeLayout(False)
		Me.OptionsGroupBoxFillPanel.PerformLayout()
		Me.UnpackButtonsPanel.ResumeLayout(False)
		Me.PostUnpackPanel.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents GotoPackageButton As System.Windows.Forms.Button
	Friend WithEvents PackagesLabel As System.Windows.Forms.Label
	Friend WithEvents BrowseForPackagePathFolderOrFileNameButton As System.Windows.Forms.Button
	Friend WithEvents PackagePathFileNameTextBox As Crowbar.TextBoxEx
	Friend WithEvents Options_LogSplitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents UseAllInDecompileButton As System.Windows.Forms.Button
	Friend WithEvents UnpackComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents CancelUnpackButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentPackageButton As System.Windows.Forms.Button
	Friend WithEvents UnpackButton As System.Windows.Forms.Button
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents UseInDecompileButton As System.Windows.Forms.Button
	Friend WithEvents UseInPreviewButton As System.Windows.Forms.Button
	Friend WithEvents UnpackerLogTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents UnpackedFilesComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoUnpackedFileButton As System.Windows.Forms.Button
	Friend WithEvents ContentsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents PackageTreeView As Crowbar.TreeViewEx
	Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
	Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
	Friend WithEvents UnpackOptionsUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents SelectionPathTextBox As System.Windows.Forms.TextBox
	Friend WithEvents SelectAllModelsAndMaterialsFoldersCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents EditGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents LogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
	Friend WithEvents FilesSelectedCountToolStripLabel As System.Windows.Forms.ToolStripLabel
	Friend WithEvents FindToolStripTextBox As ToolStripSpringTextBox
	Friend WithEvents FindToolStripButton As System.Windows.Forms.ToolStripButton
	Friend WithEvents SizeSelectedTotalToolStripLabel As System.Windows.Forms.ToolStripLabel
	Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents OptionsGroupBoxFillPanel As System.Windows.Forms.Panel
	Friend WithEvents PackageListView As System.Windows.Forms.ListView
	Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
	Friend WithEvents GameModelsOutputPathTextBox As Crowbar.TextBoxEx
	Friend WithEvents GotoOutputPathButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForOutputPathButton As System.Windows.Forms.Button
	Friend WithEvents OutputPathTextBox As Crowbar.TextBoxEx
	Friend WithEvents OutputPathComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents UseDefaultOutputSubfolderButton As System.Windows.Forms.Button
	Friend WithEvents OutputSubfolderTextBox As Crowbar.TextBoxEx
	Friend WithEvents OutputSamePathTextBox As TextBoxEx
	Friend WithEvents FolderForEachPackageCheckBox As CheckBox
	Friend WithEvents RefreshListingToolStripButton As ToolStripButton
	Friend WithEvents PostUnpackPanel As Panel
	Friend WithEvents UnpackButtonsPanel As Panel
	Friend WithEvents Panel3 As Panel
	Friend WithEvents ContentsGroupBoxFillPanel As Panel
	Friend WithEvents ContentsMinScrollerPanel As Panel
	Friend WithEvents KeepFullPathCheckBox As CheckBox
	Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
	Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
	Friend WithEvents SearchToolStripComboBox As ToolStripComboBox
End Class
