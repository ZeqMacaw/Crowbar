Imports System.IO

Public Class SourceMdlFile2531

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlFileReader As BinaryReader, ByVal mdlFileData As SourceMdlFileData2531)
		Me.theInputFileReader = mdlFileReader
		Me.theMdlFileData = mdlFileData

		Me.theMdlFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length
	End Sub

	Public Sub New(ByVal mdlFileWriter As BinaryWriter, ByVal mdlFileData As SourceMdlFileData2531)
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

		Me.theMdlFileData.id = Me.theInputFileReader.ReadChars(Me.theMdlFileData.id.Length)
		Me.theMdlFileData.theID = Me.theMdlFileData.id
		Me.theMdlFileData.version = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.checksum = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.name = Me.theInputFileReader.ReadChars(Me.theMdlFileData.name.Length)
		Me.theMdlFileData.theModelName = CStr(Me.theMdlFileData.name).Trim(Chr(0))

		Me.theMdlFileData.fileSize = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.theActualFileSize = Me.theInputFileReader.BaseStream.Length

		Me.theMdlFileData.eyePosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.eyePosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.eyePosition.z = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.illuminationPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.illuminationPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.illuminationPosition.z = Me.theInputFileReader.ReadSingle()

		Me.theMdlFileData.unknown01 = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.unknown02 = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.unknown03 = Me.theInputFileReader.ReadSingle()

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

		Me.theMdlFileData.unknown04 = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.unknown05 = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.boneCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneControllerCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneControllerOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.hitBoxSetCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.hitBoxSetOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.localAnimationCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localAnimationOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localSequenceCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localSequenceOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.sequencesIndexedFlag = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.sequenceGroupCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.sequenceGroupOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.textureCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.textureOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.texturePathCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.texturePathOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinReferenceCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinFamilyCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.bodyPartCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.bodyPartOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.localAttachmentCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localAttachmentOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.transitionCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.transitionOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.flexDescCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexDescOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexControllerCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexControllerOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexRuleCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexRuleOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.ikChainCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.ikChainOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.mouthCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.mouthOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localPoseParamaterCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localPoseParameterOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.surfacePropOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.unknownCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.unknownOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.includeModelCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.includeModelOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.unknown06 = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.unknown07 = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.unknown08 = Me.theInputFileReader.ReadInt32()

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "MDL File Header")

		'If Me.theMdlFileData.bodyPartCount = 0 AndAlso Me.theMdlFileData.localSequenceCount > 0 Then
		'	Me.theMdlFileData.theMdlFileOnlyHasAnimations = True
		'End If
	End Sub

	Public Sub ReadBones()
		If Me.theMdlFileData.boneCount > 0 Then
			Dim boneInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theBones = New List(Of SourceMdlBone2531)(Me.theMdlFileData.boneCount)
				For boneIndex As Integer = 0 To Me.theMdlFileData.boneCount - 1
					boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aBone As New SourceMdlBone2531()

					aBone.nameOffset = Me.theInputFileReader.ReadInt32()
					aBone.parentBoneIndex = Me.theInputFileReader.ReadInt32()
					For boneControllerIndexIndex As Integer = 0 To aBone.boneControllerIndex.Length - 1
						aBone.boneControllerIndex(boneControllerIndexIndex) = Me.theInputFileReader.ReadInt32()
					Next

					'For x As Integer = 0 To aBone.value.Length - 1
					'	aBone.value(x) = Me.theInputFileReader.ReadSingle()
					'Next
					'For x As Integer = 0 To aBone.scale.Length - 1
					'	aBone.scale(x) = Me.theInputFileReader.ReadSingle()
					'Next
					aBone.position.x = Me.theInputFileReader.ReadSingle()
					aBone.position.y = Me.theInputFileReader.ReadSingle()
					aBone.position.z = Me.theInputFileReader.ReadSingle()
					aBone.rotation.x = Me.theInputFileReader.ReadSingle()
					aBone.rotation.y = Me.theInputFileReader.ReadSingle()
					aBone.rotation.z = Me.theInputFileReader.ReadSingle()
					aBone.rotation.w = Me.theInputFileReader.ReadSingle()
					aBone.positionScale.x = Me.theInputFileReader.ReadSingle()
					aBone.positionScale.y = Me.theInputFileReader.ReadSingle()
					aBone.positionScale.z = Me.theInputFileReader.ReadSingle()
					aBone.rotationScale.x = Me.theInputFileReader.ReadSingle()
					aBone.rotationScale.y = Me.theInputFileReader.ReadSingle()
					aBone.rotationScale.z = Me.theInputFileReader.ReadSingle()
					'aBone.unknown01 = Me.theInputFileReader.ReadSingle()
					aBone.rotationScale.w = Me.theInputFileReader.ReadSingle()

					aBone.poseToBoneColumn0.x = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn1.x = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn2.x = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn3.x = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn0.y = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn1.y = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn2.y = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn3.y = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn0.z = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn1.z = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn2.z = Me.theInputFileReader.ReadSingle()
					aBone.poseToBoneColumn3.z = Me.theInputFileReader.ReadSingle()

					aBone.flags = Me.theInputFileReader.ReadInt32()

					aBone.proceduralRuleType = Me.theInputFileReader.ReadInt32()
					aBone.proceduralRuleOffset = Me.theInputFileReader.ReadInt32()
					aBone.physicsBoneIndex = Me.theInputFileReader.ReadInt32()
					aBone.surfacePropNameOffset = Me.theInputFileReader.ReadInt32()
					aBone.contents = Me.theInputFileReader.ReadInt32()

					Me.theMdlFileData.theBones.Add(aBone)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aBone.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aBone.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theName = " + aBone.theName)
						End If
					ElseIf aBone.theName Is Nothing Then
						aBone.theName = ""
					End If
					Me.theMdlFileData.theBoneNameToBoneIndexMap.Add(aBone.theName, boneIndex)

					If aBone.proceduralRuleOffset <> 0 Then
						If aBone.proceduralRuleType = SourceMdlBone2531.STUDIO_PROC_AXISINTERP Then
							Me.ReadAxisInterpBone(boneInputFileStreamPosition, aBone)
						ElseIf aBone.proceduralRuleType = SourceMdlBone2531.STUDIO_PROC_QUATINTERP Then
							Me.theMdlFileData.theProceduralBonesCommandIsUsed = True
							Me.ReadQuatInterpBone(boneInputFileStreamPosition, aBone)
						ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
							'Me.ReadJiggleBone(boneInputFileStreamPosition, aBone)
							Dim debug As Integer = 4242
						Else
							Dim debug As Integer = 4242
						End If
					End If
					If aBone.surfacePropNameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.surfacePropNameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aBone.theSurfacePropName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theSurfacePropName = " + aBone.theSurfacePropName)
						End If
					Else
						aBone.theSurfacePropName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
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

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneControllerOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theBoneControllers = New List(Of SourceMdlBoneController2531)(Me.theMdlFileData.boneControllerCount)
				For i As Integer = 0 To Me.theMdlFileData.boneControllerCount - 1
					'boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aBoneController As New SourceMdlBoneController2531()

					aBoneController.boneIndex = Me.theInputFileReader.ReadInt32()
					aBoneController.type = Me.theInputFileReader.ReadInt32()
					aBoneController.startAngleDegrees = Me.theInputFileReader.ReadSingle()
					aBoneController.endAngleDegrees = Me.theInputFileReader.ReadSingle()
					aBoneController.restIndex = Me.theInputFileReader.ReadInt32()
					aBoneController.inputField = Me.theInputFileReader.ReadInt32()
					For x As Integer = 0 To aBoneController.unused.Length - 1
						aBoneController.unused(x) = Me.theInputFileReader.ReadInt32()
					Next

					Me.theMdlFileData.theBoneControllers.Add(aBoneController)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBoneController")

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadAttachments()
		If Me.theMdlFileData.localAttachmentCount > 0 Then
			Dim attachmentInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localAttachmentOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theAttachments = New List(Of SourceMdlAttachment2531)(Me.theMdlFileData.localAttachmentCount)
				For attachmentIndex As Integer = 0 To Me.theMdlFileData.localAttachmentCount - 1
					attachmentInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim anAttachment As New SourceMdlAttachment2531()

					anAttachment.nameOffset = Me.theInputFileReader.ReadInt32()
					anAttachment.type = Me.theInputFileReader.ReadInt32()
					anAttachment.boneIndex = Me.theInputFileReader.ReadInt32()

					'anAttachment.attachmentPointColumn0.x = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn0.y = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn0.z = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn1.x = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn1.y = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn1.z = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn2.x = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn2.y = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn2.z = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn3.x = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn3.y = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPointColumn3.z = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPoint = New SourceVector()
					'anAttachment.attachmentPoint.x = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPoint.y = Me.theInputFileReader.ReadSingle()
					'anAttachment.attachmentPoint.z = Me.theInputFileReader.ReadSingle()
					'anAttachment.vector01.x = Me.theInputFileReader.ReadSingle()
					'anAttachment.vector01.y = Me.theInputFileReader.ReadSingle()
					'anAttachment.vector01.z = Me.theInputFileReader.ReadSingle()
					'anAttachment.vector02.x = Me.theInputFileReader.ReadSingle()
					'anAttachment.vector02.y = Me.theInputFileReader.ReadSingle()
					'anAttachment.vector02.z = Me.theInputFileReader.ReadSingle()
					'anAttachment.vector03.x = Me.theInputFileReader.ReadSingle()
					'anAttachment.vector03.y = Me.theInputFileReader.ReadSingle()
					'anAttachment.vector03.z = Me.theInputFileReader.ReadSingle()
					anAttachment.cXX = Me.theInputFileReader.ReadSingle()
					anAttachment.unused01 = Me.theInputFileReader.ReadSingle()
					anAttachment.unused02 = Me.theInputFileReader.ReadSingle()
					anAttachment.posX = Me.theInputFileReader.ReadSingle()

					anAttachment.cYX = Me.theInputFileReader.ReadSingle()
					anAttachment.unused03 = Me.theInputFileReader.ReadSingle()
					anAttachment.unused04 = Me.theInputFileReader.ReadSingle()
					anAttachment.posY = Me.theInputFileReader.ReadSingle()

					anAttachment.cZX = Me.theInputFileReader.ReadSingle()
					anAttachment.cZY = Me.theInputFileReader.ReadSingle()
					anAttachment.cZZ = Me.theInputFileReader.ReadSingle()
					anAttachment.posZ = Me.theInputFileReader.ReadSingle()


					Me.theMdlFileData.theAttachments.Add(anAttachment)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAttachment")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If anAttachment.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(attachmentInputFileStreamPosition + anAttachment.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						anAttachment.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName = " + anAttachment.theName)
						End If
					ElseIf anAttachment.theName Is Nothing Then
						anAttachment.theName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadHitboxSets()
		If Me.theMdlFileData.hitboxSetCount > 0 Then
			Dim hitboxSetInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.hitBoxSetOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theHitboxSets = New List(Of SourceMdlHitboxSet2531)(Me.theMdlFileData.hitBoxSetCount)
				For i As Integer = 0 To Me.theMdlFileData.hitBoxSetCount - 1
					hitboxSetInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aHitboxSet As New SourceMdlHitboxSet2531()

					aHitboxSet.nameOffset = Me.theInputFileReader.ReadInt32()
					aHitboxSet.hitboxCount = Me.theInputFileReader.ReadInt32()
					aHitboxSet.hitboxOffset = Me.theInputFileReader.ReadInt32()

					Me.theMdlFileData.theHitboxSets.Add(aHitboxSet)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aHitboxSet.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aHitboxSet.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitboxSet.theName = " + aHitboxSet.theName)
						End If
					Else
						aHitboxSet.theName = ""
					End If

					Me.ReadHitboxes(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, aHitboxSet)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxSets")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theHitboxSets alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadSequences()
		If Me.theMdlFileData.localSequenceCount > 0 Then
			Dim seqInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localSequenceOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theSequences = New List(Of SourceMdlSequenceDesc2531)(Me.theMdlFileData.localSequenceCount)
				For i As Integer = 0 To Me.theMdlFileData.localSequenceCount - 1
					seqInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aSequence As New SourceMdlSequenceDesc2531()

					aSequence.nameOffset = Me.theInputFileReader.ReadInt32()
					aSequence.activityNameOffset = Me.theInputFileReader.ReadInt32()
					aSequence.flags = Me.theInputFileReader.ReadInt32()
					aSequence.activityId = Me.theInputFileReader.ReadInt32()
					aSequence.activityWeight = Me.theInputFileReader.ReadInt32()
					aSequence.eventCount = Me.theInputFileReader.ReadInt32()
					aSequence.eventOffset = Me.theInputFileReader.ReadInt32()

					aSequence.bbMin.x = Me.theInputFileReader.ReadSingle()
					aSequence.bbMin.y = Me.theInputFileReader.ReadSingle()
					aSequence.bbMin.z = Me.theInputFileReader.ReadSingle()
					aSequence.bbMax.x = Me.theInputFileReader.ReadSingle()
					aSequence.bbMax.y = Me.theInputFileReader.ReadSingle()
					aSequence.bbMax.z = Me.theInputFileReader.ReadSingle()

					aSequence.blendCount = Me.theInputFileReader.ReadInt32()

					'For x As Integer = 0 To aSequence.anim.Length - 1
					'	aSequence.anim(x) = Me.theInputFileReader.ReadInt16()
					'Next
					For rowIndex As Integer = 0 To MAXSTUDIOBLENDS - 1
						For columnIndex As Integer = 0 To MAXSTUDIOBLENDS - 1
							aSequence.anim(rowIndex)(columnIndex) = Me.theInputFileReader.ReadInt16()
						Next
					Next

					aSequence.movementIndex = Me.theInputFileReader.ReadInt32()
					aSequence.groupSize(0) = Me.theInputFileReader.ReadInt32()
					aSequence.groupSize(1) = Me.theInputFileReader.ReadInt32()

					aSequence.paramIndex(0) = Me.theInputFileReader.ReadInt32()
					aSequence.paramIndex(1) = Me.theInputFileReader.ReadInt32()
					aSequence.paramStart(0) = Me.theInputFileReader.ReadSingle()
					aSequence.paramStart(1) = Me.theInputFileReader.ReadSingle()
					aSequence.paramEnd(0) = Me.theInputFileReader.ReadSingle()
					aSequence.paramEnd(1) = Me.theInputFileReader.ReadSingle()
					aSequence.paramParent = Me.theInputFileReader.ReadInt32()

					aSequence.sequenceGroup = Me.theInputFileReader.ReadInt32()

					'aSequence.test = Me.theInputFileReader.ReadInt32()
					aSequence.test = Me.theInputFileReader.ReadSingle()

					aSequence.fadeInTime = Me.theInputFileReader.ReadSingle()
					aSequence.fadeOutTime = Me.theInputFileReader.ReadSingle()

					aSequence.localEntryNodeIndex = Me.theInputFileReader.ReadInt32()
					aSequence.localExitNodeIndex = Me.theInputFileReader.ReadInt32()
					aSequence.nodeFlags = Me.theInputFileReader.ReadInt32()

					aSequence.entryPhase = Me.theInputFileReader.ReadSingle()
					aSequence.exitPhase = Me.theInputFileReader.ReadSingle()
					aSequence.lastFrame = Me.theInputFileReader.ReadSingle()

					aSequence.nextSeq = Me.theInputFileReader.ReadInt32()
					aSequence.pose = Me.theInputFileReader.ReadInt32()

					aSequence.ikRuleCount = Me.theInputFileReader.ReadInt32()
					aSequence.autoLayerCount = Me.theInputFileReader.ReadInt32()
					aSequence.autoLayerOffset = Me.theInputFileReader.ReadInt32()
					'aSequence.weightOffset = Me.theInputFileReader.ReadInt32()
					'aSequence.poseKeyOffset = Me.theInputFileReader.ReadInt32()
					aSequence.unknown01 = Me.theInputFileReader.ReadInt32()

					'aSequence.bbMin2.x = Me.theInputFileReader.ReadSingle()
					'aSequence.bbMin2.y = Me.theInputFileReader.ReadSingle()
					'aSequence.bbMin2.z = Me.theInputFileReader.ReadSingle()
					'aSequence.bbMax2.x = Me.theInputFileReader.ReadSingle()
					'aSequence.bbMax2.y = Me.theInputFileReader.ReadSingle()
					'aSequence.bbMax2.z = Me.theInputFileReader.ReadSingle()
					'For x As Integer = 0 To aSequence.test02.Length - 1
					'	aSequence.test02(x) = Me.theInputFileReader.ReadInt32()
					'Next
					For x As Integer = 0 To aSequence.test02.Length - 1
						aSequence.test02(x) = Me.theInputFileReader.ReadSingle()
					Next

					aSequence.test03 = Me.theInputFileReader.ReadInt32()

					aSequence.ikLockCount = Me.theInputFileReader.ReadInt32()
					aSequence.ikLockOffset = Me.theInputFileReader.ReadInt32()
					'aSequence.keyValueOffset = Me.theInputFileReader.ReadInt32()
					aSequence.keyValueSize = Me.theInputFileReader.ReadInt32()
					aSequence.keyValueOffset = Me.theInputFileReader.ReadInt32()

					'aSequence.unknown01 = Me.theInputFileReader.ReadInt32()
					aSequence.unknown02 = Me.theInputFileReader.ReadSingle()
					aSequence.unknown03 = Me.theInputFileReader.ReadSingle()
					For x As Integer = 0 To aSequence.unknown04.Length - 1
						aSequence.unknown04(x) = Me.theInputFileReader.ReadInt32()
					Next
					For x As Integer = 0 To aSequence.unknown05.Length - 1
						aSequence.unknown05(x) = Me.theInputFileReader.ReadSingle()
					Next

					Me.theMdlFileData.theSequences.Add(aSequence)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aSequence.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSequence.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aSequence.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequence.theName = " + aSequence.theName)
					Else
						aSequence.theName = ""
					End If

					'NOTE: Moved this line here so can show the name in the log.
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence [" + aSequence.theName + "]")

					If aSequence.activityNameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSequence.activityNameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aSequence.theActivityName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequence.theActivityName = " + aSequence.theActivityName)
						End If
					Else
						aSequence.theActivityName = ""
					End If

					'If (aSeqDesc.groupSize(0) > 1 OrElse aSeqDesc.groupSize(1) > 1) AndAlso aSeqDesc.poseKeyOffset <> 0 Then
					'	Me.ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc)
					'End If
					'If aSeqDesc.eventCount > 0 AndAlso aSeqDesc.eventOffset <> 0 Then
					'	Me.ReadEvents(seqInputFileStreamPosition, aSeqDesc)
					'End If
					'If aSeqDesc.autoLayerCount > 0 AndAlso aSeqDesc.autoLayerOffset <> 0 Then
					'	Me.ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc)
					'End If
					'If Me.theMdlFileData.boneCount > 0 AndAlso aSeqDesc.weightOffset > 0 Then
					'	Me.ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc)
					'End If
					'If aSeqDesc.ikLockCount > 0 AndAlso aSeqDesc.ikLockOffset <> 0 Then
					'	Me.ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc)
					'End If
					'If (aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)) > 0 AndAlso aSeqDesc.animIndexOffset <> 0 Then
					'	Me.ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc)
					'End If
					'If aSeqDesc.keyValueSize > 0 AndAlso aSeqDesc.keyValueOffset <> 0 Then
					'	Me.ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc)
					'End If
					'If aSeqDesc.activityModifierCount <> 0 AndAlso aSeqDesc.activityModifierOffset <> 0 Then
					'	Me.ReadActivityModifiers(seqInputFileStreamPosition, aSeqDesc)
					'End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceDescs")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadLocalAnimationDescs()
		If Me.theMdlFileData.localAnimationCount > 0 Then
			Dim animationDescInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localAnimationOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theAnimationDescs = New List(Of SourceMdlAnimationDesc2531)(Me.theMdlFileData.localAnimationCount)
				For i As Integer = 0 To Me.theMdlFileData.localAnimationCount - 1
					animationDescInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim anAnimationDesc As New SourceMdlAnimationDesc2531()

					anAnimationDesc.nameOffset = Me.theInputFileReader.ReadInt32()
					anAnimationDesc.fps = Me.theInputFileReader.ReadSingle()
					anAnimationDesc.flags = Me.theInputFileReader.ReadInt32()
					anAnimationDesc.frameCount = Me.theInputFileReader.ReadInt32()
					anAnimationDesc.movementCount = Me.theInputFileReader.ReadInt32()
					anAnimationDesc.movementOffset = Me.theInputFileReader.ReadInt32()

					anAnimationDesc.bbMin.x = Me.theInputFileReader.ReadSingle()
					anAnimationDesc.bbMin.y = Me.theInputFileReader.ReadSingle()
					anAnimationDesc.bbMin.z = Me.theInputFileReader.ReadSingle()
					anAnimationDesc.bbMax.x = Me.theInputFileReader.ReadSingle()
					anAnimationDesc.bbMax.y = Me.theInputFileReader.ReadSingle()
					anAnimationDesc.bbMax.z = Me.theInputFileReader.ReadSingle()

					anAnimationDesc.animOffset = Me.theInputFileReader.ReadInt32()

					anAnimationDesc.ikRuleCount = Me.theInputFileReader.ReadInt32()
					anAnimationDesc.ikRuleOffset = Me.theInputFileReader.ReadInt32()

					For x As Integer = 0 To anAnimationDesc.unused.Length - 1
						anAnimationDesc.unused(x) = Me.theInputFileReader.ReadInt32()
					Next

					Me.theMdlFileData.theAnimationDescs.Add(anAnimationDesc)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If anAnimationDesc.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						anAnimationDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)
						If anAnimationDesc.theName(0) = "@" Then
							anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 1)
						End If

						''NOTE: This naming is found in Garry's Mod garrysmod_dir.vpk "\models\m_anm.mdl":  "a_../combine_soldier_xsi/Hold_AR2_base.smd"
						'If anAnimationDesc.theName.StartsWith("a_../") OrElse anAnimationDesc.theName.StartsWith("a_..\") Then
						'	anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 5)
						'	anAnimationDesc.theName = Path.Combine(FileManager.GetPath(anAnimationDesc.theName), "a_" + Path.GetFileName(anAnimationDesc.theName))
						'End If

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAnimationDesc.theName = " + anAnimationDesc.theName)
						End If
					Else
						anAnimationDesc.theName = ""
					End If

					Me.ReadAnimations(animationDescInputFileStreamPosition, anAnimationDesc)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAnimationDescs alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadTextures()
		If Me.theMdlFileData.textureCount > 0 Then
			Dim textureInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long
			'Dim texturePath As String

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.textureOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theTextures = New List(Of SourceMdlTexture2531)(Me.theMdlFileData.textureCount)
				For i As Integer = 0 To Me.theMdlFileData.textureCount - 1
					textureInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aTexture As New SourceMdlTexture2531()

					aTexture.fileNameOffset = Me.theInputFileReader.ReadInt32()
					aTexture.flags = Me.theInputFileReader.ReadInt32()
					aTexture.width = Me.theInputFileReader.ReadSingle()
					aTexture.height = Me.theInputFileReader.ReadSingle()
					aTexture.unknown = Me.theInputFileReader.ReadSingle()

					Me.theMdlFileData.theTextures.Add(aTexture)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aTexture.fileNameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.fileNameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aTexture.theFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						' Convert all forward slashes to backward slashes.
						aTexture.theFileName = FileManager.GetNormalizedPathFileName(aTexture.theFileName)

						'NOTE: Leave this commented so QC file simply shows what is stored in MDL file.
						'      Crowbar should always try to show what was in original files unless user opts to do something else.
						'' Delete the path in the texture name that is already in the texturepaths list.
						'For j As Integer = 0 To Me.theMdlFileData.theTexturePaths.Count - 1
						'	texturePath = Me.theMdlFileData.theTexturePaths(j)
						'	If texturePath <> "" AndAlso aTexture.theName.StartsWith(texturePath) Then
						'		aTexture.theName = aTexture.theName.Replace(texturePath, "")
						'		Exit For
						'	End If
						'Next
						'
						''TEST: If texture name still has a path, remove the path and add it to the texturepaths list.
						'Dim texturePathName As String
						'Dim textureFileName As String
						'texturePathName = FileManager.GetPath(aTexture.theName)
						'textureFileName = Path.GetFileName(aTexture.theName)
						'If aTexture.theName <> textureFileName Then
						'	'NOTE: Place first because it should override whatever is already in list.
						'	'Me.theMdlFileData.theTexturePaths.Add(texturePathName)
						'	Me.theMdlFileData.theTexturePaths.Insert(0, texturePathName)
						'	aTexture.theName = textureFileName
						'End If

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.theFileName = " + aTexture.theFileName)
						End If
					Else
						aTexture.theFileName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTextures")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadTexturePaths()
		If Me.theMdlFileData.texturePathCount > 0 Then
			Dim texturePathInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.texturePathOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theTexturePaths = New List(Of String)(Me.theMdlFileData.texturePathCount)
				Dim texturePathOffset As Integer
				For i As Integer = 0 To Me.theMdlFileData.texturePathCount - 1
					texturePathInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aTexturePath As String

					texturePathOffset = Me.theInputFileReader.ReadInt32()

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexturePath (offset to text)")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If texturePathOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aTexturePath = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						'TEST: Convert all forward slashes to backward slashes.
						aTexturePath = FileManager.GetNormalizedPathFileName(aTexturePath)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath (text) = " + aTexturePath)
						End If
					Else
						aTexturePath = ""
					End If
					Me.theMdlFileData.theTexturePaths.Add(aTexturePath)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTexturePaths")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTexturePaths alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadSkins()
		If Me.theMdlFileData.skinFamilyCount > 0 AndAlso Me.theMdlFileData.skinReferenceCount > 0 Then
			Dim skinFamilyInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.skinOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theSkinFamilies = New List(Of List(Of Short))(Me.theMdlFileData.skinFamilyCount)
				For i As Integer = 0 To Me.theMdlFileData.skinFamilyCount - 1
					skinFamilyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aSkinFamily As New List(Of Short)()

					For j As Integer = 0 To Me.theMdlFileData.skinReferenceCount - 1
						Dim aSkinRef As Short
						aSkinRef = Me.theInputFileReader.ReadInt16()
						aSkinFamily.Add(aSkinRef)
					Next

					Me.theMdlFileData.theSkinFamilies.Add(aSkinFamily)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSkin")

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

					'If Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0 Then
					'	'$pos1 += ($matname_num * 2);
					'	Me.theInputFileReader.BaseStream.Seek(skinFamilyInputFileStreamPosition + Me.theMdlFileData.theTextures.Count * 2, SeekOrigin.Begin)
					'End If
				Next

				''TEST: Remove skinRef from each skinFamily, if it is at same skinRef index in all skinFamilies. 
				''      Start with the last skinRef index (Me.theMdlFileData.skinReferenceCount)
				''      and step -1 to 0 until skinRefs are different between skinFamilies.
				'Dim index As Integer = -1
				'For currentSkinRef As Integer = Me.theMdlFileData.skinReferenceCount - 1 To 0 Step -1
				'	For index = 0 To Me.theMdlFileData.skinFamilyCount - 1
				'		Dim aSkinRef As Integer
				'		aSkinRef = Me.theMdlFileData.theSkinFamilies(index)(currentSkinRef)

				'		If aSkinRef <> currentSkinRef Then
				'			Exit For
				'		End If
				'	Next

				'	If index = Me.theMdlFileData.skinFamilyCount Then
				'		For index = 0 To Me.theMdlFileData.skinFamilyCount - 1
				'			Me.theMdlFileData.theSkinFamilies(index).RemoveAt(currentSkinRef)
				'		Next
				'		Me.theMdlFileData.skinReferenceCount -= 1
				'	End If
				'Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadIncludeModels()
		If Me.theMdlFileData.includeModelCount > 0 Then
			Dim includeModelInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.includeModelOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theIncludeModels = New List(Of SourceMdlIncludeModel2531)(Me.theMdlFileData.includeModelCount)
				For i As Integer = 0 To Me.theMdlFileData.includeModelCount - 1
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					includeModelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anIncludeModel As New SourceMdlIncludeModel2531()

					anIncludeModel.fileNameOffset = Me.theInputFileReader.ReadInt32()
					For x As Integer = 0 To anIncludeModel.unknown.Length - 1
						anIncludeModel.unknown(x) = Me.theInputFileReader.ReadInt32()
					Next

					Me.theMdlFileData.theIncludeModels.Add(anIncludeModel)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If anIncludeModel.fileNameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + anIncludeModel.fileNameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						anIncludeModel.theFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIncludeModel.theFileName = " + anIncludeModel.theFileName)
						End If
					Else
						anIncludeModel.theFileName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIncludeModel [" + anIncludeModel.theFileName + "]")
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIncludeModels")
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
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.bodyPartOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theBodyParts = New List(Of SourceMdlBodyPart2531)(Me.theMdlFileData.bodyPartCount)
				For i As Integer = 0 To Me.theMdlFileData.bodyPartCount - 1
					bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aBodyPart As New SourceMdlBodyPart2531()

					aBodyPart.nameOffset = Me.theInputFileReader.ReadInt32()
					aBodyPart.modelCount = Me.theInputFileReader.ReadInt32()
					aBodyPart.base = Me.theInputFileReader.ReadInt32()
					aBodyPart.modelOffset = Me.theInputFileReader.ReadInt32()

					Me.theMdlFileData.theBodyParts.Add(aBodyPart)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aBodyPart.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aBodyPart.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBodyPart.theName = " + aBodyPart.theName)
						End If
					Else
						aBodyPart.theName = ""
					End If

					Me.ReadModels(bodyPartInputFileStreamPosition, aBodyPart)

					''NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts and models.
					'If i = Me.theMdlFileData.bodyPartCount - 1 Then
					'	Me.LogToEndAndAlignToNextStart(Me.theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment")
					'End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadFlexDescs()
		If Me.theMdlFileData.flexDescCount > 0 Then
			Dim flexDescInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexDescOffset, SeekOrigin.Begin)
			'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theFlexDescs = New List(Of SourceMdlFlexDesc)(Me.theMdlFileData.flexDescCount)
			For i As Integer = 0 To Me.theMdlFileData.flexDescCount - 1
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				flexDescInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aFlexDesc As New SourceMdlFlexDesc()
				aFlexDesc.nameOffset = Me.theInputFileReader.ReadInt32()
				Me.theMdlFileData.theFlexDescs.Add(aFlexDesc)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aFlexDesc.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(flexDescInputFileStreamPosition + aFlexDesc.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aFlexDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexDesc.theName = " + aFlexDesc.theName)
					End If
				Else
					aFlexDesc.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexDesc [" + aFlexDesc.theName + "]")
			Next

			'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs")
		End If
	End Sub

	Public Sub ReadFlexControllers()
		If Me.theMdlFileData.flexControllerCount > 0 Then
			Dim flexControllerInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexControllerOffset, SeekOrigin.Begin)
			'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theFlexControllers = New List(Of SourceMdlFlexController)(Me.theMdlFileData.flexControllerCount)
			For i As Integer = 0 To Me.theMdlFileData.flexControllerCount - 1
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				flexControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aFlexController As New SourceMdlFlexController()
				aFlexController.typeOffset = Me.theInputFileReader.ReadInt32()
				aFlexController.nameOffset = Me.theInputFileReader.ReadInt32()
				aFlexController.localToGlobal = Me.theInputFileReader.ReadInt32()
				aFlexController.min = Me.theInputFileReader.ReadSingle()
				aFlexController.max = Me.theInputFileReader.ReadSingle()
				Me.theMdlFileData.theFlexControllers.Add(aFlexController)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aFlexController.typeOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.typeOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aFlexController.theType = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theType = " + aFlexController.theType)
					End If
				Else
					aFlexController.theType = ""
				End If
				If aFlexController.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aFlexController.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theName = " + aFlexController.theName)
					End If
				Else
					aFlexController.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexController [" + aFlexController.theName + "]")
			Next

			If Me.theMdlFileData.theFlexControllers.Count > 0 Then
				Me.theMdlFileData.theModelCommandIsUsed = True
			End If

			'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllers")
		End If
	End Sub

	Public Sub ReadFlexRules()
		If Me.theMdlFileData.flexRuleCount > 0 Then
			Dim flexRuleInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexRuleOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theFlexRules = New List(Of SourceMdlFlexRule)(Me.theMdlFileData.flexRuleCount)
			For i As Integer = 0 To Me.theMdlFileData.flexRuleCount - 1
				flexRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aFlexRule As New SourceMdlFlexRule()
				aFlexRule.flexIndex = Me.theInputFileReader.ReadInt32()
				aFlexRule.opCount = Me.theInputFileReader.ReadInt32()
				aFlexRule.opOffset = Me.theInputFileReader.ReadInt32()
				Me.theMdlFileData.theFlexRules.Add(aFlexRule)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theFlexDescs(aFlexRule.flexIndex).theDescIsUsedByFlexRule = True

				If aFlexRule.opCount > 0 AndAlso aFlexRule.opOffset <> 0 Then
					Me.ReadFlexOps(flexRuleInputFileStreamPosition, aFlexRule)
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			If Me.theMdlFileData.theFlexRules.Count > 0 Then
				Me.theMdlFileData.theModelCommandIsUsed = True
			End If

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules " + Me.theMdlFileData.theFlexDescs.Count().ToString())
		End If
	End Sub

	Public Sub ReadPoseParamDescs()
		If Me.theMdlFileData.localPoseParamaterCount > 0 Then
			Dim poseInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localPoseParameterOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.thePoseParamDescs = New List(Of SourceMdlPoseParamDesc2531)(Me.theMdlFileData.localPoseParamaterCount)
				For i As Integer = 0 To Me.theMdlFileData.localPoseParamaterCount - 1
					poseInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aPoseParamDesc As New SourceMdlPoseParamDesc2531()

					aPoseParamDesc.nameOffset = Me.theInputFileReader.ReadInt32()
					aPoseParamDesc.flags = Me.theInputFileReader.ReadInt32()
					aPoseParamDesc.startingValue = Me.theInputFileReader.ReadSingle()
					aPoseParamDesc.endingValue = Me.theInputFileReader.ReadSingle()
					aPoseParamDesc.loopingRange = Me.theInputFileReader.ReadSingle()

					Me.theMdlFileData.thePoseParamDescs.Add(aPoseParamDesc)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aPoseParamDesc")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aPoseParamDesc.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(poseInputFileStreamPosition + aPoseParamDesc.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aPoseParamDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aPoseParamDesc.theName = " + aPoseParamDesc.theName)
						End If
					Else
						aPoseParamDesc.theName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs")
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
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				'Me.theMdlFileData.theSequenceGroupFileHeaders = New List(Of SourceMdlSequenceGroupFileHeader2531)(Me.theMdlFileData.sequenceGroupCount)
				Me.theMdlFileData.theSequenceGroups = New List(Of SourceMdlSequenceGroup2531)(Me.theMdlFileData.sequenceGroupCount)
				For sequenceGroupIndex As Integer = 0 To Me.theMdlFileData.sequenceGroupCount - 1
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					'boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aSequenceGroup As New SourceMdlSequenceGroup2531()

					'aSequenceGroup.name = Me.theInputFileReader.ReadChars(32)
					'aSequenceGroup.theName = CStr(aSequenceGroup.name).Trim(Chr(0))
					'aSequenceGroup.fileName = Me.theInputFileReader.ReadChars(64)
					'aSequenceGroup.theFileName = CStr(aSequenceGroup.fileName).Trim(Chr(0))
					'aSequenceGroup.cacheOffset = Me.theInputFileReader.ReadInt32()
					'aSequenceGroup.data = Me.theInputFileReader.ReadInt32()
					For x As Integer = 0 To aSequenceGroup.unknown.Length - 1
						'aSequenceGroup.unknown(x) = Me.theInputFileReader.ReadSingle()
						aSequenceGroup.unknown(x) = Me.theInputFileReader.ReadInt32()
					Next

					Me.theMdlFileData.theSequenceGroups.Add(aSequenceGroup)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequenceGroup ")
					'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequenceGroup " + aSequenceGroup.theName + " [filename = " + aSequenceGroup.theFileName + "]")
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceGroups " + Me.theMdlFileData.theSequenceGroups.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSequenceGroups alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadSurfaceProp()
		If Me.theMdlFileData.surfacePropOffset > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.surfacePropOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theSurfacePropName = " + Me.theMdlFileData.theSurfacePropName)
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		Else
			Me.theMdlFileData.theSurfacePropName = ""
		End If
	End Sub

	Public Sub ReadUnreadBytes()
		Me.theMdlFileData.theFileSeekLog.LogUnreadBytes(Me.theInputFileReader)
	End Sub

	Public Sub CreateFlexFrameList()
		Dim aFlexFrame As FlexFrame2531
		Dim aBodyPart As SourceMdlBodyPart2531
		Dim aModel As SourceMdlModel2531
		Dim aMesh As SourceMdlMesh2531
		Dim aFlex As SourceMdlFlex2531
		Dim searchedFlexFrame As FlexFrame2531

		Me.theMdlFileData.theFlexFrames = New List(Of FlexFrame2531)()

		'NOTE: Create the defaultflex.
		aFlexFrame = New FlexFrame2531()
		Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)

		If Me.theMdlFileData.theFlexDescs IsNot Nothing AndAlso Me.theMdlFileData.theFlexDescs.Count > 0 Then
			'Dim flexDescToMeshIndexes As List(Of List(Of Integer))
			Dim flexDescToFlexFrames As List(Of List(Of FlexFrame2531))
			Dim meshVertexIndexStart As Integer

			'flexDescToMeshIndexes = New List(Of List(Of Integer))(Me.theMdlFileData.theFlexDescs.Count)
			'For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
			'	Dim meshIndexList As New List(Of Integer)()
			'	flexDescToMeshIndexes.Add(meshIndexList)
			'Next

			flexDescToFlexFrames = New List(Of List(Of FlexFrame2531))(Me.theMdlFileData.theFlexDescs.Count)
			For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
				Dim flexFrameList As New List(Of FlexFrame2531)()
				flexDescToFlexFrames.Add(flexFrameList)
			Next

			For bodyPartIndex As Integer = 0 To Me.theMdlFileData.theBodyParts.Count - 1
				aBodyPart = Me.theMdlFileData.theBodyParts(bodyPartIndex)

				If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
					For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
						aModel = aBodyPart.theModels(modelIndex)

						If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
							For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
								aMesh = aModel.theMeshes(meshIndex)

								meshVertexIndexStart = Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

								If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
									For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
										aFlex = aMesh.theFlexes(flexIndex)

										aFlexFrame = Nothing
										If flexDescToFlexFrames(aFlex.flexDescIndex) IsNot Nothing Then
											For x As Integer = 0 To flexDescToFlexFrames(aFlex.flexDescIndex).Count - 1
												searchedFlexFrame = flexDescToFlexFrames(aFlex.flexDescIndex)(x)
												If searchedFlexFrame.flexes(0).target0 = aFlex.target0 _
												 AndAlso searchedFlexFrame.flexes(0).target1 = aFlex.target1 _
												 AndAlso searchedFlexFrame.flexes(0).target2 = aFlex.target2 _
												 AndAlso searchedFlexFrame.flexes(0).target3 = aFlex.target3 Then
													' Add to an existing FlexFrame2531.
													aFlexFrame = searchedFlexFrame
													Exit For
												End If
											Next
										End If
										If aFlexFrame Is Nothing Then
											aFlexFrame = New FlexFrame2531()
											Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)
											aFlexFrame.bodyAndMeshVertexIndexStarts = New List(Of Integer)()
											aFlexFrame.flexes = New List(Of SourceMdlFlex2531)()

											'Dim aFlexDescPartnerIndex As Integer
											'aFlexDescPartnerIndex = aMesh.theFlexes(flexIndex).flexDescPartnerIndex

											aFlexFrame.flexName = Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theName
											'If aFlexDescPartnerIndex > 0 Then
											'	'line += "flexpair """
											'	'aFlexFrame.flexName = aFlexFrame.flexName.Remove(aFlexFrame.flexName.Length - 1, 1)
											'	aFlexFrame.flexDescription = aFlexFrame.flexName
											'	aFlexFrame.flexDescription += "+"
											'	aFlexFrame.flexDescription += Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theName
											'	aFlexFrame.flexHasPartner = True
											'	aFlexFrame.flexSplit = Me.GetSplit(aFlex, meshVertexIndexStart)
											'	Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theDescIsUsedByFlex = True
											'Else
											'line += "flex """
											aFlexFrame.flexDescription = aFlexFrame.flexName
											aFlexFrame.flexHasPartner = False
											'End If
											Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theDescIsUsedByFlex = True

											flexDescToFlexFrames(aFlex.flexDescIndex).Add(aFlexFrame)
										End If

										aFlexFrame.bodyAndMeshVertexIndexStarts.Add(meshVertexIndexStart)
										aFlexFrame.flexes.Add(aFlex)

										'flexDescToMeshIndexes(aFlex.flexDescIndex).Add(meshIndex)
									Next
								End If
							Next
						End If
						'For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
						'	flexDescToMeshIndexes(x).Clear()
						'Next
					Next
				End If
			Next
		End If
	End Sub

	Public Sub WriteInternalMdlFileName(ByVal internalMdlFileName As String)
		Me.theOutputFileWriter.BaseStream.Seek(&HC, SeekOrigin.Begin)
		'TODO: Should only write up to 128 characters.
		Me.theOutputFileWriter.Write(internalMdlFileName.ToCharArray())
		'NOTE: Write the ending null byte.
		Me.theOutputFileWriter.Write(Convert.ToByte(0))
	End Sub

