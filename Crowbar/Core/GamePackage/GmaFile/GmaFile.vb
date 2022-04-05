Imports System.ComponentModel
Imports System.IO
Imports System.Text

Public Class GmaFile
	Inherits SourcePackageFile

#Region "Creation and Destruction"

	Public Sub New(ByVal gmaFileReader As BufferedBinaryReader, ByVal gmaFileData As GmaFileData)
		Me.theGmaFileReader = gmaFileReader
		Me.theGmaFileData = gmaFileData
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property FileData() As GmaFileData
		Get
			Return Me.theGmaFileData
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

		fileOffsetStart = Me.theGmaFileReader.Position

		Me.theGmaFileData.id = Me.theGmaFileReader.ReadChars(4)
		Me.theGmaFileData.version = Me.theGmaFileReader.ReadByte()
		Me.theGmaFileData.steamID = Me.theGmaFileReader.ReadBytes(8)
		Me.theGmaFileData.timestamp = Me.theGmaFileReader.ReadBytes(8)

		'				if ( m_fmtversion > 1 )
		'				{
		'					Bootil::BString strContent = m_buffer.ReadString();
		'
		'					while ( !strContent.empty() )
		'					{
		'						strContent = m_buffer.ReadString();
		'					}
		'				}
		If Me.theGmaFileData.version > 1 Then
			Me.theGmaFileData.requiredContent = FileManager.ReadNullTerminatedString(Me.theGmaFileReader)
		End If

		Me.theGmaFileData.addonName = FileManager.ReadNullTerminatedString(Me.theGmaFileReader)
		Me.theGmaFileData.addonDescription = FileManager.ReadNullTerminatedString(Me.theGmaFileReader)
		Me.theGmaFileData.addonAuthor = FileManager.ReadNullTerminatedString(Me.theGmaFileReader)
		Me.theGmaFileData.addonVersion = Me.theGmaFileReader.ReadUInt32()

		'Me.theGmaFileData.theDirectoryOffset = Me.theInputFileReader.BaseStream.Position

		fileOffsetEnd = Me.theGmaFileReader.Position - 1
		Me.theGmaFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "GMA File Header")
	End Sub

	'WRITE: 
	'		// File list
	'		unsigned int iFileNum = 0;
	'		BOOTIL_FOREACH( f, files, String::List )
	'		{
	'			unsigned long	iCRC = Bootil::File::CRC( strFolder + *f );
	'			long long		iSize = Bootil::File::Size( strFolder + *f );
	'			iFileNum++;
	'			buffer.WriteType( ( unsigned int ) iFileNum );					// File number (4)
	'			buffer.WriteString( String::GetLower( *f ) );					// File name (all lower case!) (n)
	'			buffer.WriteType( ( long long ) iSize );							// File size (8)
	'			buffer.WriteType( ( unsigned long ) iCRC );						// File CRC (4)
	'			Output::Msg( "File index: %s [CRC:%u] [Size:%s]\n", f->c_str(), iCRC, String::Format::Memory( iSize ).c_str() );
	'		}
	'		// Zero to signify end of files
	'		iFileNum = 0;
	'		buffer.WriteType( ( unsigned int ) iFileNum );
	'READ: 
	'				int iFileNumber = 1;
	'				int iOffset = 0;
	'				while ( m_buffer.ReadType<unsigned int>() != 0 )
	'				{
	'					Addon::FileEntry entry;
	'					entry.strName		= m_buffer.ReadString();
	'					entry.iSize			= m_buffer.ReadType<long long>();
	'					entry.iCRC			= m_buffer.ReadType<unsigned long>();
	'					entry.iOffset		= iOffset;
	'					entry.iFileNumber	= iFileNumber;
	'					m_index.push_back( entry );
	'					iOffset += entry.iSize;
	'					iFileNumber++;
	'				}
	Public Overrides Sub ReadEntries()
		If Not Me.theGmaFileData.IsSourcePackage Then
			Exit Sub
		End If

		Dim entry As GmaDirectoryEntry
		Dim fileNumber As UInt32 = 1
		Dim offset As Int64 = 0
		Dim fileNumberStored As UInt32

		Try
			'Dim entryDataOutputText As New StringBuilder

			' Make a fake entry for the "addon.json" file.
			entry = New GmaDirectoryEntry()

			entry.fileNumberStored = 0
			entry.DisplayPathFileName = "<addon.json>"
			entry.PathFileName = "addon.json"
			entry.sizeAsInt64 = Me.GetAddonJsonText().Length
			'entry.Crc = 0
			'entry.offset = 0
			entry.DataOffset = 0
			entry.fileNumberUsed = 0
			entry.DataSize = CType(entry.sizeAsInt64, UInt64)

			Me.theGmaFileData.theEntries.Add(entry)

			'entryDataOutputText.Append(entry.thePathFileName)
			'entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"))
			'entryDataOutputText.Append(" metadatasz=0")
			'entryDataOutputText.Append(" fnumber=0")
			'entryDataOutputText.Append(" ofs=0x" + entry.offset.ToString("X8"))
			'entryDataOutputText.Append(" sz=" + entry.size.ToString("G0"))

			'Me.theGmaFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString())
			'NotifyPackEntryRead(entry, entryDataOutputText.ToString())
			'NotifyPackEntryRead(entry)

			'entryDataOutputText.Clear()

			fileNumberStored = Me.theGmaFileReader.ReadUInt32()
			While fileNumberStored <> 0
				entry = New GmaDirectoryEntry()

				entry.DisplayPathFileName = FileManager.ReadNullTerminatedString(Me.theGmaFileReader)
				entry.PathFileName = entry.DisplayPathFileName
				entry.sizeAsInt64 = Me.theGmaFileReader.ReadInt64()
				'entry.Crc = Me.theInputFileReader.ReadUInt32()
				Me.theGmaFileReader.ReadUInt32()

				entry.fileNumberStored = fileNumberStored
				'entry.offset = offset
				entry.DataOffset = offset
				entry.fileNumberUsed = fileNumber
				entry.DataSize = CType(entry.sizeAsInt64, UInt64)

				Me.theGmaFileData.theEntries.Add(entry)

				'entryDataOutputText.Append(entry.thePathFileName)
				'entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"))
				'entryDataOutputText.Append(" metadatasz=0")
				'entryDataOutputText.Append(" fnumber=0")
				'entryDataOutputText.Append(" ofs=0x" + entry.offset.ToString("X8"))
				'entryDataOutputText.Append(" sz=" + entry.size.ToString("G0"))

				'Me.theGmaFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString())
				'NotifyPackEntryRead(entry, entryDataOutputText.ToString())
				'NotifyPackEntryRead(entry)

				'entryDataOutputText.Clear()

				offset += entry.sizeAsInt64
				fileNumber = CUInt(fileNumber + 1)
				fileNumberStored = Me.theGmaFileReader.ReadUInt32()
			End While

			Me.theGmaFileData.theFileDataOffset = Me.theGmaFileReader.Position
			For Each entry In Me.theGmaFileData.theEntries
				entry.DataOffset += Me.theGmaFileData.theFileDataOffset
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Public Overrides Sub UnpackEntryDataToFile(ByVal iEntry As SourcePackageDirectoryEntry, ByVal outputPathFileName As String)
		Dim entry As GmaDirectoryEntry = CType(iEntry, GmaDirectoryEntry)

		Dim outputFileStream As FileStream = Nothing
		Try
			outputFileStream = New FileStream(outputPathFileName, FileMode.Create)
			If outputFileStream IsNot Nothing Then
				Try
					Me.theOutputFileWriter = New BinaryWriter(outputFileStream, System.Text.Encoding.ASCII)

					If entry.DisplayPathFileName = "<addon.json>" Then
						Me.WriteAddonJsonData()
					Else
						'Me.theInputFileReader.BaseStream.Seek(Me.theGmaFileData.theFileDataOffset + entry.offset, SeekOrigin.Begin)
						'Me.theGmaFileReader.Seek(Me.theGmaFileData.theFileDataOffset + entry.DataOffset, SeekOrigin.Begin)
						Me.theGmaFileReader.Seek(entry.DataOffset, SeekOrigin.Begin)
						Dim bytes() As Byte = Me.theGmaFileReader.ReadBytes(CInt(entry.DataSize))
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

	Private Function GetAddonJsonText() As String
		If Me.theGmaFileData.id Is Nothing Then
			Me.theGmaFileData.id = Me.theGmaFileReader.ReadChars(4)
			Me.theGmaFileData.version = Me.theGmaFileReader.ReadByte()
			Me.theGmaFileData.steamID = Me.theGmaFileReader.ReadBytes(8)
			Me.theGmaFileData.timestamp = Me.theGmaFileReader.ReadBytes(8)
			If Me.theGmaFileData.version > 1 Then
				Me.theGmaFileData.requiredContent = FileManager.ReadNullTerminatedString(Me.theGmaFileReader)
			End If

			Me.theGmaFileData.addonName = FileManager.ReadNullTerminatedString(Me.theGmaFileReader)
			Me.theGmaFileData.addonDescription = FileManager.ReadNullTerminatedString(Me.theGmaFileReader)
		End If

		Dim addonJsonText As String
		addonJsonText = Me.theGmaFileData.addonDescription.Replace("""description"":", """title"":")
		addonJsonText = addonJsonText.Replace("""Description"",", """" + Me.theGmaFileData.addonName + """,")
		addonJsonText = addonJsonText.Trim()
		addonJsonText = addonJsonText.Replace(vbLf, vbCrLf)
		'addonJsonText = addonJsonText.Trim(Chr(&HA))
		Return addonJsonText
	End Function

	Private Sub WriteAddonJsonData()
		' Need to convert string to byte array to avoid length prefix value when using BinaryWriter.
		Dim text As Byte() = System.Text.Encoding.ASCII.GetBytes(Me.GetAddonJsonText())
		Me.theOutputFileWriter.Write(text)
	End Sub

#End Region

#Region "Data"

	Private theGmaFileReader As BufferedBinaryReader
	Private theOutputFileWriter As BinaryWriter
	Private theGmaFileData As GmaFileData

#End Region

End Class
