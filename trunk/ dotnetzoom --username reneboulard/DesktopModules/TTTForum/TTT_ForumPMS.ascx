<%@ Control Language="vb" autoeventwireup="false" codebehind="TTT_ForumPMS.ascx.vb" Inherits="DotNetZoom.TTT_ForumPMS" targetschema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<script language="javascript" type="text/javascript">
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
</script>

<asp:Label id="lblErrMsg" runat="server"  height="28" forecolor="red" visible="False" cssclass="TTTNormal"></asp:Label>

<asp:placeholder id="pnlNav" Runat="server" >

<table class="TTTBorder" align="center" width="810">
<tr>
<td valign="top">
	<table class="TTTBorder" width="40" align="left">
	<tr>
	<td class="TTTHeader" align="center" valign="middle" height="28"><%= DotNetZoom.GetLanguage("F_PMSShort") %>
	</td>
	</tr>
	<tr>
	<td class="TTTRow" align="center" valign="middle" height="40">
	<asp:hyperlink id="cmdInbox" Runat="server"></asp:hyperlink>
	</td>
	</tr>
	<tr>
	<td class="TTTRow" align="center" valign="middle" height="40">
	<asp:hyperlink id="cmdOutbox" Runat="server" ></asp:hyperlink>
	</td>
	</tr>
	<tr>
	<td class="TTTRow" align="center" valign="middle" height="40">
	<asp:hyperlink id="cmdCompose" Runat="server" ></asp:hyperlink>
	</td>
	</tr>
	<tr>
	<td class="TTTAltHeader" align="center" valign="middle" height="40">
	<asp:Button id="btnBack" CommandName="back" cssclass="button" runat="server" ></asp:Button>
	</td>
	</tr>
	</table>
 </td>
