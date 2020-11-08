Imports System.IO
Imports System.Xml

Module VersionModule

	Public Sub ConvertSettingsFile(ByVal appSettingsPathFileName As String)
		Dim xmlDoc As XmlDocument
		xmlDoc = New XmlDocument()
		xmlDoc.Load(appSettingsPathFileName)

		Dim fileIsChanged As Boolean = False
		If ConvertVisibilityPrivateToHidden(xmlDoc) Then
			fileIsChanged = True
		End If

		If fileIsChanged Then
			Dim currentFolder As String
			Dim settingsFileName As String
			Dim backupSettingsFileName As String

			'NOTE: "ChangeDirectory" to settings folder to avoid problems with longer filenames.
			currentFolder = My.Computer.FileSystem.CurrentDirectory
			My.Computer.FileSystem.CurrentDirectory = Path.GetDirectoryName(appSettingsPathFileName)

			settingsFileName = Path.GetFileName(appSettingsPathFileName)
			backupSettingsFileName = Path.GetFileNameWithoutExtension(appSettingsPathFileName) + " [backup " + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + "]" + Path.GetExtension(appSettingsPathFileName)

			If Not File.Exists(backupSettingsFileName) Then
				My.Computer.FileSystem.CopyFile(settingsFileName, backupSettingsFileName, False)
			End If

			My.Computer.FileSystem.CurrentDirectory = currentFolder

			xmlDoc.Save(appSettingsPathFileName)
		End If
	End Sub

#Region "Private Methods"

	Private Function CopyElementToName(ByVal element As XmlElement, ByVal tagName As String) As XmlElement
		Dim newElement As XmlElement = element.OwnerDocument.CreateElement(tagName)

		For i As Integer = 0 To element.Attributes.Count - 1
			newElement.SetAttributeNode(CType(element.Attributes(i).CloneNode(True), XmlAttribute))
		Next

		For i As Integer = 0 To element.ChildNodes.Count - 1
			newElement.AppendChild(element.ChildNodes(i).CloneNode(True))
		Next

		Return newElement
	End Function

	Private Sub RenameNodes(ByVal xmlDoc As XmlDocument, ByVal oldName As String, ByVal newName As String)
		Dim xmlNodes As XmlNodeList
		Dim parentNode As XmlNode
		Dim newElement As XmlElement

		xmlNodes = xmlDoc.SelectNodes(oldName)
		For Each anXmlNode As XmlNode In xmlNodes
			parentNode = anXmlNode.ParentNode
			newElement = CopyElementToName(CType(anXmlNode, XmlElement), newName)
			parentNode.RemoveChild(anXmlNode)
			parentNode.AppendChild(newElement)
		Next
	End Sub

	Private Function ConvertVisibilityPrivateToHidden(ByVal xmlDoc As XmlDocument) As Boolean
		Dim fileIsChanged As Boolean = False

		Dim xmlNodes As XmlNodeList

		xmlNodes = xmlDoc.SelectNodes("//WorkshopItem/Visibility")
		For Each anXmlNode As XmlNode In xmlNodes
			If anXmlNode.InnerText = "Private" Then
				anXmlNode.InnerText = "Hidden"
				fileIsChanged = True
			End If
		Next

		Return fileIsChanged
	End Function

#End Region

End Module
