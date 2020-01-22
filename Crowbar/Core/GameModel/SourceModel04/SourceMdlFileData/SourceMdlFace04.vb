Public Class SourceMdlFace04

	Public Sub New()
		For i As Integer = 0 To vertexInfo.Length - 1
			vertexInfo(i) = New SourceMdlFaceVertexInfo04()
		Next
	End Sub

	'Public vertexIndex(11) As Integer
	'------
	Public vertexInfo(2) As SourceMdlFaceVertexInfo04

End Class
