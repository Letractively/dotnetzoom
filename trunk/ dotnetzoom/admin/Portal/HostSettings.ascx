<%@ Control Inherits="DotNetZoom.HostSettings" codebehind="HostSettings.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="2"  border="0">
    <tbody>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtHostTitle.ClientID%>"><%=DotNetZoom.GetLanguage("HS_HostName") %>:</label></td>
            <td>
                <asp:textbox id="txtHostTitle" runat="server" width="300" MaxLength="256" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtHostURL.ClientID%>"><%=DotNetZoom.GetLanguage("HS_URLHost") %>:</label></td>
            <td>
                <asp:textbox id="txtHostURL" runat="server" width="300" MaxLength="256" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtHostEmail.ClientID%>"><%=DotNetZoom.GetLanguage("HS_EmailHost") %>:</label></td>
            <td>
                <asp:textbox id="txtHostEmail" runat="server" width="300" MaxLength="256" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <%=DotNetZoom.GetLanguage("HS_TimeHost") %>:</td>
            <td>
			<asp:Label id="ddlTimeserver" runat="server" CssClass="NormalTextBox" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=ddlTimeZone.ClientID%>"><%=DotNetZoom.GetLanguage("HS_TimeZoneHost") %>:</label></td>
            <td>
			<asp:DropDownList id="ddlTimeZone" Width="300px" DataValueField="Zone" DataTextField="Description" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td width="300" class="SubHead">
                <label for="<%=ddlViewState.ClientID%>"><%=DotNetZoom.GetLanguage("HS_ViewState") %>:</label></td>
            <td>
			<asp:DropDownList id="ddlViewState" Width="300px" runat="server" cssclass="NormalTextBox" >
                <asp:ListItem Value="M"></asp:ListItem>
                <asp:ListItem Value="S"></asp:ListItem>
			</asp:DropDownList>
            </td>
        </tr>
		
		<tr>
            <td width="300" class="SubHead">
                <label for="<%=ddlWhiteSpace.ClientID%>"><%=DotNetZoom.GetLanguage("HS_HTMLClean") %>:</label></td>
            <td>
			<asp:DropDownList id="ddlWhiteSpace" Width="300px" runat="server" cssclass="NormalTextBox" >
                <asp:ListItem Value="E"></asp:ListItem>
                <asp:ListItem Value="H"></asp:ListItem>
                <asp:ListItem Value="T"></asp:ListItem>
			</asp:DropDownList>
            </td>
        </tr>
		
        <tr>
            <td width="300" class="SubHead" valign="top">
                <label for="<%=cboProcessor.ClientID%>"><%=DotNetZoom.GetLanguage("HS_Paiement") %>:</label></td>
            <td>
                <asp:dropdownlist id="cboProcessor" CssClass="NormalTextBox" Runat="server" Width="300" DataValueField="URL" DataTextField="Processor"></asp:dropdownlist>
                <p>
                <asp:linkbutton id="cmdProcessor" runat="server" cssclass="CommandButton"></asp:linkbutton>
                </p>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtUserId.ClientID%>"><%=DotNetZoom.GetLanguage("HS_PaiementCode") %>:</label></td>
            <td>
                <asp:textbox id="txtUserId" runat="server" width="300" MaxLength="50" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtPassword.ClientID%>"><%=DotNetZoom.GetLanguage("HS_PaiementPassword") %>:</label></td>
            <td>
                <asp:textbox id="txtPassword" runat="server" width="300" MaxLength="50" CssClass="NormalTextBox" TextMode="Password"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtHostFee.ClientID%>"><%=DotNetZoom.GetLanguage("HS_HostFee") %>:</label></td>
            <td>
                <asp:textbox id="txtHostFee" runat="server" width="300" MaxLength="10" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=cboHostCurrency.ClientID%>"><%=DotNetZoom.GetLanguage("HS_Currency") %>:</label></td>
            <td>
                <asp:DropDownList id="cboHostCurrency" CssClass="NormalTextBox" Runat="server" Width="300" DataValueField="Code" DataTextField="Description"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtHostSpace.ClientID%>"><%=DotNetZoom.GetLanguage("HS_PortalSpace") %>:</label></td>
            <td>
                <asp:textbox id="txtHostSpace" runat="server" width="300" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtSiteLogHistory.ClientID%>"><%=DotNetZoom.GetLanguage("HS_SiteLog") %>:</label></td>
            <td>
                <asp:textbox id="txtSiteLogHistory" runat="server" width="300" MaxLength="3" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtDemoPeriod.ClientID%>"><%=DotNetZoom.GetLanguage("HS_DemoPeriod") %>:</label></td>
            <td>
                <asp:textbox id="txtDemoPeriod" runat="server" width="300" MaxLength="3" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=chkDemoSignup.ClientID%>"><%=DotNetZoom.GetLanguage("HS_DemoAllow") %></label></td>
            <td>
                <asp:checkbox id="chkDemoSignup" runat="server" CssClass="NormalTextBox"></asp:checkbox>
				&nbsp;&nbsp;<font size="1">(/default.aspx?def=demo)</font>
			</td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=chkPageTitleVersion.ClientID%>"><%=DotNetZoom.GetLanguage("HS_DNZVersion") %></label></td>
            <td>
                <asp:checkbox id="chkPageTitleVersion" runat="server" CssClass="NormalTextBox"></asp:checkbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=chkEnableErrorReport.ClientID%>"><%=DotNetZoom.GetLanguage("HS_ErrorReporting") %></label></td>
            <td>
                <asp:checkbox id="chkEnableErrorReport" runat="server" CssClass="NormalTextBox"></asp:checkbox>
				&nbsp;&nbsp;<font size="1"><%=DotNetZoom.GetLanguage("HS_ErrorReportingInfo") %></font></td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=chkEnableSSL.ClientID%>"><%=DotNetZoom.GetLanguage("HS_EnableSSL") %></label></td>
            <td>
                <asp:checkbox id="chkEnableSSL" runat="server" CssClass="NormalTextBox"></asp:checkbox>
				&nbsp;&nbsp;<font size="1"><%=DotNetZoom.GetLanguage("HS_EnableSSLInfo") %></font></td>
        </tr>

        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtEncryptionKey.ClientID%>"><%=DotNetZoom.GetLanguage("HS_PassordCrypto") %>:</label></td>
            <td>
                <asp:textbox id="txtEncryptionKey" runat="server" width="300" MaxLength="256" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtProxyServer.ClientID%>"><%=DotNetZoom.GetLanguage("HS_Proxy") %>:</label></td>
            <td>
                <asp:textbox id="txtProxyServer" runat="server" width="300" MaxLength="256" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td width="300" class="SubHead">
                <label for="<%=txtProxyPort.ClientID%>"><%=DotNetZoom.GetLanguage("HS_ProxyPort") %>:</label></td>
            <td>
                <asp:textbox id="txtProxyPort" runat="server" width="300" MaxLength="256" CssClass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
      </tbody>
