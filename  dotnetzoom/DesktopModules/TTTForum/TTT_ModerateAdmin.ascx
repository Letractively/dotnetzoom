<%@ Control Inherits="DotNetZoom.TTT_ModerateAdmin" codebehind="TTT_ModerateAdmin.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="TTT" TagName="ForumNav" Src="TTT_ForumNavigator.ascx" %>
<%@ Register TagPrefix="TTT" TagName="ModerateDetails" Src="TTT_ModerateDetails.ascx" %>
<%@ Register TagPrefix="TTT" TagName="ForumAdminNav" Src="TTT_ForumAdminNavigator.ascx" %>
<%@ import Namespace="DotNetZoom" %>
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="780">
    <tbody>
        <tr>
            <td class="TTTAltHeader" style="white-space: nowrap;" align="left" runat="server">
                &nbsp;<%= DotNetZoom.getlanguage("F_AdminModerate") %> 
            </td>
            <td class="TTTAltHeader" style="white-space: nowrap;" align="right" runat="server">
                <TTT:ForumAdminNav id="cltAdminNavigator" Runat="Server"></TTT:ForumAdminNav>
            </td>
        </tr>
        <asp:placeholder id="pnlNavigator" Runat="server">
            <tr>
                <td valign="top" width="100%" colspan="2" height="100%">
                    <TTT:FORUMNAV id="ForumNav" runat="Server" EditPost="false" IsReadonly="True"></TTT:FORUMNAV>
                </td>
            </tr>
        </asp:placeholder>
        <asp:placeholder id="pnlModerate" Runat="server">
            <tr>
                <td width="100%" colspan="2" runat="server">
                    <TTT:MODERATEDETAILS id="ModerateDetails" runat="Server"></TTT:MODERATEDETAILS>
                </td>
            </tr>
        </asp:placeholder>
        <tr>
            <td class="TTTAltHeader" align="left" colspan="2" height="28">
                &nbsp; 
                <asp:Button cssclass="button" id="btnBack" runat="server" visible="false" CommandName="back"></asp:Button>
            </td>
        </tr>
    </tbody>
</table>