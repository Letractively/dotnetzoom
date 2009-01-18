<%@ Control Language="vb" autoeventwireup="false" codebehind="Enligne.ascx.vb" Inherits="DotNetZoom.Enligne" targetschema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server" EnableViewState="false">
</portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server" EnableViewState="false">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<img height="15" width="17" src="/images/1x1.gif" Alt="ip" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -30px;">
&nbsp;<%= DotNetZoom.GetLanguage("ip_address")%>&nbsp;:
<asp:Label cssclass="normal" id="IPMessage" runat="server" EnableViewState="false" forecolor="Blue">
</asp:Label>
<br>
<img height="15" width="17" src="/images/1x1.gif" Alt="ip" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -30px;">
&nbsp;<%= DotNetZoom.GetLanguage("browser")%>&nbsp;:
<span class="normal" style="color:Blue;"><%=  GetBrowserType %></span>
<br>
<img height="15" width="17" src="/images/1x1.gif" Alt="En ligne" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -15px;">
&nbsp;<%= DotNetZoom.GetLanguage("number_of_connections")%>&nbsp;:
<asp:Label cssclass="normal" id="Message" runat="server" EnableViewState="false" forecolor="Blue">
</asp:Label>
<br>
<img height="15" width="17" src="/images/1x1.gif" Alt="depuis" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px 0px;">

&nbsp;<%= DotNetZoom.GetLanguage("server_up_time")%>&nbsp;:
<table>
<tr>
<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</td>
<td><asp:Label cssclass="normal" id="ServeurMessage" runat="server" EnableViewState="false"  forecolor="Blue" >
</asp:Label>
</td>
</tr>
</table>
<br><img height="15" width="17" src="/images/1x1.gif" Alt="img" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -60px;">

&nbsp;
<span class="normal"><%= DotNetZoom.GetLanguage("number_of_restart")%>&nbsp;:</span>
<asp:Label cssclass="normal" id="lblAppRestarts" runat="server" EnableViewState="false" forecolor="Blue">
</asp:Label>
<br>
<img height="15" width="17" src="/images/1x1.gif" Alt="mem" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -45px;">
&nbsp;
<span class="normal"><%= DotNetZoom.GetLanguage("free_memory")%>&nbsp;:</span>
<asp:Label cssclass="normal" id="lblFreeMem" runat="server" EnableViewState="false" forecolor="Blue">
</asp:Label>
<br>
<img height="15" width="17" src="/images/1x1.gif" Alt="mem" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -45px;">
&nbsp;
<span class="normal"><%= DotNetZoom.GetLanguage("cache_memory_used")%>&nbsp;:&nbsp;</span><span class="normal" style="color:Blue;"><%=Cache.Count.ToString()%></span>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>