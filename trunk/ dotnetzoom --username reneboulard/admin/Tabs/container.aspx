<%@ Page Language="VB" Debug="false" Inherits="DotNetZoom.BasePage"%>
<%@ Import Namespace="DotNetZoom" %>
<%@ Import Namespace="DotNetZoom.PortalModuleControl" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import NameSpace="System.Drawing.Drawing2D" %>
<%@ Import NameSpace="System.Drawing" %>

<script runat="server">
 
 
    Private NoImagesMessage As String = GetLanguage("NoImagesMessage")
    Private ImageUrl As String = "~/images/containers/"
Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"

Annuler.Attributes.Add("Onclick", "JavaScript:window.top.close();window.top.opener.focus();")
Annuler.Text = GetLanguage("return")
If Request.IsAuthenticated = false Then
   Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Edit Access Denied", True)
End If
DisplayImages()
End Sub 


        Public Sub ResizeImage(ByVal Source As String, ByVal Destination As String, ByVal MaxWidth As Integer, ByVal MaxHeight As Integer, ByVal Extension As String, ByVal Quality As Integer)
            Dim lWidth As Integer
            Dim lHeight As Integer
            Dim newWidth As Integer
            Dim newHeight As Integer
            Dim sRatio As Double
            Dim mImage As System.Drawing.Image 
            Dim iFormat As Imaging.ImageFormat = ImageFormat.Bmp
			Dim myEncoders() As ImageCodecInfo
			myEncoders = ImageCodecInfo.GetImageEncoders()
			Dim numEncoders As Integer = myEncoders.GetLength(0)
			Dim strNumEncoders As String = numEncoders.ToString()
			Dim i As Integer = 0
			Dim z as Integer = -1

			If Quality < 20 then 
			Quality = 20
			else
			If quality > 80 then
			Quality = 80
			end if
			end if
			
            Select Case LCase(Replace(Extension, ".", ""))
                Case "bmp"
                    iFormat = ImageFormat.Bmp
                Case "jpg"
                    iFormat = ImageFormat.Jpeg
					If numEncoders > 0 Then
					For i = 0 To numEncoders - 1
					if MyEncoders(i).MimeType = "image/jpeg" then
					z = i
					End if
					Next i
					End If
                Case "gif"
                    iFormat = ImageFormat.Gif
                Case "tif"
                    iFormat = ImageFormat.Tiff
                Case "png"
                    iFormat = ImageFormat.Png
            End Select

            Try
                mImage = System.Drawing.Image.FromFile(Source)
                lWidth = mImage.Width
                lHeight = mImage.Height

                If Not (lWidth <= MaxWidth AndAlso lHeight <= MaxHeight) Then
                    sRatio = (lHeight / lWidth)
                    If sRatio > (MaxHeight / MaxWidth) Then ' Bounded by height
					    newWidth = CShort(MaxHeight / sRatio)
                        newHeight = MaxHeight
                    Else 'Bounded by width
                        newWidth = MaxWidth
                        newHeight = CShort(MaxWidth * sRatio)
                    End If
 				  	Dim newImage as new System.Drawing.Bitmap(newWidth,newHeight)
					Dim g As Graphics = Graphics.FromImage(newImage)
 					g.InterpolationMode = InterpolationMode.HighQualityBicubic
   					g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
   					g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
   					Dim rect As New Rectangle(0, 0, newWidth,newHeight)
   					g.DrawImage(mImage, rect, 0, 0, mImage.width, mImage.height, GraphicsUnit.Pixel)

					
					Dim PropItm As PropertyItem
					For Each PropItm In mImage.PropertyItems
   						newImage.SetPropertyItem(PropItm)
					Next
		if z <> - 1 then
		Dim encoderInstance As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
		Dim encoderParametersInstance As EncoderParameters = New EncoderParameters (2)
		Dim encoderParameterInstance As EncoderParameter = New EncoderParameter (encoderInstance, Quality)
		encoderParametersInstance.Param(0) = encoderParameterInstance
		encoderInstance = System.Drawing.Imaging.Encoder.ColorDepth
		encoderParameterInstance = New EncoderParameter (encoderInstance, 24)
		encoderParametersInstance.Param(1) = encoderParameterInstance
		newImage.Save(Destination, MyEncoders(z), encoderParametersInstance)
		else
		newImage.Save(Destination, iFormat)
		end if
                newImage.Dispose()
                Else
		if z <> - 1 then
		Dim encoderInstance As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
		Dim encoderParametersInstance As EncoderParameters = New EncoderParameters (2)
		Dim encoderParameterInstance As EncoderParameter = New EncoderParameter (encoderInstance, Quality)
		encoderParametersInstance.Param(0) = encoderParameterInstance
		encoderInstance = System.Drawing.Imaging.Encoder.ColorDepth
		encoderParameterInstance = New EncoderParameter (encoderInstance, 24)
		encoderParametersInstance.Param(1) = encoderParameterInstance
		mImage.Save(Destination, MyEncoders(z), encoderParametersInstance)
		else
		mImage.Save(Destination, iFormat)
		end if
        End If
        mImage.Dispose()

            Catch ex As Exception
			ResultsMessage.text = GetLanguage("ErrorGDI")
            Throw ex
            End Try

        End Sub





