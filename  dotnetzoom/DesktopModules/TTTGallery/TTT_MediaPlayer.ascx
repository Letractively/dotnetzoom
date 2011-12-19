<%@ Control language="vb" CodeBehind="TTT_MediaPlayer.ascx.vb" AutoEventWireup="false" Inherits="DotNetZoom.TTT_MediaPlayer" %>
<table cellSpacing="0" cellPadding="0" align="center" Class="TTTMedia">
	<tr>
		<td class="TTTHeader" id="TitleBox" valign="middle" align="center" width="100%" height="28"><asp:label id="lblTitle" Runat="server" CssClass="TTTHeaderText"></asp:label></td>
	</tr>
	<tr>
	<td Runat="server" EnableViewState="false" visible="false" ID="mpg" valign="middle" align="center" width="100%" height="100%">
	
			<OBJECT id="Player" CLASSID="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" width="640" height="480" type="application/x-oleobject">
						<PARAM Name="AutoStart" Value="true">
						<PARAM NAME="SendPlayStateChangeEvents" VALUE="True">
						<PARAM Name="uiMode" Value="full">
						<PARAM Name="URL" <%= "Value=""" & MovieURL() & """" %>>
			<EMBED <%= "src=""" & Trim(MovieURL()) & """" %> ></EMBED>
			</OBJECT>
	</td>

	<td Runat="server" EnableViewState="false" visible="false" ID="wmv" valign="middle" align="center" width="100%" height="100%">
<script type="text/javascript" src="/javascript/silverlight.js"></script>
<script type="text/javascript"  src="/javascript/wmvplayer.js"></script>
<div id='mediaspace'></div>
<script type='text/javascript'>
 var cnt = document.getElementById('mediaspace');
 var src = '/javascript/wmvplayer.xaml';
 var cfg = {height:'480', width:'640', file:'<%= MovieURL() %>', backcolor:'000000', frontcolor:'FFFFFF', lightcolor:'FF6600', start:'10', overstretch:'true'};
 var ply = new jeroenwijering.Player(cnt,src,cfg);
</script> 
	
	</td>
		
	<td Runat="server" EnableViewState="false" visible="false" ID="flv" valign="middle" align="center" width="100%" height="100%">
		<p id='preview'>The player will show in this paragraph</p>
		<script type='text/javascript' src='/javascript/swfobject.js'></script>
		<script type='text/javascript'>
		var s1 = new SWFObject('/javascript/player.swf','player','640','480','9');
		s1.addParam('allowfullscreen','true');
		s1.addParam('allowscriptaccess','always');
        s1.addParam('flashvars','file=<%= MovieURL() %>');
		s1.write('preview');
		</script>
	</td>
		
	</tr>
	<tr>
		<td valign="top" align="center" Class="TTTAltHeader" height="28" width="100%"><asp:Button cssclass="button" id="btnBack" runat="server"></asp:Button></td>
	</tr>
</table>
<asp:Label id="ErrorMessage" CssClass="TTTNormalBold" runat="server" Visible="False"></asp:Label>