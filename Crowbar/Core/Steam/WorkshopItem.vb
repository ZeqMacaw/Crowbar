Imports System.ComponentModel
Imports System.Xml.Serialization

Public Class WorkshopItem
	Implements ICloneable
	Implements IDisposable
	Implements INotifyPropertyChanged

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()
		Me.Create()
	End Sub

	Private Sub Create()
		Me.theItemIsDisposed = False

		Me.theCreatorAppID = "0"

		Me.theID = WorkshopItem.DraftItemIDText
		Me.theOwnerSteamID = 0
		Me.theOwnerName = ""
		Me.thePosted = MathModule.DateTimeToUnixTimeStamp(DateTime.Now())
		Me.theUpdated = Me.thePosted

		Me.theTitle = ""
		Me.theTitleIsChanged = False
		Me.theDescription = ""
		Me.theDescriptionIsChanged = False
		Me.theChangeNote = ""
		Me.theChangeNoteIsChanged = False

		Me.theContentSize = 0
		Me.theContentPathFolderOrFileName = ""
		Me.theContentPathFolderOrFileNameIsChanged = False

		Me.thePreviewImageSize = 0
		Me.thePreviewImagePathFileName = ""
		Me.thePreviewImagePathFileNameIsChanged = False

		Me.theVisibility = SteamUGCPublishedItemVisibility.Hidden
		Me.theVisibilityIsChanged = False

		Me.theTags = New BindingListEx(Of String)()
		Me.theTagsAsTextLine = ""
		Me.theTagsIsChanged = False

		Me.theItemIsChanged = False
	End Sub

	Protected Sub New(ByVal originalObject As WorkshopItem)
		Me.theCreatorAppID = originalObject.theCreatorAppID

		'NOTE: Clone becomes a draft item.
		Me.theID = WorkshopItem.DraftItemIDText
		Me.theOwnerSteamID = originalObject.OwnerID
		Me.theOwnerName = originalObject.OwnerName
		Me.thePosted = originalObject.Posted
		Me.theUpdated = originalObject.Updated

		Me.theTitle = originalObject.Title
		Me.theTitleIsChanged = False
		Me.theDescription = originalObject.Description
		Me.theDescriptionIsChanged = False
		Me.theChangeNote = originalObject.ChangeNote
		Me.theChangeNoteIsChanged = False

		Me.theContentSize = originalObject.ContentSize
		Me.theContentPathFolderOrFileName = originalObject.ContentPathFolderOrFileName
		Me.theContentPathFolderOrFileNameIsChanged = False

		Me.thePreviewImageSize = originalObject.PreviewImageSize
		Me.thePreviewImagePathFileName = originalObject.PreviewImagePathFileName
		Me.thePreviewImagePathFileNameIsChanged = False

		Me.theVisibility = originalObject.Visibility
		Me.theVisibilityIsChanged = False

		Me.theTags = New BindingListEx(Of String)()
		For Each originalTag As String In originalObject.Tags
			Me.theTags.Add(originalTag)
		Next
		Me.theTagsAsTextLine = originalObject.TagsAsTextLine
		Me.theTagsIsChanged = False

		'NOTE: Clone becomes a draft item; thus theItemIsChanged is always False.
		Me.theItemIsChanged = False
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return New WorkshopItem(Me)
	End Function

#Region "IDisposable Support"

	Public Sub Dispose() Implements IDisposable.Dispose
		' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If Not Me.theItemIsDisposed Then
			If disposing Then
				Me.Free()
			End If
			'NOTE: free shared unmanaged resources
		End If
		Me.theItemIsDisposed = True
	End Sub

	'Protected Overrides Sub Finalize()
	'	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
	'	Dispose(False)
	'	MyBase.Finalize()
	'End Sub

#End Region

#End Region

#Region "Init and Free"

	'Private Sub Init()
	'End Sub

	Private Sub Free()
		'RemoveHandler Me.theTags.ListChanged, AddressOf Me.Tags_ListChanged
	End Sub

#End Region

