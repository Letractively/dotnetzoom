<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PrivateMessagesModule.ascx.vb" Inherits="DotNetZoom.PrivateMessagesModule" TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<script language="javascript" type="text/javascript"><!--
function SelectAllCheckboxes(intType)
{
	var blnChecked;
	
	if (intType == 1)
	{
		blnChecked = document.getElementById('chkDeleteAllInbox').checked;
	}
	else
	{
		blnChecked = document.getElementById('chkDeleteAllOutbox').checked;
	}
	
	for(intIndex=0; intIndex<document.forms[0].length; intIndex++)
	{
		if (document.forms[0].elements[intIndex].type == "checkbox")
		{
			document.forms[0].elements[intIndex].checked = blnChecked;
		}
	}
}
//--></script>
<asp:placeholder id="pnlNotAuthenticated" runat="server" visible="False">
<div class="Normal">
<p><b><%= DotNetZoom.GetLanguage("UO_no_connect")%></b></p>
<p><a href="<%= DotNetZoom.getFulldocument() %>?showlogin=1" title="<%= DotNetZoom.GetLanguage("UO_click_connect")%>"><%= DotNetZoom.GetLanguage("login")%></a></p>
</div>
</asp:placeholder>
<asp:placeholder id="pnlTabs" runat="server" visible="True">
	<asp:datalist width="100%" id="dlTabs" ItemStyle-Height="30" runat="Server" cssclass="TabHolder" separatorstyle-cssclass="TabSeparator" selecteditemstyle-cssclass="TabSelected" itemstyle-cssclass="TabDefault" itemstyle-wrap="False" cellpadding="0" repeatcolumns="3">
		<itemtemplate>
			<a href='<%# GetSubLink( Container.DataItem.Key ) %>'><b>
					<%# Container.DataItem.Value %>
				</b></a>
			</itemtemplate>
		<separatortemplate>
			<img src="images/1x1.gif" alt="*" border="0">
		</separatortemplate>
	</asp:datalist>
</asp:placeholder>
<asp:placeholder id="pnlInbox" runat="server" visible="False">
	<table class="TabPage" cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td valign="top">
				<asp:button cssclass="button" id="btnDeleteAllInbox1" onclick="btnDeleteInboxItems_Click" runat="server"></asp:button>
				<asp:datagrid id="dgInbox" runat="server" datakeyfield="MessageId" gridlines="none" borderwidth="0px" CellPadding="3" onsortcommand="dgInbox_SortCommand" onitemcommand="dgPMS_ItemCommand" allowsorting="True" width="100%" autogeneratecolumns="False" cssclass="normal">
					<alternatingitemstyle backcolor="silver"></alternatingitemstyle>
					<columns>
						<asp:templatecolumn>
							<headerstyle cssclass="HeaderCell" horizontalalign="center" verticalalign="middle"></headerstyle>
							<itemstyle horizontalalign="center" verticalalign="top"></itemstyle>
							<headertemplate>
								<input type="checkbox" value='0' onclick="SelectAllCheckboxes(1);" id="chkDeleteAllInbox">
							</headertemplate>
							<itemtemplate>
								<input type=checkbox value='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" id="Checkbox1" name="Checkbox1">
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn sortexpression="MessageRead">
							<headerstyle cssclass="HeaderCell" horizontalalign="left" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="center" verticalalign="top"></itemstyle>
							<itemtemplate>
								<img src="/images/1x1.gif" alt="*" height="16" width="17" Title="<%# GetToolTip(DataBinder.Eval(Container.DataItem, "MessageRead")) %>" <%# GetImageStyle(DataBinder.Eval(Container.DataItem, "MessageRead")) %> >
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="#">
							<headerstyle cssclass="HeaderCell" horizontalalign="left" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="center" verticalalign="top"></itemstyle>
							<itemtemplate>
								<%# Container.ItemIndex + 1 %>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn sortexpression="SenderUsername">
							<headerstyle cssclass="HeaderCell" horizontalalign="left" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="Left" verticalalign="top"></itemstyle>
							<itemtemplate>
								<asp:hyperlink id="lnkUsername" runat="server" navigateurl='<%# GetUserInfoLink(DataBinder.Eval(Container.DataItem, "SenderID")) %>' tooltip='<%# GetUserInfoTooltip(DataBinder.Eval(Container.DataItem, "SenderUsername")) %>'>
									<%# DataBinder.Eval(Container.DataItem, "SenderUsername") %>
								</asp:hyperlink>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn sortexpression="Subject">
							<headerstyle cssclass="HeaderCell" horizontalalign="left" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="left" verticalalign="top" width="100%"></itemstyle>
							<itemtemplate>
								<asp:linkbutton runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Subject") %>' causesvalidation="False" commandname="inbox" id="Linkbutton1">
								</asp:linkbutton>
								<asp:placeholder runat="server" visible="False" id="pnlMessage">
									<p></p>
									<%# DataBinder.Eval(Container.DataItem, "Message") %>
									<p></p>
									<asp:imagebutton id="btnReply" oncommand="btnReplyMessage_Click" commandname="Reply" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" imageurl="~/images/1x1.gif"  height="16" width="17" causesvalidation="False" BorderWidth="0" BorderStyle="none" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -279px;">
									</asp:imagebutton>&nbsp;
									<asp:linkbutton id="lnkReplyMessage" oncommand="btnReplyMessage_Click" commandname="Reply" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" causesvalidation="False">
									</asp:linkbutton>
									&nbsp;|&nbsp;
									<asp:imagebutton id="btnDelete" oncommand="btnDeleteMessage_Click" commandname="InboxDelete" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" imageurl="~/images/1x1.gif"  Width="17" height="16" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -75px;" causesvalidation="false" BorderWidth="0" BorderStyle="none">
									</asp:imagebutton>&nbsp;
									<asp:linkbutton id="lnkMessageViewDelete" cssclass="forumTopicLink" oncommand="btnDeleteMessage_Click" commandname="InboxDelete" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" causesvalidation="False">
									</asp:linkbutton>
									&nbsp;|&nbsp;
									<asp:imagebutton id="btnKeepAsNew" oncommand="btnKeepAsNew_Click" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" imageurl="~/images/1x1.gif" height="16" width="17" causesvalidation="false" BorderWidth="0" BorderStyle="none" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -243px;">
									</asp:imagebutton>&nbsp;
									<asp:linkbutton id="lnkKeepAsNew" cssclass="forumTopicLink" oncommand="btnKeepAsNew_Click" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" causesvalidation="False">
									</asp:linkbutton>
								</asp:placeholder>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn sortexpression="DateCreated" headerstyle-wrap="False">
							<headerstyle cssclass="HeaderCell" horizontalalign="left" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="Left" verticalalign="top" wrap="False"></itemstyle>
							<itemtemplate>
								<%# DataBinder.Eval(Container.DataItem, "DateCreated") %>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:boundcolumn datafield="MessageID" visible="False"></asp:boundcolumn>
					</columns>
				</asp:datagrid>
					<asp:button cssclass="button" id="btnDeleteAllInbox2" onclick="btnDeleteInboxItems_Click" runat="server"></asp:button>
			</td>
		</tr>
	</table>
