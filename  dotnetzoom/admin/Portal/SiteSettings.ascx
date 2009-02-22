<%@ Control Inherits="DotNetZoom.SiteSettings" codebehind="SiteSettings.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="Portal" TagName="ModuleEdit" Src="~/admin/tabs/ModuleEdit.ascx" %>
<script language="javascript" type="text/javascript"  src="<%=dotnetzoom.glbpath + "controls/PopupCalendar.js"%>"></script>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:datalist width="100%" id="dlTabs"  ItemStyle-Height="30"  OnItemCommand="dlTabs_ItemCommand" runat="Server" cssclass="TabHolder" separatorstyle-cssclass="TabSeparator" selecteditemstyle-cssclass="TabSelected" itemstyle-cssclass="TabDefault" itemstyle-wrap="False" cellpadding="0" repeatcolumns="5">
	<itemtemplate>
	<asp:LinkButton Runat="server"  Text='<%# Container.DataItem.Value %>' commandname='<%# Container.DataItem.Key %>' CausesValidation="False" ID="Linkbutton"></asp:LinkButton>
	</itemtemplate>
	<separatortemplate>
	<img src="/images/1x1.gif" alt="*" border="0">
	</separatortemplate>
</asp:datalist>
<asp:placeHolder id="Setting1" runat="server">
<!-- Debut PlaceHolder Setting1 -->	
<table class="TabPage" cellspacing="0" cellpadding="10" width="100%">
    <tbody>
    <tr>
         <td class="SubHead" align="left" width="187">
         <%= DotNetZoom.GetLanguage("SS_Label_Logo") %>:
		</td>
		<td align="left" width="188">
            <asp:dropdownlist id="cboLogo" runat="server" CssClass="NormalTextBox" Width="120" DataValueField="Value" DataTextField="Text"></asp:dropdownlist>
 		 </td>      
		<td class="SubHead" align="left" width="187">
		<%= DotNetZoom.GetLanguage("SS_Label_Background") %>:
		</td>
		<td align="left" width="188">
            <asp:dropdownlist id="cboBackground" runat="server" CssClass="NormalTextBox" Width="120" DataValueField="Value" DataTextField="Text"></asp:dropdownlist>
		</td>
         </tr>
    <tr>
	     <td colspan="4">
         <hr noshade="noshade" size="1" />
  		 </td>
    </tr>

	<tr>
         <td class="SubHead" align="left" width="187">
         <%= DotNetZoom.GetLanguage("SS_Label_UserInfo") %>:
		</td>
		<td align="left" width="188">
		<asp:checkboxlist id="chkUserInfo" runat="server" width="188" Font-Size="8pt" Font-Names="Verdana,Arial" cellspacing="0" cellpadding="0" RepeatColumns="1"></asp:checkboxlist>
		</td>      
        <td class="SubHead" align="left" width="187">
		<%= DotNetZoom.GetLanguage("SS_Label_SkinEdit") %>:
 		</td>
		<td align="left" width="188">
		<p>
		<asp:HyperLink cssclass="CommandButton" id="Portalcss" Text="Portal.css" runat="server" />
		</p><p><asp:HyperLink cssclass="CommandButton" id="TTTcss" Text="TTT.css" runat="server" />
		</p><p><asp:HyperLink cssclass="CommandButton" id="Portalskin" Text="Portal.skin" runat="server" />
		</p><p><asp:HyperLink cssclass="CommandButton" id="Portaleditskin" Text="PortalEdit.skin" runat="server" />
		 </p>
		 </td>
    </tr>
    <tr>
	     <td colspan="4">
         <hr noshade="noshade" size="1" />
  		 </td>
    </tr>
		<tr id="StyleRow" runat="server" visible="false">
            <td class="SubHead" colspan="4" align="left" width="750">
			<%= DotNetZoom.GetLanguage("SS_Label_Tigra") %>:
            &nbsp;<asp:checkbox id="chkStyleMenu" CssClass="NormalTextBox" Runat="server" AutoPostBack="True"></asp:checkbox>
        	</td>
		</tr>
		<tr>	
			<td class="SubHead" colspan="2" valign="top" align="left" width="375">
            <%= DotNetZoom.GetLanguage("SS_Label_ReplaceLOGO") %>:
			<br>
            <asp:textbox id="txtFlash" runat="server" CssClass="NormalTextBox" MaxLength="2000" width="375" TextMode="MultiLine" Rows="7"></asp:textbox>
            </td>
		    <td class="SubHead" colspan="2"  valign="top" align="left" width="375">
			<%= DotNetZoom.GetLanguage("SS_Label_Contener") %>:<br>
			<asp:checkbox cssclass="SubHead" id="chkcontainerInfo" autopostback="true" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
			<asp:checkbox cssclass="SubHead" id="chkadmincontainerInfo" autopostback="true" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
			<asp:checkbox cssclass="SubHead" id="chkeditcontainerInfo" autopostback="true" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
			<asp:checkbox cssclass="SubHead" id="chklogincontainerInfo" autopostback="true" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
			<asp:checkbox cssclass="SubHead" id="chkToolTipcontainerInfo" autopostback="true" runat="server" Font-Size="8pt" Font-Names="Verdana,Arial"></asp:checkbox>
			<portal:ModuleEdit id="ContainerEdit" visible="false" runat="server"></portal:ModuleEdit>
			<portal:ModuleEdit id="ContainerEdit1" visible="false" runat="server"></portal:ModuleEdit>
			<portal:ModuleEdit id="ContainerEdit2" visible="false" runat="server"></portal:ModuleEdit>
			<portal:ModuleEdit id="ContainerEdit3" visible="false" runat="server"></portal:ModuleEdit>
			<portal:ModuleEdit id="ContainerEdit4" visible="false" runat="server"></portal:ModuleEdit>
			</td>
		</tr>
    </tbody>
