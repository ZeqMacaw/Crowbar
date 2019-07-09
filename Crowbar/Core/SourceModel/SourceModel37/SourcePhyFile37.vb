Imports System.IO
Imports System.Text

Public Class SourcePhyFile37

#Region "Creation and Destruction"

	Public Sub New(ByVal phyFileReader As BinaryReader, ByVal phyFileData As SourcePhyFileData)
		Me.theInputFileReader = phyFileReader
		Me.thePhyFileData = phyFileData
	End Sub

#End Region

#Region "Methods"

	Public Sub ReadSourcePhyHeader()
		' Offsets: 0x00, 0x04, 0x08, 0x0C (12)
		'FROM: Zoey_TeenAngst
		'10 00 00 00 
		'00 00 00 00 
		'12 00 00 00 
		'1f de 9d 20 
		Me.thePhyFileData.size = Me.theInputFileReader.ReadInt32()
		Me.thePhyFileData.id = Me.theInputFileReader.ReadInt32()
		Me.thePhyFileData.solidCount = Me.theInputFileReader.ReadInt32()
		If Me.thePhyFileData.solidCount = 1 Then
			Me.thePhyFileData.theSourcePhyIsCollisionModel = True
		End If
		Me.thePhyFileData.checksum = Me.theInputFileReader.ReadInt32()

		'NOTE: If header size ever increases, this will at least skip over extra stuff.
		Me.theInputFileReader.BaseStream.Seek(Me.thePhyFileData.size, SeekOrigin.Begin)
	End Sub

	Public Sub ReadSourceCollisionData()
		'Me.thePhyCollisionDataList = New List(Of SourcePhyCollisionData)()

		Dim ivpsId(3) As Char
		Dim triangleCount As Integer
		Dim triangleIndex As Integer
		Dim vertices As List(Of Integer)
		Dim nextSolidDataStreamPosition As Long
		Dim phyDataStreamPosition As Long
		Dim faceDataStreamPosition As Long
		Dim vertexDataStreamPosition As Long
		Dim vertexDataOffset As Long
		'Dim boneIndexIsSet As Boolean
		Dim faceSection As SourcePhyFaceSection

		Me.thePhyFileData.theSourcePhyCollisionDatas = New List(Of SourcePhyCollisionData)()
		For solidIndex As Integer = 0 To Me.thePhyFileData.solidCount - 1
			Dim collisionData As New SourcePhyCollisionData()
			'collisionData.theFaces = New List(Of SourcePhyFace)()
			collisionData.theFaceSections = New List(Of SourcePhyFaceSection)()
			'collisionData.theVertices = New List(Of SourceVector)()
			collisionData.theVertices = New List(Of SourcePhyVertex)()
			'boneIndexIsSet = False

			'b8 01 00 00   size
			collisionData.size = Me.theInputFileReader.ReadInt32()
			nextSolidDataStreamPosition = Me.theInputFileReader.BaseStream.Position + collisionData.size


			'Me.theInputFileReader.ReadBytes(size)

			'======

			phyDataStreamPosition = Me.theInputFileReader.BaseStream.Position
			'56 50 48 59   VPHY
			Dim vphyId(3) As Char
			vphyId = Me.theInputFileReader.ReadChars(4)
			Me.theInputFileReader.BaseStream.Seek(phyDataStreamPosition, SeekOrigin.Begin)
			If vphyId <> "VPHY" Then
				Me.ReadPhyData_VERSION37()
			Else
				Me.ReadPhyData_VERSION48()
			End If

			'49 56 50 53   IVPS
			ivpsId = Me.theInputFileReader.ReadChars(4)

			vertices = New List(Of Integer)()
			'faceDataStreamPosition = Me.theInputFileReader.BaseStream.Position + collisionData.size
			vertexDataStreamPosition = Me.theInputFileReader.BaseStream.Position + collisionData.size
			While Me.theInputFileReader.BaseStream.Position < vertexDataStreamPosition
				faceSection = New SourcePhyFaceSection()

				faceDataStreamPosition = Me.theInputFileReader.BaseStream.Position

				'd0 00 00 00 
				'29 00 00 00 
				'04 15 00 00 
				vertexDataOffset = Me.theInputFileReader.ReadInt32()
				'vertexDataStreamPosition = Me.theInputFileReader.BaseStream.Position + faceDataOffset - 4
				vertexDataStreamPosition = faceDataStreamPosition + vertexDataOffset

				'TODO: This does not seem to be used by MDL v37.
				faceSection.theBoneIndex = Me.theInputFileReader.ReadInt32()
				'faceSection.theBoneIndex = Me.theInputFileReader.ReadInt32() - 1
				'If faceSection.theBoneIndex < 0 Then
				'	faceSection.theBoneIndex = 0
				'	Me.thePhyFileData.theSourcePhyIsCollisionModel = True
				'End If

				Dim test As Integer
				test = Me.theInputFileReader.ReadInt32()

				'0c 00 00 00    count of lines after this (00 - 0b)
				triangleCount = Me.theInputFileReader.ReadInt32()

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
					triangleIndex = Me.theInputFileReader.ReadByte()
					Me.theInputFileReader.ReadByte()
					Me.theInputFileReader.ReadUInt16()

					For j As Integer = 0 To 2
						phyTriangle.vertexIndex(j) = Me.theInputFileReader.ReadUInt16()
						Me.theInputFileReader.ReadUInt16()
						If Not vertices.Contains(phyTriangle.vertexIndex(j)) Then
							vertices.Add(phyTriangle.vertexIndex(j))
						End If
					Next
					faceSection.theFaces.Add(phyTriangle)
				Next
				collisionData.theFaceSections.Add(faceSection)
			End While

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
			'Dim aFloat16 As SourceFloat16
			'For i As Integer = 0 To vertices.Count - 1
			'	Dim phyVertex As New SourcePhyVertex()

			'	phyVertex.x = Me.theInputFileReader.ReadInt32()
			'	phyVertex.y = Me.theInputFileReader.ReadInt32()
			'	phyVertex.z = Me.theInputFileReader.ReadInt32()
			'	phyVertex.w = Me.theInputFileReader.ReadInt32()

			'	'phyVertex.x = Me.theInputFileReader.ReadInt16()
			'	'phyVertex.x2 = Me.theInputFileReader.ReadInt16()
			'	'phyVertex.y = Me.theInputFileReader.ReadInt16()
			'	'phyVertex.y2 = Me.theInputFileReader.ReadInt16()
			'	'phyVertex.z = Me.theInputFileReader.ReadInt16()
			'	'phyVertex.z2 = Me.theInputFileReader.ReadInt16()
			'	'phyVertex.w = Me.theInputFileReader.ReadInt16()
			'	'phyVertex.w2 = Me.theInputFileReader.ReadInt16()

			'	'phyVertex.x = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.x2 = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.y = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.y2 = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.z = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.z2 = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.w = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.w2 = Me.theInputFileReader.ReadUInt16()

			'	'aFloat16 = New SourceFloat16
			'	'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.x = aFloat16.TheFloatValue
			'	'aFloat16 = New SourceFloat16
			'	'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.x2 = aFloat16.TheFloatValue
			'	'aFloat16 = New SourceFloat16
			'	'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.y = aFloat16.TheFloatValue
			'	'aFloat16 = New SourceFloat16
			'	'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.y2 = aFloat16.TheFloatValue
			'	'aFloat16 = New SourceFloat16
			'	'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.z = aFloat16.TheFloatValue
			'	'aFloat16 = New SourceFloat16
			'	'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.z2 = aFloat16.TheFloatValue
			'	'aFloat16 = New SourceFloat16
			'	'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.w = aFloat16.TheFloatValue
			'	'aFloat16 = New SourceFloat16
			'	'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
			'	'phyVertex.w2 = aFloat16.TheFloatValue

			'	collisionData.theVertices.Add(phyVertex)
			'Next
			Dim w As Double
			'Dim w As Integer
			'Dim x As Integer
			'Dim y As Integer
			'Dim z As Integer
			'Dim w As Integer
			'Dim aFloat16 As SourceFloat16
			'NOTE: In my disassembling of MDL Decompiler with OllyDbg, it seems the 4th value (w) is not used in the conversion.
			For i As Integer = 0 To vertices.Count - 1
				'Dim phyVertex As New SourceVector()
				Dim phyVertex As New SourcePhyVertex()

				'' For debugging:
				'x = Me.theInputFileReader.ReadInt32()
				'y = Me.theInputFileReader.ReadInt32()
				'z = Me.theInputFileReader.ReadInt32()
				'w = Me.theInputFileReader.ReadInt32()
				'======
				phyVertex.vertex.x = Me.theInputFileReader.ReadSingle()
				phyVertex.vertex.y = Me.theInputFileReader.ReadSingle()
				phyVertex.vertex.z = Me.theInputFileReader.ReadSingle()
				w = Me.theInputFileReader.ReadSingle()
				'======
				'aFloat16 = New SourceFloat16
				'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
				'phyVertex.x = aFloat16.TheFloatValue
				'aFloat16 = New SourceFloat16
				'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
				'phyVertex.y = aFloat16.TheFloatValue
				'aFloat16 = New SourceFloat16
				'aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
				'phyVertex.z = aFloat16.TheFloatValue
				''aFloat16 = New SourceFloat16
				''aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
				''phyVertex.x2 = aFloat16.TheFloatValue
				''aFloat16 = New SourceFloat16
				''aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
				''phyVertex.y2 = aFloat16.TheFloatValue
				''aFloat16 = New SourceFloat16
				''aFloat16.the16BitValue = Me.theInputFileReader.ReadUInt16()
				''phyVertex.z2 = aFloat16.TheFloatValue
				'Me.theInputFileReader.ReadUInt16()
				'Me.theInputFileReader.ReadUInt16()
				'Me.theInputFileReader.ReadUInt16()
				'Me.theInputFileReader.ReadSingle()

				'w = Me.theInputFileReader.ReadSingle()
				'phyVertex.x /= w
				'phyVertex.y /= w
				'phyVertex.z /= w
				'phyVertex.x -= w
				'phyVertex.y -= w
				'phyVertex.z -= w
				'------
				'w = Me.theInputFileReader.ReadInt16()
				'Me.theInputFileReader.ReadInt16()
				'phyVertex.x /= w
				'phyVertex.y /= w
				'phyVertex.z /= w

				collisionData.theVertices.Add(phyVertex)
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
		tempInt = Me.theInputFileReader.ReadUInt16()
		tempInt = Me.theInputFileReader.ReadUInt16()

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
		Dim faceSection As SourcePhyFaceSection

		If Me.thePhyFileData.theSourcePhyCollisionDatas IsNot Nothing Then
			For collisionDataIndex As Integer = 0 To Me.thePhyFileData.theSourcePhyCollisionDatas.Count - 1
				collisionData = Me.thePhyFileData.theSourcePhyCollisionDatas(collisionDataIndex)

				For faceSectionIndex As Integer = 0 To collisionData.theFaceSections.Count - 1
					faceSection = collisionData.theFaceSections(faceSectionIndex)

					For triangleIndex As Integer = 0 To faceSection.theFaces.Count - 1
						aTriangle = faceSection.theFaces(triangleIndex)

						Me.CalculateFaceNormal(collisionData, aTriangle)
					Next
				Next
			Next
		End If
	End Sub

	Public Sub ReadSourcePhysCollisionModels()
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
							'aSourcePhysCollisionModel.theIndex = CInt(value)
							aSourcePhysCollisionModel.theIndex = Integer.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "name" Then
							aSourcePhysCollisionModel.theName = value
						ElseIf key = "parent" Then
							aSourcePhysCollisionModel.theParentIsValid = True
							aSourcePhysCollisionModel.theParentName = value
						ElseIf key = "mass" Then
							'aSourcePhysCollisionModel.theMass = CSng(value)
							aSourcePhysCollisionModel.theMass = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "surfaceprop" Then
							aSourcePhysCollisionModel.theSurfaceProp = value
						ElseIf key = "damping" Then
							'aSourcePhysCollisionModel.theDamping = CSng(value)
							aSourcePhysCollisionModel.theDamping = Single.Parse(value, TheApp.InternalNumberFormat)
							If Me.theDampingToCountMap.ContainsKey(aSourcePhysCollisionModel.theDamping) Then
								Me.theDampingToCountMap(aSourcePhysCollisionModel.theDamping) += 1
							Else
								Me.theDampingToCountMap.Add(aSourcePhysCollisionModel.theDamping, 1)
							End If
						ElseIf key = "rotdamping" Then
							'aSourcePhysCollisionModel.theRotDamping = CSng(value)
							aSourcePhysCollisionModel.theRotDamping = Single.Parse(value, TheApp.InternalNumberFormat)
							If Me.theRotDampingToCountMap.ContainsKey(aSourcePhysCollisionModel.theRotDamping) Then
								Me.theRotDampingToCountMap(aSourcePhysCollisionModel.theRotDamping) += 1
							Else
								Me.theRotDampingToCountMap.Add(aSourcePhysCollisionModel.theRotDamping, 1)
							End If
						ElseIf key = "drag" Then
							aSourcePhysCollisionModel.theDragCoefficientIsValid = True
							'aSourcePhysCollisionModel.theDragCoefficient = CSng(value)
							aSourcePhysCollisionModel.theDragCoefficient = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "rollingDrag" Then
							aSourcePhysCollisionModel.theRollingDragCoefficientIsValid = True
							aSourcePhysCollisionModel.theRollingDragCoefficient = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "inertia" Then
							'aSourcePhysCollisionModel.theInertia = CSng(value)
							aSourcePhysCollisionModel.theInertia = Single.Parse(value, TheApp.InternalNumberFormat)
							If Me.theInertiaToCountMap.ContainsKey(aSourcePhysCollisionModel.theInertia) Then
								Me.theInertiaToCountMap(aSourcePhysCollisionModel.theInertia) += 1
							Else
								Me.theInertiaToCountMap.Add(aSourcePhysCollisionModel.theInertia, 1)
							End If
						ElseIf key = "volume" Then
							'aSourcePhysCollisionModel.theVolume = CSng(value)
							aSourcePhysCollisionModel.theVolume = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "massbias" Then
							aSourcePhysCollisionModel.theMassBiasIsValid = True
							'aSourcePhysCollisionModel.theMassBias = CSng(value)
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
		Catch
			Throw
			'Finally
		End Try
	End Sub

	Public Sub ReadSourcePhyRagdollConstraintDescs()
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
							'aSourceRagdollConstraintDesc.theParentIndex = CInt(value)
							aSourceRagdollConstraintDesc.theParentIndex = Integer.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "child" Then
							'aSourceRagdollConstraintDesc.theChildIndex = CInt(value)
							aSourceRagdollConstraintDesc.theChildIndex = Integer.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "xmin" Then
							'aSourceRagdollConstraintDesc.theXMin = CSng(value)
							aSourceRagdollConstraintDesc.theXMin = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "xmax" Then
							'aSourceRagdollConstraintDesc.theXMax = CSng(value)
							aSourceRagdollConstraintDesc.theXMax = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "xfriction" Then
							'aSourceRagdollConstraintDesc.theXFriction = CSng(value)
							aSourceRagdollConstraintDesc.theXFriction = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "ymin" Then
							'aSourceRagdollConstraintDesc.theYMin = CSng(value)
							aSourceRagdollConstraintDesc.theYMin = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "ymax" Then
							'aSourceRagdollConstraintDesc.theYMax = CSng(value)
							aSourceRagdollConstraintDesc.theYMax = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "yfriction" Then
							'aSourceRagdollConstraintDesc.theYFriction = CSng(value)
							aSourceRagdollConstraintDesc.theYFriction = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "zmin" Then
							'aSourceRagdollConstraintDesc.theZMin = CSng(value)
							aSourceRagdollConstraintDesc.theZMin = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "zmax" Then
							'aSourceRagdollConstraintDesc.theZMax = CSng(value)
							aSourceRagdollConstraintDesc.theZMax = Single.Parse(value, TheApp.InternalNumberFormat)
						ElseIf key = "zfriction" Then
							'aSourceRagdollConstraintDesc.theZFriction = CSng(value)
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
		Catch
		Finally
		End Try
	End Sub

	Public Sub ReadSourcePhyCollisionRules()
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
		Catch
		Finally
		End Try
	End Sub

	Public Sub ReadSourcePhyEditParamsSection()
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
				Dim thelist As List(Of String)
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
						ElseIf key = "jointmerge" Then
							tokens = value.Split(delimiters)

							If Me.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap Is Nothing Then
								Me.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap = New Dictionary(Of String, List(Of String))()
							End If
							If Not Me.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap.ContainsKey(tokens(0)) Then
								thelist = New List(Of String)()
								Me.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap.Add(tokens(0), thelist)
							Else
								thelist = Me.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap(tokens(0))
							End If
							thelist.Add(tokens(1))
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
	End Sub

	Public Sub ReadCollisionTextSection()
		'Dim streamLastPosition As Long

		'Try
		'	streamLastPosition = Me.theInputFileReader.BaseStream.Length() - 1

		'	If streamLastPosition > Me.theInputFileReader.BaseStream.Position Then
		'	'NOTE: Use -1 to drop the null terminator character.
		'	Me.thePhyFileData.theSourcePhyCollisionText = Me.theInputFileReader.ReadChars(CInt(streamLastPosition - Me.theInputFileReader.BaseStream.Position - 1))
		'	End If
		'Catch ex As Exception
		'	Dim debug As Integer = 4242
		'End Try

		Me.thePhyFileData.theSourcePhyCollisionText = Common.ReadPhyCollisionTextSection(Me.theInputFileReader, Me.theInputFileReader.BaseStream.Length() - 1)
	End Sub

	Public Sub ReadUnreadBytes()
		Me.thePhyFileData.theFileSeekLog.LogUnreadBytes(Me.theInputFileReader)
	End Sub

