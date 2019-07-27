Public Class SourcePhyFileData
	Inherits SourceFileData


	'FROM: SourceEngine2007\src_main\public\phyfile.h
	'typedef struct phyheader_s
	'{
	'	int		size;
	'	int		id;
	'	int		solidCount;
	'	long	checkSum;	// checksum of source .mdl file
	'} phyheader_t;



	'	int		size;
	Public size As Integer
	'	int		id;
	Public id As Integer
	'	int		solidCount;
	Public solidCount As Integer
	'	long	checkSum;	// checksum of source .mdl file
	Public checksum As Integer



	Public thePhysicsMeshSmdFileName As String
	Public theSourcePhyKeyValueDataOffset As Long
	Public theSourcePhyCollisionDatas As List(Of SourcePhyCollisionData)
	Public theSourcePhyPhysCollisionModels As List(Of SourcePhyPhysCollisionModel)
	Public theSourcePhyRagdollConstraintDescs As SortedList(Of Integer, SourcePhyRagdollConstraint)
	Public theSourcePhyCollisionPairs As List(Of SourcePhyCollisionPair)
	Public theSourcePhySelfCollides As Boolean = True
	Public theSourcePhyEditParamsSection As SourcePhyEditParamsSection
	Public theSourcePhyPhysCollisionModelMostUsedValues As SourcePhyPhysCollisionModel
	Public theSourcePhyCollisionText As String
	Public theSourcePhyIsCollisionModel As Boolean = False
	Public theSourcePhyMaxConvexPieces As Integer

End Class
