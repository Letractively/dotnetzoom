'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Imports System.IO

Namespace DotNetZoom

    Public MustInherit Class ImageModule
        Inherits DotNetZoom.PortalModuleControl


        Public LinkURL As String = "/"
        Public GoogleMapURL As String = "/"

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal
        Protected WithEvents imgmenu As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents imgImage As System.Web.UI.WebControls.Image
        Protected WithEvents Image1 As System.Web.UI.WebControls.Image

        Protected WithEvents GoogleEarth As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents GoogleMap As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents Info As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents infoExif As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents Link As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents TD1 As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents cmdSendKML As System.Web.UI.WebControls.LinkButton

		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

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

        '*******************************************************
        '
        ' The Page_Load event handler on this User Control uses
        ' the Portal configuration system to obtain image details.
        ' It then sets these properties on an <asp:Image> server control.
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

 

   			Title1.EditText = getlanguage("editer")
            Dim imageSrc As String = CType(Settings("src"), String)

            ' Set Image Source, Width and Height Properties
            If Not (imageSrc Is Nothing) And imageSrc <> "" Then

                imgmenu.Visible = CType(Settings("show"), Boolean) Or _
                  CType(Settings("position"), Boolean) Or _
                    CType(Settings("optGoogleEarth"), Boolean) Or _
                    CType(Settings("optExif"), Boolean) Or _
                    CType(Settings("optInternalLink"), Boolean)


                If InStr(1, imageSrc, "://") = 0 Then
                    imageSrc = _portalSettings.UploadDirectory & imageSrc

                    If CType(Settings("optExif"), Boolean) And CType(Settings("Exif"), String) <> "" Then
                        infoExif.Attributes.Add("onmouseover", ReturnToolTip(CType(Settings("Exif"), String), "250"))
                        infoExif.Visible = True
                    End If
                    If CType(Settings("secure"), Boolean) Then
                        Dim objSecurity As New PortalSecurity()
                        imgImage.ImageUrl = objSecurity.EncryptURL(Application("cryptokey"), imageSrc)
                        'imgImage.ImageUrl = glbPath + "controls/img.ashx?" & crypto
                    Else
                        imgImage.ImageUrl = imageSrc
                    End If
                Else
                    imgImage.ImageUrl = imageSrc
                End If

                If CType(Settings("show"), Boolean) And CType(Settings("infobule"), String) <> "" Then
                    Info.Visible = True
                    Info.Attributes.Add("onmouseover", ReturnToolTip(CType(Settings("infobule"), String)))
                End If


                If CType(Settings("position"), Boolean) And CType(Settings("latlong"), String) <> "" Then
                    GoogleMap.Visible = True
                    GoogleMap.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("ShowOnMap")))
                    GoogleMapURL = """javascript:DestroyWnd;CreateWnd('" + CType(Settings("latlong"), String) + "',640,640,false)"""
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "POPUPScript", "<script language=""javascript"" type=""text/javascript"" src=""" + DotNetZoom.glbPath + "javascript/popup.js""></script>")
                End If

                If CType(Settings("optGoogleEarth"), Boolean) And CType(Settings("fileGPS"), String) <> "" Then
                    GoogleEarth.Visible = True
                    GoogleEarth.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("DownloadFile")))
                    cmdSendKML.CommandArgument = CType(Settings("fileGPS"), String)
                End If
                If CType(Settings("optInternalLink"), Boolean) And (CType(Settings("ExtLink"), String) <> "" Or CType(Settings("link"), String) <> "-1") Then
                    Link.Visible = True
                    If CType(Settings("link"), String) <> "-1" And IsNumeric(CType(Settings("link"), String)) Then
                        Dim objAdmin As New AdminDB()
                        Dim dr As SqlDataReader = objAdmin.GetTabById(CType(Settings("link"), Integer), GetLanguage("N"))
                        Dim Objtab As New TabStripDetails()
                        Objtab.ShowFriendly = False
                        Objtab.FriendlyTabName = ""
                        While dr.Read
                            Objtab.ShowFriendly = Boolean.Parse(dr("ShowFriendly").ToString)
                            Objtab.FriendlyTabName = IIf(IsDBNull(dr("FriendlyTabName")), "", dr("FriendlyTabName"))
                        End While
                        dr.Close()
                        LinkURL = FormatFriendlyURL(Objtab.FriendlyTabName, Objtab.ssl, Objtab.ShowFriendly, CType(Settings("link"), String))
                    Else
                        LinkURL = CType(Settings("ExtLink"), String)
                    End If
                    If CType(Settings("InfoLink"), String) <> "" Then
                        Link.Attributes.Add("onmouseover", ReturnToolTip(CType(Settings("InfoLink"), String)))
                    Else
                        Link.Attributes.Add("onmouseover", ReturnToolTip(LinkURL))
                    End If



                End If




                If CType(Settings("alt"), String) <> "" Then
                    imgImage.AlternateText = CType(Settings("alt"), String)
                End If
                If CType(Settings("width"), String) <> "" Then
                    imgImage.Width = Unit.Pixel(CType(Settings("width"), Integer))
                    TD1.Width = CType(Settings("width"), String)
                End If
                If CType(Settings("height"), String) <> "" Then
                    imgImage.Height = Unit.Pixel(CType(Settings("height"), Integer))
                End If
            Else
                imgImage.Visible = False
            End If

            Image1.Height = imgImage.Height
            Image1.Width = imgImage.Width
            Image1.AlternateText = imgImage.AlternateText
            Image1.ImageUrl = imgImage.ImageUrl

            imgImage.Visible = Not imgmenu.Visible

        End Sub

        Private Sub cmdSendKML_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSendKML.Click
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim FileName As String = _portalSettings.UploadDirectory + cmdSendKML.CommandArgument
            Dim strExtension As String = Replace(System.IO.Path.GetExtension(cmdSendKML.CommandArgument), ".", "")
            If InStr(1, "," & glbGoogleEarthTypes, "," & strExtension.ToLower) <> 0 Then
                ' force download dialog
                Response.Clear()

                Response.AppendHeader("content-disposition", "attachment; filename=" + cmdSendKML.CommandArgument)

                Select Case strExtension.ToLower()
                    Case "kmz"
                        Response.ContentType = "Application/vnd.google-earth.kmz"
                    Case "kml"
                        Response.ContentType = "application/vnd.google-earth.kml+xml"
                    Case "gpx"
                        Response.ContentType = "application/gpx"
                End Select
                Response.WriteFile(FileName)
                Response.End()
            End If
        End Sub

    End Class

End Namespace