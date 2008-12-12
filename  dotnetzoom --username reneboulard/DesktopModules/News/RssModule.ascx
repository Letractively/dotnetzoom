<%@ Control Language="vb" Inherits="DotNetZoom.RssModule" codebehind="RssModule.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<span class="Normal">
<asp:xml id="xmlRSS" runat="server"></asp:xml>
</span><asp:Label id="lblMessage" runat="server" visible="False" cssclass="Normal"></asp:Label>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>