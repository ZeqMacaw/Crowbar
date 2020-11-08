Public Class SourceMdlBoneWeight37

	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\Public\studio.h
	'// 16 bytes
	'struct mstudioboneweight_t
	'{
	'	float	weight[4];
	'	short	bone[4]; 
	'
	'	short	numbones;
	'	short	material;
	'
	'	short	firstref;
	'	short	lastref;
	'};

	Public weight(3) As Double
	Public bone(3) As Short
	Public boneCount As Short
	Public material As Short
	Public firstRef As Short
	Public lastRef As Short

End Class
