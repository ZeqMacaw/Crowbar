Imports System.IO
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Globalization

Public Class UnpackUserControl

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

		Me.UnpackModeComboBox.DropDownWidth = 300
		Me.theUnpackModeIndexIsBeingChangedByMe = False
		Me.theUnpackPackagePathFolderOrFileNameIsBeingChangedByMe = False

		Me.PackageTreeViewCustomMenu = New ContextMenuStrip()
		Me.PackageTreeViewCustomMenu.Items.Add(Me.DeleteSearchToolStripMenuItem)
		Me.PackageTreeViewCustomMenu.Items.Add(Me.DeleteAllSearchesToolStripMenuItem)
		Me.PackageTreeView.ContextMenuStrip = Me.PackageTreeViewCustomMenu

		Me.theSearchCount = 0
	End Sub

	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing Then
				Me.Free()
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

#End Region

#Region "Init and Free"

	Protected Overrides Sub Init()
		MyBase.Init()

		' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
		If TheApp Is Nothing Then
			Exit Sub
		End If


		Me.PackagePathFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackPackagePathFolderOrFileName", False, DataSourceUpdateMode.OnValidation)

		Me.InitOutputPathComboBox()
		Me.OutputPathTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackOutputFullPath", False, DataSourceUpdateMode.OnValidation)
		Me.OutputSamePathTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackOutputSamePath", False, DataSourceUpdateMode.OnValidation)
		Me.OutputSubfolderTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackOutputSubfolderName", False, DataSourceUpdateMode.OnValidation)
		Me.UpdateOutputPathWidgets()

		'NOTE: Adding folder icon here means it is first in the image list, which is the icon used by default 
		Dim anIcon As Bitmap = Win32Api.GetShellIcon("folder", Win32Api.FILE_ATTRIBUTE_DIRECTORY)
		Me.ImageList1.Images.Add("<Folder>", anIcon)
		'NOTE: The TreeView.Sorted property does not show in Intellisense or Properties window.
		Me.PackageTreeView.Sorted = True
		Me.PackageTreeView.TreeViewNodeSorter = New NodeSorter()
		'Me.PackageTreeView.Nodes.Add("<root>", "<root>")

		Me.InitPackageDataGridView()

		Me.InitUnpackerOptions()

		Me.theOutputPathOrOutputFileName = ""
		Me.theUnpackedRelativePathFileNames = New BindingListEx(Of String)
		Me.UnpackedFilesComboBox.DataSource = Me.theUnpackedRelativePathFileNames

		Me.UpdateUnpackMode()
		Me.UpdateWidgets(False)

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged

		AddHandler Me.PackagePathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		AddHandler Me.OutputPathTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
	End Sub

	Protected Overrides Sub Free()
		MyBase.Free()

		' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
		If Not Me.InitHasBeenCalled OrElse TheApp Is Nothing Then
			Exit Sub
		End If

		RemoveHandler Me.PackagePathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		RemoveHandler Me.OutputPathTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
		RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.ListerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.ListerBackgroundWorker_RunWorkerCompleted
		If Me.theSearchBackgroundWorker IsNot Nothing Then
			RemoveHandler Me.theSearchBackgroundWorker.ProgressChanged, AddressOf Me.SearchBackgroundWorker_ProgressChanged
			RemoveHandler Me.theSearchBackgroundWorker.RunWorkerCompleted, AddressOf Me.SearchBackgroundWorker_RunWorkerCompleted
		End If
		RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.UnpackerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.UnpackerBackgroundWorker_RunWorkerCompleted

		Me.UnpackComboBox.DataBindings.Clear()
		Me.PackagePathFileNameTextBox.DataBindings.Clear()

		Me.OutputPathTextBox.DataBindings.Clear()
		Me.OutputSamePathTextBox.DataBindings.Clear()
		Me.OutputSubfolderTextBox.DataBindings.Clear()
		Me.FreeOutputPathComboBox()

		Me.FreeUnpackerOptions()

		Me.UnpackedFilesComboBox.DataSource = Nothing
	End Sub

	Private Sub InitOutputPathComboBox()
		Dim anEnumList As IList = EnumHelper.ToList(GetType(UnpackOutputPathOptions))
		Me.OutputPathComboBox.DataBindings.Clear()
		Try
			'TODO: Delete this line when game addons folder option is implemented.
			anEnumList.RemoveAt(UnpackOutputPathOptions.GameAddonsFolder)

			Me.OutputPathComboBox.DataSource = anEnumList
			Me.OutputPathComboBox.ValueMember = "Key"
			Me.OutputPathComboBox.DisplayMember = "Value"
			Me.OutputPathComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "UnpackOutputFolderOption", False, DataSourceUpdateMode.OnPropertyChanged)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		AddHandler Me.OutputPathComboBox.SelectedValueChanged, AddressOf Me.OutputPathComboBox_SelectedValueChanged
	End Sub

	Private Sub FreeOutputPathComboBox()
		RemoveHandler Me.OutputPathComboBox.SelectedValueChanged, AddressOf Me.OutputPathComboBox_SelectedValueChanged
		Me.OutputPathComboBox.DataBindings.Clear()
	End Sub

	Private Sub InitPackageDataGridView()
		'Me.PackageDataGridView.Columns.Add("Name", 100)
		'Me.PackageDataGridView.Columns.Add("Size (bytes)", 100)
		'Me.PackageDataGridView.Columns.Add("Count", 50)
		'Me.PackageDataGridView.Columns.Add("Type", 100)
		'Me.PackageDataGridView.Columns.Add("Extension", 100)
		'Me.PackageDataGridView.Columns.Add("Package", 100)
		'Me.theSortColumnIndex = 0
		'Me.PackageDataGridView.ListViewItemSorter = New FolderAndFileListViewItemComparer(Me.theSortColumnIndex, Me.PackageDataGridView.Sorting)

		Me.PackageDataGridView.AllowUserToAddRows = False
		Me.PackageDataGridView.AllowUserToDeleteRows = False
		Me.PackageDataGridView.AllowUserToResizeRows = False
		Me.PackageDataGridView.RowHeadersVisible = False
		Me.PackageDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None
		Me.PackageDataGridView.RowTemplate.Height = 17

		Me.PackageDataGridView.AutoGenerateColumns = False
		Dim iconColumn As DataGridViewImageColumn

		iconColumn = New DataGridViewImageColumn()
		iconColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		iconColumn.HeaderText = ""
		iconColumn.ImageLayout = DataGridViewImageCellLayout.Normal
		iconColumn.MinimumWidth = 17
		iconColumn.Name = "Icon"
		iconColumn.ReadOnly = True
		iconColumn.SortMode = DataGridViewColumnSortMode.Automatic
		iconColumn.Width = 17
		Me.PackageDataGridView.Columns.Add(iconColumn)

		Dim textColumn As DataGridViewTextBoxColumn

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.HeaderText = "Name"
		textColumn.MinimumWidth = 20
		textColumn.Name = "Name"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 100
		Me.PackageDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.HeaderText = "Size (bytes)"
		textColumn.MinimumWidth = 20
		textColumn.Name = "Size"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 100
		Me.PackageDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.HeaderText = "Count"
		textColumn.MinimumWidth = 20
		textColumn.Name = "Count"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 50
		Me.PackageDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.HeaderText = "Type"
		textColumn.MinimumWidth = 20
		textColumn.Name = "Type"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 100
		Me.PackageDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.HeaderText = "Extension"
		textColumn.MinimumWidth = 20
		textColumn.Name = "Extension"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 100
		Me.PackageDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.HeaderText = "Package File"
		textColumn.MinimumWidth = 20
		textColumn.Name = "Package"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 100
		Me.PackageDataGridView.Columns.Add(textColumn)

		'textColumn = New DataGridViewTextBoxColumn()
		'textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		'textColumn.HeaderText = "Tag"
		'textColumn.MinimumWidth = 20
		'textColumn.Name = "Tag"
		'textColumn.ReadOnly = True
		'textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		'textColumn.Visible = False
		'textColumn.Width = 100
		'Me.PackageDataGridView.Columns.Add(textColumn)

		Me.theListRowIsBeingAdded = False
	End Sub

	Private Sub InitUnpackerOptions()
		Me.FolderForEachPackageCheckBox.DataBindings.Add("Checked", TheApp.Settings, "UnpackFolderForEachPackageIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.KeepFullPathCheckBox.DataBindings.Add("Checked", TheApp.Settings, "UnpackKeepFullPathIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.LogFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "UnpackLogFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

	Private Sub FreeUnpackerOptions()
		Me.FolderForEachPackageCheckBox.DataBindings.Clear()
		Me.KeepFullPathCheckBox.DataBindings.Clear()
		Me.LogFileCheckBox.DataBindings.Clear()
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

	Public Sub RunUnpackerToGetListOfPackageContents()
		'NOTE: This is needed to handle when Crowbar is opened by double-clicking a vpk file.
		'      Every test on my dev computer without this code raised this exception: "This BackgroundWorker is currently busy and cannot run multiple tasks concurrently."
		If TheApp.Unpacker.IsBusy Then
			TheApp.Unpacker.CancelAsync()
			While TheApp.Unpacker.IsBusy
				Application.DoEvents()
			End While
		End If

		If TheApp.Settings.UnpackerIsRunning Then
			Exit Sub
		End If

		Me.PackageTreeView.Nodes.Clear()
		Me.PackageTreeView.Nodes.Add("<root>", "<refreshing>")
		Me.theUnpackedRelativePathFileNames.Clear()
		Me.UpdateWidgets(True)
		'Me.PackageTreeView.Nodes(0).Text = "<refreshing>"
		Me.PackageTreeView.Nodes(0).Nodes.Clear()
		Me.PackageTreeView.Nodes(0).Tag = Nothing
		Me.PackageDataGridView.Rows.Clear()
		Me.RefreshListingButton.Image = My.Resources.CancelRefresh
		Me.RefreshListingButton.Tag = "Cancel"
		Me.SkipCurrentPackageButton.Enabled = False
		'Me.CancelUnpackButton.Text = "Cancel Listing"
		Me.CancelUnpackButton.Enabled = False
		Me.UnpackerLogTextBox.Text = ""
		Me.thePackageCount = 0
		Me.UpdateSelectionCounts()

		'Me.theNodes = New List(Of TreeNode)
		'Me.PackageTreeView.BeginUpdate()

		AddHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.ListerBackgroundWorker_ProgressChanged
		AddHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.ListerBackgroundWorker_RunWorkerCompleted

		TheApp.Unpacker.Run(PackageAction.List, Nothing, False, "")
	End Sub

#End Region

#Region "Widget Event Handlers"

	Private Sub UnpackUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
		'NOTE: This code prevents Visual Studio often inexplicably extending the right side of these textboxes.
		Me.PackagePathFileNameTextBox.Size = New System.Drawing.Size(Me.RefreshListingButton.Left - Me.RefreshListingButton.Margin.Left - Me.PackagePathFileNameTextBox.Margin.Right - Me.PackagePathFileNameTextBox.Left, 21)
		Me.OutputPathTextBox.Size = New System.Drawing.Size(Me.BrowseForOutputPathButton.Left - Me.BrowseForOutputPathButton.Margin.Left - Me.OutputPathTextBox.Margin.Right - Me.OutputPathTextBox.Left, 21)
		Me.OutputSamePathTextBox.Size = New System.Drawing.Size(Me.BrowseForOutputPathButton.Left - Me.BrowseForOutputPathButton.Margin.Left - Me.OutputSamePathTextBox.Margin.Right - Me.OutputSamePathTextBox.Left, 21)
		Me.OutputSubfolderTextBox.Size = New System.Drawing.Size(Me.BrowseForOutputPathButton.Left - Me.BrowseForOutputPathButton.Margin.Left - Me.OutputSubfolderTextBox.Margin.Right - Me.OutputSubfolderTextBox.Left, 21)

		' [04-Feb-2026] Me.DesignMode is unreliable in nested widgets.
		'If Not Me.DesignMode Then
		Me.Init()
		'End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub UnpackModeComboBox_TextChanged(sender As Object, e As EventArgs) Handles UnpackModeComboBox.TextChanged
		Me.ToolTip1.SetToolTip(Me.UnpackModeComboBox, UnpackModeComboBox.Text)
	End Sub

	'Private Sub VpkPathFileNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VpkPathFileNameTextBox.Validated
	'	Me.VpkPathFileNameTextBox.Text = FileManager.GetCleanPathFileName(Me.VpkPathFileNameTextBox.Text)
	'End Sub

	Private Sub RefreshListingButton_Click(sender As Object, e As EventArgs) Handles RefreshListingButton.Click
		If CStr(Me.RefreshListingButton.Tag) = "Refresh" Then
			Me.RunUnpackerToGetListOfPackageContents()
		Else
			TheApp.Unpacker.CancelAsync()
		End If
	End Sub

	Private Sub BrowseForPackagePathFolderOrFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForPackagePathFolderOrFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()

		openFileWdw.Title = "Open the file or folder you want to unpack"
		If File.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
			openFileWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.UnpackPackagePathFolderOrFileName)
			'ElseIf Directory.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
			'	openFileWdw.InitialDirectory = TheApp.Settings.UnpackPackagePathFolderOrFileName
		Else
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.UnpackPackagePathFolderOrFileName)
			If openFileWdw.InitialDirectory = "" Then
				openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		End If
		openFileWdw.FileName = "[Folder Selection]"
		openFileWdw.Filter = "Source Engine Package Files (*.apk;*.fpx;*.gma;*.vpk)|*.apk;*.fpx;*.gma;*.vpk|Fairy Tale Busters APK Files (*.apk)|*.apk|Tactical Intervention FPX Files (*.fpx)|*.fpx|Garry's Mod GMA Files (*.gma)|*.gma|Source Engine VPK Files (*.vpk)|*.vpk"
		'openFileWdw.Filter = "Source Engine Package Files (*.vpk;*.fpx;*.gma;*.hfs)|*.vpk;*.fpx;*.gma;*.hfs|Source Engine VPK Files (*.vpk)|*.vpk|Tactical Intervention FPX Files (*.fpx)|*.fpx|Garry's Mod GMA Files (*.gma)|*.gma|Vindictus HFS Files (*.hfs)|*.hfs"
		openFileWdw.AddExtension = True
		openFileWdw.CheckFileExists = False
		openFileWdw.Multiselect = False
		openFileWdw.ValidateNames = True

		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Try
				If Path.GetFileName(openFileWdw.FileName).StartsWith("[Folder Selection]") Then
					TheApp.Settings.UnpackPackagePathFolderOrFileName = FileManager.GetPath(openFileWdw.FileName)
				Else
					TheApp.Settings.UnpackPackagePathFolderOrFileName = openFileWdw.FileName
				End If
			Catch ex As IO.PathTooLongException
				MessageBox.Show("The file or folder you tried to select has too many characters in it. Try shortening it by moving the model files somewhere else or by renaming folders or files." + vbCrLf + vbCrLf + "Error message generated by Windows: " + vbCrLf + ex.Message, "The File or Folder You Tried to Select Is Too Long", MessageBoxButtons.OK)
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub GotoPackageButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoPackageButton.Click
		FileManager.OpenWindowsExplorer(TheApp.Settings.UnpackPackagePathFolderOrFileName)
	End Sub

	Private Sub OutputPathComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
		' Because UnpackOutputPathOptions is passed to DataSource as an IList, must manually bind for this direction.
		TheApp.Settings.UnpackOutputFolderOption = CType(Me.OutputPathComboBox.SelectedValue, UnpackOutputPathOptions)
	End Sub

	Private Sub OutputPathComboBox_TextChanged(sender As Object, e As EventArgs) Handles OutputPathComboBox.TextChanged
		Me.ToolTip1.SetToolTip(Me.OutputPathComboBox, OutputPathComboBox.Text)
	End Sub

	Private Sub OutputPathTextBox_DragDrop(sender As Object, e As DragEventArgs) Handles OutputPathTextBox.DragDrop
		Dim pathFileNames() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
		Dim pathFileName As String = pathFileNames(0)
		If Directory.Exists(pathFileName) Then
			TheApp.Settings.UnpackOutputFullPath = pathFileName
		End If
	End Sub

	Private Sub OutputPathTextBox_DragEnter(sender As Object, e As DragEventArgs) Handles OutputPathTextBox.DragEnter
		If e.Data.GetDataPresent(DataFormats.FileDrop) Then
			e.Effect = DragDropEffects.Copy
		End If
	End Sub

	Private Sub OutputPathTextBox_Validated(sender As Object, e As EventArgs) Handles OutputPathTextBox.Validated
		Me.UpdateOutputPathTextBox()
	End Sub

	Private Sub BrowseForOutputPathButton_Click(sender As Object, e As EventArgs) Handles BrowseForOutputPathButton.Click
		Me.BrowseForOutputPath()
	End Sub

	Private Sub GotoOutputPathButton_Click(sender As Object, e As EventArgs) Handles GotoOutputPathButton.Click
		Me.GotoFolder()
	End Sub

	Private Sub UseDefaultOutputSubfolderButton_Click(sender As Object, e As EventArgs) Handles UseDefaultOutputSubfolderButton.Click
		TheApp.Settings.SetDefaultUnpackOutputSubfolderName()
	End Sub

	'TODO: Change this to detect pressing of Enter key.
	'Private Sub FindToolStripTextBox_Validated(sender As Object, e As EventArgs) Handles FindToolStripTextBox.Validated
	'	Me.FindTextInPackageFiles(FindDirection.Next)
	'End Sub

	Private Sub PackageTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles PackageTreeView.AfterSelect
		Me.UpdateSelectionPathText()
		Me.ShowFilesInSelectedFolder()
	End Sub

	Private Sub PackageTreeView_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles PackageTreeView.ItemDrag
		If Me.PackageTreeView.SelectedNode IsNot Nothing Then
			Me.RunUnpackerToUnpackFilesInternal(PackageAction.UnpackToTemp, Nothing)
		End If
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
		Me.PackageDataGridView.ClearSelection()
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

	Private Sub PackageDataGridView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles PackageDataGridView.CellDoubleClick
		If e.RowIndex >= 0 Then
			Me.OpenSelectedFolderOrFile()
		End If
	End Sub

	Private Sub PackageDataGridView_SortCompare(ByVal sender As Object, ByVal e As DataGridViewSortCompareEventArgs) Handles PackageDataGridView.SortCompare
		If e.Column.Name = "Name" Then
			Dim dgv As DataGridView = CType(sender, DataGridView)
			Dim extensionValue1 As String = CStr(dgv.Rows(e.RowIndex1).Cells("Extension").Value)
			Dim extensionValue2 As String = CStr(dgv.Rows(e.RowIndex2).Cells("Extension").Value)

			If extensionValue1 = "<Folder>" AndAlso extensionValue2 <> "<Folder>" Then
				If dgv.SortOrder = SortOrder.Ascending Then
					e.SortResult = -1
				Else
					e.SortResult = 1
				End If
				e.Handled = True
			ElseIf extensionValue1 <> "<Folder>" AndAlso extensionValue2 = "<Folder>" Then
				If dgv.SortOrder = SortOrder.Ascending Then
					e.SortResult = 1
				Else
					e.SortResult = -1
				End If
				e.Handled = True
			Else
				e.Handled = False
			End If
		End If
	End Sub

	'Private Sub PackageDataGridView_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles PackageDataGridView.ItemDrag
	'	If Me.PackageDataGridView.SelectedRows.Count > 0 Then
	'		Me.RunUnpackerToExtractFiles(PackageAction.UnpackToTemp, Me.PackageDataGridView.SelectedRows)
	'	End If
	'End Sub

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

	'Private Sub PackageDataGridView_MouseDown(sender As Object, e As MouseEventArgs) Handles PackageDataGridView.MouseDown
	'	' Get the row index where the mouse was clicked
	'	Dim hitTest As DataGridView.HitTestInfo = PackageDataGridView.HitTest(e.X, e.Y)
	'	rowIndexFromMouseDown = hitTest.RowIndex

	'	' Ensure a valid row was clicked and it's the left mouse button
	'	If rowIndexFromMouseDown <> -1 AndAlso e.Button = MouseButtons.Left Then
	'		' Optional: Select the row if it's not already selected
	'		If Not PackageDataGridView.Rows(rowIndexFromMouseDown).Selected Then
	'			PackageDataGridView.ClearSelection()
	'			PackageDataGridView.Rows(rowIndexFromMouseDown).Selected = True
	'		End If
	'	End If
	'End Sub

	Private Sub PackageDataGridView_MouseMove(sender As Object, e As MouseEventArgs) Handles PackageDataGridView.MouseMove
		' Check if a row index was recorded in MouseDown and the left button is down
		'If rowIndexFromMouseDown <> -1 AndAlso e.Button = MouseButtons.Left Then
		'	' Check if the mouse has moved enough to initiate a drag operation
		'	If Math.Abs(e.X - PackageDataGridView.GetCellDisplayRectangle(0, rowIndexFromMouseDown, False).X) > SystemInformation.DragSize.Width OrElse
		'	   Math.Abs(e.Y - PackageDataGridView.GetCellDisplayRectangle(0, rowIndexFromMouseDown, False).Y) > SystemInformation.DragSize.Height Then

		'		' Start the drag-and-drop operation
		'		' Pass the row index as the data to be dragged and allow Move or Copy effects
		'		PackageDataGridView.DoDragDrop(rowIndexFromMouseDown, DragDropEffects.Move Or DragDropEffects.Copy)
		'	End If
		'End If
		If e.Button = MouseButtons.Left Then
			If Me.PackageDataGridView.SelectedRows.Count > 0 Then
				Me.RunUnpackerToExtractFiles(PackageAction.UnpackToTemp, Me.PackageDataGridView.SelectedRows)
			End If
		End If
	End Sub

	'Private Sub PackageListView_KeyDown(sender As Object, e As KeyEventArgs) Handles PackageDataGridView.KeyDown
	'	If e.KeyCode = Keys.A And e.Control Then
	'		Me.PackageDataGridView.BeginUpdate()
	'		For Each i As ListViewItem In Me.PackageDataGridView.Items
	'			i.Selected = True
	'		Next
	'		Me.PackageDataGridView.EndUpdate()
	'	End If
	'End Sub

	Private Sub PackageDataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles PackageDataGridView.SelectionChanged
		If Not Me.theListRowIsBeingAdded Then
			Me.UpdateSelectionCounts()
		End If
	End Sub

	Private Sub ToggleSizeUnitsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToggleSizeUnitsToolStripMenuItem.Click
		Me.ToggleSizeUnits()
	End Sub

	Private Sub FindToolStripTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FindToolStripTextBox.KeyPress
		If e.KeyChar = ChrW(Keys.Return) Then
			Me.FindSubstringInFileNames()
		End If
	End Sub

	Private Sub FindToolStripButton_Click(sender As Object, e As EventArgs) Handles FindToolStripButton.Click
		Me.FindSubstringInFileNames()
	End Sub

	Private Sub UnpackOptionsUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles UnpackOptionsUseDefaultsButton.Click
		TheApp.Settings.SetDefaultUnpackOptions()
	End Sub

	Private Sub UnpackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnpackButton.Click
		If Me.PackageDataGridView.SelectedRows.Count > 0 Then
			Me.RunUnpackerToExtractFiles(PackageAction.Unpack, Me.PackageDataGridView.SelectedRows)
		Else
			Me.RunUnpackerToUnpackFilesInternal(PackageAction.Unpack, Nothing)
		End If
	End Sub

	Private Sub SkipCurrentPackageButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SkipCurrentPackageButton.Click
		TheApp.Unpacker.SkipCurrentPackage()
	End Sub

	Private Sub CancelUnpackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelUnpackButton.Click
		TheApp.Unpacker.CancelAsync()
	End Sub

	Private Sub UseAllInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseAllInDecompileButton.Click
		TheApp.Settings.DecompileMdlPathFileName = TheApp.Unpacker.GetOutputPathOrOutputFileName()
	End Sub

	Private Sub UseInPreviewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInPreviewButton.Click
		TheApp.Settings.PreviewMdlPathFileName = TheApp.Unpacker.GetOutputPathFileName(Me.theUnpackedRelativePathFileNames(Me.UnpackedFilesComboBox.SelectedIndex))
		'TheApp.Settings.PreviewGameSetupSelectedIndex = TheApp.Settings.UnpackGameSetupSelectedIndex
	End Sub

	Private Sub UseInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInDecompileButton.Click
		TheApp.Settings.DecompileMdlPathFileName = TheApp.Unpacker.GetOutputPathFileName(Me.theUnpackedRelativePathFileNames(Me.UnpackedFilesComboBox.SelectedIndex))
	End Sub

	Private Sub GotoUnpackedFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoUnpackedFileButton.Click
		Dim pathFileName As String
		pathFileName = TheApp.Unpacker.GetOutputPathFileName(Me.theUnpackedRelativePathFileNames(Me.UnpackedFilesComboBox.SelectedIndex))
		FileManager.OpenWindowsExplorer(pathFileName)
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "UnpackPackagePathFolderOrFileName" AndAlso Not Me.theUnpackPackagePathFolderOrFileNameIsBeingChangedByMe Then
			Me.UpdateUnpackMode()
			Me.UpdateOutputPathWidgets()
			Me.RunUnpackerToGetListOfPackageContents()
		ElseIf e.PropertyName = "UnpackModeIndex" AndAlso Not Me.theUnpackModeIndexIsBeingChangedByMe Then
			Me.UpdateFromModeIndexChange()
		ElseIf e.PropertyName = "UnpackOutputFolderOption" Then
			Me.UpdateOutputPathWidgets()
		ElseIf e.PropertyName = "UnpackGameSetupSelectedIndex" Then
			Me.UpdateGameModelsOutputPathTextBox()
		ElseIf e.PropertyName.StartsWith("Unpack") AndAlso e.PropertyName.EndsWith("IsChecked") Then
			Me.UpdateWidgets(TheApp.Settings.UnpackerIsRunning)
		End If
	End Sub

	Private Sub ListerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 0 Then
			'TODO: Having the updating of disabled widgets here is unusual, so why not move this to before calling the backgroundworker?
			'      One advantage to doing before call: Indicates to user that action has started even when opening file takes a while.
			'Me.UpdateWidgets(True)
			'Me.PackageTreeView.Nodes(0).Nodes.Clear()
			'Me.PackageTreeView.Nodes(0).Tag = Nothing
			'Me.PackageListView.Items.Clear()
			'Me.RefreshListingToolStripButton.Text = "Cancel"
			'Me.SkipCurrentPackageButton.Enabled = False
			''Me.CancelUnpackButton.Text = "Cancel Listing"
			'Me.CancelUnpackButton.Enabled = False
			'Me.UnpackerLogTextBox.Text = ""
			''Me.theEntryIndex = -1
		ElseIf e.ProgressPercentage = 1 Then
			'Me.theEntryIndex = -1
			Me.thePackageCount += 1
			Me.UpdateContentsGroupBox()
			'ElseIf e.ProgressPercentage = 2 Then
			'	Me.theArchivePathFileName = CStr(e.UserState)
			'ElseIf e.ProgressPercentage = 3 Then
			'	Me.theArchivePathFileNameExists = (CStr(e.UserState) = "True")
			'ElseIf e.ProgressPercentage = 4 Then
			'	Me.theEntryIndex += 1
			'	Me.AddEntryTreeNode(CType(e.UserState, BasePackageDirectoryEntry))
			'ElseIf e.ProgressPercentage = 5 Then
			'	Me.theEntryIndex += 1
			'	Dim entries As List(Of BasePackageDirectoryEntry) = CType(e.UserState, List(Of BasePackageDirectoryEntry))
			'	For Each entry As BasePackageDirectoryEntry In entries
			'		Me.AddEntryTreeNode(entry)
			'	Next
		ElseIf e.ProgressPercentage = 50 Then
			Me.UnpackerLogTextBox.Text = ""
			Me.UnpackerLogTextBox.AppendText(CStr(e.UserState) + vbCr)
			'NOTE: Set the textbox to show first line of text.
			Me.UnpackerLogTextBox.Select(0, 0)
		ElseIf e.ProgressPercentage = 51 Then
			Me.UnpackerLogTextBox.AppendText(CStr(e.UserState) + vbCr)
			'NOTE: Set the textbox to show first line of text.
			Me.UnpackerLogTextBox.Select(0, 0)
		ElseIf e.ProgressPercentage = 100 Then
		End If
	End Sub

	Private Sub ListerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.ListerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.ListerBackgroundWorker_RunWorkerCompleted

		If Not e.Cancelled Then
			Dim unpackResultInfo As UnpackerOutputInfo
			unpackResultInfo = CType(e.Result, UnpackerOutputInfo)

			' Much faster to read all entries and transfer entire list at end of background process rather than transferring each entry.
			Dim entries As List(Of SourcePackageDirectoryEntry) = unpackResultInfo.entries
			Dim GetId As Func(Of SourcePackageDirectoryEntry, String) = (Function(entry) FileManager.GetPath(entry.DisplayPathFileName))
			Dim GetTag As TreeViewEx.GetTagDelegate(Of SourcePackageDirectoryEntry, List(Of SourcePackageDirectoryEntry)) = AddressOf Me.GetTag
			Dim GetDimmedStatus As Func(Of SourcePackageDirectoryEntry, Boolean) = AddressOf Me.GetDimmedStatus
			Me.PackageTreeView.InsertItems(Me.PackageTreeView.Nodes(0), entries, GetId, AddressOf Me.GetDisplayName, AddressOf Me.GetParentItem, GetTag, GetDimmedStatus)

			If Me.PackageTreeView.Nodes(0).Nodes.Count = 0 AndAlso Me.PackageTreeView.Nodes(0).Tag Is Nothing Then
				Me.PackageTreeView.Nodes.Clear()
			Else
				Me.PackageTreeView.Nodes(0).Text = "<root>"
			End If
		Else
			Me.PackageTreeView.Nodes(0).Text = "<root-incomplete>"
		End If

		If Me.PackageTreeView.Nodes.Count > 0 Then
			Me.PackageTreeView.Nodes(0).Expand()
			Me.PackageTreeView.SelectedNode = Me.PackageTreeView.Nodes(0)
			Me.ShowFilesInSelectedFolder()
		End If
		Me.UpdateSelectionPathText()
		Me.RefreshListingButton.Image = My.Resources.Refresh
		Me.RefreshListingButton.Tag = "Refresh"
		''IMPORTANT: Update the toolstrip so the Refresh button does not disappear. Not sure why it disappears without this.
		'Me.ToolStrip1.PerformLayout()
		Me.UpdateWidgets(False)
	End Sub

	Private Sub SearchBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 1 Then
			Me.theResultsRootTreeNode.Text = "<Found> " + Me.theTextToFind + " (" + Me.theResultsCount.ToString("N0", TheApp.InternalCultureInfo) + ")"
		End If
	End Sub

	Private Sub SearchBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim resultsText As String = "<Found> " + Me.theTextToFind + " (" + Me.theResultsCount.ToString("N0", TheApp.InternalCultureInfo) + ")"
		If e.Cancelled Then
			resultsText += " <incomplete>"
		End If
		Me.theResultsRootTreeNode.Text = resultsText

		RemoveHandler Me.theSearchBackgroundWorker.DoWork, AddressOf Me.CreateTreeNodesThatMatchTextToFind
		RemoveHandler Me.theSearchBackgroundWorker.ProgressChanged, AddressOf Me.SearchBackgroundWorker_ProgressChanged
		RemoveHandler Me.theSearchBackgroundWorker.RunWorkerCompleted, AddressOf Me.SearchBackgroundWorker_RunWorkerCompleted

		Me.FindToolStripButton.Image = My.Resources.Find
		Me.FindToolStripButton.Text = "Find"
		Me.theSelectedTreeNode.Nodes.Add(Me.theResultsRootTreeNode)
		Me.PackageTreeView.SelectedNode = Me.theResultsRootTreeNode

		Me.theSearchCount += 1
	End Sub

	Private Sub UnpackerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		'If e.ProgressPercentage = 75 Then
		'	Me.DoDragAndDrop(CType(e.UserState, BindingListEx(Of String)))
		'	Exit Sub
		'End If

		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			'TODO: Having the updating of disabled widgets here is unusual, so why not move this to before calling the backgroundworker?
			'      One advantage to doing before call: Indicates to user that action has started even when opening file takes a while.
			Me.UnpackerLogTextBox.Text = ""
			Me.UnpackerLogTextBox.AppendText(line + vbCr)
			Me.theOutputPathOrOutputFileName = ""
			Me.UpdateWidgets(True)
		ElseIf e.ProgressPercentage = 1 Then
			Me.UnpackerLogTextBox.AppendText(line + vbCr)
		ElseIf e.ProgressPercentage = 100 Then
			Me.UnpackerLogTextBox.AppendText(line + vbCr)
		End If
	End Sub

	Private Sub UnpackerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If Not e.Cancelled AndAlso e.Result IsNot Nothing Then
			Dim unpackResultInfo As UnpackerOutputInfo
			unpackResultInfo = CType(e.Result, UnpackerOutputInfo)

			Me.UpdateUnpackedRelativePathFileNames(unpackResultInfo.theUnpackedRelativePathFileNames)
			Me.theOutputPathOrOutputFileName = TheApp.Unpacker.GetOutputPathOrOutputFileName()
		End If

		RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.UnpackerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.UnpackerBackgroundWorker_RunWorkerCompleted

		Me.UpdateWidgets(False)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateFromModeIndexChange()
		Dim gameSetupsOffset As Integer = TheApp.Unpacker.UnpackModes.IndexOf("------") + 1
		If TheApp.Settings.UnpackModeIndex >= gameSetupsOffset Then
			Dim gameSetup As GameSetup = TheApp.Settings.GameSetups(TheApp.Settings.UnpackModeIndex - gameSetupsOffset)
			Dim gameSetupPath As String = FileManager.GetPath(gameSetup.GamePathFileName)

			If TheApp.Settings.UnpackPackagePathFolderOrFileName <> gameSetupPath Then
				Me.theUnpackPackagePathFolderOrFileNameIsBeingChangedByMe = True
				TheApp.Settings.UnpackPackagePathFolderOrFileName = gameSetupPath
				Me.theUnpackPackagePathFolderOrFileNameIsBeingChangedByMe = False
			End If

			Me.UpdateUnpackMode()
			Me.UpdateOutputPathWidgets()
		End If
		Me.RunUnpackerToGetListOfPackageContents()
	End Sub

	Private Sub UpdateUnpackMode()
		Dim firstTime As Boolean = (Me.UnpackModeComboBox.DataBindings.Count = 0)
		Me.UnpackModeComboBox.DataBindings.Clear()
		Dim previousSelectedUnpackModeIndex As Integer = TheApp.Settings.UnpackModeIndex
		Dim count As Integer = TheApp.Unpacker.UnpackModes.Count
		TheApp.Unpacker.RefreshUnpackModes()
		If TheApp.Unpacker.UnpackModes.Count > count Then
			previousSelectedUnpackModeIndex += TheApp.Unpacker.UnpackModes.Count - count
		End If

		Try
			If File.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
				' Set file mode when a file is selected.
				previousSelectedUnpackModeIndex = TheApp.Unpacker.UnpackModes.IndexOf("File")
			ElseIf Directory.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
				Dim folderIndexIsRemoved As Boolean = False
				'NOTE: Remove in reverse index order.
				Dim packageExtensions As List(Of String) = SourcePackage.GetListOfPackageExtensions()
				For Each packageExtension As String In packageExtensions
					For Each anPackagePathFileName As String In Directory.GetFiles(TheApp.Settings.UnpackPackagePathFolderOrFileName, packageExtension)
						If anPackagePathFileName.Length = 0 Then
							TheApp.Unpacker.UnpackModes.RemoveAt(TheApp.Unpacker.UnpackModes.IndexOf("Folder"))
							previousSelectedUnpackModeIndex -= 1
							folderIndexIsRemoved = True
							Exit For
						End If
					Next
					If folderIndexIsRemoved Then
						Exit For
					End If
				Next
				TheApp.Unpacker.UnpackModes.RemoveAt(TheApp.Unpacker.UnpackModes.IndexOf("File"))
				previousSelectedUnpackModeIndex -= 1
				'Else
				'	Exit Try
			End If

			Me.UnpackModeComboBox.DataSource = TheApp.Unpacker.UnpackModes
			Me.UnpackModeComboBox.ValueMember = "Key"
			Me.UnpackModeComboBox.DisplayMember = "Value"
			Me.UnpackModeComboBox.DataBindings.Add("SelectedIndex", TheApp.Settings, "UnpackModeIndex", False, DataSourceUpdateMode.OnPropertyChanged)

			Me.theUnpackModeIndexIsBeingChangedByMe = True
			If firstTime Then
				previousSelectedUnpackModeIndex += count - TheApp.Unpacker.UnpackModes.Count
			End If
			If previousSelectedUnpackModeIndex < TheApp.Unpacker.UnpackModes.Count Then
				TheApp.Settings.UnpackModeIndex = previousSelectedUnpackModeIndex
			Else
				TheApp.Settings.UnpackModeIndex = 0
			End If
			Me.theUnpackModeIndexIsBeingChangedByMe = False
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub UpdateOutputPathComboBox()
		Dim anEnumList As IList

		anEnumList = EnumHelper.ToList(GetType(UnpackOutputPathOptions))
		Me.OutputPathComboBox.DataBindings.Clear()
		Try
			'TODO: Delete this line when game addons folder option is implemented.
			anEnumList.RemoveAt(UnpackOutputPathOptions.GameAddonsFolder)

			Me.OutputPathComboBox.DataSource = anEnumList
			Me.OutputPathComboBox.ValueMember = "Key"
			Me.OutputPathComboBox.DisplayMember = "Value"
			Me.OutputPathComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "UnpackOutputFolderOption", False, DataSourceUpdateMode.OnPropertyChanged)

			' Do not use this line because it will override the value automatically assigned by the data bindings above.
			'Me.OutputPathComboBox.SelectedIndex = 0
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub UpdateOutputPathWidgets()
		Me.GameModelsOutputPathTextBox.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder)
		Me.OutputPathTextBox.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder)
		Me.OutputSamePathTextBox.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.SameFolder)
		Me.OutputSubfolderTextBox.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.Subfolder)
		Me.BrowseForOutputPathButton.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.SameFolder) OrElse (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder) OrElse (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder)
		Me.GotoOutputPathButton.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.SameFolder) OrElse (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder) OrElse (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder)
		Me.UseDefaultOutputSubfolderButton.Enabled = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.Subfolder)
		Me.UseDefaultOutputSubfolderButton.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.Subfolder)
		Me.UpdateOutputPathWidgets(TheApp.Settings.UnpackerIsRunning)

		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.SameFolder Then
			Dim parentPath As String = FileManager.GetPath(TheApp.Settings.UnpackPackagePathFolderOrFileName)
			TheApp.Settings.UnpackOutputSamePath = parentPath
		ElseIf TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder Then
			Me.UpdateGameModelsOutputPathTextBox()
		End If
	End Sub

	Private Sub UpdateOutputPathWidgets(ByVal unpackerIsRunning As Boolean)
		Me.BrowseForOutputPathButton.Enabled = (Not unpackerIsRunning) AndAlso (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder)
		Me.GotoOutputPathButton.Enabled = (Not unpackerIsRunning)
	End Sub

	Private Sub UpdateGameModelsOutputPathTextBox()
		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder Then
			Dim gameSetup As GameSetup
			Dim gamePath As String
			Dim gameModelsPath As String

			gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.UnpackGameSetupSelectedIndex)
			gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
			gameModelsPath = Path.Combine(gamePath, "models")

			Me.GameModelsOutputPathTextBox.Text = gameModelsPath
		End If
	End Sub

	Private Sub UpdateOutputPathTextBox()
		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder Then
			If String.IsNullOrEmpty(Me.OutputPathTextBox.Text) Then
				Try
					TheApp.Settings.UnpackOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
			End If
		End If
	End Sub

	Private Sub BrowseForOutputPath()
		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder Then
			'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
			'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
			Dim outputPathWdw As New OpenFileDialog()

			outputPathWdw.Title = "Open the folder you want as Output Folder"
			outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.UnpackOutputFullPath)
			If outputPathWdw.InitialDirectory = "" Then
				If File.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
					outputPathWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.UnpackPackagePathFolderOrFileName)
				ElseIf Directory.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
					outputPathWdw.InitialDirectory = TheApp.Settings.UnpackPackagePathFolderOrFileName
				Else
					outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
				End If
			End If
			outputPathWdw.FileName = "[Folder Selection]"
			outputPathWdw.AddExtension = False
			outputPathWdw.CheckFileExists = False
			outputPathWdw.Multiselect = False
			outputPathWdw.ValidateNames = False

			If outputPathWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
				' Allow dialog window to completely disappear.
				Application.DoEvents()

				TheApp.Settings.UnpackOutputFullPath = FileManager.GetPath(outputPathWdw.FileName)
			End If
		End If
	End Sub

	Private Sub GotoFolder()
		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.SameFolder Then
			FileManager.OpenWindowsExplorer(TheApp.Settings.UnpackOutputSamePath)
		ElseIf TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder Then
			Dim gameSetup As GameSetup
			Dim gamePath As String
			Dim gameModelsPath As String

			gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.UnpackGameSetupSelectedIndex)
			gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
			gameModelsPath = Path.Combine(gamePath, "models")

			If FileManager.PathExistsAfterTryToCreate(gameModelsPath) Then
				FileManager.OpenWindowsExplorer(gameModelsPath)
			End If
		ElseIf TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder Then
			FileManager.OpenWindowsExplorer(TheApp.Settings.UnpackOutputFullPath)
		End If
	End Sub

	Private Sub UpdateContentsGroupBox()
		If Me.thePackageCount > 1 Then
			Me.ContentsGroupBox.Text = "Contents of " + Me.thePackageCount.ToString("N0", TheApp.InternalCultureInfo) + " packages"
		Else
			Me.ContentsGroupBox.Text = "Contents of package"
		End If
	End Sub

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

	'Private Function GetTag(ByVal entry As BasePackageDirectoryEntry, ByVal list As List(Of PackageResourceFileNameInfo), ByVal isLeaf As Boolean) As List(Of PackageResourceFileNameInfo)
	'	Dim pathFileName As String = entry.DisplayPathFileName
	'	Dim fileSize As UInt64 = entry.DataSize
	'	Dim fileName As String

	'	Dim resourceInfo As New PackageResourceFileNameInfo()
	'	If isLeaf Then
	'		Dim fileExtension As String
	'		Dim fileExtensionWithDot As String = ""
	'		If pathFileName.StartsWith("<") Then
	'			fileName = pathFileName
	'			fileExtension = ""
	'		Else
	'			fileName = Path.GetFileName(pathFileName)

	'			fileExtension = Path.GetExtension(pathFileName)
	'			If Not String.IsNullOrEmpty(fileExtension) AndAlso fileExtension(0) = "."c Then
	'				fileExtensionWithDot = fileExtension
	'				fileExtension = fileExtension.Substring(1)
	'			End If
	'		End If

	'		resourceInfo.PathFileName = pathFileName
	'		resourceInfo.Name = fileName
	'		resourceInfo.Size = fileSize
	'		resourceInfo.Count = 1
	'		If pathFileName.StartsWith("<") Then
	'			resourceInfo.Type = "<internal data>"
	'		Else
	'			resourceInfo.Type = Win32Api.GetFileTypeDescription(fileExtensionWithDot)
	'		End If
	'		resourceInfo.Extension = fileExtension
	'		resourceInfo.IsFolder = False
	'		resourceInfo.PackagePathFileName = entry.PackagePathFileName
	'		resourceInfo.PackagePathFileNameExists = entry.PackagePathFileNameExists
	'		'resourceInfo.EntryIndex = Me.theEntryIndex
	'		resourceInfo.Entry = entry
	'	Else
	'		fileName = Path.GetFileName(pathFileName)
	'		'resourceInfo.PathFileName = resourcePathFileName
	'		resourceInfo.PathFileName = pathFileName
	'		resourceInfo.Name = fileName
	'		resourceInfo.Size = fileSize
	'		resourceInfo.Count = 1
	'		resourceInfo.Type = "Folder"
	'		resourceInfo.Extension = "<Folder>"
	'		resourceInfo.IsFolder = True
	'		'NOTE: Because same folder can be in multiple archives, don't bother showing which archive the folder is in. Crowbar only shows the first one added to the list.
	'		resourceInfo.PackagePathFileName = ""
	'		' Using this field to determine when to dim the folder in the treeview and listview.
	'		resourceInfo.PackagePathFileNameExists = entry.PackagePathFileNameExists
	'		'If Not resourceInfo.ArchivePathFileNameExists Then
	'		'	TreeNode.ForeColor = SystemColors.GrayText
	'		'End If
	'	End If

	'	If list Is Nothing Then
	'		list = New List(Of PackageResourceFileNameInfo)()
	'		list.Add(resourceInfo)
	'	Else
	'		list.Add(resourceInfo)
	'	End If

	'	Return list
	'End Function
	Private Function GetTag(ByRef entry As SourcePackageDirectoryEntry, ByVal leafEntry As SourcePackageDirectoryEntry, ByVal entries As List(Of SourcePackageDirectoryEntry), ByVal isLeaf As Boolean, ByVal nonLeafEntryExists As Boolean) As List(Of SourcePackageDirectoryEntry)
		Dim fileName As String

		'If isLeaf Then
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
			'Dim packagePathFileNameExists As Boolean = entry.PackagePathFileNameExists
			Dim packagePathFileNameExists As Boolean = leafEntry.PackageDataPathFileNameExists
			entry = entries.Find(Function(x) x.DisplayPathFileName = displayPathFileName)
			entry.Count += 1UL
			'entry.Size += size
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

	'Public Function GetTreeNodeForeColor(ByVal treeNodeForeColor As Color, ByVal entry As BasePackageDirectoryEntry) As Color
	'	If Not entry.PackagePathFileNameExists Then
	'		Return SystemColors.GrayText
	'	Else
	'		Return treeNodeForeColor
	'	End If
	'End Function

	Private Sub UpdateWidgets(ByVal unpackerIsRunning As Boolean)
		TheApp.Settings.UnpackerIsRunning = unpackerIsRunning

		Me.UnpackModeComboBox.Enabled = Not unpackerIsRunning
		Me.PackagePathFileNameTextBox.Enabled = Not unpackerIsRunning
		Me.BrowseForPackagePathFolderOrFileNameButton.Enabled = Not unpackerIsRunning

		Me.OutputPathComboBox.Enabled = Not unpackerIsRunning
		Me.OutputPathTextBox.Enabled = Not unpackerIsRunning
		Me.OutputSamePathTextBox.Enabled = Not unpackerIsRunning
		Me.OutputSubfolderTextBox.Enabled = Not unpackerIsRunning
		Me.UseDefaultOutputSubfolderButton.Enabled = Not unpackerIsRunning
		Me.UpdateOutputPathWidgets(unpackerIsRunning)

		'Me.SelectionGroupBox.Enabled = Not unpackerIsRunning

		Me.OptionsGroupBox.Enabled = Not unpackerIsRunning

		'Me.UnpackButton.Enabled = (Not unpackerIsRunning) AndAlso (Me.PackageTreeView.Nodes(0).Nodes.Count > 0)
		Dim folderResourceInfos As List(Of SourcePackageDirectoryEntry) = Nothing
		If Me.PackageTreeView.Nodes.Count > 0 Then
			folderResourceInfos = CType(Me.PackageTreeView.Nodes(0).Tag, List(Of SourcePackageDirectoryEntry))
		End If
		Me.UnpackButton.Enabled = (Not unpackerIsRunning) AndAlso (folderResourceInfos IsNot Nothing) AndAlso (folderResourceInfos.Count > 0)
		Me.SkipCurrentPackageButton.Enabled = unpackerIsRunning
		Me.CancelUnpackButton.Enabled = unpackerIsRunning
		Me.UseAllInDecompileButton.Enabled = Not unpackerIsRunning AndAlso Me.theOutputPathOrOutputFileName <> "" AndAlso Me.theUnpackedRelativePathFileNames.Count > 0

		Me.UnpackedFilesComboBox.Enabled = Not unpackerIsRunning AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
		Me.UseInPreviewButton.Enabled = Not unpackerIsRunning AndAlso Me.theOutputPathOrOutputFileName <> "" AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
		Me.UseInDecompileButton.Enabled = Not unpackerIsRunning AndAlso Me.theOutputPathOrOutputFileName <> "" AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
		Me.GotoUnpackedFileButton.Enabled = Not unpackerIsRunning AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
	End Sub

	Private Sub UpdateUnpackedRelativePathFileNames(ByVal iUnpackedRelativePathFileNames As BindingListEx(Of String))
		If iUnpackedRelativePathFileNames IsNot Nothing Then
			Me.theUnpackedRelativePathFileNames = iUnpackedRelativePathFileNames
			Me.theUnpackedRelativePathFileNames.Sort()
			'NOTE: Need to set to nothing first to force it to update.
			Me.UnpackedFilesComboBox.DataSource = Nothing
			Me.UnpackedFilesComboBox.DataSource = Me.theUnpackedRelativePathFileNames
		End If
	End Sub

	Private Sub UpdateSelectionPathText()
		Dim selectionPathText As String = ""
		Dim aTreeNode As TreeNode
		aTreeNode = Me.PackageTreeView.SelectedNode
		While aTreeNode IsNot Nothing
			If Not aTreeNode.Text.StartsWith("<Found>") Then
				selectionPathText = aTreeNode.Name + "/" + selectionPathText
			End If
			aTreeNode = aTreeNode.Parent
		End While
		Me.SelectionPathTextBox.Text = selectionPathText
	End Sub

	'Private Sub SetNodeText(ByVal treeNode As TreeNode, ByVal fileCount As Integer)
	'	Dim folderCountText As String
	'	If treeNode.Nodes.Count = 1 Then
	'		folderCountText = "1 folder "
	'	Else
	'		folderCountText = treeNode.Nodes.Count.ToString("N0", TheApp.InternalCultureInfo) + " folders "
	'	End If
	'	Dim fileCountText As String
	'	If fileCount = 1 Then
	'		fileCountText = "1 file"
	'	Else
	'		fileCountText = fileCount.ToString("N0", TheApp.InternalCultureInfo) + " files"
	'	End If
	'	treeNode.Text = treeNode.Name + " <" + folderCountText + fileCountText + ">"
	'End Sub

	'Private Sub ShowFilesInSelectedFolder()
	'	Me.PackageDataGridView.Items.Clear()

	'	Dim selectedTreeNode As TreeNode
	'	selectedTreeNode = Me.PackageTreeView.SelectedNode
	'	If selectedTreeNode IsNot Nothing AndAlso selectedTreeNode.Tag IsNot Nothing Then
	'		Dim list As List(Of PackageResourceFileNameInfo)
	'		list = CType(selectedTreeNode.Tag, List(Of PackageResourceFileNameInfo))

	'		Dim item As ListViewItem
	'		Dim anIcon As Bitmap
	'		For Each info As PackageResourceFileNameInfo In list
	'			item = New ListViewItem(info.Name)
	'			item.Tag = info
	'			If info.IsFolder Then
	'				'Dim treeNodeForFolder As TreeNode
	'				'Dim listForFolder As List(Of PackageResourceFileNameInfo)
	'				'Dim itemCountText As String
	'				'treeNodeForFolder = selectedTreeNode.Nodes.Find(info.Name, False)(0)
	'				'listForFolder = CType(treeNodeForFolder.Tag, List(Of PackageResourceFileNameInfo))
	'				'itemCountText = listForFolder.Count.ToString("N0", TheApp.InternalCultureInfo)
	'				''If listForFolder.Count = 1 Then
	'				''	itemCountText += " item"
	'				''Else
	'				''	itemCountText += " items"
	'				''End If
	'				'item.SubItems.Add(itemCountText)
	'				item.SubItems.Add(info.Size.ToString("N0", TheApp.InternalCultureInfo))
	'				item.SubItems.Add(info.Count.ToString("N0", TheApp.InternalCultureInfo))
	'			Else
	'				item.SubItems.Add(info.Size.ToString("N0", TheApp.InternalCultureInfo))
	'				item.SubItems.Add(info.Count.ToString("N0", TheApp.InternalCultureInfo))
	'			End If
	'			item.SubItems.Add(info.Type)
	'			item.SubItems.Add(info.Extension)
	'			item.SubItems.Add(info.ArchivePathFileName)

	'			If Not Me.ImageList1.Images.ContainsKey(info.Extension) Then
	'				If info.IsFolder Then
	'					anIcon = Win32Api.GetShellIcon(info.Name, Win32Api.FILE_ATTRIBUTE_DIRECTORY)
	'				Else
	'					anIcon = Win32Api.GetShellIcon(info.Name)
	'				End If
	'				Me.ImageList1.Images.Add(info.Extension, anIcon)
	'			End If
	'			item.ImageKey = info.Extension

	'			If Not info.ArchivePathFileNameExists Then
	'				item.ForeColor = SystemColors.GrayText
	'				'item.BackColor = SystemColors
	'			End If

	'			Me.PackageDataGridView.Items.Add(item)
	'		Next

	'		Me.PackageDataGridView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
	'	End If

	'	Me.UpdateSelectionCounts()
	'End Sub

	Private Sub ShowFilesInSelectedFolder()
		' Wrap with this Boolean to prevent unwanted SelectionChanged handling.
		Me.theListRowIsBeingAdded = True

		Me.PackageDataGridView.Rows.Clear()

		Dim selectedTreeNode As TreeNode
		selectedTreeNode = Me.PackageTreeView.SelectedNode
		If selectedTreeNode IsNot Nothing AndAlso selectedTreeNode.Tag IsNot Nothing Then
			Dim list As List(Of PackageResourceFileNameInfo)
			list = CType(selectedTreeNode.Tag, List(Of PackageResourceFileNameInfo))

			Dim row As DataGridViewRow
			Dim anIcon As Bitmap
			For Each info As PackageResourceFileNameInfo In list
				' Add the row first, so that the column/cell Name can be used to set its Value.
				Dim rowIndex As Integer = Me.PackageDataGridView.Rows.Add()
				row = Me.PackageDataGridView.Rows(rowIndex)

				row.Tag = info
				row.Cells("Name").Value = info.Name
				'If info.IsFolder Then
				'	item.SubItems.Add(info.Size.ToString("N0", TheApp.InternalCultureInfo))
				'	item.SubItems.Add(info.Count.ToString("N0", TheApp.InternalCultureInfo))
				'Else
				row.Cells("Size").Value = info.Size.ToString("N0", TheApp.InternalCultureInfo)
				row.Cells("Count").Value = info.Count.ToString("N0", TheApp.InternalCultureInfo)
				'End If
				row.Cells("Type").Value = info.Type
				row.Cells("Extension").Value = info.Extension
				row.Cells("Package").Value = info.ArchivePathFileName

				If Not Me.ImageList1.Images.ContainsKey(info.Extension) Then
					If info.IsFolder Then
						anIcon = Win32Api.GetShellIcon(info.Name, Win32Api.FILE_ATTRIBUTE_DIRECTORY)
					Else
						anIcon = Win32Api.GetShellIcon(info.Name)
					End If
					Me.ImageList1.Images.Add(info.Extension, anIcon)
				End If
				row.Cells("Icon").Value = Me.ImageList1.Images(info.Extension)

				If Not info.ArchivePathFileNameExists Then
					row.DefaultCellStyle.ForeColor = SystemColors.GrayText
					'item.BackColor = SystemColors
				End If
			Next

			' If the DataGridView is not currently sorted, then sortedColumn is Nothing.
			Dim sortedColumn As DataGridViewColumn = Me.PackageDataGridView.SortedColumn
			If sortedColumn Is Nothing Then
				Me.PackageDataGridView.Sort(Me.PackageDataGridView.Columns("Name"), ListSortDirection.Ascending)
			Else
				Dim direction As ListSortDirection
				If Me.PackageDataGridView.SortOrder = SortOrder.Ascending Then
					direction = ListSortDirection.Ascending
				Else
					direction = ListSortDirection.Descending
				End If
				Me.PackageDataGridView.Sort(sortedColumn, direction)
			End If

			Me.PackageDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
			Me.PackageDataGridView.ClearSelection()
		End If

		Me.theListRowIsBeingAdded = False
		Me.UpdateSelectionCounts()
	End Sub

	'NOTE: Searches the folder (and its subfolders) selected in treeview.
	Private Sub FindSubstringInFileNames()
		Me.theTextToFind = Me.FindToolStripTextBox.Text
		If Not String.IsNullOrWhiteSpace(Me.theTextToFind) Then
			Me.theSelectedTreeNode = Me.PackageTreeView.SelectedNode
			If Me.theSelectedTreeNode Is Nothing Then
				Me.theSelectedTreeNode = Me.PackageTreeView.Nodes(0)
			End If

			Me.FindToolStripButton.Image = My.Resources.CancelSearch
			Me.FindToolStripButton.Text = "Cancel"

			Me.theResultsFileCount = 0
			Me.theResultsFolderCount = 0
			Me.theResultsCount = 0
			Dim resultsRootTreeNodeText As String = "<Found> " + Me.theTextToFind + " (" + Me.theResultsCount.ToString("N0", TheApp.InternalCultureInfo) + ") <searching>"
			Me.theResultsRootTreeNode = New TreeNode(resultsRootTreeNodeText)

			Dim resultsFoldersTreeNodeText As String = "<Folders found> (0)"
			Me.theResultsFoldersTreeNode = New TreeNode(resultsFoldersTreeNodeText)
			Me.theResultsRootTreeNode.Nodes.Add(Me.theResultsFoldersTreeNode)

			Me.theSearchBackgroundWorker = New BackgroundWorker()
			Me.theSearchBackgroundWorker.WorkerReportsProgress = True
			Me.theSearchBackgroundWorker.WorkerSupportsCancellation = True
			AddHandler Me.theSearchBackgroundWorker.DoWork, AddressOf Me.CreateTreeNodesThatMatchTextToFind
			AddHandler Me.theSearchBackgroundWorker.ProgressChanged, AddressOf Me.SearchBackgroundWorker_ProgressChanged
			AddHandler Me.theSearchBackgroundWorker.RunWorkerCompleted, AddressOf Me.SearchBackgroundWorker_RunWorkerCompleted
			Me.theSearchBackgroundWorker.RunWorkerAsync(Me.theResultsCount)
		End If
	End Sub

	'NOTE: This is run in a background thread.
	Private Sub CreateTreeNodesThatMatchTextToFind(ByVal sender As Object, ByVal e As DoWorkEventArgs)
		Me.CreateTreeNodesThatMatchTextToFind(e, Me.theSelectedTreeNode, Me.theResultsRootTreeNode)
	End Sub

	'NOTE: This is run in a background thread.
	Private Sub CreateTreeNodesThatMatchTextToFind(ByVal e As DoWorkEventArgs, ByVal treeNodeToSearch As TreeNode, ByVal currentResultsTreeNode As TreeNode)
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
					If entryName.Contains(Me.theTextToFind.ToLower()) Then
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
					If entryName.Contains(Me.theTextToFind.ToLower()) Then
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

						Me.CreateTreeNodesThatMatchTextToFind(e, node, nodeClone)

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
			For Each row As DataGridViewRow In Me.PackageDataGridView.Rows
				totalFileCount += CType(row.Tag, PackageResourceFileNameInfo).Count
			Next

			For Each row As DataGridViewRow In Me.PackageDataGridView.SelectedRows
				selectedFileCount += CType(row.Tag, PackageResourceFileNameInfo).Count
				selectedByteCount += CType(row.Tag, PackageResourceFileNameInfo).Size
			Next
		End If
		'Me.UpdateSelectionCountsRecursive(selectedTreeNode, fileCount, sizeTotal)

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

	'Private Sub UpdateSelectionCountsRecursive(ByVal currentTreeNode As TreeNode, ByRef fileCount As Integer, ByRef sizeTotal As Long)
	'	If currentTreeNode IsNot Nothing AndAlso currentTreeNode.Tag IsNot Nothing Then
	'		Dim list As List(Of PackageResourceFileNameInfo)
	'		list = CType(currentTreeNode.Tag, List(Of PackageResourceFileNameInfo))

	'		fileCount += list.Count

	'		For Each item As ListViewItem In Me.PackageListView.SelectedItems
	'			sizeTotal += CType(item.Tag, PackageResourceFileNameInfo).Size
	'		Next

	'		For Each childNode As TreeNode In currentTreeNode.Nodes
	'			Me.UpdateSelectionCountsRecursive(childNode, fileCount, sizeTotal)
	'		Next
	'	End If
	'End Sub

	'Private Function GetEntriesFromFolderEntry(ByVal resourceInfos As List(Of PackageResourceFileNameInfo), ByVal treeNode As TreeNode, ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As SortedList(Of String, List(Of Integer))
	'	Dim folderNode As TreeNode
	'	Dim folderResourceInfos As List(Of PackageResourceFileNameInfo)

	'	If resourceInfos IsNot Nothing Then
	'		For Each resourceInfo As PackageResourceFileNameInfo In resourceInfos
	'			If resourceInfo.IsFolder Then
	'				folderNode = GetNodeFromPath(Me.PackageTreeView.Nodes(0), treeNode.FullPath + "\" + resourceInfo.Name)
	'				folderResourceInfos = CType(folderNode.Tag, List(Of PackageResourceFileNameInfo))
	'				archivePathFileNameToEntryIndexMap = Me.GetEntriesFromFolderEntry(folderResourceInfos, folderNode, archivePathFileNameToEntryIndexMap)
	'			Else
	'				Dim archivePathFileName As String
	'				Dim archiveEntryIndex As Integer
	'				archivePathFileName = resourceInfo.PackagePathFileName
	'				archiveEntryIndex = resourceInfo.EntryIndex
	'				Dim archiveEntryIndexes As List(Of Integer)
	'				If archivePathFileNameToEntryIndexMap.Keys.Contains(archivePathFileName) Then
	'					archiveEntryIndexes = archivePathFileNameToEntryIndexMap(archivePathFileName)
	'					archiveEntryIndexes.Add(archiveEntryIndex)
	'				Else
	'					archiveEntryIndexes = New List(Of Integer)()
	'					archiveEntryIndexes.Add(archiveEntryIndex)
	'					archivePathFileNameToEntryIndexMap.Add(archivePathFileName, archiveEntryIndexes)
	'				End If
	'			End If
	'		Next
	'	End If

	'	Return archivePathFileNameToEntryIndexMap
	'End Function
	'======
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
		Dim selectedRow As DataGridViewRow
		selectedRow = Me.PackageDataGridView.SelectedRows(0)

		Dim resourceInfo As PackageResourceFileNameInfo
		resourceInfo = CType(selectedRow.Tag, PackageResourceFileNameInfo)

		If entry.IsFolder Then
			Dim selectedTreeNode As TreeNode
			selectedTreeNode = Me.PackageTreeView.SelectedNode
			If selectedTreeNode Is Nothing Then
				selectedTreeNode = Me.PackageTreeView.Nodes(0)
			End If
			Me.PackageTreeView.SelectedNode = selectedTreeNode.Nodes(entry.DisplayPathFileName)
		Else
			' Extract the file to the user's temp folder and open it as if it were opened in File Explorer.
			'Dim archivePathFileNameToEntryIndexMap As New SortedList(Of String, List(Of Integer))()
			'Dim archiveEntryIndexes As New List(Of Integer)()
			'archiveEntryIndexes.Add(resourceInfo.EntryIndex)
			'archivePathFileNameToEntryIndexMap.Add(resourceInfo.ArchivePathFileName, archiveEntryIndexes)
			'TheApp.Unpacker.Run(PackageAction.UnpackToTempAndOpen, archivePathFileNameToEntryIndexMap, False, "")
			Dim packagePathFileNameToEntriesMap As New SortedList(Of String, List(Of SourcePackageDirectoryEntry))()
			Dim packageEntries As New List(Of SourcePackageDirectoryEntry)()
			'packageEntries.Add(resourceInfo.Entry)
			packageEntries.Add(entry)
			packagePathFileNameToEntriesMap.Add(entry.PackageDataPathFileName, packageEntries)
			TheApp.Unpacker.Run(PackageAction.UnpackToTempAndOpen, packagePathFileNameToEntriesMap, False, "")
		End If
	End Sub

	Private Sub RunUnpackerToExtractFiles(ByVal unpackerAction As PackageAction, ByVal selectedItems As DataGridViewSelectedRowCollection)
		Dim selectedResourceInfo As PackageResourceFileNameInfo
		Dim selectedResourceInfos As New List(Of PackageResourceFileNameInfo)
		For Each selectedItem As DataGridViewRow In selectedItems
			selectedResourceInfo = CType(selectedItem.Tag, PackageResourceFileNameInfo)
			selectedResourceInfos.Add(selectedResourceInfo)
		Next

		Me.RunUnpackerToUnpackFilesInternal(unpackerAction, selectedResourceInfos)
	End Sub

	Private Sub RunUnpackerToUnpackFilesInternal(ByVal unpackerAction As PackageAction, ByVal selectedResourceInfos As List(Of SourcePackageDirectoryEntry))
		Dim selectedNode As TreeNode = Me.PackageTreeView.SelectedNode
		If selectedNode Is Nothing Then
			selectedNode = Me.PackageTreeView.Nodes(0)
		End If

		Dim selectedRelativeOutputPath As String
		If selectedResourceInfos Is Nothing Then
			selectedResourceInfos = CType(selectedNode.Tag, List(Of SourcePackageDirectoryEntry))

			If selectedResourceInfos Is Nothing Then
				' This is reached when trying to Unpack a search folder with 0 results.
				Exit Sub
			End If

			selectedRelativeOutputPath = selectedNode.FullPath.Replace("<root>\", "")
			selectedRelativeOutputPath = FileManager.GetPath(selectedRelativeOutputPath)
		Else
			selectedRelativeOutputPath = FileManager.GetPath(selectedResourceInfos(0).PathFileName)
		End If

		'Dim packagePathFileNameToEntryIndexMap As New SortedList(Of String, List(Of Integer))()
		'packagePathFileNameToEntryIndexMap = Me.GetEntriesFromFolderEntry(selectedResourceInfos, selectedNode, packagePathFileNameToEntryIndexMap)
		Dim packagePathFileNameToEntriesMap As New SortedList(Of String, List(Of SourcePackageDirectoryEntry))()
		packagePathFileNameToEntriesMap = Me.GetEntriesFromFolderEntry(selectedResourceInfos, selectedNode, packagePathFileNameToEntriesMap)

		AddHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.UnpackerBackgroundWorker_ProgressChanged
		AddHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.UnpackerBackgroundWorker_RunWorkerCompleted

		'NOTE: [21-Dec-2020] Must unbind this combobox to prevent slowdown on second and subsequent unpacks.
		Me.UnpackedFilesComboBox.DataSource = Nothing

		If unpackerAction = PackageAction.UnpackToTemp Then
			'Dim message As String = TheApp.Unpacker.RunSynchronous(unpackerAction, packagePathFileNameToEntryIndexMap, TheApp.Settings.UnpackFolderForEachPackageIsChecked, selectedRelativeOutputPath)
			Dim message As String = TheApp.Unpacker.RunSynchronous(unpackerAction, packagePathFileNameToEntriesMap, TheApp.Settings.UnpackFolderForEachPackageIsChecked, selectedRelativeOutputPath)
			If message <> "" Then
				Me.UnpackerLogTextBox.AppendText(message + vbCr)
			End If

			Dim tempRelativePathsAndFileNames As List(Of String) = Nothing
			tempRelativePathsAndFileNames = TheApp.Unpacker.GetTempRelativePathsAndFileNames()

			Me.DoDragAndDrop(tempRelativePathsAndFileNames)
		Else
			'TheApp.Unpacker.Run(unpackerAction, packagePathFileNameToEntryIndexMap, TheApp.Settings.UnpackFolderForEachPackageIsChecked, selectedRelativeOutputPath)
			TheApp.Unpacker.Run(unpackerAction, packagePathFileNameToEntriesMap, TheApp.Settings.UnpackFolderForEachPackageIsChecked, selectedRelativeOutputPath)
		End If
	End Sub

	Private Sub DoDragAndDrop(ByVal iUnpackedRelativePathsAndFileNames As List(Of String))
		If iUnpackedRelativePathsAndFileNames.Count > 0 Then
			Dim pathAndFileNameCollection As New StringCollection()
			For Each pathOrFileName As String In iUnpackedRelativePathsAndFileNames
				If Not pathAndFileNameCollection.Contains(pathOrFileName) Then
					pathAndFileNameCollection.Add(pathOrFileName)
				End If
			Next

			Dim dragDropDataObject As DataObject
			dragDropDataObject = New DataObject()

			dragDropDataObject.SetFileDropList(pathAndFileNameCollection)

			Dim result As DragDropEffects
			result = Me.PackageDataGridView.DoDragDrop(dragDropDataObject, DragDropEffects.Move)
			TheApp.Unpacker.DeleteTempUnpackFolder()

			RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.UnpackerBackgroundWorker_ProgressChanged
			RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.UnpackerBackgroundWorker_RunWorkerCompleted

			Me.UpdateWidgets(False)
		End If
	End Sub

	Private Sub DeleteSearch()
		Me.PackageTreeView.SelectedNode.Parent.Nodes.Remove(Me.PackageTreeView.SelectedNode)
		Me.theSearchCount -= 1
	End Sub

	Private Sub DeleteAllSearches()
		Me.RecursivelyDeleteSearchNodes(Me.PackageTreeView.Nodes)
		Me.theSearchCount = 0
	End Sub

	Private Sub RecursivelyDeleteSearchNodes(ByVal nodes As TreeNodeCollection)
		Dim aNode As TreeNode
		For i As Integer = nodes.Count - 1 To 0 Step -1
			aNode = nodes(i)
			If aNode.Text.StartsWith("<Found>") Then
				nodes.Remove(aNode)
			Else
				Me.RecursivelyDeleteSearchNodes(aNode.Nodes)
			End If
		Next
	End Sub

	'Private Sub AddEntryTreeNode(ByVal entry As BasePackageDirectoryEntry)
	'	'Example output:
	'	'addonimage.jpg crc=0x50ea4a15 metadatasz=0 fnumber=32767 ofs=0x0 sz=10749
	'	'addonimage.vtf crc=0xc75861f5 metadatasz=0 fnumber=32767 ofs=0x29fd sz=8400
	'	'addoninfo.txt crc=0xb3d2b571 metadatasz=0 fnumber=32767 ofs=0x4acd sz=1677
	'	'materials/models/weapons/melee/crowbar.vmt crc=0x4aaf5f0 metadatasz=0 fnumber=32767 ofs=0x515a sz=566
	'	'materials/models/weapons/melee/crowbar.vtf crc=0xded2e058 metadatasz=0 fnumber=32767 ofs=0x5390 sz=174920
	'	'materials/models/weapons/melee/crowbar_normal.vtf crc=0x7ac0e054 metadatasz=0 fnumber=32767 ofs=0x2fed8 sz=1398196

	'	'Try
	'	'Dim fields() As String
	'	'fields = line.Split(" "c)

	'	'Dim pathFileName As String = fields(0)
	'	''NOTE: The last 5 fields should not have any spaces, but the path+filename field might.
	'	'For fieldIndex As Integer = 1 To fields.Length - 6
	'	'	pathFileName = pathFileName + " " + fields(fieldIndex)
	'	'Next
	'	'Dim fileSize As UInt64
	'	'fileSize = CULng(CLng(fields(fields.Length - 1).Remove(0, 3)))

	'	Dim pathFileName As String = entry.DisplayPathFileName
	'	Dim fileSize As UInt64 = entry.DataSize

	'	Dim foldersAndFileName() As String
	'	foldersAndFileName = pathFileName.Split("/"c)
	'	Dim parentTreeNode As TreeNode = Nothing
	'	Dim treeNode As TreeNode = Nothing
	'	Dim list As List(Of PackageResourceFileNameInfo)
	'	If foldersAndFileName.Length = 1 Then
	'		treeNode = Me.PackageTreeView.Nodes(0)
	'	Else
	'		parentTreeNode = Me.PackageTreeView.Nodes(0)
	'		Dim resourcePathFileName As String = ""
	'		For nameIndex As Integer = 0 To foldersAndFileName.Length - 2
	'			'For nameIndex As Integer = 0 To 0
	'			Dim name As String
	'			name = foldersAndFileName(nameIndex)

	'			If nameIndex = 0 Then
	'				resourcePathFileName = name
	'			Else
	'				resourcePathFileName += Path.DirectorySeparatorChar + name
	'			End If

	'			If parentTreeNode.Nodes.ContainsKey(name) Then
	'				treeNode = parentTreeNode.Nodes.Item(parentTreeNode.Nodes.IndexOfKey(name))
	'				list = CType(parentTreeNode.Tag, List(Of PackageResourceFileNameInfo))
	'				For Each info As PackageResourceFileNameInfo In list
	'					If info.IsFolder AndAlso info.Name = name Then
	'						info.Count += 1UL
	'						info.Size += fileSize
	'						If Me.theArchivePathFileNameExists Then
	'							info.ArchivePathFileNameExists = Me.theArchivePathFileNameExists
	'							Dim temp As New TreeNode
	'							treeNode.ForeColor = temp.ForeColor
	'						End If
	'					End If
	'				Next
	'			Else
	'				treeNode = parentTreeNode.Nodes.Add(name)
	'				treeNode.Name = name

	'				Dim resourceInfo As New PackageResourceFileNameInfo()
	'				'resourceInfo.PathFileName = name
	'				resourceInfo.PathFileName = resourcePathFileName
	'				resourceInfo.Name = name
	'				resourceInfo.Size = fileSize
	'				resourceInfo.Count = 1
	'				resourceInfo.Type = "Folder"
	'				resourceInfo.Extension = "<Folder>"
	'				resourceInfo.IsFolder = True
	'				'resourceInfo.ArchivePathFileName = Me.theArchivePathFileName
	'				'NOTE: Because same folder can be in multiple archives, don't bother showing which archive the folder is in. Crowbar only shows the first one added to the list.
	'				resourceInfo.ArchivePathFileName = ""
	'				' Using this field to determine when to dim the folder in the treeview and listview.
	'				resourceInfo.ArchivePathFileNameExists = Me.theArchivePathFileNameExists
	'				If Not resourceInfo.ArchivePathFileNameExists Then
	'					treeNode.ForeColor = SystemColors.GrayText
	'				End If

	'				If parentTreeNode.Tag Is Nothing Then
	'					list = New List(Of PackageResourceFileNameInfo)()
	'					list.Add(resourceInfo)
	'					parentTreeNode.Tag = list
	'				Else
	'					list = CType(parentTreeNode.Tag, List(Of PackageResourceFileNameInfo))
	'					list.Add(resourceInfo)
	'				End If
	'			End If
	'			parentTreeNode = treeNode
	'		Next
	'	End If
	'	If treeNode IsNot Nothing Then
	'		Dim fileName As String
	'		Dim fileExtension As String
	'		Dim fileExtensionWithDot As String = ""
	'		If pathFileName.StartsWith("<") Then
	'			fileName = pathFileName
	'			fileExtension = ""
	'		Else
	'			fileName = Path.GetFileName(pathFileName)

	'			fileExtension = Path.GetExtension(pathFileName)
	'			If Not String.IsNullOrEmpty(fileExtension) AndAlso fileExtension(0) = "."c Then
	'				fileExtensionWithDot = fileExtension
	'				fileExtension = fileExtension.Substring(1)
	'			End If
	'		End If
	'		'Dim fileSize As UInt64
	'		'fileSize = CULng(CLng(fields(fields.Length - 1).Remove(0, 3)))
	'		Dim fileType As String
	'		fileType = "<type>"

	'		Dim resourceInfo As New PackageResourceFileNameInfo()
	'		resourceInfo.PathFileName = pathFileName
	'		resourceInfo.Name = fileName
	'		resourceInfo.Size = fileSize
	'		resourceInfo.Count = 1
	'		If pathFileName.StartsWith("<") Then
	'			resourceInfo.Type = "<internal data>"
	'		Else
	'			resourceInfo.Type = Win32Api.GetFileTypeDescription(fileExtensionWithDot)
	'		End If
	'		resourceInfo.Extension = fileExtension
	'		resourceInfo.IsFolder = False
	'		resourceInfo.ArchivePathFileName = Me.theArchivePathFileName
	'		resourceInfo.ArchivePathFileNameExists = Me.theArchivePathFileNameExists
	'		resourceInfo.EntryIndex = Me.theEntryIndex

	'		If treeNode.Tag Is Nothing Then
	'			list = New List(Of PackageResourceFileNameInfo)()
	'			list.Add(resourceInfo)
	'			treeNode.Tag = list
	'		Else
	'			list = CType(treeNode.Tag, List(Of PackageResourceFileNameInfo))
	'			list.Add(resourceInfo)
	'		End If

	'		'Me.SetNodeText(treeNode, list.Count)
	'	End If
	'	'Me.PackageTreeView.Nodes(0).Expand()
	'	'Catch ex As Exception
	'	'	'TODO: Try to catch an out-of-memory exception. Probably not going to work, though.
	'	'	Dim worker As Unpacker = CType(sender, Unpacker)
	'	'	worker.CancelAsync()
	'	'	Dim debug As Integer = 4242
	'	'End Try
	'End Sub

#End Region

#Region "Data"

	Private WithEvents PackageTreeViewCustomMenu As ContextMenuStrip
	Private WithEvents DeleteSearchToolStripMenuItem As New ToolStripMenuItem("Delete search")
	Private WithEvents DeleteAllSearchesToolStripMenuItem As New ToolStripMenuItem("Delete all searches")

	Private WithEvents PackageListViewCustomMenu As ContextMenuStrip
	Private WithEvents ToggleSizeUnitsToolStripMenuItem As New ToolStripMenuItem("Toggle size units")

	Private theUnpackModeIndexIsBeingChangedByMe As Boolean
	Private theUnpackPackagePathFolderOrFileNameIsBeingChangedByMe As Boolean

	Private theUnpackedRelativePathFileNames As BindingListEx(Of String)
	Private theOutputPathOrOutputFileName As String

	'Private theSortColumnIndex As Integer

	'Private thePackEntries As List(Of Integer)
	'Private theGivenHardLinkFileName As String

	Private thePackageCount As Integer
	Private theListRowIsBeingAdded As Boolean

	Private theSearchBackgroundWorker As BackgroundWorker
	Private theSelectedTreeNode As TreeNode
	Private theResultsRootTreeNode As TreeNode
	Private theResultsFoldersTreeNode As TreeNode
	Private theTextToFind As String
	Private theResultsFileCount As Integer
	Private theResultsFolderCount As Integer
	Private theResultsCount As Integer
	Private theSearchCount As Integer

	Private theSelectedByteCount As UInt64

#End Region

End Class
