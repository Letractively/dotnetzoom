'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'=======================================================================================
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by:
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' =======================================================================================


Imports System
Imports System.Configuration
Imports System.Collections
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.HttpUtility
Imports DotNetZoom.TTTUtils
Imports System.Web.Caching
Imports System.Text.RegularExpressions

Namespace DotNetZoom

#Region "ForumThread"

    Public Class ForumThread
        Inherits ForumObject

        Private _flatView As Boolean
        Private _postsPerPage As Integer
        Private ZforumID As Integer
        Private _threadID As Integer
        Private _postID As Integer
        Private _threadPage As Integer
        Private _imageURL As String
        Private _avatarURL As String
        Private ZforumConfig As ForumConfig
        Private _parent As ForumThreadInfo
        Private _forumPostCollection As ForumPostInfoCollection
		Private _forumSearchInfoCollection As ForumSearchInfoCollection = nothing

        Public Sub New(ByVal forum As Forum, ByVal ForumID As Integer, ByVal ThreadID As Integer, ByVal PostID As Integer, ByVal ThreadPage As Integer)

            MyBase.New(forum)
            ZforumID = ForumID
            _threadID = ThreadID
            _postID = PostID
            _threadPage = ThreadPage
            _flatView = forum.IsFlatView
			
			If not HttpContext.Current.Request.IsAuthenticated and not HttpContext.Current.Session("flatview") is nothing Then
			_flatView = HttpContext.Current.Session("flatview")
			end if
			if not HttpContext.Current.Session("searchresult") is nothing then
			_forumSearchInfoCollection = CType(HttpContext.Current.Session("searchresult") , ForumSearchInfoCollection)
			end if

            ZforumConfig = forum.Config
            _postsPerPage = ZforumConfig.PostsPerPage
			' PUT IN SKIN IMAGEFOLDER
            _imageURL = ForumConfig.SkinFolder()
            _avatarURL = ZforumConfig.AvatarFolder

        End Sub 'New

        Public Overrides Sub CreateChildControls()

        End Sub

        Public Overrides Sub OnPreRender()

            Dim dbForum As New ForumDB()

            If _threadID = 0 And _postID > 0 Then
                ' Calculate _threadID and from _postID and determine what page we are looking at
                _threadID = ForumDB.TTTForum_GetThreadFromPost(_postID)
                _threadPage = CInt(dbForum.TTTForum_GetSortOrderFromPost(_postID, _flatView) / _postsPerPage)
            End If

            ' If _postID not set, set it as post at the top of the current page
            If _postID = 0 And _threadID > 0 Then
                _postID = dbForum.TTTForum_GetPostFromThreadAndPage(_threadID, _threadPage, _postsPerPage, _flatView)

            End If

            ' If looking at the first post in a thread, then increment the thread view count
            If (_threadID = _postID) Then
                ForumDB.TTTForum_IncrementThreadViews(_threadID)
            End If

            _parent = ForumThreadInfo.GetThreadInfo(_threadID)

            ' Get page of posts that will be rendered
            _forumPostCollection = New ForumPostInfoCollection(_threadID, _threadPage, _postsPerPage, _flatView, _parent)
        End Sub


		
        Private Sub RenderPosts(ByVal wr As HtmlTextWriter)
            Dim _subject As String
            If _forumPostCollection.Count > 0 Then
                Dim firstForumPost As ForumPostInfo = CType(_forumPostCollection(0), ForumPostInfo)
                Dim textsubject As ForumText = New ForumText(firstForumPost.Subject, ZforumConfig)
                _subject = String.Format("&nbsp;" & GetLanguage("F_PostSubject") & " {0}", textsubject.ProcessSingleLine())
            Else
                _subject = GetLanguage("F_NoMessage")
            End If

            Dim lastVisited As DateTime
            Dim user As ForumUser = ForumUser.GetForumUser(LoggedOnUserID)
			If HttpContext.Current.Session("LastThreadView") is nothing then
			HttpContext.Current.Session("LastThreadView") = user.LastThreadView
			end if
			lastVisited = Convert.ToDateTime(HttpContext.Current.Session("LastThreadView"))

            Dim document As String = GetFullDocument()
			

			if not _forumSearchInfoCollection is nothing then
            ' Loop round posts found by search, rendering information each one
			wr.RenderBeginTag(HtmlTextWriterTag.Tr)
			wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTFooter")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "123")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
			wr.Write(GetLanguage("F_SearchResult"))
			wr.RenderEndTag() ' Td
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            Dim forumSearchInfo As forumSearchInfo

			wr.Write("<select onchange=""location = this.options[this.selectedIndex].value;"">")
            For Each forumSearchInfo In _forumSearchInfoCollection
                forumSearchInfo.PortalID = Forum.PortalID
                forumSearchInfo.ModuleID = Forum.ModuleID
                forumSearchInfo.RenderSearch(wr, lastVisited, Page, _imageURL, document, _PostID)
            Next
			wr.Write("</select>")
			wr.RenderEndTag() ' Td
			wr.RenderEndTag() ' Tr
			end if

			
			
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)

            ' Author
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "123")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Auteur") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Thread
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write(_subject)
			wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr

            ' Loop round rows in selected thread

            Dim _forumPostInfo As ForumPostInfo
            For Each _forumPostInfo In _forumPostCollection
                Dim selected As Boolean = (_postID = _forumPostInfo.PostID)
                Dim displayAction As Boolean = False

                If _flatView OrElse (Not _flatView AndAlso selected) Then
                    displayAction = True
                End If

                'select avatar for display based on user profile setting
                Dim avatar As String = ""

                If Len(_forumPostInfo.User.Avatar) > 0 Then
                    ' <tam:note with this release, avatar full url is saved in user database />
                    avatar = _forumPostInfo.User.Avatar
                End If

                _forumPostInfo.PortalID = Forum.PortalID
                _forumPostInfo.ModuleID = Forum.ModuleID
                _forumPostInfo.ForumID = ZforumID
                _forumPostInfo.Render(wr, displayAction, _flatView, selected, lastVisited, Page, LoggedOnUserID, avatar, _imageURL, _avatarURL, document)
            Next


        End Sub

        Private Sub RenderThreadPaging(ByVal wr As HtmlTextWriter, ByVal PageCount As Integer) ' Start the new column
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "right")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "50%")
            ' wr.AddAttribute(HtmlTextWriterAttribute.Class, "CommandButton")
            wr.RenderBeginTag(HtmlTextWriterTag.Td) ' TD

            ' First, previous, next, last thread hyperlinks
            Dim backwards As Boolean
            Dim forwards As Boolean

            If _threadPage <> 0 Then
                backwards = True
            End If

            If _threadPage <> PageCount - 1 Then
                forwards = True
            End If

            If (backwards) Then
                ' < Previous
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("threadid={0}&threadpage={1}", _threadID, _threadPage), "postid=&action="))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.write("&laquo;")
                wr.Write(GetLanguage("F_Prev"))
                wr.RenderEndTag() ' A
                wr.Write("&nbsp;|&nbsp;")
            End If

            Dim iThread As Integer
            For iThread = 1 To PageCount
                If iThread <> _threadPage + 1 Then
                    wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("threadpage={0}", iThread), "postid=&action="))
                else
				wr.AddStyleAttribute(HtmlTextWriterStyle.Color, "red")
				End If
	
				
				
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.Write(iThread)
                wr.RenderEndTag() ' A
                If iThread < PageCount Then
                    wr.Write("&nbsp;|&nbsp;") ' Divider
                Else
                    wr.Write("&nbsp;")
                End If
            Next

            If (forwards) Then
                ' Next >
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("threadid={0}&threadpage={1}", _threadID, _threadPage + 2), "postid=&action="))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.Write(GetLanguage("F_Next"))
                wr.write("&raquo;")
                wr.RenderEndTag() ' A
                wr.Write("&nbsp;")
            End If

            ' Close column
            wr.Write("&nbsp;")
            wr.RenderEndTag() ' Td

        End Sub

        Private Sub RenderFooter(ByVal wr As HtmlTextWriter)
            ' Start the footer row
            ' wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "2")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTFooter")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' Into which we will put a table that will contain
            wr.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
            wr.RenderBeginTag(HtmlTextWriterTag.Table)

            ' On the left hand side: Pages x of y
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "50%")
			wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTFooterText")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' Page x of y text
            Dim pageCount As Integer = CInt(Math.Floor((ForumDB.TTTForum_GetThreadRepliesCount(_threadID)) / _postsPerPage)) + 1
            wr.RenderBeginTag(HtmlTextWriterTag.B)


			Dim TempString As String
			TempString = Replace(GetLanguage("F_Paging"), "{pagenum}", (_threadsPage + 1).ToString)
			TempString = Replace(TempString, "{numpage}", pageCount.ToString)
			wr.Write(TempString)
            wr.RenderEndTag() ' B
            wr.RenderEndTag() ' Td

            ' And on the right hand side, render thread navigation (<< < Previous | Next > >>)
            If (pageCount > 1) Then
                RenderThreadPaging(wr, pageCount)
            End If

            ' Close out this table and row
            wr.RenderEndTag() ' Tr
            wr.RenderEndTag() ' Table
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr
        End Sub

        Public Overrides Sub Render(ByVal wr As HtmlTextWriter)

            RenderTableBegin(wr, 1, 0)
            RenderPosts(wr)
            RenderFooter(wr)
            RenderTableEnd(wr)

        End Sub

    End Class 'ForumThread

