Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class ContagionSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(238430)
		Me.Name = "Contagion"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = True
		Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(ContagionTagsUserControl)
	End Sub

End Class