#End Region

#Region "Private Methods"

	'void calcNormal(float v[3][3], float out[3])
	'    float v1[3],v2[3];						// Vector 1 (x,y,z) & Vector 2 (x,y,z)
	'    static const int x = 0;						// Define X Coord
	'    static const int y = 1;						// Define Y Coord
	'    static const int z = 2;						// Define Z Coord

	'    // Finds The Vector Between 2 Points By Subtracting
	'    // The x,y,z Coordinates From One Point To Another.

	'    // Calculate The Vector From Point 1 To Point 0
	'    v1[x] = v[0][x] - v[1][x];					// Vector 1.x=Vertex[0].x-Vertex[1].x
	'    v1[y] = v[0][y] - v[1][y];					// Vector 1.y=Vertex[0].y-Vertex[1].y
	'    v1[z] = v[0][z] - v[1][z];					// Vector 1.z=Vertex[0].y-Vertex[1].z
	'    // Calculate The Vector From Point 2 To Point 1
	'    v2[x] = v[1][x] - v[2][x];					// Vector 2.x=Vertex[0].x-Vertex[1].x
	'    v2[y] = v[1][y] - v[2][y];					// Vector 2.y=Vertex[0].y-Vertex[1].y
	'    v2[z] = v[1][z] - v[2][z];					// Vector 2.z=Vertex[0].z-Vertex[1].z
	'    // Compute The Cross Product To Give Us A Surface Normal
	'    out[x] = v1[y]*v2[z] - v1[z]*v2[y];				// Cross Product For Y - Z
	'    out[y] = v1[z]*v2[x] - v1[x]*v2[z];				// Cross Product For X - Z
	'    out[z] = v1[x]*v2[y] - v1[y]*v2[x];				// Cross Product For X - Y

	'    ReduceToUnit(out);						// Normalize The Vectors
	Private Sub CalculateFaceNormal(ByVal collisionData As SourcePhyCollisionData, ByVal aTriangle As SourcePhyFace)
		Dim vertex(3) As SourceVector
		Dim vector0 As New SourceVector()
		Dim vector1 As New SourceVector()
		Dim normalVector As New SourceVector()

		For vertexIndex As Integer = 0 To 2
			vertex(vertexIndex) = collisionData.theVertices(aTriangle.vertexIndex(vertexIndex)).vertex
		Next

		vector0.x = vertex(0).x - vertex(1).x
		vector0.y = vertex(0).y - vertex(1).y
		vector0.z = vertex(0).y - vertex(1).z

		vector1.x = vertex(1).x - vertex(2).x
		vector1.y = vertex(1).y - vertex(2).y
		vector1.z = vertex(1).z - vertex(2).z
		'NOTE: Cra0kalo suggested this, but it produces same result as above.
		'vector1.x = vertex(2).x - vertex(1).x
		'vector1.y = vertex(2).y - vertex(1).y
		'vector1.z = vertex(2).z - vertex(1).z

		normalVector = vector0.CrossProduct(vector1)
		normalVector = normalVector.Normalize()

		For vertexIndex As Integer = 0 To 2
			collisionData.theVertices(aTriangle.vertexIndex(vertexIndex)).Normal.x = normalVector.x
			collisionData.theVertices(aTriangle.vertexIndex(vertexIndex)).Normal.y = normalVector.y
			collisionData.theVertices(aTriangle.vertexIndex(vertexIndex)).Normal.z = normalVector.z
		Next
	End Sub

#End Region

#Region "Data"

	Private theInputFileReader As BinaryReader
	Private thePhyFileData As SourcePhyFileData

	Private theDampingToCountMap As SortedList(Of Single, Integer)
	Private theInertiaToCountMap As SortedList(Of Single, Integer)
	Private theRotDampingToCountMap As SortedList(Of Single, Integer)

#End Region

End Class
