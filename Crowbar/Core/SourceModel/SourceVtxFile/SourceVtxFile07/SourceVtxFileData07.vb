Public Class SourceVtxFileData07
	Inherits SourceFileData




	'FROM: SourceEngine2007_source\src/public/optimize.h
	'struct FileHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	// file version as defined by OPTIMIZED_MODEL_FILE_VERSION
	'	int version;

	'	// hardware params that affect how the model is to be optimized.
	'	int vertCacheSize;
	'	unsigned short maxBonesPerStrip;
	'	unsigned short maxBonesPerTri;
	'	int maxBonesPerVert;

	'	// must match checkSum in the .mdl
	'	long checkSum;

	'	int numLODs; // garymcthack - this is also specified in ModelHeader_t and should match

	'	// one of these for each LOD
	'	int materialReplacementListOffset;
	'	MaterialReplacementListHeader_t *pMaterialReplacementList( int lodID ) const
	'	{ 
	'		MaterialReplacementListHeader_t *pDebug = 
	'			(MaterialReplacementListHeader_t *)(((byte *)this) + materialReplacementListOffset) + lodID;
	'		return pDebug;
	'	}

	'	int numBodyParts;
	'	int bodyPartOffset;
	'	inline BodyPartHeader_t *pBodyPart( int i ) const 
	'	{
	'		BodyPartHeader_t *pDebug = (BodyPartHeader_t *)(((byte *)this) + bodyPartOffset) + i;
	'		return pDebug;
	'	};	
	'};



	Public version As Integer

	Public vertexCacheSize As Integer
	Public maxBonesPerStrip As UShort
	Public maxBonesPerTri As UShort
	Public maxBonesPerVertex As Integer

	Public checksum As Integer

	Public lodCount As Integer

	Public materialReplacementListOffset As Integer

	Public bodyPartCount As Integer
	Public bodyPartOffset As Integer



	Public theVtxBodyParts As List(Of SourceVtxBodyPart07)
	Public theVtxMaterialReplacementLists As List(Of SourceVtxMaterialReplacementList07)

End Class