</table>
<!-- Fin PlaceHolder Setting1 -->	
</asp:placeHolder>
<asp:placeHolder id="Setting2" runat="server">
<!-- Debut PlaceHolder Setting2 -->	
<table class="TabPage" cellspacing="0" cellpadding="10" width="100%">
  <tbody>
    <tr>
         <td class="SubHead" width="187">
		 <%= DotNetZoom.GetLanguage("SS_Label_Admin") %>:
		 </td>
         <td width="188">
         <asp:dropdownlist id="cboAdministratorId" CssClass="NormalTextBox" Width="180" DataValueField="UserId" DataTextField="FullName" Runat="server"></asp:dropdownlist>
         </td>
		<td class="SubHead" width="187">
		<%= DotNetZoom.GetLanguage("SS_Label_Email") %>:
		</td>
		<td width="188">
		<asp:textbox id="txtregisteremail" runat="server" CssClass="NormalTextBox" MaxLength="50" width="180"></asp:textbox>
		</td>
	</tr>
	<tr>
	    <td class="SubHead" width="187">
		<%= DotNetZoom.GetLanguage("SS_Label_TimeZone") %>:&nbsp;<asp:Label id="LblTimeZone" runat="server" CssClass="NormalTextBox" ></asp:Label>
		</td>
        <td width="188">
		<asp:DropDownList id="ddlTimeZone" Width="180px" DataValueField="Zone" DataTextField="Description" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>
        </td>
        <td class="SubHead" width="187">
		<%= DotNetZoom.GetLanguage("SS_Label_TypeRegistration") %>
        </td>
        <td width="188">
        <asp:radiobuttonlist id="optUserRegistration" CssClass="Normal" RepeatDirection="Horizontal" Runat="server">
        <asp:ListItem Value="0"></asp:ListItem>
        <asp:ListItem Value="1"></asp:ListItem>
        <asp:ListItem Value="2"></asp:ListItem>
        <asp:ListItem Value="3"></asp:ListItem>
        </asp:radiobuttonlist>
        </td>
   </tr>
  	<tr id="sllline" runat="server" visible="false">
	    <td class="SubHead" width="187">
		<%= DotNetZoom.GetLanguage("SS_Use_SSL") %>:&nbsp;<asp:Label id="Label4" runat="server" CssClass="NormalTextBox" ></asp:Label>
		</td>
        <td width="188">
            <asp:CheckBox ID="SSLCheckBox" runat="server" />
        </td>
        <td class="SubHead" width="187">
        </td>
        <td width="188">
        </td>
   </tr> 
   
   <tr>
   		<td class="SubHead" width="187">
   		<%= DotNetZoom.GetLanguage("SS_Label_Vendors") %>:
        </td>
		<td width="188">
        <asp:radiobuttonlist id="optBannerAdvertising" CssClass="Normal" RepeatDirection="Horizontal" Runat="server">
        <asp:ListItem Value="0"></asp:ListItem>
        <asp:ListItem Value="1"></asp:ListItem>
        <asp:ListItem Value="2"></asp:ListItem>
        </asp:radiobuttonlist>
        </td>
        <td class="SubHead" width="187">
        <%= DotNetZoom.GetLanguage("SS_Label_Currency") %>:
		</td>
        <td width="188">
        <asp:dropdownlist id="cboCurrency" CssClass="NormalTextBox" Width="180" DataValueField="Code" DataTextField="Description" Runat="server"></asp:dropdownlist>
        </td>
   </tr>
   <tr>
        <td class="SubHead" width="187">
        <%= DotNetZoom.GetLanguage("SS_Label_Processor") %>:
		</td>
        <td width="188">
        <asp:dropdownlist id="cboProcessor" CssClass="NormalTextBox" Width="180" DataValueField="URL" DataTextField="Processor" Runat="server"></asp:dropdownlist>
        </td>
        <td class="SubHead" width="187">
        <%= DotNetZoom.GetLanguage("SS_Label_Processor_Code") %>:
		</td>
        <td width="188">
        <asp:textbox id="txtUserId" runat="server" CssClass="NormalTextBox" MaxLength="50" width="180"></asp:textbox>
        </td>
   </tr>
   <tr>
       <td class="SubHead" width="187">
	   <%= DotNetZoom.GetLanguage("SS_Label_Processor_Password") %>:
	   </td>
       <td width="188">
       <asp:textbox id="txtPassword" runat="server" CssClass="NormalTextBox" MaxLength="50" width="180" TextMode="Password"></asp:textbox>
       </td>
       <td align="center" width="375" colspan="2">
           <asp:linkbutton id="cmdProcessor" runat="server" cssclass="CommandButton"></asp:linkbutton>
       </td>
   </tr>
  </tbody>
