Public Class StringClass

	Public Shared Function ConvertFromNullTerminatedOrFullLengthString(ByVal input As String) As String
		Dim output As String
		Dim positionOfFirstNullChar As Integer
		positionOfFirstNullChar = input.IndexOf(Chr(0))
		If positionOfFirstNullChar = -1 Then
			output = input
		Else
			output = input.Substring(0, positionOfFirstNullChar)
		End If
		Return output
	End Function

	Public Shared Function RemoveUptoAndIncludingFirstDotCharacterFromString(ByVal input As String) As String
		Dim output As String
		Dim positionOfFirstDotChar As Integer
		positionOfFirstDotChar = input.IndexOf(".")
		If positionOfFirstDotChar >= 0 Then
			output = input.Substring(positionOfFirstDotChar + 1)
		Else
			output = input
		End If
		Return output
	End Function

End Class
