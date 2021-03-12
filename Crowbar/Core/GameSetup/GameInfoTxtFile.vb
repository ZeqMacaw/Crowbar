Imports System.IO

Public NotInheritable Class GameInfoTxtFile

#Region "Creation and Destruction"

	' Disallow parameterless constructor.
	Private Sub New()
	End Sub

	Public Sub New(ByVal gameInfoPathFileName As String)
		Me.theGameInfoPathFileName = gameInfoPathFileName
		Me.theBackupGameInfoPathFileName = ""
	End Sub

#End Region

#Region "Methods"

	Public Function GetSteamAppId() As String
		Dim steamAppID As String = ""

		Dim sr As StreamReader = Nothing
		Dim sw As StreamWriter = Nothing
		Dim fullText As String = ""
		Dim textUptoPosition As String = ""
		Dim textPastPosition As String = ""

		Try
			If File.Exists(Me.theGameInfoPathFileName) Then
				Dim buffer As String
				Dim token As String = ""

				sr = New StreamReader(Me.theGameInfoPathFileName)
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
										If token = "SteamAppId" Then
											steamAppID = FileManager.ReadKeyValueToken(buffer)
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

		Return steamAppID
	End Function

	Public Sub WriteNewGamePath(ByVal newGamePath As String)
		Me.MakeBackupOfGameInfoFile(newGamePath)

		Dim sr As StreamReader = Nothing
		Dim sw As StreamWriter = Nothing
		Dim fullText As String = ""
		Dim textUptoPosition As String = ""
		Dim textPastPosition As String = ""

		Try
			If File.Exists(Me.theGameInfoPathFileName) Then
				Dim buffer As String
				Dim token As String = ""

				sr = New StreamReader(Me.theGameInfoPathFileName)
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
													Me.WriteModifiedFile(newGamePath, textUptoPosition, textPastPosition)
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

	Public Sub RestoreGameInfoFile()
		Try
			If File.Exists(Me.theBackupGameInfoPathFileName) Then
				File.Copy(Me.theBackupGameInfoPathFileName, Me.theGameInfoPathFileName, True)
				File.Delete(Me.theBackupGameInfoPathFileName)
			End If
		Catch ex As Exception
			Throw
		End Try
	End Sub

#End Region

#Region "Private Methods"

	Private Sub MakeBackupOfGameInfoFile(ByVal newGamePath As String)
		Try
			If File.Exists(Me.theGameInfoPathFileName) Then
				Me.theBackupGameInfoPathFileName = Path.Combine(FileManager.GetPath(Me.theGameInfoPathFileName), Path.GetFileNameWithoutExtension(Me.theGameInfoPathFileName) + "_" + newGamePath + Path.GetExtension(Me.theGameInfoPathFileName))

				File.Copy(Me.theGameInfoPathFileName, Me.theBackupGameInfoPathFileName, True)
			End If
		Catch ex As Exception
			Throw
		End Try
	End Sub

	Private Sub WriteModifiedFile(ByVal newGamePath As String, ByVal textUptoPosition As String, ByVal textPastPosition As String)
		Dim sw As StreamWriter = Nothing

		Try
			sw = New StreamWriter(Me.theGameInfoPathFileName)

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

	Private theGameInfoPathFileName As String
	Private theBackupGameInfoPathFileName As String

#End Region

End Class
