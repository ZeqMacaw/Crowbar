Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.IO

Public Class PackageContentsUserControl

#Region "Creation and Destruction"

	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

		Me.PackageTreeViewCustomMenu = New ContextMenuStrip()
		Me.PackageTreeViewCustomMenu.Items.Add(Me.DeleteSearchToolStripMenuItem)
		Me.PackageTreeViewCustomMenu.Items.Add(Me.DeleteAllSearchesToolStripMenuItem)
		Me.PackageTreeView.ContextMenuStrip = Me.PackageTreeViewCustomMenu

		Me.PackageListViewCustomMenu = New ContextMenuStrip()
		Me.PackageListViewCustomMenu.Items.Add(Me.ToggleSizeUnitsToolStripMenuItem)
		Me.PackageListView.ContextMenuStrip = Me.PackageListViewCustomMenu

		Me.theSearchCount = 0

	End Sub

#End Region

#Region "Init and Free"

	Protected Overrides Sub Init()
		'NOTE: Adding folder icon here means it is first in the image list, which is the icon used by default 
		Dim anIcon As Bitmap
		anIcon = Win32Api.GetShellIcon("folder", Win32Api.FILE_ATTRIBUTE_DIRECTORY)
		Me.ImageList1.Images.Add("<Folder>", anIcon)
		'NOTE: Need this empty string for file without an extension because Images.ContainsKey() will not return True when the empty string is in Images.
		anIcon = Win32Api.GetShellIcon("f")
		Me.ImageList1.Images.Add("", anIcon)

		Me.PackageTreeView.ImageIndex = 0
		Me.PackageTreeView.ImageList = Me.ImageList1
		'NOTE: The TreeView.Sorted property does not show in Intellisense or Properties window.
		Me.PackageTreeView.Sorted = True
		Me.PackageTreeView.TreeViewNodeSorter = New NodeSorter()

		Me.PackageListView.SmallImageList = Me.ImageList1
		' Use VirtualMode for speedy listing, especially when item count is high (e.g. 10,000).
		Me.PackageListView.VirtualMode = True
		Me.PackageListView.VirtualListSize = 0
		Me.PackageListView.Columns.Add("Name", "Name", 100)
		Me.PackageListView.Columns.Add("Size", "Size (bytes)", 100)
		Me.PackageListView.Columns.Add("Count", "Count", 50)
		Me.PackageListView.Columns.Add("Type", "Type", 100)
		Me.PackageListView.Columns.Add("Extension", "Extension", 100)
		Me.PackageListView.Columns.Add("PackagePathFileName", "Package File", 100)
		Me.theSortColumnIndex = 0
	End Sub

#End Region

#Region "Properties"

	<Browsable(False)>
	Public Property Entries() As List(Of SourcePackageDirectoryEntry)
		Get
			Return Me.theEntries
		End Get
		Set(ByVal value As List(Of SourcePackageDirectoryEntry))
			Me.theEntries = value

			If Me.theEntries.Count > 0 Then
				Dim GetId As Func(Of SourcePackageDirectoryEntry, String) = (Function(entry) FileManager.GetPath(entry.DisplayPathFileName))
				Dim GetTag As TreeViewEx.GetTagDelegate(Of SourcePackageDirectoryEntry, List(Of SourcePackageDirectoryEntry)) = AddressOf Me.GetTag
				Dim GetDimmedStatus As Func(Of SourcePackageDirectoryEntry, Boolean) = AddressOf Me.GetDimmedStatus
				Me.PackageTreeView.InsertItems(Me.PackageTreeView.Nodes(0), Me.theEntries, GetId, AddressOf Me.GetDisplayName, AddressOf Me.GetParentItem, GetTag, GetDimmedStatus)

				If Me.PackageTreeView.Nodes(0).Nodes.Count = 0 AndAlso Me.PackageTreeView.Nodes(0).Tag Is Nothing Then
					Me.PackageTreeView.Nodes.Clear()
				Else
					Me.PackageTreeView.Nodes(0).Text = Me.theRootText
				End If
			Else
				Me.PackageTreeView.Nodes(0).Text = "<incomplete>"
			End If

			If Me.PackageTreeView.Nodes.Count > 0 Then
				Me.PackageTreeView.Nodes(0).Expand()
				Me.PackageTreeView.SelectedNode = Me.PackageTreeView.Nodes(0)
				Me.ShowFilesInSelectedFolder()
			End If
			Me.UpdateSelectionPathText()
		End Set
	End Property

	<Browsable(False)>
	Public ReadOnly Property SelectedEntries() As List(Of SourcePackageDirectoryEntry)
		Get
			If Me.PackageTreeView.Nodes.Count > 0 Then
				Return CType(Me.PackageTreeView.Nodes(0).Tag, List(Of SourcePackageDirectoryEntry))
			Else
				Return Nothing
			End If
		End Get
	End Property

#End Region

