<%@ Control Language="vb" autoeventwireup="false" EnableViewState=False Explicit="True" codebehind="Search.ascx.vb" Inherits="DotNetZoom.Search" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="4" border="0">
	<tbody>
		<tr valign="top">
		<td style="white-space: nowrap;" valign="middle">
		<label class="SubHead" for="<%=txtSearch.ClientID%>"><%= DotNetZoom.getlanguage("search_search") %>:</label>&nbsp;
		<asp:TextBox id="txtSearch" runat="server" Width="150px" columns="35" maxlength="100" cssclass="NormalTextBox"></asp:TextBox>
		<asp:LinkButton id="cmdGo" Runat="server" CssClass="CommandButton"></asp:LinkButton>
		</td>
		</tr>
		<tr valign="top">
		<td>
		<asp:DataList id="lstResults" runat="server" Width="100%" EnableViewState="false" CellPadding="4">
			<ItemTemplate><asp:Label id="lblResult" runat="server"> <%# FormatResult(DataBinder.Eval(Container.DataItem, "ModuleId"), DataBinder.Eval(Container.DataItem,"TabId"),DataBinder.Eval(Container.DataItem,"TabName"),DataBinder.Eval(Container.DataItem,"ModuleTitle"),DataBinder.Eval(Container.DataItem,"TitleField"),DataBinder.Eval(Container.DataItem,"DescriptionField"),DataBinder.Eval(Container.DataItem,"CreatedDateField"),DataBinder.Eval(Container.DataItem,"CreatedByUserField")) %> </asp:Label>
			</ItemTemplate>
		</asp:DataList>
		</td>
		</tr>
	</tbody>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>