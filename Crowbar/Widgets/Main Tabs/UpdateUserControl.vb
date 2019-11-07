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
		Me.UpdateFolderTextBox.DataBindings.Add("Text", TheApp.Settings, "UpdateUpdateDownloadPath", False, DataSourceUpdateMode.OnValidation)

		AddHandler Me.DownloadFolderTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		AddHandler Me.UpdateFolderTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
	End Sub

	Private Sub Free()
		RemoveHandler Me.DownloadFolderTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		RemoveHandler Me.UpdateFolderTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
	End Sub

#End Region

#Region "Widget Event Handlers"

	Private Sub UpdateUserControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		If Not Me.DesignMode Then
			Me.Init()
		End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub CheckForUpdateButton_Click(sender As Object, e As EventArgs) Handles CheckForUpdateButton.Click
		Me.CheckForUpdate()
	End Sub

	Private Sub BrowseForDownloadFolderButton_Click(sender As Object, e As EventArgs) Handles BrowseForDownloadFolderButton.Click
		Me.BrowseForDownloadPath()
	End Sub

	Private Sub DownloadButton_Click(sender As Object, e As EventArgs) Handles DownloadButton.Click
		Me.Download()
	End Sub

	Private Sub CancelDownloadButton_Click(sender As Object, e As EventArgs) Handles CancelDownloadButton.Click
		Me.CancelDownload()
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

			If outputInfo.DownloadIsEnabled Then
				Me.DownloadProgressBarEx.Text = "Starting download..."
				Me.DownloadProgressBarEx.Value = 0
			End If
		End If

		Me.UpdateCommandWidgets(False)
		Me.CancelCheckButton.Enabled = False
	End Sub

	Private Sub Download_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
		Me.UpdateProgressBar(Me.DownloadProgressBarEx, e.BytesReceived, e.TotalBytesToReceive)
	End Sub

	Private Sub Download_DownloadFileCompleted(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
		If e.Cancelled Then
			'	Me.LogTextBox.AppendText("Download cancelled." + vbCrLf)
			'	Me.DownloadProgressBar.Text = ""
			'	Me.DownloadProgressBar.Value = 0

			'	Dim pathFileName As String = CType(e.UserState, String)
			'	If File.Exists(pathFileName) Then
			'		Try
			'			File.Delete(pathFileName)
			'		Catch ex As Exception
			'			Me.LogTextBox.AppendText("WARNING: Problem deleting incomplete downloaded file." + vbCrLf)
			'		End Try
			'	End If
		Else
			Dim pathFileName As String = CType(e.UserState, String)
			If File.Exists(pathFileName) Then
				'Me.ProcessUpdateXml()
				'Me.LogTextBox.AppendText("Download complete." + vbCrLf + "Downloaded file: """ + pathFileName + """" + vbCrLf)
				'Me.DownloadedItemTextBox.Text = pathFileName
				'Else
				'	Me.LogTextBox.AppendText("Download failed." + vbCrLf)
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

	Private Sub CheckForUpdate()
		Me.UpdateCommandWidgets(True)
		Me.CancelCheckButton.Enabled = True
		Me.theUpdater.CheckForUpdate(AddressOf CheckForUpdate_ProgressChanged, AddressOf CheckForUpdate_RunWorkerCompleted)
	End Sub

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
		If FileManager.PathExistsAfterTryToCreate(Me.DownloadFolderTextBox.Text) Then
			Me.DownloadProgressBarEx.Text = "Checking for update..."
			Me.DownloadProgressBarEx.Value = 0

			Me.UpdateCommandWidgets(True)
			Me.CancelDownloadButton.Enabled = True
			Me.theUpdater.Download(AddressOf CheckForUpdate_ProgressChanged, AddressOf CheckForUpdate_RunWorkerCompleted, AddressOf Download_DownloadProgressChanged, AddressOf Download_DownloadFileCompleted, Me.DownloadFolderTextBox.Text)
		Else
			Me.DownloadProgressBarEx.Text = "Download failed to start because folder does not exist"
			Me.DownloadProgressBarEx.Value = 0
		End If
	End Sub

	Private Sub CancelDownload()
		Me.theUpdater.CancelDownload()
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
		Me.UpdateCommandWidgets(True)
		Me.CancelUpdateButton.Enabled = True
		Me.theUpdater.Update(AddressOf CheckForUpdate_ProgressChanged, AddressOf CheckForUpdate_RunWorkerCompleted, AddressOf Update_ProgressChanged, AddressOf Update_RunWorkerCompleted)
	End Sub

	Private Sub CancelUpdate()
		'TODO: CancelUpdate()
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

	Private theUpdater As Updater

#End Region

End Class
