<%@ Control CodeBehind="ModuleEdit.ascx.vb" language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetZoom.ModuleEdit" %>
<input type="hidden" id="PassBack" name="PassBack" value="">
<script language="JavaScript" type="text/javascript">
		function OpenColorWindow(tabid)
			{
				var m = window.open('<%=DotNetZoom.glbPath %>admin/tabs/colorselector.aspx?L=<%= DotNetZoom.GetLanguage("N") %>' , 'color', 'width=400,height=330,left=100,top=100');
				m.focus();
			}

		function Setcolor(idParentValue)
			{
			  document.getElementById('<%= GettxtcolorID %>').value = idParentValue;
			   __doPostBack('<%= GetPostBack %>','');
			}
		function OpenNewContainerWindow(tabid)
			{
				var m = window.open('<%=DotNetZoom.glbPath %>admin/tabs/container.aspx?L=<%= DotNetZoom.GetLanguage("N") %>&tabid=' + tabid , 'icone', 'width=800,height=600,left=100,top=100,scrollbars=yes');
				m.focus();
			}
		function SetContainer(idParentValue)
			{			  
			  document.getElementById('PassBack').value = idParentValue;
			 __doPostBack('<%= GetPostBack %>','');
			}

</script>
<table cellspacing="0" cellpadding="0" width="100%">
	<tr>
		<td colspan="3" valign="middle" style="border: thin; border-style: dotted; border-color: blue; padding: 5">
		<asp:Label id="lblMessage" Visible="False" runat="server" cssclass="NormalRed" enableviewstate="False"></asp:Label>
		<asp:literal id="lblPreview" runat="server"></asp:literal>
		</td>
	</tr>
	<tr>
		<td height="30" colspan="3" align="center" valign="middle">
		<asp:linkbutton id="cmdEdit" CssClass="CommandButton" Runat="server"></asp:linkbutton>
		<asp:hyperlink id="lnkcontainer" CssClass="CommandButton" runat="server"></asp:hyperlink>
		&nbsp;&nbsp;<asp:linkbutton id="cmdSee" CssClass="CommandButton" Runat="server"></asp:linkbutton>
		</td>
	</tr>
	<asp:placeholder id="rowContainer" runat="server">
	<tr>
		<td>
		<label class="SubHead" for="<%=cboAlign.ClientID%>"><%= DotNetZoom.GetLanguage("ms_contener_setup")%>:</label></td>
		<td>
		<asp:dropdownlist id="cboAlign" CssClass="NormalTextBox" Runat="server">
 			<asp:ListItem Value=""></asp:ListItem>
 			<asp:ListItem Value="left"></asp:ListItem>
 			<asp:ListItem Value="center"></asp:ListItem>
 			<asp:ListItem Value="right"></asp:ListItem>
		</asp:dropdownlist>
		</td>
	</tr>
	<tr>
		<td><label class="SubHead" for="<%=txtColor.ClientID%>"><%= DotNetZoom.GetLanguage("ms_color")%>:</label></td>
		<td><asp:textbox id="txtColor" CssClass="NormalTextBox" Runat="server" Columns="7"></asp:textbox>
		&nbsp;&nbsp;<asp:hyperlink id="lnkcolor" CssClass="CommandButton" runat="server"></asp:hyperlink>
		</td>
	</tr>
    <tr>
		<td><label class="SubHead" for="<%=txtBorder.ClientID%>"><%= DotNetZoom.GetLanguage("ms_margin")%>:</label></td>
		<td><asp:textbox id="txtBorder" CssClass="NormalTextBox" Runat="server" Columns="1"></asp:textbox>
		</td>
	</tr>
	<asp:placeholder id="rowCSS" runat="server">
        <tr>
            <td>
			<label class="SubHead" for="<%=txtCSSClass.ClientID%>"><%= DotNetZoom.GetLanguage("Title_class")%>:</label></td>
			<td>
			<asp:textbox id="txtCSSClass" CssClass="NormalTextBox" Runat="server" Columns="20"></asp:textbox>
            </td>
		</tr>
		<tr>
			<td>
			<label class="SubHead" for="<%=TitleHeaderClass.ClientID%>"><%= DotNetZoom.GetLanguage("TitleHeader_class")%>:</label></td>
			<td>
			<asp:textbox id="TitleHeaderClass" CssClass="NormalTextBox" Runat="server" Columns="20"></asp:textbox>
            </td>
        </tr>	
	<tr>
		<td colspan="2">&nbsp; 
		</td>
 	</tr>
 	<tr>
		<td colspan="3" align="center" valign="top">
		<asp:RadioButtonList id="optContainer" width="300" CssClass="NormalTextBox" Runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
			<asp:ListItem Value="A"></asp:ListItem>
			<asp:ListItem Value="B" Selected="True"></asp:ListItem>
			<asp:ListItem Value="C"></asp:ListItem>
 		</asp:RadioButtonList>
		</td>
	</tr>
	</asp:placeholder>
 	<tr>
		<td colspan="3" valign="middle">
		<label class="SubHead" style="DISPLAY: none" for="<%=txtContainer.ClientID%>">Conteneur HTML</label> 
		<asp:textbox id="txtContainer" Width="380" CssClass="NormalTextBox" Runat="server" MaxLength="4000" TextMode="MultiLine" Rows="8"></asp:textbox>
		<asp:textbox Visible="False" id="ModuleContener" Width="380" CssClass="NormalTextBox" Runat="server" MaxLength="4000" TextMode="MultiLine" Rows="8"></asp:textbox>
		<asp:textbox Visible="False" id="TitleContener" Width="380" CssClass="NormalTextBox" Runat="server" MaxLength="4000" TextMode="MultiLine" Rows="8"></asp:textbox>
		<asp:placeholder id="GlobalDefault" visible="false" runat="server">
		<br>
		<label Title="<%= DotNetZoom.GetLanguage("ms_contener_default_info")%>" class="SubHead" for="<%=chkDefault.ClientID%>"><%= DotNetZoom.GetLanguage("ms_contener_default")%></label>&nbsp;<asp:checkbox id="chkDefault" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
		<asp:placeholder id="Globalplaceholder" visible="false" runat="server">
		<label Title="<%= DotNetZoom.GetLanguage("ms_contener_global_info")%>" class="SubHead" for="<%=chkGlobal.ClientID%>"><%= DotNetZoom.GetLanguage("ms_contener_global")%></label>&nbsp;<asp:checkbox id="chkGlobal" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
		</asp:placeholder>
		</asp:placeholder>
		</td>
	</tr>
        <tr>
            <td height="30" valign="bottom" colspan="3">
                <asp:linkbutton class="CommandButton" id="cmdUpdate" runat="server"></asp:linkbutton>
                &nbsp;&nbsp; 
                <asp:linkbutton class="CommandButton" id="cmdCancel" runat="server"></asp:linkbutton>
                &nbsp;&nbsp; 
                <asp:linkbutton class="CommandButton" id="cmdDelete" runat="server"></asp:linkbutton>
            </td>
        </tr>
</asp:placeholder>
</table>