Public Class SourceVector4D

	'FROM: SourceEngine2006_source\public\vector.h
	'class Vector4D					
	'{
	'public:
	'	// Members
	'	vec_t x, y, z, w;
	'
	'	// Construction/destruction
	'	Vector4D(void);
	'	Vector4D(vec_t X, vec_t Y, vec_t Z, vec_t W);
	'	Vector4D(const float *pFloat);
	'
	'	// Initialization
	'	void Init(vec_t ix=0.0f, vec_t iy=0.0f, vec_t iz=0.0f, vec_t iw=0.0f);
	'
	'	// Got any nasty NAN's?
	'	bool IsValid() const;
	'
	'	// array access...
	'	vec_t operator[](int i) const;
	'	vec_t& operator[](int i);
	'
	'	// Base address...
	'	vec_t* Base();
	'	vec_t const* Base() const;
	'
	'	// Cast to Vector and Vector2D...
	'	Vector& AsVector3D();
	'	Vector const& AsVector3D() const;
	'
	'	Vector2D& AsVector2D();
	'	Vector2D const& AsVector2D() const;
	'
	'	// Initialization methods
	'	void Random( vec_t minVal, vec_t maxVal );
	'
	'	// equality
	'	bool operator==(const Vector4D& v) const;
	'	bool operator!=(const Vector4D& v) const;	
	'
	'	// arithmetic operations
	'	Vector4D&	operator+=(const Vector4D &v);			
	'	Vector4D&	operator-=(const Vector4D &v);		
	'	Vector4D&	operator*=(const Vector4D &v);			
	'	Vector4D&	operator*=(float s);
	'	Vector4D&	operator/=(const Vector4D &v);		
	'	Vector4D&	operator/=(float s);					
	'
	'	// negate the Vector4D components
	'	void	Negate(); 
	'
	'	// Get the Vector4D's magnitude.
	'	vec_t	Length() const;
	'
	'	// Get the Vector4D's magnitude squared.
	'	vec_t	LengthSqr(void) const;
	'
	'	// return true if this vector is (0,0,0,0) within tolerance
	'	bool IsZero( float tolerance = 0.01f ) const
	'	{
	'		return (x > -tolerance && x < tolerance &&
	'				y > -tolerance && y < tolerance &&
	'				z > -tolerance && z < tolerance &&
	'				w > -tolerance && w < tolerance);
	'	}
	'
	'	// Get the distance from this Vector4D to the other one.
	'	vec_t	DistTo(const Vector4D &vOther) const;
	'
	'	// Get the distance from this Vector4D to the other one squared.
	'	vec_t	DistToSqr(const Vector4D &vOther) const;		
	'
	'	// Copy
	'	void	CopyToArray(float* rgfl) const;	
	'
	'	// Multiply, add, and assign to this (ie: *this = a + b * scalar). This
	'	// is about 12% faster than the actual Vector4D equation (because it's done per-component
	'	// rather than per-Vector4D).
	'	void	MulAdd(Vector4D const& a, Vector4D const& b, float scalar);	
	'
	'	// Dot product.
	'	vec_t	Dot(Vector4D const& vOther) const;			
	'
	'	// No copy constructors allowed if we're in optimal mode
	'#ifdef VECTOR_NO_SLOW_OPERATIONS
	'private:
	'#else
	'public:
	'#endif
	'	Vector4D(Vector4D const& vOther);
	'
	'	// No assignment operators either...
	'	Vector4D& operator=( Vector4D const& src );
	'};

	Public x As Double
	Public y As Double
	Public z As Double
	Public w As Double

	Public debug_text As String

End Class
