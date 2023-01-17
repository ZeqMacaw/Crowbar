Imports System.ComponentModel

Public Class UnpackerInputInfo

	Public thePackageAction As PackageAction
	Public thePackagePathFileNameToEntriesMap As SortedList(Of String, List(Of SourcePackageDirectoryEntry))
	Public theGamePath As String
	Public theSelectedRelativeOutputPath As String

End Class
