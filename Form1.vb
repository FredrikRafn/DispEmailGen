Imports System.IO
Imports System.Text

Public Class Form1
    Public amount As Integer = 0
    Private rdm As New Random

    Private Function GetRandom(max As Integer) As Integer
        'rdm.Next(minValue, maxValue) returns a random number greater than or equal to minValue and less than maxValue.
        Return rdm.Next(0, max)
    End Function

    Function RandomString(r As Random)
        Dim s As String = "0123456789"
        Dim sb As New StringBuilder
        For i As Integer = 1 To 4
            Dim idx As Integer = r.Next(0, 10)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.ShowDialog()

        Dim fileName As String = SaveFileDialog1.FileName
        Dim streamWriter As StreamWriter = File.CreateText(fileName)

        Timer1.Start()

        amount = NumericUpDown1.Value

        Dim nameslist() As String = File.ReadAllLines(System.AppDomain.CurrentDomain.BaseDirectory() & "names.txt")
        Dim rndmName As String = nameslist(GetRandom(nameslist.Length))
        Dim r As New Random

        Do Until amount = 0
            streamWriter.WriteLine(rndmName & RandomString(r) & "@disposeamail.com")
            rndmName = nameslist(GetRandom(nameslist.Length))
            amount -= 1
        Loop

        streamWriter.Flush()
        MsgBox("Successfully generated " & NumericUpDown1.Value & " emails!")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label2.Text = amount
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
    End Sub
End Class
