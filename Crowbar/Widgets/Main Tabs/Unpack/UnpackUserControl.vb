Imports System.IO

Public Class UnpackUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

		Me.UnpackModeComboBox.DropDownWidth = 300
		Me.theUnpackModeIndexIsBeingChangedByMe = False
		Me.theUnpackPackagePathFolderOrFileNameIsBeingChangedByMe = False
	End Sub

#End Region

#Region "Init and Free"

	Protected Overrides Sub Init()
		Me.PackagePathFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackPackagePathFolderOrFileName", False, DataSourceUpdateMode.OnValidation)

		Me.OutputPathTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackOutputFullPath", False, DataSourceUpdateMode.OnValidation)
		Me.OutputSamePathTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackOutputSamePath", False, DataSourceUpdateMode.OnValidation)
		Me.OutputSubfolderTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackOutputSubfolderName", False, DataSourceUpdateMode.OnValidation)
		Me.UpdateOutputPathComboBox()
		Me.UpdateOutputPathWidgets()

		Me.InitUnpackerOptions()

		Me.theOutputPathOrOutputFileName = ""
		Me.theUnpackedRelativePathFileNames = New BindingListEx(Of String)
		Me.UnpackedFilesComboBox.DataSource = Me.theUnpackedRelativePathFileNames

		Me.UpdateUnpackMode()
		Me.UpdateWidgetsBasedOnUnpackerRunning(False)

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged

		AddHandler Me.PackagePathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		AddHandler Me.OutputPathTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
	End Sub

	Private Sub InitUnpackerOptions()
		Me.FolderForEachPackageCheckBox.DataBindings.Add("Checked", TheApp.Settings, "UnpackFolderForEachPackageIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.KeepFullPathCheckBox.DataBindings.Add("Checked", TheApp.Settings, "UnpackKeepFullPathIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.LogFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "UnpackLogFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

	' Do not need Free() because this widget is destroyed only on program exit.
	'Protected Overrides Sub Free()
	'	RemoveHandler Me.PackagePathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
	'	RemoveHandler Me.OutputPathTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
	'	RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
	'	RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.ListerBackgroundWorker_ProgressChanged
	'	RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.ListerBackgroundWorker_RunWorkerCompleted
	'	RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.UnpackerBackgroundWorker_ProgressChanged
	'	RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.UnpackerBackgroundWorker_RunWorkerCompleted

	'	Me.UnpackComboBox.DataBindings.Clear()
	'	Me.PackagePathFileNameTextBox.DataBindings.Clear()

	'	Me.OutputPathTextBox.DataBindings.Clear()
	'	Me.OutputSamePathTextBox.DataBindings.Clear()
	'	Me.OutputSubfolderTextBox.DataBindings.Clear()

	'	Me.FreeUnpackerOptions()

	'	Me.UnpackedFilesComboBox.DataSource = Nothing
	'End Sub

	'Private Sub FreeUnpackerOptions()
	'	Me.FolderForEachPackageCheckBox.DataBindings.Clear()
	'	Me.KeepFullPathCheckBox.DataBindings.Clear()
	'	Me.LogFileCheckBox.DataBindings.Clear()
	'End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

	Public Sub ListPackageContents()
		If TheApp.Settings.UnpackerIsRunning Then
			Exit Sub
		End If

		Me.RefreshPackagesButton.Image = My.Resources.CancelRefresh
		Me.RefreshPackagesButton.Tag = "Cancel"

		Me.PackageContentsUserControl1.Clear()

		Me.SkipCurrentPackageButton.Enabled = False
		Me.CancelUnpackButton.Enabled = False
		Me.UnpackerLogTextBox.Text = ""

		Me.UpdateWidgetsBasedOnUnpackerRunning(True)

		Me.theUnpackedRelativePathFileNames.Clear()

		TheApp.Unpacker.ListContents(AddressOf Me.ListerBackgroundWorker_ProgressChanged, AddressOf Me.ListerBackgroundWorker_RunWorkerCompleted)
	End Sub

#End Region

#Region "Widget Event Handlers"

	Private Sub UnpackUserControl_Resize(sender As Object, e As EventArgs) Handles Me.Resize
		'NOTE: This code prevents Visual Studio often inexplicably extending the right side of these textboxes.
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.PackagePathFileNameTextBox, Me.RefreshPackagesButton, False)
		Me.OutputPathTextBox.Size = New System.Drawing.Size(Me.BrowseForOutputPathButton.Left - Me.BrowseForOutputPathButton.Margin.Left - Me.OutputPathTextBox.Margin.Right - Me.OutputPathTextBox.Left, 21)
		Me.OutputSamePathTextBox.Size = New System.Drawing.Size(Me.BrowseForOutputPathButton.Left - Me.BrowseForOutputPathButton.Margin.Left - Me.OutputSamePathTextBox.Margin.Right - Me.OutputSamePathTextBox.Left, 21)
		Me.OutputSubfolderTextBox.Size = New System.Drawing.Size(Me.BrowseForOutputPathButton.Left - Me.BrowseForOutputPathButton.Margin.Left - Me.OutputSubfolderTextBox.Margin.Right - Me.OutputSubfolderTextBox.Left, 21)
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub UnpackModeComboBox_TextChanged(sender As Object, e As EventArgs) Handles UnpackModeComboBox.TextChanged
		Me.ToolTip1.SetToolTip(Me.UnpackModeComboBox, UnpackModeComboBox.Text)
	End Sub

	Private Sub BrowseForPackagePathFolderOrFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForPackagePathFolderOrFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()

		openFileWdw.Title = "Open the file or folder you want to unpack"
		If File.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
			openFileWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.UnpackPackagePathFolderOrFileName)
		Else
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.UnpackPackagePathFolderOrFileName)
			If openFileWdw.InitialDirectory = "" Then
				openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		End If
		openFileWdw.FileName = "[Folder Selection]"
		openFileWdw.Filter = "Source Engine Package Files (*.apk;*.fpx;*.gma;*.vpk)|*.apk;*.fpx;*.gma;*.vpk|Fairy Tale Busters APK Files (*.apk)|*.apk|Tactical Intervention FPX Files (*.fpx)|*.fpx|Garry's Mod GMA Files (*.gma)|*.gma|Source Engine VPK Files (*.vpk)|*.vpk"
		'openFileWdw.Filter = "Source Engine Package Files (*.hfs)|*.hfs|Vindictus HFS Files (*.hfs)|*.hfs"
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

	Private Sub RefreshPackagesButton_Click(sender As Object, e As EventArgs) Handles RefreshPackagesButton.Click
		If Me.RefreshPackagesButton.Tag.ToString() = "Refresh" Then
			Me.ListPackageContents()
		Else
			TheApp.Unpacker.CancelUnpack()
		End If
	End Sub

	Private Sub UnpackOptionsUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles UnpackOptionsUseDefaultsButton.Click
		TheApp.Settings.SetDefaultUnpackOptions()
	End Sub

	Private Sub UnpackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnpackButton.Click
		Me.Unpack()
	End Sub

	Private Sub SkipCurrentPackageButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SkipCurrentPackageButton.Click
		TheApp.Unpacker.SkipCurrentPackage()
	End Sub

	Private Sub CancelUnpackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelUnpackButton.Click
		TheApp.Unpacker.CancelUnpack()
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
			Me.ListPackageContents()
		ElseIf e.PropertyName = "UnpackModeIndex" AndAlso Not Me.theUnpackModeIndexIsBeingChangedByMe Then
			Me.UpdateFromModeIndexChange()
		ElseIf e.PropertyName = "UnpackOutputFolderOption" Then
			Me.UpdateOutputPathWidgets()
		ElseIf e.PropertyName = "UnpackGameSetupSelectedIndex" Then
			Me.UpdateGameModelsOutputPathTextBox()
		ElseIf e.PropertyName.StartsWith("Unpack") AndAlso e.PropertyName.EndsWith("IsChecked") Then
			Me.UpdateWidgetsBasedOnUnpackerRunning(TheApp.Settings.UnpackerIsRunning)
		End If
	End Sub

	Private Sub ListerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 1 Then
			Me.PackageContentsUserControl1.IncrementPackageCount()
		ElseIf e.ProgressPercentage = 50 Then
			Me.UnpackerLogTextBox.Text = ""
			Me.UnpackerLogTextBox.AppendText(CStr(e.UserState) + vbCr)
			'NOTE: Set the textbox to show first line of text.
			Me.UnpackerLogTextBox.Select(0, 0)
		ElseIf e.ProgressPercentage = 51 Then
			Me.UnpackerLogTextBox.AppendText(CStr(e.UserState) + vbCr)
			'NOTE: Set the textbox to show first line of text.
			Me.UnpackerLogTextBox.Select(0, 0)
		End If
	End Sub

	Private Sub ListerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim entries As New List(Of SourcePackageDirectoryEntry)()
		If Not e.Cancelled Then
			Dim unpackResultInfo As UnpackerOutputInfo
			unpackResultInfo = CType(e.Result, UnpackerOutputInfo)

			' Much faster to read all entries and transfer entire list at end of background process rather than transferring each entry.
			entries = unpackResultInfo.entries
		End If
		Me.PackageContentsUserControl1.Entries = entries

		Me.RefreshPackagesButton.Image = My.Resources.Refresh
		Me.RefreshPackagesButton.Tag = "Refresh"
		Me.UpdateWidgetsBasedOnUnpackerRunning(False)
	End Sub

	Private Sub UnpackerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			'TODO: Having the updating of disabled widgets here is unusual, so why not move this to before calling the backgroundworker?
			'      One advantage to doing before call: Indicates to user that action has started even when opening file takes a while.
			Me.UnpackerLogTextBox.Text = ""
			Me.UnpackerLogTextBox.AppendText(line + vbCr)
			Me.theOutputPathOrOutputFileName = ""
			Me.UpdateWidgetsBasedOnUnpackerRunning(True)
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

		Me.UpdateWidgetsBasedOnUnpackerRunning(False)
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
		Me.ListPackageContents()
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

			Me.UnpackModeComboBox.DisplayMember = "Value"
			Me.UnpackModeComboBox.ValueMember = "Key"
			Me.UnpackModeComboBox.DataSource = TheApp.Unpacker.UnpackModes
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

			Me.OutputPathComboBox.DisplayMember = "Value"
			Me.OutputPathComboBox.ValueMember = "Key"
			Me.OutputPathComboBox.DataSource = anEnumList
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

	Private Sub UpdateWidgetsBasedOnUnpackerRunning(ByVal unpackerIsRunning As Boolean)
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

		Me.PackageContentsUserControl1.Enabled = Not unpackerIsRunning
		Me.OptionsGroupBox.Enabled = Not unpackerIsRunning

		Dim selectedEntries As List(Of SourcePackageDirectoryEntry) = Me.PackageContentsUserControl1.SelectedEntries
		Me.UnpackButton.Enabled = (Not unpackerIsRunning) AndAlso (selectedEntries IsNot Nothing) AndAlso (selectedEntries.Count > 0)
		Me.SkipCurrentPackageButton.Enabled = unpackerIsRunning
		Me.CancelUnpackButton.Enabled = unpackerIsRunning
		Me.UseAllInDecompileButton.Enabled = Not unpackerIsRunning AndAlso Me.theOutputPathOrOutputFileName <> "" AndAlso Me.theUnpackedRelativePathFileNames.Count > 0

		Me.UnpackedFilesComboBox.Enabled = Not unpackerIsRunning AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
		Me.UseInPreviewButton.Enabled = Not unpackerIsRunning AndAlso Me.theOutputPathOrOutputFileName <> "" AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
		Me.UseInDecompileButton.Enabled = Not unpackerIsRunning AndAlso Me.theOutputPathOrOutputFileName <> "" AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
		Me.GotoUnpackedFileButton.Enabled = Not unpackerIsRunning AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
	End Sub

	Private Sub Unpack()
		'NOTE: [21-Dec-2020] Must unbind this combobox to prevent slowdown on second and subsequent unpacks.
		Me.UnpackedFilesComboBox.DataSource = Nothing

		Dim packagePathFileNameToEntriesMap As New SortedList(Of String, List(Of SourcePackageDirectoryEntry))
		Dim selectedRelativeOutputPath As String = ""
		Me.PackageContentsUserControl1.GetSelectedEntriesAndOutputPath(packagePathFileNameToEntriesMap, selectedRelativeOutputPath)

		TheApp.Unpacker.Unpack(AddressOf Me.UnpackerBackgroundWorker_ProgressChanged, AddressOf Me.UnpackerBackgroundWorker_RunWorkerCompleted, packagePathFileNameToEntriesMap, selectedRelativeOutputPath)
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

#End Region

#Region "Data"

	Private theUnpackModeIndexIsBeingChangedByMe As Boolean
	Private theUnpackPackagePathFolderOrFileNameIsBeingChangedByMe As Boolean

	Private theUnpackedRelativePathFileNames As BindingListEx(Of String)
	Private theOutputPathOrOutputFileName As String

#End Region

End Class
