Public Class TabPageEx
	Inherits TabPage

	Public Sub New()
		MyBase.New()

		'' This should allow Forms that inherit from this class and their widgets to use the system font instead of Visual Studio's default of Microsoft Sans Serif.
		'Me.Font = New Font(SystemFonts.MessageBoxFont.Name, 8.25)

		Me.theRealText = ""
		Me.theTextIsBeingSet = False
	End Sub

	'Public Overrides Property Text() As String
	'	Get
	'		If Me.theTextIsBeingSet Then
	'			Return MyBase.Text
	'		Else
	'			Return Me.theRealText
	'		End If
	'	End Get
	'	Set(ByVal value As String)
	'		Me.theTextIsBeingSet = True
	'		Me.theRealText = value
	'		If value IsNot Nothing Then
	'			'Dim temp As String = value.Remove(value.Length - 1)
	'			Dim temp As String = ""
	'			For Each valueChar As Char In value
	'				temp += "1"
	'			Next
	'			MyBase.Text = temp
	'			'MyBase.Text = "WWWW"
	'		End If
	'		'MyBase.Text = value
	'		Me.theTextIsBeingSet = False
	'	End Set
	'End Property

	' Inexcplicably, Windows calculates a large amount of left and right padding on owner-drawn tabs.
	'    To workaround this, store shorter text in the Text property, but actually draw the text from this variable.
	Private theRealText As String
	Private theTextIsBeingSet As Boolean

End Class
