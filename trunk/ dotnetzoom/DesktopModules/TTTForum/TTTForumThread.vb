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

Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DotNetZoom.TTTUtils
Imports System.Web.Caching



Namespace DotNetZoom

#Region "ForumThreads"

    Public Class ForumThreads
        Inherits ForumObject

        Private _forumThreadInfoCollection As ForumThreadInfoCollection
        Private ZforumID As Integer
        Private _threadCount As Integer
        Private _threadsPage As Integer
        Private _threadsPerPage As Integer
        Private _postsPerPage As Integer
        Private ZforumConfig As ForumConfig
        Private _imageURL As String = ""
        Private _galleryURL As String = ""

        Public Sub New(ByVal forum As Forum, ByVal ForumID As Integer, ByVal threadsPage As Integer)
            MyBase.New(forum)

            'ZforumConfig = ForumConfig.GetForumConfig(forum.ModuleID)
            ZforumConfig = forum.Config
            ZforumID = ForumID
            _threadsPage = threadsPage
            _threadsPerPage = ZforumConfig.ThreadsPerPage
            _postsPerPage = ZforumConfig.PostsPerPage
			' PUT IN SKIN IMAGEFOLDER
            _imageURL = ForumConfig.SkinFolder()

        End Sub 'New

        Public Overrides Sub CreateChildControls()

        End Sub 'CreateChildControls

        Public Overrides Sub OnPreRender()

            Dim dbForum As New ForumDB()
            ' Get threads that will be rendered and the total number of forum threads
            _forumThreadInfoCollection = New ForumThreadInfoCollection(ZforumID, _threadsPerPage, _threadsPage)
            _threadCount = ForumDB.TTTForum_GetThreadCount(ZforumID)

        End Sub 'OnPreRender

        Private Sub RenderThreads(ByVal wr As HtmlTextWriter)

            ' Render header row

           ' wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "2")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_sThread") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Started by column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_SBy") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Replies column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Answer") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Views column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Seen") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Last Post column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_LPost") & "&nbsp;")
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr

            ' Work out when to display new images 
            Dim lastVisited As DateTime
            Dim user As ForumUser = ForumUser.GetForumUser(LoggedOnUserID)
            If HttpContext.Current.Session("LastThreadView") Is Nothing Then
                HttpContext.Current.Session("LastThreadView") = user.LastThreadView
            End If
            lastVisited = Convert.ToDateTime(HttpContext.Current.Session("LastThreadView"))


            ' Loop round thread items, rendering information about individual threads
            Dim document As String = GetFullDocument()

            'Dim dbForum As New ForumDB()
            Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)

            'forumInfo.
            If forumInfo.IsIntegrated Then
                Dim galleryID As Integer = forumInfo.IntegratedGallery
                Dim album As String = forumInfo.IntegratedAlbumName
                Dim ZgalleryConfig As galleryConfig = galleryConfig.GetGalleryConfig(Forum.ModuleID)
                Dim _URL As String = ZgalleryConfig.ThumbFolder
                _galleryURL = BuildPath(New String(2) {ZgalleryConfig.RootURL, album, ZgalleryConfig.ThumbFolder}, "/", False, False)
            Else

                If forumInfo.IsActive Then
                    If forumInfo.IsPrivate Then
                        If Not PortalSecurity.IsInRoles(forumInfo.AuthorizedRoles) = True Then
                            HttpContext.Current.Response.Redirect(GetFullDocument() & "?edit=control&def=Access Denied", True)
                        End If
                    End If
                Else
                    HttpContext.Current.Response.Redirect(GetFullDocument(), True)
                End If


            End If

            Dim forumThreadInfo As forumThreadInfo

            For Each forumThreadInfo In _forumThreadInfoCollection
                Dim _isAuthor As Boolean = False
                If forumThreadInfo.StartedByUserID = LoggedOnUserID Then
                    _isAuthor = True
                End If
                forumThreadInfo.Render(wr, _isAuthor, _postsPerPage, lastVisited, Page, _imageURL, LoggedOnUserID, document, _galleryURL)
            Next
            'record log user last view
            If Page.Request.IsAuthenticated = True Then
                ForumUserDB.TTTForum_UpdateUserThreadView(LoggedOnUserID)
            End If

        End Sub 'RenderThreads

        Private Sub RenderThreadsPaging(ByVal wr As HtmlTextWriter) ' Start the new column
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "right")
            wr.AddAttribute(HtmlTextWriterAttribute.Valign, "bottom")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "50%")
            'wr.AddAttribute(HtmlTextWriterAttribute.Class, "CommandButton")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' First, previous, next, last thread hyperlinks
            Dim pageCount As Integer = CInt(Math.Floor((_threadCount - 1) / _threadsPerPage)) + 1
            Dim backwards As Boolean = False
            If _threadsPage <> 0 Then backwards = True

            Dim forwards As Boolean = False
            If _threadsPage < pageCount - 1 Then forwards = True

            ' << First and < Previous links
            If (backwards) Then
                ' < Previous
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("threadspage={0}", _threadsPage), "postid=&action="))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.Write("&laquo;")
                wr.Write(GetLanguage("F_Prev"))
                wr.RenderEndTag() ' A
                wr.Write("&nbsp;|&nbsp;")
            End If

            Dim iPage As Integer
            For iPage = 1 To pageCount
                If iPage <> _threadsPage + 1 Then
                    wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("threadspage={0}", iPage), "postid=&action="))
                Else
                    wr.AddStyleAttribute(HtmlTextWriterStyle.Color, "red")
                End If
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.Write(iPage)
                wr.RenderEndTag() ' A

                If iPage < pageCount Then
                    wr.Write("&nbsp;|&nbsp;") ' Divider
                Else
                    wr.Write("&nbsp;")
                End If
            Next

            ' Next > and Last >> links
            If (forwards) Then
                ' Next >
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("threadspage={0}", _threadsPage + 2), "postid=&action="))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.Write(GetLanguage("F_Next"))
                wr.write("&raquo;")
                wr.RenderEndTag() ' A
                wr.Write("&nbsp;")
            End If

            ' Close column
            wr.RenderEndTag() ' Td

        End Sub 'RenderThreadsPaging

        Private Sub RenderFooter(ByVal wr As HtmlTextWriter)

            ' Start the footer row
            ' wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "6")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTFooter")
            wr.RenderBeginTag(HtmlTextWriterTag.Td) ' Into which we will put a table that will contain

            wr.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
            wr.RenderBeginTag(HtmlTextWriterTag.Table)

            ' On the left hand side: Page x of y
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "10%")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTFooterText")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' Page x of y text

            wr.RenderBeginTag(HtmlTextWriterTag.B)

            Dim pageCount As Integer = CInt(Math.Floor((_threadCount - 1) / _threadsPerPage)) + 1
            If (_threadCount = 0) Then
                wr.Write("&nbsp;" & GetLanguage("F_NoThread"))
            Else
                Dim TempString As String
                TempString = Replace(GetLanguage("F_Paging"), "{pagenum}", (_threadsPage + 1).ToString)
                TempString = Replace(TempString, "{numpage}", pageCount.ToString)
                wr.Write(TempString)
            End If

            wr.RenderEndTag() ' B
            wr.RenderEndTag() ' Td

            ' And on the right hand side, render threads navigation (<< < Previous | Next > >>)
            If (pageCount > 1) Then
                RenderThreadsPaging(wr)
            End If
            ' Close out this table and row
            wr.RenderEndTag() ' Tr
            wr.RenderEndTag() ' Table
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr
        End Sub

        Public Overrides Sub Render(ByVal wr As HtmlTextWriter)

            RenderTableBegin(wr, 1, 2)
            'RenderHeader(wr)
            RenderThreads(wr)
            RenderFooter(wr)
            RenderTableEnd(wr) '

        End Sub

    End Class 'ForumThreads

