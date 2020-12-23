Imports System.IO
Imports System.Text

Public Class ViewUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub

	'UserControl overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
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

	Private Sub Init()
		Me.theModelViewers = New List(Of Viewer)()

		Dim anEnumList As IList
		anEnumList = EnumHelper.ToList(GetType(SupportedMdlVersion))
		Me.OverrideMdlVersionComboBox.DisplayMember = "Value"
		Me.OverrideMdlVersionComboBox.ValueMember = "Key"
		Me.OverrideMdlVersionComboBox.DataSource = anEnumList
		Me.OverrideMdlVersionComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, Me.NameOfAppSettingOverrideMdlVersionName, False, DataSourceUpdateMode.OnPropertyChanged)

		Me.UpdateDataBindings()

		Me.UpdateWidgets(False)

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
	End Sub

	Private Sub Free()
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged

		'RemoveHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		If Me.MdlPathFileNameTextBox.DataBindings("Text") IsNot Nothing Then
			RemoveHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		End If
		Me.MdlPathFileNameTextBox.DataBindings.Clear()

		Me.OverrideMdlVersionComboBox.DataBindings.Clear()

		Me.FreeDataViewer()
		Me.FreeModelViewerWithModel()
		If Me.theModelViewers IsNot Nothing Then
			For Each aModelViewer As Viewer In Me.theModelViewers
				Me.FreeModelViewer(aModelViewer)
			Next
		End If
	End Sub

#End Region

#Region "Properties"

	Public Property ViewerType() As ViewerType
		Get
			Return Me.theViewerType
		End Get
		Set(value As ViewerType)
			Me.theViewerType = value
		End Set
	End Property

#End Region

#Region "Methods"

	Public Sub RunDataViewer()
		If Not Me.AppSettingDataViewerIsRunning Then
			Me.theDataViewer = New Viewer()
			AddHandler Me.theDataViewer.ProgressChanged, AddressOf Me.DataViewerBackgroundWorker_ProgressChanged
			AddHandler Me.theDataViewer.RunWorkerCompleted, AddressOf Me.DataViewerBackgroundWorker_RunWorkerCompleted
			Me.AppSettingDataViewerIsRunning = True
			Dim mdlVersionOverride As SupportedMdlVersion
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				mdlVersionOverride = TheApp.Settings.PreviewOverrideMdlVersion
			Else
				mdlVersionOverride = TheApp.Settings.ViewOverrideMdlVersion
			End If
			Me.theDataViewer.Run(Me.AppSettingMdlPathFileName, mdlVersionOverride)

			'TODO: If viewer is not running, give user indication of what prevents viewing.
		End If
	End Sub

#End Region

