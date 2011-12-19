<%@ Control Language="vb"  codebehind="Register.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.Register" %>
<%@ Register Assembly="dotnetzoom" Namespace="DotNetZoom" TagPrefix="cc1" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="Portal" TagName="Address" Src="~/controls/Address.ascx" %>

<portal:title id="Title1" runat="server" EnableViewState="true"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:placeholder id="UserRow" Runat="server">
    <table cellspacing="1" cellpadding="0" width="600">
        <tbody>
            <tr>
                <td colspan="4">
                    <asp:Label id="lblRegister" runat="server" cssclass="Normal"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp; 
                </td>
            </tr>
            <tr>
                <td class="SubHead" width="100">
                    <label for="<%=txtFirstName.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Name") %>:</label></td>
                <td class="NormalBold" style="white-space: nowrap;">
                    <asp:textbox id="txtFirstName" tabIndex="1" runat="server" cssclass="NormalTextBox" MaxLength="50" size="25"></asp:textbox>
                    &nbsp;* 
                    <asp:requiredfieldvalidator id="valFirstName" runat="server" ControlToValidate="txtFirstName" Display="Dynamic" CssClass="NormalRed"></asp:requiredfieldvalidator>
                </td>
                <td rowspan="6">
                    &nbsp;&nbsp;</td>
                <td class="NormalBold" valign="top" width="100" rowspan="6">
                    <portal:address id="Address1" runat="server"></portal:address>
                </td>
            </tr>
            <tr>
                <td class="SubHead" width="100">
                    <label for="<%=txtLastName.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_LastName") %>:</label></td>
                <td class="NormalBold" style="white-space: nowrap;">
                    <asp:textbox id="txtLastName" tabIndex="2" runat="server" cssclass="NormalTextBox" MaxLength="50" size="25"></asp:textbox>
                    &nbsp;* 
                    <asp:requiredfieldvalidator id="valLastName" runat="server" ControlToValidate="txtLastName" Display="Dynamic" CssClass="NormalRed"></asp:requiredfieldvalidator>
                </td>
            </tr>
            <tr>
                <td class="SubHead" width="100">
                    <label for="<%=txtUsername.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_UserCode") %>:</label></td>
                <td class="NormalBold" style="white-space: nowrap;">
                    <asp:textbox id="txtUsername" tabIndex="3" runat="server" cssclass="NormalTextBox" MaxLength="100" size="25"></asp:textbox>
                    &nbsp;* 
                    <asp:requiredfieldvalidator id="valUsername" runat="server" ControlToValidate="txtUsername" Display="Dynamic" CssClass="NormalRed"></asp:requiredfieldvalidator>
                </td>
            </tr>
            <tr>
                <td class="SubHead" width="100">
                    <label for="<%=txtPassword.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Password") %>:</label></td>
                <td class="NormalBold" style="white-space: nowrap;">
                    <asp:textbox id="txtPassword" tabIndex="4" runat="server" cssclass="NormalTextBox" MaxLength="20" size="25" TextMode="Password"></asp:textbox>
                    &nbsp;* 
                    <asp:requiredfieldvalidator id="valPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic" CssClass="NormalRed"></asp:requiredfieldvalidator>
                </td>
            </tr>
            <tr>
                <td class="SubHead" width="100">
                    <label for="<%=txtConfirm.ClientID%>"><%= DotNetZoom.GetLanguage("Confirm_Password") %>:</label></td>
                <td class="NormalBold" style="white-space: nowrap;">
                    <asp:textbox id="txtConfirm" tabIndex="5" runat="server" cssclass="NormalTextBox" MaxLength="20" size="25" TextMode="Password"></asp:textbox>
                    &nbsp;* 
                    <asp:requiredfieldvalidator id="valConfirm1" runat="server" ControlToValidate="txtConfirm" Display="Dynamic" CssClass="NormalRed"></asp:requiredfieldvalidator>
                    <asp:comparevalidator id="valConfirm2" runat="server" ControlToValidate="txtConfirm" Display="Dynamic" CssClass="NormalRed" ControlToCompare="txtPassword"></asp:comparevalidator>
                </td>
            </tr>
            <tr>
                <td class="SubHead" width="100">
                    <label for="<%=txtEmail.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Email") %>:</label></td>
                <td class="NormalBold" style="white-space: nowrap;">
                    <asp:textbox id="txtEmail" tabIndex="6" runat="server" cssclass="NormalTextBox" MaxLength="100" size="25"></asp:textbox>
                    &nbsp;* 
                    <asp:requiredfieldvalidator id="valEmail1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" CssClass="NormalRed"></asp:requiredfieldvalidator>
                    <asp:regularexpressionvalidator id="valEmail2" runat="server" ControlToValidate="txtEmail" Display="Dynamic" CssClass="NormalRed" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+"></asp:regularexpressionvalidator>
                </td>
            </tr>
        </tbody>
    </table>
 <asp:placeholder id="pnlSecurite" Runat="server">
<hr>
<cc1:OpenClose ID="O1" EnableViewState="true" visible="true" show="false" runat="server">
 <table cellspacing="0" cellpadding="0" width="600">