#End Region

#Region "ForumPostInfo"

    Public Class ForumPostInfo

        Private Const PostInfoCacheKeyPrefix As String = "PostInfo"
        Private Const _profilePage As Integer = TTT_ForumDispatch.ForumDesktopType.ForumProfile

        Private _portalID As Integer
        Private ZmoduleID As Integer
        Private ZforumID As Integer
        Private _notify As Boolean
        Private _postDate As DateTime
        Private _flatSortOrder As Integer
        Private _parentPostID As Integer
        Private _postID As Integer
        Private _postLevel As Integer
        Private _threadID As Integer
        Private _treeSortOrder As Integer
        Private _body As String
        Private _remoteAddr As String
        Private _subject As String
        Private _lastModifiedDate As DateTime
        Private _lastModifiedAuthorID As Integer
        Private _lastModifiedAuthor As String
        Private Zuser As ForumUser
        Private _parent As ForumThreadInfo
        Private Zconfig As ForumConfig

        Public Sub New()

        End Sub 'New

        Private Sub New(ByVal PostID As Integer)
            Dim dbForum As New ForumDB()
            Dim dr As SqlDataReader = dbForum.TTTForum_GetSinglePost(PostID)

            If dr.Read Then
                PopulateInfo(dr)
            End If
            dr.Close()

        End Sub

        Private Sub New(ByVal DataReader As SqlDataReader)

            PopulateInfo(DataReader)

        End Sub

        Public Shared Function PopulatePostInfo(ByVal DataReader As SqlDataReader) As ForumPostInfo

            Dim postInfo As ForumPostInfo = New ForumPostInfo(DataReader)
            Return postInfo

        End Function

        Private Sub PopulateInfo(ByVal dr As SqlDataReader)

            ' Post specifics
            _postID = ConvertInteger(dr("PostID"))
            _body = ConvertString(dr("Body"))
            _flatSortOrder = ConvertInteger(dr("FlatSortOrder"))
            _notify = ConvertBoolean(dr("Notify"))
            _parentPostID = ConvertInteger(dr("ParentPostID"))
            _postDate = ConvertDateTime(dr("PostDate"))
			' A modifier avec la date de l utilisateur user.TimeZone
            _postLevel = ConvertInteger(dr("PostLevel"))
            _remoteAddr = ConvertString(dr("RemoteAddr"))
            _subject = ConvertString(dr("Subject"))
            _threadID = ConvertInteger(dr("ThreadID"))
            _treeSortOrder = ConvertInteger(dr("TreeSortOrder"))
            _flatSortOrder = ConvertInteger(dr("FlatSortOrder"))
            _lastModifiedDate = ConvertDateTime(dr("LastModifiedDate"))
			' A modifier avec la date de l utilisateur user.TimeZone
            _lastModifiedAuthorID = ConvertInteger(dr("LastModifiedAuthorID"))
            _lastModifiedAuthor = ConvertString(dr("LastModifiedAuthor"))

            Zuser = ForumUser.GetForumUser(ConvertInteger(dr("UserID")))
            _parent = ForumThreadInfo.GetThreadInfo(_threadID)
            Zconfig = ForumConfig.GetForumConfig(ZmoduleID)

        End Sub 'PopulateInfo

        Public Shared Function GetPostInfo(ByVal PostID As Integer) As ForumPostInfo

            ' Grab reference to the applicationstate object
			' Need to change Forum Cashing
            
			Dim TempKey as String = GetDBName & PostInfoCacheKeyPrefix & CStr(PostID)
			Dim context As HttpContext = HttpContext.Current
			Dim postInfo As ForumPostInfo 
            If Context.Cache(TempKey) Is Nothing Then
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' If this object has not been instantiated yet, we need to grab it
            postInfo = New ForumPostInfo(PostID)
			Context.Cache.Insert(TempKey, postInfo, DotNetZoom.CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, postInfo.ModuleID), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
			else
			postInfo  = CType(Context.Cache(TempKey), ForumPostInfo)
            End If
            Return postInfo

        End Function

        Public Shared Sub ResetPostInfo(ByVal PostID As Integer)
			Dim TempKey as String = GetDBName & PostInfoCacheKeyPrefix & CStr(PostID)
			Dim context As HttpContext = HttpContext.Current
			context.Cache.Remove(TempKey)
        End Sub

        Private Sub RenderLevelIndentCell(ByVal wr As HtmlTextWriter)
            Dim width As Integer = PostLevel * 16
            If width > 0 Then
                wr.AddAttribute(HtmlTextWriterAttribute.Width, width.ToString())
                wr.RenderBeginTag(HtmlTextWriterTag.Td)
                wr.RenderEndTag()
            End If
        End Sub 'RenderLevelIndentCell

