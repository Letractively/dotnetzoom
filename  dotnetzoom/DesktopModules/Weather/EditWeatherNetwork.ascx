<%@ Control Language="vb" codebehind="EditWeatherNetwork.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditWeatherNetwork" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="Portal" TagName="Address" Src="~/controls/Address.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<portal:address id="Address1" runat="server"></portal:address>
<br>
<span class="SubHead"><label class="SubHead" for="<%=chkPersonalize.ClientID%>"><%= DotNetZoom.GetLanguage("w_personalise")%></label>&nbsp;</span> 
<asp:CheckBox id="chkPersonalize" CssClass="NormalCheckBox" Runat="server"></asp:CheckBox>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" runat="server"  Text="Enregistrer"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdCancel" runat="server"  Text="Annuler" CausesValidation="False"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>