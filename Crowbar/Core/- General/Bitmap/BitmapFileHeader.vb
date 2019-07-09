Public Class BitmapFileHeader

	'FROM: c:\Program Files (x86)\Windows Kits\8.1\Include\um\wingdi.h
	'typedef struct tagBITMAPFILEHEADER {
	'        WORD    bfType;
	'        DWORD   bfSize;
	'        WORD    bfReserved1;
	'        WORD    bfReserved2;
	'        DWORD   bfOffBits;
	'} BITMAPFILEHEADER, FAR *LPBITMAPFILEHEADER, *PBITMAPFILEHEADER;

	Public type As UInt16
	Public size As UInt32
	Public reserved1 As UInt16
	Public reserved2 As UInt16
	Public dataOffset As UInt32

End Class
