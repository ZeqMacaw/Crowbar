Public Class SourceMdlLocalHierarchy

    'FROM: se2007_src\src_main\public\studio.h
    'struct mstudiolocalhierarchy_t
    '{
    '	DECLARE_BYTESWAP_DATADESC();
    '	int			iBone;			// bone being adjusted
    '	int			iNewParent;		// the bones new parent

    '	float		start;			// beginning of influence
    '	float		peak;			// start of full influence
    '	float		tail;			// end of full influence
    '	float		end;			// end of all influence

    '	int			iStart;			// first frame 

    '	int			localanimindex;
    '	inline mstudiocompressedikerror_t *pLocalAnim() const { return (mstudiocompressedikerror_t *)(((byte *)this) + localanimindex); };

    '	int			unused[4];
    '};



    Public boneIndex As Integer
    Public boneNewParentIndex As Integer

    Public startInfluence As Single
    Public peakInfluence As Single
    Public tailInfluence As Single
    Public endInfluence As Single

    Public startFrameIndex As Integer

    Public localAnimOffset As Integer

    Public unused(3) As Integer


    Public theLocalAnims As List(Of SourceMdlCompressedIkError)

End Class
