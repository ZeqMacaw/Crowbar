Imports System.ComponentModel

Public Class UnpackerOutputInfo

	Public theStatus As AppEnums.StatusMessage
	Public theUnpackedRelativePathFileNames As BindingListEx(Of String)
	'Public unpackerAction As VpkAppAction
	Public entries As List(Of SourcePackageDirectoryEntry)

End Class
