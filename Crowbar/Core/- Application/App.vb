Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Text

Public Class App
	Implements IDisposable

#Region "Create and Destroy"

	Public Sub New()
		Me.IsDisposed = False

		'NOTE: To use a particular culture's NumberFormat that doesn't change with user settings, 
		'      must use this constructor with False as second param.
		Me.theInternalCultureInfo = New CultureInfo("en-US", False)
		Me.theInternalNumberFormat = Me.theInternalCultureInfo.NumberFormat

		Me.theSmdFilesWritten = New List(Of String)()
	End Sub

#Region "IDisposable Support"

	Public Sub Dispose() Implements IDisposable.Dispose
		' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If Not Me.IsDisposed Then
			If disposing Then
				Me.Free()
			End If
			'NOTE: free shared unmanaged resources
		End If
		Me.IsDisposed = True
	End Sub

	'Protected Overrides Sub Finalize()
	'	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
	'	Dispose(False)
	'	MyBase.Finalize()
	'End Sub

#End Region

#End Region

#Region "Init and Free"

	Public Sub Init()
		Me.theAppPath = Application.StartupPath
		'NOTE: Needed for using DLLs placed in folder separate from main EXE file.
		Environment.SetEnvironmentVariable("path", Me.GetCustomDataPath(), EnvironmentVariableTarget.Process)
		Me.WriteRequiredFiles()
		Me.LoadAppSettings()

		If Me.Settings.SteamLibraryPaths.Count = 0 Then
			Dim libraryPath As New SteamLibraryPath()
			Me.Settings.SteamLibraryPaths.Add(libraryPath)
		End If

		Me.theUnpacker = New Unpacker()
		Me.theDecompiler = New Decompiler()
		Me.theCompiler = New Compiler()
		Me.thePacker = New Packer()
		'Me.theModelViewer = New Viewer()

		Dim documentsPath As String
		documentsPath = Path.Combine(Me.theAppPath, "Documents")
		AppConstants.HelpTutorialLink = Path.Combine(documentsPath, AppConstants.HelpTutorialLink)
		AppConstants.HelpContentsLink = Path.Combine(documentsPath, AppConstants.HelpContentsLink)
		AppConstants.HelpIndexLink = Path.Combine(documentsPath, AppConstants.HelpIndexLink)
		AppConstants.HelpTipsLink = Path.Combine(documentsPath, AppConstants.HelpTipsLink)
	End Sub

	Private Sub Free()
		If Me.theSettings IsNot Nothing Then
			Me.SaveAppSettings()
		End If
		'If Me.theCompiler IsNot Nothing Then
		'End If
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property Settings() As AppSettings
		Get
			Return Me.theSettings
		End Get
	End Property

	Public ReadOnly Property CommandLineOption_Settings_IsEnabled() As Boolean
		Get
			Return Me.theCommandLineOption_Settings_IsEnabled
		End Get
	End Property

	Public ReadOnly Property ErrorPathFileName() As String
		Get
			Return Path.Combine(Me.GetCustomDataPath(), Me.ErrorFileName)
		End Get
	End Property

	Public ReadOnly Property Unpacker() As Unpacker
		Get
			Return Me.theUnpacker
		End Get
	End Property

	Public ReadOnly Property Decompiler() As Decompiler
		Get
			Return Me.theDecompiler
		End Get
	End Property

	Public ReadOnly Property Compiler() As Compiler
		Get
			Return Me.theCompiler
		End Get
	End Property

	Public ReadOnly Property Packer() As Packer
		Get
			Return Me.thePacker
		End Get
	End Property

	'Public ReadOnly Property Viewer() As Viewer
	'	Get
	'		Return Me.theModelViewer
	'	End Get
	'End Property

	'Public Property ModelRelativePathFileName() As String
	'	Get
	'		Return Me.theModelRelativePathFileName
	'	End Get
	'	Set(ByVal value As String)
	'		Me.theModelRelativePathFileName = value
	'	End Set
	'End Property

	Public ReadOnly Property InternalCultureInfo() As CultureInfo
		Get
			Return Me.theInternalCultureInfo
		End Get
	End Property

	Public ReadOnly Property InternalNumberFormat() As NumberFormatInfo
		Get
			Return Me.theInternalNumberFormat
		End Get
	End Property

	Public Property SmdFileNames() As List(Of String)
		Get
			Return Me.theSmdFilesWritten
		End Get
		Set(ByVal value As List(Of String))
			Me.theSmdFilesWritten = value
		End Set
	End Property

