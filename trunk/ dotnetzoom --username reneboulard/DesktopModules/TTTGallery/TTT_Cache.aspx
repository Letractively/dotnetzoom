<%@ Register TagPrefix="portal" TagName="Footer" Src="~/controls/DesktopPortalFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/controls/DesktopPortalBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TTT_Cache.aspx.vb" Inherits="DotNetZoom.TTT_GalleryCache" enableViewState="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
	<title>Cache</title>
	</head>
	<body onload="postredirect();">		
		<br>
		<div class="Normal" width="200" align="center"><%= DotNetZoom.GetLanguage("Gal_CacheGen")%></div>
		<br>
		<% BuildCache() %>
		<br>
		<br>
		<div class="Normal" width="200" align="center"><%= DotNetZoom.GetLanguage("Gal_CacheRed")%>
			<asp:HyperLink id="ContinueGO" runat="server" CssClass="Normal">&nbsp;<%= DotNetZoom.GetLanguage("Gal_Cont")%> </asp:HyperLink></div>
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