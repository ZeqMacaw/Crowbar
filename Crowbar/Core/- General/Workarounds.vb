Module Workarounds

	' Call this in Form.Load() or UserControl.Load().
	' Input is the sibling control to the immediate right or the parent control of this control.
	Public Sub WorkaroundForFrameworkAnchorRightSizingBug(ByVal anchoredWidget As Control, ByVal siblingOrParentWidget As Control, Optional ByVal widgetIsParent As Boolean = False)
		If widgetIsParent Then
			anchoredWidget.Size = New System.Drawing.Size(siblingOrParentWidget.Width - siblingOrParentWidget.Padding.Right - anchoredWidget.Margin.Right - anchoredWidget.Left, anchoredWidget.Height)
		Else
			anchoredWidget.Size = New System.Drawing.Size(siblingOrParentWidget.Left - siblingOrParentWidget.Margin.Left - anchoredWidget.Margin.Right - anchoredWidget.Left, anchoredWidget.Height)
		End If
	End Sub

End Module