#Region "Widget Event Handlers"

	Private Sub UpdateUserControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		'NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.MdlPathFileNameTextBox, Me.BrowseForMdlFileButton)

		If Not Me.DesignMode Then
			Me.Init()
		End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	'Private Sub ParsePathFileName(ByVal sender As Object, ByVal e As ConvertEventArgs)
	'	If e.DesiredType IsNot GetType(String) Then
	'		Exit Sub
	'	End If
	'	e.Value = FileManager.GetCleanPathFileName(CStr(e.Value))
	'End Sub

	'Private Sub MdlPathFileNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MdlPathFileNameTextBox.Validated
	'	Me.MdlPathFileNameTextBox.Text = FileManager.GetCleanPathFileName(Me.MdlPathFileNameTextBox.Text)
	'End Sub

	Private Sub BrowseForMdlFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForMdlFileButton.Click
		Dim openFileWdw As New OpenFileDialog()

		openFileWdw.Title = "Open the MDL file you want to view"
		'openFileWdw.InitialDirectory = FileManager.GetPath(Me.AppSettingMdlPathFileName)
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.AppSettingMdlPathFileName)
		If openFileWdw.InitialDirectory = "" Then
			openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		End If
		openFileWdw.FileName = Path.GetFileName(Me.AppSettingMdlPathFileName)
		openFileWdw.Filter = "Source Engine Model Files (*.mdl) | *.mdl"
		openFileWdw.AddExtension = True
		openFileWdw.Multiselect = False
		openFileWdw.ValidateNames = True

		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.AppSettingMdlPathFileName = openFileWdw.FileName
		End If
	End Sub

	Private Sub GotoMdlFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoMdlFileButton.Click
		FileManager.OpenWindowsExplorer(Me.AppSettingMdlPathFileName)
	End Sub

	'Private Sub FromDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	Me.AppSettingMdlPathFileName = TheApp.Settings.DecompileMdlPathFileName
	'End Sub

	'Private Sub FromCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	If TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.SubfolderName Then
	'		Me.AppSettingMdlPathFileName = TheApp.Settings.CompileOutputSubfolderName
	'	Else
	'		Me.AppSettingMdlPathFileName = TheApp.Settings.CompileOutputFullPathName
	'	End If
	'End Sub

	'Private Sub SetUpGamesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditGameSetupButton.Click
	'	Dim gameSetupWdw As New GameSetupForm()
	'	Dim gameSetupFormInfo As New GameSetupFormInfo()

	'	gameSetupFormInfo.GameSetupIndex = Me.AppSettingGameSetupSelectedIndex
	'	gameSetupFormInfo.GameSetups = TheApp.Settings.GameSetups
	'	gameSetupWdw.DataSource = gameSetupFormInfo

	'	gameSetupWdw.ShowDialog()

	'	Me.AppSettingGameSetupSelectedIndex = CType(gameSetupWdw.DataSource, GameSetupFormInfo).GameSetupIndex
	'End Sub

	Private Sub ViewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewButton.Click
		Me.RunViewer(False)
	End Sub

	Private Sub ViewAsReplacementButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewAsReplacementButton.Click
		Me.RunViewer(True)
	End Sub

	Private Sub UseInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInDecompileButton.Click
		TheApp.Settings.DecompileMdlPathFileName = Me.AppSettingMdlPathFileName
	End Sub

	Private Sub OpenViewerButton_Click(sender As Object, e As EventArgs) Handles OpenViewerButton.Click
		Me.OpenViewer()
	End Sub

	Private Sub OpenMappingToolButton_Click(sender As Object, e As EventArgs) Handles OpenMappingToolButton.Click
		Me.OpenMappingTool()
	End Sub

	Private Sub RunGameButton_Click(sender As Object, e As EventArgs) Handles RunGameButton.Click
		Me.RunGame()
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If Not Me.AppSettingDataViewerIsRunning Then
			If e.PropertyName = Me.NameOfAppSettingMdlPathFileName OrElse e.PropertyName = Me.NameOfAppSettingOverrideMdlVersionName Then
				Me.UpdateWidgets(Me.AppSettingViewerIsRunning)
				Me.RunDataViewer()
			End If
		End If
	End Sub

	Private Sub DataViewerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.InfoRichTextBox.Text = ""
			'Me.InfoRichTextBox.AppendText(line + vbCr)
			'Me.AppSettingDataViewerIsRunning = True
			Me.MessageTextBox.Text = ""
			Me.MessageTextBox.AppendText(line + vbCrLf)
		ElseIf e.ProgressPercentage = 1 Then
			Me.InfoRichTextBox.AppendText(line + vbCr)
		ElseIf e.ProgressPercentage = 100 Then
			'Me.InfoRichTextBox.AppendText(line + vbCr)
			Me.MessageTextBox.AppendText(line + vbCrLf)
		End If
	End Sub

	Private Sub DataViewerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Me.FreeDataViewer()
		Me.AppSettingDataViewerIsRunning = False
	End Sub

	Private Sub ViewerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.MessageTextBox.Text = ""
			Me.MessageTextBox.AppendText(line + vbCrLf)

			Dim modelViewer As Viewer = CType(sender, Viewer)
			If modelViewer Is Me.theModelViewerWithModel Then
				Me.UpdateWidgets(True)
			End If
		ElseIf e.ProgressPercentage = 1 Then
			Me.MessageTextBox.AppendText(line + vbCrLf)
		ElseIf e.ProgressPercentage = 100 Then
			Me.MessageTextBox.AppendText(line + vbCrLf)
		End If
	End Sub

	Private Sub ViewerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim modelViewer As Viewer = CType(sender, Viewer)
		If modelViewer Is Me.theModelViewerWithModel Then
			Me.FreeModelViewerWithModel()
			Me.UpdateWidgets(False)
		Else
			Me.FreeModelViewer(modelViewer)
		End If
	End Sub

	Private Sub MappingToolBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.MessageTextBox.Text = ""
			Me.MessageTextBox.AppendText(line + vbCr)
			Me.UpdateWidgets(True)
		ElseIf e.ProgressPercentage = 1 Then
			Me.MessageTextBox.AppendText(line + vbCr)
		ElseIf e.ProgressPercentage = 100 Then
			Me.MessageTextBox.AppendText(line + vbCr)
		End If
	End Sub

	Private Sub MappingToolBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim mappingTool As MappingTool = CType(sender, MappingTool)
		RemoveHandler mappingTool.ProgressChanged, AddressOf Me.MappingToolBackgroundWorker_ProgressChanged
		RemoveHandler mappingTool.RunWorkerCompleted, AddressOf Me.MappingToolBackgroundWorker_RunWorkerCompleted

		Me.UpdateWidgets(False)
	End Sub

	Private Sub GameAppBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.MessageTextBox.Text = ""
			Me.MessageTextBox.AppendText(line + vbCr)
			Me.UpdateWidgets(True)
		ElseIf e.ProgressPercentage = 1 Then
			Me.MessageTextBox.AppendText(line + vbCr)
		ElseIf e.ProgressPercentage = 100 Then
			Me.MessageTextBox.AppendText(line + vbCr)
		End If
	End Sub

	Private Sub GameAppBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim gameApp As GameApp = CType(sender, GameApp)
		RemoveHandler gameApp.ProgressChanged, AddressOf Me.GameAppBackgroundWorker_ProgressChanged
		RemoveHandler gameApp.RunWorkerCompleted, AddressOf Me.GameAppBackgroundWorker_RunWorkerCompleted

		Me.UpdateWidgets(False)
	End Sub

