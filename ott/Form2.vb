﻿Imports System.Data.SqlClient
Public Class Form2


    Private Sub Login_Click(sender As Object, e As EventArgs) Handles Login.Click

        Dim LoggedIn As New Boolean

        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dr As SqlDataReader

        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saif5\source\repos\ott\ott\Database1.mdf;Integrated Security=True"

        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select password from Users where username='" & LoginName.Text & "' "

        dr = cmd.ExecuteReader()

        While dr.Read()
            If LoginPassword.Text = dr(0) Then
                LoggedIn = True
            Else
                MsgBox("Incorrect Username or password")
            End If
        End While

        If LoggedIn = True Then
            con.Close()
            con.Open()

            Dim cmd2 As New SqlCommand
            Dim dr2 As SqlDataReader

            cmd2.Connection = con
            cmd2.CommandType = CommandType.Text
            cmd2.CommandText = "select * from Logged where username = '" & LoginName.Text & "' "

            dr2 = cmd2.ExecuteReader

            If dr2.HasRows Then
                MsgBox("You are already logged in please log out!")
            Else

                con.Close()
                con.Open()

                cmd = New SqlCommand("INSERT INTO Logged values('" & LoginName.Text & "')", con)
                cmd.ExecuteNonQuery()
                MsgBox("Authenticated!")
                Me.Hide()
                Form3.Show()
            End If
        End If

    End Sub

    Private Sub Logout_Click(sender As Object, e As EventArgs) Handles Logout.Click

        Dim con2 As New SqlConnection
        Dim cmd2 As New SqlCommand
        Dim dr2 As SqlDataReader

        con2.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saif5\source\repos\ott\ott\Database1.mdf;Integrated Security=True"

        con2.Open()
        cmd2.Connection = con2
        cmd2.CommandType = CommandType.Text
        cmd2.CommandText = "TRUNCATE TABLE Logged"
        cmd2.ExecuteNonQuery()

        MsgBox("Logged Out")


    End Sub


End Class