Imports System.IO

Public Class SourceMdlFile06

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlFileReader As BinaryReader, ByVal mdlFileData As SourceMdlFileData06)
		Me.theInputFileReader = mdlFileReader
		Me.theMdlFileData = mdlFileData

		Me.theMdlFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length
	End Sub

	Public Sub New(ByVal mdlFileWriter As BinaryWriter, ByVal mdlFileData As SourceMdlFileData06)
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

		Me.theMdlFileData.boneCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.boneControllerCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneControllerOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.sequenceCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.sequenceOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.textureCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.textureOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.textureDataOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.skinReferenceCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinFamilyCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.bodyPartCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.bodyPartOffset = Me.theInputFileReader.ReadInt32()

		For x As Integer = 0 To Me.theMdlFileData.unused.Length - 1
			Me.theMdlFileData.unused(x) = Me.theInputFileReader.ReadInt32()
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "MDL File Header")
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

				Me.theMdlFileData.theBones = New List(Of SourceMdlBone06)(Me.theMdlFileData.boneCount)
				For boneIndex As Integer = 0 To Me.theMdlFileData.boneCount - 1
					'boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBone As New SourceMdlBone06()

					aBone.name = Me.theInputFileReader.ReadChars(32)
					aBone.theName = aBone.name
					aBone.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aBone.theName)
					If aBone.theName = "" Then
						aBone.theName = "unnamed_bone_" + boneIndex.ToString("000")
					End If
					aBone.parentBoneIndex = Me.theInputFileReader.ReadInt32()
					aBone.position.x = Me.theInputFileReader.ReadSingle()
					aBone.position.y = Me.theInputFileReader.ReadSingle()
					aBone.position.z = Me.theInputFileReader.ReadSingle()
					aBone.rotation.x = Me.theInputFileReader.ReadSingle()
					aBone.rotation.y = Me.theInputFileReader.ReadSingle()
					aBone.rotation.z = Me.theInputFileReader.ReadSingle()

					Me.theMdlFileData.theBones.Add(aBone)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones " + Me.theMdlFileData.theBones.Count.ToString())

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
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theBoneControllers = New List(Of SourceMdlBoneController06)(Me.theMdlFileData.boneControllerCount)
				For boneControllerIndex As Integer = 0 To Me.theMdlFileData.boneControllerCount - 1
					'boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBoneController As New SourceMdlBoneController06()

					aBoneController.boneIndex = Me.theInputFileReader.ReadInt32()
					aBoneController.type = Me.theInputFileReader.ReadInt32()
					aBoneController.startAngleDegrees = Me.theInputFileReader.ReadSingle()
					aBoneController.endAngleDegrees = Me.theInputFileReader.ReadSingle()

					Me.theMdlFileData.theBoneControllers.Add(aBoneController)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers " + Me.theMdlFileData.theBoneControllers.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadSequences()
		If Me.theMdlFileData.sequenceCount > 0 Then
			'Dim sequenceInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.sequenceOffset, SeekOrigin.Begin)
				Me.theMdlFileData.theSequences = New List(Of SourceMdlSequenceDesc06)(Me.theMdlFileData.sequenceCount)
				For sequenceIndex As Integer = 0 To Me.theMdlFileData.sequenceCount - 1
					Dim aSequence As New SourceMdlSequenceDesc06()

					fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aSequence.name = Me.theInputFileReader.ReadChars(32)
					aSequence.theName = aSequence.name
					aSequence.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aSequence.theName)

					aSequence.fps = Me.theInputFileReader.ReadSingle()

					aSequence.flags = Me.theInputFileReader.ReadInt32()
					aSequence.eventCount = Me.theInputFileReader.ReadInt32()
					aSequence.eventOffset = Me.theInputFileReader.ReadInt32()
					aSequence.frameCount = Me.theInputFileReader.ReadInt32()
					aSequence.unused01 = Me.theInputFileReader.ReadInt32()
					aSequence.pivotCount = Me.theInputFileReader.ReadInt32()
					aSequence.pivotOffset = Me.theInputFileReader.ReadInt32()

					aSequence.motiontype = Me.theInputFileReader.ReadInt32()
					aSequence.motionbone = Me.theInputFileReader.ReadInt32()
					aSequence.unused02 = Me.theInputFileReader.ReadInt32()
					aSequence.linearmovement.x = Me.theInputFileReader.ReadSingle()
					aSequence.linearmovement.y = Me.theInputFileReader.ReadSingle()
					aSequence.linearmovement.z = Me.theInputFileReader.ReadSingle()

					aSequence.blendCount = Me.theInputFileReader.ReadInt32()
					aSequence.animOffset = Me.theInputFileReader.ReadInt32()

					For x As Integer = 0 To aSequence.unused03.Length - 1
						aSequence.unused03(x) = Me.theInputFileReader.ReadInt32()
					Next

					Me.theMdlFileData.theSequences.Add(aSequence)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence [" + aSequence.theName + "]")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadEvents(aSequence)
					'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSequence.theEvents alignment")

					Me.ReadPivots(aSequence)
					'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSequence.thePivots alignment")

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadAnimations()
		If Me.theMdlFileData.theSequences IsNot Nothing Then
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long
			Dim aSequence As SourceMdlSequenceDesc06
			'Dim previousFrameIndex As Integer

			Try
				For sequenceIndex As Integer = 0 To Me.theMdlFileData.theSequences.Count - 1
					aSequence = Me.theMdlFileData.theSequences(sequenceIndex)

					Me.theInputFileReader.BaseStream.Seek(aSequence.animOffset, SeekOrigin.Begin)
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aSequence.theAnimations = New List(Of SourceMdlAnimation06)(aSequence.frameCount * Me.theMdlFileData.theBones.Count)
					'For frameIndex As Integer = 0 To aSequence.frameCount - 1
					'While True
					For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
						fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						Dim anAnimation As New SourceMdlAnimation06()

						anAnimation.bonePositionCount = Me.theInputFileReader.ReadInt32()
						anAnimation.bonePositionOffset = Me.theInputFileReader.ReadInt32()
						anAnimation.boneRotationCount = Me.theInputFileReader.ReadInt32()
						anAnimation.boneRotationOffset = Me.theInputFileReader.ReadInt32()

						aSequence.theAnimations.Add(anAnimation)

						fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation")

						inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						anAnimation.theBonePositionsAndRotations = New List(Of SourceBonePostionAndRotation06)(aSequence.frameCount)
						For frameIndex As Integer = 0 To aSequence.frameCount - 1
							Dim bonePositionAndRotation As New SourceBonePostionAndRotation06()
							anAnimation.theBonePositionsAndRotations.Add(bonePositionAndRotation)
						Next
						Me.ReadAnimationBonePositions(anAnimation)
						Me.ReadAnimationBoneRotations(anAnimation)

						Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

						'If anAnimation.theBonePositions.Count > 0 AndAlso anAnimation.theBonePositions(boneIndex).frameIndex < previousFrameIndex Then

						'End If
					Next
					'Next
					'End While
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

				Me.theMdlFileData.theBodyParts = New List(Of SourceMdlBodyPart06)(Me.theMdlFileData.bodyPartCount)
				For bodyPartIndex As Integer = 0 To Me.theMdlFileData.bodyPartCount - 1
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aBodyPart As New SourceMdlBodyPart06()

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

				Me.theMdlFileData.theTextures = New List(Of SourceMdlTexture06)(Me.theMdlFileData.textureCount)
				For textureIndex As Integer = 0 To Me.theMdlFileData.textureCount - 1
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					'boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aTexture As New SourceMdlTexture06()

					aTexture.fileName = Me.theInputFileReader.ReadChars(64)
					aTexture.theFileName = CStr(aTexture.fileName).Trim(Chr(0))
					aTexture.flags = Me.theInputFileReader.ReadInt32()
					aTexture.width = Me.theInputFileReader.ReadUInt32()
					aTexture.height = Me.theInputFileReader.ReadUInt32()
					aTexture.dataOffset = Me.theInputFileReader.ReadUInt32()

					Me.theMdlFileData.theTextures.Add(aTexture)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture [" + aTexture.theFileName + "]")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadTextureData(aTexture)
					'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aTexture.theData alignment")

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment")
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

				Me.theMdlFileData.theSkins = New List(Of List(Of Short))(Me.theMdlFileData.skinFamilyCount)
				For skinFamilyIndex As Integer = 0 To Me.theMdlFileData.skinFamilyCount - 1
					'boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aSkinFamily As New List(Of Short)()

					For skinRefIndex As Integer = 0 To Me.theMdlFileData.skinReferenceCount - 1
						aSkinRef = Me.theInputFileReader.ReadInt16()
						aSkinFamily.Add(aSkinRef)
					Next

					Me.theMdlFileData.theSkins.Add(aSkinFamily)

					'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkins " + Me.theMdlFileData.theSkins.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkins alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Public Sub ReadUnreadBytes()
		Me.theMdlFileData.theFileSeekLog.LogUnreadBytes(Me.theInputFileReader)
	End Sub

	' The bone positions and rotations are all zeroes, so get them from the first sequence's first frame.
	Public Sub GetBoneDataFromFirstSequenceFirstFrame()
		Dim aSequence As SourceMdlSequenceDesc06
		Dim anAnimation As SourceMdlAnimation06
		Dim aBone As SourceMdlBone06
		aSequence = Me.theMdlFileData.theSequences(0)

		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			aBone = Me.theMdlFileData.theBones(boneIndex)
			anAnimation = aSequence.theAnimations(boneIndex)

			aBone.position.x = anAnimation.theBonePositionsAndRotations(0).position.x
			aBone.position.y = anAnimation.theBonePositionsAndRotations(0).position.y
			aBone.position.z = anAnimation.theBonePositionsAndRotations(0).position.z
			aBone.rotation.x = anAnimation.theBonePositionsAndRotations(0).rotation.x
			aBone.rotation.y = anAnimation.theBonePositionsAndRotations(0).rotation.y
			aBone.rotation.z = anAnimation.theBonePositionsAndRotations(0).rotation.z
		Next
	End Sub

	Public Sub BuildBoneTransforms()
		Me.theMdlFileData.theBoneTransforms = New List(Of SourceBoneTransform06)(Me.theMdlFileData.theBones.Count)
		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			Dim aBone As SourceMdlBone06
			Dim boneTransform As New SourceBoneTransform06()
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
				Dim parentBoneTransform As SourceBoneTransform06
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

	Private Sub ReadEvents(ByVal aSequence As SourceMdlSequenceDesc06)
		If aSequence.eventCount > 0 Then
			'Dim sequenceInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				'If Me.theInputFileReader.BaseStream.Position <> aSequence.eventOffset Then
				'	Dim offsetIsNotRight As Boolean = True
				'End If

				Me.theInputFileReader.BaseStream.Seek(aSequence.eventOffset, SeekOrigin.Begin)
				aSequence.theEvents = New List(Of SourceMdlEvent06)(aSequence.eventCount)
				For eventIndex As Integer = 0 To aSequence.eventCount - 1
					'sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim anEvent As New SourceMdlEvent06()

					fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					anEvent.frameIndex = Me.theInputFileReader.ReadInt16()
					anEvent.eventType = Me.theInputFileReader.ReadInt16()

					aSequence.theEvents.Add(anEvent)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anEvent")
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadPivots(ByVal aSequence As SourceMdlSequenceDesc06)
		If aSequence.pivotCount > 0 Then
			'Dim sequenceInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				'If Me.theInputFileReader.BaseStream.Position <> aSequence.pivotOffset Then
				'	Dim offsetIsNotRight As Boolean = True
				'End If

				Me.theInputFileReader.BaseStream.Seek(aSequence.pivotOffset, SeekOrigin.Begin)
				aSequence.thePivots = New List(Of SourceMdlPivot06)(aSequence.pivotCount)
				For pivotIndex As Integer = 0 To aSequence.pivotCount - 1
					'sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					Dim aPivot As New SourceMdlPivot06()

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

	Private Sub ReadModels(ByVal aBodyPart As SourceMdlBodyPart06)
		'Dim modelInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(aBodyPart.modelOffset, SeekOrigin.Begin)

			aBodyPart.theModels = New List(Of SourceMdlModel06)(aBodyPart.modelCount)
			For bodyPartIndex As Integer = 0 To aBodyPart.modelCount - 1
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position
				'modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aModel As New SourceMdlModel06()

				aModel.name = Me.theInputFileReader.ReadChars(64)
				aModel.theName = CStr(aModel.name).Trim(Chr(0))
				aModel.type = Me.theInputFileReader.ReadInt32()

				aModel.unknown01 = Me.theInputFileReader.ReadInt32()
				aModel.unused01 = Me.theInputFileReader.ReadInt32()

				aModel.meshCount = Me.theInputFileReader.ReadInt32()
				aModel.meshOffset = Me.theInputFileReader.ReadInt32()

				aModel.vertexCount = Me.theInputFileReader.ReadInt32()
				aModel.vertexBoneInfoOffset = Me.theInputFileReader.ReadInt32()

				aModel.normalCount = Me.theInputFileReader.ReadInt32()
				aModel.normalBoneInfoOffset = Me.theInputFileReader.ReadInt32()

				aModel.unused02 = Me.theInputFileReader.ReadInt32()
				aModel.modelDataOffset = Me.theInputFileReader.ReadInt32()

				aBodyPart.theModels.Add(aModel)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel [" + aModel.theName + "]")

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				Me.ReadModelVertexBoneInfos(aModel)
				Me.ReadModelNormalBoneInfos(aModel)
				Me.ReadModelData(aModel)
				Me.ReadMeshes(aModel)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadModelVertexBoneInfos(ByVal aModel As SourceMdlModel06)
		If aModel.vertexCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Dim vertexBoneInfo As Integer
				Me.theInputFileReader.BaseStream.Seek(aModel.vertexBoneInfoOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theVertexBoneInfos = New List(Of Integer)(aModel.vertexCount)
				For vertexBoneInfoIndex As Integer = 0 To aModel.vertexCount - 1
					vertexBoneInfo = Me.theInputFileReader.ReadByte()
					aModel.theVertexBoneInfos.Add(vertexBoneInfo)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexBoneInfos " + aModel.theVertexBoneInfos.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexBoneInfos alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadModelNormalBoneInfos(ByVal aModel As SourceMdlModel06)
		If aModel.normalCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Dim normalBoneInfo As Integer
				Me.theInputFileReader.BaseStream.Seek(aModel.normalBoneInfoOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModel.theNormalBoneInfos = New List(Of Integer)(aModel.normalCount)
				For normalBoneInfoIndex As Integer = 0 To aModel.normalCount - 1
					normalBoneInfo = Me.theInputFileReader.ReadByte()
					aModel.theNormalBoneInfos.Add(normalBoneInfo)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormalBoneInfos " + aModel.theNormalBoneInfos.Count.ToString())

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theNormalBoneInfos alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadModelData(ByVal aModel As SourceMdlModel06)
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Try
			Me.theInputFileReader.BaseStream.Seek(aModel.modelDataOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			'NOTE: No idea why there are sequenceCount of model data. The first one seems to be the only one used.
			For sequenceIndex As Integer = 0 To Me.theMdlFileData.sequenceCount - 1
				Dim aModelData As New SourceMdlModelData06()

				aModelData.unknown01 = Me.theInputFileReader.ReadInt32()
				aModelData.unknown02 = Me.theInputFileReader.ReadInt32()
				aModelData.unknown03 = Me.theInputFileReader.ReadInt32()
				aModelData.vertexCount = Me.theInputFileReader.ReadInt32()
				aModelData.vertexOffset = Me.theInputFileReader.ReadInt32()
				aModelData.normalCount = Me.theInputFileReader.ReadInt32()
				aModelData.normalOffset = Me.theInputFileReader.ReadInt32()

				aModel.theModelDatas.Add(aModelData)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theModelDatas " + aModel.theModelDatas.Count.ToString())

			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theModelDatas alignment")

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			Me.ReadModelVertexes(aModel.theModelDatas(0))
			Me.ReadModelNormals(aModel.theModelDatas(0))

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ReadModelVertexes(ByVal aModelData As SourceMdlModelData06)
		If aModelData.vertexCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(aModelData.vertexOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModelData.theVertexes = New List(Of SourceVector)(aModelData.vertexCount)
				For vertexIndex As Integer = 0 To aModelData.vertexCount - 1
					Dim vertex As New SourceVector()
					vertex.x = Me.theInputFileReader.ReadSingle()
					vertex.y = Me.theInputFileReader.ReadSingle()
					vertex.z = Me.theInputFileReader.ReadSingle()
					aModelData.theVertexes.Add(vertex)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexes " + aModelData.theVertexes.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadModelNormals(ByVal aModelData As SourceMdlModelData06)
		If aModelData.normalCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(aModelData.normalOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aModelData.theNormals = New List(Of SourceVector)(aModelData.normalCount)
				For normalIndex As Integer = 0 To aModelData.normalCount - 1
					Dim normal As New SourceVector()
					normal.x = Me.theInputFileReader.ReadSingle()
					normal.y = Me.theInputFileReader.ReadSingle()
					normal.z = Me.theInputFileReader.ReadSingle()
					aModelData.theNormals.Add(normal)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormals " + aModelData.theNormals.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theNormals alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadMeshes(ByVal aModel As SourceMdlModel06)
		If aModel.meshCount > 0 Then
			'Dim meshInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(aModel.meshOffset, SeekOrigin.Begin)

				aModel.theMeshes = New List(Of SourceMdlMesh06)(aModel.meshCount)
				For meshIndex As Integer = 0 To aModel.meshCount - 1
					fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					Dim aMesh As New SourceMdlMesh06()

					aMesh.faceCount = Me.theInputFileReader.ReadInt32()
					aMesh.faceOffset = Me.theInputFileReader.ReadInt32()
					aMesh.skinref = Me.theInputFileReader.ReadInt32()
					aMesh.normalCount = Me.theInputFileReader.ReadInt32()
					aMesh.normalOffset = Me.theInputFileReader.ReadInt32()

					aModel.theMeshes.Add(aMesh)

					fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh")

					inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					Me.ReadFaces(aMesh)

					Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				Next

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theMeshes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadFaces(ByVal aMesh As SourceMdlMesh06)
		If aMesh.faceCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Try
				Me.theInputFileReader.BaseStream.Seek(aMesh.faceOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aMesh.theVertexAndNormalIndexes = New List(Of SourceMdlTriangleVertex06)()
				For faceIndex As Integer = 0 To aMesh.faceCount * 3 - 1
					Dim vertexAndNormalIndexInfo As New SourceMdlTriangleVertex06()

					vertexAndNormalIndexInfo.vertexIndex = Me.theInputFileReader.ReadUInt16()
					vertexAndNormalIndexInfo.normalIndex = Me.theInputFileReader.ReadUInt16()
					vertexAndNormalIndexInfo.s = Me.theInputFileReader.ReadInt16()
					vertexAndNormalIndexInfo.t = Me.theInputFileReader.ReadInt16()

					aMesh.theVertexAndNormalIndexes.Add(vertexAndNormalIndexInfo)
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theVertexAndNormalIndexes " + aMesh.theVertexAndNormalIndexes.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aMesh.theVertexAndNormalIndexes alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadTextureData(ByVal aTexture As SourceMdlTexture06)
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

	Private Sub ReadAnimationBonePositions(ByVal anAnimation As SourceMdlAnimation06)
		If anAnimation.bonePositionCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim startFrameIndex As Integer
			Dim stopFrameIndex As Integer

			Try
				Me.theInputFileReader.BaseStream.Seek(anAnimation.bonePositionOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				anAnimation.theRawBonePositions = New List(Of SourceMdlBonePosition06)(anAnimation.bonePositionCount)
				For bonePositionIndex As Integer = 0 To anAnimation.bonePositionCount - 1
					Dim bonePosition As New SourceMdlBonePosition06()

					bonePosition.frameIndex = Me.theInputFileReader.ReadInt32()
					bonePosition.position.x = Me.theInputFileReader.ReadSingle()
					bonePosition.position.y = Me.theInputFileReader.ReadSingle()
					bonePosition.position.z = Me.theInputFileReader.ReadSingle()

					anAnimation.theRawBonePositions.Add(bonePosition)
				Next

				'NOTE: Set up list indexed by frame for easier writing of animation SMD files.
				startFrameIndex = 0
				For bonePositionIndex As Integer = 0 To anAnimation.theRawBonePositions.Count - 1
					Dim bonePosition As New SourceMdlBonePosition06()
					bonePosition = anAnimation.theRawBonePositions(bonePositionIndex)

					If bonePositionIndex = anAnimation.theRawBonePositions.Count - 1 Then
						stopFrameIndex = anAnimation.theBonePositionsAndRotations.Count - 1
					Else
						stopFrameIndex = anAnimation.theRawBonePositions(bonePositionIndex + 1).frameIndex - 1
					End If
					For frameIndex As Integer = startFrameIndex To stopFrameIndex
						Dim bonePositionAndRotation As New SourceBonePostionAndRotation06()
						bonePositionAndRotation = anAnimation.theBonePositionsAndRotations(frameIndex)

						bonePositionAndRotation.position.x = bonePosition.position.x
						bonePositionAndRotation.position.y = bonePosition.position.y
						bonePositionAndRotation.position.z = bonePosition.position.z
					Next
					startFrameIndex = stopFrameIndex + 1
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation.theBonePositions " + anAnimation.theRawBonePositions.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimation.theBonePositions alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub ReadAnimationBoneRotations(ByVal anAnimation As SourceMdlAnimation06)
		If anAnimation.boneRotationCount > 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim startFrameIndex As Integer
			Dim stopFrameIndex As Integer

			Try
				Me.theInputFileReader.BaseStream.Seek(anAnimation.boneRotationOffset, SeekOrigin.Begin)
				fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				anAnimation.theRawBoneRotations = New List(Of SourceMdlBoneRotation06)(anAnimation.boneRotationCount)
				For boneRotationIndex As Integer = 0 To anAnimation.boneRotationCount - 1
					Dim boneRotation As New SourceMdlBoneRotation06()

					boneRotation.frameIndex = Me.theInputFileReader.ReadInt16()
					boneRotation.angle(0) = Me.theInputFileReader.ReadInt16()
					boneRotation.angle(1) = Me.theInputFileReader.ReadInt16()
					boneRotation.angle(2) = Me.theInputFileReader.ReadInt16()

					anAnimation.theRawBoneRotations.Add(boneRotation)
				Next

				'NOTE: Set up list indexed by frame for easier writing of animation SMD files.
				startFrameIndex = 0
				For boneRotationIndex As Integer = 0 To anAnimation.theRawBoneRotations.Count - 1
					Dim boneRotation As New SourceMdlBoneRotation06()
					boneRotation = anAnimation.theRawBoneRotations(boneRotationIndex)

					If boneRotationIndex = anAnimation.theRawBoneRotations.Count - 1 Then
						stopFrameIndex = anAnimation.theBonePositionsAndRotations.Count - 1
					Else
						stopFrameIndex = anAnimation.theRawBoneRotations(boneRotationIndex + 1).frameIndex - 1
					End If
					For frameIndex As Integer = startFrameIndex To stopFrameIndex
						Dim bonePositionAndRotation As New SourceBonePostionAndRotation06()
						bonePositionAndRotation = anAnimation.theBonePositionsAndRotations(frameIndex)

						'#define STUDIO_TO_RAD		(Q_PI_F/18000.0f)
						bonePositionAndRotation.rotation.x = boneRotation.angle(0) * (Math.PI / 18000)
						bonePositionAndRotation.rotation.y = boneRotation.angle(1) * (Math.PI / 18000)
						bonePositionAndRotation.rotation.z = boneRotation.angle(2) * (Math.PI / 18000)
					Next
					startFrameIndex = stopFrameIndex + 1
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation.theBoneRotations " + anAnimation.theRawBoneRotations.Count.ToString())

				'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimation.theBoneRotations alignment")
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

#End Region

#Region "Data"

	Protected theInputFileReader As BinaryReader
	Protected theOutputFileWriter As BinaryWriter

	Protected theMdlFileData As SourceMdlFileData06

#End Region

End Class
