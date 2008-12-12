'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2008
' by René Boulard ( http://www.reneboulard.qc.ca)'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection

Namespace DotNetZoom

    Public Class UsersDB

        Public Function AddUser(ByVal PortalId As Integer, ByVal FirstName As String, ByVal LastName As String, ByVal Unit As String, ByVal Street As String, ByVal City As String, ByVal Region As String, ByVal PostalCode As String, ByVal Country As String, ByVal Telephone As String, ByVal Email As String, ByVal Username As String, ByVal Password As String, ByVal Authorized As String, <SqlParameter(, , , , , ParameterDirection.Output)> ByVal UserId As Integer) As Integer

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            UserId = -1

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, FirstName, LastName, Unit, Street, City, Region, PostalCode, Country, Telephone, Email, Username, IIf(Password <> "", Password, SqlInt16.Null), IIf(Authorized <> "", Boolean.Parse(Authorized), SqlInt16.Null), UserId})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                UserId = CInt(myCommand.Parameters("@UserID").Value)
            Catch
                ' duplicate user
            End Try

            myConnection.Close()

            Return UserId
        End Function


        Public Sub DeleteUser(ByVal PortalId As Integer, ByVal UserId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, UserId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub DeleteUsers(ByVal PortalId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub UpdateUser(ByVal PortalId As Integer, ByVal UserId As Integer, ByVal FirstName As String, ByVal LastName As String, ByVal Unit As String, ByVal Street As String, ByVal City As String, ByVal Region As String, ByVal PostalCode As String, ByVal Country As String, ByVal Telephone As String, ByVal Email As String, ByVal Username As String, ByVal Password As String, Optional ByVal Authorized As String = "")

            Dim objAuthorized As Object
            If Authorized <> "" Then
                objAuthorized = Boolean.Parse(Authorized)
            Else
                objAuthorized = SqlInt16.Null
            End If

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, UserId, FirstName, LastName, Unit, Street, City, Region, PostalCode, Country, Telephone, Email, Username, IIf(Password <> "", Password, SqlInt16.Null), objAuthorized})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function UpdateUserLogin(ByVal UserId As Integer, ByVal PortalId As Integer) As Boolean

            Dim blnAuthorized As Boolean = False

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserId, PortalId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            If result.Read Then
                blnAuthorized = Boolean.Parse(result("Authorized").ToString)
            End If
            result.Close()

            Return blnAuthorized

        End Function


        Public Function GetSingleUser(ByVal PortalId As Integer, ByVal UserId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, UserId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Function GetUserCountryCode(ByVal PortalId As Integer, ByVal UserId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, UserId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function
		
		

		Public Sub UpdateUserIP(ByVal UserID As Integer, ByVal IP As String, ByVal Security_Code As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserID, IP, Security_Code})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


		Public Sub UpdateUsercodes(ByVal UserID As Integer, ByVal Country_Code As String, ByVal IPfrom As String, ByVal IPto As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)
		
            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserID, Country_Code, IPfrom, IPto })

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

				
		Public Sub UpdateCheckUserSecurity(ByVal UserID As Integer, ByVal Code As String, ByVal NextLogin As DateTime, ByVal nTry As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserID, Code, NextLogin, nTry})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


		 Public Function CheckUserSecurity(ByVal PortalID As Integer, ByVal Username As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID, Username})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

		
		
		
        Public Function GetSingleUserByUsername(ByVal PortalID As Integer, ByVal Username As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID, Username})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Function GetRolesByUser(ByVal UserId As Integer, ByVal PortalId As Integer) As String()

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserId, PortalId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' create a String array from the data
            Dim userRoles As New ArrayList()

            While result.Read()
                userRoles.Add(result("RoleId").ToString)
            End While

            result.Close()

            ' Return the String array of roles
            Return CType(userRoles.ToArray(GetType(String)), String())

        End Function

        Public Function GetPortalRoles(ByVal PortalId As Integer, ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetSingleRole(ByVal RoleID As Integer, ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {RoleID, Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Sub AddRole(ByVal PortalId As Integer, ByVal RoleName As String, ByVal Description As String, ByVal ServiceFee As Double, ByVal BillingPeriod As String, ByVal BillingFrequency As String, ByVal TrialFee As Double, ByVal TrialPeriod As Integer, ByVal TrialFrequency As String, ByVal IsPublic As Boolean, ByVal AutoAssignment As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, RoleName, Description, ServiceFee, BillingPeriod, IIf(BillingFrequency <> "", BillingFrequency, SqlInt16.Null), TrialFee, TrialPeriod, IIf(TrialFrequency <> "", TrialFrequency, SqlInt16.Null), IsPublic, AutoAssignment})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub DeleteRole(ByVal RoleId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {RoleId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub UpdateRole(ByVal RoleId As Integer, ByVal Language As String, ByVal RoleName As String, ByVal Description As String, ByVal ServiceFee As Double, ByVal BillingPeriod As String, ByVal BillingFrequency As String, ByVal TrialFee As Double, ByVal TrialPeriod As Integer, ByVal TrialFrequency As String, ByVal IsPublic As Boolean, ByVal AutoAssignment As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {RoleId, Language, RoleName, Description, ServiceFee, BillingPeriod, IIf(BillingFrequency <> "", BillingFrequency, SqlInt16.Null), TrialFee, TrialPeriod, IIf(TrialFrequency <> "", TrialFrequency, SqlInt16.Null), IsPublic, AutoAssignment})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function GetRoleMembership(ByVal PortalId As Integer, ByVal Language As String, Optional ByVal RoleId As Integer = -1, Optional ByVal UserId As Integer = -1) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, Language, IIf(RoleId <> -1, RoleId, SqlInt16.Null), IIf(UserId <> -1, UserId, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function IsUserInRole(ByVal UserId As Integer, ByVal RoleId As Integer, ByVal PortalId As Integer) As Boolean

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserId, RoleId, PortalId}, , "GetSingleUserRole")

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the result
            IsUserInRole = False

            If result.Read Then
                If Not IsDBNull(result("UserID")) Then
                    IsUserInRole = True
                End If
            End If
            result.Close()

        End Function


        Public Sub AddUserRole(ByVal PortalID As Integer, ByVal RoleId As Integer, ByVal UserId As Integer, Optional ByVal ExpiryDate As String = "")
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID, RoleId, UserId, IIf(ExpiryDate <> "", ExpiryDate, SqlInt16.Null)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub DeleteUserRole(ByVal UserRoleId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserRoleId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function GetServices(ByVal PortalId As Integer, ByVal Language As String, Optional ByVal UserId As Integer = -1) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, Language, IIf(UserId <> -1, UserId, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Sub UpdateService(ByVal UserId As Integer, ByVal RoleId As Integer, Optional ByVal Cancel As Boolean = False)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserId, RoleId, Cancel})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function GetUsers(Optional ByVal PortalId As Integer = -1, Optional ByVal Filter As String = "") As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(PortalId <> -1, PortalId, SqlInt16.Null), Filter})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

    End Class

End Namespace