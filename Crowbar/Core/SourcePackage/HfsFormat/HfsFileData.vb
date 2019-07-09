Public Class HfsFileData
	Inherits BasePackageFileData

	Public Sub New()
		MyBase.New()

	End Sub

	Public Overrides ReadOnly Property IsSourcePackage() As Boolean
		Get
			Return (Me.id = HfsFileData.HFS_ID)
		End Get
	End Property

	Public Overrides ReadOnly Property FileExtension() As String
		Get
			Return HfsFileData.TheHfsFileExtension
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

	Public id As UInt32

	Public theMainDirectoryHeaderOffset As Long

	Private Const HFS_ID As UInt32 = &H2014648
	Private Const TheHfsFileExtension As String = ".hfs"

End Class
