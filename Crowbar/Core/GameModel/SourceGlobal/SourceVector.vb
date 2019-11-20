Public Class SourceVector

	'FROM: SourceEngine2006_source\public\vector.h
	'//=========================================================
	'// 3D Vector
	'//=========================================================
	'	Class Vector
	'{
	'public:
	'	// Members
	'	vec_t x, y, z;

	'	// Construction/destruction:
	'	Vector(void); 
	'	Vector(vec_t X, vec_t Y, vec_t Z);

	'	// Initialization
	'	void Init(vec_t ix=0.0f, vec_t iy=0.0f, vec_t iz=0.0f);

	'	// Got any nasty NAN's?
	'	bool IsValid() const;

	'	// array access...
	'	vec_t operator[](int i) const;
	'	vec_t& operator[](int i);

	'	// Base address...
	'	vec_t* Base();
	'	vec_t const* Base() const;

	'	// Cast to Vector2D...
	'	Vector2D& AsVector2D();
	'	const Vector2D& AsVector2D() const;

	'	// Initialization methods
	'	void Random( vec_t minVal, vec_t maxVal );

	'	// equality
	'	bool operator==(const Vector& v) const;
	'	bool operator!=(const Vector& v) const;	

	'	// arithmetic operations
	'	FORCEINLINE Vector&	operator+=(const Vector &v);
	'	FORCEINLINE Vector&	operator-=(const Vector &v);
	'	FORCEINLINE Vector&	operator*=(const Vector &v);
	'	FORCEINLINE Vector&	operator*=(float s);
	'	FORCEINLINE Vector&	operator/=(const Vector &v);		
	'	FORCEINLINE Vector&	operator/=(float s);

	'	// negate the vector components
	'	void	Negate(); 

	'	// Get the vector's magnitude.
	'	inline vec_t	Length() const;

	'	// Get the vector's magnitude squared.
	'	FORCEINLINE vec_t LengthSqr(void) const
	'	{ 
	'		CHECK_VALID(*this);
	'		return (x*x + y*y + z*z);		
	'	}

	'	// return true if this vector is (0,0,0) within tolerance
	'	bool IsZero( float tolerance = 0.01f ) const
	'	{
	'		return (x > -tolerance && x < tolerance &&
	'				y > -tolerance && y < tolerance &&
	'				z > -tolerance && z < tolerance);
	'	}

	'	vec_t	NormalizeInPlace();
	'	bool	IsLengthGreaterThan( float val ) const;
	'	bool	IsLengthLessThan( float val ) const;

	'	// Get the distance from this vector to the other one.
	'	vec_t	DistTo(const Vector &vOther) const;

	'	// Get the distance from this vector to the other one squared.
	'	// NJS: note, VC wasn't inlining it correctly in several deeply nested inlines due to being an 'out of line' inline.  
	'	// may be able to tidy this up after switching to VC7
	'	FORCEINLINE vec_t DistToSqr(const Vector &vOther) const
	'	{
	'		Vector delta;

	'		delta.x = x - vOther.x;
	'		delta.y = y - vOther.y;
	'		delta.z = z - vOther.z;

	'		return delta.LengthSqr();
	'	}

	'	// Copy
	'	void	CopyToArray(float* rgfl) const;	

	'	// Multiply, add, and assign to this (ie: *this = a + b * scalar). This
	'	// is about 12% faster than the actual vector equation (because it's done per-component
	'	// rather than per-vector).
	'	void	MulAdd(const Vector& a, const Vector& b, float scalar);	

	'	// Dot product.
	'	vec_t	Dot(const Vector& vOther) const;			

	'	// assignment
	'	Vector& operator=(const Vector &vOther);

	'	// 2d
	'	vec_t	Length2D(void) const;					
	'	vec_t	Length2DSqr(void) const;					

	'	operator VectorByValue &()				{ return *((VectorByValue *)(this)); }
	'	operator const VectorByValue &() const	{ return *((const VectorByValue *)(this)); }

	'#ifndef VECTOR_NO_SLOW_OPERATIONS
	'	// copy constructors
	'//	Vector(const Vector &vOther);

	'	// arithmetic operations
	'	Vector	operator-(void) const;

	'	Vector	operator+(const Vector& v) const;	
	'	Vector	operator-(const Vector& v) const;	
	'	Vector	operator*(const Vector& v) const;	
	'	Vector	operator/(const Vector& v) const;	
	'	Vector	operator*(float fl) const;
	'	Vector	operator/(float fl) const;			

	'	// Cross product between two vectors.
	'	Vector	Cross(const Vector &vOther) const;		

	'	// Returns a vector with the min or max in X, Y, and Z.
	'	Vector	Min(const Vector &vOther) const;
	'	Vector	Max(const Vector &vOther) const;

	'#Else

	'private:
	'	// No copy constructors allowed if we're in optimal mode
	'	Vector(const Vector& vOther);
	'#End If
	'};




	Public Sub New()
		x = 0
		y = 0
		z = 0
	End Sub

	Public Sub New(ByVal iX As Double, ByVal iY As Double, ByVal iZ As Double)
		x = iX
		y = iY
		z = iZ
	End Sub

	Public x As Double
	Public y As Double
	Public z As Double

	Public debug_text As String

	'    // Compute The Cross Product To Give Us A Surface Normal
	'    out[x] = v1[y]*v2[z] - v1[z]*v2[y];				// Cross Product For Y - Z
	'    out[y] = v1[z]*v2[x] - v1[x]*v2[z];				// Cross Product For X - Z
	'    out[z] = v1[x]*v2[y] - v1[y]*v2[x];				// Cross Product For X - Y
	Public Function CrossProduct(ByVal otherVector As SourceVector) As SourceVector
		Dim crossVector As New SourceVector()

		crossVector.x = Me.y * otherVector.z - Me.z * otherVector.y
		crossVector.y = Me.z * otherVector.x - Me.x * otherVector.z
		crossVector.z = Me.x * otherVector.y - Me.y * otherVector.x

		Return crossVector
	End Function

	Public Function Normalize() As SourceVector
		Dim magnitude As Double
		Dim normalVector As New SourceVector()

		magnitude = Math.Sqrt(Me.x * Me.x + Me.y * Me.y + Me.z * Me.z)
		normalVector.x = Me.x / magnitude
		normalVector.y = Me.y / magnitude
		normalVector.z = Me.z / magnitude

		Return normalVector
	End Function

End Class
