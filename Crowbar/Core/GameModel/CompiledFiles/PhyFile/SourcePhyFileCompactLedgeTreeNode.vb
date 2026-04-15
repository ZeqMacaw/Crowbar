Public Class SourcePhyFileCompactLedgeTreeNode
    Public offsetRightNode As Integer
    Public offsetCompactLedge As Integer
    Public center As SourceVector
    Public radius As Single
    Public boxSizes(2) As Byte
    Public free As Byte

    Public theReadOffset As Long
    Public theCompactLedge As SourcePhyFileCompactLedge
    Public leftNode As SourcePhyFileCompactLedgeTreeNode
    Public rightNode As SourcePhyFileCompactLedgeTreeNode
End Class
