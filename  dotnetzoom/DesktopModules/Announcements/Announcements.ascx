<%@ Control Language="vb" EnableViewState="False" Inherits="DotNetZoom.Announcements" codebehind="Announcements.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="ANN" TagName="AnnData" Src="../Announcements/Announcement.ascx"%>
<portal:title id="Title1" runat="server" DisplayOptions="true" EnableViewState="False"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server" EnableViewState="True">
<asp:literal id="before" runat="server" EnableViewState="False"></asp:literal>
<div runat="server" id="ajax">
<ANN:AnnData id=AnnData  runat="server"></ANN:AnnData>
</div>
<asp:literal id="after" runat="server" EnableViewState="False"></asp:literal>
</asp:PlaceHolder>