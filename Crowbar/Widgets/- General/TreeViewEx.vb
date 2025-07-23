Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles

' Much faster filling of TreeView by using internal Dictionary.ContainsKey() rather than TreeNode.Nodes.ContainsKey().
Public Class TreeViewEx
	Inherits TreeView

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()
		Me.theNameToTreeNodeMap = New Dictionary(Of String, TreeNode)()
	End Sub

	' Empty Id means no TreeNode for item. 
	' Empty parentId means parent node is root node.
	'Public Sub InsertItems(Of ItemType, TagType)(ByVal rootTreeNode As TreeNode, ByVal items As IEnumerable(Of ItemType), ByVal GetId As Func(Of ItemType, String), ByVal GetDisplayName As Func(Of ItemType, String), ByVal GetParentItem As Func(Of ItemType, ItemType), ByVal GetTag As GetTagDelegate(Of ItemType, TagType), ByVal IsDimmed As Func(Of ItemType, Boolean))
	'	If items Is Nothing Then
	'		Exit Sub
	'	End If

	'	rootTreeNode.Nodes.Clear()
	'	Me.theTreeNodeMap.Clear()

	'	For Each item As ItemType In items
	'		Dim id As String = GetId(item)
	'		Dim node As TreeNode = Nothing

	'		If id <> "" Then
	'			If Not Me.theTreeNodeMap.ContainsKey(id) Then
	'				node = New TreeNode()
	'				With node
	'					.Name = id
	'					.Text = GetDisplayName(item)
	'				End With
	'				Me.theTreeNodeMap.Add(id, node)
	'			Else
	'				node = Me.theTreeNodeMap(id)
	'			End If
	'			' True means childItem is a leaf node. False is unused because of the True.
	'			node.Tag = GetTag(item, item, CType(node.Tag, TagType), True, False)
	'		End If

	'		Dim childItem As ItemType = item
	'		Dim childNode As TreeNode = node
	'		While True
	'			Dim parentItem As ItemType = GetParentItem(childItem)
	'			Dim parentId As String = GetId(parentItem)
	'			Dim parentNode As TreeNode

	'			If parentId <> "" Then
	'				If Not Me.theTreeNodeMap.ContainsKey(parentId) Then
	'					parentNode = New TreeNode()
	'					With parentNode
	'						.Name = parentId
	'						.Text = GetDisplayName(parentItem)
	'					End With
	'					Me.theTreeNodeMap.Add(parentId, parentNode)
	'				Else
	'					parentNode = Me.theTreeNodeMap(parentId)
	'				End If
	'			Else
	'				parentNode = rootTreeNode
	'			End If

	'			If childNode IsNot Nothing Then
	'				If parentNode.Nodes.Contains(childNode) Then
	'					' False means parentItem is not a leaf node. True means parentItem exists.
	'					parentNode.Tag = GetTag(parentItem, item, CType(parentNode.Tag, TagType), False, True)
	'				Else
	'					parentNode.Nodes.Add(childNode)
	'					' First False means parentItem is not a leaf node. Second False means parentItem is new.
	'					parentNode.Tag = GetTag(parentItem, item, CType(parentNode.Tag, TagType), False, False)
	'				End If
	'				If IsDimmed(parentItem) Then
	'					childNode.ForeColor = SystemColors.GrayText
	'					If childNode.Text = "fx" Then
	'						Dim debug As Int32 = 4242
	'					End If
	'				Else
	'					'FROM: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.treenode.forecolor?view=netframework-4.0
	'					' If null, the Color used is the ForeColor property value of the TreeView control that the tree node is assigned to.
	'					childNode.ForeColor = Nothing
	'					If childNode.Text = "fx" Then
	'						Dim debug As Int32 = 4242
	'					End If
	'				End If
	'			ElseIf childNode Is Nothing Then
	'				' True means childItem is a leaf node. False is unused because of the True.
	'				parentNode.Tag = GetTag(childItem, item, CType(parentNode.Tag, TagType), True, False)
	'			End If

	' This stuff is needed to handle custom scrollbars.
	' Override BorderStyle to allow custom border width.
	MyBase.BorderStyle = BorderStyle.None
	Me.theBorderStyle = BorderStyle.FixedSingle
	Me.theBorderWidth = 1
	Me.DrawMode = TreeViewDrawMode.OwnerDrawAll
	Me.theTreePlusIcon = New VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Closed)
	Me.theTreeMinusIcon = New VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Opened)

	'TODO: Try the following trick to keep the default scrollbars, but clip them.
	'FROM: https://www.vbforums.com/showthread.php?830825-RESOLVED-Scrolling-a-TreeView-without-scrollbars&p=5059789&viewfull=1#post5059789
	'      "increasing the size of my TreeView and clipping its region to exclude the scrollbars will work"
	'MyBase.Scrollable = False
	MyBase.Scrollable = True
	'Me.theAutoScroll = True

	Me.theScrollingIsActive = False
	Me.theMouseWheelHasMoved = False

	Me.CustomHorizontalScrollbar = New ScrollBarEx()
	Me.Controls.Add(Me.CustomHorizontalScrollbar)
	Me.CustomHorizontalScrollbar.Name = "CustomHorizontalScrollbar"
	Me.CustomHorizontalScrollbar.ScrollOrientation = ScrollBarEx.DarkScrollOrientation.Horizontal
	Me.CustomHorizontalScrollbar.TabIndex = 7
	Me.CustomHorizontalScrollbar.Visible = False
	'Me.CustomHorizontalScrollbar.Visible = True
	'Me.CustomHorizontalScrollbar.Location = New System.Drawing.Point(0, 0)
	'Me.CustomHorizontalScrollbarPopup = New Popup(Me.CustomHorizontalScrollbar)
	'Me.CustomHorizontalScrollbarPopup.Name = "CustomHorizontalScrollbarPopup"

	Me.CustomVerticalScrollBar = New ScrollBarEx()
	Me.Controls.Add(Me.CustomVerticalScrollBar)
	Me.CustomVerticalScrollBar.Name = "CustomVerticalScrollBar"
	Me.CustomVerticalScrollBar.ScrollOrientation = ScrollBarEx.DarkScrollOrientation.Vertical
	Me.CustomVerticalScrollBar.TabIndex = 7
	Me.CustomVerticalScrollBar.Visible = False

	Me.ScrollbarCornerPanel = New ScrollBarPanel()
	Me.Controls.Add(Me.ScrollbarCornerPanel)
	Me.ScrollbarCornerPanel.Name = "ScrollbarCornerPanel"
	Me.ScrollbarCornerPanel.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, ScrollBarEx.Consts.ScrollBarSize)
	Me.ScrollbarCornerPanel.Visible = False

	Me.theControlHasShown = False

	Me.theTextFormatFlags = TextFormatFlags.GlyphOverhangPadding Or TextFormatFlags.PreserveGraphicsTranslateTransform
	End Sub

	Delegate Function GetTagDelegate(Of ItemType, Tagtype)(ByRef item As ItemType, ByVal leafItem As ItemType, ByVal tag As Tagtype, ByVal blah As Boolean, ByVal blah2 As Boolean) As Tagtype

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

	<Browsable(True)>
	<Category("Appearance")>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Overloads Property BorderColor As Color
		Get
			Return Me.theBorderColor
		End Get
		Set
			Me.theBorderColor = Value
		End Set
	End Property

	<Browsable(True)>
	<Category("Appearance")>
	<Description("Colorable BorderStyle.")>
	<DefaultValue(BorderStyle.FixedSingle)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Overloads Property BorderStyle As BorderStyle
		Get
			Return Me.theBorderStyle
		End Get
		Set
			Me.theBorderStyle = Value

			If Me.theBorderStyle = Windows.Forms.BorderStyle.None Then
				Me.theBorderWidth = 0
			ElseIf Me.theBorderStyle = Windows.Forms.BorderStyle.FixedSingle Then
				Me.theBorderWidth = 1
			End If
		End Set
	End Property

	'<Browsable(True)>
	'<Category("Layout")>
	'<Description("Scrollbars appear when needed.")>
	'Public Overloads Property Scrollable As Boolean
	'	Get
	'		Return Me.theAutoScroll
	'	End Get
	'	Set
	'		MyBase.Scrollable = False
	'		Me.theAutoScroll = Value
	'	End Set
	'End Property