#Region "Properties"

	Public Property CreatorAppID As String
		Get
			Return Me.theCreatorAppID
		End Get
		Set(value As String)
			If Me.theCreatorAppID <> value Then
				Me.theCreatorAppID = value
				NotifyPropertyChanged("CreatorAppID")
			End If
		End Set
	End Property

	Public Property ID As String
		Get
			Return Me.theID
		End Get
		Set(value As String)
			If Me.theID <> value Then
				Me.theID = value
				NotifyPropertyChanged("ID")
			End If
		End Set
	End Property

	Public Property OwnerID As ULong
		Get
			Return Me.theOwnerSteamID
		End Get
		Set(value As ULong)
			If Me.theOwnerSteamID <> value Then
				Me.theOwnerSteamID = value
				NotifyPropertyChanged("OwnerID")
			End If
		End Set
	End Property

	Public Property OwnerName As String
		Get
			Return Me.theOwnerName
		End Get
		Set(value As String)
			If Me.theOwnerName <> value Then
				Me.theOwnerName = value
				NotifyPropertyChanged("OwnerName")
			End If
		End Set
	End Property

	' UnixTimeStamp
	Public Property Posted As Long
		Get
			Return Me.thePosted
		End Get
		Set(value As Long)
			If Me.thePosted <> value Then
				Me.thePosted = value
				NotifyPropertyChanged("Posted")
			End If
		End Set
	End Property

	' UnixTimeStamp
	Public Property Updated As Long
		Get
			Return Me.theUpdated
		End Get
		Set(value As Long)
			If Me.theUpdated <> value Then
				Me.theUpdated = value
				NotifyPropertyChanged("Updated")
			End If
		End Set
	End Property

	Public Property Title As String
		Get
			Return Me.theTitle
		End Get
		Set(value As String)
			If Me.theTitle <> value Then
				Me.theTitle = value
				Me.theTitleIsChanged = True
				NotifyPropertyChanged("Title")
			End If
		End Set
	End Property

	Public Property TitleIsChanged As Boolean
		Get
			Return Me.theTitleIsChanged
		End Get
		Set(value As Boolean)
			If Me.theTitleIsChanged <> value Then
				Me.theTitleIsChanged = value
			End If
		End Set
	End Property

	Public Property Description As String
		Get
			Return Me.theDescription
		End Get
		Set(value As String)
			If Me.theDescription <> value Then
				Me.theDescription = value
				Me.theDescriptionIsChanged = True
				NotifyPropertyChanged("Description")
			End If
		End Set
	End Property

	Public Property DescriptionIsChanged As Boolean
		Get
			Return Me.theDescriptionIsChanged
		End Get
		Set(value As Boolean)
			If Me.theDescriptionIsChanged <> value Then
				Me.theDescriptionIsChanged = value
			End If
		End Set
	End Property

	Public Property ChangeNote As String
		Get
			Return Me.theChangeNote
		End Get
		Set(value As String)
			If Me.theChangeNote <> value Then
				Me.theChangeNote = value
				Me.theChangeNoteIsChanged = True
				NotifyPropertyChanged("ChangeNote")
			End If
		End Set
	End Property

	Public Property ChangeNoteIsChanged As Boolean
		Get
			Return Me.theChangeNoteIsChanged
		End Get
		Set(value As Boolean)
			If Me.theChangeNoteIsChanged <> value Then
				Me.theChangeNoteIsChanged = value
			End If
		End Set
	End Property

	Public Property ContentSize As Integer
		Get
			Return Me.theContentSize
		End Get
		Set(value As Integer)
			If Me.theContentSize <> value Then
				Me.theContentSize = value
				NotifyPropertyChanged("ContentSize")
			End If
		End Set
	End Property

	Public Property ContentPathFolderOrFileName As String
		Get
			Return Me.theContentPathFolderOrFileName
		End Get
		Set(value As String)
			If Me.theContentPathFolderOrFileName <> value Then
				Me.theContentPathFolderOrFileName = value
				Me.theContentPathFolderOrFileNameIsChanged = True
				NotifyPropertyChanged("ContentPathFolderOrFileName")
			End If
		End Set
	End Property

	Public Property ContentPathFolderOrFileNameIsChanged As Boolean
		Get
			Return Me.theContentPathFolderOrFileNameIsChanged
		End Get
		Set(value As Boolean)
			If Me.theContentPathFolderOrFileNameIsChanged <> value Then
				Me.theContentPathFolderOrFileNameIsChanged = value
			End If
		End Set
	End Property

	Public Property PreviewImageSize As Integer
		Get
			Return Me.thePreviewImageSize
		End Get
		Set(value As Integer)
			If Me.thePreviewImageSize <> value Then
				Me.thePreviewImageSize = value
				NotifyPropertyChanged("PreviewImageSize")
			End If
		End Set
	End Property

	Public Property PreviewImagePathFileName As String
		Get
			Return Me.thePreviewImagePathFileName
		End Get
		Set(value As String)
			If Me.thePreviewImagePathFileName <> value Then
				Me.thePreviewImagePathFileName = value
				Me.thePreviewImagePathFileNameIsChanged = True
				NotifyPropertyChanged("PreviewImagePathFileName")
			End If
		End Set
	End Property

	Public Property PreviewImagePathFileNameIsChanged As Boolean
		Get
			Return Me.thePreviewImagePathFileNameIsChanged
		End Get
		Set(value As Boolean)
			If Me.thePreviewImagePathFileNameIsChanged <> value Then
				Me.thePreviewImagePathFileNameIsChanged = value
			End If
		End Set
	End Property

	Public Property Visibility As SteamUGCPublishedItemVisibility
		Get
			Return Me.theVisibility
		End Get
		Set(value As SteamUGCPublishedItemVisibility)
			If Me.theVisibility <> value Then
				Me.theVisibility = value
				Me.theVisibilityIsChanged = True
				NotifyPropertyChanged("Visibility")
			End If
		End Set
	End Property

	<XmlIgnore()>
	Public Property VisibilityText As String
		Get
			Return Me.theVisibility.ToString()
		End Get
		Set(value As String)
			If Me.theVisibility.ToString() <> value Then
				Me.theVisibility = CType([Enum].Parse(GetType(SteamUGCPublishedItemVisibility), value), SteamUGCPublishedItemVisibility)
				Me.theVisibilityIsChanged = True
				NotifyPropertyChanged("Visibility")
			End If
		End Set
	End Property

	Public Property VisibilityIsChanged As Boolean
		Get
			Return Me.theVisibilityIsChanged
		End Get
		Set(value As Boolean)
			If Me.theVisibilityIsChanged <> value Then
				Me.theVisibilityIsChanged = value
			End If
		End Set
	End Property

	Public Property Tags As BindingListEx(Of String)
		Get
			Return Me.theTags
		End Get
		Set(value As BindingListEx(Of String))
			Dim givenTagsAsTextLine As String = ""
			For Each word As String In value
				givenTagsAsTextLine += word + ","
			Next
			givenTagsAsTextLine.TrimEnd(","c)

			If Me.theTagsAsTextLine <> givenTagsAsTextLine Then
				Me.theTags = value
				Me.theTagsAsTextLine = givenTagsAsTextLine
				Me.theTagsIsChanged = True
				'NOTE: This line raises exception, possibly because the DataGridView gets confused by the property being a list, so use "TagsAsTextLine" property.
				'System.ArgumentOutOfRangeException
				'  HResult=0x80131502
				'  Message=Specified argument was out of the range of valid values.
				'Parameter name: rowIndex
				'  Source=System.Windows.Forms
				'  StackTrace:
				'   at System.Windows.Forms.DataGridView.InvalidateCell(Int32 columnIndex, Int32 rowIndex)
				'   at System.Windows.Forms.DataGridView.DataGridViewDataConnection.ProcessListChanged(ListChangedEventArgs e)
				'   at System.Windows.Forms.DataGridView.DataGridViewDataConnection.currencyManager_ListChanged(Object sender, ListChangedEventArgs e)
				'   at System.Windows.Forms.CurrencyManager.OnListChanged(ListChangedEventArgs e)
				'   at System.Windows.Forms.CurrencyManager.List_ListChanged(Object sender, ListChangedEventArgs e)
				'   at System.Windows.Forms.BindingSource.OnListChanged(ListChangedEventArgs e)
				'   at System.Windows.Forms.BindingSource.InnerList_ListChanged(Object sender, ListChangedEventArgs e)
				'   at System.ComponentModel.BindingList`1.OnListChanged(ListChangedEventArgs e)
				'   at System.ComponentModel.BindingList`1.Child_PropertyChanged(Object sender, PropertyChangedEventArgs e)
				'   at System.ComponentModel.PropertyChangedEventHandler.Invoke(Object sender, PropertyChangedEventArgs e)
				'   at Crowbar.WorkshopItem.NotifyPropertyChanged(String info) in E:\Users\ZeqMacaw\Documents\Visual Studio 2017\Projects\CrowbarSteamworks\CrowbarSteamworks\Core\WorkshopItem.vb:line 500
				'   at Crowbar.WorkshopItem.set_Tags(BindingListEx`1 value) in E:\Users\ZeqMacaw\Documents\Visual Studio 2017\Projects\CrowbarSteamworks\CrowbarSteamworks\Core\WorkshopItem.vb:line 423
				'   at Crowbar.PublishUserControl.UpdateItemDetails() in E:\Users\ZeqMacaw\Documents\Visual Studio 2017\Projects\CrowbarSteamworks\CrowbarSteamworks\Widgets\Main Tabs\PublishUserControl.vb:line 897
				'NotifyPropertyChanged("Tags")
				'RaiseEvent TagsPropertyChanged(Me, New PropertyChangedEventArgs("Tags"))
				'RemoveHandler Me.theTags.ListChanged, AddressOf Me.Tags_ListChanged
				'AddHandler Me.theTags.ListChanged, AddressOf Me.Tags_ListChanged
				NotifyPropertyChanged("TagsAsTextLine")
			End If
		End Set
	End Property

	<XmlIgnore()>
	Public Property TagsAsTextLine As String
		Get
			Return Me.theTagsAsTextLine
		End Get
		Set(value As String)
			If Me.theTagsAsTextLine <> value Then
				Me.theTags = New BindingListEx(Of String)()
				Dim charSeparators() As Char = {","c}
				Dim words() As String = value.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries)
				For Each word As String In words
					Me.theTags.Add(word)
				Next
				Me.theTagsAsTextLine = value
				Me.theTagsIsChanged = True
				'NOTE: This line raises exception, possibly because the DataGridView gets confused by the property being a list, so use "TagsAsTextLine" property.
				'NotifyPropertyChanged("Tags")
				'RaiseEvent TagsPropertyChanged(Me, New PropertyChangedEventArgs("Tags"))
				'RemoveHandler Me.theTags.ListChanged, AddressOf Me.Tags_ListChanged
				'AddHandler Me.theTags.ListChanged, AddressOf Me.Tags_ListChanged
				NotifyPropertyChanged("TagsAsTextLine")
			End If
		End Set
	End Property

	Public Property TagsIsChanged As Boolean
		Get
			Return Me.theTagsIsChanged
		End Get
		Set(value As Boolean)
			If Me.theTagsIsChanged <> value Then
				Me.theTagsIsChanged = value
			End If
		End Set
	End Property

	<XmlIgnore()>
	Public Property IsChanged As Boolean
		Get
			Return Me.theItemIsChanged
		End Get
		Set(value As Boolean)
			'NOTE: Set these whenever set to False and not just when it changes.
			If Not value Then
				Me.theTitleIsChanged = False
				Me.theDescriptionIsChanged = False
				Me.theChangeNoteIsChanged = False
				Me.theContentPathFolderOrFileNameIsChanged = False
				Me.thePreviewImagePathFileNameIsChanged = False
				Me.theVisibilityIsChanged = False
				Me.theTagsIsChanged = False
			End If

			If Me.theItemIsChanged <> value Then
				Me.theItemIsChanged = value
				NotifyPropertyChanged("IsChanged")
			End If
		End Set
	End Property

	<XmlIgnore()>
	Public ReadOnly Property IsDraft As Boolean
		Get
			Return Me.theID = WorkshopItem.DraftItemIDText
		End Get
	End Property

	<XmlIgnore()>
	Public Property IsTemplate As Boolean
		Get
			Return Me.theID = WorkshopItem.TemplateItemIDText
		End Get
		Set(value As Boolean)
			Me.theID = WorkshopItem.TemplateItemIDText
		End Set
	End Property

	<XmlIgnore()>
	Public ReadOnly Property IsPublished As Boolean
		Get
			Return Me.theID <> WorkshopItem.DraftItemIDText AndAlso Me.theID <> WorkshopItem.TemplateItemIDText
		End Get
	End Property

