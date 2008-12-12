<%@ Control Language="vb" Inherits="DotNetZoom.Contacts" codebehind="Contacts.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:datagrid id="grdContacts" runat="server" gridlines="none" AutoGenerateColumns="false" EnableViewState="false" CellPadding="4">
<Columns>
<asp:TemplateColumn>
<ItemTemplate>
<asp:HyperLink NavigateUrl='<%# EditURL("ItemID",DataBinder.Eval(Container.DataItem,"ItemID")) %>' Visible="<%# IsEditable %>" runat="server">
<asp:Image ImageUrl="~/images/edit.gif" AlterNateText="*" BorderWidth="0" ID="Image1" runat="server" />
</asp:HyperLink>
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="Name" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
<asp:BoundColumn DataField="Role" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
<asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
<ItemTemplate>
<asp:Label id="lblEmail" runat="server" text='<%# DisplayEmail(DataBinder.Eval(Container.DataItem, "Email")) %>'></asp:Label>
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="Contact1" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
<asp:BoundColumn DataField="Contact2" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
</Columns>
</asp:datagrid>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>