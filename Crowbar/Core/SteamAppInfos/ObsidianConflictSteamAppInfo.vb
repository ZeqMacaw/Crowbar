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
	End Sub

End Class
