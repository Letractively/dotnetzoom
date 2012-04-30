<%@ Control Language="vb" codebehind="EditXml.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditXml" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750">
    <tbody>
        <tr valign="top">
            <td class="SubHead"><%= DotNetZoom.GetLanguage("XML_data")%>: 
            </td>
        </tr>
        <tr valign="top">
            <td>
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td>
                <asp:Label id="Label1" runat="server" cssclass="SubHead"><%= DotNetZoom.GetLanguage("XML_internal")%>:</asp:Label>&nbsp; 
                <asp:RadioButton id="optXMLInternal" Runat="server" GroupName="XMLType" AutoPostBack="True"></asp:RadioButton>
                &nbsp; 
                <asp:dropdownlist id="cboXML" runat="server" Width="390" DataValueField="Value" DataTextField="Text" CssClass="NormalTextBox"></asp:dropdownlist>
                &nbsp; 
                <asp:HyperLink id="cmdUpload1" Runat="server" CssClass="CommandButton"></asp:HyperLink>
                   <asp:ImageButton ID="UploadReturn" runat="server" EnableViewState="false" Visible="False"
                        Height="16" Width="16" ImageUrl="~/images/save.gif" Style="border-width: 0px">
                    </asp:ImageButton>
 
            </td>
        </tr>
        <tr valign="top">
            <td>
                <asp:Label id="Label2" runat="server" cssclass="SubHead"><%= DotNetZoom.GetLanguage("XML_external")%>:</asp:Label>&nbsp; 
                <asp:RadioButton id="optXMLExternal" Runat="server" GroupName="XMLType" AutoPostBack="True"></asp:RadioButton>
                &nbsp; 
                <asp:TextBox id="txtXML" runat="server" CssClass="NormalTextBox" width="390" Columns="30" maxlength="150"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td>
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td class="SubHead"><%= DotNetZoom.GetLanguage("XML_transform")%>: 
            </td>
        </tr>
        <tr valign="top">
            <td>
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td>
                <label for="<%=optXSLInternal.ClientID%>"><%= DotNetZoom.GetLanguage("XML_internal")%>:</label>&nbsp; 
                <asp:RadioButton id="optXSLInternal" Runat="server" GroupName="XSLType" AutoPostBack="True"></asp:RadioButton>
                &nbsp; 
                <asp:dropdownlist id="cboXSL" runat="server" Width="390" DataValueField="Value" DataTextField="Text" CssClass="NormalTextBox"></asp:dropdownlist>
                &nbsp; 
                <asp:HyperLink id="cmdUpload2" Runat="server" CssClass="CommandButton"></asp:HyperLink>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <label for="<%=optXSLExternal.ClientID%>"><%= DotNetZoom.GetLanguage("XML_external")%>:</label>&nbsp; 
                <asp:RadioButton id="optXSLExternal" Runat="server" GroupName="XSLType" AutoPostBack="True"></asp:RadioButton>
                &nbsp; 
                <asp:TextBox id="txtXSL" runat="server" CssClass="NormalTextBox" width="390" Columns="30" maxlength="150"></asp:TextBox>
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