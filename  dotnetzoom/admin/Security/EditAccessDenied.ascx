<%@ Control codebehind="EditAccessDenied.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditAccessDenied" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<div align="left" class="ItemTitle ">
<br>
<br>
<asp:literal EnableViewState="false" id="lblTerms" runat="server">
</asp:literal>
<br>
<br>
<asp:HyperLink id="hypReturn" visible="true" runat="server">Retour</asp:HyperLink>
</div> 
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>   