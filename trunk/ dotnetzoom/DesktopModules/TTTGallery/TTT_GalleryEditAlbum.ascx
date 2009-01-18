<%@ Import namespace="DotNetZoom" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Register TagPrefix="TTT" TagName="UsersControl" Src="../TTTForum/TTT_UsersControl.ascx"%>
<%@ Control language="vb" Inherits="DotNetZoom.TTT_GalleryEditAlbum" CodeBehind="TTT_GalleryEditAlbum.ascx.vb" AutoEventWireup="false" %>
<table class="TTTBorder" cellSpacing="1" cellPadding="0" width="780" align="center">
	<asp:placeholder ID="pnlMain" Runat="server">
			<tr>
				<td width="100%" height="28">
					<table cellSpacing="0" cellPadding="3" width="100%" border="0">
						<tr>
							<td class="TTTHeader" align="left" width="120" height="28">&nbsp;
								<asp:label id="lblHeader" Runat="server"></asp:label>:</td>
							<td class="TTTHeader" align="left" height="28">
								<asp:datalist id="dlFolders" HorizontalAlign="Left" runat="server" repeatlayout="Flow" repeatdirection="Horizontal">
									<itemtemplate>
										<asp:hyperlink id="hlFolder" cssClass="TTTAltHeaderText" runat="server" navigateurl='<%# GetFolderURL(Container.DataItem) %>'>
											<%# CType(Container.DataItem, FolderDetail).Name %>
										</asp:hyperlink>
									</itemtemplate>
									<separatortemplate>&nbsp;&raquo;&nbsp;</separatortemplate>
								</asp:datalist></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="TTTAltHeader" height="28">&nbsp;
					<asp:Label id="lblInfo" runat="server" CssClass="TTTNormal" Width="100%" ForeColor="Red"></asp:Label></td>
			</tr>
			<asp:placeholder id="pnlAlbumDetails" Runat="server">
				<tr>
					<td>
						<table cellSpacing="1" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_URL") %>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="txtPath" runat="server" Width="100%" cssclass="NormalTextBox" Enabled="False"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Name") %>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="txtName" runat="server" Width="50%" cssclass="NormalTextBox" Enabled="False"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Prop") %>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="txtOwner" runat="server" Width="50%" cssclass="NormalTextBox" Enabled="False"></asp:TextBox>
									<asp:TextBox id="txtOwnerID" runat="server" Width="5%" cssclass="NormalTextBox" Enabled="False" Visible="True"></asp:TextBox>
									<asp:Button id=btnEditOwner cssclass="button" runat="server" Visible="False" CommandName="edit"></asp:Button>
									</td></tr>
								<!--Start select owner-->
								<asp:placeholder id=pnlSelectOwner Runat="server" Visible="False">
								<tr>
    							<td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_SelectProp") %>:</td>
    							<td>
								<TTT:USERSCONTROL id=ctlUsers runat="server" ShowEditButton="True" ShowEmail="True" ShowFullName="True" ShowUserName="True" Type="2"></TTT:USERSCONTROL></td></tr>
								</asp:placeholder>
								<!--End select owner-->
					
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_TitleI") %>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="txtTitle" runat="server" Width="50%" cssclass="NormalTextBox"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Desc") %>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="txtDescription" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Cat") %>:</td>
								<td class="TTTRow" align="left">
									<asp:checkboxlist id="lstCategories" runat="server" RepeatColumns="6" Font-Names="Verdana,Arial" Font-Size="8pt" width="100%"></asp:checkboxlist></td>
							</tr>
						</table>
					</td>
				</tr>
			</asp:placeholder>
			<asp:placeholder id="pnlAddFolder" Runat="server" Visible="False">
				<tr>
					<td>
						<table id="tbAddFolder" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<tr>
								<td class="TTTSubHeader" width="120"></td>
								<td class="TTTAltHeader" align="left" height="22">&nbsp;
									<asp:label id="lblAlbumTitle" runat="server" CssClass="TTTAltHeader"></asp:label></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Name") %>:</td>
								<td class="TTTRow" align="left" height="22">
									<asp:textbox id="txtNewAlbum" runat="server" CssClass="NormalTextBox" Width="50%"></asp:textbox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_TitleI") %>:</td>
								<td class="TTTRow" align="left" height="22">
									<asp:textbox id="txtAlbumTitle" runat="server" CssClass="NormalTextBox" Width="50%"></asp:textbox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Desc") %>:</td>
								<td class="TTTRow" align="left" height="22">
									<asp:textbox id="txtAlbumDescription" TextMode="MultiLine" runat="server" CssClass="NormalTextBox" Width="100%"></asp:textbox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Cat") %>:</td>
								<td class="TTTRow" align="left">
									<asp:checkboxlist id="lstCategories2" runat="server" RepeatColumns="6" Font-Names="Verdana,Arial" Font-Size="8pt" width="100%"></asp:checkboxlist></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120"></td>
								<td class="TTTAltHeader" align="left" height="27">&nbsp;
									<asp:button id="btnFolderSave" cssclass="button" runat="server" CommandName="update" ></asp:button>&nbsp;
									<asp:button id="btnFolderClose" cssclass="button" runat="server" CommandName="cancel" ></asp:button>&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</asp:placeholder>
			<asp:placeholder id="pnlAddFile" Runat="server" Visible="False">
				<tr>
					<td>
						<table id="tbAddFile" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<tr>
								<td class="TTTSubHeader" width="120"></td>
								<td class="TTTAltHeader" align="left" height="28">&nbsp;
									<asp:label id="lblfileTitle" runat="server" CssClass="TTTAltHeader" ></asp:label></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_FileType") %>:</td>
								<td class="TTTRow" align="left" height="22">
									<asp:label id="lblFileType" runat="server" CssClass="TTTRow"></asp:label></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_TitleI") %>:</td>
								<td class="TTTRow" align="left" height="22">
									<asp:textbox id="txtFileTitle" runat="server" CssClass="NormalTextBox" Width="50%"></asp:textbox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Desc") %>:</td>
								<td class="TTTRow" align="left" height="22">
									<asp:textbox id="txtFileDescription" TextMode="MultiLine" runat="server" CssClass="NormalTextBox" Width="98%"></asp:textbox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Cat") %>:</td>
								<td class="TTTRow" style="white-space: nowrap;" align="left">
									<asp:checkboxlist id="lstCategories3" runat="server" RepeatColumns="6" Font-Names="Verdana,Arial" Font-Size="8pt" width="100%"></asp:checkboxlist></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_File") %>:</td>
								<td class="TTTRow" align="left" style="white-space: nowrap;" height="22">
									<INPUT class="NormalTextBox" id="htmlUploadFile" type="file" size="112" name="loFile" runat="server" />
									&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnAdd" runat="server" CommandName="add" ImageUrl="~/Admin/AdvFileManager/Images/Upload.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>&nbsp;&nbsp;
									<img id="rotation" src="/images/rotation.gif" style="visibility:hidden; left: -20px; position: relative" alt="*" width="32" height="32">
									<br><asp:Label id="lblFileInfo" runat="server" CssClass="TTTNormal" ForeColor="Red"></asp:Label>&nbsp;
								</td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120"></td>
								<td class="TTTRow" align="center">&nbsp;<%= DotNetZoom.getlanguage("Gal_FileUpInfo") %></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120"></td>
								<td class="TTTRow" align="left">
									<asp:datagrid id="grdUpload" runat="server" width="100%" CellSpacing="0" CellPadding="3" BorderWidth="1" BorderColor="#D1D7DC" AutoGenerateColumns="False" DataKeyField="FileName">
										<Columns>
											<asp:TemplateColumn HeaderText="">
												<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
												<ItemStyle Height="22px" CssClass="TTTRow" Width="22px"></ItemStyle>
												<ItemTemplate>
													<asp:Image ImageUrl='<%# Ctype(Container.DataItem.icon, String) %>' Runat="server" ID="Image1">
													</asp:Image>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="Name" >
												<HeaderStyle HorizontalAlign="Left" Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left" Height="22px" Width="100px" CssClass="TTTRow"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn >
												<HeaderStyle Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
												<ItemStyle Height="22px" CssClass="TTTRow" Width="100px"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="Label1" runat="server" Text='<%# Ctype(Container.DataItem.Title, String) %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn >
												<HeaderStyle Height="28px" width="120px" CssClass="TTTAltHeader"></HeaderStyle>
												<ItemStyle Height="22px" CssClass="TTTRow"></ItemStyle>
												<ItemTemplate>
													<asp:label ID="Label3" runat="server" Text="<%# Ctype(Container.DataItem.Description, String) %>">
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn >
												<HeaderStyle Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
												<ItemStyle Height="22px" CssClass="TTTRow" Width="100px"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="Label2" runat="server" Text='<%# Ctype(Container.DataItem.Categories, String) %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center" Height="22px" CssClass="TTTRow"></ItemStyle>
												<ItemTemplate>
													<asp:ImageButton id="btnFileRemove" ImageURL="~/images/Delete.gif" runat="server" CommandName="delete" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Name"), String) %>' BorderWidth="0" BorderStyle="none"/>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120"></td>
								<td class="TTTAltHeader"  valign="middle" align="left">&nbsp;
									<asp:button id="btnFileClose" cssclass="button" runat="server" CommandName="close" ></asp:button>&nbsp;
									<asp:button id="btnFileUpload" cssclass="button" runat="server" CommandName="upload"></asp:button>
									<img id="rotation1" src="/images/rotation.gif" style="visibility:hidden; left: -50px; position: relative" alt="*" width="32" height="32">
									&nbsp;
									
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</asp:placeholder>
			<asp:placeholder id="pnladd" Runat="server">
		<tr>
		<td class="TTTHeader" valign="middle" align="left">&nbsp;<%= DotNetZoom.getlanguage("Gal_Add") %>&nbsp;:&nbsp;  
			<asp:button id="btnAddFolder" cssclass="button" runat="server" CommandName="folder"></asp:button>
			&nbsp;<asp:button id="btnAddFile" cssclass="button" runat="server" CommandName="file"></asp:button>
		</td>
		</tr>
		</asp:placeholder>
			<asp:placeholder id="pnlContentDir" Runat="server">
				<tr>
					<td>
						<asp:datagrid id="grdDir" runat="server" width="100%" CellSpacing="0" CellPadding="3" BorderWidth="1" BorderColor="#D1D7DC" AutoGenerateColumns="False" DataKeyField="Name">
							<Columns>
								<asp:TemplateColumn HeaderText="">
									<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Height="22px" CssClass="TTTRow" Width="22px"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton id="Imagebutton1" visible="<%# CanEdit(Container.DataItem) %>" ImageUrl='<%# Ctype(Container.DataItem, IGalleryObjectInfo).icon %>' runat="server" CommandName="edit" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>' BorderWidth="0" BorderStyle="none"/>
										<asp:Image visible="<%# Not CanEdit(Container.DataItem) %>" ImageUrl='<%# Ctype(Container.DataItem, IGalleryObjectInfo).icon %>' Runat="server">
										</asp:Image>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Name" >
									<HeaderStyle HorizontalAlign="Left" Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Height="22px" Width="100px" CssClass="TTTRow"></ItemStyle>
								</asp:BoundColumn>
						       <asp:TemplateColumn >
									<HeaderStyle Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Height="22px" CssClass="TTTRow" Width="100px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblProp" runat="server" Text='<%# Ctype(Container.DataItem, IGalleryObjectInfo).Owner.UserName %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn >
									<HeaderStyle Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Height="22px" CssClass="TTTRow" Width="100px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTitle" runat="server" Text='<%# Ctype(Container.DataItem, IGalleryObjectInfo).Title %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn >
									<HeaderStyle Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Height="22px" CssClass="TTTRow" Width="100px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCategory" runat="server" Text='<%# Ctype(Container.DataItem, IGalleryObjectInfo).Categories %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn >
									<HeaderStyle Height="28px" width="120px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Height="22px" CssClass="TTTRow"></ItemStyle>
									<ItemTemplate>
										<asp:label ID="lblDescription" runat="server" Text="<%# Ctype(Container.DataItem, IGalleryObjectInfo).Description %>">
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" Height="22px" CssClass="TTTRow"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton id="btnEdit" visible="<%# CanEdit(Container.DataItem) %>" ImageURL="~/images/Edit.gif" runat="server" CommandName="edit" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>' BorderWidth="0" BorderStyle="none"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" Height="22px" CssClass="TTTRow"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton id="btnDelete" visible="<%# CanEdit(Container.DataItem) %>" ImageURL="~/images/Delete.gif" runat="server" CommandName="delete" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>' BorderWidth="0" BorderStyle="none"/>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			   
			   </asp:placeholder> 
	 			
				
				<asp:placeholder id="pnlContentFile" Runat="server">
				<tr>
					<td>
						<asp:datagrid id="grdFile" runat="server" width="100%" CellSpacing="0" CellPadding="3" BorderWidth="1" BorderColor="#D1D7DC" AutoGenerateColumns="False" DataKeyField="Name">
							<Columns>
								<asp:TemplateColumn HeaderText="">
									<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Height="22px" CssClass="TTTRow" Width="22px"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton id="Imagebutton1" style="CURSOR: help" onmouseover='<%# ReturnGalleryToolTip( CType(Container.DataItem,IGalleryObjectInfo).thumbNail)  %>' visible="<%# CanEdit(Container.DataItem) %>" ImageUrl='<%# Ctype(Container.DataItem, IGalleryObjectInfo).icon %>' runat="server" CommandName="edit" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>' BorderWidth="0" BorderStyle="none"/>
										<asp:Image visible="<%# Not CanEdit(Container.DataItem) %>" ImageUrl='<%# Ctype(Container.DataItem, IGalleryObjectInfo).icon %>' Runat="server">
										</asp:Image>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Name" >
									<HeaderStyle HorizontalAlign="Left" Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Height="22px" Width="100px" CssClass="TTTRow"></ItemStyle>
								</asp:BoundColumn>
						       <asp:TemplateColumn >
									<HeaderStyle Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Height="22px" CssClass="TTTRow" Width="100px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblProp2" runat="server" Text='<%# Ctype(Container.DataItem, IGalleryObjectInfo).Owner.UserName %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
										<asp:TemplateColumn >
									<HeaderStyle Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Height="22px" CssClass="TTTRow" Width="100px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTitle" runat="server" Text='<%# Ctype(Container.DataItem, IGalleryObjectInfo).Title %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn >
									<HeaderStyle Height="28px" Width="100px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Height="22px" CssClass="TTTRow" Width="100px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCategory" runat="server" Text='<%# Ctype(Container.DataItem, IGalleryObjectInfo).Categories %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn >
									<HeaderStyle Height="28px" width="120px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Height="22px" CssClass="TTTRow"></ItemStyle>
									<ItemTemplate>
										<asp:label ID="lblDescription" runat="server" Text="<%# Ctype(Container.DataItem, IGalleryObjectInfo).Description %>">
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" Height="22px" CssClass="TTTRow"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton id="btnEdit" visible="<%# CanEdit(Container.DataItem) %>" ImageURL="~/images/Edit.gif" runat="server" CommandName="edit" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>' BorderWidth="0" BorderStyle="none"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" Height="22px" CssClass="TTTRow"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton id="btnDelete" visible="<%# CanEdit(Container.DataItem) %>" ImageURL="~/images/Delete.gif" runat="server" CommandName="delete" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>' BorderWidth="0" BorderStyle="none"/>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</asp:placeholder> <!--Pnlcontentfile-->
	</asp:placeholder> <!--PnlMain-->
	<asp:placeholder id="pnlRefuse" Runat="server" Visible="False">
		<tr>
			<td>
				<table cellSpacing="1" cellPadding="3" width="100%" border="0">
					<tr>
						<td class="TTTHeader" valign="middle" width="100%" height="28">&nbsp;
							<%= DotNetZoom.getlanguage("Gal_Private") %></td>
					</tr>
					<tr>
						<td class="TTTRow" valign="middle">
							<asp:Label id="lblRefuse" Runat="server" CssClass="TTTNormal" Width="84%"></asp:Label></td>
					</tr>
				</table>
			</td>
		</tr>
	</asp:placeholder>
	<asp:placeholder id="pnlAdd1" Runat="server">
	<tr  valign="middle">
		<td class="TTTAltHeader" align="left" colSpan="2">&nbsp;
			<asp:button id="CancelButton" cssclass="button" runat="server" CommandName="back"></asp:button>&nbsp;
			<asp:button cssclass="button" id="UpdateButton" runat="server" CommandName="back"></asp:button>&nbsp;
		</td>
	</tr>
	</asp:placeholder>
</table>