#End Region 'ForumThreads

#Region "ForumThreadInfo"

    Public Class ForumThreadInfo

        Private Const ThreadInfoCacheKeyPrefix As String = "ThreadInfo"
        Private Const _profilePage As Integer = TTT_ForumDispatch.ForumDesktopType.ForumProfile

        Private _portalID As Integer '<tam:note value=setting for DNN Integration>
        Private ZmoduleID As Integer '<tam:note value=setting for DNN Integration>

        Private _pinned As Boolean
        Private ZforumID As Integer
        Private _dateLastPost As DateTime
        Private _threadID As Integer
        Private _replies As Integer
        Private _views As Integer
        Private _startedByAlias As String
        Private _startedByUserID As Integer
        Private _subject As String
        Private _lastPostAlias As String
        Private _lastPostUserID As Integer
        Private _lastPostInfo As String
        Private _image As String
        Private _parent As ForumItemInfo
        Private Zuser As ForumUser
        Private Zconfig As ForumConfig

        Public Sub New()
        End Sub 'New

        Private Sub New(ByVal ThreadID As Integer)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim dbForum As New ForumDB()
            Dim NeedToExit As Boolean = False
            Dim dr As SqlDataReader = dbForum.TTTForum_GetSingleThread(ThreadID)
            If dr.Read() Then
                PopulateInfo(dr)
            Else
                NeedToExit = True
            End If
            dr.Close()
            If NeedToExit Then
                HttpContext.Current.Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString))
            End If
        End Sub

        Private Sub New(ByVal DataReader As SqlDataReader)

            PopulateInfo(DataReader)

        End Sub

        Public Shared Function PopulateThreadInfo(ByVal DataReader As SqlDataReader) As ForumThreadInfo

            Dim threadInfo As ForumThreadInfo = New ForumThreadInfo(DataReader)
            Return threadInfo

        End Function

        Private Sub PopulateInfo(ByVal dr As SqlDataReader)

            ZforumID = ConvertInteger(dr("ForumID"))
            _dateLastPost = ConvertDateTime(dr("DateLastPost"))
            ' A modifier avec la date de l utilisateur user.TimeZone
            _lastPostAlias = ConvertString(dr("LastPostAlias"))
            _lastPostUserID = ConvertInteger(dr("LastPostUserID"))
            _pinned = ConvertBoolean(dr("Pinned"))
            _replies = ConvertInteger(dr("Replies"))
            _startedByAlias = ConvertString(dr("StartedByAlias"))
            _startedByUserID = ConvertInteger(dr("StartedByUserID"))
            _subject = ConvertString(dr("Subject"))
            _threadID = ConvertInteger(dr("ThreadID"))
            _views = ConvertInteger(dr("Views"))
            _image = ConvertString(dr("Image"))

            Zuser = ForumUser.GetForumUser(_startedByUserID)
            _parent = ForumItemInfo.GetForumInfo(ZforumID)
            _portalID = _parent.PortalID
            ZmoduleID = _parent.ModuleID

            Zconfig = ForumConfig.GetForumConfig(ZmoduleID)

        End Sub 'PopulateInfo



        Public Shared Function GetThreadInfo(ByVal ThreadID As Integer) As ForumThreadInfo
            ' Grab reference to the applicationstate object
            ' Need to change Forum Cashing

            Dim TempKey As String = GetDBName & ThreadInfoCacheKeyPrefix & CStr(ThreadID)
            Dim context As HttpContext = HttpContext.Current
            Dim threadInfo As ForumThreadInfo

            If Context.Cache(TempKey) Is Nothing Then
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                ' If this object has not been instantiated yet, we need to grab it
                threadInfo = New ForumThreadInfo(ThreadID)
                Context.Cache.Insert(TempKey, threadInfo, DotNetZoom.CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, threadInfo.ModuleID), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, Nothing)
            Else
                threadInfo = CType(Context.Cache(TempKey), ForumThreadInfo)
            End If
            Return threadInfo

        End Function

        Public Shared Sub ResetThreadInfo(ByVal ThreadID As Integer)
            Dim TempKey As String = GetDBName & ThreadInfoCacheKeyPrefix & CStr(ThreadID)
            Dim context As HttpContext = HttpContext.Current
            context.Cache.Remove(TempKey)
        End Sub


