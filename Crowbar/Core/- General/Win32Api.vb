Imports System.Runtime.InteropServices
Imports System.Text

Public Class Win32Api

	''' <summary>Windows messages (WM_*, look in winuser.h)</summary>
	Public Enum WindowsMessages
		WM_CREATE = &H1
		WM_DESTROY = &H2
		WM_ACTIVATE = &H6
		WM_SETFOCUS = &H7
		WM_KILLFOCUS = &H8
		WM_PAINT = &HF
		WM_ERASEBKGND = &H14
		WM_SHOWWINDOW = &H18
		WM_FONTCHANGE = &H1D
		WM_SETCURSOR = &H20
		WM_SETFONT = &H30
		WM_WINDOWPOSCHANGING = &H46
		WM_NOTIFY = &H4E
		WM_NCDESTROY = &H82
		WM_NCCALCSIZE = &H83
		WM_NCPAINT = &H85
		WM_KEYDOWN = &H100
		WM_KEYUP = &H101
		WM_CHAR = &H102
		WM_COMMAND = &H111
		WM_HSCROLL = &H114
		WM_VSCROLL = &H115
		WM_ENTERIDLE = &H121
		WM_MOUSEMOVE = &H200
		WM_LBUTTONDOWN = &H201
		WM_LBUTTONUP = &H202
		WM_LBUTTONDBLCLK = &H203
		WM_RBUTTONDOWN = &H204
		WM_RBUTTONUP = &H205
		WM_RBUTTONDBLCLK = &H206
		WM_MBUTTONDOWN = &H207
		WM_MBUTTONUP = &H208
		WM_MBUTTONDBLCLK = &H209
		WM_MOUSEWHEEL = &H20A
		WM_PARENTNOTIFY = &H210
		WM_USER = &H400
		EM_GETSCROLLPOS = WM_USER + 221
		EM_SETSCROLLPOS = WM_USER + 222
		WM_REFLECT = &H2000
		'HWND_BROADCAST = &HFFFF
	End Enum

	Public Enum ComboBoxNotifications As Long
		CBN_DROPDOWN = &H7
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

	Public Enum EditControlMessage
		EM_REPLACESEL = &HC2
		EM_CANUNDO = &HC6
		EM_UNDO = &HC7
	End Enum

	Public Enum ListBoxMessages
		LB_GETCURSEL = &H188
		LB_GETITEMRECT = &H198
	End Enum

	Public Enum ListViewMessages
		LVM_FIRST = &H1000
		LVM_INSERTITEM = (LVM_FIRST + 77)
		LVM_DELETEITEM = (LVM_FIRST + 8)
		LVM_DELETEALLITEMS = (LVM_FIRST + 9)
		'LVM_FINDITEM = (LVM_FIRST + 13)
		LVM_GETITEMRECT = (LVM_FIRST + 14)
		LVM_INSERTCOLUMN = (LVM_FIRST + 97)
		LVM_DELETECOLUMN = (LVM_FIRST + 28)
		'LVM_SETCOLUMNWIDTH = (LVM_FIRST + 30)
		LVM_GETHEADER = (LVM_FIRST + 31)
		LVM_SETITEMSTATE = LVM_FIRST + 43
		'LVM_GETITEMTEXT = (LVM_FIRST + 45)
		'LVM_SORTITEMS = (LVM_FIRST + 48)
		LVM_GETITEMSPACING = (LVM_FIRST + 51)
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

	<StructLayout(LayoutKind.Sequential)>
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

	<StructLayout(LayoutKind.Sequential)>
	Public Structure NMHDR
		Public hwndFrom As IntPtr
		Public idFrom As UInteger
		Public code As UInteger
	End Structure

	<StructLayout(LayoutKind.Sequential)>
	Public Structure OFNOTIFY
		Public hdr As NMHDR
		Public OPENFILENAME As IntPtr
		Public fileNameShareViolation As IntPtr
	End Structure

	<StructLayout(LayoutKind.Sequential)>
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

	<StructLayout(LayoutKind.Sequential)>
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

	<DllImport("kernel32.dll", SetLastError:=True)>
	Public Shared Function CloseHandle(ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

	<DllImport("shell32.dll")>
	Shared Sub SHChangeNotify(ByVal wEventId As Integer, ByVal uFlags As Integer, ByVal dwItem1 As Integer, ByVal dwItem2 As Integer)
	End Sub

	<DllImport("shell32.dll")>
	Private Shared Function SHGetFolderPath(ByVal hwndOwner As IntPtr, ByVal nFolder As Int32, ByVal hToken As IntPtr, ByVal dwFlags As Int32, ByVal pszPath As StringBuilder) As Int32
	End Function

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)>
	Public Shared Function EnumChildWindows(ByVal hWndParent As System.IntPtr, ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As Integer) As Boolean
	End Function

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)>
	Public Shared Sub GetClassName(ByVal hWnd As System.IntPtr, ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer)
	End Sub

	<DllImport("user32.dll")>
	Public Shared Function GetDlgCtrlID(ByVal hwndCtl As System.IntPtr) As Integer
	End Function

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)>
	Public Shared Function GetParent(ByVal hWnd As IntPtr) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True)>
	Public Shared Function GetWindowInfo(ByVal hwnd As IntPtr, ByRef pwi As WINDOWINFO) As Boolean
	End Function

	<DllImport("user32.dll", SetLastError:=True)>
	Public Shared Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, ByRef lpdwProcessId As IntPtr) As Integer
	End Function

	''' <summary>Send message to a window (platform invoke)</summary>
	''' <param name="hWnd">Window handle to send to</param>
	''' <param name="msg">Message</param>
	''' <param name="wParam">wParam</param>
	''' <param name="lParam">lParam</param>
	''' <returns>Zero if failure, otherwise non-zero</returns>
	<DllImport("user32.dll", SetLastError:=True)>
	Public Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
	End Function

	'<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	'Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
	'End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByRef lParam As LV_ITEM) As IntPtr
	End Function

	'<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	'Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As LV_ITEM) As IntPtr
	'End Function
	'<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	'Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As ListViewMessages, ByVal wParam As IntPtr, ByVal lParam As LV_ITEM) As IntPtr
	'End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As System.Text.StringBuilder) As IntPtr
	End Function

	Public Declare Auto Function RtfScroll Lib "user32.dll" Alias "SendMessage" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByRef lParam As System.Drawing.Point) As Integer

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	Public Shared Function FindWindowEx(ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal lclassName As String, ByVal windowTitle As String) As IntPtr
	End Function

	<DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	Public Shared Function CreateHardLink(ByVal lpNewFileName As String, ByVal lpExistingFileName As String, ByVal lpSecurityAttributes As IntPtr) As Boolean
	End Function

	Public Enum SymbolicLink
		File = 0
		Directory = 1
	End Enum

	<DllImport("kernel32.dll", SetLastError:=True)>
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
		' Must use SHGFI_SMALLICON for the icons to show correctly in Details view.
		ret = SHGetFileInfo(path, fileAttributes, shfi, Marshal.SizeOf(shfi), SHGFI_USEFILEATTRIBUTES Or SHGFI_ICON Or SHGFI_SMALLICON)
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

	<DllImport("kernel32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
	Public Shared Function GlobalAlloc(uFlags As Integer, dwBytes As Integer) As IntPtr
	End Function

	<DllImport("kernel32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
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

	Public Shared Sub SetItemState(ByVal list As ListView, ByVal itemIndex As Integer, ByVal mask As Integer, ByVal value As Integer)
		Dim lvItem As LV_ITEM = New LV_ITEM()
		lvItem.stateMask = mask
		lvItem.state = value
		SendMessage(list.Handle, CUInt(ListViewMessages.LVM_SETITEMSTATE), New IntPtr(itemIndex), lvItem)
	End Sub

	' Select all listview items much more quickly than other ways.
	Public Shared Sub SelectAllItems(ByVal list As ListView)
		SetItemState(list, -1, 2, 2)
	End Sub

	Public Declare Function GetCaretPos Lib "user32.dll" (ByRef lpPoint As Point) As Int32
	Public Declare Function SetCaretPos Lib "user32.dll" (x As Int32, y As Int32) As Int32

	Public Shared Sub SetInnerMargins(ByVal textBox As TextBoxBase, ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer)
		Dim rect As Rectangle = GetFormattingRect(textBox)
		Dim newRect As Rectangle = New Rectangle(left, top, rect.Width - left - right, rect.Height - top - bottom)
		SetFormattingRect(textBox, newRect)
	End Sub

	Private Shared Sub SetFormattingRect(ByVal textbox As TextBoxBase, ByVal rect As Rectangle)
		Dim rc As RECT = New RECT(rect)
		SendMessageRefRect(textbox.Handle, EmSetrect, 0, rc)
	End Sub

	Private Shared Function GetFormattingRect(ByVal textbox As TextBoxBase) As Rectangle
		Dim rect As Rectangle = New Rectangle()
		SendMessage(textbox.Handle, EmGetrect, CType(0, IntPtr), rect)
		Return rect
	End Function

	'<StructLayout(LayoutKind.Sequential)>
	'Private Structure RECT
	'	Public ReadOnly Left As Integer
	'	Public ReadOnly Top As Integer
	'	Public ReadOnly Right As Integer
	'	Public ReadOnly Bottom As Integer

	'	Private Sub New(ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer)
	'		left = left
	'		top = top
	'		right = right
	'		bottom = bottom
	'	End Sub

	'	Public Sub New(ByVal r As Rectangle)
	'		Me.New(r.Left, r.Top, r.Right, r.Bottom)
	'	End Sub
	'End Structure

	<DllImport("User32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
	Private Shared Function SendMessageRefRect(ByVal hWnd As IntPtr, ByVal msg As UInteger, ByVal wParam As Integer, ByRef rect As RECT) As Integer
	End Function
	<DllImport("user32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
	Private Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As IntPtr, ByRef lParam As Rectangle) As Integer
	End Function
	Private Const EmGetrect As Integer = &HB2
	Private Const EmSetrect As Integer = &HB3

	Public Declare Function SetCursor Lib "user32.dll" Alias "SetCursor" (ByVal hCursor As IntPtr) As IntPtr

	<StructLayout(LayoutKind.Sequential)>
	Public Structure NCCALCSIZE_PARAMS
		Public rect0 As RECT
		Public rect1 As RECT
		Public rect2 As RECT
		Public lppos As IntPtr
	End Structure

	Public Declare Function SetWindowPos Lib "user32.dll" Alias "SetWindowPos" (ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInteger) As Boolean

	<Flags>
	Public Enum SWP As UInteger
		SWP_NOSIZE = &H1
		SWP_NOMOVE = &H2
		SWP_NOZORDER = &H4
		SWP_NOREDRAW = &H8
		SWP_NOACTIVATE = &H10
		SWP_FRAMECHANGED = &H20
		SWP_SHOWWINDOW = &H40
		SWP_HIDEWINDOW = &H80
		SWP_NOCOPYBITS = &H100
		SWP_NOOWNERZORDER = &H200
		SWP_NOSENDCHANGING = &H400
		SWP_DRAWFRAME = SWP_FRAMECHANGED
		SWP_NOREPOSITION = SWP_NOOWNERZORDER
		SWP_DEFERERASE = &H2000
		SWP_ASYNCWINDOWPOS = &H4000
	End Enum

	<StructLayout(LayoutKind.Sequential)>
	Public Structure WINDOWPOS
		Implements IDisposable
		Public hwnd As IntPtr
		Public hwndInsertAfter As IntPtr
		Public x As Int32
		Public y As Int32
		Public cx As Int32
		Public cy As Int32
		Public flags As Int32
		Public Sub Dispose() Implements System.IDisposable.Dispose
			hwnd = Nothing
			hwndInsertAfter = Nothing
		End Sub
	End Structure

	Public Const GWL_STYLE As Integer = -16
	Public Const GWL_EXSTYLE As Integer = -20

	Public Enum WindowsStyles As Integer
		WS_EX_TOPMOST = &H8
		WS_HSCROLL = &H100000
		WS_VSCROLL = &H200000
		WS_VISIBLE = &H10000000
	End Enum

	Public Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
		If IntPtr.Size = 4 Then
			Return CInt(GetWindowLong32(hWnd, nIndex))
		Else
			Return CInt(CLng(GetWindowLongPtr64(hWnd, nIndex)))
		End If
	End Function

	Public Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
		If IntPtr.Size = 4 Then
			Return CInt(SetWindowLongPtr32(hWnd, nIndex, dwNewLong))
		Else
			Return CInt(CLng(SetWindowLongPtr64(hWnd, nIndex, dwNewLong)))
		End If
	End Function

	Public Declare Function GetWindowLong32 Lib "user32.dll" Alias "GetWindowLongA" (ByVal hWnd As IntPtr, ByVal nIndex As Integer) As IntPtr
	Public Declare Function GetWindowLongPtr64 Lib "user32.dll" Alias "GetWindowLongPtrA" (ByVal hWnd As IntPtr, ByVal nIndex As Integer) As IntPtr
	Public Declare Function SetWindowLongPtr32 Lib "user32.dll" Alias "SetWindowLongA" (ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As IntPtr
	Public Declare Function SetWindowLongPtr64 Lib "user32.dll" Alias "SetWindowLongPtrA" (ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As IntPtr

	Public Declare Function SetParent Lib "user32.dll" Alias "SetParent" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr

	Public Shared ReadOnly HWND_TOPMOST As IntPtr = New IntPtr(-1)
	Public Shared ReadOnly HWND_NOTOPMOST As IntPtr = New IntPtr(-2)
	Public Shared ReadOnly HWND_TOP As IntPtr = New IntPtr(0)
	Public Shared ReadOnly HWND_BOTTOM As IntPtr = New IntPtr(1)

	Public Shared Function HiWord(lValue As Long) As Integer
		If (lValue And &H80000000) > 0 Then
			HiWord = CInt((lValue \ 65535) - 1)
		Else
			HiWord = CInt(lValue \ 65535)
		End If
	End Function

	Public Shared Function LoWord(lValue As Long) As Integer
		If (lValue And &H8000) > 0 Then
			LoWord = CInt(&H8000 Or (lValue And &H7FFF))
		Else
			LoWord = CInt(lValue And &HFFFF)
		End If
	End Function

	Public Declare Function GetWindowRect Lib "user32" Alias "GetWindowRect" (ByVal hwnd As IntPtr, ByRef lpRect As RECT) As Integer

	Public Declare Function GetUpdateRect Lib "user32" Alias "GetUpdateRect" (ByVal hwnd As IntPtr, ByRef rect As RECT, ByVal [erase] As Boolean) As Integer

	<StructLayout(LayoutKind.Sequential)>
	Public Structure PAINTSTRUCT
		Public hdc As IntPtr
		Public fErase As Integer
		Public rcPaint As RECT
		Public fRestore As Integer
		Public fIncUpdate As Integer
		Public Reserved1 As Integer
		Public Reserved2 As Integer
		Public Reserved3 As Integer
		Public Reserved4 As Integer
		Public Reserved5 As Integer
		Public Reserved6 As Integer
		Public Reserved7 As Integer
		Public Reserved8 As Integer
	End Structure

	Public Declare Function BeginPaint Lib "user32" Alias "BeginPaint" (ByVal hWnd As IntPtr, ByRef paintStruct As PAINTSTRUCT) As IntPtr
	Public Declare Function EndPaint Lib "user32" Alias "EndPaint" (ByVal hWnd As IntPtr, ByRef paintStruct As PAINTSTRUCT) As Boolean

#Region "ScrollBarInfo"

	Public Const OBJID_WINDOW As Integer = 0
	Public Const OBJID_SYSMENU As Integer = -1
	Public Const OBJID_TITLEBAR As Integer = -2
	Public Const OBJID_MENU As Integer = -3
	Public Const OBJID_CLIENT As Integer = -4
	Public Const OBJID_VSCROLL As Integer = -5
	Public Const OBJID_HSCROLL As Integer = -6
	Public Const OBJID_SIZEGRIP As Integer = -7
	Public Const OBJID_CARET As Integer = -8
	Public Const OBJID_CURSOR As Integer = -9
	Public Const OBJID_ALERT As Integer = -10
	Public Const OBJID_SOUND As Integer = -11
	Public Const OBJID_QUERYCLASSNAMEIDX As Integer = -12
	Public Const OBJID_NATIVEOM As Integer = -13


	'<StructLayout(LayoutKind.Sequential)>
	'Public Structure SCROLLBARINFO
	'	Public cbSize As Integer
	'	Public rcScrollBar As RECT
	'	Public thumbWidth As Integer
	'	Public thumbTop As Integer
	'	Public thumbBottom As Integer
	'	Public reserved As Integer
	'	Public rgstate As Integer()
	'End Structure
	<StructLayout(LayoutKind.Sequential)>
	Public Structure SCROLLBARINFO
		Public cbSize As Int32
		Public scrollBarRectangle As RECT
		Public dxyLineButton As Int32
		Public xyThumbTop As Int32
		Public xyThumbBottom As Int32
		Public reserved As Int32
		Public scrollbar As Int32
		Public incbtn As Int32
		Public pgup As Int32
		Public thumb As Int32
		Public pgdn As Int32
		Public decbtn As Int32
	End Structure

	Public Shared STATE_SYSTEM_UNAVAILABLE As UInteger = &H1
	Public Shared STATE_SYSTEM_PRESSED As UInteger = &H8
	Public Shared STATE_SYSTEM_INVISIBLE As UInteger = &H8000
	Public Shared STATE_SYSTEM_OFFSCREEN As UInteger = &H10000

	'<DllImport("user32.dll", SetLastError:=True)>
	'Public Shared Function GetScrollBarInfo(ByVal hWnd As IntPtr, ByVal idObject As Integer, ByRef psbi As SCROLLBARINFO) As <MarshalAs(UnmanagedType.Bool)> Boolean
	'End Function
	<DllImport("user32.dll", SetLastError:=True)>
	Public Shared Function GetScrollBarInfo(ByVal hwnd As IntPtr, ByVal idObject As Int32, ByRef psbi As SCROLLBARINFO) As Boolean
	End Function

#End Region

#Region "RichTextBox ScrollInfo"

	'NOTE: This is only usable if the scrollbar is visible.
	Public Declare Function GetScrollInfo Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal n As Integer, ByRef lpScrollInfo As SCROLLINFO) As Integer
	'Declare Function GetScrollInfo Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal fnBar As ScrollBarDirection, ByRef lpsi As SCROLLINFO) As Integer

	<StructLayout(LayoutKind.Sequential)>
	Public Structure SCROLLINFO
		Public cbSize As Integer
		Public fMask As Integer
		Public nMin As Integer
		Public nMax As Integer
		Public nPage As Integer
		Public nPos As Integer
		Public nTrackPos As Integer
	End Structure

	Private Const SIF_RANGE As Long = &H1
	Private Const SIF_PAGE As Long = &H2
	Private Const SIF_POS As Long = &H4
	Private Const SIF_DISABLENOSCROLL As Long = &H8
	Private Const SIF_TRACKPOS As Long = &H10
	Public Const SIF_ALL As Long = (SIF_RANGE Or SIF_PAGE Or SIF_POS Or SIF_TRACKPOS)

	Public Enum ScrollBarType
		SB_HORZ = &H0
		SB_VERT = &H1
		SB_CTL = &H2 'this is used for the (nBar) parameter of the GetScrollInfo function if the (hwnd) parameter is a handle to a ScrollBar
	End Enum

	'Public Shared Function GetScrollBarPos(ByVal lHwnd As IntPtr, ByVal ScrollBar As ScrollBarType) As Integer
	'	Dim lFlag As Integer
	'	Dim sbInfo As SCROLLINFO
	'	Dim lRet As Integer

	'	sbInfo.cbSize = Len(sbInfo)
	'	sbInfo.fMask = SIF_POS
	'	lFlag = ScrollBar

	'	lRet = GetScrollInfo(lHwnd, lFlag, sbInfo)
	'	If lRet > 0 Then
	'		GetScrollBarPos = sbInfo.nPos
	'	End If
	'End Function

	Public Shared Function GetScrollBarMax(ByVal lHwnd As IntPtr, ByVal scrollBarType As ScrollBarType) As Integer
		Dim max As Integer = 0

		Dim sbInfo As SCROLLINFO
		Dim lRet As Integer

		sbInfo.cbSize = Marshal.SizeOf(sbInfo)
		sbInfo.fMask = SIF_ALL

		lRet = GetScrollInfo(lHwnd, scrollBarType, sbInfo)
		If lRet > 0 Then
			max = sbInfo.nMax
		End If

		Return max
	End Function

#End Region

#Region "ScrollBar info - separate functions"

	'NOTE: This is only usable if the scrollbar is visible.
	<DllImport("user32.dll")>
	Public Shared Function GetScrollRange(ByVal hWnd As IntPtr, ByVal nBar As Integer, ByRef lpMinPos As Integer, ByRef lpMaxPos As Integer) As Boolean
	End Function

	'<DllImport("user32.dll")>
	'Public Function GetScrollPos(ByVal hWnd As Integer,
	'						 ByVal nBar As Integer) As Integer
	'End Function

	'Dim scrollMin As Integer = 0
	'Dim scrollMax As Integer = 0

	'If (GetScrollRange(rtb.Handle, SBS_VERT, scrollMin, scrollMax) Then
	'Dim pos As Integer = GetScrollPos(rtb.Handle, SBS_VERT)
	'End If

	Public Declare Function GetScrollPos Lib "user32" Alias "GetScrollPos" (ByVal hWnd As IntPtr, ByVal nBar As Integer) As Integer
	Public Declare Function SetScrollPos Lib "user32" Alias "SetScrollPos" (ByVal hWnd As IntPtr, ByVal nBar As Integer, ByVal nPos As Integer, ByVal bRedraw As Boolean) As Integer

#End Region

	Public Enum SB As UInteger
		SB_LINEUP = 0
		SB_LINELEFT = 0
		SB_LINEDOWN = 1
		SB_LINERIGHT = 1
		SB_PAGEUP = 2
		SB_PAGELEFT = 2
		SB_PAGEDOWN = 3
		SB_PAGERIGHT = 3
		SB_THUMBPOSITION = 4
		SB_THUMBTRACK = 5
		SB_TOP = 6
		SB_LEFT = 6
		SB_BOTTOM = 7
		SB_RIGHT = 7
		SB_ENDSCROLL = 8
	End Enum

	Public Const SB_BOTH As Integer = 3

	Public Declare Function ShowScrollBar Lib "user32" (ByVal hwnd As IntPtr, ByVal wBar As Integer, ByVal bShow As Boolean) As Boolean

#Region "Win32DarkMode"

	Public Enum PreferredAppMode
		[Default]
		AllowDark
		ForceDark
		ForceLight
		Max
	End Enum

	Public Structure HIGHCONTRASTW
		Public cbSize As UInteger
		Public dwFlags As Integer
		Public lpszDefaultScheme As String
	End Structure

	<DllImport("UxTheme.dll", EntryPoint:="#133", CallingConvention:=CallingConvention.Winapi)>
	Public Shared Function AllowDarkModeForWindow(hWnd As IntPtr, allow As Boolean) As Boolean
	End Function

	<DllImport("UxTheme.dll", EntryPoint:="#135", CallingConvention:=CallingConvention.Winapi)>
	Public Shared Function _AllowDarkModeForApp(allow As Boolean) As Boolean
	End Function

	<DllImport("UxTheme.dll", EntryPoint:="#135", CallingConvention:=CallingConvention.Winapi)>
	Public Shared Function SetPreferredAppMode(appMode As PreferredAppMode) As Boolean
	End Function

	<DllImport("UxTheme.dll", EntryPoint:="#137", CallingConvention:=CallingConvention.Winapi)>
	Public Shared Function IsDarkModeAllowedForWindow(hWnd As IntPtr) As Boolean
	End Function

	'<DllImport("dwmapi.dll", SetLastError:=False, ExactSpelling:=True)>
	'Public Shared Function DwmSetWindowAttribute(hwnd As IntPtr, dwAttribute As Integer, <[In]> pvAttribute As IntPtr, cbAttribute As Integer) As Integer
	'End Function
	'======
	<DllImport("DwmApi")>
	Public Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByVal attrValue As Integer(), ByVal attrSize As Integer) As Integer
	End Function

	Public Enum DwmWindowAttribute
		DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19
		DWMWA_USE_IMMERSIVE_DARK_MODE = 20
	End Enum

	<DllImport("User32.dll", CharSet:=CharSet.Auto)>
	Public Shared Function SystemParametersInfo(uiAction As UInteger, uiParam As UInteger, ByRef pvParam As HIGHCONTRASTW, fWinIni As UInteger) As Boolean
	End Function

	<DllImport("UxTheme.dll", EntryPoint:="#132", CallingConvention:=CallingConvention.Winapi)>
	Public Shared Function ShouldAppsUseDarkMode() As Boolean
	End Function

	<DllImport("UxTheme.dll", EntryPoint:="#104", CallingConvention:=CallingConvention.Winapi)>
	Public Shared Sub RefreshImmersiveColorPolicyState()
	End Sub

	Public Shared Function IsHighContrast() As Boolean
		Dim hc As HIGHCONTRASTW = New HIGHCONTRASTW()
		If SystemParametersInfo(&H42, CUInt(Marshal.SizeOf(hc)), hc, 0) Then
			Return hc.dwFlags = &H1
		End If
		Return False
	End Function

	'Public Shared Sub RefreshTitleBarThemeColor(hWnd As IntPtr)
	'	Dim dark As Boolean = False
	'	If IsDarkModeAllowedForWindow(hWnd) And ShouldAppsUseDarkMode() And Not IsHighContrast() Then
	'		dark = True
	'	End If

	'	Dim pvDark As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(dark))
	'	Marshal.StructureToPtr(dark, pvDark, False)
	'	DwmSetWindowAttribute(hWnd, 20, pvDark, Marshal.SizeOf(dark))
	'	Marshal.FreeHGlobal(pvDark)
	'End Sub
	'======
	Public Shared Sub RefreshTitleBarThemeColor(hWnd As IntPtr)
		If IsDarkModeAllowedForWindow(hWnd) And ShouldAppsUseDarkMode() And Not IsHighContrast() Then
			If DwmSetWindowAttribute(hWnd, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, {1}, 4) <> 0 Then
				DwmSetWindowAttribute(hWnd, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, {1}, 4)
			End If
		Else
			DwmSetWindowAttribute(hWnd, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, {0}, 4)
			DwmSetWindowAttribute(hWnd, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, {0}, 4)
		End If

	End Sub

	Public Shared Sub AllowDarkModeForApp(allow As Boolean)
		If Environment.OSVersion.Version.Build < 18362 Then
			_AllowDarkModeForApp(allow)
		Else
			If allow Then
				SetPreferredAppMode(PreferredAppMode.AllowDark)
			Else
				SetPreferredAppMode(PreferredAppMode.Default)
			End If
		End If
	End Sub

	' If on Win10 or later, use the mode that Windows is using.
	Public Shared Function UseWindowsTitleBarThemeColor(hWnd As IntPtr, enable As Boolean) As Boolean
		AllowDarkModeForApp(True)
		RefreshImmersiveColorPolicyState()
		Dim retval As Boolean = AllowDarkModeForWindow(hWnd, enable)
		RefreshTitleBarThemeColor(hWnd)
		Return retval
	End Function

	Public Declare Auto Function SetWindowTheme Lib "uxtheme.dll" (hWnd As IntPtr, pszSubAppName As String, pszSubIdList As String) As Integer

	<DllImport("user32.dll", SetLastError:=True)>
	Public Shared Function GetWindowDC(hWnd As IntPtr) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True)>
	Public Shared Function ReleaseDC(hWnd As IntPtr, hDc As IntPtr) As Boolean
	End Function

	<DllImport("user32.dll")>
	Public Shared Function RealGetWindowClass(ByVal hwnd As IntPtr, ByVal pszType As System.Text.StringBuilder, ByVal cchType As Integer) As Integer
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	Friend Shared Function GetComboBoxInfo(hWnd As IntPtr, ByRef pcbi As COMBOBOXINFO) As Boolean
	End Function

	<StructLayout(LayoutKind.Sequential)>
	Public Structure COMBOBOXINFO
		Public cbSize As Integer
		Public rcItem As Rectangle
		Public rcButton As Rectangle
		Public buttonState As Integer
		Public hwndCombo As IntPtr
		Public hwndEdit As IntPtr
		Public hwndList As IntPtr
		Public Sub Init()
			cbSize = Marshal.SizeOf(Me)
		End Sub
	End Structure

	Public Shared Function GetComboBoxListInternal(cboHandle As IntPtr) As IntPtr
		Dim cbInfo As New COMBOBOXINFO()
		cbInfo.Init()
		GetComboBoxInfo(cboHandle, cbInfo)
		Return cbInfo.hwndList
	End Function

	Friend NotInheritable Class NativeMethods
		<DllImport("dwmapi.dll", EntryPoint:="#127")>
		Friend Shared Sub DwmGetColorizationParameters(ByRef colors As DWMCOLORIZATIONcolors)
		End Sub
	End Class

	Public Structure DWMCOLORIZATIONcolors
		Public ColorizationColor, ColorizationAfterglow, ColorizationColorBalance, ColorizationAfterglowBalance, ColorizationBlurBalance, ColorizationGlassReflectionIntensity, ColorizationOpaqueBlend As UInteger
	End Structure

	Public Shared Function GetWindowColorizationColor(ByVal opaque As Boolean) As Color
		Dim colors As DWMCOLORIZATIONcolors
		NativeMethods.DwmGetColorizationParameters(colors)

		'Dim a As Integer
		'Dim r As Integer
		'Dim g As Integer
		'Dim b As Integer
		'Return Color.FromArgb((Byte)(opaque ? 255 : colors.ColorizationColor >> 24),
		'       (byte)(colors.ColorizationColor >> 16), 
		'       (byte)(colors.ColorizationColor >> 8), 
		'       (byte)colors.ColorizationColor);		
		'If opaque Then
		'	a = 255
		'Else
		'	a = CType(colors.ColorizationColor >> 24, Byte)
		'End If
		'r = CType(colors.ColorizationColor >> 16, Byte)
		'g = CType(colors.ColorizationColor >> 8, Byte)
		'b = CType(colors.ColorizationColor, Byte)
		Dim result As Byte() = BitConverter.GetBytes(colors.ColorizationColor)
		If opaque Then
			result(3) = 255
		End If
		Return Color.FromArgb(result(3), result(2), result(1), result(0))
	End Function


#End Region

	Public Shared Function GetHiWord(wParam As Long) As Integer
		If (wParam And &H80000000) > 0 Then
			Return CInt((wParam \ 65535) - 1)
		Else
			Return CInt(wParam \ 65535)
		End If
	End Function

	Public Shared Function GetLoWord(wParam As Long) As Integer
		If (wParam And &H8000&) > 0 Then
			Return CInt(&H8000 Or (wParam And &H7FFF&))
		Else
			Return CInt(wParam And &HFFFF&)
		End If
	End Function

	Public Shared Function SetWParam(ByVal hiWord As Integer, ByVal loWord As Integer) As Long
		Return hiWord << 16 + loWord
	End Function

	'Public Shared Function ListView_GetVerticalItemSpacing(handle As IntPtr, ByVal viewIsSmallIcon As Boolean) As Integer
	'	Dim spacing As Long = 0
	'	If viewIsSmallIcon Then
	'		spacing = 1
	'	End If
	'	Win32Api.SendMessage(handle, Win32Api.ListViewMessages.LVM_GETITEMSPACING, spacing, IntPtr.Zero)
	'	Return GetLoWord(spacing)
	'End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As ListViewMessages, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
	End Function

	'<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	'Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As ListViewMessages, ByRef wParam As Long, ByVal lParam As IntPtr) As IntPtr
	'End Function

End Class
