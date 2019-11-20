Imports System.Runtime.InteropServices

Public Class SourceMdlFlexOp

	'FROM: SourceEngine2006+_source\public\studio.h
	'struct mstudioflexop_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int		op;
	'	union 
	'	{
	'		int		index;
	'		float	value;
	'	} d;
	'};

	'FROM: SourceEngine2006+_source\public\studio.h
	'#define STUDIO_CONST	1	// get float
	'#define STUDIO_FETCH1	2	// get Flexcontroller value
	'#define STUDIO_FETCH2	3	// get flex weight
	'#define STUDIO_ADD		4
	'#define STUDIO_SUB		5
	'#define STUDIO_MUL		6
	'#define STUDIO_DIV		7
	'#define STUDIO_NEG		8	// not implemented
	'#define STUDIO_EXP		9	// not implemented
	'#define STUDIO_OPEN	10	// only used in token parsing
	'#define STUDIO_CLOSE	11
	'#define STUDIO_COMMA	12	// only used in token parsing
	'#define STUDIO_MAX		13
	'#define STUDIO_MIN		14
	'#define STUDIO_2WAY_0	15	// Fetch a value from a 2 Way slider for the 1st value RemapVal( 0.0, 0.5, 0.0, 1.0 )
	'#define STUDIO_2WAY_1	16	// Fetch a value from a 2 Way slider for the 2nd value RemapVal( 0.5, 1.0, 0.0, 1.0 )
	'#define STUDIO_NWAY	17	// Fetch a value from a 2 Way slider for the 2nd value RemapVal( 0.5, 1.0, 0.0, 1.0 )
	'#define STUDIO_COMBO	18	// Perform a combo operation (essentially multiply the last N values on the stack)
	'#define STUDIO_DOMINATE	19	// Performs a combination domination operation
	'#define STUDIO_DME_LOWER_EYELID 20	// 
	'#define STUDIO_DME_UPPER_EYELID 21	// 
	Public Const STUDIO_CONST As Integer = 1
	Public Const STUDIO_FETCH1 As Integer = 2
	Public Const STUDIO_FETCH2 As Integer = 3
	Public Const STUDIO_ADD As Integer = 4
	Public Const STUDIO_SUB As Integer = 5
	Public Const STUDIO_MUL As Integer = 6
	Public Const STUDIO_DIV As Integer = 7
	Public Const STUDIO_NEG As Integer = 8
	Public Const STUDIO_EXP As Integer = 9
	Public Const STUDIO_OPEN As Integer = 10
	Public Const STUDIO_CLOSE As Integer = 11
	Public Const STUDIO_COMMA As Integer = 12
	Public Const STUDIO_MAX As Integer = 13
	Public Const STUDIO_MIN As Integer = 14
	Public Const STUDIO_2WAY_0 As Integer = 15
	Public Const STUDIO_2WAY_1 As Integer = 16
	Public Const STUDIO_NWAY As Integer = 17
	Public Const STUDIO_COMBO As Integer = 18
	Public Const STUDIO_DOMINATE As Integer = 19
	Public Const STUDIO_DME_LOWER_EYELID As Integer = 20
	Public Const STUDIO_DME_UPPER_EYELID As Integer = 21


	Public op As Integer

	Public index As Integer
	'------
	Public value As Double

End Class
