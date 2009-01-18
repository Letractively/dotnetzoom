<%@ Control Language="vb" codebehind="TTT_EditForumPost.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.TTT_EditForumPost" %>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<%@ Register TagPrefix="TTT" TagName="ForumNav" Src="TTT_ForumNavigator.ascx" %>
<script language="JavaScript" type="text/javascript">
		function OpeniconeWindow(tabid , idParent)
			{
				var m = window.open('<%= DotnetZoom.glbPath %>DesktopModules/TTTForum/icone.aspx?L=<%= DotNetZoom.GetLanguage("N") %>&tabid=' + tabid + '&parentID=' + idParent, 'icone', 'width=800,height=600,left=100,top=100');
				m.focus();
			}
		function SetUrl(idParentValue, idParent)
			{
				eval('var txt = document.Form1.' + idParent + ';');
				txt.value =  idParentValue;
				eval('var xtxt = document.Form1.<%= MyHtmlImage.clientID %>;');
				xtxt.src = idParentValue;
				xtxt.title = idParentValue;
				xtxt.alt = idParentValue;
				
			}


		var popUp;
		function OpenNewWindow(idParent, postBack)
			{
				popUp = window.open('<%= DotnetZoom.glbPath %>DesktopModules/TTTForum/TTT_ForumSmiley.aspx?L=<%= DotNetZoom.GetLanguage("N") %>&idParent=' + idParent + '&formname=' + document.forms[0].name  , 'popupcal', 'width=400,height=180,left=100,top=100');
				popUp.focus();
			}
		function SetParentValue(formName, idParent, idParentValue)
			{
			if(idParent == "")
				{
				ftbDesktopText_editor.focus();
				sel = ftbDesktopText_editor.document.selection.createRange();
				sel.pasteHTML(idParentValue);
				}
			else
				{
				eval('var theform = document.' + formName + ';');
				eval('var txt = theform.' + idParent + ';');
				txt.value = txt.value + idParentValue;
				}
			popUp.close();
			}
