Imports System.IO
Imports System.Text

Public Class SourceMdlFile37

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlFileReader As BinaryReader, ByVal mdlFileData As SourceMdlFileData37)
		Me.theInputFileReader = mdlFileReader
		Me.theMdlFileData = mdlFileData

		Me.theMdlFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length
	End Sub

	Public Sub New(ByVal mdlFileWriter As BinaryWriter, ByVal mdlFileData As SourceMdlFileData37)
		Me.theOutputFileWriter = mdlFileWriter
		Me.theMdlFileData = mdlFileData
	End Sub

#End Region

#Region "Methods"

	Public Sub ReadMdlHeader00(ByVal logDescription As String)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theMdlFileData.id = Me.theInputFileReader.ReadChars(4)
		Me.theMdlFileData.theID = Me.theMdlFileData.id
		Me.theMdlFileData.version = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.checksum = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.name = Me.theInputFileReader.ReadChars(Me.theMdlFileData.name.Length)
		Me.theMdlFileData.theModelName = CStr(Me.theMdlFileData.name).Trim(Chr(0))

		Me.theMdlFileData.fileSize = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.theActualFileSize = Me.theInputFileReader.BaseStream.Length

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		If logDescription <> "" Then
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription + " (Actual version: " + Me.theMdlFileData.version.ToString() + "; override version: 37)")
		End If
	End Sub

	Public Sub ReadMdlHeader01(ByVal logDescription As String)
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theMdlFileData.eyePosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.eyePosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.eyePosition.z = Me.theInputFileReader.ReadSingle()

		Me.theMdlFileData.illuminationPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.illuminationPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.illuminationPosition.z = Me.theInputFileReader.ReadSingle()

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

		Me.theMdlFileData.hitboxSetCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.hitboxSetOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.animationCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.animationOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.animationGroupCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.animationGroupOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.boneDescCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneDescOffset = Me.theInputFileReader.ReadInt32()

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

		If Me.theMdlFileData.surfacePropOffset > 0 Then
			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.surfacePropOffset, SeekOrigin.Begin)
			fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

			fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
			If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theSurfacePropName = " + Me.theMdlFileData.theSurfacePropName)
			End If
			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Else
			Me.theMdlFileData.theSurfacePropName = ""
		End If

		Me.theMdlFileData.keyValueOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.keyValueSize = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.localIkAutoPlayLockCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localIkAutoPlayLockOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.mass = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.contents = Me.theInputFileReader.ReadInt32()

		For x As Integer = 0 To Me.theMdlFileData.unused.Length - 1
			Me.theMdlFileData.unused(x) = Me.theInputFileReader.ReadInt32()
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription)

		If Me.theMdlFileData.bodyPartCount = 0 AndAlso Me.theMdlFileData.localSequenceCount > 0 Then
			Me.theMdlFileData.theMdlFileOnlyHasAnimations = True
		End If
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
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theBones = New List(Of SourceMdlBone37)(Me.theMdlFileData.boneCount)
				For boneIndex As Integer = 0 To Me.theMdlFileData.boneCount - 1
					boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBone As New SourceMdlBone37()

					aBone.nameOffset = Me.theInputFileReader.ReadInt32()
					aBone.parentBoneIndex = Me.theInputFileReader.ReadInt32()

					For j As Integer = 0 To aBone.boneControllerIndex.Length - 1
						aBone.boneControllerIndex(j) = Me.theInputFileReader.ReadInt32()
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

					aBone.poseToBoneColumn0 = New SourceVector()
					aBone.poseToBoneColumn1 = New SourceVector()
					aBone.poseToBoneColumn2 = New SourceVector()
					aBone.poseToBoneColumn3 = New SourceVector()
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

					aBone.qAlignment = New SourceQuaternion()
					aBone.qAlignment.x = Me.theInputFileReader.ReadSingle()
					aBone.qAlignment.y = Me.theInputFileReader.ReadSingle()
					aBone.qAlignment.z = Me.theInputFileReader.ReadSingle()
					aBone.qAlignment.w = Me.theInputFileReader.ReadSingle()

					aBone.flags = Me.theInputFileReader.ReadInt32()

					aBone.proceduralRuleType = Me.theInputFileReader.ReadInt32()
					aBone.proceduralRuleOffset = Me.theInputFileReader.ReadInt32()
					aBone.physicsBoneIndex = Me.theInputFileReader.ReadInt32()
					aBone.surfacePropNameOffset = Me.theInputFileReader.ReadInt32()

					aBone.quat = New SourceQuaternion()
					aBone.quat.x = Me.theInputFileReader.ReadSingle()
					aBone.quat.y = Me.theInputFileReader.ReadSingle()
					aBone.quat.z = Me.theInputFileReader.ReadSingle()
					aBone.quat.w = Me.theInputFileReader.ReadSingle()

					aBone.contents = Me.theInputFileReader.ReadInt32()

					For x As Integer = 0 To aBone.unused.Length - 1
						aBone.unused(x) = Me.theInputFileReader.ReadInt32()
					Next

					Me.theMdlFileData.theBones.Add(aBone)

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
						If aBone.proceduralRuleType = SourceMdlBone37.STUDIO_PROC_AXISINTERP Then
							Me.ReadAxisInterpBone(boneInputFileStreamPosition, aBone)
						ElseIf aBone.proceduralRuleType = SourceMdlBone37.STUDIO_PROC_QUATINTERP Then
							Me.theMdlFileData.theProceduralBonesCommandIsUsed = True
							Me.ReadQuatInterpBone(boneInputFileStreamPosition, aBone)
							'ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
							'	Me.ReadJiggleBone(boneInputFileStreamPosition, aBone)
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
			Dim boneControllerInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneControllerOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theBoneControllers = New List(Of SourceMdlBoneController37)(Me.theMdlFileData.boneControllerCount)
				For i As Integer = 0 To Me.theMdlFileData.boneControllerCount - 1
					boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBoneController As New SourceMdlBoneController37()

					aBoneController.boneIndex = Me.theInputFileReader.ReadInt32()
					aBoneController.type = Me.theInputFileReader.ReadInt32()
					aBoneController.startBlah = Me.theInputFileReader.ReadSingle()
					aBoneController.endBlah = Me.theInputFileReader.ReadSingle()
					aBoneController.restIndex = Me.theInputFileReader.ReadInt32()
					aBoneController.inputField = Me.theInputFileReader.ReadInt32()
					For x As Integer = 0 To aBoneController.unused.Length - 1
						aBoneController.unused(x) = Me.theInputFileReader.ReadByte()
					Next

					Me.theMdlFileData.theBoneControllers.Add(aBoneController)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'If aBoneController.nameOffset <> 0 Then
					'	Me.theInputFileReader.BaseStream.Seek(boneControllerInputFileStreamPosition + aBoneController.nameOffset, SeekOrigin.Begin)
					'	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					'	aBoneController.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					'	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName")
					'	End If
					'Else
					'	aBoneController.theName = ""
					'End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers " + Me.theMdlFileData.theBoneControllers.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment")
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

				Me.theMdlFileData.theAttachments = New List(Of SourceMdlAttachment37)(Me.theMdlFileData.localAttachmentCount)
				For i As Integer = 0 To Me.theMdlFileData.localAttachmentCount - 1
					attachmentInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim anAttachment As New SourceMdlAttachment37()

					anAttachment.nameOffset = Me.theInputFileReader.ReadInt32
					anAttachment.type = Me.theInputFileReader.ReadInt32()
					anAttachment.boneIndex = Me.theInputFileReader.ReadInt32()
					anAttachment.localM11 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM12 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM13 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM14 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM21 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM22 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM23 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM24 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM31 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM32 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM33 = Me.theInputFileReader.ReadSingle()
					anAttachment.localM34 = Me.theInputFileReader.ReadSingle()

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
					Else
						anAttachment.theName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments " + Me.theMdlFileData.theAttachments.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment")
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
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.hitboxSetOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theHitboxSets = New List(Of SourceMdlHitboxSet37)(Me.theMdlFileData.hitboxSetCount)
				For i As Integer = 0 To Me.theMdlFileData.hitboxSetCount - 1
					hitboxSetInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aHitboxSet As New SourceMdlHitboxSet37()

					aHitboxSet.nameOffset = Me.theInputFileReader.ReadInt32()
					aHitboxSet.hitboxCount = Me.theInputFileReader.ReadInt32()
					aHitboxSet.hitboxOffset = Me.theInputFileReader.ReadInt32()

					Me.theMdlFileData.theHitboxSets.Add(aHitboxSet)

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

					Me.ReadHitboxes(hitboxSetInputFileStreamPosition, aHitboxSet)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxSets " + Me.theMdlFileData.theHitboxSets.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theHitboxSets alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadBoneDescs()
		If Me.theMdlFileData.boneDescCount > 0 Then
			Dim boneDescInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneDescOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theBoneDescs = New List(Of SourceMdlBoneDesc37)(Me.theMdlFileData.boneDescCount)
				For i As Integer = 0 To Me.theMdlFileData.boneCount - 1
					boneDescInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBoneDesc As New SourceMdlBoneDesc37()

					aBoneDesc.nameOffset = Me.theInputFileReader.ReadInt32()
					aBoneDesc.parentBoneIndex = Me.theInputFileReader.ReadInt32()

					aBoneDesc.position = New SourceVector()
					aBoneDesc.position.x = Me.theInputFileReader.ReadSingle()
					aBoneDesc.position.y = Me.theInputFileReader.ReadSingle()
					aBoneDesc.position.z = Me.theInputFileReader.ReadSingle()
					aBoneDesc.rotation = New SourceVector()
					aBoneDesc.rotation.x = Me.theInputFileReader.ReadSingle()
					aBoneDesc.rotation.y = Me.theInputFileReader.ReadSingle()
					aBoneDesc.rotation.z = Me.theInputFileReader.ReadSingle()
					aBoneDesc.positionScale = New SourceVector()
					aBoneDesc.positionScale.x = Me.theInputFileReader.ReadSingle()
					aBoneDesc.positionScale.y = Me.theInputFileReader.ReadSingle()
					aBoneDesc.positionScale.z = Me.theInputFileReader.ReadSingle()
					aBoneDesc.rotationScale = New SourceVector()
					aBoneDesc.rotationScale.x = Me.theInputFileReader.ReadSingle()
					aBoneDesc.rotationScale.y = Me.theInputFileReader.ReadSingle()
					aBoneDesc.rotationScale.z = Me.theInputFileReader.ReadSingle()

					aBoneDesc.poseToBoneColumn0 = New SourceVector()
					aBoneDesc.poseToBoneColumn1 = New SourceVector()
					aBoneDesc.poseToBoneColumn2 = New SourceVector()
					aBoneDesc.poseToBoneColumn3 = New SourceVector()
					aBoneDesc.poseToBoneColumn0.x = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn1.x = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn2.x = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn3.x = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn0.y = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn1.y = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn2.y = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn3.y = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn0.z = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn1.z = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn2.z = Me.theInputFileReader.ReadSingle()
					aBoneDesc.poseToBoneColumn3.z = Me.theInputFileReader.ReadSingle()

					For x As Integer = 0 To aBoneDesc.unused.Length - 1
						aBoneDesc.unused(x) = Me.theInputFileReader.ReadInt32()
					Next

					Me.theMdlFileData.theBoneDescs.Add(aBoneDesc)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aBoneDesc.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(boneDescInputFileStreamPosition + aBoneDesc.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aBoneDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBoneDesc.theName = " + aBoneDesc.theName)
						End If
					ElseIf aBoneDesc.theName Is Nothing Then
						aBoneDesc.theName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneDescs " + Me.theMdlFileData.theBoneDescs.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneDescs alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadAnimGroups()
		If Me.theMdlFileData.animationGroupCount > 0 Then
			'Dim seqInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.animationGroupOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theAnimGroups = New List(Of SourceMdlAnimGroup37)(Me.theMdlFileData.animationGroupCount)
				For i As Integer = 0 To Me.theMdlFileData.animationGroupCount - 1
					'seqInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anAnimGroup As New SourceMdlAnimGroup37()

					anAnimGroup.group = Me.theInputFileReader.ReadInt32()
					anAnimGroup.index = Me.theInputFileReader.ReadInt32()

					Me.theMdlFileData.theAnimGroups.Add(anAnimGroup)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimGroups " + Me.theMdlFileData.theAnimGroups.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAnimGroups alignment")
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
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theSequenceDescs = New List(Of SourceMdlSequenceDesc37)(Me.theMdlFileData.localSequenceCount)
				For i As Integer = 0 To Me.theMdlFileData.localSequenceCount - 1
					seqInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aSeqDesc As New SourceMdlSequenceDesc37()

					aSeqDesc.nameOffset = Me.theInputFileReader.ReadInt32()
					aSeqDesc.activityNameOffset = Me.theInputFileReader.ReadInt32()
					aSeqDesc.flags = Me.theInputFileReader.ReadInt32()
					aSeqDesc.activity = Me.theInputFileReader.ReadInt32()
					aSeqDesc.activityWeight = Me.theInputFileReader.ReadInt32()
					aSeqDesc.eventCount = Me.theInputFileReader.ReadInt32()
					aSeqDesc.eventOffset = Me.theInputFileReader.ReadInt32()

					aSeqDesc.bbMin.x = Me.theInputFileReader.ReadSingle()
					aSeqDesc.bbMin.y = Me.theInputFileReader.ReadSingle()
					aSeqDesc.bbMin.z = Me.theInputFileReader.ReadSingle()
					aSeqDesc.bbMax.x = Me.theInputFileReader.ReadSingle()
					aSeqDesc.bbMax.y = Me.theInputFileReader.ReadSingle()
					aSeqDesc.bbMax.z = Me.theInputFileReader.ReadSingle()

					aSeqDesc.blendCount = Me.theInputFileReader.ReadInt32()
					aSeqDesc.blendOffset = Me.theInputFileReader.ReadInt32()

					aSeqDesc.sequenceGroup = Me.theInputFileReader.ReadInt32()

					aSeqDesc.groupSize(0) = Me.theInputFileReader.ReadInt32()
					aSeqDesc.groupSize(1) = Me.theInputFileReader.ReadInt32()
					aSeqDesc.paramIndex(0) = Me.theInputFileReader.ReadInt32()
					aSeqDesc.paramIndex(1) = Me.theInputFileReader.ReadInt32()
					aSeqDesc.paramStart(0) = Me.theInputFileReader.ReadSingle()
					aSeqDesc.paramStart(1) = Me.theInputFileReader.ReadSingle()
					aSeqDesc.paramEnd(0) = Me.theInputFileReader.ReadSingle()
					aSeqDesc.paramEnd(1) = Me.theInputFileReader.ReadSingle()
					aSeqDesc.paramParent = Me.theInputFileReader.ReadInt32()

					aSeqDesc.fadeInTime = Me.theInputFileReader.ReadSingle()
					aSeqDesc.fadeOutTime = Me.theInputFileReader.ReadSingle()

					aSeqDesc.entryNodeIndex = Me.theInputFileReader.ReadInt32()
					aSeqDesc.exitNodeIndex = Me.theInputFileReader.ReadInt32()
					aSeqDesc.nodeFlags = Me.theInputFileReader.ReadInt32()

					aSeqDesc.entryPhase = Me.theInputFileReader.ReadSingle()
					aSeqDesc.exitPhase = Me.theInputFileReader.ReadSingle()
					aSeqDesc.lastFrame = Me.theInputFileReader.ReadSingle()

					aSeqDesc.nextSeq = Me.theInputFileReader.ReadInt32()
					aSeqDesc.pose = Me.theInputFileReader.ReadInt32()

					aSeqDesc.ikRuleCount = Me.theInputFileReader.ReadInt32()
					aSeqDesc.autoLayerCount = Me.theInputFileReader.ReadInt32()
					aSeqDesc.autoLayerOffset = Me.theInputFileReader.ReadInt32()
					aSeqDesc.weightOffset = Me.theInputFileReader.ReadInt32()
					aSeqDesc.poseKeyOffset = Me.theInputFileReader.ReadInt32()

					aSeqDesc.ikLockCount = Me.theInputFileReader.ReadInt32()
					aSeqDesc.ikLockOffset = Me.theInputFileReader.ReadInt32()
					aSeqDesc.keyValueOffset = Me.theInputFileReader.ReadInt32()
					aSeqDesc.keyValueSize = Me.theInputFileReader.ReadInt32()

					For x As Integer = 0 To aSeqDesc.unused.Length - 1
						aSeqDesc.unused(x) = Me.theInputFileReader.ReadInt32()
					Next

					Me.theMdlFileData.theSequenceDescs.Add(aSeqDesc)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aSeqDesc.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aSeqDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theName = " + aSeqDesc.theName)
					Else
						aSeqDesc.theName = ""
					End If

					If aSeqDesc.activityNameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.activityNameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aSeqDesc.theActivityName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theActivityName = " + aSeqDesc.theActivityName)
						End If
					Else
						aSeqDesc.theActivityName = ""
					End If

					Me.ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc)
					Me.ReadEvents(seqInputFileStreamPosition, aSeqDesc)
					Me.ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc)
					Me.ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc)
					Me.ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc)
					Me.ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc)
					Me.ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceDescs " + Me.theMdlFileData.theSequenceDescs.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadSequenceGroups()
		If Me.theMdlFileData.sequenceGroupCount > 0 Then
			Dim sequenceGroupInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.sequenceGroupOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theSequenceGroups = New List(Of SourceMdlSequenceGroup37)(Me.theMdlFileData.sequenceGroupCount)
				Me.theMdlFileData.theIncludeModelFileNames = New List(Of String)()
				For sequenceGroupIndex As Integer = 0 To Me.theMdlFileData.sequenceGroupCount - 1
					sequenceGroupInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aSequenceGroup As New SourceMdlSequenceGroup37()

					aSequenceGroup.nameOffset = Me.theInputFileReader.ReadInt32()
					aSequenceGroup.fileNameOffset = Me.theInputFileReader.ReadInt32()
					aSequenceGroup.cacheOffset = Me.theInputFileReader.ReadInt32()
					aSequenceGroup.data = Me.theInputFileReader.ReadInt32()

					Me.theMdlFileData.theSequenceGroups.Add(aSequenceGroup)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aSequenceGroup.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(sequenceGroupInputFileStreamPosition + aSequenceGroup.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aSequenceGroup.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequenceGroup.theName = " + aSequenceGroup.theName)
					Else
						aSequenceGroup.theName = ""
					End If

					If aSequenceGroup.fileNameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(sequenceGroupInputFileStreamPosition + aSequenceGroup.fileNameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aSequenceGroup.theFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequenceGroup.theFileName = " + aSequenceGroup.theFileName)
						End If
					Else
						aSequenceGroup.theFileName = ""
					End If

					If aSequenceGroup.theName = "shared_animation" Then
						Me.theMdlFileData.theIncludeModelFileNames.Add(aSequenceGroup.theFileName)
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
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

				Me.theMdlFileData.theTransitions = New List(Of List(Of Integer))(Me.theMdlFileData.transitionCount)
				For entryNodeIndex As Integer = 0 To Me.theMdlFileData.transitionCount - 1
					Dim exitNodeTransitions As New List(Of Integer)(Me.theMdlFileData.transitionCount)
					For exitNodeIndex As Integer = 0 To Me.theMdlFileData.transitionCount - 1
						Dim aTransitionValue As Integer

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

	Public Sub ReadLocalAnimationDescs()
		If Me.theMdlFileData.animationCount > 0 Then
			Dim animationDescInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.animationOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theAnimationDescs = New List(Of SourceMdlAnimationDesc37)(Me.theMdlFileData.animationCount)
				For i As Integer = 0 To Me.theMdlFileData.animationCount - 1
					animationDescInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim anAnimationDesc As New SourceMdlAnimationDesc37()

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

					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If anAnimationDesc.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						anAnimationDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)
						If Me.theMdlFileData.theFirstAnimationDesc Is Nothing AndAlso anAnimationDesc.theName(0) <> "@" Then
							Me.theMdlFileData.theFirstAnimationDesc = anAnimationDesc
						End If
						If anAnimationDesc.theName(0) = "@" Then
							anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 1)
						End If

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAnimationDesc.theName = " + anAnimationDesc.theName)
						End If
					Else
						anAnimationDesc.theName = ""
					End If

					Me.ReadAnimations(animationDescInputFileStreamPosition, anAnimationDesc)
					Me.ReadMdlMovements(animationDescInputFileStreamPosition, anAnimationDesc)
					Me.ReadMdlIkRules(animationDescInputFileStreamPosition, anAnimationDesc)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs " + Me.theMdlFileData.theAnimationDescs.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAnimationDescs alignment")
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

				Me.theMdlFileData.theBodyParts = New List(Of SourceMdlBodyPart37)(Me.theMdlFileData.bodyPartCount)
				For i As Integer = 0 To Me.theMdlFileData.bodyPartCount - 1
					bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aBodyPart As New SourceMdlBodyPart37()

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
					'NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts *and* models.
					If i = Me.theMdlFileData.bodyPartCount - 1 Then
						Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, Me.theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment")
					End If

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

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexDescOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theFlexDescs = New List(Of SourceMdlFlexDesc)(Me.theMdlFileData.flexDescCount)
				For i As Integer = 0 To Me.theMdlFileData.flexDescCount - 1
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
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs " + theMdlFileData.theFlexDescs.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexDescs alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
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

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexControllerOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theFlexControllers = New List(Of SourceMdlFlexController)(Me.theMdlFileData.flexControllerCount)
				For i As Integer = 0 To Me.theMdlFileData.flexControllerCount - 1
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
				Next

				If Me.theMdlFileData.theFlexControllers.Count > 0 Then
					Me.theMdlFileData.theModelCommandIsUsed = True
				End If

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllers " + theMdlFileData.theFlexControllers.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexControllers alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
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

			Try
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
					Me.ReadFlexOps(flexRuleInputFileStreamPosition, aFlexRule)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				If Me.theMdlFileData.theFlexRules.Count > 0 Then
					Me.theMdlFileData.theModelCommandIsUsed = True
				End If

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules " + theMdlFileData.theFlexRules.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexRules alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadIkChains()
		If Me.theMdlFileData.ikChainCount > 0 Then
			Dim ikChainInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.ikChainOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theIkChains = New List(Of SourceMdlIkChain37)(Me.theMdlFileData.ikChainCount)
				For i As Integer = 0 To Me.theMdlFileData.ikChainCount - 1
					ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anIkChain As New SourceMdlIkChain37()

					anIkChain.nameOffset = Me.theInputFileReader.ReadInt32()
					anIkChain.linkType = Me.theInputFileReader.ReadInt32()
					anIkChain.linkCount = Me.theInputFileReader.ReadInt32()
					anIkChain.linkOffset = Me.theInputFileReader.ReadInt32()

					Me.theMdlFileData.theIkChains.Add(anIkChain)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If anIkChain.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						anIkChain.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkChain.theName = " + anIkChain.theName)
						End If
					Else
						anIkChain.theName = ""
					End If

					Me.ReadIkLinks(ikChainInputFileStreamPosition, anIkChain)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkChains " + theMdlFileData.theIkChains.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theIkChains alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadIkLocks()
		If Me.theMdlFileData.localIkAutoPlayLockCount > 0 Then
			'Dim ikChainInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localIkAutoPlayLockOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theIkLocks = New List(Of SourceMdlIkLock37)(Me.theMdlFileData.localIkAutoPlayLockCount)
				For i As Integer = 0 To Me.theMdlFileData.localIkAutoPlayLockCount - 1
					'ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anIkLock As New SourceMdlIkLock37()

					anIkLock.chainIndex = Me.theInputFileReader.ReadInt32()
					anIkLock.posWeight = Me.theInputFileReader.ReadSingle()
					anIkLock.localQWeight = Me.theInputFileReader.ReadSingle()

					Me.theMdlFileData.theIkLocks.Add(anIkLock)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkLocks " + theMdlFileData.theIkLocks.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theIkLocks alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadMouths()
		If Me.theMdlFileData.mouthCount > 0 Then
			'Dim mouthInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.mouthOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theMouths = New List(Of SourceMdlMouth)(Me.theMdlFileData.mouthCount)
				For i As Integer = 0 To Me.theMdlFileData.mouthCount - 1
					'mouthInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aMouth As New SourceMdlMouth()
					aMouth.boneIndex = Me.theInputFileReader.ReadInt32()
					aMouth.forward.x = Me.theInputFileReader.ReadSingle()
					aMouth.forward.y = Me.theInputFileReader.ReadSingle()
					aMouth.forward.z = Me.theInputFileReader.ReadSingle()
					aMouth.flexDescIndex = Me.theInputFileReader.ReadInt32()
					Me.theMdlFileData.theMouths.Add(aMouth)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				If Me.theMdlFileData.theMouths.Count > 0 Then
					Me.theMdlFileData.theModelCommandIsUsed = True
				End If

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theMouths " + theMdlFileData.theMouths.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theMouths alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
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
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.thePoseParamDescs = New List(Of SourceMdlPoseParamDesc)(Me.theMdlFileData.localPoseParamaterCount)
				For i As Integer = 0 To Me.theMdlFileData.localPoseParamaterCount - 1
					poseInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aPoseParamDesc As New SourceMdlPoseParamDesc()
					aPoseParamDesc.nameOffset = Me.theInputFileReader.ReadInt32()
					aPoseParamDesc.flags = Me.theInputFileReader.ReadInt32()
					aPoseParamDesc.startingValue = Me.theInputFileReader.ReadSingle()
					aPoseParamDesc.endingValue = Me.theInputFileReader.ReadSingle()
					aPoseParamDesc.loopingRange = Me.theInputFileReader.ReadSingle()
					Me.theMdlFileData.thePoseParamDescs.Add(aPoseParamDesc)

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

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs " + theMdlFileData.thePoseParamDescs.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.thePoseParamDescs alignment")
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

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.textureOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theTextures = New List(Of SourceMdlTexture37)(Me.theMdlFileData.textureCount)
			For i As Integer = 0 To Me.theMdlFileData.textureCount - 1
				textureInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aTexture As New SourceMdlTexture37()

				aTexture.fileNameOffset = Me.theInputFileReader.ReadInt32()
				aTexture.flags = Me.theInputFileReader.ReadInt32()
				aTexture.width = Me.theInputFileReader.ReadSingle()
				aTexture.height = Me.theInputFileReader.ReadSingle()
				aTexture.worldUnitsPerU = Me.theInputFileReader.ReadSingle()
				aTexture.worldUnitsPerV = Me.theInputFileReader.ReadSingle()
				For x As Integer = 0 To aTexture.unknown.Length - 1
					aTexture.unknown(x) = Me.theInputFileReader.ReadInt32()
				Next

				Me.theMdlFileData.theTextures.Add(aTexture)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aTexture.fileNameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.fileNameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aTexture.theFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					' Convert all forward slashes to backward slashes.
					aTexture.theFileName = FileManager.GetNormalizedPathFileName(aTexture.theFileName)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.theName = " + aTexture.theFileName)
					End If
				Else
					aTexture.theFileName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTextures")

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment")
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

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.texturePathOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theTexturePaths = New List(Of String)(Me.theMdlFileData.texturePathCount)
			Dim texturePathOffset As Integer
			For i As Integer = 0 To Me.theMdlFileData.texturePathCount - 1
				texturePathInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aTexturePath As String
				texturePathOffset = Me.theInputFileReader.ReadInt32()

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If texturePathOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aTexturePath = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					'TEST: Convert all forward slashes to backward slashes.
					aTexturePath = FileManager.GetNormalizedPathFileName(aTexturePath)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath = " + aTexturePath)
					End If
				Else
					aTexturePath = ""
				End If
				Me.theMdlFileData.theTexturePaths.Add(aTexturePath)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTexturePaths")

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTexturePaths alignment")
		End If
	End Sub

	Public Sub ReadSkinFamilies()
		If Me.theMdlFileData.skinFamilyCount > 0 AndAlso Me.theMdlFileData.skinReferenceCount > 0 Then
			Dim skinFamilyInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.skinOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theSkinFamilies = New List(Of List(Of Short))(Me.theMdlFileData.skinFamilyCount)
			For i As Integer = 0 To Me.theMdlFileData.skinFamilyCount - 1
				skinFamilyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aSkinFamily As New List(Of Short)()

				For j As Integer = 0 To Me.theMdlFileData.skinReferenceCount - 1
					Dim aSkinRef As Short
					aSkinRef = Me.theInputFileReader.ReadInt16()
					aSkinFamily.Add(aSkinRef)
				Next

				Me.theMdlFileData.theSkinFamilies.Add(aSkinFamily)

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

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies")

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment")
		End If
	End Sub

	Public Sub ReadKeyValues()
		If Me.theMdlFileData.keyValueSize > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim nullChar As Char

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.keyValueOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			'NOTE: Use -1 to drop the null terminator character.
			Me.theMdlFileData.theKeyValuesText = Me.theInputFileReader.ReadChars(Me.theMdlFileData.keyValueSize - 1)
			'NOTE: Read the null terminator character.
			nullChar = Me.theInputFileReader.ReadChar()

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theKeyValuesText = " + Me.theMdlFileData.theKeyValuesText)

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theKeyValuesText alignment")
		End If
	End Sub

	'Public Sub ReadFinalBytesAlignment()
	'	Me.theMdlFileData.theFileSeekLog.LogAndAlignFromFileSeekLogEnd(Me.theInputFileReader, 4, "Final bytes alignment")
	'End Sub

	Public Sub ReadUnreadBytes()
		Me.theMdlFileData.theFileSeekLog.LogUnreadBytes(Me.theInputFileReader)
	End Sub

