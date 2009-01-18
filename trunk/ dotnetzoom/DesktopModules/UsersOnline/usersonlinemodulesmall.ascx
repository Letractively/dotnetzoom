<%@ Control Language="vb" autoeventwireup="false" codebehind="UsersOnlineModulesmall.ascx.vb" Inherits="DotNetZoom.UsersOnlineModulesmall" targetschema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server" EnableViewState="false"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server" EnableViewState="false">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<div align="left" class="Normal">
<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -105px;"> <b><u><%= DotNetZoom.GetLanguage("UO_online")%>:</u></b>
<br>
<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -319px;"> <%= DotNetZoom.GetLanguage("UO_Anonymous")%>: <b><asp:Literal id="lblGuestCountsmall" runat="server" EnableViewState="false" /></b>
<br>
<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -171px;"> <%= DotNetZoom.GetLanguage("UO_Members")%>: <b><asp:Literal id="lblMemberCountsmall" runat="server" EnableViewState="false" /></b>
<br>
<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -295px;"> <%= DotNetZoom.GetLanguage("UO_TRegistered")%>: <b><asp:Literal id="lblTotalCountsmall" runat="server" EnableViewState="false" /></b>
<br>
</div>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>