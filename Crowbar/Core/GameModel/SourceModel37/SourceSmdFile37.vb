Imports System.IO

Public Class SourceSmdFile37

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData37)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
	End Sub

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData37, ByVal phyFileData As SourcePhyFileData)
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

	Public Sub WriteTrianglesSection(ByVal lodIndex As Integer, ByVal aVtxModel As SourceVtxModel06, ByVal aModel As SourceMdlModel37, ByVal bodyPartVertexIndexStart As Integer)
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
					materialName = Me.theMdlFileData.theTextures(materialIndex).theFileName
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
									vertex1Line = Me.WriteVertexLine(aStripGroup, vtxIndexIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart, aModel)
									vertex2Line = Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart, aModel)
									vertex3Line = Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart, aModel)
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
		Catch

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
		Dim aBone As SourceMdlBone37
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
						aBone = Me.theMdlFileData.theBones(boneIndex)

						For triangleIndex As Integer = 0 To faceSection.theFaces.Count - 1
							aTriangle = faceSection.theFaces(triangleIndex)

							line = "  phy"
							Me.theOutputFileStreamWriter.WriteLine(line)

							For vertexIndex As Integer = 0 To aTriangle.vertexIndex.Length - 1
								'phyVertex = collisionData.theVertices(aTriangle.vertexIndex(vertexIndex))
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

	'TODO: Write the firstAnimDesc's first frame's frameLines because it is used for "subtract" option.
	Public Sub CalculateFirstAnimDescFrameLinesForSubtract()
		Dim boneIndex As Integer
		Dim aFrameLine As AnimationFrameLine
		Dim frameIndex As Integer
		Dim aSequenceDesc As SourceMdlSequenceDesc
		Dim anAnimationDesc As SourceMdlAnimationDesc37

		aSequenceDesc = Nothing
		anAnimationDesc = Me.theMdlFileData.theFirstAnimationDesc

		Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
		frameIndex = 0
		Me.theAnimationFrameLines.Clear()
		If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0 Then
			Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex)
		End If

		For i As Integer = 0 To Me.theAnimationFrameLines.Count - 1
			boneIndex = Me.theAnimationFrameLines.Keys(i)
			aFrameLine = Me.theAnimationFrameLines.Values(i)

			Dim aFirstAnimationDescFrameLine As New AnimationFrameLine()
			aFirstAnimationDescFrameLine.rotation = New SourceVector()
			aFirstAnimationDescFrameLine.position = New SourceVector()

			'NOTE: Only rotate by -90 deg if bone is a root bone.  Do not know why.
			'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
			'TEST: Try this version, because of "sequence_blend from Game Zombie" model.
			aFirstAnimationDescFrameLine.rotation.x = aFrameLine.rotation.x
			aFirstAnimationDescFrameLine.rotation.y = aFrameLine.rotation.y
			If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 AndAlso (aFrameLine.rotation.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone") Then
				Dim z As Double
				z = aFrameLine.rotation.z
				z += MathModule.DegreesToRadians(-90)
				aFirstAnimationDescFrameLine.rotation.z = z
			Else
				aFirstAnimationDescFrameLine.rotation.z = aFrameLine.rotation.z
			End If

			'NOTE: Only adjust position if bone is a root bone. Do not know why.
			'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
			'TEST: Try this version, because of "sequence_blend from Game Zombie" model.
			If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 AndAlso (aFrameLine.position.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone") Then
				aFirstAnimationDescFrameLine.position.x = aFrameLine.position.y
				aFirstAnimationDescFrameLine.position.y = (-aFrameLine.position.x)
				aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
			Else
				aFirstAnimationDescFrameLine.position.x = aFrameLine.position.x
				aFirstAnimationDescFrameLine.position.y = aFrameLine.position.y
				aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
			End If

			Me.theMdlFileData.theFirstAnimationDescFrameLines.Add(boneIndex, aFirstAnimationDescFrameLine)
		Next
	End Sub

	Public Sub WriteSkeletonSectionForAnimation(ByVal aSequenceDescBase As SourceMdlSequenceDescBase, ByVal anAnimationDescBase As SourceMdlAnimationDescBase)
		Dim line As String = ""
		Dim boneIndex As Integer
		Dim aFrameLine As AnimationFrameLine
		Dim endFrameLine As AnimationFrameLine
		'Dim previousFrameRootBonePosition As New SourceVector()
		Dim position As New SourceVector()
		Dim previousFrameRootBoneRotation As New SourceVector()
		Dim rotation As New SourceVector()
		Dim aSequenceDesc As SourceMdlSequenceDesc
		Dim anAnimationDesc As SourceMdlAnimationDesc37
		'Dim tempValue As Double

		aSequenceDesc = CType(aSequenceDescBase, SourceMdlSequenceDesc)
		anAnimationDesc = CType(anAnimationDescBase, SourceMdlAnimationDesc37)

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

			endFrameLine = Me.theAnimationFrameLines.Values(Me.theAnimationFrameLines.Count - 1)
			For i As Integer = 0 To Me.theAnimationFrameLines.Count - 1
				boneIndex = Me.theAnimationFrameLines.Keys(i)
				aFrameLine = Me.theAnimationFrameLines.Values(i)

				'position.x = aFrameLine.position.x
				'position.y = aFrameLine.position.y
				'position.z = aFrameLine.position.z

				'If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 Then
				'	If anAnimationDesc.theMovements IsNot Nothing Then
				'		'Dim perFrameMovement As Double
				'		Dim startFrameIndex As Integer = 0
				'		Dim endFrameIndex As Integer = 0

				'		'If frameIndex = 0 Then
				'		'	previousFrameRootBonePosition.x = position.x
				'		'	previousFrameRootBonePosition.y = position.y
				'		'	previousFrameRootBonePosition.z = position.z
				'		'End If

				'		For Each aMovement As SourceMdlMovement In anAnimationDesc.theMovements
				'			endFrameIndex = aMovement.endframeIndex

				'			'If frameIndex >= startFrameIndex AndAlso frameIndex <= endFrameIndex Then
				'			'	If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LX) > 0 Then
				'			'		perFrameMovement = aMovement.position.x / (endFrameIndex - startFrameIndex)
				'			'		position.x = previousFrameRootBonePosition.x + perFrameMovement
				'			'		aFrameLine.position.debug_text += " [x]"
				'			'	End If
				'			'	If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LY) > 0 Then
				'			'		perFrameMovement = aMovement.position.y / (endFrameIndex - startFrameIndex)
				'			'		position.y = previousFrameRootBonePosition.y + perFrameMovement
				'			'		aFrameLine.position.debug_text += " [y]"
				'			'	End If
				'			'	If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZ) > 0 Then
				'			'		perFrameMovement = aMovement.position.z / (endFrameIndex - startFrameIndex)
				'			'		position.z = previousFrameRootBonePosition.z + perFrameMovement
				'			'		aFrameLine.position.debug_text += " [z]"
				'			'	End If
				'			'End If
				'			'------
				'			'Dim t As Double
				'			'Dim scale As Double
				'			't = frameIndex / (aMovement.endframeIndex - startFrameIndex + 1)
				'			'scale = aMovement.v0 * t + 0.5 * (aMovement.v1 - aMovement.v0) * t * t
				'			'If scale <> 0 Then
				'			'	scale = 1 / scale
				'			'End If
				'			If frameIndex >= startFrameIndex AndAlso frameIndex <= endFrameIndex Then
				'				If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LX) > 0 Then
				'					'position.x = aFrameLine.position.x + scale * endFrameLine.position.x
				'					position.x = aFrameLine.position.x + aMovement.position.x
				'					aFrameLine.position.debug_text += " [x]"
				'				End If
				'				If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LY) > 0 Then
				'					'position.y = aFrameLine.position.y + scale * endFrameLine.position.y
				'					position.y = aFrameLine.position.y + aMovement.position.y
				'					aFrameLine.position.debug_text += " [y]"
				'				End If
				'				If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZ) > 0 Then
				'					'position.z = aFrameLine.position.z + scale * endFrameLine.position.z
				'					position.z = aFrameLine.position.z + aMovement.position.z
				'					aFrameLine.position.debug_text += " [z]"
				'				End If
				'			End If

				'			startFrameIndex = endFrameIndex + 1
				'		Next

				'		'If frameIndex > endFrameIndex AndAlso frameIndex < anAnimationDesc.frameCount Then
				'		'	position.x = previousFrameRootBonePosition.x
				'		'	position.y = previousFrameRootBonePosition.y
				'		'	position.z = previousFrameRootBonePosition.z
				'		'End If

				'		'previousFrameRootBonePosition.x = position.x
				'		'previousFrameRootBonePosition.y = position.y
				'		'previousFrameRootBonePosition.z = position.z
				'	End If

				'	tempValue = position.x
				'	position.x = position.y
				'	position.y = -tempValue
				'End If

				'rotation.x = aFrameLine.rotation.x
				'rotation.y = aFrameLine.rotation.y
				'rotation.z = aFrameLine.rotation.z
				'If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 Then
				'	'If anAnimationDesc.theMovements IsNot Nothing Then
				'	'	Dim perFrameMovement As Double
				'	'	Dim startFrameIndex As Integer = 0
				'	'	Dim endFrameIndex As Integer = 0

				'	'	If frameIndex = 0 Then
				'	'		previousFrameRootBoneRotation.x = rotation.x
				'	'		previousFrameRootBoneRotation.y = rotation.y
				'	'		previousFrameRootBoneRotation.z = rotation.z
				'	'	End If

				'	'	For Each aMovement As SourceMdlMovement In anAnimationDesc.theMovements
				'	'		endFrameIndex = aMovement.endframeIndex

				'	'		If frameIndex >= startFrameIndex AndAlso frameIndex <= aMovement.endframeIndex Then
				'	'			If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LXR) > 0 Then
				'	'				perFrameMovement = MathModule.DegreesToRadians(aMovement.angle) / (endFrameIndex - startFrameIndex)
				'	'				rotation.x = previousFrameRootBoneRotation.x + perFrameMovement
				'	'				aFrameLine.rotation.debug_text += " [x]"
				'	'			End If
				'	'			If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LYR) > 0 Then
				'	'				perFrameMovement = MathModule.DegreesToRadians(aMovement.angle) / (endFrameIndex - startFrameIndex)
				'	'				rotation.y = previousFrameRootBoneRotation.y + perFrameMovement
				'	'				aFrameLine.rotation.debug_text += " [y]"
				'	'			End If
				'	'			If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZR) > 0 Then
				'	'				perFrameMovement = MathModule.DegreesToRadians(aMovement.angle) / (endFrameIndex - startFrameIndex)
				'	'				rotation.z = previousFrameRootBoneRotation.z + perFrameMovement
				'	'				aFrameLine.rotation.debug_text += " [z]"
				'	'			End If
				'	'		End If
				'	'	Next

				'	'	If frameIndex > endFrameIndex AndAlso frameIndex < anAnimationDesc.frameCount Then
				'	'		rotation.x = previousFrameRootBoneRotation.x
				'	'		rotation.y = previousFrameRootBoneRotation.y
				'	'		rotation.z = previousFrameRootBoneRotation.z
				'	'	End If

				'	'	previousFrameRootBoneRotation.x = rotation.x
				'	'	previousFrameRootBoneRotation.y = rotation.y
				'	'	previousFrameRootBoneRotation.z = rotation.z
				'	'End If

				'	rotation.z = aFrameLine.rotation.z + MathModule.DegreesToRadians(-90)
				'End If
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
		Dim aBone As SourceMdlBone37
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

				'TEST: Testing this in SourceSmdFile49.
				'Dim tmp As New SourceVector()
				'tmp.x = iPosition.x + vecPos.x
				'tmp.y = iPosition.y + vecPos.y
				'tmp.z = iPosition.z + vecPos.z
				''oRotation.z = iRotation.z + vecAngle.y
				''oPosition = MathModule.VectorYawRotate(tmp, -vecAngle.y)
				oPosition.x = iPosition.x + vecPos.x
				oPosition.y = iPosition.y + vecPos.y
				oPosition.z = iPosition.z + vecPos.z
				oRotation.z = iRotation.z + vecAngle.y
			End If
		End If
	End Sub

	Private Sub AdjustPositionAndRotation(ByVal boneIndex As Integer, ByVal iPosition As SourceVector, ByVal iRotation As SourceVector, ByRef oPosition As SourceVector, ByRef oRotation As SourceVector)
		Dim aBone As SourceMdlBone37
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

	Private Function WriteVertexLine(ByVal aStripGroup As SourceVtxStripGroup06, ByVal aVtxIndexIndex As Integer, ByVal lodIndex As Integer, ByVal meshVertexIndexStart As Integer, ByVal bodyPartVertexIndexStart As Integer, ByVal aBodyModel As SourceMdlModel37) As String
		Dim aVtxVertexIndex As UShort
		Dim aVtxVertex As SourceVtxVertex06
		Dim aVertex As SourceMdlVertex37
		Dim vertexIndex As Integer
		Dim line As String

		line = ""
		Try
			aVtxVertexIndex = aStripGroup.theVtxIndexes(aVtxIndexIndex)
			aVtxVertex = aStripGroup.theVtxVertexes(aVtxVertexIndex)
			'vertexIndex = aVtxVertex.originalMeshVertexIndex + bodyPartVertexIndexStart + meshVertexIndexStart
			'aVertex = Me.theMdlFileData.theBodyParts(0).theModels(0).theVertexes(vertexIndex)
			vertexIndex = aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart
			aVertex = aBodyModel.theVertexes(vertexIndex)

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

	Private Function TransformPhyVertex(ByVal aBone As SourceMdlBone37, ByVal vertex As SourceVector, ByVal aSourcePhysCollisionModel As SourcePhyPhysCollisionModel) As SourceVector
		Dim aVectorTransformed As New SourceVector
		Dim aVector As New SourceVector()

		'aVector.x = 1 / 0.0254 * vertex.x
		'aVector.y = 1 / 0.0254 * vertex.z
		'aVector.z = 1 / 0.0254 * -vertex.y
		'Dim aParentBone As SourceMdlBone37
		'Dim aChildBone As SourceMdlBone37
		'Dim parentBoneIndex As Integer
		'Dim inputBoneMatrixColumn0 As New SourceVector()
		'Dim inputBoneMatrixColumn1 As New SourceVector()
		'Dim inputBoneMatrixColumn2 As New SourceVector()
		'Dim inputBoneMatrixColumn3 As New SourceVector()
		'Dim boneMatrixColumn0 As New SourceVector()
		'Dim boneMatrixColumn1 As New SourceVector()
		'Dim boneMatrixColumn2 As New SourceVector()
		'Dim boneMatrixColumn3 As New SourceVector()

		'aChildBone = aBone
		'inputBoneMatrixColumn0.x = aChildBone.poseToBoneColumn0.x
		'inputBoneMatrixColumn0.y = aChildBone.poseToBoneColumn0.y
		'inputBoneMatrixColumn0.z = aChildBone.poseToBoneColumn0.z
		'inputBoneMatrixColumn1.x = aChildBone.poseToBoneColumn1.x
		'inputBoneMatrixColumn1.y = aChildBone.poseToBoneColumn1.y
		'inputBoneMatrixColumn1.z = aChildBone.poseToBoneColumn1.z
		'inputBoneMatrixColumn2.x = aChildBone.poseToBoneColumn2.x
		'inputBoneMatrixColumn2.y = aChildBone.poseToBoneColumn2.y
		'inputBoneMatrixColumn2.z = aChildBone.poseToBoneColumn2.z
		'inputBoneMatrixColumn3.x = aChildBone.poseToBoneColumn3.x
		'inputBoneMatrixColumn3.y = aChildBone.poseToBoneColumn3.y
		'inputBoneMatrixColumn3.z = aChildBone.poseToBoneColumn3.z
		'While True
		'	parentBoneIndex = aChildBone.parentBoneIndex
		'	If parentBoneIndex = -1 Then
		'		aVectorTransformed = MathModule.VectorITransform(aVector, inputBoneMatrixColumn0, inputBoneMatrixColumn1, inputBoneMatrixColumn2, inputBoneMatrixColumn3)
		'		Exit While
		'	Else
		'		aParentBone = Me.theMdlFileData.theBones(parentBoneIndex)
		'		MathModule.R_ConcatTransforms(aParentBone.poseToBoneColumn0, aParentBone.poseToBoneColumn1, aParentBone.poseToBoneColumn2, aParentBone.poseToBoneColumn3, inputBoneMatrixColumn0, inputBoneMatrixColumn1, inputBoneMatrixColumn2, inputBoneMatrixColumn3, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
		'		aChildBone = aParentBone
		'		inputBoneMatrixColumn0.x = boneMatrixColumn0.x
		'		inputBoneMatrixColumn0.y = boneMatrixColumn0.y
		'		inputBoneMatrixColumn0.z = boneMatrixColumn0.z
		'		inputBoneMatrixColumn1.x = boneMatrixColumn1.x
		'		inputBoneMatrixColumn1.y = boneMatrixColumn1.y
		'		inputBoneMatrixColumn1.z = boneMatrixColumn1.z
		'		inputBoneMatrixColumn2.x = boneMatrixColumn2.x
		'		inputBoneMatrixColumn2.y = boneMatrixColumn2.y
		'		inputBoneMatrixColumn2.z = boneMatrixColumn2.z
		'		inputBoneMatrixColumn3.x = boneMatrixColumn3.x
		'		inputBoneMatrixColumn3.y = boneMatrixColumn3.y
		'		inputBoneMatrixColumn3.z = boneMatrixColumn3.z
		'	End If
		'End While
		'======
		'TODO: Probably not the correct way, but it works for bullsquid and ship01.
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

	''NOTE: From disassembling of MDL Decompiler with OllyDbg, the following calculations are used in VPHYSICS.DLL for each face:
	''      convertedZ = 1.0 / 0.0254 * lastVertex.position.z
	''      convertedY = 1.0 / 0.0254 * -lastVertex.position.y
	''      convertedX = 1.0 / 0.0254 * lastVertex.position.x
	''NOTE: From disassembling of MDL Decompiler with OllyDbg, the following calculations are used after above for each vertex:
	''      newValue1 = unknownZ1 * convertedZ + unknownY1 * convertedY + unknownX1 * convertedX + unknownW1
	''      newValue2 = unknownZ2 * convertedZ + unknownY2 * convertedY + unknownX2 * convertedX + unknownW2
	''      newValue3 = unknownZ3 * convertedZ + unknownY3 * convertedY + unknownX3 * convertedX + unknownW3
	''Seems to be same as this code:
	''Dim aBone As SourceMdlBone
	''aBone = Me.theSourceEngineModel.theMdlFileHeader.theBones(anEyeball.boneIndex)
	''eyeballPosition = MathModule.VectorITransform(anEyeball.org, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	'Private Function TransformPhyVertex(ByVal aBone As SourceMdlBone, ByVal vertex As SourceVector) As SourceVector
	'	Dim aVectorTransformed As New SourceVector
	'	Dim aVector As New SourceVector()

	'	'NOTE: Too small.
	'	'aVectorTransformed.x = vertex.x
	'	'aVectorTransformed.y = vertex.y
	'	'aVectorTransformed.z = vertex.z
	'	'------
	'	'NOTE: Rotated for:
	'	'      simple_shape
	'	'      L4D2 w_models\weapons\w_minigun
	'	'aVectorTransformed.x = 1 / 0.0254 * vertex.x
	'	'aVectorTransformed.y = 1 / 0.0254 * vertex.y
	'	'aVectorTransformed.z = 1 / 0.0254 * vertex.z
	'	'------
	'	'NOTE: Works for:
	'	'      simple_shape
	'	'      L4D2 w_models\weapons\w_minigun
	'	'      L4D2 w_models\weapons\w_smg_uzi
	'	'      L4D2 props_vehicles\van
	'	'aVectorTransformed.x = 1 / 0.0254 * vertex.z
	'	'aVectorTransformed.y = 1 / 0.0254 * -vertex.x
	'	'aVectorTransformed.z = 1 / 0.0254 * -vertex.y
	'	'------
	'	'NOTE: Rotated for:
	'	'      L4D2 w_models\weapons\w_minigun
	'	'aVectorTransformed.x = 1 / 0.0254 * vertex.x
	'	'aVectorTransformed.y = 1 / 0.0254 * -vertex.y
	'	'aVectorTransformed.z = 1 / 0.0254 * vertex.z
	'	'------
	'	'NOTE: Rotated for:
	'	'      L4D2 props_vehicles\van
	'	'aVectorTransformed.x = 1 / 0.0254 * vertex.z
	'	'aVectorTransformed.y = 1 / 0.0254 * -vertex.y
	'	'aVectorTransformed.z = 1 / 0.0254 * vertex.x
	'	'------
	'	'NOTE: Rotated for:
	'	'      L4D2 w_models\weapons\w_minigun
	'	'aVector.x = 1 / 0.0254 * vertex.x
	'	'aVector.y = 1 / 0.0254 * vertex.y
	'	'aVector.z = 1 / 0.0254 * vertex.z
	'	'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	'	'------
	'	'NOTE: Rotated for:
	'	'      L4D2 w_models\weapons\w_minigun
	'	'aVector.x = 1 / 0.0254 * vertex.x
	'	'aVector.y = 1 / 0.0254 * -vertex.y
	'	'aVector.z = 1 / 0.0254 * vertex.z
	'	'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	'	'------
	'	'NOTE: Works for:
	'	'      L4D2 w_models\weapons\w_minigun
	'	'      L4D2 w_models\weapons\w_smg_uzi
	'	'NOTE: Rotated for:
	'	'      simple_shape
	'	'      L4D2 props_vehicles\van
	'	'NOTE: Each mesh piece rotated for:
	'	'      L4D2 survivors\survivor_producer
	'	'aVector.x = 1 / 0.0254 * vertex.z
	'	'aVector.y = 1 / 0.0254 * -vertex.y
	'	'aVector.z = 1 / 0.0254 * vertex.x
	'	'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	'	'------
	'	'NOTE: Works for:
	'	'      simple_shape
	'	'      L4D2 props_vehicles\van
	'	'      L4D2 survivors\survivor_producer
	'	'      L4D2 w_models\weapons\w_autoshot_m4super
	'	'      L4D2 w_models\weapons\w_desert_eagle
	'	'      L4D2 w_models\weapons\w_minigun
	'	'      L4D2 w_models\weapons\w_rifle_m16a2
	'	'      L4D2 w_models\weapons\w_smg_uzi
	'	'NOTE: Rotated for:
	'	'      L4D2 w_models\weapons\w_desert_rifle
	'	'      L4D2 w_models\weapons\w_shotgun_spas
	'	If Me.thePhyFileData.theSourcePhyIsCollisionModel Then
	'		aVectorTransformed.x = 1 / 0.0254 * vertex.z
	'		aVectorTransformed.y = 1 / 0.0254 * -vertex.x
	'		aVectorTransformed.z = 1 / 0.0254 * -vertex.y
	'	Else
	'		aVector.x = 1 / 0.0254 * vertex.x
	'		aVector.y = 1 / 0.0254 * vertex.z
	'		aVector.z = 1 / 0.0254 * -vertex.y
	'		aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	'	End If



	'	'------
	'	'NOTE: Works for:
	'	'      survivor_producer
	'	'NOTE: Does not work for:
	'	'      w_smg_uzi()
	'	'phyVertex.x = 1 / 0.0254 * aVector.x
	'	'phyVertex.y = 1 / 0.0254 * aVector.z
	'	'phyVertex.z = 1 / 0.0254 * -aVector.y
	'	'------
	'	'NOTE: These two lines match orientation for cstrike it_lampholder1 model, 
	'	'      but still doesn't compile properly.
	'	'NOTE: Does not work for:
	'	'      w_smg_uzi()
	'	'phyVertex.x = 1 / 0.0254 * aVector.z
	'	'phyVertex.y = 1 / 0.0254 * -aVector.x
	'	'phyVertex.z = 1 / 0.0254 * -aVector.y
	'	'------
	'	'NOTE: Does not work for:
	'	'      w_smg_uzi()
	'	'phyVertex.x = 1 / 0.0254 * aVector.y
	'	'phyVertex.y = 1 / 0.0254 * aVector.x
	'	'phyVertex.z = 1 / 0.0254 * -aVector.z
	'	'------
	'	'NOTE: Does not work for:
	'	'      w_smg_uzi()
	'	'phyVertex.x = 1 / 0.0254 * aVector.x
	'	'phyVertex.y = 1 / 0.0254 * aVector.y
	'	'phyVertex.z = 1 / 0.0254 * -aVector.z
	'	'------
	'	'NOTE: Does not work for:
	'	'      w_smg_uzi()
	'	'phyVertex.x = 1 / 0.0254 * -aVector.y
	'	'phyVertex.y = 1 / 0.0254 * aVector.x
	'	'phyVertex.z = 1 / 0.0254 * aVector.z
	'	'------
	'	'NOTE: Does not work for:
	'	'      w_smg_uzi()
	'	'phyVertex.x = 1 / 0.0254 * -aVector.y
	'	'phyVertex.y = 1 / 0.0254 * aVector.x
	'	'phyVertex.z = 1 / 0.0254 * aVector.z
	'	'------
	'	'NOTE: Does not work for:
	'	'      w_smg_uzi()
	'	'phyVertex.x = 1 / 0.0254 * aVector.z
	'	'phyVertex.y = 1 / 0.0254 * aVector.y
	'	'phyVertex.z = 1 / 0.0254 * aVector.x
	'	'------
	'	'NOTE: Works for:
	'	'      w_smg_uzi()
	'	'NOTE: Does not work for:
	'	'      survivor_producer
	'	'phyVertex.x = 1 / 0.0254 * aVector.z
	'	'phyVertex.y = 1 / 0.0254 * -aVector.y
	'	'phyVertex.z = 1 / 0.0254 * aVector.x
	'	'------
	'	'phyVertex.x = 1 / 0.0254 * aVector.z
	'	'phyVertex.y = 1 / 0.0254 * -aVector.y
	'	'phyVertex.z = 1 / 0.0254 * -aVector.x
	'	'------
	'	''TODO: Find some rationale for why phys model is rotated differently for different models.
	'	''      Possibly due to rotation needed to transfrom from pose to bone position.
	'	''If Me.theSourceEngineModel.theMdlFileHeader.theAnimationDescs.Count < 2 Then
	'	''If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
	'	'If Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyIsCollisionModel Then
	'	'	'TEST: Does not rotate L4D2's van phys mesh correctly.
	'	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
	'	'	'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
	'	'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
	'	'	'TEST:  Does not rotate L4D2's van phys mesh correctly.
	'	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
	'	'	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
	'	'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
	'	'	'TEST: Does not rotate L4D2's van phys mesh correctly.
	'	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.z
	'	'	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.y
	'	'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.x
	'	'	'TEST: Does not rotate L4D2's van phys mesh correctly.
	'	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
	'	'	'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
	'	'	'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y
	'	'	'TEST: Works for L4D2's van phys mesh.
	'	'	'      Does not work for L4D2 w_model\weapons\w_minigun.mdl.
	'	'	aVector.x = 1 / 0.0254 * vertex.z
	'	'	aVector.y = 1 / 0.0254 * -vertex.x
	'	'	aVector.z = 1 / 0.0254 * -vertex.y

	'	'	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

	'	'	'======

	'	'	'Dim aVectorTransformed2 As SourceVector
	'	'	''aVectorTransformed2 = New SourceVector()
	'	'	''aVectorTransformed2 = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	'	'	''aVectorTransformed2.x = aVector.x
	'	'	''aVectorTransformed2.y = aVector.y
	'	'	''aVectorTransformed2.z = aVector.z

	'	'	'aVectorTransformed = MathModule.VectorTransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	'	'	''aVectorTransformed = MathModule.VectorTransform(aVectorTransformed2, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	'	'	''aVectorTransformed = New SourceVector()
	'	'	''aVectorTransformed.x = aVectorTransformed2.x
	'	'	''aVectorTransformed.y = aVectorTransformed2.y
	'	'	''aVectorTransformed.z = aVectorTransformed2.z
	'	'Else
	'	'	'TEST: Does not work for L4D2 w_model\weapons\w_minigun.mdl.
	'	'	aVector.x = 1 / 0.0254 * vertex.x
	'	'	aVector.y = 1 / 0.0254 * vertex.z
	'	'	aVector.z = 1 / 0.0254 * -vertex.y

	'	'	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	'	'End If
	'	'------
	'	'TEST: Does not rotate L4D2's van phys mesh correctly.
	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
	'	'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
	'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
	'	'TEST: Does not rotate L4D2's van phys mesh correctly.
	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
	'	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
	'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
	'	'TEST: works for survivor_producer; matches ref and phy meshes of van, but both are rotated 90 degrees on z-axis
	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
	'	'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
	'	'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y

	'	'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	'	''------
	'	'''TEST: Only rotate by -90 deg if bone is a root bone.  Do not know why.
	'	''If aBone.parentBoneIndex = -1 Then
	'	''	aVectorTransformed = MathModule.RotateAboutZAxis(aVectorTransformed, MathModule.DegreesToRadians(-90), aBone)
	'	''End If

	'	Return aVectorTransformed
	'End Function

	'static void CalcAnimation( const CStudioHdr *pStudioHdr,	Vector *pos, Quaternion *q, 
	'	mstudioseqdesc_t &seqdesc,
	'	int sequence, int animation,
	'	float cycle, int boneMask )
	'{
	'	int					i;
	'
	'	mstudioanimdesc_t &animdesc = pStudioHdr->pAnimdesc( animation );
	'	mstudiobone_t *pbone = pStudioHdr->pBone( 0 );
	'	mstudioanim_t *panim = animdesc.pAnim( );
	'
	'	int					iFrame;
	'	float				s;
	'
	'	float fFrame = cycle * (animdesc.numframes - 1);
	'
	'	iFrame = (int)fFrame;
	'	s = (fFrame - iFrame);
	'
	'	float *pweight = seqdesc.pBoneweight( 0 );
	'
	'	for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'	{
	'		if (panim && panim->bone == i)
	'		{
	'			if (*pweight > 0 && (pbone->flags & boneMask))
	'			{
	'				CalcBoneQuaternion( iFrame, s, pbone, panim, q[i] );
	'				CalcBonePosition  ( iFrame, s, pbone, panim, pos[i] );
	'			}
	'			panim = panim->pNext();
	'		}
	'		else if (*pweight > 0 && (pbone->flags & boneMask))
	'		{
	'			if (animdesc.flags & STUDIO_DELTA)
	'			{
	'				q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'				pos[i].Init( 0.0f, 0.0f, 0.0f );
	'			}
	'			else
	'			{
	'				q[i] = pbone->quat;
	'				pos[i] = pbone->pos;
	'			}
	'		}
	'	}
	'}
	'======
	'FROM: SourceEngine2007_source\src_main\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: Find and decode a sub-frame of animation
	'//-----------------------------------------------------------------------------
	'
	'static void CalcAnimation( const CStudioHdr *pStudioHdr,	Vector *pos, Quaternion *q, 
	'	mstudioseqdesc_t &seqdesc,
	'	int sequence, int animation,
	'	float cycle, int boneMask )
	'{
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'	pStudioHdr->m_nPerfAnimationLayers++;
	'#endif
	'
	'	virtualmodel_t *pVModel = pStudioHdr->GetVirtualModel();
	'
	'	if (pVModel)
	'	{
	'		CalcVirtualAnimation( pVModel, pStudioHdr, pos, q, seqdesc, sequence, animation, cycle, boneMask );
	'		return;
	'	}
	'
	'	mstudioanimdesc_t &animdesc = pStudioHdr->pAnimdesc( animation );
	'	mstudiobone_t *pbone = pStudioHdr->pBone( 0 );
	'	const mstudiolinearbone_t *pLinearBones = pStudioHdr->pLinearBones();
	'
	'	int					i;
	'	int					iFrame;
	'	float				s;
	'
	'	float fFrame = cycle * (animdesc.numframes - 1);
	'
	'	iFrame = (int)fFrame;
	'	s = (fFrame - iFrame);
	'
	'	int iLocalFrame = iFrame;
	'	float flStall;
	'	mstudioanim_t *panim = animdesc.pAnim( &iLocalFrame, flStall );
	'
	'	float *pweight = seqdesc.pBoneweight( 0 );
	'
	'	// if the animation isn't available, look for the zero frame cache
	'	if (!panim)
	'	{
	'		// Msg("zeroframe %s\n", animdesc.pszName() );
	'		// pre initialize
	'		for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'		{
	'			if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'			{
	'				if (animdesc.flags & STUDIO_DELTA)
	'				{
	'					q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'					pos[i].Init( 0.0f, 0.0f, 0.0f );
	'				}
	'				else
	'				{
	'					q[i] = pbone->quat;
	'					pos[i] = pbone->pos;
	'				}
	'			}
	'		}
	'
	'		CalcZeroframeData( pStudioHdr, pStudioHdr->GetRenderHdr(), NULL, pStudioHdr->pBone( 0 ), animdesc, fFrame, pos, q, boneMask, 1.0 );
	'
	'		return;
	'	}
	'
	'	// BUGBUG: the sequence, the anim, and the model can have all different bone mappings.
	'	for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'	{
	'		if (panim && panim->bone == i)
	'		{
	'			if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'			{
	'				CalcBoneQuaternion( iLocalFrame, s, pbone, pLinearBones, panim, q[i] );
	'				CalcBonePosition  ( iLocalFrame, s, pbone, pLinearBones, panim, pos[i] );
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'				pStudioHdr->m_nPerfAnimatedBones++;
	'				pStudioHdr->m_nPerfUsedBones++;
	'#endif
	'			}
	'			panim = panim->pNext();
	'		}
	'		else if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'		{
	'			if (animdesc.flags & STUDIO_DELTA)
	'			{
	'				q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'				pos[i].Init( 0.0f, 0.0f, 0.0f );
	'			}
	'			else
	'			{
	'				q[i] = pbone->quat;
	'				pos[i] = pbone->pos;
	'			}
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'			pStudioHdr->m_nPerfUsedBones++;
	'#endif
	'		}
	'	}
	'
	'	// cross fade in previous zeroframe data
	'	if (flStall > 0.0f)
	'	{
	'		CalcZeroframeData( pStudioHdr, pStudioHdr->GetRenderHdr(), NULL, pStudioHdr->pBone( 0 ), animdesc, fFrame, pos, q, boneMask, flStall );
	'	}
	'
	'	if (animdesc.numlocalhierarchy)
	'	{
	'		matrix3x4_t *boneToWorld = g_MatrixPool.Alloc();
	'		CBoneBitList boneComputed;
	'
	'		int i;
	'		for (i = 0; i < animdesc.numlocalhierarchy; i++)
	'		{
	'			mstudiolocalhierarchy_t *pHierarchy = animdesc.pHierarchy( i );
	'
	'			if ( !pHierarchy )
	'				break;
	'
	'			if (pStudioHdr->boneFlags(pHierarchy->iBone) & boneMask)
	'			{
	'				if (pStudioHdr->boneFlags(pHierarchy->iNewParent) & boneMask)
	'				{
	'					CalcLocalHierarchyAnimation( pStudioHdr, boneToWorld, boneComputed, pos, q, pbone, pHierarchy, pHierarchy->iBone, pHierarchy->iNewParent, cycle, iFrame, s, boneMask );
	'				}
	'			}
	'		}
	'
	'		g_MatrixPool.Free( boneToWorld );
	'	}
	'
	'}
	Private Sub CalcAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc37, ByVal frameIndex As Integer)
		Dim s As Double
		Dim aBone As SourceMdlBone37
		Dim anAnimation As SourceMdlAnimation37
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

	Private Function CalcBoneRotation(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone37, ByVal anAnimation As SourceMdlAnimation37, ByRef rotationQuat As SourceQuaternion) As SourceVector
		Dim angleVector As New SourceVector()

		If (anAnimation.flags And SourceMdlAnimation37.STUDIO_ROT_ANIMATED) > 0 Then
			If anAnimation.animationValueOffsets(3) <= 0 Then
				'angleVector.x = 0
				angleVector.x = aBone.rotation.x
			Else
				angleVector.x = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(3), aBone.rotationScale.x)
				'angle1[j] = pbone->value[j+3] + angle1[j] * pbone->scale[j+3];
				angleVector.x = aBone.rotation.x + angleVector.x * aBone.rotationScale.x
			End If
			If anAnimation.animationValueOffsets(4) <= 0 Then
				'angleVector.y = 0
				angleVector.y = aBone.rotation.y
			Else
				angleVector.y = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(4), aBone.rotationScale.y)
				angleVector.y = aBone.rotation.y + angleVector.y * aBone.rotationScale.y
			End If
			If anAnimation.animationValueOffsets(5) <= 0 Then
				'angleVector.z = 0
				angleVector.z = aBone.rotation.z
			Else
				angleVector.z = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(5), aBone.rotationScale.z)
				angleVector.z = aBone.rotation.z + angleVector.z * aBone.rotationScale.z
			End If

			rotationQuat = MathModule.EulerAnglesToQuaternion(angleVector)

			angleVector.debug_text = "anim"
		Else
			'rotationQuat = anAnimation.rotationQuat
			rotationQuat.x = anAnimation.rotationQuat.x
			rotationQuat.y = anAnimation.rotationQuat.y
			rotationQuat.z = anAnimation.rotationQuat.z
			rotationQuat.w = anAnimation.rotationQuat.w
			angleVector = MathModule.ToEulerAngles(rotationQuat)

			angleVector.debug_text = "rot"
		End If

		Return angleVector
	End Function

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: return a sub frame position for a single bone
	'//-----------------------------------------------------------------------------
	'void CalcBonePosition(	int frame, float s,
	'						const Vector &basePos, const Vector &baseBoneScale, 
	'						const mstudioanim_t *panim, Vector &pos	)
	'{
	'	if (panim->flags & STUDIO_ANIM_RAWPOS)
	'	{
	'		pos = *(panim->pPos());
	'		Assert( pos.IsValid() );

	'		return;
	'	}
	'	else if (!(panim->flags & STUDIO_ANIM_ANIMPOS))
	'	{
	'		if (panim->flags & STUDIO_ANIM_DELTA)
	'		{
	'			pos.Init( 0.0f, 0.0f, 0.0f );
	'		}
	'		else
	'		{
	'			pos = basePos;
	'		}
	'		return;
	'	}

	'	mstudioanim_valueptr_t *pPosV = panim->pPosV();
	'	int					j;

	'	if (s > 0.001f)
	'	{
	'		float v1, v2;
	'		for (j = 0; j < 3; j++)
	'		{
	'			ExtractAnimValue( frame, pPosV->pAnimvalue( j ), baseBoneScale[j], v1, v2 );
	'			//ZM: This is really setting pos.x when j = 0, pos.y when j = 1, and pos.z when j = 2.
	'			pos[j] = v1 * (1.0 - s) + v2 * s;
	'		}
	'	}
	'	else
	'	{
	'		for (j = 0; j < 3; j++)
	'		{
	'			//ZM: This is really setting pos.x when j = 0, pos.y when j = 1, and pos.z when j = 2.
	'			ExtractAnimValue( frame, pPosV->pAnimvalue( j ), baseBoneScale[j], pos[j] );
	'		}
	'	}

	'	if (!(panim->flags & STUDIO_ANIM_DELTA))
	'	{
	'		pos.x = pos.x + basePos.x;
	'		pos.y = pos.y + basePos.y;
	'		pos.z = pos.z + basePos.z;
	'	}

	'	Assert( pos.IsValid() );
	'}
	Private Function CalcBonePosition(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone37, ByVal anAnimation As SourceMdlAnimation37) As SourceVector
		Dim pos As New SourceVector()

		If (anAnimation.flags And SourceMdlAnimation37.STUDIO_POS_ANIMATED) > 0 Then
			If anAnimation.animationValueOffsets(0) <= 0 Then
				'pos.x = 0
				pos.x = aBone.position.x
			Else
				pos.x = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(0), aBone.positionScale.x)
				pos.x = aBone.position.x + pos.x * aBone.positionScale.x
			End If

			If anAnimation.animationValueOffsets(1) <= 0 Then
				'pos.y = 0
				pos.y = aBone.position.y
			Else
				pos.y = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(1), aBone.positionScale.y)
				pos.y = aBone.position.y + pos.y * aBone.positionScale.y
			End If

			If anAnimation.animationValueOffsets(2) <= 0 Then
				'pos.z = 0
				pos.z = aBone.position.z
			Else
				pos.z = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(2), aBone.positionScale.z)
				pos.z = aBone.position.z + pos.z * aBone.positionScale.z
			End If

			pos.debug_text = "anim"
		Else
			'pos = anAnimation.position
			pos.x = anAnimation.position.x
			pos.y = anAnimation.position.y
			pos.z = anAnimation.position.z

			pos.debug_text = "pos"
		End If

		Return pos
	End Function

	Public Function ExtractAnimValue(ByVal frameIndex As Integer, ByVal animValues As List(Of SourceMdlAnimationValue10), ByVal scale As Double) As Double
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
				'NOTE: Needs to be offset from current animValues index to match the C++ code above in comment.
				v1 = animValues(animValueIndex + k + 1).value
			Else
				'NOTE: Needs to be offset from current animValues index to match the C++ code above in comment.
				v1 = animValues(animValueIndex + animValues(animValueIndex).valid).value
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
	Private theMdlFileData As SourceMdlFileData37
	Private thePhyFileData As SourcePhyFileData
	'Private theVtxFileData As SourceVtxFileData44
	'Private theVvdFileData As SourceVvdFileData37
	'Private theModelName As String

	Private theAnimationFrameLines As SortedList(Of Integer, AnimationFrameLine)

#End Region

End Class
