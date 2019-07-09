Imports System.IO
Imports System.Text

Public Class SourceVtxFile44

#Region "Creation and Destruction"

	Public Sub New(ByVal vtxFileReader As BinaryReader, ByVal vtxFileData As SourceVtxFileData44)
		Me.theInputFileReader = vtxFileReader
		Me.theVtxFileData = vtxFileData
	End Sub

#End Region

#Region "Methods"

	Public Sub ReadSourceVtxHeader()
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
	End Sub

	Public Sub ReadSourceVtxBodyParts()
		If Me.theVtxFileData.bodyPartCount > 0 Then
			'NOTE: Stuff that is part of determining vtx strip group size.
			Me.theFirstMeshWithStripGroups = Nothing
			Me.theFirstMeshWithStripGroupsInputFileStreamPosition = -1
			Me.theSecondMeshWithStripGroups = Nothing
			Me.theExpectedStartOfSecondStripGroupList = -1
			Me.theStripGroupUsesExtra8Bytes = False

			Dim bodyPartInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theVtxFileData.bodyPartOffset, SeekOrigin.Begin)
				Me.theVtxFileData.theVtxBodyParts = New List(Of SourceVtxBodyPart)(Me.theVtxFileData.bodyPartCount)
				For i As Integer = 0 To Me.theVtxFileData.bodyPartCount - 1
					bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBodyPart As New SourceVtxBodyPart()
					aBodyPart.modelCount = Me.theInputFileReader.ReadInt32()
					aBodyPart.modelOffset = Me.theInputFileReader.ReadInt32()
					Me.theVtxFileData.theVtxBodyParts.Add(aBodyPart)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aBodyPart.modelCount > 0 AndAlso aBodyPart.modelOffset <> 0 Then
						Me.ReadSourceVtxModels(bodyPartInputFileStreamPosition, aBodyPart)
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next
			Catch
			End Try
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub ReadSourceVtxModels(ByVal bodyPartInputFileStreamPosition As Long, ByVal aBodyPart As SourceVtxBodyPart)
		Dim modelInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin)
			aBodyPart.theVtxModels = New List(Of SourceVtxModel)(aBodyPart.modelCount)
			For j As Integer = 0 To aBodyPart.modelCount - 1
				modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aModel As New SourceVtxModel()
				aModel.lodCount = Me.theInputFileReader.ReadInt32()
				aModel.lodOffset = Me.theInputFileReader.ReadInt32()
				aBodyPart.theVtxModels.Add(aModel)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aModel.lodCount > 0 AndAlso aModel.lodOffset <> 0 Then
					Me.ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel)
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch
		End Try
	End Sub

	Private Sub ReadSourceVtxModelLods(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceVtxModel)
		Dim modelLodInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.lodOffset, SeekOrigin.Begin)
			aModel.theVtxModelLods = New List(Of SourceVtxModelLod)(aModel.lodCount)
			For j As Integer = 0 To aModel.lodCount - 1
				modelLodInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aModelLod As New SourceVtxModelLod()
				aModelLod.meshCount = Me.theInputFileReader.ReadInt32()
				aModelLod.meshOffset = Me.theInputFileReader.ReadInt32()
				aModelLod.switchPoint = Me.theInputFileReader.ReadSingle()
				aModel.theVtxModelLods.Add(aModelLod)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aModelLod.meshCount > 0 AndAlso aModelLod.meshOffset <> 0 Then
					Me.ReadSourceVtxMeshes(modelLodInputFileStreamPosition, aModelLod)
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch
		End Try
	End Sub

	Private Sub ReadSourceVtxMeshes(ByVal modelLodInputFileStreamPosition As Long, ByVal aModelLod As SourceVtxModelLod)
		Dim meshInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(modelLodInputFileStreamPosition + aModelLod.meshOffset, SeekOrigin.Begin)
			aModelLod.theVtxMeshes = New List(Of SourceVtxMesh)(aModelLod.meshCount)
			For j As Integer = 0 To aModelLod.meshCount - 1
				meshInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aMesh As New SourceVtxMesh()
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
						Me.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aMesh)
					ElseIf Me.theSecondMeshWithStripGroups Is Nothing Then
						Me.theSecondMeshWithStripGroups = aMesh
						If Me.theExpectedStartOfSecondStripGroupList <> (meshInputFileStreamPosition + aMesh.stripGroupOffset) Then
							Me.theStripGroupUsesExtra8Bytes = True

							If aMesh.theVtxStripGroups IsNot Nothing Then
								aMesh.theVtxStripGroups.Clear()
							End If

							Me.ReadSourceVtxStripGroups(Me.theFirstMeshWithStripGroupsInputFileStreamPosition, Me.theFirstMeshWithStripGroups)
						End If
						Me.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aMesh)
					Else
						Me.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aMesh)
					End If
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch
		End Try
	End Sub

	'TEST: / Save the first mesh that has strip groups and loop through the mesh's strip groups.
	'      / Get the file offset and store as Me.theExpectedStartOfSecondStripGroupList.
	'      / When the next strip group's offset is read in, compare with Me.theExpectedStartOfSecondStripGroupList.
	'      If equal, then read from first mesh with strip groups without further checking.
	'      Else (if unequal), then read from first mesh with strip groups 
	'          and continue reading remaining data using larger strip group size.
	'      WORKS for the SFM, Dota 2, and L4D2 models I tested.
	Private Sub AnalyzeVtxStripGroups(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceVtxMesh)
		Try
			Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin)
			aMesh.theVtxStripGroups = New List(Of SourceVtxStripGroup)(aMesh.stripGroupCount)
			For j As Integer = 0 To aMesh.stripGroupCount - 1
				Dim aStripGroup As New SourceVtxStripGroup()
				aStripGroup.vertexCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.vertexOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.indexCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.indexOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.stripCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.stripOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.flags = Me.theInputFileReader.ReadByte()
			Next

			Me.theExpectedStartOfSecondStripGroupList = Me.theInputFileReader.BaseStream.Position
		Catch
			'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
		End Try
	End Sub

	Private Sub ReadSourceVtxStripGroups(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceVtxMesh)
		Dim stripGroupInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin)
			aMesh.theVtxStripGroups = New List(Of SourceVtxStripGroup)(aMesh.stripGroupCount)
			For j As Integer = 0 To aMesh.stripGroupCount - 1
				stripGroupInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aStripGroup As New SourceVtxStripGroup()
				aStripGroup.vertexCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.vertexOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.indexCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.indexOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.stripCount = Me.theInputFileReader.ReadInt32()
				aStripGroup.stripOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.flags = Me.theInputFileReader.ReadByte()

				'TEST: Did not work for both Engineeer and doom.
				'If (aStripGroup.flags And SourceVtxStripGroup.SourceStripGroupDeltaFixed) > 0 Then
				'TEST: Works with models I tested from SFM, TF2 and Dota 2.
				'      Failed on models compiled for L4D2.
				If Me.theStripGroupUsesExtra8Bytes Then
					''TEST: Needed for SFM model, Barabus.
					'      Skip for TF2 Engineer and Heavy.
					Me.theInputFileReader.ReadInt32()
					Me.theInputFileReader.ReadInt32()
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

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch
			'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
		End Try
	End Sub

	Private Sub ReadSourceVtxVertexes(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup)
		'Dim modelInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.vertexOffset, SeekOrigin.Begin)
			aStripGroup.theVtxVertexes = New List(Of SourceVtxVertex)(aStripGroup.vertexCount)
			For j As Integer = 0 To aStripGroup.vertexCount - 1
				'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aVertex As New SourceVtxVertex()
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

				'If aModel.lodCount > 0 Then
				'	Me.ReadSourceVtxModelLods(modelInputFileStreamPosition, aVertex)
				'End If

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch
			'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
		End Try
	End Sub

	Private Sub ReadSourceVtxIndexes(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup)
		'Dim modelInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.indexOffset, SeekOrigin.Begin)
			aStripGroup.theVtxIndexes = New List(Of UShort)(aStripGroup.indexCount)
			For j As Integer = 0 To aStripGroup.indexCount - 1
				'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				aStripGroup.theVtxIndexes.Add(Me.theInputFileReader.ReadUInt16())

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'If aModel.lodCount > 0 Then
				'	Me.ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel)
				'End If

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch
		End Try
	End Sub

	Private Sub ReadSourceVtxStrips(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup)
		'Dim modelInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.stripOffset, SeekOrigin.Begin)
			aStripGroup.theVtxStrips = New List(Of SourceVtxStrip)(aStripGroup.stripCount)
			For j As Integer = 0 To aStripGroup.stripCount - 1
				'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aStrip As New SourceVtxStrip()
				aStrip.indexCount = Me.theInputFileReader.ReadInt32()
				aStrip.indexMeshIndex = Me.theInputFileReader.ReadInt32()
				aStrip.vertexCount = Me.theInputFileReader.ReadInt32()
				aStrip.vertexMeshIndex = Me.theInputFileReader.ReadInt32()
				aStrip.boneCount = Me.theInputFileReader.ReadInt16()
				aStrip.flags = Me.theInputFileReader.ReadByte()
				aStrip.boneStateChangeCount = Me.theInputFileReader.ReadInt32()
				aStrip.boneStateChangeOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.theVtxStrips.Add(aStrip)

				'	inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'	If aModel.lodCount > 0 Then
				'		Me.ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel)
				'	End If

				'	Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch
			'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
		End Try
	End Sub

#End Region

#Region "Data"

	Private theInputFileReader As BinaryReader
	Private theVtxFileData As SourceVtxFileData44

	Private theFirstMeshWithStripGroups As SourceVtxMesh
	Private theFirstMeshWithStripGroupsInputFileStreamPosition As Long
	Private theSecondMeshWithStripGroups As SourceVtxMesh
	Private theExpectedStartOfSecondStripGroupList As Long
	Private theStripGroupUsesExtra8Bytes As Boolean

#End Region

End Class
