Public Class SourcePhyFileCompactLedge
    Public pointOffset As Integer
    Public terminalData As Integer
    Public hasChildren As Boolean
    Public isCompact As Boolean
    Public dummy As Byte
    Public sizeDivided16 As UInteger
    Public triangleCount As Short
    Public forFutureUse As Short

    Public theTriangles As List(Of SourcePhyFileCompactTriangle)
End Class