</table>
<br>
<hr noshade="noshade" size="1" />
<br>		
		
<table cellspacing="0" cellpadding="2"  border="0">
    <tbody>

        <tr>
            <td width="300">
             &nbsp;</td>
            <td  class="Head" valign="top">
			<%=DotNetZoom.GetLanguage("HS_SMTP") %>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>

		
        <tr>
            <td width="300" class="SubHead" valign="top">
                <label for="<%=txtSMTPServer.ClientID%>"><%=DotNetZoom.GetLanguage("HS_SMTP_Server") %>:</label></td>
            <td>
                <asp:textbox id="txtSMTPServer" runat="server" width="300" MaxLength="256" CssClass="NormalTextBox"></asp:textbox>
       </tr>
        <tr>
            <td width="300" class="SubHead" valign="top">
                <label for="<%=txtSMTPServerUser.ClientID%>"><%=DotNetZoom.GetLanguage("HS_SMTP_Code") %>:</label></td>
            <td>
                <asp:textbox id="txtSMTPServerUser" runat="server" width="300" MaxLength="256" CssClass="NormalTextBox"></asp:textbox>
        </tr>
        <tr>
            <td width="300" class="SubHead" valign="top">
                <label for="<%=txtSMTPServerPassword.ClientID%>"><%=DotNetZoom.GetLanguage("HS_SMTP_Password") %>:</label></td>
            <td>
                <asp:textbox id="txtSMTPServerPassword" runat="server" width="300" MaxLength="256" CssClass="NormalTextBox"></asp:textbox>
                <p>
                <asp:linkbutton id="cmdEmail" runat="server" cssclass="CommandButton"></asp:linkbutton>
                <asp:Label id="lblEmail" runat="server" cssclass="NormalRed"></asp:Label></p>
                </td>
        </tr>
      </tbody>
</table>
<br>
<hr noshade="noshade" size="1" />
<br>		
		
<table cellspacing="0" cellpadding="2"  border="0">
    <tbody>
        <tr>
            <td width="300" class="SubHead" valign="top">
                <label for="<%=txtFileExtensions.ClientID%>"><%=DotNetZoom.GetLanguage("HS_ext_upload") %>:</label></td>
            <td>
                <asp:textbox id="txtFileExtensions" runat="server" width="300" MaxLength="256" CssClass="NormalTextBox" TextMode="MultiLine" Rows="3"></asp:textbox>
            </td>
        </tr>
      </tbody>
</table>
<br>
<hr noshade="noshade" size="1" />
<br>		
		
<table cellspacing="0" cellpadding="2"  border="0">
    <tbody>

        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:linkbutton cssclass="CommandButton" id="cmdUpdate" runat="server"></asp:linkbutton>
            </td>
        </tr>
    </tbody>
</table>
<br>
<hr noshade="noshade" size="1" />
<br>
<span class="SubHead"><label for="<%=cboUpgrade.ClientID%>"><%=DotNetZoom.GetLanguage("HS_LogSQL") %>:</label></span>&nbsp; 
<asp:DropDownList id="cboUpgrade" CssClass="NormalTextBox" Runat="server"></asp:DropDownList>
&nbsp; 
<asp:LinkButton id="cmdUpgrade" CssClass="CommandButton" Runat="server"></asp:LinkButton>
<br>
<br>
<asp:Label id="lblUpgrade" runat="server" cssclass="Normal"></asp:Label>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>