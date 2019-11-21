Imports System.IO

Public Class SourceModel32
	Inherits SourceModel31

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlPathFileName As String, ByVal mdlVersion As Integer)
		MyBase.New(mdlPathFileName, mdlVersion)
	End Sub

#End Region

#Region "Properties"

	Public Overrides ReadOnly Property AniFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	'TODO: Delete after reading phy file is implemented.
	'Public Overrides ReadOnly Property PhyFileIsUsed As Boolean
	'	Get
	'		Return False
	'	End Get
	'End Property

	Public Overrides ReadOnly Property VtxFileIsUsed As Boolean
		Get
			Return Not String.IsNullOrEmpty(Me.theVtxPathFileName) AndAlso File.Exists(Me.theVtxPathFileName)
		End Get
	End Property

	Public Overrides ReadOnly Property VvdFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property HasTextureData As Boolean
		Get
			'TODO: Change back to top line after reading texture info from MDL file is implemented.
			'Return Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations AndAlso Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property HasMeshData As Boolean
		Get
			If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
					 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
					 AndAlso Me.theMdlFileData.theBones.Count > 0 _
					 AndAlso Me.theVtxFileData IsNot Nothing Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasLodMeshData As Boolean
		Get
			'If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			'		 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
			'		 AndAlso Me.theMdlFileData.theBones.Count > 0 _
			'		 AndAlso Me.theVtxFileData IsNot Nothing _
			'		 AndAlso Me.theVtxFileData.lodCount > 0 Then
			'	Return True
			'Else
			Return False
			'End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasPhysicsMeshData As Boolean
		Get
			If Me.thePhyFileDataGeneric IsNot Nothing _
			 AndAlso Me.thePhyFileDataGeneric.theSourcePhyCollisionDatas IsNot Nothing _
			 AndAlso Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
			 AndAlso Me.theMdlFileData.theBones.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasProceduralBonesData As Boolean
		Get
			'If Me.theMdlFileData IsNot Nothing _
			' AndAlso Me.theMdlFileData.theProceduralBonesCommandIsUsed _
			' AndAlso Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			' AndAlso Me.theMdlFileData.theBones IsNot Nothing _
			' AndAlso Me.theMdlFileData.theBones.Count > 0 Then
			'	Return True
			'Else
			Return False
			'End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasBoneAnimationData As Boolean
		Get
			If Me.theMdlFileData.theAnimationDescs IsNot Nothing _
			 AndAlso Me.theMdlFileData.theAnimationDescs.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasVertexAnimationData As Boolean
		Get
			'TODO: Change back to commented-out lines once implemented.
			'If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			' AndAlso Me.theMdlFileData.theFlexDescs IsNot Nothing _
			' AndAlso Me.theMdlFileData.theFlexDescs.Count > 0 Then
			'	Return True
			'Else
			'	Return False
			'End If
			Return False
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overrides Function CheckForRequiredFiles() As FilesFoundFlags
		Dim status As AppEnums.FilesFoundFlags = FilesFoundFlags.AllFilesFound

		If Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations Then
			Me.thePhyPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".phy")

			Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx11.vtx")
			If Not File.Exists(Me.theVtxPathFileName) Then
				Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx90.vtx")
				If Not File.Exists(Me.theVtxPathFileName) Then
					Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx80.vtx")
					If Not File.Exists(Me.theVtxPathFileName) Then
						Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".sw.vtx")
						If Not File.Exists(Me.theVtxPathFileName) Then
							Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".vtx")
							If Not File.Exists(Me.theVtxPathFileName) Then
								status = FilesFoundFlags.ErrorRequiredVtxFileNotFound
							End If
						End If
					End If
				End If
			End If
		End If

		Return status
	End Function

	Public Overrides Function ReadPhyFile() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		'If String.IsNullOrEmpty(Me.thePhyPathFileName) Then
		'	status = Me.CheckForRequiredFiles()
		'End If

		If Not String.IsNullOrEmpty(Me.thePhyPathFileName) Then
			If status = StatusMessage.Success Then
				Try
					Me.ReadFile(Me.thePhyPathFileName, AddressOf Me.ReadPhyFile_Internal)
					If Me.thePhyFileDataGeneric.checksum <> Me.theMdlFileData.checksum Then
						'status = StatusMessage.WarningPhyChecksumDoesNotMatchMdl
						Me.NotifySourceModelProgress(ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum, "")
					End If
				Catch ex As Exception
					status = StatusMessage.Error
				End Try
			End If
		End If

		Return status
	End Function

	Public Overrides Function WriteReferenceMeshFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = Me.WriteMeshSmdFiles(modelOutputPath, 0, 0)

		Return status
	End Function

	Public Overrides Function WriteLodMeshFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = Me.WriteMeshSmdFiles(modelOutputPath, 1, Me.theVtxFileData.lodCount - 1)

		Return status
	End Function

	Public Overloads Function WriteMeshSmdFile(ByVal smdPathFileName As String, ByVal lodIndex As Integer, ByVal aVtxModel As SourceVtxModel06, ByVal aModel As SourceMdlModel37, ByVal bodyPartVertexIndexStart As Integer) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Me.theOutputFileTextWriter = File.CreateText(smdPathFileName)
			Dim smdFile As New SourceSmdFile32(Me.theOutputFileTextWriter, Me.theMdlFileData)

			smdFile.WriteHeaderComment()

			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection(lodIndex)
			smdFile.WriteSkeletonSection(lodIndex)
			smdFile.WriteTrianglesSection(lodIndex, aVtxModel, aModel, bodyPartVertexIndexStart)
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

	Public Overrides Function WriteBoneAnimationSmdFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim anAnimationDesc As SourceMdlAnimationDesc32
		Dim smdPath As String
		'Dim smdFileName As String
		Dim smdPathFileName As String
		Dim writeStatus As String

		Try
			For anAnimDescIndex As Integer = 0 To Me.theMdlFileData.theAnimationDescs.Count - 1
				anAnimationDesc = Me.theMdlFileData.theAnimationDescs(anAnimDescIndex)

				anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, Me.Name, anAnimationDesc.theName)
				smdPathFileName = Path.Combine(modelOutputPath, anAnimationDesc.theSmdRelativePathFileName)
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

					writeStatus = Me.WriteBoneAnimationSmdFile(smdPathFileName, Nothing, anAnimationDesc)

					If writeStatus = "Success" Then
						Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, smdPathFileName)
					Else
						Me.NotifySourceModelProgress(ProgressOptions.WritingFileFailed, writeStatus)
					End If
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

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

		If Me.theVtxFileData IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugVtxFileNameSuffix)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theVtxFileData.theFileSeekLog)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		End If

		If Me.thePhyFileDataGeneric IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugPhyFileNameSuffix)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.thePhyFileDataGeneric.theFileSeekLog)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		End If

		Return status
	End Function

