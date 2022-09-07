Public Class RSourceRuiMeshHeader53

    'struct mstudioruimesh_t
    '{
    '    int numparents; // apparently you can have meshes parented To more than one bone(?)    
    '    int numvertices; // number Of verts
    '    int numfaces; // number Of faces (quads)
    '
    '    int parentindex; // this gets padding out front Of it To even off the struct
    '
    '    int vertexindex; // offset into smd style vertex data
    '    int vertmapindex; // offsets into a vertex map For Each quad
    '    int facedataindex; // offset into uv section
    '
    '    byte unk[4]; // zero sometimes, others Not. has to do with face clipping.
    '
    '    char szruimeshname[parentindex - 32]; // have to subtract header to get actual size (padding included)
    '
    '    int16 parent[numparents]; // parent(s) bone Of mesh
    '
    '    mstudioruivertmap_t vertexmap[numfaces]; // vertex map For Each face  
    '    mstudioruivert_t vertex[numvertices];
    '
    '    mstudioruimesface_t facedata[numfaces];
    '};

    Public parentCount As Integer
    Public vertexCount As Integer
    Public faceCount As Integer

    Public parentOffset As Integer

    Public vertexOffset As Integer
    Public vertexMapOffset As Integer
    Public faceOffset As Integer

    Public unk As Integer

    Public meshName(parentOffset - 32) As String

End Class
