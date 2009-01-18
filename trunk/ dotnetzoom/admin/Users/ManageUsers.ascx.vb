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
Imports System.Text.RegularExpressions

Namespace DotNetZoom

    Public Class ManageUsers
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents txtFirstName As System.Web.UI.WebControls.TextBox
        Protected WithEvents valFirstName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtLastName As System.Web.UI.WebControls.TextBox
        Protected WithEvents valLastName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtUsername As System.Web.UI.WebControls.TextBox
        Protected WithEvents valUsername As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
        Protected WithEvents valPassword As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtConfirm As System.Web.UI.WebControls.TextBox
        Protected WithEvents valConfirm1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents valConfirm2 As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
        Protected WithEvents valEmail As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents rowAuthorized As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents chkAuthorized As System.Web.UI.WebControls.CheckBox
        Protected WithEvents Address1 As Address
        Protected WithEvents Message As System.Web.UI.WebControls.Label

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdManage As System.Web.UI.WebControls.LinkButton

        Protected WithEvents pnlAudit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblCreatedDate As System.Web.UI.WebControls.Label
        Protected WithEvents lblLastLoginDate As System.Web.UI.WebControls.Label
		
		Protected WithEvents pnlSecurite As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Country As System.Web.UI.WebControls.TextBox
        Protected WithEvents IPLow As System.Web.UI.WebControls.TextBox
        Protected WithEvents IPHigh As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdUpdateCode As System.Web.UI.WebControls.LinkButton

        Private userId As Integer = -1


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
            Title1.DisplayHelp = "DisplayHelp_ManageUsers"
            valFirstName.ErrorMessage = "<br>" + GetLanguage("need_firstname")
            valLastName.ErrorMessage = "<br>" + GetLanguage("need_lastname")
            valUsername.ErrorMessage = "<br>" + GetLanguage("need_username")
            valPassword.ErrorMessage = "<br>" + GetLanguage("need_password")
            valConfirm1.ErrorMessage = "<br>" + GetLanguage("need_password_confirm")
            valConfirm2.ErrorMessage = "<br>" + GetLanguage("need_password_match")
            valEmail.ErrorMessage = "<br>" + GetLanguage("need_email")
            cmdUpdate.Text = GetLanguage("enregistrer")
            cmdUpdateCode.Text = GetLanguage("enregistrer")
            cmdCancel.Text = GetLanguage("annuler")
            cmdDelete.Text = GetLanguage("delete")
            cmdManage.Text = GetLanguage("ManageUserRoles")

            ' get userid
            If IsNumeric(Request.Params("userid")) Then
                userId = Int32.Parse(Request.Params("userid"))
            End If

            ' security check for super user
            If userId = _portalSettings.SuperUserId And userId <> Int32.Parse(Context.User.Identity.Name) Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If

            ' If this is the first visit to the page, bind the role data to the datalist
            If Page.IsPostBack = False Then
                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm")) & "');")

                BindData()

                pnlSecurite.Visible = True
                If userId = _portalSettings.SuperUserId Then
                    Address1.Visible = False
                    rowAuthorized.Visible = False
                    cmdDelete.Visible = False
                    cmdManage.Visible = False

                    If Not Request.UrlReferrer Is Nothing Then
                        ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                    Else
                        ViewState("UrlReferrer") = ""
                    End If
                Else
                    ViewState("UrlReferrer") = GetFullDocument() & "?tabid=" & TabId & "&" & GetAdminPage() & "&filter=" & Request.Params("filter")
                End If
            End If

            txtPassword.Attributes.Add("value", txtPassword.Text)
            txtConfirm.Attributes.Add("value", txtConfirm.Text)

        End Sub


        '*******************************************************
        '
        ' The Save_Click server event handler on this page is used
        ' to save the current security settings to the configuration system
        '
        '*******************************************************

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub


        Private Sub cmdUpdateCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdateCode.Click
            ' Put In IP or Country restriction
            If userId <> -1 Then
                If IPLow.Text <> "" And IPHigh.Text <> "" Then
                    If IPConvert(IPLow.Text) = 0 Or IPConvert(IPHigh.Text) = 0 Then
                        Message.Text = GetLanguage("Valid_IP_Security")
                        Exit Sub
                    End If
                    If IPConvert(IPLow.Text) > IPConvert(IPHigh.Text) Then
                        Message.Text = GetLanguage("Valid_IP_Security1")
                        Exit Sub
                    End If
                End If
                ' check the country code to make sure it is a valid one
                Dim objUser As New UsersDB()
                objUser.UpdateUsercodes(userId, Country.Text, IPLow.Text, IPHigh.Text)
                Message.Text = GetLanguage("Valid_IP_Saved")
            Else
                Message.Text = GetLanguage("Valid_IP_Not_Saved")
            End If
        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' update the user record in the database
            Dim objUser As New UsersDB()
            Dim objSecurity As New PortalSecurity()
            Dim admin As New AdminDB()
            Dim strBody As String

            If userId = -1 Then
                userId = objUser.AddUser(_portalSettings.PortalId, txtFirstName.Text, txtLastName.Text, Address1.Unit, Address1.Street, Address1.City, Address1.Region, Address1.Postal, Address1.Country, Address1.Telephone, txtEmail.Text, txtUsername.Text, objSecurity.Encrypt(PortalSettings.GetHostSettings("EncryptionKey"), txtPassword.Text), chkAuthorized.Checked.ToString, userId)

                If userId < 0 Then
                    Message.Text = GetLanguage("UserName_Already_Used")
                    Message.Text = Regex.Replace(Message.Text, "{Username}", txtUsername.Text, RegexOptions.IgnoreCase)
                Else
                    ' Add user to Forum Dbase
                    Dim dbForumUser As New ForumUserDB()
                    dbForumUser.TTTForum_UserCreateUpdateDelete(userId, txtUsername.Text, True, False, "", "", "", _portalSettings.TimeZone, "", "", "", "", "", "", "", False, True, False, True, True, True, 0)
                    strBody = admin.GetSinglelonglanguageSettings(GetLanguage("N"), "email_newuser_account")
                    strBody = Regex.Replace(strBody, "{FullName}", txtFirstName.Text & " " & txtLastName.Text, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{PortalName}", _portalSettings.PortalName, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{PortalURL}", GetPortalDomainName(PortalAlias, Request), RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{Username}", txtUsername.Text, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{Password}", txtPassword.Text, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{AdministratorEmail}", _portalSettings.Email, RegexOptions.IgnoreCase)


                    If PortalSettings.GetSiteSettings(_portalSettings.PortalId).ContainsKey(GetLanguage("N") & "_signupmessage") Then
                        strBody = Regex.Replace(strBody, "{signupmessage}", CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)(GetLanguage("N") & "_signupmessage"), String), RegexOptions.IgnoreCase)
                    Else
                        strBody = Regex.Replace(strBody, "{signupmessage}", CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)(GetLanguage("N") & "signupmessage"), String), RegexOptions.IgnoreCase)
                    End If

                    If _portalSettings.UserRegistration = 3 Then
                        strBody = Regex.Replace(strBody, "{validationcode}", _portalSettings.PortalId.ToString & "-" & userId, RegexOptions.IgnoreCase)
                        strBody = Regex.Replace(strBody, "{needcode}", "", RegexOptions.IgnoreCase)
                        strBody = Regex.Replace(strBody, "{/needcode}", "", RegexOptions.IgnoreCase)
                    Else
                        strBody = Regex.Replace(strBody, "{needcode}[^¸]+{/needcode}", "", RegexOptions.IgnoreCase)
                    End If
                    If (Regex.IsMatch(strBody, "<html>", RegexOptions.IgnoreCase) = True) Then
                        SendNotification(_portalSettings.Email, txtEmail.Text, "", GetLanguage("Register_request") & " " & _portalSettings.PortalName, strBody, "", "html")
                    Else
                        SendNotification(_portalSettings.Email, txtEmail.Text, "", GetLanguage("Register_request") & " " & _portalSettings.PortalName, strBody, "", "")
                    End If

                    ViewState("UrlReferrer") = Replace(CType(ViewState("UrlReferrer"), String), "&filter=", "&filter=" & Left(txtFirstName.Text, 1))

                    Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

                End If
            Else
                Message.Text = ""
                If txtPassword.Text <> "" Or txtConfirm.Text <> "" Then
                    If txtPassword.Text <> txtConfirm.Text Then
                        Message.Text = GetLanguage("need_password_match")
                    End If
                End If
                If Message.Text = "" Then
                    ' if activating an account, send notification
                    If chkAuthorized.Checked Then
                        Dim dr As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, userId)
                        If dr.Read() Then
                            If dr("Authorized") <> chkAuthorized.Checked Then
                                strBody = admin.GetSinglelonglanguageSettings(GetLanguage("N"), "email_newuser_account")
                                strBody = Regex.Replace(strBody, "{FullName}", txtFirstName.Text & " " & txtLastName.Text, RegexOptions.IgnoreCase)
                                strBody = Regex.Replace(strBody, "{PortalName}", _portalSettings.PortalName, RegexOptions.IgnoreCase)
                                strBody = Regex.Replace(strBody, "{PortalURL}", GetPortalDomainName(PortalAlias, Request), RegexOptions.IgnoreCase)
                                strBody = Regex.Replace(strBody, "{Username}", txtUsername.Text, RegexOptions.IgnoreCase)
                                strBody = Regex.Replace(strBody, "{Password}", txtPassword.Text, RegexOptions.IgnoreCase)
                                strBody = Regex.Replace(strBody, "{AdministratorEmail}", _portalSettings.Email, RegexOptions.IgnoreCase)


                                If PortalSettings.GetSiteSettings(_portalSettings.PortalId).ContainsKey(GetLanguage("N") & "_signupmessage") Then
                                    strBody = Regex.Replace(strBody, "{signupmessage}", CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)(GetLanguage("N") & "_signupmessage"), String), RegexOptions.IgnoreCase)
                                Else
                                    strBody = Regex.Replace(strBody, "{signupmessage}", CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)(GetLanguage("N") & "signupmessage"), String), RegexOptions.IgnoreCase)
                                End If

                                If _portalSettings.UserRegistration = 3 Then
                                    strBody = Regex.Replace(strBody, "{validationcode}", _portalSettings.PortalId.ToString & "-" & dr("UserId").ToString, RegexOptions.IgnoreCase)
                                    strBody = Regex.Replace(strBody, "{needcode}", "", RegexOptions.IgnoreCase)
                                    strBody = Regex.Replace(strBody, "{/needcode}", "", RegexOptions.IgnoreCase)
                                Else
                                    strBody = Regex.Replace(strBody, "{needcode}[^¸]+{/needcode}", "", RegexOptions.IgnoreCase)
                                End If
                                If (Regex.IsMatch(strBody, "<html>", RegexOptions.IgnoreCase) = True) Then
                                    SendNotification(_portalSettings.Email, txtEmail.Text, "", GetLanguage("Register_request") & " " & _portalSettings.PortalName, strBody, "", "html")
                                Else
                                    SendNotification(_portalSettings.Email, txtEmail.Text, "", GetLanguage("Register_request") & " " & _portalSettings.PortalName, strBody, "", "")
                                End If
                            End If
                        End If

                        dr.Close()

                    End If

                    If IsNumeric(Request.Params("userid")) Then
                        userId = Int32.Parse(Request.Params("userid"))
                    End If

                    Dim Username As String = Nothing
                    Dim objreader As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, userId)
                    If objreader.Read Then
                        Username = objreader.Item("Username")
                    End If
                    objreader.Close()
                    objreader = Nothing

                    Dim dr2 As SqlDataReader = objUser.GetSingleUserByUsername(_portalSettings.PortalId, txtUsername.Text)
                    'if a user is found with that username and the username isn't our current user's username
                    If dr2.Read And txtUsername.Text <> Username Then
                        'username already exists in DB so show user an error message
                        Message.Text = GetLanguage("UserName_Already_Used")
                        Message.Text = Regex.Replace(Message.Text, "{Username}", txtUsername.Text, RegexOptions.IgnoreCase)
                    Else
                        'update the user
                        objUser.UpdateUser(_portalSettings.PortalId, userId, txtFirstName.Text, txtLastName.Text, Address1.Unit, Address1.Street, Address1.City, Address1.Region, Address1.Postal, Address1.Country, Address1.Telephone, txtEmail.Text, txtUsername.Text, IIf(txtPassword.Text <> "", objSecurity.Encrypt(PortalSettings.GetHostSettings("EncryptionKey"), txtPassword.Text), ""), chkAuthorized.Checked.ToString)

                        dr2.Close()
                        dr2 = Nothing

                        ' Redirect browser back to home page
                        Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

                    End If

                    dr2.Close()
                    dr2 = Nothing

                End If
            End If

        End Sub

        '*******************************************************
        '
        ' The BindData helper method is used to bind the list of
        ' security roles for this portal to an asp:datalist server control
        '
        '*******************************************************

        Sub BindData()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

            Address1.ModuleId = 0
            Address1.StartTabIndex = 8

            If userId <> -1 Then
                ' Bind the Email and Password
                Dim objUser As New UsersDB()
                Dim dr As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, userId)

                ' Read first row from database
                If dr.Read() Then
                    txtFirstName.Text = dr("FirstName").ToString
                    txtLastName.Text = dr("LastName").ToString
                    txtUsername.Text = dr("Username").ToString
                    txtEmail.Text = dr("Email").ToString
                    If userId <> _portalSettings.SuperUserId Then
                        chkAuthorized.Checked = dr("Authorized")
                    End If

                    Address1.Unit = dr("Unit").ToString
                    Address1.Street = dr("Street").ToString
                    Address1.City = dr("City").ToString
                    Address1.Region = dr("Region").ToString
                    Address1.Country = dr("Country").ToString
                    Address1.Postal = dr("PostalCode").ToString
                    Address1.Telephone = dr("Telephone").ToString

                    lblCreatedDate.Text = dr("CreatedDate").ToString
                    lblLastLoginDate.Text = dr("LastLoginDate").ToString
                End If
                dr.Close()

                dr = objUser.GetUserCountryCode(_portalSettings.PortalId, userId)
                If dr.Read() Then
                    Country.Text = IIf(IsDBNull(dr("Country_Code")), "", dr("Country_Code"))
                    IPLow.Text = IIf(IsDBNull(dr("IPfrom")), "", dr("IPfrom"))
                    IPHigh.Text = IIf(IsDBNull(dr("IPto")), "", dr("IPto"))
                End If
                If IPLow.Text = "" Then
                    IPLow.Text = "0.0.0.1"
                End If
                If IPHigh.Text = "" Then
                    IPHigh.Text = "255.255.255.255"
                End If
                dr.Close()
                valPassword.Enabled = False
                valConfirm1.Enabled = False
                valConfirm2.Enabled = False
                pnlSecurite.Visible = True
            Else
                chkAuthorized.Checked = True
                cmdDelete.Visible = False
                cmdManage.Visible = False
                pnlSecurite.Visible = False
                valPassword.Enabled = True
                valConfirm1.Enabled = True
                valConfirm2.Enabled = True
                pnlAudit.Visible = False
            End If

        End Sub

        Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' get user id from dropdownlist of users
            Dim users As New UsersDB()
            users.DeleteUser(_portalSettings.PortalId, userId)

            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

        End Sub

        Private Sub cmdManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManage.Click
            Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&UserId=" & userId & "&def=User Roles", True)
        End Sub

    End Class

End Namespace