'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
' =======================================================================================
Option Strict On

Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection
Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public Class ForumUserOnlineDB

        Public Function TTTForum_UserOnline_Get(ByVal PortalId As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Sub TTTForum_UserOnline_GetCount(ByVal PortalId As Integer, ByRef GuestCount As Integer, ByRef MemberCount As Integer)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, GuestCount, MemberCount})

            Try
                ' Execute the command
                myConnection.Open()
                Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

                GuestCount = 0
                MemberCount = 0

                If result.Read() Then
                    GuestCount = ConvertInteger(result(0))
                End If

                result.NextResult()

                If result.Read() Then
                    MemberCount = ConvertInteger(result(0))
                End If
            Catch Exc As System.Exception

            End Try

        End Sub

        Public Sub TTTForum_UserOnline2_GetCount(ByVal PortalId As Integer, ByRef GuestCount As Integer, ByRef MemberCount As Integer, ByRef UserCount As Integer, ByRef NewToday As Integer, ByRef NewYesterday As Integer, ByRef LatestUsername As String)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, GuestCount, MemberCount, UserCount, NewToday, NewYesterday, LatestUsername}, CommandType.StoredProcedure, "TTTForum_UserOnline2_GetCount")

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            GuestCount = 0
            MemberCount = 0
            UserCount = 0
            NewToday = 0
            NewYesterday = 0
            LatestUsername = ""

            If result.Read() Then
                GuestCount = ConvertInteger(result(0))
            End If

            result.NextResult()

            If result.Read() Then
                MemberCount = ConvertInteger(result(0))
            End If

            result.NextResult()

            If result.Read() Then
                UserCount = ConvertInteger(result(0))
            End If

            result.NextResult()

            If result.Read() Then
                NewToday = ConvertInteger(result(0))
            End If

            result.NextResult()

            If result.Read() Then
                NewYesterday = ConvertInteger(result(0))
            End If

            result.NextResult()

            If result.Read() Then
                LatestUsername = ConvertString(result(0))
            End If

            result.Close()

        End Sub

        Public Function TTTForum_UserOnline2_Get(ByVal PortalId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId}, CommandType.StoredProcedure, "TTTForum_UserOnline2_Get")

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function TTTForum_PrivateMessage_GetInbox(ByVal UserId As Integer, ByVal SortField As String, ByVal SortDirection As String) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserId, SortField, SortDirection})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Function TTTForum_PrivateMessage_GetOutbox(ByVal UserId As Integer, ByVal SortField As String, ByVal SortDirection As String) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserId, SortField, SortDirection})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Sub TTTForum_PrivateMessage_Add(ByVal SenderId As Integer, ByVal ReceiverId As Integer, ByVal Subject As String, ByVal Message As String)
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {SenderId, ReceiverId, Subject, Message})

            Try
                ' Execute the command
                myConnection.Open()
                myCommand.ExecuteNonQuery()
            Catch exc As System.Exception
            End Try
            myConnection.Close()

        End Sub

        Public Function TTTForum_PrivateMessage_GetSingle(ByVal MessageId As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {MessageId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Sub TTTForum_PrivateMessage_Delete(ByVal MessageId As Integer)
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {MessageId})
            Try
                ' Execute the command
                myConnection.Open()
                myCommand.ExecuteNonQuery()
            Catch exc As System.Exception
            End Try
            myConnection.Close()

        End Sub


        Public Sub TTTForum_PrivateMessage_UpdateRead(ByVal MessageId As Integer)
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {MessageId})
            Try
                ' Execute the command
                myConnection.Open()
                myCommand.ExecuteNonQuery()
            Catch exc As System.Exception
            End Try
            myConnection.Close()

        End Sub

        Public Sub TTTForum_PrivateMessage_UpdateUnread(ByVal MessageId As Integer)
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {MessageId})

            Try
                ' Execute the command
                myConnection.Open()
                myCommand.ExecuteNonQuery()
            Catch exc As System.Exception
            End Try
            myConnection.Close()

        End Sub

        Public Function TTTForum_PrivateMessage_GetCount(ByVal UserId As Integer) As Integer
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserId})

            Try
                ' Execute the command
                myConnection.Open()
                Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

                If result.Read() Then
                    Return ConvertInteger(result(0))
                Else
                    Return 0
                End If

            Catch Exc As System.Exception
                Return 0
            End Try

        End Function

    End Class

End Namespace
