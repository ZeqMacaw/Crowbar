Public Class SourceMdlAnimationDesc36
	Inherits SourceMdlAnimationDescBase

	'struct mstudioanimdesc_t
	'{
	'	int	sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'
	'	float fps;		// frames per second	
	'	int	flags;		// looping/non-looping flags
	'
	'	int	numframes;
	'
	'	// piecewise movement
	'	int	nummovements;
	'	int	movementindex;
	'	inline mstudiomovement_t * const pMovement( int i ) const { return (mstudiomovement_t *)(((byte *)this) + movementindex) + i; };
	'
	'	Vector	bbmin;		// per animation bounding box
	'	Vector	bbmax;		
	'
	'	int	animindex;	// mstudioanim_t pointer relative to start of mstudioanimdesc_t data
	'					// [bone][X, Y, Z, XR, YR, ZR]
	'	inline mstudioanim_t		*pAnim( int i ) const { return  (mstudioanim_t *)(((byte *)this) + animindex) + i; };
	'
	'	int	numikrules;
	'	int	ikruleindex;
	'	inline mstudioikrule_t *pIKRule( int i ) const { return (mstudioikrule_t *)(((byte *)this) + ikruleindex) + i; };
	'
	'	int	unused[8];	// remove as appropriate
	'};

	Public nameOffset As Integer
	Public fps As Double
	Public flags As Integer
	Public frameCount As Integer
	Public movementCount As Integer
	Public movementOffset As Integer

	Public bbMin As New SourceVector()
	Public bbMax As New SourceVector()

	Public animOffset As Integer

	Public ikRuleCount As Integer
	Public ikRuleOffset As Integer

	Public unused(7) As Integer

	Public theAnimations As List(Of SourceMdlAnimation37)
	Public theIkRules As List(Of SourceMdlIkRule37)
	Public theMovements As List(Of SourceMdlMovement)
	'Public theName As String

	Public theAnimIsLinkedToSequence As Boolean = False
	Public theLinkedSequences As New List(Of SourceMdlSequenceDesc36)()

End Class
