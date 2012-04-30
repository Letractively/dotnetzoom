<%@ Control Language="vb" codebehind="EditVendors.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditVendors" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="Portal" TagName="Address" Src="~/controls/Address.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:placeholder id="pnlOptions" Visible="False" Runat="server">
    <table cellspacing="0" cellpadding="2" border="0">
        <tbody>
			<tr>
    			<td colspan="2" align="left">
           		<span class="Head"><%= DotNetZoom.GetLanguage("SS_Head_Language_Info") %>&nbsp;</span><asp:DropDownList id="ddlLanguage" Width="100px" AutoPostBack="True" DataValueField="language" DataTextField="SettingValue" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>
        		</td>
        	</tr>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=txtInstructions.ClientID%>"><%= DotNetZoom.GetLanguage("Vendors_Directives") %>&nbsp;<%= ddlLanguage.SelectedItem.Text %>:</label>
                </td>
                <td valign="bottom">
                    <asp:TextBox id="txtInstructions" Runat="server" Rows="3" TextMode="MultiLine" Columns="80" CssClass="NormalTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=cboRoles.ClientID%>"><%= DotNetZoom.GetLanguage("Vendors_WhoCanSubscribe") %>:</label>
                </td>
                <td valign="bottom">
                    <asp:dropdownlist id="cboRoles" runat="server" CssClass="NormalTextBox" DataValueField="RoleID" DataTextField="RoleName" Width="164px"></asp:dropdownlist>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=txtMessage.ClientID%>"><%= DotNetZoom.GetLanguage("Vendors_WelcomeMessage") %>&nbsp;<%= ddlLanguage.SelectedItem.Text %>:</label>
                </td>
                <td valign="bottom">
                    <asp:TextBox id="txtMessage" Runat="server" Rows="3" TextMode="MultiLine" Columns="80" CssClass="NormalTextBox"></asp:TextBox>
                </td>
            </tr>
           <tr runat="Server" Id="ClassificationsEdit" Visible="False" valign="top">
		   <td valign="middle" class="SubHead" >
		   <%= DotNetZoom.GetLanguage("Vendors_Edit_Classifications") %>
		   </td>
             <td>
			 <asp:TextBox id="txtAddName" Runat="server" width="150px" maxlength="200" CssClass="NormalTextBox"></asp:TextBox>
			 <asp:linkbutton class="CommandButton" id="cmdAddName" runat="server" CausesValidation="False" ></asp:linkbutton>
			 <div style="overflow: auto; height: 120px; border: 1px silver solid;">
             <asp:datagrid id="grdEditClassifications" OnItemCommand="grdEditClassifications_ItemCommand" DataKeyField="ClassificationId" runat="server" Width="100%" EnableViewState="true" AutoGenerateColumns="false" CellSpacing="0" gridlines="none" BorderStyle="None">
             	<Columns>
		        <asp:TemplateColumn ItemStyle-Width="20">
                    	<ItemTemplate>
						<asp:ImageButton ToolTip='<%# DotNetZoom.GetLanguage("delete") %>' ID="cmdDeleteUserRole" Runat="server" AlternateText='<%# DotNetZoom.GetLanguage("delete") %>' CausesValidation="False" CommandName="Delete" ImageURL="~/images/delete.gif" BorderWidth="0" BorderStyle="none"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>										
                    <asp:BoundColumn DataField="ClassificationName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
				</Columns>
                </asp:datagrid>								
     			</div>
                </td>
             </tr>
        </tbody>
    </table>
    <p align="left">
        <asp:LinkButton class="CommandButton" id="cmdUpdateOptions" runat="server" CausesValidation="False" ></asp:LinkButton>
        &nbsp;
        <asp:LinkButton id="cmdCancelOptions" runat="server" CssClass="CommandButton" CausesValidation="False" ></asp:LinkButton>
    </p>
