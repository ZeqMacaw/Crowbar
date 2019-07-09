Public Class SourcePhyCollisionData

	'FROM: SourceEngine2006_source\utils\studiomdl\collisionmodel.cpp
	'Class CPhysCollisionModel
	'{
	'public:
	'   [...]
	'	CPhysCollide	*m_pCollisionData;
	'	CPhysCollisionModel	*m_pNext;
	'};
	'CPhysCollisionModel *pPhys = g_JointedModel.m_pCollisionList;

	'pPhys = g_JointedModel.m_pCollisionList;
	'while ( pPhys )
	'{
	'	int size = physcollision->CollideSize( pPhys->m_pCollisionData );
	'	fwrite( &size, sizeof(int), 1, fp );
	'	char *buf = (char *)stackalloc( size );
	'	physcollision->CollideWrite( buf, pPhys->m_pCollisionData );
	'	fwrite( buf, size, 1, fp );
	'	pPhys = pPhys->m_pNext;
	'}

	'	int		size;
	Public size As Integer

	'Public theBoneIndex As Integer
	'Public theFaces As List(Of SourcePhyFace)
	Public theFaceSections As List(Of SourcePhyFaceSection)
	'Public theVertices As List(Of SourceVector)
	Public theVertices As List(Of SourcePhyVertex)

End Class
