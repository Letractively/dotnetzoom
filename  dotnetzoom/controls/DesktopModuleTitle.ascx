<%@ Control CodeBehind="DesktopModuleTitle.ascx.vb" language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetZoom.DesktopModuleTitle" %>
<%@ Register TagPrefix="cc1" Namespace="SolpartWebControls" Assembly="SolpartWebControls" %>
<asp:literal id="Titlebefore" visible="false" runat="server" EnableViewState="false"></asp:literal>
<asp:PlaceHolder ID="pnlAdminTitle" Runat="server" Visible="False" EnableViewState="false">
<table class='<asp:literal id="TableTitle1" runat="server" />' width="100%">
	<tr>
	<td align="left" valign="middle">
	<asp:Image Visible="False" BorderWidth="0" runat=server ID="cmdEditModuleImage1"/>
	</td>
	<td align="left" valign="middle" style="white-space: nowrap;">
	<asp:label id="lblEditModuleTitle" runat="server" EnableViewState="false" />
	</td>
	<td id="CellEdit" Runat="server" EnableViewState="false" visible="false" align="left" valign="middle">
	<asp:Hyperlink ID="cmdEditContent1" Runat="server" EnableViewState="false"><img  src="/images/edit.gif" alt="*" style="border-width:0px;" /></asp:Hyperlink>
	</td>
	<td id="cellOptions" Runat="server" EnableViewState="false" visible="false" align="left" valign="middle">
	<asp:Hyperlink ID="cmdEditOptions1" Visible=False Runat="server" EnableViewState="false"><img  src="/images/view.gif" alt="*" style="border-width:0px;"></asp:Hyperlink>
	</td>
	<td id="cellOptions2" Runat="server" EnableViewState="false" visible="false" align="left" valign="middle">
	<asp:Hyperlink ID="cmdEditOptions2" Visible=False Runat="server" EnableViewState="false"><img  src="/images/view.gif" alt="*" style="border-width:0px;"></asp:Hyperlink>
	</td>
	<td Runat="server" EnableViewState="false" visible="false" width="100%" ID="cAdmin" Height="30"  align="right" valign="middle">
    <asp:literal id="cmdAdmin" runat="server" Visible="False" EnableViewState="false" />
    </td>
	<td id="cellhelp" width="100%" Runat="server" EnableViewState="true" visible="false" align="right" valign="middle" >
	<asp:HyperLink EnableViewState="true" ID="help1" visible="false" runat="server">
	<img height="12" width="12" border="0" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -362px;">
	</asp:HyperLink>
	</td>
