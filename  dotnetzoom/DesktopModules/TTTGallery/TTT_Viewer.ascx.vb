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
Imports System.Text

Namespace DotNetZoom
    Public Class TTT_Viewer
        Inherits DotNetZoom.PortalModuleControl
        Protected WithEvents MovePrevious As System.Web.UI.WebControls.HyperLink
        Protected WithEvents Title As System.Web.UI.WebControls.Label
        Protected WithEvents MoveNext As System.Web.UI.WebControls.HyperLink
        Protected WithEvents Image As System.Web.UI.WebControls.Image
        Protected WithEvents cmdBack As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
        Protected WithEvents img As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents info As System.Web.UI.WebControls.Image
        Protected Zrequest As GalleryViewerRequest
        Protected Zconfig As GalleryConfig

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

			cmdBack.Text = GetLanguage("return")
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim tabID As Integer = Int32.Parse(HttpContext.Current.Request("tabid"))

            Zconfig = GalleryConfig.GetGalleryConfig(ModuleId)
            Zrequest = New GalleryViewerRequest(ModuleId)
            If Not Zconfig.RootFolder.IsPopulated Then
                'Response.Redirect(Request.RawUrl)
            End If

            If Not Zrequest.Folder.IsPopulated Then
                ' Zrequest.Folder.Populate()
                ' Server.Transfer("~/DesktopModules/tttGallery/TTT_cache.aspx")
                'Response.Redirect(Request.RawUrl)
            End If



            Dim sb As New StringBuilder()

            ' Set the backward nav
            sb.Append(GetFullDocument() & "?tabid=")
            sb.Append(tabID)
            sb.Append("&gallerypage=")
            sb.Append(TTT_GalleryDispatch.GalleryDesktopType.GalleryBrowser)
            sb.Append("&currentitem=")
            sb.Append(CStr(Zrequest.PreviousItem))
            sb.Append("&path=")
            sb.Append(Zrequest.Path)
            sb.Append("&mid=" & ModuleId.ToString)

            MovePrevious.NavigateUrl = sb.ToString
            sb.Length = 0

            ' Set forward nav
            sb.Append(GetFullDocument() & "?tabid=")
            sb.Append(tabID)
            sb.Append("&gallerypage=")
            sb.Append(TTT_GalleryDispatch.GalleryDesktopType.GalleryBrowser)
            sb.Append("&currentitem=")
            sb.Append(CStr(Zrequest.NextItem))
            sb.Append("&path=")
            sb.Append(Zrequest.Path)
            sb.Append("&mid=" & ModuleId.ToString)

            MoveNext.NavigateUrl = sb.ToString
            sb.Length = 0


            Dim config As GalleryConfig = GalleryConfig.GetGalleryConfig(ModuleId)


            Try
                ' Image.ImageUrl = CryptoUrl(Zrequest.CurrentItem.URL, config.IsPrivate)
                img.Height = CStr(Zrequest.GalleryConfig.FixedHeight + 4)
                img.Width = CStr(Zrequest.GalleryConfig.FixedWidth + 4)
                Image.ImageUrl = Zrequest.CurrentItem.URL
                If Zrequest.CurrentItem.Width <> "0" Then
                    CalculatePhotoSize()
                End If

            Catch ex As Exception
                ClearModuleCache(ModuleId)
                Response.Clear()
                Response.End()
            End Try



			If config.InfoBule Then
                ' Image properties

                If File.Exists(Server.MapPath(Zrequest.CurrentItem.URL)) Then
                    Dim Exif As New ExifWorks(Server.MapPath(Zrequest.CurrentItem.URL))
                    Dim TempTooltip As String
                    TempTooltip = Exif.ToString()
                    If TempTooltip <> "" Then
                        info.Visible = True
                        info.Attributes.Add("onmouseover", ReturnToolTip(TempTooltip, "200"))
                    End If
                    Exif.Dispose()
                    If Zrequest.CurrentItem.Description <> "" Then
                        Image.Attributes.Add("onmouseover", ReturnToolTip(Zrequest.CurrentItem.Description))
                    End If
                End If
            End If
            Title.Text = Zrequest.CurrentItem.Title.Replace(vbCrLf, "<br>")


            ' image info
            'Image {imgnum} de {albumnum} {imgname} ({imgsize}KB) 

            lblInfo.Text = Replace(GetLanguage("Gal_ImgInfo"), "{imgnum}", Zrequest.CurrentItemNumber.ToString())
            lblInfo.Text = Replace(lblInfo.Text, "{albumnum}", Zrequest.Folder.BrowsableItems.Count.ToString())
            lblInfo.Text = Replace(lblInfo.Text, "{imgname}", Zrequest.CurrentItem.Name)
            lblInfo.Text = Replace(lblInfo.Text, "{imgsize}", Math.Ceiling(Zrequest.CurrentItem.Size / 1024).ToString)

            sb.Length = 0

            sb.Append(GetFullDocument() & "?tabid=")
            sb.Append(tabID)
            sb.Append("&path=")
            sb.Append(Zrequest.Path)
            sb.Append("&currentstrip=" & Zrequest.currentstrip)
            cmdBack.NavigateUrl = sb.ToString

        End Sub

        Private Sub CalculatePhotoSize()
            Dim lWidth As Integer = CInt(Zrequest.CurrentItem.Width)
            Dim lHeight As Integer = CInt(Zrequest.CurrentItem.Height)
            Dim newWidth As Integer = lWidth
            Dim newHeight As Integer = lHeight
            Dim sRatio As Double

            If (lWidth > Zrequest.GalleryConfig.FixedWidth Or lHeight > Zrequest.GalleryConfig.FixedHeight) Or (lWidth < Zrequest.GalleryConfig.FixedWidth And lHeight < Zrequest.GalleryConfig.FixedHeight) Then
                sRatio = (lHeight / lWidth)
                If sRatio > 1 Then ' Bounded by height
                    newWidth = CShort(Zrequest.GalleryConfig.FixedHeight / sRatio)
                    newHeight = Zrequest.GalleryConfig.FixedHeight
                Else 'Bounded by width
                    newWidth = Zrequest.GalleryConfig.FixedWidth
                    newHeight = CShort(Zrequest.GalleryConfig.FixedWidth * sRatio)
                End If
            End If
            Image.Width = Unit.Pixel(CInt(newWidth))
            Image.Height = Unit.Pixel(CInt(newHeight))

        End Sub
    End Class
End Namespace