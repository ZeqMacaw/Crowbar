Public Class ParseMeshSmdInfo

	Public Sub New()
		messages = New List(Of String)()
		boneNames = New List(Of String)()
	End Sub

	Public messages As List(Of String)
	Public lineCount As Integer
	Public boneCount As Integer
	Public boneNames As List(Of String)

End Class
