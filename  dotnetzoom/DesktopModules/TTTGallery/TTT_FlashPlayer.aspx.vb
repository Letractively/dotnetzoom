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
' by Ren� Boulard ( http://www.reneboulard.qc.ca)'
'========================================================================================
Option Strict On

Namespace DotNetZoom
    Public Class TTT_GalleryFlashPlayer
        Inherits DotNetZoom.BasePage
        Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
        Protected WithEvents FLight As NetBrick.Web.FlashLight
        Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            GalleryConfig.SetSkinCSS(Page)
            Dim ModuleID As Integer = Int32.Parse(HttpContext.Current.Request("mid"))
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim _ModuleSettings As New ModuleSettings
            Dim isinrole As Boolean = False
            For Each _ModuleSettings In _portalSettings.ActiveTab.Modules
                If _ModuleSettings.ModuleId = ModuleID Then
                    If PortalSecurity.IsInRoles(CStr(IIf(_ModuleSettings.AuthorizedViewRoles <> "", _ModuleSettings.AuthorizedViewRoles, _portalSettings.ActiveTab.AuthorizedRoles))) Then
                        isinrole = True
                    End If
                End If
            Next
            If Not isinrole Then
                RegisterBADip(Request.UserHostAddress)
                Dim Admin As New AdminDB()
                ErrorMessage.Visible = True
                ErrorMessage.Text = ProcessLanguage(Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "AccessDeniedInfo"), Page)
            Else

                ErrorMessage.Visible = False
                If Not Request.Params("media") Is Nothing Then
                    lblTitle.Text = Request.Params("media").ToString
                End If

                If Not Request.Params("path") Is Nothing Then
                    FLight.Movie = Request.Params("path").ToString
                End If
            End If

        End Sub

    End Class

End Namespace