#End Region

#Region "Private Properties"

	Private ReadOnly Property NameOfAppSettingMdlPathFileName() As String
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return "PreviewMdlPathFileName"
			Else
				Return "ViewMdlPathFileName"
			End If
		End Get
	End Property

	Private ReadOnly Property NameOfAppSettingOverrideMdlVersionName() As String
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return "PreviewOverrideMdlVersion"
			Else
				Return "ViewOverrideMdlVersion"
			End If
		End Get
	End Property

	Private ReadOnly Property NameOfAppSettingGameSetupSelectedIndex() As String
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return "PreviewGameSetupSelectedIndex"
			Else
				Return "ViewGameSetupSelectedIndex"
			End If
		End Get
	End Property

	Private Property AppSettingGameSetupSelectedIndex() As Integer
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return TheApp.Settings.PreviewGameSetupSelectedIndex
			Else
				Return TheApp.Settings.ViewGameSetupSelectedIndex
			End If
		End Get
		Set(value As Integer)
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				TheApp.Settings.PreviewGameSetupSelectedIndex = value
			Else
				TheApp.Settings.ViewGameSetupSelectedIndex = value
			End If
		End Set
	End Property

	Private Property AppSettingMdlPathFileName() As String
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return TheApp.Settings.PreviewMdlPathFileName
			Else
				Return TheApp.Settings.ViewMdlPathFileName
			End If
		End Get
		Set(value As String)
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				TheApp.Settings.PreviewMdlPathFileName = value
			Else
				TheApp.Settings.ViewMdlPathFileName = value
			End If
		End Set
	End Property

	Private Property AppSettingDataViewerIsRunning() As Boolean
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return TheApp.Settings.PreviewDataViewerIsRunning
			Else
				Return TheApp.Settings.ViewDataViewerIsRunning
			End If
		End Get
		Set(value As Boolean)
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				TheApp.Settings.PreviewDataViewerIsRunning = value
			Else
				TheApp.Settings.ViewDataViewerIsRunning = value
			End If
		End Set
	End Property

	Private Property AppSettingViewerIsRunning() As Boolean
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return TheApp.Settings.PreviewViewerIsRunning
			Else
				Return TheApp.Settings.ViewViewerIsRunning
			End If
		End Get
		Set(value As Boolean)
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				TheApp.Settings.PreviewViewerIsRunning = value
			Else
				TheApp.Settings.ViewViewerIsRunning = value
			End If
		End Set
	End Property

	Private ReadOnly Property ViewAsReplacementSubfolderName() As String
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return "-preview"
			Else
				Return "-view"
			End If
		End Get
	End Property

#End Region