#End Region

#Region "Private Methods"

	Private Sub ReadAxisInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone2531)
		Dim axisInterpBoneInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)

			axisInterpBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aBone.theAxisInterpBone = New SourceMdlAxisInterpBone2531()
			aBone.theAxisInterpBone.controlBoneIndex = Me.theInputFileReader.ReadInt32()
			aBone.theAxisInterpBone.axis = Me.theInputFileReader.ReadInt32()
			For x As Integer = 0 To aBone.theAxisInterpBone.pos.Length - 1
				aBone.theAxisInterpBone.pos(x).x = Me.theInputFileReader.ReadSingle()
				aBone.theAxisInterpBone.pos(x).y = Me.theInputFileReader.ReadSingle()
				aBone.theAxisInterpBone.pos(x).z = Me.theInputFileReader.ReadSingle()
			Next
			For x As Integer = 0 To aBone.theAxisInterpBone.quat.Length - 1
				aBone.theAxisInterpBone.quat(x).x = Me.theInputFileReader.ReadSingle()
				aBone.theAxisInterpBone.quat(x).y = Me.theInputFileReader.ReadSingle()
				aBone.theAxisInterpBone.quat(x).z = Me.theInputFileReader.ReadSingle()
				aBone.theAxisInterpBone.quat(x).z = Me.theInputFileReader.ReadSingle()
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAxisInterpBone")

			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadQuatInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone2531)
		Dim quatInterpBoneInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)

			quatInterpBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aBone.theQuatInterpBone = New SourceMdlQuatInterpBone2531()
			aBone.theQuatInterpBone.controlBoneIndex = Me.theInputFileReader.ReadInt32()
			aBone.theQuatInterpBone.triggerCount = Me.theInputFileReader.ReadInt32()
			aBone.theQuatInterpBone.triggerOffset = Me.theInputFileReader.ReadInt32()

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theQuatInterpBone")

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
				Me.ReadTriggers(quatInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
			End If

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadTriggers(ByVal quatInterpBoneInputFileStreamPosition As Long, ByVal aQuatInterpBone As SourceMdlQuatInterpBone2531)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(quatInterpBoneInputFileStreamPosition + aQuatInterpBone.triggerOffset, SeekOrigin.Begin)
			'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aQuatInterpBone.theTriggers = New List(Of SourceMdlQuatInterpInfo2531)(aQuatInterpBone.triggerCount)
			For j As Integer = 0 To aQuatInterpBone.triggerCount - 1
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				Dim aTrigger As New SourceMdlQuatInterpInfo2531()

				aTrigger.inverseToleranceAngle = Me.theInputFileReader.ReadSingle()

				aTrigger.trigger.x = Me.theInputFileReader.ReadSingle()
				aTrigger.trigger.y = Me.theInputFileReader.ReadSingle()
				aTrigger.trigger.z = Me.theInputFileReader.ReadSingle()
				aTrigger.trigger.w = Me.theInputFileReader.ReadSingle()

				aTrigger.pos.x = Me.theInputFileReader.ReadSingle()
				aTrigger.pos.y = Me.theInputFileReader.ReadSingle()
				aTrigger.pos.z = Me.theInputFileReader.ReadSingle()

				aTrigger.quat.x = Me.theInputFileReader.ReadSingle()
				aTrigger.quat.y = Me.theInputFileReader.ReadSingle()
				aTrigger.quat.z = Me.theInputFileReader.ReadSingle()
				aTrigger.quat.w = Me.theInputFileReader.ReadSingle()

				aQuatInterpBone.theTriggers.Add(aTrigger)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.aQuatInterpBone.aTrigger")
			Next

			'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aQuatInterpBone.theTriggers")
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadHitboxes(ByVal hitboxOffsetInputFileStreamPosition As Long, ByVal aHitboxSet As SourceMdlHitboxSet2531)
		If aHitboxSet.hitboxCount > 0 Then
			'Dim hitboxInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(hitboxOffsetInputFileStreamPosition, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aHitboxSet.theHitboxes = New List(Of SourceMdlHitbox2531)(aHitboxSet.hitboxCount)
				For j As Integer = 0 To aHitboxSet.hitboxCount - 1
					'hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aHitbox As New SourceMdlHitbox2531()

					aHitbox.boneIndex = Me.theInputFileReader.ReadInt32()
					aHitbox.groupIndex = Me.theInputFileReader.ReadInt32()
					aHitbox.boundingBoxMin.x = Me.theInputFileReader.ReadSingle()
					aHitbox.boundingBoxMin.y = Me.theInputFileReader.ReadSingle()
					aHitbox.boundingBoxMin.z = Me.theInputFileReader.ReadSingle()
					aHitbox.boundingBoxMax.x = Me.theInputFileReader.ReadSingle()
					aHitbox.boundingBoxMax.y = Me.theInputFileReader.ReadSingle()
					aHitbox.boundingBoxMax.z = Me.theInputFileReader.ReadSingle()

					aHitboxSet.theHitboxes.Add(aHitbox)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitbox")

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet.theHitboxes")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadModels(ByVal bodyPartInputFileStreamPosition As Long, ByVal aBodyPart As SourceMdlBodyPart2531)
		If aBodyPart.modelCount > 0 Then
			Dim modelInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aBodyPart.theModels = New List(Of SourceMdlModel2531)(aBodyPart.modelCount)
				For j As Integer = 0 To aBodyPart.modelCount - 1
					modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aModel As New SourceMdlModel2531()

					aModel.name = Me.theInputFileReader.ReadChars(aModel.name.Length)
					aModel.theName = CStr(aModel.name).Trim(Chr(0))
					aModel.type = Me.theInputFileReader.ReadInt32()
					aModel.boundingRadius = Me.theInputFileReader.ReadSingle()

					aModel.meshCount = Me.theInputFileReader.ReadInt32()
					aModel.meshOffset = Me.theInputFileReader.ReadInt32()
					aModel.vertexCount = Me.theInputFileReader.ReadInt32()
					aModel.vertexOffset = Me.theInputFileReader.ReadInt32()
					aModel.tangentOffset = Me.theInputFileReader.ReadInt32()

					aModel.vertexListType = Me.theInputFileReader.ReadInt32()

					For x As Integer = 0 To aModel.unknown01.Length - 1
						aModel.unknown01(x) = Me.theInputFileReader.ReadSingle()
					Next

					'aModel.unknownCount = Me.theInputFileReader.ReadInt32()
					'aModel.unknownOffset = Me.theInputFileReader.ReadInt32()

					For x As Integer = 0 To aModel.unknown02.Length - 1
						aModel.unknown02(x) = Me.theInputFileReader.ReadSingle()
					Next
					'For x As Integer = 0 To aModel.unknown03.Length - 1
					'	aModel.unknown03(x) = Me.theInputFileReader.ReadInt32()
					'Next

					aModel.attachmentCount = Me.theInputFileReader.ReadInt32()
					aModel.attachmentOffset = Me.theInputFileReader.ReadInt32()
					aModel.eyeballCount = Me.theInputFileReader.ReadInt32()
					aModel.eyeballOffset = Me.theInputFileReader.ReadInt32()
					For x As Integer = 0 To aModel.unknown03.Length - 1
						aModel.unknown03(x) = Me.theInputFileReader.ReadInt32()
					Next

					aModel.unknown01Count = Me.theInputFileReader.ReadInt32()
					aModel.unknown01Offset = Me.theInputFileReader.ReadInt32()
					aModel.unknown02Count = Me.theInputFileReader.ReadInt32()
					aModel.unknown02Offset = Me.theInputFileReader.ReadInt32()

					aBodyPart.theModels.Add(aModel)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel [" + aModel.theName + "]")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
					Me.ReadEyeballs(modelInputFileStreamPosition, aModel)
					Me.ReadMeshes(modelInputFileStreamPosition, aModel)
					'If (Me.theMdlFileData.flags And SourceMdlFileData2531.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
					If aModel.vertexListType = 0 Then
						Me.ReadVertexesType0(modelInputFileStreamPosition, aModel)
					ElseIf aModel.vertexListType = 1 Then
						Me.ReadVertexesType1(modelInputFileStreamPosition, aModel)
					Else
						Me.ReadVertexesType2(modelInputFileStreamPosition, aModel)
					End If
					Me.ReadTangents(modelInputFileStreamPosition, aModel)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aBodyPart.theModels alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadEyeballs(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel2531)
		If aModel.eyeballCount > 0 AndAlso aModel.eyeballOffset <> 0 Then
			Dim eyeballInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.eyeballOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theEyeballs = New List(Of SourceMdlEyeball2531)(aModel.eyeballCount)
				For k As Integer = 0 To aModel.eyeballCount - 1
					eyeballInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim anEyeball As New SourceMdlEyeball2531()

					anEyeball.nameOffset = Me.theInputFileReader.ReadInt32()

					anEyeball.boneIndex = Me.theInputFileReader.ReadInt32()
					anEyeball.org.x = Me.theInputFileReader.ReadSingle()
					anEyeball.org.y = Me.theInputFileReader.ReadSingle()
					anEyeball.org.z = Me.theInputFileReader.ReadSingle()
					anEyeball.zOffset = Me.theInputFileReader.ReadSingle()
					anEyeball.radius = Me.theInputFileReader.ReadSingle()
					anEyeball.up.x = Me.theInputFileReader.ReadSingle()
					anEyeball.up.y = Me.theInputFileReader.ReadSingle()
					anEyeball.up.z = Me.theInputFileReader.ReadSingle()
					anEyeball.forward.x = Me.theInputFileReader.ReadSingle()
					anEyeball.forward.y = Me.theInputFileReader.ReadSingle()
					anEyeball.forward.z = Me.theInputFileReader.ReadSingle()

					anEyeball.texture = Me.theInputFileReader.ReadInt32()
					anEyeball.iris_material = Me.theInputFileReader.ReadInt32()
					anEyeball.iris_scale = Me.theInputFileReader.ReadSingle()
					anEyeball.glint_material = Me.theInputFileReader.ReadInt32()

					anEyeball.upperFlexDesc(0) = Me.theInputFileReader.ReadInt32()
					anEyeball.upperFlexDesc(1) = Me.theInputFileReader.ReadInt32()
					anEyeball.upperFlexDesc(2) = Me.theInputFileReader.ReadInt32()
					anEyeball.lowerFlexDesc(0) = Me.theInputFileReader.ReadInt32()
					anEyeball.lowerFlexDesc(1) = Me.theInputFileReader.ReadInt32()
					anEyeball.lowerFlexDesc(2) = Me.theInputFileReader.ReadInt32()
					anEyeball.upperTarget(0) = Me.theInputFileReader.ReadSingle()
					anEyeball.upperTarget(1) = Me.theInputFileReader.ReadSingle()
					anEyeball.upperTarget(2) = Me.theInputFileReader.ReadSingle()
					anEyeball.lowerTarget(0) = Me.theInputFileReader.ReadSingle()
					anEyeball.lowerTarget(1) = Me.theInputFileReader.ReadSingle()
					anEyeball.lowerTarget(2) = Me.theInputFileReader.ReadSingle()

					anEyeball.upperLidFlexDesc = Me.theInputFileReader.ReadInt32()
					anEyeball.lowerLidFlexDesc = Me.theInputFileReader.ReadInt32()

					anEyeball.minPitch = Me.theInputFileReader.ReadSingle()
					anEyeball.maxPitch = Me.theInputFileReader.ReadSingle()
					anEyeball.minYaw = Me.theInputFileReader.ReadSingle()
					anEyeball.maxYaw = Me.theInputFileReader.ReadSingle()

					aModel.theEyeballs.Add(anEyeball)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anEyeball")

					''NOTE: Set the default value to -1 to distinguish it from value assigned to it by ReadMeshes().
					'anEyeball.theTextureIndex = -1

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'NOTE: The mdl file doesn't appear to store the eyeball name; studiomdl only uses it internally with eyelids.
					If anEyeball.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(eyeballInputFileStreamPosition + anEyeball.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						anEyeball.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEyeball.theName = " + anEyeball.theName)
						End If
					Else
						anEyeball.theName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'If aModel.theEyeballs.Count > 0 Then
				'	Me.theMdlFileData.theModelCommandIsUsed = True
				'End If

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theEyeballs")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theEyeballs alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadMeshes(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel2531)
		If aModel.meshCount > 0 AndAlso aModel.meshOffset <> 0 Then
			Dim meshInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.meshOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theMeshes = New List(Of SourceMdlMesh2531)(aModel.meshCount)
				For meshIndex As Integer = 0 To aModel.meshCount - 1
					meshInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aMesh As New SourceMdlMesh2531()

					aMesh.materialIndex = Me.theInputFileReader.ReadInt32()
					aMesh.modelOffset = Me.theInputFileReader.ReadInt32()
					aMesh.vertexCount = Me.theInputFileReader.ReadInt32()
					aMesh.vertexIndexStart = Me.theInputFileReader.ReadInt32()
					aMesh.flexCount = Me.theInputFileReader.ReadInt32()
					aMesh.flexOffset = Me.theInputFileReader.ReadInt32()

					Dim meshVertexData As New SourceMdlMeshVertexData()
					meshVertexData.modelVertexDataP = Me.theInputFileReader.ReadInt32()
					For x As Integer = 0 To MAX_NUM_LODS - 1
						meshVertexData.lodVertexCount(x) = Me.theInputFileReader.ReadInt32()
					Next
					aMesh.vertexData = meshVertexData

					aModel.theMeshes.Add(aMesh)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh")

					'' Fill-in eyeball texture index info.
					'If aMesh.materialType = 1 Then
					'	aModel.theEyeballs(aMesh.materialParam).theTextureIndex = aMesh.materialIndex
					'End If

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aMesh.flexCount > 0 AndAlso aMesh.flexOffset <> 0 Then
						Me.ReadFlexes(meshInputFileStreamPosition, aMesh)
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theMeshes")

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 32, "aModel.theMeshes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadFlexes(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceMdlMesh2531)
		aMesh.theFlexes = New List(Of SourceMdlFlex2531)(aMesh.flexCount)
		Dim flexInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.flexOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			For k As Integer = 0 To aMesh.flexCount - 1
				flexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aFlex As New SourceMdlFlex2531()

				aFlex.flexDescIndex = Me.theInputFileReader.ReadInt32()

				aFlex.target0 = Me.theInputFileReader.ReadSingle()
				aFlex.target1 = Me.theInputFileReader.ReadSingle()
				aFlex.target2 = Me.theInputFileReader.ReadSingle()
				aFlex.target3 = Me.theInputFileReader.ReadSingle()

				aFlex.vertCount = Me.theInputFileReader.ReadInt32()
				aFlex.vertOffset = Me.theInputFileReader.ReadInt32()

				aFlex.unknown = Me.theInputFileReader.ReadInt32()
				'------
				'aFlex.flexDescPartnerIndex = Me.theInputFileReader.ReadInt32()
				'aFlex.vertAnimType = Me.theInputFileReader.ReadByte()
				'For x As Integer = 0 To aFlex.unusedChar.Length - 1
				'	aFlex.unusedChar(x) = Me.theInputFileReader.ReadChar()
				'Next
				'For x As Integer = 0 To aFlex.unused.Length - 1
				'	aFlex.unused(x) = Me.theInputFileReader.ReadInt32()
				'Next

				aMesh.theFlexes.Add(aFlex)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aFlex.vertCount > 0 AndAlso aFlex.vertOffset <> 0 Then
					Me.ReadVertAnims(flexInputFileStreamPosition, aFlex)
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFlexes " + aMesh.theFlexes.Count.ToString())

			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aMesh.theFlexes alignment")
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Function SortVertAnims(ByVal x As SourceMdlVertAnim2531, ByVal y As SourceMdlVertAnim2531) As Integer
		Return x.index.CompareTo(y.index)
	End Function

	Private Sub ReadVertAnims(ByVal flexInputFileStreamPosition As Long, ByVal aFlex As SourceMdlFlex2531)
		Dim eyeballInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Dim aVertAnim As SourceMdlVertAnim2531
			aFlex.theVertAnims = New List(Of SourceMdlVertAnim2531)(aFlex.vertCount)
			For k As Integer = 0 To aFlex.vertCount - 1
				eyeballInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				aVertAnim = New SourceMdlVertAnim2531()

				aVertAnim.index = Me.theInputFileReader.ReadUInt16()

				'aVertAnim.deltaX = Me.theInputFileReader.ReadByte()
				'aVertAnim.deltaY = Me.theInputFileReader.ReadByte()
				'aVertAnim.deltaZ = Me.theInputFileReader.ReadByte()
				'aVertAnim.nDeltaX = Me.theInputFileReader.ReadByte()
				'aVertAnim.nDeltaY = Me.theInputFileReader.ReadByte()
				'aVertAnim.nDeltaZ = Me.theInputFileReader.ReadByte()
				'------
				'TEST: almost correct
				'aVertAnim.deltaX = Me.theInputFileReader.ReadSByte()
				'aVertAnim.deltaY = Me.theInputFileReader.ReadSByte()
				'aVertAnim.deltaZ = Me.theInputFileReader.ReadSByte()
				'aVertAnim.nDeltaX = Me.theInputFileReader.ReadSByte()
				'aVertAnim.nDeltaY = Me.theInputFileReader.ReadSByte()
				'aVertAnim.nDeltaZ = Me.theInputFileReader.ReadSByte()
				'------
				'aVertAnim.deltaX = Me.theInputFileReader.ReadSByte()
				'aVertAnim.nDeltaX = Me.theInputFileReader.ReadSByte()
				'aVertAnim.deltaY = Me.theInputFileReader.ReadSByte()
				'aVertAnim.nDeltaY = Me.theInputFileReader.ReadSByte()
				'aVertAnim.deltaZ = Me.theInputFileReader.ReadSByte()
				'aVertAnim.nDeltaZ = Me.theInputFileReader.ReadSByte()
				'------
				aVertAnim.deltaX = Me.theInputFileReader.ReadInt16()
				aVertAnim.deltaY = Me.theInputFileReader.ReadInt16()
				aVertAnim.deltaZ = Me.theInputFileReader.ReadInt16()
				'------
				'For x As Integer = 0 To 2
				'	aVertAnim.deltaByte(x) = Me.theInputFileReader.ReadByte()
				'Next
				'For x As Integer = 0 To 2
				'	aVertAnim.nDeltaByte(x) = Me.theInputFileReader.ReadByte()
				'Next
				'------
				'For x As Integer = 0 To 2
				'	aVertAnim.deltaUShort(x) = Me.theInputFileReader.ReadUInt16()
				'Next

				aFlex.theVertAnims.Add(aVertAnim)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			'aFlex.theVertAnims.Sort(AddressOf Me.SortVertAnims)

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims " + aFlex.theVertAnims.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aFlex.theVertAnims alignment")
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadFlexOps(ByVal flexRuleInputFileStreamPosition As Long, ByVal aFlexRule As SourceMdlFlexRule)
		'Dim flexRuleInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(flexRuleInputFileStreamPosition + aFlexRule.opOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aFlexRule.theFlexOps = New List(Of SourceMdlFlexOp)(aFlexRule.opCount)
			For i As Integer = 0 To aFlexRule.opCount - 1
				'flexRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aFlexOp As New SourceMdlFlexOp()
				aFlexOp.op = Me.theInputFileReader.ReadInt32()
				If aFlexOp.op = SourceMdlFlexOp.STUDIO_CONST Then
					aFlexOp.value = Me.theInputFileReader.ReadSingle()
				Else
					aFlexOp.index = Me.theInputFileReader.ReadInt32()
					If aFlexOp.op = SourceMdlFlexOp.STUDIO_FETCH2 Then
						Me.theMdlFileData.theFlexDescs(aFlexOp.index).theDescIsUsedByFlexRule = True
					End If
				End If
				aFlexRule.theFlexOps.Add(aFlexOp)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexRule.theFlexOps " + aFlexRule.theFlexOps.Count.ToString())
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadVertexesType0(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel2531)
		If aModel.vertexCount > 0 Then
			'Dim hitboxInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theVertexesType0 = New List(Of SourceMdlType0Vertex2531)(aModel.vertexCount)
				For j As Integer = 0 To aModel.vertexCount - 1
					'hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aVertex As New SourceMdlType0Vertex2531()

					For x As Integer = 0 To aVertex.weight.Length - 1
						aVertex.weight(x) = Me.theInputFileReader.ReadByte()
					Next
					aVertex.unknown1 = Me.theInputFileReader.ReadByte()
					For x As Integer = 0 To aVertex.boneIndex.Length - 1
						aVertex.boneIndex(x) = Me.theInputFileReader.ReadInt16()
					Next
					aVertex.unknown2 = Me.theInputFileReader.ReadInt16()

					aVertex.position.x = Me.theInputFileReader.ReadSingle()
					aVertex.position.y = Me.theInputFileReader.ReadSingle()
					aVertex.position.z = Me.theInputFileReader.ReadSingle()
					aVertex.normal.x = Me.theInputFileReader.ReadSingle()
					aVertex.normal.y = Me.theInputFileReader.ReadSingle()
					aVertex.normal.z = Me.theInputFileReader.ReadSingle()
					aVertex.texCoordU = Me.theInputFileReader.ReadSingle()
					aVertex.texCoordV = Me.theInputFileReader.ReadSingle()

					aModel.theVertexesType0.Add(aVertex)

					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexesType0 " + aModel.theVertexesType0.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadVertexesType1(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel2531)
		If aModel.vertexCount > 0 Then
			Dim hitboxInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theVertexesType1 = New List(Of SourceMdlType1Vertex2531)(aModel.vertexCount)
				For j As Integer = 0 To aModel.vertexCount - 1
					hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aVertex As New SourceMdlType1Vertex2531()

					'aVertex.positionX = Me.theInputFileReader.ReadUInt16()
					'aVertex.positionY = Me.theInputFileReader.ReadUInt16()
					'aVertex.positionZ = Me.theInputFileReader.ReadUInt16()
					'aVertex.normalIndex = Me.theInputFileReader.ReadUInt16()
					'aVertex.texCoordU = Me.theInputFileReader.ReadUInt16()
					'aVertex.texCoordV = Me.theInputFileReader.ReadUInt16()
					'For x As Integer = 0 To aVertex.unknown.Length - 1
					'	aVertex.unknown(x) = Me.theInputFileReader.ReadByte()
					'Next
					aVertex.positionX = Me.theInputFileReader.ReadUInt16()
					aVertex.positionY = Me.theInputFileReader.ReadUInt16()
					aVertex.positionZ = Me.theInputFileReader.ReadUInt16()
					'aVertex.normalX = Me.theInputFileReader.ReadUInt16()
					'aVertex.normalY = Me.theInputFileReader.ReadUInt16()
					'aVertex.normalZ = Me.theInputFileReader.ReadUInt16()
					aVertex.normalX = Me.theInputFileReader.ReadByte()
					aVertex.normalY = Me.theInputFileReader.ReadByte()
					aVertex.normalZ = Me.theInputFileReader.ReadByte()
					'Me.theInputFileReader.ReadByte()
					aVertex.texCoordU = Me.theInputFileReader.ReadByte()
					Me.theInputFileReader.ReadByte()
					aVertex.texCoordV = Me.theInputFileReader.ReadByte()
					'Me.theInputFileReader.ReadByte()
					'aVertex.scaleX = Me.theInputFileReader.ReadByte()
					'aVertex.scaleY = Me.theInputFileReader.ReadByte()
					'aVertex.scaleZ = Me.theInputFileReader.ReadByte()

					aModel.theVertexesType1.Add(aVertex)

					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'DEBUG:
					Me.theInputFileReader.BaseStream.Seek(hitboxInputFileStreamPosition, SeekOrigin.Begin)
					For x As Integer = 0 To aVertex.unknown.Length - 1
						aVertex.unknown(x) = Me.theInputFileReader.ReadByte()
					Next

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexesType1 " + aModel.theVertexesType1.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadVertexesType2(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel2531)
		If aModel.vertexCount > 0 Then
			'Dim hitboxInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theVertexesType2 = New List(Of SourceMdlType2Vertex2531)(aModel.vertexCount)
				For j As Integer = 0 To aModel.vertexCount - 1
					'hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aVertex As New SourceMdlType2Vertex2531()

					'aVertex.positionX = Me.theInputFileReader.ReadUInt16()
					'aVertex.positionY = Me.theInputFileReader.ReadUInt16()
					'aVertex.positionZ = Me.theInputFileReader.ReadUInt16()
					'aVertex.normalIndex = Me.theInputFileReader.ReadUInt16()
					'aVertex.texCoordU = Me.theInputFileReader.ReadUInt16()
					'aVertex.texCoordV = Me.theInputFileReader.ReadUInt16()
					'For x As Integer = 0 To aVertex.unknown.Length - 1
					'	aVertex.unknown(x) = Me.theInputFileReader.ReadByte()
					'Next
					aVertex.positionX = Me.theInputFileReader.ReadByte()
					aVertex.positionY = Me.theInputFileReader.ReadByte()
					aVertex.positionZ = Me.theInputFileReader.ReadByte()
					'aVertex.positionX = Me.theInputFileReader.ReadSByte()
					'aVertex.positionY = Me.theInputFileReader.ReadSByte()
					'aVertex.positionZ = Me.theInputFileReader.ReadSByte()

					aVertex.normalX = Me.theInputFileReader.ReadByte()
					aVertex.normalY = Me.theInputFileReader.ReadByte()
					aVertex.texCoordU = Me.theInputFileReader.ReadByte()
					aVertex.normalZ = Me.theInputFileReader.ReadByte()
					aVertex.texCoordV = Me.theInputFileReader.ReadByte()

					aModel.theVertexesType2.Add(aVertex)

					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexesType2 " + aModel.theVertexesType2.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadTangents(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel2531)
		If aModel.vertexCount > 0 Then
			'Dim hitboxInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.tangentOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theTangents = New List(Of SourceMdlTangent2531)(aModel.vertexCount)
				For j As Integer = 0 To aModel.vertexCount - 1
					'hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aTangent As New SourceMdlTangent2531()

					aTangent.x = Me.theInputFileReader.ReadSingle()
					aTangent.y = Me.theInputFileReader.ReadSingle()
					aTangent.z = Me.theInputFileReader.ReadSingle()
					aTangent.w = Me.theInputFileReader.ReadSingle()

					aModel.theTangents.Add(aTangent)

					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTangent")

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theTangents " + aModel.theTangents.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadAnimations(ByVal animationDescInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc2531)
		If Me.theMdlFileData.boneCount > 0 AndAlso anAnimationDesc.animOffset <> 0 Then
			Dim animationInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.animOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				anAnimationDesc.theAnimations = New List(Of SourceMdlAnimation2531)(Me.theMdlFileData.boneCount)
				For boneIndex As Integer = 0 To Me.theMdlFileData.boneCount - 1
					animationInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim anAnimation As New SourceMdlAnimation2531()

					'anAnimation.flags = Me.theInputFileReader.ReadInt32()
					'If (anAnimation.flags And SourceMdlAnimation2531.STUDIO_POS_ANIMATED) > 0 Then
					'	anAnimation.theOffsets(0) = Me.theInputFileReader.ReadInt32()
					'	anAnimation.theOffsets(1) = Me.theInputFileReader.ReadInt32()
					'	anAnimation.theOffsets(2) = Me.theInputFileReader.ReadInt32()

					'	inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'	anAnimation.thePositionAnimationXValues = New List(Of SourceMdlAnimationValue2531)()
					'	anAnimation.thePositionAnimationYValues = New List(Of SourceMdlAnimationValue2531)()
					'	anAnimation.thePositionAnimationZValues = New List(Of SourceMdlAnimationValue2531)()
					'	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(0), anAnimationDesc.frameCount, anAnimation.thePositionAnimationXValues, "anAnimation.thePositionAnimationXValues")
					'	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(1), anAnimationDesc.frameCount, anAnimation.thePositionAnimationYValues, "anAnimation.thePositionAnimationYValues")
					'	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(2), anAnimationDesc.frameCount, anAnimation.thePositionAnimationZValues, "anAnimation.thePositionAnimationZValues")

					'	Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					'Else
					'	anAnimation.thePosition = New SourceVector()
					'	anAnimation.thePosition.x = Me.theInputFileReader.ReadSingle()
					'	anAnimation.thePosition.y = Me.theInputFileReader.ReadSingle()
					'	anAnimation.thePosition.z = Me.theInputFileReader.ReadSingle()
					'End If
					'If (anAnimation.flags And SourceMdlAnimation2531.STUDIO_ROT_ANIMATED) > 0 Then
					'	anAnimation.theOffsets(3) = Me.theInputFileReader.ReadInt32()
					'	anAnimation.theOffsets(4) = Me.theInputFileReader.ReadInt32()
					'	anAnimation.theOffsets(5) = Me.theInputFileReader.ReadInt32()

					'	inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'	anAnimation.theRotationAnimationXValues = New List(Of SourceMdlAnimationValue2531)()
					'	anAnimation.theRotationAnimationYValues = New List(Of SourceMdlAnimationValue2531)()
					'	anAnimation.theRotationAnimationZValues = New List(Of SourceMdlAnimationValue2531)()
					'	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(3), anAnimationDesc.frameCount, anAnimation.theRotationAnimationXValues, "anAnimation.theRotationAnimationXValues")
					'	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(4), anAnimationDesc.frameCount, anAnimation.theRotationAnimationYValues, "anAnimation.theRotationAnimationYValues")
					'	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(5), anAnimationDesc.frameCount, anAnimation.theRotationAnimationZValues, "anAnimation.theRotationAnimationZValues")

					'	Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					'Else
					'	anAnimation.theRotation = New SourceQuaternion()
					'	anAnimation.theRotation.x = Me.theInputFileReader.ReadSingle()
					'	anAnimation.theRotation.y = Me.theInputFileReader.ReadSingle()
					'	anAnimation.theRotation.z = Me.theInputFileReader.ReadSingle()
					'	anAnimation.theRotation.w = Me.theInputFileReader.ReadSingle()
					'End If
					'------
					anAnimation.unknown = Me.theInputFileReader.ReadSingle()
					anAnimation.theOffsets(0) = Me.theInputFileReader.ReadInt32()
					anAnimation.theOffsets(1) = Me.theInputFileReader.ReadInt32()
					anAnimation.theOffsets(2) = Me.theInputFileReader.ReadInt32()
					anAnimation.theOffsets(3) = Me.theInputFileReader.ReadInt32()
					anAnimation.theOffsets(4) = Me.theInputFileReader.ReadInt32()
					anAnimation.theOffsets(5) = Me.theInputFileReader.ReadInt32()
					anAnimation.theOffsets(6) = Me.theInputFileReader.ReadInt32()

					anAnimationDesc.theAnimations.Add(anAnimation)

					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If anAnimation.theOffsets(0) > 0 Then
						Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(0), anAnimationDesc.frameCount, anAnimation.thePositionAnimationXValues, "anAnimation.thePositionAnimationXValues")
					End If
					If anAnimation.theOffsets(1) > 0 Then
						Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(1), anAnimationDesc.frameCount, anAnimation.thePositionAnimationYValues, "anAnimation.thePositionAnimationYValues")
					End If
					If anAnimation.theOffsets(2) > 0 Then
						Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(2), anAnimationDesc.frameCount, anAnimation.thePositionAnimationZValues, "anAnimation.thePositionAnimationZValues")
					End If

					If anAnimation.theOffsets(3) > 0 Then
						Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(3), anAnimationDesc.frameCount, anAnimation.theRotationAnimationXValues, "anAnimation.theRotationAnimationXValues")
					End If
					If anAnimation.theOffsets(4) > 0 Then
						Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(4), anAnimationDesc.frameCount, anAnimation.theRotationAnimationYValues, "anAnimation.theRotationAnimationYValues")
					End If
					If anAnimation.theOffsets(5) > 0 Then
						Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(5), anAnimationDesc.frameCount, anAnimation.theRotationAnimationZValues, "anAnimation.theRotationAnimationZValues")
					End If
					If anAnimation.theOffsets(6) > 0 Then
						Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(6), anAnimationDesc.frameCount, anAnimation.theRotationAnimationWValues, "anAnimation.theRotationAnimationWValues")
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAnimations " + anAnimationDesc.theAnimations.Count.ToString())

				'Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "anAnimationDesc.theMeshes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadAnimationValues(ByVal animationValuesInputFileStreamPosition As Long, ByVal frameCount As Integer, ByVal animationValues As List(Of SourceMdlAnimationValue2531), ByVal debugDescription As String)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim frameCountRemainingToBeChecked As Integer
		Dim anAnimationValue As New SourceMdlAnimationValue2531()
		Dim currentTotal As Byte
		Dim validCount As Byte

		Try
			Me.theInputFileReader.BaseStream.Seek(animationValuesInputFileStreamPosition, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			frameCountRemainingToBeChecked = frameCount
			While (frameCountRemainingToBeChecked > 0)
				anAnimationValue.value = Me.theInputFileReader.ReadInt16()
				currentTotal = anAnimationValue.total
				If currentTotal = 0 Then
					Dim badIfThisIsReached As Integer = 42
					Exit While
				End If
				frameCountRemainingToBeChecked -= currentTotal
				animationValues.Add(anAnimationValue)

				validCount = anAnimationValue.valid
				For i As Integer = 1 To validCount
					anAnimationValue.value = Me.theInputFileReader.ReadInt16()
					animationValues.Add(anAnimationValue)
				Next
			End While

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, debugDescription)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Function GetSplit(ByVal aFlex As SourceMdlFlex2531, ByVal meshVertexIndexStart As Integer) As Double
		'TODO: Reverse these calculations to get split number.
		'      Yikes! This really should be run over *all* vertex anims to get the exact split number.
		'float scale = 1.0;
		'float side = 0.0;
		'if (g_flexkey[i].split > 0)
		'{
		'	if (psrcanim->pos.x > g_flexkey[i].split) 
		'	{
		'		scale = 0;
		'	}
		'	else if (psrcanim->pos.x < -g_flexkey[i].split) 
		'	{
		'		scale = 1.0;
		'	}
		'	else
		'	{
		'		float t = (g_flexkey[i].split - psrcanim->pos.x) / (2.0 * g_flexkey[i].split);
		'		scale = 3 * t * t - 2 * t * t * t;
		'	}
		'}
		'else if (g_flexkey[i].split < 0)
		'{
		'	if (psrcanim->pos.x < g_flexkey[i].split) 
		'	{
		'		scale = 0;
		'	}
		'	else if (psrcanim->pos.x > -g_flexkey[i].split) 
		'	{
		'		scale = 1.0;
		'	}
		'	else
		'	{
		'		float t = (g_flexkey[i].split - psrcanim->pos.x) / (2.0 * g_flexkey[i].split);
		'		scale = 3 * t * t - 2 * t * t * t;
		'	}
		'}
		'side = 1.0 - scale;
		'pvertanim->side  = 255.0F*pvanim->side;



		'Dim aVertex As SourceVertex
		'Dim vertexIndex As Integer
		'Dim aVertAnim As SourceMdlVertAnim
		'Dim side As Double
		'Dim scale As Double
		'Dim split As Double
		'aVertAnim = aFlex.theVertAnims(0)
		'vertexIndex = aVertAnim.index + meshVertexIndexStart
		'If Me.theSourceEngineModel.theVvdFileHeader.fixupCount = 0 Then
		'	aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(vertexIndex)
		'Else
		'	'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
		'	'      Maybe the listing by lodIndex is only needed internally by graphics engine.
		'	'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
		'	aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(0)(vertexIndex)
		'End If
		'side = aVertAnim.side / 255
		'scale = 1 - side
		'If scale = 1 Then
		'	split = -(aVertex.positionX - 1)
		'ElseIf scale = 0 Then
		'Else
		'End If

		Return 1
	End Function

#End Region

#Region "Data"

	Protected theInputFileReader As BinaryReader
	Protected theOutputFileWriter As BinaryWriter

	Protected theMdlFileData As SourceMdlFileData2531

#End Region

End Class
