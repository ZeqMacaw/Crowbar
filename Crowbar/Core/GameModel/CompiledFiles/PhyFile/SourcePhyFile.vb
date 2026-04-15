Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic.Logging

Public Class SourcePhyFile

#Region "Creation and Destruction"

    Public Sub New(ByVal phyFileReader As BinaryReader, ByVal phyFileData As SourcePhyFileData, Optional ByVal endOffset As Long = 0)
        Me.theInputFileReader = phyFileReader
        Me.thePhyFileData = phyFileData
        Me.thePhyEndOffset = endOffset

        Me.thePhyFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length
    End Sub

#End Region

#Region "Methods"

    Public Sub ReadSourcePhyHeader()
        Dim fileOffsetStart As Long = Me.theInputFileReader.BaseStream.Position

        Me.thePhyFileData.size = Me.theInputFileReader.ReadInt32()
        Me.thePhyFileData.id = Me.theInputFileReader.ReadInt32()
        Me.thePhyFileData.solidCount = Me.theInputFileReader.ReadInt32()
        Me.thePhyFileData.checksum = Me.theInputFileReader.ReadInt32()

        'NOTE: If header size ever increases, this will at least skip over extra stuff.
        Me.theInputFileReader.BaseStream.Seek(fileOffsetStart + Me.thePhyFileData.size, SeekOrigin.Begin)

        Me.thePhyFileData.theFileSeekLog.Add(fileOffsetStart, Me.theInputFileReader.BaseStream.Position - 1, "Header")
    End Sub

    Public Sub ReadSourceCollisionData()
        Me.thePhyFileData.theSolids = New List(Of SourcePhyFileCompactSurface)
        For soildIndex As Integer = 0 To Me.thePhyFileData.solidCount - 1
            Dim compactSurface As New SourcePhyFileCompactSurface

            Dim compactSurfaceHeaderOffsetStart As Long = Me.theInputFileReader.BaseStream.Position
            Dim solidSize As Integer = Me.theInputFileReader.ReadInt32()
            Dim physicsId As Char() = Me.theInputFileReader.ReadChars(4)
            If physicsId = "VPHY" Then
                compactSurface.theCompactSurfaceHeader = New SourcePhyFileCompactSurfaceHeader
                With compactSurface.theCompactSurfaceHeader
                    .version = Me.theInputFileReader.ReadInt16()
                    .modelType = Me.theInputFileReader.ReadInt16()
                    .surfaceSize = Me.theInputFileReader.ReadInt32()
                    .dragAxisAreas = New SourceVector(Me.theInputFileReader.ReadSingle(), Me.theInputFileReader.ReadSingle(), Me.theInputFileReader.ReadSingle())
                    .axisMapSize = Me.theInputFileReader.ReadInt32()
                End With
            Else
                Me.theInputFileReader.BaseStream.Seek(-4, SeekOrigin.Current)
            End If
            Me.thePhyFileData.theFileSeekLog.Add(compactSurfaceHeaderOffsetStart, Me.theInputFileReader.BaseStream.Position - 1, "CompactSurfaceHeader")

            Dim compactSurfaceOffsetStart As Long = Me.ReadCompactSurface(compactSurface)
            Me.theInputFileReader.BaseStream.Seek(compactSurfaceOffsetStart + compactSurface.offsetLedgetreeRoot, SeekOrigin.Begin)

            compactSurface.theRootLedgeTree = Me.ReadCompactLedgeTreeNode()

            Dim readCompactLedgeTreeStack As New Stack(Of SourcePhyFileCompactLedgeTreeNode)
            readCompactLedgeTreeStack.Push(compactSurface.theRootLedgeTree)
            Dim vertexCount As UShort = 0
            Dim vertexOffset As Long = 0

            While readCompactLedgeTreeStack.Count > 0
                Dim topCompactLedgeTreeNode As SourcePhyFileCompactLedgeTreeNode = readCompactLedgeTreeStack.Pop()

                If topCompactLedgeTreeNode.offsetCompactLedge <> 0 Then
                    Me.theInputFileReader.BaseStream.Seek(topCompactLedgeTreeNode.theReadOffset + topCompactLedgeTreeNode.offsetCompactLedge, SeekOrigin.Begin)
                    topCompactLedgeTreeNode.theCompactLedge = Me.ReadCompactLedge(vertexCount, vertexOffset)
                End If

                If topCompactLedgeTreeNode.offsetRightNode <> 0 Then
                    Const NodeByteSize As Long = 28
                    Me.theInputFileReader.BaseStream.Seek(topCompactLedgeTreeNode.theReadOffset + NodeByteSize, SeekOrigin.Begin)
                    topCompactLedgeTreeNode.leftNode = Me.ReadCompactLedgeTreeNode()
                    Me.theInputFileReader.BaseStream.Seek(topCompactLedgeTreeNode.theReadOffset + topCompactLedgeTreeNode.offsetRightNode, SeekOrigin.Begin)
                    topCompactLedgeTreeNode.rightNode = Me.ReadCompactLedgeTreeNode()
                    readCompactLedgeTreeStack.Push(topCompactLedgeTreeNode.rightNode)
                    readCompactLedgeTreeStack.Push(topCompactLedgeTreeNode.leftNode)
                End If
            End While


            Me.theInputFileReader.BaseStream.Seek(vertexOffset, SeekOrigin.Begin)
            compactSurface.thePoints = New List(Of SourceVector)
            For vertexIndex As UShort = 0 To vertexCount
                compactSurface.thePoints.Add(New SourceVector(Me.theInputFileReader.ReadSingle(), Me.theInputFileReader.ReadSingle(), Me.theInputFileReader.ReadSingle()))
                Me.theInputFileReader.ReadSingle()
            Next

            Me.thePhyFileData.theFileSeekLog.Add(vertexOffset, Me.theInputFileReader.BaseStream.Position - 1, "CompactPoints")

            Me.thePhyFileData.theSolids.Add(compactSurface)
            Me.theInputFileReader.BaseStream.Seek(compactSurfaceHeaderOffsetStart + solidSize + 4, SeekOrigin.Begin)
        Next
    End Sub

    Public Sub ReadSourceCollisionText()
        Dim collisionTextOffsetStart As Long = Me.theInputFileReader.BaseStream.Position

        Me.thePhyFileData.theCollisions = New List(Of SourcePhyFileCollision)
        Me.thePhyFileData.theConstraints = New List(Of SourcePhyFileConstraints)
        Me.thePhyFileData.theSelfCollisions = True
        Me.thePhyFileData.theCollisionRules = New List(Of SourcePhyFileCollisionRules)
        Me.thePhyFileData.theCollisionText = ""

        Dim textClass As String = FileManager.ReadTextLine(Me.theInputFileReader)

        Do While textClass IsNot Nothing
            If textClass = "solid {" Then
                Me.ReadCollision()
            ElseIf textClass = "ragdollconstraint {" Then
                Me.ReadConstraints()
            ElseIf textClass = "collisionrules {" Then
                Me.ReadCollisionRules()
            ElseIf textClass = "animatedfriction {" Then
                Me.ReadAnimatedFriction()
            ElseIf textClass = "editparams {" Then
                Me.ReadEditParameters()
            Else
                Dim endOffset As Long
                If Me.thePhyEndOffset = 0 Then
                    endOffset = Me.theInputFileReader.BaseStream.Length() - 1
                Else
                    endOffset = Me.thePhyEndOffset
                End If
                Me.thePhyFileData.theCollisionText = textClass + vbLf + ReadPhyCollisionTextSection(Me.theInputFileReader, endOffset)
            End If
            textClass = FileManager.ReadTextLine(Me.theInputFileReader)
        Loop


        Me.thePhyFileData.theFileSeekLog.Add(collisionTextOffsetStart, Me.theInputFileReader.BaseStream.Position - 1, "CollisionText")


    End Sub

    Public Sub ReadUnreadBytes()
        Me.thePhyFileData.theFileSeekLog.LogUnreadBytes(Me.theInputFileReader)
    End Sub

