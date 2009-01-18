<%@ Page Language="VB" Debug="false" Inherits="DotNetZoom.BasePage"%>
<%@ Import Namespace="DotNetZoom" %>
<%@ Import Namespace="DotNetZoom.PortalModuleControl" %>
<%@Import Namespace="System.Drawing.Imaging" %>
<%@Import NameSpace="System.Drawing.Drawing2D" %>
<%@Import NameSpace="System.Drawing" %>

<script runat="server">


Private NoFileMessage As String = GetLanguage("NoFileMessage") 
Private UploadSuccessMessage As String = GetLanguage("UploadSuccessMessage")
Private NoImagesMessage As String = GetLanguage("NoImagesMessage")
Private NoFolderSpecifiedMessage As String = GetLanguage("NoFolderSpecifiedMessage")
Private NoSpaceLeft As String = GetLanguage("NoSpaceLeft") 
Private NoFileToDeleteMessage As String =  GetLanguage("NoFileToDeleteMessage") 
Private InvalidFileTypeMessage As String = GetLanguage("InvalidFileTypeMessage2") 
Private AcceptedFileTypes As String = "jpg gif"  
Private Filename as string = ""
Private CurrentImagesFolder as string = ""

Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"
Retour.Text = GetLanguage("annuler") 
UploadImage.Text = Getlanguage("upload")
Choixbutton.ToolTip = getlanguage("select_this_image")
Choixbutton1.ToolTip = Choixbutton.ToolTip
Choixbutton2.ToolTip = Choixbutton.ToolTip
Choixbutton3.ToolTip = Choixbutton.ToolTip
Choixbutton4.ToolTip = Choixbutton.ToolTip
Choixbutton5.ToolTip = Choixbutton.ToolTip
Choixbutton6.ToolTip = Choixbutton.ToolTip
        Choixbutton7.ToolTip = Choixbutton.ToolTip
        Choixbutton.ImageURL = DotNetZoom.glbPath & "images/TTT/folder.gif"
        Choixbutton1.ImageUrl = DotNetZoom.glbPath & "images/TTT/folder1.gif"
        Choixbutton2.ImageUrl = DotNetZoom.glbPath & "images/TTT/folder2.gif"
        Choixbutton3.ImageUrl = DotNetZoom.glbPath & "images/TTT/folder.jpg"
        Choixbutton4.ImageUrl = DotNetZoom.glbPath & "images/TTT/folder1.jpg"
        Choixbutton5.ImageUrl = DotNetZoom.glbPath & "images/TTT/folder2.jpg"
        Choixbutton6.ImageUrl = DotNetZoom.glbPath & "images/TTT/folder3.jpg"
        Choixbutton7.ImageUrl = DotNetZoom.glbPath & "images/TTT/folder4.jpg"
Range1.Text = Getlanguage("ImageQualityRange")
Range2.Text = Getlanguage("ImageRangeToSave")
Range3.Text = Getlanguage("ImageRangeToSave")

 ResultsMessage.Text = ""           
 If (Request.IsAuthenticated = false) or (Session("UploadPath") Is Nothing) or (Session("UploadPath") = "nil" )Then
 	Session("UploadPath") = "nil"
            Response.Redirect(glbPath & "default.aspx?tabid=" & _portalSettings.ActiveTab.TabId & "&def=Edit Access Denied", True)
 End If

   Filename = Request("name") 
   CurrentImagesFolder = "/" & CType((Session("UploadPath")), String)
   
