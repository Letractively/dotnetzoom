<%@ Control Language="vb" autoeventwireup="false" codebehind="TTT_ForumSearch.ascx.vb" Inherits="DotNetZoom.TTT_ForumSearch" %>
<script language="javascript" type="text/javascript"><!--
function SelectAllCheckboxes(intType)
{
	var blnChecked;
	
	if (intType == 1)
	{
		blnChecked = document.getElementById('chkAllbox').checked;
	}
	else
	{
		blnChecked = document.getElementById('chkAllbox').checked;
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

<script language="javascript" type="text/javascript"  src="<%=dotnetzoom.glbpath + "controls/PopupCalendar.js"%>"></script>
<table cellspacing="0" cellpadding="0" width="750" border="0">
    <tbody>
	<tr>
    	<td colspan="6" align="left">
           <span class="Head"><%= DotNetZoom.GetLanguage("Forum_Search") %></span> 
        </td>
    </tr>
    <tr>
	     <td colspan="6">
        &nbsp;
  		 </td>
    </tr>
	<tr>
	<td align="left" class="SubHead">
	<label for="<%=txtEndDate.ClientID%>"><%= DotNetZoom.GetLanguage("Search_Text") %>:</label> 
	<td>
	<td align="left" class="SubHead">
	<asp:textbox id="txtSearch" runat="server" EnableViewState="false" CssClass="NormalTextBox" Width="100px"></asp:textbox>
	</td>
	<td align="left" class="SubHead">
	<label for="<%=txtEndDate.ClientID%>"><%= DotNetZoom.GetLanguage("Search_Subject") %>:</label> 
	<td>
	<td align="left" class="SubHead">
	<asp:textbox id="txtObject" runat="server" EnableViewState="false" CssClass="NormalTextBox" Width="100px"></asp:textbox>
	</td>
	<td align="left" class="SubHead">
	<label for="<%= txtStartDate.ClientID%>"><%= DotNetZoom.GetLanguage("Stat_StartDate") %>:</label> 
	<td>
	<td align="left" class="SubHead">
    <asp:TextBox id="txtStartDate" runat="server" CssClass="NormalTextBox" Columns="20" width="120"></asp:TextBox>
    &nbsp; 
    <asp:HyperLink id="cmdStartCalendar" CssClass="CommandButton" Runat="server"></asp:HyperLink>
	<td>
	</tr>
	<tr>
       <td>&nbsp;
    </td>
	</tr>
	<tr>
	<td align="left" class="SubHead">
	<label for="<%=txtEndDate.ClientID%>"><%= DotNetZoom.GetLanguage("Search_Alias") %>:</label> 
	<td>
	<td align="left" class="SubHead">
	<asp:textbox id="txtAlias" runat="server" EnableViewState="false" CssClass="NormalTextBox" Width="100px"></asp:textbox>
	</td>
	<td align="left" class="SubHead">
	&nbsp;
	<td>
	<td align="left" class="SubHead">
	&nbsp;
	</td>
	<td align="left" class="SubHead">
    <label for="<%=txtEndDate.ClientID%>"><%= DotNetZoom.GetLanguage("Stat_EndDate") %>:</label> 
	<td>
	<td align="left" class="SubHead">
    <asp:TextBox id="txtEndDate" runat="server" CssClass="NormalTextBox" Columns="20" width="120"></asp:TextBox>
    &nbsp; 
    <asp:HyperLink id="cmdEndCalendar" CssClass="CommandButton" Runat="server"></asp:HyperLink>
	<td>
	</tr>
    </tbody>
</table>
<br>
<br>
<span onmouseover="<%= DotNetZoom.ReturnToolTip(DotNetZoom.GetLanguage("Search_SelectAllForumGroup")) %>" class="Head"><input type="checkbox" value='0' onclick="SelectAllCheckboxes(1);" id="chkAllbox">&nbsp;<%= DotNetZoom.GetLanguage("Select_Forum_Search") %></span> 
<asp:datalist id="lstGroup" runat="server" separatorstyle-cssclass="ForumSeparator" runat="Server" cssclass="ForumHolder" itemstyle-cssclass="ForumDefault" Width="100%" repeatcolumns="3" DataKeyField="ForumGroupID">
    <ItemTemplate>
	<table cellspacing="0" cellpadding="0" width="100%" border="0">
    <tbody>
	<tr>
    <td class="TTTAltHeader" align="left">
	<span onmouseover="<%# DotNetZoom.ReturnToolTip(DotNetZoom.GetLanguage("Search_SelectAllForum") + " " + CType(DataBinder.Eval(Container.DataItem, "Name"), String)) %>" class="Head"><asp:Checkbox id="chkForum" OnCheckedChanged="Check_Clicked" Text="" runat="server" CssClass="NormalTextBox" AutoPostBack="True"></asp:Checkbox>
	&nbsp;<%# CType(DataBinder.Eval(Container.DataItem, "Name"), String) %></span>
	<br>
	</td>
	</tr>
	</tbody>
	</table>
    <asp:DataList id="lstForum" cssclass="SubHead" cellpadding="0" Width="100%" datasource='<%# GetForums(CType(DataBinder.Eval(Container.DataItem, "ForumGroupID"), Integer)) %>' runat="server">
            <ItemTemplate>
			<input type=checkbox value='<%# DataBinder.Eval(Container.DataItem, "ForumID") %>' runat="server" id="chkSearch" name="Checkbox1">
			<span onmouseover="<%# DotNetZoom.ReturnToolTip(CType(DataBinder.Eval(Container.DataItem, "Description"), String))  %>"><%# CType(DataBinder.Eval(Container.DataItem, "Name"), String) %></span>
           </ItemTemplate>
       </asp:DataList>
    </ItemTemplate>
</asp:datalist>	
<br>
<br>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
    <tbody>	
        <tr>
            <td >
			&nbsp;&nbsp;<asp:Button cssclass="button" id="cmdSearch"  runat="server"></asp:Button>
    		&nbsp;&nbsp;<asp:Button cssclass="button" id="cmdBack"  runat="server"></asp:Button>
            </td>
        </tr>
    </tbody>
</table>