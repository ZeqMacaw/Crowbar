Module GenericsModule

	Public Function ListsAreEqual(ByVal list1 As List(Of Double), ByVal list2 As List(Of Double)) As Boolean
		Dim theListsAreEqual As Boolean

		theListsAreEqual = True
		If list1.Count <> list2.Count Then
			theListsAreEqual = False
		Else
			For i As Integer = 0 To list1.Count - 1
				If list1(i) <> list2(i) Then
					theListsAreEqual = False
					Exit For
				End If
			Next
		End If

		Return theListsAreEqual
	End Function

End Module
