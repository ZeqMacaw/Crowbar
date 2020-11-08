Public Class SourceMdlIkRule

	'FROM: SourceEngine2006+_source\public\studio.h
	'struct mstudioikrule_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int			index;

	'	int			type;
	'	int			chain;

	'	int			bone;

	'	int			slot;	// iktarget slot.  Usually same as chain.
	'	float		height;
	'	float		radius;
	'	float		floor;
	'	Vector		pos;
	'	Quaternion	q;

	'	int			compressedikerrorindex;
	'	inline mstudiocompressedikerror_t *pCompressedError() const { return (mstudiocompressedikerror_t *)(((byte *)this) + compressedikerrorindex); };
	'	int			unused2;

	'	int			iStart;
	'	int			ikerrorindex;
	'	inline mstudioikerror_t *pError( int i ) const { return  (ikerrorindex) ? (mstudioikerror_t *)(((byte *)this) + ikerrorindex) + (i - iStart) : NULL; };

	'	float		start;	// beginning of influence
	'	float		peak;	// start of full influence
	'	float		tail;	// end of full influence
	'	float		end;	// end of all influence

	'	float		unused3;	// 
	'	float		contact;	// frame footstep makes ground concact
	'	float		drop;		// how far down the foot should drop when reaching for IK
	'	float		top;		// top of the foot box

	'	int			unused6;
	'	int			unused7;
	'	int			unused8;

	'	int			szattachmentindex;		// name of world attachment
	'	inline char * const pszAttachment( void ) const { return ((char *)this) + szattachmentindex; }

	'	int			unused[7];

	'	mstudioikrule_t() {}

	'private:
	'	// No copy constructors allowed
	'	mstudioikrule_t(const mstudioikrule_t& vOther);
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

	Public compressedIkErrorOffset As Integer
	Public unused2 As Integer
	Public ikErrorIndexStart As Integer
	Public ikErrorOffset As Integer

	Public influenceStart As Double
	Public influencePeak As Double
	Public influenceTail As Double
	Public influenceEnd As Double

	Public unused3 As Double
	Public contact As Double
	Public drop As Double
	Public top As Double

	Public unused6 As Integer
	Public unused7 As Integer
	Public unused8 As Integer

	Public attachmentNameOffset As Integer

	Public unused(6) As Integer



	Public theAttachmentName As String
	Public theCompressedIkError As SourceMdlCompressedIkError



	' For the 'type' field:
	'FROM: se2007_src\src_main\public\studio.h
	'#define IK_SELF 1
	'#define IK_WORLD 2
	'#define IK_GROUND 3
	'#define IK_RELEASE 4
	'#define IK_ATTACHMENT 5
	'#define IK_UNLATCH 6
	Public Const IK_SELF As Integer = 1
	Public Const IK_WORLD As Integer = 2
	Public Const IK_GROUND As Integer = 3
	Public Const IK_RELEASE As Integer = 4
	Public Const IK_ATTACHMENT As Integer = 5
	Public Const IK_UNLATCH As Integer = 6

End Class
