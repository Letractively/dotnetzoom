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

    Public Class SecurityRoles
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents cboUsers As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cboRoles As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtExpiryDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdAdd As System.Web.UI.WebControls.LinkButton
        Protected WithEvents grdUserRoles As System.Web.UI.WebControls.DataGrid
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdExpiryCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents valExpiryDate As System.Web.UI.WebControls.CompareValidator
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Public RoleId As Integer = -1
        Public UserId As Integer = -1

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

        '*******************************************************
        '
        ' The Page_Load server event handler on this page is used
        ' to populate the role information for the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If Not PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If
            Title1.DisplayHelp = "DisplayHelp_SecurityRole"
            ' Obtain PortalSettings from Current Context
            valExpiryDate.ErrorMessage = "<br>" + GetLanguage("not_a_date")
            ' Verify that the current user has access to this page
            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = False Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If

            If IsNumeric(Request.Params("RoleId")) Then
                RoleId = Int32.Parse(Request.Params("RoleId"))
            End If

            If IsNumeric(Request.Params("UserId")) Then
                UserId = Int32.Parse(Request.Params("UserId"))
            End If

            ' If this is the first visit to the page, bind the role data to the datalist
            If Page.IsPostBack = False Then

                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm_erase_role")) & "');")
                cmdExpiryCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtExpiryDate)
				cmdExpiryCalendar.Text = GetLanguage("calendar")
				cmdAdd.Text = GetLanguage("add")
				cmdCancel.Text = GetLanguage("annuler")
				cmdDelete.Text = GetLanguage("delete")
                BindData()

                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = ""
                End If
            End If

            BindGrid()
        End Sub

        '*******************************************************
        '
        ' The BindData helper method is used to bind the list of
        ' security roles for this portal to an asp:datalist server control
        '
        '*******************************************************'

        Sub BindData()

            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)
            Dim objUser As New UsersDB()

		
            ' bind all portal roles to dropdownlist
            cboRoles.DataSource = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))
            cboRoles.DataBind()
            cboRoles.Items.Insert(0, New ListItem("<" & GetLanguage("Role_AllRoles") & ">", "-1"))
            If RoleId <> -1 Then
			    cboRoles.Items.FindByValue(RoleId.ToString).Selected = True
                cboRoles.Enabled = False
                 If RoleID = _portalSettings.AdministratorRoleId Or RoleID = _portalSettings.RegisteredRoleId Then
                    cmdDelete.Visible = False
				 else
				 	cmdDelete.Visible = true
                 End If
			 else
			 cmdDelete.Visible = false
            End If

            ' bind all portal users to dropdownlist
            cboUsers.DataSource = objUser.GetUsers(_portalSettings.PortalId)
            cboUsers.DataBind()
            cboUsers.Items.Insert(0, New ListItem("<" & GetLanguage("all") & ">", "-1"))
            If UserId <> -1 Then
                cboUsers.Items.FindByValue(UserId.ToString).Selected = True
                cboUsers.Enabled = False
                cmdDelete.Visible = False
            End If

        End Sub

        Sub BindGrid()

			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		
		
            Dim objUser As New UsersDB()

			
			grdUserRoles.Columns(1).HeaderText = GetLanguage("Name")
            grdUserRoles.Columns(2).HeaderText = GetLanguage("Label_UserName")
            grdUserRoles.Columns(3).HeaderText = GetLanguage("address_Email")
            grdUserRoles.Columns(4).HeaderText = GetLanguage("Role_PortalRole")
            grdUserRoles.Columns(5).HeaderText = GetLanguage("Role_Expiry_Date")
			
			
            If RoleId <> -1 Then
                grdUserRoles.DataSource = objUser.GetRoleMembership(_portalSettings.PortalId, GetLanguage("N"), RoleId)
                grdUserRoles.DataBind()
            End If
            If UserId <> -1 Then
                grdUserRoles.DataSource = objUser.GetRoleMembership(_portalSettings.PortalId, GetLanguage("N"),  , UserId)
                grdUserRoles.DataBind()
            End If
        End Sub

        Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

            Dim objUser As New UsersDB()
            Dim dr As SqlDataReader

            ' do not expire the portal Administrator account
            If cboUsers.SelectedItem.Value = _portalSettings.AdministratorId.ToString And cboRoles.SelectedItem.Value = _portalSettings.AdministratorRoleId.ToString Then
                txtExpiryDate.Text = ""
            End If

            If Int32.Parse(cboRoles.SelectedItem.Value) = -1 Then
                ' all roles
                dr = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))
                While dr.Read
                    objUser.AddUserRole(_portalSettings.PortalId, dr("RoleId"), cboUsers.SelectedItem.Value, CheckDateSql(txtExpiryDate.Text))
                End While
                dr.Close()
            Else
                If Int32.Parse(cboUsers.SelectedItem.Value) = -1 Then
                    ' all users
                    dr = objUser.GetUsers(_portalSettings.PortalId)
                    While dr.Read
                        objUser.AddUserRole(_portalSettings.PortalId, cboRoles.SelectedItem.Value, dr("UserId"), CheckDateSql(txtExpiryDate.Text))
                    End While
                    dr.Close()
                Else
                    ' single user/role
                    objUser.AddUserRole(_portalSettings.PortalId, cboRoles.SelectedItem.Value, cboUsers.SelectedItem.Value, CheckDateSql(txtExpiryDate.Text))
                End If
            End If

            BindGrid()
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)
        End Sub

        Public Sub grdUserRoles_Delete(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            Dim objUser As New UsersDB()

            objUser.DeleteUserRole(Integer.Parse(grdUserRoles.DataKeys(e.Item.ItemIndex).ToString()))

            grdUserRoles.EditItemIndex = -1

            BindGrid()
        End Sub

		
		
        Private Sub grdUserRoles_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdUserRoles.ItemCreated

            Dim cmdDeleteUserRole As Control = e.Item.FindControl("cmdDeleteUserRole")

            If Not cmdDeleteUserRole Is Nothing Then
                CType(cmdDeleteUserRole, ImageButton).Attributes.Add("onClick", "javascript: return confirm('" & rtesafe(GetLanguage("request_confirm_erase_userrole")) & "')")
            End If

        End Sub

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

 			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objUser As New UsersDB()
            Dim dr As SqlDataReader

            If RoleId <> -1 Then
                dr = objUser.GetRoleMembership(_portalSettings.PortalId, GetLanguage("N"), RoleId)
            End If
            If UserId <> -1 Then
                dr = objUser.GetRoleMembership(_portalSettings.PortalId,GetLanguage("N"), , UserId)
            End If

            While dr.Read
                objUser.DeleteUserRole(dr("UserRoleId"))
            End While
            dr.Close()

            BindGrid()

        End Sub
    End Class

End Namespace