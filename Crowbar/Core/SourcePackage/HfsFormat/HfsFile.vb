Imports System.ComponentModel
Imports System.IO
Imports System.Text

Public Class HfsFile
	Inherits BasePackageFile

#Region "Creation and Destruction"

	Public Sub New(ByVal packageDirectoryFileReader As BinaryReader, ByVal packageFileReader As BinaryReader, ByVal hfsFileData As HfsFileData)
		Me.thePackageDirectoryInputFileReader = packageDirectoryFileReader
		Me.theInputFileReader = packageFileReader
		Me.theHfsFileData = hfsFileData
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property FileData() As HfsFileData
		Get
			Return Me.theHfsFileData
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overrides Sub ReadHeader()
		'Dim fileOffsetStart As Long
		'Dim fileOffsetEnd As Long

		'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		'&H02014648 = "HF" 01 02
		Me.theHfsFileData.id = Me.theInputFileReader.ReadUInt32()

		'    char {4}     - Signature (HF) (0x06054648)
		Dim aByte As Byte
		Dim aDoubleWord As UInt32
		For offset As Long = 1 To Me.theInputFileReader.BaseStream.Length
			Me.theInputFileReader.BaseStream.Seek(-offset, SeekOrigin.End)
			aByte = Me.theInputFileReader.ReadByte()
			If aByte = 6 Then
				Me.theInputFileReader.BaseStream.Seek(Me.theInputFileReader.BaseStream.Position - 4, SeekOrigin.Begin)
				aDoubleWord = Me.theInputFileReader.ReadUInt32()
				'&H06054648 = "HF" 05 06
				If aDoubleWord = &H6054648 Then
					Me.theHfsFileData.theMainDirectoryHeaderOffset = Me.theInputFileReader.BaseStream.Position
					Exit For
				End If
			End If
		Next offset

		'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		'Me.theHfsFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "HFS File Header")
	End Sub

	Public Overrides Sub ReadEntries(ByVal bw As BackgroundWorker)
		If Not Me.theHfsFileData.IsSourcePackage Then
			Exit Sub
		End If

		Try
			'Dim entry As HfsDirectoryEntry
			Dim entryDataOutputText As New StringBuilder

			'    uint16 {2}   - Number of disk
			'    uint16 {2}   - Disk where CD starts
			'    uint16 {2}   - Number of CD records
			'NOTE: Skip the first 6 bytes.
			Me.theInputFileReader.BaseStream.Seek(Me.theHfsFileData.theMainDirectoryHeaderOffset + 6, SeekOrigin.Begin)

			'    uint16 {2}   - Count of CD records
			'    uint32 {4}   - Size of central directory
			'    uint32 {4}   - offset to start of CD
			'NOTE: Ignore these other fields.
			'    uint16 {2}   - Zip comment length
			'    char {X}     - comment
			Dim mainDirectoryEntryCount As UInt16
			Dim mainDirectorySize As UInt32
			Dim mainDirectoryOffset As UInt32
			mainDirectoryEntryCount = Me.theInputFileReader.ReadUInt16()
			mainDirectorySize = Me.theInputFileReader.ReadUInt32()
			mainDirectoryOffset = Me.theInputFileReader.ReadUInt32()

			Dim blockPosition As Long
			Dim nextMainDirectoryEntryOffset As Long

			Me.theInputFileReader.BaseStream.Seek(mainDirectoryOffset, SeekOrigin.Begin)
			For mainDirectoryEntryIndex As Integer = 0 To mainDirectoryEntryCount - 1
				Dim mainDirectoryEntry As New HfsMainDirectoryEntry()

				mainDirectoryEntry.signature = Me.theInputFileReader.ReadUInt32()
				mainDirectoryEntry.unused01 = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.unused02 = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.unused03 = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.unused04 = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.fileLastModificationTime = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.fileLastModificationDate = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.crc = Me.theInputFileReader.ReadUInt32()
				mainDirectoryEntry.compressedFileSize = Me.theInputFileReader.ReadUInt32()
				mainDirectoryEntry.decompressedFileSize = Me.theInputFileReader.ReadUInt32()
				mainDirectoryEntry.fileNameSize = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.extraFieldSize = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.commentSize = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.unused05 = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.unused06 = Me.theInputFileReader.ReadUInt16()
				mainDirectoryEntry.unused07 = Me.theInputFileReader.ReadUInt32()
				mainDirectoryEntry.fileDataHeaderOffset = Me.theInputFileReader.ReadUInt32()

				If mainDirectoryEntry.fileNameSize > 0 Then
					blockPosition = Me.theInputFileReader.BaseStream.Position
					Dim fileNameEncoded(mainDirectoryEntry.fileNameSize - 1) As Byte
					fileNameEncoded = Me.theInputFileReader.ReadBytes(mainDirectoryEntry.fileNameSize)
					Me.DecodeBlockWithKey(blockPosition, Me.fileNameKey, fileNameEncoded)
					mainDirectoryEntry.fileName = System.Text.Encoding.ASCII.GetString(fileNameEncoded)
				End If
				If mainDirectoryEntry.extraFieldSize > 0 Then
					Dim extraField(mainDirectoryEntry.extraFieldSize - 1) As Byte
					extraField = Me.theInputFileReader.ReadBytes(mainDirectoryEntry.extraFieldSize)
				End If
				If mainDirectoryEntry.commentSize > 0 Then
					Dim comment(mainDirectoryEntry.commentSize - 1) As Byte
					comment = Me.theInputFileReader.ReadBytes(mainDirectoryEntry.commentSize)
				End If

				nextMainDirectoryEntryOffset = Me.theInputFileReader.BaseStream.Position

				'TODO: Read file header and data.
				Me.theInputFileReader.BaseStream.Seek(mainDirectoryEntry.fileDataHeaderOffset, SeekOrigin.Begin)

				Dim entry As New HfsDirectoryEntry()

				entry.signature = Me.theInputFileReader.ReadUInt32()
				If entry.signature <> &H2014648 Then
					Dim somethingIsWrong As Integer = 4242
				End If
				entry.unused01 = Me.theInputFileReader.ReadUInt16()
				entry.unused02 = Me.theInputFileReader.ReadUInt16()
				entry.unused03 = Me.theInputFileReader.ReadUInt16()
				entry.fileLastModificationTime = Me.theInputFileReader.ReadUInt16()
				entry.fileLastModificationDate = Me.theInputFileReader.ReadUInt16()
				entry.crc = Me.theInputFileReader.ReadUInt32()
				entry.compressedFileSize = Me.theInputFileReader.ReadUInt32()
				entry.decompressedFileSize = Me.theInputFileReader.ReadUInt32()
				entry.fileNameSize = Me.theInputFileReader.ReadUInt16()
				entry.extraFieldSize = Me.theInputFileReader.ReadUInt16()

				If entry.fileNameSize > 0 Then
					blockPosition = Me.theInputFileReader.BaseStream.Position
					Dim fileNameEncoded(entry.fileNameSize - 1) As Byte
					fileNameEncoded = Me.theInputFileReader.ReadBytes(entry.fileNameSize)
					Me.DecodeBlockWithKey(blockPosition, Me.fileNameKey, fileNameEncoded)
					entry.fileName = System.Text.Encoding.ASCII.GetString(fileNameEncoded)
					'TODO: Figure out how the path is stored and insert it here.
					If Path.GetExtension(entry.fileName) = ".comp" Then
						entry.thePathFileName = Path.GetFileNameWithoutExtension(entry.fileName)
					Else
						entry.thePathFileName = entry.fileName
					End If
				End If
				If entry.extraFieldSize > 0 Then
					Dim extraField(entry.extraFieldSize - 1) As Byte
					extraField = Me.theInputFileReader.ReadBytes(entry.extraFieldSize)
				End If
				If entry.compressedFileSize > 0 AndAlso entry.decompressedFileSize > 0 Then
					entry.offset = Me.theInputFileReader.BaseStream.Position
				End If
				'entry.fileDataBlockPosition = Me.theInputFileReader.BaseStream.Position

				Me.theHfsFileData.theEntries.Add(entry)

				entryDataOutputText.Append(entry.thePathFileName)
				entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"))
				entryDataOutputText.Append(" metadatasz=0")
				entryDataOutputText.Append(" fnumber=0")
				entryDataOutputText.Append(" ofs=0x" + entry.offset.ToString("X8"))
				entryDataOutputText.Append(" sz=" + entry.decompressedFileSize.ToString("G0"))

				Me.theHfsFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString())
				NotifyPackEntryRead(entry, entryDataOutputText.ToString())

				entryDataOutputText.Clear()

				Me.theInputFileReader.BaseStream.Seek(nextMainDirectoryEntryOffset, SeekOrigin.Begin)
			Next
			If mainDirectoryOffset + mainDirectorySize <> Me.theInputFileReader.BaseStream.Position Then
				Dim somethingIsWrong As Integer = 4242
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Public Overrides Sub UnpackEntryDataToFile(ByVal iEntry As BasePackageDirectoryEntry, ByVal outputPathFileName As String)
		Dim entry As HfsDirectoryEntry
		entry = CType(iEntry, HfsDirectoryEntry)

		Dim outputFileStream As FileStream = Nothing
		Try
			outputFileStream = New FileStream(outputPathFileName, FileMode.Create)
			If outputFileStream IsNot Nothing Then
				Try
					Me.theOutputFileWriter = New BinaryWriter(outputFileStream, System.Text.Encoding.ASCII)

					Me.theInputFileReader.BaseStream.Seek(entry.offset, SeekOrigin.Begin)
					Dim bytes() As Byte
					bytes = Me.theInputFileReader.ReadBytes(CInt(entry.compressedFileSize))

					If Path.GetExtension(entry.fileName) = ".comp" Then
						'Dim key() As Byte
						'key = DecompressData(entry.offset, bytes)
						'Me.DecodeBlockWithKey(entry.offset, key, bytes)
						'Me.DecodeBlockWithKey(entry.offset + 1, key, bytes)
					Else
						'	Me.DecodeBlockWithKey(entry.offset, fileNameKey, bytes)
					End If

					Me.theOutputFileWriter.Write(bytes)
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

	'public static void XorBlockWithKey(byte[] buffer, byte[] key, int src_position)
	'{
	'    Debug.Assert(key.Length == 4 || key.Length == 4096);
	'
	'    for (int x = 0; x < buffer.Length; x++)
	'    {
	'        buffer[x] ^= key[(src_position + x) & (key.Length - 1)];
	'    }
	'}
	Private Sub DecodeBlockWithKey(ByVal offset As Long, ByVal key() As Byte, ByRef blockOfBytes As Byte())
		For x As Integer = 0 To blockOfBytes.Length - 1
			blockOfBytes(x) = blockOfBytes(x) Xor key(CInt((offset + x) And (key.Length - 1)))
		Next
	End Sub

	'        public static byte[] BruteforceInnerKey(byte[] buffer, int start)
	'        {
	'            byte[] key = new byte[4];
	'
	'            for (int i = 0; i <= 3; i++)
	'            {
	'                int keypos = (int)((start + i) & 3);
	'
	'                for (byte z = 1; z < 0xFF; z++)
	'                {
	'                    byte test = (byte)(buffer[i] ^ z);
	'
	'                    if ((i == 0 && test == 'c') || (i == 1 && test == 'o') || (i == 2 && test == 'm') || (i == 3 && test == 'p'))
	'                    {
	'                        key[keypos] = z;
	'                    }
	'                }
	'            }
	'
	'            return key;
	'        }
	Private Function DecompressData(ByVal offset As Long, ByVal blockOfBytes As Byte()) As Byte()
		Dim key(3) As Byte

		For i As Integer = 0 To 3
			Dim keypos As Integer
			keypos = CInt((offset + i) And 3)

			Try
				For z As Byte = 1 To &HFE
					Dim test As Byte
					test = blockOfBytes(i) Xor z

					' "comp" in hex is: 63 6F 6D 70 
					If ((i = 0 AndAlso test = &H63) OrElse (i = 1 AndAlso test = &H6F) OrElse (i = 2 AndAlso test = &H6D) OrElse (i = 3 AndAlso test = &H70)) Then
						key(keypos) = z
					End If
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		Next

		Return key
	End Function

