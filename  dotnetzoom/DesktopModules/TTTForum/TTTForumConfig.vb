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
Imports System.Collections.Specialized
Imports System.IO
Imports System.Web
Imports System.Web.Caching


Namespace DotNetZoom

    Public Class ForumConfig

#Region "Private Vars"

        Private Const ForumConfigCacheKeyPrefix As String = "ForumConfig"

        Private mImageFolder As String = DefaultImageFolder
        Private mAvatarFolder As String = DefaultAvatarFolder
        Private mAvatarModuleID As Integer = 0
        Private mUserAvatar As Boolean = True
        Private mHTMLMailFormat As Boolean = True
        Private mMailNotification As Boolean = True
        Private mShowSenderAddressOnEmail As Boolean = False 'Notify mail with sender address
        Private mAutomatedEmailAddress As String = DefaultEmailAddress
        Private mEmailDomain As String = DefaultEmailDomain
        Private mThreadsPerPage As Integer = DefaultThreadsPerPage
        Private mPostsPerPage As Integer = DefaultPostsPerPage
        Private mImageSizeHeight As Integer = DefaultImageHeight
        Private mImageSizeWidth As Integer = DefaultImageWidth
        Private mMaxAvatarSize As Integer = DefaultMaxAvatarSize
        Private mImageTypes As New ArrayList()
        Private mImageExtensions As String = DefaultImageExtensions
        Private mUserOnlineIntegrate As Boolean = False
        Private mStatsUpdateInterval As Integer = DefaultStatsUpdateInterval

        ' ModuleID for these settings
        Private mModuleID As Integer


#End Region

