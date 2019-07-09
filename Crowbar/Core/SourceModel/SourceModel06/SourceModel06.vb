Imports System.IO

' Example: barney from HLAlpha
Public Class SourceModel06
	Inherits SourceModel04

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlPathFileName As String, ByVal mdlVersion As Integer)
		MyBase.New(mdlPathFileName, mdlVersion)
	End Sub

#End Region

#Region "Properties"

	Public Overrides ReadOnly Property HasTextureData As Boolean
		Get
			Return Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0
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
			Return Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overrides Function CheckForRequiredFiles() As FilesFoundFlags
		Dim status As AppEnums.FilesFoundFlags = FilesFoundFlags.AllFilesFound

		Return status
	End Function

	Public Overrides Function WriteReferenceMeshFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim aBodyPart As SourceMdlBodyPart06
		Dim aBodyModel As SourceMdlModel06
		'Dim smdFileName As String
		Dim smdPathFileName As String
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
					Next
				End If
			Next
		End If

		Return status
	End Function

	Public Overrides Function WriteBoneAnimationSmdFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim aSequenceDesc As SourceMdlSequenceDesc06
		Dim smdPath As String
		'Dim smdFileName As String
		Dim smdPathFileName As String

		Try
			For aSequenceIndex As Integer = 0 To Me.theMdlFileData.theSequences.Count - 1
				aSequenceDesc = Me.theMdlFileData.theSequences(aSequenceIndex)

				For blendIndex As Integer = 0 To aSequenceDesc.blendCount - 1
					If aSequenceDesc.blendCount = 1 Then
						aSequenceDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileName, Me.theName, aSequenceDesc.theName, -1)
					Else
						aSequenceDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileName, Me.theName, aSequenceDesc.theName, blendIndex)
					End If

					smdPathFileName = Path.Combine(modelOutputPath, aSequenceDesc.theSmdRelativePathFileName)
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

	Public Overrides Function WriteTextureFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim aTextureList As List(Of SourceMdlTexture06)
		If Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0 Then
			aTextureList = Me.theMdlFileData.theTextures
		Else
			Return StatusMessage.Error
		End If

		Dim texturePath As String
		Dim texturePathFileName As String
		Dim aTexture As SourceMdlTexture06
		For textureIndex As Integer = 0 To aTextureList.Count - 1
			Try
				aTexture = aTextureList(textureIndex)
				texturePathFileName = Path.Combine(modelOutputPath, aTexture.theFileName)
				texturePath = FileManager.GetPath(texturePathFileName)
				If FileManager.PathExistsAfterTryToCreate(texturePath) Then
					Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, texturePathFileName)
					'NOTE: Check here in case writing is canceled in the above event.
					If Me.theWritingIsCanceled Then
						status = StatusMessage.Canceled
						Return status
					ElseIf Me.theWritingSingleFileIsCanceled Then
						Me.theWritingSingleFileIsCanceled = False
						Continue For
					End If

					Dim aBitmap As New BitmapFile(texturePathFileName, aTexture.width, aTexture.height, aTexture.theData)
					aBitmap.Write()

					Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, texturePathFileName)
				End If
			Catch ex As Exception
				status = StatusMessage.Error
			End Try
		Next

		Return status
	End Function

	Public Overrides Function WriteAccessedBytesDebugFiles(ByVal debugPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim debugPathFileName As String

		If Me.theMdlFileData IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugMdlFileNameSuffix)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theMdlFileData.theFileSeekLog)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		End If

		Return status
	End Function

	Public Overrides Function GetTextureFileNames() As List(Of String)
		Dim textureFileNames As New List(Of String)()

		For i As Integer = 0 To Me.theMdlFileData.theTextures.Count - 1
			Dim aTexture As SourceMdlTexture06
			aTexture = Me.theMdlFileData.theTextures(i)

			textureFileNames.Add(aTexture.theFileName)
		Next

		Return textureFileNames
	End Function

#End Region

#Region "Private Methods"

	Protected Overrides Sub ReadMdlFileHeader_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData06()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile06(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()
	End Sub

	Protected Overrides Sub ReadMdlFileForViewer_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData06()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile06(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()

		'mdlFile.ReadTexturePaths()
		mdlFile.ReadTextures()
	End Sub

	Protected Overrides Sub ReadMdlFile_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData06()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile06(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()

		mdlFile.ReadBones()
		mdlFile.ReadBoneControllers()

		'NOTE: Must read sequences before reading animations.
		mdlFile.ReadSequences()
		mdlFile.ReadAnimations()

		mdlFile.ReadBodyParts()

		mdlFile.ReadTextures()
		mdlFile.ReadSkins()

		mdlFile.ReadUnreadBytes()

		' Post-processing.
		mdlFile.GetBoneDataFromFirstSequenceFirstFrame()
		mdlFile.BuildBoneTransforms()
	End Sub

	Protected Overrides Sub WriteQcFile()
		Dim qcFile As New SourceQcFile06(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.theName)

		Try
			qcFile.WriteHeaderComment()

			qcFile.WriteModelNameCommand()

			qcFile.WriteBodyGroupCommand()

			'If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			'	qcFile.WriteTextureFileNameComments()
			'End If

			qcFile.WriteControllerCommand()

			qcFile.WriteSequenceCommands()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Protected Overloads Function WriteMeshSmdFile(ByVal smdPathFileName As String, ByVal aModel As SourceMdlModel06) As AppEnums.StatusMessage
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

	Protected Overloads Sub WriteMeshSmdFile(ByVal aModel As SourceMdlModel06)
		Dim externalTexturesAreUsed As Boolean = False

		Dim smdFile As New SourceSmdFile06(Me.theOutputFileTextWriter, Me.theMdlFileData)

		Try
			smdFile.WriteHeaderComment()

			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection()
			smdFile.WriteSkeletonSection()
			smdFile.WriteTrianglesSection(aModel)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		If externalTexturesAreUsed Then
			Me.theMdlFileData.theTextures = Nothing
		End If
	End Sub

	Public Overloads Function WriteBoneAnimationSmdFile(ByVal smdPathFileName As String, ByVal aSequence As SourceMdlSequenceDesc06, ByVal blendIndex As Integer) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Me.theOutputFileTextWriter = File.CreateText(smdPathFileName)

			Dim smdFile As New SourceSmdFile06(Me.theOutputFileTextWriter, Me.theMdlFileData)

			smdFile.WriteHeaderComment()

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

	Protected Overrides Sub WriteMdlFileNameToMdlFile(ByVal internalMdlFileName As String)
		Dim mdlFile As New SourceMdlFile06(Me.theOutputFileBinaryWriter, Me.theMdlFileData)

		mdlFile.WriteInternalMdlFileName(internalMdlFileName)
	End Sub

#End Region

#Region "Data"

	Private theMdlFileData As SourceMdlFileData06
	'Private theTextureMdlFileData10 As SourceMdlFileData06

#End Region

End Class
