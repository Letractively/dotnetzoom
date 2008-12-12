<%@ Control Language="vb" Inherits="DotNetZoom.WeatherNetwork" codebehind="WeatherNetwork.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server" EnableViewState="false"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server" EnableViewState="false">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:literal id="lblScript" runat="server" EnableViewState="false" ></asp:literal>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>