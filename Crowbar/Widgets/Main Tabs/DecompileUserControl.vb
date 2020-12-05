Imports System.IO
Imports System.Text

Public Class DecompileUserControl

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		Me.MdlPathFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, "DecompileMdlPathFileName", False, DataSourceUpdateMode.OnValidation)

		Me.OutputPathTextBox.DataBindings.Add("Text", TheApp.Settings, "DecompileOutputFullPath", False, DataSourceUpdateMode.OnValidation)
		Me.OutputSubfolderTextBox.DataBindings.Add("Text", TheApp.Settings, "DecompileOutputSubfolderName", False, DataSourceUpdateMode.OnValidation)
		Me.InitOutputPathComboBox()
		Me.UpdateOutputPathWidgets()

		Me.InitDecompilerOptions()

		Me.theDecompiledRelativePathFileNames = New BindingListEx(Of String)
		Me.DecompiledFilesComboBox.DataSource = Me.theDecompiledRelativePathFileNames

		Me.UpdateDecompileMode()
		Me.UpdateWidgets(False)

		AddHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		AddHandler Me.OutputPathTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
		AddHandler TheApp.Decompiler.ProgressChanged, AddressOf Me.DecompilerBackgroundWorker_ProgressChanged
		AddHandler TheApp.Decompiler.RunWorkerCompleted, AddressOf Me.DecompilerBackgroundWorker_RunWorkerCompleted
	End Sub

	Private Sub InitDecompilerOptions()
		Me.QcFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileQcFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.GroupIntoQciFilesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileGroupIntoQciFilesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.SkinFamilyOnSingleLineCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileQcSkinFamilyOnSingleLineIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.IncludeDefineBoneLinesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileQcIncludeDefineBoneLinesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.UseMixedCaseForKeywordsCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileQcUseMixedCaseForKeywordsIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.ReferenceMeshSmdFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileReferenceMeshSmdFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.RemovePathFromMaterialFileNamesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileRemovePathFromSmdMaterialFileNamesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.UseNonValveUvConversionCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileUseNonValveUvConversionIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.BoneAnimationSmdFilesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileBoneAnimationSmdFilesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.PlaceInAnimsSubfolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileBoneAnimationPlaceInSubfolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.TextureBmpFilesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileTextureBmpFilesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.LodMeshSmdFilesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileLodMeshSmdFilesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.PhysicsMeshSmdFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompilePhysicsMeshSmdFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.VertexAnimationVtaFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileVertexAnimationVtaFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.ProceduralBonesVrdFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileProceduralBonesVrdFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.FolderForEachModelCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileFolderForEachModelIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.PrefixMeshFileNamesWithModelNameCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompilePrefixFileNamesWithModelNameIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.FormatForStricterImportersCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileStricterFormatIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.LogFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileLogFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.DebugInfoCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileDebugInfoFilesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.DeclareSequenceQciCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileDeclareSequenceQciFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Dim anEnumList As IList
		anEnumList = EnumHelper.ToList(GetType(SupportedMdlVersion))
		Me.OverrideMdlVersionComboBox.DisplayMember = "Value"
		Me.OverrideMdlVersionComboBox.ValueMember = "Key"
		Me.OverrideMdlVersionComboBox.DataSource = anEnumList
		Me.OverrideMdlVersionComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "DecompileOverrideMdlVersion", False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

	Private Sub Free()
		RemoveHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		RemoveHandler Me.OutputPathTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
		RemoveHandler TheApp.Decompiler.ProgressChanged, AddressOf Me.DecompilerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Decompiler.RunWorkerCompleted, AddressOf Me.DecompilerBackgroundWorker_RunWorkerCompleted

		Me.DecompileComboBox.DataBindings.Clear()
		Me.MdlPathFileNameTextBox.DataBindings.Clear()

		Me.OutputPathTextBox.DataBindings.Clear()
		Me.OutputSubfolderTextBox.DataBindings.Clear()

		Me.FreeDecompilerOptions()

		Me.DecompiledFilesComboBox.DataSource = Nothing
	End Sub

	Private Sub FreeDecompilerOptions()
		Me.QcFileCheckBox.DataBindings.Clear()
		Me.GroupIntoQciFilesCheckBox.DataBindings.Clear()
		Me.SkinFamilyOnSingleLineCheckBox.DataBindings.Clear()
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.DataBindings.Clear()
		Me.IncludeDefineBoneLinesCheckBox.DataBindings.Clear()
		Me.ReferenceMeshSmdFileCheckBox.DataBindings.Clear()
		Me.RemovePathFromMaterialFileNamesCheckBox.DataBindings.Clear()
		Me.UseNonValveUvConversionCheckBox.DataBindings.Clear()
		Me.BoneAnimationSmdFilesCheckBox.DataBindings.Clear()
		Me.PlaceInAnimsSubfolderCheckBox.DataBindings.Clear()

		Me.TextureBmpFilesCheckBox.DataBindings.Clear()
		Me.LodMeshSmdFilesCheckBox.DataBindings.Clear()
		Me.PhysicsMeshSmdFileCheckBox.DataBindings.Clear()
		Me.VertexAnimationVtaFileCheckBox.DataBindings.Clear()
		Me.ProceduralBonesVrdFileCheckBox.DataBindings.Clear()

		Me.FolderForEachModelCheckBox.DataBindings.Clear()
		Me.PrefixMeshFileNamesWithModelNameCheckBox.DataBindings.Clear()
		Me.FormatForStricterImportersCheckBox.DataBindings.Clear()

		Me.LogFileCheckBox.DataBindings.Clear()
		Me.DebugInfoCheckBox.DataBindings.Clear()

		Me.DeclareSequenceQciCheckBox.DataBindings.Clear()

		Me.OverrideMdlVersionComboBox.DataBindings.Clear()
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

	Private Sub DecompileUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
		'NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.MdlPathFileNameTextBox, Me.BrowseForMdlPathFolderOrFileNameButton)
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.OutputPathTextBox, Me.BrowseForOutputPathButton)
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.OutputSubfolderTextBox, Me.BrowseForOutputPathButton)

		If Not Me.DesignMode Then
			Me.Init()
		End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	'Private Sub MdlPathFileNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MdlPathFileNameTextBox.Validated
	'	Me.MdlPathFileNameTextBox.Text = FileManager.GetCleanPathFileName(Me.MdlPathFileNameTextBox.Text)
	'End Sub

	Private Sub BrowseForMdlPathFolderOrFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForMdlPathFolderOrFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()

		openFileWdw.Title = "Open the file or folder you want to decompile"
		If File.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
			openFileWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.DecompileMdlPathFileName)
			'ElseIf Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
			'	openFileWdw.InitialDirectory = TheApp.Settings.DecompileMdlPathFileName
		Else
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.DecompileMdlPathFileName)
			If openFileWdw.InitialDirectory = "" Then
				openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		End If
		openFileWdw.FileName = "[Folder Selection]"
		openFileWdw.Filter = "Source Engine MDL Files (*.mdl) | *.mdl"
		openFileWdw.AddExtension = True
		openFileWdw.CheckFileExists = False
		openFileWdw.Multiselect = False
		openFileWdw.ValidateNames = True

		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Try
				If Path.GetFileName(openFileWdw.FileName) = "[Folder Selection].mdl" Then
					TheApp.Settings.DecompileMdlPathFileName = FileManager.GetPath(openFileWdw.FileName)
				Else
					TheApp.Settings.DecompileMdlPathFileName = openFileWdw.FileName
				End If
			Catch ex As IO.PathTooLongException
				MessageBox.Show("The file or folder you tried to select has too many characters in it. Try shortening it by moving the model files somewhere else or by renaming folders or files." + vbCrLf + vbCrLf + "Error message generated by Windows: " + vbCrLf + ex.Message, "The File or Folder You Tried to Select Is Too Long", MessageBoxButtons.OK)
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub GotoMdlButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoMdlButton.Click
		FileManager.OpenWindowsExplorer(TheApp.Settings.DecompileMdlPathFileName)
	End Sub

	'Private Sub OutputSubfolderNameRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	If Me.OutputSubfolderNameRadioButton.Checked Then
	'		TheApp.Settings.DecompileOutputFolderOption = OutputFolderOptions.SubfolderName
	'	Else
	'		TheApp.Settings.DecompileOutputFolderOption = OutputFolderOptions.PathName
	'	End If

	'	Me.UpdateOutputFolderWidgets()
	'End Sub

	'Private Sub UseDefaultOutputSubfolderNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	TheApp.Settings.SetDefaultDecompileOutputSubfolderName()
	'	'Me.OutputSubfolderNameTextBox.DataBindings("Text").ReadValue()
	'End Sub

	'Private Sub OutputPathNameRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	Me.UpdateOutputFolderWidgets()
	'End Sub

	'Private Sub OutputPathNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	'Me.OutputFullPathTextBox.Text = FileManager.GetCleanPathFileName(Me.OutputFullPathTextBox.Text)
	'	Me.UpdateOutputFullPathTextBox()
	'End Sub

	'Private Sub BrowseForOutputPathNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
	'	'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
	'	Dim outputPathWdw As New OpenFileDialog()

	'	outputPathWdw.Title = "Open the folder you want as Output Folder"
	'	If Directory.Exists(TheApp.Settings.DecompileOutputFullPath) Then
	'		outputPathWdw.InitialDirectory = TheApp.Settings.DecompileOutputFullPath
	'	ElseIf File.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
	'		outputPathWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.DecompileMdlPathFileName)
	'	ElseIf Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
	'		outputPathWdw.InitialDirectory = TheApp.Settings.DecompileMdlPathFileName
	'	Else
	'		outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
	'	End If
	'	outputPathWdw.FileName = "[Folder Selection]"
	'	outputPathWdw.AddExtension = False
	'	outputPathWdw.CheckFileExists = False
	'	outputPathWdw.Multiselect = False
	'	outputPathWdw.ValidateNames = False

	'	If outputPathWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
	'		' Allow dialog window to completely disappear.
	'		Application.DoEvents()

	'		TheApp.Settings.DecompileOutputFullPath = FileManager.GetPath(outputPathWdw.FileName)
	'	End If
	'End Sub

	'Private Sub GotoOutputButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	FileManager.OpenWindowsExplorer(Me.OutputFullPathTextBox.Text)
	'End Sub

	Private Sub OutputPathTextBox_DragDrop(sender As Object, e As DragEventArgs) Handles OutputPathTextBox.DragDrop
		Dim pathFileNames() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
		Dim pathFileName As String = pathFileNames(0)
		If Directory.Exists(pathFileName) Then
			TheApp.Settings.DecompileOutputFullPath = pathFileName
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
		TheApp.Settings.SetDefaultDecompileOutputSubfolderName()
	End Sub

	Private Sub DecompileOptionsUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles DecompileOptionsUseDefaultsButton.Click
		TheApp.Settings.SetDefaultDecompileReCreateFilesOptions()
	End Sub

	Private Sub DecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecompileButton.Click
		Me.RunDecompiler()
	End Sub

	Private Sub SkipCurrentModelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SkipCurrentModelButton.Click
		TheApp.Decompiler.SkipCurrentModel()
	End Sub

	Private Sub CancelDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelDecompileButton.Click
		TheApp.Decompiler.CancelAsync()
	End Sub

	Private Sub UseAllInCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseAllInCompileButton.Click
		TheApp.Settings.CompileQcPathFileName = TheApp.Decompiler.GetOutputPathFolderOrFileName()
		TheApp.Settings.CompileMode = InputOptions.FolderRecursion
	End Sub

	Private Sub UseInEditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInEditButton.Click
		'TODO: Use the selected decompiled file as Edit tab's input file.
	End Sub

	Private Sub UseInCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInCompileButton.Click
		TheApp.Settings.CompileQcPathFileName = TheApp.Decompiler.GetOutputPathFileName(Me.theDecompiledRelativePathFileNames(Me.DecompiledFilesComboBox.SelectedIndex))
		TheApp.Settings.CompileMode = InputOptions.File
	End Sub

	Private Sub GotoDecompiledFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoDecompiledFileButton.Click
		Dim pathFileName As String
		pathFileName = TheApp.Decompiler.GetOutputPathFileName(Me.theDecompiledRelativePathFileNames(Me.DecompiledFilesComboBox.SelectedIndex))
		FileManager.OpenWindowsExplorer(pathFileName)
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "DecompileMdlPathFileName" Then
			Me.UpdateDecompileMode()
		ElseIf e.PropertyName = "DecompileOutputFolderOption" Then
			Me.UpdateOutputPathWidgets()
		ElseIf e.PropertyName.StartsWith("Decompile") AndAlso e.PropertyName.EndsWith("IsChecked") Then
			Me.UpdateWidgets(TheApp.Settings.DecompilerIsRunning)
		End If
	End Sub

	Private Sub DecompilerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.DecompilerLogTextBox.Text = ""
			Me.DecompilerLogTextBox.AppendText(line + vbCr)
			Me.UpdateWidgets(True)
		ElseIf e.ProgressPercentage = 1 Then
			Me.DecompilerLogTextBox.AppendText(line + vbCr)
		ElseIf e.ProgressPercentage = 100 Then
			Me.DecompilerLogTextBox.AppendText(line + vbCr)
		End If
	End Sub

	Private Sub DecompilerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If Not e.Cancelled Then
			Dim decompileResultInfo As DecompilerOutputInfo
			decompileResultInfo = CType(e.Result, DecompilerOutputInfo)
			Me.UpdateDecompiledRelativePathFileNames(decompileResultInfo.theDecompiledRelativePathFileNames)
		End If

		Me.UpdateWidgets(False)
	End Sub

