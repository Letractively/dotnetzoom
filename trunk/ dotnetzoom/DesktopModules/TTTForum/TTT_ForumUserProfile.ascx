<%@ Control Language="vb" autoeventwireup="false" Explicit="True" codebehind="TTT_ForumUserProfile.ascx.vb" Inherits="DotNetZoom.TTT_ForumUserProfile" %>
<asp:placeholder id="pnlUserProfile" Runat="server">
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="750" align="center">
     <tbody>
          <tr>
               <td class="TTTHeader" width="100%" height="28">
                    <p>
                        <span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_UserProfile") %></span>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="TTTAltHeader" height="28">
                    <asp:Label id="lblInfo" width="100%" cssclass="TTTNormal" forecolor="Red" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="TTTSubHeader" width="180"></td>
                                <td class="TTTAltHeader" height="28">
                                    <span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_UserProfileParam") %></span></td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserID") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtUserID" runat="server" cssclass="NormalTextBox" width="340" Columns="26" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    <p>
                                        &nbsp;<%= DotNetZoom.getlanguage("F_UserCode") %>:
                                    </p>
                                </td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtUserName" runat="server" cssclass="NormalTextBox" width="340" Columns="26" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserName") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtFullName" runat="server" cssclass="NormalTextBox" width="340" Columns="26" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserRTE") %></td>
                                <td class="TTTRow" align="left">
                                    <asp:checkbox id="chkRichText" CssClass="TTTNormal" runat="server" Checked="True"></asp:checkbox>
									&nbsp;<%= DotNetZoom.getlanguage("F_UserRTEInfo") %>
                                </td>
                            </tr>
                            <asp:placeholder id="pnlAvatarDefault" Runat="server">
<script language="JavaScript" type="text/javascript">
		function OpeniconeWindow(tabid , idParent)
			{
				var m = window.open('<%= dotnetzoom.glbPath %>DesktopModules/TTTForum/icone.aspx?L=<%= DotNetZoom.GetLanguage("N") %>&tabid=' + tabid + '&parentID=' + idParent, 'icone', 'width=800,height=600,left=100,top=100');
				m.focus();
			}
		function SetUrl(idParentValue, idParent)
			{
				eval('var txt = document.Form1.' + idParent + ';');
				txt.value =  idParentValue;
				eval('var xtxt = document.Form1.<%= imgAvatar.clientID %>;');
				xtxt.src = idParentValue;
				xtxt.title = idParentValue;
				xtxt.alt = idParentValue;
				
			}
