Public Class SourceMdlMesh06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	int					numtris;
	'	int					triindex;		// separate triangles (->mstudiotrivert_t)
	'	int					skinref;
	'	int					numnorms;		// per mesh normals
	'	int					normindex;		// normal vec3_t
	'	// TOMAS: "0"
	'} mstudiomesh_t;

	Public faceCount As Integer
	Public faceOffset As Integer
	Public skinref As Integer
	Public normalCount As Integer
	Public normalOffset As Integer


	Public theVertexAndNormalIndexes As List(Of SourceMdlTriangleVertex06)

End Class
