<%@ Control language="vb" Inherits="DotNetZoom.IFrame" CodeBehind="IFrame.ascx.vb" AutoEventWireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<portal:title runat="server" id="Title1" />
<asp:PlaceHolder ID="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:Label ID="lblIFrame" Runat="server" EnableViewState="false"></asp:Label>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>