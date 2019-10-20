Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Xml

Public Class Updater
	Implements INotifyPropertyChanged

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

		Me.theUpdateCheckMessage = "[not checked yet]"
		Me.theChangelog = ""

		Me.isRunning = False
	End Sub

#End Region

#Region "Init and Free"

#End Region

#Region "Properties"

	Public Property UpdateCheckMessage() As String
		Get
			Return Me.theUpdateCheckMessage
		End Get
		Set(ByVal value As String)
			If Me.theUpdateCheckMessage <> value Then
				Me.theUpdateCheckMessage = value
				NotifyPropertyChanged("UpdateCheckMessage")
			End If
		End Set
	End Property

	Public Property Changelog() As String
		Get
			Return Me.theChangelog
		End Get
		Set(ByVal value As String)
			If Me.theChangelog <> value Then
				Me.theChangelog = value
				NotifyPropertyChanged("Changelog")
			End If
		End Set
	End Property

#End Region

#Region "Methods"

	Public Sub Run()
		If Not Me.isRunning Then
			Me.isRunning = True

			Dim worker As BackgroundWorkerEx = Nothing
			Dim inputInfo As String = ""
			worker = BackgroundWorkerEx.RunBackgroundWorker(worker, AddressOf Me.Worker_DoWork, AddressOf Me.Worker_ProgressChanged, AddressOf Me.Worker_RunWorkerCompleted, inputInfo)

		End If
	End Sub

	Public Sub CheckForUpdate()
		Me.isRunning = True
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

			'NOTE: Call the property so the NotifyPropertyChanged event is raised.
			Me.Changelog = appNameVersion + vbCrLf + CType(root("body"), String)

			Dim assets As ArrayList = CType(root("assets"), ArrayList)
			Dim asset As Dictionary(Of String, Object) = CType(assets(0), Dictionary(Of String, Object))
			Dim FileLink As String = CType(asset("browser_download_url"), String)
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
			'NOTE: Call the property so the NotifyPropertyChanged event is raised.
			Me.UpdateCheckMessage = updateCheckStatusMessage + lastCheckedMessage
			Me.isRunning = False
		End Try
	End Sub

	Public Sub DownloadNewVersion()
		Dim uri As Uri = New Uri("https://github.com/ZeqMacaw/test/blob/master/update.xml")
		Dim appDataPath As String = FileManager.GetPath(TheApp.SevenZrExePathFileName)
		Dim outputPathFileName As String = Path.Combine(appDataPath, "update.xml")

		'Me.LogTextBox.AppendText("Downloading workshop item as: """ + outputPathFileName + """" + vbCrLf)

		'Me.DownloadButton.Enabled = False
		'Me.CancelDownloadButton.Enabled = True

		Me.theWebClient = New WebClient()
		AddHandler Me.theWebClient.DownloadProgressChanged, AddressOf WebClient_DownloadProgressChanged
		AddHandler Me.theWebClient.DownloadFileCompleted, AddressOf WebClient_DownloadFileCompleted
		Me.theWebClient.DownloadFileAsync(uri, outputPathFileName, outputPathFileName)
	End Sub

	Public Sub DecompressAndRunNewVersion()
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

	Private Sub WebClient_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
		''Me.DownloadProgressBar.Text = e.BytesReceived.ToString("N0") + " / " + e.TotalBytesToReceive.ToString("N0") + " bytes   " + e.ProgressPercentage.ToString() + " %"
		''Me.DownloadProgressBar.Value = CInt(e.BytesReceived * Me.DownloadProgressBar.Maximum / e.TotalBytesToReceive)
		'Me.UpdateProgressBar(e.BytesReceived, e.TotalBytesToReceive)
	End Sub

	Private Sub WebClient_DownloadFileCompleted(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
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
				Me.ProcessUpdateXml()
				'Me.LogTextBox.AppendText("Download complete." + vbCrLf + "Downloaded file: """ + pathFileName + """" + vbCrLf)
				'Me.DownloadedItemTextBox.Text = pathFileName
				'Else
				'	Me.LogTextBox.AppendText("Download failed." + vbCrLf)
			End If
		End If

		RemoveHandler Me.theWebClient.DownloadProgressChanged, AddressOf Me.WebClient_DownloadProgressChanged
		RemoveHandler Me.theWebClient.DownloadFileCompleted, AddressOf Me.WebClient_DownloadFileCompleted
		Me.theWebClient = Nothing

		'Me.DownloadButton.Enabled = True
		'Me.CancelDownloadButton.Enabled = False
	End Sub

	'NOTE: This is run in a background thread.
	Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)
		Dim outputInfo As String = ""

		'TODO: In background worker, download release web page and changelog.
		'TODO: Show info.
		'TODO: In Download_RunWorkerCompleted, run the background worker for downloading the app file.
		'TODO: In DownloadAppFile_RunWorkerCompleted, run the new app.
		'TODO: Copy SevenZr.exe and CrowbarLauncher.exe from resources into appdata folder.
		'TODO: Decompress, via 7zr.exe, Crowbar.7z file into appdata folder.
		'TODO: Run CrowbarLauncher.exe, which moves new Crowbar.exe to where current Crowbar.exe is and then runs the new Crowbar.exe.
		'TODO: Crowbar when opened, deletes CrowbarLauncher.exe if it exists.
		'InstalledVersion = mainAssembly.GetName().Version;
		'args.IsUpdateAvailable = CurrentVersion > InstalledVersion;

		e.Result = outputInfo
	End Sub

	Private Sub Worker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 0 Then
		ElseIf e.ProgressPercentage = 1 Then
		End If
	End Sub

	Private Sub Worker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If e.Cancelled Then
		Else
		End If

		Me.isRunning = False
	End Sub

#End Region

#Region "Events"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#End Region

#Region "Private Methods"

	Private Sub ProcessUpdateXml()

	End Sub

	Private Sub CancelDownload()
		If Me.theWebClient IsNot Nothing Then
			Me.theWebClient.CancelAsync()
		End If
	End Sub

	Protected Sub NotifyPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

#End Region

#Region "Data"

	Private theUpdateCheckMessage As String
	Private theChangelog As String

	Private isRunning As Boolean
	Private theWebClient As WebClient

#End Region

End Class
