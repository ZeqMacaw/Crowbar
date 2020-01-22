Public Class SourceMdlAnimationValuePointer

	'FROM: SourceEngine2006_source\public\studio.h
	'struct mstudioanim_valueptr_t
	'{
	'	short	offset[3];
	'	inline mstudioanimvalue_t *pAnimvalue( int i ) const { if (offset[i] > 0) return  (mstudioanimvalue_t *)(((byte *)this) + offset[i]); else return NULL; };
	'};


	Public animXValueOffset As Short
	Public animYValueOffset As Short
	Public animZValueOffset As Short

	Public theAnimXValues As List(Of SourceMdlAnimationValue)
	Public theAnimYValues As List(Of SourceMdlAnimationValue)
	Public theAnimZValues As List(Of SourceMdlAnimationValue)

End Class
