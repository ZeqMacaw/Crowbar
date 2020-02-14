Imports System.IO
Imports System.Text

Public Class CompileUserControl

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

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

	Private Sub Init()
		Me.QcPathFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, "CompileQcPathFileName", False, DataSourceUpdateMode.OnValidation)

		Me.OutputPathTextBox.DataBindings.Add("Text", TheApp.Settings, "CompileOutputFullPath", False, DataSourceUpdateMode.OnValidation)
		Me.OutputSubfolderTextBox.DataBindings.Add("Text", TheApp.Settings, "CompileOutputSubfolderName", False, DataSourceUpdateMode.OnValidation)
		Me.InitOutputPathComboBox()
		Me.UpdateOutputPathWidgets()

		'NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
		Me.GameSetupComboBox.DisplayMember = "GameName"
		Me.GameSetupComboBox.ValueMember = "GameName"
		Me.GameSetupComboBox.DataSource = TheApp.Settings.GameSetups
		Me.GameSetupComboBox.DataBindings.Add("SelectedIndex", TheApp.Settings, "CompileGameSetupSelectedIndex", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.InitCrowbarOptions()
		Me.InitCompilerOptions()

		Me.theCompiledRelativePathFileNames = New BindingListEx(Of String)
		Me.CompiledFilesComboBox.DataSource = Me.theCompiledRelativePathFileNames

		Me.UpdateCompileMode()
		Me.UpdateWidgets(False)
		Me.UpdateCompilerOptions()

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
		AddHandler TheApp.Compiler.ProgressChanged, AddressOf Me.CompilerBackgroundWorker_ProgressChanged
		AddHandler TheApp.Compiler.RunWorkerCompleted, AddressOf Me.CompilerBackgroundWorker_RunWorkerCompleted

		AddHandler Me.QcPathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		AddHandler Me.OutputPathTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
	End Sub

	Private Sub InitOutputPathComboBox()
		Dim anEnumList As IList

		anEnumList = EnumHelper.ToList(GetType(CompileOutputPathOptions))
		Me.OutputPathComboBox.DataBindings.Clear()
		Try
			Me.OutputPathComboBox.DisplayMember = "Value"
			Me.OutputPathComboBox.ValueMember = "Key"
			Me.OutputPathComboBox.DataSource = anEnumList
			Me.OutputPathComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "CompileOutputFolderOption", False, DataSourceUpdateMode.OnPropertyChanged)

			Me.OutputPathComboBox.SelectedIndex = 0
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub InitCrowbarOptions()
		Me.GoldSourceEngineLogFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "CompileGoldSourceLogFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.SourceEngineLogFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "CompileSourceLogFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

	Private Sub InitCompilerOptions()
		Me.theSelectedCompilerOptions = New List(Of String)()

		' GoldSource

		' Source

		Me.CompilerOptionDefineBonesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "CompileOptionDefineBonesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.CompilerOptionDefineBonesCreateFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "CompileOptionDefineBonesCreateFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.CompilerOptionDefineBonesFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, "CompileOptionDefineBonesQciFileName", False, DataSourceUpdateMode.OnValidation)
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "CompileOptionDefineBonesModifyQcFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.CompilerOptionNoP4CheckBox.DataBindings.Add("Checked", TheApp.Settings, "CompileOptionNoP4IsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.CompilerOptionVerboseCheckBox.DataBindings.Add("Checked", TheApp.Settings, "CompileOptionVerboseIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

	Private Sub Free()
		RemoveHandler Me.QcPathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		RemoveHandler Me.OutputPathTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
		RemoveHandler TheApp.Compiler.ProgressChanged, AddressOf Me.CompilerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Compiler.RunWorkerCompleted, AddressOf Me.CompilerBackgroundWorker_RunWorkerCompleted

		Me.QcPathFileNameTextBox.DataBindings.Clear()

		Me.OutputPathTextBox.DataBindings.Clear()
		Me.OutputSubfolderTextBox.DataBindings.Clear()

		Me.GameSetupComboBox.DataSource = Nothing
		Me.GameSetupComboBox.DataBindings.Clear()

		Me.FreeCrowbarOptions()
		Me.FreeCompilerOptions()

		Me.CompileComboBox.DataBindings.Clear()

		Me.CompiledFilesComboBox.DataBindings.Clear()
	End Sub

	Private Sub FreeCrowbarOptions()
		Me.GoldSourceEngineLogFileCheckBox.DataBindings.Clear()
		Me.SourceEngineLogFileCheckBox.DataBindings.Clear()
	End Sub

	Private Sub FreeCompilerOptions()
		' GoldSource

		' Source
		'RemoveHandler Me.CompilerOptionDefineBonesCheckBox.CheckedChanged, AddressOf Me.CompilerOptionDefineBonesCheckBox_CheckedChanged
		'RemoveHandler Me.CompilerOptionNoP4CheckBox.CheckedChanged, AddressOf Me.CompilerOptionNoP4CheckBox_CheckedChanged
		'RemoveHandler Me.CompilerOptionVerboseCheckBox.CheckedChanged, AddressOf Me.CompilerOptionVerboseCheckBox_CheckedChanged

		Me.CompilerOptionDefineBonesCheckBox.DataBindings.Clear()
		Me.CompilerOptionDefineBonesFileNameTextBox.DataBindings.Clear()
		Me.CompilerOptionNoP4CheckBox.DataBindings.Clear()
		Me.CompilerOptionVerboseCheckBox.DataBindings.Clear()
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

	Private Sub CompileUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
		'NOTE: This code prevents Visual Studio often inexplicably extending the right side of these textboxes.
		Me.QcPathFileNameTextBox.Size = New System.Drawing.Size(Me.BrowseForQcPathFolderOrFileNameButton.Left - Me.BrowseForQcPathFolderOrFileNameButton.Margin.Left - Me.QcPathFileNameTextBox.Margin.Right - Me.QcPathFileNameTextBox.Left, 21)
		Me.OutputPathTextBox.Size = New System.Drawing.Size(Me.BrowseForOutputPathButton.Left - Me.BrowseForOutputPathButton.Margin.Left - Me.OutputPathTextBox.Margin.Right - Me.OutputPathTextBox.Left, 21)
		Me.OutputSubfolderTextBox.Size = New System.Drawing.Size(Me.BrowseForOutputPathButton.Left - Me.BrowseForOutputPathButton.Margin.Left - Me.OutputSubfolderTextBox.Margin.Right - Me.OutputSubfolderTextBox.Left, 21)
		Me.GameModelsOutputPathTextBox.Size = New System.Drawing.Size(Me.BrowseForOutputPathButton.Left - Me.BrowseForOutputPathButton.Margin.Left - Me.GameModelsOutputPathTextBox.Margin.Right - Me.GameModelsOutputPathTextBox.Left, 21)
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	'Private Sub QcPathFileNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	'Me.QcPathFileNameTextBox.Text = FileManager.GetCleanPathFileName(Me.QcPathFileNameTextBox.Text)
	'	Me.SetCompilerOptionsText()
	'End Sub

	Private Sub BrowseForQcPathFolderOrFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForQcPathFolderOrFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()

		openFileWdw.Title = "Open the file or folder you want to compile"
		If File.Exists(TheApp.Settings.CompileQcPathFileName) Then
			openFileWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.CompileQcPathFileName)
			'ElseIf Directory.Exists(TheApp.Settings.CompileQcPathFileName) Then
			'	openFileWdw.InitialDirectory = TheApp.Settings.CompileQcPathFileName
		Else
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.CompileQcPathFileName)
			If openFileWdw.InitialDirectory = "" Then
				openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		End If
		openFileWdw.FileName = "[Folder Selection]"
		'openFileWdw.Filter = "Source Engine QC Files (*.qc)|*.qc|All Files (*.*)|*.*"
		openFileWdw.Filter = "Source Engine QC Files (*.qc)|*.qc"
		openFileWdw.AddExtension = True
		openFileWdw.CheckFileExists = False
		openFileWdw.Multiselect = False
		'openFileWdw.Multiselect = True
		openFileWdw.ValidateNames = True

		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			If Path.GetFileName(openFileWdw.FileName) = "[Folder Selection].qc" Then
				TheApp.Settings.CompileQcPathFileName = FileManager.GetPath(openFileWdw.FileName)
			Else
				TheApp.Settings.CompileQcPathFileName = openFileWdw.FileName
			End If

			'Me.SetCompilerOptionsText()
		End If
	End Sub

	Private Sub GotoQcButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoQcButton.Click
		FileManager.OpenWindowsExplorer(TheApp.Settings.CompileQcPathFileName)
	End Sub

	'Private Sub OutputFolderCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles OutputFolderCheckBox.CheckedChanged
	'	Me.UpdateOutputFolderWidgets()
	'End Sub

	'Private Sub OutputSubfolderNameRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputSubfolderNameRadioButton.CheckedChanged
	'       If Me.OutputSubfolderNameRadioButton.Checked Then
	'           TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.SubfolderName
	'       Else
	'           TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.PathName
	'       End If

	'       Me.UpdateOutputFolderWidgets()
	'End Sub

	'Private Sub OutputFolderPathNameRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputFullPathRadioButton.CheckedChanged
	'	Me.UpdateOutputFolderWidgets()
	'End Sub

	'Private Sub UseDefaultOutputSubfolderNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseDefaultOutputSubfolderNameButton.Click
	'	TheApp.Settings.SetDefaultCompileOutputSubfolderName()
	'	'Me.OutputSubfolderNameTextBox.DataBindings("Text").ReadValue()
	'End Sub

	'Private Sub OutputPathNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputFullPathTextBox.Validated
	'	'Me.OutputFullPathTextBox.Text = FileManager.GetCleanPathFileName(Me.OutputFullPathTextBox.Text)
	'	Me.UpdateOutputFullPathTextBox()
	'End Sub

	'Private Sub BrowseForOutputPathNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForOutputPathNameButton.Click
	'	'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
	'	'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
	'       Dim outputPathWdw As New OpenFileDialog()

	'       outputPathWdw.Title = "Open the folder you want as Output Folder"
	'	If Directory.Exists(TheApp.Settings.CompileOutputFullPath) Then
	'		outputPathWdw.InitialDirectory = TheApp.Settings.CompileOutputFullPath
	'	ElseIf File.Exists(TheApp.Settings.CompileQcPathFileName) Then
	'		outputPathWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.CompileQcPathFileName)
	'	ElseIf Directory.Exists(TheApp.Settings.CompileQcPathFileName) Then
	'		outputPathWdw.InitialDirectory = TheApp.Settings.CompileQcPathFileName
	'	Else
	'		outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
	'	End If
	'       outputPathWdw.FileName = "[Folder Selection]"
	'       outputPathWdw.AddExtension = False
	'       outputPathWdw.CheckFileExists = False
	'       outputPathWdw.Multiselect = False
	'       outputPathWdw.ValidateNames = False

	'       If outputPathWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
	'           ' Allow dialog window to completely disappear.
	'           Application.DoEvents()

	'           TheApp.Settings.CompileOutputFullPath = FileManager.GetPath(outputPathWdw.FileName)
	'       End If
	'End Sub

	'Private Sub GotoOutputButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoOutputButton.Click
	'	FileManager.OpenWindowsExplorer(Me.OutputFullPathTextBox.Text)
	'End Sub

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
		TheApp.Settings.SetDefaultCompileOutputSubfolderName()
	End Sub

	'Private Sub GameSetupComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	'Me.SetCompilerOptionsText()
	'	Me.UpdateCompilerOptions(TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex), TheApp.Settings.GameSetups(Me.GameSetupComboBox.SelectedIndex))
	'End Sub

	'Private Sub SetUpGamesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditGameSetupButton.Click
	'	Dim gameSetupWdw As New GameSetupForm()
	'	Dim gameSetupFormInfo As New GameSetupFormInfo()

	'	'gameSetupFormInfo.GameSetupIndex = Me.GameSetupComboBox.SelectedIndex
	'	gameSetupFormInfo.GameSetupIndex = TheApp.Settings.CompileGameSetupSelectedIndex
	'	gameSetupFormInfo.GameSetups = TheApp.Settings.GameSetups
	'	gameSetupWdw.DataSource = gameSetupFormInfo

	'	gameSetupWdw.ShowDialog()

	'	'Me.GameSetupComboBox.SelectedIndex = CType(gameSetupWdw.DataSource, GameSetupFormInfo).GameSetupIndex
	'	'TheApp.Settings.SelectedGameSetup = TheApp.Settings.GameSetups(Me.GameSetupComboBox.SelectedIndex).GameName
	'	TheApp.Settings.CompileGameSetupSelectedIndex = CType(gameSetupWdw.DataSource, GameSetupFormInfo).GameSetupIndex
	'End Sub

	'Private Sub CompilerOptionNoP4CheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompilerOptionNoP4CheckBox.CheckedChanged
	'	Me.EditCompilerOptionsText("nop4", Me.CompilerOptionNoP4CheckBox.Checked)
	'	Me.SetCompilerOptionsText()
	'End Sub

	'Private Sub CompilerOptionVerboseCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompilerOptionVerboseCheckBox.CheckedChanged
	'	Me.EditCompilerOptionsText("verbose", Me.CompilerOptionVerboseCheckBox.Checked)
	'	Me.SetCompilerOptionsText()
	'End Sub

	'Private Sub CompilerOptionDefineBonesCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompilerOptionDefineBonesCheckBox.CheckedChanged
	'	Me.EditCompilerOptionsText("definebones", Me.CompilerOptionDefineBonesCheckBox.Checked)
	'	Me.SetCompilerOptionsText()
	'	Me.UpdateCompilerOptionDefineBonesWidgets()
	'End Sub

	'Private Sub CompilerOptionDefineBonesCreateFileCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CompilerOptionDefineBonesCreateFileCheckBox.CheckedChanged
	'	Me.UpdateCompilerOptionDefineBonesWidgets()
	'End Sub

	Private Sub CompileOptionsUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles CompileOptionsSourceEngineUseDefaultsButton.Click
		TheApp.Settings.SetDefaultCompileOptions()
	End Sub

	Private Sub DirectCompilerOptionsTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DirectCompilerOptionsTextBox.TextChanged
		Me.SetCompilerOptionsText()
	End Sub

	Private Sub CompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompileButton.Click
		Me.RunCompiler()
	End Sub

	Private Sub SkipCurrentModelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SkipCurrentModelButton.Click
		TheApp.Compiler.SkipCurrentModel()
	End Sub

	Private Sub CancelCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelCompileButton.Click
		TheApp.Compiler.CancelAsync()
	End Sub

	Private Sub UseAllInPackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseAllInPackButton.Click
		'TODO: Use the output folder (including file name when needed) as the pack tab's input file or folder.
	End Sub

	Private Sub UseInViewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInViewButton.Click
		TheApp.Settings.ViewMdlPathFileName = TheApp.Compiler.GetOutputPathFileName(Me.theCompiledRelativePathFileNames(Me.CompiledFilesComboBox.SelectedIndex))
		TheApp.Settings.ViewGameSetupSelectedIndex = TheApp.Settings.CompileGameSetupSelectedIndex
	End Sub

	Private Sub RecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecompileButton.Click
		Me.Recompile()
	End Sub

	Private Sub UseInPackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInPackButton.Click
		'TODO: Use the selected compiled file as Pack tab's input file.
	End Sub

	Private Sub GotoCompiledMdlButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoCompiledMdlButton.Click
		Dim pathFileName As String
		pathFileName = TheApp.Compiler.GetOutputPathFileName(Me.theCompiledRelativePathFileNames(Me.CompiledFilesComboBox.SelectedIndex))
		FileManager.OpenWindowsExplorer(pathFileName)
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "CompileQcPathFileName" Then
			Me.UpdateCompileMode()
			Me.SetCompilerOptionsText()
		ElseIf e.PropertyName = "CompileOutputFolderOption" Then
			Me.UpdateOutputPathWidgets()
		ElseIf e.PropertyName = "CompileGameSetupSelectedIndex" Then
			Me.UpdateGameModelsOutputPathTextBox()
			Me.UpdateCompilerOptions()
		ElseIf e.PropertyName = "CompileOptionDefineBonesIsChecked" Then
			Me.EditCompilerOptionsText("definebones", TheApp.Settings.CompileOptionDefineBonesIsChecked)
			Me.SetCompilerOptionsText()
			Me.UpdateCompilerOptionDefineBonesWidgets()
		ElseIf e.PropertyName = "CompileOptionDefineBonesCreateFileIsChecked" Then
			Me.UpdateCompilerOptionDefineBonesWidgets()
			'ElseIf e.PropertyName = "CompileOptionDefineBonesModifyQcFileIsChecked" Then
			'	Me.UpdateCompilerOptionDefineBonesWidgets()
		ElseIf e.PropertyName = "CompileOptionNoP4IsChecked" Then
			Me.EditCompilerOptionsText("nop4", TheApp.Settings.CompileOptionNoP4IsChecked)
			Me.SetCompilerOptionsText()
		ElseIf e.PropertyName = "CompileOptionVerboseIsChecked" Then
			Me.EditCompilerOptionsText("verbose", TheApp.Settings.CompileOptionVerboseIsChecked)
			Me.SetCompilerOptionsText()
		ElseIf e.PropertyName.StartsWith("Compile") AndAlso e.PropertyName.EndsWith("IsChecked") Then
			Me.UpdateWidgets(TheApp.Settings.CompilerIsRunning)
		End If
	End Sub

	Private Sub CompilerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		'If e.ProgressPercentage = 0 Then
		'	Me.CompileLogRichTextBox.Text = ""
		'	Me.UpdateWidgets(True)
		'ElseIf e.ProgressPercentage = 1 Then
		'	Me.CompileLogRichTextBox.AppendText(line + vbCr + vbCr)
		'ElseIf e.ProgressPercentage = 2 Then
		'	Me.CompileLogRichTextBox.AppendText(line + vbCr)
		'ElseIf e.ProgressPercentage = 3 Then
		'	Me.CompileLogRichTextBox.AppendText(vbCr + line + vbCr)
		'ElseIf e.ProgressPercentage = 4 Then
		'	Me.CompileLogRichTextBox.AppendText(vbCr + vbCr + vbCr + line + vbCr)
		'ElseIf e.ProgressPercentage = 5 Then
		'	Me.CompileLogRichTextBox.AppendText(vbCr + vbCr + vbCr)
		'End If
		If e.ProgressPercentage = 0 Then
			Me.CompileLogRichTextBox.Text = ""
			Me.CompileLogRichTextBox.AppendText(line + vbCr)
			Me.UpdateWidgets(True)
		ElseIf e.ProgressPercentage = 1 Then
			Me.CompileLogRichTextBox.AppendText(line + vbCr)
		ElseIf e.ProgressPercentage = 100 Then
			Me.CompileLogRichTextBox.AppendText(line + vbCr)
		End If
	End Sub

	Private Sub CompilerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim statusText As String

		If e.Cancelled Then
			statusText = "Compile canceled"
		Else
			Dim compileResultInfo As CompilerOutputInfo
			compileResultInfo = CType(e.Result, CompilerOutputInfo)
			If compileResultInfo.theStatus = StatusMessage.Error Then
				statusText = "Compile failed; check the log"
			Else
				statusText = "Compile succeeded"
			End If
			Me.UpdateCompiledRelativePathFileNames(compileResultInfo.theCompiledRelativePathFileNames)
		End If

		Me.UpdateWidgets(False)
	End Sub