#Region "ThreadInfo-Render"

        Public Sub Render(ByVal wr As HtmlTextWriter, ByVal IsAuthor As Boolean, ByVal threadsPerPage As Integer, ByVal lastVisited As DateTime, ByVal page As Page, ByVal imageURL As String, ByVal loggedOnUserID As Integer, ByVal document As String, ByVal galleryURL As String)
            ' Start row
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Render thread image, 
            ' if number of posts in this thread is great than the number of posts that can be displayed on one page
            Dim totalPosts As Integer = Replies + 1
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "25")
            wr.AddAttribute(HtmlTextWriterAttribute.Valign, "middle")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            If Len(_image) > 0 Then
                ' hyperlink to full size image
                If _parent.IsIntegrated Then
                    wr.AddAttribute(HtmlTextWriterAttribute.Href, Replace(_image, "/_thumbs", ""))
                Else
                    wr.AddAttribute(HtmlTextWriterAttribute.Href, _image)
                End If
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.AddAttribute(HtmlTextWriterAttribute.Alt, "*")
                wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                wr.AddAttribute(HtmlTextWriterAttribute.Src, _image)
                wr.RenderBeginTag(HtmlTextWriterTag.Img) ' Img
                wr.RenderEndTag() ' Img
                wr.RenderEndTag() ' A
            Else
                wr.AddAttribute(HtmlTextWriterAttribute.Src, glbPath & "images/1x1.gif")
                wr.AddAttribute(HtmlTextWriterAttribute.Width, "32")
                wr.AddAttribute(HtmlTextWriterAttribute.Height, "32")
                If _pinned Then
                    wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: -16px -192px;")
                    wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_PinnedPost"))
                    wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_Pinned"))
                Else
                    If (totalPosts > threadsPerPage) Then
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: -16px -32px;")
                        wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_AThread"))
                        wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_aAThread"))
                    Else
                        wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: -16px 0px;")
                        wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_AThread0"))
                        wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_aAThread0"))
                    End If
                End If
                wr.RenderBeginTag(HtmlTextWriterTag.Img)
                wr.RenderEndTag() ' Img
            End If

            wr.RenderEndTag() ' Td

            ' Thread subject with link (and indicate whether or not this thread is pinned)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.Valign, "middle")
            wr.AddAttribute(HtmlTextWriterAttribute.align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "25")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)

            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("threadid={0}&scope=post", ThreadID), "postid=&action=&searchpage=&threadpage=&threadspage=") + String.Format("#{0}", ThreadID))
            wr.RenderBeginTag(HtmlTextWriterTag.A)
            wr.Write(Subject)
            wr.RenderEndTag() ' A

            ' If thread spans several pages, then we need to indicate this in the thread list
            ' by displaying text like (Page 1, 2, 3, ..., 5)
            Dim pageCount As Integer
            If (totalPosts > threadsPerPage) Then
                wr.Write(" (")
                wr.Write(GetLanguage("FZpage") & " ")
                pageCount = CInt(Math.Floor((totalPosts - 1) / threadsPerPage)) + 1
                Dim pageCountCapped As Integer = Math.Min(pageCount, threadsPerPage)
                Dim showFinalPage As Boolean = (pageCountCapped < pageCount)

                Dim threadPage As Integer

                For threadPage = 1 To pageCountCapped
                    wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("threadid={0}&threadpage={1}&scope=post", ThreadID.ToString, threadPage.ToString), "postid=&action=&searchpage=&threadspage="))
                    wr.RenderBeginTag(HtmlTextWriterTag.A)
                    wr.Write(String.Format("{0}", threadPage))
                    wr.RenderEndTag() ' A
                    If ((threadPage < pageCountCapped) OrElse showFinalPage) Then
                        wr.Write(", ")
                    End If
                Next

                If (showFinalPage) Then
                    If (pageCount > pageCountCapped) Then '<tam:note value=shhould check later
                        wr.Write("..., ")
                        wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("threadid={0}&threadpage={1}", ThreadID.ToString, pageCount.ToString), "postid=&action=&searchpage=&threadspage="))
                        wr.RenderBeginTag(HtmlTextWriterTag.A)
                        wr.Write(pageCount.ToString())
                        wr.RenderEndTag() ' A
                    End If
                End If
                wr.Write(")")
            End If

            ' Display new image if this thread is new since last time user visited
            If loggedOnUserID > 0 AndAlso (lastVisited < _dateLastPost) Then
                wr.AddAttribute(HtmlTextWriterAttribute.Src, imageURL + GetLanguage("N") + "_new.gif")
                wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_NewPost"))
                wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_NewPostA"))
                wr.RenderBeginTag(HtmlTextWriterTag.Img)
                wr.RenderEndTag() ' Img
            End If

            ' Display edit to the started post of the thread if loggoned user is thread's author
            If IsAuthor Then
                wr.Write("&nbsp;")
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("edit=control&postid={0}&forumid={1}&mid={2}&tabid={3}&action=edit", _threadID.ToString, ZforumID.ToString, ZmoduleID.ToString, _portalSettings.ActiveTab.TabId), "searchpage=&threadspage="))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                wr.AddAttribute(HtmlTextWriterAttribute.Src, glbPath & "images/1x1.gif")
                wr.AddAttribute(HtmlTextWriterAttribute.Width, "16")
                wr.AddAttribute(HtmlTextWriterAttribute.Height, "16")

                wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: 0px -128px;")
                wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_EditYPost"))
                wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_EditYPostA"))
                wr.RenderBeginTag(HtmlTextWriterTag.Img)
                wr.RenderEndTag() ' Img

                wr.RenderEndTag() ' A
            End If
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' Started by
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("forumpage={0}&userid={1}", _profilePage.ToString, _startedByUserID.ToString), "scope=&threadid=&searchpage=&threadpage=&threadspage="))
            wr.RenderBeginTag(HtmlTextWriterTag.A)
            wr.Write(StartedByAlias)
            wr.RenderEndTag() 'A
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' Replies
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "50")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.Write(Replies)
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' Views
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "50")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.Write(Views)
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "140")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")

            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("threadid={0}&threadpage={1}&scope=post", _threadID.ToString, pageCount.ToString), "postid=&action=&searchpage=&threadspage="))
            wr.RenderBeginTag(HtmlTextWriterTag.A)
            wr.Write(LastPostInfo(loggedOnUserID))
            wr.RenderEndTag() 'A

            wr.Write(GetLanguage("F_Who") & " ")
            wr.RenderBeginTag(HtmlTextWriterTag.B)
            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("forumpage={0}&userid={1}&tabid={2}", _profilePage.ToString, _lastPostUserID.ToString, _portalSettings.ActiveTab.TabId), "scope=&threadid=&searchpage=&threadpage=&threadspage="))
            wr.RenderBeginTag(HtmlTextWriterTag.A)
            wr.Write(LastPostAlias)
            wr.RenderEndTag() 'A
            wr.RenderEndTag() 'B

            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td
            ' End row
            wr.RenderEndTag() ' Tr

        End Sub 'Render 

