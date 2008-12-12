<%@ Control Language="vb" codebehind="ModuleDefinitions.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.ModuleDefinitions" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<script language="JavaScript" type="text/javascript">
		function OpenNewWindow(tabid)
			{
				var m = window.open('admin/tabs/icone.aspx?L=<%= DotNetZoom.GetLanguage("N") %>&tabid=' + tabid , 'icone', 'width=800,height=600,left=100,top=100');
				m.focus();
			}
		function SetUrl(idParentValue)
			{
			  document.getElementById('edit_txticone').value = idParentValue;
			  __doPostBack('edit$cmdView','');
			}
</script>
<table id="tabAddModule" cellspacing="0" cellpadding="4" width="600"  border="0" runat="server">
    <tbody>
        <tr>
            <td class="SubHead" valign="top" width="110">
                <%= DotNetZoom.GetLanguage("MS_Directives") %>: 
            </td>
            <td class="Normal">
			<asp:Label id="lblModuleDef" EnableViewState="false" runat="server" cssclass="Normal" font-size="Small" forecolor="#800040"></asp:Label> 
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="SubHead" width="110">
                <label for="<%=cboModule.ClientID%>"><%= DotNetZoom.GetLanguage("MS_Script") %>:</label> 
            </td>
            <td>
                <asp:DropDownList id="cboModule" CssClass="NormalTextBox" Runat="server"></asp:DropDownList>
                &nbsp; 
                <asp:HyperLink id="cmdUpload" CssClass="CommandButton" Runat="server"></asp:HyperLink>
            </td>
        </tr>
    </tbody>
</table>
<table id="tabEditModule" cellspacing="0" cellpadding="4"  border="0" runat="server">
    <tbody>
        <tr>
            <td class="SubHead" width="128">
                <label for="<%=txtFriendlyName.ClientID%>"><%= DotNetZoom.GetLanguage("MS_ModuleName") %>:</label> 
            </td>
            <td>
                <asp:TextBox id="txtFriendlyName" runat="server" maxlength="150" Columns="30" width="390" cssclass="NormalTextBox"></asp:TextBox>
                <br>
                <asp:RequiredFieldValidator id="valFriendlyName" runat="server" ControlToValidate="txtFriendlyName"  Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="SubHead" valign="top" width="128">
                <label for="<%=txtDescription.ClientID%>"><%= DotNetZoom.GetLanguage("MS_ModuleDescription") %>:</label> 
            </td>
            <td>
                <asp:TextBox id="txtDescription" runat="server" maxlength="2000" Columns="30" width="390" cssclass="NormalTextBox" Rows="10" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="SubHead" style="white-space: nowrap;" width="128">
                <label for="<%=txtDesktopSrc.ClientID%>"><%= DotNetZoom.GetLanguage("MS_SRC_URL") %>:</label> 
            </td>
            <td>
                <asp:TextBox id="txtDesktopSrc" runat="server" maxlength="150" Columns="30" width="390" cssclass="NormalTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="SubHead" style="white-space: nowrap;" width="128">
                <label for="<%=txtEditSrc.ClientID%>"><%= DotNetZoom.GetLanguage("MS_EDIT_URL") %>:</label> 
            </td>
            <td>
                <asp:TextBox id="txtEditSrc" runat="server" maxlength="150" Columns="30" width="390" cssclass="NormalTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="SubHead" style="white-space: nowrap;" width="128">
                <label for="<%=txtHelpSrc.ClientID%>"><%= DotNetZoom.GetLanguage("MS_HELP_URL") %>:</label> 
            </td>
            <td>
                <asp:TextBox id="txtHelpSrc" runat="server" maxlength="150" Columns="30" width="390" cssclass="NormalTextBox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="SubHead" style="white-space: nowrap;">
            <label title="<%= DotNetZoom.GetLanguage("MS_IconeHelp") %>" for="<%=txtIcone.ClientID%>"><%= DotNetZoom.GetLanguage("ms_icone") %>:</label> 
			</td>
            <td colspan="2">
	         <asp:textbox id="txticone" runat="server" cssclass="NormalTextBox" width="300" MaxLength="200"></asp:textbox>
             &nbsp; 
			<asp:hyperlink id="lnkicone" CssClass="CommandButton" runat="server" EnableViewState="false"></asp:hyperlink>
		   &nbsp;&nbsp;<asp:Image Id="MyHtmlImage" runat="server" Visible="false" EnableViewState="false"></asp:Image>
		   </td>
        </tr>
        <tr>
            <td class="SubHead" width="128">
                <label for="<%=chkPremium.ClientID%>"><%= DotNetZoom.GetLanguage("MS_Bonus") %></label> 
            </td>
            <td>
                <asp:CheckBox id="chkPremium" CssClass="NormalTextBox" Runat="server"></asp:CheckBox>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" runat="server"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdCancel" runat="server" CausesValidation="False"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdDelete" runat="server" CausesValidation="False"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>