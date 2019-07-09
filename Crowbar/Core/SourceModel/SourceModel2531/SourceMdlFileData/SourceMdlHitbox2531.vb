Public Class SourceMdlHitbox2531

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudiobbox_t
	'{
	'	int					bone;
	'	int					group;				// intersection group
	'	Vector				bbmin;				// bounding box
	'	Vector				bbmax;	
	'// f64: -
	'// 	int					szhitboxnameindex;	// offset to the name of the hitbox.
	'//	char				padding[32];		// future expansion.
	'
	'//	char* pszHitboxName(void* pHeader)
	'//	{
	'//		if( szhitboxnameindex == 0 )
	'//			return "";
	'
	'//// NJS: Just a cosmetic change, next time the model format is rebuilt, please use the NEXT_MODEL_FORMAT_REVISION.
	'//// also, do a grep to find the corresponding #ifdefs.
	'//#ifdef NEXT_MODEL_FORMAT_REVISION
	'//		return ((char*)this) + szhitboxnameindex;
	'//#else
	'//		return ((char*)pHeader) + szhitboxnameindex;
	'//#endif
	'//	}
	'};

	Public boneIndex As Integer
	Public groupIndex As Integer
	Public boundingBoxMin As New SourceVector()
	Public boundingBoxMax As New SourceVector()

End Class
