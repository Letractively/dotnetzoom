<%@ Import namespace="DotNetZoom" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Register TagPrefix="TTT" TagName="UsersControl" Src="../TTTForum/TTT_UsersControl.ascx"%>
<%@ Register Assembly="dotnetzoom" Namespace="DotNetZoom" TagPrefix="cc1" %>
<%@ Control language="vb" Inherits="DotNetZoom.TTT_GalleryEditAlbum" CodeBehind="TTT_GalleryEditAlbum.ascx.vb" AutoEventWireup="false" %>
<table class="TTTBorder" cellSpacing="1" cellPadding="0" width="780" align="center">
	<asp:placeholder ID="pnlMain" Runat="server">
			<tr>
				<td width="100%" height="28">
					<table cellSpacing="0" cellPadding="3" width="100%" border="0">
						<tr>
							<td class="TTTHeader" align="left" valign="middle" width="200" height="28">&nbsp;
								<asp:label id="lblHeader" Runat="server"></asp:label>:&nbsp;
                            <asp:imagebutton id="ClearCache" runat="server" EnableViewState="false" visible="False" height="16" width="16" ImageURL="~/images/1x1.gif" style="border-width:0px;"></asp:imagebutton>
                            <asp:imagebutton id="SubAlbum" runat="server" EnableViewState="false" visible="False" height="16" width="16" imageurl="~/Admin/AdvFileManager/Images/NewFolder.gif" style="border-width:0px"></asp:imagebutton>
                            <asp:imagebutton id="UploadImage2" runat="server" EnableViewState="false" visible="False" height="16" width="16" imageurl="~/Admin/AdvFileManager/Images/Upload.gif" style="border-width:0px"></asp:imagebutton>
                            </td>
							<td class="TTTHeader" align="left" height="28">
								<asp:datalist id="dlFolders" HorizontalAlign="Left" runat="server" repeatlayout="Flow" repeatdirection="Horizontal">
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
				<td class="TTTAltHeader" height="28">&nbsp;
                
					<asp:Label id="lblInfo" runat="server" CssClass="TTTNormal" Width="100%" ForeColor="Red"></asp:Label></td>
			</tr>
			<asp:placeholder id="pnlAlbumDetails" Runat="server">
			<tr>
				<td>
					<table cellSpacing="1" cellPadding="0" width="100%" border="0">
                       <tr>
                        <td>
                          <table>
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
							</td>
                        </tr>
							<!--Start select owner-->
						<asp:placeholder id=pnlSelectOwner Runat="server" Visible="False">
						<tr>
    						<td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_SelectProp") %>:</td>
    						<td>
							<TTT:USERSCONTROL id=ctlUsers runat="server" ShowEditButton="True" ShowEmail="True" ShowFullName="True" ShowUserName="True" Type="2"></TTT:USERSCONTROL>
                            </td>
                        </tr>
						</asp:placeholder>
							<!--End select owner-->
					
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_TitleI") %>:</td>
							<td class="TTTRow" align="left">
							<asp:TextBox id="txtTitle" runat="server" Width="50%" cssclass="NormalTextBox"></asp:TextBox></td>
						</tr>
						<tr>
						    <td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("OrderBy") %></td>
							<td class="TTTRow" align="left">
							<asp:TextBox id="txtSortOrder" runat="server" Width="15%" cssclass="NormalTextBox"></asp:TextBox></td>
						</tr>
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Desc") %>:</td>
							<td class="TTTRow" align="left">
							<asp:TextBox id="txtDescription" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
						</tr>
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Gal_Latitude")%>:</td>
							<td class="TTTRow" align="left">
							<asp:TextBox id="Latitude" TextMode="singleline" runat="server" Width="50%" cssclass="NormalTextBox"></asp:TextBox></td>
						</tr>
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Gal_Longitude")%>:</td>
							<td class="TTTRow" align="left">
							<asp:TextBox id="Longitude" TextMode="SingleLine" runat="server" Width="50%" cssclass="NormalTextBox"></asp:TextBox></td>
						</tr>



						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Cat") %>:</td>
							<td class="TTTRow" align="left">
							<asp:DropDownList ID="ddCategories" runat=server></asp:DropDownList></td>
						</tr>

                        </table>
                        </td>
                        <td class="TTTRow" valign="middle" align="center" width="150">
                        <asp:placeholder id="FolderImage" Visible="false" Runat="server">
                        <table style="font-weight: bold; font-size: 8.5pt; color: black; text-decoration: none;">
    <tr><td>
	<asp:Image id="ImgFile" EnableViewState="True" AlternateText="thumnail" Runat="server"></asp:Image>
    </td>
    <td>
    <table><tr><td>
    <asp:Image id="ImgFileIcon"  AlternateText="gpsicon" EnableViewState="True" Runat="server"></asp:Image>
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
    </td></tr>
	<tr align="left">
		<td Align=Left valign=top><input id="UploadFile" type="file" name="UploadFile"  size="15" runat="server" /></td>
		<td Align=Left valign=top><asp:button id="UploadImage" runat="server" /></td>
	</tr>