</table>
<!-- Fin PlaceHolder Setting2 -->	
</asp:placeHolder>
<asp:placeHolder id="Setting3" runat="server">
<!-- Debut PlaceHolder Setting3 -->	
<table class="TabPage" cellspacing="0" cellpadding="10" width="100%">
	<tbody>
	<tr>
    	<td colspan="2" align="left">
           <span class="Head"><%= DotNetZoom.GetLanguage("SS_Head_Language") %></span> 
        </td>
    </tr>
     <tr>
            <td colspan="2" align="left" width="750">
 			<table>
			<tr>
			<td align="left" width="200">
			<span class="SubHead">
			<%= DotNetZoom.GetLanguage("SS_Label_Language_Default") %>:</span><br>
			<asp:DropDownList id="ddlSiteLanguage" Width="100px" DataValueField="language" DataTextField="SettingValue" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>
			</td>
			<td class="header" align="left" width="575"><span class="SubHead">
			<%= DotNetZoom.GetLanguage("SS_Label_Language_ToUse") %>:</span><br><asp:checkboxlist id="chkAuthLanguage" runat="server" width="575" Font-Size="8pt" Font-Names="Verdana,Arial" cellspacing="0" cellpadding="0" RepeatColumns="4"></asp:checkboxlist>
			</td>
			</tr>
			</table>
			</td>
		</tr>	
    <tr>
	     <td colspan="2">
         <hr noshade="noshade" size="1" />
  		 </td>
    </tr>
	<tr>
    	<td colspan="2" align="left">
           <span class="Head"><%= DotNetZoom.GetLanguage("SS_Head_Language_Info") %>&nbsp;</span><asp:DropDownList id="ddlLanguage" Width="100px" AutoPostBack="True" DataValueField="language" DataTextField="SettingValue" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>
        </td>
    </tr>
 	<tr>
         <td colspan="2" align="left" width="750">
		 <span class="SubHead"><%= DotNetZoom.GetLanguage("SS_Label_Mod_Info") %>:</span>
         &nbsp;&nbsp;&nbsp;<asp:HyperLink cssclass="CommandButton"  id="PortalTerms" runat="server" />
		&nbsp;&nbsp;&nbsp;<asp:HyperLink cssclass="CommandButton" id="PortalPrivacy" runat="server" />
		 </td>      
        </tr>
      <tr>
         <td align="left">
		 <span class="SubHead"><%= DotNetZoom.GetLanguage("SS_Site_Title") %>&nbsp;<%= ddlLanguage.SelectedItem.Text %>:</span>
		<br>
        <asp:textbox id="txtPortalName" runat="server" CssClass="NormalTextBox" MaxLength="128" width="375"></asp:textbox>
        </td>
		<td align="left">
    	 <span class="SubHead"><%= DotNetZoom.GetLanguage("SS_Site_Title_Bottom") %>&nbsp;<%= ddlLanguage.SelectedItem.Text %>:</span>
		<br>
        <asp:textbox id="txtFooterText" runat="server" CssClass="NormalTextBox" MaxLength="100" width="375"></asp:textbox>
        </td>
     </tr>
       <tr>
	        <td valign="top" align="left">
	    	<span class="SubHead"><%= DotNetZoom.GetLanguage("SS_Site_Description") %>&nbsp;<%= ddlLanguage.SelectedItem.Text %>:</span>
			<br>
            <asp:textbox id="txtDescription" runat="server" CssClass="NormalTextBox" MaxLength="500" width="375" TextMode="MultiLine" Rows="3"></asp:textbox>
            </td>
            <td align="left">
			<span class="SubHead"><%= DotNetZoom.GetLanguage("SS_Site_Keywords") %>&nbsp;<%= ddlLanguage.SelectedItem.Text %>:</span>
			<br>
            <asp:textbox id="txtKeyWords" runat="server" CssClass="NormalTextBox" MaxLength="500" width="375" TextMode="MultiLine" Rows="3"></asp:textbox>
            <br><br>
            <asp:linkbutton id="cmdGoogle" runat="server" cssclass="CommandButton"></asp:linkbutton>
            </td>
		</tr>
		<tr>
			<td align="left">
			<span class="SubHead"><%= DotNetZoom.GetLanguage("SS_txtlogin") %>&nbsp;<%= ddlLanguage.SelectedItem.Text %>:</span>
			<br>
            <asp:textbox id="txtLogin" runat="server" CssClass="NormalTextBox" MaxLength="1000" width="375px" TextMode="MultiLine" Rows="7"></asp:textbox>
			</td>
    	    <td align="left">
			<span class="SubHead"><%= DotNetZoom.GetLanguage("SS_txtRegistration") %>&nbsp;<%= ddlLanguage.SelectedItem.Text %>:</span>
            <br>
			<asp:textbox id="txtRegistration" runat="server" CssClass="NormalTextBox" MaxLength="1000" width="375px" TextMode="MultiLine" Rows="7"></asp:textbox>
			</td>
         </tr>
        <tr>
        	<td align="left">
			<span class="SubHead"><%= DotNetZoom.GetLanguage("SS_txtSignup") %>&nbsp;<%= ddlLanguage.SelectedItem.Text %>:</span>
			<br>
            <asp:textbox id="txtSignup" runat="server" CssClass="NormalTextBox" MaxLength="1000" width="375px" TextMode="MultiLine" Rows="7"></asp:textbox>
            </td>
		    <td id="DemoCell" runat="server" visible="false" align="left">
		    <span class="SubHead"><%= DotNetZoom.GetLanguage("SS_DemoDirectives") %>&nbsp;<%= ddlLanguage.SelectedItem.Text %>:</span>
		    <br>
            <asp:textbox id="txtInstructionDemo" runat="server" CssClass="NormalTextBox" MaxLength="2000" width="375" TextMode="MultiLine" Rows="7"></asp:textbox>
            </td>
        </tr>
    </tbody>
