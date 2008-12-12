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

Imports System.Web.Security
Imports System.Text.RegularExpressions

Namespace DotNetZoom

    Public Class Register
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents Title1 As DesktopModuleTitle
        Protected WithEvents UserRow As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblRegister As System.Web.UI.WebControls.Label
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
        Protected WithEvents valEmail1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents valEmail2 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents Address1 As Address
        Protected WithEvents Message As System.Web.UI.WebControls.Label
        Protected WithEvents lblRegistration As System.Web.UI.WebControls.Label

        Protected WithEvents RegisterBtn As System.Web.UI.WebControls.LinkButton
        Protected WithEvents UnregisterBtn As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents ReturnBtn As System.Web.UI.WebControls.LinkButton

        Protected WithEvents ServicesRow As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents myDataGrid As System.Web.UI.WebControls.DataGrid
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
        Protected WithEvents ReturnButton As System.Web.UI.WebControls.LinkButton
		Protected WithEvents Tableservices As System.Web.UI.HtmlControls.HtmlTable

        Private Services As Integer = 0
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
			valFirstName.ErrorMessage = "<br>" + GetLanguage("need_firstname")
			valLastName.ErrorMessage = "<br>" + GetLanguage("need_lastname")
			valUsername.ErrorMessage = "<br>" + GetLanguage("need_username")
			valPassword.ErrorMessage = "<br>" + GetLanguage("need_password")
			valConfirm1.ErrorMessage = "<br>" + GetLanguage("need_password_confirm")
			valConfirm2.ErrorMessage = "<br>" + GetLanguage("need_password_match")
			valEmail1.ErrorMessage = "<br>" + GetLanguage("need_email")
			
			ReturnButton.Text = GetLanguage("return")
			valEmail2.ErrorMessage = "<br>" + GetLanguage("need_valid_email")

			myDataGrid.Columns(1).HeaderText = GetLanguage("Role_Name")
            myDataGrid.Columns(2).HeaderText = GetLanguage("Role_Description")
            myDataGrid.Columns(3).HeaderText = GetLanguage("ServicesFee")
            myDataGrid.Columns(4).HeaderText = GetLanguage("Role_Frequency")
            myDataGrid.Columns(5).HeaderText = GetLanguage("Role_FrequencyP")
            myDataGrid.Columns(6).HeaderText = GetLanguage("Role_Trial")
            myDataGrid.Columns(7).HeaderText = GetLanguage("Role_Frequency")
            myDataGrid.Columns(8).HeaderText = GetLanguage("Role_FrequencyP")
            myDataGrid.Columns(9).HeaderText = GetLanguage("Role_Expiry_Date")

		
			RegisterBtn.Text = GetLanguage("U_Subscribe")
			cmdCancel.Text = GetLanguage("annuler")
			UnregisterBtn.Text = GetLanguage("U_QUIT_site")
            ' Verify that the current user has access to this page
            If _portalSettings.UserRegistration = 0 And Request.IsAuthenticated = False Then
                Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Access Denied", True)
            End If

            If IsNumeric(Request.Params("Services")) Then
                Services = Int32.Parse(Request.Params("Services"))
            End If

            ' free subscriptions
            If IsNumeric(Request.Params("RoleID")) Then
                RoleID = Int32.Parse(Request.Params("RoleID"))

                Dim objUser As New UsersDB()

                objUser.UpdateService(CType(Context.User.Identity.Name, Integer), RoleID, IIf(Not Request.Params("cancel") Is Nothing, True, False))

                Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Register", True)

            End If

            If Services = 1 Then
                Title1.DisplayTitle = GetLanguage("Membership_Serv")
                Title1.DisplayHelp = "DisplayHelp_Services"
            ElseIf Request.IsAuthenticated Then
                If PortalSecurity.IsSuperUser Or PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                    UnregisterBtn.Visible = False
                End If
                Title1.DisplayTitle = GetLanguage("PersonnalInfo")
                Title1.DisplayHelp = "DisplayHelp_RegisterEdit"
                lblRegister.Text = GetLanguage("Required_Info")
                RegisterBtn.Text = GetLanguage("enregistrer")
            End If



            ' If this is the first visit to the page, bind the role data to the datalist
            If Page.IsPostBack = False Then
                UnregisterBtn.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm_unsubscribe")) & "');")

                BindData()

                Try
                    SetFormFocus(txtFirstName)
                Catch
                    'control not there or error setting focus
                End Try

                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = ""
                End If
            End If

            txtPassword.Attributes.Add("value", txtPassword.Text)
            txtConfirm.Attributes.Add("value", txtConfirm.Text)
            If PortalSettings.GetSiteSettings(_portalSettings.PortalId).ContainsKey(GetLanguage("N") & "_registrationmessage") Then
                lblRegistration.Text = CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)(GetLanguage("N") & "_registrationmessage"), String)
            Else
                lblRegistration.Text = CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("registrationmessage"), String)
            End If

        End Sub

        Sub BindData()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

            Address1.ModuleId = -1
            Address1.StartTabIndex = 7
            If Services = 1 Then

				Tableservices.Visible = False

				
                UserRow.Visible = False

                Dim objUser As New UsersDB()


                myDataGrid.DataSource = objUser.GetServices(_portalSettings.PortalId, GetLanguage("N"))
                myDataGrid.DataBind()

	            Dim dr As SqlDataReader = objUser.GetServices(_portalSettings.PortalId, GetLanguage("N"))
			
				while dr.read()
				if dr("TrialFrequency") <> "" then
                myDataGrid.Columns(6).Visible = True 
                myDataGrid.Columns(7).Visible = True 
                myDataGrid.Columns(8).Visible = True 
				end if
				end while
				

                dr.close()

                If myDataGrid.Items.Count <> 0 Then
                    lblMessage.Text = ProcessLanguage(GetLanguage("register_more_info"), page)
                Else
                    myDataGrid.Visible = False
                    lblMessage.Text = GetLanguage("register_no")
                End If
                lblMessage.Visible = True
                myDataGrid.Columns(0).Visible = False ' subscribe
                myDataGrid.Columns(9).Visible = False ' expiry date

                ServicesRow.Visible = True
            Else
                UserRow.Visible = True

                If Request.IsAuthenticated Then
                    valPassword.Enabled = False
                    valConfirm1.Enabled = False
                    valConfirm2.Enabled = False

                    Dim users As New UsersDB()
                    Dim dr As SqlDataReader = users.GetSingleUser(_portalSettings.PortalId, Int32.Parse(Context.User.Identity.Name))

                    ' Read first row from database
                    If dr.Read() Then
                        txtFirstName.Text = dr("FirstName").ToString
                        txtLastName.Text = dr("LastName").ToString
                        txtUsername.Text = dr("Username").ToString
                        txtEmail.Text = dr("Email").ToString

                        Address1.Unit = dr("Unit").ToString
                        Address1.Street = dr("Street").ToString
                        Address1.City = dr("City").ToString
                        Address1.Region = dr("Region").ToString
                        Address1.Country = dr("Country").ToString
                        Address1.Postal = dr("PostalCode").ToString
                        Address1.Telephone = dr("Telephone").ToString
                    End If
                    dr.Close()

                    Dim objUser As New UsersDB()

					

					
                    myDataGrid.DataSource = objUser.GetServices(_portalSettings.PortalId, GetLanguage("N"), CType(Context.User.Identity.Name, Integer))
                    myDataGrid.DataBind()

					dr = objUser.GetServices(_portalSettings.PortalId, GetLanguage("N"), CType(Context.User.Identity.Name, Integer))
   					while dr.read()
					if dr("TrialFrequency") <> "" then
        	        myDataGrid.Columns(6).Visible = True 
            	    myDataGrid.Columns(7).Visible = True 
                	myDataGrid.Columns(8).Visible = True 
					end if
					if dr("ExpiryDate").ToString <> "" then
					myDataGrid.Columns(9).Visible = True 
					end if
					end while
					dr.Close()
					
                    ' if no e-commerce service available then hide options
                    ServicesRow.Visible = False
					If myDataGrid.Items.Count <> 0 then
                    dr = objAdmin.GetSinglePortal(_portalSettings.PortalId)
                    If dr.Read Then
                        If dr("PaymentProcessor").ToString <> "" and dr("ProcessorUserId").ToString <> "" Then
		                    ServicesRow.Visible = True
							Title1.DisplayHelp = "DisplayHelp_RegisterEditServices"
							Tableservices.Visible = True
                        End If
                    End If
                    dr.Close()
					end if
                Else
				Title1.DisplayHelp = "DisplayHelp_Register"
                 Select Case _portalSettings.UserRegistration
                        Case 1 ' private
                            Title1.DisplayTitle = GetLanguage("Register_Title")
	                        lblRegister.Text = GetLanguage("Private_Info")
                        Case 2 ' public
                            Title1.DisplayTitle = GetLanguage("Register_Title")
                            lblRegister.Text = GetLanguage("Public_Register_info")
                        Case 3 ' verified
                            Title1.DisplayTitle = GetLanguage("Register_Title")
                            lblRegister.Text = GetLanguage("Verified_Register_info")
                    End Select
                    lblRegister.Text += GetLanguage("Register_Required_Info")

                    RegisterBtn.Text = GetLanguage("banner_register") 
                    UnregisterBtn.Visible = False
                    ServicesRow.Visible = False
                    valPassword.Enabled = True
                    valConfirm1.Enabled = True
                    valConfirm2.Enabled = True
                End If
            End If

        End Sub

        Private Sub RegisterBtn_Click(ByVal sender As Object, ByVal E As EventArgs) Handles RegisterBtn.Click

            Dim strBody As String
                          
            
            ' Only attempt a save/update if all form fields on the page are valid
            If Page.IsValid = True Then

                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                ' Add New User to Portal User Database
                Dim objUser As New UsersDB()
                Dim objSecurity As New PortalSecurity()
                Dim admin As New AdminDB()

                If Request.IsAuthenticated Then
                    Message.Text = ""
                    If txtPassword.Text <> "" Or txtConfirm.Text <> "" Then
                        If txtPassword.Text <> txtConfirm.Text Then
                            Message.Text = GetLanguage("need_password_match")
                        End If
                    End If

                    If Message.Text = "" Then
                        Dim Username As String = Nothing
                        Dim objreader As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, CType(context.User.Identity.Name, Integer))
                        If objreader.Read Then
                            Username = objreader.Item("Username")
                        End If
                        objreader.Close()
                        objreader = Nothing

                        Dim dr As SqlDataReader = objUser.GetSingleUserByUsername(_portalSettings.PortalId, txtUsername.Text)
                        'if a user is found with that username and the username isn't our current user's username
                        If dr.Read And txtUsername.Text <> Username Then
                            'username already exists in DB so show user an error message
                            Message.Text = ProcessEMail(GetLanguage("UserName_Already_Used")) 
                        Else
                            'update the user
 
                            Dim GotAmod As Boolean = False

                            objUser.UpdateUser(_portalSettings.PortalId, CType(Context.User.Identity.Name, Integer), txtFirstName.Text, txtLastName.Text, Address1.Unit, Address1.Street, Address1.City, Address1.Region, Address1.Postal, Address1.Country, Address1.Telephone, txtEmail.Text, txtUsername.Text, IIf(txtPassword.Text <> "", objSecurity.Encrypt(PortalSettings.GetHostSettings("EncryptionKey"), txtPassword.Text), ""))
                            ' informe le webmestre du site du changement d'info
                            If dr.read Then

                                If txtFirstName.Text.ToLower <> dr("FirstName").ToString.Tolower() Then
                                    GotAmod = True
                                End If

                                If txtLastName.Text.ToLower <> dr("LastName").ToString.tolower() Then
                                    GotAmod = True
                                End If

                                If Address1.Unit <> dr("Unit").ToString Then
                                    GotAmod = True
                                End If

                                If Address1.Street.ToLower <> dr("Street").ToString.tolower() Then
                                    GotAmod = True
                                End If

                                If Address1.City.ToLower <> dr("City").ToString.tolower() Then
                                    GotAmod = True
                                End If

                                If Address1.Region.ToLower <> dr("Region").ToString.tolower() Then
                                    GotAmod = True
                                End If

                                If Address1.Country.ToLower <> dr("Country").ToString.Tolower Then
                                    GotAmod = True
                                End If

                                If Address1.Postal.ToLower <> dr("PostalCode").ToString.tolower() Then
                                    GotAmod = True
                                End If

                                If Address1.Telephone <> dr("Telephone").ToString Then
                                    GotAmod = True
                                End If

                                If txtEmail.Text <> dr("Email").ToString Then
                                    GotAmod = True
                                End If
                            End If

                            If GotAmod Then

                                If Not PortalSettings.GetSiteSettings(_portalSettings.PortalId)("registeremail") Is Nothing Then
                                    strBody = admin.GetSinglelonglanguageSettings(GetLanguage("N"), "email_newuser_notice", PortalId)
                                    If strBody = "" Then
                                        strBody = admin.GetSinglelonglanguageSettings(GetLanguage("N"), "email_newuser_notice")
                                    End If
                                    strBody = ProcessEMail(strBody)
                                    If (Regex.IsMatch(strBody, "<html>", RegexOptions.IgnoreCase) = True) Then
                                        SendNotification(txtEmail.Text, PortalSettings.GetSiteSettings(_portalSettings.PortalId)("registeremail"), "", GetLanguage("Account_Mod") & " " & txtFirstName.Text & " " & txtLastName.Text, strBody, "", "html")
                                    Else
                                        SendNotification(txtEmail.Text, PortalSettings.GetSiteSettings(_portalSettings.PortalId)("registeremail"), "", GetLanguage("Account_Mod") & " " & txtFirstName.Text & " " & txtLastName.Text, strBody, "", "")
                                    End If
                                End If

                                strBody = admin.GetSinglelonglanguageSettings(GetLanguage("N"), "email_account_mod_notice", PortalId)
                                If strBody = "" Then
                                    strBody = admin.GetSinglelonglanguageSettings(GetLanguage("N"), "email_account_mod_notice")
                                End If
                                strBody = ProcessEMail(strBody)
                                If (Regex.IsMatch(strBody, "<html>", RegexOptions.IgnoreCase) = True) Then
                                    SendNotification(_portalSettings.Email, txtEmail.Text, "", GetLanguage("Account_Mod") & " " & txtFirstName.Text & " " & txtLastName.Text, strBody, "", "html")
                                Else
                                    SendNotification(_portalSettings.Email, txtEmail.Text, "", GetLanguage("Account_Mod") & " " & txtFirstName.Text & " " & txtLastName.Text, strBody, "", "")
                                End If
                            End If
                            dr.Close()
                            dr = Nothing

                            ' Redirect browser back to home page
                            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
                            End If
                            dr.Close()
                            dr = Nothing

                    End If
                Else
                    Dim UserId As Integer

                    UserId = objUser.AddUser(_portalSettings.PortalId, txtFirstName.Text, txtLastName.Text, Address1.Unit, Address1.Street, Address1.City, Address1.Region, Address1.Postal, Address1.Country, Address1.Telephone, txtEmail.Text, txtUsername.Text, objSecurity.Encrypt(portalSettings.GetHostSettings("EncryptionKey"), txtPassword.Text), IIf(_portalSettings.UserRegistration = 1, CStr(False), CStr(True)), UserId)

                    If UserId >= 0 Then
					    ' Add user to Forum Dbase
						Dim dbForumUser As New ForumUserDB()
                    	dbForumUser.TTTForum_UserCreateUpdateDelete(userId, txtUsername.Text, True, False, "", "", "", _portalSettings.TimeZone, "", "", "", "", "", "", "", False, True, False, True, True, True, 0)
						
                        strBody = Admin.GetSingleLonglanguageSettings( GetLanguage("N"), "email_newuser_notice", PortalID)
						if strBody = "" then
						strBody = Admin.GetSingleLonglanguageSettings( GetLanguage("N"), "email_newuser_notice")
						end if
						strBody = ProcessEmail(StrBody)
						if (Regex.IsMatch(strBody, "<html>", RegexOptions.IgnoreCase) = true) then
                        SendNotification(txtEmail.Text, _portalSettings.Email, "", GetLanguage("Register_request") & " " &  _portalSettings.PortalName , strBody, "", "html")
                        else
                        SendNotification(txtEmail.Text, _portalSettings.Email, "", GetLanguage("Register_request") & " " &  _portalSettings.PortalName , strBody, "", "")
						end if
						' complete registration
                        Select Case _portalSettings.UserRegistration
                        	               	

                            Case 1 ' private
								strBody = Admin.GetSingleLonglanguageSettings( GetLanguage("N"), "email_newuser_private")
								strBody = ProcessEmail(StrBody)
			             		If portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey(getlanguage("N") & "_signupmessage") then
								strBody = Regex.Replace(strBody, "{signupmessage}" , CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)(getlanguage("N") & "_signupmessage"), String), RegexOptions.IgnoreCase)
								else 
								strBody = Regex.Replace(strBody, "{signupmessage}" , CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)(getlanguage("N") & "signupmessage"), String), RegexOptions.IgnoreCase)
								end if
								if (Regex.IsMatch(strBody, "<html>", RegexOptions.IgnoreCase) = true) then
        		                SendNotification(_portalSettings.Email, txtEmail.Text, "",   GetLanguage("Register_request") & " " &  _portalSettings.PortalName, strBody, "", "html")
                		        else
                        		SendNotification(_portalSettings.Email, txtEmail.Text, "",   GetLanguage("Register_request") & " " &  _portalSettings.PortalName, strBody, "", "")
								end if
                               
                            Case 2 ' public
                                UserId = objSecurity.UserLogin(txtUsername.Text, txtPassword.Text, _portalSettings.PortalId)
                                FormsAuthentication.SetAuthCookie(UserId.ToString, False)
                                
                                

                            Case 3 ' verified
								strBody = Admin.GetSingleLonglanguageSettings( GetLanguage("N"), "email_newuser_verified")
								strBody = ProcessEmail(StrBody, UserID)

			             		If portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey(getlanguage("N") & "_signupmessage") then
								strBody = Regex.Replace(strBody, "{signupmessage}" , CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)(getlanguage("N") & "_signupmessage"), String), RegexOptions.IgnoreCase)
								else 
								strBody = Regex.Replace(strBody, "{signupmessage}" , CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)(getlanguage("N") & "signupmessage"), String), RegexOptions.IgnoreCase)
								end if
								if (Regex.IsMatch(strBody, "<html>", RegexOptions.IgnoreCase) = true) then
        		                SendNotification(_portalSettings.Email, txtEmail.Text, "",   GetLanguage("Register_request") & " " &  _portalSettings.PortalName, strBody, "", "html")
                		        else
                        		SendNotification(_portalSettings.Email, txtEmail.Text, "",   GetLanguage("Register_request") & " " &  _portalSettings.PortalName, strBody, "", "")
								end if
                               
                       End Select
                        Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Bienvenue", true)
                    Else
					Message.Text = ProcessEMail(GetLanguage("UserName_Already_Used")) 
                        
                    End If
                End If

            End If

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)
        End Sub

        Private Sub UnregisterBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UnregisterBtn.Click
            Dim users As New UsersDB()
			Dim Admin as New AdminDB()
            Dim strBody As String

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

  			strBody = Admin.GetSingleLonglanguageSettings( GetLanguage("N"), "email_newuser_notice", PortalID)
			if strBody = "" then
			strBody = Admin.GetSingleLonglanguageSettings( GetLanguage("N"), "email_newuser_notice")
			end if
			strBody = ProcessEmail(StrBody)
			if (Regex.IsMatch(strBody, "<html>", RegexOptions.IgnoreCase) = true) then
            SendNotification(txtEmail.Text, _portalSettings.Email, "", _portalSettings.PortalName & " " & GetLanguage("Member_Quit") , strBody, "", "html")
            else
            SendNotification(txtEmail.Text, _portalSettings.Email, "", _portalSettings.PortalName & " " & GetLanguage("Member_Quit") , strBody)
			end if

            

            users.DeleteUser(_portalSettings.PortalId, CType(Context.User.Identity.Name, Integer))

            Response.Redirect("~" & GetDocument() & "?logoff=ok", True)

        End Sub

        Function FormatURL() As String

            Dim strServerPath As String

            strServerPath = Request.ApplicationPath
            If Not strServerPath.EndsWith("/") Then
                strServerPath += "/"
            End If

            Return strServerPath & "Register.aspx?tabid=" & TabId

        End Function

        Private Sub ReturnButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnButton.Click
            Response.Redirect(ViewState("UrlReferrer"), True)
        End Sub

        Public Function ServiceText(ByVal objSubscribed As Object) As String
            If IsDBNull(objSubscribed) Then
                ServiceText = GetLanguage("banner_register") 
            Else
                ServiceText = GetLanguage("annuler")
            End If
        End Function

		Public Function ServiceExpired(ByVal objExpiryDate As Object) As Boolean
            Dim tempExpiryDate As DateTime = DateTime.parse("9999-12-31")
			If Not IsDBNull(objExpiryDate) Then
                if tempExpiryDate = objExpiryDate then
				return False
				else
				return true
				end if
			else
			Return True
            End If
		End Function
		

        Public Function ServiceURL(ByVal strKeyName As String, ByVal strKeyValue As String, ByVal objServiceFee As Object, ByVal objSubscribed As Object) As String
            Dim dblServiceFee As Double = 0
            If Not IsDBNull(objServiceFee) Then
                dblServiceFee = objServiceFee
            End If
			
            If IsDBNull(objSubscribed) Then
                If dblServiceFee <> 0 Then
                    ServiceURL = "~/admin/Sales/PayPalSubscription.aspx?tabid=" & TabId & "&" & strKeyName & "=" & strKeyValue
                Else
                    ServiceURL = "~" & GetDocument() & "?edit=control&tabid=" & TabId & "&" & strKeyName & "=" & strKeyValue & "&def=Register"
                End If
            Else ' cancel
                If dblServiceFee <> 0 Then
                    ServiceURL = "~/admin/Sales/PayPalSubscription.aspx?tabid=" & TabId & "&" & strKeyName & "=" & strKeyValue & "&cancel=1"
                Else
                    ServiceURL = "~" & GetDocument() & "?edit=control&tabid=" & TabId & "&" & strKeyName & "=" & strKeyValue & "&def=Register&cancel=1"
                End If
            End If
        End Function

		Private Function ProcessEMail( ByVal ToProcess As String, Optional ByVal TUserID As Integer = -1) as String
		' to process {date} {firstname} {lastname} {app} {street} {city} {province} {country} {postalcode}
		' to process {phone} {email} {PortalName} {AdministratorEmail} {PortalURL} {username} {password}
		' to process {validationcode}
         Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

		ToProcess = Regex.Replace(ToProcess, "{date}" , ProcessLanguage("{date}"), RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{firstname}" , txtFirstName.Text, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{lastname}" , txtLastName.Text, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{FullName}" , txtFirstName.Text & " " & txtLastName.Text, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{app}" , Address1.Unit, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{street}" , Address1.Street, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{city}" , Address1.City, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{province}" , Address1.Region, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{country}" , Address1.Country, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{postalcode}" , Address1.Postal, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{phone}" , Address1.Telephone, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{email}" , txtEmail.Text, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{PortalName}" , _portalSettings.PortalName, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{AdministratorEmail}" , _portalSettings.Email, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{PortalURL}" , GetPortalDomainName(PortalAlias, Request), RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{username}" , txtUsername.Text, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{password}" , txtPassword.Text, RegexOptions.IgnoreCase)
		ToProcess = Regex.Replace(ToProcess, "{validationcode}" , _portalSettings.PortalId.ToString & "-" & TUserId.ToString, RegexOptions.IgnoreCase)
		Return ToProcess
		End Function
    
	End Class

End Namespace