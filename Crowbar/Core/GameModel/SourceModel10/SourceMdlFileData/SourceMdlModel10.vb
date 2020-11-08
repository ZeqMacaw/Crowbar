Public Class SourceMdlModel10

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// studio models
	'typedef struct
	'{
	'	char				name[64];
	'
	'	int					type;
	'
	'	float				boundingradius;
	'
	'	int					nummesh;
	'	int					meshindex;
	'
	'	int					numverts;		// number of unique vertices
	'	int					vertinfoindex;	// vertex bone info
	'	int					vertindex;		// vertex vec3_t
	'	int					numnorms;		// number of unique surface normals
	'	int					norminfoindex;	// normal bone info
	'	int					normindex;		// normal vec3_t
	'
	'	int					numgroups;		// deformation groups
	'	int					groupindex;
	'} mstudiomodel_t;

	Public name(63) As Char
	Public type As Integer
	'	float				boundingradius;
	Public boundingRadius As Double
	Public meshCount As Integer
	Public meshOffset As Integer

	Public vertexCount As Integer
	Public vertexBoneInfoOffset As Integer
	Public vertexOffset As Integer
	Public normalCount As Integer
	Public normalBoneInfoOffset As Integer
	Public normalOffset As Integer

	' Based on source code, these two fields do not seem to be used.
	Public groupCount As Integer
	Public groupOffset As Integer


	Public theSmdFileName As String
	Public theName As String
	Public theMeshes As List(Of SourceMdlMesh10)
	Public theNormals As List(Of SourceVector)
	'Public theNormals As List(Of SourceVectorSingle)
	Public theNormalBoneInfos As List(Of Integer)
	Public theVertexes As List(Of SourceVector)
	'Public theVertexes As List(Of SourceVectorSingle)
	Public theVertexBoneInfos As List(Of Integer)

End Class
