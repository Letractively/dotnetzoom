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
Imports System.Configuration
Imports System.Collections
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.HttpUtility
Imports DotNetZoom.TTTUtils
Imports System.Web.Caching

Namespace DotNetZoom

#Region "ForumItems"

    Public Class ForumItems
        Inherits ForumObject

        Private _forumItemCollection As ForumItemCollection
        Private _forumGroupID As Integer
        Private _threadsCount As Integer
        Private _postsCount As Integer
        Private ZforumConfig As ForumConfig
       
        Public Sub New(ByVal forum As Forum, ByVal ForumGroupID As Integer)

            MyBase.New(forum)
            _forumGroupID = ForumGroupID
            ZforumConfig = forum.Config

        End Sub 'New

        Public Overrides Sub CreateChildControls()

        End Sub

        Public Overrides Sub OnPreRender()

            _forumItemCollection = New ForumItemCollection(_forumGroupID)

        End Sub

        Private Sub RenderForums(ByVal wr As HtmlTextWriter)

            Dim lastVisited As DateTime
            Dim user As ForumUser = ForumUser.GetForumUser(LoggedOnUserID)
			If HttpContext.Current.Session("LastThreadView") is nothing then
			HttpContext.Current.Session("LastThreadView") = user.LastThreadView
			end if
			lastVisited = Convert.ToDateTime(HttpContext.Current.Session("LastThreadView"))

            Dim document As String = GetFullDocument()
			
			' PUT IN SKIN IMAGEFOLDER
            Dim imageURL As String = ForumConfig.SkinFolder()
            Dim _forumItemInfo As ForumItemInfo

            For Each _forumItemInfo In _forumItemCollection
                _forumItemInfo.Render(wr, lastVisited, Page, imageURL, LoggedOnUserID, document)
            Next

        End Sub

        Public Overrides Sub Render(ByVal wr As HtmlTextWriter)

            RenderTableBegin(wr, 1, 2)
            RenderForums(wr)
            RenderTableEnd(wr)

        End Sub

    End Class 'ForumItem

#End Region

