Imports System.IO

Public Class GameInfoTxtFile

#Region "Creation and Destruction"

	' This is only way to create the GameInfoTxtFile singleton object.
	Public Shared Function Create() As GameInfoTxtFile
		If GameInfoTxtFile.theGameInfoTxtFile Is Nothing Then
			GameInfoTxtFile.theGameInfoTxtFile = New GameInfoTxtFile()
		End If
		Return GameInfoTxtFile.theGameInfoTxtFile
	End Function

	' Only here to make compiler happy with anything that inherits from this class.
	Protected Sub New()
		Me.theGameInfoPathFileNames = New List(Of String)(2)
		Me.theBackupGameInfoPathFileNames = New SortedList(Of String, String)(2)
	End Sub

#End Region

#Region "Methods"

	Public Sub WriteNewGamePath(ByVal gameInfoPathFileName As String, ByVal newGamePath As String)
		Me.MakeBackupOfGameInfoFile(gameInfoPathFileName, newGamePath)

		Dim sr As StreamReader = Nothing
		Dim sw As StreamWriter = Nothing
		Dim fullText As String = ""
		Dim textUptoPosition As String = ""
		Dim textPastPosition As String = ""

		Try
			If File.Exists(gameInfoPathFileName) Then
				Dim buffer As String
				Dim token As String = ""

				sr = New StreamReader(gameInfoPathFileName)
				fullText = sr.ReadToEnd()
				buffer = fullText

				token = FileManager.ReadKeyValueToken(buffer)
				If token = "GameInfo" Then
					token = FileManager.ReadKeyValueToken(buffer)
					If token = "{" Then
						While token <> ""
							token = FileManager.ReadKeyValueToken(buffer)
							If token = "FileSystem" Then
								token = FileManager.ReadKeyValueToken(buffer)
								If token = "{" Then
									While token <> ""
										token = FileManager.ReadKeyValueToken(buffer)
										If token = "SearchPaths" Then
											token = FileManager.ReadKeyValueToken(buffer)
											If token = "{" Then
												Dim token2 As String = ""
												Dim searchPath0Position As Long
												searchPath0Position = sr.BaseStream.Length - buffer.Length
												textUptoPosition = fullText.Substring(0, CInt(searchPath0Position))
												textPastPosition = buffer
												token = FileManager.ReadKeyValueToken(buffer)
												token2 = FileManager.ReadKeyValueToken(buffer)
												If token = "Game" AndAlso token2 = newGamePath Then
													'NOTE: Do nothing because crowbar search path already exists.
												Else
													sr.Close()
													Me.WriteModifiedFile(gameInfoPathFileName, newGamePath, textUptoPosition, textPastPosition)
												End If
											End If
											Exit While
										End If
									End While
								End If
								Exit While
							End If
						End While
					End If
				End If
			End If
		Catch ex As Exception
			Throw
		Finally
			If sr IsNot Nothing Then
				sr.Close()
			End If
		End Try
	End Sub

	Public Sub RestoreGameInfoFile(ByVal gameInfoPathFileName As String)
		Try
			If Me.theGameInfoPathFileNames.Contains(gameInfoPathFileName) Then
				Me.theGameInfoPathFileNames.Remove(gameInfoPathFileName)

				If Not Me.theGameInfoPathFileNames.Contains(gameInfoPathFileName) Then
					If Me.theBackupGameInfoPathFileNames.ContainsKey(gameInfoPathFileName) Then
						Dim backupPathFileName As String
						backupPathFileName = Me.theBackupGameInfoPathFileNames(gameInfoPathFileName)

						If File.Exists(backupPathFileName) Then
							File.Copy(backupPathFileName, gameInfoPathFileName, True)
							File.Delete(backupPathFileName)
							Me.theBackupGameInfoPathFileNames.Remove(gameInfoPathFileName)
						End If
					End If
				End If
			End If
		Catch ex As Exception
			Throw
		End Try
	End Sub

#End Region

#Region "Private Methods"

	Private Sub MakeBackupOfGameInfoFile(ByVal gameInfoPathFileName As String, ByVal newGamePath As String)
		Try
			Me.theGameInfoPathFileNames.Add(gameInfoPathFileName)

			If Not Me.theBackupGameInfoPathFileNames.ContainsKey(gameInfoPathFileName) Then
				If File.Exists(gameInfoPathFileName) Then
					Dim backupPathFileName As String
					backupPathFileName = Path.Combine(FileManager.GetPath(gameInfoPathFileName), Path.GetFileNameWithoutExtension(gameInfoPathFileName) + "_" + newGamePath + Path.GetExtension(gameInfoPathFileName))

					File.Copy(gameInfoPathFileName, backupPathFileName, True)
					Me.theBackupGameInfoPathFileNames.Add(gameInfoPathFileName, backupPathFileName)
				End If
			End If
		Catch ex As Exception
			Throw
		End Try
	End Sub

	Private Sub WriteModifiedFile(ByVal gameInfoPathFileName As String, ByVal newGamePath As String, ByVal textUptoPosition As String, ByVal textPastPosition As String)
		Dim sw As StreamWriter = Nothing

		Try
			sw = New StreamWriter(gameInfoPathFileName)

			sw.Write(textUptoPosition)

			' Write line terminator, then write Game{4 tabs}crowbar
			sw.WriteLine()
			sw.Write(vbTab + vbTab + vbTab + "Game" + vbTab + vbTab + vbTab + vbTab + newGamePath)

			sw.Write(textPastPosition)
		Catch ex As Exception
			Throw
		Finally
			If sw IsNot Nothing Then
				sw.Close()
			End If
		End Try
	End Sub

#End Region

#Region "Data"

	Private Shared theGameInfoTxtFile As GameInfoTxtFile

	' This var is used for simple resource counting.
	Protected theGameInfoPathFileNames As List(Of String)
	Protected theBackupGameInfoPathFileNames As SortedList(Of String, String)

#End Region

End Class
