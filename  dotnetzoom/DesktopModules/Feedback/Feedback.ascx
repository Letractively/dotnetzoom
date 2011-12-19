<%@ Control Language="vb" EnableViewState=False autoeventwireup="false" Explicit="True" codebehind="Feedback.ascx.vb" Inherits="DotNetZoom.Feedback" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" NAME="Title1" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="4" border="0"><tbody><tr id="rowSendTo" valign="top" runat="server">
<td class="SubHead">
<label for="<%=txtSendTo.ClientID%>"><%= DotNetZoom.GetLanguage("to")%>:</label>
</td>
<td><asp:TextBox id="txtSendTo" runat="server" maxlength="100" columns="35" cssclass="NormalTextBox" Width="350px"></asp:TextBox>
</td>
</tr>
<tr valign="top">
<td class="SubHead">
<label for="<%=txtEmail.ClientID%>"><%= DotNetZoom.GetLanguage("email")%>:</label>
</td>
<td>
<asp:TextBox id="txtEmail" runat="server" maxlength="100" columns="35" cssclass="NormalTextBox" Width="350px"></asp:TextBox>
</td>
</tr>
<tr valign="top">
<td class="SubHead">
<label for="<%=txtName.ClientID%>"><%= DotNetZoom.GetLanguage("your_name")%>:</label>
</td>
<td>
<asp:TextBox id="txtName" runat="server" maxlength="100" columns="35" cssclass="NormalTextBox" Width="350px"></asp:TextBox>
</td>
</tr>
<tr valign="top">
<td class="SubHead"><label for="<%=txtSubject.ClientID%>"><%= DotNetZoom.GetLanguage("object")%>:</label>
</td>
<td>
<asp:TextBox id="txtSubject" runat="server" maxlength="100" columns="35" cssclass="NormalTextBox" Width="350px"></asp:TextBox>
</td>
</tr>
<tr valign="top">
<td class="SubHead">
<label for="<%=txtBody.ClientID%>"><%= DotNetZoom.GetLanguage("label_message_body")%>:</label>
</td>
<td>
<asp:TextBox id="txtBody" runat="server" columns="25" Width="350px" CssClass="NormalTextBox" Rows="16" TextMode="Multiline"></asp:TextBox>
</td>
</tr>
<tr valign="top">
<td>&nbsp;</td>
<td valign="middle" style="white-space: nowrap;">
<asp:LinkButton id="cmdSend" runat="server" CssClass="CommandButton" CausesValidation="True">Envoyer</asp:LinkButton>&nbsp;&nbsp;
<asp:LinkButton id="cmdCancel" runat="server" CssClass="CommandButton" CausesValidation="False">Annuler</asp:LinkButton>&nbsp;&nbsp;
<asp:LinkButton id="cmdUpdate" runat="server" CssClass="CommandButton" CausesValidation="False">Actualiser</asp:LinkButton></td></tr><tr valign="top"><td valign="middle" colspan="2"><asp:Label id="lblMessage" runat="server" cssclass="NormalRed"></asp:Label></td></tr></tbody></table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>