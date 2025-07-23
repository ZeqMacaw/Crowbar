Imports System.ComponentModel

Public Class ScrollBarPanel
	Inherits Panel

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theRightAndBottomBorderWidth = 0
	End Sub

#End Region

#Region "Init and Free"

	'Private Sub Init()
	'    ' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
	'    If TheApp IsNot Nothing Then
	'        Me.UpdateTheme()
	'        AddHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
	'    End If
	'End Sub

	'Private Sub Free()
	'    ' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
	'    If TheApp IsNot Nothing Then
	'        RemoveHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
	'    End If
	'End Sub

#End Region

#Region "Properties"

	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Property RightAndBottomBorderWidth() As Integer
		Get
			Return Me.theRightAndBottomBorderWidth
		End Get
		Set(ByVal value As Integer)
			If Me.theRightAndBottomBorderWidth <> value Then
				Me.theRightAndBottomBorderWidth = value
			End If
		End Set
	End Property

	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Property RightAndBottomBorderColor() As Color
		Get
			Return Me.theRightAndBottomBorderColor
		End Get
		Set(ByVal value As Color)
			If Me.theRightAndBottomBorderColor <> value Then
				Me.theRightAndBottomBorderColor = value
			End If
		End Set
	End Property

#End Region

#Region "Methods"

#End Region

#Region "Events"

#End Region

#Region "Widget Event Handlers"

	'Protected Overrides Sub OnHandleCreated(ByVal e As System.EventArgs)
	'	MyBase.OnHandleCreated(e)
	'	' [04-Feb-2026] Me.DesignMode is unreliable in nested widgets.
	'	'If Not Me.DesignMode Then
	'	Me.Init()
	'	'End If
	'End Sub

	'Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
	'	Me.Free()
	'	MyBase.OnHandleDestroyed(e)
	'End Sub

	' Works without needing to call SetStyle.
	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		MyBase.OnPaint(e)

		' Paint right and bottom borders.
		If Me.theRightAndBottomBorderWidth > 0 Then
			Dim g As Graphics = e.Graphics
			Using borderColorPen As New Pen(Me.theRightAndBottomBorderColor, Me.theRightAndBottomBorderWidth)
				' - 1 for 0-based coord
				Dim leftBottomPoint As New Point(0, Me.ClientRectangle.Bottom - 1)
				Dim rightBottomPoint As New Point(Me.ClientRectangle.Right - 1, Me.ClientRectangle.Bottom - 1)
				Dim rightTopPoint As New Point(Me.ClientRectangle.Right - 1, 0)
				borderColorPen.Alignment = Drawing2D.PenAlignment.Outset
				g.DrawLine(borderColorPen, leftBottomPoint, rightBottomPoint)
				borderColorPen.Alignment = Drawing2D.PenAlignment.Inset
				g.DrawLine(borderColorPen, rightTopPoint, rightBottomPoint)
			End Using
		End If
	End Sub

#End Region

#Region "Data"

	Private theRightAndBottomBorderWidth As Integer
	Private theRightAndBottomBorderColor As Color

#End Region

End Class
