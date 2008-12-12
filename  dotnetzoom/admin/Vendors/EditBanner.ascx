<%@ Control Language="vb" codebehind="EditBanner.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditBanner" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>

<script language="javascript" type="text/javascript"  src="controls/PopupCalendar.js"></script>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="2" cellpadding="0" width="750">
    <tbody>
        <tr valign="top">
            <td class="SubHead" width="100">
                <label for="<%=txtBannerName.ClientID%>"><%= DotNetZoom.GetLanguage("Vendor_Banner_Name") %>:</label></td>
            <td rowspan="8">
                &nbsp;</td>
            <td>
                <asp:textbox id="txtBannerName" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="100"></asp:textbox>
                <br>
                <asp:requiredfieldValidator id="valBannerName" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtBannerName"></asp:requiredfieldValidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=cboBannerType.ClientID%>"><%= DotNetZoom.GetLanguage("Vendor_Banner_Type") %>:</label></td>
            <td>
                <asp:DropDownList id="cboBannerType" runat="server" cssclass="NormalTextBox" width="390" DataValueField="BannerTypeId" DataTextField="BannerTypeName"></asp:DropDownList>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=cboImage.ClientID%>"><%= DotNetZoom.GetLanguage("Vendor_Banner_Image") %>:</label></td>
            <td>
                <asp:dropdownlist id="cboImage" runat="server" CssClass="NormalTextBox" DataValueField="Value" DataTextField="Text" Width="390"></asp:dropdownlist>
                &nbsp; 
                <asp:HyperLink id="cmdUpload" CssClass="CommandButton" Runat="server"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtURL.ClientID%>"><%= DotNetZoom.GetLanguage("Vendor_Banner_URL") %>:</label></td>
            <td>
                <asp:TextBox id="txtURL" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtImpressions.ClientID%>"><%= DotNetZoom.GetLanguage("Vendor_Banner_Imp") %>:</label></td>
            <td>
                <asp:TextBox id="txtImpressions" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="10"></asp:TextBox>
                <br>
                <asp:requiredfieldValidator id="valImpressions" runat="server" CssClass="NormalRed" Display="Dynamic"  ControlToValidate="txtImpressions"></asp:requiredfieldValidator>
                <asp:compareValidator id="compareImpressions" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtImpressions" Type="Integer" Operator="DataTypeCheck"></asp:compareValidator>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtCPM.ClientID%>"><%= DotNetZoom.GetLanguage("Vendor_Banner_CPM") %>:</label></td>
            <td>
                <asp:TextBox id="txtCPM" runat="server" cssclass="NormalTextBox" width="390" Columns="30" maxlength="7"></asp:TextBox>
                <br>
                <asp:requiredfieldValidator id="valCPM" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtCPM"></asp:requiredfieldValidator>
                <asp:compareValidator id="compareCPM" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtCPM" Type="Currency" Operator="DataTypeCheck"></asp:compareValidator>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtStartDate.ClientID%>"><%= DotNetZoom.GetLanguage("Vendor_Banner_StartDate") %>:</label></td>
            <td>
                <asp:TextBox id="txtStartDate" runat="server" cssclass="NormalTextBox" width="120" Columns="30" maxlength="11"></asp:TextBox>
                &nbsp; 
                <asp:hyperlink id="cmdStartCalendar" CssClass="CommandButton" Runat="server"></asp:hyperlink>
                <asp:comparevalidator id="valStartDate" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtStartDate" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtEndDate.ClientID%>"><%= DotNetZoom.GetLanguage("Vendor_Banner_EndDate") %>:</label></td>
            <td>
                <asp:TextBox id="txtEndDate" runat="server" cssclass="NormalTextBox" width="120" Columns="30" maxlength="11"></asp:TextBox>
                &nbsp; 
                <asp:hyperlink id="cmdEndCalendar" CssClass="CommandButton" Runat="server"></asp:hyperlink>
                <asp:comparevalidator id="valEndDate" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtEndDate" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator>
            </td>
        </tr>
    </tbody>
</table>
<br>
<span class="SubHead"><%= DotNetZoom.GetLanguage("Vendor_Banner_Optional") %></span> 
<p align="left">
    <asp:linkbutton class="CommandButton" id="cmdUpdate" runat="server" ></asp:linkbutton>
    &nbsp; 
    <asp:linkbutton class="CommandButton" id="cmdCancel" runat="server"  CausesValidation="False"></asp:linkbutton>
    &nbsp; 
    <asp:linkbutton class="CommandButton" id="cmdDelete" runat="server"  CausesValidation="False"></asp:linkbutton>
</p>
<hr width="500" noshade="noshade" size="1" />
<asp:placeholder id="pnlAudit" Runat="server">
    <span class="Normal"><%= DotNetZoom.GetLanguage("label_update_by") %>&nbsp;<asp:Label id="lblCreatedBy" runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("label_update_the") %>&nbsp;<asp:Label id="lblCreatedDate" runat="server"></asp:Label>
    <br>
    </span>
    <br>
    <table cellspacing="0" cellpadding="0" width="500">
        <tbody>
            <tr valign="top">
                <td class="SubHead" width="100">
                    Expositions:</td>
                <td valign="middle" width="320">
                    <asp:Label id="lblViews" runat="server" cssclass="Normal"></asp:Label></td>
            </tr>
            <tr valign="top">
                <td class="SubHead" width="100">
                    <%= DotNetZoom.GetLanguage("Vendors_Clicks") %>:</td>
                <td valign="middle" width="320">
                    <asp:Label id="lblClickThroughs" runat="server" cssclass="Normal"></asp:Label></td>
            </tr>
            <tr valign="top">
                <td class="SubHead" width="100">
                    <label for="<%=chkLog.ClientID%>"><%= DotNetZoom.GetLanguage("Vendors_SeeClicks") %></label></td>
                <td valign="middle" width="320">
                    <asp:Checkbox id="chkLog" runat="server" CssClass="NormalTextBox" AutoPostBack="True"></asp:Checkbox>
                </td>
            </tr>
        </tbody>
    </table>
    <asp:datagrid id="grdLog" runat="server" Width="100%" EnableViewState="false" AutoGenerateColumns="false" CellSpacing="3" gridlines="none" BorderStyle="None">
        <Columns>
            <asp:BoundColumn DataField="LogDate" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
            <asp:BoundColumn DataField="Views" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        </Columns>
    </asp:datagrid>
</asp:placeholder>
<br>
<p align="left">
<span class="Normal"> 
<%= DotNetZoom.GetLanguage("VendorBannerInfo") %>
<%= DotNetZoom.GetLanguage("VendorBannerInfo1") %>
</span>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>