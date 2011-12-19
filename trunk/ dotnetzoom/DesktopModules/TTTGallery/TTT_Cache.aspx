<%@ Register TagPrefix="portal" TagName="Footer" Src="~/controls/DesktopPortalFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/controls/DesktopPortalBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TTT_Cache.aspx.vb" Inherits="DotNetZoom.TTT_GalleryCache" enableViewState="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title>Cache</title>
<asp:placeholder id="CSS" runat="server"></asp:placeholder>
</head>
<body onload="postredirect();">	
<div align="center">
	<table class="TTTBorder" cellSpacing="1" cellPadding="0" align="center">
	<tr><td class="TTTHeader" valign="middle" height="28"><%= DotNetZoom.GetLanguage("Gal_CacheGen")%></td></tr>
	<tr><td align="center" valign="middle" class="TTTRow">
		<table><tr><td><img  src="/images/1x1.gif" alt="-" style="border-width:0px;height:100px;width:550px;" ></td></tr>
		<tr><td align="center">
	<img alt="activity indicator" src="/images/ajax-loader.gif">
		</td></tr>
                <tr><td><img  src="/images/1x1.gif" alt="-" style="border-width:0px;height:100px;width:550px;" ></td></tr>
		</table>
	</td></tr>
	<tr><td align="center" class="TTTRow" valign="middle" height="22">
<asp:HyperLink id="ContinueGO" runat="server" CssClass="Normal">&nbsp;<%= DotNetZoom.GetLanguage("Gal_Cont")%> </asp:HyperLink>
	</td></tr>
	</table>
<div>
<br>
<% BuildCache() %>
<br>
		<script type="text/javascript" language="javascript">
		<!-- Hide from older browsers
		function postredirect()
		{
		window.location = '<% =NewURL() %>';
		}
		-->
		</script>		
	</body>
</html>