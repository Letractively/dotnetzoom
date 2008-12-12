<%@ Control Language="vb" codebehind="TTT_ForumDetails.ascx.vb" autoeventwireup="false" Inherits="DotNetZoom.TTT_ForumDetails" %>
<%@ Register TagPrefix="TTT" TagName="ForumModerateDetails" Src="TTT_ModerateDetails.ascx" %>
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="100%">
    <tbody>
        <tr>
            <td class="TTTHeader" width="100%" height="28">
                <span class="TTTHeaderText">&nbsp;<%= DotNetZoom.GetLanguage("F_AdminParam") %></span> 
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="1" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="TTTSubHeader" width="148" height="24">
                                &nbsp;<%= DotNetZoom.GetLanguage("F_ForumID") %>:</td>
                            <td class="TTTRow" align="left">
                                <asp:textbox id="txtForumID" Columns="26" Enabled="False" width="380" cssclass="NormalTextBox" runat="server"></asp:textbox>
                                &nbsp; 
                                <asp:checkbox id="chkActive" runat="server"></asp:checkbox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TTTSubHeader" width="148" height="24">
                                &nbsp;<%= DotNetZoom.GetLanguage("F_ForumName") %>:</td>
                            <td class="TTTRow" align="left">
                                <asp:textbox id="txtForumName" Columns="26" width="380" cssclass="NormalTextBox" runat="server"></asp:textbox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TTTSubHeader" width="148" height="24">
                                &nbsp;<%= DotNetZoom.GetLanguage("F_ForumGroup") %>:</td>
                            <td class="TTTRow" align="left">
                                <asp:textbox id="txtForumGroupName" Enabled="False" cssclass="NormalTextBox" runat="server" Width="380px"></asp:textbox>
                                <asp:textbox id="txtForumGroupID" Columns="26" width="75px" cssclass="NormalTextBox" runat="server" Visible="False"></asp:textbox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TTTSubHeader" width="148" height="24">
                                &nbsp;<%= DotNetZoom.GetLanguage("F_Desc") %>:</td>
                            <td class="TTTRow" align="left">
                                <asp:textbox id="txtForumDescription" Columns="26" width="380" cssclass="NormalTextBox" runat="server" TextMode="MultiLine"></asp:textbox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TTTSubHeader" width="148" height="24">
                                &nbsp;<%= DotNetZoom.GetLanguage("F_CreatedBy") %>:</td>
                            <td class="TTTRow" align="left">
                                <asp:textbox id="txtForumCreatorName" Enabled="False" cssclass="NormalTextBox" runat="server" Width="380px"></asp:textbox>
                                <asp:textbox id="txtForumCreatorID" Columns="26" width="75px" cssclass="NormalTextBox" runat="server" Visible="False"></asp:textbox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TTTSubHeader" width="148" height="24">
                                &nbsp;<%= DotNetZoom.GetLanguage("F_Private") %></td>
                            <td class="TTTRow" align="left">
                                <asp:checkbox id="chkPrivate" runat="server" AutoPostBack="True"></asp:checkbox>
                            </td>
                        </tr>
                        <asp:placeholder id="pnlPrivate" Visible="True" Runat="server">
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.GetLanguage("F_AuthRoles") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:checkboxlist id="authRoles" width="100%" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial" RepeatColumns="3"></asp:checkboxlist>
                                </td>
                            </tr>
                        </asp:placeholder>
                        <tr>
                            <td class="TTTSubHeader" width="148" height="24">
                                &nbsp;<%= DotNetZoom.GetLanguage("F_Moderated") %></td>
                            <td class="TTTRow" align="left">
                                <asp:checkbox id="chkModerated" runat="server" AutoPostBack="True"></asp:checkbox>
                                &nbsp; 
                                <asp:hyperlink id="lnkForumModerateAdmin" runat="server" CssClass="CommandButton" Visible="False"></asp:hyperlink>
                                &nbsp; 
                            </td>
                        </tr>
                        <asp:placeholder id="pnlForumModerateAdmin" Runat="server">
                            <tr>
                                <td colspan="2">
                                    <TTT:ForumModerateDetails id="ctlForumModerateAdmin" runat="server"></TTT:ForumModerateDetails>
                                </td>
                            </tr>
                        </asp:placeholder>
                        <tr>
			            	<td class="TTTAltHeader" align="left"  height="28">
			                	&nbsp; 
            			    	<asp:Button cssclass="button" id="btnBack" runat="server" CommandName="back"></asp:Button>
            				</td>

                            <td class="TTTAltHeader" align="right"  height="28">
                                <asp:button id="btnUpdate" cssclass="button" runat="server" CommandName="save"></asp:button>
                                &nbsp; 
                            </td>
                        </tr>
                        <tr>
                            <td class="TTTAltHeader" width="100%" colspan="2" height="28">
                                <span class="TTTHeaderText">&nbsp;<%= DotNetZoom.GetLanguage("F_GalleryParam") %></span>&nbsp;<span class="TTTNormal">&nbsp;(<%= DotNetZoom.GetLanguage("F_GalleryParam1") %>)</span> 
                            </td>
                        </tr>
                        <tr>
                            <td class="TTTSubHeader" width="148" height="24">
                                &nbsp;<%= DotNetZoom.GetLanguage("F_GalleryParam2") %></td>
                            <td class="TTTRow" align="left">
                            <asp:checkbox id="chkIntegrated" runat="server"></asp:checkbox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TTTSubHeader" width="148" height="24">
                                &nbsp;<%= DotNetZoom.GetLanguage("F_GalleryParam3") %>:</td>
                            <td class="TTTRow" align="left">
                                <asp:textbox id="txtGalleryID" Columns="26" width="339px" cssclass="NormalTextBox" runat="server" ReadOnly="True"></asp:textbox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TTTSubHeader" width="148" height="24">
                                &nbsp;<%= DotNetZoom.GetLanguage("F_GalleryParam4") %>:</td>
                            <td class="TTTRow" align="left">
                                <asp:textbox id="txtAlbumName" Columns="26" width="339px" cssclass="NormalTextBox" runat="server" ReadOnly="True"></asp:textbox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>