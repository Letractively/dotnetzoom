'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
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
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Data.SqlTypes
Imports System.Reflection

Namespace DotNetZoom

    Public Class DiscussionDB


        Public Function GetTopLevelMessages(ByVal ModuleId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetThreadMessages(ByVal Parent As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Parent})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Function GetSingleMessage(ByVal ItemId As Integer, ByVal ModuleId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ItemId, ModuleId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Sub DeleteMessage(ByVal ItemId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ItemId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

        Public Sub AddMessage(ByVal ModuleId As Integer, ByVal ParentId As Integer, ByVal UserName As String, ByVal Title As String, ByVal Body As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim objSecurity As New PortalSecurity()

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleId, _
                              ParentId, _
                              UserName, _
                              objSecurity.InputFilter(Title, PortalSecurity.FilterFlag.NoMarkup), _
                              objSecurity.InputFilter(Body, PortalSecurity.FilterFlag.NoMarkup)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

        Public Sub UpdateMessage(ByVal ItemId As Integer, ByVal UserName As String, ByVal Title As String, ByVal Body As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim objSecurity As New PortalSecurity()

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ItemId, _
                              UserName, _
                              objSecurity.InputFilter(Title, PortalSecurity.FilterFlag.NoMarkup), _
                              objSecurity.InputFilter(Body, PortalSecurity.FilterFlag.NoMarkup)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

    End Class

End Namespace
