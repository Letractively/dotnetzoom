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
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection

Namespace DotNetZoom

    Public Class EventsDB


        Public Function GetModuleEvents(ByVal ModuleId As Integer, Optional ByVal StartDate As String = "", Optional ByVal EndDate As String = "") As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
              CType(MethodBase.GetCurrentMethod(), MethodInfo), _
              New Object() {ModuleId, IIf(StartDate <> "", StartDate, SqlInt16.Null), IIf(EndDate <> "", EndDate, SqlInt16.Null)})

            ' Execute the command
              myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Function GetSingleModuleEvent(ByVal ItemId As Integer, ByVal ModuleId As Integer) As SqlDataReader

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


        Public Sub DeleteModuleEvent(ByVal ItemID As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ItemID})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub AddModuleEvent(ByVal ModuleId As Integer, ByVal Description As String, ByVal DateTime As Date, ByVal Title As String, ByVal ExpireDate As String, ByVal UserName As String, ByVal Every As String, ByVal Period As String, ByVal IconFile As String, ByVal AltText as String)
              Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
             Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleId, Description, DateTime, Title, IIf(ExpireDate <> "", ExpireDate, SqlInt16.Null), UserName, IIf(Every <> "", Every, SqlInt16.Null), IIf(Period <> "", Period, SqlInt16.Null), IIf(IconFile <> "", IconFile, SqlInt16.Null), IIf(AltText <> "", AltText, SqlInt16.Null)})

             myConnection.Open()
             myCommand.ExecuteNonQuery()
             myConnection.Close()
        End Sub


        Public Sub UpdateModuleEvent(ByVal ItemId As Integer, ByVal Description As String, ByVal DateTime As Date, ByVal Title As String, ByVal ExpireDate As String, ByVal UserName As String, ByVal Every As String, ByVal Period As String, ByVal IconFile As String, ByVal AltText as String)
             Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
             Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ItemId, Description, DateTime, Title, IIf(ExpireDate <> "", ExpireDate, SqlInt16.Null), UserName, IIf(Every <> "", Every, SqlInt16.Null), IIf(Period <> "", Period, SqlInt16.Null), IIf(IconFile <> "", IconFile, SqlInt16.Null), IIf(AltText <> "", AltText, SqlInt16.Null)})

             myConnection.Open()
             myCommand.ExecuteNonQuery()
             myConnection.Close()
        End Sub

    End Class

End Namespace