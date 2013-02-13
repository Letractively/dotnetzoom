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


Imports System.Configuration
Imports System.IO
Imports System.Text
Imports System.Web.Mail
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public Class TTT_EditForumPost
        Inherits DotNetZoom.PortalModuleControl

		
    	Protected WithEvents MyHtmlImage As System.Web.UI.WebControls.Image
		Protected WithEvents txticone As System.Web.UI.WebControls.TextBox
		Protected WithEvents lnkicone As System.Web.UI.WebControls.hyperlink
		Protected WithEvents btnBack As System.Web.UI.WebControls.Button
		
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
        Protected WithEvents txtThreadID As System.Web.UI.WebControls.TextBox
        Protected WithEvents ddlForumGroup As System.Web.UI.WebControls.DropDownList
        Protected WithEvents ddlForum As System.Web.UI.WebControls.DropDownList
        Protected WithEvents pnlForumSelect As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblAuthor As System.Web.UI.WebControls.Label
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
        Protected WithEvents pnlOldPost As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents RteDeskTop As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkPinned As System.Web.UI.WebControls.CheckBox
        Protected WithEvents pnlPinned As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtMessage As System.Web.UI.WebControls.TextBox
        Protected WithEvents pnlImage As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents chkNotify As System.Web.UI.WebControls.CheckBox
        Protected WithEvents btnPreview As System.Web.UI.WebControls.Button
        Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
        Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
        Protected WithEvents pnlNewPost As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblPreview As System.Web.UI.WebControls.Label
        Protected WithEvents btnBackEdit As System.Web.UI.WebControls.Button
        Protected WithEvents pnlPreview As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblModerate As System.Web.UI.WebControls.Label
        Protected WithEvents btnBackForum As System.Web.UI.WebControls.Button
        Protected WithEvents pnlModerate As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents ctlForumNav As DotNetZoom.TTT_ForumNavigator
        Protected WithEvents pnlNotify As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlGetSmiley As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents lblScript As System.Web.UI.WebControls.Literal
		
        Protected WithEvents FCKeditor1 As DotNetZoom.FCKEditor

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

		Private Zconfig As ForumConfig 
        Private _author As ForumUser
        Private _tabid As Integer
        Private action As String
        Private ZforumID As Integer
        Private _threadid As Integer
        Private _threadpage As Integer
        Private _postid As Integer
        

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
            ' Obtain PortalSettings from Current Context

            If Not Context.Request.IsAuthenticated Then
                Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "def=Register"), True)
            End If



			lnkicone.Tooltip = GetLanguage("select_icone_tooltip")	
			lnkicone.Text = GetLanguage("select_icone")	
			btnBackForum.Text = GetLanguage("return")
			btnBackForum.Tooltip = GetLanguage("return")
			btnBack.Text = GetLanguage("return")
			btnBack.Tooltip = GetLanguage("return")
			btnPreview.Text = GetLanguage("preview")	
			btnPreview.Tooltip = GetLanguage("preview")	
			btnSubmit.Text = GetLanguage("enregistrer")	
			btnSubmit.Tooltip = GetLanguage("enregistrer")
			btnCancel.Text = GetLanguage("annuler")	
			btnCancel.Tooltip = GetLanguage("annuler")
			btnBackEdit.Text = GetLanguage("return")	
			btnBackEdit.Tooltip = GetLanguage("return")		


            If Zconfig Is Nothing Then
                Zconfig = ForumConfig.GetForumConfig(ModuleId)
            End If


            _tabid = TabId

            If _author Is Nothing Then
                _author = ForumUser.GetForumUser(Int16.Parse(Context.User.Identity.Name))
            End If

            If IsNumeric(Request.Params("forumid")) AndAlso ZforumID = 0 Then
                ZforumID = Int32.Parse(Request.Params("forumid"))
            End If

            If IsNumeric(Request.Params("threadid")) AndAlso _threadid = 0 Then
                _threadid = Int32.Parse(Request.Params("threadid"))
            End If

            If IsNumeric(Request.Params("threadpage")) AndAlso _threadpage = 0 Then
                _threadpage = Int32.Parse(Request.Params("threadpage"))
            End If

            If IsNumeric(Request.Params("postid")) AndAlso _postid = 0 Then
                _postid = Int32.Parse(Request.Params("postid"))
            End If

            If Not (Request.Params("action") Is Nothing) AndAlso (action Is Nothing) Then
                action = Request.Params("action")
                btnSubmit.CommandName = action
            End If



            RteDeskTop.Visible = (_author.UseRichText = True)
            txtMessage.Visible = (_author.UseRichText = False)

            Dim ZuserID As Integer = Int16.Parse(COntext.User.Identity.Name)
            Dim _UserURL As String = _portalSettings.UploadDirectory
            _UserURL = _UserURL & "userimage/" & ZuserID.ToString

            Session("FCKeditor:UserFilesPath") = _UserURL & "/"

            If Request.IsAuthenticated = False Then
                FCKeditor1.LinkBrowserURL = ""
                Session("FCKeditor:UserFilesPath") = Nothing
                FCKeditor1.ImageBrowserURL = ""
            Else
                Dim _UserphysicalPath As String = Server.MapPath(_UserURL)
                If Not Directory.Exists(_UserphysicalPath) Then
                    Try
                        IO.Directory.CreateDirectory(_UserphysicalPath)
                    Catch exc As System.Exception
                    End Try
                End If
                FCKeditor1.ImageBrowserURL = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
                Dim objAdmin As New AdminDB()
                Dim tmpUploadRoles As String = ""
                If Not CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("uploadroles"), String) Is Nothing Then
                    tmpUploadRoles = CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("uploadroles"), String)
                End If
                If PortalSecurity.IsInRoles(tmpUploadRoles) Then
                    FCKeditor1.LinkBrowserURL = glbPath & "admin/AdvFileManager/filemanager.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
                Else
                    FCKeditor1.LinkBrowserURL = ""
                End If
            End If

            SetEditor(FCKeditor1)
            FCKeditor1.Width = Unit.Pixel(700)
            FCKeditor1.Height = unit.pixel(500)



            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If

                If ZforumID = 0 Then
                    Me.pnlForumSelect.Visible = True
                    Me.pnlNewPost.Visible = False
                    Me.lblInfo.Text = GetLanguage("F_NeedF")
                    'BindForumGroup()
                Else
                    GeneratePost()
                    lblInfo.Visible = False
                End If

                pnlNotify.Visible = Zconfig.MailNotification
                pnlGetSmiley.Visible = (Zconfig.AvatarModuleID > 0)


            End If


            Dim ParentID As String
            If _author.UseRichText = True Then
                pnlGetSmiley.Visible = False
            Else
                ParentID = Server.HtmlEncode(txtMessage.ClientID)
            End If
            lblScript.Text = "<a href=""javascript:OpenNewWindow('" + ParentID + "&mid=" + ModuleId.ToString + "') "">"


            If txticone.Text <> "" Then
                MyHtmlImage.ImageUrl = txticone.Text
                MyHtmlImage.AlternateText = txticone.Text
                MyHtmlImage.ToolTip = txticone.Text
                MyHtmlImage.Visible = True
            Else
                MyHtmlImage.ImageUrl = glbPath & "images/1x1.gif"
		   	myHtmlImage.AlternateText = "*"
	   		myHtmlImage.ToolTip = "*"
			MyHtmlImage.Visible = True
			end if
			ParentID = Server.HtmlEncode(TxtIcone.ClientID)
            lnkicone.NavigateUrl = "javascript:OpeniconeWindow('" + tabID.ToString + "','" + ParentID + "')"
            btnSubmit.Attributes.Add("onclick", "setajaxloading('#hide');")
            JQueryScript(Me.Page)
        End Sub

        Private Sub PopulateGallery()

            Dim dbForum As New ForumDB()
            Dim _forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)
            Dim _galleryID As Integer = _forumInfo.IntegratedGallery
            Dim _albumName As String = _forumInfo.IntegratedAlbumName

            ' <tam:galleryconfig note=process only if a config exists, this should be done from TTTGallery module>
            If _forumInfo.IntegratedGallery = 0 OrElse Len(_forumInfo.IntegratedAlbumName) = 0 Then
                lblInfo.Text = GetLanguage("F_NoGal")
                pnlImage.Visible = False
                Return
            End If
            '</tam:galleryconfig>

            Dim ZgalleryConfig As GalleryConfig = GalleryConfig.GetGalleryConfig(_galleryID)
            Dim _thumbsURL As String = TTTUtils.BuildPath(New String(2) {ZgalleryConfig.RootURL, _albumName, ZgalleryConfig.ThumbFolder}, "/", False, False)
            Dim _physicalPath As String = Server.MapPath(_thumbsURL)

            ' <tam:getfile note=populate gallery folder to get file list>
            Try
                 If Directory.Exists(_physicalPath) Then
                 Session("ForumThumbPath") = _thumbsURL 
                Else
				    Session("ForumThumbPath") = Nothing
                    lblInfo.Text = GetLanguage("F_NoGalR")
                    txtIcone.Visible = False
					lnkIcone.Visible = False
					MyHTMLImage.Visible = False
					pnlImage.Visible = False
                    Return
                End If

            Catch Exc As System.Exception
                lblInfo.Text = Exc.ToString
                Return
            End Try
            '</tam:getfile>
            
			txtIcone.Visible = True
			lnkIcone.Visible = True
			MyHTMLImage.Visible = True
			pnlImage.Visible = True

			
        End Sub

		
		
		
        Private Sub StartNewPost()
            ZforumID = Int16.Parse(ddlForum.Items(ddlForum.SelectedIndex).Value)
            Me.pnlForumSelect.Visible = False
            Me.pnlNewPost.Visible = True
            Me.lblInfo.Text = replace(GetLanguage("F_NewMP"), "{forum}" ,  ddlForum.Items(ddlForum.SelectedIndex).Text & "</>")
            GeneratePost()
        End Sub

        Private Sub GeneratePost()
            Dim dbForum As New ForumDB()
            Dim fPost As ForumPostInfo
			Dim fThread As ForumThreadInfo
          
			PopulateGallery()
            Dim authorName As String = _author.Alias
            If action = "new" OrElse action = "edit" Then
                ' display pinned check if user is admin
                If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) Then
                    pnlPinned.Visible = True
                Else
                    pnlPinned.Visible = False
                End If


                lblAuthor.Text = authorName
                txtThreadID.Text = "0"

                Dim _forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)
                If not _forumInfo.IsIntegrated Then
                    txtIcone.Visible = False
					lnkIcone.Visible = False
					MyHTMLImage.Visible = False
					pnlImage.Visible = False
                End If

            End If

            If action = "edit" OrElse action = "reply" OrElse action = "quote" OrElse action = "delete" Then
                
                fPost = ForumPostInfo.GetPostInfo(_postid)

                lblAuthor.Text = fPost.User.Alias
                lblMessage.Text = Server.HtmlDecode(fPost.Body)

                If action = "edit" Then
                    pnlOldPost.Visible = False

                    txtThreadID.Text = fPost.ThreadID.ToString
                    txtSubject.Text = fPost.Subject
					
					' put back the checked pinned
					Fthread = ForumThreadInfo.GetThreadInfo(fPost.ThreadID)
					chkPinned.Checked =  Fthread.pinned 

                    ' <tam:note insert value for keeping this value when update thread, even it invisible />
                    chkNotify.Checked = fPost.Notify
                    txtIcone.Text = fPost.Parent.Image

		
					If _author.UseRichText Then
                        FCKeditor1.value = Server.HtmlDecode(fPost.Body)
                    Else
                        txtMessage.Text() = Server.HtmlDecode(fPost.Body)
                    End If
                ElseIf action = "delete" Then
				
				
			'Modification de rene pour effacer les posts si super user 2004-04-09

            Dim _authorID As Integer = Int16.Parse(Context.User.Identity.Name)
         
		    If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
            OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) Then
			
			authorName = replace(GetLanguage("F_DeletedBy"),"{authorName}", authorName)
			authorName = replace(authorName, "{object}" , fPost.Subject)
			            
			dbForum.TTTForum_DeletePost(_PostID, _authorID,  authorName)
			Else 
			Dim TempString As string = replace(getLanguage("F_Deleted"), "{authorName}", authorName) & ProcessLanguage("{date}")
			dbForum.TTTForum_UpdatePost(_threadID, _postid, False , GetLanguage("list_none"), TempString, false, "", _authorID, authorName, True, False)
			
			End If

			' Reset Cache
			  ForumItemInfo.ResetForumInfo(ZforumID)
              ForumThreadInfo.ResetThreadInfo(_threadID)
              ForumUser.ResetForumUser(_authorID)
			  ForumPostInfo.ResetPostInfo(_postid)
			
			
			
			Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)
			If forumInfo.IsActive and not forumInfo.IsPrivate Then
			' Make new RSS Feed here
                        Dim RSSURL As String = GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId
			Dim dr As SqlDataReader = dbForum.TTTForum_GetThreads(ZforumID, 20, 0, RSSURL)
			CreateForumRSS(dr, "Subject", "LastPostAlias", "URLField", "DateLastPost", forumInfo.name & " " & forumInfo.Description, Request.MapPath(_portalSettings.UploadDirectory) & "forum" & ZforumID.ToString & ".xml")
			dr.Close()

			end if
  
			
			
                    Dim strURL As String = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, forumid = " & ZforumID & " & scope = thread)
            Response.Redirect(strURL)
            	
			
			
			'fin de la modification
			
	                Else ' <tam:note reply or quote />
                        Dim subject As String = fPost.Subject
                        Dim replySubject As String = subject

                        pnlOldPost.Visible = True

                        If (replySubject.Length >= 3) Then
                            If Not (replySubject.Substring(0, 3) = GetLanguage("F_RE")) Then
                                replySubject = GetLanguage("F_RE") + " " + replySubject
                            End If
                        Else
                            replySubject = GetLanguage("F_RE") + " " + replySubject
                        End If
                        txtSubject.Text = replySubject
                        txtThreadID.Text = fPost.ThreadID.ToString
                    End If

                    If (action = "quote") Then
                        Dim fText As ForumText = New ForumText(Server.HtmlDecode(fPost.Body), Zconfig)
						If _author.UseRichText Then
                            FCKeditor1.value = fText.ProcessQuoteBody(fPost.User.Alias, True) '<tam:note alias be implemented>
                        Else
                            txtMessage.Text = fText.ProcessQuoteBody(fPost.User.Alias, False) '<tam:note alias be implemented>
                        End If
                    End If
                End If


        End Sub


        Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
   
            Dim _forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)
            Dim _newPostID As Integer
            Dim _parentPostID As Integer
            Dim _threadID As Integer = Int16.Parse(txtThreadID.Text)
            Dim _authorID As Integer = Int16.Parse(Context.User.Identity.Name)
            Dim _author As ForumUser = ForumUser.GetForumUser(_authorID)
            Dim _AuthorAlias As String = _author.Alias
            Dim _RemoteAddr As String = Request.ServerVariables("REMOTE_ADDR")
            Dim _notify As Boolean = chkNotify.Checked
            Dim _subject As String = txtSubject.Text
            Dim _isPinned As Boolean = Me.chkPinned.Checked
            Dim _image As String = txtIcone.Text
            Dim _isTrusted As Boolean = _author.IsTrusted
            Dim _isModerated As Boolean = _forumInfo.IsModerated
            Dim _body As String
            Dim dbForum As New ForumDB()
			'Modification de rene pour mettre à jour les stats 2004-04-17
    		  Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ForumStatistics.ResetStats(_portalSettings.PortalID, ModuleID)

	
            lblInfo.Text = ""
            If Len(_subject) = 0 Then
                lblInfo.Text = GetLanguage("F_NeedObject")
                lblInfo.Visible = True
                Return
            End If

			
			
			
            Select Case action
                Case "new"
					If _author.UseRichText Then
                	_body = Server.HtmlEncode(FCKeditor1.value) & "<br>" & _author.signature
                	Else
                	_body = txtMessage.Text & "<br>" & _author.signature
                	End If

                    _newPostID = dbForum.TTTForum_AddPost(0, ZforumID, _authorID, _RemoteAddr, _notify, _subject, _body, _isPinned, _image, 0)

                    'Clear threadinfo cache to make sure new data will be displayed
                    ForumItemInfo.ResetForumInfo(ZforumID)
                    ForumThreadInfo.ResetThreadInfo(_threadID)
                    ForumUser.ResetForumUser(_authorID)
                Case "reply"
				   	If _author.UseRichText Then
                   	_body = Server.HtmlEncode(FCKeditor1.value) & "<br>" & _author.signature
            		Else
                	_body = txtMessage.Text & "<br>" & _author.signature
            		End If
                    _parentPostID = _postid
                    _newPostID = dbForum.TTTForum_AddPost(_parentPostID, ZforumID, _authorID, _RemoteAddr, _notify, _subject, _body, _isPinned, _image, 0)

                    'Clear threadinfo & postinfo cache to make sure new data will be displayed
                    ForumItemInfo.ResetForumInfo(ZforumID)
                    ForumThreadInfo.ResetThreadInfo(_threadID)
                    ForumUser.ResetForumUser(_authorID)
                Case "quote"
					
					If _author.UseRichText Then
                	_body = Server.HtmlEncode(FCKeditor1.value) & "<br>" & _author.signature
                	Else
                	_body = txtMessage.Text & "<br>" & _author.signature
                	End If

                    _parentPostID = _postid
                    _newPostID = dbForum.TTTForum_AddPost(_parentPostID, ZforumID, _authorID, _RemoteAddr, _notify, _subject, _body, _isPinned, _image, 0)

                    'Clear threadinfo & postinfo cache to make sure new data will be displayed
                    ForumItemInfo.ResetForumInfo(ZforumID)
                    ForumThreadInfo.ResetThreadInfo(_threadID)
                    ForumUser.ResetForumUser(_authorID)
                Case "edit"
				    If _author.UseRichText Then
                	_body = Server.HtmlEncode(FCKeditor1.value) 
                	Else
                	_body = txtMessage.Text 
                	End If
			
				
                    dbForum.TTTForum_UpdatePost(_threadID, _postid, _notify, _subject, _body, _isPinned, _image, _authorID, _AuthorAlias, True, False)

                    _newPostID = _postid

                    'Clear postinfo cache to make sure new data will be displayed
                   
                    ForumUser.ResetForumUser(_authorID)
                Case "delete"
					If _author.UseRichText Then
                	_body = Server.HtmlEncode(FCKeditor1.value) 
                	Else
                	_body = txtMessage.Text 
                	End If
                    dbForum.TTTForum_UpdatePost(_threadID, _postid, _notify, _subject, _body, _isPinned, _image, _authorID, _AuthorAlias, True, False)
					_isTrusted = true
                    _newPostID = _postid

                    'Clear postinfo cache to make sure new data will be displayed
                    
                    ForumUser.ResetForumUser(_authorID)
            End Select

			ForumPostInfo.ResetPostInfo(_postid)
            Dim returnURL As String = GetReturnURL(_newPostID)

            ' Send notification email
            Dim _emailType As ForumEmail.ForumEmailType
            Dim _URL As String

            If _isModerated AndAlso Not _isTrusted Then
                _emailType = ForumEmail.ForumEmailType.PostModerate
                _URL = GetFullDocument() & "?tabid=" & _tabid '& "&scope=moderateforum&forumpage=0"
                If Zconfig.MailNotification Then SendForumMail(_newPostID, _URL, _emailType)

                Me.pnlNewPost.Visible = False
                Me.pnlOldPost.Visible = False
                Me.pnlModerate.Visible = True
				Dim objAdmin As New AdminDB()
				Me.lblModerate.Text = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "Forum_Moderated")
				

            Else
                _emailType = ForumEmail.ForumEmailType.PostNormal
                _URL = returnURL
                If Zconfig.MailNotification Then SendForumMail(_newPostID, _URL, _emailType)
	
				If _forumInfo.IsActive and not _forumInfo.IsPrivate Then
				' Make new RSS Feed here
                    Dim RSSURL As String = GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId
	 			Dim dr As SqlDataReader = dbForum.TTTForum_GetThreads(ZforumID, 20, 0, RSSURL)
				CreateForumRSS(dr, "Subject", "LastPostAlias", "URLField", "DateLastPost",  _forumInfo.name & " " & _forumInfo.Description, Request.MapPath(_portalSettings.UploadDirectory) & "forum" & ZforumID.ToString & ".xml")
				dr.Close()
				end if
                Response.Redirect(returnURL)

            End If

        End Sub

        Private Function GetReturnURL(ByVal PostID As Integer) As String
            Dim returnString As String

            If action = "new" Then
                returnString = GetFullDocument() & "?forumid=" & ZforumID & "&tabid=" & _tabid & "&scope=post&threadid=" & PostID.ToString
            ElseIf action = "edit" OrElse action = "delete" Then
                returnString = CType(ViewState("UrlReferrer"), String)
            Else ' for reply & quote
                Dim pageCount As Double = Math.Floor((ForumDB.TTTForum_GetThreadRepliesCount(_threadid)) / Zconfig.PostsPerPage) + 1
                If pageCount > 1 Then
                    returnString = GetFullDocument() & "?forumid=" & ZforumID & "&tabid=" & _tabid & "&scope=post&threadid=" & _threadid & "&threadpage=" & pageCount.ToString
                Else
                    returnString = GetFullDocument() & "?forumid=" & ZforumID & "&tabid=" & _tabid & "&scope=post&threadid=" & _threadid.ToString
                End If
            End If

            Return returnString

        End Function


        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub


        Protected Sub InsertAvatar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
            ' Get the letter value clicked
            Dim avatar As String = CType(sender, ImageButton).CommandArgument
            	
            If _author.UseRichText = True Then
                FCKeditor1.value += avatar
            Else
                txtMessage.Text += avatar
            End If

        End Sub

        Private Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click
            Dim fText As ForumText
	
			
            If _author.UseRichText Then
                fText = New ForumText(FCKeditor1.value, Zconfig)
            Else
                fText = New ForumText(txtMessage.Text, Zconfig)
            End If

            lblPreview.Text = fText.Process()
            pnlPreview.Visible = True
            pnlNewPost.Visible = False
        End Sub

        Private Sub btnBackEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBackEdit.Click
            pnlPreview.Visible = False
            pnlNewPost.Visible = True
        End Sub

        Private Sub btnBackForum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBackForum.Click
            ' Redirect back to the forum thread page
            Dim strURL As String = GetFullDocument() & "?forumid=" & ZforumID & "&tabid=" & _tabid & "&scope=thread"
            Response.Redirect(strURL)
        End Sub

        Private Sub ctlForumNav_ForumSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlForumNav.ForumSelected
            ZforumID = ctlForumNav.SelectedForum.ForumID
            Dim strURL As String = GetURL(GetFullDocument(), Page, String.Format("forumid={0}", ZforumID), "")
            Response.Redirect(strURL)
        End Sub

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub

		
     End Class

End Namespace