<td valign="top">
	<asp:placeholder id="pnlInbox" Runat="server"  Visible="False">
		<table class="TTTBorder" align="right" width="760">
		<tr>
		<td valign="top">
		<asp:DataGrid id="dgInbox" runat="server" ItemStyle-Height="24" BorderColor="#D1D7DC" CellSpacing="0" DataKeyField="MessageId" OnSortCommand="dgInbox_SortCommand" OnItemCommand="dgPMS_ItemCommand" AllowSorting="True" Width="100%" AutoGenerateColumns="False" CellPadding="0" gridlines="none" BorderWidth="0">
		<Columns>
		<asp:TemplateColumn>
		<HeaderStyle cssclass="TTTHeader" width="25" horizontalalign="center" verticalalign="Middle"></HeaderStyle>
		<ItemStyle horizontalalign="center" width="25" cssclass="TTTRow" verticalalign="top"></ItemStyle>
		<HeaderTemplate>
		<input type="checkbox" value='0' onclick="SelectAllCheckboxes(1);" id="chkDeleteAllInbox" />
		</HeaderTemplate>
		<ItemTemplate>
		<input type="checkbox" value='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server"  id="Checkbox1" name="Checkbox1" />
		</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="MessageRead">
		<HeaderStyle cssclass="TTTHeader" width="25" horizontalalign="center" verticalalign="Middle"></HeaderStyle>
		<ItemStyle cssclass="TTTRow" horizontalalign="center" verticalalign="top"></ItemStyle>
		<ItemTemplate>
		<img src="/images/1x1.gif" alt="*" height="16" width="17" Title="<%# GetToolTip(DataBinder.Eval(Container.DataItem, "MessageRead")) %>" <%# GetImageStyle(DataBinder.Eval(Container.DataItem, "MessageRead")) %> >
		</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="#">
		<HeaderStyle cssclass="TTTHeader" width="25" horizontalalign="center" verticalalign="Middle"></HeaderStyle>
		<ItemStyle cssclass="TTTRow" width="25" horizontalalign="center" verticalalign="top"></ItemStyle>
		<ItemTemplate>
		<%# Container.ItemIndex + 1 %> 
		</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn SortExpression="SenderUsername" DataField="SenderUsername">
		<HeaderStyle cssclass="TTTHeader" width="100" horizontalalign="center" verticalalign="Middle"></HeaderStyle>
		<ItemStyle cssclass="TTTRow" width="100" horizontalalign="center" verticalalign="top"></ItemStyle>
		</asp:BoundColumn>
		<asp:TemplateColumn SortExpression="Subject">
		<HeaderStyle cssclass="TTTHeader"  horizontalalign="left" verticalalign="Middle"></HeaderStyle>
		<ItemStyle horizontalalign="left"  verticalalign="top"></ItemStyle>
		<ItemTemplate>
			<table width="100%">
			<tr>
			<td class="TTTRow" width="100%">
		    <asp:placeholder ID="pnlSubject" Runat="server" >
				<table>
				<tr>
				<td height="24" valign="top">&nbsp;<span class="TTTNormal">
				<asp:LinkButton Runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Subject") %>' CausesValidation="False" CommandName="inbox" ID="Linkbutton1"></asp:LinkButton>
				</span>
				</td>
				</tr>
				</table>	
			</asp:placeholder>
			<asp:placeholder Runat="server" Visible="False"  ID="pnlMessage">
				<table>
				<tr>
				<td style="white-space: nowrap;"  height="24" align="left">
				<asp:LinkButton Runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Subject") %>' CausesValidation="False" CommandName="inbox" ID="Linkbutton3"></asp:LinkButton>
				</td>
				</tr>
				<tr>
				<td Class="TTTNormal" height="36" colspan="2">
				<%# DataBinder.Eval(Container.DataItem, "Message") %> 
				</td>
				</tr>
				<tr>
				<td style="white-space: nowrap;" height="24" width="100%" align="right">
				<asp:ImageButton ID="btnReply" oncommand="btnReplyMessage_Click" commandname="Reply" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" imageurl="~/images/1x1.gif"  height="16" width="17" causesvalidation="False" BorderWidth="0" BorderStyle="none" style="background: url('/images/uostrip.gif') no-repeat; background-position: 0px -279px;"></asp:ImageButton>
				<asp:linkbutton id="lnkReplyMessage" CssClass="CommandButton" OnCommand="btnReplyMessage_Click" CommandName="Reply" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' Runat="server"  CausesValidation="False" ></asp:linkbutton>&nbsp;
				<asp:imagebutton id="btnDelete" oncommand="btnDeleteMessage_Click" commandname="InboxDelete" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" imageurl="~/images/1x1.gif"  Width="17" height="16" style="background: url('/images/uostrip.gif') no-repeat; background-position: 0px -75px;" causesvalidation="false" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
				<asp:linkbutton id="lnkMessageViewDelete" CssClass="CommandButton" OnCommand="btnDeleteMessage_Click" CommandName="InboxDelete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' Runat="server"  CausesValidation="False" ></asp:linkbutton>&nbsp;
				<asp:imagebutton id="btnKeepAsNew" oncommand="btnKeepAsNew_Click" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" imageurl="~/images/1x1.gif" height="16" width="17" causesvalidation="false" BorderWidth="0" BorderStyle="none" style="background: url('/images/uostrip.gif') no-repeat; background-position: 0px -243px;"></asp:imagebutton>
				<asp:linkbutton id="lnkKeepAsNew" CssClass="CommandButton" OnCommand="btnKeepAsNew_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' Runat="server" CausesValidation="False"></asp:linkbutton>&nbsp;
				</td>
				</tr>
				</table>	
			</asp:placeholder>
			</td>
			</tr>
			</table>
		</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="DateCreated" HeaderStyle-Wrap="False">
		<HeaderStyle cssclass="TTTHeader" width="150" horizontalalign="Center" verticalalign="Middle"></HeaderStyle>
		<ItemStyle cssclass="TTTRow" width="150" horizontalalign="Center" verticalalign="top" wrap="False"></ItemStyle>
		<ItemTemplate>
		<%# DataBinder.Eval(Container.DataItem, "DateCreated") %> 
 		</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="MessageID" Visible="False"></asp:BoundColumn>
		</Columns>
		</asp:DataGrid>
		</td>
		</tr>
		<tr>
		<td class="TTTAltHeader" style="white-space: nowrap;" height="28">&nbsp; 
    	<asp:Button id="btnDeleteAllInbox" onclick="btnDeleteInboxItems_Click" cssclass="button" runat="server"></asp:Button>
		</td>
		</tr>
		</table>
   	</asp:placeholder>
	
	<asp:placeholder id="pnlNoMessages" Runat="server"  Visible="False">
		<table class="TTTBorder" align="right" width="700">
		<tr>
		<td class="TTTAltHeader" align="left" height="28">&nbsp;<span class="TTTAltHeaderText"><%= DotNetZoom.GetLanguage("UO_NoMessage") %></span> 
		</td>
		</tr>
  		</table>
    </asp:placeholder>

	<asp:placeholder id="pnlOutbox" Runat="server"  Visible="False">
		<table class="TTTBorder" align="right" width="700">
		<tr>
		<td valign="top">
		<asp:DataGrid id="dgOutbox" runat="server"  ItemStyle-Height="24" DataKeyField="MessageId" OnSortCommand="dgOutbox_SortCommand" OnItemCommand="dgPMS_ItemCommand" AllowSorting="True" Width="100%" AutoGenerateColumns="False" CellPadding="0" gridlines="none" BorderWidth="0">
		<Columns>
		<asp:TemplateColumn>
		<HeaderStyle cssclass="TTTHeader" width="25" horizontalalign="center" verticalalign="Middle"></HeaderStyle>
		<ItemStyle cssclass="TTTRow" horizontalalign="center" verticalalign="top"></ItemStyle>
		<HeaderTemplate>
		<input type="checkbox" value='0' onclick="SelectAllCheckboxes(1);" id="chkDeleteAllOutbox" />
		</HeaderTemplate>
		<ItemTemplate>
		<input type="checkbox" value='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server"  id="Checkbox2" name="Checkbox2" />
		</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="MessageRead">
		<HeaderStyle cssclass="TTTHeader" width="25" horizontalalign="center" verticalalign="Middle"></HeaderStyle>
		<ItemStyle cssclass="TTTRow" horizontalalign="center" verticalalign="top"></ItemStyle>
		<ItemTemplate>
		<img src="/images/1x1.gif" alt="*" height="16" width="17" Title="<%# GetToolTip(DataBinder.Eval(Container.DataItem, "MessageRead")) %>" <%# GetImageStyle(DataBinder.Eval(Container.DataItem, "MessageRead")) %> >
		</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="#">
		<HeaderStyle cssclass="TTTHeader" width="25" horizontalalign="center" verticalalign="Middle"></HeaderStyle>
		<ItemStyle cssclass="TTTRow" horizontalalign="center" verticalalign="top"></ItemStyle>
		<ItemTemplate>
		<%# Container.ItemIndex + 1 %> 
		</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn SortExpression="ReceiverUsername" DataField="ReceiverUsername">
		<HeaderStyle cssclass="TTTHeader" width="100" horizontalalign="center" verticalalign="Middle"></HeaderStyle>
		<ItemStyle cssclass="TTTRow" width="100" horizontalalign="center" verticalalign="top"></ItemStyle>
		</asp:BoundColumn>
		<asp:TemplateColumn SortExpression="Subject">
		<HeaderStyle cssclass="TTTHeader" horizontalalign="left" verticalalign="Middle"></HeaderStyle>
		<ItemStyle horizontalalign="left" verticalalign="top"></ItemStyle>
		<ItemTemplate>
			<table width="100%">
			<tr>
			<td class="TTTRow" width="100%">
			<asp:placeholder ID="pnlSubject2" Runat="server" >
				<table>
				<tr>
				<td height="24" valign="top">&nbsp;<span class="TTTNormal">
				<asp:LinkButton Runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Subject") %>' CausesValidation="False" CommandName="outbox" ID="Linkbutton4"></asp:LinkButton>
				</span>
				</td>
				</tr>
				</table>
			</asp:placeholder>
			<asp:placeholder Runat="server"  Visible="False" ID="pnlMessage2">
			<table width="100%">
			<tr>
			<td style="white-space: nowrap;" height="24" align="left">
			<asp:LinkButton Runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Subject") %>' CausesValidation="False" CommandName="outbox" ID="Linkbutton5"></asp:LinkButton>
			</td>
			</tr>
			<tr>
			<td class="TTTNormal" width="100%" height="36" colspan="2">
			<%# DataBinder.Eval(Container.DataItem, "Message") %> 
			</td>
			</tr>
			<tr>
			<td style="white-space: nowrap;" height="24" width="100%" align="left">&nbsp;
			<asp:imagebutton id="btnOutboxDelete" oncommand="btnDeleteMessage_Click" commandname="OutboxDelete" commandargument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' runat="server" imageurl="~/images/1x1.gif"  Width="17" height="16" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -75px;"   causesvalidation="false" BorderWidth="0" BorderStyle="none"/>&nbsp;
			<asp:linkbutton id="lnkOutboxDelete" OnCommand="btnDeleteMessage_Click" CommandName="OutboxDelete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' Runat="server"  CssClass="CommandButton" CausesValidation="False" />&nbsp;
			</td>
			</tr>
			</table>
		</asp:placeholder>
		</td>
		</tr>
		</table>
		</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="DateCreated" HeaderStyle-Wrap="False">
		<HeaderStyle cssclass="TTTHeader" width="150" horizontalalign="center" verticalalign="Middle"></HeaderStyle>
		<ItemStyle cssclass="TTTRow" horizontalalign="center" verticalalign="top" wrap="False"></ItemStyle>
		<ItemTemplate>
		<%# DataBinder.Eval(Container.DataItem, "DateCreated") %> 
		</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="MessageID" Visible="False"></asp:BoundColumn>
		</Columns>
		</asp:DataGrid>
		</td>
		</tr>
		<tr>
		<td class="TTTAltHeader" height="28">&nbsp;
		<asp:Button id="btnDeleteAllOutbox" onclick="btnDeleteOutboxItems_Click" cssclass="button" runat="server" ></asp:Button>
		</td>
		</tr>
		</table>
    </asp:placeholder>
    <asp:placeholder id="pnlCompose" Runat="server" Visible="False">
	<table class="TTTBorder" align="right" width="700">
	<tr>
	<td class="TTTHeader" align="left" colspan="2" height="28">&nbsp;<%= DotNetZoom.GetLanguage("UO_write") %></td>
	</tr>
	<tr>
	<td class="TTTSubHeader" valign="top" height="24">&nbsp;<%= DotNetZoom.GetLanguage("UO_to") %>
	</td>
	<td class="TTTRow" >
	<asp:placeholder id="pnlUser" Runat="server"  Visible="True">
	<asp:TextBox id="txtRecipient" Runat="server"  CssClass="NormalTextBox" Width="200" Wrap="False"></asp:TextBox>
	<asp:Button cssclass="button" id="btnFindUser" Runat="server"  CausesValidation="false"></asp:Button>
	<asp:TextBox id="txtRecipientID" Runat="server"  CssClass="NormalTextBox" Visible="False" Width="25"></asp:TextBox>
	</asp:placeholder>
	<asp:placeholder id="pnlFindUser" Runat="server"  Visible="False">
		<asp:dropdownlist id="drpResults" CssClass="NormalTextBox" runat="server"  Visible="True" Width="200" DataTextField="Alias" DataValueField="UserID"></asp:dropdownlist>
		<asp:Button cssclass="button" id="btnSelect" Runat="server"  CausesValidation="false"></asp:Button>
	</asp:placeholder>

	</td>
	</tr>
	<tr>
	<td class="TTTSubHeader" style="white-space: nowrap;" valign="top"  height="24">&nbsp;<%= DotNetZoom.GetLanguage("UO_Object") %>
	</td>
	<td class="TTTRow"  height="24">
	<asp:TextBox id="txtSubject" Runat="server"  CssClass="NormalTextBox" Width="500" MaxLength="255"></asp:TextBox>
	</td>
	</tr>
	<tr>
	<td class="TTTSubHeader" valign="top" width="20%">&nbsp;<%= DotNetZoom.GetLanguage("UO_Message") %>
	</td>
	<td class="TTTRow"  height="24">
	<FCKeditorV2:FCKeditor id="FCKeditor1" BasePath="~/FCKeditor/" runat="server"></FCKeditorV2:FCKeditor>
	</td>
	</tr>
	<tr>
	<td class="TTTAltHeader" colspan="2" height="28">&nbsp;
	<asp:Button cssclass="button" id="btnSend" Runat="server"></asp:Button>
	</td>
	</tr>
	</table>
	</asp:placeholder>
</td>
</tr>
</table>	
	
</asp:placeholder>