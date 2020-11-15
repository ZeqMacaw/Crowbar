Imports System.ComponentModel

Public Class UnpackerInputInfo

	Public theArchiveAction As ArchiveAction
	'Public theArchiveEntryIndexes As List(Of Integer)
	'Public theEntries As List(Of VpkDirectoryEntry)
	Public theArchivePathFileNameToEntryIndexesMap As SortedList(Of String, List(Of Integer))
	Public theGamePath As String
	Public theOutputPathIsExtendedWithPackageName As Boolean
	Public theSelectedRelativeOutputPath As String

End Class
