Imports System.Runtime.InteropServices
Imports System.Text

Public Class Win32Api

	''' <summary>Windows messages (WM_*, look in winuser.h)</summary>
	Public Enum WindowsMessages
		WM_ACTIVATE = &H6
		WM_COMMAND = &H111
		WM_ENTERIDLE = &H121
		WM_MOUSEWHEEL = &H20A
		WM_NOTIFY = &H4E
		WM_SHOWWINDOW = &H18
		'HWND_BROADCAST = &HFFFF
	End Enum

	Public Enum DialogChangeStatus As Long
		CDN_FIRST = &HFFFFFDA7UI
		CDN_INITDONE = (CDN_FIRST - &H0)
		CDN_SELCHANGE = (CDN_FIRST - &H1)
		CDN_FOLDERCHANGE = (CDN_FIRST - &H2)
		CDN_SHAREVIOLATION = (CDN_FIRST - &H3)
		CDN_HELP = (CDN_FIRST - &H4)
		CDN_FILEOK = (CDN_FIRST - &H5)
		CDN_TYPECHANGE = (CDN_FIRST - &H6)
	End Enum

	Public Enum DialogChangeProperties
		CDM_FIRST = (&H400 + 100)
		CDM_GETSPEC = (CDM_FIRST + &H0)
		CDM_GETFILEPATH = (CDM_FIRST + &H1)
		CDM_GETFOLDERPATH = (CDM_FIRST + &H2)
		CDM_GETFOLDERIDLIST = (CDM_FIRST + &H3)
		CDM_SETCONTROLTEXT = (CDM_FIRST + &H4)
		CDM_HIDECONTROL = (CDM_FIRST + &H5)
		CDM_SETDEFEXT = (CDM_FIRST + &H6)
	End Enum

	Public Enum ListViewMessages
		LVM_FIRST = &H1000
		LVM_INSERTITEM = (LVM_FIRST + 77)
		LVM_DELETEALLITEMS = (LVM_FIRST + 9)
		'LVM_FINDITEM = (LVM_FIRST + 13)
		'LVM_SETCOLUMNWIDTH = (LVM_FIRST + 30)
		'LVM_GETITEMTEXT = (LVM_FIRST + 45)
		'LVM_SORTITEMS = (LVM_FIRST + 48)
		'LVSCW_AUTOSIZE_USEHEADER = -2
	End Enum

	Public Enum ListViewEnums
		LVIF_TEXT = &H1
		LVIF_IMAGE = &H2
		LVIF_PARAM = &H4
		LVIF_STATE = &H8
		LVIF_INDENT = &H10
		LVIF_GROUPID = &H100
		LVIF_COLUMNS = &H200
	End Enum

	'Public Enum SpecialFolderCSIDL As Integer
	'	CSIDL_DESKTOP = &H0
	'	' <desktop>
	'	CSIDL_INTERNET = &H1
	'	' Internet Explorer (icon on desktop)
	'	CSIDL_PROGRAMS = &H2
	'	' Start Menu\Programs
	'	CSIDL_CONTROLS = &H3
	'	' My Computer\Control Panel
	'	CSIDL_PRINTERS = &H4
	'	' My Computer\Printers
	'	CSIDL_PERSONAL = &H5
	'	' My Documents
	'	CSIDL_FAVORITES = &H6
	'	' <user name>\Favorites
	'	CSIDL_STARTUP = &H7
	'	' Start Menu\Programs\Startup
	'	CSIDL_RECENT = &H8
	'	' <user name>\Recent
	'	CSIDL_SENDTO = &H9
	'	' <user name>\SendTo
	'	CSIDL_BITBUCKET = &HA
	'	' <desktop>\Recycle Bin
	'	CSIDL_STARTMENU = &HB
	'	' <user name>\Start Menu
	'	CSIDL_DESKTOPDIRECTORY = &H10
	'	' <user name>\Desktop
	'	CSIDL_DRIVES = &H11
	'	' My Computer
	'	CSIDL_NETWORK = &H12
	'	' Network Neighborhood
	'	CSIDL_NETHOOD = &H13
	'	' <user name>\nethood
	'	CSIDL_FONTS = &H14
	'	' windows\fonts
	'	CSIDL_TEMPLATES = &H15
	'	CSIDL_COMMON_STARTMENU = &H16
	'	' All Users\Start Menu
	'	CSIDL_COMMON_PROGRAMS = &H17
	'	' All Users\Programs
	'	CSIDL_COMMON_STARTUP = &H18
	'	' All Users\Startup
	'	CSIDL_COMMON_DESKTOPDIRECTORY = &H19
	'	' All Users\Desktop
	'	CSIDL_APPDATA = &H1A
	'	' <user name>\Application Data
	'	CSIDL_PRINTHOOD = &H1B
	'	' <user name>\PrintHood
	'	CSIDL_LOCAL_APPDATA = &H1C
	'	' <user name>\Local Settings\Applicaiton Data (non roaming)
	'	CSIDL_ALTSTARTUP = &H1D
	'	' non localized startup
	'	CSIDL_COMMON_ALTSTARTUP = &H1E
	'	' non localized common startup
	'	CSIDL_COMMON_FAVORITES = &H1F
	'	CSIDL_INTERNET_CACHE = &H20
	'	CSIDL_COOKIES = &H21
	'	CSIDL_HISTORY = &H22
	'	CSIDL_COMMON_APPDATA = &H23
	'	' All Users\Application Data
	'	CSIDL_WINDOWS = &H24
	'	' GetWindowsDirectory()
	'	CSIDL_SYSTEM = &H25
	'	' GetSystemDirectory()
	'	CSIDL_PROGRAM_FILES = &H26
	'	' C:\Program Files
	'	CSIDL_MYPICTURES = &H27
	'	' C:\Program Files\My Pictures
	'	CSIDL_PROFILE = &H28
	'	' USERPROFILE
	'	CSIDL_SYSTEMX86 = &H29
	'	' x86 system directory on RISC
	'	CSIDL_PROGRAM_FILESX86 = &H2A
	'	' x86 C:\Program Files on RISC
	'	CSIDL_PROGRAM_FILES_COMMON = &H2B
	'	' C:\Program Files\Common
	'	CSIDL_PROGRAM_FILES_COMMONX86 = &H2C
	'	' x86 Program Files\Common on RISC
	'	CSIDL_COMMON_TEMPLATES = &H2D
	'	' All Users\Templates
	'	CSIDL_COMMON_DOCUMENTS = &H2E
	'	' All Users\Documents
	'	CSIDL_COMMON_ADMINTOOLS = &H2F
	'	' All Users\Start Menu\Programs\Administrative Tools
	'	CSIDL_ADMINTOOLS = &H30
	'	' <user name>\Start Menu\Programs\Administrative Tools
	'	CSIDL_CONNECTIONS = &H31
	'	' Network and Dial-up Connections
	'End Enum

	Public Const Desktop As String = "::{00021400-0000-0000-C000-000000000046}"
	Public Const MyComputer As String = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"
	Public Const NetworkPlaces As String = "::{208D2C60-3AEA-1069-A2D7-08002B30309D}"
	Public Const Printers As String = "::{2227A280-3AEA-1069-A2DE-08002B30309D}"
	Public Const RecycleBin As String = "::{645FF040-5081-101B-9F08-00AA002F954E}"
	Public Const Tasks As String = "::{D6277990-4C6A-11CF-8D87-00AA0060F5BF}"

	<StructLayout(LayoutKind.Sequential)> _
	Public Structure LV_ITEM
		Public mask As Integer
		Public iItem As Integer
		Public iSubItem As Integer
		Public state As Integer
		Public stateMask As Integer
		<MarshalAs(UnmanagedType.LPStr)> Public pszText As String
		Public cchTextMax As Integer
		Public iImage As Integer
	End Structure

	<StructLayout(LayoutKind.Sequential)> _
	Public Structure NMHDR
		Public hwndFrom As IntPtr
		Public idFrom As UInteger
		Public code As UInteger
	End Structure

	<StructLayout(LayoutKind.Sequential)> _
	Public Structure OFNOTIFY
		Public hdr As NMHDR
		Public OPENFILENAME As IntPtr
		Public fileNameShareViolation As IntPtr
	End Structure

	<StructLayout(LayoutKind.Sequential)> _
	Public Structure RECT
		Private _Left As Integer, _Top As Integer, _Right As Integer, _Bottom As Integer

		Public Sub New(ByVal Rectangle As Rectangle)
			Me.New(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
		End Sub
		Public Sub New(ByVal Left As Integer, ByVal Top As Integer, ByVal Right As Integer, ByVal Bottom As Integer)
			_Left = Left
			_Top = Top
			_Right = Right
			_Bottom = Bottom
		End Sub

		Public Property X() As Integer
			Get
				Return _Left
			End Get
			Set(ByVal value As Integer)
				_Left = value
			End Set
		End Property
		Public Property Y() As Integer
			Get
				Return _Top
			End Get
			Set(ByVal value As Integer)
				_Top = value
			End Set
		End Property
		Public Property Left() As Integer
			Get
				Return _Left
			End Get
			Set(ByVal value As Integer)
				_Left = value
			End Set
		End Property
		Public Property Top() As Integer
			Get
				Return _Top
			End Get
			Set(ByVal value As Integer)
				_Top = value
			End Set
		End Property
		Public Property Right() As Integer
			Get
				Return _Right
			End Get
			Set(ByVal value As Integer)
				_Right = value
			End Set
		End Property
		Public Property Bottom() As Integer
			Get
				Return _Bottom
			End Get
			Set(ByVal value As Integer)
				_Bottom = value
			End Set
		End Property
		Public Property Height() As Integer
			Get
				Return _Bottom - _Top
			End Get
			Set(ByVal value As Integer)
				_Bottom = value - _Top
			End Set
		End Property
		Public Property Width() As Integer
			Get
				Return _Right - _Left
			End Get
			Set(ByVal value As Integer)
				_Right = value + _Left
			End Set
		End Property
		Public Property Location() As Point
			Get
				Return New Point(Left, Top)
			End Get
			Set(ByVal value As Point)
				_Left = value.X
				_Top = value.Y
			End Set
		End Property
		Public Property Size() As Size
			Get
				Return New Size(Width, Height)
			End Get
			Set(ByVal value As Size)
				_Right = value.Width + _Left
				_Bottom = value.Height + _Top
			End Set
		End Property

		Public Shared Widening Operator CType(ByVal Rectangle As RECT) As Rectangle
			Return New Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height)
		End Operator
		Public Shared Widening Operator CType(ByVal Rectangle As Rectangle) As RECT
			Return New RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
		End Operator
		Public Shared Operator =(ByVal Rectangle1 As RECT, ByVal Rectangle2 As RECT) As Boolean
			Return Rectangle1.Equals(Rectangle2)
		End Operator
		Public Shared Operator <>(ByVal Rectangle1 As RECT, ByVal Rectangle2 As RECT) As Boolean
			Return Not Rectangle1.Equals(Rectangle2)
		End Operator

		Public Overrides Function ToString() As String
			Return "{Left: " & _Left & "; " & "Top: " & _Top & "; Right: " & _Right & "; Bottom: " & _Bottom & "}"
		End Function

		Public Overloads Function Equals(ByVal Rectangle As RECT) As Boolean
			Return Rectangle.Left = _Left AndAlso Rectangle.Top = _Top AndAlso Rectangle.Right = _Right AndAlso Rectangle.Bottom = _Bottom
		End Function
		Public Overloads Overrides Function Equals(ByVal [Object] As Object) As Boolean
			If TypeOf [Object] Is RECT Then
				Return Equals(DirectCast([Object], RECT))
			ElseIf TypeOf [Object] Is Rectangle Then
				Return Equals(New RECT(DirectCast([Object], Rectangle)))
			End If

			Return False
		End Function
	End Structure

	<StructLayout(LayoutKind.Sequential)> _
	Public Structure WINDOWINFO
		Dim cbSize As Integer
		Dim rcWindow As RECT
		Dim rcClient As RECT
		Dim dwStyle As Integer
		Dim dwExStyle As Integer
		Dim dwWindowStatus As UInt32
		Dim cxWindowBorders As UInt32
		Dim cyWindowBorders As UInt32
		Dim atomWindowType As UInt16
		Dim wCreatorVersion As Short
	End Structure

	Public Delegate Function EnumWindowsProc(ByVal Handle As IntPtr, ByVal Parameter As IntPtr) As Boolean

	<DllImport("kernel32.dll", SetLastError:=True)> _
	Public Shared Function CloseHandle(ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

	<DllImport("shell32.dll")> _
	Shared Sub SHChangeNotify(ByVal wEventId As Integer, ByVal uFlags As Integer, ByVal dwItem1 As Integer, ByVal dwItem2 As Integer)
	End Sub

	<DllImport("shell32.dll")> _
	Private Shared Function SHGetFolderPath(ByVal hwndOwner As IntPtr, ByVal nFolder As Int32, ByVal hToken As IntPtr, ByVal dwFlags As Int32, ByVal pszPath As StringBuilder) As Int32
	End Function

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
	Public Shared Function EnumChildWindows(ByVal hWndParent As System.IntPtr, ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As Integer) As Boolean
	End Function

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
	Public Shared Sub GetClassName(ByVal hWnd As System.IntPtr, ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer)
	End Sub

	<DllImport("user32.dll")> _
	Public Shared Function GetDlgCtrlID(ByVal hwndCtl As System.IntPtr) As Integer
	End Function

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
	Public Shared Function GetParent(ByVal hWnd As IntPtr) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True)> _
	Public Shared Function GetWindowInfo(ByVal hwnd As IntPtr, ByRef pwi As WINDOWINFO) As Boolean
	End Function

	<DllImport("user32.dll", SetLastError:=True)> _
	Public Shared Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, ByRef lpdwProcessId As IntPtr) As Integer
	End Function

	''' <summary>Send message to a window (platform invoke)</summary>
	''' <param name="hWnd">Window handle to send to</param>
	''' <param name="msg">Message</param>
	''' <param name="wParam">wParam</param>
	''' <param name="lParam">lParam</param>
	''' <returns>Zero if failure, otherwise non-zero</returns>
	<DllImport("user32.dll", SetLastError:=True)> _
	Public Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As LV_ITEM) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As System.Text.StringBuilder) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	Public Shared Function FindWindowEx(ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal lclassName As String, ByVal windowTitle As String) As IntPtr
	End Function

	<DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	Public Shared Function CreateHardLink(ByVal lpNewFileName As String, ByVal lpExistingFileName As String, ByVal lpSecurityAttributes As IntPtr) As Boolean
	End Function

	Public Enum SymbolicLink
		File = 0
		Directory = 1
	End Enum

	<DllImport("kernel32.dll", SetLastError:=True)> _
	Public Shared Function CreateSymbolicLink(lpSymlinkFileName As String, lpTargetFileName As String, dwFlags As SymbolicLink) As Boolean
	End Function

	Private Const MAX_PATH As Integer = 260
	Private Const NAMESIZE As Integer = 80
	Private Const SHGFI_LARGEICON As Int32 = &H0
	Private Const SHGFI_SMALLICON As Int32 = &H1
	Private Const SHGFI_USEFILEATTRIBUTES As Int32 = &H10
	Private Const SHGFI_ICON As Int32 = &H100
	Public Const FILE_ATTRIBUTE_DIRECTORY As Integer = &H10
	'Public Const CFSTR_FILEDESCRIPTORW As String = "FileGroupDescriptorW"
	'Public Const CFSTR_PREFERREDDROPEFFECT As String = "Preferred DropEffect"
	'Public Const CFSTR_PERFORMEDDROPEFFECT As String = "Performed DropEffect"
	'Public Const FD_PROGRESSUI As Int32 = &H4000

	<StructLayout(LayoutKind.Sequential)>
	Private Structure SHFILEINFO
		Public hIcon As IntPtr
		Public iIcon As Integer
		Public dwAttributes As Integer
		<MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)>
		Public szDisplayName As String
		<MarshalAs(UnmanagedType.ByValTStr, SizeConst:=NAMESIZE)>
		Public szTypeName As String
	End Structure

	<DllImport("Shell32.dll")>
	Private Shared Function SHGetFileInfo(pszPath As String,
										  dwFileAttributes As Integer,
										  ByRef psfi As SHFILEINFO,
										  cbFileInfo As Integer,
										  uFlags As Integer) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True)>
	Private Shared Function DestroyIcon(hIcon As IntPtr) As Boolean
	End Function

	Public Shared Function GetShellIcon(path As String, Optional ByVal fileAttributes As Integer = 0) As Bitmap
		Dim bmp As Bitmap
		Dim ret As IntPtr
		Dim shfi As SHFILEINFO

		bmp = Nothing
		shfi = New SHFILEINFO()
		ret = SHGetFileInfo(path, fileAttributes, shfi, Marshal.SizeOf(shfi), SHGFI_USEFILEATTRIBUTES Or SHGFI_ICON)
		If ret <> IntPtr.Zero Then
			bmp = System.Drawing.Icon.FromHandle(shfi.hIcon).ToBitmap
			DestroyIcon(shfi.hIcon)
		End If

		Return bmp
	End Function

	'<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	'Public Shared Function RegisterWindowMessage(ByVal lpString As String) As UInteger
	'End Function

	'Public Shared Function GetSpecialFolderPath(ByVal folderCSIDL As SpecialFolderCSIDL) As String
	'	Dim winPath As New StringBuilder(300)
	'	If SHGetFolderPath(Nothing, folderCSIDL, Nothing, 0, winPath) <> 0 Then
	'		'Throw New ApplicationException("Can't get window's directory")
	'		Return ""
	'	End If
	'	Return winPath.ToString()
	'End Function

	'Public Shared Function GetContentType(ByVal extension As String) As String
	'	Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension)
	'	If Not regKey Is Nothing Then
	'		Dim ct As Object = regKey.GetValue("Content Type")
	'		If Not ct Is Nothing Then
	'			Return ct.ToString()
	'		End If
	'	End If
	'	Return ""
	'End Function

	Public Shared Function GetFileTypeDescription(ByVal extension As String) As String
		Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension)
		If Not regKey Is Nothing Then
			Dim extensionDefaultValue As Object = regKey.GetValue("")
			If extensionDefaultValue IsNot Nothing Then
				Dim classname As String = extensionDefaultValue.ToString()
				Dim classnameKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(classname)
				If classnameKey IsNot Nothing Then
					Dim ct As Object = classnameKey.GetValue("")
					If ct IsNot Nothing Then
						Return ct.ToString()
					End If
				End If
			End If
		End If
		Return ""
	End Function

	Public Shared Function CreateFileAssociation(ByVal extension As String, ByVal className As String, ByVal description As String, ByVal exeProgram As String) As Boolean
		Const SHCNE_ASSOCCHANGED As Integer = &H8000000
		Const SHCNF_IDLIST As Integer = 0

		' ensure that there is a leading dot
		If extension.Substring(0, 1) <> "." Then
			extension = "." + extension
		End If

		Dim currentUser As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser
		Dim classesKey As Microsoft.Win32.RegistryKey = Nothing
		Dim extensionKey As Microsoft.Win32.RegistryKey = Nothing
		Dim classnameKey As Microsoft.Win32.RegistryKey = Nothing
		Dim defaultIconKey As Microsoft.Win32.RegistryKey = Nothing
		Dim shellKey As Microsoft.Win32.RegistryKey = Nothing
		Dim shellOpenCommandKey As Microsoft.Win32.RegistryKey = Nothing
		Try
			Win32Api.DeleteFileAssociation(extension, className, description, "")

			classesKey = currentUser.OpenSubKey("Software\Classes", True)

			extensionKey = classesKey.CreateSubKey(extension)
			extensionKey.SetValue("", className)

			classnameKey = classesKey.CreateSubKey(className)
			classnameKey.SetValue("", description)

			defaultIconKey = classesKey.CreateSubKey(className + "\DefaultIcon")
			defaultIconKey.SetValue("", exeProgram + ",0")

			shellKey = classesKey.CreateSubKey(className + "\Shell")
			shellKey.SetValue("", "Open")

			shellOpenCommandKey = classesKey.CreateSubKey(className + "\Shell\Open\Command")
			shellOpenCommandKey.SetValue("", exeProgram + " ""%1""")
		Catch ex As Exception
			Return False
		Finally
			If Not classesKey Is Nothing Then classesKey.Close()
			If Not extensionKey Is Nothing Then extensionKey.Close()
			If Not classnameKey Is Nothing Then classnameKey.Close()
			If Not shellOpenCommandKey Is Nothing Then shellOpenCommandKey.Close()
		End Try

		' notify Windows that file associations have changed
		SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, 0, 0)

		Return True
	End Function

	Public Shared Function FileAssociationIsAlreadyAssigned(ByVal extension As String, ByVal className As String, ByVal description As String, ByVal exeProgram As String) As Boolean
		' ensure that there is a leading dot
		If extension.Substring(0, 1) <> "." Then
			extension = "." + extension
		End If

		Dim currentUser As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser
		Dim classesKey As Microsoft.Win32.RegistryKey = Nothing
		Dim shellKey As Microsoft.Win32.RegistryKey = Nothing
		Dim shellOpenCommandKey As Microsoft.Win32.RegistryKey = Nothing
		Try
			classesKey = currentUser.OpenSubKey("Software\Classes", True)

			'shellKey = classesKey.OpenSubKey(className + "\Shell")
			'If shellKey IsNot Nothing Then
			'	If shellKey.GetValueKind("") = Microsoft.Win32.RegistryValueKind.String Then
			'		Dim keyValueString3 As String = CType(shellKey.GetValue(""), String)
			'		If keyValueString3 = "Open" Then
			'			Return True
			'		End If
			'	End If
			'End If

			shellOpenCommandKey = classesKey.OpenSubKey(className + "\Shell\Open\Command")
			If shellOpenCommandKey IsNot Nothing Then
				If shellOpenCommandKey.GetValueKind("") = Microsoft.Win32.RegistryValueKind.String Then
					Dim keyValueString3 As String = CType(shellOpenCommandKey.GetValue(""), String)
					If keyValueString3 = (exeProgram + " ""%1""") Then
						Return True
					End If
				End If
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Not classesKey Is Nothing Then classesKey.Close()
			'If Not key1 Is Nothing Then key1.Close()
			'If Not key2 Is Nothing Then key2.Close()
			If Not shellOpenCommandKey Is Nothing Then shellOpenCommandKey.Close()
		End Try

		Return False
	End Function

	Public Shared Function DeleteFileAssociation(ByVal extension As String, ByVal className As String, ByVal description As String, ByVal exeProgram As String) As Boolean
		Const SHCNE_ASSOCCHANGED As Integer = &H8000000
		Const SHCNF_IDLIST As Integer = 0

		' ensure that there is a leading dot
		If extension.Substring(0, 1) <> "." Then
			extension = "." + extension
		End If

		Dim currentUser As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser
		Dim classesKey As Microsoft.Win32.RegistryKey = Nothing
		Dim shellOpenCommandKey As Microsoft.Win32.RegistryKey = Nothing
		Try
			classesKey = currentUser.OpenSubKey("Software\Classes", True)
			shellOpenCommandKey = classesKey.OpenSubKey(className + "\Shell\Open\Command")
			If shellOpenCommandKey IsNot Nothing Then
				If shellOpenCommandKey.GetValueKind("") = Microsoft.Win32.RegistryValueKind.String Then
					Dim keyValueString3 As String = CType(shellOpenCommandKey.GetValue(""), String)
					If exeProgram = "" OrElse keyValueString3 = (exeProgram + " ""%1""") Then
						classesKey.DeleteSubKey(className + "\Shell\Open\Command", False)
						classesKey.DeleteSubKey(className + "\Shell\Open", False)
						classesKey.DeleteSubKey(className + "\Shell", False)
						classesKey.DeleteSubKey(className + "\DefaultIcon", False)
						classesKey.DeleteSubKey(className, False)
						classesKey.DeleteSubKey(extension, False)
					End If
				End If
			End If
		Catch ex As Exception
			Return False
		Finally
			If Not classesKey Is Nothing Then classesKey.Close()
			'If Not key1 Is Nothing Then key1.Close()
			'If Not key2 Is Nothing Then key2.Close()
			If Not shellOpenCommandKey Is Nothing Then shellOpenCommandKey.Close()
		End Try

		' notify Windows that file associations have changed
		SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, 0, 0)

		Return True
	End Function

	<DllImport("kernel32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)> _
	Public Shared Function GlobalAlloc(uFlags As Integer, dwBytes As Integer) As IntPtr
	End Function

	<DllImport("kernel32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)> _
	Public Shared Function GlobalFree(handle As HandleRef) As IntPtr
	End Function

	' Clipboard formats used for cut/copy/drag operations
	Public Const CFSTR_PREFERREDDROPEFFECT As String = "Preferred DropEffect"
	Public Const CFSTR_PERFORMEDDROPEFFECT As String = "Performed DropEffect"
	Public Const CFSTR_FILEDESCRIPTORW As String = "FileGroupDescriptorW"
	Public Const CFSTR_FILECONTENTS As String = "FileContents"

	' File Descriptor Flags
	Public Const FD_CLSID As Int32 = &H1
	Public Const FD_SIZEPOINT As Int32 = &H2
	Public Const FD_ATTRIBUTES As Int32 = &H4
	Public Const FD_CREATETIME As Int32 = &H8
	Public Const FD_ACCESSTIME As Int32 = &H10
	Public Const FD_WRITESTIME As Int32 = &H20
	Public Const FD_FILESIZE As Int32 = &H40
	Public Const FD_PROGRESSUI As Int32 = &H4000
	Public Const FD_LINKUI As Int32 = &H8000

	' Global Memory Flags
	Public Const GMEM_MOVEABLE As Int32 = &H2
	Public Const GMEM_ZEROINIT As Int32 = &H40
	Public Const GHND As Int32 = (GMEM_MOVEABLE Or GMEM_ZEROINIT)
	Public Const GMEM_DDESHARE As Int32 = &H2000

	' IDataObject constants
	Public Const DV_E_TYMED As Int32 = &H80040069

End Class
