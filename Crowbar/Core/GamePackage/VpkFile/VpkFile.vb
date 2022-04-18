Imports System.ComponentModel
Imports System.IO
Imports System.Text

Public Class VpkFile
	Inherits SourcePackageFile

#Region "Creation and Destruction"

	'Public Sub New(ByVal packageDirectoryFileReader As BinaryReader, ByVal vpkFileReader As BinaryReader, ByVal vpkFileData As VpkFileData)
	Public Sub New(ByVal vpkFileReader As BufferedBinaryReader, ByVal vpkDataFileReader As BufferedBinaryReader, ByVal vpkFileData As VpkFileData)
		Me.theVpkFileReader = vpkFileReader
		Me.theVpkDataFileReader = vpkDataFileReader
		Me.theVpkFileData = vpkFileData
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property FileData() As VpkFileData
		Get
			Return Me.theVpkFileData
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overrides Sub ReadHeader()
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Me.theVpkFileReader.Seek(0, SeekOrigin.Begin)
		fileOffsetStart = Me.theVpkFileReader.Position

		Me.theVpkFileData.id = Me.theVpkFileReader.ReadUInt32()

		If Me.theVpkFileData.PackageHasID Then
			Me.ReadValveVpkHeader()
		ElseIf Me.IsVtmbVpk() Then
			Me.ReadVtmbVpkHeader()
		Else
			Me.ReadValveVpkHeader()
		End If

		fileOffsetEnd = Me.theVpkFileReader.Position - 1
		Me.theVpkFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VPK File Header")
	End Sub

	Private Sub ReadValveVpkHeader()
		Me.theVpkFileData.version = Me.theVpkFileReader.ReadUInt32()
		Me.theVpkFileData.directoryLength = Me.theVpkFileReader.ReadUInt32()

		If Me.theVpkFileData.version = 2 Then
			Me.theVpkFileData.unused01 = Me.theVpkFileReader.ReadUInt32()
			Me.theVpkFileData.packageHashLength = Me.theVpkFileReader.ReadUInt32()
			Me.theVpkFileData.extraLength = Me.theVpkFileReader.ReadUInt32()
			Me.theVpkFileData.unused02 = Me.theVpkFileReader.ReadUInt32()
			' The version = 196610 is used by Titanfall and Titanfall 2.
		ElseIf Me.theVpkFileData.version = 196610 Then
			Me.theVpkFileData.unused01 = Me.theVpkFileReader.ReadUInt32()
		End If

		Me.theVpkFileData.theDirectoryOffset = Me.theVpkFileReader.Position
	End Sub

	Private Function IsVtmbVpk() As Boolean
		Dim vpkIsVtmb As Boolean = False
		Dim startPosition As Long = Me.theVpkFileReader.Position
		Dim fileLength As Long = Me.theVpkFileReader.Length

		Me.theVpkFileReader.Seek(-1, SeekOrigin.End)
		Dim vtmbVpkType As Integer = Me.theVpkFileReader.ReadByte()
		'NOTE: Skip reading vtmbVpkType = 1 because it is just a directory of entries with no data.
		If vtmbVpkType = 0 Then
			Dim directoryEndOffset As Long = Me.theVpkFileReader.Seek(-9, SeekOrigin.End)
			Me.theVpkFileData.theEntryCount = Me.theVpkFileReader.ReadUInt32()
			Me.theVpkFileData.theDirectoryOffset = Me.theVpkFileReader.ReadUInt32()

			If Me.theVpkFileData.theDirectoryOffset >= fileLength Then
				Me.theVpkFileData.theEntryCount = 0
				vpkIsVtmb = False
			Else
				'TODO: It is VTMB VPK package if offsets and lengths match in the directory at end of file.
				'      Would need to check that offsets and lengths are within file length boundaries.
				vpkIsVtmb = True
				Dim entryPathFileNameLength As UInteger
				Try
					Me.theVpkFileReader.Seek(Me.theVpkFileData.theDirectoryOffset, SeekOrigin.Begin)
					For i As UInteger = 0 To CUInt(Me.theVpkFileData.theEntryCount - 1)
						entryPathFileNameLength = Me.theVpkFileReader.ReadUInt32()
						' Test against Windows MAXPATH.
						If entryPathFileNameLength <= 260 Then
							Me.theVpkFileReader.Seek(entryPathFileNameLength + 8, SeekOrigin.Current)
						Else
							Me.theVpkFileData.theEntryCount = 0
							vpkIsVtmb = False
							Exit For
						End If
					Next
					'NOTE: Do not accept 'vtmbVpkType = 1' as a valid VtmbVpk because it is just a directory of entries with no data.
					If Me.theVpkFileReader.Position <> directoryEndOffset Then
						Me.theVpkFileData.theEntryCount = 0
						vpkIsVtmb = False
					End If
				Catch ex As Exception
					Me.theVpkFileData.theEntryCount = 0
					vpkIsVtmb = False
				End Try
			End If
		End If

		Me.theVpkFileReader.Seek(startPosition, SeekOrigin.Begin)
		Return vpkIsVtmb
	End Function

	'Example output:
	'addonimage.jpg crc=0x50ea4a15 metadatasz=0 fnumber=32767 ofs=0x0 sz=10749
	'addonimage.vtf crc=0xc75861f5 metadatasz=0 fnumber=32767 ofs=0x29fd sz=8400
	'addoninfo.txt crc=0xb3d2b571 metadatasz=0 fnumber=32767 ofs=0x4acd sz=1677
	'materials/models/weapons/melee/crowbar.vmt crc=0x4aaf5f0 metadatasz=0 fnumber=32767 ofs=0x515a sz=566
	'materials/models/weapons/melee/crowbar.vtf crc=0xded2e058 metadatasz=0 fnumber=32767 ofs=0x5390 sz=174920
	'materials/models/weapons/melee/crowbar_normal.vtf crc=0x7ac0e054 metadatasz=0 fnumber=32767 ofs=0x2fed8 sz=1398196
	Private Sub ReadVtmbVpkHeader()
		Me.theVpkFileReader.Seek(-1, SeekOrigin.End)
		Dim vtmbVpkType As Integer = Me.theVpkFileReader.ReadByte()
		'NOTE: Skip reading vtmbVpkType = 1 because it is just a directory of entries with no data.
		If vtmbVpkType = 0 OrElse vtmbVpkType = 1 Then
			Dim directoryEndOffset As Long = Me.theVpkFileReader.Seek(-9, SeekOrigin.End)
			Me.theVpkFileData.theEntryCount = Me.theVpkFileReader.ReadUInt32()
			Me.theVpkFileData.theDirectoryOffset = Me.theVpkFileReader.ReadUInt32()
			'TODO: It is VTMB VPK package if offsets and lengths match in the directory at end of file.
			'      Would need to check that offsets and lengths are within file length boundaries.
			Dim entryPathFileNameLength As UInteger
			Try
				Me.theVpkFileReader.Seek(Me.theVpkFileData.theDirectoryOffset, SeekOrigin.Begin)
				For i As UInteger = 0 To CUInt(Me.theVpkFileData.theEntryCount - 1)
					entryPathFileNameLength = Me.theVpkFileReader.ReadUInt32()
					'entry.thePathFileName = Me.theInputFileReader.ReadChars(CInt(entryPathFileNameLength))
					'entry.dataOffset = Me.theInputFileReader.ReadUInt32()
					'entry.dataLength = Me.theInputFileReader.ReadUInt32()
					Me.theVpkFileReader.Seek(entryPathFileNameLength + 8, SeekOrigin.Current)
				Next
				'NOTE: Do not accept 'vtmbVpkType = 1' as a valid VtmbVpk because it is just a directory of entries with no data.
				If Me.theVpkFileReader.Position <> directoryEndOffset OrElse vtmbVpkType = 1 Then
					Me.theVpkFileData.theEntryCount = 0
					'theVpkIsVtmb = False
				End If
			Catch ex As Exception
				Me.theVpkFileData.theEntryCount = 0
				'theVpkIsVtmb = False
			End Try
		End If
	End Sub

	Public Overrides Sub ReadEntries()
		If Not Me.theVpkFileData.IsSourcePackage Then
			Exit Sub
		End If

		If Not Me.theVpkFileData.PackageHasID Then
			ReadVtmbEntries()
			Exit Sub
		End If

		Dim vpkFileHasMoreToRead As Boolean = True
		Dim entryExtension As String = ""
		Dim entryPath As String = ""
		Dim entryFileName As String = ""
		Dim entry As VpkDirectoryEntry
		While vpkFileHasMoreToRead
			'Try
			entryExtension = FileManager.ReadNullTerminatedString(Me.theVpkFileReader)
			If String.IsNullOrEmpty(entryExtension) Then
				Exit While
			End If
			'Catch ex As Exception
			'	'vpkFileHasMoreToRead = False
			'	Exit While
			'End Try

			While vpkFileHasMoreToRead
				'Try
				entryPath = FileManager.ReadNullTerminatedString(Me.theVpkFileReader)
				If String.IsNullOrEmpty(entryPath) Then
					Exit While
				End If
				'Catch ex As Exception
				'	vpkFileHasMoreToRead = False
				'	Exit While
				'End Try

				While vpkFileHasMoreToRead
					'Try
					entryFileName = FileManager.ReadNullTerminatedString(Me.theVpkFileReader)
					If String.IsNullOrEmpty(entryFileName) Then
						Exit While
					End If
					'Catch ex As Exception
					'	vpkFileHasMoreToRead = False
					'	Exit While
					'End Try

					entry = New VpkDirectoryEntry()
					'entry.Crc = Me.theInputFileReader.ReadUInt32()
					Me.theVpkFileReader.ReadUInt32()
					entry.preloadByteCount = Me.theVpkFileReader.ReadUInt16()
					entry.multiFilePackageFileIndex = Me.theVpkFileReader.ReadUInt16()
					' The version = 196610 is used by Titanfall and Titanfall 2.
					If Me.theVpkFileData.version = 196610 Then
						Dim endBytes As UInt16
						entry.dataLength = 0
						entry.titanfallEntryBlocks = New List(Of VpkTitanfallEntryBlock)()
						While True
							Try
								Dim entryBlock As New VpkTitanfallEntryBlock()
								entryBlock.entryFlags = Me.theVpkFileReader.ReadUInt32()
								entryBlock.textureFlags = Me.theVpkFileReader.ReadUInt16()
								entryBlock.offset = Me.theVpkFileReader.ReadInt64()
								entryBlock.compressedSize = Me.theVpkFileReader.ReadUInt64()
								entryBlock.uncompressedSize = Me.theVpkFileReader.ReadUInt64()
								entry.titanfallEntryBlocks.Add(entryBlock)

								entry.dataLength += entryBlock.uncompressedSize

								endBytes = Me.theVpkFileReader.ReadUInt16()
								If endBytes = &HFFFF Then
									Exit While
								End If
							Catch ex As Exception
								vpkFileHasMoreToRead = False
								Exit While
							End Try
						End While
					Else
						entry.DataOffset = Me.theVpkFileReader.ReadUInt32()
						If entry.multiFilePackageFileIndex = &H7FFF Then
							entry.DataOffset += Me.theVpkFileData.theDirectoryOffset + Me.theVpkFileData.directoryLength
						End If
						entry.dataLength = Me.theVpkFileReader.ReadUInt32()
						'entry.endBytes = Me.theVpkFileReader.ReadUInt16()
						Me.theVpkFileReader.ReadUInt16()
						'If entry.endBytes <> &HFFFF Then
						'	Dim debug As Integer = 4242
						'End If
					End If
					If entry.preloadByteCount > 0 Then
						entry.preloadBytesOffset = Me.theVpkFileReader.Position
						Me.theVpkFileReader.Position += entry.preloadByteCount

						If entry.dataLength = 0 Then
							'NOTE: Reaches here when a packed file is fully stored in the "_dir" vpk file, so override whatever packageIndex was assigned.
							entry.multiFilePackageFileIndex = &H7FFF
						End If
					End If

					If vpkFileHasMoreToRead Then
						entry.DataSize = entry.preloadByteCount + entry.dataLength
						If entryPath = " " Then
							entry.DisplayPathFileName = entryFileName + "." + entryExtension
						Else
							entry.DisplayPathFileName = entryPath + "/" + entryFileName + "." + entryExtension
						End If
						entry.PathFileName = entry.DisplayPathFileName
						Me.theVpkFileData.theEntries.Add(entry)
					End If
				End While
			End While
		End While
	End Sub

	Private Sub ReadVtmbEntries()
		Dim entryPathFileNameLength As UInteger
		Dim entryFileName As String = ""
		Dim entry As VpkDirectoryEntry
		'Dim entryDataOutputText As New StringBuilder

		Me.theVpkFileReader.Seek(Me.theVpkFileData.theDirectoryOffset, SeekOrigin.Begin)
		For i As UInteger = 0 To CUInt(Me.theVpkFileData.theEntryCount - 1)
			entry = New VpkDirectoryEntry()

			entryPathFileNameLength = Me.theVpkFileReader.ReadUInt32()
			entry.DisplayPathFileName = Me.theVpkFileReader.ReadChars(CInt(entryPathFileNameLength))
			entry.PathFileName = entry.DisplayPathFileName
			entry.DataOffset = Me.theVpkFileReader.ReadUInt32()
			entry.dataLength = Me.theVpkFileReader.ReadUInt32()

			'entry.Crc = 0
			entry.preloadByteCount = 0
			'entry.archiveIndex = &H7FFF
			'entry.endBytes = 0
			'entry.isVtmbVpk = True
			entry.DataSize = entry.preloadByteCount + entry.dataLength

			Me.theVpkFileData.theEntries.Add(entry)

			'entryDataOutputText.Append(entry.thePathFileName)
			'entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"))
			'entryDataOutputText.Append(" metadatasz=" + entry.preloadByteCount.ToString("G0"))
			'entryDataOutputText.Append(" fnumber=" + entry.archiveIndex.ToString("G0"))
			'entryDataOutputText.Append(" ofs=0x" + entry.dataOffset.ToString("X8"))
			'entryDataOutputText.Append(" sz=" + (entry.preloadByteCount + entry.dataLength).ToString("G0"))

			'Me.theVpkFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString())
			'NotifyPackEntryRead(entry, entryDataOutputText.ToString())
			'NotifyPackEntryRead(entry)

			'entryDataOutputText.Clear()

			'If bw IsNot Nothing AndAlso bw.CancellationPending Then
			'	Exit For
			'End If
		Next
	End Sub

	Public Overrides Sub UnpackEntryDataToFile(ByVal iEntry As SourcePackageDirectoryEntry, ByVal outputPathFileName As String)
		Dim entry As VpkDirectoryEntry = CType(iEntry, VpkDirectoryEntry)

		Dim outputFileStream As FileStream = Nothing
		Try
			outputFileStream = New FileStream(outputPathFileName, FileMode.Create)
			If outputFileStream IsNot Nothing Then
				Try
					'Me.theOutputFileWriter = New BinaryWriter(outputFileStream, System.Text.Encoding.ASCII)
					Me.theOutputFileWriter = New BinaryWriter(outputFileStream, System.Text.Encoding.Default)

					' The titanfallEntryBlocks are only used by Titanfall and Titanfall 2.
					If entry.titanfallEntryBlocks IsNot Nothing AndAlso entry.titanfallEntryBlocks.Count > 0 Then
						If entry.preloadByteCount > 0 Then
							Me.theVpkFileReader.Seek(entry.preloadBytesOffset, SeekOrigin.Begin)
							Dim preloadBytes() As Byte
							preloadBytes = Me.theVpkFileReader.ReadBytes(CInt(entry.preloadByteCount))
							Me.theOutputFileWriter.Write(preloadBytes)
						End If
						For Each entryBlock As VpkTitanfallEntryBlock In entry.titanfallEntryBlocks
							Me.theVpkDataFileReader.Seek(entryBlock.offset, SeekOrigin.Begin)
							If entryBlock.compressedSize = entryBlock.uncompressedSize Then
								Dim bytes() As Byte = Me.theVpkDataFileReader.ReadBytes(CInt(entryBlock.compressedSize))
								Me.theOutputFileWriter.Write(bytes)

								'If entry.titanfallEntryBlocks.Count > 1 Then
								'	Dim debug As Integer = 4242
								'End If
							Else
								Dim compressedBytes() As Byte = Me.theVpkDataFileReader.ReadBytes(CInt(entryBlock.compressedSize))
								'TODO: Might need to redo size of byte array to handle uncompressedSize > Int32.
								Dim uncompressedlength As Int32 = CInt(entryBlock.uncompressedSize)
								Dim uncompressedBytes(uncompressedlength - 1) As Byte

								Dim status As Integer = LzhamApi.Decompress(compressedBytes, uncompressedBytes)

								Me.theOutputFileWriter.Write(uncompressedBytes)
							End If
						Next
					Else
						If entry.preloadByteCount > 0 Then
							Me.theVpkFileReader.Seek(entry.preloadBytesOffset, SeekOrigin.Begin)
							Dim preloadBytes() As Byte
							preloadBytes = Me.theVpkFileReader.ReadBytes(CInt(entry.preloadByteCount))
							Me.theOutputFileWriter.Write(preloadBytes)
						End If
						Me.theVpkDataFileReader.Seek(entry.DataOffset, SeekOrigin.Begin)
						Dim bytes() As Byte = Me.theVpkDataFileReader.ReadBytes(CInt(entry.dataLength))
						Me.theOutputFileWriter.Write(bytes)
					End If
				Catch ex As Exception
					Dim debug As Integer = 4242
				Finally
					If Me.theOutputFileWriter IsNot Nothing Then
						Me.theOutputFileWriter.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If outputFileStream IsNot Nothing Then
				outputFileStream.Close()
			End If
		End Try
	End Sub

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	Private theVpkFileReader As BufferedBinaryReader
	Private theVpkDataFileReader As BufferedBinaryReader
	Private theOutputFileWriter As BinaryWriter
	Private theVpkFileData As VpkFileData

#End Region

End Class
