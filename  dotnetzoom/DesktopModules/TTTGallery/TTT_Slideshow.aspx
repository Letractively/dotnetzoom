<%@ Page Language="vb" AutoEventWireup="false" enableviewstate="false" Codebehind="TTT_Slideshow.aspx.vb" Inherits="DotNetZoom.TTT_GallerySlideshow" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<title><%= DotNetZoom.GetLanguage("Slideshow_Player") %></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
		<asp:placeholder id="CSS" runat="server"></asp:placeholder>

	</head>
	<body onload="runSlideShow()">
		<script type="text/javascript" language="javascript">
			function runSlideShow(){
			if (document.all){
				document.images.SlideShow.style.filter="blendTrans(duration=2)"
				document.images.SlideShow.style.filter="blendTrans(duration=crossFadeDuration)"
				document.images.SlideShow.filters.blendTrans.Apply()      
			}
			document.images.SlideShow.src = preLoad[j].src		
			if (document.getElementById) document.getElementById("TitleBox").innerHTML= Title[j].replace("^", "'")	
			if (document.getElementById) document.getElementById("CaptionBox").innerHTML= Description[j].replace("^", "'")
			if (document.all){
				document.images.SlideShow.filters.blendTrans.Play()
			}
			j = j + 1  
			   
			if (j > (p-1)) j=0
			t = setTimeout('runSlideShow()', slideShowSpeed)
			}
		</script>
		<form id="Form1" method="post" runat="server">
			<asp:label id="ClientJavascript" EnableViewState="false" runat="server"></asp:label>
			<asp:PlaceHolder id="pnlModuleContent" Runat="server">
				<table class="TTTBorder" cellSpacing="0" cellPadding="0" align="center">
					<tr>
						<td class="TTTHeader" id="TitleBox" align="center" valign="middle" height="28"><%= DotNetZoom.getlanguage("Gal_Loading") %></td>
					</tr>
					<tr>
                    	<td id="img" Runat="server" valign="middle" align="center" class="TTTRow">
							<asp:Label id="ImageSrc" EnableViewState="false" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="TTTAltHeader" id="CaptionBox" align="center" valign="middle" height="24"></td>
					</tr>
				</table>
				<asp:Label id="ErrorMessage" EnableViewState="false" runat="server" Visible="False" CssClass="TTTNormalBold"></asp:Label>
			</asp:PlaceHolder></form>
	</body>
</html>