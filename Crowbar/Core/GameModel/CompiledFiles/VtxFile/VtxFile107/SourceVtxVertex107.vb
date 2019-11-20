Public Class SourceVtxVertex107

	'FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
	'struct Vertex_t
	'{
	'	// these index into the mesh's vert[origMeshVertID]'s bones
	'	unsigned char boneWeightIndex[MAX_NUM_BONES_PER_VERT];
	'//	unsigned char boneWeights[MAX_NUM_BONES_PER_VERT];
	'	// for sw skinned verts, these are indices into the global list of bones
	'	// for hw skinned verts, these are hardware bone indices
	'	short boneID[MAX_NUM_BONES_PER_VERT];
	'	short origMeshVertID;
	'	unsigned char numBones;
	'};
	'------
	'FROM: VAMPTools-master\MDLConverter\inc\external\optimize.h
	'struct Vertex_t
	'{
	'	unsigned short origMeshVertID;
	'
	'	// these index into the mesh's vert[origMeshVertID]'s bones
	'	unsigned char boneWeightIndex[MAX_NUM_BONES_PER_VERT];
	'	unsigned char numBones;
	'	
	'	// for sw skinned verts, these are indices into the global list of bones
	'	// for hw skinned verts, these are hardware bone indices
	'	char boneID[MAX_NUM_BONES_PER_VERT];
	'	char unknown[3];
	'};

	'Public boneWeightIndex(SourceModel2531.MAX_NUM_BONES_PER_VERT - 1) As Byte
	'Public boneId(SourceModel2531.MAX_NUM_BONES_PER_VERT - 1) As Short
	'Public originalMeshVertexIndex As Short
	'Public boneCount As Byte
	'------
	'Public originalMeshVertexIndex As UShort
	'Public boneWeightIndex(SourceModel2531.MAX_NUM_BONES_PER_VERT - 1) As Byte
	'Public boneCount As Byte
	'Public boneIndex(SourceModel2531.MAX_NUM_BONES_PER_VERT - 1) As Byte
	'Public unknown(2) As Byte
	'------
	'NOTE: 12 bytes
	Public unknown01 As Short
	Public boneIndex(SourceModel2531.MAX_NUM_BONES_PER_VERT - 1) As Short
	Public unknown02 As Short
	Public originalMeshVertexIndex As UShort

End Class
