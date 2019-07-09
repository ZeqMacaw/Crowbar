Public Class SourceVector48bits

	'FROM: SourceEngine2006_source\public\compressed_vector.h
	'//=========================================================
	'// Fit a 3D vector in 48 bits
	'//=========================================================

	'	Class Vector48
	'{
	'public:
	'	// Construction/destruction:
	'	Vector48(void) {}
	'	Vector48(vec_t X, vec_t Y, vec_t Z) { x.SetFloat( X ); y.SetFloat( Y ); z.SetFloat( Z ); }

	'	// assignment
	'	Vector48& operator=(const Vector &vOther);
	'	operator Vector ();

	'	const float operator[]( int i ) const { return (((float16 *)this)[i]).GetFloat(); }

	'	float16 x;
	'	float16 y;
	'	float16 z;
	'};



	Public Sub New()
		Me.theXInput = New SourceFloat16bits()
		Me.theYInput = New SourceFloat16bits()
		Me.theZInput = New SourceFloat16bits()
	End Sub

	'Public theBytes(5) As Byte
	Public theXInput As SourceFloat16bits
	Public theYInput As SourceFloat16bits
	Public theZInput As SourceFloat16bits

	Public ReadOnly Property x() As Double
		Get
			Return Me.theXInput.TheFloatValue
		End Get
	End Property

	Public ReadOnly Property y() As Double
		Get
			Return Me.theYInput.TheFloatValue
		End Get
	End Property

	Public ReadOnly Property z() As Double
		Get
			Return Me.theZInput.TheFloatValue
		End Get
	End Property
End Class
