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

Imports System.Web.Mail
Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public Class TTT_ForumModerate
        Inherits DotNetZoom.PortalModuleControl

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Protected WithEvents lstPost As System.Web.UI.WebControls.DataList
        Protected WithEvents cmdBack As System.Web.UI.WebControls.Button
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.Button
        Protected WithEvents cmdApprove As System.Web.UI.WebControls.Button
        Protected WithEvents cmdReject As System.Web.UI.WebControls.Button
        Protected WithEvents lblID As System.Web.UI.WebControls.Label
        Protected WithEvents lblAuthor As System.Web.UI.WebControls.Label
        Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
        Protected WithEvents ddlActions As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtNotes As System.Web.UI.WebControls.TextBox
        Protected WithEvents btnUpdate As System.Web.UI.WebControls.Button
        Protected WithEvents btnClose As System.Web.UI.WebControls.Button
        Protected WithEvents pnlAudit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected forumid As Integer
        Protected Zconfig As ForumConfig


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

            If IsNumeric(Request.Params("forumid")) Then
                forumid = Int32.Parse(Request.Params("forumid"))
            End If

            Zconfig = ForumConfig.GetForumConfig(ModuleId)

            If Not Page.IsPostBack Then
                BindList()
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = ""
                End If
            End If
			Dim ImageFolder As String = ForumConfig.SkinImageFolder()
			cmdBack.Text = GetLanguage("return")
			cmdBack.ToolTip = GetLanguage("F_ReturnMod")
			cmdDelete.Text = GetLanguage("erase")
			cmdDelete.ToolTip = GetLanguage("erase")
			cmdApprove.Text = GetLanguage("F_approve")
			cmdApprove.ToolTip = GetLanguage("F_approve")
			cmdReject.Text = GetLanguage("reject")
			cmdReject.ToolTip = GetLanguage("reject")

			ddlActions.Items.FindByValue("None").Text = GetLanguage("F_SelectAction")
			ddlActions.Items.FindByValue("Approve").Text = GetLanguage("F_approve")
			ddlActions.Items.FindByValue("Reject").Text = GetLanguage("reject")
			ddlActions.Items.FindByValue("Delete").Text = GetLanguage("erase")
			btnUpdate.Text = GetLanguage("enregistrer")
			btnUpdate.ToolTip = GetLanguage("enregistrer")
			btnClose.Text = GetLanguage("return")
			btnClose.ToolTip = GetLanguage("return")

			Dim objCSS As Control = page.FindControl("CSS")
			Dim objTTTCSS As Control = page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If (Not objCSS Is Nothing) and (objTTTCSS Is Nothing) Then
                    ' put in the ttt.css
					objLink = New System.Web.UI.LiteralControl("TTTCSS")
					If Request.IsAuthenticated Then
					Dim UserCSS as ForumUser
					UserCSS = ForumUser.GetForumUser(Int16.Parse(Context.User.Identity.Name))
					Select Case UserCSS.Skin
					case "Jardin Floral"
                            objLink.Text = "<link href=""" & glbPath & "images/TTT/skin1/ttt.css"" type=""text/css"" rel=""stylesheet"">"

                        Case "Stibnite"
                            objLink.Text = "<link href=""" & glbPath & "images/TTT/skin2/ttt.css"" type=""text/css"" rel=""stylesheet"">"

                        Case "Algues bleues"
                            objLink.Text = "<link href=""" & glbPath & "images/TTT/skin3/ttt.css"" type=""text/css"" rel=""stylesheet"">"
					
					Case Else
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
					End Select
					else
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                    End If
					objCSS.Controls.Add(objLink)
            End If
				
			
        End Sub

        Sub BindList()

            Dim PostModerateCollection As ForumPostModerateCollection = New ForumPostModerateCollection(forumid)
            lstPost.DataSource = PostModerateCollection
            lstPost.DataBind()
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            ' Redirect back to forum page
            Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub

        Private Sub lstPost_Select(ByVal Sender As System.Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles lstPost.ItemCommand

            ' Determine the command of the button             
            Dim cmdButton As ImageButton = DirectCast(e.CommandSource, ImageButton)
            Dim postID As Integer = Int16.Parse(cmdButton.CommandArgument)
            Dim selListItem As DataListItem = DirectCast(e.Item, DataListItem)
            Dim bodyPanel As Placeholder = DirectCast(selListItem.FindControl("pnlBody"), PlaceHolder)

            Select Case cmdButton.CommandName.ToLower
                Case "collapse"
                    Dim cmdExpand As ImageButton = CType(selListItem.FindControl("btnExpand"), ImageButton)
                    cmdExpand.Visible = True
                    cmdButton.Visible = False
                    bodyPanel.Visible = False

                Case "expand"
                    Dim cmdCollapse As ImageButton = CType(selListItem.FindControl("btnCollapse"), ImageButton)
                    cmdCollapse.Visible = True
                    bodyPanel.Visible = True
                    cmdButton.Visible = False

                Case "edit"
                    Dim selIndex As Integer = selListItem.ItemIndex
                    Dim postModerate As ForumPostModerate = ForumPostModerate.GetPostModerate(postID)
                    With postModerate
                        Me.lblID.Text = postID.ToString
                        Me.lblAuthor.Text = postModerate.User.Alias & " (Posts: " & postModerate.User.PostCount & ")"
                        Me.lblSubject.Text = postModerate.Subject
                        Me.lblMessage.Text = FormatBody(postModerate.Body)
                    End With
                    Me.pnlAudit.Visible = True

            End Select
            'BindList()
        End Sub

#Region "Functions"

        Public Function FormatUser(ByVal User As Object) As String

        End Function

        Public Function FormatBody(ByVal Body As Object) As String
            Dim Zconfig As ForumConfig = ForumConfig.GetForumConfig(ModuleId)
            If Not IsDBNull(Body) Then
                Dim bodyForumText As ForumText = New ForumText(Server.HtmlDecode(CType(Body, String)), Zconfig)
                'Return bodyForumText.Process(Zconfig.AvatarFolder)
                Return bodyForumText.Process()
            End If
        End Function

#End Region 'Function

        Private Sub cmdApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdApprove.Click, cmdDelete.Click, cmdReject.Click
            Dim cmd As String = DirectCast(sender, Button).CommandName
            Dim ZuserID As Integer = Int16.Parse(Context.User.Identity.Name)
            Dim _notes As String = ""
            Dim i As Integer

            For i = 0 To lstPost.Items.Count - 1
                Dim myListItem As DataListItem = lstPost.Items(i)
                Dim myCheck As CheckBox = DirectCast(myListItem.FindControl("chkApprove"), CheckBox)

                If myCheck.Checked Then
                    Dim _postID As Integer = CType(lstPost.DataKeys(i), Integer)
                    Dim _post As ForumPostInfo = ForumPostInfo.GetPostInfo(_postID)
                    Dim _mailURL As String = GetFullDocument() & "?forumid=" & forumid & "&tabid=" & _portalSettings.ActiveTab.TabId & "&scope=post&threadid=" & _post.ThreadID & "#" & _post.ThreadID

                    Select Case cmd.ToLower
                        Case "approve"
                            ApprovePost(_postID, ZuserID, _notes, _mailURL)
                        Case "reject"
                            RejectPost(_postID, ZuserID, _notes, _mailURL)
                        Case "delete"
                            DeletePost(_postID, ZuserID, _notes, _mailURL)
                    End Select
                End If
            Next
            BindList()
        End Sub

        Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
            Dim _action As String = ddlActions.SelectedItem.Value.ToString
            If _action.ToLower = "none" Then Return

            Dim ZuserID As Integer = Int16.Parse(Context.User.Identity.Name)
            Dim _postID As Integer = Int16.Parse(lblID.Text)
            Dim _notes As String = txtNotes.Text
            Dim _mailURL As String = GetFullDocument() & "?forumid=" & forumid & "&tabid=" & _portalSettings.ActiveTab.TabId & "&scope=post&threadid=" & _postID.ToString

            Select Case _action.ToLower
                Case "approve"
                    ApprovePost(_postID, ZuserID, _notes, _mailURL)
                Case "reject"
                    RejectPost(_postID, ZuserID, _notes, _mailURL)
                Case "delete"
                    DeletePost(_postID, ZuserID, _notes, _mailURL)
            End Select

            pnlAudit.Visible = False
            BindList()
        End Sub

        Private Sub ApprovePost(ByVal PostID As Integer, ByVal UserID As Integer, ByVal Notes As String, ByVal URL As String)

            Dim dbForum As New ForumDB()

            dbForum.TTTForum_Moderate_ApprovePost(PostID, UserID, Notes)
			ForumPostInfo.ResetPostInfo(postid)
            If Zconfig.MailNotification Then
                ' send notification mail to author
                SendForumMail(PostID, URL, ForumEmail.ForumEmailType.PostApprove)
                ' send notification mail to subscribe
                SendForumMail(PostID, URL, ForumEmail.ForumEmailType.PostNormal)
            End If

			Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(forumID)
			If forumInfo.IsActive and not forumInfo.IsPrivate Then
			' Make new RSS Feed here
                Dim RSSURL As String = GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId
			Dim dr As SqlDataReader = dbForum.TTTForum_GetThreads(forumID, 20, 0, RSSURL)
			CreateForumRSS(dr, "Subject", "LastPostAlias", "URLField", "DateLastPost",  forumInfo.name & " " & forumInfo.Description, Request.MapPath(_portalSettings.UploadDirectory) & "forum" & forumID.ToString & ".xml")
			dr.Close()
			end if

        End Sub

        Private Sub RejectPost(ByVal PostID As Integer, ByVal UserID As Integer, ByVal Notes As String, ByVal URL As String)

            Dim dbForum As New ForumDB()
            dbForum.TTTForum_Moderate_RejectPost(PostID, UserID, Notes)
             ForumPostInfo.ResetPostInfo(postid)
            If Zconfig.MailNotification Then
                ' send notification mail to author
                SendForumMail(PostID, URL, ForumEmail.ForumEmailType.PostReject)
            End If
        End Sub

        Private Sub DeletePost(ByVal PostID As Integer, ByVal UserID As Integer, ByVal Notes As String, ByVal URL As String)

            If Zconfig.MailNotification Then
                ' this should do before delete as we need post info to send mail to client
                SendForumMail(PostID, URL, ForumEmail.ForumEmailType.PostDelete)
            End If

            Dim dbForum As New ForumDB()
            dbForum.TTTForum_Moderate_DeletePost(PostID, UserID, Notes)
            
        End Sub

        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
            pnlAudit.Visible = False
        End Sub

        Private Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub
        Public Function GetEditStyle() As String
            Return "border-width:0px; background: url('" & Zconfig.ImageFolder() & "forum.gif') no-repeat; background-position: 0px -128px;"
        End Function
    End Class

End Namespace