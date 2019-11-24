Imports System.IO

Public Class SourceVtxFile107

#Region "Creation and Destruction"

	Public Sub New(ByVal vtxFileReader As BinaryReader, ByVal vtxFileData As SourceVtxFileData107)
		Me.theInputFileReader = vtxFileReader
		Me.theVtxFileData = vtxFileData

		Me.theVtxFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length
	End Sub

#End Region

#Region "Methods"

	Public Sub ReadSourceVtxHeader()
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theVtxFileData.version = Me.theInputFileReader.ReadInt32()

		Me.theVtxFileData.vertexCacheSize = Me.theInputFileReader.ReadInt32()
		Me.theVtxFileData.maxBonesPerStrip = Me.theInputFileReader.ReadUInt16()
		Me.theVtxFileData.maxBonesPerTri = Me.theInputFileReader.ReadUInt16()
		Me.theVtxFileData.maxBonesPerVertex = Me.theInputFileReader.ReadInt32()

		Me.theVtxFileData.checksum = Me.theInputFileReader.ReadInt32()

		Me.theVtxFileData.lodCount = Me.theInputFileReader.ReadInt32()

		Me.theVtxFileData.materialReplacementListOffset = Me.theInputFileReader.ReadInt32()

		Me.theVtxFileData.bodyPartCount = Me.theInputFileReader.ReadInt32()
		Me.theVtxFileData.bodyPartOffset = Me.theInputFileReader.ReadInt32()

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VTX File Header (Actual version: " + Me.theVtxFileData.version.ToString() + "; override version: 107)")
	End Sub

	Public Sub ReadSourceVtxBodyParts()
		If Me.theVtxFileData.bodyPartCount > 0 Then
			''NOTE: Stuff that is part of determining vtx strip group size.
			'Me.theFirstMeshWithStripGroups = Nothing
			'Me.theFirstMeshWithStripGroupsInputFileStreamPosition = -1
			'Me.theSecondMeshWithStripGroups = Nothing
			'Me.theExpectedStartOfSecondStripGroupList = -1
			'Me.theStripGroupUsesExtra8Bytes = False

			Dim bodyPartInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theVtxFileData.bodyPartOffset, SeekOrigin.Begin)

				Me.theVtxFileData.theVtxBodyParts = New List(Of SourceVtxBodyPart107)(Me.theVtxFileData.bodyPartCount)
				For i As Integer = 0 To Me.theVtxFileData.bodyPartCount - 1
					bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aBodyPart As New SourceVtxBodyPart107()

					aBodyPart.modelCount = Me.theInputFileReader.ReadInt32()
					aBodyPart.modelOffset = Me.theInputFileReader.ReadInt32()

					Me.theVtxFileData.theVtxBodyParts.Add(aBodyPart)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadSourceVtxModels(bodyPartInputFileStreamPosition, aBodyPart)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next
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

	Private Sub ReadSourceVtxModels(ByVal bodyPartInputFileStreamPosition As Long, ByVal aBodyPart As SourceVtxBodyPart107)
		If aBodyPart.modelCount > 0 AndAlso aBodyPart.modelOffset <> 0 Then
			Dim modelInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin)

				aBodyPart.theVtxModels = New List(Of SourceVtxModel107)(aBodyPart.modelCount)
				For j As Integer = 0 To aBodyPart.modelCount - 1
					modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aModel As New SourceVtxModel107()

					aModel.lodCount = Me.theInputFileReader.ReadInt32()
					aModel.lodOffset = Me.theInputFileReader.ReadInt32()

					aBodyPart.theVtxModels.Add(aModel)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSourceVtxModelLods(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceVtxModel107)
		If aModel.lodCount > 0 AndAlso aModel.lodOffset <> 0 Then
			Dim modelLodInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.lodOffset, SeekOrigin.Begin)

				aModel.theVtxModelLods = New List(Of SourceVtxModelLod107)(aModel.lodCount)
				For j As Integer = 0 To aModel.lodCount - 1
					modelLodInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aModelLod As New SourceVtxModelLod107()

					aModelLod.meshCount = Me.theInputFileReader.ReadInt32()
					aModelLod.meshOffset = Me.theInputFileReader.ReadInt32()
					aModelLod.switchPoint = Me.theInputFileReader.ReadSingle()

					aModel.theVtxModelLods.Add(aModelLod)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModelLod")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadSourceVtxMeshes(modelLodInputFileStreamPosition, aModelLod)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSourceVtxMeshes(ByVal modelLodInputFileStreamPosition As Long, ByVal aModelLod As SourceVtxModelLod107)
		If aModelLod.meshCount > 0 AndAlso aModelLod.meshOffset <> 0 Then
			Dim meshInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelLodInputFileStreamPosition + aModelLod.meshOffset, SeekOrigin.Begin)

				aModelLod.theVtxMeshes = New List(Of SourceVtxMesh107)(aModelLod.meshCount)
				For j As Integer = 0 To aModelLod.meshCount - 1
					meshInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aMesh As New SourceVtxMesh107()

					'aMesh.stripGroupCount = Me.theInputFileReader.ReadInt32()
					'aMesh.stripGroupOffset = Me.theInputFileReader.ReadInt32()
					''aMesh.flags = Me.theInputFileReader.ReadByte()
					'------
					aMesh.stripGroupCount = Me.theInputFileReader.ReadInt16()
					aMesh.flags = Me.theInputFileReader.ReadByte()
					aMesh.unknown = Me.theInputFileReader.ReadByte()
					aMesh.stripGroupOffset = Me.theInputFileReader.ReadInt32()

					aModelLod.theVtxMeshes.Add(aMesh)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aMesh.stripGroupCount > 0 AndAlso aMesh.stripGroupOffset <> 0 Then
						'If Me.theFirstMeshWithStripGroups Is Nothing Then
						'	Me.theFirstMeshWithStripGroups = aMesh
						'	Me.theFirstMeshWithStripGroupsInputFileStreamPosition = meshInputFileStreamPosition
						'	Me.AnalyzeVtxStripGroups(meshInputFileStreamPosition, aMesh)
						'	Me.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aMesh)
						'ElseIf Me.theSecondMeshWithStripGroups Is Nothing Then
						'	Me.theSecondMeshWithStripGroups = aMesh
						'	If Me.theExpectedStartOfSecondStripGroupList <> (meshInputFileStreamPosition + aMesh.stripGroupOffset) Then
						'		Me.theStripGroupUsesExtra8Bytes = True

						'		If aMesh.theVtxStripGroups IsNot Nothing Then
						'			aMesh.theVtxStripGroups.Clear()
						'		End If

						'		Me.ReadSourceVtxStripGroups(Me.theFirstMeshWithStripGroupsInputFileStreamPosition, Me.theFirstMeshWithStripGroups)
						'	End If
						'	Me.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aMesh)
						'Else
						Me.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aMesh)
						'End If
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	''TEST: / Save the first mesh that has strip groups and loop through the mesh's strip groups.
	''      / Get the file offset and store as Me.theExpectedStartOfSecondStripGroupList.
	''      / When the next strip group's offset is read in, compare with Me.theExpectedStartOfSecondStripGroupList.
	''      If equal, then read from first mesh with strip groups without further checking.
	''      Else (if unequal), then read from first mesh with strip groups 
	''          and continue reading remaining data using larger strip group size.
	''      WORKS for the SFM, Dota 2, and L4D2 models I tested.
	'Private Sub AnalyzeVtxStripGroups(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceVtxMesh107)
	'	Try
	'		Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin)
	'		aMesh.theVtxStripGroups = New List(Of SourceVtxStripGroup107)(aMesh.stripGroupCount)
	'		For j As Integer = 0 To aMesh.stripGroupCount - 1
	'			Dim aStripGroup As New SourceVtxStripGroup107()
	'			aStripGroup.vertexCount = Me.theInputFileReader.ReadInt32()
	'			aStripGroup.vertexOffset = Me.theInputFileReader.ReadInt32()
	'			aStripGroup.indexCount = Me.theInputFileReader.ReadInt32()
	'			aStripGroup.indexOffset = Me.theInputFileReader.ReadInt32()
	'			aStripGroup.stripCount = Me.theInputFileReader.ReadInt32()
	'			aStripGroup.stripOffset = Me.theInputFileReader.ReadInt32()
	'			aStripGroup.flags = Me.theInputFileReader.ReadByte()
	'		Next

	'		Me.theExpectedStartOfSecondStripGroupList = Me.theInputFileReader.BaseStream.Position
	'	Catch ex As Exception
	'		'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
	'		Dim debug As Integer = 4242
	'	End Try
	'End Sub

	Private Sub ReadSourceVtxStripGroups(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceVtxMesh107)
		Dim stripGroupInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin)

			aMesh.theVtxStripGroups = New List(Of SourceVtxStripGroup107)(aMesh.stripGroupCount)
			For j As Integer = 0 To aMesh.stripGroupCount - 1
				stripGroupInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				Dim aStripGroup As New SourceVtxStripGroup107()

				aStripGroup.vertexCount = Me.theInputFileReader.ReadInt16()
				aStripGroup.indexCount = Me.theInputFileReader.ReadInt16()
				aStripGroup.stripCount = Me.theInputFileReader.ReadInt16()
				aStripGroup.flags = Me.theInputFileReader.ReadByte()
				aStripGroup.unknown = Me.theInputFileReader.ReadByte()
				aStripGroup.vertexOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.indexOffset = Me.theInputFileReader.ReadInt32()
				aStripGroup.stripOffset = Me.theInputFileReader.ReadInt32()

				aMesh.theVtxStripGroups.Add(aStripGroup)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup")

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aStripGroup.vertexCount > 0 AndAlso aStripGroup.vertexOffset <> 0 Then
					If (aStripGroup.flags And SourceVtxStripGroup107.STRIPGROUP_USES_STATIC_PROP_VERTEXES) > 0 Then
						Me.ReadSourceVtxVertexesForStaticProp(stripGroupInputFileStreamPosition, aStripGroup)
					Else
						Me.ReadSourceVtxVertexes(stripGroupInputFileStreamPosition, aStripGroup)
					End If
				End If
				If aStripGroup.indexCount > 0 AndAlso aStripGroup.indexOffset <> 0 Then
					Me.ReadSourceVtxIndexes(stripGroupInputFileStreamPosition, aStripGroup)
				End If
				If aStripGroup.stripCount > 0 AndAlso aStripGroup.stripOffset <> 0 Then
					Me.ReadSourceVtxStrips(stripGroupInputFileStreamPosition, aStripGroup)
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxVertexesForStaticProp(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup107)
		'Dim modelInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.vertexOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aStripGroup.theVtxVertexesForStaticProp = New List(Of UInt16)(aStripGroup.vertexCount)
			For j As Integer = 0 To aStripGroup.vertexCount - 1
				'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				Dim aVertexMeshIndex As UInt16

				aVertexMeshIndex = Me.theInputFileReader.ReadUInt16()

				aStripGroup.theVtxVertexesForStaticProp.Add(aVertexMeshIndex)

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxVertexesForStaticProp " + aStripGroup.theVtxVertexesForStaticProp.Count.ToString())
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxVertexes(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup107)
		'Dim modelInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.vertexOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aStripGroup.theVtxVertexes = New List(Of SourceVtxVertex107)(aStripGroup.vertexCount)
			For j As Integer = 0 To aStripGroup.vertexCount - 1
				'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				Dim aVertex As New SourceVtxVertex107()

				aVertex.unknown01 = Me.theInputFileReader.ReadInt16()
				For x As Integer = 0 To aVertex.boneIndex.Length - 1
					aVertex.boneIndex(x) = Me.theInputFileReader.ReadInt16()
				Next
				aVertex.unknown02 = Me.theInputFileReader.ReadInt16()
				aVertex.originalMeshVertexIndex = Me.theInputFileReader.ReadUInt16()

				aStripGroup.theVtxVertexes.Add(aVertex)

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxVertexes " + aStripGroup.theVtxVertexes.Count.ToString())
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxIndexes(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup107)
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
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				Dim anIndex As UShort

				anIndex = Me.theInputFileReader.ReadUInt16()

				aStripGroup.theVtxIndexes.Add(anIndex)

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIndex")

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'If aModel.lodCount > 0 Then
				'	Me.ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel)
				'End If

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxIndexes " + aStripGroup.theVtxIndexes.Count.ToString())
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxStrips(ByVal stripGroupInputFileStreamPosition As Long, ByVal aStripGroup As SourceVtxStripGroup107)
		Dim stripInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.stripOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aStripGroup.theVtxStrips = New List(Of SourceVtxStrip107)(aStripGroup.stripCount)
			For j As Integer = 0 To aStripGroup.stripCount - 1
				stripInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				Dim aStrip As New SourceVtxStrip107()

				aStrip.indexCount = Me.theInputFileReader.ReadInt16()
				aStrip.indexMeshIndex = Me.theInputFileReader.ReadInt16()
				aStrip.vertexCount = Me.theInputFileReader.ReadInt16()
				aStrip.vertexMeshIndex = Me.theInputFileReader.ReadInt16()

				aStrip.boneCount = Me.theInputFileReader.ReadByte()
				aStrip.flags = Me.theInputFileReader.ReadByte()
				aStrip.boneStateChangeCount = Me.theInputFileReader.ReadInt16()
				aStrip.boneStateChangeOffset = Me.theInputFileReader.ReadInt32()

				aStripGroup.theVtxStrips.Add(aStrip)

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStrip")

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aStrip.boneStateChangeCount > 0 AndAlso aStrip.boneStateChangeOffset <> 0 Then
					Me.ReadSourceVtxBoneStateChanges(stripInputFileStreamPosition, aStrip)
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxStrips " + aStripGroup.theVtxStrips.Count.ToString())
		Catch ex As Exception
			'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadSourceVtxBoneStateChanges(ByVal stripInputFileStreamPosition As Long, ByVal aStrip As SourceVtxStrip107)
		'Dim modelInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(stripInputFileStreamPosition + aStrip.boneStateChangeOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aStrip.theVtxBoneStateChanges = New List(Of SourceVtxBoneStateChange107)(aStrip.boneStateChangeCount)
			For j As Integer = 0 To aStrip.boneStateChangeCount - 1
				'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				Dim aBoneStateChange As New SourceVtxBoneStateChange107()

				aBoneStateChange.hardwareId = Me.theInputFileReader.ReadInt16()
				aBoneStateChange.newBoneId = Me.theInputFileReader.ReadInt16()

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
	End Sub

#End Region

#Region "Data"

	Private theInputFileReader As BinaryReader
	Private theVtxFileData As SourceVtxFileData107

	'Private theFirstMeshWithStripGroups As SourceVtxMesh107
	'Private theFirstMeshWithStripGroupsInputFileStreamPosition As Long
	'Private theSecondMeshWithStripGroups As SourceVtxMesh107
	'Private theExpectedStartOfSecondStripGroupList As Long
	'Private theStripGroupUsesExtra8Bytes As Boolean

#End Region

End Class
