<%@ Control Language="vb" codebehind="EditDocs.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditDocs" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750" border="0">
    <tbody>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtName.ClientID%>"><%= DotNetZoom.GetLanguage("header_title")%>:</label> 
            </td>
            <td>
                <asp:TextBox id="txtName" runat="server" maxlength="150" Width="390" cssclass="NormalTextBox"></asp:TextBox>
                <br>
                <asp:RequiredFieldValidator id="valName" runat="server" CssClass="NormalRed" ControlToValidate="txtName" Display="Static"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label style="DISPLAY: none" for="<%=optInternal.ClientID%>"><%= DotNetZoom.GetLanguage("select_internal_doc")%></label> 
                <asp:RadioButton id="optInternal" AutoPostBack="True" GroupName="ImageType" Runat="server"></asp:RadioButton>
                &nbsp;<label for="<%=cboInternal.ClientID%>">&nbsp;<%= DotNetZoom.GetLanguage("internal_doc")%>:</label> 
            </td>
            <td>
                <asp:dropdownlist id="cboInternal" runat="server" Width="200" CssClass="NormalTextBox" DataTextField="Text" DataValueField="Value"></asp:dropdownlist>
                &nbsp; 
                <asp:HyperLink id="cmdUpload" CssClass="CommandButton" Runat="server"></asp:HyperLink>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label style="DISPLAY: none" for="<%=optExternal.ClientID%>"><%= DotNetZoom.GetLanguage("select_external_doc")%></label> 
                <asp:RadioButton id="optExternal" AutoPostBack="True" GroupName="ImageType" Runat="server"></asp:RadioButton>
                &nbsp;<%= DotNetZoom.GetLanguage("external_doc")%><label for="<%=txtExternal.ClientID%>">:</label> 
            </td>
            <td>
                <asp:textbox id="txtExternal" runat="server" maxlength="250" Width="390" cssclass="NormalTextBox"></asp:textbox>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtCategory.ClientID%>"><%= DotNetZoom.GetLanguage("type_doc")%>:</label> 
            </td>
            <td>
                <asp:TextBox id="txtCategory" runat="server" maxlength="50" Width="390" cssclass="NormalTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=chkSyndicate.ClientID%>"><%= DotNetZoom.GetLanguage("label_syndicate")%></label>: 
            </td>
            <td>
                <asp:CheckBox id="chkSyndicate" CssClass="NormalTextBox" Runat="server"></asp:CheckBox>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:LinkButton cssclass="CommandButton" id="cmdUpdate" runat="server"  Text="Enregistrer"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton cssclass="CommandButton" id="cmdCancel" runat="server"  Text="Annuler" CausesValidation="False"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton cssclass="CommandButton" id="cmdDelete" runat="server"  Text="Supprimer" CausesValidation="False"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton cssclass="CommandButton" id="cmdSyndicate" runat="server"  Text="contenu en syndication" CausesValidation="False">Contenu en syndication</asp:LinkButton>
</p>
<hr width="500" noshade="noshade" size="1" />
<asp:placeholder id="pnlAudit" Runat="server">
    <span class="Normal"><%= DotNetZoom.GetLanguage("label_update_by") %>&nbsp;<asp:Label id="lblCreatedBy" runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("label_update_the") %>&nbsp;<asp:Label id="lblCreatedDate" runat="server"></asp:Label></span>
<br>
</asp:placeholder>
<br>
<span class="SubHead"><%= DotNetZoom.GetLanguage("channel_syndicate") %> :</span>&nbsp;<asp:Label id="lblSyndicate" runat="server" cssclass="SubHead"></asp:Label> 
<br>
<br>
<table cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr valign="top">
            <td class="SubHead"><%= DotNetZoom.GetLanguage("label_clicks") %> :</td>
            <td>
            <asp:Label id="lblClicks" runat="server" cssclass="Normal"></asp:Label></td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=chkLog.ClientID%>"><%= DotNetZoom.GetLanguage("label_see_stat") %></label></td>
            <td>
                <asp:Checkbox id="chkLog" runat="server" CssClass="NormalTextBox" AutoPostBack="True"></asp:Checkbox>
            </td>
        </tr>
    </tbody>
</table>
<asp:datagrid id="grdLog" runat="server" Width="100%" EnableViewState="false" AutoGenerateColumns="false" CellSpacing="3" gridlines="none" BorderStyle="None">
    <Columns>
        <asp:BoundColumn DataField="DateTime" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn DataField="FullName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
    </Columns>
</asp:datagrid>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>