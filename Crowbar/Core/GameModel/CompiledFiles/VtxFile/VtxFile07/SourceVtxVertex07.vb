Public Class SourceVtxVertex07

	'FROM: src/public/studio.h
	'// NOTE!!! : Changing this number also changes the vtx file format!!!!!
	'#define MAX_NUM_BONES_PER_VERT 3

	'FROM: src/public/optimize.h
	'struct Vertex_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	// these index into the mesh's vert[origMeshVertID]'s bones
	'	unsigned char boneWeightIndex[MAX_NUM_BONES_PER_VERT];
	'	unsigned char numBones;

	'	unsigned short origMeshVertID;

	'	// for sw skinned verts, these are indices into the global list of bones
	'	// for hw skinned verts, these are hardware bone indices
	'	char boneID[MAX_NUM_BONES_PER_VERT];
	'};

	Public boneWeightIndex(MAX_NUM_BONES_PER_VERT - 1) As Byte
	Public boneCount As Byte

	Public originalMeshVertexIndex As UShort

	Public boneId(MAX_NUM_BONES_PER_VERT - 1) As Byte

End Class
