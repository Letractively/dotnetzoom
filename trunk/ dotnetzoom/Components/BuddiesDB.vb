Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection

Imports DotNetZoom

Public Class BuddiesDB

    Dim _spPrefix As String = "UsersOnline_"

    Public Function GetBuddiesList(ByVal portalID As Integer, ByVal userID As Integer) As SqlDataReader

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {portalID, userID}, CommandType.StoredProcedure, _spPrefix & "GetBuddiesList")

        ' Execute the command
        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ' Return the datareader 
        Return result

    End Function

    Public Function GetBuddiesListDataTable(ByVal portalID As Integer, ByVal userID As Integer) As DataTable

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {portalID, userID}, CommandType.StoredProcedure, _spPrefix & "GetBuddiesList")

        Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(myCommand)

        myDataAdapter.SelectCommand.Connection = myConnection

        Dim myTable As DataTable = New DataTable

        myDataAdapter.Fill(myTable)

        Return myTable

    End Function

    Public Function AddBuddy(ByVal userID As Integer, ByVal buddyID As Integer) As SqlDataReader

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {userID, buddyID}, CommandType.StoredProcedure, _spPrefix & "AddBuddy")

        ' Execute the command
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()

    End Function

    Public Function DeleteBuddy(ByVal userID As Integer, ByVal buddyID As Integer) As SqlDataReader

        ' Create Instance of Connection and Command Object
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        ' Generate Command Object based on Method
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {userID, buddyID}, CommandType.StoredProcedure, _spPrefix & "DeleteBuddy")

        ' Execute the command
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()

    End Function

End Class