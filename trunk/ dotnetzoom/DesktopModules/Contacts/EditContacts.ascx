<%@ Control Language="vb" codebehind="EditContacts.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditContacts" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750"  border="0">
    <tbody>
        <tr valign="top">
            <td class="SubHead" width="100">
                <label for="<%=NameField.ClientID%>"><%= DotNetZoom.GetLanguage("Name") %>:</label> 
            </td>
            <td rowspan="5">
                &nbsp; 
            </td>
            <td align="left">
                <asp:TextBox id="NameField" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="50"></asp:TextBox>
            </td>
            <td width="25" rowspan="5">
                &nbsp; 
            </td>
            <td class="Normal" width="250">
                <asp:RequiredFieldValidator id="Requiredfieldvalidator1" runat="server" Display="Static" ControlToValidate="NameField"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=RoleField.ClientID%>"><%= DotNetZoom.GetLanguage("contact_title") %>:</label> 
            </td>
            <td>
                <asp:TextBox id="RoleField" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="100"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=EmailField.ClientID%>"><%= DotNetZoom.GetLanguage("contact_email") %>:</label> 
            </td>
            <td>
                <asp:TextBox id="EmailField" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="100"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=Contact1Field.ClientID%>"><%= DotNetZoom.GetLanguage("contact_telephone") %>:</label> 
            </td>
            <td>
                <asp:TextBox id="Contact1Field" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="250"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=Contact2Field.ClientID%>"><%= DotNetZoom.GetLanguage("contact_telephone") %>:</label> 
            </td>
            <td>
                <asp:TextBox id="Contact2Field" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="250"></asp:TextBox>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" runat="server" Text="Enregistrer" ></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdCancel" runat="server" Text="Annuler"  CausesValidation="False"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdDelete" runat="server" Text="Supprimer"  CausesValidation="False"></asp:LinkButton>
</p>
<hr width="500" noshade="noshade" size="1" />
<asp:placeholder id="pnlAudit" Runat="server">
    <span class="Normal"><%= DotNetZoom.GetLanguage("label_update_by") %>&nbsp;<asp:Label id="CreatedBy" runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("label_update_the") %>&nbsp;<asp:Label id="CreatedDate" runat="server"></asp:Label>
    </span>
</asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>