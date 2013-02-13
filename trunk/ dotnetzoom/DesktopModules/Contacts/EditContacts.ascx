<%@ Control Language="vb" CodeBehind="EditContacts.ascx.vb" AutoEventWireup="false"
    Explicit="True" Inherits="DotNetZoom.EditContacts" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<Portal:Title ID="Title1" runat="server"></Portal:Title>
<asp:Literal ID="before" runat="server" EnableViewState="false"></asp:Literal>
<asp:PlaceHolder ID="AdminPanel" runat="server" Visible="false">
    <table cellspacing="5" cellpadding="5" width="750" border="0">
        <tbody>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="SubHead" colspan="2">
                    <%= DotNetZoom.GetLanguage("Contact_ShowEdit")%>:
                </td>
            </tr>
            <tr valign="top">
                <td align="left" colspan="2">
                    <asp:RadioButtonList ID="chkCapcha" runat="server" Width="600" Font-Size="8pt" Font-Names="Verdana,Arial"
                        CellSpacing="0" CellPadding="0" RepeatColumns="1">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr width="500" noshade="noshade" size="1" />
                </td>
            </tr>
            <tr>
                <td class="SubHead" colspan="2">
                    <%= DotNetZoom.GetLanguage("Contact_ShowEditField")%>:
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkname" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"
                        Enabled="false" Checked="true"></asp:CheckBox>
                </td>
                <td>
                    <asp:TextBox ID="txtname" CssClass="NormalTextBox" Width="390" Columns="30" MaxLength="50"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkrole" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial">
                    </asp:CheckBox>
                </td>
                <td>
                    <asp:TextBox ID="txtrole" CssClass="NormalTextBox" Width="390" Columns="30" MaxLength="50"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkemail" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial">
                    </asp:CheckBox>
                </td>
                <td>
                    <asp:TextBox ID="txtemail" CssClass="NormalTextBox" Width="390" Columns="30" MaxLength="50"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkphone1" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial">
                    </asp:CheckBox>
                </td>
                <td>
                    <asp:TextBox ID="Txtphone1" CssClass="NormalTextBox" Width="390" Columns="30" MaxLength="50"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkphone2" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial">
                    </asp:CheckBox>
                </td>
                <td>
                    <asp:TextBox ID="Txtphone2" CssClass="NormalTextBox" Width="390" Columns="30" MaxLength="50"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </tbody>
    </table>
    <p align="left">
        <asp:LinkButton class="CommandButton" ID="cmdUpdate1" runat="server" Text="Enregistrer"
            CausesValidation="False"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton class="CommandButton" ID="cmdCancel1" runat="server" Text="Annuler"
            CausesValidation="False"></asp:LinkButton>
    </p>
</asp:PlaceHolder>
<asp:PlaceHolder ID="MainPanel" runat="server" Visible="true">
    <table cellspacing="0" cellpadding="0" width="750" border="0">
        <tbody>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr valign="top">
                <td class="SubHead" width="100">
                    <label for="<%=NameField.ClientID%>">
                        <%= GetName("Name")%>:</label>
                </td>
                <td rowspan="5">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:TextBox ID="NameField" runat="server" CssClass="NormalTextBox" Width="390" Columns="30"
                        MaxLength="50"></asp:TextBox>
                </td>
                <td width="25" rowspan="5">
                    &nbsp;
                </td>
                <td class="Normal" width="250">
                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" Display="Static"
                        ControlToValidate="NameField"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr valign="top" runat="server" id="tr0">
                <td class="SubHead">
                    <label for="<%=RoleField.ClientID%>">
                        <%= GetName("contact_title")%>:</label>
                </td>
                <td>
                    <asp:TextBox ID="RoleField" runat="server" CssClass="NormalTextBox" Width="390" Columns="30"
                        MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr valign="top" runat="server" id="tr1">
                <td class="SubHead">
                    <label for="<%=EmailField.ClientID%>">
                        <%= GetName("contact_email")%>:</label>
                </td>
                <td>
                    <asp:TextBox ID="EmailField" runat="server" CssClass="NormalTextBox" Width="390"
                        Columns="30" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr valign="top" runat="server" id="tr2">
                <td class="SubHead">
                    <label for="<%=Contact1Field.ClientID%>">
                        <%= GetName("contact_telephone1")%>:</label>
                </td>
                <td>
                    <asp:TextBox ID="Contact1Field" runat="server" CssClass="NormalTextBox" Width="390"
                        Columns="30" MaxLength="250"></asp:TextBox>
                </td>
            </tr>
            <tr valign="top" runat="server" id="tr3">
                <td class="SubHead">
                    <label for="<%=Contact2Field.ClientID%>">
                        <%= GetName("contact_telephone2")%>:</label>
                </td>
                <td>
                    <asp:TextBox ID="Contact2Field" runat="server" CssClass="NormalTextBox" Width="390"
                        Columns="30" MaxLength="250"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
    <p align="left">
        <asp:LinkButton class="CommandButton" ID="cmdUpdate" runat="server" Text="Enregistrer"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton class="CommandButton" ID="cmdCancel" runat="server" Text="Annuler"
            CausesValidation="False"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton class="CommandButton" ID="cmdDelete" runat="server" Text="Supprimer"
            CausesValidation="False"></asp:LinkButton>
    </p>
    <hr width="500" noshade="noshade" size="1" />
    <asp:PlaceHolder ID="pnlAudit" runat="server"><span class="Normal">
        <%= DotNetZoom.GetLanguage("label_update_by") %>&nbsp;<asp:Label ID="CreatedBy" runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("label_update_the") %>&nbsp;<asp:Label
            ID="CreatedDate" runat="server"></asp:Label>
    </span></asp:PlaceHolder>
</asp:PlaceHolder>
<asp:Literal ID="after" runat="server" EnableViewState="false"></asp:Literal>