Public Class SourceMdlAnimation06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	int					numpos;			// count of mstudiobnonepos_t
	'	int					posindex;		// (->mstudiobnonepos_t)
	'	int					numrot;			// count of mstudiobonerot_t
	'	int					rotindex;		// (->mstudiobonerot_t)
	'} mstudioanim_t;

	Public bonePositionCount As Integer
	Public bonePositionOffset As Integer
	Public boneRotationCount As Integer
	Public boneRotationOffset As Integer


	Public theRawBonePositions As List(Of SourceMdlBonePosition06)
	Public theRawBoneRotations As List(Of SourceMdlBoneRotation06)

	Public theBonePositionsAndRotations As List(Of SourceBonePostionAndRotation06)

End Class
