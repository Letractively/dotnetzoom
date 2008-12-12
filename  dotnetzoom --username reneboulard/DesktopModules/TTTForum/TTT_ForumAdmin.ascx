<%@ Control Inherits="DotNetZoom.TTT_ForumAdmin" codebehind="TTT_ForumAdmin.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="TTT" TagName="ForumNav" Src="TTT_ForumNavigator.ascx" %>
<%@ Register TagPrefix="TTT" TagName="ForumDetails" Src="TTT_ForumDetails.ascx" %>
<%@ Register TagPrefix="TTT" TagName="ForumAdminNav" Src="TTT_ForumAdminNavigator.ascx" %>
<%@ import Namespace="DotNetZoom" %>
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="780">
    <tbody>
        <tr>
            <td class="TTTAltHeader" style="white-space: nowrap;" align="left" runat="server">
                &nbsp;<%= DotNetZoom.GetLanguage("F_GroupAdmin") %> 
            </td>
            <td class="TTTAltHeader" style="white-space: nowrap;" align="right" runat="server">
                <TTT:ForumAdminNav id="ctlForumAdminNavigator" runat="Server"></TTT:ForumAdminNav>
            </td>
        </tr>
        <asp:placeholder id="pnlNavigator" Runat="server">
            <tr>
                <td valign="top" width="780" colspan="2" height="100%">
                    <TTT:FORUMNAV id="ForumNavigator" runat="Server" EditPost="false" IsReadonly="False"></TTT:FORUMNAV>
                </td>
            </tr>
        </asp:placeholder>
        <asp:placeholder id="pnlForumDetails" Runat="server">
            <tr>
                <td colspan="2">
                    <TTT:FORUMDETAILS id="ForumDetails" runat="Server"></TTT:FORUMDETAILS>
                </td>
            </tr>
        </asp:placeholder>
    </tbody>
</table>