#End Region

#Region "Methods"

	Public Function CommandLineValueIsAnAppSetting(ByVal commandLineValue As String) As Boolean
		Return commandLineValue.StartsWith(App.SettingsParameter)
	End Function

	Public Sub WriteRequiredFiles()
		Dim steamAPIDLLPathFileName As String = Path.Combine(Me.GetCustomDataPath(), App.theSteamAPIDLLFileName)
		Me.WriteResourceToFileIfDifferent(My.Resources.steam_api, steamAPIDLLPathFileName)

		'NOTE: Although Crowbar itself does not need the DLL file extracted, CrowbarSteamPipe needs it extracted.
		Dim steamworksDotNetPathFileName As String = Path.Combine(Me.GetCustomDataPath(), App.theSteamworksDotNetDLLFileName)
		Me.WriteResourceToFileIfDifferent(My.Resources.Steamworks_NET, steamworksDotNetPathFileName)

		Dim crowbarSteamPipePathFileName As String = Path.Combine(Me.GetCustomDataPath(), App.CrowbarSteamPipeFileName)
		Me.WriteResourceToFileIfDifferent(My.Resources.CrowbarSteamPipe, crowbarSteamPipePathFileName)

		Me.LzmaExePathFileName = Path.Combine(Me.GetCustomDataPath(), App.theLzmaExeFileName)
		Me.WriteResourceToFileIfDifferent(My.Resources.lzma, Me.LzmaExePathFileName)

		'NOTE: Only write settings file if it does not exist.
		Dim appSettingsPathFileName As String = Path.Combine(Me.GetCustomDataPath(), App.theAppSettingsFileName)
		Try
			If Not File.Exists(appSettingsPathFileName) Then
				File.WriteAllText(appSettingsPathFileName, My.Resources.Crowbar_Settings)
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			'Throw New Exception(ex.Message, ex.InnerException)
			Exit Sub
		Finally
		End Try
	End Sub

	Public Sub WriteUpdaterFiles()
		Me.SevenZrExePathFileName = Path.Combine(Me.GetCustomDataPath(), App.theSevenZrEXEFileName)
		Me.WriteResourceToFileIfDifferent(My.Resources.SevenZr, Me.SevenZrExePathFileName)

		Me.CrowbarLauncherExePathFileName = Path.Combine(Me.GetCustomDataPath(), App.theCrowbarLauncherEXEFileName)
		Me.WriteResourceToFileIfDifferent(My.Resources.CrowbarLauncher, Me.CrowbarLauncherExePathFileName)
	End Sub

	Public Sub DeleteUpdaterFiles()
		Me.SevenZrExePathFileName = Path.Combine(Me.GetCustomDataPath(), App.theSevenZrEXEFileName)
		Try
			If File.Exists(Me.SevenZrExePathFileName) Then
				File.Delete(Me.SevenZrExePathFileName)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Me.CrowbarLauncherExePathFileName = Path.Combine(Me.GetCustomDataPath(), App.theCrowbarLauncherEXEFileName)
		Try
			If File.Exists(Me.CrowbarLauncherExePathFileName) Then
				File.Delete(Me.CrowbarLauncherExePathFileName)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Public Sub WriteSteamAppIdFile(ByVal appID As UInteger)
		Me.WriteSteamAppIdFile(appID.ToString())
	End Sub

	Public Sub WriteSteamAppIdFile(ByVal appID_text As String)
		Dim steamAppIDPathFileName As String = Path.Combine(Me.GetCustomDataPath(), App.theSteamAppIDFileName)
		Using sw As StreamWriter = File.CreateText(steamAppIDPathFileName)
			sw.WriteLine(appID_text)
		End Using
	End Sub

	Public Function GetDebugPath(ByVal outputPath As String, ByVal modelName As String) As String
		'Dim logsPath As String

		'logsPath = Path.Combine(outputPath, modelName + "_" + App.LogsSubFolderName)

		'Return logsPath
		Return outputPath
	End Function

	Public Sub SaveAppSettings()
		Dim appSettingsPath As String
		Dim appSettingsPathFileName As String

		appSettingsPathFileName = Me.GetAppSettingsPathFileName()
		appSettingsPath = FileManager.GetPath(appSettingsPathFileName)

		If FileManager.PathExistsAfterTryToCreate(appSettingsPath) Then
			FileManager.WriteXml(Me.theSettings, appSettingsPathFileName)
		End If
	End Sub

	Public Sub InitAppInfo()
		If Me.SteamAppInfos Is Nothing Then
			Me.SteamAppInfos = SteamAppInfoBase.GetSteamAppInfos()
		End If
	End Sub

	'TODO: [GetCustomDataPath] Have location option where custom data and settings is saved.
	Public Function GetCustomDataPath() As String
		Dim customDataPath As String
		'Dim appDataPath As String

		'' If the settings file exists in the app's Data folder, then load it.
		'appDataPath = Me.GetAppDataPath()
		'If appDataPath <> "" Then
		'	customDataPath = appDataPath
		'Else
		'NOTE: Use "standard Windows location for app data".
		'NOTE: Using Path.Combine in case theStartupFolder is a root folder, like "C:\".
		customDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZeqMacaw")
		customDataPath += Path.DirectorySeparatorChar
		'customDataPath += "Crowbar"
		customDataPath += My.Application.Info.ProductName
		customDataPath += " "
		customDataPath += My.Application.Info.Version.ToString(2)

		FileManager.CreatePath(customDataPath)
		'End If

		Return customDataPath
	End Function

	Public Function GetPreviewsPath() As String
		Dim customDataPath As String = TheApp.GetCustomDataPath()
		Dim previewsPath As String = Path.Combine(customDataPath, App.PreviewsRelativePath)
		If FileManager.PathExistsAfterTryToCreate(previewsPath) Then
			Return previewsPath
		Else
			Return ""
		End If
	End Function

	Public Function GetAppSettingsPathFileName() As String
		Return Path.Combine(Me.GetCustomDataPath(), App.theAppSettingsFileName)
	End Function

