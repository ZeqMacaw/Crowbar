Imports System.ComponentModel

Public Module AppEnums

	Public Enum InputOptions
		<Description("File")> File
		<Description("Folder")> Folder
		<Description("Folder and subfolders")> FolderRecursion
	End Enum

	Public Enum DownloadOutputPathOptions
		'<Description("Downloads folder")> DownloadsFolder
		<Description("Documents folder")> DocumentsFolder
		<Description("Work folder")> WorkFolder
	End Enum

	Public Enum UnpackOutputPathOptions
		<Description("Same folder (as Package)")> SameFolder
		<Description("Subfolder (of Package)")> Subfolder
		<Description("Work folder")> WorkFolder
		<Description("Game's addons folder")> GameAddonsFolder
	End Enum

	Public Enum UnpackSearchFieldOptions
		<Description("Files")> Files
		<Description("Files and Folders")> FilesAndFolders
	End Enum

	Public Enum DecompileOutputPathOptions
		<Description("Work folder")> WorkFolder
		<Description("Subfolder (of MDL input)")> Subfolder
	End Enum

	Public Enum CompileOutputPathOptions
		<Description("Game's ""models"" folder")> GameModelsFolder
		<Description("Work folder")> WorkFolder
		<Description("Subfolder (of QC input)")> Subfolder
	End Enum

	Public Enum PackInputOptions
		<Description("Folder")> Folder
		<Description("Parent of child folders")> ParentFolder
	End Enum

	Public Enum PackOutputPathOptions
		<Description("Work folder")> WorkFolder
		<Description("Parent folder")> ParentFolder
	End Enum

	Public Enum PublishSearchFieldOptions
		<Description("ID")> ID
		<Description("Owner")> Owner
		<Description("Title")> Title
		<Description("Description")> Description
		<Description("[All fields]")> AllFields
	End Enum

	Public Enum StatusMessage
		<Description("Success")> Success
		<Description("Error")> [Error]
		<Description("Canceled")> Canceled
		<Description("Skipped")> Skipped

		<Description("ErrorUnableToCreateTempFolder")> ErrorUnableToCreateTempFolder

		<Description("ErrorRequiredSequenceGroupMdlFileNotFound")> ErrorRequiredSequenceGroupMdlFileNotFound
		<Description("ErrorRequiredTextureMdlFileNotFound")> ErrorRequiredTextureMdlFileNotFound

		<Description("ErrorRequiredMdlFileNotFound")> ErrorRequiredMdlFileNotFound
		<Description("ErrorRequiredAniFileNotFound")> ErrorRequiredAniFileNotFound
		<Description("ErrorRequiredVtxFileNotFound")> ErrorRequiredVtxFileNotFound
		<Description("ErrorRequiredVvdFileNotFound")> ErrorRequiredVvdFileNotFound

		<Description("ErrorInvalidMdlFileId")> ErrorInvalidMdlFileId
		<Description("ErrorInvalidInternalMdlFileSize")> ErrorInvalidInternalMdlFileSize
	End Enum

	<FlagsAttribute>
	Public Enum FilesFoundFlags
		<Description("AllFilesFound")> AllFilesFound = 0
		<Description("ErrorRequiredSequenceGroupMdlFileNotFound")> ErrorRequiredSequenceGroupMdlFileNotFound = 1
		<Description("ErrorRequiredTextureMdlFileNotFound")> ErrorRequiredTextureMdlFileNotFound = 2

		<Description("ErrorRequiredMdlFileNotFound")> ErrorRequiredMdlFileNotFound = 4
		<Description("ErrorRequiredAniFileNotFound")> ErrorRequiredAniFileNotFound = 8
		<Description("ErrorRequiredVtxFileNotFound")> ErrorRequiredVtxFileNotFound = 16
		<Description("ErrorRequiredVvdFileNotFound")> ErrorRequiredVvdFileNotFound = 32

		<Description("Error")> [Error] = 64
	End Enum

	Public Enum ActionType
		<Description("Unknown")> Unknown
		<Description("SetUpGames")> SetUpGames
		<Description("Download")> Download
		<Description("Unpack")> Unpack
		<Description("Preview")> Preview
		<Description("Decompile")> Decompile
		<Description("Edit")> Edit
		<Description("Compile")> Compile
		<Description("View")> View
		<Description("Pack")> Pack
		<Description("Publish")> Publish
		'<Description("Options")> Options
	End Enum

	Public Enum ContainerType
		GMA
		VPK
	End Enum

	Public Enum ArchiveAction
		Undefined
		'Extract
		ExtractAndOpen
		ExtractToTemp
		ExtractFolderTree
		Insert
		List
		Pack
		Unpack
	End Enum

	Public Enum ViewerType
		<Description("Preview")> Preview
		<Description("View")> View
	End Enum

	Public Enum DecompiledFileType
		QC
		ReferenceMesh
		LodMesh
		BoneAnimation
		PhysicsMesh
		VertexAnimation
		ProceduralBones
		TextureBmp
		Debug
		DeclareSequenceQci
	End Enum

	Public Enum ProgressOptions
		WarningPhyFileChecksumDoesNotMatchMdlFileChecksum

		WritingFileStarted
		WritingFileFailed
		WritingFileFinished
	End Enum

	Public Enum FindDirection
		Previous
		[Next]
	End Enum

	Public Enum GameEngine
		<Description("GoldSource")> GoldSource
		<Description("Source")> Source
		<Description("Source 2")> Source2
	End Enum

	Public Enum SupportedMdlVersion
		<Description("Do not override")> DoNotOverride
		<Description("06")> MDLv06
		<Description("10")> MDLv10
		<Description("2531")> MDLv2531
		<Description("27")> MDLv27
		<Description("28")> MDLv28
		<Description("29")> MDLv29
		<Description("30")> MDLv30
		<Description("31")> MDLv31
		<Description("32")> MDLv32
		<Description("35")> MDLv35
		<Description("36")> MDLv36
		<Description("37")> MDLv37
		<Description("38")> MDLv38
		<Description("44")> MDLv44
		<Description("45")> MDLv45
		<Description("46")> MDLv46
		<Description("47")> MDLv47
		<Description("48")> MDLv48
		<Description("49")> MDLv49
		<Description("52")> MDLv52
		<Description("53")> MDLv53
		<Description("57")> MDLv57
	End Enum

	Public Enum OrientationType
		<Description("Horizontal")> Horizontal
		<Description("Vertical")> Vertical
	End Enum

End Module
