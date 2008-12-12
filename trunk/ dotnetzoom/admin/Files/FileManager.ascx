<%@ Control Inherits="DotNetZoom.FileManager" codebehind="FileManager.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="Portal" TagName="TagFileManager" Src="~/Admin/AdvFileManager/TAGAdvFileManager.ascx" %>
<portal:title id="Title1" runat="server" DisplayOptions="True"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table align="center">
<tr>
<td>
<asp:Label id="lblDiskSpace" runat="server" enableviewstate="False" cssclass="Normal"></asp:Label>
<portal:TagFileManager id="Files" runat="server"></portal:TagFileManager>
</td>
</tr>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>