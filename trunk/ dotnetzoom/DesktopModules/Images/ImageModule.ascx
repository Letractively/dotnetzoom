<%@ Control language="vb" Inherits="DotNetZoom.ImageModule" CodeBehind="ImageModule.ascx.vb" AutoEventWireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<portal:title runat="server" EnableViewState="false" id=Title1 />
<asp:PlaceHolder id="pnlModuleContent" Runat="server" EnableViewState="false">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:image id="imgImage" runat="server" EnableViewState="false" />
<asp:placeholder id="imgmenu" Runat="server" EnableViewState="False" visible="False">
<div class="nav">
<table cellpadding="0" cellspacing="0">
    <tr>
        <td id="Td1" align="left" runat="server" valign="top" width="200">
	        <ul>
	        <li class="folder" style="z-index: 1"><asp:image id="Image1" runat="server" EnableViewState="false" />
	            <ul>
	            <li id="GoogleEarth" runat="server" EnableViewState="false" visible="false" class="items"><asp:LinkButton id="cmdSendKML" runat="server" ><img  src="/images/icon_sitesettings_16px.gif" alt="" style="border-width:0px;" /> Google Earth</asp:LinkButton></li>
	            <li id="GoogleMap" runat="server" EnableViewState="false" visible="false" class="items"><a href=<%= GoogleMapURL %>><img  src="/images/icon_sitesettings_16px.gif" alt="" style="border-width:0px;" /> Google Map</a></li>
	            <li id="Info" runat="server" EnableViewState="false" visible="false" class="items"><a href=""><img  src="/images/icon_sitesettings_16px.gif" alt="" style="border-width:0px;" /> Information</a></li>
	            <li id="infoExif" runat="server" EnableViewState="false" visible="false" class="items"><a href=""><img  src="/images/icon_sitesettings_16px.gif" alt="" style="border-width:0px;" /> Exif</a></li>
	            <li id="Link" runat="server" EnableViewState="false" visible="false" class="items"><a href=<%= LinkURL %>><img  src="/images/icon_sitesettings_16px.gif" alt="" style="border-width:0px;" /> Link</a></li>
	            </ul>
	        </li>
	        </ul>
	    </td>
	</tr>
</table>
</div>
</asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>