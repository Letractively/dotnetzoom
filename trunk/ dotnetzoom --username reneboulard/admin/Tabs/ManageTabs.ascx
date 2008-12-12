<%@ Control Language="vb" autoeventwireup="false" Explicit="True" codebehind="ManageTabs.ascx.vb" Inherits="DotNetZoom.ManageTabs" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:PlaceHolder ID="PlaceHolder1" runat="server">
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
<table cellspacing="0" cellpadding="0" width="750" border="0">
    <tbody>
        <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="100">
                <%= DotNetZoom.GetLanguage("ts_tabname")%>:</td>
            <td>
                <asp:textbox id="tabName" runat="server" cssclass="NormalTextBox" width="300"></asp:textbox>
                <asp:RequiredFieldValidator id="valtabName" runat="server" Display="Dynamic" CssClass="NormalRed" ControlToValidate="tabName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="rowlanguage" runat="server">
            <td class="SubHead" width="200">
            <%= DotNetZoom.GetLanguage("ts_tabname")%>&nbsp;<asp:DropDownList id="ddlLanguage" Width="100px" AutoPostBack="True" DataValueField="language" DataTextField="SettingValue" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>:</td>
            <td>
         <asp:textbox id="ltabName" runat="server" cssclass="NormalTextBox" width="300"></asp:textbox>
	     &nbsp;&nbsp;<asp:linkbutton cssclass="CommandButton" id="cmdUpdateName" runat="server" Text="Update"></asp:linkbutton>
            </td>
        </tr>

        <tr>
            <td class="SubHead">
            <label title="<%= DotNetZoom.GetLanguage("select_icone_tooltip")%>" for="<%=txtIcone.ClientID%>"><%= DotNetZoom.GetLanguage("ts_icone")%>:</label> 
			</td>
            <td colspan="2">
	         <asp:textbox id="txticone" runat="server" cssclass="NormalTextBox" width="300" MaxLength="200"></asp:textbox>
             &nbsp; 
			<asp:hyperlink id="lnkicone" CssClass="CommandButton" runat="server" EnableViewState="false"></asp:hyperlink>
		   &nbsp;&nbsp;<asp:Image Id="MyHtmlImage" runat="server" Visible="false" EnableViewState="false"></asp:Image>
		   </td>
        </tr>
						
        <tr>
            <td class="SubHead" width="100">
                <label for="<%=cboTab.ClientID%>"><%= DotNetZoom.GetLanguage("ts_parenttab")%>:</label> 
            </td>
            <td>
                <asp:DropDownList id="cboTab" width="300" CssClass="NormalTextBox" DataTextField="TabName" DataValueField="TabId" Runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="100">
                <label for="<%=cbocss.ClientID%>"><%= DotNetZoom.GetLanguage("ts_css")%>:</label> 
            </td>
            <td>
                <asp:DropDownList id="cbocss" AutoPostBack="True" width="300" CssClass="NormalTextBox" DataTextField="Text" DataValueField="Value" Runat="server"></asp:DropDownList>
            &nbsp;
			<asp:hyperlink cssclass="CommandButton" id="EditCSS" runat="server" />
			</td>
        </tr>
   		<tr>
            <td class="SubHead" width="100">
                <label for="<%=cboskin.ClientID%>"><%= DotNetZoom.GetLanguage("ts_skin")%>:</label> 
            </td>
            <td>
                <asp:DropDownList id="cboskin" AutoPostBack="True" width="300" CssClass="NormalTextBox" DataTextField="Text" DataValueField="Value" Runat="server"></asp:DropDownList>
			&nbsp;
			<asp:Hyperlink cssclass="CommandButton"  id="Editskin" runat="server" />
			</td>
        </tr>
        <tr id="StyleRow" runat="server" visible="false">
		    <td class="SubHead" width="100">
                <label for="<%=chkStyleMenu.ClientID%>"><%= DotNetZoom.GetLanguage("ts_editmenuparam")%>:</label> 
            </td>
           <td>
                <asp:checkbox id="chkStyleMenu" CssClass="NormalTextBox" Runat="server" AutoPostBack="True"></asp:checkbox>
        	</td>
		</tr>
        <tr id="rowTemplate" runat="server">
            <td class="SubHead" width="100">
                <label for="<%=cboTemplate.ClientID%>"><%= DotNetZoom.GetLanguage("ts_tabtemplate")%>:</label> 
            </td>
            <td>
                <asp:DropDownList id="cboTemplate" width="300" CssClass="NormalTextBox" DataTextField="TabName" DataValueField="TabId" Runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr id="rowTemplate1" runat="server">
            <td class="SubHead" width="100">
                <label for="<%=cboTemplate1.ClientID%>"><%= DotNetZoom.GetLanguage("ts_xmltemplate")%>:</label> 
            </td>
            <td>
                <asp:DropDownList id="cboTemplate1" width="300" CssClass="NormalTextBox" Runat="server"></asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td class="SubHead" style="white-space: nowrap;">
                <label title="<%= DotNetZoom.GetLanguage("ts_visibleinfo")%>" for="<%=IsVisible.ClientID%>"><%= DotNetZoom.GetLanguage("ts_visible")%></label></td>
            <td>
                <asp:checkbox id="IsVisible" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
            </td>
        </tr>
        
        <tr>
            <td class="SubHead">
                <label title="<%= DotNetZoom.GetLanguage("ts_disableinfo")%>" for="<%=DisableLink.ClientID%>"><%= DotNetZoom.GetLanguage("ts_disable")%></label></td>
            <td>
                <asp:checkbox id="DisableLink" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtLeftPaneWidth.ClientID%>"><%= DotNetZoom.GetLanguage("ts_tableleftwidth")%>:</label></td>
            <td>
                <asp:textbox id="txtLeftPaneWidth" runat="server" cssclass="NormalTextBox" width="30" MaxLength="5"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtRightPaneWidth.ClientID%>"><%= DotNetZoom.GetLanguage("ts_tablerightwidth")%>:</label></td>
            <td>
                <asp:textbox id="txtRightPaneWidth" runat="server" cssclass="NormalTextBox" width="30" MaxLength="5"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="SubHead">
                <%= DotNetZoom.GetLanguage("ts_TabAdminRole")%>: 
            </td>
            <td>
                <asp:checkboxlist id="adminRoles" runat="server" width="300" Font-Size="8pt" Font-Names="Verdana,Arial" RepeatColumns="2"></asp:checkboxlist>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp; 
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <%= DotNetZoom.GetLanguage("ts_TabViewRole")%>: 
            </td>
            <td>
                <asp:checkboxlist id="authRoles" runat="server" width="300" Font-Size="8pt" Font-Names="Verdana,Arial" RepeatColumns="2"></asp:checkboxlist>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:linkbutton cssclass="CommandButton" id="cmdUpdate" runat="server"></asp:linkbutton>
    &nbsp;&nbsp; 
    <asp:linkbutton cssclass="CommandButton" id="cmdCancel" runat="server"  CausesValidation="False"></asp:linkbutton>
    &nbsp;&nbsp; 
    <asp:linkbutton cssclass="CommandButton" id="cmdDelete" runat="server"  CausesValidation="False"></asp:linkbutton>
    &nbsp;&nbsp; 
    <asp:linkbutton cssclass="CommandButton" id="cmdXML" runat="server"  CausesValidation="False"></asp:linkbutton>
