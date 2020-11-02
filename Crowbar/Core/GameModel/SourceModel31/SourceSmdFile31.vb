Imports System.IO

Public Class SourceSmdFile31

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData31)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
	End Sub

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData31, ByVal phyFileData As SourcePhyFileData)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
		Me.thePhyFileData = phyFileData
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

	Public Sub WriteNodesSection(ByVal lodIndex As Integer)
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

	Public Sub WriteSkeletonSection(ByVal lodIndex As Integer)
		Dim line As String = ""

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
			line = "    "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).position.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).position.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).position.z.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).rotation.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).rotation.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).rotation.z.ToString("0.000000", TheApp.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteTrianglesSection(ByVal lodIndex As Integer, ByVal aVtxModel As SourceVtxModel06, ByVal aModel As SourceMdlModel31, ByVal bodyPartVertexIndexStart As Integer)
		Dim line As String = ""
		Dim materialLine As String = ""
		Dim vertex1Line As String = ""
		Dim vertex2Line As String = ""
		Dim vertex3Line As String = ""

		'triangles
		line = "triangles"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Dim aVtxLod As SourceVtxModelLod06
		Dim aVtxMesh As SourceVtxMesh06
		Dim aStripGroup As SourceVtxStripGroup06
		'Dim cumulativeVertexCount As Integer
		'Dim maxIndexForMesh As Integer
		'Dim cumulativeMaxIndex As Integer
		Dim materialIndex As Integer
		Dim materialName As String
		Dim meshVertexIndexStart As Integer

		Try
			aVtxLod = aVtxModel.theVtxModelLods(lodIndex)

			If aVtxLod.theVtxMeshes IsNot Nothing Then
				'cumulativeVertexCount = 0
				'maxIndexForMesh = 0
				'cumulativeMaxIndex = 0
				For meshIndex As Integer = 0 To aVtxLod.theVtxMeshes.Count - 1
					aVtxMesh = aVtxLod.theVtxMeshes(meshIndex)
					materialIndex = aModel.theMeshes(meshIndex).materialIndex
					materialName = Me.theMdlFileData.theTextures(materialIndex).thePathFileName
					'TODO: This was used in previous versions, but maybe should leave as above.
					'materialName = Path.GetFileName(Me.theSourceEngineModel.theMdlFileHeader.theTextures(materialIndex).theName)

					meshVertexIndexStart = aModel.theMeshes(meshIndex).vertexIndexStart

					If aVtxMesh.theVtxStripGroups IsNot Nothing Then
						For groupIndex As Integer = 0 To aVtxMesh.theVtxStripGroups.Count - 1
							aStripGroup = aVtxMesh.theVtxStripGroups(groupIndex)

							If aStripGroup.theVtxStrips IsNot Nothing AndAlso aStripGroup.theVtxIndexes IsNot Nothing AndAlso aStripGroup.theVtxVertexes IsNot Nothing Then
								For vtxIndexIndex As Integer = 0 To aStripGroup.theVtxIndexes.Count - 3 Step 3
									''NOTE: studiomdl.exe will complain if texture name for eyeball is not at start of line.
									'line = materialName
									'Me.theOutputFileStreamWriter.WriteLine(line)
									'Me.WriteVertexLine(aStripGroup, vtxIndexIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
									'Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
									'Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
									'------
									'NOTE: studiomdl.exe will complain if texture name for eyeball is not at start of line.
									materialLine = materialName
									vertex1Line = Me.WriteVertexLine(aStripGroup, vtxIndexIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
									vertex2Line = Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
									vertex3Line = Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
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
				Next
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteTrianglesSectionForPhysics()
		Dim line As String = ""

		'triangles
		line = "triangles"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Dim collisionData As SourcePhyCollisionData
		Dim aBone As SourceMdlBone31
		Dim boneIndex As Integer
		Dim aTriangle As SourcePhyFace
		Dim faceSection As SourcePhyFaceSection
		Dim phyVertex As SourcePhyVertex
		Dim aVectorTransformed As SourceVector
		Dim aSourcePhysCollisionModel As SourcePhyPhysCollisionModel

		Try
			If Me.thePhyFileData.theSourcePhyCollisionDatas IsNot Nothing Then
				For collisionDataIndex As Integer = 0 To Me.thePhyFileData.theSourcePhyCollisionDatas.Count - 1
					collisionData = Me.thePhyFileData.theSourcePhyCollisionDatas(collisionDataIndex)

					If collisionDataIndex < Me.thePhyFileData.theSourcePhyPhysCollisionModels.Count Then
						aSourcePhysCollisionModel = Me.thePhyFileData.theSourcePhyPhysCollisionModels(collisionDataIndex)
					Else
						aSourcePhysCollisionModel = Nothing
					End If

					For faceSectionIndex As Integer = 0 To collisionData.theFaceSections.Count - 1
						faceSection = collisionData.theFaceSections(faceSectionIndex)

						If faceSection.theBoneIndex >= Me.theMdlFileData.theBones.Count Then
							Continue For
						End If
						If aSourcePhysCollisionModel IsNot Nothing AndAlso Me.theMdlFileData.theBoneNameToBoneIndexMap.ContainsKey(aSourcePhysCollisionModel.theName) Then
							boneIndex = Me.theMdlFileData.theBoneNameToBoneIndexMap(aSourcePhysCollisionModel.theName)
						Else
							boneIndex = faceSection.theBoneIndex
						End If
						aBone = Me.theMdlFileData.theBones(faceSection.theBoneIndex)

						For triangleIndex As Integer = 0 To faceSection.theFaces.Count - 1
							aTriangle = faceSection.theFaces(triangleIndex)

							line = "  phy"
							Me.theOutputFileStreamWriter.WriteLine(line)

							'  19 -0.000009 0.000001 0.999953 0.0 0.0 0.0 1 0
							'  19 -0.000005 1.000002 -0.000043 0.0 0.0 0.0 1 0
							'  19 -0.008333 0.997005 1.003710 0.0 0.0 0.0 1 0
							'NOTE: MDL Decompiler 0.4.1 lists the vertices in reverse order than they are stored, and this seems to match closely with the teenangst source file.
							'For vertexIndex As Integer = aTriangle.vertexIndex.Length - 1 To 0 Step -1
							For vertexIndex As Integer = 0 To aTriangle.vertexIndex.Length - 1
								phyVertex = faceSection.theVertices(aTriangle.vertexIndex(vertexIndex))

								aVectorTransformed = Me.TransformPhyVertex(aBone, phyVertex.vertex, aSourcePhysCollisionModel)

								line = "    "
								line += boneIndex.ToString(TheApp.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.x.ToString("0.000000", TheApp.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.y.ToString("0.000000", TheApp.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.z.ToString("0.000000", TheApp.InternalNumberFormat)

								'line += " 0 0 0"
								'------
								line += " "
								line += phyVertex.Normal.x.ToString("0.000000", TheApp.InternalNumberFormat)
								line += " "
								line += phyVertex.Normal.y.ToString("0.000000", TheApp.InternalNumberFormat)
								line += " "
								line += phyVertex.Normal.z.ToString("0.000000", TheApp.InternalNumberFormat)

								line += " 0 0"
								'NOTE: The studiomdl.exe doesn't need the integer values at end.
								'line += " 1 0"
								Me.theOutputFileStreamWriter.WriteLine(line)
							Next
						Next
					Next
				Next
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	''TODO: Write the firstAnimDesc's first frame's frameLines because it is used for "subtract" option.
	'Public Sub CalculateFirstAnimDescFrameLinesForSubtract()
	'	Dim boneIndex As Integer
	'	Dim aFrameLine As AnimationFrameLine
	'	Dim frameIndex As Integer
	'	Dim aSequenceDesc As SourceMdlSequenceDesc31
	'	Dim anAnimationDesc As SourceMdlAnimationDesc31

	'	aSequenceDesc = Nothing
	'	anAnimationDesc = Me.theMdlFileData.theFirstAnimationDesc

	'	Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
	'	frameIndex = 0
	'	Me.theAnimationFrameLines.Clear()
	'	If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0 Then
	'		Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex)
	'	End If

	'	For i As Integer = 0 To Me.theAnimationFrameLines.Count - 1
	'		boneIndex = Me.theAnimationFrameLines.Keys(i)
	'		aFrameLine = Me.theAnimationFrameLines.Values(i)

	'		Dim aFirstAnimationDescFrameLine As New AnimationFrameLine()
	'		aFirstAnimationDescFrameLine.rotation = New SourceVector()
	'		aFirstAnimationDescFrameLine.position = New SourceVector()

	'		'NOTE: Only rotate by -90 deg if bone is a root bone.  Do not know why.
	'		'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
	'		'TEST: Try this version, because of "sequence_blend from Game Zombie" model.
	'		aFirstAnimationDescFrameLine.rotation.x = aFrameLine.rotation.x
	'		aFirstAnimationDescFrameLine.rotation.y = aFrameLine.rotation.y
	'		If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 AndAlso (aFrameLine.rotation.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone") Then
	'			Dim z As Double
	'			z = aFrameLine.rotation.z
	'			z += MathModule.DegreesToRadians(-90)
	'			aFirstAnimationDescFrameLine.rotation.z = z
	'		Else
	'			aFirstAnimationDescFrameLine.rotation.z = aFrameLine.rotation.z
	'		End If

	'		'NOTE: Only adjust position if bone is a root bone. Do not know why.
	'		'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
	'		'TEST: Try this version, because of "sequence_blend from Game Zombie" model.
	'		If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 AndAlso (aFrameLine.position.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone") Then
	'			aFirstAnimationDescFrameLine.position.x = aFrameLine.position.y
	'			aFirstAnimationDescFrameLine.position.y = (-aFrameLine.position.x)
	'			aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
	'		Else
	'			aFirstAnimationDescFrameLine.position.x = aFrameLine.position.x
	'			aFirstAnimationDescFrameLine.position.y = aFrameLine.position.y
	'			aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
	'		End If

	'		Me.theMdlFileData.theFirstAnimationDescFrameLines.Add(boneIndex, aFirstAnimationDescFrameLine)
	'	Next
	'End Sub

	Public Sub WriteSkeletonSectionForAnimation(ByVal aSequenceDescBase As SourceMdlSequenceDescBase, ByVal anAnimationDescBase As SourceMdlAnimationDescBase)
		Dim line As String = ""
		Dim boneIndex As Integer
		Dim aFrameLine As AnimationFrameLine
		Dim position As New SourceVector()
		Dim rotation As New SourceVector()
		Dim aSequenceDesc As SourceMdlSequenceDesc31
		Dim anAnimationDesc As SourceMdlAnimationDesc31

		aSequenceDesc = CType(aSequenceDescBase, SourceMdlSequenceDesc31)
		anAnimationDesc = CType(anAnimationDescBase, SourceMdlAnimationDesc31)

		'skeleton
		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
		For frameIndex As Integer = 0 To anAnimationDesc.frameCount - 1
			Me.theAnimationFrameLines.Clear()
			Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex)

			If TheApp.Settings.DecompileStricterFormatIsChecked Then
				line = "time "
			Else
				line = "  time "
			End If
			line += CStr(frameIndex)
			Me.theOutputFileStreamWriter.WriteLine(line)

			For i As Integer = 0 To Me.theAnimationFrameLines.Count - 1
				boneIndex = Me.theAnimationFrameLines.Keys(i)
				aFrameLine = Me.theAnimationFrameLines.Values(i)

				'position.x = aFrameLine.position.x
				'position.y = aFrameLine.position.y
				'position.z = aFrameLine.position.z

				'rotation.x = aFrameLine.rotation.x
				'rotation.y = aFrameLine.rotation.y
				'rotation.z = aFrameLine.rotation.z
				'------
				Dim adjustedPosition As New SourceVector()
				Dim adjustedRotation As New SourceVector()
				Me.AdjustPositionAndRotationByPiecewiseMovement(frameIndex, boneIndex, anAnimationDesc.theMovements, aFrameLine.position, aFrameLine.rotation, adjustedPosition, adjustedRotation)
				Me.AdjustPositionAndRotation(boneIndex, adjustedPosition, adjustedRotation, position, rotation)

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

				If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
					line += "   # "
					line += "pos: "
					line += aFrameLine.position.debug_text
					line += "   "
					line += "rot: "
					line += aFrameLine.rotation.debug_text
				End If

				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

	Private Sub AdjustPositionAndRotationByPiecewiseMovement(ByVal frameIndex As Integer, ByVal boneIndex As Integer, ByVal movements As List(Of SourceMdlMovement), ByVal iPosition As SourceVector, ByVal iRotation As SourceVector, ByRef oPosition As SourceVector, ByRef oRotation As SourceVector)
		Dim aBone As SourceMdlBone31
		aBone = Me.theMdlFileData.theBones(boneIndex)

		oPosition.x = iPosition.x
		oPosition.y = iPosition.y
		oPosition.z = iPosition.z
		oPosition.debug_text = iPosition.debug_text
		oRotation.x = iRotation.x
		oRotation.y = iRotation.y
		oRotation.z = iRotation.z
		oRotation.debug_text = iRotation.debug_text

		If aBone.parentBoneIndex = -1 Then
			If movements IsNot Nothing AndAlso frameIndex > 0 Then
				Dim previousFrameIndex As Integer
				Dim vecPos As SourceVector
				Dim vecAngle As SourceVector

				previousFrameIndex = 0
				vecPos = New SourceVector()
				vecAngle = New SourceVector()

				For Each aMovement As SourceMdlMovement In movements
					If frameIndex <= aMovement.endframeIndex Then
						Dim f As Double
						Dim d As Double
						f = (frameIndex - previousFrameIndex) / (aMovement.endframeIndex - previousFrameIndex)
						d = aMovement.v0 * f + 0.5 * (aMovement.v1 - aMovement.v0) * f * f
						vecPos.x = vecPos.x + d * aMovement.vector.x
						vecPos.y = vecPos.y + d * aMovement.vector.y
						vecPos.z = vecPos.z + d * aMovement.vector.z
						vecAngle.y = vecAngle.y * (1 - f) + MathModule.DegreesToRadians(aMovement.angle) * f

						Exit For
					Else
						previousFrameIndex = aMovement.endframeIndex
						vecPos.x = aMovement.position.x
						vecPos.y = aMovement.position.y
						vecPos.z = aMovement.position.z
						vecAngle.y = MathModule.DegreesToRadians(aMovement.angle)
					End If
				Next

				Dim tmp As New SourceVector()
				tmp.x = iPosition.x + vecPos.x
				tmp.y = iPosition.y + vecPos.y
				tmp.z = iPosition.z + vecPos.z
				'oRotation.z = iRotation.z + vecAngle.y
				'oPosition = MathModule.VectorYawRotate(tmp, -vecAngle.y)
			End If
		End If
	End Sub

	Private Sub AdjustPositionAndRotation(ByVal boneIndex As Integer, ByVal iPosition As SourceVector, ByVal iRotation As SourceVector, ByRef oPosition As SourceVector, ByRef oRotation As SourceVector)
		Dim aBone As SourceMdlBone31
		aBone = Me.theMdlFileData.theBones(boneIndex)

		If aBone.parentBoneIndex = -1 Then
			oPosition.x = iPosition.y
			oPosition.y = -iPosition.x
			oPosition.z = iPosition.z
		Else
			oPosition.x = iPosition.x
			oPosition.y = iPosition.y
			oPosition.z = iPosition.z
		End If

		If aBone.parentBoneIndex = -1 Then
			oRotation.x = iRotation.x
			oRotation.y = iRotation.y
			oRotation.z = iRotation.z + MathModule.DegreesToRadians(-90)
		Else
			oRotation.x = iRotation.x
			oRotation.y = iRotation.y
			oRotation.z = iRotation.z
		End If
	End Sub

	Private Function WriteVertexLine(ByVal aStripGroup As SourceVtxStripGroup06, ByVal aVtxIndexIndex As Integer, ByVal lodIndex As Integer, ByVal meshVertexIndexStart As Integer, ByVal bodyPartVertexIndexStart As Integer) As String
		Dim aVtxVertexIndex As UShort
		Dim aVtxVertex As SourceVtxVertex06
		Dim aVertex As SourceMdlVertex31
		Dim vertexIndex As Integer
		Dim line As String

		line = ""
		Try
			aVtxVertexIndex = aStripGroup.theVtxIndexes(aVtxIndexIndex)
			aVtxVertex = aStripGroup.theVtxVertexes(aVtxVertexIndex)
			vertexIndex = aVtxVertex.originalMeshVertexIndex + bodyPartVertexIndexStart + meshVertexIndexStart
			'If Me.theVvdFileData.fixupCount = 0 Then
			'	aVertex = Me.theVvdFileData.theVertexes(vertexIndex)
			'Else
			'	'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
			'	'      Maybe the listing by lodIndex is only needed internally by graphics engine.
			'	'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
			'	aVertex = Me.theVvdFileData.theFixedVertexesByLod(0)(vertexIndex)
			'	'aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
			'End If
			aVertex = Me.theMdlFileData.theBodyParts(0).theModels(0).theVertexes(vertexIndex)

			line = "  "
			line += aVertex.boneWeight.bone(0).ToString(TheApp.InternalNumberFormat)

			line += " "
			If (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
				line += aVertex.position.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += (-aVertex.position.x).ToString("0.000000", TheApp.InternalNumberFormat)
			Else
				line += aVertex.position.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.position.y.ToString("0.000000", TheApp.InternalNumberFormat)
			End If
			line += " "
			line += aVertex.position.z.ToString("0.000000", TheApp.InternalNumberFormat)

			line += " "
			If (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
				line += aVertex.normal.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += (-aVertex.normal.x).ToString("0.000000", TheApp.InternalNumberFormat)
			Else
				line += aVertex.normal.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.normal.y.ToString("0.000000", TheApp.InternalNumberFormat)
			End If
			line += " "
			line += aVertex.normal.z.ToString("0.000000", TheApp.InternalNumberFormat)

			line += " "
			line += aVertex.texCoordX.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			'line += aVertex.texCoordY.ToString("0.000000", TheApp.InternalNumberFormat)
			line += (1 - aVertex.texCoordY).ToString("0.000000", TheApp.InternalNumberFormat)

			line += " "
			line += aVertex.boneWeight.boneCount.ToString(TheApp.InternalNumberFormat)
			For boneWeightBoneIndex As Integer = 0 To aVertex.boneWeight.boneCount - 1
				line += " "
				line += aVertex.boneWeight.bone(boneWeightBoneIndex).ToString(TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.boneWeight.weight(boneWeightBoneIndex).ToString("0.000000", TheApp.InternalNumberFormat)
			Next
			'Me.theOutputFileStreamWriter.WriteLine(line)
		Catch ex As Exception
			line = "// " + line
		End Try

		Return line
	End Function

	Private Function TransformPhyVertex(ByVal aBone As SourceMdlBone31, ByVal vertex As SourceVector, ByVal aSourcePhysCollisionModel As SourcePhyPhysCollisionModel) As SourceVector
		Dim aVectorTransformed As New SourceVector
		Dim aVector As New SourceVector()

		'If Me.thePhyFileData.theSourcePhyIsCollisionModel Then
		'	aVectorTransformed.x = 1 / 0.0254 * vertex.z
		'	aVectorTransformed.y = 1 / 0.0254 * -vertex.x
		'	aVectorTransformed.z = 1 / 0.0254 * -vertex.y
		'Else
		'	aVector.x = 1 / 0.0254 * vertex.x
		'	aVector.y = 1 / 0.0254 * vertex.z
		'	aVector.z = 1 / 0.0254 * -vertex.y
		'	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'End If
		aVector.x = 1 / 0.0254 * vertex.x
		aVector.y = 1 / 0.0254 * vertex.z
		aVector.z = 1 / 0.0254 * -vertex.y
		If aSourcePhysCollisionModel IsNot Nothing Then
			If Me.theMdlFileData.theBoneNameToBoneIndexMap.ContainsKey(aSourcePhysCollisionModel.theName) Then
				aBone = Me.theMdlFileData.theBones(Me.theMdlFileData.theBoneNameToBoneIndexMap(aSourcePhysCollisionModel.theName))
			Else
				aVectorTransformed.x = 1 / 0.0254 * vertex.z
				aVectorTransformed.y = 1 / 0.0254 * -vertex.x
				aVectorTransformed.z = 1 / 0.0254 * -vertex.y
				Return aVectorTransformed
			End If
		End If
		aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

		Return aVectorTransformed
	End Function

	Private Sub CalcAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc31, ByVal anAnimationDesc As SourceMdlAnimationDesc31, ByVal frameIndex As Integer)
		Dim s As Double
		Dim aBone As SourceMdlBone31
		Dim anAnimation As SourceMdlAnimation31
		Dim rot As SourceVector
		Dim pos As SourceVector
		Dim aFrameLine As AnimationFrameLine

		s = 0

		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			aBone = Me.theMdlFileData.theBones(boneIndex)
			anAnimation = anAnimationDesc.theAnimations(boneIndex)

			If anAnimation IsNot Nothing Then
				If Me.theAnimationFrameLines.ContainsKey(boneIndex) Then
					aFrameLine = Me.theAnimationFrameLines(boneIndex)
				Else
					aFrameLine = New AnimationFrameLine()
					Me.theAnimationFrameLines.Add(boneIndex, aFrameLine)
				End If

				aFrameLine.rotationQuat = New SourceQuaternion()
				rot = CalcBoneRotation(frameIndex, s, aBone, anAnimation, aFrameLine.rotationQuat)
				aFrameLine.rotation = New SourceVector()

				aFrameLine.rotation.x = rot.x
				aFrameLine.rotation.y = rot.y
				aFrameLine.rotation.z = rot.z

				aFrameLine.rotation.debug_text = rot.debug_text

				pos = Me.CalcBonePosition(frameIndex, s, aBone, anAnimation)
				aFrameLine.position = New SourceVector()
				aFrameLine.position.x = pos.x
				aFrameLine.position.y = pos.y
				aFrameLine.position.z = pos.z
				aFrameLine.position.debug_text = pos.debug_text
			End If
		Next
	End Sub

	Private Function CalcBoneRotation(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone31, ByVal anAnimation As SourceMdlAnimation31, ByRef rotationQuat As SourceQuaternion) As SourceVector
		Dim rot As New SourceQuaternion()
		Dim angleVector As New SourceVector()

		If (aBone.flags And SourceMdlBone2531.STUDIO_PROC_AXISINTERP) > 0 Then
			angleVector.x = 0
			angleVector.y = 0
			angleVector.z = 0

			angleVector.debug_text = "AXISINTERP"
		Else
			If anAnimation.theOffsets(3) <= 0 OrElse (aBone.flags And SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0 Then
				rot.x = aBone.rotation.x
			Else
				rot.x = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationXValues, aBone.rotationScale.x, 0)
			End If
			If anAnimation.theOffsets(4) <= 0 OrElse (aBone.flags And SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0 Then
				rot.y = aBone.rotation.y
			Else
				rot.y = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationYValues, aBone.rotationScale.y, 0)
			End If
			If anAnimation.theOffsets(5) <= 0 OrElse (aBone.flags And SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0 Then
				rot.z = aBone.rotation.z
			Else
				rot.z = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationZValues, aBone.rotationScale.z, 0)
			End If
			'rot.w = 1

			'angleVector = MathModule.ToEulerAngles2531(rot)

			angleVector.x = rot.x
			angleVector.y = rot.y
			angleVector.z = rot.z
			angleVector.debug_text = "anim"
		End If

		rotationQuat = rot

		Return angleVector
	End Function

	Private Function CalcBonePosition(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone31, ByVal anAnimation As SourceMdlAnimation31) As SourceVector
		Dim pos As New SourceVector()

		If anAnimation.theOffsets(0) <= 0 Then
			pos.x = aBone.position.x
		Else
			pos.x = Me.ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationXValues, aBone.positionScale.x, aBone.position.x)
		End If

		If anAnimation.theOffsets(1) <= 0 Then
			pos.y = aBone.position.y
		Else
			pos.y = Me.ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationYValues, aBone.positionScale.y, aBone.position.y)
		End If

		If anAnimation.theOffsets(2) <= 0 Then
			pos.z = aBone.position.z
		Else
			pos.z = Me.ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationZValues, aBone.positionScale.z, aBone.position.z)
		End If

		pos.debug_text = "anim"

		Return pos
	End Function

	Public Function ExtractAnimValue(ByVal frameIndex As Integer, ByVal animValues As List(Of SourceMdlAnimationValue2531), ByVal scale As Double, ByVal adjustment As Double) As Double
		Dim v1 As Double
		' k is frameCountRemainingToBeChecked
		Dim k As Integer
		Dim animValueIndex As Integer

		Try
			k = frameIndex
			animValueIndex = 0
			While animValues(animValueIndex).total <= k
				k -= animValues(animValueIndex).total
				animValueIndex += animValues(animValueIndex).valid + 1
				If animValueIndex >= animValues.Count OrElse animValues(animValueIndex).total = 0 Then
					'NOTE: Bad if it reaches here. This means maybe a newer format of the anim data was used for the model.
					v1 = 0
					Return v1
				End If
			End While

			If animValues(animValueIndex).valid > k Then
				'NOTE: The animValues index needs to be offset from current animValues index to match the C++ code above in comment.
				' value[n] = ( sequence[i].panim[q]->pos[j][n][k] - bonetable[j].pos[k] ) / bonetable[j].posscale[k]; 
				'	v = ( sequence[i].panim[q]->rot[j][n][k-3] - bonetable[j].rot[k-3] ); 
				'	if (v >= Q_PI)
				'		v -= Q_PI * 2;
				'	if (v < -Q_PI)
				'		v += Q_PI * 2;
				'	value[n] = v / bonetable[j].rotscale[k-3]; 
				v1 = animValues(animValueIndex + k + 1).value * scale + adjustment
			Else
				'NOTE: The animValues index needs to be offset from current animValues index to match the C++ code above in comment.
				v1 = animValues(animValueIndex + animValues(animValueIndex).valid).value * scale + adjustment
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return v1
	End Function

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	'Private theAniFileData As SourceAniFileData44
	Private theMdlFileData As SourceMdlFileData31
	Private thePhyFileData As SourcePhyFileData
	'Private theVtxFileData As SourceVtxFileData44
	'Private theVvdFileData As SourceVvdFileData37
	'Private theModelName As String

	Private theAnimationFrameLines As SortedList(Of Integer, AnimationFrameLine)

#End Region

End Class