</table>
<!-- Fin PlaceHolder Setting3 -->	
</asp:placeHolder>
<asp:placeHolder id="Setting4" runat="server">
<!-- Debut PlaceHolder Setting4 -->	
<table class="TabPage" cellspacing="0" cellpadding="10" width="100%">
    <tbody>
       <tr id="SiteRow7" runat="server" visible="false">
                                <td colspan="2" align="left">
                                <span class="Head"><%= DotNetZoom.GetLanguage("SS_Head_Demo") %></span><font size="1">&nbsp;(/<%= DotNetZoom.GetLanguage("N") %>.default.aspx?def=demo)</font> 
                            </td>
                        </tr>

						<tr id="SiteRow6" runat="server" visible="false">
            				<td class="SubHead" width="300">
							<%= DotNetZoom.GetLanguage("SS_Head_Demo_Allow") %>
                			</td>
            				<td>
                			<asp:checkbox id="chkDemoSignup" runat="server" CssClass="NormalTextBox"></asp:checkbox>
							</td>
        				</tr>
						<asp:PlaceHolder ID="pnlDemoContent" Runat="server">
						<tr>
            				<td class="SubHead" width="300">
							<%= DotNetZoom.GetLanguage("SS_Label_Demo_Domain") %>:
							</td>
            				<td>
                            <asp:radiobuttonlist id="chkDemoDomain" CssClass="Normal" RepeatDirection="Horizontal" Runat="server">
                                    <asp:ListItem Value="Y"></asp:ListItem>
                                    <asp:ListItem Value="N"></asp:ListItem>
                            </asp:radiobuttonlist>
							</td>
        				</tr>
        				<tr>
            				<td class="SubHead" width="300">
							<%= DotNetZoom.GetLanguage("SS_Label_Allow_Demo_Create") %>: 
            				</td>
            				<td>
                				<asp:checkboxlist id="authRoles" runat="server" width="300" Font-Size="8pt" Font-Names="Verdana,Arial" RepeatColumns="2"></asp:checkboxlist>
            				</td>
			        	</tr>
						</asp:PlaceHolder>		
    </tbody>