#End Region

#Region "ThreadInfo-Public Properties"

        Public ReadOnly Property PortalID() As Integer
            Get
                Return _portalID
            End Get
        End Property

        Public ReadOnly Property ModuleID() As Integer
            Get
                Return _parent.ModuleID
            End Get
        End Property

        Public Property Pinned() As Boolean
            Get
                Return _pinned
            End Get
            Set(ByVal Value As Boolean)
                _pinned = Value
            End Set
        End Property

        Public Property DateLastPost() As DateTime
            Get
                Return _dateLastPost
            End Get
            Set(ByVal Value As DateTime)
                _dateLastPost = Value
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

        Public Property ThreadID() As Integer
            Get
                Return _threadID
            End Get
            Set(ByVal Value As Integer)
                _threadID = Value
            End Set
        End Property


        Public Property Replies() As Integer
            Get
                Return _replies
            End Get
            Set(ByVal Value As Integer)
                _replies = Value
            End Set
        End Property


        Public Property Views() As Integer
            Get
                Return _views
            End Get
            Set(ByVal Value As Integer)
                _views = Value
            End Set
        End Property


        Public Property LastPostAlias() As String
            Get
                Return _lastPostAlias
            End Get
            Set(ByVal Value As String)
                _lastPostAlias = Value
            End Set
        End Property

        Public Property LastPostUserID() As Integer
            Get
                Return _lastPostUserID
            End Get
            Set(ByVal Value As Integer)
                _lastPostUserID = Value
            End Set
        End Property


        Public Property StartedByAlias() As String
            Get
                Return _startedByAlias
            End Get
            Set(ByVal Value As String)
                _startedByAlias = Value
            End Set
        End Property

        Public Property StartedByUserID() As Integer
            Get
                Return _startedByUserID
            End Get
            Set(ByVal Value As Integer)
                _startedByUserID = Value
            End Set
        End Property


        Public Property Subject() As String
            Get
                Return _subject
            End Get
            Set(ByVal Value As String)
                _subject = Value
            End Set
        End Property

        Public Property Image() As String
            Get
                Return _image
            End Get
            Set(ByVal Value As String)
                _image = Value
            End Set
        End Property

        Public ReadOnly Property User() As ForumUser
            Get
                Return Zuser
            End Get
        End Property

        Public ReadOnly Property Parent() As ForumItemInfo
            Get
                Return _parent
            End Get
        End Property

        Public ReadOnly Property LastPostInfo(ByVal loggedOnUserID As Integer) As String
            Get

                ' ViewerId DateTime to see the date time in local time for registered user

                Dim TempDateTime As DateTime
                If loggedOnUserID > 0 Then
                    Dim Currentuser As ForumUser = ForumUser.GetForumUser(loggedOnUserID)
                    TempDateTime = _dateLastPost.AddMinutes(GetTimeDiff(Currentuser.TimeZone))
                Else
                    TempDateTime = _dateLastPost.AddMinutes(GetTimeDiff(-99))
                End If

                Return TTTUtils.GetLastPostInfo(TempDateTime, _lastPostAlias, _pinned)
            End Get
        End Property

