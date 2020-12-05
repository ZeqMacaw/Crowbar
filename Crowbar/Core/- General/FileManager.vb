Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Xml.Serialization

Public Class FileManager

#Region "Read Methods"

	Public Shared Function FilePathHasInvalidChars(ByVal path As String) As Boolean
		Dim ret As Boolean = False

		If String.IsNullOrEmpty(path) Then
			ret = True
		Else
			Try
				Dim fileName As String = System.IO.Path.GetFileName(path)
				Dim fileDirectory As String = System.IO.Path.GetDirectoryName(path)
			Catch generatedExceptionName As ArgumentException
				' Path functions will throw this 
				' if path contains invalid chars
				ret = True
			End Try
			ret = (path.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0)
			If ret = False Then
				ret = (path.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
			End If
		End If

		Return ret
	End Function

	Public Shared Function ReadNullTerminatedString(ByVal inputFileReader As BinaryReader) As String
		Dim text As New StringBuilder()
		text.Length = 0
		While inputFileReader.PeekChar() > 0
			text.Append(inputFileReader.ReadChar())
		End While
		' Read the null character.
		inputFileReader.ReadChar()
		Return text.ToString()
	End Function

	'Public Function ReadKeyValueLine(ByVal textFileReader As StreamReader, ByVal key As String) As String
	'	Dim line As String
	'	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
	'	Dim tokens As String()
	'	line = textFileReader.ReadLine()
	'	If line IsNot Nothing Then
	'		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
	'		If tokens.Length = 2 AndAlso tokens(0) = key Then
	'			Return tokens(1)
	'		End If
	'	End If
	'	Throw New Exception()
	'End Function

	'Public Function ReadKeyValueLine(ByVal textFileReader As StreamReader, ByVal key1 As String, ByVal key2 As String, ByRef oKey As String, ByRef oValue As String) As Boolean
	'	Dim line As String
	'	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
	'	Dim tokens As String()
	'	line = textFileReader.ReadLine()
	'	If line IsNot Nothing Then
	'		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
	'		If tokens.Length = 2 AndAlso (tokens(0) = key1 OrElse tokens(0) = key2) Then
	'			oKey = tokens(0)
	'			oValue = tokens(1)
	'			Return True
	'		End If
	'	End If
	'	Return False
	'End Function

	'Public Function ReadKeyValueLine(ByVal textFileReader As StreamReader, ByRef oKey As String, ByRef oValue As String) As Boolean
	'	Dim line As String
	'	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
	'	Dim tokens As String() = {""}
	'	line = textFileReader.ReadLine()
	'	If line IsNot Nothing Then
	'		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
	'		If tokens.Length = 2 Then
	'			oKey = tokens(0)
	'			oValue = tokens(1)
	'			Return True
	'		ElseIf tokens.Length = 1 Then
	'			oKey = tokens(0)
	'			oValue = tokens(0)
	'			Return False
	'		End If
	'	End If
	'	oKey = line
	'	oValue = line
	'	Return False
	'End Function

	'Public Shared Function ReadKeyValueLine(ByVal inputFileReader As BinaryReader, ByRef oKey As String, ByRef oValue As String) As Boolean
	'	Dim line As String
	'	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
	'	Dim tokens As String() = {""}
	'	line = ReadTextLine(inputFileReader)
	'	If line IsNot Nothing Then
	'		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
	'		If tokens.Length = 2 Then
	'			oKey = tokens(0)
	'			oValue = tokens(1)
	'			Return True
	'		ElseIf tokens.Length = 1 Then
	'			oKey = tokens(0)
	'			oValue = tokens(0)
	'			Return False
	'		End If
	'	End If
	'	oKey = line
	'	oValue = line
	'	Return False
	'End Function

	Public Shared Function ReadKeyValueLine(ByVal inputFileReader As BinaryReader, ByRef oKey As String, ByRef oValue As String) As Boolean
		Dim line As String
		Dim delimiters As Char() = {""""c}
		Dim tokens As String() = {""}
		line = ReadTextLine(inputFileReader)
		If line IsNot Nothing Then
			tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
			If tokens.Length = 3 Then
				oKey = tokens(0)
				oValue = tokens(2)
				Return True
			ElseIf tokens.Length = 2 Then
				oKey = tokens(0)
				oValue = tokens(1)
				Return True
			ElseIf tokens.Length = 1 Then
				oKey = tokens(0)
				oValue = tokens(0)
				Return False
			End If
		End If
		oKey = line
		oValue = line
		Return False
	End Function

	'const char *KeyValues::ReadToken( CUtlBuffer &buf, bool &wasQuoted, bool &wasConditional )
	'{
	'	wasQuoted = false;
	'	wasConditional = false;
	'
	'	if ( !buf.IsValid() )
	'		return NULL; 
	'
	'	// eating white spaces and remarks loop
	'	while ( true )
	'	{
	'		buf.EatWhiteSpace();
	'		if ( !buf.IsValid() )
	'			return NULL;	// file ends after reading whitespaces
	'
	'		// stop if it's not a comment; a new token starts here
	'		if ( !buf.EatCPPComment() )
	'			break;
	'	}
	'
	'	const char *c = (const char*)buf.PeekGet( sizeof(char), 0 );
	'	if ( !c )
	'		return NULL;
	'
	'	// read quoted strings specially
	'	if ( *c == '\"' )
	'	{
	'		wasQuoted = true;
	'		buf.GetDelimitedString( m_bHasEscapeSequences ? GetCStringCharConversion() : GetNoEscCharConversion(), 
	'			s_pTokenBuf, KEYVALUES_TOKEN_SIZE );
	'		return s_pTokenBuf;
	'	}
	'
	'	if ( *c == '{' || *c == '}' )
	'	{
	'		// it's a control char, just add this one char and stop reading
	'		s_pTokenBuf[0] = *c;
	'		s_pTokenBuf[1] = 0;
	'		buf.SeekGet( CUtlBuffer::SEEK_CURRENT, 1 );
	'		return s_pTokenBuf;
	'	}
	'
	'	// read in the token until we hit a whitespace or a control character
	'	bool bReportedError = false;
	'	bool bConditionalStart = false;
	'	int nCount = 0;
	'	while ( ( c = (const char*)buf.PeekGet( sizeof(char), 0 ) ) )
	'	{
	'		// end of file
	'		if ( *c == 0 )
	'			break;
	'
	'		// break if any control character appears in non quoted tokens
	'		if ( *c == '"' || *c == '{' || *c == '}' )
	'			break;
	'
	'		if ( *c == '[' )
	'			bConditionalStart = true;
	'
	'		if ( *c == ']' && bConditionalStart )
	'		{
	'			wasConditional = true;
	'		}
	'
	'		// break on whitespace
	'		if ( isspace(*c) )
	'			break;
	'
	'		if (nCount < (KEYVALUES_TOKEN_SIZE-1) )
	'		{
	'			s_pTokenBuf[nCount++] = *c;	// add char to buffer
	'		}
	'		else if ( !bReportedError )
	'		{
	'			bReportedError = true;
	'			g_KeyValuesErrorStack.ReportError(" ReadToken overflow" );
	'		}
	'
	'		buf.SeekGet( CUtlBuffer::SEEK_CURRENT, 1 );
	'	}
	'	s_pTokenBuf[ nCount ] = 0;
	'	return s_pTokenBuf;
	'}
	Public Shared Function ReadKeyValueToken(ByRef buffer As String) As String
		Dim token As String

		Do
			buffer = buffer.TrimStart()
			If buffer.StartsWith("/") Then
				Dim pos As Integer
				pos = buffer.IndexOf(Chr(&HA), 1)
				If pos > -1 Then
					buffer = buffer.Substring(pos + 1)
				Else
					buffer = ""
					Exit Do
				End If
			Else
				Exit Do
			End If
		Loop Until buffer = ""

		If buffer = "" Then
			token = ""
		ElseIf buffer.StartsWith("""") Then
			Dim pos As Integer
			pos = buffer.IndexOf("""", 1)
			If pos > -1 Then
				'NOTE: Remove the double-quotes.
				token = buffer.Substring(1, pos - 1)
				buffer = buffer.Substring(pos + 1)
			Else
				token = ""
			End If
		ElseIf buffer.StartsWith("{") OrElse buffer.StartsWith("}") Then
			token = buffer.Substring(0, 1)
			buffer = buffer.Substring(1)
		Else
			' Read in the token characters until a control character.
			Dim delimiters As Char() = {""""c, "{"c, "}"c}
			Dim tokens As String() = {""}
			tokens = buffer.Split(delimiters)
			token = tokens(0)

			tokens = token.Split()
			token = tokens(0)
			buffer = buffer.Substring(token.Length)
		End If

		Return token
	End Function

	Public Shared Function ReadTextLine(ByVal inputFileReader As BinaryReader) As String
		Dim line As New StringBuilder()
		Dim aChar As Char = " "c
		Try
			While True
				aChar = inputFileReader.ReadChar()
				If aChar = Chr(0) OrElse aChar = Chr(&HA) Then
					Exit While
				End If
				line.Append(aChar)
			End While
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
		If line.Length > 0 Then
			Return line.ToString()
		End If
		Return Nothing
	End Function

#End Region

#Region "Path"

	Public Shared Function GetPathFileNameWithoutExtension(ByVal pathFileName As String) As String
		Try
			Return Path.Combine(FileManager.GetPath(pathFileName), Path.GetFileNameWithoutExtension(pathFileName))
		Catch ex As Exception
			Return String.Empty
		End Try
	End Function

	'NOTE: Replacement for Path.GetDirectoryName, because GetDirectoryName returns "Nothing" when something like "C:\" is the path.
	Public Shared Function GetPath(ByVal pathFileName As String) As String
		Try
			pathFileName = FileManager.GetNormalizedPathFileName(pathFileName)
			Dim length As Integer = pathFileName.LastIndexOf(Path.DirectorySeparatorChar)
			If length < 1 Then
				pathFileName = ""
			ElseIf length > 0 Then
				pathFileName = pathFileName.Substring(0, length)
			End If
			If pathFileName.Length = 2 AndAlso pathFileName(1) = ":"c Then
				pathFileName += Path.DirectorySeparatorChar
			End If
			Return pathFileName
		Catch ex As Exception
			Return String.Empty
		End Try
	End Function

	Public Shared Sub CreatePath(ByVal path As String)
		Try
			If Not Directory.Exists(path) Then
				Directory.CreateDirectory(path)
			End If
		Catch ex As Exception
			Throw ex
		End Try
	End Sub

	Public Shared Function PathExistsAfterTryToCreate(ByVal aPath As String) As Boolean
		If Not Directory.Exists(aPath) Then
			Try
				Directory.CreateDirectory(aPath)
			Catch ex As Exception
			End Try
		End If
		Return Directory.Exists(aPath)
	End Function

	Private Shared Function GetFullPath(maybeRelativePath As String, baseDirectory As String) As String
		If baseDirectory Is Nothing Then
			baseDirectory = Environment.CurrentDirectory
		End If

		Dim root As String = Path.GetPathRoot(maybeRelativePath)
		If String.IsNullOrEmpty(root) Then
			Return Path.GetFullPath(Path.Combine(baseDirectory, maybeRelativePath))
		ElseIf root = "\" Then
			Return Path.GetFullPath(Path.Combine(Path.GetPathRoot(baseDirectory), maybeRelativePath.Remove(0, 1)))
		End If

		Return maybeRelativePath
	End Function

	Public Shared Function GetRelativePathFileName(ByVal fromPath As String, ByVal toPath As String) As String
		Dim fromPathAbsolute As String
		Dim toPathAbsolute As String

		If fromPath = "" Then
			Return toPath
		End If

		fromPathAbsolute = Path.GetFullPath(fromPath)
		toPathAbsolute = Path.GetFullPath(toPath)

		'Dim fromAttr As Integer = GetPathAttribute(fromPathAbsolute)
		'Dim toAttr As Integer = GetPathAttribute(toPathAbsolute)

		'IMPORTANT: Use Uri.MakeRelativeUri() instead of PathRelativePathTo(), 
		'      because PathRelativePathTo() does not handle unicode characters properly.
		' MAX_PATH = 260
		'Dim newPathFileName As New StringBuilder(260)
		'If PathRelativePathTo(newPathFileName, fromPathAbsolute, fromAttr, toPathAbsolute, toAttr) = 0 Then
		'	'Throw New ArgumentException("Paths must have a common prefix")
		'	Return toPathAbsolute
		'End If
		'NOTE: Need to add the Path.DirectorySeparatorChar to force MakeRelativeUri() to treat the paths as folder names, not file names.
		'      Otherwise, for example, this happens:
		'      path1 = "C:\temp\Crowbar"
		'      path2 = "C:\temp\Crowbar\addon.txt"
		'      diff  = "Crowbar\addon.txt"
		'      WANT: diff = "addon.txt"
		Dim path1 As Uri = New Uri(fromPathAbsolute + Path.DirectorySeparatorChar)
		Dim path2 As Uri = New Uri(toPathAbsolute + Path.DirectorySeparatorChar)
		Dim diff As Uri = path1.MakeRelativeUri(path2)
		' Convert Uri escaped characters and convert Uri forward slash to default directory separator.
		Dim newPathFileName As String = Uri.UnescapeDataString(diff.OriginalString).Replace("/", Path.DirectorySeparatorChar)

        Dim cleanedPath As String
		cleanedPath = newPathFileName.ToString()
		If cleanedPath.StartsWith("." + Path.DirectorySeparatorChar) Then
			cleanedPath = cleanedPath.Remove(0, 2)
		End If
		'NOTE: Remove the ending path separator that is there because of modified inputs to MakeRelativeUri() earlier.
		cleanedPath = cleanedPath.TrimEnd(Path.DirectorySeparatorChar)
		Return cleanedPath
	End Function

	Public Shared Function GetCleanPath(ByVal givenPath As String, ByVal returnFullPath As Boolean) As String
		Dim cleanPath As String
		cleanPath = givenPath
		For Each invalidChar As Char In Path.GetInvalidPathChars()
			cleanPath = cleanPath.Replace(invalidChar, "")
		Next
		If returnFullPath Then
			Try
				cleanPath = Path.GetFullPath(cleanPath)
			Catch ex As Exception
				cleanPath = cleanPath.Replace(":", "")
			End Try
		End If

		Return cleanPath
	End Function

	Public Shared Function GetCleanPathFileName(ByVal givenPathFileName As String, ByVal returnFullPathFileName As Boolean) As String
        Dim cleanPathFileName As String

        Dim cleanedPathGivenPathFileName As String
        cleanedPathGivenPathFileName = givenPathFileName
        For Each invalidChar As Char In Path.GetInvalidPathChars()
            cleanedPathGivenPathFileName = cleanedPathGivenPathFileName.Replace(invalidChar, "")
        Next
        If returnFullPathFileName Then
            Try
                cleanedPathGivenPathFileName = Path.GetFullPath(cleanedPathGivenPathFileName)
            Catch ex As Exception
                cleanedPathGivenPathFileName = cleanedPathGivenPathFileName.Replace(":", "")
            End Try
        End If

        Dim cleanedGivenFileName As String
        cleanedGivenFileName = Path.GetFileName(cleanedPathGivenPathFileName)
        For Each invalidChar As Char In Path.GetInvalidFileNameChars()
            cleanedGivenFileName = cleanedGivenFileName.Replace(invalidChar, "")
        Next

        Dim cleanedGivenPath As String
        cleanedGivenPath = FileManager.GetPath(cleanedPathGivenPathFileName)

        If cleanedGivenFileName = "" Then
            cleanPathFileName = cleanedPathGivenPathFileName
        Else
            cleanPathFileName = Path.Combine(cleanedGivenPath, cleanedGivenFileName)
        End If

        Return cleanPathFileName
    End Function

	Public Shared Sub ParsePath(ByVal sender As Object, ByVal e As ConvertEventArgs)
		If e.DesiredType IsNot GetType(String) Then
			Exit Sub
		End If
		If CStr(e.Value) <> "" Then
			e.Value = FileManager.GetCleanPath(CStr(e.Value), True)
		End If
	End Sub

	Public Shared Sub ParsePathFileName(ByVal sender As Object, ByVal e As ConvertEventArgs)
		If e.DesiredType IsNot GetType(String) Then
			Exit Sub
		End If
		If CStr(e.Value) <> "" Then
            e.Value = FileManager.GetCleanPathFileName(CStr(e.Value), True)
        End If
	End Sub

	Public Shared Function GetNormalizedPathFileName(ByVal givenPathFileName As String) As String
		Dim cleanPathFileName As String

		cleanPathFileName = givenPathFileName
		If Path.DirectorySeparatorChar <> Path.AltDirectorySeparatorChar Then
			cleanPathFileName = givenPathFileName.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
		End If

		Return cleanPathFileName
	End Function

	Public Shared Function GetTestedPathFileName(ByVal iPathFileName As String) As String
		Dim testedPathFileName As String = iPathFileName
		Dim pathFileNameWithoutExtension As String = FileManager.GetPathFileNameWithoutExtension(iPathFileName)
		Dim extension As String = Path.GetExtension(iPathFileName)
		Dim number As Integer = 1
		While File.Exists(testedPathFileName)
			testedPathFileName = pathFileNameWithoutExtension + "(" + number.ToString() + ")" + extension
			number += 1
		End While
		Return testedPathFileName
	End Function

	Public Shared Function GetTestedPath(ByVal iPath As String) As String
		Dim testedPathFileName As String = iPath
		Dim number As Integer = 1
		While Directory.Exists(testedPathFileName)
			testedPathFileName = iPath + "(" + number.ToString() + ")"
			number += 1
		End While
		Return testedPathFileName
	End Function

	Public Shared Function GetLongestExtantPath(ByVal iPath As String, Optional ByRef topNonextantPath As String = "") As String
		If iPath <> "" AndAlso Not Directory.Exists(iPath) Then
			topNonextantPath = iPath
			Dim shorterPath As String
			shorterPath = FileManager.GetPath(iPath)
			'NOTE: This "If" handles cases such as iPath = "F:\" when "F" is not a valid drive.
			If shorterPath = iPath Then
				iPath = ""
			Else
				iPath = FileManager.GetLongestExtantPath(shorterPath, topNonextantPath)
			End If
		End If
		Return iPath
	End Function

	' Example: "C:\folder\subfolder\temp" returns "C:\folder"
	' Example: "subfolder\temp"           returns "subfolder"
	' Example: "temp"                     returns ""
	Public Shared Function GetTopFolderPath(ByVal iPathFileName As String) As String
		Dim topFolderPath As String = ""
		Dim fullPath As String
		Dim splitPathArray As String()

		If FileManager.GetPath(iPathFileName) = "" Then
			Return ""
		End If

		iPathFileName = FileManager.GetNormalizedPathFileName(iPathFileName)
		fullPath = Path.GetFullPath(iPathFileName)
		splitPathArray = iPathFileName.Split(Path.DirectorySeparatorChar)
		If iPathFileName = fullPath Then
			'NOTE: Path.Combine does not put in the DirectorySeparatorChar, so combine directly.
			topFolderPath = splitPathArray(0) + Path.DirectorySeparatorChar + splitPathArray(1)
		Else
			topFolderPath = splitPathArray(0)
		End If

		Return topFolderPath
	End Function

	' Delete the path if all recursive subfolders are empty.
	' Example: "C:\folder\subfolder\temp" where temp contains "subtemp\subsubtemp".
	' Returns the top-most folder path that was deleted.
	Public Shared Function DeleteEmptySubpath(ByVal fullPath As String) As String
		Dim fullPathDeleted As String = ""

		If Not String.IsNullOrEmpty(fullPath) Then
			Try
				For Each aFullPath As String In Directory.EnumerateDirectories(fullPath)
					fullPathDeleted = FileManager.DeleteEmptySubpath(aFullPath)
				Next

				Dim entries As String() = Directory.GetFileSystemEntries(fullPath)
				If entries.Length = 0 Then
					Directory.Delete(fullPath)
					fullPathDeleted = fullPath
				End If
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If

		Return fullPathDeleted
	End Function

	'This is the code that works like GetTempFileName, but instead creates a folder:
	'public string GetTempDirectory() {
	'	string path = Path.GetRandomFileName();
	'	Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), path));
	'	return path;
	'}

	Private Shared Function GetPathAttribute(ByVal path As String) As Integer
		Dim di As New DirectoryInfo(path)
		If di.Exists Then
			Return FILE_ATTRIBUTE_DIRECTORY
		End If

		Dim fi As New FileInfo(path)
		If fi.Exists Then
			Return FILE_ATTRIBUTE_NORMAL
		End If

		Throw New FileNotFoundException()
	End Function

	Private Const FILE_ATTRIBUTE_DIRECTORY As Integer = &H10
	Private Const FILE_ATTRIBUTE_NORMAL As Integer = &H80

	<DllImport("shlwapi.dll", SetLastError:=True)> _
	Private Shared Function PathRelativePathTo(ByVal pszPath As StringBuilder, ByVal pszFrom As String, ByVal dwAttrFrom As Integer, ByVal pszTo As String, ByVal dwAttrTo As Integer) As Integer
	End Function

#End Region

#Region "Folder"

	Public Shared Sub CopyFolder(ByVal source As String, ByVal destination As String, ByVal overwrite As Boolean)
		' Create the destination folder if missing.
		If Not Directory.Exists(destination) Then
			Directory.CreateDirectory(destination)
		End If

		Dim dirInfo As New DirectoryInfo(source)

		' Copy all files.
		For Each fileInfo As FileInfo In dirInfo.GetFiles()
			fileInfo.CopyTo(Path.Combine(destination, fileInfo.Name), overwrite)
		Next

		' Recursively copy all sub-directories.
		For Each subDirectoryInfo As DirectoryInfo In dirInfo.GetDirectories()
			CopyFolder(subDirectoryInfo.FullName, Path.Combine(destination, subDirectoryInfo.Name), overwrite)
		Next
	End Sub

	Public Shared Function GetFolderSize(ByVal aFolder As String) As ULong
		Dim size As ULong
		Dim FolderInfo As DirectoryInfo = New IO.DirectoryInfo(aFolder)
		For Each File As FileInfo In FolderInfo.GetFiles
			size += CULng(File.Length)
		Next
		For Each SubFolderInfo As DirectoryInfo In FolderInfo.GetDirectories
			size += GetFolderSize(SubFolderInfo.FullName)
		Next
		Return size
	End Function

#End Region

#Region "XML Serialization"

	Public Shared Function ReadXml(ByVal theType As Type, ByVal rootElementName As String, ByVal fileName As String) As Object
		Dim x As New XmlSerializer(theType, New XmlRootAttribute(rootElementName))
		Return ReadXml(x, fileName)
	End Function

	Public Shared Function ReadXml(ByVal theType As Type, ByVal fileName As String) As Object
		Dim x As New XmlSerializer(theType)

		'Dim objStreamReader As New StreamReader(fileName)
		'Dim iObject As Object = Nothing
		'Try
		'	iObject = x.Deserialize(objStreamReader)
		'Catch
		'	'TODO: Rename the corrupted file.
		'	Throw
		'Finally
		'	objStreamReader.Close()
		'End Try
		'Return iObject
		'======
		Return ReadXml(x, fileName)
	End Function

	Public Shared Function ReadXml(ByVal x As XmlSerializer, ByVal fileName As String) As Object
		Dim objStreamReader As New StreamReader(fileName)
		Dim iObject As Object = Nothing
		Dim thereWasReadError As Boolean = False

		Try
			iObject = x.Deserialize(objStreamReader)
		Catch
			thereWasReadError = True
			Throw
		Finally
			objStreamReader.Close()

			If thereWasReadError Then
				Try
					Dim newFileName As String
					newFileName = Path.Combine(FileManager.GetPath(fileName), Path.GetFileNameWithoutExtension(fileName))
					newFileName += "[corrupted]"
					newFileName += Path.GetExtension(fileName)
					File.Move(fileName, newFileName)
				Catch ex As Exception
					'NOTE: Ignore what is likely a File.Move exception, because do not care if it fails.
				End Try
			End If
		End Try
		Return iObject
	End Function

	Public Shared Sub WriteXml(ByVal iObject As Object, ByVal rootElementName As String, ByVal fileName As String)
		Dim x As New XmlSerializer(iObject.GetType(), New XmlRootAttribute(rootElementName))
		WriteXml(iObject, x, fileName)
	End Sub

	Public Shared Sub WriteXml(ByVal iObject As Object, ByVal fileName As String)
		Dim x As New XmlSerializer(iObject.GetType())

		'Dim objStreamWriter As New StreamWriter(fileName)
		'x.Serialize(objStreamWriter, iObject)
		'objStreamWriter.Close()
		'======
		WriteXml(iObject, x, fileName)
	End Sub

	Public Shared Sub WriteXml(ByVal iObject As Object, ByVal x As XmlSerializer, ByVal fileName As String)
		'Dim objStreamWriter As New StreamWriter(fileName)
		'NOTE: Use Xml.XmlWriterSettings to preserve CRLF line endings used by multi-line textboxes.
		Dim settings As Xml.XmlWriterSettings = New Xml.XmlWriterSettings()
		settings.Indent = True
		settings.IndentChars = (ControlChars.Tab)
		settings.OmitXmlDeclaration = False
		settings.NewLineHandling = Xml.NewLineHandling.Entitize
		Dim objStreamWriter As Xml.XmlWriter = Xml.XmlWriter.Create(fileName, settings)
		x.Serialize(objStreamWriter, iObject)
		objStreamWriter.Close()
	End Sub

#End Region

#Region "Process"

	Public Shared Sub OpenWindowsExplorer(ByVal pathFileName As String)
		If File.Exists(pathFileName) Then
			Process.Start("explorer.exe", "/select,""" + pathFileName + """")
		ElseIf Directory.Exists(pathFileName) Then
			Process.Start("explorer.exe", "/e,""" + pathFileName + """")
		Else
			Dim shorterPathFileName As String
			shorterPathFileName = FileManager.GetPath(pathFileName)
			If Not String.IsNullOrWhiteSpace(shorterPathFileName) Then
				FileManager.OpenWindowsExplorer(shorterPathFileName)
			End If
		End If
	End Sub

#End Region

End Class
