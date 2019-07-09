Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing

Public Class SplitContainerEx
    Inherits SplitContainer

	''''' <summary>Determines the thickness of the splitter.</summary>
	''<DefaultValue(GetType(Integer), "5"), Description("Determines the thickness of the splitter.")>
	''Public Overridable Shadows Property SplitterWidth() As Integer
	''    Get
	''        Return MyBase.SplitterWidth
	''    End Get
	''    Set(ByVal value As Integer)
	''        If value < 5 Then value = 5

	''        MyBase.SplitterWidth = value
	''    End Set
	''End Property

	'Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
	'	MyBase.OnMouseDown(e)

	'	'If Me.PointToClient(Cursor.Position).Y <= Me.SplitterDistance + 10 Then
	'	'	Me.theSplitterBarDifference = Me.SplitterDistance - Cursor.Position.Y
	'	'	Me.theSplitterBarIsBeingMoved = True
	'	'End If
	'End Sub

	'Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
	'	MyBase.OnMouseMove(e)

	'	'TODO: If the cursor is within the top portion of Panel2, then change Cursor of the widget there. 
	'	Dim aWidget As Control = FindControlAtScreenPosition(Me.ParentForm, Cursor.Position)
	'	If (Me.PointToClient(Cursor.Position).Y > (Me.SplitterDistance + Me.SplitterWidth)) Then
	'		If (Me.PointToClient(Cursor.Position).Y <= (Me.SplitterDistance + Me.SplitterWidth) + 10) Then
	'			aWidget.Cursor = Cursors.HSplit
	'		Else
	'			aWidget.Cursor = Cursors.Default
	'		End If
	'	End If
	'	'======
	'	'If (Me.PointToClient(Cursor.Position).Y > (Me.SplitterDistance + Me.SplitterWidth)) AndAlso (Me.PointToClient(Cursor.Position).Y <= (Me.SplitterDistance + Me.SplitterWidth) + 10) Then
	'	'	Me.Cursor = Cursors.HSplit
	'	'Else
	'	'	Me.Cursor = Cursors.Default
	'	'End If
	'	'======
	'	'Dim aWidget As Control = FindControlAtScreenPosition(Me.ParentForm, Cursor.Position)
	'	'If Me.Panel2.Controls(0).PointToClient(Cursor.Position).Y <= 10 Then
	'	'	Me.Panel2.Controls(0).Cursor = Cursors.HSplit
	'	'Else
	'	'	Me.Panel2.Controls(0).Cursor = Cursors.Default
	'	'End If

	'	'If Me.theSplitterBarIsBeingMoved Then
	'	'	'If MouseButtons = MouseButtons.Left Then
	'	'	'	'Me.TopMiddleSplitContainer.SplitterDistance = Me.TopMiddleSplitContainer.PointToClient(Cursor.Position).Y - Me.TopMiddleSplitContainer.SplitterWidth - Me.ItemGroupBox.PointToClient(Cursor.Position).Y
	'	'	'	Me.TopMiddleSplitContainer.SplitterDistance = Cursor.Position.Y + Me.theSplitterBarDifference
	'	'	'End If
	'	'	'Dim args As New SplitterCancelEventArgs(Me.TopMiddleSplitContainer.PointToClient(Cursor.Position).X, Me.TopMiddleSplitContainer.PointToClient(Cursor.Position).Y, 0, 0)
	'	'	'Me.TopMiddleSplitContainer.OnSplitterMoving(args)
	'	'End If
	'End Sub

	'Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
	'	MyBase.OnMouseUp(e)

	'	'If Me.theSplitterBarIsBeingMoved Then
	'	'	Me.SplitterDistance = Cursor.Position.Y + Me.theSplitterBarDifference
	'	'	Me.theSplitterBarIsBeingMoved = False
	'	'End If
	'End Sub

	'Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
	'	MyBase.OnPaint(e)

	'	'If MyBase.SplitterWidth >= 4 Then
	'	'    '======
	'	'    ' Draw dots. 
	'	'    Dim points(2) As Point
	'	'    Dim pointRect = Rectangle.Empty

	'	'    If Orientation = Windows.Forms.Orientation.Horizontal Then
	'	'        points(0) = New Point((MyBase.Width \ 2), SplitterDistance + (SplitterWidth \ 2))
	'	'        points(1) = New Point(points(0).X - 10, points(0).Y)
	'	'        points(2) = New Point(points(2).X + 10, points(0).Y)
	'	'        pointRect = New Rectangle(points(1).X - 2, points(1).Y - 2, 25, 5)
	'	'    Else
	'	'        points(0) = New Point(SplitterDistance + (SplitterWidth \ 2), (MyBase.Height \ 2))
	'	'        points(1) = New Point(points(0).X, points(0).Y - 10)
	'	'        points(2) = New Point(points(0).X, points(0).Y + 10)
	'	'        pointRect = New Rectangle(points(1).X - 2, points(1).Y - 2, 5, 25)
	'	'    End If

	'	'    ' Figure out the correct system colors.
	'	'    Using br = New SolidBrush(SystemColors.Control)
	'	'        e.Graphics.FillRectangle(br, pointRect)
	'	'    End Using

	'	'    For Each p In points
	'	'        p.Offset(-1, -1)
	'	'        Using br = New SolidBrush(SystemColors.ControlDarkDark)
	'	'            e.Graphics.FillEllipse(br, New Rectangle(p, New Size(3, 3)))
	'	'        End Using
	'	'    Next
	'	'    '======
	'	'    'Dim facePoints(1) As Point
	'	'    'Dim shadowPoints(1) As Point

	'	'    'If Me.Orientation = Windows.Forms.Orientation.Horizontal Then
	'	'    '    facePoints(0) = New Point((Me.Width \ 2) - 10, Me.SplitterDistance + (Me.SplitterWidth \ 2))
	'	'    '    facePoints(1) = New Point(facePoints(0).X + 10 * 2, facePoints(0).Y)
	'	'    '    shadowPoints(0) = New Point(facePoints(0).X, facePoints(0).Y + 1)
	'	'    '    shadowPoints(1) = New Point(facePoints(1).X, facePoints(1).Y + 1)
	'	'    'End If

	'	'    'Using aPen = New Pen(SystemColors.ControlLightLight)
	'	'    '    e.Graphics.DrawLine(aPen, facePoints(0), facePoints(1))
	'	'    'End Using
	'	'    'Using aPen = New Pen(SystemColors.ControlDarkDark)
	'	'    '    e.Graphics.DrawLine(aPen, shadowPoints(0), shadowPoints(1))
	'	'    'End Using
	'	'    '======
	'	'End If
	'End Sub

	'Private Function FindControlAtScreenPosition(ByVal aForm As Form, p As Point) As Control
	'	If (Not aForm.Bounds.Contains(p)) Then
	'		Return Nothing
	'	End If
	'	Dim c As Control = aForm
	'	Dim c1 As Control = Nothing
	'	While c IsNot Nothing
	'		c1 = c
	'		c = c.GetChildAtPoint(c.PointToClient(p), GetChildAtPointSkip.Invisible)
	'	End While
	'	Return c1
	'End Function

	'Private theSplitterBarDifference As Integer
	'Private theSplitterBarIsBeingMoved As Boolean = False

End Class