Public Class Nightfire007FileData
	Inherits BasePackageFileData

	Public Sub New()
		MyBase.New()

	End Sub

	Public Overrides ReadOnly Property IsSourcePackage() As Boolean
		Get
			Return ((Me.id = Nightfire007FileData.NIGHTFIRE007_ID) AndAlso (Me.id2_Text = Nightfire007FileData.NIGHTFIRE007_ID2_TEXT))
		End Get
	End Property

	Public Overrides ReadOnly Property FileExtension() As String
		Get
			Return Nightfire007FileData.TheNightfire007FileExtension
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

	'01 00 00 00 03 00 00 00 04 00 00 00 52 4F 4F 54 
	' 52 4F 4F 54 = ROOT
	Public id As UInt32
	Public version As UInt32
	Public unknown As UInt32
	Public id2_Text As String

	Private Const NIGHTFIRE007_ID As UInt32 = &H1
	Private Const NIGHTFIRE007_ID2_TEXT As String = "ROOT"
	Private Const TheNightfire007FileExtension As String = ".007"

End Class
