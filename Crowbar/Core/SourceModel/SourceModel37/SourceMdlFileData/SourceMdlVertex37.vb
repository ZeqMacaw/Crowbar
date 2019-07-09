Public Class SourceMdlVertex37

	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\Public\studio.h
	'// NOTE: This is exactly 64 bytes, two cache lines
	'struct mstudiovertex_t
	'{
	'	mstudioboneweight_t	m_BoneWeights;
	'	Vector			m_vecPosition;
	'	Vector			m_vecNormal;
	'	Vector2D		m_vecTexCoord;
	'};

	Public Sub New()
		Me.boneWeight = New SourceMdlBoneWeight37()
		Me.position = New SourceVector()
		Me.normal = New SourceVector()
	End Sub

	Public boneWeight As SourceMdlBoneWeight37
	Public position As SourceVector
	Public normal As SourceVector
	Public texCoordX As Double
	Public texCoordY As Double

End Class
