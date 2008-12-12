<%@ Control Language="vb" codebehind="bienvenue.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.Bienvenue" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<div align="left">
<asp:Label EnableViewState="false" id="lblbienvenue" runat="server" cssclass="Normal" font-size="Medium" forecolor="#800040"> 
    <br>
    <br>
    {BienvenuePortail} 
    <br>
    <br>
    <br>
    </asp:Label>
<br>
<asp:LinkButton class="CommandButton" id="cmdCancel" runat="server" CausesValidation="False"></asp:LinkButton>
</div>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>