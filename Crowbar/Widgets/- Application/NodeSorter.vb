Public Class NodeSorter
	Implements IComparer

	Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
		Dim tx As TreeNode = CType(x, TreeNode)
		Dim ty As TreeNode = CType(y, TreeNode)

		If tx.Text.StartsWith("<") AndAlso ty.Text.StartsWith("<") Then
			Return String.Compare(tx.Text, ty.Text)
		ElseIf tx.Text.StartsWith("<") Then
			Return 1
		ElseIf ty.Text.StartsWith("<") Then
			Return -1
		Else
			Return String.Compare(tx.Text, ty.Text)
		End If

	End Function

End Class
