<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PackageContentsUserControl
	Inherits BaseUserControl

	''UserControl overrides dispose to clean up the component list.
	'<System.Diagnostics.DebuggerNonUserCode()> _
	'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
	'	Try
	'		If disposing AndAlso components IsNot Nothing Then
	'			components.Dispose()
	'		End If
	'	Finally
	'		MyBase.Dispose(disposing)
	'	End Try
	'End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.ContentsGroupBox = New System.Windows.Forms.GroupBox()
		Me.ContentsGroupBoxFillPanel = New System.Windows.Forms.Panel()
		Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
		Me.PackageTreeView = New Crowbar.TreeViewEx()
		Me.PackageListView = New System.Windows.Forms.ListView()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.SelectionPathTextBox = New System.Windows.Forms.TextBox()
		Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.SearchToolStripTextBox = New Crowbar.ToolStripSpringTextBox()
		Me.SearchToolStripButton = New System.Windows.Forms.ToolStripButton()
		Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.FilesSelectedCountToolStripLabel = New System.Windows.Forms.ToolStripLabel()
		Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
		Me.SizeSelectedTotalToolStripLabel = New System.Windows.Forms.ToolStripLabel()
		Me.ContentsMinScrollerPanel = New System.Windows.Forms.Panel()
		Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
		Me.ContentsGroupBox.SuspendLayout()
		Me.ContentsGroupBoxFillPanel.SuspendLayout()
		CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer3.Panel1.SuspendLayout()
		Me.SplitContainer3.Panel2.SuspendLayout()
		Me.SplitContainer3.SuspendLayout()
		Me.Panel3.SuspendLayout()
		Me.ToolStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'ContentsGroupBox
		'
		Me.ContentsGroupBox.Controls.Add(Me.ContentsGroupBoxFillPanel)
		Me.ContentsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ContentsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.ContentsGroupBox.Name = "ContentsGroupBox"
		Me.ContentsGroupBox.Size = New System.Drawing.Size(572, 347)
		Me.ContentsGroupBox.TabIndex = 1
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
		Me.ContentsGroupBoxFillPanel.Size = New System.Drawing.Size(566, 326)
		Me.ContentsGroupBoxFillPanel.TabIndex = 12
		'
		'SplitContainer3
		'
		Me.SplitContainer3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
		Me.SplitContainer3.Size = New System.Drawing.Size(566, 275)
		Me.SplitContainer3.SplitterDistance = 250
		Me.SplitContainer3.TabIndex = 6
		'
		'PackageTreeView
		'
		Me.PackageTreeView.BackColor = System.Drawing.SystemColors.Control
		Me.PackageTreeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PackageTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
		Me.PackageTreeView.HideSelection = False
		Me.PackageTreeView.Location = New System.Drawing.Point(0, 0)
		Me.PackageTreeView.Name = "PackageTreeView"
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
		Me.PackageListView.Size = New System.Drawing.Size(312, 275)
		Me.PackageListView.Sorting = System.Windows.Forms.SortOrder.Ascending
		Me.PackageListView.TabIndex = 1
		Me.PackageListView.UseCompatibleStateImageBehavior = False
		Me.PackageListView.View = System.Windows.Forms.View.Details
		'
		'Panel3
		'
		Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Panel3.Controls.Add(Me.SelectionPathTextBox)
		Me.Panel3.Location = New System.Drawing.Point(0, 0)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(566, 26)
		Me.Panel3.TabIndex = 11
		'
		'SelectionPathTextBox
		'
		Me.SelectionPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SelectionPathTextBox.Location = New System.Drawing.Point(0, 0)
		Me.SelectionPathTextBox.Name = "SelectionPathTextBox"
		Me.SelectionPathTextBox.ReadOnly = True
		Me.SelectionPathTextBox.Size = New System.Drawing.Size(566, 22)
		Me.SelectionPathTextBox.TabIndex = 1
		'
		'ToolStrip1
		'
		Me.ToolStrip1.CanOverflow = False
		Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SearchToolStripTextBox, Me.SearchToolStripButton, Me.ToolStripSeparator1, Me.FilesSelectedCountToolStripLabel, Me.ToolStripSeparator3, Me.SizeSelectedTotalToolStripLabel})
		Me.ToolStrip1.Location = New System.Drawing.Point(0, 301)
		Me.ToolStrip1.Name = "ToolStrip1"
		Me.ToolStrip1.Size = New System.Drawing.Size(566, 25)
		Me.ToolStrip1.Stretch = True
		Me.ToolStrip1.TabIndex = 10
		Me.ToolStrip1.Text = "ToolStrip1"
		'
		'SearchToolStripTextBox
		'
		Me.SearchToolStripTextBox.Name = "SearchToolStripTextBox"
		Me.SearchToolStripTextBox.Size = New System.Drawing.Size(460, 25)
		Me.SearchToolStripTextBox.ToolTipText = "Text to search for"
		'
		'SearchToolStripButton
		'
		Me.SearchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.SearchToolStripButton.Image = Global.Crowbar.My.Resources.Resources.Search
		Me.SearchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.SearchToolStripButton.Name = "SearchToolStripButton"
		Me.SearchToolStripButton.RightToLeftAutoMirrorImage = True
		Me.SearchToolStripButton.Size = New System.Drawing.Size(23, 22)
		Me.SearchToolStripButton.Text = "Search"
		Me.SearchToolStripButton.ToolTipText = "Find"
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
		'ContentsMinScrollerPanel
		'
		Me.ContentsMinScrollerPanel.Location = New System.Drawing.Point(0, 0)
		Me.ContentsMinScrollerPanel.Name = "ContentsMinScrollerPanel"
		Me.ContentsMinScrollerPanel.Size = New System.Drawing.Size(250, 110)
		Me.ContentsMinScrollerPanel.TabIndex = 12
		'
		'ImageList1
		'
		Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
		Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
		Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
		'
		'PackageContentsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.ContentsGroupBox)
		Me.Name = "PackageContentsUserControl"
		Me.Size = New System.Drawing.Size(572, 347)
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
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents ContentsGroupBox As GroupBox
	Friend WithEvents ContentsGroupBoxFillPanel As Panel
	Friend WithEvents SplitContainer3 As SplitContainer
	Friend WithEvents PackageTreeView As TreeViewEx
	Friend WithEvents PackageListView As ListView
	Friend WithEvents Panel3 As Panel
	Friend WithEvents SelectionPathTextBox As TextBox
	Friend WithEvents ToolStrip1 As ToolStrip
	Friend WithEvents SearchToolStripTextBox As ToolStripSpringTextBox
	Friend WithEvents SearchToolStripButton As ToolStripButton
	Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
	Friend WithEvents FilesSelectedCountToolStripLabel As ToolStripLabel
	Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
	Friend WithEvents SizeSelectedTotalToolStripLabel As ToolStripLabel
	Friend WithEvents ContentsMinScrollerPanel As Panel
	Friend WithEvents ImageList1 As ImageList
End Class