#End Region

#Region "Data"

	Private thePackageDirectoryInputFileReader As BinaryReader
	Private theInputFileReader As BinaryReader
	Private theOutputFileWriter As BinaryWriter
	Private theHfsFileData As HfsFileData

	Dim fileNameKey() As Byte = {120, 189, 2, 71, 140, 201, 22, 83, 144, 213, 42, 110, 165, 224, 63, 122, _
169, 236, 83, 150, 221, 24, 68, 129, 194, 7, 120, 189, 246, 12, 75, 134, _
253, 3, 68, 137, 202, 23, 80, 157, 214, 66, 135, 200, 12, 87, 146, 221, _
 24, 107, 174, 225, 36, 127, 186, 242, 79, 148, 186, 253, 32, 99, 174, 233, _
 84, 159, 213, 10, 79, 132, 193, 62, 123, 136, 205, 18, 87, 157, 216, 103, _
162, 225, 13, 80, 151, 218, 25, 65, 143, 202, 25, 92, 147, 214, 45, 104, _
167, 226, 33, 103, 168, 237, 86, 147, 220, 25, 74, 143, 192, 5, 127, 186, _
245, 48, 83, 150, 252, 1, 66, 143, 200, 20, 95, 187, 254, 65, 132, 207, _
 10, 85, 144, 211, 41, 110, 163, 224, 61, 122, 183, 236, 81, 150, 219, 25, _
 68, 110, 171, 24, 93, 146, 215, 12, 73, 134, 194, 1, 68, 139, 206, 21, _
 80, 159, 218, 105, 133, 203, 12, 81, 146, 223, 24, 101, 174, 227, 27, 94, _
150, 211, 44, 105, 186, 255, 32, 101, 174, 235, 84, 144, 211, 22, 73, 140, _
199, 2, 125, 184, 203, 14, 86, 155, 216, 101, 162, 140, 199, 51, 118, 185, _
252, 64, 141, 202, 23, 92, 145, 214, 43, 104, 165, 226, 62, 101, 168, 239, _
 82, 145, 220, 27, 70, 141, 192, 4, 94, 149, 208, 15, 74, 185, 252, 3, _
 70, 141, 203, 20, 81, 146, 254, 67, 132, 201, 10, 87, 144, 220, 23, 106, _
173, 224, 35, 126, 185, 209, 50, 119, 185, 252, 39, 98, 173, 232, 91, 158, _
209, 20, 79, 133, 194, 63, 68, 137, 206, 19, 80, 157, 218, 103, 173, 201, _
 12, 83, 123, 184, 5, 66, 143, 196, 25, 95, 146, 209, 44, 107, 166, 253, _
 32, 103, 170, 233, 87, 144, 221, 22, 75, 140, 193, 2, 127, 184, 245, 50, _
119, 184, 253, 6, 67, 140, 201, 26, 118, 187, 253, 64, 131, 206, 9, 84, _
159, 210, 21, 104, 171, 225, 62, 123, 168, 237, 82, 151, 191, 250, 37, 96, _
164, 25, 94, 147, 208, 13, 74, 135, 252, 1, 70, 138, 201, 20, 83, 158, _
213, 65, 132, 203, 14, 85, 147, 220, 25, 77, 128, 199, 26, 89, 148, 211, _
 46, 118, 187, 252, 33, 98, 175, 232, 85, 158, 211, 20, 72, 139, 198, 1, _
124, 135, 202, 13, 80, 147, 251, 5, 64, 131, 239, 50, 117, 184, 251, 70, _
129, 204, 24, 93, 146, 215, 44, 105, 166, 227, 32, 101, 170, 238, 85, 144, _
223, 26, 73, 97, 166, 27, 88, 149, 211, 14, 117, 184, 255, 2, 65, 140, _
203, 22, 93, 186, 255, 64, 133, 206, 11, 84, 145, 210, 23, 104, 172, 231, _
 31, 88, 149, 206, 51, 116, 185, 250, 39, 97, 172, 231, 90, 157, 208, 19, _
 78, 137, 196, 63, 69, 138, 207, 20, 81, 158, 219, 104, 132, 170, 237, 55, _
124, 185, 6, 67, 128, 197, 26, 95, 148, 209, 47, 106, 185, 252, 35, 102, _
173, 232, 87, 146, 209, 23, 72, 141, 198, 3, 91, 150, 237, 48, 119, 186, _
250, 7, 64, 141, 198, 50, 119, 184, 253, 70, 131, 205, 8, 91, 158, 209, _
 20, 111, 170, 229, 32, 99, 169, 205, 48, 115, 190, 249, 36, 111, 162, 229, _
 88, 148, 209, 14, 75, 184, 253, 2, 71, 140, 201, 22, 82, 145, 253, 64, _
135, 202, 9, 84, 126, 187, 8, 76, 131, 198, 29, 88, 151, 210, 49, 116, _
187, 254, 38, 99, 172, 233, 90, 159, 208, 21, 78, 139, 196, 0, 67, 134, _
201, 49, 114, 191, 248, 5, 78, 170, 238, 49, 116, 191, 250, 69, 128, 195, _
  6, 89, 156, 208, 45, 106, 167, 252, 33, 102, 171, 232, 85, 146, 187, 232, _
 45, 98, 167, 28, 89, 150, 211, 48, 117, 187, 254, 5, 64, 143, 202, 25, _
117, 184, 255, 66, 130, 207, 8, 85, 158, 211, 20, 78, 133, 192, 31, 89, _
138, 207, 48, 117, 190, 251, 36, 97, 162, 231, 89, 156, 215, 18, 77, 136, _
251, 62, 65, 132, 207, 21, 82, 159, 247, 35, 102, 169, 236, 55, 114, 189, _
  7, 76, 129, 198, 27, 88, 149, 210, 47, 116, 185, 255, 34, 97, 172, 235, _
 86, 157, 208, 23, 74, 100, 160, 31, 90, 169, 236, 51, 118, 189, 248, 7, _
 66, 130, 238, 51, 116, 185, 250, 71, 128, 205, 6, 91, 157, 208, 19, 110, _
169, 228, 2, 71, 136, 205, 54, 114, 189, 248, 43, 110, 161, 228, 95, 154, _
213, 16, 116, 185, 254, 3, 64, 141, 202, 23, 92, 184, 253, 67, 134, 168, _
245, 50, 127, 180, 9, 78, 131, 192, 28, 91, 150, 205, 48, 119, 186, 249, _
 36, 99, 174, 230, 91, 156, 209, 18, 79, 136, 197, 62, 100, 171, 237, 54, _
115, 188, 249, 10, 102, 171, 236, 49, 114, 190, 249, 68, 143, 194, 5, 88, _
155, 214, 17, 108, 184, 253, 34, 103, 172, 202, 53, 112, 179, 246, 41, 99, _
160, 29, 90, 151, 236, 49, 118, 187, 248, 5, 67, 142, 197, 49, 116, 187, _
254, 69, 128, 207, 10, 90, 112, 183, 10, 73, 132, 195, 30, 69, 136, 207, _
 49, 114, 191, 248, 37, 110, 163, 228, 89, 154, 215, 17, 76, 183, 250, 61, _
 64, 131, 206, 52, 113, 178, 223, 34, 101, 168, 235, 54, 113, 188, 247, 74, _
141, 199, 28, 89, 150, 211, 48, 117, 186, 255, 36, 97, 175, 234, 89, 156, _
182, 235, 40, 101, 162, 31, 100, 168, 239, 50, 113, 188, 251, 6, 77, 169, _
236, 51, 117, 190, 251, 68, 129, 194, 7, 88, 157, 214, 19, 72, 133, 222, _
  3, 68, 137, 202, 55, 112, 189, 246, 42, 109, 160, 227, 94, 153, 212, 47, _
114, 181, 248, 4, 65, 142, 203, 24, 116, 185, 221, 32, 99, 174, 246, 51, _
112, 181, 10, 79, 132, 193, 30, 91, 136, 204, 51, 118, 189, 248, 39, 98, _
161, 228, 91, 158, 214, 19, 76, 102, 157, 32, 103, 170, 233, 52, 115, 189, _
246, 34, 103, 168, 237, 54, 115, 188, 249, 74, 142, 193, 4, 95, 154, 213, _
 16, 115, 182, 249, 1, 67, 142, 201, 52, 127, 178, 245, 40, 107, 166, 225, _
 91, 168, 237, 50, 119, 188, 249, 6, 67, 128, 236, 48, 119, 186, 249, 68, _
131, 171, 248, 61, 114, 183, 13, 72, 135, 194, 1, 68, 139, 206, 53, 112, _
191, 249, 42, 111, 160, 229, 94, 155, 212, 17, 114, 183, 249, 60, 98, 175, _
232, 53, 126, 154, 223, 32, 101, 175, 234, 53, 112, 179, 246, 73, 140, 199, _
  2, 93, 151, 204, 49, 118, 187, 248, 37, 98, 175, 199, 58, 114, 183, 236, _
 41, 102, 163, 32, 101, 170, 239, 52, 112, 191, 250, 9, 101, 168, 239, 50, _
113, 188, 251, 69, 142, 195, 4, 89, 117, 176, 15, 74, 153, 220, 0, 69, _
142, 203, 52, 113, 178, 247, 40, 109, 166, 226, 93, 152, 235, 46, 113, 180, _
255, 58, 69, 128, 196, 19, 86, 153, 220, 39, 98, 173, 232, 59, 126, 182, _
 11, 72, 133, 194, 31, 68, 137, 206, 51, 112, 188, 251, 38, 109, 160, 231, _
 90, 153, 177, 238, 43, 89, 156, 35, 102, 173, 232, 55, 114, 177, 221, 32, _
100, 169, 234, 55, 112, 189, 246, 75, 140, 193, 2, 94, 153, 212, 15, 87, _
152, 221, 6, 67, 140, 201, 59, 126, 177, 244, 47, 106, 165, 224, 99, 166, _
233, 51, 112, 189, 250, 7, 76, 168, 237, 50, 119, 188, 229, 34, 111, 164, _
249, 62, 115, 176, 13, 74, 135, 221, 0, 71, 138, 201, 52, 115, 190, 245, _
 40, 111, 161, 226, 95, 152, 213, 46, 115, 91, 158, 37, 96, 172, 233, 58, _
 86, 155, 220, 33, 98, 175, 232, 53, 127, 178, 245, 72, 139, 198, 1, 92, _
135, 202, 13, 119, 188, 249, 5, 64, 131, 198, 57, 124, 183, 242, 42, 103, _
156, 33, 102, 171, 232, 53, 114, 191, 244, 33, 100, 171, 238, 53, 112, 191, _
250, 73, 140, 166, 250, 57, 116, 179, 14, 85, 152, 223, 2, 65, 140, 200, _
 53, 126, 179, 244, 41, 106, 167, 224, 93, 166, 234, 45, 112, 179, 254, 57, _
 97, 162, 206, 19, 84, 152, 219, 38, 97, 172, 231, 58, 125, 176, 243, 78, _
134, 195, 0, 69, 138, 207, 52, 113, 190, 251, 40, 108, 163, 251, 56, 117, _
178, 239, 20, 89, 158, 35, 97, 172, 235, 54, 125, 153, 220, 35, 102, 173, _
232, 52, 113, 178, 247, 72, 141, 198, 3, 92, 118, 173, 19, 84, 153, 218, _
  7, 64, 141, 198, 59, 124, 177, 243, 46, 105, 164, 223, 98, 165, 232, 43, _
118, 177, 251, 8, 100, 169, 238, 16, 83, 158, 217, 36, 111, 165, 250, 63, _
116, 177, 14, 75, 152, 221, 2, 71, 141, 200, 55, 114, 177, 244, 43, 110, _
165, 224, 95, 153, 141, 208, 23, 90, 153, 36, 99, 174, 229, 17, 87, 152, _
221, 38, 99, 172, 233, 58, 127, 176, 245, 79, 138, 197, 0, 67, 134, 201, _
 12, 82, 159, 216, 4, 79, 130, 197, 56, 123, 182, 241, 44, 87, 154, 34, _
103, 172, 233, 54, 115, 176, 220, 33, 102, 171, 233, 52, 115, 190, 232, 45, _
 98, 167, 252, 57, 118, 178, 17, 84, 155, 222, 5, 64, 143, 202, 57, 124, _
176, 245, 46, 107, 164, 225, 98, 167, 232, 45, 118, 95, 152, 37, 110, 138, _
207, 16, 85, 158, 219, 36, 96, 163, 230, 57, 124, 183, 242, 77, 136, 219, _
 30, 70, 139, 200, 53, 114, 191, 244, 10, 77, 128, 195, 57, 118, 179, 208, _
 21, 90, 159, 36, 97, 174, 235, 57, 85, 152, 223, 34, 97, 172, 235, 54, _
125, 176, 244, 73, 138, 160, 255, 58, 105, 172, 19, 86, 157, 219, 4, 65, _
130, 199, 56, 125, 182, 243, 44, 105, 155, 222, 97, 164, 239, 42, 117, 176, _
243, 31, 71, 137, 204, 23, 82, 157, 216, 43, 110, 161, 228, 63, 117, 178, _
 15, 84, 153, 222, 3, 64, 141, 202, 55, 125, 176, 247, 42, 105, 164, 254, _
 59, 72, 141, 210, 22, 93, 152, 39, 98, 161, 205, 16, 87, 154, 217, 39, _
 96, 173, 230, 59, 124, 177, 242, 79, 136, 197, 31, 66, 104, 173, 22, 83, _
156, 217, 10, 79, 128, 196, 63, 122, 181, 240, 19, 86, 153, 220, 103, 162, _
234, 55, 124, 152, 221, 34, 103, 172, 233, 21, 80, 148, 233, 46, 99, 160, _
253, 58, 119, 172, 17, 86, 154, 217, 4, 67, 142, 197, 56, 127, 178, 241, _
 44, 104, 165, 222, 99, 164, 142, 213, 16, 95, 154, 41, 70, 139, 204, 17, _
 82, 159, 216, 37, 110, 163, 228, 56, 123, 182, 241, 76, 151, 218, 29, 64, _
131, 206, 54, 80, 147, 214, 9, 76, 135, 194, 61, 120, 139, 209, 22, 91, _
152, 37, 98, 175, 228, 16, 85, 154, 222, 37, 96, 175, 234, 57, 124, 179, _
235, 40, 101, 163, 254, 37, 104, 175, 18, 81, 156, 219, 6, 77, 131, 196, _
 57, 122, 183, 240, 45, 86, 155, 220, 97, 163, 238, 41, 116, 82, 190, 3, _
 68, 137, 202, 23, 81, 156, 215, 42, 109, 160, 227, 62, 121, 180, 239, 85, _
154, 223, 4, 65, 142, 203, 56, 125, 178, 247, 8, 69, 130, 255, 4, 73, _
142, 211, 16, 93, 154, 38, 109, 137, 204, 19, 86, 157, 216, 39, 98, 161, _
231, 56, 125, 182, 243, 76, 137, 189, 224, 39, 106, 170, 23, 80, 157, 214, _
 11, 76, 129, 194, 63, 120, 180, 207, 18, 85, 152, 219, 102, 161, 236, 39, _
 83, 153, 222, 35, 67, 142, 201, 20, 95, 146, 213, 40, 100, 161, 254, 59, _
104, 173, 18, 87, 156, 217, 6, 66, 129, 196, 59, 126, 181, 240, 47, 106, _
153, 193, 7, 74, 137, 212, 19, 94, 149, 1, 68, 139, 206, 22, 83, 156, _
217, 42, 111, 160, 229, 62, 123, 180, 240, 83, 150, 217, 28, 71, 111, 168, _
 21, 94, 147, 213, 8, 75, 134, 193, 60, 71, 138, 205, 16, 83, 153, 38, _
 99, 160, 204, 17, 86, 155, 216, 37, 98, 174, 229, 29, 82, 151, 236, 41, _
102, 163, 224, 37, 107, 174, 21, 80, 159, 218, 9, 76, 131, 198, 61, 123, _
180, 241, 18, 87, 152, 221, 102, 163, 139, 214, 30, 122, 191, 0, 69, 142, _
203, 20, 81, 146, 215, 41, 108, 167, 226, 61, 120, 171, 238, 81, 148, 223, _
  5, 66, 143, 196, 57, 93, 144, 211, 14, 73, 132, 192, 5, 74, 143, 212, _
 17, 94, 155, 40, 68, 137, 207, 18, 81, 156, 219, 38, 109, 160, 231, 58, _
121, 183, 239, 42, 121, 188, 227, 38, 109, 168, 23, 82, 146, 215, 8, 77, _
134, 195, 60, 121, 138, 207, 16, 84, 159, 218, 101, 160, 227, 15, 82, 120, _
189, 6, 66, 141, 200, 27, 94, 145, 212, 47, 106, 165, 224, 36, 105, 174, _
 19, 80, 157, 218, 7, 76, 129, 198, 58, 121, 180, 243, 11, 120, 189, 194, _
  7, 76, 137, 215, 18, 81, 189, 0, 71, 138, 201, 20, 83, 158, 214, 43, _
108, 161, 226, 63, 120, 181, 238, 83, 148, 189, 230, 35, 108, 169, 26, 95, _
144, 213, 14, 75, 133, 192, 3, 70, 137, 204, 23, 82, 157, 216, 107, 136, _
205, 18, 87, 156, 217, 38, 64, 131, 198, 25, 83, 144, 237, 42, 103, 188, _
225, 38, 107, 168, 21, 83, 158, 213, 8, 79, 130, 193, 60, 123, 182, 205, _
 19, 84, 153, 197, 0, 79, 138, 217, 53, 120, 191, 1, 66, 143, 200, 21, _
 94, 147, 212, 41, 106, 167, 225, 60, 103, 170, 237, 80, 147, 222, 25, 68, _
 98, 166, 25, 92, 151, 210, 13, 72, 187, 254, 1, 68, 136, 213, 18, 95, _
148, 0, 69, 138, 207, 20, 81, 159, 218, 41, 108, 163, 230, 24, 85, 146, _
239, 52, 120, 191, 226, 33, 108, 171, 22, 93, 144, 215, 10, 74, 135, 192, _
 61, 70, 139, 204, 17, 82, 159, 216, 100, 175, 174, 243, 52, 121, 186, 7, _
 64, 141, 198, 26, 93, 144, 211, 46, 105, 164, 255, 34, 101, 168, 20, 81, _
158, 219, 8, 77, 130, 199, 60, 90, 149, 207, 52, 121, 190, 195, 0, 77, _
138, 215, 28, 120, 188, 3, 70, 141, 200, 23, 82, 145, 212, 43, 110, 166, _
227, 60, 121, 170, 240, 55, 122, 185, 228, 35, 109, 166, 27, 92, 145, 210, _
 15, 72, 133, 254, 3, 69, 136, 203, 22, 81, 156, 215, 67, 134, 201, 12, _
 80, 126, 185, 4, 79, 130, 197, 24, 91, 150, 209, 43, 120, 189, 226, 39, _
108, 169, 22, 83, 144, 213, 11, 78, 133, 192, 63, 122, 137, 204, 54, 123, _
184, 196, 3, 78, 133, 241, 52, 123, 190, 5, 64, 143, 201, 26, 95, 144, _
213, 46, 107, 164, 225, 34, 103, 169, 236, 87, 146, 184, 229, 46, 99, 164, _
 25, 90, 150, 209, 12, 119, 186, 253, 0, 67, 142, 201, 20, 80, 188, 1, _
 70, 139, 200, 21, 82, 159, 212, 41, 66, 135, 220, 25, 86, 147, 240, 53, _
122, 191, 228, 32, 111, 170, 25, 92, 147, 214, 13, 72, 135, 194, 2, 71, _
136, 205, 22, 83, 156, 198, 13, 105, 172, 240, 53, 126, 187, 4, 65, 130, _
199, 24, 93, 150, 210, 45, 104, 187, 254, 33, 100, 175, 234, 85, 144, 212, _
  9, 78, 96, 163, 30, 89, 148, 239, 50, 117, 191, 196, 1, 78, 139, 216, _
 52, 121, 190, 3, 64, 140, 203, 22, 93, 144, 215, 42, 105, 164, 227, 27, _
 73, 140, 243, 54, 125, 184, 231, 34, 97, 164, 27, 93, 150, 211, 12, 73, _
186, 255, 0, 69, 142, 203, 21, 80, 147, 255, 66, 133, 173, 246, 51, 124, _
185, 11, 78, 129, 196, 31, 90, 149, 208, 51, 118, 185, 227, 32, 109, 170, _
 23, 92, 145, 214, 11, 72, 133, 195, 62, 104, 173, 242, 55, 124, 185, 198, _
  3, 64, 173, 240, 55, 122, 185, 4, 67, 142, 197, 24, 95, 145, 210, 47, _
104, 165, 254, 35, 100, 169, 245, 48, 124, 185, 234, 47, 96, 165, 30, 91, _
148, 209, 50, 118, 185, 252, 7, 66, 141, 200, 27, 119, 186, 253, 71, 140, _
201, 22, 83, 115, 182, 9, 76, 135, 194, 26, 87, 140, 241, 54, 123, 184, _
229, 34, 111, 164, 24, 95, 146, 209, 12, 75, 134, 253, 0, 71, 138, 202, _
 48, 127, 186, 201, 37, 104, 175, 242, 49, 124, 184, 5, 78, 131, 196, 25, _
 90, 151, 208, 45, 118, 186, 253, 32, 99, 174, 233, 84, 159, 183, 232, 45, _
103, 162, 29, 88, 171, 238, 49, 116, 191, 250, 5, 79, 132, 240, 53, 122, _
191, 4, 65, 142, 203, 24, 92, 147, 214, 45, 69, 130, 223, 4, 73, 142, _
243, 49, 124, 187, 230, 45, 96, 167, 26, 89, 148, 211, 13, 118, 187, 252, _
  1, 66, 143, 200, 21, 94, 186, 227, 36, 105, 170, 247, 48, 125, 182, 11, _
 76, 129, 195, 30, 89, 148, 207, 50, 117, 184, 251, 38, 97, 171, 24, 93, _
146, 215, 12, 73, 101, 160, 35, 102, 174, 243, 48, 125, 186, 199, 12, 104, _
173, 242, 55, 125, 184, 7, 66, 129, 196, 27, 94, 149, 208, 47, 105, 186, _
255, 7, 74, 137, 244, 51, 126, 181, 232, 44, 97, 162, 31, 88, 149, 238, _
 51, 116, 185, 250, 6, 65, 140, 199, 51, 118, 185, 252, 71, 130, 168, 244, _
 63, 114, 181, 8, 75, 134, 193, 28, 71, 138, 242, 55, 124, 185, 230, 35, _
 96, 165, 26, 95, 148, 208, 15, 74, 185, 252, 3, 107, 168, 245, 50, 127, _
181, 225, 36, 107, 174, 245, 48, 127, 186, 9, 76, 128, 197, 30, 91, 148, _
209, 50, 119, 184, 253, 38, 98, 173, 245, 62, 115, 180, 233, 42, 103, 160, _
 29, 103, 170, 237, 48, 115, 190, 249, 4, 79, 171, 238, 54, 123, 184, 5, _
 66, 143, 196, 25, 94, 112, 179, 9, 70, 131, 192, 5, 74, 143, 244, 49, _
126, 187, 233, 44, 99, 166, 29, 88, 151, 210, 49, 116, 187, 253, 6, 67, _
140, 201, 61, 89, 156, 227, 38, 109, 171, 244, 49, 114, 183, 8, 77, 134, _
195, 28, 89, 139, 206, 49, 116, 191, 250, 37, 96, 163, 230, 89, 147, 179, _
238, 41, 100, 159, 34, 101, 168, 235, 54, 126, 187, 200, 36, 105, 174, 243, _
 48, 125, 186, 7, 77, 128, 199, 26, 89, 148, 211, 46, 88, 157, 194, 6, _
 77, 136, 247, 50, 113, 180, 235, 46, 101, 160, 28, 89, 170, 239, 48, 117, _
190, 251, 4, 65, 130, 239, 50, 117, 184, 230, 35, 108, 169, 250, 63, 112, _
180, 15, 74, 133, 192, 3, 70, 137, 204, 55, 114, 186, 231, 44, 97, 166, _
 27, 88, 149, 210, 15, 116, 93, 226, 39, 108, 169, 246, 51, 112, 156, 225, _
 38, 106, 169, 244, 51, 126, 181, 8, 79, 130, 193, 28, 88, 149, 206, 51, _
116, 185, 250, 0, 79, 138, 249, 63, 112, 181, 238, 43, 100, 161, 34, 103, _
168, 237, 55, 114, 189, 248, 11, 103, 170, 237, 48, 115, 190, 6, 67, 128, _
166, 249, 60, 119, 178, 13, 72, 155, 193, 6, 75, 136, 245, 50, 127, 180, _
233, 46, 99, 161, 28, 91, 150, 237, 48, 119, 186, 249, 4, 110, 170, 249, _
 21, 88, 159, 226, 33, 108, 171, 246, 61, 115, 180, 9, 74, 135, 192, 29, _
 70, 139, 204, 49, 115, 190, 249, 36, 111, 162, 248, 61, 118, 179, 236, 40, _
 91, 158, 33, 100, 175, 234, 53, 112, 179, 223, 37, 106, 175, 244, 49, 126, _
187, 8, 77, 130, 199, 29, 88, 114, 207, 20, 89, 158, 195, 0, 77, 138, _
246, 61, 112, 183, 234, 41, 100, 163, 30, 101, 168, 236, 49, 114, 191, 248, _
  5, 78, 170, 239, 23, 90, 154, 231, 32, 109, 166, 251, 60, 113, 178, 15, _
 72, 132, 223, 2, 69, 136, 203, 54, 113, 188, 247, 42, 98, 167, 28, 89, _
150, 176, 211, 22, 89, 156, 39, 109, 170, 247, 60, 88, 157, 226, 39, 108, _
169, 246, 50, 113, 180, 11, 78, 133, 192, 31, 90, 137, 204, 48, 90, 153, _
196, 3, 78, 133, 248, 63, 114, 177, 239, 40, 101, 158, 35, 100, 169, 234, _
 55, 112, 189, 247, 35, 102, 169, 236, 55, 114, 189, 229, 46, 99, 165, 248, _
 59, 118, 177, 12, 87, 154, 221, 0, 67, 137, 246, 51, 112, 181, 234, 47, _
100, 161, 30, 91, 169, 236, 51, 118, 88, 229, 34, 111, 164, 208, 21, 91, _
158, 229, 32, 111, 170, 249, 60, 115, 182, 13, 75, 132, 193, 2, 71, 136, _
205, 54, 115, 188, 249, 14, 67, 132, 249, 58, 119, 176, 237, 22, 91, 156, _
 32, 99, 174, 233, 52, 127, 155, 222, 33, 100, 175, 245, 50, 127, 180, 9, _
 78, 131, 163, 254, 57, 116, 208, 21, 90, 159, 196, 1, 78, 139, 248, 61, _
114, 182, 237, 40, 103, 162, 33, 100, 171, 238, 53, 112, 188, 249, 10, 73, _
140, 211, 22, 93, 152, 231, 34, 98, 167, 248, 61, 118, 179, 12, 73, 154, _
223, 0, 68, 143, 202, 53, 112, 179, 246, 41, 108, 167, 255, 57, 116, 143, _
210, 21, 88, 155, 38, 97, 172, 231, 20, 89, 158, 227, 32, 109, 170, 247, _
 60, 113, 182, 10, 73, 132, 195, 30, 69, 109, 210, 23, 92, 153, 199, 2, _
 65, 132, 251, 62, 117, 176, 239, 42, 89, 159, 32, 101, 174, 235, 52, 113, _
178, 222, 35, 100, 168, 235, 19, 92, 153, 234, 47, 96, 165, 254, 59, 117, _
176, 19, 86, 153, 220, 7, 66, 141, 200, 59, 113, 182, 235, 40, 101, 162, _
 31, 100, 169, 141, 208, 28, 89, 230, 35, 96, 140, 209, 22, 91, 152, 229, _
 35, 110, 165, 248, 63, 114, 177, 12, 75, 134, 221, 3, 68, 137, 202, 55, _
 95, 154, 201, 12, 67, 134, 254, 59, 116, 177, 210, 23, 88, 157, 38, 99, _
172, 232, 59, 87, 154, 221, 32, 99, 174, 233, 52, 127, 181, 233, 44, 103, _
162, 253, 56, 107, 174, 17, 84, 152, 197, 2, 79, 132, 249, 62, 115, 176, _
237, 42, 102, 157, 32, 103, 170, 233, 52, 115, 91, 232, 4, 72, 143, 210, _
 17, 92, 155, 230, 45, 96, 167, 250, 58, 119, 176, 13, 86, 155, 220, 1, _
 66, 143, 200, 52, 127, 178, 245, 13, 70, 131, 252, 57, 74, 143, 209, 20, _
 95, 154, 37, 96, 163, 207, 18, 85, 152, 228, 33, 110, 171, 248, 61, 114, _
183, 12, 73, 134, 191, 228, 41, 110, 211, 16, 93, 154, 199, 12, 65, 135, _
250, 57, 116, 179, 238, 21, 88, 159, 34, 97, 175, 232, 53, 126, 154, 223, _
 32, 74, 137, 212, 19, 93, 150, 235, 44, 97, 162, 255, 56, 117, 174, 19, _
 85, 152, 219, 6, 65, 140, 199, 58, 125, 176, 243, 41, 102, 163, 195, 6, _
 73, 140, 215, 18, 93, 152, 44, 72, 141, 210, 23, 92, 153, 230, 35, 96, _
165, 251, 62, 117, 176, 15, 74, 153, 220, 3, 70, 104, 212, 19, 94, 149, _
200, 15, 66, 129, 252, 59, 118, 142, 211, 20, 89, 154, 39, 96, 173, 230, _
 18, 87, 153, 220, 39, 98, 173, 232, 30, 83, 148, 233, 42, 102, 161, 252, _
 39, 106, 173, 16, 83, 158, 217, 4, 64, 133, 250, 63, 116, 177, 238, 43, _
 88, 157, 34, 102, 173, 149, 210, 31, 84, 192, 5, 74, 143, 212, 16, 95, _
154, 233, 44, 99, 166, 253, 56, 119, 178, 18, 87, 152, 221, 6, 67, 140, _
201, 58, 80, 151, 201, 10, 71, 128, 253, 6, 75, 140, 209, 18, 95, 153, _
 36, 111, 139, 206, 17, 84, 159, 218, 37, 96, 164, 249, 62, 115, 176, 238, _
 41, 100, 191, 226, 37, 111, 212, 17, 94, 155, 200, 13, 66, 135, 252, 57, _
119, 178, 209, 20, 91, 158, 37, 96, 175, 234, 57, 86, 124, 195, 6, 77, _
136, 215, 18, 81, 148, 235, 45, 102, 163, 252, 57, 106, 175, 16, 85, 158, _
219, 5, 64, 131, 198, 57, 124, 183, 242, 8, 69, 190, 194, 5, 72, 139}

#End Region

End Class