#End Region

#Region "Private Methods"

	'TODO: VERIFY ReadAxisInterpBone()
	Private Sub ReadAxisInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone37)
		Dim axisInterpBoneInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			axisInterpBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			aBone.theAxisInterpBone = New SourceMdlAxisInterpBone()
			aBone.theAxisInterpBone.control = Me.theInputFileReader.ReadInt32()
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

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
			'	Me.ReadTriggers(axisInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
			'End If

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAxisInterpBone")
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadQuatInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone37)
		Dim quatInterpBoneInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			quatInterpBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			aBone.theQuatInterpBone = New SourceMdlQuatInterpBone()
			aBone.theQuatInterpBone.controlBoneIndex = Me.theInputFileReader.ReadInt32()
			aBone.theQuatInterpBone.triggerCount = Me.theInputFileReader.ReadInt32()
			aBone.theQuatInterpBone.triggerOffset = Me.theInputFileReader.ReadInt32()

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
				Me.ReadTriggers(quatInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
			End If

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theQuatInterpBone")
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadTriggers(ByVal quatInterpBoneInputFileStreamPosition As Long, ByVal aQuatInterpBone As SourceMdlQuatInterpBone)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(quatInterpBoneInputFileStreamPosition + aQuatInterpBone.triggerOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aQuatInterpBone.theTriggers = New List(Of SourceMdlQuatInterpBoneInfo)(aQuatInterpBone.triggerCount)
			For j As Integer = 0 To aQuatInterpBone.triggerCount - 1
				Dim aTrigger As New SourceMdlQuatInterpBoneInfo()

				aTrigger.inverseToleranceAngle = Me.theInputFileReader.ReadSingle()

				aTrigger.trigger = New SourceQuaternion()
				aTrigger.trigger.x = Me.theInputFileReader.ReadSingle()
				aTrigger.trigger.y = Me.theInputFileReader.ReadSingle()
				aTrigger.trigger.z = Me.theInputFileReader.ReadSingle()
				aTrigger.trigger.w = Me.theInputFileReader.ReadSingle()

				aTrigger.pos = New SourceVector()
				aTrigger.pos.x = Me.theInputFileReader.ReadSingle()
				aTrigger.pos.y = Me.theInputFileReader.ReadSingle()
				aTrigger.pos.z = Me.theInputFileReader.ReadSingle()

				aTrigger.quat = New SourceQuaternion()
				aTrigger.quat.x = Me.theInputFileReader.ReadSingle()
				aTrigger.quat.y = Me.theInputFileReader.ReadSingle()
				aTrigger.quat.z = Me.theInputFileReader.ReadSingle()
				aTrigger.quat.w = Me.theInputFileReader.ReadSingle()

				aQuatInterpBone.theTriggers.Add(aTrigger)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aQuatInterpBone.theTriggers " + aQuatInterpBone.theTriggers.Count.ToString())
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadHitboxes(ByVal hitboxSetInputFileStreamPosition As Long, ByVal aHitboxSet As SourceMdlHitboxSet37)
		If aHitboxSet.hitboxCount > 0 Then
			Dim hitboxInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aHitboxSet.theHitboxes = New List(Of SourceMdlHitbox37)(aHitboxSet.hitboxCount)
				For j As Integer = 0 To aHitboxSet.hitboxCount - 1
					hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aHitbox As New SourceMdlHitbox37()

					aHitbox.boneIndex = Me.theInputFileReader.ReadInt32()
					aHitbox.groupIndex = Me.theInputFileReader.ReadInt32()
					aHitbox.boundingBoxMin.x = Me.theInputFileReader.ReadSingle()
					aHitbox.boundingBoxMin.y = Me.theInputFileReader.ReadSingle()
					aHitbox.boundingBoxMin.z = Me.theInputFileReader.ReadSingle()
					aHitbox.boundingBoxMax.x = Me.theInputFileReader.ReadSingle()
					aHitbox.boundingBoxMax.y = Me.theInputFileReader.ReadSingle()
					aHitbox.boundingBoxMax.z = Me.theInputFileReader.ReadSingle()
					aHitbox.nameOffset = Me.theInputFileReader.ReadInt32()
					For x As Integer = 0 To aHitbox.unused.Length - 1
						aHitbox.unused(x) = Me.theInputFileReader.ReadByte()
					Next

					aHitboxSet.theHitboxes.Add(aHitbox)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aHitbox.nameOffset <> 0 Then
						'NOTE: The nameOffset is absolute offset in studiomdl.
						Me.theInputFileReader.BaseStream.Seek(aHitbox.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aHitbox.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitbox.theName = " + aHitbox.theName)
						End If
					Else
						aHitbox.theName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet.theHitboxes " + aHitboxSet.theHitboxes.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadPoseKeys(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		If (aSeqDesc.groupSize(0) > 1 OrElse aSeqDesc.groupSize(1) > 1) AndAlso aSeqDesc.poseKeyOffset <> 0 Then
			Try
				Dim poseKeyCount As Integer
				poseKeyCount = aSeqDesc.groupSize(0) + aSeqDesc.groupSize(1)
				Dim poseKeyInputFileStreamPosition As Long
				'Dim inputFileStreamPosition As Long
				Dim fileOffsetStart As Long
				Dim fileOffsetEnd As Long

				Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.poseKeyOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aSeqDesc.thePoseKeys = New List(Of Double)(poseKeyCount)
				For j As Integer = 0 To poseKeyCount - 1
					poseKeyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aPoseKey As Double
					aPoseKey = Me.theInputFileReader.ReadSingle()
					aSeqDesc.thePoseKeys.Add(aPoseKey)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.thePoseKeys " + aSeqDesc.thePoseKeys.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadEvents(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		If aSeqDesc.eventCount > 0 AndAlso aSeqDesc.eventOffset <> 0 Then
			Try
				'Dim eventInputFileStreamPosition As Long
				'Dim inputFileStreamPosition As Long
				Dim fileOffsetStart As Long
				Dim fileOffsetEnd As Long
				'Dim fileOffsetStart2 As Long
				'Dim fileOffsetEnd2 As Long

				Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aSeqDesc.theEvents = New List(Of SourceMdlEvent37)(aSeqDesc.eventCount)
				For j As Integer = 0 To aSeqDesc.eventCount - 1
					'eventInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anEvent As New SourceMdlEvent37()

					anEvent.cycle = Me.theInputFileReader.ReadSingle()
					anEvent.eventIndex = Me.theInputFileReader.ReadInt32()
					anEvent.eventType = Me.theInputFileReader.ReadInt32()
					For x As Integer = 0 To anEvent.options.Length - 1
						anEvent.options(x) = Me.theInputFileReader.ReadChar()
					Next

					aSeqDesc.theEvents.Add(anEvent)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theEvents " + aSeqDesc.theEvents.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theEvents alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadAutoLayers(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		If aSeqDesc.autoLayerCount > 0 AndAlso aSeqDesc.autoLayerOffset <> 0 Then
			Try
				Dim autoLayerInputFileStreamPosition As Long
				'Dim inputFileStreamPosition As Long
				Dim fileOffsetStart As Long
				Dim fileOffsetEnd As Long

				Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.autoLayerOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aSeqDesc.theAutoLayers = New List(Of SourceMdlAutoLayer37)(aSeqDesc.autoLayerCount)
				For j As Integer = 0 To aSeqDesc.autoLayerCount - 1
					autoLayerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anAutoLayer As New SourceMdlAutoLayer37()

					anAutoLayer.sequenceIndex = Me.theInputFileReader.ReadInt32()
					anAutoLayer.flags = Me.theInputFileReader.ReadInt32()
					anAutoLayer.influenceStart = Me.theInputFileReader.ReadSingle()
					anAutoLayer.influencePeak = Me.theInputFileReader.ReadSingle()
					anAutoLayer.influenceTail = Me.theInputFileReader.ReadSingle()
					anAutoLayer.influenceEnd = Me.theInputFileReader.ReadSingle()

					aSeqDesc.theAutoLayers.Add(anAutoLayer)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAutoLayers " + aSeqDesc.theAutoLayers.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadMdlAnimBoneWeights(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		If Me.theMdlFileData.boneCount > 0 AndAlso aSeqDesc.weightOffset > 0 Then
			Try
				Dim weightListInputFileStreamPosition As Long
				'Dim inputFileStreamPosition As Long
				Dim fileOffsetStart As Long
				Dim fileOffsetEnd As Long
				'Dim fileOffsetStart2 As Long
				'Dim fileOffsetEnd2 As Long

				Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.weightOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aSeqDesc.theBoneWeightsAreDefault = True
				aSeqDesc.theBoneWeights = New List(Of Double)(Me.theMdlFileData.boneCount)
				For j As Integer = 0 To Me.theMdlFileData.boneCount - 1
					weightListInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anAnimBoneWeight As Double
					anAnimBoneWeight = Me.theInputFileReader.ReadSingle()
					aSeqDesc.theBoneWeights.Add(anAnimBoneWeight)

					If anAnimBoneWeight <> 1 Then
						aSeqDesc.theBoneWeightsAreDefault = False
					End If

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theBoneWeights " + aSeqDesc.theBoneWeights.Count.ToString())
				End If
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSequenceIkLocks(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		If aSeqDesc.ikLockCount > 0 AndAlso aSeqDesc.ikLockOffset <> 0 Then
			Try
				Dim lockInputFileStreamPosition As Long
				'Dim inputFileStreamPosition As Long
				Dim fileOffsetStart As Long
				Dim fileOffsetEnd As Long

				Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.ikLockOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aSeqDesc.theIkLocks = New List(Of SourceMdlIkLock37)(aSeqDesc.ikLockCount)
				For j As Integer = 0 To aSeqDesc.ikLockCount - 1
					lockInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anIkLock As New SourceMdlIkLock37()

					anIkLock.chainIndex = Me.theInputFileReader.ReadInt32()
					anIkLock.posWeight = Me.theInputFileReader.ReadSingle()
					anIkLock.localQWeight = Me.theInputFileReader.ReadSingle()

					aSeqDesc.theIkLocks.Add(anIkLock)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theIkLocks " + aSeqDesc.theIkLocks.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theIkLocks alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadMdlAnimIndexes(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		If (aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)) > 0 AndAlso aSeqDesc.blendOffset <> 0 Then
			Try
				Dim animIndexCount As Integer
				animIndexCount = aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)
				Dim animIndexInputFileStreamPosition As Long
				'Dim inputFileStreamPosition As Long
				Dim fileOffsetStart As Long
				Dim fileOffsetEnd As Long

				Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.blendOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aSeqDesc.theAnimDescIndexes = New List(Of Short)(animIndexCount)
				For j As Integer = 0 To animIndexCount - 1
					animIndexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anAnimIndex As Short

					anAnimIndex = Me.theInputFileReader.ReadInt16()

					aSeqDesc.theAnimDescIndexes.Add(anAnimIndex)

					If Me.theMdlFileData.theAnimationDescs IsNot Nothing Then
						'NOTE: Set this boolean for use in writing lines in qc file.
						Me.theMdlFileData.theAnimationDescs(anAnimIndex).theAnimIsLinkedToSequence = True
						Me.theMdlFileData.theAnimationDescs(anAnimIndex).theLinkedSequences.Add(aSeqDesc)
					End If

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'TODO: A sequence can point to same anims as another?
				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAnimDescIndexes " + aSeqDesc.theAnimDescIndexes.Count.ToString())
				End If

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theAnimDescIndexes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadSequenceKeyValues(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		If aSeqDesc.keyValueSize > 0 AndAlso aSeqDesc.keyValueOffset <> 0 Then
			Try
				Dim fileOffsetStart As Long
				Dim fileOffsetEnd As Long

				Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.keyValueOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aSeqDesc.theKeyValues = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theKeyValues")

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theKeyValues alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadAnimations(ByVal animationDescInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc37)
		If anAnimationDesc.animOffset > 0 Then
			Dim animationInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long
			Dim animationValuesEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.animOffset, SeekOrigin.Begin)
				'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				animationValuesEnd = 0

				anAnimationDesc.theAnimations = New List(Of SourceMdlAnimation37)(Me.theMdlFileData.theBones.Count)
				For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
					animationInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim anAnimation As New SourceMdlAnimation37()

					anAnimation.flags = Me.theInputFileReader.ReadInt32()
					If (anAnimation.flags And SourceMdlAnimation37.STUDIO_POS_ANIMATED) > 0 Then
						anAnimation.animationValueOffsets(0) = Me.theInputFileReader.ReadInt32()
						anAnimation.animationValueOffsets(1) = Me.theInputFileReader.ReadInt32()
						anAnimation.animationValueOffsets(2) = Me.theInputFileReader.ReadInt32()
					Else
						anAnimation.position = New SourceVector()
						anAnimation.position.x = Me.theInputFileReader.ReadSingle()
						anAnimation.position.y = Me.theInputFileReader.ReadSingle()
						anAnimation.position.z = Me.theInputFileReader.ReadSingle()
					End If
					If (anAnimation.flags And SourceMdlAnimation37.STUDIO_ROT_ANIMATED) > 0 Then
						anAnimation.animationValueOffsets(3) = Me.theInputFileReader.ReadInt32()
						anAnimation.animationValueOffsets(4) = Me.theInputFileReader.ReadInt32()
						anAnimation.animationValueOffsets(5) = Me.theInputFileReader.ReadInt32()
						anAnimation.unused = Me.theInputFileReader.ReadInt32()
					Else
						anAnimation.rotationQuat = New SourceQuaternion()
						anAnimation.rotationQuat.x = Me.theInputFileReader.ReadSingle()
						anAnimation.rotationQuat.y = Me.theInputFileReader.ReadSingle()
						anAnimation.rotationQuat.z = Me.theInputFileReader.ReadSingle()
						anAnimation.rotationQuat.w = Me.theInputFileReader.ReadSingle()
					End If

					anAnimationDesc.theAnimations.Add(anAnimation)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If (anAnimation.flags And SourceMdlAnimation37.STUDIO_POS_ANIMATED) > 0 Then
						Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 0, anAnimationDesc.frameCount, animationValuesEnd)
						Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 1, anAnimationDesc.frameCount, animationValuesEnd)
						Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 2, anAnimationDesc.frameCount, animationValuesEnd)
					End If
					If (anAnimation.flags And SourceMdlAnimation37.STUDIO_ROT_ANIMATED) > 0 Then
						Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 3, anAnimationDesc.frameCount, animationValuesEnd)
						Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 4, anAnimationDesc.frameCount, animationValuesEnd)
						Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 5, anAnimationDesc.frameCount, animationValuesEnd)
						anAnimation.unused = Me.theInputFileReader.ReadInt32()
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				If animationValuesEnd > 0 Then
					Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, animationValuesEnd, 4, "anAnimation.theAnimationValues alignment")
				End If

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAnimations " + anAnimationDesc.theAnimations.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theAnimations alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadAnimationValues(ByVal animationInputFileStreamPosition As Long, ByVal anAnimation As SourceMdlAnimation37, ByVal offsetIndex As Integer, ByVal frameCount As Integer, ByRef fileOffsetEnd As Long)
		If anAnimation.animationValueOffsets(offsetIndex) > 0 Then
			Dim fileOffsetStart As Long
			'Dim fileOffsetEnd As Long
			Dim frameCountRemainingToBeChecked As Integer
			Dim currentTotal As Byte
			Dim validCount As Byte
			Dim animValues As List(Of SourceMdlAnimationValue10)

			anAnimation.theAnimationValues(offsetIndex) = New List(Of SourceMdlAnimationValue10)()
			animValues = anAnimation.theAnimationValues(offsetIndex)

			Try
				Me.theInputFileReader.BaseStream.Seek(animationInputFileStreamPosition + anAnimation.animationValueOffsets(offsetIndex), SeekOrigin.Begin)
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
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadMdlMovements(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc37)
		If anAnimationDesc.movementCount > 0 Then
			Dim movementInputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.movementOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				anAnimationDesc.theMovements = New List(Of SourceMdlMovement)(anAnimationDesc.movementCount)
				For j As Integer = 0 To anAnimationDesc.movementCount - 1
					movementInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aMovement As New SourceMdlMovement()

					aMovement.endframeIndex = Me.theInputFileReader.ReadInt32()
					aMovement.motionFlags = Me.theInputFileReader.ReadInt32()
					aMovement.v0 = Me.theInputFileReader.ReadSingle()
					aMovement.v1 = Me.theInputFileReader.ReadSingle()
					aMovement.angle = Me.theInputFileReader.ReadSingle()

					aMovement.vector = New SourceVector()
					aMovement.vector.x = Me.theInputFileReader.ReadSingle()
					aMovement.vector.y = Me.theInputFileReader.ReadSingle()
					aMovement.vector.z = Me.theInputFileReader.ReadSingle()
					aMovement.position = New SourceVector()
					aMovement.position.x = Me.theInputFileReader.ReadSingle()
					aMovement.position.y = Me.theInputFileReader.ReadSingle()
					aMovement.position.z = Me.theInputFileReader.ReadSingle()

					anAnimationDesc.theMovements.Add(aMovement)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theMovements " + anAnimationDesc.theMovements.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theMovements alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadMdlIkRules(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc37)
		If anAnimationDesc.ikRuleCount > 0 Then
			Dim ikRuleInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.ikRuleOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				anAnimationDesc.theIkRules = New List(Of SourceMdlIkRule37)(anAnimationDesc.ikRuleCount)
				For j As Integer = 0 To anAnimationDesc.ikRuleCount - 1
					ikRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anIkRule As New SourceMdlIkRule37()

					anIkRule.index = Me.theInputFileReader.ReadInt32()
					anIkRule.type = Me.theInputFileReader.ReadInt32()
					anIkRule.chain = Me.theInputFileReader.ReadInt32()
					anIkRule.bone = Me.theInputFileReader.ReadInt32()

					anIkRule.slot = Me.theInputFileReader.ReadInt32()
					anIkRule.height = Me.theInputFileReader.ReadSingle()
					anIkRule.radius = Me.theInputFileReader.ReadSingle()
					anIkRule.floor = Me.theInputFileReader.ReadSingle()

					anIkRule.pos = New SourceVector()
					anIkRule.pos.x = Me.theInputFileReader.ReadSingle()
					anIkRule.pos.y = Me.theInputFileReader.ReadSingle()
					anIkRule.pos.z = Me.theInputFileReader.ReadSingle()
					anIkRule.q = New SourceQuaternion()
					anIkRule.q.x = Me.theInputFileReader.ReadSingle()
					anIkRule.q.y = Me.theInputFileReader.ReadSingle()
					anIkRule.q.z = Me.theInputFileReader.ReadSingle()
					anIkRule.q.w = Me.theInputFileReader.ReadSingle()

					anIkRule.weight = Me.theInputFileReader.ReadSingle()
					anIkRule.group = Me.theInputFileReader.ReadInt32()
					anIkRule.ikErrorIndexStart = Me.theInputFileReader.ReadInt32()
					anIkRule.ikErrorOffset = Me.theInputFileReader.ReadInt32()

					anIkRule.influenceStart = Me.theInputFileReader.ReadSingle()
					anIkRule.influencePeak = Me.theInputFileReader.ReadSingle()
					anIkRule.influenceTail = Me.theInputFileReader.ReadSingle()
					anIkRule.influenceEnd = Me.theInputFileReader.ReadSingle()

					anIkRule.commit = Me.theInputFileReader.ReadSingle()
					anIkRule.contact = Me.theInputFileReader.ReadSingle()
					anIkRule.pivot = Me.theInputFileReader.ReadSingle()
					anIkRule.release = Me.theInputFileReader.ReadSingle()

					'NOTE: Change NaN to 0. This is needed for HL2 beta leak "hl2\models\Police.mdl" for various $animations.
					If Double.IsNaN(anIkRule.influenceStart) Then
						anIkRule.influenceStart = 0
					End If
					If Double.IsNaN(anIkRule.influencePeak) Then
						anIkRule.influencePeak = 0
					End If
					If Double.IsNaN(anIkRule.influenceTail) Then
						anIkRule.influenceTail = 0
					End If
					If Double.IsNaN(anIkRule.influenceEnd) Then
						anIkRule.influenceEnd = 0
					End If
					If Double.IsNaN(anIkRule.contact) Then
						anIkRule.contact = 0
					End If

					anAnimationDesc.theIkRules.Add(anIkRule)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadMdlIkErrors(ikRuleInputFileStreamPosition, anIkRule, anAnimationDesc.frameCount)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theIkRules " + anAnimationDesc.theIkRules.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theIkRules alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadMdlIkErrors(ByVal ikRuleInputFileStreamPosition As Long, ByVal anIkRule As SourceMdlIkRule37, ByVal frameCount As Integer)
		'pikrule->start	= g_panimation[i]->ikrule[j].start / (g_panimation[i]->numframes - 1.0f);
		'pikrule->end	= g_panimation[i]->ikrule[j].end / (g_panimation[i]->numframes - 1.0f);
		'pRule->numerror = pRule->end - pRule->start + 1;
		'if (pRule->end >= panim->numframes)
		'	pRule->numerror = pRule->numerror + 2;
		Dim ikErrorStart As Integer
		Dim ikErrorEnd As Integer
		Dim ikErrorCount As Integer
		Try
			ikErrorStart = CInt(anIkRule.influenceStart * (frameCount - 1))
			ikErrorEnd = CInt(anIkRule.influenceEnd * (frameCount - 1))
			ikErrorCount = ikErrorEnd - ikErrorStart + 1
			If ikErrorEnd >= frameCount Then
				ikErrorCount += 2
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		If ikErrorCount > 0 Then
			'Dim ikErrorInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.ikErrorOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				anIkRule.theIkErrors = New List(Of SourceMdlIkError37)(ikErrorCount)
				For j As Integer = 0 To ikErrorCount - 1
					'ikErrorInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anIkError As New SourceMdlIkError37()

					anIkError.pos = New SourceVector()
					anIkError.pos.x = Me.theInputFileReader.ReadSingle()
					anIkError.pos.y = Me.theInputFileReader.ReadSingle()
					anIkError.pos.z = Me.theInputFileReader.ReadSingle()
					anIkError.q = New SourceQuaternion()
					anIkError.q.x = Me.theInputFileReader.ReadSingle()
					anIkError.q.y = Me.theInputFileReader.ReadSingle()
					anIkError.q.z = Me.theInputFileReader.ReadSingle()
					anIkError.q.w = Me.theInputFileReader.ReadSingle()

					anIkRule.theIkErrors.Add(anIkError)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theIkErrors " + anIkRule.theIkErrors.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadFlexOps(ByVal flexRuleInputFileStreamPosition As Long, ByVal aFlexRule As SourceMdlFlexRule)
		If aFlexRule.opCount > 0 AndAlso aFlexRule.opOffset <> 0 Then
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

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexOps alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadIkLinks(ByVal ikChainInputFileStreamPosition As Long, ByVal anIkChain As SourceMdlIkChain37)
		If anIkChain.linkCount > 0 Then
			'Dim ikLinkInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.linkOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				anIkChain.theLinks = New List(Of SourceMdlIkLink37)(anIkChain.linkCount)
				For j As Integer = 0 To anIkChain.linkCount - 1
					'ikLinkInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anIkLink As New SourceMdlIkLink37()

					anIkLink.boneIndex = Me.theInputFileReader.ReadInt32()
					anIkLink.contact.x = Me.theInputFileReader.ReadSingle()
					anIkLink.contact.y = Me.theInputFileReader.ReadSingle()
					anIkLink.contact.z = Me.theInputFileReader.ReadSingle()
					anIkLink.limits.x = Me.theInputFileReader.ReadSingle()
					anIkLink.limits.y = Me.theInputFileReader.ReadSingle()
					anIkLink.limits.z = Me.theInputFileReader.ReadSingle()

					anIkChain.theLinks.Add(anIkLink)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkChain.theLinks " + anIkChain.theLinks.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadModels(ByVal bodyPartInputFileStreamPosition As Long, ByVal aBodyPart As SourceMdlBodyPart37)
		If aBodyPart.modelCount > 0 Then
			Dim modelInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aBodyPart.theModels = New List(Of SourceMdlModel37)(aBodyPart.modelCount)
				For j As Integer = 0 To aBodyPart.modelCount - 1
					modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aModel As New SourceMdlModel37()

					aModel.name = Me.theInputFileReader.ReadChars(aModel.name.Length)
					aModel.theName = CStr(aModel.name).Trim(Chr(0))
					aModel.type = Me.theInputFileReader.ReadInt32()
					aModel.boundingRadius = Me.theInputFileReader.ReadSingle()
					aModel.meshCount = Me.theInputFileReader.ReadInt32()
					aModel.meshOffset = Me.theInputFileReader.ReadInt32()

					aModel.vertexCount = Me.theInputFileReader.ReadInt32()
					aModel.vertexOffset = Me.theInputFileReader.ReadInt32()
					aModel.tangentOffset = Me.theInputFileReader.ReadInt32()

					aModel.attachmentCount = Me.theInputFileReader.ReadInt32()
					aModel.attachmentOffset = Me.theInputFileReader.ReadInt32()
					aModel.eyeballCount = Me.theInputFileReader.ReadInt32()
					aModel.eyeballOffset = Me.theInputFileReader.ReadInt32()

					For x As Integer = 0 To aModel.unused.Length - 1
						aModel.unused(x) = Me.theInputFileReader.ReadInt32()
					Next

					aBodyPart.theModels.Add(aModel)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
					Me.ReadEyeballs(modelInputFileStreamPosition, aModel)
					Me.ReadMeshes(modelInputFileStreamPosition, aModel)

					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					''NOTE: Although studiomdl source code indicates ALIGN64, it seems to align on 32.
					'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 32, "aModel.theVertexes pre-alignment (NOTE: Should end at: " + CStr(modelInputFileStreamPosition + aModel.vertexOffset - 1) + ")")
					Me.ReadVertexes(modelInputFileStreamPosition, aModel)

					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theTangents pre-alignment (NOTE: Should end at: " + CStr(modelInputFileStreamPosition + aModel.tangentOffset - 1) + ")")
					Me.ReadTangents(modelInputFileStreamPosition, aModel)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels " + aBodyPart.theModels.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadEyeballs(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel37)
		If aModel.eyeballCount > 0 AndAlso aModel.eyeballOffset <> 0 Then
			Dim eyeballInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.eyeballOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theEyeballs = New List(Of SourceMdlEyeball37)(aModel.eyeballCount)
				For eyeballIndex As Integer = 0 To aModel.eyeballCount - 1
					eyeballInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anEyeball As New SourceMdlEyeball37()

					anEyeball.nameOffset = Me.theInputFileReader.ReadInt32()
					anEyeball.boneIndex = Me.theInputFileReader.ReadInt32()
					anEyeball.org = New SourceVector()
					anEyeball.org.x = Me.theInputFileReader.ReadSingle()
					anEyeball.org.y = Me.theInputFileReader.ReadSingle()
					anEyeball.org.z = Me.theInputFileReader.ReadSingle()
					anEyeball.zOffset = Me.theInputFileReader.ReadSingle()
					anEyeball.radius = Me.theInputFileReader.ReadSingle()
					anEyeball.up = New SourceVector()
					anEyeball.up.x = Me.theInputFileReader.ReadSingle()
					anEyeball.up.y = Me.theInputFileReader.ReadSingle()
					anEyeball.up.z = Me.theInputFileReader.ReadSingle()
					anEyeball.forward = New SourceVector()
					anEyeball.forward.x = Me.theInputFileReader.ReadSingle()
					anEyeball.forward.y = Me.theInputFileReader.ReadSingle()
					anEyeball.forward.z = Me.theInputFileReader.ReadSingle()
					anEyeball.texture = Me.theInputFileReader.ReadInt32()

					anEyeball.irisMaterial = Me.theInputFileReader.ReadInt32()
					anEyeball.irisScale = Me.theInputFileReader.ReadSingle()
					anEyeball.glintMaterial = Me.theInputFileReader.ReadInt32()

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

					anEyeball.pitch(0) = Me.theInputFileReader.ReadSingle()
					anEyeball.pitch(1) = Me.theInputFileReader.ReadSingle()
					anEyeball.yaw(0) = Me.theInputFileReader.ReadSingle()
					anEyeball.yaw(1) = Me.theInputFileReader.ReadSingle()

					aModel.theEyeballs.Add(anEyeball)

					'NOTE: Set the default value to -1 to distinguish it from value assigned to it by ReadMeshes().
					anEyeball.theTextureIndex = -1

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

				If aModel.theEyeballs.Count > 0 Then
					Me.theMdlFileData.theModelCommandIsUsed = True
				End If

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theEyeballs " + aModel.theEyeballs.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theEyeballs alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadMeshes(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel37)
		If aModel.meshCount > 0 AndAlso aModel.meshOffset <> 0 Then
			Dim meshInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.meshOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theMeshes = New List(Of SourceMdlMesh37)(aModel.meshCount)
				For meshIndex As Integer = 0 To aModel.meshCount - 1
					meshInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aMesh As New SourceMdlMesh37()

					aMesh.materialIndex = Me.theInputFileReader.ReadInt32()
					aMesh.modelOffset = Me.theInputFileReader.ReadInt32()

					aMesh.vertexCount = Me.theInputFileReader.ReadInt32()
					aMesh.vertexIndexStart = Me.theInputFileReader.ReadInt32()
					aMesh.flexCount = Me.theInputFileReader.ReadInt32()
					aMesh.flexOffset = Me.theInputFileReader.ReadInt32()
					aMesh.materialType = Me.theInputFileReader.ReadInt32()
					aMesh.materialParam = Me.theInputFileReader.ReadInt32()

					aMesh.id = Me.theInputFileReader.ReadInt32()
					aMesh.center.x = Me.theInputFileReader.ReadSingle()
					aMesh.center.y = Me.theInputFileReader.ReadSingle()
					aMesh.center.z = Me.theInputFileReader.ReadSingle()
					For x As Integer = 0 To aMesh.unused.Length - 1
						aMesh.unused(x) = Me.theInputFileReader.ReadInt32()
					Next

					aModel.theMeshes.Add(aMesh)

					' Fill-in eyeball texture index info.
					If aMesh.materialType = 1 Then
						aModel.theEyeballs(aMesh.materialParam).theTextureIndex = aMesh.materialIndex
					End If

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aMesh.flexCount > 0 AndAlso aMesh.flexOffset <> 0 Then
						Me.ReadFlexes(meshInputFileStreamPosition, aMesh)
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theMeshes " + aModel.theMeshes.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theMeshes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadVertexes(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel37)
		If aModel.vertexCount > 0 AndAlso aModel.vertexOffset <> 0 Then
			Dim vertexInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				If Me.theMdlFileData.theVertexes Is Nothing Then
					Me.theMdlFileData.theVertexes = New List(Of SourceMdlVertex37)()
				End If

				aModel.theVertexes = New List(Of SourceMdlVertex37)(aModel.vertexCount)
				For vertexIndex As Integer = 0 To aModel.vertexCount - 1
					vertexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aVertex As New SourceMdlVertex37()

					aVertex.boneWeight.weight(0) = Me.theInputFileReader.ReadSingle()
					aVertex.boneWeight.weight(1) = Me.theInputFileReader.ReadSingle()
					aVertex.boneWeight.weight(2) = Me.theInputFileReader.ReadSingle()
					aVertex.boneWeight.weight(3) = Me.theInputFileReader.ReadSingle()
					aVertex.boneWeight.bone(0) = Me.theInputFileReader.ReadInt16()
					aVertex.boneWeight.bone(1) = Me.theInputFileReader.ReadInt16()
					aVertex.boneWeight.bone(2) = Me.theInputFileReader.ReadInt16()
					aVertex.boneWeight.bone(3) = Me.theInputFileReader.ReadInt16()
					aVertex.boneWeight.boneCount = Me.theInputFileReader.ReadInt16()
					aVertex.boneWeight.material = Me.theInputFileReader.ReadInt16()
					aVertex.boneWeight.firstRef = Me.theInputFileReader.ReadInt16()
					aVertex.boneWeight.lastRef = Me.theInputFileReader.ReadInt16()
					aVertex.position.x = Me.theInputFileReader.ReadSingle()
					aVertex.position.y = Me.theInputFileReader.ReadSingle()
					aVertex.position.z = Me.theInputFileReader.ReadSingle()
					aVertex.normal.x = Me.theInputFileReader.ReadSingle()
					aVertex.normal.y = Me.theInputFileReader.ReadSingle()
					aVertex.normal.z = Me.theInputFileReader.ReadSingle()
					aVertex.texCoordX = Me.theInputFileReader.ReadSingle()
					aVertex.texCoordY = Me.theInputFileReader.ReadSingle()

					aModel.theVertexes.Add(aVertex)
					Me.theMdlFileData.theVertexes.Add(aVertex)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexes " + aModel.theVertexes.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadTangents(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel37)
		If aModel.vertexCount > 0 AndAlso aModel.tangentOffset <> 0 Then
			Dim vertexInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.tangentOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theTangents = New List(Of SourceVector4D)(aModel.vertexCount)
				For vertexIndex As Integer = 0 To aModel.vertexCount - 1
					vertexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aTangent As New SourceVector4D()

					aTangent.x = Me.theInputFileReader.ReadSingle()
					aTangent.y = Me.theInputFileReader.ReadSingle()
					aTangent.z = Me.theInputFileReader.ReadSingle()
					aTangent.w = Me.theInputFileReader.ReadSingle()

					aModel.theTangents.Add(aTangent)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theTangents " + aModel.theTangents.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theTangents alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadFlexes(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceMdlMesh37)
		If aMesh.flexCount > 0 AndAlso aMesh.flexOffset <> 0 Then
			Dim flexInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.flexOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aMesh.theFlexes = New List(Of SourceMdlFlex37)(aMesh.flexCount)
				For k As Integer = 0 To aMesh.flexCount - 1
					flexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aFlex As New SourceMdlFlex37()

					aFlex.flexDescIndex = Me.theInputFileReader.ReadInt32()

					aFlex.target0 = Me.theInputFileReader.ReadSingle()
					aFlex.target1 = Me.theInputFileReader.ReadSingle()
					aFlex.target2 = Me.theInputFileReader.ReadSingle()
					aFlex.target3 = Me.theInputFileReader.ReadSingle()

					aFlex.vertCount = Me.theInputFileReader.ReadInt32()
					aFlex.vertOffset = Me.theInputFileReader.ReadInt32()

					aMesh.theFlexes.Add(aFlex)

					''NOTE: Set the frame index here because it is determined by order of flexes in mdl file.
					''      Start the indexing at 1 because first frame (frame 0) is "basis" frame.
					'Me.theCurrentFrameIndex += 1
					'Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theVtaFrameIndex = Me.theCurrentFrameIndex

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aFlex.vertCount > 0 AndAlso aFlex.vertOffset <> 0 Then
						Me.ReadVertAnims(flexInputFileStreamPosition, aFlex)
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFlexes " + aMesh.theFlexes.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aMesh.theFlexes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadVertAnims(ByVal flexInputFileStreamPosition As Long, ByVal aFlex As SourceMdlFlex37)
		If aFlex.vertCount > 0 AndAlso aFlex.vertOffset <> 0 Then
			'Dim vertAnimInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aFlex.theVertAnims = New List(Of SourceMdlVertAnim37)(aFlex.vertCount)
				For k As Integer = 0 To aFlex.vertCount - 1
					'vertAnimInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aVertAnim As New SourceMdlVertAnim37()

					aVertAnim.index = Me.theInputFileReader.ReadInt32()
					aVertAnim.delta.x = Me.theInputFileReader.ReadSingle()
					aVertAnim.delta.y = Me.theInputFileReader.ReadSingle()
					aVertAnim.delta.z = Me.theInputFileReader.ReadSingle()
					aVertAnim.nDelta.x = Me.theInputFileReader.ReadSingle()
					aVertAnim.nDelta.y = Me.theInputFileReader.ReadSingle()
					aVertAnim.nDelta.z = Me.theInputFileReader.ReadSingle()

					aFlex.theVertAnims.Add(aVertAnim)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims " + aFlex.theVertAnims.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aFlex.theVertAnims alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub CreateFlexFrameList()
		Dim aFlexFrame As FlexFrame37
		Dim aBodyPart As SourceMdlBodyPart37
		Dim aModel As SourceMdlModel37
		Dim aMesh As SourceMdlMesh37
		Dim aFlex As SourceMdlFlex37
		Dim searchedFlexFrame As FlexFrame37

		Me.theMdlFileData.theFlexFrames = New List(Of FlexFrame37)()

		'NOTE: Create the defaultflex.
		aFlexFrame = New FlexFrame37()
		Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)

		If Me.theMdlFileData.theFlexDescs IsNot Nothing AndAlso Me.theMdlFileData.theFlexDescs.Count > 0 Then
			'Dim flexDescToMeshIndexes As List(Of List(Of Integer))
			Dim flexDescToFlexFrames As List(Of List(Of FlexFrame37))
			Dim meshVertexIndexStart As Integer

			'flexDescToMeshIndexes = New List(Of List(Of Integer))(Me.theMdlFileData.theFlexDescs.Count)
			'For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
			'	Dim meshIndexList As New List(Of Integer)()
			'	flexDescToMeshIndexes.Add(meshIndexList)
			'Next

			flexDescToFlexFrames = New List(Of List(Of FlexFrame37))(Me.theMdlFileData.theFlexDescs.Count)
			For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
				Dim flexFrameList As New List(Of FlexFrame37)()
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
													' Add to an existing flexFrame.
													aFlexFrame = searchedFlexFrame
													Exit For
												End If
											Next
										End If
										If aFlexFrame Is Nothing Then
											aFlexFrame = New FlexFrame37()
											Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)
											aFlexFrame.bodyAndMeshVertexIndexStarts = New List(Of Integer)()
											aFlexFrame.flexes = New List(Of SourceMdlFlex37)()

											'Dim aFlexDescPartnerIndex As Integer
											'aFlexDescPartnerIndex = aMesh.theFlexes(flexIndex).flexDescPartnerIndex

											aFlexFrame.flexName = Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theName
											'If aFlexDescPartnerIndex > 0 Then
											'	aFlexFrame.flexDescription = aFlexFrame.flexName
											'	aFlexFrame.flexDescription += "+"
											'	aFlexFrame.flexDescription += Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theName
											'	aFlexFrame.flexHasPartner = True
											'	aFlexFrame.flexSplit = Me.GetSplit(aFlex, meshVertexIndexStart)
											'	Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theDescIsUsedByFlex = True
											'Else
											aFlexFrame.flexDescription = aFlexFrame.flexName
											'aFlexFrame.flexHasPartner = False
											'End If
											Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theDescIsUsedByFlex = True

											flexDescToFlexFrames(aFlex.flexDescIndex).Add(aFlexFrame)
										End If

										aFlexFrame.bodyAndMeshVertexIndexStarts.Add(meshVertexIndexStart)
										aFlexFrame.flexes.Add(aFlex)
									Next
								End If
							Next
						End If
					Next
				End If
			Next
		End If
	End Sub

#End Region

#Region "Data"

	Protected theInputFileReader As BinaryReader
	Protected theOutputFileWriter As BinaryWriter

	Protected theMdlFileData As SourceMdlFileData37

#End Region

End Class
