Public Class SourceMdlModel31

	Public Sub New()
		'MyBase.New()

		Me.theSmdFileNames = New List(Of String)(MAX_NUM_LODS)
		For i As Integer = 0 To MAX_NUM_LODS - 1
			Me.theSmdFileNames.Add("")
		Next
	End Sub

	Public name(63) As Char
	Public type As Integer
	Public boundingRadius As Double
	Public meshCount As Integer
	Public meshOffset As Integer

	Public vertexCount As Integer
	Public vertexOffset As Integer

	' MDL 27 to 30 
	Public normalOffset_MDL27to30 As Integer

	Public tangentOffset As Integer

	' MDL 27 to 30 
	Public texCoordOffset_MDL27to30 As Integer
	Public boneWeightsOffset_MDL27to30 As Integer

	Public attachmentCount As Integer
	Public attachmentOffset As Integer
	Public eyeballCount As Integer
	Public eyeballOffset As Integer

	'Public unused(7) As Integer

	Public theSmdFileNames As List(Of String)
	'Public theEyeballs As List(Of SourceMdlEyeball37)
	Public theMeshes As List(Of SourceMdlMesh31)
	Public theName As String
	Public theTangents As List(Of SourceVector4D)
	Public theVertexes As List(Of SourceMdlVertex31)

End Class
