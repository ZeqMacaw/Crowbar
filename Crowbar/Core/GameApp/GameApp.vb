Imports System.ComponentModel
Imports System.IO

Public Class GameApp
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.isDisposed = False

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.GameApp_DoWork
	End Sub

#Region "IDisposable Support"

	'Public Sub Dispose() Implements IDisposable.Dispose
	'	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
	'	Dispose(True)
	'	GC.SuppressFinalize(Me)
	'End Sub

	Protected Overloads Sub Dispose(ByVal disposing As Boolean)
		If Not Me.IsDisposed Then
			If disposing Then
				Me.Halt(False)
			End If
			'NOTE: free shared unmanaged resources
		End If
		Me.IsDisposed = True
		MyBase.Dispose(disposing)
	End Sub

	Protected Overrides Sub Finalize()
		' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
		Dispose(False)
		MyBase.Finalize()
	End Sub

#End Region

#End Region

#Region "Init and Free"

	'Private Sub Init()
	'End Sub

	'Private Sub Free()
	'End Sub

#End Region

#Region "Methods"

	Public Sub Run(ByVal gameSetupSelectedIndex As Integer)
		Dim info As New GameAppInfo()
		info.gameSetupSelectedIndex = gameSetupSelectedIndex
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Halt()
		Me.Halt(False)
	End Sub

#End Region

#Region "Event Handlers"

#End Region

#Region "Private Methods that can be called in either the main thread or the background thread"

	Private Sub Halt(ByVal calledFromBackgroundThread As Boolean)
		If Me.theGameAppProcess IsNot Nothing AndAlso Not Me.theGameAppProcess.HasExited Then
			Try
				If Not Me.theGameAppProcess.CloseMainWindow() Then
					Me.theGameAppProcess.Kill()
				End If
			Catch ex As Exception
				Dim debug As Integer = 4242
			Finally
				Me.theGameAppProcess.Close()
				Me.theGameAppProcess = Nothing
				'NOTE: This raises an exception when the background thread has already completed its work.
				'If calledFromBackgroundThread Then
				'	Me.UpdateProgressStop("Model viewer closed.")
				'End If
			End Try
		End If
	End Sub

#End Region

#Region "Private Methods that are called in the background thread"

	Private Sub GameApp_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Me.ReportProgress(0, "")

		Dim info As GameAppInfo

		info = CType(e.Argument, GameAppInfo)
		Me.theGameSetupSelectedIndex = info.gameSetupSelectedIndex
		If GameAppInputsAreOkay() Then
			Me.UpdateProgress(1, "Game run.")
			Me.RunGameApp()
		End If
	End Sub

	'TODO: Check inputs as done in Compiler.CompilerInputsAreValid().
	Private Function GameAppInputsAreOkay() As Boolean
		Dim inputsAreValid As Boolean

		inputsAreValid = True

		Dim gameSetup As GameSetup
		Dim gameAppPathFileName As String
		gameSetup = TheApp.Settings.GameSetups(Me.theGameSetupSelectedIndex)
		gameAppPathFileName = gameSetup.GameAppPathFileName

		If Not File.Exists(gameAppPathFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage("The game's executable, """ + gameAppPathFileName + """, does not exist.")
			Me.UpdateProgress(1, My.Resources.ErrorMessageSDKMissingCause)
		End If

		Return inputsAreValid
	End Function

	Private Sub RunGameApp()
		Dim gameAppPathFileName As String
		'Dim steamAppPathFileName As String
		'Dim gameAppId As String
		Dim gamePath As String
		Dim gameFileName As String
		Dim gameAppOptions As String
		'Dim currentFolder As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(Me.theGameSetupSelectedIndex)
		gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
		gameFileName = Path.GetFileName(gameSetup.GamePathFileName)
		gameAppPathFileName = gameSetup.GameAppPathFileName
		gameAppOptions = gameSetup.GameAppOptions

		'currentFolder = Directory.GetCurrentDirectory()
		'Directory.SetCurrentDirectory(gameModelsPath)

		Dim arguments As String = ""
		arguments += " -game """
		arguments += gamePath
		arguments += """ "
		arguments += gameAppOptions

		Me.theGameAppProcess = New Process()
		Dim myProcessStartInfo As New ProcessStartInfo(gameAppPathFileName, arguments)
		myProcessStartInfo.CreateNoWindow = True
		myProcessStartInfo.RedirectStandardError = True
		myProcessStartInfo.RedirectStandardOutput = True
		myProcessStartInfo.UseShellExecute = False
		' Instead of using asynchronous running, use synchronous and wait for process to exit, so this background thread won't complete until model viewer is closed.
		'      This allows background thread to announce to main thread when model viewer process exits.
		Me.theGameAppProcess.EnableRaisingEvents = True
		Me.theGameAppProcess.StartInfo = myProcessStartInfo

		Me.theGameAppProcess.Start()
		Me.theGameAppProcess.WaitForExit()
		Me.theGameAppProcess.Close()
		Me.theGameAppProcess = Nothing

		'Directory.SetCurrentDirectory(currentFolder)
	End Sub

	Private Sub UpdateProgressStart(ByVal line As String)
		Me.UpdateProgressInternal(0, line)
	End Sub

	Private Sub UpdateProgressStop(ByVal line As String)
		Me.UpdateProgressInternal(100, vbCr + line)
	End Sub

	Private Sub UpdateProgress()
		Me.UpdateProgressInternal(1, "")
	End Sub

	Private Sub WriteErrorMessage(ByVal line As String)
		Me.UpdateProgressInternal(1, "ERROR: " + line)
	End Sub

	Private Sub UpdateProgress(ByVal indentLevel As Integer, ByVal line As String)
		Dim indentedLine As String

		indentedLine = ""
		For i As Integer = 1 To indentLevel
			indentedLine += "  "
		Next
		indentedLine += line
		Me.UpdateProgressInternal(1, indentedLine)
	End Sub

	Private Sub UpdateProgressInternal(ByVal progressValue As Integer, ByVal line As String)
		''If progressValue = 0 Then
		''	Do not write to file stream.
		'If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
		'    Me.theLogFileStream.WriteLine(line)
		'    Me.theLogFileStream.Flush()
		'End If

		Me.ReportProgress(progressValue, line)
	End Sub

#End Region

#Region "Data"

	Private isDisposed As Boolean

	Private theGameSetupSelectedIndex As Integer

	Private theGameAppProcess As Process

#End Region

End Class
