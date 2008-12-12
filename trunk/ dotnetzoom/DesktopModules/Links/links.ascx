<%@ Control Language="vb" Inherits="DotNetZoom.Links" codebehind="Links.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %><portal:title id="Title1" DisplayOptions="True" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" EnableViewState="false" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:placeholder id="pnlList" Runat="server" EnableViewState="false"  Visible="False">
<asp:DataList id="lstLinks" runat="server" EnableViewState="false" ItemStyle-VerticalAlign="Top" ItemStyle-CssClass='<%# GetCLASS() %>'  CellPadding="4">
<ItemTemplate>
<asp:HyperLink EnableViewState="false" id="editLink" NavigateUrl='<%# EditURL("ItemID",DataBinder.Eval(Container.DataItem,"ItemID")) %>' Visible="<%# IsEditable %>" runat="server"  ><asp:Image id="editLinkImage" ImageUrl="~/images/edit.gif" AlternateText="Modifier" Visible="<%# IsEditable %>" Runat="server" EnableViewState="false" /></asp:HyperLink>
<asp:HyperLink EnableViewState="false" onmouseover='<%# DotNetZoom.ReturnToolTip(DataBinder.Eval(Container.DataItem,"Description")) %>'  Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>' NavigateUrl='<%# FormatURL(DataBinder.Eval(Container.DataItem,"Url"),DataBinder.Eval(Container.DataItem,"ItemID")) %>' Target= '<%# IIF(DataBinder.Eval(Container.DataItem,"NewWindow"),"_blank","_self") %>' runat="server" />
</ItemTemplate>
</asp:DataList>
</asp:placeholder>
<asp:placeholder id="pnlDropdown" Runat="server" EnableViewState="false" Visible="False">
<table cellspacing="0" cellpadding="4" border="0">
<tbody>
<tr>
<td>
<asp:ImageButton id="cmdEdit" Runat="server"  EnableViewState="false" ImageUrl="~/images/edit.gif" BorderWidth="0" BorderStyle="none"></asp:ImageButton>Hyperlien 
<asp:DropDownList id="cboLinks" Runat="server" EnableViewState="false" DataTextField="Title" DataValueField="ItemID" CssClass="NormalTextBox"></asp:DropDownList>&nbsp;
<asp:LinkButton id="cmdGo" Runat="server" EnableViewState="false" CssClass="CommandButton"></asp:LinkButton>&nbsp;
<asp:LinkButton id="cmdInfo" Runat="server" EnableViewState="false" CssClass="CommandButton"></asp:LinkButton>
</td>
</tr>
<tr>
<td>
<asp:Label id="lblDescription" runat="server" EnableViewState="false" cssclass="Normal"></asp:Label>
</td>
</tr>
</tbody>
</table></asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>