<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TTT_MediaPlayer.aspx.vb" Inherits="DotNetZoom.TTT_GalleryMediaPlayer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<title><%= DotNetZoom.GetLanguage("Media_Player") %></title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
		<asp:placeholder id="CSS" runat="server"></asp:placeholder>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" align="center" Class="TTTBorder">
				<tr>
					<td class="TTTHeader" id="TitleBox" align="center" width="100%" height="28"><asp:label id="lblTitle" Runat="server" CssClass="TTTHeaderText"></asp:label></td>
				</tr>
				<tr>
				<td id="VU" align="center" width="100%" height="100%">
					<OBJECT id="Player" CLASSID="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" width="640" height="480" type="application/x-oleobject">
						<PARAM Name="AutoStart" Value="true">
						<PARAM NAME="SendPlayStateChangeEvents" VALUE="True">
						<PARAM Name="uiMode" Value="full">
						<PARAM Name="URL" <%= "Value=""" & Trim(MovieURL()) & """" %>>
			<EMBED <%= "src=""" & Trim(MovieURL()) & """" %> ></EMBED>
			</OBJECT>
					</td>
				</tr>				
			</table>
			<asp:Label id="ErrorMessage" CssClass="TTTNormalBold" runat="server" Visible="False"></asp:Label>
		</form>
	</body>
</html>