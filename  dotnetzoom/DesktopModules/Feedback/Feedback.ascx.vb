'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by Ren� Boulard ( http://www.reneboulard.qc.ca)'
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

    Public MustInherit Class Feedback
        Inherits DotNetZoom.PortalModuleControl


        Protected WithEvents pnlCaptcha As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents before As System.Web.UI.WebControls.Literal
        Protected WithEvents after As System.Web.UI.WebControls.Literal
        Protected WithEvents erreur As System.Web.UI.WebControls.Label


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

        Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents rowSendTo As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents txtSendTo As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtBody As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdSend As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Request.IsAuthenticated Then
                Title1.DisplayHelp = "DisplayHelp_InfoFeedback"
                cmdSend.ToolTip = GetLanguage("tooltip_send")
            Else
                Title1.DisplayHelp = "DisplayHelp_Captcha_Feedback"
                cmdSend.ToolTip = GetLanguage("tooltip_Captchasend")
            End If


            cmdSend.Text = GetLanguage("send")
            cmdCancel.Text = GetLanguage("annuler")
            cmdUpdate.Text = GetLanguage("enregistrer")
            cmdUpdate.ToolTip = GetLanguage("tooltip_feedback")
            If Not (Request.Params("sendto") Is Nothing) Then
                ' Change Title
                Title1.DisplayTitle = GetLanguage("Feedback_Contact")
            End If

            If Not Page.IsPostBack Then
                pnlCaptcha.Visible = Not Request.IsAuthenticated

                If Not (Request.Params("def") Is Nothing) Then
                    If Not (Request.Params("sendto") Is Nothing) Then
                        ' Get E-Mail
                        Title1.DisplayTitle = GetLanguage("Feedback_Contact")
                        If Not Request.UrlReferrer Is Nothing Then
                            ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                        Else
                            ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                        End If
                        Dim objSecurity As New DotNetZoom.PortalSecurity()
                        txtSendTo.Text = objSecurity.Decrypt(Application("cryptokey"), Request.Params("sendto"))
                        If txtSendTo.Text = "" Then
                            EditDenied()
                        End If
                    Else
                        EditDenied()
                    End If
                End If


                If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Then
                    rowSendTo.Visible = True
                    cmdUpdate.Visible = True
                Else
                    rowSendTo.Visible = False
                    cmdUpdate.Visible = False
                End If
                ' Get settings from the database
                If ModuleId > 0 Then
                    Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)
                    txtSendTo.Text = CType(settings("sendto"), String)
                    If txtSendTo.Text = "" Then
                        txtSendTo.Text = _portalSettings.Email
                    End If
                Else
                    cmdUpdate.Visible = False
                End If
                GetUser()
            End If
        End Sub


        Private Sub SendTheMEssage()

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Get settings from the database
            Dim strSendTo As String = _portalSettings.Email
            If ModuleId > 0 Then
                Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)
                strSendTo = CType(settings("sendto"), String)
                If strSendTo = "" Then
                    strSendTo = _portalSettings.Email
                End If
            End If

            ' pnlCaptcha.Visible = Not Request.IsAuthenticated'
            If txtEmail.Text <> "" And txtSubject.Text <> "" And txtName.Text <> "" And txtBody.Text <> "" Then
                SendNotification(_portalSettings.Email, strSendTo, "", txtSubject.Text, GetLanguage("email") & " : " & txtEmail.Text & vbCrLf & vbCrLf & txtBody.Text & vbCrLf & vbCrLf & txtName.Text)
                lblMessage.Text = "<br>" & GetLanguage("FeedBack_Mail_Send")
                pnlCaptcha.Visible = False
                cmdSend.Visible = False
                InitializeForm()
                If Not (Request.Params("sendto") Is Nothing) Then
                    cmdCancel.Text = GetLanguage("return")
                End If
            Else
                lblMessage.Text = "<br>" & GetLanguage("FeedBack_New_Add")
            End If
        End Sub



        Sub SendBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSend.Click
            ' Send E-Mail
            If pnlCaptcha.Visible Then
                If CheckCapcha(Request) Then
                    erreur.Visible = False
                    Sendthemessage()
                Else
                    erreur.Visible = True
                    If Request.Form("recaptcha_response_field") <> "" Then
                        ' Erreur Try Again
                        erreur.Text = GetLanguage("ANNnotvalid")
                    Else
                        ' empty field
                        erreur.Text = GetLanguage("ANNnotext")
                    End If
                End If
            Else
                Sendthemessage()
            End If


        End Sub

        Sub CancelBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            If Not CType(ViewState("UrlReferrer"), String) Is Nothing Then
                Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
            Else
                InitializeForm()
                lblMessage.Text = ""
            End If

        End Sub

        Private Sub InitializeForm()

            txtEmail.Text = ""
            txtName.Text = ""
            txtSubject.Text = ""
            txtBody.Text = ""

            GetUser()

        End Sub

        Private Sub GetUser()

            If Request.IsAuthenticated Then

                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                Dim objUser As New UsersDB()

                Dim drUser As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, Int32.Parse(Context.User.Identity.Name))
                If drUser.Read Then
                    txtEmail.Text = drUser("Email").ToString
                    txtName.Text = drUser("FullName").ToString
                End If
                drUser.Close()

            End If

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

            If ModuleId > 0 Then
                ' Update settings in the database
                Dim admin As New AdminDB()
                admin.UpdateModuleSetting(ModuleId, "sendto", txtSendTo.Text)
            End If

        End Sub

    End Class

End Namespace