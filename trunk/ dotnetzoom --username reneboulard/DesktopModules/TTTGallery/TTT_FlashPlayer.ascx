<%@ Register TagPrefix="NB" Namespace="NetBrick.Web" Assembly="NetBrick.Web" %>
<%@ Control language="vb" CodeBehind="TTT_FlashPlayer.ascx.vb" AutoEventWireup="false" Inherits="DotNetZoom.TTT_FlashPlayer" %>
<table cellSpacing="1" cellPadding="5" width="400" align="center" Class="TTTBorder">
	<tr>
		<td class="TTTHeader" id="TitleBox" align="center" valign="middle" width="100%" height="28"><asp:label id="lblTitle" Runat="server" CssClass="TTTHeaderText"></asp:label></td>
	</tr>
	<tr>
		<td id="VU" valign="middle" width="100%" height="100%">
			<NB:FLASHLIGHT id="FLight" runat="server" height="360" Width="400" WMode="Transparent" Scale="ShowAll" SAlign="Top"></NB:FLASHLIGHT>
		</td>
	</tr>
	<tr>
		<td valign="top" align="center" Class="TTTAltHeader" height=28 width="100%"><asp:Button cssclass="button" id="btnBack" runat="server"></asp:Button></td>
	</tr>
</table>
<asp:Label id="ErrorMessage" CssClass="NormalBold" runat="server" Visible="False"></asp:Label>