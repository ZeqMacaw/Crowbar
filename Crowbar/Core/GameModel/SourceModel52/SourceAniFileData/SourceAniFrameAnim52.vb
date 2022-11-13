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
	Public oldBoneFlags As Integer
	Public unkDataIndex As Integer
	Public unused As Integer


	'NOTE: These are indexed by global bone index.
	Public theBoneFlags As List(Of Byte)
	'Public theUnknownBytes01 As List(Of Byte)
	'Public theUnknownBytes02 As List(Of Byte)
	'Public theUnknownBytes03 As List(Of Byte)
	Public theUnknownBytes As List(Of Integer)
	Public theBoneConstantInfos As List(Of BoneConstantInfo49)
	'NOTE: This is indexed by frame index and global bone index.
	Public theBoneFrameDataInfos As List(Of List(Of BoneFrameDataInfo49))

	' In V52 they added scale tracks and redid how these work
	Public Const STUDIO_FRAME_RAWPOS As Integer = &H1
	Public Const STUDIO_FRAME_RAWROT As Integer = &H2
	Public Const STUDIO_FRAME_RAWSCALE As Integer = &H4
	Public Const STUDIO_FRAME_ANIMPOS As Integer = &H8
	Public Const STUDIO_FRAME_ANIMROT As Integer = &H10
	Public Const STUDIO_FRAME_ANIMSCALE As Integer = &H20

End Class
