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
Option Strict On

Imports System
Imports System.Configuration
Imports System.Collections
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.HttpUtility
Imports DotNetZoom.TTTUtils
Imports System.Web.Caching

Namespace DotNetZoom

#Region "ForumThread"

    Public Class ForumPostsModerate
        Inherits ForumObject

        Private _listView As Boolean
        Private _PostsPerPage As Integer
        Private _imageURL As String
        Private _avatarURL As String
        Private ZforumID As Integer
        Private _threadID As Integer
        Private _postID As Integer
        Private _moderatePage As Integer
        Private ZforumConfig As ForumConfig
        Private _parent As ForumThreadInfo
        Private _postModerateCollection As ForumPostModerateCollection
        Private WithEvents _viewDropDownList As DropDownList
        Private WithEvents cmdApprove As System.Web.UI.WebControls.ImageButton

        Public Sub New(ByVal forum As Forum, ByVal ForumID As Integer, ByVal PostID As Integer, ByVal ModeratePage As Integer)

            MyBase.New(forum)
            ZforumID = ForumID
            '_threadID = ThreadID
            _postID = PostID
            _moderatePage = ModeratePage
            '_flatView = forum.IsFlatView

            ZforumConfig = forum.Config
            _PostsPerPage = ZforumConfig.ThreadsPerPage
			
			' PUT IN SKIN IMAGEFOLDER
            _imageURL = ForumConfig.SkinImageFolder()
            _avatarURL = ZforumConfig.AvatarFolder
        End Sub 'New

        Public Overrides Sub CreateChildControls()
            '_viewDropDownList = New DropDownList()
            '_viewDropDownList.AutoPostBack = True
            '_viewDropDownList.Items.Add("Flat View")
            '_viewDropDownList.Items.Add("Tree View")
            '_viewDropDownList.Width = Unit.Pixel(150)
            '_viewDropDownList.CssClass = "WebSolutionFormControl"
            'AddHandler _viewDropDownList.SelectedIndexChanged, AddressOf ViewDropDownList_SelectedIndexChanged

            'Controls.Add(_viewDropDownList)
        End Sub

        Public Overrides Sub OnPreRender()

            Dim dbForum As New ForumDB()

            ' Get page of posts that will be rendered
            _postModerateCollection = New ForumPostModerateCollection(ZforumID)
        End Sub

        Private Sub RenderPosts(ByVal wr As HtmlTextWriter)
            
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)

            ' Author
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Auteur") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Post
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Message") )
            wr.RenderEndTag() ' Td

            ' Approved status column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_approve") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Date Post column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "200")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_PostDate") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Check
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "50")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Validate") & "&nbsp;")
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr

            ' Loop round rows in selected thread
            Dim document As String = GetFullDocument()
            Dim _PostModerate As ForumPostModerate
            For Each _PostModerate In _postModerateCollection
                wr.RenderBeginTag(HtmlTextWriterTag.Tr)
                wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "5")
                wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
                wr.AddAttribute(HtmlTextWriterAttribute.Height, "25")
                wr.RenderBeginTag(HtmlTextWriterTag.Td)
                Dim chkApprove As New CheckBox()
                With chkApprove
                    .ID = _PostModerate.PostID.ToString
                    .Checked = True
                End With
                Controls.Add(chkApprove)
                wr.RenderEndTag() ' Td
                wr.RenderEndTag() ' Tr


                Dim selected As Boolean = (_postID = _PostModerate.PostID)
                'select avatar for display based on user profile setting
                Dim avatar As String = ""

                If Len(_PostModerate.User.Avatar) > 0 Then
                    ' <tam:note with this release, avatar full url is saved in user database />
                    avatar = _PostModerate.User.Avatar
                End If

                _PostModerate.PortalID = Forum.PortalID
                _PostModerate.ModuleID = Forum.ModuleID
                _PostModerate.ForumID = ZforumID
                _PostModerate.Render(wr, True, _listView, selected, Page, LoggedOnUserID, avatar, _imageURL, _avatarURL, document)
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

            If _moderatePage <> 0 Then
                backwards = True
            End If

            If _moderatePage <> PageCount - 1 Then
                forwards = True
            End If

            If (backwards) Then
                ' < Previous 
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("threadid={0}&moderatePage={1}", _threadID, _moderatePage), "postid=&action="))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.write("&laquo;")
                wr.Write(GetLanguage("F_Prev"))
                wr.RenderEndTag() ' A
                wr.Write("&nbsp;|&nbsp;")
            End If

            Dim iThread As Integer
            For iThread = 1 To PageCount
                If iThread <> _moderatePage + 1 Then
                    wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("moderatePage={0}", iThread), "postid=&action="))
                Else
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
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("threadid={0}&moderatePage={1}", _threadID, _moderatePage + 2), "postid=&action="))
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
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "25")
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "6")
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
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' Reply link
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("edit=control&postid={0}&forumid={1}&mid={2}&tabid={3}&action=reply", _postID.ToString, ZforumID.ToString, "1", _portalSettings.ActiveTab.TabId), "moderatePage=&searchpage=&threadspage="))
            wr.RenderBeginTag(HtmlTextWriterTag.A)
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Src, _imageURL + "TTT_PostReply.gif")
            wr.AddAttribute(HtmlTextWriterAttribute.Alt, "*")
            wr.RenderBeginTag(HtmlTextWriterTag.Img) ' Img
            wr.RenderEndTag() ' Img
            wr.RenderEndTag() ' A
            wr.Write("&nbsp;")

            ' Quote link
            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("edit=control&postid={0}&forumid={1}&mid={2}&tabid={3}&action=quote", _postID.ToString, ZforumID.ToString, "1", _portalSettings.ActiveTab.TabId), "moderatePage=&searchpage=&threadspage="))
            wr.RenderBeginTag(HtmlTextWriterTag.A)
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Src, _imageURL + "TTT_PostQuote.gif")
            wr.AddAttribute(HtmlTextWriterAttribute.Alt, "*")
            wr.RenderBeginTag(HtmlTextWriterTag.Img) ' Img
            wr.RenderEndTag() ' Img
            wr.RenderEndTag() ' A

            wr.AddAttribute(HtmlTextWriterAttribute.Id, "cmdApprove")
            wr.AddAttribute(HtmlTextWriterAttribute.Type, "image")
            wr.AddAttribute(HtmlTextWriterAttribute.Src, _imageURL + "TTT_PostQuote.gif")
            wr.AddAttribute(HtmlTextWriterAttribute.Onclick, "cmdApprove_Click")
            wr.RenderBeginTag(HtmlTextWriterTag.Input)
            wr.RenderEndTag()

            wr.RenderEndTag() 'td

            'AddHandler cmdApprove.Click, AddressOf cmdApprove_Click

            ' Page x of y text
            Dim pageCount As Integer = CInt(Math.Floor((ForumDB.TTTForum_GetThreadRepliesCount(_threadID)) / _PostsPerPage)) + 1
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

        Private Sub ViewDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _viewDropDownList.SelectedIndexChanged

        End Sub

        Private Sub cmdApprove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        End Sub

    End Class 'ForumThread

