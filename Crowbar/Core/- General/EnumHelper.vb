Public NotInheritable Class EnumHelper

	Public Shared Function GetDescription(ByVal value As System.Enum) As String
		If value Is Nothing Then
			Throw New ArgumentNullException("value")
		End If
		Dim description As String = value.ToString()
		Dim fieldInfo As Reflection.FieldInfo = value.GetType().GetField(description)
		Dim attributes As System.ComponentModel.DescriptionAttribute() = CType(fieldInfo.GetCustomAttributes(GetType(System.ComponentModel.DescriptionAttribute), False), System.ComponentModel.DescriptionAttribute())
		If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
			description = attributes(0).Description
		End If
		Return description
	End Function

	Public Shared Function ToList(ByVal type As Type) As IList
		If type Is Nothing Then
			Throw New ArgumentNullException("type")
		End If
		'Dim list As ArrayList = New ArrayList()
		Dim list As List(Of KeyValuePair(Of System.Enum, String)) = New List(Of KeyValuePair(Of System.Enum, String))()
		Dim enumValues As Array = System.Enum.GetValues(type)
		For Each value As System.Enum In enumValues
			list.Add(New KeyValuePair(Of System.Enum, String)(value, GetDescription(value)))
		Next
		Return list
	End Function

	Public Shared Sub InsertIntoList(ByVal index As Integer, ByVal value As System.Enum, ByRef list As IList)
		list.Insert(index, New KeyValuePair(Of System.Enum, String)(value, GetDescription(value)))
	End Sub

	Public Shared Sub RemoveFromList(ByVal value As System.Enum, ByRef list As IList)
		list.Remove(New KeyValuePair(Of System.Enum, String)(value, GetDescription(value)))
	End Sub

	Public Shared Function Contains(ByVal value As System.Enum, ByVal list As IList) As Boolean
		Return list.Contains(New KeyValuePair(Of System.Enum, String)(value, GetDescription(value)))
	End Function

	Public Shared Function FindKeyFromDescription(ByVal description As String, ByVal list As IList) As System.Enum
		Dim key As System.Enum = Nothing
		For Each pair As KeyValuePair(Of System.Enum, String) In list
			If pair.Value = description Then
				key = pair.Key
				Exit For
			End If
		Next
		Return key
	End Function

	Public Shared Function IndexOf(ByVal key As System.Enum, ByVal list As IList) As Integer
		Return list.IndexOf(New KeyValuePair(Of System.Enum, String)(key, GetDescription(key)))
	End Function

	Public Shared Function IndexOfKeyAsString(ByVal keyText As String, ByVal list As IList) As Integer
		Dim index As Integer = -1
		For pairIndex As Integer = 0 To list.Count - 1
			Dim pair As KeyValuePair(Of System.Enum, String) = CType(list(pairIndex), KeyValuePair(Of [Enum], String))
			If pair.Key.ToString() = keyText Then
				index = pairIndex
				Exit For
			End If
		Next
		Return index
	End Function

	Public Shared Function IndexOfKeyAsCaseInsensitiveString(ByVal keyText As String, ByVal list As IList) As Integer
		Dim index As Integer = -1
		For pairIndex As Integer = 0 To list.Count - 1
			Dim pair As KeyValuePair(Of System.Enum, String) = CType(list(pairIndex), KeyValuePair(Of [Enum], String))
			If pair.Key.ToString().ToLower() = keyText.ToLower() Then
				index = pairIndex
				Exit For
			End If
		Next
		Return index
	End Function

	Public Shared Function IndexOf(ByVal description As String, ByVal list As IList) As Integer
		Dim index As Integer = -1
		Dim key As System.Enum = Nothing
		key = FindKeyFromDescription(description, list)
		If key IsNot Nothing Then
			index = list.IndexOf(New KeyValuePair(Of System.Enum, String)(key, GetDescription(key)))
		End If
		Return index
	End Function

	Public Shared Function Key(ByVal index As Integer, ByVal list As IList) As System.Enum
		Dim pair As KeyValuePair(Of System.Enum, String) = CType(list(index), KeyValuePair(Of [Enum], String))
		Return pair.Key
	End Function

	Public Shared Function Value(ByVal index As Integer, ByVal list As IList) As String
		Dim pair As KeyValuePair(Of System.Enum, String) = CType(list(index), KeyValuePair(Of [Enum], String))
		Return pair.Value
	End Function

End Class
