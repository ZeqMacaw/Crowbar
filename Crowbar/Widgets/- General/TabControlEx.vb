Imports System.ComponentModel

Public Class TabControlEx
	Inherits System.Windows.Forms.TabControl

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

		' Use the system font instead of Visual Studio's default of Microsoft Sans Serif.
		Me.Font = New Font(SystemFonts.MessageBoxFont.Name, 8.25)

		''NOTE: To workaround a bug with TabControl.TabPages.Insert() not inserting, force the handle to be created.
		'Dim h As IntPtr = Me.Handle

		Me.ShowToolTips = True
		Me.HotTrack = True
		Me.theCursorIsOverTabs = False
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
		If TheApp IsNot Nothing Then
			Me.UpdateTheme()
			AddHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
		End If
	End Sub

	Private Sub Free()
		' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
		If TheApp IsNot Nothing Then
			RemoveHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
		End If
	End Sub

#End Region

#Region "Properties"

	'Public Overrides Property BackColor() As Color
	'	Get
	'		Return Me.theBackColor
	'	End Get
	'	Set(ByVal value As Color)
	'		Me.theBackColor = value
	'	End Set
	'End Property

	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Property TabBackColor1() As Color
		Get
			Return Me.theTabBackColor1
		End Get
		Set(ByVal value As Color)
			Me.theTabBackColor1 = value
		End Set
	End Property

	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Property TabBackColor2() As Color
		Get
			Return Me.theTabBackColor2
		End Get
		Set(ByVal value As Color)
			Me.theTabBackColor2 = value
		End Set
	End Property

	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Property SelectedTabBackColor() As Color
		Get
			Return Me.theSelectedTabBackColor
		End Get
		Set(ByVal value As Color)
			Me.theSelectedTabBackColor = value
		End Set
	End Property

	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Property TabPageForeColor() As Color
		Get
			Return Me.theTabPageForeColor
		End Get
		Set(ByVal value As Color)
			Me.theTabPageForeColor = value
		End Set
	End Property

	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Property TabPageBackColor() As Color
		Get
			Return Me.theTabPageBackColor
		End Get
		Set(ByVal value As Color)
			Me.theTabPageBackColor = value
		End Set
	End Property

	'Public Property PlusTabIsShown() As Boolean
	'	Get
	'		Return Me.thePlusTabIsShown
	'	End Get
	'	Set(ByVal value As Boolean)
	'		Me.thePlusTabIsShown = value
	'	End Set
	'End Property

	Private ReadOnly Property ScrollPosition() As Int32
		Get
			Dim multiplier As Int32 = -1
			Dim tabRect As Rectangle
			Do
				tabRect = GetTabRect(multiplier + 1)
				multiplier += 1
			Loop While tabRect.Left < 0 AndAlso multiplier < Me.TabCount
			Return multiplier
		End Get
	End Property

#End Region

#Region "Methods"

#End Region

