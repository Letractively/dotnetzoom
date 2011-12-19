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

Imports DotNetZoom.TTTUtils
Imports System.Web.Caching

Namespace DotNetZoom

    Public Class GalleryUser
#Region "GalleryUser"

        Public Const GalleryUserCacheKeyPrefix As String = "GalleryUser"
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

        Private _userID As Integer
        Private _userName As String



        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal UserID As Integer)

            _userID = UserID

            ' Grab settings from the database
            Dim dbUsers As New UsersDB()
            Dim dr As SqlDataReader = dbUsers.GetSingleUser(_portalSettings.PortalId, UserID)

            If dr.Read Then
                _userName = ConvertString(dr("UserName"))
            End If
            dr.Close()

        End Sub

        Public Shared Function GetGalleryUser(ByVal UserID As Integer) As GalleryUser

            ' Grab reference to the applicationstate object
            ' Need to change Forum Cashing
            Dim TempKey As String = GetDBname() & GalleryUserCacheKeyPrefix & CStr(UserID)
            Dim context As HttpContext = HttpContext.Current
            Dim user As GalleryUser = CType(context.Cache(TempKey), GalleryUser)

            If user Is Nothing Then
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                ' If this object has not been instantiated yet, we need to grab it
                user = New GalleryUser(UserID)
                context.Cache.Insert(TempKey, user, DotNetZoom.CDp(_portalSettings.PortalId, _portalSettings.ActiveTab.TabId), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), Caching.CacheItemPriority.Low, Nothing)
            End If
            Return user

        End Function



#Region "User - Public Properties"

        Public Property UserID() As Integer
            Get
                Return _userID
            End Get
            Set(ByVal Value As Integer)
                _userID = Value
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