</table>
</asp:PlaceHolder>
<asp:PlaceHolder ID="pnlModuleTitle" Runat="server" Visible="False" EnableViewState="false">
<div id="divModuleTitle" runat="server" class="nav">
<table width="100%" class='<asp:literal id="TableTitle" runat="server" />'>
	<tr>
	<td>
	<cc1:solpartmenu id="ctlMenu" MenuEffects-MouseOutHideDelay="500" runat="server" MouseOutHideDelay="1"  ForceDownlevel="False" Moveable="False" IconWidth="0" MenuEffects-MouseOverExpand="True" MenuEffects-MouseOverDisplay="Highlight" MenuEffects-MenuTransitionStyle=" " SystemImagesPath="/" SeparateCSS="True" MenuCSSPlaceHolderControl="SPMenuStyle" MenuCSS-SubMenu="Option_SubMenu" MenuCSS-RootMenuArrow="Option_RootMenuArrow" MenuCSS-MenuItemSel="Option_MenuItemSel" MenuCSS-MenuItem="Option_MenuItem" MenuCSS-MenuIcon="Option_MenuIcon" MenuCSS-MenuContainer="Option_MenuContainer" MenuCSS-MenuBreak="Option_MenuBreak" MenuCSS-MenuBar="Option_MenuBar" MenuCSS-MenuArrow="Option_MenuArrow"></cc1:solpartmenu>
	</td>
	<td id="rowadmin1" runat="server" EnableViewState="false" visible="false" align="left" valign="middle" style="white-space: nowrap;">
	<ul>
	<li class="folder" style="z-index: 1"><a title="<%= DotNetZoom.GetLanguage("modifier")%>" href="javascript:toggleBox('nav',1)" ><img  src="/images/action.gif" alt="" style="border-width:0px;"></a>
	<ul id="MC" runat="server" EnableViewState="false" visible="true">
	<li id="modifier" runat="server" EnableViewState="false" visible="false" class="items"><asp:Hyperlink id="cmdEditModule" runat="server" EnableViewState="false" Visible="False"><img  src="/images/icon_sitesettings_16px.gif" alt="" style="border-width:0px;" /> </asp:Hyperlink></li>
	<li id="modifierC" runat="server" EnableViewState="false" visible="false" class="items"><asp:Hyperlink ID="cmdEditContent" Runat="server" EnableViewState="false"><img  src="/images/edit.gif" alt="*" style="border-width:0px;" /> </asp:Hyperlink></li>
	<li id="param" runat="server" EnableViewState="false" visible="false" class="items"><asp:Hyperlink ID="cmdEditOptions" Visible=False Runat="server" EnableViewState="false"><img  src="/images/view.gif" alt="*" style="border-width:0px;"> </asp:Hyperlink></li>
	<li id="param2" runat="server" EnableViewState="false" visible="false" class="items"><asp:Hyperlink ID="cmdEditOptions4" Visible=False Runat="server" EnableViewState="false"><img  src="/images/view.gif" alt="*" style="border-width:0px;"> </asp:Hyperlink></li>
	<li id="delete" runat="server" EnableViewState="false" visible="false" class="items"><asp:LinkButton id="cmddelete" runat="server" EnableViewState="false" Visible="False" ><img  src="/images/delete.gif" alt="*" style="border-width:0px;"> </asp:LinkButton></li>
	<li id="top" runat="server" EnableViewState="false" visible="false" class="items"><asp:LinkButton id="cmdModuleTop" runat="server" EnableViewState="false" Visible="False" ><img  src="/images/up1.gif" alt="*" style="border-width:0px;"> </asp:LinkButton></li>
	<li id="haut" runat="server" EnableViewState="false" visible="false" class="items"><asp:LinkButton id="cmdModuleUp" runat="server" EnableViewState="false" Visible="False" ><img  src="/images/up.gif" alt="*" style="border-width:0px;"> </asp:LinkButton></li>
	<li id="bas" runat="server" EnableViewState="false" visible="false" Class="items"><asp:LinkButton id="cmdModuleDown" runat="server" EnableViewState="false" Visible="False" ><img  src="/images/dn.gif" alt="*" style="border-width:0px;" > </asp:LinkButton></li>
	<li id="gauche" runat="server" EnableViewState="false" visible="false" Class="items"><asp:LinkButton id="cmdModuleLeft" runat="server" EnableViewState="false" Visible="False" ><img  src="/images/lt.gif" alt="*" style="border-width:0px;" > </asp:LinkButton></li>
	<li id="droite" runat="server" EnableViewState="false" visible="false" Class="items"><asp:LinkButton id="cmdModuleRight" runat="server" EnableViewState="false" Visible="False" ><img  src="/images/rt.gif" alt="*" style="border-width:0px;" > </asp:LinkButton></li>
	<li id="bottom" runat="server" EnableViewState="false" visible="false" Class="items"><asp:LinkButton id="cmdModuleBottom" runat="server" EnableViewState="false" Visible="False" ><img  src="/images/dn1.gif" alt="*" style="border-width:0px;" > </asp:LinkButton></li>
	<li id="purger" runat="server" EnableViewState="false" visible="false" Class="items"><asp:LinkButton id="cmdModuleRefresh" runat="server" EnableViewState="false" Visible="False"><img  src="/images/restore.gif" alt="*" style="border-width:0px;"> </asp:LinkButton></li>
	</ul>
	</li>
	</ul>
	</td>
	<td align="left" valign="middle" width="100%" style="white-space: nowrap;">
	<asp:Image Visible="False" BorderWidth="0" runat=server ID="cmdEditModuleImage"/>
	&nbsp;
	<asp:label id="lblModuleTitle" cssclass="Head" runat="server" EnableViewState="false" />&nbsp;
	</td>
	<asp:PlaceHolder ID="pnlHelp" Runat="server" EnableViewState="true" visible="false">
		<td align="right" valign="middle" >
		<asp:HyperLink EnableViewState="true" ID="help" visible="false" runat="server">
            <img height="12" width="12" border="0" src="/images/1x1.gif" Alt="*" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -362px;">
		</asp:HyperLink>
		</td>
	</asp:PlaceHolder>
	<td id="rowDisplay" runat="server" EnableViewState="false" visible="false" align="right" valign="top" >
	<asp:LinkButton ID="cmdDisplayModule" Runat="server"  EnableViewState="true" Visible="False" CausesValidation="False">
	<img height="12" width="12" border="0" src="/images/1x1.gif" Alt="-" style="<%= styleplusminus %>"></asp:LinkButton>
	</td>
	</tr>
	</table>
</div>
</asp:PlaceHolder>
<asp:Literal ID="contextmenu" runat="server" Visible="false" EnableViewState="false"></asp:Literal>
<asp:literal id="Titleafter" runat="server" visible="false" EnableViewState="false"></asp:literal>