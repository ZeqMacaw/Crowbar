Imports System.Runtime.InteropServices

Public Class SourceFloat16bits

	'FROM: SourceEngine2006_source\public\compressed_vector.h

	'//=========================================================
	'// 16 bit float
	'//=========================================================


	'const int float32bias = 127;
	'const int float16bias = 15;

	'const float maxfloat16bits = 65504.0f;

	'Class float16
	'{
	'public:
	'	//float16() {}
	'	//float16( float f ) { m_storage.rawWord = ConvertFloatTo16bits(f); }

	'	void Init() { m_storage.rawWord = 0; }
	'//	float16& operator=(const float16 &other) { m_storage.rawWord = other.m_storage.rawWord; return *this; }
	'//	float16& operator=(const float &other) { m_storage.rawWord = ConvertFloatTo16bits(other); return *this; }
	'//	operator unsigned short () { return m_storage.rawWord; }
	'//	operator float () { return Convert16bitFloatTo32bits( m_storage.rawWord ); }
	'	unsigned short GetBits() const 
	'	{ 
	'		return m_storage.rawWord; 
	'	}
	'	float GetFloat() const 
	'	{ 
	'		return Convert16bitFloatTo32bits( m_storage.rawWord ); 
	'	}
	'	void SetFloat( float in ) 
	'	{ 
	'		m_storage.rawWord = ConvertFloatTo16bits( in ); 
	'	}

	'	bool IsInfinity() const
	'	{
	'		return m_storage.bits.biased_exponent == 31 && m_storage.bits.mantissa == 0;
	'	}
	'	bool IsNaN() const
	'	{
	'		return m_storage.bits.biased_exponent == 31 && m_storage.bits.mantissa != 0;
	'	}

	'	bool operator==(const float16 other) const { return m_storage.rawWord == other.m_storage.rawWord; }
	'	bool operator!=(const float16 other) const { return m_storage.rawWord != other.m_storage.rawWord; }

	'//	bool operator< (const float other) const	   { return GetFloat() < other; }
	'//	bool operator> (const float other) const	   { return GetFloat() > other; }

	'protected:
	'	union float32bits
	'	{
	'		float rawFloat;
	'		struct 
	'		{
	'			unsigned int mantissa : 23;
	'			unsigned int biased_exponent : 8;
	'			unsigned int sign : 1;
	'		} bits;
	'	};

	'	union float16bits
	'	{
	'		unsigned short rawWord;
	'		struct
	'		{
	'			unsigned short mantissa : 10;
	'			unsigned short biased_exponent : 5;
	'			unsigned short sign : 1;
	'		} bits;
	'	};

	'	static bool IsNaN( float16bits in )
	'	{
	'		return in.bits.biased_exponent == 31 && in.bits.mantissa != 0;
	'	}
	'	static bool IsInfinity( float16bits in )
	'	{
	'		return in.bits.biased_exponent == 31 && in.bits.mantissa == 0;
	'	}

	'	// 0x0001 - 0x03ff
	'	static unsigned short ConvertFloatTo16bits( float input )
	'	{
	'		if ( input > maxfloat16bits )
	'			input = maxfloat16bits;
	'		else if ( input < -maxfloat16bits )
	'			input = -maxfloat16bits;

	'		float16bits output;
	'		float32bits inFloat;

	'		inFloat.rawFloat = input;

	'		output.bits.sign = inFloat.bits.sign;

	'		if ( (inFloat.bits.biased_exponent==0) && (inFloat.bits.mantissa==0) ) 
	'		{ 
	'			// zero
	'			output.bits.mantissa = 0;
	'			output.bits.biased_exponent = 0;
	'		}
	'		else if ( (inFloat.bits.biased_exponent==0) && (inFloat.bits.mantissa!=0) ) 
	'		{  
	'			// denorm -- denorm float maps to 0 half
	'			output.bits.mantissa = 0;
	'			output.bits.biased_exponent = 0;
	'		}
	'		else if ( (inFloat.bits.biased_exponent==0xff) && (inFloat.bits.mantissa==0) ) 
	'		{ 
	'#If 0 Then
	'			// infinity
	'			output.bits.mantissa = 0;
	'			output.bits.biased_exponent = 31;
	'#Else
	'			// infinity maps to maxfloat
	'			output.bits.mantissa = 0x3ff;
	'			output.bits.biased_exponent = 0x1e;
	'#End If
	'		}
	'		else if ( (inFloat.bits.biased_exponent==0xff) && (inFloat.bits.mantissa!=0) ) 
	'		{ 
	'#If 0 Then
	'			// NaN
	'			output.bits.mantissa = 1;
	'			output.bits.biased_exponent = 31;
	'#Else
	'			// NaN maps to zero
	'			output.bits.mantissa = 0;
	'			output.bits.biased_exponent = 0;
	'#End If
	'		}
	'		else 
	'		{ 
	'			// regular number
	'			int new_exp = inFloat.bits.biased_exponent-127;

	'			if (new_exp<-24) 
	'			{ 
	'				// this maps to 0
	'				output.bits.mantissa = 0;
	'				output.bits.biased_exponent = 0;
	'			}

	'			if (new_exp<-14) 
	'			{
	'				// this maps to a denorm
	'				output.bits.biased_exponent = 0;
	'				unsigned int exp_val = ( unsigned int )( -14 - ( inFloat.bits.biased_exponent - float32bias ) );
	'				if( exp_val > 0 && exp_val < 11 )
	'				{
	'					output.bits.mantissa = ( 1 << ( 10 - exp_val ) ) + ( inFloat.bits.mantissa >> ( 13 + exp_val ) );
	'				}
	'			}
	'			else if (new_exp>15) 
	'			{ 
	'#If 0 Then
	'				// map this value to infinity
	'				output.bits.mantissa = 0;
	'				output.bits.biased_exponent = 31;
	'#Else
	'				// to big. . . maps to maxfloat
	'				output.bits.mantissa = 0x3ff;
	'				output.bits.biased_exponent = 0x1e;
	'#End If
	'			}
	'			else 
	'			{
	'				output.bits.biased_exponent = new_exp+15;
	'				output.bits.mantissa = (inFloat.bits.mantissa >> 13);
	'			}
	'		}
	'		return output.rawWord;
	'	}

	'	static float Convert16bitFloatTo32bits( unsigned short input )
	'	{
	'		float32bits output;
	'		float16bits inFloat;
	'		inFloat.rawWord = input;

	'		if( IsInfinity( inFloat ) )
	'		{
	'			return maxfloat16bits * ( ( inFloat.bits.sign == 1 ) ? -1.0f : 1.0f );
	'		}
	'		if( IsNaN( inFloat ) )
	'		{
	'			return 0.0;
	'		}
	'		if( inFloat.bits.biased_exponent == 0 && inFloat.bits.mantissa != 0 )
	'		{
	'			// denorm
	'			const float half_denorm = (1.0f/16384.0f); // 2^-14
	'			float mantissa = ((float)(inFloat.bits.mantissa)) / 1024.0f;
	'			float sgn = (inFloat.bits.sign)? -1.0f :1.0f;
	'			output.rawFloat = sgn*mantissa*half_denorm;
	'		}
	'		else
	'		{
	'			// regular number
	'			output.bits.mantissa = inFloat.bits.mantissa << (23-10);
	'			output.bits.biased_exponent = (inFloat.bits.biased_exponent - float16bias + float32bias) * (inFloat.bits.biased_exponent != 0);
	'			output.bits.sign = inFloat.bits.sign;
	'		}

	'		return output.rawFloat;
	'	}


	'	float16bits m_storage;
	'};

	Public ReadOnly Property TheFloatValue() As Double
		Get
			'		float32bits output;
			'Dim result As Single
			Dim result As Double
			'		float16bits inFloat;
			'		inFloat.rawWord = input;

			Dim sign As Integer
			Dim floatSign As Integer
			sign = Me.GetSign(Me.the16BitValue)
			If sign = 1 Then
				floatSign = -1
			Else
				floatSign = 1
			End If

			'		if( IsInfinity( inFloat ) )
			'		{
			'			return maxfloat16bits * ( ( inFloat.bits.sign == 1 ) ? -1.0f : 1.0f );
			'		}
			If Me.IsInfinity(Me.the16BitValue) Then
				Return maxfloat16bits * floatSign
			End If

			'		if( IsNaN( inFloat ) )
			'		{
			'			return 0.0;
			'		}
			If Me.IsNaN(Me.the16BitValue) Then
				Return 0
			End If

			'		if( inFloat.bits.biased_exponent == 0 && inFloat.bits.mantissa != 0 )
			'		{
			'			// denorm
			'			const float half_denorm = (1.0f/16384.0f); // 2^-14
			'			float mantissa = ((float)(inFloat.bits.mantissa)) / 1024.0f;
			'			float sgn = (inFloat.bits.sign)? -1.0f :1.0f;
			'			output.rawFloat = sgn*mantissa*half_denorm;
			'		}
			'		else
			'		{
			'			// regular number
			'			output.bits.mantissa = inFloat.bits.mantissa << (23-10);
			'			output.bits.biased_exponent = (inFloat.bits.biased_exponent - float16bias + float32bias) * (inFloat.bits.biased_exponent != 0);
			'			output.bits.sign = inFloat.bits.sign;
			'		}
			Dim mantissa As Integer
			Dim biased_exponent As Integer
			'Dim anInteger32 As Integer
			'Dim anInteger32Bytes() As Byte
			mantissa = Me.GetMantissa(Me.the16BitValue)
			biased_exponent = Me.GetBiasedExponent(Me.the16BitValue)
			If biased_exponent = 0 AndAlso mantissa <> 0 Then
				Dim floatMantissa As Single
				floatMantissa = mantissa / 1024.0F
				result = floatSign * floatMantissa * half_denorm
			Else
				'anInteger32 = Me.Get32BitFloat(Me.the16BitValue)
				'anInteger32Bytes = BitConverter.GetBytes(anInteger32)
				''Array.Reverse(anInteger32Bytes)
				'result = BitConverter.ToSingle(anInteger32Bytes, 0)
				'------
				result = Me.Getsingle(Me.the16BitValue)

				' For debugging the conversion.
				'result = CType(anInteger32, Single)
			End If

			'		return output.rawFloat;
			Return result
		End Get
	End Property

	'			unsigned short mantissa : 10;
	'			unsigned short biased_exponent : 5;
	'			unsigned short sign : 1;

	Private Function GetMantissa(ByVal value As UShort) As Integer
		Return (value And &H3FF)
	End Function

	Private Function GetBiasedExponent(ByVal value As UShort) As Integer
		Return (value And &H7C00) >> 10
	End Function

	Private Function GetSign(ByVal value As UShort) As Integer
		Return (value And &H8000) >> 15
	End Function

	'Private Function Get32BitFloat(ByVal value As UShort) As Integer
	'	'FROM:
	'	'			unsigned short mantissa : 10;
	'	'			unsigned short biased_exponent : 5;
	'	'			unsigned short sign : 1;
	'	'TO:
	'	'			unsigned int mantissa : 23;
	'	'			unsigned int biased_exponent : 8;
	'	'			unsigned int sign : 1;
	'	Dim bitsResult As IntegerAndSingleUnion
	'	Dim mantissa As Integer
	'	Dim biased_exponent As Integer
	'	Dim sign As Integer
	'	Dim resultMantissa As Integer
	'	Dim resultBiasedExponent As Integer
	'	Dim resultSign As Integer
	'	bitsResult = 0

	'	mantissa = Me.GetMantissa(Me.the16BitValue)
	'	biased_exponent = Me.GetBiasedExponent(Me.the16BitValue)
	'	sign = Me.GetSign(Me.the16BitValue)

	'	'			output.bits.mantissa = inFloat.bits.mantissa << (23-10);
	'	'			output.bits.biased_exponent = (inFloat.bits.biased_exponent - float16bias + float32bias) * (inFloat.bits.biased_exponent != 0);
	'	'			output.bits.sign = inFloat.bits.sign;
	'	resultMantissa = mantissa
	'	If biased_exponent = 0 Then
	'		resultBiasedExponent = 0
	'	Else
	'		resultBiasedExponent = (biased_exponent - float16bias + float32bias) << 23
	'	End If
	'	resultSign = sign << 31

	'	' For debugging.
	'	'------
	'	' TEST PASSED:
	'	'If (resultMantissa Or &H7FFFFF) <> &H7FFFFF Then
	'	'	Dim i As Integer = 42
	'	'End If
	'	'------
	'	' TEST PASSED:
	'	'If (resultBiasedExponent Or &H7F800000) <> &H7F800000 Then
	'	'	Dim i As Integer = 42
	'	'End If
	'	'------
	'	' TEST PASSED:
	'	'If resultSign <> &H80000000 AndAlso resultSign <> 0 Then
	'	'	Dim i As Integer = 42
	'	'End If

	'	bitsResult = resultSign Or resultBiasedExponent Or resultMantissa

	'	Return bitsResult
	'End Function

	Private Function GetSingle(ByVal value As UShort) As Single
		'FROM:
		'			unsigned short mantissa : 10;
		'			unsigned short biased_exponent : 5;
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

		mantissa = Me.GetMantissa(Me.the16BitValue)
		biased_exponent = Me.GetBiasedExponent(Me.the16BitValue)
		sign = Me.GetSign(Me.the16BitValue)

		'			output.bits.mantissa = inFloat.bits.mantissa << (23-10);
		'			output.bits.biased_exponent = (inFloat.bits.biased_exponent - float16bias + float32bias) * (inFloat.bits.biased_exponent != 0);
		'			output.bits.sign = inFloat.bits.sign;
		resultMantissa = mantissa << (23 - 10)
		If biased_exponent = 0 Then
			resultBiasedExponent = 0
		Else
			resultBiasedExponent = (biased_exponent - float16bias + float32bias) << 23
		End If
		resultSign = sign << 31

		' For debugging.
		'------
		' TEST PASSED:
		'If (resultMantissa Or &H7FFFFF) <> &H7FFFFF Then
		'	Dim i As Integer = 42
		'End If
		'------
		' TEST PASSED:
		'If (resultBiasedExponent Or &H7F800000) <> &H7F800000 Then
		'	Dim i As Integer = 42
		'End If
		'------
		' TEST PASSED:
		'If resultSign <> &H80000000 AndAlso resultSign <> 0 Then
		'	Dim i As Integer = 42
		'End If

		bitsResult.i = resultSign Or resultBiasedExponent Or resultMantissa

		Return bitsResult.s
	End Function

	'Private Function GetDouble(ByVal value As UShort) As Double
	'	'FROM:
	'	'			unsigned short mantissa : 10;
	'	'			unsigned short biased_exponent : 5;
	'	'			unsigned short sign : 1;
	'	'TO:
	'	'			unsigned int mantissa : 23;
	'	'			unsigned int biased_exponent : 8;
	'	'			unsigned int sign : 1;
	'	Dim bitsResult As Integer
	'	Dim mantissa As Integer
	'	Dim biased_exponent As Integer
	'	Dim sign As Integer
	'	Dim resultMantissa As Integer
	'	Dim resultBiasedExponent As Integer
	'	Dim resultSign As Integer
	'	bitsResult = 0

	'	mantissa = Me.GetMantissa(Me.the16BitValue)
	'	biased_exponent = Me.GetBiasedExponent(Me.the16BitValue)
	'	sign = Me.GetSign(Me.the16BitValue)

	'	'			output.bits.mantissa = inFloat.bits.mantissa << (23-10);
	'	'			output.bits.biased_exponent = (inFloat.bits.biased_exponent - float16bias + float32bias) * (inFloat.bits.biased_exponent != 0);
	'	'			output.bits.sign = inFloat.bits.sign;
	'	resultMantissa = mantissa << (23 - 10)
	'	If biased_exponent = 0 Then
	'		resultBiasedExponent = 0
	'	Else
	'		resultBiasedExponent = (biased_exponent - float16bias + float32bias) << 23
	'	End If
	'	resultSign = sign << 31

	'	'bitsResult = resultSign Or resultBiasedExponent Or resultMantissa

	'	'Return bitsResult
	'	'Return resultSign * Math.Pow(2, resultBiasedExponent - 127) * resultMantissa
	'	Return sign * Math.Pow(2, biased_exponent - 1023) * mantissa
	'End Function

	'	static bool IsInfinity( float16bits in )
	'	{
	'		return in.bits.biased_exponent == 31 && in.bits.mantissa == 0;
	'	}
	Private Function IsInfinity(ByVal value As UShort) As Boolean
		Dim mantissa As Integer
		Dim biased_exponent As Integer

		mantissa = Me.GetMantissa(value)
		biased_exponent = Me.GetBiasedExponent(value)
		Return ((biased_exponent = 31) And (mantissa = 0))
	End Function

	'	static bool IsNaN( float16bits in )
	'	{
	'		return in.bits.biased_exponent == 31 && in.bits.mantissa != 0;
	'	}
	Private Function IsNaN(ByVal value As UShort) As Boolean
		Dim mantissa As Integer
		Dim biased_exponent As Integer

		mantissa = Me.GetMantissa(value)
		biased_exponent = Me.GetBiasedExponent(value)
		Return ((biased_exponent = 31) And (mantissa <> 0))
	End Function

	'const int float32bias = 127;
	'const int float16bias = 15;
	'const float maxfloat16bits = 65504.0f;
	'const float half_denorm = (1.0f/16384.0f); // 2^-14
	Const float32bias As Integer = 127
	Const float16bias As Integer = 15
	Const maxfloat16bits As Single = 65504.0F
	Const half_denorm As Single = (1.0F / 16384.0F)

	Public the16BitValue As UShort

	<StructLayout(LayoutKind.Explicit)> _
	Public Structure IntegerAndSingleUnion
		<FieldOffset(0)> Public i As Integer
		<FieldOffset(0)> Public s As Single
	End Structure

End Class
