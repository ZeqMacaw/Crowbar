Imports System.ComponentModel
Imports System.IO
Imports System.Net

Public Class UpdateUserControl

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		Me.theUpdater = New Updater()
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		Me.DownloadFolderTextBox.DataBindings.Add("Text", TheApp.Settings, "UpdateDownloadPath", False, DataSourceUpdateMode.OnValidation)

		Me.UpdateToNewPathCheckBox.DataBindings.Add("Checked", TheApp.Settings, "UpdateUpdateToNewPathIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.UpdateFolderTextBox.DataBindings.Add("Text", TheApp.Settings, "UpdateUpdateDownloadPath", False, DataSourceUpdateMode.OnValidation)
		Me.UpdateCopySettingsCheckBox.DataBindings.Add("Checked", TheApp.Settings, "UpdateCopySettingsIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		AddHandler Me.DownloadFolderTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		AddHandler Me.UpdateFolderTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName

		Me.CurrentVersionLabel.Text = "Current Version: " + My.Application.Info.Version.ToString(2)
	End Sub

	Private Sub Free()
		RemoveHandler Me.DownloadFolderTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		RemoveHandler Me.UpdateFolderTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName

		Me.DownloadFolderTextBox.DataBindings.Clear()

		Me.UpdateToNewPathCheckBox.DataBindings.Clear()
		Me.UpdateFolderTextBox.DataBindings.Clear()
		Me.UpdateCopySettingsCheckBox.DataBindings.Clear()
	End Sub

#End Region

#Region "Methods"

	Public Sub CheckForUpdate()
		Me.CheckForUpdateTextBox.Text = "Checking for update..."
		Me.UpdateCommandWidgets(True)
		Me.CancelCheckButton.Enabled = True
		Me.theUpdater.CheckForUpdate(AddressOf CheckForUpdate_ProgressChanged, AddressOf CheckForUpdate_RunWorkerCompleted)
	End Sub

#End Region

#Region "Events"

	Public Delegate Sub UpdateAvailableEventHandler(ByVal sender As Object, ByVal e As UpdateAvailableEventArgs)
	Public Event UpdateAvailable As UpdateAvailableEventHandler
	Protected Sub NotifyUpdateAvailable(ByVal updateIsAvailable As Boolean)
		RaiseEvent UpdateAvailable(Me, New UpdateAvailableEventArgs(updateIsAvailable))
	End Sub

#End Region

#Region "Widget Event Handlers"

	Private Sub UpdateUserControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		'NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.DownloadFolderTextBox, Me.BrowseForDownloadFolderButton)
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.DownloadProgressBarEx, Me.CancelDownloadButton)
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.UpdateFolderTextBox, Me.BrowseForUpdateFolderButton)
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.UpdateProgressBarEx, Me.CancelUpdateButton)

		If Not Me.DesignMode Then
			Me.Init()
		End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub CheckForUpdateButton_Click(sender As Object, e As EventArgs) Handles CheckForUpdateButton.Click
		Me.CheckForUpdate()
	End Sub

	Private Sub DownloadFolderTextBox_DragDrop(sender As Object, e As DragEventArgs) Handles DownloadFolderTextBox.DragDrop
		Dim pathFileNames() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
		Dim pathFileName As String = pathFileNames(0)
		If Directory.Exists(pathFileName) Then
			TheApp.Settings.UpdateDownloadPath = pathFileName
		End If
	End Sub

	Private Sub DownloadFolderTextBox_DragEnter(sender As Object, e As DragEventArgs) Handles DownloadFolderTextBox.DragEnter
		If e.Data.GetDataPresent(DataFormats.FileDrop) Then
			e.Effect = DragDropEffects.Copy
		End If
	End Sub

	Private Sub BrowseForDownloadFolderButton_Click(sender As Object, e As EventArgs) Handles BrowseForDownloadFolderButton.Click
		Me.BrowseForDownloadPath()
	End Sub

	Private Sub CancelCheckButton_Click(sender As Object, e As EventArgs) Handles CancelCheckButton.Click
		Me.CancelCheckForUpdate()
	End Sub

	Private Sub DownloadButton_Click(sender As Object, e As EventArgs) Handles DownloadButton.Click
		Me.Download()
	End Sub

	Private Sub CancelDownloadButton_Click(sender As Object, e As EventArgs) Handles CancelDownloadButton.Click
		Me.CancelDownload()
	End Sub

	Private Sub GotoDownloadFileButton_Click(sender As Object, e As EventArgs) Handles GotoDownloadFileButton.Click
		Me.GotoDownloadFile()
	End Sub

	Private Sub UpdateFolderTextBox_DragDrop(sender As Object, e As DragEventArgs) Handles UpdateFolderTextBox.DragDrop
		Dim pathFileNames() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
		Dim pathFileName As String = pathFileNames(0)
		If Directory.Exists(pathFileName) Then
			TheApp.Settings.UpdateUpdateDownloadPath = pathFileName
		End If
	End Sub

	Private Sub UpdateFolderTextBox_DragEnter(sender As Object, e As DragEventArgs) Handles UpdateFolderTextBox.DragEnter
		If e.Data.GetDataPresent(DataFormats.FileDrop) Then
			e.Effect = DragDropEffects.Copy
		End If
	End Sub

	Private Sub BrowseForUpdateFolderButton_Click(sender As Object, e As EventArgs) Handles BrowseForUpdateFolderButton.Click
		Me.BrowseForUpdateDownloadPath()
	End Sub

	Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
		Me.UpdateApp()
	End Sub

	Private Sub CancelUpdateButton_Click(sender As Object, e As EventArgs) Handles CancelUpdateButton.Click
		Me.CancelUpdate()
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub CheckForUpdate_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 0 Then
			' Changelog
			Me.ChangelogTextBox.Text = CType(e.UserState, String)
		End If
	End Sub

	Private Sub CheckForUpdate_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If e.Cancelled Then
			Me.CheckForUpdateTextBox.Text = "Check canceled."
		Else
			Dim outputInfo As Updater.StatusOutputInfo = Nothing
			outputInfo = CType(e.Result, Updater.StatusOutputInfo)
			Me.CheckForUpdateTextBox.Text = CType(outputInfo.StatusMessage, String)
			NotifyUpdateAvailable(outputInfo.UpdateIsAvailable)

			If outputInfo.UpdateIsEnabled AndAlso Not outputInfo.UpdateIsAvailable Then
				Me.theCurrentProgressBar.Text = "No available update."
				Me.theCurrentProgressBar.Value = 0
			ElseIf outputInfo.DownloadIsEnabled AndAlso Not (outputInfo.UpdateIsEnabled AndAlso Not outputInfo.UpdateIsAvailable) Then
				Me.theCurrentProgressBar.Text = "Starting download..."
				Me.theCurrentProgressBar.Value = 0
			End If
		End If

		Me.UpdateCommandWidgets(False)
		Me.CancelCheckButton.Enabled = False
	End Sub

	Private Sub Download_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
		Me.UpdateProgressBar(Me.theCurrentProgressBar, e.BytesReceived, e.TotalBytesToReceive)
	End Sub

	Private Sub Download_DownloadFileCompleted(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
		Dim pathFileName As String = CType(e.UserState, String)
		If e.Cancelled Then
			Me.theCurrentProgressBar.Text = "Download failed."
			Me.theCurrentProgressBar.Value = 0

			If File.Exists(pathFileName) Then
				Try
					File.Delete(pathFileName)
				Catch ex As Exception
					Me.theCurrentProgressBar.Text += "WARNING: Problem deleting incomplete downloaded file: """ + Path.GetFileName(pathFileName) + """"
				End Try
			End If
		Else
			If File.Exists(pathFileName) Then
				Me.theCurrentProgressBar.Text = "Downloaded file: """ + Path.GetFileName(pathFileName) + """   " + Me.theCurrentProgressBar.Text
				Me.GotoDownloadFileButton.Enabled = True
				Me.theDownloadedPathFileName = pathFileName
			Else
				Me.theCurrentProgressBar.Text = "Download failed."
			End If
		End If

		Dim client As WebClient = CType(sender, WebClient)
		RemoveHandler client.DownloadProgressChanged, AddressOf Me.Download_DownloadProgressChanged
		RemoveHandler client.DownloadFileCompleted, AddressOf Me.Download_DownloadFileCompleted
		client = Nothing

		Me.UpdateCommandWidgets(False)
		Me.CancelDownloadButton.Enabled = False
	End Sub

	Private Sub Update_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 0 Then
		ElseIf e.ProgressPercentage = 1 Then
		End If
	End Sub

	Private Sub Update_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If e.Cancelled Then
		Else
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub CancelCheckForUpdate()
		Me.theUpdater.CancelCheckForUpdate()
	End Sub

	Private Sub BrowseForDownloadPath()
		'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
		'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
		Dim outputPathWdw As New OpenFileDialog()

		outputPathWdw.Title = "Open the folder you want as Download Folder"
		outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.UpdateDownloadPath)
		If outputPathWdw.InitialDirectory = "" Then
			outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		End If
		outputPathWdw.FileName = "[Folder Selection]"
		outputPathWdw.AddExtension = False
		outputPathWdw.CheckFileExists = False
		outputPathWdw.Multiselect = False
		outputPathWdw.ValidateNames = False

		If outputPathWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			TheApp.Settings.UpdateDownloadPath = FileManager.GetPath(outputPathWdw.FileName)
		End If
	End Sub

	Private Sub Download()
		If FileManager.PathExistsAfterTryToCreate(TheApp.Settings.UpdateDownloadPath) Then
			Me.DownloadProgressBarEx.Text = "Checking for update..."
			Me.DownloadProgressBarEx.Value = 0
			Me.theCurrentProgressBar = Me.DownloadProgressBarEx

			Me.UpdateCommandWidgets(True)
			Me.CancelDownloadButton.Enabled = True
			Me.GotoDownloadFileButton.Enabled = False
			Me.theUpdater.Download(AddressOf CheckForUpdate_ProgressChanged, AddressOf CheckForUpdate_RunWorkerCompleted, AddressOf Download_DownloadProgressChanged, AddressOf Download_DownloadFileCompleted, TheApp.Settings.UpdateDownloadPath)
		Else
			Me.DownloadProgressBarEx.Text = "Download failed to start because folder does not exist"
			Me.DownloadProgressBarEx.Value = 0
		End If
	End Sub

	Private Sub CancelDownload()
		Me.theUpdater.CancelDownload()
	End Sub

	Public Sub GotoDownloadFile()
		FileManager.OpenWindowsExplorer(Me.theDownloadedPathFileName)
	End Sub

	Private Sub BrowseForUpdateDownloadPath()
		'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
		'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
		Dim outputPathWdw As New OpenFileDialog()

		outputPathWdw.Title = "Open the folder you want as Update Download Folder"
		outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.UpdateUpdateDownloadPath)
		If outputPathWdw.InitialDirectory = "" Then
			outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		End If
		outputPathWdw.FileName = "[Folder Selection]"
		outputPathWdw.AddExtension = False
		outputPathWdw.CheckFileExists = False
		outputPathWdw.Multiselect = False
		outputPathWdw.ValidateNames = False

		If outputPathWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			TheApp.Settings.UpdateUpdateDownloadPath = FileManager.GetPath(outputPathWdw.FileName)
		End If
	End Sub

	' Named UpdateApp to avoid confusion with existing UserControl.Update().
	Private Sub UpdateApp()
		Me.UpdateProgressBarEx.Text = "Checking for update..."
		Me.UpdateProgressBarEx.Value = 0
		Me.theCurrentProgressBar = Me.UpdateProgressBarEx

		Me.UpdateCommandWidgets(True)
		Me.CancelUpdateButton.Enabled = True
		Dim localPath As String
		If TheApp.Settings.UpdateUpdateToNewPathIsChecked Then
			localPath = TheApp.Settings.UpdateUpdateDownloadPath
		Else
			localPath = TheApp.GetCustomDataPath()
		End If
		Me.theUpdater.Update(AddressOf CheckForUpdate_ProgressChanged, AddressOf CheckForUpdate_RunWorkerCompleted, AddressOf Download_DownloadProgressChanged, AddressOf Download_DownloadFileCompleted, localPath, AddressOf Update_ProgressChanged, AddressOf Update_RunWorkerCompleted)
	End Sub

	Private Sub CancelUpdate()
		Me.theUpdater.CancelUpdate()
	End Sub

	Private Sub UpdateCommandWidgets(ByVal taskIsRunning As Boolean)
		Me.CheckForUpdateButton.Enabled = Not taskIsRunning
		'Me.CancelCheckButton.Enabled = taskIsRunning

		Me.BrowseForDownloadFolderButton.Enabled = Not taskIsRunning
		Me.DownloadButton.Enabled = Not taskIsRunning
		'Me.CancelDownloadButton.Enabled = taskIsRunning

		Me.BrowseForUpdateFolderButton.Enabled = Not taskIsRunning
		Me.UpdateButton.Enabled = Not taskIsRunning
		'Me.CancelUpdateButton.Enabled = taskIsRunning
	End Sub

	Private Sub UpdateProgressBar(ByVal aProgressBar As ProgressBarEx, ByVal bytesReceived As Long, ByVal totalBytesToReceive As Long)
		Dim progressPercentage As Integer = CInt(bytesReceived * aProgressBar.Maximum / totalBytesToReceive)
		aProgressBar.Text = bytesReceived.ToString("N0") + " / " + totalBytesToReceive.ToString("N0") + " bytes   " + progressPercentage.ToString() + " %"
		aProgressBar.Value = progressPercentage
	End Sub

#End Region

#Region "Data"

	Private theCurrentProgressBar As ProgressBarEx
	Private theUpdater As Updater
	Private theDownloadedPathFileName As String

#End Region

	Public Class UpdateAvailableEventArgs
		Inherits System.EventArgs

		Public Sub New(ByVal updateIsAvailable As Boolean)
			MyBase.New()

			Me.theUpdateIsAvailable = updateIsAvailable
		End Sub

		Public ReadOnly Property UpdateIsAvailable As Boolean
			Get
				Return Me.theUpdateIsAvailable
			End Get
		End Property

		Private theUpdateIsAvailable As Boolean

	End Class

End Class
