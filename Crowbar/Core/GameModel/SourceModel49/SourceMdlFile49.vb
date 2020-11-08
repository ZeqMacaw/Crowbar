Imports System.IO

Public Class SourceMdlFile49

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlFileReader As BinaryReader, ByVal mdlFileData As SourceMdlFileData49)
		Me.theInputFileReader = mdlFileReader
		Me.theMdlFileData = mdlFileData

		Me.theMdlFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length
	End Sub

	Public Sub New(ByVal mdlFileWriter As BinaryWriter, ByVal mdlFileData As SourceMdlFileData49)
		Me.theOutputFileWriter = mdlFileWriter
		Me.theMdlFileData = mdlFileData
	End Sub

	' Only here to make compiler happy with SourceAniFileXX that inherits from this class.
	Protected Sub New()
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

		Me.theMdlFileData.name = Me.theInputFileReader.ReadChars(64)
		Me.theMdlFileData.theModelName = CStr(Me.theMdlFileData.name).Trim(Chr(0))

		Me.theMdlFileData.fileSize = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.theActualFileSize = Me.theInputFileReader.BaseStream.Length

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		If logDescription <> "" Then
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription + " (MDL version: " + Me.theMdlFileData.version.ToString() + ")")
		End If
	End Sub

	Public Sub ReadMdlHeader01(ByVal logDescription As String)
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		' Offsets: 0x50, 0x54, 0x58
		Me.theMdlFileData.eyePosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.eyePosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.eyePosition.z = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x5C, 0x60, 0x64
		Me.theMdlFileData.illuminationPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.illuminationPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.illuminationPosition.z = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x68, 0x6C, 0x70
		Me.theMdlFileData.hullMinPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMinPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMinPosition.z = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x74, 0x78, 0x7C
		Me.theMdlFileData.hullMaxPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMaxPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMaxPosition.z = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x80, 0x84, 0x88
		Me.theMdlFileData.viewBoundingBoxMinPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMinPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMinPosition.z = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x8C, 0x90, 0x94
		Me.theMdlFileData.viewBoundingBoxMaxPosition.x = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMaxPosition.y = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMaxPosition.z = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x98
		Me.theMdlFileData.flags = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x9C (156), 0xA0
		Me.theMdlFileData.boneCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xA4, 0xA8
		Me.theMdlFileData.boneControllerCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneControllerOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xAC (172), 0xB0
		Me.theMdlFileData.hitboxSetCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.hitboxSetOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xB4 (180), 0xB8
		Me.theMdlFileData.localAnimationCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localAnimationOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xBC (188), 0xC0 (192)
		Me.theMdlFileData.localSequenceCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localSequenceOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xC4, 0xC8
		Me.theMdlFileData.activityListVersion = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.eventsIndexed = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xCC (204), 0xD0 (208)
		Me.theMdlFileData.textureCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.textureOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xD4 (212), 0xD8
		Me.theMdlFileData.texturePathCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.texturePathOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xDC, 0xE0 (224), 0xE4 (228)
		Me.theMdlFileData.skinReferenceCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinFamilyCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinFamilyOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xE8 (232), 0xEC (236)
		Me.theMdlFileData.bodyPartCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.bodyPartOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xF0 (240), 0xF4 (244)
		Me.theMdlFileData.localAttachmentCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localAttachmentOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xF8, 0xFC, 0x0100
		Me.theMdlFileData.localNodeCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localNodeOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.localNodeNameOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x0104 (), 0x0108 ()
		Me.theMdlFileData.flexDescCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexDescOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x010C (), 0x0110 ()
		Me.theMdlFileData.flexControllerCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexControllerOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x0114 (), 0x0118 ()
		Me.theMdlFileData.flexRuleCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexRuleOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x011C (), 0x0120 ()
		Me.theMdlFileData.ikChainCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.ikChainOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x0124 (), 0x0128 ()
		Me.theMdlFileData.mouthCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.mouthOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x012C (), 0x0130 ()
		Me.theMdlFileData.localPoseParamaterCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localPoseParameterOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x0134 ()
		Me.theMdlFileData.surfacePropOffset = Me.theInputFileReader.ReadInt32()

		'TODO: Same as some lines below. Move to a separate function.
		If Me.theMdlFileData.surfacePropOffset > 0 Then
			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.surfacePropOffset, SeekOrigin.Begin)
			fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

			fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
			'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theSurfacePropName = " + Me.theMdlFileData.theSurfacePropName)
			'End If
			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Else
			Me.theMdlFileData.theSurfacePropName = ""
		End If

		' Offsets: 0x0138 (312), 0x013C (316)
		Me.theMdlFileData.keyValueOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.keyValueSize = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.localIkAutoPlayLockCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localIkAutoPlayLockOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.mass = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.contents = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.includeModelCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.includeModelOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.virtualModelP = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.animBlockNameOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.animBlockCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.animBlockOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.animBlockModelP = Me.theInputFileReader.ReadInt32()
		If Me.theMdlFileData.animBlockCount > 0 Then
			If Me.theMdlFileData.animBlockNameOffset > 0 Then
				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.animBlockNameOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theAnimBlockRelativePathFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theAnimBlockRelativePathFileName = " + Me.theMdlFileData.theAnimBlockRelativePathFileName)
				'End If
				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			End If
			If Me.theMdlFileData.animBlockOffset > 0 Then
				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.animBlockOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theAnimBlocks = New List(Of SourceMdlAnimBlock)(Me.theMdlFileData.animBlockCount)
				For offset As Integer = 0 To Me.theMdlFileData.animBlockCount - 1
					Dim anAnimBlock As New SourceMdlAnimBlock()
					anAnimBlock.dataStart = Me.theInputFileReader.ReadInt32()
					anAnimBlock.dataEnd = Me.theInputFileReader.ReadInt32()
					Me.theMdlFileData.theAnimBlocks.Add(anAnimBlock)
				Next

				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theAnimBlocks " + Me.theMdlFileData.theAnimBlocks.Count.ToString())
				'End If
				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			End If
		End If

		Me.theMdlFileData.boneTableByNameOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.vertexBaseP = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.indexBaseP = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.directionalLightDot = Me.theInputFileReader.ReadByte()

		Me.theMdlFileData.rootLod = Me.theInputFileReader.ReadByte()

		Me.theMdlFileData.allowedRootLodCount_VERSION48 = Me.theInputFileReader.ReadByte()

		Me.theMdlFileData.unused = Me.theInputFileReader.ReadByte()

		Me.theMdlFileData.zeroframecacheindex_VERSION44to47 = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.flexControllerUiCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexControllerUiOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.vertAnimFixedPointScale = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.surfacePropLookup = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.studioHeader2Offset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.unused2 = Me.theInputFileReader.ReadInt32()

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription)

		If Me.theMdlFileData.bodyPartCount = 0 AndAlso Me.theMdlFileData.localSequenceCount > 0 Then
			Me.theMdlFileData.theMdlFileOnlyHasAnimations = True
		End If
	End Sub

	Public Sub ReadMdlHeader02(ByVal logDescription As String)
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theMdlFileData.sourceBoneTransformCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.sourceBoneTransformOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.illumPositionAttachmentNumber = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.maxEyeDeflection = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.linearBoneOffset = Me.theInputFileReader.ReadInt32()

		'TODO: According to MDL v48 source code, the following fields are not used.
		'      Test various MDL v48 models to see if any use these. 

		Me.theMdlFileData.nameCopyOffset = Me.theInputFileReader.ReadInt32()
		If Me.theMdlFileData.nameCopyOffset > 0 Then
			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Me.theInputFileReader.BaseStream.Seek(fileOffsetStart + Me.theMdlFileData.nameCopyOffset, SeekOrigin.Begin)
			fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theNameCopy = FileManager.ReadNullTerminatedString(Me.theInputFileReader)
			Me.theMdlFileData.theModelName = Me.theMdlFileData.theNameCopy

			fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theNameCopy = " + Me.theMdlFileData.theNameCopy)
			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Else
			Me.theMdlFileData.theNameCopy = ""
		End If

		Me.theMdlFileData.boneFlexDriverCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneFlexDriverOffset = Me.theInputFileReader.ReadInt32()

		For x As Integer = 0 To Me.theMdlFileData.reserved.Length - 1
			Me.theMdlFileData.reserved(x) = Me.theInputFileReader.ReadInt32()
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription)
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

				Me.theMdlFileData.theBones = New List(Of SourceMdlBone)(Me.theMdlFileData.boneCount)
				For boneIndex As Integer = 0 To Me.theMdlFileData.boneCount - 1
					boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBone As New SourceMdlBone()

					aBone.nameOffset = Me.theInputFileReader.ReadInt32()
					aBone.parentBoneIndex = Me.theInputFileReader.ReadInt32()

					For j As Integer = 0 To aBone.boneControllerIndex.Length - 1
						aBone.boneControllerIndex(j) = Me.theInputFileReader.ReadInt32()
					Next
					aBone.position = New SourceVector()
					aBone.position.x = Me.theInputFileReader.ReadSingle()
					aBone.position.y = Me.theInputFileReader.ReadSingle()
					aBone.position.z = Me.theInputFileReader.ReadSingle()

					aBone.quat = New SourceQuaternion()
					aBone.quat.x = Me.theInputFileReader.ReadSingle()
					aBone.quat.y = Me.theInputFileReader.ReadSingle()
					aBone.quat.z = Me.theInputFileReader.ReadSingle()
					aBone.quat.w = Me.theInputFileReader.ReadSingle()

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
					aBone.contents = Me.theInputFileReader.ReadInt32()

					For k As Integer = 0 To 7
						aBone.unused(k) = Me.theInputFileReader.ReadInt32()
					Next

					Me.theMdlFileData.theBones.Add(aBone)

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					If aBone.nameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.nameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aBone.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theName = " + aBone.theName)
						'End If
					ElseIf aBone.theName Is Nothing Then
						aBone.theName = ""
					End If
					Me.theMdlFileData.theBoneNameToBoneIndexMap.Add(aBone.theName, boneIndex)

					If aBone.proceduralRuleOffset <> 0 Then
						If aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_AXISINTERP Then
							'TODO: The text file for this info seems to be in a different file than the VRD file.
							Me.ReadAxisInterpBone(boneInputFileStreamPosition, aBone)
						ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_QUATINTERP Then
							Me.theMdlFileData.theProceduralBonesCommandIsUsed = True
							Me.ReadQuatInterpBone(boneInputFileStreamPosition, aBone)
						ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_AIMATBONE OrElse aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_AIMATATTACH Then
							Me.theMdlFileData.theProceduralBonesCommandIsUsed = True
							' Used by pistons on Portal 2 "player\ballbot.mdl" model.
							Me.ReadAimAtBone(boneInputFileStreamPosition, aBone)
						ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
							Me.ReadJiggleBone(boneInputFileStreamPosition, aBone)
						End If
					End If

					If aBone.surfacePropNameOffset <> 0 Then
						Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.surfacePropNameOffset, SeekOrigin.Begin)
						fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						aBone.theSurfacePropName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theSurfacePropName = " + aBone.theSurfacePropName)
						'End If
					Else
						aBone.theSurfacePropName = ""
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones " + Me.theMdlFileData.theBones.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	'TODO: VERIFY ReadAxisInterpBone()
	Private Sub ReadAxisInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone)
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
		Catch
		End Try
	End Sub

	Private Sub ReadQuatInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone)
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
		Catch
		End Try
	End Sub

	Private Sub ReadAimAtBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone)
		Dim aimAtBoneInputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aimAtBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			aBone.theAimAtBone = New SourceMdlAimAtBone()
			aBone.theAimAtBone.parentBoneIndex = Me.theInputFileReader.ReadInt32()
			aBone.theAimAtBone.aimBoneOrAttachmentIndex = Me.theInputFileReader.ReadInt32()

			aBone.theAimAtBone.aim = New SourceVector()
			aBone.theAimAtBone.aim.x = Me.theInputFileReader.ReadSingle()
			aBone.theAimAtBone.aim.y = Me.theInputFileReader.ReadSingle()
			aBone.theAimAtBone.aim.z = Me.theInputFileReader.ReadSingle()

			aBone.theAimAtBone.up = New SourceVector()
			aBone.theAimAtBone.up.x = Me.theInputFileReader.ReadSingle()
			aBone.theAimAtBone.up.y = Me.theInputFileReader.ReadSingle()
			aBone.theAimAtBone.up.z = Me.theInputFileReader.ReadSingle()

			aBone.theAimAtBone.basePos = New SourceVector()
			aBone.theAimAtBone.basePos.x = Me.theInputFileReader.ReadSingle()
			aBone.theAimAtBone.basePos.y = Me.theInputFileReader.ReadSingle()
			aBone.theAimAtBone.basePos.z = Me.theInputFileReader.ReadSingle()

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAimAtBone")
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
		Catch
		End Try
	End Sub

	Private Sub ReadJiggleBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aBone.theJiggleBone = New SourceMdlJiggleBone()
		aBone.theJiggleBone.flags = Me.theInputFileReader.ReadInt32()
		aBone.theJiggleBone.length = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.tipMass = Me.theInputFileReader.ReadSingle()

		aBone.theJiggleBone.yawStiffness = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.yawDamping = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.pitchStiffness = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.pitchDamping = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.alongStiffness = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.alongDamping = Me.theInputFileReader.ReadSingle()

		aBone.theJiggleBone.angleLimit = Me.theInputFileReader.ReadSingle()

		aBone.theJiggleBone.minYaw = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.maxYaw = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.yawFriction = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.yawBounce = Me.theInputFileReader.ReadSingle()

		aBone.theJiggleBone.minPitch = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.maxPitch = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.pitchFriction = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.pitchBounce = Me.theInputFileReader.ReadSingle()

		aBone.theJiggleBone.baseMass = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseStiffness = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseDamping = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMinLeft = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMaxLeft = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseLeftFriction = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMinUp = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMaxUp = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseUpFriction = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMinForward = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMaxForward = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseForwardFriction = Me.theInputFileReader.ReadSingle()

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theJiggleBone")
	End Sub

	Public Sub ReadBoneControllers()
		If Me.theMdlFileData.boneControllerCount > 0 Then
			Dim boneControllerInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneControllerOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theBoneControllers = New List(Of SourceMdlBoneController)(Me.theMdlFileData.boneControllerCount)
			For i As Integer = 0 To Me.theMdlFileData.boneControllerCount - 1
				boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aBoneController As New SourceMdlBoneController()

				aBoneController.boneIndex = Me.theInputFileReader.ReadInt32()
				aBoneController.type = Me.theInputFileReader.ReadInt32()
				aBoneController.startBlah = Me.theInputFileReader.ReadSingle()
				aBoneController.endBlah = Me.theInputFileReader.ReadSingle()
				aBoneController.restIndex = Me.theInputFileReader.ReadInt32()
				aBoneController.inputField = Me.theInputFileReader.ReadInt32()
				If Me.theMdlFileData.version > 10 Then
					For x As Integer = 0 To aBoneController.unused.Length - 1
						aBoneController.unused(x) = Me.theInputFileReader.ReadInt32()
					Next
				End If

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

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localAttachmentOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theAttachments = New List(Of SourceMdlAttachment)(Me.theMdlFileData.localAttachmentCount)
			For i As Integer = 0 To Me.theMdlFileData.localAttachmentCount - 1
				attachmentInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anAttachment As New SourceMdlAttachment()

				If Me.theMdlFileData.version = 10 Then
					anAttachment.name = Me.theInputFileReader.ReadChars(32)
					anAttachment.theName = anAttachment.name
					anAttachment.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(anAttachment.theName)
					anAttachment.type = Me.theInputFileReader.ReadInt32()
					anAttachment.bone = Me.theInputFileReader.ReadInt32()

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
				Else
					anAttachment.nameOffset = Me.theInputFileReader.ReadInt32()
					anAttachment.flags = Me.theInputFileReader.ReadInt32()
					anAttachment.localBoneIndex = Me.theInputFileReader.ReadInt32()
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
					For x As Integer = 0 To 7
						anAttachment.unused(x) = Me.theInputFileReader.ReadInt32()
					Next
				End If

				Me.theMdlFileData.theAttachments.Add(anAttachment)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If anAttachment.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(attachmentInputFileStreamPosition + anAttachment.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					anAttachment.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName = " + anAttachment.theName)
					'End If
				Else
					anAttachment.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments " + Me.theMdlFileData.theAttachments.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment")
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

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.hitboxSetOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theHitboxSets = New List(Of SourceMdlHitboxSet)(Me.theMdlFileData.hitboxSetCount)
			For i As Integer = 0 To Me.theMdlFileData.hitboxSetCount - 1
				hitboxSetInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aHitboxSet As New SourceMdlHitboxSet()
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
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitboxSet.theName = " + aHitboxSet.theName)
					'End If
				Else
					aHitboxSet.theName = ""
				End If
				Me.ReadHitboxes(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, aHitboxSet)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxSets " + Me.theMdlFileData.theHitboxSets.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theHitboxSets alignment")
		End If
	End Sub

	Private Sub ReadHitboxes(ByVal hitboxOffsetInputFileStreamPosition As Long, ByVal aHitboxSet As SourceMdlHitboxSet)
		If aHitboxSet.hitboxCount > 0 Then
			Dim hitboxInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(hitboxOffsetInputFileStreamPosition, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aHitboxSet.theHitboxes = New List(Of SourceMdlHitbox)(aHitboxSet.hitboxCount)
			For j As Integer = 0 To aHitboxSet.hitboxCount - 1
				hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aHitbox As New SourceMdlHitbox()

				aHitbox.boneIndex = Me.theInputFileReader.ReadInt32()
				aHitbox.groupIndex = Me.theInputFileReader.ReadInt32()
				aHitbox.boundingBoxMin.x = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMin.y = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMin.z = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMax.x = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMax.y = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMax.z = Me.theInputFileReader.ReadSingle()
				aHitbox.nameOffset = Me.theInputFileReader.ReadInt32()
				'NOTE: Roll (z) is first.
				aHitbox.boundingBoxPitchYawRoll.z = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxPitchYawRoll.x = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxPitchYawRoll.y = Me.theInputFileReader.ReadSingle()
				aHitbox.unknown = Me.theInputFileReader.ReadSingle()
				For x As Integer = 0 To aHitbox.unused_VERSION49.Length - 1
					aHitbox.unused_VERSION49(x) = Me.theInputFileReader.ReadInt32()
				Next

				aHitboxSet.theHitboxes.Add(aHitbox)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'NOTE: Ignore getting the hitbox name because it's not important for compiling, 
				'      and unknown when to use nameOffset as absolute or relative offset.
				'NOTE: I looked at many models from various games that use MDL44 and none of them had anything for the name. 
				'NOTE: Contrary to the studiomdl source code for MDL44, the nameOffset is absolute offset for the following tested models 
				'      from Half-Life 2\hl1\hl1_pak_dir\models: agrunt, scientist
				'      Me.theInputFileReader.BaseStream.Seek(aHitbox.nameOffset, SeekOrigin.Begin)
				'NOTE: A custom CSS model (MDL v48) has "aHitbox.nameOffset <> 0", 
				'      and it looks like the aHitbox.nameOffset is intended to be an absolute offset 
				'      to the initial null byte at the start of the string list at end of file.
				'      CSS HLMV opens and shows the hitboxes.
				'NOTE: The "SCAL" seems to be in every MDL file in Vindictus.
				If aHitbox.nameOffset <> 0 AndAlso ((Me.theMdlFileData.version = 44 AndAlso Me.theMdlFileData.flexControllerUiOffset = SourceMdlFileData49.text_SCAL_VERSION44Vindictus) OrElse Me.theMdlFileData.version = 49) Then
					'NOTE: For Vindictus pet_succubus_tiny.mdl (MDL44), relative offset is correct.
					Me.theInputFileReader.BaseStream.Seek(hitboxInputFileStreamPosition + aHitbox.nameOffset, SeekOrigin.Begin)
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
		End If
	End Sub

	Public Sub ReadBoneTableByName()
		If Me.theMdlFileData.boneTableByNameOffset <> 0 AndAlso Me.theMdlFileData.theBones IsNot Nothing Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneTableByNameOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theBoneTableByName = New List(Of Integer)(Me.theMdlFileData.theBones.Count)
			Dim index As Byte
			For i As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
				index = Me.theInputFileReader.ReadByte()
				Me.theMdlFileData.theBoneTableByName.Add(index)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneTableByName")
		End If
	End Sub

	Public Sub ReadLocalAnimationDescs()
		Dim animInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localAnimationOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theMdlFileData.theAnimationDescs = New List(Of SourceMdlAnimationDesc49)(Me.theMdlFileData.localAnimationCount)
		For i As Integer = 0 To Me.theMdlFileData.localAnimationCount - 1
			animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anAnimationDesc As New SourceMdlAnimationDesc49()

			anAnimationDesc.theOffsetStart = Me.theInputFileReader.BaseStream.Position

			anAnimationDesc.baseHeaderOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.nameOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.fps = Me.theInputFileReader.ReadSingle()
			anAnimationDesc.flags = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.frameCount = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.movementCount = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.movementOffset = Me.theInputFileReader.ReadInt32()

			anAnimationDesc.ikRuleZeroFrameOffset_VERSION49 = Me.theInputFileReader.ReadInt32()

			For x As Integer = 0 To anAnimationDesc.unused1.Length - 1
				anAnimationDesc.unused1(x) = Me.theInputFileReader.ReadInt32()
			Next

			anAnimationDesc.animBlock = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.animOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.ikRuleCount = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.ikRuleOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.animblockIkRuleOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.localHierarchyCount = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.localHierarchyOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.sectionOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.sectionFrameCount = Me.theInputFileReader.ReadInt32()

			anAnimationDesc.spanFrameCount = Me.theInputFileReader.ReadInt16()
			anAnimationDesc.spanCount = Me.theInputFileReader.ReadInt16()
			anAnimationDesc.spanOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.spanStallTime = Me.theInputFileReader.ReadSingle()

			Me.theMdlFileData.theAnimationDescs.Add(anAnimationDesc)

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			Me.ReadAnimationDescName(animInputFileStreamPosition, anAnimationDesc)
			Me.ReadAnimationDescSpanData(animInputFileStreamPosition, anAnimationDesc)
			Me.ReadMdlMovements(animInputFileStreamPosition, anAnimationDesc)

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs " + Me.theMdlFileData.theAnimationDescs.Count.ToString())
	End Sub

	Public Sub ReadAnimationSections()
		If Me.theMdlFileData.theAnimationDescs IsNot Nothing Then
			For Each anAnimationDesc As SourceMdlAnimationDesc49 In Me.theMdlFileData.theAnimationDescs
				anAnimationDesc.theSectionsOfFrameAnim = New List(Of SourceAniFrameAnim49)()
				Dim aSectionOfFrameAnimation As SourceAniFrameAnim49
				aSectionOfFrameAnimation = New SourceAniFrameAnim49()
				anAnimationDesc.theSectionsOfFrameAnim.Add(aSectionOfFrameAnimation)

				anAnimationDesc.theSectionsOfAnimations = New List(Of List(Of SourceMdlAnimation))()
				Dim aSectionOfAnimation As List(Of SourceMdlAnimation)
				aSectionOfAnimation = New List(Of SourceMdlAnimation)()
				anAnimationDesc.theSectionsOfAnimations.Add(aSectionOfAnimation)

				If anAnimationDesc.sectionOffset <> 0 AndAlso anAnimationDesc.sectionFrameCount > 0 Then
					'TODO: Shouldn't this be set to largest sectionFrameCount?
					Me.theMdlFileData.theSectionFrameCount = anAnimationDesc.sectionFrameCount
					If Me.theMdlFileData.theSectionFrameMinFrameCount >= anAnimationDesc.frameCount Then
						Me.theMdlFileData.theSectionFrameMinFrameCount = anAnimationDesc.frameCount - 1
					End If

					'FROM: simplify.cpp:
					'      panim->numsections = (int)(panim->numframes / panim->sectionframes) + 2;
					'NOTE: It is unclear why "+ 2" is used in studiomdl.
					Dim sectionCount As Integer = CInt(Math.Truncate(anAnimationDesc.frameCount / anAnimationDesc.sectionFrameCount)) + 2

					'NOTE: First sectionOfAnimation was created above.
					For sectionIndex As Integer = 1 To sectionCount - 1
						aSectionOfFrameAnimation = New SourceAniFrameAnim49()
						anAnimationDesc.theSectionsOfFrameAnim.Add(aSectionOfFrameAnimation)

						aSectionOfAnimation = New List(Of SourceMdlAnimation)()
						anAnimationDesc.theSectionsOfAnimations.Add(aSectionOfAnimation)
					Next

					Dim offset As Long
					offset = anAnimationDesc.theOffsetStart + anAnimationDesc.sectionOffset
					If offset <> Me.theInputFileReader.BaseStream.Position Then
						'TODO: It looks like more than one animDesc can point to same sections, so need to revise how this is done.
						'Me.theMdlFileData.theFileSeekLog.Add(Me.theInputFileReader.BaseStream.Position, Me.theInputFileReader.BaseStream.Position, "[ERROR] anAnimationDesc.theSections [" + anAnimationDesc.theName + "] offset mismatch: pos = " + Me.theInputFileReader.BaseStream.Position.ToString() + " offset = " + offset.ToString())
						Me.theInputFileReader.BaseStream.Seek(offset, SeekOrigin.Begin)
					End If

					anAnimationDesc.theSections = New List(Of SourceMdlAnimationSection)(sectionCount)
					For sectionIndex As Integer = 0 To sectionCount - 1
						Me.ReadMdlAnimationSection(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, Me.theMdlFileData.theFileSeekLog)
					Next
				End If
			Next
		End If
	End Sub

	Public Sub ReadAnimationMdlBlocks()
		If Me.theMdlFileData.theAnimationDescs IsNot Nothing Then
			For Each anAnimationDesc As SourceMdlAnimationDesc49 In Me.theMdlFileData.theAnimationDescs
				Try
					Dim animInputFileStreamPosition As Long = anAnimationDesc.theOffsetStart
					Dim sectionIndex As Integer

					'NOTE: Need to check section.animBlock no matter what anAnimationDesc.animBlock is.
					If anAnimationDesc.theSections IsNot Nothing AndAlso anAnimationDesc.theSections.Count > 0 Then
						Dim sectionCount As Integer = anAnimationDesc.theSections.Count
						Dim sectionFrameCount As Integer
						Dim section As SourceMdlAnimationSection
						Dim adjustedAnimOffset As Long

						For sectionIndex = 0 To sectionCount - 1
							section = anAnimationDesc.theSections(sectionIndex)
							If section.animBlock = 0 Then
								'NOTE: This is weird, but it fits with a few oddball models (such as L4D2 "left4dead2\ghostanim.mdl") while not messing up the normal ones.
								adjustedAnimOffset = section.animOffset + (anAnimationDesc.animOffset - anAnimationDesc.theSections(0).animOffset)

								If sectionIndex < sectionCount - 2 Then
									sectionFrameCount = anAnimationDesc.sectionFrameCount
								Else
									'NOTE: Due to the weird calculation of sectionCount in studiomdl, this line is called twice, which means there are two "last" sections.
									'      This also likely means that the last section is bogus unused data.
									sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 2) * anAnimationDesc.sectionFrameCount)
								End If

								Me.ReadAnimationFrames(animInputFileStreamPosition + adjustedAnimOffset, anAnimationDesc, sectionFrameCount, sectionIndex, (sectionIndex >= sectionCount - 2) Or (anAnimationDesc.frameCount = (sectionIndex + 1) * anAnimationDesc.sectionFrameCount))
							End If
						Next
					ElseIf anAnimationDesc.animBlock = 0 Then
						sectionIndex = 0
						Me.ReadAnimationFrames(animInputFileStreamPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, sectionIndex, True)
					End If

					'NOTE: These seem to always be stored in the MDL file for MDL44.
					If Me.theMdlFileData.version = 44 OrElse anAnimationDesc.animBlock = 0 Then
						Me.ReadMdlIkRules(animInputFileStreamPosition, anAnimationDesc)
						Me.ReadLocalHierarchies(animInputFileStreamPosition, anAnimationDesc)
					End If
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
			Next
		End If
	End Sub

	Protected Sub ReadAnimationDescName(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49)
		If anAnimationDesc.nameOffset <> 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			anAnimationDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)
			'If anAnimDesc.theName(0) = "@" Then
			'	anAnimDesc.theName = anAnimDesc.theName.Remove(0, 1)
			'End If

			'NOTE: This naming is found in Garry's Mod garrysmod_dir.vpk "\models\m_anm.mdl":  "a_../combine_soldier_xsi/Hold_AR2_base.smd"
			If anAnimationDesc.theName.StartsWith("a_../") OrElse anAnimationDesc.theName.StartsWith("a_..\") Then
				anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 5)
				anAnimationDesc.theName = Path.Combine(FileManager.GetPath(anAnimationDesc.theName), "a_" + Path.GetFileName(anAnimationDesc.theName))
			End If

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theName = " + anAnimationDesc.theName)
		Else
			anAnimationDesc.theName = ""
		End If
	End Sub

	'			for (j = 0; j < g_numbones; j++)
	'			{
	'				if (g_bonetable[j].flags & BONE_HAS_SAVEFRAME_POS)
	'				{
	'					for (int n = 0; n < panimdesc[i].zeroframecount; n++)
	'					{
	'						*(Vector48 *)pData = anim->sanim[panimdesc[i].zeroframespan*n][j].pos;
	'						pData += sizeof( Vector48 );
	'					}
	'				}
	'				if (g_bonetable[j].flags & BONE_HAS_SAVEFRAME_ROT)
	'				{
	'					for (int n = 0; n < panimdesc[i].zeroframecount; n++)
	'					{
	'						Quaternion q;
	'						AngleQuaternion( anim->sanim[panimdesc[i].zeroframespan*n][j].rot, q );
	'						*((Quaternion64 *)pData) = q;
	'						pData += sizeof( Quaternion64 );
	'					}
	'				}
	'			}
	Protected Function ReadAnimationDescSpanData(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49) As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long = 0

		' The data seems to be copy of first several anim frames in the ANI file.
		If anAnimationDesc.spanFrameCount <> 0 OrElse anAnimationDesc.spanCount <> 0 OrElse anAnimationDesc.spanOffset <> 0 OrElse anAnimationDesc.spanStallTime <> 0 Then
			'NOTE: This code is reached by HL2 antlion.mdl and HL2:EP1 ghostanim_van.mdl.
			'NOTE: This code is reached by L4D2's pak01_dir.vpk\models\v_models\v_huntingrifle.mdl and v_snip_awp.mdl.
			'NOTE: This code is reached by DoI's doi_models_dir_vpk\models\weapons\v_g43.mdl and v_vickers.mdl.
			fileOffsetStart = animInputFileStreamPosition + anAnimationDesc.spanOffset
			fileOffsetEnd = animInputFileStreamPosition + anAnimationDesc.spanOffset - 1
			Dim aBone As SourceMdlBone
			For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
				aBone = Me.theMdlFileData.theBones(boneIndex)
				If (aBone.flags And SourceMdlBone.BONE_HAS_SAVEFRAME_POS) > 0 Then
					'SourceVector48bits (6 bytes)
					fileOffsetEnd += anAnimationDesc.spanCount * 6
				End If
				If (aBone.flags And SourceMdlBone.BONE_HAS_SAVEFRAME_ROT) > 0 Then
					'SourceQuaternion64bits (8 bytes)
					fileOffsetEnd += anAnimationDesc.spanCount * 8
				End If
				If (aBone.flags And SourceMdlBone.BONE_HAS_SAVEFRAME_ROT32) > 0 Then
					'SourceQuaternion32bits (4 bytes)
					fileOffsetEnd += anAnimationDesc.spanCount * 4
				End If
				If aBone.flags > &HFFFFFF Then
					Dim debug As Integer = 4242
				End If
			Next

			Me.theMdlFileData.theAnimBlockSizeNoStallOptionIsUsed = True

			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.spanOffset (zeroframes/saveframes) [" + anAnimationDesc.theName + "] [spanFrameCount = " + anAnimationDesc.spanFrameCount.ToString() + "] [spanCount = " + anAnimationDesc.spanCount.ToString() + "]")

			''TEST: 
			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.spanOffset (zeroframes/saveframes) alignment")
			''TEST: 
			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 8, "anAnimationDesc.spanOffset (zeroframes/saveframes) alignment")
			''TEST: 
			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 16, "anAnimationDesc.spanOffset (zeroframes/saveframes) alignment")

			'If fileOffsetEndOfAnimDescsIncludingSpanData < fileOffsetEnd Then
			'	fileOffsetEndOfAnimDescsIncludingSpanData = fileOffsetEnd
			'End If
		End If

		Return fileOffsetEnd
	End Function

	Protected Sub ReadAnimationFrames(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49, ByVal sectionFrameCount As Integer, ByVal sectionIndex As Integer, ByVal lastSectionIsBeingRead As Boolean)
		If ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_FRAMEANIM) <> 0) Then
			'TODO: Do any MDL v48 models use this flag?
			'NOTE: This code is reached by L4D2's pak01_dir.vpk\models\v_models\v_huntingrifle.mdl.
			Me.ReadAnimationFrameByBone(animInputFileStreamPosition, anAnimationDesc, sectionFrameCount, sectionIndex, lastSectionIsBeingRead)
		Else
			Me.ReadMdlAnimation(animInputFileStreamPosition, anAnimationDesc, sectionFrameCount, sectionIndex, lastSectionIsBeingRead)
		End If
	End Sub

	Protected Sub ReadAnimationFrameByBone(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49, ByVal sectionFrameCount As Integer, ByVal sectionIndex As Integer, ByVal lastSectionIsBeingRead As Boolean)
		Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition, SeekOrigin.Begin)

		Dim animFrameInputFileStreamPosition As Long
		Dim boneFrameDataStartInputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim boneCount As Integer
		Dim boneFlag As Byte
		Dim aBoneConstantInfo As BoneConstantInfo49
		Dim aBoneFrameDataInfoList As List(Of BoneFrameDataInfo49)
		Dim aBoneFrameDataInfo As BoneFrameDataInfo49

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Dim aSectionOfAnimation As SourceAniFrameAnim49
		aSectionOfAnimation = anAnimationDesc.theSectionsOfFrameAnim(sectionIndex)

		boneCount = Me.theMdlFileData.theBones.Count
		Try
			animFrameInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aSectionOfAnimation.constantsOffset = Me.theInputFileReader.ReadInt32()
			aSectionOfAnimation.frameOffset = Me.theInputFileReader.ReadInt32()
			aSectionOfAnimation.frameLength = Me.theInputFileReader.ReadInt32()
			For x As Integer = 0 To aSectionOfAnimation.unused.Length - 1
				aSectionOfAnimation.unused(x) = Me.theInputFileReader.ReadInt32()
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAniFrameAnim [" + anAnimationDesc.theName + "] (frameCount = " + CStr(anAnimationDesc.frameCount) + "; sectionFrameCount = " + CStr(sectionFrameCount) + ")")

			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aSectionOfAnimation.theBoneFlags = New List(Of Byte)(boneCount)
			For boneIndex As Integer = 0 To boneCount - 1
				boneFlag = Me.theInputFileReader.ReadByte()
				aSectionOfAnimation.theBoneFlags.Add(boneFlag)

				'DEBUG:
				If (boneFlag And &H20) > 0 Then
					Dim unknownFlagIsUsed As Integer = 4242
				End If
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAniFrameAnim.theBoneFlags " + aSectionOfAnimation.theBoneFlags.Count.ToString())
			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAniFrameAnim.theBoneFlags alignment")

			If aSectionOfAnimation.constantsOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(animFrameInputFileStreamPosition + aSectionOfAnimation.constantsOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aSectionOfAnimation.theBoneConstantInfos = New List(Of BoneConstantInfo49)(boneCount)
				For boneIndex As Integer = 0 To boneCount - 1
					aBoneConstantInfo = New BoneConstantInfo49()
					aSectionOfAnimation.theBoneConstantInfos.Add(aBoneConstantInfo)

					boneFlag = aSectionOfAnimation.theBoneFlags(boneIndex)
					If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_CONST_ROT2) > 0 Then
						aBoneConstantInfo.theConstantRotationUnknown = New SourceQuaternion48bitsViaBytes()
						aBoneConstantInfo.theConstantRotationUnknown.theBytes = Me.theInputFileReader.ReadBytes(6)
					End If
					If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_RAWROT) > 0 Then
						aBoneConstantInfo.theConstantRawRot = New SourceQuaternion48bits()
						aBoneConstantInfo.theConstantRawRot.theXInput = Me.theInputFileReader.ReadUInt16()
						aBoneConstantInfo.theConstantRawRot.theYInput = Me.theInputFileReader.ReadUInt16()
						aBoneConstantInfo.theConstantRawRot.theZWInput = Me.theInputFileReader.ReadUInt16()
					End If
					If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_RAWPOS) > 0 Then
						aBoneConstantInfo.theConstantRawPos = New SourceVector48bits()
						aBoneConstantInfo.theConstantRawPos.theXInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
						aBoneConstantInfo.theConstantRawPos.theYInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
						aBoneConstantInfo.theConstantRawPos.theZInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
					End If
					'If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_CONST_ROT2) > 0 Then
					'	aBoneConstantInfo.theConstantRotationUnknown = New SourceQuaternion48bitsViaBytes()
					'	aBoneConstantInfo.theConstantRotationUnknown.theBytes = Me.theInputFileReader.ReadBytes(6)
					'End If
				Next

				If Me.theInputFileReader.BaseStream.Position > fileOffsetStart Then
					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSectionOfAnimation.theBoneConstantInfos " + aSectionOfAnimation.theBoneConstantInfos.Count.ToString())
					Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSectionOfAnimation.theBoneConstantInfos alignment")
				End If
			End If

			If aSectionOfAnimation.frameLength > 0 AndAlso aSectionOfAnimation.frameOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(animFrameInputFileStreamPosition + aSectionOfAnimation.frameOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aSectionOfAnimation.theBoneFrameDataInfos = New List(Of List(Of BoneFrameDataInfo49))(sectionFrameCount)

				'NOTE: This adjustment is weird, but it fits all the data I've seen.
				Dim adjustedFrameCount As Integer
				If lastSectionIsBeingRead Then
					adjustedFrameCount = sectionFrameCount
				Else
					adjustedFrameCount = sectionFrameCount + 1
				End If

				For frameIndex As Integer = 0 To adjustedFrameCount - 1
					aBoneFrameDataInfoList = New List(Of BoneFrameDataInfo49)(boneCount)
					If lastSectionIsBeingRead OrElse (frameIndex < (adjustedFrameCount - 1)) Then
						aSectionOfAnimation.theBoneFrameDataInfos.Add(aBoneFrameDataInfoList)
					End If

					boneFrameDataStartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					For boneIndex As Integer = 0 To boneCount - 1
						aBoneFrameDataInfo = New BoneFrameDataInfo49()
						aBoneFrameDataInfoList.Add(aBoneFrameDataInfo)

						boneFlag = aSectionOfAnimation.theBoneFlags(boneIndex)

						If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_ANIM_ROT2) > 0 Then
							aBoneFrameDataInfo.theAnimRotationUnknown = New SourceQuaternion48bitsViaBytes()
							aBoneFrameDataInfo.theAnimRotationUnknown.theBytes = Me.theInputFileReader.ReadBytes(6)
						End If
						If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_ANIMROT) > 0 Then
							aBoneFrameDataInfo.theAnimRotation = New SourceQuaternion48bits()
							aBoneFrameDataInfo.theAnimRotation.theXInput = Me.theInputFileReader.ReadUInt16()
							aBoneFrameDataInfo.theAnimRotation.theYInput = Me.theInputFileReader.ReadUInt16()
							aBoneFrameDataInfo.theAnimRotation.theZWInput = Me.theInputFileReader.ReadUInt16()
						End If
						If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_ANIMPOS) > 0 Then
							aBoneFrameDataInfo.theAnimPosition = New SourceVector48bits()
							aBoneFrameDataInfo.theAnimPosition.theXInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
							aBoneFrameDataInfo.theAnimPosition.theYInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
							aBoneFrameDataInfo.theAnimPosition.theZInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
						End If
						If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_FULLANIMPOS) > 0 Then
							aBoneFrameDataInfo.theFullAnimPosition = New SourceVector()
							aBoneFrameDataInfo.theFullAnimPosition.x = Me.theInputFileReader.ReadSingle()
							aBoneFrameDataInfo.theFullAnimPosition.y = Me.theInputFileReader.ReadSingle()
							aBoneFrameDataInfo.theFullAnimPosition.z = Me.theInputFileReader.ReadSingle()
						End If
						'If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_ANIM_ROT2) > 0 Then
						'	aBoneFrameDataInfo.theAnimRotationUnknown = New SourceQuaternion48bitsViaBytes()
						'	aBoneFrameDataInfo.theAnimRotationUnknown.theBytes = Me.theInputFileReader.ReadBytes(6)
						'End If
					Next

					'DEBUG: Check frame data length for debugging.
					If ((aSectionOfAnimation.frameLength) <> (Me.theInputFileReader.BaseStream.Position - boneFrameDataStartInputFileStreamPosition)) Then
						Dim somethingIsWrong As Integer = 4242
					End If
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Dim text As String
				text = "aSectionOfAnimation.theBoneFrameDataInfos " + aSectionOfAnimation.theBoneFrameDataInfos.Count.ToString()
				If Not lastSectionIsBeingRead Then
					text += " plus an extra unused aBoneFrameDataInfo"
				End If
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, text)
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSectionOfAnimation.theBoneFrameDataInfos alignment")
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Protected Sub ReadMdlAnimation(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49, ByVal sectionFrameCount As Integer, ByVal sectionIndex As Integer, ByVal lastSectionIsBeingRead As Boolean)
		Dim animationInputFileStreamPosition As Long
		Dim nextAnimationInputFileStreamPosition As Long
		Dim animValuePointerInputFileStreamPosition As Long
		Dim rotValuePointerInputFileStreamPosition As Long
		Dim posValuePointerInputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim anAnimation As SourceMdlAnimation
		Dim boneCount As Integer
		Dim boneIndex As Byte

		Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition, SeekOrigin.Begin)

		Dim aSectionOfAnimation As List(Of SourceMdlAnimation)
		aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)

		If Me.theMdlFileData.theBones Is Nothing Then
			boneCount = 1
		Else
			boneCount = Me.theMdlFileData.theBones.Count
		End If
		For j As Integer = 0 To boneCount - 1
			animationInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			boneIndex = Me.theInputFileReader.ReadByte()
			If boneIndex = 255 Then
				Me.theInputFileReader.ReadByte()
				Me.theInputFileReader.ReadInt16()

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(animationInputFileStreamPosition, fileOffsetEnd, "anAnimationDesc.anAnimation [" + anAnimationDesc.theName + "] (boneIndex = 255)")

				Exit For
			End If
			'DEBUG:
			If boneIndex >= boneCount Then
				' L4D2 "left4dead2\ghostanim.mdl" reaches here.
				Dim badIfGetsHere As Integer = 42
				Exit For
			End If

			anAnimation = New SourceMdlAnimation()
			aSectionOfAnimation.Add(anAnimation)

			anAnimation.boneIndex = boneIndex
			anAnimation.flags = Me.theInputFileReader.ReadByte()
			anAnimation.nextSourceMdlAnimationOffset = Me.theInputFileReader.ReadInt16()

			'DEBUG:
			If (anAnimation.flags And &H40) > 0 Then
				Dim badIfGetsHere As Integer = 42
			End If
			If (anAnimation.flags And &H80) > 0 Then
				Dim badIfGetsHere As Integer = 42
			End If

			'If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
			'End If
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
				anAnimation.theRot64bits = New SourceQuaternion64bits()
				anAnimation.theRot64bits.theBytes = Me.theInputFileReader.ReadBytes(8)

				'Me.DebugQuaternion(anAnimation.theRot64)
			End If
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
				anAnimation.theRot48bits = New SourceQuaternion48bits()
				anAnimation.theRot48bits.theXInput = Me.theInputFileReader.ReadUInt16()
				anAnimation.theRot48bits.theYInput = Me.theInputFileReader.ReadUInt16()
				anAnimation.theRot48bits.theZWInput = Me.theInputFileReader.ReadUInt16()
			End If
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0 Then
				anAnimation.thePos = New SourceVector48bits()
				anAnimation.thePos.theXInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
				anAnimation.thePos.theYInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
				anAnimation.thePos.theZInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
			End If

			animValuePointerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			' First, read both sets of offsets.
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0 Then
				rotValuePointerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				anAnimation.theRotV = New SourceMdlAnimationValuePointer()

				anAnimation.theRotV.animXValueOffset = Me.theInputFileReader.ReadInt16()
				If anAnimation.theRotV.theAnimXValues Is Nothing Then
					anAnimation.theRotV.theAnimXValues = New List(Of SourceMdlAnimationValue)()
				End If

				anAnimation.theRotV.animYValueOffset = Me.theInputFileReader.ReadInt16()
				If anAnimation.theRotV.theAnimYValues Is Nothing Then
					anAnimation.theRotV.theAnimYValues = New List(Of SourceMdlAnimationValue)()
				End If

				anAnimation.theRotV.animZValueOffset = Me.theInputFileReader.ReadInt16()
				If anAnimation.theRotV.theAnimZValues Is Nothing Then
					anAnimation.theRotV.theAnimZValues = New List(Of SourceMdlAnimationValue)()
				End If
			End If
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0 Then
				posValuePointerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				anAnimation.thePosV = New SourceMdlAnimationValuePointer()

				anAnimation.thePosV.animXValueOffset = Me.theInputFileReader.ReadInt16()
				If anAnimation.thePosV.theAnimXValues Is Nothing Then
					anAnimation.thePosV.theAnimXValues = New List(Of SourceMdlAnimationValue)()
				End If

				anAnimation.thePosV.animYValueOffset = Me.theInputFileReader.ReadInt16()
				If anAnimation.thePosV.theAnimYValues Is Nothing Then
					anAnimation.thePosV.theAnimYValues = New List(Of SourceMdlAnimationValue)()
				End If

				anAnimation.thePosV.animZValueOffset = Me.theInputFileReader.ReadInt16()
				If anAnimation.thePosV.theAnimZValues Is Nothing Then
					anAnimation.thePosV.theAnimZValues = New List(Of SourceMdlAnimationValue)()
				End If
			End If

			Me.theMdlFileData.theFileSeekLog.Add(animationInputFileStreamPosition, Me.theInputFileReader.BaseStream.Position - 1, "anAnimationDesc.anAnimation  [" + anAnimationDesc.theName + "] (frameCount = " + CStr(anAnimationDesc.frameCount) + "; sectionFrameCount = " + CStr(sectionFrameCount) + ")")

			' Second, read the anim values using the offsets.
			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0 Then
				If anAnimation.theRotV.animXValueOffset > 0 Then
					Me.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animXValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.theRotV.theAnimXValues, "anAnimation.theRotV.theAnimXValues")
				End If
				If anAnimation.theRotV.animYValueOffset > 0 Then
					Me.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animYValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.theRotV.theAnimYValues, "anAnimation.theRotV.theAnimYValues")
				End If
				If anAnimation.theRotV.animZValueOffset > 0 Then
					Me.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animZValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.theRotV.theAnimZValues, "anAnimation.theRotV.theAnimZValues")
				End If
			End If
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0 Then
				If anAnimation.thePosV.animXValueOffset > 0 Then
					Me.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animXValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.thePosV.theAnimXValues, "anAnimation.thePosV.theAnimXValues")
				End If
				If anAnimation.thePosV.animYValueOffset > 0 Then
					Me.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animYValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.thePosV.theAnimYValues, "anAnimation.thePosV.theAnimYValues")
				End If
				If anAnimation.thePosV.animZValueOffset > 0 Then
					Me.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animZValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.thePosV.theAnimZValues, "anAnimation.thePosV.theAnimZValues")
				End If
			End If
			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

			'NOTE: If the offset is 0 then there are no more bone animation structures, so end the loop.
			If anAnimation.nextSourceMdlAnimationOffset = 0 Then
				'j = boneCount
				'lastFullAnimDataWasFound = True
				Exit For
			Else
				' Skip to next anim, just in case not all data is being read in.
				nextAnimationInputFileStreamPosition = animationInputFileStreamPosition + anAnimation.nextSourceMdlAnimationOffset
				''TEST: Use this with ANI file, to see if it extracts better.
				'nextAnimationInputFileStreamPosition = animationInputFileStreamPosition + CType(anAnimation.nextSourceMdlAnimationOffset, UShort)
				If nextAnimationInputFileStreamPosition < Me.theInputFileReader.BaseStream.Position Then
					'PROBLEM! Should not be going backwards in file.
					Dim i As Integer = 42
					Exit For
				ElseIf nextAnimationInputFileStreamPosition > Me.theInputFileReader.BaseStream.Position Then
					'PROBLEM! Should not be skipping ahead. Crowbar has skipped some data, but continue decompiling.
					Dim i As Integer = 42
				End If

				Me.theInputFileReader.BaseStream.Seek(nextAnimationInputFileStreamPosition, SeekOrigin.Begin)
			End If
		Next

		If boneIndex <> 255 Then
			'NOTE: There is always an unused empty data structure at the end of the list.
			'prevanim					= destanim;
			'destanim->nextoffset		= pData - (byte *)destanim;
			'destanim					= (mstudioanim_t *)pData;
			'pData						+= sizeof( *destanim );
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position
			Me.theInputFileReader.ReadByte()
			Me.theInputFileReader.ReadByte()
			Me.theInputFileReader.ReadInt16()

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.anAnimation [" + anAnimationDesc.theName + "] (unused empty data structure at the end of the list)")
		End If

		'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.anAnimation [" + anAnimationDesc.theName + "] alignment")
	End Sub

	'==========================================================
	'FROM: SourceEngine2007_source\utils\studiomdl\simplify.cpp
	'      Section within: static void CompressAnimations( ).
	'      This shows how the data is stored before being written to file.
	'memset( data, 0, sizeof( data ) ); 
	'pcount = data; 
	'pvalue = pcount + 1;
	'
	'pcount->num.valid = 1;
	'pcount->num.total = 1;
	'pvalue->value = value[0];
	'pvalue++;
	'
	'// build a RLE of deltas from the default pose
	'for (m = 1; m < n; m++)
	'{
	'	if (pcount->num.total == 255)
	'	{
	'		// chain too long, force a new entry
	'		pcount = pvalue;
	'		pvalue = pcount + 1;
	'		pcount->num.valid++;
	'		pvalue->value = value[m];
	'		pvalue++;
	'	} 
	'	// insert value if they're not equal, 
	'	// or if we're not on a run and the run is less than 3 units
	'	else if ((value[m] != value[m-1]) 
	'		|| ((pcount->num.total == pcount->num.valid) && ((m < n - 1) && value[m] != value[m+1])))
	'	{
	'		if (pcount->num.total != pcount->num.valid)
	'		{
	'			//if (j == 0) printf("%d:%d   ", pcount->num.valid, pcount->num.total ); 
	'			pcount = pvalue;
	'			pvalue = pcount + 1;
	'		}
	'		pcount->num.valid++;
	'		pvalue->value = value[m];
	'		pvalue++;
	'	}
	'	pcount->num.total++;
	'}
	'//if (j == 0) printf("%d:%d\n", pcount->num.valid, pcount->num.total ); 
	'
	'panim->anim[w][j].num[k] = pvalue - data;
	'if (panim->anim[w][j].num[k] == 2 && value[0] == 0)
	'{
	'	panim->anim[w][j].num[k] = 0;
	'}
	'else
	'{
	'	panim->anim[w][j].data[k] = (mstudioanimvalue_t *)kalloc( pvalue - data, sizeof( mstudioanimvalue_t ) );
	'	memmove( panim->anim[w][j].data[k], data, (pvalue - data) * sizeof( mstudioanimvalue_t ) );
	'}

	'=======================================================
	'FROM: SourceEngine2007_source\utils\studiomdl\write.cpp
	'      Section within: void WriteAnimationData( s_animation_t *srcanim, mstudioanimdesc_t *destanimdesc, byte *&pLocalData, byte *&pExtData ).
	'      This shows how the data is written to file.
	'mstudioanim_valueptr_t *posvptr	= NULL;
	'mstudioanim_valueptr_t *rotvptr	= NULL;
	'
	'// allocate room for rotation ptrs
	'rotvptr	= (mstudioanim_valueptr_t *)pData;
	'pData += sizeof( *rotvptr );
	'
	'// skip all position info if there's no animation
	'if (psrcdata->num[0] != 0 || psrcdata->num[1] != 0 || psrcdata->num[2] != 0)
	'{
	'	posvptr	= (mstudioanim_valueptr_t *)pData;
	'	pData += sizeof( *posvptr );
	'}
	'
	'mstudioanimvalue_t	*destanimvalue = (mstudioanimvalue_t *)pData;
	'
	'if (rotvptr)
	'{
	'	// store rotation animations
	'	for (k = 3; k < 6; k++)
	'	{
	'		if (psrcdata->num[k] == 0)
	'		{
	'			rotvptr->offset[k-3] = 0;
	'		}
	'		else
	'		{
	'			rotvptr->offset[k-3] = ((byte *)destanimvalue - (byte *)rotvptr);
	'			for (n = 0; n < psrcdata->num[k]; n++)
	'			{
	'				destanimvalue->value = psrcdata->data[k][n].value;
	'				destanimvalue++;
	'			}
	'		}
	'	}
	'	destanim->flags |= STUDIO_ANIM_ANIMROT;
	'}
	'
	'if (posvptr)
	'{
	'	// store position animations
	'	for (k = 0; k < 3; k++)
	'	{
	'		if (psrcdata->num[k] == 0)
	'		{
	'			posvptr->offset[k] = 0;
	'		}
	'		else
	'		{
	'			posvptr->offset[k] = ((byte *)destanimvalue - (byte *)posvptr);
	'			for (n = 0; n < psrcdata->num[k]; n++)
	'			{
	'				destanimvalue->value = psrcdata->data[k][n].value;
	'				destanimvalue++;
	'			}
	'		}
	'	}
	'	destanim->flags |= STUDIO_ANIM_ANIMPOS;
	'}
	'rawanimbytes += ((byte *)destanimvalue - pData);
	'pData = (byte *)destanimvalue;

	'===================================================
	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'      The ExtractAnimValue function shows how the values are extracted per frame from the data in the mdl file.
	'void ExtractAnimValue( int frame, mstudioanimvalue_t *panimvalue, float scale, float &v1 )
	'{
	'	if ( !panimvalue )
	'	{
	'		v1 = 0;
	'		return;
	'	}

	'	int k = frame;

	'	while (panimvalue->num.total <= k)
	'	{
	'		k -= panimvalue->num.total;
	'		panimvalue += panimvalue->num.valid + 1;
	'		if ( panimvalue->num.total == 0 )
	'		{
	'			Assert( 0 ); // running off the end of the animation stream is bad
	'			v1 = 0;
	'			return;
	'		}
	'	}
	'	if (panimvalue->num.valid > k)
	'	{
	'		v1 = panimvalue[k+1].value * scale;
	'	}
	'	else
	'	{
	'		// get last valid data block
	'		v1 = panimvalue[panimvalue->num.valid].value * scale;
	'	}
	'}
	Private Sub ReadMdlAnimValues(ByVal animValuesInputFileStreamPosition As Long, ByVal frameCount As Integer, ByVal lastSectionIsBeingRead As Boolean, ByVal theAnimValues As List(Of SourceMdlAnimationValue), ByVal debugDescription As String)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim frameCountRemainingToBeChecked As Integer
		Dim animValue As New SourceMdlAnimationValue()
		Dim currentTotal As Byte
		Dim validCount As Byte
		Dim accumulatedTotal As Integer

		Me.theInputFileReader.BaseStream.Seek(animValuesInputFileStreamPosition, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		frameCountRemainingToBeChecked = frameCount
		accumulatedTotal = 0
		While (frameCountRemainingToBeChecked > 0)
			animValue.value = Me.theInputFileReader.ReadInt16()
			currentTotal = animValue.total
			accumulatedTotal += currentTotal
			If currentTotal = 0 Then
				Dim badIfThisIsReached As Integer = 42
				Exit While
			End If
			frameCountRemainingToBeChecked -= currentTotal
			theAnimValues.Add(animValue)

			validCount = animValue.valid
			For i As Integer = 1 To validCount
				animValue.value = Me.theInputFileReader.ReadInt16()
				theAnimValues.Add(animValue)
			Next
		End While
		If Not lastSectionIsBeingRead AndAlso accumulatedTotal = frameCount Then
			Me.theInputFileReader.ReadInt16()
			Me.theInputFileReader.ReadInt16()
		End If

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, debugDescription + " (accumulatedTotal = " + CStr(accumulatedTotal) + ")")
	End Sub

	'Private Sub DebugQuaternion(ByVal q As SourceQuaternion64)
	'	Dim sqx As Double = q.X * q.X
	'	Dim sqy As Double = q.Y * q.Y
	'	Dim sqz As Double = q.Z * q.Z
	'	Dim sqw As Double = q.W * q.W

	'	' If quaternion is normalised the unit is one, otherwise it is the correction factor
	'	Dim unit As Double = sqx + sqy + sqz + sqw
	'	If unit = 1 Then
	'		Dim i As Integer = 42
	'	ElseIf unit = -1 Then
	'		Dim i As Integer = 42
	'	Else
	'		Dim i As Integer = 42
	'	End If

	'End Sub

	Protected Function ReadMdlIkRules(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49) As Long
		If anAnimationDesc.ikRuleCount > 0 Then
			Dim ikRuleInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			If Me.theMdlFileData.version >= 48 AndAlso anAnimationDesc.animBlock > 0 AndAlso anAnimationDesc.animblockIkRuleOffset = 0 Then
				'Return 0
			Else
				Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.ikRuleOffset, SeekOrigin.Begin)
			End If
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			anAnimationDesc.theIkRules = New List(Of SourceMdlIkRule)(anAnimationDesc.ikRuleCount)
			For ikRuleIndex As Integer = 0 To anAnimationDesc.ikRuleCount - 1
				ikRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anIkRule As New SourceMdlIkRule()

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

				anIkRule.compressedIkErrorOffset = Me.theInputFileReader.ReadInt32()
				anIkRule.unused2 = Me.theInputFileReader.ReadInt32()
				anIkRule.ikErrorIndexStart = Me.theInputFileReader.ReadInt32()
				anIkRule.ikErrorOffset = Me.theInputFileReader.ReadInt32()

				anIkRule.influenceStart = Me.theInputFileReader.ReadSingle()
				anIkRule.influencePeak = Me.theInputFileReader.ReadSingle()
				anIkRule.influenceTail = Me.theInputFileReader.ReadSingle()
				anIkRule.influenceEnd = Me.theInputFileReader.ReadSingle()

				anIkRule.unused3 = Me.theInputFileReader.ReadSingle()
				anIkRule.contact = Me.theInputFileReader.ReadSingle()
				anIkRule.drop = Me.theInputFileReader.ReadSingle()
				anIkRule.top = Me.theInputFileReader.ReadSingle()

				anIkRule.unused6 = Me.theInputFileReader.ReadInt32()
				anIkRule.unused7 = Me.theInputFileReader.ReadInt32()
				anIkRule.unused8 = Me.theInputFileReader.ReadInt32()

				anIkRule.attachmentNameOffset = Me.theInputFileReader.ReadInt32()

				For x As Integer = 0 To anIkRule.unused.Length - 1
					anIkRule.unused(x) = Me.theInputFileReader.ReadInt32()
				Next

				anAnimationDesc.theIkRules.Add(anIkRule)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If anIkRule.attachmentNameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.attachmentNameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					anIkRule.theAttachmentName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkRule.theAttachmentName = " + anIkRule.theAttachmentName)
					'End If
				Else
					anIkRule.theAttachmentName = ""
				End If

				If anIkRule.compressedIkErrorOffset <> 0 Then
					Dim compressedIkErrorsEndOffset As Long

					compressedIkErrorsEndOffset = Me.ReadCompressedIkErrors(ikRuleInputFileStreamPosition, ikRuleIndex, anAnimationDesc)

					'If fileOffsetEndOfIkRuleExtraData < compressedIkErrorsEndOffset Then
					'	fileOffsetEndOfIkRuleExtraData = compressedIkErrorsEndOffset
					'End If
				End If

				If anIkRule.ikErrorOffset <> 0 Then
					Dim debug As Integer = 4242
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Dim description As String
			description = "anAnimationDesc.theIkRules " + anAnimationDesc.theIkRules.Count.ToString()
			If anAnimationDesc.animBlock > 0 AndAlso anAnimationDesc.animblockIkRuleOffset = 0 Then
				description += "   [animblockIkRuleOffset = 0]"
			End If
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, description)

			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theIkRules alignment")

			'	Return Me.theInputFileReader.BaseStream.Position - 1
		End If

		Return 0
	End Function

	Private Function ReadCompressedIkErrors(ByVal ikRuleInputFileStreamPosition As Long, ByVal ikRuleIndex As Integer, ByVal anAnimationDesc As SourceMdlAnimationDesc49) As Long
		Dim anIkRule As SourceMdlIkRule
		anIkRule = anAnimationDesc.theIkRules(ikRuleIndex)

		Dim compressedIkErrorInputFileStreamPosition As Long
		Dim kInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.compressedIkErrorOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position
		compressedIkErrorInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		anIkRule.theCompressedIkError = New SourceMdlCompressedIkError()

		' First, read the scale data.
		For k As Integer = 0 To anIkRule.theCompressedIkError.scale.Length - 1
			kInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			anIkRule.theCompressedIkError.scale(k) = Me.theInputFileReader.ReadSingle()

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(kInputFileStreamPosition, fileOffsetEnd, "anIkRule.theCompressedIkError [ikRuleIndex = " + ikRuleIndex.ToString() + "] [scale = " + anIkRule.theCompressedIkError.scale(k).ToString() + "]")
		Next

		' Second, read the offset data.
		For k As Integer = 0 To anIkRule.theCompressedIkError.offset.Length - 1
			kInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			anIkRule.theCompressedIkError.offset(k) = Me.theInputFileReader.ReadInt16()

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(kInputFileStreamPosition, fileOffsetEnd, "anIkRule.theCompressedIkError [ikRuleIndex = " + ikRuleIndex.ToString() + "] [offset = " + anIkRule.theCompressedIkError.offset(k).ToString() + "]")
		Next

		'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkRule.theCompressedIkError (scale and offset data)")

		'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		'TODO:  Third, read the anim values. This code does not correctly handle versions below 48.
		If Me.theMdlFileData.version >= 48 Then
			For k As Integer = 0 To anIkRule.theCompressedIkError.scale.Length - 1
				'compressedIkErrorInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				' Read the mstudioanimvalue_t.
				'int size = srcanim->ikrule[j].errorData.numanim[k] * sizeof( mstudioanimvalue_t );
				'memmove( pData, srcanim->ikrule[j].errorData.anim[k], size );
				'TODO: Figure out what frameCount should be.
				If anIkRule.theCompressedIkError.offset(k) > 0 Then
					'Dim frameCount As Integer = 1
					anIkRule.theCompressedIkError.theAnimValues(k) = New List(Of SourceMdlAnimationValue)()
					Me.ReadMdlAnimValues(compressedIkErrorInputFileStreamPosition + anIkRule.theCompressedIkError.offset(k), anAnimationDesc.frameCount, True, anIkRule.theCompressedIkError.theAnimValues(k), "anIkRule.theCompressedIkError.theAnimValues(" + k.ToString() + ")")
				End If
			Next
		End If

		'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

		'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkRule.theCompressedIkError ")

		Return Me.theInputFileReader.BaseStream.Position - 1
	End Function

	Protected Sub ReadMdlAnimationSection(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49, ByVal aFileSeekLog As FileSeekLog)
		'Dim animSectionInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			fileOffsetStart = animInputFileStreamPosition

			'animSectionInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			Dim anAnimSection As New SourceMdlAnimationSection()
			anAnimSection.animBlock = Me.theInputFileReader.ReadInt32()
			anAnimSection.animOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.theSections.Add(anAnimSection)

			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			'aFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimSection [" + anAnimationDesc.theName + "] [animBlock = " + anAnimSection.animBlock.ToString() + "] [calculated ANI file Offset = " + (Me.theMdlFileData.theAnimBlocks(anAnimSection.animBlock).dataStart + anAnimSection.animOffset).ToString() + "]")
			If Me.theMdlFileData.theAnimBlocks Is Nothing Then
				aFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimSection [" + anAnimationDesc.theName + "] [animBlock = " + anAnimSection.animBlock.ToString() + "]")
			Else
				Dim animBlock As Integer
				Dim description As String
				animBlock = anAnimSection.animBlock
				If animBlock = 0 Then
					description = "MDL"
				Else
					description = "ANI"
				End If
				aFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimSection [" + anAnimationDesc.theName + "] [animBlock = " + animBlock.ToString() + "] [calculated " + description + " file Offset = " + (Me.theMdlFileData.theAnimBlocks(animBlock).dataStart + anAnimSection.animOffset).ToString() + "]")
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Protected Sub ReadMdlMovements(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49)
		If anAnimationDesc.movementCount > 0 Then
			Dim movementInputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

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
		End If
	End Sub

	Protected Function ReadLocalHierarchies(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49) As Long
		If anAnimationDesc.localHierarchyCount > 0 Then
			Dim localHieararchyInputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.localHierarchyOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			anAnimationDesc.theLocalHierarchies = New List(Of SourceMdlLocalHierarchy)(anAnimationDesc.localHierarchyCount)
			For j As Integer = 0 To anAnimationDesc.localHierarchyCount - 1
				localHieararchyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aLocalHierarchy As New SourceMdlLocalHierarchy()

				aLocalHierarchy.boneIndex = Me.theInputFileReader.ReadInt32()
				aLocalHierarchy.boneNewParentIndex = Me.theInputFileReader.ReadInt32()
				aLocalHierarchy.startInfluence = Me.theInputFileReader.ReadSingle()
				aLocalHierarchy.peakInfluence = Me.theInputFileReader.ReadSingle()
				aLocalHierarchy.tailInfluence = Me.theInputFileReader.ReadSingle()
				aLocalHierarchy.endInfluence = Me.theInputFileReader.ReadSingle()
				aLocalHierarchy.startFrameIndex = Me.theInputFileReader.ReadInt32()
				aLocalHierarchy.localAnimOffset = Me.theInputFileReader.ReadInt32()
				For x As Integer = 0 To aLocalHierarchy.unused.Length - 1
					aLocalHierarchy.unused(x) = Me.theInputFileReader.ReadInt32()
				Next

				anAnimationDesc.theLocalHierarchies.Add(aLocalHierarchy)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theLocalHierarchies " + anAnimationDesc.theLocalHierarchies.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theLocalHierarchies alignment")

			Return Me.theInputFileReader.BaseStream.Position - 1
		End If
	End Function

	Public Sub ReadSequenceDescs()
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

				Me.theMdlFileData.theSequenceDescs = New List(Of SourceMdlSequenceDesc)(Me.theMdlFileData.localSequenceCount)
				For i As Integer = 0 To Me.theMdlFileData.localSequenceCount - 1
					seqInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aSeqDesc As New SourceMdlSequenceDesc()

					aSeqDesc.baseHeaderOffset = Me.theInputFileReader.ReadInt32()
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
					aSeqDesc.animIndexOffset = Me.theInputFileReader.ReadInt32()
					aSeqDesc.movementIndex = Me.theInputFileReader.ReadInt32()
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

					aSeqDesc.localEntryNodeIndex = Me.theInputFileReader.ReadInt32()
					aSeqDesc.localExitNodeIndex = Me.theInputFileReader.ReadInt32()
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
					aSeqDesc.cyclePoseIndex = Me.theInputFileReader.ReadInt32()

					aSeqDesc.activityModifierOffset = 0
					aSeqDesc.activityModifierCount = 0
					If Me.theMdlFileData.version = 48 OrElse Me.theMdlFileData.version = 49 Then
						aSeqDesc.activityModifierOffset = Me.theInputFileReader.ReadInt32()
						aSeqDesc.activityModifierCount = Me.theInputFileReader.ReadInt32()
						For x As Integer = 0 To 4
							aSeqDesc.unused(x) = Me.theInputFileReader.ReadInt32()
						Next
					Else
						For x As Integer = 0 To 6
							aSeqDesc.unused(x) = Me.theInputFileReader.ReadInt32()
						Next
					End If

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
						'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theActivityName = " + aSeqDesc.theActivityName)
						'End If
					Else
						aSeqDesc.theActivityName = ""
					End If
					If (aSeqDesc.groupSize(0) > 1 OrElse aSeqDesc.groupSize(1) > 1) AndAlso aSeqDesc.poseKeyOffset <> 0 Then
						Me.ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc)
					End If
					If aSeqDesc.eventCount > 0 AndAlso aSeqDesc.eventOffset <> 0 Then
						Me.ReadEvents(seqInputFileStreamPosition, aSeqDesc)
					End If
					If aSeqDesc.autoLayerCount > 0 AndAlso aSeqDesc.autoLayerOffset <> 0 Then
						Me.ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc)
					End If
					If Me.theMdlFileData.boneCount > 0 AndAlso aSeqDesc.weightOffset > 0 Then
						Me.ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc)
					End If
					If aSeqDesc.ikLockCount > 0 AndAlso aSeqDesc.ikLockOffset <> 0 Then
						Me.ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc)
					End If
					If (aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)) > 0 AndAlso aSeqDesc.animIndexOffset <> 0 Then
						Me.ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc)
					End If
					If aSeqDesc.keyValueSize > 0 AndAlso aSeqDesc.keyValueOffset <> 0 Then
						Me.ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc)
					End If
					If aSeqDesc.activityModifierCount <> 0 AndAlso aSeqDesc.activityModifierOffset <> 0 Then
						Me.ReadActivityModifiers(seqInputFileStreamPosition, aSeqDesc)
					End If

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceDescs " + Me.theMdlFileData.theSequenceDescs.Count.ToString())
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadPoseKeys(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
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
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.thePoseKeys [" + aSeqDesc.theName + "] " + aSeqDesc.thePoseKeys.Count.ToString())
	End Sub

	Private Sub ReadEvents(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim eventCount As Integer
		eventCount = aSeqDesc.eventCount
		Dim eventInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theEvents = New List(Of SourceMdlEvent)(eventCount)
		For j As Integer = 0 To eventCount - 1
			eventInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anEvent As New SourceMdlEvent()
			anEvent.cycle = Me.theInputFileReader.ReadSingle()
			anEvent.eventIndex = Me.theInputFileReader.ReadInt32()
			anEvent.eventType = Me.theInputFileReader.ReadInt32()
			For x As Integer = 0 To anEvent.options.Length - 1
				anEvent.options(x) = Me.theInputFileReader.ReadChar()
			Next
			anEvent.nameOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.theEvents.Add(anEvent)

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'if ( isdigit( g_sequence[i].event[j].eventname[0] ) )
			'{
			'	 pevent[j].event = atoi( g_sequence[i].event[j].eventname );
			'	 pevent[j].type = 0;
			'	 pevent[j].szeventindex = 0;
			'}
			'Else
			'{
			'	 AddToStringTable( &pevent[j], &pevent[j].szeventindex, g_sequence[i].event[j].eventname );
			'	 pevent[j].type = NEW_EVENT_STYLE;
			'}
			If anEvent.nameOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(eventInputFileStreamPosition + anEvent.nameOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

				anEvent.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEvent.theName = " + anEvent.theName)
				'End If
			Else
				'anEvent.theName = ""
				anEvent.theName = anEvent.eventIndex.ToString()
			End If

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theEvents [" + aSeqDesc.theName + "] " + aSeqDesc.theEvents.Count.ToString())
	End Sub

	Private Sub ReadAutoLayers(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim autoLayerCount As Integer
		autoLayerCount = aSeqDesc.autoLayerCount
		Dim autoLayerInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.autoLayerOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theAutoLayers = New List(Of SourceMdlAutoLayer)(autoLayerCount)
		For j As Integer = 0 To autoLayerCount - 1
			autoLayerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anAutoLayer As New SourceMdlAutoLayer()
			anAutoLayer.sequenceIndex = Me.theInputFileReader.ReadInt16()
			anAutoLayer.poseIndex = Me.theInputFileReader.ReadInt16()
			anAutoLayer.flags = Me.theInputFileReader.ReadInt32()
			anAutoLayer.influenceStart = Me.theInputFileReader.ReadSingle()
			anAutoLayer.influencePeak = Me.theInputFileReader.ReadSingle()
			anAutoLayer.influenceTail = Me.theInputFileReader.ReadSingle()
			anAutoLayer.influenceEnd = Me.theInputFileReader.ReadSingle()
			aSeqDesc.theAutoLayers.Add(anAutoLayer)

			'NOTE: Change NaN to 0. This is needed for HL2DM\HL2\hl2_misc_dir.vpk\models\combine_soldier_anims.mdl for its "Man_Gun" $sequence.
			If Double.IsNaN(anAutoLayer.influenceStart) Then
				anAutoLayer.influenceStart = 0
			End If
			If Double.IsNaN(anAutoLayer.influencePeak) Then
				anAutoLayer.influencePeak = 0
			End If
			If Double.IsNaN(anAutoLayer.influenceTail) Then
				anAutoLayer.influenceTail = 0
			End If
			If Double.IsNaN(anAutoLayer.influenceEnd) Then
				anAutoLayer.influenceEnd = 0
			End If

			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAutoLayers [" + aSeqDesc.theName + "] " + aSeqDesc.theAutoLayers.Count.ToString())
	End Sub

	Private Sub ReadMdlAnimBoneWeights(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
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
		'NOTE: A sequence can point to same weights as another.
		'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theBoneWeights [" + aSeqDesc.theName + "] " + aSeqDesc.theBoneWeights.Count.ToString())
		'End If
	End Sub

	Private Sub ReadSequenceIkLocks(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim lockCount As Integer
		lockCount = aSeqDesc.ikLockCount
		Dim lockInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.ikLockOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theIkLocks = New List(Of SourceMdlIkLock)(lockCount)
		For j As Integer = 0 To lockCount - 1
			lockInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anIkLock As New SourceMdlIkLock()
			anIkLock.chainIndex = Me.theInputFileReader.ReadInt32()
			anIkLock.posWeight = Me.theInputFileReader.ReadSingle()
			anIkLock.localQWeight = Me.theInputFileReader.ReadSingle()
			anIkLock.flags = Me.theInputFileReader.ReadInt32()
			For x As Integer = 0 To anIkLock.unused.Length - 1
				anIkLock.unused(x) = Me.theInputFileReader.ReadInt32()
			Next
			aSeqDesc.theIkLocks.Add(anIkLock)

			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theIkLocks [" + aSeqDesc.theName + "] " + aSeqDesc.theIkLocks.Count.ToString())
	End Sub

	Private Sub ReadMdlAnimIndexes(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim animIndexCount As Integer
		animIndexCount = aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)
		Dim animIndexInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.animIndexOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theAnimDescIndexes = New List(Of Short)(animIndexCount)
		For j As Integer = 0 To animIndexCount - 1
			animIndexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anAnimIndex As Short
			anAnimIndex = Me.theInputFileReader.ReadInt16()
			aSeqDesc.theAnimDescIndexes.Add(anAnimIndex)

			If Me.theMdlFileData.theAnimationDescs IsNot Nothing AndAlso Me.theMdlFileData.theAnimationDescs.Count > anAnimIndex Then
				'NOTE: Set this boolean for use in writing lines in qc file.
				Me.theMdlFileData.theAnimationDescs(anAnimIndex).theAnimIsLinkedToSequence = True
				Me.theMdlFileData.theAnimationDescs(anAnimIndex).theLinkedSequences.Add(aSeqDesc)
			End If

			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		'TODO: A sequence can point to same anims as another?
		'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAnimDescIndexes [" + aSeqDesc.theName + "] " + aSeqDesc.theAnimDescIndexes.Count.ToString())
		'End If
	End Sub

	Private Sub ReadSequenceKeyValues(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.keyValueOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theKeyValues = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theKeyValues [" + aSeqDesc.theName + "] = " + aSeqDesc.theKeyValues)
	End Sub

	Private Sub ReadActivityModifiers(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim activityModifierCount As Integer
		Dim activityModifierInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.activityModifierOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		activityModifierCount = aSeqDesc.activityModifierCount
		aSeqDesc.theActivityModifiers = New List(Of SourceMdlActivityModifier)(activityModifierCount)
		For j As Integer = 0 To activityModifierCount - 1
			activityModifierInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anActivityModifier As New SourceMdlActivityModifier()
			anActivityModifier.nameOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.theActivityModifiers.Add(anActivityModifier)

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			If anActivityModifier.nameOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(activityModifierInputFileStreamPosition + anActivityModifier.nameOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

				anActivityModifier.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anActivityModifier.theName = " + anActivityModifier.theName)
				'End If
			Else
				anActivityModifier.theName = ""
			End If

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theActivityModifiers [" + aSeqDesc.theName + "] " + aSeqDesc.theActivityModifiers.Count.ToString())
	End Sub

	Public Sub ReadLocalNodeNames()
		'	// save transition graph
		'	int *pxnodename = (int *)pData;
		'	phdr->localnodenameindex = (pData - pStart);
		'	pData += g_numxnodes * sizeof( *pxnodename );
		'	ALIGN4( pData );
		'	for (i = 0; i < g_numxnodes; i++)
		'	{
		'		AddToStringTable( phdr, pxnodename, g_xnodename[i+1] );
		'		// printf("%d : %s\n", i, g_xnodename[i+1] );
		'		pxnodename++;
		'	}
		If Me.theMdlFileData.localNodeCount > 0 AndAlso Me.theMdlFileData.localNodeNameOffset <> 0 Then
			Dim localNodeNameInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localNodeNameOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theLocalNodeNames = New List(Of String)(Me.theMdlFileData.localNodeCount)
			Dim localNodeNameOffset As Integer
			For i As Integer = 0 To Me.theMdlFileData.localNodeCount - 1
				localNodeNameInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aLocalNodeName As String
				localNodeNameOffset = Me.theInputFileReader.ReadInt32()

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If localNodeNameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(localNodeNameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aLocalNodeName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aLocalNodeName = " + aLocalNodeName)
					'End If
				Else
					aLocalNodeName = ""
				End If
				Me.theMdlFileData.theLocalNodeNames.Add(aLocalNodeName)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theLocalNodeNames " + Me.theMdlFileData.theLocalNodeNames.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theLocalNodeNames alignment")
		End If
	End Sub

	Public Sub ReadLocalNodes()
		'	ptransition	= (byte *)pData;
		'	phdr->numlocalnodes = IsChar( g_numxnodes );
		'	phdr->localnodeindex = IsInt24( pData - pStart );
		'	pData += g_numxnodes * g_numxnodes * sizeof( byte );
		'	ALIGN4( pData );
		'	for (i = 0; i < g_numxnodes; i++)
		'	{
		'//		printf("%2d (%12s) : ", i + 1, g_xnodename[i+1] );
		'		for (j = 0; j < g_numxnodes; j++)
		'		{
		'			*ptransition++ = g_xnode[i][j];
		'//			printf(" %2d", g_xnode[i][j] );
		'		}
		'//		printf("\n" );
		'	}
		If Me.theMdlFileData.localNodeCount > 0 AndAlso Me.theMdlFileData.localNodeOffset <> 0 Then
			'Dim localNodeInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localNodeOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theLocalNodes = New List(Of List(Of Byte))(Me.theMdlFileData.localNodeCount)
			For i As Integer = 0 To Me.theMdlFileData.localNodeCount - 1
				Dim exitNodes As New List(Of Byte)(Me.theMdlFileData.localNodeCount)

				For j As Integer = 0 To Me.theMdlFileData.localNodeCount - 1
					Dim nodeValue As Byte
					nodeValue = Me.theInputFileReader.ReadByte()
					exitNodes.Add(nodeValue)
				Next

				Me.theMdlFileData.theLocalNodes.Add(exitNodes)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theLocalNodes " + Me.theMdlFileData.theLocalNodes.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theLocalNodes alignment")
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

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.bodyPartOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theBodyParts = New List(Of SourceMdlBodyPart)(Me.theMdlFileData.bodyPartCount)
			For bodyPartIndex As Integer = 0 To Me.theMdlFileData.bodyPartCount - 1
				bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aBodyPart As New SourceMdlBodyPart()
				aBodyPart.nameOffset = Me.theInputFileReader.ReadInt32()
				aBodyPart.modelCount = Me.theInputFileReader.ReadInt32()
				aBodyPart.base = Me.theInputFileReader.ReadInt32()
				aBodyPart.modelOffset = Me.theInputFileReader.ReadInt32()
				Me.theMdlFileData.theBodyParts.Add(aBodyPart)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aBodyPart.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aBodyPart.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBodyPart.theName = " + aBodyPart.theName)
					'End If
				Else
					aBodyPart.theName = ""
				End If

				Me.ReadModels(bodyPartInputFileStreamPosition, aBodyPart, bodyPartIndex)
				'NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts and models.
				If bodyPartIndex = Me.theMdlFileData.bodyPartCount - 1 Then
					Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, Me.theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment")
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts " + Me.theMdlFileData.theBodyParts.Count.ToString())
		End If
	End Sub

	Private Sub ReadModels(ByVal bodyPartInputFileStreamPosition As Long, ByVal aBodyPart As SourceMdlBodyPart, ByVal bodyPartIndex As Integer)
		If aBodyPart.modelCount > 0 Then
			Dim modelInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long
			Dim modelName As String

			Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin)
			'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aBodyPart.theModels = New List(Of SourceMdlModel)(aBodyPart.modelCount)
			For j As Integer = 0 To aBodyPart.modelCount - 1
				modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Dim aModel As New SourceMdlModel()

				aModel.name = Me.theInputFileReader.ReadChars(64)
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
				Dim modelVertexData As New SourceMdlModelVertexData()
				modelVertexData.vertexDataP = Me.theInputFileReader.ReadInt32()
				modelVertexData.tangentDataP = Me.theInputFileReader.ReadInt32()
				aModel.vertexData = modelVertexData
				For x As Integer = 0 To 7
					aModel.unused(x) = Me.theInputFileReader.ReadInt32()
				Next

				aBodyPart.theModels.Add(aModel)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
				Me.ReadEyeballs(modelInputFileStreamPosition, aModel)
				Me.ReadMeshes(modelInputFileStreamPosition, aModel)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

				modelName = CStr(aModel.name).Trim(Chr(0))
				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel Name = " + modelName)
			Next

			'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels " + aBodyPart.theModels.Count.ToString())
		End If
	End Sub

	Private Sub ReadMeshes(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel)
		If aModel.meshCount > 0 AndAlso aModel.meshOffset <> 0 Then
			aModel.theMeshes = New List(Of SourceMdlMesh)(aModel.meshCount)
			Dim meshInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.meshOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			For meshIndex As Integer = 0 To aModel.meshCount - 1
				meshInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aMesh As New SourceMdlMesh()

				aMesh.materialIndex = Me.theInputFileReader.ReadInt32()
				aMesh.modelOffset = Me.theInputFileReader.ReadInt32()
				aMesh.vertexCount = Me.theInputFileReader.ReadInt32()
				aMesh.vertexIndexStart = Me.theInputFileReader.ReadInt32()
				aMesh.flexCount = Me.theInputFileReader.ReadInt32()
				aMesh.flexOffset = Me.theInputFileReader.ReadInt32()
				aMesh.materialType = Me.theInputFileReader.ReadInt32()
				aMesh.materialParam = Me.theInputFileReader.ReadInt32()
				aMesh.id = Me.theInputFileReader.ReadInt32()
				aMesh.centerX = Me.theInputFileReader.ReadSingle()
				aMesh.centerY = Me.theInputFileReader.ReadSingle()
				aMesh.centerZ = Me.theInputFileReader.ReadSingle()
				Dim meshVertexData As New SourceMdlMeshVertexData()
				meshVertexData.modelVertexDataP = Me.theInputFileReader.ReadInt32()
				For x As Integer = 0 To MAX_NUM_LODS - 1
					meshVertexData.lodVertexCount(x) = Me.theInputFileReader.ReadInt32()
				Next
				aMesh.vertexData = meshVertexData
				For x As Integer = 0 To 7
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
		End If
	End Sub

	Private Sub ReadEyeballs(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel)
		If aModel.eyeballCount > 0 AndAlso aModel.eyeballOffset <> 0 Then
			aModel.theEyeballs = New List(Of SourceMdlEyeball)(aModel.eyeballCount)
			Dim eyeballInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.eyeballOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			For k As Integer = 0 To aModel.eyeballCount - 1
				eyeballInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anEyeball As New SourceMdlEyeball()

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

				anEyeball.unused1 = Me.theInputFileReader.ReadInt32()
				anEyeball.irisScale = Me.theInputFileReader.ReadSingle()
				anEyeball.unused2 = Me.theInputFileReader.ReadInt32()

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

				For x As Integer = 0 To anEyeball.unused.Length - 1
					anEyeball.unused(x) = Me.theInputFileReader.ReadInt32()
				Next

				anEyeball.eyeballIsNonFacs = Me.theInputFileReader.ReadByte()

				For x As Integer = 0 To anEyeball.unused3.Length - 1
					anEyeball.unused3(x) = Me.theInputFileReader.ReadChar()
				Next
				For x As Integer = 0 To anEyeball.unused4.Length - 1
					anEyeball.unused4(x) = Me.theInputFileReader.ReadInt32()
				Next

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
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEyeball.theName = " + anEyeball.theName)
					'End If
				Else
					anEyeball.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			If aModel.theEyeballs.Count > 0 Then
				' Detect if $upaxis Y was used.
				'FROM: [48] SourceEngine2007_source se2007_src\src_main\utils\studiomdl\studiomdl.cpp
				'      Option_Eyeball()
				'	AngleMatrix( g_defaultrotation, vtmp );
				'	VectorIRotate( Vector( 0, 0, 1 ), vtmp, tmp );
				'	VectorIRotate( tmp, pmodel->source->boneToPose[eyeball->bone], eyeball->up );
				Dim anEyeball As SourceMdlEyeball
				Dim aBone As SourceMdlBone
				'Dim upVec As New SourceVector(0, 0, 1)
				Dim tmp As SourceVector
				anEyeball = aModel.theEyeballs(0)
				aBone = Me.theMdlFileData.theBones(anEyeball.boneIndex)
				tmp = MathModule.VectorIRotate(aModel.theEyeballs(0).up, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				If tmp.y > 0.99 AndAlso tmp.y < 1.01 Then
					Me.theMdlFileData.theUpAxisYCommandWasUsed = True
				End If
			End If

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theEyeballs " + aModel.theEyeballs.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theEyeballs alignment")
		End If
	End Sub

	Private Sub ReadFlexes(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceMdlMesh)
		aMesh.theFlexes = New List(Of SourceMdlFlex)(aMesh.flexCount)
		Dim flexInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.flexOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		For k As Integer = 0 To aMesh.flexCount - 1
			flexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim aFlex As New SourceMdlFlex()

			aFlex.flexDescIndex = Me.theInputFileReader.ReadInt32()

			aFlex.target0 = Me.theInputFileReader.ReadSingle()
			aFlex.target1 = Me.theInputFileReader.ReadSingle()
			aFlex.target2 = Me.theInputFileReader.ReadSingle()
			aFlex.target3 = Me.theInputFileReader.ReadSingle()

			aFlex.vertCount = Me.theInputFileReader.ReadInt32()
			aFlex.vertOffset = Me.theInputFileReader.ReadInt32()

			aFlex.flexDescPartnerIndex = Me.theInputFileReader.ReadInt32()
			aFlex.vertAnimType = Me.theInputFileReader.ReadByte()
			For x As Integer = 0 To aFlex.unusedChar.Length - 1
				aFlex.unusedChar(x) = Me.theInputFileReader.ReadChar()
			Next
			For x As Integer = 0 To aFlex.unused.Length - 1
				aFlex.unused(x) = Me.theInputFileReader.ReadInt32()
			Next
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
	End Sub

	Private Sub ReadVertAnims(ByVal flexInputFileStreamPosition As Long, ByVal aFlex As SourceMdlFlex)
		aFlex.theVertAnims = New List(Of SourceMdlVertAnim)(aFlex.vertCount)
		Dim eyeballInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Me.theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Dim aVertAnim As SourceMdlVertAnim
		For k As Integer = 0 To aFlex.vertCount - 1
			eyeballInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			If aFlex.vertAnimType = aFlex.STUDIO_VERT_ANIM_WRINKLE Then
				aVertAnim = New SourceMdlVertAnimWrinkle()
			Else
				aVertAnim = New SourceMdlVertAnim()
			End If

			aVertAnim.index = Me.theInputFileReader.ReadUInt16()
			aVertAnim.speed = Me.theInputFileReader.ReadByte()
			aVertAnim.side = Me.theInputFileReader.ReadByte()

			For x As Integer = 0 To 2
				aVertAnim.deltaUShort(x) = Me.theInputFileReader.ReadUInt16()
			Next
			For x As Integer = 0 To 2
				aVertAnim.nDeltaUShort(x) = Me.theInputFileReader.ReadUInt16()
			Next

			If aFlex.vertAnimType = aFlex.STUDIO_VERT_ANIM_WRINKLE Then
				CType(aVertAnim, SourceMdlVertAnimWrinkle).wrinkleDelta = Me.theInputFileReader.ReadInt16()
			End If

			aFlex.theVertAnims.Add(aVertAnim)

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		'aFlex.theVertAnims.Sort(AddressOf Me.SortVertAnims)

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims " + aFlex.theVertAnims.Count.ToString())

		Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aFlex.theVertAnims alignment")
	End Sub

	Private Function SortVertAnims(ByVal x As SourceMdlVertAnim, ByVal y As SourceMdlVertAnim) As Integer
		Return x.index.CompareTo(y.index)
	End Function

	Public Sub ReadFlexDescs()
		If Me.theMdlFileData.flexDescCount > 0 Then
			Dim flexDescInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

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
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexDesc.theName = " + aFlexDesc.theName)
					'End If
				Else
					aFlexDesc.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs " + Me.theMdlFileData.theFlexDescs.Count.ToString())
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
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theType = " + aFlexController.theType)
					'End If
				Else
					aFlexController.theType = ""
				End If
				If aFlexController.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aFlexController.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theName = " + aFlexController.theName)
					'End If
				Else
					aFlexController.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			'If Me.theMdlFileData.theFlexControllers.Count > 0 Then
			'	Me.theMdlFileData.theModelCommandIsUsed = True
			'End If

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllers " + Me.theMdlFileData.theFlexControllers.Count.ToString())
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

			'If Me.theMdlFileData.theFlexRules.Count > 0 Then
			'	Me.theMdlFileData.theModelCommandIsUsed = True
			'End If

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules " + Me.theMdlFileData.theFlexRules.Count.ToString())
		End If
	End Sub

	Private Sub ReadFlexOps(ByVal flexRuleInputFileStreamPosition As Long, ByVal aFlexRule As SourceMdlFlexRule)
		'Dim flexRuleInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

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
	End Sub

	Public Sub ReadIkChains()
		If Me.theMdlFileData.ikChainCount > 0 Then
			Dim ikChainInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.ikChainOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theIkChains = New List(Of SourceMdlIkChain)(Me.theMdlFileData.ikChainCount)
			For i As Integer = 0 To Me.theMdlFileData.ikChainCount - 1
				ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anIkChain As New SourceMdlIkChain()
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
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkChain.theName = " + anIkChain.theName)
					'End If
				Else
					anIkChain.theName = ""
				End If
				Me.ReadIkLinks(ikChainInputFileStreamPosition, anIkChain)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkChains " + Me.theMdlFileData.theIkChains.Count.ToString())
		End If
	End Sub

	Private Sub ReadIkLinks(ByVal ikChainInputFileStreamPosition As Long, ByVal anIkChain As SourceMdlIkChain)
		If anIkChain.linkCount > 0 Then
			'Dim ikLinkInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.linkOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			anIkChain.theLinks = New List(Of SourceMdlIkLink)(anIkChain.linkCount)
			For j As Integer = 0 To anIkChain.linkCount - 1
				'ikLinkInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anIkLink As New SourceMdlIkLink()
				anIkLink.boneIndex = Me.theInputFileReader.ReadInt32()
				anIkLink.idealBendingDirection.x = Me.theInputFileReader.ReadSingle()
				anIkLink.idealBendingDirection.y = Me.theInputFileReader.ReadSingle()
				anIkLink.idealBendingDirection.z = Me.theInputFileReader.ReadSingle()
				anIkLink.unused0.x = Me.theInputFileReader.ReadSingle()
				anIkLink.unused0.y = Me.theInputFileReader.ReadSingle()
				anIkLink.unused0.z = Me.theInputFileReader.ReadSingle()
				anIkChain.theLinks.Add(anIkLink)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkChain.theLinks " + anIkChain.theLinks.Count.ToString())
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

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localIkAutoPlayLockOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theIkLocks = New List(Of SourceMdlIkLock)(Me.theMdlFileData.localIkAutoPlayLockCount)
			For i As Integer = 0 To Me.theMdlFileData.localIkAutoPlayLockCount - 1
				'ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anIkLock As New SourceMdlIkLock()
				anIkLock.chainIndex = Me.theInputFileReader.ReadInt32()
				anIkLock.posWeight = Me.theInputFileReader.ReadSingle()
				anIkLock.localQWeight = Me.theInputFileReader.ReadSingle()
				anIkLock.flags = Me.theInputFileReader.ReadInt32()
				For x As Integer = 0 To anIkLock.unused.Length - 1
					anIkLock.unused(x) = Me.theInputFileReader.ReadInt32()
				Next
				Me.theMdlFileData.theIkLocks.Add(anIkLock)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkLocks " + Me.theMdlFileData.theIkLocks.Count.ToString())
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
				'Me.theMdlFileData.theModelCommandIsUsed = True
				' Seems like any $model can have these lines, so simply assign them to first one.
				Me.theMdlFileData.theBodyParts(0).theModelCommandIsUsed = True
			End If

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theMouths " + Me.theMdlFileData.theMouths.Count.ToString())
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
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aPoseParamDesc.theName = " + aPoseParamDesc.theName)
					'End If
				Else
					aPoseParamDesc.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs " + Me.theMdlFileData.thePoseParamDescs.Count.ToString())
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

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.textureOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theTextures = New List(Of SourceMdlTexture)(Me.theMdlFileData.textureCount)
			For i As Integer = 0 To Me.theMdlFileData.textureCount - 1
				textureInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aTexture As New SourceMdlTexture()
				aTexture.nameOffset = Me.theInputFileReader.ReadInt32()
				aTexture.flags = Me.theInputFileReader.ReadInt32()
				aTexture.used = Me.theInputFileReader.ReadInt32()
				aTexture.unused1 = Me.theInputFileReader.ReadInt32()
				aTexture.materialP = Me.theInputFileReader.ReadInt32()
				aTexture.clientMaterialP = Me.theInputFileReader.ReadInt32()
				For x As Integer = 0 To 9
					aTexture.unused(x) = Me.theInputFileReader.ReadInt32()
				Next
				Me.theMdlFileData.theTextures.Add(aTexture)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aTexture.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aTexture.thePathFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					' Convert all forward slashes to backward slashes.
					aTexture.thePathFileName = FileManager.GetNormalizedPathFileName(aTexture.thePathFileName)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.thePathFileName = " + aTexture.thePathFileName)
					'End If
				Else
					aTexture.thePathFileName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTextures " + Me.theMdlFileData.theTextures.Count.ToString())

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
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath = " + aTexturePath)
					'End If
				Else
					aTexturePath = ""
				End If
				Me.theMdlFileData.theTexturePaths.Add(aTexturePath)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTexturePaths " + Me.theMdlFileData.theTexturePaths.Count.ToString())

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

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.skinFamilyOffset, SeekOrigin.Begin)
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
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies " + Me.theMdlFileData.theSkinFamilies.Count.ToString())

			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment")
		End If
	End Sub

	Public Sub ReadModelGroups()
		If Me.theMdlFileData.includeModelCount > 0 Then
			Dim includeModelInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.includeModelOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theModelGroups = New List(Of SourceMdlModelGroup)(Me.theMdlFileData.includeModelCount)
			For i As Integer = 0 To Me.theMdlFileData.includeModelCount - 1
				includeModelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aModelGroup As New SourceMdlModelGroup()
				aModelGroup.labelOffset = Me.theInputFileReader.ReadInt32()
				aModelGroup.fileNameOffset = Me.theInputFileReader.ReadInt32()
				Me.theMdlFileData.theModelGroups.Add(aModelGroup)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aModelGroup.labelOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + aModelGroup.labelOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aModelGroup.theLabel = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aModelGroup.theLabel = " + aModelGroup.theLabel)
					'End If
				Else
					aModelGroup.theLabel = ""
				End If
				If aModelGroup.fileNameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + aModelGroup.fileNameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aModelGroup.theFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aModelGroup.theFileName = " + aModelGroup.theFileName)
					'End If
				Else
					aModelGroup.theFileName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theModelGroups " + Me.theMdlFileData.theModelGroups.Count.ToString())
		End If
	End Sub

	Public Sub ReadFlexControllerUis()
		If Me.theMdlFileData.flexControllerUiCount > 0 AndAlso Not (Me.theMdlFileData.version = 44 AndAlso Me.theMdlFileData.flexControllerUiOffset = SourceMdlFileData49.text_SCAL_VERSION44Vindictus) Then
			Dim flexControllerUiInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexControllerUiOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theFlexControllerUis = New List(Of SourceMdlFlexControllerUi)(Me.theMdlFileData.flexControllerUiCount)
			For i As Integer = 0 To Me.theMdlFileData.flexControllerUiCount - 1
				flexControllerUiInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aFlexControllerUi As New SourceMdlFlexControllerUi()
				aFlexControllerUi.nameOffset = Me.theInputFileReader.ReadInt32()
				aFlexControllerUi.config0 = Me.theInputFileReader.ReadInt32()
				aFlexControllerUi.config1 = Me.theInputFileReader.ReadInt32()
				aFlexControllerUi.config2 = Me.theInputFileReader.ReadInt32()
				aFlexControllerUi.remapType = Me.theInputFileReader.ReadByte()
				aFlexControllerUi.controlIsStereo = Me.theInputFileReader.ReadByte()
				For x As Integer = 0 To aFlexControllerUi.unused.Length - 1
					aFlexControllerUi.unused(x) = Me.theInputFileReader.ReadByte()
				Next
				Me.theMdlFileData.theFlexControllerUis.Add(aFlexControllerUi)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aFlexControllerUi.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(flexControllerUiInputFileStreamPosition + aFlexControllerUi.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aFlexControllerUi.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexControllerUi.theName = " + aFlexControllerUi.theName)
					'End If
				Else
					aFlexControllerUi.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllerUis " + Me.theMdlFileData.theFlexControllerUis.Count.ToString())
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

	'FROM: SourceEngine2007\src_main\utils\studiomdl\write.cpp
	'static void WriteBoneTransforms( studiohdr2_t *phdr, mstudiobone_t *pBone )
	'{
	'	matrix3x4_t identity;
	'	SetIdentityMatrix( identity );

	'	int nTransformCount = 0;
	'	for (int i = 0; i < g_numbones; i++)
	'	{
	'		if ( g_bonetable[i].flags & BONE_ALWAYS_PROCEDURAL )
	'			continue;
	'		int nParent = g_bonetable[i].parent;

	'		// Transformation is necessary if either you or your parent was realigned
	'		if ( MatricesAreEqual( identity, g_bonetable[i].srcRealign ) &&
	'			 ( ( nParent < 0 ) || MatricesAreEqual( identity, g_bonetable[nParent].srcRealign ) ) )
	'			continue;

	'		++nTransformCount;
	'	}

	'	// save bone transform info
	'	mstudiosrcbonetransform_t *pSrcBoneTransform = (mstudiosrcbonetransform_t *)pData;
	'	phdr->numsrcbonetransform = nTransformCount;
	'	phdr->srcbonetransformindex = pData - pStart;
	'	pData += nTransformCount * sizeof( mstudiosrcbonetransform_t );
	'	int bt = 0;
	'	for ( int i = 0; i < g_numbones; i++ )
	'	{
	'		if ( g_bonetable[i].flags & BONE_ALWAYS_PROCEDURAL )
	'			continue;
	'		int nParent = g_bonetable[i].parent;
	'		if ( MatricesAreEqual( identity, g_bonetable[i].srcRealign ) &&
	'			( ( nParent < 0 ) || MatricesAreEqual( identity, g_bonetable[nParent].srcRealign ) ) )
	'			continue;
	'
	'		// What's going on here?
	'		// So, when we realign a bone, we want to do it in a way so that the child bones
	'		// have the same bone->world transform. If we take T as the src realignment transform
	'		// for the parent, P is the parent to world, and C is the child to parent, we expect 
	'		// the child->world is constant after realignment:
	'		//		CtoW = P * C = ( P * T ) * ( T^-1 * C )
	'		// therefore Cnew = ( T^-1 * C )
	'		if ( nParent >= 0 )
	'		{
	'			MatrixInvert( g_bonetable[nParent].srcRealign, pSrcBoneTransform[bt].pretransform );
	'		}
	'		else
	'		{
	'			SetIdentityMatrix( pSrcBoneTransform[bt].pretransform );
	'		}
	'		MatrixCopy( g_bonetable[i].srcRealign, pSrcBoneTransform[bt].posttransform );
	'		AddToStringTable( &pSrcBoneTransform[bt], &pSrcBoneTransform[bt].sznameindex, g_bonetable[i].name );
	'		++bt;
	'	}
	'	ALIGN4( pData );
	'
	'[second part is in comment before next Sub below]
	'}
	Public Sub ReadBoneTransforms()
		If Me.theMdlFileData.sourceBoneTransformCount > 0 Then
			Dim boneInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.sourceBoneTransformOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theBoneTransforms = New List(Of SourceMdlBoneTransform)(Me.theMdlFileData.sourceBoneTransformCount)
			For i As Integer = 0 To Me.theMdlFileData.sourceBoneTransformCount - 1
				boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aBoneTransform As New SourceMdlBoneTransform()

				aBoneTransform.nameOffset = Me.theInputFileReader.ReadInt32()

				aBoneTransform.preTransformColumn0 = New SourceVector()
				aBoneTransform.preTransformColumn1 = New SourceVector()
				aBoneTransform.preTransformColumn2 = New SourceVector()
				aBoneTransform.preTransformColumn3 = New SourceVector()
				aBoneTransform.preTransformColumn0.x = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn1.x = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn2.x = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn3.x = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn0.y = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn1.y = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn2.y = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn3.y = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn0.z = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn1.z = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn2.z = Me.theInputFileReader.ReadSingle()
				aBoneTransform.preTransformColumn3.z = Me.theInputFileReader.ReadSingle()

				aBoneTransform.postTransformColumn0 = New SourceVector()
				aBoneTransform.postTransformColumn1 = New SourceVector()
				aBoneTransform.postTransformColumn2 = New SourceVector()
				aBoneTransform.postTransformColumn3 = New SourceVector()
				aBoneTransform.postTransformColumn0.x = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn1.x = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn2.x = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn3.x = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn0.y = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn1.y = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn2.y = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn3.y = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn0.z = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn1.z = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn2.z = Me.theInputFileReader.ReadSingle()
				aBoneTransform.postTransformColumn3.z = Me.theInputFileReader.ReadSingle()

				Me.theMdlFileData.theBoneTransforms.Add(aBoneTransform)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aBoneTransform.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBoneTransform.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aBoneTransform.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBoneTransform.theName = " + aBoneTransform.theName)
					'End If
				ElseIf aBoneTransform.theName Is Nothing Then
					aBoneTransform.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneTransforms " + Me.theMdlFileData.theBoneTransforms.Count.ToString())

			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneTransforms alignment")
		End If
	End Sub

	' Part of function in above Sub's comment.
	'	if (g_numbones > 1)
	'	{
	'		// write second bone table
	'		phdr->linearboneindex = pData - (byte *)phdr;
	'		mstudiolinearbone_t *pLinearBone =  (mstudiolinearbone_t *)pData;
	'		pData += sizeof( *pLinearBone );
	'
	'		pLinearBone->numbones = g_numbones;
	'
	'#define WRITE_BONE_BLOCK( type, srcfield, dest, destindex ) \
	'		type *##dest = (type *)pData; \
	'		pLinearBone->##destindex = pData - (byte *)pLinearBone; \
	'		pData += g_numbones * sizeof( *##dest ); \
	'		ALIGN4( pData ); \
	'		for ( int i = 0; i < g_numbones; i++) \
	'			dest##[i] = pBone[i].##srcfield;

	'		WRITE_BONE_BLOCK( int, flags, pFlags, flagsindex );
	'		WRITE_BONE_BLOCK( int, parent, pParent, parentindex );
	'		WRITE_BONE_BLOCK( Vector, pos, pPos, posindex );
	'		WRITE_BONE_BLOCK( Quaternion, quat, pQuat, quatindex );
	'		WRITE_BONE_BLOCK( RadianEuler, rot, pRot, rotindex );
	'		WRITE_BONE_BLOCK( matrix3x4_t, poseToBone, pPoseToBone, posetoboneindex );
	'		WRITE_BONE_BLOCK( Vector, posscale, pPoseScale, posscaleindex );
	'		WRITE_BONE_BLOCK( Vector, rotscale, pRotScale, rotscaleindex );
	'		WRITE_BONE_BLOCK( Quaternion, qAlignment, pQAlignment, qalignmentindex );
	'	}
	Public Sub ReadLinearBoneTable()
		If Me.theMdlFileData.linearBoneOffset > 0 Then
			Try
				Dim boneTableInputFileStreamPosition As Long
				'Dim inputFileStreamPosition As Long
				Dim fileOffsetStart As Long
				Dim fileOffsetEnd As Long
				Dim fileOffsetStart2 As Long
				Dim fileOffsetEnd2 As Long

				'If Me.theMdlFileData.studioHeader2Offset_VERSION48 + Me.theMdlFileData.linearBoneOffset <> Me.theInputFileReader.BaseStream.Position Then
				'	Dim debug As Integer = 4242
				'End If
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.studioHeader2Offset + Me.theMdlFileData.linearBoneOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				boneTableInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				Dim linearBoneTable As SourceMdlLinearBone
				Me.theMdlFileData.theLinearBoneTable = New SourceMdlLinearBone()
				linearBoneTable = Me.theMdlFileData.theLinearBoneTable
				linearBoneTable.boneCount = Me.theInputFileReader.ReadInt32()
				linearBoneTable.flagsOffset = Me.theInputFileReader.ReadInt32()
				linearBoneTable.parentOffset = Me.theInputFileReader.ReadInt32()
				linearBoneTable.posOffset = Me.theInputFileReader.ReadInt32()
				linearBoneTable.quatOffset = Me.theInputFileReader.ReadInt32()
				linearBoneTable.rotOffset = Me.theInputFileReader.ReadInt32()
				linearBoneTable.poseToBoneOffset = Me.theInputFileReader.ReadInt32()
				linearBoneTable.posScaleOffset = Me.theInputFileReader.ReadInt32()
				linearBoneTable.rotScaleOffset = Me.theInputFileReader.ReadInt32()
				linearBoneTable.qAlignmentOffset = Me.theInputFileReader.ReadInt32()
				For x As Integer = 0 To linearBoneTable.unused.Length - 1
					linearBoneTable.unused(x) = Me.theInputFileReader.ReadInt32()
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theLinearBoneTable header")

				Me.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.flagsOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position
				For i As Integer = 0 To linearBoneTable.boneCount - 1
					Dim flags As Integer
					flags = Me.theInputFileReader.ReadInt32()
					linearBoneTable.theFlags.Add(flags)
				Next
				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theFlags")
				'End If
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theFlags alignment")

				If boneTableInputFileStreamPosition + linearBoneTable.parentOffset <> Me.theInputFileReader.BaseStream.Position Then
					Dim debug As Integer = 4242
				End If

				Me.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.parentOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position
				For i As Integer = 0 To linearBoneTable.boneCount - 1
					Dim parent As Integer
					parent = Me.theInputFileReader.ReadInt32()
					linearBoneTable.theParents.Add(parent)
				Next
				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theParents")
				'End If
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theParents alignment")

				If boneTableInputFileStreamPosition + linearBoneTable.posOffset <> Me.theInputFileReader.BaseStream.Position Then
					Dim debug As Integer = 4242
				End If

				Me.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.posOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position
				For i As Integer = 0 To linearBoneTable.boneCount - 1
					Dim position As New SourceVector
					position.x = Me.theInputFileReader.ReadSingle()
					position.y = Me.theInputFileReader.ReadSingle()
					position.z = Me.theInputFileReader.ReadSingle()
					linearBoneTable.thePositions.Add(position)
				Next
				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.thePositions")
				'End If
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.thePositions alignment")

				If boneTableInputFileStreamPosition + linearBoneTable.quatOffset <> Me.theInputFileReader.BaseStream.Position Then
					Dim debug As Integer = 4242
				End If

				Me.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.quatOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position
				For i As Integer = 0 To linearBoneTable.boneCount - 1
					Dim quaternion As New SourceQuaternion
					quaternion.x = Me.theInputFileReader.ReadSingle()
					quaternion.y = Me.theInputFileReader.ReadSingle()
					quaternion.z = Me.theInputFileReader.ReadSingle()
					quaternion.w = Me.theInputFileReader.ReadSingle()
					linearBoneTable.theQuaternions.Add(quaternion)
				Next
				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theQuaternions")
				'End If
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theQuaternions alignment")

				If boneTableInputFileStreamPosition + linearBoneTable.rotOffset <> Me.theInputFileReader.BaseStream.Position Then
					Dim debug As Integer = 4242
				End If

				Me.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.rotOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position
				For i As Integer = 0 To linearBoneTable.boneCount - 1
					Dim rotation As New SourceVector
					rotation.x = Me.theInputFileReader.ReadSingle()
					rotation.y = Me.theInputFileReader.ReadSingle()
					rotation.z = Me.theInputFileReader.ReadSingle()
					linearBoneTable.theRotations.Add(rotation)
				Next
				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theRotations")
				'End If
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theRotations alignment")

				If boneTableInputFileStreamPosition + linearBoneTable.poseToBoneOffset <> Me.theInputFileReader.BaseStream.Position Then
					Dim debug As Integer = 4242
				End If

				Me.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.poseToBoneOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position
				For i As Integer = 0 To linearBoneTable.boneCount - 1
					Dim poseToBoneDataColumn0 As New SourceVector()
					Dim poseToBoneDataColumn1 As New SourceVector()
					Dim poseToBoneDataColumn2 As New SourceVector()
					Dim poseToBoneDataColumn3 As New SourceVector()
					poseToBoneDataColumn0.x = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn1.x = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn2.x = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn3.x = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn0.y = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn1.y = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn2.y = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn3.y = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn0.z = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn1.z = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn2.z = Me.theInputFileReader.ReadSingle()
					poseToBoneDataColumn3.z = Me.theInputFileReader.ReadSingle()
					linearBoneTable.thePoseToBoneDataColumn0s.Add(poseToBoneDataColumn0)
					linearBoneTable.thePoseToBoneDataColumn1s.Add(poseToBoneDataColumn1)
					linearBoneTable.thePoseToBoneDataColumn2s.Add(poseToBoneDataColumn2)
					linearBoneTable.thePoseToBoneDataColumn3s.Add(poseToBoneDataColumn3)
				Next
				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.thePoseToBoneDataColumns")
				'End If
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.thePoseToBoneDataColumns alignment")

				If boneTableInputFileStreamPosition + linearBoneTable.posScaleOffset <> Me.theInputFileReader.BaseStream.Position Then
					Dim debug As Integer = 4242
				End If

				Me.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.posScaleOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position
				For i As Integer = 0 To linearBoneTable.boneCount - 1
					Dim positionScale As New SourceVector
					positionScale.x = Me.theInputFileReader.ReadSingle()
					positionScale.y = Me.theInputFileReader.ReadSingle()
					positionScale.z = Me.theInputFileReader.ReadSingle()
					linearBoneTable.thePositionScales.Add(positionScale)
				Next
				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.thePositionScales")
				'End If
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.thePositionScales alignment")

				If boneTableInputFileStreamPosition + linearBoneTable.rotScaleOffset <> Me.theInputFileReader.BaseStream.Position Then
					Dim debug As Integer = 4242
				End If

				Me.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.rotScaleOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position
				For i As Integer = 0 To linearBoneTable.boneCount - 1
					Dim rotationScale As New SourceVector
					rotationScale.x = Me.theInputFileReader.ReadSingle()
					rotationScale.y = Me.theInputFileReader.ReadSingle()
					rotationScale.z = Me.theInputFileReader.ReadSingle()
					linearBoneTable.theRotationScales.Add(rotationScale)
				Next
				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theRotationScales")
				'End If
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theRotationScales alignment")

				If boneTableInputFileStreamPosition + linearBoneTable.qAlignmentOffset <> Me.theInputFileReader.BaseStream.Position Then
					Dim debug As Integer = 4242
				End If

				Me.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.qAlignmentOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position
				For i As Integer = 0 To linearBoneTable.boneCount - 1
					Dim qAlignment As New SourceQuaternion
					qAlignment.x = Me.theInputFileReader.ReadSingle()
					qAlignment.y = Me.theInputFileReader.ReadSingle()
					qAlignment.z = Me.theInputFileReader.ReadSingle()
					qAlignment.w = Me.theInputFileReader.ReadSingle()
					linearBoneTable.theQAlignments.Add(qAlignment)
				Next
				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
				'If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theQAlignments")
				'End If
				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theQAlignments alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	'Public Sub ReadFinalBytesAlignment()
	'	Me.theMdlFileData.theFileSeekLog.LogAndAlignFromFileSeekLogEnd(Me.theInputFileReader, 4, "Final bytes alignment")
	'End Sub

	'Public Sub ReadUnknownValues(ByVal aFileSeekLog As FileSeekLog)
	'	'Me.theMdlFileData.theUnknownValues = New List(Of UnknownValue)()

	'	Dim offsetStart As Long
	'	Dim offsetEnd As Long
	'	Dim offsetGapStart As Long
	'	Dim offsetGapEnd As Long
	'	offsetStart = -1
	'	Try
	'		For i As Integer = 0 To aFileSeekLog.theFileSeekList.Count - 1
	'			If offsetStart = -1 Then
	'				offsetStart = aFileSeekLog.theFileSeekList.Keys(i)
	'			End If
	'			offsetEnd = aFileSeekLog.theFileSeekList.Values(i)
	'			If (i = aFileSeekLog.theFileSeekList.Count - 1) Then
	'				Exit For
	'			ElseIf (offsetEnd + 1 <> aFileSeekLog.theFileSeekList.Keys(i + 1)) Then
	'				offsetGapStart = offsetEnd + 1
	'				offsetGapEnd = aFileSeekLog.theFileSeekList.Keys(i + 1) - 1
	'				Me.theInputFileReader.BaseStream.Seek(offsetGapStart, SeekOrigin.Begin)
	'				For offset As Long = offsetGapStart To offsetGapEnd Step 4
	'					If offsetGapEnd - offset < 3 Then
	'						For byteOffset As Long = offset To offsetGapEnd
	'							Dim anUnknownValue As New UnknownValue()
	'							anUnknownValue.offset = byteOffset
	'							anUnknownValue.type = "Byte"
	'							anUnknownValue.value = Me.theInputFileReader.ReadByte()
	'							Me.theMdlFileData.theUnknownValues.Add(anUnknownValue)
	'						Next
	'					Else
	'						Dim anUnknownValue As New UnknownValue()
	'						anUnknownValue.offset = offset
	'						anUnknownValue.type = "Int32"
	'						anUnknownValue.value = Me.theInputFileReader.ReadInt32()
	'						Me.theMdlFileData.theUnknownValues.Add(anUnknownValue)
	'					End If
	'				Next
	'				offsetStart = -1
	'			End If
	'		Next
	'	Catch ex As Exception
	'		Dim debug As Integer = 4242
	'	End Try
	'End Sub

	Public Sub ReadUnreadBytes()
		Me.theMdlFileData.theFileSeekLog.LogUnreadBytes(Me.theInputFileReader)
	End Sub

	Public Sub PostProcess()
		If Me.theMdlFileData.theBodyParts IsNot Nothing Then
			For Each aBodyPart As SourceMdlBodyPart In Me.theMdlFileData.theBodyParts
				For Each aBodyModel As SourceMdlModel In aBodyPart.theModels
					If aBodyModel.theEyeballs IsNot Nothing AndAlso aBodyModel.theEyeballs.Count > 0 Then
						aBodyPart.theModelCommandIsUsed = True
						aBodyPart.theEyeballOptionIsUsed = True
						Exit For
					End If

					If aBodyModel.theMeshes IsNot Nothing Then
						For Each aMesh As SourceMdlMesh In aBodyModel.theMeshes
							If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
								aBodyPart.theModelCommandIsUsed = True
								Exit For
							End If
						Next
						If aBodyPart.theModelCommandIsUsed Then
							Exit For
						End If
					End If
				Next
			Next
		End If
	End Sub

	Public Sub CreateFlexFrameList()
		Dim aFlexFrame As FlexFrame
		Dim aBodyPart As SourceMdlBodyPart
		Dim aModel As SourceMdlModel
		Dim aMesh As SourceMdlMesh
		Dim aFlex As SourceMdlFlex
		Dim searchedFlexFrame As FlexFrame

		'Me.theMdlFileData.theFlexFrames = New List(Of FlexFrame)()

		''NOTE: Create the defaultflex.
		'aFlexFrame = New FlexFrame()
		'Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)

		If Me.theMdlFileData.theFlexDescs IsNot Nothing AndAlso Me.theMdlFileData.theFlexDescs.Count > 0 Then
			Dim flexDescToFlexFrames As List(Of List(Of FlexFrame))
			Dim meshVertexIndexStart As Integer
			Dim cumulativebodyPartVertexIndexStart As Integer

			cumulativebodyPartVertexIndexStart = 0
			For bodyPartIndex As Integer = 0 To Me.theMdlFileData.theBodyParts.Count - 1
				aBodyPart = Me.theMdlFileData.theBodyParts(bodyPartIndex)

				flexDescToFlexFrames = New List(Of List(Of FlexFrame))(Me.theMdlFileData.theFlexDescs.Count)
				For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
					Dim flexFrameList As New List(Of FlexFrame)()
					flexDescToFlexFrames.Add(flexFrameList)
				Next

				aBodyPart.theFlexFrames = New List(Of FlexFrame)()
				'NOTE: Create the defaultflex.
				aFlexFrame = New FlexFrame()
				aBodyPart.theFlexFrames.Add(aFlexFrame)

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
											aFlexFrame = New FlexFrame()
											'Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)
											aBodyPart.theFlexFrames.Add(aFlexFrame)
											aFlexFrame.bodyAndMeshVertexIndexStarts = New List(Of Integer)()
											aFlexFrame.flexes = New List(Of SourceMdlFlex)()

											Dim aFlexDescPartnerIndex As Integer
											aFlexDescPartnerIndex = aMesh.theFlexes(flexIndex).flexDescPartnerIndex

											aFlexFrame.flexName = Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theName
											If aFlexDescPartnerIndex > 0 Then
												'line += "flexpair """
												'aFlexFrame.flexName = aFlexFrame.flexName.Remove(aFlexFrame.flexName.Length - 1, 1)
												aFlexFrame.flexDescription = aFlexFrame.flexName
												aFlexFrame.flexDescription += "+"
												aFlexFrame.flexDescription += Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theName
												aFlexFrame.flexHasPartner = True
												aFlexFrame.flexPartnerName = Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theName
												aFlexFrame.flexSplit = Me.GetSplit(aFlex, meshVertexIndexStart)
												Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theDescIsUsedByFlex = True
											Else
												'line += "flex """
												aFlexFrame.flexDescription = aFlexFrame.flexName
												aFlexFrame.flexHasPartner = False
											End If
											Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theDescIsUsedByFlex = True

											flexDescToFlexFrames(aFlex.flexDescIndex).Add(aFlexFrame)
										End If

										aFlexFrame.bodyAndMeshVertexIndexStarts.Add(meshVertexIndexStart + cumulativebodyPartVertexIndexStart)
										aFlexFrame.flexes.Add(aFlex)

										'flexDescToMeshIndexes(aFlex.flexDescIndex).Add(meshIndex)
									Next
								End If
							Next
						End If
						'For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
						'	flexDescToMeshIndexes(x).Clear()
						'Next

						cumulativebodyPartVertexIndexStart += aModel.vertexCount
					Next
				End If
			Next
		End If
	End Sub

	Public Sub WriteInternalMdlFileName(ByVal internalMdlFileName As String)
		Me.theOutputFileWriter.BaseStream.Seek(&HC, SeekOrigin.Begin)
		'TODO: Should only write up to 64 characters.
		Me.theOutputFileWriter.Write(internalMdlFileName.ToCharArray())
		'NOTE: Write the ending null byte.
		Me.theOutputFileWriter.Write(Convert.ToByte(0))
	End Sub

	Public Sub WriteInternalMdlFileNameCopy(ByVal internalMdlFileNameCopy As String)
		If Me.theMdlFileData.nameCopyOffset > 0 Then
			' Set a new offset for the file name at end-of-file's null byte.
			Me.theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End)
			'NOTE: Important that offset be an Integer (4 bytes) rather than a Long (8 bytes).
			Dim offset As Integer
			offset = CInt(Me.theOutputFileWriter.BaseStream.Position) - &H198
			Me.theOutputFileWriter.BaseStream.Seek(&H1AC, SeekOrigin.Begin)
			Me.theOutputFileWriter.Write(offset)

			' Write the new file name.
			'Me.theOutputFileWriter.BaseStream.Seek(offset, SeekOrigin.Begin)
			Me.theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End)
			Me.theOutputFileWriter.Write(internalMdlFileNameCopy.ToCharArray())
			'NOTE: Write the ending null byte.
			Me.theOutputFileWriter.Write(Convert.ToByte(0))

			' Write the new file size.
			Me.theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End)
			offset = CInt(Me.theOutputFileWriter.BaseStream.Position)
			Me.theOutputFileWriter.BaseStream.Seek(&H4C, SeekOrigin.Begin)
			Me.theOutputFileWriter.Write(offset)
		End If
	End Sub

	'TODO: Review WriteInternalAniFileName() for each MDL version.
	Public Sub WriteInternalAniFileName(ByVal internalAniFileName As String)
		If Me.theMdlFileData.animBlockCount > 0 Then
			If Me.theMdlFileData.animBlockNameOffset > 0 Then

				If Me.theMdlFileData.version = 44 Then
					Me.theOutputFileWriter.BaseStream.Seek(Me.theMdlFileData.animBlockNameOffset, SeekOrigin.Begin)
					'TODO: Should only write up to existing count of characters.
					If Me.theMdlFileData.theAnimBlockRelativePathFileName.Length > internalAniFileName.Length Then
						Me.theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End)
						Dim offset As Long
						offset = Me.theOutputFileWriter.BaseStream.Position
						Me.theOutputFileWriter.BaseStream.Seek(&H15C, SeekOrigin.Begin)
						Me.theOutputFileWriter.Write(offset)
						Me.theOutputFileWriter.BaseStream.Seek(offset, SeekOrigin.Begin)
					End If
					Me.theOutputFileWriter.Write(internalAniFileName.ToCharArray())
					'NOTE: Write the ending null byte.
					Me.theOutputFileWriter.Write(Convert.ToByte(0))
				ElseIf Me.theMdlFileData.version <= 48 Then
					' Set a new offset for the file name at end-of-file's second null byte.
					Me.theOutputFileWriter.BaseStream.Seek(-2, SeekOrigin.End)
					'NOTE: Important that offset be an Integer (4 bytes) rather than a Long (8 bytes).
					Dim offset As Integer
					offset = CInt(Me.theOutputFileWriter.BaseStream.Position)
					Me.theOutputFileWriter.BaseStream.Seek(&H15C, SeekOrigin.Begin)
					Me.theOutputFileWriter.Write(offset)

					' Write the new file name.
					Me.theOutputFileWriter.BaseStream.Seek(offset, SeekOrigin.Begin)
					Me.theOutputFileWriter.Write(internalAniFileName.ToCharArray())
					'NOTE: Write the ending null byte.
					Me.theOutputFileWriter.Write(Convert.ToByte(0))

					' Write the new end-of-file's null bytes.
					Me.theOutputFileWriter.Write(Convert.ToByte(0))
					Me.theOutputFileWriter.Write(Convert.ToByte(0))

					' Write the new file size.
					Me.theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End)
					offset = CInt(Me.theOutputFileWriter.BaseStream.Position)
					Me.theOutputFileWriter.BaseStream.Seek(&H4C, SeekOrigin.Begin)
					Me.theOutputFileWriter.Write(offset)
				Else
					' Set a new offset for the file name at end-of-file's null byte.
					Me.theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End)
					'NOTE: Important that offset be an Integer (4 bytes) rather than a Long (8 bytes).
					Dim offset As Integer
					offset = CInt(Me.theOutputFileWriter.BaseStream.Position)
					Me.theOutputFileWriter.BaseStream.Seek(&H15C, SeekOrigin.Begin)
					Me.theOutputFileWriter.Write(offset)

					' Write the new file name.
					'Me.theOutputFileWriter.BaseStream.Seek(offset, SeekOrigin.Begin)
					Me.theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End)
					Me.theOutputFileWriter.Write(internalAniFileName.ToCharArray())
					'NOTE: Write the ending null byte.
					Me.theOutputFileWriter.Write(Convert.ToByte(0))

					' Write the new file size.
					Me.theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End)
					offset = CInt(Me.theOutputFileWriter.BaseStream.Position)
					Me.theOutputFileWriter.BaseStream.Seek(&H4C, SeekOrigin.Begin)
					Me.theOutputFileWriter.Write(offset)
				End If

			End If
		End If
	End Sub