#Region "Private Methods"

	Private Sub UpdateDataBindings()
		Me.MdlPathFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, Me.NameOfAppSettingMdlPathFileName, False, DataSourceUpdateMode.OnValidation)
		'AddHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		AddHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName

		'NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
		Me.GameSetupComboBox.DisplayMember = "GameName"
		Me.GameSetupComboBox.ValueMember = "GameName"
		Me.GameSetupComboBox.DataSource = TheApp.Settings.GameSetups
		Me.GameSetupComboBox.DataBindings.Add("SelectedIndex", TheApp.Settings, Me.NameOfAppSettingGameSetupSelectedIndex, False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

	Private Sub UpdateWidgets(ByVal modelViewerIsRunning As Boolean)
		Me.AppSettingViewerIsRunning = modelViewerIsRunning

		If String.IsNullOrEmpty(Me.AppSettingMdlPathFileName) _
			OrElse Not (Path.GetExtension(Me.AppSettingMdlPathFileName).ToLower() = ".mdl") _
			OrElse Not File.Exists(Me.AppSettingMdlPathFileName) Then
			Me.ViewButton.Enabled = False
			Me.ViewAsReplacementButton.Enabled = False
			Me.UseInDecompileButton.Enabled = False
		Else
			Me.ViewButton.Enabled = Not modelViewerIsRunning
			Me.ViewAsReplacementButton.Enabled = Not modelViewerIsRunning
			Me.UseInDecompileButton.Enabled = True
		End If
	End Sub

	Private Sub RunViewer(ByVal viewAsReplacement As Boolean)
		Me.theModelViewerWithModel = New Viewer()
		AddHandler Me.theModelViewerWithModel.ProgressChanged, AddressOf Me.ViewerBackgroundWorker_ProgressChanged
		AddHandler Me.theModelViewerWithModel.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
		Me.theModelViewerWithModel.Run(Me.AppSettingGameSetupSelectedIndex, Me.AppSettingMdlPathFileName, viewAsReplacement, ViewAsReplacementSubfolderName)

		'TODO: If viewer is not running, give user indication of what prevents viewing.
	End Sub

	Private Sub OpenViewer()
		Dim aModelViewer As Viewer
		aModelViewer = New Viewer()
		AddHandler aModelViewer.ProgressChanged, AddressOf Me.ViewerBackgroundWorker_ProgressChanged
		AddHandler aModelViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
		aModelViewer.Run(Me.AppSettingGameSetupSelectedIndex)

		Me.theModelViewers.Add(aModelViewer)

		'TODO: If viewer is not running, give user indication of what prevents viewing.
	End Sub

	Private Sub OpenMappingTool()
		Dim mappingTool As MappingTool
		mappingTool = New MappingTool()
		AddHandler mappingTool.ProgressChanged, AddressOf Me.MappingToolBackgroundWorker_ProgressChanged
		AddHandler mappingTool.RunWorkerCompleted, AddressOf Me.MappingToolBackgroundWorker_RunWorkerCompleted
		mappingTool.Run(Me.AppSettingGameSetupSelectedIndex)

		'TODO: If viewer is not running, give user indication of what prevents viewing.
	End Sub

	Private Sub RunGame()
		Dim gameApp As GameApp
		gameApp = New GameApp()
		AddHandler gameApp.ProgressChanged, AddressOf Me.GameAppBackgroundWorker_ProgressChanged
		AddHandler gameApp.RunWorkerCompleted, AddressOf Me.GameAppBackgroundWorker_RunWorkerCompleted
		gameApp.Run(Me.AppSettingGameSetupSelectedIndex)

		'TODO: If gameApp is not running, give user indication of what prevents viewing.
	End Sub

	Private Sub FreeDataViewer()
		If Me.theDataViewer IsNot Nothing Then
			RemoveHandler Me.theDataViewer.ProgressChanged, AddressOf Me.DataViewerBackgroundWorker_ProgressChanged
			RemoveHandler Me.theDataViewer.RunWorkerCompleted, AddressOf Me.DataViewerBackgroundWorker_RunWorkerCompleted
			Me.theDataViewer.Dispose()
			Me.theDataViewer = Nothing
		End If
	End Sub

	Private Sub FreeModelViewerWithModel()
		If Me.theModelViewerWithModel IsNot Nothing Then
			RemoveHandler Me.theModelViewerWithModel.ProgressChanged, AddressOf Me.ViewerBackgroundWorker_ProgressChanged
			RemoveHandler Me.theModelViewerWithModel.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
			Me.theModelViewerWithModel.Dispose()
			Me.theModelViewerWithModel = Nothing
		End If
	End Sub

	Private Sub FreeModelViewer(ByVal aModelViewer As Viewer)
		If aModelViewer IsNot Nothing Then
			RemoveHandler aModelViewer.ProgressChanged, AddressOf Me.ViewerBackgroundWorker_ProgressChanged
			RemoveHandler aModelViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
			aModelViewer.Dispose()
			aModelViewer = Nothing

			Me.theModelViewers.Remove(aModelViewer)
		End If
	End Sub

#End Region

#Region "Data"

	Dim theViewerType As ViewerType

	Dim theDataViewer As Viewer
	Dim theModelViewerWithModel As Viewer
	Dim theModelViewers As List(Of Viewer)

#End Region

End Class
