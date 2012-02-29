<%@ Control Language="vb" codebehind="Signup.ascx.vb" EnableViewState="true" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.Signup" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<div class="main" id="main">
<table cellspacing="0" cellpadding="4" width="750"  border="0">
    <tbody>
        <tr>
            <td colspan="2">
                <table cellspacing="0" cellpadding="2"  border="0">
                    <tbody>
                        <tr>
                            <td class="Normal">
                                <asp:Label id="lblInstructions" runat="server" cssclass="Normal"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="3">
                <table cellspacing="0" cellpadding="0" width="100%" >
                    <tbody>
                        <tr>
                            <td align="left">
							<%= DotNetZoom.GetLanguage("S_InstalPortal") %>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr noshade="noshade" size="1" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="2"  border="0">
                    <tbody>
                        <tr>
                            <td valign="middle" colspan="2">
                                <asp:Label id="lblMessage" runat="server" cssclass="NormalRed"></asp:Label></td>
                        </tr>
                        <tr id="rowType" runat="server">
                            <td class="SubHead">
							<%= DotNetZoom.GetLanguage("S_PortalType") %>:</td>
                            <td class="SubHead" colspan="2">
                                <asp:RadioButton id="optParent" GroupName="PortalType" Runat="server" CssClass="NormalTextBox"></asp:RadioButton>
                                &nbsp;<%= DotNetZoom.GetLanguage("S_PortalTypeParent") %>&nbsp;&nbsp; 
                                <asp:RadioButton id="optChild" GroupName="PortalType" Runat="server" CssClass="NormalTextBox" AutoPostBack="True"></asp:RadioButton>
                                &nbsp;<%= DotNetZoom.GetLanguage("S_PortalTypeChild") %> 
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead">
                                <label for="<%=txtPortalName.ClientID%>"><%= DotNetZoom.GetLanguage("S_PortalName") %>:</label></td>
                            <td>
                                <asp:textbox id="txtPortalName" runat="server" CssClass="NormalTextBox" width="300" MaxLength="128"></asp:textbox>
                            </td>
                       </tr>
                       <tr>
                       <td></td>
                            <td>
                                <asp:requiredfieldvalidator id="valPortalName" runat="server" CssClass="Normal" Display="Dynamic" ControlToValidate="txtPortalName"></asp:requiredfieldvalidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead">
                                <label for="<%=cboTemplate.ClientID%>"><%= DotNetZoom.GetLanguage("S_PortalTemplate") %>:</label></td>
                            <td>
                                <asp:DropDownList id="cboTemplate" AutoPostBack="True" Runat="server" CssClass="NormalTextBox" Width="300"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                         
                        <tr>
                            <td colspan="2">
                                <asp:Label id="lblAdminAccount" runat="server" cssclass="NormalBold">
								<%= DotNetZoom.GetLanguage("S_PortalAdmin") %></asp:Label></td>
                        </tr>
                     
                        <tr>
                            <td class="SubHead">
                                <label for="<%=txtFirstName.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Name") %>:</label></td>
                            <td>
                                <asp:textbox id="txtFirstName" runat="server" CssClass="NormalTextBox" width="300" MaxLength="100"></asp:textbox>
                            </td>
                       </tr>
                       <tr>
                        <td></td>
                            <td>
                                <asp:requiredfieldvalidator id="valFirstName" runat="server" CssClass="Normal" Display="Dynamic"  ControlToValidate="txtFirstName"></asp:requiredfieldvalidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead">
                                <label for="<%=txtLastName.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_LastName") %>:</label></td>
                            <td>
                                <asp:textbox id="txtLastName" runat="server" CssClass="NormalTextBox" width="300" MaxLength="100"></asp:textbox>
                            </td>
                       </tr>
                       <tr>
                       <td></td>
                            <td> 
                              <asp:requiredfieldvalidator id="valLastName" runat="server" CssClass="Normal" Display="Dynamic" ControlToValidate="txtLastName"></asp:requiredfieldvalidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead">
                                <label for="<%=txtUsername.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_UserCode") %>:</label></td>
                            <td>
                                <asp:textbox id="txtUsername" runat="server" CssClass="NormalTextBox" width="300" MaxLength="100"></asp:textbox>
                            </td>
                       </tr>
                       <tr>
                       <td></td>
                            <td>
                                <asp:requiredfieldvalidator id="valUsername" runat="server" CssClass="Normal" Display="Dynamic"  ControlToValidate="txtUsername"></asp:requiredfieldvalidator>
								<asp:RegularExpressionValidator id="RvalUsername" CssClass="Normal" ControlToValidate="txtUsername" ValidationExpression=".{8,}"  Display="dynamic" RunAt="server" ></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead">
                                <label for="<%=txtPassword.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Password") %>:</label></td>
                            <td>
                                <asp:textbox id="txtPassword" runat="server" CssClass="NormalTextBox" width="300" MaxLength="20" textmode="password"></asp:textbox>
                            </td>
                       </tr>
                       <tr>
                       <td></td>
                            <td>
                                <asp:requiredfieldvalidator id="valPassword" runat="server" CssClass="Normal" Display="Dynamic" ControlToValidate="txtPassword"></asp:requiredfieldvalidator>
								<asp:RegularExpressionValidator id="RvalPassword" CssClass="Normal" ControlToValidate="TxtPassword" ValidationExpression=".{8,}"  Display="dynamic" RunAt="server" ></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead">
                                <label for="<%=txtEmail.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Email") %>:</label></td>
                            <td>
                                <asp:textbox id="txtEmail" runat="server" CssClass="NormalTextBox" width="300" MaxLength="100"></asp:textbox>
                            </td>
                       </tr>
                       <tr>
                       <td></td>
                            <td>
                                <asp:requiredfieldvalidator id="valEmail" runat="server" CssClass="Normal" Display="Dynamic" ControlToValidate="txtEmail"></asp:requiredfieldvalidator>
								<asp:RegularExpressionValidator id="RvalEmail" ControlToValidate="txtEMail" CssClass="Normal" ValidationExpression="^[\w\.-]+@[\w-]+\.[\w\.-]+$"   Display="static" RunAt="server"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
			<td>
			<asp:Image Id="MyHtmlImage" width="300" Height="200" runat="server" Visible="false" EnableViewState="false"></asp:Image>
			</td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="2"  border="0">
                    <tbody>
                        <tr>
                            <td colspan="3">
                                <table cellspacing="0" cellpadding="0"  border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:linkbutton cssclass="CommandButton" id="cmdUpdate" runat="server"></asp:linkbutton>
                                                &nbsp;&nbsp; 
                                                <asp:linkbutton cssclass="CommandButton" id="cmdCancel" runat="server" CausesValidation="False"></asp:linkbutton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table cellspacing="0" cellpadding="2"  border="0">
                    <tbody>
                        <tr>
                            <td class="Normal">
							<asp:Label id="lblPortalDef" EnableViewState="false" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
</div>
<div align="left">
<asp:ValidationSummary 
     id="valSum"
     ShowMessageBox = "true"
     DisplayMode="BulletList" 
     runat="server"
     Font-Size="12"/>
</div>
<div class="attendre" id="attendre" align="center" style="background: silver; border: thin dotted; padding: 4px; visibility:hidden;  top: -400px; position: relative">
<span class="Head"><input class="button" onclick="toggleBox('attendre',0);toggleBox('main',1);" type="button" value="<%= DotNetZoom.GetLanguage("S_F_Wait") %>"></span><br><br>
<img src="/images/rotation.gif" alt="*" width="32" height="32">
<br><br>
<span class="SubHead">
<%= DotNetZoom.GetLanguage("S_F_Wait_Info") %></span>
</div>

<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>