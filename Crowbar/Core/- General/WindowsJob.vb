Imports System.Runtime.InteropServices

Public Enum JobObjectInfoType
	AssociateCompletionPortInformation = 7
	BasicLimitInformation = 2
	BasicUIRestrictions = 4
	EndOfJobTimeInformation = 6
	ExtendedLimitInformation = 9
	SecurityLimitInformation = 5
	GroupInformation = 11
End Enum

<StructLayout(LayoutKind.Sequential)> _
Public Structure SECURITY_ATTRIBUTES
	Public nLength As Integer
	Public lpSecurityDescriptor As IntPtr
	Public bInheritHandle As Integer
End Structure

<StructLayout(LayoutKind.Sequential)> _
Structure JOBOBJECT_BASIC_LIMIT_INFORMATION
	Public PerProcessUserTimeLimit As Int64
	Public PerJobUserTimeLimit As Int64
	Public LimitFlags As Int16
	Public MinimumWorkingSetSize As UInt32
	Public MaximumWorkingSetSize As UInt32
	Public ActiveProcessLimit As Int16
	Public Affinity As Int64
	Public PriorityClass As Int16
	Public SchedulingClass As Int16
End Structure

<StructLayout(LayoutKind.Sequential)> _
Structure IO_COUNTERS
	Public ReadOperationCount As UInt64
	Public WriteOperationCount As UInt64
	Public OtherOperationCount As UInt64
	Public ReadTransferCount As UInt64
	Public WriteTransferCount As UInt64
	Public OtherTransferCount As UInt64
End Structure

<StructLayout(LayoutKind.Sequential)> _
Structure JOBOBJECT_EXTENDED_LIMIT_INFORMATION
	Public BasicLimitInformation As JOBOBJECT_BASIC_LIMIT_INFORMATION
	Public IoInfo As IO_COUNTERS
	Public ProcessMemoryLimit As UInt32
	Public JobMemoryLimit As UInt32
	Public PeakProcessMemoryUsed As UInt32
	Public PeakJobMemoryUsed As UInt32
End Structure

Public Class WindowsJob
	Implements IDisposable

	<DllImport("kernel32.dll", CharSet:=CharSet.Unicode)> _
	Private Shared Function CreateJobObject(ByVal a As Object, ByVal lpName As String) As IntPtr
	End Function

	<DllImport("kernel32.dll")> _
	Private Shared Function SetInformationJobObject(ByVal hJob As IntPtr, ByVal infoType As JobObjectInfoType, ByVal lpJobObjectInfo As IntPtr, ByVal cbJobObjectInfoLength As UInteger) As Boolean
	End Function

	<DllImport("kernel32.dll", SetLastError:=True)> _
	Private Shared Function AssignProcessToJobObject(ByVal job As IntPtr, ByVal process As IntPtr) As Boolean
	End Function

	Private m_handle As IntPtr
	Private m_disposed As Boolean = False

	Public Sub New()
		m_handle = CreateJobObject(Nothing, Nothing)

		Dim info As New JOBOBJECT_BASIC_LIMIT_INFORMATION()
		info.LimitFlags = &H2000

		Dim extendedInfo As New JOBOBJECT_EXTENDED_LIMIT_INFORMATION()
		extendedInfo.BasicLimitInformation = info

		Dim length As Integer = Marshal.SizeOf(GetType(JOBOBJECT_EXTENDED_LIMIT_INFORMATION))
		Dim extendedInfoPtr As IntPtr = Marshal.AllocHGlobal(length)
		Marshal.StructureToPtr(extendedInfo, extendedInfoPtr, False)

		If Not SetInformationJobObject(m_handle, JobObjectInfoType.ExtendedLimitInformation, extendedInfoPtr, CUInt(length)) Then
			Throw New Exception(String.Format("Unable to set information.  Error: {0}", Marshal.GetLastWin32Error()))
		End If
	End Sub

#Region "IDisposable Members"

	Public Sub Dispose() Implements System.IDisposable.Dispose
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

#End Region

	Private Sub Dispose(ByVal disposing As Boolean)
		If m_disposed Then
			Exit Sub
		End If

		If disposing Then
		End If

		Close()
		m_disposed = True
	End Sub

	Public Sub Close()
		Win32Api.CloseHandle(m_handle)
		m_handle = IntPtr.Zero
	End Sub

	Public Function AddProcess(ByVal handle As IntPtr) As Boolean
		Return AssignProcessToJobObject(m_handle, handle)
	End Function

End Class
