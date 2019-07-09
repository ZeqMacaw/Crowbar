Imports Microsoft.VisualBasic.ApplicationServices

Public Class SingleInstanceApplication
	Inherits WindowsFormsApplicationBase

	Public Overloads Shared Sub Run(ByVal form As Form, ByVal handler As EventHandler(Of SingleInstanceEventArgs))
		AddHandler SingleInstanceEvent, handler
		applicationBase = New SingleInstanceApplication()
		applicationBase.MainForm = form
		AddHandler applicationBase.StartupNextInstance, AddressOf StartupNextInstanceEventHandler
		applicationBase.Run(Environment.GetCommandLineArgs())
	End Sub

	Private Shared Sub StartupNextInstanceEventHandler(ByVal sender As Object, ByVal e As StartupNextInstanceEventArgs)
		RaiseEvent SingleInstanceEvent(applicationBase, New SingleInstanceEventArgs(e.CommandLine, applicationBase.MainForm))
	End Sub

	Private Shared Event SingleInstanceEvent As EventHandler(Of SingleInstanceEventArgs)
	Private Shared applicationBase As SingleInstanceApplication

	Private Sub New()
		MyBase.IsSingleInstance = True
	End Sub

End Class
