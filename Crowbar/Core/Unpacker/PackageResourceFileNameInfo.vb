Imports System.ComponentModel

Public Class PackageResourceFileNameInfo
	Implements ICloneable
	Implements INotifyPropertyChanged

	Public Sub New()
		'MyBase.New()
	End Sub

	Protected Sub New(ByVal originalObject As PackageResourceFileNameInfo)
		Me.thePathFileName = originalObject.PathFileName
		Me.theName = originalObject.Name
		Me.theSize = originalObject.Size
		Me.theCount = originalObject.Count
		Me.theType = originalObject.Type
		Me.theExtension = originalObject.Extension
		Me.theResourceFileIsFolder = originalObject.IsFolder
		Me.theArchivePathFileName = originalObject.ArchivePathFileName
		Me.thePathFileNameExists = originalObject.ArchivePathFileNameExists
		Me.theEntryIndex = originalObject.EntryIndex
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return New PackageResourceFileNameInfo(Me)
	End Function

#Region "Properties"

	Public Property PathFileName() As String
		Get
			Return Me.thePathFileName
		End Get
		Set(ByVal value As String)
			Me.thePathFileName = value
			NotifyPropertyChanged("PathFileName")
		End Set
	End Property

	Public Property Name() As String
		Get
			Return Me.theName
		End Get
		Set(ByVal value As String)
			Me.theName = value
			NotifyPropertyChanged("Name")
		End Set
	End Property

	Public Property Size() As UInt64
		Get
			Return Me.theSize
		End Get
		Set(ByVal value As UInt64)
			Me.theSize = value
			NotifyPropertyChanged("Size")
		End Set
	End Property

	Public Property Count() As UInt64
		Get
			Return Me.theCount
		End Get
		Set(ByVal value As UInt64)
			Me.theCount = value
			NotifyPropertyChanged("Count")
		End Set
	End Property

	Public Property Type() As String
		Get
			Return Me.theType
		End Get
		Set(ByVal value As String)
			Me.theType = value
			NotifyPropertyChanged("Type")
		End Set
	End Property

	Public Property Extension() As String
		Get
			Return Me.theExtension
		End Get
		Set(ByVal value As String)
			Me.theExtension = value
			NotifyPropertyChanged("Extension")
		End Set
	End Property

	Public Property IsFolder() As Boolean
		Get
			Return Me.theResourceFileIsFolder
		End Get
		Set(ByVal value As Boolean)
			Me.theResourceFileIsFolder = value
			NotifyPropertyChanged("IsFolder")
		End Set
	End Property

	Public Property ArchivePathFileName() As String
		Get
			Return Me.theArchivePathFileName
		End Get
		Set(ByVal value As String)
			Me.theArchivePathFileName = value
			NotifyPropertyChanged("ArchivePathFileName")
		End Set
	End Property

	Public Property ArchivePathFileNameExists() As Boolean
		Get
			Return Me.thePathFileNameExists
		End Get
		Set(ByVal value As Boolean)
			Me.thePathFileNameExists = value
			NotifyPropertyChanged("PathFileNameExists")
		End Set
	End Property

	Public Property EntryIndex() As Integer
		Get
			Return Me.theEntryIndex
		End Get
		Set(ByVal value As Integer)
			Me.theEntryIndex = value
			NotifyPropertyChanged("EntryIndex")
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

	Private theArchivePathFileName As String

	Private thePathFileName As String
	Private thePathFileNameExists As Boolean

	Private theName As String
	Private theSize As UInt64
	Private theCount As UInt64
	Private theType As String
	Private theExtension As String

	Private theResourceFileIsFolder As Boolean

	Private theEntryIndex As Integer

#End Region

End Class
