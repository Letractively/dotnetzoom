<%@ Control Language="vb" autoeventwireup="false" codebehind="TTT_ForumUsersOnline.ascx.vb" Inherits="DotNetZoom.TTT_ForumUsersOnline" targetschema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
<tr>
<td class="TTTHeader" width="100%">
<asp:placeholder id="pnlActiveMember" Runat="server">
&nbsp;<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -105px;">&nbsp;<b><%= DotNetZoom.getlanguage("F_WhoThere") %>:</b>&nbsp;
<asp:Literal EnableViewState="false" id="lblMembers" runat="server" />
</asp:placeholder>
</td>
</tr>
<tr>
<td class="TTTRow">
<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -319px;">&nbsp;<%= DotNetZoom.GetLanguage("UO_Anonymous")%>:&nbsp; <asp:Literal EnableViewState="false" id="lblGuestCount" runat="server" />
<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -171px;">&nbsp;<%=DotNetZoom.GetLanguage("UO_Members")%>:&nbsp; <asp:Literal EnableViewState="false" id="lblMemberCount" runat="server" />
<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -295px;">&nbsp;<%= DotNetZoom.GetLanguage("UO_TRegistered")%>:&nbsp; <asp:Literal EnableViewState="false" id="lblTotalCount" runat="server" />
<br>
<asp:placeholder id="pnlGuestMessage" Runat="server" >
<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -319px;">&nbsp; <asp:Literal EnableViewState="false" id="lblGuestMessage" runat="server" />
</asp:placeholder>
<asp:placeholder id="pnlMemberMessage" Runat="server"  Visible="False">
<asp:placeholder id="pnlMember" Runat="server" >
<img height="14" width="17" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -171px;">&nbsp; &nbsp;<asp:hyperlink id="hypUser" runat="server" Font-Bold="True"></asp:hyperlink>
 &nbsp;&nbsp; 
<asp:HyperLink id="hypCount" Runat="server" Font-Bold="True"></asp:HyperLink></asp:placeholder>
</asp:placeholder>
</td>
</tr>
</table>