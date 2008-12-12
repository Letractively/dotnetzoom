<%@ Control Inherits="DotNetZoom.FAQs" CodeBehind="FAQs.ascx.vb" language="vb" AutoEventWireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<portal:title runat="server" EnableViewState="false" ID="Title1" />
<asp:PlaceHolder ID="pnlModuleContent" Runat="server" EnableViewState="false">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<ASP:DataList id="lstFAQs" DataKeyField="ItemID" runat="server" EnableViewState="false" CellPadding="4">
<ItemTemplate>
<asp:HyperLink NavigateUrl='<%# EditURL("ItemID",DataBinder.Eval(Container.DataItem,"ItemID")) %>' Visible="<%# IsEditable %>" runat="server" EnableViewState="false" ID="Hyperlink1"><asp:Image ID=Hyperlink1Image Runat=server  ImageUrl="~/images/edit.gif" AlternateText="edit" Visible="<%#IsEditable%>"/></asp:HyperLink>
<a href="javascript:onclick=ShowHide('<%# lstFAQs.ClientID & "_" & DataBinder.Eval(Container.DataItem,"ItemID") %>','img_<%# lstFAQs.ClientID & "_" & DataBinder.Eval(Container.DataItem,"ItemID") %>');">
<img alt="+" id="img_<%# lstFAQs.ClientID & "_" & DataBinder.Eval(Container.DataItem,"ItemID") %>" src="images/1x1.gif" height="12" width="12" style="border-width:0px; background: url('/images/uostrip.gif') no-repeat; background-position: 0px -407px;">
</a>
<a href="javascript:onclick=ShowHide('<%# lstFAQs.ClientID & "_" & DataBinder.Eval(Container.DataItem,"ItemID") %>','img_<%# lstFAQs.ClientID & "_" & DataBinder.Eval(Container.DataItem,"ItemID") %>');">
<%# HtmlDecode(DataBinder.Eval(Container.DataItem, "question")) %>
</a>
<div style="display: none; padding: 5px 0px 5px 15px;" id="<%# lstFAQs.ClientID & "_" & DataBinder.Eval(Container.DataItem,"ItemID") %>">
<%# HtmlDecode(DataBinder.Eval(Container.DataItem, "answer")) %>
</div>
</ItemTemplate>
</ASP:DataList>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>