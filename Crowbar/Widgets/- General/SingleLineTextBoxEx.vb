Imports System.ComponentModel

Public Class SingleLineTextBoxEx
	Inherits Control

	Public Sub New()
		MyBase.New()

		'Me.BorderStyle = BorderStyle.FixedSingle
		Me.ForeColor = WidgetTextColor
		Me.BackColor = WidgetDeepBackColor
		Me.theCueBannerText = ""
	End Sub

	Public Property BorderStyle() As BorderStyle
		Get
			Return Me.theBorderStyle
		End Get
		Set
			Me.theBorderStyle = Value
		End Set
	End Property

	Public Property MaxLength() As Int32
		Get
			Return Me.theMaxLength
		End Get
		Set
			If Me.theMaxLength <> Value Then
				Me.theMaxLength = Value
			End If
		End Set
	End Property

	Public Property [ReadOnly]() As Boolean
		Get
			Return Me.theControlIsReadOnly
		End Get
		Set
			If Me.theControlIsReadOnly <> Value Then
				Me.theControlIsReadOnly = Value

				If Me.theControlIsReadOnly Then
					Me.ForeColor = WidgetDisabledTextColor
					Me.BackColor = WidgetDeepDisabledBackColor
				Else
					Me.ForeColor = WidgetTextColor
					Me.BackColor = WidgetDeepBackColor
				End If
			End If
		End Set
	End Property

	Public Property WordWrap() As Boolean
		Get
			Return Me.theControlIsWordWrapped
		End Get
		Set
			If Me.theControlIsWordWrapped <> Value Then
				Me.theControlIsWordWrapped = Value
			End If
		End Set
	End Property

	<Browsable(True)>
	<Category("Appearance")>
	<Description("Sets the text of the cue (dimmed text that only shows when Text property is empty).")>
	Public Property CueBannerText As String
		Get
			Return Me.theCueBannerText
		End Get
		Set
			Me.theCueBannerText = Value
		End Set
	End Property

	Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
		If e.Control AndAlso e.KeyCode = Keys.A Then
			Me.SelectAll()
		End If
		MyBase.OnKeyDown(e)
	End Sub

	Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If e.KeyChar = ChrW(Keys.Return) Then
			Try
				' Cause validation, which means Validating and Validated events are raised.
				Me.FindForm().Validate()
				If TypeOf Me.Parent Is ContainerControl Then
					CType(Me.Parent, ContainerControl).Validate()
				End If
				'NOTE: Prevent annoying beep when textbox is single line.
				e.Handled = True
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
		MyBase.OnKeyPress(e)
	End Sub

	'Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
	'	'MyBase.OnPaintBackground(e)
	'	Using sb As New SolidBrush(Color.Red)
	'		e.Graphics.FillRectangle(sb, Me.ClientRectangle)
	'		sb.Dispose()
	'	End Using
	'End Sub

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		'MyBase.OnPaint(e)

		' Draw background border.
		Using backColorBrush As New SolidBrush(WidgetDeepDisabledBackColor)
			Dim aRect As Rectangle = e.ClipRectangle
			e.Graphics.FillRectangle(backColorBrush, aRect)
		End Using

		' Draw background.
		Using backColorBrush As New SolidBrush(WidgetDeepBackColor)
			Dim aRect As Rectangle = e.ClipRectangle
			aRect.Inflate(-1, -1)
			e.Graphics.FillRectangle(backColorBrush, aRect)
		End Using

		' Draw text.
		If Me.Text <> "" AndAlso Me.theOriginalFont IsNot Nothing Then
			'TextRenderer.DrawText(e.Graphics, Me.Text, Me.theOriginalFont, e.ClipRectangle, WidgetTextColor, WidgetDeepBackColor, TextFormatFlags.VerticalCenter)
			TextRenderer.DrawText(e.Graphics, Me.Text, Me.theOriginalFont, e.ClipRectangle, WidgetTextColor, WidgetDeepBackColor, TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)
		End If

		' Draw cue banner text.
		If Me.theCueBannerText <> "" AndAlso Me.Text = "" AndAlso Me.theOriginalFont IsNot Nothing Then
			Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, FontStyle.Italic, Me.theOriginalFont.Unit)
			'TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(1, 0), WidgetDisabledTextColor, WidgetDeepBackColor)
			TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, e.ClipRectangle, WidgetDisabledTextColor, WidgetDeepBackColor, TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)
		End If
	End Sub

	Protected Overrides Sub OnTextChanged(e As EventArgs)
		MyBase.OnTextChanged(e)

		If Me.theOriginalFont Is Nothing Then
			Exit Sub
		End If

		If GetStyle(ControlStyles.UserPaint) <> (Me.theCueBannerText <> "" AndAlso Me.Text = "") Then
			SetStyle(ControlStyles.AllPaintingInWmPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.DoubleBuffer, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			If Me.theOriginalFont IsNot Nothing Then
				Me.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, Me.theOriginalFont.Style, Me.theOriginalFont.Unit)
			End If
			Me.Invalidate()
		End If
	End Sub

	Protected Overrides Sub OnVisibleChanged(e As EventArgs)
		MyBase.OnVisibleChanged(e)

		If Me.theOriginalFont Is Nothing Then
			'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
			'      so save the Font after widget is visible for first time, but before changing style within the widget.
			Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)

			SetStyle(ControlStyles.AllPaintingInWmPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.DoubleBuffer, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
		End If
	End Sub

	Private Sub SelectAll()

	End Sub

	Private theBorderStyle As BorderStyle
	Private theMaxLength As Int32
	Private theControlIsReadOnly As Boolean
	Private theControlIsWordWrapped As Boolean

	Private theCueBannerText As String
	Private theOriginalFont As Font

End Class
