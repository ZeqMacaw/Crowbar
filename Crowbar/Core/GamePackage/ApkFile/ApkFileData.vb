Public Class ApkFileData
	Inherits BasePackageFileData

	Public Sub New()
		MyBase.New()

	End Sub

	Public Overrides ReadOnly Property IsSourcePackage() As Boolean
		Get
			Return (Me.id = ApkFileData.APK_ID)
		End Get
	End Property

	Public Overrides ReadOnly Property FileExtension() As String
		Get
			Return ApkFileData.TheApkFileExtension
		End Get
	End Property

	Public Overrides ReadOnly Property DirectoryFileNameSuffix() As String
		Get
			Return ""
		End Get
	End Property

	Public Overrides ReadOnly Property DirectoryFileNameSuffixWithExtension() As String
		Get
			Return ""
		End Get
	End Property

	'FROM: Fairy Tale Busters "models00.apk"
	'57 23 00 00   ID of APK
	'10 00 00 00   offset of files
	'DE 05 00 00   
	'85 EB D9 0A   offset of directory

	Public id As UInt32
	Public offsetOfFiles As Long
	Public fileCount As UInt32
	Public offsetOfDirectory As Long

	Private Const APK_ID As UInt32 = &H2357
	Private Const TheApkFileExtension As String = ".apk"

End Class
