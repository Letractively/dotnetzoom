<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="MyBuddiesModule.ascx.vb" Inherits="DotNetZoom.MyBuddiesModule" TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing=0 cellpadding=4 width="100%">
  <tr class=Normal>
    <td><asp:DataList id=lstOnlineBuddies Runat="server" cssclass="Normal">
						<itemtemplate>
							<asp:Label ID="lblUserNumber" Runat="server" />:&nbsp;
							<asp:HyperLink ID="lnkUsername" Runat="server" NavigateUrl='<%# GetUserInfoLink(DataBinder.Eval(Container.DataItem, "BuddyID")) %>' Tooltip='<%# GetUserInfoTooltip(DataBinder.Eval(Container.DataItem, "Username")) %>'>
								<%# DataBinder.Eval(Container.DataItem, "UserName") %>
							</asp:HyperLink>
							<br>
						</ItemTemplate>
						<headertemplate>
							<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -119px;"> <b><u><%= DotNetZoom.GetLanguage("UO_online")%>:</u></b><br>
						</HeaderTemplate>
					</asp:DataList><asp:DataList id=lstOfflineBuddies Runat="server" cssclass="Normal">
						<itemtemplate>
							<asp:Label ID="lblUserNumber" Runat="server" />:&nbsp;
							<asp:HyperLink ID="lnkUsername" Runat="server" NavigateUrl='<%# GetUserInfoLink(DataBinder.Eval(Container.DataItem, "BuddyID")) %>' Tooltip='<%# GetUserInfoTooltip(DataBinder.Eval(Container.DataItem, "Username")) %>'>
								<%# DataBinder.Eval(Container.DataItem, "UserName") %>
							</asp:HyperLink>
							<br>
						</ItemTemplate>
						<headertemplate>
							<hr>
							<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -105px;"> <b><u><%= DotNetZoom.GetLanguage("UO_offline")%>:</u></b><br>
						</HeaderTemplate>
					</asp:DataList><asp:Label id=lblWarningMessage Runat="server"></asp:Label></td></tr></table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>