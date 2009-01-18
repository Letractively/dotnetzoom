Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.IO

Namespace DotNetZoom

    Public MustInherit Class PrivateMessagesModule
        Inherits DotNetZoom.PortalModuleControl
        Protected WithEvents pnlNotAuthenticated As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents dlTabs As System.Web.UI.WebControls.DataList
        Protected WithEvents pnlTabs As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents btnDeleteAllInbox1 As System.Web.UI.WebControls.Button
        Protected WithEvents dgInbox As System.Web.UI.WebControls.DataGrid
        Protected WithEvents btnDeleteAllInbox2 As System.Web.UI.WebControls.Button
        Protected WithEvents pnlInbox As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlNoMessages As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents btnDeleteAllOutbox1 As System.Web.UI.WebControls.Button
        Protected WithEvents dgOutbox As System.Web.UI.WebControls.DataGrid
        Protected WithEvents Button2 As System.Web.UI.WebControls.Button
        Protected WithEvents pnlOutbox As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtReceipient As System.Web.UI.WebControls.TextBox
        Protected WithEvents btnFindUser As System.Web.UI.WebControls.Button
        Protected WithEvents rfvReceipient As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents lblErrMsg As System.Web.UI.WebControls.Label
        Protected WithEvents drpResults As System.Web.UI.WebControls.DropDownList
        Protected WithEvents btnSelect As System.Web.UI.WebControls.Button
        Protected WithEvents pnlFindUser As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
        Protected WithEvents rfvSubject As System.Web.UI.WebControls.RequiredFieldValidator
		
        Protected WithEvents FCKeditor1 As FredCK.FCKeditorV2.FCKEditor

        Protected WithEvents btnSend As System.Web.UI.WebControls.Button
        Protected WithEvents pnlCompose As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

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
            rfvReceipient.ErrorMessage = GetLanguage("UO_No_User")
            rfvSubject.ErrorMessage = GetLanguage("UO_Need_Object")
		btnFindUser.Text = GetLanguage("UO_FindUser")
		btnSelect.Text = GetLanguage("UO_Select")
		btnSend.Text = GetLanguage("UO_Send")
		btnDeleteAllInbox1.Text = GetLanguage("delete")
		btnDeleteAllInbox1.ToolTip = GetLanguage("UO_erase")
		btnDeleteAllInbox2.Text = GetLanguage("delete")
		btnDeleteAllInbox2.ToolTip = GetLanguage("UO_erase")
		btnDeleteAllOutbox1.Text = GetLanguage("delete")
		btnDeleteAllOutbox1.ToolTip = GetLanguage("UO_erase")
		Button2.Text = GetLanguage("delete")
		Button2.ToolTip = GetLanguage("UO_erase")
		If Request.Params("edit") <> "" Then
		Title1.DisplayTitle = getlanguage("PrivateMessages")
		end if
            If Page.IsPostBack = False Then

                If Request.IsAuthenticated = False Then
				    Title1.DisplayHelp = "DisplayHelp_NeedToLogin"
                    pnlNotAuthenticated.Visible = True
                    pnlTabs.Visible = False
                    Return
                End If

			
				
                Dim slTabs As SortedList = New SortedList

                slTabs.Add("1", GetLanguage("UO_InBox"))
                slTabs.Add("2", GetLanguage("UO_OutBox"))
                slTabs.Add("3", GetLanguage("UO_write"))

                dlTabs.DataSource = slTabs
                dlTabs.DataBind()

                If Request("pmsTabId") <> "" Then
                    dlTabs.SelectedIndex = Request("pmsTabId") - 1
                Else
                    dlTabs.SelectedIndex = 0
                End If

                If Request("pmsTabId") = 1 Or Request("pmsTabId") = "" Then
                    BindInbox()
					Title1.DisplayHelp = "DisplayHelp_PMSInbox"
                End If

                If Request("pmsTabId") = 2 Then
                    BindOutbox()
					Title1.DisplayHelp = "DisplayHelp_PMSOutbox"
                End If

                If Request("pmsTabId") = 3 Then
					SetFckEditor()
                    BindCompose()
					Title1.DisplayHelp = "DisplayHelp_PMSCompose"
                End If

            End If

        End Sub

		Private Sub SetFckEditor()
	            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
				Dim ZuserID As Integer = Int16.Parse(COntext.User.Identity.Name)
            Dim _UserURL As String = _portalSettings.UploadDirectory
		   		_UserUrl = _UserURL & "userimage/" & ZuserID.ToString
				Session("FCKeditor:UserFilesPath") = _UserURL & "/"
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

            FCKeditor1.Width = Unit.Pixel(700)
				FCKeditor1.Height = unit.pixel(500)
				if GetLanguage("fckeditor_language") <> "auto"
				FCKeditor1.DefaultLanguage = GetLanguage("fckeditor_language")
				FCKeditor1.AutoDetectLanguage = False
				end if
            FCKeditor1.ImageBrowserURL = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
            Dim CSSFileName As String = ""
				CSSFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/fck_editorarea.css")
				If File.Exists(CSSFileName) then
            	FCKeditor1.EditorAreaCSS= _portalSettings.UploadDirectory & "skin/fckeditor/fck_editorarea.css"
				end if

				CSSFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/fckstyles.xml")
				If File.Exists(CSSFileName) then
				FCKeditor1.StylesXmlPath =  _portalSettings.UploadDirectory & "skin/fckeditor/fckstyles.xml"
    			End If

				CSSFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/")
				If Directory.Exists(CSSFileName) then
				FCKeditor1.SkinPath =  _portalSettings.UploadDirectory & "skin/fckeditor/"
    			End If
				
	   			Dim _UserphysicalPath As String = Server.MapPath(_UserURL)		
	       		If Not Directory.Exists(_UserphysicalPath) Then
               		Try
                	IO.Directory.CreateDirectory(_UserphysicalPath)
                	Catch exc As System.Exception
                	End Try
            	End If
	
		
		End Sub
		
		
        Private Sub BindInbox()

            Dim objMessages As MessagesDB = New MessagesDB

            Dim InboxSortField As String = ""
            Dim InboxSortdirection As String = ""

            If Not ViewState("InboxSortField") = Nothing Then
                InboxSortField = ViewState("InboxSortField")
                InboxSortdirection = ViewState("InboxSortDirection")
            End If

            dgInbox.DataSource = objMessages.GetPrivateMessagesInbox((New Utility).GetUserID(), InboxSortField, InboxSortdirection)
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

            pnlOutbox.Visible = False
            pnlCompose.Visible = False

        End Sub

        Private Sub BindOutbox()

            Dim objMessages As MessagesDB = New MessagesDB

            Dim OutboxSortField As String = ""
            Dim OutboxSortDirection As String = ""

            If Not ViewState("OutboxSortField") = Nothing Then
                OutboxSortField = ViewState("OutboxSortField")
                OutboxSortDirection = ViewState("OutboxSortDirection")
            End If

            dgOutbox.DataSource = objMessages.GetPrivateMessagesOutbox((New Utility).GetUserID(), OutboxSortField, OutboxSortDirection)
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

        Private Sub BindCompose()

            pnlInbox.Visible = False
            pnlOutbox.Visible = False
            pnlCompose.Visible = True

            If Request("ReplyId") <> "" Then
                Dim objUser As MessagesDB = New MessagesDB

                Dim dr As SqlDataReader = objUser.GetSinglePrivateMessage(Request("ReplyId"))

                If (dr.Read()) Then
                    txtReceipient.Text = dr("Username")
                    txtSubject.Text = GetLanguage("UO_Re") & dr("Subject")
					FCKeditor1.value = GetLanguage("UO_ReMessage")  + Server.HtmlDecode(dr("Message"))
					FCKeditor1.value = FormatDisableHtml(FCKeditor1.value)
                End If

                dr.Close()

            End If

            If Request("UserID") <> "" Then
                Dim objUser As UsersDB = New UsersDB

                Dim dr As SqlDataReader = objUser.GetSingleUser(Me.PortalId, Int32.Parse(Request("UserID")))

                If (dr.Read()) Then
                    txtReceipient.Text = dr("Username")
                End If

                dr.Close()

            End If

        End Sub

        Protected Function GetSubLink(ByVal pmsTabId As String) As String

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Request.Params("edit") <> "" Then
                Return GetFullDocument() & "?edit=control&amp;tabid=" & _portalSettings.ActiveTab.TabId & "&amp;pmsTabId=" & pmsTabId & "&amp;def=PrivateMessages"
            Else
                Return GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&amp;pmsTabId=" & pmsTabId
            End If

        End Function

        Protected Sub dgPMS_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Dim MessagePanel As PlaceHolder
            Dim ItemCollection As DataGridItemCollection

            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

                Dim objMessages As MessagesDB = New MessagesDB

                If e.CommandName = "inbox" Then
                    ItemCollection = dgInbox.Items
                    objMessages.UpdateMessageRead(dgInbox.DataKeys(e.Item.ItemIndex))
                Else
                    ItemCollection = dgOutbox.Items
                    objMessages.UpdateMessageRead(dgOutbox.DataKeys(e.Item.ItemIndex))
                End If

                Dim CurrentItem As DataGridItem
                For Each CurrentItem In ItemCollection
                    If CurrentItem.ItemIndex = e.Item.ItemIndex Then

                        MessagePanel = CurrentItem.Cells(4).Controls(3)

                        If MessagePanel.Visible Then
                            MessagePanel.Visible = False
                        Else
                            MessagePanel.Visible = True
                        End If

                        Exit For

                    End If
                Next

            End If

        End Sub

        Protected Sub dgInbox_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)

            ViewState.Add("InboxSortField", e.SortExpression)

            If ViewState("InboxSortDirection") Is Nothing Then
                ViewState("InboxSortDirection") = "ASC"
            Else
                If ViewState("InboxSortDirection") = "ASC" Then
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
                If ViewState("OutboxSortDirection") = "ASC" Then
                    ViewState("OutboxSortDirection") = "DESC"
                Else
                    ViewState("OutboxSortDirection") = "ASC"
                End If
            End If

            BindOutbox()

        End Sub

        Protected Sub btnReplyMessage_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Request.Params("edit") <> "" Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&pmsTabId=3&def=PrivateMessages&ReplyId=" & e.CommandArgument)
            Else
                Response.Redirect(GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&pmsTabId=3&ReplyId=" & e.CommandArgument)
            End If

        End Sub

        Protected Sub btnDeleteMessage_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

            Dim objMessages As MessagesDB = New MessagesDB
            objMessages.DeletePrivateMessages(e.CommandArgument)

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If e.CommandName = "InboxDelete" Then
                If Request.Params("edit") <> "" Then
                    Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=PrivateMessages&pmsTabId=1")
                Else
                    Response.Redirect(GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&pmsTabId=1")
                End If
            Else
                If Request.Params("edit") <> "" Then
                    Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=PrivateMessages&pmsTabId=2")
                Else
                    Response.Redirect(GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&pmsTabId=2")
                End If
            End If

        End Sub

        Protected Sub btnKeepAsNew_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

            Dim objMessages As MessagesDB = New MessagesDB
            objMessages.UpdateMessageUnread(e.CommandArgument)

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Request.Params("edit") <> "" Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=PrivateMessages&pmsTabId=1")
            Else
                Response.Redirect(GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&pmsTabId=1")
            End If

        End Sub

        Protected Sub btnDeleteInboxItems_Click(ByVal sender As Object, ByVal e As System.EventArgs)

            Dim ItemCollection As DataGridItemCollection
            Dim CurrentItem As DataGridItem

            ItemCollection = dgInbox.Items

            For Each CurrentItem In ItemCollection
                If CurrentItem.ItemType = ListItemType.AlternatingItem Or CurrentItem.ItemType = ListItemType.Item Then
                    Dim chkDeleteMessage As HtmlInputCheckBox
                    chkDeleteMessage = CurrentItem.Cells(0).Controls(1)
                    If chkDeleteMessage.Checked Then
                        Dim objMessages As MessagesDB = New MessagesDB
                        objMessages.DeletePrivateMessages(chkDeleteMessage.Value)
                    End If
                End If
            Next

            BindInbox()

        End Sub

        Protected Sub btnDeleteOutboxItems_Click(ByVal sender As Object, ByVal e As System.EventArgs)

            Dim ItemCollection As DataGridItemCollection
            Dim CurrentItem As DataGridItem

            ItemCollection = dgOutbox.Items

            For Each CurrentItem In ItemCollection
                If CurrentItem.ItemType = ListItemType.AlternatingItem Or CurrentItem.ItemType = ListItemType.Item Then
                    Dim chkDeleteMessage As HtmlInputCheckBox
                    chkDeleteMessage = CurrentItem.Cells(0).Controls(1)
                    If chkDeleteMessage.Checked Then
                        Dim objMessages As MessagesDB = New MessagesDB
                        objMessages.DeletePrivateMessages(chkDeleteMessage.Value)
                    End If
                End If
            Next

            BindOutbox()

        End Sub

        Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objMessages As MessagesDB = New MessagesDB

            Dim UserId As Integer = objMessages.GetUserId(_portalSettings.PortalId, txtReceipient.Text)


            If Len(txtSubject.Text) = 0 _
            OrElse Len(Me.FCKeditor1.value) = 0 Then
                lblErrMsg.Text = GetLanguage("UO_Need_Object") + "<br>"
                lblErrMsg.Visible = True
                SetFckEditor()
                Return
            End If



            If UserId = -1 Then
                lblErrMsg.Text = GetLanguage("UO_No_User") + "<br>"
                lblErrMsg.Visible = True
                SetFckEditor()
                Return
            Else
                FCKeditor1.value = FormatDisableHtml(FCKeditor1.value)
                objMessages.AddPrivateMessage((New Utility).GetUserID(), UserId, txtSubject.Text, FCKeditor1.value)
                SendMailNotification((New Utility).GetUserID(), UserId, txtSubject.Text)
            End If

            If Request.Params("edit") <> "" Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=PrivateMessages&pmsTabId=1")
            Else
                Response.Redirect(GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&pmsTabId=1")
            End If

        End Sub

        Private Sub SendMailNotification(ByVal SenderId As Integer, ByVal ReceiverId As Integer, ByVal Subject As String)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If portalSettings.GetSiteSettings(_portalSettings.PortalID)("PMSMailNotice") <> "NO" Then
                Dim StrBody As String
                Dim objUser As New UsersDB()
                Dim Sendername As String = ""

                Dim drI As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, SenderID)
                If drI.Read() Then
                    SenderName = drI("Username")
                End If
                drI.Close()

                Dim dr As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, ReceiverId)
                If dr.Read() Then
                    strBody = GetLanguage("UO_Message_Notice")
                    strBody = Replace(StrBody, "{FullName}", dr("FullName"))
                    strBody = Replace(StrBody, "{SenderName}", SenderName)
                    StrBody = Replace(StrBody, "{MessageURL}", GetFullDocument() & "?edit=control&pmsTabId=1&def=PrivateMessages")
                    strBody = vbCrLf & strBody & vbCrLf
                    Dim StringObject As String = ProcessLanguage(GetLanguage("UO_Notice_Object"))
                    SendNotification(_portalSettings.Email, dr("Email").ToString, "", StringObject, strBody)
                End If
                dr.Close()
            End If
        End Sub


        Private Sub btnFindUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindUser.Click
            pnlFindUser.Visible = True

            Dim objMessages As MessagesDB = New MessagesDB
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim SearchText As String

            If txtReceipient.Text.Length = 0 Then
                SearchText = "%"
            Else
                SearchText = Replace(txtReceipient.Text, "*", "%")
            End If

            drpResults.DataSource = objMessages.GetUsersByUsername(_portalSettings.PortalId, SearchText)
            drpResults.DataBind()

            lblErrMsg.Visible = False

            If drpResults.Items.Count = 0 Then
                drpResults.Items.Add(New ListItem(GetLanguage("UO_NotFound"), ""))
            End If
            SetFckEditor()
        End Sub

        Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
            txtReceipient.Text = drpResults.SelectedItem.Value
            pnlFindUser.Visible = False
            SetFckEditor()
        End Sub

        Public Function GetToolTip(ByVal MessageRead As Boolean) As String
            If MessageRead Then
                Return GetLanguage("UO_OldMessage")
            Else
                Return GetLanguage("UO_NewMessage")
            End If
        End Function


        Public Function GetImageStyle(ByVal MessageRead As Boolean) As String
            If MessageRead Then
                Return "style=""background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -227px;"""
            Else
                Return "style=""background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -243px;"""
            End If
        End Function


        Protected Function GetUserInfoLink(ByVal userID As String) As String
            Return GetFullDocument() & "?edit=control&TabID=" & TabId.ToString() + "&def=UserInfo&UserID=" + userID
        End Function

        Protected Function GetUserInfoTooltip(ByVal userName As String) As String
		    Return replace(GetLanguage("UO_see_user"), "{username}", userName)
        End Function

        Protected Function GetTableSizeUnit() As Unit
            If Request.Url.ToString().ToLower().IndexOf("default.aspx") = -1 Then
                Return Unit.Pixel(750)
            Else
                Return Unit.Percentage(100)
            End If
        End Function

        Private Function GetCaseInsensitiveSearch(ByVal search As String) As String
            Dim result As String = String.Empty

            Dim index As Integer

            For index = 0 To search.Length - 1
                Dim character As Char = search.Chars(index)
                Dim characterLower As Char = Char.ToLower(character)
                Dim characterUpper As Char = Char.ToUpper(character)

                If characterUpper = characterLower Then
                    result = result + character
                Else
                    result = result + "[" + characterLower + characterUpper + "]"
                End If

            Next index
            Return result
        End Function 'GetCaseInsensitiveSearch
		
		
        Private Function FormatDisableHtml(ByVal _text As String) as String
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
		
        Private Function ReplaceCaseInsensitive(ByVal text As String, ByVal oldValue As String, ByVal newValue As String) As String
            oldValue = GetCaseInsensitiveSearch(oldValue)

            Return Regex.Replace([text], oldValue, newValue)

        End Function 'ReplaceCaseInsensitive
		
		

    End Class

End Namespace