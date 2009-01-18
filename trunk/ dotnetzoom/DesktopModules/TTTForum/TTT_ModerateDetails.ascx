<%@ Control Inherits="DotNetZoom.TTT_ModerateDetails" codebehind="TTT_ModerateDetails.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="TTT" TagName="UsersControl" Src="TTT_UsersControl.ascx" %>
<%@ import Namespace="DotNetZoom" %>
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="100%">
    <tbody>
        <tr>
            <td valign="top" width="70%">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                    <tbody>
                        <tr>
                            <td width="100%" height="100%">
                                <TTT:UsersControl id="ctlUsers" runat="server" ShowModerateButton="True" ShowEditButton="False" ShowEmail="True" ShowUserName="False" ShowFullName="False" ShowUserNameLink="True" ShowAddress="False"  Type="1"></TTT:UsersControl>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td valign="top" width="30%">
                <table cellspacing="1" cellpadding="0" width="100%" border="0" runat="server">
                    <tbody>
                        <tr>
                            <td class="TTTHeader" valign="middle" height="28">
                                <span class="TTTHeaderText" runat="server"><%= DotNetZoom.getlanguage("F_Moderators") %></span> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                                    <tbody>
                                        <tr>
                                            <td class="TTTAltHeader" valign="middle" align="center" width="25" height="28">
                                            </td>
                                            <td class="TTTAltHeader" valign="middle" align="left" width="200" height="28">
                                                <span class="TTTAltHeaderText" runat="server"><%= DotNetZoom.getlanguage("F_UserName") %></span> 
                                            </td>
                                            <td class="TTTAltHeader" valign="middle" align="center" width="25" height="28">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td runat="Server">
                                <asp:datalist id="lstModerators" runat="server" SelectedIndex="-1" DataKeyField="UserID" Width="100%" BorderColor="#D1D7DC" BorderWidth="1" GridLines="both" CellPadding="0" CellSpacing="0">
                                    <ItemTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" runat="server">
                                            <tr>
                                                <td class="TTTRow" width="25" height="25" align="center">
                                                    <asp:ImageButton id="btnRemoveUser" ToolTip='<%# DotNetZoom.getlanguage("F_Remove") %>' AlternateText='<%# DotNetZoom.getlanguage("F_Remove") %>' OnClick="RemoveUser_Click" ImageURL="~/images/lt.gif" runat="server" CommandName="Remove" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "UserID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                                </td>
                                                <td class="TTTRow" width="200" height="25">
                                                    <asp:HyperLink NavigateUrl='<%# FormatUserURL(DataBinder.Eval(Container.DataItem,"UserId")) %>' runat="server">
                                                        <%# CType(DataBinder.Eval(Container.DataItem,"FullName"), String) %> 
                                                    </asp:HyperLink>
                                                </td>
                                                <td class="TTTRow" height="25" width="25" align="center">
                                                    <asp:ImageButton id="btnExpand" ToolTip='<%# DotNetZoom.getlanguage("F_SeeDetails") %>' AlternateText='<%# DotNetZoom.getlanguage("F_SeeDetails") %>' ImageURL="~/images/plus2.gif" runat="server" CommandName="expand" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "UserID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <SelectedItemTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" runat="server">
                                            <tr>
                                                <td class="TTTRow" width="25" height="25" align="center">
                                                    <asp:ImageButton id="Imagebutton5" ToolTip='<%# DotNetZoom.getlanguage("F_Remove") %>' AlternateText='<%# DotNetZoom.getlanguage("F_Remove") %>' OnClick="RemoveUser_Click" ImageURL="~/images/lt.gif" runat="server" CommandName="Remove" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "UserID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                                </td>
                                                <td class="TTTRow" width="200" height="25">
                                                    <asp:HyperLink NavigateUrl='<%# FormatUserURL(DataBinder.Eval(Container.DataItem,"UserId")) %>' runat="server">
                                                        <%# CType(DataBinder.Eval(Container.DataItem,"FullName"), String) %> 
                                                    </asp:HyperLink>
                                                </td>
                                                <td class="TTTRow" height="25" width="25" align="Right">
                                                    <asp:ImageButton id="btnCollapse" ToolTip='<%# DotNetZoom.getlanguage("F_Close") %>' AlternateText='<%# DotNetZoom.getlanguage("F_Close") %>' ImageURL="~/images/minus2.gif" runat="server" CommandName="collapse" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "UserID"), String) %>' BorderWidth="0" BorderStyle="none"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <table width="100%" cellpadding="0" cellspacing="1" border="0" runat="server">
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_UserAlias") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DataBinder.Eval(Container.DataItem, "Alias") %>' runat="server" id="Label3" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_Since") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DataBinder.Eval(Container.DataItem, "CreatedDate") %>' runat="server" id="Label4" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_Post") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DataBinder.Eval(Container.DataItem, "PostCount") %>' runat="server" id="Label9" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_Validated") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DataBinder.Eval(Container.DataItem, "PostsModeratedCount") %>' runat="server" id="Label5" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_ModerateNotice") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DotNetZoom.TrueFalse(DataBinder.Eval(Container.DataItem, "EmailNotification")) %>' runat="server" id="Label12" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_ModerateEMail") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DisplayEmail(DataBinder.Eval(Container.DataItem, "Email")) %>' runat="server" id="Label6" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_UserURL") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DisplayWebsite(DataBinder.Eval(Container.DataItem, "URL")) %>' runat="server" id="Label7" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_UserMSN") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DataBinder.Eval(Container.DataItem, "MSN") %>' runat="server" id="Label10" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_UserICQ") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DataBinder.Eval(Container.DataItem, "ICQ") %>' runat="server" id="Label11" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_UserAdress") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DisplayAddress(DataBinder.Eval(Container.DataItem, "Unit"),DataBinder.Eval(Container.DataItem, "Street"), DataBinder.Eval(Container.DataItem, "City"), DataBinder.Eval(Container.DataItem, "Region"), DataBinder.Eval(Container.DataItem, "Country"), DataBinder.Eval(Container.DataItem, "PostalCode")) %>' runat="server" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_UserCountry") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DataBinder.Eval(Container.DataItem, "Country") %>' runat="server" id="Label1" /> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TTTSubHeader" height="25" width="150" align="Left">
                                                                &nbsp;<%= DotNetZoom.getlanguage("F_UserTime0") %>:</td>
                                                            <td class="TTTRow" height="25" width="150" align="Left">
                                                                &nbsp; <asp:Label cssclass="TTTRow" text='<%# DataBinder.Eval(Container.DataItem, "TimeZone") %>' runat="server" id="Label8" /> 
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </SelectedItemTemplate>
                                </asp:datalist>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>