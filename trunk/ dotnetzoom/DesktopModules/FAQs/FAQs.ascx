<%@ Control Inherits="DotNetZoom.FAQs" CodeBehind="FAQs.ascx.vb" language="vb" AutoEventWireup="false" Explicit="True" %>
<%@ Register Assembly="dotnetzoom" Namespace="DotNetZoom" TagPrefix="cc1" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<portal:title runat="server" EnableViewState="false" ID="Title1" />
<asp:PlaceHolder ID="pnlModuleContent" Runat="server" EnableViewState="false">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<ASP:DataList id="lstFAQs" DataKeyField="ItemID" runat="server" EnableViewState="false" CellPadding="0">
<ItemTemplate>
<table><tr><td valign="top">
<asp:HyperLink NavigateUrl='<%# EditURL("ItemID",DataBinder.Eval(Container.DataItem,"ItemID")) %>' Visible="<%# IsEditable %>" runat="server" EnableViewState="false" ID="Hyperlink2"><asp:Image ID=Image1 Runat=server  ImageURL="~/images/edit.gif" AlternateText="edit" Visible="<%#IsEditable%>"/></asp:HyperLink>
</td><td>
<cc1:OpenClose ID="O" Show="false" What='<%# HtmlDecode(DataBinder.Eval(Container.DataItem, "question")) %>' runat="server">
<%# HtmlDecode(DataBinder.Eval(Container.DataItem, "answer")) %>
</cc1:OpenClose>
</td></tr></table>
</ItemTemplate>
</ASP:DataList>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>