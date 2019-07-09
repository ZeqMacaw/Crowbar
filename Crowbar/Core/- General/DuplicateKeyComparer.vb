Public Class DuplicateKeyComparer(Of TKey As IComparable)
	Implements IComparer(Of TKey)

	Public Function Compare(x As TKey, y As TKey) As Integer Implements IComparer(Of TKey).Compare
		Dim result As Integer = x.CompareTo(y)

		If result = 0 Then
			Return 1
		Else
			' Handle equality as being greater
			Return result
		End If
	End Function

End Class
