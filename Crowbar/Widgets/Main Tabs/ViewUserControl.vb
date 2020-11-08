Imports System.IO
Imports System.Text

Public Class ViewUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		Me.theTwoModelViewers = New List(Of Viewer)(2)
		Me.theTwoModelViewers.Add(Nothing)
		Me.theTwoModelViewers.Add(Nothing)
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
		AddHandler Me.ParentForm.ResizeEnd, AddressOf Me.ParentForm_ResizeEnd
	End Sub

	Private Sub Free()
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
		If Me.ParentForm IsNot Nothing Then
			RemoveHandler Me.ParentForm.ResizeEnd, AddressOf Me.ParentForm_ResizeEnd
		End If

		'RemoveHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
		If Me.MdlPathFileNameTextBox.DataBindings("Text") IsNot Nothing Then
			RemoveHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		End If
		Me.MdlPathFileNameTextBox.DataBindings.Clear()

		Me.OverrideMdlVersionComboBox.DataBindings.Clear()

		Me.FreeDataViewer()
		Me.FreeModelViewerWithModel()
		If Me.theTwoModelViewers IsNot Nothing Then
			For Each aModelViewer As Viewer In Me.theTwoModelViewers
				Me.FreeModelViewer(aModelViewer)
			Next
			Me.theTwoModelViewers.Clear()
		End If
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

	Private Sub ViewUserControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		'NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.MdlPathFileNameTextBox, Me.BrowseForMdlFileButton)

		If Not Me.DesignMode Then
			Me.Init()
		End If
	End Sub

	Private Sub ViewUserControl_HandleDestroyed(sender As Object, e As EventArgs) Handles MyBase.HandleDestroyed
		If Not Me.DesignMode Then
			Me.Free()
		End If
	End Sub

	Private Sub ViewUserControl_Resize(sender As Object, e As EventArgs) Handles Me.Resize
		If ParentForm IsNot Nothing AndAlso Me.Visible Then
			If Me.ParentForm.Bounds.Equals(Me.ParentForm.RestoreBounds) OrElse ParentForm.WindowState = FormWindowState.Maximized Then
				If Me.theHlmvAppWindowHandle <> IntPtr.Zero Then
					Me.ResizeHlmvWidgets()
				End If
				If Me.theFirstHlmvAppWindowHandle <> IntPtr.Zero Then
					Me.ResizeFirstHlmvWidgets()
				End If
				If Me.theSecondHlmvAppWindowHandle <> IntPtr.Zero Then
					Me.ResizeSecondHlmvWidgets()
				End If
			End If
		End If
	End Sub

	Private Sub ParentForm_ResizeEnd(sender As Object, e As EventArgs)
		If Me.Visible Then
			If Me.theHlmvAppWindowHandle <> IntPtr.Zero Then
				Me.ResizeHlmvWidgets()
			End If
			If Me.theFirstHlmvAppWindowHandle <> IntPtr.Zero Then
				Me.ResizeFirstHlmvWidgets()
			End If
			If Me.theSecondHlmvAppWindowHandle <> IntPtr.Zero Then
				Me.ResizeSecondHlmvWidgets()
			End If
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

	Private Sub OpenTwoViewersButton_Click(sender As Object, e As EventArgs) Handles OpenTwoViewersButton.Click
		Me.OpenTwoViewers()
	End Sub

	Private Sub CloseTwoViewersButton_Click(sender As Object, e As EventArgs) Handles CloseTwoViewersButton.Click
		If Me.theTwoModelViewers IsNot Nothing Then
			For Each aModelViewer As Viewer In Me.theTwoModelViewers
				Me.FreeModelViewer(aModelViewer)
			Next
			Me.theTwoModelViewers(0) = Nothing
			Me.theTwoModelViewers(1) = Nothing
		End If
	End Sub

	'Private Sub HlmvSplitContainer_SplitterMoved(sender As Object, e As EventArgs) Handles HlmvSplitContainer.SplitterMoved
	'	If Me.theHlmvAppWindowHandle <> IntPtr.Zero Then
	'		Win32Api.MoveWindow(Me.theHlmvAppWindowHandle, 0, 0, Me.HlmvSplitContainer.Panel1.Width, Me.HlmvSplitContainer.Panel1.Height, True)
	'		'TODO: Need to call this only after Splitter has finished moving (i.e. MouseUp occurs).
	'		'Win32Api.SetForegroundWindow(Me.theHlmvAppWindowHandle)
	'	End If
	'End Sub

	Private Sub FirstHlmvSplitContainer_MouseDown(sender As Object, e As EventArgs) Handles FirstHlmvSplitContainer.MouseDown
		If Me.theFirstHlmvAppWindowHandle <> IntPtr.Zero Then
			Me.theMouseDownIsActiveOnHlmvSplitContainer = True
		End If
	End Sub

	Private Sub FirstHlmvSplitContainer_MouseUp(sender As Object, e As EventArgs) Handles FirstHlmvSplitContainer.MouseUp
		If Me.theFirstHlmvAppWindowHandle <> IntPtr.Zero AndAlso Me.theMouseDownIsActiveOnHlmvSplitContainer Then
			Me.theMouseDownIsActiveOnHlmvSplitContainer = False
			Win32Api.SetForegroundWindow(Me.theFirstHlmvAppWindowHandle)
			'Threading.Thread.Sleep(1500)
			Me.FirstHlmvMainPanel.Refresh()
			Me.FirstHlmvModelPanel.Refresh()
		End If
	End Sub

	Private Sub FirstHlmvSplitContainer_SplitterMoved(sender As Object, e As EventArgs) Handles FirstHlmvSplitContainer.SplitterMoved
		If Me.theFirstHlmvAppWindowHandle <> IntPtr.Zero AndAlso Not Me.theMouseDownIsActiveOnHlmvSplitContainer Then
			Me.ResizeFirstHlmvWidgets()
		End If
	End Sub

	Private Sub SecondHlmvSplitContainer_MouseDown(sender As Object, e As EventArgs) Handles SecondHlmvSplitContainer.MouseDown
		If Me.theSecondHlmvAppWindowHandle <> IntPtr.Zero Then
			Me.theMouseDownIsActiveOnHlmvSplitContainer = True
		End If
	End Sub

	Private Sub SecondHlmvSplitContainer_MouseUp(sender As Object, e As EventArgs) Handles SecondHlmvSplitContainer.MouseUp
		If Me.theSecondHlmvAppWindowHandle <> IntPtr.Zero AndAlso Me.theMouseDownIsActiveOnHlmvSplitContainer Then
			Me.theMouseDownIsActiveOnHlmvSplitContainer = False
			Win32Api.SetForegroundWindow(Me.theSecondHlmvAppWindowHandle)
			'Threading.Thread.Sleep(1500)
			Me.SecondHlmvMainPanel.Refresh()
			Me.SecondHlmvModelPanel.Refresh()
		End If
	End Sub

	Private Sub SecondHlmvSplitContainer_SplitterMoved(sender As Object, e As EventArgs) Handles SecondHlmvSplitContainer.SplitterMoved
		If Me.theSecondHlmvAppWindowHandle <> IntPtr.Zero AndAlso Not Me.theMouseDownIsActiveOnHlmvSplitContainer Then
			Me.ResizeSecondHlmvWidgets()
		End If
	End Sub

	Private Sub HlmvSplitContainer_MouseDown(sender As Object, e As EventArgs) Handles HlmvSplitContainer.MouseDown
		If Me.theHlmvAppWindowHandle <> IntPtr.Zero OrElse Me.theFirstHlmvAppWindowHandle <> IntPtr.Zero OrElse Me.theSecondHlmvAppWindowHandle <> IntPtr.Zero Then
			Me.theMouseDownIsActiveOnHlmvSplitContainer = True
		End If
	End Sub

	Private Sub HlmvSplitContainer_MouseUp(sender As Object, e As EventArgs) Handles HlmvSplitContainer.MouseUp
		If (Me.theHlmvAppWindowHandle <> IntPtr.Zero OrElse Me.theFirstHlmvAppWindowHandle <> IntPtr.Zero OrElse Me.theSecondHlmvAppWindowHandle <> IntPtr.Zero) AndAlso Me.theMouseDownIsActiveOnHlmvSplitContainer Then
			Me.theMouseDownIsActiveOnHlmvSplitContainer = False
			'Win32Api.SetForegroundWindow(Me.theHlmvAppWindowHandle)
			'Threading.Thread.Sleep(1500)
		End If
	End Sub

	Private Sub HlmvSplitContainer_SplitterMoved(sender As Object, e As EventArgs) Handles HlmvSplitContainer.SplitterMoved
		If (Me.theHlmvAppWindowHandle <> IntPtr.Zero OrElse Me.theFirstHlmvAppWindowHandle <> IntPtr.Zero OrElse Me.theSecondHlmvAppWindowHandle <> IntPtr.Zero) AndAlso Not Me.theMouseDownIsActiveOnHlmvSplitContainer Then
			Me.ResizeHlmvWidgets()
		End If
	End Sub

	'Private Sub SplitContainer2_MouseDown(sender As Object, e As EventArgs) Handles SplitContainer2.MouseDown
	'	If Me.theHlmvAppWindowHandle <> IntPtr.Zero Then
	'		Me.theMouseDownIsActiveOnSplitContainer2 = True
	'	End If
	'End Sub

	'Private Sub SplitContainer2_MouseUp(sender As Object, e As EventArgs) Handles SplitContainer2.MouseUp
	'	If Me.theHlmvAppWindowHandle <> IntPtr.Zero Then
	'		Me.theMouseDownIsActiveOnSplitContainer2 = False
	'	End If
	'End Sub

	'Private Sub SplitContainer2_SplitterMoved(sender As Object, e As EventArgs) Handles SplitContainer2.SplitterMoved
	'	If Me.theHlmvAppWindowHandle <> IntPtr.Zero AndAlso Not Me.theMouseDownIsActiveOnSplitContainer2 Then
	'		'Me.SplitContainer3.SplitterDistance = Me.SplitContainer2.SplitterDistance
	'		'Win32Api.MoveWindow(Me.theHlmvAppWindowHandle, 0, 0, Me.HlmvSplitContainer.Panel1.Width, Me.HlmvSplitContainer.Panel1.Height, True)
	'		Win32Api.MoveWindow(Me.theModelPanelHandle, 0, 0, Me.HlmvModelPanel.Width, Me.HlmvModelPanel.Height, True)
	'		'Win32Api.MoveWindow(Me.theModelPanelSiblingHandle, 0, 0, Me.HlmvModelPanel.Width, Me.HlmvModelPanel.Height, True)
	'		' Need to call this only after Splitter has finished moving (i.e. MouseUp occurs).
	'		Win32Api.SetForegroundWindow(Me.theHlmvAppWindowHandle)
	'		'Win32Api.PostMessage(Me.theHlmvAppWindowHandle, Win32Api.WndMsg.WM_SIZE, CType(0, IntPtr), CType(0, IntPtr))
	'	End If
	'End Sub

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
		If e.ProgressPercentage = 50 Then
			Dim viewer As Viewer = CType(sender, Viewer)

			If Me.theTwoModelViewers.Contains(viewer) Then
				If viewer Is Me.theTwoModelViewers(0) Then
					Me.theFirstHlmvAppWindowHandle = CType(e.UserState, IntPtr)
					Me.InitFirstViewerWidgets()

					If Me.theTwoModelViewers(1) Is Nothing Then
						Dim aModelViewer As Viewer
						aModelViewer = New Viewer()
						AddHandler aModelViewer.ProgressChanged, AddressOf Me.ViewerBackgroundWorker_ProgressChanged
						AddHandler aModelViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
						aModelViewer.Run(Me.AppSettingGameSetupSelectedIndex)

						Me.theTwoModelViewers(1) = aModelViewer

						'TODO: If viewer is not running, give user indication of what prevents viewing.
					End If
				Else
					Me.theSecondHlmvAppWindowHandle = CType(e.UserState, IntPtr)
					Me.InitSecondViewerWidgets()
				End If
			Else
				Me.theHlmvAppWindowHandle = CType(e.UserState, IntPtr)
				Me.InitViewerWidgets()
			End If
			Exit Sub
		ElseIf e.ProgressPercentage = 51 Then
			Dim viewer As Viewer = CType(sender, Viewer)

			If Me.theTwoModelViewers.Contains(viewer) Then
				If viewer Is Me.theTwoModelViewers(0) Then
					Win32Api.SetParent(Me.theFirstHlmvAppWindowHandle, IntPtr.Zero)
					Win32Api.SetParent(Me.theFirstModelPanelHandle, Me.theFirstHlmvAppWindowHandle)
					Win32Api.SetParent(Me.theFirstOptionsPanelHandle, Me.theFirstHlmvAppWindowHandle)
					Me.theTwoModelViewers(0) = Nothing
				Else
					Win32Api.SetParent(Me.theSecondHlmvAppWindowHandle, IntPtr.Zero)
					Win32Api.SetParent(Me.theSecondModelPanelHandle, Me.theSecondHlmvAppWindowHandle)
					Win32Api.SetParent(Me.theSecondOptionsPanelHandle, Me.theSecondHlmvAppWindowHandle)
					Me.theTwoModelViewers(1) = Nothing
				End If
			Else
				Win32Api.SetParent(Me.theHlmvAppWindowHandle, IntPtr.Zero)
				Win32Api.SetParent(Me.theModelPanelHandle, Me.theHlmvAppWindowHandle)
				Win32Api.SetParent(Me.theOptionsPanelHandle, Me.theHlmvAppWindowHandle)
			End If
			Exit Sub
		End If

		Dim line As String = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			'Me.MessageTextBox.Text = ""
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

	Private Sub InitFirstViewerWidgets()
		Win32Api.SetWindowLong(Me.theFirstHlmvAppWindowHandle, Win32Api.WindowLongFlags.GWL_STYLE, CInt(Win32Api.WindowStyles.WS_VISIBLE))
		Win32Api.SetParent(Me.theFirstHlmvAppWindowHandle, Me.FirstHlmvMainPanel.Handle)
		'Win32Api.SetWindowPos(Me.theFirstHlmvAppWindowHandle, Win32Api.HWND_TOP, 0, 0, 0, 0, Win32Api.SetWindowPosFlags.IgnoreMove Or Win32Api.SetWindowPosFlags.IgnoreResize)
		Win32Api.MoveWindow(Me.theFirstHlmvAppWindowHandle, 0, 0, Me.FirstHlmvMainPanel.Width, Me.FirstHlmvMainPanel.Height, True)

		' This is the model panel.
		Me.theFirstModelPanelHandle = Win32Api.FindWindowEx(Me.theFirstHlmvAppWindowHandle, IntPtr.Zero, "mx_class", "")
		If Me.theFirstModelPanelHandle <> IntPtr.Zero Then
			Win32Api.SetParent(Me.theFirstModelPanelHandle, Me.FirstHlmvModelPanel.Handle)
			Win32Api.MoveWindow(Me.theFirstModelPanelHandle, 0, 0, Me.FirstHlmvModelPanel.Width, Me.FirstHlmvModelPanel.Height, True)
		End If

		Win32Api.SetForegroundWindow(Me.theFirstHlmvAppWindowHandle)
		'Threading.Thread.Sleep(1500)
		Me.FirstHlmvMainPanel.Refresh()
		Me.FirstHlmvModelPanel.Refresh()
		'Threading.Thread.Sleep(500)
	End Sub

	Private Sub InitSecondViewerWidgets()
		Win32Api.SetWindowLong(Me.theSecondHlmvAppWindowHandle, Win32Api.WindowLongFlags.GWL_STYLE, CInt(Win32Api.WindowStyles.WS_VISIBLE))
		Win32Api.SetParent(Me.theSecondHlmvAppWindowHandle, Me.SecondHlmvMainPanel.Handle)
		Win32Api.MoveWindow(Me.theSecondHlmvAppWindowHandle, 0, 0, Me.SecondHlmvMainPanel.Width, Me.SecondHlmvMainPanel.Height, True)

		' This is the model panel.
		Me.theSecondModelPanelHandle = Win32Api.FindWindowEx(Me.theSecondHlmvAppWindowHandle, IntPtr.Zero, "mx_class", "")
		If Me.theSecondModelPanelHandle <> IntPtr.Zero Then
			Win32Api.SetParent(Me.theSecondModelPanelHandle, Me.SecondHlmvModelPanel.Handle)
			Win32Api.MoveWindow(Me.theSecondModelPanelHandle, 0, 0, Me.SecondHlmvModelPanel.Width, Me.SecondHlmvModelPanel.Height, True)
		End If

		Win32Api.SetForegroundWindow(Me.theSecondHlmvAppWindowHandle)
		'Threading.Thread.Sleep(1500)
		Me.SecondHlmvMainPanel.Refresh()
		Me.SecondHlmvModelPanel.Refresh()
	End Sub

	Private Sub InitViewerWidgets()
		'Dim hlmvThread As UInt32 = Win32Api.GetWindowThreadProcessId(Me.theHlmvAppWindowHandle, IntPtr.Zero)

		' Using WS_CHILD prevents menu from showing.
		'Win32Api.SetWindowLong(Me.theHlmvAppWindowHandle, Win32Api.WindowLongFlags.GWL_STYLE, CInt(Win32Api.WindowStyles.WS_VISIBLE Or Win32Api.WindowStyles.WS_CHILD))
		Win32Api.SetWindowLong(Me.theHlmvAppWindowHandle, Win32Api.WindowLongFlags.GWL_STYLE, CInt(Win32Api.WindowStyles.WS_VISIBLE))
		Win32Api.SetParent(Me.theHlmvAppWindowHandle, Me.HlmvMainPanel.Handle)
		'Win32Api.SetWindowLong(Me.theHlmvAppWindowHandle, Win32Api.WindowLongFlags.GWL_STYLE, CInt(Win32Api.WindowStyles.WS_VISIBLE))
		''Win32Api.SetWindowLong(Me.theHlmvAppWindowHandle, Win32Api.WindowLongFlags.GWL_EXSTYLE, CInt(Win32Api.WindowStylesEx.WS_EX_TOOLWINDOW))
		'' Move the window to a widget within the ViewUserControl.
		Win32Api.MoveWindow(Me.theHlmvAppWindowHandle, 0, 0, Me.HlmvMainPanel.Width, Me.HlmvMainPanel.Height, True)
		'Dim mainThread As UInt32 = Win32Api.GetWindowThreadProcessId(Me.Handle, IntPtr.Zero)
		'Dim resultIsSuccess As Boolean = Win32Api.AttachThreadInput(mainThread, hlmvThread, True)
		'Dim resultIsSuccess As Boolean = Win32Api.AttachThreadInput(hlmvThread, mainThread, True)
		'Dim resultIsSuccess As Boolean = Win32Api.AttachThreadInput(mainThread, hlmvThread, False)
		'Dim resultIsSuccess As Boolean = Win32Api.AttachThreadInput(hlmvThread, mainThread, False)

		'' This is a sibling control of the model panel.
		'Me.theModelPanelSiblingHandle = Win32Api.FindWindowEx(Me.theHlmvAppWindowHandle, IntPtr.Zero, "shaderdx8", "shaderdx8")
		'If Me.theModelPanelSiblingHandle <> IntPtr.Zero Then
		'	Win32Api.SetParent(Me.theModelPanelSiblingHandle, Me.HlmvModelPanel.Handle)
		'	Win32Api.MoveWindow(Me.theModelPanelSiblingHandle, 0, 0, Me.HlmvModelPanel.Width, Me.HlmvModelPanel.Height, True)
		'End If

		' This is the model panel.
		Me.theModelPanelHandle = Win32Api.FindWindowEx(Me.theHlmvAppWindowHandle, IntPtr.Zero, "mx_class", "")
		If Me.theModelPanelHandle <> IntPtr.Zero Then
			'Win32Api.SetWindowLong(Me.theModelPanelHandle, Win32Api.WindowLongFlags.GWL_STYLE, CInt(Win32Api.WindowStyles.WS_VISIBLE Or Win32Api.WindowStyles.WS_CHILD))
			Win32Api.SetParent(Me.theModelPanelHandle, Me.HlmvModelPanel.Handle)
			Win32Api.MoveWindow(Me.theModelPanelHandle, 0, 0, Me.HlmvModelPanel.Width, Me.HlmvModelPanel.Height, True)
		End If

		'' This is the options panel below the model panel.
		'Me.theOptionsPanelHandle = Win32Api.FindWindowEx(Me.theHlmvAppWindowHandle, IntPtr.Zero, "mx_class", "Control Panel")
		'If Me.theOptionsPanelHandle <> IntPtr.Zero Then
		'	Win32Api.SetParent(Me.theOptionsPanelHandle, Me.HlmvOptionsPanel.Handle)
		'	Win32Api.MoveWindow(Me.theOptionsPanelHandle, 0, 0, Me.HlmvOptionsPanel.Width, Me.HlmvOptionsPanel.Height, True)
		'End If

		Win32Api.SetForegroundWindow(Me.theHlmvAppWindowHandle)
		'Threading.Thread.Sleep(1500)
	End Sub

	Private Sub ResizeFirstHlmvWidgets()
		Win32Api.MoveWindow(Me.theFirstHlmvAppWindowHandle, 0, 0, Me.FirstHlmvMainPanel.Width, Me.FirstHlmvMainPanel.Height, True)
		Win32Api.MoveWindow(Me.theFirstModelPanelHandle, 0, 0, Me.FirstHlmvModelPanel.Width, Me.FirstHlmvModelPanel.Height, True)
		Win32Api.SetForegroundWindow(Me.theFirstHlmvAppWindowHandle)
		'Win32Api.ShowWindow(Me.theFirstHlmvAppWindowHandle, Win32Api.ShowWindowCommands.Show)
		'Threading.Thread.Sleep(1500)
		Me.FirstHlmvMainPanel.Refresh()
		Me.FirstHlmvModelPanel.Refresh()
	End Sub

	Private Sub ResizeSecondHlmvWidgets()
		Win32Api.MoveWindow(Me.theSecondHlmvAppWindowHandle, 0, 0, Me.SecondHlmvMainPanel.Width, Me.SecondHlmvMainPanel.Height, True)
		Win32Api.MoveWindow(Me.theSecondModelPanelHandle, 0, 0, Me.SecondHlmvModelPanel.Width, Me.SecondHlmvModelPanel.Height, True)
		Win32Api.SetForegroundWindow(Me.theSecondHlmvAppWindowHandle)
		'Win32Api.ShowWindow(Me.theSecondHlmvAppWindowHandle, Win32Api.ShowWindowCommands.Show)
		'Threading.Thread.Sleep(1500)
		Me.SecondHlmvMainPanel.Refresh()
		Me.SecondHlmvModelPanel.Refresh()
	End Sub

	Private Sub ResizeHlmvWidgets()
		Win32Api.MoveWindow(Me.theHlmvAppWindowHandle, 0, 0, Me.HlmvMainPanel.Width, Me.HlmvMainPanel.Height, True)
		Win32Api.MoveWindow(Me.theModelPanelHandle, 0, 0, Me.HlmvModelPanel.Width, Me.HlmvModelPanel.Height, True)
		'Win32Api.MoveWindow(Me.theOptionsPanelHandle, 0, 0, Me.HlmvOptionsPanel.Width, Me.HlmvOptionsPanel.Height, True)
		Win32Api.SetForegroundWindow(Me.theHlmvAppWindowHandle)
		'Threading.Thread.Sleep(1500)

		'Me.ParentForm.Activate()
		'Me.Select()
		'Win32Api.SetForegroundWindow(Me.ParentForm.Handle)

		Me.ResizeFirstHlmvWidgets()
		Me.ResizeSecondHlmvWidgets()
	End Sub

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

	Private Sub OpenTwoViewers()
		Dim aModelViewer As Viewer

		' Open first viewer, only if there are no viewers open.
		If Me.theTwoModelViewers(0) Is Nothing AndAlso Me.theTwoModelViewers(1) Is Nothing Then
			aModelViewer = New Viewer()
			AddHandler aModelViewer.ProgressChanged, AddressOf Me.ViewerBackgroundWorker_ProgressChanged
			AddHandler aModelViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
			aModelViewer.Run(Me.AppSettingGameSetupSelectedIndex)

			Me.theTwoModelViewers(0) = aModelViewer

			'TODO: If viewer is not running, give user indication of what prevents viewing.
		End If

		'Threading.Thread.Sleep(50)

		' Open second viewer after the background process starts the first viewer.
		'If Me.theTwoModelViewers(1) Is Nothing Then
		'	aModelViewer = New Viewer()
		'	AddHandler aModelViewer.ProgressChanged, AddressOf Me.ViewerBackgroundWorker_ProgressChanged
		'	AddHandler aModelViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
		'	aModelViewer.Run(Me.AppSettingGameSetupSelectedIndex)

		'	Me.theTwoModelViewers(1) = aModelViewer

		'	'TODO: If viewer is not running, give user indication of what prevents viewing.
		'End If
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
			aModelViewer.Halt()
			aModelViewer = Nothing
		End If
	End Sub

#End Region

#Region "Data"

	Private theViewerType As ViewerType

	Private theDataViewer As Viewer
	Private theModelViewerWithModel As Viewer
	Private theModelViewers As List(Of Viewer)
	Private theTwoModelViewers As List(Of Viewer)

	Private theMouseDownIsActiveOnHlmvSplitContainer As Boolean = False

	' First viewer
	Private theFirstHlmvAppWindowHandle As IntPtr
	Private theFirstModelPanelHandle As IntPtr
	Private theFirstOptionsPanelHandle As IntPtr
	' Second viewer
	Private theSecondHlmvAppWindowHandle As IntPtr
	Private theSecondModelPanelHandle As IntPtr
	Private theSecondOptionsPanelHandle As IntPtr

	Private theHlmvAppWindowHandle As IntPtr
	Private theModelPanelHandle As IntPtr
	Private theOptionsPanelHandle As IntPtr

#End Region

End Class
