'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'=======================================================================================
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by:
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' =======================================================================================

Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports System.IO

Namespace DotNetZoom

    Public MustInherit Class TTT_ForumPMS
        Inherits DotNetZoom.PortalModuleControl
        Protected WithEvents cmdInbox As System.Web.UI.WebControls.HyperLink
        
        Protected WithEvents cmdOutbox As System.Web.UI.WebControls.HyperLink
        
        Protected WithEvents cmdCompose As System.Web.UI.WebControls.HyperLink
        
        Protected WithEvents dgInbox As System.Web.UI.WebControls.DataGrid
        Protected WithEvents btnDeleteAllInbox As System.Web.UI.WebControls.Button
        Protected WithEvents pnlInbox As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlNoMessages As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents dgOutbox As System.Web.UI.WebControls.DataGrid
        Protected WithEvents btnDeleteAllOutbox As System.Web.UI.WebControls.Button
        Protected WithEvents lblErrMsg As System.Web.UI.WebControls.Label
        Protected WithEvents pnlOutbox As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents btnFindUser As System.Web.UI.WebControls.Button
        Protected WithEvents drpResults As System.Web.UI.WebControls.DropDownList
        Protected WithEvents btnSelect As System.Web.UI.WebControls.Button
        Protected WithEvents pnlFindUser As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents pnlUser As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
        Protected WithEvents rfvSubject As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents FCKeditor1 As FredCK.FCKeditorV2.FCKEditor
		
        Protected WithEvents rfvMessage As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents pnlCompose As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents btnSend As System.Web.UI.WebControls.Button
        Protected WithEvents txtRecipientID As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtRecipient As System.Web.UI.WebControls.TextBox
        Protected WithEvents rfvRecipient As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents btnBack As System.Web.UI.WebControls.Button
        Protected WithEvents pnlNav As System.Web.UI.WebControls.PlaceHolder
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Protected ZuserID As Integer
		Private _author As ForumUser
		Protected ImageFolder As String = ForumConfig.SkinImageFolder()
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
 
	        If Request.IsAuthenticated = false Then
                Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "showlogin=1"))
            End If
	
			btnBack.Text = GetLanguage("return")
			btnBack.Tooltip = GetLanguage("return")
			btnDeleteAllInbox.Text = GetLanguage("erase")
			btnDeleteAllInbox.ToolTip = GetLanguage("UO_erase")
			btnDeleteAllOutbox.Text = GetLanguage("erase")
			btnDeleteAllOutbox.ToolTip = GetLanguage("UO_erase")
			btnSend.Text = GetLanguage("UO_Send")
			btnSend.ToolTip = GetLanguage("UO_Send")
			btnFindUser.Text = GetLanguage("UO_FindUser")
			btnFindUser.ToolTip = GetLanguage("UO_FindUser")
			btnSelect.Text = GetLanguage("UO_Select")
			btnSelect.Tooltip = GetLanguage("UO_Select")
			Dim objCSS As Control = page.FindControl("CSS")
			Dim objTTTCSS As Control = page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl
			
			
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
                Else
                    objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                End If
                objCSS.Controls.Add(objLink)
            End If

            ZuserID = Int16.Parse(COntext.User.Identity.Name)


            If Page.IsPostBack = False Then

                If Request.Params("pmsTabId") = "1" Or Request.Params("pmsTabId") = "" Then
                    BindInbox()
                End If

                If Request.Params("pmsTabId") = "2" Then
                    BindOutbox()
                End If

                If Request.Params("pmsTabId") = "3" Then
                    _author = ForumUser.GetForumUser(Int16.Parse(Context.User.Identity.Name))
                    SetFckEditor()
                    BindCompose()
                End If

                CreateLink()
                ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
            End If


            cmdInbox.Text = "<img height=""32"" width=""32"" src=""" & glbPath & "images/1x1.gif"" Alt=""*"" style=""border-width:0px; background: url('" & ImageFolder & "forum.gif') no-repeat; background-position: -16px -224px;"">"
            cmdInbox.ToolTip = GetLanguage("UO_InBox")
            cmdOutbox.Text = "<img height=""32"" width=""32"" src=""" & glbPath & "images/1x1.gif"" Alt=""*"" style=""border-width:0px; background: url('" & ImageFolder & "forum.gif') no-repeat; background-position: -16px -288px;"">"
            cmdOutbox.ToolTip = GetLanguage("UO_OutBox")
            cmdCompose.Text = "<img height=""32"" width=""32"" src=""" & glbPath & "images/1x1.gif"" Alt=""*"" style=""border-width:0px; background: url('" & ImageFolder & "forum.gif') no-repeat; background-position: -16px -256px;"">"
            cmdCompose.ToolTip = GetLanguage("UO_write")
        End Sub

        Private Sub SetFckEditor()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim ZuserURL As String = _portalSettings.UploadDirectory
            ZuserUrl = ZuserURL & "userimage/" & ZuserID.ToString
            Session("FCKeditor:UserFilesPath") = ZuserURL & "/"
            FCKeditor1.LinkBrowserURL = glbPath & "admin/AdvFileManager/filemanager.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)

            If Request.IsAuthenticated = False Then
                FCKeditor1.LinkBrowserURL = ""
            Else
                Dim objAdmin As New AdminDB()
                Dim tmpUploadRoles As String = ""
                If Not CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("uploadroles"), String) Is Nothing Then
                    tmpUploadRoles = CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("uploadroles"), String)
                End If
                If Not PortalSecurity.IsInRoles(tmpUploadRoles) Then
                    FCKeditor1.LinkBrowserURL = ""
                End If
            End If
            FCKeditor1.Width = Unit.Pixel(610)
            FCKeditor1.Height = unit.pixel(500)
            If GetLanguage("fckeditor_language") <> "auto" Then
                FCKeditor1.DefaultLanguage = GetLanguage("fckeditor_language")
                FCKeditor1.AutoDetectLanguage = False
            End If
            FCKeditor1.ImageBrowserURL = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)

            ' set the css for the editor if it exist
            If Directory.Exists(Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/")) Then
                FCKeditor1.SkinPath = _portalSettings.UploadDirectory & "skin/fckeditor/"
                FCKeditor1.EditorAreaCSS = _portalSettings.UploadDirectory & "skin/fckeditor/fck_editorarea.css"
                FCKeditor1.StylesXmlPath = _portalSettings.UploadDirectory & "skin/fckeditor/fckstyles.xml"
                ' FCKeditor1.TemplatesXmlPath = _portalSettings.UploadDirectory & "skin/fckeditor/fcktemplates.xml"
            End If


            Dim ZuserphysicalPath As String = Server.MapPath(ZuserURL)
            If Not Directory.Exists(ZuserphysicalPath) Then
                Try
                    IO.Directory.CreateDirectory(ZuserphysicalPath)
                Catch exc As System.Exception
                End Try
            End If

        End Sub

        Private Sub BindInbox()

            Dim dbUserOnline As New ForumUserOnlineDB()

            Dim InboxSortField As String = ""
            Dim InboxSortdirection As String = ""

            If Not ViewState("InboxSortField") Is Nothing Then
                InboxSortField = CType(ViewState("InboxSortField"), String)
                InboxSortdirection = CType(ViewState("InboxSortDirection"), String)
            End If

            Try

                dgInbox.DataSource = dbUserOnline.TTTForum_PrivateMessage_GetInbox(ZuserID, InboxSortField, InboxSortdirection)
                dgInbox.Columns(3).HeaderText = GetLanguage("UO_From")
                dgInbox.Columns(4).HeaderText = GetLanguage("UO_Object")
                dgInbox.Columns(5).HeaderText = GetLanguage("UO_Date_received")
                dgInbox.DataBind()

                If dgInbox.Items.Count = 0 Then
                    pnlInbox.Visible = False
                    pnlNoMessages.Visible = True

                Else
                    pnlInbox.Visible = True
                    pnlNoMessages.Visible = False
                End If
            Catch Exc As System.Exception
                pnlNav.Visible = False
                pnlInbox.Visible = False
                lblErrMsg.Text = Exc.Message
                lblErrMsg.Visible = True
            End Try

            pnlOutbox.Visible = False
            pnlCompose.Visible = False

        End Sub

        Private Sub dgInbox_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgInbox.ItemCreated

            Dim btnReply As Control = e.Item.FindControl("btnReply")

            If Not btnReply Is Nothing Then
                CType(btnReply, ImageButton).alternatetext = GetLanguage("UO_reply")
                CType(btnReply, ImageButton).Tooltip = GetLanguage("UO_reply")
            End If

            Dim lnkReplyMessage As Control = e.Item.FindControl("lnkReplyMessage")

            If Not lnkReplyMessage Is Nothing Then
                CType(lnkReplyMessage, linkbutton).text = " " & GetLanguage("UO_reply")
                CType(lnkReplyMessage, linkbutton).Tooltip = GetLanguage("UO_reply")
            End If


            Dim btnKeepAsNew As Control = e.Item.FindControl("btnKeepAsNew")

            If Not btnKeepAsNew Is Nothing Then
                CType(btnKeepAsNew, ImageButton).alternatetext = GetLanguage("UO_KeepNew")
                CType(btnKeepAsNew, ImageButton).Tooltip = GetLanguage("UO_KeepNew")
            End If

            Dim lnkKeepAsNew As Control = e.Item.FindControl("lnkKeepAsNew")

            If Not lnkKeepAsNew Is Nothing Then
                CType(lnkKeepAsNew, linkbutton).text = " " & GetLanguage("UO_KeepNew")
                CType(lnkKeepAsNew, linkbutton).Tooltip = GetLanguage("UO_KeepNew")
            End If

            Dim btnDelete As Control = e.Item.FindControl("btnDelete")

            If Not btnDelete Is Nothing Then
                CType(btnDelete, ImageButton).alternatetext = GetLanguage("delete")
                CType(btnDelete, ImageButton).Tooltip = GetLanguage("delete")
            End If

            Dim lnkMessageViewDelete As Control = e.Item.FindControl("lnkMessageViewDelete")

            If Not lnkMessageViewDelete Is Nothing Then
                CType(lnkMessageViewDelete, linkbutton).text = " " & GetLanguage("delete")
                CType(lnkMessageViewDelete, linkbutton).Tooltip = GetLanguage("delete")
            End If

            Dim Linkbutton1 As Control = e.Item.FindControl("Linkbutton1")

            If Not Linkbutton1 Is Nothing Then
                CType(Linkbutton1, linkbutton).Tooltip = GetLanguage("UO_see_message")
            End If


        End Sub

        Private Sub dgOutbox_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgOutbox.ItemCreated

            Dim btnOutboxDelete As Control = e.Item.FindControl("btnOutboxDelete")

            If Not btnOutboxDelete Is Nothing Then
                CType(btnOutboxDelete, ImageButton).alternatetext = GetLanguage("delete")
                CType(btnOutboxDelete, ImageButton).Tooltip = GetLanguage("delete")
            End If

            Dim lnkOutboxDelete As Control = e.Item.FindControl("lnkOutboxDelete")

            If Not lnkOutboxDelete Is Nothing Then
                CType(lnkOutboxDelete, linkbutton).text = " " & GetLanguage("delete")
                CType(lnkOutboxDelete, linkbutton).Tooltip = GetLanguage("delete")
            End If

            Dim Linkbutton2 As Control = e.Item.FindControl("Linkbutton2")

            If Not Linkbutton2 Is Nothing Then
                CType(Linkbutton2, linkbutton).Tooltip = GetLanguage("UO_see_message")
            End If


        End Sub


        Private Sub BindOutbox()

            Dim dbUserOnline As New ForumUserOnlineDB()

            Dim OutboxSortField As String = ""
            Dim OutboxSortDirection As String = ""

            If Not ViewState("OutboxSortField") Is Nothing Then
                OutboxSortField = CType(ViewState("OutboxSortField"), String)
                OutboxSortDirection = CType(ViewState("OutboxSortDirection"), String)
            End If

            dgOutbox.DataSource = dbUserOnline.TTTForum_PrivateMessage_GetOutbox(ZuserID, OutboxSortField, OutboxSortDirection)
            dgOutbox.Columns(3).HeaderText = GetLanguage("UO_to")
            dgOutbox.Columns(4).HeaderText = GetLanguage("UO_Object")
            dgOutbox.Columns(5).HeaderText = GetLanguage("UO_Date_Send")
            dgOutbox.DataBind()

            If dgOutbox.Items.Count = 0 Then
                pnlOutbox.Visible = False
                pnlNoMessages.Visible = True

            Else
                pnlOutbox.Visible = True
                pnlNoMessages.Visible = False
            End If

            pnlInbox.Visible = False
            pnlCompose.Visible = False

        End Sub

        Private Sub BindCompose()

            pnlInbox.Visible = False
            pnlOutbox.Visible = False
            pnlCompose.Visible = True
            Dim dbUserOnline As New ForumUserOnlineDB()
            Dim dbForumUser As New ForumUserDB()

            If IsNumeric(Request.Params("ReplayId")) Then

                Dim dr As SqlDataReader = dbUserOnline.TTTForum_PrivateMessage_GetSingle(Int16.Parse(Request.Params("ReplyId")))

                If (dr.Read()) Then
                    txtRecipient.Text = ConvertString(dr("Alias"))
                    txtSubject.Text = GetLanguage("UO_Re") & " " & ConvertString(dr("Subject"))
                    FCKeditor1.Value = GetLanguage("UO_ReMessage") + Server.HtmlDecode(ConvertString(dr("Message")))
                End If
                dr.Close()
            End If

            If IsNumeric(Request.Params("userid")) Then
                Dim dr As SqlDataReader = dbForumUser.TTTForum_GetUser(Int16.Parse(Request.Params("userid")))
                If dr.Read Then
                    txtRecipient.Text = ConvertString(dr("Alias"))
                    txtRecipientID.Text = Request.Params("userid")
                End If
                dr.Close()
            End If

        End Sub

        Protected Sub CreateLink()


            Me.cmdInbox.NavigateUrl = TTTUtils.GetURL(GetFullDocument(), Page, String.Format("pmsTabId={0}", 1), "")

            Me.cmdOutbox.NavigateUrl = TTTUtils.GetURL(GetFullDocument(), Page, String.Format("pmsTabId={0}", 2), "")

            Me.cmdCompose.NavigateUrl = TTTUtils.GetURL(GetFullDocument(), Page, String.Format("pmsTabId={0}", 3), "")

        End Sub

        Protected Sub dgPMS_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Dim MessagePanel As Placeholder
            Dim SubjectPanel As Placeholder
            Dim ItemCollection As DataGridItemCollection

            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

                Dim dbUserOnline As New ForumUserOnlineDB()

                If e.CommandName = "inbox" Then
                    ItemCollection = dgInbox.Items
                    dbUserOnline.TTTForum_PrivateMessage_UpdateRead(CType(dgInbox.DataKeys.Item(e.Item.ItemIndex), Integer))
                Else
                    ItemCollection = dgOutbox.Items
                    dbUserOnline.TTTForum_PrivateMessage_UpdateRead(CType(dgOutbox.DataKeys(e.Item.ItemIndex), Integer))
                End If

                Dim CurrentItem As DataGridItem = ItemCollection.Item(e.Item.ItemIndex)

                If e.CommandName = "inbox" Then
                    MessagePanel = CType(CurrentItem.FindControl("pnlMessage"), PlaceHolder)
                    SubjectPanel = CType(CurrentItem.FindControl("pnlSubject"), PlaceHolder)
                Else
                    MessagePanel = CType(CurrentItem.FindControl("pnlMessage2"), PlaceHolder)
                    SubjectPanel = CType(CurrentItem.FindControl("pnlSubject2"), PlaceHolder)
                End If

                If MessagePanel.Visible Then
                    SubjectPanel.Visible = True
                    MessagePanel.Visible = False
                Else
                    MessagePanel.Visible = True
                    SubjectPanel.Visible = False
                End If

            End If

        End Sub

        Protected Sub dgInbox_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)

            ViewState.Add("InboxSortField", e.SortExpression)

            If ViewState("InboxSortDirection") Is Nothing Then
                ViewState("InboxSortDirection") = "ASC"
            Else
                If CType(ViewState("InboxSortDirection"), String) = "ASC" Then
                    ViewState("InboxSortDirection") = "DESC"
                Else
                    ViewState("InboxSortDirection") = "ASC"
                End If
            End If

            BindInbox()

        End Sub

        Protected Sub dgOutbox_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)

            ViewState.Add("OutboxSortField", e.SortExpression)

            If ViewState("OutboxSortDirection") Is Nothing Then
                ViewState("OutboxSortDirection") = "ASC"
            Else
                If CType(ViewState("OutboxSortDirection"), String) = "ASC" Then
                    ViewState("OutboxSortDirection") = "DESC"
                Else
                    ViewState("OutboxSortDirection") = "ASC"
                End If
            End If

            BindOutbox()

        End Sub

        Protected Sub btnReplyMessage_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

            Dim replyURL As String = TTTUtils.GetURL(GetFullDocument(), Page, String.Format("pmsTabId={0}&ReplyId={1}", "3", e.CommandArgument), "")
            Response.Redirect(replyURL)

        End Sub

        Protected Sub btnDeleteMessage_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

            Dim dbUserOnline As New ForumUserOnlineDB()
            dbUserOnline.TTTForum_PrivateMessage_Delete(CType(e.CommandArgument, Integer))

            Dim returnURL As String

            If e.CommandName = "InboxDelete" Then

                returnURL = TTTUtils.GetURL(GetFullDocument(), Page, String.Format("pmsTabId={0}", "1"), "ReplyId=&def=")

            Else

                returnURL = TTTUtils.GetURL(GetFullDocument(), Page, String.Format("pmsTabId={0}", "2"), "replyid=&def=")
                Response.Redirect(returnURL)

            End If

            Response.Redirect(returnURL)

        End Sub

        Protected Sub btnKeepAsNew_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

            Dim dbUserOnline As New ForumUserOnlineDB()
            dbUserOnline.TTTForum_PrivateMessage_UpdateUnread(CType(e.CommandArgument, Integer))
            Dim returnURL As String = TTTUtils.GetURL(GetFullDocument(), Page, String.Format("pmsTabId={0}", "1"), "ReplyId=&def=")
            Response.Redirect(returnURL)

        End Sub

        Private Sub SendMailNotification(ByVal SenderId As Integer, ByVal ReceiverId As Integer, ByVal Subject As String)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If portalSettings.GetSiteSettings(_portalSettings.PortalID)("PMSMailNotice") <> "NO" Then

                Dim StrBody As String
                Dim objUser As New UsersDB()
                Dim Sendername As String = ""
                Dim Zuser As ForumUser = ForumUser.GetForumUser(SenderId)
                Sendername = Zuser.Alias

                Dim dr As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, ReceiverId)
                If dr.Read() Then
                    strBody = GetLanguage("UO_Message_Notice")
                    strBody = Replace(StrBody, "{FullName}", dr("FullName").ToString)
                    strBody = Replace(StrBody, "{SenderName}", SenderName)
                    StrBody = Replace(StrBody, "{MessageURL}", GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId.ToString & "&pmsTabId=1&def=UsersPMS&forumpage=4")
                    strBody = vbCrLf & strBody & vbCrLf
                    Dim StringObject As String = ProcessLanguage(GetLanguage("UO_Notice_Object"))
                    SendNotification(_portalSettings.Email, dr("Email").ToString, "", StringObject, strBody)
                End If
                dr.Close()
            End If
        End Sub



        Protected Sub btnDeleteInboxItems_Click(ByVal sender As Object, ByVal e As System.EventArgs)

            Dim ItemCollection As DataGridItemCollection
            Dim CurrentItem As DataGridItem

            ItemCollection = dgInbox.Items

            For Each CurrentItem In ItemCollection
                If CurrentItem.ItemType = ListItemType.AlternatingItem Or CurrentItem.ItemType = ListItemType.Item Then
                    Dim chkDeleteMessage As HtmlInputCheckBox
                    chkDeleteMessage = CType(CurrentItem.Cells(0).Controls(1), HtmlInputCheckBox)
                    If chkDeleteMessage.Checked Then
                        Dim dbUserOnline As New ForumUserOnlineDB()
                        dbUserOnline.TTTForum_PrivateMessage_Delete(CType(chkDeleteMessage.Value, Integer))
                    End If
                End If
            Next

            BindInbox()

        End Sub

        Protected Sub btnDeleteOutboxItems_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteAllOutbox.Click

            Dim ItemCollection As DataGridItemCollection
            Dim CurrentItem As DataGridItem

            ItemCollection = dgOutbox.Items

            For Each CurrentItem In ItemCollection
                If CurrentItem.ItemType = ListItemType.AlternatingItem Or CurrentItem.ItemType = ListItemType.Item Then
                    Dim chkDeleteMessage As HtmlInputCheckBox
                    chkDeleteMessage = CType(CurrentItem.Cells(0).Controls(1), HtmlInputCheckBox)
                    If chkDeleteMessage.Checked Then
                        Dim dbUserOnline As New ForumUserOnlineDB()
                        dbUserOnline.TTTForum_PrivateMessage_Delete(CType(chkDeleteMessage.Value, Integer))
                    End If
                End If
            Next

            BindOutbox()

        End Sub

        Private Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
            Dim RecipientID As Integer = 0
            lblErrMsg.Text = ""
            lblErrMsg.Visible = False



            If Len(txtRecipient.Text) = 0 _
            OrElse Len(txtSubject.Text) = 0 _
            OrElse Len(Me.FCKeditor1.value) = 0 Then
                lblErrMsg.Text = GetLanguage("UO_Need_Object")
                lblErrMsg.Visible = True
                SetFckEditor()
                Return
            End If

            ' Find user if alias input manually by user inteads of select from the list
            If Len(txtRecipientID.Text) = 0 Then
                Dim dbForumUser As New ForumUserDB()
                Dim dr As SqlDataReader = dbForumUser.TTTForum_GetUserFromAlias(txtRecipient.Text)
                If dr.Read Then
                    RecipientID = ConvertInteger(dr("UserID"))
                End If
                dr.Close()
            Else
                RecipientID = Int16.Parse(txtRecipientID.Text)
            End If

            Dim dbUserOnline As New ForumUserOnlineDB()

            If RecipientID = -1 OrElse RecipientID = 0 Then
                lblErrMsg.Text = GetLanguage("F_NoUserFound")
                lblErrMsg.Visible = True
                SetFckEditor()
                Return
            Else
                FCKeditor1.value = FormatDHtml(FCKeditor1.value)
                dbUserOnline.TTTForum_PrivateMessage_Add(ZuserID, RecipientID, txtSubject.Text, FCKeditor1.value)
                SendMailNotification(ZuserID, RecipientID, txtSubject.Text)
            End If

            If Request.Url.ToString().ToLower().IndexOf(GetFullDocument()) = -1 Then
                Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
            Else
                Response.Redirect(TTTUtils.ForumPMSLink(TabId), True)
            End If

        End Sub

        Private Sub btnFindUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindUser.Click
            pnlFindUser.Visible = True
            pnlUser.Visible = False

            Dim dbForumUser As New ForumUserDB()

            ' dont need "%", as we handle it in sproc
            Dim SearchText As String = Replace(txtRecipient.Text, "*", "")

            drpResults.DataSource = dbForumUser.TTTForum_GetUsers(_portalSettings.PortalId, SearchText)
            drpResults.DataValueField = "UserID"
            drpResults.DataTextField = "Alias"
            drpResults.DataBind()

            lblErrMsg.Visible = False

            If drpResults.Items.Count = 0 Then
                drpResults.Items.Add(New ListItem(GetLanguage("F_NoUser"), ""))
            End If
            SetFckEditor()
        End Sub

        Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
            txtRecipient.Text = drpResults.SelectedItem.Text
            txtRecipientID.Text = drpResults.SelectedItem.Value
            pnlFindUser.Visible = False
            pnlUser.Visible = True
            SetFckEditor()
        End Sub

        Public Function GetImageStyle(ByVal MessageRead As Boolean) As String
            If MessageRead Then
                Return "style=""background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -227px;"""
            Else
                Return "style=""background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -243px;"""
            End If
        End Function


        Public Function GetToolTip(ByVal MessageRead As Boolean) As String
            If MessageRead Then
                Return GetLanguage("UO_OldMessage")
            Else
                Return GetLanguage("UO_NewMessage")
            End If
        End Function
		
		
		
        Private Function FormatDHtml(ByVal _text As String) as String
            ' From Code Project forums...

			_text = ReplaceCaseInsensitive(_text, "<!--", "&lt;!--")
			_text = ReplaceCaseInsensitive(_text, "//-->", "//--&gt;")
            _text = ReplaceCaseInsensitive(_text, "<script", "&lt;script")
            _text = ReplaceCaseInsensitive(_text, "</script", "&lt;/script")
            _text = ReplaceCaseInsensitive(_text, "<input", "&lt;input")
            _text = ReplaceCaseInsensitive(_text, "</input", "&lt;/input")
            _text = ReplaceCaseInsensitive(_text, "<object", "&lt;object")
            _text = ReplaceCaseInsensitive(_text, "</object", "&lt;/object")
            _text = ReplaceCaseInsensitive(_text, "<applet", "&lt;applet")
            _text = ReplaceCaseInsensitive(_text, "</applet", "&lt;/applet")
            _text = ReplaceCaseInsensitive(_text, "<form", "&lt;form")
            _text = ReplaceCaseInsensitive(_text, "</form", "&lt;/form")
            _text = ReplaceCaseInsensitive(_text, "<select", "&lt;select")
            _text = ReplaceCaseInsensitive(_text, "</select", "&lt;/select")
            _text = ReplaceCaseInsensitive(_text, "<option", "&lt;option")
            _text = ReplaceCaseInsensitive(_text, "</option", "&lt;/option")
            _text = ReplaceCaseInsensitive(_text, "<iframe", "&lt;iframe")
            _text = ReplaceCaseInsensitive(_text, "</iframe", "&lt;/iframe")
			Return _text
        End Function 'FormatDisableHtml
			
		
        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Dim returnURL As String = ForumHomeLink(_portalSettings.ActiveTab.TabId)
            Response.Redirect(returnURL)
        End Sub

    End Class

End Namespace