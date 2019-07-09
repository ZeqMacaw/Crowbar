Public Class SourceMdlAttachment2531

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudioattachment_t
	'{
	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int					type;
	'	int					bone;
	'	matrix3x4_t			local; // attachment point
	'};
	'------
	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// attachment
	'typedef struct 
	'{
	'	char				name[32];
	'	int					type;
	'	int					bone;
	'	vec3_t				org;	// attachment point
	'	vec3_t				vectors[3];
	'} mstudioattachment_t;

	Public nameOffset As Integer
	Public type As Integer
	Public boneIndex As Integer

	'Public attachmentPointColumn0 As New SourceVector()
	'Public attachmentPointColumn1 As New SourceVector()
	'Public attachmentPointColumn2 As New SourceVector()
	'Public attachmentPointColumn3 As New SourceVector()
	'Public attachmentPoint As New SourceVector()
	'Public vector01 As New SourceVector()
	'Public vector02 As New SourceVector()
	'Public vector03 As New SourceVector()
	'------
	'      float cX = bytesToFloat(this.file, cOffset + 24);
	'      float cY = bytesToFloat(this.file, cOffset + 40);
	'      float cZ = bytesToFloat(this.file, cOffset + 56);
	'      float cXX = bytesToFloat(this.file, cOffset + 12);
	'      float cYX = bytesToFloat(this.file, cOffset + 28);
	'      float cZX = bytesToFloat(this.file, cOffset + 44);
	'      float cZY = bytesToFloat(this.file, cOffset + 48);
	'      float cZZ = bytesToFloat(this.file, cOffset + 52);
	Public cXX As Double '12
	Public unused01 As Double '16
	Public unused02 As Double '20
	Public posX As Double '24

	Public cYX As Double '28
	Public unused03 As Double '32
	Public unused04 As Double '36
	Public posY As Double '40

	Public cZX As Double '44
	Public cZY As Double '48
	Public cZZ As Double '52
	Public posZ As Double '56

	Public theName As String

End Class
