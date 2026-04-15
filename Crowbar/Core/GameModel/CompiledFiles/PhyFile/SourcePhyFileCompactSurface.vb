Public Class SourcePhyFileCompactSurface
    Public massCenter As SourceVector
    Public rotationInertia As SourceVector
    Public upperLimitRadius As Single
    Public maxFactorSurfaceDeviation As Byte
    Public byteSize As Integer
    Public offsetLedgetreeRoot As Integer
    Public dummy(2) As Integer

    Public theCompactSurfaceHeader As SourcePhyFileCompactSurfaceHeader
    Public theRootLedgeTree As SourcePhyFileCompactLedgeTreeNode
    Public thePoints As List(Of SourceVector)
End Class