#End Region

#Region "Private Methods"

	'Private Sub UpdateOutputFolderWidgets()
	'	Me.OutputSubfolderNameTextBox.ReadOnly = Not Me.OutputSubfolderNameRadioButton.Checked
	'	Me.OutputFullPathTextBox.ReadOnly = Me.OutputSubfolderNameRadioButton.Checked
	'	Me.BrowseForOutputPathNameButton.Enabled = Not Me.OutputSubfolderNameRadioButton.Checked
	'End Sub

	'Private Sub UpdateOutputFullPathTextBox()
	'	If String.IsNullOrEmpty(Me.OutputFullPathTextBox.Text) Then
	'		Try
	'			TheApp.Settings.DecompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
	'		Catch ex As Exception
	'			Dim debug As Integer = 4242
	'		End Try
	'	End If
	'End Sub

	Private Sub InitOutputPathComboBox()
		Dim anEnumList As IList

		anEnumList = EnumHelper.ToList(GetType(DecompileOutputPathOptions))
		Me.OutputPathComboBox.DataBindings.Clear()
		Try
			Me.OutputPathComboBox.DisplayMember = "Value"
			Me.OutputPathComboBox.ValueMember = "Key"
			Me.OutputPathComboBox.DataSource = anEnumList
			Me.OutputPathComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "DecompileOutputFolderOption", False, DataSourceUpdateMode.OnPropertyChanged)

			' Do not use this line because it will override the value automatically assigned by the data bindings above.
			'Me.OutputPathComboBox.SelectedIndex = 0
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub UpdateOutputPathWidgets()
		Me.OutputPathTextBox.Visible = (TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.WorkFolder)
		Me.OutputSubfolderTextBox.Visible = (TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.Subfolder)
		Me.BrowseForOutputPathButton.Enabled = (TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.WorkFolder)
		Me.BrowseForOutputPathButton.Visible = (TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.WorkFolder)
		Me.GotoOutputPathButton.Enabled = (TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.WorkFolder)
		Me.GotoOutputPathButton.Visible = (TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.WorkFolder)
		Me.UseDefaultOutputSubfolderButton.Enabled = (TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.Subfolder)
		Me.UseDefaultOutputSubfolderButton.Visible = (TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.Subfolder)
	End Sub

	Private Sub UpdateOutputPathTextBox()
		If TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.WorkFolder Then
			If String.IsNullOrEmpty(Me.OutputPathTextBox.Text) Then
				Try
					TheApp.Settings.DecompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
			End If
		End If
	End Sub

	Private Sub BrowseForOutputPath()
		If TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.WorkFolder Then
			'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
			'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
			Dim outputPathWdw As New OpenFileDialog()

			outputPathWdw.Title = "Open the folder you want as Output Folder"
			'If Directory.Exists(TheApp.Settings.DecompileOutputFullPath) Then
			'	outputPathWdw.InitialDirectory = TheApp.Settings.DecompileOutputFullPath
			'Else
			outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.DecompileOutputFullPath)
			If outputPathWdw.InitialDirectory = "" Then
				If File.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
					outputPathWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.DecompileMdlPathFileName)
				ElseIf Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
					outputPathWdw.InitialDirectory = TheApp.Settings.DecompileMdlPathFileName
				Else
					outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
				End If
			End If
			'End If
			outputPathWdw.FileName = "[Folder Selection]"
			outputPathWdw.AddExtension = False
			outputPathWdw.CheckFileExists = False
			outputPathWdw.Multiselect = False
			outputPathWdw.ValidateNames = False

			If outputPathWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
				' Allow dialog window to completely disappear.
				Application.DoEvents()

				TheApp.Settings.DecompileOutputFullPath = FileManager.GetPath(outputPathWdw.FileName)
			End If
		End If
	End Sub

	Private Sub GotoFolder()
		If TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.WorkFolder Then
			'FileManager.OpenWindowsExplorer(Me.OutputPathTextBox.Text)
			FileManager.OpenWindowsExplorer(TheApp.Settings.DecompileOutputFullPath)
		End If
	End Sub

	Private Sub UpdateWidgets(ByVal decompilerIsRunning As Boolean)
		TheApp.Settings.DecompilerIsRunning = decompilerIsRunning

		Me.DecompileComboBox.Enabled = Not decompilerIsRunning
		Me.MdlPathFileNameTextBox.Enabled = Not decompilerIsRunning
		Me.BrowseForMdlPathFolderOrFileNameButton.Enabled = Not decompilerIsRunning

		'Me.OutputSubfolderNameRadioButton.Enabled = Not decompilerIsRunning
		'Me.OutputSubfolderNameTextBox.Enabled = Not decompilerIsRunning
		'Me.UseDefaultOutputSubfolderNameButton.Enabled = Not decompilerIsRunning
		'Me.OutputFullPathRadioButton.Enabled = Not decompilerIsRunning
		'Me.OutputFullPathTextBox.Enabled = Not decompilerIsRunning
		'Me.BrowseForOutputPathNameButton.Enabled = Not decompilerIsRunning
		Me.OutputPathComboBox.Enabled = Not decompilerIsRunning
		Me.OutputPathTextBox.Enabled = Not decompilerIsRunning
		Me.OutputSubfolderTextBox.Enabled = Not decompilerIsRunning
		Me.BrowseForOutputPathButton.Enabled = Not decompilerIsRunning
		Me.GotoOutputPathButton.Enabled = Not decompilerIsRunning
		Me.UseDefaultOutputSubfolderButton.Enabled = Not decompilerIsRunning

		Me.ReCreateFilesGroupBox.Enabled = Not decompilerIsRunning
		Me.GroupIntoQciFilesCheckBox.Enabled = TheApp.Settings.DecompileQcFileIsChecked
		Me.SkinFamilyOnSingleLineCheckBox.Enabled = TheApp.Settings.DecompileQcFileIsChecked
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Enabled = TheApp.Settings.DecompileQcFileIsChecked
		Me.IncludeDefineBoneLinesCheckBox.Enabled = TheApp.Settings.DecompileQcFileIsChecked
		Me.UseMixedCaseForKeywordsCheckBox.Enabled = TheApp.Settings.DecompileQcFileIsChecked

		Me.RemovePathFromMaterialFileNamesCheckBox.Enabled = TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked
		Me.UseNonValveUvConversionCheckBox.Enabled = TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked

		Me.PlaceInAnimsSubfolderCheckBox.Enabled = TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked

		Me.OptionsGroupBox.Enabled = Not decompilerIsRunning

		Me.DecompileButton.Enabled = Not decompilerIsRunning _
		 AndAlso (TheApp.Settings.DecompileQcFileIsChecked _
		 OrElse TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked _
		 OrElse TheApp.Settings.DecompileLodMeshSmdFilesIsChecked _
		 OrElse TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked _
		 OrElse TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked _
		 OrElse TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked _
		 OrElse TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked _
		 OrElse TheApp.Settings.DecompileTextureBmpFilesIsChecked _
		 OrElse TheApp.Settings.DecompileLogFileIsChecked _
		 OrElse TheApp.Settings.DecompileDebugInfoFilesIsChecked)
		Me.SkipCurrentModelButton.Enabled = decompilerIsRunning
		Me.CancelDecompileButton.Enabled = decompilerIsRunning
		Me.UseAllInCompileButton.Enabled = Not decompilerIsRunning AndAlso Me.theDecompiledRelativePathFileNames.Count > 0

		Me.DecompiledFilesComboBox.Enabled = Not decompilerIsRunning AndAlso Me.theDecompiledRelativePathFileNames.Count > 0
		Me.UseInEditButton.Enabled = Not decompilerIsRunning AndAlso Me.theDecompiledRelativePathFileNames.Count > 0
		Me.UseInCompileButton.Enabled = Not decompilerIsRunning AndAlso Me.theDecompiledRelativePathFileNames.Count > 0 AndAlso (Path.GetExtension(Me.theDecompiledRelativePathFileNames(Me.DecompiledFilesComboBox.SelectedIndex)) = ".qc")
		Me.GotoDecompiledFileButton.Enabled = Not decompilerIsRunning AndAlso Me.theDecompiledRelativePathFileNames.Count > 0
	End Sub

	Private Sub UpdateDecompiledRelativePathFileNames(ByVal iDecompiledRelativePathFileNames As BindingListEx(Of String))
		'Me.theDecompiledRelativePathFileNames.Clear()
		'For Each pathFileName As String In iDecompiledRelativePathFileNames
		'	Me.theDecompiledRelativePathFileNames.Add(pathFileName)
		'Next
		If iDecompiledRelativePathFileNames IsNot Nothing Then
			Me.theDecompiledRelativePathFileNames = iDecompiledRelativePathFileNames
			'NOTE: Do not sort because the list is already sorted by file and then by folder.
			'Me.theDecompiledRelativePathFileNames.Sort()
			'NOTE: Need to set to nothing first to force it to update.
			Me.DecompiledFilesComboBox.DataSource = Nothing
			Me.DecompiledFilesComboBox.DataSource = Me.theDecompiledRelativePathFileNames
		End If
	End Sub

	Private Sub UpdateDecompileMode()
		Dim anEnumList As IList
		Dim previousSelectedInputOption As InputOptions

		anEnumList = EnumHelper.ToList(GetType(InputOptions))
		previousSelectedInputOption = TheApp.Settings.DecompileMode
		Me.DecompileComboBox.DataBindings.Clear()
		Try
			If File.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
				' Set file mode when a file is selected.
				previousSelectedInputOption = InputOptions.File
			ElseIf Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
				'NOTE: Remove in reverse index order.
				If Directory.GetFiles(TheApp.Settings.DecompileMdlPathFileName, "*.mdl").Length = 0 Then
					anEnumList.RemoveAt(InputOptions.Folder)
				End If
				anEnumList.RemoveAt(InputOptions.File)
				'Else
				'	Exit Try
			End If

			Me.DecompileComboBox.DisplayMember = "Value"
			Me.DecompileComboBox.ValueMember = "Key"
			Me.DecompileComboBox.DataSource = anEnumList
			Me.DecompileComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "DecompileMode", False, DataSourceUpdateMode.OnPropertyChanged)

			If EnumHelper.Contains(previousSelectedInputOption, anEnumList) Then
				TheApp.Settings.DecompileMode = previousSelectedInputOption
			Else
				TheApp.Settings.DecompileMode = CType(EnumHelper.Key(0, anEnumList), InputOptions)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub RunDecompiler()
		TheApp.Decompiler.Run()
	End Sub

#End Region

#Region "Data"

	Private theDecompiledRelativePathFileNames As BindingListEx(Of String)

#End Region

End Class
