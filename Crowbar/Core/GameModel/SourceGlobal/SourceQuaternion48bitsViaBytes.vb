Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class SourceQuaternion48bitsViaBytes

	'FROM: Kerry at Valve via Splinks on 24-Apr-2017
	'//=========================================================
	'// 48 bit sorted Quaternion
	'//=========================================================
	' 
	'class Quaternion48S
	'{
	'public:
	'       // Construction/destruction:
	'       Quaternion48S(void);
	'       Quaternion48S(vec_t X, vec_t Y, vec_t Z);
	' 
	'       // assignment
	'       // Quaternion& operator=(const Quaternion48 &vOther);
	'       Quaternion48S& operator=(const Quaternion &vOther);
	'       operator Quaternion () const;
	'//private:
	'       // shift the quaternion so that the largest value is recreated by the sqrt()
	'       // abcd maps modulo into quaternion xyzw starting at "offset"
	'       // "offset" is split into two 1 bit fields so that the data packs into 6 bytes (3 shorts)
	'       unsigned short a:15;       // first of the 3 consecutive smallest quaternion elements
	'       unsigned short offsetH:1;  // high bit of "offset"
	'       unsigned short b:15;
	'       unsigned short offsetL:1;  // low bit of "offset"
	'       unsigned short c:15;
	'       unsigned short dneg:1;            // sign of the largest quaternion element
	'};
	' 
	'#define SCALE48S 23168.0f         // needs to fit 2*sqrt(0.5) into 15 bits.
	'#define SHIFT48S 16384                   // half of 2^15 bits.
	' 
	'inline Quaternion48S::operator Quaternion ()    const
	'{
	'       Quaternion tmp;
	' 
	'       COMPILE_TIME_ASSERT( sizeof( Quaternion48S ) == 6 );
	' 
	'       float *ptmp = &tmp.x;
	'       int ia = offsetL + offsetH * 2;
	'       int ib = ( ia + 1 ) % 4;
	'       int ic = ( ia + 2 ) % 4;
	'       int id = ( ia + 3 ) % 4;
	'       ptmp[ia] = ( (int)a - SHIFT48S ) * ( 1.0f / SCALE48S );
	'       ptmp[ib] = ( (int)b - SHIFT48S ) * ( 1.0f / SCALE48S );
	'       ptmp[ic] = ( (int)c - SHIFT48S ) * ( 1.0f / SCALE48S );
	'       ptmp[id] = sqrt( 1.0f - ptmp[ia] * ptmp[ia] - ptmp[ib] * ptmp[ib] - ptmp[ic] * ptmp[ic] );
	'       if (dneg)
	'              ptmp[id] = -ptmp[id];
	' 
	'       return tmp;
	'}
	' 
	'inline Quaternion48S& Quaternion48S::operator=(const Quaternion &vOther)  
	'{
	'       CHECK_VALID(vOther);
	' 
	'       const float *ptmp = &vOther.x;
	' 
	'       // find largest field, make sure that one is recreated by the sqrt to minimize error
	'       int i = 0;
	'       if ( fabs( ptmp[i] ) < fabs( ptmp[1] ) )
	'       {
	'              i = 1;
	'       }
	'       if ( fabs( ptmp[i] ) < fabs( ptmp[2] ) )
	'       {
	'              i = 2;
	'       }
	'       if ( fabs( ptmp[i] ) < fabs( ptmp[3] ) )
	'       {
	'              i = 3;
	'       }
	' 
	'       int offset = ( i + 1 ) % 4; // make "a" so that "d" is the largest element
	'       offsetL = offset & 1;
	'       offsetH = offset > 1;
	'       a = clamp( (int)(ptmp[ offset ] * SCALE48S) + SHIFT48S, 0, (int)(SCALE48S * 2) );
	'       b = clamp( (int)(ptmp[ ( offset + 1 ) % 4 ] * SCALE48S) + SHIFT48S, 0, (int)(SCALE48S * 2) );
	'       c = clamp( (int)(ptmp[ ( offset + 2 ) % 4 ] * SCALE48S) + SHIFT48S, 0, (int)(SCALE48S * 2) );
	'       dneg = ( ptmp[ ( offset + 3 ) % 4 ] < 0.0f );
	' 
	'       return *this;
	'}

	Public Sub New()
		Me.theQuaternion = New SourceQuaternion()
		Me.theQuaternionIsComputed = False
	End Sub

	Public theBytes(5) As Byte

	Public ReadOnly Property x() As Double
		Get
			Me.ComputeQuaternion()
			Return Me.theQuaternion.x
		End Get
	End Property

	Public ReadOnly Property y() As Double
		Get
			Me.ComputeQuaternion()
			Return Me.theQuaternion.y
		End Get
	End Property

	Public ReadOnly Property z() As Double
		Get
			Me.ComputeQuaternion()
			Return Me.theQuaternion.z
		End Get
	End Property

	Public ReadOnly Property w() As Double
		Get
			Me.ComputeQuaternion()
			Return Me.theQuaternion.w
		End Get
	End Property

	Public ReadOnly Property quaternion() As SourceQuaternion
		Get
			Me.ComputeQuaternion()
			Return Me.theQuaternion
		End Get
	End Property

	Private Sub ComputeQuaternion()
		If Not Me.theQuaternionIsComputed Then
			'1a-15-1b-15-1c-15 where 1a << 1 + 1b is index of missing component and 1c is sign of missing component 

			Dim tempInteger As UInt32
			Dim tempInteger2 As UInt32
			Dim uIntegerA As UInt32
			Dim uIntegerB As UInt32
			Dim uIntegerC As UInt32
			Dim missingComponentSign As Double
			Dim missingComponentIndex As UInt32
			tempInteger = CUInt(Me.theBytes(1) And &H7F)
			tempInteger2 = CUInt(Me.theBytes(0))
			uIntegerA = (tempInteger << 8) Or (tempInteger2)

			tempInteger = CUInt(Me.theBytes(3) And &H7F)
			tempInteger2 = CUInt(Me.theBytes(2))
			uIntegerB = (tempInteger << 8) Or (tempInteger2)

			tempInteger = CUInt(Me.theBytes(5) And &H7F)
			tempInteger2 = CUInt(Me.theBytes(4))
			uIntegerC = (tempInteger << 8) Or (tempInteger2)

			tempInteger = CUInt(Me.theBytes(1) And &H80)
			tempInteger2 = CUInt(Me.theBytes(3) And &H80)
			missingComponentIndex = (tempInteger >> 6) Or (tempInteger2 >> 7)
			If (Me.theBytes(5) And &H80) > 0 Then
				missingComponentSign = -1
			Else
				missingComponentSign = 1
			End If

			Dim a As Double
			Dim b As Double
			Dim c As Double

			a = (uIntegerA - 16384) / 23168
			b = (uIntegerB - 16384) / 23168
			c = (uIntegerC - 16384) / 23168

			If missingComponentIndex = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_X Then
				Me.theQuaternion.x = Me.GetMissingComponent(a, b, c, missingComponentSign)
				Me.theQuaternion.y = a
				Me.theQuaternion.z = b
				Me.theQuaternion.w = c
			ElseIf missingComponentIndex = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_Y Then
				Me.theQuaternion.x = c
				Me.theQuaternion.y = Me.GetMissingComponent(a, b, c, missingComponentSign)
				Me.theQuaternion.z = a
				Me.theQuaternion.w = b
			ElseIf missingComponentIndex = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_Z Then
				Me.theQuaternion.x = b
				Me.theQuaternion.y = c
				Me.theQuaternion.z = Me.GetMissingComponent(a, b, c, missingComponentSign)
				Me.theQuaternion.w = a
			ElseIf missingComponentIndex = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_W Then
				Me.theQuaternion.x = a
				Me.theQuaternion.y = b
				Me.theQuaternion.z = c
				Me.theQuaternion.w = Me.GetMissingComponent(a, b, c, missingComponentSign)
			End If
		End If
	End Sub

	Private Function GetMissingComponent(ByVal a As Double, ByVal b As Double, ByVal c As Double, ByVal missingComponentSign As Double) As Double
		Return Math.Sqrt(1 - a * a - b * b - c * c) * missingComponentSign
	End Function

	Public Const MISSING_COMPONENT_W As Integer = 0
	Public Const MISSING_COMPONENT_X As Integer = 1
	Public Const MISSING_COMPONENT_Y As Integer = 2
	Public Const MISSING_COMPONENT_Z As Integer = 3

	Private theQuaternion As SourceQuaternion

	Private theQuaternionIsComputed As Boolean

End Class

Public Enum EndianType
	<Description("LittleEndian")> Little
	<Description("BigEndian")> Big
End Enum
