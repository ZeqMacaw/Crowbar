Public Class SourceMdlMesh10

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// meshes
	'typedef struct 
	'{
	'	int					numtris;
	'	int					triindex;
	'	int					skinref;
	'	int					numnorms;		// per mesh normals
	'	int					normindex;		// normal vec3_t
	'} mstudiomesh_t;

	Public faceCount As Integer
	Public faceOffset As Integer
	Public skinref As Integer
	Public normalCount As Integer
	' Based on source code, this field does not seem to be used.
	Public normalOffset As Integer


	Public theStripsAndFans As List(Of SourceMeshTriangleStripOrFan10)

End Class