#End Region

#Region "ForumPostInfo"

    Public Class ForumPostModerate

        Private Const PostModerateCacheKeyPrefix As String = "PostModerate"
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
        Private _isApproved As Boolean
        Private _isLocked As Boolean
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

        Public Shared Function PopulatePostModerate(ByVal DataReader As SqlDataReader) As ForumPostModerate

            Dim postModerate As ForumPostModerate = New ForumPostModerate(DataReader)
            Return postModerate

        End Function

        Private Sub PopulateInfo(ByVal dr As SqlDataReader)

            ' Post specifics
            _postID = ConvertInteger(dr("PostID"))
            _body = ConvertString(dr("Body"))
            _flatSortOrder = ConvertInteger(dr("FlatSortOrder"))
            _notify = ConvertBoolean(dr("Notify"))
            _parentPostID = ConvertInteger(dr("ParentPostID"))
            _postDate = ConvertDateTime(dr("PostDate"))
            _postLevel = ConvertInteger(dr("PostLevel"))
            _remoteAddr = ConvertString(dr("RemoteAddr"))
            _subject = ConvertString(dr("Subject"))
            _threadID = ConvertInteger(dr("ThreadID"))
            _treeSortOrder = ConvertInteger(dr("TreeSortOrder"))
            _flatSortOrder = ConvertInteger(dr("FlatSortOrder"))
            _lastModifiedDate = ConvertDateTime(dr("LastModifiedDate"))
            _lastModifiedAuthorID = ConvertInteger(dr("LastModifiedAuthorID"))
            _lastModifiedAuthor = ConvertString(dr("LastModifiedAuthor"))
            _isApproved = ConvertBoolean(dr("IsApproved"))
            _isLocked = ConvertBoolean(dr("IsLocked"))

            Zuser = ForumUser.GetForumUser(ConvertInteger(dr("UserID")))
            _parent = ForumThreadInfo.GetThreadInfo(_threadID)
            Zconfig = ForumConfig.GetForumConfig(ZmoduleID)

        End Sub 'PopulateInfo


        Public Shared Function GetPostModerate(ByVal PostID As Integer) As ForumPostModerate

            ' Grab reference to the applicationstate object
            ' Need to change Forum Cashing

            Dim TempKey As String = GetDBName & PostModerateCacheKeyPrefix & CStr(PostID)
            Dim context As HttpContext = HttpContext.Current
            Dim postModerate As ForumPostModerate
            If Context.Cache(TempKey) Is Nothing Then
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                ' If this object has not been instantiated yet, we need to grab it
                postModerate = New ForumPostModerate(PostID)
                Context.Cache.Insert(TempKey, postModerate, DotNetZoom.CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, postModerate.ModuleID), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), Caching.CacheItemPriority.low, Nothing)
            Else
                postModerate = CType(Context.Cache(TempKey), ForumPostModerate)
            End If
            Return postModerate

        End Function

        '       Public Shared Sub ResetPostModerate(ByVal PostID As Integer)
        '			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        '			Dim TempKey as String = GetDBName & PostModerateCacheKeyPrefix & CStr(PostID)
        '			Dim context As HttpContext = HttpContext.Current
        '			Dim postModerate As ForumPostModerate = CType(Context.Cache(TempKey), ForumPostModerate)
        '			If Not postModerate Is Nothing Then
        '				context.Cache.Remove(TempKey)
        '			End If
        '       End Sub


        Private Sub RenderLevelIndentCell(ByVal wr As HtmlTextWriter)
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "16")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.RenderEndTag()
        End Sub 'RenderLevelIndentCell

