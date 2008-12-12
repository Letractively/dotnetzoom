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

Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports System.Web.Caching

Namespace DotNetZoom

    Public Class ForumUser
#Region "ForumUser"

        Private Const ForumUserCacheKeyPrefix As String = "ForumUser"
        Private ZuserID As Integer
        Private _forumUserID As Integer
        Private _alias As String
        Private _useRichText As Boolean
        Private _name As String
        Private _fullname As String
        Private _firstname As String
        Private _lastname As String
        Private _email As String
        Private _userAvatar As Boolean
        Private _avatar As String
        Private _postCount As Integer
        Private _URL As String
        Private _Signature As String
        Private _TimeZone As Integer
        Private _Occupation As String
        Private _interests As String
        Private _MSN As String
        Private _Yahoo As String
        Private _AIM As String
        Private _ICQ As String
        Private _Skin As String
        Private _LastActivity As DateTime
        Private _LastForumsView As DateTime
        Private _LastThreadView As DateTime
        Private _FlatView As Boolean
        Private _IsTrusted As Boolean
        Private _EnableThreadTracking As Boolean
        Private _EnableDisplayUnreadThreadsOnly As Boolean
        Private _EnableDisplayInMemberList As Boolean
        Private _EnablePrivateMessages As Boolean
        Private _EnableOnlineStatus As Boolean
        Private _IsModerator As Boolean
        Private _PostsModerated As Integer

        Private _AvatarURL As String

        Public Sub New()
        End Sub 'New

        Private Sub New(ByVal UserID As Integer)

            ZuserID = UserID
			CheckUser(ZuserID)
            ' Grab settings from the database
            Dim dbForumUser As New ForumUserDB()

            Dim dr As SqlDataReader = dbForumUser.TTTForum_GetUser(ZuserID)            

            If dr.Read Then
                _alias = ConvertString(dr("Alias"))
                _useRichText = ConvertBoolean(dr("UseRichText"))
                _name = ConvertString(dr("UserName"))
                _fullname = ConvertString(dr("FullName"))
                _firstname = ConvertString(dr("FirstName"))
                _lastname = ConvertString(dr("LastName"))
                _email = ConvertString(dr("Email"))
                _userAvatar = ConvertBoolean(dr("UserAvatar"))
                _avatar = ConvertString(dr("Avatar"))
                _postCount = ConvertInteger(dr("PostCount"))
                _URL = ConvertString(dr("URL"))
                _Signature = ConvertString(dr("Signature"))
                _TimeZone = ConvertInteger(dr("TimeZone"))
                _Occupation = ConvertString(dr("Occupation"))
                _interests = ConvertString(dr("interests"))
                _MSN = ConvertString(dr("MSN"))
                _Yahoo = ConvertString(dr("Yahoo"))
                _AIM = ConvertString(dr("AIM"))
                _ICQ = ConvertString(dr("ICQ"))
                _Skin = ConvertString(dr("Skin"))
                _LastActivity = ConvertDateTime(dr("LastActivity"))
                _LastForumsView = ConvertDateTime(dr("LastForumsView"))
                _LastThreadView = ConvertDateTime(dr("LastThreadView"))
                _FlatView = ConvertBoolean(dr("FlatView"))
                _IsTrusted = ConvertBoolean(dr("IsTrusted"))
                _EnableThreadTracking = ConvertBoolean(dr("EnableThreadTracking"))
                _EnableDisplayUnreadThreadsOnly = ConvertBoolean(dr("EnableDisplayUnreadThreadsOnly"))
                _EnableDisplayInMemberList = ConvertBoolean(dr("EnableDisplayInMemberList"))
                _EnablePrivateMessages = ConvertBoolean(dr("EnablePrivateMessages"))
                _EnableOnlineStatus = ConvertBoolean(dr("EnableOnlineStatus"))

            End If
            dr.Close()

        End Sub

		
		Private Sub AddNewUser(ByVal LoggedOnId As Integer)
		    Dim objUsers As New UsersDB()
			Dim dbForumUser As New ForumUserDB()
       		Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim drDNN As SqlDataReader = objUsers.GetSingleUser(_portalSettings.PortalId, LoggedOnID)
            drDNN.Read()
            Dim userName As String
            If Not drDNN("UserName") Is DBNull.Value Then
                userName = ConvertString(drDNN("UserName"))
            End If
            drDNN.Close()
			Dim TimeZone As Integer
			TimeZone =  _portalSettings.TimeZone
            dbForumUser.TTTForum_UserCreateUpdateDelete(LoggedOnID, userName, True, False, "", "", "", TimeZone, "", "", "", "", "", "", "", False, True, False, True, True, True, 0)
		end sub
		
        Private Sub CheckUser(ByVal LoggedOnID As Integer)
           Dim dbForumUser As New ForumUserDB()
            Dim dr As SqlDataReader = dbForumUser.TTTForum_GetUser(LoggedOnID)
            If dr.Read Then
                Try
                    Dim forumUserID As Integer = TTTUtils.ConvertInteger(dr("ForumUserID"))
                    If forumUserID < 1 Then
                        AddNewUser(LoggedOnID)
                    End If
                Catch Exc As System.Exception
                    'Add forum user if not exists
                    AddNewUser(LoggedOnID)
				End Try
            End If
            dr.Close()
        End Sub
		
        Public Function UpdateForumUser() As String
		ResetForumUser(ZuserID)
            Dim dbForumUser As New ForumUserDB()
            Try
                dbForumUser.TTTForum_UserCreateUpdateDelete( _
                    ZuserID, _
                    _alias, _
                    _useRichText, _
                    _userAvatar, _
                    _avatar, _
                    _URL, _
                    _Signature, _
                    _TimeZone, _
                    _Occupation, _
                    _interests, _
                    _MSN, _
                    _Yahoo, _
                    _AIM, _
                    _ICQ, _
                    _Skin, _
                    _IsTrusted, _
                    _EnableThreadTracking, _
                    _EnableDisplayUnreadThreadsOnly, _
                    _EnableDisplayInMemberList, _
                    _EnablePrivateMessages, _
                    _EnableOnlineStatus, _
                    1)
            Catch Exc As System.Exception
                Return Exc.Message
            End Try

            Return ""
        End Function


        Public Shared Function GetForumUser(ByVal UserID As Integer) As ForumUser

            ' Grab reference to the applicationstate object
			' Need to change Forum Cashing
            
			Dim TempKey as String = GetDBName & ForumUserCacheKeyPrefix & CStr(UserID)
			Dim context As HttpContext = HttpContext.Current
			Dim user As ForumUser 			
            If Context.Cache(TempKey) Is Nothing Then
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' If this object has not been instantiated yet, we need to grab it
            user = New ForumUser(UserID)
			Context.Cache.Insert(TempKey, user, DotNetZoom.CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), Caching.CacheItemPriority.low, nothing)
			else
			user = CType(Context.Cache(TempKey), ForumUser)
            End If
            Return user

        End Function

        Public Shared Sub ResetForumUser(ByVal UserID As Integer)
			Dim TempKey as String = GetDBName & ForumUserCacheKeyPrefix & CStr(UserID)
			Dim context As HttpContext = HttpContext.Current
			context.Cache.Remove(TempKey)
        End Sub


