Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class MilitaryConflictVietnamSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(1012110)
		Me.Name = "Military Conflict: Vietnam"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(MilitaryConflictVietnamTagsUserControl)
	End Sub

End Class
