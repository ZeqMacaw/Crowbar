Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class ScrollBarEx
	Inherits Control

#Region "Create and Destroy"

	Public Sub New()
		SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
		SetStyle(ControlStyles.Selectable, False)

		Me.theSmallChange = 1
		Me.theLargeChange = 10

		_scrollTimer = New Timer()
		_scrollTimer.Interval = Consts.SmallChangeStartDelay
		AddHandler _scrollTimer.Tick, AddressOf ScrollTimerTick

		Me.theRightAndBottomPaddingColorIsUsed = False
		Me.theRightAndBottomBorderWidth = 0
	End Sub

#End Region

#Region "Init and Free"

#End Region

#Region "Properties"

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
			If value < Minimum Then
				value = Minimum
			End If
			Dim maximumValue As Integer = Maximum - ViewSize
			'If Maximum <= ViewSize Then
			'	maximumValue = Maximum
			'End If
			If value > maximumValue Then
				value = maximumValue
			End If
			If _value = value Then
				Return
			End If
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

	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Overloads Property LeftAndTopPaddingColorIsUsed As Boolean
		Get
			Return Me.theRightAndBottomPaddingColorIsUsed
		End Get
		Set(ByVal value As Boolean)
			If Me.theRightAndBottomPaddingColorIsUsed <> value Then
				Me.theRightAndBottomPaddingColorIsUsed = value
			End If
		End Set
	End Property

	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Property LeftAndTopPaddingColor() As Color
		Get
			Return Me.theRightAndBottomPaddingColor
		End Get
		Set(ByVal value As Color)
			If Me.theRightAndBottomPaddingColor <> value Then
				Me.theRightAndBottomPaddingColor = value
			End If
		End Set
	End Property

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
		'Console.WriteLine("ScrollBy " + newValue.ToString())
	End Sub

	Public Sub ScrollByPhysical(ByVal offsetInPixels As Integer)
		Dim isVert As Boolean = _scrollOrientation = DarkScrollOrientation.Vertical
		Dim thumbPos As Integer = If(isVert, (_thumbArea.Top - _trackArea.Top), (_thumbArea.Left - _trackArea.Left))
		Dim newPosition As Integer = thumbPos - offsetInPixels
		ScrollToPhysical(newPosition)
	End Sub

	Public Sub UpdateScrollBar()
		Dim area As Rectangle = Me.ClientRectangle

		If _scrollOrientation = DarkScrollOrientation.Vertical Then
			_upArrowArea = New Rectangle(area.Left, area.Top, Consts.ArrowButtonSize, Consts.ArrowButtonSize)
			_downArrowArea = New Rectangle(area.Left, area.Bottom - Consts.ArrowButtonSize, Consts.ArrowButtonSize, Consts.ArrowButtonSize)
			If Me.theRightAndBottomPaddingColorIsUsed Then
				_upArrowArea.X += 1
				_downArrowArea.X += 1
			End If
		ElseIf _scrollOrientation = DarkScrollOrientation.Horizontal Then
			_upArrowArea = New Rectangle(area.Left, area.Top, Consts.ArrowButtonSize, Consts.ArrowButtonSize)
			_downArrowArea = New Rectangle(area.Right - Consts.ArrowButtonSize, area.Top, Consts.ArrowButtonSize, Consts.ArrowButtonSize)
			If Me.theRightAndBottomPaddingColorIsUsed Then
				_upArrowArea.Y += 1
				_downArrowArea.Y += 1
			End If
		End If

		If _scrollOrientation = DarkScrollOrientation.Vertical Then
			_trackArea = New Rectangle(area.Left, area.Top + Consts.ArrowButtonSize, area.Width, area.Height - (Consts.ArrowButtonSize * 2))
			If Me.theRightAndBottomPaddingColorIsUsed Then
				_trackArea.X += 1
			End If
		ElseIf _scrollOrientation = DarkScrollOrientation.Horizontal Then
			_trackArea = New Rectangle(area.Left + Consts.ArrowButtonSize, area.Top, area.Width - (Consts.ArrowButtonSize * 2), area.Height)
			If Me.theRightAndBottomPaddingColorIsUsed Then
				_trackArea.Y += 1
			End If
		End If


		UpdateThumb()
		Invalidate()
	End Sub

