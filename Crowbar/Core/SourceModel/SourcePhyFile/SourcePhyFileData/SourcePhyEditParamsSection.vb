Public Class SourcePhyEditParamsSection

	'fprintf( fp, "editparams {\n" );
	'KeyWriteString( fp, "rootname", g_JointedModel.m_rootName );
	'KeyWriteFloat( fp, "totalmass", g_JointedModel.m_totalMass );

	' Example: 
	'editparams {
	'"rootname" "valvebiped.bip01_pelvis"
	'"totalmass" "100.000000"
	'}

	' Example from HL2 beta leak alyx.phy:
	'editparams {
	'"rootname" "valvebiped.bip01_pelvis"
	'"totalmass" "60.000000"
	'"jointmerge" "valvebiped.bip01_pelvis,valvebiped.bip01"
	'"jointmerge" "valvebiped.bip01_pelvis,valvebiped.bip01_spine1"
	'}

	Public concave As String
	Public jointMergeMap As Dictionary(Of String, List(Of String))
	Public rootName As String
	Public totalMass As Single

End Class
