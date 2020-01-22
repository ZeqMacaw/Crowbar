Public Class SourceMdlBoneRotation06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	short 				frame;			// frame id (frame <= numframes)
	'	short 				angle[3];		// (values: +/-18000; 18000 = 180deg)
	'} mstudiobonerot_t;

	Public frameIndex As Short
	Public angle(2) As Short

End Class
