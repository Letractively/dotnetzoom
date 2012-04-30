<%@ Page Language="VB" Debug="false" Inherits="DotNetZoom.BasePage"%>
<%@ Import Namespace="DotNetZoom" %>
<%@ Import Namespace="DotNetZoom.PortalModuleControl" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import NameSpace="System.Drawing.Drawing2D" %>
<%@ Import NameSpace="System.Drawing" %>

<script runat="server">
 
 
    Private NoImagesMessage As String = GetLanguage("NoImagesMessage")
    Private ImageUrl As String = glbPath & "images/containers/"
Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"

Annuler.Attributes.Add("Onclick", "JavaScript:window.top.close();window.top.opener.focus();")
Annuler.Text = GetLanguage("return")
        If Request.IsAuthenticated = False Then
            
            EditDenied()
        End If
        DisplayImages()
End Sub 



Public Sub DisplayImages() 


' AppPath = HttpContext.Current.Request.PhysicalApplicationPath 
        Dim FilesArray() As String = Nothing
 Dim NumOfImag As Integer = 1

        Dim strFolder As String = Request.MapPath(glbPath + "images/containers/")
				
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