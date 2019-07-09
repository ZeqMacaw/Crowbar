Imports System.ComponentModel
Imports System.IO
Imports System.Xml.Serialization

Public Class GameSetup
	Implements ICloneable
	Implements INotifyPropertyChanged

#Region "Create and Destroy"

	Public Sub New()
		'MyBase.New()

		Me.theGameName = "Left 4 Dead 2"
		Me.theGameEngine = AppEnums.GameEngine.Source
		Me.theGamePathFileName = "C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\left4dead2\gameinfo.txt"
		Me.theGameAppPathFileName = "C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\left4dead2.exe"
		Me.theGameAppOptions = ""
		Me.theCompilerPathFileName = "C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\studiomdl.exe"
		Me.theViewerPathFileName = "C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\hlmv.exe"
		Me.theMappingToolPathFileName = "C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\hammer.exe"
		Me.thePackerPathFileName = "C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\vpk.exe"
	End Sub

	Protected Sub New(ByVal originalObject As GameSetup)
		Me.theGameName = originalObject.GameName
		Me.theGameEngine = originalObject.GameEngine
		'Me.theGamePathFileName = originalObject.GamePathFileName
		'Me.theGameAppPathFileName = originalObject.GameAppPathFileName
		Me.theGamePathFileName = originalObject.GamePathFileNameUnprocessed
		Me.theGameAppPathFileName = originalObject.GameAppPathFileNameUnprocessed
		Me.theGameAppOptions = originalObject.GameAppOptions
		'Me.theCompilerPathFileName = originalObject.CompilerPathFileName
		'Me.theViewerPathFileName = originalObject.ViewerPathFileName
		'Me.theMappingToolPathFileName = originalObject.MappingToolPathFileName
		'Me.theUnpackerPathFileName = originalObject.UnpackerPathFileName
		Me.theCompilerPathFileName = originalObject.CompilerPathFileNameUnprocessed
		Me.theViewerPathFileName = originalObject.ViewerPathFileNameUnprocessed
		Me.theMappingToolPathFileName = originalObject.MappingToolPathFileNameUnprocessed
		Me.thePackerPathFileName = originalObject.PackerPathFileNameUnprocessed
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return New GameSetup(Me)
	End Function

#End Region

