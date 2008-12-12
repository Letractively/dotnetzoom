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

    Public Class EditSearch
        Inherits DotNetZoom.PortalModuleControl
		
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Protected WithEvents txtResults As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkDescription As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkBreadcrumbs As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkAudit As System.Web.UI.WebControls.CheckBox
        Protected WithEvents cboTables As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdAdd As System.Web.UI.WebControls.LinkButton
        Protected WithEvents grdCriteria As System.Web.UI.WebControls.DataGrid

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton

        Public arrFields As ArrayList

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

            Title1.DisplayHelp = "DisplayHelp_EditSearch"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			cmdUpdate.Text = getLanguage("enregistrer")
			cmdCancel.Text = getlanguage("annuler")
			cmdAdd.Text = getlanguage("add")
            If Page.IsPostBack = False Then

                If ModuleId > 0 Then

                    txtResults.Text = CType(Settings("maxresults"), String)
                    txtTitle.Text = CType(Settings("maxtitle"), String)
                    txtDescription.Text = CType(Settings("maxdescription"), String)
                    chkDescription.Checked = CType(Settings("showdescription"), Boolean)
                    chkAudit.Checked = CType(Settings("showaudit"), Boolean)
                    chkBreadcrumbs.Checked = CType(Settings("showbreadcrumbs"), Boolean)

                End If

                Dim objSearch As New SearchDB()

                cboTables.DataSource = objSearch.GetTables
                cboTables.DataBind()

                BindData()

                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = "~" & GetDocument() & "?tabid=" & TabId

            End If

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            ' Update settings in the database
            Dim admin As New AdminDB()

            admin.UpdateModuleSetting(ModuleId, "maxresults", txtResults.Text)
            admin.UpdateModuleSetting(ModuleId, "maxtitle", txtTitle.Text)
            admin.UpdateModuleSetting(ModuleId, "maxdescription", txtDescription.Text)
            admin.UpdateModuleSetting(ModuleId, "showdescription", chkDescription.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "showaudit", chkAudit.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "showbreadcrumbs", chkBreadcrumbs.Checked.ToString)
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub


        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

        Private Sub BindData(Optional ByVal intSearchId As Integer = -1)

            Dim objSearch As New SearchDB()

            If intSearchId <> -1 Then
                arrFields = objSearch.GetFields(intSearchId, ModuleId)
            End If
			grdCriteria.Columns(1).HeaderText = GetLanguage("search_header_table")
			grdCriteria.Columns(2).HeaderText = GetLanguage("search_header_title")
			grdCriteria.Columns(3).HeaderText = GetLanguage("search_header_desc")
			grdCriteria.Columns(4).HeaderText = GetLanguage("search_header_update")
			grdCriteria.Columns(5).HeaderText = GetLanguage("search_header_who")
            grdCriteria.DataSource = objSearch.GetSearch(ModuleId)
            grdCriteria.DataBind()

        End Sub

        Public Function SelectField(ByVal objFieldValue As Object) As String
            Dim intField As Integer

            If IsDBNull(objFieldValue) Then
                Return "0"
            Else
                For intField = 0 To arrFields.Count - 1
                    If objFieldValue = arrFields(intField) Then
                        Return intField.ToString
                    End If
                Next intField
            End If
        End Function

        Private Sub grdCriteria_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdCriteria.ItemCreated
            Dim cmdDeleteCriteria As Control = e.Item.FindControl("cmdDeleteCriteria")
            If Not cmdDeleteCriteria Is Nothing Then
                CType(cmdDeleteCriteria, ImageButton).Attributes.Add("onClick", "javascript: return confirm('" & rtesafe(GetLanguage("request_confirm")) & "')")
            End If
        End Sub

        Public Sub grdCriteria_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            grdCriteria.EditItemIndex = e.Item.ItemIndex
            grdCriteria.SelectedIndex = -1

            BindData(Integer.Parse(grdCriteria.DataKeys(e.Item.ItemIndex).ToString()))
        End Sub

        Public Sub grdCriteria_Delete(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            Dim objSearch As New SearchDB()

            objSearch.DeleteSearch(Integer.Parse(grdCriteria.DataKeys(e.Item.ItemIndex).ToString()))

            grdCriteria.EditItemIndex = -1

            BindData()
        End Sub

        Public Sub grdCriteria_Update(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)

            Dim cboTitleField As DropDownList = e.Item.Cells(2).Controls(1)
            Dim cboDescriptionField As DropDownList = e.Item.Cells(3).Controls(1)
            Dim cboCreatedDateField As DropDownList = e.Item.Cells(4).Controls(1)
            Dim cboCreatedByUserField As DropDownList = e.Item.Cells(5).Controls(1)

            Dim strTitleField as String = ""
            If cboTitleField.SelectedIndex <> -1 And cboTitleField.SelectedIndex <> 0 Then
                strTitleField = cboTitleField.SelectedItem.Value
            End If
            Dim strDescriptionField as String = ""
            If cboDescriptionField.SelectedIndex <> -1 And cboDescriptionField.SelectedIndex <> 0 Then
                strDescriptionField = cboDescriptionField.SelectedItem.Value
            End If
            Dim strCreatedDateField as String = ""
            If cboCreatedDateField.SelectedIndex <> -1 And cboCreatedDateField.SelectedIndex <> 0 Then
                strCreatedDateField = cboCreatedDateField.SelectedItem.Value
            End If
            Dim strCreatedByUserField as String= ""
            If cboCreatedByUserField.SelectedIndex <> -1 And cboCreatedByUserField.SelectedIndex <> 0 Then
                strCreatedByUserField = cboCreatedByUserField.SelectedItem.Value
            End If

            Dim objSearch As New SearchDB()

            objSearch.UpdateSearch(Integer.Parse(grdCriteria.DataKeys(e.Item.ItemIndex).ToString()), strTitleField, strDescriptionField, strCreatedDateField, strCreatedByUserField)

            grdCriteria.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub grdCriteria_CancelEdit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            grdCriteria.EditItemIndex = -1
            BindData()
        End Sub

        Private Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

            Dim objSearch As New SearchDB()

            If Not cboTables.SelectedItem Is Nothing Then
                objSearch.AddSearch(ModuleId, cboTables.SelectedItem.Value)
            End If

            BindData()

        End Sub

    End Class

End Namespace