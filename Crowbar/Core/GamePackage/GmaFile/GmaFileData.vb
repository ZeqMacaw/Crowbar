Public Class GmaFileData
	Inherits BasePackageFileData

	Public Sub New()
		MyBase.New()

	End Sub

	Public Overrides ReadOnly Property IsSourcePackage() As Boolean
		Get
			Return (Me.id = GmaFileData.GMA_ID)
		End Get
	End Property

	Public Overrides ReadOnly Property FileExtension() As String
		Get
			Return GmaFileData.TheGmaFileExtension
		End Get
	End Property

	Public Overrides ReadOnly Property DirectoryFileNameSuffix() As String
		Get
			'Return GmaFileData.TheGmaDirectoryFileNameSuffix
			Return ""
		End Get
	End Property

	Public Overrides ReadOnly Property DirectoryFileNameSuffixWithExtension() As String
		Get
			'Return GmaFileData.TheGmaDirectoryFileNameSuffix + GmaFileData.TheGmaFileExtension
			Return ""
		End Get
	End Property

	'FROM: Garry's Mod Addon Tool source gmad-master\gmad-master\src\create_gmad.cpp   Create()
	'		buffer.Write( Addon::Ident, 4 );				// Ident (4)
	'		buffer.WriteType( ( char ) Addon::Version );		// Version (1)
	'		// SteamID (8) [unused]
	'		buffer.WriteType( ( unsigned long long ) 0ULL );
	'		// TimeStamp (8)
	'		buffer.WriteType( ( unsigned long long ) Bootil::Time::UnixTimestamp() );
	'		// Required content (a list of strings)
	'		buffer.WriteType( ( char ) 0 ); // signifies nothing
	'		// Addon Name (n)
	'		buffer.WriteString( strTitle );
	'		// Addon Description (n)
	'		buffer.WriteString( strDescription );
	'		// Addon Author (n) [unused]
	'		buffer.WriteString( "Author Name" );
	'		// Addon Version (4) [unused]
	'		buffer.WriteType( ( int ) 1 );

	Public id As String
	Public version As Byte
	Public steamID(7) As Byte
	Public timestamp(7) As Byte
	Public requiredContent As String
	Public addonName As String
	Public addonDescription As String
	Public addonAuthor As String
	Public addonVersion As UInt32

	Public crc As UInt32

	Private Const GMA_ID As String = "GMAD"
	'Private Const TheGmaDirectoryFileNameSuffix As String = "_dir"
	Private Const TheGmaFileExtension As String = ".gma"
	Public theFileDataOffset As Long

End Class
