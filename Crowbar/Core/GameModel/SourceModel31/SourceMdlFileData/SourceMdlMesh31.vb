Public Class SourceMdlMesh31

	Public materialIndex As Integer
	Public modelOffset As Integer

	Public vertexCount As Integer
	Public vertexIndexStart As Integer
	Public flexCount As Integer
	Public flexOffset As Integer
	Public materialType As Integer
	Public materialParam As Integer

	Public id As Integer
	Public center As New SourceVector()

	' MDL 27
	Public unused_MDL27(11) As Integer
	'------
	' MDL 28 to 31
	Public unused(4) As Integer

	'Public theFlexes As List(Of SourceMdlFlex37)

End Class
