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

    Public MustInherit Class TTT_ForumAdmin

        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents forumNav As TTT_ForumNavigator
        Protected WithEvents forumDetails As TTT_ForumDetails
        
		Protected WithEvents pnlNavigator As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlForumDetails As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents cmdForumSetting As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkForumSetting As System.Web.UI.WebControls.HyperLink
		
        Protected WithEvents cmdModerateAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkModerateAdmin As System.Web.UI.WebControls.HyperLink
		
        Protected WithEvents cmdHome As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkHome As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdUserAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkUserAdmin As System.Web.UI.WebControls.HyperLink
		

        Private ZforumID As Integer
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
			Dim objCSS As Control = page.FindControl("CSS")
			Dim objTTTCSS As Control = page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
 			Dim ImageFolder As String = ForumConfig.SkinImageFolder()
            If (Not objCSS Is Nothing) and (objTTTCSS Is Nothing) Then
                    ' put in the ttt.css
					objLink = New System.Web.UI.LiteralControl("TTTCSS")
					If Request.IsAuthenticated Then
					Dim UserCSS as ForumUser
					UserCSS = ForumUser.GetForumUser(Int16.Parse(Context.User.Identity.Name))
					Select Case UserCSS.Skin
					case "Jardin Floral"
                            objLink.Text = "<link href=""" & "images/TTT/skin1/ttt.css"" type=""text/css"" rel=""stylesheet"">"
					case "Stibnite"
                            objLink.Text = "<link href=""" & "images/TTT/skin2/ttt.css"" type=""text/css"" rel=""stylesheet"">"
					case "Algues bleues"
                            objLink.Text = "<link href=""" & "images/TTT/skin3/ttt.css"" type=""text/css"" rel=""stylesheet"">"
					Case Else
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
					End Select
					else
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                    End If
					objCSS.Controls.Add(objLink)
            End If


            If IsNumeric(Request.Params("forumid")) Then
                ZforumID = Int32.Parse(Request.Params("forumid"))
                If ZforumID > 0 Then
                    _validForum = True
                End If
            Else
                Session("filter") = Nothing
            End If

            PopulateControls()
		
        End Sub

        Private Sub PopulateControls()
			
            Me.pnlForumDetails.Visible = _validForum
            Me.pnlNavigator.Visible = Not _validForum

        End Sub

        Private Sub forumNav_ForumSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles forumNav.ForumSelected
            Dim ZforumID As Integer = forumNav.SelectedForum.ForumID
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim strURL As String = Request.Url.ToString & "&forumid=" & ZforumID.ToString & "&tabid=" & _portalsettings.activetab.tabid
            Response.Redirect(strURL)
        End Sub
    End Class

End Namespace