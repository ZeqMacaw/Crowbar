Public Class SourceVtxStripGroup07

	'FROM: src/public/optimize.h
	'// a locking group
	'// a single vertex buffer
	'// a single index buffer
	'struct StripGroupHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	// These are the arrays of all verts and indices for this mesh.  strips index into this.
	'	int numVerts;
	'	int vertOffset;
	'	inline Vertex_t *pVertex( int i ) const 
	'	{ 
	'		return (Vertex_t *)(((byte *)this) + vertOffset) + i; 
	'	};

	'	int numIndices;
	'	int indexOffset;
	'	inline unsigned short *pIndex( int i ) const 
	'	{ 
	'		return (unsigned short *)(((byte *)this) + indexOffset) + i; 
	'	};

	'	int numStrips;
	'	int stripOffset;
	'	inline StripHeader_t *pStrip( int i ) const 
	'	{ 
	'		return (StripHeader_t *)(((byte *)this) + stripOffset) + i; 
	'	};

	'	unsigned char flags;
	'};
	'======
	' VERSION 49
	'FROM: AlienSwarm_source\src\public\optimize.h
	'// a locking group
	'// a single vertex buffer
	'// a single index buffer
	'struct StripGroupHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	// These are the arrays of all verts and indices for this mesh.  strips index into this.
	'	int numVerts;
	'	int vertOffset;
	'	inline Vertex_t *pVertex( int i ) const 
	'	{ 
	'		return (Vertex_t *)(((byte *)this) + vertOffset) + i; 
	'	};

	'	int numIndices;
	'	int indexOffset;
	'	inline unsigned short *pIndex( int i ) const 
	'	{ 
	'		return (unsigned short *)(((byte *)this) + indexOffset) + i; 
	'	};

	'	int numStrips;
	'	int stripOffset;
	'	inline StripHeader_t *pStrip( int i ) const 
	'	{ 
	'		return (StripHeader_t *)(((byte *)this) + stripOffset) + i; 
	'	};

	'	unsigned char flags;

	'	int numTopologyIndices;
	'	int topologyOffset;
	'	inline unsigned short *pTopologyIndex( int i ) const 
	'	{ 
	'		return (unsigned short *)(((byte *)this) + topologyOffset) + i; 
	'	};
	'};


	Public vertexCount As Integer
	Public vertexOffset As Integer

	Public indexCount As Integer
	Public indexOffset As Integer

	Public stripCount As Integer
	Public stripOffset As Integer

	Public flags As Byte

	'------
	' MDL VERSION 49 (except L4D and L4D2?) adds these two fields
	'	int numTopologyIndices;
	'	int topologyOffset;
	Public topologyIndexCount As Integer
	Public topologyIndexOffset As Integer



	Public theVtxVertexes As List(Of SourceVtxVertex07)
	Public theVtxIndexes As List(Of UShort)
	Public theVtxStrips As List(Of SourceVtxStrip07)
	Public theVtxTopologyIndexes As List(Of UShort)


	'FROM: src/public/optimize.h
	'	Enum StripGroupFlags_t
	'{
	'	STRIPGROUP_IS_FLEXED		= 0x01,
	'	STRIPGROUP_IS_HWSKINNED		= 0x02,
	'	STRIPGROUP_IS_DELTA_FLEXED	= 0x04,
	'	STRIPGROUP_SUPPRESS_HW_MORPH = 0x08,	// NOTE: This is a temporary flag used at run time.
	'};

	Public Const SourceStripGroupFlexed As Byte = &H1
	Public Const SourceStripGroupHwSkinned As Byte = &H2
	Public Const SourceStripGroupDeltaFixed As Byte = &H4
	Public Const SourceStripGroupSuppressHwMorph As Byte = &H8

End Class
