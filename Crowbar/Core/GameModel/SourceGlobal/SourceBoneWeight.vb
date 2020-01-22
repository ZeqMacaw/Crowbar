Public Class SourceBoneWeight

	'// 16 bytes
	'struct mstudioboneweight_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	float	weight[MAX_NUM_BONES_PER_VERT];
	'	char	bone[MAX_NUM_BONES_PER_VERT]; 
	'	byte	numbones;

	'//	byte	material;
	'//	short	firstref;
	'//	short	lastref;
	'};

	'	float	weight[MAX_NUM_BONES_PER_VERT];
	Public weight(MAX_NUM_BONES_PER_VERT - 1) As Single
	'	char	bone[MAX_NUM_BONES_PER_VERT]; 
	Public bone(MAX_NUM_BONES_PER_VERT - 1) As Byte
	'	byte	numbones;
	Public boneCount As Byte

End Class
