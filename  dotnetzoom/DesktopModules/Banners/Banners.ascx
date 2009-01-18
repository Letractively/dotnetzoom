<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Control language="vb" Inherits="DotNetZoom.Banners" CodeBehind="Banners.ascx.vb" AutoEventWireup="false" Explicit="True" %>
<portal:title runat="server" EnableViewState="false" id="Title1" />
<asp:PlaceHolder ID="pnlModuleContent" Runat="server" EnableViewState="false" >
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:DataList repeatdirection="Vertical" id=lstBanners runat="server" EnableViewState="false" CellPadding="4">
<ItemStyle VerticalAlign="Middle" HorizontalAlign="Center"></ItemStyle>
<ItemTemplate>
<asp:HyperLink id="hypBanner" Runat="server" EnableViewState="false" NavigateUrl='<%# DotNetZoom.glbPath & "DesktopModules/Banners/BannerClickThrough.aspx?BannerId=" & DataBinder.Eval(Container.DataItem,"BannerId") %>'>
<asp:Image id="hypBannerImage" Runat="server" EnableViewState="false" ToolTip=' <%# DataBinder.Eval(Container.DataItem,"BannerName") %> ' AlternateText='<%# DataBinder.Eval(Container.DataItem,"BannerName") %>' ImageUrl='<%# FormatImagePath(DataBinder.Eval(Container.DataItem,"ImageFile")) %>'/>
</asp:HyperLink>
</ItemTemplate>
</asp:DataList>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>