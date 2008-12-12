<%@ Control Language="vb" autoeventwireup="false" codebehind="TAGFileExplorer.ascx.vb" Inherits="DotNetZoom.TAGFileExplorer" targetschema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="TAG" Namespace="TAG.WebControls" Assembly="TAG.WebControls" %>
<asp:Label id="lblErr" visible="false" enableviewstate="false" forecolor="Red" runat="server"></asp:Label>
<asp:table id="tblHeader" cssclass="ExplorerHeader" runat="server" CellPadding="2" CellSpacing="1">
    <asp:TableRow>
        <asp:TableCell width="20px" HorizontalAlign="Center">
		<input type="checkbox" value='0' onclick="SelectAllCheckboxes();" id="chkAllcheckbox">
            	<asp:CheckBox visible="false" runat="server" AutoPostBack="True" ID="chkSelectAll"></asp:CheckBox>
        </asp:TableCell>
        <asp:TableCell width="20px" HorizontalAlign="Center" Text=" "></asp:TableCell>
        <asp:TableCell width="240px" HorizontalAlign="Center"><asp:LinkButton id="chkSortNamedown" CommandName="Name DESC" runat="server"><img src="<%= DotNetZoom.glbPath %>images/sortdescending.gif" height="9" width="10" border="0" Alt="desc"></asp:linkButton><%= DotNetZoom.GetLanguage("F_Header_Name") %><asp:LinkButton id="chkSortNameup" CommandName="Name ASC" runat="server"><img src="<%= DotNetZoom.glbPath %>images/sortascending.gif" height="9" width="10" border="0" Alt="asc"></asp:linkButton></asp:TableCell>
        <asp:TableCell width="50px" HorizontalAlign="Center"><asp:LinkButton id="chkSortTypedown" CommandName="Type DESC" runat="server"><img src="<%= DotNetZoom.glbPath %>images/sortdescending.gif" height="9" width="10" border="0" Alt="desc"></asp:linkButton><%= DotNetZoom.GetLanguage("F_Header_Type") %><asp:LinkButton id="chkSortTypeup" CommandName="Type ASC" runat="server"><img src="<%= DotNetZoom.glbPath %>images/sortascending.gif" height="9" width="10" border="0" Alt="asc"></asp:linkButton></asp:TableCell>
        <asp:TableCell width="60px" HorizontalAlign="Center"><asp:LinkButton id="chkSortSizedown" CommandName="RawSize DESC" runat="server"><img src="<%= DotNetZoom.glbPath %>images/sortdescending.gif" height="9" width="10" border="0" Alt="desc"></asp:linkButton><%= DotNetZoom.GetLanguage("F_Header_Size") %><asp:LinkButton id="chkSortSizeup" CommandName="RawSize ASC" runat="server"><img src="<%= DotNetZoom.glbPath %>images/sortascending.gif" height="9" width="10" border="0" Alt="asc"></asp:linkButton></asp:TableCell>
        <asp:TableCell width="95px" HorizontalAlign="Center"><%= DotNetZoom.GetLanguage("F_Header_Created") %></asp:TableCell>
        <asp:TableCell width="95px" HorizontalAlign="Center"><%= DotNetZoom.GetLanguage("F_Header_Mod") %></asp:TableCell>
    </asp:TableRow>