#End Region

#Region "Methods"

	Public Sub SetAllChangedForNonEmptyFields()
		If Me.theTitle <> "" Then
			Me.theTitleIsChanged = True
		End If
		If Me.theDescription <> "" Then
			Me.theDescriptionIsChanged = True
		End If
		If Me.theChangeNote <> "" Then
			Me.theChangeNoteIsChanged = True
		End If
		If Me.theContentPathFolderOrFileName <> "" Then
			Me.theContentPathFolderOrFileNameIsChanged = True
		End If
		If Me.thePreviewImagePathFileName <> "" Then
			Me.thePreviewImagePathFileNameIsChanged = True
		End If

		'NOTE: Always set IsChanged for Visibility and Tags.
		Me.theVisibilityIsChanged = True
		Me.theTagsIsChanged = True

		'NOTE: Always set IsChanged for item.
		Me.theItemIsChanged = True
	End Sub

#End Region

#Region "Event Handlers"

	'Private Sub Tags_ListChanged(sender As Object, e As ListChangedEventArgs)
	'	'If e.ListChangedType = ListChangedType.ItemDeleted AndAlso e.OldIndex = -2 Then
	'	'	'NOTE: Ignore the "pre-delete" event.
	'	'	Exit Sub
	'	'End If
	'	'If e.ListChangedType = ListChangedType.ItemChanged Then
	'	'End If
	'	RaiseEvent TagsPropertyChanged(Me, New PropertyChangedEventArgs("Tags"))
	'End Sub

