Imports System.IO
Imports System.Text

Public Class SourceVtxFile07

#Region "Creation and Destruction"

	Public Sub New(ByVal vtxFileReader As BinaryReader, ByVal vtxFileData As SourceVtxFileData07, Optional ByVal vtxFileOffsetStart As Long = 0)
		Me.theInputFileReader = vtxFileReader
		Me.theVtxFileOffsetStart = vtxFileOffsetStart
		Me.theVtxFileData = vtxFileData

		Me.theVtxFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length
	End Sub

#End Region

#Region "Methods"

	Public Sub ReadSourceVtxHeader()
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		' Offset: 0x00
		Me.theVtxFileData.version = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x04, 0x08, 0x0A (10), 0x0C (12)
		Me.theVtxFileData.vertexCacheSize = Me.theInputFileReader.ReadInt32()
		Me.theVtxFileData.maxBonesPerStrip = Me.theInputFileReader.ReadUInt16()
		Me.theVtxFileData.maxBonesPerTri = Me.theInputFileReader.ReadUInt16()
		Me.theVtxFileData.maxBonesPerVertex = Me.theInputFileReader.ReadInt32()

		' Offset: 0x10 (16)
		Me.theVtxFileData.checksum = Me.theInputFileReader.ReadInt32()

		' Offset: 0x14 (20)
		Me.theVtxFileData.lodCount = Me.theInputFileReader.ReadInt32()

		' Offset: 0x18 (24)
		Me.theVtxFileData.materialReplacementListOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x1C (28), 0x20 (32)
		Me.theVtxFileData.bodyPartCount = Me.theInputFileReader.ReadInt32()
		Me.theVtxFileData.bodyPartOffset = Me.theInputFileReader.ReadInt32()

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VTX File Header (Actual version: " + Me.theVtxFileData.version.ToString() + "; override version: 7)")
	End Sub

	Public Sub ReadSourceVtxBodyParts()
		If Me.theVtxFileData.bodyPartCount > 0 Then
			'NOTE: Stuff that is part of determining vtx strip group size.
			Me.theFirstMeshWithStripGroups = Nothing
			Me.theFirstMeshWithStripGroupsInputFileStreamPosition = -1
			Me.theModelLodHasOnlyOneMesh = False
			Me.theSecondMeshWithStripGroups = Nothing
			Me.theExpectedStartOfDataAfterFirstStripGroupList = -1
			Me.theStripGroupAndStripUseExtraFields = False

			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theVtxFileOffsetStart + Me.theVtxFileData.bodyPartOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.ReadSourceVtxBodyParts_Internal()

				'TODO: If there is no second mesh with strip groups, then check if calculated offset matches actual offset.
				'      If does not match, then read again with larger sizes.
				If Me.theSecondMeshWithStripGroups Is Nothing Then
					If Me.theStartOfStripGroupVtxStrips <> -1 AndAlso Me.theExpectedStartOfDataAfterFirstStripGroupList <> Me.theStartOfStripGroupVtxStrips Then
						Me.theStripGroupAndStripUseExtraFields = True
						Me.theInputFileReader.BaseStream.Seek(Me.theVtxFileOffsetStart + Me.theVtxFileData.bodyPartOffset, SeekOrigin.Begin)
						Me.ReadSourceVtxBodyParts_Internal()
					End If
				End If

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theVtxFileData.theVtxBodyParts " + Me.theVtxFileData.theVtxBodyParts.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadSourceVtxMaterialReplacementLists()
		If Me.theVtxFileData.materialReplacementListOffset <> 0 Then
			Dim materialReplacementListInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theVtxFileOffsetStart + Me.theVtxFileData.materialReplacementListOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theVtxFileData.theVtxMaterialReplacementLists = New List(Of SourceVtxMaterialReplacementList07)(Me.theVtxFileData.lodCount)
				For i As Integer = 0 To Me.theVtxFileData.lodCount - 1
					materialReplacementListInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aMaterialReplacementList As New SourceVtxMaterialReplacementList07()

					aMaterialReplacementList.replacementCount = Me.theInputFileReader.ReadInt32()
					aMaterialReplacementList.replacementOffset = Me.theInputFileReader.ReadInt32()

					Me.theVtxFileData.theVtxMaterialReplacementLists.Add(aMaterialReplacementList)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadSourceVtxMaterialReplacements(materialReplacementListInputFileStreamPosition, aMaterialReplacementList)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theVtxFileData.theVtxMaterialReplacementLists " + Me.theVtxFileData.theVtxMaterialReplacementLists.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadUnreadBytes()
		Me.theVtxFileData.theFileSeekLog.LogUnreadBytes(Me.theInputFileReader)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub ReadSourceVtxBodyParts_Internal()
		Dim bodyPartInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long

		Try
			Me.theVtxFileData.theVtxBodyParts = New List(Of SourceVtxBodyPart07)(Me.theVtxFileData.bodyPartCount)
			For i As Integer = 0 To Me.theVtxFileData.bodyPartCount - 1
				bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aBodyPart As New SourceVtxBodyPart07()

				aBodyPart.modelCount = Me.theInputFileReader.ReadInt32()
				aBodyPart.modelOffset = Me.theInputFileReader.ReadInt32()

				Me.theVtxFileData.theVtxBodyParts.Add(aBodyPart)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				Me.ReadSourceVtxModels(bodyPartInputFileStreamPosition, aBodyPart)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxModels(ByVal bodyPartInputFileStreamPosition As Long, ByVal aBodyPart As SourceVtxBodyPart07)
		If aBodyPart.modelCount > 0 AndAlso aBodyPart.modelOffset <> 0 Then
			Dim modelInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aBodyPart.theVtxModels = New List(Of SourceVtxModel07)(aBodyPart.modelCount)
				For j As Integer = 0 To aBodyPart.modelCount - 1
					modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aModel As New SourceVtxModel07()

					aModel.lodCount = Me.theInputFileReader.ReadInt32()
					aModel.lodOffset = Me.theInputFileReader.ReadInt32()

					aBodyPart.theVtxModels.Add(aModel)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aModel.lodCount > 0 AndAlso aModel.lodOffset <> 0 Then
						Me.ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel)
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theVtxModels " + aBodyPart.theVtxModels.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSourceVtxModelLods(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceVtxModel07)
		Dim modelLodInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.lodOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aModel.theVtxModelLods = New List(Of SourceVtxModelLod07)(aModel.lodCount)
			For j As Integer = 0 To aModel.lodCount - 1
				modelLodInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aModelLod As New SourceVtxModelLod07()

				aModelLod.meshCount = Me.theInputFileReader.ReadInt32()
				aModelLod.meshOffset = Me.theInputFileReader.ReadInt32()
				aModelLod.switchPoint = Me.theInputFileReader.ReadSingle()
				aModelLod.theVtxModelLodUsesFacial = False

				aModel.theVtxModelLods.Add(aModelLod)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aModelLod.meshCount > 0 AndAlso aModelLod.meshOffset <> 0 Then
					Me.ReadSourceVtxMeshes(modelLodInputFileStreamPosition, aModelLod)
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVtxModelLods " + aModel.theVtxModelLods.Count.ToString())
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxMeshes(ByVal modelLodInputFileStreamPosition As Long, ByVal aModelLod As SourceVtxModelLod07)
		Dim meshInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(modelLodInputFileStreamPosition + aModelLod.meshOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aModelLod.theVtxMeshes = New List(Of SourceVtxMesh07)(aModelLod.meshCount)
			For j As Integer = 0 To aModelLod.meshCount - 1
				meshInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aMesh As New SourceVtxMesh07()

				aMesh.stripGroupCount = Me.theInputFileReader.ReadInt32()
				aMesh.stripGroupOffset = Me.theInputFileReader.ReadInt32()
				aMesh.flags = Me.theInputFileReader.ReadByte()

				aModelLod.theVtxMeshes.Add(aMesh)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aMesh.stripGroupCount > 0 AndAlso aMesh.stripGroupOffset <> 0 Then
					If Me.theFirstMeshWithStripGroups Is Nothing Then
						Me.theFirstMeshWithStripGroups = aMesh
						Me.theFirstMeshWithStripGroupsInputFileStreamPosition = meshInputFileStreamPosition
						Me.AnalyzeVtxStripGroups(meshInputFileStreamPosition, aMesh)

						''NOTE: If there is only one mesh, then compare end offset of VtxStripGroups with start of VtxStrips.
						'If aModelLod.meshCount = 1 Then
						'	If aMesh.theVtxStripGroups(0).stripCount > 0 AndAlso aMesh.theVtxStripGroups(0).stripOffset <> 0 Then
						'		If Me.theExpectedStartOfDataAfterFirstStripGroupList <> (meshInputFileStreamPosition + aMesh.stripGroupOffset + aMesh.theVtxStripGroups(0).stripOffset) Then
						'			Me.theStripGroupAndStripUseExtraFields = True
						'		End If
						'	End If
						'End If
					ElseIf Me.theSecondMeshWithStripGroups Is Nothing Then
						Me.theSecondMeshWithStripGroups = aMesh
						If Me.theExpectedStartOfDataAfterFirstStripGroupList <> (meshInputFileStreamPosition + aMesh.stripGroupOffset) Then
							Me.theStripGroupAndStripUseExtraFields = True

							'If aMesh.theVtxStripGroups IsNot Nothing Then
							'	aMesh.theVtxStripGroups.Clear()
							'End If
							Me.theVtxFileData.theFileSeekLog.Remove(Me.theFirstMeshWithStripGroupsInputFileStreamPosition + Me.theFirstMeshWithStripGroups.stripGroupOffset)
							Me.theVtxFileData.theFileSeekLog.Remove(Me.theFirstMeshWithStripGroupsInputFileStreamPosition + Me.theFirstMeshWithStripGroups.stripGroupOffset + Me.theFirstMeshWithStripGroups.theVtxStripGroups(0).stripOffset)

							Me.ReadSourceVtxStripGroups(Me.theFirstMeshWithStripGroupsInputFileStreamPosition, aModelLod, Me.theFirstMeshWithStripGroups)
						End If
					End If

					Me.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aModelLod, aMesh)
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModelLod.theVtxMeshes " + aModelLod.theVtxMeshes.Count.ToString())
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	'TEST: / Save the first mesh that has strip groups and loop through the mesh's strip groups.
	'      / Get the file offset and store as Me.theExpectedStartOfSecondStripGroupList.
	'      / When the next strip group's offset is read in, compare with Me.theExpectedStartOfSecondStripGroupList.
	'      If equal, then read from first mesh with strip groups without further checking.
	'      Else (if unequal), then read from first mesh with strip groups 
	'          and continue reading remaining data using larger strip group size.
	'      WORKS for the SFM, Dota 2, and L4D2 models I tested.
	Private Sub AnalyzeVtxStripGroups(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceVtxMesh07)
		Try
			Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin)
			aMesh.theVtxStripGroups = New List(Of SourceVtxStripGroup07)(aMesh.stripGroupCount)
			For j As Integer = 0 To aMesh.stripGroupCount - 1
				Dim aStripGroup As New SourceVtxStripGroup07()
				aStripGroup.vertexCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.vertexOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.indexCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.indexOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.stripCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.stripOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.flags = Me.theInputFileReader.ReadByte()

				aMesh.theVtxStripGroups.Add(aStripGroup)
			Next

			Me.theExpectedStartOfDataAfterFirstStripGroupList = Me.theInputFileReader.BaseStream.Position
		Catch ex As Exception
			'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxStripGroups(ByVal meshInputFileStreamPosition As Long, ByVal aModelLod As SourceVtxModelLod07, ByVal aMesh As SourceVtxMesh07)
		Dim stripGroupInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aMesh.theVtxStripGroups = New List(Of SourceVtxStripGroup07)(aMesh.stripGroupCount)
			For j As Integer = 0 To aMesh.stripGroupCount - 1
				stripGroupInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aStripGroup As New SourceVtxStripGroup07()

				aStripGroup.vertexCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.vertexOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.indexCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.indexOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.stripCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.stripOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.flags = Me.theInputFileReader.ReadByte()

				''TEST: Did not work for both Engineeer and doom.
				''If (aStripGroup.flags And SourceVtxStripGroup.SourceStripGroupDeltaFixed) > 0 Then
				''TEST: Works with models I tested from SFM, TF2 and Dota 2.
				''      Failed on models compiled for L4D2.
				'If Me.theStripGroupUsesExtra8Bytes Then
				'	''TEST: Needed for SFM model, Barabus.
				'	'      Skip for TF2 Engineer and Heavy.
				'	Me.theInputFileReader.ReadInt32()
				'	Me.theInputFileReader.ReadInt32()
				'End If
				'TEST:
				If Me.theStripGroupAndStripUseExtraFields Then
					aStripGroup.topologyIndexCount = Me.theInputFileReader.ReadInt32()
					aStripGroup.topologyIndexOffset = Me.theInputFileReader.ReadInt32()
				End If

				aMesh.theVtxStripGroups.Add(aStripGroup)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aStripGroup.vertexCount > 0 AndAlso aStripGroup.vertexOffset <> 0 Then
					Me.ReadSourceVtxVertexes(stripGroupInputFileStreamPosition, aStripGroup)
					'TEST: Did not correct the missing SFM HWM Sniper left arm mesh.
					'ElseIf j > 0 Then
					'	aStripGroup.vertexCount = aMesh.theVtxStripGroups(j - 1).vertexCount
					'	Me.ReadSourceVtxVertexes(stripGroupInputFileStreamPosition, aStripGroup)
					'TEST: Did not correct the missing SFM HWM Sniper left arm mesh.
					'ElseIf j > 0 Then
					'	aStripGroup.theVtxVertexes = aMesh.theVtxStripGroups(j - 1).theVtxVertexes
				End If
				If aStripGroup.indexCount > 0 AndAlso aStripGroup.indexOffset <> 0 Then
					Me.ReadSourceVtxIndexes(stripGroupInputFileStreamPosition, aStripGroup)
				End If
				If aStripGroup.stripCount > 0 AndAlso aStripGroup.stripOffset <> 0 Then
					Me.ReadSourceVtxStrips(stripGroupInputFileStreamPosition, aStripGroup)
				End If
				'TEST:
				If Me.theStripGroupAndStripUseExtraFields Then
					If aStripGroup.topologyIndexCount > 0 AndAlso aStripGroup.topologyIndexOffset <> 0 Then
						Me.ReadSourceVtxTopologyIndexes(stripGroupInputFileStreamPosition, aStripGroup)
					End If
				End If

				' Set whether stripgroup has flex vertexes in it or not for $lod facial and nofacial options.
				If (aStripGroup.flags And SourceVtxStripGroup07.SourceStripGroupFlexed) > 0 OrElse (aStripGroup.flags And SourceVtxStripGroup07.SourceStripGroupDeltaFixed) > 0 Then
					aModelLod.theVtxModelLodUsesFacial = True
					'------
					'Dim aVtxVertex As SourceVtxVertex
					'For Each aVtxVertexIndex As UShort In aStripGroup.theVtxIndexes
					'	aVtxVertex = aStripGroup.theVtxVertexes(aVtxVertexIndex)

					'	' for (i = 0; i < pStudioMesh->numflexes; i++)
					'	' for (j = 0; j < pflex[i].numverts; j++)
					'	'The meshflexes are found in the MDL file > bodypart > model > mesh.theFlexes
					'	For Each meshFlex As SourceMdlFlex In meshflexes

					'	Next
					'Next
					''Dim debug As Integer = 4242
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theVtxStripGroups " + aMesh.theVtxStripGroups.Count.ToString())
		Catch ex As Exception
			'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxVertexes(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup07)
		'Dim modelInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.vertexOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aStripGroup.theVtxVertexes = New List(Of SourceVtxVertex07)(aStripGroup.vertexCount)
			For j As Integer = 0 To aStripGroup.vertexCount - 1
				'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aVertex As New SourceVtxVertex07()

				For i As Integer = 0 To MAX_NUM_BONES_PER_VERT - 1
					aVertex.boneWeightIndex(i) = Me.theInputFileReader.ReadByte()
				Next
				aVertex.boneCount = Me.theInputFileReader.ReadByte()
				aVertex.originalMeshVertexIndex = Me.theInputFileReader.ReadUInt16()
				For i As Integer = 0 To MAX_NUM_BONES_PER_VERT - 1
					aVertex.boneId(i) = Me.theInputFileReader.ReadByte()
				Next

				aStripGroup.theVtxVertexes.Add(aVertex)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxVertexes " + aStripGroup.theVtxVertexes.Count.ToString())
		Catch ex As Exception
			'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxIndexes(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup07)
		'Dim modelInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.indexOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aStripGroup.theVtxIndexes = New List(Of UShort)(aStripGroup.indexCount)
			For j As Integer = 0 To aStripGroup.indexCount - 1
				'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				aStripGroup.theVtxIndexes.Add(Me.theInputFileReader.ReadUInt16())

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxIndexes " + aStripGroup.theVtxIndexes.Count.ToString())
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxStrips(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup07)
		Dim stripInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			If Me.theStartOfStripGroupVtxStrips = -1 Then
				Me.theStartOfStripGroupVtxStrips = stripGroupInputFileStreamPosition + aStripGroup.stripOffset
			End If

			Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.stripOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aStripGroup.theVtxStrips = New List(Of SourceVtxStrip07)(aStripGroup.stripCount)
			For j As Integer = 0 To aStripGroup.stripCount - 1
				stripInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aStrip As New SourceVtxStrip07()

				aStrip.indexCount = Me.theInputFileReader.ReadInt32()
				aStrip.indexMeshIndex = Me.theInputFileReader.ReadInt32()
				aStrip.vertexCount = Me.theInputFileReader.ReadInt32()
				aStrip.vertexMeshIndex = Me.theInputFileReader.ReadInt32()
				aStrip.boneCount = Me.theInputFileReader.ReadInt16()
				aStrip.flags = Me.theInputFileReader.ReadByte()
				aStrip.boneStateChangeCount = Me.theInputFileReader.ReadInt32()
				aStrip.boneStateChangeOffset = Me.theInputFileReader.ReadInt32()
				'TEST:
				If Me.theStripGroupAndStripUseExtraFields Then
					aStrip.unknownBytes01 = Me.theInputFileReader.ReadInt32()
					aStrip.unknownBytes02 = Me.theInputFileReader.ReadInt32()
				End If

				aStripGroup.theVtxStrips.Add(aStrip)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'TODO: Commented-out due to using a ton of memory and long time to read incorrectly the model 
				'      from this bug report: 2019-03-08 bad decompile\alyx_sfm
				'Me.ReadSourceVtxBoneStateChanges(stripInputFileStreamPosition, aStrip)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxStrips " + aStripGroup.theVtxStrips.Count.ToString())
		Catch ex As Exception
			'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxTopologyIndexes(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup07)
		'Dim topologyInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.topologyIndexOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aStripGroup.theVtxTopologyIndexes = New List(Of UShort)(aStripGroup.topologyIndexCount)
			For j As Integer = 0 To aStripGroup.topologyIndexCount - 1
				'topologyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim topologyIndex As UShort

				topologyIndex = Me.theInputFileReader.ReadUInt16()

				aStripGroup.theVtxTopologyIndexes.Add(topologyIndex)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxTopologies " + aStripGroup.theVtxTopologyIndexes.Count.ToString())
		Catch ex As Exception
			'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxBoneStateChanges(ByVal stripInputFileStreamPosition As Long, ByVal aStrip As SourceVtxStrip07)
		'TODO: On an alyx.mdl from SFM, aStrip.boneStateChangeCount is over 800,000, so maybe when astrip.flag has bit flag 4 set, skip this reading.
		If ((aStrip.flags And 1) = 0) OrElse ((aStrip.flags And 4) > 0) Then
			Exit Sub
		End If

		'NOTE: It seems that if boneCount = 0 then a SourceVtxBoneStateChange is stored.
		Dim boneStateChangeCount As Integer
		boneStateChangeCount = aStrip.boneStateChangeCount
		If aStrip.boneCount = 0 AndAlso aStrip.boneStateChangeOffset <> 0 Then
			boneStateChangeCount = 1
		End If

		If boneStateChangeCount > 0 AndAlso aStrip.boneStateChangeOffset <> 0 Then
			'Dim modelInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(stripInputFileStreamPosition + aStrip.boneStateChangeOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				'aStrip.theVtxBoneStateChanges = New List(Of SourceVtxBoneStateChange)(aStrip.boneStateChangeCount)
				aStrip.theVtxBoneStateChanges = New List(Of SourceVtxBoneStateChange07)(boneStateChangeCount)
				For j As Integer = 0 To boneStateChangeCount - 1
					'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aBoneStateChange As New SourceVtxBoneStateChange07()

					aBoneStateChange.hardwareId = Me.theInputFileReader.ReadInt32()
					aBoneStateChange.newBoneId = Me.theInputFileReader.ReadInt32()

					aStrip.theVtxBoneStateChanges.Add(aBoneStateChange)

					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStrip")

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStrip.theVtxBoneStateChanges " + aStrip.theVtxBoneStateChanges.Count.ToString())
			Catch ex As Exception
				'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSourceVtxMaterialReplacements(ByVal materialReplacementListInputFileStreamPosition As Long, ByVal aMaterialReplacementList As SourceVtxMaterialReplacementList07)
		If aMaterialReplacementList.replacementCount > 0 AndAlso aMaterialReplacementList.replacementOffset <> 0 Then
			Dim materialReplacementInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(materialReplacementListInputFileStreamPosition + aMaterialReplacementList.replacementOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aMaterialReplacementList.theVtxMaterialReplacements = New List(Of SourceVtxMaterialReplacement07)(aMaterialReplacementList.replacementCount)
				For j As Integer = 0 To aMaterialReplacementList.replacementCount - 1
					materialReplacementInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aMaterialReplacement As New SourceVtxMaterialReplacement07()

					aMaterialReplacement.materialIndex = Me.theInputFileReader.ReadInt16()
					aMaterialReplacement.nameOffset = Me.theInputFileReader.ReadInt32()

					aMaterialReplacementList.theVtxMaterialReplacements.Add(aMaterialReplacement)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aMaterialReplacement.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(materialReplacementInputFileStreamPosition + aMaterialReplacement.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aMaterialReplacement.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theVtxFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aMaterialReplacement.theName = " + aMaterialReplacement.theName)
						End If
					ElseIf aMaterialReplacement.theName Is Nothing Then
						aMaterialReplacement.theName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMaterialReplacementList.theVtxMaterialReplacements " + aMaterialReplacementList.theVtxMaterialReplacements.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

#End Region

#Region "Data"

	Private theInputFileReader As BinaryReader
	Private theVtxFileOffsetStart As Long
	Private theVtxFileData As SourceVtxFileData07

	Private theFirstMeshWithStripGroups As SourceVtxMesh07
	Private theFirstMeshWithStripGroupsInputFileStreamPosition As Long
	Private theModelLodHasOnlyOneMesh As Boolean
	Private theSecondMeshWithStripGroups As SourceVtxMesh07
	Private theExpectedStartOfDataAfterFirstStripGroupList As Long
	Private theStartOfStripGroupVtxStrips As Long
	Private theStripGroupAndStripUseExtraFields As Boolean

#End Region

End Class