<tr align="left"><td bgcolor="#d7d79f" colspan="3">
<%= DotNetZoom.GetLanguage("LabelQuaImage")%>
<asp:textbox id="txtquality" runat="server" width="20" MaxLength="2"></asp:textbox>%
	
<asp:RangeValidator id="Range1"
	       Display = "Dynamic"
           ControlToValidate="txtquality"
           MinimumValue="20"
           MaximumValue="80"
           Type="Integer"
           EnableClientScript="True"
           runat="server"/><br />
<%= DotNetZoom.GetLanguage("LabelMaxHeightImage")%>
<asp:textbox id="txtSizeH" runat="server" width="30" MaxLength="3"></asp:textbox>px
	<asp:RangeValidator id="Range2"
	       Display = "Dynamic"
           ControlToValidate="txtSizeH"
           MinimumValue="10"
           MaximumValue="200"
           Type="Integer"
           EnableClientScript="True"
           runat="server"/><br />
<%= DotNetZoom.GetLanguage("LabelMaxWImage")%>
<asp:textbox id="txtSizeL" runat="server" width="30" MaxLength="3"></asp:textbox>px
	<asp:RangeValidator id="Range3"
	       Display = "Dynamic"
           ControlToValidate="txtSizeL"
           MinimumValue="10"
           MaximumValue="200"
           Type="Integer"
           EnableClientScript="True"
           runat="server"/>
	</td></tr>
</table>
</asp:placeholder>
                        </td>
                       </tr>
                    </table>
                </td>
            </tr>
            </asp:placeholder>
            <asp:placeholder id="pnlMapOption" Runat="server">
            <tr>
             <td>


<script language="JavaScript" type="text/javascript">
    function OpenColorWindow(tabid) {
        var m = window.open('<%=DotNetZoom.glbPath %>admin/tabs/colorselector.aspx?L=<%= DotNetZoom.GetLanguage("N") %>', 'color', 'width=400,height=330,left=100,top=100');
        m.focus();
    }

    function Setcolor(idParentValue) {
        document.getElementById('<%=txtColor.ClientID%>').value = idParentValue;
        document.getElementById('selhicolor').style.backgroundColor = idParentValue;
    }

