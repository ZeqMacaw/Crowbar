Public MustInherit Class SourcePackageFileData
	Inherits SourceFileData

	Public Sub New()
		MyBase.New()

		Me.theEntries = New List(Of SourcePackageDirectoryEntry)()
		'Me.theEntryDataOutputTexts = New List(Of String)()
	End Sub

	Public MustOverride ReadOnly Property IsSourcePackage() As Boolean
	Public MustOverride ReadOnly Property FileExtension() As String
	Public MustOverride ReadOnly Property DirectoryFileNameSuffix() As String
	Public MustOverride ReadOnly Property DirectoryFileNameSuffixWithExtension() As String

	Public theEntries As List(Of SourcePackageDirectoryEntry)
	'Public theEntryDataOutputTexts As List(Of String)

End Class
