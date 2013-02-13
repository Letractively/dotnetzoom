'=======================================================================================
' TTTGALLERY MODULE FOR DOTNETNUKE (1.0.10)
'=======================================================================================
' Original version by:              DAVID BARRETT http://www.davidbarrett.net
' A modified version for DNN by:    KENNYRICE
' This version for TTTCompany       http://www.tttcompany.com
' Author:                           TAM TRAN MINH   (tamttt - tam@tttcompany.com)
' Slideshow component:              FREEK VAN OORT
' Flash Player component:           TYLER JENSEN
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
'========================================================================================
Option Strict On

Imports System
Imports System.Collections.Specialized
Imports System.IO
Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports System.Web.Caching

Namespace DotNetZoom

    Public Class GalleryConfig

        Public Enum GalleryDisplayOption
            None
            FileInfo
            Description
            FileInfoAndDescription
        End Enum


#Region "Private Vars"

        Private Const GalleryConfigCacheKeyPrefix As String = "G_"

        ' Root filesystem Path
        Private mRootPath As String = ""
        Private mRootURL As String = DefaultRootURL
        Private mThumbFolder As String = DefaultThumbFolder
        ' Ajout par rene boulard 2003-04-24 pour image folder
        Private mResFolder As String = DefaultResFolder
        Private mSourceFolder As String = DefaultSourceFolder
        Private mtempFolder As String = DefaultTempFolder
        Private mGalleryTitle As String = DefaultGalleryTitle
        Private mGalleryDescription As String = DefaultGalleryDescription

        ' Number of pics per row on each page view & Number of rows per page view
        Private mStripWidth As Integer = DefaultStripWidth
        Private mStripHeight As Integer = DefaultStripHeight

        ' Acceptable file types
        Private mFileExtensions As String = DefaultFileExtensions
        Private mMovieExtensions As String = DefaultMovieExtensions
        Private mFlashExtensions As String = DefaultFlashExtensions
        Private mCategoryValues As String = DefaultCategoryValues
        Private mFileTypes As New ArrayList()
        Private mMovieTypes As New ArrayList()
        Private mFlashTypes As New ArrayList()
        Private mCategories As New ArrayList()

        ' Cache Settings
        Private mBuildCacheonStart As Boolean = True
        Private mCheckboxGPS As Boolean = False
        Private mGoogleAPI As String = ""
        Private mCheckboxIndex As Boolean = False


        Private mFixedSize As Boolean = True
        Private mFixedWidth As Integer = DefaultFixedWidth
        Private mQuality As Integer = DefaultQuality
        Private mFixedHeight As Integer = DefaultFixedHeight
        Private mKeepSource As Boolean = False
        Private mSlideshowSpeed As Integer = DefaultSlideshowSpeed
        Private mIsPrivate As Boolean = False
        Private mCryptoUrl As Boolean = False
        Private mIsIntegrated As Boolean = False
        Private mIntegratedForumGroup As Integer = 0
        Private mSlideshowPopup As Boolean = False
        Private mInfoBule As Boolean = True
        Private mMaxFileSize As Integer = DefaultMaxFileSize
        Private mQuota As Integer = 0 'Zero means unlimited

        ' Thumbnail creation
        Private mMaxThumbWidth As Integer = DefaultMaxThumbWidth
        Private mMaxThumbHeight As Integer = DefaultMaxThumbHeight

        ' Holds a reference to the root gallery folder
        Private mRootFolder As GalleryFolder

        ' ModuleID for these settings
        Private mModuleID As Integer

        ' IsValidPath return whether the root gallery path is valid for this machine
        Private mIsValidPath As Boolean

        ' Owner - for User Gallery feature
        Private mOwnerID As Integer = DefaultOwnerID


        ' AllowDownload feature
        Private mAllowDownload As Boolean = True
        Private mDownloadRoles As String = glbRoleUnauthUser & ";"
        ' It's Avatar gallery for Forum module
        Private mIsAvatarsGallery As Boolean = False

        'Display Option
        Private mdisplayOption As String = DefaultDisplayOption

