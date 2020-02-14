Public Class SourceAniFrameAnim52

	'FROM: AlienSwarm_source\src\public\studio.h
	'struct mstudio_frame_anim_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();

	'	inline byte		*pBoneFlags( void ) const { return (((byte *)this) + sizeof( struct mstudio_frame_anim_t )); };

	'	int				constantsoffset;
	'	inline byte		*pConstantData( void ) const { return (((byte *)this) + constantsoffset); };

	'	int				frameoffset;
	'	int 			framelength;
	'	inline byte		*pFrameData( int iFrame  ) const { return (((byte *)this) + frameoffset + iFrame * framelength); };

	'	int				unused[3];
	'};


	Public constantsOffset As Integer
	Public frameOffset As Integer
	Public frameLength As Integer
	Public unused(2) As Integer


	'NOTE: These are indexed by global bone index.
	Public theBoneFlags As List(Of Byte)
	'Public theUnknownBytes01 As List(Of Byte)
	'Public theUnknownBytes02 As List(Of Byte)
	'Public theUnknownBytes03 As List(Of Byte)
	Public theUnknownBytes As List(Of Integer)
	Public theBoneConstantInfos As List(Of BoneConstantInfo49)
	'NOTE: This is indexed by frame index and global bone index.
	Public theBoneFrameDataInfos As List(Of List(Of BoneFrameDataInfo49))

	'FROM: AlienSwarm_source\src\public\studio.h
	' Values for the field, theBoneFlags:
	'#define STUDIO_FRAME_RAWPOS		0x01 // Vector48 in constants
	'#define STUDIO_FRAME_RAWROT		0x02 // Quaternion48 in constants
	'#define STUDIO_FRAME_ANIMPOS	0x04 // Vector48 in framedata
	'#define STUDIO_FRAME_ANIMROT	0x08 // Quaternion48 in framedata
	'#define STUDIO_FRAME_FULLANIMPOS	0x10 // Vector in framedata
	Public Const STUDIO_FRAME_RAWPOS As Integer = &H1
	Public Const STUDIO_FRAME_RAWROT As Integer = &H2
	Public Const STUDIO_FRAME_ANIMPOS As Integer = &H4
	Public Const STUDIO_FRAME_ANIMROT As Integer = &H8
	Public Const STUDIO_FRAME_FULLANIMPOS As Integer = &H10

	'Public Const STUDIO_FRAME_UNKNOWN01 As Integer = &H40	' Seems to be 6 rotation bytes in constants based on tests. New format that is not Quaternion48. Maybe Quaternion48Smallest3?
	'Public Const STUDIO_FRAME_UNKNOWN02 As Integer = &H80	' Seems to be 6 rotation bytes in framedata based on tests. New format that is not Quaternion48. Maybe Quaternion48Smallest3?
	'FROM: Kerry at Valve via Splinks on 24-Apr-2017
	'#define STUDIO_FRAME_CONST_ROT2   0x40 // Quaternion48S in constants
	'#define STUDIO_FRAME_ANIM_ROT2    0x80 // Quaternion48S in framedata
	Public Const STUDIO_FRAME_CONST_ROT2 As Integer = &H40
	Public Const STUDIO_FRAME_ANIM_ROT2 As Integer = &H80


End Class
