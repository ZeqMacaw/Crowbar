Public Class SourcePhyCollisionPair

	'if ( g_JointedModel.m_noSelfCollisions )
	'{
	'	fprintf(fp, "collisionrules {\n" );
	'	KeyWriteInt( fp, "selfcollisions", 0 );
	'	fprintf(fp, "}\n");
	'}
	'else if ( g_JointedModel.m_pCollisionPairs )
	'{
	'	fprintf(fp, "collisionrules {\n" );
	'	collisionpair_t *pPair = g_JointedModel.m_pCollisionPairs;
	'	while ( pPair )
	'	{
	'		pPair->obj0 = g_JointedModel.CollisionIndex( pPair->pName0 );
	'		pPair->obj1 = g_JointedModel.CollisionIndex( pPair->pName1 );
	'		if ( pPair->obj0 >= 0 && pPair->obj1 >= 0 && pPair->obj0 != pPair->obj1 )
	'		{
	'			KeyWriteIntPair( fp, "collisionpair", pPair->obj0, pPair->obj1 );
	'		}
	'		else
	'		{
	'			MdlWarning("Invalid collision pair (%s, %s)\n", pPair->pName0, pPair->pName1 );
	'		}
	'		pPair = pPair->pNext;
	'	}
	'	fprintf(fp, "}\n");
	'}

	'struct collisionpair_t
	'{
	'	int obj0;
	'	int obj1;
	'	const char *pName0;
	'	const char *pName1;
	'	collisionpair_t *pNext;
	'};

	' Example: 
	'collisionrules {
	'"collisionpair" "13,16"
	'"collisionpair" "12,16"
	'"collisionpair" "15,13"
	'"collisionpair" "15,12"
	'"collisionpair" "14,11"
	'"collisionpair" "2,10"
	'"collisionpair" "2,7"
	'"collisionpair" "1,10"
	'"collisionpair" "1,7"
	'"collisionpair" "2,5"
	'"collisionpair" "2,8"
	'"collisionpair" "9,17"
	'"collisionpair" "9,1"
	'"collisionpair" "6,17"
	'"collisionpair" "6,1"
	'"collisionpair" "6,9"
	'"collisionpair" "6,8"
	'"collisionpair" "10,11"
	'"collisionpair" "10,14"
	'"collisionpair" "10,0"
	'"collisionpair" "7,11"
	'"collisionpair" "7,14"
	'"collisionpair" "7,0"
	'"collisionpair" "17,10"
	'"collisionpair" "17,7"
	'"collisionpair" "17,3"
	'"collisionpair" "17,4"
	'}


	'	int obj0;
	Public obj0 As Integer
	'	int obj1;
	Public obj1 As Integer
	'	const char *pName0;
	Public name0 As String
	'	const char *pName1;
	Public name1 As String

End Class
