<%@ Control Language="vb" codebehind="Address.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.Address" %>
<%@ Register TagPrefix="wc" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="CountryListBox" %>
<table cellspacing="0" cellpadding="1" border="0">
    <tbody>
        <tr id="rowCountry" runat="server">
            <td valign="top" style="white-space: nowrap;" width="90">
                <label class="SubHead" for="<%=cboCountry.ClientID%>"><asp:Label id="lblCountry" runat="server" cssclass="SubHead"></asp:Label>:</label></td>
            <td valign="top" style="white-space: nowrap;">
                <WC:COUNTRYLISTBOX id="cboCountry" runat="server" CssClass="NormalTextBox" AutoPostBack="True" DataTextField="Description" DataValueField="Code" Width="200px" LocalhostCountryCode="CA" TestIP=""></WC:COUNTRYLISTBOX>
                <asp:CheckBox id="chkCountry" CssClass="NormalTextBox" AutoPostBack="True" Runat="server" Visible="False"></asp:CheckBox>
                <asp:Label id="lblCountryRequired" runat="server" cssclass="NormalBold"></asp:Label>
                <asp:requiredfieldvalidator id="valCountry" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="cboCountry"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr id="rowRegion" runat="server">
            <td valign="top" style="white-space: nowrap;" width="90">
                <label class="SubHead" for="<%=cboRegion.ClientID%>"><asp:Label id="lblRegion" runat="server" cssclass="SubHead"></asp:Label>:</label></td>
            <td valign="top" style="white-space: nowrap;">
                <asp:DropDownList id="cboRegion" runat="server" cssclass="NormalTextBox" DataTextField="Description" DataValueField="Code" Width="200px" Visible="False"></asp:DropDownList>
                <asp:textbox id="txtRegion" runat="server" cssclass="NormalTextBox" Width="200px" MaxLength="50"></asp:textbox>
                <asp:CheckBox id="chkRegion" CssClass="NormalTextBox" AutoPostBack="True" Runat="server" Visible="False"></asp:CheckBox>
                <asp:Label id="lblRegionRequired" runat="server" cssclass="NormalBold"></asp:Label>
                <asp:requiredfieldvalidator id="valRegion1" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="cboRegion"></asp:requiredfieldvalidator>
                <asp:requiredfieldvalidator id="valRegion2" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtRegion"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr id="rowCity" runat="server">
            <td valign="top" style="white-space: nowrap;" width="90">
                <label class="SubHead" for="<%=txtCity.ClientID%>"><asp:Label id="lblCity" runat="server" cssclass="SubHead"></asp:Label>:</label></td>
            <td valign="top" style="white-space: nowrap;">
                <asp:textbox id="txtCity" runat="server" cssclass="NormalTextBox" Width="200px" MaxLength="50"></asp:textbox>
                <asp:CheckBox id="chkCity" CssClass="NormalTextBox" AutoPostBack="True" Runat="server" Visible="False"></asp:CheckBox>
                <asp:Label id="lblCityRequired" runat="server" cssclass="NormalBold"></asp:Label>
                <asp:requiredfieldvalidator id="valCity" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtCity"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr id="rowStreet" runat="server">
            <td valign="top" style="white-space: nowrap;" width="90">
                <label class="SubHead" for="<%=txtStreet.ClientID%>"><asp:Label id="lblStreet" runat="server" cssclass="SubHead"></asp:Label>:</label></td>
            <td valign="top" style="white-space: nowrap;">
                <asp:textbox id="txtStreet" runat="server" cssclass="NormalTextBox" Width="200px" MaxLength="50"></asp:textbox>
                <asp:CheckBox id="chkStreet" CssClass="NormalTextBox" AutoPostBack="True" Runat="server" Visible="False"></asp:CheckBox>
                <asp:Label id="lblStreetRequired" runat="server" cssclass="NormalBold"></asp:Label>
                <asp:requiredfieldvalidator id="valStreet" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtStreet"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr id="rowUnit" runat="server">
            <td valign="top" style="white-space: nowrap;" width="90">
                <label class="SubHead" for="<%=txtUnit.ClientID%>"><asp:Label id="lblUnit" runat="server" cssclass="SubHead"></asp:Label>:</label></td>
            <td valign="top" style="white-space: nowrap;">
                <asp:textbox id="txtUnit" runat="server" cssclass="NormalTextBox" Width="200px" MaxLength="50"></asp:textbox>
            </td>
        </tr>
        <tr id="rowPostal" runat="server">
            <td valign="top" style="white-space: nowrap;" width="90">
                <label class="SubHead" for="<%=txtPostal.ClientID%>"><asp:Label id="lblPostal" runat="server" cssclass="SubHead"></asp:Label>:</label></td>
            <td valign="top" style="white-space: nowrap;">
                <asp:textbox id="txtPostal" runat="server" cssclass="NormalTextBox" Width="200px" MaxLength="50"></asp:textbox>
                <asp:CheckBox id="chkPostal" CssClass="NormalTextBox" AutoPostBack="True" Runat="server" Visible="False"></asp:CheckBox>
                <asp:Label id="lblPostalRequired" runat="server" cssclass="NormalBold"></asp:Label>
                <asp:requiredfieldvalidator id="valPostal" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtPostal"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr id="rowTelephone" runat="server">
            <td valign="top" style="white-space: nowrap;" width="90">
                <label class="SubHead" for="<%=txtTelephone.ClientID%>"><asp:Label id="lblTelephone" runat="server" cssclass="SubHead"></asp:Label>:</label></td>
            <td valign="top" style="white-space: nowrap;">
                <asp:textbox id="txtTelephone" runat="server" cssclass="NormalTextBox" Width="200px" MaxLength="50"></asp:textbox>
                <asp:CheckBox id="chkTelephone" CssClass="NormalTextBox" AutoPostBack="True" Runat="server" Visible="False"></asp:CheckBox>
                <asp:Label id="lblTelephoneRequired" runat="server" cssclass="NormalBold"></asp:Label>
                <asp:requiredfieldvalidator id="valTelephone" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtTelephone"></asp:requiredfieldvalidator>
            </td>
        </tr>
    </tbody>
</table>