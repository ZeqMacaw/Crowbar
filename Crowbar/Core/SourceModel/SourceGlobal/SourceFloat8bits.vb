Imports System.Runtime.InteropServices

'TODO: 
Public Class SourceFloat8bits

	Public ReadOnly Property TheFloatValue() As Double
		Get
			Dim result As Double

			Dim sign As Integer
			Dim floatSign As Integer
			sign = Me.GetSign(Me.the8BitValue)
			If sign = 1 Then
				floatSign = -1
			Else
				floatSign = 1
			End If

			If Me.IsInfinity(Me.the8BitValue) Then
				Return maxfloat8bits * floatSign
			End If

			If Me.IsNaN(Me.the8BitValue) Then
				Return 0
			End If

			Dim mantissa As Integer
			Dim biased_exponent As Integer
			mantissa = Me.GetMantissa(Me.the8BitValue)
			biased_exponent = Me.GetBiasedExponent(Me.the8BitValue)
			If biased_exponent = 0 AndAlso mantissa <> 0 Then
				Dim floatMantissa As Single
				floatMantissa = mantissa / 8.0F
				result = floatSign * floatMantissa * half_denorm
			Else
				result = Me.GetSingle(Me.the8BitValue)

				' For debugging the conversion.
				'result = CType(anInteger32, Single)
			End If

			Return result
		End Get
	End Property

	'			unsigned short mantissa : 3;
	'			unsigned short biased_exponent : 4;
	'			unsigned short sign : 1;

	Private Function GetMantissa(ByVal value As Byte) As Integer
		Return (value And &H7)
	End Function

	Private Function GetBiasedExponent(ByVal value As Byte) As Integer
		Return (value And &H78) >> 3
	End Function

	Private Function GetSign(ByVal value As Byte) As Integer
		Return (value And &H80) >> 7
	End Function

	Private Function GetSingle(ByVal value As Byte) As Single
		'FROM:
		'			unsigned short mantissa : 3;
		'			unsigned short biased_exponent : 4;
		'			unsigned short sign : 1;
		'TO:
		'			unsigned int mantissa : 23;
		'			unsigned int biased_exponent : 8;
		'			unsigned int sign : 1;
		Dim bitsResult As IntegerAndSingleUnion
		Dim mantissa As Integer
		Dim biased_exponent As Integer
		Dim sign As Integer
		Dim resultMantissa As Integer
		Dim resultBiasedExponent As Integer
		Dim resultSign As Integer
		bitsResult.i = 0

		mantissa = Me.GetMantissa(Me.the8BitValue)
		biased_exponent = Me.GetBiasedExponent(Me.the8BitValue)
		sign = Me.GetSign(Me.the8BitValue)

		resultMantissa = mantissa << (23 - 3)
		If biased_exponent = 0 Then
			resultBiasedExponent = 0
		Else
			resultBiasedExponent = (biased_exponent - float8bias + float32bias) << 23
		End If
		resultSign = sign << 31

		'' For debugging.
		''------
		'' TEST PASSED:
		''If (resultMantissa Or &H7FFFFF) <> &H7FFFFF Then
		''	Dim i As Integer = 42
		''End If
		''------
		'' TEST PASSED:
		''If (resultBiasedExponent Or &H7F800000) <> &H7F800000 Then
		''	Dim i As Integer = 42
		''End If
		''------
		'' TEST PASSED:
		''If resultSign <> &H80000000 AndAlso resultSign <> 0 Then
		''	Dim i As Integer = 42
		''End If

		bitsResult.i = resultSign Or resultBiasedExponent Or resultMantissa

		Return bitsResult.s
	End Function

	Private Function IsInfinity(ByVal value As Byte) As Boolean
		Dim mantissa As Integer
		Dim biased_exponent As Integer

		mantissa = Me.GetMantissa(value)
		biased_exponent = Me.GetBiasedExponent(value)
		Return ((biased_exponent = 15) And (mantissa = 0))
	End Function

	Private Function IsNaN(ByVal value As Byte) As Boolean
		Dim mantissa As Integer
		Dim biased_exponent As Integer

		mantissa = Me.GetMantissa(value)
		biased_exponent = Me.GetBiasedExponent(value)
		Return ((biased_exponent = 15) And (mantissa <> 0))
	End Function

	Const float32bias As Integer = 127
	Const float8bias As Integer = 7
	Const maxfloat8bits As Single = 122880.0F
	Const half_denorm As Single = (1.0F / 8.0F)

	Public the8BitValue As Byte

	<StructLayout(LayoutKind.Explicit)> _
	Public Structure IntegerAndSingleUnion
		<FieldOffset(0)> Public i As Integer
		<FieldOffset(0)> Public s As Single
	End Structure

End Class
