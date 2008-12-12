<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Control language="vb" CodeBehind="TTT_Viewer.ascx.vb" AutoEventWireup="false" Inherits="DotNetZoom.TTT_Viewer" %>
<table class="TTTBorder" cellspacing="1" cellpadding="0" align="center">
	<tr>
		<td class="TTTHeader" valign="middle" height="28">
			<asp:HyperLink id="cmdAlbum" runat="server"  CssClass="TTTHeaderText"></asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="TTTAltHeader" align="left">&nbsp;
						<asp:hyperlink id="MovePrevious" runat="server" ImageUrl="~/images/lt.gif"><%= DotNetZoom.getlanguage("Gal_Prev") %></asp:hyperlink>
					</td>
					<td class="TTTAltHeader" align="center">
						<asp:label id="Title" cssclass="TTTNormalBold" runat="server"></asp:label>
					</td>
					<td class="TTTAltHeader" align="right">
						<asp:hyperlink id="MoveNext" runat="server" ImageUrl="~/images/rt.gif"><%= DotNetZoom.getlanguage("Gal_Next") %></asp:hyperlink>&nbsp;
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="center" class="TTTRow" height="22">
			<asp:label id="Description" cssclass="TTTRow" runat="server"></asp:label>
		</td>
	</tr>
	<tr>
		<td valign="middle" align="center" class="TTTRow">
			<asp:image id="Image" Alternatetext="Image" runat="server" BorderStyle="Outset" BorderWidth="2px" BorderColor="#D1D7DC"></asp:image>
		</td>
	</tr>
	<tr>
		<td align="center" valign="middle" class="TTTRow" height="22">
			<asp:label id="lblInfo" cssclass="TTTRow" runat="server"></asp:label>
		</td>
	</tr>
	<tr>
		<td class="TTTAltHeader" style="white-space: nowrap;" valign="middle" align="center">
		<asp:HyperLink id="cmdBack" runat="server" CssClass="button"></asp:HyperLink>
		</td>
	</tr>
</table>