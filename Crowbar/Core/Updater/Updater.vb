Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Xml

Public Class Updater

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

	End Sub

#End Region

#Region "CheckForUpdate"

	Public Class StatusOutputInfo
		Public StatusMessage As String
		Public DownloadIsEnabled As Boolean
	End Class

	Public Sub CheckForUpdate(ByVal given_ProgressChanged As ProgressChangedEventHandler, ByVal given_RunWorkerCompleted As RunWorkerCompletedEventHandler)
		Me.theDownloadTaskIsEnabled = False
		Me.theCheckForUpdateBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(Me.theCheckForUpdateBackgroundWorker, AddressOf Me.CheckForUpdate_DoWork, given_ProgressChanged, given_RunWorkerCompleted, Nothing)
	End Sub

	Public Sub CancelCheckForUpdate()
		If Me.theCheckForUpdateBackgroundWorker IsNot Nothing AndAlso Me.theCheckForUpdateBackgroundWorker.IsBusy Then
			Me.theCheckForUpdateBackgroundWorker.CancelAsync()
		End If
	End Sub

	Private theCheckForUpdateBackgroundWorker As BackgroundWorkerEx

	'NOTE: This is run in a background thread.
	Private Sub CheckForUpdate_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)

		Dim appVersion As Version = Nothing
		Dim fileSize As ULong = 0

		'FROM: https://www.codeproject.com/Questions/1255767/Could-not-create-SSL-TLS-secure-channel
		'FROM: https://blogs.perficient.com/2016/04/28/tsl-1-2-and-net-support/
		'      .NET 4.0 does not support TLS 1.2 and does not have an SecurityProtocolType Enum value for TLS1.2.
		'      To use in .NET 4.0, use the number: CType(3072, SecurityProtocolType)
		'      [Not sure] Need .NET 4.5 (or above) installed.
		'NOTE: GitHub API requires this.
		ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)

		' Get data from latest release page via GitHub API.
		'FROM: https://developer.github.com/v3/repos/releases
		'      All API access is over HTTPS, and accessed from https://api.github.com. All data is sent and received as JSON.
		'FROM: https://developer.github.com/v3/repos/releases/#get-the-latest-release
		'      Get the latest release: https://api.github.com/repos/ZeqMacaw/Crowbar/releases/latest
		Dim request As HttpWebRequest = CType(WebRequest.Create("https://api.github.com/repos/ZeqMacaw/Crowbar/releases/latest"), HttpWebRequest)
		request.Method = "GET"
		'NOTE: GitHub API suggests using something like this.
		request.UserAgent = "ZeqMacaw_Crowbar"
		Dim response As HttpWebResponse = Nothing
		Dim dataStream As Stream
		Dim reader As StreamReader = Nothing
		Dim remoteFileLink As String = ""
		Dim localFileName As String = ""
		Try
			response = CType(request.GetResponse(), HttpWebResponse)
			dataStream = response.GetResponseStream()
			reader = New StreamReader(dataStream)
			Dim responseFromServer As String = reader.ReadToEnd()

			Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
			Dim root As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(responseFromServer)

			Dim appNameVersion As String = CType(root("name"), String)
			'NOTE: Must append ".0.0" to version so that Version comparisons are correct.
			Dim appVersionText As String = appNameVersion.Replace("Crowbar ", "") + ".0.0"
			appVersion = New Version(appVersionText)

			'Dim appVersionIsNewer As Boolean = appVersion > My.Application.Info.Version
			'Dim appVersionIsOlder As Boolean = appVersion < My.Application.Info.Version
			'Dim appVersionIsEqual As Boolean = appVersion = My.Application.Info.Version

			bw.ReportProgress(0, appNameVersion + vbCrLf + CType(root("body"), String))

			Dim assets As ArrayList = CType(root("assets"), ArrayList)
			Dim asset As Dictionary(Of String, Object) = CType(assets(0), Dictionary(Of String, Object))
			Me.theRemoteFileLink = CType(asset("browser_download_url"), String)
			Me.theLocalFileName = CType(asset("name"), String)
			fileSize = CType(asset("size"), ULong)
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If reader IsNot Nothing Then
				reader.Close()
			End If
			If response IsNot Nothing Then
				response.Close()
			End If

			Dim updateCheckStatusMessage As String
			If appVersion Is Nothing Then
				updateCheckStatusMessage = "Unable to get update info. Please try again later.   "
			ElseIf appVersion = My.Application.Info.Version Then
				updateCheckStatusMessage = "Crowbar is up to date.   "
			ElseIf appVersion > My.Application.Info.Version Then
				updateCheckStatusMessage = "Update to version " + appVersion.ToString(2) + " available.   Size: " + MathModule.ByteUnitsConversion(fileSize) + "   "
			Else
				'NOTE: Should not get here if versioning is done correctly.
				updateCheckStatusMessage = ""
			End If
			Dim now As DateTime = DateTime.Now()
			Dim lastCheckedMessage As String = "Last checked: " + now.ToLongDateString() + " " + now.ToShortTimeString()

			Dim outputInfo As New Updater.StatusOutputInfo()
			outputInfo.StatusMessage = updateCheckStatusMessage + lastCheckedMessage
			outputInfo.DownloadIsEnabled = Me.theDownloadTaskIsEnabled
			e.Result = outputInfo
		End Try
	End Sub

