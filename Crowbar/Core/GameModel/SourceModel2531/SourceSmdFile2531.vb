Imports System.IO

Public Class SourceSmdFile2531

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData2531)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
	End Sub

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData2531, ByVal phyFileData As SourcePhyFileData)
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

		Dim rotation As SourceVector

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
			rotation = MathModule.ToEulerAngles(Me.theMdlFileData.theBones(boneIndex).rotation)

			line = "    "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).position.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).position.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).position.z.ToString("0.000000", TheApp.InternalNumberFormat)
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

	Public Sub WriteSkeletonSectionForAnimation(ByVal aSequenceDescBase As SourceMdlSequenceDescBase, ByVal anAnimationDescBase As SourceMdlAnimationDescBase)
		Dim line As String = ""
		Dim boneIndex As Integer
		Dim aFrameLine As AnimationFrameLine
		Dim position As New SourceVector()
		Dim rotation As New SourceVector()
		Dim aSequenceDesc As SourceMdlSequenceDesc2531
		Dim anAnimationDesc As SourceMdlAnimationDesc2531

		aSequenceDesc = CType(aSequenceDescBase, SourceMdlSequenceDesc2531)
		anAnimationDesc = CType(anAnimationDescBase, SourceMdlAnimationDesc2531)

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

				position.x = aFrameLine.position.x
				position.y = aFrameLine.position.y
				position.z = aFrameLine.position.z

				rotation.x = aFrameLine.rotation.x
				rotation.y = aFrameLine.rotation.y
				rotation.z = aFrameLine.rotation.z

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

	Public Sub WriteTrianglesSection(ByVal lodIndex As Integer, ByVal aVtxModel As SourceVtxModel107, ByVal aBodyModel As SourceMdlModel2531, ByVal bodyPartVertexIndexStart As Integer)
		Dim line As String = ""
		Dim materialLine As String = ""
		Dim vertex1Line As String = ""
		Dim vertex2Line As String = ""
		Dim vertex3Line As String = ""

		'triangles
		line = "triangles"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Dim aVtxLod As SourceVtxModelLod107
		Dim aVtxMesh As SourceVtxMesh107
		Dim aStripGroup As SourceVtxStripGroup107
		Dim aStrip As SourceVtxStrip107
		Dim materialIndex As Integer
		Dim materialName As String
		Dim meshVertexIndexStart As Integer

		Try
			aVtxLod = aVtxModel.theVtxModelLods(lodIndex)

			If aVtxLod.theVtxMeshes IsNot Nothing Then
				For meshIndex As Integer = 0 To aVtxLod.theVtxMeshes.Count - 1
					aVtxMesh = aVtxLod.theVtxMeshes(meshIndex)
					materialIndex = aBodyModel.theMeshes(meshIndex).materialIndex
					materialName = Me.theMdlFileData.theTextures(materialIndex).theFileName
					'TODO: This was used in previous versions, but maybe should leave as above.
					'materialName = Path.GetFileName(Me.theSourceEngineModel.theMdlFileHeader.theTextures(materialIndex).theName)

					meshVertexIndexStart = aBodyModel.theMeshes(meshIndex).vertexIndexStart

					If aVtxMesh.theVtxStripGroups IsNot Nothing Then
						For groupIndex As Integer = 0 To aVtxMesh.theVtxStripGroups.Count - 1
							aStripGroup = aVtxMesh.theVtxStripGroups(groupIndex)

							If aStripGroup.theVtxStrips IsNot Nothing AndAlso aStripGroup.theVtxIndexes IsNot Nothing AndAlso (aStripGroup.theVtxVertexesForStaticProp IsNot Nothing OrElse aStripGroup.theVtxVertexes IsNot Nothing) Then
								For vtxStripIndex As Integer = 0 To aStripGroup.theVtxStrips.Count - 1
									aStrip = aStripGroup.theVtxStrips(vtxStripIndex)

									For vtxIndexIndex As Integer = 0 To aStrip.indexCount - 3 Step 3
										materialLine = materialName
										vertex1Line = Me.GetVertexLine(aBodyModel, aStripGroup, vtxIndexIndex + aStrip.indexMeshIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
										vertex2Line = Me.GetVertexLine(aBodyModel, aStripGroup, vtxIndexIndex + aStrip.indexMeshIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
										vertex3Line = Me.GetVertexLine(aBodyModel, aStripGroup, vtxIndexIndex + aStrip.indexMeshIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
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
		Dim aBone As SourceMdlBone2531
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
						'aBone = Me.theMdlFileData.theBones(faceSection.theBoneIndex)

						For triangleIndex As Integer = 0 To faceSection.theFaces.Count - 1
							aTriangle = faceSection.theFaces(triangleIndex)

							line = "  phy"
							Me.theOutputFileStreamWriter.WriteLine(line)

							For vertexIndex As Integer = 0 To aTriangle.vertexIndex.Length - 1
								'phyVertex = collisionData.theVertices(aTriangle.vertexIndex(vertexIndex))
								phyVertex = faceSection.theVertices(aTriangle.vertexIndex(vertexIndex))

								aVectorTransformed = Me.TransformPhyVertex(aBone, phyVertex.vertex, aSourcePhysCollisionModel)
								'aVectorTransformed = Me.TransformPhyVertex(aBone, phyVertex.vertex)

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

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

	Private Function GetVertexLine(ByVal aBodyModel As SourceMdlModel2531, ByVal aStripGroup As SourceVtxStripGroup107, ByVal aVtxIndexIndex As Integer, ByVal lodIndex As Integer, ByVal meshVertexIndexStart As Integer, ByVal bodyPartVertexIndexStart As Integer) As String
		Dim line As String

		Dim aVtxVertexIndex As UShort
		Dim originalMeshVertexIndex As UInt16
		Dim vertexIndex As Integer
		Dim boneIndexes As New List(Of Integer)(4)
		Dim position As New SourceVector()
		Dim normal As New SourceVector()
		Dim texCoordU As Double
		Dim texCoordV As Double
		Dim weights As New List(Of Double)(4)

		line = ""
		Try
			aVtxVertexIndex = aStripGroup.theVtxIndexes(aVtxIndexIndex)
			If (aStripGroup.flags And SourceVtxStripGroup107.STRIPGROUP_USES_STATIC_PROP_VERTEXES) > 0 Then
				originalMeshVertexIndex = aStripGroup.theVtxVertexesForStaticProp(aVtxVertexIndex)
			Else
				originalMeshVertexIndex = aStripGroup.theVtxVertexes(aVtxVertexIndex).originalMeshVertexIndex
			End If
			'vertexIndex = originalMeshVertexIndex + bodyPartVertexIndexStart + meshVertexIndexStart
			vertexIndex = originalMeshVertexIndex + meshVertexIndexStart

			If aBodyModel.vertexListType = 0 Then
				For i As Integer = 0 To aBodyModel.theVertexesType0(vertexIndex).boneIndex.Length - 1
					weights.Add(aBodyModel.theVertexesType0(vertexIndex).weight(i) / 255)
					If weights(i) > 0 Then
						boneIndexes.Add(aBodyModel.theVertexesType0(vertexIndex).boneIndex(i))
					End If
				Next
				position.x = aBodyModel.theVertexesType0(vertexIndex).position.x
				position.y = aBodyModel.theVertexesType0(vertexIndex).position.y
				position.z = aBodyModel.theVertexesType0(vertexIndex).position.z
				normal.x = aBodyModel.theVertexesType0(vertexIndex).normal.x
				normal.y = aBodyModel.theVertexesType0(vertexIndex).normal.y
				normal.z = aBodyModel.theVertexesType0(vertexIndex).normal.z
				texCoordU = aBodyModel.theVertexesType0(vertexIndex).texCoordU
				texCoordV = aBodyModel.theVertexesType0(vertexIndex).texCoordV
			ElseIf aBodyModel.vertexListType = 1 Then
				boneIndexes.Add(0)
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMinPosition.x
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMinPosition.y
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMinPosition.z
				''position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMinPosition.y
				''position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMinPosition.z
				''position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMinPosition.x
				'normal.x = (aBodyModel.theVertexesType1(vertexIndex).normalX / 65535) * Me.theMdlFileData.hullMaxPosition.x
				'normal.y = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535) * Me.theMdlFileData.hullMaxPosition.y
				'normal.z = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535) * Me.theMdlFileData.hullMaxPosition.z
				'texCoordU = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535)
				'texCoordV = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535)

				' Too big compared to Therese.
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ)
				' Too big compared to Therese.
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 255)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 255)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 255)
				' Not correct scale for some models and car_idle seems shortened on one axis. Thus, it seems 3 scale values should be used.
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 4095)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 4095)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 4095)
				' Too small compared to Therese.
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 32767)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 32767)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 32767)
				' Too small compared to Therese.
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535)
				' Flattens car_idle
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.unknown01
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.unknown02
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.unknown03

				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMinPosition.x
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMinPosition.y
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMinPosition.z
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 255) * Me.theMdlFileData.hullMinPosition.x
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 255) * Me.theMdlFileData.hullMinPosition.y
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 255) * Me.theMdlFileData.hullMinPosition.z
				'normal.x = (aBodyModel.theVertexesType1(vertexIndex).normalX / 65535)
				'normal.y = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535)
				'normal.z = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535)
				'texCoordU = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535)
				'texCoordV = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535)
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.unknown02
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.unknown02
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.unknown02
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMaxPosition.x
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMaxPosition.y
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMaxPosition.z
				'Dim aBone As SourceMdlBone2531
				'aBone = Me.theMdlFileData.theBones(0)
				'Dim vecin As New SourceVector
				'vecin.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535)
				'vecin.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535)
				'vecin.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535)
				'position = MathModule.VectorTransform(vecin, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (aBodyModel.theVertexesType1(vertexIndex).scaleX)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (aBodyModel.theVertexesType1(vertexIndex).scaleY)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (aBodyModel.theVertexesType1(vertexIndex).scaleZ)
				'NOTE: This seems to work for many models, but seems to be wrong for:  
				'      models\character\npc\common\freshcorpses\femalefreshcorpse\femalefreshcorpse.mdl
				'      models\character\monster\werewolf\werewolf_head\werewolf_head.mdl
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z)
				'NOTE: This seems to work for many models, but seems to be wrong for:  
				'      models\character\npc\common\freshcorpses\femalefreshcorpse\femalefreshcorpse.mdl
				position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0))
				position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1))
				position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2))
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x - Me.theMdlFileData.unknown01)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y - Me.theMdlFileData.unknown02)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z - Me.theMdlFileData.unknown03)
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x + Me.theMdlFileData.unknown01)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y + Me.theMdlFileData.unknown02)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z + Me.theMdlFileData.unknown03)
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) + Me.theMdlFileData.unknown01)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) + Me.theMdlFileData.unknown02)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) + Me.theMdlFileData.unknown03)
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) + Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0))
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) + Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1))
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) + Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2))
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0))
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1))
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2))
				' This puts the corpse facing up, but still stretched along z-axis.
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0))
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1))
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2))
				' This is close, but scale is probably wrong and slightly stretched along z-axis.
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2)
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2)
				' Completely flat.
				'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX) * (Me.theMdlFileData.hullMaxPosition.x) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0)
				'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY) * (Me.theMdlFileData.hullMaxPosition.y) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1)
				'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ) * (Me.theMdlFileData.hullMaxPosition.z) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2)

				'TODO: Verify these.
				normal.x = (aBodyModel.theVertexesType1(vertexIndex).normalX / 255)
				normal.y = (aBodyModel.theVertexesType1(vertexIndex).normalY / 255)
				normal.z = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 255)

				' These are correct.
				texCoordU = (aBodyModel.theVertexesType1(vertexIndex).texCoordU / 255)
				texCoordV = (aBodyModel.theVertexesType1(vertexIndex).texCoordV / 255)
			ElseIf aBodyModel.vertexListType = 2 Then
				boneIndexes.Add(0)
				'position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 255) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x)
				'position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 255) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y)
				'position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z)
				'position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 127.5) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x)
				'position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 127.5) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y)
				'position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 127.5) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z)
				'position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0))
				'position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1))
				'position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2))
				'position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0))
				'position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1))
				'position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2))
				'position.x = ((aBodyModel.theVertexesType2(vertexIndex).positionX - 128) / 255) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x)
				'position.y = ((aBodyModel.theVertexesType2(vertexIndex).positionY - 128) / 255) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y)
				'position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z)
				' This makes candelabra really close to candelabra_candles.
				position.x = ((aBodyModel.theVertexesType2(vertexIndex).positionX - 128) / 255) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0))
				position.y = ((aBodyModel.theVertexesType2(vertexIndex).positionY - 128) / 255) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1))
				position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2))
				' Too big for candelabra.
				'position.x = ((aBodyModel.theVertexesType2(vertexIndex).positionX - 128) / 255) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0)
				'position.y = ((aBodyModel.theVertexesType2(vertexIndex).positionY - 128) / 255) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1)
				'position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2)
				' Too big for candelabra.
				'position.x = ((aBodyModel.theVertexesType2(vertexIndex).positionX - 128) / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0))
				'position.y = ((aBodyModel.theVertexesType2(vertexIndex).positionY - 128) / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1))
				'position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2))
				' Candelabra: Too big and upside-down and under candelabra_candles.
				'position.x = ((aBodyModel.theVertexesType2(vertexIndex).positionX - 128) / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0))
				'position.y = ((aBodyModel.theVertexesType2(vertexIndex).positionY - 128) / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1))
				'position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2))

				'TODO: Verify these.
				normal.x = (aBodyModel.theVertexesType2(vertexIndex).normalX / 255)
				normal.y = (aBodyModel.theVertexesType2(vertexIndex).normalY / 255)
				normal.z = (aBodyModel.theVertexesType2(vertexIndex).normalZ / 255)

				' These are correct.
				texCoordU = (aBodyModel.theVertexesType2(vertexIndex).texCoordU / 255)
				texCoordV = (aBodyModel.theVertexesType2(vertexIndex).texCoordV / 255)
			Else
				Dim debug As Integer = 4242
			End If

			'NOTE: Clamp the UV coords so they are always between 0 and 1.
			'      Example that has U > 1: "models\items\jadedragon\info\info_jadedragon.mdl"
			If texCoordU < 0 OrElse texCoordU > 1 Then
				texCoordU -= Math.Truncate(texCoordU)
			End If
			If texCoordV < 0 OrElse texCoordV > 1 Then
				texCoordV -= Math.Truncate(texCoordV)
			End If

			line = "  "
			line += boneIndexes(0).ToString(TheApp.InternalNumberFormat)

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
			line += texCoordU.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += (1 - texCoordV).ToString("0.000000", TheApp.InternalNumberFormat)

			If aBodyModel.vertexListType = 0 Then
				line += " "
				line += boneIndexes.Count.ToString(TheApp.InternalNumberFormat)
				For i As Integer = 0 To boneIndexes.Count - 1
					line += " "
					line += boneIndexes(i).ToString(TheApp.InternalNumberFormat)
					line += " "
					line += weights(i).ToString("0.000000", TheApp.InternalNumberFormat)
				Next
			End If
		Catch ex As Exception
			line = "// " + line
		End Try

		Return line
	End Function

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
	Private Sub CalcAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc2531, ByVal anAnimationDesc As SourceMdlAnimationDesc2531, ByVal frameIndex As Integer)
		Dim s As Double
		Dim aBone As SourceMdlBone2531
		Dim anAnimation As SourceMdlAnimation2531
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
	'Private Function CalcBoneRotation(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone2531, ByVal anAnimation As SourceMdlAnimation2531, ByRef rotationQuat As SourceQuaternion) As SourceVector
	'	'Dim rot As New SourceQuaternion()
	'	Dim angleVector As New SourceVector()

	'	If anAnimation.theOffsets(3) <= 0 Then
	'		angleVector.x = aBone.rotation.x
	'	Else
	'		angleVector.x = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationXValues, aBone.rotationScale.x, aBone.rotation.x)
	'	End If
	'	If anAnimation.theOffsets(4) <= 0 Then
	'		angleVector.y = aBone.rotation.y
	'	Else
	'		angleVector.y = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationYValues, aBone.rotationScale.y, aBone.rotation.y)
	'	End If
	'	If anAnimation.theOffsets(5) <= 0 Then
	'		angleVector.z = aBone.rotation.z
	'	Else
	'		angleVector.z = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationZValues, aBone.rotationScale.z, aBone.rotation.z)
	'	End If

	'	angleVector.debug_text = "anim"

	'	rotationQuat = MathModule.EulerAnglesToQuaternion(angleVector)
	'	Return angleVector
	'End Function
	Private Function CalcBoneRotation(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone2531, ByVal anAnimation As SourceMdlAnimation2531, ByRef rotationQuat As SourceQuaternion) As SourceVector
		Dim rot As New SourceQuaternion()
		Dim angleVector As New SourceVector()

		If (aBone.flags And SourceMdlBone2531.STUDIO_PROC_AXISINTERP) > 0 Then
			angleVector.x = 0
			angleVector.y = 0
			angleVector.z = 0

			angleVector.debug_text = "anim"
		Else
			If anAnimation.theOffsets(3) <= 0 OrElse (aBone.flags And SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0 Then
				rot.x = aBone.rotation.x
			Else
				'rot.x = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationXValues, aBone.rotationScale.x, aBone.rotation.x)
				rot.x = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationXValues, aBone.rotationScale.x, 0)
			End If
			If anAnimation.theOffsets(4) <= 0 OrElse (aBone.flags And SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0 Then
				rot.y = aBone.rotation.y
			Else
				'rot.y = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationYValues, aBone.rotationScale.y, aBone.rotation.y)
				rot.y = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationYValues, aBone.rotationScale.y, 0)
			End If
			If anAnimation.theOffsets(5) <= 0 OrElse (aBone.flags And SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0 Then
				rot.z = aBone.rotation.z
			Else
				'rot.z = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationZValues, aBone.rotationScale.z, aBone.rotation.z)
				rot.z = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationZValues, aBone.rotationScale.z, 0)
			End If
			If anAnimation.theOffsets(6) <= 0 OrElse (aBone.flags And SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0 Then
				rot.w = aBone.rotation.w
			Else
				'rot.w = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationWValues, aBone.rotationScale.w, aBone.rotation.w)
				rot.w = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationWValues, aBone.rotationScale.w, 0)
			End If

			angleVector = MathModule.ToEulerAngles2531(rot)
			angleVector.debug_text = "anim"
		End If

		rotationQuat = rot
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
	Private Function CalcBonePosition(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone2531, ByVal anAnimation As SourceMdlAnimation2531) As SourceVector
		Dim pos As New SourceVector()

		If anAnimation.theOffsets(0) <= 0 Then
			'pos.x = 0
			pos.x = aBone.position.x
		Else
			pos.x = Me.ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationXValues, aBone.positionScale.x, aBone.position.x)
		End If

		If anAnimation.theOffsets(1) <= 0 Then
			'pos.y = 0
			pos.y = aBone.position.y
		Else
			pos.y = Me.ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationYValues, aBone.positionScale.y, aBone.position.y)
		End If

		If anAnimation.theOffsets(2) <= 0 Then
			'pos.z = 0
			pos.z = aBone.position.z
		Else
			pos.z = Me.ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationZValues, aBone.positionScale.z, aBone.position.z)
		End If

		pos.debug_text = "anim"

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

	Private Function TransformPhyVertex(ByVal aBone As SourceMdlBone2531, ByVal vertex As SourceVector, ByVal aSourcePhysCollisionModel As SourcePhyPhysCollisionModel) As SourceVector
		'Private Function TransformPhyVertex(ByVal aBone As SourceMdlBone2531, ByVal vertex As SourceVector) As SourceVector
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
		'------
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

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData2531
	Private thePhyFileData As SourcePhyFileData

	Private theAnimationFrameLines As SortedList(Of Integer, AnimationFrameLine)

#End Region


End Class
