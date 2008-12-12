'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.9)
'======================================================================================= 
' For TTTCompany                    http://www.tttcompany.com
' Author:                           TAM TRAN MINH   (tamttt - tam@tttcompany.com)
' With ideas/code contributed by:   JOE BRINKMAN    (Jbrinkman - joe.brinkman@tag-software.net) 
'                                   SAM HUNT        (Ossy - Sam.Hunt@nastech.eds.com)
'                                   CLEM MESSERLI   (Webguy96 - webguy96@hotmail.com)
'=======================================================================================
Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls


Namespace DotNetZoom

#Region "ForumSearch"

    Public Class ForumSearch
        Inherits ForumObject
        Private _forumSearchInfoCollection As ForumSearchInfoCollection
        Private ZforumID As String
        Private _postsCount As Integer
        Private _postsPerPage As Integer
        Private _searchPage As Integer
        Private _searchTerms As String
		Private _searchObject As String
        Private _imageURL As String
		Private _UserAlias As String
		Private _StartDate As String
		Private _EndDate As String
        Private ZforumConfig As ForumConfig

        Public Sub New(ByVal forum As Forum, ByVal ForumID As String, ByVal searchPage As Integer, ByVal searchObject As String,  ByVal searchTerms As String, Optional ByVal UserAlias As String = "", Optional ByVal StartDate As String = "", Optional ByVal EndDate As String = "")

            MyBase.New(forum)
			Dim objSecurity As New PortalSecurity()
            ZforumID = objSecurity.InputFilter(ForumID, PortalSecurity.FilterFlag.NoSQL)
            _searchPage = searchPage
            _searchTerms = objSecurity.InputFilter(searchTerms, PortalSecurity.FilterFlag.NoSQL)
			_searchObject = objSecurity.InputFilter(searchObject, PortalSecurity.FilterFlag.NoSQL)
            ZforumConfig = forum.Config
            _postsPerPage = ZforumConfig.PostsPerPage
			_UserAlias = objSecurity.InputFilter(UserAlias, PortalSecurity.FilterFlag.NoSQL)
			_StartDate = objSecurity.InputFilter(StartDate, PortalSecurity.FilterFlag.NoSQL)
			_EndDate = objSecurity.InputFilter(EndDate, PortalSecurity.FilterFlag.NoSQL)
			' PUT IN SKIN IMAGEFOLDER
            _imageURL = ForumConfig.SkinFolder()

        End Sub 'New

        Public Overrides Sub OnPreRender()

            Dim dbForum As New ForumDB()
            ' Get search results that will be rendered and the total number of search results
            _forumSearchInfoCollection = dbForum.TTTForum_SearchGetResults(_searchObject, _searchTerms, ZforumID, _postsPerPage, _searchPage, LoggedOnUserID, Forum.PortalID, _UserAlias, _StartDate , _EndDate)
			HttpContext.Current.Session("searchresult") = _forumSearchInfoCollection
			If _forumSearchInfoCollection.Count > 0 Then
                _postsCount = CType(_forumSearchInfoCollection(0), ForumSearchInfo).RecordCount
            End If
        End Sub 'OnPreRender

        Private Sub RenderSearch(ByVal wr As HtmlTextWriter) ' Render header row
           ' wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "2")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_SearchResult") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Posted by column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_PostFrom") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Date column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_PostDate") & "&nbsp;")
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr

            ' Work out when to display new images
            Dim lastVisited As DateTime
            Dim user As ForumUser = ForumUser.GetForumUser(LoggedOnUserID)
			If HttpContext.Current.Session("LastThreadView") is nothing then
			HttpContext.Current.Session("LastThreadView") = user.LastThreadView
			end if
			lastVisited = Convert.ToDateTime(HttpContext.Current.Session("LastThreadView"))


            ' Loop round posts found by search, rendering information each one
            Dim document As String = GetFullDocument()
            Dim forumSearchInfo As forumSearchInfo
            For Each forumSearchInfo In _forumSearchInfoCollection
                forumSearchInfo.PortalID = Forum.PortalID
                forumSearchInfo.ModuleID = Forum.ModuleID
                forumSearchInfo.Render(wr, lastVisited, Page, _imageURL, document)
            Next

        End Sub

        Private Sub RenderSearchPaging(ByVal wr As HtmlTextWriter)
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "right")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "50%")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' First, previous, next, last thread hyperlinks
            Dim pageCount As Integer = Math.Floor((_postsCount - 1) / _postsPerPage) + 1
            Dim backwards As Boolean = False
            If _searchPage <> 0 Then backwards = True
            Dim forwards As Boolean = False
            If _searchPage < pageCount - 1 Then forwards = True

            ' < Previous links
            If (backwards) Then
                ' < Previous
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("searchpage={0}", _searchPage), "postid="))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.write("&laquo;")
                wr.Write(GetLanguage("F_Prev"))
                wr.RenderEndTag() ' A
                wr.Write("&nbsp;|&nbsp;")

            End If

            Dim iPage As Integer
            For iPage = 1 To pageCount
                If iPage <> _searchPage + 1 Then
                    wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("searchpage={0}", iPage), "postid="))
                else
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

            If (forwards) Then
                ' Next >
                wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("searchpage={0}", _searchPage + 2), "postid="))
                wr.RenderBeginTag(HtmlTextWriterTag.A)
                wr.Write(GetLanguage("F_Next"))
                wr.write("&raquo;")
                wr.RenderEndTag() ' A
                wr.Write("&nbsp;")
            End If

            ' Close column
            wr.RenderEndTag() ' Td
        End Sub

        Private Sub RenderFooter(ByVal wr As HtmlTextWriter)

           ' wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "4")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTFooter")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' Into which we will put a table that will contain
            wr.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
            wr.RenderBeginTag(HtmlTextWriterTag.Table)

            ' Start row
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "50%")
			wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTFooterText")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)

            ' On the left hand side: Page x of y

            wr.RenderBeginTag(HtmlTextWriterTag.B)

            Dim pageCount As Integer = Math.Floor((_postsCount - 1) / _postsPerPage) + 1
            If (_postsCount = 0) Then
                wr.Write("&nbsp;" + GetLanguage("F_NoPost"))
            Else
			Dim TempString As String
			TempString = Replace(GetLanguage("F_Paging"), "{pagenum}", (_searchPage + 1).ToString)
			TempString = Replace(TempString, "{numpage}", pageCount.ToString)
			wr.Write(TempString)
            End If

            wr.RenderEndTag() ' B
            wr.RenderEndTag() ' Td

            ' And on the right hand side, render search navigation (<< < Previous | Next > >>)
            If (pageCount > 1) Then
                RenderSearchPaging(wr)
            End If
            ' Close out this table and row
            wr.RenderEndTag() ' Tr
            wr.RenderEndTag() ' Table
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr
        End Sub

        Public Overrides Sub Render(ByVal wr As HtmlTextWriter)

            RenderTableBegin(wr, 1, 3)
            'RenderHeader(wr)
            RenderSearch(wr)
            RenderFooter(wr)
            RenderTableEnd(wr)

        End Sub 'Render

    End Class 'ForumSearch

