Public Class SourceMdlQuatInterpBone

	'FROM: SourceEngineXXXX_source\public\studio.h

	'struct mstudioquatinterpbone_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int				control;// local transformation to check
	'	int				numtriggers;
	'	int				triggerindex;
	'	inline mstudioquatinterpinfo_t *pTrigger( int i ) const { return  (mstudioquatinterpinfo_t *)(((byte *)this) + triggerindex) + i; };
	'
	'	mstudioquatinterpbone_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudioquatinterpbone_t(const mstudioquatinterpbone_t& vOther);
	'};



	Public controlBoneIndex As Integer
	Public triggerCount As Integer
	Public triggerOffset As Integer

	Public theTriggers As List(Of SourceMdlQuatInterpBoneInfo)

End Class
