Imports System.Runtime.InteropServices

Public Class SourceMdlAnimation

	'FROM: SourceEngineXXXX_source\public\studio.h
	'// per bone per animation DOF and weight pointers
	'struct mstudioanim_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	byte				bone;
	'	byte				flags;		// weighing options

	'	// valid for animating data only
	'	inline byte				*pData( void ) const { return (((byte *)this) + sizeof( struct mstudioanim_t )); };
	'	inline mstudioanim_valueptr_t	*pRotV( void ) const { return (mstudioanim_valueptr_t *)(pData()); };
	'	inline mstudioanim_valueptr_t	*pPosV( void ) const { return (mstudioanim_valueptr_t *)(pData()) + ((flags & STUDIO_ANIM_ANIMROT) != 0); };

	'	// valid if animation unvaring over timeline
	'	inline Quaternion48		*pQuat48( void ) const { return (Quaternion48 *)(pData()); };
	'	inline Quaternion64		*pQuat64( void ) const { return (Quaternion64 *)(pData()); };
	'	inline Vector48			*pPos( void ) const { return (Vector48 *)(pData() + ((flags & STUDIO_ANIM_RAWROT) != 0) * sizeof( *pQuat48() ) + ((flags & STUDIO_ANIM_RAWROT2) != 0) * sizeof( *pQuat64() ) ); };

	'	short				nextoffset;
	'	inline mstudioanim_t	*pNext( void ) const { if (nextoffset != 0) return  (mstudioanim_t *)(((byte *)this) + nextoffset); else return NULL; };
	'};


	'	byte				bone;
	Public boneIndex As Byte
	'	byte				flags;		// weighing options
	Public flags As Byte
	'	short				nextoffset;
	Public nextSourceMdlAnimationOffset As Short

	'MDL 53 stuff
	Public positionScale As Double
	Public unk As Short
	'Public OffsetX As Short
	'Public OffsetY As Short
	'Public OffsetZ As Short
	'Public OffsetL As Short
	Public PosX As SourceFloat16bits
	Public PosY As SourceFloat16bits
	Public PosZ As SourceFloat16bits
	Public ScaleX As SourceFloat16bits
	Public ScaleY As SourceFloat16bits
	Public ScaleZ As SourceFloat16bits
	Public nextTitanfall2MdlAnimationOffset As Integer


	' Values for the field, flags:
	'#define STUDIO_ANIM_RAWPOS	0x01 // Vector48
	'#define STUDIO_ANIM_RAWROT	0x02 // Quaternion48
	'#define STUDIO_ANIM_ANIMPOS	0x04 // mstudioanim_valueptr_t
	'#define STUDIO_ANIM_ANIMROT	0x08 // mstudioanim_valueptr_t
	'#define STUDIO_ANIM_DELTA	0x10
	'#define STUDIO_ANIM_RAWROT2	0x20 // Quaternion64
	Public Const STUDIO_ANIM_RAWPOS As Integer = &H1
	Public Const STUDIO_ANIM_RAWROT As Integer = &H2
	Public Const STUDIO_ANIM_ANIMPOS As Integer = &H4
	Public Const STUDIO_ANIM_ANIMROT As Integer = &H8
	Public Const STUDIO_ANIM_DELTA As Integer = &H10
	Public Const STUDIO_ANIM_RAWROT2 As Integer = &H20

	' MDL 53 values for flags:
	'#define STUDIO_ANIM_DELTA_53    0x01 // this appears to be delta until proven otherwise
	'// These work as toggles, flag enabled is raw data, flag disabled is pointers, see 'STUDIO_ANIM_READBONE_53' for exception.
	'#define STUDIO_ANIM_RAWPOS_53	0x02 // Vector48
	'#define STUDIO_ANIM_RAWROT_53	0x04 // Quaternion48
	'#define STUDIO_ANIM_RAWSCALE_53	0x08 // Vector48
	'// if above flag is disabled and below is enabled there is special exceptions
	'#define STUDIO_ANIM_READBONE_53 0x10 // read bone data if any Of the above are disabled, only observed For rotation
	Public Const STUDIO_ANIM_DELTA_53 As Integer = &H1
	Public Const STUDIO_ANIM_RAWPOS_53 As Integer = &H2
	Public Const STUDIO_ANIM_RAWROT_53 As Integer = &H4
	Public Const STUDIO_ANIM_RAWSCALE_53 As Integer = &H8
	Public Const STUDIO_ANIM_UNKFLAG_53 As Integer = &H10


	' Do not use union, because it will have to rely on size of a .NET Framework data type.
	'<StructLayout(LayoutKind.Explicit)> _
	'Public Structure theData
	'	<FieldOffset(0)> Public theRotV As SourceMdlAnimationValuePointer
	'	<FieldOffset(0)> Public thePosV As SourceMdlAnimationValuePointer
	'	<FieldOffset(0)> Public theQuat48 As SourceQuaternion48
	'	<FieldOffset(0)> Public theQuat64 As SourceQuaternion64
	'	<FieldOffset(8)> Public thePosV2 As SourceMdlAnimationValuePointer
	'End Structure


	' if (flags & STUDIO_ANIM_ANIMROT) <> 0 then this field is filled
	Public theRotV As SourceMdlAnimationValuePointer
	' if (flags & STUDIO_ANIM_ANIMPOS) <> 0 then this field is filled
	Public thePosV As SourceMdlAnimationValuePointer
	' if (flags & STUDIO_ANIM_RAWROT) <> 0 then this field is filled
	Public theRot48bits As SourceQuaternion48bits
	' if (flags & STUDIO_ANIM_RAWROT2) <> 0 then this field is filled
	Public theRot64bits As SourceQuaternion64bits
	' if (flags & STUDIO_ANIM_RAWPOS) <> 0 then this field is filled
	Public thePos As SourceVector48bits

End Class
