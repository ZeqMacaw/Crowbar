Imports System.IO
Imports System.Text

Public Class SourceVvdFile04

#Region "Creation and Destruction"

	Public Sub New(ByVal vvdFileReader As BinaryReader, ByVal vvdFileData As SourceVvdFileData04, Optional ByVal vvdFileOffsetStart As Long = 0)
		Me.theInputFileReader = vvdFileReader
		Me.theVvdFileOffsetStart = vvdFileOffsetStart
		Me.theVvdFileData = vvdFileData

		Me.theVvdFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length
	End Sub

#End Region

#Region "Methods"

	Public Sub ReadSourceVvdHeader()
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theVvdFileData.id = Me.theInputFileReader.ReadChars(4)
		Me.theVvdFileData.version = Me.theInputFileReader.ReadInt32()
		Me.theVvdFileData.checksum = Me.theInputFileReader.ReadInt32()
		Me.theVvdFileData.lodCount = Me.theInputFileReader.ReadInt32()
		For i As Integer = 0 To MAX_NUM_LODS - 1
			Me.theVvdFileData.lodVertexCount(i) = Me.theInputFileReader.ReadInt32()
		Next
		Me.theVvdFileData.fixupCount = Me.theInputFileReader.ReadInt32()
		Me.theVvdFileData.fixupTableOffset = Me.theInputFileReader.ReadInt32()
		Me.theVvdFileData.vertexDataOffset = Me.theInputFileReader.ReadInt32()
		Me.theVvdFileData.tangentDataOffset = Me.theInputFileReader.ReadInt32()

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theVvdFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VVD File Header")
	End Sub

	Public Sub ReadVertexes(Optional ByVal mdlVersion As Integer = 0)
		If Me.theVvdFileData.lodCount <= 0 Then
			Exit Sub
		End If

		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(Me.theVvdFileOffsetStart + Me.theVvdFileData.vertexDataOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		'Dim boneWeightingIsIncorrect As Boolean
		Dim weight As Single
		Dim boneIndex As Byte

		Dim vertexCount As Integer
		vertexCount = Me.theVvdFileData.lodVertexCount(0)
		Me.theVvdFileData.theVertexes = New List(Of SourceVertex)(vertexCount)
		For j As Integer = 0 To vertexCount - 1
			Dim aStudioVertex As New SourceVertex()

			Dim boneWeight As New SourceBoneWeight()
			'boneWeightingIsIncorrect = False
			For x As Integer = 0 To MAX_NUM_BONES_PER_VERT - 1
				weight = Me.theInputFileReader.ReadSingle()
				boneWeight.weight(x) = weight
				'If weight > 1 Then
				'	boneWeightingIsIncorrect = True
				'End If
			Next
			For x As Integer = 0 To MAX_NUM_BONES_PER_VERT - 1
				boneIndex = Me.theInputFileReader.ReadByte()
				boneWeight.bone(x) = boneIndex
				'If boneIndex > 127 Then
				'	boneWeightingIsIncorrect = True
				'End If
			Next
			boneWeight.boneCount = Me.theInputFileReader.ReadByte()
			''TODO: ReadVertexes() -- boneWeight.boneCount > MAX_NUM_BONES_PER_VERT, which seems like incorrect vvd format 
			'If boneWeight.boneCount > MAX_NUM_BONES_PER_VERT Then
			'	boneWeight.boneCount = CByte(MAX_NUM_BONES_PER_VERT)
			'End If
			'If boneWeightingIsIncorrect Then
			'	boneWeight.boneCount = 0
			'End If
			aStudioVertex.boneWeight = boneWeight

			aStudioVertex.positionX = Me.theInputFileReader.ReadSingle()
			aStudioVertex.positionY = Me.theInputFileReader.ReadSingle()
			aStudioVertex.positionZ = Me.theInputFileReader.ReadSingle()
			aStudioVertex.normalX = Me.theInputFileReader.ReadSingle()
			aStudioVertex.normalY = Me.theInputFileReader.ReadSingle()
			aStudioVertex.normalZ = Me.theInputFileReader.ReadSingle()
			aStudioVertex.texCoordX = Me.theInputFileReader.ReadSingle()
			aStudioVertex.texCoordY = Me.theInputFileReader.ReadSingle()
			If mdlVersion >= 54 AndAlso mdlVersion <= 59 Then
				Me.theInputFileReader.ReadSingle()
				Me.theInputFileReader.ReadSingle()
				Me.theInputFileReader.ReadSingle()
				Me.theInputFileReader.ReadSingle()
			End If
			Me.theVvdFileData.theVertexes.Add(aStudioVertex)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theVvdFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theVertexes " + Me.theVvdFileData.theVertexes.Count.ToString())
	End Sub

	Public Sub ReadFixups()
		If Me.theVvdFileData.fixupCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theVvdFileOffsetStart + Me.theVvdFileData.fixupTableOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theVvdFileData.theFixups = New List(Of SourceVvdFixup04)(Me.theVvdFileData.fixupCount)
			For fixupIndex As Integer = 0 To Me.theVvdFileData.fixupCount - 1
				Dim aFixup As New SourceVvdFixup04()

				aFixup.lodIndex = Me.theInputFileReader.ReadInt32()
				aFixup.vertexIndex = Me.theInputFileReader.ReadInt32()
				aFixup.vertexCount = Me.theInputFileReader.ReadInt32()
				Me.theVvdFileData.theFixups.Add(aFixup)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theVvdFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theFixups " + Me.theVvdFileData.theFixups.Count.ToString())

			If Me.theVvdFileData.lodCount > 0 Then
				Me.theInputFileReader.BaseStream.Seek(Me.theVvdFileOffsetStart + Me.theVvdFileData.vertexDataOffset, SeekOrigin.Begin)

				For lodIndex As Integer = 0 To Me.theVvdFileData.lodCount - 1
					Me.SetupFixedVertexes(lodIndex)
				Next
			End If
		End If
	End Sub

	Public Sub ReadUnreadBytes()
		Me.theVvdFileData.theFileSeekLog.LogUnreadBytes(Me.theInputFileReader)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub SetupFixedVertexes(ByVal lodIndex As Integer)
		Dim aFixup As SourceVvdFixup04
		Dim aStudioVertex As SourceVertex

		Try
			Me.theVvdFileData.theFixedVertexesByLod(lodIndex) = New List(Of SourceVertex)
			For fixupIndex As Integer = 0 To Me.theVvdFileData.theFixups.Count - 1
				aFixup = Me.theVvdFileData.theFixups(fixupIndex)

				If aFixup.lodIndex >= lodIndex Then
					For j As Integer = 0 To aFixup.vertexCount - 1
						aStudioVertex = Me.theVvdFileData.theVertexes(aFixup.vertexIndex + j)
						Me.theVvdFileData.theFixedVertexesByLod(lodIndex).Add(aStudioVertex)
					Next
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

#End Region

#Region "Data"

	Private theInputFileReader As BinaryReader
	Private theVvdFileOffsetStart As Long
	Private theVvdFileData As SourceVvdFileData04

#End Region

End Class
