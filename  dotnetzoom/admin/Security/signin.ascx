<%@ Control EnableViewState="true" Language="vb" Inherits="DotNetZoom.Signin" codebehind="Signin.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>


<table width="100%"  cellspacing="0" cellpadding="3" border="0"><tr><td align="center" valign="middle">
<table width="500" style="border:2px solid #a1a1a1;padding:10px 10px;border-radius:25px;-moz-border-radius:25px;box-shadow: 10px 10px 5px #888888;"><tr><td align="left">
<asp:literal id="SignInTitlebefore" Visible="False" runat="server" EnableViewState="false" ></asp:literal>
<table class='<asp:literal id="TableTitle" runat="server" />' width="100%" cellspacing="0" cellpadding="3" border="0">
    <tbody>
	   <tr>
		<td align="left" valign="middle" >
		<asp:HyperLink EnableViewState="true" ID="help" visible="false" runat="server">
		<img height="12" width="12" border="0" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -362px;">
		</asp:HyperLink>
		</td>
	   <td align="center" valign="middle">
	   <span class='<asp:literal id="ItemTitle" runat="server" />'><%= DotNetZoom.getlanguage("Label_enter") %></span>
	   </td>
	   <td runat="server" id="exitTD" align="right" valign="middle"><a title="<%= DotNetZoom.GetLanguage("admin_menu_hide")%>" href="javascript:toggleBox('signin',0)" >
	   <img height="14" width="14" border="0" src="/images/1x1.gif" Alt="ca" title="<%= DotNetZoom.GetLanguage("Signin_hide")%>" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -333px;"></a>
	   </td>
	   </tr>
	</tbody>
</table>
<asp:literal id="SignInTitleafter" Visible="False" runat="server" EnableViewState="false" ></asp:literal>
<asp:literal id="SignInbefore" Visible="False" runat="server" EnableViewState="false" ></asp:literal>
<table width="100%" cellspacing="0" cellpadding="3" border="0">
    <tbody>
        <tr>
            <td valign="middle" style="white-space: nowrap;">
                <label class="NormalBold" for="<%=txtUsername.ClientID%>"><%= DotNetZoom.getlanguage("Label_UserName") %> :</label>
            </td>
			<td valign="middle">
                <asp:TextBox id="txtUsername" runat="server" columns="9" width="130" cssclass="NormalTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" style="white-space: nowrap;">
                <label class="NormalBold" for="<%=txtPassword.ClientID%>"><%= DotNetZoom.getlanguage("Label_Password") %> :</label>
            </td>
            <td valign="middle">
                <asp:TextBox id="txtPassword" runat="server" columns="9" width="130" cssclass="NormalTextBox" textmode="password"></asp:TextBox>
            </td>

        </tr>
        <tr id="rowVerification1" runat="server" visible="false">
            <td valign="middle">
                <label class="NormalBold" for="<%=txtVerification.ClientID%>"><%= DotNetZoom.getlanguage("Label_ValidCode") %> :</label>
            </td>
            <td valign="middle">
                <asp:TextBox id="txtVerification" runat="server" columns="9" width="130" cssclass="NormalTextBox"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td valign="middle" colspan="2">
                <asp:checkbox cssclass="Normal" id="chkCookie" runat="server"></asp:checkbox>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="center" colspan="2">
                <asp:Button cssclass="button"  CausesValidation="false" id="cmdLogin" runat="server" ></asp:Button>
                &nbsp;
                <asp:Button cssclass="button"  CausesValidation="false" id="cmdRegister" runat="server" ></asp:Button>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="center" colspan="2">
                <asp:Button cssclass="button"  CausesValidation="false"  id="cmdSendPassword" runat="server" ></asp:Button>
            </td>
        </tr>
        <tr>
            <td valign="middle" colspan="2">
                <asp:Label id="lblMessage" runat="server" cssclass="NormalRed"></asp:Label></td>
        </tr>
        <tr>
            <td valign="middle" colspan="2">
                <asp:Label id="lblLogin" runat="server" cssclass="Normal"></asp:Label></td>
        </tr>
    </tbody>
</table>
<asp:literal id="SignInafter" Visible="False" runat="server" EnableViewState="false" ></asp:literal>
</td></tr></table>
</td></tr></table>
