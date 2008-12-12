<%@ Control Language="vb" codebehind="TTT_ForumModerate.ascx.vb" autoeventwireup="false" Inherits="DotNetZoom.TTT_ForumModerate" %>
<%@ import Namespace="DotNetZoom" %>
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="100%">
    <tbody>
        <tr>
            <td class="TTTHeader" width="100%" height="28">
                <asp:Label id="Label1" cssclass="TTTHeaderText" runat="server">&nbsp;<%= DotNetZoom.GetLanguage("F_ModerateForum") %></asp:Label></td>
        </tr>
        <tr>
            <td width="100%">
                <table cellspacing="1" cellpadding="0" width="100%" border="0" runat="server">
                    <tbody>
                        <tr>
                            <td class="TTTAltHeader" style="white-space: nowrap;" valign="middle" width="30" height="28" runat="server">
                            </td>
                            <td class="TTTAltHeader" style="white-space: nowrap;" valign="middle" width="400" height="28" runat="server">
                                <%= DotNetZoom.GetLanguage("F_Object") %> 
                            </td>
                            <td class="TTTAltHeader" style="white-space: nowrap;" valign="middle" width="120" height="28" runat="server">
                                <%= DotNetZoom.GetLanguage("F_Auteur") %>
                            </td>
                            <td class="TTTAltHeader" style="white-space: nowrap;" valign="middle" width="180" height="28" runat="server">
                                <%= DotNetZoom.GetLanguage("F_Created") %> 
                            </td>
                            <td class="TTTAltHeader" style="white-space: nowrap;" valign="middle" width="30" height="28" runat="server">
                            </td>
                            <td class="TTTAltHeader" style="white-space: nowrap;" valign="middle" width="30" height="28" runat="server">
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:datalist id="lstPost" runat="server" DataKeyField="PostID" CellPadding="0" Width="100%">
                    <ItemTemplate>
                        <table width="100%" cellpadding="0" cellspacing="1" border="0" runat="server">
                            <tr>
                                <td class="TTTRow" style="white-space: nowrap;" width="30" height="25" align="center">
                                    <asp:ImageButton ToolTip='<%# DotNetZoom.getlanguage("F_Close") %>' AlternateText='<%# DotNetZoom.getlanguage("F_Close") %>' id="btnCollapse" ImageUrl="~/images/minus2.gif" runat="server" CommandName="collapse" Visible="False" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "PostID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                    <asp:ImageButton ToolTip='<%# DotNetZoom.getlanguage("F_Expand") %>' AlternateText='<%# DotNetZoom.getlanguage("F_Expand") %>'  id="btnExpand" ImageUrl="~/images/plus2.gif" runat="server" CommandName="expand" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "PostID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                </td>
                                <td class="TTTRow" style="white-space: nowrap;" width="400" height="25" align="Left">
                                    <asp:Label cssclass="TTTNormal" text='<%# DataBinder.Eval(Container.DataItem, "Subject") %>' runat="server" id="Label1" /> 
                                </td>
                                <td class="TTTRowHighlight" style="white-space: nowrap;" width="120" height="25" align="center">
                                    <asp:Label cssclass="TTTNormal" width="100%" text='<%# CType(Container.DataItem, ForumPostModerate).User.Alias & " (messages: " & CType(Container.DataItem, ForumPostModerate).User.PostCount & ")" %>' runat="server" id="Label2" /> 
                                </td>
                                <td class="TTTRowHighlight" style="white-space: nowrap;" width="180" height="25" align="center">
                                    <asp:Label cssclass="TTTNormal" width="100%" text='<%# DataBinder.Eval(Container.DataItem, "PostDate") %>' runat="server" id="Label3" /> 
                                </td>
                                <td class="TTTRowHighlight" style="white-space: nowrap;" width="30" height="25" align="center">
                                    <asp:CheckBox Runat="server" EnableViewState="False" ID="chkApprove"></asp:CheckBox>
                                </td>
                                <td class="TTTRowHighlight" style="white-space: nowrap;" width="30" height="25" align="center">
                                    <asp:ImageButton id="btnEdit" ToolTip='<%# DotNetZoom.getlanguage("F_Audit") %>' AlternateText='<%# DotNetZoom.getlanguage("F_Audit") %>' height="16" width="16" ImageUrl="~/images/1x1.gif" runat="server" CommandName="Edit" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "PostID"), String) %>' style='<%# GetEditStyle() %>'  />
                                </td>
                            </tr>
                        </table>
                        <asp:placeholder ID="pnlBody" Runat="server" Visible="False">
                            <table width="100%" cellpadding="0" cellspacing="1" border="0" runat="server">
                                <tr>
                                    <td class="TTTNormal" style="white-space: nowrap;" width="30" height="25" align="center"></td>
                                    <td class="TTTNormal">
                                        <asp:Label cssclass="TTTNormal" text='<%# FormatBody(DataBinder.Eval(Container.DataItem,"Body")) %>' runat="server" id="Label4" /> 
                                    </td>
                                </tr>
                            </table>
                        </asp:placeholder>
                    </ItemTemplate>
                </asp:datalist>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                    <tbody>
                        <tr>
                            <td class="TTTAltHeader" style="white-space: nowrap;" align="left" width="50%" height="28" runat="server">
                                &nbsp; 
                                <asp:button id="cmdBack" cssclass="button" runat="server" CommandName="Back"></asp:button>
                                &nbsp; 
                            </td>
                            <td class="TTTAltHeader" style="white-space: nowrap;" align="right" width="100%" height="28" runat="server">
                                <asp:button id="cmdDelete" cssclass="button" runat="server" CommandName="Delete"></asp:button>
                                &nbsp; 
                                <asp:button id="cmdApprove" cssclass="button" runat="server" CommandName="Approve"></asp:button>
                                &nbsp; 
                                <asp:button id="cmdReject" cssclass="button" runat="server" CommandName="Reject"></asp:button>
                                &nbsp; 
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <asp:placeholder id="pnlAudit" Runat="server" Visible="False">
            <tr>
                <td>
                    <table cellspacing="1" cellpadding="3" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="25">
                                    <%= DotNetZoom.GetLanguage("F_ArticleID") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:Label id="lblID" cssclass="TTTNormal" runat="server" width="340px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="25">
                                    <%= DotNetZoom.GetLanguage("F_Auteur") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:Label id="lblAuthor" cssclass="TTTNormal" runat="server" width="340px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="25">
                                    <%= DotNetZoom.GetLanguage("F_Object") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:Label id="lblSubject" cssclass="TTTNormal" runat="server" width="340px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="25">
                                    <%= DotNetZoom.GetLanguage("F_Message") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:Label id="lblMessage" cssclass="TTTNormal" runat="server" width="340px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="25">
                                    <asp:DropDownList class="NormalTextBox" id="ddlActions" runat="server" Width="100%">
                                        <asp:ListItem Value="None"></asp:ListItem>
                                        <asp:ListItem Value="Approve"></asp:ListItem>
                                        <asp:ListItem Value="Reject"></asp:ListItem>
                                        <asp:ListItem Value="Delete"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="TTTRow" valign="top" align="left" rowspan="2">
                                    <asp:textbox id="txtNotes" cssclass="NormalTextBox" runat="server" width="100%" Columns="26" TextMode="MultiLine" Height="50"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="25">
                                    &nbsp; 
                                    <asp:button id="btnUpdate" cssclass="button" runat="server" CommandName="back" ></asp:button>
                                    &nbsp; 
                                    <asp:button id="btnClose" cssclass="button" runat="server" CommandName="back" ></asp:button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </asp:placeholder>
    </tbody>
</table>