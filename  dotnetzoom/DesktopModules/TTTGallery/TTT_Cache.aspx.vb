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

Imports System.IO
Imports System.Text.RegularExpressions

Namespace DotNetZoom
    Public Class TTT_GalleryCache
        Inherits System.Web.UI.Page
        Protected WithEvents ContinueGO As System.Web.UI.WebControls.HyperLink

        Dim Zrequest As BaseGalleryRequest

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

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
		GalleryConfig.SetSkinCSS(Me.Page)
	
			ContinueGO.Text = GetLanguage("Gal_Cont")		
            If Not Request.Params("mid") Is Nothing Then
            Dim ModuleID As Integer = Int32.Parse(Request("mid"))
            Zrequest = New GalleryRequest(ModuleID)
			end if
            ContinueGO.NavigateUrl = NewURL()

        End Sub

        Public Function NewURL() As String
                 Return "../../" & GetLanguage("N") & ".default.aspx?" & _
                    "&tabid=" & Request("tabid") & _
                    "&path=" & Request("path")
        End Function

        Public Sub BuildCache()

            Zrequest.Folder.Populate()

        End Sub

    End Class

End Namespace