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

	Public Enum WindowStyles As UInteger
		WS_BORDER = &H800000
		WS_CAPTION = &HC00000
		WS_CHILD = &H40000000
		WS_CLIPCHILDREN = &H2000000
		WS_CLIPSIBLINGS = &H4000000
		WS_DISABLED = &H8000000
		WS_DLGFRAME = &H400000
		WS_GROUP = &H20000
		WS_HSCROLL = &H100000
		WS_MAXIMIZE = &H1000000
		WS_MAXIMIZEBOX = &H10000
		WS_MINIMIZE = &H20000000
		WS_MINIMIZEBOX = &H20000
		WS_OVERLAPPED = &H0
		WS_OVERLAPPEDWINDOW = WS_OVERLAPPED Or WS_CAPTION Or WS_SYSMENU Or WS_SIZEFRAME Or WS_MINIMIZEBOX Or WS_MAXIMIZEBOX
		WS_POPUP = &H80000000UI
		WS_POPUPWINDOW = WS_POPUP Or WS_BORDER Or WS_SYSMENU
		WS_SIZEFRAME = &H40000
		WS_SYSMENU = &H80000
		WS_TABSTOP = &H10000
		WS_VISIBLE = &H10000000
		WS_VSCROLL = &H200000
	End Enum

	Public Enum WindowStylesEx As UInteger
		WS_EX_ACCEPTFILES = &H10
		WS_EX_APPWINDOW = &H40000
		WS_EX_CLIENTEDGE = &H200
		WS_EX_COMPOSITED = &H2000000
		WS_EX_CONTEXTHELP = &H400
		WS_EX_CONTROLPARENT = &H10000
		WS_EX_DLGMODALFRAME = &H1
		WS_EX_LAYERED = &H80000
		WS_EX_LAYOUTRTL = &H400000
		WS_EX_LEFT = &H0
		WS_EX_LEFTSCROLLBAR = &H4000
		WS_EX_LTRREADING = &H0
		WS_EX_MDICHILD = &H40
		WS_EX_NOACTIVATE = &H8000000
		WS_EX_NOINHERITLAYOUT = &H100000
		WS_EX_NOPARENTNOTIFY = &H4
		WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE Or WS_EX_CLIENTEDGE
		WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE Or WS_EX_TOOLWINDOW Or WS_EX_TOPMOST
		WS_EX_RIGHT = &H1000
		WS_EX_RIGHTSCROLLBAR = &H0
		WS_EX_RTLREADING = &H2000
		WS_EX_STATICEDGE = &H20000
		WS_EX_TOOLWINDOW = &H80
		WS_EX_TOPMOST = &H8
		WS_EX_TRANSPARENT = &H20
		WS_EX_WINDOWEDGE = &H100
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

	Public Enum WindowLongFlags
		GWL_EXSTYLE = -20
		GWLP_HINSTANCE = -6
		GWLP_HWNDPARENT = -8
		GWL_ID = -12
		GWL_STYLE = -16
		GWL_USERDATA = -21
		GWL_WNDPROC = -4
		DWLP_USER = &H8
		DWLP_MSGRESULT = &H0
		DWLP_DLGPROC = &H4
	End Enum

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

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)>
	Public Shared Function SetParent(hWndChild As IntPtr, hWndNewParent As IntPtr) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True)>
	Public Shared Function GetWindowInfo(ByVal hwnd As IntPtr, ByRef pwi As WINDOWINFO) As Boolean
	End Function

	<DllImport("user32.dll", SetLastError:=True)>
	Public Shared Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, ByRef lpdwProcessId As IntPtr) As UInt32
	End Function

	<DllImport("user32.dll")>
	Public Shared Function AttachThreadInput(ByVal idAttach As System.UInt32, ByVal idAttachTo As System.UInt32, ByVal fAttach As Boolean) As Boolean
	End Function

	<DllImport("user32.dll")>
	Public Shared Function MoveWindow(ByVal hWnd As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal bRepaint As Boolean) As Boolean
	End Function

	<DllImport("user32.dll", EntryPoint:="SetWindowLong")>
	Public Shared Function SetWindowLong(ByVal hWnd As IntPtr, <MarshalAs(UnmanagedType.I4)> nIndex As WindowLongFlags, ByVal dwNewLong As Integer) As Integer
	End Function

	'<System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint:="SetWindowLong")>
	'Private Shared Function SetWindowLong32(ByVal hWnd As IntPtr, <MarshalAs(UnmanagedType.I4)> nIndex As WindowLongFlags, ByVal dwNewLong As Integer) As Integer
	'End Function

	'<System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint:="SetWindowLongPtr")>
	'Private Shared Function SetWindowLongPtr64(ByVal hWnd As IntPtr, <MarshalAs(UnmanagedType.I4)> nIndex As WindowLongFlags, ByVal dwNewLong As IntPtr) As IntPtr
	'End Function

	'Public Shared Function SetWindowLong(ByVal hWnd As IntPtr, nIndex As WindowLongFlags, ByVal dwNewLong As IntPtr) As IntPtr
	'	If IntPtr.Size = 8 Then
	'		Return SetWindowLongPtr64(hWnd, nIndex, dwNewLong)
	'	Else
	'		Return New IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32))
	'	End If
	'End Function

	<DllImport("user32.dll")>
	Public Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

	Public Shared ReadOnly HWND_BOTTOM As New IntPtr(1)
	Public Shared ReadOnly HWND_NOTOPMOST As New IntPtr(-2)
	Public Shared ReadOnly HWND_TOP As New IntPtr(0)
	Public Shared ReadOnly HWND_TOPMOST As New IntPtr(-1)

	<Flags>
	Public Enum SetWindowPosFlags As UInteger
		''' <summary>If the calling thread and the thread that owns the window are attached to different input queues,
		''' the system posts the request to the thread that owns the window. This prevents the calling thread from
		''' blocking its execution while other threads process the request.</summary>
		''' <remarks>SWP_ASYNCWINDOWPOS</remarks>
		SynchronousWindowPosition = &H4000
		''' <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
		''' <remarks>SWP_DEFERERASE</remarks>
		DeferErase = &H2000
		''' <summary>Draws a frame (defined in the window's class description) around the window.</summary>
		''' <remarks>SWP_DRAWFRAME</remarks>
		DrawFrame = &H20
		''' <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
		''' the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
		''' is sent only when the window's size is being changed.</summary>
		''' <remarks>SWP_FRAMECHANGED</remarks>
		FrameChanged = &H20
		''' <summary>Hides the window.</summary>
		''' <remarks>SWP_HIDEWINDOW</remarks>
		HideWindow = &H80
		''' <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
		''' top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
		''' parameter).</summary>
		''' <remarks>SWP_NOACTIVATE</remarks>
		DoNotActivate = &H10
		''' <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
		''' contents of the client area are saved and copied back into the client area after the window is sized or
		''' repositioned.</summary>
		''' <remarks>SWP_NOCOPYBITS</remarks>
		DoNotCopyBits = &H100
		''' <summary>Retains the current position (ignores X and Y parameters).</summary>
		''' <remarks>SWP_NOMOVE</remarks>
		IgnoreMove = &H2
		''' <summary>Does not change the owner window's position in the Z order.</summary>
		''' <remarks>SWP_NOOWNERZORDER</remarks>
		DoNotChangeOwnerZOrder = &H200
		''' <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
		''' the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
		''' window uncovered as a result of the window being moved. When this flag is set, the application must
		''' explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
		''' <remarks>SWP_NOREDRAW</remarks>
		DoNotRedraw = &H8
		''' <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
		''' <remarks>SWP_NOREPOSITION</remarks>
		DoNotReposition = &H200
		''' <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
		''' <remarks>SWP_NOSENDCHANGING</remarks>
		DoNotSendChangingEvent = &H400
		''' <summary>Retains the current size (ignores the cx and cy parameters).</summary>
		''' <remarks>SWP_NOSIZE</remarks>
		IgnoreResize = &H1
		''' <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
		''' <remarks>SWP_NOZORDER</remarks>
		IgnoreZOrder = &H4
		''' <summary>Displays the window.</summary>
		''' <remarks>SWP_SHOWWINDOW</remarks>
		ShowWindow = &H40
	End Enum

	<DllImport("user32.dll", SetLastError:=True)>
	Public Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As SetWindowPosFlags) As Boolean
	End Function

	Enum ShowWindowCommands As Integer
		''' <summary>
		''' Hides the window and activates another window.
		''' </summary>
		Hide = 0
		''' <summary>
		''' Activates and displays a window. If the window is minimized or
		''' maximized, the system restores it to its original size and position.
		''' An application should specify this flag when displaying the window
		''' for the first time.
		''' </summary>
		Normal = 1
		''' <summary>
		''' Activates the window and displays it as a minimized window.
		''' </summary>
		ShowMinimized = 2
		''' <summary>
		''' Maximizes the specified window.
		''' </summary>
		Maximize = 3
		' is this the right value?
		''' <summary>
		''' Activates the window and displays it as a maximized window.
		''' </summary>      
		ShowMaximized = 3
		''' <summary>
		''' Displays a window in its most recent size and position. This value
		''' is similar to <see cref="Win32.ShowWindowCommands.Normal"/>, except
		''' the window is not actived.
		''' </summary>
		ShowNoActivate = 4
		''' <summary>
		''' Activates the window and displays it in its current size and position.
		''' </summary>
		Show = 5
		''' <summary>
		''' Minimizes the specified window and activates the next top-level
		''' window in the Z order.
		''' </summary>
		Minimize = 6
		''' <summary>
		''' Displays the window as a minimized window. This value is similar to
		''' <see cref="Win32.ShowWindowCommands.ShowMinimized"/>, except the
		''' window is not activated.
		''' </summary>
		ShowMinNoActive = 7
		''' <summary>
		''' Displays the window in its current size and position. This value is
		''' similar to <see cref="Win32.ShowWindowCommands.Show"/>, except the
		''' window is not activated.
		''' </summary>
		ShowNA = 8
		''' <summary>
		''' Activates and displays the window. If the window is minimized or
		''' maximized, the system restores it to its original size and position.
		''' An application should specify this flag when restoring a minimized window.
		''' </summary>
		Restore = 9
		''' <summary>
		''' Sets the show state based on the SW_* value specified in the
		''' STARTUPINFO structure passed to the CreateProcess function by the
		''' program that started the application.
		''' </summary>
		ShowDefault = 10
		''' <summary>
		'''  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
		''' that owns the window is not responding. This flag should only be
		''' used when minimizing windows from a different thread.
		''' </summary>
		ForceMinimize = 11
	End Enum

	<DllImport("user32.dll")>
	Public Shared Function ShowWindow(hWnd As IntPtr, <MarshalAs(UnmanagedType.I4)> nCmdShow As ShowWindowCommands) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

	<DllImport("user32.dll")>
	Public Shared Function UpdateWindow(ByVal hWnd As IntPtr) As Boolean
	End Function

	Public Enum WndMsg
		WM_ACTIVATE = &H6
		WM_ACTIVATEAPP = &H1C
		WM_AFXFIRST = &H360
		WM_AFXLAST = &H37F
		WM_APP = &H8000
		WM_ASKCBFORMATNAME = &H30C
		WM_CANCELJOURNAL = &H4B
		WM_CANCELMODE = &H1F
		WM_CAPTURECHANGED = &H215
		WM_CHANGECBCHAIN = &H30D
		WM_CHANGEUISTATE = &H127
		WM_CHAR = &H102
		WM_CHARTOITEM = &H2F
		WM_CHILDACTIVATE = &H22
		WM_CLEAR = &H303
		WM_CLOSE = &H10
		WM_COMMAND = &H111
		WM_COMPACTING = &H41
		WM_COMPAREITEM = &H39
		WM_CONTEXTMENU = &H7B
		WM_COPY = &H301
		WM_COPYDATA = &H4A
		WM_CREATE = &H1
		WM_CTLCOLORBTN = &H135
		WM_CTLCOLORDLG = &H136
		WM_CTLCOLOREDIT = &H133
		WM_CTLCOLORLISTBOX = &H134
		WM_CTLCOLORMSGBOX = &H132
		WM_CTLCOLORSCROLLBAR = &H137
		WM_CTLCOLORSTATIC = &H138
		WM_CUT = &H300
		WM_DEADCHAR = &H103
		WM_DELETEITEM = &H2D
		WM_DESTROY = &H2
		WM_DESTROYCLIPBOARD = &H307
		WM_DEVICECHANGE = &H219
		WM_DEVMODECHANGE = &H1B
		WM_DISPLAYCHANGE = &H7E
		WM_DRAWCLIPBOARD = &H308
		WM_DRAWITEM = &H2B
		WM_DROPFILES = &H233
		WM_ENABLE = &HA
		WM_ENDSESSION = &H16
		WM_ENTERIDLE = &H121
		WM_ENTERMENULOOP = &H211
		WM_ENTERSIZEMOVE = &H231
		WM_ERASEBKGND = &H14
		WM_EXITMENULOOP = &H212
		WM_EXITSIZEMOVE = &H232
		WM_FONTCHANGE = &H1D
		WM_GETDLGCODE = &H87
		WM_GETFONT = &H31
		WM_GETHOTKEY = &H33
		WM_GETICON = &H7F
		WM_GETMINMAXINFO = &H24
		WM_GETOBJECT = &H3D
		WM_GETTEXT = &HD
		WM_GETTEXTLENGTH = &HE
		WM_HANDHELDFIRST = &H358
		WM_HANDHELDLAST = &H35F
		WM_HELP = &H53
		WM_HOTKEY = &H312
		WM_HSCROLL = &H114
		WM_HSCROLLCLIPBOARD = &H30E
		WM_ICONERASEBKGND = &H27
		WM_IME_CHAR = &H286
		WM_IME_COMPOSITION = &H10F
		WM_IME_COMPOSITIONFULL = &H284
		WM_IME_CONTROL = &H283
		WM_IME_ENDCOMPOSITION = &H10E
		WM_IME_KEYDOWN = &H290
		WM_IME_KEYLAST = &H10F
		WM_IME_KEYUP = &H291
		WM_IME_NOTIFY = &H282
		WM_IME_REQUEST = &H288
		WM_IME_SELECT = &H285
		WM_IME_SETCONTEXT = &H281
		WM_IME_STARTCOMPOSITION = &H10D
		WM_INITDIALOG = &H110
		WM_INITMENU = &H116
		WM_INITMENUPOPUP = &H117
		WM_INPUTLANGCHANGE = &H51
		WM_INPUTLANGCHANGEREQUEST = &H50
		WM_KEYDOWN = &H100
		WM_KEYFIRST = &H100
		WM_KEYLAST = &H108
		WM_KEYUP = &H101
		WM_KILLFOCUS = &H8
		WM_LBUTTONDBLCLK = &H203
		WM_LBUTTONDOWN = &H201
		WM_LBUTTONUP = &H202
		WM_MBUTTONDBLCLK = &H209
		WM_MBUTTONDOWN = &H207
		WM_MBUTTONUP = &H208
		WM_MDIACTIVATE = &H222
		WM_MDICASCADE = &H227
		WM_MDICREATE = &H220
		WM_MDIDESTROY = &H221
		WM_MDIGETACTIVE = &H229
		WM_MDIICONARRANGE = &H228
		WM_MDIMAXIMIZE = &H225
		WM_MDINEXT = &H224
		WM_MDIREFRESHMENU = &H234
		WM_MDIRESTORE = &H223
		WM_MDISETMENU = &H230
		WM_MDITILE = &H226
		WM_MEASUREITEM = &H2C
		WM_MENUCHAR = &H120
		WM_MENUCOMMAND = &H126
		WM_MENUDRAG = &H123
		WM_MENUGETOBJECT = &H124
		WM_MENURBUTTONUP = &H122
		WM_MENUSELECT = &H11F
		WM_MOUSEACTIVATE = &H21
		WM_MOUSEFIRST = &H200
		WM_MOUSEHOVER = &H2A1
		WM_MOUSELAST = &H20D
		WM_MOUSELEAVE = &H2A3
		WM_MOUSEMOVE = &H200
		WM_MOUSEWHEEL = &H20A
		WM_MOUSEHWHEEL = &H20E
		WM_MOVE = &H3
		WM_MOVING = &H216
		WM_NCACTIVATE = &H86
		WM_NCCALCSIZE = &H83
		WM_NCCREATE = &H81
		WM_NCDESTROY = &H82
		WM_NCHITTEST = &H84
		WM_NCLBUTTONDBLCLK = &HA3
		WM_NCLBUTTONDOWN = &HA1
		WM_NCLBUTTONUP = &HA2
		WM_NCMBUTTONDBLCLK = &HA9
		WM_NCMBUTTONDOWN = &HA7
		WM_NCMBUTTONUP = &HA8
		WM_NCMOUSEHOVER = &H2A0
		WM_NCMOUSELEAVE = &H2A2
		WM_NCMOUSEMOVE = &HA0
		WM_NCPAINT = &H85
		WM_NCRBUTTONDBLCLK = &HA6
		WM_NCRBUTTONDOWN = &HA4
		WM_NCRBUTTONUP = &HA5
		WM_NCXBUTTONDBLCLK = &HAD
		WM_NCXBUTTONDOWN = &HAB
		WM_NCXBUTTONUP = &HAC
		WM_NCUAHDRAWCAPTION = &HAE
		WM_NCUAHDRAWFRAME = &HAF
		WM_NEXTDLGCTL = &H28
		WM_NEXTMENU = &H213
		WM_NOTIFY = &H4E
		WM_NOTIFYFORMAT = &H55
		WM_NULL = &H0
		WM_PAINT = &HF
		WM_PAINTCLIPBOARD = &H309
		WM_PAINTICON = &H26
		WM_PALETTECHANGED = &H311
		WM_PALETTEISCHANGING = &H310
		WM_PARENTNOTIFY = &H210
		WM_PASTE = &H302
		WM_PENWINFIRST = &H380
		WM_PENWINLAST = &H38F
		WM_POWER = &H48
		WM_POWERBROADCAST = &H218
		WM_PRINT = &H317
		WM_PRINTCLIENT = &H318
		WM_QUERYDRAGICON = &H37
		WM_QUERYENDSESSION = &H11
		WM_QUERYNEWPALETTE = &H30F
		WM_QUERYOPEN = &H13
		WM_QUEUESYNC = &H23
		WM_QUIT = &H12
		WM_RBUTTONDBLCLK = &H206
		WM_RBUTTONDOWN = &H204
		WM_RBUTTONUP = &H205
		WM_RENDERALLFORMATS = &H306
		WM_RENDERFORMAT = &H305
		WM_SETCURSOR = &H20
		WM_SETFOCUS = &H7
		WM_SETFONT = &H30
		WM_SETHOTKEY = &H32
		WM_SETICON = &H80
		WM_SETREDRAW = &HB
		WM_SETTEXT = &HC
		WM_SETTINGCHANGE = &H1A
		WM_SHOWWINDOW = &H18
		WM_SIZE = &H5
		WM_SIZECLIPBOARD = &H30B
		WM_SIZING = &H214
		WM_SPOOLERSTATUS = &H2A
		WM_STYLECHANGED = &H7D
		WM_STYLECHANGING = &H7C
		WM_SYNCPAINT = &H88
		WM_SYSCHAR = &H106
		WM_SYSCOLORCHANGE = &H15
		WM_SYSCOMMAND = &H112
		WM_SYSDEADCHAR = &H107
		WM_SYSKEYDOWN = &H104
		WM_SYSKEYUP = &H105
		WM_TCARD = &H52
		WM_TIMECHANGE = &H1E
		WM_TIMER = &H113
		WM_UNDO = &H304
		WM_UNINITMENUPOPUP = &H125
		WM_USER = &H400
		WM_USERCHANGED = &H54
		WM_VKEYTOITEM = &H2E
		WM_VSCROLL = &H115
		WM_VSCROLLCLIPBOARD = &H30A
		WM_WINDOWPOSCHANGED = &H47
		WM_WINDOWPOSCHANGING = &H46
		WM_WININICHANGE = &H1A
		WM_XBUTTONDBLCLK = &H20D
		WM_XBUTTONDOWN = &H20B
		WM_XBUTTONUP = &H20C
	End Enum

	''' <summary>Send message to a window (platform invoke)</summary>
	''' <param name="hWnd">Window handle to send to</param>
	''' <param name="msg">Message</param>
	''' <param name="wParam">wParam</param>
	''' <param name="lParam">lParam</param>
	''' <returns>Zero if failure, otherwise non-zero</returns>
	<DllImport("user32.dll", SetLastError:=True)>
	Public Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal Msg As WndMsg, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As WndMsg, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As WndMsg, ByVal wParam As IntPtr, ByVal lParam As LV_ITEM) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As WndMsg, ByVal wParam As IntPtr, ByVal lParam As System.Text.StringBuilder) As IntPtr
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
