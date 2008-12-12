<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Control language="vb" Inherits="DotNetZoom.HtmlModule" CodeBehind="HtmlModule.ascx.vb" AutoEventWireup="false" Explicit="True" %>
<portal:title runat="server" EnableViewState="false" id="Title1" />
<asp:PlaceHolder ID="pnlModuleContent" Runat="server" EnableViewState="false">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:literal id="HtmlHolder" runat="server" EnableViewState="false" />
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>