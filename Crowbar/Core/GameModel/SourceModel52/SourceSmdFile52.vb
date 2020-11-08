Imports System.IO

Public Class SourceSmdFile52

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData52)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
	End Sub

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData52, ByVal vvdFileData As SourceVvdFileData04)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
		Me.theVvdFileData = vvdFileData
	End Sub

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData52, ByVal phyFileData As SourcePhyFileData)
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
		Dim boneCount As Integer

		'nodes
		line = "nodes"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If Me.theMdlFileData.theBones Is Nothing Then
			boneCount = 0
		Else
			boneCount = Me.theMdlFileData.theBones.Count
		End If
		For boneIndex As Integer = 0 To boneCount - 1
			'Dim aBone As SourceMdlBone
			'aBone = Me.theMdlFileData.theBones(boneIndex)
			'If lodIndex = -2 AndAlso aBone.proceduralRuleOffset <> 0 AndAlso aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
			'	Continue For
			'End If

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
		Dim aBone As SourceMdlBone

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

			line = "    "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += aBone.position.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += aBone.position.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += aBone.position.z.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += aBone.rotation.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += aBone.rotation.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += aBone.rotation.z.ToString("0.000000", TheApp.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteTrianglesSection(ByVal aVtxModel As SourceVtxModel07, ByVal lodIndex As Integer, ByVal aModel As SourceMdlModel, ByVal bodyPartVertexIndexStart As Integer)
		Dim line As String = ""
		Dim materialLine As String = ""
		Dim vertex1Line As String = ""
		Dim vertex2Line As String = ""
		Dim vertex3Line As String = ""

		'triangles
		line = "triangles"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Dim aVtxLod As SourceVtxModelLod07
		Dim aVtxMesh As SourceVtxMesh07
		Dim aStripGroup As SourceVtxStripGroup07
		'Dim cumulativeVertexCount As Integer
		'Dim maxIndexForMesh As Integer
		'Dim cumulativeMaxIndex As Integer
		Dim materialIndex As Integer
		Dim materialPathFileName As String
		Dim materialFileName As String
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
					materialPathFileName = Me.theMdlFileData.theTextures(materialIndex).thePathFileName
					materialFileName = Me.theMdlFileData.theModifiedTextureFileNames(materialIndex)

					meshVertexIndexStart = aModel.theMeshes(meshIndex).vertexIndexStart

					If aVtxMesh.theVtxStripGroups IsNot Nothing Then
						If TheApp.Settings.DecompileDebugInfoFilesIsChecked AndAlso materialPathFileName <> materialFileName Then
							materialLine = "// In the MDL file as: " + materialPathFileName
							Me.theOutputFileStreamWriter.WriteLine(materialLine)
						End If

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
									materialLine = materialFileName
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
		Dim aBone As SourceMdlBone
		Dim aTriangle As SourcePhyFace
		Dim faceSection As SourcePhyFaceSection
		Dim phyVertex As SourcePhyVertex
		Dim aVectorTransformed As SourceVector

		Try
			If Me.thePhyFileData.theSourcePhyCollisionDatas IsNot Nothing Then
				Me.ProcessTransformsForPhysics()

				For collisionDataIndex As Integer = 0 To Me.thePhyFileData.theSourcePhyCollisionDatas.Count - 1
					collisionData = Me.thePhyFileData.theSourcePhyCollisionDatas(collisionDataIndex)

					For faceSectionIndex As Integer = 0 To collisionData.theFaceSections.Count - 1
						faceSection = collisionData.theFaceSections(faceSectionIndex)

						If faceSection.theBoneIndex >= Me.theMdlFileData.theBones.Count Then
							Continue For
						End If
						aBone = Me.theMdlFileData.theBones(faceSection.theBoneIndex)

						For triangleIndex As Integer = 0 To faceSection.theFaces.Count - 1
							aTriangle = faceSection.theFaces(triangleIndex)

							line = "  phy"
							Me.theOutputFileStreamWriter.WriteLine(line)

							'  19 -0.000009 0.000001 0.999953 0.0 0.0 0.0 1 0
							For vertexIndex As Integer = 0 To aTriangle.vertexIndex.Length - 1
								'phyVertex = collisionData.theVertices(aTriangle.vertexIndex(vertexIndex))
								phyVertex = faceSection.theVertices(aTriangle.vertexIndex(vertexIndex))

								aVectorTransformed = Me.TransformPhyVertex(aBone, phyVertex.vertex)

								line = "    "
								line += faceSection.theBoneIndex.ToString(TheApp.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.x.ToString("0.000000", TheApp.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.y.ToString("0.000000", TheApp.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.z.ToString("0.000000", TheApp.InternalNumberFormat)

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
		Dim anAnimationDesc As SourceMdlAnimationDesc52

		aSequenceDesc = Nothing
		anAnimationDesc = Me.theMdlFileData.theFirstAnimationDesc

		Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
		frameIndex = 0
		Me.theAnimationFrameLines.Clear()
		'If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0 Then
		Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex)
		'End If

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
		Dim aBone As SourceMdlBone
		Dim boneIndex As Integer
		Dim aFrameLine As AnimationFrameLine
		Dim position As New SourceVector()
		Dim rotation As New SourceVector()
		'Dim tempRotation As New SourceVector()
		Dim aSequenceDesc As SourceMdlSequenceDesc
		Dim anAnimationDesc As SourceMdlAnimationDesc52
		'Dim beforeMovementFrameRotation As New SourceVector()
		'Dim previousFrameRotation As New SourceVector()
		'Dim beforeMovementFramePosition As New SourceVector()
		'Dim previousFramePosition As New SourceVector()

		aSequenceDesc = CType(aSequenceDescBase, SourceMdlSequenceDesc)
		anAnimationDesc = CType(anAnimationDescBase, SourceMdlAnimationDesc52)

		'skeleton
		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If Me.theMdlFileData.theBones IsNot Nothing Then
			Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
			For frameIndex As Integer = 0 To anAnimationDesc.frameCount - 1
				Me.theAnimationFrameLines.Clear()

				If ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_FRAMEANIM) <> 0) Then
					Dim sectionFrameIndex As Integer
					Dim sectionIndex As Integer
					Dim aSectionOfAnimation As SourceAniFrameAnim52
					If anAnimationDesc.sectionFrameCount = 0 Then
						sectionIndex = 0
						sectionFrameIndex = frameIndex
					Else
						sectionIndex = CInt(Math.Truncate(frameIndex / anAnimationDesc.sectionFrameCount))
						sectionFrameIndex = frameIndex - (sectionIndex * anAnimationDesc.sectionFrameCount)
					End If
					aSectionOfAnimation = anAnimationDesc.theSectionsOfFrameAnim(sectionIndex)

					Dim aBoneConstantInfo As BoneConstantInfo49
					Dim aBoneFrameDataInfo As BoneFrameDataInfo49

					For boneIndex = 0 To Me.theMdlFileData.theBones.Count - 1
						aBone = Me.theMdlFileData.theBones(boneIndex)

						aFrameLine = New AnimationFrameLine()
						Me.theAnimationFrameLines.Add(boneIndex, aFrameLine)

						aFrameLine.position = New SourceVector()
						aFrameLine.rotation = New SourceVector()

						If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
							aFrameLine.position.x = 0
							aFrameLine.position.y = 0
							aFrameLine.position.z = 0
							aFrameLine.position.debug_text = "ZERO"
							aFrameLine.rotation.x = 0
							aFrameLine.rotation.y = 0
							aFrameLine.rotation.z = 0
							aFrameLine.rotation.debug_text = "ZERO"
						Else
							aFrameLine.position.x = aBone.position.x
							aFrameLine.position.y = aBone.position.y
							aFrameLine.position.z = aBone.position.z
							aFrameLine.position.debug_text = "BONE"
							aFrameLine.rotation.x = aBone.rotation.x
							aFrameLine.rotation.y = aBone.rotation.y
							aFrameLine.rotation.z = aBone.rotation.z
							aFrameLine.rotation.debug_text = "BONE"
						End If

						Dim boneFlag As Integer
						boneFlag = aSectionOfAnimation.theBoneFlags(boneIndex)
						If aSectionOfAnimation.theBoneConstantInfos IsNot Nothing Then
							aBoneConstantInfo = aSectionOfAnimation.theBoneConstantInfos(boneIndex)
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_RAWROT) > 0 Then
								aFrameLine.rotation = MathModule.ToEulerAngles(aBoneConstantInfo.theConstantRawRot.quaternion)
								aFrameLine.rotation.debug_text = "RAWROT"
							End If
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_RAWPOS) > 0 Then
								aFrameLine.position.x = aBoneConstantInfo.theConstantRawPos.x
								aFrameLine.position.y = aBoneConstantInfo.theConstantRawPos.y
								aFrameLine.position.z = aBoneConstantInfo.theConstantRawPos.z
								aFrameLine.position.debug_text = "RAWPOS"
							End If
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_CONST_ROT2) > 0 Then
								aFrameLine.rotation = MathModule.ToEulerAngles(aBoneConstantInfo.theConstantRotationUnknown.quaternion)
								aFrameLine.rotation.debug_text = "FRAME_CONST_ROT2"
							End If
						End If
						If aSectionOfAnimation.theBoneFrameDataInfos IsNot Nothing Then
							aBoneFrameDataInfo = aSectionOfAnimation.theBoneFrameDataInfos(sectionFrameIndex)(boneIndex)
							'If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_ANIMROT) > 0 Then
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_FULLANIMPOS) > 0 Then
								aFrameLine.rotation = MathModule.ToEulerAngles(aBoneFrameDataInfo.theAnimRotation.quaternion)
								aFrameLine.rotation.debug_text = "ANIMROT"
							End If
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_ANIMPOS) > 0 Then
								aFrameLine.position.x = aBoneFrameDataInfo.theAnimPosition.x
								aFrameLine.position.y = aBoneFrameDataInfo.theAnimPosition.y
								aFrameLine.position.z = aBoneFrameDataInfo.theAnimPosition.z
								aFrameLine.position.debug_text = "ANIMPOS"
							End If
							'If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_FULLANIMPOS) > 0 Then
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_ANIMROT) > 0 Then
								'aFrameLine.position.x = aBoneFrameDataInfo.theFullAnimPosition.x
								'aFrameLine.position.y = aBoneFrameDataInfo.theFullAnimPosition.y
								'aFrameLine.position.z = aBoneFrameDataInfo.theFullAnimPosition.z
								aFrameLine.position.x = aBoneFrameDataInfo.theAnimPosition.x
								aFrameLine.position.y = aBoneFrameDataInfo.theAnimPosition.y
								aFrameLine.position.z = aBoneFrameDataInfo.theAnimPosition.z
								aFrameLine.position.debug_text = "FULLANIMPOS"
							End If
							If (boneFlag And &H20) > 0 Then
								Dim unknownFlagIsUsed As Integer = 4242
							End If
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_ANIM_ROT2) > 0 Then
								aFrameLine.rotation = MathModule.ToEulerAngles(aBoneFrameDataInfo.theAnimRotationUnknown.quaternion)
								aFrameLine.rotation.debug_text = "FRAME_ANIM_ROT2"
							End If
						End If
					Next
				Else
					Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex)
				End If

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
		End If

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

	Private Sub AdjustPositionAndRotation(ByVal boneIndex As Integer, ByVal iPosition As SourceVector, ByVal iRotation As SourceVector, ByRef oPosition As SourceVector, ByRef oRotation As SourceVector)
		Dim aBone As SourceMdlBone
		aBone = Me.theMdlFileData.theBones(boneIndex)

		'If iPosition.debug_text = "desc_delta" OrElse iPosition.debug_text.StartsWith("delta") Then
		'	Dim aFirstAnimationDescFrameLine As AnimationFrameLine
		'	aFirstAnimationDescFrameLine = Me.theMdlFileData.theFirstAnimationDescFrameLines(boneIndex)

		'	oPosition.x = iPosition.x + aFirstAnimationDescFrameLine.position.x
		'	oPosition.y = iPosition.y + aFirstAnimationDescFrameLine.position.y
		'	oPosition.z = iPosition.z + aFirstAnimationDescFrameLine.position.z
		'Else
		If aBone.parentBoneIndex = -1 Then
			oPosition.x = iPosition.y
			oPosition.y = -iPosition.x
			oPosition.z = iPosition.z
		Else
			oPosition.x = iPosition.x
			oPosition.y = iPosition.y
			oPosition.z = iPosition.z
		End If

		'If iRotation.debug_text = "desc_delta" OrElse iRotation.debug_text.StartsWith("delta") Then
		'	Dim aFirstAnimationDescFrameLine As AnimationFrameLine
		'	aFirstAnimationDescFrameLine = Me.theMdlFileData.theFirstAnimationDescFrameLines(boneIndex)

		'	Dim quat As New SourceQuaternion()
		'	Dim quat2 As New SourceQuaternion()
		'	Dim quatResult As New SourceQuaternion()
		'	Dim magnitude As Double
		'	quat = MathModule.EulerAnglesToQuaternion(iRotation)
		'	quat2 = MathModule.EulerAnglesToQuaternion(aFirstAnimationDescFrameLine.rotation)

		'	quat.x *= -1
		'	quat.y *= -1
		'	quat.z *= -1
		'	quatResult.x = quat.w * quat2.x + quat.x * quat2.w + quat.y * quat2.z - quat.z * quat2.y
		'	quatResult.y = quat.w * quat2.y - quat.x * quat2.z + quat.y * quat2.w + quat.z * quat2.x
		'	quatResult.z = quat.w * quat2.z + quat.x * quat2.y - quat.y * quat2.x + quat.z * quat2.w
		'	quatResult.w = quat.w * quat2.w + quat.x * quat2.x + quat.y * quat2.y - quat.z * quat2.z

		'	magnitude = Math.Sqrt(quatResult.w * quatResult.w + quatResult.x * quatResult.x + quatResult.y * quatResult.y + quatResult.z * quatResult.z)
		'	quatResult.x /= magnitude
		'	quatResult.y /= magnitude
		'	quatResult.z /= magnitude
		'	quatResult.w /= magnitude

		'	Dim tempRotation As New SourceVector()
		'	tempRotation = MathModule.ToEulerAngles(quatResult)
		'	oRotation.x = tempRotation.y
		'	oRotation.y = tempRotation.z
		'	oRotation.z = tempRotation.x
		'Else
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

	Private Sub AdjustPositionAndRotationByPiecewiseMovement(ByVal frameIndex As Integer, ByVal boneIndex As Integer, ByVal movements As List(Of SourceMdlMovement), ByVal iPosition As SourceVector, ByVal iRotation As SourceVector, ByRef oPosition As SourceVector, ByRef oRotation As SourceVector)
		Dim aBone As SourceMdlBone
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

				'TEST: UNTESTED on MDL v52 - Works in MDL v49 for translation and maybe rotation on Z, but ignores other axis rotations because the above does not work for rotations.
				oPosition.x = iPosition.x + vecPos.x
				oPosition.y = iPosition.y + vecPos.y
				oPosition.z = iPosition.z + vecPos.z
				oRotation.z = iRotation.z + vecAngle.y
			End If
		End If
	End Sub

	Private Sub AdjustPositionAndRotationForDelta(ByVal iPosition As SourceVector, ByVal iRotation As SourceVector, ByRef oPosition As SourceVector, ByRef oRotation As SourceVector)

	End Sub

	Private Function WriteVertexLine(ByVal aStripGroup As SourceVtxStripGroup07, ByVal aVtxIndexIndex As Integer, ByVal lodIndex As Integer, ByVal meshVertexIndexStart As Integer, ByVal bodyPartVertexIndexStart As Integer) As String
		Dim aVtxVertexIndex As UShort
		Dim aVtxVertex As SourceVtxVertex07
		Dim aVertex As SourceVertex
		Dim vertexIndex As Integer
		Dim line As String

		line = ""
		Try
			aVtxVertexIndex = aStripGroup.theVtxIndexes(aVtxIndexIndex)
			aVtxVertex = aStripGroup.theVtxVertexes(aVtxVertexIndex)
			vertexIndex = aVtxVertex.originalMeshVertexIndex + bodyPartVertexIndexStart + meshVertexIndexStart
			If Me.theVvdFileData.fixupCount = 0 Then
				aVertex = Me.theVvdFileData.theVertexes(vertexIndex)
			Else
				'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
				'      Maybe the listing by lodIndex is only needed internally by graphics engine.
				'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
				aVertex = Me.theVvdFileData.theFixedVertexesByLod(0)(vertexIndex)
				'aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
			End If

			line = "  "
			line += aVertex.boneWeight.bone(0).ToString(TheApp.InternalNumberFormat)

			line += " "
			If (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
				'NOTE: This does not work for L4D2 w_models\weapons\w_minigun.mdl.
				line += aVertex.positionY.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += (-aVertex.positionX).ToString("0.000000", TheApp.InternalNumberFormat)
			Else
				'NOTE: This works for L4D2 w_models\weapons\w_minigun.mdl.
				line += aVertex.positionX.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.positionY.ToString("0.000000", TheApp.InternalNumberFormat)
			End If
			line += " "
			line += aVertex.positionZ.ToString("0.000000", TheApp.InternalNumberFormat)

			line += " "
			If (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
				line += aVertex.normalY.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += (-aVertex.normalX).ToString("0.000000", TheApp.InternalNumberFormat)
			Else
				line += aVertex.normalX.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.normalY.ToString("0.000000", TheApp.InternalNumberFormat)
			End If
			line += " "
			line += aVertex.normalZ.ToString("0.000000", TheApp.InternalNumberFormat)

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

	Private Sub CalculateFirstAnimDescFrameLinesForPhysics(ByRef aFirstAnimationDescFrameLine As AnimationFrameLine)
		Dim boneIndex As Integer
		Dim aFrameLine As AnimationFrameLine
		Dim frameIndex As Integer
		Dim frameLineIndex As Integer
		Dim aSequenceDesc As SourceMdlSequenceDesc
		Dim anAnimationDesc As SourceMdlAnimationDesc52

		aSequenceDesc = Nothing
		anAnimationDesc = Me.theMdlFileData.theAnimationDescs(0)

		Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
		frameIndex = 0
		Me.theAnimationFrameLines.Clear()
		'If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0 Then
		Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex)
		'End If

		frameLineIndex = 0
		boneIndex = Me.theAnimationFrameLines.Keys(frameLineIndex)
		aFrameLine = Me.theAnimationFrameLines.Values(frameLineIndex)

		aFirstAnimationDescFrameLine.rotation = New SourceVector()
		aFirstAnimationDescFrameLine.position = New SourceVector()

		aFirstAnimationDescFrameLine.rotation.x = aFrameLine.rotation.x
		aFirstAnimationDescFrameLine.rotation.y = aFrameLine.rotation.y
		'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
		'	Dim z As Double
		'	z = aFrameLine.rotation.z
		'	z += MathModule.DegreesToRadians(-90)
		'	aFirstAnimationDescFrameLine.rotation.z = z
		'Else
		aFirstAnimationDescFrameLine.rotation.z = aFrameLine.rotation.z
		'End If

		''NOTE: Only adjust position if bone is a root bone. Do not know why.
		''If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
		''TEST: Try this version, because of "sequence_blend from Game Zombie" model.
		'If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 AndAlso (aFrameLine.position.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone") Then
		'	aFirstAnimationDescFrameLine.position.x = aFrameLine.position.y
		'	aFirstAnimationDescFrameLine.position.y = (-aFrameLine.position.x)
		'	aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
		'Else
		aFirstAnimationDescFrameLine.position.x = aFrameLine.position.x
		aFirstAnimationDescFrameLine.position.y = aFrameLine.position.y
		aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
		'End If
	End Sub

	Private Sub ProcessTransformsForPhysics()
		If Me.thePhyFileData.theSourcePhyIsCollisionModel Then
			Dim aFirstAnimationDescFrameLine As New AnimationFrameLine()
			Me.CalculateFirstAnimDescFrameLinesForPhysics(aFirstAnimationDescFrameLine)

			Dim position As SourceVector
			Dim rotation As SourceVector
			position = aFirstAnimationDescFrameLine.position
			rotation = aFirstAnimationDescFrameLine.rotation

			'MathModule.AngleMatrix(rotation.y, rotation.z, rotation.x, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			'MathModule.AngleMatrix(rotation.x, rotation.y, rotation.z, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			'Me.worldToPoseColumn3.x = position.x
			'Me.worldToPoseColumn3.y = position.y
			'Me.worldToPoseColumn3.z = position.z
			'------
			'MathModule.AngleMatrix(rotation.x, rotation.y, rotation.z, poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
			'poseToWorldColumn3.x = position.x
			'poseToWorldColumn3.y = position.y
			'poseToWorldColumn3.z = position.z
			'MathModule.MatrixInvert(poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			'------
			MathModule.AngleMatrix(rotation.x, rotation.y, rotation.z + MathModule.DegreesToRadians(-90), poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
			poseToWorldColumn3.x = position.y
			poseToWorldColumn3.y = -position.x
			poseToWorldColumn3.z = position.z
			MathModule.MatrixInvert(poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
		End If
	End Sub

	'NOTE: From disassembling of MDL Decompiler with OllyDbg, the following calculations are used in VPHYSICS.DLL for each face:
	'      convertedZ = 1.0 / 0.0254 * lastVertex.position.z
	'      convertedY = 1.0 / 0.0254 * -lastVertex.position.y
	'      convertedX = 1.0 / 0.0254 * lastVertex.position.x
	'NOTE: From disassembling of MDL Decompiler with OllyDbg, the following calculations are used after above for each vertex:
	'      newValue1 = unknownZ1 * convertedZ + unknownY1 * convertedY + unknownX1 * convertedX + unknownW1
	'      newValue2 = unknownZ2 * convertedZ + unknownY2 * convertedY + unknownX2 * convertedX + unknownW2
	'      newValue3 = unknownZ3 * convertedZ + unknownY3 * convertedY + unknownX3 * convertedX + unknownW3
	'Seems to be same as this code:
	'Dim aBone As SourceMdlBone
	'aBone = Me.theSourceEngineModel.theMdlFileHeader.theBones(anEyeball.boneIndex)
	'eyeballPosition = MathModule.VectorITransform(anEyeball.org, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
	Private Function TransformPhyVertex(ByVal aBone As SourceMdlBone, ByVal vertex As SourceVector) As SourceVector
		Dim aVectorTransformed As New SourceVector
		Dim aVector As New SourceVector()

		'NOTE: Too small.
		'aVectorTransformed.x = vertex.x
		'aVectorTransformed.y = vertex.y
		'aVectorTransformed.z = vertex.z
		'------
		'NOTE: Rotated for:
		'      simple_shape
		'      L4D2 w_models\weapons\w_minigun
		'aVectorTransformed.x = 1 / 0.0254 * vertex.x
		'aVectorTransformed.y = 1 / 0.0254 * vertex.y
		'aVectorTransformed.z = 1 / 0.0254 * vertex.z
		'------
		'NOTE: Works for:
		'      simple_shape
		'      L4D2 w_models\weapons\w_minigun
		'      L4D2 w_models\weapons\w_smg_uzi
		'      L4D2 props_vehicles\van
		'aVectorTransformed.x = 1 / 0.0254 * vertex.z
		'aVectorTransformed.y = 1 / 0.0254 * -vertex.x
		'aVectorTransformed.z = 1 / 0.0254 * -vertex.y
		'------
		'NOTE: Rotated for:
		'      L4D2 w_models\weapons\w_minigun
		'aVectorTransformed.x = 1 / 0.0254 * vertex.x
		'aVectorTransformed.y = 1 / 0.0254 * -vertex.y
		'aVectorTransformed.z = 1 / 0.0254 * vertex.z
		'------
		'NOTE: Rotated for:
		'      L4D2 props_vehicles\van
		'aVectorTransformed.x = 1 / 0.0254 * vertex.z
		'aVectorTransformed.y = 1 / 0.0254 * -vertex.y
		'aVectorTransformed.z = 1 / 0.0254 * vertex.x
		'------
		'NOTE: Rotated for:
		'      L4D2 w_models\weapons\w_minigun
		'aVector.x = 1 / 0.0254 * vertex.x
		'aVector.y = 1 / 0.0254 * vertex.y
		'aVector.z = 1 / 0.0254 * vertex.z
		'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'------
		'NOTE: Rotated for:
		'      L4D2 w_models\weapons\w_minigun
		'aVector.x = 1 / 0.0254 * vertex.x
		'aVector.y = 1 / 0.0254 * -vertex.y
		'aVector.z = 1 / 0.0254 * vertex.z
		'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'------
		'NOTE: Works for:
		'      L4D2 w_models\weapons\w_minigun
		'      L4D2 w_models\weapons\w_smg_uzi
		'NOTE: Rotated for:
		'      simple_shape
		'      L4D2 props_vehicles\van
		'NOTE: Each mesh piece rotated for:
		'      L4D2 survivors\survivor_producer
		'aVector.x = 1 / 0.0254 * vertex.z
		'aVector.y = 1 / 0.0254 * -vertex.y
		'aVector.z = 1 / 0.0254 * vertex.x
		'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'------
		'NOTE: Works for:
		'      simple_shape
		'      L4D2 props_vehicles\van
		'      L4D2 survivors\survivor_producer
		'      L4D2 w_models\weapons\w_autoshot_m4super
		'      L4D2 w_models\weapons\w_desert_eagle
		'      L4D2 w_models\weapons\w_minigun
		'      L4D2 w_models\weapons\w_rifle_m16a2
		'      L4D2 w_models\weapons\w_smg_uzi
		'NOTE: Rotated for:
		'      L4D2 w_models\weapons\w_desert_rifle
		'      L4D2 w_models\weapons\w_shotgun_spas
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
		'------
		If Me.thePhyFileData.theSourcePhyIsCollisionModel Then
			'Dim copyOfVector As New SourceVector()
			''copyOfVector.x = 1 / 0.0254 * vertex.x
			''copyOfVector.y = 1 / 0.0254 * vertex.y
			''copyOfVector.z = 1 / 0.0254 * vertex.z
			''copyOfVector.x = 1 / 0.0254 * vertex.x
			''copyOfVector.y = 1 / 0.0254 * vertex.z
			''copyOfVector.z = 1 / 0.0254 * -vertex.y
			'copyOfVector.x = 1 / 0.0254 * vertex.z
			'copyOfVector.y = 1 / 0.0254 * -vertex.x
			'copyOfVector.z = 1 / 0.0254 * -vertex.y
			'aVector = MathModule.VectorTransform(copyOfVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			''aVector.x = 1 / 0.0254 * vertex.z
			''aVector.y = 1 / 0.0254 * -vertex.x
			''aVector.z = 1 / 0.0254 * -vertex.y
			''aVectorTransformed.x = 1 / 0.0254 * vertex.z
			''aVectorTransformed.y = 1 / 0.0254 * -vertex.x
			''aVectorTransformed.z = 1 / 0.0254 * -vertex.y

			'Dim debug As Integer = 4242

			''Dim temp As Double
			''temp = aVector.y
			''aVector.y = aVector.z
			''aVector.z = -temp
			''------
			''aVector.y = -aVector.y
			''------
			''Dim temp As Double
			''temp = aVector.x
			''aVector.x = aVector.z
			''aVector.z = -aVector.y
			''aVector.y = -temp
			''------
			''Dim temp As Double
			''temp = aVector.x
			''aVectorTransformed.x = aVector.z
			''aVectorTransformed.z = -aVector.y
			''aVectorTransformed.y = -temp

			'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

			'[2017-12-22]
			' Correct for cube_like_mesh
			'aVectorTransformed.x = 1 / 0.0254 * vertex.z
			'aVectorTransformed.y = 1 / 0.0254 * -vertex.x
			'aVectorTransformed.z = 1 / 0.0254 * -vertex.y
			'------
			' Correct for L4D2 w_models/weapons/w_desert_rifle.mdl
			'aVectorTransformed.x = 1 / 0.0254 * vertex.z
			'aVectorTransformed.y = 1 / 0.0254 * -vertex.y
			'aVectorTransformed.z = 1 / 0.0254 * vertex.x
			'------
			'aVector.x = 1 / 0.0254 * vertex.z
			'aVector.y = 1 / 0.0254 * -vertex.x
			'aVector.z = 1 / 0.0254 * -vertex.y
			'aVector.x = 1 / 0.0254 * vertex.x
			'aVector.y = 1 / 0.0254 * vertex.z
			'aVector.z = 1 / 0.0254 * -vertex.y
			'aVector.x = 1 / 0.0254 * vertex.x
			'aVector.y = 1 / 0.0254 * vertex.y
			'aVector.z = 1 / 0.0254 * vertex.z
			'aVector.x = 1 / 0.0254 * vertex.x
			'aVector.y = 1 / 0.0254 * -vertex.z
			'aVector.z = 1 / 0.0254 * vertex.y
			'aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			'aVector.x = 1 / 0.0254 * vertex.x
			'aVector.y = 1 / 0.0254 * vertex.z
			'aVector.z = 1 / 0.0254 * -vertex.y
			'aVector.x = 1 / 0.0254 * vertex.z
			'aVector.y = 1 / 0.0254 * -vertex.x
			'aVector.z = 1 / 0.0254 * -vertex.y
			'aVector.x = 1 / 0.0254 * vertex.x
			'aVector.y = 1 / 0.0254 * vertex.y
			'aVector.z = 1 / 0.0254 * vertex.z
			'aVectorTransformed = MathModule.VectorITransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			'aVector.x = 1 / 0.0254 * vertex.x
			'aVector.y = 1 / 0.0254 * vertex.y
			'aVector.z = 1 / 0.0254 * vertex.z
			'aVector.x = 1 / 0.0254 * vertex.x  
			'aVector.y = 1 / 0.0254 * vertex.z  
			'aVector.z = 1 / 0.0254 * -vertex.y 
			'aVector.x = 1 / 0.0254 * -vertex.x
			'aVector.y = 1 / 0.0254 * vertex.z
			'aVector.z = 1 / 0.0254 * vertex.y
			'aVector.x = 1 / 0.0254 * vertex.y
			'aVector.y = 1 / 0.0254 * -vertex.x
			'aVector.z = 1 / 0.0254 * vertex.z
			'aVector.x = 1 / 0.0254 * -vertex.y
			'aVector.y = 1 / 0.0254 * vertex.x
			'aVector.z = 1 / 0.0254 * vertex.z
			'aVector.x = 1 / 0.0254 * vertex.y
			'aVector.y = 1 / 0.0254 * -vertex.x
			'aVector.z = 1 / 0.0254 * -vertex.z   
			'aVector.x = 1 / 0.0254 * vertex.y
			'aVector.y = 1 / 0.0254 * vertex.x
			'aVector.z = 1 / 0.0254 * -vertex.z
			' Correct for cube_like_mesh
			' Correct for L4D2 w_models/weapons/w_desert_rifle.mdl
			' Incorrect for L4D2 w_models/weapons/w_rifle_m16a2.mdl
			'aVector.x = 1 / 0.0254 * -vertex.y
			'aVector.y = 1 / 0.0254 * -vertex.x
			'aVector.z = 1 / 0.0254 * -vertex.z
			'aVectorTransformed = MathModule.VectorITransform(aVector, Me.poseToWorldColumn0, Me.poseToWorldColumn1, Me.poseToWorldColumn2, Me.poseToWorldColumn3)
			'======
			''FROM: collisionmodel.cpp ConvertToWorldSpace()
			'aVector.x = 1 / 0.0254 * vertex.x
			'aVector.y = 1 / 0.0254 * vertex.z
			'aVector.z = 1 / 0.0254 * -vertex.y
			'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			''Dim worldToBoneColumn0 As New SourceVector()
			''Dim worldToBoneColumn1 As New SourceVector()
			''Dim worldToBoneColumn2 As New SourceVector()
			''Dim worldToBoneColumn3 As New SourceVector()
			''MathModule.AngleMatrix(aBone.rotation.x, aBone.rotation.y, aBone.rotation.z, worldToBoneColumn0, worldToBoneColumn1, worldToBoneColumn2, worldToBoneColumn3)
			''worldToBoneColumn3.x = aBone.position.x
			''worldToBoneColumn3.y = aBone.position.y
			''worldToBoneColumn3.z = aBone.position.z
			''aVector.x = aVectorTransformed.x
			''aVector.y = aVectorTransformed.y
			''aVector.z = aVectorTransformed.z
			''aVectorTransformed = MathModule.VectorTransform(aVector, worldToBoneColumn0, worldToBoneColumn1, worldToBoneColumn2, worldToBoneColumn3)
			'aVector.x = aVectorTransformed.x
			'aVector.y = aVectorTransformed.y
			'aVector.z = aVectorTransformed.z
			'aVectorTransformed = MathModule.VectorTransform(aVector, poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
			'======
			''FROM: collisionmodel.cpp ConvertToWorldSpace()
			'aVector.x = 1 / 0.0254 * vertex.x
			'aVector.y = 1 / 0.0254 * vertex.z
			'aVector.z = 1 / 0.0254 * -vertex.y
			'aVectorTransformed = MathModule.VectorTransform(aVector, poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
			'aVector.x = aVectorTransformed.x
			'aVector.y = aVectorTransformed.y
			'aVector.z = aVectorTransformed.z
			'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			'======
			''FROM: collisionmodel.cpp ConvertToWorldSpace()
			' ''NOTE: These 3 lines work for airport_fuel_truck, ambulance, and army_truck, but not w_desert_file and w_rifle_m16a2.
			'aVector.x = 1 / 0.0254 * vertex.x
			'aVector.y = 1 / 0.0254 * vertex.z
			'aVector.z = 1 / 0.0254 * -vertex.y
			' ''NOTE: These 3 lines work for w_desert_file and w_rifle_m16a2, but not ambulance and army_truck.
			''aVector.x = 1 / 0.0254 * vertex.x
			''aVector.y = 1 / 0.0254 * -vertex.y
			''aVector.z = 1 / 0.0254 * vertex.z
			'aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			'aVector.x = aVectorTransformed.x
			'aVector.y = aVectorTransformed.y
			'aVector.z = aVectorTransformed.z
			''aVector.x = aVectorTransformed.x
			''aVector.y = aVectorTransformed.z
			''aVector.z = -aVectorTransformed.y
			'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			'======
			'FROM: collisionmodel.cpp ConvertToWorldSpace()
			If (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
				'NOTE: These 3 lines do not work for airport_fuel_truck, ambulance, and army_truck.
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.z
				'aVector.z = 1 / 0.0254 * -vertex.y
				'aVector.x = 1 / 0.0254 * vertex.y
				'aVector.y = 1 / 0.0254 * -vertex.x
				'aVector.z = 1 / 0.0254 * vertex.z
				'aVector.x = 1 / 0.0254 * -vertex.y
				'aVector.y = 1 / 0.0254 * -vertex.x
				'aVector.z = 1 / 0.0254 * vertex.z
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.y
				'aVector.z = 1 / 0.0254 * vertex.z
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.z
				'aVector.z = 1 / 0.0254 * vertex.y
				'' Still need a rotate 90 on the z.
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * -vertex.y
				'aVector.z = 1 / 0.0254 * vertex.z
				'aVector.x = 1 / 0.0254 * -vertex.y
				'aVector.y = 1 / 0.0254 * vertex.x
				'aVector.z = 1 / 0.0254 * vertex.z
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * -vertex.z
				'aVector.z = 1 / 0.0254 * vertex.y
				'aVector.x = 1 / 0.0254 * vertex.z
				'aVector.y = 1 / 0.0254 * -vertex.x
				'aVector.z = 1 / 0.0254 * -vertex.y

				'aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				'aVector.x = aVectorTransformed.x
				'aVector.y = aVectorTransformed.y
				'aVector.z = aVectorTransformed.z
				'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

				'------

				'TEST: Works for props_vehciles that use $staticprop.
				aVector.x = 1 / 0.0254 * vertex.z
				aVector.y = 1 / 0.0254 * -vertex.x
				aVector.z = 1 / 0.0254 * -vertex.y
				aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				aVector.x = aVectorTransformed.x
				aVector.y = aVectorTransformed.z
				aVector.z = -aVectorTransformed.y
				aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			Else
				'NOTE: These 3 lines work for w_desert_file and w_rifle_m16a2, but not ambulance and army_truck.
				'TEST: Did not work for 50_cal. Rotated 180. Original model has phys mesh rotated oddly, anyway.
				'TEST: Incorrect. Need to 180 on the Y and -1 on scale Z. Noticable on w_minigun.
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * -vertex.y
				'aVector.z = 1 / 0.0254 * vertex.z
				'TEST: Incorrect. Need to 180 on the Y. Noticable on w_minigun.
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.y
				'aVector.z = 1 / 0.0254 * vertex.z
				'TEST: Incorrect. Need to -1 on scale Z. Noticable on w_minigun.
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.y
				'aVector.z = 1 / 0.0254 * -vertex.z
				'TEST: Works for w_minigun.
				'TEST: Works for 50cal, but be aware that the phys mesh does not look right for the model. It does look like original model, though.
				'TEST: Does not work for Garry's Mod addon "dodge_daytona" 236224475.
				aVector.x = 1 / 0.0254 * vertex.x
				aVector.y = 1 / 0.0254 * -vertex.y
				aVector.z = 1 / 0.0254 * -vertex.z

				aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				aVector.x = aVectorTransformed.x
				aVector.y = aVectorTransformed.y
				aVector.z = aVectorTransformed.z
				aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			End If
		Else
			'FROM: collisionmodel.cpp ConvertToBoneSpace()
			'aVector.x = 1 / 0.0254 * vertex.x
			'aVector.y = 1 / 0.0254 * vertex.y
			'aVector.z = 1 / 0.0254 * vertex.z
			aVector.x = 1 / 0.0254 * vertex.x
			aVector.y = 1 / 0.0254 * vertex.z
			aVector.z = 1 / 0.0254 * -vertex.y
			aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		End If

		'If Me.theMdlFileData.theBoneTransforms IsNot Nothing Then
		'	Dim transform As SourceMdlBoneTransform
		'	Dim boneIndex As Integer
		'	Dim preToBoneColumn0 As New SourceVector()
		'	Dim preToBoneColumn1 As New SourceVector()
		'	Dim preToBoneColumn2 As New SourceVector()
		'	Dim preToBoneColumn3 As New SourceVector()
		'	Dim boneToPostColumn0 As New SourceVector()
		'	Dim boneToPostColumn1 As New SourceVector()
		'	Dim boneToPostColumn2 As New SourceVector()
		'	Dim boneToPostColumn3 As New SourceVector()

		'	'TODO: Find the real boneIndex based on boneName
		'	boneIndex = 0
		'	transform = Me.theMdlFileData.theBoneTransforms(boneIndex)

		'	'MathModule.MatrixInvert(poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
		'	MathModule.R_ConcatTransforms(transform.preTransformColumn0, transform.preTransformColumn1, transform.preTransformColumn2, transform.preTransformColumn3, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3, preToBoneColumn0, preToBoneColumn1, preToBoneColumn2, preToBoneColumn3)
		'	MathModule.R_ConcatTransforms(preToBoneColumn0, preToBoneColumn1, preToBoneColumn2, preToBoneColumn3, transform.postTransformColumn0, transform.postTransformColumn1, transform.postTransformColumn2, transform.postTransformColumn3, boneToPostColumn0, boneToPostColumn1, boneToPostColumn2, boneToPostColumn3)
		'	aVectorTransformed = MathModule.VectorITransform(aVector, boneToPostColumn0, boneToPostColumn1, boneToPostColumn2, boneToPostColumn3)
		'Else
		'	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'End If



		'------
		'NOTE: Works for:
		'      survivor_producer
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * aVector.x
		'phyVertex.y = 1 / 0.0254 * aVector.z
		'phyVertex.z = 1 / 0.0254 * -aVector.y
		'------
		'NOTE: These two lines match orientation for cstrike it_lampholder1 model, 
		'      but still doesn't compile properly.
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * aVector.z
		'phyVertex.y = 1 / 0.0254 * -aVector.x
		'phyVertex.z = 1 / 0.0254 * -aVector.y
		'------
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * aVector.y
		'phyVertex.y = 1 / 0.0254 * aVector.x
		'phyVertex.z = 1 / 0.0254 * -aVector.z
		'------
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * aVector.x
		'phyVertex.y = 1 / 0.0254 * aVector.y
		'phyVertex.z = 1 / 0.0254 * -aVector.z
		'------
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * -aVector.y
		'phyVertex.y = 1 / 0.0254 * aVector.x
		'phyVertex.z = 1 / 0.0254 * aVector.z
		'------
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * -aVector.y
		'phyVertex.y = 1 / 0.0254 * aVector.x
		'phyVertex.z = 1 / 0.0254 * aVector.z
		'------
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * aVector.z
		'phyVertex.y = 1 / 0.0254 * aVector.y
		'phyVertex.z = 1 / 0.0254 * aVector.x
		'------
		'NOTE: Works for:
		'      w_smg_uzi()
		'NOTE: Does not work for:
		'      survivor_producer
		'phyVertex.x = 1 / 0.0254 * aVector.z
		'phyVertex.y = 1 / 0.0254 * -aVector.y
		'phyVertex.z = 1 / 0.0254 * aVector.x
		'------
		'phyVertex.x = 1 / 0.0254 * aVector.z
		'phyVertex.y = 1 / 0.0254 * -aVector.y
		'phyVertex.z = 1 / 0.0254 * -aVector.x
		'------
		'If Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyIsCollisionModel Then
		'	'TEST: Does not rotate L4D2's van phys mesh correctly.
		'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		'	'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
		'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		'	'TEST:  Does not rotate L4D2's van phys mesh correctly.
		'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
		'	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
		'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		'	'TEST: Does not rotate L4D2's van phys mesh correctly.
		'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.z
		'	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.y
		'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.x
		'	'TEST: Does not rotate L4D2's van phys mesh correctly.
		'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		'	'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
		'	'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y
		'	'TEST: Works for L4D2's van phys mesh.
		'	'      Does not work for L4D2 w_model\weapons\w_minigun.mdl.
		'	aVector.x = 1 / 0.0254 * vertex.z
		'	aVector.y = 1 / 0.0254 * -vertex.x
		'	aVector.z = 1 / 0.0254 * -vertex.y
		'Else
		'	'TEST: Does not work for L4D2 w_model\weapons\w_minigun.mdl.
		'	aVector.x = 1 / 0.0254 * vertex.x
		'	aVector.y = 1 / 0.0254 * vertex.z
		'	aVector.z = 1 / 0.0254 * -vertex.y

		'	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'End If
		'------
		'TEST: Does not rotate L4D2's van phys mesh correctly.
		'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
		'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		'TEST: Does not rotate L4D2's van phys mesh correctly.
		'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
		'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
		'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		'TEST: works for survivor_producer; matches ref and phy meshes of van, but both are rotated 90 degrees on z-axis
		'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
		'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y

		'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

		Return aVectorTransformed
	End Function

	'Private Sub CreateAnimationFrameLines(ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc52, ByVal frameIndex As Integer)

	'End Sub

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
	Private Sub CalcAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc52, ByVal frameIndex As Integer)
		Dim s As Double
		Dim animIndex As Integer
		Dim aBone As SourceMdlBone
		Dim aWeight As Double
		Dim anAnimation As SourceMdlAnimation
		Dim rot As SourceVector
		Dim pos As SourceVector
		Dim aFrameLine As AnimationFrameLine
		Dim sectionFrameIndex As Integer

		s = 0

		animIndex = 0

		'If anAnimationDesc.theAnimations Is Nothing OrElse animIndex >= anAnimationDesc.theAnimations.Count Then
		'	anAnimation = Nothing
		'Else
		'	anAnimation = anAnimationDesc.theAnimations(animIndex)
		'End If
		'------
		Dim sectionIndex As Integer
		Dim aSectionOfAnimation As List(Of SourceMdlAnimation)
		If anAnimationDesc.sectionFrameCount = 0 Then
			sectionIndex = 0
			sectionFrameIndex = frameIndex
		Else
			sectionIndex = CInt(Math.Truncate(frameIndex / anAnimationDesc.sectionFrameCount))
			sectionFrameIndex = frameIndex - (sectionIndex * anAnimationDesc.sectionFrameCount)
		End If
		'aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
		'If anAnimationDesc.theSectionsOfAnimations Is Nothing OrElse animIndex >= aSectionOfAnimation.Count Then
		'	anAnimation = Nothing
		'Else
		'	anAnimation = aSectionOfAnimation(animIndex)
		'End If
		anAnimation = Nothing
		aSectionOfAnimation = Nothing
		If anAnimationDesc.theSectionsOfAnimations IsNot Nothing Then
			aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
			If animIndex >= 0 AndAlso animIndex < aSectionOfAnimation.Count Then
				anAnimation = aSectionOfAnimation(animIndex)
			End If
		End If

		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			aBone = Me.theMdlFileData.theBones(boneIndex)

			If aSequenceDesc IsNot Nothing Then
				aWeight = aSequenceDesc.theBoneWeights(boneIndex)
			Else
				'NOTE: This should only be needed for a delta $animation.
				'      Arbitrarily assign 1 so that the following code will add frame lines for this $animation.
				aWeight = 1
			End If

			If anAnimation IsNot Nothing AndAlso anAnimation.boneIndex = boneIndex Then
				If aWeight > 0 Then
					If Me.theAnimationFrameLines.ContainsKey(boneIndex) Then
						aFrameLine = Me.theAnimationFrameLines(boneIndex)
					Else
						aFrameLine = New AnimationFrameLine()
						Me.theAnimationFrameLines.Add(boneIndex, aFrameLine)
					End If

					aFrameLine.rotationQuat = New SourceQuaternion()
					'rot = CalcBoneRotation(frameIndex, s, aBone, anAnimation)
					rot = CalcBoneRotation(sectionFrameIndex, s, aBone, anAnimation, aFrameLine.rotationQuat)
					aFrameLine.rotation = New SourceVector()

					'NOTE: z = z puts head-foot axis horizontally
					'      facing viewer
					aFrameLine.rotation.x = rot.x
					aFrameLine.rotation.y = rot.y
					aFrameLine.rotation.z = rot.z
					'------
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = rot.y - 1.570796
					'aFrameLine.rotation.z = rot.z
					'------
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = rot.x
					'aFrameLine.rotation.z = rot.z
					'------
					'------
					'NOTE: x = z puts head-foot axis horizontally
					'      facing away from viewer
					'aFrameLine.rotation.x = rot.z
					'aFrameLine.rotation.y = rot.y
					'aFrameLine.rotation.z = rot.x
					'------
					'aFrameLine.rotation.x = rot.z
					'aFrameLine.rotation.y = rot.x
					'aFrameLine.rotation.z = rot.y
					'------
					'------
					'NOTE: y = z  : head-foot axis vertically correctly
					'      x = -x : upside-down
					'      z = y  : 
					' facing to window right
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					'NOTE: Upside-down; facing to window left
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					' facing to window right
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					'NOTE: Upside-down; facing to window left
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					' facing to window left
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					'NOTE: Upside-down; facing to window right
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					' facing to window left
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					'NOTE: Upside-down; facing to window right
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					'------
					' facing to window right
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					'NOTE: Upside-down; facing to window left
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					' facing to window right
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.x
					'------
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.x
					'------
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.x
					'------
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.x

					aFrameLine.rotation.debug_text = rot.debug_text

					'pos = Me.CalcBonePosition(frameIndex, s, aBone, anAnimation)
					pos = Me.CalcBonePosition(sectionFrameIndex, s, aBone, anAnimation)
					aFrameLine.position = New SourceVector()
					aFrameLine.position.x = pos.x
					aFrameLine.position.y = pos.y
					aFrameLine.position.z = pos.z
					aFrameLine.position.debug_text = pos.debug_text
				End If

				animIndex += 1
				'If animIndex >= anAnimationDesc.theAnimations.Count Then
				'	anAnimation = Nothing
				'Else
				'	anAnimation = anAnimationDesc.theAnimations(animIndex)
				'End If
				If animIndex >= aSectionOfAnimation.Count Then
					anAnimation = Nothing
				Else
					anAnimation = aSectionOfAnimation(animIndex)
				End If
			ElseIf aWeight > 0 Then
				If Me.theAnimationFrameLines.ContainsKey(boneIndex) Then
					aFrameLine = Me.theAnimationFrameLines(boneIndex)
				Else
					aFrameLine = New AnimationFrameLine()
					Me.theAnimationFrameLines.Add(boneIndex, aFrameLine)
				End If

				'NOTE: Changed in v0.25.
				'If (anAnimationDesc.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
				If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
					aFrameLine.rotation = New SourceVector()
					aFrameLine.rotation.x = 0
					aFrameLine.rotation.y = 0
					aFrameLine.rotation.z = 0
					aFrameLine.rotation.debug_text = "desc_delta"

					aFrameLine.position = New SourceVector()
					aFrameLine.position.x = 0
					aFrameLine.position.y = 0
					aFrameLine.position.z = 0
					aFrameLine.position.debug_text = "desc_delta"
				Else
					aFrameLine.rotation = New SourceVector()
					aFrameLine.rotation.x = aBone.rotation.x
					aFrameLine.rotation.y = aBone.rotation.y
					aFrameLine.rotation.z = aBone.rotation.z
					aFrameLine.rotation.debug_text = "desc_bone"

					aFrameLine.position = New SourceVector()
					aFrameLine.position.x = aBone.position.x
					aFrameLine.position.y = aBone.position.y
					aFrameLine.position.z = aBone.position.z
					aFrameLine.position.debug_text = "desc_bone"
				End If
			End If
		Next
	End Sub

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: return a sub frame rotation for a single bone
	'//-----------------------------------------------------------------------------
	'void CalcBoneQuaternion( int frame, float s, 
	'						const Quaternion &baseQuat, const RadianEuler &baseRot, const Vector &baseRotScale, 
	'						int iBaseFlags, const Quaternion &baseAlignment, 
	'						const mstudioanim_t *panim, Quaternion &q )
	'{
	'	if ( panim->flags & STUDIO_ANIM_RAWROT )
	'	{
	'		q = *(panim->pQuat48());
	'		Assert( q.IsValid() );
	'		return;
	'	} 

	'	if ( panim->flags & STUDIO_ANIM_RAWROT2 )
	'	{
	'		q = *(panim->pQuat64());
	'		Assert( q.IsValid() );
	'		return;
	'	}

	'	if ( !(panim->flags & STUDIO_ANIM_ANIMROT) )
	'	{
	'		if (panim->flags & STUDIO_ANIM_DELTA)
	'		{
	'			q.Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'		}
	'		else
	'		{
	'			q = baseQuat;
	'		}
	'		return;
	'	}

	'	mstudioanim_valueptr_t *pValuesPtr = panim->pRotV();

	'	if (s > 0.001f)
	'	{
	'		QuaternionAligned	q1, q2;
	'		RadianEuler			angle1, angle2;

	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 0 ), baseRotScale.x, angle1.x, angle2.x );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 1 ), baseRotScale.y, angle1.y, angle2.y );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 2 ), baseRotScale.z, angle1.z, angle2.z );

	'		if (!(panim->flags & STUDIO_ANIM_DELTA))
	'		{
	'			angle1.x = angle1.x + baseRot.x;
	'			angle1.y = angle1.y + baseRot.y;
	'			angle1.z = angle1.z + baseRot.z;
	'			angle2.x = angle2.x + baseRot.x;
	'			angle2.y = angle2.y + baseRot.y;
	'			angle2.z = angle2.z + baseRot.z;
	'		}

	'		Assert( angle1.IsValid() && angle2.IsValid() );
	'		if (angle1.x != angle2.x || angle1.y != angle2.y || angle1.z != angle2.z)
	'		{
	'			AngleQuaternion( angle1, q1 );
	'			AngleQuaternion( angle2, q2 );

	'	#ifdef _X360
	'			fltx4 q1simd, q2simd, qsimd;
	'			q1simd = LoadAlignedSIMD( q1 );
	'			q2simd = LoadAlignedSIMD( q2 );
	'			qsimd = QuaternionBlendSIMD( q1simd, q2simd, s );
	'			StoreUnalignedSIMD( q.Base(), qsimd );
	'	#else
	'			QuaternionBlend( q1, q2, s, q );
	'#End If
	'		}
	'		else
	'		{
	'			AngleQuaternion( angle1, q );
	'		}
	'	}
	'	else
	'	{
	'		RadianEuler			angle;

	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 0 ), baseRotScale.x, angle.x );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 1 ), baseRotScale.y, angle.y );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 2 ), baseRotScale.z, angle.z );

	'		if (!(panim->flags & STUDIO_ANIM_DELTA))
	'		{
	'			angle.x = angle.x + baseRot.x;
	'			angle.y = angle.y + baseRot.y;
	'			angle.z = angle.z + baseRot.z;
	'		}

	'		Assert( angle.IsValid() );
	'		AngleQuaternion( angle, q );
	'	}

	'	Assert( q.IsValid() );

	'	// align to unified bone
	'	if (!(panim->flags & STUDIO_ANIM_DELTA) && (iBaseFlags & BONE_FIXED_ALIGNMENT))
	'	{
	'		QuaternionAlign( baseAlignment, q, q );
	'	}
	'}
	'
	'inline void CalcBoneQuaternion( int frame, float s, 
	'						const mstudiobone_t *pBone,
	'						const mstudiolinearbone_t *pLinearBones,
	'						const mstudioanim_t *panim, Quaternion &q )
	'{
	'	if (pLinearBones)
	'	{
	'		CalcBoneQuaternion( frame, s, pLinearBones->quat(panim->bone), pLinearBones->rot(panim->bone), pLinearBones->rotscale(panim->bone), pLinearBones->flags(panim->bone), pLinearBones->qalignment(panim->bone), panim, q );
	'	}
	'	else
	'	{
	'		CalcBoneQuaternion( frame, s, pBone->quat, pBone->rot, pBone->rotscale, pBone->flags, pBone->qAlignment, panim, q );
	'	}
	'}
	'Private Function CalcBoneQuaternion(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation) As SourceQuaternion
	'	Dim rot As New SourceQuaternion()
	'	Dim angleVector As New SourceVector()

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
	'		rot.x = anAnimation.theRot48.x
	'		rot.y = anAnimation.theRot48.y
	'		rot.z = anAnimation.theRot48.z
	'		rot.w = anAnimation.theRot48.w
	'		Return rot
	'	ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
	'		rot.x = anAnimation.theRot64.x
	'		rot.y = anAnimation.theRot64.y
	'		rot.z = anAnimation.theRot64.z
	'		rot.w = anAnimation.theRot64.w
	'		Return rot
	'	End If

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) = 0 Then
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
	'			rot.x = 0
	'			rot.y = 0
	'			rot.z = 0
	'			rot.w = 1
	'		Else
	'			rot.x = aBone.quat.x
	'			rot.y = aBone.quat.y
	'			rot.z = aBone.quat.z
	'			rot.w = aBone.quat.w
	'		End If
	'		Return rot
	'	End If

	'	Dim rotV As SourceMdlAnimationValuePointer

	'	rotV = anAnimation.theRotV

	'	If rotV.animValueOffset(0) <= 0 Then
	'		angleVector.x = 0
	'	Else
	'		angleVector.x = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(0), aBone.rotationScaleX)
	'	End If
	'	If rotV.animValueOffset(1) <= 0 Then
	'		angleVector.y = 0
	'	Else
	'		angleVector.y = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(1), aBone.rotationScaleY)
	'	End If
	'	If rotV.animValueOffset(2) <= 0 Then
	'		angleVector.z = 0
	'	Else
	'		angleVector.z = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(2), aBone.rotationScaleZ)
	'	End If

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
	'		angleVector.x += aBone.quat.x
	'		angleVector.y += aBone.quat.y
	'		angleVector.z += aBone.quat.z
	'	End If

	'	rot = MathModule.AngleQuaternion(angleVector)

	'	'	if (!(panim->flags & STUDIO_ANIM_DELTA) && (iBaseFlags & BONE_FIXED_ALIGNMENT))
	'	'	{
	'	'		QuaternionAlign( baseAlignment, q, q );
	'	'	}

	'	Return rot
	'End Function
	Private Function CalcBoneRotation(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation, ByRef rotationQuat As SourceQuaternion) As SourceVector
		Dim rot As New SourceQuaternion()
		Dim angleVector As New SourceVector()

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
			rot.x = anAnimation.theRot48bits.x
			rot.y = anAnimation.theRot48bits.y
			rot.z = anAnimation.theRot48bits.z
			rot.w = anAnimation.theRot48bits.w
			rotationQuat.x = rot.x
			rotationQuat.y = rot.y
			rotationQuat.z = rot.z
			rotationQuat.w = rot.w
			angleVector = MathModule.ToEulerAngles(rot)

			angleVector.debug_text = "raw48"
			Return angleVector
		ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
			'angleVector.x = anAnimation.theRot64.xRadians
			'angleVector.y = anAnimation.theRot64.yRadians
			'angleVector.z = anAnimation.theRot64.zRadians
			'------
			rot.x = anAnimation.theRot64bits.x
			rot.y = anAnimation.theRot64bits.y
			rot.z = anAnimation.theRot64bits.z
			rot.w = anAnimation.theRot64bits.w
			rotationQuat.x = rot.x
			rotationQuat.y = rot.y
			rotationQuat.z = rot.z
			rotationQuat.w = rot.w
			angleVector = MathModule.ToEulerAngles(rot)

			''TEST: Rotate z by -90 degrees.
			''TEST: Rotate y by -90 degrees.
			'angleVector.y += MathModule.DegreesToRadians(-90)

			angleVector.debug_text = "raw64 (" + rot.x.ToString() + ", " + rot.y.ToString() + ", " + rot.z.ToString() + ", " + rot.w.ToString() + ")"
			Return angleVector
		End If

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) = 0 Then
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
				angleVector.x = 0
				angleVector.y = 0
				angleVector.z = 0
				rotationQuat.x = 0
				rotationQuat.y = 0
				rotationQuat.z = 0
				rotationQuat.w = 0
				angleVector.debug_text = "delta"
			Else
				angleVector.x = aBone.rotation.x
				angleVector.y = aBone.rotation.y
				angleVector.z = aBone.rotation.z
				rotationQuat.x = 0
				rotationQuat.y = 0
				rotationQuat.z = 0
				rotationQuat.w = 0
				angleVector.debug_text = "bone"
			End If
			Return angleVector
		End If

		Dim rotV As SourceMdlAnimationValuePointer

		rotV = anAnimation.theRotV

		If rotV.animXValueOffset <= 0 Then
			angleVector.x = 0
		Else
			angleVector.x = Me.ExtractAnimValue(frameIndex, rotV.theAnimXValues, aBone.rotationScale.x)
		End If
		If rotV.animYValueOffset <= 0 Then
			angleVector.y = 0
		Else
			angleVector.y = Me.ExtractAnimValue(frameIndex, rotV.theAnimYValues, aBone.rotationScale.y)
		End If
		If rotV.animZValueOffset <= 0 Then
			angleVector.z = 0
		Else
			angleVector.z = Me.ExtractAnimValue(frameIndex, rotV.theAnimZValues, aBone.rotationScale.z)
		End If

		angleVector.debug_text = "anim"

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
			angleVector.x += aBone.rotation.x
			angleVector.y += aBone.rotation.y
			angleVector.z += aBone.rotation.z
			angleVector.debug_text += "+bone"
		End If

		rotationQuat = MathModule.EulerAnglesToQuaternion(angleVector)
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
	Private Function CalcBonePosition(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation) As SourceVector
		Dim pos As New SourceVector()

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0 Then
			'If aBone.parentBoneIndex = -1 Then
			'	pos.x = anAnimation.thePos.y
			'	pos.y = -anAnimation.thePos.x
			'	pos.z = anAnimation.thePos.z
			'Else
			pos.x = anAnimation.thePos.x
			pos.y = anAnimation.thePos.y
			pos.z = anAnimation.thePos.z
			'	'------
			'	'pos.y = anAnimation.thePos.z
			'	'pos.z = anAnimation.thePos.y
			'	'------
			'	'pos.x = anAnimation.thePos.y
			'	'pos.y = -anAnimation.thePos.x
			'	'pos.z = anAnimation.thePos.z
			'End If

			pos.debug_text = "raw"
			Return pos
		ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) = 0 Then
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
				pos.x = 0
				pos.y = 0
				pos.z = 0
				pos.debug_text = "delta"
			Else
				pos.x = aBone.position.x
				pos.y = aBone.position.y
				pos.z = aBone.position.z
				'pos.y = aBone.positionZ
				'pos.z = -aBone.positionY
				pos.debug_text = "bone"
			End If
			Return pos
		End If

		Dim posV As SourceMdlAnimationValuePointer

		posV = anAnimation.thePosV

		If posV.animXValueOffset <= 0 Then
			pos.x = 0
		Else
			pos.x = Me.ExtractAnimValue(frameIndex, posV.theAnimXValues, aBone.positionScale.x)
		End If

		If posV.animYValueOffset <= 0 Then
			pos.y = 0
		Else
			pos.y = Me.ExtractAnimValue(frameIndex, posV.theAnimYValues, aBone.positionScale.y)
		End If

		If posV.animZValueOffset <= 0 Then
			pos.z = 0
		Else
			pos.z = Me.ExtractAnimValue(frameIndex, posV.theAnimZValues, aBone.positionScale.z)
		End If

		pos.debug_text = "anim"

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
			pos.x += aBone.position.x
			pos.y += aBone.position.y
			pos.z += aBone.position.z
			pos.debug_text += "+bone"
		End If

		Return pos
	End Function

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'void ExtractAnimValue( int frame, mstudioanimvalue_t *panimvalue, float scale, float &v1 )
	'{
	'	if ( !panimvalue )
	'	{
	'		v1 = 0;
	'		return;
	'	}

	'	int k = frame;

	'	while (panimvalue->num.total <= k)
	'	{
	'		k -= panimvalue->num.total;
	'		panimvalue += panimvalue->num.valid + 1;
	'		if ( panimvalue->num.total == 0 )
	'		{
	'			Assert( 0 ); // running off the end of the animation stream is bad
	'			v1 = 0;
	'			return;
	'		}
	'	}
	'	if (panimvalue->num.valid > k)
	'	{
	'		v1 = panimvalue[k+1].value * scale;
	'	}
	'	else
	'	{
	'		// get last valid data block
	'		v1 = panimvalue[panimvalue->num.valid].value * scale;
	'	}
	'}
	Public Function ExtractAnimValue(ByVal frameIndex As Integer, ByVal animValues As List(Of SourceMdlAnimationValue), ByVal scale As Double) As Double
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
				v1 = animValues(animValueIndex + k + 1).value * scale
			Else
				'NOTE: Needs to be offset from current animValues index to match the C++ code above in comment.
				v1 = animValues(animValueIndex + animValues(animValueIndex).valid).value * scale
			End If
		Catch
		End Try

		Return v1
	End Function

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData52
	Private thePhyFileData As SourcePhyFileData
	Private theVvdFileData As SourceVvdFileData04

	Private theAnimationFrameLines As SortedList(Of Integer, AnimationFrameLine)

	Private worldToPoseColumn0 As New SourceVector()
	Private worldToPoseColumn1 As New SourceVector()
	Private worldToPoseColumn2 As New SourceVector()
	Private worldToPoseColumn3 As New SourceVector()
	Private poseToWorldColumn0 As New SourceVector()
	Private poseToWorldColumn1 As New SourceVector()
	Private poseToWorldColumn2 As New SourceVector()
	Private poseToWorldColumn3 As New SourceVector()

	Private previousFrameRotation As New SourceVector()
	Private frame0Position As New SourceVector()

#End Region

End Class