#Region "ForumPostModerate-Render"

        Public Sub Render(ByVal wr As HtmlTextWriter, ByVal displayActions As Boolean, ByVal ListView As Boolean, ByVal selected As Boolean, ByVal page As Page, ByVal loggedOnUserID As Integer, ByVal avatar As String, ByVal imageURL As String, ByVal avatarURL As String, ByVal document As String)

            ' New row
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)

            ' Left hand side contains user information (user alias, avatar, number of posts etc)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")

            'If Not ListView And Not selected Then
            If Not selected Then
                wr.AddAttribute(HtmlTextWriterAttribute.align, "center")
            Else
                wr.AddAttribute(HtmlTextWriterAttribute.Valign, "top")
                wr.AddAttribute(HtmlTextWriterAttribute.Rowspan, "2")
            End If

            wr.AddAttribute(HtmlTextWriterAttribute.Width, "150")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' We will put this user information in its own table in the first column
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "3")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "150")
            wr.RenderBeginTag(HtmlTextWriterTag.Table)

            ' User alias and number of posts
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            'link to user profile
            wr.RenderBeginTag(HtmlTextWriterTag.B)
            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("forumpage={0}&userid={1}", _profilePage.ToString, Zuser.UserID.ToString), "scope=&threadid=&searchpage=&moderatePage=&threadspage="))
            wr.RenderBeginTag(HtmlTextWriterTag.A)
            wr.Write(User.Alias)
            wr.RenderEndTag() ' A
            wr.RenderEndTag() ' B

            ' link to user webpage
            If Len(Zuser.URL) > 0 Then

                wr.AddAttribute(HtmlTextWriterAttribute.Href, Zuser.URL)
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.Write(Replace(Zuser.URL, "http://", ""))
                wr.RenderEndTag() ' A
                wr.Write("<br>")
            End If

            ' user post count

            wr.Write(String.Format(GetLanguage("F_Post") & ": {0}", User.PostCount))
            wr.Write("<br>")
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr

            ' End user information table
            wr.RenderEndTag() ' Table
            wr.RenderEndTag() ' Td


            'If Not selected Then
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "25")
            'End If

            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.AddAttribute(HtmlTextWriterAttribute.Name, PostID.ToString())
            wr.Write("&nbsp;")


            'If Not ListView And Not selected Then
            If Not selected Then
                ' Provide link to select a different post when in tree view mode
                Dim linkURL As String = TTTUtils.GetURL(document, page, String.Format("postid={0}", PostID.ToString), "action=&searchpage=&threadspage=")
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("postid={0}", PostID.ToString), "action=&searchpage=&threadspage=" + "#" + PostID.ToString))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
            Else
                ' bold subject if post is selected
                wr.RenderBeginTag(HtmlTextWriterTag.B)
            End If

            Dim subjectForumText As ForumText = New ForumText(Subject, Zconfig)
            wr.Write(subjectForumText.ProcessSingleLine())
            If Not selected Then
                wr.RenderEndTag() ' A
            Else
                wr.RenderEndTag() ' B
            End If
            wr.RenderEndTag() ' span
            wr.RenderEndTag() ' Td

            ' Approved status
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            'wr.AddAttribute(HtmlTextWriterAttribute.Width, "100")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.Write(IIf(_isApproved, GetLanguage("yes"), GetLanguage("no")))
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' Date
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            'wr.AddAttribute(HtmlTextWriterAttribute.Width, "200")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.Write(_postDate)
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' Check
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            'wr.AddAttribute(HtmlTextWriterAttribute.Width, "50")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            'wr.RenderBeginTag(HtmlTextWriterTag.Span)

            wr.AddAttribute(HtmlTextWriterAttribute.Name, "ChkSelect")
            wr.AddAttribute(HtmlTextWriterAttribute.Type, "Checkbox")
            wr.AddAttribute(HtmlTextWriterAttribute.Id, PostID.ToString())
            wr.RenderBeginTag(HtmlTextWriterTag.Input)

            wr.RenderEndTag() ' input

            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr

            ' Message row

            If selected Then
                wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
                wr.RenderBeginTag(HtmlTextWriterTag.Tr)
                wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "4")

                wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
                wr.RenderBeginTag(HtmlTextWriterTag.Td)


                Dim bodyForumText As ForumText = New ForumText(HtmlDecode(Body), Zconfig)

                wr.Write(bodyForumText.Process())
                wr.RenderEndTag() ' Td
                wr.RenderEndTag() ' Tr
            End If

            ' Close out table and this row
            'wr.RenderEndTag() ' Table
            'wr.RenderEndTag() ' Td
            'wr.RenderEndTag() ' Tr
        End Sub 'Render