#Region "Methods"

	Public Sub Clear()
		Me.thePackageCount = 0

		Me.PackageTreeView.Nodes.Clear()
		Me.PackageTreeView.Nodes.Add(Me.theRootText, "<refreshing>")
		Me.PackageTreeView.Nodes(0).Nodes.Clear()
		Me.PackageTreeView.Nodes(0).Tag = Nothing

		' Clear the listview.
		Me.PackageListView.VirtualListSize = 0

		Me.UpdateSelectionCounts()
	End Sub

	Public Sub IncrementPackageCount()
		Me.thePackageCount += 1
		If Me.thePackageCount > 1 Then
			Me.ContentsGroupBox.Text = "Contents of " + Me.thePackageCount.ToString("N0", TheApp.InternalCultureInfo) + " packages"
		Else
			Me.ContentsGroupBox.Text = "Contents of package"
		End If
	End Sub

	Public Sub GetSelectedEntriesAndOutputPath(ByRef packagePathFileNameToEntriesMap As SortedList(Of String, List(Of SourcePackageDirectoryEntry)), ByRef selectedRelativeOutputPath As String)
		Dim selectedNode As TreeNode = Me.PackageTreeView.SelectedNode
		If selectedNode Is Nothing Then
			selectedNode = Me.PackageTreeView.Nodes(0)
		End If
		Dim selectedIndexes As ListView.SelectedIndexCollection = Me.PackageListView.SelectedIndices

		Dim selectedResourceInfos As List(Of SourcePackageDirectoryEntry)
		If selectedIndexes.Count = 0 Then
			selectedResourceInfos = CType(selectedNode.Tag, List(Of SourcePackageDirectoryEntry))
			If selectedResourceInfos Is Nothing Then
				' This is reached when trying to Unpack a search folder with 0 results.
				Exit Sub
			End If

			selectedRelativeOutputPath = selectedNode.FullPath.Replace(Me.theRootText + "\", "")
			selectedRelativeOutputPath = FileManager.GetPath(selectedRelativeOutputPath)
		Else
			selectedResourceInfos = New List(Of SourcePackageDirectoryEntry)
			Dim selectedResourceInfo As SourcePackageDirectoryEntry
			Dim selectedItem As ListViewItem
			For Each selectedIndex As Integer In selectedIndexes
				selectedItem = Me.PackageListView.Items(selectedIndex)
				selectedResourceInfo = CType(selectedItem.Tag, SourcePackageDirectoryEntry)
				selectedResourceInfos.Add(selectedResourceInfo)
			Next

			selectedRelativeOutputPath = FileManager.GetPath(selectedResourceInfos(0).PathFileName)
		End If

		packagePathFileNameToEntriesMap = Me.GetEntriesFromFolderEntry(selectedResourceInfos, selectedNode, packagePathFileNameToEntriesMap)
	End Sub

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub PackageTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles PackageTreeView.AfterSelect
		Me.UpdateSelectionPathText()
		Me.ShowFilesInSelectedFolder()
	End Sub

	Private Sub PackageTreeView_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles PackageTreeView.ItemDrag
		'If Me.PackageTreeView.SelectedNode IsNot Nothing Then
		'	Me.RunUnpackerToUnpackFilesInternal(PackageAction.UnpackToTemp, Nothing)
		'End If
		Me.DoDragAndDrop()
	End Sub

	Private Sub PackageTreeView_MouseDown(sender As Object, e As MouseEventArgs) Handles PackageTreeView.MouseDown
		Dim treeView As TreeView
		Dim clickedNode As TreeNode

		treeView = CType(sender, Windows.Forms.TreeView)
		clickedNode = treeView.GetNodeAt(e.X, e.Y)
		If clickedNode Is Nothing Then
			'clickedNode = Me.PackageTreeView.Nodes(0)
			Exit Sub
		End If

		''NOTE: Right-clicking on a node does not select the node. Need to select the node so context menu will work.
		'If e.Button = MouseButtons.Right Then
		'	treeView.SelectedNode = clickedNode
		'End If
		'NOTE: This selects the node before dragging starts; otherwise dragging would use whatever was selected before the mousedown.
		treeView.SelectedNode = clickedNode

		'Me.UpdateSelectionPathText()
		'Me.ShowFilesInSelectedFolder()

		' Must use SelectedIndices instead of SelectedItems when using ListView.VirtualMode = True.
		'Me.PackageListView.SelectedItems.Clear()
		Me.PackageListView.SelectedIndices.Clear()
	End Sub

	'NOTE: Need this because listview item stays selected when selecting its parent folder.
	'      That is, PackageTreeView.AfterSelect event is not raised.
	Private Sub PackageTreeView_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles PackageTreeView.NodeMouseClick
		If Me.PackageTreeView.SelectedNode Is e.Node Then
			Me.UpdateSelectionPathText()
			Me.ShowFilesInSelectedFolder()
		End If
	End Sub

	'NOTE: This is only needed because TreeView BackColor does not automatically change when Windows Theme is switched.
	Private Sub PackageTreeView_SystemColorsChanged(sender As Object, e As EventArgs) Handles PackageTreeView.SystemColorsChanged
		Me.PackageTreeView.BackColor = SystemColors.Control
	End Sub

	Private Sub CustomMenu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PackageTreeViewCustomMenu.Opening
		Me.DeleteSearchToolStripMenuItem.Enabled = Me.PackageTreeView.SelectedNode IsNot Nothing AndAlso Me.PackageTreeView.SelectedNode.Text.StartsWith("<Found>")
		Me.DeleteAllSearchesToolStripMenuItem.Enabled = Me.theSearchCount > 0
	End Sub

	Private Sub DeleteSearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteSearchToolStripMenuItem.Click
		Me.DeleteSearch()
	End Sub

	Private Sub CopyAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteAllSearchesToolStripMenuItem.Click
		Me.DeleteAllSearches()
	End Sub

	Private Sub PackageListView_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles PackageListView.ColumnClick
		If e.Column <> Me.theSortColumnIndex Then
			Me.theSortColumnIndex = e.Column
			Me.PackageListView.Sorting = SortOrder.Ascending
		Else
			If Me.PackageListView.Sorting = SortOrder.Ascending Then
				Me.PackageListView.Sorting = SortOrder.Descending
			Else
				Me.PackageListView.Sorting = SortOrder.Ascending
			End If
		End If

		Dim selectedTreeNode As TreeNode = Me.PackageTreeView.SelectedNode
		If selectedTreeNode IsNot Nothing AndAlso selectedTreeNode.Tag IsNot Nothing Then
			Dim list As List(Of SourcePackageDirectoryEntry) = CType(selectedTreeNode.Tag, List(Of SourcePackageDirectoryEntry))
			list.Sort(New SourcePackageDirectoryEntry.Comparer(Me.PackageListView.Columns(Me.theSortColumnIndex).Name, Me.PackageListView.Sorting))
			Me.PackageListView.Invalidate()
		End If
	End Sub

	Private Sub PackageListView_DoubleClick(sender As Object, e As EventArgs) Handles PackageListView.DoubleClick
		Me.OpenSelectedFolderOrFile()
	End Sub

	Private Sub PackageListView_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles PackageListView.ItemDrag
		'If Me.PackageListView.SelectedIndices.Count > 0 Then
		'	Me.RunUnpackerToExtractFiles(PackageAction.UnpackToTemp, Me.PackageListView.SelectedIndices)
		'End If
		Me.DoDragAndDrop()
	End Sub

	'NOTE: Tried to show the highlight in TreeView when clicking empty space in ListView, but it did not work.
	'Private Sub PackageListView_MouseDown(sender As Object, e As MouseEventArgs) Handles PackageListView.MouseDown
	'	Dim listView As ListView
	'	Dim clickedItem As ListViewItem

	'	listView = CType(sender, Windows.Forms.ListView)
	'	clickedItem = listView.GetItemAt(e.X, e.Y)
	'	If clickedItem Is Nothing Then
	'		Me.PackageTreeView.Select()
	'	End If
	'End Sub

	Private Sub PackageListView_KeyDown(sender As Object, e As KeyEventArgs) Handles PackageListView.KeyDown
		If e.KeyCode = Keys.A And e.Control Then
			'Me.PackageListView.BeginUpdate()
			'For Each i As ListViewItem In Me.PackageListView.Items
			'	i.Selected = True
			'Next
			Win32Api.SelectAllItems(Me.PackageListView)
			'Me.PackageListView.EndUpdate()
		End If
	End Sub

	Private Sub PackageListView_RetrieveVirtualItem(ByVal sender As Object, ByVal e As RetrieveVirtualItemEventArgs) Handles PackageListView.RetrieveVirtualItem
		Dim selectedTreeNode As TreeNode = Me.PackageTreeView.SelectedNode
		If selectedTreeNode IsNot Nothing AndAlso selectedTreeNode.Tag IsNot Nothing Then
			Dim list As List(Of SourcePackageDirectoryEntry) = CType(selectedTreeNode.Tag, List(Of SourcePackageDirectoryEntry))
			Dim entry As SourcePackageDirectoryEntry = list(e.ItemIndex)

			Dim entrySize As String
			If TheApp.Settings.UnpackByteUnitsOption = ByteUnitsOption.Binary Then
				entrySize = MathModule.BinaryByteUnitsConversion(entry.Size)
			Else
				entrySize = entry.Size.ToString("N0", TheApp.InternalCultureInfo)
			End If

			e.Item = New ListViewItem(entry.Name)
			e.Item.Tag = entry
			e.Item.SubItems.Add(entrySize)
			e.Item.SubItems.Add(entry.Count.ToString("N0", TheApp.InternalCultureInfo))
			e.Item.SubItems.Add(entry.Type)
			e.Item.SubItems.Add(entry.Extension)
			e.Item.SubItems.Add(entry.PackageDataPathFileName)

			'NOTE: Need this special case for empty string because Images.ContainsKey() will not return True when the empty string is in Images.
			'      Prevents adding multiple empty strings to Images, which can add up quickly when hovering over a file without an extension
			'      and can cause flickering of the treeview that shares the ImageList.
			If entry.Extension = "" AndAlso Not entry.IsFolder Then
				' Must use ImageIndex instead of ImageKey when ListView.VirtualMode = True.
				e.Item.ImageIndex = 1
			Else
				If Not Me.ImageList1.Images.ContainsKey(entry.Extension) Then
					Dim anIcon As Bitmap
					If entry.IsFolder Then
						anIcon = Win32Api.GetShellIcon(entry.Name, Win32Api.FILE_ATTRIBUTE_DIRECTORY)
					Else
						anIcon = Win32Api.GetShellIcon(entry.Name)
					End If
					Me.ImageList1.Images.Add(entry.Extension, anIcon)
				End If
				' Must use ImageIndex instead of ImageKey when ListView.VirtualMode = True.
				e.Item.ImageIndex = Me.ImageList1.Images.IndexOfKey(entry.Extension)
			End If

			If Not entry.PackageDataPathFileNameExists Then
				e.Item.ForeColor = SystemColors.GrayText
				'e.item.BackColor = SystemColors
			End If
		Else
			e.Item = New ListViewItem()
			e.Item.SubItems.Add("0")
			e.Item.SubItems.Add("0")
			e.Item.SubItems.Add("0")
			e.Item.SubItems.Add("0")
			e.Item.SubItems.Add("0")
		End If
	End Sub

	Private Sub PackageListView_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles PackageListView.ItemSelectionChanged
		Me.UpdateSelectionCounts()
	End Sub

	Private Sub PackageListView_VirtualItemsSelectionRangeChanged(sender As Object, e As ListViewVirtualItemsSelectionRangeChangedEventArgs) Handles PackageListView.VirtualItemsSelectionRangeChanged
		Me.UpdateSelectionCounts()
	End Sub

	Private Sub ToggleSizeUnitsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToggleSizeUnitsToolStripMenuItem.Click
		Me.ToggleSizeUnits()
	End Sub

	Private Sub SearchToolStripTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SearchToolStripTextBox.KeyPress
		If e.KeyChar = ChrW(Keys.Return) Then
			Me.SearchForSubstringInFolderAndFileNames()
		End If
	End Sub

	Private Sub SearchToolStripTextBox_Click(sender As Object, e As EventArgs) Handles SearchToolStripButton.Click
		Me.SearchForSubstringInFolderAndFileNames()
	End Sub

#End Region

#Region "Core Event Handlers"

	'NOTE: This is run in a background thread.
	Private Sub SearchBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
		Me.CreateTreeNodesThatMatchTextToSearchFor_Recursively(e, Me.theSelectedTreeNode, Me.theResultsRootTreeNode)
	End Sub

	Private Sub SearchBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 1 Then
			Me.theResultsRootTreeNode.Text = "<Found> " + Me.theTextToSearchFor + " (" + Me.theResultsCount.ToString("N0", TheApp.InternalCultureInfo) + ")"
		End If
	End Sub

	Private Sub SearchBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim resultsText As String = "<Found> " + Me.theTextToSearchFor + " (" + Me.theResultsCount.ToString("N0", TheApp.InternalCultureInfo) + ")"
		If e.Cancelled Then
			resultsText += " <incomplete>"
		End If
		Me.theResultsRootTreeNode.Text = resultsText

		RemoveHandler Me.theSearchBackgroundWorker.DoWork, AddressOf Me.SearchBackgroundWorker_DoWork
		RemoveHandler Me.theSearchBackgroundWorker.ProgressChanged, AddressOf Me.SearchBackgroundWorker_ProgressChanged
		RemoveHandler Me.theSearchBackgroundWorker.RunWorkerCompleted, AddressOf Me.SearchBackgroundWorker_RunWorkerCompleted

		Me.SearchToolStripButton.Image = My.Resources.Search
		Me.SearchToolStripButton.Text = "Search"
		Me.theSelectedTreeNode.Nodes.Add(Me.theResultsRootTreeNode)
		Me.PackageTreeView.SelectedNode = Me.theResultsRootTreeNode

		Me.theSearchCount += 1
	End Sub

#End Region

#Region "Private Methods"

	Private Function GetDisplayName(ByVal entry As SourcePackageDirectoryEntry) As String
		Return Path.GetFileName(FileManager.GetPath(entry.DisplayPathFileName))
	End Function

	Private Function GetParentItem(ByVal childItem As SourcePackageDirectoryEntry) As SourcePackageDirectoryEntry
		Dim parentItem As New SourcePackageDirectoryEntry()
		parentItem.DisplayPathFileName = FileManager.GetPath(childItem.DisplayPathFileName)
		parentItem.PackageDataPathFileNameExists = childItem.PackageDataPathFileNameExists
		'If Not childItem.IsFolder Then
		'	parentItem.Size = childItem.Size
		'End If
		Return parentItem
	End Function

	Private Function GetTag(ByRef entry As SourcePackageDirectoryEntry, ByVal leafEntry As SourcePackageDirectoryEntry, ByVal entries As List(Of SourcePackageDirectoryEntry), ByVal isLeaf As Boolean, ByVal nonLeafEntryExists As Boolean) As List(Of SourcePackageDirectoryEntry)
		Dim fileName As String

		If entry Is leafEntry Then
			Dim fileExtension As String
			Dim fileExtensionWithDot As String = ""
			If entry.DisplayPathFileName.StartsWith("<") Then
				fileName = entry.DisplayPathFileName
				fileExtension = ""
			Else
				fileName = Path.GetFileName(entry.PathFileName)

				fileExtension = Path.GetExtension(entry.PathFileName)
				If Not String.IsNullOrEmpty(fileExtension) AndAlso fileExtension(0) = "."c Then
					fileExtensionWithDot = fileExtension
					fileExtension = fileExtension.Substring(1)
				End If
			End If

			entry.Name = fileName
			entry.Size = entry.DataSize
			entry.Count = 1
			If entry.DisplayPathFileName.StartsWith("<") Then
				entry.Type = "<internal data>"
			Else
				entry.Type = Win32Api.GetFileTypeDescription(fileExtensionWithDot)
			End If
			entry.Extension = fileExtension
			entry.IsFolder = False
		ElseIf nonLeafEntryExists Then
			Dim displayPathFileName As String = entry.DisplayPathFileName
			Dim packagePathFileNameExists As Boolean = leafEntry.PackageDataPathFileNameExists
			entry = entries.Find(Function(x) x.DisplayPathFileName = displayPathFileName)
			entry.Count += 1UL
			entry.Size += leafEntry.Size
			' Only change if True, so that a folder shows undimmed if at least one file in it exists.
			If packagePathFileNameExists Then
				entry.PackageDataPathFileNameExists = True
			End If
		Else
			fileName = Path.GetFileName(entry.DisplayPathFileName)
			entry.Name = fileName
			entry.Count = 1
			entry.Size = leafEntry.Size
			entry.Type = "Folder"
			entry.Extension = "<Folder>"
			entry.IsFolder = True
			'NOTE: Because same folder can be in multiple packages, don't bother showing which package the folder is in. Crowbar only shows the first one added to the list.
			entry.PackageDataPathFileName = ""
			entry.PackageDataPathFileNameExists = leafEntry.PackageDataPathFileNameExists
			'If Not entry.PackagePathFileNameExists Then
			'	TreeNode.ForeColor = SystemColors.GrayText
			'End If
		End If

		If Not nonLeafEntryExists Then
			If entries Is Nothing Then
				entries = New List(Of SourcePackageDirectoryEntry)()
				entries.Add(entry)
			Else
				entries.Add(entry)
			End If
		End If

		Return entries
	End Function

	Private Function GetDimmedStatus(ByVal entry As SourcePackageDirectoryEntry) As Boolean
		Return (Not entry.PackageDataPathFileNameExists)
	End Function

	Private Sub UpdateSelectionPathText()
		Dim selectionPathText As String = ""
		Dim aTreeNode As TreeNode = Me.PackageTreeView.SelectedNode

		If aTreeNode IsNot Nothing Then
			selectionPathText = aTreeNode.FullPath.Replace("\", "/")
		End If

		Me.SelectionPathTextBox.Text = selectionPathText
	End Sub

	Private Sub ShowFilesInSelectedFolder()
		Dim selectedTreeNode As TreeNode = Me.PackageTreeView.SelectedNode
		If selectedTreeNode IsNot Nothing AndAlso selectedTreeNode.Tag IsNot Nothing Then
			Dim list As List(Of SourcePackageDirectoryEntry) = CType(selectedTreeNode.Tag, List(Of SourcePackageDirectoryEntry))
			list.Sort(New SourcePackageDirectoryEntry.Comparer(Me.PackageListView.Columns(Me.theSortColumnIndex).Name, Me.PackageListView.Sorting))
			Me.PackageListView.VirtualListSize = list.Count
		Else
			Me.PackageListView.VirtualListSize = 0
		End If
		Me.PackageListView.Invalidate()
		Me.PackageListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
		Me.UpdateSelectionCounts()
	End Sub

	Private Sub UpdateSelectionCounts()
		Dim selectedFileCount As UInt64 = 0
		Dim totalFileCount As UInt64 = 0
		Me.theSelectedByteCount = 0

		Dim selectedTreeNode As TreeNode
		selectedTreeNode = Me.PackageTreeView.SelectedNode
		If selectedTreeNode IsNot Nothing AndAlso selectedTreeNode.Tag IsNot Nothing Then
			Dim entries As List(Of SourcePackageDirectoryEntry)
			entries = CType(selectedTreeNode.Tag, List(Of SourcePackageDirectoryEntry))

			'fileCount = list.Count
			'For Each item As ListViewItem In Me.PackageListView.Items
			'	totalFileCount += CType(item.Tag, PackageResourceFileNameInfo).Count
			'Next

			'For Each item As ListViewItem In Me.PackageListView.SelectedItems
			'	selectedFileCount += CType(item.Tag, PackageResourceFileNameInfo).Count
			'	selectedByteCount += CType(item.Tag, PackageResourceFileNameInfo).Size
			'Next

			For index As Integer = 0 To Me.PackageListView.Items.Count - 1
				totalFileCount += CType(Me.PackageListView.Items(index).Tag, SourcePackageDirectoryEntry).Count
			Next

			'TODO: Speed this up; takes about a second per 1,000 items.
			Dim selectedCount As Integer = Me.PackageListView.SelectedIndices.Count
			Dim selectedIndex As Integer
			Dim entry As SourcePackageDirectoryEntry
			If selectedCount = 0 Then
				selectedCount = Me.PackageListView.Items.Count
				For index As Integer = 0 To selectedCount - 1
					entry = CType(Me.PackageListView.Items(index).Tag, SourcePackageDirectoryEntry)
					selectedFileCount += entry.Count
					Me.theSelectedByteCount += entry.Size
				Next
			Else
				For index As Integer = 0 To selectedCount - 1
					selectedIndex = Me.PackageListView.SelectedIndices(index)
					entry = CType(Me.PackageListView.Items(selectedIndex).Tag, SourcePackageDirectoryEntry)
					selectedFileCount += entry.Count
					Me.theSelectedByteCount += entry.Size
				Next
			End If
		End If

		Me.FilesSelectedCountToolStripLabel.Text = selectedFileCount.ToString("N0", TheApp.InternalCultureInfo) + " / " + totalFileCount.ToString("N0", TheApp.InternalCultureInfo)

		If TheApp.Settings.UnpackByteUnitsOption = ByteUnitsOption.Binary Then
			Me.SizeSelectedTotalToolStripLabel.Text = MathModule.BinaryByteUnitsConversion(Me.theSelectedByteCount)
		Else
			Me.SizeSelectedTotalToolStripLabel.Text = Me.theSelectedByteCount.ToString("N0", TheApp.InternalCultureInfo)
		End If

		'IMPORTANT: Update the toolstrip so the items are resized properly. Needed because of the 'springing' textbox.
		Me.ToolStrip1.PerformLayout()
	End Sub

	Private Sub ToggleSizeUnits()
		If TheApp.Settings.UnpackByteUnitsOption = ByteUnitsOption.Binary Then
			TheApp.Settings.UnpackByteUnitsOption = ByteUnitsOption.Bytes
			Me.SizeSelectedTotalToolStripLabel.Text = Me.theSelectedByteCount.ToString("N0", TheApp.InternalCultureInfo)
		Else
			TheApp.Settings.UnpackByteUnitsOption = ByteUnitsOption.Binary
			Me.SizeSelectedTotalToolStripLabel.Text = MathModule.BinaryByteUnitsConversion(Me.theSelectedByteCount)
		End If

		Me.PackageListView.Invalidate()
		'IMPORTANT: Update the toolstrip so the items are resized properly. Needed because of the 'springing' textbox.
		Me.ToolStrip1.PerformLayout()
	End Sub

	Private Function GetEntriesFromFolderEntry(ByVal entries As List(Of SourcePackageDirectoryEntry), ByVal treeNode As TreeNode, ByVal packagePathFileNameToEntriesMap As SortedList(Of String, List(Of SourcePackageDirectoryEntry))) As SortedList(Of String, List(Of SourcePackageDirectoryEntry))
		Dim folderNode As TreeNode
		Dim folderResourceInfos As List(Of SourcePackageDirectoryEntry)

		If entries IsNot Nothing Then
			For Each entry As SourcePackageDirectoryEntry In entries
				If entry.IsFolder Then
					folderNode = GetNodeFromPath(Me.PackageTreeView.Nodes(0), treeNode.FullPath + "\" + entry.Name)
					folderResourceInfos = CType(folderNode.Tag, List(Of SourcePackageDirectoryEntry))
					packagePathFileNameToEntriesMap = Me.GetEntriesFromFolderEntry(folderResourceInfos, folderNode, packagePathFileNameToEntriesMap)
				Else
					Dim packagePathFileName As String
					'Dim packageEntry As BasePackageDirectoryEntry
					packagePathFileName = entry.PackageDataPathFileName
					'packageEntry = resourceInfo.Entry
					Dim packageEntries As List(Of SourcePackageDirectoryEntry)
					If packagePathFileNameToEntriesMap.Keys.Contains(packagePathFileName) Then
						packageEntries = packagePathFileNameToEntriesMap(packagePathFileName)
						'packageEntries.Add(packageEntry)
						packageEntries.Add(entry)
					Else
						packageEntries = New List(Of SourcePackageDirectoryEntry)()
						'packageEntries.Add(packageEntry)
						packageEntries.Add(entry)
						packagePathFileNameToEntriesMap.Add(packagePathFileName, packageEntries)
					End If
				End If
			Next
		End If

		Return packagePathFileNameToEntriesMap
	End Function

	Private Function GetNodeFromPath(node As TreeNode, path As String) As TreeNode
		Dim foundNode As TreeNode = Nothing
		If node.FullPath = path Then
			Return node
		End If
		For Each tn As TreeNode In node.Nodes
			If tn.FullPath = path Then
				Return tn
			ElseIf tn.Nodes.Count > 0 Then
				foundNode = GetNodeFromPath(tn, path)
			End If
			If foundNode IsNot Nothing Then
				Return foundNode
			End If
		Next
		Return Nothing
	End Function

	Private Sub OpenSelectedFolderOrFile()
		Dim selectedItem As ListViewItem = Me.PackageListView.Items(Me.PackageListView.SelectedIndices(0))

		Dim entry As SourcePackageDirectoryEntry = CType(selectedItem.Tag, SourcePackageDirectoryEntry)

		If entry.IsFolder Then
			Dim selectedTreeNode As TreeNode
			selectedTreeNode = Me.PackageTreeView.SelectedNode
			If selectedTreeNode Is Nothing Then
				selectedTreeNode = Me.PackageTreeView.Nodes(0)
			End If
			Me.PackageTreeView.SelectedNode = selectedTreeNode.Nodes(entry.DisplayPathFileName)
		Else
			Dim packagePathFileNameToEntriesMap As New SortedList(Of String, List(Of SourcePackageDirectoryEntry))()
			Dim packageEntries As New List(Of SourcePackageDirectoryEntry)()
			packageEntries.Add(entry)
			packagePathFileNameToEntriesMap.Add(entry.PackageDataPathFileName, packageEntries)
			' Extract the file to the user's temp folder and open it as if it were opened in File Explorer.
			TheApp.Unpacker.Run(PackageAction.UnpackToTempAndOpen, packagePathFileNameToEntriesMap, False, "")
		End If
	End Sub

	Private Sub DoDragAndDrop()
		Dim packagePathFileNameToEntriesMap As New SortedList(Of String, List(Of SourcePackageDirectoryEntry))
		Dim selectedRelativeOutputPath As String = ""
		Me.GetSelectedEntriesAndOutputPath(packagePathFileNameToEntriesMap, selectedRelativeOutputPath)

		Dim message As String = TheApp.Unpacker.RunSynchronous(PackageAction.UnpackToTemp, packagePathFileNameToEntriesMap, TheApp.Settings.UnpackFolderForEachPackageIsChecked, selectedRelativeOutputPath)
		'If message <> "" Then
		'	Me.UnpackerLogTextBox.AppendText(message + vbCr)
		'End If

		Dim tempRelativePathsAndFileNames As List(Of String) = Nothing
		tempRelativePathsAndFileNames = TheApp.Unpacker.GetTempRelativePathsAndFileNames()

		If tempRelativePathsAndFileNames.Count > 0 Then
			Dim pathAndFileNameCollection As New StringCollection()
			For Each pathOrFileName As String In tempRelativePathsAndFileNames
				If Not pathAndFileNameCollection.Contains(pathOrFileName) Then
					pathAndFileNameCollection.Add(pathOrFileName)
				End If
			Next

			Dim dragDropDataObject As DataObject
			dragDropDataObject = New DataObject()

			dragDropDataObject.SetFileDropList(pathAndFileNameCollection)

			Dim result As DragDropEffects
			result = Me.PackageListView.DoDragDrop(dragDropDataObject, DragDropEffects.Move)
			TheApp.Unpacker.DeleteTempUnpackFolder()
		End If
	End Sub

#Region "Search"

	'NOTE: Searches the folder selected in treeview. Includes searching through its subfolders.
	Private Sub SearchForSubstringInFolderAndFileNames()
		Me.theTextToSearchFor = Me.SearchToolStripTextBox.Text
		If Not String.IsNullOrWhiteSpace(Me.theTextToSearchFor) Then
			Me.theSelectedTreeNode = Me.PackageTreeView.SelectedNode
			If Me.theSelectedTreeNode Is Nothing Then
				Me.theSelectedTreeNode = Me.PackageTreeView.Nodes(0)
			End If

			Me.SearchToolStripButton.Image = My.Resources.CancelSearch
			Me.SearchToolStripButton.Text = "Cancel"

			Me.theResultsFileCount = 0
			Me.theResultsFolderCount = 0
			Me.theResultsCount = 0
			Dim resultsRootTreeNodeText As String = "<Found> " + Me.theTextToSearchFor + " (" + Me.theResultsCount.ToString("N0", TheApp.InternalCultureInfo) + ") <searching>"
			Me.theResultsRootTreeNode = New TreeNode(resultsRootTreeNodeText)

			Dim resultsFoldersTreeNodeText As String = "<Folders found> (0)"
			Me.theResultsFoldersTreeNode = New TreeNode(resultsFoldersTreeNodeText)
			Me.theResultsRootTreeNode.Nodes.Add(Me.theResultsFoldersTreeNode)

			Me.theSearchBackgroundWorker = New BackgroundWorker()
			Me.theSearchBackgroundWorker.WorkerReportsProgress = True
			Me.theSearchBackgroundWorker.WorkerSupportsCancellation = True
			AddHandler Me.theSearchBackgroundWorker.DoWork, AddressOf Me.SearchBackgroundWorker_DoWork
			AddHandler Me.theSearchBackgroundWorker.ProgressChanged, AddressOf Me.SearchBackgroundWorker_ProgressChanged
			AddHandler Me.theSearchBackgroundWorker.RunWorkerCompleted, AddressOf Me.SearchBackgroundWorker_RunWorkerCompleted
			Me.theSearchBackgroundWorker.RunWorkerAsync(Me.theResultsCount)
		End If
	End Sub

	'NOTE: This is run in a background thread.
	Private Sub CreateTreeNodesThatMatchTextToSearchFor_Recursively(ByVal e As DoWorkEventArgs, ByVal treeNodeToSearch As TreeNode, ByVal currentResultsTreeNode As TreeNode)
		Dim entries As List(Of SourcePackageDirectoryEntry)
		entries = CType(treeNodeToSearch.Tag, List(Of SourcePackageDirectoryEntry))

		If Me.theSearchBackgroundWorker.CancellationPending Then
			e.Cancel = True
			Exit Sub
		End If

		If entries IsNot Nothing Then
			Dim entryName As String
			Dim currentResultsTreeNodeList As List(Of SourcePackageDirectoryEntry)
			currentResultsTreeNodeList = CType(currentResultsTreeNode.Tag, List(Of SourcePackageDirectoryEntry))
			Dim currentResultsFolderTreeNodeList As List(Of SourcePackageDirectoryEntry) = CType(Me.theResultsFoldersTreeNode.Tag, List(Of SourcePackageDirectoryEntry))

			Dim nodeClone As TreeNode
			For Each entry As SourcePackageDirectoryEntry In entries
				If Not entry.IsFolder Then
					entryName = entry.Name.ToLower()
					If entryName.Contains(Me.theTextToSearchFor.ToLower()) Then
						If currentResultsTreeNodeList Is Nothing Then
							currentResultsTreeNodeList = New List(Of SourcePackageDirectoryEntry)()
							currentResultsTreeNode.Tag = currentResultsTreeNodeList
						End If
						currentResultsTreeNodeList.Add(entry)

						Me.theResultsFileCount += 1
						Me.theSearchBackgroundWorker.ReportProgress(1)
					End If
				Else
					entryName = entry.Name.ToLower()
					If entryName.Contains(Me.theTextToSearchFor.ToLower()) Then
						If currentResultsFolderTreeNodeList Is Nothing Then
							currentResultsFolderTreeNodeList = New List(Of SourcePackageDirectoryEntry)()
							Me.theResultsFoldersTreeNode.Tag = currentResultsFolderTreeNodeList
						End If
						Dim entryClone As SourcePackageDirectoryEntry = CType(entry.Clone(), SourcePackageDirectoryEntry)
						entryClone.Name = entryClone.DisplayPathFileName
						currentResultsFolderTreeNodeList.Add(entryClone)

						Me.theResultsFolderCount += 1

						'If Not Me.theResultsFoldersTreeNode.Nodes.ContainsKey(info.Name) Then
						Me.theResultsFoldersTreeNode.Text = "<Folders found> (" + Me.theResultsFolderCount.ToString("N0", TheApp.InternalCultureInfo) + ")"

						'TODO: Add a special Tag to above node that allows double-clicking on it to go to real folder.
						'End If

						'Me.theResultsCount += 1
						Me.theSearchBackgroundWorker.ReportProgress(1)
					End If
				End If

				If Me.theSearchBackgroundWorker.CancellationPending Then
					e.Cancel = True
					Exit Sub
				End If
			Next

			Dim count As Integer
			For Each node As TreeNode In treeNodeToSearch.Nodes
				If Not node.Text.StartsWith("<Found>") Then
					If Not currentResultsTreeNode.Nodes.ContainsKey(node.Name) Then
						'NOTE: Do not use node.Clone() because it includes the cloning of child nodes.
						'nodeClone = CType(node.Clone(), TreeNode)
						nodeClone = New TreeNode(node.Text)
						nodeClone.Name = node.Name
						currentResultsTreeNode.Nodes.Add(nodeClone)
						count = Me.theResultsFileCount

						Me.CreateTreeNodesThatMatchTextToSearchFor_Recursively(e, node, nodeClone)

						If Me.theSearchBackgroundWorker.CancellationPending Then
							e.Cancel = True
							Exit Sub
						End If

						Me.theResultsCount = Me.theResultsFileCount + Me.theResultsFolderCount
						If count = Me.theResultsFileCount Then
							currentResultsTreeNode.Nodes.Remove(nodeClone)
						Else
							For Each info As SourcePackageDirectoryEntry In entries
								If info.IsFolder Then
									entryName = info.Name.ToLower()

									If entryName = nodeClone.Name.ToLower() Then
										If currentResultsTreeNodeList Is Nothing Then
											currentResultsTreeNodeList = New List(Of SourcePackageDirectoryEntry)()
											currentResultsTreeNode.Tag = currentResultsTreeNodeList
										End If
										currentResultsTreeNodeList.Add(info)
									End If
								End If

								If Me.theSearchBackgroundWorker.CancellationPending Then
									e.Cancel = True
									Exit Sub
								End If
							Next
						End If
					End If
				End If

				If Me.theSearchBackgroundWorker.CancellationPending Then
					e.Cancel = True
					Exit Sub
				End If
			Next
		End If
	End Sub

	Private Sub DeleteSearch()
		Me.PackageTreeView.SelectedNode.Parent.Nodes.Remove(Me.PackageTreeView.SelectedNode)
		Me.theSearchCount -= 1
	End Sub

	Private Sub DeleteAllSearches()
		Me.DeleteSearchNodes_Recursively(Me.PackageTreeView.Nodes)
		Me.theSearchCount = 0
	End Sub

	Private Sub DeleteSearchNodes_Recursively(ByVal nodes As TreeNodeCollection)
		Dim aNode As TreeNode
		For i As Integer = nodes.Count - 1 To 0 Step -1
			aNode = nodes(i)
			If aNode.Text.StartsWith("<Found>") Then
				nodes.Remove(aNode)
			Else
				Me.DeleteSearchNodes_Recursively(aNode.Nodes)
			End If
		Next
	End Sub

#End Region

#End Region

#Region "Data"

	Private theRootText As String = "<root>"

	Private thePackageCount As Integer
	Private theEntries As List(Of SourcePackageDirectoryEntry)

	Private WithEvents PackageTreeViewCustomMenu As ContextMenuStrip
	Private WithEvents DeleteSearchToolStripMenuItem As New ToolStripMenuItem("Delete search")
	Private WithEvents DeleteAllSearchesToolStripMenuItem As New ToolStripMenuItem("Delete all searches")

	Private WithEvents PackageListViewCustomMenu As ContextMenuStrip
	Private WithEvents ToggleSizeUnitsToolStripMenuItem As New ToolStripMenuItem("Toggle size units")

	Private theSortColumnIndex As Integer

	Private theSearchBackgroundWorker As BackgroundWorker
	Private theSelectedTreeNode As TreeNode
	Private theResultsRootTreeNode As TreeNode
	Private theResultsFoldersTreeNode As TreeNode
	Private theTextToSearchFor As String
	Private theResultsFileCount As Integer
	Private theResultsFolderCount As Integer
	Private theResultsCount As Integer
	Private theSearchCount As Integer

	Private theSelectedByteCount As UInt64

#End Region

End Class
