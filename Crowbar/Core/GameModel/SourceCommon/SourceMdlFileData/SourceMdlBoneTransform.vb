Public Class SourceMdlBoneTransform

	'FROM: SourceEngine2007\src_main\public\studio.h
	'//-----------------------------------------------------------------------------
	'// Src bone transforms are transformations that will convert .dmx or .smd-based animations into .mdl-based animations
	'// NOTE: The operation you should apply is: pretransform * bone transform * posttransform
	'//-----------------------------------------------------------------------------
	'struct mstudiosrcbonetransform_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();

	'	int			sznameindex;
	'	inline const char *pszName( void ) const { return ((char *)this) + sznameindex; }
	'	matrix3x4_t	pretransform;	
	'	matrix3x4_t	posttransform;	
	'};

	'	int			sznameindex;
	Public nameOffset As Integer

	'	matrix3x4_t	pretransform;	
	Public preTransformColumn0 As SourceVector
	Public preTransformColumn1 As SourceVector
	Public preTransformColumn2 As SourceVector
	Public preTransformColumn3 As SourceVector
	'	matrix3x4_t	posttransform;	
	Public postTransformColumn0 As SourceVector
	Public postTransformColumn1 As SourceVector
	Public postTransformColumn2 As SourceVector
	Public postTransformColumn3 As SourceVector



	Public theName As String

End Class
