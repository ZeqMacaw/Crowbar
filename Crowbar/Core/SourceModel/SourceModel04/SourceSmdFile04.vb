Imports System.IO

Public Class SourceSmdFile04

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData04)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
	End Sub

#End Region

#Region "Methods"

	Public Sub WriteHeaderComment()
		Common.WriteHeaderComment(Me.theOutputFileStreamWriter)
	End Sub

	Public Sub WriteHeaderSection()
		Dim line As String = ""

		'version 1
		line = "version 1"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteNodesSection()
		Dim line As String = ""
		Dim name As String

		'nodes
		line = "nodes"
		Me.theOutputFileStreamWriter.WriteLine(line)

		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			name = "bone" + boneIndex.ToString()

			line = "  "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)
			line += " """
			line += name
			line += """ "
			line += Me.theMdlFileData.theBones(boneIndex).parentBoneIndex.ToString(TheApp.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteSkeletonSection()
		Dim line As String = ""
		Dim aBone As SourceMdlBone04
		Dim position As New SourceVector()
		Dim rotation As New SourceVector()

		'skeleton
		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If TheApp.Settings.DecompileStricterFormatIsChecked Then
			line = "time 0"
		Else
			line = "  time 0"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)
		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			aBone = Me.theMdlFileData.theBones(boneIndex)

			If aBone.parentBoneIndex = -1 Then
				position.x = aBone.position.y
				position.y = -aBone.position.x
				position.z = aBone.position.z

				rotation.x = 0
				rotation.y = 0
				rotation.z = MathModule.DegreesToRadians(-90)
			Else
				position.x = aBone.position.x
				position.y = aBone.position.y
				position.z = aBone.position.z

				rotation.x = 0
				rotation.y = 0
				rotation.z = 0
			End If

			'If aBone.parentBoneIndex = -1 Then
			'	position.x = aBone.positionY.TheFloatValue
			'	position.y = -aBone.positionX.TheFloatValue
			'	position.z = aBone.positionZ.TheFloatValue

			'	rotation.x = aBone.rotationX.TheFloatValue
			'	rotation.y = aBone.rotationY.TheFloatValue
			'	rotation.z = aBone.rotationZ.TheFloatValue + MathModule.DegreesToRadians(-90)
			'Else
			'	position.x = aBone.positionX.TheFloatValue
			'	position.y = aBone.positionY.TheFloatValue
			'	position.z = aBone.positionZ.TheFloatValue

			'	''rotation.x = MathModule.DegreesToRadians(aBone.rotationX / 200)
			'	''rotation.y = MathModule.DegreesToRadians(aBone.rotationY / 200)
			'	''rotation.z = MathModule.DegreesToRadians(aBone.rotationZ / 200)
			'	rotation.x = aBone.rotationX.TheFloatValue
			'	rotation.y = aBone.rotationY.TheFloatValue
			'	rotation.z = aBone.rotationZ.TheFloatValue
			'End If

			line = "    "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += position.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += position.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += position.z.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += rotation.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += rotation.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += rotation.z.ToString("0.000000", TheApp.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteSkeletonSectionForAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc04)
		Dim line As String = ""
		Dim aBone As SourceMdlBone04
		Dim aSequence As SourceMdlSequence04
		Dim position As New SourceVector()
		Dim rotation As New SourceVector()

		'skeleton
		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		For frameIndex As Integer = 0 To aSequenceDesc.frameCount - 1
			If TheApp.Settings.DecompileStricterFormatIsChecked Then
				line = "time "
			Else
				line = "  time "
			End If
			line += CStr(frameIndex)
			Me.theOutputFileStreamWriter.WriteLine(line)

			aSequence = aSequenceDesc.theSequences(frameIndex)
			For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
				aBone = Me.theMdlFileData.theBones(boneIndex)

				If aBone.parentBoneIndex = -1 Then
					position.x = aSequence.thePositionsAndRotations(boneIndex).position.y
					position.y = -aSequence.thePositionsAndRotations(boneIndex).position.x
					position.z = aSequence.thePositionsAndRotations(boneIndex).position.z

					rotation.x = aSequence.thePositionsAndRotations(boneIndex).rotation.x
					rotation.y = aSequence.thePositionsAndRotations(boneIndex).rotation.y
					rotation.z = aSequence.thePositionsAndRotations(boneIndex).rotation.z + MathModule.DegreesToRadians(-90)
				Else
					position.x = aSequence.thePositionsAndRotations(boneIndex).position.x
					position.y = aSequence.thePositionsAndRotations(boneIndex).position.y
					position.z = aSequence.thePositionsAndRotations(boneIndex).position.z

					rotation.x = aSequence.thePositionsAndRotations(boneIndex).rotation.x
					rotation.y = aSequence.thePositionsAndRotations(boneIndex).rotation.y
					rotation.z = aSequence.thePositionsAndRotations(boneIndex).rotation.z
				End If

				line = "    "
				line += boneIndex.ToString(TheApp.InternalNumberFormat)

				line += " "
				line += position.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.z.ToString("0.000000", TheApp.InternalNumberFormat)

				line += " "
				line += rotation.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += rotation.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += rotation.z.ToString("0.000000", TheApp.InternalNumberFormat)

				'If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				'	line += "   # "
				'	line += "pos: "
				'	line += aFrameLine.position.debug_text
				'	line += "   "
				'	line += "rot: "
				'	line += aFrameLine.rotation.debug_text
				'End If

				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteTrianglesSection(ByVal aBodyModel As SourceMdlModel04)
		Dim line As String = ""
		Dim materialLine As String = ""
		Dim vertex1Line As String = ""
		Dim vertex2Line As String = ""
		Dim vertex3Line As String = ""
		Dim materialName As String
		Dim aMesh As SourceMdlMesh04

		'triangles
		line = "triangles"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Try
			If aBodyModel.theMeshes IsNot Nothing Then
				For meshIndex As Integer = 0 To aBodyModel.theMeshes.Count - 1
					aMesh = aBodyModel.theMeshes(meshIndex)
					materialName = aMesh.theTextureFileName

					If aMesh.theFaces IsNot Nothing Then
						For faceIndex As Integer = 0 To aMesh.theFaces.Count - 1
							materialLine = materialName

							vertex1Line = Me.GetVertexLine(aBodyModel, aMesh, aMesh.theFaces(faceIndex).vertexInfo(2))
							vertex2Line = Me.GetVertexLine(aBodyModel, aMesh, aMesh.theFaces(faceIndex).vertexInfo(1))
							vertex3Line = Me.GetVertexLine(aBodyModel, aMesh, aMesh.theFaces(faceIndex).vertexInfo(0))

							If vertex1Line.StartsWith("// ") OrElse vertex2Line.StartsWith("// ") OrElse vertex3Line.StartsWith("// ") Then
								materialLine = "// " + materialLine
								If Not vertex1Line.StartsWith("// ") Then
									vertex1Line = "// " + vertex1Line
								End If
								If Not vertex2Line.StartsWith("// ") Then
									vertex2Line = "// " + vertex2Line
								End If
								If Not vertex3Line.StartsWith("// ") Then
									vertex3Line = "// " + vertex3Line
								End If
							End If
							Me.theOutputFileStreamWriter.WriteLine(materialLine)
							Me.theOutputFileStreamWriter.WriteLine(vertex1Line)
							Me.theOutputFileStreamWriter.WriteLine(vertex2Line)
							Me.theOutputFileStreamWriter.WriteLine(vertex3Line)
						Next
					End If
				Next
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

	Private Function GetVertexLine(ByVal aBodyModel As SourceMdlModel04, ByVal aMesh As SourceMdlMesh04, ByVal aVertexInfo As SourceMdlFaceVertexInfo04) As String
		Dim line As String
		Dim boneIndex As Integer
		Dim aBone As SourceMdlBone04
		Dim position As New SourceVector
		Dim normal As New SourceVector
		Dim texCoordX As Double
		Dim texCoordY As Double

		line = ""
		Try
			boneIndex = aBodyModel.theVertexes(aVertexInfo.vertexIndex).boneIndex
			aBone = Me.theMdlFileData.theBones(boneIndex)

			'position.x = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x
			'position.y = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
			'position.z = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z
			position.x = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
			position.y = -aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x
			position.z = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z
			'position.x = aBone.position.x + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x
			'position.y = aBone.position.y + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
			'position.z = aBone.position.z + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z
			'position.x = aBone.position.y + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
			'position.y = -(aBone.position.x + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x)
			'position.z = aBone.position.z + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z

			'position.x = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x
			'position.y = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
			'position.z = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z
			'position.x = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
			'position.y = -(aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x)
			'position.z = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z

			normal.x = aBodyModel.theNormals(aVertexInfo.normalIndex).vector.x
			normal.y = aBodyModel.theNormals(aVertexInfo.normalIndex).vector.y
			normal.z = aBodyModel.theNormals(aVertexInfo.normalIndex).vector.z

			If aBodyModel.theVertexes(aVertexInfo.vertexIndex).boneIndex <> aBodyModel.theNormals(aVertexInfo.normalIndex).index Then
				Dim debug As Integer = 4242
			End If

			texCoordX = aVertexInfo.s / aMesh.textureWidth
			texCoordY = aVertexInfo.t / aMesh.textureHeight
			'texCoordY = 1 - aVertexInfo.t / aMesh.textureHeight

			line = "  "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)

			line += " "
			line += position.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += position.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += position.z.ToString("0.000000", TheApp.InternalNumberFormat)

			line += " "
			line += normal.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += normal.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += normal.z.ToString("0.000000", TheApp.InternalNumberFormat)

			line += " "
			line += texCoordX.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += texCoordY.ToString("0.000000", TheApp.InternalNumberFormat)
			'line += (1 - texCoordY).ToString("0.000000", TheApp.InternalNumberFormat)
		Catch ex As Exception
			line = "// " + line
		End Try

		Return line
	End Function

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData04

#End Region

End Class
