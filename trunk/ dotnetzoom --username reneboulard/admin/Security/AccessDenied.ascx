<%@ Control codebehind="AccessDenied.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.AccessDeniedPage" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:literal EnableViewState="false" id="lblTerms" runat="server">
</asp:literal>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>