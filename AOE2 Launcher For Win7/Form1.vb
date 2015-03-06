Public Class Form1

    Public Declare Function TerminateProcess Lib "kernel32" (ByVal hProcess As IntPtr, ByVal uExitCode As UInteger) As Integer


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.TopMost = True

        If My.Computer.FileSystem.FileExists("age2_x1\age2_x1.exe") = True Then
            Button1.Visible = True
            Button2.Text = "Start (1.0)"
            Button2.Height = Button2.Height - 54
            Button2.Top = Button2.Top + 54
        End If

        CheckBox1.Checked = My.Settings.autostart
        CheckBox2.Checked = My.Settings.fix
        CheckBox3.Checked = My.Settings.close
        If CheckBox1.Checked = False Then
            autos.Enabled = False

        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If ProcessesRunning("age2_x1") = 1 Then
            Dim response As MsgBoxResult
            response = MsgBox("The game is still running! " & vbNewLine & "Do you really want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "=_=")
            If response = MsgBoxResult.Yes Then
                Me.Dispose()
            ElseIf response = MsgBoxResult.No Then
                e.Cancel = True
                Exit Sub
            End If
        End If

    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Home Then
            Button3.PerformClick()

            End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        My.Settings.Save()
        System.Diagnostics.Process.Start("age2_x1\age2_x1.exe")

        If CheckBox2.Checked = True Then
            Timer1.Enabled = True
            For Each ObjPro As Process In Process.GetProcessesByName("EXPLORER")
                Do Until TerminateProcess(ObjPro.Handle, 1)
                    Application.DoEvents()
                Loop
            Next

        End If


    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            My.Settings.autostart = True

        Else
            My.Settings.autostart = False

        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            My.Settings.fix = True
        Else
            My.Settings.fix = False
        End If
    End Sub

    Public Function ProcessesRunning(ByVal ProcessName As String) As Integer
        Try
            Return Process.GetProcessesByName(ProcessName).GetUpperBound(0) + 1
        Catch
            Return 0
        End Try
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If ProcessesRunning("age2_x1") = 0 Then
            If ProcessesRunning("explorer") = 0 Then
                System.Diagnostics.Process.Start("explorer")
            End If

            Timer1.Stop()
            If CheckBox3.Checked = True Then
                End
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        System.Diagnostics.Process.Start("age2_x1.exe")

        If CheckBox2.Checked = True Then
            Timer1.Enabled = True
            For Each ObjPro As Process In Process.GetProcessesByName("EXPLORER")
                Do Until TerminateProcess(ObjPro.Handle, 1)
                    Application.DoEvents()
                Loop
            Next

        End If
    End Sub

    Private Sub autos_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles autos.Tick

        If My.Computer.FileSystem.FileExists("age2_x1\age2_x1.exe") = True Then
            Button1.PerformClick()
            autos.Stop()
        Else
            Button2.PerformClick()
            autos.Stop()
        End If
        My.Settings.Save()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ProcessesRunning("explorer") = 0 Then
            System.Diagnostics.Process.Start("explorer")
        ElseIf ProcessesRunning("age2_x1") = 0 Then
            Dim response As MsgBoxResult
            response = MsgBox("The game is not running." & vbNewLine & "Do you really want to use the function?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "~_~")
            If response = MsgBoxResult.Yes Then
                If ProcessesRunning("explorer") = 1 Then
                    For Each ObjPro As Process In Process.GetProcessesByName("EXPLORER")
                        Do Until TerminateProcess(ObjPro.Handle, 1)
                            Application.DoEvents()
                        Loop
                    Next
                End If
            ElseIf response = MsgBoxResult.No Then
                Return
                Exit Sub
            End If
        Else
            If ProcessesRunning("explorer") = 1 Then
                For Each ObjPro As Process In Process.GetProcessesByName("EXPLORER")
                    Do Until TerminateProcess(ObjPro.Handle, 1)
                        Application.DoEvents()
                    Loop
                Next
            End If
        End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("http://louislam.coms.hk")
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            My.Settings.close = True
        Else
            My.Settings.close = False
        End If
        My.Settings.Save()
    End Sub
End Class

