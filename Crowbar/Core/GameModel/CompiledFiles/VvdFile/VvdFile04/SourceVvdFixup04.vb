Public Class SourceVvdFixup04

	'FROM: public/studio.h
	'// apply sequentially to lod sorted vertex and tangent pools to re-establish mesh order
	'struct vertexFileFixup_t
	'{
	'	int		lod;				// used to skip culled root lod
	'	int		sourceVertexID;		// absolute index from start of vertex/tangent blocks
	'	int		numVertexes;
	'};

	Public lodIndex As Integer
	Public vertexIndex As Integer
	Public vertexCount As Integer

End Class
