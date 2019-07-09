Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class AnarchyArcadeSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(266430)
		Me.Name = "Anarchy Arcade"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(AnarchyArcadeTagsUserControl)
	End Sub

End Class
