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
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DotNetZoom.TTTUtils
Imports System.Web.Caching

Namespace DotNetZoom


#Region "ForumStatistics"

    Public Class ForumStatistics

        Private Const ThreadInfoCacheKeyPrefix As String = "ForumStatistics"

        Private _portalID As Integer '<tam:note value=setting for DNN Integration>
        Private ZmoduleID As Integer '<tam:note value=setting for DNN Integration>
        Private Zconfig As ForumConfig

        Private _currentAnonymousUsers As Integer = 0
        Private _activeUsers As New ArrayList() 'top 10 users
        Private _activeModerators As New ArrayList() 'top 10 moderators
        Private _moderationActions As New ArrayList()

        Private _DateCreated As DateTime
        Private _totalUsers As Integer = 0
        Private _totalPosts As Integer = 0
        Private _totalThreads As Integer = 0
        Private _totalModerators As Integer = 0
        Private _totalModeratedPosts As Integer = 0
        Private _totalAnonymousUsers As Integer = 0
        Private _newPostsInPast24Hours As Integer = 0
        Private _newThreadsInPast24Hours As Integer = 0
        Private _newUsersInPast24Hours As Integer = 0
        Private _mostViewsPostId As Integer = 0
        Private _mostViewsSubject As String = ""
        Private _mostActivePostId As Integer = 0
        Private _mostActiveSubject As String = ""
        Private _mostReadPostId As Integer = 0
        Private _mostReadPostSubject As String = ""
        Private _mostActiveUser As String = ""
        Private _mostActiveUserID As Integer = 0
        Private _newestUser As String = ""
        Private _newestUserID As Integer = 0

        Public Sub New()
        End Sub 'New

        Private Sub New(ByVal PortalID As Integer, ByVal ModuleID As Integer)
            _portalID = PortalID
            ZmoduleID = ModuleID
            Zconfig = ForumConfig.GetForumConfig(ZmoduleID)

            Dim dbForum As New ForumDB()
            Dim dr As SqlDataReader = dbForum.TTTForum_Statistics_Get(_portalID, ZmoduleID, Zconfig.StatsUpdateInterval)
            dr.Read()
            Try
                _DateCreated = ConvertDateTime(dr("DateCreated"))
                _totalUsers = ConvertInteger(dr("TotalUsers"))
                _totalPosts = ConvertInteger(dr("TotalPosts"))
                _totalModerators = ConvertInteger(dr("TotalModerators"))
                _totalModeratedPosts = ConvertInteger(dr("TotalModeratedPosts"))
                _totalThreads = ConvertInteger(dr("TotalTopics"))
                '_totalAnonymousUsers = ConvertInteger(dr("TotalAnonymousUsers"))
                _newPostsInPast24Hours = ConvertInteger(dr("NewPostsInPast24Hours"))
                _newThreadsInPast24Hours = ConvertInteger(dr("NewThreadsInPast24Hours"))
                '_newUsersInPast24Hours = ConvertInteger(dr("NewUsersInPast24Hours"))
                _mostViewsPostId = ConvertInteger(dr("MostViewsPostID"))
                _mostViewsSubject = ConvertString(dr("MostViewsSubject"))
                _mostActivePostId = ConvertInteger(dr("MostActivePostID"))
                _mostActiveSubject = ConvertString(dr("MostActiveSubject"))
                _mostReadPostId = ConvertInteger(dr("MostReadPostID"))
                _mostReadPostSubject = ConvertString(dr("MostReadSubject"))
                '_mostActiveUser = ConvertString(dr("MostActiveUser"))
                _mostActiveUserID = ConvertInteger(dr("MostActiveUserID"))
                '_newestUser = ConvertString(dr("NewestUser"))
                '_newestUserID = ConvertInteger(dr("NewestUserID"))
            Catch Exc As System.Exception
            End Try

            dr.NextResult()
            Try
                While dr.Read
                    Dim Zuser As ForumUser = ForumUser.GetForumUser(dr("UserID"))
                    _activeUsers.Add(Zuser)
                End While
            Catch Exc As System.Exception
            End Try

            dr.NextResult()
            Try
                While dr.Read
                    Dim Zuser As ForumUser = ForumUser.GetForumUser(dr("UserID"))
                    Zuser.PostsModerated = ConvertInteger(dr("PostsModerated"))
                    Zuser.IsModerator = True
                    _activeModerators.Add(Zuser)
                End While
            Catch Exc As System.Exception
            End Try

            dr.NextResult()
            If dr.Read Then ' for moderateAction
            End If

            dr.Close()

        End Sub



        Public Shared Function GetStats(ByVal PortalID As Integer, ByVal ModuleID As Integer) As ForumStatistics

            ' Grab reference to the applicationstate object
			' Need to change Forum Cashing
            
			Dim TempKey as String = GetDBName & "ForumStatistics" & PortalID.ToString & "-" & ModuleID.ToString
			Dim context As HttpContext = HttpContext.Current
			Dim forumStats As ForumStatistics 			
            If Context.Cache(TempKey) Is Nothing Then
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' If this object has not been instantiated yet, we need to grab it
            forumStats = New ForumStatistics(PortalID, ModuleID)
            Context.Cache.Insert(TempKey, forumStats, DotNetZoom.CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, ModuleID), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
			else
			forumStats = CType(Context.Cache(TempKey), ForumStatistics)
	        End If
            Return forumStats

        End Function

        Public Shared Sub ResetStats(ByVal PortalID As Integer, ByVal ModuleID As Integer)
			Dim TempKey as String = GetDBName & "ForumStatistics" & PortalID.ToString & "-" & ModuleID.ToString
			Dim context As HttpContext = HttpContext.Current
			context.Cache.Remove(TempKey)
        End Sub
		
	
        Public ReadOnly Property Config() As ForumConfig
            Get
                Return Zconfig
            End Get
        End Property

        Public ReadOnly Property DateCreated() As DateTime
            Get
                Return _DateCreated
            End Get
        End Property

        Public Property ActiveUsers() As ArrayList
            Get
                Return _activeUsers
            End Get
            Set(ByVal Value As ArrayList)
                _activeUsers = Value
            End Set
        End Property

        Public Property ActiveModerators() As ArrayList
            Get
                Return _activeModerators
            End Get
            Set(ByVal Value As ArrayList)
                _activeModerators = Value
            End Set
        End Property

        Public Property ModerationActions() As ArrayList
            Get
                Return _moderationActions
            End Get
            Set(ByVal Value As ArrayList)
                _moderationActions = Value
            End Set
        End Property

        Public Property CurrentAnonymousUsers() As Integer
            Get
                Return _currentAnonymousUsers
            End Get
            Set(ByVal Value As Integer)
                _currentAnonymousUsers = Value
            End Set
        End Property

        Public Property TotalUsers() As Integer
            Get
                Return _totalUsers
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then
                    _totalUsers = 0
                Else
                    _totalUsers = Value
                End If
            End Set
        End Property

        Public Property TotalAnonymousUsers() As Integer
            Get
                Return _totalAnonymousUsers
            End Get
            Set(ByVal Value As Integer)
                _totalAnonymousUsers = Value
            End Set
        End Property

        Public Property TotalModerators() As Integer
            Get
                Return _totalModerators
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then
                    _totalModerators = 0
                Else
                    _totalModerators = Value
                End If
            End Set
        End Property

        Public Property TotalModeratedPosts() As Integer
            Get
                Return _totalModeratedPosts
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then
                    _totalModeratedPosts = 0
                Else
                    _totalModeratedPosts = Value
                End If
            End Set
        End Property

        Public Property TotalPosts() As Integer
            Get
                Return _totalPosts
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then
                    _totalPosts = 0
                Else
                    _totalPosts = Value
                End If
            End Set
        End Property

        Public Property TotalThreads() As Integer
            Get
                Return _totalThreads
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then
                    _totalThreads = 0
                Else
                    _totalThreads = Value
                End If
            End Set
        End Property

        Public Property NewPostsInPast24Hours() As Integer
            Get
                Return _newPostsInPast24Hours
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then
                    _newPostsInPast24Hours = 0
                Else
                    _newPostsInPast24Hours = Value
                End If
            End Set
        End Property

        'Public Property NewUsersInPast24Hours() As Integer
        '    Get
        '        Return _newUsersInPast24Hours
        '    End Get
        '    Set(ByVal Value As Integer)
        '        If Value < 0 Then
        '            _newUsersInPast24Hours = 0
        '        Else
        '            _newUsersInPast24Hours = Value
        '        End If
        '    End Set
        'End Property

        Public Property NewThreadsInPast24Hours() As Integer
            Get
                Return _newThreadsInPast24Hours
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then
                    _newThreadsInPast24Hours = 0
                Else
                    _newThreadsInPast24Hours = Value
                End If
            End Set
        End Property

        Public Property MostViewsPostID() As Integer
            Get
                Return _mostViewsPostId
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then
                    _mostViewsPostId = 0
                Else
                    _mostViewsPostId = Value
                End If
            End Set
        End Property

        Public Property MostViewsSubject() As String
            Get
                Return _mostViewsSubject
            End Get
            Set(ByVal Value As String)
                _mostViewsSubject = Value
            End Set
        End Property

        Public Property MostActivePostID() As Integer
            Get
                Return _mostActivePostId
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then
                    _mostActivePostId = 0
                Else
                    _mostActivePostId = Value
                End If
            End Set
        End Property

        Public Property MostActiveSubject() As String
            Get
                Return _mostActiveSubject
            End Get
            Set(ByVal Value As String)
                _mostActiveSubject = Value
            End Set
        End Property

        Public Property MostReadPostID() As Integer
            Get
                Return _mostReadPostId
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then
                    _mostReadPostId = 0
                Else
                    _mostReadPostId = Value
                End If
            End Set
        End Property

        Public Property MostReadPostSubject() As String
            Get
                Return _mostReadPostSubject
            End Get
            Set(ByVal Value As String)
                _mostReadPostSubject = Value
            End Set
        End Property

        Public Property MostActiveUser() As String
            Get
                Return _mostActiveUser
            End Get
            Set(ByVal Value As String)
                _mostActiveUser = Value
            End Set
        End Property


        Public Property MostActiveUserID() As Integer
            Get
                Return _mostActiveUserID
            End Get
            Set(ByVal Value As Integer)
                _mostActiveUserID = Value
            End Set
        End Property

        'Public Property NewestUser() As String
        '    Get
        '        Return _newestUser
        '    End Get
        '    Set(ByVal Value As String)
        '        _newestUser = Value
        '    End Set
        'End Property

        'Public Property NewestUserID() As Integer
        '    Get
        '        Return _newestUserID
        '    End Get
        '    Set(ByVal Value As Integer)
        '        _newestUserID = Value
        '    End Set
        'End Property


    End Class 'ForumStatistics

#End Region


End Namespace