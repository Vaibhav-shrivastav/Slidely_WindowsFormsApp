Imports System.Net.Http
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Private currentIndex As Integer = 0
    Private submissions As List(Of Submission)

    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadSubmissions()
        DisplaySubmission()
    End Sub

    Private Async Function LoadSubmissions() As Task
        Using client As New HttpClient()
            Dim response = Await client.GetStringAsync("http://localhost:3000/read")
            submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(response)
        End Using
    End Function

    Private Sub DisplaySubmission()
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim submission = submissions(currentIndex)
            lblSubmissionDetails.Text = $"Name: {submission.name}" & vbCrLf &
                                        $"Email: {submission.email}" & vbCrLf &
                                        $"Phone: {submission.phone}" & vbCrLf &
                                        $"GitHub: {submission.github_link}" & vbCrLf &
                                        $"Time: {submission.stopwatch_time}"
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission()
        End If
    End Sub
End Class

Public Class Submission
    Public Property name As String
    Public Property email As String
    Public Property phone As String
    Public Property github_link As String
    Public Property stopwatch_time As String
End Class
