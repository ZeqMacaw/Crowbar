Public Class SourceMdlBonePosition06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	int					frame;			// frame id (frame <= numframes)
	'	vec3_t				pos;
	'} mstudiobonepos_t;

	Public frameIndex As Integer
	Public position As New SourceVector()

End Class
