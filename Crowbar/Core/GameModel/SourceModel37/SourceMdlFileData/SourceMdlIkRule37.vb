Public Class SourceMdlIkRule37

	'struct mstudioikrule_t
	'{
	'	int		index;
	'
	'	int		type;
	'	int		chain;
	'
	'	int		bone;
	'
	'	int			slot;	// iktarget slot.  Usually same as chain.
	'	float		height;
	'	float		radius;
	'	float		floor;
	'	Vector		pos;
	'	Quaternion	q;
	'
	'	float		flWeight;
	'
	'	int		group; // match sub-sequence IK rules together
	'
	'	int		iStart;
	'	int		ikerrorindex;
	'	inline mstudioikerror_t *pError( int i ) const { return  (mstudioikerror_t *)(((byte *)this) + ikerrorindex) + (i - iStart); };
	'
	'	float		start;	// beginning of influence
	'	float		peak;	// start of full influence
	'	float		tail;	// end of full influence
	'	float		end;	// end of all influence
	'
	'	float		commit;		// unused: frame footstep target should be committed
	'	float		contact;	// unused: frame footstep makes ground concact
	'	float		pivot;		// unused: frame ankle can begin rotation from latched orientation
	'	float		release;	// unused: frame ankle should end rotation from latched orientation
	'};

	Public index As Integer
	Public type As Integer
	Public chain As Integer
	Public bone As Integer

	Public slot As Integer
	Public height As Double
	Public radius As Double
	Public floor As Double

	Public pos As SourceVector
	Public q As SourceQuaternion

	Public weight As Double
	Public group As Integer
	Public ikErrorIndexStart As Integer
	Public ikErrorOffset As Integer

	Public influenceStart As Double
	Public influencePeak As Double
	Public influenceTail As Double
	Public influenceEnd As Double

	Public commit As Double
	Public contact As Double
	Public pivot As Double
	Public release As Double

	Public theIkErrors As List(Of SourceMdlIkError37)

End Class
