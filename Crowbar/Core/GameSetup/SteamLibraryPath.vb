Imports System.ComponentModel
Imports System.Xml.Serialization

Public Class SteamLibraryPath
	Implements ICloneable
	Implements INotifyPropertyChanged

#Region "Create and Destroy"

	Public Sub New()
		'MyBase.New()

		Me.theMacro = "<library1>"
		Me.thePath = "C:\Program Files (x86)\Steam"
		Me.theUseCount = 0
	End Sub

	Protected Sub New(ByVal originalObject As SteamLibraryPath)
		Me.theMacro = originalObject.Macro()
		Me.thePath = originalObject.LibraryPath
		Me.theUseCount = originalObject.UseCount
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return New SteamLibraryPath(Me)
	End Function

#End Region

#Region "Properties"

	Public Property Macro() As String
		Get
			Return Me.theMacro
		End Get
		Set(ByVal value As String)
			If Me.theMacro <> value Then
				Me.theMacro = value
				NotifyPropertyChanged("Macro")
			End If
		End Set
	End Property

	Public Property LibraryPath() As String
		Get
			Return Me.thePath
		End Get
		Set(ByVal value As String)
			Me.thePath = value
			NotifyPropertyChanged("LibraryPath")
		End Set
	End Property

	Public Property UseCount() As Integer
		Get
			Return Me.theUseCount
		End Get
		Set(ByVal value As Integer)
			Me.theUseCount = value
			NotifyPropertyChanged("UseCount")
		End Set
	End Property

#End Region

#Region "Methods"

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

	Private theMacro As String
	Private thePath As String
	Private theUseCount As Integer

#End Region

End Class
