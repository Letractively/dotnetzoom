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

Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public MustInherit Class TTT_ModerateAdmin

        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents forumNav As TTT_ForumNavigator
        Protected WithEvents moderateDetails As TTT_ModerateDetails
        Protected WithEvents pnlNavigator As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlModerate As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents btnBack As System.Web.UI.WebControls.Button


        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Public Shared ZmoduleID As Integer
        Public Shared ZforumID As Integer
        Private _validForum As Boolean

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


            If IsNumeric(Request.Params("forumid")) Then
                ZforumID = Int32.Parse(Request.Params("forumid"))
                If ZforumID > 0 Then _validForum = True
            End If

            If IsNumeric(Request.Params("mid")) Then
                ZmoduleID = Int32.Parse(Request.Params("mid"))
            End If

            PopulateControls()
            

			btnBack.Text = GetLanguage("return")
			btnBack.Tooltip = GetLanguage("return")
			
	       End Sub

        Private Sub PopulateControls()

			btnBack.Visible = _validForum
            pnlModerate.Visible = _validForum
            pnlNavigator.Visible = Not _validForum
			   

        End Sub

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
             Response.Redirect(ForumModerateAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID) + "&selectedindex=" + session("selectedindex").ToString )
        End Sub

        Private Sub forumNav_ForumSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles forumNav.ForumSelected
            Dim _selForum As ForumItemInfo = _forumNav.SelectedForum
            Dim strURL As String

            If _selForum.IsModerated Then
                ' to Moderate Admin page if forum's already moderated
                strURL = ForumModerateAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID, _selForum.ForumID)
            Else
                ' to Forum Admin page if forum not
                strURL = TTTUtils.ForumAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID, _selForum.ForumID) + "&selectedindex=" + session("selectedindex").ToString
            End If
            Response.Redirect(strURL)
        End Sub
    End Class

End Namespace