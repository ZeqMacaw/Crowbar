<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PublishUserControl
	Inherits BaseUserControl

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PublishUserControl))
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.RefreshGameItemsButton = New System.Windows.Forms.Button()
		Me.ItemOwnerLabel = New System.Windows.Forms.Label()
		Me.ItemPostedTextBox = New Crowbar.DateTimeTextBoxEx()
		Me.ItemUpdatedTextBox = New Crowbar.DateTimeTextBoxEx()
		Me.QuotaProgressBar = New Crowbar.ProgressBarEx()
		Me.TopMiddleSplitContainer = New Crowbar.SplitContainerEx()
		Me.ItemsPanel = New System.Windows.Forms.Panel()
		Me.ItemsDataGridView = New System.Windows.Forms.DataGridView()
		Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.AddItemToolStripButton = New System.Windows.Forms.ToolStripButton()
		Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.SearchItemsToolStripComboBox = New System.Windows.Forms.ToolStripComboBox()
		Me.SearchItemsToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
		Me.SearchItemsToolStripButton = New System.Windows.Forms.ToolStripButton()
		Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
		Me.ItemCountsToolStripLabel = New System.Windows.Forms.ToolStripLabel()
		Me.FindItemToolStripTextBox = New Crowbar.ToolStripSpringTextBox()
		Me.GamePanel = New System.Windows.Forms.Panel()
		Me.GameLabel = New System.Windows.Forms.Label()
		Me.AppIdComboBox = New System.Windows.Forms.ComboBox()
		Me.PublishRequiresSteamLabel = New System.Windows.Forms.Label()
		Me.OpenSteamSubscriberAgreementButton = New System.Windows.Forms.Button()
		Me.MiddleBottomSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.ItemGroupBox = New Crowbar.GroupBoxEx()
		Me.ItemTagsSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.DescriptionChangeNoteSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.ItemDescriptionTextBox = New Crowbar.RichTextBoxEx()
		Me.ItemDescriptionTopPanel = New System.Windows.Forms.Panel()
		Me.ToggleWordWrapForDescriptionCheckBox = New System.Windows.Forms.CheckBox()
		Me.ItemDescriptionLabel = New System.Windows.Forms.Label()
		Me.ItemChangeNoteTextBox = New Crowbar.RichTextBoxEx()
		Me.ItemChangeNoteTopPanel = New System.Windows.Forms.Panel()
		Me.ToggleWordWrapForChangeNotePanel = New System.Windows.Forms.Panel()
		Me.ToggleWordWrapForChangeNoteCheckBox = New System.Windows.Forms.CheckBox()
		Me.ItemChangeNoteLabel = New System.Windows.Forms.Label()
		Me.ItemTopPanel = New System.Windows.Forms.Panel()
		Me.ItemIDLabel = New System.Windows.Forms.Label()
		Me.ItemIDTextBox = New Crowbar.TextBoxEx()
		Me.ItemOwnerTextBox = New Crowbar.TextBoxEx()
		Me.ItemTitleLabel = New System.Windows.Forms.Label()
		Me.ItemTitleTextBox = New Crowbar.TextBoxEx()
		Me.ItemBottomPanel = New System.Windows.Forms.Panel()
		Me.ItemContentFolderOrFileLabel = New System.Windows.Forms.Label()
		Me.ItemContentPathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.BrowseItemContentPathFileNameButton = New System.Windows.Forms.Button()
		Me.ItemPreviewImageLabel = New System.Windows.Forms.Label()
		Me.ItemPreviewImagePathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.BrowseItemPreviewImagePathFileNameButton = New System.Windows.Forms.Button()
		Me.ItemPreviewImagePictureBox = New System.Windows.Forms.PictureBox()
		Me.ItemVisibilityComboBox = New System.Windows.Forms.ComboBox()
		Me.ItemVisibilityLabel = New System.Windows.Forms.Label()
		Me.SaveAsTemplateOrDraftItemButton = New System.Windows.Forms.Button()
		Me.RefreshOrRevertItemButton = New System.Windows.Forms.Button()
		Me.SaveTemplateButton = New System.Windows.Forms.Button()
		Me.OpenWorkshopPageButton = New System.Windows.Forms.Button()
		Me.DeleteItemButton = New System.Windows.Forms.Button()
		Me.ItemLeftMinScrollPanel = New System.Windows.Forms.Panel()
		Me.ItemTagsGroupBox = New Crowbar.GroupBoxEx()
		Me.LogTextBox = New Crowbar.RichTextBoxEx()
		Me.PublishItemButton = New System.Windows.Forms.Button()
		Me.QueueListView = New System.Windows.Forms.ListView()
		CType(Me.TopMiddleSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TopMiddleSplitContainer.Panel1.SuspendLayout()
		Me.TopMiddleSplitContainer.Panel2.SuspendLayout()
		Me.TopMiddleSplitContainer.SuspendLayout()
		Me.ItemsPanel.SuspendLayout()
		CType(Me.ItemsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.ToolStrip1.SuspendLayout()
		Me.GamePanel.SuspendLayout()
		CType(Me.MiddleBottomSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.MiddleBottomSplitContainer.Panel1.SuspendLayout()
		Me.MiddleBottomSplitContainer.Panel2.SuspendLayout()
		Me.MiddleBottomSplitContainer.SuspendLayout()
		Me.ItemGroupBox.SuspendLayout()
		CType(Me.ItemTagsSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.ItemTagsSplitContainer.Panel1.SuspendLayout()
		Me.ItemTagsSplitContainer.Panel2.SuspendLayout()
		Me.ItemTagsSplitContainer.SuspendLayout()
		CType(Me.DescriptionChangeNoteSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.DescriptionChangeNoteSplitContainer.Panel1.SuspendLayout()
		Me.DescriptionChangeNoteSplitContainer.Panel2.SuspendLayout()
		Me.DescriptionChangeNoteSplitContainer.SuspendLayout()
		Me.ItemDescriptionTopPanel.SuspendLayout()
		Me.ItemChangeNoteTopPanel.SuspendLayout()
		Me.ToggleWordWrapForChangeNotePanel.SuspendLayout()
		Me.ItemTopPanel.SuspendLayout()
		Me.ItemBottomPanel.SuspendLayout()
		CType(Me.ItemPreviewImagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'RefreshGameItemsButton
		'
		Me.RefreshGameItemsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.RefreshGameItemsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.RefreshGameItemsButton.FlatAppearance.BorderSize = 0
		Me.RefreshGameItemsButton.Image = Global.Crowbar.My.Resources.Resources.Refresh
		Me.RefreshGameItemsButton.Location = New System.Drawing.Point(394, 3)
		Me.RefreshGameItemsButton.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.RefreshGameItemsButton.Name = "RefreshGameItemsButton"
		Me.RefreshGameItemsButton.Padding = New System.Windows.Forms.Padding(0, 0, 1, 2)
		Me.RefreshGameItemsButton.Size = New System.Drawing.Size(23, 22)
		Me.RefreshGameItemsButton.TabIndex = 36
		Me.ToolTip1.SetToolTip(Me.RefreshGameItemsButton, "Refresh Game Items")
		Me.RefreshGameItemsButton.UseVisualStyleBackColor = True
		'
		'ItemOwnerLabel
		'
		Me.ItemOwnerLabel.Location = New System.Drawing.Point(150, 4)
		Me.ItemOwnerLabel.Name = "ItemOwnerLabel"
		Me.ItemOwnerLabel.Size = New System.Drawing.Size(45, 13)
		Me.ItemOwnerLabel.TabIndex = 35
		Me.ItemOwnerLabel.Text = "Owner:"
		Me.ToolTip1.SetToolTip(Me.ItemOwnerLabel, "Double-click to swap between Steam Name and Steam ID.")
		'
		'ItemPostedTextBox
		'
		Me.ItemPostedTextBox.CueBannerText = ""
		Me.ItemPostedTextBox.Location = New System.Drawing.Point(334, 0)
		Me.ItemPostedTextBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemPostedTextBox.Name = "ItemPostedTextBox"
		Me.ItemPostedTextBox.ReadOnly = True
		Me.ItemPostedTextBox.Size = New System.Drawing.Size(120, 22)
		Me.ItemPostedTextBox.TabIndex = 2
		Me.ToolTip1.SetToolTip(Me.ItemPostedTextBox, "Posted")
		'
		'ItemUpdatedTextBox
		'
		Me.ItemUpdatedTextBox.CueBannerText = ""
		Me.ItemUpdatedTextBox.Location = New System.Drawing.Point(460, 0)
		Me.ItemUpdatedTextBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemUpdatedTextBox.Name = "ItemUpdatedTextBox"
		Me.ItemUpdatedTextBox.ReadOnly = True
		Me.ItemUpdatedTextBox.Size = New System.Drawing.Size(120, 22)
		Me.ItemUpdatedTextBox.TabIndex = 3
		Me.ToolTip1.SetToolTip(Me.ItemUpdatedTextBox, "Updated")
		'
		'QuotaProgressBar
		'
		Me.QuotaProgressBar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.QuotaProgressBar.Location = New System.Drawing.Point(3, 31)
		Me.QuotaProgressBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.QuotaProgressBar.Name = "QuotaProgressBar"
		Me.QuotaProgressBar.Size = New System.Drawing.Size(125, 22)
		Me.QuotaProgressBar.TabIndex = 37
		Me.ToolTip1.SetToolTip(Me.QuotaProgressBar, "Quota")
		'
		'TopMiddleSplitContainer
		'
		Me.TopMiddleSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TopMiddleSplitContainer.Location = New System.Drawing.Point(0, 0)
		Me.TopMiddleSplitContainer.Margin = New System.Windows.Forms.Padding(2)
		Me.TopMiddleSplitContainer.Name = "TopMiddleSplitContainer"
		Me.TopMiddleSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'TopMiddleSplitContainer.Panel1
		'
		Me.TopMiddleSplitContainer.Panel1.Controls.Add(Me.ItemsPanel)
		Me.TopMiddleSplitContainer.Panel1.Controls.Add(Me.GamePanel)
		Me.TopMiddleSplitContainer.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
		'
		'TopMiddleSplitContainer.Panel2
		'
		Me.TopMiddleSplitContainer.Panel2.AutoScroll = True
		Me.TopMiddleSplitContainer.Panel2.Controls.Add(Me.MiddleBottomSplitContainer)
		Me.TopMiddleSplitContainer.Size = New System.Drawing.Size(770, 534)
		Me.TopMiddleSplitContainer.SplitterDistance = 139
		Me.TopMiddleSplitContainer.TabIndex = 28
		'
		'ItemsPanel
		'
		Me.ItemsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.ItemsPanel.Controls.Add(Me.ItemsDataGridView)
		Me.ItemsPanel.Controls.Add(Me.ToolStrip1)
		Me.ItemsPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ItemsPanel.Location = New System.Drawing.Point(3, 26)
		Me.ItemsPanel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemsPanel.Name = "ItemsPanel"
		Me.ItemsPanel.Size = New System.Drawing.Size(764, 110)
		Me.ItemsPanel.TabIndex = 31
		'
		'ItemsDataGridView
		'
		Me.ItemsDataGridView.AllowUserToAddRows = False
		Me.ItemsDataGridView.AllowUserToDeleteRows = False
		Me.ItemsDataGridView.AllowUserToOrderColumns = True
		Me.ItemsDataGridView.AllowUserToResizeRows = False
		Me.ItemsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.ItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.ItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ItemsDataGridView.Location = New System.Drawing.Point(0, 0)
		Me.ItemsDataGridView.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemsDataGridView.MultiSelect = False
		Me.ItemsDataGridView.Name = "ItemsDataGridView"
		Me.ItemsDataGridView.ReadOnly = True
		Me.ItemsDataGridView.RowHeadersVisible = False
		Me.ItemsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.ItemsDataGridView.ShowCellErrors = False
		Me.ItemsDataGridView.ShowRowErrors = False
		Me.ItemsDataGridView.Size = New System.Drawing.Size(760, 81)
		Me.ItemsDataGridView.TabIndex = 3
		'
		'ToolStrip1
		'
		Me.ToolStrip1.CanOverflow = False
		Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddItemToolStripButton, Me.ToolStripSeparator1, Me.SearchItemsToolStripComboBox, Me.SearchItemsToolStripTextBox, Me.SearchItemsToolStripButton, Me.ToolStripSeparator2, Me.ItemCountsToolStripLabel, Me.FindItemToolStripTextBox})
		Me.ToolStrip1.Location = New System.Drawing.Point(0, 81)
		Me.ToolStrip1.Name = "ToolStrip1"
		Me.ToolStrip1.Size = New System.Drawing.Size(760, 25)
		Me.ToolStrip1.Stretch = True
		Me.ToolStrip1.TabIndex = 30
		Me.ToolStrip1.Text = "ToolStrip1"
		'
		'AddItemToolStripButton
		'
		Me.AddItemToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.AddItemToolStripButton.Image = CType(resources.GetObject("AddItemToolStripButton.Image"), System.Drawing.Image)
		Me.AddItemToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.AddItemToolStripButton.Name = "AddItemToolStripButton"
		Me.AddItemToolStripButton.Size = New System.Drawing.Size(60, 22)
		Me.AddItemToolStripButton.Text = "Add Item"
		'
		'ToolStripSeparator1
		'
		Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
		Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
		'
		'SearchItemsToolStripComboBox
		'
		Me.SearchItemsToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.SearchItemsToolStripComboBox.Items.AddRange(New Object() {"ID:", "Title:", "Description:", "Owner:", "[All fields]:"})
		Me.SearchItemsToolStripComboBox.Name = "SearchItemsToolStripComboBox"
		Me.SearchItemsToolStripComboBox.Size = New System.Drawing.Size(80, 25)
		Me.SearchItemsToolStripComboBox.ToolTipText = "Field to search"
		'
		'SearchItemsToolStripTextBox
		'
		Me.SearchItemsToolStripTextBox.Name = "SearchItemsToolStripTextBox"
		Me.SearchItemsToolStripTextBox.Size = New System.Drawing.Size(100, 25)
		Me.SearchItemsToolStripTextBox.ToolTipText = "Text to search for"
		'
		'SearchItemsToolStripButton
		'
		Me.SearchItemsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.SearchItemsToolStripButton.Image = Global.Crowbar.My.Resources.Resources.Find
		Me.SearchItemsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.SearchItemsToolStripButton.Name = "SearchItemsToolStripButton"
		Me.SearchItemsToolStripButton.Size = New System.Drawing.Size(23, 22)
		Me.SearchItemsToolStripButton.Text = "Search"
		Me.SearchItemsToolStripButton.ToolTipText = "Search"
		'
		'ToolStripSeparator2
		'
		Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
		Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
		'
		'ItemCountsToolStripLabel
		'
		Me.ItemCountsToolStripLabel.Name = "ItemCountsToolStripLabel"
		Me.ItemCountsToolStripLabel.Size = New System.Drawing.Size(168, 22)
		Me.ItemCountsToolStripLabel.Text = "0 drafts + 0 published = 0 total"
		'
		'FindItemToolStripTextBox
		'
		Me.FindItemToolStripTextBox.Name = "FindItemToolStripTextBox"
		Me.FindItemToolStripTextBox.Size = New System.Drawing.Size(279, 25)
		Me.FindItemToolStripTextBox.ToolTipText = "Title to find"
		Me.FindItemToolStripTextBox.Visible = False
		'
		'GamePanel
		'
		Me.GamePanel.Controls.Add(Me.GameLabel)
		Me.GamePanel.Controls.Add(Me.AppIdComboBox)
		Me.GamePanel.Controls.Add(Me.RefreshGameItemsButton)
		Me.GamePanel.Controls.Add(Me.PublishRequiresSteamLabel)
		Me.GamePanel.Controls.Add(Me.OpenSteamSubscriberAgreementButton)
		Me.GamePanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.GamePanel.Location = New System.Drawing.Point(3, 0)
		Me.GamePanel.Name = "GamePanel"
		Me.GamePanel.Size = New System.Drawing.Size(764, 26)
		Me.GamePanel.TabIndex = 37
		'
		'GameLabel
		'
		Me.GameLabel.Location = New System.Drawing.Point(0, 7)
		Me.GameLabel.Name = "GameLabel"
		Me.GameLabel.Size = New System.Drawing.Size(39, 13)
		Me.GameLabel.TabIndex = 22
		Me.GameLabel.Text = "Game:"
		'
		'AppIdComboBox
		'
		Me.AppIdComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.AppIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.AppIdComboBox.FormattingEnabled = True
		Me.AppIdComboBox.Location = New System.Drawing.Point(45, 3)
		Me.AppIdComboBox.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
		Me.AppIdComboBox.Name = "AppIdComboBox"
		Me.AppIdComboBox.Size = New System.Drawing.Size(349, 21)
		Me.AppIdComboBox.TabIndex = 0
		'
		'PublishRequiresSteamLabel
		'
		Me.PublishRequiresSteamLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.PublishRequiresSteamLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.PublishRequiresSteamLabel.Location = New System.Drawing.Point(423, 3)
		Me.PublishRequiresSteamLabel.Name = "PublishRequiresSteamLabel"
		Me.PublishRequiresSteamLabel.Size = New System.Drawing.Size(136, 21)
		Me.PublishRequiresSteamLabel.TabIndex = 1
		Me.PublishRequiresSteamLabel.Text = "Publish requires Steam"
		Me.PublishRequiresSteamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'OpenSteamSubscriberAgreementButton
		'
		Me.OpenSteamSubscriberAgreementButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OpenSteamSubscriberAgreementButton.Location = New System.Drawing.Point(565, 3)
		Me.OpenSteamSubscriberAgreementButton.Name = "OpenSteamSubscriberAgreementButton"
		Me.OpenSteamSubscriberAgreementButton.Size = New System.Drawing.Size(199, 22)
		Me.OpenSteamSubscriberAgreementButton.TabIndex = 2
		Me.OpenSteamSubscriberAgreementButton.Text = "Open Steam Subscriber Agreement"
		Me.OpenSteamSubscriberAgreementButton.UseVisualStyleBackColor = True
		'
		'MiddleBottomSplitContainer
		'
		Me.MiddleBottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.MiddleBottomSplitContainer.Location = New System.Drawing.Point(0, 0)
		Me.MiddleBottomSplitContainer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.MiddleBottomSplitContainer.Name = "MiddleBottomSplitContainer"
		Me.MiddleBottomSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'MiddleBottomSplitContainer.Panel1
		'
		Me.MiddleBottomSplitContainer.Panel1.Controls.Add(Me.ItemGroupBox)
		Me.MiddleBottomSplitContainer.Panel1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
		'
		'MiddleBottomSplitContainer.Panel2
		'
		Me.MiddleBottomSplitContainer.Panel2.Controls.Add(Me.LogTextBox)
		Me.MiddleBottomSplitContainer.Panel2.Controls.Add(Me.QuotaProgressBar)
		Me.MiddleBottomSplitContainer.Panel2.Controls.Add(Me.PublishItemButton)
		Me.MiddleBottomSplitContainer.Panel2.Controls.Add(Me.QueueListView)
		Me.MiddleBottomSplitContainer.Panel2MinSize = 45
		Me.MiddleBottomSplitContainer.Size = New System.Drawing.Size(770, 391)
		Me.MiddleBottomSplitContainer.SplitterDistance = 324
		Me.MiddleBottomSplitContainer.TabIndex = 26
		'
		'ItemGroupBox
		'
		Me.ItemGroupBox.Controls.Add(Me.ItemTagsSplitContainer)
		Me.ItemGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ItemGroupBox.IsReadOnly = False
		Me.ItemGroupBox.Location = New System.Drawing.Point(3, 4)
		Me.ItemGroupBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemGroupBox.Name = "ItemGroupBox"
		Me.ItemGroupBox.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemGroupBox.SelectedValue = Nothing
		Me.ItemGroupBox.Size = New System.Drawing.Size(764, 316)
		Me.ItemGroupBox.TabIndex = 31
		Me.ItemGroupBox.TabStop = False
		Me.ItemGroupBox.Text = "Item"
		'
		'ItemTagsSplitContainer
		'
		Me.ItemTagsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ItemTagsSplitContainer.Location = New System.Drawing.Point(3, 19)
		Me.ItemTagsSplitContainer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemTagsSplitContainer.Name = "ItemTagsSplitContainer"
		'
		'ItemTagsSplitContainer.Panel1
		'
		Me.ItemTagsSplitContainer.Panel1.AutoScroll = True
		Me.ItemTagsSplitContainer.Panel1.Controls.Add(Me.DescriptionChangeNoteSplitContainer)
		Me.ItemTagsSplitContainer.Panel1.Controls.Add(Me.ItemTopPanel)
		Me.ItemTagsSplitContainer.Panel1.Controls.Add(Me.ItemBottomPanel)
		Me.ItemTagsSplitContainer.Panel1.Controls.Add(Me.ItemLeftMinScrollPanel)
		'
		'ItemTagsSplitContainer.Panel2
		'
		Me.ItemTagsSplitContainer.Panel2.Controls.Add(Me.ItemTagsGroupBox)
		Me.ItemTagsSplitContainer.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 3, 4)
		Me.ItemTagsSplitContainer.Size = New System.Drawing.Size(758, 293)
		Me.ItemTagsSplitContainer.SplitterDistance = 580
		Me.ItemTagsSplitContainer.SplitterWidth = 3
		Me.ItemTagsSplitContainer.TabIndex = 25
		'
		'DescriptionChangeNoteSplitContainer
		'
		Me.DescriptionChangeNoteSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DescriptionChangeNoteSplitContainer.Location = New System.Drawing.Point(0, 68)
		Me.DescriptionChangeNoteSplitContainer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.DescriptionChangeNoteSplitContainer.Name = "DescriptionChangeNoteSplitContainer"
		'
		'DescriptionChangeNoteSplitContainer.Panel1
		'
		Me.DescriptionChangeNoteSplitContainer.Panel1.Controls.Add(Me.ItemDescriptionTextBox)
		Me.DescriptionChangeNoteSplitContainer.Panel1.Controls.Add(Me.ItemDescriptionTopPanel)
		Me.DescriptionChangeNoteSplitContainer.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
		'
		'DescriptionChangeNoteSplitContainer.Panel2
		'
		Me.DescriptionChangeNoteSplitContainer.Panel2.Controls.Add(Me.ItemChangeNoteTextBox)
		Me.DescriptionChangeNoteSplitContainer.Panel2.Controls.Add(Me.ItemChangeNoteTopPanel)
		Me.DescriptionChangeNoteSplitContainer.Size = New System.Drawing.Size(580, 111)
		Me.DescriptionChangeNoteSplitContainer.SplitterDistance = 295
		Me.DescriptionChangeNoteSplitContainer.TabIndex = 5
		'
		'ItemDescriptionTextBox
		'
		Me.ItemDescriptionTextBox.AcceptsTab = True
		Me.ItemDescriptionTextBox.CueBannerText = "required"
		Me.ItemDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ItemDescriptionTextBox.Location = New System.Drawing.Point(3, 19)
		Me.ItemDescriptionTextBox.Name = "ItemDescriptionTextBox"
		Me.ItemDescriptionTextBox.Size = New System.Drawing.Size(292, 92)
		Me.ItemDescriptionTextBox.TabIndex = 5
		Me.ItemDescriptionTextBox.Text = ""
		Me.ItemDescriptionTextBox.WordWrap = False
		'
		'ItemDescriptionTopPanel
		'
		Me.ItemDescriptionTopPanel.Controls.Add(Me.ToggleWordWrapForDescriptionCheckBox)
		Me.ItemDescriptionTopPanel.Controls.Add(Me.ItemDescriptionLabel)
		Me.ItemDescriptionTopPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.ItemDescriptionTopPanel.Location = New System.Drawing.Point(3, 0)
		Me.ItemDescriptionTopPanel.Name = "ItemDescriptionTopPanel"
		Me.ItemDescriptionTopPanel.Size = New System.Drawing.Size(292, 19)
		Me.ItemDescriptionTopPanel.TabIndex = 17
		'
		'ToggleWordWrapForDescriptionCheckBox
		'
		Me.ToggleWordWrapForDescriptionCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ToggleWordWrapForDescriptionCheckBox.Appearance = System.Windows.Forms.Appearance.Button
		Me.ToggleWordWrapForDescriptionCheckBox.BackgroundImage = Global.Crowbar.My.Resources.Resources.WordWrapOff
		Me.ToggleWordWrapForDescriptionCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.ToggleWordWrapForDescriptionCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.ToggleWordWrapForDescriptionCheckBox.Location = New System.Drawing.Point(278, 4)
		Me.ToggleWordWrapForDescriptionCheckBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ToggleWordWrapForDescriptionCheckBox.Name = "ToggleWordWrapForDescriptionCheckBox"
		Me.ToggleWordWrapForDescriptionCheckBox.Size = New System.Drawing.Size(13, 13)
		Me.ToggleWordWrapForDescriptionCheckBox.TabIndex = 16
		Me.ToggleWordWrapForDescriptionCheckBox.UseVisualStyleBackColor = True
		'
		'ItemDescriptionLabel
		'
		Me.ItemDescriptionLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemDescriptionLabel.Location = New System.Drawing.Point(0, 4)
		Me.ItemDescriptionLabel.Name = "ItemDescriptionLabel"
		Me.ItemDescriptionLabel.Size = New System.Drawing.Size(275, 13)
		Me.ItemDescriptionLabel.TabIndex = 9
		Me.ItemDescriptionLabel.Text = "Description (### / ### characters max):"
		'
		'ItemChangeNoteTextBox
		'
		Me.ItemChangeNoteTextBox.AcceptsTab = True
		Me.ItemChangeNoteTextBox.CueBannerText = ""
		Me.ItemChangeNoteTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ItemChangeNoteTextBox.Location = New System.Drawing.Point(0, 19)
		Me.ItemChangeNoteTextBox.Name = "ItemChangeNoteTextBox"
		Me.ItemChangeNoteTextBox.Size = New System.Drawing.Size(281, 92)
		Me.ItemChangeNoteTextBox.TabIndex = 6
		Me.ItemChangeNoteTextBox.Text = ""
		Me.ItemChangeNoteTextBox.WordWrap = False
		'
		'ItemChangeNoteTopPanel
		'
		Me.ItemChangeNoteTopPanel.Controls.Add(Me.ToggleWordWrapForChangeNotePanel)
		Me.ItemChangeNoteTopPanel.Controls.Add(Me.ItemChangeNoteLabel)
		Me.ItemChangeNoteTopPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.ItemChangeNoteTopPanel.Location = New System.Drawing.Point(0, 0)
		Me.ItemChangeNoteTopPanel.Name = "ItemChangeNoteTopPanel"
		Me.ItemChangeNoteTopPanel.Size = New System.Drawing.Size(281, 19)
		Me.ItemChangeNoteTopPanel.TabIndex = 18
		'
		'ToggleWordWrapForChangeNotePanel
		'
		Me.ToggleWordWrapForChangeNotePanel.Controls.Add(Me.ToggleWordWrapForChangeNoteCheckBox)
		Me.ToggleWordWrapForChangeNotePanel.Dock = System.Windows.Forms.DockStyle.Right
		Me.ToggleWordWrapForChangeNotePanel.Location = New System.Drawing.Point(267, 0)
		Me.ToggleWordWrapForChangeNotePanel.Name = "ToggleWordWrapForChangeNotePanel"
		Me.ToggleWordWrapForChangeNotePanel.Size = New System.Drawing.Size(14, 19)
		Me.ToggleWordWrapForChangeNotePanel.TabIndex = 18
		'
		'ToggleWordWrapForChangeNoteCheckBox
		'
		Me.ToggleWordWrapForChangeNoteCheckBox.Appearance = System.Windows.Forms.Appearance.Button
		Me.ToggleWordWrapForChangeNoteCheckBox.BackgroundImage = Global.Crowbar.My.Resources.Resources.WordWrapOff
		Me.ToggleWordWrapForChangeNoteCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.ToggleWordWrapForChangeNoteCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.ToggleWordWrapForChangeNoteCheckBox.Location = New System.Drawing.Point(0, 4)
		Me.ToggleWordWrapForChangeNoteCheckBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ToggleWordWrapForChangeNoteCheckBox.Name = "ToggleWordWrapForChangeNoteCheckBox"
		Me.ToggleWordWrapForChangeNoteCheckBox.Size = New System.Drawing.Size(13, 13)
		Me.ToggleWordWrapForChangeNoteCheckBox.TabIndex = 17
		Me.ToggleWordWrapForChangeNoteCheckBox.UseVisualStyleBackColor = True
		'
		'ItemChangeNoteLabel
		'
		Me.ItemChangeNoteLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemChangeNoteLabel.Location = New System.Drawing.Point(0, 4)
		Me.ItemChangeNoteLabel.Name = "ItemChangeNoteLabel"
		Me.ItemChangeNoteLabel.Size = New System.Drawing.Size(264, 13)
		Me.ItemChangeNoteLabel.TabIndex = 11
		Me.ItemChangeNoteLabel.Text = "Content Changes (### / ### characters max):"
		'
		'ItemTopPanel
		'
		Me.ItemTopPanel.Controls.Add(Me.ItemIDLabel)
		Me.ItemTopPanel.Controls.Add(Me.ItemIDTextBox)
		Me.ItemTopPanel.Controls.Add(Me.ItemOwnerLabel)
		Me.ItemTopPanel.Controls.Add(Me.ItemOwnerTextBox)
		Me.ItemTopPanel.Controls.Add(Me.ItemPostedTextBox)
		Me.ItemTopPanel.Controls.Add(Me.ItemUpdatedTextBox)
		Me.ItemTopPanel.Controls.Add(Me.ItemTitleLabel)
		Me.ItemTopPanel.Controls.Add(Me.ItemTitleTextBox)
		Me.ItemTopPanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.ItemTopPanel.Location = New System.Drawing.Point(0, 0)
		Me.ItemTopPanel.Name = "ItemTopPanel"
		Me.ItemTopPanel.Size = New System.Drawing.Size(580, 68)
		Me.ItemTopPanel.TabIndex = 36
		'
		'ItemIDLabel
		'
		Me.ItemIDLabel.Location = New System.Drawing.Point(3, 4)
		Me.ItemIDLabel.Name = "ItemIDLabel"
		Me.ItemIDLabel.Size = New System.Drawing.Size(25, 13)
		Me.ItemIDLabel.TabIndex = 4
		Me.ItemIDLabel.Text = "ID:"
		'
		'ItemIDTextBox
		'
		Me.ItemIDTextBox.CueBannerText = ""
		Me.ItemIDTextBox.Location = New System.Drawing.Point(34, 0)
		Me.ItemIDTextBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemIDTextBox.Name = "ItemIDTextBox"
		Me.ItemIDTextBox.ReadOnly = True
		Me.ItemIDTextBox.Size = New System.Drawing.Size(110, 22)
		Me.ItemIDTextBox.TabIndex = 0
		'
		'ItemOwnerTextBox
		'
		Me.ItemOwnerTextBox.CueBannerText = ""
		Me.ItemOwnerTextBox.Location = New System.Drawing.Point(201, 0)
		Me.ItemOwnerTextBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemOwnerTextBox.Name = "ItemOwnerTextBox"
		Me.ItemOwnerTextBox.ReadOnly = True
		Me.ItemOwnerTextBox.Size = New System.Drawing.Size(120, 22)
		Me.ItemOwnerTextBox.TabIndex = 1
		'
		'ItemTitleLabel
		'
		Me.ItemTitleLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemTitleLabel.Location = New System.Drawing.Point(3, 28)
		Me.ItemTitleLabel.Name = "ItemTitleLabel"
		Me.ItemTitleLabel.Size = New System.Drawing.Size(577, 12)
		Me.ItemTitleLabel.TabIndex = 8
		Me.ItemTitleLabel.Text = "Title (### / ### characters max):"
		'
		'ItemTitleTextBox
		'
		Me.ItemTitleTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemTitleTextBox.CueBannerText = "required"
		Me.ItemTitleTextBox.Location = New System.Drawing.Point(3, 44)
		Me.ItemTitleTextBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemTitleTextBox.Name = "ItemTitleTextBox"
		Me.ItemTitleTextBox.Size = New System.Drawing.Size(577, 22)
		Me.ItemTitleTextBox.TabIndex = 4
		Me.ItemTitleTextBox.WordWrap = False
		'
		'ItemBottomPanel
		'
		Me.ItemBottomPanel.Controls.Add(Me.ItemContentFolderOrFileLabel)
		Me.ItemBottomPanel.Controls.Add(Me.ItemContentPathFileNameTextBox)
		Me.ItemBottomPanel.Controls.Add(Me.BrowseItemContentPathFileNameButton)
		Me.ItemBottomPanel.Controls.Add(Me.ItemPreviewImageLabel)
		Me.ItemBottomPanel.Controls.Add(Me.ItemPreviewImagePathFileNameTextBox)
		Me.ItemBottomPanel.Controls.Add(Me.BrowseItemPreviewImagePathFileNameButton)
		Me.ItemBottomPanel.Controls.Add(Me.ItemPreviewImagePictureBox)
		Me.ItemBottomPanel.Controls.Add(Me.ItemVisibilityComboBox)
		Me.ItemBottomPanel.Controls.Add(Me.ItemVisibilityLabel)
		Me.ItemBottomPanel.Controls.Add(Me.SaveAsTemplateOrDraftItemButton)
		Me.ItemBottomPanel.Controls.Add(Me.RefreshOrRevertItemButton)
		Me.ItemBottomPanel.Controls.Add(Me.SaveTemplateButton)
		Me.ItemBottomPanel.Controls.Add(Me.OpenWorkshopPageButton)
		Me.ItemBottomPanel.Controls.Add(Me.DeleteItemButton)
		Me.ItemBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.ItemBottomPanel.Location = New System.Drawing.Point(0, 179)
		Me.ItemBottomPanel.Name = "ItemBottomPanel"
		Me.ItemBottomPanel.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.ItemBottomPanel.Size = New System.Drawing.Size(580, 114)
		Me.ItemBottomPanel.TabIndex = 37
		'
		'ItemContentFolderOrFileLabel
		'
		Me.ItemContentFolderOrFileLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemContentFolderOrFileLabel.Location = New System.Drawing.Point(3, 3)
		Me.ItemContentFolderOrFileLabel.Name = "ItemContentFolderOrFileLabel"
		Me.ItemContentFolderOrFileLabel.Size = New System.Drawing.Size(415, 13)
		Me.ItemContentFolderOrFileLabel.TabIndex = 18
		Me.ItemContentFolderOrFileLabel.Text = "Content Folder or File (### / ### MB max):"
		'
		'ItemContentPathFileNameTextBox
		'
		Me.ItemContentPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemContentPathFileNameTextBox.CueBannerText = "required"
		Me.ItemContentPathFileNameTextBox.Location = New System.Drawing.Point(3, 19)
		Me.ItemContentPathFileNameTextBox.Name = "ItemContentPathFileNameTextBox"
		Me.ItemContentPathFileNameTextBox.Size = New System.Drawing.Size(415, 22)
		Me.ItemContentPathFileNameTextBox.TabIndex = 7
		Me.ItemContentPathFileNameTextBox.WordWrap = False
		'
		'BrowseItemContentPathFileNameButton
		'
		Me.BrowseItemContentPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseItemContentPathFileNameButton.Location = New System.Drawing.Point(424, 19)
		Me.BrowseItemContentPathFileNameButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.BrowseItemContentPathFileNameButton.Name = "BrowseItemContentPathFileNameButton"
		Me.BrowseItemContentPathFileNameButton.Size = New System.Drawing.Size(75, 22)
		Me.BrowseItemContentPathFileNameButton.TabIndex = 8
		Me.BrowseItemContentPathFileNameButton.Text = "Browse..."
		Me.BrowseItemContentPathFileNameButton.UseVisualStyleBackColor = True
		'
		'ItemPreviewImageLabel
		'
		Me.ItemPreviewImageLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemPreviewImageLabel.Location = New System.Drawing.Point(3, 45)
		Me.ItemPreviewImageLabel.Name = "ItemPreviewImageLabel"
		Me.ItemPreviewImageLabel.Size = New System.Drawing.Size(415, 13)
		Me.ItemPreviewImageLabel.TabIndex = 13
		Me.ItemPreviewImageLabel.Text = "Preview Image (### / ### MB max |  Required resolution: 512x512):"
		'
		'ItemPreviewImagePathFileNameTextBox
		'
		Me.ItemPreviewImagePathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemPreviewImagePathFileNameTextBox.CueBannerText = "required"
		Me.ItemPreviewImagePathFileNameTextBox.Location = New System.Drawing.Point(3, 60)
		Me.ItemPreviewImagePathFileNameTextBox.Name = "ItemPreviewImagePathFileNameTextBox"
		Me.ItemPreviewImagePathFileNameTextBox.Size = New System.Drawing.Size(415, 22)
		Me.ItemPreviewImagePathFileNameTextBox.TabIndex = 9
		Me.ItemPreviewImagePathFileNameTextBox.WordWrap = False
		'
		'BrowseItemPreviewImagePathFileNameButton
		'
		Me.BrowseItemPreviewImagePathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseItemPreviewImagePathFileNameButton.Location = New System.Drawing.Point(424, 60)
		Me.BrowseItemPreviewImagePathFileNameButton.Name = "BrowseItemPreviewImagePathFileNameButton"
		Me.BrowseItemPreviewImagePathFileNameButton.Size = New System.Drawing.Size(75, 22)
		Me.BrowseItemPreviewImagePathFileNameButton.TabIndex = 10
		Me.BrowseItemPreviewImagePathFileNameButton.Text = "Browse..."
		Me.BrowseItemPreviewImagePathFileNameButton.UseVisualStyleBackColor = True
		'
		'ItemPreviewImagePictureBox
		'
		Me.ItemPreviewImagePictureBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ItemPreviewImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.ItemPreviewImagePictureBox.Location = New System.Drawing.Point(505, 7)
		Me.ItemPreviewImagePictureBox.Name = "ItemPreviewImagePictureBox"
		Me.ItemPreviewImagePictureBox.Size = New System.Drawing.Size(75, 75)
		Me.ItemPreviewImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.ItemPreviewImagePictureBox.TabIndex = 27
		Me.ItemPreviewImagePictureBox.TabStop = False
		'
		'ItemVisibilityComboBox
		'
		Me.ItemVisibilityComboBox.FormattingEnabled = True
		Me.ItemVisibilityComboBox.Location = New System.Drawing.Point(63, 88)
		Me.ItemVisibilityComboBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemVisibilityComboBox.Name = "ItemVisibilityComboBox"
		Me.ItemVisibilityComboBox.Size = New System.Drawing.Size(100, 21)
		Me.ItemVisibilityComboBox.TabIndex = 11
		'
		'ItemVisibilityLabel
		'
		Me.ItemVisibilityLabel.Location = New System.Drawing.Point(3, 92)
		Me.ItemVisibilityLabel.Name = "ItemVisibilityLabel"
		Me.ItemVisibilityLabel.Size = New System.Drawing.Size(54, 13)
		Me.ItemVisibilityLabel.TabIndex = 10
		Me.ItemVisibilityLabel.Text = "Visibility:"
		'
		'SaveAsTemplateOrDraftItemButton
		'
		Me.SaveAsTemplateOrDraftItemButton.Location = New System.Drawing.Point(169, 88)
		Me.SaveAsTemplateOrDraftItemButton.Name = "SaveAsTemplateOrDraftItemButton"
		Me.SaveAsTemplateOrDraftItemButton.Size = New System.Drawing.Size(111, 22)
		Me.SaveAsTemplateOrDraftItemButton.TabIndex = 12
		Me.SaveAsTemplateOrDraftItemButton.Text = "Save as Template"
		Me.SaveAsTemplateOrDraftItemButton.UseVisualStyleBackColor = True
		'
		'RefreshOrRevertItemButton
		'
		Me.RefreshOrRevertItemButton.Location = New System.Drawing.Point(286, 88)
		Me.RefreshOrRevertItemButton.Name = "RefreshOrRevertItemButton"
		Me.RefreshOrRevertItemButton.Size = New System.Drawing.Size(75, 22)
		Me.RefreshOrRevertItemButton.TabIndex = 13
		Me.RefreshOrRevertItemButton.Text = "Refresh"
		Me.RefreshOrRevertItemButton.UseVisualStyleBackColor = True
		'
		'SaveTemplateButton
		'
		Me.SaveTemplateButton.Location = New System.Drawing.Point(367, 88)
		Me.SaveTemplateButton.Name = "SaveTemplateButton"
		Me.SaveTemplateButton.Size = New System.Drawing.Size(75, 22)
		Me.SaveTemplateButton.TabIndex = 14
		Me.SaveTemplateButton.Text = "Save"
		Me.SaveTemplateButton.UseVisualStyleBackColor = True
		'
		'OpenWorkshopPageButton
		'
		Me.OpenWorkshopPageButton.Enabled = False
		Me.OpenWorkshopPageButton.Location = New System.Drawing.Point(367, 88)
		Me.OpenWorkshopPageButton.Name = "OpenWorkshopPageButton"
		Me.OpenWorkshopPageButton.Size = New System.Drawing.Size(132, 22)
		Me.OpenWorkshopPageButton.TabIndex = 15
		Me.OpenWorkshopPageButton.Text = "Open Workshop Page"
		Me.OpenWorkshopPageButton.UseVisualStyleBackColor = True
		'
		'DeleteItemButton
		'
		Me.DeleteItemButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DeleteItemButton.Location = New System.Drawing.Point(505, 88)
		Me.DeleteItemButton.Name = "DeleteItemButton"
		Me.DeleteItemButton.Size = New System.Drawing.Size(75, 22)
		Me.DeleteItemButton.TabIndex = 16
		Me.DeleteItemButton.Text = "Delete..."
		Me.DeleteItemButton.UseVisualStyleBackColor = True
		'
		'ItemLeftMinScrollPanel
		'
		Me.ItemLeftMinScrollPanel.Location = New System.Drawing.Point(0, 0)
		Me.ItemLeftMinScrollPanel.Name = "ItemLeftMinScrollPanel"
		Me.ItemLeftMinScrollPanel.Size = New System.Drawing.Size(580, 1)
		Me.ItemLeftMinScrollPanel.TabIndex = 38
		'
		'ItemTagsGroupBox
		'
		Me.ItemTagsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ItemTagsGroupBox.IsReadOnly = False
		Me.ItemTagsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.ItemTagsGroupBox.Margin = New System.Windows.Forms.Padding(0)
		Me.ItemTagsGroupBox.Name = "ItemTagsGroupBox"
		Me.ItemTagsGroupBox.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.ItemTagsGroupBox.SelectedValue = Nothing
		Me.ItemTagsGroupBox.Size = New System.Drawing.Size(172, 289)
		Me.ItemTagsGroupBox.TabIndex = 17
		Me.ItemTagsGroupBox.TabStop = False
		Me.ItemTagsGroupBox.Text = "Tags"
		'
		'LogTextBox
		'
		Me.LogTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.LogTextBox.CueBannerText = ""
		Me.LogTextBox.HideSelection = False
		Me.LogTextBox.Location = New System.Drawing.Point(134, 0)
		Me.LogTextBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.LogTextBox.Name = "LogTextBox"
		Me.LogTextBox.ReadOnly = True
		Me.LogTextBox.Size = New System.Drawing.Size(633, 59)
		Me.LogTextBox.TabIndex = 19
		Me.LogTextBox.Text = ""
		Me.LogTextBox.WordWrap = False
		'
		'PublishItemButton
		'
		Me.PublishItemButton.Location = New System.Drawing.Point(3, 0)
		Me.PublishItemButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.PublishItemButton.Name = "PublishItemButton"
		Me.PublishItemButton.Size = New System.Drawing.Size(125, 22)
		Me.PublishItemButton.TabIndex = 18
		Me.PublishItemButton.Text = "Publish"
		Me.PublishItemButton.UseVisualStyleBackColor = True
		'
		'QueueListView
		'
		Me.QueueListView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.QueueListView.HideSelection = False
		Me.QueueListView.Location = New System.Drawing.Point(568, 0)
		Me.QueueListView.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.QueueListView.Name = "QueueListView"
		Me.QueueListView.Size = New System.Drawing.Size(199, 63)
		Me.QueueListView.TabIndex = 20
		Me.QueueListView.UseCompatibleStateImageBehavior = False
		Me.QueueListView.Visible = False
		'
		'PublishUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.TopMiddleSplitContainer)
		Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.Name = "PublishUserControl"
		Me.Size = New System.Drawing.Size(770, 534)
		Me.TopMiddleSplitContainer.Panel1.ResumeLayout(False)
		Me.TopMiddleSplitContainer.Panel2.ResumeLayout(False)
		CType(Me.TopMiddleSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TopMiddleSplitContainer.ResumeLayout(False)
		Me.ItemsPanel.ResumeLayout(False)
		Me.ItemsPanel.PerformLayout()
		CType(Me.ItemsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ToolStrip1.ResumeLayout(False)
		Me.ToolStrip1.PerformLayout()
		Me.GamePanel.ResumeLayout(False)
		Me.MiddleBottomSplitContainer.Panel1.ResumeLayout(False)
		Me.MiddleBottomSplitContainer.Panel2.ResumeLayout(False)
		CType(Me.MiddleBottomSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.MiddleBottomSplitContainer.ResumeLayout(False)
		Me.ItemGroupBox.ResumeLayout(False)
		Me.ItemTagsSplitContainer.Panel1.ResumeLayout(False)
		Me.ItemTagsSplitContainer.Panel2.ResumeLayout(False)
		CType(Me.ItemTagsSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ItemTagsSplitContainer.ResumeLayout(False)
		Me.DescriptionChangeNoteSplitContainer.Panel1.ResumeLayout(False)
		Me.DescriptionChangeNoteSplitContainer.Panel2.ResumeLayout(False)
		CType(Me.DescriptionChangeNoteSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.DescriptionChangeNoteSplitContainer.ResumeLayout(False)
		Me.ItemDescriptionTopPanel.ResumeLayout(False)
		Me.ItemChangeNoteTopPanel.ResumeLayout(False)
		Me.ToggleWordWrapForChangeNotePanel.ResumeLayout(False)
		Me.ItemTopPanel.ResumeLayout(False)
		Me.ItemTopPanel.PerformLayout()
		Me.ItemBottomPanel.ResumeLayout(False)
		Me.ItemBottomPanel.PerformLayout()
		CType(Me.ItemPreviewImagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents TopMiddleSplitContainer As SplitContainerEx
	Friend WithEvents ItemsDataGridView As DataGridView
	Friend WithEvents PublishRequiresSteamLabel As Label
	Friend WithEvents OpenSteamSubscriberAgreementButton As Button
	Friend WithEvents AppIdComboBox As ComboBox
	Friend WithEvents GameLabel As Label
	Friend WithEvents MiddleBottomSplitContainer As SplitContainer
	Friend WithEvents ItemGroupBox As GroupBoxEx
	Friend WithEvents ItemTagsSplitContainer As SplitContainer
	Friend WithEvents OpenWorkshopPageButton As Button
	Friend WithEvents ItemTitleLabel As Label
	Friend WithEvents DeleteItemButton As Button
	Friend WithEvents ItemVisibilityComboBox As ComboBox
	Friend WithEvents BrowseItemPreviewImagePathFileNameButton As Button
	Friend WithEvents ItemTitleTextBox As TextBoxEx
	Friend WithEvents ItemPreviewImageLabel As Label
	Friend WithEvents ItemIDTextBox As TextBoxEx
	Friend WithEvents ItemPreviewImagePathFileNameTextBox As TextBoxEx
	Friend WithEvents ItemVisibilityLabel As Label
	Friend WithEvents ItemPostedTextBox As DateTimeTextBoxEx
	Friend WithEvents BrowseItemContentPathFileNameButton As Button
	Friend WithEvents ItemUpdatedTextBox As DateTimeTextBoxEx
	Friend WithEvents ItemContentPathFileNameTextBox As TextBoxEx
	Friend WithEvents ItemContentFolderOrFileLabel As Label
	Friend WithEvents DescriptionChangeNoteSplitContainer As SplitContainer
	Friend WithEvents ItemDescriptionLabel As Label
	Friend WithEvents ItemDescriptionTextBox As RichTextBoxEx
	Friend WithEvents ItemChangeNoteLabel As Label
	Friend WithEvents ItemChangeNoteTextBox As RichTextBoxEx
	Friend WithEvents ItemPreviewImagePictureBox As PictureBox
	Friend WithEvents ItemIDLabel As Label
	Friend WithEvents ItemTagsGroupBox As GroupBoxEx
	Friend WithEvents QueueListView As ListView
	Friend WithEvents LogTextBox As RichTextBoxEx
	Friend WithEvents PublishItemButton As Button
	Friend WithEvents ToolStrip1 As ToolStrip
	Friend WithEvents ItemCountsToolStripLabel As ToolStripLabel
	Friend WithEvents FindItemToolStripTextBox As ToolStripSpringTextBox
	Friend WithEvents SearchItemsToolStripButton As ToolStripButton
	Friend WithEvents ItemsPanel As Panel
	Friend WithEvents RefreshOrRevertItemButton As Button
	Friend WithEvents SearchItemsToolStripTextBox As ToolStripTextBox
	Friend WithEvents AddItemToolStripButton As ToolStripButton
	Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
	Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
	Friend WithEvents SaveAsTemplateOrDraftItemButton As Button
	Friend WithEvents ItemOwnerTextBox As TextBoxEx
	Friend WithEvents ItemOwnerLabel As Label
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents SearchItemsToolStripComboBox As ToolStripComboBox
	Friend WithEvents SaveTemplateButton As Button
	Friend WithEvents QuotaProgressBar As ProgressBarEx
	Friend WithEvents ToggleWordWrapForDescriptionCheckBox As CheckBox
	Friend WithEvents ToggleWordWrapForChangeNoteCheckBox As CheckBox
	Friend WithEvents RefreshGameItemsButton As Button
	Friend WithEvents GamePanel As Panel
	Friend WithEvents ItemTopPanel As Panel
	Friend WithEvents ItemBottomPanel As Panel
	Friend WithEvents ItemLeftMinScrollPanel As Panel
	Friend WithEvents ItemDescriptionTopPanel As Panel
	Friend WithEvents ItemChangeNoteTopPanel As Panel
	Friend WithEvents ToggleWordWrapForChangeNotePanel As Panel
End Class
