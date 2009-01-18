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

Namespace DotNetZoom

    Public Class EditUserDefinedTable
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents tblFields As System.Web.UI.WebControls.Table
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Private UserDefinedRowId As Integer = -1

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

            Title1.DisplayHelp = "DisplayHelp_EditUserDefinedTable"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			cmdUpdate.Text = GetLanguage("enregistrer")
			cmdCancel.Text = GetLanguage("annuler")
			cmdDelete.Text = GetLanguage("delete")		

            If IsNumeric(Request.Params("UserDefinedRowId")) Then
                UserDefinedRowId = Int32.Parse(Request.Params("UserDefinedRowId"))
            End If

            BuildTable()

            If Page.IsPostBack = False Then

                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm")) & "');")

                If UserDefinedRowId <> -1 Then
                    Dim objUserDefinedTable As New UserDefinedTableDB()

                    Dim dr As SqlDataReader = objUserDefinedTable.GetSingleUserDefinedRow(UserDefinedRowId, ModuleId)
                    While dr.Read()
                        CType(tblFields.FindControl(dr("FieldTitle").ToString), TextBox).Text = dr("FieldValue").ToString
                    End While
                    dr.Close()
                Else
                    cmdDelete.Visible = False
                End If

                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = GetFullDocument() & "?tabid=" & TabId


            End If

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click, cmdCancel.Click
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

        Private Sub BuildTable()
            Dim objUserDefinedTable As New UserDefinedTableDB()
            Dim objRow As TableRow
            Dim objCell As TableCell
            Dim objTextBox As TextBox

            Dim dr As SqlDataReader = objUserDefinedTable.GetUserDefinedFields(ModuleId)
            While dr.Read
                objRow = New TableRow()

                objCell = New TableCell()
                objCell.VerticalAlign = VerticalAlign.Top
                objCell.Controls.Add(New LiteralControl(dr("FieldTitle").ToString & ":"))
                objCell.CssClass = "SubHead"
                objRow.Cells.Add(objCell)

                objCell = New TableCell()
                objCell.VerticalAlign = VerticalAlign.Top
                objTextBox = New TextBox()
                objTextBox.ID = dr("FieldTitle")
                objTextBox.TextMode = TextBoxMode.MultiLine
                objTextBox.Columns = "50"
                objTextBox.Rows = 3
                objTextBox.CssClass = "NormalTextBox"
                objCell.Controls.Add(objTextBox)

                objRow.Cells.Add(objCell)

                tblFields.Rows.Add(objRow)
            End While
            dr.Close()
        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

            Dim objUserDefinedTable As New UserDefinedTableDB()
            Dim ValidInput As Boolean = True
            Dim strMessage As String = ""

            Dim dr As SqlDataReader = objUserDefinedTable.GetUserDefinedFields(ModuleId)
            While dr.Read
                If Request.Form("edit$" & dr("FieldTitle")) <> "" Then
                    Select Case dr("FieldType").ToString
                        Case "Int32"
                            If ValidateNumeric(Request.Form("edit$" & dr("FieldTitle")), "0123456789+-") = False Then
                                strMessage += "<br>" & dr("FieldTitle").ToString & " " & GetLanguage("need_integer")
                                ValidInput = False
                            End If
                        Case "Decimal"
                            If ValidateNumeric(Trim(Request.Form("edit$" & dr("FieldTitle"))), "0123456789+-.") = False Then
                                strMessage += "<br>" & dr("FieldTitle").ToString & " " & GetLanguage("need_decimal")
                                ValidInput = False
                            End If
                        Case "DateTime"
                            If IsDate(Request.Form("edit$" & dr("FieldTitle"))) = False Then
                                strMessage += "<br>" & dr("FieldTitle").ToString & " " & GetLanguage("Valid_Date_Format")
                                ValidInput = False
                            End If
                        Case "Boolean"
                            Select Case LCase(Request.Form("edit$" & dr("FieldTitle")))
                                Case "true", "false"
                                Case Else
                                    strMessage += "<br>" & dr("FieldTitle").ToString & " " & GetLanguage("Valid_Boolean")
                                    ValidInput = False
                            End Select
                    End Select
                End If
            End While
            dr.Close()

            If ValidInput Then
                If UserDefinedRowId = -1 Then
                    UserDefinedRowId = objUserDefinedTable.AddUserDefinedRow(ModuleId, UserDefinedRowId)
                End If

                dr = objUserDefinedTable.GetUserDefinedFields(ModuleId)
                While dr.Read
                    objUserDefinedTable.UpdateUserDefinedData(UserDefinedRowId, dr("UserDefinedFieldId"), Request.Form("edit$" & dr("FieldTitle")))
                End While
                dr.Close()

                objUserDefinedTable.UpdateUserDefinedRow(UserDefinedRowId)
				' Reset data cashe
				
				ClearModuleCache(ModuleId)

                Response.Redirect(ViewState("UrlReferrer"), True)
            Else
                lblMessage.Text = strMessage
            End If

        End Sub

        Private Function ValidateNumeric(ByVal strValue As String, ByVal strValid As String) As Boolean

            Dim intCounter As Integer
            Dim blnValid As Boolean = True

                For intCounter = 1 To Len(strValue)
                    If InStr(1, strValid, Mid(strValue, intCounter, 1)) = 0 Then
                        blnValid = False
                    End If
                Next intCounter

            Return blnValid

        End Function

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
            Dim objUserDefinedTable As New UserDefinedTableDB()
            objUserDefinedTable.DeleteUserDefinedRow(UserDefinedRowId)
			' Reset data cashe
			
			ClearModuleCache(ModuleId)

            Response.Redirect(ViewState("UrlReferrer"), True)
        End Sub

    End Class

End Namespace