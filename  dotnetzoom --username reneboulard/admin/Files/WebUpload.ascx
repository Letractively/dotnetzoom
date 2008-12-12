<%@ Control Language="vb" codebehind="WebUpload.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.WebUpload" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:placeHolder id="Upload" runat="server">
<table cellspacing="0" cellpadding="0" >
    <tbody>
	  <asp:placeHolder id="CanUpload" runat="server">
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
		<td class="SubHead" align="left" colspan="2"><%= DotNetZoom.GetLanguage("F_UploadInDirec") %>
        <asp:Label id="lblRootDir" runat="server" cssclass="Normal"></asp:Label></td>
        </td>
		</tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>

		<tr>
		<td class="SubHead" align="left">
        <label for="<%=cmdBrowse.ClientID%>"><%= DotNetZoom.GetLanguage("F_UploadFile") %>&nbsp;:&nbsp;</label> 
		</td>
		    <td align="left">
                <input id="cmdBrowse" type="file" size="50" runat="server" />&nbsp;&nbsp;<asp:LinkButton id="cmdAdd" Runat="server" CssClass="CommandButton"></asp:LinkButton>
			</td>
        </tr>
        <tr>
		<td>
		 &nbsp;
		</td>
        <td class="SubHead">
        <asp:CheckBox id="chkUnzip"  Runat="server" CssClass="Normal" Textalign="Right"></asp:CheckBox>
		</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <label style="DISPLAY: none" for="<%=lstFiles.ClientID%>"><%= DotNetZoom.GetLanguage("F_UploadFileList") %></label> 
                <asp:ListBox id="lstFiles" Visible="False" Runat="server" CssClass="NormalTextBox" Width="500px" Rows="5"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left">
                <asp:LinkButton id="cmdRemove" Runat="server" CssClass="CommandButton"></asp:LinkButton>
             &nbsp;   
            </td>
            <td align="left">
             <asp:LinkButton id="cmdCancel" runat="server" Cssclass="CommandButton"></asp:LinkButton>
            </td>
        </tr>
		</asp:placeHolder>
        <tr>
            <td align="justify" colspan="2">
                <asp:Label id="lblMessage" runat="server" cssclass="NormalRed" width="400" enableviewstate="False"></asp:Label></td>
        </tr>
    </tbody>
</table>
</asp:placeHolder>
<asp:placeHolder id="Options" runat="server">
<table align="center">
<tr>
<td colspan="2">
&nbsp;</td>
</tr>
<tr>
<td>
<asp:linkbutton id="cmdSynchronize" runat="server" Cssclass="CommandButton"></asp:linkbutton>
<br><br>
<asp:Label id="lblDiskSpace" runat="server" enableviewstate="False" cssclass="Normal"></asp:Label>
<br>
<br>
<span class="SubHead"><label for="<%=chkUploadRoles.ClientID%>"><%= DotNetZoom.GetLanguage("F_Upload_Auth") %>&nbsp;:&nbsp;</label></span>
<br>
<br>
<asp:checkboxlist id="chkUploadRoles" runat="server" width="600" Font-Size="8pt" Font-Names="Verdana,Arial" cellspacing="0" cellpadding="0" RepeatColumns="2"></asp:checkboxlist>
<br>
<asp:linkbutton cssclass="CommandButton" id="cmdUpdate" runat="server" ></asp:linkbutton>
&nbsp;&nbsp;<asp:linkbutton cssclass="CommandButton" id="cmdCancelOptions" runat="server"></asp:linkbutton>
</td>
</tr>
</table>
</asp:placeHolder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>