if not Page.IsPostBack Then
TxtQuality.Text = "80"
txtSizeH.Text = "200"
txtSizeL.Text = "200"
DisplayImages() 
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
		Dim encoderParameterInstance As EncoderParameter = New EncoderParameter (encoderInstance, 50)
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
Dim StrFolder As String
ResultsMessage.text = ""
' Obtain PortalSettings from Current Context
Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
Dim objAdmin As New AdminDB()
 If Page.IsValid Then
   If CanUpload() Then 
     If Not (CurrentImagesFolder = "") Then 
       If Not (UploadFile.PostedFile.FileName.Trim() = "") Then 
         If IsValidFileType(UploadFile.PostedFile.FileName.ToLower()) Then 
           Try 
             Dim UploadFileName As String = "" 
             Dim UploadFileDestination As String = "" 
             UploadFileName = UploadFile.PostedFile.FileName.ToLower() 
             UploadFileName = UploadFileName.Substring(UploadFileName.LastIndexOf("\") + 1)

			 Dim ext As String = LCase(UploadFileName.Substring(UploadFileName.LastIndexOf(".") + 1, UploadFileName.Length - UploadFileName.LastIndexOf(".") - 1)) 		
 			
                                
             UploadFileDestination = Request.MapPath(CurrentImagesFolder)
                                
   			UploadFileDestination += "\"
   			UpLoadFileDestination = Replace(UpLoadFileDestination, "/" , "\")		
   				    		

			If System.IO.File.Exists(UploadFileDestination + FileName + ".jpg") then
		    System.IO.File.Delete(UploadFileDestination + FileName + ".jpg")
			End if
			
			
			If System.IO.File.Exists(UploadFileDestination + FileName + ".gif") then
	 		System.IO.File.Delete(UploadFileDestination + FileName + ".gif")
			end if
			
			
			
	  		If Ext = "jpg" then
			UploadFile.PostedFile.SaveAs(UploadFileDestination + "temp" + ".jpg")
			ResizeImage(UploadFileDestination + "temp" + ".jpg", UploadFileDestination + FileName + ".jpg", txtSizeL.Text, txtSizeH.Text, Ext, TxtQuality.Text)
			System.IO.File.Delete(UploadFileDestination + "temp" + ".jpg")
			end If

	  		If Ext = "gif" then
			UploadFile.PostedFile.SaveAs(UploadFileDestination + "temp" + ".gif")
			ResizeImage(UploadFileDestination + "temp" + ".gif", UploadFileDestination + FileName + ".gif", txtSizeL.Text, txtSizeH.Text, Ext, TxtQuality.Text)
			System.IO.File.Delete(UploadFileDestination + "temp" + ".gif")
			
			end If
			
		    ResultsMessage.text = GetLanguage("MagicFileMessage1") & "<br>" & GetLanguage("MagicFileMessage2")


			strFolder = Request.MapPath(_portalSettings.UploadDirectory)
			objAdmin.AddDirectory( strFolder, objAdmin.GetFolderSizeRecursive(strFolder))
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
End Sub 



Public Sub Return_OnClick(ByVal sender As Object, ByVal e As EventArgs)

 If (Session("ReturnPath") Is Nothing) Then
    If Request.Params("tabid") Is Nothing Then
                Response.Redirect(GetFullDocument() & "?reset=oui", True)
	Else
                Response.Redirect(GetFullDocument() & "?tabid=" & Request.Params("tabid") & "&reset=oui", True)
	End if
 	else
	If InStr(1, Lcase(Session("ReturnPath")), "reset") <> 0 Then
		Response.Redirect(Session("ReturnPath"))
	else
	If InStr(1, Lcase(Session("ReturnPath")), "?") <> 0 Then
	    Response.Redirect(Session("ReturnPath") & "&reset=oui")
	else
		Response.Redirect(Session("ReturnPath") & "?reset=oui")
	end if
	end if


 End If
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
			Return True 
			else
			Return False
			end If        


End Function 





Public Sub DisplayImages() 

   Dim UploadFileName As String = "" 
   Dim UploadFileDestination As String = "" 
        
        UploadFileDestination = Request.MapPath(CurrentImagesFolder)
   UploadFileDestination += "\"
   UpLoadFileDestination = Replace(UpLoadFileDestination, "/" , "\")
   Response.Expires = -1
   Iconebutton.ToolTip = getlanguage("click_to_erase")
   Iconebutton.visible = "false"
   IconeMessage.visible = "true"
   IconeMessage.Text = GetLanguage("MagicFileMessage")
   Iconebutton.Attributes("onclick") = "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm_delete_icone")) & "');"
   If System.IO.File.Exists(UploadFileDestination + FileName + ".jpg") then
	'Display the image
	Iconebutton.visible = "true"
	IconeMessage.visible = "false"
    Iconebutton.ImageUrl = CurrentImagesFolder & "/" & Filename & ".jpg"
   End if
			
   If System.IO.File.Exists(UploadFileDestination + FileName + ".gif") then
    Iconebutton.visible = "true"
	IconeMessage.visible = "false"
            Iconebutton.ImageUrl = CurrentImagesFolder & "/" & Filename & ".gif"
   end if
	If ResultsMessage.Text = "" then
   	ResultsMessage.Text = "URL: " & IconeButton.ImageUrl
	end if
  
End Sub

      Sub ImageButton_Click(sender As Object, e As ImageClickEventArgs) 
            Dim UploadFileDestination As String = "" 
        
        UploadFileDestination += Request.MapPath(CurrentImagesFolder)
            UploadFileDestination += "\"
			UpLoadFileDestination = Replace(UpLoadFileDestination, "/" , "\")
   					
   				    		

			If System.IO.File.Exists(UploadFileDestination + FileName + ".jpg") then
		    System.IO.File.Delete(UploadFileDestination + FileName + ".jpg")
			End if
			
			
			If System.IO.File.Exists(UploadFileDestination + FileName + ".gif") then
	 		System.IO.File.Delete(UploadFileDestination + FileName + ".gif")
			end if

       DisplayImages()
	   
      End Sub

      Sub ChoixButton_Click(sender As Object, e As ImageClickEventArgs) 
            Dim TempFileName As String = ""
			Dim ext As String = ""
			Dim UploadFileDestination As String = "" 
         
        UploadFileDestination = Request.MapPath(CurrentImagesFolder)
            UploadFileDestination += "\"
			UpLoadFileDestination = Replace(UpLoadFileDestination, "/" , "\")
   					
   				    		

			If System.IO.File.Exists(UploadFileDestination + FileName + ".jpg") then
		    System.IO.File.Delete(UploadFileDestination + FileName + ".jpg")
			End if
			
			
			If System.IO.File.Exists(UploadFileDestination + FileName + ".gif") then
	 		System.IO.File.Delete(UploadFileDestination + FileName + ".gif")
			end if

 	  
    	   TempFileName = SerVer.MapPath(Sender.ImageUrl)
		   ext = TempFileName.Substring(TempFileName.LastIndexOf(".") , TempFileName.Length - TempFileName.LastIndexOf(".") ) 

          System.IO.File.Copy(TempFileName, UpLoadFileDestination & FileName & ext)

       DisplayImages()
	   ResultsMessage.text = GetLanguage("MagicFileMessage1") & "<br>" & GetLanguage("MagicFileMessage2")
	   
      End Sub

	  
	  

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
<title><%= DotNetZoom.GetLanguage("Add_Image") %></title>
<asp:literal id="StyleSheet" runat="server"></asp:literal>
</head>
<body>
<form id="Form1" method="post" runat="server">
<table width=100% cellpadding=0 cellspacing=0 border=0>
<tr><td align="center">

<table cellpadding=10 border=5>
<tr>
<td width=100% align="center">
<asp:literal visible="false" id="IconeMessage" runat="server" />	
<asp:ImageButton id="Iconebutton" runat="server"
           BorderWidth="0" 
		   BorderStyle="none"
           AlternateText="icone"
           ImageAlign="left"
           OnClick="ImageButton_Click"/>
</td></tr></table>
<p>&nbsp;</p>
<table cellpadding=10 border=5>
<tr>
<td>
<%= DotNetZoom.GetLanguage("MagicFileInfo") & DotNetZoom.GetLanguage("MagicFileInfo1") %>
</td>
</tr></table>
<table width=100% cellpadding=10 border=5>
<tr>
<td>
<asp:ImageButton id="Choixbutton" runat="server"
           AlternateText="icone"
		   width="95" 
		   height="95" 
		   BorderWidth="0" 
		   BorderStyle="none"
           ImageAlign="left"
		   OnClick="ChoixButton_Click"/>
</td>
<td>
<asp:ImageButton id="Choixbutton1" runat="server"
           AlternateText="icone"
		   width="95" 
		   height="95" 
		   BorderWidth="0" 
		   BorderStyle="none"
           ImageAlign="left"
		   OnClick="ChoixButton_Click"/>
</td>
<td>
<asp:ImageButton id="Choixbutton2" runat="server"
           AlternateText="icone"
		   width="95" 
		   height="40" 
		   BorderWidth="0" 
		   BorderStyle="none"
		   ImageAlign="left"
		   OnClick="ChoixButton_Click"/>
</td>
<td>
<asp:ImageButton id="Choixbutton3" runat="server"
           AlternateText="icone"
		   width="95" 
		   height="95" 
		   BorderWidth="0" 
		   BorderStyle="none"
		   ImageAlign="left"
		   OnClick="ChoixButton_Click"/>
</td>
<td>
<asp:ImageButton id="Choixbutton4" runat="server"
           AlternateText="icone"
		   width="94" 
		   height="94" 
		   BorderWidth="0" 
		   BorderStyle="none"
           ImageAlign="left"
		   OnClick="ChoixButton_Click"/>
</td>
<td>
<asp:ImageButton id="Choixbutton5" runat="server"
           AlternateText="icone"
		   width="94" 
		   height="94" 
		   BorderWidth="0" 
		   BorderStyle="none"
		   ImageAlign="left"
		   OnClick="ChoixButton_Click"/>
</td>
<td>
<asp:ImageButton id="Choixbutton6" runat="server"
           AlternateText="icone"
		   width="94" 
		   height="40" 
		   BorderWidth="0" 
		   BorderStyle="none"
           ImageAlign="left"
		   OnClick="ChoixButton_Click"/>
</td>
<td>
<asp:ImageButton id="Choixbutton7" runat="server"
           AlternateText="icone"
		   width="94" 
		   height="40" 
		   BorderWidth="0" 
		   BorderStyle="none"
           ImageAlign="left"
		   OnClick="ChoixButton_Click"/>
</td></tr></table>
</td></tr>
<tr><td height=16 style="padding-left:10px;border-top: 1 solid #999999; background-color:#99ccff;">

<div align="center">
<table cellpadding=10 border=0>
<tr>
<td width="100%" align="left">
<asp:literal id="ResultsMessage" visible="true" runat="server" />
</td>
</tr>
</table>
<hr>
<table style="font-weight: bold; font-size: 8.5pt; color: black; text-decoration: none;">
	<tr align="left">
		<td Align=Left valign=top><input id="UploadFile" type="file" name="UploadFile" size="65" runat="server" /></td>
		<td Align=Left valign=top><asp:button id="UploadImage" runat="server" onclick="UploadImage_OnClick" /></td>
		<td Align=Left valign=top><asp:button id="Retour" runat="server" onclick="Return_OnClick" /></td>
	</tr>
<tr align="left"><td bgcolor="#d7d79f" colspan="3">
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
           MaximumValue="200"
           Type="Integer"
           EnableClientScript="True"
           runat="server"/>&nbsp;&nbsp;&nbsp;
<%= DotNetZoom.GetLanguage("LabelMaxWImage")%>
<asp:textbox id="txtSizeL" runat="server" width="30" MaxLength="3"></asp:textbox>px
	<asp:RangeValidator id="Range3"
	       Display = "Dynamic"
           ControlToValidate="txtSizeL"
           MinimumValue="10"
           MaximumValue="200"
           Type="Integer"
           EnableClientScript="True"
           runat="server"/>
	</td></tr>
</table>
</div>


</form>
</body>
</html>