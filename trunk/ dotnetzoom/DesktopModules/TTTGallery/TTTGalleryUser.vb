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
'========================================================================================
Option Strict On

Imports DotNetZoom.TTTUtils
Imports System.Web.Caching

Namespace DotNetZoom

    Public Class GalleryUser
#Region "GalleryUser"

        Private Const GalleryUserCacheKeyPrefix As String = "GalleryUser"
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

        Private ZuserID As Integer
        Private _userName As String
        Private _email As String


      
        Public Sub New()
        End Sub 'New

        Private Sub New(ByVal UserID As Integer)

            ZuserID = UserID

            ' Grab settings from the database
            Dim dbUsers As New UsersDB()
            Dim dr As SqlDataReader = dbUsers.GetSingleUser(_portalSettings.PortalID, UserID)

            If dr.Read Then
                _userName = ConvertString(dr("UserName"))
                _email = ConvertString(dr("Email"))
            End If
            dr.Close()

        End Sub

        Public Shared Function GetGalleryUser(ByVal UserID As Integer) As GalleryUser

            ' Grab reference to the applicationstate object
			' Need to change Forum Cashing
			Dim TempKey as String = GetDBName & GalleryUserCacheKeyPrefix & CStr(UserID)
			Dim context As HttpContext = HttpContext.Current
			Dim user As GalleryUser = CType(Context.Cache(TempKey), GalleryUser)
			
            If user Is Nothing Then
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' If this object has not been instantiated yet, we need to grab it
            user = New GalleryUser(UserID)
			Context.Cache.Insert(TempKey, user, DotNetZoom.CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), Caching.CacheItemPriority.low, nothing)
            End If
            Return user

        End Function

	

#Region "User - Public Properties"

        Public Property UserID() As Integer
            Get
                Return ZuserID
            End Get
            Set(ByVal Value As Integer)
                ZuserID = Value
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return _userName
            End Get
            Set(ByVal Value As String)
                _userName = Value
            End Set
        End Property


#End Region

#End Region 'GalleryUser
    End Class 'GalleryUser

End Namespace