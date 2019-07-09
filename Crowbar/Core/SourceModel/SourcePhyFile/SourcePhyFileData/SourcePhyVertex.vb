Imports System.Collections

Public Class SourcePhyVertex

	Public Sub New()
		Me.theNormals = New List(Of SourceVector)()
	End Sub

	Public vertex As New SourceVector()

	' Store the normals in here. 
	' Expected use: phyVertexObject.Normals.Add(vertexNormal)
	' Add a vertex normal for each face.
	Public ReadOnly Property Normals As List(Of SourceVector)
		Get
			Return Me.theNormals
		End Get
	End Property

	' Get the final normal from here.
	Public ReadOnly Property Normal As SourceVector
		Get
			Return Me.GetAverageNormal()
		End Get
	End Property

	Private Function GetAverageNormal() As SourceVector
		Me.theNormal = New SourceVector()
		For Each customNormal As SourceVector In Me.theNormals
			Me.theNormal.x += customNormal.x
			Me.theNormal.y += customNormal.y
			Me.theNormal.z += customNormal.z
		Next
		Me.theNormal.x /= Me.theNormals.Count
		Me.theNormal.y /= Me.theNormals.Count
		Me.theNormal.z /= Me.theNormals.Count
		MathModule.VectorNormalize(Me.theNormal)
		Return Me.theNormal
	End Function

	Private theNormals As List(Of SourceVector)
	Private theNormal As SourceVector

End Class
