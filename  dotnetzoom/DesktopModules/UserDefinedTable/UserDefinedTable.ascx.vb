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

Namespace DotNetZoom

    Public MustInherit Class UserDefinedTable
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected WithEvents grdData As System.Web.UI.WebControls.DataGrid
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
        Protected WithEvents cmdManage As System.Web.UI.WebControls.LinkButton
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			

            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"

            BindData()

            If IsEditable Then
                cmdManage.Visible = True
				cmdManage.Text = GetLanguage("ManageTableUDT")
            Else
                cmdManage.Visible = False
            End If

        End Sub

        Private Sub BindData()

            Dim objUserDefinedTable As New UserDefinedTableDB()

            Dim strSortField As String = ""
            Dim strSortOrder As String
            Dim intColumn As Integer

            Dim dr As SqlDataReader

            If ViewState("SortField") <> "" And ViewState("SortOrder") <> "" Then
                strSortField = ViewState("SortField")
                strSortOrder = ViewState("SortOrder")
            Else
                Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)
                If CType(settings("sortfield"), String) <> "" Then
                    dr = objUserDefinedTable.GetSingleUserDefinedField(CType(settings("sortfield"), Integer))
                    If dr.Read Then
                        strSortField = dr("FieldTitle").ToString
                    End If
                    dr.Close()
                End If
                If CType(settings("sortorder"), String) <> "" Then
                    strSortOrder = CType(settings("sortorder"), String)
                Else
                    strSortOrder = "ASC"
                End If
            End If

            For intColumn = grdData.Columns.Count - 1 To 1 Step -1
                grdData.Columns.RemoveAt(intColumn)
            Next

            dr = objUserDefinedTable.GetUserDefinedFields(ModuleId)
            While dr.Read
                Dim colField As New BoundColumn()
                colField.HeaderText = dr("FieldTitle").ToString
                If dr("FieldTitle").ToString = strSortField Then
                    If strSortOrder = "ASC" Then
                        colField.HeaderText += "<img src=""" & glbPath() & "images/sortascending.gif"" border=""0"" alt=""Sorted By " & strSortField & " In Ascending Order"">"
                    Else
                        colField.HeaderText += "<img src=""" & glbPath() & "images/sortdescending.gif"" border=""0"" alt=""Sorted By " & strSortField & " In Descending Order"">"
                    End If
                End If
                colField.DataField = dr("FieldTitle").ToString
                colField.Visible = dr("Visible")
                colField.SortExpression = dr("FieldTitle").ToString & "|ASC"
                Select Case dr("FieldType").ToString
                    Case "DateTime"
                        colField.DataFormatString = "{0:MMM dd yyyy}"
                    Case "Int32"
                        colField.DataFormatString = "{0:#,###,##0}"
                        colField.HeaderStyle.HorizontalAlign = HorizontalAlign.Right
                        colField.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                    Case "Decimal"
                        colField.DataFormatString = "{0:#,###,##0.00}"
                        colField.HeaderStyle.HorizontalAlign = HorizontalAlign.Right
                        colField.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                End Select
                grdData.Columns.Add(colField)
            End While
            dr.Close()

            Dim ds As DataSet

            ds = objUserDefinedTable.GetUserDefinedRows(ModuleId)

            Dim dv As DataView

            ' create a dataview to process the sort and filter options
            dv = New DataView(ds.Tables(0))

            ' sort data view
            If strSortField <> "" And strSortOrder <> "" And dv.count > 0 Then
                dv.Sort = strSortField & " " & strSortOrder
            End If

            grdData.DataSource = dv
            grdData.DataBind()
        End Sub

        Public Sub grdData_Sort(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
            Dim strSort() As String = Split(e.SortExpression, "|")

            If strSort(0) = ViewState("SortField") Then
                If ViewState("SortOrder") = "ASC" Then
                    ViewState("SortOrder") = "DESC"
                Else
                    ViewState("SortOrder") = "ASC"
                End If
            Else
                ViewState("SortOrder") = strSort(1)
            End If

            ViewState("SortField") = strSort(0)

            BindData()
        End Sub

        Private Sub cmdManage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdManage.Click
            Response.Redirect(GetFullDocument() & "?tabid=" & TabId & "&mid=" & ModuleId & "&def=Manage UDT", True)
        End Sub

    End Class

End Namespace