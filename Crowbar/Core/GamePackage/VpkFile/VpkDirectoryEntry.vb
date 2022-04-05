Public Class VpkDirectoryEntry
	Inherits SourcePackageDirectoryEntry

	Public Sub New()
		MyBase.New()

		' Write this value to indicate a single-file VPK; i.e. not a multi-file VPK.
		Me.multiFilePackageFileIndex = &H7FFF
		'Me.isVtmbVpk = False
	End Sub

	'FROM: Nem's Tools\hllib245\HLLib\VPKFile.h
	'		struct VPKDirectoryEntry
	'		{
	'			hlUInt uiCRC;
	'			hlUShort uiPreloadBytes;
	'			hlUShort uiArchiveIndex;
	'			hlUInt uiEntryOffset;
	'			hlUInt uiEntryLength;
	'			hlUShort uiDummy0;			// Always 0xffff.
	'		};

	'FROM: VPKReader-master\VPKReader\VPKFile.cpp
	'struct VPKDirectoryEntry
	'{
	'    unsigned int CRC; // A 32bit CRC of the file's data.
	'    unsigned short PreloadBytes; // The number of bytes contained in the index file.
	'
	'    // A zero based index of the archive this file's data is contained in.
	'    // If 0x7fff, the data follows the directory.
	'    unsigned short ArchiveIndex;
	'
	'    // If ArchiveIndex is 0x7fff, the offset of the file data relative to the end of the directory (see the header for more details).
	'    // Otherwise, the offset of the data from the start of the specified archive.
	'    unsigned int EntryOffset;
	'
	'    // If zero, the entire file is stored in the preload data.
	'    // Otherwise, the number of bytes stored starting at EntryOffset.
	'    unsigned int EntryLength;
	'
	'    unsigned short Terminator;
	'};

	'Public crc As UInt32
	Public preloadByteCount As UInt16
	Public preloadBytesOffset As Long
	Public dataLength As UInt64
	Public multiFilePackageFileIndex As UInt16
	'Public endBytes As UInt16

	'TODO: Titanfall VPK
	'Public unknown01 As UInt16
	'Public unknown02 As UInt32
	'Public unknown03 As UInt32
	'Public unknown04 As UInt32
	'Public unknown05 As UInt32
	'Public endOfEntryBytes As UInt16

	'Public isVtmbVpk As Boolean
	Public PackageDirPathFileName As String
	Public titanfallEntryBlocks As List(Of VpkTitanfallEntryBlock)

End Class