</asp:table>
<div id="ScrollingPanel" class="scroll">
    <asp:datagrid id="grdFileFolders" CssClass="FileFolders"  runat="server" CellPadding="2" CellSpacing="1" ShowHeader="False"  GridLines="None" AllowSorting="True" AutoGenerateColumns="False" onSortCommand="SortFileFolders">
        <SelectedItemStyle wrap="False"></SelectedItemStyle>
        <EditItemStyle wrap="False"></EditItemStyle>
        <AlternatingItemStyle wrap="False"></AlternatingItemStyle>
        <ItemStyle wrap="False"></ItemStyle>
        <HeaderStyle horizontalalign="Center"></HeaderStyle>
        <Columns>
            <tag:CheckBoxColumn  AutoPostBack="False" CommandName="CheckClick">
                <HeaderStyle width="20px"></HeaderStyle>
                <ItemStyle width="20px"></ItemStyle>
            </tag:CheckBoxColumn>
            <asp:TemplateColumn HeaderText=" ">
                <HeaderStyle width="20px"></HeaderStyle>
                <ItemStyle width="20px"></ItemStyle>
                <ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.Icon") %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Name">
                <HeaderStyle width="240px"></HeaderStyle>
                <ItemStyle width="240px"></ItemStyle>
                <ItemTemplate>
                    <asp:LinkButton id="lnkNameColumn" runat="server" CommandName="Select" ToolTip='<%# GetFullUrlName( DataBinder.Eval(Container, "DataItem.Name")) %>' Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>' CausesValidation="false"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox id="txtRename" runat="server" width="180px" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'></asp:TextBox>
                    <tag:RolloverImageButton id="imgEditOK" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Name") %>' CommandName="EditOK" width="16px" height="16px" DisabledImageURL="Images/OK_Disabled.gif" EnabledImageURL="Images/OK.gif" RolloverImageURL="Images/OK_Rollover.gif" StatusText='<%# DotNetZoom.GetLanguage("F_Rename") %>' ToolTip='<%# DotNetZoom.GetLanguage("F_Rename") %>'></tag:RolloverImageButton>
                    <tag:RolloverImageButton id="imgEditCancel" runat="server" CommandName="EditCancel" width="16px" height="16px" DisabledImageURL="Images/Delete_Disabled.gif" EnabledImageURL="Images/Delete.gif" RolloverImageURL="Images/Delete_Rollover.gif" StatusText='<%# DotNetZoom.GetLanguage("annuler") %>' ToolTip='<%# DotNetZoom.GetLanguage("annuler") %>'></tag:RolloverImageButton>
                </EditItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn SortExpression="Type" HeaderText="Type">
                <HeaderStyle width="50px"></HeaderStyle>
                <ItemStyle width="50px"></ItemStyle>
                <ItemTemplate>
                    <asp:Label runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.Type") %>'></asp:Label> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.Type") %>'></asp:Label> 
                </EditItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn SortExpression="Size" HeaderText="Size">
                <HeaderStyle width="60px"></HeaderStyle>
                <ItemStyle wrap="False" horizontalalign="Right" width="60px"></ItemStyle>
                <ItemTemplate>
                    <asp:Label runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.Size") %>'></asp:Label> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.Size") %>'></asp:Label> 
                </EditItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn SortExpression="CreateDate" HeaderText="CreateDate">
                <HeaderStyle width="95px"></HeaderStyle>
                <ItemStyle wrap="False" horizontalalign="Right" width="95px"></ItemStyle>
                <ItemTemplate>
                    <asp:Label runat="server"  text='<%# String.Format("{0:yyyy/MM/dd hh:mm tt}", DataBinder.Eval(Container, "DataItem.CreateDate")) %>' ></asp:Label> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label runat="server"  text='<%# String.Format("{0:yyyy/MM/dd hh:mm tt}", DataBinder.Eval(Container, "DataItem.CreateDate")) %>' ></asp:Label> 
                </EditItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn SortExpression="ModifyDate" HeaderText="ModifyDate">
                <HeaderStyle width="95px"></HeaderStyle>
                <ItemStyle wrap="False" horizontalalign="Right" width="95px"></ItemStyle>
                <ItemTemplate>
                    <asp:Label runat="server"  text='<%# String.Format("{0:yyyy/MM/dd hh:mm tt}", DataBinder.Eval(Container, "DataItem.ModifyDate")) %>' ></asp:Label> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label runat="server"  text='<%# String.Format("{0:yyyy/MM/dd hh:mm tt}", DataBinder.Eval(Container, "DataItem.ModifyDate")) %>' ></asp:Label> 
                </EditItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:datagrid>
</div>