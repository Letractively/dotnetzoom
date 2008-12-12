<%@ Control language="vb" Inherits="DotNetZoom.ImageModule" CodeBehind="ImageModule.ascx.vb" AutoEventWireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<portal:title runat="server" EnableViewState="false" id=Title1 />
<asp:PlaceHolder id="pnlModuleContent" Runat="server" EnableViewState="false">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:image id="imgImage" runat="server" EnableViewState="false" />
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>