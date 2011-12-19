<%@ Page Language="vb" AutoEventWireup="false" enableviewstate="false" Codebehind="TTT_MediaPlayer.aspx.vb" Inherits="DotNetZoom.TTT_GalleryMediaPlayer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<title><%= DotNetZoom.GetLanguage("Media_Player") %></title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta http-equiv="Content-Script-Type" content="text/javascript">
		<asp:placeholder id="CSS" runat="server"></asp:placeholder>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
<table cellSpacing="0" cellPadding="0" align="center" Class="TTTMedia">
		<asp:label id="lblTitle" Runat="server" CssClass="TTTHeaderText" visible="False"></asp:label>
	<tr>
	<td Runat="server" EnableViewState="false" visible="false" ID="mpg" valign="middle" align="center" width="100%" height="100%">
	
			<OBJECT id="Player" CLASSID="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" width="<%= Zconfig.FixedWidth.ToString() %>" height="<%= Zconfig.FixedHeight.ToString() %>" type="application/x-oleobject">
						<PARAM Name="AutoStart" Value="true">
						<PARAM NAME="SendPlayStateChangeEvents" VALUE="True">
						<PARAM Name="uiMode" Value="full">
						<PARAM Name="URL" <%= "Value=""" & MovieURL() & """" %>>
			<EMBED <%= "src=""" & MovieURL() & """" %> ></EMBED>
			</OBJECT>
	</td>

	<td Runat="server" EnableViewState="false" visible="false" ID="wmv" valign="middle" align="center" width="100%" height="100%">
<script type="text/javascript" src="/javascript/silverlight.js"></script>
<script type="text/javascript"  src="/javascript/wmvplayer.js"></script>
<div id='mediaspace'></div>
<script type='text/javascript'>
 var cnt = document.getElementById('mediaspace');
 var src = '/javascript/wmvplayer.xaml';
 var cfg = {height:'<%= Zconfig.FixedHeight.ToString() %>', width:'<%= Zconfig.FixedWidth.ToString() %>', file:'<%=MovieURL()%>', backcolor:'000000', frontcolor:'FFFFFF', lightcolor:'FF6600', start:'10', overstretch:'true'};
 var ply = new jeroenwijering.Player(cnt,src,cfg);
</script> 
	
	</td>
		
	<td Runat="server" EnableViewState="false" visible="false" ID="flv" valign="middle" align="center" width="100%" height="100%">
		<p id='preview'>The player will show in this paragraph</p>
		<script type='text/javascript' src='/javascript/swfobject.js'></script>
		<script type='text/javascript'>
		var s1 = new SWFObject('/javascript/player.swf','player','<%= Zconfig.FixedWidth.ToString() %>','<%= Zconfig.FixedHeight.ToString() %>','9');
		s1.addParam('allowfullscreen','true');
		s1.addParam('allowscriptaccess','always');
        s1.addParam('flashvars','file=<%=MovieURL()%>');
		s1.write('preview');
		</script>
	</td>
	</tr>
</table>
<asp:Label id="ErrorMessage" CssClass="TTTNormalBold" runat="server" Visible="True"></asp:Label>

		</form>
	</body>
</html>