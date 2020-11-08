Imports System.IO
Imports System.Text

Public Class SourceQcFile

#Region "Methods"

	Public Function GetQcModelName(ByVal qcPathFileName As String) As String
		Dim qcModelName As String

		qcModelName = ""

		Using inputFileStream As StreamReader = New StreamReader(qcPathFileName)
			Dim inputLine As String
			Dim modifiedLine As String

			While (Not (inputFileStream.EndOfStream))
				inputLine = inputFileStream.ReadLine()

				modifiedLine = inputLine.ToLower().TrimStart()
				If modifiedLine.StartsWith("""$modelname""") Then
					modifiedLine = modifiedLine.Replace("""$modelname""", "$modelname")
				End If
				If modifiedLine.StartsWith("$modelname") Then
					modifiedLine = modifiedLine.Replace("$modelname", "")
					modifiedLine = modifiedLine.Trim()

					' Need to remove any comment after the file name token (which may or may not be double-quoted).
					Dim pos As Integer
					If modifiedLine.StartsWith("""") Then
						pos = modifiedLine.IndexOf("""", 1)
						If pos >= 0 Then
							modifiedLine = modifiedLine.Substring(1, pos - 1)
						End If
					Else
						pos = modifiedLine.IndexOf(" ")
						If pos >= 0 Then
							modifiedLine = modifiedLine.Substring(0, pos)
						End If
					End If

					'temp = temp.Trim(Chr(34))
					qcModelName = modifiedLine.Replace("/", "\")
					Exit While
				End If
			End While
		End Using

		Return qcModelName
	End Function

	Public Function InsertAnIncludeFileCommand(ByVal qcPathFileName As String, ByVal qciPathFileName As String) As String
		Dim line As String = ""

		Using outputFileStream As StreamWriter = File.AppendText(qcPathFileName)
			outputFileStream.WriteLine()

			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line += "$Include"
			Else
				line += "$include"
			End If
			line += " "
			line += """"
			line += FileManager.GetRelativePathFileName(FileManager.GetPath(qcPathFileName), qciPathFileName)
			line += """"
			outputFileStream.WriteLine(line)
		End Using

		Return line
	End Function

#End Region

#Region "Private Delegates"

	'Private Delegate Sub WriteGroupDelegate()

#End Region

#Region "Private Methods"

	'Private Sub WriteHeaderComment()
	'	Dim line As String = ""

	'	line = "// "
	'	line += TheApp.GetHeaderComment()
	'	Me.theOutputFileStream.WriteLine(line)
	'End Sub

	'Private Sub WriteModelNameCommand()
	'	Dim line As String = ""
	'	'Dim modelPath As String
	'	Dim modelPathFileName As String

	'	'modelPath = FileManager.GetPath(CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0)))
	'	'modelPathFileName = Path.Combine(modelPath, theSourceEngineModel.ModelName + ".mdl")
	'	'modelPathFileName = CStr(theSourceEngineModel.MdlFileHeader.name).Trim(Chr(0))
	'	modelPathFileName = theSourceEngineModel.MdlFileHeader.theName

	'	Me.theOutputFileStream.WriteLine()

	'	'$modelname "survivors/survivor_producer.mdl"
	'	'$modelname "custom/survivor_producer.mdl"
	'If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
	'	line = "$ModelName "
	'Else
	'	line = "$modelname "
	'End If
	'	line += """"
	'	line += modelPathFileName
	'	line += """"
	'	Me.theOutputFileStream.WriteLine(line)
	'End Sub

	'Private Sub WriteGroup(ByVal qciGroupName As String, ByVal writeGroupAction As WriteGroupDelegate, ByVal includeLineIsCommented As Boolean, ByVal includeLineIsIndented As Boolean)
	'	If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
	'		Dim qciFileName As String
	'		Dim qciPathFileName As String
	'		Dim mainOutputFileStream As StreamWriter

	'		mainOutputFileStream = Me.theOutputFileStream

	'		Try
	'			'qciPathFileName = Path.Combine(Me.theOutputPathName, Me.theOutputFileNameWithoutExtension + "_flexes.qci")
	'			qciFileName = Me.theOutputFileNameWithoutExtension + "_" + qciGroupName + ".qci"
	'			qciPathFileName = Path.Combine(Me.theOutputPathName, qciFileName)

	'			Me.theOutputFileStream = File.CreateText(qciPathFileName)

	'			'Me.WriteFlexLines()
	'			'Me.WriteFlexControllerLines()
	'			'Me.WriteFlexRuleLines()
	'			writeGroupAction.Invoke()
	'		Catch ex As Exception
	'			Throw
	'		Finally
	'			If Me.theOutputFileStream IsNot Nothing Then
	'				Me.theOutputFileStream.Flush()
	'				Me.theOutputFileStream.Close()

	'				Me.theOutputFileStream = mainOutputFileStream
	'			End If
	'		End Try

	'		Try
	'			If File.Exists(qciPathFileName) Then
	'				Dim qciFileInfo As New FileInfo(qciPathFileName)
	'				If qciFileInfo.Length > 0 Then
	'					Dim line As String = ""

	'					Me.theOutputFileStream.WriteLine()

	'					If includeLineIsCommented Then
	'						line += "// "
	'					End If
	'					If includeLineIsIndented Then
	'						line += vbTab
	'					End If
	'					line += "$Include"
	'					line += " "
	'					line += """"
	'					line += qciFileName
	'					line += """"
	'					Me.theOutputFileStream.WriteLine(line)
	'				End If
	'			End If
	'		Catch ex As Exception
	'			Throw
	'		End Try
	'	Else
	'		'Me.WriteFlexLines()
	'		'Me.WriteFlexControllerLines()
	'		'Me.WriteFlexRuleLines()
	'		writeGroupAction.Invoke()
	'	End If
	'End Sub

	Protected Function GetSkinFamiliesOfChangedMaterials(ByVal iSkinFamilies As List(Of List(Of Short))) As List(Of List(Of Short))
		Dim skinFamilies As List(Of List(Of Short))
		Dim skinReferenceCount As Integer
		Dim firstSkinFamily As List(Of Short)
		Dim aSkinFamily As List(Of Short)
		Dim textureFileNameIndexes As List(Of Short)

		skinReferenceCount = iSkinFamilies(0).Count
		skinFamilies = New List(Of List(Of Short))(iSkinFamilies.Count)

		Try
			For skinFamilyIndex As Integer = 0 To iSkinFamilies.Count - 1
				textureFileNameIndexes = New List(Of Short)(skinReferenceCount)
				skinFamilies.Add(textureFileNameIndexes)
			Next

			firstSkinFamily = iSkinFamilies(0)
			For j As Integer = 0 To skinReferenceCount - 1
				'NOTE: Start at second skin family because comparing first with all others.
				For i As Integer = 1 To iSkinFamilies.Count - 1
					aSkinFamily = iSkinFamilies(i)

					If firstSkinFamily(j) <> aSkinFamily(j) Then
						For skinFamilyIndex As Integer = 0 To iSkinFamilies.Count - 1
							aSkinFamily = iSkinFamilies(skinFamilyIndex)

							textureFileNameIndexes = skinFamilies(skinFamilyIndex)
							textureFileNameIndexes.Add(aSkinFamily(j))
						Next

						Exit For
					End If
				Next
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return skinFamilies
	End Function

	Protected Function GetTextureGroupSkinFamilyLines(ByVal skinFamilies As List(Of List(Of String))) As List(Of String)
		Dim lines As New List(Of String)()
		Dim aSkinFamily As List(Of String)
		Dim aTextureFileName As String
		Dim line As String = ""

		If TheApp.Settings.DecompileQcSkinFamilyOnSingleLineIsChecked Then
			Dim textureFileNameMaxLengths As New List(Of Integer)()
			Dim length As Integer

			aSkinFamily = skinFamilies(0)
			For textureFileNameIndex As Integer = 0 To aSkinFamily.Count - 1
				aTextureFileName = aSkinFamily(textureFileNameIndex)
				length = aTextureFileName.Length

				textureFileNameMaxLengths.Add(length)
			Next

			For skinFamilyIndex As Integer = 1 To skinFamilies.Count - 1
				aSkinFamily = skinFamilies(skinFamilyIndex)

				For textureFileNameIndex As Integer = 0 To aSkinFamily.Count - 1
					aTextureFileName = aSkinFamily(textureFileNameIndex)
					length = aTextureFileName.Length

					If length > textureFileNameMaxLengths(textureFileNameIndex) Then
						textureFileNameMaxLengths(textureFileNameIndex) = length
					End If
				Next
			Next

			For skinFamilyIndex As Integer = 0 To skinFamilies.Count - 1
				aSkinFamily = skinFamilies(skinFamilyIndex)

				line = vbTab
				line += "{"
				line += " "

				For textureFileNameIndex As Integer = 0 To aSkinFamily.Count - 1
					aTextureFileName = aSkinFamily(textureFileNameIndex)
					length = textureFileNameMaxLengths(textureFileNameIndex)

					'NOTE: Need at least "+ 2" to account for the double-quotes.
					line += LSet("""" + aTextureFileName + """", length + 3)
				Next

				'line += " "
				line += "}"
				lines.Add(line)
			Next
		Else
			For skinFamilyIndex As Integer = 0 To skinFamilies.Count - 1
				aSkinFamily = skinFamilies(skinFamilyIndex)

				line = vbTab
				line += "{"
				lines.Add(line)

				For textureFileNameIndex As Integer = 0 To aSkinFamily.Count - 1
					aTextureFileName = aSkinFamily(textureFileNameIndex)

					line = vbTab
					line += vbTab
					line += """"
					line += aTextureFileName
					line += """"

					lines.Add(line)
				Next

				line = vbTab
				line += "}"
				lines.Add(line)
			Next
		End If

		Return lines
	End Function

#End Region

#Region "Data"

	'Private theSourceEngineModel As SourceModel_Old
	'Private theOutputFileStream As StreamWriter
	'Private theOutputPathName As String
	'Private theOutputFileNameWithoutExtension As String

#End Region

End Class
