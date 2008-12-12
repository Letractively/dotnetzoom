<%@ Control language="vb" Inherits="DotNetZoom.XmlModule" CodeBehind="XmlModule.ascx.vb" AutoEventWireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<portal:title runat="server" id=Title1 />
<asp:PlaceHolder ID="pnlModuleContent" Runat="server">
<asp:xml id="xmlContent" runat="server" />
</asp:PlaceHolder>