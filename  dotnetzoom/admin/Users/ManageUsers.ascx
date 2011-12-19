<%@ Control Language="vb" codebehind="ManageUsers.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.ManageUsers" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="Portal" TagName="Address" Src="~/controls/Address.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="1" cellpadding="0">
    <tbody>
        <tr>
            <td class="SubHead" width="100">
                <label for="<%=txtFirstName.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Name") %>:</label></td>
            <td>
                <asp:textbox id="txtFirstName" tabIndex="1" runat="server" size="25" MaxLength="50" cssclass="NormalTextBox"></asp:textbox>
                <asp:requiredfieldvalidator id="valFirstName" runat="server" Display="Dynamic" ControlToValidate="txtFirstName" CssClass="NormalRed"></asp:requiredfieldvalidator>
            </td>
            <td rowspan="7">
                &nbsp;&nbsp;</td>
            <td valign="top" rowspan="7">
                <portal:address id="Address1" runat="server"></portal:address>
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="100">
                <label for="<%=txtLastName.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_LastName") %>:</label></td>
            <td>
                <asp:textbox id="txtLastName" tabIndex="2" runat="server" size="25" MaxLength="50" cssclass="NormalTextBox"></asp:textbox>
                <asp:requiredfieldvalidator id="valLastName" runat="server" Display="Dynamic" ControlToValidate="txtLastName" CssClass="NormalRed"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="100">
                <label for="<%=txtUsername.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_UserCode") %>:</label></td>
            <td>
                <asp:textbox id="txtUsername" tabIndex="3" runat="server" size="25" MaxLength="100" cssclass="NormalTextBox"></asp:textbox>
                <asp:requiredfieldvalidator id="valUsername" runat="server" Display="Dynamic" ControlToValidate="txtUsername" CssClass="NormalRed"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="100">
                <label for="<%=txtPassword.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Password") %>:</label></td>
            <td>
                <asp:textbox id="txtPassword" tabIndex="4" runat="server" size="25" MaxLength="20" cssclass="NormalTextBox" TextMode="Password"></asp:textbox>
                <asp:requiredfieldvalidator id="valPassword" runat="server" Display="Dynamic" ControlToValidate="txtPassword" CssClass="NormalRed"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="100">
                <label for="<%=txtConfirm.ClientID%>"><%= DotNetZoom.GetLanguage("Confirm_Password") %>:</label></td>
            <td>
                <asp:textbox id="txtConfirm" tabIndex="5" runat="server" size="25" MaxLength="20" cssclass="NormalTextBox" TextMode="Password"></asp:textbox>
                <asp:requiredfieldvalidator id="valConfirm1" runat="server" Display="Dynamic" ControlToValidate="txtConfirm" CssClass="NormalRed"></asp:requiredfieldvalidator>
                <asp:comparevalidator id="valConfirm2" runat="server" Display="Dynamic" ControlToValidate="txtConfirm" CssClass="NormalRed" ControlToCompare="txtPassword"></asp:comparevalidator>
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="100">
                <label for="<%=txtEmail.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Email") %>:</label></td>
            <td>
                <asp:textbox id="txtEmail" tabIndex="6" runat="server" size="25" MaxLength="100" cssclass="NormalTextBox"></asp:textbox>
                <asp:requiredfieldvalidator id="valEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail" CssClass="NormalRed"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr id="rowAuthorized" runat="server">
            <td class="SubHead" width="100">
                <label for="<%=chkAuthorized.ClientID%>"><%= DotNetZoom.GetLanguage("U_Autorized") %>:</label></td>
            <td>
                <asp:CheckBox id="chkAuthorized" tabIndex="7" Runat="server"></asp:CheckBox>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:linkbutton cssclass="CommandButton" id="cmdUpdate" runat="server"></asp:linkbutton>
    &nbsp; 
    <asp:linkbutton cssclass="CommandButton" id="cmdCancel" runat="server"  CausesValidation="False"></asp:linkbutton>
    &nbsp; 
    <asp:linkbutton cssclass="CommandButton" id="cmdDelete" runat="server" CausesValidation="False"></asp:linkbutton>
    &nbsp; 
    <asp:linkbutton cssclass="CommandButton" id="cmdManage" runat="server" CausesValidation="False"></asp:linkbutton>
</p>
<asp:placeholder id="pnlSecurite" Runat="server">
<hr>
<p align="left" class="SubHead">
<%= DotNetZoom.GetLanguage("U_SecurityControl") %>
<br>
<%= DotNetZoom.GetLanguage("U_SecurityContryCode") %> : <asp:textbox id="Country" runat="server" Width="150px" MaxLength="50" cssclass="NormalTextBox"></asp:textbox>
&nbsp;
<%= DotNetZoom.GetLanguage("U_SecurityIPFROM") %> : <asp:textbox id="IPlow" runat="server"  Width="150px" MaxLength="15" cssclass="NormalTextBox"></asp:textbox>
&nbsp;
<%= DotNetZoom.GetLanguage("U_SecurityIPTO") %> : <asp:textbox id="IPHigh" runat="server"  Width="150px" MaxLength="15" cssclass="NormalTextBox"></asp:textbox>
<br>
<br>
<asp:linkbutton cssclass="CommandButton" id="cmdUpdateCode" runat="server" Text="Enregistrer"></asp:linkbutton>
</p>
<hr>
</asp:placeholder>
<asp:placeholder id="pnlAudit" Runat="server">
    <table>
        <tbody>
            <tr>
                <td class="SubHead">
				<%= DotNetZoom.GetLanguage("U_Created_Date") %> :</td>
                <td>
                    <asp:Label id="lblCreatedDate" runat="server" cssclass="Normal"></asp:Label></td>
            </tr>
            <tr>
                <td class="SubHead">
				<%= DotNetZoom.GetLanguage("U_LastLogin_Date") %>:</td>
                <td>
                    <asp:Label id="lblLastLoginDate" runat="server" cssclass="Normal"></asp:Label></td>
            </tr>
        </tbody>
    </table>
</asp:placeholder>
<p align="left">
    <asp:Label id="Message" runat="server" cssclass="NormalRed"></asp:Label>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>