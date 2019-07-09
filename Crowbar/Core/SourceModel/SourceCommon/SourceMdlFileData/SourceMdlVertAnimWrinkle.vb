Public Class SourceMdlVertAnimWrinkle
	Inherits SourceMdlVertAnim

	'FROM: SourceEngineXXXX_source\public\studio.h
	'// this is the memory image of vertex anims (16-bit fixed point)
	'struct mstudiovertanim_wrinkle_t : public mstudiovertanim_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();

	'	short	wrinkledelta;

	'	inline void SetWrinkleFixed( float flWrinkle )
	'	{
	'		int nWrinkleDeltaInt = flWrinkle * g_VertAnimFixedPointScaleInv;
	'		wrinkledelta = clamp( nWrinkleDeltaInt, -32767, 32767 );
	'	}

	'	inline Vector4D GetDeltaFixed()
	'	{
	'		return Vector4D( delta[0]*g_VertAnimFixedPointScale, delta[1]*g_VertAnimFixedPointScale, delta[2]*g_VertAnimFixedPointScale, wrinkledelta*g_VertAnimFixedPointScale );
	'	}

	'	inline void GetDeltaFixed4DAligned( Vector4DAligned *vFillIn )
	'	{
	'		vFillIn->Set( delta[0]*g_VertAnimFixedPointScale, delta[1]*g_VertAnimFixedPointScale, delta[2]*g_VertAnimFixedPointScale, wrinkledelta*g_VertAnimFixedPointScale );
	'	}
	'};



	Public wrinkleDelta As Short

End Class
