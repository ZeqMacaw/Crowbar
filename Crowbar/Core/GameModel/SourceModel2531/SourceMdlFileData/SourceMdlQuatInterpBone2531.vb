Public Class SourceMdlQuatInterpBone2531

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudioquatinterpbone_t
	'{
	'	int				control;// local transformation to check
	'	int				numtriggers;
	'	int				triggerindex;
	'	inline mstudioquatinterpinfo_t *pTrigger( int i ) const { return  (mstudioquatinterpinfo_t *)(((byte *)this) + triggerindex) + i; };
	'};

	Public controlBoneIndex As Integer
	Public triggerCount As Integer
	Public triggerOffset As Integer


	Public theTriggers As List(Of SourceMdlQuatInterpInfo2531)

End Class
