Public Class BitmapInfoHeader

	'FROM: c:\Program Files (x86)\Windows Kits\8.1\Include\um\wingdi.h
	'typedef struct tagBITMAPINFOHEADER{
	'        DWORD      biSize;
	'        LONG       biWidth;
	'        LONG       biHeight;
	'        WORD       biPlanes;
	'        WORD       biBitCount;
	'        DWORD      biCompression;
	'        DWORD      biSizeImage;
	'        LONG       biXPelsPerMeter;
	'        LONG       biYPelsPerMeter;
	'        DWORD      biClrUsed;
	'        DWORD      biClrImportant;
	'} BITMAPINFOHEADER, FAR *LPBITMAPINFOHEADER, *PBITMAPINFOHEADER;

	Public size As UInt32
	Public width As Int32
	Public height As Int32
	Public planes As UInt16
	Public bitCount As UInt16
	Public compression As UInt32
	Public sizeImage As UInt32
	Public xPelsPerMeter As Int32
	Public yPelsPerMeter As Int32
	Public clrUsed As UInt32
	Public clrImportant As UInt32

End Class
