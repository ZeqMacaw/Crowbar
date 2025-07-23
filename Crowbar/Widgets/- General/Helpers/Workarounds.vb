Module Workarounds

	' Call this in Form.Load() or UserControl.Load().
	' Input is the sibling control to the immediate right or the parent control of the anchoredWidget.
	Public Sub WorkaroundForFrameworkAnchorRightSizingBug(ByVal anchoredWidget As Control, ByVal siblingOrParentWidget As Control, Optional ByVal widgetIsParent As Boolean = False)
		If widgetIsParent Then
			anchoredWidget.Size = New System.Drawing.Size(siblingOrParentWidget.Width - siblingOrParentWidget.Padding.Right - anchoredWidget.Margin.Right - anchoredWidget.Left, anchoredWidget.Height)
		Else
			anchoredWidget.Size = New System.Drawing.Size(siblingOrParentWidget.Left - siblingOrParentWidget.Margin.Left - anchoredWidget.Margin.Right - anchoredWidget.Left, anchoredWidget.Height)
		End If
	End Sub

	' Call this in Form.Load() or UserControl.Load().
	Public Sub WorkaroundForFrameworkAnchorRightLocationBug(ByVal anchoredWidget As Control)
		Dim parentWidget As Control = anchoredWidget.Parent
		anchoredWidget.Left = parentWidget.Left + parentWidget.Width - parentWidget.Padding.Right - anchoredWidget.Margin.Right - anchoredWidget.Width
	End Sub

End Module
