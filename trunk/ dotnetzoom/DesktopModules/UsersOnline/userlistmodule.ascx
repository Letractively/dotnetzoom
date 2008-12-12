<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="UserListModule.ascx.vb" Inherits="DotNetZoom.UserListModule" TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<portal:title id=Title1 runat="server"></portal:title>
<asp:PlaceHolder id=pnlModuleContent Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing=0 cellpadding=4 width='100%'>
  <TBODY>
  <tr class=Normal>
    <td>
      <table 
      style="BORDER-RIGHT: #003366 1px solid; BORDER-TOP: #003366 1px solid; BORDER-LEFT: #003366 1px solid; BORDER-BOTTOM: #003366 1px solid" 
      cellspacing=2 cellpadding=4 width="100%" border=0>
        <tr>
          <th align=left><%= DotNetZoom.GetLanguage("UO_SearchOptions")%></th></tr>
        <tr class="Normal">
          <td style="white-space: nowrap;"><%= DotNetZoom.GetLanguage("UO_See")%>&nbsp;<asp:DropDownList id=drpGroupFilter Runat="server" CssClass="Normal">
				<asp:ListItem Value="All Users"></asp:ListItem>
				<asp:ListItem Value="Online Users Only"></asp:ListItem>
			</asp:DropDownList>&nbsp;<%= DotNetZoom.GetLanguage("UO_by")%>&nbsp;<asp:DropDownList id=drpSortOrder Runat="server" CssClass="Normal">
				<asp:ListItem value="Username"></asp:ListItem>
				<asp:listitem value="FirstName"></asp:listitem>
			</asp:DropDownList>&nbsp;<%= DotNetZoom.GetLanguage("UO_or")%>&nbsp;<asp:DropDownList id=drpSortDirection Runat="server" CssClass="Normal">
				<asp:ListItem value="ASC"></asp:ListItem>
				<asp:ListItem value="DESC"></asp:ListItem>
			</asp:DropDownList><asp:LinkButton id=lnkSearch runat="server"></asp:LinkButton></td></tr></table><br><asp:repeater id=rptUsers Runat="server" EnableViewState="True">
<headertemplate>
	<table 
	style="BORDER-RIGHT: #003366 1px solid; BORDER-TOP: #003366 1px solid; BORDER-LEFT: #003366 1px solid; BORDER-BOTTOM: #003366 1px solid" 
	cellspacing=2 cellpadding=4 width="100%" border=0>
		<tr bgcolor=#003366 class="NormalBold">
		<th align=left width="20%"><font color="white"><%= DotNetZoom.GetLanguage("UO_UserName")%></font></th>
		<th align=left width="20%"><font color="white"><%= DotNetZoom.GetLanguage("UO_Name")%></font></th> 
		<th align=left width="20%"><font color="white"><%= DotNetZoom.GetLanguage("UO_Since")%></font></th> 
		<th align=left width="20%"><font color="white"><%= DotNetZoom.GetLanguage("UO_OnLineNow")%></font></th> 
		<th align=left width="20%"><font color="white"><%= DotNetZoom.GetLanguage("UO_Body")%></font></th> 
	</tr>			
</HeaderTemplate>
<itemtemplate>
	<tr bgcolor=#efefef class="normal">
		<td>
			<asp:HyperLink ID="lnkUsername" Runat="server" NavigateUrl='<%# GetUserInfoLink(DataBinder.Eval(Container.DataItem, "UserID")) %>' Tooltip='<%# GetUserInfoTooltip(DataBinder.Eval(Container.DataItem, "Username")) %>'>
				<%# DataBinder.Eval(Container.DataItem, "UserName") %>
			</asp:HyperLink>
		</td>
		<td><%# DataBinder.Eval(Container.DataItem, "FirstName") %>&nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName") %></td>
		<td><%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:dd MMM yyyy}") %></td>
		<td><font color=<%# GetOnlineColor(DataBinder.Eval(Container.DataItem, "SessionID").ToString()) %>><b><%# GetOnlineStatus(DataBinder.Eval(Container.DataItem, "SessionID").ToString()) %></b></font></td>
		<td><font color=<%# GetBuddyColor(DataBinder.Eval(Container.DataItem, "BuddyID").ToString()) %>><b><%# GetBuddyStatus(DataBinder.Eval(Container.DataItem, "BuddyID").ToString()) %></b></font></td>
	</tr>
</ItemTemplate>
<footertemplate>
	</table>
</FooterTemplate>
</asp:repeater><asp:placeholder id=pnlPaging runat="server" visible="False">
      <table cellspacing=2 cellpadding=4 width="100%" border=0>
        <tr class="normal">
          <td><%= DotNetZoom.GetLanguage("UO_Page")%>&nbsp;<asp:Label id=lblPageNumber Runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("UO_From")%>&nbsp;<asp:Label id=lblPageCount Runat="server"></asp:Label></td>
          <td align=right><asp:linkbutton id=lnkFirstPage runat="server" CommandName="FirstPage" OnCommand="Page_Changed"></asp:linkbutton>&nbsp;<asp:linkbutton id=lnkPrevPage runat="server" CommandName="PrevPage" OnCommand="Page_Changed"></asp:linkbutton>&nbsp;<asp:linkbutton id=lnkNextPage runat="server" CommandName="NextPage" OnCommand="Page_Changed"></asp:linkbutton>&nbsp;<asp:linkbutton id=lnkLastPage runat="server" CommandName="LastPage" OnCommand="Page_Changed"></asp:linkbutton></td></tr></table></asp:placeholder></td></tr></TBODY></table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>