#Region "Widget Event Handlers"

	'	'Protected Overrides Sub OnControlAdded(ByVal e As ControlEventArgs)
	'	'	If TypeOf e.Control Is TabPage Then
	'	'		Dim page As TabPage = CType(e.Control, Windows.Forms.TabPage)
	'	'		'' Prevent the default inheriting of BackColor.
	'	'		'page.BackColor = Control.DefaultBackColor
	'	'		page.BackColor = Me.theTabPageBackColor
	'	'	End If
	'	'	MyBase.OnControlAdded(e)
	'	'End Sub

	'	'Private Sub TabControlEx_ParentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ParentChanged
	'	'	'If Me.theBackColor = Color.Empty AndAlso Me.Parent IsNot Nothing Then
	'	'	'	Me.theBackColor = Me.Parent.BackColor
	'	'	'End If
	'	'	If Me.theBackColor = Color.Transparent AndAlso Me.Parent IsNot Nothing Then
	'	'		Me.theBackColor = Me.Parent.BackColor
	'	'	End If
	'	'End Sub

	'	Protected Sub blah(ByVal e As System.Windows.Forms.PaintEventArgs)
	'		MyBase.OnPaint(e)
	'		e.Graphics.Clear(BackColor)
	'		Dim r As Rectangle = Me.ClientRectangle
	'		If TabCount <= 0 Then Return
	'		'Draw a custom background for Transparent TabPages
	'		r = SelectedTab.Bounds
	'		Dim sf As New StringFormat
	'		sf.Alignment = StringAlignment.Center
	'		sf.LineAlignment = StringAlignment.Center
	'		Dim DrawFont As New Font(Font.FontFamily, 24, FontStyle.Regular, GraphicsUnit.Pixel)
	'		ControlPaint.DrawStringDisabled(e.Graphics, "Micks Ownerdraw TabControl", DrawFont, BackColor, RectangleF.op_Implicit(r), sf)
	'		DrawFont.Dispose()
	'		'Draw a border around TabPage
	'		r.Inflate(3, 3)
	'		Dim tp As TabPage = TabPages(SelectedIndex)
	'		Dim PaintBrush As New SolidBrush(tp.BackColor)
	'		e.Graphics.FillRectangle(PaintBrush, r)
	'		ControlPaint.DrawBorder(e.Graphics, r, PaintBrush.Color, ButtonBorderStyle.Outset)
	'		'Draw the Tabs
	'		For index As Integer = 0 To TabCount - 1
	'			tp = TabPages(index)
	'			r = GetTabRect(index)
	'			Dim bs As ButtonBorderStyle = ButtonBorderStyle.Outset
	'			If index = SelectedIndex Then bs = ButtonBorderStyle.Inset
	'			PaintBrush.Color = tp.BackColor
	'			e.Graphics.FillRectangle(PaintBrush, r)
	'			ControlPaint.DrawBorder(e.Graphics, r, PaintBrush.Color, bs)
	'			PaintBrush.Color = tp.ForeColor

	'			'Set up rotation for left and right aligned tabs
	'			If Alignment = TabAlignment.Left Or Alignment = TabAlignment.Right Then
	'				Dim RotateAngle As Single = 90
	'				If Alignment = TabAlignment.Left Then RotateAngle = 270
	'				Dim cp As New PointF(r.Left + (r.Width \ 2), r.Top + (r.Height \ 2))
	'				e.Graphics.TranslateTransform(cp.X, cp.Y)
	'				e.Graphics.RotateTransform(RotateAngle)
	'				r = New Rectangle(-(r.Height \ 2), -(r.Width \ 2), r.Height, r.Width)
	'			End If
	'			'Draw the Tab Text
	'			If tp.Enabled Then
	'				e.Graphics.DrawString(tp.Text, Font, PaintBrush, RectangleF.op_Implicit(r), sf)
	'			Else
	'				ControlPaint.DrawStringDisabled(e.Graphics, tp.Text, Font, tp.BackColor, RectangleF.op_Implicit(r), sf)
	'			End If

	'			e.Graphics.ResetTransform()

	'		Next
	'		PaintBrush.Dispose()
	'	End Sub

	'	Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawItemEventArgs)
	'		'NOTE: On TabPages.Insert, sometimes .NET sends an out-of-bounds index.
	'		If e.Index < 0 OrElse e.Index > Me.TabPages.Count - 1 Then
	'			MyBase.OnDrawItem(e)
	'			Exit Sub
	'		End If

	'		Dim tabControlBackColorBrush As Brush = New SolidBrush(Me.BackColor)
	'		Dim tabRect As Rectangle = Me.GetTabRect(e.Index)
	'		'Dim textFont As Font
	'		Dim textBrush As Brush

	'		If (e.State And DrawItemState.Selected) <> 0 Then
	'			' Selected

	'			' Fill right-of-tabs background.
	'			Dim lastTabRect As Rectangle = Me.GetTabRect(Me.TabPages.Count - 1)
	'			Dim rightOfTabsRect As Rectangle = New Rectangle(lastTabRect.X + lastTabRect.Width, lastTabRect.Y - 5, Me.Width - (lastTabRect.X + lastTabRect.Width), lastTabRect.Height + 5)
	'			e.Graphics.FillRectangle(tabControlBackColorBrush, rightOfTabsRect)

	'			'Dim tabBackColorBrush As New SolidBrush(Me.SelectedTab.BackColor)

	'			' Fill tab background.
	'			Dim tabBackColorBrush As New SolidBrush(WidgetHighSelectedBackColor)
	'			tabRect.Height += 1
	'			e.Graphics.FillRectangle(tabBackColorBrush, tabRect)
	'			tabRect.Height -= 1
	'			tabBackColorBrush.Dispose()

	'			'' Fill tab text background.
	'			''Dim tabTextBackColorBrush As New SolidBrush(Me.theSelectedTabBackColor)
	'			'Dim tabTextRect As New Rectangle(tabRect.X + 1, tabRect.Y + 1, tabRect.Width - 2, tabRect.Height - 2)
	'			'e.Graphics.FillRectangle(SystemBrushes.Highlight, tabTextRect)
	'			''tabTextBackColorBrush.Dispose()

	'			' Fill tab page background.
	'			'Dim tabPageBackColorBrush As New SolidBrush(Me.theTabPageBackColor)
	'			Dim tabPageBackColorBrush As New SolidBrush(Color.Red)
	'			Dim tabPageRect As Rectangle = Me.DisplayRectangle
	'			'tabPageRect.X -= 3
	'			'tabPageRect.Y -= 1
	'			'tabPageRect.Width += 5
	'			'tabPageRect.Height += 3
	'			e.Graphics.FillRectangle(tabPageBackColorBrush, tabPageRect)
	'			tabPageBackColorBrush.Dispose()

	'			e.DrawFocusRectangle()
	'			'textFont = New System.Drawing.Font(Me.Font, FontStyle.Bold)
	'			textBrush = SystemBrushes.ControlText
	'		Else
	'			' Normal

	'			' Fill left-of-tabs and above-tabs background.
	'			Dim aboveTabsRect As Rectangle = New Rectangle(e.Bounds.Left - 2, e.Bounds.Top - 2, e.Bounds.Width + 4, 2)
	'			e.Graphics.FillRectangle(tabControlBackColorBrush, aboveTabsRect)
	'			If e.Index = 0 Then
	'				Dim leftOfTabsRect As Rectangle = New Rectangle(e.Bounds.Left - 4, e.Bounds.Top - 2, 2, e.Bounds.Height + 4)
	'				e.Graphics.FillRectangle(tabControlBackColorBrush, leftOfTabsRect)
	'			End If

	'			' Fill tab background.
	'			Dim tabBackColorBrush As New Drawing2D.LinearGradientBrush(tabRect, Me.theTabBackColor1, Me.theTabBackColor2, Drawing2D.LinearGradientMode.Vertical)
	'			e.Graphics.FillRectangle(tabBackColorBrush, tabRect)
	'			tabBackColorBrush.Dispose()

	'			'textFont = Me.Font
	'			textBrush = SystemBrushes.ControlText
	'		End If
	'		If tabRect.Contains(Me.PointToClient(Windows.Forms.Cursor.Position)) AndAlso Me.MouseIsOverTabs AndAlso Me.HotTrack Then
	'			Dim tabPageHotTrackBrush As New SolidBrush(SystemColors.ButtonHighlight)
	'			'Dim tabPageHotTrackPen As New Pen(SystemColors.InactiveCaption)
	'			Dim hotTrackRect As Rectangle = Me.GetTabRect(e.Index)
	'			hotTrackRect.X += 3
	'			hotTrackRect.Y += 2
	'			hotTrackRect.Width -= 6
	'			hotTrackRect.Height -= 3
	'			e.Graphics.FillRectangle(tabPageHotTrackBrush, hotTrackRect)
	'			'e.Graphics.DrawRectangle(tabPageHotTrackPen, hotTrackRect)
	'			'tabPageHotTrackPen.Dispose()
	'			tabPageHotTrackBrush.Dispose()
	'			'textBrush = SystemBrushes.HotTrack
	'		End If

	'		'Dim theStringFormat As New StringFormat
	'		'theStringFormat.Alignment = StringAlignment.Center
	'		'theStringFormat.LineAlignment = StringAlignment.Center
	'		'theStringFormat.FormatFlags = StringFormatFlags.NoWrap
	'		'e.Graphics.DrawString(Me.TabPages(e.Index).Text, Me.Font, textBrush, tabRect, theStringFormat)
	'		'theStringFormat.Dispose()
	'		'------
	'		TextRenderer.DrawText(e.Graphics, Me.TabPages(e.Index).Text, Me.Font, tabRect, Me.theTabPageForeColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)

	'		'NOTE: Moved the setting of tooltip text outside of this event, because it sometimes causes exception.
	'		'If e.Graphics.MeasureString(Me.TabPages(e.Index).Text, Me.Font).Width > tabRect.Width Then
	'		'	'tabRect.X += 10
	'		'	'tabRect.Width -= 20
	'		'	'tabRect.Width += 50
	'		'	'theStringFormat.Alignment = StringAlignment.Near
	'		'	'theStringFormat.LineAlignment = StringAlignment.Near
	'		'	'theStringFormat.Trimming = StringTrimming.EllipsisCharacter
	'		'	Me.TabPages(e.Index).ToolTipText = Me.TabPages(e.Index).Text
	'		'Else
	'		'	Me.TabPages(e.Index).ToolTipText = ""
	'		'End If

	'		tabControlBackColorBrush.Dispose()

	'		MyBase.OnDrawItem(e)
	'	End Sub

	Protected Overrides Sub OnHandleCreated(ByVal e As System.EventArgs)
		MyBase.OnHandleCreated(e)
		' [04-Feb-2026] Me.DesignMode is unreliable in nested widgets.
		'If Not Me.DesignMode Then
		Me.Init()
		'End If

		If Me.Multiline = False Then
			Scroller.Font = New Font("Marlett", Me.Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, Me.Font.GdiCharSet)
			Win32Api.SetParent(Scroller.Handle, Me.Handle)
		End If

		Me.OnFontChanged(EventArgs.Empty)
	End Sub

	Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
		Me.Free()
		MyBase.OnHandleDestroyed(e)
	End Sub

	'	'NOTE: Right-clicking a tab selects it. (OnMouseClick didn't run for right-click.)
	'	Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
	'		If e.Button = Windows.Forms.MouseButtons.Right Then
	'			For i As Integer = 0 To Me.TabCount - 1
	'				If Me.GetTabRect(i).Contains(e.Location) Then
	'					Me.SelectedIndex = i
	'					Exit For
	'				End If
	'			Next i
	'		End If
	'		MyBase.OnMouseDown(e)
	'	End Sub

	'Draw the tab page and the tab items.
	' Need the following line for OnPaint() to be called by Windows:
	'	Me.SetStyle(ControlStyles.UserPaint, True)
	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		If Me.TabCount > 0 Then
			Dim theme As TabControlTheme = Nothing
			' This check prevents problems with viewing and saving Forms in VS Designer.
			If TheApp IsNot Nothing Then
				theme = TheApp.Settings.SelectedAppTheme.TabControlTheme
			End If
			If theme IsNot Nothing Then
				Me.theTabControlBackColor = theme.EnabledBackColor
				Me.theTabBackColor1 = theme.EnabledBackColor
				Me.theTabBackColor2 = theme.EnabledBackColor
				Me.theSelectedTabBackColor = theme.SelectedBackColor
				Me.theTabPageForeColor = theme.EnabledForeColor
				Me.theTabPageBackColor = theme.EnabledBackColor
			End If

			' Draw TabControl background.
			Using tabControlBackColorBrush As New SolidBrush(Me.theTabControlBackColor)
				Dim clipRect As Rectangle = e.ClipRectangle
				e.Graphics.FillRectangle(tabControlBackColorBrush, clipRect)
			End Using

			Dim redChannel As Byte = 0
			Dim greenChannel As Byte = 0
			Dim blueChannel As Byte = 0
			For index As Integer = 0 To Me.TabCount - 1
				Dim tabRect As Rectangle = Me.GetTabRect(index)

				If index = Me.SelectedIndex Then
					' Draw tab background.
					Using tabBackColorBrush As New SolidBrush(Me.theSelectedTabBackColor)
						tabRect.Height += 1
						e.Graphics.FillRectangle(tabBackColorBrush, tabRect)
						tabRect.Height -= 1
					End Using

					TextRenderer.DrawText(e.Graphics, Me.TabPages(index).Text, Me.Font, tabRect, Me.theTabPageForeColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)
				Else
					' Normal

					' Draw tab background.
					Using tabBackColorBrush As New SolidBrush(Me.theTabBackColor1)
						tabRect.Height += 1
						e.Graphics.FillRectangle(tabBackColorBrush, tabRect)
						tabRect.Height -= 1
					End Using
					If tabRect.Contains(Me.PointToClient(Windows.Forms.Cursor.Position)) AndAlso Me.theCursorIsOverTabs AndAlso Me.HotTrack Then
						' The '+ 30' makes the color slightly brighter.
						'redChannel = CByte(Math.Min(255, Me.theSelectedTabBackColor.R + 30))
						'greenChannel = CByte(Math.Min(255, Me.theSelectedTabBackColor.G + 30))
						'blueChannel = CByte(Math.Min(255, Me.theSelectedTabBackColor.B + 30))
						'Dim trackColor As Color = Color.FromArgb(redChannel, greenChannel, blueChannel)
						'Using tabPageHotTrackBrush As New SolidBrush(trackColor)
						'	e.Graphics.FillRectangle(tabPageHotTrackBrush, tabRect)
						'End Using
						Using tabPageHotTrackBrush As New Drawing2D.LinearGradientBrush(tabRect, Me.theSelectedTabBackColor, WidgetHighBackColor, Drawing2D.LinearGradientMode.Vertical)
							'tabRect.Height += 1
							e.Graphics.FillRectangle(tabPageHotTrackBrush, tabRect)
							'tabRect.Height -= 1
						End Using
					End If

					TextRenderer.DrawText(e.Graphics, Me.TabPages(index).Text, Me.Font, tabRect, Me.theTabPageForeColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)

				End If
			Next

			Dim borderRect As Rectangle = Me.TabPages(0).Bounds
			If Me.SelectedTab IsNot Nothing Then
				borderRect = Me.SelectedTab.Bounds
			End If

			' Draw a thin border at left, right, and bottom sides of the TabControl.
			'Using thinBorderPen As New Pen(WidgetHighDisabledBackColor)
			Using thinBorderPen As New Pen(Me.theTabBackColor1, 1)
				Dim thinBorderRect As Rectangle = borderRect
				thinBorderRect.Inflate(Me.Padding.X, Me.Padding.Y)
				e.Graphics.DrawRectangle(thinBorderPen, thinBorderRect)
			End Using

			' Draw a border at top side of the TabPages (i.e. along bottom of tabs) that matches color of selected tab.
			' This will cover the top of the thinBorderRect.
			Using tabBackColorBrush As New SolidBrush(Me.theSelectedTabBackColor)
				Dim topBorderRect As Rectangle = borderRect
				topBorderRect.Inflate(Me.Padding.X, 0)
				topBorderRect.Y = Me.GetTabRect(0).Bottom - 1
				topBorderRect.Height = 2
				e.Graphics.FillRectangle(tabBackColorBrush, topBorderRect)
			End Using
		End If
	End Sub

	'	'Protected Overrides Sub OnPaint(e As PaintEventArgs)
	'	'	'	We must always paint the entire area of the tab control
	'	'	If e.ClipRectangle.Equals(Me.ClientRectangle) Then
	'	'		Me.CustomPaint(e.Graphics)
	'	'	Else
	'	'		'	it is less intensive to just reinvoke the paint with the whole surface available to draw on.
	'	'		Me.Invalidate()
	'	'	End If
	'	'End Sub

	'	'Public Const WM_PAINT As Integer = &HF
	'	'Public Const WM_ERASEBKGND As Integer = &H14
	'	'Public Const WM_NCPAINT As Integer = &H85

	'	'Public Structure RECT
	'	'	Public Left As Integer
	'	'	Public Top As Integer
	'	'	Public Right As Integer
	'	'	Public Bottom As Integer
	'	'End Structure

	'	'Public Declare Function GetUpdateRect Lib "user32" (ByVal hWnd As System.IntPtr, ByRef rc As RECT, ByRef fErase As Boolean) As Boolean

	'	'Protected Overrides Sub WndProc(ByRef m As Message)
	'	'	'If m.Msg = WM_ERASEBKGND Then
	'	'	'	Dim updateRect As RECT = New RECT()
	'	'	'	If GetUpdateRect(m.HWnd, updateRect, False) Then
	'	'	'		Dim g As Graphics = Me.CreateGraphics()
	'	'	'		Dim e As New PaintEventArgs(g, Rectangle.FromLTRB(updateRect.Left, updateRect.Top, updateRect.Right, updateRect.Bottom))
	'	'	'		Me.OnPaintBackground(e)
	'	'	'		g.Dispose()
	'	'	'		'm.Result = CType(1, IntPtr)
	'	'	'		m.Msg = 0
	'	'	'	Else
	'	'	'		MyBase.WndProc(m)
	'	'	'	End If
	'	'	'Else
	'	'	'	MyBase.WndProc(m)
	'	'	'End If
	'	'	'------
	'	'	'Dim text As String = ""
	'	'	'If m.Msg = WM_PAINT Then
	'	'	'	text = Me.TabPages(Me.SelectedIndex).Text
	'	'	'	Me.TabPages(Me.SelectedIndex).Text = ""
	'	'	'End If
	'	'	'MyBase.WndProc(m)
	'	'	'If m.Msg = WM_PAINT Then
	'	'	'	Me.TabPages(Me.SelectedIndex).Text = text
	'	'	'End If
	'	'	'------
	'	'	'If m.Msg = WM_NCPAINT AndAlso m.WParam <> CType(1, IntPtr) Then
	'	'	'	Dim tabAreaRect As New Rectangle(Me.ClientRectangle.X, Me.ClientRectangle.Y, Me.ClientRectangle.Width, Me.ClientRectangle.Height - Me.DisplayRectangle.Height)
	'	'	'	Dim tabAreaRegion As New Region(tabAreaRect)
	'	'	'	Dim point As Point
	'	'	'	point = Me.PointToScreen(Me.Location)
	'	'	'	tabAreaRegion.Translate(point.X, point.Y)
	'	'	'	Dim hrgn As Region = Drawing.Region.FromHrgn(m.WParam)
	'	'	'	hrgn.Intersect(tabAreaRegion)
	'	'	'	Dim g As Graphics = Me.CreateGraphics()
	'	'	'	Dim updateRect As Rectangle = Rectangle.Truncate(hrgn.GetBounds(g))
	'	'	'	If Not (hrgn.IsEmpty(g)) Then
	'	'	'		Dim e As New PaintEventArgs(g, updateRect)
	'	'	'		Me.OnPaintBackground(e)
	'	'	'		'Me.Invalidate(hrgn)
	'	'	'	Else
	'	'	'		MyBase.WndProc(m)
	'	'	'	End If
	'	'	'	g.Dispose()
	'	'	'Else
	'	'	'	MyBase.WndProc(m)
	'	'	'End If
	'	'	'------
	'	'	'DEBUG: Only use try-catch here for debugging, because it might cause program to run too slowly.
	'	'	'Try
	'	'	If m.Msg = WM_NCPAINT AndAlso m.WParam <> CType(1, IntPtr) Then
	'	'		Dim tabAreaRect As New Rectangle(Me.ClientRectangle.X, Me.ClientRectangle.Y, Me.ClientRectangle.Width, Me.ClientRectangle.Height - Me.DisplayRectangle.Height)
	'	'		Dim tabAreaRegion As New Region(tabAreaRect)
	'	'		Dim point As Point
	'	'		point = Me.PointToScreen(Me.Location)
	'	'		tabAreaRegion.Translate(point.X, point.Y)
	'	'		Dim g As Graphics = Me.CreateGraphics()
	'	'		If Not (tabAreaRegion.IsEmpty(g)) AndAlso Me.SelectedIndex >= 0 Then
	'	'			Me.Invalidate(Me.GetTabRect(Me.SelectedIndex))
	'	'			MyBase.WndProc(m)
	'	'		Else
	'	'			MyBase.WndProc(m)
	'	'		End If
	'	'		g.Dispose()
	'	'	Else
	'	'		MyBase.WndProc(m)
	'	'	End If
	'	'	'Catch ex As System.ComponentModel.Win32Exception
	'	'	'	Dim i As Integer = 42
	'	'	'Catch ex As Exception
	'	'	'End Try
	'	'End Sub

	Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
		Me.theCursorIsOverTabs = True
	End Sub

	Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
		Me.theCursorIsOverTabs = False
	End Sub

	'	'Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
	'	'	MyBase.OnResize(e)

	'	'	'Me.Refresh()
	'	'	For Each tabPage As TabPage In Me.TabPages
	'	'		tabPage.Update()
	'	'	Next
	'	'End Sub

	'	'Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
	'	'	If Me.DesignMode Then
	'	'		'' If this is in the designer let's put a nice gradient on the back
	'	'		'' By default the tabcontrol has a fixed grey background. Yuck!
	'	'		'Dim backBrush As New LinearGradientBrush( _
	'	'		'	Me.Bounds, _
	'	'		'	SystemColors.ControlLightLight, _
	'	'		'	SystemColors.ControlLight, _
	'	'		'	Drawing2D.LinearGradientMode.Vertical)
	'	'		'pevent.Graphics.FillRectangle(backBrush, Me.Bounds)
	'	'		'backBrush.Dispose()
	'	'	Else
	'	'		' At runtime we want a transparent background.
	'	'		' So let's paint the containing control (there has to be one).
	'	'		Me.InvokePaintBackground(Me.Parent, e)
	'	'		Me.InvokePaint(Me.Parent, e)
	'	'		'------
	'	'		'If TabPages.Count > 0 Then
	'	'		'	Dim tabControlBackColorBrush As Brush = New SolidBrush(Me.BackColor)
	'	'		'	' Fill right-of-tabs background.
	'	'		'	Dim lastTabRect As Rectangle = Me.GetTabRect(Me.TabPages.Count - 1)
	'	'		'	Dim rightOfTabsRect As Rectangle = New Rectangle(lastTabRect.X + lastTabRect.Width, lastTabRect.Y - 5, Me.Width - (lastTabRect.X + lastTabRect.Width), lastTabRect.Height + 5)
	'	'		'	e.Graphics.FillRectangle(tabControlBackColorBrush, rightOfTabsRect)
	'	'		'	tabControlBackColorBrush.Dispose()
	'	'		'End If
	'	'		'------
	'	'		'Dim gc As Drawing2D.GraphicsContainer = e.Graphics.BeginContainer
	'	'		'Dim TranslateRect As Rectangle = New Rectangle(Me.Location, Me.Size)
	'	'		'Dim pe As New PaintEventArgs(e.Graphics, TranslateRect)
	'	'		'e.Graphics.TranslateTransform(-Me.Left, -Me.Top)
	'	'		'Me.InvokePaintBackground(Me.Parent, pe)
	'	'		'Me.InvokePaint(Me.Parent, pe)
	'	'		'e.Graphics.ResetTransform()
	'	'		'e.Graphics.EndContainer(gc)
	'	'	End If
	'	'End Sub

	'	'=============================================================================================

	'	'''NOTE: OnPaint and OnPaintBackground are not called by TabControl.
	'	'Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
	'	'	'	'Dim theBackColorBrush As New SolidBrush(SystemColors.InactiveCaption)
	'	'	'	'e.Graphics.FillRectangle(theBackColorBrush, e.ClipRectangle)
	'	'	'	'theBackColorBrush.Dispose()
	'	'	'	'------
	'	'	'	'MyBase.OnPaint(e)
	'	'	'	Me.Invalidate()
	'	'	'	' Fill right-of-tabs background.
	'	'	'	Dim tabControlBackColorBrush As Brush = New SolidBrush(Me.BackColor)
	'	'	'	Dim lastTabRect As Rectangle = Me.GetTabRect(Me.TabPages.Count - 1)
	'	'	'	Dim rightOfTabsRect As Rectangle = New Rectangle(lastTabRect.X + lastTabRect.Width, lastTabRect.Y - 5, Me.Width - (lastTabRect.X + lastTabRect.Width), lastTabRect.Height + 5)
	'	'	'	e.Graphics.FillRectangle(tabControlBackColorBrush, rightOfTabsRect)
	'	'	'	tabControlBackColorBrush.Dispose()
	'	'	'======

	'	'	'   Paint all the tabs
	'	'	'Me.Padding = New Point(0, 0)
	'	'	If Me.TabCount > 0 Then
	'	'		For index As Integer = 0 To Me.TabCount - 1
	'	'			Me.PaintTab(e, index)
	'	'		Next
	'	'	End If

	'	'	'   paint a border round the pagebox
	'	'	'   We can't make the border disappear so have to do it like this.
	'	'	If Me.TabCount > 0 Then
	'	'		Dim borderRect As Rectangle = Me.TabPages(0).Bounds
	'	'		If Me.SelectedTab IsNot Nothing Then
	'	'			borderRect = Me.SelectedTab.Bounds
	'	'		End If
	'	'		'borderRect.Inflate(Me.Padding.X, Me.Padding.Y)
	'	'		'ControlPaint.DrawBorder(e.Graphics, borderRect, ThemedColors.ToolBorder, ButtonBorderStyle.Solid)
	'	'		ControlPaint.DrawBorder3D(e.Graphics, borderRect, Border3DStyle.Raised, Border3DSide.All)
	'	'	End If
	'	'End Sub

	'	'Private Sub PaintTab(ByVal e As System.Windows.Forms.PaintEventArgs, ByVal index As Integer)
	'	'	Dim tabControlBackColorBrush As Brush = New SolidBrush(Me.BackColor)
	'	'	Dim tabRect As Rectangle = Me.GetTabRect(index)
	'	'	'Dim textFont As Font
	'	'	Dim textBrush As Brush

	'	'	'tabRect.Inflate(Me.Padding.X, Me.Padding.Y)
	'	'	If index = Me.SelectedIndex Then
	'	'		' Selected

	'	'		'' Fill right-of-tabs background.
	'	'		'Dim lastTabRect As Rectangle = Me.GetTabRect(Me.TabPages.Count - 1)
	'	'		'Dim rightOfTabsRect As Rectangle = New Rectangle(lastTabRect.X + lastTabRect.Width, lastTabRect.Y - 5, Me.Width - (lastTabRect.X + lastTabRect.Width), lastTabRect.Height + 5)
	'	'		'e.Graphics.FillRectangle(tabControlBackColorBrush, rightOfTabsRect)

	'	'		'Dim tabBackColorBrush As New SolidBrush(Me.SelectedTab.BackColor)

	'	'		' Fill tab background.
	'	'		Dim tabBackColorBrush As New SolidBrush(Me.theSelectedTabBackColor)
	'	'		tabRect.Height += 1
	'	'		e.Graphics.FillRectangle(tabBackColorBrush, tabRect)
	'	'		tabRect.Height -= 1
	'	'		tabBackColorBrush.Dispose()

	'	'		'' Fill tab text background.
	'	'		''Dim tabTextBackColorBrush As New SolidBrush(Me.theSelectedTabBackColor)
	'	'		'Dim tabTextRect As New Rectangle(tabRect.X + 1, tabRect.Y + 1, tabRect.Width - 2, tabRect.Height - 2)
	'	'		'e.Graphics.FillRectangle(SystemBrushes.Highlight, tabTextRect)
	'	'		''tabTextBackColorBrush.Dispose()

	'	'		'' Fill tab page background.
	'	'		'Dim tabPageBackColorBrush As New SolidBrush(Me.theTabPageBackColor)
	'	'		'Dim tabPageRect As Rectangle = Me.DisplayRectangle
	'	'		'tabPageRect.X -= 3
	'	'		'tabPageRect.Y -= 1
	'	'		'tabPageRect.Width += 5
	'	'		'tabPageRect.Height += 3
	'	'		''e.Graphics.FillRectangle(tabBackColorBrush, tabPageRect)
	'	'		'e.Graphics.FillRectangle(tabPageBackColorBrush, tabPageRect)
	'	'		'tabPageBackColorBrush.Dispose()

	'	'		'tabBackColorBrush.Dispose()

	'	'		'e.DrawFocusRectangle()
	'	'		'textFont = New System.Drawing.Font(Me.Font, FontStyle.Bold)
	'	'		textBrush = SystemBrushes.ControlText
	'	'	Else
	'	'		' Normal

	'	'		'' Fill left-of-tabs and above-tabs background.
	'	'		''Dim aboveTabsRect As Rectangle = New Rectangle(e.Bounds.Left - 2, e.Bounds.Top - 2, e.Bounds.Width + 4, 2)
	'	'		'Dim aboveTabsRect As Rectangle = New Rectangle(tabRect.Left - 2, tabRect.Top - 2, tabRect.Width + 4, 2)
	'	'		'e.Graphics.FillRectangle(tabControlBackColorBrush, aboveTabsRect)
	'	'		'If index = 0 Then
	'	'		'	'Dim leftOfTabsRect As Rectangle = New Rectangle(e.Bounds.Left - 4, e.Bounds.Top - 2, 2, e.Bounds.Height + 4)
	'	'		'	Dim leftOfTabsRect As Rectangle = New Rectangle(tabRect.Left - 4, tabRect.Top - 2, 2, tabRect.Height + 4)
	'	'		'	e.Graphics.FillRectangle(tabControlBackColorBrush, leftOfTabsRect)
	'	'		'End If

	'	'		' Fill tab background.
	'	'		Dim tabBackColorBrush As New Drawing2D.LinearGradientBrush(tabRect, Me.theTabBackColor1, Me.theTabBackColor2, Drawing2D.LinearGradientMode.Vertical)
	'	'		e.Graphics.FillRectangle(tabBackColorBrush, tabRect)
	'	'		tabBackColorBrush.Dispose()

	'	'		'textFont = Me.Font
	'	'		textBrush = SystemBrushes.ControlText

	'	'		' Draw tab border.
	'	'		Dim points As Point() = { _
	'	'		 New Point(tabRect.Left, tabRect.Bottom) _
	'	'		 , New Point(tabRect.Left, tabRect.Top) _
	'	'		 , New Point(tabRect.Right, tabRect.Top) _
	'	'		 , New Point(tabRect.Right, tabRect.Bottom) _
	'	'		}
	'	'		e.Graphics.DrawLines(SystemPens.ControlText, points)
	'	'	End If
	'	'	If tabRect.Contains(Me.PointToClient(Windows.Forms.Cursor.Position)) AndAlso Me.HotTrack Then
	'	'		Dim tabPageHotTrackBrush As New SolidBrush(SystemColors.ButtonHighlight)
	'	'		'Dim tabPageHotTrackPen As New Pen(SystemColors.InactiveCaption)
	'	'		Dim hotTrackRect As Rectangle = Me.GetTabRect(index)
	'	'		hotTrackRect.X += 3
	'	'		hotTrackRect.Y += 2
	'	'		hotTrackRect.Width -= 6
	'	'		hotTrackRect.Height -= 3
	'	'		e.Graphics.FillRectangle(tabPageHotTrackBrush, hotTrackRect)
	'	'		'e.Graphics.DrawRectangle(tabPageHotTrackPen, hotTrackRect)
	'	'		'tabPageHotTrackPen.Dispose()
	'	'		tabPageHotTrackBrush.Dispose()
	'	'		'textBrush = SystemBrushes.HotTrack
	'	'	End If

	'	'	Dim theStringFormat As New StringFormat
	'	'	theStringFormat.Alignment = StringAlignment.Center
	'	'	theStringFormat.LineAlignment = StringAlignment.Center
	'	'	e.Graphics.DrawString(Me.TabPages(index).Text, Me.Font, textBrush, tabRect, theStringFormat)
	'	'	'e.Graphics.DrawString(Me.TabPages(e.Index).Text, textFont, textBrush, tabRect, theStringFormat)
	'	'	theStringFormat.Dispose()

	'	'	tabControlBackColorBrush.Dispose()
	'	'End Sub

	'	''NOTE: OnPaintBackground is not called by TabControl.
	'	'Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
	'	'	'	' Fill right-of-tabs background.
	'	'	'	Dim tabControlBackColorBrush As Brush = New SolidBrush(Me.BackColor)
	'	'	'	Dim lastTabRect As Rectangle = Me.GetTabRect(Me.TabPages.Count - 1)
	'	'	'	Dim rightOfTabsRect As Rectangle = New Rectangle(lastTabRect.X + lastTabRect.Width, lastTabRect.Y - 5, Me.Width - (lastTabRect.X + lastTabRect.Width), lastTabRect.Height + 5)
	'	'	'	e.Graphics.FillRectangle(tabControlBackColorBrush, rightOfTabsRect)
	'	'	'	tabControlBackColorBrush.Dispose()
	'	'	'======
	'	'	If Me.DesignMode Then
	'	'		'' If this is in the designer let's put a nice gradient on the back
	'	'		'' By default the tabcontrol has a fixed grey background. Yuck!
	'	'		'Dim backBrush As New LinearGradientBrush( _
	'	'		'	Me.Bounds, _
	'	'		'	SystemColors.ControlLightLight, _
	'	'		'	SystemColors.ControlLight, _
	'	'		'	Drawing2D.LinearGradientMode.Vertical)
	'	'		'pevent.Graphics.FillRectangle(backBrush, Me.Bounds)
	'	'		'backBrush.Dispose()
	'	'	Else
	'	'		' At runtime we want a transparent background.
	'	'		' So let's paint the containing control (there has to be one).
	'	'		Me.InvokePaintBackground(Me.Parent, e)
	'	'		Me.InvokePaint(Me.Parent, e)
	'	'	End If
	'	'End Sub

	'#Region "DeeplyNestedWorkaround"

	'	'Public Delegate Sub SizeChangedDelegate(ByVal e As EventArgs)

	'	'Private Sub SizeChangedDelegateHandler(ByVal e As EventArgs)
	'	'	MyBase.OnSizeChanged(e)
	'	'End Sub

	'	'Protected Overrides Sub OnSizeChanged(e As EventArgs)
	'	'	If Me.Handle.ToInt32 > 0 Then
	'	'		Dim obj(0) As Object
	'	'		obj(0) = e
	'	'		Dim scd As New SizeChangedDelegate(AddressOf SizeChangedDelegateHandler)
	'	'		Me.BeginInvoke(scd, obj)
	'	'	End If
	'	'End Sub

	'#End Region

	Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
		MyBase.OnFontChanged(e)

		' These two lines prevent incorrectly drawn text in the tabs when switching to manually painted TanControl.
		'FROM: https://stackoverflow.com/questions/7128996/owner-drawn-tabcontrol-has-wider-tabs
		Dim hFont As IntPtr = Me.Font.ToHfont()
		Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_SETFONT, hFont, New IntPtr(-1))

		Me.Scroller.Font = New Font("Marlett", Me.Font.SizeInPoints, FontStyle.Regular, GraphicsUnit.Point)
		' The '-1' prevents writing over the border at bottom of tabs.
		Me.Scroller.Height = Me.ItemSize.Height - 1
		Me.Scroller.Width = Me.ItemSize.Height * 2
		'Me.Scroller.Width = Me.ItemSize.Height * 3

		Me.OnResize(EventArgs.Empty)
	End Sub

	Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
		MyBase.OnResize(e)
		Invalidate(True)
		If Me.Multiline Then
			Return
		End If
		If Me.Alignment = TabAlignment.Top Then
			Scroller.Location = New Point(Me.Width - Scroller.Width, 2)
		Else
			Scroller.Location = New Point(Me.Width - Scroller.Width, Me.Height - Scroller.Height - 2)
		End If
	End Sub

	Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
		If m.Msg = Win32Api.WindowsMessages.WM_PARENTNOTIFY Then
			If (m.WParam.ToInt32() And &HFFFF) = Win32Api.WindowsMessages.WM_CREATE Then
				Dim WindowName As New System.Text.StringBuilder(16)
				Win32Api.RealGetWindowClass(m.LParam, WindowName, 16)
				If WindowName.ToString = "msctls_updown32" Then
					'unhook the existing updown control as it will be recreated if 
					'the tabcontrol is recreated (alignment, visible changed etc..)
					If UPDown IsNot Nothing Then
						UPDown.ReleaseHandle()
					End If
					'and hook it.
					UPDown = New NativeUpDown
					UPDown.AssignHandle(m.LParam)
				End If
			End If
		End If

		MyBase.WndProc(m)
	End Sub

