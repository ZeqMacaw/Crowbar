Imports System.Runtime.InteropServices

Public Class SourceQuaternion48bitsSmallest3

	Public Sub New()
		Me.theQuaternion = New SourceQuaternion()
		Me.theQuaternionIsComputed = False
	End Sub

	Public Property AInput() As UShort
		Get
			Return Me.theAInput
		End Get
		Set(value As UShort)
			If Me.theAInput <> value Then
				Me.theAInput = value
				Me.theQuaternionIsComputed = False
			End If
		End Set
	End Property

	Public Property BInput() As UShort
		Get
			Return Me.theBInput
		End Get
		Set(value As UShort)
			If Me.theBInput <> value Then
				Me.theBInput = value
				Me.theQuaternionIsComputed = False
			End If
		End Set
	End Property

	Public Property CInput() As UShort
		Get
			Return Me.theCInput
		End Get
		Set(value As UShort)
			If Me.theCInput <> value Then
				Me.theCInput = value
				Me.theQuaternionIsComputed = False
			End If
		End Set
	End Property

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

	Private Sub ComputeQuaternion()
		If Not Me.theQuaternionIsComputed Then
			Dim missingComponentType As Integer
			Dim integerA As Integer
			Dim integerB As Integer
			Dim integerC As Integer
			Dim a As Double
			Dim b As Double
			Dim c As Double



			' Get component type from first 2 bits.
			missingComponentType = (Me.theAInput And &HC000) >> 14

			' Get A from last 13 bits of A and first 2 bits of B.
			integerA = (Me.theAInput And &H1FFF) Or ((Me.theBInput And &HC000) >> 14)

			' Get B from last 14 bits of B and first 1 bits of C.
			integerB = (Me.theBInput And &H3FFF) Or ((Me.theCInput And &H8000) >> 15)

			' Get C from last 15 bits of C.
			integerC = (Me.theCInput And &H7FFF) >> 1

			a = integerA / 1024.0F * (Maximum - Minimum) + Minimum
			b = integerB / 1024.0F * (Maximum - Minimum) + Minimum
			c = integerC / 1024.0F * (Maximum - Minimum) + Minimum
			'======
			'' Get A from first 15 bits of A.
			'integerA = (Me.theAInput And &HFFFE) >> 1

			'' Get B from last 1 bits of A and first 14 bits of B.
			'integerB = ((Me.theAInput And &H1) << 15) Or ((Me.theBInput And &H8000) >> 15)

			'' Get C from last 2 bits of B and first 13 bits of C.
			'integerC = (Me.theBInput And &H1FFF) Or ((Me.theCInput And &HC000) >> 14)

			'' Get component type from last 3 bits.
			'missingComponentType = (Me.theCInput And &HC000) >> 14

			'a = integerA / 1024.0F * (Maximum - Minimum) + Minimum
			'b = integerB / 1024.0F * (Maximum - Minimum) + Minimum
			'c = integerC / 1024.0F * (Maximum - Minimum) + Minimum
			'======
			'3-15-15-15 in axisFlag s2 s1 s0 (High to low)
			'so:
			'730b 5dFB c15F
			'011 100110000101101 011101111110111 100000101011111
			'3 4C2D 3BF7 415F
			'a = 1.41421 * (integerA - &H3FFF) / &H7FFF
			'b = 1.41421 * (integerB - &H3FFF) / &H7FFF
			'c = 1.41421 * (integerC - &H3FFF) / &H7FFF




			If missingComponentType = SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_X Then
				Me.theQuaternion.x = Me.GetMissingComponent(a, b, c)
				Me.theQuaternion.y = a
				Me.theQuaternion.z = b
				Me.theQuaternion.w = c
			ElseIf missingComponentType = SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_Y Then
				Me.theQuaternion.x = a
				Me.theQuaternion.y = Me.GetMissingComponent(a, b, c)
				Me.theQuaternion.z = b
				Me.theQuaternion.w = c
			ElseIf missingComponentType = SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_Z Then
				Me.theQuaternion.x = a
				Me.theQuaternion.y = b
				Me.theQuaternion.z = Me.GetMissingComponent(a, b, c)
				Me.theQuaternion.w = c
			ElseIf missingComponentType = SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_W Then
				Me.theQuaternion.x = a
				Me.theQuaternion.y = b
				Me.theQuaternion.z = c
				Me.theQuaternion.w = Me.GetMissingComponent(a, b, c)
			End If

			Me.theQuaternionIsComputed = True
		End If
	End Sub

	Private Function GetMissingComponent(ByVal a As Double, ByVal b As Double, c As Double) As Double
		Return Math.Sqrt(1 - a * a - b * b - c * c)
	End Function

	Private Const MISSING_COMPONENT_X As Integer = 0
	Private Const MISSING_COMPONENT_Y As Integer = 1
	Private Const MISSING_COMPONENT_Z As Integer = 2
	Private Const MISSING_COMPONENT_W As Integer = 3
	'private const float Minimum = -1.0f / 1.414214f; // note: 1.0f / sqrt(2)
	'private const float Maximum = +1.0f / 1.414214f;
	Private Const Minimum As Double = -1 / 1.414214
	Private Const Maximum As Double = 1 / 1.414214

	Private theAInput As UShort
	Private theBInput As UShort
	Private theCInput As UShort

	Private theQuaternion As SourceQuaternion

	Private theQuaternionIsComputed As Boolean

	'<StructLayout(LayoutKind.Explicit)> _
	'Public Structure IntegerAndSingleUnion
	'	<FieldOffset(0)> Public i As Int32
	'	<FieldOffset(0)> Public s As Single
	'End Structure

End Class
