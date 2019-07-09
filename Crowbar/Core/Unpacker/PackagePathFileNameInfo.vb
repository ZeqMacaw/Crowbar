Imports System.ComponentModel

Public Class PackagePathFileNameInfo
	Implements INotifyPropertyChanged

#Region "Properties"

	Public Property RelativePathFileName() As String
		Get
			Return Me.theRelativePathFileName
		End Get
		Set(ByVal value As String)
			Me.theRelativePathFileName = value
			NotifyPropertyChanged("RelativePathFileName")
		End Set
	End Property

	Public Property PathFileName() As String
		Get
			Return Me.thePathFileName
		End Get
		Set(ByVal value As String)
			Me.thePathFileName = value
			NotifyPropertyChanged("PathFileName")
		End Set
	End Property

#End Region

#Region "Events"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#End Region

#Region "Private Methods"

	Protected Sub NotifyPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

#End Region

#Region "Data"

	Private theRelativePathFileName As String
	Private thePathFileName As String

#End Region

End Class
