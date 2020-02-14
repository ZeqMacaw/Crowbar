Public Class SourceVtxStrip07

	'FROM: src/public/optimize.h
	'// a strip is a piece of a stripgroup that is divided by bones 
	'// (and potentially tristrips if we remove some degenerates.)
	'struct StripHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	// indexOffset offsets into the mesh's index array.
	'	int numIndices;
	'	int indexOffset;

	'	// vertexOffset offsets into the mesh's vert array.
	'	int numVerts;
	'	int vertOffset;

	'	// use this to enable/disable skinning.  
	'	// May decide (in optimize.cpp) to put all with 1 bone in a different strip 
	'	// than those that need skinning.
	'	short numBones;  

	'	unsigned char flags;

	'	int numBoneStateChanges;
	'	int boneStateChangeOffset;
	'	inline BoneStateChangeHeader_t *pBoneStateChange( int i ) const 
	'	{ 
	'		return (BoneStateChangeHeader_t *)(((byte *)this) + boneStateChangeOffset) + i; 
	'	};
	'};

	Public indexCount As Integer
	Public indexMeshIndex As Integer

	Public vertexCount As Integer
	Public vertexMeshIndex As Integer

	Public boneCount As Short

	Public flags As Byte

	Public boneStateChangeCount As Integer
	Public boneStateChangeOffset As Integer

	Public unknownBytes01 As Integer
	Public unknownBytes02 As Integer



	Public theVtxBoneStateChanges As List(Of SourceVtxBoneStateChange07)



	'FROM: src/public/optimize.h
	'enum StripHeaderFlags_t {
	'	STRIP_IS_TRILIST	= 0x01,
	'	STRIP_IS_TRISTRIP	= 0x02
	'};

	Public Const SourceStripTriList As Byte = &H1
	Public Const SourceStripTriStrip As Byte = &H2

End Class
