<%@ Import namespace="DotNetZoom" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Control language="vb" Inherits="DotNetZoom.TTT_Gallery" CodeBehind="TTT_Gallery.ascx.vb" AutoEventWireup="false" %>
<script language="javascript" type="text/javascript" src="<%= dotnetzoom.glbPath + "javascript/popup.js"%>"></script>
<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder ID="pnlModuleContent" Runat="server" EnableViewState="false" >
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table class="TTTBorder" cellspacing="0" cellpadding="0" width="100%">
<tr>
<td>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
<tr>
<td class="TTTHeader" style="white-space: nowrap;" height="28">
&nbsp;
<asp:datalist id="dlFolders" runat="server" EnableViewState="false" repeatlayout="Flow" repeatdirection="Horizontal">
<itemtemplate>
<asp:hyperlink cssClass="TTTHeaderText" runat="server" EnableViewState="false" navigateurl='<%# GetFolderURL(Container.DataItem) %>'>
<%# CType(Container.DataItem, FolderDetail).Name %>
</asp:hyperlink>
</itemtemplate>
<separatortemplate>&nbsp;&raquo;&nbsp;</separatortemplate>
</asp:datalist>&nbsp;
<asp:imagebutton id="ClearCache" runat="server" EnableViewState="false" visible="False" height="16" width="16" ImageURL="~/images/1x1.gif" style="border-width:0px;"></asp:imagebutton>
<asp:imagebutton id="SubAlbum" runat="server" EnableViewState="false" visible="False" height="16" width="16" imageurl="~/Admin/AdvFileManager/Images/NewFolder.gif" style="border-width:0px"></asp:imagebutton>
<asp:imagebutton id="UploadImage" runat="server" EnableViewState="false" visible="False" height="16" width="16" imageurl="~/Admin/AdvFileManager/Images/Upload.gif" style="border-width:0px"></asp:imagebutton>
</td>
<td class="TTTHeader" valign="middle" style="white-space: nowrap;" align="right" height="28">
<font color="#ff0000">
<asp:Literal id="lblInfo" runat="server" EnableViewState="false"  Visible="False" />
</font>
<asp:hyperlink id="lnkAdmin" CssClass="CommandButton" runat="server" EnableViewState="false" visible="False"></asp:hyperlink>
<asp:hyperlink id="lnkManager" CssClass="CommandButton" runat="server" EnableViewState="false" visible="False"></asp:hyperlink>
<asp:hyperlink id="BrowserLink" CssClass="CommandButton" runat="server" EnableViewState="false" visible="False"></asp:hyperlink>
<asp:hyperlink id="SlideshowLink" CssClass="CommandButton" runat="server" EnableViewState="false" visible="False"></asp:hyperlink>
&nbsp;</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
<tr>
<td class="TTTAltHeader" colspan="3" height="28">
&nbsp;<span class="TTTNormal">
<asp:Literal id="lblPageInfo" runat="server" EnableViewState="false" />
</span>
<asp:datalist id="dlPager" runat="server" EnableViewState="false" repeatlayout="Flow" repeatdirection="Horizontal">
<SelectedItemStyle ForeColor="red">
</SelectedItemStyle>
<selecteditemtemplate>
<%# Ctype(Container.DataItem, PagerDetail).Text %>
</selecteditemtemplate>
<itemtemplate>
<asp:hyperlink id="hlStrip" runat="server" EnableViewState="false" navigateurl='<%# GetPagerURL(Container.DataItem) %>'>
<%# Ctype(Container.DataItem, PagerDetail).Text %>
</asp:hyperlink>
</itemtemplate>
</asp:datalist>
</td>
<td class="TTTAltHeader" valign="middle" align="center" width="300" height="28">
<span class="NormalBold">
<asp:Literal id="Stats" runat="server" EnableViewState="false" /></span>
</td></tr>
</table>
</td>
</tr>
<tr>
<td >
<table cellspacing="0" cellpadding="0" width="100%" border="0">
<tr>
<td class="TTTRow" colspan="3">
<asp:datalist id="dlStrip" runat="server" EnableViewState="<%# ViewStateAllow() %>" cssclass="NormalBold" DataKeyField="Name" RepeatDirection="Horizontal" cellpadding="15" width="100%">
<ItemStyle horizontalalign="Center" verticalalign="Bottom"></ItemStyle>
<ItemTemplate>
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="center" valign="top" height="100%">
<asp:hyperlink id="Thumb" runat="server" EnableViewState="false" onmouseover='<%# GetImageToolTip( Container.DataItem )  %>' navigateurl="<%# GetBrowserURL(Container.DataItem) %>" visible="<%# Not Ctype(Container.DataItem, IGalleryObjectInfo).Thumbnail = string.empty %>">
<asp:Image ID="ImgThumb" runat="server" AlternateText="*" BorderWidth="0" imageurl="<%# Ctype(Container.DataItem, IGalleryObjectInfo).Thumbnail %>"/>
</asp:hyperlink>
</td>
</tr>
</table>
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="center" valign="bottom" height="20">
<asp:hyperlink id="Name" CssClass="TTTAltHeaderText" runat="server" EnableViewState="false" navigateurl="<%# GetBrowserURL(Container.DataItem) %>"  >
<%# Ctype(Container.DataItem, IGalleryObjectInfo).Title %>
</asp:hyperlink>
<br>
<span Class="TTTNormal">
<%# GetItemInfo(Container.DataItem) %>
</span>
</td>
</tr>
<tr>
<td align="center" valign="top" height="3"></td>
</tr>
<tr>
<td align="center" valign="top" height="20">
<asp:hyperlink id="lnkSlideshow" visible="<%# CanView(Container.DataItem) %>" navigateurl="<%# GetSlideshowURL(Container.DataItem) %>"  EnableViewState="false" runat="server">
<%# SetImage("0px -288px") %>
</asp:hyperlink>
<asp:hyperlink id="lnkDiscussion" runat="server" EnableViewState="false" navigateurl="<%# GetForumURL(Container.DataItem) %>" visible="<%# CanDiscuss()%>">
<%# SetImage("0px -48px") %>
</asp:hyperlink>
<asp:hyperlink id="lnkEditRes" runat="server" EnableViewState="false" visible="<%# ItemIconAuthority(Container.DataItem) %>" navigateurl="<%# GetEditIconURL(Container.DataItem) %>">
<%#SetImage("0px 0px")%>
</asp:hyperlink>
<asp:hyperlink id="lnkEdit" runat="server" EnableViewState="false" visible="<%# ItemAuthority(Container.DataItem) %>" navigateurl="<%# GetEditURL(Container.DataItem) %>">
<%# SetImage("0px -128px")%>
</asp:hyperlink>
<asp:ImageButton id="btnDownload" runat="server"  height="16" width="16" visible="<%# CanDownload(Container.DataItem) %>" CommandName="edit" ImageURL="~/images/1x1.gif" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>' style='<%# GetImageStyle("0px -96px") %>'>
</asp:ImageButton>
<asp:ImageButton id="Delete" runat="server"  height="16" width="16" visible="<%# ItemAuthority(Container.DataItem) %>" CommandName="delete" ImageURL="~/images/1x1.gif" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>'  style='<%# GetImageStyle("0px -32px") %>'>
</asp:ImageButton>
</td>
</tr>
</table>
</ItemTemplate>
</asp:datalist>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
<tr>
<td class="TTTAltHeader" colspan="3" height="28">
&nbsp;
<span Class="TTTNormal">
<asp:Literal id="lblPageInfo2" runat="server" EnableViewState="false" /></span>
<asp:datalist id="dlPager2" runat="server" EnableViewState="false" repeatlayout="Flow" repeatdirection="Horizontal">
<SelectedItemStyle ForeColor="red">
</SelectedItemStyle>
<selecteditemtemplate><%# Ctype(Container.DataItem, PagerDetail).Text %></selecteditemtemplate>
<itemtemplate>
<asp:hyperlink id="Hyperlink1" runat="server" EnableViewState="false" navigateurl='<%# GetPagerURL(Container.DataItem) %>'>
<%# Ctype(Container.DataItem, PagerDetail).Text %>
</asp:hyperlink>
</itemtemplate>
</asp:datalist>
</td>
</tr>
</table>
</td>
</tr>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>