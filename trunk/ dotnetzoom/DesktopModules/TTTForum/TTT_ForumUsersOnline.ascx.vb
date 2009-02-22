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

    Public MustInherit Class TTT_ForumUsersOnline
        Inherits System.Web.UI.UserControl

        Protected WithEvents pnlGuestMessage As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlMemberMessage As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents hypUser As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblMembers As System.Web.UI.WebControls.Literal
        Protected WithEvents lblCount As System.Web.UI.WebControls.Literal
        Protected WithEvents hypCount As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblGuestMessage As System.Web.UI.WebControls.Literal
        Protected WithEvents lblMemberMessage As System.Web.UI.WebControls.Literal
        Protected WithEvents pnlActiveMember As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlMember As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblGuestCount0 As System.Web.UI.WebControls.Literal
        Protected WithEvents lblMemberCount0 As System.Web.UI.WebControls.Literal
        Protected WithEvents lblGuestCount As System.Web.UI.WebControls.Literal
        Protected WithEvents lblMemberCount As System.Web.UI.WebControls.Literal
        Protected WithEvents lblTotalCount As System.Web.UI.WebControls.Literal

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Private ZuserID As Integer = -1
        Private ZmoduleID As Integer

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
            'Put user code to initialize the page here

            If Request.IsAuthenticated Then
                ZuserID = Int16.Parse(Context.User.Identity.Name)
            Else
                ZuserID = -1
            End If

                Dim dbUserOnline As New ForumUserOnlineDB()
                Dim sb As New System.Text.StringBuilder()
                Dim Zconfig As ForumConfig = ForumConfig.GetForumConfig(ZmoduleID)

                Dim GuestCount As Integer = 0
                Dim MemberCount As Integer = 0
                Dim UserCount As Integer = 0
                Dim NewToday As Integer = 0
                Dim NewYesterday As Integer = 0
                Dim LatestUsername As String = ""

                dbUserOnline.TTTForum_UserOnline2_GetCount(_portalSettings.PortalId, GuestCount, MemberCount, UserCount, NewToday, NewYesterday, LatestUsername)

                lblGuestCount.Text = "<b>" & GuestCount.ToString & "</b>"
                lblMemberCount.Text = "<b>" & MemberCount.ToString & "</b>"

                lblTotalCount.Text = "<b>" & (GuestCount + MemberCount).ToString & "</b>"

                pnlMemberMessage.Visible = Request.IsAuthenticated
                pnlGuestMessage.Visible = Not Request.IsAuthenticated

                If Request.IsAuthenticated Then

                    Dim ZforumUser As forumUser = forumUser.GetForumUser(ZuserID)
                    With hypUser
                        .Text = ZforumUser.Alias
                        .ToolTip = GetLanguage("F_ClickToModProfile") 
                        .NavigateUrl = TTTUtils.ForumUserProfileLink(_portalSettings.ActiveTab.TabId, Int16.Parse(Context.User.Identity.Name))
                    End With
					Dim NumMessages as integer = dbUserOnline.TTTForum_PrivateMessage_GetCount(ZuserID)
					hypCount.Tooltip = GetLanguage("F_GoToPMS") 
					If NumMessages = 0 then
					HypCount.Text = " " & GetLanguage("F_GotPMS") 
					else
					hypCount.Text = Replace(GetLanguage("F_PMSCount"), "{pmscount}",  ConvertString(NumMessages))
					end if
					hypCount.NavigateUrl = ForumPMSLink(_portalSettings.ActiveTab.TabId)

                    ' Handle if no user online module implemented
                    Try
                        Dim drmembers As SqlDataReader

                        drmembers = dbUserOnline.TTTForum_UserOnline2_Get(_portalSettings.PortalId)

                        While drmembers.Read
                            
                            sb.Append("<A href=""")
                        sb.Append(GetFullDocument())
                            sb.Append("?tabid=")
                            sb.Append(_portalSettings.ActiveTab.TabId.ToString)
                            sb.Append("&amp;forumpage=")
                            sb.Append(TTT_ForumDispatch.ForumDesktopType.ForumProfile)
                            sb.Append("&amp;userid=")
                            sb.Append(ConvertInteger(drmembers("UserID")).ToString)
                            sb.Append("""><b>")
                            sb.Append(ConvertString(drmembers("Alias")))
                            sb.Append("</b></A>, ")

                        End While
                        drmembers.Close()

                        If sb.Length > 0 Then
                            lblMembers.Text = Left(sb.ToString, (sb.Length - 2))
                        End If

                    Catch Exc As System.Exception
                        lblMembers.Text = "<font color=""#ff0000"">" & GetLanguage("F_UOError") & "</font>"
                    End Try

                Else
                sb.Append(GetLanguage("F_Anonymous") & " ")
                ' javascript:__doPostBack('banner$cmdRegister','')
                sb.Append("<A href=""javascript:__doPostBack('banner$cmdRegister','')")
                ' sb.Append(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "edit=control&amp;def=Register", GetLanguage("N"), "&amp;"))
                sb.Append("""><b>" & GetLanguage("F_AnonymousClick") & "</b></A>")

                lblGuestMessage.Text = sb.ToString
                End If
        End Sub

        Public Property ModuleID() As Integer
            Get
                Return ZmoduleID
            End Get
            Set(ByVal Value As Integer)
                ZmoduleID = Value
            End Set
        End Property

    End Class

End Namespace