#End Region

    End Class 'ForumThreadInfo

#End Region


#Region "ForumThreadInfoCollection"

    Public Class ForumThreadInfoCollection
        Inherits ArrayList

        Private ZforumID As Integer
        Private ZpageSize As Integer
        Private ZpageIndex As Integer

        Public Sub New()

        End Sub

        Public Sub New(ByVal ForumID As Integer, ByVal PageSize As Integer, ByVal PageIndex As Integer)

            ZforumID = ForumID
            ZpageSize = PageSize
            ZpageIndex = PageIndex

            PopulateCollection()
            'Return Me
        End Sub 'New

        Private Sub PopulateCollection()
            Dim dbForum As New ForumDB()
            Dim strFilter As String = "" '<tam note=reserve for filter in the next verion" />
            Dim dr As SqlDataReader = dbForum.TTTForum_GetThreads(ZforumID, ZpageSize, ZpageIndex, strFilter)
            While dr.Read
                Dim threadInfo As ForumThreadInfo = ForumThreadInfo.PopulateThreadInfo(dr)
                Me.Add(threadInfo)
            End While
            dr.Close()

        End Sub

        Public Sub New(ByVal c As ICollection)
            MyBase.New(c)
        End Sub 'New

        Public ReadOnly Property ForumID() As Integer
            Get
                Return ZforumID
            End Get
        End Property

        Public ReadOnly Property PageSize() As Integer
            Get
                Return ZpageSize
            End Get
        End Property

        Public ReadOnly Property PageIndex() As Integer
            Get
                Return ZpageIndex
            End Get
        End Property

    End Class 'ForumThreadInfoCollection

#End Region


End Namespace