#End Region

#Region "Child Widget Event Handlers"

#Region "Handle custom scrollbar (updowncontrol)"

	'FROM: Mick Doherty's TabControl Tips
	'      Add a custom Scroller to Tabcontrol.
	'      https://dotnetrix.co.uk/tabcontrol.htm#tip15

	Private Sub Scroller_ScrollLeft(ByVal sender As Object, ByVal e As System.EventArgs) Handles Scroller.ScrollLeft
		If Me.TabCount = 0 Then
			Return
		End If
		Dim scrollPos As Int32 = Math.Max(0, (ScrollPosition - 1) * &H10000)
		Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, IntPtr.op_Explicit(scrollPos Or &H4), IntPtr.Zero)
		Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, IntPtr.op_Explicit(scrollPos Or &H8), IntPtr.Zero)
	End Sub

	Private Sub Scroller_ScrollRight(ByVal sender As Object, ByVal e As System.EventArgs) Handles Scroller.ScrollRight
		If Me.TabCount = 0 Then
			Return
		End If
		If GetTabRect(Me.TabCount - 1).Right <= Me.Scroller.Left Then Return
		Dim scrollPos As Int32 = Math.Max(0, (ScrollPosition + 1) * &H10000)
		Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, IntPtr.op_Explicit(scrollPos Or &H4), IntPtr.Zero)
		Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, IntPtr.op_Explicit(scrollPos Or &H8), IntPtr.Zero)
	End Sub

	'Private Sub Scroller_TabClose(ByVal sender As Object, ByVal e As System.EventArgs) Handles Scroller.TabClose
	'	If Me.SelectedTab IsNot Nothing Then
	'		Me.TabPages.Remove(Me.SelectedTab)
	'	End If
	'End Sub

