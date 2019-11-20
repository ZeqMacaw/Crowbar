Public Class SourceVvdFileData04
	Inherits SourceFileData

	'FROM: SourceEngine2007\src_main\public\studio.h
	'// ----------------------------------------------------------
	'// Studio Model Vertex Data File
	'// Position independent flat data for cache manager
	'// ----------------------------------------------------------
	'
	'// little-endian "IDSV"
	'#define MODEL_VERTEX_FILE_ID		(('V'<<24)+('S'<<16)+('D'<<8)+'I')
	'#define MODEL_VERTEX_FILE_VERSION	4
	'// this id (IDCV) is used once the vertex data has been compressed (see CMDLCache::CreateThinVertexes)
	'#define MODEL_VERTEX_FILE_THIN_ID	(('V'<<24)+('C'<<16)+('D'<<8)+'I')
	'
	'struct vertexFileHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int		id;								// MODEL_VERTEX_FILE_ID
	'	int		version;						// MODEL_VERTEX_FILE_VERSION
	'	long	checksum;						// same as studiohdr_t, ensures sync
	'	int		numLODs;						// num of valid lods
	'	int		numLODVertexes[MAX_NUM_LODS];	// num verts for desired root lod
	'	int		numFixups;						// num of vertexFileFixup_t
	'	int		fixupTableStart;				// offset from base to fixup table
	'	int		vertexDataStart;				// offset from base to vertex block
	'	int		tangentDataStart;				// offset from base to tangent block
	'[...functions...]
	'};

	' "IDSV"
	Public id(3) As Char
	Public version As Integer
	Public checksum As Integer
	Public lodCount As Integer
	Public lodVertexCount(MAX_NUM_LODS - 1) As Integer
	Public fixupCount As Integer
	Public fixupTableOffset As Integer
	Public vertexDataOffset As Integer
	Public tangentDataOffset As Integer



	'Public theVertexesByLod(MAX_NUM_LODS - 1) As List(Of SourceVertex)
	Public theVertexes As List(Of SourceVertex)
	Public theFixups As List(Of SourceVvdFixup04)
	Public theFixedVertexesByLod(MAX_NUM_LODS - 1) As List(Of SourceVertex)

End Class