#Region "ForumPostInfo-Render"

		Private iColor As integer = 0
		Private Function ProcessSearch( ByVal ObjectSearch As String, Byval ToTransform As String) As String
		' searchterms=
		' useralias=
		' searchobject=
		If not objectSearch is nothing and not objectSearch is string.empty then
                Dim Word As String
                Dim HTMLWord As String
                Dim stringpattern As String
                Dim strColor As String = ""
                If iColor > 5 Then
                    iColor = 0
                End If
                For Each Word In ObjectSearch.Split(":"c)
                    If Not Word Is String.Empty And Not Word Is Nothing Then
                        stringpattern = "(" + Word + ")(?=[^>]*<)"
                        iColor += 1
                        Select Case iColor
                            Case 1
                                strColor = "#FF0000"
                            Case 2
                                strColor = "#00FF7F"
                            Case 3
                                strColor = "#FF6347"
                            Case 4
                                strColor = "#FFFF00"
                            Case Else
                                strColor = "#9ACD32"
                        End Select
                        ToTransform = Regex.Replace(ToTransform, stringpattern, "<b style=""background: " + strColor + "; color: black"">" & Word & "</b>", RegexOptions.IgnoreCase)
                        HTMLWord = HtmlEncode(Word)
                        If HTMLWord <> Word Then
                            stringpattern = "(" + HTMLWord + ")(?=[^>]*<)"
                            ToTransform = Regex.Replace(ToTransform, stringpattern, "<b style=""background: " + strColor + "; color: black"">" & Word & "</b>", RegexOptions.IgnoreCase)
                        End If
                    End If
                Next
            End If

		Return Regex.Replace(ToTransform, "<tag>", "", RegexOptions.IgnoreCase)
		
		end Function

        Public Sub Render(ByVal wr As HtmlTextWriter, ByVal displayActions As Boolean, ByVal flatView As Boolean, ByVal selected As Boolean, ByVal lastVisited As DateTime, ByVal page As Page, ByVal loggedOnUserID As Integer, ByVal avatar As String, ByVal imageURL As String, ByVal avatarURL As String, ByVal document As String)

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' New row
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)

            ' Left hand side contains user information (user alias, avatar, number of posts etc)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")

            If Not flatView And Not selected Then
                wr.AddAttribute(HtmlTextWriterAttribute.align, "center")
            Else
                wr.AddAttribute(HtmlTextWriterAttribute.Valign, "top")
            End If

            wr.AddAttribute(HtmlTextWriterAttribute.Width, "123")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' We will put this user information in its own table in the first column
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "3")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "123")
            wr.RenderBeginTag(HtmlTextWriterTag.Table)

            ' User alias and number of posts
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

			
		' link to user Mail
		If Zuser.EnablePrivateMessages then
                If (loggedOnUserID <> Zuser.UserID) And (loggedOnUserID > 0) Then
                    wr.AddAttribute(HtmlTextWriterAttribute.Href, FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "forumpage=4&pmstabid=3&userid=" & Zuser.UserID))
                    wr.RenderBeginTag(HtmlTextWriterTag.A)

                    wr.AddAttribute(HtmlTextWriterAttribute.Width, "16")
                    wr.AddAttribute(HtmlTextWriterAttribute.Height, "16")
                    wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: 0px -224px;")
                    wr.AddAttribute(HtmlTextWriterAttribute.Src, "images/1x1.gif")
                    wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_SendPMSTo") & " " & User.Alias)
                    wr.AddAttribute(HtmlTextWriterAttribute.Alt, "*")
                    wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                    wr.RenderBeginTag(HtmlTextWriterTag.Img)
                    wr.RenderEndTag() ' img
                    wr.RenderEndTag() ' A
                    wr.Write("&nbsp;")
                End If
		end if

			
			
            'link to user profile
            wr.RenderBeginTag(HtmlTextWriterTag.B)
            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("forumpage={0}&userid={1}&tabid={2}", _profilePage.ToString, Zuser.UserID.ToString, _portalSettings.ActiveTab.TabId), "scope=&threadid=&searchpage=&threadpage=&threadspage="))
            wr.AddAttribute(HtmlTextWriterAttribute.title, GetLanguage("F_SeeProfileof") & " " & User.Alias)
            wr.RenderBeginTag(HtmlTextWriterTag.A)
            

				If Not HttpContext.Current.Request.Params("useralias") is Nothing then
				' Put color on if search userAlias
				' useralias=
				' searchobject=
				wr.Write(ProcessSearch(HttpContext.Current.Request.Params("useralias"), "<tag>" & User.Alias & "<tag>"))
				else
				wr.Write(User.Alias)
				end if

			
			
            wr.RenderEndTag() ' A
            wr.RenderEndTag() ' B
            wr.Write("<br>")
            ' link to user webpage
            If Len(Zuser.URL) > 0 Then
                wr.AddAttribute(HtmlTextWriterAttribute.Href, Zuser.URL)
                wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_VisitWWWof") & " " & User.Alias)
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.Write(Replace(Zuser.URL, "http://", ""))
                wr.RenderEndTag() ' A
                wr.Write("<br>")
            End If

			
			
			
            ' Avatar adn user post count is always displayed in flat view mode,
            ' but only displayed in tree view mode for the selected post

            If flatView OrElse (Not flatView And selected) Then
                ' put ladder here

                wr.AddAttribute(HtmlTextWriterAttribute.Src, "images/1x1.gif")
                wr.AddAttribute(HtmlTextWriterAttribute.Height, "12")
                wr.AddAttribute(HtmlTextWriterAttribute.Width, "64")



				Select Case User.PostCount
				Case 0 To 10
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "background: url('/images/ttt/stars.gif') no-repeat; background-position: 0px 0px;")
				Case 11 to 15
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "background: url('/images/ttt/stars.gif') no-repeat; background-position: 0px -12px;")
				Case 16 to 20
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "background: url('/images/ttt/stars.gif') no-repeat; background-position: 0px -24px;")
				Case 21 to 25
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "background: url('/images/ttt/stars.gif') no-repeat; background-position: 0px -36px;")
				Case 26 to 30
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "background: url('/images/ttt/stars.gif') no-repeat; background-position: 0px -48px;")
				Case 31 to 35
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "background: url('/images/ttt/stars.gif') no-repeat; background-position: 0px -60px;")
				Case 36 to 40
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "background: url('/images/ttt/stars.gif') no-repeat; background-position: 0px -72px;")
				Case 41 to 45
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "background: url('/images/ttt/stars.gif') no-repeat; background-position: 0px -84px;")
				Case else
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "background: url('/images/ttt/stars.gif') no-repeat; background-position: 0px -96px;")
				End Select
				Dim TempString As String
				TempString = replace(GetLanguage("F_Contributed"), "{username}", user.Alias) 
				TempString = replace(TempString, "{numpost}", user.PostCount.ToString) 
				
                Dim LoggonedUserID As Integer
                If Page.Request.IsAuthenticated Then
                    LoggonedUserID = ConvertInteger(Page.User.Identity.Name)
					Dim Currentuser As ForumUser = ForumUser.GetForumUser(LoggonedUserID)
					Dim TpDateTime as DateTime
					TpDateTime = user.LastActivity.AddMinutes(GetTimeDiff(Currentuser.TimeZone))
					TempString = replace(TempString, "{datelast}", TpDateTime.ToString()) 
                Else
                    LoggonedUserID = -1
					TempString = replace(TempString, "{datelast}", user.LastActivity.ToLongDateString) 
	            End If

				
				wr.AddAttribute(HtmlTextWriterAttribute.Title, TempString )
				wr.AddAttribute(HtmlTextWriterAttribute.Alt, User.PostCount.ToString)
                wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                wr.RenderBeginTag(HtmlTextWriterTag.Img)
                wr.RenderEndTag() ' img
                wr.Write("<br>")
				
            End If

            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr

            If flatView OrElse (Not flatView And selected) Then

                If Not avatar = String.Empty Then
                    wr.RenderBeginTag(HtmlTextWriterTag.Tr)
                    wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
                    wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
                    wr.RenderBeginTag(HtmlTextWriterTag.Td)
                    wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                    wr.AddAttribute(HtmlTextWriterAttribute.Src, avatar)
                    wr.AddAttribute(HtmlTextWriterAttribute.Alt, User.Alias)
                    wr.RenderBeginTag(HtmlTextWriterTag.Img)
                    wr.RenderEndTag() ' Img
                    wr.RenderEndTag() ' Td
                    wr.RenderEndTag() ' Tr
                End If
            End If

            ' End user information table
            wr.RenderEndTag() ' Table
            wr.RenderEndTag() ' Td

            ' Start row which will display subject, body and actions (reply, edit, etc)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.Valign, "top")
            
            If Not flatView And Not selected Then
                wr.AddAttribute(HtmlTextWriterAttribute.Height, "100%")
            End If
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' Start a new table for this information
            wr.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "3")
            wr.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")

            If Not flatView And Not selected Then
                wr.AddAttribute(HtmlTextWriterAttribute.Height, "100%")
            End If
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
            wr.RenderBeginTag(HtmlTextWriterTag.Table)

            ' Highlighted row (subject and when posted information)
            'wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)

            If Not flatView Then
                RenderLevelIndentCell(wr)
            End If

            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighLight")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.RenderBeginTag(HtmlTextWriterTag.B)
            ' wr.AddAttribute(HtmlTextWriterAttribute.Name, PostID.ToString())
            'wr.RenderBeginTag(HtmlTextWriterTag.A)
            'wr.RenderEndTag() ' A

            If Not flatView And Not selected Then
                ' Provide link to select a different post when in tree view mode
                Dim linkURL As String = TTTUtils.GetURL(document, page, String.Format("postid={0}&tabid={1}", PostID.ToString, _portalSettings.ActiveTab.TabId), "action=&threadpage=&searchpage=&threadspage=")
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("postid={0}", PostID.ToString), "action=&threadpage=&searchpage=&threadspage=" + "#" + PostID.ToString))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
            End If

            Dim subjectForumText As ForumText = New ForumText(Subject, Zconfig)
            'wr.Write(subjectForumText.ProcessSingleLine(imageURL))
            wr.Write(subjectForumText.ProcessSingleLine())

            If Not flatView And Not selected Then
                wr.RenderEndTag() ' A
            End If
            wr.RenderEndTag() ' B

            ' Display new image if this post is new since last time user visited
            If loggedOnUserID > 0 AndAlso (lastVisited < PostDate) Then
                wr.AddAttribute(HtmlTextWriterAttribute.Src, imageURL + GetLanguage("N") + "_new.gif")
				wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_NewPost"))
				wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_NewPostA"))
                wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                wr.RenderBeginTag(HtmlTextWriterTag.Img)
                wr.RenderEndTag() ' img
            End If

            wr.Write("<br>")
			
			
			' ViewerId DateTime to see the date time in local time for registered user
			

			Dim TempDateTime as DateTime
            If loggedOnUserID > 0 Then
				Dim Currentuser As ForumUser = ForumUser.GetForumUser(loggedOnUserID)
				TempDateTime = PostDate.AddMinutes(GetTimeDiff(Currentuser.TimeZone))
                Else
				TempDateTime = PostDate.AddMinutes(GetTimeDiff(-99))
                End If

            wr.Write(String.Format(GetLanguage("F_Posted") & " {0} {1}", TempDateTime.ToString("dd MMM yy"), TempDateTime.ToString("t")))
			
            ' Display edited tag if post has been modified
            If Len(_lastModifiedAuthor) > 0 Then
                wr.Write("<br>")
                wr.AddAttribute(HtmlTextWriterAttribute.Src, "images/1x1.gif")
                wr.AddAttribute(HtmlTextWriterAttribute.Width, "16")
                wr.AddAttribute(HtmlTextWriterAttribute.Height, "16")
                wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: 0px -128px;")
                wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_PModified"))
				wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_PModifiedA"))
                wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                wr.RenderBeginTag(HtmlTextWriterTag.Img)
                wr.RenderEndTag() 'Img
                wr.RenderBeginTag(HtmlTextWriterTag.B)
                wr.Write("&nbsp;")
			' ViewerId DateTime to see the date time in local time for registered user
			

			
                If loggedOnUserID > 0 Then
                    Dim Currentuser As ForumUser = ForumUser.GetForumUser(loggedOnUserID)
                    TempDateTime = _lastModifiedDate.AddMinutes(GetTimeDiff(Currentuser.TimeZone))
                Else
                    TempDateTime = _lastModifiedDate.AddMinutes(GetTimeDiff(-99))
                End If
                Dim TempString1 As String = Replace(GetLanguage("F_LastModified"), "{author}", _lastModifiedAuthor)
			    TempString1 = Replace(TempString1, "{datetime}", TempDateTime.ToString)
                wr.Write(TempString1)
                wr.RenderEndTag() ' B
            End If

            
            wr.RenderEndTag() ' Td

            ' Reply, Quote and edit row
     
            If displayActions AndAlso PortalSecurity.IsInRoles(CType(portalSettings.GetEditModuleSettings(ModuleId), ModuleSettings).AuthorizedEditRoles.ToString) = True Then
                'add centering to reply quote edit
                wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighLight")
                wr.AddAttribute(HtmlTextWriterAttribute.Align, "Right")
                wr.RenderBeginTag(HtmlTextWriterTag.Td)
				wr.RenderBeginTag(HtmlTextWriterTag.ul)
				
				
                ' Delete link (logged user is admin)
				If (User.UserID = loggedOnUserID) _
                OrElse (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) Then
                    wr.AddAttribute(HtmlTextWriterAttribute.style, "display:inline")
					wr.AddAttribute(HtmlTextWriterAttribute.Class, "button")
					wr.RenderBeginTag(HtmlTextWriterTag.li)
                    wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("edit=control&postid={0}&forumid={1}&mid={2}&tabid={3}&action=delete", _postID.ToString, ZforumID.ToString, ZmoduleID.ToString, _portalSettings.ActiveTab.TabId), "searchpage=&threadspage=&searchterms=&searchobject="))
                    ' http://www.dotnetzoom.com/fr.forum.aspx?forumid=0&searchterms=go&action=delete&scope=post&edit=control&threadid=191&tabid=106&postid=192&mid=456&searchobject=go
 					wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_ErasePost"))
					wr.AddAttribute(HtmlTextWriterAttribute.onClick, "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm_erase_message")) & "');")
                    wr.RenderBeginTag(HtmlTextWriterTag.A)
					wr.Write(GetLanguage("erase"))
                    wr.RenderEndTag() ' A
					wr.RenderEndTag() ' li
                  
                End If

                ' Edit link (logged on user is author or admin)
                If (User.UserID = loggedOnUserID) _
                OrElse (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) Then
                    wr.AddAttribute(HtmlTextWriterAttribute.style, "display:inline")
					wr.AddAttribute(HtmlTextWriterAttribute.Class, "button")
					wr.RenderBeginTag(HtmlTextWriterTag.li)
                    wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("edit=control&postid={0}&forumid={1}&mid={2}&tabid={3}&action=edit", _postID.ToString, ZforumID.ToString, ZmoduleID.ToString, _portalSettings.ActiveTab.TabId.ToString), "searchpage=&threadspage=&searchterms=&searchobject="))
                    wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("FZEditPost"))
					wr.RenderBeginTag(HtmlTextWriterTag.A)
					wr.Write( GetLanguage("modifier"))
                    wr.RenderEndTag() ' A
					wr.RenderEndTag() ' li
                End If

                ' <tam:note check security here>

                ' Quote link
                wr.AddAttribute(HtmlTextWriterAttribute.style, "display:inline")
				wr.AddAttribute(HtmlTextWriterAttribute.Class, "button")
				wr.RenderBeginTag(HtmlTextWriterTag.li)
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("edit=control&postid={0}&forumid={1}&mid={2}&tabid={3}&action=quote", _postID.ToString, ZforumID.ToString, ZmoduleID.ToString, _portalSettings.ActiveTab.TabId), "searchpage=&threadspage=&searchterms=&searchobject="))
                wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_QuotePost"))
				wr.RenderBeginTag(HtmlTextWriterTag.A)
				wr.Write(GetLanguage("F_QuotePostA"))
                wr.RenderEndTag() ' A
				wr.RenderEndTag() ' li
                

                ' Reply link
                wr.AddAttribute(HtmlTextWriterAttribute.style, "display:inline")
				wr.AddAttribute(HtmlTextWriterAttribute.Class, "button")
				wr.RenderBeginTag(HtmlTextWriterTag.li)
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("edit=control&postid={0}&forumid={1}&mid={2}&tabid={3}&action=reply", _postID.ToString, ZforumID.ToString, ZmoduleID.ToString, _portalSettings.ActiveTab.TabId), "searchpage=&threadspage=&searchterms=&searchobject="))
                wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_PostReply"))
				wr.RenderBeginTag(HtmlTextWriterTag.A)
				
				wr.Write( GetLanguage("F_PostReplyA"))
                wr.RenderEndTag() ' A
				wr.RenderEndTag() ' li
			
				wr.RenderEndTag() ' ul
                wr.RenderEndTag() ' Td
            End If
            wr.RenderEndTag()   ' Tr

            ' Message row
            'If Not flatView And Not selected Then
            If flatView OrElse (Not flatView And selected) Then
                wr.AddAttribute(HtmlTextWriterAttribute.Class, "Normal")
                wr.RenderBeginTag(HtmlTextWriterTag.Tr)
                If Not flatView Then
                    RenderLevelIndentCell(wr)
                End If
				wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "2")
				wr.AddAttribute(HtmlTextWriterAttribute.Class, "forumtable")
				wr.RenderBeginTag(HtmlTextWriterTag.Td)
			

                'Dim bodyForumText As ForumText = New ForumText(Body)
                Dim bodyForumText As ForumText = New ForumText(HtmlDecode(Body), Zconfig)
                
                wr.Write(bodyForumText.ProcessHtml())

              
                wr.RenderEndTag() ' Td
                wr.RenderEndTag() ' Tr

            End If

            ' Close out table and this row
            wr.RenderEndTag() ' Table
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr
        End Sub 'Render

