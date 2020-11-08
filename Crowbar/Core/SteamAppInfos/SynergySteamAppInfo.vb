Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class SynergySteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(17520)
		Me.Name = "Synergy"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = True
		Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(SynergyTagsUserControl)
	End Sub

	Public Overrides Function ProcessFileNameWithItemIdBeforeUpload(ByVal item As WorkshopItem, ByVal itemID As String, ByVal bw As BackgroundWorkerEx) As String
		Dim processedPathFileName As String = FileManager.GetPath(item.ContentPathFolderOrFileName)
		Dim fileExtension As String = Path.GetExtension(item.ContentPathFolderOrFileName)

		processedPathFileName = Path.Combine(processedPathFileName, itemID + "_pak" + fileExtension)
		Me.theProcessedPathFileName = ""

		If File.Exists(processedPathFileName) Then
			'TODO: Copy to temp folder with new name. Delete in cleanup function.
			'Me.theTempProcessedPathFileName = tempPathFileName
		Else
			Try
				If File.Exists(item.ContentPathFolderOrFileName) Then
					File.Move(item.ContentPathFolderOrFileName, processedPathFileName)
					bw.ReportProgress(0, "Renamed """ + Path.GetFileName(item.ContentPathFolderOrFileName) + """ to """ + Path.GetFileName(processedPathFileName) + """." + vbCrLf)
					Me.theProcessedPathFileName = processedPathFileName
					Me.theGivenPathFileName = item.ContentPathFolderOrFileName
				End If
			Catch ex As Exception
				bw.ReportProgress(0, "Crowbar tried to rename the file """ + Path.GetFileName(item.ContentPathFolderOrFileName) + """ to """ + Path.GetFileName(processedPathFileName) + """ but Windows gave this message: " + ex.Message)
			End Try
		End If

		Return processedPathFileName
	End Function

	Public Overrides Sub CleanUpAfterUpload(ByVal bw As BackgroundWorkerEx)
		If Me.theProcessedPathFileName <> "" AndAlso File.Exists(Me.theProcessedPathFileName) Then
			Try
				File.Move(Me.theProcessedPathFileName, Me.theGivenPathFileName)
				bw.ReportProgress(0, "Renamed """ + Path.GetFileName(Me.theProcessedPathFileName) + """ to """ + Path.GetFileName(Me.theGivenPathFileName) + """." + vbCrLf)
			Catch ex As Exception
				bw.ReportProgress(0, "Crowbar tried to rename the file """ + Path.GetFileName(Me.theProcessedPathFileName) + """ to """ + Path.GetFileName(Me.theGivenPathFileName) + """ but Windows gave this message: " + ex.Message)
			End Try
		End If
	End Sub

	Private theGivenPathFileName As String
	Private theProcessedPathFileName As String
	'Private theTempProcessedPathFileName As String

End Class
