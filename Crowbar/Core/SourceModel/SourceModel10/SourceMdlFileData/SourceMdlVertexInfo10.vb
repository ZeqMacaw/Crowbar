Public Class SourceMdlVertexInfo10

	'NOTE: This struct in the source code is used to build what is stored in the MDL file.
	'FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.h
	'typedef struct {
	'	int					vertindex;
	'	int					normindex;		// index into normal array
	'	int					s,t;
	'	float				u,v;
	'} s_trianglevert_t;

	'NOTE: This class is used only for help in reading in the data and does not correlate to the source code directly.
	'NOTE: This class only uses some of the fields based on the above struct.
	Public vertexIndex As UInt16
	Public normalIndex As UInt16
	Public s As Int16
	Public t As Int16

End Class