</script>


                <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
				        <td class="TTTHeader""  height="28">&nbsp;<%= DotNetZoom.GetLanguage("MapOptions")%></td>
			        </tr>
					<tr><td>
                        <cc1:OpenClose ID="OpenClose1"  TDClass="TTTSubHeader" Show="false" runat="server">
                        
                    <table cellSpacing="0" cellPadding="0" width="770" border="0">
                    		<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Width")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_width" TextMode="SingleLine" runat="server" Width="50%" cssclass="NormalTextBox"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Height")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_height" TextMode="SingleLine" runat="server" Width="50%" cssclass="NormalTextBox"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_FullScreen")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_full_screen" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Center")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_center" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Zoom")%>:</td>
								<td class="TTTRow" align="left">
                                    <asp:DropDownList id="options_zoom"  runat="server" >
                                      <asp:ListItem Selected="True" Value="'auto'"> Auto </asp:ListItem>
                                      <asp:ListItem Value="1">  1  </asp:ListItem>
                                      <asp:ListItem Value="2">  2 </asp:ListItem>
                                      <asp:ListItem Value="3">  3  </asp:ListItem>
                                      <asp:ListItem Value="4">  4 </asp:ListItem>
                                      <asp:ListItem Value="5">  5  </asp:ListItem>
                                      <asp:ListItem Value="6">  6 </asp:ListItem>
                                      <asp:ListItem Value="7">  7  </asp:ListItem>
                                      <asp:ListItem Value="8">  8 </asp:ListItem>
                                      <asp:ListItem Value="9">  9  </asp:ListItem>
                                      <asp:ListItem Value="10">  10 </asp:ListItem>
                                      <asp:ListItem Value="11">  11 </asp:ListItem>
                                      <asp:ListItem Value="12">  12 </asp:ListItem>
                                      <asp:ListItem Value="13">  13 </asp:ListItem>
                                      <asp:ListItem Value="14">  14 </asp:ListItem>
                                      <asp:ListItem Value="15">  15 </asp:ListItem>
                                      <asp:ListItem Value="16">  16 </asp:ListItem>
                                      <asp:ListItem Value="17">  17 </asp:ListItem>
                                      <asp:ListItem Value="18">  18 </asp:ListItem>
                                      <asp:ListItem Value="19">  19 </asp:ListItem>
                                    </asp:DropDownList>

                           
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Opacity")%>:</td>
								<td class="TTTRow" align="left">
                                    <asp:DropDownList id="options_map_opacity"  runat="server" >
                                      <asp:ListItem Selected="True" Value="1"> 100% </asp:ListItem>
                                      <asp:ListItem Value="0.9"> 90%  </asp:ListItem>
                                      <asp:ListItem Value="0.8"> 80% </asp:ListItem>
                                      <asp:ListItem Value="0.7"> 70%  </asp:ListItem>
                                      <asp:ListItem Value="0.6"> 60% </asp:ListItem>
                                      <asp:ListItem Value="0.5"> 50%  </asp:ListItem>
                                      <asp:ListItem Value="0.4"> 40% </asp:ListItem>
                                      <asp:ListItem Value="0.3"> 30%  </asp:ListItem>
                                      <asp:ListItem Value="0.2"> 20% </asp:ListItem>
                                      <asp:ListItem Value="0.1"> 10%  </asp:ListItem>
                                      <asp:ListItem Value="0"  >  0% </asp:ListItem>
                                      
                                    </asp:DropDownList>

                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Type")%>:</td>
								<td class="TTTRow" align="left">
									<asp:DropDownList id="options_map_type"  runat="server" >
                                    
                                   <asp:ListItem Selected="True" Value="'G_NORMAL_MAP'"> Google </asp:ListItem>
                                   <asp:ListItem Value="'G_SATELLITE_MAP'"> Google satellite </asp:ListItem>
                                   <asp:ListItem Value="'G_HYBRID_MAP'"> Google hybrid </asp:ListItem>
                                   <asp:ListItem Value="'G_PHYSICAL_MAP'"> Google terrain </asp:ListItem>
                                   <asp:ListItem Value="'MYTOPO_TILES'"> MYTopo </asp:ListItem>
                                   <asp:ListItem Value="'USGS_TOPO_TILES'"> USGS Topo </asp:ListItem>
                                   <asp:ListItem Value="'USGS_COLOR_TILES'"> USGS Topo Color </asp:ListItem>
                                   <asp:ListItem Value="'USGS_COLOR_HYBRID_TILES'"> USGS Topo hybrid </asp:ListItem>
                                   <asp:ListItem Value="'USGS_AERIAL_TILES'"> USGS Aerial </asp:ListItem>
                                   <asp:ListItem Value="'US_COUNTY_TILES'"> US County </asp:ListItem>
                                   <asp:ListItem Value="'NRCAN_TOPORAMA_TILES'"> Canada Toporama </asp:ListItem>
                                   <asp:ListItem Value="'NRCAN_TOPORAMA2_TILES'"> Canada Toporama (no name) </asp:ListItem>
                                   <asp:ListItem Value="'NRCAN_TOPO_TILES'"> Canada Toporama (old) </asp:ListItem>
                                   <asp:ListItem Value="'LANDSAT_TILES'"> Landsat 30 m </asp:ListItem>
                                   <asp:ListItem Value="'BLUEMARBLE_TILES'"> Blue Marble </asp:ListItem>
                                   <asp:ListItem Value="'OPENSTREETMAP_TILES'"> Open Street </asp:ListItem>
                                   <asp:ListItem Value="'OPENCYCLEMAP_TILES'"> Open Cycle </asp:ListItem>
                                   <asp:ListItem Value="'YAHOO_MAP'"> Yahoo </asp:ListItem>
                                   <asp:ListItem Value="'YAHOO_AERIAL'"> Yahoo Aerial </asp:ListItem>
                                   <asp:ListItem Value="'YAHOO_HYBRID'"> Yahoo Hybrid </asp:ListItem>
                                   <asp:ListItem Value="'G_SATELLITE_3D_MAP'"> Satellite 3D </asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
 
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_D_Zoom")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_doubleclick_zoom"  runat="server" ></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_M_Zoom")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_mousewheel_zoom" runat="server" ></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_C_Options")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_centering_options" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>
                            </table>
                        </cc1:OpenClose>

                    </td></tr>
 
 					<tr><td colspan="2">
                        <cc1:OpenClose ID="OpenClose2"  TDClass="TTTSubHeader" Show="false" runat="server">
                        <table cellSpacing="0" cellPadding="0" width="770" border="0">

                            <!--  widgets on the map: -->
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Zoom_Ctl")%>:</td>
								<td class="TTTRow" align="left">
           						    <asp:DropDownList id="options_zoom_control"  runat="server" >
                                      <asp:ListItem Selected="True" Value="'large'"> Large </asp:ListItem>
                                      <asp:ListItem Value="'small'"> Small </asp:ListItem>
                                      <asp:ListItem Value="'3d'">  3D   </asp:ListItem>
                                    </asp:DropDownList>
           
                            </tr>

							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Scale_Ctl")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_scale_control" runat="server" ></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Center_Coord")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_center_coordinates" runat="server" ></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Crooshair_Ctl")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_crosshair_hidden" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Opacity_Ctl")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_map_opacity_control" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Ctl_Style")%>:</td>
								<td class="TTTRow" align="left">
									<asp:DropDownList id="options_map_type_control_style"  runat="server">
                                    <asp:ListItem Selected="True" Value="'none'"> none </asp:ListItem>
                                    <asp:ListItem Value="'list'"> list </asp:ListItem>
                                    <asp:ListItem Value="'menu'"> menu </asp:ListItem>
                                    <asp:ListItem Value="'google'"> google </asp:ListItem>
                                    </asp:DropDownList></td>
                      
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Ctl_Filter")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_map_type_control_filter" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Ctl_Excluded")%>:</td>
								<td class="TTTRow" align="left">
                                   <asp:checkboxlist id="options_map_type_control_excluded" runat="server" RepeatColumns="3" Font-Names="Verdana,Arial" Font-Size="8pt" width="98%">
                                   <asp:ListItem Selected="True" Value="'G_NORMAL_MAP'"> Google </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'G_SATELLITE_MAP'"> Google satellite </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'G_HYBRID_MAP'"> Google hybrid </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'G_PHYSICAL_MAP'"> Google terrain </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'MYTOPO_TILES'"> MYTopo </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'USGS_TOPO_TILES'"> USGS Topo </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'USGS_COLOR_TILES'"> USGS Topo Color </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'USGS_COLOR_HYBRID_TILES'"> USGS Topo hybrid </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'USGS_AERIAL_TILES'"> USGS Aerial </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'US_COUNTY_TILES'"> US County </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'NRCAN_TOPORAMA_TILES'"> Canada Toporama </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'NRCAN_TOPORAMA2_TILES'"> Canada Toporama (no name) </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'NRCAN_TOPO_TILES'"> Canada Toporama (old) </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'LANDSAT_TILES'"> Landsat 30 m </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'BLUEMARBLE_TILES'"> Blue Marble </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'OPENSTREETMAP_TILES'"> Open Street </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'OPENCYCLEMAP_TILES'"> Open Cycle </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'YAHOO_MAP'"> Yahoo </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'YAHOO_AERIAL'"> Yahoo Aerial </asp:ListItem>
                                   <asp:ListItem Selected="True" Value="'YAHOO_HYBRID'"> Yahoo Hybrid </asp:ListItem>
                                   <asp:ListItem Value="'G_SATELLITE_3D_MAP'"> Satellite 3D </asp:ListItem>

                                   </asp:checkboxlist>

                                    </td>
                            </tr>
                            

                        </table>
                        </cc1:OpenClose>

                    </td></tr>
 					<tr><td colspan="2">
                      <cc1:OpenClose ID="OpenClose3"  TDClass="TTTSubHeader" Show="false" runat="server">
                       <table cellSpacing="0" cellPadding="0" width="770" border="0">


							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Legend")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_legend_options_legend" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Legend_Pos")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_legend_options_position" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Legend_Drag")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_legend_options_draggable" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Legend_Coll")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_legend_options_collapsible" runat="server" ></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Mes_Tools")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_measurement_tools" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>
 

                        </table>

                        </cc1:OpenClose>

                    </td></tr>
 					<tr><td colspan="2">
                        <cc1:OpenClose ID="OpenClose4"  TDClass="TTTSubHeader" Show="false" runat="server">
                        <table cellSpacing="0" cellPadding="0" width="770" border="0">

							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Tracklist_Opt")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_tracklist_options_tracklist" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Tracklist_Opt_Pos")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_tracklist_options_position" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Tracklist_Width")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_tracklist_options_max_width" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>

							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Tracklist_Height")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_tracklist_options_max_height" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Tracklist_Desc")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_tracklist_options_desc" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Tracklist_Zoom_Lnk")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_tracklist_options_zoom_links" runat="server" ></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Tracklist_Tlp")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_tracklist_options_tooltips" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Tracklist_Drag")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_tracklist_options_draggable" runat="server" ></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Tracklist_Col")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_tracklist_options_collapsible" runat="server"></asp:CheckBox></td>
                            </tr>


                        </table>
                        </cc1:OpenClose>

                    </td></tr>
 					<tr><td colspan="2">
                        <cc1:OpenClose ID="OpenClose5"  TDClass="TTTSubHeader" Show="false" runat="server">
                        <table cellSpacing="0" cellPadding="0" width="770" border="0">

							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Default_Mark")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_default_marker" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Shadows")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_shadows" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Mark_Link")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_marker_link_target" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Info_Width")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_info_window_width" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>

							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Thumb_Width")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_thumbnail_width" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Photo_Size")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_photo_size" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>

							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Labels_Hide")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_hide_labels" runat="server" ></asp:CheckBox></td>
                            </tr>

							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Label_Off")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_label_offset" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Label_Center")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_label_centered" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_D_Direct")%>:</td>
								<td class="TTTRow" align="left">
									<asp:CheckBox id="options_driving_directions" runat="server"></asp:CheckBox></td>
                            </tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Garmin_Icon")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="options_garmin_icon_set" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>


                        </table>
                        </cc1:OpenClose>

                    </td></tr>
 					<tr><td colspan="2">
                        <cc1:OpenClose ID="OpenClose6"  TDClass="TTTSubHeader" Show="false" runat="server">
                        <table cellSpacing="0" cellPadding="0" width="770" border="0">
 	<tr>
		<td class="TTTSubHeader" width="120"><%= DotNetZoom.GetLanguage("ms_color")%>:</td>
		<td class="TTTRow" align="left">
        <div id="selhicolor" style="background-color : <%=txtcolor.text%>  ; height: 20px ; width: 74px ; border-width: 1px ; border-style: solid ;"></div>
        &nbsp;&nbsp;
        <asp:textbox id="txtColor" CssClass="NormalTextBox" Runat="server" Columns="7"></asp:textbox>
		&nbsp;&nbsp;<asp:hyperlink id="lnkcolor" CssClass="CommandButton" runat="server"></asp:hyperlink>
		</td>
	</tr>

 							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Gal_UploadGPXFile")%>:</td>
								<td class="TTTRow" align="left" style="white-space: nowrap;" height="22">
									<input id="UploadGPXFile" type="file" name="UploadGPXFile" size="65" runat="server" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:button id="MakeTrack" cssclass="button" runat="server" CommandName="update" ></asp:button>
								</td>
							</tr>
                           
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Tracks")%>:
                                
                                
                                </td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="trk_info" Rows="15" Columns="105" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                            </tr>


                        </table>
                        </cc1:OpenClose>

                    </td></tr>
 					<tr><td colspan="2">
                        <cc1:OpenClose ID="OpenClose7"  TDClass="TTTSubHeader" Show="false" runat="server">
                        <table cellSpacing="0" cellPadding="0" width="770" border="0">
                            
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("Map_Markers")%>:</td>
								<td class="TTTRow" align="left">
									<asp:TextBox id="Draw_Marker" Rows="15" Columns="105" TextMode="MultiLine" runat="server" Width="98%" cssclass="NormalTextBox"></asp:TextBox></td>
                          </tr>
 

                        </table>
                        </cc1:OpenClose>

                    </td></tr>
 
 
                            		   

					</table>
                    </td>
                    </tr>

			</asp:placeholder>
