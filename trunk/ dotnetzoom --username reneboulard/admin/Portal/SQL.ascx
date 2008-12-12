<%@ Control Language="vb" codebehind="SQL.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.SQL" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table align="center">
<tr>
<td>
<label style="DISPLAY: none" for="<%=txtQuery.ClientID%>">SQL Query</label> 
<asp:TextBox id="txtQuery" Rows="20" Columns="100" TextMode="MultiLine" Runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
<asp:LinkButton id="cmdExecute" runat="server" CssClass="CommandButton"></asp:LinkButton>
</td>
</tr>
<tr>
<td>
<br>
<%=DotNetZoom.GetLanguage("SQL_Info") %> :<br>
<asp:TextBox id="txtConnectionString" width="320px" Columns="25" Runat="server"></asp:TextBox>&nbsp;&nbsp;<%= DotNetZoom.GetLanguage("SQL_Connection") %>
<br>  
</td>
</tr>
<tr>
<td> 
<input id="cmdBrowseScript" type="file" size="50" runat="server" />&nbsp;&nbsp;
<asp:LinkButton id="SQLcmdExecute" runat="server" CssClass="CommandButton"></asp:LinkButton>
</td>
</tr>
<tr>
<td>
<asp:DataGrid id="grdResults" Runat="server"  ItemStyle-CssClass="Normal" HeaderStyle-CssClass="SubHead" AutoGenerateColumns="True"></asp:DataGrid>
</td>
</tr>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>