#End Region

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "AppThemeName" Then
			Me.UpdateTheme()
			Me.Refresh()
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateTheme()
		Dim theme As TabControlTheme = Nothing
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.TabControlTheme
		End If
		If theme IsNot Nothing Then
			Me.SetStyle(ControlStyles.UserPaint, True)
		Else
			Me.SetStyle(ControlStyles.UserPaint, False)
		End If
	End Sub

#End Region

#Region "Data"

	'Private theDefaultPadding As Point
	'Private theBackColor As Color
	Private theTabControlBackColor As Color
	Private theTabBackColor1 As Color
	Private theTabBackColor2 As Color
	Private theSelectedTabBackColor As Color
	Private theTabPageForeColor As Color
	Private theTabPageBackColor As Color
	'Private theRealFont As Font

	'Private thePlusTabIsShown As Boolean

	Dim theCursorIsOverTabs As Boolean

	' Most likely this internal widget is needed to allow scrolling via the WM_HSCROLL message.
	Private UPDown As NativeUpDown
	Private WithEvents Scroller As New TabScroller

#End Region

	Friend Class NativeUpDown
		Inherits NativeWindow

		Public Sub New()
			MyBase.New()
		End Sub

		Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
			If m.Msg = Win32Api.WindowsMessages.WM_DESTROY OrElse m.Msg = Win32Api.WindowsMessages.WM_NCDESTROY Then
				Me.ReleaseHandle()
			ElseIf m.Msg = Win32Api.WindowsMessages.WM_WINDOWPOSCHANGING Then
				'Move the updown control off the edge so it's not visible
				Dim wp As Win32Api.WINDOWPOS = DirectCast(m.GetLParam(GetType(Win32Api.WINDOWPOS)), Win32Api.WINDOWPOS)
				wp.x += wp.cx
				Runtime.InteropServices.Marshal.StructureToPtr(wp, m.LParam, True)
				'_bounds = New Rectangle(wp.x, wp.y, wp.cx, wp.cy)
			End If

			MyBase.WndProc(m)
		End Sub

		'Private _bounds As Rectangle

	End Class

End Class
