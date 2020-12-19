Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class ObsidianConflictSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(17750)
		Me.Name = "Obsidian Conflict"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(ObsidianConflictTagsUserControl)
		Me.KeyValuesControlType = GetType(ObsidianConflictKeyValuesUserControl)
	End Sub

	Public Enum MountOptions
		<Description("hl2")> hl2
		<Description("lostcoast")> lostcoast
		<Description("episodic")> episodic
		<Description("ep2")> ep2
		<Description("hl2mp")> hl2mp
		<Description("cstrike")> cstrike
		<Description("dod")> dod
		<Description("tf")> tf
	End Enum

End Class
