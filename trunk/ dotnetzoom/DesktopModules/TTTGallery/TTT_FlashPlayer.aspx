<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TTT_FlashPlayer.aspx.vb" Inherits="DotNetZoom.TTT_GalleryFlashPlayer" %>
<%@ Register TagPrefix="NB" Namespace="NetBrick.Web" Assembly="NetBrick.Web" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<title><%= DotNetZoom.GetLanguage("Flash_Player") %></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="1" cellPadding="0" width="400" align="center" border="0">
				<tr>
					<td class="TTTHeader" id="TitleBox" align="center" width="100%" height="28"><asp:label id="lblTitle" Runat="server" CssClass="TTTHeaderText"></asp:label></td>
				</tr>
				<tr>
					<td id="VU" align="center" valign="top" width="100%" height="400">
						<NB:FLASHLIGHT id="FLight" runat="server" height="372" Width="400" WMode="Transparent" Scale="ShowAll" SAlign="Top"></NB:FLASHLIGHT>
					</td>
				</tr>
			</table>
			<asp:Label id="ErrorMessage" CssClass="TTTNormalBold" runat="server" Visible="False"></asp:Label>
		</form>
	</body>
</html>