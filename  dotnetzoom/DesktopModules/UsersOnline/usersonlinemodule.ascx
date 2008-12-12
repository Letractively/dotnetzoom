<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="UsersOnlineModule.ascx.vb" Inherits="DotNetZoom.UsersOnlineModule" TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<portal:title id="Title1" runat="server"></portal:title>
	<asp:PlaceHolder id="pnlModuleContent" Runat="server">
	<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
		<table cellSpacing="0" cellPadding="4" width="100%">
			<tr class="Normal">
				<td><img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -91px;"> <B><U><%= DotNetZoom.GetLanguage("UO_Registered")%>:</U></B><br>
					<img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -133px;"> <%= DotNetZoom.GetLanguage("UO_LRegistered")%>: <B>
						<asp:HyperLink ID="lnkLatestUserName" Runat="server" EnableViewState="false" /></B><br>
					<img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -185px;"> <%= DotNetZoom.GetLanguage("UO_TRegistered")%>: <B>
						<asp:Literal id="lblNewToday" Runat="server" EnableViewState="false" /></B><br>
					<img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -199px;"> <%= DotNetZoom.GetLanguage("UO_YRegistered")%>: <B>
						<asp:Literal id="lblNewYesterday" Runat="server" EnableViewState="false" /></B><br>
					<img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -213px;"> <%= DotNetZoom.GetLanguage("UO_TRegistered")%>: <B>
						<asp:Literal id="lblUserCount" Runat="server" EnableViewState="false" /></B><br>
					<HR>
					<img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -105px;"> <B><U><%= DotNetZoom.GetLanguage("UO_online")%>:</U></B>
					<br>
					<img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -319px;"> <%= DotNetZoom.GetLanguage("UO_Anonymous")%>: <B>
						<asp:Literal id="lblGuestCount" Runat="server" EnableViewState="false" /></B><br>
					<img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -171px;"> <%= DotNetZoom.GetLanguage("UO_Members")%>: <B>
						<asp:Literal id="lblMemberCount" Runat="server" EnableViewState="false" /></B><br>
					<img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -295px;"> <%= DotNetZoom.GetLanguage("UO_TRegistered")%>: <B>
						<asp:Literal id="lblTotalCount" Runat="server" EnableViewState="false" /></B><br>
					<asp:Repeater id="rptOnlineNow" Runat="server" EnableViewState="false">
						<HeaderTemplate>
							<HR><img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -119px;"> <B><U><%= DotNetZoom.GetLanguage("UO_OnLine_Now")%>:</U></B><br>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:Literal ID="lblUserNumber" Runat="server" EnableViewState="false" />:&nbsp;<asp:HyperLink ID="lnkUsername" Runat="server" EnableViewState="false" NavigateUrl='<%# GetUserInfoLink(DataBinder.Eval(Container.DataItem, "UserID")) %>' Tooltip='<%# GetUserInfoTooltip(DataBinder.Eval(Container.DataItem, "Username")) %>'><%# DataBinder.Eval(Container.DataItem, "UserName") %></asp:HyperLink>
							<br>
						</ItemTemplate>
					</asp:Repeater></td>
			</tr>
		</table>
		<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
	</asp:PlaceHolder>