﻿Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class CodenameCureSteamAppInfo
    Inherits SteamAppInfoBase

    Public Sub New()
        MyBase.New()

        Me.ID = New AppId_t(355180)
        Me.Name = "Codename Cure"
        Me.UsesSteamUGC = True
        Me.CanUseContentFolderOrFile = False
        'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
        Me.TagsControlType = GetType(CodenameCureTagsUserControl)
    End Sub

End Class