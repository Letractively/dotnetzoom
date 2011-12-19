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

#Region "ForumModerate"

    Public Class ForumModerate
        Inherits ForumObject

        Private _forumModerateCollection As ForumModerateCollection
        Private ZuserID As Integer
        Private _postsToModerate As Integer
        Private ZforumConfig As ForumConfig
        Private _imageURL As String

        Public Sub New(ByVal forum As Forum)

            MyBase.New(forum)
            ZuserID = LoggedOnUserID
            ZforumConfig = ForumConfig.GetForumConfig(forum.ModuleID)

        End Sub 'New

        Public Overrides Sub CreateChildControls()

        End Sub

        Public Overrides Sub OnPreRender()

            _forumModerateCollection = New ForumModerateCollection(ZuserID)

        End Sub

        Private Sub RenderForums(ByVal wr As HtmlTextWriter)
            ' Render header row
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "25")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "50")
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.RenderEndTag() ' Td

            ' name column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Group") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Posts to moderate column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Post") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Last Post column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.style, "white-space: nowrap;")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "200")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Created") & "&nbsp;")
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr

            Dim document As String = GetFullDocument()
			' PUT IN SKIN IMAGEFOLDER
            _imageURL = ForumConfig.SkinImageFolder()
            Dim _forumModerateInfo As ForumModerateInfo

            For Each _forumModerateInfo In _forumModerateCollection
                _forumModerateInfo.Render(wr, Page, _imageURL, ZuserID, document)
            Next

        End Sub

        Public Overrides Sub Render(ByVal wr As HtmlTextWriter)

            RenderTableBegin(wr, 1, 2)
            RenderForums(wr)
            RenderTableEnd(wr)

        End Sub

    End Class 'ForumItem

#End Region

