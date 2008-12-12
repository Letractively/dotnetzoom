<%@ Control Language="vb" codebehind="EditRoles.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditRoles" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750" border="0">
    <tbody>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtRoleName.ClientID%>"><%= DotNetZoom.GetLanguage("Name") %>:</label></td>
            <td align="left">
                <asp:TextBox id="txtRoleName" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="50"></asp:TextBox>
                <asp:RequiredFieldValidator id="valRoleName" runat="server" Display="Dynamic" ControlToValidate="txtRoleName" CssClass="NormalRed"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtDescription.ClientID%>"><%= DotNetZoom.GetLanguage("label_description") %>:</label></td>
            <td>
                <asp:TextBox id="txtDescription" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="1000" Height="84px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
<asp:placeHolder id="pnlServicesFee" runat="server">
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtServiceFee.ClientID%>"><%= DotNetZoom.GetLanguage("ServicesFee") %>:</label></td>
            <td>
                <asp:TextBox id="txtServiceFee" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="50"></asp:TextBox>
                <asp:comparevalidator id="valServiceFee1" runat="server" Display="Dynamic"  ControlToValidate="txtServiceFee" CssClass="NormalRed" Operator="DataTypeCheck" Type="Currency"></asp:comparevalidator>
                <asp:comparevalidator id="valServiceFee2" runat="server" Display="Dynamic"  ControlToValidate="txtServiceFee" CssClass="NormalRed" Operator="GreaterThan" ValueToCompare="0"></asp:comparevalidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtBillingPeriod.ClientID%>"><%= DotNetZoom.GetLanguage("Billing_Period") %>:</label></td>
            <td>
                <asp:TextBox id="txtBillingPeriod" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="50"></asp:TextBox>
                <asp:comparevalidator id="valBillingPeriod1" runat="server" Display="Dynamic"  ControlToValidate="txtBillingPeriod" CssClass="NormalRed" Operator="DataTypeCheck" Type="Integer"></asp:comparevalidator>
                <asp:comparevalidator id="valBillingPeriod2" runat="server" Display="Dynamic"  ControlToValidate="txtBillingPeriod" CssClass="NormalRed" Operator="GreaterThan" ValueToCompare="0"></asp:comparevalidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=cboBillingFrequency.ClientID%>"><%= DotNetZoom.GetLanguage("Billing_Frequency") %>:</label></td>
            <td>
                <asp:DropDownList id="cboBillingFrequency" runat="server" CssClass="NormalTextBox" DataTextField="Description" DataValueField="Code" Width="388px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtTrialFee.ClientID%>"><%= DotNetZoom.GetLanguage("Demo_Trial_Fee") %>:</label></td>
            <td>
                <asp:TextBox id="txtTrialFee" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="50"></asp:TextBox>
                <asp:comparevalidator id="valTrialFee1" runat="server" Display="Dynamic"  ControlToValidate="txtTrialFee" CssClass="NormalRed" Operator="DataTypeCheck" Type="Currency"></asp:comparevalidator>
                <asp:comparevalidator id="valTrialFee2" runat="server" Display="Dynamic"  ControlToValidate="txtTrialFee" CssClass="NormalRed" Operator="GreaterThan" ValueToCompare="0"></asp:comparevalidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtTrialPeriod.ClientID%>"><%= DotNetZoom.GetLanguage("Billing_Period") %>:</label></td>
            <td>
                <asp:TextBox id="txtTrialPeriod" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="50"></asp:TextBox>
                <asp:comparevalidator id="valTrialPeriod1" runat="server" Display="Dynamic"  ControlToValidate="txtTrialPeriod" CssClass="NormalRed" Operator="DataTypeCheck" Type="Integer"></asp:comparevalidator>
                <asp:comparevalidator id="valTrialPeriod2" runat="server" Display="Dynamic"  ControlToValidate="txtTrialPeriod" CssClass="NormalRed" Operator="GreaterThan" ValueToCompare="0"></asp:comparevalidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=cboTrialFrequency.ClientID%>"><%= DotNetZoom.GetLanguage("Demo_Trial_Fee") %>:</label></td>
            <td>
                <asp:DropDownList id="cboTrialFrequency" runat="server" CssClass="NormalTextBox" DataTextField="Description" DataValueField="Code" Width="388px"></asp:DropDownList>
            </td>
        </tr>
		<tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
</asp:placeHolder>
		<tr valign="top">
            <td class="SubHead"><%= DotNetZoom.GetLanguage("Role_Upload") %>:
			</td>
            <td class="normal">
             <asp:CheckBox id="chkUpload" Runat="server"></asp:CheckBox>
            </td>
        </tr>
<asp:placeHolder id="pnlAssignation" runat="server">
        <tr>
            <td class="SubHead">
                <label for="<%=chkIsPublic.ClientID%>"><%= DotNetZoom.GetLanguage("Role_Assignation_Public") %></label></td>
            <td>
                <asp:CheckBox id="chkIsPublic" Runat="server"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=chkAutoAssignment.ClientID%>"><%= DotNetZoom.GetLanguage("Role_Assignation_Auto") %></label></td>
            <td>
                <asp:CheckBox id="chkAutoAssignment" Runat="server"></asp:CheckBox>
            </td>
        </tr>
</asp:placeHolder>
    </tbody>
</table>
<p align="left">
    <asp:LinkButton id="cmdUpdate" runat="server" CssClass="CommandButton"></asp:LinkButton>
    &nbsp;
    <asp:LinkButton id="cmdCancel" runat="server" CssClass="CommandButton" CausesValidation="False"></asp:LinkButton>
    &nbsp;
    <asp:LinkButton id="cmdDelete" runat="server" CssClass="CommandButton" CausesValidation="False"></asp:LinkButton>
    &nbsp;
    <asp:LinkButton id="cmdManage" runat="server" CssClass="CommandButton" CausesValidation="False"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>