#End Region

#Region "Events"

	Public Event ValueChanged As EventHandler(Of ScrollValueEventArgs)

#End Region

#Region "Widget Event Handlers"

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
			Me.ScrollBy(-Me.theSmallChange)
			Invalidate()
			Return
		End If

		If _downArrowArea.Contains(e.Location) AndAlso e.Button = MouseButtons.Left Then
			_downArrowClicked = True
			_scrollTimer.Enabled = True
			Me.ScrollBy(Me.theSmallChange)
			Invalidate()
			Return
		End If

		If _trackArea.Contains(e.Location) AndAlso e.Button = MouseButtons.Left Then
			If _scrollOrientation = DarkScrollOrientation.Vertical Then
				Dim modRect As New Rectangle(_thumbArea.Left, _trackArea.Top, _thumbArea.Width, _trackArea.Height)
				If Not modRect.Contains(e.Location) Then
					Return
				End If
				_verticalTrackAreaClicked = True
				_scrollTimer.Enabled = True
				Dim loc As Integer = e.Location.Y
				'loc -= _upArrowArea.Bottom - 1
				'loc -= CInt(_thumbArea.Height / 2)
				'ScrollToPhysical(loc)
				_directionFactor = If(loc < _thumbArea.Top, -1, 1)
				Me.ScrollBy(Me.theLargeChange * _directionFactor)
			Else
				Dim modRect As New Rectangle(_trackArea.Left, _thumbArea.Top, _trackArea.Width, _thumbArea.Height)
				If Not modRect.Contains(e.Location) Then
					Return
				End If
				_horizontalTrackAreaClicked = True
				_scrollTimer.Enabled = True
				Dim loc As Integer = e.Location.X
				'loc -= _upArrowArea.Right - 1
				'loc -= CInt(_thumbArea.Width / 2)
				'ScrollToPhysical(loc)
				_directionFactor = If(loc < _thumbArea.Left, -1, 1)
				Me.ScrollBy(Me.theLargeChange * _directionFactor)
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
		_verticalTrackAreaClicked = False
		_horizontalTrackAreaClicked = False
		Invalidate()
	End Sub

	Protected Overrides Sub OnMouseClick(ByVal e As MouseEventArgs)
		_scrollTimer.Enabled = False
		_scrollTimer.Interval = Consts.SmallChangeStartDelay
		MyBase.OnMouseClick(e)
		'If _upArrowClicked Then
		'	'ScrollBy(-1)
		'	Me.ScrollBy(-Me.theSmallChange)
		'ElseIf _downArrowClicked Then
		'	'ScrollBy(1)
		'	Me.ScrollBy(Me.theSmallChange)
		'End If
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

		If _isScrolling AndAlso Not _verticalTrackAreaClicked AndAlso Not _horizontalTrackAreaClicked Then

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

	Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
		MyBase.OnMouseWheel(e)

		Dim upOrDownValue As Integer = Me.theSmallChange * 3
		If e.Delta > 0 Then
			' Moving wheel away from user = up.
			Me.ScrollBy(-upOrDownValue)
		Else
			' Moving wheel toward user = down.
			Me.ScrollBy(upOrDownValue)
		End If
	End Sub

	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		Dim g As Graphics = e.Graphics

		Dim upIcon As Bitmap = If(_upArrowHot, My.Resources.scrollbar_arrow_hot, My.Resources.scrollbar_arrow_standard)
		If _upArrowClicked Then
			upIcon = My.Resources.scrollbar_arrow_clicked
		End If
		If Not Enabled Then
			upIcon = My.Resources.scrollbar_arrow_disabled
		End If
		If _scrollOrientation = DarkScrollOrientation.Vertical Then
			upIcon.RotateFlip(RotateFlipType.RotateNoneFlipY)
		ElseIf _scrollOrientation = DarkScrollOrientation.Horizontal Then
			upIcon.RotateFlip(RotateFlipType.Rotate90FlipNone)
		End If
		g.DrawImageUnscaled(upIcon, CInt(_upArrowArea.Left + (_upArrowArea.Width * 0.5) - (upIcon.Width * 0.5)), CInt(_upArrowArea.Top + (_upArrowArea.Height * 0.5) - (upIcon.Height * 0.5)))

		Dim downIcon As Bitmap = If(_downArrowHot, My.Resources.scrollbar_arrow_hot, My.Resources.scrollbar_arrow_standard)
		If _downArrowClicked Then
			downIcon = My.Resources.scrollbar_arrow_clicked
		End If
		If Not Enabled Then
			downIcon = My.Resources.scrollbar_arrow_disabled
		End If
		If _scrollOrientation = DarkScrollOrientation.Horizontal Then
			downIcon.RotateFlip(RotateFlipType.Rotate270FlipNone)
		End If
		g.DrawImageUnscaled(downIcon, CInt(_downArrowArea.Left + (_downArrowArea.Width * 0.5) - (downIcon.Width * 0.5)), CInt(_downArrowArea.Top + (_downArrowArea.Height * 0.5) - (downIcon.Height * 0.5)))

		If Enabled Then
			'Dim scrollColor As Color = If(_thumbHot, Colors.GreyHighlight, Colors.GreySelection)
			'If _isScrolling Then scrollColor = Colors.ActiveControl
			Dim scrollColor As Color = If(_thumbHot, Color.FromArgb(122, 128, 132), Color.FromArgb(92, 92, 92))
			If _isScrolling Then
				scrollColor = Color.FromArgb(159, 178, 196)
			End If

			Using b As New SolidBrush(scrollColor)
				g.FillRectangle(b, _thumbArea)
			End Using
		End If

		' Paint right and bottom padding lines (to the left and above scrollbars).
		If Me.theRightAndBottomPaddingColorIsUsed Then
			Using borderColorPen As New Pen(Me.theRightAndBottomPaddingColor)
				' - 1 for 0-based coord
				Dim leftTopPoint As New Point(0, 0)
				If Me._scrollOrientation = DarkScrollOrientation.Horizontal Then
					Dim rightTopPoint As New Point(Me.ClientRectangle.Right - 1, 0)
					g.DrawLine(borderColorPen, leftTopPoint, rightTopPoint)
				Else
					Dim leftBottomPoint As New Point(0, Me.ClientRectangle.Bottom - 1)
					g.DrawLine(borderColorPen, leftTopPoint, leftBottomPoint)
				End If
			End Using
		End If

		' Paint right and bottom borders.
		If Me.theRightAndBottomBorderWidth > 0 Then
			Using borderColorPen As New Pen(Me.theRightAndBottomBorderColor, Me.theRightAndBottomBorderWidth)
				Dim half As Integer = CInt(Math.Ceiling(Me.theRightAndBottomBorderWidth * 0.5))
				If Me._scrollOrientation = DarkScrollOrientation.Horizontal Then
					Dim leftBottomPoint As New Point(0, Me.ClientRectangle.Bottom - half)
					Dim rightBottomPoint As New Point(Me.ClientRectangle.Right, Me.ClientRectangle.Bottom - half)
					g.DrawLine(borderColorPen, leftBottomPoint, rightBottomPoint)
				Else
					Dim rightTopPoint As New Point(Me.ClientRectangle.Right - half, 0)
					Dim rightBottomPoint As New Point(Me.ClientRectangle.Right - half, Me.ClientRectangle.Bottom)
					g.DrawLine(borderColorPen, rightTopPoint, rightBottomPoint)
				End If
			End Using
		End If
	End Sub

	Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
		Dim g As Graphics = e.Graphics
		Dim rect As Rectangle = e.ClipRectangle
		Using b As New SolidBrush(Me.BackColor)
			g.FillRectangle(b, rect)
		End Using
		'If Me._scrollOrientation = DarkScrollOrientation.Vertical Then
		'	Using p As New Pen(Color.Red)
		'		Dim point1 As New Point(rect.Left + rect.Width, rect.Top)
		'		Dim point2 As New Point(point1.X, rect.Top + rect.Height)
		'		g.DrawLine(p, point1, point2)
		'	End Using
		'End If
	End Sub

	'Protected Overrides Sub WndProc(ByRef m As Message)
	'	Select Case m.Msg
	'		Case Win32Api.WindowsMessages.WM_NCCALCSIZE
	'			Me.OnNonClientCalcSize(m)
	'	End Select

	'	MyBase.WndProc(m)
	'End Sub

