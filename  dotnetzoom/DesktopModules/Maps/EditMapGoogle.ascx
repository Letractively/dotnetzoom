<%@ Control Language="vb" codebehind="EditMapGoogle.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditMapGoogle" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="Portal" TagName="Address" Src="~/controls/Address.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<script language="JavaScript" type="text/javascript">
		function OpenNewWindow(tabid)
			{
				var m = window.open('<%=dotnetzoom.glbpath%>admin/tabs/icone.aspx?L=<%= DotNetZoom.GetLanguage("N") %>&tabid=' + tabid , 'icone', 'width=800,height=600,left=100,top=100');
				m.focus();
			}
			
		function SetUrl(idParentValue)
			{
			  
			  document.getElementById('edit_txticone').value = idParentValue;
			  __doPostBack('edit$cmdUpdate','');
			}
</script>
<table cellspacing="0" cellpadding="0" width="750">
    <tbody>
        <tr>
            <td width="340" valign="top">
<table cellspacing="0" cellpadding="0" width="100%">
    <tbody>
        <tr valign="bottom">
            <td class="SubHead" width="90">
                <label for="<%=txtLocation.ClientID%>"><%= DotNetZoom.getlanguage("MapGoogle_location") %>:</label>&nbsp; 
            </td>
            <td align="left">
                <asp:TextBox id="txtLocation" Width="250px" Columns="30" CssClass="NormalTextBox" Runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" id="valLocation" ControlToValidate="txtLocation" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr valign="bottom">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr valign="bottom">
            <td valign="top" colspan="2">
                <portal:address id="Address1" runat="server"></portal:address>
            </td>
        </tr>
        <tr valign="bottom">
            <td colspan="2">
                &nbsp;</td>
        </tr>

		<tr valign="bottom">
            <td class="SubHead" width="90">
                <label class="SubHead" for="<%=txtLATLONG.ClientID%>"><%= DotNetZoom.getlanguage("GoogleLatLong") %></label>&nbsp; 
            </td>
            <td align="left">
                <asp:TextBox id="txtLATLONG" Width="250px" Columns="30" CssClass="NormalTextBox" Runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" id="valLatLong" ControlToValidate="txtLATLONG" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
		</tr>
		<tr valign="bottom">
            <td class="SubHead" width="90">
             &nbsp; 
            </td>
            <td align="left">
		    <asp:LinkButton cssclass="CommandButton" id="cmdLatLong" runat="server" ></asp:LinkButton>
            </td>
		</tr>
        <tr valign="bottom">
            <td colspan="2">
                &nbsp;</td>
        </tr>
		<tr valign="bottom">
            <td class="SubHead" width="90">
                <label class="SubHead" for="<%=txtgoogleAPI.ClientID%>"><%= DotNetZoom.getlanguage("GoogleAPI") %></label>&nbsp; 
            </td>
            <td align="left">
                <asp:TextBox id="txtgoogleAPI" Width="250px" Columns="30" CssClass="NormalTextBox" Runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator id="valgoogleAPI" runat="server" ControlToValidate="txtgoogleAPI" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr valign="bottom">
            <td colspan="2">
                &nbsp;</td>
        </tr>
		<tr valign="bottom">
            <td class="SubHead" width="90">&nbsp;
            </td>
            <td align="left">
				<asp:hyperlink id="hypgetAPI" runat="server" EnableViewState="false" CssClass="CommandButton"></asp:hyperlink>
            </td>
        </tr>
    </tbody>
</table>
</td>
<td class="SubHead" valign="top" width="410">
<table cellspacing="0" cellpadding="5" width="100%">
    <tbody>
        <tr>
            <td colspan="2">
			<asp:literal id="map" runat="server" EnableViewState="false" ></asp:literal>
			</td>
		</tr>
		<tr valign="bottom">
            <td class="SubHead" align="left" colspan="2">
			<asp:CheckBox id="chkDisplayMap" CssClass="NormalTextBox" Runat="server"></asp:CheckBox>
            </td>
		</tr>
        <tr valign="bottom" colspan="2">
            <td class="SubHead" align="left" colspan="2">
			<asp:CheckBox id="chkDisplayResize" CssClass="NormalTextBox" Runat="server"></asp:CheckBox>
            </td>
		</tr>
        <tr valign="bottom">
            <td class="SubHead" align="left" colspan="2">
			<asp:CheckBox id="chkDisplayType" CssClass="NormalTextBox" Runat="server"></asp:CheckBox>
            </td>
		</tr>
        <tr valign="bottom">
            <td class="SubHead" align="left" colspan="2">
			<asp:CheckBox id="chkDisplayPointer" CssClass="NormalTextBox" Runat="server"></asp:CheckBox>
            </td>
		</tr>
        <tr>
         <td class="SubHead" valign="top">
            <label title="<%= DotNetZoom.GetLanguage("ms_icone_info")%>" for="<%=txtIcone.ClientID%>"><%= DotNetZoom.GetLanguage("ms_icone")%>:</label> 
		</td>
        <td valign="top">
	         <asp:textbox id="txticone" runat="server" cssclass="NormalTextBox" width="200" MaxLength="200"></asp:textbox>
             &nbsp; 
			<asp:hyperlink id="lnkicone" CssClass="CommandButton" runat="server" EnableViewState="false"></asp:hyperlink>
		</td>
        </tr>
        <tr valign="top">
            <td class="SubHead" align="left" colspan="2">
		    <asp:Image Id="MyHtmlImage" runat="server" Visible="false" EnableViewState="false"></asp:Image>
            </td>
		</tr>
		<tr valign="bottom">
            <td align="left" colspan="2">
                <asp:TextBox id="txtScript" Width="400px" TextMode="MultiLine" Rows="3" CssClass="NormalTextBox" Runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" id="valScript" ControlToValidate="txtScript" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
		</tr>
		<tr valign="bottom">
            <td align="left" colspan="2">
		    <asp:LinkButton cssclass="CommandButton" id="cmdGenerateScript" runat="server" ></asp:LinkButton>
            </td>
		</tr>
    </tbody>
</table>
</td>
</tr>
</tbody>
</table>
<p align="left">
    <asp:LinkButton cssclass="CommandButton" id="cmdUpdate" runat="server" ></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton cssclass="CommandButton" id="cmdCancel" runat="server"  CausesValidation="False"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>