Imports System.ComponentModel
Imports System.IO

Public Class SetUpGamesUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

		'NOTE: Try-Catch is needed so that widget will be shown in MainForm without raising exception.
		Try
			Me.Init()
		Catch
		End Try
	End Sub

#End Region

#Region "Init and Free"

	Protected Sub Init()
		Me.GameSetupComboBox.DisplayMember = "GameName"
		Me.GameSetupComboBox.ValueMember = "GameName"
		Me.GameSetupComboBox.DataSource = TheApp.Settings.GameSetups
		Me.GameSetupComboBox.DataBindings.Add("SelectedIndex", TheApp.Settings, "SetUpGamesGameSetupSelectedIndex", False, DataSourceUpdateMode.OnPropertyChanged)

		Dim textColumn As DataGridViewTextBoxColumn
		Dim buttonColumn As DataGridViewButtonColumn
		Me.SteamLibraryPathsDataGridView.AutoGenerateColumns = False
		Me.SteamLibraryPathsDataGridView.DataSource = TheApp.Settings.SteamLibraryPaths

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.DataPropertyName = "Macro"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.DisplayIndex = 0
		textColumn.HeaderText = "Macro"
		textColumn.Name = "Macro"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.NotSortable
		Me.SteamLibraryPathsDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.DataPropertyName = "LibraryPath"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.DisplayIndex = 1
		textColumn.FillWeight = 100
		textColumn.HeaderText = "Library Path"
		textColumn.Name = "LibraryPath"
		textColumn.SortMode = DataGridViewColumnSortMode.NotSortable
		Me.SteamLibraryPathsDataGridView.Columns.Add(textColumn)

		buttonColumn = New DataGridViewButtonColumn()
		buttonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		buttonColumn.DisplayIndex = 2
		buttonColumn.DefaultCellStyle.BackColor = SystemColors.Control
		buttonColumn.HeaderText = "Browse"
		buttonColumn.Name = "Browse"
		buttonColumn.SortMode = DataGridViewColumnSortMode.NotSortable
		buttonColumn.Text = "Browse..."
		buttonColumn.UseColumnTextForButtonValue = True
		Me.SteamLibraryPathsDataGridView.Columns.Add(buttonColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.DataPropertyName = "UseCount"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.DisplayIndex = 4
		textColumn.HeaderText = "Use Count"
		textColumn.Name = "UseCount"
		textColumn.SortMode = DataGridViewColumnSortMode.NotSortable
		Me.SteamLibraryPathsDataGridView.Columns.Add(textColumn)

		Me.SteamAppPathFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, "SteamAppPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)

		Me.UpdateWidgets()
		Me.UpdateUseCounts()

		If TheApp.Settings.SteamLibraryPaths.Count = 0 Then
			TheApp.Settings.SteamLibraryPaths.AddNew()
		End If

		AddHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
		AddHandler TheApp.Settings.GameSetups.ListChanged, AddressOf Me.GameSetups_ListChanged
		AddHandler Me.SteamLibraryPathsDataGridView.SetMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.SetMacroInSelectedGameSetupToolStripMenuItem_Click
		AddHandler Me.SteamLibraryPathsDataGridView.SetMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.SetMacroInAllGameSetupsToolStripMenuItem_Click
		AddHandler Me.SteamLibraryPathsDataGridView.ClearMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.ClearMacroInSelectedGameSetupToolStripMenuItem_Click
		AddHandler Me.SteamLibraryPathsDataGridView.ClearMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.ClearMacroInAllGameSetupsToolStripMenuItem_Click
		AddHandler Me.SteamLibraryPathsDataGridView.ChangeToThisMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.ChangeToThisMacroInSelectedGameSetupToolStripMenuItem_Click
		AddHandler Me.SteamLibraryPathsDataGridView.ChangeToThisMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.ChangeToThisMacroInAllGameSetupsToolStripMenuItem_Click
	End Sub

	Protected Sub Free()
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
		RemoveHandler TheApp.Settings.GameSetups.ListChanged, AddressOf Me.GameSetups_ListChanged
		RemoveHandler Me.GamePathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		RemoveHandler Me.SteamLibraryPathsDataGridView.SetMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.SetMacroInSelectedGameSetupToolStripMenuItem_Click
		RemoveHandler Me.SteamLibraryPathsDataGridView.SetMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.SetMacroInAllGameSetupsToolStripMenuItem_Click
		RemoveHandler Me.SteamLibraryPathsDataGridView.ClearMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.ClearMacroInSelectedGameSetupToolStripMenuItem_Click
		RemoveHandler Me.SteamLibraryPathsDataGridView.ClearMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.ClearMacroInAllGameSetupsToolStripMenuItem_Click
		RemoveHandler Me.SteamLibraryPathsDataGridView.ChangeToThisMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.ChangeToThisMacroInSelectedGameSetupToolStripMenuItem_Click
		RemoveHandler Me.SteamLibraryPathsDataGridView.ChangeToThisMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.ChangeToThisMacroInAllGameSetupsToolStripMenuItem_Click

		Me.GameSetupComboBox.DataSource = Nothing
		Me.GameSetupComboBox.DataBindings.Clear()
		Me.SteamAppPathFileNameTextBox.DataBindings.Clear()
		Me.SteamLibraryPathsDataGridView.DataSource = Nothing
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub GameSetupComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GameSetupComboBox.SelectedIndexChanged
		Me.UpdateWidgets()
		Me.UpdateWidgetsBasedOnGameEngine()
	End Sub

	Private Sub AddGameSetupButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddGameSetupButton.Click
		Dim gamesetup As New GameSetup()
		gamesetup.GameName = "<New Game>"
		TheApp.Settings.GameSetups.Add(gamesetup)

		Me.GameSetupComboBox.SelectedIndex = TheApp.Settings.GameSetups.IndexOf(gamesetup)

		Me.UpdateWidgets()
		Me.UpdateUseCounts()
	End Sub

	Private Sub BrowseForGamePathFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForGamePathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine LibList.gam File"
			openFileWdw.Filter = "GoldSource Engine LibList.gam File|liblist.gam|GAM Files (*.gam)|*.txt|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine GameInfo.txt File"
			openFileWdw.Filter = "Source Engine GameInfo.txt File|gameinfo.txt|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine GameInfo.gi File"
			openFileWdw.Filter = "Source 2 Engine GameInfo.gi File|gameinfo.gi|GI Files (*.gi)|*.txt|All Files (*.*)|*.*"
		End If
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theSelectedGameSetup.GamePathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theSelectedGameSetup.GamePathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			SetPathFileNameField(openFileWdw.FileName, Me.theSelectedGameSetup.GamePathFileNameUnprocessed)
		End If
	End Sub

	Private Sub BrowseForGameAppPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseForGameAppPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine Game's Executable File"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine Game's Executable File"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine Game's Executable File"
		End If
		openFileWdw.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theSelectedGameSetup.GameAppPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theSelectedGameSetup.GameAppPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			SetPathFileNameField(openFileWdw.FileName, Me.theSelectedGameSetup.GameAppPathFileNameUnprocessed)
		End If
	End Sub

	Private Sub ClearGameAppOptionsButton_Click(sender As Object, e As EventArgs) Handles ClearGameAppOptionsButton.Click
		Me.GameAppOptionsTextBox.Text = ""
	End Sub

	Private Sub BrowseForCompilerPathFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForCompilerPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine Model Compiler Tool"
			openFileWdw.Filter = "GoldSource Engine Model Compiler Tool File|studiomdl.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine Model Compiler Tool"
			openFileWdw.Filter = "Source Engine Model Compiler Tool File|studiomdl.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine Model Compiler Tool"
			openFileWdw.Filter = "Source 2 Engine Model Compiler Tool File|studiomdl.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		End If
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theSelectedGameSetup.CompilerPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theSelectedGameSetup.CompilerPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			SetPathFileNameField(openFileWdw.FileName, Me.theSelectedGameSetup.CompilerPathFileNameUnprocessed)
		End If
	End Sub

	Private Sub BrowseForViewerPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseForViewerPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine Model Viewer Tool"
			openFileWdw.Filter = "GoldSource Engine Model Viewer Tool File|hlmv.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine Model Viewer Tool"
			openFileWdw.Filter = "Source Engine Model Viewer Tool File|hlmv.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine Model Viewer Tool"
			openFileWdw.Filter = "Source 2 Engine Model Viewer Tool File|hlmv.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		End If
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theSelectedGameSetup.ViewerPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theSelectedGameSetup.ViewerPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			SetPathFileNameField(openFileWdw.FileName, Me.theSelectedGameSetup.ViewerPathFileNameUnprocessed)
		End If
	End Sub

	Private Sub BrowseForMappingToolPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseForMappingToolPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine Mapping Tool"
			openFileWdw.Filter = "GoldSource Engine Mapping Tool Files|hammer.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine Mapping Tool"
			openFileWdw.Filter = "Source Engine Mapping Tool Files|hammer.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine Mapping Tool"
			openFileWdw.Filter = "Source 2 Engine Mapping Tool Files|hammer.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		End If
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theSelectedGameSetup.MappingToolPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theSelectedGameSetup.MappingToolPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			SetPathFileNameField(openFileWdw.FileName, Me.theSelectedGameSetup.MappingToolPathFileNameUnprocessed)
		End If
	End Sub

	Private Sub BrowseForUnpackerPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseForUnpackerPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine Packer/Unpacker Tool"
			openFileWdw.Filter = "GoldSource Engine Packer/Unpacker Tool Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine Packer/Unpacker Tool"
			openFileWdw.Filter = "Source Engine Packer/Unpacker Tool Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine Packer/Unpacker Tool"
			openFileWdw.Filter = "Source 2 Engine Packer/Unpacker Tool Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		End If
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theSelectedGameSetup.PackerPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theSelectedGameSetup.PackerPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			SetPathFileNameField(openFileWdw.FileName, Me.theSelectedGameSetup.PackerPathFileNameUnprocessed)
		End If
	End Sub

	Private Sub CloneGameSetupButton_Click(sender As Object, e As EventArgs) Handles CloneGameSetupButton.Click
		Dim cloneGameSetup As GameSetup
		cloneGameSetup = CType(Me.theSelectedGameSetup.Clone(), GameSetup)
		cloneGameSetup.GameName = "Clone of " + Me.theSelectedGameSetup.GameName
		TheApp.Settings.GameSetups.Add(cloneGameSetup)

		Me.GameSetupComboBox.SelectedIndex = TheApp.Settings.GameSetups.IndexOf(cloneGameSetup)

		Me.UpdateWidgets()
		Me.UpdateUseCounts()
	End Sub

	Private Sub DeleteGameSetupButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteGameSetupButton.Click
		'Dim selectedIndex As Integer

		'selectedIndex = Me.GameSetupComboBox.SelectedIndex
		'If selectedIndex >= 0 AndAlso TheApp.Settings.GameSetups.Count > 1 Then
		'	TheApp.Settings.GameSetups.RemoveAt(selectedIndex)
		'End If
		TheApp.Settings.GameSetups.Remove(Me.theSelectedGameSetup)

		Me.UpdateWidgets()
		Me.UpdateUseCounts()
	End Sub

	Private Sub CreateModelsFolderTreeButton_Click(sender As Object, e As EventArgs) Handles CreateModelsFolderTreeButton.Click
		'TODO: [CreateModelsFolderTreeButton_Click] Call a function in Unpacker to do the unpacking.
		Dim gamePath As String = FileManager.GetPath(Me.theSelectedGameSetup.GamePathFileName)
		TheApp.Unpacker.UnpackFolderTreeFromVPK(gamePath)
	End Sub

	Private Sub BrowseForSteamAppPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseForSteamAppPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		openFileWdw.Title = "Select Steam Executable File"
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.Filter = "Steam Executable File|steam.exe|All Files (*.*)|*.*"
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.SteamAppPathFileName)
		openFileWdw.FileName = Path.GetFileName(TheApp.Settings.SteamAppPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			TheApp.Settings.SteamAppPathFileNameUnprocessed = openFileWdw.FileName
		End If
	End Sub

	Private Sub SteamLibraryPathsDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles SteamLibraryPathsDataGridView.CellContentClick
		Dim senderGrid As DataGridView = CType(sender, DataGridView)

		If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso e.RowIndex >= 0 Then
			Dim openFileWdw As New OpenFileDialog()
			openFileWdw.Title = "Select a Steam Library Folder"
			openFileWdw.CheckFileExists = False
			openFileWdw.Multiselect = False
			openFileWdw.ValidateNames = True
			'openFileWdw.Filter = "Source Engine Packer/Unpacker Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.SteamLibraryPaths(e.RowIndex).LibraryPath)
			'If Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
			'	openFileWdw.InitialDirectory = TheApp.Settings.DecompileMdlPathFileName
			'Else
			'	openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			'End If
			openFileWdw.FileName = "[Folder Selection]"
			If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
				' Allow dialog window to completely disappear.
				Application.DoEvents()

				Try
					TheApp.Settings.SteamLibraryPaths(e.RowIndex).LibraryPath = FileManager.GetLongestExtantPath(openFileWdw.FileName)
				Catch ex As IO.PathTooLongException
					MessageBox.Show("The file or folder you tried to select has too many characters in it. Try shortening it by moving the model files somewhere else or by renaming folders or files." + vbCrLf + vbCrLf + "Error message generated by Windows: " + vbCrLf + ex.Message, "The File or Folder You Tried to Select Is Too Long", MessageBoxButtons.OK)
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
			End If
		End If
	End Sub

	Private Sub SetMacroInSelectedGameSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.SetMacroInSelectedGameSetup()
	End Sub

	Private Sub SetMacroInAllGameSetupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.SetMacroInAllGameSetups()
	End Sub

	Private Sub ClearMacroInSelectedGameSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.ClearMacroInSelectedGameSetup()
	End Sub

	Private Sub ClearMacroInAllGameSetupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.ClearMacroInAllGameSetups()
	End Sub

	Private Sub ChangeToThisMacroInSelectedGameSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.ChangeToThisMacroInSelectedGameSetup()
	End Sub

	Private Sub ChangeToThisMacroInAllGameSetupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.ChangeToThisMacroInAllGameSetups()
	End Sub

	Private Sub AddLibraryPathButton_Click(sender As Object, e As EventArgs) Handles AddLibraryPathButton.Click
		Dim libraryPath As SteamLibraryPath
		libraryPath = TheApp.Settings.SteamLibraryPaths.AddNew()
		libraryPath.Macro = "<library" + (TheApp.Settings.SteamLibraryPaths.IndexOf(libraryPath) + 1).ToString() + ">"
	End Sub

	Private Sub DeleteLibraryPathButton_Click(sender As Object, e As EventArgs) Handles DeleteLibraryPathButton.Click
		' Do not allow first item to be deleted.
		If TheApp.Settings.SteamLibraryPaths.Count > 1 Then
			Dim itemIndex As Integer
			Dim aSteamLibraryPath As SteamLibraryPath
			itemIndex = TheApp.Settings.SteamLibraryPaths.Count - 1
			aSteamLibraryPath = TheApp.Settings.SteamLibraryPaths(itemIndex)
			If aSteamLibraryPath.UseCount = 0 Then
				TheApp.Settings.SteamLibraryPaths.Remove(aSteamLibraryPath)
			End If
		End If
	End Sub

	'Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
	'	TheApp.SaveAppSettings()
	'End Sub

	'Private Sub GoBackButton_Click(sender As Object, e As EventArgs) Handles GoBackButton.Click
	'	'TODO: Go back to the tab that opened the Set Up Games tab.
	'	Dim debug As Integer = 4242
	'End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "SteamAppPathFileName" Then
			Me.UpdateUseCounts()
		End If
	End Sub

	Protected Sub GameSetups_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
		'If e.ListChangedType = ListChangedType.ItemAdded Then
		'ElseIf e.ListChangedType = ListChangedType.ItemDeleted AndAlso e.OldIndex = -2 Then
		'ElseIf e.ListChangedType = ListChangedType.ItemChanged Then
		If e.ListChangedType = ListChangedType.ItemChanged Then
			If e.PropertyDescriptor IsNot Nothing Then
				If e.PropertyDescriptor.Name = "GamePathFileName" OrElse e.PropertyDescriptor.Name = "GameAppPathFileName" OrElse e.PropertyDescriptor.Name = "CompilerPathFileName" OrElse e.PropertyDescriptor.Name = "ViewerPathFileName" OrElse e.PropertyDescriptor.Name = "MappingToolPathFileName" OrElse e.PropertyDescriptor.Name = "PackerPathFileName" Then
					Me.UpdateUseCounts()
				ElseIf e.PropertyDescriptor.Name = "GameEngine" Then
					Me.UpdateWidgetsBasedOnGameEngine()
				End If
			End If
		End If
	End Sub

	Private Sub ParsePathFileName(ByVal sender As Object, ByVal e As ConvertEventArgs)
		e.Value = Me.ParsePathFileName(CType(e.Value, String))
	End Sub

	Private Function ParsePathFileName(ByVal iPathFileName As String) As String
		Dim originalText As String = iPathFileName
		iPathFileName = TheApp.GetProcessedPathFileName(iPathFileName)
		If iPathFileName <> "" Then
			iPathFileName = FileManager.GetCleanPathFileName(iPathFileName, True)
		End If
		SetPathFileNameField(iPathFileName, originalText)
		Return originalText
	End Function

#End Region

#Region "Private Methods"

	Private Sub UpdateWidgets()
		Dim gameSetupCount As Integer
		gameSetupCount = TheApp.Settings.GameSetups.Count

		Me.GameSetupComboBox.Enabled = (gameSetupCount > 0)

		Me.GamePathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.BrowseForGamePathFileNameButton.Enabled = (gameSetupCount > 0)
		Me.GameAppPathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.GameAppOptionsTextBox.Enabled = (gameSetupCount > 0)
		Me.ClearGameAppOptionsButton.Enabled = (gameSetupCount > 0)
		Me.BrowseForGameAppPathFileNameButton.Enabled = (gameSetupCount > 0)
		Me.CompilerPathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.BrowseForCompilerPathFileNameButton.Enabled = (gameSetupCount > 0)

		Me.ViewerPathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.BrowseForViewerPathFileNameButton.Enabled = (gameSetupCount > 0)

		Me.MappingToolPathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.BrowseForMappingToolPathFileNameButton.Enabled = (gameSetupCount > 0)

		Me.PackerPathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.BrowseForUnpackerPathFileNameButton.Enabled = (gameSetupCount > 0)

		Me.CloneGameSetupButton.Enabled = (gameSetupCount > 0)
		Me.DeleteGameSetupButton.Enabled = (gameSetupCount > 1)

		'NOTE: Reset the bindings, because a new game setup has been chosen.

		Me.theSelectedGameSetup = TheApp.Settings.GameSetups(Me.GameSetupComboBox.SelectedIndex)

		Me.GameNameTextBox.DataBindings.Clear()
		Me.GameNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "GameName", False, DataSourceUpdateMode.OnValidation)

		Me.UpdateGameEngineComboBox()

		Me.GamePathFileNameTextBox.DataBindings.Clear()
		Me.GamePathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "GamePathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)
		RemoveHandler Me.GamePathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		AddHandler Me.GamePathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		'TEST: Was testing these lines for converting bad text found in Settings file. Problem is that Me.theSelectedGameSetup.GamePathFileNameUnprocessed
		'      always raises events when changed and ends up back here to do it again, thus leading to stack overflow.
		'Me.GamePathFileNameTextBox.Text = Me.ParsePathFileName(Me.theSelectedGameSetup.GamePathFileNameUnprocessed)
		'Me.GamePathFileNameTextBox.DataBindings("Text").WriteValue()

		Me.GameAppPathFileNameTextBox.DataBindings.Clear()
		Me.GameAppPathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "GameAppPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)
		RemoveHandler Me.GameAppPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		AddHandler Me.GameAppPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		Me.GameAppOptionsTextBox.DataBindings.Clear()
		Me.GameAppOptionsTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "GameAppOptions", False, DataSourceUpdateMode.OnValidation)

		Me.CompilerPathFileNameTextBox.DataBindings.Clear()
		Me.CompilerPathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "CompilerPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)
		RemoveHandler Me.CompilerPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		AddHandler Me.CompilerPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName

		Me.ViewerPathFileNameTextBox.DataBindings.Clear()
		Me.ViewerPathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "ViewerPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)
		RemoveHandler Me.ViewerPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		AddHandler Me.ViewerPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName

		Me.MappingToolPathFileNameTextBox.DataBindings.Clear()
		Me.MappingToolPathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "MappingToolPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)
		RemoveHandler Me.MappingToolPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		AddHandler Me.MappingToolPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName

		Me.PackerPathFileNameTextBox.DataBindings.Clear()
		Me.PackerPathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "PackerPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)
		RemoveHandler Me.PackerPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		AddHandler Me.PackerPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
	End Sub

	Private Sub UpdateGameEngineComboBox()
		Dim anEnumList As IList

		anEnumList = EnumHelper.ToList(GetType(GameEngine))
		'NOTE: For now, remove the Source 2 value.
		EnumHelper.RemoveFromList(GameEngine.Source2, anEnumList)

		Me.EngineComboBox.DataBindings.Clear()
		Try
			Me.EngineComboBox.DisplayMember = "Value"
			Me.EngineComboBox.ValueMember = "Key"
			Me.EngineComboBox.DataSource = anEnumList
			Me.EngineComboBox.DataBindings.Add("SelectedValue", Me.theSelectedGameSetup, "GameEngine", False, DataSourceUpdateMode.OnPropertyChanged)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub UpdateWidgetsBasedOnGameEngine()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			Me.GamePathLabel.Text = "LibList.gam:"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			Me.GamePathLabel.Text = "GameInfo.txt:"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			Me.GamePathLabel.Text = "GameInfo.gi:"
		End If

		Me.PackerLabel.Visible = Me.theSelectedGameSetup.GameEngine = GameEngine.Source
		Me.PackerPathFileNameTextBox.Visible = Me.theSelectedGameSetup.GameEngine = GameEngine.Source
		Me.BrowseForUnpackerPathFileNameButton.Visible = Me.theSelectedGameSetup.GameEngine = GameEngine.Source
		Me.CreateModelsFolderTreeButton.Visible = Me.theSelectedGameSetup.GameEngine = GameEngine.Source
	End Sub

	Private Sub SetPathFileNameField(ByVal inputText As String, ByRef outputText As String)
		If outputText.Length > 0 AndAlso outputText(0) = "<" Then
			For Each aSteamLibraryPath As SteamLibraryPath In TheApp.Settings.SteamLibraryPaths
				SetMacroInText(aSteamLibraryPath.LibraryPath, aSteamLibraryPath.Macro, inputText)
			Next
		End If
		outputText = inputText
	End Sub

	Private Sub UpdateUseCounts()
		Dim useCount As Integer
		Dim aMacro As String

		For Each aSteamLibraryPath As SteamLibraryPath In TheApp.Settings.SteamLibraryPaths
			aMacro = aSteamLibraryPath.Macro

			useCount = 0
			For Each aGameSetup As GameSetup In TheApp.Settings.GameSetups
				If aGameSetup.GamePathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
				If aGameSetup.GameAppPathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
				If aGameSetup.CompilerPathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
				If aGameSetup.ViewerPathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
				If aGameSetup.MappingToolPathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
				If aGameSetup.PackerPathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
			Next
			If TheApp.Settings.SteamAppPathFileNameUnprocessed.StartsWith(aMacro) Then
				useCount += 1
			End If
			aSteamLibraryPath.UseCount = useCount
		Next
	End Sub

	Private Sub SetMacroInSelectedGameSetup()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		Me.SetMacroInOneGameSetup(aSteamLibraryPath.LibraryPath, aSteamLibraryPath.Macro, Me.theSelectedGameSetup)
	End Sub

	Private Sub SetMacroInAllGameSetups()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		For Each aGameSetup As GameSetup In TheApp.Settings.GameSetups
			Me.SetMacroInOneGameSetup(aSteamLibraryPath.LibraryPath, aSteamLibraryPath.Macro, aGameSetup)
		Next
	End Sub

	Private Sub ClearMacroInSelectedGameSetup()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		Me.SetMacroInOneGameSetup(aSteamLibraryPath.Macro, aSteamLibraryPath.LibraryPath, Me.theSelectedGameSetup)
	End Sub

	Private Sub ClearMacroInAllGameSetups()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		For Each aGameSetup As GameSetup In TheApp.Settings.GameSetups
			Me.SetMacroInOneGameSetup(aSteamLibraryPath.Macro, aSteamLibraryPath.LibraryPath, aGameSetup)
		Next
	End Sub

	Private Sub ChangeToThisMacroInSelectedGameSetup()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		Me.SetMacroInOneGameSetup("<>", aSteamLibraryPath.Macro, Me.theSelectedGameSetup)
	End Sub

	Private Sub ChangeToThisMacroInAllGameSetups()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		For Each aGameSetup As GameSetup In TheApp.Settings.GameSetups
			Me.SetMacroInOneGameSetup("<>", aSteamLibraryPath.Macro, aGameSetup)
		Next
	End Sub

	Private Function GetSelectedSteamLibraryPath() As SteamLibraryPath
		Dim aSteamLibraryPath As SteamLibraryPath
		Dim selectedRowIndex As Integer

		If Me.SteamLibraryPathsDataGridView.SelectedCells.Count > 0 Then
			selectedRowIndex = Me.SteamLibraryPathsDataGridView.SelectedCells(0).RowIndex
		Else
			selectedRowIndex = 0
		End If
		aSteamLibraryPath = TheApp.Settings.SteamLibraryPaths(selectedRowIndex)

		Return aSteamLibraryPath
	End Function

	Private Sub SetMacroInOneGameSetup(ByVal oldText As String, ByVal newText As String, ByVal aGameSetup As GameSetup)
		SetMacroInText(oldText, newText, aGameSetup.GamePathFileNameUnprocessed)
		SetMacroInText(oldText, newText, aGameSetup.GameAppPathFileNameUnprocessed)
		SetMacroInText(oldText, newText, aGameSetup.CompilerPathFileNameUnprocessed)
		SetMacroInText(oldText, newText, aGameSetup.ViewerPathFileNameUnprocessed)
		SetMacroInText(oldText, newText, aGameSetup.MappingToolPathFileNameUnprocessed)
		SetMacroInText(oldText, newText, aGameSetup.PackerPathFileNameUnprocessed)
	End Sub

	Private Sub SetMacroInText(ByVal oldText As String, ByVal newText As String, ByRef fullText As String)
		If oldText = "<>" AndAlso fullText.StartsWith("<") Then
			Dim index As Integer
			index = fullText.IndexOf(">")
			If index >= 1 Then
				Dim nonMacroText As String
				nonMacroText = fullText.Substring(index + 1)
				fullText = newText + nonMacroText
			End If
		ElseIf fullText.StartsWith(oldText) Then
			Dim remainingText As String
			remainingText = fullText.Substring(oldText.Length)
			fullText = newText + remainingText
		End If
	End Sub

#End Region

#Region "Data"

	Private theSelectedGameSetup As GameSetup

#End Region

End Class