#End Region

#Region "Child Widget Event Handlers"

#End Region

#Region "Private Methods"

	Private Sub ScrollTimerTick(ByVal sender As Object, ByVal e As EventArgs)
		If Not _upArrowClicked AndAlso Not _downArrowClicked AndAlso Not _verticalTrackAreaClicked AndAlso Not _horizontalTrackAreaClicked Then
			_scrollTimer.Enabled = False
			_scrollTimer.Interval = Consts.SmallChangeStartDelay
			Return
		End If

		'_scrollTimer.Interval = 150
		_scrollTimer.Interval = 75

		If _upArrowClicked Then
			'ScrollBy(-1)
			Me.ScrollBy(-Me.theSmallChange)
		ElseIf _downArrowClicked Then
			'ScrollBy(1)
			Me.ScrollBy(Me.theSmallChange)
		ElseIf _verticalTrackAreaClicked Then
			Me.ScrollBy(Me.theLargeChange * _directionFactor)
		ElseIf _horizontalTrackAreaClicked Then
			Me.ScrollBy(Me.theLargeChange * _directionFactor)
		End If
	End Sub

	Private Sub UpdateThumb(ByVal Optional forceRefresh As Boolean = False)
		If ViewSize >= Maximum Then
			Return
		End If
		Dim maximumValue As Integer = Maximum - ViewSize
		If Value > maximumValue Then
			Value = maximumValue
		End If
		_viewContentRatio = CSng(ViewSize) / CSng(Maximum)
		Dim viewAreaSize As Integer = Maximum - ViewSize
		Dim positionRatio As Double = CSng(Value) / CSng(viewAreaSize)

		If _scrollOrientation = DarkScrollOrientation.Vertical Then
			Dim thumbSize As Integer = CInt((_trackArea.Height * _viewContentRatio))
			If thumbSize < Consts.MinimumThumbSize Then
				thumbSize = Consts.MinimumThumbSize
			End If
			Dim trackAreaSize As Integer = _trackArea.Height - thumbSize
			Dim thumbPosition As Integer = CInt((trackAreaSize * positionRatio))
			'_thumbArea = New Rectangle(_trackArea.Left + 3, _trackArea.Top + thumbPosition, Consts.ScrollBarSize - 6, thumbSize)
			_thumbArea = New Rectangle(_trackArea.Left + CInt(Math.Truncate(0.5 * (Consts.ScrollBarSize - Me.theThumbWidth))), _trackArea.Top + thumbPosition, Me.theThumbWidth, thumbSize)
			'If Me.theRightAndBottomPaddingColorIsUsed Then
			'	_thumbArea.X += 1
			'End If
		ElseIf _scrollOrientation = DarkScrollOrientation.Horizontal Then
			Dim thumbSize As Integer = CInt((_trackArea.Width * _viewContentRatio))
			If thumbSize < Consts.MinimumThumbSize Then
				thumbSize = Consts.MinimumThumbSize
			End If
			Dim trackAreaSize As Integer = _trackArea.Width - thumbSize
			Dim thumbPosition As Integer = CInt((trackAreaSize * positionRatio))
			'_thumbArea = New Rectangle(_trackArea.Left + thumbPosition, _trackArea.Top + 3, thumbSize, Consts.ScrollBarSize - 6)
			_thumbArea = New Rectangle(_trackArea.Left + thumbPosition, _trackArea.Top + CInt(Math.Truncate(0.5 * (Consts.ScrollBarSize - Me.theThumbWidth))), thumbSize, Me.theThumbWidth)
			'If Me.theRightAndBottomPaddingColorIsUsed Then
			'	_thumbArea.Y += 1
			'End If
		End If

		If forceRefresh Then
			Invalidate()
			Update()
		End If
	End Sub

	'Private Sub OnNonClientCalcSize(ByRef m As Message)
	'	If CInt(m.WParam) = 0 Then
	'		Dim rect As Win32Api.RECT = CType(Marshal.PtrToStructure(m.LParam, GetType(Win32Api.RECT)), Win32Api.RECT)
	'		If Me._scrollOrientation = DarkScrollOrientation.Vertical Then
	'			rect.Right += 1
	'		Else
	'			rect.Bottom += 1
	'		End If
	'		Marshal.StructureToPtr(rect, m.LParam, False)
	'		m.Result = IntPtr.Zero
	'	ElseIf CInt(m.WParam) = 1 Then
	'		Dim nccsp As Win32Api.NCCALCSIZE_PARAMS = CType(Marshal.PtrToStructure(m.LParam, GetType(Win32Api.NCCALCSIZE_PARAMS)), Win32Api.NCCALCSIZE_PARAMS)
	'		If Me._scrollOrientation = DarkScrollOrientation.Vertical Then
	'			nccsp.rect0.Right += 1
	'		Else
	'			nccsp.rect0.Bottom += 1
	'		End If
	'		Marshal.StructureToPtr(nccsp, m.LParam, False)
	'		m.Result = IntPtr.Zero
	'	End If
	'End Sub

