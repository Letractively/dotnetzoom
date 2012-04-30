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
    Public Class TTT_MediaPlayer
        Inherits DotNetZoom.PortalModuleControl
        Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
        Protected WithEvents btnBack As System.Web.UI.WebControls.Button
        Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
		Protected WithEvents flv As System.Web.UI.HtmlControls.HtmlTableCell
		Protected WithEvents wmv As System.Web.UI.HtmlControls.HtmlTableCell
		Protected WithEvents mpg As System.Web.UI.HtmlControls.HtmlTableCell

		
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
            'Put user code to initialize the page here

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			btnBack.text = GetLanguage("return")
            ErrorMessage.Visible = False
            If Not Request.Params("media") Is Nothing Then
                lblTitle.Text = Request.Params("media").ToString
	           Dim StrExtension As String = lblTitle.Text
               If InStr(1, lblTitle.Text, ".") <> 0 Then
               StrExtension = Mid(lblTitle.Text, InStrRev(lblTitle.Text, ".")).ToLower
               Select Case StrExtension.Tolower()
                        Case ".flv", ".mp4"
                            flv.Visible = True
					Case ".wmv"
					wmv.visible = True
					Case else
					mpg.visible = True
				End Select
               End If

        
			
				
            End If
            If Not Page.IsPostBack Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If
            End If

        End Sub

        Public Function MovieURL() As String
            Return Request.Params("path")
        End Function

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub
    End Class
End Namespace