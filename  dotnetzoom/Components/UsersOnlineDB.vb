Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection

Imports DotNetZoom

Public Class UsersOnlineDB

    Dim _spPrefix As String = "UsersOnline_"

    Public Sub GetUserCounts(ByVal PortalId As Integer, ByRef GuestCount As Integer, ByRef MemberCount As Integer, ByRef UserCount As Integer, ByRef NewToday As Integer, ByRef NewYesterday As Integer, ByRef LatestUsername As String, ByRef LatestUserID As Integer)

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {PortalId, GuestCount, MemberCount, UserCount, NewToday, NewYesterday, LatestUsername, LatestUserID}, CommandType.StoredProcedure, _spPrefix & "GetUserCounts")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        GuestCount = 0
        MemberCount = 0
        UserCount = 0
        NewToday = 0
        NewYesterday = 0
        LatestUsername = ""
        LatestUserID = 0

        If result.Read() Then
            GuestCount = result(0)
        End If

        result.NextResult()

        If result.Read() Then
            MemberCount = result(0)
        End If

        result.NextResult()

        If result.Read() Then
            UserCount = result(0)
        End If

        result.NextResult()

        If result.Read() Then
            NewToday = result(0)
        End If

        result.NextResult()

        If result.Read() Then
            NewYesterday = result(0)
        End If

        result.NextResult()

        If result.Read() Then
            LatestUsername = result(0)
            LatestUserID = result(1)
        End If

        result.Close()

    End Sub

    Public Function GetUsers(ByVal PortalId As Integer, ByVal LoggedInUserID As Integer, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByVal sortOrder As String, ByVal sortDirection As String) As SqlDataReader

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {PortalId, LoggedInUserID, pageNumber, pageSize, sortOrder, sortDirection}, CommandType.StoredProcedure, _spPrefix & "GetUsers")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ' Return the datareader 
        Return result

    End Function

    Public Function GetUsersOnlineOnly(ByVal PortalId As Integer, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByVal LoggedInUserID As Integer) As SqlDataReader

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {PortalId, pageNumber, pageSize, LoggedInUserID}, CommandType.StoredProcedure, _spPrefix & "GetUsersOnlineOnly")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ' Return the datareader 
        Return result

    End Function

    Public Function GetUsersBuddiesOnly(ByVal PortalId As Integer, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByVal LoggedInUserID As Integer) As SqlDataReader

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {PortalId, pageNumber, pageSize, LoggedInUserID}, CommandType.StoredProcedure, _spPrefix & "GetUsersBuddiesOnly")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ' Return the datareader 
        Return result

    End Function

    Public Function GetOnlineUsers(ByVal PortalId As Integer) As SqlDataReader

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {PortalId}, CommandType.StoredProcedure, _spPrefix & "GetOnlineUsers")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ' Return the datareader 
        Return result

    End Function

    Public Function GetSingleUser(ByVal PortalID As Integer, ByVal UserID As Integer, ByVal LoggedInUserID As Integer) As SqlDataReader

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {PortalID, UserID, LoggedInUserID}, CommandType.StoredProcedure, _spPrefix & "GetSingleUser")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ' Return the datareader 
        Return result

    End Function

End Class
