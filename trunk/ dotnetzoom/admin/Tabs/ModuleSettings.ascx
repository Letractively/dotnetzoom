<%@ Control codebehind="ModuleSettings.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.ModuleSettingsPage" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="Portal" TagName="ModuleEdit" Src="~/admin/tabs/ModuleEdit.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<script language="JavaScript" type="text/javascript">
		function OpenNewWindow(tabid)
			{
				var m = window.open('<%=DotNetZoom.glbPath %>admin/tabs/icone.aspx?L=<%= DotNetZoom.GetLanguage("N") %>&tabid=' + tabid , 'icone', 'width=800,height=600,left=100,top=100');
				m.focus();
			}
			
		function SetUrl(idParentValue)
			{
			  
			  document.getElementById('edit_txticone').value = idParentValue;
			  __doPostBack('edit$cmdView','');
			}
</script>
<table cellspacing="0" cellpadding="0" width="750" border="0">
    <tbody>
	     <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>

        <tr>
            <td class="SubHead" width="300">
                <label for="<%=txtTitle.ClientID%>"><%= DotNetZoom.GetLanguage("ms_title")%>:</label> 
            </td>
            <td>
                <asp:textbox id="txtTitle" runat="server" cssclass="NormalTextBox" width="300"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="300">
                <label for="<%=ddlLanguage.ClientID%>"><%= DotNetZoom.GetLanguage("ms_language")%>:</label> 
            </td>
            <td>
			<asp:DropDownList id="ddlLanguage" Width="100px" AutoPostBack="False" DataValueField="language" DataTextField="SettingValue" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="SubHead" style="white-space: nowrap;">
            <label title="<%= DotNetZoom.GetLanguage("ms_icone_info")%>" for="<%=txtIcone.ClientID%>"><%= DotNetZoom.GetLanguage("ms_icone")%>:</label> 
			</td>
            <td colspan="2">
	         <asp:textbox id="txticone" runat="server" cssclass="NormalTextBox" width="300" MaxLength="200"></asp:textbox>
             &nbsp; 
			<asp:hyperlink id="lnkicone" CssClass="CommandButton" runat="server" EnableViewState="false"></asp:hyperlink>
		   &nbsp;&nbsp;<asp:Image Id="MyHtmlImage" runat="server" Visible="false" EnableViewState="false"></asp:Image>
		   </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>
        <tr>
            <td class="SubHead" style="white-space: nowrap;" width="300">
                <label for="<%=cboTab.ClientID%>"><%= DotNetZoom.GetLanguage("ms_move_to_tab")%>:</label> 
            </td>
            <td>
                <asp:dropdownlist id="cboTab" width="300" DataTextField="TabName" DataValueField="TabId" CssClass="NormalTextBox" Runat="server"></asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>
        <tr>
            <td class="SubHead" style="white-space: nowrap;" width="300">
                <label for="<%=chkShowTitle.ClientID%>"><%= DotNetZoom.GetLanguage("ms_show_title")%>:</label> 
            </td>
            <td>
                <asp:checkbox id="chkShowTitle" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
            </td>
        </tr>
        <tr>
            <td class="SubHead" style="white-space: nowrap;" width="300">
                <label for="<%=chkAllTabs.ClientID%>"><%= DotNetZoom.GetLanguage("ms_showall_tabs")%>:</label> 
            </td>
            <td>
                <asp:checkbox id="chkAllTabs" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>
        <tr>
            <td class="SubHead" style="white-space: nowrap;" width="300">
                <label for="<%=cboPersonalize.ClientID%>"><%= DotNetZoom.GetLanguage("ms_custom")%>:</label> 
            </td>
            <td>
                <asp:dropdownlist id="cboPersonalize" width="300" CssClass="NormalTextBox" Runat="server">
                    <asp:ListItem Value="0"></asp:ListItem>
                    <asp:ListItem Value="1"></asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="300">
                <label title="<%= DotNetZoom.GetLanguage("ms_cache_info")%>" for="<%=txtCacheTime.ClientID%>"><%= DotNetZoom.GetLanguage("ms_cache")%>:</label> 
            </td>
            <td>
                <asp:textbox id="txtCacheTime" runat="server" cssclass="NormalTextBox" width="300" Columns="10" MaxLength="6"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>
        <tr>
            <td class="SubHead" valign="top" width="300">
                <%= DotNetZoom.GetLanguage("ms_see")%>:<br>
                (<span style="FONT-SIZE: 8pt;"><%= DotNetZoom.GetLanguage("ms_see_info")%></span>&nbsp;) 
            </td>
            <td>
                <asp:checkboxlist id="chkAuthViewRoles" runat="server" width="300" Font-Size="8pt" Font-Names="Verdana,Arial" cellspacing="0" cellpadding="0" RepeatColumns="2"></asp:checkboxlist>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>
        <tr>
            <td class="SubHead" valign="top" width="300">
                <%= DotNetZoom.GetLanguage("ms_mod")%>: 
            </td>
            <td>
                <asp:checkboxlist id="chkAuthEditRoles" runat="server" width="300" Font-Size="8pt" Font-Names="Verdana,Arial" cellspacing="0" cellpadding="0" RepeatColumns="2"></asp:checkboxlist>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>
        <tr>
            <td class="SubHead" valign="top" width="300">
            <%= DotNetZoom.GetLanguage("ms_contener")%>:
			</td>
            <td>
                <table cellspacing="0" cellpadding="4" border="0" width="400">
                    <tbody>
					<tr>
					<td colspan="3">
					<portal:ModuleEdit id="ContainerEdit" runat="server"></portal:ModuleEdit>
					</td>
					</tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>
        <tr>
            <td valign="bottom" colspan="2">
                <asp:linkbutton class="CommandButton" id="cmdUpdate" runat="server" Text="Update"></asp:linkbutton>
                &nbsp;&nbsp; 
                <asp:linkbutton class="CommandButton" id="cmdCancel" runat="server" Text="Cancel"></asp:linkbutton>
                &nbsp;&nbsp; 
                <asp:linkbutton class="CommandButton" id="cmdDelete" runat="server" Text="Delete"></asp:linkbutton>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>
    </tbody>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>