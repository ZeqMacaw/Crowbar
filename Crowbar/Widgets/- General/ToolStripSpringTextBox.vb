'FROM: How to: Stretch a ToolStripTextBox to Fill the Remaining Width of a ToolStrip (Windows Forms)
'      https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/stretch-a-toolstriptextbox-to-fill-the-remaining-width-of-a-toolstrip-wf
'      05-Aug-2017

Public Class ToolStripSpringTextBox
	Inherits ToolStripTextBox

	'IMPORTANT: The control must have AutoSize = True for this to be called.
	Public Overrides Function GetPreferredSize(ByVal constrainingSize As Size) As Size

		' Use the default size if the text box is on the overflow menu
		' or is on a vertical ToolStrip.
		If IsOnOverflow Or Owner.Orientation = Orientation.Vertical Then
			Return DefaultSize
		End If

		' Declare a variable to store the total available width as 
		' it is calculated, starting with the display width of the 
		' owning ToolStrip.
		Dim width As Int32 = Owner.DisplayRectangle.Width

		' Subtract the width of the overflow button if it is displayed. 
		If Owner.OverflowButton.Visible Then
			width = width - Owner.OverflowButton.Width - Owner.OverflowButton.Margin.Horizontal()
		End If

		' Declare a variable to maintain a count of ToolStripSpringTextBox 
		' items currently displayed in the owning ToolStrip. 
		Dim springBoxCount As Int32 = 0

		For Each item As ToolStripItem In Owner.Items

			' Ignore items on the overflow menu.
			If item.IsOnOverflow Then Continue For

			If TypeOf item Is ToolStripSpringTextBox Then
				' For ToolStripSpringTextBox items, increment the count and 
				' subtract the margin width from the total available width.
				springBoxCount += 1
				width -= item.Margin.Horizontal
			Else
				' For all other items, subtract the full width from the total
				' available width.
				width = width - item.Width - item.Margin.Horizontal
			End If
		Next

		' If there are multiple ToolStripSpringTextBox items in the owning
		' ToolStrip, divide the total available width between them. 
		If springBoxCount > 1 Then width = CInt(width / springBoxCount)

		' If the available width is less than the default width, use the
		' default width, forcing one or more items onto the overflow menu.
		If width < DefaultSize.Width Then width = DefaultSize.Width

		' Retrieve the preferred size from the base class, but change the
		' width to the calculated width. 
		Dim preferredSize As Size = MyBase.GetPreferredSize(constrainingSize)
		preferredSize.Width = width
		Return preferredSize

	End Function

End Class
