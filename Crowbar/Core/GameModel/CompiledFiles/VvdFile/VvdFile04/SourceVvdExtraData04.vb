Imports System.ComponentModel

Public Class SourceVvdExtraData04

	'FROM: [49] csgo_studiomdl\public\studio.h
	'struct ExtraVertexAttributesHeader_t
	'{
	'	int		m_count; // Number of individual extra attribute chunks
	'	int		m_totalbytes; // Total size of extra attribute data (all chunks plus header and index)
	'};
	'
	'struct ExtraVertexAttributeIndex_t
	'{
	'	ExtraVertexAttributeType_t	m_type;
	'	int							m_offset;
	'	int							m_bytes; //bytes per vertex
	'};

	Public type As ExtraVertexAttributeType
	Public offset As Int32
	Public bytesPerVertex As Int32



	Public theTextureCoordinates As List(Of SourceTextureCoordinates)



	'FROM: [49] csgo_studiomdl\public\studio.h
	'enum ExtraVertexAttributeType_t
	'{
	'	STUDIO_EXTRA_ATTRIBUTE_TEXCOORD0 = 0,
	'	STUDIO_EXTRA_ATTRIBUTE_TEXCOORD1,
	'	STUDIO_EXTRA_ATTRIBUTE_TEXCOORD2,
	'	STUDIO_EXTRA_ATTRIBUTE_TEXCOORD3,
	'	STUDIO_EXTRA_ATTRIBUTE_TEXCOORD4,
	'	STUDIO_EXTRA_ATTRIBUTE_TEXCOORD5,
	'	STUDIO_EXTRA_ATTRIBUTE_TEXCOORD6,
	'	STUDIO_EXTRA_ATTRIBUTE_TEXCOORD7
	'};

	Public Enum ExtraVertexAttributeType
		<Description("STUDIO_EXTRA_ATTRIBUTE_TEXCOORD0")> STUDIO_EXTRA_ATTRIBUTE_TEXCOORD0
		<Description("STUDIO_EXTRA_ATTRIBUTE_TEXCOORD1")> STUDIO_EXTRA_ATTRIBUTE_TEXCOORD1
		<Description("STUDIO_EXTRA_ATTRIBUTE_TEXCOORD2")> STUDIO_EXTRA_ATTRIBUTE_TEXCOORD2
		<Description("STUDIO_EXTRA_ATTRIBUTE_TEXCOORD3")> STUDIO_EXTRA_ATTRIBUTE_TEXCOORD3
		<Description("STUDIO_EXTRA_ATTRIBUTE_TEXCOORD4")> STUDIO_EXTRA_ATTRIBUTE_TEXCOORD4
		<Description("STUDIO_EXTRA_ATTRIBUTE_TEXCOORD5")> STUDIO_EXTRA_ATTRIBUTE_TEXCOORD5
		<Description("STUDIO_EXTRA_ATTRIBUTE_TEXCOORD6")> STUDIO_EXTRA_ATTRIBUTE_TEXCOORD6
		<Description("STUDIO_EXTRA_ATTRIBUTE_TEXCOORD7")> STUDIO_EXTRA_ATTRIBUTE_TEXCOORD7
	End Enum

End Class
