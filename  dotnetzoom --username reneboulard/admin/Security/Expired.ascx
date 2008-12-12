<%@ Control Language="vb" Inherits="DotNetZoom.Expired" codebehind="Expired.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<table cellspacing="0" cellpadding="3" border="0">
    <tbody>
        <tr>
            <td valign="middle">
                <asp:Label class="NormalRed" id="Message" runat="server"></asp:Label></td>
        </tr>
    </tbody>
</table>
<br>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>