Imports System.IO
Imports System.Text

Public Class SourceVtxFile06

#Region "Creation and Destruction"

	Public Sub New(ByVal vtxFileReader As BinaryReader, ByVal vtxFileData As SourceVtxFileData06)
		Me.theInputFileReader = vtxFileReader
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
		Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VTX File Header (Actual version: " + Me.theVtxFileData.version.ToString() + "; override version: 6)")
	End Sub

	Public Sub ReadSourceVtxBodyParts()
		If Me.theVtxFileData.bodyPartCount > 0 AndAlso Me.theVtxFileData.bodyPartOffset <> 0 Then
			Dim bodyPartInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theVtxFileData.bodyPartOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theVtxFileData.theVtxBodyParts = New List(Of SourceVtxBodyPart06)(Me.theVtxFileData.bodyPartCount)
				For i As Integer = 0 To Me.theVtxFileData.bodyPartCount - 1
					bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBodyPart As New SourceVtxBodyPart06()

					aBodyPart.modelCount = Me.theInputFileReader.ReadInt32()
					aBodyPart.modelOffset = Me.theInputFileReader.ReadInt32()

					Me.theVtxFileData.theVtxBodyParts.Add(aBodyPart)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadSourceVtxModels(bodyPartInputFileStreamPosition, aBodyPart)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

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
				Me.theInputFileReader.BaseStream.Seek(Me.theVtxFileData.materialReplacementListOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theVtxFileData.theVtxMaterialReplacementLists = New List(Of SourceVtxMaterialReplacementList06)(Me.theVtxFileData.bodyPartCount)
				'For i As Integer = 0 To Me.theVtxFileData.bodyPartCount - 1
				For i As Integer = 0 To Me.theVtxFileData.lodCount - 1
					materialReplacementListInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aMaterialReplacementList As New SourceVtxMaterialReplacementList06()

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

	Private Sub ReadSourceVtxModels(ByVal bodyPartInputFileStreamPosition As Long, ByVal aBodyPart As SourceVtxBodyPart06)
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

				aBodyPart.theVtxModels = New List(Of SourceVtxModel06)(aBodyPart.modelCount)
				For j As Integer = 0 To aBodyPart.modelCount - 1
					modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aModel As New SourceVtxModel06()

					aModel.lodCount = Me.theInputFileReader.ReadInt32()
					aModel.lodOffset = Me.theInputFileReader.ReadInt32()

					aBodyPart.theVtxModels.Add(aModel)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theVtxModels " + aBodyPart.theVtxModels.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSourceVtxModelLods(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceVtxModel06)
		If aModel.lodCount > 0 AndAlso aModel.lodOffset <> 0 Then
			Dim modelLodInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.lodOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theVtxModelLods = New List(Of SourceVtxModelLod06)(aModel.lodCount)
				For j As Integer = 0 To aModel.lodCount - 1
					modelLodInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aModelLod As New SourceVtxModelLod06()

					aModelLod.meshCount = Me.theInputFileReader.ReadInt32()
					aModelLod.meshOffset = Me.theInputFileReader.ReadInt32()
					aModelLod.switchPoint = Me.theInputFileReader.ReadSingle()
					aModelLod.theVtxModelLodUsesFacial = False

					aModel.theVtxModelLods.Add(aModelLod)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadSourceVtxMeshes(modelLodInputFileStreamPosition, aModelLod)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVtxModelLods " + aModel.theVtxModelLods.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSourceVtxMeshes(ByVal modelLodInputFileStreamPosition As Long, ByVal aModelLod As SourceVtxModelLod06)
		If aModelLod.meshCount > 0 AndAlso aModelLod.meshOffset <> 0 Then
			Dim meshInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelLodInputFileStreamPosition + aModelLod.meshOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModelLod.theVtxMeshes = New List(Of SourceVtxMesh06)(aModelLod.meshCount)
				For j As Integer = 0 To aModelLod.meshCount - 1
					meshInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aMesh As New SourceVtxMesh06()

					aMesh.stripGroupCount = Me.theInputFileReader.ReadInt32()
					aMesh.stripGroupOffset = Me.theInputFileReader.ReadInt32()
					aMesh.flags = Me.theInputFileReader.ReadByte()

					aModelLod.theVtxMeshes.Add(aMesh)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aModelLod, aMesh)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModelLod.theVtxMeshes " + aModelLod.theVtxMeshes.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSourceVtxStripGroups(ByVal meshInputFileStreamPosition As Long, ByVal aModelLod As SourceVtxModelLod06, ByVal aMesh As SourceVtxMesh06)
		If aMesh.stripGroupCount > 0 AndAlso aMesh.stripGroupOffset <> 0 Then
			Dim stripGroupInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aMesh.theVtxStripGroups = New List(Of SourceVtxStripGroup06)(aMesh.stripGroupCount)
				For j As Integer = 0 To aMesh.stripGroupCount - 1
					stripGroupInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aStripGroup As New SourceVtxStripGroup06()

					aStripGroup.vertexCount = Me.theInputFileReader.ReadInt32()
					aStripGroup.vertexOffset = Me.theInputFileReader.ReadInt32()
					aStripGroup.indexCount = Me.theInputFileReader.ReadInt32()
					aStripGroup.indexOffset = Me.theInputFileReader.ReadInt32()
					aStripGroup.stripCount = Me.theInputFileReader.ReadInt32()
					aStripGroup.stripOffset = Me.theInputFileReader.ReadInt32()
					aStripGroup.flags = Me.theInputFileReader.ReadByte()

					aMesh.theVtxStripGroups.Add(aStripGroup)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadSourceVtxVertexes(stripGroupInputFileStreamPosition, aStripGroup)
					Me.ReadSourceVtxIndexes(stripGroupInputFileStreamPosition, aStripGroup)
					Me.ReadSourceVtxStrips(stripGroupInputFileStreamPosition, aStripGroup)

					'TODO: Set whether stripgroup has flex vertexes in it or not for $lod facial and nofacial options.
					If (aStripGroup.flags And SourceVtxStripGroup06.STRIPGROUP_IS_FLEXED) > 0 Then
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
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSourceVtxVertexes(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup06)
		If aStripGroup.vertexCount > 0 AndAlso aStripGroup.vertexOffset <> 0 Then
			'Dim modelInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.vertexOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aStripGroup.theVtxVertexes = New List(Of SourceVtxVertex06)(aStripGroup.vertexCount)
				For j As Integer = 0 To aStripGroup.vertexCount - 1
					'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aVertex As New SourceVtxVertex06()

					For x As Integer = 0 To aVertex.boneWeightIndex.Length - 1
						aVertex.boneWeightIndex(x) = Me.theInputFileReader.ReadByte()
					Next
					For x As Integer = 0 To aVertex.boneIndex.Length - 1
						aVertex.boneIndex(x) = Me.theInputFileReader.ReadInt16()
					Next
					aVertex.originalMeshVertexIndex = Me.theInputFileReader.ReadInt16()
					aVertex.boneCount = Me.theInputFileReader.ReadByte()

					aStripGroup.theVtxVertexes.Add(aVertex)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxVertexes " + aStripGroup.theVtxVertexes.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSourceVtxIndexes(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup06)
		If aStripGroup.indexCount > 0 AndAlso aStripGroup.indexOffset <> 0 Then
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
		End If
	End Sub

	Private Sub ReadSourceVtxStrips(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup06)
		If aStripGroup.stripCount > 0 AndAlso aStripGroup.stripOffset <> 0 Then
			Dim stripInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.stripOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aStripGroup.theVtxStrips = New List(Of SourceVtxStrip06)(aStripGroup.stripCount)
				For j As Integer = 0 To aStripGroup.stripCount - 1
					stripInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aStrip As New SourceVtxStrip06()

					aStrip.indexCount = Me.theInputFileReader.ReadInt32()
					aStrip.indexMeshIndex = Me.theInputFileReader.ReadInt32()
					aStrip.vertexCount = Me.theInputFileReader.ReadInt32()
					aStrip.vertexMeshIndex = Me.theInputFileReader.ReadInt32()
					aStrip.boneCount = Me.theInputFileReader.ReadInt16()
					aStrip.flags = Me.theInputFileReader.ReadByte()
					aStrip.boneStateChangeCount = Me.theInputFileReader.ReadInt32()
					aStrip.boneStateChangeOffset = Me.theInputFileReader.ReadInt32()

					aStripGroup.theVtxStrips.Add(aStrip)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadSourceVtxBoneStateChanges(stripInputFileStreamPosition, aStrip)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxStrips " + aStripGroup.theVtxStrips.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSourceVtxBoneStateChanges(ByVal stripInputFileStreamPosition As Long, ByVal aStrip As SourceVtxStrip06)
		'TEST: 
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

				'aStrip.theVtxBoneStateChanges = New List(Of SourceVtxBoneStateChange06)(aStrip.boneStateChangeCount)
				aStrip.theVtxBoneStateChanges = New List(Of SourceVtxBoneStateChange06)(boneStateChangeCount)
				'For j As Integer = 0 To aStrip.boneStateChangeCount - 1
				For j As Integer = 0 To boneStateChangeCount - 1
					'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aBoneStateChange As New SourceVtxBoneStateChange06()

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

	Private Sub ReadSourceVtxMaterialReplacements(ByVal materialReplacementListInputFileStreamPosition As Long, ByVal aMaterialReplacementList As SourceVtxMaterialReplacementList06)
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

				aMaterialReplacementList.theVtxMaterialReplacements = New List(Of SourceVtxMaterialReplacement06)(aMaterialReplacementList.replacementCount)
				For j As Integer = 0 To aMaterialReplacementList.replacementCount - 1
					materialReplacementInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aMaterialReplacement As New SourceVtxMaterialReplacement06()

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
	Private theVtxFileData As SourceVtxFileData06

#End Region

End Class
