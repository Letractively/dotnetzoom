<%@ Control Language="vb" codebehind="EditImage.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditImage" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750" >
    <tbody>
        <tr valign="top">
            <td class="SubHead" width="120">
                <label style="DISPLAY: none" for="<%=optInternal.ClientID%>"><%= DotNetZoom.GetLanguage("image_select_iternal")%></label> 
                <asp:RadioButton id="optInternal" Runat="server" GroupName="ImageType" AutoPostBack="True"></asp:RadioButton>
                &nbsp;<label for="<%=cboInternal.ClientID%>"><%= DotNetZoom.GetLanguage("image_iternal")%>:</label> 
            </td>
            <td class="Normal">
                <asp:dropdownlist id="cboInternal" runat="server" CssClass="NormalTextBox" Width="390" DataValueField="Value" DataTextField="Text"></asp:dropdownlist>
                &nbsp; 
                <asp:HyperLink id="cmdUpload" Runat="server" CssClass="CommandButton"></asp:HyperLink>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label style="DISPLAY: none" for="<%=optExternal.ClientID%>"><%= DotNetZoom.GetLanguage("image_select_external")%></label>&nbsp;<asp:RadioButton id="optExternal" Runat="server" GroupName="ImageType" AutoPostBack="True"></asp:RadioButton>
                <%= DotNetZoom.GetLanguage("image_external")%><label for="<%=txtExternal.ClientID%>">:</label> 
            </td>
            <td class="Normal">
                <asp:TextBox id="txtExternal" runat="server" cssclass="NormalTextBox" width="390" Columns="30"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtAlt.ClientID%>"><%= DotNetZoom.GetLanguage("image_alt_text")%>:</label></td>
            <td>
                <asp:TextBox id="txtAlt" runat="server" cssclass="NormalTextBox" Columns="50"></asp:TextBox>
                <asp:RequiredFieldValidator id="valAltText" runat="server" ControlToValidate="txtAlt" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtArtist.ClientID%>"><%= DotNetZoom.GetLanguage("image_artist")%>:</label></td>
            <td>
                <asp:TextBox id="txtArtist" runat="server" cssclass="NormalTextBox" Columns="50"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtCopyright.ClientID%>"><%= DotNetZoom.GetLanguage("image_copyright")%>:</label></td>
            <td>
                <asp:TextBox id="txtCopyright" runat="server" cssclass="NormalTextBox" Columns="50"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtDescription.ClientID%>"><%= DotNetZoom.GetLanguage("image_description")%>:</label></td>
            <td>
                <asp:TextBox id="txtDescription" runat="server" cssclass="NormalTextBox" Columns="50"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtTitle.ClientID%>"><%= DotNetZoom.GetLanguage("image_title")%>:</label></td>
            <td>
                <asp:TextBox id="txtTitle" runat="server" cssclass="NormalTextBox" Columns="50"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtWidth.ClientID%>"><%= DotNetZoom.GetLanguage("image_width")%>:</label></td>
            <td>
                <asp:TextBox id="txtWidth" runat="server" cssclass="NormalTextBox" Columns="50"></asp:TextBox>
                <asp:RegularExpressionValidator id="valWidth" runat="server" ControlToValidate="txtWidth" Display="Dynamic"  ValidationExpression="^[1-9]+[0]*$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtHeight.ClientID%>"><%= DotNetZoom.GetLanguage("image_height")%>:</label></td>
            <td>
                <asp:TextBox id="txtHeight" runat="server" cssclass="NormalTextBox" Columns="50"></asp:TextBox>
                <asp:RegularExpressionValidator id="valHeight" runat="server" ControlToValidate="txtHeight" Display="Dynamic" ValidationExpression="^[1-9]+[0]*$"></asp:RegularExpressionValidator>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" runat="server" ></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdCancel" runat="server"  CausesValidation="False"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>