#Region "Properties"

	Public Property GameName() As String
		Get
			Return Me.theGameName
		End Get
		Set(ByVal value As String)
			If Me.theGameName <> value Then
				Me.theGameName = value
				NotifyPropertyChanged("GameName")
			End If
		End Set
	End Property

	Public Property GameEngine() As GameEngine
		Get
			Return Me.theGameEngine
		End Get
		Set(ByVal value As GameEngine)
			If Me.theGameEngine <> value Then
				Me.theGameEngine = value
				NotifyPropertyChanged("GameEngine")
			End If
		End Set
	End Property

	<XmlIgnore()> _
	Public ReadOnly Property GamePathFileName() As String
		Get
			Return TheApp.GetProcessedPathFileName(Me.theGamePathFileName)
		End Get
		'Set(ByVal value As String)
		'	Me.theGamePathFileName = value
		'	NotifyPropertyChanged("GamePathFileName")
		'End Set
	End Property

	<XmlElement("GamePathFileName")> _
	Public Property GamePathFileNameUnprocessed() As String
		Get
			Return Me.theGamePathFileName
		End Get
		Set(ByVal value As String)
			Me.theGamePathFileName = value
			NotifyPropertyChanged("GamePathFileName")
			NotifyPropertyChanged("GamePathFileNameUnprocessed")
		End Set
	End Property

	<XmlIgnore()> _
	Public ReadOnly Property GameAppPathFileName() As String
		Get
			Return TheApp.GetProcessedPathFileName(Me.theGameAppPathFileName)
		End Get
		'Set(ByVal value As String)
		'	Me.theGameAppPathFileName = value
		'	NotifyPropertyChanged("GameAppPathFileName")
		'End Set
	End Property

	<XmlElement("GameAppPathFileName")> _
	Public Property GameAppPathFileNameUnprocessed() As String
		Get
			Return Me.theGameAppPathFileName
		End Get
		Set(ByVal value As String)
			Me.theGameAppPathFileName = value
			NotifyPropertyChanged("GameAppPathFileName")
			NotifyPropertyChanged("GameAppPathFileNameUnprocessed")
		End Set
	End Property

	Public Property GameAppOptions() As String
		Get
			Return Me.theGameAppOptions
		End Get
		Set(ByVal value As String)
			Me.theGameAppOptions = value
			NotifyPropertyChanged("GameAppOptions")
		End Set
	End Property

	<XmlIgnore()> _
	Public ReadOnly Property CompilerPathFileName() As String
		Get
			Return TheApp.GetProcessedPathFileName(Me.theCompilerPathFileName)
		End Get
		'Set(ByVal value As String)
		'	Me.theCompilerPathFileName = value
		'	NotifyPropertyChanged("CompilerPathFileName")
		'End Set
	End Property

	<XmlElement("CompilerPathFileName")> _
	Public Property CompilerPathFileNameUnprocessed() As String
		Get
			Return Me.theCompilerPathFileName
		End Get
		Set(ByVal value As String)
			Me.theCompilerPathFileName = value
			NotifyPropertyChanged("CompilerPathFileName")
			NotifyPropertyChanged("CompilerPathFileNameUnprocessed")
		End Set
	End Property

	<XmlIgnore()> _
	Public ReadOnly Property ViewerPathFileName() As String
		Get
			Return TheApp.GetProcessedPathFileName(Me.theViewerPathFileName)
		End Get
		'Set(ByVal value As String)
		'	Me.theViewerPathFileName = value
		'	NotifyPropertyChanged("ViewerPathFileName")
		'End Set
	End Property

	<XmlElement("ViewerPathFileName")> _
	Public Property ViewerPathFileNameUnprocessed() As String
		Get
			Return Me.theViewerPathFileName
		End Get
		Set(ByVal value As String)
			Me.theViewerPathFileName = value
			NotifyPropertyChanged("ViewerPathFileName")
			NotifyPropertyChanged("ViewerPathFileNameUnprocessed")
		End Set
	End Property

	<XmlIgnore()> _
	Public ReadOnly Property MappingToolPathFileName() As String
		Get
			Return TheApp.GetProcessedPathFileName(Me.theMappingToolPathFileName)
		End Get
		'Set(ByVal value As String)
		'	Me.theMappingToolPathFileName = value
		'	NotifyPropertyChanged("MappingToolPathFileName")
		'End Set
	End Property

	<XmlElement("MappingToolPathFileName")> _
	Public Property MappingToolPathFileNameUnprocessed() As String
		Get
			Return Me.theMappingToolPathFileName
		End Get
		Set(ByVal value As String)
			Me.theMappingToolPathFileName = value
			NotifyPropertyChanged("MappingToolPathFileName")
			NotifyPropertyChanged("MappingToolPathFileNameUnprocessed")
		End Set
	End Property

	<XmlIgnore()> _
	Public ReadOnly Property PackerPathFileName() As String
		Get
			Return TheApp.GetProcessedPathFileName(Me.thePackerPathFileName)
		End Get
		'Set(ByVal value As String)
		'	Me.theUnpackerPathFileName = value
		'	NotifyPropertyChanged("PackerPathFileName")
		'End Set
	End Property

	<XmlElement("PackerPathFileName")> _
	Public Property PackerPathFileNameUnprocessed() As String
		Get
			Return Me.thePackerPathFileName
		End Get
		Set(ByVal value As String)
			Me.thePackerPathFileName = value
			NotifyPropertyChanged("PackerPathFileName")
			NotifyPropertyChanged("PackerPathFileNameUnprocessed")
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

	Private theGameName As String
	Private theGameEngine As GameEngine
	Private theGamePathFileName As String
	Private theGameAppPathFileName As String
	Private theGameAppId As String
	Private theGameAppOptions As String
	Private theCompilerPathFileName As String
	Private theViewerPathFileName As String
	Private theMappingToolPathFileName As String
	Private thePackerPathFileName As String

#End Region

End Class