Public Sub DisplayImages() 


' AppPath = HttpContext.Current.Request.PhysicalApplicationPath 
        Dim FilesArray() As String = Nothing
 Dim NumOfImag As Integer = 1

 Dim strFolder As String = Request.MapPath("~/images/containers/")
				
 If System.IO.Directory.Exists(strFolder) Then
    FilesArray = System.IO.Directory.GetDirectories(strFolder)
 end if

 
 
 GalleryPlaceHolder.Controls.Clear() 
  GalleryPlaceHolder.Controls.Add(New LiteralControl("<table style=""font-size: 8pt"" border=""0"" align=""center"" cellpadding=""5"" cellspacing=""5""><tr>"))
 
        If (FilesArray Is Nothing) Then
            GalleryPlaceHolder.Controls.Add(New LiteralControl("<td>"))
            GalleryPlaceHolder.Controls.Add(New LiteralControl("</td>"))

            ResultsMessage.Text = NoImagesMessage
        Else
            Dim ImageFileName As String = ""
            Dim ContainerText As String = ""
            Dim ImageFileLocation As String = ""
            Dim thumbWidth As Integer = 375
            Dim thumbHeight As Integer = 375
  
            GalleryPlaceHolder.Controls.Add(New LiteralControl("<td width=""375"" align=""center""  style=""height: 137px;background-color: #e3e3c7  ;"" valign=""middle"" ondblclick=""OpenFile('');""><br><h2>VIDE<h2><br></td>"))
   
   
            For Each ImageFile As String In FilesArray
                Dim dir As New System.IO.DirectoryInfo(ImageFile)
                ImageFileName = ImageFile.ToString() & "\preview.jpg"

                Dim myImage As System.Drawing.Image = System.Drawing.Image.FromFile(ImageFileName.ToString())

                ContainerText = dir.Name
	   
                Dim myHtmlImage As System.Web.UI.HtmlControls.HtmlImage = New System.Web.UI.HtmlControls.HtmlImage()
                myHtmlImage.Src = ImageUrl + dir.Name + "/preview.jpg"
                myHtmlImage.Alt = dir.Name
                myHtmlImage.Attributes.Add("Title", dir.Name)
                If NumOfImag = 2 Then
                    GalleryPlaceHolder.Controls.Add(New LiteralControl("</td></tr><tr>"))
                    NumOfImag = 0
                End If
                NumOfImag += 1
                GalleryPlaceHolder.Controls.Add(New LiteralControl("<td width=""375px"" align=""center""  style=""height: 137px;background-color:#f1f1e3;"" valign=""middle"">"))

	   
                If myImage.Width > myImage.Height Then
                    If myImage.Width > thumbWidth Then
                        myHtmlImage.Width = thumbWidth
                        myHtmlImage.Height = Convert.ToInt32(myImage.Height * thumbWidth / myImage.Width)
                    Else
                        myHtmlImage.Width = myImage.Width
                        myHtmlImage.Height = myImage.Height
                    End If
                Else
                    If myImage.Height > thumbHeight Then
                        myHtmlImage.Height = thumbHeight
                        myHtmlImage.Width = Convert.ToInt32(myImage.Width * thumbHeight / myImage.Height)
                    Else
                        myHtmlImage.Width = myImage.Width
                        myHtmlImage.Height = myImage.Height
                    End If
                End If
                myHtmlImage.Attributes("ondblclick") = "OpenFile('" + ContainerText + "');"
                myHtmlImage.Attributes("onmouseover") = DotNetZoom.ReturnToolTip(DotNetZoom.GetLanguage("TwoClickToSelect"))
                GalleryPlaceHolder.Controls.Add(myHtmlImage)
                GalleryPlaceHolder.Controls.Add(New LiteralControl("</td>"))
                myImage.Dispose()
            Next
        End If
  GalleryPlaceHolder.Controls.Add(New LiteralControl("</tr></table>" ))

End Sub


	  

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title><%= DotNetZoom.GetLanguage("Add_Icone") %></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
<asp:literal id="StyleSheet" enableviewstate="false" runat="server" />
<style type="text/css">
.Main {overflow: auto; height:550px; vertical-align : top;}
</style>
<script language="javascript">

function OpenFile( ContainerInfo )
{
	window.top.close() ;
	window.top.opener.focus() ;
	window.top.opener.SetContainer(ContainerInfo) ;

}
</script>	

</head>
<body>
<form id="Form1" method="post" runat="server">
<p>
&nbsp;&nbsp;&nbsp;<asp:button id="Annuler" runat="server" />
<asp:literal id="ResultsMessage" runat="server" /></p>
<div Class="Main">
<asp:PlaceHolder id="GalleryPlaceHolder" runat="server"></asp:PlaceHolder>
</div>
</form>
</body>
</html>