#End Region

#Region "Methods"

	Public Sub InsertItems(Of ItemType, TagType)(ByVal rootTreeNode As TreeNode, ByVal items As IEnumerable(Of ItemType), ByVal GetId As Func(Of ItemType, String), ByVal GetDisplayName As Func(Of ItemType, String), ByVal GetParentItem As Func(Of ItemType, ItemType), ByVal GetTag As GetTagDelegate(Of ItemType, TagType), ByVal IsDimmed As Func(Of ItemType, Boolean))
		If items Is Nothing Then
			Exit Sub
		End If

		rootTreeNode.Nodes.Clear()
		Me.theNameToTreeNodeMap.Clear()

		For Each leafItem As ItemType In items
			Dim item As ItemType = leafItem
			'Dim itemIsLeafItem As Boolean = True
			Dim childNode As TreeNode = Nothing

			While True
				Dim nodeName As String = GetId(item)
				Dim node As TreeNode

				If nodeName <> "" Then
					If Not Me.theNameToTreeNodeMap.ContainsKey(nodeName) Then
						node = New TreeNode()
						With node
							.Name = nodeName
							.Text = GetDisplayName(item)
						End With
						Me.theNameToTreeNodeMap.Add(nodeName, node)
					Else
						node = Me.theNameToTreeNodeMap(nodeName)
					End If
				Else
					node = rootTreeNode
				End If

				If childNode IsNot Nothing Then
					If node.Nodes.Contains(childNode) Then
						' False means item is not a leaf node. True means item exists.
						node.Tag = GetTag(item, leafItem, CType(node.Tag, TagType), False, True)
					Else
						node.Nodes.Add(childNode)
						' First False means item is not a leaf node. Second False means item is new.
						node.Tag = GetTag(item, leafItem, CType(node.Tag, TagType), False, False)
					End If
					If IsDimmed(item) Then
						childNode.ForeColor = SystemColors.GrayText
					Else
						'FROM: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.treenode.forecolor?view=netframework-4.0
						' If null, the Color used is the ForeColor property value of the TreeView control that the tree node is assigned to.
						childNode.ForeColor = Nothing
					End If
				ElseIf childNode Is Nothing Then
					' True means item is a leaf node. False is unused because of the True.
					node.Tag = GetTag(item, leafItem, CType(node.Tag, TagType), True, False)
				End If
				'If Not itemIsLeafItem Then
				'	If IsDimmed(item) Then
				'		childNode.ForeColor = SystemColors.GrayText
				'	Else
				'		'FROM: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.treenode.forecolor?view=netframework-4.0
				'		' If null, the Color used is the ForeColor property value of the TreeView control that the tree node is assigned to.
				'		childNode.ForeColor = Nothing
				'	End If
				'End If

				If nodeName <> "" Then
					item = GetParentItem(item)
					'itemIsLeafItem = False
					childNode = node
				Else
					Exit While
				End If
			End While
		Next
	End Sub

