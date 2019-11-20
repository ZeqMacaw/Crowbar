Public Class FlexFrame37

	Public flexName As String
	Public flexDescription As String
	'NOTE: Confirmed that MDL v37 does not have "flexpair" option. 
	'      No "flexpair" found in MDL v37 studiomdl.exe but was found in MDL v44 (Half-Life 2) studiomdl.exe.
	'      Also, SourceMdlFlex37 does not have the "flexDescPartnerIndex" field.
	'Public flexHasPartner As Boolean
	Public flexSplit As Double
	Public bodyAndMeshVertexIndexStarts As List(Of Integer)
	Public flexes As List(Of SourceMdlFlex37)
	Public meshIndexes As List(Of Integer)

End Class
