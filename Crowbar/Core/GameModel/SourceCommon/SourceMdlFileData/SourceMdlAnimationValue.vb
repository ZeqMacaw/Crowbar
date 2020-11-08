Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Explicit)> _
Public Structure SourceMdlAnimationValue

	'FROM: SourceEngine2006_source\public\studio.h
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