</table>
<!-- Fin PlaceHolder Setting4 -->	
</asp:placeHolder>
<asp:placeHolder id="Setting5" runat="server">
<!-- Debut PlaceHolder Setting5 -->	
<table class="TabPage" cellspacing="0" cellpadding="10" width="100%">
    <tbody>
		<tr>
		 <td>
		 <table cellspacing="0" cellpadding="0" width="100%">
    		<tbody>
        	<tr>
         <td align="left" colspan="2">
         <span class="Head"><%= DotNetZoom.GetLanguage("SS_Head_PortalInfo") %></span> 
         </td>
        </tr>
        <tr>
         <td colspan="2">
         <hr noshade="noshade" size="1" />
        </td>
        </tr>
    </tbody>
</table>
<!-- Debut PortalRow2 -->	

                <asp:datagrid id="grdModuleDefs" width="750" runat="server" CssClass="Normal"  OnCancelCommand="grdModuleDefs_CancelEdit" OnUpdateCommand="grdModuleDefs_Update" OnEditCommand="grdModuleDefs_Edit" AutoGenerateColumns="False" CellSpacing="4" CellPadding="4" Border="0" gridlines="none" DataKeyField="ModuleDefID">
                    <Columns>
                        <asp:TemplateColumn ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <asp:imagebutton id="cmdEditModuleDefs" runat="server" causesvalidation="false" commandname="Edit" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' ImageURL="~/images/edit.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:imagebutton id="cmdSaveModuleDefs" runat="server" causesvalidation="false" commandname="Update" AlternateText='<%# DotNetZoom.GetLanguage("enregistrer") %>' ImageURL="~/images/save.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                <asp:imagebutton id="cmdCancelModuleDefs" runat="server" causesvalidation="false" commandname="Cancel" AlternateText='<%# DotNetZoom.GetLanguage("annuler") %>' ImageURL="~/images/cancel.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-CssClass="Normal" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Image runat="server" ImageUrl='<%# IIf(DataBinder.Eval(Container.DataItem, "Subscribed") = 1, "/images/checked.gif", "/images/unchecked.gif") %>' AlternateText='<%# IIf(DataBinder.Eval(Container.DataItem, "Subscribed") = 1, "Checked", "UnChecked") %>' ID="Image2" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label id="lblCheckBox2" runat="server" /> 
                                <asp:CheckBox runat="server" id="Checkbox2" Checked='<%# IIf(DataBinder.Eval(Container.DataItem, "Subscribed") = 1, "True", "False") %>' />
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="FriendlyName" ReadOnly="True" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                        <asp:BoundColumn DataField="Description" ReadOnly="True" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" text='<%# FormatFee(DataBinder.Eval(Container.DataItem, "HostFee")) %>' id="Label1" /> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label id="lblHostingFee" runat="server" />
			                    <asp:TextBox runat="server" id="txtHostingFee" Columns="10" MaxLength="50" Text='<%# FormatFee(DataBinder.Eval(Container.DataItem, "HostFee")) %>' Enabled='<%# DotNetZoom.PortalSecurity.IsSuperUser() %>' />
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-CssClass="NormalBold" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <asp:Label runat="server" text='<%# FormatCurrency %>' id="Label2" /> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label runat="server" text='<%# FormatCurrency %>' id="Label3" /> 
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:datagrid>
<table id="PortalRow3" runat="server" visible="true" cellspacing="0" cellpadding="0" width="100%">
    <tbody>
        <tr>
                            <td class="SubHead" width="300">
							<%= DotNetZoom.GetLanguage("SS_Label_PortalBasicFee") %>:</td>
                            <td class="Normal">
								<asp:textbox id="txtHostFee" Visible="False" Enabled="False" runat="server" CssClass="NormalTextBox" MaxLength="10" width="30"></asp:textbox> 
	                            <asp:Label id="lblHostFee" runat="server" cssclass="Normal"></asp:Label>&nbsp;&nbsp;<asp:Label id="lblHostCurrency" runat="server" cssclass="NormalBold"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="SubHead" width="300">
							<%= DotNetZoom.GetLanguage("SS_Label_PortalExtraFee") %>:</td>
                            <td class="Normal">
                                <asp:Label id="lblModuleFee" runat="server" cssclass="Normal"></asp:Label>&nbsp;&nbsp;<asp:Label id="lblModuleCurrency" runat="server" cssclass="NormalBold"></asp:Label></td>
                        </tr>
                        <tr>
         					<td colspan="2">
         					<hr noshade="noshade" size="1" />
        					</td>
                        </tr>
                        <tr>
                            <td class="SubHead" width="300">
							<%= DotNetZoom.GetLanguage("SS_Label_TotalPortalFee") %>:</td>
                            <td class="Normal">
                                <asp:Label id="lblTotalFee" runat="server" cssclass="Normal"></asp:Label>&nbsp;&nbsp;<asp:Label id="lblTotalCurrency" runat="server" cssclass="NormalBold"></asp:Label></td>
                        </tr>
		    </tbody>
