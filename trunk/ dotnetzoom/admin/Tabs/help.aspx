<%@ Page Language="VB" Debug="false"  Inherits="DotNetZoom.BasePage"%>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<%@ Import Namespace="DotNetZoom" %>
<script runat="server">
Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"

if not Page.IsPostBack then	
Dim TobjAdmin As New AdminDB()
if not Request.Params("help") Is Nothing then
Help.Text = ProcessLanguage(TobjAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), Request.Params("help")), page)
end if
	 If Request.IsAuthenticated then
                If Context.User.Identity.Name = _portalSettings.SuperUserId Then
                    cmdEdit.Visible = True
                    cmdUpdate.Text = GetLanguage("enregistrer")
                    cmdCancel.Text = GetLanguage("return")
                    cmdEdit.Text = GetLanguage("modifier")
                End If
	end if
end if
end sub
		Private Sub SetFckEditor()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        ' FCKeditor1.LinkBrowser = true
			FCKeditor1.width = unit.pixel(650)
			FCKeditor1.Height = unit.pixel(350)
			if GetLanguage("fckeditor_language") <> "auto"
			FCKeditor1.DefaultLanguage = GetLanguage("fckeditor_language")
			FCKeditor1.AutoDetectLanguage = False
			end if
        FCKeditor1.LinkBrowserURL = glbPath & "admin/AdvFileManager/filemanager.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
        FCKeditor1.ImageBrowserURL = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
			Session("FCKeditor:UserFilesPath") = _portalSettings.UploadDirectory
			' set the css for the editor if it exist
			Dim CSSFileName As String = Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/fck_editorarea.css")
			If System.IO.File.Exists(CSSFileName) then
            FCKeditor1.EditorAreaCSS= _portalSettings.UploadDirectory & "skin/fckeditor/fck_editorarea.css"
			end if
			CSSFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/htmlstyles.xml")
			If System.IO.File.Exists(CSSFileName) then
            FCKeditor1.StylesXmlPath = _portalSettings.UploadDirectory & "skin/fckeditor/htmlstyles.xml"
			else
			CSSFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/fckstyles.xml")
			If System.IO.File.Exists(CSSFileName) then
			FCKeditor1.StylesXmlPath =  _portalSettings.UploadDirectory & "skin/fckeditor/fckstyles.xml"
    		End If
			End If
			CSSFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditorhtml/")
			If System.IO.Directory.Exists(CSSFileName) then
			FCKeditor1.SkinPath =  _portalSettings.UploadDirectory & "skin/fckeditorhtml/"
			else
			CSSFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/")
			If System.IO.Directory.Exists(CSSFileName) then
			FCKeditor1.SkinPath =  _portalSettings.UploadDirectory & "skin/fckeditor/"
    		End If
			End If
		end sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) 
            pnlRichTextBox.Visible = False
			pnlhelp.Visible = True
        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) 

		Dim TobjAdmin As New AdminDB()
		if not Request.Params("help") Is Nothing then
        TObjAdmin.UpdatelonglanguageSetting(GetLanguage("N"), Request.Params("help") , FCKeditor1.value)
		Help.Text = ProcessLanguage(FCKeditor1.value, Page)
		end if		
        pnlRichTextBox.Visible = False
		pnlhelp.Visible = True
		
        End Sub
		
		
        Private Sub cmdEdit_Click(ByVal sender As Object, ByVal e As EventArgs) 
            pnlRichTextBox.Visible = True
			pnlhelp.Visible = False
			SetFckEditor()
		
			if not Request.Params("help") Is Nothing then
			Dim TobjAdmin As New AdminDB()
			FCKeditor1.value = TobjAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), Request.Params("help"))
			end if
			
        End Sub
		

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
 <head>
  <title><%= DotNetZoom.GetLanguage("Site_Help") %></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
	<meta name="robots" content="noindex, nofollow" />
	<asp:literal id="StyleSheet" enableviewstate="false" runat="server" />
    <style type="text/css">.scroll {overflow: auto; height : 79%}</style>
<!--[if IE]>
<style type="text/css">.scroll {overflow: auto; height : 328px}</style>
<![endif]-->
	
<script type="text/javascript">
		
function Cancel()
{
	window.top.close() ;
	window.top.opener.focus() ;
}
</script>

</head>
<body>
<form id="Form1"  enableviewstate="true" method="post" runat="server">
<asp:placeholder id="pnlRichTextBox" Runat="server" Visible="False">
<div align="center">
<FCKeditorV2:FCKeditor id="FCKeditor1" BasePath="~/FCKeditor/" runat="server"></FCKeditorV2:FCKeditor>
</div>
<p align="left">
    <asp:LinkButton cssclass="CommandButton" id="cmdUpdate"  onclick="cmdUpdate_Click" visible="true" runat="server"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton cssclass="CommandButton" id="cmdCancel"  onclick="cmdCancel_Click" visible="true" runat="server" CausesValidation="False"></asp:LinkButton>
</p>
</asp:placeholder>	
<asp:placeholder id="pnlhelp" Runat="server" Visible="True">
<table  class="OtherCellTop" cellpadding="0" cellspacing="0" border="0" width="100%">
 	<tr>
		<td align="center"  height="10%" valign="middle" width="100%">
		<span class="Head"><%= DotNetZoom.GetLanguage("HelpInfo") %></span>
		</td>
	</tr>
</table>
<div align="center" class="scroll">
<table class="MainBorder" cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr>
		<td align="left" valign="top">
		<asp:literal id="Help" runat="server" />
		</td>
	</tr>
</table>
</div>
<table class="OtherCellBottom" cellpadding="0" cellspacing="0" border="0" width="100%">
 	<tr>
		<td align="left" height="10%">
	    <asp:LinkButton cssclass="CommandButton" id="cmdEdit" onclick="cmdEdit_Click" visible="False" runat="server" CausesValidation="False"></asp:LinkButton>
		<td>
	 	<td align="right">
		<input id="btnCANCEL" class="button" type="button" value="<%= DotNetZoom.GetLanguage("return") %>" onclick="Cancel();" />
		</td>
	</tr>
</table>
</asp:placeholder>
</form>	
</body>
</html>