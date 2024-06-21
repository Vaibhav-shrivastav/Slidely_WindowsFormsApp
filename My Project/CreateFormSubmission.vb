Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class CreateSubmissionForm
    Private stopwatchRunning As Boolean = False
    Private stopwatchStart As DateTime
    Private elapsed As TimeSpan = TimeSpan.Zero

    Private Sub btnStartStop_Click(sender As Object, e As EventArgs) Handles btnStartStop.Click
        If stopwatchRunning Then
            elapsed += DateTime.Now - stopwatchStart
            stopwatchRunning = False
            btnStartStop.Text = "Start"
            Timer1.Stop()
        Else
            stopwatchStart = DateTime.Now
            stopwatchRunning = True
            btnStartStop.Text = "Stop"
            Timer1.Start()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If stopwatchRunning Then
            lblStopwatch.Text = (elapsed + (DateTime.Now - stopwatchStart)).ToString("hh\:mm\:ss")
        End If
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim submission As New Submission With {
            .name = txtName.Text,
            .email = txtEmail.Text,
            .phone = txtPhone.Text,
            .github_link = txtGithub.Text,
            .stopwatch_time = lblStopwatch.Text
        }

        Dim json As String = JsonConvert.SerializeObject(submission)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        Using client As New HttpClient()
            Dim response = Await client.PostAsync("http://localhost:3000/submit", content)
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission successful!")
            Else
                MessageBox.Show("Submission failed.")
            End If
        End Using
    End Sub
End Class

Public Class Submission
    Public Property name As String
    Public Property email As String
    Public Property phone As String
    Public Property github_link As String
    Public Property stopwatch_time As String
End Class