#Region "User - Public Properties"

        Public Property UserID() As Integer
            Get
                Return ZuserID
            End Get
            Set(ByVal Value As Integer)
                ZuserID = Value
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

        Public Property FullName() As String
            Get
                Return _fullname
            End Get
            Set(ByVal Value As String)
                _fullname = Value
            End Set
        End Property

        Public Property FirstName() As String
            Get
                Return _firstname
            End Get
            Set(ByVal Value As String)
                _firstname = Value
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return _lastname
            End Get
            Set(ByVal Value As String)
                _lastname = Value
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

        Public Property UseRichText() As Boolean
            Get
                Return _useRichText
            End Get
            Set(ByVal Value As Boolean)
                _useRichText = Value
            End Set
        End Property

        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal Value As String)
                _email = Value
            End Set
        End Property

        Public Property UserAvatar() As Boolean
            Get
                Return _userAvatar
            End Get
            Set(ByVal Value As Boolean)
                _userAvatar = Value
            End Set
        End Property

        Public Property Avatar() As String
            Get
                Return _avatar
            End Get
            Set(ByVal Value As String)
                _avatar = Value
            End Set
        End Property

        Public Property PostCount() As Integer
            Get
                Return _postCount
            End Get
            Set(ByVal Value As Integer)
                _postCount = Value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return _URL
            End Get
            Set(ByVal Value As String)
                _URL = Value
            End Set
        End Property

        Public Property Signature() As String
            Get
                Return _Signature
            End Get
            Set(ByVal Value As String)
                _Signature = Value
            End Set
        End Property

        Public Property TimeZone() As Integer
            Get
                Return _TimeZone
            End Get
            Set(ByVal Value As Integer)
                _TimeZone = Value
            End Set
        End Property

        Public Property Occupation() As String
            Get
                Return _Occupation
            End Get
            Set(ByVal Value As String)
                _Occupation = Value
            End Set
        End Property

        Public Property Interests() As String
            Get
                Return _interests
            End Get
            Set(ByVal Value As String)
                _interests = Value
            End Set
        End Property

        Public Property MSN() As String
            Get
                Return _MSN
            End Get
            Set(ByVal Value As String)
                _MSN = Value
            End Set
        End Property

        Public Property Yahoo() As String
            Get
                Return _Yahoo
            End Get
            Set(ByVal Value As String)
                _Yahoo = Value
            End Set
        End Property

        Public Property AIM() As String
            Get
                Return _AIM
            End Get
            Set(ByVal Value As String)
                _AIM = Value
            End Set
        End Property

        Public Property ICQ() As String
            Get
                Return _ICQ
            End Get
            Set(ByVal Value As String)
                _ICQ = Value
            End Set
        End Property

        Public Property Skin() As String
            Get
                Return _Skin
            End Get
            Set(ByVal Value As String)
                _Skin = Value
            End Set
        End Property

        Public Property LastActivity() As DateTime
            Get
                Return _LastActivity
            End Get
            Set(ByVal Value As DateTime)
                _LastActivity = Value
            End Set
        End Property

        Public Property LastForumsView() As DateTime
            Get
                Return _LastForumsView
            End Get
            Set(ByVal Value As DateTime)
                _LastForumsView = Value
            End Set
        End Property

        Public Property LastThreadView() As DateTime
            Get
                Return _LastThreadView
            End Get
            Set(ByVal Value As DateTime)
                _LastThreadView = Value
            End Set
        End Property

        Public Property FlatView() As Boolean
            Get
                Return _FlatView
            End Get
            Set(ByVal Value As Boolean)
                _FlatView = Value
            End Set
        End Property

        Public Property IsTrusted() As Boolean
            Get
                Return _IsTrusted
            End Get
            Set(ByVal Value As Boolean)
                _IsTrusted = Value
            End Set
        End Property

        Public Property EnableThreadTracking() As Boolean
            Get
                Return _EnableThreadTracking
            End Get
            Set(ByVal Value As Boolean)
                _EnableThreadTracking = Value
            End Set
        End Property

        Public Property EnableDisplayUnreadThreadsOnly() As Boolean
            Get
                Return _EnableDisplayUnreadThreadsOnly
            End Get
            Set(ByVal Value As Boolean)
                _EnableDisplayUnreadThreadsOnly = Value
            End Set
        End Property

        Public Property EnableDisplayInMemberList() As Boolean
            Get
                Return _EnableDisplayInMemberList
            End Get
            Set(ByVal Value As Boolean)
                _EnableDisplayInMemberList = Value
            End Set
        End Property

        Public Property EnablePrivateMessages() As Boolean
            Get
                Return _EnablePrivateMessages
            End Get
            Set(ByVal Value As Boolean)
                _EnablePrivateMessages = Value
            End Set
        End Property

        Public Property EnableOnlineStatus() As Boolean
            Get
                Return _EnableOnlineStatus
            End Get
            Set(ByVal Value As Boolean)
                _EnableOnlineStatus = Value
            End Set
        End Property

        Public Property IsModerator() As Boolean
            Get
                Dim dbUser As New ForumUserDB()
                Dim dr As SqlDataReader = dbUser.TTTForum_Moderate_GetForumsByUser(ZuserID)

                Try 'Added by robfoulk
                    If dr.Read Then
                        Return True
                    Else
                        Return False
                    End If
                Finally
                    dr.Close()
                End Try

            End Get
            Set(ByVal Value As Boolean)
                _IsModerator = Value
            End Set
        End Property

        Public Property PostsModerated() As Integer
            Get
                Return _PostsModerated
            End Get
            Set(ByVal Value As Integer)
                _PostsModerated = Value
            End Set
        End Property

