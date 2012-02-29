<%@ Page Language="VB" Debug="false"  Inherits="DotNetZoom.BasePage"%>
<%@ Register TagPrefix="Editor"  Namespace="dotnetzoom" Assembly="DotNetZoom" %>
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
        End If
        

end sub
    Private Sub SetFckEditor()
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        FCKeditor1.Width = Unit.Pixel(650)
        FCKeditor1.Height = Unit.Pixel(350)
        SetEditor(FCKeditor1)
        FCKeditor1.LinkBrowserURL = glbPath & "admin/AdvFileManager/filemanager.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
        FCKeditor1.ImageBrowserURL = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
        Session("FCKeditor:UserFilesPath") = _portalSettings.UploadDirectory
        ' set the css for the editor if it exist
    End Sub

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
	
<script type="text/javascript">
		
function Cancel()
{
	window.top.close() ;
	window.top.opener.focus() ;
}
</script>

</head>
<body class="MainBorder">
<form id="Form1"  enableviewstate="true" method="post" runat="server">
<asp:placeholder id="pnlRichTextBox" Runat="server" Visible="False">
<div align="center">
<editor:FCKeditor id="FCKeditor1" BasePath="~/FCKeditor/" runat="server"></editor:FCKeditor>
</div>
<p align="left">
    <asp:LinkButton cssclass="CommandButton" id="cmdUpdate"  onclick="cmdUpdate_Click" visible="true" runat="server"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton cssclass="CommandButton" id="cmdCancel"  onclick="cmdCancel_Click" visible="true" runat="server" CausesValidation="False"></asp:LinkButton>
</p>
</asp:placeholder>	
<asp:placeholder id="pnlhelp" Runat="server" Visible="True">
<div align="center">
<table class="master" cellpadding="10" cellspacing="0" border="0" width="100%">
	<tr>
	<td align="right" valign="top" class="normal"><a href="javascript:Cancel();" title="<%= DotNetZoom.GetLanguage("return") %>"><%= DotNetZoom.GetLanguage("return") %></a>
    </td>
	</tr>
	<tr>
		<td height="330px" align="left" valign="top">
		<asp:literal id="Help" runat="server" />
		</td>
	</tr>
	<tr>
	 	<td align="right" class="normal">
	 	<asp:LinkButton cssclass="CommandButton" id="cmdEdit" onclick="cmdEdit_Click" visible="False" runat="server" CausesValidation="False"></asp:LinkButton>
        &nbsp;&nbsp;&nbsp;
        <a href="javascript:Cancel();" title="<%= DotNetZoom.GetLanguage("return") %>"><%= DotNetZoom.GetLanguage("return") %></a>
		</td>
	</tr>
</table>
</div>
</asp:placeholder>
</form>	
</body>
</html>