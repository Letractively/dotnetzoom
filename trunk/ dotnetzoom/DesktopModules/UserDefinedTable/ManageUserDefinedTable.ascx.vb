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

    Public Class ManageUserDefinedTable
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents cmdAddField As System.Web.UI.WebControls.LinkButton
        Protected WithEvents grdFields As System.Web.UI.WebControls.DataGrid
        Protected WithEvents cboSortField As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cboSortOrder As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
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
			Title1.DisplayTitle = getlanguage("title_manage_user_table")
			Title1.DisplayHelp = "DisplayHelp_MAnageUserDefinedTable"
			cmdAddField.Text = GetLanguage("AddField")
			cmdCancel.Text = GetLanguage("annuler")
            If Page.IsPostBack = False Then

                BindData()

                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = Replace(Request.UrlReferrer.ToString(), "insertrow=true&", "")

            End If

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click, cmdCancel.Click
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

        Public Sub grdFields_CancelEdit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            grdFields.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub grdFields_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            grdFields.EditItemIndex = e.Item.ItemIndex
            grdFields.SelectedIndex = -1
            BindData()
        End Sub

        Sub grdFields_Item_Bound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            Dim itemType As ListItemType
            itemType = CType(e.Item.ItemType, ListItemType)
            If (itemType = ListItemType.EditItem) Then
                Dim chkCheckbox2 As WebControls.CheckBox
                Dim lblCheckBox2 As WebControls.Label
                chkCheckbox2 = CType(e.Item.FindControl("Checkbox2"), WebControls.CheckBox)
                lblCheckBox2 = CType(e.Item.FindControl("lblCheckBox2"), WebControls.Label)
                lblCheckBox2.Text = "<label style=""display:none;"" for=""" & chkCheckbox2.ClientID.ToString & """>Visible</label>"

                Dim txtFieldTitle As WebControls.TextBox
                Dim lblFieldTitle As WebControls.Label
                txtFieldTitle = CType(e.Item.FindControl("txtFieldTitle"), WebControls.TextBox)
                lblFieldTitle = CType(e.Item.FindControl("lblFieldTitle"), WebControls.Label)
                lblFieldTitle.Text = "<label style=""display:none;"" for=""" & txtFieldTitle.ClientID.ToString & """>Title</label>"

                Dim cboFieldType As WebControls.DropDownList
				
                Dim lblFieldType As WebControls.Label
                cboFieldType = CType(e.Item.FindControl("cboFieldType"), WebControls.DropDownList)
				
				cboFieldType.items.FindByValue("String").Text = GetLanguage("String")
				cboFieldType.items.FindByValue("Int32").Text = GetLanguage("Int32")
				cboFieldType.items.FindByValue("Decimal").Text = GetLanguage("Decimal")
				cboFieldType.items.FindByValue("DateTime").Text = GetLanguage("DateTime")
				cboFieldType.items.FindByValue("Boolean").Text = GetLanguage("Boolean")
                lblFieldType = CType(e.Item.FindControl("lblFieldType"), WebControls.Label)
                lblFieldType.Text = "<label style=""display:none;"" for=""" & cboFieldType.ClientID.ToString & """>Type</label>"

            End If
        End Sub
        Public Sub grdFields_Update(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)

            Dim chkVisible As CheckBox = CType(e.Item.FindControl("Checkbox2"), WebControls.CheckBox)
            Dim txtFieldTitle As TextBox = CType(e.Item.FindControl("txtFieldTitle"), WebControls.TextBox)
            Dim cboFieldType As DropDownList = CType(e.Item.FindControl("cboFieldType"), WebControls.DropDownList)

            If txtFieldTitle.Text <> "" Then
                Dim objUserDefinedTable As New UserDefinedTableDB()

                If Integer.Parse(grdFields.DataKeys(e.Item.ItemIndex).ToString()) = -1 Then
                    objUserDefinedTable.AddUserDefinedField(ModuleId, txtFieldTitle.Text, chkVisible.Checked, cboFieldType.SelectedItem.Value)
                Else
                    objUserDefinedTable.UpdateUserDefinedField(Integer.Parse(grdFields.DataKeys(e.Item.ItemIndex).ToString()), txtFieldTitle.Text, chkVisible.Checked, cboFieldType.SelectedItem.Value)
                End If

                grdFields.EditItemIndex = -1
                BindData()
            Else
                grdFields.EditItemIndex = -1
                BindData()
            End If
        End Sub

        Public Sub grdFields_Delete(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            Dim objUserDefinedTable As New UserDefinedTableDB()

            objUserDefinedTable.DeleteUserDefinedField(Integer.Parse(grdFields.DataKeys(e.Item.ItemIndex).ToString()))

            grdFields.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub grdFields_Move(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Dim objUserDefinedTable As New UserDefinedTableDB()

            Select Case e.CommandArgument.ToString
                Case "Up"
                    objUserDefinedTable.UpdateUserDefinedFieldOrder(Integer.Parse(grdFields.DataKeys(e.Item.ItemIndex).ToString()), -1)
                    BindData()
                Case "Down"
                    objUserDefinedTable.UpdateUserDefinedFieldOrder(Integer.Parse(grdFields.DataKeys(e.Item.ItemIndex).ToString()), 1)
                    BindData()
            End Select

        End Sub

        Private Sub cmdAddField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddField.Click
            grdFields.EditItemIndex = 0
            BindData(True)
        End Sub

        Private Sub BindData(Optional ByVal blnInsertField As Boolean = False)
            Dim objUserDefinedTable As New UserDefinedTableDB()
            Dim dr As SqlDataReader = objUserDefinedTable.GetUserDefinedFields(ModuleId)
            Dim ds As DataSet

            ds = ConvertDataReaderToDataSet(dr)
            dr.Close()

            ' inserting a new field
            If blnInsertField Then
                Dim row As DataRow
                row = ds.Tables(0).NewRow()
                row("UserDefinedFieldId") = "-1"
                row("FieldTitle") = ""
                row("Visible") = True
                row("FieldType") = "String"
                ds.Tables(0).Rows.InsertAt(row, 0)
                grdFields.EditItemIndex = 0
            End If

            grdFields.DataSource = ds
            grdFields.DataBind()
			grdFields.Columns(1).HeaderText = GetLanguage("grdFields_visible")
			grdFields.Columns(2).HeaderText = GetLanguage("grdFields_Title")
			grdFields.Columns(3).HeaderText = GetLanguage("grdFields_Type")


            cboSortField.DataSource = objUserDefinedTable.GetUserDefinedFields(ModuleId)
            cboSortField.DataBind()
            cboSortField.Items.Insert(0, New ListItem(GetLanguage("list_none"), ""))

            ' Get settings from the database
            Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)

            cboSortField.ClearSelection()
            If Not cboSortField.Items.FindByValue(CType(settings("sortfield"), String)) Is Nothing Then
                cboSortField.Items.FindByValue(CType(settings("sortfield"), String)).Selected = True
            End If
            cboSortOrder.ClearSelection()
   			cboSortOrder.Items.FindByValue("").Text = GetLanguage("list_none")
			cboSortOrder.Items.FindByValue("ASC").Text = GetLanguage("UO_ASC")
			cboSortOrder.Items.FindByValue("DESC").Text = GetLanguage("UO_DESC")

            If Not cboSortOrder.Items.FindByValue(CType(settings("sortorder"), String)) Is Nothing Then
                cboSortOrder.Items.FindByValue(CType(settings("sortorder"), String)).Selected = True
            End If
        End Sub

        Private Sub grdFields_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdFields.ItemCreated

            Dim cmdDeleteUserDefinedField As Control = e.Item.FindControl("cmdDeleteUserDefinedField")

            If Not cmdDeleteUserDefinedField Is Nothing Then
                CType(cmdDeleteUserDefinedField, ImageButton).Attributes.Add("onClick", "javascript: return confirm('" & rtesafe(GetLanguage("request_confirm")) & "')")
            End If

            If e.Item.ItemType = ListItemType.Header Then
                e.Item.Cells(1).Attributes.Add("Scope", "col")
                e.Item.Cells(2).Attributes.Add("Scope", "col")
                e.Item.Cells(3).Attributes.Add("Scope", "col")
            End If

        End Sub

        Private Sub cboSortField_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSortField.SelectedIndexChanged
            ' Update settings in the database
            Dim admin As New AdminDB()

            If Not cboSortField.SelectedItem Is Nothing Then
                admin.UpdateModuleSetting(ModuleId, "sortfield", cboSortField.SelectedItem.Value)
            End If

            BindData()
        End Sub

        Private Sub cboSortOrder_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSortOrder.SelectedIndexChanged
            ' Update settings in the database
            Dim admin As New AdminDB()

            If Not cboSortOrder.SelectedItem Is Nothing Then
                admin.UpdateModuleSetting(ModuleId, "sortorder", cboSortOrder.SelectedItem.Value)
            End If

            BindData()
        End Sub

        Public Function GetFieldTypeName(ByVal strFieldType As String) As String
            Select Case strFieldType
                Case "String" : Return GetLanguage("String")
                Case "Int32" : Return GetLanguage("Int32")
                Case "Decimal" : Return GetLanguage("Decimal")
                Case "DateTime" : Return GetLanguage("DateTime")
                Case "Boolean" : Return GetLanguage("Boolean")
                Case Else : Return GetLanguage("String")
            End Select
        End Function

        Public Function GetFieldTypeIndex(ByVal strFieldType As String) As Integer
            Select Case strFieldType
                Case "String" : Return 0
                Case "Int32" : Return 1
                Case "Decimal" : Return 2
                Case "DateTime" : Return 3
                Case "Boolean" : Return 4
                Case Else : Return 0
            End Select
        End Function

    End Class

End Namespace