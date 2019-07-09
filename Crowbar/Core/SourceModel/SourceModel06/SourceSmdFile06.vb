Imports System.IO

Public Class SourceSmdFile06

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData06)
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
			name = Me.theMdlFileData.theBones(boneIndex).theName

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
		Dim aBone As SourceMdlBone06
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

				rotation.x = aBone.rotation.x
				rotation.y = aBone.rotation.y
				rotation.z = aBone.rotation.z + MathModule.DegreesToRadians(-90)
				'angleVector.y += MathModule.DegreesToRadians(-90)
			Else
				position.x = aBone.position.x
				position.y = aBone.position.y
				position.z = aBone.position.z

				rotation.x = aBone.rotation.x
				rotation.y = aBone.rotation.y
				rotation.z = aBone.rotation.z
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
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteSkeletonSectionForAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc06, ByVal blendIndex As Integer)
		Dim line As String = ""
		Dim aBone As SourceMdlBone06
		Dim anAnimation As SourceMdlAnimation06
		Dim position As New SourceVector()
		Dim rotation As New SourceVector()
		Dim scale As Double
		Dim tempValue As Double

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

			For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
				aBone = Me.theMdlFileData.theBones(boneIndex)
				anAnimation = aSequenceDesc.theAnimations(boneIndex)

				If aBone.parentBoneIndex = -1 Then
					'position.x = anAnimation.theBonePositionsAndRotations(frameIndex).position.y
					'position.y = -anAnimation.theBonePositionsAndRotations(frameIndex).position.x
					'position.z = anAnimation.theBonePositionsAndRotations(frameIndex).position.z
					'======
					position.x = anAnimation.theBonePositionsAndRotations(frameIndex).position.x
					position.y = anAnimation.theBonePositionsAndRotations(frameIndex).position.y
					position.z = anAnimation.theBonePositionsAndRotations(frameIndex).position.z
					scale = frameIndex / (aSequenceDesc.frameCount - 1)
					If (aSequenceDesc.motiontype And SourceModule10.STUDIO_LX) = SourceModule10.STUDIO_LX Then
						position.x += scale * aSequenceDesc.linearmovement.x
					End If
					If (aSequenceDesc.motiontype And SourceModule10.STUDIO_LY) = SourceModule10.STUDIO_LY Then
						position.y += scale * aSequenceDesc.linearmovement.y
					End If
					If (aSequenceDesc.motiontype And SourceModule10.STUDIO_LZ) = SourceModule10.STUDIO_LZ Then
						position.z += scale * aSequenceDesc.linearmovement.z
					End If
					'NOTE: cos(90) = 0; sin(90) = 1
					tempValue = position.x
					position.x = position.y
					position.y = -tempValue

					rotation.x = anAnimation.theBonePositionsAndRotations(frameIndex).rotation.x
					rotation.y = anAnimation.theBonePositionsAndRotations(frameIndex).rotation.y
					rotation.z = anAnimation.theBonePositionsAndRotations(frameIndex).rotation.z + MathModule.DegreesToRadians(-90)
				Else
					position.x = anAnimation.theBonePositionsAndRotations(frameIndex).position.x
					position.y = anAnimation.theBonePositionsAndRotations(frameIndex).position.y
					position.z = anAnimation.theBonePositionsAndRotations(frameIndex).position.z

					rotation.x = anAnimation.theBonePositionsAndRotations(frameIndex).rotation.x
					rotation.y = anAnimation.theBonePositionsAndRotations(frameIndex).rotation.y
					rotation.z = anAnimation.theBonePositionsAndRotations(frameIndex).rotation.z
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

	Public Sub WriteTrianglesSection(ByVal aBodyModel As SourceMdlModel06)
		Dim line As String = ""
		Dim materialLine As String = ""
		Dim vertex1Line As String = ""
		Dim vertex2Line As String = ""
		Dim vertex3Line As String = ""
		Dim materialIndex As Integer
		Dim materialName As String
		Dim aMesh As SourceMdlMesh06
		Dim aTexture As SourceMdlTexture06

		'triangles
		line = "triangles"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Try
			If aBodyModel.theMeshes IsNot Nothing Then
				For meshIndex As Integer = 0 To aBodyModel.theMeshes.Count - 1
					aMesh = aBodyModel.theMeshes(meshIndex)
					materialIndex = aMesh.skinref
					aTexture = Me.theMdlFileData.theTextures(materialIndex)
					materialName = aTexture.theFileName

					If aMesh.theVertexAndNormalIndexes IsNot Nothing Then
						For groupIndex As Integer = 0 To aMesh.theVertexAndNormalIndexes.Count - 3 Step 3
							materialLine = materialName

							'NOTE: Reverse the order of the vertices so the normals will be on the correct side of the face.
							vertex1Line = Me.GetVertexLine(aBodyModel, aMesh.theVertexAndNormalIndexes(groupIndex + 2), aTexture)
							vertex2Line = Me.GetVertexLine(aBodyModel, aMesh.theVertexAndNormalIndexes(groupIndex + 1), aTexture)
							vertex3Line = Me.GetVertexLine(aBodyModel, aMesh.theVertexAndNormalIndexes(groupIndex), aTexture)

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

	Private Function GetVertexLine(ByVal aBodyModel As SourceMdlModel06, ByVal aStripGroup As SourceMdlTriangleVertex06, ByVal aTexture As SourceMdlTexture06) As String
		Dim line As String
		Dim boneIndex As Integer
		Dim vecin As New SourceVector
		Dim rawPosition As SourceVector
		Dim rawNormal As SourceVector
		Dim position As New SourceVector
		Dim normal As New SourceVector
		Dim texCoordX As Double
		Dim texCoordY As Double

		line = ""
		Try
			boneIndex = aBodyModel.theVertexBoneInfos(aStripGroup.vertexIndex)
			Dim boneTransform As SourceBoneTransform06
			boneTransform = Me.theMdlFileData.theBoneTransforms(boneIndex)

			' Reverse these.
			'FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
			'      void Grab_Triangles( s_model_t *pmodel )
			'	// move vertex position to object space.
			'	VectorSubtract( p.org, bonefixup[p.bone].worldorg, tmp );
			'	VectorTransform(tmp, bonefixup[p.bone].im, p.org );
			'------
			'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\mdldec\smdfile.cpp
			'      void WriteTriangles( FILE * pFile, mstudiomodel_t * pmodel )
			'			// Transform vertex position
			'			vec3_t vecin, vecpos, vecnorm;
			'			VectorCopy( pvert[ ptrivert->vertindex ], vecin );
			'			VectorTransform( vecin,
			'				g_bonetransform[ pvertbone[ ptrivert->vertindex ] ], vecpos );
			MathModule.VectorCopy(aBodyModel.theModelDatas(0).theVertexes(aStripGroup.vertexIndex), vecin)
			rawPosition = MathModule.VectorTransform(vecin, boneTransform.matrixColumn0, boneTransform.matrixColumn1, boneTransform.matrixColumn2, boneTransform.matrixColumn3)

			position.x = rawPosition.y
			position.y = -rawPosition.x
			position.z = rawPosition.z

			' Reverse these.
			'FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
			'      void Grab_Triangles( s_model_t *pmodel )
			'	// move normal to object space.
			'	VectorCopy( normal.org, tmp );
			'	VectorTransform(tmp, bonefixup[p.bone].im, normal.org );
			'	VectorNormalize( normal.org );
			'------
			'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\mdldec\smdfile.cpp
			'      void WriteTriangles( FILE * pFile, mstudiomodel_t * pmodel )
			'			// Transform vertex normal
			'			VectorCopy( pnorm[ ptrivert->normindex ], vecin );
			'			VectorRotate( vecin,
			'				g_bonetransform[ pnormbone[ ptrivert->normindex ] ], vecnorm );
			'			VectorNormalize( vecnorm );
			MathModule.VectorCopy(aBodyModel.theModelDatas(0).theNormals(aStripGroup.normalIndex), vecin)
			rawNormal = MathModule.VectorRotate(vecin, boneTransform.matrixColumn0, boneTransform.matrixColumn1, boneTransform.matrixColumn2, boneTransform.matrixColumn3)
			MathModule.VectorNormalize(rawNormal)

			normal.x = rawNormal.y
			normal.y = -rawNormal.x
			normal.z = rawNormal.z

			' Reverse these.
			'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\mdldec\smdfile.cpp
			'      void WriteTriangles( FILE * pFile, mstudiomodel_t * pmodel )
			'		// Texture "scale" factor
			'		float ss = 1.0f / ( float )pcurtexture->width;
			'		float st = 1.0f / ( float )pcurtexture->height;
			'				( float )ptrivert->s * ss,
			'				1.0f - ( float )ptrivert->t * st );
			texCoordX = aStripGroup.s / aTexture.width
			texCoordY = 1 - aStripGroup.t / aTexture.height

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
			'NOTE: Unlike all other versions, MDL v06 does not use (1 - texCoordY).
			line += texCoordY.ToString("0.000000", TheApp.InternalNumberFormat)
		Catch ex As Exception
			line = "// " + line
		End Try

		Return line
	End Function

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData06

#End Region

End Class
