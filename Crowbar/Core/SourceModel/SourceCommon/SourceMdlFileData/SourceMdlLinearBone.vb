Public Class SourceMdlLinearBone

	Public Sub New()
		Me.theFlags = New List(Of Integer)
		Me.theParents = New List(Of Integer)
		Me.thePositions = New List(Of SourceVector)
		Me.theQuaternions = New List(Of SourceQuaternion)
		Me.theRotations = New List(Of SourceVector)
		Me.thePoseToBoneDataColumn0s = New List(Of SourceVector)
		Me.thePoseToBoneDataColumn1s = New List(Of SourceVector)
		Me.thePoseToBoneDataColumn2s = New List(Of SourceVector)
		Me.thePoseToBoneDataColumn3s = New List(Of SourceVector)
		Me.thePositionScales = New List(Of SourceVector)
		Me.theRotationScales = New List(Of SourceVector)
		Me.theQAlignments = New List(Of SourceQuaternion)
	End Sub

	'FROM: SourceEngine2007\src_main\public\studio.h
	'struct mstudiolinearbone_t	
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'
	'	int numbones;
	'
	'	int flagsindex;
	'	inline int flags( int i ) const { Assert( i >= 0 && i < numbones); return *((int *)(((byte *)this) + flagsindex) + i); };
	'	inline int *pflags( int i ) { Assert( i >= 0 && i < numbones); return ((int *)(((byte *)this) + flagsindex) + i); };
	'
	'	int	parentindex;
	'	inline int parent( int i ) const { Assert( i >= 0 && i < numbones); return *((int *)(((byte *)this) + parentindex) + i); };
	'
	'	int	posindex;
	'	inline Vector pos( int i ) const { Assert( i >= 0 && i < numbones); return *((Vector *)(((byte *)this) + posindex) + i); };
	'
	'	int quatindex;
	'	inline Quaternion quat( int i ) const { Assert( i >= 0 && i < numbones); return *((Quaternion *)(((byte *)this) + quatindex) + i); };
	'
	'	int rotindex;
	'	inline RadianEuler rot( int i ) const { Assert( i >= 0 && i < numbones); return *((RadianEuler *)(((byte *)this) + rotindex) + i); };
	'
	'	int posetoboneindex;
	'	inline matrix3x4_t poseToBone( int i ) const { Assert( i >= 0 && i < numbones); return *((matrix3x4_t *)(((byte *)this) + posetoboneindex) + i); };
	'
	'	int	posscaleindex;
	'	inline Vector posscale( int i ) const { Assert( i >= 0 && i < numbones); return *((Vector *)(((byte *)this) + posscaleindex) + i); };
	'
	'	int	rotscaleindex;
	'	inline Vector rotscale( int i ) const { Assert( i >= 0 && i < numbones); return *((Vector *)(((byte *)this) + rotscaleindex) + i); };
	'
	'	int	qalignmentindex;
	'	inline Quaternion qalignment( int i ) const { Assert( i >= 0 && i < numbones); return *((Quaternion *)(((byte *)this) + qalignmentindex) + i); };
	'
	'	int unused[6];
	'
	'	mstudiolinearbone_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudiolinearbone_t(const mstudiolinearbone_t& vOther);
	'};

	'	int numbones;
	Public boneCount As Integer

	'	int flagsindex;
	Public flagsOffset As Integer

	'	int	parentindex;
	Public parentOffset As Integer

	'	int	posindex;
	Public posOffset As Integer

	'	int quatindex;
	Public quatOffset As Integer

	'	int rotindex;
	Public rotOffset As Integer

	'	int posetoboneindex;
	Public poseToBoneOffset As Integer

	'	int	posscaleindex;
	Public posScaleOffset As Integer

	'	int	rotscaleindex;
	Public rotScaleOffset As Integer

	'	int	qalignmentindex;
	Public qAlignmentOffset As Integer

	'	int unused[6];
	Public unused(5) As Integer



	'FROM: SourceEngine2007\src_main\utils\studiomdl\write.cpp
	'      static void WriteBoneTransforms( studiohdr2_t *phdr, mstudiobone_t *pBone )
	'
	'#define WRITE_BONE_BLOCK( type, srcfield, dest, destindex ) \
	'		type *##dest = (type *)pData; \
	'		pLinearBone->##destindex = pData - (byte *)pLinearBone; \
	'		pData += g_numbones * sizeof( *##dest ); \
	'		ALIGN4( pData ); \
	'		for ( int i = 0; i < g_numbones; i++) \
	'			dest##[i] = pBone[i].##srcfield;

	'		WRITE_BONE_BLOCK( int, flags, pFlags, flagsindex );
	'		WRITE_BONE_BLOCK( int, parent, pParent, parentindex );
	'		WRITE_BONE_BLOCK( Vector, pos, pPos, posindex );
	'		WRITE_BONE_BLOCK( Quaternion, quat, pQuat, quatindex );
	'		WRITE_BONE_BLOCK( RadianEuler, rot, pRot, rotindex );
	'		WRITE_BONE_BLOCK( matrix3x4_t, poseToBone, pPoseToBone, posetoboneindex );
	'		WRITE_BONE_BLOCK( Vector, posscale, pPoseScale, posscaleindex );
	'		WRITE_BONE_BLOCK( Vector, rotscale, pRotScale, rotscaleindex );
	'		WRITE_BONE_BLOCK( Quaternion, qAlignment, pQAlignment, qalignmentindex );
	Public theFlags As List(Of Integer)
	Public theParents As List(Of Integer)
	Public thePositions As List(Of SourceVector)
	Public theQuaternions As List(Of SourceQuaternion)
	Public theRotations As List(Of SourceVector)
	Public thePoseToBoneDataColumn0s As List(Of SourceVector)
	Public thePoseToBoneDataColumn1s As List(Of SourceVector)
	Public thePoseToBoneDataColumn2s As List(Of SourceVector)
	Public thePoseToBoneDataColumn3s As List(Of SourceVector)
	Public thePositionScales As List(Of SourceVector)
	Public theRotationScales As List(Of SourceVector)
	Public theQAlignments As List(Of SourceQuaternion)

End Class
