Imports System.IO
Imports System.Text

Public Class SourceMdlFile

#Region "Creation and Destruction"

	Public Sub New()
	End Sub

#End Region

#Region "Methods"

	Public Overridable Sub ReadMdlHeader00()

	End Sub

	Public Overridable Sub ReadMdlHeader01()

	End Sub

	Public Overridable Sub ReadMdlHeader02()

	End Sub

	Public Overridable Sub ReadTextures()

	End Sub

	Public Overridable Sub ReadTexturePaths()

	End Sub

	Public Overridable Sub ReadFileForViewer(ByVal inputPathFileName As String, ByVal aMdlFileData As SourceMdlFileData)
		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Try
			inputFileStream = New FileStream(inputPathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					Me.theMdlFileData = aMdlFileData
					Me.theMdlFileData.theActualFileSize = inputFileStream.Length
					Me.theMdlFileData.theSectionFrameCount = 0
					Me.theMdlFileData.theModelCommandIsUsed = False
					Me.theMdlFileData.theProceduralBonesCommandIsUsed = False
					Me.theMdlFileData.theFileSeekLog = New FileSeekLog()

					Me.ReadMdlHeader00()
					Me.ReadMdlHeader01()
					'Me.ReadMdlHeader02()

					Me.ReadTexturePaths()
					Me.ReadTextures()
				Catch
				Finally
					If Me.theInputFileReader IsNot Nothing Then
						Me.theInputFileReader.Close()
					End If
				End Try
			End If
		Catch
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try
	End Sub

	Public Sub WriteHeaderNameToFile(ByVal inputPathFileName As String, ByVal headerName As String)
		Dim inputFileStream As FileStream

		inputFileStream = Nothing
		Try
			inputFileStream = New FileStream(inputPathFileName, FileMode.Open)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					Me.theMdlFileData = New SourceMdlFileData()
					Me.ReadMdlHeader00()
				Catch ex As Exception
					Throw
				Finally
					If Me.theInputFileReader IsNot Nothing Then
						Me.theInputFileReader.Close()
					End If
				End Try
			End If
		Catch
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try

		inputFileStream = Nothing
		Try
			inputFileStream = New FileStream(inputPathFileName, FileMode.Open)
			If inputFileStream IsNot Nothing Then
				Dim inputFileWriter As BinaryWriter = Nothing
				Try
					'NOTE: Important to set System.Text.Encoding.ASCII so that ReadChars() only reads in one byte per Char.
					inputFileWriter = New BinaryWriter(inputFileStream, System.Text.Encoding.ASCII)

					If Me.theMdlFileData.version > 10 Then
						inputFileWriter.BaseStream.Seek(&HC, SeekOrigin.Begin)
					Else
						inputFileWriter.BaseStream.Seek(&H8, SeekOrigin.Begin)
					End If
					If Me.theMdlFileData.version <> 2531 Then
						'TODO: Should only write up to 64 characters.
						inputFileWriter.Write(headerName.ToCharArray())
					Else
						'NOTE: Extra name bytes for "Vampire The Masquerade - Bloodlines".
						'TODO: Should only write up to 128 characters.
						inputFileWriter.Write(headerName.ToCharArray())
					End If
					'NOTE: Write the ending null byte.
					inputFileWriter.Write(Convert.ToByte(0))
				Catch
				Finally
					If inputFileWriter IsNot Nothing Then
						inputFileWriter.Close()
					End If
				End Try
			End If
		Catch
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try
	End Sub

#End Region

#Region "Private Methods"

	'Protected Sub ReadMdlHeader00(ByVal logDescription As String)
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	' Offsets: 0x00, 0x04, 0x08, 0x0C (12), 0x4C (76)
	'	Me.theMdlFileData.id = Me.theInputFileReader.ReadChars(4)
	'	Me.theMdlFileData.theID = Me.theMdlFileData.id
	'	Me.theMdlFileData.version = Me.theInputFileReader.ReadInt32()

	'	' This does not seem to be in v6 or v10.
	'	If Me.theMdlFileData.version > 10 Then
	'		Me.theMdlFileData.checksum = Me.theInputFileReader.ReadInt32()
	'	End If

	'	If Me.theMdlFileData.version <> 2531 Then
	'		Me.theMdlFileData.name = Me.theInputFileReader.ReadChars(64)
	'		Me.theMdlFileData.theName = CStr(Me.theMdlFileData.name).Trim(Chr(0))
	'	Else
	'		'NOTE: Extra name bytes for "Vampire The Masquerade - Bloodlines".
	'		Me.theMdlFileData.nameForVtmb = Me.theInputFileReader.ReadChars(128)
	'	End If

	'	Me.theMdlFileData.fileSize = Me.theInputFileReader.ReadInt32()

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	If logDescription <> "" Then
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription)
	'	End If
	'End Sub

	'Protected Sub ReadMdlHeader01(ByVal logDescription As String)
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	Dim fileOffsetStart2 As Long
	'	Dim fileOffsetEnd2 As Long

	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	'NOTE: Read unknown bytes for VtMB.
	'	If Me.theMdlFileData.version = 2531 Then
	'		Me.theInputFileReader.ReadSingle()
	'		Me.theInputFileReader.ReadSingle()
	'		Me.theInputFileReader.ReadSingle()
	'	End If

	'	' Offsets: 0x50, 0x54, 0x58
	'	Me.theMdlFileData.eyePositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theMdlFileData.eyePositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theMdlFileData.eyePositionZ = Me.theInputFileReader.ReadSingle()

	'	If Me.theMdlFileData.version > 10 Then
	'		' Offsets: 0x5C, 0x60, 0x64
	'		Me.theMdlFileData.illuminationPositionX = Me.theInputFileReader.ReadSingle()
	'		Me.theMdlFileData.illuminationPositionY = Me.theInputFileReader.ReadSingle()
	'		Me.theMdlFileData.illuminationPositionZ = Me.theInputFileReader.ReadSingle()
	'	End If

	'	' Offsets: 0x68, 0x6C, 0x70
	'	Me.theMdlFileData.hullMinPositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theMdlFileData.hullMinPositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theMdlFileData.hullMinPositionZ = Me.theInputFileReader.ReadSingle()

	'	' Offsets: 0x74, 0x78, 0x7C
	'	Me.theMdlFileData.hullMaxPositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theMdlFileData.hullMaxPositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theMdlFileData.hullMaxPositionZ = Me.theInputFileReader.ReadSingle()

	'	' Offsets: 0x80, 0x84, 0x88
	'	Me.theMdlFileData.viewBoundingBoxMinPositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theMdlFileData.viewBoundingBoxMinPositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theMdlFileData.viewBoundingBoxMinPositionZ = Me.theInputFileReader.ReadSingle()

	'	' Offsets: 0x8C, 0x90, 0x94
	'	Me.theMdlFileData.viewBoundingBoxMaxPositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theMdlFileData.viewBoundingBoxMaxPositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theMdlFileData.viewBoundingBoxMaxPositionZ = Me.theInputFileReader.ReadSingle()

	'	' Offsets: 0x98
	'	Me.theMdlFileData.flags = Me.theInputFileReader.ReadInt32()

	'	'NOTE: Read unknown bytes for VtMB.
	'	If Me.theMdlFileData.version = 2531 Then
	'		Me.theInputFileReader.ReadSingle()
	'		Me.theInputFileReader.ReadSingle()
	'	End If

	'	' Offsets: 0x9C (156), 0xA0
	'	Me.theMdlFileData.boneCount = Me.theInputFileReader.ReadInt32()
	'	Me.theMdlFileData.boneOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xA4, 0xA8
	'	Me.theMdlFileData.boneControllerCount = Me.theInputFileReader.ReadInt32()
	'	Me.theMdlFileData.boneControllerOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xAC (172), 0xB0
	'	Me.theMdlFileData.hitboxSetCount = Me.theInputFileReader.ReadInt32()
	'	Me.theMdlFileData.hitboxSetOffset = Me.theInputFileReader.ReadInt32()

	'	If Me.theMdlFileData.version = 10 Then
	'		Me.theMdlFileData.localSequenceCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.localSequenceOffset = Me.theInputFileReader.ReadInt32()

	'		Me.theMdlFileData.sequenceGroupCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.sequenceGroupOffset = Me.theInputFileReader.ReadInt32()
	'	ElseIf Me.theMdlFileData.version = 2531 Then
	'		Me.theMdlFileData.localAnimationCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.localAnimationOffset = Me.theInputFileReader.ReadInt32()

	'		Me.theMdlFileData.localSequenceCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.localSequenceOffset = Me.theInputFileReader.ReadInt32()

	'		Me.theMdlFileData.localNodeCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.localNodeOffset = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.localNodeNameOffset = Me.theInputFileReader.ReadInt32()
	'	Else
	'		' Offsets: 0xB4 (180), 0xB8
	'		Me.theMdlFileData.localAnimationCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.localAnimationOffset = Me.theInputFileReader.ReadInt32()

	'		' Offsets: 0xBC (188), 0xC0 (192)
	'		Me.theMdlFileData.localSequenceCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.localSequenceOffset = Me.theInputFileReader.ReadInt32()

	'		' Offsets: 0xC4, 0xC8
	'		Me.theMdlFileData.activityListVersion = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.eventsIndexed = Me.theInputFileReader.ReadInt32()
	'	End If

	'	' Offsets: 0xCC (204), 0xD0 (208)
	'	Me.theMdlFileData.textureCount = Me.theInputFileReader.ReadInt32()
	'	Me.theMdlFileData.textureOffset = Me.theInputFileReader.ReadInt32()

	'	'TODO: Check this.
	'	If Me.theMdlFileData.version > 10 Then
	'		' Offsets: 0xD4 (212), 0xD8
	'		Me.theMdlFileData.texturePathCount = Me.theInputFileReader.ReadInt32()
	'	End If
	'	'TODO: For version 10: 	int					texturedataindex;
	'	Me.theMdlFileData.texturePathOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xDC, 0xE0 (224), 0xE4 (228)
	'	Me.theMdlFileData.skinReferenceCount = Me.theInputFileReader.ReadInt32()
	'	Me.theMdlFileData.skinFamilyCount = Me.theInputFileReader.ReadInt32()
	'	Me.theMdlFileData.skinFamilyOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xE8 (232), 0xEC (236)
	'	Me.theMdlFileData.bodyPartCount = Me.theInputFileReader.ReadInt32()
	'	Me.theMdlFileData.bodyPartOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xF0 (240), 0xF4 (244)
	'	Me.theMdlFileData.localAttachmentCount = Me.theInputFileReader.ReadInt32()
	'	Me.theMdlFileData.localAttachmentOffset = Me.theInputFileReader.ReadInt32()

	'	If Me.theMdlFileData.version = 10 Then
	'		Me.theMdlFileData.soundtable = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.soundindex = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.soundgroups = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.soundgroupindex = Me.theInputFileReader.ReadInt32()
	'	End If

	'	If Me.theMdlFileData.version <> 2531 Then
	'		' Offsets: 0xF8, 0xFC, 0x0100
	'		Me.theMdlFileData.localNodeCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.localNodeOffset = Me.theInputFileReader.ReadInt32()
	'	End If

	'	If Me.theMdlFileData.version > 10 Then
	'		If Me.theMdlFileData.version <> 2531 Then
	'			Me.theMdlFileData.localNodeNameOffset = Me.theInputFileReader.ReadInt32()
	'		End If

	'		' Offsets: 0x0104 (), 0x0108 ()
	'		Me.theMdlFileData.flexDescCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.flexDescOffset = Me.theInputFileReader.ReadInt32()

	'		' Offsets: 0x010C (), 0x0110 ()
	'		Me.theMdlFileData.flexControllerCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.flexControllerOffset = Me.theInputFileReader.ReadInt32()

	'		' Offsets: 0x0114 (), 0x0118 ()
	'		Me.theMdlFileData.flexRuleCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.flexRuleOffset = Me.theInputFileReader.ReadInt32()

	'		' Offsets: 0x011C (), 0x0120 ()
	'		Me.theMdlFileData.ikChainCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.ikChainOffset = Me.theInputFileReader.ReadInt32()

	'		' Offsets: 0x0124 (), 0x0128 ()
	'		Me.theMdlFileData.mouthCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.mouthOffset = Me.theInputFileReader.ReadInt32()

	'		' Offsets: 0x012C (), 0x0130 ()
	'		Me.theMdlFileData.localPoseParamaterCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.localPoseParameterOffset = Me.theInputFileReader.ReadInt32()

	'		If Me.theMdlFileData.version <> 2531 Then
	'			' Offsets: 0x0134 ()
	'			Me.theMdlFileData.surfacePropOffset = Me.theInputFileReader.ReadInt32()

	'			'TODO: Same as some lines below. Move to a separate function.
	'			If Me.theMdlFileData.surfacePropOffset > 0 Then
	'				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.surfacePropOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				Me.theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theSurfacePropName")
	'				End If
	'				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'			Else
	'				Me.theMdlFileData.theSurfacePropName = ""
	'			End If

	'			' Offsets: 0x0138 (312), 0x013C (316)
	'			Me.theMdlFileData.keyValueOffset = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.keyValueSize = Me.theInputFileReader.ReadInt32()
	'		End If

	'		Me.theMdlFileData.localIkAutoPlayLockCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.localIkAutoPlayLockOffset = Me.theInputFileReader.ReadInt32()

	'		If Me.theMdlFileData.version = 2531 Then
	'			Me.theMdlFileData.animBlockNameOffset = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.flexControllerUiCount_VERSION48 = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.flexControllerUiOffset_VERSION48 = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.includeModelCount = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.includeModelOffset = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.surfacePropOffset = Me.theInputFileReader.ReadInt32()

	'			'TODO: Same as some lines above. Move to a separate function.
	'			If Me.theMdlFileData.surfacePropOffset > 0 Then
	'				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.surfacePropOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				Me.theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theSurfacePropName")
	'				End If
	'				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'			Else
	'				Me.theMdlFileData.theSurfacePropName = ""
	'			End If

	'			Me.theMdlFileData.boneTableByNameOffset = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.animBlockCount = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.animBlockOffset = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.directionalLightDot = Me.theInputFileReader.ReadByte()

	'			Me.theMdlFileData.rootLod = Me.theInputFileReader.ReadByte()

	'			Me.theMdlFileData.allowedRootLodCount_VERSION48 = Me.theInputFileReader.ReadByte()

	'			Me.theMdlFileData.unused = Me.theInputFileReader.ReadByte()
	'		Else
	'			Me.theMdlFileData.mass = Me.theInputFileReader.ReadSingle()
	'			Me.theMdlFileData.contents = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.includeModelCount = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.includeModelOffset = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.virtualModelP = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.animBlockNameOffset = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.animBlockCount = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.animBlockOffset = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.animBlockModelP = Me.theInputFileReader.ReadInt32()
	'			If Me.theMdlFileData.animBlockCount > 0 Then
	'				If Me.theMdlFileData.animBlockNameOffset > 0 Then
	'					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'					Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.animBlockNameOffset, SeekOrigin.Begin)
	'					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'					Me.theMdlFileData.theAnimBlockRelativePathFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theAnimBlockRelativePathFileName")
	'					End If
	'					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'				End If
	'				If Me.theMdlFileData.animBlockOffset > 0 Then
	'					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'					Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.animBlockOffset, SeekOrigin.Begin)
	'					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'					Me.theMdlFileData.theAnimBlocks = New List(Of SourceMdlAnimBlock)(Me.theMdlFileData.animBlockCount)
	'					For offset As Integer = 0 To Me.theMdlFileData.animBlockCount - 1
	'						Dim anAnimBlock As New SourceMdlAnimBlock()
	'						anAnimBlock.dataStart = Me.theInputFileReader.ReadInt32()
	'						anAnimBlock.dataEnd = Me.theInputFileReader.ReadInt32()
	'						Me.theMdlFileData.theAnimBlocks.Add(anAnimBlock)
	'					Next

	'					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theAnimBlocks")
	'					End If
	'					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'				End If
	'			End If

	'			Me.theMdlFileData.boneTableByNameOffset = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.vertexBaseP = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.indexBaseP = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.directionalLightDot = Me.theInputFileReader.ReadByte()

	'			Me.theMdlFileData.rootLod = Me.theInputFileReader.ReadByte()

	'			Me.theMdlFileData.allowedRootLodCount_VERSION48 = Me.theInputFileReader.ReadByte()

	'			Me.theMdlFileData.unused = Me.theInputFileReader.ReadByte()

	'			Me.theMdlFileData.unused4 = Me.theInputFileReader.ReadInt32()

	'			If Me.theMdlFileData.version > 44 Then
	'				Me.theMdlFileData.flexControllerUiCount_VERSION48 = Me.theInputFileReader.ReadInt32()
	'				Me.theMdlFileData.flexControllerUiOffset_VERSION48 = Me.theInputFileReader.ReadInt32()
	'			Else
	'				'NOTE: Ignore these values.
	'				Me.theInputFileReader.ReadInt32()
	'				Me.theInputFileReader.ReadInt32()
	'			End If

	'			Me.theMdlFileData.unused3(0) = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.unused3(1) = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.studioHeader2Offset_VERSION48 = Me.theInputFileReader.ReadInt32()

	'			Me.theMdlFileData.unused2 = Me.theInputFileReader.ReadInt32()
	'		End If
	'	End If

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription)

	'	If Me.theMdlFileData.bodyPartCount = 0 AndAlso Me.theMdlFileData.localSequenceCount > 0 Then
	'		Me.theMdlFileData.theMdlFileOnlyHasAnimations = True
	'	End If
	'End Sub

	'Protected Sub ReadMdlHeader02(ByVal logDescription As String)
	'	'Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	'Dim fileOffsetStart2 As Long
	'	'Dim fileOffsetEnd2 As Long

	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	If Me.theMdlFileData.version > 44 Then
	'		Me.theMdlFileData.sourceBoneTransformCount = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.sourceBoneTransformOffset = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.illumPositionAttachmentIndex = Me.theInputFileReader.ReadInt32()
	'		Me.theMdlFileData.maxEyeDeflection = Me.theInputFileReader.ReadSingle()
	'		Me.theMdlFileData.linearBoneOffset = Me.theInputFileReader.ReadInt32()
	'		For x As Integer = 0 To Me.theMdlFileData.reserved.Length - 1
	'			Me.theMdlFileData.reserved(x) = Me.theInputFileReader.ReadInt32()
	'		Next
	'		'======
	'		'For x As Integer = 0 To Me.theMdlFileData.studiohdr2.Length - 1
	'		'	Me.theMdlFileData.studiohdr2(x) = Me.theInputFileReader.ReadInt32()
	'		'Next
	'	End If

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription)
	'End Sub

	'Private Sub ReadBones()
	'	If Me.theMdlFileData.boneCount > 0 Then
	'		Dim boneInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Try
	'			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneOffset, SeekOrigin.Begin)
	'			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'			Me.theMdlFileData.theBones = New List(Of SourceMdlBone)(Me.theMdlFileData.boneCount)
	'			For i As Integer = 0 To Me.theMdlFileData.boneCount - 1
	'				boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'				Dim aBone As New SourceMdlBone()

	'				If Me.theMdlFileData.version = 10 Then
	'					aBone.name = Me.theInputFileReader.ReadChars(32)
	'					aBone.theName = aBone.name
	'					aBone.theName = StringClass.ConvertFromNullTerminatedString(aBone.theName)
	'					aBone.parentBoneIndex = Me.theInputFileReader.ReadInt32()
	'					aBone.flags = Me.theInputFileReader.ReadInt32()
	'					For j As Integer = 0 To 5
	'						aBone.boneControllerIndex(j) = Me.theInputFileReader.ReadInt32()
	'					Next
	'					For j As Integer = 0 To 5
	'						aBone.value(j) = Me.theInputFileReader.ReadInt32()
	'					Next
	'					For j As Integer = 0 To 5
	'						aBone.scale(j) = Me.theInputFileReader.ReadInt32()
	'					Next
	'				Else
	'					aBone.nameOffset = Me.theInputFileReader.ReadInt32()

	'					aBone.parentBoneIndex = Me.theInputFileReader.ReadInt32()

	'					'' Skip some fields.
	'					'Me.theInputFileReader.ReadBytes(208)
	'					'------
	'					For j As Integer = 0 To aBone.boneControllerIndex.Length - 1
	'						aBone.boneControllerIndex(j) = Me.theInputFileReader.ReadInt32()
	'					Next
	'					aBone.position = New SourceVector()
	'					aBone.position.x = Me.theInputFileReader.ReadSingle()
	'					aBone.position.y = Me.theInputFileReader.ReadSingle()
	'					aBone.position.z = Me.theInputFileReader.ReadSingle()

	'					aBone.quat = New SourceQuaternion()
	'					aBone.quat.x = Me.theInputFileReader.ReadSingle()
	'					aBone.quat.y = Me.theInputFileReader.ReadSingle()
	'					aBone.quat.z = Me.theInputFileReader.ReadSingle()
	'					aBone.quat.w = Me.theInputFileReader.ReadSingle()

	'					If Me.theMdlFileData.version = 2531 Then
	'						For j As Integer = 0 To aBone.animChannels.Length - 1
	'							aBone.animChannels(j) = Me.theInputFileReader.ReadSingle()
	'						Next
	'					Else
	'						aBone.rotation = New SourceVector()
	'						aBone.rotation.x = Me.theInputFileReader.ReadSingle()
	'						aBone.rotation.y = Me.theInputFileReader.ReadSingle()
	'						aBone.rotation.z = Me.theInputFileReader.ReadSingle()
	'						aBone.positionScale = New SourceVector()
	'						aBone.positionScale.x = Me.theInputFileReader.ReadSingle()
	'						aBone.positionScale.y = Me.theInputFileReader.ReadSingle()
	'						aBone.positionScale.z = Me.theInputFileReader.ReadSingle()
	'						aBone.rotationScale = New SourceVector()
	'						aBone.rotationScale.x = Me.theInputFileReader.ReadSingle()
	'						aBone.rotationScale.y = Me.theInputFileReader.ReadSingle()
	'						aBone.rotationScale.z = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone00 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone01 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone02 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone03 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone10 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone11 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone12 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone13 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone20 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone21 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone22 = Me.theInputFileReader.ReadSingle()
	'						'aBone.poseToBone23 = Me.theInputFileReader.ReadSingle()
	'					End If

	'					aBone.poseToBoneColumn0 = New SourceVector()
	'					aBone.poseToBoneColumn1 = New SourceVector()
	'					aBone.poseToBoneColumn2 = New SourceVector()
	'					aBone.poseToBoneColumn3 = New SourceVector()
	'					aBone.poseToBoneColumn0.x = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn1.x = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn2.x = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn3.x = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn0.y = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn1.y = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn2.y = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn3.y = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn0.z = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn1.z = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn2.z = Me.theInputFileReader.ReadSingle()
	'					aBone.poseToBoneColumn3.z = Me.theInputFileReader.ReadSingle()
	'					'------
	'					'aBone.poseToBoneColumn0.x = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn0.y = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn0.z = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn1.x = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn1.y = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn1.z = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn2.x = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn2.y = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn2.z = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn3.x = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn3.y = Me.theInputFileReader.ReadSingle()
	'					'aBone.poseToBoneColumn3.z = Me.theInputFileReader.ReadSingle()

	'					If Me.theMdlFileData.version <> 2531 Then
	'						aBone.qAlignment = New SourceQuaternion()
	'						aBone.qAlignment.x = Me.theInputFileReader.ReadSingle()
	'						aBone.qAlignment.y = Me.theInputFileReader.ReadSingle()
	'						aBone.qAlignment.z = Me.theInputFileReader.ReadSingle()
	'						aBone.qAlignment.w = Me.theInputFileReader.ReadSingle()
	'					End If

	'					aBone.flags = Me.theInputFileReader.ReadInt32()

	'					aBone.proceduralRuleType = Me.theInputFileReader.ReadInt32()
	'					aBone.proceduralRuleOffset = Me.theInputFileReader.ReadInt32()
	'					aBone.physicsBoneIndex = Me.theInputFileReader.ReadInt32()
	'					aBone.surfacePropNameOffset = Me.theInputFileReader.ReadInt32()
	'					aBone.contents = Me.theInputFileReader.ReadInt32()

	'					If Me.theMdlFileData.version <> 2531 Then
	'						For k As Integer = 0 To 7
	'							aBone.unused(k) = Me.theInputFileReader.ReadInt32()
	'						Next
	'					End If
	'				End If

	'				Me.theMdlFileData.theBones.Add(aBone)

	'				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'				If aBone.nameOffset <> 0 Then
	'					Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.nameOffset, SeekOrigin.Begin)
	'					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'					aBone.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theName")
	'					End If
	'				ElseIf aBone.theName Is Nothing Then
	'					aBone.theName = ""
	'				End If
	'				If aBone.proceduralRuleOffset <> 0 Then
	'					If aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_AXISINTERP Then
	'						Me.ReadAxisInterpBone(boneInputFileStreamPosition, aBone)
	'					ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_QUATINTERP Then
	'						Me.theMdlFileData.theProceduralBonesCommandIsUsed = True
	'						Me.ReadQuatInterpBone(boneInputFileStreamPosition, aBone)
	'					ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
	'						Me.ReadJiggleBone(boneInputFileStreamPosition, aBone)
	'					End If
	'				End If
	'				If aBone.surfacePropNameOffset <> 0 Then
	'					Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.surfacePropNameOffset, SeekOrigin.Begin)
	'					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'					aBone.theSurfacePropName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theSurfacePropName")
	'					End If
	'				Else
	'					aBone.theSurfacePropName = ""
	'				End If

	'				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'			Next

	'			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones")

	'			Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
	'		Catch
	'		End Try
	'	End If
	'End Sub


	''TODO: VERIFY ReadAxisInterpBone()
	'Private Sub ReadAxisInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone)
	'	Dim axisInterpBoneInputFileStreamPosition As Long
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Try
	'		Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		axisInterpBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		aBone.theAxisInterpBone = New SourceMdlAxisInterpBone()
	'		aBone.theAxisInterpBone.control = Me.theInputFileReader.ReadInt32()
	'		For x As Integer = 0 To aBone.theAxisInterpBone.pos.Length - 1
	'			aBone.theAxisInterpBone.pos(x).x = Me.theInputFileReader.ReadSingle()
	'			aBone.theAxisInterpBone.pos(x).y = Me.theInputFileReader.ReadSingle()
	'			aBone.theAxisInterpBone.pos(x).z = Me.theInputFileReader.ReadSingle()
	'		Next
	'		For x As Integer = 0 To aBone.theAxisInterpBone.quat.Length - 1
	'			aBone.theAxisInterpBone.quat(x).x = Me.theInputFileReader.ReadSingle()
	'			aBone.theAxisInterpBone.quat(x).y = Me.theInputFileReader.ReadSingle()
	'			aBone.theAxisInterpBone.quat(x).z = Me.theInputFileReader.ReadSingle()
	'			aBone.theAxisInterpBone.quat(x).z = Me.theInputFileReader.ReadSingle()
	'		Next

	'		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		'If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
	'		'	Me.ReadTriggers(axisInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
	'		'End If

	'		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAxisInterpBone")
	'	Catch
	'	End Try
	'End Sub

	'Private Sub ReadQuatInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone)
	'	Dim quatInterpBoneInputFileStreamPosition As Long
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Try
	'		Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		quatInterpBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		aBone.theQuatInterpBone = New SourceMdlQuatInterpBone()
	'		aBone.theQuatInterpBone.controlBoneIndex = Me.theInputFileReader.ReadInt32()
	'		aBone.theQuatInterpBone.triggerCount = Me.theInputFileReader.ReadInt32()
	'		aBone.theQuatInterpBone.triggerOffset = Me.theInputFileReader.ReadInt32()

	'		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
	'			Me.ReadTriggers(quatInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
	'		End If

	'		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theQuatInterpBone")
	'	Catch
	'	End Try
	'End Sub

	'Private Sub ReadTriggers(ByVal quatInterpBoneInputFileStreamPosition As Long, ByVal aQuatInterpBone As SourceMdlQuatInterpBone)
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Try
	'		Me.theInputFileReader.BaseStream.Seek(quatInterpBoneInputFileStreamPosition + aQuatInterpBone.triggerOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		aQuatInterpBone.theTriggers = New List(Of SourceMdlQuatInterpBoneInfo)(aQuatInterpBone.triggerCount)
	'		For j As Integer = 0 To aQuatInterpBone.triggerCount - 1
	'			Dim aTrigger As New SourceMdlQuatInterpBoneInfo()

	'			aTrigger.inverseToleranceAngle = Me.theInputFileReader.ReadSingle()

	'			aTrigger.trigger = New SourceQuaternion()
	'			aTrigger.trigger.x = Me.theInputFileReader.ReadSingle()
	'			aTrigger.trigger.y = Me.theInputFileReader.ReadSingle()
	'			aTrigger.trigger.z = Me.theInputFileReader.ReadSingle()
	'			aTrigger.trigger.w = Me.theInputFileReader.ReadSingle()

	'			aTrigger.pos = New SourceVector()
	'			aTrigger.pos.x = Me.theInputFileReader.ReadSingle()
	'			aTrigger.pos.y = Me.theInputFileReader.ReadSingle()
	'			aTrigger.pos.z = Me.theInputFileReader.ReadSingle()

	'			aTrigger.quat = New SourceQuaternion()
	'			aTrigger.quat.x = Me.theInputFileReader.ReadSingle()
	'			aTrigger.quat.y = Me.theInputFileReader.ReadSingle()
	'			aTrigger.quat.z = Me.theInputFileReader.ReadSingle()
	'			aTrigger.quat.w = Me.theInputFileReader.ReadSingle()

	'			aQuatInterpBone.theTriggers.Add(aTrigger)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aQuatInterpBone.theTriggers")
	'	Catch
	'	End Try
	'End Sub

	'Private Sub ReadJiggleBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone)
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	aBone.theJiggleBone = New SourceMdlJiggleBone()
	'	aBone.theJiggleBone.flags = Me.theInputFileReader.ReadInt32()
	'	aBone.theJiggleBone.length = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.tipMass = Me.theInputFileReader.ReadSingle()

	'	aBone.theJiggleBone.yawStiffness = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.yawDamping = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.pitchStiffness = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.pitchDamping = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.alongStiffness = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.alongDamping = Me.theInputFileReader.ReadSingle()

	'	aBone.theJiggleBone.angleLimit = Me.theInputFileReader.ReadSingle()

	'	aBone.theJiggleBone.minYaw = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.maxYaw = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.yawFriction = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.yawBounce = Me.theInputFileReader.ReadSingle()

	'	aBone.theJiggleBone.minPitch = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.maxPitch = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.pitchFriction = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.pitchBounce = Me.theInputFileReader.ReadSingle()

	'	aBone.theJiggleBone.baseMass = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseStiffness = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseDamping = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseMinLeft = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseMaxLeft = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseLeftFriction = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseMinUp = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseMaxUp = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseUpFriction = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseMinForward = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseMaxForward = Me.theInputFileReader.ReadSingle()
	'	aBone.theJiggleBone.baseForwardFriction = Me.theInputFileReader.ReadSingle()

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theJiggleBone")
	'End Sub

	'Private Sub ReadBoneControllers()
	'	If Me.theMdlFileData.boneControllerCount > 0 Then
	'		Dim boneControllerInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		'Dim fileOffsetStart2 As Long
	'		'Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneControllerOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theBoneControllers = New List(Of SourceMdlBoneController)(Me.theMdlFileData.boneControllerCount)
	'		For i As Integer = 0 To Me.theMdlFileData.boneControllerCount - 1
	'			boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aBoneController As New SourceMdlBoneController()

	'			aBoneController.boneIndex = Me.theInputFileReader.ReadInt32()
	'			aBoneController.type = Me.theInputFileReader.ReadInt32()
	'			aBoneController.startBlah = Me.theInputFileReader.ReadSingle()
	'			aBoneController.endBlah = Me.theInputFileReader.ReadSingle()
	'			aBoneController.restIndex = Me.theInputFileReader.ReadInt32()
	'			aBoneController.inputField = Me.theInputFileReader.ReadInt32()
	'			If Me.theMdlFileData.version > 10 Then
	'				For x As Integer = 0 To aBoneController.unused.Length - 1
	'					aBoneController.unused(x) = Me.theInputFileReader.ReadInt32()
	'				Next
	'			End If

	'			Me.theMdlFileData.theBoneControllers.Add(aBoneController)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			'If aBoneController.nameOffset <> 0 Then
	'			'	Me.theInputFileReader.BaseStream.Seek(boneControllerInputFileStreamPosition + aBoneController.nameOffset, SeekOrigin.Begin)
	'			'	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'			'	aBoneController.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'			'	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'			'	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'			'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName")
	'			'	End If
	'			'Else
	'			'	aBoneController.theName = ""
	'			'End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment")
	'	End If
	'End Sub

	'Private Sub ReadAttachments()
	'	If Me.theMdlFileData.localAttachmentCount > 0 Then
	'		Dim attachmentInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localAttachmentOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theAttachments = New List(Of SourceMdlAttachment)(Me.theMdlFileData.localAttachmentCount)
	'		For i As Integer = 0 To Me.theMdlFileData.localAttachmentCount - 1
	'			attachmentInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim anAttachment As New SourceMdlAttachment()

	'			If Me.theMdlFileData.version = 10 Then
	'				anAttachment.name = Me.theInputFileReader.ReadChars(32)
	'				anAttachment.theName = anAttachment.name
	'				anAttachment.theName = StringClass.ConvertFromNullTerminatedString(anAttachment.theName)
	'				anAttachment.type = Me.theInputFileReader.ReadInt32()
	'				anAttachment.bone = Me.theInputFileReader.ReadInt32()

	'				anAttachment.attachmentPoint = New SourceVector()
	'				anAttachment.attachmentPoint.x = Me.theInputFileReader.ReadSingle()
	'				anAttachment.attachmentPoint.y = Me.theInputFileReader.ReadSingle()
	'				anAttachment.attachmentPoint.z = Me.theInputFileReader.ReadSingle()
	'				For x As Integer = 0 To 2
	'					anAttachment.vectors(x) = New SourceVector()
	'					anAttachment.vectors(x).x = Me.theInputFileReader.ReadSingle()
	'					anAttachment.vectors(x).y = Me.theInputFileReader.ReadSingle()
	'					anAttachment.vectors(x).z = Me.theInputFileReader.ReadSingle()
	'				Next
	'			Else
	'				anAttachment.nameOffset = Me.theInputFileReader.ReadInt32()
	'				anAttachment.flags = Me.theInputFileReader.ReadInt32()
	'				anAttachment.localBoneIndex = Me.theInputFileReader.ReadInt32()
	'				anAttachment.localM11 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM12 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM13 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM14 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM21 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM22 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM23 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM24 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM31 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM32 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM33 = Me.theInputFileReader.ReadSingle()
	'				anAttachment.localM34 = Me.theInputFileReader.ReadSingle()
	'				For x As Integer = 0 To 7
	'					anAttachment.unused(x) = Me.theInputFileReader.ReadInt32()
	'				Next
	'			End If

	'			Me.theMdlFileData.theAttachments.Add(anAttachment)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If anAttachment.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(attachmentInputFileStreamPosition + anAttachment.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				anAttachment.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName")
	'				End If
	'			Else
	'				anAttachment.theName = ""
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment")
	'	End If
	'End Sub

	'Private Sub ReadHitboxSets()
	'	If Me.theMdlFileData.hitboxSetCount > 0 Then
	'		Dim hitboxSetInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.hitboxSetOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theHitboxSets = New List(Of SourceMdlHitboxSet)(Me.theMdlFileData.hitboxSetCount)
	'		For i As Integer = 0 To Me.theMdlFileData.hitboxSetCount - 1
	'			hitboxSetInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aHitboxSet As New SourceMdlHitboxSet()
	'			aHitboxSet.nameOffset = Me.theInputFileReader.ReadInt32()
	'			aHitboxSet.hitboxCount = Me.theInputFileReader.ReadInt32()
	'			aHitboxSet.hitboxOffset = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.theHitboxSets.Add(aHitboxSet)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If aHitboxSet.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aHitboxSet.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitboxSet.theName")
	'				End If
	'			Else
	'				aHitboxSet.theName = ""
	'			End If
	'			Me.ReadHitboxes(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, aHitboxSet)

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxSets")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theHitboxSets alignment")
	'	End If
	'End Sub

	'Private Sub ReadHitboxes(ByVal hitboxOffsetInputFileStreamPosition As Long, ByVal aHitboxSet As SourceMdlHitboxSet)
	'	If aHitboxSet.hitboxCount > 0 Then
	'		Dim hitboxInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(hitboxOffsetInputFileStreamPosition, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		aHitboxSet.theHitboxes = New List(Of SourceMdlHitbox)(aHitboxSet.hitboxCount)
	'		For j As Integer = 0 To aHitboxSet.hitboxCount - 1
	'			hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aHitbox As New SourceMdlHitbox()

	'			aHitbox.boneIndex = Me.theInputFileReader.ReadInt32()
	'			aHitbox.groupIndex = Me.theInputFileReader.ReadInt32()
	'			aHitbox.boundingBoxMin.x = Me.theInputFileReader.ReadSingle()
	'			aHitbox.boundingBoxMin.y = Me.theInputFileReader.ReadSingle()
	'			aHitbox.boundingBoxMin.z = Me.theInputFileReader.ReadSingle()
	'			aHitbox.boundingBoxMax.x = Me.theInputFileReader.ReadSingle()
	'			aHitbox.boundingBoxMax.y = Me.theInputFileReader.ReadSingle()
	'			aHitbox.boundingBoxMax.z = Me.theInputFileReader.ReadSingle()

	'			If Me.theMdlFileData.version >= 49 Then
	'				aHitbox.nameOffset = Me.theInputFileReader.ReadInt32()
	'				'NOTE: Roll (z) is first.
	'				aHitbox.boundingBoxPitchYawRoll.z = Me.theInputFileReader.ReadSingle()
	'				aHitbox.boundingBoxPitchYawRoll.x = Me.theInputFileReader.ReadSingle()
	'				aHitbox.boundingBoxPitchYawRoll.y = Me.theInputFileReader.ReadSingle()
	'				For x As Integer = 0 To aHitbox.unused_VERSION49.Length - 1
	'					aHitbox.unused_VERSION49(x) = Me.theInputFileReader.ReadInt32()
	'				Next
	'			ElseIf Me.theMdlFileData.version > 10 Then
	'				aHitbox.nameOffset = Me.theInputFileReader.ReadInt32()
	'				For x As Integer = 0 To aHitbox.unused.Length - 1
	'					aHitbox.unused(x) = Me.theInputFileReader.ReadInt32()
	'				Next
	'			End If

	'			aHitboxSet.theHitboxes.Add(aHitbox)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If aHitbox.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(hitboxInputFileStreamPosition + aHitbox.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aHitbox.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitbox.theName")
	'				End If
	'			Else
	'				aHitbox.theName = ""
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet.theHitboxes")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
	'	End If
	'End Sub

	'Private Sub ReadBoneTableByName()
	'	If Me.theMdlFileData.boneTableByNameOffset <> 0 Then
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneTableByNameOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theBoneTableByName = New List(Of Integer)(Me.theMdlFileData.theBones.Count)
	'		Dim index As Byte
	'		For i As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
	'			index = Me.theInputFileReader.ReadByte()
	'			Me.theMdlFileData.theBoneTableByName.Add(index)
	'		Next


	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneTableByName")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theBoneTableByName alignment")
	'	End If
	'End Sub

	'Protected Sub ReadMdlAnimationForVersion10(ByVal animInputFileStreamPosition As Long)
	'	'Dim animationInputFileStreamPosition As Long
	'	'Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	'Dim fileOffsetStart2 As Long
	'	'Dim fileOffsetEnd2 As Long
	'	'Dim nextSetOfValuesOffset As Long

	'	fileOffsetStart = animInputFileStreamPosition

	'	'If anAnimation.thePosV.animXValueOffset > 0 Then
	'	'	Me.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animXValueOffset, sectionFrameCount, anAnimation.thePosV.theAnimXValues, "anAnimation.thePosV.theAnimXValues")
	'	'End If
	'	'If anAnimation.thePosV.animYValueOffset > 0 Then
	'	'	Me.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animYValueOffset, sectionFrameCount, anAnimation.thePosV.theAnimYValues, "anAnimation.thePosV.theAnimYValues")
	'	'End If
	'	'If anAnimation.thePosV.animZValueOffset > 0 Then
	'	'	Me.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animZValueOffset, sectionFrameCount, anAnimation.thePosV.theAnimZValues, "anAnimation.thePosV.theAnimZValues")
	'	'End If

	'	'If anAnimation.theRotV.animXValueOffset > 0 Then
	'	'	Me.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animXValueOffset, sectionFrameCount, anAnimation.theRotV.theAnimXValues, "anAnimation.theRotV.theAnimXValues")
	'	'End If
	'	'If anAnimation.theRotV.animYValueOffset > 0 Then
	'	'	Me.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animYValueOffset, sectionFrameCount, anAnimation.theRotV.theAnimYValues, "anAnimation.theRotV.theAnimYValues")
	'	'End If
	'	'If anAnimation.theRotV.animZValueOffset > 0 Then
	'	'	Me.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animZValueOffset, sectionFrameCount, anAnimation.theRotV.theAnimZValues, "anAnimation.theRotV.theAnimZValues")
	'	'End If


	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAnimations [this includes other logged data offsets]")

	'	Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "anAnimationDesc.theAnimations alignment")
	'End Sub

	'Private Sub ReadLocalAnimationDescs()
	'	Dim animInputFileStreamPosition As Long
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	Dim fileOffsetStart2 As Long
	'	Dim fileOffsetEnd2 As Long
	'	'Dim anAnimation As SourceMdlAnimation
	'	Dim aSectionOfAnimation As List(Of SourceMdlAnimation)

	'	Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localAnimationOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	Me.theMdlFileData.theAnimationDescs = New List(Of SourceMdlAnimationDesc)(Me.theMdlFileData.localAnimationCount)
	'	For i As Integer = 0 To Me.theMdlFileData.localAnimationCount - 1
	'		animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim anAnimationDesc As New SourceMdlAnimationDesc()

	'		anAnimationDesc.baseHeaderOffset = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.nameOffset = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.fps = Me.theInputFileReader.ReadSingle()
	'		anAnimationDesc.flags = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.frameCount = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.movementCount = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.movementOffset = Me.theInputFileReader.ReadInt32()

	'		For x As Integer = 0 To anAnimationDesc.unused1.Length - 1
	'			anAnimationDesc.unused1(x) = Me.theInputFileReader.ReadInt32()
	'		Next

	'		anAnimationDesc.animBlock = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.animOffset = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.ikRuleCount = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.ikRuleOffset = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.animblockIkRuleOffset = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.localHierarchyCount = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.localHierarchyOffset = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.sectionOffset = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.sectionFrameCount = Me.theInputFileReader.ReadInt32()

	'		anAnimationDesc.spanFrameCount = Me.theInputFileReader.ReadInt16()
	'		anAnimationDesc.spanCount = Me.theInputFileReader.ReadInt16()
	'		anAnimationDesc.spanOffset = Me.theInputFileReader.ReadInt32()
	'		anAnimationDesc.spanStallTime = Me.theInputFileReader.ReadSingle()

	'		'For x As Integer = 0 To anAnimationDesc.unknown.Length - 1
	'		'	anAnimationDesc.unknown(x) = Me.theInputFileReader.ReadInt32()
	'		'Next
	'		Me.theMdlFileData.theAnimationDescs.Add(anAnimationDesc)

	'		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		If anAnimationDesc.nameOffset <> 0 Then
	'			Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin)
	'			fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'			anAnimationDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)
	'			'If anAnimDesc.theName(0) = "@" Then
	'			'	anAnimDesc.theName = anAnimDesc.theName.Remove(0, 1)
	'			'End If

	'			'NOTE: This naming is found in Garry's Mod garrysmod_dir.vpk "\models\m_anm.mdl":  "a_../combine_soldier_xsi/Hold_AR2_base.smd"
	'			If anAnimationDesc.theName.StartsWith("a_../") OrElse anAnimationDesc.theName.StartsWith("a_..\") Then
	'				anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 5)
	'				anAnimationDesc.theName = Path.Combine(FileManager.GetPath(anAnimationDesc.theName), "a_" + Path.GetFileName(anAnimationDesc.theName))
	'			End If

	'			fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'			If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAnimationDesc.theName")
	'			End If
	'		Else
	'			anAnimationDesc.theName = ""
	'		End If

	'		If Me.theMdlFileData.theFirstAnimationDesc Is Nothing AndAlso anAnimationDesc.theName(0) <> "@" Then
	'			Me.theMdlFileData.theFirstAnimationDesc = anAnimationDesc
	'		End If

	'		'If anAnimationDesc.animBlock = 0 AndAlso ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0) Then
	'		If ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0) Then
	'			'If Me.theMdlFileData.version >= 49 AndAlso Me.theMdlFileData.version <> 2531 Then
	'			'	anAnimationDesc.theAniFrameAnims = New List(Of SourceAniFrameAnim)()
	'			'End If

	'			anAnimationDesc.theSectionsOfAnimations = New List(Of List(Of SourceMdlAnimation))()
	'			aSectionOfAnimation = New List(Of SourceMdlAnimation)()
	'			anAnimationDesc.theSectionsOfAnimations.Add(aSectionOfAnimation)

	'			If ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_FRAMEANIM) <> 0) Then
	'				'TODO: VERSION 49 data encoding
	'				'#define STUDIO_FRAMEANIM 0x0040		// animation is encoded as by frame x bone instead of RLE bone x frame

	'				If anAnimationDesc.sectionOffset <> 0 AndAlso anAnimationDesc.sectionFrameCount > 0 Then
	'				ElseIf anAnimationDesc.animBlock = 0 Then
	'				End If
	'			Else
	'				If anAnimationDesc.sectionOffset <> 0 AndAlso anAnimationDesc.sectionFrameCount > 0 Then
	'					Dim sectionFrameCount As Integer
	'					Dim sectionCount As Integer

	'					Me.theMdlFileData.theSectionFrameCount = anAnimationDesc.sectionFrameCount
	'					If Me.theMdlFileData.theSectionFrameMinFrameCount >= anAnimationDesc.frameCount Then
	'						Me.theMdlFileData.theSectionFrameMinFrameCount = anAnimationDesc.frameCount - 1
	'					End If

	'					''FROM: simplify.cpp:
	'					''      panim->numsections = (int)(panim->numframes / panim->sectionframes) + 2;
	'					''NOTE: It is unclear why "+ 2" is used in studiomdl.
	'					'sectionCount = CInt(Math.Truncate(anAnimationDesc.frameCount / anAnimationDesc.sectionFrameCount)) + 2
	'					sectionCount = CInt(Math.Truncate(anAnimationDesc.frameCount / anAnimationDesc.sectionFrameCount)) + 1
	'					'DEBUG:
	'					'sectionCount = 1

	'					'NOTE: First sectionOfAnimation was created above.
	'					For sectionIndex As Integer = 1 To sectionCount - 1
	'						aSectionOfAnimation = New List(Of SourceMdlAnimation)()
	'						anAnimationDesc.theSectionsOfAnimations.Add(aSectionOfAnimation)
	'					Next

	'					Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.sectionOffset, SeekOrigin.Begin)
	'					anAnimationDesc.theSections = New List(Of SourceMdlAnimationSection)(sectionCount)
	'					For sectionIndex As Integer = 0 To sectionCount - 1
	'						Me.ReadMdlAnimationSection(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, Me.theMdlFileData.theFileSeekLog)
	'					Next

	'					If anAnimationDesc.animBlock = 0 Then
	'						For sectionIndex As Integer = 0 To sectionCount - 1
	'							aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
	'							Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.theSections(sectionIndex).animOffset, SeekOrigin.Begin)

	'							If sectionIndex < sectionCount - 1 Then
	'								sectionFrameCount = anAnimationDesc.sectionFrameCount
	'							Else
	'								sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 1) * anAnimationDesc.sectionFrameCount)
	'							End If
	'							Me.ReadMdlAnimation(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, sectionFrameCount, aSectionOfAnimation)

	'							'NOTE: This is a guess based on pattern of extracted info.
	'							fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'							Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "anAnimationDesc.theAnimations alignment")
	'						Next
	'					End If
	'				ElseIf anAnimationDesc.animBlock = 0 Then
	'					Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.animOffset, SeekOrigin.Begin)
	'					Me.ReadMdlAnimation(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, anAnimationDesc.frameCount, anAnimationDesc.theSectionsOfAnimations(0))

	'					'NOTE: This is a guess based on pattern of extracted info.
	'					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'					Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "anAnimationDesc.theAnimations alignment")
	'				End If
	'			End If

	'			If anAnimationDesc.animBlock = 0 AndAlso anAnimationDesc.ikRuleCount > 0 Then
	'				Me.ReadMdlIkRules(animInputFileStreamPosition, anAnimationDesc)
	'			End If
	'			'ElseIf anAnimationDesc.animBlock > 0 Then
	'			'NOTE: This anim data is read from ANI file.
	'		End If

	'		If anAnimationDesc.movementCount > 0 Then
	'			Me.ReadMdlMovements(animInputFileStreamPosition, anAnimationDesc)
	'		End If

	'		If anAnimationDesc.localHierarchyCount > 0 Then
	'			Me.ReadLocalHierarchies(animInputFileStreamPosition, anAnimationDesc)
	'		End If

	'		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs")

	'	Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theAnimationDescs alignment")
	'End Sub

	'Protected Sub ReadMdlAnimation(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc, ByVal sectionFrameCount As Integer, ByVal aSectionOfAnimation As List(Of SourceMdlAnimation))
	'	Dim animationInputFileStreamPosition As Long
	'	Dim nextAnimationInputFileStreamPosition As Long
	'	Dim animValuePointerInputFileStreamPosition As Long
	'	Dim rotValuePointerInputFileStreamPosition As Long
	'	Dim posValuePointerInputFileStreamPosition As Long
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	'Dim fileOffsetStart2 As Long
	'	'Dim fileOffsetEnd2 As Long
	'	'Dim nextSetOfValuesOffset As Long
	'	'Dim lastFullAnimDataWasFound As Boolean
	'	Dim anAnimation As SourceMdlAnimation
	'	Dim boneCount As Integer
	'	Dim boneIndex As Byte

	'	'Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition, SeekOrigin.Begin)
	'	'fileOffsetStart = Me.theInputFileReader.BaseStream.Position
	'	fileOffsetStart = animInputFileStreamPosition

	'	'If anAnimationDesc.theAnimations Is Nothing Then
	'	'	anAnimationDesc.theAnimations = New List(Of SourceMdlAnimation)()
	'	'End If

	'	boneCount = Me.theMdlFileData.theBones.Count
	'	'lastFullAnimDataWasFound = False
	'	For j As Integer = 0 To boneCount - 1
	'		animationInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		boneIndex = Me.theInputFileReader.ReadByte()
	'		If boneIndex = 255 Then
	'			Me.theInputFileReader.ReadByte()
	'			Me.theInputFileReader.ReadInt16()
	'			Continue For
	'		End If
	'		'DEBUG:
	'		If boneIndex >= boneCount Then
	'			Dim badIfGetsHere As Integer = 42
	'			Exit For
	'		End If

	'		'''NOTE: If the offset is 0 then there are no more bone animation structures, so end the loop.
	'		''If anAnimation.nextSourceMdlAnimationOffset = 0 Then
	'		''	j = boneCount
	'		''End If
	'		'If lastFullAnimDataWasFound Then
	'		'	Exit For
	'		'End If

	'		'anAnimation = Nothing
	'		'For animationIndex As Integer = 0 To anAnimationDesc.theAnimations.Count - 1
	'		'	If anAnimationDesc.theAnimations(animationIndex).boneIndex = boneIndex Then
	'		'		anAnimation = anAnimationDesc.theAnimations(animationIndex)
	'		'		Exit For
	'		'	End If
	'		'Next
	'		'If anAnimation Is Nothing Then
	'		anAnimation = New SourceMdlAnimation()
	'		'anAnimationDesc.theAnimations.Add(anAnimation)
	'		aSectionOfAnimation.Add(anAnimation)
	'		'End If

	'		anAnimation.boneIndex = boneIndex
	'		'anAnimation = anAnimationDesc.theAnimations(boneIndex)
	'		anAnimation.flags = Me.theInputFileReader.ReadByte()
	'		anAnimation.nextSourceMdlAnimationOffset = Me.theInputFileReader.ReadInt16()

	'		'If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
	'		'End If
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
	'			anAnimation.theRot64bits = New SourceQuaternion64bits()
	'			anAnimation.theRot64bits.theBytes = Me.theInputFileReader.ReadBytes(8)

	'			'Me.DebugQuaternion(anAnimation.theRot64)
	'		End If
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
	'			anAnimation.theRot48bits = New SourceQuaternion48bits()
	'			'anAnimation.theRot48.theBytes = Me.theInputFileReader.ReadBytes(6)
	'			anAnimation.theRot48bits.theXInput = Me.theInputFileReader.ReadUInt16()
	'			anAnimation.theRot48bits.theYInput = Me.theInputFileReader.ReadUInt16()
	'			anAnimation.theRot48bits.theZWInput = Me.theInputFileReader.ReadUInt16()
	'		End If
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0 Then
	'			anAnimation.thePos = New SourceVector48bits()
	'			'anAnimation.thePos.theBytes = Me.theInputFileReader.ReadBytes(6)
	'			anAnimation.thePos.theXInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'			anAnimation.thePos.theYInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'			anAnimation.thePos.theZInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'		End If

	'		animValuePointerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		' First, read both sets of offsets.
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0 Then
	'			rotValuePointerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			'rotValuePointerInputFileStreamPosition = animValuePointerInputFileStreamPosition
	'			anAnimation.theRotV = New SourceMdlAnimationValuePointer()

	'			anAnimation.theRotV.animXValueOffset = Me.theInputFileReader.ReadInt16()
	'			If anAnimation.theRotV.theAnimXValues Is Nothing Then
	'				anAnimation.theRotV.theAnimXValues = New List(Of SourceMdlAnimationValue)()
	'			End If

	'			anAnimation.theRotV.animYValueOffset = Me.theInputFileReader.ReadInt16()
	'			If anAnimation.theRotV.theAnimYValues Is Nothing Then
	'				anAnimation.theRotV.theAnimYValues = New List(Of SourceMdlAnimationValue)()
	'			End If

	'			anAnimation.theRotV.animZValueOffset = Me.theInputFileReader.ReadInt16()
	'			If anAnimation.theRotV.theAnimZValues Is Nothing Then
	'				anAnimation.theRotV.theAnimZValues = New List(Of SourceMdlAnimationValue)()
	'			End If
	'		End If
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0 Then
	'			posValuePointerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			'posValuePointerInputFileStreamPosition = animValuePointerInputFileStreamPosition
	'			anAnimation.thePosV = New SourceMdlAnimationValuePointer()

	'			anAnimation.thePosV.animXValueOffset = Me.theInputFileReader.ReadInt16()
	'			If anAnimation.thePosV.theAnimXValues Is Nothing Then
	'				anAnimation.thePosV.theAnimXValues = New List(Of SourceMdlAnimationValue)()
	'			End If

	'			anAnimation.thePosV.animYValueOffset = Me.theInputFileReader.ReadInt16()
	'			If anAnimation.thePosV.theAnimYValues Is Nothing Then
	'				anAnimation.thePosV.theAnimYValues = New List(Of SourceMdlAnimationValue)()
	'			End If

	'			anAnimation.thePosV.animZValueOffset = Me.theInputFileReader.ReadInt16()
	'			If anAnimation.thePosV.theAnimZValues Is Nothing Then
	'				anAnimation.thePosV.theAnimZValues = New List(Of SourceMdlAnimationValue)()
	'			End If
	'		End If

	'		Me.theMdlFileData.theFileSeekLog.Add(animationInputFileStreamPosition, Me.theInputFileReader.BaseStream.Position - 1, "anAnimationDesc.anAnimation")

	'		' Second, read the anim values using the offsets.
	'		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0 Then
	'			If anAnimation.theRotV.animXValueOffset > 0 Then
	'				Me.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animXValueOffset, sectionFrameCount, anAnimation.theRotV.theAnimXValues, "anAnimation.theRotV.theAnimXValues")
	'			End If
	'			If anAnimation.theRotV.animYValueOffset > 0 Then
	'				Me.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animYValueOffset, sectionFrameCount, anAnimation.theRotV.theAnimYValues, "anAnimation.theRotV.theAnimYValues")
	'			End If
	'			If anAnimation.theRotV.animZValueOffset > 0 Then
	'				Me.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animZValueOffset, sectionFrameCount, anAnimation.theRotV.theAnimZValues, "anAnimation.theRotV.theAnimZValues")
	'			End If
	'		End If
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0 Then
	'			If anAnimation.thePosV.animXValueOffset > 0 Then
	'				Me.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animXValueOffset, sectionFrameCount, anAnimation.thePosV.theAnimXValues, "anAnimation.thePosV.theAnimXValues")
	'			End If
	'			If anAnimation.thePosV.animYValueOffset > 0 Then
	'				Me.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animYValueOffset, sectionFrameCount, anAnimation.thePosV.theAnimYValues, "anAnimation.thePosV.theAnimYValues")
	'			End If
	'			If anAnimation.thePosV.animZValueOffset > 0 Then
	'				Me.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animZValueOffset, sectionFrameCount, anAnimation.thePosV.theAnimZValues, "anAnimation.thePosV.theAnimZValues")
	'			End If
	'		End If
	'		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

	'		'anAnimationDesc.theAnimations.Add(anAnimation)

	'		'NOTE: If the offset is 0 then there are no more bone animation structures, so end the loop.
	'		If anAnimation.nextSourceMdlAnimationOffset = 0 Then
	'			'j = boneCount
	'			'lastFullAnimDataWasFound = True
	'			Exit For
	'		Else
	'			' Skip to next anim, just in case not all data is being read in.
	'			nextAnimationInputFileStreamPosition = animationInputFileStreamPosition + anAnimation.nextSourceMdlAnimationOffset
	'			''TEST: Use this with ANI file, so see if it extracts better.
	'			'nextAnimationInputFileStreamPosition = animationInputFileStreamPosition + CType(anAnimation.nextSourceMdlAnimationOffset, UShort)
	'			If nextAnimationInputFileStreamPosition < Me.theInputFileReader.BaseStream.Position Then
	'				'PROBLEM! Should not be going backwards in file.
	'				Dim i As Integer = 42
	'				Exit For
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(nextAnimationInputFileStreamPosition, SeekOrigin.Begin)
	'		End If
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	'If Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
	'	'	'NOTE: There are duplicates that do hit this line.
	'	'	Dim debug As Integer = 42
	'	'End If
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theSectionsOfAnimations [this includes other logged data offsets]")
	'End Sub

	''==========================================================
	''FROM: SourceEngine2007_source\utils\studiomdl\simplify.cpp
	''      Section within: static void CompressAnimations( ).
	''      This shows how the data is stored before being written to file.
	''memset( data, 0, sizeof( data ) ); 
	''pcount = data; 
	''pvalue = pcount + 1;
	''
	''pcount->num.valid = 1;
	''pcount->num.total = 1;
	''pvalue->value = value[0];
	''pvalue++;
	''
	''// build a RLE of deltas from the default pose
	''for (m = 1; m < n; m++)
	''{
	''	if (pcount->num.total == 255)
	''	{
	''		// chain too long, force a new entry
	''		pcount = pvalue;
	''		pvalue = pcount + 1;
	''		pcount->num.valid++;
	''		pvalue->value = value[m];
	''		pvalue++;
	''	} 
	''	// insert value if they're not equal, 
	''	// or if we're not on a run and the run is less than 3 units
	''	else if ((value[m] != value[m-1]) 
	''		|| ((pcount->num.total == pcount->num.valid) && ((m < n - 1) && value[m] != value[m+1])))
	''	{
	''		if (pcount->num.total != pcount->num.valid)
	''		{
	''			//if (j == 0) printf("%d:%d   ", pcount->num.valid, pcount->num.total ); 
	''			pcount = pvalue;
	''			pvalue = pcount + 1;
	''		}
	''		pcount->num.valid++;
	''		pvalue->value = value[m];
	''		pvalue++;
	''	}
	''	pcount->num.total++;
	''}
	''//if (j == 0) printf("%d:%d\n", pcount->num.valid, pcount->num.total ); 
	''
	''panim->anim[w][j].num[k] = pvalue - data;
	''if (panim->anim[w][j].num[k] == 2 && value[0] == 0)
	''{
	''	panim->anim[w][j].num[k] = 0;
	''}
	''else
	''{
	''	panim->anim[w][j].data[k] = (mstudioanimvalue_t *)kalloc( pvalue - data, sizeof( mstudioanimvalue_t ) );
	''	memmove( panim->anim[w][j].data[k], data, (pvalue - data) * sizeof( mstudioanimvalue_t ) );
	''}

	''=======================================================
	''FROM: SourceEngine2007_source\utils\studiomdl\write.cpp
	''      Section within: void WriteAnimationData( s_animation_t *srcanim, mstudioanimdesc_t *destanimdesc, byte *&pLocalData, byte *&pExtData ).
	''      This shows how the data is written to file.
	''mstudioanim_valueptr_t *posvptr	= NULL;
	''mstudioanim_valueptr_t *rotvptr	= NULL;
	''
	''// allocate room for rotation ptrs
	''rotvptr	= (mstudioanim_valueptr_t *)pData;
	''pData += sizeof( *rotvptr );
	''
	''// skip all position info if there's no animation
	''if (psrcdata->num[0] != 0 || psrcdata->num[1] != 0 || psrcdata->num[2] != 0)
	''{
	''	posvptr	= (mstudioanim_valueptr_t *)pData;
	''	pData += sizeof( *posvptr );
	''}
	''
	''mstudioanimvalue_t	*destanimvalue = (mstudioanimvalue_t *)pData;
	''
	''if (rotvptr)
	''{
	''	// store rotation animations
	''	for (k = 3; k < 6; k++)
	''	{
	''		if (psrcdata->num[k] == 0)
	''		{
	''			rotvptr->offset[k-3] = 0;
	''		}
	''		else
	''		{
	''			rotvptr->offset[k-3] = ((byte *)destanimvalue - (byte *)rotvptr);
	''			for (n = 0; n < psrcdata->num[k]; n++)
	''			{
	''				destanimvalue->value = psrcdata->data[k][n].value;
	''				destanimvalue++;
	''			}
	''		}
	''	}
	''	destanim->flags |= STUDIO_ANIM_ANIMROT;
	''}
	''
	''if (posvptr)
	''{
	''	// store position animations
	''	for (k = 0; k < 3; k++)
	''	{
	''		if (psrcdata->num[k] == 0)
	''		{
	''			posvptr->offset[k] = 0;
	''		}
	''		else
	''		{
	''			posvptr->offset[k] = ((byte *)destanimvalue - (byte *)posvptr);
	''			for (n = 0; n < psrcdata->num[k]; n++)
	''			{
	''				destanimvalue->value = psrcdata->data[k][n].value;
	''				destanimvalue++;
	''			}
	''		}
	''	}
	''	destanim->flags |= STUDIO_ANIM_ANIMPOS;
	''}
	''rawanimbytes += ((byte *)destanimvalue - pData);
	''pData = (byte *)destanimvalue;

	''===================================================
	''FROM: SourceEngine2007_source\public\bone_setup.cpp
	''      The ExtractAnimValue function shows how the values are extracted per frame from the data in the mdl file.
	''void ExtractAnimValue( int frame, mstudioanimvalue_t *panimvalue, float scale, float &v1 )
	''{
	''	if ( !panimvalue )
	''	{
	''		v1 = 0;
	''		return;
	''	}

	''	int k = frame;

	''	while (panimvalue->num.total <= k)
	''	{
	''		k -= panimvalue->num.total;
	''		panimvalue += panimvalue->num.valid + 1;
	''		if ( panimvalue->num.total == 0 )
	''		{
	''			Assert( 0 ); // running off the end of the animation stream is bad
	''			v1 = 0;
	''			return;
	''		}
	''	}
	''	if (panimvalue->num.valid > k)
	''	{
	''		v1 = panimvalue[k+1].value * scale;
	''	}
	''	else
	''	{
	''		// get last valid data block
	''		v1 = panimvalue[panimvalue->num.valid].value * scale;
	''	}
	''}
	'Private Sub ReadMdlAnimValues(ByVal animValuesInputFileStreamPosition As Long, ByVal frameCount As Integer, ByVal theAnimValues As List(Of SourceMdlAnimationValue), ByVal debugDescription As String)
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	Dim frameCountRemainingToBeChecked As Integer
	'	Dim animValue As New SourceMdlAnimationValue()
	'	Dim currentTotal As Byte
	'	Dim validCount As Byte

	'	Me.theInputFileReader.BaseStream.Seek(animValuesInputFileStreamPosition, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	frameCountRemainingToBeChecked = frameCount
	'	While (frameCountRemainingToBeChecked > 0)
	'		animValue.value = Me.theInputFileReader.ReadInt16()
	'		currentTotal = animValue.total
	'		If currentTotal = 0 Then
	'			Dim badIfThisIsReached As Integer = 42
	'			Exit While
	'		End If
	'		frameCountRemainingToBeChecked -= currentTotal
	'		theAnimValues.Add(animValue)

	'		validCount = animValue.valid
	'		For i As Integer = 1 To validCount
	'			animValue.value = Me.theInputFileReader.ReadInt16()
	'			theAnimValues.Add(animValue)
	'		Next
	'	End While

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, debugDescription)
	'End Sub

	''Private Sub DebugQuaternion(ByVal q As SourceQuaternion64)
	''	Dim sqx As Double = q.X * q.X
	''	Dim sqy As Double = q.Y * q.Y
	''	Dim sqz As Double = q.Z * q.Z
	''	Dim sqw As Double = q.W * q.W

	''	' If quaternion is normalised the unit is one, otherwise it is the correction factor
	''	Dim unit As Double = sqx + sqy + sqz + sqw
	''	If unit = 1 Then
	''		Dim i As Integer = 42
	''	ElseIf unit = -1 Then
	''		Dim i As Integer = 42
	''	Else
	''		Dim i As Integer = 42
	''	End If

	''End Sub

	'Protected Sub ReadMdlIkRules(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc)
	'	Dim ikRuleInputFileStreamPosition As Long
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	Dim fileOffsetStart2 As Long
	'	Dim fileOffsetEnd2 As Long

	'	Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.ikRuleOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	anAnimationDesc.theIkRules = New List(Of SourceMdlIkRule)(anAnimationDesc.ikRuleCount)
	'	For j As Integer = 0 To anAnimationDesc.ikRuleCount - 1
	'		ikRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim anIkRule As New SourceMdlIkRule()

	'		anIkRule.index = Me.theInputFileReader.ReadInt32()
	'		anIkRule.type = Me.theInputFileReader.ReadInt32()
	'		anIkRule.chain = Me.theInputFileReader.ReadInt32()
	'		anIkRule.bone = Me.theInputFileReader.ReadInt32()

	'		anIkRule.slot = Me.theInputFileReader.ReadInt32()
	'		anIkRule.height = Me.theInputFileReader.ReadSingle()
	'		anIkRule.radius = Me.theInputFileReader.ReadSingle()
	'		anIkRule.floor = Me.theInputFileReader.ReadSingle()

	'		anIkRule.pos = New SourceVector()
	'		anIkRule.pos.x = Me.theInputFileReader.ReadSingle()
	'		anIkRule.pos.y = Me.theInputFileReader.ReadSingle()
	'		anIkRule.pos.z = Me.theInputFileReader.ReadSingle()
	'		anIkRule.q = New SourceQuaternion()
	'		anIkRule.q.x = Me.theInputFileReader.ReadSingle()
	'		anIkRule.q.y = Me.theInputFileReader.ReadSingle()
	'		anIkRule.q.z = Me.theInputFileReader.ReadSingle()
	'		anIkRule.q.w = Me.theInputFileReader.ReadSingle()

	'		anIkRule.compressedIkErrorOffset = Me.theInputFileReader.ReadInt32()
	'		anIkRule.unused2 = Me.theInputFileReader.ReadInt32()
	'		anIkRule.ikErrorIndexStart = Me.theInputFileReader.ReadInt32()
	'		anIkRule.ikErrorOffset = Me.theInputFileReader.ReadInt32()

	'		anIkRule.influenceStart = Me.theInputFileReader.ReadSingle()
	'		anIkRule.influencePeak = Me.theInputFileReader.ReadSingle()
	'		anIkRule.influenceTail = Me.theInputFileReader.ReadSingle()
	'		anIkRule.influenceEnd = Me.theInputFileReader.ReadSingle()

	'		anIkRule.unused3 = Me.theInputFileReader.ReadSingle()
	'		anIkRule.contact = Me.theInputFileReader.ReadSingle()
	'		anIkRule.drop = Me.theInputFileReader.ReadSingle()
	'		anIkRule.top = Me.theInputFileReader.ReadSingle()

	'		anIkRule.unused6 = Me.theInputFileReader.ReadInt32()
	'		anIkRule.unused7 = Me.theInputFileReader.ReadInt32()
	'		anIkRule.unused8 = Me.theInputFileReader.ReadInt32()

	'		anIkRule.attachmentNameOffset = Me.theInputFileReader.ReadInt32()

	'		For x As Integer = 0 To anIkRule.unused.Length - 1
	'			anIkRule.unused(x) = Me.theInputFileReader.ReadInt32()
	'		Next

	'		anAnimationDesc.theIkRules.Add(anIkRule)

	'		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		If anIkRule.attachmentNameOffset <> 0 Then
	'			Me.theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.attachmentNameOffset, SeekOrigin.Begin)
	'			fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'			anIkRule.theAttachmentName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'			fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'			If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkRule.theAttachmentName")
	'			End If
	'		Else
	'			anIkRule.theAttachmentName = ""
	'		End If

	'		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theIkRules")

	'	Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "anAnimationDesc.theIkRules alignment")
	'End Sub

	'Protected Sub ReadMdlAnimationSection(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc, ByVal aFileSeekLog As FileSeekLog)
	'	Dim animSectionInputFileStreamPosition As Long
	'	'Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	'Dim fileOffsetStart2 As Long
	'	'Dim fileOffsetEnd2 As Long

	'	fileOffsetStart = animInputFileStreamPosition

	'	animSectionInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'	Dim anAnimSection As New SourceMdlAnimationSection()
	'	anAnimSection.animBlock = Me.theInputFileReader.ReadInt32()
	'	anAnimSection.animOffset = Me.theInputFileReader.ReadInt32()
	'	anAnimationDesc.theSections.Add(anAnimSection)

	'	'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'	'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	aFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theSections")
	'End Sub

	'Protected Sub ReadMdlMovements(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc)
	'	Dim movementInputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.movementOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	anAnimationDesc.theMovements = New List(Of SourceMdlMovement)(anAnimationDesc.movementCount)
	'	For j As Integer = 0 To anAnimationDesc.movementCount - 1
	'		movementInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim aMovement As New SourceMdlMovement()

	'		aMovement.endframeIndex = Me.theInputFileReader.ReadInt32()
	'		aMovement.motionFlags = Me.theInputFileReader.ReadInt32()
	'		aMovement.v0 = Me.theInputFileReader.ReadSingle()
	'		aMovement.v1 = Me.theInputFileReader.ReadSingle()
	'		aMovement.angle = Me.theInputFileReader.ReadSingle()

	'		aMovement.vector = New SourceVector()
	'		aMovement.vector.x = Me.theInputFileReader.ReadSingle()
	'		aMovement.vector.y = Me.theInputFileReader.ReadSingle()
	'		aMovement.vector.z = Me.theInputFileReader.ReadSingle()
	'		aMovement.position = New SourceVector()
	'		aMovement.position.x = Me.theInputFileReader.ReadSingle()
	'		aMovement.position.y = Me.theInputFileReader.ReadSingle()
	'		aMovement.position.z = Me.theInputFileReader.ReadSingle()

	'		anAnimationDesc.theMovements.Add(aMovement)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theMovements")

	'	Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "anAnimationDesc.theMovements alignment")
	'End Sub

	'Protected Sub ReadLocalHierarchies(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc)
	'	Dim localHieararchyInputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.localHierarchyOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	anAnimationDesc.theLocalHierarchies = New List(Of SourceMdlLocalHierarchy)(anAnimationDesc.localHierarchyCount)
	'	For j As Integer = 0 To anAnimationDesc.localHierarchyCount - 1
	'		localHieararchyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim aLocalHierarchy As New SourceMdlLocalHierarchy()

	'		aLocalHierarchy.boneIndex = Me.theInputFileReader.ReadInt32()
	'		aLocalHierarchy.boneNewParentIndex = Me.theInputFileReader.ReadInt32()
	'		aLocalHierarchy.startInfluence = Me.theInputFileReader.ReadSingle()
	'		aLocalHierarchy.peakInfluence = Me.theInputFileReader.ReadSingle()
	'		aLocalHierarchy.tailInfluence = Me.theInputFileReader.ReadSingle()
	'		aLocalHierarchy.endInfluence = Me.theInputFileReader.ReadSingle()
	'		aLocalHierarchy.startFrameIndex = Me.theInputFileReader.ReadInt32()
	'		aLocalHierarchy.localAnimOffset = Me.theInputFileReader.ReadInt32()
	'		For x As Integer = 0 To aLocalHierarchy.unused.Length - 1
	'			aLocalHierarchy.unused(x) = Me.theInputFileReader.ReadInt32()
	'		Next

	'		anAnimationDesc.theLocalHierarchies.Add(aLocalHierarchy)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theLocalHierarchies")

	'	Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "anAnimationDesc.theLocalHierarchies alignment")
	'End Sub

	'Private Sub ReadSequenceDescs()
	'	Dim seqInputFileStreamPosition As Long
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	Dim fileOffsetStart2 As Long
	'	Dim fileOffsetEnd2 As Long

	'	Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localSequenceOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	Me.theMdlFileData.theSequenceDescs = New List(Of SourceMdlSequenceDesc)(Me.theMdlFileData.localSequenceCount)
	'	For i As Integer = 0 To Me.theMdlFileData.localSequenceCount - 1
	'		seqInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim aSeqDesc As New SourceMdlSequenceDesc()
	'		aSeqDesc.baseHeaderOffset = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.nameOffset = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.activityNameOffset = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.flags = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.activity = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.activityWeight = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.eventCount = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.eventOffset = Me.theInputFileReader.ReadInt32()

	'		aSeqDesc.bbMin.x = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.bbMin.y = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.bbMin.z = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.bbMax.x = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.bbMax.y = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.bbMax.z = Me.theInputFileReader.ReadSingle()

	'		aSeqDesc.blendCount = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.animIndexOffset = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.movementIndex = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.groupSize(0) = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.groupSize(1) = Me.theInputFileReader.ReadInt32()

	'		aSeqDesc.paramIndex(0) = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.paramIndex(1) = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.paramStart(0) = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.paramStart(1) = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.paramEnd(0) = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.paramEnd(1) = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.paramParent = Me.theInputFileReader.ReadInt32()

	'		aSeqDesc.fadeInTime = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.fadeOutTime = Me.theInputFileReader.ReadSingle()

	'		aSeqDesc.localEntryNodeIndex = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.localExitNodeIndex = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.nodeFlags = Me.theInputFileReader.ReadInt32()

	'		aSeqDesc.entryPhase = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.exitPhase = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.lastFrame = Me.theInputFileReader.ReadSingle()

	'		aSeqDesc.nextSeq = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.pose = Me.theInputFileReader.ReadInt32()

	'		aSeqDesc.ikRuleCount = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.autoLayerCount = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.autoLayerOffset = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.weightOffset = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.poseKeyOffset = Me.theInputFileReader.ReadInt32()

	'		aSeqDesc.ikLockCount = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.ikLockOffset = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.keyValueOffset = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.keyValueSize = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.cyclePoseIndex = Me.theInputFileReader.ReadInt32()

	'		aSeqDesc.activityModifierOffset = 0
	'		aSeqDesc.activityModifierCount = 0
	'		If Me.theMdlFileData.version = 49 Then
	'			aSeqDesc.activityModifierOffset = Me.theInputFileReader.ReadInt32()
	'			aSeqDesc.activityModifierCount = Me.theInputFileReader.ReadInt32()
	'			For x As Integer = 0 To 4
	'				aSeqDesc.unused(x) = Me.theInputFileReader.ReadInt32()
	'			Next
	'		Else
	'			For x As Integer = 0 To 6
	'				aSeqDesc.unused(x) = Me.theInputFileReader.ReadInt32()
	'			Next
	'		End If

	'		Me.theMdlFileData.theSequenceDescs.Add(aSeqDesc)

	'		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		If aSeqDesc.nameOffset <> 0 Then
	'			Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.nameOffset, SeekOrigin.Begin)
	'			fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'			aSeqDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'			fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theLabel")
	'		Else
	'			aSeqDesc.theName = ""
	'		End If
	'		If aSeqDesc.activityNameOffset <> 0 Then
	'			Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.activityNameOffset, SeekOrigin.Begin)
	'			fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'			aSeqDesc.theActivityName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'			fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'			If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theActivityName")
	'			End If
	'		Else
	'			aSeqDesc.theActivityName = ""
	'		End If
	'		If (aSeqDesc.groupSize(0) > 1 OrElse aSeqDesc.groupSize(1) > 1) AndAlso aSeqDesc.poseKeyOffset <> 0 Then
	'			Me.ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc)
	'		End If
	'		If aSeqDesc.eventCount > 0 AndAlso aSeqDesc.eventOffset <> 0 Then
	'			Me.ReadEvents(seqInputFileStreamPosition, aSeqDesc)
	'		End If
	'		If aSeqDesc.autoLayerCount > 0 AndAlso aSeqDesc.autoLayerOffset <> 0 Then
	'			Me.ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc)
	'		End If
	'		If Me.theMdlFileData.boneCount > 0 AndAlso aSeqDesc.weightOffset > 0 Then
	'			Me.ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc)
	'		End If
	'		If aSeqDesc.ikLockCount > 0 AndAlso aSeqDesc.ikLockOffset <> 0 Then
	'			Me.ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc)
	'		End If
	'		If (aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)) > 0 AndAlso aSeqDesc.animIndexOffset <> 0 Then
	'			Me.ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc)
	'		End If
	'		If aSeqDesc.keyValueSize > 0 AndAlso aSeqDesc.keyValueOffset <> 0 Then
	'			Me.ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc)
	'		End If
	'		If aSeqDesc.activityModifierCount <> 0 AndAlso aSeqDesc.activityModifierOffset <> 0 Then
	'			Me.ReadActivityModifiers(seqInputFileStreamPosition, aSeqDesc)
	'		End If

	'		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceDescs")
	'End Sub

	'Private Sub ReadPoseKeys(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
	'	Dim poseKeyCount As Integer
	'	poseKeyCount = aSeqDesc.groupSize(0) + aSeqDesc.groupSize(1)
	'	Dim poseKeyInputFileStreamPosition As Long
	'	'Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.poseKeyOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	aSeqDesc.thePoseKeys = New List(Of Double)(poseKeyCount)
	'	For j As Integer = 0 To poseKeyCount - 1
	'		poseKeyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim aPoseKey As Double
	'		aPoseKey = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.thePoseKeys.Add(aPoseKey)

	'		'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.thePoseKeys")
	'End Sub

	'Private Sub ReadEvents(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
	'	Dim eventCount As Integer
	'	eventCount = aSeqDesc.eventCount
	'	Dim eventInputFileStreamPosition As Long
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	Dim fileOffsetStart2 As Long
	'	Dim fileOffsetEnd2 As Long

	'	Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	aSeqDesc.theEvents = New List(Of SourceMdlEvent)(eventCount)
	'	For j As Integer = 0 To eventCount - 1
	'		eventInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim anEvent As New SourceMdlEvent()
	'		anEvent.cycle = Me.theInputFileReader.ReadSingle()
	'		anEvent.eventIndex = Me.theInputFileReader.ReadInt32()
	'		anEvent.eventType = Me.theInputFileReader.ReadInt32()
	'		For x As Integer = 0 To anEvent.options.Length - 1
	'			anEvent.options(x) = Me.theInputFileReader.ReadChar()
	'		Next
	'		anEvent.nameOffset = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.theEvents.Add(anEvent)

	'		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		'if ( isdigit( g_sequence[i].event[j].eventname[0] ) )
	'		'{
	'		'	 pevent[j].event = atoi( g_sequence[i].event[j].eventname );
	'		'	 pevent[j].type = 0;
	'		'	 pevent[j].szeventindex = 0;
	'		'}
	'		'Else
	'		'{
	'		'	 AddToStringTable( &pevent[j], &pevent[j].szeventindex, g_sequence[i].event[j].eventname );
	'		'	 pevent[j].type = NEW_EVENT_STYLE;
	'		'}
	'		If anEvent.nameOffset <> 0 Then
	'			Me.theInputFileReader.BaseStream.Seek(eventInputFileStreamPosition + anEvent.nameOffset, SeekOrigin.Begin)
	'			fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'			anEvent.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'			fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'			If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEvent.theName")
	'			End If
	'		Else
	'			'anEvent.theName = ""
	'			anEvent.theName = anEvent.eventIndex.ToString()
	'		End If

	'		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theEvents")

	'	Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "aSeqDesc.theEvents alignment")
	'End Sub

	'Private Sub ReadAutoLayers(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
	'	Dim autoLayerCount As Integer
	'	autoLayerCount = aSeqDesc.autoLayerCount
	'	Dim autoLayerInputFileStreamPosition As Long
	'	'Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.autoLayerOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	aSeqDesc.theAutoLayers = New List(Of SourceMdlAutoLayer)(autoLayerCount)
	'	For j As Integer = 0 To autoLayerCount - 1
	'		autoLayerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim anAutoLayer As New SourceMdlAutoLayer()
	'		anAutoLayer.sequenceIndex = Me.theInputFileReader.ReadInt16()
	'		anAutoLayer.poseIndex = Me.theInputFileReader.ReadInt16()
	'		anAutoLayer.flags = Me.theInputFileReader.ReadInt32()
	'		anAutoLayer.influenceStart = Me.theInputFileReader.ReadSingle()
	'		anAutoLayer.influencePeak = Me.theInputFileReader.ReadSingle()
	'		anAutoLayer.influenceTail = Me.theInputFileReader.ReadSingle()
	'		anAutoLayer.influenceEnd = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.theAutoLayers.Add(anAutoLayer)

	'		'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAutoLayers")
	'End Sub

	'Private Sub ReadMdlAnimBoneWeights(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
	'	Dim weightListInputFileStreamPosition As Long
	'	'Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	'Dim fileOffsetStart2 As Long
	'	'Dim fileOffsetEnd2 As Long

	'	Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.weightOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	aSeqDesc.theBoneWeightsAreDefault = True
	'	aSeqDesc.theBoneWeights = New List(Of Double)(Me.theMdlFileData.boneCount)
	'	For j As Integer = 0 To Me.theMdlFileData.boneCount - 1
	'		weightListInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim anAnimBoneWeight As Double
	'		anAnimBoneWeight = Me.theInputFileReader.ReadSingle()
	'		aSeqDesc.theBoneWeights.Add(anAnimBoneWeight)

	'		If anAnimBoneWeight <> 1 Then
	'			aSeqDesc.theBoneWeightsAreDefault = False
	'		End If

	'		'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	'NOTE: A sequence can point to same weights as another.
	'	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theBoneWeights")
	'	End If
	'End Sub

	'Private Sub ReadSequenceIkLocks(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
	'	Dim lockCount As Integer
	'	lockCount = aSeqDesc.ikLockCount
	'	Dim lockInputFileStreamPosition As Long
	'	'Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.ikLockOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	aSeqDesc.theIkLocks = New List(Of SourceMdlIkLock)(lockCount)
	'	For j As Integer = 0 To lockCount - 1
	'		lockInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim anIkLock As New SourceMdlIkLock()
	'		anIkLock.chainIndex = Me.theInputFileReader.ReadInt32()
	'		anIkLock.posWeight = Me.theInputFileReader.ReadSingle()
	'		anIkLock.localQWeight = Me.theInputFileReader.ReadSingle()
	'		anIkLock.flags = Me.theInputFileReader.ReadInt32()
	'		For x As Integer = 0 To anIkLock.unused.Length - 1
	'			anIkLock.unused(x) = Me.theInputFileReader.ReadInt32()
	'		Next
	'		aSeqDesc.theIkLocks.Add(anIkLock)

	'		'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theIkLocks")
	'End Sub

	'Private Sub ReadMdlAnimIndexes(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
	'	Dim animIndexCount As Integer
	'	animIndexCount = aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)
	'	Dim animIndexInputFileStreamPosition As Long
	'	'Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.animIndexOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	aSeqDesc.theAnimDescIndexes = New List(Of Short)(animIndexCount)
	'	For j As Integer = 0 To animIndexCount - 1
	'		animIndexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim anAnimIndex As Short
	'		anAnimIndex = Me.theInputFileReader.ReadInt16()
	'		aSeqDesc.theAnimDescIndexes.Add(anAnimIndex)

	'		'NOTE: Set this boolean for use in writing lines in qc file.
	'		Me.theMdlFileData.theAnimationDescs(anAnimIndex).theAnimIsLinkedToSequence = True
	'		Me.theMdlFileData.theAnimationDescs(anAnimIndex).theLinkedSequences.Add(aSeqDesc)

	'		'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	'TODO: A sequence can point to same anims as another?
	'	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAnimDescIndexes")
	'	End If

	'	Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "aSeqDesc.theAnimDescIndexes alignment")
	'End Sub

	'Private Sub ReadSequenceKeyValues(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.keyValueOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	aSeqDesc.theKeyValues = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theKeyValues")

	'	Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "aSeqDesc.theKeyValues alignment")
	'End Sub

	'Private Sub ReadActivityModifiers(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
	'	Dim activityModifierCount As Integer
	'	Dim activityModifierInputFileStreamPosition As Long
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	Dim fileOffsetStart2 As Long
	'	Dim fileOffsetEnd2 As Long

	'	Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.activityModifierOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	activityModifierCount = aSeqDesc.activityModifierCount
	'	aSeqDesc.theActivityModifiers = New List(Of SourceMdlActivityModifier)(activityModifierCount)
	'	For j As Integer = 0 To activityModifierCount - 1
	'		activityModifierInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim anActivityModifier As New SourceMdlActivityModifier()
	'		anActivityModifier.nameOffset = Me.theInputFileReader.ReadInt32()
	'		aSeqDesc.theActivityModifiers.Add(anActivityModifier)

	'		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		If anActivityModifier.nameOffset <> 0 Then
	'			Me.theInputFileReader.BaseStream.Seek(activityModifierInputFileStreamPosition + anActivityModifier.nameOffset, SeekOrigin.Begin)
	'			fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'			anActivityModifier.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'			fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'			If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anActivityModifier.theName")
	'			End If
	'		Else
	'			anActivityModifier.theName = ""
	'		End If

	'		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theActivityModifiers")

	'	'Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "aSeqDesc.theActivityModifiers alignment")
	'End Sub

	'Private Sub ReadLocalNodeNames()
	'	'	// save transition graph
	'	'	int *pxnodename = (int *)pData;
	'	'	phdr->localnodenameindex = (pData - pStart);
	'	'	pData += g_numxnodes * sizeof( *pxnodename );
	'	'	ALIGN4( pData );
	'	'	for (i = 0; i < g_numxnodes; i++)
	'	'	{
	'	'		AddToStringTable( phdr, pxnodename, g_xnodename[i+1] );
	'	'		// printf("%d : %s\n", i, g_xnodename[i+1] );
	'	'		pxnodename++;
	'	'	}
	'	If Me.theMdlFileData.localNodeCount > 0 AndAlso Me.theMdlFileData.localNodeNameOffset <> 0 Then
	'		Dim localNodeNameInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localNodeNameOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theLocalNodeNames = New List(Of String)(Me.theMdlFileData.localNodeCount)
	'		Dim localNodeNameOffset As Integer
	'		For i As Integer = 0 To Me.theMdlFileData.localNodeCount - 1
	'			localNodeNameInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aLocalNodeName As String
	'			localNodeNameOffset = Me.theInputFileReader.ReadInt32()

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If localNodeNameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(localNodeNameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aLocalNodeName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aLocalNodeName")
	'				End If
	'			Else
	'				aLocalNodeName = ""
	'			End If
	'			Me.theMdlFileData.theLocalNodeNames.Add(aLocalNodeName)

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theLocalNodeNames")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theLocalNodeNames alignment")
	'	End If
	'End Sub

	''TODO: 
	'Private Sub ReadLocalNodes()
	'	'	ptransition	= (byte *)pData;
	'	'	phdr->numlocalnodes = IsChar( g_numxnodes );
	'	'	phdr->localnodeindex = IsInt24( pData - pStart );
	'	'	pData += g_numxnodes * g_numxnodes * sizeof( byte );
	'	'	ALIGN4( pData );
	'	'	for (i = 0; i < g_numxnodes; i++)
	'	'	{
	'	'//		printf("%2d (%12s) : ", i + 1, g_xnodename[i+1] );
	'	'		for (j = 0; j < g_numxnodes; j++)
	'	'		{
	'	'			*ptransition++ = g_xnode[i][j];
	'	'//			printf(" %2d", g_xnode[i][j] );
	'	'		}
	'	'//		printf("\n" );
	'	'	}
	'End Sub

	'Private Sub ReadBodyParts()
	'	If Me.theMdlFileData.bodyPartCount > 0 Then
	'		Dim bodyPartInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.bodyPartOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theBodyParts = New List(Of SourceMdlBodyPart)(Me.theMdlFileData.bodyPartCount)
	'		For i As Integer = 0 To Me.theMdlFileData.bodyPartCount - 1
	'			bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aBodyPart As New SourceMdlBodyPart()
	'			aBodyPart.nameOffset = Me.theInputFileReader.ReadInt32()
	'			aBodyPart.modelCount = Me.theInputFileReader.ReadInt32()
	'			aBodyPart.base = Me.theInputFileReader.ReadInt32()
	'			aBodyPart.modelOffset = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.theBodyParts.Add(aBodyPart)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If aBodyPart.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aBodyPart.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBodyPart.theName")
	'				End If
	'			Else
	'				aBodyPart.theName = ""
	'			End If

	'			Me.ReadModels(bodyPartInputFileStreamPosition, aBodyPart)
	'			'NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts and models.
	'			If i = Me.theMdlFileData.bodyPartCount - 1 Then
	'				Me.LogToEndAndAlignToNextStart(Me.theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment")
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts")
	'	End If
	'End Sub

	'Private Sub ReadModels(ByVal bodyPartInputFileStreamPosition As Long, ByVal aBodyPart As SourceMdlBodyPart)
	'	If aBodyPart.modelCount > 0 Then
	'		Dim modelInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		'Dim fileOffsetStart2 As Long
	'		'Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		aBodyPart.theModels = New List(Of SourceMdlModel)(aBodyPart.modelCount)
	'		For j As Integer = 0 To aBodyPart.modelCount - 1
	'			modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aModel As New SourceMdlModel()

	'			If Me.theMdlFileData.version = 2531 Then
	'				Me.ReadModelVersion2531(aModel)
	'			Else
	'				aModel.name = Me.theInputFileReader.ReadChars(64)
	'				aModel.type = Me.theInputFileReader.ReadInt32()
	'				aModel.boundingRadius = Me.theInputFileReader.ReadSingle()
	'				aModel.meshCount = Me.theInputFileReader.ReadInt32()
	'				aModel.meshOffset = Me.theInputFileReader.ReadInt32()
	'				aModel.vertexCount = Me.theInputFileReader.ReadInt32()
	'				aModel.vertexOffset = Me.theInputFileReader.ReadInt32()
	'				aModel.tangentOffset = Me.theInputFileReader.ReadInt32()
	'				aModel.attachmentCount = Me.theInputFileReader.ReadInt32()
	'				aModel.attachmentOffset = Me.theInputFileReader.ReadInt32()
	'				aModel.eyeballCount = Me.theInputFileReader.ReadInt32()
	'				aModel.eyeballOffset = Me.theInputFileReader.ReadInt32()
	'				Dim modelVertexData As New SourceMdlModelVertexData()
	'				modelVertexData.vertexDataP = Me.theInputFileReader.ReadInt32()
	'				modelVertexData.tangentDataP = Me.theInputFileReader.ReadInt32()
	'				aModel.vertexData = modelVertexData
	'				For x As Integer = 0 To 7
	'					aModel.unused(x) = Me.theInputFileReader.ReadInt32()
	'				Next
	'			End If

	'			aBodyPart.theModels.Add(aModel)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			'NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
	'			Me.ReadEyeballs(modelInputFileStreamPosition, aModel)
	'			Me.ReadMeshes(modelInputFileStreamPosition, aModel)

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels")
	'	End If
	'End Sub

	'Private Sub ReadModelVersion2531(ByRef aModel As SourceMdlModel)
	'	aModel.name = Me.theInputFileReader.ReadChars(64)
	'	aModel.type = Me.theInputFileReader.ReadInt32()
	'	aModel.boundingRadius = Me.theInputFileReader.ReadSingle()

	'	aModel.attachmentCount = Me.theInputFileReader.ReadInt32()
	'	aModel.attachmentOffset = Me.theInputFileReader.ReadInt32()
	'	aModel.eyeballCount = Me.theInputFileReader.ReadInt32()
	'	aModel.eyeballOffset = Me.theInputFileReader.ReadInt32()

	'	Dim modelVertexData As New SourceMdlModelVertexData()
	'	modelVertexData.vertexDataP = Me.theInputFileReader.ReadInt32()
	'	modelVertexData.tangentDataP = Me.theInputFileReader.ReadInt32()
	'	aModel.vertexData = modelVertexData

	'	'FROM: MDLConverter for VtMB
	'	'// - BKH - Sep 7, 2012 - Increased padding size to adjust for alignment issues
	'	'int unused[10]; // remove as appropriate
	'	For x As Integer = 0 To 9
	'		Me.theInputFileReader.ReadInt32()
	'	Next

	'	aModel.meshCount = Me.theInputFileReader.ReadInt32()
	'	aModel.meshOffset = Me.theInputFileReader.ReadInt32()
	'	aModel.vertexCount = Me.theInputFileReader.ReadInt32()
	'	aModel.vertexOffset = Me.theInputFileReader.ReadInt32()
	'	aModel.tangentOffset = Me.theInputFileReader.ReadInt32()

	'	aModel.vertexListType = Me.theInputFileReader.ReadInt32()
	'End Sub

	'Private Sub ReadMeshes(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel)
	'	If aModel.meshCount > 0 AndAlso aModel.meshOffset <> 0 Then
	'		aModel.theMeshes = New List(Of SourceMdlMesh)(aModel.meshCount)
	'		Dim meshInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		'Dim fileOffsetStart2 As Long
	'		'Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.meshOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		For meshIndex As Integer = 0 To aModel.meshCount - 1
	'			meshInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aMesh As New SourceMdlMesh()

	'			If Me.theMdlFileData.version = 2531 Then
	'				Me.ReadMeshVersion2531(aMesh)
	'			Else
	'				aMesh.materialIndex = Me.theInputFileReader.ReadInt32()
	'				aMesh.modelOffset = Me.theInputFileReader.ReadInt32()
	'				aMesh.vertexCount = Me.theInputFileReader.ReadInt32()
	'				aMesh.vertexIndexStart = Me.theInputFileReader.ReadInt32()
	'				aMesh.flexCount = Me.theInputFileReader.ReadInt32()
	'				aMesh.flexOffset = Me.theInputFileReader.ReadInt32()
	'				aMesh.materialType = Me.theInputFileReader.ReadInt32()
	'				aMesh.materialParam = Me.theInputFileReader.ReadInt32()
	'				aMesh.id = Me.theInputFileReader.ReadInt32()
	'				aMesh.centerX = Me.theInputFileReader.ReadSingle()
	'				aMesh.centerY = Me.theInputFileReader.ReadSingle()
	'				aMesh.centerZ = Me.theInputFileReader.ReadSingle()
	'				Dim meshVertexData As New SourceMdlMeshVertexData()
	'				meshVertexData.modelVertexDataP = Me.theInputFileReader.ReadInt32()
	'				For x As Integer = 0 To MAX_NUM_LODS - 1
	'					meshVertexData.lodVertexCount(x) = Me.theInputFileReader.ReadInt32()
	'				Next
	'				aMesh.vertexData = meshVertexData
	'				For x As Integer = 0 To 7
	'					aMesh.unused(x) = Me.theInputFileReader.ReadInt32()
	'				Next
	'			End If

	'			aModel.theMeshes.Add(aMesh)

	'			' Fill-in eyeball texture index info.
	'			If aMesh.materialType = 1 Then
	'				aModel.theEyeballs(aMesh.materialParam).theTextureIndex = aMesh.materialIndex
	'			End If

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If aMesh.flexCount > 0 AndAlso aMesh.flexOffset <> 0 Then
	'				Me.ReadFlexes(meshInputFileStreamPosition, aMesh)
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theMeshes")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "aModel.theMeshes alignment")
	'	End If
	'End Sub

	'Private Sub ReadMeshVersion2531(ByRef aMesh As SourceMdlMesh)
	'	aMesh.materialIndex = Me.theInputFileReader.ReadInt32()
	'	aMesh.modelOffset = Me.theInputFileReader.ReadInt32()
	'	aMesh.vertexCount = Me.theInputFileReader.ReadInt32()
	'	aMesh.vertexIndexStart = Me.theInputFileReader.ReadInt32()
	'	aMesh.flexCount = Me.theInputFileReader.ReadInt32()
	'	aMesh.flexOffset = Me.theInputFileReader.ReadInt32()

	'	Dim meshVertexData As New SourceMdlMeshVertexData()
	'	meshVertexData.modelVertexDataP = Me.theInputFileReader.ReadInt32()
	'	For x As Integer = 0 To MAX_NUM_LODS - 1
	'		meshVertexData.lodVertexCount(x) = Me.theInputFileReader.ReadInt32()
	'	Next
	'	aMesh.vertexData = meshVertexData
	'End Sub

	'Private Sub ReadEyeballs(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel)
	'	If aModel.eyeballCount > 0 AndAlso aModel.eyeballOffset <> 0 Then
	'		aModel.theEyeballs = New List(Of SourceMdlEyeball)(aModel.eyeballCount)
	'		Dim eyeballInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.eyeballOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		For k As Integer = 0 To aModel.eyeballCount - 1
	'			eyeballInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim anEyeball As New SourceMdlEyeball()

	'			anEyeball.nameOffset = Me.theInputFileReader.ReadInt32()
	'			anEyeball.boneIndex = Me.theInputFileReader.ReadInt32()
	'			anEyeball.org = New SourceVector()
	'			anEyeball.org.x = Me.theInputFileReader.ReadSingle()
	'			anEyeball.org.y = Me.theInputFileReader.ReadSingle()
	'			anEyeball.org.z = Me.theInputFileReader.ReadSingle()
	'			anEyeball.zOffset = Me.theInputFileReader.ReadSingle()
	'			anEyeball.radius = Me.theInputFileReader.ReadSingle()
	'			anEyeball.up = New SourceVector()
	'			anEyeball.up.x = Me.theInputFileReader.ReadSingle()
	'			anEyeball.up.y = Me.theInputFileReader.ReadSingle()
	'			anEyeball.up.z = Me.theInputFileReader.ReadSingle()
	'			anEyeball.forward = New SourceVector()
	'			anEyeball.forward.x = Me.theInputFileReader.ReadSingle()
	'			anEyeball.forward.y = Me.theInputFileReader.ReadSingle()
	'			anEyeball.forward.z = Me.theInputFileReader.ReadSingle()
	'			anEyeball.texture = Me.theInputFileReader.ReadInt32()

	'			anEyeball.unused1 = Me.theInputFileReader.ReadInt32()
	'			anEyeball.irisScale = Me.theInputFileReader.ReadSingle()
	'			anEyeball.unused2 = Me.theInputFileReader.ReadInt32()

	'			anEyeball.upperFlexDesc(0) = Me.theInputFileReader.ReadInt32()
	'			anEyeball.upperFlexDesc(1) = Me.theInputFileReader.ReadInt32()
	'			anEyeball.upperFlexDesc(2) = Me.theInputFileReader.ReadInt32()
	'			anEyeball.lowerFlexDesc(0) = Me.theInputFileReader.ReadInt32()
	'			anEyeball.lowerFlexDesc(1) = Me.theInputFileReader.ReadInt32()
	'			anEyeball.lowerFlexDesc(2) = Me.theInputFileReader.ReadInt32()
	'			anEyeball.upperTarget(0) = Me.theInputFileReader.ReadSingle()
	'			anEyeball.upperTarget(1) = Me.theInputFileReader.ReadSingle()
	'			anEyeball.upperTarget(2) = Me.theInputFileReader.ReadSingle()
	'			anEyeball.lowerTarget(0) = Me.theInputFileReader.ReadSingle()
	'			anEyeball.lowerTarget(1) = Me.theInputFileReader.ReadSingle()
	'			anEyeball.lowerTarget(2) = Me.theInputFileReader.ReadSingle()

	'			anEyeball.upperLidFlexDesc = Me.theInputFileReader.ReadInt32()
	'			anEyeball.lowerLidFlexDesc = Me.theInputFileReader.ReadInt32()

	'			For x As Integer = 0 To anEyeball.unused.Length - 1
	'				anEyeball.unused(x) = Me.theInputFileReader.ReadInt32()
	'			Next

	'			anEyeball.eyeballIsNonFacs = Me.theInputFileReader.ReadByte()

	'			For x As Integer = 0 To anEyeball.unused3.Length - 1
	'				anEyeball.unused3(x) = Me.theInputFileReader.ReadChar()
	'			Next
	'			For x As Integer = 0 To anEyeball.unused4.Length - 1
	'				anEyeball.unused4(x) = Me.theInputFileReader.ReadInt32()
	'			Next

	'			aModel.theEyeballs.Add(anEyeball)

	'			'NOTE: Set the default value to -1 to distinguish it from value assigned to it by ReadMeshes().
	'			anEyeball.theTextureIndex = -1

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			'NOTE: The mdl file doesn't appear to store the eyeball name; studiomdl only uses it internally with eyelids.
	'			If anEyeball.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(eyeballInputFileStreamPosition + anEyeball.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				anEyeball.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEyeball.theName")
	'				End If
	'			Else
	'				anEyeball.theName = ""
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		If aModel.theEyeballs.Count > 0 Then
	'			Me.theMdlFileData.theModelCommandIsUsed = True
	'		End If

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theEyeballs")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "aModel.theEyeballs alignment")
	'	End If
	'End Sub

	'Private Sub ReadFlexes(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceMdlMesh)
	'	aMesh.theFlexes = New List(Of SourceMdlFlex)(aMesh.flexCount)
	'	Dim flexInputFileStreamPosition As Long
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	'Dim fileOffsetStart2 As Long
	'	'Dim fileOffsetEnd2 As Long

	'	Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.flexOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	For k As Integer = 0 To aMesh.flexCount - 1
	'		flexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim aFlex As New SourceMdlFlex()

	'		aFlex.flexDescIndex = Me.theInputFileReader.ReadInt32()

	'		aFlex.target0 = Me.theInputFileReader.ReadSingle()
	'		aFlex.target1 = Me.theInputFileReader.ReadSingle()
	'		aFlex.target2 = Me.theInputFileReader.ReadSingle()
	'		aFlex.target3 = Me.theInputFileReader.ReadSingle()

	'		aFlex.vertCount = Me.theInputFileReader.ReadInt32()
	'		aFlex.vertOffset = Me.theInputFileReader.ReadInt32()

	'		aFlex.flexDescPartnerIndex = Me.theInputFileReader.ReadInt32()
	'		aFlex.vertAnimType = Me.theInputFileReader.ReadByte()
	'		For x As Integer = 0 To aFlex.unusedChar.Length - 1
	'			aFlex.unusedChar(x) = Me.theInputFileReader.ReadChar()
	'		Next
	'		For x As Integer = 0 To aFlex.unused.Length - 1
	'			aFlex.unused(x) = Me.theInputFileReader.ReadInt32()
	'		Next
	'		aMesh.theFlexes.Add(aFlex)

	'		''NOTE: Set the frame index here because it is determined by order of flexes in mdl file.
	'		''      Start the indexing at 1 because first frame (frame 0) is "basis" frame.
	'		'Me.theCurrentFrameIndex += 1
	'		'Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theVtaFrameIndex = Me.theCurrentFrameIndex

	'		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		If aFlex.vertCount > 0 AndAlso aFlex.vertOffset <> 0 Then
	'			Me.ReadVertAnims(flexInputFileStreamPosition, aFlex)
	'		End If

	'		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFlexes")

	'	Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "aMesh.theFlexes alignment")
	'End Sub

	'Private Sub ReadVertAnims(ByVal flexInputFileStreamPosition As Long, ByVal aFlex As SourceMdlFlex)
	'	aFlex.theVertAnims = New List(Of SourceMdlVertAnim)(aFlex.vertCount)
	'	Dim eyeballInputFileStreamPosition As Long
	'	Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	'Dim fileOffsetStart2 As Long
	'	'Dim fileOffsetEnd2 As Long

	'	Me.theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	Dim aVertAnim As SourceMdlVertAnim
	'	For k As Integer = 0 To aFlex.vertCount - 1
	'		eyeballInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		If aFlex.vertAnimType = aFlex.STUDIO_VERT_ANIM_WRINKLE Then
	'			aVertAnim = New SourceMdlVertAnimWrinkle()
	'		Else
	'			aVertAnim = New SourceMdlVertAnim()
	'		End If

	'		aVertAnim.index = Me.theInputFileReader.ReadUInt16()
	'		aVertAnim.speed = Me.theInputFileReader.ReadByte()
	'		aVertAnim.side = Me.theInputFileReader.ReadByte()

	'		For x As Integer = 0 To 2
	'			aVertAnim.deltaUShort(x) = Me.theInputFileReader.ReadUInt16()
	'		Next
	'		For x As Integer = 0 To 2
	'			aVertAnim.nDeltaUShort(x) = Me.theInputFileReader.ReadUInt16()
	'		Next

	'		If aFlex.vertAnimType = aFlex.STUDIO_VERT_ANIM_WRINKLE Then
	'			CType(aVertAnim, SourceMdlVertAnimWrinkle).wrinkleDelta = Me.theInputFileReader.ReadInt16()
	'		End If

	'		aFlex.theVertAnims.Add(aVertAnim)

	'		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	'aFlex.theVertAnims.Sort(AddressOf Me.SortVertAnims)

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims")

	'	Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "aFlex.theVertAnims alignment")
	'End Sub

	'Private Function SortVertAnims(ByVal x As SourceMdlVertAnim, ByVal y As SourceMdlVertAnim) As Integer
	'	Return x.index.CompareTo(y.index)
	'End Function

	'Private Sub ReadFlexDescs()
	'	If Me.theMdlFileData.flexDescCount > 0 Then
	'		Dim flexDescInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexDescOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theFlexDescs = New List(Of SourceMdlFlexDesc)(Me.theMdlFileData.flexDescCount)
	'		For i As Integer = 0 To Me.theMdlFileData.flexDescCount - 1
	'			flexDescInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aFlexDesc As New SourceMdlFlexDesc()
	'			aFlexDesc.nameOffset = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.theFlexDescs.Add(aFlexDesc)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If aFlexDesc.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(flexDescInputFileStreamPosition + aFlexDesc.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aFlexDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexDesc.theName")
	'				End If
	'			Else
	'				aFlexDesc.theName = ""
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs")
	'	End If
	'End Sub

	'Private Sub ReadFlexControllers()
	'	If Me.theMdlFileData.flexControllerCount > 0 Then
	'		Dim flexControllerInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexControllerOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theFlexControllers = New List(Of SourceMdlFlexController)(Me.theMdlFileData.flexControllerCount)
	'		For i As Integer = 0 To Me.theMdlFileData.flexControllerCount - 1
	'			flexControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aFlexController As New SourceMdlFlexController()
	'			aFlexController.typeOffset = Me.theInputFileReader.ReadInt32()
	'			aFlexController.nameOffset = Me.theInputFileReader.ReadInt32()
	'			aFlexController.localToGlobal = Me.theInputFileReader.ReadInt32()
	'			aFlexController.min = Me.theInputFileReader.ReadSingle()
	'			aFlexController.max = Me.theInputFileReader.ReadSingle()
	'			Me.theMdlFileData.theFlexControllers.Add(aFlexController)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If aFlexController.typeOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.typeOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aFlexController.theType = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theType")
	'				End If
	'			Else
	'				aFlexController.theType = ""
	'			End If
	'			If aFlexController.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aFlexController.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theName")
	'				End If
	'			Else
	'				aFlexController.theName = ""
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		If Me.theMdlFileData.theFlexControllers.Count > 0 Then
	'			Me.theMdlFileData.theModelCommandIsUsed = True
	'		End If

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllers")
	'	End If
	'End Sub

	'Private Sub ReadFlexRules()
	'	If Me.theMdlFileData.flexRuleCount > 0 Then
	'		Dim flexRuleInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		'Dim fileOffsetStart2 As Long
	'		'Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexRuleOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theFlexRules = New List(Of SourceMdlFlexRule)(Me.theMdlFileData.flexRuleCount)
	'		For i As Integer = 0 To Me.theMdlFileData.flexRuleCount - 1
	'			flexRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aFlexRule As New SourceMdlFlexRule()
	'			aFlexRule.flexIndex = Me.theInputFileReader.ReadInt32()
	'			aFlexRule.opCount = Me.theInputFileReader.ReadInt32()
	'			aFlexRule.opOffset = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.theFlexRules.Add(aFlexRule)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			Me.theMdlFileData.theFlexDescs(aFlexRule.flexIndex).theDescIsUsedByFlexRule = True

	'			If aFlexRule.opCount > 0 AndAlso aFlexRule.opOffset <> 0 Then
	'				Me.ReadFlexOps(flexRuleInputFileStreamPosition, aFlexRule)
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		If Me.theMdlFileData.theFlexRules.Count > 0 Then
	'			Me.theMdlFileData.theModelCommandIsUsed = True
	'		End If

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules")
	'	End If
	'End Sub

	'Private Sub ReadFlexOps(ByVal flexRuleInputFileStreamPosition As Long, ByVal aFlexRule As SourceMdlFlexRule)
	'	'Dim flexRuleInputFileStreamPosition As Long
	'	'Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	'Dim fileOffsetStart2 As Long
	'	'Dim fileOffsetEnd2 As Long

	'	Me.theInputFileReader.BaseStream.Seek(flexRuleInputFileStreamPosition + aFlexRule.opOffset, SeekOrigin.Begin)
	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	aFlexRule.theFlexOps = New List(Of SourceMdlFlexOp)(aFlexRule.opCount)
	'	For i As Integer = 0 To aFlexRule.opCount - 1
	'		'flexRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'		Dim aFlexOp As New SourceMdlFlexOp()
	'		aFlexOp.op = Me.theInputFileReader.ReadInt32()
	'		If aFlexOp.op = SourceMdlFlexOp.STUDIO_CONST Then
	'			aFlexOp.value = Me.theInputFileReader.ReadSingle()
	'		Else
	'			aFlexOp.index = Me.theInputFileReader.ReadInt32()
	'			If aFlexOp.op = SourceMdlFlexOp.STUDIO_FETCH2 Then
	'				Me.theMdlFileData.theFlexDescs(aFlexOp.index).theDescIsUsedByFlexRule = True
	'			End If
	'		End If
	'		aFlexRule.theFlexOps.Add(aFlexOp)

	'		'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'	Next

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexRule.theFlexOps")
	'End Sub

	'Private Sub ReadIkChains()
	'	If Me.theMdlFileData.ikChainCount > 0 Then
	'		Dim ikChainInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.ikChainOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theIkChains = New List(Of SourceMdlIkChain)(Me.theMdlFileData.ikChainCount)
	'		For i As Integer = 0 To Me.theMdlFileData.ikChainCount - 1
	'			ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim anIkChain As New SourceMdlIkChain()
	'			anIkChain.nameOffset = Me.theInputFileReader.ReadInt32()
	'			anIkChain.linkType = Me.theInputFileReader.ReadInt32()
	'			anIkChain.linkCount = Me.theInputFileReader.ReadInt32()
	'			anIkChain.linkOffset = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.theIkChains.Add(anIkChain)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If anIkChain.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				anIkChain.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkChain.theName")
	'				End If
	'			Else
	'				anIkChain.theName = ""
	'			End If
	'			Me.ReadIkLinks(ikChainInputFileStreamPosition, anIkChain)

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkChains")
	'	End If
	'End Sub

	'Private Sub ReadIkLinks(ByVal ikChainInputFileStreamPosition As Long, ByVal anIkChain As SourceMdlIkChain)
	'	If anIkChain.linkCount > 0 Then
	'		'Dim ikLinkInputFileStreamPosition As Long
	'		'Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		'Dim fileOffsetStart2 As Long
	'		'Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.linkOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		anIkChain.theLinks = New List(Of SourceMdlIkLink)(anIkChain.linkCount)
	'		For j As Integer = 0 To anIkChain.linkCount - 1
	'			'ikLinkInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim anIkLink As New SourceMdlIkLink()
	'			anIkLink.boneIndex = Me.theInputFileReader.ReadInt32()
	'			anIkLink.idealBendingDirection.x = Me.theInputFileReader.ReadSingle()
	'			anIkLink.idealBendingDirection.y = Me.theInputFileReader.ReadSingle()
	'			anIkLink.idealBendingDirection.z = Me.theInputFileReader.ReadSingle()
	'			anIkLink.unused0.x = Me.theInputFileReader.ReadSingle()
	'			anIkLink.unused0.y = Me.theInputFileReader.ReadSingle()
	'			anIkLink.unused0.z = Me.theInputFileReader.ReadSingle()
	'			anIkChain.theLinks.Add(anIkLink)

	'			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkChain.theLinks")
	'	End If
	'End Sub

	'Private Sub ReadIkLocks()
	'	If Me.theMdlFileData.localIkAutoPlayLockCount > 0 Then
	'		'Dim ikChainInputFileStreamPosition As Long
	'		'Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		'Dim fileOffsetStart2 As Long
	'		'Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localIkAutoPlayLockOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theIkLocks = New List(Of SourceMdlIkLock)(Me.theMdlFileData.localIkAutoPlayLockCount)
	'		For i As Integer = 0 To Me.theMdlFileData.localIkAutoPlayLockCount - 1
	'			'ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim anIkLock As New SourceMdlIkLock()
	'			anIkLock.chainIndex = Me.theInputFileReader.ReadInt32()
	'			anIkLock.posWeight = Me.theInputFileReader.ReadSingle()
	'			anIkLock.localQWeight = Me.theInputFileReader.ReadSingle()
	'			anIkLock.flags = Me.theInputFileReader.ReadInt32()
	'			For x As Integer = 0 To anIkLock.unused.Length - 1
	'				anIkLock.unused(x) = Me.theInputFileReader.ReadInt32()
	'			Next
	'			Me.theMdlFileData.theIkLocks.Add(anIkLock)

	'			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkLocks")
	'	End If
	'End Sub

	'Private Sub ReadMouths()
	'	If Me.theMdlFileData.mouthCount > 0 Then
	'		'Dim mouthInputFileStreamPosition As Long
	'		'Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		'Dim fileOffsetStart2 As Long
	'		'Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.mouthOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theMouths = New List(Of SourceMdlMouth)(Me.theMdlFileData.mouthCount)
	'		For i As Integer = 0 To Me.theMdlFileData.mouthCount - 1
	'			'mouthInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aMouth As New SourceMdlMouth()
	'			aMouth.boneIndex = Me.theInputFileReader.ReadInt32()
	'			aMouth.forwardX = Me.theInputFileReader.ReadSingle()
	'			aMouth.forwardY = Me.theInputFileReader.ReadSingle()
	'			aMouth.forwardZ = Me.theInputFileReader.ReadSingle()
	'			aMouth.flexDescIndex = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.theMouths.Add(aMouth)

	'			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		If Me.theMdlFileData.theMouths.Count > 0 Then
	'			Me.theMdlFileData.theModelCommandIsUsed = True
	'		End If

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theMouths")
	'	End If
	'End Sub

	'Private Sub ReadPoseParamDescs()
	'	If Me.theMdlFileData.localPoseParamaterCount > 0 Then
	'		Dim poseInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localPoseParameterOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.thePoseParamDescs = New List(Of SourceMdlPoseParamDesc)(Me.theMdlFileData.localPoseParamaterCount)
	'		For i As Integer = 0 To Me.theMdlFileData.localPoseParamaterCount - 1
	'			poseInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aPoseParamDesc As New SourceMdlPoseParamDesc()
	'			aPoseParamDesc.nameOffset = Me.theInputFileReader.ReadInt32()
	'			aPoseParamDesc.flags = Me.theInputFileReader.ReadInt32()
	'			aPoseParamDesc.startingValue = Me.theInputFileReader.ReadSingle()
	'			aPoseParamDesc.endingValue = Me.theInputFileReader.ReadSingle()
	'			aPoseParamDesc.loopingRange = Me.theInputFileReader.ReadSingle()
	'			Me.theMdlFileData.thePoseParamDescs.Add(aPoseParamDesc)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If aPoseParamDesc.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(poseInputFileStreamPosition + aPoseParamDesc.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aPoseParamDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aPoseParamDesc.theName")
	'				End If
	'			Else
	'				aPoseParamDesc.theName = ""
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs")
	'	End If
	'End Sub

	'Private Sub ReadTextures()
	'	If Me.theMdlFileData.textureCount > 0 Then
	'		Dim textureInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long
	'		'Dim texturePath As String

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.textureOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theTextures = New List(Of SourceMdlTexture)(Me.theMdlFileData.textureCount)
	'		For i As Integer = 0 To Me.theMdlFileData.textureCount - 1
	'			textureInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aTexture As New SourceMdlTexture()
	'			aTexture.nameOffset = Me.theInputFileReader.ReadInt32()
	'			aTexture.flags = Me.theInputFileReader.ReadInt32()
	'			aTexture.used = Me.theInputFileReader.ReadInt32()
	'			aTexture.unused1 = Me.theInputFileReader.ReadInt32()
	'			aTexture.materialP = Me.theInputFileReader.ReadInt32()
	'			aTexture.clientMaterialP = Me.theInputFileReader.ReadInt32()
	'			For x As Integer = 0 To 9
	'				aTexture.unused(x) = Me.theInputFileReader.ReadInt32()
	'			Next
	'			Me.theMdlFileData.theTextures.Add(aTexture)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If aTexture.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aTexture.theFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				' Convert all forward slashes to backward slashes.
	'				aTexture.theFileName = FileManager.GetNormalizedPathFileName(aTexture.theFileName)

	'				'NOTE: Leave this commented so QC file simply shows what is stored in MDL file.
	'				'      Crowbar should always try to show what was in original files unless user opts to do something else.
	'				'' Delete the path in the texture name that is already in the texturepaths list.
	'				'For j As Integer = 0 To Me.theMdlFileData.theTexturePaths.Count - 1
	'				'	texturePath = Me.theMdlFileData.theTexturePaths(j)
	'				'	If texturePath <> "" AndAlso aTexture.theName.StartsWith(texturePath) Then
	'				'		aTexture.theName = aTexture.theName.Replace(texturePath, "")
	'				'		Exit For
	'				'	End If
	'				'Next
	'				'
	'				''TEST: If texture name still has a path, remove the path and add it to the texturepaths list.
	'				'Dim texturePathName As String
	'				'Dim textureFileName As String
	'				'texturePathName = FileManager.GetPath(aTexture.theName)
	'				'textureFileName = Path.GetFileName(aTexture.theName)
	'				'If aTexture.theName <> textureFileName Then
	'				'	'NOTE: Place first because it should override whatever is already in list.
	'				'	'Me.theMdlFileData.theTexturePaths.Add(texturePathName)
	'				'	Me.theMdlFileData.theTexturePaths.Insert(0, texturePathName)
	'				'	aTexture.theName = textureFileName
	'				'End If

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.theName")
	'				End If
	'			Else
	'				aTexture.theFileName = ""
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTextures")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theTextures alignment")
	'	End If
	'End Sub

	'Private Sub ReadTexturePaths()
	'	If Me.theMdlFileData.texturePathCount > 0 Then
	'		Dim texturePathInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.texturePathOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theTexturePaths = New List(Of String)(Me.theMdlFileData.texturePathCount)
	'		Dim texturePathOffset As Integer
	'		For i As Integer = 0 To Me.theMdlFileData.texturePathCount - 1
	'			texturePathInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aTexturePath As String
	'			texturePathOffset = Me.theInputFileReader.ReadInt32()

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If texturePathOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aTexturePath = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				'TEST: Convert all forward slashes to backward slashes.
	'				aTexturePath = FileManager.GetNormalizedPathFileName(aTexturePath)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath")
	'				End If
	'			Else
	'				aTexturePath = ""
	'			End If
	'			Me.theMdlFileData.theTexturePaths.Add(aTexturePath)

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTexturePaths")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theTexturePaths alignment")
	'	End If
	'End Sub

	'Private Sub ReadSkinFamilies()
	'	If Me.theMdlFileData.skinFamilyCount > 0 Then
	'		Dim skinFamilyInputFileStreamPosition As Long
	'		'Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		'Dim fileOffsetStart2 As Long
	'		'Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.skinFamilyOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theSkinFamilies = New List(Of List(Of Integer))(Me.theMdlFileData.skinFamilyCount)
	'		For i As Integer = 0 To Me.theMdlFileData.skinFamilyCount - 1
	'			skinFamilyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aSkinFamily As New List(Of Integer)()

	'			For j As Integer = 0 To Me.theMdlFileData.skinReferenceCount - 1
	'				Dim aSkinRef As Integer
	'				aSkinRef = Me.theInputFileReader.ReadInt16()
	'				aSkinFamily.Add(aSkinRef)
	'			Next

	'			Me.theMdlFileData.theSkinFamilies.Add(aSkinFamily)

	'			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

	'			'If Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0 Then
	'			'	'$pos1 += ($matname_num * 2);
	'			'	Me.theInputFileReader.BaseStream.Seek(skinFamilyInputFileStreamPosition + Me.theMdlFileData.theTextures.Count * 2, SeekOrigin.Begin)
	'			'End If
	'		Next

	'		'TEST: Remove skinRef from each skinFamily, if it is at same skinRef index in all skinFamilies. 
	'		'      Start with the last skinRef index (Me.theMdlFileData.skinReferenceCount)
	'		'      and step -1 to 0 until skinRefs are different between skinFamilies.
	'		Dim index As Integer = -1
	'		For currentSkinRef As Integer = Me.theMdlFileData.skinReferenceCount - 1 To 0 Step -1
	'			For index = 0 To Me.theMdlFileData.skinFamilyCount - 1
	'				Dim aSkinRef As Integer
	'				aSkinRef = Me.theMdlFileData.theSkinFamilies(index)(currentSkinRef)

	'				If aSkinRef <> currentSkinRef Then
	'					Exit For
	'				End If
	'			Next

	'			If index = Me.theMdlFileData.skinFamilyCount Then
	'				For index = 0 To Me.theMdlFileData.skinFamilyCount - 1
	'					Me.theMdlFileData.theSkinFamilies(index).RemoveAt(currentSkinRef)
	'				Next
	'				Me.theMdlFileData.skinReferenceCount -= 1
	'			End If
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment")
	'	End If
	'End Sub

	'Private Sub ReadModelGroups()
	'	If Me.theMdlFileData.includeModelCount > 0 Then
	'		Dim includeModelInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.includeModelOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theModelGroups = New List(Of SourceMdlModelGroup)(Me.theMdlFileData.includeModelCount)
	'		For i As Integer = 0 To Me.theMdlFileData.includeModelCount - 1
	'			includeModelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aModelGroup As New SourceMdlModelGroup()
	'			aModelGroup.labelOffset = Me.theInputFileReader.ReadInt32()
	'			aModelGroup.fileNameOffset = Me.theInputFileReader.ReadInt32()
	'			Me.theMdlFileData.theModelGroups.Add(aModelGroup)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If aModelGroup.labelOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + aModelGroup.labelOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aModelGroup.theLabel = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aModelGroup.theLabel")
	'				End If
	'			Else
	'				aModelGroup.theLabel = ""
	'			End If
	'			If aModelGroup.fileNameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + aModelGroup.fileNameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aModelGroup.theFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aModelGroup.theFileName")
	'				End If
	'			Else
	'				aModelGroup.theFileName = ""
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theModelGroups")
	'	End If
	'End Sub

	'Private Sub ReadFlexControllerUis()
	'	If Me.theMdlFileData.flexControllerUiCount_VERSION48 > 0 Then
	'		Dim flexControllerUiInputFileStreamPosition As Long
	'		Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim fileOffsetStart2 As Long
	'		Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexControllerUiOffset_VERSION48, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		Me.theMdlFileData.theFlexControllerUis = New List(Of SourceMdlFlexControllerUi)(Me.theMdlFileData.flexControllerUiCount_VERSION48)
	'		For i As Integer = 0 To Me.theMdlFileData.flexControllerUiCount_VERSION48 - 1
	'			flexControllerUiInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim aFlexControllerUi As New SourceMdlFlexControllerUi()
	'			aFlexControllerUi.nameOffset = Me.theInputFileReader.ReadInt32()
	'			aFlexControllerUi.config0 = Me.theInputFileReader.ReadInt32()
	'			aFlexControllerUi.config1 = Me.theInputFileReader.ReadInt32()
	'			aFlexControllerUi.config2 = Me.theInputFileReader.ReadInt32()
	'			aFlexControllerUi.remapType = Me.theInputFileReader.ReadByte()
	'			aFlexControllerUi.controlIsStereo = Me.theInputFileReader.ReadByte()
	'			For x As Integer = 0 To aFlexControllerUi.unused.Length - 1
	'				aFlexControllerUi.unused(x) = Me.theInputFileReader.ReadByte()
	'			Next
	'			Me.theMdlFileData.theFlexControllerUis.Add(aFlexControllerUi)

	'			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			If aFlexControllerUi.nameOffset <> 0 Then
	'				Me.theInputFileReader.BaseStream.Seek(flexControllerUiInputFileStreamPosition + aFlexControllerUi.nameOffset, SeekOrigin.Begin)
	'				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

	'				aFlexControllerUi.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

	'				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
	'				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
	'					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexControllerUi.theName")
	'				End If
	'			Else
	'				aFlexControllerUi.theName = ""
	'			End If

	'			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllerUis")
	'	End If
	'End Sub

	'Private Sub ReadKeyValues()
	'	If Me.theMdlFileData.keyValueSize > 0 Then
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		Dim nullChar As Char

	'		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.keyValueOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		'NOTE: Use -1 to drop the null terminator character.
	'		Me.theMdlFileData.theKeyValuesText = Me.theInputFileReader.ReadChars(Me.theMdlFileData.keyValueSize - 1)
	'		'NOTE: Read the null terminator character.
	'		nullChar = Me.theInputFileReader.ReadChar()

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theKeyValuesText")

	'		Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "theMdlFileData.theKeyValuesText alignment")
	'	End If
	'End Sub

	'Private Sub ReadUnknownValues(ByVal aFileSeekLog As FileSeekLog)
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
	'	Catch

	'	End Try
	'End Sub

#End Region

#Region "Data"

	Protected theMdlFileData As SourceMdlFileData
	Protected theInputFileReader As BinaryReader

#End Region

End Class
