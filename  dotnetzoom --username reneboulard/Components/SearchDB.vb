'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( shaunw1@shaw.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
'
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
Imports System.Reflection
Imports System.Data.SqlTypes
Imports System.Web.HttpUtility
Namespace DotNetZoom

    Public Class SearchDB


        Public Function GetResults(ByVal PortalId As Integer, ByVal ModuleId As Integer, ByVal Search As String, ByVal MaxResults As Integer) As DataSet

            Dim strMultiple As String
            Dim strSQL As String

            ' build template
            strSQL = "select Tabs.TabId, Tabs.TabName, Tabs.AuthorizedRoles, "
            strSQL += "Modules.ModuleId, Modules.ModuleTitle, Modules.AuthorizedViewRoles, "
            strSQL += "'TitleField' = NULL, "
            strSQL += "'DescriptionField' = NULL, "
            strSQL += "'CreatedDateField' = NULL, "
            strSQL += "'CreatedByUserField' = NULL "
            strSQL += "from Modules "
            strSQL += "inner join Tabs on Modules.TabId = Tabs.TabId "
            strSQL += "where 1 = 0"

            strMultiple = " /* Template */ " & strSQL

            ' build multiple result set query from search criteria
            Dim dr As SqlDataReader = GetSearch(ModuleId)
            While dr.Read
                If Not IsDBNull(dr("TitleField")) Then
                    strSQL = "select Tabs.TabId, Tabs.TabName, Tabs.AuthorizedRoles, "
                    strSQL += "Modules.ModuleId, Modules.ModuleTitle, Modules.AuthorizedViewRoles, "
                    strSQL += "'TitleField' = " & IIf(IsDBNull(dr("TitleField")), "NULL", dr("TitleField")) & ", "
                    strSQL += "'DescriptionField' = " & IIf(IsDBNull(dr("DescriptionField")), "NULL", dr("DescriptionField")) & ", "
                    strSQL += "'CreatedDateField' = " & IIf(IsDBNull(dr("CreatedDateField")), "NULL", "convert(nvarchar," & dr("CreatedDateField") & ",101)") & ", "
                    If Not IsDBNull(dr("CreatedByUserField")) Then
                        strSQL += "'CreatedByUserField' = Users.FirstName + ' ' + Users.LastName "
                    Else
                        strSQL += "'CreatedByUserField' = NULL "
                    End If
                    strSQL += "from " & dr("TableName").ToString & " "
                    strSQL += "inner join Modules on " & dr("TableName").ToString & ".ModuleId = Modules.ModuleId "
                    strSQL += "inner join Tabs on Modules.TabId = Tabs.TabId "
                    If Not IsDBNull(dr("CreatedByUserField")) Then
                        strSQL += "left outer join Users on " & dr("TableName").ToString & "." & dr("CreatedByUserField").ToString & " = Users.UserId "
                    End If
                    strSQL += "where Tabs.PortalId = " & PortalId.ToString & " and (Modules.Language = '' or Modules.Language = '" & GetLanguage("N") & "' )and ( "
                    strSQL += IIf(IsDBNull(dr("TitleField")), "''", dr("TitleField")) & " like '%" & Search & "%' COLLATE French_CI_AI or "
                    strSQL += IIf(IsDBNull(dr("DescriptionField")), "''", dr("DescriptionField")) & " like '%" & Search & "%' COLLATE French_CI_AI or "
                    strSQL += IIf(IsDBNull(dr("TitleField")), "''", dr("TitleField")) & " like '%" & HTMLEncode(Search) & "%' COLLATE French_CI_AI or "
                    strSQL += IIf(IsDBNull(dr("DescriptionField")), "''", dr("DescriptionField")) & " like '%" & HTMLEncode(Search) & "%' COLLATE French_CI_AI )"
                    strMultiple += " /* " & dr("TableName").ToString & " */ " & strSQL
                End If
            End While
            dr.Close()

            ' create dataset
            Dim dataSet As dataSet = New dataSet()

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {}, CommandType.Text, strMultiple)

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' add datatable to dataset
            Dim dataTable As dataTable = New dataTable()
            Dim schema As dataTable = result.GetSchemaTable()
            Dim intCounter As Integer
            For intCounter = 0 To schema.Rows.Count - 1
                Dim dataRow As DataRow = schema.Rows(intCounter)
                Dim columnName As String = CType(dataRow("ColumnName"), String)
                Dim column As DataColumn = New DataColumn(columnName, System.Type.GetType("System.String"))
                dataTable.Columns.Add(column)
            Next
            dataSet.Tables.Add(dataTable)

            ' build single dataset from multiple resultsets
            Dim intResults As Integer = 0
            Do
                While result.Read
                    ' check security
                    If PortalSecurity.IsInRoles(IIf(result("AuthorizedViewRoles").ToString <> "", result("AuthorizedViewRoles").ToString, result("AuthorizedRoles").ToString)) Then
                        If intResults < MaxResults Or MaxResults = -1 Then
                            Dim dataRow As DataRow = dataTable.NewRow()

                            For intCounter = 0 To result.FieldCount - 1
                                dataRow(intCounter) = result.GetValue(intCounter)
                            Next

                            dataTable.Rows.Add(dataRow)

                            intResults = intResults + 1
                        Else
                            Exit Do
                        End If
                    End If
                End While

                ' next result set
            Loop Until Not result.NextResult()
            result.Close()

            ' Return the dataset
            Return dataSet

        End Function


        Public Function GetTables() As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {}, CommandType.Text, "exec sp_fkeys 'Modules'")

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetSearch(ByVal ModuleId As Integer) As SqlDataReader

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


        Public Function AddSearch(ByVal ModuleId As Integer, ByVal TableName As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleId, TableName})

            ' Execute the command
            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Function


        Public Function GetSingleSearch(ByVal SearchId As Integer, ByVal ModuleId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {SearchId, ModuleId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function


        Public Function GetFields(ByVal SearchId As Integer, ByVal ModuleId As Integer) As ArrayList

            Dim arrFields As New ArrayList()

            arrFields.Add(getlanguage("list_none"))

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            Dim dr As SqlDataReader = GetSingleSearch(SearchId, ModuleId)
            If dr.Read Then

                ' Generate Command Object based on Method
                Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                    CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                    New Object() {}, CommandType.Text, "select * from " & dr("TableName").ToString & " where 1 = 0")

                ' Execute the command
                myConnection.Open()
                Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

                ' build the arraylist
                Dim schema As DataTable = result.GetSchemaTable()
                Dim intFields As Integer
                For intFields = 0 To schema.Rows.Count - 1
                    Dim dataRow As dataRow = schema.Rows(intFields)
                    arrFields.Add(CType(dataRow("ColumnName"), String))
                Next
                result.Close()

            End If
            dr.Close()

            ' Return the arraylist
            Return arrFields

        End Function

        Public Function UpdateSearch(ByVal SearchId As Integer, ByVal TitleField As String, ByVal DescriptionField As String, ByVal CreatedDateField As String, ByVal CreatedByUserField As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {SearchId, IIf(TitleField <> "", TitleField, SqlInt16.Null), IIf(DescriptionField <> "", DescriptionField, SqlInt16.Null), IIf(CreatedDateField <> "", CreatedDateField, SqlInt16.Null), IIf(CreatedByUserField <> "", CreatedByUserField, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Function

        Public Function DeleteSearch(ByVal SearchId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {SearchId})

            ' Execute the command
            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Function

    End Class

End Namespace