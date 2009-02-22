'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' =======================================================================================
Option Strict On

Imports System.IO
Imports System
Imports System.Web
Imports System.Web.UI
Imports Microsoft.VisualBasic
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public Class TTT_ForumUserAdmin
        Inherits PortalModuleControl

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Protected WithEvents pnlUsersList As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
        Protected WithEvents pnlModerate As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtUserID As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtUserName As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAlias As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkThreadTracking As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkPrivateMessage As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkIsTrusted As System.Web.UI.WebControls.CheckBox
        Protected WithEvents CancelButton As System.Web.UI.WebControls.Button
        Protected WithEvents UpdateButton As System.Web.UI.WebControls.Button
        Protected WithEvents pnlUserAdmin As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents ctlUsers As TTT_usersControl

        Public Shared Zuser As ForumUser
        Protected WithEvents pnlUsersControl As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lnkProfile As System.Web.UI.WebControls.HyperLink

        Private ZuserID As Integer = 0

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
 			

            If IsNumeric(Request.Params("userid")) Then
                ZuserID = Int32.Parse(Request.Params("userid"))
            End If

			lnkProfile.Text = GetLanguage("F_lnkProfile")
			UpdateButton.Text = GetLanguage("enregistrer")
			UpdateButton.ToolTip = GetLanguage("enregistrer")
			CancelButton.Text = GetLanguage("return")	
			CancelButton.Tooltip = GetLanguage("return")

			
			
            GenerateControl()


            If Not Page.IsPostBack Then
			
                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = GetFullDocument() + "?edit=user&editpage=6&mid=" & ModuleId.ToString()

                If ZuserID > 0 Then
                    Dim Zconfig As ForumConfig = ForumConfig.GetForumConfig(ModuleId)

                    ForumUser.ResetForumUser(ZuserID)
                    Zuser = ForumUser.GetForumUser(ZuserID)

                    With Zuser
                        txtUserID.Text = ConvertString(.UserID)
                        txtUserName.Text = .Name
                        txtAlias.Text = .Alias
                        chkThreadTracking.Checked = .EnableThreadTracking
                        chkPrivateMessage.Checked = .EnablePrivateMessages
                        chkIsTrusted.Checked = .IsTrusted
                    End With

                    Dim strProfile As String = TTTUtils.ForumUserProfileLink(TabId, ZuserID)
                    lnkProfile.NavigateUrl = strProfile


                    ' disply moderated forum list if user is moderator
                    Me.pnlModerate.Visible = Zuser.IsModerator
                End If


            End If

			

			
        End Sub

        Private Sub GenerateControl()
            Me.pnlUsersList.Visible = (ZuserID = 0)
            Me.pnlUserAdmin.Visible = (ZuserID > 0)
        End Sub

        Private Sub PopulateModerate(ByVal UserID As Integer)

        End Sub

        Private Sub UpdateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateButton.Click

            With Zuser
                .IsTrusted = chkIsTrusted.Checked
                .EnableThreadTracking = chkThreadTracking.Checked
                .EnablePrivateMessages = chkPrivateMessage.Checked

            End With

            lblInfo.Text = Zuser.UpdateForumUser()
            If Len(lblInfo.Text) = 0 Then Response.Redirect(CType(ViewState("UrlReferrer"), String))

        End Sub

        Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelButton.Click
            ' Redirect back to forum page
            Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub

        Private Sub ctlUsersZuserSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlUsers.UserSelected

            Dim url As String = Request.Url.ToString & "&userid=" & CType(ctlUsers.SelectedUser, ForumUser).UserID.ToString & "&tabid=" & _portalSettings.ActiveTab.TabId
            Response.Redirect(url)
        End Sub
    End Class

End Namespace