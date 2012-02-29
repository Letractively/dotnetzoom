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

Imports System.Web.Security
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Namespace DotNetZoom

    Public MustInherit Class Signin
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents SignInTitlebefore As System.Web.UI.WebControls.Literal
		Protected WithEvents SignInTitleafter As System.Web.UI.WebControls.Literal
		Protected WithEvents SignInbefore As System.Web.UI.WebControls.Literal
		Protected WithEvents SignInafter As System.Web.UI.WebControls.Literal
        Protected WithEvents txtUsername As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
        Protected WithEvents rowVerification1 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents txtVerification As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkCookie As System.Web.UI.WebControls.CheckBox
        Protected WithEvents cmdLogin As System.Web.UI.WebControls.Button
        Protected WithEvents cmdRegister As System.Web.UI.WebControls.Button
        Protected WithEvents cmdSendPassword As System.Web.UI.WebControls.Button
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
        Protected WithEvents lblLogin As System.Web.UI.WebControls.Label
		Protected WithEvents help As System.Web.UI.WebControls.HyperLink
		Protected WithEvents TableTitle As System.Web.UI.WebControls.Literal
		Protected WithEvents ItemTitle As System.Web.UI.WebControls.Literal

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
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			
			Help.ToolTip = getlanguage("title_enter")
			Help.Visible = True
            help.NavigateUrl = "javascript:var m = window.open('" + glbHTTP() + "admin/tabs/help.aspx?help=DisplayHelp_Signin&TabId=" & _portalSettings.ActiveTab.TabId & "&L=" & GetLanguage("N") & "', 'help', 'width=640,height=400,left=100,top=100,titlebar=0,scrollbars=1,menubar=0,status=0,location=0,resizable=1');m.focus();"

     		Dim _Setting As HashTable = PortalSettings.GetSiteSettings(_portalSettings.PortalID)
			If _Setting("loginModuleContainer") <> "" then
				Dim arrContainer As Array = SplitContainer(_Setting("loginModuleContainer"), _portalSettings.UploadDirectory,  IIf(_Setting("logincontainerAlignment") <> "", _Setting("logincontainerAlignment"), ""), IIf(_Setting("logincontainerColor") <> "", _Setting("logincontainerColor"), ""), IIf(_Setting("logincontainerBorder") <> "", _Setting("logincontainerBorder"), ""))
				SignInbefore.text = arrContainer(0)
				SignInbefore.Visible = True
				SignInafter.Text = arrContainer(1)
				SignInafter.Visible = True
		 	end if

			If _Setting("loginTitleContainer") <> "" then
				Dim TarrContainer As Array = SplitContainer(_Setting("loginTitleContainer"), _portalSettings.UploadDirectory,  IIf(_Setting("logincontainerAlignment") <> "", _Setting("logincontainerAlignment"), ""), IIf(_Setting("logincontainerColor") <> "", _Setting("logincontainerColor"), ""), IIf(_Setting("logincontainerBorder") <> "", _Setting("logincontainerBorder"), ""))
				SignInTitlebefore.text = TarrContainer(0)
				SignInTitlebefore.Visible = True
				SignInTitleafter.Text = TarrContainer(1)
				SignInTitleafter.Visible = True
		 	end if
   

			If _Setting("logincontainerTitleHeaderClass") <> "" then
			TableTitle.Text = _Setting("logincontainerTitleHeaderClass")
			else
			TableTitle.Text = "headertitle"
			end if
			
			If _Setting("logincontainerTitleCSSClass") <> "" then
			ItemTitle.Text = _Setting("logincontainerTitleCSSClass")
			else
			ItemTitle.Text =  "ItemTitle"
			end if


            chkCookie.Text = GetLanguage("Login_Keep")
            chkCookie.Checked = True
			cmdLogin.Text = getlanguage("Button_Enter")
			cmdLogin.Tooltip = getlanguage("Button_EnterTooltip")
			cmdRegister.Text = getlanguage("Button_Register")
			cmdRegister.Tooltip = getlanguage("Button_RegisterTooltip")
		    cmdSendPassword.Text = getlanguage("Button_Password")
			cmdSendPassword.Tooltip = getlanguage("Button_PasswordTooltip")
            If _portalSettings.UserRegistration = 0 Then
                cmdRegister.Visible = False
            End If

            txtPassword.Attributes.Add("value", txtPassword.Text)

            If Page.IsPostBack = False Then
                Try
                    SetFormFocus(txtUsername)
                Catch
                    'control not there or error setting focus
                End Try
            End If

            
			If portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey(GetLanguage("N") & "_loginmessage") then
			lblLogin.Text = CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)(GetLanguage("N") & "_loginmessage"), String)
			else 
            lblLogin.Text = CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)("loginmessage"), String)
			end if
        End Sub

        Private Sub cmdLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdLogin.Click

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)


			
            Dim objUser As New UsersDB()
            Dim objSecurity As New PortalSecurity()
			Dim Admin as New AdminDB()
            Dim blnLogin As Boolean = True
			Dim CanLogin As Boolean = True
			Dim Attempt As Integer = 0
			' Check user security, last loggin attempt, and guid

			Dim Sdr As SqlDataReader = objUser.CheckUserSecurity(_portalSettings.PortalId, txtUsername.Text)

			If Sdr.Read() Then
				If Sdr("Code").ToString <> "" then
				' mean that we have an attempt for first login after creation of a portal
				If (Request.Params("validate") Is Nothing) or Request.Params("validate") <> Sdr("Code").ToString then
				CanLogin = False
				lblMessage.Text = ProcessLanguage(admin.getsinglelonglanguageSettings(GetLanguage("N"), "Security_Enter_Portal"), page)
				end if
				if Request.Params("validate") = Sdr("Code").ToString then
				objUser.UpdateCheckUserSecurity(Sdr("UserID"), "", DateTime.now.ADDYears(-30), 0)
				end if
				else
				
			  If Sdr("NextLogin").ToString <> "" then
			  ' No Problem first attempt try, userid, code, nextlogin
			  ' if the record is empty then no failed attempts
			  ' if ntry > 3 then cannot attempt to logging again for another 10 minutes
			  Attempt = IIf(IsDBNull(Sdr("Try")), 0, Sdr("Try"))

				If CType(Sdr("NextLogin"), DateTime) < DateTime.Now() then
				  CanLogin = True
				  lblMessage.Text = ""
				  objUser.UpdateCheckUserSecurity(Sdr("UserID"), "", DateTime.now.ADDYears(-30), 0)
				Else
			    If Attempt > 3 then
				' he need to wait
				CanLogin = False
				RegisterBADip(Request.UserHostAddress)
				lblMessage.Text = ProcessLanguage(Admin.getsinglelonglanguageSettings(GetLanguage("N"), "Security_Enter_PortalWait"), page)
			    objUser.UpdateCheckUserSecurity(Sdr("UserId"), "", DateTime.now.ADDMinutes(10), Attempt +1)
				  end if
				end If
			  end If
			end if
			end if
			Sdr.Close()

		If canLogin	then	
            If _portalSettings.UserRegistration = 3 Then ' verified

				Dim dr As SqlDataReader = objUser.GetSingleUserByUsername(_portalSettings.PortalId, txtUsername.Text)
                If dr.Read() Then
                    If dr("LastLoginDate").ToString = "" And dr("IsSuperUser") = False Then
                        blnLogin = False
                        If rowVerification1.Visible Then
                            If txtVerification.Text <> "" Then
                                If txtVerification.Text = (_portalSettings.PortalId.ToString & "-" & dr("UserId").ToString) Then
                                    blnLogin = True
                                Else
								    RegisterBADip(Request.UserHostAddress)
                                    lblMessage.Text = getlanguage("RegisterMessage5") 
                                End If
                            Else
                                lblMessage.Text = getlanguage("RegisterMessage4")
                            End If
                        Else
                            rowVerification1.Visible = True
                        End If
                    End If
                End If
                dr.Close()
            End If

            If blnLogin Then
                    ' Attempt to Validate User Credentials
                    Dim dr As SqlDataReader = objUser.GetSingleUserByUsername(_portalSettings.PortalId, txtUsername.Text)
                    Dim LastLoginDate As String = ""
                    If dr.Read() Then
                        LastLoginDate = dr("LastLoginDate").ToString
                    End If
                    dr.Close()

                    Dim userId As Integer = objSecurity.UserLogin(txtUsername.Text, txtPassword.Text, _portalSettings.PortalId)

                    If userId >= 0 Then


                        dr = objUser.GetUserCountryCode(_portalSettings.PortalId, userId)
                        Dim TempUserCode As String
                        Dim IPLow As String = ""
                        Dim IPHigh As String = ""
                        Dim IPNow As String = Request.UserHostAddress
                        ' Read first row from database
                        If dr.Read() Then
                            TempUserCode = IIf(IsDBNull(dr("Country_Code")), "", dr("Country_Code"))
                            IPLow = IIf(IsDBNull(dr("IPfrom")), "", dr("IPfrom"))
                            IPHigh = IIf(IsDBNull(dr("IPto")), "", dr("IPto"))
                            If IPLow <> "" And IPHigh <> "" Then
                                If (IPConvert(IPNow) < IPConvert(IPLow)) Or (IPConvert(IPNow) > IPConvert(IPHigh)) Then
                                    ' does not fall in the IP range
                                    lblMessage.Text = ProcessLanguage(Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "Security_Enter_PortalIP"), Page)
                                    lblMessage.Text = Replace(lblMessage.Text, "{IP}", " -> " & DisplayCountryName(Request.UserHostAddress) & " " & Request.UserHostAddress)
                                    Exit Sub
                                End If
                            End If

                            If TempUserCode = "" Or InStr(1, TempUserCode.ToLower, DisplayCountrycode(Request.UserHostAddress).ToLower) Then
                                ' Use security system to set the UserID within a client-side Cookie
                                objUser.UpdateUserIP(userId, Request.UserHostAddress, "")
                                FormsAuthentication.SetAuthCookie(userId.ToString(), chkCookie.Checked)

                                objUser.UpdateCheckUserSecurity(userId, "", DateTime.Now.AddYears(-30), 0)
                                ' Reset Session variable
                                Session.Contents.RemoveAll()
                                Session("LastLoginDate") = LastLoginDate

                                ' Redirect browser back to home page
                                ' If admin admin or webmestre webmestre then redirect to account
                                If (txtUsername.Text.ToLower() = txtPassword.Text.ToLower()) And (txtUsername.Text.ToLower() = "admin" Or txtUsername.Text.ToLower() = "webmestre") Then
                                    ' http://my-dnz.com/fr.accueil.aspx?edit=control&tabid=1&def=Register
                                    Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "def=Register"), True)
                                Else
                                    Dim TempQuerystring As String = Context.Request.QueryString.ToString()
                                    If Not Request.Params("showlogin") Is Nothing Then
                                        TempQuerystring = TempQuerystring.Replace("&showlogin=1", "")
                                        TempQuerystring = TempQuerystring.Replace("showlogin=1&", "")
                                        TempQuerystring = TempQuerystring.Replace("?showlogin=1", "")
                                    End If
                                    Response.Redirect(GetFullDocument(_portalSettings.ActiveTab.ssl) & "?" & TempQuerystring, True)
                                End If
                            Else
                                lblMessage.Text = ProcessLanguage(Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "Security_Enter_PortalIP"), Page)
                                lblMessage.Text = Replace(lblMessage.Text, "{IP}", " -> " & DisplayCountryName(Request.UserHostAddress) & " " & Request.UserHostAddress)
                            End If
                        End If
                        dr.Close()
                    Else
                        RegisterBADip(Request.UserHostAddress)
                        Dim drUser As SqlDataReader = objUser.GetSingleUserByUsername(_portalSettings.PortalId, txtUsername.Text)
                        If drUser.Read() Then
                            Attempt = Attempt + 1
                            objUser.UpdateCheckUserSecurity(drUser("UserId"), "", DateTime.Now.AddMinutes(10), Attempt)
                        End If
                        drUser.Close()
                        If Attempt > 2 Then
                            lblMessage.Text = ProcessLanguage(Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "Security_Enter_PortalWait1"), Page)
                        Else
                            lblMessage.Text = GetLanguage("RegisterMessage3")
                        End If
                    End If
                End If
            End If

        End Sub

        Private Sub cmdSendPassword_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdSendPassword.Click

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Trim(txtUsername.Text) <> "" Then
                Dim objUser As New UsersDB()
                Dim objSecurity As New PortalSecurity()

                Dim dr As SqlDataReader = objUser.GetSingleUserByUsername(_portalSettings.PortalId, txtUsername.Text)

                If dr.Read() Then
                    Dim Admin As New AdminDB()
                    Dim strBody As String

                    strBody = Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "email_password_recall", PortalId)
                    If strBody = "" Then
                        strBody = Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "email_password_recall")
                    End If

                    strBody = Regex.Replace(strBody, "{FullName}", dr("FullName"), RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{PortalName}", _portalSettings.PortalName, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{PortalURL}", GetPortalDomainName(PortalAlias, Request), RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{Username}", dr("Username").ToString, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{Password}", objSecurity.Decrypt(PortalSettings.GetHostSettings("EncryptionKey"), dr("Password").ToString), RegexOptions.IgnoreCase)

                    If _portalSettings.UserRegistration = 3 And dr("LastLoginDate").ToString = "" And dr("IsSuperUser") = False Then
                        strBody = Regex.Replace(strBody, "{validationcode}", _portalSettings.PortalId.ToString & "-" & dr("UserId").ToString, RegexOptions.IgnoreCase)
                        strBody = Regex.Replace(strBody, "{needcode}", "", RegexOptions.IgnoreCase)
                        strBody = Regex.Replace(strBody, "{/needcode}", "", RegexOptions.IgnoreCase)
                    Else
                        strBody = Regex.Replace(strBody, "{needcode}[^¸]+{/needcode}", "", RegexOptions.IgnoreCase)
                    End If

                    If Not IsDBNull(dr("Authorized")) Then
                        If dr("Authorized") Then
                            strBody = Regex.Replace(strBody, "{notauthorized}[^{}]+{/notauthorized}", "", RegexOptions.IgnoreCase)
                        End If
                    Else
                        strBody = Regex.Replace(strBody, "{notauthorized}[^{}]+{/notauthorized}", "", RegexOptions.IgnoreCase)
                    End If

                    If (Regex.IsMatch(strBody, "<html>", RegexOptions.IgnoreCase) = True) Then
                        lblMessage.Text = SendNotification(_portalSettings.Email, dr("Email").ToString, "", GetLanguage("Password_Notice") & " " & _portalSettings.PortalName, strBody, "", "html")
                    Else
                        lblMessage.Text = SendNotification(_portalSettings.Email, dr("Email").ToString, "", GetLanguage("Password_Notice") & " " & _portalSettings.PortalName, strBody)
                    End If
                    If lblMessage.Text = "" Then
                        lblMessage.Text = GetLanguage("RegisterMessage2")
                    Else
                        lblMessage.Text = GetLanguage("RegisterMessage6")
                    End If
                Else
                    RegisterBADip(Request.UserHostAddress)
                    lblMessage.Text = GetLanguage("RegisterMessage1")
                End If

                dr.Close()

            Else
                RegisterBADip(Request.UserHostAddress)
                lblMessage.Text = GetLanguage("RegisterMessage")
            End If

        End Sub

        Private Sub cmdRegister_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdRegister.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "def=Register"), True)
        End Sub

    End Class

End Namespace