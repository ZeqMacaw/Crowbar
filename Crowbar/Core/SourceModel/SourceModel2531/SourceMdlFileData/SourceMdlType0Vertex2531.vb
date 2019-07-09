Public Class SourceMdlType0Vertex2531

	'struct mstudiovertex_t // NOTE: This is exactly 64 bytes, two cache lines
	'{
	'	mstudioboneweight_t	m_BoneWeights;
	'	Vector		m_vecPosition;
	'	Vector		m_vecNormal;
	'	Vector2D	m_vecTexCoord;
	'};
	'
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

	' Example: "character\pc\female\brujah\armor0\brujah_female_armor_0.mdl" 
	'          seems to use struct size of 12 + 32 = 44 bytes.
	'NOTE: The weight and bone fields are smaller than above. VtMB must only accept max of 3 bone weights per vertex.
	'Public weight(1) As Double
	'Public boneIndex(1) As Short
	'Public weight As Double
	'Public boneIndex As Short
	' Weights total to 255.
	Public weight(2) As Byte
	Public unknown1 As Byte
	Public boneIndex(2) As Short
	Public unknown2 As Short
	'Public boneCount As Byte
	'Public materialIndex As Short
	'Public firstRef As Short
	'Public lastRef As Short

	Public position As New SourceVector()
	Public normal As New SourceVector()
	Public texCoordU As Double
	Public texCoordV As Double
	'Public test(7) As Double

End Class
