<%@ Control language="vb" CodeBehind="TTT_MediaPlayer.ascx.vb" AutoEventWireup="false" Inherits="DotNetZoom.TTT_MediaPlayer" %>
<table cellSpacing="0" cellPadding="0" align="center" Class="TTTBorder">
	<tr>
		<td class="TTTHeader" id="TitleBox" valign="middle" align="center" width="100%" height="28"><asp:label id="lblTitle" Runat="server" CssClass="TTTHeaderText"></asp:label></td>
	</tr>
	<tr>
		<td id="VU" valign="middle" align="center" width="100%" height="100%">
	
			<OBJECT id="Player" CLASSID="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" width="640" height="480" type="application/x-oleobject">
						<PARAM Name="AutoStart" Value="true">
						<PARAM NAME="SendPlayStateChangeEvents" VALUE="True">
						<PARAM Name="uiMode" Value="full">
						<PARAM Name="URL" <%= "Value=""" & Trim(MovieURL()) & """" %>>
			<EMBED <%= "src=""" & Trim(MovieURL()) & """" %> ></EMBED>
			</OBJECT>
		</td>
	</tr>
	<tr>
		<td valign="top" align="center" Class="TTTAltHeader" height="28" width="100%"><asp:Button cssclass="button" id="btnBack" runat="server"></asp:Button></td>
	</tr>
</table>
<asp:Label id="ErrorMessage" CssClass="TTTNormalBold" runat="server" Visible="False"></asp:Label>