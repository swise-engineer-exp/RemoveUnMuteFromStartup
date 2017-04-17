Imports System.Threading

Public Class Form1

    Const StartupFolder As String = "C:\Users\g5_User\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\" 'SAW 160825 1107
    Const g5AppPath As String = "C:\Program Files\Schulmerich Carillons, LLC\SCL_g5\"
    Const g5App As String = "SCL_g5.exe"

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Remove the nircmd "mutesysvolume 0" to the Startup Folder

        Try
            If My.Computer.FileSystem.FileExists(g5AppPath + "nircmd.exe") = True Then 'SAW 160825 1351
                My.Computer.FileSystem.DeleteFile(g5AppPath + "nircmd.exe")
            End If
            If My.Computer.FileSystem.FileExists(StartupFolder + "nircmd - Shortcut.lnk") = True Then 'SAW 160825 1351
                My.Computer.FileSystem.DeleteFile(StartupFolder + "nircmd - Shortcut.lnk")
            End If
        Catch ex As Exception
            TextBox1.Visible = True
            TextBox1.Text = ("Undo Failed:" + vbNewLine + vbNewLine + ex.Message)
        End Try
        'Start the Thread that will Launch the g5
        Dim Start_G5 As New System.Threading.Thread(AddressOf G5_Launcher)
        Start_G5.Priority = ThreadPriority.Normal
        Start_G5.Start()

        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub G5_Launcher()
        Dim g5Exe As String = ""

        For Each foundFile As String In My.Computer.FileSystem.GetFiles _
              (g5AppPath, FileIO.SearchOption.SearchAllSubDirectories, g5App)
            g5Exe = foundFile
        Next
        Thread.Sleep(2000)
        Process.Start(g5Exe)
    End Sub

End Class
