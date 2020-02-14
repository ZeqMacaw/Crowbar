Public Class SourceMdlTriangleVertex06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	short				vertindex;		// index into vertex array (relative)
	'	short				normindex;		// index into normal array (relative)
	'	short				s, t;			// s,t position on skin
	'} mstudiotrivert_t;

	Public vertexIndex As UInt16
	Public normalIndex As UInt16
	Public s As Int16
	Public t As Int16

End Class
