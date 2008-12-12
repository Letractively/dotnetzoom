<%@ Control Inherits="DotNetZoom.TTT_ForumStatistics" codebehind="TTT_ForumStatistics.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ import Namespace="DotNetZoom" %>
<table  cellspacing="0" cellpadding="0" width="100%">
<tr>
<td class="TTTHeader" width="100%">&nbsp;<img height="15" width="17" src="images/1x1.gif" Alt="uo" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -30px;">&nbsp;&nbsp;<%= DotNetZoom.getlanguage("F_StatsForum") %>
</td>
</tr>
<tr>
<td class="tttRow" width="100%"><br>    
&nbsp;<img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -119px;">&nbsp;<asp:Literal id="lblTotalUsers" runat="server" EnableViewState="false" /><asp:Literal id="lblTotalThreads" runat="server" EnableViewState="false" /><asp:Literal id="lblTotalPosts" runat="server" EnableViewState="false" visible="True" /><br>
&nbsp;<img height="12" width="18" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -147px;">&nbsp;<%= DotNetZoom.getlanguage("F_Stats24") %>&nbsp;<asp:Literal id="lblNewThreadsInPast24Hours" runat="server" EnableViewState="false" /><asp:Literal id="lblNewPostsInPast24Hours" runat="server" EnableViewState="false" /><br>
&nbsp;<img height="12" width="18" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -159px;">&nbsp;<%= DotNetZoom.getlanguage("F_StatsMost") %>&nbsp;<asp:Literal id="lblMostViewedThread" runat="server" EnableViewState="false" /><br>
&nbsp;<img height="12" width="18" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -147px;">&nbsp;<%= DotNetZoom.getlanguage("F_StatsMostPost") %>&nbsp;<asp:Literal id="lblMostActiveThread" runat="server" EnableViewState="false" /><br>
&nbsp;<img height="14" width="17" src="images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -119px;">&nbsp;<%= DotNetZoom.getlanguage("F_StatsTop10") %>&nbsp;<asp:Literal id="lbl10ActiveUsers" runat="server" EnableViewState="false" />
</td>
</tr>
</table>
                