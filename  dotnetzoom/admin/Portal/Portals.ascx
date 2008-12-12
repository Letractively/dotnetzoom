<%@ Control Language="vb" codebehind="Portals.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.Portals" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" EditURL="~/default.aspx?edit=control&def=Signup&hostpage=" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>

<asp:datagrid id="grdPortals" runat="server" gridlines="none" BorderStyle="None" CellPadding="4" CellSpacing="0" AutoGenerateColumns="false" EnableViewState="false" Width="100%">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="20">
            <ItemTemplate>
                <asp:HyperLink NavigateUrl='<%# EditURL("PortalID",DataBinder.Eval(Container.DataItem,"PortalID")) %>' runat="server" ID="Hyperlink1">
                    <asp:Image ImageUrl="~/images/edit.gif" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' ToolTip='<%# DotNetZoom.GetLanguage("modifier") %>' runat="server" ID="Hyperlink1Image" />
                </asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
            <ItemTemplate>
                <asp:Label id="lblPortal" runat="server" text='<%# FormatPortal(DataBinder.Eval(Container.DataItem, "PortalName"),DataBinder.Eval(Container.DataItem, "PortalAlias")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn  DataField="PortalAlias" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn  DataField="Users" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
        <asp:BoundColumn  DataField="HostSpace" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn  DataField="HostFee" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" DataFormatString="{0:0.00}" />
        <asp:BoundColumn  DataField="ExpiryDate" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" DataFormatString="{0:d}" />
    </Columns>
</asp:datagrid>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>