#End Region

#Region "ForumSearchInfo"

    Public Class ForumSearchInfo

        Private _portalID As Integer '<tam:note value=setting for DNN intergration>
        Private ZmoduleID As Integer '<tam:note value=setting for DNN intergration>

        Private _postDate As DateTime
        Private _postID As Integer
        Private _threadID As Integer
        Private _recordCount As Integer ' Should not be moved from this class
        Private _alias As String
        Private _subject As String
        Private _ForumID As String

        Public Sub New()
        End Sub 'New
		
		Public Sub RenderSearch(ByVal wr As HtmlTextWriter, ByVal lastVisited As DateTime, ByVal page As Page, ByVal imageURL As String, ByVal document As String, ByVal TPostID As Integer)


            wr.Write("<option value =""")
			
            wr.Write(TTTUtils.GetURL(document, page, String.Format("threadid={0}&scope=post&postid={1}&forumid={2}", _threadID, _postID, _ForumID), "action=&threadspage=&threadpage=&searchpage=&enddate=&startdate=") + String.Format("#{0}", _threadID))
			'http://localhost/fr.forum.aspx?tabid=549&searchterms=&action=search&useralias=&forumsid=&startdate=%20%2000:00&enddate=20070211%2023:59&mid=1047&searchobject=
			
		    wr.write("""")
			If TPostID = _PostID then
            wr.write(" selected=""selected"" ")
			end if
			wr.write("> ")
			wr.Write("(" & GetLanguage("UO_From") & " : " & _alias & ") ")
            wr.Write(GetLanguage("UO_Object") & " : " & Subject & " ")
			Dim TempDateTime as DateTime
            If loggedOnUserID > 0 Then
               	Dim Currentuser As ForumUser = ForumUser.GetForumUser(loggedOnUserID)
				TempDateTime = PostDate.AddMinutes(GetTimeDiff(Currentuser.TimeZone))
                Else
				TempDateTime = PostDate.AddMinutes(GetTimeDiff(-99))
                End If
            wr.Write("[" & String.Format("{0}", TempDateTime.ToString("dd MMM yy")) & "]")
			wr.Write("</option>")
		End Sub 'Render 
		
        Public Sub Render(ByVal wr As HtmlTextWriter, ByVal lastVisited As DateTime, ByVal page As Page, ByVal imageURL As String, ByVal document As String)
            ' Start row
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)

            ' Render post image
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "25")
            wr.AddAttribute(HtmlTextWriterAttribute.Valign, "middle")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Src, imageURL + "TTT_board_thread.gif")
            wr.AddAttribute(HtmlTextWriterAttribute.Alt, "*")
            wr.RenderBeginTag(HtmlTextWriterTag.Img)
            wr.RenderEndTag() ' Img
            wr.RenderEndTag() ' Td

            ' Post subject with hyperlink
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.Valign, "middle")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "25")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(document, page, String.Format("threadid={0}&scope=post&postid={1}&forumid={2}", _threadID, _postID, _ForumID), "action=&threadspage=&threadpage=&searchpage=&enddate=&startdate=") + String.Format("#{0}", _threadID))
			'http://localhost/fr.forum.aspx?tabid=549&searchterms=&action=search&useralias=&forumsid=&startdate=%20%2000:00&enddate=20070211%2023:59&mid=1047&searchobject=
			wr.RenderBeginTag(HtmlTextWriterTag.A)
            wr.Write(Subject)
            wr.RenderEndTag() ' A

            ' Display new image if this post is new since last time user visited
            If  (loggedOnUserID > 0) AndAlso (lastVisited < PostDate) Then
                wr.AddAttribute(HtmlTextWriterAttribute.Src, imageURL + GetLanguage("N") + "_new.gif")
                wr.AddAttribute(HtmlTextWriterAttribute.Alt, "*")
                wr.RenderBeginTag(HtmlTextWriterTag.Img) ' Img
                wr.RenderEndTag() ' Img
            End If
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' Posted by
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.Write([Alias])
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' Date
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "200")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
			' ViewerId DateTime to see the date time in local time for registered user
			
			Dim TempDateTime as DateTime
            If loggedOnUserID > 0 Then
               	Dim Currentuser As ForumUser = ForumUser.GetForumUser(loggedOnUserID)
				TempDateTime = PostDate.AddMinutes(GetTimeDiff(Currentuser.TimeZone))
                Else
				TempDateTime = PostDate.AddMinutes(GetTimeDiff(-99))
                End If

            wr.Write(String.Format("{0}", TempDateTime.ToString("dd MMM yy")))
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


        Public Property PostDate() As DateTime
            Get
                Return _postDate
            End Get
            Set(ByVal Value As DateTime)
                _postDate = Value
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

        Public Property ThreadID() As Integer
            Get
                Return _threadID
            End Get
            Set(ByVal Value As Integer)
                _threadID = Value
            End Set
        End Property


        Public Property RecordCount() As Integer
            Get
                Return _recordCount
            End Get
            Set(ByVal Value As Integer)
                _recordCount = Value
            End Set
        End Property


        Public Property [Alias]() As String
            Get
                Return _alias
            End Get
            Set(ByVal Value As String)
                _alias = Value
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
        Public Property ForumID() As String
            Get
                Return _ForumID
            End Get
            Set(ByVal Value As String)
                _ForumID = Value
            End Set
        End Property

    End Class 'ForumSearchInfo

#End Region

#Region "ForumSearchInfoCollection"

    Public Class ForumSearchInfoCollection
        Inherits ArrayList

        Public Sub New()
        End Sub 'New


        Public Sub New(ByVal c As ICollection)
            MyBase.New(c)
        End Sub 'New

    End Class 'ForumSearchInfoCollection

#End Region


End Namespace