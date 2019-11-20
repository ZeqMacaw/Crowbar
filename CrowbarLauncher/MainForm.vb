Imports System.Collections.ObjectModel
Imports System.IO

Public Class MainForm

	Private Sub Init()
		'TODO: Move new Crowbar.exe to where current Crowbar.exe is and then run the new Crowbar.exe.
		Dim commandLineValues As New ReadOnlyCollection(Of String)(System.Environment.GetCommandLineArgs())
		Dim startupPath As String = Application.StartupPath
		Dim sourcePathFileName As String = Path.Combine(startupPath, "Crowbar.exe")
		Dim targetPathFileName As String = commandLineValues(2)

		If commandLineValues.Count > 2 AndAlso commandLineValues(1) <> "" Then
			Dim crowbarExeProcessId As Int32 = CInt(commandLineValues(1))
			' GetProcessById will raise exception if crowbarExeProcessId is invalid, which should mean that Crowbar has already closed.
			Try
				Dim crowbarExeProcess As Process = Process.GetProcessById(crowbarExeProcessId)
				crowbarExeProcess.WaitForExit()
			Catch ex As Exception
			End Try

			Try
				If File.Exists(targetPathFileName) Then
					File.Delete(targetPathFileName)
				End If
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try

			Try
				File.Move(sourcePathFileName, targetPathFileName)
				'Me.UpdateProgress(2, "CROWBAR: Moved package file """ + sourcePathFileName + """ to """ + targetPath + """")
			Catch ex As Exception
				Dim debug As Integer = 4242
				'Me.UpdateProgress()
				'Me.UpdateProgress(2, "WARNING: Crowbar tried to move the file, """ + sourcePathFileName + """, to the output folder, but Windows complained with this message: " + ex.Message.Trim())
				'Me.UpdateProgress(2, "SOLUTION: Pack again (and hope Windows does not complain again) or move the file yourself.")
				'Me.UpdateProgress()
			End Try
		End If

		If File.Exists(targetPathFileName) Then
			' Run CrowbarLauncher.exe and exit Crowbar.
			Dim crowbarLauncherExeProcess As New Process()
			Try
				crowbarLauncherExeProcess.StartInfo.UseShellExecute = False
				'NOTE: From Microsoft website: 
				'      On Windows Vista and earlier versions of the Windows operating system, 
				'      the length of the arguments added to the length of the full path to the process must be less than 2080. 
				'      On Windows 7 and later versions, the length must be less than 32699. 
				crowbarLauncherExeProcess.StartInfo.FileName = targetPathFileName
				'crowbarLauncherExeProcess.StartInfo.Arguments = ""
#If DEBUG Then
				crowbarLauncherExeProcess.StartInfo.CreateNoWindow = False
#Else
				crowbarLauncherExeProcess.StartInfo.CreateNoWindow = True
#End If
				crowbarLauncherExeProcess.Start()
				Application.Exit()
			Catch ex As Exception
				Dim debug As Integer = 4242
				'Throw New System.Exception("Crowbar tried to compress the file """ + gmaPathFileName + """ to """ + processedPathFileName + """ but Windows gave this message: " + ex.Message)
			Finally
			End Try
		End If
	End Sub

	Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
		Me.Init()
	End Sub

End Class
