﻿Imports System.ComponentModel
Imports System.IO
Imports System.Text

Public Class ApkFile
	Inherits SourcePackageFile

#Region "Creation and Destruction"

	Public Sub New(ByVal packageFileReader As BufferedBinaryReader, ByVal apkFileData As ApkFileData)
		Me.theInputFileReader = packageFileReader
		Me.theApkFileData = apkFileData
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property FileData() As ApkFileData
		Get
			Return Me.theApkFileData
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overrides Sub ReadHeader()
		Me.theInputFileReader.Seek(0, SeekOrigin.Begin)

		Me.theApkFileData.id = Me.theInputFileReader.ReadUInt32()
		Me.theApkFileData.offsetOfFiles = Me.theInputFileReader.ReadUInt32()
		Me.theApkFileData.fileCount = Me.theInputFileReader.ReadUInt32()
		Me.theApkFileData.offsetOfDirectory = Me.theInputFileReader.ReadUInt32()
	End Sub

	Public Overrides Sub ReadEntries()
		If Not Me.theApkFileData.IsSourcePackage Then
			Exit Sub
		End If

		Try
			Dim entry As ApkDirectoryEntry
			'Dim entryDataOutputText As New StringBuilder
			Dim pathFileName As String

			Me.theInputFileReader.Seek(Me.theApkFileData.offsetOfDirectory, SeekOrigin.Begin)

			For directoryEntryIndex As UInt32 = 0 To CUInt(Me.theApkFileData.fileCount - 1)
				entry = New ApkDirectoryEntry()
				'entryDataOutputText.Clear()

				entry.pathFileNameSize = Me.theInputFileReader.ReadUInt32()
				pathFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)
				entry.DisplayPathFileName = pathFileName.Replace("\"c, "/"c)
				'entry.offsetOfFile = Me.theInputFileReader.ReadUInt32()
				entry.DataOffset = Me.theInputFileReader.ReadUInt32()
				entry.DataSize = Me.theInputFileReader.ReadUInt32()
				entry.offsetOfNextDirectoryEntry = Me.theInputFileReader.ReadUInt32()
				Me.theInputFileReader.ReadUInt32()
				'entry.Crc = 0
				'entry.theDataSize = entry.fileSize
				Me.theApkFileData.theEntries.Add(entry)

				'entryDataOutputText.Append(entry.thePathFileName)
				'entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"))
				'entryDataOutputText.Append(" metadatasz=0")
				'entryDataOutputText.Append(" fnumber=0")
				'entryDataOutputText.Append(" ofs=0x" + entry.offsetOfFile.ToString("X8"))
				'entryDataOutputText.Append(" sz=" + entry.fileSize.ToString("G0"))
				'Me.theApkFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString())

				'NotifyPackEntryRead(entry, entryDataOutputText.ToString())
				'NotifyPackEntryRead(entry)
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Public Overrides Sub UnpackEntryDataToFile(ByVal iEntry As SourcePackageDirectoryEntry, ByVal outputPathFileName As String)
		Dim entry As ApkDirectoryEntry
		entry = CType(iEntry, ApkDirectoryEntry)

		Dim outputFileStream As FileStream = Nothing
		Try
			outputFileStream = New FileStream(outputPathFileName, FileMode.Create)
			If outputFileStream IsNot Nothing Then
				Try
					Me.theOutputFileWriter = New BinaryWriter(outputFileStream, System.Text.Encoding.ASCII)

					'Me.theInputFileReader.BaseStream.Seek(entry.offsetOfFile, SeekOrigin.Begin)
					Me.theInputFileReader.Seek(entry.DataOffset, SeekOrigin.Begin)
					Dim bytes() As Byte
					bytes = Me.theInputFileReader.ReadBytes(CInt(entry.DataSize))
					Me.theOutputFileWriter.Write(bytes)
				Catch ex As Exception
					Dim debug As Integer = 4242
				Finally
					If Me.theOutputFileWriter IsNot Nothing Then
						Me.theOutputFileWriter.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If outputFileStream IsNot Nothing Then
				outputFileStream.Close()
			End If
		End Try
	End Sub

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	Private theInputFileReader As BufferedBinaryReader
	Private theOutputFileWriter As BinaryWriter
	Private theApkFileData As ApkFileData

#End Region

End Class
