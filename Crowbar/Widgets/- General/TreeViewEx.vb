Imports System.Drawing.Drawing2D

' Much faster filling of TreeView by using internal Dictionary.ContainsKey() rather than TreeNode.Nodes.ContainsKey().
Public Class TreeViewEx
	Inherits TreeView

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

	'			If parentId <> "" Then
	'				childItem = parentItem
	'				childNode = parentNode
	'			Else
	'				Exit While
	'			End If
	'		End While
	'	Next
	'End Sub
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

	Delegate Function GetTagDelegate(Of ItemType, Tagtype)(ByRef item As ItemType, ByVal leafItem As ItemType, ByVal tag As Tagtype, ByVal blah As Boolean, ByVal blah2 As Boolean) As Tagtype

	Protected Overrides Sub OnDrawNode(e As DrawTreeNodeEventArgs)
		If e.Node.Bounds.IsEmpty Then
			Exit Sub
		End If

		e.DrawDefault = True
		If (e.State And TreeNodeStates.Selected) = TreeNodeStates.Selected Then
			If (e.State And TreeNodeStates.Focused) = TreeNodeStates.Focused Then
				'e.Graphics.FillRectangle(Brushes.Red, e.Bounds)
				'TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.NodeFont, e.Bounds, e.Node.ForeColor, e.Node.BackColor, TextFormatFlags.GlyphOverhangPadding)
				'e.DrawDefault = False
			Else
				e.Graphics.FillRectangle(SystemBrushes.ControlDark, e.Bounds)
				TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.NodeFont, e.Bounds, e.Node.ForeColor, e.Node.BackColor, TextFormatFlags.GlyphOverhangPadding)
				e.DrawDefault = False
			End If
		End If

		MyBase.OnDrawNode(e)
	End Sub

	Private ReadOnly theNameToTreeNodeMap As Dictionary(Of String, TreeNode)

End Class
