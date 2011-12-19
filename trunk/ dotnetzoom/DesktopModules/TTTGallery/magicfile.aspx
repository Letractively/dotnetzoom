<%@ Page Language="VB" Debug="false" Inherits="DotNetZoom.BasePage" %>

<%@ Import Namespace="DotNetZoom" %>
<%@ Import Namespace="DotNetZoom.PortalModuleControl" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="System.Drawing.Drawing2D" %>
<%@ Import Namespace="System.Drawing" %>
<script runat="server">

    

    Private NoFileMessage As String = GetLanguage("NoFileMessage")
    Private UploadSuccessMessage As String = GetLanguage("UploadSuccessMessage")
    Private NoImagesMessage As String = GetLanguage("NoImagesMessage")
    Private NoFolderSpecifiedMessage As String = GetLanguage("NoFolderSpecifiedMessage")
    Private NoSpaceLeft As String = GetLanguage("NoSpaceLeft")
    Private NoFileToDeleteMessage As String = GetLanguage("NoFileToDeleteMessage")
    Private InvalidFileTypeMessage As String = GetLanguage("InvalidFileTypeMessage2")
    Private AcceptedFileTypes As String = "jpg gif"
    Private Filename As String = ""
    Private CurrentImagesFolder As String = ""
    Private Zrequest As GalleryRequest
    Private config As GalleryConfig
    Private ModuleID As Integer

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Request.QueryString("image") Is Nothing And Not Session("IconeFilePath") Is Nothing Then
            Dim sImagePath As String = Server.MapPath(Session("IconeFilePath"))
            Dim oImage As System.Drawing.Image
            oImage = System.Drawing.Image.FromFile(sImagePath)
            Response.ContentType = "image/" & Request.QueryString("image")
            oImage.Save(Response.OutputStream, ImageFormat.Jpeg)
            oImage.Dispose()
            Response.End()
        Else
        
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"
            Retour.Text = GetLanguage("return")
            UploadImage.Text = GetLanguage("upload")
            Choixbutton.ToolTip = GetLanguage("select_this_image")
            Choixbutton1.ToolTip = Choixbutton.ToolTip
            Choixbutton2.ToolTip = Choixbutton.ToolTip
            Choixbutton3.ToolTip = Choixbutton.ToolTip
            Choixbutton4.ToolTip = Choixbutton.ToolTip
            Choixbutton5.ToolTip = Choixbutton.ToolTip
            Choixbutton6.ToolTip = Choixbutton.ToolTip
            Choixbutton7.ToolTip = Choixbutton.ToolTip
            Choixbutton.ImageUrl = DotNetZoom.ForumConfig.DefaultImageFolder() & "folder.gif"
            Choixbutton1.ImageUrl = DotNetZoom.ForumConfig.DefaultImageFolder() & "folder1.gif"
            Choixbutton2.ImageUrl = DotNetZoom.ForumConfig.DefaultImageFolder() & "folder2.gif"
            Choixbutton3.ImageUrl = DotNetZoom.ForumConfig.DefaultImageFolder() & "folder.jpg"
            Choixbutton4.ImageUrl = DotNetZoom.ForumConfig.DefaultImageFolder() & "folder1.jpg"
            Choixbutton5.ImageUrl = DotNetZoom.ForumConfig.DefaultImageFolder() & "folder2.jpg"
            Choixbutton6.ImageUrl = DotNetZoom.ForumConfig.DefaultImageFolder() & "folder3.jpg"
            Choixbutton7.ImageUrl = DotNetZoom.ForumConfig.DefaultImageFolder() & "folder4.jpg"
            Range1.Text = GetLanguage("ImageQualityRange")
            Range2.Text = GetLanguage("ImageRangeToSave")
            Range3.Text = GetLanguage("ImageRangeToSave")

            ResultsMessage.Text = ""
            If (Request.IsAuthenticated = False) Or (Session("UploadPath") Is Nothing) Or (Session("UploadPath") = "nil") Then
                Session("UploadPath") = "nil"
                EditDenied()
            End If

            Filename = Request("name")
            CurrentImagesFolder = "/" & CType((Session("UploadPath")), String)
   
            If Not Page.IsPostBack Then
                txtquality.Text = "80"
                txtSizeH.Text = "200"
                txtSizeL.Text = "200"
                DisplayImages()
            End If
            If Not Request.Params("mid") Is Nothing Then
                ' Determine ModuleId 
                If IsNumeric(Request.Params("mid")) Then
                    ModuleID = Request.Params("mid")
                    Zrequest = New GalleryRequest(ModuleID)
                    config = GalleryConfig.GetGalleryConfig(ModuleID)
                End If
            Else
                EditDenied()
            End If
        End If
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
        Dim z As Integer = -1

        If Quality < 20 Then
            Quality = 20
        Else
            If Quality > 80 Then
                Quality = 80
            End If
        End If
			
        Select Case LCase(Replace(Extension, ".", ""))
            Case "bmp"
                iFormat = ImageFormat.Bmp
            Case "jpg"
                iFormat = ImageFormat.Jpeg
                If numEncoders > 0 Then
                    For i = 0 To numEncoders - 1
                        If myEncoders(i).MimeType = "image/jpeg" Then
                            z = i
                        End If
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
                Dim newImage As New System.Drawing.Bitmap(newWidth, newHeight)
                Dim g As Graphics = Graphics.FromImage(newImage)
                g.InterpolationMode = InterpolationMode.HighQualityBicubic
                g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                Dim rect As New Rectangle(0, 0, newWidth, newHeight)
                g.DrawImage(mImage, rect, 0, 0, mImage.Width, mImage.Height, GraphicsUnit.Pixel)

					
                Dim PropItm As PropertyItem
                For Each PropItm In mImage.PropertyItems
                    newImage.SetPropertyItem(PropItm)
                Next
                If z <> -1 Then
                    Dim encoderInstance As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
                    Dim encoderParametersInstance As EncoderParameters = New EncoderParameters(2)
                    Dim encoderParameterInstance As EncoderParameter = New EncoderParameter(encoderInstance, Quality)
                    encoderParametersInstance.Param(0) = encoderParameterInstance
                    encoderInstance = System.Drawing.Imaging.Encoder.ColorDepth
                    encoderParameterInstance = New EncoderParameter(encoderInstance, 24)
                    encoderParametersInstance.Param(1) = encoderParameterInstance
                    newImage.Save(Destination, myEncoders(z), encoderParametersInstance)
                Else
                    newImage.Save(Destination, iFormat)
                End If
                newImage.Dispose()
            Else
                If z <> -1 Then
                    Dim encoderInstance As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
                    Dim encoderParametersInstance As EncoderParameters = New EncoderParameters(2)
                    Dim encoderParameterInstance As EncoderParameter = New EncoderParameter(encoderInstance, 50)
                    encoderParametersInstance.Param(0) = encoderParameterInstance
                    encoderInstance = System.Drawing.Imaging.Encoder.ColorDepth
                    encoderParameterInstance = New EncoderParameter(encoderInstance, 24)
                    encoderParametersInstance.Param(1) = encoderParameterInstance
                    mImage.Save(Destination, myEncoders(z), encoderParametersInstance)
                Else
                    mImage.Save(Destination, iFormat)
                End If
            End If
            mImage.Dispose()

        Catch ex As Exception
            ResultsMessage.Text = GetLanguage("ErrorGDI")
            Throw ex
        End Try

    End Sub


    Public Sub UploadImage_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Page.Validate()

        Dim StrFolder As String
        ResultsMessage.Text = ""
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
                                UploadFileDestination = Replace(UploadFileDestination, "/", "\")
   				    		

                                If System.IO.File.Exists(UploadFileDestination + Filename + ".jpg") Then
                                    System.IO.File.Delete(UploadFileDestination + Filename + ".jpg")
                                End If
			
			
                                If System.IO.File.Exists(UploadFileDestination + Filename + ".gif") Then
                                    System.IO.File.Delete(UploadFileDestination + Filename + ".gif")
                                End If
			
                                ResetGal()
			
                                If ext = "jpg" Then
                                    UploadFile.PostedFile.SaveAs(UploadFileDestination + "temp" + ".jpg")
                                    ResizeImage(UploadFileDestination + "temp" + ".jpg", UploadFileDestination + Filename + ".jpg", txtSizeL.Text, txtSizeH.Text, ext, txtquality.Text)
                                    System.IO.File.Delete(UploadFileDestination + "temp" + ".jpg")
                                End If

                                If ext = "gif" Then
                                    UploadFile.PostedFile.SaveAs(UploadFileDestination + "temp" + ".gif")
                                    ResizeImage(UploadFileDestination + "temp" + ".gif", UploadFileDestination + Filename + ".gif", txtSizeL.Text, txtSizeH.Text, ext, txtquality.Text)
                                    System.IO.File.Delete(UploadFileDestination + "temp" + ".gif")
			
                                End If
			
                                ResultsMessage.Text = GetLanguage("MagicFileMessage1") & "<br>" & GetLanguage("MagicFileMessage2")


                                StrFolder = Request.MapPath(_portalSettings.UploadDirectory)
                                objAdmin.AddDirectory(StrFolder, objAdmin.GetFolderSizeRecursive(StrFolder))
           
                            Catch ex As Exception
                            End Try
                            
                            ' Return to album
                            Session("IconeFilePath") = Nothing
                            Session("UploadPath") = Nothing
                            If (Session("ReturnPath") Is Nothing) Then
                                If Request.Params("tabid") Is Nothing Then
                                    Response.Redirect(GetFullDocument() & "?reset=oui", True)
                                Else
                                    Response.Redirect(GetFullDocument() & "?tabid=" & Request.Params("tabid") & "&reset=oui", True)
                                End If
                            Else
                                If InStr(1, LCase(Session("ReturnPath")), "reset") <> 0 Then
                                    Response.Redirect(Session("ReturnPath"))
                                Else
                                    If InStr(1, LCase(Session("ReturnPath")), "?") <> 0 Then
                                        Response.Redirect(Session("ReturnPath") & "&reset=oui")
                                    Else
                                        Response.Redirect(Session("ReturnPath") & "?reset=oui")
                                    End If
                                End If


                            End If
                            
                            
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
        Session("IconeFilePath") = Nothing
        Session("UploadPath") = Nothing
        If (Session("ReturnPath") Is Nothing) Then
            If Request.Params("tabid") Is Nothing Then
                Response.Redirect(GetFullDocument() & "?reset=oui", True)
            Else
                Response.Redirect(GetFullDocument() & "?tabid=" & Request.Params("tabid") & "&reset=oui", True)
            End If
        Else
            If InStr(1, LCase(Session("ReturnPath")), "reset") <> 0 Then
                Response.Redirect(Session("ReturnPath"))
            Else
                If InStr(1, LCase(Session("ReturnPath")), "?") <> 0 Then
                    Response.Redirect(Session("ReturnPath") & "&reset=oui")
                Else
                    Response.Redirect(Session("ReturnPath") & "?reset=oui")
                End If
            End If


        End If
    End Sub




    Private Function IsValidFileType(ByVal FileName As String) As Boolean
        Dim ext As String = FileName.Substring(FileName.LastIndexOf(".") + 1, FileName.Length - FileName.LastIndexOf(".") - 1)
        Dim I As Integer

        I = AcceptedFileTypes.LastIndexOf(ext)
        If I > -1 Then
            Return True
        Else
            Return False
        End If
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
            StrFolder = Request.MapPath(_portalSettings.UploadDirectory)
            SpaceUsed = objAdmin.GetdirectorySpaceUsed(StrFolder)
            SpaceUsed = SpaceUsed / 1048576
        End If

        If Request.IsAuthenticated And ((SpaceUsed <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Then
            Return True
        Else
            Return False
        End If


    End Function





    Public Sub DisplayImages()
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

        Dim UploadFileName As String = ""
        Dim UploadFileDestination As String = ""
        
        UploadFileDestination = Request.MapPath(CurrentImagesFolder)
        UploadFileDestination += "\"
        UploadFileDestination = Replace(UploadFileDestination, "/", "\")
        Response.Expires = -1
        Iconebutton.ToolTip = GetLanguage("click_to_erase")
        Iconebutton.Visible = "false"
        IconeMessage.Visible = "true"
        IconeMessage.Text = GetLanguage("MagicFileMessage")
        Iconebutton.Attributes("onclick") = "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm_delete_icone")) & "');"
        If System.IO.File.Exists(UploadFileDestination + Filename + ".jpg") Then
            'Display the image
            Iconebutton.Visible = "true"
            IconeMessage.Visible = "false"
            Iconebutton.ImageUrl = CurrentImagesFolder & "/" & Filename & ".jpg?" + DateTime.Now.ToString("yyyyMMddHHmmss")
            
            'Iconebutton.ImageUrl = glbPath() & "DesktopModules/TTTGallery/magicfile.aspx?image=jpeg" & "&tabid=" & _portalSettings.ActiveTab.TabId.ToString()
            Session("IconeFilePath") = CurrentImagesFolder & "/" & Filename & ".jpg"
        ElseIf System.IO.File.Exists(UploadFileDestination + Filename + ".gif") Then
            Iconebutton.Visible = "true"
            IconeMessage.Visible = "false"
            Iconebutton.ImageUrl = CurrentImagesFolder & "/" & Filename & ".gif?" + DateTime.Now.ToString("yyyyMMddHHmmss")
            'Iconebutton.ImageUrl = glbPath() & "DesktopModules/TTTGallery/magicfile.aspx?image=gif" & "&tabid=" & _portalSettings.ActiveTab.TabId.ToString()
            Session("IconeFilePath") = CurrentImagesFolder & "/" & Filename & ".gif"
        End If
        If ResultsMessage.Text = "" Then
            ResultsMessage.Text = "URL: " & Iconebutton.ImageUrl
        End If
  
    End Sub

    Sub ImageButton_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim UploadFileDestination As String = Request.MapPath(CurrentImagesFolder)
        UploadFileDestination += "\"
        UploadFileDestination = Replace(UploadFileDestination, "/", "\")
        ResetGal()

        If System.IO.File.Exists(UploadFileDestination + Filename + ".jpg") Then
            System.IO.File.Delete(UploadFileDestination + Filename + ".jpg")
        End If
			
			
        If System.IO.File.Exists(UploadFileDestination + Filename + ".gif") Then
            System.IO.File.Delete(UploadFileDestination + Filename + ".gif")
        End If

        DisplayImages()
	   
    End Sub

    Sub ResetGal()
        
        Session("reset") = Zrequest.Path
        GalleryConfig.ResetGalleryConfig(ModuleID)
        Zrequest.Folder.Reset()
        ' so remove the from cache
        ClearModuleCache(ModuleID)
        
    End Sub
    
    
    Sub ChoixButton_Click(sender As Object, e As ImageClickEventArgs)
        Dim TempFileName As String = ""
        Dim ext As String = ""
        Dim UploadFileDestination As String = ""
        ResetGal()
        UploadFileDestination = Request.MapPath(CurrentImagesFolder)
        UploadFileDestination += "\"
        UploadFileDestination = Replace(UploadFileDestination, "/", "\")
   					
   				    		

        If System.IO.File.Exists(UploadFileDestination + Filename + ".jpg") Then
            System.IO.File.Delete(UploadFileDestination + Filename + ".jpg")
        End If
			
			
        If System.IO.File.Exists(UploadFileDestination + Filename + ".gif") Then
            System.IO.File.Delete(UploadFileDestination + Filename + ".gif")
        End If

 	  
        TempFileName = Server.MapPath(sender.ImageUrl)
        ext = TempFileName.Substring(TempFileName.LastIndexOf("."), TempFileName.Length - TempFileName.LastIndexOf("."))

        System.IO.File.Copy(TempFileName, UploadFileDestination & Filename & ext)

        DisplayImages()
        ResultsMessage.Text = GetLanguage("MagicFileMessage1") & "<br>" & GetLanguage("MagicFileMessage2")
	   
    End Sub

	  
	  

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="Content-Script-Type" content="text/javascript">
    <title>
        <%= DotNetZoom.GetLanguage("Add_Image") %></title>
    <asp:Literal ID="StyleSheet" runat="server"></asp:Literal>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="center">
                <table cellpadding="10" border="5">
                    <tr>
                        <td width="100%" align="center">
                            <asp:Literal Visible="false" ID="IconeMessage" runat="server" />
                            <asp:ImageButton ID="Iconebutton" runat="server" BorderWidth="0" BorderStyle="none"
                                AlternateText="icone" ImageAlign="left" OnClick="ImageButton_Click" />
                        </td>
                    </tr>
                </table>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Button ID="Retour" runat="server" OnClick="Return_OnClick" /></p>
                <p>
                    &nbsp;</p>
                <table cellpadding="10" border="5">
                    <tr>
                        <td>
                            <%= DotNetZoom.GetLanguage("MagicFileInfo") & DotNetZoom.GetLanguage("MagicFileInfo1") %>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="10" border="5">
                    <tr>
                        <td>
                            <asp:ImageButton ID="Choixbutton" runat="server" AlternateText="icone" Width="95"
                                Height="95" BorderWidth="0" BorderStyle="none" ImageAlign="left" OnClick="ChoixButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="Choixbutton1" runat="server" AlternateText="icone" Width="95"
                                Height="95" BorderWidth="0" BorderStyle="none" ImageAlign="left" OnClick="ChoixButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="Choixbutton2" runat="server" AlternateText="icone" Width="95"
                                Height="40" BorderWidth="0" BorderStyle="none" ImageAlign="left" OnClick="ChoixButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="Choixbutton3" runat="server" AlternateText="icone" Width="95"
                                Height="95" BorderWidth="0" BorderStyle="none" ImageAlign="left" OnClick="ChoixButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="Choixbutton4" runat="server" AlternateText="icone" Width="94"
                                Height="94" BorderWidth="0" BorderStyle="none" ImageAlign="left" OnClick="ChoixButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="Choixbutton5" runat="server" AlternateText="icone" Width="94"
                                Height="94" BorderWidth="0" BorderStyle="none" ImageAlign="left" OnClick="ChoixButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="Choixbutton6" runat="server" AlternateText="icone" Width="94"
                                Height="40" BorderWidth="0" BorderStyle="none" ImageAlign="left" OnClick="ChoixButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="Choixbutton7" runat="server" AlternateText="icone" Width="94"
                                Height="40" BorderWidth="0" BorderStyle="none" ImageAlign="left" OnClick="ChoixButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="16" style="padding-left: 10px; border-top: 1 solid #999999; background-color: #99ccff;">
                <div align="center">
                    <table cellpadding="10" border="0">
                        <tr>
                            <td width="100%" align="left">
                                <asp:Literal ID="ResultsMessage" Visible="true" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <hr>
                    <table style="font-weight: bold; font-size: 8.5pt; color: black; text-decoration: none;">
                        <tr align="left">
                            <td align="Left" valign="top">
                                <input id="UploadFile" type="file" name="UploadFile" size="65" runat="server" />
                            </td>
                            <td align="Left" valign="top">
                                <asp:Button ID="UploadImage" runat="server" OnClick="UploadImage_OnClick" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#d7d79f" colspan="3">
                                <%= DotNetZoom.GetLanguage("LabelQuaImage")%>
                                <asp:TextBox ID="txtquality" runat="server" Width="20" MaxLength="2"></asp:TextBox>%
                                <asp:RangeValidator ID="Range1" Display="Dynamic" ControlToValidate="txtquality"
                                    MinimumValue="20" MaximumValue="80" Type="Integer" EnableClientScript="True"
                                    runat="server" />&nbsp;&nbsp;&nbsp;
                                <%= DotNetZoom.GetLanguage("LabelMaxHeightImage")%>
                                <asp:TextBox ID="txtSizeH" runat="server" Width="30" MaxLength="3"></asp:TextBox>px
                                <asp:RangeValidator ID="Range2" Display="Dynamic" ControlToValidate="txtSizeH" MinimumValue="10"
                                    MaximumValue="200" Type="Integer" EnableClientScript="True" runat="server" />&nbsp;&nbsp;&nbsp;
                                <%= DotNetZoom.GetLanguage("LabelMaxWImage")%>
                                <asp:TextBox ID="txtSizeL" runat="server" Width="30" MaxLength="3"></asp:TextBox>px
                                <asp:RangeValidator ID="Range3" Display="Dynamic" ControlToValidate="txtSizeL" MinimumValue="10"
                                    MaximumValue="200" Type="Integer" EnableClientScript="True" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
