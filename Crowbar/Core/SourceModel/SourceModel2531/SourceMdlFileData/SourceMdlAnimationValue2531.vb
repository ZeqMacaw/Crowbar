Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Explicit)> _
Public Structure SourceMdlAnimationValue2531

	'FROM: SourceEngine2003_source HL2 Beta 2003\src_main\Public\studio.h
	'// animation frames
	'union mstudioanimvalue_t
	'{
	'	struct 
	'	{
	'		byte	valid;
	'		byte	total;
	'	} num;
	'	short		value;
	'};


	<FieldOffset(0)> Public valid As Byte
	<FieldOffset(1)> Public total As Byte

	<FieldOffset(0)> Public value As Short

End Structure
