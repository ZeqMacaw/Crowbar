Public Class SourceMdlAnimationDesc2531
	Inherits SourceMdlAnimationDescBase

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudioanimdesc_t
	'{
	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'
	'	float				fps;		// frames per second	
	'	int					flags;		// looping/non-looping flags
	'
	'	int					numframes;
	'
	'	// piecewise movement
	'	int					nummovements;
	'	int					movementindex;
	'	inline mstudiomovement_t * const pMovement( int i ) const { return (mstudiomovement_t *)(((byte *)this) + movementindex) + i; };
	'
	'	Vector				bbmin;		// per animation bounding box
	'	Vector				bbmax;		
	'
	'	int	animindex;		// mstudioanim_t pointer relative to start of mstudioanimdesc_t data
	'						// [bone][X, Y, Z, XR, YR, ZR]
	'	inline mstudioanim_t		*pAnim( int i ) const { return  (mstudioanim_t *)(((byte *)this) + animindex) + i; };
	'
	'	int					numikrules;
	'	int					ikruleindex;
	'	inline mstudioikrule_t *pIKRule( int i ) const { return (mstudioikrule_t *)(((byte *)this) + ikruleindex) + i; };
	'
	'	int					hz[3];	// f64: ~unused[8]
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

	Public unused(2) As Integer


	Public theAnimations As List(Of SourceMdlAnimation2531)
	'Public theName As String

End Class
