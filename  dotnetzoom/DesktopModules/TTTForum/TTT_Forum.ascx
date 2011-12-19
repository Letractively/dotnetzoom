<%@ Control Language="vb" Inherits="DotNetZoom.TTT_Forum" codebehind="TTT_Forum.ascx.vb" autoeventwireup="false" %>
<%@ Register TagPrefix="TTT" Namespace="DotNetZoom" Assembly="DotNetZoom" %>
<%@ Register TagPrefix="TTT" TagName="UsersOnline" Src="TTT_ForumUsersOnline.ascx" %>
<%@ Register TagPrefix="TTT" TagName="Statistics" Src="TTT_ForumStatistics.ascx" %>
<%@ import Namespace="DotNetZoom" %>
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="100%" border="0">
<tr>
<td class="TTTHeader" align="left" >
<!-- 
<%= DotNetZoom.GetLanguage("F_ForumSearch") %>
-->
<asp:textbox id="txtSearch" runat="server" EnableViewState="false" CssClass="NormalTextBox" Width="100px"></asp:textbox>&nbsp;
<asp:linkbutton id="btnSearch" runat="server" EnableViewState="false" CssClass="CommandButton"></asp:linkbutton>&nbsp;
</td>
<td class="TTTHeader" align="right" >
<!-- 
<%= DotNetZoom.GetLanguage("F_Group") %>
-->
<asp:hyperlink id="lnkAdmin" CssClass="CommandButton" runat="server" EnableViewState="false" Visible="False"></asp:hyperlink>
<asp:hyperlink id="lnkModerate" CssClass="CommandButton" runat="server" EnableViewState="false" Visible="False"></asp:hyperlink>
<asp:hyperlink id="lnkPMS" CssClass="CommandButton" runat="server" EnableViewState="false" Visible="False"></asp:hyperlink>
<asp:hyperlink id="lnkProfile" CssClass="CommandButton" runat="server" EnableViewState="false"  Visible="False"></asp:hyperlink>
<asp:hyperlink id="lnkSubscribe" CssClass="CommandButton" runat="server" EnableViewState="false"  Visible="False"></asp:hyperlink>
<asp:hyperlink id="lnkSearch" CssClass="CommandButton" runat="server" EnableViewState="false"></asp:hyperlink>
<asp:hyperlink id="lnkHome" CssClass="CommandButton" runat="server" EnableViewState="false" ></asp:hyperlink>
&nbsp;
</td>
</tr>
<tr>
<td class="TTTSubHeader" valign="middle" style="white-space: nowrap;" align="left" colspan="2">
<asp:button id="cmdNewTopic1" runat="server" EnableViewState="false" CssClass="button"  Visible="False"></asp:button>
<asp:checkbox id="chkEmail" runat="server" EnableViewState="false" CssClass="TTTNormal" Visible="False" AutoPostBack="True"></asp:checkbox>
</td>
</tr>
</table>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
<tr>
<td align="left" style="white-space: nowrap;" width="75%">
<span class="TTTSubHeader"><asp:Literal id="lblBreadCrumbs1" runat="server" EnableViewState="false" /></span>
</td>
<td align="right" style="white-space: nowrap;" width="25%">
<asp:dropdownlist id="ddlDisplay" EnableViewState="true" runat="server" CssClass="NormalTextBox" Width="120" AutoPostBack="True"></asp:dropdownlist>
&nbsp;&nbsp;
</td>
</tr>
</table>
<TTT:FORUM id="TTTForum" Runat="server"></TTT:FORUM>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
<tr>
<td align="left" style="white-space: nowrap;" width="75%">
<span class="TTTSubHeader"><asp:literal id="lblBreadCrumbs2" runat="server" EnableViewState="false" /></span>
</td>
<td align="right" style="white-space: nowrap;" width="25%">
<asp:button id="cmdNewTopic2" runat="server" EnableViewState="false" CssClass="button" Visible="False"></asp:button>
&nbsp;&nbsp;
</td>
</tr>
</table>
<asp:Placeholder id="pnlLeft" Runat="server" EnableViewState="false" Visible="false">
<asp:Placeholder id="pnlStats" Runat="server" EnableViewState="false">
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="100%">
<tr>
<td class="TTTRow" valign="middle">
<TTT:STATISTICS id="ctlStatistics" runat="Server" EnableViewState="false"></TTT:STATISTICS>
</td>
</tr>
</table>
</asp:Placeholder>	
<asp:Placeholder id="pnlUserOnline" Runat="server" EnableViewState="false">
<br>
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="100%">
<tr>
<td>
<TTT:USERSONLINE id="ctlUserOnline" runat="Server" EnableViewState="false"></TTT:USERSONLINE>
</td>
</tr>
</table>
</asp:Placeholder>
</asp:Placeholder>
