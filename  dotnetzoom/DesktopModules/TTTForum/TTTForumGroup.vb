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
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DotNetZoom.TTTUtils
Imports System.Web.Caching

Namespace DotNetZoom

#Region "ForumGroup"

    Public Class ForumGroup
        Inherits ForumObject

        Private _forumGroupInfoCollection As ForumGroupInfoCollection
        Private _groupsCount As Integer = 0
        Private _forumsCount As Integer = 0
        Private _portalID As Integer
        Private ZmoduleID As Integer

        Public Sub New(ByVal forum As Forum, ByVal PortalID As Integer, ByVal ModuleID As Integer)
            MyBase.New(forum)
            _portalID = PortalID
            ZmoduleID = ModuleID
        End Sub 'New

        Public Overrides Sub CreateChildControls()

        End Sub 'CreateChildControls

        Public Overrides Sub OnPreRender()
            Dim dbForum As New ForumDB()
            ' Get groups
            _forumGroupInfoCollection = New ForumGroupInfoCollection(_portalID, ZmoduleID)

        End Sub 'OnPreRender

        Private Sub RenderHeader(ByVal wr As HtmlTextWriter)

        End Sub 'RenderHeader

        Private Sub RenderGroups(ByVal wr As HtmlTextWriter)

            ' Render header row
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.RenderEndTag() ' Td

            ' name column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Style, "white-space: nowrap;")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Group") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Threads column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Thread") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Posts column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_Post") & "&nbsp;")
            wr.RenderEndTag() ' Td

            ' Last Post column
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Style, "white-space: nowrap;")
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "200")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.Write("&nbsp;" & GetLanguage("F_LPost") & "&nbsp;")
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr

            ' Loop round thread items, rendering information about individual groups
            
            Dim document As String = GetFullDocument()
            Dim imageURL As String = ForumConfig.SkinImageFolder()
            

            Dim lastVisited As DateTime
            Dim user As ForumUser = ForumUser.GetForumUser(LoggedOnUserID)

			If HttpContext.Current.Session("LastThreadView") is nothing then
			HttpContext.Current.Session("LastThreadView") = user.LastThreadView
			end if
			lastVisited = Convert.ToDateTime(HttpContext.Current.Session("LastThreadView"))

            _groupsCount = _forumGroupInfoCollection.Count
            Dim _forumGroupInfo As ForumGroupInfo
            Dim dbForum As New ForumDB()

            For Each _forumGroupInfo In _forumGroupInfoCollection
                '_forumGroupInfo.PortalID = Forum.PortalID
                '_forumGroupInfo.ModuleID = Forum.ModuleID
                _forumGroupInfo.Render(wr)

                Dim _forumGroupID As Integer = _forumGroupInfo.ForumGroupID
                Dim _forumItemCollection As ForumItemCollection = New ForumItemCollection(_forumGroupID)
                Dim _forumItemInfo As ForumItemInfo

                For Each _forumItemInfo In _forumItemCollection
                    Dim _displayForum As Boolean = False
                    If _forumItemInfo.IsActive Then
                        If _forumItemInfo.IsPrivate Then
                            If PortalSecurity.IsInRoles(_forumItemInfo.AuthorizedRoles) = True Then
                                _displayForum = True
                            End If
                        Else
                            _displayForum = True
                        End If
                    End If
                    If _displayForum Then
                        _forumsCount += 1 'for statistics                    
                        _forumItemInfo.Render(wr, lastVisited, Page, imageURL, LoggedOnUserID, document)
                    End If
                Next
            Next
            'record log user last view
            If Page.Request.IsAuthenticated = True Then
                ForumUserDB.TTTForum_UpdateUserForumsView(LoggedOnUserID)
            End If

        End Sub 'RenderGroups


        Private Sub RenderFooter(ByVal wr As HtmlTextWriter)

            ' Start the footer row
            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "5")
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTFooter")
            wr.RenderBeginTag(HtmlTextWriterTag.Td) ' Into which we will put a table that will contain

            wr.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
            wr.RenderBeginTag(HtmlTextWriterTag.Table)

            ' On the left hand side: Forum statistics

            wr.RenderBeginTag(HtmlTextWriterTag.Tr)
            wr.AddAttribute(HtmlTextWriterAttribute.Align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
			wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTFooterText")
            wr.RenderBeginTag(HtmlTextWriterTag.Td)
            wr.RenderBeginTag(HtmlTextWriterTag.B)
 
          	Dim strFooter As String
			strFooter = replace(GetLanguage("F_CountForumGroup"), "{forumscount}", _forumsCount.ToString)
			strFooter = replace(strFooter, "{groupscount}", _groupsCount.ToString)
		
            wr.Write("&nbsp;")
            wr.Write(strFooter)
            wr.RenderEndTag() ' B
            wr.RenderEndTag() ' Td

            ' Close out this table and row
            wr.RenderEndTag() ' Tr
            wr.RenderEndTag() ' Table
            wr.RenderEndTag() ' Td
            wr.RenderEndTag() ' Tr

        End Sub

        Public Overrides Sub Render(ByVal wr As HtmlTextWriter)

            RenderTableBegin(wr, 1, 2)
            RenderGroups(wr)
            RenderFooter(wr)
            RenderTableEnd(wr) '

        End Sub 'Render

    End Class

#End Region


    Public Class ForumGroupInfo
#Region "ForumGroupInfo"

        Private Const ForumGroupInfoCacheKeyPrefix As String = "ForumGroupInfo"

        Private _forumGroupID As Integer
        Private _portalID As Integer
        Private ZmoduleID As Integer
        Private _name As String
        Private _createdDate As DateTime
        Private _createdByUser As Integer
        Private _creator As ForumUser
        Private _sortOrder As Integer
        Private _forumCount As Integer

        Public Sub New()
        End Sub 'New

        Private Sub New(ByVal ForumGroupID As Integer)
            Dim dbForum As New ForumDB()
            Dim dr As SqlDataReader = dbForum.TTTForum_GetSingleGroup(ForumGroupID)
            If dr.Read() Then
                PopulateInfo(dr)
            End If
            dr.Close()

        End Sub

        Private Sub New(ByVal DataReader As SqlDataReader)

            PopulateInfo(DataReader)

        End Sub

        Public Shared Function PopulateGroupInfo(ByVal DataReader As SqlDataReader) As ForumGroupInfo

            Dim groupInfo As ForumGroupInfo = New ForumGroupInfo(DataReader)
            Return groupInfo

        End Function

		
        Public Shared Function GetGroupInfo(ByVal ForumGroupID As Integer) As ForumGroupInfo
            ' Grab reference to the applicationstate object
			' Need to change Forum Cashing
			Dim TempKey as String = GetDBName & ForumGroupInfoCacheKeyPrefix & CStr(ForumGroupID)
			Dim context As HttpContext = HttpContext.Current
            Dim groupInfo As ForumGroupInfo 

		
            If Context.Cache(TempKey) Is Nothing Then
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' If this object has not been instantiated yet, we need to grab it
            groupInfo = New ForumGroupInfo(ForumGroupID)
			Context.Cache.Insert(TempKey, groupInfo, DotNetZoom.CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, groupInfo.ModuleID), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
			Else
            groupInfo = CType(Context.Cache(TempKey), ForumGroupInfo)
            End If
            Return groupInfo

        End Function


        Public Shared Sub ResetGroupInfo(ByVal ForumGroupID As Integer)
			Dim TempKey as String = GetDBName & ForumGroupInfoCacheKeyPrefix & CStr(ForumGroupID)
			Dim context As HttpContext = HttpContext.Current
			context.Cache.Remove(TempKey)
        End Sub

		
		
		
        Private Sub PopulateInfo(ByVal dr As SqlDataReader)
            Dim _groupInfo As New ForumGroupInfo()

            ' Forum specifics
            _forumGroupID = ConvertInteger(dr("ForumGroupID"))

            _name = ConvertString(dr("Name"))
            _sortOrder = ConvertInteger(dr("SortOrder"))
            _portalID = ConvertInteger(dr("PortalID"))
            ZmoduleID = ConvertInteger(dr("ModuleID"))
            _createdDate = ConvertDateTime(dr("CreatedDate"))
            _createdByUser = ConvertInteger(dr("CreatedByUser"))
            _forumCount = ConvertInteger(dr("ForumCount"))

        End Sub      'PopulateForumGr

        'Public Sub Render(ByVal wr As HtmlTextWriter, ByVal GroupsPerPage As Integer, ByVal lastVisited As DateTime, ByVal page As Page, ByVal images As String, ByVal document As String)
        Public Sub Render(ByVal wr As HtmlTextWriter)

            ' Start row
            wr.RenderBeginTag(HtmlTextWriterTag.Tr) '<tr
            wr.AddAttribute(HtmlTextWriterAttribute.Class, "TTTAltHeader")
            wr.AddAttribute(HtmlTextWriterAttribute.Valign, "middle")
            wr.AddAttribute(HtmlTextWriterAttribute.align, "left")
            wr.AddAttribute(HtmlTextWriterAttribute.Height, "28")
            wr.AddAttribute(HtmlTextWriterAttribute.Colspan, "5")

            ' group name  with link
            wr.RenderBeginTag(HtmlTextWriterTag.Td) '<td            
            wr.Write("&nbsp;" & _name)
            wr.RenderEndTag() ' Td

            ' End row
            wr.RenderEndTag() ' Tr

        End Sub 'Render 

        Public Property ForumGroupID() As Integer
            Get
                Return _forumGroupID
            End Get
            Set(ByVal Value As Integer)
                _forumGroupID = Value
            End Set
        End Property

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

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

        Public Property CreatedDate() As DateTime
            Get
                Return _createdDate
            End Get
            Set(ByVal Value As DateTime)
                _createdDate = Value
            End Set
        End Property

        Public Property CreatedByUser() As Integer
            Get
                Return _createdByUser
            End Get
            Set(ByVal Value As Integer)
                _createdByUser = Value
            End Set
        End Property

        Public ReadOnly Property Creator() As ForumUser
            Get
                _creator = ForumUser.GetForumUser(_createdByUser)
                Return _creator
            End Get

        End Property
        Public Property SortOrder() As Integer
            Get
                Return _sortOrder
            End Get
            Set(ByVal Value As Integer)
                _sortOrder = Value
            End Set
        End Property

        Public Property ForumCount() As Integer
            Get
                Return _forumCount
            End Get
            Set(ByVal Value As Integer)
                _forumCount = Value
            End Set
        End Property

#End Region
    End Class 'ForumGroupInfo


#Region "ForumGroupInfoCollection"

    Public Class ForumGroupInfoCollection
        Inherits ArrayList

        Private _portalID As Integer
        Private ZmoduleID As Integer
       
        Public Sub New()

        End Sub

        Public Sub New(ByVal PortalID As Integer, ByVal ModuleID As Integer)

            _portalID = PortalID
            ZmoduleID = ModuleID

            PopulateCollection()
            'Return Me
        End Sub 'New

        Private Sub PopulateCollection()
            Dim dbForum As New ForumDB()
            Dim dr As SqlDataReader = dbForum.TTTForum_GetGroups(_portalID, ZmoduleID)
            While dr.Read
                Dim groupInfo As ForumGroupInfo = ForumGroupInfo.PopulateGroupInfo(dr)
                Me.Add(groupInfo)
            End While
            dr.Close()

        End Sub

        Public Sub New(ByVal c As ICollection)
            MyBase.New(c)
        End Sub 'New

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

    End Class 'ForumGroupInfoCollection

#End Region


End Namespace