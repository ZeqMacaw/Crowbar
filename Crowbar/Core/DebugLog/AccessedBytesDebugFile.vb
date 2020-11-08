Imports System.IO

Public Class AccessedBytesDebugFile

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter)
		Me.theOutputFileStreamWriter = outputFileStream
	End Sub

#End Region

#Region "Methods"

	Public Sub WriteHeaderComment()
		Dim line As String = ""

		line = "// "
		line += TheApp.GetHeaderComment()
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteFileSeekLog(ByVal aFileSeekLog As FileSeekLog)
		Dim line As String

		line = "====== File Size ======"
		Me.WriteLogLine(0, line)

		line = aFileSeekLog.theFileSize.ToString("N0")
		Me.WriteLogLine(1, line)

		line = "====== File Seek Log ======"
		Me.WriteLogLine(0, line)

		'line = "--- Summary ---"
		'Me.WriteLogLine(0, line)

		Dim offsetStart As Long
		Dim offsetEnd As Long
		'offsetStart = -1
		'offsetEnd = -1
		'For i As Integer = 0 To aFileSeekLog.theFileSeekList.Count - 1
		'	If offsetStart = -1 Then
		'		offsetStart = aFileSeekLog.theFileSeekList.Keys(i)
		'	End If
		'	offsetEnd = aFileSeekLog.theFileSeekList.Values(i)

		'	If aFileSeekLog.theFileSeekDescriptionList.Values(i).StartsWith("[ERROR] Unread bytes") Then
		'		If i > 0 Then
		'			line = offsetStart.ToString("N0") + " - " + (aFileSeekLog.theFileSeekList.Keys(i) - 1).ToString("N0")
		'			Me.WriteLogLine(1, line)
		'		End If
		'		If aFileSeekLog.theFileSeekDescriptionList.Values(i).StartsWith("[ERROR] Unread bytes (all zeroes)") Then
		'			line = aFileSeekLog.theFileSeekList.Keys(i).ToString("N0") + " - " + offsetEnd.ToString("N0") + " [ERROR] Unread bytes (all zeroes)"
		'		Else
		'			line = aFileSeekLog.theFileSeekList.Keys(i).ToString("N0") + " - " + offsetEnd.ToString("N0") + " [ERROR] Unread bytes (non-zero)"
		'		End If
		'		Me.WriteLogLine(1, line)
		'		offsetStart = -1
		'	ElseIf (i = aFileSeekLog.theFileSeekList.Count - 1) OrElse (offsetEnd + 1 <> aFileSeekLog.theFileSeekList.Keys(i + 1)) Then
		'		line = offsetStart.ToString("N0") + " - " + offsetEnd.ToString("N0")
		'		Me.WriteLogLine(1, line)
		'		offsetStart = -1
		'	End If
		'Next

		'line = "------------------------"
		'Me.WriteLogLine(0, line)
		'line = "--- Each Section or Loop ---"
		'Me.WriteLogLine(0, line)

		offsetEnd = -1
		For i As Integer = 0 To aFileSeekLog.theFileSeekList.Count - 1
			offsetStart = aFileSeekLog.theFileSeekList.Keys(i)
			offsetEnd = aFileSeekLog.theFileSeekList.Values(i)

			line = offsetStart.ToString("N0") + " - " + offsetEnd.ToString("N0") + " " + aFileSeekLog.theFileSeekDescriptionList.Values(i)
			Me.WriteLogLine(1, line)
		Next

		line = "========================"
		Me.WriteLogLine(0, line)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub WriteFileSeparatorLines()
		Dim line As String

		Me.WriteLogLine(0, "")
		Me.WriteLogLine(0, "")
		line = "################################################################################"
		Me.WriteLogLine(0, line)
		Me.WriteLogLine(0, "")
		Me.WriteLogLine(0, "")
	End Sub

	Private Sub WriteLogLine(ByVal indentLevel As Integer, ByVal line As String)
		Dim indentedLine As String = ""
		For i As Integer = 1 To indentLevel
			indentedLine += vbTab
		Next
		indentedLine += line
		Me.theOutputFileStreamWriter.WriteLine(indentedLine)
		Me.theOutputFileStreamWriter.Flush()
	End Sub

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter

#End Region

End Class
