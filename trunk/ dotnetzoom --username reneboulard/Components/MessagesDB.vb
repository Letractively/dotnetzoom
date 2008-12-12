Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection

Imports DotNetZoom

Public Class MessagesDB

    Dim _spPrefix As String = "UsersOnline_"

    Public Function GetPrivateMessagesInbox(ByVal UserId As Integer, ByVal SortField As String, ByVal SortDirection As String) as SqlDataReader
        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {UserId, SortField, SortDirection}, CommandType.StoredProcedure, _spPrefix & "GetPrivateMessagesInbox")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ' Return the datareader 
        Return result
    End Function

    Public Function GetPrivateMessagesOutbox(ByVal UserId As Integer, ByVal SortField As String, ByVal SortDirection As String) as SqlDataReader
        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {UserId, SortField, SortDirection}, CommandType.StoredProcedure, _spPrefix & "GetPrivateMessagesOutbox")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ' Return the datareader 
        Return result
    End Function

    Public Sub AddPrivateMessage(ByVal SenderId As Integer, ByVal ReceiverId As Integer, ByVal Subject As String, ByVal Message As String)
        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {SenderId, ReceiverId, Subject, Message}, CommandType.StoredProcedure, _spPrefix & "AddPrivateMessage")

        ' Execute the command
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()
    End Sub

    Public Function GetSinglePrivateMessage(ByVal MessageId As Integer) as SqlDataReader
        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {MessageId}, CommandType.StoredProcedure, _spPrefix & "GetSinglePrivateMessage")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ' Return the datareader 
        Return result
    End Function

    Public Sub DeletePrivateMessages(ByVal MessageId As Integer)
        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {MessageId}, CommandType.StoredProcedure, _spPrefix & "DeletePrivateMessages")

        ' Execute the command
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()
    End Sub

    Public Function GetUsersByUsername(Optional ByVal PortalId As Integer = -1, Optional ByVal Filter As String = "") As SqlDataReader

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {IIf(PortalId <> -1, PortalId, SqlInt16.Null), Filter}, CommandType.StoredProcedure, _spPrefix & "GetUsersByUsername")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ' Return the datareader 
        Return result

    End Function

    Public Function GetUserId(ByVal PortalId As Integer, ByVal UserName As String) As Integer

        Dim userID As Integer = -1
        Dim result As SqlDataReader = GetUsersByUsername(PortalId, UserName)

        If (result.Read()) Then
            userID = result("userid")
        End If

        result.Close()

        Return userID

    End Function

    Public Sub UpdateMessageRead(ByVal MessageId As Integer)
        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {MessageId}, CommandType.StoredProcedure, _spPrefix & "UpdateMessageRead")

        ' Execute the command
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()
    End Sub

    Public Sub UpdateMessageUnread(ByVal MessageId As Integer)
        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {MessageId}, CommandType.StoredProcedure, _spPrefix & "UpdateMessageUnread")

        ' Execute the command
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()
    End Sub

    Public Sub GetMessageCount(ByVal UserId As Integer, ByRef unreadCount As Integer, ByRef readCount As Integer)
        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {UserId, unreadCount, readCount}, CommandType.StoredProcedure, _spPrefix & "GetMessageCount")


        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        If result.Read() Then
            unreadCount = result(0)
        End If

        result.NextResult()

        If result.Read() Then
            readCount = result(0)
        End If

        result.Close()

    End Sub

End Class