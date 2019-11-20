Public Class BasePackageDirectoryEntry

	Public Sub New()
		MyBase.New()

		' Write this value to avoid using multi-chunk stuff.
		Me.archiveIndex = &H7FFF
	End Sub

	Public crc As UInt32
	Public archiveIndex As UInt16

	Public thePathFileName As String
	Public theRealPathFileName As String

End Class
