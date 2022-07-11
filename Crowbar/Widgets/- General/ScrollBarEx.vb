Imports System.ComponentModel

Public Class ScrollBarEx
	Inherits Control

	Public Sub New()
		SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
		SetStyle(ControlStyles.Selectable, False)

		Me.theSmallChange = 1
		Me.theLargeChange = 10

		_scrollTimer = New Timer()
		_scrollTimer.Interval = 1
		AddHandler _scrollTimer.Tick, AddressOf ScrollTimerTick
	End Sub

	<Category("Behavior")>
	<Description("The orientation type of the scrollbar.")>
	<DefaultValue(DarkScrollOrientation.Vertical)>
	Public Property ScrollOrientation As DarkScrollOrientation
		Get
			Return _scrollOrientation
		End Get
		Set(ByVal value As DarkScrollOrientation)
			_scrollOrientation = value
			UpdateScrollBar()
		End Set
	End Property

	<Category("Behavior")>
	<Description("The value that the scroll thumb position represents.")>
	<DefaultValue(0)>
	Public Property Value As Integer
		Get
			Return _value
		End Get
		Set(ByVal value As Integer)
			If value < Minimum Then value = Minimum
			Dim maximumValue As Integer = Maximum - ViewSize
			If value > maximumValue Then value = maximumValue
			If _value = value Then Return
			_value = value
			UpdateThumb(True)
			RaiseEvent ValueChanged(Me, New ScrollValueEventArgs(value))
		End Set
	End Property

	<Category("Behavior")>
	<Description("The lower limit value of the scrollable range.")>
	<DefaultValue(0)>
	Public Property Minimum As Integer
		Get
			Return _minimum
		End Get
		Set(ByVal value As Integer)
			_minimum = value
			UpdateScrollBar()
		End Set
	End Property

	<Category("Behavior")>
	<Description("The upper limit value of the scrollable range.")>
	<DefaultValue(100)>
	Public Property Maximum As Integer
		Get
			Return _maximum
		End Get
		Set(ByVal value As Integer)
			_maximum = value
			UpdateScrollBar()
		End Set
	End Property

	<Category("Behavior")>
	<Description("The view size for the scrollable area.")>
	<DefaultValue(0)>
	Public Property ViewSize As Integer
		Get
			Return _viewSize
		End Get
		Set(ByVal value As Integer)
			_viewSize = value
			UpdateScrollBar()
		End Set
	End Property

	<Category("Behavior")>
	<Description("Change when scroll bar clicked or when PAGE UP or PAGE DOWN key pressed.")>
	<DefaultValue(10)>
	Public Property LargeChange As Integer
		Get
			Return Me.theLargeChange
		End Get
		Set(ByVal value As Integer)
			Me.theLargeChange = value
		End Set
	End Property

	<Category("Behavior")>
	<Description("Change when a scroll arrow clicked or arrow key pressed.")>
	<DefaultValue(1)>
	Public Property SmallChange As Integer
		Get
			Return Me.theSmallChange
		End Get
		Set(ByVal value As Integer)
			Me.theSmallChange = value
		End Set
	End Property

	Public Overloads Property Visible As Boolean
		Get
			Return MyBase.Visible
		End Get
		Set(ByVal value As Boolean)
			If MyBase.Visible = value Then Return
			MyBase.Visible = value
		End Set
	End Property

	'NOTE: This is documented to be preferred way to set the cursor for derived Control.
	Protected Overrides ReadOnly Property DefaultCursor As Cursor
		Get
			Return Cursors.Arrow
		End Get
	End Property

	Protected Overrides Sub OnResize(ByVal e As EventArgs)
		MyBase.OnResize(e)
		UpdateScrollBar()
	End Sub

	Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
		MyBase.OnMouseDown(e)

		If _thumbArea.Contains(e.Location) AndAlso e.Button = MouseButtons.Left Then
			_isScrolling = True
			_initialContact = e.Location

			If _scrollOrientation = DarkScrollOrientation.Vertical Then
				_initialValue = _thumbArea.Top
			Else
				_initialValue = _thumbArea.Left
			End If

			Invalidate()
			Return
		End If

		If _upArrowArea.Contains(e.Location) AndAlso e.Button = MouseButtons.Left Then
			_upArrowClicked = True
			_scrollTimer.Enabled = True
			Invalidate()
			Return
		End If

		If _downArrowArea.Contains(e.Location) AndAlso e.Button = MouseButtons.Left Then
			_downArrowClicked = True
			_scrollTimer.Enabled = True
			Invalidate()
			Return
		End If

		If _trackArea.Contains(e.Location) AndAlso e.Button = MouseButtons.Left Then
			If _scrollOrientation = DarkScrollOrientation.Vertical Then
				Dim modRect As New Rectangle(_thumbArea.Left, _trackArea.Top, _thumbArea.Width, _trackArea.Height)
				If Not modRect.Contains(e.Location) Then
					Return
				End If
				Dim loc As Integer = e.Location.Y
				loc -= _upArrowArea.Bottom - 1
				loc -= CInt(_thumbArea.Height / 2)
				'ScrollToPhysical(loc)
				Dim directionFactor As Integer = If(loc < _thumbArea.Top, -1, 1)
				Me.ScrollBy(Me.theLargeChange * directionFactor)
			Else
				Dim modRect As New Rectangle(_trackArea.Left, _thumbArea.Top, _trackArea.Width, _thumbArea.Height)
				If Not modRect.Contains(e.Location) Then
					Return
				End If
				Dim loc As Integer = e.Location.X
				loc -= _upArrowArea.Right - 1
				loc -= CInt(_thumbArea.Width / 2)
				'ScrollToPhysical(loc)
				Dim directionFactor As Integer = If(loc < _thumbArea.Left, -1, 1)
				Me.ScrollBy(Me.theLargeChange * directionFactor)
			End If

			_isScrolling = True
			_initialContact = e.Location
			_thumbHot = True

			If _scrollOrientation = DarkScrollOrientation.Vertical Then
				_initialValue = _thumbArea.Top
			Else
				_initialValue = _thumbArea.Left
			End If

			Invalidate()
			Return
		End If
	End Sub

	Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
		MyBase.OnMouseUp(e)
		_isScrolling = False
		_thumbClicked = False
		_upArrowClicked = False
		_downArrowClicked = False
		Invalidate()
	End Sub

	Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
		MyBase.OnMouseMove(e)

		If Not _isScrolling Then
			Dim thumbHot As Boolean = _thumbArea.Contains(e.Location)

			If _thumbHot <> thumbHot Then
				_thumbHot = thumbHot
				Invalidate()
			End If

			Dim upArrowHot As Boolean = _upArrowArea.Contains(e.Location)

			If _upArrowHot <> upArrowHot Then
				_upArrowHot = upArrowHot
				Invalidate()
			End If

			Dim downArrowHot As Boolean = _downArrowArea.Contains(e.Location)

			If _downArrowHot <> downArrowHot Then
				_downArrowHot = downArrowHot
				Invalidate()
			End If
		End If

		If _isScrolling Then

			If e.Button <> MouseButtons.Left Then
				OnMouseUp(Nothing)
				Return
			End If

			Dim difference As New Point(e.Location.X - _initialContact.X, e.Location.Y - _initialContact.Y)

			If _scrollOrientation = DarkScrollOrientation.Vertical Then
				Dim thumbPos As Integer = (_initialValue - _trackArea.Top)
				Dim newPosition As Integer = thumbPos + difference.Y
				ScrollToPhysical(newPosition)
			ElseIf _scrollOrientation = DarkScrollOrientation.Horizontal Then
				Dim thumbPos As Integer = (_initialValue - _trackArea.Left)
				Dim newPosition As Integer = thumbPos + difference.X
				ScrollToPhysical(newPosition)
			End If

			UpdateScrollBar()
		End If
	End Sub

	Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
		MyBase.OnMouseLeave(e)
		_thumbHot = False
		_upArrowHot = False
		_downArrowHot = False
		Invalidate()
	End Sub

	Private Sub ScrollTimerTick(ByVal sender As Object, ByVal e As EventArgs)
		If Not _upArrowClicked AndAlso Not _downArrowClicked Then
			_scrollTimer.Enabled = False
			Return
		End If

		If _upArrowClicked Then
			'ScrollBy(-1)
			Me.ScrollBy(-Me.theSmallChange)
		ElseIf _downArrowClicked Then
			'ScrollBy(1)
			Me.ScrollBy(Me.theSmallChange)
		End If
	End Sub

	Public Sub ScrollTo(ByVal position As Integer)
		Value = position
	End Sub

	Public Sub ScrollToPhysical(ByVal positionInPixels As Integer)
		Dim isVert As Boolean = (_scrollOrientation = DarkScrollOrientation.Vertical)
		Dim trackAreaSize As Integer = If(isVert, _trackArea.Height - _thumbArea.Height, _trackArea.Width - _thumbArea.Width)
		Dim positionRatio As Double = CSng(positionInPixels) / CSng(trackAreaSize)
		Dim viewScrollSize As Integer = (Maximum - ViewSize)
		Dim newValue As Integer = CInt((positionRatio * viewScrollSize))
		Value = newValue
	End Sub

	Public Sub ScrollBy(ByVal offset As Integer)
		Dim newValue As Integer = Value + offset
		ScrollTo(newValue)
	End Sub

	Public Sub ScrollByPhysical(ByVal offsetInPixels As Integer)
		Dim isVert As Boolean = _scrollOrientation = DarkScrollOrientation.Vertical
		Dim thumbPos As Integer = If(isVert, (_thumbArea.Top - _trackArea.Top), (_thumbArea.Left - _trackArea.Left))
		Dim newPosition As Integer = thumbPos - offsetInPixels
		ScrollToPhysical(newPosition)
	End Sub

	Public Sub UpdateScrollBar()
		Dim area As Rectangle = ClientRectangle

		If _scrollOrientation = DarkScrollOrientation.Vertical Then
			_upArrowArea = New Rectangle(area.Left, area.Top, Consts.ArrowButtonSize, Consts.ArrowButtonSize)
			_downArrowArea = New Rectangle(area.Left, area.Bottom - Consts.ArrowButtonSize, Consts.ArrowButtonSize, Consts.ArrowButtonSize)
		ElseIf _scrollOrientation = DarkScrollOrientation.Horizontal Then
			_upArrowArea = New Rectangle(area.Left, area.Top, Consts.ArrowButtonSize, Consts.ArrowButtonSize)
			_downArrowArea = New Rectangle(area.Right - Consts.ArrowButtonSize, area.Top, Consts.ArrowButtonSize, Consts.ArrowButtonSize)
		End If

		If _scrollOrientation = DarkScrollOrientation.Vertical Then
			_trackArea = New Rectangle(area.Left, area.Top + Consts.ArrowButtonSize, area.Width, area.Height - (Consts.ArrowButtonSize * 2))
		ElseIf _scrollOrientation = DarkScrollOrientation.Horizontal Then
			_trackArea = New Rectangle(area.Left + Consts.ArrowButtonSize, area.Top, area.Width - (Consts.ArrowButtonSize * 2), area.Height)
		End If

		UpdateThumb()
		Invalidate()
	End Sub

	Private Sub UpdateThumb(ByVal Optional forceRefresh As Boolean = False)
		If ViewSize >= Maximum Then Return
		Dim maximumValue As Integer = Maximum - ViewSize
		If Value > maximumValue Then Value = maximumValue
		_viewContentRatio = CSng(ViewSize) / CSng(Maximum)
		Dim viewAreaSize As Integer = Maximum - ViewSize
		Dim positionRatio As Double = CSng(Value) / CSng(viewAreaSize)

		If _scrollOrientation = DarkScrollOrientation.Vertical Then
			Dim thumbSize As Integer = CInt((_trackArea.Height * _viewContentRatio))
			If thumbSize < Consts.MinimumThumbSize Then thumbSize = Consts.MinimumThumbSize
			Dim trackAreaSize As Integer = _trackArea.Height - thumbSize
			Dim thumbPosition As Integer = CInt((trackAreaSize * positionRatio))
			_thumbArea = New Rectangle(_trackArea.Left + 3, _trackArea.Top + thumbPosition, Consts.ScrollBarSize - 6, thumbSize)
		ElseIf _scrollOrientation = DarkScrollOrientation.Horizontal Then
			Dim thumbSize As Integer = CInt((_trackArea.Width * _viewContentRatio))
			If thumbSize < Consts.MinimumThumbSize Then thumbSize = Consts.MinimumThumbSize
			Dim trackAreaSize As Integer = _trackArea.Width - thumbSize
			Dim thumbPosition As Integer = CInt((trackAreaSize * positionRatio))
			_thumbArea = New Rectangle(_trackArea.Left + thumbPosition, _trackArea.Top + 3, thumbSize, Consts.ScrollBarSize - 6)
		End If

		If forceRefresh Then
			Invalidate()
			Update()
		End If
	End Sub

	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		Dim g As Graphics = e.Graphics

		Dim upIcon As Bitmap = If(_upArrowHot, My.Resources.scrollbar_arrow_hot, My.Resources.scrollbar_arrow_standard)
		If _upArrowClicked Then upIcon = My.Resources.scrollbar_arrow_clicked
		If Not Enabled Then upIcon = My.Resources.scrollbar_arrow_disabled

		If _scrollOrientation = DarkScrollOrientation.Vertical Then
			upIcon.RotateFlip(RotateFlipType.RotateNoneFlipY)
		ElseIf _scrollOrientation = DarkScrollOrientation.Horizontal Then
			upIcon.RotateFlip(RotateFlipType.Rotate90FlipNone)
		End If

		g.DrawImageUnscaled(upIcon, CInt(_upArrowArea.Left + (_upArrowArea.Width / 2) - (upIcon.Width / 2)), CInt(_upArrowArea.Top + (_upArrowArea.Height / 2) - (upIcon.Height / 2)))
		Dim downIcon As Bitmap = If(_downArrowHot, My.Resources.scrollbar_arrow_hot, My.Resources.scrollbar_arrow_standard)
		If _downArrowClicked Then downIcon = My.Resources.scrollbar_arrow_clicked
		If Not Enabled Then downIcon = My.Resources.scrollbar_arrow_disabled
		If _scrollOrientation = DarkScrollOrientation.Horizontal Then downIcon.RotateFlip(RotateFlipType.Rotate270FlipNone)
		g.DrawImageUnscaled(downIcon, CInt(_downArrowArea.Left + (_downArrowArea.Width / 2) - (downIcon.Width / 2)), CInt(_downArrowArea.Top + (_downArrowArea.Height / 2) - (downIcon.Height / 2)))

		If Enabled Then
			'Dim scrollColor As Color = If(_thumbHot, Colors.GreyHighlight, Colors.GreySelection)
			'If _isScrolling Then scrollColor = Colors.ActiveControl
			Dim scrollColor As Color = If(_thumbHot, Color.FromArgb(122, 128, 132), Color.FromArgb(92, 92, 92))
			If _isScrolling Then scrollColor = Color.FromArgb(159, 178, 196)

			Using b As New SolidBrush(scrollColor)
				g.FillRectangle(b, _thumbArea)
			End Using
		End If
	End Sub

	Public Event ValueChanged As EventHandler(Of ScrollValueEventArgs)

	Public Enum DarkScrollOrientation
		Vertical
		Horizontal
	End Enum

	Private _scrollOrientation As DarkScrollOrientation
	Private _value As Integer
	Private _minimum As Integer = 0
	Private _maximum As Integer = 100
	Private _viewSize As Integer
	Private _trackArea As Rectangle
	Private _viewContentRatio As Single
	Private _thumbArea As Rectangle
	Private _upArrowArea As Rectangle
	Private _downArrowArea As Rectangle
	Private _thumbHot As Boolean
	Private _upArrowHot As Boolean
	Private _downArrowHot As Boolean
	Private _thumbClicked As Boolean
	Private _upArrowClicked As Boolean
	Private _downArrowClicked As Boolean
	Private _isScrolling As Boolean
	Private _initialValue As Integer
	Private _initialContact As Point
	Private _scrollTimer As Timer
	Private theLargeChange As Integer
	Private theSmallChange As Integer
	Private ReadOnly _DefaultCursor As Cursor

	Class Consts
		Public Shared ScrollBarSize As Integer = 15
		Public Shared ArrowButtonSize As Integer = 15
		Public Shared MinimumThumbSize As Integer = 11
	End Class

End Class
