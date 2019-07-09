Public Class SourceMdlModel04

	Public unknownSingle As Double
	Public vertexCount As Integer
	Public normalCount As Integer
	Public meshCount As Integer

	Public theSmdFileName As String
	Public theMeshes As New List(Of SourceMdlMesh04)()
	Public theNormals As New List(Of SourceMdlNormal04)()
	Public theVertexes As New List(Of SourceMdlVertex04)()

End Class