#End Region

#Region "Shared Methods"


        Public Shared Sub SetSkinCSS(ByVal Page As Page)

            ' Obtain PortalSettings from Current Context
            Dim objCSS As Control = Page.FindControl("CSS")
            Dim objTTTCSS As Control = Page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If (Not objCSS Is Nothing) And (objTTTCSS Is Nothing) Then
                ' put in the ttt.css
                objLink = New System.Web.UI.LiteralControl("TTTCSS")
                objLink.Text = vbCrLf + "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                objCSS.Controls.Add(objLink)
            End If

        End Sub

        Public Shared Sub SetFancyBoxCSS(ByVal Page As Page)
            ' Obtain PortalSettings from Current Context
            Dim objCSS As Control = Page.FindControl("CSS")
            Dim objfancybox As Control = Page.FindControl("FancyCSS")
            Dim objLink As System.Web.UI.LiteralControl
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If (Not objCSS Is Nothing) And (objfancybox Is Nothing) Then
                ' put in the ttt.css
                objLink = New System.Web.UI.LiteralControl("FancyCSS")
                objLink.Text = vbCrLf + "<link href=""" + glbPath + "javascript/fancybox.css"" type=""text/css"" rel=""stylesheet"" media=""screen"">"
                objCSS.Controls.Add(objLink)
            End If
        End Sub



        ' These are all default values
        ' Changes are not necessary
        Public Shared ReadOnly Property DefaultRootURL() As String
            Get
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                Return _portalSettings.UploadDirectory & "Album"
            End Get
        End Property

        ' Ajout par rene boulard 2003-04-24 pour image folder

        Public Shared ReadOnly Property DefaultResFolder() As String
            Get
                Return "_res"
            End Get
        End Property

        Public Shared ReadOnly Property DefaultThumbFolder() As String
            Get
                Return "_thumbs"
            End Get
        End Property

        Public Shared ReadOnly Property DefaultSourceFolder() As String
            Get
                Return "_source"
            End Get
        End Property

        Public Shared ReadOnly Property DefaultTempFolder() As String
            Get
                Return "_temp"
            End Get
        End Property

        Public Shared ReadOnly Property DefaultGalleryTitle() As String
            Get
                Return "Album"
            End Get
        End Property

        Public Shared ReadOnly Property DefaultGalleryDescription() As String
            Get
                Return "Album"
            End Get
        End Property

        Public Shared ReadOnly Property DefaultStripWidth() As Integer
            Get
                Return 4
            End Get
        End Property

        Public Shared ReadOnly Property DefaultStripHeight() As Integer
            Get
                Return 2
            End Get
        End Property

        Public Shared ReadOnly Property DefaultMaxThumbWidth() As Integer
            Get
                Return 100
            End Get
        End Property

        Public Shared ReadOnly Property DefaultMaxThumbHeight() As Integer
            Get
                Return 100
            End Get
        End Property

        Public Shared ReadOnly Property DefaultFileExtensions() As String
            Get
                Return ".jpg;.gif;.bmp;.png;.tif"
            End Get
        End Property

        Public Shared ReadOnly Property DefaultMovieExtensions() As String
            Get
                Return ".mov;.wmv;.mpg;.avi;.asf;.asx;.mpeg;.mid;.midi;.wav;.aiff;.mp3;.flv;.mp4"
            End Get
        End Property

        Public Shared ReadOnly Property DefaultFlashExtensions() As String
            Get
                Return ".swf"
            End Get
        End Property

        Public Shared ReadOnly Property DefaultCategoryValues() As String
            Get
                Return GetLanguage("Gal_DefaultCategory")
            End Get
        End Property

        Public Shared ReadOnly Property DefaultMaxFileSize() As Integer
            Get
                Return 1000
            End Get
        End Property

        Public Shared ReadOnly Property DefaultFixedWidth() As Integer
            Get
                Return 500
            End Get
        End Property

        Public Shared ReadOnly Property DefaultQuality() As Integer
            Get
                Return 50
            End Get
        End Property

        Public Shared ReadOnly Property DefaultFixedHeight() As Integer
            Get
                Return 500
            End Get
        End Property

        Public Shared ReadOnly Property DefaultSlideshowSpeed() As Integer
            Get
                Return 3000
            End Get
        End Property

        Public Shared ReadOnly Property DefaultOwnerID() As Integer
            Get
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                Return _portalSettings.AdministratorId
            End Get
        End Property

        Public Shared ReadOnly Property DefaultOwner() As String
            Get
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                Dim dbUsers As New UsersDB()
                Dim dr As SqlDataReader = dbUsers.GetSingleUser(_portalSettings.PortalId, _portalSettings.AdministratorId)
                Dim userName As String
                If dr.Read Then
                    userName = ConvertString(dr("UserName"))
                End If
                dr.Close()

                Return userName

            End Get
        End Property

        Public Shared ReadOnly Property DefaultDisplayOption() As String
            Get
                Return "None"
            End Get
        End Property



        Public Shared Function GetGalleryConfig(ByVal ModuleID As Integer) As GalleryConfig

            ' Grab reference to the applicationstate object

            Dim TempKey As String = GetDBname() & GalleryConfigCacheKeyPrefix & CStr(ModuleID)
            Dim context As HttpContext = HttpContext.Current
            Dim config As GalleryConfig
            If context.Cache(TempKey) Is Nothing Then
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                ' If this object has not been instantiated yet, we need to grab it
                config = New GalleryConfig(ModuleID)
                context.Cache.Insert(TempKey, config, DotNetZoom.CDp(_portalSettings.PortalId, _portalSettings.ActiveTab.TabId, ModuleID), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.Normal, Nothing)
            Else
                config = CType(context.Cache(TempKey), GalleryConfig)
            End If

            Return config

        End Function

        Public Shared Sub ResetGalleryConfig(ByVal ModuleID As Integer)
            Dim TempKey As String = GetDBname() & GalleryConfigCacheKeyPrefix & CStr(ModuleID)
            Dim context As HttpContext = HttpContext.Current
            context.Cache.Remove(TempKey)
            ' GetGalleryConfig(ModuleID)
        End Sub

