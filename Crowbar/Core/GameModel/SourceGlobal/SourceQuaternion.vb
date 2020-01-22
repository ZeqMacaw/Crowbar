Public Class SourceQuaternion

	'FROM: SourceEngine2006_source\public\vector.h
	'class Quaternion				// same data-layout as engine's vec4_t,
	'{								//		which is a vec_t[4]
	'public:
	'	inline Quaternion(void)	{ 
	'
	'	// Initialize to NAN to catch errors
	'#ifdef _DEBUG
	'#ifdef VECTOR_PARANOIA
	'		x = y = z = w = VEC_T_NAN;
	'#endif
	'#endif
	'	}
	'	inline Quaternion(vec_t ix, vec_t iy, vec_t iz, vec_t iw) : x(ix), y(iy), z(iz), w(iw) { }
	'	inline Quaternion(RadianEuler const &angle);	// evil auto type promotion!!!
	'
	'	inline void Init(vec_t ix=0.0f, vec_t iy=0.0f, vec_t iz=0.0f, vec_t iw=0.0f)	{ x = ix; y = iy; z = iz; w = iw; }
	'
	'	bool IsValid() const;
	'
	'	bool operator==( const Quaternion &src ) const;
	'	bool operator!=( const Quaternion &src ) const;
	'
	'	// array access...
	'	vec_t operator[](int i) const;
	'	vec_t& operator[](int i);
	'
	'	vec_t x, y, z, w;
	'};

	'FROM: SourceEngine2006_source\public\tier0\basetypes.h
	'typedef float vec_t;



	Public x As Double
	Public y As Double
	Public z As Double
	Public w As Double

	'NOTE: These Radians properties do not calculate correctly.

	'Public ReadOnly Property xRadians() As Double
	'	Get
	'		'    cos_a = W;
	'		'    angle = acos( cos_a ) * 2;
	'		'    sin_a = sqrt( 1.0 - cos_a * cos_a );
	'		'    if ( fabs( sin_a ) < 0.0005 ) sin_a = 1;
	'		'    axis -> x = X / sin_a;
	'		'    axis -> y = Y / sin_a;
	'		'    axis -> z = Z / sin_a;
	'		Dim cos_a As Double
	'		Dim angle As Double
	'		Dim sin_a As Double

	'		cos_a = Me.w
	'		angle = Math.Acos(cos_a) * 2
	'		sin_a = Math.Sqrt(1.0 - cos_a * cos_a)
	'		If Math.Abs(sin_a) < 0.000005 Then
	'			sin_a = 1
	'		End If

	'		Return Me.x / sin_a
	'	End Get
	'End Property

	'Public ReadOnly Property yRadians() As Double
	'	Get
	'		Dim cos_a As Double
	'		Dim angle As Double
	'		Dim sin_a As Double

	'		cos_a = Me.w
	'		angle = Math.Acos(cos_a) * 2
	'		sin_a = Math.Sqrt(1.0 - cos_a * cos_a)
	'		If Math.Abs(sin_a) < 0.000005 Then
	'			sin_a = 1
	'		End If

	'		Return Me.y / sin_a
	'	End Get
	'End Property

	'Public ReadOnly Property zRadians() As Double
	'	Get
	'		Dim cos_a As Double
	'		Dim angle As Double
	'		Dim sin_a As Double

	'		cos_a = Me.w
	'		angle = Math.Acos(cos_a) * 2
	'		sin_a = Math.Sqrt(1.0 - cos_a * cos_a)
	'		If Math.Abs(sin_a) < 0.000005 Then
	'			sin_a = 1
	'		End If

	'		Return Me.z / sin_a
	'	End Get
	'End Property

End Class
