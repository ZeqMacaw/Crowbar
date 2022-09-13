Public Class RSourcePerTriCollisionHeader52

    'struct pertriheader_t
    '{
    '    int version; // unsure If this Is an actual version Or type
    '                 // set to '2' for static prop.
    '
    '    // aabb sizes, same as hulls
    '    Vector3 bbmin;
    '    Vector3 bbmax;		
    '
    '    int unused[8]; // hopefully, checks out With other mdl structs 
    '};

    ' 2 for static props, 0 for everything else, unknown if 1 is used.
    Public version As Integer

    ' Size of AABB
    Public bbMin As SourceVector
    Public bbMax As SourceVector

    Public unused(7) As Integer

End Class