#End Region

#Region "Private Methods"

    Private Function ReadCompactSurface(ByRef compactSurface As SourcePhyFileCompactSurface) As Long
        Dim compactSurfaceOffsetStart As Long = Me.theInputFileReader.BaseStream.Position

        With compactSurface
            .massCenter = New SourceVector(Me.theInputFileReader.ReadSingle(), Me.theInputFileReader.ReadSingle(), Me.theInputFileReader.ReadSingle())
            .rotationInertia = New SourceVector(Me.theInputFileReader.ReadSingle(), Me.theInputFileReader.ReadSingle(), Me.theInputFileReader.ReadSingle())
            .upperLimitRadius = Me.theInputFileReader.ReadSingle()
        End With

        Dim bitField As UInteger = Me.theInputFileReader.ReadUInt32()
        With compactSurface
            .maxFactorSurfaceDeviation = CByte((bitField And Byte.MaxValue))
            .byteSize = CInt(bitField >> 8)
            .offsetLedgetreeRoot = Me.theInputFileReader.ReadInt32()
            .dummy = {Me.theInputFileReader.ReadInt32(), Me.theInputFileReader.ReadInt32(), Me.theInputFileReader.ReadInt32()}
        End With

        Me.thePhyFileData.theFileSeekLog.Add(compactSurfaceOffsetStart, Me.theInputFileReader.BaseStream.Position - 1, "CompactSurface")
        Return compactSurfaceOffsetStart
    End Function

    Private Function ReadCompactLedgeTreeNode() As SourcePhyFileCompactLedgeTreeNode
        Dim compactLedgeTreeNodeOffsetStart As Long = Me.theInputFileReader.BaseStream.Position

        Dim compactLedgeTreeNode As New SourcePhyFileCompactLedgeTreeNode
        With compactLedgeTreeNode
            .offsetRightNode = Me.theInputFileReader.ReadInt32()
            .offsetCompactLedge = Me.theInputFileReader.ReadInt32()
            .center = New SourceVector(Me.theInputFileReader.ReadSingle(), Me.theInputFileReader.ReadSingle(), Me.theInputFileReader.ReadSingle())
            .radius = Me.theInputFileReader.ReadSingle()
            .boxSizes = {Me.theInputFileReader.ReadByte(), Me.theInputFileReader.ReadByte(), Me.theInputFileReader.ReadByte()}
            .free = Me.theInputFileReader.ReadByte()
            .theReadOffset = compactLedgeTreeNodeOffsetStart
        End With

        Me.thePhyFileData.theFileSeekLog.Add(compactLedgeTreeNodeOffsetStart, Me.theInputFileReader.BaseStream.Position - 1, "CompactLedgeTreeNode")

        Return compactLedgeTreeNode
    End Function

    Private Function ReadCompactLedge(ByRef vertexCount As UShort, ByRef vertexOffset As Long) As SourcePhyFileCompactLedge
        Dim compactLedgeOffsetStart As Long = Me.theInputFileReader.BaseStream.Position

        Dim compactLedge As New SourcePhyFileCompactLedge
        With compactLedge
            .pointOffset = Me.theInputFileReader.ReadInt32()
            .terminalData = Me.theInputFileReader.ReadInt32()
        End With
        Dim bitField As UInteger = Me.theInputFileReader.ReadUInt32()
        With compactLedge
            .hasChildren = (bitField And 3) <> 0
            .isCompact = ((bitField >> 2) And 3) <> 0
            .dummy = CByte((bitField >> 4) And 15)
            .sizeDivided16 = CUInt(bitField >> 8)
            .triangleCount = Me.theInputFileReader.ReadInt16()
            .forFutureUse = Me.theInputFileReader.ReadInt16()
        End With

        Me.thePhyFileData.theFileSeekLog.Add(compactLedgeOffsetStart, Me.theInputFileReader.BaseStream.Position - 1, "CompactLedge")

        Dim compactTrianglesOffsetStart As Long = Me.theInputFileReader.BaseStream.Position

        compactLedge.theTriangles = New List(Of SourcePhyFileCompactTriangle)
        For triangleIndex As Integer = 0 To compactLedge.triangleCount - 1
            Dim compactTriangle As SourcePhyFileCompactTriangle = Me.ReadCompactTriangle()
            For Each compactEdge As SourcePhyFileCompactEdge In compactTriangle.edges
                If compactEdge.startPointIndex > vertexCount Then
                    vertexCount = compactEdge.startPointIndex
                End If
            Next
            compactLedge.theTriangles.Add(compactTriangle)
        Next
        vertexOffset = compactLedgeOffsetStart + compactLedge.pointOffset

        Me.thePhyFileData.theFileSeekLog.Add(compactTrianglesOffsetStart, Me.theInputFileReader.BaseStream.Position - 1, "CompactTriangles")

        Return compactLedge
    End Function

    Private Function ReadCompactTriangle() As SourcePhyFileCompactTriangle
        Dim compactTriangle As New SourcePhyFileCompactTriangle
        Dim bitField As UInteger = Me.theInputFileReader.ReadUInt32()
        With compactTriangle
            .triangleIndex = CUShort(bitField And 4095)
            .pierceIndex = CUShort((bitField >> 12) And 4095)
            .materialIndex = CByte((bitField >> 24) And SByte.MaxValue)
            .isVirtual = (bitField >> 31) <> 0
            .edges = {Me.ReadCompactEdge(), Me.ReadCompactEdge(), Me.ReadCompactEdge()}
        End With
        Return compactTriangle
    End Function

    Private Function ReadCompactEdge() As SourcePhyFileCompactEdge
        Dim compactEdge As New SourcePhyFileCompactEdge
        Dim bitField As UInteger = Me.theInputFileReader.ReadUInt32()
        With compactEdge
            .startPointIndex = CUShort(bitField And UShort.MaxValue)
            .oppositeIndex = (CShort((bitField >> 16) And Short.MaxValue) << 1) >> 1
            .isVirtual = (bitField >> 31) <> 0
        End With
        Return compactEdge
    End Function

    Private Sub ReadCollision()
        Dim collision As New SourcePhyFileCollision
        Dim textKey As String = ""
        Dim textValue As String = ""

        While FileManager.ReadKeyValueLine(Me.theInputFileReader, textKey, textValue)
            If textKey = "index" Then
                collision.index = Integer.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "name" Then
                collision.name = textValue
            ElseIf textKey = "parent" Then
                collision.parent = textValue
            ElseIf textKey = "mass" Then
                collision.mass = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "surfaceprop" Then
                collision.surfaceProperties = textValue
            ElseIf textKey = "damping" Then
                collision.damping = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "rotdamping" Then
                collision.rotationDamping = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "drag" Then
                collision.drag = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "inertia" Then
                collision.inertia = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "volume" Then
                collision.volume = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "massbias" Then
                collision.massBias = Single.Parse(textValue, TheApp.InternalNumberFormat)
            End If
        End While

        Me.thePhyFileData.theCollisions.Add(collision)
    End Sub

    Private Sub ReadConstraints()
        Dim constraints As New SourcePhyFileConstraints
        Dim textKey As String = ""
        Dim textValue As String = ""

        While FileManager.ReadKeyValueLine(Me.theInputFileReader, textKey, textValue)
            If textKey = "parent" Then
                constraints.parent = Integer.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "child" Then
                constraints.child = Integer.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "xmin" Then
                constraints.rollMinmum = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "xmax" Then
                constraints.rollMaxmum = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "xfriction" Then
                constraints.rollFriction = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "ymin" Then
                constraints.pitchMinmum = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "ymax" Then
                constraints.pitchMaxmum = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "yfriction" Then
                constraints.pitchFriction = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "zmin" Then
                constraints.yawMinmum = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "zmax" Then
                constraints.yawMaxmum = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "zfriction" Then
                constraints.yawFriction = Single.Parse(textValue, TheApp.InternalNumberFormat)
            End If
        End While

        Me.thePhyFileData.theConstraints.Add(constraints)
    End Sub

    Private Sub ReadCollisionRules()

        Dim textKey As String = ""
        Dim textValue As String = ""

        While FileManager.ReadKeyValueLine(Me.theInputFileReader, textKey, textValue)
            If textKey = "selfcollisions" Then
                Me.thePhyFileData.theSelfCollisions = Integer.Parse(textValue, TheApp.InternalNumberFormat) = 0
            ElseIf textKey = "collisionpair" Then
                Dim pairs As String() = textValue.Split(","c)

                If pairs.Length = 2 Then
                    Dim collisionRules As New SourcePhyFileCollisionRules
                    collisionRules.jointBone = Integer.Parse(pairs(0), TheApp.InternalNumberFormat)
                    collisionRules.collidBone = Integer.Parse(pairs(1), TheApp.InternalNumberFormat)
                    Me.thePhyFileData.theCollisionRules.Add(collisionRules)
                End If
            End If
        End While


    End Sub

    Private Sub ReadAnimatedFriction()
        Dim animatedFriction As New SourcePhyFileAnimatedFriction
        Dim textKey As String = ""
        Dim textValue As String = ""

        While FileManager.ReadKeyValueLine(Me.theInputFileReader, textKey, textValue)
            If textKey = "animfrictionmin" Then
                animatedFriction.animatedFrictionMinmum = CInt(Single.Parse(textValue, TheApp.InternalNumberFormat))
            ElseIf textKey = "animfrictionmax" Then
                animatedFriction.animatedFrictionMaxmum = CInt(Single.Parse(textValue, TheApp.InternalNumberFormat))
            ElseIf textKey = "animfrictiontimein" Then
                animatedFriction.animatedFrictionTimeIn = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "animfrictiontimeout" Then
                animatedFriction.animatedFrictionTimeOut = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "animfrictiontimehold" Then
                animatedFriction.animatedFrictionTimeHold = Single.Parse(textValue, TheApp.InternalNumberFormat)
            End If
        End While

        Me.thePhyFileData.theAnimatedFriction = animatedFriction
    End Sub

    Private Sub ReadEditParameters()
        Dim editParameters As New SourcePhyFileEditParameters
        editParameters.jointMergers = New List(Of SourcePhyFileJointMerge)
        Dim textKey As String = ""
        Dim textValue As String = ""

        While FileManager.ReadKeyValueLine(Me.theInputFileReader, textKey, textValue)
            If textKey = "rootname" Then
                editParameters.rootName = textValue
            ElseIf textKey = "totalmass" Then
                editParameters.totalMass = Single.Parse(textValue, TheApp.InternalNumberFormat)
            ElseIf textKey = "concave" Then
                editParameters.concave = Integer.Parse(textValue, TheApp.InternalNumberFormat) <> 0
            ElseIf textKey = "jointmerge" Then
                Dim pairs As String() = textValue.Split(","c)
                If pairs.Length = 2 Then
                    editParameters.jointMergers.Add(New SourcePhyFileJointMerge With {
                        .parent = pairs(0),
                        .child = pairs(1)
                    })
                End If
            End If
        End While

        Me.thePhyFileData.theEditParameters = editParameters
    End Sub
#End Region

#Region "Data"

    Private ReadOnly theInputFileReader As BinaryReader
    Private ReadOnly thePhyFileData As SourcePhyFileData
    Private ReadOnly thePhyEndOffset As Long

#End Region

End Class
