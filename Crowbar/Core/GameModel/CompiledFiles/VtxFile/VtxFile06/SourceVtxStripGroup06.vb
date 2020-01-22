Public Class SourceVtxStripGroup06

	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
	'// a locking group
	'// a single vertex buffer
	'// a single index buffer
	'struct StripGroupHeader_t
	'{
	'	// These are the arrays of all verts and indices for this mesh.  strips index into this.
	'	int numVerts;
	'	int vertOffset;
	'	inline Vertex_t *pVertex( int i ) const 
	'	{ 
	'		return (Vertex_t *)(((byte *)this) + vertOffset) + i; 
	'	};
	'
	'	int numIndices;
	'	int indexOffset;
	'	inline unsigned short *pIndex( int i ) const 
	'	{ 
	'		return (unsigned short *)(((byte *)this) + indexOffset) + i; 
	'	};
	'
	'	int numStrips;
	'	int stripOffset;
	'	inline StripHeader_t *pStrip( int i ) const 
	'	{ 
	'		return (StripHeader_t *)(((byte *)this) + stripOffset) + i; 
	'	};
	'
	'	unsigned char flags;
	'};

	Public vertexCount As Integer
	Public vertexOffset As Integer
	Public indexCount As Integer
	Public indexOffset As Integer
	Public stripCount As Integer
	Public stripOffset As Integer
	Public flags As Byte

	Public theVtxIndexes As List(Of UShort)
	Public theVtxStrips As List(Of SourceVtxStrip06)
	Public theVtxVertexes As List(Of SourceVtxVertex06)


	'FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
	'      [Version 36]
	'enum StripGroupFlags_t {
	'	STRIPGROUP_IS_FLEXED	= 0x01,
	'	STRIPGROUP_IS_HWSKINNED	= 0x02
	'};
	'------
	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
	'enum StripGroupFlags_t {
	'	STRIPGROUP_IS_FLEXED	= 0x01,
	'	STRIPGROUP_IS_HWSKINNED	= 0x02
	'};

	Public Const STRIPGROUP_IS_FLEXED As Byte = &H1
	Public Const STRIPGROUP_IS_HWSKINNED As Byte = &H2

End Class
