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

Imports System.Text
Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public Class TTT_Forum
        Inherits PortalModuleControl

        Dim bCanEdit As Boolean = False
        Protected WithEvents btnSearch As System.Web.UI.WebControls.LinkButton
        Protected WithEvents ddlDisplay As System.Web.UI.WebControls.DropDownList
        Protected WithEvents TTTForum As DotNetZoom.Forum

        Protected WithEvents lblBreadCrumbs1 As System.Web.UI.WebControls.Literal
        Protected WithEvents lblBreadCrumbs2 As System.Web.UI.WebControls.Literal
        Protected WithEvents cmdNewTopic1 As System.Web.UI.WebControls.Button
        Protected WithEvents cmdNewTopic2 As System.Web.UI.WebControls.Button


        Protected WithEvents lnkHome As System.Web.UI.WebControls.HyperLink


        Protected WithEvents lnkSearch As System.Web.UI.WebControls.HyperLink



        Protected WithEvents lnkProfile As System.Web.UI.WebControls.HyperLink



        Protected WithEvents lnkAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents txtSearch As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkEmail As System.Web.UI.WebControls.CheckBox


        Protected WithEvents lnkSubscribe As System.Web.UI.WebControls.HyperLink



        Protected WithEvents lnkModerate As System.Web.UI.WebControls.HyperLink

        Protected WithEvents btnApprove As System.Web.UI.WebControls.ImageButton
        Protected WithEvents Tr1 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents pnlStats As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlUserOnline As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlLeft As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents ctlStatistics As DotNetZoom.TTT_ForumStatistics
        Protected WithEvents ctlUserOnline As DotNetZoom.TTT_ForumUsersOnline
        
        Protected WithEvents lnkPMS As System.Web.UI.WebControls.HyperLink


        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Private _threadID As Integer
        Private _scope As String
        Private ZforumID As Integer
        Private Zconfig As ForumConfig

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

            If Zconfig Is Nothing Then
                Zconfig = ForumConfig.GetForumConfig(ModuleId)
            End If

			Dim ImageFolder As String = ForumConfig.SkinImageFolder()

            If IsNumeric(Request.Params("forumid")) Then
                ZforumID = Int32.Parse(Request.Params("forumid"))
                btnSearch.Text = GetLanguage("go")
                btnSearch.ToolTip = GetLanguage("F_Search1")
                ' Security Check to see if the user try to access a place that is not allowed
                Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)

                If forumInfo.IsActive Then
                    If forumInfo.IsPrivate Then
                        If Not PortalSecurity.IsInRoles(forumInfo.AuthorizedRoles) = True Then
                            AccessDenied()
                        End If
                    End If
                Else
                    HttpContext.Current.Response.Redirect(GetFullDocument(), True)
                End If

            Else
                btnSearch.Text = GetLanguage("go")
                btnSearch.ToolTip = GetLanguage("F_Search")
            End If

            If IsNumeric(Request.Params("threadid")) Then
                _threadID = Int32.Parse(Request.Params("threadid"))
            End If

            If Not (Request.Params("scope") Is Nothing) Then
                _scope = Request.Params("scope")
            Else
                ' display left panel on forum homepage only
                pnlLeft.Visible = True
                Me.pnlUserOnline.Visible = Zconfig.UserOnlineIntegrate
            End If

            'Set value for user controls
            ctlStatistics.ModuleID = ModuleId
            ctlUserOnline.ModuleID = ModuleId

            With TTTForum
                .PortalID = _portalSettings.PortalId
                .ModuleID = ModuleId
                .Config = Zconfig
                .Title = _portalSettings.ActiveTab.TabName
            End With

            PopulateLink()

            If _scope = "post" Then
                ddlDisplay.Visible = True
            Else
                ddlDisplay.Visible = False
            End If

            If Not Page.IsPostBack Then

                ddlDisplay.Items.Clear()    'implement display dropdown list
                ddlDisplay.Items.Add(GetLanguage("F_FlatView"))
                ddlDisplay.Items.Add(GetLanguage("F_TreeView"))

                ddlDisplay.Items.FindByText(GetLanguage("F_FlatView")).Selected = True
                TTTForum.IsFlatView = True

                If Request.IsAuthenticated = True Then
                    Dim LoggedOnID As Integer = TTTUtils.ConvertInteger(Context.User.Identity.Name)

                    Dim dr As SqlDataReader = ForumUserDB.TTTForum_GetUserLastView(LoggedOnID)
                    If dr.Read Then
                        If Not dr("FlatView") Is DBNull.Value Then
                            If CBool(dr("FlatView")) = False Then
                                ddlDisplay.ClearSelection()
                                ddlDisplay.Items.FindByText(GetLanguage("F_TreeView")).Selected = True
                                TTTForum.IsFlatView = False
                            End If
                        End If
                    End If
                    dr.Close()
                Else
                    If Not HttpContext.Current.Session("flatview") Is Nothing Then
                        If CBool(HttpContext.Current.Session("flatview")) = False Then
                            ddlDisplay.ClearSelection()
                            ddlDisplay.Items.FindByText(GetLanguage("F_TreeView")).Selected = True
                        End If
                    End If



                End If
            End If

            'TTTForum.IsFlatView = (ddlDisplay.SelectedItem.Text = GetLanguage("F_FlatView"))
            BuildBreadCrumbs() '<tam:note need to be re-implemented />

            lnkAdmin.Text = GetLanguage("F_cmdAdmin")

            lnkModerate.Text = GetLanguage("cmdModerate")

            lnkPMS.Text = GetLanguage("cmdPMS")

            lnkProfile.Text = GetLanguage("cmdProfile")

            lnkSubscribe.Text = GetLanguage("cmdSubscribe")

            lnkSearch.Text = GetLanguage("cmdSearch")



            lnkHome.Text = GetLanguage("cmdHome")

            cmdNewTopic1.Text = GetLanguage("cmdNewTopic")
            cmdNewTopic2.Text = GetLanguage("cmdNewTopic")
            CmdNewTopic1.ToolTip = GetLanguage("F_AddNewThread")
            CmdNewTopic2.ToolTip = GetLanguage("F_AddNewThread")

        End Sub

        Private Sub ddlDisplay_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDisplay.SelectedIndexChanged
            Dim itemSelected As String = ddlDisplay.SelectedItem.Text
            Dim _flatView As Boolean = (itemSelected.ToLower = GetLanguage("F_FlatView").ToLower)

            '<tam:note value=set property for forum control />
            TTTForum.IsFlatView = _flatView

            '<tam:note value=update database if it's an authenticated user?>
            If Request.IsAuthenticated = True Then
                ForumUserDB.TTTForum_UpdateUserViewType(Int16.Parse(Context.User.Identity.Name), _flatView)
            Else
                Session("flatview") = _flatView
            End If

        End Sub

        Protected Sub ForumSearch(ByVal searchTerms As String)

            ' Redirect user to search page
            If (searchTerms.Length > 0) Then
                Dim redirectURL As String = String.Empty
                searchTerms = searchTerms.Replace("&", ":amp:")
                redirectURL = TTTUtils.GetURL(GetFullDocument(), Page, String.Format("tabid={0}&action=search&searchterms=" + searchTerms + "&searchobject=" + searchTerms, _portalSettings.ActiveTab.TabId.ToString), "forumpage=&scope=&threadid=&postid=&threadpage=&startdate=&enddate=&forumsid=&useralias=&threadspage=&searchpage=")
                Page.Response.Redirect(redirectURL)
            End If

        End Sub

        Private Function ExpandString(ByVal Base As String, ByVal Expand As String, ByVal Name As String) As String
            Dim sb As New StringBuilder()

            sb.Append(Base)
            sb.Append("&nbsp;&raquo;&nbsp;<a href=""")
            Expand = Expand.replace("&", "&amp;")
            sb.Append(Expand)
            sb.Append(""" class=""")
            sb.Append("ButtonCommand""")
            sb.Append(">")
            sb.Append(Name)
            sb.Append("</a>")

            Return sb.ToString
            'sb.Length = 0
        End Function

        Private Sub BuildBreadCrumbs()

            Dim ForumLink As String = ""
            Dim ThreadLink As String = ""
            Dim currentLink As String = ""
            Dim strBase As String = TTTUtils.GetURL(GetFullDocument(), TTTForum.Page, "", "forumid=&scope=&postid=&action=&searchpage=&searchterms=&threadspage=&threadid=")
            Dim BasedLink As String = ExpandString("", strBase, TTTForum.Title)
            Dim forumInfo As ForumItemInfo
            Dim threadInfo As ForumThreadInfo
            Dim postInfo As ForumPostInfo
            Dim strBreadCrumbs As String = ""

            If _scope Is Nothing Then
                strBreadCrumbs = BasedLink
            ElseIf _scope = "thread" Then
                currentLink = (Request.Url).ToString
                forumInfo = ForumItemInfo.GetForumInfo(ZforumID)
                ' Check security once more just to make sure
                If forumInfo.IsActive Then
                    If forumInfo.IsPrivate Then
                        If Not PortalSecurity.IsInRoles(forumInfo.AuthorizedRoles) = True Then
                            AccessDenied()
                        End If
                    End If
                Else
                    HttpContext.Current.Response.Redirect(GetFullDocument(), True)
                End If
                strBreadCrumbs = ExpandString(BasedLink, currentLink, forumInfo.Name)
            ElseIf _scope = "post" Then
                If _threadID > 0 Then
                    threadInfo = ForumThreadInfo.GetThreadInfo(_threadID)
                ElseIf _threadID = 0 AndAlso IsNumeric(Request.Params("postid")) Then
                    Dim _postID As Integer = Int16.Parse(Request.Params("postid"))
                    postInfo = ForumPostInfo.GetPostInfo(_postID)
                    threadInfo = postInfo.Parent
                End If

                strBreadCrumbs = BasedLink

                If Not threadInfo Is Nothing Then
                    forumInfo = threadInfo.Parent

                    ' Check security once more just to make sure
                    If forumInfo.IsActive Then
                        If forumInfo.IsPrivate Then
                            If Not PortalSecurity.IsInRoles(forumInfo.AuthorizedRoles) = True Then
                                AccessDenied()
                            End If
                        End If
                    Else
                        HttpContext.Current.Response.Redirect(GetFullDocument(), True)
                    End If
                    ForumLink = TTTUtils.GetURL(GetFullDocument(), TTTForum.Page, String.Format("scope=thread&forumid={0}", forumInfo.ForumID), "postid=&action=&searchpage=&threadspage=&threadid=")
                    strBreadCrumbs = ExpandString(BasedLink, ForumLink, forumInfo.Name)
                    ThreadLink = TTTUtils.GetURL(GetFullDocument(), TTTForum.Page, String.Format("scope=post&threadid={0}", threadInfo.ThreadID.ToString), "postid=&action=&searchpage=&searchterms=&threadspage=")
                    strBreadCrumbs = ExpandString(strBreadCrumbs, ThreadLink, threadInfo.Subject)
                End If

            Else
                strBreadCrumbs = BasedLink
            End If
            lblBreadCrumbs1.Text = strBreadCrumbs
            lblBreadCrumbs2.Text = strBreadCrumbs

        End Sub

        Private Sub PopulateLink()
            ' home
            Dim strHome As String = TTTUtils.ForumHomeLink(TabId)

            lnkHome.NavigateUrl = strHome

            ' search
            Dim strSearch As String = TTTUtils.ForumSearchLink(TabId, ModuleId)
            lnkSearch.NavigateUrl = strSearch


            If Request.IsAuthenticated Then

                ' moderate
                Dim logonedUser As ForumUser = ForumUser.GetForumUser(Int16.Parse(Context.User.Identity.Name))
                If logonedUser.IsModerator Then
                    Dim strModerate As String = TTTUtils.ForumModerateLink(TabId)
                    lnkModerate.NavigateUrl = strModerate
                    lnkModerate.Visible = True

                End If

                'private message
                Dim strPMS As String = TTTUtils.ForumPMSLink(TabId)
                lnkPMS.NavigateUrl = strPMS
                lnkPMS.Visible = Zconfig.UserOnlineIntegrate
                ' profile

                Dim strProfile As String = TTTUtils.ForumUserProfileLink(TabId, Int16.Parse(Context.User.Identity.Name))
                lnkProfile.NavigateUrl = strProfile
                lnkProfile.Visible = True

                ' subscribe

                If Zconfig.MailNotification Then
                    Dim strSubscribe As String = TTTUtils.ForumSubscribeLink(TabId, ModuleId, Int16.Parse(Context.User.Identity.Name))
                    lnkSubscribe.NavigateUrl = strSubscribe
                    lnkSubscribe.Visible = True
                End If

                ' <tam:note show thread email notification checkbox
                If _scope = "post" AndAlso Zconfig.MailNotification Then
                    chkEmail.Visible = True
                    chkEmail.Text = GetLanguage("F_CheckMail")
                    Dim dbForum As New ForumDB()
                    chkEmail.Checked = dbForum.TTTForum_TrackingThreadExists(_threadID, Int16.Parse(Context.User.Identity.Name))
                Else
                    chkEmail.Visible = False
                End If
            Else

                lnkProfile.Visible = False

            End If

            ' new thread
            If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True _
                OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                OrElse PortalSecurity.IsInRoles(CType(portalSettings.GetEditModuleSettings(ModuleId), ModuleSettings).AuthorizedEditRoles.ToString) = True) Then

                If _scope = "post" OrElse _scope = "thread" Then
                    cmdNewTopic1.Visible = True
                    cmdNewTopic2.Visible = True
                End If
            Else
                cmdNewTopic1.Visible = False
                cmdNewTopic2.Visible = False
            End If

            ' admin
            If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString)) = True _
            OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) Then

                Dim strAdmin As String = TTTUtils.ForumSettingLink(TabId, ModuleId)

                lnkAdmin.NavigateUrl = strAdmin

                lnkAdmin.Visible = True

            Else

                lnkAdmin.Visible = False

            End If

        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Dim strSearch As String = txtSearch.Text
            ForumSearch(strSearch)
        End Sub

        Private Sub chkEmail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEmail.CheckedChanged
            Dim dbForum As New ForumDB()
            dbForum.TTTForum_TrackingThreadCreateDelete(_threadID, Int16.Parse(Context.User.Identity.Name), chkEmail.Checked)
        End Sub

        Private Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApprove.Click
            Dim ForumTable As Table = CType(TTTForum.FindControl("ForumContent"), Table)
            Dim chk As CheckBox = CType(ForumTable.FindControl("72"), CheckBox)
            Dim str As Boolean = chk.Checked

        End Sub

        Private Sub cmdNewTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNewTopic1.Click, cmdNewTopic2.Click
            If IsNumeric(Request.Params("forumid")) Then
                ZforumID = Int32.Parse(Request.Params("forumid"))
                ' Security Check to see if the user try to access a place that is not allowed
                Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)
                If forumInfo.IsActive Then
                    If forumInfo.IsPrivate Then
                        If Not PortalSecurity.IsInRoles(forumInfo.AuthorizedRoles) = True Then
                            AccessDenied()
                        End If
                    End If
                    HttpContext.Current.Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&mid=" & ModuleId & "&forumid=" & ZforumID & "&scope=thread&action=new", True)
                Else
                    HttpContext.Current.Response.Redirect(GetFullDocument(), True)
                End If
            Else
                HttpContext.Current.Response.Redirect(GetFullDocument(), True)
            End If
        End Sub


		
    End Class
End Namespace