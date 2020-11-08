Imports System.IO

Public Class BitmapFile

#Region "Creation and Destruction"

	'Public Sub New(ByVal mdlFileReader As BinaryReader, ByVal mdlFileData As SourceMdlFileData10)
	'	Me.theInputFileReader = mdlFileReader
	'	Me.theMdlFileData = mdlFileData
	'End Sub

	Public Sub New(ByVal bmpPathFileName As String, ByVal width As UInt32, ByVal height As UInt32, ByVal data As List(Of Byte))
		Me.thePathFileName = bmpPathFileName
		Me.theWidth = width
		Me.theHeight = height
		Me.theData = data
	End Sub

#End Region

#Region "Methods"

	'int WriteBMPfile (char *szFile, byte *pbBits, int width, int height, byte *pbPalette)
	'{
	'	int i, rc = 0;
	'	FILE *pfile = NULL;
	'	BITMAPFILEHEADER bmfh;
	'	BITMAPINFOHEADER bmih;
	'	RGBQUAD rgrgbPalette[256];
	'	ULONG cbBmpBits;
	'	BYTE* pbBmpBits;
	'	byte  *pb, *pbPal = NULL;
	'	ULONG cbPalBytes;
	'	ULONG biTrueWidth;
	'
	'	// Bogus parameter check
	'	if (!(pbPalette != NULL && pbBits != NULL))
	'		{ rc = -1000; goto GetOut; }
	'
	'	// File exists?
	'	if ((pfile = fopen(szFile, "wb")) == NULL)
	'		{ rc = -1; goto GetOut; }
	'
	'	biTrueWidth = ((width + 3) & ~3);
	'	cbBmpBits = biTrueWidth * height;
	'	cbPalBytes = 256 * sizeof( RGBQUAD );
	'
	'	// Bogus file header check
	'	bmfh.bfType = MAKEWORD( 'B', 'M' );
	'	bmfh.bfSize = sizeof bmfh + sizeof bmih + cbBmpBits + cbPalBytes;
	'	bmfh.bfReserved1 = 0;
	'	bmfh.bfReserved2 = 0;
	'	bmfh.bfOffBits = sizeof bmfh + sizeof bmih + cbPalBytes;
	'
	'	// Write file header
	'	if (fwrite(&bmfh, sizeof bmfh, 1/*count*/, pfile) != 1)
	'		{ rc = -2; goto GetOut; }
	'
	'	// Size of structure
	'	bmih.biSize = sizeof bmih;
	'	// Width
	'	bmih.biWidth = biTrueWidth;
	'	// Height
	'	bmih.biHeight = height;
	'	// Only 1 plane 
	'	bmih.biPlanes = 1;
	'	// Only 8-bit supported.
	'	bmih.biBitCount = 8;
	'	// Only non-compressed supported.
	'	bmih.biCompression = BI_RGB;
	'	bmih.biSizeImage = 0;
	'
	'	// huh?
	'	bmih.biXPelsPerMeter = 0;
	'	bmih.biYPelsPerMeter = 0;
	'
	'	// Always full palette
	'	bmih.biClrUsed = 256;
	'	bmih.biClrImportant = 0;
	'	
	'	// Write info header
	'	if (fwrite(&bmih, sizeof bmih, 1/*count*/, pfile) != 1)
	'		{ rc = -3; goto GetOut; }
	'	
	'
	'	// convert to expanded palette
	'	pb = pbPalette;
	'
	'	// Copy over used entries
	'	for (i = 0; i < (int)bmih.biClrUsed; i++)
	'	{
	'		rgrgbPalette[i].rgbRed   = *pb++;
	'		rgrgbPalette[i].rgbGreen = *pb++;
	'		rgrgbPalette[i].rgbBlue  = *pb++;
	'        rgrgbPalette[i].rgbReserved = 0;
	'	}
	'
	'	// Write palette (bmih.biClrUsed entries)
	'	cbPalBytes = bmih.biClrUsed * sizeof( RGBQUAD );
	'	if (fwrite(rgrgbPalette, cbPalBytes, 1/*count*/, pfile) != 1)
	'		{ rc = -6; goto GetOut; }
	'
	'
	'	pbBmpBits = malloc(cbBmpBits);
	'
	'	pb = pbBits;
	'	// reverse the order of the data.
	'	pb += (height - 1) * width;
	'	for(i = 0; i < bmih.biHeight; i++)
	'	{
	'		memmove(&pbBmpBits[biTrueWidth * i], pb, width);
	'		pb -= width;
	'	}
	'
	'	// Write bitmap bits (remainder of file)
	'	if (fwrite(pbBmpBits, cbBmpBits, 1/*count*/, pfile) != 1)
	'		{ rc = -7; goto GetOut; }
	'
	'	free(pbBmpBits);
	'
	'GetOut:
	'	if (pfile) 
	'		fclose(pfile);
	'
	'	return rc;
	'}
	Public Sub Write()
		Dim outputFileStream As FileStream = Nothing
		Dim outputFileWriter As BinaryWriter = Nothing
		Try
			outputFileStream = New FileStream(Me.thePathFileName, FileMode.OpenOrCreate)
			If outputFileStream IsNot Nothing Then
				Try
					outputFileWriter = New BinaryWriter(outputFileStream, System.Text.Encoding.ASCII)

					'	biTrueWidth = ((width + 3) & ~3);
					'	cbBmpBits = biTrueWidth * height;
					'	cbPalBytes = 256 * sizeof( RGBQUAD );
					Dim alignedWidthUsedInFile As UInt32
					Dim fileHeaderSize As UInt32
					Dim infoHeaderSize As UInt32
					Dim paletteSize As UInt32
					Dim dataSize As UInt32
					'paddedWidthUsedInFile = CUInt(MathModule.AlignLong(Me.theWidth, 3))
					'NOTE: Align to 4 byte boundary.
					alignedWidthUsedInFile = CUInt(MathModule.AlignLong(Me.theWidth, 4))
					fileHeaderSize = 14
					infoHeaderSize = 40
					' 256 * size of BitmapRgbQuad = 256 * 4 = 1024
					paletteSize = 1024
					dataSize = alignedWidthUsedInFile * Me.theHeight

					'	// Write file header
					outputFileWriter.Write("B"c)
					outputFileWriter.Write("M"c)
					outputFileWriter.Write(fileHeaderSize + infoHeaderSize + paletteSize + dataSize)
					outputFileWriter.Write(CType(0, UInt16))
					outputFileWriter.Write(CType(0, UInt16))
					outputFileWriter.Write(fileHeaderSize + infoHeaderSize + paletteSize)

					'	// Write info header
					outputFileWriter.Write(infoHeaderSize)
					outputFileWriter.Write(alignedWidthUsedInFile)
					outputFileWriter.Write(Me.theHeight)
					outputFileWriter.Write(CType(1, UInt16))
					outputFileWriter.Write(CType(8, UInt16))
					outputFileWriter.Write(CType(0, UInt32))
					outputFileWriter.Write(CType(0, UInt32))
					outputFileWriter.Write(CType(0, UInt32))
					outputFileWriter.Write(CType(0, UInt32))
					outputFileWriter.Write(CType(256, UInt32))
					outputFileWriter.Write(CType(0, UInt32))

					'	// Write palette (bmih.biClrUsed entries)
					For dataIndex As Integer = Me.theData.Count - 768 To Me.theData.Count - 1 Step 3
						outputFileWriter.Write(Me.theData(dataIndex + 2))
						outputFileWriter.Write(Me.theData(dataIndex + 1))
						outputFileWriter.Write(Me.theData(dataIndex))
						outputFileWriter.Write(CType(0, Byte))
					Next

					'	// Write bitmap bits (remainder of file)
					' Write the rows in reverse order.
					Dim startOfLastRowOffset As Integer
					startOfLastRowOffset = CInt(Me.theData.Count - 768 - Me.theWidth)
					'For dataStoredIndex As Integer = startOfLastRowOffset To 0 Step CInt(-Me.theWidth)
					Dim dataStoredIndex As Integer = startOfLastRowOffset
					For rowIndex As UInteger = 0 To Me.theHeight - 1UI
						For dataIndex As Integer = dataStoredIndex To CInt(dataStoredIndex + Me.theWidth - 1)
							outputFileWriter.Write(Me.theData(dataIndex))
						Next
						For paddingIndex As Integer = CInt(dataStoredIndex + Me.theWidth) To CInt(dataStoredIndex + alignedWidthUsedInFile - 1)
							outputFileWriter.Write(CType(0, Byte))
						Next
						dataStoredIndex -= CInt(Me.theWidth)
					Next
				Catch ex As Exception
					Dim debug As Integer = 4242
				Finally
					If outputFileWriter IsNot Nothing Then
						outputFileWriter.Close()
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

	'Protected theInputFileReader As BinaryReader
	'Protected theOutputFileWriter As BinaryWriter

	Public thePathFileName As String
	Public theWidth As UInt32
	Public theHeight As UInt32
	Public theData As List(Of Byte)

#End Region


End Class
