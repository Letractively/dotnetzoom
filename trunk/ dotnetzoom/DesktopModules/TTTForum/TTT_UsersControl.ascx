<%@ Control Inherits="DotNetZoom.TTT_UsersControl" codebehind="TTT_UsersControl.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ import Namespace="DotNetZoom" %>
<table cellspacing="1" cellpadding="0" width="100%" border="0" runat="server">
    <tbody>
        <tr>
            <td width="100%" height="100%">
                <asp:datagrid id="grdUsers" runat="server" AutoGenerateColumns="False" AllowPaging="True" width="100%" BorderColor="#D1D7DC" BorderWidth="1" CellPadding="3" CellSpacing="0">
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle horizontalalign="Center" height="28px" width="20px" cssclass="TTTAltHeader"></HeaderStyle>
                            <ItemStyle horizontalalign="Center" height="25px" width="20px" cssclass="TTTRow"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ImageURL="~/images/TTT/TTT_Add.gif" Visible='<%# CanEdit() %>' OnClick="UserSelect_Click" AlternateText='<%# DotNetZoom.getlanguage("F_UserSelect") %>' CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID") %>' Runat="server" BorderWidth="0" BorderStyle="none"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="UserName">
                            <HeaderStyle horizontalalign="Left" height="28px" cssclass="TTTAltHeader"></HeaderStyle>
                            <ItemStyle horizontalalign="Left" height="25px" cssclass="TTTRow"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle height="28px" cssclass="TTTAltHeader"></HeaderStyle>
                            <ItemStyle height="25px" cssclass="TTTRow"></ItemStyle>
                            <ItemTemplate>
                                <asp:HyperLink NavigateUrl='<%# FormatUserURL(DataBinder.Eval(Container.DataItem,"UserId")) %>' runat="server">
                                    <%# CType(DataBinder.Eval(Container.DataItem,"UserName"), String) %> 
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="FullName">
                            <HeaderStyle horizontalalign="Left" height="28px" width="180px" cssclass="TTTAltHeader"></HeaderStyle>
                            <ItemStyle horizontalalign="Left" height="25px" width="180px" cssclass="TTTRow"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle height="28px" width="100px" cssclass="TTTAltHeader"></HeaderStyle>
                            <ItemStyle height="25px" width="100px" cssclass="TTTRow"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblEmail" runat="server" text='<%# DisplayEmail(DataBinder.Eval(Container.DataItem, "Email")) %>'></asp:Label> 
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle height="28px" width="200px" cssclass="TTTAltHeader"></HeaderStyle>
                            <ItemStyle height="25px" cssclass="TTTRow"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblAddress" runat="server" text='<%# DisplayAddress(DataBinder.Eval(Container.DataItem, "Unit"),DataBinder.Eval(Container.DataItem, "Street"), DataBinder.Eval(Container.DataItem, "City"), DataBinder.Eval(Container.DataItem, "Region"), DataBinder.Eval(Container.DataItem, "Country"), DataBinder.Eval(Container.DataItem, "PostalCode")) %>'></asp:Label> 
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle height="28px" width="25px" cssclass="TTTAltHeader"></HeaderStyle>
                            <ItemStyle horizontalalign="Center" height="25px" cssclass="TTTRow"></ItemStyle>
                            <ItemTemplate>
                                <asp:Image ImageUrl='<%# ForumConfig.SkinImageFolder() & "TTT_s_moderate.gif" %>' visible='<%# IsModerator(CType(DataBinder.Eval(Container.DataItem, "UserID"), String)) %>' runat="server" ID="Image1"></asp:Image>
                                <asp:ImageButton id="btnAddModerator" OnClick="UserSelect_Click" AlternateText='<%# DotNetZoom.getlanguage("F_Add") %>'   ImageURL="~/images/rt.gif" runat="server" CommandName="Add" visible='<%# CanAdd(CType(DataBinder.Eval(Container.DataItem, "UserID"), String)) %>' CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "UserID"), String) %>' BorderWidth="0" BorderStyle="none" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                   <PagerStyle height="28px" position="Top" horizontalalign="Center" cssclass="TTTAltHeader"></PagerStyle>
                </asp:datagrid>
            </td>
        </tr>
    </tbody>
</table>