Imports System.Runtime.InteropServices

Public Class SourceQuaternion48bitsAssumedW

	'FROM: SourceEngine2006_source\public\compressed_vector.h
	'//=========================================================
	'// 48 bit Quaternion
	'//=========================================================

	'	Class Quaternion48
	'{
	'public:
	'	// Construction/destruction:
	'	Quaternion48(void); 
	'	Quaternion48(vec_t X, vec_t Y, vec_t Z);

	'	// assignment
	'	// Quaternion& operator=(const Quaternion48 &vOther);
	'	Quaternion48& operator=(const Quaternion &vOther);
	'	operator Quaternion ();
	'private:
	'	unsigned short x:16;
	'	unsigned short y:16;
	'	unsigned short z:15;
	'	unsigned short wneg:1;
	'};



	'Public theBytes(5) As Byte
	Public theXInput As UShort
	Public theYInput As UShort
	Public theZWInput As UShort


	'FROM: SourceEngine2006_source\public\compressed_vector.h
	'inline Quaternion48::operator Quaternion ()	
	'{
	'	static Quaternion tmp;

	'	tmp.x = ((int)x - 32768) * (1 / 32768.0);
	'	tmp.y = ((int)y - 32768) * (1 / 32768.0);
	'	tmp.z = ((int)z - 16384) * (1 / 16384.0);
	'	tmp.w = sqrt( 1 - tmp.x * tmp.x - tmp.y * tmp.y - tmp.z * tmp.z );
	'		If (wneg) Then
	'		tmp.w = -tmp.w;
	'	return tmp; 
	'}

	Public ReadOnly Property x() As Double
		Get
			Dim result As Double

			'result = (Me.theXInput - 32768) * (1 / 32768)
			result = (CInt(Me.theXInput) - 32768) * (1 / 32768)
			Return result
		End Get
	End Property

	Public ReadOnly Property y() As Double
		Get
			Dim result As Double

			'result = (Me.theYInput - 32768) * (1 / 32768)
			result = (CInt(Me.theYInput) - 32768) * (1 / 32768)
			Return result
		End Get
	End Property

	Public ReadOnly Property z() As Double
		Get
			Dim result As Double

			result = (CInt(Me.theZWInput) - 32768) * (1 / 32768)
			Return result
		End Get
	End Property

	Public ReadOnly Property w() As Double
		Get
			Return Math.Sqrt(1 - Me.x * Me.x - Me.y * Me.y - Me.z * Me.z)
		End Get
	End Property

	'Public ReadOnly Property wneg() As Double
	'	Get
	'		If (Me.theZWInput And &H8000) > 0 Then
	'			Return -1
	'		Else
	'			Return 1
	'		End If
	'	End Get
	'End Property

	Public ReadOnly Property quaternion() As SourceQuaternion
		Get
			Dim quat As New SourceQuaternion()
			quat.x = Me.x
			quat.y = Me.y
			quat.z = Me.z
			quat.w = Me.w
			Return quat
		End Get
	End Property

	'<StructLayout(LayoutKind.Explicit)> _
	'Public Structure IntegerAndSingleUnion
	'	<FieldOffset(0)> Public i As Integer
	'	<FieldOffset(0)> Public s As Single
	'End Structure

End Class
