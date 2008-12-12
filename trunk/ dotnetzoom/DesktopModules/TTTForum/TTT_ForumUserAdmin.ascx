<%@ Control Language="vb" autoeventwireup="false" codebehind="TTT_ForumUserAdmin.ascx.vb" Inherits="DotNetZoom.TTT_ForumUserAdmin" %>
<%@ Register TagPrefix="TTT" TagName="ModerateForums" Src="TTT_ModeratorForumList.ascx" %>
<%@ Register TagPrefix="TTT" TagName="UsersControl" Src="TTT_UsersControl.ascx" %>
<%@ Register TagPrefix="TTT" TagName="ForumAdminNav" Src="TTT_ForumAdminNavigator.ascx" %>
<table cellspacing="0" cellpadding="0" width="780" border="0">
    <tbody>
        <tr>
            <td class="TTTAltHeader" align="left">
                &nbsp;<%= DotNetZoom.getlanguage("F_AdminUser") %></td>
            <td class="TTTAltHeader" align="right">
                <TTT:ForumAdminNav id="ctlForumAdminNavigator" runat="Server"></TTT:ForumAdminNav>
            </td>
        </tr>
        <asp:placeholder id="pnlUsersList" Runat="server">
            <tr>
                <td colspan="2">
                    <TTT:USERSCONTROL id="ctlUsers" runat="server" ShowEmail="True" ShowUserNameLink="True"></TTT:USERSCONTROL>
                </td>
            </tr>
        </asp:placeholder>
        <asp:placeholder id="pnlUserAdmin" Runat="server">
            <tr>
                <td colspan="2">
                    <table class="TTTBorder" cellspacing="1" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td class="TTTHeader" width="100%" colspan="2" height="28">
                                    <span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_UserAdmin") %></span></td>
                            </tr>
                            <tr>
                                <td class="TTTAltHeader" colspan="2" height="28">
                                    &nbsp; <asp:Label id="lblInfo" runat="server" forecolor="Red" cssclass="TTTNormal" width="100%"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserID") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtUserID" runat="server" Enabled="False" Columns="26" width="340" cssclass="NormalTextBox"></asp:textbox>
                                    &nbsp; 
                                   
                                    <asp:hyperlink id="lnkProfile" runat="server" CssClass="CommandButton"></asp:hyperlink>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserCode") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtUserName" runat="server" Enabled="False" Columns="26" width="340" cssclass="NormalTextBox"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserAlias") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtAlias" runat="server" Enabled="False" Columns="26" width="340" cssclass="NormalTextBox"></asp:textbox>
                                </td>
                            </tr>
                            <asp:placeholder id="pnlModerate" Runat="server">
                                <tr>
                                    <td class="TTTSubHeader" width="180">
                                        &nbsp;<%= DotNetZoom.getlanguage("F_UserMode") %>:</td>
                                    <td class="TTTRow" align="left">
                                        <TTT:MODERATEFORUMS id="ctlModerateForums" runat="server"></TTT:MODERATEFORUMS>
                                    </td>
                                </tr>
                            </asp:placeholder>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserThread") %></td>
                                <td class="TTTRow" align="left">
                                    <asp:checkbox id="chkThreadTracking" runat="server" CssClass="TTTNormal"></asp:checkbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserPMS") %></td>
                                <td class="TTTRow" align="left">
                                    <asp:checkbox id="chkPrivateMessage" runat="server" CssClass="TTTNormal"></asp:checkbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserTrust") %></td>
                                <td class="TTTRow" align="left">
                                    <asp:checkbox id="chkIsTrusted" runat="server" CssClass="TTTNormal"></asp:checkbox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="TTTAltHeader" align="left" colspan="2">
                                    &nbsp; 
                                    <asp:button cssclass="button" id="CancelButton" runat="server"  CommandName="back"></asp:button>
                                    &nbsp; 
                                    <asp:button  cssclass="button" id="UpdateButton" runat="server"  CommandName="back"></asp:button>
                                    &nbsp; 
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </asp:placeholder>
    </tbody>
</table>