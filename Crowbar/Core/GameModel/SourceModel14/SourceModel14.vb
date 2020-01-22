Imports System.IO
Imports System.Runtime.InteropServices

Public Class SourceModel14
	Inherits SourceModel06

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlPathFileName As String, ByVal mdlVersion As Integer)
		MyBase.New(mdlPathFileName, mdlVersion)

		'Me.theExternalTexturesAreUsed = False
	End Sub

#End Region

#Region "Properties"

	Public Overrides ReadOnly Property SequenceGroupMdlFilesAreUsed As Boolean
		Get
			'Return Me.theMdlFileData IsNot Nothing AndAlso Me.theMdlFileData.sequenceGroupCount > 1 AndAlso Me.theMdlFileData.sequenceGroupCount = Me.theSequenceGroupMdlPathFileNames.Count
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property TextureMdlFileIsUsed As Boolean
		Get
			'Return Me.theMdlFileData.textureCount = 0 AndAlso Not String.IsNullOrEmpty(Me.theTextureMdlPathFileName)
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property HasTextureData As Boolean
		Get
			'Return Me.theMdlFileData.textureCount > 0 OrElse Me.theTextureMdlFileData10 IsNot Nothing
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property HasMeshData As Boolean
		Get
			If Me.theMdlFileData.theBones IsNot Nothing _
					 AndAlso Me.theMdlFileData.theBones.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasBoneAnimationData As Boolean
		Get
			If Me.theMdlFileData.theSequences IsNot Nothing _
			 AndAlso Me.theMdlFileData.theSequences.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasTextureFileData As Boolean
		Get
			'Return Me.theMdlFileData.textureCount > 0 OrElse Me.theTextureMdlFileData10 IsNot Nothing
			Return False
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overrides Function CheckForRequiredFiles() As FilesFoundFlags
		Dim status As AppEnums.FilesFoundFlags = FilesFoundFlags.AllFilesFound

		'Dim mdlPath As String
		'Dim mdlFileNameWithoutExtension As String
		'Dim mdlExtension As String
		'Dim textureMdlFileName As String

		'Try
		'	mdlPath = FileManager.GetPath(Me.theMdlPathFileName)
		'	mdlFileNameWithoutExtension = Path.GetFileNameWithoutExtension(Me.theMdlPathFileName)
		'	mdlExtension = Path.GetExtension(Me.theMdlPathFileName)

		'	'TODO: Fill theSequenceGroupMdlPathFileNames with actual names stored as is done in ReadSequenceGroupMdlFiles().
		'	'      Requires reading in the SequenceGroup data.
		'	Me.theSequenceGroupMdlPathFileNames = New List(Of String)(Me.theMdlFileData.sequenceGroupCount)

		'	Me.theSequenceGroupMdlPathFileNames.Add(Me.theMdlPathFileName)
		'	'NOTE: Start index at 1 because 0 is the main MDL file, handled above.
		'	For sequenceGroupIndex As Integer = 1 To Me.theMdlFileData.sequenceGroupCount - 1
		'		Dim aSequenceGroupMdlFileName As String
		'		Dim aSequenceGroupMdlPathFileName As String
		'		'sequenceGroupMdlFileName = Path.GetFileName(aSequenceGroup.theFileName)
		'		'sequenceGroupMdlPathFileName = Path.Combine(mdlPath, sequenceGroupMdlFileName)
		'		aSequenceGroupMdlFileName = mdlFileNameWithoutExtension + sequenceGroupIndex.ToString("00") + mdlExtension
		'		aSequenceGroupMdlPathFileName = Path.Combine(mdlPath, aSequenceGroupMdlFileName)
		'		'If Not File.Exists(aSequenceGroupMdlPathFileName) Then
		'		'	status = StatusMessage.Error
		'		'End If
		'		Me.theSequenceGroupMdlPathFileNames.Add(aSequenceGroupMdlPathFileName)

		'		If Not File.Exists(aSequenceGroupMdlPathFileName) Then
		'			status = FilesFoundFlags.ErrorRequiredSequenceGroupMdlFileNotFound
		'			Return status
		'		End If
		'	Next

		'	If Me.theMdlFileData.textureCount = 0 Then
		'		textureMdlFileName = mdlFileNameWithoutExtension + "T" + mdlExtension
		'		Me.theTextureMdlPathFileName = Path.Combine(mdlPath, textureMdlFileName)
		'		If Not File.Exists(Me.theTextureMdlPathFileName) Then
		'			status = FilesFoundFlags.ErrorRequiredTextureMdlFileNotFound
		'			Return status
		'		End If
		'	End If
		'Catch ex As Exception
		'	status = FilesFoundFlags.Error
		'End Try

		Return status
	End Function

	Public Overrides Function ReadSequenceGroupMdlFiles() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim aSequenceGroup As SourceMdlSequenceGroup10
		Dim mdlPath As String
		Dim sequenceGroupMdlFileName As String
		Dim sequenceGroupMdlPathFileName As String

		'NOTE: Start at index 1 because sequence group 0 is in the main MDL file.
		For sequenceGroupIndex As Integer = 1 To Me.theMdlFileData.sequenceGroupCount - 1
			aSequenceGroup = Me.theMdlFileData.theSequenceGroups(sequenceGroupIndex)
			mdlPath = FileManager.GetPath(Me.theMdlPathFileName)
			sequenceGroupMdlFileName = Path.GetFileName(aSequenceGroup.theFileName)
			sequenceGroupMdlPathFileName = Path.Combine(mdlPath, sequenceGroupMdlFileName)
			status = Me.ReadSequenceGroupMdlFile(sequenceGroupMdlPathFileName, sequenceGroupIndex)
		Next

		Return status
	End Function

	Public Overrides Function ReadTextureMdlFile() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		'If String.IsNullOrEmpty(Me.theTextureMdlPathFileName) Then
		'	status = Me.CheckForRequiredFiles()
		'End If

		If status = StatusMessage.Success Then
			Try
				Me.ReadFile(Me.theTextureMdlPathFileName, AddressOf Me.ReadTextureMdlFile_Internal)
			Catch ex As Exception
				status = StatusMessage.Error
			End Try
		End If

		Return status
	End Function

	Public Overrides Function WriteReferenceMeshFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		'Dim smdFileName As String
		'Dim smdPathFileName As String
		'Dim aBodyPart As SourceVtxBodyPart
		'Dim aVtxModel As SourceVtxModel
		'Dim aModel As SourceMdlModel
		'Dim bodyPartVertexIndexStart As Integer

		'bodyPartVertexIndexStart = 0
		'If Me.theVtxFileData48.theVtxBodyParts IsNot Nothing AndAlso Me.theMdlFileData48.theBodyParts IsNot Nothing Then
		'	For bodyPartIndex As Integer = 0 To Me.theVtxFileData48.theVtxBodyParts.Count - 1
		'		aBodyPart = Me.theVtxFileData48.theVtxBodyParts(bodyPartIndex)

		'		If aBodyPart.theVtxModels IsNot Nothing Then
		'			For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
		'				aVtxModel = aBodyPart.theVtxModels(modelIndex)

		'				If aVtxModel.theVtxModelLods IsNot Nothing Then
		'					aModel = Me.theMdlFileData48.theBodyParts(bodyPartIndex).theModels(modelIndex)
		'					If aModel.name(0) = ChrW(0) AndAlso aVtxModel.theVtxModelLods(0).theVtxMeshes Is Nothing Then
		'						Continue For
		'					End If

		'					For lodIndex As Integer = lodStartIndex To lodStopIndex
		'						smdFileName = SourceFileNamesModule.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, lodIndex, Me.theMdlFileData48.theModelCommandIsUsed, Me.theName, Me.theMdlFileData48.theBodyParts(bodyPartIndex).theModels(modelIndex).name, Me.theMdlFileData48.theBodyParts.Count, Me.theMdlFileData48.theBodyParts(bodyPartIndex).theModels.Count)
		'						smdPathFileName = Path.Combine(modelOutputPath, smdFileName)

		'						Me.NotifySourceModelProgress(ProgressOptions.WritingSmdFileStarted, smdPathFileName)
		'						'NOTE: Check here in case writing is canceled in the above event.
		'						If Me.theWritingIsCanceled Then
		'							status = StatusMessage.Canceled
		'							Return status
		'						ElseIf Me.theWritingSingleFileIsCanceled Then
		'							status = StatusMessage.Skipped
		'							Exit For
		'						End If

		'						Me.WriteMeshSmdFile(smdPathFileName, lodIndex, aVtxModel, aModel, bodyPartVertexIndexStart)

		'						Me.NotifySourceModelProgress(ProgressOptions.WritingSmdFileFinished, smdPathFileName)
		'					Next

		'					bodyPartVertexIndexStart += aModel.vertexCount
		'				End If
		'			Next
		'		End If
		'	Next
		'End If
		Dim aBodyPart As SourceMdlBodyPart14
		Dim aBodyModel As SourceMdlModel14
		'Dim smdFileName As String
		Dim smdPathFileName As String
		'Dim aVertex As SourceVector
		If Me.theMdlFileData.theBodyParts IsNot Nothing Then
			For bodyPartIndex As Integer = 0 To Me.theMdlFileData.theBodyParts.Count - 1
				aBodyPart = Me.theMdlFileData.theBodyParts(bodyPartIndex)

				If aBodyPart.theModels IsNot Nothing Then
					For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
						aBodyModel = aBodyPart.theModels(modelIndex)
						If aBodyModel.theName = "blank" Then
							Continue For
						End If

						aBodyModel.theSmdFileName = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileName, bodyPartIndex, modelIndex, 0, Me.theName, aBodyModel.theName)
						smdPathFileName = Path.Combine(modelOutputPath, aBodyModel.theSmdFileName)

						Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, smdPathFileName)
						'NOTE: Check here in case writing is canceled in the above event.
						If Me.theWritingIsCanceled Then
							status = StatusMessage.Canceled
							Return status
						ElseIf Me.theWritingSingleFileIsCanceled Then
							Me.theWritingSingleFileIsCanceled = False
							Continue For
						End If

						Me.WriteMeshSmdFile(smdPathFileName, aBodyModel)

						Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, smdPathFileName)
						'If aBodyModel.theVertexes IsNot Nothing Then
						'	For vertexIndex As Integer = 0 To aBodyModel.theVertexes.Count - 1
						'		aVertex = aBodyModel.theVertexes(vertexIndex)

						'	Next
						'End If
					Next
				End If
			Next
		End If

		Return status
	End Function

	Public Overrides Function WriteBoneAnimationSmdFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim aSequenceDesc As SourceMdlSequenceDesc10
		Dim smdPath As String
		'Dim smdFileName As String
		Dim smdPathFileName As String

		Try
			For aSequenceIndex As Integer = 0 To Me.theMdlFileData.theSequences.Count - 1
				aSequenceDesc = Me.theMdlFileData.theSequences(aSequenceIndex)

				For blendIndex As Integer = 0 To aSequenceDesc.blendCount - 1
					If aSequenceDesc.blendCount = 1 Then
						aSequenceDesc.theSmdRelativePathFileNames(blendIndex) = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileNames(blendIndex), Me.theName, aSequenceDesc.theName, -1)
					Else
						aSequenceDesc.theSmdRelativePathFileNames(blendIndex) = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileNames(blendIndex), Me.theName, aSequenceDesc.theName, blendIndex)
					End If

					smdPathFileName = Path.Combine(modelOutputPath, aSequenceDesc.theSmdRelativePathFileNames(blendIndex))
					smdPath = FileManager.GetPath(smdPathFileName)
					If FileManager.PathExistsAfterTryToCreate(smdPath) Then
						Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, smdPathFileName)
						'NOTE: Check here in case writing is canceled in the above event.
						If Me.theWritingIsCanceled Then
							status = StatusMessage.Canceled
							Return status
						ElseIf Me.theWritingSingleFileIsCanceled Then
							Me.theWritingSingleFileIsCanceled = False
							Continue For
						End If

						Me.WriteBoneAnimationSmdFile(smdPathFileName, aSequenceDesc, blendIndex)

						Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, smdPathFileName)
					End If
				Next
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return status
	End Function

	'Public Overrides Function WriteTextureFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
	'	Dim status As AppEnums.StatusMessage = StatusMessage.Success

	'	Dim aTextureList As List(Of SourceMdlTexture14)
	'	If Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0 Then
	'		aTextureList = Me.theMdlFileData.theTextures
	'	ElseIf Me.theTextureMdlFileData10 IsNot Nothing Then
	'		aTextureList = Me.theTextureMdlFileData10.theTextures
	'	Else
	'		Return StatusMessage.Error
	'	End If

	'	Dim texturePath As String
	'	Dim texturePathFileName As String
	'	Dim aTexture As SourceMdlTexture14
	'	For textureIndex As Integer = 0 To aTextureList.Count - 1
	'		Try
	'			aTexture = aTextureList(textureIndex)
	'			texturePathFileName = Path.Combine(modelOutputPath, aTexture.theFileName)
	'			texturePath = FileManager.GetPath(texturePathFileName)
	'			If FileManager.PathExistsAfterTryToCreate(texturePath) Then
	'				Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, texturePathFileName)
	'				'NOTE: Check here in case writing is canceled in the above event.
	'				If Me.theWritingIsCanceled Then
	'					status = StatusMessage.Canceled
	'					Return status
	'				ElseIf Me.theWritingSingleFileIsCanceled Then
	'					Me.theWritingSingleFileIsCanceled = False
	'					Continue For
	'				End If

	'				Dim aBitmap As New BitmapFile(texturePathFileName, aTexture.width, aTexture.height, aTexture.theData)
	'				aBitmap.Write()

	'				Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, texturePathFileName)
	'			End If
	'		Catch ex As Exception
	'			status = StatusMessage.Error
	'		End Try
	'	Next

	'	Return status
	'End Function

	Public Overloads Function WriteBoneAnimationSmdFile(ByVal smdPathFileName As String, ByVal aSequence As SourceMdlSequenceDesc10, ByVal blendIndex As Integer) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Me.theOutputFileTextWriter = File.CreateText(smdPathFileName)

			Dim smdFile As New SourceSmdFile14(Me.theOutputFileTextWriter, Me.theMdlFileData)

			'smdFile.WriteHeaderComment()

			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection()
			smdFile.WriteSkeletonSectionForAnimation(aSequence, blendIndex)
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theOutputFileTextWriter IsNot Nothing Then
				Me.theOutputFileTextWriter.Flush()
				Me.theOutputFileTextWriter.Close()
			End If
		End Try

		Return status
	End Function

	'Public Overrides Function WriteTextureFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
	'	Dim status As AppEnums.StatusMessage = StatusMessage.Success

	'	Dim aTextureList As List(Of SourceMdlTexture14)
	'	If Me.theMdlFileData10.textureCount > 0 Then
	'		aTextureList = Me.theMdlFileData10.theTextures
	'	ElseIf Me.theTextureMdlFileData10 IsNot Nothing Then
	'		aTextureList = Me.theTextureMdlFileData10.theTextures
	'	Else
	'		Return StatusMessage.Error
	'	End If

	'	Dim texturePathFileName As String
	'	Dim aTexture As SourceMdlTexture14
	'	Dim managedDataArray As Byte()
	'	Dim size As Integer
	'	Dim unmanagedDataPointer As IntPtr
	'	Dim newBitmap As Bitmap
	'	For textureIndex As Integer = 0 To aTextureList.Count - 1
	'		Try
	'			aTexture = aTextureList(textureIndex)
	'			texturePathFileName = Path.Combine(modelOutputPath, aTexture.theFileName)

	'			managedDataArray = aTexture.theData.ToArray()

	'			size = Marshal.SizeOf(managedDataArray(0)) * managedDataArray.Length
	'			unmanagedDataPointer = Marshal.AllocHGlobal(size)
	'			Marshal.Copy(managedDataArray, 0, unmanagedDataPointer, managedDataArray.Length)
	'			newBitmap = New Bitmap(aTexture.width, aTexture.height, 8 * aTexture.width, System.Drawing.Imaging.PixelFormat.Format32bppRgb, unmanagedDataPointer)
	'			newBitmap.Save(texturePathFileName, System.Drawing.Imaging.ImageFormat.Bmp)
	'		Catch ex As Exception
	'			status = StatusMessage.Error
	'		Finally
	'			Marshal.FreeHGlobal(unmanagedDataPointer)
	'		End Try
	'	Next

	'	Return status
	'End Function

	Public Overrides Function WriteAccessedBytesDebugFiles(ByVal debugPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim debugPathFileName As String

		If Me.theMdlFileData IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugMdlFileNameSuffix)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theMdlFileData.theFileSeekLog)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		End If

		'If Me.theSequenceGroupMdlFileDatas10 IsNot Nothing Then
		'	Dim fileName As String
		'	Dim fileNameWithoutExtension As String
		'	Dim fileExtension As String
		'	For i As Integer = 0 To Me.theSequenceGroupMdlFileDatas10.Count - 1
		'		fileName = Me.theName + " " + My.Resources.Decompile_DebugSequenceGroupMDLFileNameSuffix
		'		fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName)
		'		fileExtension = Path.GetExtension(fileName)
		'		debugPathFileName = Path.Combine(debugPath, fileNameWithoutExtension + (i + 1).ToString("00") + fileExtension)
		'		Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
		'		Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theSequenceGroupMdlFileDatas10(i).theFileSeekLog)
		'		Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		'	Next
		'End If

		'If Me.theTextureMdlFileData10 IsNot Nothing Then
		'	debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugTextureMDLFileNameSuffix)
		'	Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
		'	Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theTextureMdlFileData10.theFileSeekLog)
		'	Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		'End If

		Return status
	End Function

	Public Overrides Function GetTextureFileNames() As List(Of String)
		Dim textureFileNames As New List(Of String)()

		For i As Integer = 0 To Me.theMdlFileData.theTextures.Count - 1
			Dim aTexture As SourceMdlTexture14
			aTexture = Me.theMdlFileData.theTextures(i)

			textureFileNames.Add(aTexture.theFileName)
		Next

		Return textureFileNames
	End Function

