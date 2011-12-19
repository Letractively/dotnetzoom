<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Control language="vb" CodeBehind="TTT_Slideshow.ascx.vb" AutoEventWireup="false" Inherits="DotNetZoom.TTT_Slideshow" %>
<script language="javascript" type="text/javascript">
			function runSlideShow(){
			if (document.all){
				document.images.SlideShow.style.filter="blendTrans(duration=2)"
				document.images.SlideShow.style.filter="blendTrans(duration=crossFadeDuration)"
				document.images.SlideShow.filters.blendTrans.Apply()      
			}
			document.images.SlideShow.src = preLoad[j].src		
			if (document.getElementById) document.getElementById("TitleBox").innerHTML=Title[j].replace("^", "'")	
			if (document.getElementById) document.getElementById("CaptionBox").innerHTML=Description[j].replace("^", "'")
			if (document.all){
				document.images.SlideShow.filters.blendTrans.Play()
			}
			j = j + 1  			   
			if (j > (p-1)) j=0
			t = setTimeout('runSlideShow()', slideShowSpeed)
			}			
</script>
	<asp:label id="ClientJavascript" EnableViewState="false" runat="server"></asp:label>
	<asp:PlaceHolder id="pnlModuleContent" Runat="server">
		<table class="TTTBorder" cellSpacing="0" cellPadding="0" align="center">
			<tr>
				<td class="TTTHeader" id="TitleBox" align="center" valign="middle" height="28"><%= DotNetZoom.getlanguage("Gal_Loading") %></td>
			</tr>
			<tr>
				<td id="img" Runat="server" valign="middle" align="center" class="TTTRow">
					<asp:Label id="ImageSrc" EnableViewState="false" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="TTTRow" height="24" id="CaptionBox" align="center" valign="middle"></td>
			</tr>
			<tr>
				<td class="TTTAltHeader" align="center" valign="top" height="28">
					<asp:Button cssclass="button" id="btnBack" runat="server"></asp:Button></td>
			</tr>
		</table>
		<asp:Label id="ErrorMessage" runat="server" EnableViewState="false" CssClass="TTTNormalBold" Visible="False"></asp:Label>
<script language="JavaScript" type="text/javascript"> runSlideShow(); </script>
	</asp:PlaceHolder>