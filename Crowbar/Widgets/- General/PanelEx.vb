Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class PanelEx
    Inherits Panel

    'NOTE: DataBind a group of RadioButtons to a DataSource property that is an Integer or an Enum.
    'thisPanelEx.DataBindings.Add("SelectedIndex", theDataSourceThatHasTheIntegerProperty, "NameOfIntegerProperty", False, DataSourceUpdateMode.OnPropertyChanged)
    'thisPanelEx.DataBindings.Add("SelectedValue", theDataSourceThatHasTheEnumProperty, "NameOfEnumProperty", False, DataSourceUpdateMode.OnPropertyChanged)

#Region "Create and Destroy"

    Public Sub New()
        MyBase.New()

        ' Override BorderStyle to allow custom border width.
        MyBase.BorderStyle = BorderStyle.None

        Me.theScrollingIsActive = False

        Me.CustomHorizontalScrollbar = New ScrollBarEx()
        Me.Controls.Add(Me.CustomHorizontalScrollbar)
        'Me.CustomHorizontalScrollbar.Location = New System.Drawing.Point(0, Me.ClientRectangle.Height)
        Me.CustomHorizontalScrollbar.Name = "CustomHorizontalScrollbar"
        'Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.Width, ScrollBarEx.Consts.ScrollBarSize)
        Me.CustomHorizontalScrollbar.ScrollOrientation = ScrollBarEx.DarkScrollOrientation.Horizontal
        Me.CustomHorizontalScrollbar.TabIndex = 7
        Me.CustomHorizontalScrollbar.Text = "CustomHorizontalScrollbar"
        Me.CustomHorizontalScrollbar.Visible = False

        Me.CustomVerticalScrollBar = New ScrollBarEx()
        Me.Controls.Add(Me.CustomVerticalScrollBar)
        'Me.CustomVerticalScrollBar.Location = New System.Drawing.Point(Me.ClientRectangle.Width, 0)
        Me.CustomVerticalScrollBar.Name = "CustomVerticalScrollBar"
        'Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.Height)
        Me.CustomVerticalScrollBar.ScrollOrientation = ScrollBarEx.DarkScrollOrientation.Vertical
        Me.CustomVerticalScrollBar.TabIndex = 7
        Me.CustomVerticalScrollBar.Text = "CustomVerticalScrollBar"
        Me.CustomVerticalScrollBar.Visible = False

        Me.ScrollbarCornerPanel = New ScrollBarPanel()
        Me.Controls.Add(Me.ScrollbarCornerPanel)
        Me.ScrollbarCornerPanel.Name = "ScrollbarCornerPanel"
        Me.ScrollbarCornerPanel.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, ScrollBarEx.Consts.ScrollBarSize)
        Me.ScrollbarCornerPanel.Visible = False

        Me.theControlHasShown = False

        Me.theSelectedIndex = -1
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

    Public ReadOnly Property RadioButtons() As RadioButton()
        Get
            Return Me.theRadioButtonList.ToArray()
        End Get
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SelectedIndex() As Integer
        Get
            Return Me.theSelectedIndex
        End Get
        Set(ByVal value As Integer)
            If value < 0 OrElse value >= Me.theRadioButtonList.Count Then
                Return
            End If
            If value <> Me.theSelectedIndex Then
                Dim radioButton As RadioButton
                radioButton = Me.theRadioButtonList(value)
                radioButton.Checked = True
                Me.SetIndex(value)
            End If
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SelectedValue() As System.Enum
        Get
            Return Me.theSelectedValue
        End Get
        Set(ByVal value As System.Enum)
            'NOTE: This test is needed because Visual Studio Designer sets the property to Nothing in InitializeComponent().
            If value Is Nothing Then
                Return
            End If
            If Not value.Equals(Me.theSelectedValue) Then
                Dim radioButton As RadioButton
                For i As Integer = 0 To Me.theRadioButtonList.Count - 1
                    radioButton = Me.theRadioButtonList(i)
                    If value.Equals(radioButton.Tag) Then
                        radioButton.Checked = True
                        Me.SetValue(CType(radioButton.Tag, System.Enum))
                        Exit For
                    End If
                Next
            End If
        End Set
    End Property

