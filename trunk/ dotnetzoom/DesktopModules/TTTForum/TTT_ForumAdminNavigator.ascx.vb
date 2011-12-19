'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
' =======================================================================================
Option Strict On

Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public MustInherit Class TTT_ForumAdminNavigator

        Inherits DotNetZoom.PortalModuleControl

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Protected WithEvents lnkForumSetting As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkUserAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkForumAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkModerateAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkHome As System.Web.UI.WebControls.HyperLink

        Public Shared ZmoduleID As Integer
        Public Shared Zpage As Integer

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

        '**************************************************************************************       
        '**************************************************************************************
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If IsNumeric(Request.Params("mid")) Then
                ZmoduleID = Int16.Parse(Request.Params("mid"))
            End If
            If IsNumeric(Request.Params("editpage")) Then
                Zpage = Int16.Parse(Request.Params("editpage"))
            End If
            CreateLink()
        End Sub

        Private Sub CreateLink()

            ' If Zpage <> TTT_EditForum.ForumEditType.GlobalSettings Then

            Dim strForumSetting As String = TTTUtils.ForumSettingLink(_portalSettings.ActiveTab.TabId, ZmoduleID)
            lnkForumSetting.NavigateUrl = strForumSetting
            lnkForumSetting.Visible = True
            lnkForumSetting.Text = GetLanguage("F_ForumSetting")
           	If Zpage = TTT_EditForum.ForumEditType.GlobalSettings Then
			lnkForumSetting.ForeColor = system.drawing.color.Red
            End If


            Dim strUserAdmin As String = ForumUserAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID)
            lnkUserAdmin.NavigateUrl = strUserAdmin
            lnkUserAdmin.Visible = True
            lnkUserAdmin.Text = GetLanguage("F_UserAdmin")
           	If Zpage = TTT_EditForum.ForumEditType.ForumUserAdmin Then
			lnkUserAdmin.ForeColor = system.drawing.color.Red
            End If

                Dim strForumAdmin As String = ForumAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID)
                 lnkForumAdmin.NavigateUrl = strForumAdmin
                lnkForumAdmin.Visible = True
            lnkForumAdmin.Text = GetLanguage("F_ForumAdmin")
           	If Zpage = TTT_EditForum.ForumEditType.ForumAdmin Then
			lnkForumAdmin.ForeColor = system.drawing.color.Red
            End If
            
                Dim strForumModerateAdmin As String = ForumModerateAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID)
                lnkModerateAdmin.NavigateUrl = strForumModerateAdmin
				
                lnkModerateAdmin.Visible = True
				lnkModerateAdmin.Text = GetLanguage("F_ModerateAdmin")
			If Zpage = TTT_EditForum.ForumEditType.ForumModerateAdmin Then
			lnkModerateAdmin.ForeColor = system.drawing.color.Red
            End If

            Dim strHome As String = ForumHomeLink(_portalSettings.ActiveTab.TabId)
            lnkHome.NavigateUrl = strHome
			lnkHome.Text = GetLanguage("F_Home")

        End Sub

    End Class

End Namespace