#End Region

#Region "Private Methods"

	Private Sub LoadAppSettings()
		Dim appSettingsPathFileName As String
		appSettingsPathFileName = Me.GetAppSettingsPathFileName()

		Dim commandLineOption_Settings_IsEnabled As Boolean = False
		Dim commandLineValues As New ReadOnlyCollection(Of String)(System.Environment.GetCommandLineArgs())
		If commandLineValues.Count > 1 AndAlso commandLineValues(1) <> "" Then
			Dim command As String = commandLineValues(1)
			If command.StartsWith(App.SettingsParameter) Then
				commandLineOption_Settings_IsEnabled = True
				Dim oldAppSettingsPathFileName As String = command.Replace(App.SettingsParameter, "")
				oldAppSettingsPathFileName = oldAppSettingsPathFileName.Replace("""", "")
				If File.Exists(oldAppSettingsPathFileName) Then
					File.Copy(oldAppSettingsPathFileName, appSettingsPathFileName, True)
				End If
			End If
		End If

		If File.Exists(appSettingsPathFileName) Then
			Try
				VersionModule.ConvertSettingsFile(appSettingsPathFileName)
				Me.theSettings = CType(FileManager.ReadXml(GetType(AppSettings), appSettingsPathFileName), AppSettings)
			Catch
				Me.CreateAppSettings()
			End Try
		Else
			' File not found, so init default values.
			Me.CreateAppSettings()
		End If
	End Sub

	Private Sub CreateAppSettings()
		Me.theSettings = New AppSettings()

		Dim gameSetup As New GameSetup()
		Me.theSettings.GameSetups.Add(gameSetup)

		Dim aPath As New SteamLibraryPath()
		Me.theSettings.SteamLibraryPaths.Add(aPath)

		Me.SaveAppSettings()
	End Sub

	'Private Function GetAppDataPath() As String
	'	Dim appDataPath As String
	'	Dim appDataPathFileName As String

	'	appDataPath = Path.Combine(Me.theAppPath, App.theDataFolderName)
	'	appDataPathFileName = Path.Combine(appDataPath, App.theAppSettingsFileName)

	'	If File.Exists(appDataPathFileName) Then
	'		Return appDataPath
	'	Else
	'		Return ""
	'	End If
	'End Function

	Private Sub WriteResourceToFileIfDifferent(ByVal dataResource As Byte(), ByVal pathFileName As String)
		Try
			Dim isDifferentOrNotExist As Boolean = True
			If File.Exists(pathFileName) Then
				Dim resourceHash() As Byte
				Dim sha As New Security.Cryptography.SHA512Managed()
				resourceHash = sha.ComputeHash(dataResource)

				Dim fileStream As FileStream = File.Open(pathFileName, FileMode.Open)
				Dim fileHash() As Byte = sha.ComputeHash(fileStream)
				fileStream.Close()

				isDifferentOrNotExist = False
				For x As Integer = 0 To resourceHash.Length - 1
					If resourceHash(x) <> fileHash(x) Then
						isDifferentOrNotExist = True
						Exit For
					End If
				Next
			End If

			If isDifferentOrNotExist Then
				File.WriteAllBytes(pathFileName, dataResource)
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			'Throw New Exception(ex.Message, ex.InnerException)
			Exit Sub
		Finally
		End Try
	End Sub

	Public Function GetHeaderComment() As String
		Dim line As String

		line = "Created by "
		line += Me.GetProductNameAndVersion()

		Return line
	End Function

	Public Function GetProductNameAndVersion() As String
		Dim result As String

		result = My.Application.Info.ProductName
		result += " "
		result += My.Application.Info.Version.ToString(2)

		Return result
	End Function

	Public Function GetProcessedPathFileName(ByVal pathFileName As String) As String
		Dim result As String
		Dim aMacro As String

		result = pathFileName

		For Each aSteamLibraryPath As SteamLibraryPath In Me.Settings.SteamLibraryPaths
			aMacro = aSteamLibraryPath.Macro
			If pathFileName.StartsWith(aMacro) Then
				pathFileName = pathFileName.Remove(0, aMacro.Length)
				If pathFileName.StartsWith("\") Then
					pathFileName = pathFileName.Remove(0, 1)
				End If
				result = Path.Combine(aSteamLibraryPath.LibraryPath, pathFileName)
			End If
		Next

		Return result
	End Function

#End Region

#Region "Data"

	Private IsDisposed As Boolean

	Private theInternalCultureInfo As CultureInfo
	Private theInternalNumberFormat As NumberFormatInfo

	Private theSettings As AppSettings
	'NOTE: Use slash at start to avoid confusing with a pathFileName that Windows Explorer might use with auto-open.
	Public Const SettingsParameter As String = "/settings="
	Private theCommandLineOption_Settings_IsEnabled As Boolean

	' Location of the exe.
	Private theAppPath As String

	Private Const theSteamAPIDLLFileName As String = "steam_api.dll"
	Private Const theSteamworksDotNetDLLFileName As String = "Steamworks.NET.dll"
	Private Const theSevenZrEXEFileName As String = "7zr.exe"
	Private Const theCrowbarLauncherEXEFileName As String = "CrowbarLauncher.exe"
	Private Const theLzmaExeFileName As String = "lzma.exe"
	Public SevenZrExePathFileName As String
	Public CrowbarLauncherExePathFileName As String
	Public LzmaExePathFileName As String
	Public SteamAppInfos As List(Of SteamAppInfoBase)

	Private Const PreviewsRelativePath As String = "previews"
	Public Const CrowbarSteamPipeFileName As String = "CrowbarSteamPipe.exe"
	Private Const theSteamAppIDFileName As String = "steam_appid.txt"
	'Private Const theDataFolderName As String = "Data"
	Private Const theAppSettingsFileName As String = "Crowbar Settings.xml"

	Public Const AnimsSubFolderName As String = "anims"
	Public Const LogsSubFolderName As String = "logs"

	Private ErrorFileName As String = "unhandled_exception_error.txt"

	Private theUnpacker As Unpacker
	Private theDecompiler As Decompiler
	Private theCompiler As Compiler
	Private thePacker As Packer
	'Private theModelViewer As Viewer
	Private theModelRelativePathFileName As String

	Private theSmdFilesWritten As List(Of String)

#End Region

End Class
