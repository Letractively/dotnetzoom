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

Namespace DotNetZoom
    Public Class TTT_GalleryDispatch
        Inherits PortalModuleControl

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim _DefaultPage As String = glbPath & "DeskTopModules/TTTGallery/TTT_Gallery.ascx"

        Public Enum GalleryDesktopType
            GalleryMain
            GalleryBrowser
            GallerySlideshow
            GalleryMediaPlayer
            GalleryFlashPlayer
            GalleryGPS
        End Enum

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Dim config As GalleryConfig = GalleryConfig.GetGalleryConfig(ModuleId)
            If config.CheckboxGPS Then
                _DefaultPage = glbPath & "DeskTopModules/TTTGallery/GPS_Map.ascx"
            End If
            'If config.IsValidPath Then
            'Dim Zrequest As GalleryRequest = New GalleryRequest(ModuleId)
            'End If

            If IsNumeric(Request.Params("mid")) Then
                If Request.Params("mid") = ModuleId.ToString Then
                    If IsNumeric(Request.Params("gallerypage")) Then
                        Dim GalleryPage As Integer = CInt(Request.Params("gallerypage"))
                        Select Case GalleryPage
                            Case GalleryDesktopType.GalleryMain
                                _DefaultPage = glbPath & "DeskTopModules/TTTGallery/TTT_Gallery.ascx"
                            Case GalleryDesktopType.GalleryBrowser
                                _DefaultPage = glbPath & "DeskTopModules/TTTGallery/TTT_Viewer.ascx"
                            Case GalleryDesktopType.GallerySlideshow
                                _DefaultPage = glbPath & "DeskTopModules/TTTGallery/TTT_Slideshow.ascx"
                            Case GalleryDesktopType.GalleryMediaPlayer
                                _DefaultPage = glbPath & "DeskTopModules/TTTGallery/TTT_MediaPlayer.ascx"
                            Case GalleryDesktopType.GalleryFlashPlayer
                                _DefaultPage = glbPath & "DeskTopModules/TTTGallery/TTT_FlashPlayer.ascx"
                            Case GalleryDesktopType.GalleryGPS
                                _DefaultPage = glbPath & "DeskTopModules/TTTGallery/GPS_Map.ascx"

                        End Select
                    End If
                End If
            End If

            Dim objModule As PortalModuleControl = CType(CType(Me.Page, BasePage).LoadModule(_DefaultPage), PortalModuleControl)
            If Not objModule Is Nothing Then
                objModule.ModuleConfiguration = Me.ModuleConfiguration
                Controls.Add(objModule)
            End If
            GalleryConfig.SetSkinCSS(Me.Page)


        End Sub

    End Class
End Namespace