#End Region

#Region "ForumPostModerate-Public Properties"

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
                Return _lastModifiedAuthor
            End Get
            Set(ByVal Value As String)
                _lastModifiedAuthor = Value
            End Set
        End Property


#End Region

    End Class 'ForumPostInfo

#End Region

#Region "ForumPostModerateCollection"

    Public Class ForumPostModerateCollection
        Inherits ArrayList

        Private ZforumID As Integer
        Private _moderatePage As Integer
        Private _PostsPerPage As Integer

        Public Sub New()

        End Sub

        Public Sub New(ByVal ForumID As Integer)

            ZforumID = ForumID
            PopulateCollection()

        End Sub 'New

        Private Sub PopulateCollection() 'PostInfoCollection
            Dim dbForum As New ForumDB()
            Dim strFilter As String = "" 'this is reserve for filter in next version
            Dim dr As SqlDataReader = dbForum.TTTForum_Moderate_GetPosts(ZforumID)
            While dr.Read
                Dim postModerate As ForumPostModerate = ForumPostModerate.PopulatePostModerate(dr)
                'postInfo.Parent = _parent
                Me.Add(postModerate)
            End While
        End Sub

        Public ReadOnly Property ForumID() As Integer
            Get
                Return ZforumID
            End Get
        End Property

        Public ReadOnly Property moderatePage() As Integer
            Get
                Return _moderatePage
            End Get
        End Property

        Public ReadOnly Property PostsPerPage() As Integer
            Get
                Return _PostsPerPage
            End Get
        End Property

    End Class

#End Region

End Namespace 'TTTForum