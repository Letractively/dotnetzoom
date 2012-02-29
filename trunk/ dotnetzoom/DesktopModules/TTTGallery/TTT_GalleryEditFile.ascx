<%@ Import namespace="DotNetZoom" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Register TagPrefix="TTT" TagName="UsersControl" Src="../TTTForum/TTT_UsersControl.ascx"%>
<%@ Control language="vb" Inherits="DotNetZoom.TTT_GalleryEditFile" CodeBehind="TTT_GalleryEditFile.ascx.vb" AutoEventWireup="false" %>
<table class="TTTBorder" cellspacing="1" cellpadding="0" width="780" align="center">
	<tbody>
		<tr>
			<td colspan="3" >
				<table cellpadding=0 cellspacing=0 border=0>
					<tr>
						<td class="TTTHeader" align="left" style="white-space: nowrap;" width="90" height=28>
						&nbsp;<span class="TTTHeaderText"><%= DotNetZoom.getlanguage("Gal_EditFile") %></span>&nbsp;:&nbsp;
					</td>
					<td class="TTTHeader" style="white-space: nowrap;" align="left" width="690" height="28">
						<asp:datalist id="dlFolders" repeatdirection="Horizontal" repeatlayout="Flow" runat="server" HorizontalAlign="Left">
							<itemtemplate>
								<asp:hyperlink id="hlFolder" cssClass="TTTAltHeaderText" runat="server" navigateurl='<%# GetFolderURL(Container.DataItem) %>'>
									<%# CType(Container.DataItem, FolderDetail).Name %>
								</asp:hyperlink>
							</itemtemplate>
							<separatortemplate>&nbsp;/&nbsp;</separatortemplate>
						</asp:datalist>
					</td>
					
					</tr>				
				</table>  			
			</td>			
		</tr>
		<tr>
			<td class="TTTAltHeader" height="28" colspan="3">
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
								<asp:TextBox id="txtPath" runat="server" cssclass="NormalTextBox" Enabled="False" Width="50%"></asp:TextBox></td>
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
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("OrderBy") %></td>
									<td class="TTTRow" align="left">
									<asp:TextBox id="txtSortOrder" runat="server" Width="50%" cssclass="NormalTextBox"></asp:TextBox></td>
						</tr>
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("WaterMark")%></td>
									<td class="TTTRow" align="left">
									<asp:TextBox id="txtWaterMark" runat="server" Width="50%" cssclass="NormalTextBox"></asp:TextBox></td>
						</tr>

						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Desc") %>:</td>
							<td class="TTTRow" align="left">
								<asp:TextBox id="txtDescription" TextMode="MultiLine" runat="server" cssclass="NormalTextBox" Width="50%"></asp:TextBox></td>
						</tr>
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Cat") %>:</td>
							<td class="TTTRow" align="left">
								<asp:DropDownList ID="ddCategories" runat=server></asp:DropDownList></td>
						</tr>
					</table>
				</td>
				<td class="TTTRow" valign="middle" align="center" width="150">
                <asp:HyperLink ID="magicfile" runat="server">
					<asp:Image id="imgFile" Runat="server"></asp:Image></asp:HyperLink></td>
                    <td class="TTTRow" valign="middle" align="center">
                        <table><tr><td>
    <asp:Image id="ImgFileIcon" AlternateText="gpsicon" EnableViewState="True" Runat="server"></asp:Image>
    </td><td>
    <div id="Div1" EnableViewState="True" Runat="server" style="padding:0px; height:97px;  width:49px; :auto; overflow-x:hidden; ">
      <asp:datalist id="gpsIconImage" runat="server" EnableViewState="True" cssclass="NormalBold"  RepeatDirection="Vertical" cellpadding="0" width="100%">
        <ItemStyle BorderWidth="0" horizontalalign="Center" verticalalign="Middle"></ItemStyle>
        <ItemTemplate>
         <asp:ImageButton ID="ImgThumb" runat="server" Width="24" Height="24" AlternateText="<%# CType(Container.DataItem, FolderDetail).Name %>" BorderWidth="0"  CommandArgument="<%# CType(Container.DataItem, FolderDetail).url %>" imageurl="<%# CType(Container.DataItem, FolderDetail).url %>"/>
        </ItemTemplate>
        </asp:datalist>
    </div>
    </td></tr></table>

                    </td>
			</tr>
		</asp:placeholder>
		<tr valign="middle">
			<td class="TTTAltHeader" align="left" colspan="3">
				&nbsp;
				<asp:button id="CancelButton" cssclass="button" runat="server" CommandName="back"></asp:button>
				&nbsp;
				<asp:button id="UpdateButton" cssclass="button" runat="server" CommandName="back"></asp:button>
				&nbsp;
			</td>
		</tr>
	</tbody>
</table>