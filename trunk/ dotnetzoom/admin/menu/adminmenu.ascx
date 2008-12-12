<%@ Control Inherits="DotNetZoom.adminmenu" codebehind="adminmenu.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<div align="left">
<asp:literal id="lblAdminMenu" visible="false" runat="server" EnableViewState="false" />
</div>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>