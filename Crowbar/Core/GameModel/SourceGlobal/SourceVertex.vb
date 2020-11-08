Public Class SourceVertex

	'// NOTE: This is exactly 48 bytes
	'struct mstudiovertex_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	mstudioboneweight_t	m_BoneWeights;
	'	Vector				m_vecPosition;
	'	Vector				m_vecNormal;
	'	Vector2D			m_vecTexCoord;

	'	mstudiovertex_t() {}

	'private:
	'	// No copy constructors allowed
	'	mstudiovertex_t(const mstudiovertex_t& vOther);
	'};

	'	mstudioboneweight_t	m_BoneWeights;
	Public boneWeight As SourceBoneWeight

	'NOTE: Changed to Double, so that the values will be properly written to file with 6 decimal digits.
	''	Vector				m_vecPosition;
	'Public positionX As Single
	'Public positionY As Single
	'Public positionZ As Single
	''	Vector				m_vecNormal;
	'Public normalX As Single
	'Public normalY As Single
	'Public normalZ As Single
	''	Vector2D			m_vecTexCoord;
	'Public texCoordX As Single
	'Public texCoordY As Single
	'	Vector				m_vecPosition;
	Public positionX As Double
	Public positionY As Double
	Public positionZ As Double
	'	Vector				m_vecNormal;
	Public normalX As Double
	Public normalY As Double
	Public normalZ As Double
	'	Vector2D			m_vecTexCoord;
	Public texCoordX As Double
	Public texCoordY As Double

End Class
