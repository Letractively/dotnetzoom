<%@ Control Language="vb" Inherits="DotNetZoom.MapGoogle" codebehind="MapGoogle.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="2" cellpadding="2" border="0">
	<tbody>
		<tr>
		<td valign="middle" colspan="3">
		<asp:Label id="lblLocation" runat="server" cssclass="NormalBold"></asp:Label>
		</td>
		</tr>
		<tr>
		<td valign="middle" colspan="3">
		<asp:Label id="lblAddress" runat="server" cssclass="Normal"></asp:Label>
		</td>
		</tr>
		<tr>
		<td valign="middle" colspan="3">
		<asp:HyperLink id="hypDirections" Runat="server" CssClass="NormalBold"></asp:HyperLink>
		</td>
		</tr>
		<tr>
		<td id="colSize" align="left" runat="server">
		<label for="<%=cboSize.ClientID%>"><span class="SubHead"><%= DotNetZoom.getlanguage("MapGoogle_size") %>:</span></label>&nbsp;
		<asp:DropDownList id="cboSize" Runat="server" CssClass="NormalTextBox" AutoPostBack="True">
		<asp:ListItem Value="width: 250px; height: 150px">Small</asp:ListItem>
		<asp:ListItem Value="width: 500px; height: 300px">Large</asp:ListItem>
		</asp:DropDownList>
		</td>
		<td id="colZoom" align="right" runat="server">
		<label for="<%=cboZoom.ClientID%>"><span class="SubHead"><%= DotNetZoom.getlanguage("MapGoogle_zoom") %>:</span></label>&nbsp;
		<asp:DropDownList id="cboZoom" Runat="server" CssClass="NormalTextBox" AutoPostBack="True">
		<asp:ListItem Value="0">1</asp:ListItem>
		<asp:ListItem Value="1">2</asp:ListItem>
		<asp:ListItem Value="2">3</asp:ListItem>
		<asp:ListItem Value="3">4</asp:ListItem>
		<asp:ListItem Value="4">5</asp:ListItem>
		<asp:ListItem Value="5">6</asp:ListItem>
		<asp:ListItem Value="6">7</asp:ListItem>
		<asp:ListItem Value="7">8</asp:ListItem>
		<asp:ListItem Value="8">9</asp:ListItem>
		<asp:ListItem Value="9">10</asp:ListItem>
		<asp:ListItem Value="10">11</asp:ListItem>
		<asp:ListItem Value="11">12</asp:ListItem>
		<asp:ListItem Value="12">13</asp:ListItem>
		<asp:ListItem Value="13">14</asp:ListItem>
		<asp:ListItem Value="14">15</asp:ListItem>
		<asp:ListItem Value="15">16</asp:ListItem>
		<asp:ListItem Value="16">17</asp:ListItem>

		</asp:DropDownList>
		</td>
		</tr>
		<tr>
		<td valign="middle" colspan="3">
		<asp:LinkButton class="CommandButton" id="hypMap" visible="False" runat="server" ></asp:LinkButton>
		<asp:literal id="hypMapImage" visible="false" Runat="server" />
		</td>
		</tr>
	</tbody>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>