</table>
<table cellspacing="0" cellpadding="0" width="100%">
    <tbody>
		<tr><td>&nbsp;</td></tr>
       <tr>
			<td class="SubHead" align="left" width="300">
			<%= DotNetZoom.GetLanguage("SS_PortalDiskSpace") %>:
			</td>
			<td>
       		<asp:textbox id="txtHostSpace" Enabled="False" runat="server" CssClass="NormalTextBox" MaxLength="4" width="30"></asp:textbox>
       		</td>
		</tr>
		<tr>
       		<td class="SubHead" align="left" width="300">
			<%= DotNetZoom.GetLanguage("SS_SiteLogDays") %>:
			</td>
			<td>
       		<asp:textbox id="txtSiteLogHistory" Enabled="False" runat="server" CssClass="NormalTextBox" MaxLength="3" width="30"></asp:textbox>
	   		</td>
  	   </tr>
	   	<tr><td>&nbsp;</td></tr>
    </tbody>
</table>
<table id="PortalRow5" runat="server" visible="true" cellspacing="0" cellpadding="0" width="100%">
    <tbody>
        <tr>
            <td class="SubHead" width="300">
			<%= DotNetZoom.GetLanguage("SS_PortalExpired") %>:</td>
            <td class="NormalTextBox">
                <asp:textbox visible="True" Enabled="False" id="txtExpiryDate" runat="server" CssClass="NormalTextBox" MaxLength="15" width="150"></asp:textbox>
	            <asp:HyperLink visible="False" id="cmdExpiryCalendar" CssClass="CommandButton" Runat="server"></asp:HyperLink>
    	        <asp:CompareValidator id="valExpiryDate" runat="server" CssClass="NormalRed" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="txtExpiryDate"></asp:CompareValidator>
			</td>
        </tr>
		<tr><td>&nbsp;</td></tr>
        <tr id="PortalRow7" runat="server" visible="true">
            <td colspan="2">
                <asp:linkbutton id="cmdRenew" runat="server" cssclass="CommandButton"></asp:linkbutton>
            </td>
        </tr>
    </tbody>
