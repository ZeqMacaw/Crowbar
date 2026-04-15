Public Class SourcePhyFileData
	Inherits SourceFileData

	Public size As Integer
	Public id As Integer
	Public solidCount As Integer
	Public checksum As Integer

	Public theSolids As List(Of SourcePhyFileCompactSurface)
	Public theCollisions As List(Of SourcePhyFileCollision)
	Public theConstraints As List(Of SourcePhyFileConstraints)
	Public theSelfCollisions As Boolean
	Public theCollisionRules As List(Of SourcePhyFileCollisionRules)
	Public theAnimatedFriction As SourcePhyFileAnimatedFriction
	Public theEditParameters As SourcePhyFileEditParameters
	Public theCollisionText As String

	Public thePhysicsFileName As String
End Class
