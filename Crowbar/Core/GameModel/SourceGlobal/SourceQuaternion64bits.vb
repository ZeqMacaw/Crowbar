Imports System.Runtime.InteropServices

Public Class SourceQuaternion64bits

	'FROM: https://bitbucket.org/VoiDeD/steamre/src/9214bf3b662b/Resources/NetHook/mathlib/compressed_vector.h
	'//=========================================================
	'// 64 bit Quaternion
	'//=========================================================

	'	Class Quaternion64
	'{
	'public:
	'        // Construction/destruction:
	'        Quaternion64(void); 
	'        Quaternion64(vec_t X, vec_t Y, vec_t Z);

	'        // assignment
	'        // Quaternion& operator=(const Quaternion64 &vOther);
	'        Quaternion64& operator=(const Quaternion &vOther);
	'        operator Quaternion ();
	'private:
	'        uint64 x:21;
	'        uint64 y:21;
	'        uint64 z:21;
	'        uint64 wneg:1;
	'};

	'inline Quaternion64::operator Quaternion ()     
	'{
	'        Quaternion tmp;
	'
	'        // shift to -1048576, + 1048575, then round down slightly to -1.0 < x < 1.0
	'        tmp.x = ((int)x - 1048576) * (1 / 1048576.5f);
	'        tmp.y = ((int)y - 1048576) * (1 / 1048576.5f);
	'        tmp.z = ((int)z - 1048576) * (1 / 1048576.5f);
	'        tmp.w = sqrt( 1 - tmp.x * tmp.x - tmp.y * tmp.y - tmp.z * tmp.z );
	'		If (wneg) Then
	'                tmp.w = -tmp.w;
	'        return tmp; 
	'}

	'inline Quaternion64& Quaternion64::operator=(const Quaternion &vOther)  
	'{
	'        CHECK_VALID(vOther);
	'
	'        x = clamp( (int)(vOther.x * 1048576) + 1048576, 0, 2097151 );
	'        y = clamp( (int)(vOther.y * 1048576) + 1048576, 0, 2097151 );
	'        z = clamp( (int)(vOther.z * 1048576) + 1048576, 0, 2097151 );
	'        wneg = (vOther.w < 0);
	'        return *this; 
	'}



	'FROM: www.j3d.org/matrix_faq/matrfaq_latest.html
	'Q57. How do I convert a quaternion to a rotation axis and angle?
	'----------------------------------------------------------------
	'  A quaternion can be converted back to a rotation axis and angle
	'  using the following algorithm:

	'    quaternion_normalise( |X,Y,Z,W| );
	'    cos_a = W;
	'    angle = acos( cos_a ) * 2;
	'    sin_a = sqrt( 1.0 - cos_a * cos_a );
	'    if ( fabs( sin_a ) < 0.0005 ) sin_a = 1;
	'    axis -> x = X / sin_a;
	'    axis -> y = Y / sin_a;
	'    axis -> z = Z / sin_a;



	Public theBytes(7) As Byte

	Public ReadOnly Property x() As Double
		Get
			Dim byte0 As Integer
			Dim byte1 As Integer
			Dim byte2 As Integer
			Dim bitsResult As IntegerAndSingleUnion
			Dim result As Double

			byte0 = (CInt(Me.theBytes(0)) And &HFF)
			byte1 = (CInt(Me.theBytes(1)) And &HFF) << 8
			byte2 = (CInt(Me.theBytes(2)) And &H1F) << 16
			'------
			'byte0 = (CInt(Me.theBytes(7)) And &HFF)
			'byte1 = (CInt(Me.theBytes(6)) And &HFF) << 8
			'byte2 = (CInt(Me.theBytes(5)) And &H1F) << 16

			bitsResult.i = byte2 Or byte1 Or byte0
			'Return bitsResult.s
			'Return CUInt(((Me.theBytes(2) And &H1F) << 16) And ((Me.theBytes(1) And &HFF) << 8) And (Me.theBytes(0) And &HFF))
			result = (bitsResult.i - 1048576) * (1 / 1048576.5)
			Return result
		End Get
	End Property

	Public ReadOnly Property y() As Double
		Get
			Dim byte2 As Integer
			Dim byte3 As Integer
			Dim byte4 As Integer
			Dim byte5 As Integer
			Dim bitsResult As IntegerAndSingleUnion
			Dim result As Double

			byte2 = (CInt(Me.theBytes(2)) And &HE0) >> 5
			byte3 = (CInt(Me.theBytes(3)) And &HFF) << 3
			byte4 = (CInt(Me.theBytes(4)) And &HFF) << 11
			byte5 = (CInt(Me.theBytes(5)) And &H3) << 19
			'------
			'byte2 = (CInt(Me.theBytes(5)) And &HE0) >> 5
			'byte3 = (CInt(Me.theBytes(4)) And &HFF) << 3
			'byte4 = (CInt(Me.theBytes(3)) And &HFF) << 11
			'byte5 = (CInt(Me.theBytes(2)) And &H3) << 19

			bitsResult.i = byte5 Or byte4 Or byte3 Or byte2
			'Return bitsResult.s
			'Return CUInt(((Me.theBytes(5) And &H3) << 19) And ((Me.theBytes(4) And &HFF) << 11) And ((Me.theBytes(3) And &HFF) << 3) And (Me.theBytes(2) And &HE0) >> 5)
			result = (bitsResult.i - 1048576) * (1 / 1048576.5)
			Return result
		End Get
	End Property

	Public ReadOnly Property z() As Double
		Get
			Dim byte5 As Integer
			Dim byte6 As Integer
			Dim byte7 As Integer
			Dim bitsResult As IntegerAndSingleUnion
			Dim result As Double

			byte5 = (CInt(Me.theBytes(5)) And &HFC) >> 2
			byte6 = (CInt(Me.theBytes(6)) And &HFF) << 6
			byte7 = (CInt(Me.theBytes(7)) And &H7F) << 14
			'------
			'byte5 = (CInt(Me.theBytes(2)) And &HFC) >> 2
			'byte6 = (CInt(Me.theBytes(1)) And &HFF) << 6
			'byte7 = (CInt(Me.theBytes(0)) And &H7F) << 14

			bitsResult.i = byte7 Or byte6 Or byte5
			'Return bitsResult.s
			'Return CUInt(((Me.theBytes(7) And &H7F) << 14) And ((Me.theBytes(6) And &HFF) << 6) And ((Me.theBytes(5) And &HFC) >> 2))
			result = (bitsResult.i - 1048576) * (1 / 1048576.5)
			Return result
		End Get
	End Property

	Public ReadOnly Property w() As Double
		Get
			Dim result As Double

			'result = Me.wneg
			result = Math.Sqrt(1 - Me.x * Me.x - Me.y * Me.y - Me.z * Me.z) * Me.wneg
			Return result
		End Get
	End Property

	Public ReadOnly Property wneg() As Double
		Get
			If (Me.theBytes(7) And &H80) > 0 Then
				Return -1
			Else
				Return 1
			End If
		End Get
	End Property

	Public ReadOnly Property xRadians() As Double
		Get
			'    cos_a = W;
			'    angle = acos( cos_a ) * 2;
			'    sin_a = sqrt( 1.0 - cos_a * cos_a );
			'    if ( fabs( sin_a ) < 0.0005 ) sin_a = 1;
			'    axis -> x = X / sin_a;
			'    axis -> y = Y / sin_a;
			'    axis -> z = Z / sin_a;
			Dim cos_a As Double
			Dim angle As Double
			Dim sin_a As Double

			cos_a = Me.w
			angle = Math.Acos(cos_a) * 2
			sin_a = Math.Sqrt(1.0 - cos_a * cos_a)
			If Math.Abs(sin_a) < 0.000005 Then
				sin_a = 1
			End If

			Return Me.x / sin_a
		End Get
	End Property

	Public ReadOnly Property yRadians() As Double
		Get
			Dim cos_a As Double
			Dim angle As Double
			Dim sin_a As Double

			cos_a = Me.w
			angle = Math.Acos(cos_a) * 2
			sin_a = Math.Sqrt(1.0 - cos_a * cos_a)
			If Math.Abs(sin_a) < 0.000005 Then
				sin_a = 1
			End If

			Return Me.y / sin_a
		End Get
	End Property

	Public ReadOnly Property zRadians() As Double
		Get
			Dim cos_a As Double
			Dim angle As Double
			Dim sin_a As Double

			cos_a = Me.w
			angle = Math.Acos(cos_a) * 2
			sin_a = Math.Sqrt(1.0 - cos_a * cos_a)
			If Math.Abs(sin_a) < 0.000005 Then
				sin_a = 1
			End If

			Return Me.z / sin_a
		End Get
	End Property

	<StructLayout(LayoutKind.Explicit)> _
	Public Structure IntegerAndSingleUnion
		<FieldOffset(0)> Public i As Integer
		<FieldOffset(0)> Public s As Single
	End Structure

End Class
