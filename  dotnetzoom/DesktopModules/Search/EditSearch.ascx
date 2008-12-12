<%@ Control Language="vb" codebehind="EditSearch.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditSearch" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr>
            <td class="SubHead">
                <label for="<%=txtResults.ClientID%>"><%= DotNetZoom.getlanguage("Search_max_result") %>:</label></td>
            <td class="NormalTextBox">
                <asp:textbox id="txtResults" runat="server" MaxLength="5" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtTitle.ClientID%>"><%= DotNetZoom.getlanguage("Search_max_width") %>:</label></td>
            <td class="NormalTextBox">
                <asp:textbox id="txtTitle" runat="server" MaxLength="5" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtDescription.ClientID%>"><%= DotNetZoom.getlanguage("Search_max_width_desc") %>:</label></td>
            <td class="NormalTextBox">
                <asp:textbox id="txtDescription" runat="server" MaxLength="5" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=chkDescription.ClientID%>"><%= DotNetZoom.getlanguage("Search_show_desc") %></label></td>
            <td class="NormalTextBox">
                <asp:checkbox id="chkDescription" runat="server" CssClass="NormalTextBox"></asp:checkbox>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=chkAudit.ClientID%>"><%= DotNetZoom.getlanguage("Search_show_audit") %></label></td>
            <td class="NormalTextBox">
                <asp:checkbox id="chkAudit" runat="server" CssClass="NormalTextBox"></asp:checkbox>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <p>
                    <label for="<%=chkBreadcrumbs.ClientID%>"><%= DotNetZoom.getlanguage("Search_show_breadcrum") %></label>
                </p>
            </td>
            <td class="NormalTextBox">
                <asp:checkbox id="chkBreadcrumbs" runat="server" CssClass="NormalTextBox"></asp:checkbox>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" runat="server" ></asp:LinkButton>
    &nbsp;
    <asp:LinkButton class="CommandButton" id="cmdCancel" runat="server"  CausesValidation="False"></asp:LinkButton>
</p>
<hr />
<br>
<table cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr>
            <td valign="bottom">
                <span class="SubHead"><label for="<%=cboTables.ClientID%>"><%= DotNetZoom.getlanguage("Search_sql_table") %>:</label></span> &nbsp;
                <asp:DropDownList id="cboTables" CssClass="NormalTextBox" DataValueField="FKTABLE_NAME" DataTextField="FKTABLE_NAME" Runat="server"></asp:DropDownList>
                &nbsp;
                <asp:LinkButton id="cmdAdd" CssClass="CommandButton" Runat="server"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="middle">
                <asp:datagrid id="grdCriteria" runat="server" CssClass="Normal" EnableViewState="True" OnCancelCommand="grdCriteria_CancelEdit" OnUpdateCommand="grdCriteria_Update" OnDeleteCommand="grdCriteria_Delete" OnEditCommand="grdCriteria_Edit" AutoGenerateColumns="False" CellSpacing="4" CellPadding="4" gridlines="none" BorderStyle="None" DataKeyField="SearchID">
                    <Columns>
                        <asp:TemplateColumn ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <asp:imagebutton id="cmdEditCriteria" runat="server" causesvalidation="false" commandname="Edit" ImageUrl="~/images/edit.gif" AlternateText="Modifier" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                <asp:ImageButton ID="cmdDeleteCriteria" Runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Supprimer" BorderWidth="0" BorderStyle="none"></asp:ImageButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:imagebutton id="cmdSaveCriteria" runat="server" causesvalidation="false" commandname="Update" ImageUrl="~/images/save.gif" AlternateText="Enregistrer" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                <asp:imagebutton id="cmdCancelCriteria" runat="server" causesvalidation="false" commandname="Cancel" ImageUrl="~/images/cancel.gif" AlternateText="Annuler" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="TableName" ReadOnly="True" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                        <asp:TemplateColumn itemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
                            <ItemTemplate>
                                <asp:Label runat="server" text='<%# DataBinder.Eval(Container.DataItem, "TitleField") %>' id="Label1" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="cboTitleField" Runat="server" CssClass="NormalTextBox" DataSource='<%# arrFields %>' SelectedIndex='<%# SelectField(DataBinder.Eval(Container.DataItem, "TitleField")) %>'></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
                            <ItemTemplate>
                                <asp:Label runat="server" text='<%# DataBinder.Eval(Container.DataItem, "DescriptionField") %>' id="Label2" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="cboDescriptionField" Runat="server" CssClass="NormalTextBox" DataSource='<%# arrFields %>' SelectedIndex='<%# SelectField(DataBinder.Eval(Container.DataItem, "DescriptionField")) %>'></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
                            <ItemTemplate>
                                <asp:Label runat="server" text='<%# DataBinder.Eval(Container.DataItem, "CreatedDateField") %>' id="Label3" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="cboCreatedDateField" Runat="server" CssClass="NormalTextBox" DataSource='<%# arrFields %>' SelectedIndex='<%# SelectField(DataBinder.Eval(Container.DataItem, "CreatedDateField")) %>'></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
                            <ItemTemplate>
                                <asp:Label runat="server" text='<%# DataBinder.Eval(Container.DataItem, "CreatedByUserField") %>' id="Label4" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="cboCreatedByUserField" Runat="server" CssClass="NormalTextBox" DataSource='<%# arrFields %>' SelectedIndex='<%# SelectField(DataBinder.Eval(Container.DataItem, "CreatedByUserField")) %>'></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:datagrid>
            </td>
        </tr>
    </tbody>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>