#End Region

#Region "Private Methods"

	'Protected Sub LogToEndAndAlignToNextStart(ByVal fileOffsetEnd As Long, ByVal byteAlignmentCount As Integer, ByVal description As String)
	'	Dim fileOffsetStart2 As Long
	'	Dim fileOffsetEnd2 As Long

	'	'fileOffsetStart2 = fileOffsetEnd + 1
	'	'fileOffsetEnd2 = MathModule.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
	'	'If fileOffsetEnd2 >= fileOffsetStart2 Then
	'	'	Me.theInputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
	'	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, description)
	'	'End If
	'	fileOffsetStart2 = fileOffsetEnd + 1
	'	fileOffsetEnd2 = MathModule.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
	'	Me.theInputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
	'	If fileOffsetEnd2 >= fileOffsetStart2 Then
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, description)
	'	End If
	'End Sub

	'NOTE: eyelidPartIndex values:
	'      0: lowerer
	'      1: raiser
	Private Function FindFlexFrameIndex(ByVal flexFrames As List(Of FlexFrame), ByVal flexName As String, ByVal eyelidPartIndex As Integer) As Integer
		Dim eyelidFlexCount As Integer = 0
		For i As Integer = 0 To flexFrames.Count - 1
			If flexName = flexFrames(i).flexName Then
				If eyelidFlexCount = eyelidPartIndex Then
					Return i
				Else
					eyelidFlexCount += 1
				End If
			End If
		Next
		Return 0
	End Function

	Private Function GetSplit(ByVal aFlex As SourceMdlFlex, ByVal meshVertexIndexStart As Integer) As Double
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

	Protected theMdlFileData As SourceMdlFileData49

#End Region

End Class
