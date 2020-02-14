Imports System.IO

' Example: PLAYER from HLAlpha
Public Class SourceModel04
	Inherits SourceModel

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlPathFileName As String, ByVal mdlVersion As Integer)
		MyBase.New(mdlPathFileName, mdlVersion)
	End Sub

#End Region

#Region "Properties"

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
			If Me.theMdlFileData.theSequenceDescs IsNot Nothing _
			 AndAlso Me.theMdlFileData.theSequenceDescs.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasTextureFileData As Boolean
		Get
			Return True
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

		Dim aBodyPart As SourceMdlBodyPart04
		Dim aBodyModel As SourceMdlModel04
		'Dim smdFileName As String
		Dim smdPathFileName As String
		If Me.theMdlFileData.theBodyParts IsNot Nothing Then
			For bodyPartIndex As Integer = 0 To Me.theMdlFileData.theBodyParts.Count - 1
				aBodyPart = Me.theMdlFileData.theBodyParts(bodyPartIndex)

				If aBodyPart.theModels IsNot Nothing Then
					For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
						aBodyModel = aBodyPart.theModels(modelIndex)

						aBodyModel.theSmdFileName = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileName, bodyPartIndex, modelIndex, 0, Me.theName, "")
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

		Dim aSequenceDesc As SourceMdlSequenceDesc04
		Dim smdPath As String
		Dim smdPathFileName As String

		Try
			For aSequenceIndex As Integer = 0 To Me.theMdlFileData.theSequenceDescs.Count - 1
				aSequenceDesc = Me.theMdlFileData.theSequenceDescs(aSequenceIndex)

				aSequenceDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileName, Me.theName, aSequenceDesc.theName, -1)

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

					Me.WriteBoneAnimationSmdFile(smdPathFileName, aSequenceDesc)

					Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, smdPathFileName)
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return status
	End Function

	Public Overrides Function WriteTextureFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim aBodyPart As SourceMdlBodyPart04
		Dim aModel As SourceMdlModel04
		Dim aMesh As SourceMdlMesh04
		Dim texturePath As String
		Dim textureFileName As String
		Dim texturePathFileName As String
		For bodyPartIndex As Integer = 0 To Me.theMdlFileData.theBodyParts.Count - 1
			aBodyPart = Me.theMdlFileData.theBodyParts(bodyPartIndex)
			For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
				aModel = aBodyPart.theModels(modelIndex)
				For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
					aMesh = aModel.theMeshes(meshIndex)
					Try
						texturePath = modelOutputPath
						'textureFileName = "bodypart" + bodyPartIndex.ToString() + "_model" + modelIndex.ToString() + "_mesh" + meshIndex.ToString() + ".bmp"
						textureFileName = aMesh.theTextureFileName
						texturePathFileName = Path.Combine(texturePath, textureFileName)
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

							Dim aBitmap As New BitmapFile(texturePathFileName, aMesh.textureWidth, aMesh.textureHeight, aMesh.theTextureBmpData)
							aBitmap.Write()

							Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, texturePathFileName)
						End If
					Catch ex As Exception
						status = StatusMessage.Error
					End Try
				Next
			Next
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

#End Region

#Region "Private Methods"

	Protected Overrides Sub ReadMdlFileHeader_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData04()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile04(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()
	End Sub

	Protected Overrides Sub ReadMdlFileForViewer_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData04()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If


		Dim mdlFile As New SourceMdlFile04(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()
	End Sub

	Protected Overrides Sub ReadMdlFile_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData04()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If


		Dim mdlFile As New SourceMdlFile04(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()

		mdlFile.ReadBones()
		mdlFile.ReadSequenceDescs()
		mdlFile.ReadBodyParts()

		mdlFile.ReadUnreadBytes()

		' Post-processing.
		'mdlFile.GetBoneDataFromFirstSequenceFirstFrame()
	End Sub

	Protected Overloads Function WriteMeshSmdFile(ByVal smdPathFileName As String, ByVal aModel As SourceMdlModel04) As AppEnums.StatusMessage
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

	Protected Overloads Sub WriteMeshSmdFile(ByVal aModel As SourceMdlModel04)
		Dim externalTexturesAreUsed As Boolean = False

		Dim smdFile As New SourceSmdFile04(Me.theOutputFileTextWriter, Me.theMdlFileData)

		Try
			smdFile.WriteHeaderComment()

			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection()
			smdFile.WriteSkeletonSection()
			smdFile.WriteTrianglesSection(aModel)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Public Overloads Function WriteBoneAnimationSmdFile(ByVal smdPathFileName As String, ByVal aSequence As SourceMdlSequenceDesc04) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Me.theOutputFileTextWriter = File.CreateText(smdPathFileName)

			Dim smdFile As New SourceSmdFile04(Me.theOutputFileTextWriter, Me.theMdlFileData)

			smdFile.WriteHeaderComment()

			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection()
			smdFile.WriteSkeletonSectionForAnimation(aSequence)
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

#End Region

#Region "Data"

	Private theMdlFileData As SourceMdlFileData04

#End Region

End Class