#End Region

#Region "Public Properties"

        Public ReadOnly Property RootFolder() As GalleryFolder
            Get
                Return mRootFolder
            End Get
        End Property

        Public ReadOnly Property ItemsPerStrip() As Integer
            Get
                Return CInt(mStripWidth * mStripHeight)
            End Get
        End Property

        Public ReadOnly Property RootPath() As String
            Get
                Return mRootPath
            End Get
        End Property

        Public ReadOnly Property RootURL() As String
            Get
                Return mRootURL
            End Get
        End Property

        Public ReadOnly Property ThumbFolder() As String
            Get
                Return mThumbFolder
            End Get
        End Property

        ' Ajout par rene boulard 2003-04-24 pour image folder		

        Public ReadOnly Property ResFolder() As String
            Get
                Return mResFolder
            End Get
        End Property

        Public ReadOnly Property SourceFolder() As String
            Get
                Return mSourceFolder
            End Get
        End Property

        Public ReadOnly Property TempFolder() As String
            Get
                Return mtempFolder
            End Get
        End Property

        Public ReadOnly Property GalleryTitle() As String
            Get
                Return mGalleryTitle
            End Get
        End Property

        Public ReadOnly Property GalleryDescription() As String
            Get
                Return mGalleryDescription
            End Get
        End Property


        Public ReadOnly Property GoogleAPI() As String
            Get
                Return mGoogleAPI
            End Get
        End Property

        Public ReadOnly Property CheckboxGPS() As Boolean
            Get
                Return mCheckboxGPS
            End Get
        End Property

        Public ReadOnly Property CheckboxIndex() As Boolean
            Get
                Return mCheckboxIndex
            End Get
        End Property


        Public ReadOnly Property BuildCacheonStart() As Boolean
            Get
                Return mBuildCacheonStart
            End Get
        End Property

        Public ReadOnly Property StripWidth() As Integer
            Get
                Return mStripWidth
            End Get
        End Property

        Public ReadOnly Property StripHeight() As Integer
            Get
                Return mStripHeight
            End Get
        End Property

        Public ReadOnly Property MaximumThumbWidth() As Integer
            Get
                Return mMaxThumbWidth
            End Get
        End Property

        Public ReadOnly Property MaximumThumbHeight() As Integer
            Get
                Return mMaxThumbHeight
            End Get
        End Property

        Public ReadOnly Property IsValidImageType(ByVal FileExtension As String) As Boolean
            Get
                Return mFileTypes.Contains(LCase(FileExtension))
            End Get
        End Property

        Public ReadOnly Property IsValidMovieType(ByVal FileExtension As String) As Boolean
            Get
                Return mMovieTypes.Contains(LCase(FileExtension))
            End Get
        End Property

        Public ReadOnly Property IsValidFlashType(ByVal FileExtension As String) As Boolean
            Get
                Return mFlashTypes.Contains(LCase(FileExtension))
            End Get
        End Property

        Public ReadOnly Property IsValidFileType(ByVal FileExtension As String) As Boolean
            Get
                Return (IsValidImageType(LCase(FileExtension)) OrElse IsValidMovieType(LCase(FileExtension)) OrElse IsValidFlashType(LCase(FileExtension)) OrElse (LCase(FileExtension) = ".zip"))
            End Get
        End Property

        Public ReadOnly Property FileExtensions() As String
            Get
                Return mFileExtensions
            End Get
        End Property

        Public ReadOnly Property MovieExtensions() As String
            Get
                Return mMovieExtensions
            End Get
        End Property

        Public ReadOnly Property MovieTypes() As ArrayList
            Get
                Return Me.mMovieTypes
            End Get
        End Property

        Public ReadOnly Property CategoryValues() As String
            Get
                Return mCategoryValues
            End Get
        End Property

        Public ReadOnly Property Categories() As ArrayList
            Get
                Return mCategories
            End Get
        End Property

        Public ReadOnly Property IsValidPath() As Boolean
            Get
                Return mIsValidPath
            End Get
        End Property

        Public ReadOnly Property ModuleID() As Integer
            Get
                Return mModuleID
            End Get
        End Property

        Public ReadOnly Property IsFixedSize() As Boolean
            Get
                Return mFixedSize
            End Get
        End Property

        Public ReadOnly Property FixedWidth() As Integer
            Get
                Return mFixedWidth
            End Get
        End Property


        Public ReadOnly Property Quality() As Integer
            Get
                Return mQuality
            End Get
        End Property

        Public ReadOnly Property MaxFileSize() As Integer
            Get
                Return mMaxFileSize
            End Get
        End Property


        Public ReadOnly Property Quota() As Integer
            Get
                Return mQuota
            End Get
        End Property

        Public ReadOnly Property FixedHeight() As Integer
            Get
                Return mFixedHeight
            End Get
        End Property

        Public ReadOnly Property IsKeepSource() As Boolean
            Get
                Return mKeepSource
            End Get
        End Property

        Public ReadOnly Property SlideshowSpeed() As Integer
            Get
                Return mSlideshowSpeed
            End Get
        End Property

        Public ReadOnly Property IsPrivate() As Boolean
            Get
                Return mIsPrivate
            End Get
        End Property

        Public ReadOnly Property CryptoURL() As Boolean
            Get
                Return mCryptoUrl
            End Get
        End Property

        Public ReadOnly Property IsIntegrated() As Boolean
            Get
                Return mIsIntegrated
            End Get
        End Property

        Public ReadOnly Property IntegratedForumGroup() As Integer
            Get
                Return mIntegratedForumGroup
            End Get
        End Property

        Public ReadOnly Property SlideshowPopup() As Boolean
            Get
                Return mSlideshowPopup
            End Get
        End Property

        Public ReadOnly Property InfoBule() As Boolean
            Get
                Return mInfoBule
            End Get
        End Property


        Public ReadOnly Property OwnerID() As Integer
            Get
                Return mOwnerID
            End Get
        End Property



        Public ReadOnly Property AllowDownload() As Boolean
            Get
                Return mAllowDownload
            End Get
        End Property

        Public ReadOnly Property DownloadRoles() As String
            Get
                Return mDownloadRoles
            End Get
        End Property

        Public ReadOnly Property IsAvatarsGallery() As Boolean
            Get
                Return mIsAvatarsGallery
            End Get
        End Property

        Public ReadOnly Property DisplayOption() As String
            Get
                Return mdisplayOption
            End Get
        End Property

