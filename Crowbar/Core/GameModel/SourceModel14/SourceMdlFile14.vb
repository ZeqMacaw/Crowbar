Imports System.IO

Public Class SourceMdlFile14

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlFileReader As BinaryReader, ByVal mdlFileData As SourceMdlFileData14)
		Me.theInputFileReader = mdlFileReader
		Me.theMdlFileData = mdlFileData

		Me.theMdlFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length
	End Sub

	Public Sub New(ByVal mdlFileWriter As BinaryWriter, ByVal mdlFileData As SourceMdlFileData14)
		Me.theOutputFileWriter = mdlFileWriter
		Me.theMdlFileData = mdlFileData
	End Sub

#End Region

#Region "Methods"

	Public Sub ReadMdlHeader()
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theMdlFileData.id = Me.theInputFileReader.ReadChars(4)
		Me.theMdlFileData.theID = Me.theMdlFileData.id
		Me.theMdlFileData.version = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.name = Me.theInputFileReader.ReadChars(64)
		Me.theMdlFileData.theModelName = CStr(Me.theMdlFileData.name).Trim(Chr(0))

		Me.theMdlFileData.fileSize = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.theActualFileSize = Me.theInputFileReader.BaseStream.Length

		Me.theMdlFileData.eyePosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.eyePosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.eyePosition.z = Me.theInputFileReader.ReadSingle()

		Me.theMdlFileData.hullMinPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMinPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMinPosition.z = Me.theInputFileReader.ReadSingle()

		Me.theMdlFileData.hullMaxPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMaxPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMaxPosition.z = Me.theInputFileReader.ReadSingle()

		Me.theMdlFileData.viewBoundingBoxMinPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMinPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMinPosition.z = Me.theInputFileReader.ReadSingle()

		Me.theMdlFileData.viewBoundingBoxMaxPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMaxPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMaxPosition.z = Me.theInputFileReader.ReadSingle()

		Me.theMdlFileData.flags = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.boneCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.boneControllerCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneControllerOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.hitboxCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.hitboxOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.sequenceCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.sequenceOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.sequenceGroupCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.sequenceGroupOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.textureCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.textureOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.textureDataOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.skinReferenceCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinFamilyCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.bodyPartCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.bodyPartOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.attachmentCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.attachmentOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.soundTable = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.soundOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.soundGroups = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.soundGroupOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.transitionCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.transitionOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.unknown01 = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.subModelCount = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.vertexCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.indexCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.indexOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.vertexOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.normalOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.uvOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.unknown08 = Me.theInputFileReader.ReadInt32()
		'Me.theMdlFileData.unknown09 = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.weightingWeightOffset = Me.theInputFileReader.ReadInt32()
		'Me.theMdlFileData.unknown10 = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.weightingBoneOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.unknown11 = Me.theInputFileReader.ReadInt32()

		For x As Integer = 0 To Me.theMdlFileData.subModelOffsets.Length - 1
			Me.theMdlFileData.subModelOffsets(x) = Me.theInputFileReader.ReadInt32()
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "MDL File Header")

		'If Me.theMdlFileData.bodyPartCount = 0 AndAlso Me.theMdlFileData.localSequenceCount > 0 Then
		'	Me.theMdlFileData.theMdlFileOnlyHasAnimations = True
		'End If
	End Sub

	Public Sub ReadBones()
		If Me.theMdlFileData.boneCount > 0 Then
			'Dim boneInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theBones = New List(Of SourceMdlBone10)(Me.theMdlFileData.boneCount)
				For boneIndex As Integer = 0 To Me.theMdlFileData.boneCount - 1
					'boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBone As New SourceMdlBone10()

					aBone.name = Me.theInputFileReader.ReadChars(32)
					aBone.theName = aBone.name
					aBone.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aBone.theName)
					aBone.parentBoneIndex = Me.theInputFileReader.ReadInt32()
					aBone.flags = Me.theInputFileReader.ReadInt32()
					For boneControllerIndexIndex As Integer = 0 To aBone.boneControllerIndex.Length - 1
						aBone.boneControllerIndex(boneControllerIndexIndex) = Me.theInputFileReader.ReadInt32()
					Next
					aBone.position = New SourceVector()
					aBone.position.x = Me.theInputFileReader.ReadSingle()
					aBone.position.y = Me.theInputFileReader.ReadSingle()
					aBone.position.z = Me.theInputFileReader.ReadSingle()
					aBone.rotation = New SourceVector()
					aBone.rotation.x = Me.theInputFileReader.ReadSingle()
					aBone.rotation.y = Me.theInputFileReader.ReadSingle()
					aBone.rotation.z = Me.theInputFileReader.ReadSingle()
					aBone.positionScale = New SourceVector()
					aBone.positionScale.x = Me.theInputFileReader.ReadSingle()
					aBone.positionScale.y = Me.theInputFileReader.ReadSingle()
					aBone.positionScale.z = Me.theInputFileReader.ReadSingle()
					aBone.rotationScale = New SourceVector()
					aBone.rotationScale.x = Me.theInputFileReader.ReadSingle()
					aBone.rotationScale.y = Me.theInputFileReader.ReadSingle()
					aBone.rotationScale.z = Me.theInputFileReader.ReadSingle()

					Me.theMdlFileData.theBones.Add(aBone)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones " + Me.theMdlFileData.theBones.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadBoneControllers()
		If Me.theMdlFileData.boneControllerCount > 0 Then
			'Dim boneControllerInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneControllerOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theBoneControllers = New List(Of SourceMdlBoneController10)(Me.theMdlFileData.boneControllerCount)
			For boneControllerIndex As Integer = 0 To Me.theMdlFileData.boneControllerCount - 1
				'boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aBoneController As New SourceMdlBoneController10()

				aBoneController.boneIndex = Me.theInputFileReader.ReadInt32()
				aBoneController.type = Me.theInputFileReader.ReadInt32()
				aBoneController.startAngleDegrees = Me.theInputFileReader.ReadSingle()
				aBoneController.endAngleDegrees = Me.theInputFileReader.ReadSingle()
				aBoneController.restIndex = Me.theInputFileReader.ReadInt32()
				aBoneController.index = Me.theInputFileReader.ReadInt32()

				Me.theMdlFileData.theBoneControllers.Add(aBoneController)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers " + Me.theMdlFileData.theBoneControllers.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment")
		End If
	End Sub

	Public Sub ReadAttachments()
		If Me.theMdlFileData.attachmentCount > 0 Then
			'Dim attachmentInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.attachmentOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theAttachments = New List(Of SourceMdlAttachment10)(Me.theMdlFileData.attachmentCount)
			For attachmentIndex As Integer = 0 To Me.theMdlFileData.attachmentCount - 1
				'attachmentInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anAttachment As New SourceMdlAttachment10()

				anAttachment.name = Me.theInputFileReader.ReadChars(32)
				anAttachment.theName = anAttachment.name
				anAttachment.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(anAttachment.theName)
				anAttachment.type = Me.theInputFileReader.ReadInt32()
				anAttachment.boneIndex = Me.theInputFileReader.ReadInt32()

				anAttachment.attachmentPoint = New SourceVector()
				anAttachment.attachmentPoint.x = Me.theInputFileReader.ReadSingle()
				anAttachment.attachmentPoint.y = Me.theInputFileReader.ReadSingle()
				anAttachment.attachmentPoint.z = Me.theInputFileReader.ReadSingle()
				For x As Integer = 0 To 2
					anAttachment.vectors(x) = New SourceVector()
					anAttachment.vectors(x).x = Me.theInputFileReader.ReadSingle()
					anAttachment.vectors(x).y = Me.theInputFileReader.ReadSingle()
					anAttachment.vectors(x).z = Me.theInputFileReader.ReadSingle()
				Next

				Me.theMdlFileData.theAttachments.Add(anAttachment)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments " + Me.theMdlFileData.theAttachments.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment")
		End If
	End Sub

	Public Sub ReadHitboxes()
		If Me.theMdlFileData.hitboxCount > 0 Then
			'Dim hitboxInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.hitboxOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theHitboxes = New List(Of SourceMdlHitbox10)(Me.theMdlFileData.hitboxCount)
			For hitboxIndex As Integer = 0 To Me.theMdlFileData.hitboxCount - 1
				'hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aHitbox As New SourceMdlHitbox10()

				aHitbox.boneIndex = Me.theInputFileReader.ReadInt32()
				aHitbox.groupIndex = Me.theInputFileReader.ReadInt32()
				aHitbox.boundingBoxMin.x = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMin.y = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMin.z = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMax.x = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMax.y = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMax.z = Me.theInputFileReader.ReadSingle()

				Me.theMdlFileData.theHitboxes.Add(aHitbox)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet.theHitboxes " + Me.theMdlFileData.theHitboxes.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
		End If
	End Sub

	Public Sub ReadSequences()
		If Me.theMdlFileData.sequenceCount > 0 Then
			'Dim sequenceInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.sequenceOffset, SeekOrigin.Begin)
				Me.theMdlFileData.theSequences = New List(Of SourceMdlSequenceDesc10)(Me.theMdlFileData.sequenceCount)
				For sequenceIndex As Integer = 0 To Me.theMdlFileData.sequenceCount - 1
					'sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aSequence As New SourceMdlSequenceDesc10()

					fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aSequence.name = Me.theInputFileReader.ReadChars(32)
					aSequence.theName = aSequence.name
					aSequence.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aSequence.theName)

					aSequence.fps = Me.theInputFileReader.ReadSingle()

					aSequence.flags = Me.theInputFileReader.ReadInt32()
					aSequence.activityId = Me.theInputFileReader.ReadInt32()
					aSequence.activityWeight = Me.theInputFileReader.ReadInt32()
					aSequence.eventCount = Me.theInputFileReader.ReadInt32()
					aSequence.eventOffset = Me.theInputFileReader.ReadInt32()
					aSequence.frameCount = Me.theInputFileReader.ReadInt32()
					aSequence.pivotCount = Me.theInputFileReader.ReadInt32()
					aSequence.pivotOffset = Me.theInputFileReader.ReadInt32()

					aSequence.motiontype = Me.theInputFileReader.ReadInt32()
					aSequence.motionbone = Me.theInputFileReader.ReadInt32()
					aSequence.linearmovement.x = Me.theInputFileReader.ReadSingle()
					aSequence.linearmovement.y = Me.theInputFileReader.ReadSingle()
					aSequence.linearmovement.z = Me.theInputFileReader.ReadSingle()
					aSequence.automoveposindex = Me.theInputFileReader.ReadInt32()
					aSequence.automoveangleindex = Me.theInputFileReader.ReadInt32()

					aSequence.bbMin.x = Me.theInputFileReader.ReadSingle()
					aSequence.bbMin.y = Me.theInputFileReader.ReadSingle()
					aSequence.bbMin.z = Me.theInputFileReader.ReadSingle()
					aSequence.bbMax.x = Me.theInputFileReader.ReadSingle()
					aSequence.bbMax.y = Me.theInputFileReader.ReadSingle()
					aSequence.bbMax.z = Me.theInputFileReader.ReadSingle()

					aSequence.blendCount = Me.theInputFileReader.ReadInt32()
					aSequence.theSmdRelativePathFileNames = New List(Of String)(aSequence.blendCount)
					For i As Integer = 0 To aSequence.blendCount - 1
						aSequence.theSmdRelativePathFileNames.Add("")
					Next

					aSequence.animOffset = Me.theInputFileReader.ReadInt32()

					For x As Integer = 0 To aSequence.blendType.Length - 1
						aSequence.blendType(x) = Me.theInputFileReader.ReadInt32()
					Next
					For x As Integer = 0 To aSequence.blendStart.Length - 1
						aSequence.blendStart(x) = Me.theInputFileReader.ReadSingle()
					Next
					For x As Integer = 0 To aSequence.blendEnd.Length - 1
						aSequence.blendEnd(x) = Me.theInputFileReader.ReadSingle()
					Next
					aSequence.blendParent = Me.theInputFileReader.ReadInt32()

					aSequence.groupIndex = Me.theInputFileReader.ReadInt32()
					aSequence.entryNodeIndex = Me.theInputFileReader.ReadInt32()
					aSequence.exitNodeIndex = Me.theInputFileReader.ReadInt32()
					aSequence.nodeFlags = Me.theInputFileReader.ReadInt32()
					aSequence.nextSeq = Me.theInputFileReader.ReadInt32()

					'TODO: unknown bytes
					Me.theInputFileReader.ReadInt32()
					Me.theInputFileReader.ReadInt32()
					Me.theInputFileReader.ReadInt32()

					Me.theMdlFileData.theSequences.Add(aSequence)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence [" + aSequence.theName + "]")

					'Me.ReadEvents(aSequence)
					'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSequence.theEvents alignment")

					'Me.ReadPivots(aSequence)
					'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSequence.thePivots alignment")
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadSequenceGroups()
		If Me.theMdlFileData.sequenceGroupCount > 0 Then
			'Dim boneInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.sequenceGroupOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theSequenceGroupFileHeaders = New List(Of SourceMdlSequenceGroupFileHeader10)(Me.theMdlFileData.sequenceGroupCount)
				Me.theMdlFileData.theSequenceGroups = New List(Of SourceMdlSequenceGroup10)(Me.theMdlFileData.sequenceGroupCount)
				For sequenceGroupIndex As Integer = 0 To Me.theMdlFileData.sequenceGroupCount - 1
					Dim aSequenceGroupFileHeader As New SourceMdlSequenceGroupFileHeader10()
					Me.theMdlFileData.theSequenceGroupFileHeaders.Add(aSequenceGroupFileHeader)

					'boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aSequenceGroup As New SourceMdlSequenceGroup10()

					aSequenceGroup.name = Me.theInputFileReader.ReadChars(32)
					aSequenceGroup.theName = CStr(aSequenceGroup.name).Trim(Chr(0))
					aSequenceGroup.fileName = Me.theInputFileReader.ReadChars(64)
					aSequenceGroup.theFileName = CStr(aSequenceGroup.fileName).Trim(Chr(0))
					aSequenceGroup.cacheOffset = Me.theInputFileReader.ReadInt32()
					aSequenceGroup.data = Me.theInputFileReader.ReadInt32()

					Me.theMdlFileData.theSequenceGroups.Add(aSequenceGroup)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceGroups " + Me.theMdlFileData.theSequenceGroups.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSequenceGroups alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadTransitions()
		If Me.theMdlFileData.transitionCount > 0 Then
			'Dim boneInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.transitionOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theTransitions = New List(Of List(Of Byte))(Me.theMdlFileData.transitionCount)
				For entryNodeIndex As Integer = 0 To Me.theMdlFileData.transitionCount - 1
					Dim exitNodeTransitions As New List(Of Byte)(Me.theMdlFileData.transitionCount)
					For exitNodeIndex As Integer = 0 To Me.theMdlFileData.transitionCount - 1
						Dim aTransitionValue As Byte
						aTransitionValue = Me.theInputFileReader.ReadByte()
						exitNodeTransitions.Add(aTransitionValue)
					Next
					Me.theMdlFileData.theTransitions.Add(exitNodeTransitions)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTransitions " + Me.theMdlFileData.theTransitions.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTransitions alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadAnimations(ByVal sequenceGroupIndex As Integer)
		If Me.theMdlFileData.theSequences IsNot Nothing Then
			Dim animationInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long
			Dim aSequence As SourceMdlSequenceDesc10
			Dim animationValuesEndInputFileStreamPosition As Long

			Try
				For sequenceIndex As Integer = 0 To Me.theMdlFileData.theSequences.Count - 1
					aSequence = Me.theMdlFileData.theSequences(sequenceIndex)
					animationValuesEndInputFileStreamPosition = 0

					If aSequence.groupIndex <> sequenceGroupIndex Then
						Continue For
					End If

					Me.theInputFileReader.BaseStream.Seek(aSequence.animOffset, SeekOrigin.Begin)
					'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aSequence.theAnimations = New List(Of SourceMdlAnimation10)(aSequence.blendCount * Me.theMdlFileData.theBones.Count)
					For blendIndex As Integer = 0 To aSequence.blendCount - 1
						For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
							animationInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
							fileOffsetStart = Me.theInputFileReader.BaseStream.Position
							Dim anAnimation As New SourceMdlAnimation10()

							For offsetIndex As Integer = 0 To anAnimation.animationValueOffsets.Length - 1
								anAnimation.animationValueOffsets(offsetIndex) = Me.theInputFileReader.ReadUInt16()

								If anAnimation.animationValueOffsets(offsetIndex) > 0 Then
									inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

									Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.animationValueOffsets(offsetIndex), aSequence.frameCount, anAnimation.theAnimationValues(offsetIndex))
									animationValuesEndInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

									Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
								End If
							Next
							aSequence.theAnimations.Add(anAnimation)

							fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation")
						Next
					Next

					If animationValuesEndInputFileStreamPosition > 0 Then
						inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, animationValuesEndInputFileStreamPosition - 1, 4, "aSequence.theAnimations - End of AnimationValues alignment")

						Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					End If

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence.theAnimations")

					Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSequence.theAnimations alignment")
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadBodyParts()
		If Me.theMdlFileData.bodyPartCount > 0 Then
			Dim bodyPartInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long
			Dim modelsEndInputFileStreamPosition As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.bodyPartOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				modelsEndInputFileStreamPosition = 0

				Me.theMdlFileData.theBodyParts = New List(Of SourceMdlBodyPart14)(Me.theMdlFileData.bodyPartCount)
				For bodyPartIndex As Integer = 0 To Me.theMdlFileData.bodyPartCount - 1
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBodyPart As New SourceMdlBodyPart14()

					aBodyPart.name = Me.theInputFileReader.ReadChars(64)
					aBodyPart.theName = CStr(aBodyPart.name).Trim(Chr(0))
					aBodyPart.modelCount = Me.theInputFileReader.ReadInt32()
					aBodyPart.base = Me.theInputFileReader.ReadInt32()
					aBodyPart.modelOffset = Me.theInputFileReader.ReadInt32()

					Me.theMdlFileData.theBodyParts.Add(aBodyPart)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadModels(aBodyPart)
					'If bodyPartIndex = Me.theMdlFileData.bodyPartCount - 1 Then
					'	modelsEndInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					'End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart [" + aBodyPart.theName + "]")
				Next

				'If modelsEndInputFileStreamPosition > 0 Then
				'	inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'	Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, modelsEndInputFileStreamPosition - 1, 4, "theMdlFileData.theBodyParts - End of Models alignment")

				'	Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				'End If

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadIndexes()
		If Me.theMdlFileData.indexCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.indexOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theIndexes = New List(Of UInt16)(Me.theMdlFileData.indexCount)
				For i As Integer = 0 To Me.theMdlFileData.indexCount - 1
					Dim index As New UInt16()

					index = Me.theInputFileReader.ReadUInt16()

					Me.theMdlFileData.theIndexes.Add(index)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIndexes " + Me.theMdlFileData.theIndexes.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadVertexes()
		If Me.theMdlFileData.vertexCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.vertexOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Dim unused As Double
				Me.theMdlFileData.theVertexes = New List(Of SourceVector)(Me.theMdlFileData.vertexCount)
				For vertexIndex As Integer = 0 To Me.theMdlFileData.vertexCount - 1
					Dim vertex As New SourceVector()

					vertex.x = Me.theInputFileReader.ReadSingle()
					vertex.y = Me.theInputFileReader.ReadSingle()
					vertex.z = Me.theInputFileReader.ReadSingle()
					unused = Me.theInputFileReader.ReadSingle()

					Me.theMdlFileData.theVertexes.Add(vertex)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theVertexes " + Me.theMdlFileData.theVertexes.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadNormals()
		If Me.theMdlFileData.vertexCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.normalOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Dim unused As Double
				Me.theMdlFileData.theNormals = New List(Of SourceVector)(Me.theMdlFileData.vertexCount)
				For normalIndex As Integer = 0 To Me.theMdlFileData.vertexCount - 1
					Dim normal As New SourceVector()

					normal.x = Me.theInputFileReader.ReadSingle()
					normal.y = Me.theInputFileReader.ReadSingle()
					normal.z = Me.theInputFileReader.ReadSingle()
					unused = Me.theInputFileReader.ReadSingle()

					Me.theMdlFileData.theNormals.Add(normal)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theNormals " + Me.theMdlFileData.theNormals.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadUVs()
		If Me.theMdlFileData.vertexCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.uvOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theUVs = New List(Of SourceVector)(Me.theMdlFileData.vertexCount)
				For uvIndex As Integer = 0 To Me.theMdlFileData.vertexCount - 1
					Dim uv As New SourceVector()

					uv.x = Me.theInputFileReader.ReadSingle()
					uv.y = Me.theInputFileReader.ReadSingle()

					Me.theMdlFileData.theUVs.Add(uv)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theUVs " + Me.theMdlFileData.theUVs.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadWeightingWeights()
		If Me.theMdlFileData.vertexCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.weightingWeightOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theWeightings = New List(Of SourceMdlWeighting14)(Me.theMdlFileData.vertexCount)
				For weightingIndex As Integer = 0 To Me.theMdlFileData.vertexCount - 1
					Dim weighting As New SourceMdlWeighting14()

					For x As Integer = 0 To 3
						weighting.weights(x) = Me.theInputFileReader.ReadSingle()
					Next

					Me.theMdlFileData.theWeightings.Add(weighting)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theWeights.weights " + Me.theMdlFileData.theWeightings.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadWeightingBones()
		If Me.theMdlFileData.theWeightings.Count > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.weightingBoneOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				For Each weighting As SourceMdlWeighting14 In Me.theMdlFileData.theWeightings
					weighting.boneCount = 0
					For x As Integer = 0 To 3
						weighting.bones(x) = Me.theInputFileReader.ReadByte()
						If weighting.bones(x) <> &HFF Then
							weighting.boneCount += 1
						End If
					Next
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theWeightings.bones " + Me.theMdlFileData.theWeightings.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadTextures()
		If Me.theMdlFileData.textureCount > 0 Then
			'Dim boneInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.textureOffset, SeekOrigin.Begin)

				Me.theMdlFileData.theTextures = New List(Of SourceMdlTexture14)(Me.theMdlFileData.textureCount)
				For textureIndex As Integer = 0 To Me.theMdlFileData.textureCount - 1
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					'boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aTexture As New SourceMdlTexture14()

					aTexture.fileName = Me.theInputFileReader.ReadChars(64)
					aTexture.theFileName = CStr(aTexture.fileName).Trim(Chr(0))
					aTexture.textureName = Me.theInputFileReader.ReadChars(64)
					aTexture.theTextureName = CStr(aTexture.textureName).Trim(Chr(0))
					aTexture.flags = Me.theInputFileReader.ReadInt32()
					aTexture.width = Me.theInputFileReader.ReadUInt32()
					aTexture.height = Me.theInputFileReader.ReadUInt32()
					aTexture.dataOffset = Me.theInputFileReader.ReadUInt32()

					''TODO: Unknown bytes.
					'Me.theInputFileReader.ReadUInt32()
					'Me.theInputFileReader.ReadUInt32()
					'Me.theInputFileReader.ReadUInt32()
					'Me.theInputFileReader.ReadUInt32()

					Me.theMdlFileData.theTextures.Add(aTexture)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture [" + aTexture.theFileName + "][" + aTexture.theTextureName + "]")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.ReadTextureData(aTexture)
					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aTexture.theData alignment")

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadSkins()
		If Me.theMdlFileData.skinFamilyCount > 0 AndAlso Me.theMdlFileData.skinReferenceCount > 0 Then
			'Dim boneInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long
			Dim aSkinRef As Short

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.skinOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theSkinFamilies = New List(Of List(Of Short))(Me.theMdlFileData.skinFamilyCount)
				For skinFamilyIndex As Integer = 0 To Me.theMdlFileData.skinFamilyCount - 1
					'boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aSkinFamily As New List(Of Short)()

					For skinRefIndex As Integer = 0 To Me.theMdlFileData.skinReferenceCount - 1
						aSkinRef = Me.theInputFileReader.ReadInt16()
						aSkinFamily.Add(aSkinRef)
					Next

					Me.theMdlFileData.theSkinFamilies.Add(aSkinFamily)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies " + Me.theMdlFileData.theSkinFamilies.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadSequenceGroupMdlHeader()
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theMdlFileData.id = Me.theInputFileReader.ReadChars(4)
		Me.theMdlFileData.theID = Me.theMdlFileData.id
		Me.theMdlFileData.version = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.name = Me.theInputFileReader.ReadChars(64)
		Me.theMdlFileData.theModelName = CStr(Me.theMdlFileData.name).Trim(Chr(0))

		Me.theMdlFileData.fileSize = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.theActualFileSize = Me.theInputFileReader.BaseStream.Length

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "SequenceGroupMDL File Header")
	End Sub

	Public Sub ReadUnreadBytes()
		Me.theMdlFileData.theFileSeekLog.LogUnreadBytes(Me.theInputFileReader)
	End Sub

	Public Sub BuildBoneTransforms()
		Me.theMdlFileData.theBoneTransforms = New List(Of SourceBoneTransform10)(Me.theMdlFileData.theBones.Count)
		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			Dim aBone As SourceMdlBone10
			Dim boneTransform As New SourceBoneTransform10()
			Dim parentBoneIndex As Integer

			aBone = Me.theMdlFileData.theBones(boneIndex)

			Dim boneMatrixColumn0 As New SourceVector()
			Dim boneMatrixColumn1 As New SourceVector()
			Dim boneMatrixColumn2 As New SourceVector()
			Dim boneMatrixColumn3 As New SourceVector()
			'MathModule.AngleMatrix(aBone.rotation.x, aBone.rotation.y, aBone.rotation.z, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
			'MathModule.AngleMatrix(aBone.rotation.z, aBone.rotation.x, aBone.rotation.y, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
			MathModule.AngleMatrix(aBone.rotation.y, aBone.rotation.z, aBone.rotation.x, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)

			boneMatrixColumn3.x = aBone.position.x
			boneMatrixColumn3.y = aBone.position.y
			boneMatrixColumn3.z = aBone.position.z

			parentBoneIndex = Me.theMdlFileData.theBones(boneIndex).parentBoneIndex
			If parentBoneIndex = -1 Then
				boneTransform.matrixColumn0.x = boneMatrixColumn0.x
				boneTransform.matrixColumn0.y = boneMatrixColumn0.y
				boneTransform.matrixColumn0.z = boneMatrixColumn0.z
				boneTransform.matrixColumn1.x = boneMatrixColumn1.x
				boneTransform.matrixColumn1.y = boneMatrixColumn1.y
				boneTransform.matrixColumn1.z = boneMatrixColumn1.z
				boneTransform.matrixColumn2.x = boneMatrixColumn2.x
				boneTransform.matrixColumn2.y = boneMatrixColumn2.y
				boneTransform.matrixColumn2.z = boneMatrixColumn2.z
				boneTransform.matrixColumn3.x = boneMatrixColumn3.x
				boneTransform.matrixColumn3.y = boneMatrixColumn3.y
				boneTransform.matrixColumn3.z = boneMatrixColumn3.z
			Else
				Dim parentBoneTransform As SourceBoneTransform10
				parentBoneTransform = Me.theMdlFileData.theBoneTransforms(parentBoneIndex)

				'			R_ConcatTransforms( g_bonetransform[pbones[i].parent], bonematrix, g_bonetransform[i] );
				MathModule.R_ConcatTransforms(parentBoneTransform.matrixColumn0, parentBoneTransform.matrixColumn1, parentBoneTransform.matrixColumn2, parentBoneTransform.matrixColumn3, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3, boneTransform.matrixColumn0, boneTransform.matrixColumn1, boneTransform.matrixColumn2, boneTransform.matrixColumn3)
			End If

			Me.theMdlFileData.theBoneTransforms.Add(boneTransform)
		Next
	End Sub

	Public Sub WriteInternalMdlFileName(ByVal internalMdlFileName As String)
		Me.theOutputFileWriter.BaseStream.Seek(&H8, SeekOrigin.Begin)
		'TODO: Should only write up to 64 characters.
		Me.theOutputFileWriter.Write(internalMdlFileName.ToCharArray())
		'NOTE: Write the ending null byte.
		Me.theOutputFileWriter.Write(Convert.ToByte(0))
	End Sub

