Imports System.IO
Imports System.Threading

Public Class AppExceptionHandler

	Public Sub Application_ThreadException(ByVal sender As Object, ByVal t As ThreadExceptionEventArgs)
		Dim anUnhandledExceptionWindow As New UnhandledExceptionWindow()
		Try
			Dim errorReportText As String

			errorReportText = "################################################################################"
			errorReportText += vbCrLf
			errorReportText += "###### "
			errorReportText += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
			errorReportText += "   "
			errorReportText += My.Application.Info.ProductName
			errorReportText += " "
			errorReportText += My.Application.Info.Version.ToString(2)
			errorReportText += vbCrLf
			errorReportText += vbCrLf

			errorReportText += "=== Steps to reproduce the error ==="
			errorReportText += vbCrLf
			errorReportText += "[Describe the last few tasks you did in "
			errorReportText += My.Application.Info.ProductName
			errorReportText += " before the error occurred.]"
			errorReportText += vbCrLf
			errorReportText += vbCrLf

			errorReportText += "=== What you expected to see ==="
			errorReportText += vbCrLf
			errorReportText += "[Explain what you expected "
			errorReportText += My.Application.Info.ProductName
			errorReportText += " to do.]"
			errorReportText += vbCrLf
			errorReportText += vbCrLf

			errorReportText += "=== Context info ==="
			errorReportText += vbCrLf
			If TheApp Is Nothing Then
				errorReportText += "Exception occured before or after TheApp's lifetime."
			Else
				'If TheApp.Settings.ViewerIsRunning Then
				'	errorReportText += "Viewing "
				'	errorReportText += TheApp.Settings.ViewMdlPathFileName
				'	errorReportText += vbCrLf
				'End If
				If TheApp.Settings.DecompilerIsRunning Then
					errorReportText += "Decompiling "
					errorReportText += TheApp.Settings.DecompileMdlPathFileName
					errorReportText += vbCrLf
				End If
				If TheApp.Settings.CompilerIsRunning Then
					errorReportText += "Compiling "
					errorReportText += TheApp.Settings.CompileQcPathFileName
					errorReportText += vbCrLf
				End If
			End If
			errorReportText += vbCrLf
			errorReportText += vbCrLf

			errorReportText += "=== Exception error description ==="
			errorReportText += vbCrLf
			errorReportText += t.Exception.Message
			errorReportText += vbCrLf
			errorReportText += vbCrLf

			errorReportText += "=== Call stack ==="
			errorReportText += vbCrLf
			errorReportText += t.Exception.StackTrace
			errorReportText += vbCrLf

			errorReportText += vbCrLf
			errorReportText += vbCrLf
			errorReportText += vbCrLf

			Me.WriteToErrorFile(errorReportText)

			anUnhandledExceptionWindow.ErrorReportTextBox.Text = errorReportText
			anUnhandledExceptionWindow.ShowDialog()
		Catch
		Finally
			anUnhandledExceptionWindow.Dispose()
		End Try

		Application.Exit()
	End Sub

	Private Sub WriteToErrorFile(ByVal errorReportText As String)
		Using sw As StreamWriter = New StreamWriter(TheApp.ErrorPathFileName, True)
			sw.Write(errorReportText)
			sw.Close()
		End Using
	End Sub

End Class