<asp:placeholder id="pnlAdd1" Runat="server">
	<tr valign="middle">
		<td class="TTTAltHeader" align="left">
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="CancelButton" cssclass="button" runat="server" CommandName="back"></asp:button>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button cssclass="button" id="UpdateButton" runat="server" CommandName="back"></asp:button>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:button cssclass="button" id="EditMapOptions" runat="server" CommandName="back"></asp:button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;             
		</td>
	</tr>
</asp:placeholder>	
<asp:placeholder id="pnladd" Runat="server">
	<tr>
	    <td class="TTTSubHeader" align="left">
        <table cellSpacing="1" cellPadding="1" width="100%" border="0"><tr><td align=left>
 		<%= DotNetZoom.GetLanguage("SS_Label_TimeZone") %>:&nbsp;<asp:Label id="LblTimeZone" runat="server" CssClass="NormalTextBox" ></asp:Label>
		&nbsp;&nbsp;<asp:DropDownList id="ddlTimeZone" Width="300px" DataValueField="Zone" DataTextField="Description" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>
        &nbsp;&nbsp;<asp:DropDownList id="ddljours" Width="60px"  runat="server" CssClass="NormalTextBox" >
                                      <asp:ListItem Value="-1"> -1 </asp:ListItem>
                                      <asp:ListItem Selected="True" Value="0">  0 </asp:ListItem>
                                      <asp:ListItem Value="1">  1 </asp:ListItem>
                                      
                                        </asp:DropDownList>
        &nbsp;&nbsp;<asp:DropDownList id="ddlheure" Width="60px"  runat="server" CssClass="NormalTextBox" >
                                              <asp:ListItem Value="-12"> -12</asp:ListItem>
                                              <asp:ListItem Value="-11"> -11</asp:ListItem>
                                              <asp:ListItem Value="-10"> -10</asp:ListItem>
                                              <asp:ListItem Value="-9"> -9 </asp:ListItem>
                                              <asp:ListItem Value="-8"> -8 </asp:ListItem>
                                              <asp:ListItem Value="-7"> -7 </asp:ListItem>
                                              <asp:ListItem Value="-6"> -6 </asp:ListItem>
                                              <asp:ListItem Value="-5"> -5 </asp:ListItem>
                                              <asp:ListItem Value="-4"> -4 </asp:ListItem>
                                              <asp:ListItem Value="-3"> -3 </asp:ListItem>
                                              <asp:ListItem Value="-2"> -2 </asp:ListItem>
                                              <asp:ListItem Value="-1"> -1 </asp:ListItem>
                                              <asp:ListItem Selected="True" Value="0">  0 </asp:ListItem>
                                              <asp:ListItem Value="1">  1 </asp:ListItem>
                                              <asp:ListItem Value="2">  2 </asp:ListItem>
                                              <asp:ListItem Value="3">  3 </asp:ListItem>
                                              <asp:ListItem Value="4">  4 </asp:ListItem>
                                              <asp:ListItem Value="5">  5 </asp:ListItem>
                                              <asp:ListItem Value="6">  6 </asp:ListItem>
                                              <asp:ListItem Value="7">  7 </asp:ListItem>
                                               <asp:ListItem Value="8">  8 </asp:ListItem>
                                            <asp:ListItem Value="9">  9 </asp:ListItem>
                                            <asp:ListItem Value="10">  10</asp:ListItem>
                                            <asp:ListItem Value="11">  11</asp:ListItem>
                                            <asp:ListItem Value="12">  12</asp:ListItem>
                                        </asp:DropDownList>       
        </td>
        <td align=right>
        <asp:ImageButton id="btngpsEditall" Height="16" Width="16" ImageURL="~/images/gps/40Earth.png"  runat="server" CommandName="gps" CommandArgument="reset" BorderWidth="0" BorderStyle="none"/>
        </td></tr></table>


