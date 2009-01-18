<%@ Control Language="vb" Inherits="DotNetZoom.Document" codebehind="Document.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" EnableViewState="false" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" EnableViewState="false" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:datagrid id="D" runat="server" EnableViewState="false" CellPadding="4" DataKeyField="ItemID" OnEditCommand="D_Edit" AutoGenerateColumns="false" BorderWidth="0" gridlines="none"><Columns>
<asp:TemplateColumn><ItemTemplate>
<asp:HyperLink id="E" NavigateUrl='<%# EditURL("ItemID",DataBinder.Eval(Container.DataItem,"ItemID")) %>' Visible="<%# IsEditable %>" runat="server">
<asp:Image id="EI" EnableViewState="false" ImageURL="~/images/edit.gif" Visible="<%# IsEditable %>" AlternateText='<%# DotNetZoom.GetLanguage("modifier")%>' runat="server" /></asp:HyperLink></ItemTemplate></asp:TemplateColumn>
<asp:TemplateColumn HeaderStyle-CssClass="NormalBold"><ItemTemplate>
<asp:HyperLink id="DL" EnableViewState="false" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>' NavigateUrl='<%# FormatURL(DataBinder.Eval(Container.DataItem,"URL"),DataBinder.Eval(Container.DataItem,"ItemID")) %>' CssClass="Normal" Target="_self" runat="server" /></ItemTemplate></asp:TemplateColumn>
<asp:BoundColumn DataField="CreatedByUser" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
<asp:BoundColumn DataField="Category" ItemStyle-Wrap="false" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
<asp:BoundColumn DataField="CreatedDate" DataFormatString="{0:d}" ItemStyle-CssClass="Normal" ItemStyle-Wrap="false" HeaderStyle-Cssclass="NormalBold" />
<asp:TemplateColumn HeaderStyle-CssClass="NormalBold" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><span class="Normal"><%# FormatSize(DataBinder.Eval(Container.DataItem,"Size")) %></span></ItemTemplate></asp:TemplateColumn>
<asp:TemplateColumn><ItemTemplate>
<asp:LinkButton ID="LD" EnableViewState="false" Runat="server" CssClass="CommandButton" text='<%# DotNetZoom.GetLanguage("download") %>'  commandname="Edit"></asp:LinkButton></ItemTemplate></asp:TemplateColumn></Columns></asp:datagrid>
<br>
<asp:HyperLink id="rssLink" EnableViewState="false" Visible="False" runat="server">
<img height="14" width="36" src="/images/1x1.gif" Alt="rss" style="border-width:0px; background: url('/images/uostrip.gif') no-repeat; background-position: 0px -445px;">
</asp:HyperLink>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>