</asp:placeholder>
<asp:placeholder id="pnlNoMessages" runat="server" visible="False">
	<table class="TabPage" cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td align="left">&nbsp;<span class="NormalBold"><%= DotNetZoom.GetLanguage("UO_NoMessage") %></span></td>
		</tr>
	</table>
</asp:placeholder>
<asp:placeholder id="pnlOutbox" runat="server" visible="False">
	<table class="TabPage" id="Table1" cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td valign="top">
					<asp:button cssclass="button" id="btnDeleteAllOutbox1" onclick="btnDeleteOutboxItems_Click" runat="server"></asp:button>
				<asp:datagrid id="dgOutbox" runat="server" datakeyfield="MessageId" CellPadding="3" gridlines="none" borderwidth="0px" onsortcommand="dgOutbox_SortCommand" onitemcommand="dgPMS_ItemCommand" allowsorting="True" width="100%" autogeneratecolumns="False" cssclass="normal">
					<alternatingitemstyle backcolor="silver"></alternatingitemstyle>
					<columns>
						<asp:templatecolumn>
							<headerstyle cssclass="HeaderCell" horizontalalign="center" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="center" verticalalign="top"></itemstyle>
							<headertemplate>
								<input type="checkbox" value='0' onclick="SelectAllCheckboxes(1);" id="chkDeleteAllInbox">
							</headertemplate>
							<itemtemplate>
								<input type=checkbox value='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" id="Checkbox2" name="Checkbox2">
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn sortexpression="MessageRead">
							<headerstyle cssclass="HeaderCell" horizontalalign="left" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="center" verticalalign="top"></itemstyle>
							<itemtemplate>
							<img src="/images/1x1.gif" alt="*" height="16" width="17" Title="<%# GetToolTip(DataBinder.Eval(Container.DataItem, "MessageRead")) %>" <%# GetImageStyle(DataBinder.Eval(Container.DataItem, "MessageRead")) %> >
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="#">
							<headerstyle cssclass="HeaderCell" horizontalalign="left" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="center" verticalalign="top"></itemstyle>
							<itemtemplate>
								<%# Container.ItemIndex + 1 %>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn sortexpression="ReceiverUsername">
							<headerstyle cssclass="HeaderCell" horizontalalign="left" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="Left" verticalalign="top"></itemstyle>
            				<itemtemplate>
								<asp:hyperlink id="Hyperlink1" runat="server" navigateurl='<%# GetUserInfoLink(DataBinder.Eval(Container.DataItem, "ReceiverID")) %>' tooltip='<%# GetUserInfoTooltip(DataBinder.Eval(Container.DataItem, "ReceiverUsername")) %>'>
									<%# DataBinder.Eval(Container.DataItem, "ReceiverUsername") %>
								</asp:hyperlink>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn sortexpression="Subject">
							<headerstyle cssclass="HeaderCell" horizontalalign="left" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="left" verticalalign="top" width="100%"></itemstyle>
							<itemtemplate>
								<asp:linkbutton runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Subject") %>' causesvalidation="False" commandname="outbox" id="Linkbutton2"/>
								<asp:placeholder runat="server" visible="False" id="Panel1">
									<p></p>
									<%# DataBinder.Eval(Container.DataItem, "Message") %>
									<p></p>
									<asp:imagebutton id="btnOutboxDelete" oncommand="btnDeleteMessage_Click" commandname="OutboxDelete" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" imageurl="~/images/1x1.gif"  Width="17" height="16" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -75px;"   causesvalidation="false" BorderWidth="0" BorderStyle="none"/>
									<asp:linkbutton id="lnkOutboxDelete" oncommand="btnDeleteMessage_Click" commandname="OutboxDelete" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" causesvalidation="False" />
								</asp:placeholder>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn sortexpression="DateCreated" headerstyle-wrap="False">
							<headerstyle cssclass="HeaderCell" horizontalalign="left" verticalalign="middle"></headerstyle>
							<itemstyle cssclass="Cell" horizontalalign="Left" verticalalign="top" wrap="False"></itemstyle>
							<itemtemplate>
								<%# DataBinder.Eval(Container.DataItem, "DateCreated") %>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:boundcolumn datafield="MessageID" visible="False"></asp:boundcolumn>
					</columns>
				</asp:datagrid>
					<asp:button cssclass="button" id="Button2" onclick="btnDeleteOutboxItems_Click" runat="server"></asp:button>
			</td>
		</tr>
	</table>