#End Region

#Region "Public Methods, Functions"

        Shared Sub New()
        End Sub

        Public Sub New(ByVal ModuleID As Integer)
            Dim Path As String
            Dim fileExtension As String
            Dim catValue As String

            ' Grab the moduleID
            mModuleID = ModuleID

            ' Grab settings from the database
            Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleID)

            ' Now iterate through all the values and init local variables
            mRootURL = GetValue(settings("RootURL"), "")
            If mRootURL = "" Then
                mRootURL = DefaultRootURL + ModuleID.ToString
            End If

            mGalleryTitle = GetValue(settings("GalleryTitle"), mGalleryTitle)

            Try
                Path = HttpContext.Current.Request.MapPath(mRootURL)
                If System.IO.Directory.Exists(Path) Then
                    mRootPath = Path
                    mIsValidPath = True
                End If
            Catch ex As Exception
                LogMessage(HttpContext.Current.Request, "Erreur GalleryConfig PathNotExists, " + Path + " " + ex.Message)

            End Try

            mGalleryDescription = GetValue(settings("GalleryDescription"), mGalleryDescription)
            mCheckboxGPS = CBool(GetValue(settings("CheckboxGPS"), CStr(mCheckboxGPS)))
            mGoogleAPI = GetValue(settings("GoogleAPI"), mGoogleAPI)
            mCheckboxIndex = CBool(GetValue(settings("CheckboxIndex"), CStr(mCheckboxIndex)))


            mBuildCacheonStart = CBool(GetValue(settings("BuildCacheOnStart"), CStr(mBuildCacheonStart)))
            mStripWidth = CInt(GetValue(settings("StripWidth"), CStr(mStripWidth)))
            mStripHeight = CInt(GetValue(settings("StripHeight"), CStr(mStripHeight)))
            mMaxThumbWidth = CInt(GetValue(settings("MaxThumbWidth"), CStr(mMaxThumbWidth)))
            mMaxThumbHeight = CInt(GetValue(settings("MaxThumbHeight"), CStr(mMaxThumbHeight)))

            mQuota = CInt(GetValue(settings("Quota"), CStr(mQuota)))
            mMaxFileSize = CInt(GetValue(settings("MaxFileSize"), CStr(mMaxFileSize)))
            mFixedSize = CBool(GetValue(settings("IsFixedSize"), CStr(mFixedSize)))
            mQuality = CInt(GetValue(settings("Quality"), CStr(mQuality)))
            mFixedWidth = CInt(GetValue(settings("FixedWidth"), CStr(mFixedWidth)))
            mFixedHeight = CInt(GetValue(settings("FixedHeight"), CStr(mFixedHeight)))
            mKeepSource = CBool(GetValue(settings("IsKeepSource"), CStr(mKeepSource)))
            mSlideshowSpeed = CInt(GetValue(settings("SlideshowSpeed"), CStr(mSlideshowSpeed)))
            mIsIntegrated = CBool(GetValue(settings("IsIntegrated"), CStr(mIsIntegrated)))
            mIsPrivate = CBool(GetValue(settings("IsPrivate"), CStr(mIsPrivate)))
            mCryptoUrl = CBool(GetValue(settings("CryptoURL"), CStr(mCryptoUrl)))
            mIntegratedForumGroup = CInt(GetValue(settings("IntegratedForumGroup"), CStr(mIntegratedForumGroup)))
            mSlideshowPopup = CBool(GetValue(settings("SlideshowPopup"), CStr(mSlideshowPopup)))
            mInfoBule = CBool(GetValue(settings("InfoBule"), CStr(mInfoBule)))
            mAllowDownload = CBool(GetValue(settings("AllowDownload"), CStr(mAllowDownload)))
            mDownloadRoles = GetValue(settings("DownloadRoles"), CStr(mDownloadRoles))
            mIsAvatarsGallery = CBool(GetValue(settings("IsAvatarsGallery"), CStr(mIsAvatarsGallery)))
            mdisplayOption = GetValue(settings("DisplayOption"), mdisplayOption)

            mOwnerID = CInt(GetValue(settings("OwnerID"), CStr(mOwnerID)))

            mFileExtensions = GetValue(settings("FileExtensions"), mFileExtensions)
            ' Iterate through the file extensions and create the collection
            For Each fileExtension In Split(mFileExtensions, ";", , CompareMethod.Text)
                mFileTypes.Add(LCase(fileExtension))
            Next

            mMovieExtensions = GetValue(settings("MovieExtensions"), mMovieExtensions)
            ' Iterate through the movie extensions and create the collection
            For Each fileExtension In Split(mMovieExtensions, ";", , CompareMethod.Text)
                mMovieTypes.Add(LCase(fileExtension))
            Next

            For Each fileExtension In Split(mFlashExtensions, ";", , CompareMethod.Text)
                mFlashTypes.Add(LCase(fileExtension))
            Next

            mCategoryValues = GetValue(settings("CategoryValues"), mCategoryValues)
            For Each catValue In Split(mCategoryValues, ";", , CompareMethod.Text)
                mCategories.Add(LCase(catValue))
            Next

            ' Edit right on root folder to be assigned to: 
            ' Admin 
            ' TabAdmin - as mentioned by Katse
            ' Owner - if it's a private gallery
            ' User with module edit role - if it's a public gallery

            'Dim isEditable As Boolean = False

            'Try 'BRT: Need Exception Handling
            'If (HttpContext.Current.User.Identity.IsAuthenticated) Then
            'If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
            'OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
            'OrElse (Int16.Parse(HttpContext.Current.User.Identity.Name) = mOwnerID) _
            'OrElse (Not mIsPrivate AndAlso PortalSecurity.IsInRoles(CType(PortalSettings.GetEditModuleSettings(ModuleID), ModuleSettings).AuthorizedEditRoles.ToString) = True) Then
            'isEditable = True
            'End If
            'End If

            'Catch Exc As System.Exception
            'End Try

            ' Only run if we've got a valid filesystem path
            If mIsValidPath Then

                ' Initialize the root folder object
                mRootFolder = New GalleryFolder("-1", mGalleryTitle, _
                    mRootPath, _
                    mRootURL, _
                    "", _
                    mThumbFolder, _
                    "", _
                    "", _
                    Nothing, _
                    0, _
                    Me, _
                    mGalleryTitle, _
                    mGalleryDescription, _
                    mCategoryValues, _
                    mOwnerID, _
                    GetValue(settings("latitude"), "0"), _
                    GetValue(settings("longitude"), "0"), _
                    GetValue(settings("gpsicon"), "/images/gps/64folder.png"), _
                    GetValue(settings("gpsiconsize"), "[64,64]"), _
                    "", _
                    0)

                ' Build the cache at once if required.
                If BuildCacheonStart Then
                    'HttpContext.Current.Response.Write("<!--Populate ALL : " + mRootURL + "-->" + vbCrLf)
                    PopulateAllFolders(mRootFolder)
                Else
                    If Not mRootFolder.IsPopulated Then
                        'HttpContext.Current.Response.Write("<!--Populate ROOT : " + mRootURL + "-->" + vbCrLf)
                        'mRootFolder.Populate()
                    End If
                End If

            End If

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