<%@ Control Inherits="DotNetZoom.TTT_ForumAdminNavigator" codebehind="TTT_ForumAdminNavigator.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
    <tbody>
        <tr>
            <td class="TTTAltHeader" style="white-space: nowrap;" align="right" width="100%" runat="server">
                <asp:hyperlink id="lnkForumSetting" CssClass="CommandButton" runat="server" Visible="False"></asp:hyperlink>
                <asp:hyperlink id="lnkUserAdmin" CssClass="CommandButton" runat="server" Visible="False"></asp:hyperlink>
                <asp:hyperlink id="lnkForumAdmin" CssClass="CommandButton" runat="server" Visible="False"></asp:hyperlink>
                <asp:hyperlink id="lnkModerateAdmin" CssClass="CommandButton" runat="server" Visible="False"></asp:hyperlink>
				<asp:hyperlink id="lnkHome" CssClass="CommandButton" runat="server"></asp:hyperlink>&nbsp;
            </td>
        </tr>
    </tbody>
</table>