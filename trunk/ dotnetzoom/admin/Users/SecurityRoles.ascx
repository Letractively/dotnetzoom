<%@ Control Language="vb" codebehind="SecurityRoles.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.SecurityRoles" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>

<script language="javascript" type="text/javascript"  src="<%=dotnetzoom.glbpath + "controls/PopupCalendar.js"%>"></script>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750" border="0">
    <tbody>
        <tr>
            <td>
                <table cellspacing="1" cellpadding="1" >
                    <tbody>
						<tr>
							<td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="NormalBold" width="169">
                                <label for="<%=cboUsers.ClientID%>"><%= DotNetZoom.GetLanguage("Label_UserName") %></label></td>
                            <td class="NormalBold" width="171">
                                <label for="<%=cboRoles.ClientID%>"><%= DotNetZoom.GetLanguage("Role_PortalRole") %></label></td>
                            <td class="NormalBold" width="166">
                                <label for="<%=txtExpiryDate.ClientID%>"><%= DotNetZoom.GetLanguage("Role_ExpiryDate") %></label>
                            </td>
                            <td class="NormalBold">
                                &nbsp;</td>							
                        </tr>
                        <tr>
                            <td valign="top" width="169">
                                <asp:dropdownlist id="cboUsers" runat="server" CssClass="NormalTextBox" DataValueField="UserID" DataTextField="FullName" Width="168px"></asp:dropdownlist>
                            </td>
                            <td valign="top" width="171">
                                <asp:dropdownlist id="cboRoles" runat="server" CssClass="NormalTextBox" DataValueField="RoleID" DataTextField="RoleName" Width="164px"></asp:dropdownlist>
                            </td>
                            <td valign="top" width="166">
                                <asp:textbox id="txtExpiryDate" runat="server" CssClass="NormalTextBox" Width="151px"></asp:textbox>
                            </td>
							<td valign="top" width="240">
							<asp:HyperLink id="cmdExpiryCalendar" CssClass="CommandButton" Runat="server"></asp:HyperLink>
                             &nbsp;&nbsp;&nbsp;
                             <asp:linkbutton id="cmdAdd" runat="server" Width="159px" cssclass="CommandButton">Ajouter</asp:linkbutton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:CompareValidator id="valExpiryDate" runat="server" CssClass="NormalRed" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="txtExpiryDate"></asp:CompareValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr valign="top">
            <td width="755">
                &nbsp; 
                <asp:datagrid id="grdUserRoles" runat="server" Width="100%" gridlines="none" BorderStyle="None" CellPadding="4" CellSpacing="0" AutoGenerateColumns="false" EnableViewState="false" DataKeyField="UserRoleID" OnDeleteCommand="grdUserRoles_Delete">
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ToolTip='<%# DotNetZoom.GetLanguage("delete") %>' ID="cmdDeleteUserRole" Runat="server" AlternateText='<%# DotNetZoom.GetLanguage("delete") %>' CausesValidation="False" CommandName="Delete" ImageURL="~/images/delete.gif" BorderWidth="0" BorderStyle="none"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="FullName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
               			<asp:BoundColumn DataField="UserName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        				<asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
            				<ItemTemplate>
                				<asp:Label id="lblEmail" runat="server" text='<%# DotNetZoom.formatEmail(DataBinder.Eval(Container.DataItem, "Email"), page) %>'></asp:Label>
            				</ItemTemplate>
        				</asp:TemplateColumn>
		                <asp:BoundColumn DataField="RoleName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
                        <asp:BoundColumn DataField="ExpiryDate" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" DataFormatString="{0:d}" />
                    </Columns>
                </asp:datagrid>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:linkbutton id="cmdCancel" runat="server" CssClass="CommandButton" CausesValidation="False"></asp:linkbutton>
                &nbsp;&nbsp; 
                <asp:linkbutton id="cmdDelete" runat="server" CssClass="CommandButton" CausesValidation="False"></asp:linkbutton>
            </td>
        </tr>
    </tbody>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>