#End Region

#Region "Private Methods"

	'Private Sub UpdateOutputFolderWidgets()
	'	Me.CompileOutputFolderGroupBox.Enabled = Me.OutputFolderCheckBox.Checked
	'	Me.OutputSubfolderNameTextBox.ReadOnly = Not Me.OutputSubfolderNameRadioButton.Checked
	'	Me.OutputFullPathTextBox.ReadOnly = Me.OutputSubfolderNameRadioButton.Checked
	'	Me.BrowseForOutputPathNameButton.Enabled = Not Me.OutputSubfolderNameRadioButton.Checked
	'End Sub

	'Private Sub UpdateOutputFullPathTextBox()
	'	If String.IsNullOrEmpty(Me.OutputFullPathTextBox.Text) Then
	'		Try
	'			TheApp.Settings.CompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
	'		Catch
	'		End Try
	'	End If
	'End Sub

	Private Sub UpdateOutputPathWidgets()
		Me.GameModelsOutputPathTextBox.Visible = (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.GameModelsFolder)
		Me.OutputPathTextBox.Visible = (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.WorkFolder)
		Me.OutputSubfolderTextBox.Visible = (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.Subfolder)
		Me.BrowseForOutputPathButton.Enabled = (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.WorkFolder)
		Me.BrowseForOutputPathButton.Visible = (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.GameModelsFolder) OrElse (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.WorkFolder)
		Me.GotoOutputPathButton.Enabled = (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.GameModelsFolder) OrElse (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.WorkFolder)
		Me.GotoOutputPathButton.Visible = (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.GameModelsFolder) OrElse (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.WorkFolder)
		Me.UseDefaultOutputSubfolderButton.Enabled = (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.Subfolder)
		Me.UseDefaultOutputSubfolderButton.Visible = (TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.Subfolder)

		If TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.GameModelsFolder Then
			Me.UpdateGameModelsOutputPathTextBox()
		End If
	End Sub

	Private Sub UpdateGameModelsOutputPathTextBox()
		If TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.GameModelsFolder Then
			Dim gameSetup As GameSetup
			Dim gamePath As String
			Dim gameModelsPath As String

			gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
			gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
			gameModelsPath = Path.Combine(gamePath, "models")

			Me.GameModelsOutputPathTextBox.Text = gameModelsPath
		End If
	End Sub

	Private Sub UpdateOutputPathTextBox()
		If TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.WorkFolder Then
			If String.IsNullOrEmpty(Me.OutputPathTextBox.Text) Then
				Try
					TheApp.Settings.CompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
			End If
		End If
	End Sub

	Private Sub BrowseForOutputPath()
		If TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.WorkFolder Then
			'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
			'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
			Dim outputPathWdw As New OpenFileDialog()

			outputPathWdw.Title = "Open the folder you want as Output Folder"
			'If Directory.Exists(TheApp.Settings.CompileOutputFullPath) Then
			'	outputPathWdw.InitialDirectory = TheApp.Settings.CompileOutputFullPath
			'Else
			outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.CompileOutputFullPath)
			If outputPathWdw.InitialDirectory = "" Then
				If File.Exists(TheApp.Settings.CompileQcPathFileName) Then
					outputPathWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.CompileQcPathFileName)
				ElseIf Directory.Exists(TheApp.Settings.CompileQcPathFileName) Then
					outputPathWdw.InitialDirectory = TheApp.Settings.CompileQcPathFileName
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

				TheApp.Settings.CompileOutputFullPath = FileManager.GetPath(outputPathWdw.FileName)
			End If
		End If
	End Sub

	Private Sub GotoFolder()
		If TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.GameModelsFolder Then
			Dim gameSetup As GameSetup
			Dim gamePath As String
			Dim gameModelsPath As String

			gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
			gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
			gameModelsPath = Path.Combine(gamePath, "models")

			If FileManager.PathExistsAfterTryToCreate(gameModelsPath) Then
				FileManager.OpenWindowsExplorer(gameModelsPath)
			End If
		ElseIf TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.WorkFolder Then
			'FileManager.OpenWindowsExplorer(Me.OutputPathTextBox.Text)
			FileManager.OpenWindowsExplorer(TheApp.Settings.CompileOutputFullPath)
		End If
	End Sub

	Private Sub UpdateCompilerOptionDefineBonesWidgets()
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Enabled = Me.CompilerOptionDefineBonesCheckBox.Checked
		Me.CompilerOptionDefineBonesFileNameLabel.Enabled = Me.CompilerOptionDefineBonesCreateFileCheckBox.Enabled AndAlso Me.CompilerOptionDefineBonesCreateFileCheckBox.Checked
		Me.CompilerOptionDefineBonesFileNameTextBox.Enabled = Me.CompilerOptionDefineBonesCreateFileCheckBox.Enabled AndAlso Me.CompilerOptionDefineBonesCreateFileCheckBox.Checked
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Enabled = Me.CompilerOptionDefineBonesCreateFileCheckBox.Enabled AndAlso Me.CompilerOptionDefineBonesCreateFileCheckBox.Checked
	End Sub

	Private Sub UpdateWidgets(ByVal compilerIsRunning As Boolean)
		TheApp.Settings.CompilerIsRunning = compilerIsRunning

		Me.CompileComboBox.Enabled = Not compilerIsRunning
		Me.QcPathFileNameTextBox.Enabled = Not compilerIsRunning
		Me.BrowseForQcPathFolderOrFileNameButton.Enabled = Not compilerIsRunning

		'Me.OutputSubfolderNameRadioButton.Enabled = Not compilerIsRunning
		'Me.OutputSubfolderNameTextBox.Enabled = Not compilerIsRunning
		'Me.UseDefaultOutputSubfolderNameButton.Enabled = Not compilerIsRunning
		'Me.OutputFullPathRadioButton.Enabled = Not compilerIsRunning
		'Me.OutputFullPathTextBox.Enabled = Not compilerIsRunning
		'Me.BrowseForOutputPathNameButton.Enabled = Not compilerIsRunning
		Me.OutputPathComboBox.Enabled = Not compilerIsRunning
		Me.OutputPathTextBox.Enabled = Not compilerIsRunning
		Me.OutputSubfolderTextBox.Enabled = Not compilerIsRunning
		Me.BrowseForOutputPathButton.Enabled = Not compilerIsRunning
		Me.GotoOutputPathButton.Enabled = Not compilerIsRunning
		Me.UseDefaultOutputSubfolderButton.Enabled = Not compilerIsRunning

		Me.OptionsGroupBox.Enabled = Not compilerIsRunning

		Me.CompileButton.Enabled = Not compilerIsRunning
		Me.SkipCurrentModelButton.Enabled = compilerIsRunning
		Me.CancelCompileButton.Enabled = compilerIsRunning
		Me.UseAllInPackButton.Enabled = Not compilerIsRunning AndAlso Me.theCompiledRelativePathFileNames.Count > 0

		Me.CompiledFilesComboBox.Enabled = Not compilerIsRunning AndAlso Me.theCompiledRelativePathFileNames.Count > 0
		Me.UseInViewButton.Enabled = Not compilerIsRunning AndAlso Me.theCompiledRelativePathFileNames.Count > 0 AndAlso (Path.GetExtension(Me.theCompiledRelativePathFileNames(Me.CompiledFilesComboBox.SelectedIndex)) = ".mdl")
		Me.RecompileButton.Enabled = Not compilerIsRunning AndAlso Me.theCompiledRelativePathFileNames.Count > 0
		Me.UseInPackButton.Enabled = Not compilerIsRunning AndAlso Me.theCompiledRelativePathFileNames.Count > 0
		Me.GotoCompiledMdlButton.Enabled = Not compilerIsRunning AndAlso Me.theCompiledRelativePathFileNames.Count > 0
	End Sub

	Private Sub UpdateCompiledRelativePathFileNames(ByVal iCompiledRelativePathFileNames As BindingListEx(Of String))
		'Me.theCompiledRelativePathFileNames.Clear()
		If iCompiledRelativePathFileNames IsNot Nothing Then
			'For Each pathFileName As String In iCompiledRelativePathFileNames
			'	Me.theCompiledRelativePathFileNames.Add(pathFileName)
			'Next
			Me.theCompiledRelativePathFileNames = iCompiledRelativePathFileNames
			'NOTE: Do not sort because the list is already sorted by file and then by folder.
			'Me.theCompiledRelativePathFileNames.Sort()
			'NOTE: Need to set to nothing first to force it to update.
			Me.CompiledFilesComboBox.DataSource = Nothing
			Me.CompiledFilesComboBox.DataSource = Me.theCompiledRelativePathFileNames
		End If
	End Sub

	Private Sub UpdateCompileMode()
		Dim anEnumList As IList
		Dim previousSelectedInputOption As InputOptions

		anEnumList = EnumHelper.ToList(GetType(InputOptions))
		previousSelectedInputOption = TheApp.Settings.CompileMode
		Me.CompileComboBox.DataBindings.Clear()
		Try
			If File.Exists(TheApp.Settings.CompileQcPathFileName) Then
				' Set file mode when a file is selected.
				previousSelectedInputOption = InputOptions.File
			ElseIf Directory.Exists(TheApp.Settings.CompileQcPathFileName) Then
				'NOTE: Remove in reverse index order.
				If Directory.GetFiles(TheApp.Settings.CompileQcPathFileName, "*.qc").Length = 0 Then
					anEnumList.RemoveAt(InputOptions.Folder)
				End If
				anEnumList.RemoveAt(InputOptions.File)
				'Else
				'	Exit Try
			End If

			Me.CompileComboBox.DisplayMember = "Value"
			Me.CompileComboBox.ValueMember = "Key"
			Me.CompileComboBox.DataSource = anEnumList
			Me.CompileComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "CompileMode", False, DataSourceUpdateMode.OnPropertyChanged)

			If EnumHelper.Contains(previousSelectedInputOption, anEnumList) Then
				TheApp.Settings.CompileMode = previousSelectedInputOption
			Else
				TheApp.Settings.CompileMode = CType(EnumHelper.Key(0, anEnumList), InputOptions)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub RunCompiler()
		TheApp.Compiler.Run()
	End Sub

	'Private Sub OpenDefineBonesFile(ByVal modelOutputPath As String)
	'    Try
	'        Dim fileName As String
	'        If String.IsNullOrEmpty(Path.GetExtension(TheApp.Settings.CompileOptionDefineBonesQciFileName)) Then
	'            fileName = TheApp.Settings.CompileOptionDefineBonesQciFileName + ".qci"
	'        Else
	'            fileName = TheApp.Settings.CompileOptionDefineBonesQciFileName
	'        End If
	'        Dim pathFileName As String
	'        pathFileName = Path.Combine(modelOutputPath, fileName)
	'        Me.theDefineBonesFileStream = File.CreateText(pathFileName)
	'    Catch ex As Exception
	'        Me.theDefineBonesFileStream = Nothing
	'    End Try
	'End Sub

	Private Sub UpdateCompilerOptions()
		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)

		If gameSetup.GameEngine = GameEngine.GoldSource Then
			Me.CompilerOptionsGoldSourceEnginePanel.Visible = True
			Me.CompilerOptionsSourceEnginePanel.Visible = False

			'Me.EditCompilerOptionsText("definebones", TheApp.Settings.CompileOptionDefineBonesIsChecked)

			Me.EditCompilerOptionsText("definebones", False)
			Me.EditCompilerOptionsText("nop4", False)
			Me.EditCompilerOptionsText("verbose", False)
		Else
			Me.CompilerOptionsGoldSourceEnginePanel.Visible = False
			Me.CompilerOptionsSourceEnginePanel.Visible = True

			Me.EditCompilerOptionsText("definebones", TheApp.Settings.CompileOptionDefineBonesIsChecked)
			Me.EditCompilerOptionsText("nop4", TheApp.Settings.CompileOptionNoP4IsChecked)
			Me.EditCompilerOptionsText("verbose", TheApp.Settings.CompileOptionVerboseIsChecked)
		End If

		Me.SetCompilerOptionsText()
	End Sub

	Private Sub EditCompilerOptionsText(ByVal iCompilerOption As String, ByVal optionIsEnabled As Boolean)
		Dim compilerOption As String

		compilerOption = "-" + iCompilerOption

		If optionIsEnabled Then
			If Not Me.theSelectedCompilerOptions.Contains(compilerOption) Then
				Me.theSelectedCompilerOptions.Add(compilerOption)
				Me.theSelectedCompilerOptions.Sort()
			End If
		Else
			If Me.theSelectedCompilerOptions.Contains(compilerOption) Then
				Me.theSelectedCompilerOptions.Remove(compilerOption)
			End If
		End If
	End Sub

	'SET Left4Dead2PathRootFolder=C:\Program Files (x86)\Steam\SteamApps\common\left 4 dead 2\
	'SET StudiomdlPathName=%Left4Dead2PathRootFolder%bin\studiomdl.exe
	'SET Left4Dead2PathSubFolder=%Left4Dead2PathRootFolder%left4dead2
	'SET StudiomdlParams=-game "%Left4Dead2PathSubFolder%" -nop4 -verbose -nox360
	'SET FileName=%ModelName%_%TargetApp%
	'"%StudiomdlPathName%" %StudiomdlParams% .\%FileName%.qc > .\%FileName%.log
	Private Sub SetCompilerOptionsText()
		Dim qcFileName As String
		Dim gamePathFileName As String
		Dim selectedIndex As Integer
		Dim gameSetup As GameSetup

		''NOTE: Must use Me.GameSetupComboBox.SelectedIndex because TheApp.Settings.SelectedGameSetupIndex might not be updated yet.
		'selectedIndex = Me.GameSetupComboBox.SelectedIndex
		selectedIndex = TheApp.Settings.CompileGameSetupSelectedIndex

		gameSetup = TheApp.Settings.GameSetups(selectedIndex)

		qcFileName = Path.GetFileName(TheApp.Settings.CompileQcPathFileName)
		gamePathFileName = gameSetup.GamePathFileName

		TheApp.Settings.CompileOptionsText = ""
		'NOTE: Available in Framework 4.0:
		'TheApp.Settings.CompilerOptionsText = String.Join(" ", Me.compilerOptions)
		'------
		For Each compilerOption As String In Me.theSelectedCompilerOptions
			TheApp.Settings.CompileOptionsText += " "
			TheApp.Settings.CompileOptionsText += compilerOption
		Next
		If Me.DirectCompilerOptionsTextBox.Text.Trim() <> "" Then
			TheApp.Settings.CompileOptionsText += " "
			TheApp.Settings.CompileOptionsText += Me.DirectCompilerOptionsTextBox.Text
		End If

		Me.CompilerOptionsTextBox.Text = """"
		Me.CompilerOptionsTextBox.Text += gameSetup.CompilerPathFileName
		Me.CompilerOptionsTextBox.Text += """"

		If gameSetup.GameEngine = GameEngine.Source Then
			Me.CompilerOptionsTextBox.Text += " "
			Me.CompilerOptionsTextBox.Text += "-game"
			Me.CompilerOptionsTextBox.Text += " "
			Me.CompilerOptionsTextBox.Text += """"
			Me.CompilerOptionsTextBox.Text += FileManager.GetPath(gamePathFileName)
			Me.CompilerOptionsTextBox.Text += """"
		End If

		If TheApp.Settings.CompileOptionsText.Trim() <> "" Then
			Me.CompilerOptionsTextBox.Text += TheApp.Settings.CompileOptionsText
		End If

		Me.CompilerOptionsTextBox.Text += " "
		Me.CompilerOptionsTextBox.Text += """"
		Me.CompilerOptionsTextBox.Text += qcFileName
		Me.CompilerOptionsTextBox.Text += """"
	End Sub

	Private Sub Recompile()
		'TODO: Compile the selected QC.
		Me.RunCompiler()
	End Sub

#End Region

#Region "Data"

	Private theSelectedCompilerOptions As List(Of String)
	Private theModelRelativePathFileName As String

	Private theCompiledRelativePathFileNames As BindingListEx(Of String)

#End Region

End Class