</asp:placeholder>
<asp:placeholder id="pnlCompose" runat="server" visible="False">
	<table class="TabPage" id="Table2" cellspacing="0" cellpadding="1" width="100%">
		<tr><td></td><td><asp:label id="lblErrMsg" runat="server" visible="False" forecolor="red"></asp:label></td></tr>
		
		<tr>
			<td class="Cell" valign="top" width="20%"><%= DotNetZoom.GetLanguage("UO_to") %></td>
			<td class="Cell" width="80%">
				
				<asp:textbox id="txtReceipient" runat="server" width="200"></asp:textbox>
				<asp:button cssclass="button" id="btnFindUser" runat="server" causesvalidation="false"></asp:button>
				<asp:requiredfieldvalidator id="rfvReceipient" runat="server" controltovalidate="txtReceipient" errormessage="*" forecolor="red"  Font-Size="Small" display="Dynamic"></asp:requiredfieldvalidator>
				<br>
				<%= DotNetZoom.GetLanguage("UOSearchHelp") %>
				<asp:placeholder id="pnlFindUser" runat="server" visible="False">
					<p></p>
					<asp:dropdownlist id="drpResults" runat="server" visible="True" datatextfield="Username" datavaluefield="Username"></asp:dropdownlist>
					<asp:button cssclass="button" id="btnSelect" runat="server" causesvalidation="false"></asp:button>
					<p></p>
				</asp:placeholder></td>
		</tr>
		<tr>
			<td class="CellAlternate"><%= DotNetZoom.GetLanguage("UO_Object") %></td>
			<td class="CellAlternate" width="100%">
				<asp:textbox id="txtSubject" runat="server" width="400" maxlength="255"></asp:textbox>
				<asp:requiredfieldvalidator id="rfvSubject" runat="server" controltovalidate="txtSubject" errormessage="*" Font-Size="Small" forecolor="red" ></asp:requiredfieldvalidator></td>
		</tr>
		<tr>
			<td class="Cell" valign="top"><%= DotNetZoom.GetLanguage("UO_Message") %></td>
			<td class="Cell" width="100%">
			<FCKeditorV2:FCKeditor id="FCKeditor1" BasePath="~/FCKeditor/" runat="server"></FCKeditorV2:FCKeditor>
			</td>
		</tr>
		<tr>
			<td class="CellAlternate">&nbsp;</td>
			<td class="CellAlternate" width="100%">
				<asp:button cssclass="button" id="btnSend" runat="server"></asp:button></td>
		</tr>
	</table>
</asp:PlaceHolder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>