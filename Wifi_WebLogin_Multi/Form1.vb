Public Class Form1
    Dim i As Integer = 1
    Dim pageready As Boolean = False
    Dim username As String = ""
    Dim password As String = ""
    Private Sub pagewait(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
        If WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then
            pageready = True
            RemoveHandler WebBrowser1.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf pagewait)
        End If
    End Sub
    Private Sub waitforpageload()
        AddHandler WebBrowser1.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf pagewait)
        While Not pageready
            Application.DoEvents()
        End While
        pageready = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        username = TextBox1.Text
        password = TextBox2.Text
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
    Private Sub Input_Username()
        AddHandler TextBox1.TextChanged, New EventHandler(AddressOf TextBox1_TextChanged)
        Application.DoEvents()
        AddHandler TextBox1.TextChanged, New EventHandler(AddressOf TextBox2_TextChanged)
        Application.DoEvents()
        AddHandler Button1.Click, New EventHandler(AddressOf Button1_Click)
        Application.DoEvents()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i As Integer = 0 To 2
            i = 1
            If username = "" Or password = "" Then
                Me.Visible = True
                Input_Username()
            Else
                Me.Visible = False
                Dim Test1 As Integer = 0
                Dim Test2 As Integer = 0
                Dim Test3 As Integer = 0
                Dim Test4 As Integer = 0

                On Error Resume Next
                If My.Computer.Network.Ping("www.google.com") = True Then
                    Test1 = 1
                ElseIf My.Computer.Network.Ping("www.google.com") = False Then
                    Test1 = 0
                End If

                If My.Computer.Network.Ping("www.yahoo.com") = True Then
                    Test2 = 1
                ElseIf My.Computer.Network.Ping("www.yahoo.com") = False Then
                    Test2 = 0
                End If

                If My.Computer.Network.Ping("www.bing.com") = True Then
                    Test3 = 1
                ElseIf My.Computer.Network.Ping("www.bing.com") = False Then
                    Test3 = 0
                End If

                Test4 = Test1 + Test2 + Test3

                If Test4 > 0 Then
                    MsgBox("Internernet Available")

                Else
                    'MsgBox("Authentication Required")
                    WebBrowser1.Navigate("https://1.1.1.1/login.html")
                    waitforpageload()
                    WebBrowser1.ScriptErrorsSuppressed = True
                    Dim j As Integer = 1
                    For j = 0 To 2
                        j = 1

                        If WebBrowser1.Document.Title = "Web Authentication" Then
                            Dim check As HtmlElement = WebBrowser1.Document.GetElementById("Submit")
                            If check IsNot Nothing Then
                                j = 3

                            Else
                                WebBrowser1.Navigate("https://1.1.1.1/login.html")
                                waitforpageload()

                            End If
                        Else
                            WebBrowser1.Navigate("https://1.1.1.1/login.html")
                            waitforpageload()

                        End If
                    Next
                    j = 1

                    WebBrowser1.Document.GetElementById("username").SetAttribute("value", username)
                    WebBrowser1.Document.GetElementById("password").SetAttribute("value", password)
                    WebBrowser1.Document.GetElementById("Submit").InvokeMember("click")

                End If
            End If

            System.Threading.Thread.Sleep(1000)

        Next
    End Sub
End Class