#End Region

#Region "Private Methods"

	Private Sub ReadEvents(ByVal aSequence As SourceMdlSequenceDesc10)
		If aSequence.eventCount > 0 Then
			'Dim sequenceInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				If Me.theInputFileReader.BaseStream.Position <> aSequence.eventOffset Then
					Dim offsetIsNotRight As Boolean = True
				End If

				Me.theInputFileReader.BaseStream.Seek(aSequence.eventOffset, SeekOrigin.Begin)
				aSequence.theEvents = New List(Of SourceMdlEvent10)(aSequence.eventCount)
				For eventIndex As Integer = 0 To aSequence.eventCount - 1
					'sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anEvent As New SourceMdlEvent10()

					fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					anEvent.frameIndex = Me.theInputFileReader.ReadInt32()
					anEvent.eventIndex = Me.theInputFileReader.ReadInt32()
					anEvent.eventType = Me.theInputFileReader.ReadInt32()
					anEvent.options = Me.theInputFileReader.ReadChars(64)
					anEvent.theOptions = CStr(anEvent.options).Trim(Chr(0))

					aSequence.theEvents.Add(anEvent)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anEvent")
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadPivots(ByVal aSequence As SourceMdlSequenceDesc10)
		If aSequence.pivotCount > 0 Then
			'Dim sequenceInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				If Me.theInputFileReader.BaseStream.Position <> aSequence.pivotOffset Then
					Dim offsetIsNotRight As Boolean = True
				End If

				Me.theInputFileReader.BaseStream.Seek(aSequence.pivotOffset, SeekOrigin.Begin)
				aSequence.thePivots = New List(Of SourceMdlPivot10)(aSequence.pivotCount)
				For pivotIndex As Integer = 0 To aSequence.pivotCount - 1
					'sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aPivot As New SourceMdlPivot10()

					fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aPivot.point.x = Me.theInputFileReader.ReadSingle()
					aPivot.point.y = Me.theInputFileReader.ReadSingle()
					aPivot.point.z = Me.theInputFileReader.ReadSingle()
					aPivot.pivotStart = Me.theInputFileReader.ReadInt32()
					aPivot.pivotEnd = Me.theInputFileReader.ReadInt32()

					aSequence.thePivots.Add(aPivot)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aPivot")
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadAnimationValues(ByVal animValuesInputFileStreamPosition As Long, ByVal frameCount As Integer, ByVal animValues As List(Of SourceMdlAnimationValue10))
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim frameCountRemainingToBeChecked As Integer
		Dim currentTotal As Byte
		Dim validCount As Byte

		Me.theInputFileReader.BaseStream.Seek(animValuesInputFileStreamPosition, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		frameCountRemainingToBeChecked = frameCount
		While (frameCountRemainingToBeChecked > 0)
			Dim animValue As New SourceMdlAnimationValue10()
			animValue.value = Me.theInputFileReader.ReadInt16()
			currentTotal = animValue.total
			If currentTotal = 0 Then
				Dim badIfThisIsReached As Integer = 42
				Exit While
			End If
			frameCountRemainingToBeChecked -= currentTotal
			animValues.Add(animValue)

			validCount = animValue.valid
			For i As Integer = 1 To validCount
				Dim animValue2 As New SourceMdlAnimationValue10()
				animValue2.value = Me.theInputFileReader.ReadInt16()
				animValues.Add(animValue2)
			Next
		End While

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation.theAnimationValues")
	End Sub

	Private Sub ReadModels(ByVal aBodyPart As SourceMdlBodyPart14)
		'Dim modelInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(aBodyPart.modelOffset, SeekOrigin.Begin)

			aBodyPart.theModels = New List(Of SourceMdlModel14)(aBodyPart.modelCount)
			For bodyPartIndex As Integer = 0 To aBodyPart.modelCount - 1
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aModel As New SourceMdlModel14()

				aModel.name = Me.theInputFileReader.ReadChars(32)
				aModel.theName = CStr(aModel.name).Trim(Chr(0))
				aModel.modelIndex = Me.theInputFileReader.ReadInt32()

				For x As Integer = 0 To aModel.weightingHeaderOffsets.Length - 1
					aModel.weightingHeaderOffsets(x) = Me.theInputFileReader.ReadInt32()
				Next

				aBodyPart.theModels.Add(aModel)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel [" + aModel.theName + "]")

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.ReadModelVertexBoneInfos(aModel)
				'Me.ReadModelNormalBoneInfos(aModel)
				'Me.ReadModelVertexes(aModel)
				'Me.ReadModelNormals(aModel)
				'Me.ReadMeshes(aModel)
				Me.ReadWeightingHeaders(aModel)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadWeightingHeaders(ByVal aModel As SourceMdlModel14)
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Try
			aModel.theWeightingHeaders = New List(Of SourceMdlWeightingHeader14)(aModel.weightingHeaderOffsets.Length)
			For x As Integer = 0 To aModel.weightingHeaderOffsets.Length - 1
				If aModel.weightingHeaderOffsets(x) > 0 Then
					Me.theInputFileReader.BaseStream.Seek(aModel.weightingHeaderOffsets(x), SeekOrigin.Begin)
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					Dim weightingHeader As New SourceMdlWeightingHeader14()

					weightingHeader.weightingHeaderIndex = Me.theInputFileReader.ReadInt32()
					weightingHeader.weightingBoneDataCount = Me.theInputFileReader.ReadInt32()
					weightingHeader.weightingBoneDataOffset = Me.theInputFileReader.ReadInt32()

					aModel.theWeightingHeaders.Add(weightingHeader)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theWeightingHeader [" + aModel.theName + "]")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadWeightingBoneDatas(weightingHeader)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadWeightingBoneDatas(ByVal weightingHeader As SourceMdlWeightingHeader14)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(weightingHeader.weightingBoneDataOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			weightingHeader.theWeightingBoneDatas = New List(Of SourceMdlWeightingBoneData14)(weightingHeader.weightingBoneDataCount)
			For i As Integer = 0 To weightingHeader.weightingBoneDataCount - 1
				Dim aBoneData As New SourceMdlWeightingBoneData14()

				For x As Integer = 0 To aBoneData.theWeightingBoneIndexes.Length - 1
					aBoneData.theWeightingBoneIndexes(x) = Me.theInputFileReader.ReadByte()
				Next

				weightingHeader.theWeightingBoneDatas.Add(aBoneData)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "weightingHeader.theWeightingBoneDatas " + weightingHeader.theWeightingBoneDatas.Count.ToString())
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	'Private Sub ReadModelVertexBoneInfos(ByVal aModel As SourceMdlModel14)
	'	If aModel.vertexCount > 0 Then
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long

	'		Try
	'			Dim vertexBoneInfo As Integer
	'			Me.theInputFileReader.BaseStream.Seek(aModel.vertexBoneInfoOffset, SeekOrigin.Begin)
	'			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'			aModel.theVertexBoneInfos = New List(Of Integer)(aModel.vertexCount)
	'			For vertexBoneInfoIndex As Integer = 0 To aModel.vertexCount - 1
	'				vertexBoneInfo = Me.theInputFileReader.ReadByte()
	'				aModel.theVertexBoneInfos.Add(vertexBoneInfo)
	'			Next

	'			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexBoneInfos " + aModel.theVertexBoneInfos.Count.ToString())

	'			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexBoneInfos alignment")
	'		Catch ex As Exception
	'			Dim debug As Integer = 4242
	'		End Try
	'	End If
	'End Sub

	'Private Sub ReadModelNormalBoneInfos(ByVal aModel As SourceMdlModel14)
	'	If aModel.normalCount > 0 Then
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long

	'		Try
	'			Dim normalBoneInfo As Integer
	'			Me.theInputFileReader.BaseStream.Seek(aModel.normalBoneInfoOffset, SeekOrigin.Begin)
	'			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'			aModel.theNormalBoneInfos = New List(Of Integer)(aModel.normalCount)
	'			For normalBoneInfoIndex As Integer = 0 To aModel.normalCount - 1
	'				normalBoneInfo = Me.theInputFileReader.ReadByte()
	'				aModel.theNormalBoneInfos.Add(normalBoneInfo)
	'			Next

	'			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormalBoneInfos " + aModel.theNormalBoneInfos.Count.ToString())

	'			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theNormalBoneInfos alignment")
	'		Catch ex As Exception
	'			Dim debug As Integer = 4242
	'		End Try
	'	End If
	'End Sub

	'Private Sub ReadModelVertexes(ByVal aModel As SourceMdlModel14)
	'	If aModel.vertexCount > 0 Then
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long

	'		Try
	'			Me.theInputFileReader.BaseStream.Seek(aModel.vertexOffset, SeekOrigin.Begin)
	'			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'			aModel.theVertexes = New List(Of SourceVector)(aModel.vertexCount)
	'			For vertexIndex As Integer = 0 To aModel.vertexCount - 1
	'				Dim vertex As New SourceVector()
	'				vertex.x = Me.theInputFileReader.ReadSingle()
	'				vertex.y = Me.theInputFileReader.ReadSingle()
	'				vertex.z = Me.theInputFileReader.ReadSingle()
	'				aModel.theVertexes.Add(vertex)
	'			Next

	'			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexes " + aModel.theVertexes.Count.ToString())

	'			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexes alignment")
	'		Catch ex As Exception
	'			Dim debug As Integer = 4242
	'		End Try
	'	End If
	'End Sub

	'Private Sub ReadModelNormals(ByVal aModel As SourceMdlModel14)
	'	If aModel.normalCount > 0 Then
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long

	'		Try
	'			Me.theInputFileReader.BaseStream.Seek(aModel.normalOffset, SeekOrigin.Begin)
	'			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'			aModel.theNormals = New List(Of SourceVector)(aModel.normalCount)
	'			For normalIndex As Integer = 0 To aModel.normalCount - 1
	'				Dim normal As New SourceVector()
	'				normal.x = Me.theInputFileReader.ReadSingle()
	'				normal.y = Me.theInputFileReader.ReadSingle()
	'				normal.z = Me.theInputFileReader.ReadSingle()
	'				aModel.theNormals.Add(normal)
	'			Next

	'			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormals " + aModel.theNormals.Count.ToString())

	'			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theNormals alignment")
	'		Catch ex As Exception
	'			Dim debug As Integer = 4242
	'		End Try
	'	End If
	'End Sub

	'Private Sub ReadMeshes(ByVal aModel As SourceMdlModel14)
	'	If aModel.weightingBoneIndexCount > 0 Then
	'		'Dim meshInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		'Dim fileOffsetStart2 As Long
	'		'Dim fileOffsetEnd2 As Long

	'		Try
	'			Me.theInputFileReader.BaseStream.Seek(aModel.weightingHeaderOffset, SeekOrigin.Begin)

	'			aModel.theMeshes = New List(Of SourceMdlMesh14)(aModel.weightingBoneIndexCount)
	'			For meshIndex As Integer = 0 To aModel.weightingBoneIndexCount - 1
	'				fileOffsetStart = Me.theInputFileReader.BaseStream.Position
	'				Dim aMesh As New SourceMdlMesh14()

	'				aMesh.faceCount = Me.theInputFileReader.ReadInt32()
	'				aMesh.faceOffset = Me.theInputFileReader.ReadInt32()
	'				aMesh.skinref = Me.theInputFileReader.ReadInt32()
	'				aMesh.normalCount = Me.theInputFileReader.ReadInt32()
	'				aMesh.normalOffset = Me.theInputFileReader.ReadInt32()

	'				aModel.theMeshes.Add(aMesh)

	'				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh")

	'				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'				Me.ReadFaces(aMesh)

	'				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'			Next

	'			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theMeshes alignment")
	'		Catch ex As Exception
	'			Dim debug As Integer = 4242
	'		End Try
	'	End If
	'End Sub

	'Private Sub ReadFaces(ByVal aMesh As SourceMdlMesh14)
	'	If aMesh.faceCount > 0 Then
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long

	'		Try
	'			Me.theInputFileReader.BaseStream.Seek(aMesh.faceOffset, SeekOrigin.Begin)
	'			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'			aMesh.theStripsAndFans = New List(Of SourceMeshTriangleStripOrFan10)()
	'			'For faceIndex As Integer = 0 To aMesh.faceCount - 1
	'			While True
	'				Dim aStripOrFan As New SourceMeshTriangleStripOrFan10()

	'				Dim listCount As Short
	'				listCount = Me.theInputFileReader.ReadInt16()
	'				If listCount = 0 Then
	'					'NOTE: End of list marker has been reached. 
	'					'Exit For
	'					Exit While
	'				Else
	'					aStripOrFan.theVertexInfos = New List(Of SourceMdlVertexInfo10)()
	'					If listCount < 0 Then
	'						aStripOrFan.theFacesAreStoredAsTriangleStrips = False
	'					Else
	'						aStripOrFan.theFacesAreStoredAsTriangleStrips = True
	'					End If
	'				End If
	'				listCount = CShort(Math.Abs(listCount))

	'				For listIndex As Integer = 0 To listCount - 1
	'					Dim vertexAndNormalIndexInfo As New SourceMdlVertexInfo10()
	'					vertexAndNormalIndexInfo.vertexIndex = Me.theInputFileReader.ReadUInt16()
	'					vertexAndNormalIndexInfo.normalIndex = Me.theInputFileReader.ReadUInt16()
	'					vertexAndNormalIndexInfo.s = Me.theInputFileReader.ReadInt16()
	'					vertexAndNormalIndexInfo.t = Me.theInputFileReader.ReadInt16()

	'					aStripOrFan.theVertexInfos.Add(vertexAndNormalIndexInfo)
	'				Next

	'				aMesh.theStripsAndFans.Add(aStripOrFan)

	'				'If faceIndex = aMesh.faceCount - 1 Then
	'				'	' The list should end with a zero.
	'				'	Dim endOfListMarker As Short
	'				'	endOfListMarker = Me.theInputFileReader.ReadInt16()
	'				'	If endOfListMarker <> 0 Then
	'				'		Dim endOfListMarkerIsNotZero As Integer = 4242
	'				'	End If
	'				'End If
	'			End While
	'			'Next

	'			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFaces " + aMesh.theStripsAndFans.Count.ToString())

	'			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aMesh.theFaces alignment")
	'		Catch ex As Exception
	'			Dim debug As Integer = 4242
	'		End Try
	'	End If
	'End Sub

	Private Sub ReadTextureData(ByVal aTexture As SourceMdlTexture14)
		'Dim boneInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(aTexture.dataOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aTexture.theData = New List(Of Byte)(CType(aTexture.width * aTexture.height, Integer))
			'FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
			'      Void ResizeTexture(s_texture_t * ptexture)
			'          ptexture->size = ptexture->skinwidth * ptexture->skinheight + 256 * 3;
			For byteIndex As Long = 0 To (aTexture.width * aTexture.height + 256 * 3) - 1
				'boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim data As Byte

				data = Me.theInputFileReader.ReadByte()

				aTexture.theData.Add(data)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture.theData")
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

#End Region

#Region "Data"

	Protected theInputFileReader As BinaryReader
	Protected theOutputFileWriter As BinaryWriter

	Protected theMdlFileData As SourceMdlFileData14

#End Region

End Class
