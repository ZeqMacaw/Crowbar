Imports System.IO
Imports System.Runtime.CompilerServices
Imports Steamworks

Public Class SteamAppInfoBase

#Region "Change this section when adding or deleting a game"

	'Private BladeSymphonyAppID As New AppId_t(225600)
	'Private EstrangedActIAppID As New AppId_t(261820)
	'Private SourceFilmmakerAppID As New AppId_t(1840)
	'Private SourceSDKAppID As New AppId_t(211)
	'Private TeamFortress2AppID As New AppId_t(440)
	'Private ZombiePanicSourceAppID As New AppId_t(17500)
	'If Me.Name = "Anarchy Arcade" Then
	'	Me.Create(AnarchyArcadeAppID, True, contentFileExtensionList, GetType(AnarchyArcadeTagsUserControl))
	'ElseIf Me.Name = "Black Mesa" Then
	'	Me.Create(BlackMesaAppID, True, contentFileExtensionList, GetType(BlackMesaTagsUserControl))
	'ElseIf Me.Name = "Blade Symphony" Then
	'	Me.Create(BladeSymphonyAppID, False, contentFileExtensionList, GetType(Base_TagsUserControl))
	'ElseIf Me.Name = "Contagion" Then
	'	contentFileExtensionList.Add("vpk", "Source Engine VPK Files")
	'	Me.Create(ContagionAppID, False, contentFileExtensionList, GetType(ContagionTagsUserControl))
	'ElseIf Me.Name = "Estranged: Act I" Then
	'	Me.Create(EstrangedActIAppID, True, contentFileExtensionList, GetType(EstrangedActITagsUserControl))
	'ElseIf Me.Name = "Garry's Mod" Then
	'	Me.CanUseContentFolderOrFile = True
	'	contentFileExtensionList.Add("gma", "Garry's Mod GMA Files")
	'	Me.Create(GarrysModAppID, False, contentFileExtensionList, GetType(GarrysModTagsUserControl))
	'ElseIf Me.Name = "Left 4 Dead 2" Then
	'	contentFileExtensionList.Add("vpk", "Source Engine VPK Files")
	'	Me.Create(Left4Dead2AppID, False, contentFileExtensionList, GetType(Left4Dead2TagsUserControl))
	'ElseIf Me.Name = "Source Filmmaker" Then
	'	Me.Create(SourceFilmmakerAppID, False, contentFileExtensionList, GetType(SourceFilmmakerTagsUserControl))
	'ElseIf Me.Name = "Source SDK" Then
	'	Me.Create(SourceSDKAppID, False, contentFileExtensionList, GetType(Base_TagsUserControl))
	'ElseIf Me.Name = "Team Fortress 2" Then
	'	Me.Create(TeamFortress2AppID, False, contentFileExtensionList, GetType(Base_TagsUserControl))
	'ElseIf Me.Name = "Zombie Panic! Source" Then
	'	Me.Create(ZombiePanicSourceAppID, True, contentFileExtensionList, GetType(Base_TagsUserControl))	
	Public Shared Function GetSteamAppInfos() As List(Of SteamAppInfoBase)
		Dim steamAppInfos As New List(Of SteamAppInfoBase)()

		Dim anAppInfo As SteamAppInfoBase

		'anAppInfo = New AnarchyArcadeAppInfo(GetType(AnarchyArcadeTagsUserControl))
		'steamAppInfos.Add(anAppInfo)
		anAppInfo = New BlackMesaSteamAppInfo()
		steamAppInfos.Add(anAppInfo)
		'anAppInfo = New SteamAppInfo("Blade Symphony")
		'steamAppInfos.Add(anAppInfo)
		anAppInfo = New ContagionSteamAppInfo()
		steamAppInfos.Add(anAppInfo)
		'anAppInfo = New SteamAppInfo("Estranged: Act I")
		'steamAppInfos.Add(anAppInfo)
		anAppInfo = New GarrysModSteamAppInfo()
		steamAppInfos.Add(anAppInfo)
		anAppInfo = New Left4Dead2SteamAppInfo()
		steamAppInfos.Add(anAppInfo)
		'anAppInfo = New SteamAppInfo("Source Filmmaker")
		'steamAppInfos.Add(anAppInfo)
		'anAppInfo = New SteamAppInfo("Team Fortress 2")
		'steamAppInfos.Add(anAppInfo)
		anAppInfo = New ZombiePanicSourceSteamAppInfo()
		steamAppInfos.Add(anAppInfo)

		Return steamAppInfos
	End Function

#End Region

	Public Sub New()
		Me.ContentFileExtensionsAndDescriptions = New SortedList(Of String, String)()
	End Sub

	Public Overridable Function ProcessFileAfterDownload(ByVal givenPathFileName As String, ByVal bw As BackgroundWorkerEx) As String
		Dim processedPathFileName As String = givenPathFileName
		Return processedPathFileName
	End Function

	Public Overridable Function ProcessFileBeforeUpload(ByVal item As WorkshopItem, ByVal bw As BackgroundWorkerEx) As String
		Dim processedPathFileName As String = item.ContentPathFolderOrFileName
		Return processedPathFileName
	End Function

	Public Overridable Sub CleanUpAfterUpload(ByVal bw As BackgroundWorkerEx)
	End Sub

#Region "Delegates"

#End Region

#Region "Data"

	Public Property ID As AppId_t
	Public Property Name As String
	Public Property UsesSteamUGC As Boolean
	Public Property CanUseContentFolderOrFile As Boolean
	Public Property ContentFileExtensionsAndDescriptions As SortedList(Of String, String)
	Public Property TagsControlType As Type

#End Region

End Class
