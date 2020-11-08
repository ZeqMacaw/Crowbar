Public Class SourceMdlModelVertexData

	'struct mstudio_modelvertexdata_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	Vector				*Position( int i ) const;
	'	Vector				*Normal( int i ) const;
	'	Vector4D			*TangentS( int i ) const;
	'	Vector2D			*Texcoord( int i ) const;
	'	mstudioboneweight_t	*BoneWeights( int i ) const;
	'	mstudiovertex_t		*Vertex( int i ) const;
	'	bool				HasTangentData( void ) const;
	'	int					GetGlobalVertexIndex( int i ) const;
	'	int					GetGlobalTangentIndex( int i ) const;

	'	// base of external vertex data stores
	'	const void			*pVertexData;
	'	const void			*pTangentData;
	'};

	'	const void			*pVertexData;
	Public vertexDataP As Integer
	'	const void			*pTangentData;
	Public tangentDataP As Integer

End Class
