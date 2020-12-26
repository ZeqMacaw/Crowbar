Imports System.Xml.Serialization

' This class, along with XmlElement(Type) tag, allows XML serializing/deserializing 
'     of the System.Drawing.Color object into a readable color name.
'     Without this class, would have to use two properties instead of the one.
'
' Example usage:
'
'<XmlElement(Type:=GetType(XmlColor))>
'Public Property SomeColor() As Color
'	Get
'		Return Me.theSomeColor
'	End Get
'	Set(ByVal value As Color)
'		Me.theSomeColor = value
'	End Set
'End Property

Public Class XmlColor
	Private theColor As Color

	Public Sub New()
	End Sub

	Public Sub New(ByVal color As Color)
		Me.theColor = color
	End Sub

	<XmlText()>
	Public Property ColorName() As String
		Get
			Return ColorTranslator.ToHtml(Me.theColor)
		End Get
		Set(ByVal value As String)
			Me.theColor = ColorTranslator.FromHtml(value)
		End Set
	End Property

	<XmlIgnore()>
	Public Property Argb As Integer
		Get
			Return Me.theColor.ToArgb()
		End Get
		Set(ByVal value As Integer)
			Me.theColor = Color.FromArgb(value)
		End Set
	End Property

	Public Shared Widening Operator CType(ByVal cw As XmlColor) As Color
		Return cw.theColor
	End Operator

	Public Shared Narrowing Operator CType(ByVal b As Color) As XmlColor
		Return New XmlColor(b)
	End Operator

End Class