#End Region

#Region "Private Methods"

	Protected Overrides Sub ReadMdlFileHeader_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData14()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile14(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()
	End Sub

	Protected Overrides Sub ReadMdlFileForViewer_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData14()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile14(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()

		''mdlFile.ReadTexturePaths()
		'mdlFile.ReadTextures()
	End Sub

	Protected Overrides Sub ReadMdlFile_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData14()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Me.theMdlFileData.theFileName = Me.theName
		Dim mdlFile As New SourceMdlFile14(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()

		mdlFile.ReadBones()
		mdlFile.ReadBoneControllers()
		mdlFile.ReadAttachments()
		mdlFile.ReadHitboxes()

		'NOTE: Must read sequences before reading animations.
		mdlFile.ReadSequences()
		mdlFile.ReadSequenceGroups()
		'mdlFile.ReadTransitions()

		mdlFile.ReadAnimations(0)

		mdlFile.ReadBodyParts()
		mdlFile.ReadIndexes()
		mdlFile.ReadVertexes()
		mdlFile.ReadNormals()
		mdlFile.ReadUVs()
		mdlFile.ReadWeightingWeights()
		mdlFile.ReadWeightingBones()

		mdlFile.ReadTextures()
		mdlFile.ReadSkins()

		mdlFile.ReadUnreadBytes()

		' Post-processing.
		'mdlFile.BuildBoneTransforms()
	End Sub

	'Protected Overrides Sub ReadSequenceGroupMdlFile(ByVal sequenceGroupIndex As Integer)
	'	If Me.theSequenceGroupMdlFileDatas10 Is Nothing Then
	'		Me.theSequenceGroupMdlFileDatas10 = New List(Of SourceMdlFileData14)()
	'	End If

	'	Dim aSequenceGroupMdlFileData10 As New SourceMdlFileData14()
	'	'NOTE: Need some data from the main MDL file.
	'	aSequenceGroupMdlFileData10.theBones = Me.theMdlFileData.theBones
	'	aSequenceGroupMdlFileData10.theSequences = Me.theMdlFileData.theSequences

	'	Dim sequenceGroupMdlFile As New SourceMdlFile10(Me.theInputFileReader, aSequenceGroupMdlFileData10)

	'	sequenceGroupMdlFile.ReadSequenceGroupMdlHeader()
	'	Me.theMdlFileData.theSequenceGroupFileHeaders(sequenceGroupIndex).theActualFileSize = aSequenceGroupMdlFileData10.theActualFileSize
	'	sequenceGroupMdlFile.ReadAnimations(sequenceGroupIndex)

	'	Me.theSequenceGroupMdlFileDatas10.Add(aSequenceGroupMdlFileData10)
	'End Sub

	'Protected Overrides Sub ReadTextureMdlFile_Internal()
	'	If Me.theTextureMdlFileData10 Is Nothing Then
	'		Me.theTextureMdlFileData10 = New SourceMdlFileData14()
	'	End If

	'	Dim textureMdlFile As New SourceMdlFile10(Me.theInputFileReader, Me.theTextureMdlFileData10)

	'	textureMdlFile.ReadMdlHeader()
	'	textureMdlFile.ReadTextures()
	'	textureMdlFile.ReadSkins()

	'	If Me.theMdlFileData.theTextures Is Nothing Then
	'		Me.theExternalTexturesAreUsed = True
	'	End If
	'End Sub

	Protected Overrides Sub WriteQcFile()
		'If Me.theExternalTexturesAreUsed Then
		'	Me.theMdlFileData.skinReferenceCount = Me.theTextureMdlFileData10.skinReferenceCount
		'	Me.theMdlFileData.skinFamilyCount = Me.theTextureMdlFileData10.skinFamilyCount
		'	Me.theMdlFileData.theSkinFamilies = Me.theTextureMdlFileData10.theSkinFamilies
		'	Me.theMdlFileData.theTextures = Me.theTextureMdlFileData10.theTextures
		'End If

		Dim qcFile As New SourceQcFile14(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.theName)

		Try
			qcFile.WriteHeaderComment()

			qcFile.WriteModelNameCommand()
			'qcFile.WriteCDCommand()
			'qcFile.WriteCDTextureCommand()
			'qcFile.WriteClipToTexturesCommand()
			'qcFile.WriteScaleCommand()

			qcFile.WriteBodyGroupCommand()

			qcFile.WriteFlagsCommand()
			qcFile.WriteEyePositionCommand()

			'qcFile.WriteExternalTexturesCommand()
			'qcFile.WriteTextureGroupCommand()
			'If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			'	qcFile.WriteTextureFileNameComments()
			'End If
			'qcFile.WriteTexRenderMode()

			'qcFile.WriteAttachmentCommand()

			qcFile.WriteCBoxCommand()
			qcFile.WriteBBoxCommand()
			'qcFile.WriteHBoxCommands()

			'qcFile.WriteControllerCommand()

			'qcFile.WriteSequenceGroupSizeCommand()
			'qcFile.WriteSequenceGroupCommands()
			'qcFile.WriteSequenceCommands()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try

		'If Me.theExternalTexturesAreUsed Then
		'	Me.theMdlFileData.skinReferenceCount = 0
		'	Me.theMdlFileData.skinFamilyCount = 0
		'	Me.theMdlFileData.theSkinFamilies = Nothing
		'	Me.theMdlFileData.theTextures = Nothing
		'End If
	End Sub

	Protected Overloads Function WriteMeshSmdFile(ByVal smdPathFileName As String, ByVal aModel As SourceMdlModel14) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Me.theOutputFileTextWriter = File.CreateText(smdPathFileName)

			Me.WriteMeshSmdFile(aModel)
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theOutputFileTextWriter IsNot Nothing Then
				Me.theOutputFileTextWriter.Flush()
				Me.theOutputFileTextWriter.Close()
			End If
		End Try

		Return status
	End Function

	Protected Overloads Sub WriteMeshSmdFile(ByVal aModel As SourceMdlModel14)
		'If Me.theExternalTexturesAreUsed Then
		'	Me.theMdlFileData.theTextures = Me.theTextureMdlFileData10.theTextures
		'End If

		Dim smdFile As New SourceSmdFile14(Me.theOutputFileTextWriter, Me.theMdlFileData)

		Try
			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection()
			smdFile.WriteSkeletonSection()
			smdFile.WriteTrianglesSection(aModel)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		'If Me.theExternalTexturesAreUsed Then
		'	Me.theMdlFileData.theTextures = Nothing
		'End If
	End Sub

	Protected Overrides Sub WriteMdlFileNameToMdlFile(ByVal internalMdlFileName As String)
		Dim mdlFile As New SourceMdlFile14(Me.theOutputFileBinaryWriter, Me.theMdlFileData)

		mdlFile.WriteInternalMdlFileName(internalMdlFileName)
	End Sub

#End Region

#Region "Data"

	Private theMdlFileData As SourceMdlFileData14
	'Private theSequenceGroupMdlFileDatas10 As List(Of SourceMdlFileData14)
	'Private theTextureMdlFileData10 As SourceMdlFileData14
	'Private theExternalTexturesAreUsed As Boolean

#End Region

End Class