#End Region

#Region "Methods"

#End Region

#Region "Events"

    Public Event SelectedValueChanged As EventHandler
    Public Event SelectedIndexChanged As EventHandler

#End Region

#Region "Widget Event Handlers"

    Protected Overrides Sub OnHandleCreated(ByVal e As System.EventArgs)
        MyBase.OnHandleCreated(e)
        ' [04-Feb-2026] Me.DesignMode is unreliable in nested widgets.
        'If Not Me.DesignMode Then
        Me.Init()
        'End If
    End Sub

    Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
        Me.Free()
        MyBase.OnHandleDestroyed(e)
    End Sub

    Protected Overloads Overrides Sub OnControlAdded(ByVal e As ControlEventArgs)
        If TypeOf e.Control Is RadioButton Then
            Dim radioButton As RadioButton = CType(e.Control, RadioButton)
            Me.theRadioButtonList.Add(radioButton)
            AddHandler radioButton.CheckedChanged, AddressOf Me.RadioButton_CheckedChanged

            If Me.theRadioButtonList.Count = 1 Then
                Me.SelectedIndex = 0
                If radioButton.Tag IsNot Nothing Then
                    Me.theSelectedValue = CType(radioButton.Tag, System.Enum)
                End If
            End If
        End If
        MyBase.OnControlAdded(e)
    End Sub

    Protected Overloads Overrides Sub OnControlRemoved(ByVal e As ControlEventArgs)
        If TypeOf e.Control Is RadioButton Then
            Dim radioButton As RadioButton = CType(e.Control, RadioButton)
            Me.theRadioButtonList.Remove(radioButton)
            RemoveHandler radioButton.CheckedChanged, AddressOf Me.RadioButton_CheckedChanged
        End If
        MyBase.OnControlRemoved(e)
    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)

        If Me.CustomVerticalScrollBar.Visible Then
            Dim upOrDownValue As Integer = Me.CustomVerticalScrollBar.SmallChange
            If e.Delta > 0 Then
                ' Moving wheel away from user = up.
                Me.CustomVerticalScrollBar.Value -= upOrDownValue
            Else
                ' Moving wheel toward user = down.
                Me.CustomVerticalScrollBar.Value += upOrDownValue
            End If
        End If
    End Sub

    '' Works without needing to call SetStyle.
    'Protected Overrides Sub OnPaint(e As PaintEventArgs)
    '    MyBase.OnPaint(e)
    'End Sub

    'Protected Overrides Sub OnScroll(e As ScrollEventArgs)
    '	MyBase.OnScroll(e)
    '	Me.Invalidate()
    '	Me.UpdateScrollbars()
    'End Sub

    Protected Overridable Sub OnSelectedIndexChanged(ByVal e As EventArgs)
        RaiseEvent SelectedIndexChanged(Me, e)
    End Sub

    Protected Overridable Sub OnSelectedValueChanged(ByVal e As EventArgs)
        RaiseEvent SelectedValueChanged(Me, e)
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        ' This check prevents incorrect painting due to premature creation of Handle.
        '    Also, prevents unneeded resizing and painting when scrolling.
        If Me.theControlHasShown AndAlso Not Me.theScrollingIsActive Then
            MyBase.OnSizeChanged(e)

            'TODO: Find better way because the following 3 lines update the interface properly, but UpdateNonClientPadding() is called 2 or 3 times, and UpdateVerticalScrollbar() is called 1 or 2 times.
            'NOTE: Force calling UpdateNonClientPadding() here so that the correct clientHeight is used for scrollbars.
            'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
            Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
            Me.UpdateScrollbars()
            Me.Refresh()
        End If
    End Sub

    Protected Overrides Sub OnVisibleChanged(e As EventArgs)
        MyBase.OnVisibleChanged(e)

        If Me.Visible Then
            If Not Me.theControlHasShown Then
                Me.theControlHasShown = True

                'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
                Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
            End If

            Me.Invalidate()
            Me.UpdateScrollbars()
        End If
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If Not Me.theScrollingIsActive Then
            Select Case m.Msg
                Case Win32Api.WindowsMessages.WM_NCCALCSIZE
                    Me.OnNonClientCalcSize(m)
                Case Win32Api.WindowsMessages.WM_NCPAINT
                    Me.OnNonClientPaint(m)
            End Select
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub OnNonClientCalcSize(ByRef m As Message)
        Dim theme As PanelTheme = Nothing
        ' This check prevents problems with viewing and saving Forms in VS Designer.
        If TheApp IsNot Nothing Then
            theme = TheApp.Settings.SelectedAppTheme.PanelTheme
        End If
        If theme IsNot Nothing Then
            Me.UpdateNonClientPadding()
            If CInt(m.WParam) = 0 Then
                Dim rect As Win32Api.RECT = CType(Marshal.PtrToStructure(m.LParam, GetType(Win32Api.RECT)), Win32Api.RECT)
                Me.ResizeClientRect(Me.NonClientPadding, rect)
                Marshal.StructureToPtr(rect, m.LParam, False)
                m.Result = IntPtr.Zero
            ElseIf CInt(m.WParam) = 1 Then
                Dim nccsp As Win32Api.NCCALCSIZE_PARAMS = CType(Marshal.PtrToStructure(m.LParam, GetType(Win32Api.NCCALCSIZE_PARAMS)), Win32Api.NCCALCSIZE_PARAMS)
                Me.ResizeClientRect(Me.NonClientPadding, nccsp.rect0)
                Marshal.StructureToPtr(nccsp, m.LParam, False)
                m.Result = IntPtr.Zero
            End If
        End If
    End Sub

    Private Sub OnNonClientPaint(ByRef m As Message)
        Dim theme As PanelTheme = Nothing
        ' This check prevents problems with viewing and saving Forms in VS Designer.
        If TheApp IsNot Nothing Then
            theme = TheApp.Settings.SelectedAppTheme.PanelTheme
        End If
        If theme IsNot Nothing Then
            Dim borderColor As Color
            Dim borderWidth As Integer
            If Me.Enabled Then
                borderColor = theme.EnabledBorderColor
                borderWidth = theme.EnabledBorderWidth
            Else
                borderColor = theme.DisabledBorderColor
                borderWidth = theme.DisabledBorderWidth
            End If

            Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
            Try
                Using g As Graphics = Graphics.FromHdc(hDC)
                    ' Draw border.
                    Using borderColorPen As New Pen(borderColor, borderWidth)
                        borderColorPen.Alignment = Drawing2D.PenAlignment.Inset
                        Dim aRect As Rectangle = Rectangle.Truncate(g.VisibleClipBounds)
                        If borderWidth = 1 Then
                            'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw when pen width is 1.
                            aRect.Width -= 1
                            aRect.Height -= 1
                        End If
                        g.DrawRectangle(borderColorPen, aRect)
                    End Using
                End Using
            Finally
                Win32Api.ReleaseDC(Me.Handle, hDC)
            End Try
            m.Result = IntPtr.Zero
        End If
    End Sub