#End Region

#Region "Comments"

	''NOTE: This is run in a background thread.
	'Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
	'	Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)
	'	Dim outputInfo As String = ""

	'	'TODO: In background worker, download release web page and changelog.
	'	'TODO: Show info.
	'	'TODO: In Download_RunWorkerCompleted, run the background worker for downloading the app file.
	'	'TODO: In DownloadAppFile_RunWorkerCompleted, run the new app.
	'	'TODO: Copy SevenZr.exe and CrowbarLauncher.exe from resources into appdata folder.
	'	'TODO: Decompress, via 7zr.exe, Crowbar.7z file into appdata folder.
	'	'TODO: Run CrowbarLauncher.exe, which moves new Crowbar.exe to where current Crowbar.exe is and then runs the new Crowbar.exe.
	'	'TODO: Crowbar when opened, deletes CrowbarLauncher.exe if it exists.
	'	'InstalledVersion = mainAssembly.GetName().Version;
	'	'args.IsUpdateAvailable = CurrentVersion > InstalledVersion;

	'	e.Result = outputInfo
	'End Sub

	'Private Sub Worker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
	'	If e.ProgressPercentage = 0 Then
	'	ElseIf e.ProgressPercentage = 1 Then
	'	End If
	'End Sub

	'Private Sub Worker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
	'	If e.Cancelled Then
	'	Else
	'	End If
	'End Sub

#End Region

#Region "Download"

	Public Sub Download(ByVal checkForUpdate_ProgressChanged As ProgressChangedEventHandler, ByVal checkForUpdate_RunWorkerCompleted As RunWorkerCompletedEventHandler, ByVal download_DownloadProgressChanged As DownloadProgressChangedEventHandler, ByVal download_DownloadFileCompleted As AsyncCompletedEventHandler, ByVal localPath As String)
		Me.theDownloadProgressChangedHandler = download_DownloadProgressChanged
		Me.theDownloadFileCompletedHandler = download_DownloadFileCompleted
		Me.theLocalPath = localPath
		Me.theDownloadTaskIsEnabled = True
		Me.theCheckForUpdate_RunWorkerCompleted = checkForUpdate_RunWorkerCompleted
		Me.theCheckForUpdateBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(Me.theCheckForUpdateBackgroundWorker, AddressOf Me.CheckForUpdate_DoWork, checkForUpdate_ProgressChanged, AddressOf Me.CheckForUpdate_RunWorkerCompleted, Nothing)
	End Sub

	Public Sub CancelDownload()
		If Me.theWebClient IsNot Nothing Then
			Me.theWebClient.CancelAsync()
		End If
	End Sub

	Private Sub DownloadAfterCheckForUpdate()
		Dim localPathFileName As String = Path.Combine(Me.theLocalPath, Me.theLocalFileName)
		If File.Exists(localPathFileName) Then
			'TODO: rename the download
		End If

		'Dim uri As Uri = New Uri("https://github.com/ZeqMacaw/test/blob/master/update.xml")
		Dim remoteFileUri As New Uri(Me.theRemoteFileLink)

		Me.theWebClient = New WebClient()
		AddHandler Me.theWebClient.DownloadProgressChanged, Me.theDownloadProgressChangedHandler
		AddHandler Me.theWebClient.DownloadFileCompleted, Me.theDownloadFileCompletedHandler
		Me.theWebClient.DownloadFileAsync(remoteFileUri, localPathFileName, localPathFileName)
	End Sub

#End Region