</script>
                                <tr>
                                    <td class="TTTSubHeader" width="180">
                                        &nbsp;<%= DotNetZoom.getlanguage("F_UserSelectAv") %>:</td>
                                    <td class="TTTRow" valign="middle" align="left" height="28">
                     
                                    <asp:hyperlink id="lnkavatar" Visible="True" CssClass="CommandButton" runat="server" EnableViewState="false"></asp:hyperlink>
                                        <asp:DropDownList id="ddlAvatar" Width="340px" Visible=false runat="server" cssclass="NormalTextBox" AutoPostBack="True"></asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:placeholder>
                            <asp:placeholder id="pnlUserAvatar" Runat="server" Visible="False">
                                <tr>
                                    <td class="TTTSubHeader" width="180">
                                        &nbsp;<%= DotNetZoom.getlanguage("F_UserOwnAv") %>:</td>
                                    <td class="TTTRow" align="left">
                                        <asp:checkbox id="chkAvatar" CssClass="TTTNormal" runat="server" AutoPostBack="True"></asp:checkbox>
                                    </td>
                                </tr>
                            </asp:placeholder>
                            <asp:placeholder id="pnlAvatarUpload" Runat="server">
                                <tr>
                                    <td class="TTTSubHeader" width="180">
                                        &nbsp;<%= DotNetZoom.getlanguage("F_UserUploadAv") %>:</td>
                                    <td class="TTTRow" align="left">
                                        <input class="NormalTextBox" id="htmlAvatar" type="file" size="50" name="htmlAvatar" runat="server" />&nbsp; 
                                        <asp:button id="btnUploadAvatar" CssClass="button" runat="server"></asp:button>
                                    </td>
                                </tr>
                            </asp:placeholder>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_Avatar") %>:</td>
                                <td class="TTTRow" align="left" height="24">
                                    <asp:textbox id="txtAvatar" Visible="True" style="display :none" runat="server" cssclass="NormalTextBox" width="339px" Columns="26"></asp:textbox>
                                    &nbsp; 
                                    <asp:Image id="imgAvatar"  AlternateText="*" runat="server" Visible="False"></asp:Image>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_Skin") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:DropDownList id="ddlSkin" Width="340px" runat="server" cssclass="NormalTextBox" AutoPostBack="True"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserEmail") %></td>
                                <td class="TTTRow" align="left">
                                    <asp:checkbox id="chkMemberList" CssClass="TTTNormal" runat="server"></asp:checkbox>
									<span class="Normal">&nbsp;<%= DotNetZoom.getlanguage("F_UserEmailInfo") %></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserShowUO") %></td>
                                <td class="TTTRow" align="left">
                                    <asp:checkbox id="chkOnlineStatus" CssClass="TTTNormal" runat="server"></asp:checkbox>
									<span class="Normal">&nbsp;<%= DotNetZoom.getlanguage("F_UserShowUOInfo") %></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserSignature") %></td>
                                <td class="TTTRow" align="left">
                                    <asp:TextBox id="txtSignature" Width="340px" runat="server" cssclass="NormalTextBox" TextMode="MultiLine"></asp:TextBox>
                                    &nbsp; 
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserTime") %></td>
                                <td class="TTTRow" align="left">
                                    <asp:DropDownList id="ddlTimeZone" DataValueField="Zone" DataTextField="Description" Width="340px" runat="server" cssclass="NormalTextBox" AutoPostBack="True"></asp:DropDownList>
                                	<asp:Label id="lblTimeZone" width="100%" cssclass="TTTNormal" runat="server"></asp:Label>
								</tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserWork") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtOccupation" runat="server" cssclass="NormalTextBox" width="339px" Columns="26"></asp:textbox>
                                    &nbsp; 
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserInterest") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtInterests" runat="server" cssclass="NormalTextBox" width="339px" Columns="26"></asp:textbox>
                                    &nbsp; 
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
	</table>	
    </asp:placeholder>
    <asp:placeholder id="pnlPublicInfo" Runat="server">
	<table class="TTTBorder" cellspacing="0" cellpadding="0" width="750" align="center">
        <tbody>
            <tr>
                <td>
                    <table cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="TTTSubHeader" width="180"></td>
                                <td colspan="2" class="TTTAltHeader" height="28">
                                    <p>
                                        <span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("F_UserInfoUser") %></span>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserAlias") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:TextBox id="txtAlias" Width="340px" runat="server" cssclass="NormalTextBox"></asp:TextBox>
                                </td><td class="TTTRow" align="center">
								    <asp:hyperlink id="lnkPMS" style="display:block; width :120px" Cssclass="CommandButton" runat="server" ></asp:hyperlink>
                                </td>
                            </tr>
                            <asp:placeholder id="pnlEmail" Runat="server">
                                <tr>
                                    <td class="TTTSubHeader" width="180">
                                        &nbsp;<%= DotNetZoom.getlanguage("F_UserEmail0") %>:</td>
                                    <td class="TTTRow" align="left">
                                        <asp:textbox id="txtEmail" runat="server" cssclass="NormalTextBox" width="340" Columns="26"></asp:textbox>
                                     </td><td class="TTTRow" align="center">   
										<asp:hyperlink style="display:block; width :120px" Cssclass="CommandButton" id="lnkEmail" runat="server" ></asp:hyperlink>
                                    </td>
                                </tr>
                            </asp:placeholder>
                            <tr Runat="Server" Id="RowURL">
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserURL") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:textbox id="txtURL" runat="server" cssclass="NormalTextBox" width="340" Columns="26"></asp:textbox>
                                </td><td class="TTTRow" align="center">    
									<asp:hyperlink style="display:block; width :120px" Cssclass="CommandButton" id="lnkWWW" runat="server" ></asp:hyperlink>
                                </td>
                            </tr>
                            <tr Runat="Server" Id="RowMSN">
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserMSN") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:TextBox id="txtMSN" Width="340" runat="server" cssclass="NormalTextBox"></asp:TextBox>
                                 </td><td class="TTTRow" align="center">   
									<asp:hyperlink style="display:block; width :120px" Cssclass="CommandButton" id="lnkMSN" runat="server" ></asp:hyperlink>
                                </td>
                            </tr>
                            <tr Runat="Server" Id="RowYahoo">
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserYAHOO") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:TextBox id="txtYahoo" Width="340" runat="server" cssclass="NormalTextBox"></asp:TextBox>
                                </td><td class="TTTRow" align="center">    
									<asp:hyperlink style="display:block; width :120px" Cssclass="CommandButton" id="lnkYahoo" runat="server" ></asp:hyperlink>
                                </td>
                            </tr>
                            <tr Runat="Server" Id="RowAIM">
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserAIM") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:TextBox id="txtAIM" Width="340" runat="server" cssclass="NormalTextBox"></asp:TextBox>
                                 </td><td class="TTTRow" align="center">   
									<asp:hyperlink style="display:block; width :120px" Cssclass="CommandButton" id="lnkAIM" runat="server" ></asp:hyperlink>
                                </td>
                            </tr>
                            <tr Runat="Server" Id="RowICQ">
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserICQ") %>:</td>
                                <td class="TTTRow" align="left">
                                    <asp:TextBox id="txtICQ" Width="340" runat="server" cssclass="NormalTextBox"></asp:TextBox>
								</td><td class="TTTRow" align="center">
                                    <asp:hyperlink style="display:block; width :120px" Cssclass="CommandButton" id="lnkICQ" runat="server" ></asp:hyperlink>
                                </td>
                            </tr>
                            <tr>
                                <td class="TTTSubHeader" width="180">
                                    &nbsp;<%= DotNetZoom.getlanguage("F_UserStats") %>:</td>
                                <td class="TTTRow" colspan="2" align="left">
                                    <asp:Label id="lblStatistic" runat="server" cssclass="TTTNormal"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
  
    <tr valign="top">
        <td class="TTTAltHeader" align="left" colspan="2">
            &nbsp; 
            <asp:button cssclass="button" id="CancelButton" runat="server"  CommandName="Cancel"></asp:button>
            &nbsp; 
            <asp:button cssclass="button" id="UpdateButton" runat="server"  CommandName="Update"></asp:button>
            &nbsp; 
        </td>
    </tr>
  </tbody>
</table>
</asp:placeholder>