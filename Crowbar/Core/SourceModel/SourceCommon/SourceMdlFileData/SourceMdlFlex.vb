Public Class SourceMdlFlex

	'FROM: SourceEngineXXXX_source\public\studio.h
	'struct mstudioflex_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					flexdesc;	// input value

	'	float				target0;	// zero
	'	float				target1;	// one
	'	float				target2;	// one
	'	float				target3;	// zero

	'	int					numverts;
	'	int					vertindex;

	'	inline	mstudiovertanim_t *pVertanim( int i ) const { Assert( vertanimtype == STUDIO_VERT_ANIM_NORMAL ); return (mstudiovertanim_t *)(((byte *)this) + vertindex) + i; };
	'	inline	mstudiovertanim_wrinkle_t *pVertanimWrinkle( int i ) const { Assert( vertanimtype == STUDIO_VERT_ANIM_WRINKLE ); return  (mstudiovertanim_wrinkle_t *)(((byte *)this) + vertindex) + i; };

	'	inline	byte *pBaseVertanim( ) const { return ((byte *)this) + vertindex; };
	'	inline	int	VertAnimSizeBytes() const { return ( vertanimtype == STUDIO_VERT_ANIM_NORMAL ) ? sizeof(mstudiovertanim_t) : sizeof(mstudiovertanim_wrinkle_t); }

	'	int					flexpair;	// second flex desc
	'	unsigned char		vertanimtype;	// See StudioVertAnimType_t
	'	unsigned char		unusedchar[3];
	'	int					unused[6];
	'};



	Public flexDescIndex As Integer

	Public target0 As Double
	Public target1 As Double
	Public target2 As Double
	Public target3 As Double

	Public vertCount As Integer
	Public vertOffset As Integer

	Public flexDescPartnerIndex As Integer
	Public vertAnimType As Byte
	Public unusedChar(2) As Char
	Public unused(5) As Integer



	Public theVertAnims As List(Of SourceMdlVertAnim)



	'Enum StudioVertAnimType_t
	'{
	'	STUDIO_VERT_ANIM_NORMAL = 0,
	'	STUDIO_VERT_ANIM_WRINKLE,
	'};
	Public STUDIO_VERT_ANIM_NORMAL As Byte = 0
	Public STUDIO_VERT_ANIM_WRINKLE As Byte = 1

End Class