#End Region

#Region "Data"

	Class Consts
		' Must use same size as default scrollbar to prevent default one from drawing over custom border (when default larger than custom scrollbar).
		'Public Shared ScrollBarSize As Integer = SystemInformation.VerticalScrollBarWidth
		Public Shared ScrollBarSize As Integer = 11
		Public Shared ArrowButtonSize As Integer = 11
		Public Shared MinimumThumbSize As Integer = 11
		'Public Shared SmallChangeStartDelay As Integer = 500
		Public Shared SmallChangeStartDelay As Integer = 250
	End Class

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
	Private theThumbWidth As Integer = 5
	Private _upArrowArea As Rectangle
	Private _downArrowArea As Rectangle
	Private _thumbHot As Boolean
	Private _upArrowHot As Boolean
	Private _downArrowHot As Boolean
	Private _thumbClicked As Boolean
	Private _upArrowClicked As Boolean
	Private _downArrowClicked As Boolean
	Private _verticalTrackAreaClicked As Boolean
	Private _horizontalTrackAreaClicked As Boolean
	Private _directionFactor As Integer
	Private _isScrolling As Boolean
	Private _initialValue As Integer
	Private _initialContact As Point
	Private _scrollTimer As Timer
	Private theLargeChange As Integer
	Private theSmallChange As Integer
	Private ReadOnly _DefaultCursor As Cursor

	Private theRightAndBottomPaddingColorIsUsed As Boolean
	Private theRightAndBottomPaddingColor As Color
	Private theRightAndBottomBorderWidth As Integer
	Private theRightAndBottomBorderColor As Color

#End Region

End Class