<tr><td colspan="2">
<p class="Normal"><%= DotNetZoom.GetLanguage("U_SecurityContryHelp") %></p></td> 
</tr>
<tr><td  valign="top" width="100px">
<asp:DropDownList id="cboCountry"  AutoPostBack="true" runat="server" cssclass="NormalTextBox" DataTextField="Description" DataValueField="Code" Width="200px" Visible="True"></asp:DropDownList>
</td><td width="500px">
<asp:datalist width="100%" id="dlCountryCode"  HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" ShowHeader="false" ShowFooter="false" ItemStyle-Height="30"  runat="Server" cssclass="Normal" itemstyle-wrap="False" cellpadding="0" repeatcolumns="5">
        <ItemTemplate>
		<asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("delete") & " -> " & DataBinder.Eval(Container, "DataItem.Description") %>' id="imgdelete" ImageURL="~/images/delete.gif"  Width="16px" Height="16px"  CommandName='<%# DataBinder.Eval(Container, "DataItem.Code") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Code") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        <%#DataBinder.Eval(Container, "DataItem.Description")%>
        </ItemTemplate>
	<separatortemplate>
	<img src="/images/1x1.gif" alt="*" border="0">
	</separatortemplate>
</asp:datalist>
<asp:textbox id="Country" visible="False" runat="server" Width="150px" MaxLength="50" cssclass="NormalTextBox"></asp:textbox>
</td></tr></table>
<hr>
<table cellspacing="0" cellpadding="0" width="600">
<tr><td colspan="4" width="600px">
<p class="Normal"><%= DotNetZoom.GetLanguage("U_SecurityIPHelp") %></p></td> 
</tr>
<tr align="left"><td class="SubHead" width="100px"><%= DotNetZoom.GetLanguage("U_SecurityIPFROM") %></td>
<td width="200px"><asp:textbox id="IPlow" runat="server"  Width="150px" MaxLength="15" cssclass="NormalTextBox"></asp:textbox></td>
<td class="SubHead" width="100px"><%= DotNetZoom.GetLanguage("U_SecurityIPTO") %></td>
<td width="200px"><asp:textbox id="IPHigh" runat="server"  Width="150px" MaxLength="15" cssclass="NormalTextBox"></asp:textbox>
</td></tr></table>
</cc1:OpenClose>

</asp:placeholder>
<asp:placeholder id="pnlAudit" visible="false" Runat="server">
<hr>
    <table cellspacing="0" cellpadding="0">
        <tbody>
            <tr align="left">
                <td width="150px" align="right" class="SubHead">
				<%= DotNetZoom.GetLanguage("U_Created_Date") %>&nbsp;:&nbsp;&nbsp;</td>
                <td width="150px" align="left">
                <asp:Label id="lblCreatedDate" visible="true" runat="server" cssclass="Normal"></asp:Label></td>
                <td width="150px"  align="right" class="SubHead">
				<%= DotNetZoom.GetLanguage("U_LastLogin_Date") %>&nbsp;:&nbsp;&nbsp;</td>
                <td width="150px" align="left">
                <asp:Label id="lblLastLoginDate" visible="true" runat="server" cssclass="Normal"></asp:Label></td>
            </tr>
        </tbody>
    </table>
<hr>
</asp:placeholder>
    <p align="left">
        <asp:linkbutton cssclass="CommandButton" id="RegisterBtn" runat="server" ></asp:linkbutton>
        &nbsp;&nbsp; 
        <asp:linkbutton cssclass="CommandButton" id="cmdCancel" runat="server" CausesValidation="False" ></asp:linkbutton>
        &nbsp;&nbsp; 
        <asp:linkbutton cssclass="CommandButton" id="UnregisterBtn" runat="server" ></asp:linkbutton>
    </p>
    <p align="left">
        <asp:Label id="Message" runat="server" cssclass="NormalRed"></asp:Label>
    </p>
    <p align="left">
        <asp:Label id="lblRegistration" runat="server" cssclass="Normal" width="600"></asp:Label>
    </p>
</asp:placeholder>
<asp:placeholder id="ServicesRow" visible="False" Runat="server">
    <table cellspacing="0" cellpadding="4" width="600" border="0">
        <tbody>
            <tr valign="top">
                <td colspan="3">
                    <table id="tableservices" runat="Server" cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <span class="Head"><%= DotNetZoom.GetLanguage("Membership_Serv") %></span> 
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
                    <p>
                        <asp:datagrid id="myDataGrid" runat="server" gridlines="none" Border="0" CellPadding="5" CellSpacing="5" AutoGenerateColumns="false" EnableViewState="true">
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:HyperLink visible='<%# ServiceExpired(DataBinder.Eval(Container.DataItem,"ExpiryDate"))%>' Text='<%# ServiceText(DataBinder.Eval(Container.DataItem,"Subscribed")) %>' CssClass="CommandButton" NavigateUrl='<%# ServiceURL("RoleID",DataBinder.Eval(Container.DataItem,"RoleID"),DataBinder.Eval(Container.DataItem,"ServiceFee"),DataBinder.Eval(Container.DataItem,"Subscribed")) %>' runat="server" ID="Hyperlink1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="RoleName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                                <asp:BoundColumn DataField="Description" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                                <asp:BoundColumn DataField="ServiceFee" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" DataFormatString="{0:#,##0.00}" />
								<asp:BoundColumn DataField="BillingPeriod" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
								<asp:BoundColumn DataField="BillingFrequency" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
 								<asp:BoundColumn visible="False" DataField="TrialFee" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" DataFormatString="{0:#,##0.00}" />
                                <asp:BoundColumn visible="False" DataField="TrialPeriod" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                                <asp:BoundColumn visible="False" DataField="TrialFrequency" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
								<asp:BoundColumn visible="False" DataField="ExpiryDate" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                            </Columns>
                        </asp:datagrid>
                        <asp:Label id="lblMessage" runat="server" cssclass="Normal" visible="False"></asp:Label>
                    </p>
                    <p>
                        <asp:linkbutton cssclass="CommandButton" id="ReturnButton" runat="server" CausesValidation="False" ></asp:linkbutton>
                    </p>
                </td>
            </tr>
        </tbody>
    </table>
</asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>