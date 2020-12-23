Imports System.Drawing.Drawing2D

Public Class TreeViewEx
	Inherits TreeView

	Public Sub New()
		MyBase.New()
	End Sub

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

End Class