#End Region

#Region "Private Methods"

	Protected Overrides Sub ReadMdlFileHeader_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData32()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile32(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")
	End Sub

	Protected Overrides Sub ReadMdlFileForViewer_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData32()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile32(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")

		''mdlFile.ReadTexturePaths()
		'mdlFile.ReadTextures()
	End Sub

	Protected Overrides Sub ReadMdlFile_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData32()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile32(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")

		' Read what WriteBoneInfo() writes.
		mdlFile.ReadBones()
		mdlFile.ReadBoneControllers()
		mdlFile.ReadAttachments()
		mdlFile.ReadHitboxSets()

		' Read what WriteSequenceInfo() writes.
		'NOTE: Must read sequences before reading animations.
		mdlFile.ReadSequences()
		mdlFile.ReadSequenceGroups()
		mdlFile.ReadTransitions()

		' Read what WriteAnimations() writes.
		mdlFile.ReadLocalAnimationDescs()

		' Read what WriteModel() writes.
		mdlFile.ReadBodyParts()
		mdlFile.ReadFlexDescs()
		mdlFile.ReadFlexControllers()
		'NOTE: This must be after flex descs are read so that flex desc usage can be saved in flex desc.
		mdlFile.ReadFlexRules()
		mdlFile.ReadIkChains()
		mdlFile.ReadIkLocks()
		mdlFile.ReadMouths()
		mdlFile.ReadPoseParamDescs()

		' Read what WriteTextures() writes.
		mdlFile.ReadTexturePaths()
		'NOTE: ReadTextures must be after ReadTexturePaths(), so it can compare with the texture paths.
		mdlFile.ReadTextures()
		mdlFile.ReadSkinFamilies()

		'' Read what WriteKeyValues() writes.
		'mdlFile.ReadKeyValues()

		'mdlFile.ReadFinalBytesAlignment()
		mdlFile.ReadUnreadBytes()

		'' Post-processing.
		'mdlFile.BuildBoneTransforms()
	End Sub

	Protected Overrides Sub ReadPhyFile_Internal()
		If Me.thePhyFileDataGeneric Is Nothing Then
			Me.thePhyFileDataGeneric = New SourcePhyFileData()
		End If

		Dim phyFile As New SourcePhyFile(Me.theInputFileReader, Me.thePhyFileDataGeneric)

		phyFile.ReadSourcePhyHeader()
		If Me.thePhyFileDataGeneric.solidCount > 0 Then
			phyFile.ReadSourceCollisionData()
			phyFile.CalculateVertexNormals()
			phyFile.ReadSourcePhysCollisionModels()
			phyFile.ReadSourcePhyRagdollConstraintDescs()
			phyFile.ReadSourcePhyCollisionRules()
			phyFile.ReadSourcePhyEditParamsSection()
			phyFile.ReadCollisionTextSection()
		End If
		phyFile.ReadUnreadBytes()
	End Sub

	Protected Overrides Sub ReadVtxFile_Internal()
		If Me.theVtxFileData Is Nothing Then
			Me.theVtxFileData = New SourceVtxFileData06()
		End If

		Dim vtxFile As New SourceVtxFile06(Me.theInputFileReader, Me.theVtxFileData)

		vtxFile.ReadSourceVtxHeader()
		'If Me.theVtxFileData.lodCount > 0 Then
		vtxFile.ReadSourceVtxBodyParts()
		'End If
		vtxFile.ReadSourceVtxMaterialReplacementLists()
	End Sub

	Protected Overrides Sub WriteQcFile()
		Dim qcFile As New SourceQcFile32(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.thePhyFileDataGeneric, Me.theVtxFileData, Me.theName)

		Try
			qcFile.WriteHeaderComment()

			qcFile.WriteModelNameCommand()

			qcFile.WriteStaticPropCommand()

			If Me.theMdlFileData.theModelCommandIsUsed Then
				qcFile.WriteModelCommand()
				qcFile.WriteBodyGroupCommand(1)
			Else
				qcFile.WriteBodyGroupCommand(0)
			End If
			qcFile.WriteGroup("lod", AddressOf qcFile.WriteGroupLod, False, False)

			qcFile.WriteSurfacePropCommand()
			qcFile.WriteJointSurfacePropCommand()
			qcFile.WriteContentsCommand()
			qcFile.WriteJointContentsCommand()
			qcFile.WriteIllumPositionCommand()

			qcFile.WriteEyePositionCommand()
			qcFile.WriteNoForcedFadeCommand()
			qcFile.WriteForcePhonemeCrossfadeCommand()

			qcFile.WriteAmbientBoostCommand()
			qcFile.WriteOpaqueCommand()
			qcFile.WriteObsoleteCommand()
			qcFile.WriteCdMaterialsCommand()
			qcFile.WriteTextureGroupCommand()
			If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				qcFile.WriteTextureFileNameComments()
			End If

			qcFile.WriteAttachmentCommand()

			qcFile.WriteGroup("box", AddressOf qcFile.WriteGroupBox, True, False)

			qcFile.WriteControllerCommand()
			qcFile.WriteScreenAlignCommand()

			qcFile.WriteGroup("bone", AddressOf qcFile.WriteGroupBone, False, False)

			qcFile.WriteGroup("animation", AddressOf qcFile.WriteGroupAnimation, False, False)

			qcFile.WriteGroup("collision", AddressOf qcFile.WriteGroupCollision, False, False)

			'qcFile.WriteKeyValues(Me.theMdlFileData.theKeyValuesText, "$KeyValues")
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Protected Overrides Function WriteMeshSmdFiles(ByVal modelOutputPath As String, ByVal lodStartIndex As Integer, ByVal lodStopIndex As Integer) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		'Dim smdFileName As String
		Dim smdPathFileName As String
		Dim aBodyPart As SourceVtxBodyPart06
		Dim aVtxModel As SourceVtxModel06
		Dim aBodyModel As SourceMdlModel37
		Dim bodyPartVertexIndexStart As Integer

		bodyPartVertexIndexStart = 0
		If Me.theVtxFileData.theVtxBodyParts IsNot Nothing AndAlso Me.theMdlFileData.theBodyParts IsNot Nothing Then
			For bodyPartIndex As Integer = 0 To Me.theVtxFileData.theVtxBodyParts.Count - 1
				aBodyPart = Me.theVtxFileData.theVtxBodyParts(bodyPartIndex)

				If aBodyPart.theVtxModels IsNot Nothing Then
					For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
						aVtxModel = aBodyPart.theVtxModels(modelIndex)

						If aVtxModel.theVtxModelLods IsNot Nothing Then
							aBodyModel = Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex)
							If aBodyModel.name(0) = ChrW(0) AndAlso aVtxModel.theVtxModelLods(0).theVtxMeshes Is Nothing Then
								Continue For
							End If

							For lodIndex As Integer = lodStartIndex To lodStopIndex
								'TODO: Why would this count be different than the file header count?
								If lodIndex >= aVtxModel.theVtxModelLods.Count Then
									Exit For
								End If

								Try
									Dim bodyModelName As String
									bodyModelName = Me.theMdlFileData.theSequenceGroups(0).theFileName
									If String.IsNullOrEmpty(bodyModelName) OrElse FileManager.FilePathHasInvalidChars(bodyModelName) Then
										bodyModelName = Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex).name
									End If
									aBodyModel.theSmdFileNames(lodIndex) = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames(lodIndex), bodyPartIndex, modelIndex, lodIndex, Me.theName, bodyModelName)
									smdPathFileName = Path.Combine(modelOutputPath, aBodyModel.theSmdFileNames(lodIndex))

									Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, smdPathFileName)
									'NOTE: Check here in case writing is canceled in the above event.
									If Me.theWritingIsCanceled Then
										status = StatusMessage.Canceled
										Return status
									ElseIf Me.theWritingSingleFileIsCanceled Then
										Me.theWritingSingleFileIsCanceled = False
										Continue For
									End If

									Me.WriteMeshSmdFile(smdPathFileName, lodIndex, aVtxModel, aBodyModel, bodyPartVertexIndexStart)

									Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, smdPathFileName)
								Catch ex As Exception
									Dim debug As Integer = 4242
								End Try
							Next

							bodyPartVertexIndexStart += aBodyModel.vertexCount
						End If
					Next
				End If
			Next
		End If

		Return status
	End Function

	Protected Overrides Sub WritePhysicsMeshSmdFile()
		Dim physicsMeshSmdFile As New SourceSmdFile32(Me.theOutputFileTextWriter, Me.theMdlFileData, Me.thePhyFileDataGeneric)

		Try
			physicsMeshSmdFile.WriteHeaderComment()

			physicsMeshSmdFile.WriteHeaderSection()
			physicsMeshSmdFile.WriteNodesSection(-1)
			physicsMeshSmdFile.WriteSkeletonSection(-1)
			physicsMeshSmdFile.WriteTrianglesSectionForPhysics()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Protected Overrides Sub WriteBoneAnimationSmdFile(ByVal aSequenceDesc As SourceMdlSequenceDescBase, ByVal anAnimationDesc As SourceMdlAnimationDescBase)
		Dim smdFile As New SourceSmdFile32(Me.theOutputFileTextWriter, Me.theMdlFileData)

		Try
			smdFile.WriteHeaderComment()

			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection(-1)
			smdFile.WriteSkeletonSectionForAnimation(aSequenceDesc, anAnimationDesc)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

#End Region

#Region "Data"

	Private theMdlFileData As SourceMdlFileData32
	'Private thePhyFileData As SourcePhyFileData37
	Private theVtxFileData As SourceVtxFileData06

#End Region

End Class
