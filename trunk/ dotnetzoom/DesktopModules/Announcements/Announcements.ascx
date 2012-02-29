<%@ Control Language="vb" EnableViewState="False" Inherits="DotNetZoom.Announcements" codebehind="Announcements.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="ANN" TagName="AnnData" Src="../Announcements/Announcement.ascx"%>
<portal:title id="Title1" runat="server" EnableViewState="False"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server" EnableViewState="True">
<asp:literal id="before" runat="server" EnableViewState="False"></asp:literal>
<ANN:AnnData id=AnnData  runat="server"></ANN:AnnData>
<asp:literal id="after" runat="server" EnableViewState="False"></asp:literal>
</asp:PlaceHolder>