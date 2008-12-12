<%@ Control Language="vb" Inherits="DotNetZoom.Announcements" codebehind="Announcements.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server" EnableViewState="false"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server" EnableViewState="false">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:DataList id="lstAnnouncements" runat="server" EnableViewState="false" CellPadding="4">
<ItemTemplate>
<div class="ItemTitle">
<asp:HyperLink id="editLink1" title='<%# DotNetZoom.getlanguage("modifier") %>' NavigateUrl='<%# EditURL("ItemID",DataBinder.Eval(Container.DataItem,"ItemID")) %>' Visible="<%#IsEditable%>" runat="server">
<%# DataBinder.Eval(Container.DataItem,"Title") %>
</asp:HyperLink>
<%# iif( IsEditable, "", DataBinder.Eval(Container.DataItem,"Title")) %></div>
<div class="Normal"><%# DataBinder.Eval(Container.DataItem,"Description") %>&nbsp;
<asp:HyperLink Tooltip='<%# DataBinder.Eval(Container.DataItem,"Title") %>' Text='<%# DotNetZoom.GetLanguage("see_more") %>' NavigateUrl='<%# FormatURL(DataBinder.Eval(Container.DataItem,"URL"),DataBinder.Eval(Container.DataItem,"ItemID")) %>' Visible='<%# DataBinder.Eval(Container.DataItem,"URL") <> String.Empty %>' runat="server"></asp:HyperLink>
</div>
<span class="OtherTabs">(<%# FormatDate(DataBinder.Eval(Container.DataItem,"CreatedDate")) %>)</span>
</ItemTemplate>
</asp:DataList>
<br>
<asp:HyperLink id="rssLink" Visible="False" runat="server">
<img height="14" width="36" src="images/1x1.gif" Alt="*" style="border-width:0px; background: url('/images/uostrip.gif') no-repeat; background-position: 0px -445px;">
</asp:HyperLink>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>