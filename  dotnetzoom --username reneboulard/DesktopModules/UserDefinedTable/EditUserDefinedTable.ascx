<%@ Control language="vb" CodeBehind="EditUserDefinedTable.ascx.vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetZoom.EditUserDefinedTable" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<portal:title runat="server" id="Title1" />
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:Table ID="tblFields" Runat="server"></asp:Table>
<asp:Label ID="lblMessage" Runat="server" CssClass="NormalRed"></asp:Label>
<p align="left">
<asp:LinkButton id="cmdUpdate" runat="server" class="CommandButton"  />
&nbsp;
<asp:LinkButton id="cmdCancel" CausesValidation="False" runat="server" class="CommandButton"  />
&nbsp;
<asp:LinkButton id="cmdDelete" CausesValidation="False" runat="server" class="CommandButton"  />
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>