#Region "ForumModerateInfo"

    Public Class ForumModerateInfo

        Private Const ForumModerateInfoCacheKeyPrefix As String = "ForumModerateInfo"
        Private Const _profilePage As Integer = TTT_ForumDispatch.ForumDesktopType.ForumProfile

        Private _portalID As Integer
        Private ZmoduleID As Integer
        Private ZforumID As Integer
        Private _ForumGroupID As Integer
        Private _forumGroup As ForumGroupInfo
        Private _Name As String
        Private _Description As String
        Private _CreatedDate As DateTime
        Private _IsModerated As Boolean
        Private _SortOrder As Integer
        Private _IsActive As Boolean
        Private _PostsToModerate As Integer
        Private ZforumConfig As ForumConfig

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

        Public Shared Function PopulateForumInfo(ByVal DataReader As SqlDataReader) As ForumModerateInfo

            Dim forumModerate As ForumModerateInfo = New ForumModerateInfo(DataReader)
            Return forumModerate

        End Function

 
        Public Shared Function GetForumModerateInfo(ByVal ForumID As Integer) As ForumModerateInfo

            ' Grab reference to the applicationstate object
			' Need to change Forum Cashing
            
			Dim TempKey as String = GetDBName & ForumModerateInfoCacheKeyPrefix & CStr(ForumID)
			Dim context As HttpContext = HttpContext.Current
			Dim forumModerateInfo As forumModerateInfo 
            If Context.Cache(TempKey) Is Nothing Then
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' If this object has not been instantiated yet, we need to grab it
            forumModerateInfo = New forumModerateInfo(ForumID)
			Context.Cache.Insert(TempKey, forumModerateInfo, DotNetZoom.CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, forumModerateInfo.ModuleID), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
			Else
			forumModerateInfo = CType(Context.Cache(TempKey), forumModerateInfo)
            End If
            Return forumModerateInfo

        End Function

        Public Shared Sub ResetForumModerateInfo(ByVal ForumID As Integer)
			Dim TempKey as String = GetDBName & ForumModerateInfoCacheKeyPrefix & CStr(ForumID)
			Dim context As HttpContext = HttpContext.Current
				context.Cache.Remove(TempKey)
        End Sub
		

        Private Sub PopulateInfo(ByVal dr As SqlDataReader)

            ZforumID = ConvertInteger(dr("ForumID"))
            _ForumGroupID = ConvertInteger(dr("ForumGroupID"))
            _portalID = ConvertInteger(dr("PortalID"))
            ZmoduleID = ConvertInteger(dr("ModuleID"))
            _Name = ConvertString(dr("Name"))
            _Description = ConvertString(dr("Description"))
            _CreatedDate = ConvertDateTime(dr("CreatedDate"))
            _IsModerated = ConvertBoolean(dr("IsModerated"))
            _SortOrder = ConvertInteger(dr("SortOrder"))
            _IsActive = ConvertBoolean(dr("IsActive"))
            _PostsToModerate = ConvertInteger(dr("PostsToModerate"))

            ZforumConfig = ForumConfig.GetForumConfig(ZmoduleID)

        End Sub      'PopulateInfo

        Public Sub Render(ByVal wr As HtmlTextWriter, ByVal Page As Page, ByVal imageURL As String, ByVal UserID As Integer, ByVal document As String)

            ' New row
            wr.RenderBeginTag(HtmlTextWriterTag.Tr) '<tr
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.Valign, "middle")
            wr.AddAttribute(HtmlTextWriterAttribute.align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "25")

            wr.RenderBeginTag(HtmlTextWriterTag.Td) '<td
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "32")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "32")
            wr.AddAttribute(HtmlTextWriterAttribute.Src, glbPath & "images/1x1.gif")
            wr.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px; background: url('" & imageURL & "forum.gif') no-repeat; background-position: -16px -128px;")
            wr.AddAttribute(HtmlTextWriterAttribute.Alt, "*")

            wr.RenderBeginTag(HtmlTextWriterTag.Img)
            wr.RenderEndTag() ' Img
            wr.RenderEndTag() ' </td>

            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRow")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.RenderBeginTag(HtmlTextWriterTag.Td) '<td
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTSubHeader")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' FormatFriendlyURL(_PortalSettings.activetab.FriendlyTabName,  _portalSettings.ActiveTab.ssl, _PortalSettings.activetab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, String.Format("edit=control&mid={0}&forumid={1}&editpage={2}", ZmoduleID.ToString, ZforumID.ToString, CType(TTT_EditForum.ForumEditType.ForumModerate, Integer).ToString))

            wr.AddAttribute(HtmlTextWriterAttribute.Href, TTTUtils.GetURL(GetFullDocument(), Page, String.Format("edit=control&mid={0}&forumid={1}&editpage={2}&tabid={3}", ZmoduleID.ToString, ZforumID.ToString, CType(TTT_EditForum.ForumEditType.ForumModerate, Integer).ToString, _portalSettings.ActiveTab.TabId.ToString), "scope=&forumpage="))
            wr.RenderBeginTag(HtmlTextWriterTag.A) '<A
            Dim forumName As ForumText = New ForumText(_Name, ZforumConfig)
            
            wr.Write(forumName.ProcessSingleLine())
            wr.RenderEndTag() ' A> 
            wr.RenderEndTag() ' Span

            ' Display forum description
            If Len(_Description) > 0 Then
                wr.Write("<br>")
                wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
                wr.RenderBeginTag(HtmlTextWriterTag.Span) 'Span
                wr.Write(_Description)
                wr.RenderEndTag() ' Span
            End If
            wr.RenderEndTag() ' Td

            ' Posts to moderate count
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
            wr.Write(_PostsToModerate.ToString)
            wr.RenderEndTag() ' Span
            wr.RenderEndTag() ' Td

            ' Created Date
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTRowHighlight")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTNormal")
            wr.RenderBeginTag(HtmlTextWriterTag.Span)
			
			' ViewerId DateTime to see the date time in local time for registered user
			

			Dim TempDateTime as DateTime
            If userID > 0 Then
               	Dim Currentuser As ForumUser = ForumUser.GetForumUser(userID)
				TempDateTime = _CreatedDate.AddMinutes(GetTimeDiff(Currentuser.TimeZone))
                Else
				TempDateTime = _CreatedDate.AddMinutes(GetTimeDiff(-99))
                End If
			
			
			
            wr.Write(TempDateTime.ToString)
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

        Public Property PostsToModerate() As Integer
            Get
                Return _PostsToModerate
            End Get
            Set(ByVal Value As Integer)
                _PostsToModerate = Value
            End Set
        End Property

    End Class 'ForumItemInfo

#End Region

#Region "ForumModerateCollection"

    Public Class ForumModerateCollection
        Inherits ArrayList

        Private ZuserID As Integer

        Public Sub New()

        End Sub

        Public Sub New(ByVal UserID As Integer)

            ZuserID = UserID
            PopulateCollection()
        End Sub 'New

        Private Sub PopulateCollection()
            Dim dbForum As New ForumDB()
            Dim dr As SqlDataReader = dbForum.TTTForum_Moderate_GetForums(ZuserID)
            While dr.Read
                ' Dim forumModerateInfo As forumModerateInfo = forumModerateInfo.PopulateForumInfo(dr)
                Me.Add(forumModerateInfo.PopulateForumInfo(dr))
            End While

        End Sub

        Public Sub New(ByVal c As ICollection)
            MyBase.New(c)
        End Sub 'New

        Public Property UserID() As Integer
            Get
                Return ZuserID
            End Get
            Set(ByVal Value As Integer)
                ZuserID = Value
            End Set
        End Property

    End Class 'ForumItemCollection

#End Region

End Namespace