#Region "ForumItemInfo"

    Public Class ForumItemInfo

        Private Const ForumItemInfoCacheKeyPrefix As String = "ForumItemInfo"
        Private Const _profilePage As Integer = TTT_ForumDispatch.ForumDesktopType.ForumProfile

        Private _portalID As Integer
        Private ZmoduleID As Integer
        Private ZforumID As Integer
        Private _IsActive As Boolean
        Private _ForumGroupID As Integer
        Private _forumGroup As ForumGroupInfo
        Private _Name As String
        Private _Description As String
        Private _CreatedByUser As Integer
        Private _Creator As ForumUser
        Private _CreatedDate As DateTime
        Private _IsModerated As Boolean
        Private _SortOrder As Integer
        Private _TotalPosts As Integer
        Private _TotalThreads As Integer
        Private _MostRecentPostID As Integer
        Private _MostRecentPostAuthor As String
        Private _MostRecentPostAuthorID As Integer
        Private _MostRecentPostDate As DateTime
        Private _IsIntegrated As Boolean
        Private _IntegratedGallery As Integer
        Private _IntegratedAlbumName As String
        Private _IsPrivate As Boolean
        Private _AuthorizedRoles As String = ""
        Private Zconfig As ForumConfig


        Public Sub New()

        End Sub 'New

        Private Sub New(ByVal ForumID As Integer)
            Dim dbForum As New ForumDB()
            Dim dr As SqlDataReader = dbForum.TTTForum_GetSingleForum(ForumID)
            If dr.Read() Then
                PopulateInfo(dr)
            End If

            dr.Close()

        End Sub

        Private Sub New(ByVal DataReader As SqlDataReader)

            PopulateInfo(DataReader)

        End Sub

        Public Shared Function PopulateForumInfo(ByVal DataReader As SqlDataReader) As ForumItemInfo

            Dim forumInfo As ForumItemInfo = New ForumItemInfo(DataReader)
            Return forumInfo

        End Function

        Public Shared Function GetForumInfo(ByVal ForumID As Integer) As ForumItemInfo

            ' Grab reference to the applicationstate object
			' Need to change Forum Cashing
            
			Dim TempKey as String = GetDBName & ForumItemInfoCacheKeyPrefix & CStr(ForumID)
			Dim context As HttpContext = HttpContext.Current
			Dim forumInfo As ForumItemInfo
            If Context.Cache(TempKey) Is Nothing Then
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' If this object has not been instantiated yet, we need to grab it
            forumInfo = New ForumItemInfo(ForumID)
			Context.Cache.Insert(TempKey, forumInfo, DotNetZoom.CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, ForumInfo.ModuleID), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
 			else
			forumInfo = CType(Context.Cache(TempKey), ForumItemInfo)
            End If
            Return forumInfo

        End Function

        Public Shared Sub ResetForumInfo(ByVal ForumID As Integer)
			Dim TempKey as String = GetDBName & ForumItemInfoCacheKeyPrefix & CStr(ForumID)
			Dim context As HttpContext = HttpContext.Current
			context.Cache.Remove(TempKey)
        End Sub
		
        Public Sub UpdateForumInfo()
            Dim dbForum As New ForumDB()
            dbForum.TTTForum_ForumCreateUpdateDelete(_ForumGroupID, _portalID, ZmoduleID, _Name, _Description, _CreatedByUser, _IsModerated, _IsActive, _IsIntegrated, _IntegratedGallery, _IntegratedAlbumName, _IsPrivate, _AuthorizedRoles, 1, ZforumID)
        End Sub

        Private Sub PopulateInfo(ByVal dr As SqlDataReader)

            ZforumID = ConvertInteger(dr("ForumID"))
            _ForumGroupID = ConvertInteger(dr("ForumGroupID"))
            _portalID = ConvertInteger(dr("PortalID"))
            ZmoduleID = ConvertInteger(dr("ModuleID"))
            _IsActive = ConvertBoolean(dr("IsActive"))
            _Name = ConvertString(dr("Name"))
            _Description = ConvertString(dr("Description"))
            _CreatedByUser = ConvertInteger(dr("CreatedByUser"))
            _CreatedDate = ConvertDateTime(dr("CreatedDate"))
            _IsModerated = ConvertBoolean(dr("IsModerated"))
            _SortOrder = ConvertInteger(dr("SortOrder"))
            _TotalPosts = ConvertInteger(dr("TotalPosts"))
            _TotalThreads = ConvertInteger(dr("TotalThreads"))
            _MostRecentPostID = ConvertInteger(dr("MostRecentPostID"))
            _MostRecentPostAuthor = ConvertString(dr("MostRecentPostAuthor"))
            _MostRecentPostAuthorID = ConvertInteger(dr("MostRecentPostAuthorID"))
            _MostRecentPostDate = ConvertDateTime(dr("MostRecentPostDate"))
            _IsIntegrated = ConvertBoolean(dr("IsIntegrated"))
            _IntegratedGallery = ConvertInteger(dr("IntegratedGallery"))
            _IntegratedAlbumName = ConvertString(dr("IntegratedAlbumName"))
            _IsPrivate = ConvertBoolean(dr("IsPrivate"))
            Zconfig = ForumConfig.GetForumConfig(ZmoduleID)

            Dim dbForum As New ForumDB()
            Dim drRoles As SqlDataReader = dbForum.TTTForum_PrivateForum_GetRoles(ZforumID)

            While drRoles.Read
                _AuthorizedRoles += ConvertString(drRoles("RoleID")) & ";"
            End While
            drRoles.Close()

        End Sub      'PopulateInfo

        Public Sub Render(ByVal wr As HtmlTextWriter, ByVal lastVisited As DateTime, ByVal page As Page, ByVal imageURL As String, ByVal loggedOnUserID As Integer, ByVal document As String)

            ' New row
            wr.RenderBeginTag(HtmlTextWriterTag.Tr) '<tr
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.Valign, "middle")
            wr.AddAttribute(HtmlTextWriterAttribute.align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "25")

            wr.RenderBeginTag(HtmlTextWriterTag.Td) '<td
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Src, glbPath & "images/1x1.gif")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "32")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "32")

            If _IsPrivate Then
                If (loggedOnUserID > 0) AndAlso (_MostRecentPostDate > lastVisited) Then

                    wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: -16px -160px;")
                    wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_NewMessages"))
                    wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_NewMessages0"))
                Else

                    wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: -16px -128px;")
                    wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_NoNewMessage"))
                    wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_NoNewMessage0"))
                End If
            Else
                If (loggedOnUserID > 0) AndAlso (_MostRecentPostDate > lastVisited) Then

                    wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: -16px -96px;")
                    wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_NewMessages"))
                    wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_NewMessages0"))
                Else

                    wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: -16px -64px;")
                    wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_NoNewMessage"))
                    wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_NoNewMessage0"))
                End If
            End If

            wr.RenderBeginTag(HtmlTextWriterTag.Img)
            wr.RenderEndTag() ' Img
            wr.RenderEndTag() ' </td>

            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.RenderBeginTag(HtmlTextWriterTag.Td) '<td
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTSubHeader")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("forumid={0}&scope=thread", ZforumID), ""))
            wr.RenderBeginTag(HtmlTextWriterTag.A) '<A
            wr.Write(_Name)
            wr.RenderEndTag() ' A> 

            ' Display new image if this post is new since last time user visited
            If loggedOnUserID > 0 AndAlso (lastVisited < _CreatedDate) Then
                wr.AddAttribute(HtmlTextWriterAttribute.Src, imageURL + GetLanguage("N") + "_new.gif")
                wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("F_NewPost"))
                wr.AddAttribute(HtmlTextWriterAttribute.Alt, GetLanguage("F_NewPostA"))
                wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                wr.RenderBeginTag(HtmlTextWriterTag.Img)
                wr.RenderEndTag()
            End If
            wr.RenderEndTag() ' Span

            ' Display forum description
            If Len(_Description) > 0 Then
                wr.Write("<br>")
                wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
                wr.RenderBeginTag(HtmlTextWriterTag.Span) 'Span
                wr.Write(_Description)
                wr.RenderEndTag() ' Span
            End If

            ' if rss put it on
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If File.Exists(HttpContext.Current.Request.MapPath(_portalSettings.UploadDirectory) & "forum" & ZforumID.ToString & ".xml") Then
                wr.Write("&nbsp;")
                wr.AddAttribute(HtmlTextWriterAttribute.Href, "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & _portalSettings.UploadDirectory & "forum" & ZforumID.ToString & ".xml")
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.AddAttribute(HtmlTextWriterAttribute.Src, glbPath & "images/1x1.gif")
                wr.AddAttribute(HtmlTextWriterAttribute.Title, GetLanguage("channel_syndicate"))
                wr.AddAttribute(HtmlTextWriterAttribute.Alt, "rss")
                wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -445px;")
                wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                wr.AddAttribute(HtmlTextWriterAttribute.Width, "36")
                wr.AddAttribute(HtmlTextWriterAttribute.Height, "14")
                wr.RenderBeginTag(HtmlTextWriterTag.Img)
                wr.RenderEndTag() ' img
                wr.RenderEndTag() ' A
            End If
			
			
			
            wr.RenderEndTag() ' Td

            ' Threads count
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.Write(_TotalThreads.ToString)
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' Posts count
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.Write(_TotalPosts.ToString)
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' Last Post
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)

            'wr.Write(LastPostInfo) 
            If _MostRecentPostAuthorID > 0 Then
                Dim Zconfig As ForumConfig = ForumConfig.GetForumConfig(ZmoduleID)
				
			Dim TempDateTime as DateTime
            If loggedOnUserID > 0 Then
				Dim Currentuser As ForumUser = ForumUser.GetForumUser(loggedOnUserID)
				TempDateTime = _MostRecentPostDate.AddMinutes(GetTimeDiff(Currentuser.TimeZone))
                Else
				TempDateTime = _MostRecentPostDate.AddMinutes(GetTimeDiff(-99))
             End If
				
                Dim lastPostInfo As String = TTTUtils.GetLastPostInfo(TempDateTime, MostRecentPostAuthor, False)
                Dim lastPostThreadID As Integer = ForumDB.TTTForum_GetThreadFromPost(_MostRecentPostID)
                Dim pageCount As Integer = CInt(Math.Floor(ForumDB.TTTForum_GetThreadRepliesCount(lastPostThreadID) / Zconfig.PostsPerPage)) + 1

                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("forumid={0}&threadid={1}&threadpage={2}&scope=post", ZforumID, lastPostThreadID.ToString, pageCount.ToString), ""))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.Write(lastPostInfo)
                wr.RenderEndTag() 'A
                wr.Write(GetLanguage("F_Who") + " ")
                wr.RenderBeginTag(HtmlTextWriterTag.B)
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("forumpage={0}&userid={1}", _profilePage.ToString, _MostRecentPostAuthorID.ToString), ""))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.Write(_MostRecentPostAuthor)
                wr.RenderEndTag() 'A
                wr.RenderEndTag() 'B
            Else
                wr.Write(GetLanguage("F_none"))
            End If

            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' End row
            wr.RenderEndTag() ' Tr

        End Sub 'Render

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

        Public Property IsActive() As Boolean
            Get
                Return _IsActive
            End Get
            Set(ByVal Value As Boolean)
                _IsActive = Value
            End Set
        End Property

        Public Property ForumGroupID() As Integer
            Get
                Return _ForumGroupID
            End Get
            Set(ByVal Value As Integer)
                _ForumGroupID = Value
            End Set
        End Property

        Public ReadOnly Property ForumGroup() As ForumGroupInfo
            Get
                Dim dbForum As New ForumDB()
                _forumGroup = ForumGroupInfo.GetGroupInfo(_ForumGroupID)
                Return _forumGroup
            End Get
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal Value As String)
                _Description = Value
            End Set
        End Property

        Public Property CreatedByUser() As Integer
            Get
                Return _CreatedByUser
            End Get
            Set(ByVal Value As Integer)
                _CreatedByUser = Value
            End Set
        End Property

        Public ReadOnly Property Creator() As ForumUser
            Get
                _Creator = ForumUser.GetForumUser(_CreatedByUser)
                Return _Creator
            End Get

        End Property

        Public Property CreatedDate() As DateTime
            Get
                Return _CreatedDate
            End Get
            Set(ByVal Value As DateTime)
                _CreatedDate = Value
            End Set
        End Property

        Public Property IsModerated() As Boolean
            Get
                Return _IsModerated
            End Get
            Set(ByVal Value As Boolean)
                _IsModerated = Value
            End Set
        End Property

        Public Property SortOrder() As Integer
            Get
                Return _SortOrder
            End Get
            Set(ByVal Value As Integer)
                _SortOrder = Value
            End Set
        End Property

        Public Property TotalPosts() As Integer
            Get
                Return _TotalPosts
            End Get
            Set(ByVal Value As Integer)
                _TotalPosts = Value
            End Set
        End Property

        Public Property TotalThreads() As Integer
            Get
                Return _TotalThreads
            End Get
            Set(ByVal Value As Integer)
                _TotalThreads = Value
            End Set
        End Property

        Public Property MostRecentPostID() As Integer
            Get
                Return _MostRecentPostID
            End Get
            Set(ByVal Value As Integer)
                _MostRecentPostID = Value
            End Set
        End Property

        Public Property MostRecentPostAuthor() As String
            Get
                Return _MostRecentPostAuthor
            End Get
            Set(ByVal Value As String)
                _MostRecentPostAuthor = Value
            End Set
        End Property

        Public Property MostRecentPostAuthorID() As Integer
            Get
                Return _MostRecentPostAuthorID
            End Get
            Set(ByVal Value As Integer)
                _MostRecentPostAuthorID = Value
            End Set
        End Property

        Public Property MostRecentPostDate() As DateTime
            Get
                Return _MostRecentPostDate
            End Get
            Set(ByVal Value As DateTime)
                _MostRecentPostDate = Value
            End Set
        End Property

        Public Property IsIntegrated() As Boolean
            Get
                Return _IsIntegrated
            End Get
            Set(ByVal Value As Boolean)
                _IsIntegrated = Value
            End Set
        End Property

        Public Property IntegratedGallery() As Integer
            Get
                Return _IntegratedGallery
            End Get
            Set(ByVal Value As Integer)
                _IntegratedGallery = Value
            End Set
        End Property

        Public Property IntegratedAlbumName() As String
            Get
                Return _IntegratedAlbumName
            End Get
            Set(ByVal Value As String)
                _IntegratedAlbumName = Value
            End Set
        End Property

        Public Property IsPrivate() As Boolean
            Get
                Return _IsPrivate
            End Get
            Set(ByVal Value As Boolean)
                _IsPrivate = Value
            End Set
        End Property

        Public Property AuthorizedRoles() As String
            Get
                Return _AuthorizedRoles
            End Get
            Set(ByVal Value As String)
                _AuthorizedRoles = Value
            End Set
        End Property



    End Class 'ForumItemInfo

#End Region

#Region "ForumItemCollection"

    Public Class ForumItemCollection
        Inherits ArrayList

        Private _forumGroupID As Integer

        Public Sub New()

        End Sub

        Public Sub New(ByVal ForumGroupID As Integer)

            _forumGroupID = ForumGroupID

            PopulateCollection()
        End Sub 'New

        Private Sub PopulateCollection()
            Dim dbForum As New ForumDB()
            Dim dr As SqlDataReader = dbForum.TTTForum_GetForums(_forumGroupID)
            While dr.Read
                Dim forumInfo As ForumItemInfo = ForumItemInfo.PopulateForumInfo(dr)
                Me.Add(forumInfo)
            End While
            dr.Close()

        End Sub

        Public Sub New(ByVal c As ICollection)
            MyBase.New(c)
        End Sub 'New

        Public Property ForumGroupID() As Integer
            Get
                Return _forumGroupID
            End Get
            Set(ByVal Value As Integer)
                _forumGroupID = Value
            End Set
        End Property

    End Class 'ForumItemCollection

#End Region

End Namespace