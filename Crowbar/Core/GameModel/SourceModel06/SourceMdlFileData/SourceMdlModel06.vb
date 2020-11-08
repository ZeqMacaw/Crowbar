Public Class SourceMdlModel06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	char				name[64];
	'
	'	int					type;
	'
	'	int					unk01;			// TOMAS: (==1)
	'	int					unused01;		// TOMAS: UNUSED (checked)
	'
	'	int					nummesh;
	'	int					meshindex;
	'
	'	// vertex bone info
	'	int					numverts;		// number of unique vertices
	'	int					vertinfoindex;	// vertex bone info
	'
	'	// normal bone info
	'	int					numnorms;		// number of unique surface normals
	'	int					norminfoindex;	// normal bone info
	'
	'	// TOMAS: NEW IN MDL v6
	'	int					unused02;		// TOMAS: UNUSED (checked)
	'	int					modeldataindex;	// (->mstudiomodeldata_t)
	'} mstudiomodel_t;

	Public name(63) As Char
	Public type As Integer

	Public unknown01 As Integer
	Public unused01 As Integer

	Public meshCount As Integer
	Public meshOffset As Integer

	Public vertexCount As Integer
	Public vertexBoneInfoOffset As Integer

	Public normalCount As Integer
	Public normalBoneInfoOffset As Integer

	Public unused02 As Integer
	Public modelDataOffset As Integer


	Public theSmdFileName As String
	Public theName As String
	Public theMeshes As New List(Of SourceMdlMesh06)()
	Public theNormalBoneInfos As New List(Of Integer)()
	Public theVertexBoneInfos As New List(Of Integer)()
	Public theModelDatas As New List(Of SourceMdlModelData06)()

End Class
