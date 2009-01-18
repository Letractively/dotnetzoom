<%@ Control Inherits="DotNetZoom.TTT_ForumNavigator" codebehind="TTT_ForumNavigator.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ import Namespace="DotNetZoom" %>
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="100%" runat="server">
    <tbody>
        <tr>
            <td>
                <asp:datalist id="lstGroup" runat="server" CellSpacing="0" CellPadding="0" BorderColor="white" BorderWidth="1" GridLines="Horizontal" DataKeyField="ForumGroupID" Width="100%">
                    <HeaderTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblHeader">
                            <tr>
                                <td class="TTTHeader" width="60%" height="28">
                                    <span class="TTTHeaderText">&nbsp;<%= DotNetZoom.GetLanguage("F_ForumGroupS") %></span> 
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" runat="server" id="tblGroup">
                            <tr>
                                <td class="TTTSubHeader" width="80%" align="left" height="24" style="white-space: nowrap;">
                                    &nbsp; <asp:Label cssclass="TTTHeaderText" text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' runat="server" EnableViewState="true" id="Label1" /> <span class="TTTNormal">(<%# DataBinder.Eval(Container.DataItem,"ForumCount") %> <%# DotNetZoom.GetLanguage("F_Groups") %>)</span> 
                                </td>
                                <td class="TTTRowHighlight" width="20%" align="right" height="24" style="white-space: nowrap;">
                                    <asp:placeholder ID="pnlGroupEdit" Runat="server" Visible='<%# GroupCanEdit %>' >
                                        <asp:ImageButton  ToolTip='<%# DotNetZoom.GetLanguage("erase") %>' AlternateText='<%# DotNetZoom.GetLanguage("erase") %>' id="btnGroupDelete2" ImageURL="~/images/1x1.gif" Visible='<%# DataBinder.Eval(Container.DataItem,"ForumCount") = "0" %>'  runat="server" CommandName="delete" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' Width="16" Height="16" style='<%# GetimgStyle("delete") %>'/>
                                        <asp:ImageButton  ToolTip='<%# DotNetZoom.GetLanguage("F_Up1") %>' AlternateText='<%# DotNetZoom.GetLanguage("F_Up") %>' id="btnGroupUp2" ImageURL="~/images/up.gif" runat="server" CommandName="up" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                        <asp:ImageButton  ToolTip='<%# DotNetZoom.GetLanguage("F_Down1") %>' AlternateText='<%# DotNetZoom.GetLanguage("F_Down") %>' id="btnGroupDown2" ImageURL="~/images/dn.gif" runat="server" CommandName="down" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' BorderWidth="0" BorderStyle="none"/>&nbsp;
								</asp:placeholder>
                                </td>
                                <td class="TTTRowHighlight" align="center" height="24" style="white-space: nowrap;">
                                    <asp:ImageButton id="btnExpand" ToolTip='<%# DotNetZoom.GetLanguage("F_Expand") %>' AlternateText='<%# DotNetZoom.GetLanguage("F_Expand") %>' ImageURL="~/images/plus2.gif" runat="server" CommandName='<%# GetEditCommand %>' CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' BorderWidth="0" BorderStyle="none" />
                                    &nbsp; 
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <SelectedItemTemplate>
                        <table id="tblViewForum" cellspacing="0" cellpadding="0" width="100%" class="TTTBorder">
                            <tr>
                                <td class="TTTSubHeader" width="100%" align="left" height="24" style="white-space: nowrap;">
                                    &nbsp; <asp:Label cssclass="TTTHeaderText" text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' runat="server" EnableViewState="true" id="Label3" /> 
                                </td>
                                <td class="TTTRowHighlight" align="center" width="25" height="24" style="white-space: nowrap;">
                                    <asp:ImageButton id="btnColapse" ToolTip='<%# DotNetZoom.GetLanguage("F_Close") %>' AlternateText='<%# DotNetZoom.GetLanguage("F_Close") %>' ImageURL="~/images/minus2.gif" runat="server" CommandName='collapse' CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                    &nbsp; 
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DataList id="lstViewForum" GridLines="Horizontal" BorderWidth="1" CellPadding="0" CellSpacing="0" Width="100%" Height="100%" datasource="<%# BindForum() %>" runat="server">
                                        <ItemTemplate>
                                            <table width="100%" cellpadding="0" cellspacing="0" runat="server">
                                                <tr>
                                                    <td class="TTTRowHighlight" align="center" width="24" height="24" style="white-space: nowrap;">
                                                        <asp:ImageButton  id="btnSelect" AlternateText='<%# GetEditText(DataBinder.Eval(Container.DataItem, "IsModerated"))  %>' Tooltip='<%# GetEditText(DataBinder.Eval(Container.DataItem, "IsModerated")) %>' OnClick="ForumSelect_Click" height="16" width="16" style='<%# GetEditStyle(DataBinder.Eval(Container.DataItem, "IsModerated")) %>' runat="server" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumID"), String) %>'  ImageURL="~/images/1x1.gif" />
                                                    </td>
                                                    <td class="TTTRow" height="24" width="60%">
                                                        <asp:Label cssclass="TTTSubHeader" text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' runat="server" EnableViewState="true" id="Label2" />&nbsp; <span class="TTTNormal">(<%# DataBinder.Eval(Container.DataItem,"TotalPosts") %> <%# DotNetZoom.GetLanguage("F_Post") %>)</span> 
                                                    </td>
                                                    <td class="TTTRowHighlight" width="40%" height="24" align="center" style="white-space: nowrap;">
                                                        <span class="TTTNormal"><%# DotNetZoom.GetLanguage("F_CreatedBy") %>:&nbsp;<%# FormatUser(DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>&nbsp;<%# DotNetZoom.GetLanguage("F_CreatedOn") %>&nbsp;<%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %> </span> 
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </SelectedItemTemplate>
                    <EditItemTemplate>
                        <table id="tblGroupEdit" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                                        <tr>
                                            <td class="TTTSubHeader" width="100%" height="24" align="left">
                                                &nbsp; <asp:Label id="lbGroupName" cssclass="TTTHeaderText" text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' width="90%" runat="server" EnableViewState="true" ></asp:Label> 
                                            </td>
                                            <td class="TTTSubHeader" style="white-space: nowrap;" width="25" height="24" align="center">
                                                <asp:ImageButton id="btnCloseGroup" Tooltip='<%# DotNetZoom.GetLanguage("F_Close") %>' AlternateText='<%# DotNetZoom.GetLanguage("F_Close") %>' ImageURL="~/images/minus2.gif" runat="server" CommandName="cancel" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' BorderWidth="0" BorderStyle="none" />
                                                &nbsp; 
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblGroupEditDetails" cellspacing="1" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="TTTSubHeader" width="150" height="24">
                                                &nbsp;<%# DotNetZoom.GetLanguage("F_GroupName") %>:</td>
                                            <td class="TTTRow" align="left" height="24">
                                                <asp:textbox id="txtGroupName" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' runat="server" cssclass="NormalTextBox" width="360" Columns="26"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TTTSubHeader" width="150" height="24">
                                                &nbsp;<%# DotNetZoom.GetLanguage("F_GroupID") %>:</td>
                                            <td class="TTTRow" align="left" height="24">
                                                <asp:textbox id="txtGroupID" Text='<%# DataBinder.Eval(Container.DataItem, "ForumGroupID") %>' runat="server" cssclass="NormalTextBox" width="360" Enabled="False" Columns="26"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TTTSubHeader" width="150" height="24">
                                                &nbsp;<%# DotNetZoom.GetLanguage("F_CreatedBy") %>:</td>
                                            <td class="TTTRow" align="left" height="24">
                                                <asp:textbox id="txtGroupCreatorName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Creator").Name %>' cssclass="NormalTextBox" width="360" Enabled="False" Columns="26"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TTTSubHeader" width="150" height="24">
                                                &nbsp;<%# DotNetZoom.GetLanguage("F_NumberOf_F") %>:</td>
                                            <td class="TTTRow" align="left" height="24">
                                                <asp:textbox id="txtGroupCreatedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ForumCount") %>' cssclass="NormalTextBox" width="360" Enabled="False" Columns="26"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TTTSubHeader" width="150" height="24">
                                                &nbsp;</td>
                                            <td class="TTTAltHeader" align="right" height="24">
                                                &nbsp; 
                                                <asp:Button cssclass="button" ToolTip='<%# DotNetZoom.GetLanguage("F_Cancel") %>' id="btnCancelGroup"  Text='<%# DotNetZoom.GetLanguage("F_Cancel") %>' runat="server" CommandName="cancel" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' />
                                                &nbsp; 
                                                <asp:Button cssclass="button" ToolTip='<%# DotNetZoom.GetLanguage("enregistrer") %>' id="btnUpdateGroup"  Text='<%# DotNetZoom.GetLanguage("enregistrer") %>' runat="server" CommandName="update" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' />
                                                &nbsp;&nbsp; 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TTTSubHeader" width="150">
                                                &nbsp;<%# DotNetZoom.GetLanguage("F_ListOf_F") %>:</td>
                                            <td align="left" valign="top">
                                                <asp:DataList id="lstForum" GridLines="Horizontal" BorderWidth="1" BorderColor="white" CellPadding="0" CellSpacing="0" Width="100%" Height="100%" datasource="<%# BindForum() %>" runat="server">
                                                    <ItemTemplate>
                                                        <table width="100%" cellpadding="1" cellspacing="0" border="0" runat="server">
                                                            <tr>
                                                                <td class="TTTRow" width="60%" height="24">
                                                                    <asp:Label cssclass="TTTSubHeader" text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' runat="server" EnableViewState="true" id="lblForumName" />&nbsp; <span class="TTTNormal">(<%# DataBinder.Eval(Container.DataItem,"TotalPosts") %>  <%# DotNetZoom.GetLanguage("F_Post") %>)</span> 
                                                                </td>
                                                                <td class="TTTRowHighlight" width="40%" height="24" align="center" style="white-space: nowrap;">
                                                                    <span class="TTTNormal"><%# DotNetZoom.GetLanguage("F_CreatedBy") %>:&nbsp;<%# FormatUser(DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>&nbsp;<%# DotNetZoom.GetLanguage("F_CreatedOn") %>&nbsp;<%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %> </span> 
                                                                </td>
                                                                <td class="TTTRowHighlight" align="right" height="24" style="white-space: nowrap;">
                                                                    <asp:ImageButton  id="btnForumEdit" ToolTip='<%# DotNetZoom.GetLanguage("F_Edit") %>' AlternateText='<%# DotNetZoom.GetLanguage("F_Edit") %>' height="16" width="16" ImageURL="~/images/1x1.gif" runat="server" CommandName="edit" OnClick="EditForum_Click" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumID"), String) %>' style='<%# GetimgStyle("edit") %>'/>
                                                                    <asp:ImageButton  id="btnForumDelete" ToolTip='<%# DotNetZoom.GetLanguage("erase") %>' AlternateText=<%# DotNetZoom.GetLanguage("erase") %> height="16" width="16" ImageURL="~/images/1x1.gif" runat="server" CommandName="delete" OnClick="deleteForum_Click" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumID"), String) %>' style='<%# GetimgStyle("delete") %>'/>
                                                                    <asp:ImageButton  id="btnForumUp" ToolTip='<%# DotNetZoom.GetLanguage("F_Up") %>' AlternateText='<%# DotNetZoom.GetLanguage("F_Up") %>' ImageURL="~/images/up.gif" runat="server" CommandName="up" OnClick="ForumUp_Click" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                                                    <asp:ImageButton  id="btnForumDown" ToolTip='<%# DotNetZoom.GetLanguage("F_Down") %>' AlternateText='<%# DotNetZoom.GetLanguage("F_Down") %>' ImageURL="~/images/dn.gif" runat="server" CommandName="down" OnClick="ForumDown_Click" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                                                    &nbsp; 
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <table cellspacing="0" cellpadding="0" border="0" runat="server" width="100%" id="tblForumAdd">
                                                            <tr>
                                                                <td height="28">
                                                                    <table id="Table4" cellpadding="1" cellspacing="0" border="0" runat="server" width="100%">
                                                                        <tr>
                                                                            <td class="TTTAltHeader" align="left" valign="top" width="81%" style="white-space: nowrap;">
                                                                                <span class="TTTSubHeaderText" runat="server" id="Span1"><%# DotNetZoom.GetLanguage("F_AddNewForum") %>&nbsp;</span> 
                                                                                <asp:textbox id="txtNewForum" runat="server" width="81%" cssclass="NormalTextBox"></asp:textbox>
                                                                            </td>
                                                                            <td class="TTTAltHeader" align="right" valign="top">
                                                                                <asp:ImageButton ToolTip='<%# DotNetZoom.GetLanguage("F_AddNewForum") %>' AlternateText='<%# DotNetZoom.GetLanguage("F_Add") %>' id="btnAddForum" height="16" width="16" ImageURL="~/images/1x1.gif" runat="server" CommandName="add" OnClick="AddForum_Click" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), String) %>' style='<%# GetimgStyle("add") %>'/>
                                                                                &nbsp; 
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <table id="tblGroupAdd" cellspacing="0" cellpadding="3" width="100%" border="0">
                            <tr>
                                <td class="TTTAltHeader" width="150">
                                    <%# DotNetZoom.GetLanguage("F_AddNewGroup") %>:</td>
                                <td class="TTTAltHeader" align="left">
                                    <asp:textbox id="txtNewGroupName" runat="server" width="80%" cssclass="NormalTextBox"></asp:textbox>
                                    &nbsp; 
                                    <asp:ImageButton ToolTip='<%# DotNetZoom.GetLanguage("F_AddNewGroup") %>' AlternateText='<%# DotNetZoom.GetLanguage("F_Add") %>' id="btnAddGroup" height="16" width="16" ImageURL="~/images/1x1.gif" runat="server" CommandName="add" CommandArgument='-1' style='<%# GetimgStyle("add") %>'/>
                                    &nbsp; 
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:datalist>
            </td>
        </tr>
    </tbody>
</table>