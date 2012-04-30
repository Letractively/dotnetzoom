<%@ Page Language="VB" Debug="false" Inherits="DotNetZoom.BasePage"%>
<%@ Import Namespace="DotNetZoom" %>
<%@ Import Namespace="System.IO" %>
<script runat="server">

Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
Sauvegarder.Text = GetLanguage("enregistrer")
Delete.Text = GetLanguage("delete")
Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"

If not page.IsPostBack then
' first Load
whatfile.text = ""
' Verify that the current user has access to edit this module
If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) then
If Request.Params("file") <> "" then
	Dim StrExtension As String = ""
	
If Request.Params("file") = "portal.css" or Request.Params("file") = "ttt.css" then
whatfile.text = Request.Params("file")
LoadStyleSheet( whatfile.text)
else
If InStr(1, Request.Params("file"), "skin/") = 1 then
    whatfile.text = Request.Params("file")
	If InStr(1, Request.Params("file"), ".") Then
		strExtension = Mid(Request.Params("file"), InStrRev(Request.Params("file"), ".") + 1)
	End If

    Select Case strExtension.ToLower()
           Case "skin" , "css" : LoadStyleSheet( whatfile.text)
           Case Else : whatfile.text = ""
    End Select
end if
end if
end if
end if
If whatFile.Text = "" then
Sauvegarder.Visible = False
Delete.Visible = False
end if
else
'postBack
end if
End Sub 

Public Sub Delete_OnClick(ByVal sender As Object, ByVal e As EventArgs) 
Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
' delete
if File.Exists(Server.MapPath(_PortalSettings.UploadDirectory & whatfile.text)) then
File.Delete(Server.MapPath(_PortalSettings.UploadDirectory & whatfile.text))
txtStyleSheet.Text = ""
Message.text = GetLanguage("Skin_FileErased") & " : "
else
Message.text = GetLanguage("SkinFileNoExist") & " : "
end if
End Sub 
 

Public Sub Sauvegarder_OnClick(ByVal sender As Object, ByVal e As EventArgs) 
Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
' Sauvegarder
If not System.IO.Directory.Exists(Server.MapPath(_PortalSettings.UploadDirectory  & "skin/")) Then
       System.IO.Directory.CreateDirectory(Server.MapPath(_PortalSettings.UploadDirectory  & "skin/"))
End If
Dim strExtension as String = ""
strExtension = Mid(whatfile.text, InStrRev(whatfile.text, ".") + 1)
Message.text = ""
If StrExtension = "skin" then
			' check skin file
			Dim strtxtpage as string = txtStyleSheet.Text
            Dim options As RegexOptions = RegexOptions.IgnoreCase
            strtxtpage = Regex.Replace(strtxtpage, "<form[^>]*>", "<form>", options)
            strtxtpage = Regex.Replace(strtxtpage, "</form[^><]*>", "</form>", options)
	      	Dim myValues As [String]() =  { "<head>", "</head>", "<body", "</title>", "{description}", "{keyword}", "{copyright}", "{generator}", "{hypCopyright}", "{banner}", "{leftline}", "{leftspacer}" , "{leftpane}", "{contentpane}", "{rightspacer}", "{rightline}", "{rightpane}", "{footer}", "{tigramenu}" , "{solpartmenu}", "{tabmenu}"}
      		Dim i As Integer
            For i = myValues.GetLowerBound(0) To myValues.GetUpperBound(0)
			strtxtpage = Regex.Replace(strtxtpage, myValues(i), myValues(i) , options)
 			next i
  			txtStyleSheet.Text = strtxtpage
			
			If Regex.IsMatch(strtxtpage, "<form>") and Regex.IsMatch(strtxtpage, "</form>") Then
			If strtxtpage.IndexOf("<form>") > strtxtpage.IndexOf("</form>") then
			' Form it out of sequence
			Message.text = GetLanguage("Skin_NeedForm") & "<br>"

			end if
			Else
			' il manque <form> ou </form>
			Message.text = Message.text & GetLanguage("Skin_NeedForm2") & "<br>" 
			End If

			If not Regex.IsMatch(strtxtpage, "{contentpane}") Then
			' il manque "{contentpane}"
			Message.text = Message.text & GetLanguage("Skin_NeedContentpane") & "<br>"
			End If
end if

if Message.text = "" then
Dim objStream As StreamWriter
objStream = File.CreateText(Server.MapPath(_PortalSettings.UploadDirectory & whatfile.text))
txtStyleSheet.Text = txtStyleSheet.Text.trim()
objStream.WriteLine(txtStyleSheet.Text)
objStream.Close()
Message.text = GetLanguage("Skin_FileSaved") & " : "
ClearPortalCache(_PortalSettings.PortalId)
end if
End Sub 

Private Sub LoadStyleSheet(ByVal FileToRead As String)

Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
' read file file
Message.text = ""
Dim objStreamReader As StreamReader
if File.Exists(Server.MapPath(_PortalSettings.UploadDirectory & FileToRead)) then

objStreamReader = File.OpenText(Server.MapPath(_PortalSettings.UploadDirectory & FileToRead))
txtStyleSheet.Text = objStreamReader.ReadToEnd
objStreamReader.Close()
else
if fileToRead = "skin/portal.skin" then
                If File.Exists(Server.MapPath(glbPath + "host.skin")) Then
                    objStreamReader = File.OpenText(Server.MapPath(glbPath + "host.skin"))
                    txtStyleSheet.Text = objStreamReader.ReadToEnd
                    objStreamReader.Close()
                End If
end if
if fileToRead = "skin/portaledit.skin" then
                If File.Exists(Server.MapPath(glbPath + "hostedit.skin")) Then
                    objStreamReader = File.OpenText(Server.MapPath(glbPath + "hostedit.skin"))
                    txtStyleSheet.Text = objStreamReader.ReadToEnd
                    objStreamReader.Close()
                End If
end if
end if
End Sub
 

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title><%= DotNetZoom.GetLanguage("modifier") %></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
<asp:literal id="StyleSheet" enableviewstate="false" runat="server" />
</head>
<body>
<form id="Form1" method="post" runat="server">
<div align="center" style="width: 100%">
<table style="width: 800px">
<tr>
<td align="left">
<asp:literal id="Message" runat="server" EnableViewState="false"></asp:literal>
<asp:label id="WhatFile" runat="server" EnableViewState="true"></asp:label>
</td>
</tr>
<tr>
<td align="left">
<asp:textbox id="txtStyleSheet" visible="true" CssClass="NormalTextBox" TextMode="MultiLine" Rows="30" Runat="server" Columns="105"></asp:textbox>
</td>
</tr>
<tr>
<td align="left">
&nbsp;&nbsp; 
<asp:button id="Sauvegarder" runat="server" onclick="Sauvegarder_OnClick" />
&nbsp;&nbsp; 
<input type=button value="<%= GetLanguage("return")%>" onClick="javascript:self.close();">
&nbsp;&nbsp; 
<asp:button id="Delete" runat="server" onclick="Delete_OnClick" />
</td>
</tr>
</table>
</div>
</form>
</body>
</html>