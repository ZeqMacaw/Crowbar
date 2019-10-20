Imports System.IO

Module Main

	' Entry point of application.
	Public Function Main() As Integer
		'' Create a job with JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE flag, so that all processes 
		''	(e.g. HLMV called by Crowbar) associated with the job 
		''	terminate when the last handle to the job is closed.
		'' From MSDN: By default, processes created using CreateProcess by a process associated with a job 
		''	are associated with the job.
		'TheJob = New WindowsJob()
		'TheJob.AddProcess(Process.GetCurrentProcess().Handle())

		Dim anExceptionHandler As New AppExceptionHandler()
		AddHandler Application.ThreadException, AddressOf anExceptionHandler.Application_ThreadException
		' Set the unhandled exception mode to call Application.ThreadException event for all Windows Forms exceptions.
		Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)

		'Dim appUniqueIdentifier As String
		'Dim appMutex As System.Threading.Mutex
		'appUniqueIdentifier = Application.ExecutablePath.Replace("\", "_")
		'appMutex = New System.Threading.Mutex(False, appUniqueIdentifier)
		'If appMutex.WaitOne(0, False) = False Then
		'	appMutex.Close()
		'	appMutex = Nothing
		'	'MessageBox.Show("Another instance is already running!")
		'	Win32Api.PostMessage(CType(Win32Api.WindowsMessages.HWND_BROADCAST, IntPtr), appUniqueWindowsMessageIdentifier, IntPtr.Zero, IntPtr.Zero)
		'Else
		'NOTE: Use the Windows Vista and later visual styles (such as rounded buttons).
		Application.EnableVisualStyles()
		'NOTE: Needed for keeping Label and Button text rendering correctly.
		Application.SetCompatibleTextRenderingDefault(False)

		AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf ResolveAssemblies

		TheApp = New App()
		'Try
		TheApp.Init()
		If TheApp.Settings.AppIsSingleInstance Then
			SingleInstanceApplication.Run(New MainForm(), AddressOf StartupNextInstanceEventHandler)
		Else
			Windows.Forms.Application.Run(MainForm)
		End If
		'Catch e As Exception
		'	MsgBox(e.Message)
		'Finally
		'End Try
		TheApp.Dispose()
		'End If

		Return 0
	End Function

	Private Sub StartupNextInstanceEventHandler(ByVal sender As Object, ByVal e As SingleInstanceEventArgs)
		If e.MainForm.WindowState = FormWindowState.Minimized Then
			e.MainForm.WindowState = FormWindowState.Normal
		End If
		e.MainForm.Activate()
		CType(e.MainForm, MainForm).Startup(e.CommandLine)
	End Sub

	Private Function ResolveAssemblies(sender As Object, e As System.ResolveEventArgs) As Reflection.Assembly
		Dim desiredAssembly As Reflection.AssemblyName = New Reflection.AssemblyName(e.Name)
		'If desiredAssembly.Name = "SevenZipSharp" Then
		'	Return Reflection.Assembly.Load(My.Resources.SevenZipSharp)
		'ElseIf desiredAssembly.Name = "Steamworks.NET" Then
		'	Return Reflection.Assembly.Load(My.Resources.Steamworks_NET)
		'Else
		'	Return Nothing
		'End If
		If desiredAssembly.Name = "Steamworks.NET" Then
			Return Reflection.Assembly.Load(My.Resources.Steamworks_NET)
		Else
			Return Nothing
		End If
	End Function

	'Public TheJob As WindowsJob
	Public TheApp As App

End Module
