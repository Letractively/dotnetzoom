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
    Public Class EditRoles

        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents pnlServicesFee As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents pnlAssignation As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtRoleName As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtServiceFee As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtBillingPeriod As System.Web.UI.WebControls.TextBox
        Protected WithEvents cboBillingFrequency As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtTrialFee As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtTrialPeriod As System.Web.UI.WebControls.TextBox
        Protected WithEvents cboTrialFrequency As System.Web.UI.WebControls.DropDownList
        Protected WithEvents chkIsPublic As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkAutoAssignment As System.Web.UI.WebControls.CheckBox
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
		Protected WithEvents chkUpload As System.Web.UI.WebControls.CheckBox
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdManage As System.Web.UI.WebControls.LinkButton
		Protected WithEvents valRoleName As System.Web.UI.WebControls.RequiredFieldValidator
		Protected WithEvents valServiceFee1 As System.Web.UI.WebControls.comparevalidator
		Protected WithEvents valServiceFee2 As System.Web.UI.WebControls.comparevalidator
		Protected WithEvents valBillingPeriod1 As System.Web.UI.WebControls.comparevalidator
		Protected WithEvents valBillingPeriod2 As System.Web.UI.WebControls.comparevalidator
		Protected WithEvents valTrialFee1 As System.Web.UI.WebControls.comparevalidator
		Protected WithEvents valTrialFee2 As System.Web.UI.WebControls.comparevalidator
		Protected WithEvents valTrialPeriod1 As System.Web.UI.WebControls.comparevalidator
		Protected WithEvents valTrialPeriod2 As System.Web.UI.WebControls.comparevalidator
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Private RoleID As Integer = -1

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
            If Not PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If
            Title1.DisplayHelp = "DisplayHelp_EditRole"

            valRoleName.ErrorMessage = "<br>" + GetLanguage("need_role_name")
            valServiceFee1.ErrorMessage = "<br>" + GetLanguage("bad_value_services")
            valServiceFee2.ErrorMessage = "<br>" + GetLanguage("bad_value_services0")
            valBillingPeriod1.ErrorMessage = "<br>" + GetLanguage("bad_billing_period")
            valBillingPeriod2.ErrorMessage = "<br>" + GetLanguage("bad_billing_period0")
            valTrialFee1.ErrorMessage = "<br>" + GetLanguage("bad_trial_fee")
            valTrialFee2.ErrorMessage = "<br>" + GetLanguage("bad_trial_fee0")
            valTrialPeriod1.ErrorMessage = "<br>" + GetLanguage("bad_trial_period")
            valTrialPeriod2.ErrorMessage = "<br>" + GetLanguage("bad_trial_period0")
            cmdUpdate.Text = GetLanguage("enregistrer")
            cmdCancel.Text = GetLanguage("annuler")
            cmdDelete.Text = GetLanguage("delete")
            cmdManage.Text = GetLanguage("ManageRoles")



            If IsNumeric(Request.Params("RoleID")) Then
                RoleID = Int32.Parse(Request.Params("RoleID"))
            End If

            If Page.IsPostBack = False Then
                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm")) & "');")

                Dim objUser As New UsersDB()
                Dim objAdmin As New AdminDB()


                Dim dt As New DataTable()
                Dim drw As DataRow

                dt.Columns.Add(New DataColumn("Code", GetType(String)))
                dt.Columns.Add(New DataColumn("Description", GetType(String)))

                Dim Result As SqlDataReader = objAdmin.GetBillingFrequencyCodes(GetLanguage("N"))
                While Result.Read()
                    drw = dt.NewRow()
                    drw(0) = Result(0)
                    drw(1) = Result(2)
                    dt.Rows.Add(drw)
                End While
                Result.Close()
                Dim dv As New DataView(dt)


                cboBillingFrequency.DataSource = dv
                cboBillingFrequency.DataBind()
                cboBillingFrequency.Items.FindByValue("N").Selected = True


                cboTrialFrequency.DataSource = dv
                cboTrialFrequency.DataBind()
                cboTrialFrequency.Items.FindByValue("N").Selected = True

                If RoleID <> -1 Then

                    Dim dr As SqlDataReader = objUser.GetSingleRole(RoleID, GetLanguage("N"))
                    If dr.Read() Then
                        txtRoleName.Text = dr("RoleName").ToString
                        txtDescription.Text = dr("Description").ToString
                        If Format(dr("ServiceFee"), "#,##0.00") <> "0.00" Then
                            txtServiceFee.Text = Format(dr("ServiceFee"), "#,##0.00")
                            txtBillingPeriod.Text = dr("BillingPeriod").ToString
                            If Not cboBillingFrequency.Items.FindByValue(dr("BillingFrequency").ToString) Is Nothing Then
                                cboBillingFrequency.ClearSelection()
                                cboBillingFrequency.Items.FindByValue(dr("BillingFrequency").ToString).Selected = True
                            End If
                        End If
                        If dr("TrialFrequency").ToString <> "N" Then
                            txtTrialFee.Text = Format(dr("TrialFee"), "#,##0.00")
                            txtTrialPeriod.Text = dr("TrialPeriod").ToString
                            If Not cboTrialFrequency.Items.FindByValue(dr("TrialFrequency").ToString) Is Nothing Then
                                cboTrialFrequency.ClearSelection()
                                cboTrialFrequency.Items.FindByValue(dr("TrialFrequency").ToString).Selected = True
                            End If
                        End If
                        chkIsPublic.Checked = dr("IsPublic")
                        chkAutoAssignment.Checked = dr("AutoAssignment")
                        dr.Close()
                    Else ' security violation attempt to access item not related to this Module
                        dr.Close()
                        Response.Redirect(GetFullDocument() & "?tabid=" & TabId & Request.Params("tabid") & "&" & GetAdminPage(), True)
                    End If
                    Dim Admin As New AdminDB()
                    Dim UploadRoles As String = _portalSettings.AdministratorRoleId.ToString & ";"
                    If Not CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("uploadroles"), String) Is Nothing Then
                        UploadRoles = CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("uploadroles"), String)
                    End If
                    If InStr(1, UploadRoles, RoleID.ToString() & ";") Then
                        chkUpload.Checked = True
                    Else
                        chkUpload.Checked = False
                    End If

                    If RoleID = _portalSettings.AdministratorRoleId Then
                        chkUpload.Checked = True
                    End If


                    If RoleID = _portalSettings.AdministratorRoleId Or RoleID = _portalSettings.RegisteredRoleId Then
                        cmdDelete.Visible = False
                        pnlServicesFee.Visible = False
                        pnlAssignation.Visible = False
                    Else
                        pnlServicesFee.Visible = True
                        pnlAssignation.Visible = True
                    End If

                    If RoleID = _portalSettings.RegisteredRoleId Then
                        cmdManage.Visible = False
                    End If
                Else
                    cmdDelete.Visible = False
                    cmdManage.Visible = False
                End If

            End If

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            If Page.IsValid Then
                Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

                Dim dblServiceFee As Double = 0
                Dim intBillingPeriod As Integer = 1
                Dim strBillingFrequency As String = "N"

                If txtServiceFee.Text <> "" Then
                    dblServiceFee = Double.Parse(txtServiceFee.Text)
                    If txtBillingPeriod.Text <> "" Then
                        intBillingPeriod = Integer.Parse(txtBillingPeriod.Text)
                    End If
                    strBillingFrequency = cboBillingFrequency.SelectedItem.Value
                End If

                Dim dblTrialFee As Double = 0
                Dim intTrialPeriod As Integer = 1
                Dim strTrialFrequency As String = "N"

                If dblServiceFee <> 0 And cboTrialFrequency.SelectedItem.Value <> "N" Then
                    If txtTrialFee.Text <> "" Then
                        dblTrialFee = Double.Parse(txtTrialFee.Text)
                    End If
                    If txtTrialPeriod.Text <> "" Then
                        intTrialPeriod = Integer.Parse(txtTrialPeriod.Text)
                    End If
                    strTrialFrequency = cboTrialFrequency.SelectedItem.Value
                End If

                Dim objUser As New UsersDB()

                If RoleID = -1 Then
                    objUser.AddRole(_portalSettings.PortalId, txtRoleName.Text, txtDescription.Text, dblServiceFee, intBillingPeriod, strBillingFrequency, dblTrialFee, intTrialPeriod, strTrialFrequency, chkIsPublic.Checked, chkAutoAssignment.Checked)
                Else
                    objUser.UpdateRole(RoleID, GetLanguage("N"), txtRoleName.Text, txtDescription.Text, dblServiceFee, intBillingPeriod, strBillingFrequency, dblTrialFee, intTrialPeriod, strTrialFrequency, chkIsPublic.Checked, chkAutoAssignment.Checked)
                    Dim Admin As New AdminDB()
                    Dim UploadRoles As String = _portalSettings.AdministratorRoleId.ToString & ";"
                    If Not CType(Settings("uploadroles"), String) Is Nothing Then
                        UploadRoles = CType(Settings("uploadroles"), String)
                    End If

                    If RoleID = _portalSettings.AdministratorRoleId Then
                        chkUpload.Checked = True
                    End If


                    If chkUpload.Checked Then
                        If InStr(1, UploadRoles, RoleID.ToString() & ";") = 0 Then
                            ' Add 
                            UploadRoles += RoleID.ToString() & ";"
                        End If
                    Else
                        UploadRoles = Replace(UploadRoles, RoleID.ToString() & ";", "")
                    End If
                    Admin.UpdatePortalSetting(_portalSettings.PortalId, "uploadroles", UploadRoles)


                End If

                Response.Redirect(GetFullDocument() & "?tabid=" & TabId & Request.Params("tabid") & "&" & GetAdminPage(), True)

            End If
        End Sub

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
            Dim objUser As New UsersDB()

            objUser.DeleteRole(RoleID)

            Response.Redirect(GetFullDocument() & "?tabid=" & TabId & "&" & GetAdminPage(), True)

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            Response.Redirect(GetFullDocument() & "?tabid=" & TabId & "&" & GetAdminPage(), True)
        End Sub

        Private Sub cmdManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManage.Click
            Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&RoleId=" & RoleID & "&def=User Roles", True)
        End Sub

        Public Function FormatURL(ByVal strKeyName As String, ByVal strKeyValue As String) As String
            FormatURL = EditURL(strKeyName, strKeyValue) & "&RoleID=" & RoleID.ToString()
        End Function

        Public Function DisplayAddress(ByVal Unit As Object, ByVal Street As Object, ByVal City As Object, ByVal Region As Object, ByVal Country As Object, ByVal PostalCode As Object) As String
            DisplayAddress = FormatAddress(Unit, Street, City, Region, Country, PostalCode)
        End Function

        Public Function DisplayEmail(ByVal Email As Object) As String
            DisplayEmail = FormatEmail(Email, page)
        End Function

        Public Function DisplayLastLogin(ByVal LastLogin As Object) As String
            If Not IsDBNull(LastLogin) Then
                DisplayLastLogin = Format(LastLogin, "yyyy/MM/dd")
            Else
                DisplayLastLogin = ""
            End If
        End Function
		
		
    End Class

End Namespace