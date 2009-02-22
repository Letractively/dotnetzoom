<%@ Page Language="VB" Debug="false" Inherits="DotNetZoom.BasePage"%>
<%@ Import Namespace="DotNetZoom" %>
<%@ Import Namespace="DotNetZoom.PortalModuleControl" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import NameSpace="System.Drawing.Drawing2D" %>
<%@ Import NameSpace="System.Drawing" %>

<script runat="server">
 
 
Private NoFileMessage As String = GetLanguage("NoFileMessage") 
Private UploadSuccessMessage As String = GetLanguage("UploadSuccessMessage")
Private NoImagesMessage As String = GetLanguage("NoImagesMessage")
Private NoFolderSpecifiedMessage As String = GetLanguage("NoFolderSpecifiedMessage")
Private NoSpaceLeft As String = GetLanguage("NoSpaceLeft") 
Private NoFileToDeleteMessage As String =  GetLanguage("NoFileToDeleteMessage") 
Private InvalidFileTypeMessage As String = GetLanguage("InvalidFileTypeMessage") 
Private AcceptedFileTypes As String = "jpg gif png tif bmp"  
Private ImagesFolder As String = ""
Private ImageUrl As String = ""
Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"

If Not Page.IsPostBack then
TxtQuality.Text = "80"
txtSizeH.Text = "200"
txtSizeL.Text = "200"
Range1.Text = Getlanguage("ImageQualityRange")
Range2.Text = Getlanguage("ImageRangeToSave800")
Range3.Text = Getlanguage("ImageRangeToSave800")
ResultsMessage.text = ""
end if

 
   If CType((Session("FCKeditor:UserFilesPath")), String) <> "" then
   ImagesFolder = CType((Session("FCKeditor:UserFilesPath")), String)
   UploadImage.Text = Getlanguage("upload")
   DeleteImage.Text = Getlanguage("delete")
	Retour.Text = GetLanguage("annuler") 
   
            ImagesFolder = Request.MapPath(ImagesFolder)
   ImagesFolder = Replace(ImagesFolder, "/" , "\")
   ImagesFolder = Replace(ImagesFolder, "\\" , "\")
   
    If Not ImagesFolder.EndsWith("\") Then
          ImagesFolder += "\"
   	End If


   ImageUrl =  "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & CType((Session("FCKeditor:UserFilesPath")), String) 
    If Not ImageUrl.EndsWith("/") Then
          ImageUrl += "/"
   	End If


 Retour.Attributes.Add("Onclick", "OpenFile('');")
 DeleteImage.Attributes.Add("onClick", "JavaScript:return confirm('" & rtesafe(GetLanguage("request_confirm")) & "');")
  CanIUpload.Visible = True
  DisplayImages()
  else
  CanIUpload.Visible = False
  ResultsMessage.Text = "<blockquote>" + GetLanguage("filemanager_security") + GetLanguage("filemanager_security1") + GetLanguage("filemanager_security2") + "</blockquote>"
  ResultsMessage.Text = ProcessLanguage(ResultsMessage.Text, page)
  end if
If ResultsMessage.Text <> "" then
message.visible = true
else
message.visible = False
end if 
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




Public Sub UploadImage_OnClick(ByVal sender As Object, ByVal e As EventArgs) 
        Page.Validate()

        ResultsMessage.Text = ""
' Obtain PortalSettings from Current Context
Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
Dim objAdmin As New AdminDB()
 If Page.IsValid Then
   If CanUpload() Then 
     If Not (ImagesFolder = "") Then 
       If Not (UploadFile.PostedFile.FileName.Trim() = "") Then 
         If IsValidFileType(UploadFile.PostedFile.FileName.Tolower()) Then 
           Try 
             Dim UploadFileName As String = "" 
             UploadFileName = UploadFile.PostedFile.FileName.ToLower() 
             UploadFileName = UploadFileName.Substring(UploadFileName.LastIndexOf("\") + 1)

			 Dim ext As String = LCase(UploadFileName.Substring(UploadFileName.LastIndexOf(".") + 1, UploadFileName.Length - UploadFileName.LastIndexOf(".") - 1)) 		
  					

			If System.IO.File.Exists(ImagesFolder + UploadFileName) then
		    System.IO.File.Delete(ImagesFolder + UploadFileName)
			End if
			
			UploadFile.PostedFile.SaveAs(ImagesFolder + "temp" + Ext)
			ResizeImage(ImagesFolder + "temp" + Ext, ImagesFolder + UploadFileName, TxtSizeL.text, TxtSizeH.text, Ext, TxtQuality.Text)
			System.IO.File.Delete(ImagesFolder + "temp" + Ext)

           Catch ex As Exception
           End Try 
         Else 
           ResultsMessage.Text = InvalidFileTypeMessage 
         End If 
       Else 
         ResultsMessage.Text = NoFileMessage 
       End If 
     Else 
       ResultsMessage.Text = NoFolderSpecifiedMessage 
     End If 
   Else 
     ResultsMessage.Text = NoSpaceLeft 
   End If 
 Else 
   ResultsMessage.Text = InvalidFileTypeMessage 
 End If 
DisplayImages()
If ResultsMessage.Text <> "" then
message.visible = true
else
message.visible = False
end if 
End Sub 


Private Function IsValidFileType(ByVal FileName As String) As Boolean 
Dim ext As String = FileName.Substring(FileName.LastIndexOf(".") + 1, FileName.Length - FileName.LastIndexOf(".") - 1) 
Dim I As integer

i = AcceptedFileTypes.lastIndexOf(ext)
if i > -1 then
    return true 
	else
    return false
	end if 
End Function 

Private Function CanUpload() As Boolean 

			CanUpload = false
			If CType((Session("FCKeditor:UserFilesPath")), String) <> "" then
			Dim SpaceUsed As Double
			Dim StrFolder As String
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

            If Not (Request.Params("hostpage") Is Nothing) Then
				SpaceUsed = 0
            Else
            strFolder = Request.MapPath(_portalSettings.UploadDirectory)
			SpaceUsed = objAdmin.GetDirectorySpaceUsed(strFolder)
			If SpaceUsed = 0 then
			SpaceUsed = objAdmin.GetFolderSizeRecursive(strFolder)
			objAdmin.AddDirectory( strFolder, SpaceUsed )
			End If
			SpaceUsed = SpaceUsed / 1048576
			End If

			If Request.IsAuthenticated and ((SpaceUsed <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Then
			CanIUpload.Visible = True
			Return True 
			else
			CanIUpload.Visible = False
			Return False
			end If 
			else
			CanIUpload.Visible = False
			Return False
			end if       


End Function 


Public Sub DisplayImages() 


' AppPath = HttpContext.Current.Request.PhysicalApplicationPath 
        Dim FilesArray() As String = Nothing
 Dim NumOfImag As Integer = 0

 If Not (ImagesFolder = "") Then 
   Try 
   FilesArray  = System.IO.Directory.GetFiles(ImagesFolder, "*") 
   Catch e As Exception
   ResultsMessage.Text = NoImagesMessage 
   End Try 
 End If 

  GalleryPlaceHolder.Controls.Clear() 
  GalleryPlaceHolder.Controls.Add(New LiteralControl("<table style=""font-size: 8pt"" border=""0"" align=""center"" cellpadding=""1"" cellspacing=""2""><tr>"))
 
 If (FilesArray.Length = 0 ) Then 
  GalleryPlaceHolder.Controls.Add(New LiteralControl("<td>"))
  GalleryPlaceHolder.Controls.Add(New LiteralControl("</td>"))
  ResultsMessage.Text = NoImagesMessage 
 Else 
   Dim ImageFileName As String = "" 
   Dim ImageFileLocation As String = "" 
   Dim thumbWidth As Integer = 94 
   Dim thumbHeight As Integer = 94 

   For Each ImageFile As String In FilesArray 
    
       ImageFileName = ImageFile.ToString() 
       ImageFileName = ImageFileName.Substring(ImageFileName.LastIndexOf("\") + 1) 

	   If IsValidFileType(ImageFileName) Then 
	    Try 
       Dim myHtmlImage As System.Web.UI.HtmlControls.HtmlImage = New System.Web.UI.HtmlControls.HtmlImage () 
       myHtmlImage.Src = ImageUrl + ImageFileName
	   myHtmlImage.Alt = ImageFileName
	   myHtmlImage.Attributes.Add("Title", ImageFileName)
	  	If NumOfImag = 6 then
     	GalleryPlaceHolder.Controls.Add(New LiteralControl("</td></tr><tr>"))
	    NumofImag = 0
	    end if
	  NumofImag += 1
	  GalleryPlaceHolder.Controls.Add(New LiteralControl("<td width=""110px"" align=""center""  style=""height: 110px;background-color:#f1f1e3;"" valign=""middle"" onclick=""divClick(this,'" + ImageFileName + "');"">"))

	   
      Dim myImage As System.Drawing.Image = System.Drawing.Image.FromFile(ImageFile.ToString()) 
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
       myHtmlImage.Attributes("ondblclick") = "OpenFile('" + ImageUrl + ImageFileName + "');" 
       GalleryPlaceHolder.Controls.Add(myHtmlImage)
		GalleryPlaceHolder.Controls.Add(New LiteralControl ("<br>" + ImageFileName + "<br>" + myImage.Width.ToString() + "x" + myImage.Height.ToString() + " (" + (FileLen(ImageFile)/1024).ToString("0.00") + "k)" )) 
	   GalleryPlaceHolder.Controls.Add(New LiteralControl("</td>"))
       myImage.Dispose() 
	  
     Catch e As Exception
     GalleryPlaceHolder.Controls.Add(New LiteralControl("<br>" +  ImageFileName + "</td>"))
      End Try 
	  End If
   Next 
 End If 
  GalleryPlaceHolder.Controls.Add(New LiteralControl("</tr></table>" ))
End Sub

Public Sub DeleteImage_OnClick(ByVal sender As Object, ByVal e As EventArgs)
ResultsMessage.text = "" 
' If Request.IsAuthenticated AndAlso Not (FileToDelete.Value = "") AndAlso Not (FileToDelete.Value = "undefined") Then 
ResultsMessage.Text = ""
If Not (FileToDelete.Value = "") AndAlso Not (FileToDelete.Value = "undefined") Then 
   Try 
     Dim AppPath As String = HttpContext.Current.Request.PhysicalApplicationPath 
     System.IO.File.Delete(ImagesFolder + FileToDelete.Value)
	 ResultsMessage.Text = GetLanguage("ImageDeleted")
	 ResultsMessage.Text = Replace(ResultsMessage.Text, "{imagename}", FileToDelete.Value)
   Catch ex As Exception 
     ResultsMessage.Text = GetLanguage("ImageDeleteError")
   End Try 
 Else 
   ResultsMessage.Text = NoFileToDeleteMessage 
 End If 
 DisplayImages() 
If ResultsMessage.Text <> "" then
message.visible = true
else
message.visible = False
end if 
End Sub 

	  

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
<title><%= DotNetZoom.GetLanguage("Add_Image") %></title>
<asp:literal id="StyleSheet" enableviewstate="false" runat="server" />

<style type="text/css">
.messagetxt {
border-bottom:Gray 1px solid;
border-left:White 1px solid;
border-top:White 1px solid;
border-right:Gray 1px solid;
font-size:12px;
font-weight:400;
text-decoration: none; 
color:#000;
}
.message {
border-bottom:Gray 1px solid;
border-left:White 1px solid;
border-top:White 1px solid;
border-right:Gray 1px solid;
background:silver;
font-size:12px;
font-weight:700;
text-decoration: none; 
color:#000;
}
.scroll {overflow: auto; vertical-align: top; height: 460px;}

</style>

<script language="javascript">
lastDiv = null;
function divClick(theDiv,filename) {
	if (lastDiv) {
		lastDiv.style.backgroundColor = "#f1f1e3";
	}
	lastDiv = theDiv;
	theDiv.style.backgroundColor = "#e3e3c7";
	document.getElementById("FileToDelete").value = filename;
}

function OpenFile( fileUrl )
{
	window.top.close() ;
	window.top.opener.focus() ;
	window.top.opener.SetUrl( fileUrl ) ;
}
function toggleBox(szDivID, iState) // 1 visible, 0 hidden
{
  if(document.layers)
  {document.layers[szDivID].visibility = iState ? "show" : "hide";}
  else if(document.getElementById)
  {var obj = document.getElementById(szDivID);
  obj.style.visibility = iState ? "visible" : "hidden";}
  else if(document.all)
  {document.all[szDivID].style.visibility = iState ? "visible" : "hidden";}
}
</script>	

</head>
<body>
<form id="Form1" method="post" runat="server">
<div id="message" style="position:absolute;top:25px;left:25px;background:#e3e3c7" runat="server">
<table width="400" class="message">
<tr>
<td align="center">
<%= DotNetZoom.GetLanguage("error_panel_title")%>
</td>
<td align="right">
<a title="<%= DotNetZoom.GetLanguage("admin_menu_hide")%>" href="javascript:toggleBox('message',0)" >
<img height="14" width="14" border="0" src="<%=dotnetzoom.glbPath %>images/1x1.gif" Alt="X" title="<%= DotNetZoom.GetLanguage("admin_menu_hide")%>" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -333px;"></a>
</td>
</tr>
</table>
<table width="400" cellpadding="5" class="messagetxt">
<tr>
<td align="left">
<asp:literal id="ResultsMessage" runat="server" />
</td>
</tr>
</table>
</div>
<div id="ScrollingPanel" align="center" class="scroll">
<asp:PlaceHolder id="GalleryPlaceHolder" runat="server"></asp:PlaceHolder>
</div>


<asp:PlaceHolder id="CanIUpload" visible="False" runat="server">
<div align="center">
<hr>
<span style="font-weight: bold; font-size: 8.5pt; color: black; text-decoration: none;""><%= DotNetZoom.GetLanguage("LabelUploadDeleteCancel1")%></span>
<hr>
<table style="font-weight: bold; font-size: 8.5pt; color: black; text-decoration: none;">
	<tr align="left">
		<td Align=Left valign=top><input id="UploadFile" type="file" name="UploadFile" size="55" runat="server" /></td>
		<td Align=Left valign=top><asp:button id="UploadImage" runat="server" onclick="UploadImage_OnClick" /></td>
		<td Align=Left valign=top><asp:button id="DeleteImage" runat="server" onclick="DeleteImage_OnClick" /></td>
		<td Align=Left valign=top><asp:button id="Retour" runat="server" /></td>
	</tr>
	<tr align="left"><td bgcolor="#d7d79f" colspan="4">
<%= DotNetZoom.GetLanguage("LabelQuaImage")%>
<asp:textbox id="txtquality" runat="server" width="20" MaxLength="2"></asp:textbox>%
	<asp:RangeValidator id="Range1"
	       Display = "Dynamic"
           ControlToValidate="txtquality"
           MinimumValue="20"
           MaximumValue="80"
           Type="Integer"
           EnableClientScript="True"
           runat="server"/>&nbsp;&nbsp;&nbsp;
<%= DotNetZoom.GetLanguage("LabelMaxHeightImage")%>   
<asp:textbox id="txtSizeH" runat="server" width="30" MaxLength="3"></asp:textbox>px
	<asp:RangeValidator id="Range2"
	       Display = "Dynamic"
           ControlToValidate="txtSizeH"
           MinimumValue="10"
           MaximumValue="800"
           Type="Integer"
           EnableClientScript="True"
           runat="server"/>&nbsp;&nbsp;&nbsp;
<%= DotNetZoom.GetLanguage("LabelMaxWImage")%>
<asp:textbox id="txtSizeL" runat="server" width="30" MaxLength="3"></asp:textbox>px
	<asp:RangeValidator id="Range3"
	       Display = "Dynamic"
           ControlToValidate="txtSizeL"
           MinimumValue="10"
           MaximumValue="800"
           Type="Integer"
           EnableClientScript="True"
           runat="server"/>
	</td></tr>
</table>
</div>
</asp:PlaceHolder>
<input type="hidden" id="FileToDelete" Value="" runat="server" />
</form>
</body>
</html>