#End Region

#Region "ForumPostInfo-Public Properties"

        Public Property PortalID() As Integer
            Get
                Return _portalID
            End Get
            Set(ByVal Value As Integer)
                _portalID = Value
            End Set
        End Property

        Public Property ModuleID() As Integer
            Get
                Return ZmoduleID
            End Get
            Set(ByVal Value As Integer)
                ZmoduleID = Value
            End Set
        End Property

        Public Property ForumID() As Integer
            Get
                Return ZforumID
            End Get
            Set(ByVal Value As Integer)
                ZforumID = Value
            End Set
        End Property

        Public Property Notify() As Boolean
            Get
                Return _notify
            End Get
            Set(ByVal Value As Boolean)
                _notify = Value
            End Set
        End Property


        Public Property PostDate() As DateTime
            Get
                Return _postDate
            End Get
            Set(ByVal Value As DateTime)
                _postDate = Value
            End Set
        End Property


        Public Property FlatSortOrder() As Integer
            Get
                Return _flatSortOrder
            End Get
            Set(ByVal Value As Integer)
                _flatSortOrder = Value
            End Set
        End Property


        Public Property ParentPostID() As Integer
            Get
                Return _parentPostID
            End Get
            Set(ByVal Value As Integer)
                _parentPostID = Value
            End Set
        End Property


        Public Property PostID() As Integer
            Get
                Return _postID
            End Get
            Set(ByVal Value As Integer)
                _postID = Value
            End Set
        End Property


        Public Property PostLevel() As Integer
            Get
                Return _postLevel
            End Get
            Set(ByVal Value As Integer)
                _postLevel = Value
            End Set
        End Property


        Public Property ThreadID() As Integer
            Get
                Return _threadID
            End Get
            Set(ByVal Value As Integer)
                _threadID = Value
            End Set
        End Property


        Public Property TreeSortOrder() As Integer
            Get
                Return _treeSortOrder
            End Get
            Set(ByVal Value As Integer)
                _treeSortOrder = Value
            End Set
        End Property


        Public Property User() As ForumUser
            Get
                Return Zuser
            End Get
            Set(ByVal Value As ForumUser)
                Zuser = Value
            End Set
        End Property

        Public Property Parent() As ForumThreadInfo
            Get
                Return _parent
            End Get
            Set(ByVal Value As ForumThreadInfo)
                _parent = Value
            End Set
        End Property

        Public Property Body() As String
            Get
				If Not HttpContext.Current.Request.Params("searchterms") is Nothing then
					' searchterms=
					' useralias=
					' searchobject=
					return ProcessSearch(HttpContext.Current.Request.Params("searchterms"), "<tag>" & _body & "<tag>")
				end if
                Return _body
            End Get
            Set(ByVal Value As String)
                _body = Value
            End Set
        End Property


        Public Property RemoteAddr() As String
            Get
                Return _remoteAddr
            End Get
            Set(ByVal Value As String)
                _remoteAddr = Value
            End Set
        End Property

        Public Property Subject() As String
            Get
				If Not HttpContext.Current.Request.Params("searchobject") is Nothing then
					' searchterms=
					' useralias=
					' searchobject=
					return ProcessSearch(HttpContext.Current.Request.Params("searchobject"), "<tag>" & _subject & "<tag>")
				end if
                Return _subject
            End Get
            Set(ByVal Value As String)
                _subject = Value
            End Set
        End Property

        Public Property LastModifiedDate() As DateTime
            Get
                Return _lastModifiedDate
            End Get
            Set(ByVal Value As DateTime)
                _lastModifiedDate = Value
            End Set
        End Property

        Public Property LastModifiedAuthorID() As Integer
            Get
                Return _lastModifiedAuthorID
            End Get
            Set(ByVal Value As Integer)
                _lastModifiedAuthorID = Value
            End Set
        End Property

        Public Property LastModifiedAuthor() As String
            Get
				If Not HttpContext.Current.Request.Params("useralias") is Nothing then
					' searchterms=
					' useralias=
					' searchobject=
					return ProcessSearch(HttpContext.Current.Request.Params("useralias"), "<tag>" & _lastModifiedAuthor & "<tag>")
				end if
                Return _lastModifiedAuthor
            End Get
            Set(ByVal Value As String)
                _lastModifiedAuthor = Value
            End Set
        End Property


