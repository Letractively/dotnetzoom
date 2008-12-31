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

Imports System.IO
Imports System.Text
Imports System.Runtime.Serialization.Formatters

Namespace DotNetZoom

    Public Class HostSettings
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents ddlTimeZone As System.Web.UI.WebControls.DropDownList
		Protected WithEvents ddlViewState As System.Web.UI.WebControls.DropDownList
		Protected WithEvents ddlWhiteSpace As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtHostTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHostURL As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHostEmail As System.Web.UI.WebControls.TextBox
        Protected WithEvents cboProcessor As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdProcessor As System.Web.UI.WebControls.LinkButton
        Protected WithEvents txtUserId As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHostFee As System.Web.UI.WebControls.TextBox
        Protected WithEvents cboHostCurrency As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtHostSpace As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtSiteLogHistory As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDemoPeriod As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkDemoSignup As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkPageTitleVersion As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkEnableErrorReport As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkEnableSSL As System.Web.UI.WebControls.CheckBox
        Protected WithEvents txtEncryptionKey As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtProxyServer As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtProxyPort As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtSMTPServer As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtSMTPServerUser As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtSMTPServerPassword As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdEmail As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
        Protected WithEvents txtFileExtensions As System.Web.UI.WebControls.TextBox

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton

        Protected WithEvents cboUpgrade As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpgrade As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblUpgrade As System.Web.UI.WebControls.Label
		Protected WithEvents ddlTimeserver As System.Web.UI.WebControls.Label
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

        '*******************************************************
        '
        ' The Page_Load server event handler on this user control is used
        ' to populate the current site settings from the config system
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

		Title1.DisplayHelp = "DisplayHelp_HostSettings"
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Verify that the current user has access to access this page
            If not PortalSecurity.IsSuperUser Then
                Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If

			ddlTimeserver.Text = datetime.Now().ToString()
			
		 ' If this is the first visit to the page, populate the site data
            If Page.IsPostBack = False Then
			cmdProcessor.Text = GetLanguage("HS_GO_Processor")
			cmdEmail.Text = GetLanguage("HS_test_email")
			cmdUpdate.Text = GetLanguage("enregistrer")
			cmdUpgrade.Text = GetLanguage("go")
			
			ddlViewState.Items.Insert(0, New ListItem(GetLanguage("list_none"), "-1"))
			ddlViewState.Items.FindByValue("S").Text = GetLanguage("ViewState_SQL")
			ddlViewState.Items.FindByValue("M").Text = GetLanguage("ViewState_memory")
			
			
			ddlWhiteSpace.Items.Insert(0, New ListItem(GetLanguage("list_none"), "-1"))
			ddlWhiteSpace.Items.FindByValue("E").Text = GetLanguage("WhiteSpace_Only")
			ddlWhiteSpace.Items.FindByValue("H").Text = GetLanguage("WhiteSpaceHTML")
			ddlWhiteSpace.Items.FindByValue("T").Text = GetLanguage("WhiteSpace_ALL")
            
	

            BindData()
            End If

        End Sub


		
        Private Sub BindData()
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

            txtHostTitle.Text = portalSettings.GetHostSettings("HostTitle").ToString
            txtHostURL.Text = portalSettings.GetHostSettings("HostURL").ToString
            txtHostEmail.Text = portalSettings.GetHostSettings("HostEmail").ToString

            cboProcessor.DataSource = objAdmin.GetProcessorCodes
            cboProcessor.DataBind()
			ddlTimeZone.DataSource =  objAdmin.GetTimeZoneCodes(GetLanguage("N"))
			ddlTimeZone.DataBind()
			
			Try
 		    ddlTimeZone.SelectedValue = portalSettings.GetHostSettings("TimeZone")
			Catch ex As Exception
       		ddlTimeZone.SelectedValue = 0     
			End Try


            If Not cboProcessor.Items.FindByText(portalSettings.GetHostSettings("PaymentProcessor").ToString) Is Nothing Then
                cboProcessor.Items.FindByText(portalSettings.GetHostSettings("PaymentProcessor").ToString).Selected = True
            End If
			If portalSettings.GetHostSettings.ContainsKey("ViewState") then
            If Not ddlViewState.Items.FindByValue(portalSettings.GetHostSettings("ViewState").ToString) Is Nothing Then
                ddlViewState.Items.FindByValue(portalSettings.GetHostSettings("ViewState").ToString).Selected = True
			else
			ddlViewState.Items.FindByText(GetLanguage("list_none")).Selected = True
            End If
			else
			ddlViewState.Items.FindByText(GetLanguage("list_none")).Selected = True
			End If

			If portalSettings.GetHostSettings.ContainsKey("WhiteSpace") then
            If Not ddlWhiteSpace.Items.FindByValue(portalSettings.GetHostSettings("WhiteSpace").ToString) Is Nothing Then
                ddlWhiteSpace.Items.FindByValue(portalSettings.GetHostSettings("WhiteSpace").ToString).Selected = True
            else
			ddlWhiteSpace.Items.FindByText(GetLanguage("list_none")).Selected = True
			End If
            else
			ddlWhiteSpace.Items.FindByText(GetLanguage("list_none")).Selected = True
			End If



            txtUserId.Text = portalSettings.GetHostSettings("ProcessorUserId").ToString
            txtPassword.Text = portalSettings.GetHostSettings("ProcessorPassword").ToString

            txtHostFee.Text = Format(portalSettings.GetHostSettings("HostFee").ToString, "0.00")
            cboHostCurrency.DataSource = objAdmin.GetCurrencies(GetLanguage("N"))
            cboHostCurrency.DataBind()
            If Not cboHostCurrency.Items.FindByValue(portalSettings.GetHostSettings("HostCurrency")) Is Nothing Then
                cboHostCurrency.Items.FindByValue(portalSettings.GetHostSettings("HostCurrency").ToString).Selected = True
            Else
                cboHostCurrency.Items.FindByValue("CAD").Selected = True
            End If
            txtHostSpace.Text = portalSettings.GetHostSettings("HostSpace").ToString
            txtSiteLogHistory.Text = portalSettings.GetHostSettings("SiteLogHistory").ToString
            txtDemoPeriod.Text = portalSettings.GetHostSettings("DemoPeriod").ToString
            If portalSettings.GetHostSettings("DemoSignup").ToString = "Y" Then
                chkDemoSignup.Checked = True
            Else
                chkDemoSignup.Checked = False
            End If
            If portalSettings.GetHostSettings.ContainsKey("DisablePageTitleVersion") Then
                If portalSettings.GetHostSettings("DisablePageTitleVersion").ToString = "Y" Then
                    chkPageTitleVersion.Checked = True
                Else
                    chkPageTitleVersion.Checked = False
                End If
            Else
                chkPageTitleVersion.Enabled = False
            End If

            If portalSettings.GetHostSettings.ContainsKey("EnableErrorReporting") Then
                If portalSettings.GetHostSettings("EnableErrorReporting").ToString = "Y" Then
                    chkEnableErrorReport.Checked = True
                Else
                    chkEnableErrorReport.Checked = False
                End If
            Else
                chkEnableErrorReport.Checked = False
            End If


            If PortalSettings.GetHostSettings.ContainsKey("chkEnableSSL") Then
                If PortalSettings.GetHostSettings("chkEnableSSL").ToString = "Y" Then
                    chkEnableSSL.Checked = True
                Else
                    chkEnableSSL.Checked = False
                End If
            Else
                chkEnableSSL.Checked = False
            End If


            txtEncryptionKey.Text = portalSettings.GetHostSettings("EncryptionKey").ToString
            txtProxyServer.Text = portalSettings.GetHostSettings("ProxyServer").ToString
            txtProxyPort.Text = portalSettings.GetHostSettings("ProxyPort").ToString
            txtSMTPServer.Text = portalSettings.GetHostSettings("SMTPServer").ToString
			if portalSettings.GetHostSettings.ContainsKey("SMTPServerUser") = True then
			txtSMTPServerUser.Text = portalSettings.GetHostSettings("SMTPServerUser").ToString
			else
			txtSMTPServerUser.Text = ""
			end if
			if portalSettings.GetHostSettings.ContainsKey("SMTPServerPassword") = True then
			txtSMTPServerPassword.Text = portalSettings.GetHostSettings("SMTPServerPassword").ToString
			else
			txtSMTPServerPassword.Text = ""
			end if
            txtFileExtensions.Text = portalSettings.GetHostSettings("FileExtensions").ToString

            Dim intVersion As Integer
            For intVersion = 0 To System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).ProductBuildPart()
                cboUpgrade.Items.Add("1.0." & intVersion.ToString)
            Next

        End Sub

        Private Sub Update_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

            objAdmin.UpdateHostSetting("HostTitle", txtHostTitle.Text)
            objAdmin.UpdateHostSetting("HostURL", txtHostURL.Text)
            objAdmin.UpdateHostSetting("HostEmail", txtHostEmail.Text)
            objAdmin.UpdateHostSetting("PaymentProcessor", cboProcessor.SelectedItem.Text)
			objAdmin.UpdateHostSetting("ViewState", ddlViewState.SelectedItem.Value)
			objAdmin.UpdateHostSetting("WhiteSpace", ddlWhiteSpace.SelectedItem.Value)
            objAdmin.UpdateHostSetting("ProcessorUserId", txtUserId.Text)
            objAdmin.UpdateHostSetting("ProcessorPassword", txtPassword.Text)
			txtHostFee.Text = Format(txtHostFee.Text, "0.00")
			txtHostFee.Text = replace(txtHostFee.Text, "," , ".")
            objAdmin.UpdateHostSetting("HostFee", txtHostFee.Text)
            objAdmin.UpdateHostSetting("HostCurrency", cboHostCurrency.SelectedItem.Value)
            objAdmin.UpdateHostSetting("HostSpace", txtHostSpace.Text)
            objAdmin.UpdateHostSetting("SiteLogHistory", txtSiteLogHistory.Text)
            objAdmin.UpdateHostSetting("DemoPeriod", txtDemoPeriod.Text)
            objAdmin.UpdateHostSetting("DemoSignup", IIf(chkDemoSignup.Checked, "Y", "N"))
            objAdmin.UpdateHostSetting("DisablePageTitleVersion", IIf(chkPageTitleVersion.Checked, "Y", "N"))
            objAdmin.UpdateHostSetting("EnableErrorReporting", IIf(chkEnableErrorReport.Checked, "Y", "N"))
            objAdmin.UpdateHostSetting("chkEnableSSL", IIf(chkEnableSSL.Checked, "Y", "N"))
            objAdmin.UpdateHostSetting("ProxyServer", txtProxyServer.Text)
            objAdmin.UpdateHostSetting("ProxyPort", txtProxyPort.Text)
            objAdmin.UpdateHostSetting("SMTPServer", txtSMTPServer.Text)
			objAdmin.UpdateHostSetting("SMTPServerUser", txtSMTPServerUser.Text)
			objAdmin.UpdateHostSetting("SMTPServerPassword", txtSMTPServerPassword.Text)
            objAdmin.UpdateHostSetting("FileExtensions", txtFileExtensions.Text)
			objAdmin.UpdateHostSetting("TimeZone", ddlTimeZone.SelectedItem.Value)
			' Reset Memory Cashe
			ClearHostCache()
			' re-encrypt user passwords if EncryptionKey has changed
            If txtEncryptionKey.Text <> portalSettings.GetHostSettings("EncryptionKey").ToString Then
                Dim objSecurity As New PortalSecurity()
                Dim objUsers As New UsersDB()
                Dim strPassword As String
                Dim dr As SqlDataReader
                dr = objUsers.GetUsers()
                While dr.Read
                    strPassword = dr("Password").ToString
                    strPassword = objSecurity.Decrypt(portalSettings.GetHostSettings("EncryptionKey").ToString, strPassword)
                    If strPassword = "" Then ' decryption error - reset password
						Dim r As New Random()
						Dim i As Integer
						For i = 0 To (9)
						strPassword = strPassword & Chr(Int((26 * r.NextDouble()) + 65))
						Next
						GetLanguage("Reset_PasswordTXT")
						Dim sb As New System.Text.StringBuilder()
						sb.AppendFormat(GetLanguage("Reset_PasswordTXT"), dr("FullName"), GetPortalDomainName(PortalAlias, Request), dr("Username").ToString, strPassword ) 
                        SendNotification(_portalSettings.Email, dr("Email").ToString, "", GetLanguage("Mod_Password") & " " & _portalSettings.PortalName , sb.ToString())
                    End If
                    strPassword = objSecurity.Encrypt(txtEncryptionKey.Text, strPassword)
                    objUsers.UpdateUser(-1, dr("UserId").ToString, dr("FirstName").ToString, dr("LastName").ToString, dr("Unit").ToString, dr("Street").ToString, dr("City").ToString, dr("Region").ToString, dr("PostalCode").ToString, dr("Country").ToString, dr("Telephone").ToString, dr("Email").ToString, dr("Username").ToString, strPassword)
                End While
                dr.Close()

                ' set the encryption key
                objAdmin.UpdateHostSetting("Encryptionkey", txtEncryptionKey.Text)
            End If

            ' Redirect to this site to refresh host settings
            Response.Redirect(Request.RawUrl, True)

        End Sub

        Private Sub cmdProcessor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcessor.Click
            Response.Redirect(AddHTTP(cboProcessor.SelectedItem.Value), True)
        End Sub

        Private Sub cmdUpgrade_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpgrade.Click

            If File.Exists(Server.MapPath("Database\" & cboUpgrade.SelectedItem.Text & ".log")) Then
                Dim objStreamReader As StreamReader
                objStreamReader = File.OpenText(Server.MapPath("Database\" & cboUpgrade.SelectedItem.Text & ".log"))
                lblUpgrade.Text = Replace(objStreamReader.ReadToEnd, ControlChars.Lf, "<br>")
                objStreamReader.Close()
            Else
                lblUpgrade.Text = GetLanguage("HS_NoLog")
            End If

        End Sub

        Private Sub cmdEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEmail.Click
            If txtHostEmail.Text <> "" Then
                Dim strMessage As String = SendNotification(txtHostEmail.Text, txtHostEmail.Text, "", txtHostTitle.Text & " " & GetLanguage("Email_Test"), "")
                If strMessage <> "" Then
                    lblEmail.Text = "<br>" & strMessage
                Else
                    lblEmail.Text = "<br>" & GetLanguage("MailSend")
                End If
            Else
                lblEmail.Text = GetLanguage("HS_Need_Email") 
            End If
        End Sub

    End Class

End Namespace