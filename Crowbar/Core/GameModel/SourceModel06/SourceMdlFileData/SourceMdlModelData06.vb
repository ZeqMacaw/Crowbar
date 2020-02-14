Public Class SourceMdlModelData06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	// TOMAS: UNDONE:
	'	int					unk01;
	'	int					unk02;
	'	int					unk03;
	'
	'	int					numverts;		// number of unique vertices
	'	int					vertindex;		// vertex vec3_t (data)
	'
	'	int					numnorms;		// number of unique surface normals
	'	int					normindex;		// normal vec3_t (data)
	'
	'} mstudiomodeldata_t;

	Public unknown01 As Integer
	Public unknown02 As Integer
	Public unknown03 As Integer

	Public vertexCount As Integer
	Public vertexOffset As Integer

	Public normalCount As Integer
	Public normalOffset As Integer


	Public theNormals As List(Of SourceVector)
	Public theVertexes As List(Of SourceVector)

End Class
