Public Class BasePackageDirectoryEntry

	Public Sub New()
		MyBase.New()

		' Write this value to avoid using multi-chunk stuff.
		Me.archiveIndex = &H7FFF
	End Sub

	Public crc As UInt32
	Public archiveIndex As UInt16

	' The text that should be shown in user interface, for example, in Garry's Mod, the meta data as a file (without quotes): "<addon.json>"
	'    This field should start with a "<" to signify that theRealPathFileName has the real text. 
	Public thePathFileName As String
	' The text that should be used for an actual file name, for example (without quotes): "addon.json"
	Public theRealPathFileName As String

End Class