#End Region

#Region "Events"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
	'Public Event TagsPropertyChanged As PropertyChangedEventHandler

#End Region

#Region "Private Methods"

	Protected Sub NotifyPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

#End Region

#Region "Constants"

	Public Enum SteamUGCPublishedItemVisibility
		'<Description("<no change>")> NoChange = -1
		<Description("Public")> [Public] = 0
		<Description("Friends-Only")> FriendsOnly = 1
		<Description("Hidden")> Hidden = 2
		<Description("Unlisted")> Unlisted = 3
	End Enum

#End Region

#Region "Data"

	Private Const DraftItemIDText As String = "<draft>"
	Private Const TemplateItemIDText As String = "<template>"

	Private theItemIsDisposed As Boolean

	Private theCreatorAppID As String

	Private theID As String
	Private theOwnerSteamID As ULong
	Private theOwnerName As String
	Private thePosted As Long
	Private theUpdated As Long

	Private theTitle As String
	Private theTitleIsChanged As Boolean
	Private theDescription As String
	Private theDescriptionIsChanged As Boolean
	Private theChangeNote As String
	Private theChangeNoteIsChanged As Boolean

	Private theContentSize As Integer
	Private theContentPathFolderOrFileName As String
	Private theContentPathFolderOrFileNameIsChanged As Boolean

	Private thePreviewImageSize As Integer
	Private thePreviewImagePathFileName As String
	Private thePreviewImagePathFileNameIsChanged As Boolean

	Private theVisibility As SteamUGCPublishedItemVisibility
	Private theVisibilityIsChanged As Boolean

	Private theTags As BindingListEx(Of String)
	Private theTagsAsTextLine As String
	Private theTagsIsChanged As Boolean

	Private theItemIsChanged As Boolean

#End Region

End Class
