<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="MyMessagesModule.ascx.vb" Inherits="DotNetZoom.MyMessagesModule" TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing=0 cellpadding=4 width="100%">
<tr  class="Normal"><td>
<img height="10" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -259px;" title="<%= DotNetZoom.GetLanguage("UO_ToRead")%>">&nbsp;<a href='<%= GetMessageUrl() %>'><b><%= DotNetZoom.GetLanguage("UO_pm")%></b></a><br>
<img height="10" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -309px;" title="<%= DotNetZoom.GetLanguage("UO_NotRead")%>"> <%= DotNetZoom.GetLanguage("UO_NotRead")%>: <b><asp:label id="lblUnreadCount" runat="server"></asp:label></b><br>
<img height="10" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -269px;" title="<%= DotNetZoom.GetLanguage("UO_Read")%>"> <%= DotNetZoom.GetLanguage("UO_Read")%>: <b><asp:label style="text-decoration: blink" id="lblReadCount" runat="server"></asp:label></b>
<asp:label id=lblWarningMessage runat="server"></asp:label></td></tr></table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal></asp:placeholder>