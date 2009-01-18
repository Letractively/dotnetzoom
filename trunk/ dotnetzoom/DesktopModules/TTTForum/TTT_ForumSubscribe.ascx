<%@ Control Language="vb" codebehind="TTT_ForumSubscribe.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.TTT_ForumSubscribe" %>
<%@ import Namespace="DotNetZoom" %>
<table class="TTTBorder" cellspacing="1" cellpadding="1" width="100%">
    <tbody>
        <tr>
            <td class="TTTHeader" colspan="2" height="28">
                <span class="TTTTitle">&nbsp;<%= DotNetZoom.getlanguage("F_SubForum") %></span> 
            </td>
        </tr>
		<tr>
          <td class="TTTAltHeader" width="100%" colspan="2" height="28">
          <span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_SubParamEMail") %></span><asp:span class="TTTNormal">&nbsp;
		  <%= DotNetZoom.getlanguage("F_SubParamEMailInfo") %></span></td>
        </tr>

        <tr>
            <td valign="top" width="80%">
                <asp:datalist id="lstGroup" runat="server" Width="100%" BorderColor="#D1D7DC" BorderWidth="1" GridLines="both" CellPadding="0" CellSpacing="0" DataKeyField="ForumGroupID">
                    <SelectedItemTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" runat="server" id="Table1">
                            <tr>
                                <td class="TTTAltHeader" width="100%">
                                    &nbsp; 
                                    <asp:ImageButton id="btnCollapse" ToolTip='<%# DotNetZoom.getlanguage("F_Close") %>' AlternateText='<%# DotNetZoom.getlanguage("F_Close") %>' ImageURL="~/images/minus2.gif" runat="server" CommandName="collapse" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' BorderWidth="0" BorderStyle="none" />
                                    &nbsp; <asp:Label cssclass="TTTAltHeaderText" text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' runat="server" id="lblGroupName" /> 
                                </td>
                            </tr>
                        </table>
                        <asp:DataList id="lstForum" CellSpacing="0" CellPadding="0" GridLines="both" BorderWidth="0" BorderColor="#D1D7DC" Width="100%" datasource="<%# GetForums() %>" runat="server">
                            <ItemTemplate>
                                <asp:placeholder ID="pnlForumList" Runat="server" visible='<%# CanView(IIf(DataBinder.Eval(Container.DataItem, "AuthorizedRoles") is System.DBNull.Value, "", DataBinder.Eval(Container.DataItem, "AuthorizedRoles"))) %>'>
                                    <table width="100%" cellpadding="3" cellspacing="1" border="0" runat="server" id="Table2">
                                        <tr>
                                            <td class="TTTSubHeader" width="50" align="Center" valign="middle">
                                                <asp:image id="forumimage" ImageUrl='<%# ForumConfig.SkinImageFolder() & "TTT_board_thread.gif" %>' runat="server"></asp:image>
                                            </td>
                                            <td class="TTTRow" width="100%">
                                                <asp:Label cssclass="TTTSubHeader" text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' runat="server" id="lblForumName" /> <span class="TTTNormal">&nbsp;(<%# DataBinder.Eval(Container.DataItem,"Description") %>)</span> 
                                            </td>
                                            <td class="TTTRowHighlight">
                                                <asp:ImageButton id="btnForumAdd" ToolTip='<%# DotNetZoom.getlanguage("F_Subscribe") %>' AlternateText='<%# DotNetZoom.getlanguage("F_Subscribe") %>' ImageURL="~/images/rt.gif" runat="server" CommandName="Add" OnClick="AddSubscribe_Click" visible='<%# CanAdd(CType(DataBinder.Eval(Container.DataItem, "ForumID"), String)) %>' CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                                <asp:ImageButton id="btnForumRemove" ToolTip='<%# DotNetZoom.getlanguage("F_UnSubscribe") %>' AlternateText='<%# DotNetZoom.getlanguage("F_Cancel") %>' ImageURL="~/images/lt.gif" runat="server" CommandName="Delete" OnClick="RemoveSubscribe_Click" visible='<%# CanRemove(CType(DataBinder.Eval(Container.DataItem, "ForumID"), String)) %>' CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:placeholder>
                            </ItemTemplate>
                        </asp:DataList>
                    </SelectedItemTemplate>
                    <ItemStyle cssclass="TTTNormal"></ItemStyle>
                    <ItemTemplate>
                        <table width="100%" cellpadding="0" cellspacing="1" border="0" runat="server" id="Table5">
                            <tr>
                                <td class="TTTAltHeader" width="100%">
                                    &nbsp; 
                                    <asp:ImageButton id="btnExpand" ToolTip='<%# DotNetZoom.getlanguage("F_Expand") %>' AlternateText='<%# DotNetZoom.getlanguage("F_Expand") %>' ImageURL="~/images/plus2.gif" runat="server" CommandName="expand" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                    &nbsp; <asp:Label cssclass="TTTAltHeaderText" text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' runat="server" id="Label1" /> 
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:datalist>
            </td>
            <td class="TTTRow" valign="top" width="20%" runat="server">
                <asp:listbox id="lstSubscription" runat="server" Width="100%" Rows="6" DataTextField="Name" DataValueField="ForumID" CssClass="TTTSubHeader"></asp:listbox>
            </td>
        </tr>
        <tr>
            <td class="TTTAltHeader" colspan="2" height="28">
                &nbsp; 
                <asp:button cssclass="button" id="btnBack" runat="server"  CommandName="back"></asp:button>
                &nbsp; 
            </td>
        </tr>
    </tbody>
</table>