#Region "Shared Methods"

        ' These are all default values
        Public Shared ReadOnly Property DefaultImageFolder() As String
            Get
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                If CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("ImageFolder"), String) = "" Then
                    Return glbPath() & "images/ttt/"
                Else
                    Return CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("ImageFolder"), String)
                End If
            End Get
        End Property

        Public Shared ReadOnly Property SkinImageFolder() As String
            Get
                Dim context As HttpContext = HttpContext.Current
                If context.Request.IsAuthenticated Then
                    Dim UserCSS As ForumUser
                    UserCSS = ForumUser.GetForumUser(Int16.Parse(context.User.Identity.Name))
                    Select Case UserCSS.Skin
                        Case "Jardin Floral"
                            Return glbPath() & "images/ttt/skin1/"
                        Case "Stibnite"
                            Return glbPath() & "images/ttt/skin2/"
                        Case "Algues bleues"
                            Return glbPath() & "images/ttt/skin3/"
                    End Select
                End If
                Return DefaultImageFolder()
            End Get
        End Property

        Public Shared ReadOnly Property DefaultSkinFolder() As String
            Get
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                Return _portalSettings.UploadDirectory & "skin/ttt.css"
            End Get
        End Property



        Public Shared Sub SetSkinCSS(ByVal Page As Page)

            Dim objCSS As Control = Page.FindControl("CSS")
            Dim objTTTCSS As Control = Page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl


            If (Not objCSS Is Nothing) And (objTTTCSS Is Nothing) Then
                ' put in the ttt.css
                objLink = New System.Web.UI.LiteralControl("TTTCSS")
                If HttpContext.Current.Request.IsAuthenticated Then
                    Dim UserCSS As ForumUser
                    UserCSS = ForumUser.GetForumUser(Int16.Parse(HttpContext.Current.User.Identity.Name))
                    Select Case UserCSS.Skin
                        Case "Jardin Floral"
                            objLink.Text = "<link href=""" & glbPath() & "images/ttt/skin1/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                        Case "Stibnite"
                            objLink.Text = "<link href=""" & glbPath() & "images/ttt/skin2/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                        Case "Algues bleues"
                            objLink.Text = "<link href=""" & glbPath() & "images/ttt/skin3/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                        Case Else
                            objLink.Text = "<link href=""" & DefaultSkinFolder() & """ type=""text/css"" rel=""stylesheet"">"
                    End Select
                Else
                    objLink.Text = "<link href=""" & DefaultSkinFolder() & """ type=""text/css"" rel=""stylesheet"">"
                End If
                objCSS.Controls.Add(objLink)
            End If

        End Sub


        Public Shared ReadOnly Property DefaultAvatarFolder() As String
            Get
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                Return _portalSettings.UploadDirectory + "forums/Avatars"
            End Get
        End Property


        Public Shared ReadOnly Property DefaultEmailAddress() As String
            Get
                Dim DomainName As String = HttpContext.Current.Request.Url.Host.ToLower()
                If DomainName.IndexOf("www.") = 0 Then
                    DomainName = Replace(DomainName, "www.", "")
                End If
                Return "Babillard@" & DomainName
            End Get
        End Property

        Public Shared ReadOnly Property DefaultEmailDomain() As String
            Get
                Dim DomainName As String = HttpContext.Current.Request.Url.Host.ToLower()
                If DomainName.IndexOf("www.") = 0 Then
                    DomainName = Replace(DomainName, "www.", "")
                End If
                Return DomainName
            End Get
        End Property

        Public Shared ReadOnly Property DefaultThreadsPerPage() As Integer
            Get
                Return 10
            End Get
        End Property

        Public Shared ReadOnly Property DefaultPostsPerPage() As Integer
            Get
                Return 5
            End Get
        End Property

        Public Shared ReadOnly Property DefaultImageHeight() As Integer
            Get
                Return 300
            End Get
        End Property

        Public Shared ReadOnly Property DefaultImageWidth() As Integer
            Get
                Return 300
            End Get
        End Property

        Public Shared ReadOnly Property DefaultMaxAvatarSize() As Integer
            Get
                Return 100
            End Get
        End Property

        Public Shared ReadOnly Property DefaultImageExtensions() As String
            Get
                Return ".jpg;.gif;.bmp;.png;.tif"
            End Get
        End Property

        Public Shared ReadOnly Property DefaultStatsUpdateInterval() As Integer
            Get
                Return 12
            End Get
        End Property

        Public Shared Function GetForumConfig(ByVal ModuleID As Integer) As ForumConfig

            ' Grab reference to the applicationstate object
            ' Need to change Forum Cashing

            Dim TempKey As String = GetDBname() & ForumConfigCacheKeyPrefix & CStr(ModuleID)
            Dim context As HttpContext = HttpContext.Current
            Dim config As ForumConfig

            If context.Cache(TempKey) Is Nothing Then
                ' If this object has not been instantiated yet, we need to grab it
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                config = New ForumConfig(ModuleID)
                context.Cache.Insert(TempKey, config, DotNetZoom.CDp(_portalSettings.PortalId, _portalSettings.ActiveTab.TabId, ModuleID), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.Normal, Nothing)
            Else
                config = CType(context.Cache(TempKey), ForumConfig)
            End If
            Return config

        End Function

        Public Shared Sub ResetForumConfig(ByVal ModuleID As Integer)
            Dim TempKey As String = GetDBname() & ForumConfigCacheKeyPrefix & CStr(ModuleID)
            Dim context As HttpContext = HttpContext.Current
            context.Cache.Remove(TempKey)
        End Sub

#End Region

#Region "Public Properties"

        Public ReadOnly Property ImageFolder() As String
            Get
                Return mImageFolder
            End Get
        End Property

        Public ReadOnly Property AvatarFolder() As String
            Get
                Return mAvatarFolder
            End Get
        End Property

        Public ReadOnly Property AvatarModuleID() As Integer
            Get
                If mAvatarModuleID > 0 Then
                    Return mAvatarModuleID
                Else
                    Dim DBForum As New ForumDB()
                    Dim dr As SqlDataReader = DBForum.TTTForum_GetAvatarsModule(mModuleID.ToString())
                    Dim _avatarModuleID As Integer
                    If dr.Read Then
                        _avatarModuleID = TTTUtils.ConvertInteger(dr("ModuleID"))
                    Else
                        _avatarModuleID = 0
                    End If
                    dr.Close()
                    Return _avatarModuleID
                End If
            End Get
        End Property


        Public ReadOnly Property ShowSenderAddressOnEmail() As Boolean
            Get
                Return mShowSenderAddressOnEmail
            End Get
        End Property

        Public ReadOnly Property AutomatedEmailAddress() As String
            Get
                Return mAutomatedEmailAddress
            End Get
        End Property

        Public ReadOnly Property EmailDomain() As String
            Get
                Return mEmailDomain
            End Get
        End Property

        Public ReadOnly Property HTMLMailFormat() As Boolean
            Get
                Return mHTMLMailFormat
            End Get
        End Property

        Public ReadOnly Property UserAvatar() As Boolean
            Get
                Return mUserAvatar
            End Get
        End Property

        Public ReadOnly Property MailNotification() As Boolean
            Get
                Return mMailNotification
            End Get
        End Property

        Public ReadOnly Property ThreadsPerPage() As Integer
            Get
                Return mThreadsPerPage
            End Get
        End Property

        Public ReadOnly Property PostsPerPage() As Integer
            Get
                Return mPostsPerPage
            End Get
        End Property

        Public ReadOnly Property ImageSizeHeight() As Integer
            Get
                Return mImageSizeHeight
            End Get
        End Property

        Public ReadOnly Property ImageSizeWidth() As Integer
            Get
                Return mImageSizeWidth
            End Get
        End Property

        Public ReadOnly Property MaxAvatarSize() As Integer
            Get
                Return mMaxAvatarSize
            End Get
        End Property

        Public ReadOnly Property ImageExtensions() As String
            Get
                Return mImageExtensions
            End Get
        End Property

        Public ReadOnly Property IsValidImageType(ByVal ImageExtension As String) As Boolean
            Get
                Return mImageTypes.Contains(ImageExtension)
            End Get
        End Property

        Public ReadOnly Property UserOnlineIntegrate() As Boolean
            Get
                Return mUserOnlineIntegrate
            End Get
        End Property


        Public ReadOnly Property StatsUpdateInterval() As Integer
            Get
                Return mStatsUpdateInterval
            End Get
        End Property

#End Region

#Region "Public Methods, Functions"

        Shared Sub New()
        End Sub

        Private Sub New(ByVal ModuleID As Integer)

            mModuleID = ModuleID

            ' Grab settings from the database
            Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleID)

            ' Now iterate through all the values and init local variables         
            mImageFolder = CStr(GetValue(settings("ImageFolder"), CStr(mImageFolder)))
            mAvatarFolder = CStr(GetValue(settings("AvatarFolder"), CStr(mAvatarFolder + ModuleID.ToString() + "/")))
            mAvatarModuleID = CInt(GetValue(settings("AvatarModuleID"), CStr(mAvatarModuleID)))
            mShowSenderAddressOnEmail = CBool(GetValue(settings("ShowSenderAddressOnEmail"), CStr(mShowSenderAddressOnEmail)))
            mAutomatedEmailAddress = CStr(GetValue(settings("AutomatedEmailAddress"), CStr(mAutomatedEmailAddress)))
            mEmailDomain = CStr(GetValue(settings("EmailDomain"), CStr(mEmailDomain)))
            mHTMLMailFormat = CBool(GetValue(settings("HTMLMailFormat"), CStr(mHTMLMailFormat)))
            mUserAvatar = CBool(GetValue(settings("UserAvatar"), CStr(mUserAvatar)))
            mMailNotification = CBool(GetValue(settings("MailNotification"), CStr(mMailNotification)))
            mThreadsPerPage = CInt(GetValue(settings("ThreadsPerPage"), CStr(mThreadsPerPage)))
            mPostsPerPage = CInt(GetValue(settings("PostsPerPage"), CStr(mPostsPerPage)))
            mImageSizeHeight = CInt(GetValue(settings("ImageSizeHeigh"), CStr(mImageSizeHeight)))
            mImageSizeWidth = CInt(GetValue(settings("ImageSizeWidth"), CStr(mImageSizeWidth)))
            mMaxAvatarSize = CInt(GetValue(settings("MaxAvatarSize"), CStr(mMaxAvatarSize)))
            mImageExtensions = CStr(GetValue(settings("ImageExtensions"), CStr(mImageExtensions)))
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If PortalSettings.GetSiteSettings(_portalSettings.PortalId)("PortalUserOnline") <> "NO" Then
                mUserOnlineIntegrate = CBool(GetValue(settings("UserOnlineIntegrate"), CStr(mUserOnlineIntegrate)))
            Else
                ' turn it off if not On the Portal
                mUserOnlineIntegrate = False
            End If

            mStatsUpdateInterval = CInt(GetValue(settings("StatsUpdateInterval"), CStr(mStatsUpdateInterval)))

            Dim imageExtension As String

            For Each imageExtension In Split(mImageExtensions, ";", , CompareMethod.Text)
                mImageTypes.Add(LCase(imageExtension))
            Next

        End Sub


#End Region

#Region "Private Functions"

        Private Function GetValue(ByVal Input As Object, ByVal DefaultValue As String) As String
            ' Used to determine if a valid input is provided, if not, return default value
            If Input Is Nothing Then
                Return DefaultValue
            Else
                Return CStr(Input)
            End If

        End Function

#End Region

    End Class

End Namespace