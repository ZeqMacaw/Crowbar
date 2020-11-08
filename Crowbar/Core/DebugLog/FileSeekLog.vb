Imports System.IO

Public Class FileSeekLog

	Public Sub New()
		Me.theFileSeekList = New SortedList(Of Long, Long)
		Me.theFileSeekDescriptionList = New SortedList(Of Long, String)
	End Sub

	Public Function ContainsKey(ByVal startOffset As Long) As Boolean
		Return Me.theFileSeekList.ContainsKey(startOffset)
	End Function

	Public Sub Add(ByVal startOffset As Long, ByVal endOffset As Long, ByVal description As String)
		Try
			If Me.theFileSeekList.ContainsKey(startOffset) AndAlso Me.theFileSeekList(startOffset) = endOffset Then
				Me.theFileSeekDescriptionList(startOffset) += "; " + description
			ElseIf Me.theFileSeekList.ContainsKey(startOffset) Then
				Dim temp As String
				temp = Me.theFileSeekDescriptionList(startOffset)
				Me.theFileSeekDescriptionList(startOffset) = "[ERROR] "
				Me.theFileSeekDescriptionList(startOffset) += temp + "; [" + startOffset.ToString() + " - " + endOffset.ToString() + "] " + description
			Else
				Me.theFileSeekList.Add(startOffset, endOffset)
				Me.theFileSeekDescriptionList.Add(startOffset, description)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Public Sub Remove(ByVal startOffset As Long)
		Try
			If Me.theFileSeekList.ContainsKey(startOffset) Then
				Me.theFileSeekList.Remove(startOffset)
			End If
			If Me.theFileSeekDescriptionList.ContainsKey(startOffset) Then
				Me.theFileSeekDescriptionList.Remove(startOffset)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Public Sub Clear()
		Me.theFileSeekList.Clear()
		Me.theFileSeekDescriptionList.Clear()
	End Sub

	Public Property FileSize() As Long
		Get
			Return Me.theFileSize
		End Get
		Set(value As Long)
			If Me.theFileSize <> value Then
				Me.theFileSize = value
				Me.Add(Me.theFileSize, Me.theFileSize, "END OF FILE + 1 (File size)")
			End If
		End Set
	End Property

	Public Sub LogToEndAndAlignToNextStart(ByVal inputFileReader As BinaryReader, ByVal fileOffsetEnd As Long, ByVal byteAlignmentCount As Integer, ByVal description As String, Optional ByVal expectedAlignOffsetEnd As Long = -1)
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		fileOffsetStart2 = fileOffsetEnd + 1
		fileOffsetEnd2 = MathModule.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
		inputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
		If fileOffsetEnd2 >= fileOffsetStart2 Then
			Dim allZeroesWereFound As Boolean
			If expectedAlignOffsetEnd > -1 AndAlso expectedAlignOffsetEnd <> fileOffsetEnd2 Then
				description = "[ERROR: Should end at " + CStr(expectedAlignOffsetEnd) + "] " + description
				description += " - " + fileOffsetStart2.ToString() + ":" + Me.GetByteValues(inputFileReader, fileOffsetStart2, fileOffsetEnd2, allZeroesWereFound)
				description += " - " + (fileOffsetEnd2 + 1).ToString() + ":" + Me.GetByteValues(inputFileReader, fileOffsetEnd2 + 1, expectedAlignOffsetEnd, allZeroesWereFound)
			Else
				description += Me.GetByteValues(inputFileReader, fileOffsetStart2, fileOffsetEnd2, allZeroesWereFound)
			End If

			Me.Add(fileOffsetStart2, fileOffsetEnd2, description)
		End If
	End Sub

	Public Sub LogAndAlignFromFileSeekLogEnd(ByVal inputFileReader As BinaryReader, ByVal byteAlignmentCount As Integer, ByVal description As String)
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		'fileOffsetStart2 = Me.theFileSeekList.Values(Me.theFileSeekList.Count - 1) + 1
		'fileOffsetEnd2 = MathModule.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
		'inputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
		'If fileOffsetEnd2 >= fileOffsetStart2 Then
		'	Me.Add(fileOffsetStart2, fileOffsetEnd2, description)
		'End If
		'Me.LogToEndAndAlignToNextStart(inputFileReader, Me.theFileSeekList.Values(Me.theFileSeekList.Count - 1), byteAlignmentCount, description)
		'NOTE: The "- 2" skips the final value that should be the "END OF FILE + 1 (File size)".
		Me.LogToEndAndAlignToNextStart(inputFileReader, Me.theFileSeekList.Values(Me.theFileSeekList.Count - 2), byteAlignmentCount, description)
	End Sub

	Public Sub LogUnreadBytes(ByVal inputFileReader As BinaryReader)
		Dim offsetStart As Long
		Dim offsetEnd As Long
		Dim description As String
		Dim byteValues As String
		Dim allZeroesWereFound As Boolean
		Dim tempFileSeekList As New SortedList(Of Long, Long)()
		Dim tempFileSeekDescriptionList As New SortedList(Of Long, String)()

		offsetStart = -1
		offsetEnd = -1
		Try
			For i As Integer = 0 To Me.theFileSeekList.Count - 1
				offsetStart = Me.theFileSeekList.Keys(i)

				If offsetEnd < offsetStart - 1 Then
					description = "[ERROR] Unread bytes"
					byteValues = Me.GetByteValues(inputFileReader, offsetEnd + 1, offsetStart - 1, allZeroesWereFound)
					If allZeroesWereFound Then
						description += " (all zeroes)"
					Else
						description += " (non-zero)"
					End If
					description += byteValues

					' Can't add into the list that is being iterated, so use temp list.
					If Me.theFileSeekList.ContainsKey(offsetEnd + 1) Then
						Dim temp As String
						temp = Me.theFileSeekDescriptionList(offsetEnd + 1)
						Me.theFileSeekDescriptionList(offsetEnd + 1) = "[ERROR] "
						Me.theFileSeekDescriptionList(offsetEnd + 1) += temp + "; [" + (offsetEnd + 1).ToString() + " - " + (offsetStart - 1).ToString() + "] " + description
					Else
						tempFileSeekList.Add(offsetEnd + 1, offsetStart - 1)
						tempFileSeekDescriptionList.Add(offsetEnd + 1, description)
					End If
				End If

				offsetEnd = Me.theFileSeekList.Values(i)
			Next

			For i As Integer = 0 To tempFileSeekList.Count - 1
				Me.Add(tempFileSeekList.Keys(i), tempFileSeekList.Values(i), tempFileSeekDescriptionList.Values(i))
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Me.LogErrors()
	End Sub

	Public theFileSize As Long
	Public theFileSeekList As SortedList(Of Long, Long)
	Public theFileSeekDescriptionList As SortedList(Of Long, String)

	Private Sub LogErrors()
		Dim offsetStart As Long
		Dim offsetEnd As Long

		Try
			For i As Integer = 0 To Me.theFileSeekList.Count - 1
				offsetStart = Me.theFileSeekList.Keys(i)
				offsetEnd = Me.theFileSeekList.Values(i)

				If (i < Me.theFileSeekList.Count - 1) AndAlso (offsetEnd + 1 <> Me.theFileSeekList.Keys(i + 1)) Then
					Me.theFileSeekDescriptionList(offsetStart) = "[ERROR] [End offset is incorrect] " + Me.theFileSeekDescriptionList(offsetStart)
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Function GetByteValues(ByVal inputFileReader As BinaryReader, ByVal fileOffsetStart2 As Long, ByVal fileOffsetEnd2 As Long, ByRef allZeroesWereFound As Boolean) As String
		Dim byteValues As String

		Dim inputFileStreamPosition As Long
		inputFileStreamPosition = inputFileReader.BaseStream.Position

		Dim byteValue As Byte

		Dim adjustedFileOffsetEnd2 As Long
		If (fileOffsetEnd2 - fileOffsetStart2) > 20 Then
			adjustedFileOffsetEnd2 = fileOffsetStart2 + 20
		Else
			adjustedFileOffsetEnd2 = fileOffsetEnd2
		End If

		Try
			inputFileReader.BaseStream.Seek(fileOffsetStart2, SeekOrigin.Begin)
			allZeroesWereFound = True
			byteValues = " ["
			For byteOffset As Long = fileOffsetStart2 To adjustedFileOffsetEnd2
				byteValue = inputFileReader.ReadByte()
				byteValues += " " + byteValue.ToString("X2")
				If byteValue <> 0 Then
					allZeroesWereFound = False
				End If
			Next
			If (fileOffsetEnd2 - fileOffsetStart2) > 20 Then
				byteValues += " ..."
				''NOTE: Indicate non-zeroes if more than 20 bytes unread because might be non-zeroes past the first 20.
				'allZeroesWereFound = False
				For byteOffset As Long = adjustedFileOffsetEnd2 + 1 To fileOffsetEnd2
					byteValue = inputFileReader.ReadByte()
					If byteValue <> 0 Then
						allZeroesWereFound = False
						Exit For
					End If
				Next
			End If
			byteValues += " ]"

			inputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Catch ex As Exception
			allZeroesWereFound = False
			byteValues = "[incomplete read due to error]"
		End Try

		Return byteValues
	End Function

End Class
