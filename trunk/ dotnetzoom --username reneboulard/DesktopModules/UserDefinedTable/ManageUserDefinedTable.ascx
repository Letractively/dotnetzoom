<%@ Control Language="vb" codebehind="ManageUserDefinedTable.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.ManageUserDefinedTable" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:datagrid id="grdFields" runat="server" DataKeyField="UserDefinedFieldId" gridlines="none" BorderStyle="None" CellPadding="2" CellSpacing="0" AutoGenerateColumns="False" OnItemDataBound="grdFields_Item_Bound" OnItemCommand="grdFields_Move" OnDeleteCommand="grdFields_Delete" OnEditCommand="grdFields_Edit" OnUpdateCommand="grdFields_Update" OnCancelCommand="grdFields_CancelEdit" CssClass="Normal">
    <Columns>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:imagebutton id="cmdEditUserDefinedField" runat="server" causesvalidation="false" commandname="Edit" ImageUrl="~/images/edit.gif" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' ToolTip='<%# DotNetZoom.GetLanguage("modifier") %>' BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                <asp:imagebutton id="cmdDeleteUserDefinedField" runat="server" causesvalidation="false" commandname="Delete" ImageUrl="~/images/delete.gif" AlternateText='<%# DotNetZoom.GetLanguage("erase") %>' ToolTip='<%# DotNetZoom.GetLanguage("erase") %>' BorderWidth="0" BorderStyle="none"></asp:imagebutton>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:imagebutton id="cmdSaveUserDefinedField" runat="server" causesvalidation="false" commandname="Update" ImageUrl="~/images/save.gif" AlternateText='<%# DotNetZoom.GetLanguage("enregistrer") %>' ToolTip='<%# DotNetZoom.GetLanguage("enregistrer") %>' BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                <asp:imagebutton id="cmdCancelUserDefinedField" runat="server" causesvalidation="false" commandname="Cancel" ImageUrl="~/images/cancel.gif" AlternateText='<%# DotNetZoom.GetLanguage("annuler") %>' ToolTip='<%# DotNetZoom.GetLanguage("annuler") %>' BorderWidth="0" BorderStyle="none"></asp:imagebutton>
            </EditItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Image runat="server" ImageUrl='<%# IIf(DataBinder.Eval(Container.DataItem, "Visible") = True, "~/images/checked.gif", "~/images/unchecked.gif") %>' ID="Image2" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label id="lblCheckBox2" runat="server" />
                <asp:CheckBox runat="server" id="Checkbox2" Checked='<%# IIf(DataBinder.Eval(Container.DataItem, "Visible") = True, "True", "False") %>' />
            </EditItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
            <ItemTemplate>
                <asp:Label runat="server" text='<%# DataBinder.Eval(Container.DataItem, "FieldTitle") %>' id="Label1" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label id="lblFieldTitle" runat="server" />
                <asp:TextBox runat="server" id="txtFieldTitle" Columns="30" MaxLength="50" Text='<%# DataBinder.Eval(Container.DataItem, "FieldTitle") %>' />
            </EditItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn  ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
            <ItemTemplate>
                <asp:Label runat="server" text='<%# GetFieldTypeName(DataBinder.Eval(Container.DataItem, "FieldType")) %>' id="Label2" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label id="lblFieldType" runat="server" />
                <asp:DropDownList ID="cboFieldType" Runat="server" CssClass="NormalTextBox" SelectedIndex='<%# GetFieldTypeIndex(DataBinder.Eval(Container.DataItem, "FieldType")) %>'>
                    <asp:ListItem Value="String"></asp:ListItem>
                    <asp:ListItem Value="Int32"></asp:ListItem>
                    <asp:ListItem Value="Decimal"></asp:ListItem>
                    <asp:ListItem Value="DateTime"></asp:ListItem>
                    <asp:ListItem Value="Boolean"></asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:imagebutton id="cmdMoveUserDefinedFieldUp" runat="server" causesvalidation="false" commandname="Item" CommandArgument="Up" ImageUrl="~/images/up.gif" AlternateText='<%# DotNetZoom.GetLanguage("Move_Field_Up") %>' ToolTip='<%# DotNetZoom.GetLanguage("Move_Field_Up") %>'></asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:imagebutton id="cmdMoveUserDefinedFieldDown" runat="server" causesvalidation="false" commandname="Item" CommandArgument="Down" ImageUrl="~/images/dn.gif" AlternateText='<%# DotNetZoom.GetLanguage("Move_Field_Down") %>' ToolTip='<%# DotNetZoom.GetLanguage("Move_Field_Down") %>' BorderWidth="0" BorderStyle="none"></asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:datagrid>
<br>
<span class="SubHead"><%= dotNetZoom.GetLanguage("OrderBy") %>:</span>&nbsp;<asp:DropDownList id="cboSortField" CssClass="NormalTextBox" AutoPostBack="True" DataValueField="UserDefinedFieldId" DataTextField="FieldTitle" Runat="server"></asp:DropDownList>
&nbsp;<asp:DropDownList id="cboSortOrder" CssClass="NormalTextBox" AutoPostBack="True" Runat="server">
    <asp:ListItem Value=""></asp:ListItem>
    <asp:ListItem Value="ASC"></asp:ListItem>
    <asp:ListItem Value="DESC"></asp:ListItem>
    </asp:DropDownList>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdAddField" runat="server"  CausesValidation="False"></asp:LinkButton>
    &nbsp;
    <asp:LinkButton class="CommandButton" id="cmdCancel" runat="server"  CausesValidation="False"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>