#Region "Update"

	Public Sub Update(ByVal checkForUpdate_ProgressChanged As ProgressChangedEventHandler, ByVal checkForUpdate_RunWorkerCompleted As RunWorkerCompletedEventHandler, ByVal given_ProgressChanged As ProgressChangedEventHandler, ByVal given_RunWorkerCompleted As RunWorkerCompletedEventHandler)
		Me.theDownloadTaskIsEnabled = True
		'Me.CheckForUpdate(checkForUpdate_ProgressChanged, checkForUpdate_RunWorkerCompleted)

		Me.theUpdateBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(Me.theUpdateBackgroundWorker, AddressOf Me.Update_DoWork, given_ProgressChanged, given_RunWorkerCompleted, Nothing)
	End Sub

	Private theUpdateBackgroundWorker As BackgroundWorkerEx

	'NOTE: This is run in a background thread.
	Private Sub Update_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		' Copy SevenZr.exe and CrowbarLauncher.exe from resources into appdata folder.
		TheApp.WriteUpdaterFiles()

		Dim currentFolder As String
		currentFolder = Directory.GetCurrentDirectory()
		Dim appDataPath As String = FileManager.GetPath(TheApp.SevenZrExePathFileName)
		Directory.SetCurrentDirectory(appDataPath)

		'TODO: Decompress, via 7zr.exe, Crowbar.7z file into appdata folder.
		Dim compressedNewCrowbarFileName As String = "Crowbar_2019-10-16_0.64.7z"
		Dim sevenZrExeProcess As New Process()
		Try
			sevenZrExeProcess.StartInfo.UseShellExecute = False
			'NOTE: From Microsoft website: 
			'      On Windows Vista and earlier versions of the Windows operating system, 
			'      the length of the arguments added to the length of the full path to the process must be less than 2080. 
			'      On Windows 7 and later versions, the length must be less than 32699. 
			sevenZrExeProcess.StartInfo.FileName = TheApp.SevenZrExePathFileName
			sevenZrExeProcess.StartInfo.Arguments = "x """ + compressedNewCrowbarFileName + """"
#If DEBUG Then
			sevenZrExeProcess.StartInfo.CreateNoWindow = False
#Else
						lzmaExeProcess.StartInfo.CreateNoWindow = True
#End If
			sevenZrExeProcess.Start()
			sevenZrExeProcess.WaitForExit()
		Catch ex As Exception
			Throw New System.Exception("Crowbar tried to decompress the file """ + compressedNewCrowbarFileName + """ but Windows gave this message: " + ex.Message)
		Finally
			sevenZrExeProcess.Close()
		End Try

		Dim newCrowbarPathFileName As String = Path.Combine(appDataPath, "Crowbar.exe")
		If File.Exists(newCrowbarPathFileName) Then
			' Run CrowbarLauncher.exe and exit Crowbar.
			Dim crowbarLauncherExeProcess As New Process()
			Dim startupPath As String = Application.StartupPath
			Dim currentCrowbarExePathFileName As String = Path.Combine(startupPath, "Crowbar.exe")
			Try
				crowbarLauncherExeProcess.StartInfo.UseShellExecute = False
				'NOTE: From Microsoft website: 
				'      On Windows Vista and earlier versions of the Windows operating system, 
				'      the length of the arguments added to the length of the full path to the process must be less than 2080. 
				'      On Windows 7 and later versions, the length must be less than 32699. 
				crowbarLauncherExeProcess.StartInfo.FileName = TheApp.CrowbarLauncherExePathFileName
				crowbarLauncherExeProcess.StartInfo.Arguments = Process.GetCurrentProcess().Id.ToString() + " """ + currentCrowbarExePathFileName + """"
#If DEBUG Then
				crowbarLauncherExeProcess.StartInfo.CreateNoWindow = False
#Else
				lzmaExeProcess.StartInfo.CreateNoWindow = True
#End If
				crowbarLauncherExeProcess.Start()
				Application.Exit()
			Catch ex As Exception
				Dim debug As Integer = 4242
				'Throw New System.Exception("Crowbar tried to compress the file """ + gmaPathFileName + """ to """ + processedPathFileName + """ but Windows gave this message: " + ex.Message)
			Finally
			End Try
		End If

		Directory.SetCurrentDirectory(currentFolder)
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub CheckForUpdate_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If e.Cancelled Then
			'Me.CheckForUpdateTextBox.Text = "Check canceled."
		Else
			If Me.theDownloadTaskIsEnabled Then
				Me.DownloadAfterCheckForUpdate()
			End If
		End If
		Me.theCheckForUpdate_RunWorkerCompleted(Me, e)
	End Sub

#End Region

#Region "Events"

#End Region

#Region "Data"

	Private theWebClient As WebClient
	Private theCheckForUpdate_RunWorkerCompleted As RunWorkerCompletedEventHandler
	Private theDownloadProgressChangedHandler As DownloadProgressChangedEventHandler
	Private theDownloadFileCompletedHandler As AsyncCompletedEventHandler
	Private theDownloadTaskIsEnabled As Boolean
	Private theRemoteFileLink As String
	Private theLocalPath As String
	Private theLocalFileName As String

#End Region

End Class