#End Region

#Region "Events"

#End Region

#Region "Widget Event Handlers"

	Protected Overrides Sub OnHandleCreated(e As EventArgs)
		MyBase.OnHandleCreated(e)

		If Me.theOriginalFont Is Nothing Then
			Me.Font = New Font(SystemFonts.MessageBoxFont.Name, 8.25)
			'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
			'      so save the Font before changing style.
			Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)

			''SetStyle(ControlStyles.AllPaintingInWmPaint, True)
			''SetStyle(ControlStyles.DoubleBuffer, True)
			'SetStyle(ControlStyles.UserPaint, True)
		End If
		'Win32Api.ShowScrollBar(Me.Handle, Win32Api.SB_BOTH, False)

		' [04-Feb-2026] Me.DesignMode is unreliable in nested widgets.
		'If Not Me.DesignMode Then
		Me.Init()
		'End If
	End Sub

	Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
		Me.Free()
		MyBase.OnHandleDestroyed(e)
	End Sub

	Protected Overrides Sub OnAfterSelect(e As TreeViewEventArgs)
		MyBase.OnAfterSelect(e)

		'If Me.theOriginalFont IsNot Nothing Then
		'	' EnsureVisible() does not seem to work without scrollbars.
		'	MyBase.Scrollable = True
		'	e.Node.EnsureVisible()
		'	MyBase.Scrollable = False
		'	Me.UpdateScrollbars()
		'End If
		'------
		e.Node.EnsureVisible()
		Me.UpdateScrollbars()
	End Sub

	Protected Overrides Sub OnAfterExpand(e As TreeViewEventArgs)
		MyBase.OnAfterExpand(e)
		Me.UpdateScrollbars()
	End Sub

	Protected Overrides Sub OnAfterCollapse(e As TreeViewEventArgs)
		MyBase.OnAfterCollapse(e)
		Me.UpdateScrollbars()
	End Sub

	'NOTE: Windows calls the default painting AFTER the call to OnDrawNode, so can not modify the default painting here.
	'NOTE: The e.Bounds seems to be entire width of TreeView, whereas node.Bounds seems to be just the rect of the text area.
	Protected Overrides Sub OnDrawNode(e As DrawTreeNodeEventArgs)
		Dim theme As TreeViewTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.TreeViewTheme
		End If
		If theme IsNot Nothing Then
			If Me.theMouseWheelHasMoved Then
				Me.UpdateScrollbars()
				Me.theMouseWheelHasMoved = False
			End If
			Dim node As TreeNode = e.Node
			Dim textRect As Rectangle = node.Bounds
			If textRect.IsEmpty Then
				Exit Sub
			End If

			Dim g As Graphics = e.Graphics
			Dim nodeRect As Rectangle = e.Bounds
			Dim nodeLevel As Integer = node.Level

			' Draw expansion icon.
			Dim expandRect As Rectangle = nodeRect
			expandRect.X += Me.Indent * nodeLevel + 4
			expandRect.Width = 16
			If node.Nodes.Count > 0 Then
				If node.IsExpanded Then
					Me.theTreeMinusIcon.DrawBackground(g, expandRect)
				Else
					Me.theTreePlusIcon.DrawBackground(g, expandRect)
				End If
			End If

			' Draw node icon.
			Dim iconRect As Rectangle = nodeRect
			iconRect.X += Me.Indent * nodeLevel + 20
			iconRect.Width = 16
			g.DrawImage(Me.ImageList.Images(0), iconRect.X, iconRect.Y)

			' Draw text.
			' Why widen the rect by 2 pixels?
			textRect.Width += 2
			Dim textForeColor As Color = theme.EnabledForeColor
			Dim textBackColor As Color = theme.EnabledBackColor
			Dim state As TreeNodeStates = e.State
			If (state And TreeNodeStates.Selected) > 0 Then
				If (state And TreeNodeStates.Focused) > 0 Then
					textBackColor = theme.FocusBackColor
				Else
					textBackColor = theme.SelectedBackColor
				End If
			End If
			Using backColorBrush As New SolidBrush(textBackColor)
				g.FillRectangle(backColorBrush, textRect)
			End Using
			TextRenderer.DrawText(g, node.Text, Me.theOriginalFont, textRect, textForeColor, textBackColor, Me.theTextFormatFlags)
			'Dim textSize As Size = TextRenderer.MeasureText(g, node.Text, Me.theOriginalFont, textRect.Size, Me.theTextFormatFlags)
			'If textSize.Width > node.Bounds.Left + node.Bounds.Width Then
			'	Dim debug As Integer = 4242
			'End If
			'node.Tag = node.Bounds.Left + textSize.Width

			e.DrawDefault = False
		Else
			e.DrawDefault = True
		End If
		MyBase.OnDrawNode(e)
	End Sub

	' Can not use this because it updates the scrollbar on the *next* wheel move.
	'Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
	'	MyBase.OnMouseWheel(e)
	'	Me.UpdateScrollbars()
	'End Sub

	'Protected Overrides Sub OnPaint(e As PaintEventArgs)
	'	MyBase.OnPaint(e)
	'	Using borderColorPen As New Pen(Color.Green)
	'		Dim aRect As Rectangle = Me.ClientRectangle
	'		'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw.
	'		aRect.Width -= 1
	'		aRect.Height -= 1
	'		e.Graphics.DrawRectangle(borderColorPen, aRect.Left, aRect.Top, aRect.Width, aRect.Height)
	'	End Using
	'End Sub

	Protected Overrides Sub OnSizeChanged(e As EventArgs)
		' This check prevents incorrect painting due to premature creation of Handle.
		'    Also, prevents unneeded resizing and painting when scrolling.
		If Me.theControlHasShown AndAlso Not Me.theScrollingIsActive Then
			MyBase.OnSizeChanged(e)

			''NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
			'Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
			Me.UpdateScrollbars()
			'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
			Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
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

			''NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
			'Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
			'Me.Invalidate()
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
				Case Win32Api.WindowsMessages.WM_MOUSEWHEEL
					Me.theMouseWheelHasMoved = True
					'Case Win32Api.WindowsMessages.WM_PAINT
					'	Me.OnNonClientPaint(m)
			End Select
		End If

		MyBase.WndProc(m)

		'If m.Msg = Win32Api.WindowsMessages.WM_PAINT Then
		'	Me.UpdateScrollbars()
		'End If
		'If m.Msg = Win32Api.WindowsMessages.WM_NCPAINT Then
		'	Me.UpdateScrollbars()
		'End If
		'If Me.theScrollingIsActive Then
		'Select Case m.Msg
		'	Case Win32Api.WindowsMessages.WM_NCCALCSIZE
		'		'Win32Api.ShowScrollBar(Me.Handle, Win32Api.SB_BOTH, False)
		'		Me.UpdateBottomBorder()
		'	'Case Win32Api.WindowsMessages.WM_NCPAINT
		'	'	'	Win32Api.ShowScrollBar(Me.Handle, Win32Api.SB_BOTH, False)
		'	'	Me.UpdateBottomBorder()
		'	Case Win32Api.WindowsMessages.WM_PAINT
		'		'Win32Api.ShowScrollBar(Me.Handle, Win32Api.SB_BOTH, False)
		'		Me.UpdateBottomBorder()
		'End Select
		'Me.UpdateBottomBorder()
		'End If
	End Sub

	Private Sub OnNonClientCalcSize(ByRef m As Message)
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
		'------
		'' Disabling horizontal scrollbar also disables scrolling.
		'Dim style As Integer = Win32Api.GetWindowLong(Me.Handle, Win32Api.GWL_STYLE)
		'If (style And Win32Api.WindowsStyles.WS_hSCROLL) > 0 Then
		'	Win32Api.SetWindowLong(Me.Handle, Win32Api.GWL_STYLE, style And (Not Win32Api.WindowsStyles.WS_hSCROLL))
		'End If
		'If (style And Win32Api.WindowsStyles.WS_VSCROLL) > 0 Then
		'	Win32Api.SetWindowLong(Me.Handle, Win32Api.GWL_STYLE, style And (Not Win32Api.WindowsStyles.WS_VSCROLL))
		'End If
	End Sub

	Private Sub OnNonClientPaint(ByRef m As Message)
		Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
		Try
			Using g As Graphics = Graphics.FromHdc(hDC)
				' Draw border.
				'Using backColorBrush As New SolidBrush(Me.theBorderColor)
				'	Dim aRect As RectangleF = g.VisibleClipBounds
				'	g.FillRectangle(backColorBrush, aRect)
				'End Using
				Using borderColorPen As New Pen(Me.theBorderColor, Me.theBorderWidth)
					borderColorPen.Alignment = Drawing2D.PenAlignment.Inset
					Dim aRect As Rectangle = Rectangle.Truncate(g.VisibleClipBounds)
					'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw.
					aRect.Width -= 1
					aRect.Height -= 1
					g.DrawRectangle(borderColorPen, aRect)
				End Using

				' Paint TreeView padding.
				Using backColorPen As New Pen(Me.BackColor)
					Dim aRect As Rectangle = Me.ClientRectangle
					''NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw.
					'aRect.Width -= 1
					'aRect.Height -= 1
					'aRect.Inflate(-Me.theBorderWidth, -Me.theBorderWidth)
					'aRect.X += Me.theBorderWidth
					'aRect.Inflate(1, 1)
					aRect.Offset(Me.theBorderWidth, Me.theBorderWidth)
					aRect.Width += 1
					aRect.Height += 1
					g.DrawRectangle(backColorPen, aRect)
				End Using
			End Using
		Finally
			Win32Api.ReleaseDC(Me.Handle, hDC)
		End Try
		m.Result = IntPtr.Zero
	End Sub

	Private Sub CustomHorizontalScrollbar_ValueChanged(ByVal sender As Object, ByVal e As ScrollValueEventArgs) Handles CustomHorizontalScrollbar.ValueChanged
		If Not Me.theScrollingIsActive Then
			Me.theScrollingIsActive = True

			'Me.UpdateScrolling(e.Value, 0)
			'------
			'Dim horizontalValue As Integer = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_HORZ)
			'If e.Value < horizontalValue Then
			'	If horizontalValue - e.Value <= 5 Then
			'		Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_LINELEFT, IntPtr.Zero)
			'	Else
			'		Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_PAGELEFT, IntPtr.Zero)
			'	End If
			'ElseIf e.Value > horizontalValue Then
			'	If e.Value - horizontalValue <= 5 Then
			'		Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_LINERIGHT, IntPtr.Zero)
			'	Else
			'		Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_PAGERIGHT, IntPtr.Zero)
			'	End If
			'End If
			'------
			Dim thumbValue As UInt32 = CUInt(e.Value * &H10000 + Win32Api.SB.SB_THUMBPOSITION)
			Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, thumbValue, IntPtr.Zero)

			Me.theScrollingIsActive = False
		End If
	End Sub

	Private Sub CustomVerticalScrollBar_ValueChanged(ByVal sender As Object, ByVal e As ScrollValueEventArgs) Handles CustomVerticalScrollBar.ValueChanged
		If Not Me.theScrollingIsActive Then
			Me.theScrollingIsActive = True

			'Me.UpdateScrolling(0, e.Value)
			'------
			'Dim aNode As TreeNode = Me.TopNode
			'Dim visibleIndex As Integer = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_VERT)
			'While aNode IsNot Nothing AndAlso visibleIndex < e.Value
			'	aNode = aNode.NextVisibleNode
			'	visibleIndex += 1
			'End While
			'While aNode IsNot Nothing AndAlso visibleIndex > e.Value
			'	aNode = aNode.PrevVisibleNode
			'	visibleIndex -= 1
			'End While
			'Me.TopNode = aNode
			'------
			'' Does not move internal scrollbar or contents.
			'Dim thumbValue As UInt32 = CUInt(e.Value * &H10000 + Win32Api.SB.SB_THUMBPOSITION)
			'Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_VSCROLL, thumbValue, IntPtr.Zero)
			'------
			'' Does not move internal scrollbar or contents.
			'Dim thumbValue As UInt32 = CUInt(e.Value * &H10000 + Win32Api.SB.SB_THUMBTRACK)
			'Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_VSCROLL, thumbValue, IntPtr.Zero)
			'------
			'' Does not move contents.
			'Dim thumbValue As Integer = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_VERT)
			'Win32Api.SetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_VERT, e.Value, True)
			'------
			' Works -- scrolls internal scrollbar and contents.
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
			If e.Value < thumbValue Then
				If thumbValue - e.Value <= pageChange Then
					For i As Integer = thumbValue To e.Value + 1 Step -1
						Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_VSCROLL, Win32Api.SB.SB_LINEUP, IntPtr.Zero)
					Next
				Else
					Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_VSCROLL, Win32Api.SB.SB_PAGEUP, IntPtr.Zero)
				End If
			ElseIf e.Value > thumbValue Then
				If e.Value - thumbValue <= pageChange Then
					For i As Integer = thumbValue To e.Value - 1
						Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_VSCROLL, Win32Api.SB.SB_LINEDOWN, IntPtr.Zero)
					Next
				Else
					Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_VSCROLL, Win32Api.SB.SB_PAGEDOWN, IntPtr.Zero)
				End If
			End If

			Me.theScrollingIsActive = False
		End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

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
		Dim theme As TreeViewTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.TreeViewTheme
		End If
		If theme IsNot Nothing Then
			Me.ForeColor = theme.EnabledForeColor
			Me.BackColor = theme.EnabledBackColor
			Me.theBorderColor = theme.EnabledBorderColor

			Me.CustomHorizontalScrollbar.LeftAndTopPaddingColor = Me.BackColor
			Me.CustomHorizontalScrollbar.LeftAndTopPaddingColorIsUsed = True
			Me.CustomHorizontalScrollbar.RightAndBottomBorderColor = Me.theBorderColor

			Me.CustomVerticalScrollBar.LeftAndTopPaddingColor = Me.BackColor
			Me.CustomVerticalScrollBar.LeftAndTopPaddingColorIsUsed = True
			Me.CustomVerticalScrollBar.RightAndBottomBorderColor = Me.theBorderColor

			Me.ScrollbarCornerPanel.BackColor = Me.BackColor
			Me.ScrollbarCornerPanel.RightAndBottomBorderColor = Me.theBorderColor
		Else
			Me.ForeColor = SystemColors.ControlText
			Me.BackColor = SystemColors.Control
			Me.theBorderColor = SystemColors.WindowFrame
		End If
	End Sub

	'Private Function GetContentSize() As Size
	'	Dim contentSize As New Size(0, 0)

	'	'Dim contentWidth As Integer = Me.PreferredSize.Width - ScrollBarEx.Consts.ScrollBarSize - Me.Margin.Left
	'	'Dim contentWidth As Integer = Me.PreferredSize.Width

	'	'Dim contentHeight As Integer = Me.PreferredSize.Height - ScrollBarEx.Consts.ScrollBarSize - Me.Margin.Top
	'	'Dim contentHeight As Integer = Me.PreferredSize.Height
	'	'Dim contentHeight As Integer = Me.theContentRectangle.Height

	'	If Me.Nodes.Count > 0 Then
	'		Dim node As TreeNode = Me.Nodes(0)
	'		While node IsNot Nothing
	'			If node.IsVisible Then
	'				''Dim nodeWidth As Integer = Me.Indent * node.Level + node.Bounds.Width
	'				Dim nodeWidth As Integer = node.Bounds.Left + node.Bounds.Width

	'				' Is this accurate?: You can use the TreeNode.Bounds.Right property, which provides the rightmost pixel coordinate of the node's text relative to the TreeView.
	'				'    maxRight = Math.Max(maxRight, node.Bounds.Right)

	'				'If node.Bounds.Left < 0 Then

	'				'End If
	'				'Dim nodeWidth As Integer = CType(node.Tag, Integer)
	'				If contentSize.Width < nodeWidth Then
	'					contentSize.Width = nodeWidth
	'				End If

	'				contentSize.Height += Me.ItemHeight
	'				'			contentSize.Height += node.Bounds.Height
	'			End If

	'			node = node.NextVisibleNode
	'		End While
	'	End If

	'	Return contentSize
	'End Function

	'Private Sub DrawDebugRectangle(ByVal rect As RectangleF, ByVal color As Color)
	'	Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
	'	Try
	'		Using g As Graphics = Graphics.FromHdc(hDC)
	'			Using backColorBrush As New SolidBrush(color)
	'				g.FillRectangle(backColorBrush, rect)
	'			End Using
	'		End Using
	'	Finally
	'		Win32Api.ReleaseDC(Me.Handle, hDC)
	'	End Try
	'End Sub

	Private Sub UpdateNonClientPadding()
		If Me.DesignMode Then
			Exit Sub
		End If

		' Default TreeView has 1-pixel padding.
		Dim left As Integer = 1
		Dim top As Integer = 1
		Dim right As Integer = 1
		Dim bottom As Integer = 1

		If Me.Scrollable Then
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

		If Me.theBorderStyle = Windows.Forms.BorderStyle.FixedSingle Then
			left += 1
			top += 1
			right += 1
			bottom += 1
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

	'		''Me.CustomHorizontalScrollbar.Value += leftOrRightValue
	'		''Me.CustomVerticalScrollBar.Value += upOrDownValue
	'		''Me.HorizontalScroll.Value = leftOrRightValue
	'		'If upOrDownValue <= Me.CustomVerticalScrollBar.Minimum OrElse upOrDownValue > Me.CustomVerticalScrollBar.Maximum Then
	'		'	Me.AutoScrollPosition = New Point(leftOrRightValue, upOrDownValue)
	'		'Else
	'		'	Me.VerticalScroll.Value = upOrDownValue
	'		'End If
	'		''Me.Invalidate()
	'		''Me.Invalidate(True)
	'		'Me.Refresh()
	'		'======
	'		'Win32Api.SetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_HORZ, leftOrRightValue, True)
	'		'------
	'		Dim horizontalValue As Integer = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_HORZ)
	'		If leftOrRightValue < horizontalValue Then
	'			If horizontalValue - leftOrRightValue <= 5 Then
	'				Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_LINELEFT, IntPtr.Zero)
	'			Else
	'				Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_PAGELEFT, IntPtr.Zero)
	'			End If
	'		Else
	'			If leftOrRightValue - horizontalValue <= 5 Then
	'				Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_LINERIGHT, IntPtr.Zero)
	'			Else
	'				Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_HSCROLL, Win32Api.SB.SB_PAGERIGHT, IntPtr.Zero)
	'			End If
	'		End If

	'		''DEBUG: 
	'		'Dim value As Integer = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_VERT)
	'		Dim aNode As TreeNode = Me.TopNode
	'		Dim visibleIndex As Integer = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_VERT)
	'		While aNode IsNot Nothing AndAlso visibleIndex < upOrDownValue
	'			aNode = aNode.NextVisibleNode
	'			visibleIndex += 1
	'		End While
	'		While aNode IsNot Nothing AndAlso visibleIndex > upOrDownValue
	'			aNode = aNode.PrevVisibleNode
	'			visibleIndex -= 1
	'		End While
	'		Me.TopNode = aNode
	'		'======
	'		'Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
	'		'Try
	'		'	Using g As Graphics = Graphics.FromHdc(hDC)
	'		'		Dim left As Integer = 0
	'		'		Dim top As Integer = 0
	'		'		If Me.CustomHorizontalScrollbar.Visible Then
	'		'			left = Me.CustomHorizontalScrollbar.Value
	'		'		End If
	'		'		If Me.CustomVerticalScrollBar.Visible Then
	'		'			top = Me.CustomVerticalScrollBar.Value
	'		'		End If
	'		'		g.TranslateTransform(left, top)
	'		'	End Using
	'		'Finally
	'		'	Win32Api.ReleaseDC(Me.Handle, hDC)
	'		'End Try
	'		'======
	'		'Me.Refresh()
	'		'Me.Invalidate()

	'		Me.theScrollingIsActive = False
	'	End If
	'End Sub

	Private Sub UpdateScrollbars()
		If Me.DesignMode Then
			Exit Sub
		End If

		Dim theme As TreeViewTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.TreeViewTheme
		End If
		If theme IsNot Nothing Then
			Me.UpdateHorizontalScrollbar()
			Me.UpdateVerticalScrollbar()

			If Me.CustomHorizontalScrollbar.Visible AndAlso Me.CustomVerticalScrollBar.Visible Then
				' +1 for TreeView padding
				If Me.theBorderStyle = Windows.Forms.BorderStyle.FixedSingle Then
					Me.ScrollbarCornerPanel.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize + Me.theBorderWidth + 1, ScrollBarEx.Consts.ScrollBarSize + Me.theBorderWidth + 1)
					Me.ScrollbarCornerPanel.RightAndBottomBorderWidth = 1
				Else
					Me.ScrollbarCornerPanel.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize + 1, ScrollBarEx.Consts.ScrollBarSize + 1)
					Me.ScrollbarCornerPanel.RightAndBottomBorderWidth = 0
				End If
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

	'Private Sub UpdateHorizontalScrollbar()
	'	'NOTE: Parent can be Nothing on exiting. Prevent the exception with this check.
	'	'If Not Me.theScrollingIsActive AndAlso Me.Parent IsNot Nothing AndAlso Me.theAutoScroll Then
	'	If Not Me.theScrollingIsActive AndAlso Me.Parent IsNot Nothing AndAlso Me.Scrollable Then
	'		Dim contentSize As Size = Me.GetContentSize()
	'		Dim contentWidth As Integer = contentSize.Width
	'		Dim clientWidth As Integer = Me.ClientRectangle.Width
	'		Dim contentHeight As Integer = contentSize.Height
	'		Dim clientHeight As Integer = Me.ClientRectangle.Height
	'		'If contentHeight > clientHeight AndAlso Me.theAutoScroll Then
	'		'	'clientWidth -= ScrollBarEx.Consts.ScrollBarSize
	'		'End If
	'		If contentWidth > clientWidth Then
	'			Me.theScrollingIsActive = True

	'			Me.CustomHorizontalScrollbar.Minimum = 0
	'			Me.CustomHorizontalScrollbar.Maximum = contentWidth
	'			Me.CustomHorizontalScrollbar.Value = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_HORZ)
	'			Me.CustomHorizontalScrollbar.ViewSize = clientWidth
	'			Me.CustomHorizontalScrollbar.SmallChange = 5
	'			Me.CustomHorizontalScrollbar.LargeChange = clientWidth - 5 * 2

	'			'NOTE: Assign to Parent so it can draw over non-client area of RichTextBoxEx.
	'			Me.CustomHorizontalScrollbar.Parent = Me.Parent
	'			Me.CustomHorizontalScrollbar.BringToFront()
	'			'NOTE: Point is relative to Me.ClientRectangle.
	'			Dim aPoint As New Point(Me.ClientRectangle.Left, Me.ClientRectangle.Height)
	'			'Dim aPoint As New Point(Me.ClientRectangle.Left - Me.NonClientPadding.Left, Me.ClientRectangle.Height + Me.NonClientPadding.Bottom - ScrollBarEx.Consts.ScrollBarSize)
	'			'Dim aPoint As New Point(Me.ClientRectangle.Left - Me.NonClientPadding.Left, Me.ClientRectangle.Height + Me.NonClientPadding.Bottom - ScrollBarEx.Consts.ScrollBarSize * 2 - 5)
	'			'NOTE: Location must be relative to Parent.
	'			aPoint = Me.PointToScreen(aPoint)
	'			aPoint = Me.CustomHorizontalScrollbar.Parent.PointToClient(aPoint)
	'			Me.CustomHorizontalScrollbar.Location = aPoint
	'			'Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.Width, ScrollBarEx.Consts.ScrollBarSize)
	'			'Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.NonClientPadding.Left + Me.ClientRectangle.Left + Me.ClientRectangle.Width + Me.NonClientPadding.Right, ScrollBarEx.Consts.ScrollBarSize)
	'			Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.ClientRectangle.Width, ScrollBarEx.Consts.ScrollBarSize)
	'			Me.CustomHorizontalScrollbar.Show()

	'			Me.theScrollingIsActive = False
	'		Else
	'			Me.theScrollingIsActive = True
	'			Me.CustomHorizontalScrollbar.Hide()
	'			Me.theScrollingIsActive = False
	'		End If
	'	End If
	'End Sub

	'Private Sub UpdateVerticalScrollbar()
	'	'NOTE: Parent can be Nothing on exiting. Prevent the exception with this check.
	'	'If Not Me.theScrollingIsActive AndAlso Me.Parent IsNot Nothing AndAlso Me.theAutoScroll Then
	'	If Not Me.theScrollingIsActive AndAlso Me.Parent IsNot Nothing AndAlso Me.Scrollable Then
	'		Dim contentSize As Size = Me.GetContentSize()
	'		Dim contentHeight As Integer = contentSize.Height()
	'		Dim clientHeight As Integer = Me.ClientRectangle.Height
	'		If contentHeight > clientHeight Then
	'			Me.theScrollingIsActive = True

	'			'Me.CustomVerticalScrollBar.Minimum = 0
	'			'Me.CustomVerticalScrollBar.Maximum = contentHeight
	'			''Me.CustomVerticalScrollBar.Value = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_VERT)
	'			'Me.CustomVerticalScrollBar.ViewSize = clientHeight
	'			'Me.CustomVerticalScrollBar.SmallChange = Me.ItemHeight
	'			'Me.CustomVerticalScrollBar.LargeChange = Me.ItemHeight * 4
	'			'------
	'			Me.CustomVerticalScrollBar.Minimum = 0
	'			Me.CustomVerticalScrollBar.Maximum = contentHeight \ Me.ItemHeight
	'			Me.CustomVerticalScrollBar.Value = Win32Api.GetScrollPos(Me.Handle, Win32Api.ScrollBarType.SB_VERT)
	'			' The -1 is needed for scrolling to partially-shown bottom-most node in tree.
	'			Me.CustomVerticalScrollBar.ViewSize = (clientHeight \ Me.ItemHeight) - 1
	'			Me.CustomVerticalScrollBar.SmallChange = 1
	'			Me.CustomVerticalScrollBar.LargeChange = 4

	'			'NOTE: Assign to Parent so it can draw over non-client area.
	'			Me.CustomVerticalScrollBar.Parent = Me.Parent
	'			Me.CustomVerticalScrollBar.BringToFront()
	'			'NOTE: Point is relative to Me.ClientRectangle.
	'			Dim aPoint As New Point(Me.ClientRectangle.Width, Me.ClientRectangle.Top)
	'			'Dim aPoint As New Point(Me.ClientRectangle.Width + Me.NonClientPadding.Right - ScrollBarEx.Consts.ScrollBarSize, Me.ClientRectangle.Top - Me.NonClientPadding.Top)
	'			'Dim aPoint As New Point(Me.ClientRectangle.Width + Me.NonClientPadding.Right - ScrollBarEx.Consts.ScrollBarSize * 2 - 5, Me.ClientRectangle.Top - Me.NonClientPadding.Top)
	'			'NOTE: Location must be relative to Parent.
	'			aPoint = Me.PointToScreen(aPoint)
	'			aPoint = Me.CustomVerticalScrollBar.Parent.PointToClient(aPoint)
	'			Me.CustomVerticalScrollBar.Location = aPoint
	'			'Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.Height)
	'			'Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.ClientRectangle.Top + Me.NonClientPadding.Top + Me.ClientRectangle.Height + Me.NonClientPadding.Bottom)
	'			Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.ClientRectangle.Height)
	'			Me.CustomVerticalScrollBar.Show()

	'			Me.theScrollingIsActive = False
	'		Else
	'			Me.theScrollingIsActive = True
	'			Me.CustomVerticalScrollBar.Hide()
	'			Me.theScrollingIsActive = False
	'		End If
	'	End If
	'End Sub

	Private Sub UpdateHorizontalScrollbar()
		'NOTE: Parent can be Nothing on exiting. Prevent the exception with this check.
		If Not Me.theScrollingIsActive AndAlso Me.Parent IsNot Nothing AndAlso Me.Scrollable Then
			'If Not Me.theScrollingIsActive Then
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
					Me.CustomHorizontalScrollbar.ViewSize = Me.ClientRectangle.Width
					Me.CustomHorizontalScrollbar.SmallChange = 5
					Me.CustomHorizontalScrollbar.LargeChange = scrollInfo.nPage
				End If

				'NOTE: Assign to Parent so it can draw over non-client area.
				Me.CustomHorizontalScrollbar.Parent = Me.Parent
				Me.CustomHorizontalScrollbar.BringToFront()
				' -1 for TreeView padding
				Dim aPoint As New Point(Me.ClientRectangle.Left - 1, Me.ClientRectangle.Height)
				'NOTE: Location must be relative to Parent.
				aPoint = Me.PointToScreen(aPoint)
				aPoint = Me.CustomHorizontalScrollbar.Parent.PointToClient(aPoint)
				Me.CustomHorizontalScrollbar.Location = aPoint
				' +2,+1 for TreeView padding
				If Me.theBorderStyle = Windows.Forms.BorderStyle.FixedSingle Then
					Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.ClientRectangle.Width + 2, ScrollBarEx.Consts.ScrollBarSize + Me.theBorderWidth + 1)
					Me.CustomHorizontalScrollbar.RightAndBottomBorderWidth = 1
				Else
					Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.ClientRectangle.Width + 2, ScrollBarEx.Consts.ScrollBarSize + 1)
					Me.CustomHorizontalScrollbar.RightAndBottomBorderWidth = 0
				End If
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
		If Not Me.theScrollingIsActive AndAlso Me.Parent IsNot Nothing AndAlso Me.Scrollable Then
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
					' The -1 is needed for scrolling to partially-shown bottom-most node in tree.
					Me.CustomVerticalScrollBar.ViewSize = (Me.ClientRectangle.Height \ Me.ItemHeight) - 1
					Me.CustomVerticalScrollBar.SmallChange = 1
					Me.CustomVerticalScrollBar.LargeChange = scrollInfo.nPage
				End If

				'NOTE: Assign to Parent so it can draw over non-client area.
				Me.CustomVerticalScrollBar.Parent = Me.Parent
				Me.CustomVerticalScrollBar.BringToFront()
				' -1 for TreeView padding
				Dim aPoint As New Point(Me.ClientRectangle.Width, Me.ClientRectangle.Top - 1)
				'NOTE: Location must be relative to Parent.
				aPoint = Me.PointToScreen(aPoint)
				aPoint = Me.CustomVerticalScrollBar.Parent.PointToClient(aPoint)
				Me.CustomVerticalScrollBar.Location = aPoint
				' +1,+2 for TreeView padding
				If Me.theBorderStyle = Windows.Forms.BorderStyle.FixedSingle Then
					Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize + Me.theBorderWidth + 1, Me.ClientRectangle.Height + 2)
					Me.CustomVerticalScrollBar.RightAndBottomBorderWidth = 1
				Else
					Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize + 1, Me.ClientRectangle.Height + 2)
					Me.CustomVerticalScrollBar.RightAndBottomBorderWidth = 0
				End If
				Me.CustomVerticalScrollBar.Show()

				Me.theScrollingIsActive = False
		Else
		Me.theScrollingIsActive = True
		Me.CustomVerticalScrollBar.Hide()
		Me.theScrollingIsActive = False
		End If
		End If
	End Sub

#End Region

#Region "Data"

	Private theBorderColor As Color
	Private theBorderStyle As BorderStyle
	Private theBorderWidth As Integer
	Private NonClientPadding As Padding
	'Private theAutoScroll As Boolean
	'Private CustomHorizontalScrollbarPopup As Popup
	Private WithEvents CustomHorizontalScrollbar As ScrollBarEx
	Private WithEvents CustomVerticalScrollBar As ScrollBarEx
	Private ScrollbarCornerPanel As ScrollBarPanel
	Private theControlHasShown As Boolean
	Private theScrollingIsActive As Boolean
	Private theMouseWheelHasMoved As Boolean
	Private theOriginalFont As Font

	Private theTreePlusIcon As VisualStyleRenderer
	Private theTreeMinusIcon As VisualStyleRenderer
	Private theTextFormatFlags As TextFormatFlags

	Private ReadOnly theNameToTreeNodeMap As Dictionary(Of String, TreeNode)

#End Region

End Class