</p>
</asp:PlaceHolder>
<asp:PlaceHolder ID="PlaceHolder2" runat="server">
<asp:CheckBoxList ID="CheckBoxList1" runat="server" width="600" Font-Size="8pt" Font-Names="Verdana,Arial" cellspacing="0" cellpadding="0" RepeatColumns="4">
</asp:CheckBoxList>
<table cellspacing="0" cellpadding="0" width="750" border="0">
    <tbody>
        <tr>
		<asp:textbox id="XMLTextBox" runat="server" cssclass="NormalTextBox" TextMode="MultiLine" width="750px" height="500px"></asp:textbox>
        </tr>
    </tbody>
</table>

<p align="left">
    <asp:linkbutton cssclass="CommandButton" id="cmdCancel1" runat="server" CausesValidation="False"></asp:linkbutton>
    &nbsp;&nbsp; 
    <asp:linkbutton cssclass="CommandButton" id="cmdXML2" runat="server"  CausesValidation="False"></asp:linkbutton>
    &nbsp;&nbsp; 
    <asp:linkbutton cssclass="CommandButton" id="cmdXML3" runat="server"  CausesValidation="False"></asp:linkbutton>
    <asp:textbox id="XMLsaveBox" runat="server" cssclass="NormalTextBox" width="300"></asp:textbox>
    &nbsp;&nbsp;
    <asp:linkbutton cssclass="CommandButton" id="cmdXML4" runat="server"  CausesValidation="False"></asp:linkbutton>
</p>
</asp:PlaceHolder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>