Public Class BaseUserControl

	Public Sub New()

		'TEST: See if this prevents the overlapping or larger text on Chinese Windows.
		' This should allow Forms that inherit from this class and their widgets to use the system font instead of Visual Studio's default of Microsoft Sans Serif.
		Me.Font = New Font(SystemFonts.MessageBoxFont.Name, 8.25)

		' This call is required by the designer.
		InitializeComponent()

		Me.theControlIsInDesignMode = False
	End Sub

	'UserControl overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing Then
				Me.Free()
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	Protected Overridable Sub Init()
	End Sub

	Protected Overridable Sub Free()
	End Sub

	Private Sub BaseUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
		If Not Me.DesignMode Then
			Me.Init()
			Me.theControlIsInDesignMode = True
		End If
	End Sub

	Protected theControlIsInDesignMode As Boolean

End Class