</table>
<table id="SiteTable1" runat="server" visible="false" cellspacing="0" cellpadding="0" width="100%">
    <tbody>	
       <tr>
         <td colspan="3">
         <hr noshade="noshade" size="1" />
         </td>
       </tr>
	
        <tr>
            <td class="SubHead" width="300" valign="top">
            <%= DotNetZoom.GetLanguage("SS_PortalAlias") %>:
           </td>
           <td colspan="2">
            <table cellspacing="0" cellpadding="0" width="100%">
            <tr><td id="ssltd" width="30" runat="server" class="NormalBold" align="left">Sub&nbsp;&nbsp;ssl&nbsp;</td><td align="center" colspan="2" class="NormalBold"><%= DotNetZoom.GetLanguage("P_Alias") %></td></tr>
                <tr>
                    <td class="SubHead" align="left" colspan="2">
                    &nbsp;<asp:CheckBox ID="sslSubDomainBox1" runat="server"  Checked="False"/>
                    &nbsp;<asp:CheckBox ID="sslCheckBox1" runat="server"  Checked="False"/>
                    &nbsp;&nbsp;<asp:textbox id="txtPortalAlias" runat="server" CssClass="NormalTextBox" MaxLength="50" Width="300px"></asp:textbox>
                    &nbsp;&nbsp;<asp:ImageButton id="AddSetting" ImageURL="~/images/add.gif" Width="16px" Height="16px" runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead" align="left" colspan="2">
			        <asp:datagrid id="grdPortalsAlias" runat="server" gridlines="none" BorderStyle="None" CellPadding="4" CellSpacing="0" AutoGenerateColumns="false" EnableViewState="true" Width="100%">
                        <Columns>
                        <asp:TemplateColumn  ItemStyle-Width="300">
                            <ItemTemplate>
                            <asp:CheckBox ID="sslSubDomainBox" runat="server"  Checked='<%# DataBinder.Eval(Container, "DataItem.SubPortal") %>' visible='<%# sslCheckBox.checked %>'/>
                            &nbsp;<asp:CheckBox ID="sslCheckBox" runat="server"  Checked='<%# DataBinder.Eval(Container, "DataItem.ssl") %>' visible='<%# sslCheckBox.checked %>'/>
                            &nbsp;&nbsp;<asp:TextBox id="txtRename" runat="server" MaxLength="50" Width="300px" Text='<%# DataBinder.Eval(Container, "DataItem.PortalAlias") %>'></asp:TextBox>
                            &nbsp;&nbsp;<asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("enregistrer") %>' id="imgEditOK" ImageURL="~/images/save.gif" CommandName="EditOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.PortalAlias") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
                            &nbsp;&nbsp;<asp:ImageButton id="DelAlias" tooltip='<%# DotNetZoom.GetLanguage("delete") %>' visible="true" ImageURL="~/images/delete.gif" CommandName="DeleteOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.PortalAlias") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="True"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        </Columns>
                    </asp:datagrid>
                    </td>
                </tr>
            </table>
           </td>
        </tr>
    </tbody>
</table>
</td>
</tr>
</tbody>
</table>
</asp:placeHolder>
<table class="TabPage" cellspacing="5" cellpadding="5" width="100%">
    <tbody>
		<tr>
         <td align="left">
               <asp:linkbutton cssclass="CommandButton" id="cmdUpdate" runat="server"></asp:linkbutton>
                &nbsp;&nbsp; 
                <asp:linkbutton cssclass="CommandButton" id="cmdCancel" runat="server"></asp:linkbutton>
                &nbsp;&nbsp; 
                <asp:linkbutton cssclass="CommandButton" id="cmdDelete" runat="server" Visible="False"></asp:linkbutton>
            </td>
        </tr>
    </tbody>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>