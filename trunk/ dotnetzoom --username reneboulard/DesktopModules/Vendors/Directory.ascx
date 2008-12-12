<%@ Control Language="vb" autoeventwireup="false" Explicit="True" codebehind="Directory.ascx.vb" Inherits="DotNetZoom.ServiceDirectory" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server" EnableViewState="false"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
    <table cellspacing="0" cellpadding="2" border="0">
        <tbody>
            <tr>
                <td class="NormalBold" valign="bottom">
                    <label for="<%=txtSearch.ClientID%>"><%= DotNetZoom.getlanguage("label_search") %>:</label></td>
                <td valign="bottom">
                    <asp:TextBox id="txtSearch" Runat="server" CssClass="NormalTextBox" MaxLength="200" Columns="30"></asp:TextBox>
                </td>
                <td valign="bottom">
                    <asp:LinkButton id="lnkSearch" Runat="server" CssClass="CommandButton"></asp:LinkButton>
                </td>
				<td valign="bottom">
				&nbsp;
				<asp:LinkButton id="cmdSignup" Runat="server" CssClass="CommandButton"></asp:LinkButton>
            	</td>
			</tr>
        </tbody>
    </table>
    <asp:DataList id="lstVendors" runat="server" RepeatColumns="1" CellPadding="4">
        <ItemTemplate>
            <table border="0">
                <tr>
                    <td colspan="2">
                        <asp:HyperLink ID="lnkVendorName" Runat="server" cssClass="NormalBold"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td width="50%" valign="top" style="white-space: nowrap;">
                        <asp:Label id="lblAddress" runat="server" cssclass="Normal"></asp:Label> <asp:Label id="lblTelephone" runat="server" cssclass="Normal"></asp:Label> <asp:Label id="lblFax" runat="server" cssclass="Normal"></asp:Label> <asp:Label id="lblEmail" runat="server" cssclass="Normal"></asp:Label>
                        <asp:HyperLink ID="lnkWebsite" Runat="server" CssClass="Normal"></asp:HyperLink>
                    </td>
                    <td width="50%" valign="top" style="white-space: nowrap;">
                        <asp:Hyperlink ID="lnkMap" Runat="server" CssClass="Normal" Visible="False"></asp:Hyperlink><br>
                        <asp:Hyperlink ID="lnkDirections" Runat="server" CssClass="Normal" Visible="False"></asp:Hyperlink><br>
                        <asp:Hyperlink ID="lnkFeedback" Runat="server" CssClass="Normal" Visible="False"></asp:Hyperlink>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Hyperlink ID="lnkLogo" Runat="server" CssClass="Normal"></asp:Hyperlink>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>