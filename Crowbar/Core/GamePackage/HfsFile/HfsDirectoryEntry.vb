Public Class HfsDirectoryEntry
	Inherits BasePackageDirectoryEntry

	'    char {4}     - Signature (HF) (0x02014648)
	'    uint16 {2}   - Version needed to extract (seen as 20)
	'    uint16 {2}   - General purpose bit flag
	'    uint16 {2}   - Compression method (always 'Stored')
	'    uint16 {2}   - Last mod file time
	'    uint16 {2}   - Last mod file date
	'    uint32 {4}   - CRC32
	'    uint32 {4}   - Compressed File Size
	'    uint32 {4}   - Decompressed File Size
	'    uint16 {2}   - Filename Length
	'    uint16 {2}   - Extra Field Length
	'    char {X}     - Filename (Obfuscated)
	'    char {X}     - Extra field
	'    byte {X}     - File Data (Obfuscated, Compressed)
	Public signature As UInt32
	Public unused01 As UInt16
	Public unused02 As UInt16
	Public unused03 As UInt16
	Public fileLastModificationTime As UInt16
	Public fileLastModificationDate As UInt16
	'Public crc As UInt32
	Public compressedFileSize As UInt32
	Public decompressedFileSize As UInt32
	Public fileNameSize As UInt16
	Public extraFieldSize As UInt16

	Public fileName As String
	Public offset As Long
	'Public fileDataBlockPosition As Long

End Class