</asp:placeholder>
<asp:placeholder id="pnlContent" Runat="server">
    <p align="left">
        <asp:Label id="lblInstructions" runat="server" visible="False" cssclass="Normal" width="350"></asp:Label>
    </p>
    <table cellspacing="0" cellpadding="4" border="0">
        <tbody>
            <tr>
                <td Valign="top">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="1" border="0">
                                        <tbody>
                                            <tr valign="top">
                                                <td class="SubHead" width="90">
                                                    <label for="<%=txtVendorName.ClientID%>"><%= DotNetZoom.GetLanguage("Vendors_Bussiness") %>:</label></td>
                                                <td class="NormalBold" style="white-space: nowrap;" align="left">
                                                    <asp:textbox id="txtVendorName" tabIndex="1" runat="server" cssclass="NormalTextBox" width="200px" maxlength="50"></asp:textbox>
                                                    &nbsp;*
                                                    <asp:requiredfieldvalidator id="valVendorName" runat="server" CssClass="NormalRed" ControlToValidate="txtVendorName" Display="Dynamic"></asp:requiredfieldvalidator>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="SubHead" width="90">
                                                    <label for="<%=txtFirstName.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Name") %>:</label></td>
                                                <td class="NormalBold" style="white-space: nowrap;">
                                                    <asp:textbox id="txtFirstName" tabIndex="2" runat="server" cssclass="NormalTextBox" width="200px" maxlength="50"></asp:textbox>
                                                    &nbsp;*
                                                    <asp:requiredfieldvalidator id="valFirstName" runat="server" CssClass="NormalRed" ControlToValidate="txtFirstName" Display="Dynamic"></asp:requiredfieldvalidator>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="SubHead" width="90">
                                                    <label for="<%=txtLastName.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_LastName") %>:</label></td>
                                                <td class="NormalBold" style="white-space: nowrap;">
                                                    <asp:textbox id="txtLastName" tabIndex="3" runat="server" cssclass="NormalTextBox" width="200px" maxlength="50"></asp:textbox>
                                                    &nbsp;*
                                                    <asp:requiredfieldvalidator id="valLastName" runat="server" CssClass="NormalRed" ControlToValidate="txtLastName" Display="Dynamic"></asp:requiredfieldvalidator>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td valign="top">
                                    <portal:address id="Address1" runat="server"></portal:address>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="1" border="0">
                                        <tbody>
                                            <tr valign="top">
                                                <td colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="SubHead" width="90">
                                                    <label for="<%=txtEmail.ClientID%>"><%= DotNetZoom.GetLanguage("S_F_Email") %>:</label></td>
                                                <td class="NormalBold" style="white-space: nowrap;">
                                                    <asp:textbox id="txtEmail" tabIndex="11" runat="server" cssclass="NormalTextBox" width="200px" maxlength="50"></asp:textbox>
                                                    &nbsp;*
                                                    <asp:requiredfieldvalidator id="valEmail" runat="server" CssClass="NormalRed" ControlToValidate="txtEmail" Display="Dynamic"></asp:requiredfieldvalidator>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="SubHead" width="90">
                                                    <label for="<%=txtFax.ClientID%>"><%= DotNetZoom.GetLanguage("Vendors_Fax") %>:</label></td>
                                                <td>
                                                    <asp:textbox id="txtFax" tabIndex="12" runat="server" cssclass="NormalTextBox" width="200px" maxlength="50"></asp:textbox>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="SubHead" width="90">
                                                    <label for="<%=txtWebsite.ClientID%>"><%= DotNetZoom.GetLanguage("Vendors_WebSite") %>:</label></td>
                                                <td>
                                                    <asp:textbox id="txtWebsite" tabIndex="13" runat="server" cssclass="NormalTextBox" width="200px" maxlength="100"></asp:textbox>
                                                </td>
                                            </tr>
                                            <tr id="rowVendor1" valign="top" runat="server">
                                                <td class="SubHead" width="90">
                                                    <label for="<%=cboLogo.ClientID%>"><%= DotNetZoom.GetLanguage("Vendors_Logo") %>:</label></td>
                                                <td>
                                                    <asp:dropdownlist id="cboLogo" tabIndex="14" runat="server" CssClass="NormalTextBox" DataValueField="Value" DataTextField="Text" Width="200px"></asp:dropdownlist>
                                                    &nbsp;
                                                    <asp:HyperLink id="cmdUpload" Runat="server" CssClass="CommandButton"></asp:HyperLink>
                                                    <asp:imagebutton id="UploadReturn" runat="server" EnableViewState="false" visible="False" height="16" width="16" imageurl="~/images/save.gif" style="border-width:0px"></asp:imagebutton>

                                                </td>
                                            </tr>
                                            <tr id="rowVendor2" valign="top" runat="server">
                                                <td class="SubHead" width="90">
                                                    <label for="<%=chkAuthorized.ClientID%>"><%= DotNetZoom.GetLanguage("U_Autorized") %>:</label></td>
                                                <td>
                                                    <asp:CheckBox id="chkAuthorized" tabIndex="15" Runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td id="colVendor" valign="top" runat="server">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr valign="top">
                                <td class="SubHead" align="center">
                                    <%= DotNetZoom.GetLanguage("Vendor_Classification") %>:
								</td>
                            </tr>
                            <tr valign="top">
                                <td valign="middle">
								<div style="overflow: auto; height: 120px; border: 1px silver solid;">
                    			<asp:datagrid id="grdClassifications" runat="server" Width="100%" EnableViewState="true" AutoGenerateColumns="false" CellSpacing="0" gridlines="none" BorderStyle="None">
                        			<Columns>
		                    			<asp:TemplateColumn ItemStyle-Width="20">
                                			<ItemTemplate>
											<input type=checkbox value='<%# DataBinder.Eval(Container.DataItem, "ClassificationId") %>' checked='<%# DataBinder.Eval(Container.DataItem,"IsAssociated") %>'  runat="server" id="Checkbox1" name="Checkbox1">
        									</ItemTemplate>
                            			</asp:TemplateColumn>										
                            			<asp:BoundColumn DataField="ClassificationName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                        			</Columns>
                    			</asp:datagrid>								
     							</div>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    &nbsp;</td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead" align="center">
                                    <label for="<%=txtKeyWords.ClientID%>"><%= DotNetZoom.GetLanguage("Vendor_KeyWord") %>:</label></td>
                            </tr>
                            <tr valign="top">
                                <td valign="middle">
                                    <asp:textbox id="txtKeyWords" tabIndex="17" Runat="server" Rows="10" TextMode="MultiLine" CssClass="NormalTextBox" Width="200px"></asp:textbox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <p>
                        <br>
                        <asp:linkbutton class="CommandButton" id="cmdUpdate" runat="server"  ></asp:linkbutton>
                        &nbsp;
                        <asp:linkbutton class="CommandButton" id="cmdCancel" runat="server" CausesValidation="False"  ></asp:linkbutton>
                        &nbsp;
                        <asp:linkbutton class="CommandButton" id="cmdDelete" runat="server" CausesValidation="False" ></asp:linkbutton>
                    </p>
                    <asp:placeholder id="pnlAudit" Runat="server">
				      <span class="Normal"><%= DotNetZoom.GetLanguage("label_update_by") %>&nbsp;<asp:Label id="lblCreatedBy" runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("label_update_the") %>&nbsp;<asp:Label id="lblCreatedDate" runat="server"></asp:Label>
                        <br>
                        </span>
                        <br>
                        <table cellspacing="0" cellpadding="0" width="500">
                            <tbody>
                                <tr valign="top">
                                    <td class="SubHead" width="100">
									<%= DotNetZoom.GetLanguage("Vendor_Banner_Imp") %>:
									</td>
                                    <td valign="middle" width="320">
                                        <asp:Label id="lblViews" runat="server" cssclass="Normal"></asp:Label></td>
                                </tr>
                                <tr valign="top">
                                    <td class="SubHead" width="100">
                                     <%= DotNetZoom.GetLanguage("Vendors_Clicks") %>:
									 </td>
                                    <td valign="middle" width="320">
                                        <asp:Label id="lblClickThroughs" runat="server" cssclass="Normal"></asp:Label></td>
                                </tr>
                                <tr valign="top">
                                    <td class="SubHead" width="100">
                                        <label for="<%=chkLog.ClientID%>"><%= DotNetZoom.GetLanguage("Vendors_SeeClicks") %></label></td>
                                    <td valign="middle" width="320">
                                        <asp:Checkbox id="chkLog" runat="server" CssClass="NormalTextBox" AutoPostBack="True"></asp:Checkbox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <asp:datagrid id="grdLog" runat="server" Width="100%" EnableViewState="true" AutoGenerateColumns="false" CellSpacing="3" gridlines="none" BorderStyle="None">
                            <Columns>
                                <asp:BoundColumn DataField="Search" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                                <asp:BoundColumn DataField="Requests" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                                <asp:BoundColumn DataField="LastRequest" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                            </Columns>
                        </asp:datagrid>
                        <br>
                    </asp:placeholder>
                </td>
            </tr>
            <tr id="rowBanner1" valign="top" height=* runat="server">
                <td colspan="2">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td align="left"></td>
                                <td align="left">
                                    <span class="Head"><%= DotNetZoom.GetLanguage("Vendor_Banner_Publicity") %></span>&nbsp;</td>
                                <td align="right">
                                    <asp:linkbutton id="cmdAddBanner" runat="server" CausesValidation="False"  cssclass="CommandButton" EnableViewState="true"></asp:linkbutton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr id="rowBanner2" runat="server">
                <td colspan="2">
                    <asp:datagrid id="grdBanners" runat="server" Width="100%" EnableViewState="true" AutoGenerateColumns="false" CellSpacing="3" gridlines="none" BorderStyle="None">
                        <Columns>
                            <asp:TemplateColumn ItemStyle-Width="20">
                                <ItemTemplate>
                                    <asp:HyperLink NavigateUrl='<%# FormatURL("BannerId",DataBinder.Eval(Container.DataItem,"BannerId")) %>' runat="server" ID="Hyperlink1">
                                        <asp:Image ImageURL="~/images/edit.gif" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' runat="server" ID="Hyperlink1Image" />
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="BannerName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                       			<asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
                                    <ItemTemplate>
									<%# GetBannerName(DataBinder.Eval(Container.DataItem,"BannerTypeId"))%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            <asp:BoundColumn DataField="URL" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                            <asp:BoundColumn DataField="Impressions" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundColumn DataField="CPM" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:#,##0.00}" />
                            <asp:BoundColumn DataField="Views" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundColumn DataField="ClickThroughs" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundColumn DataField="StartDate" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" DataFormatString="{0:d}" />
                            <asp:BoundColumn DataField="EndDate" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" DataFormatString="{0:d}" />
                        </Columns>
                    </asp:datagrid>
                    <p align="left">
                        <asp:linkbutton class="CommandButton" id="cmdBack" runat="server" CausesValidation="False" ></asp:linkbutton>
                    </p>
                    <br>
                </td>
            </tr>
        </tbody>
    </table>
</asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>