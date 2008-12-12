<%@ Control Inherits="DotNetZoom.BulkEmail" codebehind="BulkEmail.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table id="TableMessage" runat="server" align="center" cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=cboRoles.ClientID%>"><%= DotNetZoom.GetLanguage("Mail_Portal_Role") %>:</label></td>
            <td width=*>
                <asp:DropDownList id="cboRoles" runat="server" CssClass="NormalTextBox" DataTextField="RoleName" DataValueField="RoleID" Width="448px"></asp:DropDownList>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtEmail.ClientID%>"><%= DotNetZoom.GetLanguage("address_Email") %>:</label></td>
            <td>
                <asp:TextBox id="txtEmail" runat="server" CssClass="NormalTextBox" width="700" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
			
                <label for="<%=txtSubject.ClientID%>"><%= DotNetZoom.GetLanguage("UO_Object") %>:</label></td>
            <td>
                <asp:TextBox id="txtSubject" runat="server" cssclass="NormalTextBox" width="700" columns="40" maxlength="100"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td>
            </td>
            <td valign="middle">
                <span class="normal"><%= DotNetZoom.GetLanguage("Mail_Info") %></span> 
                <asp:RadioButtonList id="optView" CssClass="NormalTextBox" Runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                    <asp:ListItem Value="B"></asp:ListItem>
                    <asp:ListItem Value="R" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <%= DotNetZoom.GetLanguage("UO_Message") %>:</td>
            <td>
                <asp:placeholder id="pnlBasicTextBox" Runat="server" Visible="False">
                    <asp:TextBox id="txtMessage" runat="server" CssClass="NormalTextBox" width="700" columns="75" textmode="multiline" rows="25"></asp:TextBox>
                </asp:placeholder>
                <asp:placeholder id="pnlRichTextBox" Runat="server" Visible="True">
				<FCKeditorV2:FCKeditor id="FCKeditor1" BasePath="~/FCKeditor/" runat="server"></FCKeditorV2:FCKeditor>
				</asp:placeholder>
            </td>
        </tr>
        <tr valign="top">
            <td>
                &nbsp;</td>
            <td>
			    <span  class="SubHead"><%= DotNetZoom.GetLanguage("Mail_Add_File") %>:</span><br>
                <asp:dropdownlist id="cboAttachment" runat="server" CssClass="NormalTextBox" DataTextField="Text" DataValueField="Value" Width="200px"></asp:dropdownlist>
                &nbsp; 
                <asp:HyperLink id="cmdUpload" CssClass="CommandButton" Runat="server"></asp:HyperLink>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td>
                &nbsp;</td>
            <td>
                <asp:LinkButton class="CommandButton" id="btnSend" runat="server"></asp:LinkButton>
            </td>
        </tr>
    </tbody>
</table>
<table id="TableSendTo" width="100%" runat="server" align="center" cellspacing="0" cellpadding="2" border="0">
    <tbody>
	    <tr valign="top">
            <td class="Head">
            <%= DotNetZoom.GetLanguage("Mail_Send_To") %>
			<hr noshade="noshade" size="1" />
			</td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
			<asp:literal id="SendTo" runat="server"></asp:literal>
            </td>
        </tr>
	    <tr valign="top">
            <td class="Head">
			<hr noshade="noshade" size="1" />
			</td>
        </tr>
		<tr Align="left" valign="top">
            <td>
                <asp:LinkButton class="CommandButton" id="btnSendAnother" runat="server"></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton class="CommandButton" id="btnReturn" runat="server"></asp:LinkButton>
	        </td>
        </tr>

    </tbody>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>