</td></tr>
</asp:placeholder>

			<asp:placeholder id="pnlAddFolder" Runat="server" Visible="False">
				<tr>
					<td >
						<table id="tbAddFolder" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<tr>
								<td class="TTTSubHeader" width="120"></td>
								<td class="TTTAltHeader" align="left" height="22">&nbsp;
									<asp:label id="lblAlbumTitle" runat="server" CssClass="TTTAltHeader"></asp:label></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Name") %>:</td>
								<td class="TTTRow" align="left" height="22">
									<asp:textbox id="txtNewAlbum" runat="server" CssClass="NormalTextBox" Width="50%"></asp:textbox>
                                    
                                    <Asp:RegularExpressionValidator id="txtNewAlbumValidator" ControlToValidate="txtNewAlbum" CssClass="Normal" ValidationExpression="^([a-zA-Z0-9_-]{2,12})$"   Display="static" RunAt="server">
                                                                        </asp:RegularExpressionValidator>
                                    
                                    </td>
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
                                <asp:DropDownList ID="ddCategories2" runat=server></asp:DropDownList></td>
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
					<td >
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
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("WaterMark")%></td>
									<td class="TTTRow"align="left" height="22">
									<asp:TextBox id="txtWaterMark" runat="server" Width="50%" cssclass="NormalTextBox"></asp:TextBox></td>
						    </tr>

							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Desc") %>:</td>
								<td class="TTTRow" align="left" height="22">
									<asp:textbox id="txtFileDescription" TextMode="MultiLine" runat="server" CssClass="NormalTextBox" Width="98%"></asp:textbox></td>
							</tr>
							<tr>
								<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Cat") %>:</td>
								<td class="TTTRow" style="white-space: nowrap;" align="left">
									<asp:DropDownList ID="ddCategories3" runat=server></asp:DropDownList></td>
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
													<asp:TextBox id="Title" runat="server" Text='<%# Ctype(Container.DataItem.Title, String) %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn >
												<HeaderStyle Height="28px" width="120px" CssClass="TTTAltHeader"></HeaderStyle>
												<ItemStyle Height="22px" CssClass="TTTRow"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox ID="Description" runat="server" Text="<%# Ctype(Container.DataItem.Description, String) %>">
													</asp:TextBox>
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
													<asp:ImageButton id="btnFileSave" ImageURL="~/images/save.gif" runat="server" CommandName="save" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Name"), String) %>' BorderWidth="0" BorderStyle="none"/>
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
		
	
			<asp:placeholder id="pnlContentDir" Runat="server">
				<tr>
					<td >
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
										<asp:Label id="lblProp" runat="server" Text='<%# New GalleryUser(Ctype(Container.DataItem, IGalleryObjectInfo).OwnerID).UserName %>'>
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

 								<asp:TemplateColumn Visible=false>
									<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" Height="22px" CssClass="TTTRow"></ItemStyle>
				              		<ItemTemplate>
                                    <asp:ImageButton id="btngpsEdit" visible="false" Height="16" Width="16" ImageURL="~/images/gps/40Earth.png" runat="server" CommandName="gps" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>' BorderWidth="0" BorderStyle="none"/>
									</ItemTemplate>
								</asp:TemplateColumn>

                     
							</Columns>
						</asp:datagrid></td>
				</tr>
			   
			   </asp:placeholder> 
	 			
				
				<asp:placeholder id="pnlContentFile" Runat="server">
				<tr>
					<td >
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
										<asp:Label id="lblProp2" runat="server" Text='<%# New GalleryUser(Ctype(Container.DataItem, IGalleryObjectInfo).OwnerID).UserName %>'>
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

								<asp:TemplateColumn Visible=false>
									<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" Height="22px" CssClass="TTTRow"></ItemStyle>
				              		<ItemTemplate>
                                    <asp:ImageButton id="btngpsEdit" Height="16" Width="16" ImageURL="~/images/gps/40Earth.png"  runat="server" CommandName="gps" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>' BorderWidth="0" BorderStyle="none"/>
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:templatecolumn Visible=False>
                                	<HeaderStyle Height="28px" Width="22px" CssClass="TTTAltHeader"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" Height="22px" CssClass="TTTRow"></ItemStyle>
				              		<ItemTemplate>
								    <input type=checkbox value='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>' runat="server" id="Checkbox1" name="Checkbox1">
							        </itemtemplate>
						        </asp:templatecolumn>
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

</table>