#End Region

#End Region 'ForumUser
    End Class 'ForumUser


#Region "ForumModerator"

    Public Class ForumModerator
        Inherits ForumUser

        Private Const ForumModeratorCacheKeyPrefix As String = "ForumModerator"
        ' DNN user properties

        Private _Unit As String
        Private _Street As String
        Private _City As String
        Private _Region As String
        Private _Country As String
        Private _PostalCode As String

        Private ZforumID As Integer
        Private _CreatedDate As DateTime
        Private _EmailNotification As Boolean
        Private _PostsModerated As Integer
        Private _AuthorizedForums As New ArrayList()
        Private _ModeratedForums As ForumModerateCollection

        Public Sub New()
            MyBase.New()
        End Sub 'New

        Private Sub New(ByVal DataReader As SqlDataReader)
            MyBase.New()
            PopulateInfo(DataReader)
        End Sub

        Private Sub New(ByVal UserID As Integer, ByVal ForumID As Integer)
            MyBase.New()

            ZforumID = ForumID
            ' Grab settings from the database
            Dim dbForum As New ForumDB()
            Dim dr As SqlDataReader = dbForum.TTTForum_Moderate_GetForumModerator(ForumID, UserID)

            PopulateInfo(dr)
            dr.Close()

        End Sub

        Public Shared Function PopulateModerateInfo(ByVal DataReader As SqlDataReader) As ForumModerator

            Dim moderatorInfo As ForumModerator = New ForumModerator(DataReader)
            Return moderatorInfo

        End Function

        Private Sub PopulateInfo(ByVal dr As SqlDataReader)

            UserID = ConvertInteger(dr("UserID"))
            Name = ConvertString(dr("UserName"))
            Email = ConvertString(dr("Email"))
            [Alias] = ConvertString(dr("Alias"))
            TimeZone = ConvertInteger(dr("TimeZone"))
            URL = ConvertString(dr("URL"))
            MSN = ConvertString(dr("MSN"))
            AIM = ConvertString(dr("AIM"))
            ICQ = ConvertString(dr("ICQ"))
            PostCount = ConvertInteger(dr("PostCount"))
            LastActivity = ConvertDateTime(dr("LastActivity"))
            LastForumsView = ConvertDateTime(dr("LastForumsView"))
            LastThreadView = ConvertDateTime(dr("LastThreadView"))
            IsTrusted = ConvertBoolean(dr("IsTrusted"))
            IsModerator = True
            FullName = ConvertString(dr("FullName"))
            FirstName = ConvertString(dr("FirstName"))
            LastName = ConvertString(dr("LastName"))
            _Unit = ConvertString(dr("Unit"))
            _Street = ConvertString(dr("Street"))
            _City = ConvertString(dr("City"))
            _Region = ConvertString(dr("Region"))
            _Country = ConvertString(dr("Country"))
            _PostalCode = ConvertString(dr("PostalCode"))
            _CreatedDate = ConvertDateTime(dr("CreatedDate"))
            _EmailNotification = ConvertBoolean(dr("EmailNotification"))
            _PostsModerated = ConvertInteger(dr("PostsModerated"))



            Dim dbForumUser As New ForumUserDB()
            Dim drMD As SqlDataReader = dbForumUser.TTTForum_Moderate_GetForumsByUser(UserID)
            While drMD.Read
                _AuthorizedForums.Add(ConvertInteger(drMD("ForumID")))
            End While

            drMD.Close()

        End Sub 'PopulateInfo


        Public Shared Function GetForumModerator(ByVal UserID As Integer, ByVal ForumID As Integer) As ForumModerator

            ' Grab reference to the applicationstate object
			' Need to change Forum Cashing
            
			Dim TempKey as String = GetDBName & ForumModeratorCacheKeyPrefix & CStr(UserID)
			Dim context As HttpContext = HttpContext.Current
			Dim moderator As ForumModerator 
            If Context.Cache(TempKey) Is Nothing Then
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' If this object has not been instantiated yet, we need to grab it
            moderator = New ForumModerator(UserID, ForumID)
			Context.Cache.Insert(TempKey, moderator, DotNetZoom.CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), Caching.CacheItemPriority.low, nothing)
			Else
			moderator = CType(Context.Cache(TempKey), ForumModerator)
            End If
            Return moderator

        End Function

        Public Shared Sub ResetForumModerator(ByVal UserID As Integer)
			Dim TempKey as String = GetDBName & ForumModeratorCacheKeyPrefix & CStr(UserID)
			Dim context As HttpContext = HttpContext.Current
				context.Cache.Remove(TempKey)
        End Sub
		

        Public ReadOnly Property Unit() As String
            Get
                Return _Unit
            End Get
        End Property

        Public ReadOnly Property Street() As String
            Get
                Return _Street
            End Get
        End Property

        Public ReadOnly Property City() As String
            Get
                Return _City
            End Get
        End Property

        Public ReadOnly Property Region() As String
            Get
                Return _Region
            End Get
        End Property

        Public ReadOnly Property Country() As String
            Get
                Return _Country
            End Get
        End Property

        Public ReadOnly Property PostalCode() As String
            Get
                Return _PostalCode
            End Get
        End Property

        Public ReadOnly Property CreatedDate() As DateTime
            Get
                Return _CreatedDate
            End Get
        End Property

        Public Property EmailNotification() As Boolean
            Get
                Return _EmailNotification
            End Get
            Set(ByVal Value As Boolean)
                _EmailNotification = Value
            End Set
        End Property

        Public Property PostsModeratedCount() As Integer
            Get
                Return _PostsModerated
            End Get
            Set(ByVal Value As Integer)
                _PostsModerated = Value
            End Set
        End Property

        Public Property AuthorizedForum() As ArrayList
            Get
                Return _AuthorizedForums
            End Get
            Set(ByVal Value As ArrayList)
                _AuthorizedForums = Value
            End Set
        End Property

        Public ReadOnly Property IsForumModerator(ByVal ForumID As Integer) As Boolean
            Get
                Return _AuthorizedForums.Contains(ForumID)
            End Get
        End Property

        'ForumModerator
    End Class 'ForumModerator

#End Region


#Region "UserModerateCollection"

    Public Class ForumModeratorCollection
        Inherits ArrayList

        Private ZforumID As Integer
        Private ZuserIDList As New ArrayList()

        Public Sub New()

        End Sub

        Public Sub New(ByVal ForumID As Integer)

            ZforumID = ForumID

            PopulateCollection()
            'Return Me
        End Sub 'New

        Private Sub PopulateCollection()
            Dim dbForum As New ForumDB()

            Dim dr As SqlDataReader = dbForum.TTTForum_Moderate_GetForumModerators(ZforumID)
            While dr.Read
                Dim moderator As ForumModerator = ForumModerator.PopulateModerateInfo(dr)
                Me.Add(moderator)
                ZuserIDList.Add(moderator.UserID) ' to determine forum's moderator by userID inteads of ForumModerator object
            End While

            dr.Close()

        End Sub

        Public Sub New(ByVal c As ICollection)
            MyBase.New(c)
        End Sub 'New

        Public ReadOnly Property IsForumModerator(ByVal UserID As Integer) As Boolean
            Get
                Return ZuserIDList.Contains(UserID)
            End Get
        End Property

    End Class 'ForumThreadInfoCollection


#End Region



End Namespace