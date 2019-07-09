Public Class SourceMdlVertAnim2531

	'Public Sub New()
	'	For x As Integer = 0 To 2
	'		Me.theDelta(x) = New SourceFloat16bits()
	'	Next
	'End Sub

	'FROM: SourceEngine2003_source HL2 Beta 2003\src_main\Public\studio.h
	'struct mstudiovertanim_t
	'{
	'	int					index;
	'	Vector				delta;
	'	Vector				ndelta;
	'};

	Public index As UShort

	'Public deltaX As Byte
	'Public deltaY As Byte
	'Public deltaZ As Byte
	'Public nDeltaX As Byte
	'Public nDeltaY As Byte
	'Public nDeltaZ As Byte
	'------
	'Public deltaX As SByte
	'Public deltaY As SByte
	'Public deltaZ As SByte
	'Public nDeltaX As SByte
	'Public nDeltaY As SByte
	'Public nDeltaZ As SByte
	'------
	Public deltaX As Short
	Public deltaY As Short
	Public deltaZ As Short


	'======

	'Public Property deltaByte(ByVal index As Integer) As Byte
	'	Get
	'		Return Me.theDelta(index).the8BitValue
	'	End Get
	'	Set(ByVal value As Byte)
	'		Me.theDelta(index).the8BitValue = value
	'	End Set
	'End Property

	'Public Property flDelta(ByVal index As Integer) As SourceFloat8bits
	'	Get
	'		Return Me.theDelta(index)
	'	End Get
	'	Set(ByVal value As SourceFloat8bits)
	'		Me.theDelta(index) = value
	'	End Set
	'End Property

	'Public Property nDeltaByte(ByVal index As Integer) As Byte
	'	Get
	'		Return Me.theNDelta(index).the8BitValue
	'	End Get
	'	Set(ByVal value As Byte)
	'		Me.theNDelta(index).the8BitValue = value
	'	End Set
	'End Property

	'Public Property flNDelta(ByVal index As Integer) As SourceFloat8bits
	'	Get
	'		Return Me.theNDelta(index)
	'	End Get
	'	Set(ByVal value As SourceFloat8bits)
	'		Me.theNDelta(index) = value
	'	End Set
	'End Property


	'Private theDelta(2) As SourceFloat8bits
	'Private theNDelta(2) As SourceFloat8bits

	'Public Sub New()
	'	For x As Integer = 0 To 2
	'		Me.theDelta(x) = New SourceFloat8bits()
	'	Next
	'	For x As Integer = 0 To 2
	'		Me.theNDelta(x) = New SourceFloat8bits()
	'	Next
	'End Sub

	'======

	'Public Property deltaUShort(ByVal index As Integer) As UShort
	'	Get
	'		Return Me.theDelta(index).the16BitValue
	'	End Get
	'	Set(ByVal value As UShort)
	'		Me.theDelta(index).the16BitValue = value
	'	End Set
	'End Property

	'Public Property flDelta(ByVal index As Integer) As SourceFloat16bits
	'	Get
	'		Return Me.theDelta(index)
	'	End Get
	'	Set(ByVal value As SourceFloat16bits)
	'		Me.theDelta(index) = value
	'	End Set
	'End Property


	'Private theDelta(2) As SourceFloat16bits

	'Public Sub New()
	'	For x As Integer = 0 To 2
	'		Me.theDelta(x) = New SourceFloat16bits()
	'	Next
	'End Sub

End Class
