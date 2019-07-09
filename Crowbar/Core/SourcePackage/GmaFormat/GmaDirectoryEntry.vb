Public Class GmaDirectoryEntry
	Inherits BasePackageDirectoryEntry

	'FROM: Garry's Mod Addon Tool source gmad-master\gmad-master\include\AddonFormat.h
	'	struct FileEntry
	'	{
	'		Bootil::BString	strName;
	'		long long		iSize;
	'		unsigned long	iCRC;
	'		unsigned int	iFileNumber;
	'		long long		iOffset;
	'
	'		typedef std::list< FileEntry > List;
	'	};

	Public fileNumberStored As UInt32
	Public size As Int64

	Public offset As Int64
	Public fileNumberUsed As UInt32

End Class
