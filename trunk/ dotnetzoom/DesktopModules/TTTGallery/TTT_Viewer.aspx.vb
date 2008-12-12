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

Imports System.IO
Imports System.Text

Namespace DotNetZoom
    Public Class TTT_GalleryViewer
        Inherits System.Web.UI.Page
        Protected WithEvents Title1 As System.Web.UI.WebControls.Label
        Protected WithEvents Image As System.Web.UI.WebControls.Image
        Protected WithEvents Description As System.Web.UI.WebControls.Label
        Protected WithEvents MovePrevious As System.Web.UI.WebControls.HyperLink
        Protected WithEvents Status As System.Web.UI.WebControls.Label
        Protected WithEvents ImageInfo As System.Web.UI.WebControls.Label
        Protected WithEvents MoveNext As System.Web.UI.WebControls.HyperLink
        Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents cmdAlbum As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblTile As System.Web.UI.WebControls.Label
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
        Protected WithEvents Album As System.Web.UI.WebControls.Label

        Protected Zrequest As GalleryViewerRequest

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

			Dim objCSS As Control = page.FindControl("CSS")
			Dim objTTTCSS As Control = page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If (Not objCSS Is Nothing) and (objTTTCSS Is Nothing) Then
                    ' put in the ttt.css
					objLink = New System.Web.UI.LiteralControl("TTTCSS")
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                    objCSS.Controls.Add(objLink)
            End If
			
			
            Dim ModuleID As Integer = Int32.Parse(HttpContext.Current.Request("mid"))

            Zrequest = New GalleryViewerRequest(ModuleID)

            If Not Zrequest.Folder.IsPopulated Then
                Server.Transfer("TTT_cache.aspx")
            End If

            Dim sb As New StringBuilder()

            ' Set the backward nav
            sb.Append("TTT_viewer.aspx?L=" & GetLanguage("N") & "&path=")
            sb.Append(Zrequest.Path)
            sb.Append("&currentitem=")
            sb.Append(CStr(Zrequest.PreviousItem))
            sb.Append("&mid=")
            sb.Append(ModuleID.ToString)
            sb.Append("&tabid=")
            sb.Append(_portalSettings.ActiveTab.TabId)

            MovePrevious.NavigateUrl = sb.ToString
            sb.Length = 0

            ' Set forward nav
            sb.Append("TTT_viewer.aspx?L=" & GetLanguage("N") & "&path=")
            sb.Append(Zrequest.Path)
            sb.Append("&tabid=")
            sb.Append(_portalSettings.ActiveTab.TabId)
            sb.Append("&currentitem=")
            sb.Append(CStr(Zrequest.NextItem))
            sb.Append("&mid=")
            sb.Append(ModuleID.ToString)
            MoveNext.NavigateUrl = sb.ToString
            sb.Length = 0

            ' Image properties
            Album.Text = Zrequest.Folder.Name
            Image.ImageUrl = Zrequest.CurrentItem.URL
            Dim config As GalleryConfig = GalleryConfig.GetGalleryConfig(ModuleID)

            If config.InfoBule Then
                ' Image properties
                Dim TempTooltip As String
                If File.Exists(Server.MapPath(Image.ImageUrl)) Then
                    Dim Exif As New ExifWorks(Server.MapPath(Image.ImageUrl))
                    TempTooltip = Exif.ToString()
                    If TempTooltip <> "" Then
                        Image.Attributes.Add("onmouseover", ReturnToolTip("<pre>" & TempTooltip & "</pre>"))
                    End If
                    Exif.Dispose()
                End If
            End If


            
            Title1.Text = Zrequest.CurrentItem.Title.Replace(vbCrLf, "<br>")
            
            Description.Text = Zrequest.CurrentItem.Description.Replace(vbCrLf, "<br>")

            ' image info
            'Image {imgnum} de {albumnum} {imgname} ({imgsize}KB) 

            lblInfo.Text = Replace(GetLanguage("Gal_ImgInfo"), "{imgnum}" , Zrequest.CurrentItemNumber.ToString())
            lblInfo.Text = Replace(lblInfo.Text, "{albumnum}" , Zrequest.Folder.BrowsableItems.Count.ToString())
            lblInfo.Text = Replace(lblInfo.Text, "{imgname}" , Zrequest.CurrentItem.Name)
	        lblInfo.Text = Replace(lblInfo.Text, "{imgsize}" , Math.Ceiling(Zrequest.CurrentItem.Size / 1024).ToString)


        End Sub

    End Class

End Namespace