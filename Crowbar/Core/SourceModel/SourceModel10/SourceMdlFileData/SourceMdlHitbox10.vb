Public Class SourceMdlHitbox10

	Public Sub New()
		Me.boundingBoxMin = New SourceVector()
		Me.boundingBoxMax = New SourceVector()
	End Sub



	'// intersection boxes
	'typedef struct
	'{
	'	int					bone;
	'	int					group;			// intersection group
	'	vec3_t				bbmin;		// bounding box
	'	vec3_t				bbmax;		
	'} mstudiobbox_t;



	Public boneIndex As Integer
	Public groupIndex As Integer
	Public boundingBoxMin As SourceVector
	Public boundingBoxMax As SourceVector

End Class