#End Region

#Region "Child Widget Event Handlers"

    Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim radioButton As RadioButton = CType(sender, RadioButton)
        If radioButton.Checked Then
            Me.SetIndex(Me.theRadioButtonList.IndexOf(radioButton))
            Me.SetValue(CType(radioButton.Tag, System.Enum))
        End If
    End Sub

    Private Sub CustomHorizontalScrollbar_ValueChanged(ByVal sender As Object, ByVal e As ScrollValueEventArgs) Handles CustomHorizontalScrollbar.ValueChanged
        Dim scrollInfo As Win32Api.SCROLLINFO
        Dim lRet As Integer
        scrollInfo.cbSize = Marshal.SizeOf(scrollInfo)
        scrollInfo.fMask = Win32Api.SIF_ALL
        lRet = Win32Api.GetScrollInfo(Me.Handle, Win32Api.ScrollBarType.SB_HORZ, scrollInfo)
        Dim pageChange As Integer = 0
        If lRet > 0 Then
            pageChange = scrollInfo.nPage
        End If
        Dim thumbValue As Integer = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_HORZ)
        Dim value As Integer = e.Value \ Me.CustomHorizontalScrollbar.SmallChange
        Dim currentThumbValue As Integer = thumbValue \ Me.CustomHorizontalScrollbar.SmallChange
        If value < currentThumbValue Then
            If currentThumbValue - value < pageChange Then
                For i As Integer = currentThumbValue To value + 1 Step -1
                    Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_LINELEFT, IntPtr.Zero)
                Next
            Else
                Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_PAGELEFT, IntPtr.Zero)
            End If
        ElseIf value > currentThumbValue Then
            If value - currentThumbValue < pageChange Then
                For i As Integer = currentThumbValue To value - 1
                    Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_LINERIGHT, IntPtr.Zero)
                Next
            Else
                Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_PAGERIGHT, IntPtr.Zero)
            End If
        End If
    End Sub

    Private Sub CustomVerticalScrollBar_ValueChanged(ByVal sender As Object, ByVal e As ScrollValueEventArgs) Handles CustomVerticalScrollBar.ValueChanged
        ''Me.UpdateScrolling(0, e.Value)
        'If Not Me.theScrollingIsActive Then
        '	Me.theScrollingIsActive = True
        '	Win32Api.ShowScrollBar(Me.Handle, 1, True)
        '	Me.VerticalScroll.Value = e.Value
        '	Win32Api.ShowScrollBar(Me.Handle, 0, False)
        '	Win32Api.ShowScrollBar(Me.Handle, 1, False)
        '	Me.theScrollingIsActive = False
        '	'Me.Refresh()
        'End If
        '------
        Dim scrollInfo As Win32Api.SCROLLINFO
        Dim lRet As Integer
        scrollInfo.cbSize = Marshal.SizeOf(scrollInfo)
        scrollInfo.fMask = Win32Api.SIF_ALL
        lRet = Win32Api.GetScrollInfo(Me.Handle, Win32Api.ScrollBarType.SB_VERT, scrollInfo)
        Dim pageChange As Integer = 0
        If lRet > 0 Then
            pageChange = scrollInfo.nPage
        End If
        Dim thumbValue As Integer = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_VERT)
        Dim value As Integer = e.Value
        Dim currentThumbValue As Integer = thumbValue
        If value < currentThumbValue Then
            If currentThumbValue - value < pageChange Then
                value \= VerticalScroll.SmallChange
                currentThumbValue \= VerticalScroll.SmallChange
                For i As Integer = currentThumbValue - 1 To value Step -1
                    Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_VSCROLL, Win32Api.SB.SB_LINEUP, IntPtr.Zero)
                Next
            Else
                Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_VSCROLL, Win32Api.SB.SB_PAGEUP, IntPtr.Zero)
            End If
        ElseIf value > currentThumbValue Then
            If value - currentThumbValue < pageChange Then
                value \= VerticalScroll.SmallChange
                currentThumbValue \= VerticalScroll.SmallChange
                For i As Integer = currentThumbValue + 1 To value
                    Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_VSCROLL, Win32Api.SB.SB_LINEDOWN, IntPtr.Zero)
                Next
            Else
                Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_VSCROLL, Win32Api.SB.SB_PAGEDOWN, IntPtr.Zero)
            End If
        End If
    End Sub

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
        Dim theme As PanelTheme = Nothing
        If TheApp IsNot Nothing Then
            theme = TheApp.Settings.SelectedAppTheme.PanelTheme
        End If
        If theme IsNot Nothing Then
            Me.ForeColor = theme.EnabledForeColor
            Me.BackColor = theme.EnabledBackColor
            'MyBase.BackColor = Color.Red
        Else
            Me.ForeColor = Control.DefaultForeColor
            Me.BackColor = Control.DefaultBackColor
        End If

        'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
        Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
    End Sub

    Private Sub UpdateNonClientPadding()
        If Me.DesignMode Then
            Exit Sub
        End If

        Dim left As Integer = 0
        Dim top As Integer = 0
        Dim right As Integer = 0
        Dim bottom As Integer = 0

        If Me.AutoScroll Then
            Dim scrollBarInfo As New Win32Api.SCROLLBARINFO()
            scrollBarInfo.cbSize = Marshal.SizeOf(scrollBarInfo.[GetType]())
            Dim resultIsSuccess As Boolean = Win32Api.GetScrollBarInfo(Me.Handle, Win32Api.OBJID_VSCROLL, scrollBarInfo)
            If (scrollBarInfo.scrollbar And Win32Api.STATE_SYSTEM_INVISIBLE) = 0 Then
                right += ScrollBarEx.Consts.ScrollBarSize - scrollBarInfo.dxyLineButton
            End If
            resultIsSuccess = Win32Api.GetScrollBarInfo(Me.Handle, Win32Api.OBJID_HSCROLL, scrollBarInfo)
            If (scrollBarInfo.scrollbar And Win32Api.STATE_SYSTEM_INVISIBLE) = 0 Then
                bottom += ScrollBarEx.Consts.ScrollBarSize - scrollBarInfo.dxyLineButton
            End If
        End If

        Dim theme As PanelTheme = Nothing
        If TheApp IsNot Nothing Then
            theme = TheApp.Settings.SelectedAppTheme.PanelTheme
        End If
        If theme IsNot Nothing Then
            Dim borderWidth As Integer
            If Me.Enabled Then
                borderWidth = theme.EnabledBorderWidth
            Else
                borderWidth = theme.DisabledBorderWidth
            End If
            left += borderWidth
            top += borderWidth
            right += borderWidth
            bottom += borderWidth
        End If

        Me.NonClientPadding = New Padding(left, top, right, bottom)
    End Sub

    Private Sub ResizeClientRect(ByVal padding As Padding, ByRef rect As Win32Api.RECT)
        rect.Left += padding.Left
        rect.Top += padding.Top

        rect.Right -= padding.Right
        rect.Bottom -= padding.Bottom
    End Sub

    'Private Sub UpdateScrolling(ByVal leftOrRightValue As Integer, ByVal upOrDownValue As Integer)
    '	If Not Me.theScrollingIsActive Then
    '		Me.theScrollingIsActive = True

    '		'SetDisplayRectLocation(0, AutoScrollPosition.Y + upOrDownValue)
    '		'Me.Invalidate()
    '		'------
    '		'Dim scrollPosition As New Point(leftOrRightValue, upOrDownValue)
    '		'Win32Api.RtfScroll(Me.Handle, Win32Api.WindowsMessages.EM_SETSCROLLPOS, IntPtr.Zero, scrollPosition)
    '		'------
    '		'Me.CustomHorizontalScrollbar.Value += leftOrRightValue
    '		'Me.CustomVerticalScrollBar.Value += upOrDownValue
    '		'Me.HorizontalScroll.Value = leftOrRightValue
    '		'If upOrDownValue <= Me.CustomVerticalScrollBar.Minimum OrElse upOrDownValue > Me.CustomVerticalScrollBar.Maximum Then
    '		'Me.AutoScrollPosition = New Point(leftOrRightValue, upOrDownValue)
    '		'Else
    '		'	Me.CustomHorizontalScrollbar.Value = leftOrRightValue
    '		'	Me.CustomVerticalScrollBar.Value = upOrDownValue
    '		'End If
    '		'Me.Invalidate()
    '		'Me.Invalidate(True)
    '		'Me.Refresh()

    '		Me.theScrollingIsActive = False
    '	End If
    'End Sub

    Private Sub UpdateScrollbars()
        If Me.DesignMode Then
            Exit Sub
        End If

        Dim theme As PanelTheme = Nothing
        If TheApp IsNot Nothing Then
            theme = TheApp.Settings.SelectedAppTheme.PanelTheme
        End If
        If theme IsNot Nothing Then
            Me.UpdateHorizontalScrollbar()
            Me.UpdateVerticalScrollbar()

            If Me.CustomHorizontalScrollbar.Visible AndAlso Me.CustomVerticalScrollBar.Visible Then
                Me.ScrollbarCornerPanel.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize + theme.EnabledBorderWidth, ScrollBarEx.Consts.ScrollBarSize + theme.EnabledBorderWidth)
                Me.ScrollbarCornerPanel.RightAndBottomBorderWidth = 1
                'NOTE: Assign to Parent so it can draw over non-client area.
                Me.ScrollbarCornerPanel.Parent = Me.Parent
                Me.ScrollbarCornerPanel.BringToFront()
                Dim aPoint As New Point(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
                'NOTE: Location must be relative to Parent.
                aPoint = Me.PointToScreen(aPoint)
                aPoint = Me.ScrollbarCornerPanel.Parent.PointToClient(aPoint)
                Me.ScrollbarCornerPanel.Location = aPoint
                Me.ScrollbarCornerPanel.Show()
            Else
                Me.ScrollbarCornerPanel.Hide()
            End If
        Else
            Me.theScrollingIsActive = True
            Me.CustomHorizontalScrollbar.Hide()
            Me.CustomVerticalScrollBar.Hide()
            Me.ScrollbarCornerPanel.Hide()
            Me.theScrollingIsActive = False
        End If
    End Sub

    Private Sub UpdateHorizontalScrollbar()
        'NOTE: Parent can be Nothing on exiting. Prevent the exception with this check.
        If Not Me.theScrollingIsActive AndAlso Me.Parent IsNot Nothing AndAlso Me.AutoScroll Then
            Dim scrollBarInfo As New Win32Api.SCROLLBARINFO()
            scrollBarInfo.cbSize = Marshal.SizeOf(scrollBarInfo.[GetType]())
            Dim resultIsSuccess As Boolean = Win32Api.GetScrollBarInfo(Me.Handle, Win32Api.OBJID_HSCROLL, scrollBarInfo)

            If (scrollBarInfo.scrollbar And Win32Api.STATE_SYSTEM_INVISIBLE) = 0 Then
                Me.theScrollingIsActive = True

                Dim scrollInfo As Win32Api.SCROLLINFO
                Dim lRet As Integer
                scrollInfo.cbSize = Marshal.SizeOf(scrollInfo)
                scrollInfo.fMask = Win32Api.SIF_ALL
                lRet = Win32Api.GetScrollInfo(Me.Handle, Win32Api.ScrollBarType.SB_HORZ, scrollInfo)
                If lRet > 0 Then
                    Me.CustomHorizontalScrollbar.Minimum = scrollInfo.nMin
                    Me.CustomHorizontalScrollbar.Maximum = scrollInfo.nMax
                    Me.CustomHorizontalScrollbar.Value = scrollInfo.nTrackPos
                    Me.CustomHorizontalScrollbar.ViewSize = Me.ClientRectangle.Width - Me.NonClientPadding.Right
                    Me.CustomHorizontalScrollbar.SmallChange = Me.HorizontalScroll.SmallChange
                    Me.CustomHorizontalScrollbar.LargeChange = scrollInfo.nPage
                End If

                'NOTE: Assign to Parent so it can draw over non-client area.
                Me.CustomHorizontalScrollbar.Parent = Me.Parent
                Me.CustomHorizontalScrollbar.BringToFront()
                Dim aPoint As New Point(Me.ClientRectangle.Left, Me.ClientRectangle.Height)
                'NOTE: Location must be relative to Parent.
                aPoint = Me.PointToScreen(aPoint)
                aPoint = Me.CustomHorizontalScrollbar.Parent.PointToClient(aPoint)
                Me.CustomHorizontalScrollbar.Location = aPoint
                Dim theme As PanelTheme = TheApp.Settings.SelectedAppTheme.PanelTheme
                Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.ClientRectangle.Width, ScrollBarEx.Consts.ScrollBarSize + theme.EnabledBorderWidth)
                Me.CustomHorizontalScrollbar.RightAndBottomBorderWidth = 1
                Me.CustomHorizontalScrollbar.Show()

                Me.theScrollingIsActive = False
            Else
                Me.theScrollingIsActive = True
                Me.CustomHorizontalScrollbar.Hide()
                Me.theScrollingIsActive = False
            End If
        End If
    End Sub

    Private Sub UpdateVerticalScrollbar()
        'NOTE: Parent can be Nothing on exiting. Prevent the exception with this check.
        If Not Me.theScrollingIsActive AndAlso Me.Parent IsNot Nothing AndAlso Me.AutoScroll Then
            Dim scrollBarInfo As New Win32Api.SCROLLBARINFO()
            scrollBarInfo.cbSize = Marshal.SizeOf(scrollBarInfo.[GetType]())
            Dim resultIsSuccess As Boolean = Win32Api.GetScrollBarInfo(Me.Handle, Win32Api.OBJID_VSCROLL, scrollBarInfo)

            If (scrollBarInfo.scrollbar And Win32Api.STATE_SYSTEM_INVISIBLE) = 0 Then
                Me.theScrollingIsActive = True

                Dim scrollInfo As Win32Api.SCROLLINFO
                Dim lRet As Integer
                scrollInfo.cbSize = Marshal.SizeOf(scrollInfo)
                scrollInfo.fMask = Win32Api.SIF_ALL
                lRet = Win32Api.GetScrollInfo(Me.Handle, Win32Api.ScrollBarType.SB_VERT, scrollInfo)
                If lRet > 0 Then
                    Me.CustomVerticalScrollBar.Minimum = scrollInfo.nMin
                    Me.CustomVerticalScrollBar.Maximum = scrollInfo.nMax
                    Me.CustomVerticalScrollBar.Value = scrollInfo.nTrackPos
                    Me.CustomVerticalScrollBar.ViewSize = Me.ClientRectangle.Height
                    Me.CustomVerticalScrollBar.SmallChange = VerticalScroll.SmallChange
                    Me.CustomVerticalScrollBar.LargeChange = scrollInfo.nPage
                End If

                'NOTE: Assign to Parent so it can draw over non-client area.
                Me.CustomVerticalScrollBar.Parent = Me.Parent
                Me.CustomVerticalScrollBar.BringToFront()
                Dim aPoint As New Point(Me.ClientRectangle.Width, Me.ClientRectangle.Top)
                'NOTE: Location must be relative to Parent.
                aPoint = Me.PointToScreen(aPoint)
                aPoint = Me.CustomVerticalScrollBar.Parent.PointToClient(aPoint)
                Me.CustomVerticalScrollBar.Location = aPoint
                Dim theme As PanelTheme = TheApp.Settings.SelectedAppTheme.PanelTheme
                Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize + theme.EnabledBorderWidth, Me.ClientRectangle.Height)
                Me.CustomVerticalScrollBar.RightAndBottomBorderWidth = 1
                Me.CustomVerticalScrollBar.Show()

                Me.theScrollingIsActive = False
            Else
                Me.theScrollingIsActive = True
                Me.CustomVerticalScrollBar.Hide()
                Me.theScrollingIsActive = False
            End If
        End If
    End Sub

    Private Sub SetIndex(ByVal index As Integer)
        Me.theSelectedIndex = index
        Me.OnSelectedIndexChanged(New EventArgs())
    End Sub

    Private Sub SetValue(ByVal enumValue As System.Enum)
        Me.theSelectedValue = enumValue
        Me.OnSelectedValueChanged(New EventArgs())
    End Sub

#End Region

#Region "Data"

    Private theRadioButtonList As New System.Collections.Generic.List(Of RadioButton)()
    Private theSelectedIndex As Integer
    Private theSelectedValue As System.Enum

    Private NonClientPadding As Padding
    Private WithEvents CustomHorizontalScrollbar As ScrollBarEx
    Private WithEvents CustomVerticalScrollBar As ScrollBarEx
    Private ScrollbarCornerPanel As ScrollBarPanel
    Private theControlHasShown As Boolean
    Private theScrollingIsActive As Boolean

#End Region

End Class
