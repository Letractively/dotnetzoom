<%@ Control Language="vb" Inherits="DotNetZoom.UserDefinedTable" codebehind="UserDefinedTable.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
    <asp:datagrid id="grdData" runat="server" OnSortCommand="grdData_Sort" AllowSorting="True" ItemStyle-CssClass="Normal" HeaderStyle-CssClass="NormalBold" AutoGenerateColumns="False" CellPadding="4" gridlines="none">
        <Columns>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:HyperLink ImageURL="~/images/edit.gif" NavigateUrl='<%# EditURL("UserDefinedRowId",DataBinder.Eval(Container.DataItem,"UserDefinedRowId")) %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:datagrid>
    <p align="center">
        <asp:LinkButton id="cmdManage" Runat="server" Text="Manage User Defined Table" CssClass="CommandButton"></asp:LinkButton>
    </p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>