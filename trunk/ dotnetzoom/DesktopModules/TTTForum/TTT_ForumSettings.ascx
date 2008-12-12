<%@ Control Language="vb" codebehind="TTT_ForumSettings.ascx.vb" autoeventwireup="false" Inherits="DotNetZoom.TTT_ForumSettings" %>
<%@ Register TagPrefix="TTT" TagName="ForumAdminNav" Src="TTT_ForumAdminNavigator.ascx" %>
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="780">
    <tbody>
        <tr>
            <td class="TTTAltHeader" align="left" height="28">
                <span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_ForumSetting") %></span></td>
            <td class="TTTAltHeader" style="white-space: nowrap;" align="right" runat="server">
                <TTT:ForumAdminNav id="ctlForumAdminNav" runat="Server"></TTT:ForumAdminNav>
            </td>
        </tr>
            <tr>
                <td class="TTTHeader" align="left" width="60%" colspan="2" height="28">
                    <span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_AdminParam") %></span></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="TTTBorder" cellspacing="1" cellpadding="0" width="100%">
                        <tbody>
						<!-- Setting1 -->
						<tr class="TTTAltHeader">
						<td colspan="2" height="28">
						<a href="javascript:onclick=ShowHide('<%= me.ClientID & "_" & "1" %>','img<%= me.ClientID & "_" & "1" %>');">
						<img alt="+" id="img<%= me.ClientID & "_" & "1" %>" src="images/1x1.gif" height="12" width="12" style="border-width:0px; background: url('/images/uostrip.gif') no-repeat; background-position: 0px -407px;">
						</a>
						<a href="javascript:onclick=ShowHide('<%= me.ClientID & "_" & "1" %>','img<%= me.ClientID & "_" & "1" %>');">
						<span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_ImgSettings") %></span>
						</a>
						</td>
						</tr>
                            <tr id="<%= me.ClientID & "_" & "1" %>" style="display: none;">
							 <td Colspan="2">
							  <table>
							   <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_ImgDir") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtImageFolder" runat="server" Enabled="true" Columns="26" width="380" cssclass="NormalTextBox"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_AvDir") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtAvatarFolder" runat="server" Enabled="false" Columns="26" width="380" cssclass="NormalTextBox"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_AvID") %>:</td>
                                <td class="TTTRow" style="white-space: nowrap;" align="left">
                                 
                                <asp:label id="lblAvatar" runat="server" EnableViewState="false" Visible="False" CssClass="CommandButton">
				                <asp:hyperlink id="cmdavatar" runat="server"  Visible="False"><img src="/images/TTT/TTT_s_jpg.gif" AlT="*" Border="0"></asp:hyperlink>
                                <asp:hyperlink id="lnkavatar" runat="server" Visible="False"></asp:hyperlink>
				                </asp:label>

               
                                    <asp:placeholder id="pnlAvatarConfig" Runat="server">
                                        <asp:imagebutton id="btnAvatarConfig" runat="server" CommandName="Add" AlternateText="Config" ImageUrl="~/Images/Avatars/smiley_smile.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                        <asp:Label id="lblAvatarInfo" runat="server" forecolor="Red"></asp:Label>
                                    </asp:placeholder>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_AvYes") %></td>
                                <td class="TTTRow" align="left">
                                    <asp:checkbox id="chkUserAvatar" runat="server" CssClass="TTTNormal"></asp:checkbox>
                                </td>
                            </tr>
						   </table>
						  </td>
						  </tr>
						<!-- Setting2 -->
						<tr class="TTTAltHeader">
						<td colspan="2" height="28">
						<a href="javascript:onclick=ShowHide('<%= me.ClientID & "_" & "2" %>','img<%= me.ClientID & "_" & "2" %>');">
						<img alt="+" id="img<%= me.ClientID & "_" & "2" %>" src="images/1x1.gif" height="12" width="12" style="border-width:0px; background: url('/images/uostrip.gif') no-repeat; background-position: 0px -407px;">
						</a>
						<a href="javascript:onclick=ShowHide('<%= me.ClientID & "_" & "2" %>','img<%= me.ClientID & "_" & "2" %>');">
						<span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_EMailYes") %></span> <span class="TTTNormal">&nbsp;
									<%= DotNetZoom.getlanguage("F_EMailInfo") %></span>
						</a>
						</td>
						</tr>
                            <tr id="<%= me.ClientID & "_" & "2" %>" style="display: none;">
							 <td Colspan="2">
							  <table>
							   <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_EMailS") %>
                                </td>
                                <td class="TTTRow" align="left">
                                    <asp:checkbox id="chkNotify" runat="server" CssClass="TTTNormal"></asp:checkbox>
									&nbsp; <span class="TTTNormal"><%= DotNetZoom.getlanguage("F_EMailInfo1") %></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_EMailAdd") %>&nbsp;</td>
                                <td class="TTTRow" align="left">
                                    <asp:checkbox id="chkSenderAddress" runat="server" CssClass="TTTNormal"></asp:checkbox>
									&nbsp; <span class="TTTNormal"><%= DotNetZoom.getlanguage("F_EMailInfo2") %></span></td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_EMailAut") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtAutomatedAddress" runat="server" Columns="26" width="380" cssclass="NormalTextBox"></asp:textbox>
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_EMailDom") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtEmailDomain" runat="server" Columns="26" width="380" cssclass="NormalTextBox"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_EMailFormat") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:RadioButtonList id="optHTMLFormat" runat="server" CssClass="TTTNormal" RepeatDirection="Horizontal" Width="189px">
                                        <asp:ListItem Value="False"></asp:ListItem>
                                        <asp:ListItem Value="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
						   </table>
						  </td>
						  </tr>
						<!-- Setting3 -->
						<tr class="TTTAltHeader">
						<td colspan="2" height="28">
						<a href="javascript:onclick=ShowHide('<%= me.ClientID & "_" & "3" %>','img<%= me.ClientID & "_" & "3" %>');">
						<img alt="+" id="img<%= me.ClientID & "_" & "3" %>" src="images/1x1.gif" height="12" width="12" style="border-width:0px; background: url('/images/uostrip.gif') no-repeat; background-position: 0px -407px;">
						</a>
						<a href="javascript:onclick=ShowHide('<%= me.ClientID & "_" & "3" %>','img<%= me.ClientID & "_" & "3" %>');">
						<span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_ForumFormat") %></span>
						</a>
						</td>
						</tr>
                            <tr id="<%= me.ClientID & "_" & "3" %>" style="display: none;">
							 <td Colspan="2">
							  <table>
							   <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_ThreadP") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtTheardsPerPage" runat="server" Columns="26" width="75px" cssclass="NormalTextBox"></asp:textbox>
                                    <asp:regularexpressionvalidator id="Regularexpressionvalidator6" runat="server" CssClass="TTTNormal" ControlToValidate="txtTheardsPerPage" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_PostPage") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtPostsPerPage" runat="server" Columns="26" width="75px" cssclass="NormalTextBox"></asp:textbox>
                                    <asp:regularexpressionvalidator id="Regularexpressionvalidator7" runat="server" CssClass="TTTNormal" ControlToValidate="txtPostsPerPage" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_AvMaxS") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtAvatarSize" runat="server" Columns="26" width="75px" cssclass="NormalTextBox"></asp:textbox>
                                    <span class="TTTNormal"><%= DotNetZoom.getlanguage("Gal_KB") %></span>
                                    <asp:regularexpressionvalidator id="Regularexpressionvalidator3" runat="server" CssClass="TTTNormal" ControlToValidate="txtAvatarSize" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_Stats") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtStatsUpdateInterval" runat="server" Columns="26" width="75px" cssclass="NormalTextBox"></asp:textbox>
                                    <span class="TTTNormal"><%= DotNetZoom.getlanguage("F_StatsH") %></span>
                                    <asp:regularexpressionvalidator id="Regularexpressionvalidator1" runat="server" CssClass="TTTNormal" ControlToValidate="txtStatsUpdateInterval" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator>
                                </td>
                            </tr>
						   </table>
						  </td>
						  </tr>

							<!-- Setting4 -->
						<tr class="TTTAltHeader">
						<td colspan="2" height="28">
						<a href="javascript:onclick=ShowHide('<%= me.ClientID & "_" & "4" %>','img<%= me.ClientID & "_" & "4" %>');">
						<img alt="+" id="img<%= me.ClientID & "_" & "4" %>" src="images/1x1.gif" height="12" width="12" style="border-width:0px; background: url('/images/uostrip.gif') no-repeat; background-position: 0px -407px;">
						</a>
						<a href="javascript:onclick=ShowHide('<%= me.ClientID & "_" & "4" %>','img<%= me.ClientID & "_" & "4" %>');">
						<span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_UOInt") %></span>
						</a>
						</td>
						</tr>
                            <tr id="<%= me.ClientID & "_" & "4" %>" style="display: none;">
							 <td Colspan="2">
							  <table>
							   <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UOuse") %></td>
                                <td class="TTTRow" valign="top" style="white-space: nowrap;" align="left">
                                    <asp:checkbox id="chkUserOnline" runat="server" CssClass="TTTNormal"></asp:checkbox>
                                    &nbsp;
                                </td>
                            </tr>
						   </table>
						  </td>
						  </tr>

							<!-- Setting5 -->
						<tr class="TTTAltHeader">
						<td colspan="2" height="28">
						<a href="javascript:onclick=ShowHide('<%= me.ClientID & "_" & "5" %>','img<%= me.ClientID & "_" & "5" %>');">
						<img alt="+" id="img<%= me.ClientID & "_" & "5" %>" src="images/1x1.gif" height="12" width="12" style="border-width:0px; background: url('/images/uostrip.gif') no-repeat; background-position: 0px -407px;">
						</a>
						<a href="javascript:onclick=ShowHide('<%= me.ClientID & "_" & "5" %>','img<%= me.ClientID & "_" & "5" %>');">
						<span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_GalInt") %></span>
						</a>
						</td>
						</tr>
                            <tr id="<%= me.ClientID & "_" & "5" %>" style="display: none;">
							 <td Colspan="2">
							  <table>
							   <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_GalHeight") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtImageHeight" runat="server" Columns="26" width="75px" cssclass="NormalTextBox" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_GalWith") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtImageWidth" runat="server" Columns="26" width="75px" cssclass="NormalTextBox" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="148" height="24">
                                    <%= DotNetZoom.getlanguage("F_GalImgT") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtImageExtensions" runat="server" Columns="26" width="380" cssclass="NormalTextBox" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
						   </table>
						  </td>
						  </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        <tr>
            <td class="TTTAltHeader" align="left" colspan="2" height="28">&nbsp;
				<asp:button cssclass="button" id="btnUpdate" runat="server" CommandName="save" ></asp:button>&nbsp;
            </td>
        </tr>

    </tbody>
</table>