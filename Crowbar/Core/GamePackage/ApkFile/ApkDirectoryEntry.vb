Public Class ApkDirectoryEntry
	Inherits BasePackageDirectoryEntry

	'FROM: Fairy Tale Busters "models00.apk"
	'1B 00 00 00   length of file name without the ending null character
	'6D 6F 64 65 6C 73 5C 42 6C 61 6E 6B 5F 4D 6F 64 65 6C 2E 64 78 39 30 2E 76 74 78   models\Blank_Model.dx90.vtx
	'00 
	'14 00 00 00   offset of file
	'AE 00 00 00   length of file
	'B5 EB D9 0A   offset of next directory entry
	'00 00 00 00   

	' This size does not include the null character at the end.
	Public pathFileNameSize As UInt32
	'Public pathFileName(variable_size) As Char
	'      Use this field in base class: Public thePathFileName As String
	'Public nullCharacter As Byte
	Public offsetOfFile As UInt32
	Public fileSize As UInt32
	Public offsetOfNextDirectoryEntry As Int64
	Public unknown As UInt32

End Class
