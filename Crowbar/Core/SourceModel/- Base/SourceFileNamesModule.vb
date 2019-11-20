Imports System.IO

Module SourceFileNamesModule

	Public Function CreateBodyGroupSmdFileName(ByVal givenBodyGroupSmdFileName As String, ByVal bodyPartIndex As Integer, ByVal modelIndex As Integer, ByVal lodIndex As Integer, ByVal modelName As String, ByVal bodyModelName As String) As String
		'Dim bodyModelNameTrimmed As String
		'Dim bodyModelFileName As String = ""
		'Dim bodyModelFileNameWithoutExtension As String
		'Dim bodyGroupSmdFileName As String = ""

		'If (bodyPartIndex = 0 AndAlso modelIndex = 0 AndAlso lodIndex = 0) _
		' AndAlso (theModelCommandIsUsed OrElse (bodyPartCount = 1 AndAlso bodyModelCount = 1)) _
		' Then
		'	bodyGroupSmdFileName = modelName
		'	bodyGroupSmdFileName += "_reference"
		'	bodyGroupSmdFileName += ".smd"
		'Else
		'	If FileManager.FilePathHasInvalidChars(bodyModelName.Trim(Chr(0))) Then
		'		bodyModelFileName = "body"
		'		bodyModelFileName += CStr(bodyPartIndex)
		'		bodyModelFileName += "_model"
		'		bodyModelFileName += CStr(modelIndex)
		'	Else
		'		bodyModelFileName = Path.GetFileName(bodyModelName.Trim(Chr(0))).ToLower(TheApp.InternalCultureInfo)
		'	End If
		'	bodyModelFileNameWithoutExtension = Path.GetFileNameWithoutExtension(bodyModelFileName)

		'	If Not bodyModelFileName.StartsWith(modelName) Then
		'		bodyGroupSmdFileName += modelName + "_"
		'	End If
		'	bodyGroupSmdFileName += bodyModelFileNameWithoutExtension
		'	If lodIndex > 0 Then
		'		bodyGroupSmdFileName += "_lod"
		'		bodyGroupSmdFileName += lodIndex.ToString()
		'	End If
		'	bodyGroupSmdFileName += ".smd"
		'End If
		'======
		'bodyModelNameTrimmed = bodyModelName.Trim(Chr(0))
		'Try
		'	bodyModelFileName = Path.GetFileName(bodyModelNameTrimmed).ToLower(TheApp.InternalCultureInfo)
		'	If FileManager.FilePathHasInvalidChars(bodyModelFileName) Then
		'		bodyModelFileName = "body"
		'		bodyModelFileName += CStr(bodyPartIndex)
		'		bodyModelFileName += "_model"
		'		bodyModelFileName += CStr(modelIndex)
		'	End If
		'Catch ex As Exception
		'	bodyModelFileName = "body"
		'	bodyModelFileName += CStr(bodyPartIndex)
		'	bodyModelFileName += "_model"
		'	bodyModelFileName += CStr(modelIndex)
		'End Try
		'bodyModelFileNameWithoutExtension = Path.GetFileNameWithoutExtension(bodyModelFileName)
		'
		'If Not bodyModelFileName.StartsWith(modelName) Then
		'	bodyGroupSmdFileName += modelName + "_"
		'End If
		'bodyGroupSmdFileName += bodyModelFileNameWithoutExtension
		'If lodIndex > 0 Then
		'	bodyGroupSmdFileName += "_lod"
		'	bodyGroupSmdFileName += lodIndex.ToString()
		'End If
		'bodyGroupSmdFileName += ".smd"
		'======
		''NOTE: Ignore bodyModelName altogether because already making up the first part of file names 
		''      so might as well make the rest of the file names unique with an easy pattern.

		'Dim bodyGroupSmdFileName As String

		'If bodyPartIndex = 0 AndAlso modelIndex = 0 AndAlso lodIndex = 0 AndAlso Not String.IsNullOrEmpty(sequenceGroupFileName) AndAlso Not FileManager.FilePathHasInvalidChars(sequenceGroupFileName) Then
		'	bodyGroupSmdFileName = Path.GetFileName(sequenceGroupFileName.Trim(Chr(0))).ToLower(TheApp.InternalCultureInfo)
		'	If Not bodyGroupSmdFileName.StartsWith(modelName) Then
		'		bodyGroupSmdFileName = modelName + "_" + bodyGroupSmdFileName
		'	End If
		'Else
		'	bodyGroupSmdFileName = modelName
		'	bodyGroupSmdFileName += "_"
		'	If bodyPartCount = 1 AndAlso bodyModelCount = 1 AndAlso lodIndex = 0 Then
		'		bodyGroupSmdFileName += "reference"
		'	Else
		'		bodyGroupSmdFileName += "body"
		'		bodyGroupSmdFileName += CStr(bodyPartIndex)
		'		bodyGroupSmdFileName += "_model"
		'		bodyGroupSmdFileName += CStr(modelIndex)
		'	End If
		'	If lodIndex > 0 Then
		'		bodyGroupSmdFileName += "_lod"
		'		bodyGroupSmdFileName += lodIndex.ToString()
		'	End If
		'	If includeExtension Then
		'		bodyGroupSmdFileName += ".smd"
		'	End If
		'End If
		'======
		' Use bodyModel name, but make sure the file name is unique for this model.
		Dim bodyGroupSmdFileName As String = ""
		Dim bodyModelFileName As String = ""
		Dim bodyModelFileNameWithoutExtension As String = ""

		If Not String.IsNullOrEmpty(givenBodyGroupSmdFileName) Then
			bodyGroupSmdFileName = givenBodyGroupSmdFileName
		Else
			Try
				bodyModelFileName = Path.GetFileName(bodyModelName.Trim(Chr(0)))
				If FileManager.FilePathHasInvalidChars(bodyModelFileName) Then
					bodyModelFileName = "body"
					bodyModelFileName += CStr(bodyPartIndex)
					bodyModelFileName += "_model"
					bodyModelFileName += CStr(modelIndex)
				End If
			Catch ex As Exception
				bodyModelFileName = "body"
				bodyModelFileName += CStr(bodyPartIndex)
				bodyModelFileName += "_model"
				bodyModelFileName += CStr(modelIndex)
			End Try
			bodyModelFileNameWithoutExtension = Path.GetFileNameWithoutExtension(bodyModelFileName)

			If TheApp.Settings.DecompilePrefixFileNamesWithModelNameIsChecked AndAlso Not bodyModelFileName.ToLower(TheApp.InternalCultureInfo).StartsWith(modelName.ToLower(TheApp.InternalCultureInfo)) Then
				bodyGroupSmdFileName += modelName + "_"
			End If
			bodyGroupSmdFileName += bodyModelFileNameWithoutExtension
			If lodIndex > 0 Then
				bodyGroupSmdFileName += "_lod"
				bodyGroupSmdFileName += lodIndex.ToString()
			End If

			bodyGroupSmdFileName = SourceFileNamesModule.GetUniqueSmdFileName(bodyGroupSmdFileName)

			bodyGroupSmdFileName += ".smd"
		End If

		Return bodyGroupSmdFileName
	End Function

	Public Function GetAnimationSmdRelativePath(ByVal modelName As String) As String
		Dim path As String

		path = ""
		If TheApp.Settings.DecompileBoneAnimationPlaceInSubfolderIsChecked Then
			path = modelName + "_" + App.AnimsSubFolderName
		End If

		Return path
	End Function

	Public Function CreateAnimationSmdRelativePathFileName(ByVal givenAnimationSmdRelativePathFileName As String, ByVal modelName As String, ByVal iAnimationName As String, Optional ByVal blendIndex As Integer = -2) As String
		Dim animationName As String
		Dim animationSmdRelativePathFileName As String

		If Not String.IsNullOrEmpty(givenAnimationSmdRelativePathFileName) Then
			animationSmdRelativePathFileName = givenAnimationSmdRelativePathFileName
		Else
			' Clean the iAnimationName.
			Try
				iAnimationName = iAnimationName.Trim(Chr(0))
				'iAnimationName = iAnimationName.Replace(":", "")
				'iAnimationName = iAnimationName.Replace("\", "")
				'iAnimationName = iAnimationName.Replace("/", "")
				For Each invalidChar As Char In Path.GetInvalidFileNameChars()
					iAnimationName = iAnimationName.Replace(invalidChar, "")
				Next
				If FileManager.FilePathHasInvalidChars(iAnimationName) Then
					iAnimationName = "anim"
				End If
			Catch ex As Exception
				iAnimationName = "anim"
			End Try

			' Set the name
			If blendIndex >= 0 Then
				' For MDL v6 and v10.
				animationName = iAnimationName + "_blend" + (blendIndex + 1).ToString("00")
			ElseIf blendIndex = -1 Then
				' For MDL v6 and v10.
				animationName = iAnimationName
			Else
				If String.IsNullOrEmpty(iAnimationName) Then
					animationName = ""
				ElseIf iAnimationName(0) = "@"c Then
					'NOTE: The file name for the animation data file is not stored in mdl file (which makes sense), 
					'      so make the file name the same as the animation name.
					animationName = iAnimationName.Substring(1)
				Else
					animationName = iAnimationName
				End If
			End If

			' If anims are not stored in anims folder, add some more to the name.
			If Not TheApp.Settings.DecompileBoneAnimationPlaceInSubfolderIsChecked Then
				animationName = modelName + "_anim_" + iAnimationName
			End If

			' Set the path.
			animationSmdRelativePathFileName = Path.Combine(GetAnimationSmdRelativePath(modelName), animationName)

			animationSmdRelativePathFileName = SourceFileNamesModule.GetUniqueSmdFileName(animationSmdRelativePathFileName)

			' Set the extension.
			If Path.GetExtension(animationSmdRelativePathFileName) <> ".smd" Then
				'animationSmdRelativePathFileName = Path.ChangeExtension(animationSmdRelativePathFileName, ".smd")
				'NOTE: Add the ".smd" extension, keeping the existing extension in file name, which is often ".dmx" for newer models. 
				'      Thus, user can see that model might have newer features that Crowbar does not yet handle.
				animationSmdRelativePathFileName += ".smd"
			End If
		End If

		Return animationSmdRelativePathFileName
	End Function

	Public Function CreateCorrectiveAnimationName(ByVal givenAnimationSmdRelativePathFileName As String) As String
		Dim animationName As String

		animationName = givenAnimationSmdRelativePathFileName + "_" + "corrective_animation"

		Return animationName
	End Function

	Public Function CreateCorrectiveAnimationSmdRelativePathFileName(ByVal givenAnimationSmdRelativePathFileName As String, ByVal modelName As String) As String
		Dim animationSmdRelativePathFileName As String

		animationSmdRelativePathFileName = CreateCorrectiveAnimationName(givenAnimationSmdRelativePathFileName) + ".smd"
		animationSmdRelativePathFileName = Path.Combine(GetAnimationSmdRelativePath(modelName), animationSmdRelativePathFileName)

		Return animationSmdRelativePathFileName
	End Function

	Public Function GetVrdFileName(ByVal modelName As String) As String
		Dim vrdFileName As String

		vrdFileName = modelName
		vrdFileName += ".vrd"

		Return vrdFileName
	End Function

	Public Function GetVtaFileName(ByVal modelName As String, ByVal bodyPartIndex As Integer) As String
		Dim vtaFileName As String

		vtaFileName = modelName
		vtaFileName += "_"
		vtaFileName += (bodyPartIndex + 1).ToString("00")
		vtaFileName += ".vta"

		Return vtaFileName
	End Function

	Public Function CreatePhysicsSmdFileName(ByVal givenPhysicsSmdFileName As String, ByVal modelName As String) As String
		Dim physicsSmdFileName As String

		If Not String.IsNullOrEmpty(givenPhysicsSmdFileName) Then
			physicsSmdFileName = givenPhysicsSmdFileName
		Else
			physicsSmdFileName = modelName
			physicsSmdFileName += "_physics"

			physicsSmdFileName = SourceFileNamesModule.GetUniqueSmdFileName(physicsSmdFileName)

			physicsSmdFileName += ".smd"
		End If

		Return physicsSmdFileName
	End Function

	Public Function GetDeclareSequenceQciFileName(ByVal modelName As String) As String
		Dim declareSequenceQciFileName As String

		declareSequenceQciFileName = modelName
		declareSequenceQciFileName += "_DeclareSequence.qci"

		Return declareSequenceQciFileName
	End Function

	''TODO: Call *after* both ReadTextures() and ReadTexturePaths() are called.
	'Public Sub CopyPathsFromTextureFileNamesToTexturePaths(ByVal texturePaths As List(Of String), ByVal texturePathFileNames As List(Of String))
	'	' Make all lowercase list copy of texturePaths.
	'	Dim texturePathsLowercase As List(Of String)
	'	texturePathsLowercase = New List(Of String)(texturePaths.Count)
	'	For Each aTexturePath As String In texturePaths
	'		texturePathsLowercase.Add(aTexturePath.ToLower())
	'	Next

	'	For texturePathFileNameIndex As Integer = 0 To texturePathFileNames.Count - 1
	'		Dim aTexturePathFileName As String
	'		Dim aTexturePathFileNameLowercase As String
	'		aTexturePathFileName = texturePathFileNames(texturePathFileNameIndex)
	'		aTexturePathFileNameLowercase = aTexturePathFileName.ToLower()

	'		' If the texturePathFileName starts with a path that is in the texturePaths list, then remove the texturePath from the texturePathFileName.
	'		For texturePathIndex As Integer = 0 To texturePathsLowercase.Count - 1
	'			Dim aTexturePathLowercase As String
	'			aTexturePathLowercase = texturePathsLowercase(texturePathIndex)

	'			If aTexturePathLowercase <> "" AndAlso aTexturePathFileNameLowercase.StartsWith(aTexturePathLowercase) Then
	'				Dim startOffsetAfterPathSeparator As Integer
	'				If aTexturePathLowercase.EndsWith(Path.DirectorySeparatorChar) OrElse aTexturePathLowercase.EndsWith(Path.AltDirectorySeparatorChar) Then
	'					startOffsetAfterPathSeparator = aTexturePathLowercase.Length
	'				Else
	'					startOffsetAfterPathSeparator = aTexturePathLowercase.Length + 1
	'				End If
	'				texturePathFileNames(texturePathFileNameIndex) = aTexturePathFileName.Substring(startOffsetAfterPathSeparator)
	'				Exit For
	'			End If
	'		Next

	'		Dim texturePath As String
	'		Dim texturePathLowercase As String
	'		Dim textureFileName As String
	'		texturePath = FileManager.GetPath(aTexturePathFileName)
	'		texturePathLowercase = texturePath.ToLower()
	'		textureFileName = Path.GetFileName(aTexturePathFileName)
	'		If aTexturePathFileName <> textureFileName AndAlso Not texturePathsLowercase.Contains(texturePathLowercase) AndAlso Not texturePathsLowercase.Contains(texturePathLowercase + Path.DirectorySeparatorChar) AndAlso Not texturePathsLowercase.Contains(texturePathLowercase + Path.AltDirectorySeparatorChar) Then
	'			'NOTE: Place first because it should override whatever is already in list.
	'			texturePaths.Insert(0, texturePath)
	'		End If
	'	Next
	'End Sub

	'NOTE: Call *after* both ReadTextures() and ReadTexturePaths() are called.
	Public Sub MovePathsFromTextureFileNamesToTexturePaths(ByRef texturePaths As List(Of String), ByRef texturePathFileNames As List(Of String))
		' Make all lowercase list copy of texturePaths.
		Dim texturePathsLowercase As List(Of String)
		texturePathsLowercase = New List(Of String)(texturePaths.Count)
		For Each aTexturePath As String In texturePaths
			texturePathsLowercase.Add(aTexturePath.ToLower())
		Next

		'NOTE: Use index so can modify the list member, not a copy of it.
		For fileNameIndex As Integer = 0 To texturePathFileNames.Count - 1
			Dim aTexturePathFileName As String
			aTexturePathFileName = texturePathFileNames(fileNameIndex)
            aTexturePathFileName = FileManager.GetCleanPathFileName(aTexturePathFileName, False)

            Dim aTexturePath As String
			Dim aTextureFileName As String
			aTexturePath = FileManager.GetPath(aTexturePathFileName)
			aTextureFileName = Path.GetFileName(aTexturePathFileName)

			Dim aTexturePathFileNameLowercase As String
			Dim aTexturePathLowercase As String
			aTexturePathFileNameLowercase = aTexturePathFileName.ToLower()
			aTexturePathLowercase = FileManager.GetPath(aTexturePathFileNameLowercase)

			' If the texturePathFileName starts with a path, then ...
			If aTexturePathLowercase <> "" Then
				' ... insert the path into texturePaths, if it is not already there.
				If Not texturePathsLowercase.Contains(aTexturePathLowercase) AndAlso Not texturePathsLowercase.Contains(aTexturePathLowercase + Path.DirectorySeparatorChar) AndAlso Not texturePathsLowercase.Contains(aTexturePathLowercase + Path.AltDirectorySeparatorChar) Then
					'NOTE: Place first because it should override whatever is already in list.
					texturePaths.Insert(0, aTexturePath)
					texturePathsLowercase.Insert(0, aTexturePathLowercase)
				End If

				' ... and remove it from the texturePathFileName in texturePathFileNames.
				texturePathFileNames(fileNameIndex) = aTextureFileName
			End If
		Next
	End Sub

	Private Function GetUniqueSmdFileName(ByVal givenSmdFileName As String) As String
		Dim smdFileName As String = givenSmdFileName

		'NOTE: Starting this at 1 means the first file name will not have a number and the second name will have a 2.
		Dim nameNumber As Integer = 1
		While TheApp.SmdFileNames.Contains(smdFileName.ToLower(TheApp.InternalCultureInfo))
			nameNumber += 1
			smdFileName = givenSmdFileName + "_" + nameNumber.ToString()
		End While

		TheApp.SmdFileNames.Add(smdFileName.ToLower(TheApp.InternalCultureInfo))
		Return smdFileName
	End Function

End Module
