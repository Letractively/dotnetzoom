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
Imports System.xml
Imports System.Text
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom
    Public Class TTT_Slideshow
        Inherits DotNetZoom.PortalModuleControl
        Protected WithEvents ClientJavascript As System.Web.UI.WebControls.Label
        Protected WithEvents ImageSrc As System.Web.UI.WebControls.Label
        Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents btnBack As System.Web.UI.WebControls.Button
        Protected WithEvents img As System.Web.UI.HtmlControls.HtmlTableCell

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

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			btnBack.text = GetLanguage("return")
		
            ErrorMessage.Visible = False

            Dim TabID As Integer = Int32.Parse(HttpContext.Current.Request("tabid"))
            Dim Zconfig As GalleryConfig = GalleryConfig.GetGalleryConfig(ModuleId)
            Dim Zrequest As GalleryRequest = New GalleryRequest(ModuleId)
            If Not Zconfig.RootFolder.IsPopulated Then
                'Response.Redirect(Request.RawUrl)
            End If

            If Not Zrequest.Folder.IsPopulated Then
                ' Zrequest.Folder.Populate()
                ' Server.Transfer("~/DesktopModules/tttGallery/TTT_cache.aspx")
                'Response.Redirect(Request.RawUrl)
            End If


            If Zrequest.Folder.IsBrowsable Then

        Dim albumPath As String = Zrequest.Folder.Path
        Dim SlideSpeed As String = CType(Zrequest.GalleryConfig.SlideshowSpeed, String)
        Dim sb As New StringBuilder()

        Dim StartImage As String = ""

        'Generate Clientside Javascript for Slideshow
        Dim JavaScript As String = ""
        Dim Count As Integer

                sb.Append("<script language='javascript'>")
                sb.Append(vbCrLf)
                sb.Append("var slideShowSpeed = ")
                sb.Append(SlideSpeed)
                sb.Append(vbCrLf)
                sb.Append("var Pic = new Array()")
                sb.Append(vbCrLf)
                sb.Append("var Title = new Array()")
                sb.Append(vbCrLf)
                sb.Append("var Description = new Array()")
                sb.Append(vbCrLf)

        Dim item As Object
        Dim image As GalleryFile


                For Each item In Zrequest.FileItems
                    image = CType(item, GalleryFile)
                    If image.Type = IGalleryObjectInfo.ItemType.Image Then
        'StartImage = CryptoUrl(image.URL, Zrequest.GalleryConfig.IsPrivate)
                        StartImage = image.URL
                        sb.Append("Pic[")
                        sb.Append(Count)
        'BRT: sb.Append("] = ')
                        sb.Append("] = """)
        'sb.Append(CryptoUrl(image.URL, Zrequest.GalleryConfig.IsPrivate))
                        sb.Append(image.URL)
        'BRT: sb.Append("'")
                        sb.Append("""")
                        sb.Append(vbCrLf)
                        sb.Append("Title[")
                        sb.Append(Count)
        'BRT: sb.Append("] = '")
                        sb.Append("] = """)
                        sb.Append(JSEncode(image.Title.Replace(vbCrLf, "<br>")))
        'BRT: sb.Append("'")
                        sb.Append("""")
                        sb.Append(vbCrLf)
                        sb.Append("Description[")
                        sb.Append(Count)
        'BRT: sb.Append("] = '")
                        sb.Append("] = """)
                        sb.Append(JSEncode(image.Description.Replace(vbCrLf, "<br>")))
        'BRT: sb.Append("'")
                        sb.Append("""")
                        sb.Append(vbCrLf)
                        Count = Count + 1
                    End If
                Next

                sb.Append("var t" & vbCrLf)
                sb.Append("var j = 0" & vbCrLf)
                sb.Append("var p = Pic.length")
                sb.Append(vbCrLf)

                sb.Append("var preLoad = new Array()")
                sb.Append(vbCrLf)
                sb.Append("for (i = 0; i < p; i++){")
                sb.Append(vbCrLf)
                sb.Append("preLoad[i] = new Image()")
                sb.Append(vbCrLf)
                sb.Append("preLoad[i].src = Pic[i]")
                sb.Append(vbCrLf)
                sb.Append("}")
                sb.Append(vbCrLf)
                sb.Append("</script>")

                ClientJavascript.Text = sb.ToString
                ImageSrc.Text = "<img src=" & StartImage & " name='SlideShow' heigth=100% style='border-color:#D1D7DC;border-width:2px;border-style:Outset;'>"

            Else
                ErrorMessage.Visible = True
                ErrorMessage.Text = GetLanguage("Gal_NoImage")
            End If
            If Not Page.IsPostBack Then
        ' Store URL Referrer to return to portal
                img.Height = Zrequest.GalleryConfig.FixedHeight.ToString
                img.Width = Zrequest.GalleryConfig.FixedWidth.ToString

                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If
            End If
        End Sub



        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Response.Redirect(ViewState("UrlReferrer").ToString)
        End Sub
    End Class
End Namespace