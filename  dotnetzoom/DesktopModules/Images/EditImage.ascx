<%@ Control Language="vb" codebehind="EditImage.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditImage" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750" >
    <tbody>
        <tr valign="top">
            <td class="SubHead" width="250">
                <asp:RadioButton id="optInternal" Runat="server" GroupName="ImageType" AutoPostBack="True"></asp:RadioButton>
                &nbsp;<%= DotNetZoom.GetLanguage("image_iternal")%>&nbsp;: 
            </td>
            <td class="Normal">
                <asp:dropdownlist id="cboInternal"  AutoPostBack="true" runat="server" CssClass="NormalTextBox" Width="300" DataValueField="Value" DataTextField="Text"></asp:dropdownlist>
                &nbsp; 
                <asp:HyperLink id="cmdUpload" Runat="server" CssClass="CommandButton"></asp:HyperLink>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <asp:RadioButton id="optExternal" Runat="server" GroupName="ImageType" AutoPostBack="True"></asp:RadioButton>
                &nbsp;<%= DotNetZoom.GetLanguage("image_external")%>&nbsp;: 
            </td>
            <td class="Normal">
                <asp:TextBox id="txtExternal" runat="server" cssclass="NormalTextBox" Width="300" Columns="75"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td class="SubHead" colspan="2">
                <asp:Checkbox id="optSecure" Runat="server" ></asp:Checkbox>&nbsp;<%= DotNetZoom.GetLanguage("image_Secure")%>            
            </td><td><%= DotNetZoom.GetLanguage("image_Secure_info")%></td>
        </tr>
        <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>

        <tr valign="top">
            <td class="SubHead">
                <%= DotNetZoom.GetLanguage("image_alt_text")%>&nbsp;:
                </td>
            <td>
                <asp:TextBox id="txtAlt" runat="server" cssclass="NormalTextBox" Columns="75"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <%= DotNetZoom.GetLanguage("image_width")%>&nbsp;:
                </td>
            <td>
                <asp:TextBox id="txtWidth" runat="server" cssclass="NormalTextBox" Columns="75"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <%= DotNetZoom.GetLanguage("image_height")%>&nbsp;:</td>
            <td>
                <asp:TextBox id="txtHeight" runat="server" cssclass="NormalTextBox" Columns="75"></asp:TextBox>
            </td>
        </tr>

 <tr><td colspan="2"><hr /></td></tr>

        <tr valign="top">
            <td class="SubHead">
                <asp:Checkbox id="optPosition" Runat="server" ></asp:Checkbox>&nbsp;<%= DotNetZoom.GetLanguage("image_GoogleMap")%>&nbsp;:
            </td>
            <td>
                <asp:TextBox id="TxtLatLong" runat="server" cssclass="NormalTextBox" Columns="75"></asp:TextBox>
            </td>
        </tr>

        <tr valign="top">
            <td class="SubHead">
                <asp:Checkbox id="optGoogleEarth" Runat="server" ></asp:Checkbox>&nbsp;<%= DotNetZoom.GetLanguage("image_GoogleEarth")%>            
            </td>
            <td>
                 <asp:dropdownlist id="cboInternalGPS"  AutoPostBack="false" runat="server" CssClass="NormalTextBox" Width="300" DataValueField="Value" DataTextField="Text"></asp:dropdownlist>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <asp:Checkbox id="optInternalLink" Runat="server" ></asp:Checkbox>&nbsp;<%= DotNetZoom.GetLanguage("image_Link")%>:</td>
            <td>
                 <asp:dropdownlist id="cboInternalLink"  AutoPostBack="false" runat="server" CssClass="NormalTextBox" Width="300" DataValueField="TabId" DataTextField="TabName"></asp:dropdownlist>
            </td>
        </tr>


         <tr valign="top">
            <td class="SubHead">
                <asp:Checkbox id="optInfoBule" Runat="server" ></asp:Checkbox>&nbsp;<%= DotNetZoom.GetLanguage("image_description")%>:
            </td>
            <td>
                <asp:TextBox id="txtDescription"  TextMode="MultiLine"  MaxLength="250" height="150" runat="server" cssclass="NormalTextBox" Columns="75"></asp:TextBox>
            </td>
        </tr>
         <tr valign="top">
            <td class="SubHead">
                <asp:Checkbox id="optExif" Runat="server" ></asp:Checkbox>&nbsp;<%= DotNetZoom.GetLanguage("image_Exif")%>:</td>
            <td>
                <asp:TextBox id="txtExif" runat="server" TextMode="MultiLine"  MaxLength="250" height="150" cssclass="NormalTextBox" Columns="75"></asp:TextBox>
            </td>
        </tr>
   
<tr><td colspan="2"><hr /></td></tr>

        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtTitle.ClientID%>"><%= DotNetZoom.GetLanguage("image_title")%>:</label></td>
            <td>
                <asp:TextBox id="txtTitle" runat="server" cssclass="NormalTextBox" Columns="75"></asp:TextBox>
            </td>
        </tr>

        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtArtist.ClientID%>"><%= DotNetZoom.GetLanguage("image_artist")%>:</label></td>
            <td>
                <asp:TextBox id="txtArtist" runat="server" cssclass="NormalTextBox" Columns="75"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtCopyright.ClientID%>"><%= DotNetZoom.GetLanguage("image_copyright")%>:</label></td>
            <td>
                <asp:TextBox id="txtCopyright" runat="server" cssclass="NormalTextBox" Columns="75"></asp:TextBox>
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