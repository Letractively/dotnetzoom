<%@ Page Language="VB" Debug="false" Inherits="DotNetZoom.BasePage"%>
<%@ Import Namespace="DotNetZoom" %>
<%@ Import Namespace="DotNetZoom.PortalModuleControl" %>
<%@ Register TagPrefix="Portal" TagName="TagFileManager" Src="~/Admin/AdvFileManager/TAGAdvFileManager.ascx" %>
<script runat="server">
Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)


Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"

Dim objAdmin As New AdminDB()

Dim tmpUploadRoles As String = ""
If Not CType( portalSettings.GetSiteSettings(_portalSettings.PortalID)("uploadroles"), String) Is Nothing Then
   tmpUploadRoles = CType( portalSettings.GetSiteSettings(_portalSettings.PortalID)("uploadroles"), String)
End If

 If (Request.IsAuthenticated = false) or (PortalSecurity.IsInRoles(tmpUploadRoles) = False) or Session("FCKeditor:UserFilesPath") = "" Then
 	Session("FCKeditor:UserFilesPath") = ""
	Files.visible = False
	ResultsMessage.Text = "<blockquote>" + GetLanguage("filemanager_security") + GetLanguage("filemanager_security1") + GetLanguage("filemanager_security2") + "</blockquote>"
	ResultsMessage.Text = ProcessLanguage(ResultsMessage.Text, page)
else
' Retour.Attributes.Add("Onclick", "OpenFile('')")
End If
 
End Sub 

	  

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
<title><%= DotNetZoom.GetLanguage("File_Exploreur") %></title>
<asp:literal id="StyleSheet" enableviewstate="false" runat="server" />
<style type="text/css">
.scroll {overflow: auto; height: 320px; border: 1px silver solid;}
</style>

</head>
<body>
<form id="Form1" method="post" runat="server">
<portal:TagFileManager id="Files" runat="server"></portal:TagFileManager>
<asp:literal id="ResultsMessage" runat="server" />
</form>
</body>
</html>