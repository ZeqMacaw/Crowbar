Imports System.IO
Imports System.Text

' PURPOSE: Like BinaryReader, but faster overall reading via fewer calls to BaseStream.Read().
' DO NOT USE INHERITANCE because must hide BaseStream property to prevent derived objects from accessing BaseStream.Seek.
'    Use composition instead, i.e. use BinaryReader internally.
Public Class BufferedBinaryReader

#Region "Creation and Destruction"

	Public Sub New(ByVal stream As Stream)
		Me.theBinaryReader = New BinaryReader(stream)
		Me.Create(stream)
	End Sub

	Public Sub New(ByVal stream As Stream, ByVal encoding As Encoding)
		Me.theBinaryReader = New BinaryReader(stream, encoding)
		Me.Create(stream)
	End Sub

	Private Sub Create(ByVal stream As Stream)
		Me.theBufferSize = 4096
		Me.theBuffer = New Byte(Me.theBufferSize - 1) {}
		Me.thePositionInBuffer = Me.theBufferSize
	End Sub

	Public Sub Close()
		Me.theBinaryReader.Close()
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property Length() As Long
		Get
			Return Me.theBinaryReader.BaseStream.Length
		End Get
	End Property

	Public Property Position() As Long
		Get
			Return Me.theBinaryReader.BaseStream.Position
		End Get
		Set
			Me.theBinaryReader.BaseStream.Position = Value
			' Force a refresh of the buffer when called again.
			Me.thePositionInBuffer = Me.theBufferSize
		End Set
	End Property

#End Region

#Region "Methods"

	'Public Function PeekChar() As Integer
	'	Return Me.theBinaryReader.PeekChar()
	'End Function

	'Public Function ReadBoolean() As Boolean
	'	FillBuffer(1)
	'	Return Me.theBuffer(Me.thePositionInBuffer) <> 0
	'End Function

	Public Function ReadByte() As Byte
		Me.thePositionInBuffer += 1
		Return Me.theBinaryReader.ReadByte()
	End Function

	Public Function ReadBytes(ByVal count As Integer) As Byte()
		Me.thePositionInBuffer += count
		Return Me.theBinaryReader.ReadBytes(count)
	End Function

	Public Function ReadChar() As Char
		Dim position As Long = Me.theBinaryReader.BaseStream.Position
		Dim value As Char = Me.theBinaryReader.ReadChar()
		Me.thePositionInBuffer += CInt(Me.theBinaryReader.BaseStream.Position - position)
		Return value
	End Function

	Public Function ReadChars(ByVal count As Integer) As Char()
		Dim position As Long = Me.theBinaryReader.BaseStream.Position
		Dim value() As Char = Me.theBinaryReader.ReadChars(count)
		Me.thePositionInBuffer += CInt(Me.theBinaryReader.BaseStream.Position - position)
		Return value
	End Function

	Public Function ReadInt16() As Int16
		FillBuffer(2)
		Return CType(Me.theBuffer(Me.thePositionInBuffer - 2), Int16) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 1), Int16) << 8)
	End Function

	Public Function ReadUInt16() As UInt16
		FillBuffer(2)
		Return CType(Me.theBuffer(Me.thePositionInBuffer - 2), UInt16) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 1), UInt16) << 8)
	End Function

	Public Function ReadInt32() As Int32
		FillBuffer(4)
		Return CType(Me.theBuffer(Me.thePositionInBuffer - 4), Int32) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 3), Int32) << 8) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 2), Int32) << 16) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 1), Int32) << 24)
	End Function

	Public Function ReadUInt32() As UInt32
		FillBuffer(4)
		Return CType(Me.theBuffer(Me.thePositionInBuffer - 4), UInt32) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 3), UInt32) << 8) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 2), UInt32) << 16) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 1), UInt32) << 24)
	End Function

	Public Function ReadInt64() As Int64
		FillBuffer(8)
		Return CType(Me.theBuffer(Me.thePositionInBuffer - 8), Int64) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 7), Int64) << 8) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 6), Int64) << 16) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 5), Int64) << 24) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 4), Int64) << 32) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 3), Int64) << 40) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 2), Int64) << 48) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 1), Int64) << 56)
	End Function

	Public Function ReadUInt64() As UInt64
		FillBuffer(8)
		Return CType(Me.theBuffer(Me.thePositionInBuffer - 8), UInt64) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 7), UInt64) << 8) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 6), UInt64) << 16) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 5), UInt64) << 24) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 4), UInt64) << 32) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 3), UInt64) << 40) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 2), UInt64) << 48) Or (CType(Me.theBuffer(Me.thePositionInBuffer - 1), UInt64) << 56)
	End Function

	Public Function Seek(ByVal offset As Long, ByVal origin As SeekOrigin) As Long
		Dim value As Long = Me.theBinaryReader.BaseStream.Seek(offset, origin)
		' Force a refresh of the buffer when called again.
		Me.thePositionInBuffer = Me.theBufferSize
		Return value
	End Function

#End Region

#Region "Private Methods"

	Protected Sub FillBuffer(ByVal byteCountToRead As Integer)
		' If the position has passed the end of buffer, then set it to buffer end, to prevent problems in calculations.
		If Me.thePositionInBuffer > Me.theBufferSize Then
			Me.thePositionInBuffer = Me.theBufferSize
		End If

		' If reading the next bytes goes over the buffer size, then refill buffer.
		If Me.thePositionInBuffer + byteCountToRead > Me.theBufferSize Then
			Dim currentStreamPosition As Long = Me.theBinaryReader.BaseStream.Position
			Dim targetOffsetInBuffer As Integer = 0

			Me.thePositionInBuffer = 0

			Dim byteCountActuallyRead As Integer = 0
			Do
				byteCountActuallyRead = Me.theBinaryReader.BaseStream.Read(Me.theBuffer, targetOffsetInBuffer, Me.theBufferSize - targetOffsetInBuffer)
				If byteCountActuallyRead = 0 Then
					If Me.thePositionInBuffer + byteCountToRead <= Me.theBinaryReader.BaseStream.Position Then
						Exit Do
					Else
						Throw New EndOfStreamException("Read beyond EOF.")
					End If
				End If
				targetOffsetInBuffer += byteCountActuallyRead
			Loop While targetOffsetInBuffer < Me.theBufferSize
			Me.theBinaryReader.BaseStream.Position = currentStreamPosition
		End If
		Me.theBinaryReader.BaseStream.Position += byteCountToRead
		Me.thePositionInBuffer += byteCountToRead
	End Sub

#End Region

#Region "Data"

	Private theBinaryReader As BinaryReader
	Private theBufferSize As Integer
	Private theBuffer As Byte()
	Private thePositionInBuffer As Integer

#End Region

End Class
