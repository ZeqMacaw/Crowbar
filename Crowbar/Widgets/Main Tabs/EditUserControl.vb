Public Class EditUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

	End Sub

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

#End Region

#Region "Init and Free"

	Protected Overrides Sub Init()
		MyBase.Init()

		' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
		If TheApp Is Nothing Then
			Exit Sub
		End If
	End Sub

	Protected Overrides Sub Free()
		MyBase.Free()

		' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
		If Not Me.InitHasBeenCalled OrElse TheApp Is Nothing Then
			Exit Sub
		End If
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

	Private Sub EditUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
		' [04-Feb-2026] Me.DesignMode is unreliable in nested widgets.
		'If Not Me.DesignMode Then
		Me.Init()
		'End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

#End Region

End Class
