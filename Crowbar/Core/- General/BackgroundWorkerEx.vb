Imports System.ComponentModel

Public Class BackgroundWorkerEx
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Shared Function RunBackgroundWorker(ByVal bw As BackgroundWorkerEx, ByVal bw_DoWork As DoWorkEventHandler, ByVal bw_ProgressChanged As ProgressChangedEventHandler, ByVal bw_RunWorkerCompleted As RunWorkerCompletedEventHandler, ByVal bw_argument As Object) As BackgroundWorkerEx
		If bw Is Nothing Then
			bw = New BackgroundWorkerEx()
			bw.WorkerSupportsCancellation = True
			bw.WorkerReportsProgress = True
		Else
			While bw.IsBusy
				bw.CancelAsync()
				Application.DoEvents()
			End While
		End If

		bw.DoWorkHandler = bw_DoWork
		bw.ProgressChangedHandler = bw_ProgressChanged
		AddHandler bw.RunWorkerCompleted, AddressOf bw.BWE_RunWorkerCompleted
		bw.ExternalRunWorkerCompletedHandler = bw_RunWorkerCompleted

		bw.RunWorkerAsync(bw_argument)

		Return bw
	End Function

#End Region

#Region "Properties"

	Public Property DoWorkHandler() As DoWorkEventHandler
		Get
			Return Me.theDoWorkHandler
		End Get
		Set
			Me.theDoWorkHandler = Value
			AddHandler Me.DoWork, Me.theDoWorkHandler
		End Set
	End Property

	Public Property ProgressChangedHandler() As ProgressChangedEventHandler
		Get
			Return Me.theProgressChangedHandler
		End Get
		Set
			Me.theProgressChangedHandler = Value
			AddHandler Me.ProgressChanged, Me.theProgressChangedHandler
		End Set
	End Property

	Public Property ExternalRunWorkerCompletedHandler() As RunWorkerCompletedEventHandler
		Get
			Return Me.theExternalRunWorkerCompletedHandler
		End Get
		Set
			Me.theExternalRunWorkerCompletedHandler = Value
		End Set
	End Property

#End Region

#Region "Methods"

	Public Sub Kill()
		RemoveHandler Me.DoWork, Me.theDoWorkHandler
		RemoveHandler Me.ProgressChanged, Me.theProgressChangedHandler
		RemoveHandler Me.RunWorkerCompleted, AddressOf Me.BWE_RunWorkerCompleted
	End Sub

#End Region

#Region "Private Methods"

	Private Sub BWE_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		'Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)

		'RemoveHandler bw.DoWork, Me.theDoWorkHandler
		'RemoveHandler bw.ProgressChanged, Me.theProgressChangedHandler
		'RemoveHandler bw.RunWorkerCompleted, AddressOf Me.BWE_RunWorkerCompleted
		'Me.theExternalRunWorkerCompletedHandler(bw, e)
		'======
		Me.Kill()
		Me.theExternalRunWorkerCompletedHandler(Me, e)
	End Sub

#End Region

#Region "Data"

	Private theDoWorkHandler As DoWorkEventHandler
	Private theProgressChangedHandler As ProgressChangedEventHandler
	Private theExternalRunWorkerCompletedHandler As RunWorkerCompletedEventHandler

#End Region

End Class
