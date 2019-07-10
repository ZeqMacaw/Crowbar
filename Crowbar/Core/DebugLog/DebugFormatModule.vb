Module DebugFormatModule

	Public Function FormatByteWithHexLine(ByVal name As String, ByVal value As Byte) As String
		Dim line As String
		line = name
		line += ": "
		line += value.ToString("N0")
		line += " (0x"
		line += value.ToString("X2")
		line += ")"
		Return line
	End Function

	Public Function FormatIntegerLine(ByVal name As String, ByVal value As Integer) As String
		Dim line As String
		line = name
		line += ": "
		line += value.ToString("N0")
		Return line
	End Function

	Public Function FormatIntegerAsHexLine(ByVal name As String, ByVal value As Integer) As String
		Dim line As String
		line = name
		line += ": "
		line += "0x"
		line += value.ToString("X8")
		Return line
	End Function

	Public Function FormatIntegerWithHexLine(ByVal name As String, ByVal value As Integer) As String
		Dim line As String
		line = name
		line += ": "
		line += value.ToString("N0")
		line += " (0x"
		line += value.ToString("X8")
		line += ")"
		Return line
	End Function

	Public Function FormatLongWithHexLine(ByVal name As String, ByVal value As Long) As String
		Dim line As String
		line = name
		line += ": "
		line += value.ToString("N0", TheApp.InternalNumberFormat)
		line += " (0x"
		line += value.ToString("X16")
		line += ")"
		Return line
	End Function

	Public Function FormatSingleFloatLine(ByVal name As String, ByVal value As Single) As String
		Dim line As String
		line = name
		line += ": "
		line += value.ToString("N6", TheApp.InternalNumberFormat)
		Return line
	End Function

	Public Function FormatDoubleFloatLine(ByVal name As String, ByVal value As Double) As String
		Dim line As String
		line = name
		line += ": "
		line += value.ToString("N6", TheApp.InternalNumberFormat)
		Return line
	End Function

	Public Function FormatStringLine(ByVal name As String, ByVal value As String) As String
		Dim line As String
		line = name
		line += ": "
		line += CStr(value).TrimEnd(Chr(0))
		Return line
	End Function

	Public Function FormatIndexLine(ByVal name As String, ByVal value As Integer) As String
		Dim line As String
		line = "["
		line += name
		line += " index: "
		line += value.ToString("N0")
		line += "]"
		Return line
	End Function

	Public Function FormatVectorLine(ByVal name As String, ByVal x As Double, ByVal y As Double, ByVal z As Double) As String
		Dim line As String
		line = name
		line += "[x,y,z]: ("
		line += x.ToString("N6", TheApp.InternalNumberFormat)
		line += ", "
		line += y.ToString("N6", TheApp.InternalNumberFormat)
		line += ", "
		line += z.ToString("N6", TheApp.InternalNumberFormat)
		line += ")"
		Return line
	End Function

	Public Function FormatVectorLine(ByVal name As String, ByVal value As SourceVector) As String
		Dim line As String
		line = name
		line += "[x,y,z]: ("
		line += value.x.ToString("N6", TheApp.InternalNumberFormat)
		line += ", "
		line += value.y.ToString("N6", TheApp.InternalNumberFormat)
		line += ", "
		line += value.z.ToString("N6", TheApp.InternalNumberFormat)
		line += ")"
		Return line
	End Function

	Public Function FormatQuaternionLine(ByVal name As String, ByVal value As SourceQuaternion) As String
		Dim line As String
		line = name
		line += "[x,y,z,w]: ("
		line += value.x.ToString("N6", TheApp.InternalNumberFormat)
		line += ", "
		line += value.y.ToString("N6", TheApp.InternalNumberFormat)
		line += ", "
		line += value.z.ToString("N6", TheApp.InternalNumberFormat)
		line += ", "
		line += value.w.ToString("N6", TheApp.InternalNumberFormat)
		line += ")"
		Return line
	End Function

End Module
