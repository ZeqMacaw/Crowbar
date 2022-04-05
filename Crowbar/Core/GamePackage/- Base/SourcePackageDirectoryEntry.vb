Imports System.ComponentModel

Public Class SourcePackageDirectoryEntry
	Implements ICloneable
	Implements INotifyPropertyChanged

	Public Sub New()
		MyBase.New()
	End Sub

	Protected Sub New(ByVal originalObject As SourcePackageDirectoryEntry)
		Me.thePackageDataPathFileName = originalObject.PackageDataPathFileName
		Me.thePackageDataPathFileNameExists = originalObject.PackageDataPathFileNameExists

		Me.theEntryIsFolder = originalObject.IsFolder
		Me.theName = originalObject.Name
		Me.theSize = originalObject.Size
		Me.theCount = originalObject.Count
		Me.theType = originalObject.Type
		Me.theExtension = originalObject.Extension

		Me.theDisplayPathFileName = originalObject.DisplayPathFileName
		Me.thePathFileName = originalObject.PathFileName
		Me.theDataOffset = originalObject.DataOffset
		Me.theDataSize = originalObject.DataSize
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return New SourcePackageDirectoryEntry(Me)
	End Function

#Region "Properties"

	Public Property PackageDataPathFileName() As String
		Get
			Return Me.thePackageDataPathFileName
		End Get
		Set(ByVal value As String)
			Me.thePackageDataPathFileName = value
			NotifyPropertyChanged("PackagePathFileName")
		End Set
	End Property

	Public Property PackageDataPathFileNameExists() As Boolean
		Get
			Return Me.thePackageDataPathFileNameExists
		End Get
		Set(ByVal value As Boolean)
			Me.thePackageDataPathFileNameExists = value
			NotifyPropertyChanged("PackagePathFileNameExists")
		End Set
	End Property

	Public Property IsFolder() As Boolean
		Get
			Return Me.theEntryIsFolder
		End Get
		Set(ByVal value As Boolean)
			Me.theEntryIsFolder = value
			NotifyPropertyChanged("IsFolder")
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

	Public Property DisplayPathFileName() As String
		Get
			Return Me.theDisplayPathFileName
		End Get
		Set(ByVal value As String)
			Me.theDisplayPathFileName = value
			NotifyPropertyChanged("DisplayPathFileName")
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

	Public Property DataOffset() As Int64
		Get
			Return Me.theDataOffset
		End Get
		Set(ByVal value As Int64)
			Me.theDataOffset = value
			NotifyPropertyChanged("DataOffset")
		End Set
	End Property

	Public Property DataSize() As UInt64
		Get
			Return Me.theDataSize
		End Get
		Set(ByVal value As UInt64)
			Me.theDataSize = value
			NotifyPropertyChanged("DataSize")
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

	' Properties related to the package.

	Private thePackageDataPathFileName As String
	Private thePackageDataPathFileNameExists As Boolean


	' Properties related to asset.

	Private theEntryIsFolder As Boolean

	Private theName As String
	Private theSize As UInt64
	Private theCount As UInt64
	Private theType As String
	Private theExtension As String


	' Properties related to asset location.

	' The text that should be shown in user interface, for example, in Garry's Mod, the metadata as a file (without quotes): "<addon.json>"
	'    This field should start with a "<" to signify that PathFileName has the real text. 
	Private theDisplayPathFileName As String
	' The text that should be used for an actual file name when DisplayPathFileName is not real text, for example (without quotes): "addon.json"
	Private thePathFileName As String
	Private theDataOffset As Int64
	Private theDataSize As UInt64

#End Region

	Class Comparer
		Implements IComparer(Of SourcePackageDirectoryEntry)

		Public Sub New(ByVal iProperty As String, ByVal iSortOrder As SortOrder)
			MyBase.New()
			Me.thePropertyName = iProperty
			Me.theSortOrder = iSortOrder
		End Sub

		Public Function Compare(ByVal xInfo As SourcePackageDirectoryEntry, ByVal yInfo As SourcePackageDirectoryEntry) As Integer Implements IComparer(Of SourcePackageDirectoryEntry).Compare
			Dim returnVal As Integer = -1

			If xInfo Is Nothing OrElse yInfo Is Nothing Then
				Return returnVal
			End If

			If xInfo.Extension = "<Folder>" AndAlso yInfo.Extension <> "<Folder>" Then
				returnVal = -1
			ElseIf xInfo.Extension <> "<Folder>" AndAlso yInfo.Extension = "<Folder>" Then
				returnVal = 1
			Else
				If Me.thePropertyName = "Name" Then
					returnVal = String.Compare(xInfo.Name, yInfo.Name)
				ElseIf Me.thePropertyName = "Size" Then
					If xInfo.Size < yInfo.Size Then
						returnVal = -1
					Else
						returnVal = 1
					End If
				ElseIf Me.thePropertyName = "Count" Then
					If xInfo.Count < yInfo.Count Then
						returnVal = -1
					Else
						returnVal = 1
					End If
				ElseIf Me.thePropertyName = "Type" Then
					returnVal = String.Compare(xInfo.Type, yInfo.Type)
				ElseIf Me.thePropertyName = "Extension" Then
					returnVal = String.Compare(xInfo.Extension, yInfo.Extension)
				ElseIf Me.thePropertyName = "PackagePathFileName" Then
					returnVal = String.Compare(xInfo.PackageDataPathFileName, yInfo.PackageDataPathFileName)
				End If
				If Me.theSortOrder = SortOrder.Descending Then
					returnVal *= -1
				End If
			End If

			Return returnVal
		End Function

		Private thePropertyName As String
		Private theSortOrder As SortOrder

	End Class

End Class