</script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
    <tbody>
        <tr>
            <td>
                <table class="TTTBorder" cellspacing="0" cellpadding="1" width="780">
                    <tbody>
                        <tr>
                            <td class="TTTHeader" width="100%" height="28">
                                <span class="TTTTitle">&nbsp;<%= DotNetZoom.getlanguage("F_EditMode") %></span>&nbsp;
								</td>
                        </tr>
                        <tr>
                            <td class="TTTAltHeader" width="100%" height="28">
                                &nbsp; <asp:Label id="lblInfo" forecolor="red" cssclass="TTTNormal" runat="server"></asp:Label>
                                <asp:textbox id="txtThreadID" cssclass="TTTNormal" runat="server" maxlength="100" Columns="30" width="89px" Visible="False"></asp:textbox>
                            </td>
                        </tr>
                        <asp:placeholder id="pnlForumSelect" Visible="False" Runat="server">
                            <tr>
                                <td>
                                    <table id="tbForumSelect" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="TTTRow" align="left" colspan="2">
                                                    <TTT:FORUMNAV id="ctlForumNav" runat="server" width="100%" EditPost="true" IsReadonly="true"></TTT:FORUMNAV>
                                                </td>
                                            </tr>
										     <tr>
            									<td class="TTTAltHeader" align="left" colspan="2" height="28">
                								&nbsp; 
                								<asp:Button id="btnBack" cssclass="button" runat="server" CommandName="back" ></asp:Button>
                								&nbsp; 
            									</td>
        									</tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </asp:placeholder>
                        <asp:placeholder id="pnlOldPost" Visible="False" Runat="server">
                            <tr>
                                <td>
                                    <table cellspacing="1" cellpadding="3" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="TTTSubHeader" width="137">
                                                    &nbsp;<%= DotNetZoom.getlanguage("F_From") %>:</td>
                                                <td class="TTTRow" align="left">
                                                    <asp:Label id="lblAuthor" cssclass="TTTNormal" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="TTTSubHeader" width="137">
                                                    &nbsp;<%= DotNetZoom.getlanguage("F_Message") %>:</td>
                                                <td class="TTTRow" align="left">
                                                    <asp:Label id="lblMessage" cssclass="TTTNormal" runat="server"></asp:Label></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </asp:placeholder>
                        <asp:placeholder id="pnlNewPost" Runat="server">
                            <tr>
                                <td>
                                    <table cellspacing="1" cellpadding="3" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="TTTSubHeader" width="137">
                                                    &nbsp;<%= DotNetZoom.getlanguage("F_Object") %>:</td>
                                                <td class="TTTRow" align="left">
                                                    <asp:textbox id="txtSubject" cssclass="TTTNormal" runat="server" maxlength="100" Columns="30" width="390"></asp:textbox>
                                                </td>
                                            </tr>
                                            <asp:placeholder id="pnlPinned" Visible="False" Runat="server">
                                                <tr>
                                                    <td class="TTTSubHeader" width="137">
                                                        &nbsp;<%= DotNetZoom.getlanguage("F_ToSee") %></td>
                                                    <td class="TTTRow" align="left">
                                                        <asp:checkbox id="chkPinned" Runat="server" CssClass="NormalTextBox"></asp:checkbox>
                                                    </td>
                                                </tr>
                                            </asp:placeholder>
                                            <tr>
                                                <td class="TTTSubHeader" valign="top" width="137" height="28">
                                                    &nbsp;<%= DotNetZoom.getlanguage("F_Message") %>: 
                                                </td>
                                                <td class="TTTRow" align="left" rowspan="2">
                                                    <asp:textbox id="txtMessage" runat="server" Columns="44" width="390" CssClass="NormalTextBox" TextMode="Multiline" Rows="6"></asp:textbox>
                           							<asp:placeHolder id="RteDeskTop" Runat="Server" Visible="False">
													<FCKeditorV2:FCKeditor id="FCKeditor1" BasePath="~/FCKeditor/" runat="server"></FCKeditorV2:FCKeditor>
													</asp:placeHolder>
                                                </td>
                                            </tr>
                                                <tr>
                                                   <td class="TTTSubHeader" valign="top" width="137">
									                <asp:placeholder id="pnlGetSmiley" Runat="server">
                                                    <asp:literal id="lblScript" runat="server" EnableViewState="false" /><img class="TTTSubHeader" src="/images/Avatars/smiley_tongue.gif" border="0" />
													<%= DotNetZoom.getlanguage("F_AddImo") %></a>
													</asp:placeholder>	
                                                    </td>
                                                </tr>
                                            
                                            <asp:placeholder id="pnlImage" Visible="False" Runat="server">
                                                <tr>
                                                    <td class="TTTSubHeader" width="137" height="37">
                                                        &nbsp;<%= DotNetZoom.getlanguage("F_Image") %>:</td>
                                                    <td class="TTTRow" align="left" height="37">
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
															     <tr>
            														<td align="left" width="290" style="white-space: nowrap;">
																		<table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            				<tbody>
															     				<tr><td align="left">
																				<asp:textbox id="txticone" runat="server" cssclass="NormalTextBox" width="200" EnableViewState="True" MaxLength="200"></asp:textbox>
             													 				</td></tr>
																 				<tr><td height="25" valign="middle"><asp:hyperlink id="lnkicone" Visible="True" CssClass="CommandButton" runat="server" EnableViewState="false"></asp:hyperlink>
																 				</td></tr>
                                                            				</tbody>
                                                        				</table>
																	<td align="left">
																	<asp:Image Id="MyHtmlImage" runat="server" Visible="True" EnableViewState="True"></asp:Image>
		   															</td>
                                      							 </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </asp:placeholder>
                                            <asp:placeholder id="pnlNotify" Runat="server">
                                                <tr>
                                                    <td class="TTTSubHeader" width="137">
                                                        &nbsp;<%= DotNetZoom.getlanguage("F_RNotice") %>:</td>
                                                    <td class="TTTRow" align="left">
                                                        <asp:checkbox id="chkNotify" Runat="server" CssClass="NormalTextBox"></asp:checkbox>
                                                    </td>
                                                </tr>
                                            </asp:placeholder>
                                            <tr id="hide" class="hide" >
                                                <td class="TTTSubHeader" width="137"></td>
                                                <td class="TTTAltHeader" align="left">
                                                    &nbsp; 
                                                    <asp:button id="btnPreview" runat="server" cssclass="button"></asp:button>
                                                    &nbsp; 
                                                    <asp:button id="btnSubmit" runat="server"  cssclass="button"></asp:button>
                                                    &nbsp; 
                                                    <asp:button id="btnCancel" runat="server"  cssclass="button"></asp:button>
                                                    &nbsp; 
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </asp:placeholder>
                        <asp:placeholder id="pnlPreview" Visible="False" Runat="server">
                            <tr>
                                <td>
                                    <table cellspacing="1" cellpadding="3" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="TTTAltHeader" colspan="2">
                                                    &nbsp;<%= DotNetZoom.getlanguage("F_Preview") %>:</td>
                                            </tr>
                                            <tr>
                                                <td class="TTTSubHeader" width="137">
                                                    &nbsp;<%= DotNetZoom.getlanguage("F_Message") %>:</td>
                                                <td class="TTTRow" align="left">
                                                    <asp:Label id="lblPreview" cssclass="TTTNormal" runat="server" width="390"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="TTTSubHeader" width="137" height="28"></td>
                                                <td class="TTTAltHeader" align="left" height="28">
                                                    &nbsp; 
                                                    <asp:button id="btnBackEdit" runat="server" cssclass="button"></asp:button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </asp:placeholder>
                        <asp:placeholder id="pnlModerate" Visible="False" Runat="server">
                            <tr>
                                <td>
                                    <table cellspacing="1" cellpadding="3" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="TTTRow" valign="middle">
                                                    <asp:Label id="lblModerate" cssclass="TTTNormal" runat="server" width="84%"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="TTTRow" valign="middle">
                                                    <asp:button id="btnBackForum" runat="server"  cssclass="button"></asp:button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </asp:placeholder>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<div id="show" class="show" align="center" style="background: silver; border: thin dotted; padding: 4px; visibility:hidden;  top: -33px; position: relative"><%= DotNetZoom.getlanguage("F_Saving") %>
<br><br>
<img src="/images/rotation.gif" alt="*" width="32" height="32">
<br><br>
</div>