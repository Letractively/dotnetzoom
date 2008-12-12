<%@ Import namespace="DotNetZoom" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Register TagPrefix="TTT" TagName="UsersControl" Src="../TTTForum/TTT_UsersControl.ascx"%>
<%@ Control language="vb" Inherits="DotNetZoom.TTT_GalleryEditFile" CodeBehind="TTT_GalleryEditFile.ascx.vb" AutoEventWireup="false" %>
<table class="TTTBorder" cellspacing="1" cellpadding="0" width="780" align="center">
	<tbody>
		<tr>
			<td colspan=2 >
				<table cellpadding=0 cellspacing=0 border=0>
					<tr>
						<td class="TTTHeader" align="left" style="white-space: nowrap;" width="90" height=28>
						&nbsp;<span class="TTTHeaderText"><%= DotNetZoom.getlanguage("Gal_EditFile") %></span>&nbsp;:&nbsp;
					</td>
					<td class="TTTHeader" style="white-space: nowrap;" align="left" width=90% height="28">
						<asp:datalist id="dlFolders" repeatdirection="Horizontal" repeatlayout="Flow" runat="server" HorizontalAlign="Left">
							<itemtemplate>
								<asp:hyperlink id="hlFolder" cssClass="TTTAltHeaderText" runat="server" navigateurl='<%# GetFolderURL(Container.DataItem) %>'>
									<%# CType(Container.DataItem, FolderDetail).Name %>
								</asp:hyperlink>
							</itemtemplate>
							<separatortemplate>&nbsp;&raquo;&nbsp;</separatortemplate>
						</asp:datalist>
					</td>
					
					</tr>				
				</table>  			
			</td>			
		</tr>
		<tr>
			<td class="TTTAltHeader" height="28" colspan="2">
				&nbsp;
				<asp:Label id="lblInfo" CssClass="TTTNormal" ForeColor="Red" runat="server"></asp:Label>
			</td>
		</tr>
		<asp:placeholder id="pnlFileDetails" Runat="server">
			<tr>
				<td>
					<table cellSpacing="1" cellPadding="0" width="100%" border="0">
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_URL") %>:</td>
							<td class="TTTRow" align="left">
								<asp:TextBox id="txtPath" runat="server" cssclass="NormalTextBox" Enabled="False" Width="100%"></asp:TextBox></td>
						</tr>
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Name") %>:</td>
							<td class="TTTRow" align="left">
								<asp:TextBox id="txtName" runat="server" cssclass="NormalTextBox" Enabled="False" Width="50%"></asp:TextBox></td>
						</tr>
						<tr>
						
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Prop") %>:</td>
							<td class="TTTRow" align="left">
								<asp:TextBox id="txtOwner" runat="server" Width="50%" cssclass="NormalTextBox" Enabled="False"></asp:TextBox>
								<asp:Button id=btnEditOwner cssclass="button" runat="server" Visible="False" CommandName="edit"></asp:Button>
								<asp:TextBox id="txtOwnerID" runat="server" Width="0" cssclass="NormalTextBox" Visible="False"></asp:TextBox></td></tr>
							<!--Start select owner-->
							<asp:placeholder id=pnlSelectOwner Runat="server" Visible="False">
							<tr>
    						<td class=TTTSubHeader width=120>&nbsp;<%= DotNetZoom.getlanguage("Gal_SelectProp") %>:</td>
    						<td>
							<TTT:USERSCONTROL id=ctlUsers runat="server" ShowEditButton="True" ShowEmail="True" ShowFullName="True" ShowUserName="True" Type="2"></TTT:USERSCONTROL></td></tr>
							</asp:placeholder>
							<!--End select owner-->

						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_TitleI") %>:</td>
							<td class="TTTRow" align="left">
								<asp:TextBox id="txtTitle" runat="server" cssclass="NormalTextBox" Width="50%"></asp:TextBox></td>
						</tr>
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Desc") %>:</td>
							<td class="TTTRow" align="left">
								<asp:TextBox id="txtDescription" TextMode="MultiLine" runat="server" cssclass="NormalTextBox" Width="100%"></asp:TextBox></td>
						</tr>
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Cat") %>:</td>
							<td class="TTTRow" align="left">
								<asp:checkboxlist id="lstCategories" runat="server" RepeatColumns="4" Font-Names="Verdana,Arial" Font-Size="8pt" width="100%"></asp:checkboxlist></td>
						</tr>
					</table>
				</td>
				<td class="TTTRow" valign="middle" align="center" width="150">
					<asp:Image id="imgFile" Runat="server"></asp:Image></td>
			</tr>
		</asp:placeholder>
		<tr valign="middle">
			<td class="TTTAltHeader" align="left" colspan="2">
				&nbsp;
				<asp:button id="CancelButton" cssclass="button" runat="server" CommandName="back"></asp:button>
				&nbsp;
				<asp:button id="UpdateButton" cssclass="button" runat="server" CommandName="back"></asp:button>
				&nbsp;
			</td>
		</tr>
	</tbody>
</table>