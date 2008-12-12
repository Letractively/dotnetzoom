'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.9)
'=======================================================================================
' For TTTCompany                    http://www.tttcompany.com
' Author:                           TAM TRAN MINH   (tamttt - tam@tttcompany.com)
' With ideas/code contributed by:   JOE BRINKMAN    (Jbrinkman - joe.brinkman@tag-software.net)
'                                   SAM HUNT        (Ossy - Sam.Hunt@nastech.eds.com)
'                                   CLEM MESSERLI   (Webguy96 - webguy96@hotmail.com)
'=======================================================================================
Option Strict On

Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public MustInherit Class TTT_ForumStatistics
        Inherits System.Web.UI.UserControl

        Protected WithEvents lblNewThreadsInPast24Hours As System.Web.UI.WebControls.Literal
        Protected WithEvents lblNewPostsInPast24Hours As System.Web.UI.WebControls.Literal
        Protected WithEvents lblMostViewedThread As System.Web.UI.WebControls.Literal
        Protected WithEvents lblMostActiveThread As System.Web.UI.WebControls.Literal
        Protected WithEvents lblMostReadThread As System.Web.UI.WebControls.Literal
        Protected WithEvents lblTotalUsers As System.Web.UI.WebControls.Literal
        Protected WithEvents lblTotalThreads As System.Web.UI.WebControls.Literal
        Protected WithEvents lblTotalPosts As System.Web.UI.WebControls.Literal
        Protected WithEvents lbl10ActiveUsers As System.Web.UI.WebControls.Literal

        Private _portalID As Integer
        Private _tabID As Integer
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

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim sb As New System.Text.StringBuilder()

            _portalID = _portalSettings.PortalId
            _tabID = _portalSettings.ActiveTab.TabId

            ' Get the statistics
            Dim forumStats As ForumStatistics = ForumStatistics.GetStats(_portalID, ZmoduleID)
            lblTotalUsers.Text = "<b>" & forumStats.TotalUsers.ToString & "</b> " & getlanguage("F_StatsUser") & " "
            lblTotalThreads.Text = "<b>" & forumStats.TotalThreads.ToString & "</b> " & getlanguage("F_StatsNThread") & " "
            lblTotalPosts.Text = "<b>" & forumStats.TotalPosts.ToString & "</b> " & getlanguage("F_StatsNPost") & " " 

            lblNewThreadsInPast24Hours.Text = "<b>" & forumStats.NewThreadsInPast24Hours.ToString & "</b> " & getlanguage("F_StatsNewThread") & " " 
            lblNewPostsInPast24Hours.Text = "<b>" & forumStats.NewPostsInPast24Hours.ToString & "</b> " & getlanguage("F_StatsNewPost") & " " 
            lblMostViewedThread.Text = forumURL(forumStats.MostViewsPostID, forumStats.MostViewsSubject)
            lblMostActiveThread.Text = forumURL(forumStats.MostActivePostID, forumStats.MostActiveSubject)

            Dim iCount As Integer
            Dim user As ForumUser

            If forumStats.ActiveUsers.Count > 0 Then
                For iCount = 0 To forumStats.ActiveUsers.Count - 1
                    user = CType(forumStats.ActiveUsers.Item(iCount), ForumUser)
                    sb.Append("<A href=""")
                    sb.Append(AddHTTP(GetDomainName(Request)))
					sb.Append(GetDocument())
                    sb.Append("?tabid=")
                    sb.Append(_tabID.ToString)
                    sb.Append("&amp;forumpage=")
                    sb.Append(TTT_ForumDispatch.ForumDesktopType.ForumProfile)
                    sb.Append("&amp;userid=")
                    sb.Append(user.UserID.ToString)
                    sb.Append("""><b>")
                    sb.Append(user.Alias)
                    sb.Append("</b></A>, ")
                Next

                If sb.Length > 0 Then
                    lbl10ActiveUsers.Text = Left(sb.ToString, (sb.Length - 2))
                End If

            End If

        End Sub

        Private Function forumURL(ByVal ID As Integer, ByVal Subject As String) As String
            Dim sb As New System.Text.StringBuilder()

            sb.Append("<a href=""")
            sb.Append(GetFullDocument())
            sb.Append("?tabid=")
            sb.Append(_tabID.ToString)
            sb.Append("&amp;scope=post")
            sb.Append("&amp;threadid=" & ID.ToString)
            sb.Append("""><b>")
            sb.Append(Subject)
            sb.Append("</b></a>")
            Return sb.ToString

        End Function


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