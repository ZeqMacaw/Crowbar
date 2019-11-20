Public Class SourcePhyRagdollConstraint

	'//-----------------------------------------------------------------------------
	'// Purpose: special limited ballsocket constraint for ragdolls.
	'//			Has axis limits for all 3 axes.
	'//-----------------------------------------------------------------------------
	'struct constraint_ragdollparams_t
	'{
	'	constraint_breakableparams_t constraint;
	'	matrix3x4_t			constraintToReference;// xform constraint space to refobject space
	'	matrix3x4_t			constraintToAttached;	// xform constraint space to attached object space
	'	int					parentIndex;				// NOTE: only used for parsing.  NEED NOT BE SET for create
	'	int					childIndex;					// NOTE: only used for parsing.  NEED NOT BE SET for create
	'	constraint_axislimit_t	axes[3];
	'	bool				onlyAngularLimits;			// only angular limits (not translation as well?)
	'	bool				isActive;
	'	bool				useClockwiseRotations;		// HACKHACK: Did this wrong in version one.  Fix in the future.
	'	inline void Defaults()
	'	{
	'		constraint.Defaults();
	'		isActive = true;
	'		SetIdentityMatrix( constraintToReference );
	'		SetIdentityMatrix( constraintToAttached );
	'		parentIndex = -1;
	'		childIndex = -1;
	'		axes[0].Defaults();
	'		axes[1].Defaults();
	'		axes[2].Defaults();
	'		onlyAngularLimits = false;
	'		useClockwiseRotations = false;
	'	}
	'};

	'// by default, write constraints from each limb to its parent
	'pPhys = g_JointedModel.m_pCollisionList;
	'while ( pPhys )
	'{
	'	// check to see if bone collapse/remap has left this with parent pointing at itself
	'	if ( pPhys->m_parent )
	'	{
	'		constraint_ragdollparams_t ragdoll;
	'		BuildRagdollConstraint( pPhys, ragdoll );
	'		if ( ragdoll.parentIndex != ragdoll.childIndex )
	'		{
	'			fprintf( fp, "ragdollconstraint {\n" );
	'			KeyWriteInt( fp, "parent", ragdoll.parentIndex );
	'			KeyWriteInt( fp, "child", ragdoll.childIndex );
	'			KeyWriteFloat( fp, "xmin", ragdoll.axes[0].minRotation );
	'			KeyWriteFloat( fp, "xmax", ragdoll.axes[0].maxRotation );
	'			KeyWriteFloat( fp, "xfriction", ragdoll.axes[0].torque );
	'			KeyWriteFloat( fp, "ymin", ragdoll.axes[1].minRotation );
	'			KeyWriteFloat( fp, "ymax", ragdoll.axes[1].maxRotation );
	'			KeyWriteFloat( fp, "yfriction", ragdoll.axes[1].torque );
	'			KeyWriteFloat( fp, "zmin", ragdoll.axes[2].minRotation );
	'			KeyWriteFloat( fp, "zmax", ragdoll.axes[2].maxRotation );
	'			KeyWriteFloat( fp, "zfriction", ragdoll.axes[2].torque );
	'			fprintf( fp, "}\n" );
	'		}
	'	}
	'	pPhys = pPhys->m_pNext;
	'}

	' Example: 
	'ragdollconstraint {
	'"parent" "0"
	'"child" "1"
	'"xmin" "-10.000000"
	'"xmax" "10.000000"
	'"xfriction" "0.000000"
	'"ymin" "-16.000000"
	'"ymax" "16.000000"
	'"yfriction" "0.000000"
	'"zmin" "-20.000000"
	'"zmax" "30.000000"
	'"zfriction" "0.000000"
	'}

	Public theParentIndex As Integer
	Public theChildIndex As Integer
	Public theXMin As Single
	Public theXMax As Single
	Public theXFriction As Single
	Public theYMin As Single
	Public theYMax As Single
	Public theYFriction As Single
	Public theZMin As Single
	Public theZMax As Single
	Public theZFriction As Single

End Class
