Imports System.ComponentModel

Public Class UnpackerInputInfo

	Public thePackageAction As PackageAction
	'Public thePackagePathFileNameToEntryIndexesMap As SortedList(Of String, List(Of Integer))
	Public thePackagePathFileNameToEntriesMap As SortedList(Of String, List(Of SourcePackageDirectoryEntry))
	'Public theEntries As List(Of BasePackageDirectoryEntry)
	Public theGamePath As String
	Public theOutputPathIsExtendedWithPackageName As Boolean
	Public theSelectedRelativeOutputPath As String

End Class
