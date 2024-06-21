Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
    Dim viewForm As New ViewSubmissionsForm()
    viewForm.Show()
End Sub

Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateSubmission.Click
    Dim createForm As New CreateSubmissionForm()
    createForm.Show()
End Sub
