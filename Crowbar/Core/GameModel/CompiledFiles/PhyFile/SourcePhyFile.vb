Imports System.IO
Imports System.Text

Public Class SourcePhyFile

#Region "Creation and Destruction"

	Public Sub New(ByVal phyFileReader As BinaryReader, ByVal phyFileData As SourcePhyFileData, Optional ByVal endOffset As Long = 0)
		Me.theInputFileReader = phyFileReader
		Me.thePhyFileData = phyFileData
		Me.thePhyEndOffset = endOffset

		Me.thePhyFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length
	End Sub

#End Region

#Region "Methods"

	' Big endian binary reader functions
	Public Function ReadInt32BE() As Integer
		Dim bytes() As Byte = Me.theInputFileReader.ReadBytes(4)
		Dim b1 As Integer = (bytes(0) >> 0) And &HFF
		Dim b2 As Integer = (bytes(1) >> 8) And &HFF
		Dim b3 As Integer = (bytes(2) >> 16) And &HFF
		Dim b4 As Integer = (bytes(3) >> 24) And &HFF

		Return b1 << 24 Or b2 << 16 Or b3 << 8 Or b4 << 0
	End Function

	Public Function ReadInt16BE() As Short
		Dim bytes() As Byte = Me.theInputFileReader.ReadBytes(2)
		'Dim b1 As Integer = (bytes(0) >> 0) And &HFF
		'Dim b2 As Integer = (bytes(1) >> 8) And &HFF
		Array.Reverse(bytes)

		'Return CShort(b1 << 8 Or b2 << 0)
		Return BitConverter.ToInt16(bytes, 0)
	End Function

	Public Function ReadUInt16BE() As UShort
		Dim bytes() As Byte = Me.theInputFileReader.ReadBytes(2)
		Dim b1 As Integer = (bytes(0) >> 0) And &HFF
		Dim b2 As Integer = (bytes(1) >> 8) And &HFF

		Return CUShort(b1 << 8 Or b2 << 0)
	End Function

	Public Function ReadSingleBE() As Single
		Dim bytes() As Byte = Me.theInputFileReader.ReadBytes(4)
		'Dim b1 As Integer = (bytes(0) >> 0) And &HFF
		'Dim b2 As Integer = (bytes(1) >> 8) And &HFF
		'Dim b3 As Integer = (bytes(2) >> 16) And &HFF
		'Dim b4 As Integer = (bytes(3) >> 24) And &HFF
		'Dim num As Single = b1 << 24 Or b2 << 16 Or b3 << 8 Or b4 << 0
		Array.Reverse(bytes)
		Dim num As Single = BitConverter.ToSingle(bytes, 0)

		Return num
	End Function

	Public Sub ReadSourcePhyHeader()
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.thePhyFileData.isBigEndian = False

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		' Offsets: 0x00, 0x04, 0x08, 0x0C (12)
		'FROM: Zoey_TeenAngst
		'10 00 00 00 
		'00 00 00 00 
		'12 00 00 00 
		'1f de 9d 20 

		If Me.theInputFileReader.ReadInt32() > 16 Then
			' Possibly big endian, let's confirm this
			Me.theInputFileReader.BaseStream.Seek(-4, SeekOrigin.Current)
			If ReadInt32BE() = 16 Then
				' Big endian file
				Me.thePhyFileData.isBigEndian = True
				Me.theInputFileReader.BaseStream.Seek(-4, SeekOrigin.Current)
			Else
				' Nevermind, continue as normal
				Me.theInputFileReader.BaseStream.Seek(-4, SeekOrigin.Current)
			End If
		Else
			' Nevermind, continue as normal
			Me.theInputFileReader.BaseStream.Seek(-4, SeekOrigin.Current)
		End If

		If Me.thePhyFileData.isBigEndian Then
			Me.thePhyFileData.size = ReadInt32BE()
			Me.thePhyFileData.id = ReadInt32BE()
			Me.thePhyFileData.solidCount = ReadInt32BE()
			Me.thePhyFileData.checksum = ReadInt32BE()
		Else
			Me.thePhyFileData.size = Me.theInputFileReader.ReadInt32()
			Me.thePhyFileData.id = Me.theInputFileReader.ReadInt32()
			Me.thePhyFileData.solidCount = Me.theInputFileReader.ReadInt32()
			Me.thePhyFileData.checksum = Me.theInputFileReader.ReadInt32()
		End If

		'NOTE: If header size ever increases, this will at least skip over extra stuff.
		Me.theInputFileReader.BaseStream.Seek(fileOffsetStart + Me.thePhyFileData.size, SeekOrigin.Begin)

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Header")
	End Sub

	Public Sub ReadSourceCollisionData()
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Dim ivpsId(3) As Char
		Dim triangleCount As Integer
		Dim triangleIndex As Integer
		Dim vertices As List(Of Integer)
		Dim nextSolidDataStreamPosition As Long
		Dim phyDataStreamPosition As Long
		Dim faceDataStreamPosition As Long
		Dim vertexDataStreamPosition As Long
		Dim vertexDataOffset As Long
		Dim convexMesh As SourcePhyConvexMesh

		Me.thePhyFileData.theSourcePhyMaxConvexPieces = 0
		Me.thePhyFileData.theSourcePhyCollisionDatas = New List(Of SourcePhyCollisionData)()
		For solidIndex As Integer = 0 To Me.thePhyFileData.solidCount - 1
			Dim collisionData As New SourcePhyCollisionData()
			collisionData.theConvexMeshes = New List(Of SourcePhyConvexMesh)()
			collisionData.theVertices = New List(Of SourcePhyVertex)()

			Me.theBoneIndexes = New SortedSet(Of Integer)()

			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			'b8 01 00 00   size
			If Me.thePhyFileData.isBigEndian Then
				collisionData.size = ReadInt32BE()
			Else
				collisionData.size = Me.theInputFileReader.ReadInt32()
			End If

			nextSolidDataStreamPosition = Me.theInputFileReader.BaseStream.Position + collisionData.size

			phyDataStreamPosition = Me.theInputFileReader.BaseStream.Position
			'56 50 48 59   VPHY
			Dim vphyId(3) As Char
			vphyId = Me.theInputFileReader.ReadChars(4)
			Me.theInputFileReader.BaseStream.Seek(phyDataStreamPosition, SeekOrigin.Begin)
			If vphyId <> "VPHY" And vphyId <> "YHPV" Then
				Me.ReadPhyData_VERSION37()
			Else
				Me.ReadPhyData_VERSION48()
			End If

			'49 56 50 53   IVPS
			ivpsId = Me.theInputFileReader.ReadChars(4)

			vertices = New List(Of Integer)()
			vertexDataStreamPosition = Me.theInputFileReader.BaseStream.Position + collisionData.size
			While Me.theInputFileReader.BaseStream.Position < vertexDataStreamPosition
				convexMesh = New SourcePhyConvexMesh()

				faceDataStreamPosition = Me.theInputFileReader.BaseStream.Position

				'd0 00 00 00 
				'29 00 00 00 
				'04 15 00 00 
				If Me.thePhyFileData.isBigEndian Then
					vertexDataOffset = ReadInt32BE()
				Else
					vertexDataOffset = Me.theInputFileReader.ReadInt32()
				End If

				vertexDataStreamPosition = faceDataStreamPosition + vertexDataOffset

				If Me.thePhyFileData.isBigEndian Then
					convexMesh.theBoneIndex = ReadInt32BE() - 1
				Else
					convexMesh.theBoneIndex = Me.theInputFileReader.ReadInt32() - 1
				End If

				Me.theBoneIndexes.Add(convexMesh.theBoneIndex)

				If Me.thePhyFileData.isBigEndian Then
					' flags: has_children: (self.flags >> 0) & 3  ' 0 = false; > 0 true
					convexMesh.flags = ReadInt32BE()

					'0c 00 00 00    count of lines after this (00 - 0b)
					triangleCount = ReadInt16BE()
					ReadInt16BE()
				Else
					' flags: has_children: (self.flags >> 0) & 3  ' 0 = false; > 0 true
					convexMesh.flags = Me.theInputFileReader.ReadInt32()

					'0c 00 00 00    count of lines after this (00 - 0b)
					triangleCount = Me.theInputFileReader.ReadInt32()
				End If


				'00 b0 00 00 
				'	00 00 06 00   ' vertex index 00
				'	01 00 18 00   ' vertex index 01
				'	02 00 0e 00   ' vertex index 02
				'01 a0 00 00 
				'	00 00 05 00 
				'	03 00 1d 00   ' vertex index 03
				'	01 00 fa 7f 
				'02 70 00 00 
				'	04 00 06 00   ' vertex index 04
				'	03 00 fb 7f 
				'	00 00 0c 00 
				'03 60 00 00 
				'	04 00 22 00 
				'	05 00 17 00 
				'	03 00 fa 7f 
				'04 80 00 00 
				'	00 00 f2 7f 
				'	02 00 07 00 
				'	06 00 02 00 
				'05 90 00 00 
				'	00 00 fe 7f 
				'	06 00 13 00 
				'	04 00 f4 7f 
				'06 30 00 00 
				'	06 00 f9 7f 
				'	02 00 e8 7f 
				'	01 00 02 00 
				'07 20 00 00 
				'	06 00 fe 7f 
				'	01 00 04 00 
				'	07 00 0b 00 
				'08 50 00 00 
				'	03 00 06 00 
				'	07 00 fc 7f 
				'	01 00 e3 7f 
				'09 50 00 00 
				'	03 00 e9 7f 
				'	05 00 08 00 
				'	07 00 fa 7f 
				'0a 10 00 00 
				'	04 00 ed 7f 
				'	06 00 f5 7f 
				'	07 00 02 00 
				'0b 
				'	00 
				'	00 00 
				'	04 00 
				'		fe 7f 
				'	07 00 
				'		f8 7f 
				'	05 00 
				'		de 7f 
				For i As Integer = 0 To triangleCount - 1
					Dim phyTriangle As New SourcePhyFace()
					If Me.thePhyFileData.isBigEndian Then
						Me.theInputFileReader.ReadByte()
						triangleIndex = Me.theInputFileReader.ReadByte()
					Else
						triangleIndex = Me.theInputFileReader.ReadByte()
						Me.theInputFileReader.ReadByte()
					End If

					Me.theInputFileReader.ReadUInt16()

					For j As Integer = 0 To 2
						If Me.thePhyFileData.isBigEndian Then
							phyTriangle.vertexIndex(j) = ReadUInt16BE()
						Else
							phyTriangle.vertexIndex(j) = Me.theInputFileReader.ReadUInt16()
						End If

						Me.theInputFileReader.ReadUInt16()
						If Not vertices.Contains(phyTriangle.vertexIndex(j)) Then
							vertices.Add(phyTriangle.vertexIndex(j))
						End If
					Next
					convexMesh.theFaces.Add(phyTriangle)
				Next
				collisionData.theConvexMeshes.Add(convexMesh)
			End While

			If Me.thePhyFileData.theSourcePhyMaxConvexPieces < collisionData.theConvexMeshes.Count Then
				Me.thePhyFileData.theSourcePhyMaxConvexPieces = collisionData.theConvexMeshes.Count
			End If

			Me.theInputFileReader.BaseStream.Seek(vertexDataStreamPosition, SeekOrigin.Begin)

			' Vertex data section.
			'	' 8 distinct vertices
			'ae 69 29 3b 01 6f 4d bd f6 4a 9b 3c 78 f7 18 00 
			'ed dd fe 3d f0 37 1c 3d b8 60 16 3d 78 f7 18 00 
			'e2 98 a4 3a 08 34 1c 3d df 72 22 3d 78 f7 18 00 
			'6a cb 00 3e 21 6b 4d bd a7 26 83 3c 78 f7 18 00 
			'fb 9a c7 3a d7 29 1a bd 06 46 0d bd 78 f7 18 00 
			'ec 69 ff 3d f7 25 1a bd 30 58 19 bd 78 f7 18 00 
			'20 04 4b 39 34 79 4f 3d 82 e2 61 bc 78 f7 18 00 
			'07 b1 fc 3d 1c 7d 4f 3d 90 15 89 bc 78 f7 18 00 
			Dim w As Double
			Dim convexMesh0Vertices As List(Of SourcePhyVertex)
			convexMesh0Vertices = collisionData.theConvexMeshes(0).theVertices
			For i As Integer = 0 To vertices.Count - 1
				Dim phyVertex As New SourcePhyVertex()

				If Me.thePhyFileData.isBigEndian Then
					phyVertex.vertex.x = ReadSingleBE()
					phyVertex.vertex.y = ReadSingleBE()
					phyVertex.vertex.z = ReadSingleBE()
					w = ReadSingleBE()
				Else
					phyVertex.vertex.x = Me.theInputFileReader.ReadSingle()
					phyVertex.vertex.y = Me.theInputFileReader.ReadSingle()
					phyVertex.vertex.z = Me.theInputFileReader.ReadSingle()
					w = Me.theInputFileReader.ReadSingle()
				End If

				convexMesh0Vertices.Add(phyVertex)
			Next
			For convexMeshIndex As Integer = 1 To collisionData.theConvexMeshes.Count - 1
				convexMesh = collisionData.theConvexMeshes(convexMeshIndex)
				For i As Integer = 0 To vertices.Count - 1
					Dim phyVertex As New SourcePhyVertex()

					phyVertex.vertex.x = convexMesh0Vertices(i).vertex.x
					phyVertex.vertex.y = convexMesh0Vertices(i).vertex.y
					phyVertex.vertex.z = convexMesh0Vertices(i).vertex.z

					convexMesh.theVertices.Add(phyVertex)
				Next
			Next



			'TODO: Figure out this section.
			' Section after vertex section. Presumably, this section contains normal data, but still unsure how it is arranged.
			' It does not seem to be arranged by count of face sections.
			' First 32-bits seems to be size of list or pointer to next list.
			' Example data from two-cubes (concavity_test from Bang).
			' One list of Two structs.
			'Address: 0x0490
			'38 00 00 00   ' Size of list or pointer to next list of structs, which in this case is last section of CollisionData below.
			'70 fd ff ff 
			'00 00 80 31 
			'a9 13 d0 bd 7a ec 1d 3d 1c 12 89 3e 
			'60 bf 84 00 
			'------
			'00 00 00 00    ' Zero offset to indicate last struct?
			'b4 fb ff ff 
			'00 00 00 00 
			'00 00 00 00 00 00 00 00 26 33 34 3e 
			'91 91 91 00 



			'TODO: Figure out this section.
			' Last section of the CollisionData.
			' Example data from two-cubes (concavity_test from Bang).
			'Address: 0x04c8
			'00 00 00 00 
			'68 fc ff ff 
			'00 00 00 32 
			'a9 13 50 be 79 ec 9d 3d 26 33 34 3e 
			'91 91 91 00



			Me.thePhyFileData.theSourcePhyCollisionDatas.Add(collisionData)

			Dim boneIndexesText As String = ""
			For Each boneIndex As Integer In Me.theBoneIndexes
				boneIndexesText += "  " + boneIndex.ToString()
			Next
			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Solid(" + solidIndex.ToString() + ")   Convex mesh count = " + collisionData.theConvexMeshes.Count.ToString() + "   Bone indexes:" + boneIndexesText)

			Me.theInputFileReader.BaseStream.Seek(nextSolidDataStreamPosition, SeekOrigin.Begin)
		Next
		Me.thePhyFileData.theSourcePhyKeyValueDataOffset = Me.theInputFileReader.BaseStream.Position
	End Sub

	Public Sub ReadPhyData_VERSION37()
		'FROM: HL2 leak - bullsquid.phy
		'66 c5 36 bc 
		'd8 ac 31 bd 
		'd9 14 dd bd 
		'19 bd 01 3d 
		'ae 4f 31 3d 
		'52 d0 4a 3d 
		'e1 01 0b 3f 
		'd8 1c 03 00 
		'00 03 00 00 
		'00 00 00 00 
		'00 00 00 00
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
	End Sub

	Public Sub ReadPhyData_VERSION48()
		Dim tempInt As Integer

		'56 50 48 59   VPHY
		Dim vphyId(3) As Char
		vphyId = Me.theInputFileReader.ReadChars(4)

		'00 01         version?
		'00 00         model type?
		If Me.thePhyFileData.isBigEndian Then
			tempInt = ReadUInt16BE()
			tempInt = ReadUInt16BE()
		Else
			tempInt = Me.theInputFileReader.ReadUInt16()
			tempInt = Me.theInputFileReader.ReadUInt16()
		End If

		'9c 01 00 00   surface size? might be size of remaining solid struct after "axisMapSize?" field
		'              Seems to be size of data struct from VPHY field to last section of CollisionData.
		Me.theInputFileReader.ReadInt32()


		'00 00 30 3f   dragAxisAreas x?
		'00 00 80 3f   dragAxisAreas y?
		'00 00 80 3f   dragAxisAreas z?
		'00 00 00 00   axisMapSize?
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()

		'2c fe 80 3d 
		'a5 85 83 39 
		'27 ab 91 3a 
		'52 06 3c 3a 
		'28 a7 a9 3a 
		'c8 2a bb 3a 
		'5e 08 a8 3d 
		'ec 9c 01 00 
		'80 01 00 00   size of something? add this to address right after it = address right after the next VPHY
		'00 00 00 00 
		'00 00 00 00 
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
		Me.theInputFileReader.ReadInt32()
	End Sub

	Public Sub CalculateVertexNormals()
		Dim collisionData As SourcePhyCollisionData
		Dim aTriangle As SourcePhyFace
		Dim faceSection As SourcePhyConvexMesh

		If Me.thePhyFileData.theSourcePhyCollisionDatas IsNot Nothing Then
			For collisionDataIndex As Integer = 0 To Me.thePhyFileData.theSourcePhyCollisionDatas.Count - 1
				collisionData = Me.thePhyFileData.theSourcePhyCollisionDatas(collisionDataIndex)

				For faceSectionIndex As Integer = 0 To collisionData.theConvexMeshes.Count - 1
					faceSection = collisionData.theConvexMeshes(faceSectionIndex)

					For triangleIndex As Integer = 0 To faceSection.theFaces.Count - 1
						aTriangle = faceSection.theFaces(triangleIndex)

						Me.CalculateFaceNormal(faceSection, aTriangle)
					Next
				Next
			Next
		End If
	End Sub

	Public Sub ReadSourcePhysCollisionModels()
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Try
			Dim line As String
			Dim thereIsAValue As Boolean = True
			Dim key As String = ""
			Dim value As String = ""
			Dim tempStreamOffset As Long
			Dim aSourcePhysCollisionModel As SourcePhyPhysCollisionModel
			Me.thePhyFileData.theSourcePhyPhysCollisionModels = New List(Of SourcePhyPhysCollisionModel)()
			Me.theDampingToCountMap = New SortedList(Of Single, Integer)()
			Me.theInertiaToCountMap = New SortedList(Of Single, Integer)()
			Me.theRotDampingToCountMap = New SortedList(Of Single, Integer)()
			Do
				aSourcePhysCollisionModel = New SourcePhyPhysCollisionModel()
				tempStreamOffset = Me.theInputFileReader.BaseStream.Position
				line = FileManager.ReadTextLine(Me.theInputFileReader)
				If line Is Nothing OrElse line <> "solid {" Then
					Me.theInputFileReader.BaseStream.Seek(tempStreamOffset, SeekOrigin.Begin)
					Exit Do
				End If

				While thereIsAValue
					thereIsAValue = FileManager.ReadKeyValueLine(Me.theInputFileReader, key, value)
					If thereIsAValue Then
						If key = "index" Then
							aSourcePhysCollisionModel.theIndex = Integer.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "name" Then
							aSourcePhysCollisionModel.theName = value
						ElseIf key = "parent" Then
							aSourcePhysCollisionModel.theParentIsValid = True
							aSourcePhysCollisionModel.theParentName = value
						ElseIf key = "mass" Then
							aSourcePhysCollisionModel.theMass = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "surfaceprop" Then
							aSourcePhysCollisionModel.theSurfaceProp = value
						ElseIf key = "damping" Then
							aSourcePhysCollisionModel.theDamping = Single.Parse(value, TheApp.InternalNumberFormat)
							If Me.theDampingToCountMap.ContainsKey(aSourcePhysCollisionModel.theDamping) Then
								Me.theDampingToCountMap(aSourcePhysCollisionModel.theDamping) += 1
							Else
								Me.theDampingToCountMap.Add(aSourcePhysCollisionModel.theDamping, 1)
							End If
						ElseIf key = "rotdamping" Then
							aSourcePhysCollisionModel.theRotDamping = Single.Parse(value, TheApp.InternalNumberFormat)
							If Me.theRotDampingToCountMap.ContainsKey(aSourcePhysCollisionModel.theRotDamping) Then
								Me.theRotDampingToCountMap(aSourcePhysCollisionModel.theRotDamping) += 1
							Else
								Me.theRotDampingToCountMap.Add(aSourcePhysCollisionModel.theRotDamping, 1)
							End If
						ElseIf key = "drag" Then
							aSourcePhysCollisionModel.theDragCoefficientIsValid = True
							aSourcePhysCollisionModel.theDragCoefficient = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "rollingDrag" Then
							aSourcePhysCollisionModel.theRollingDragCoefficientIsValid = True
							aSourcePhysCollisionModel.theRollingDragCoefficient = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "inertia" Then
							aSourcePhysCollisionModel.theInertia = Single.Parse(value, TheApp.InternalNumberFormat)
							If Me.theInertiaToCountMap.ContainsKey(aSourcePhysCollisionModel.theInertia) Then
								Me.theInertiaToCountMap(aSourcePhysCollisionModel.theInertia) += 1
							Else
								Me.theInertiaToCountMap.Add(aSourcePhysCollisionModel.theInertia, 1)
							End If
						ElseIf key = "volume" Then
							aSourcePhysCollisionModel.theVolume = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "massbias" Then
							aSourcePhysCollisionModel.theMassBiasIsValid = True
							aSourcePhysCollisionModel.theMassBias = Single.Parse(value, TheApp.InternalNumberFormat)
						End If
					End If
				End While

				'NOTE: Above while loop should return the ending brace.
				If key Is Nothing OrElse key <> "}" Then
					Exit Do
				End If
				Me.thePhyFileData.theSourcePhyPhysCollisionModels.Add(aSourcePhysCollisionModel)
				thereIsAValue = True
			Loop Until line Is Nothing

			Dim maxValue As Single
			Dim maxCount As Integer
			maxCount = 0
			Me.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues = New SourcePhyPhysCollisionModel()
			For i As Integer = 0 To Me.theDampingToCountMap.Count - 1
				If maxCount <= Me.theDampingToCountMap.Values(i) Then
					maxValue = Me.theDampingToCountMap.Keys(i)
					maxCount = Me.theDampingToCountMap.Values(i)
				End If
			Next
			Me.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theDamping = maxValue
			maxCount = 0
			For i As Integer = 0 To Me.theInertiaToCountMap.Count - 1
				If maxCount <= Me.theInertiaToCountMap.Values(i) Then
					maxValue = Me.theInertiaToCountMap.Keys(i)
					maxCount = Me.theInertiaToCountMap.Values(i)
				End If
			Next
			Me.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theInertia = maxValue
			maxCount = 0
			For i As Integer = 0 To Me.theRotDampingToCountMap.Count - 1
				If maxCount <= Me.theRotDampingToCountMap.Values(i) Then
					maxValue = Me.theRotDampingToCountMap.Keys(i)
					maxCount = Me.theRotDampingToCountMap.Values(i)
				End If
			Next
			Me.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping = maxValue
		Catch ex As Exception
			Throw
			'Finally
		End Try

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Properties")
	End Sub

	Public Sub ReadSourcePhyRagdollConstraintDescs()
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Try
			Dim line As String
			Dim thereIsAValue As Boolean = True
			Dim key As String = ""
			Dim value As String = ""
			Dim tempStreamOffset As Long
			Dim aSourceRagdollConstraintDesc As SourcePhyRagdollConstraint
			Me.thePhyFileData.theSourcePhyRagdollConstraintDescs = New SortedList(Of Integer, SourcePhyRagdollConstraint)()
			Do
				aSourceRagdollConstraintDesc = New SourcePhyRagdollConstraint()
				tempStreamOffset = Me.theInputFileReader.BaseStream.Position
				line = FileManager.ReadTextLine(Me.theInputFileReader)
				If line Is Nothing OrElse line <> "ragdollconstraint {" Then
					Me.theInputFileReader.BaseStream.Seek(tempStreamOffset, SeekOrigin.Begin)
					Exit Do
				End If

				While thereIsAValue
					thereIsAValue = FileManager.ReadKeyValueLine(Me.theInputFileReader, key, value)
					If thereIsAValue Then
						If key = "parent" Then
							aSourceRagdollConstraintDesc.theParentIndex = Integer.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "child" Then
							aSourceRagdollConstraintDesc.theChildIndex = Integer.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "xmin" Then
							aSourceRagdollConstraintDesc.theXMin = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "xmax" Then
							aSourceRagdollConstraintDesc.theXMax = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "xfriction" Then
							aSourceRagdollConstraintDesc.theXFriction = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "ymin" Then
							aSourceRagdollConstraintDesc.theYMin = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "ymax" Then
							aSourceRagdollConstraintDesc.theYMax = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "yfriction" Then
							aSourceRagdollConstraintDesc.theYFriction = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "zmin" Then
							aSourceRagdollConstraintDesc.theZMin = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "zmax" Then
							aSourceRagdollConstraintDesc.theZMax = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "zfriction" Then
							aSourceRagdollConstraintDesc.theZFriction = Single.Parse(value, TheApp.InternalNumberFormat)
						End If
					End If
				End While

				'NOTE: Above while loop should return the ending brace.
				If key Is Nothing OrElse key <> "}" Then
					Exit Do
				End If
				Me.thePhyFileData.theSourcePhyRagdollConstraintDescs.Add(aSourceRagdollConstraintDesc.theChildIndex, aSourceRagdollConstraintDesc)
				thereIsAValue = True
			Loop Until line Is Nothing
		Catch ex As Exception
			Throw
		Finally
		End Try

		If fileOffsetStart < Me.theInputFileReader.BaseStream.Position Then
			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Ragdoll constraints")
		End If
	End Sub

	Public Sub ReadSourcePhyCollisionRules()
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Try
			Dim line As String
			Dim thereIsAValue As Boolean = True
			Dim key As String = ""
			Dim value As String = ""
			Dim tempStreamOffset As Long
			Dim aSourcePhyCollisionPair As SourcePhyCollisionPair
			Me.thePhyFileData.theSourcePhyCollisionPairs = New List(Of SourcePhyCollisionPair)()
			Do
				tempStreamOffset = Me.theInputFileReader.BaseStream.Position
				line = FileManager.ReadTextLine(Me.theInputFileReader)
				If line Is Nothing OrElse line <> "collisionrules {" Then
					Me.theInputFileReader.BaseStream.Seek(tempStreamOffset, SeekOrigin.Begin)
					Exit Do
				End If

				Dim delimiters As Char() = {","c}
				Dim tokens As String() = {""}
				While thereIsAValue
					thereIsAValue = FileManager.ReadKeyValueLine(Me.theInputFileReader, key, value)
					If thereIsAValue Then
						If key = "collisionpair" Then
							tokens = value.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
							If tokens.Length = 2 Then
								aSourcePhyCollisionPair = New SourcePhyCollisionPair()
								aSourcePhyCollisionPair.obj0 = Integer.Parse(tokens(0), TheApp.InternalNumberFormat)
								aSourcePhyCollisionPair.obj1 = Integer.Parse(tokens(1), TheApp.InternalNumberFormat)
								Me.thePhyFileData.theSourcePhyCollisionPairs.Add(aSourcePhyCollisionPair)
							End If
						ElseIf key = "selfcollisions" Then
							Me.thePhyFileData.theSourcePhySelfCollides = False
						End If
					End If
				End While

				'NOTE: Above while loop should return the ending brace.
				If key Is Nothing OrElse key <> "}" Then
					Exit Do
				End If
				thereIsAValue = True
			Loop Until line Is Nothing
		Catch ex As Exception
			Throw
		Finally
		End Try

		If fileOffsetStart < Me.theInputFileReader.BaseStream.Position Then
			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Collision rules")
		End If
	End Sub

	Public Sub ReadSourcePhyAnimatedFrictionSection()
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Try
			Dim line As String
			Dim thereIsAValue As Boolean = True
			Dim key As String = ""
			Dim value As String = ""
			Dim tempStreamOffset As Long
			Me.thePhyFileData.theSourcePhyAnimatedFrictionSection = New SourcePhyAnimatedFrictionSection()
			Do
				tempStreamOffset = Me.theInputFileReader.BaseStream.Position
				line = FileManager.ReadTextLine(Me.theInputFileReader)
				If line Is Nothing OrElse line <> "animatedfriction {" Then
					Me.theInputFileReader.BaseStream.Seek(tempStreamOffset, SeekOrigin.Begin)
					Exit Do
				End If

				While thereIsAValue
					thereIsAValue = FileManager.ReadKeyValueLine(Me.theInputFileReader, key, value)
					If thereIsAValue Then
						If key = "animfrictionmin" Then
							Me.thePhyFileData.theSourcePhyAnimatedFrictionSection.animFrictionMin = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "animfrictionmax" Then
							Me.thePhyFileData.theSourcePhyAnimatedFrictionSection.animFrictionMax = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "animfrictiontimein" Then
							Me.thePhyFileData.theSourcePhyAnimatedFrictionSection.animFrictionTimeIn = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "animfrictiontimeout" Then
							Me.thePhyFileData.theSourcePhyAnimatedFrictionSection.animFrictionTimeOut = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "animfrictiontimehold" Then
							Me.thePhyFileData.theSourcePhyAnimatedFrictionSection.animFrictionTimeHold = Single.Parse(value, TheApp.InternalNumberFormat)
						End If
					End If
				End While

				'NOTE: Above while loop should return the ending brace.
				If key Is Nothing OrElse key <> "}" Then
					Exit Do
				End If
				thereIsAValue = True
			Loop Until line Is Nothing
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try

		If fileOffsetStart < Me.theInputFileReader.BaseStream.Position Then
			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Animated Friction")
		End If
	End Sub

	Public Sub ReadSourcePhyEditParamsSection()
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Try
			Dim line As String
			Dim thereIsAValue As Boolean = True
			Dim key As String = ""
			Dim value As String = ""
			Dim tempStreamOffset As Long
			Me.thePhyFileData.theSourcePhyEditParamsSection = New SourcePhyEditParamsSection()
			Do
				tempStreamOffset = Me.theInputFileReader.BaseStream.Position
				line = FileManager.ReadTextLine(Me.theInputFileReader)
				If line Is Nothing OrElse line <> "editparams {" Then
					Me.theInputFileReader.BaseStream.Seek(tempStreamOffset, SeekOrigin.Begin)
					Exit Do
				End If

				Dim delimiters As Char() = {","c}
				Dim tokens As String() = {""}
				While thereIsAValue
					thereIsAValue = FileManager.ReadKeyValueLine(Me.theInputFileReader, key, value)
					If key = "rootname" Then
						thereIsAValue = True
						If key <> value Then
							Me.thePhyFileData.theSourcePhyEditParamsSection.rootName = value
							'Else
							'	Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyEditParamsSection.rootName = ""
						End If
					ElseIf thereIsAValue Then
						If key = "concave" Then
							Me.thePhyFileData.theSourcePhyEditParamsSection.concave = value
						ElseIf key = "totalmass" Then
							Me.thePhyFileData.theSourcePhyEditParamsSection.totalMass = Single.Parse(value, TheApp.InternalNumberFormat)
						End If
					End If
				End While

				'NOTE: Above while loop should return the ending brace.
				If key Is Nothing OrElse key <> "}" Then
					Exit Do
				End If
				thereIsAValue = True
			Loop Until line Is Nothing
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try

		If fileOffsetStart < Me.theInputFileReader.BaseStream.Position Then
			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Edit params")
		End If
	End Sub

	Public Sub ReadCollisionTextSection()
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Try
			Dim endOffset As Long
			If Me.thePhyEndOffset = 0 Then
				endOffset = Me.theInputFileReader.BaseStream.Length() - 1
			Else
				endOffset = Me.thePhyEndOffset
			End If

			Me.thePhyFileData.theSourcePhyCollisionText = Common.ReadPhyCollisionTextSection(Me.theInputFileReader, endOffset)
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try

		If fileOffsetStart < Me.theInputFileReader.BaseStream.Position Then
			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Collision text")
		End If
	End Sub

	Public Sub ReadUnreadBytes()
		Me.thePhyFileData.theFileSeekLog.LogUnreadBytes(Me.theInputFileReader)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub CalculateFaceNormal(ByVal faceSection As SourcePhyConvexMesh, ByVal aTriangle As SourcePhyFace)
		Dim vertex(3) As SourceVector
		Dim vector0 As New SourceVector()
		Dim vector1 As New SourceVector()
		Dim normalVector As New SourceVector()

		For vertexIndex As Integer = 0 To 2
			vertex(vertexIndex) = faceSection.theVertices(aTriangle.vertexIndex(vertexIndex)).vertex
		Next

		vector0.x = vertex(0).x - vertex(1).x
		vector0.y = vertex(0).y - vertex(1).y
		vector0.z = vertex(0).z - vertex(1).z

		vector1.x = vertex(1).x - vertex(2).x
		vector1.y = vertex(1).y - vertex(2).y
		vector1.z = vertex(1).z - vertex(2).z

		normalVector = vector0.CrossProduct(vector1)
		'NOTE: Do not need to normalize here. It will be normalized once after all of the normals are added together.
		'normalVector = normalVector.Normalize()

		Dim phyVertex As SourcePhyVertex
		For vertexIndex As Integer = 0 To 2
			'NOTE: Instead of storing all of the normals, just store one and keep adding to it. 
			'      Can then just do the normalize once when normal is first accessed.
			phyVertex = faceSection.theVertices(aTriangle.vertexIndex(vertexIndex))
			'phyVertex.Normal.x += normalVector.x
			'phyVertex.Normal.y += normalVector.y
			'phyVertex.Normal.z += normalVector.z
			phyVertex.UnnormalizedNormal.x += normalVector.x
			phyVertex.UnnormalizedNormal.y += normalVector.y
			phyVertex.UnnormalizedNormal.z += normalVector.z
		Next
	End Sub

#End Region

#Region "Data"

	Private theInputFileReader As BinaryReader
	Private thePhyFileData As SourcePhyFileData
	Private thePhyEndOffset As Long

	Private theDampingToCountMap As SortedList(Of Single, Integer)
	Private theInertiaToCountMap As SortedList(Of Single, Integer)
	Private theRotDampingToCountMap As SortedList(Of Single, Integer)

	Private theBoneIndexes As SortedSet(Of Integer)

#End Region

End Class