#End Region

    End Class 'ForumPostInfo

#End Region

#Region "ForumPostInfoCollection"

    Public Class ForumPostInfoCollection
        Inherits ArrayList

        Private _threadID As Integer
        Private _threadPage As Integer
        Private _postsPerPage As Integer
        Private _flatView As Boolean
        Private _parent As ForumThreadInfo

        Public Sub New()

        End Sub

        Public Sub New(ByVal ThreadID As Integer, ByVal ThreadPage As Integer, ByVal PostsPerPage As Integer, ByVal FlatView As Boolean, ByVal Parent As ForumThreadInfo)

            _threadID = ThreadID
            _threadPage = ThreadPage
            _postsPerPage = PostsPerPage
            _flatView = FlatView
            _parent = Parent

            PopulateCollection()
            'Return Me
        End Sub 'New

        Private Sub PopulateCollection() 'PostInfoCollection
            Dim dbForum As New ForumDB()
            Dim strFilter As String = "" 'this is reserve for filter in next version
            Dim dr As SqlDataReader = dbForum.TTTForum_GetPosts(_threadID, _threadPage, _postsPerPage, _flatView, strFilter)
            While dr.Read
                Dim postInfo As ForumPostInfo = ForumPostInfo.PopulatePostInfo(dr)
                postInfo.Parent = _parent
                Me.Add(postInfo)
            End While
        End Sub

        Public ReadOnly Property ThreadID() As Integer
            Get
                Return _threadID
            End Get
        End Property

        Public ReadOnly Property ThreadPage() As Integer
            Get
                Return _threadPage
            End Get
        End Property

        Public ReadOnly Property PostsPerPage() As Integer
            Get
                Return _postsPerPage
            End Get
        End Property

        Public ReadOnly Property FlatView() As Boolean
            Get
                Return _flatView
            End Get
        End Property

    End Class

#End Region

End Namespace 'TTTForum