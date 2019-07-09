Public Class SourceMdlCompressedIkError

    'FROM: se2007_src\src_main\public\studio.h
    'struct mstudiocompressedikerror_t
    '{
    '	DECLARE_BYTESWAP_DATADESC();
    '	float	scale[6];
    '	short	offset[6];
    '	inline mstudioanimvalue_t *pAnimvalue( int i ) const { if (offset[i] > 0) return  (mstudioanimvalue_t *)(((byte *)this) + offset[i]); else return NULL; };
    '	mstudiocompressedikerror_t(){}

    'private:
    '	// No copy constructors allowed
    '	mstudiocompressedikerror_t(const mstudiocompressedikerror_t& vOther);
    '};



	Public scale(5) As Double
    Public offset(5) As Short

	Public theAnimValues(5) As List(Of SourceMdlAnimationValue)

End Class
