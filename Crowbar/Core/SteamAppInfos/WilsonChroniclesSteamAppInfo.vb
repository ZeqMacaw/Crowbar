Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class WilsonChroniclesSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(313240)
		Me.Name = "Wilson Chronicles"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(WilsonChroniclesTagsUserControl)
	End Sub

End Class
