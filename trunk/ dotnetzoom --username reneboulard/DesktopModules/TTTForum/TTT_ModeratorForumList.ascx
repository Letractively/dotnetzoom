<%@ Control Inherits="DotNetZoom.TTT_ModeratorForumList" codebehind="TTT_ModeratorForumList.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ import Namespace="DotNetZoom" %>
<asp:literal id="message" runat="server"></asp:literal>
<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
    <tbody>
        <tr>
            <td class="TTTRowHighLight" width="100%" height="100%">
                <asp:datagrid id="grdForums" runat="server" BorderWidth="1" BorderColor="#D1D7DC" CellSpacing="0" CellPadding="1" width="100%" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundColumn DataField="ForumName">
                            <HeaderStyle cssclass="TTTAltHeader" height="28" width="320" horizontalalign="Left"></HeaderStyle>
                            <ItemStyle cssclass="TTTRow" height="25" horizontalalign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CreatedDate">
                            <HeaderStyle cssclass="TTTAltHeader" height="28" width="120" horizontalalign="Center"></HeaderStyle>
                            <ItemStyle cssclass="TTTRow" height="25" horizontalalign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle cssclass="TTTAltHeader" height="28" width="120" horizontalalign="Center"></HeaderStyle>
                            <ItemStyle cssclass="TTTRow" height="25" horizontalalign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:CheckBox id="Email" Checked='<%# Ctype(DataBinder.Eval(Container.DataItem, "EmailNotification"), Boolean) %>' Runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn Visible="false" DataField="ForumID">
                            <HeaderStyle cssclass="TTTAltHeader" height="28" width="120" horizontalalign="Center"></HeaderStyle>
                            <ItemStyle cssclass="TTTRow" height="25" horizontalalign="Center"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle horizontalalign="Center" height="28" position="Bottom" cssclass="TTTFooter"></PagerStyle>
                </asp:datagrid>
            </td>
        </tr>
        <tr>
            <td class="TTTAltHeader" align="right" height="28">
                <asp:button cssclass="button" id="btnSave" runat="server"  CommandName="Save"></asp:button>
                &nbsp; 
            </td>
        </tr>
    </tbody>
</table>