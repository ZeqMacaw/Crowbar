Public Class SourceMdlFlex37

	'struct mstudioflex_t
	'{
	'	int	flexdesc;	// input value
	'
	'	float	target0;	// zero
	'	float	target1;	// one
	'	float	target2;	// one
	'	float	target3;	// zero
	'
	'	int	numverts;
	'	int	vertindex;
	'	inline	mstudiovertanim_t *pVertanim( int i ) const { return  (mstudiovertanim_t *)(((byte *)this) + vertindex) + i; };
	'};

	Public flexDescIndex As Integer

	Public target0 As Double
	Public target1 As Double
	Public target2 As Double
	Public target3 As Double

	Public vertCount As Integer
	Public vertOffset As Integer

	Public theVertAnims As List(Of SourceMdlVertAnim37)

End Class
