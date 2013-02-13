<%@ Import namespace="DotNetZoom" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<%@ Control language="vb" Inherits="DotNetZoom.TTT_Gallery" CodeBehind="TTT_Gallery.ascx.vb" AutoEventWireup="false" %>

<style type="text/css">
#fancybox-buttons {
	position: fixed;
	left: 0;
	width: 100%;
	z-index: 1005;
}

#fancybox-buttons.top {
	top: 10px;
}

#fancybox-buttons.bottom {
	bottom: 10px;
}

#fancybox-buttons ul {
	display: block;
	width: 190px;
	height: 30px;
	margin: 0 auto;
	padding: 0;
	list-style: none;
	background: #111;
	-webkit-box-shadow: 0 1px 3px #000,0 0 0 1px rgba(0,0,0,.7),inset 0 0 0 1px rgba(255,255,255,.05);
	-moz-box-shadow: 0 1px 3px #000,0 0 0 1px rgba(0,0,0,.7),inset 0 0 0 1px rgba(255,255,255,.05);
	background: #111 -webkit-gradient(linear,0% 0%,0% 100%,from(rgba(255,255,255,.2)),color-stop(.5,rgba(255,255,255,.15)),color-stop(.5,rgba(255,255,255,.1)),to(rgba(255,255,255,.15)));
	background: #111 -moz-linear-gradient(top,rgba(255,255,255,.2) 0%,rgba(255,255,255,.15) 50%,rgba(255,255,255,.1) 50%,rgba(255,255,255,.15) 100%);
	border-radius: 3px;
}

#fancybox-buttons ul li {
	float: left;
	margin: 0;
	padding: 0;
}

#fancybox-buttons a {
	display: block;
	width: 30px;
	height: 30px;
	text-indent: -9999px;
	background-image: url('/images/buttons.png');
	background-repeat: no-repeat;
	outline: none;
}

#fancybox-buttons a.btnPrev {
	background-position: 6px 0;
}

#fancybox-buttons a.btnNext {
	background-position: -33px 0;
	border-right: 1px solid #3e3e3e;
}

#fancybox-buttons a.btnPlay {
	background-position: 0 -30px;
}
#fancybox-buttons a.btnPlayl {
	background-position: -57px -30px;
}
#fancybox-buttons a.btnPlayOn {
	background-position: -30px -30px;
}

#fancybox-buttons a.btnToggle {
	background-position: 3px -60px;
	border-left: 1px solid #111;
	border-right: 1px solid #3e3e3e;
}

#fancybox-buttons a.btnToggleOn {
	background-position: -27px -60px;
}

#fancybox-buttons a.btnClose {
	border-left: 1px solid #111;
	background-position: -57px 0px;
}

#fancybox-buttons a.btnDisabled {
	opacity : 0.5;
	cursor: default;
}

.btnNone {
    background-position: -30px -30px;
}

</style>

<script language="JavaScript" type="text/javascript">

    function SetFancyBoxDirection(whatdir) {
    if (FancyBoxDirectionD == whatdir)
    {
    FancyBoxDirection = function(){};
    clearTimeout(t);FancyBoxDirectionD=0;
    jQuery('#btnPlay').removeClass('btnNone');
    jQuery('#btnPlay').addClass('btnPlay');
    jQuery('#btnPlayl').removeClass('btnNone');
    jQuery('#btnPlayl').addClass('btnPlayl');
    jQuery('#btnPlay').removeAttr('title');
    jQuery('#btnPlay').attr('title', '<%= DotNetZoom.rtesafe(GetLanguage("Gal_lnkshow")) %>');
    jQuery('#btnPlayl').removeAttr('title');
    jQuery('#btnPlayl').attr('title', '<%= DotNetZoom.rtesafe(GetLanguage("Gal_lnkshow")) %>');
    }
    else {FancyBoxDirectionD = whatdir;}


                                   switch (FancyBoxDirectionD)
                                   {
                                   case 1:
                                   jQuery.fancybox.Dnext();
                                   break;
                                   case 2:
                                   jQuery.fancybox.Dprev();
                                   break;
                                   }
    
    }

    function ToggleFitToScreen(selectedIndex) { if (FancyBoxFitToScreen) {FancyBoxFitToScreen=false}
                                   else {
                                   FancyBoxFitToScreen=true;
                                   } 
                                   jQuery.fancybox.pos(selectedIndex);
                                   };

    function ToggleClass() { if (FancyBoxFitToScreen) {
                                   jQuery('#btnToggle').removeClass('btnToggle');
                                   jQuery('#btnToggle').addClass('btnToggleOn');
                                   jQuery('#btnToggle').removeAttr('title');
                                   jQuery('#btnToggle').attr('title', '<%= DotNetZoom.rtesafe(GetLanguage("MapGoogle_zoom")) %>');

                                   }
                                   else {}
                                   switch (FancyBoxDirectionD)
                                   {
                                   case 1:
                                   jQuery('#btnPlay').removeClass('btnPlay');
                                   jQuery('#btnPlay').addClass('btnNone');
                                   jQuery('#btnPlay').removeAttr('title');
                                   jQuery('#btnPlay').attr('title', '<%= DotNetZoom.rtesafe(GetLanguage("Gal_Stop")) %>');

                                   break;
                                   case 2:
                                   jQuery('#btnPlayl').removeClass('btnPlayl');
                                   jQuery('#btnPlayl').addClass('btnNone');
                                   jQuery('#btnPlayl').removeAttr('title');
                                   jQuery('#btnPlayl').attr('title', '<%= DotNetZoom.rtesafe(GetLanguage("Gal_Stop")) %>');
                                   break;
                                   }
                                                                    
                                   };

        jQuery(document).ready(function () {
        /* Apply fancybox to multiple items */
        
        jQuery("a[rel=flash<%= config.ModuleID.ToString %>]").fancybox({
                'padding':10,
        		'width'	  : <%= config.Fixedwidth %>,
		        'height'  : <%= config.FixedHeight + 24 %>,
                'overlayColor': '#fff', 
                'titleFromAlt' : true,
                'transitionIn':	'elastic',
	            'transitionOut'	:'fade',
	            'speedIn': 250,
	            'speedOut': 250,
	            'overlayOpacity':0.3,
                'type'			: 'swf',
                'swf'			: {
			   	        'wmode'		: 'transparent',
				        'allowfullscreen'	: 'true'
			                      }
        });

        jQuery("a[rel=movie<%= config.ModuleID.ToString %>]").fancybox({
                'autoScale' : true,
		        'width'	  : <%= config.Fixedwidth + 20 %>,
		        'height'  : <%= config.FixedHeight + 35 %>,
                'modal'   : false,
                'showCloseButton' : true,
                'showNavArrows' : true,
                'titleFromAlt' : true,
                'titleFromRev' : true,
                'autoScale'	: true,
                'transitionIn':	'elastic',
	            'transitionOut'	:'fade',
	            'speedIn': 250,
	            'speedOut': 250,
		        'type'	: 'iframe'
	    });



        jQuery("a[rel=image<%= config.ModuleID.ToString %>]").fancybox({
            'autoScale'     : false,
            'margin' : 10,
            'type'	: 'image',
            'transitionIn': 'none',
            'transitionOut': 'none',
            'titleFromRev' : true,
            'cyclic': true,
            'titlePosition': 'over',
            'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
                return '<div id="fancybox-title-over"><div id="fancybox-buttons" class="top"><ul><li><a class="btnPrev" title="<%= DotNetZoom.rtesafe(GetLanguage("Gal_Prev")) %>" href="javascript:jQuery.fancybox.prev();SetFancyBoxDirection(0);"></a></li><li><a id="btnPlayl" class="btnPlayl" title="<%= DotNetZoom.rtesafe(GetLanguage("Gal_lnkshow")) %>" href="javascript:SetFancyBoxDirection(2);"></a></li><li><a id="btnPlay" class="btnPlay" title="<%= DotNetZoom.rtesafe(GetLanguage("Gal_lnkshow")) %>" href="javascript:SetFancyBoxDirection(1);"></a></li><li><a class="btnNext" title="<%= DotNetZoom.rtesafe(GetLanguage("Gal_Next")) %>" href="javascript:jQuery.fancybox.next();SetFancyBoxDirection(0);"></a></li><li><a id="btnToggle" class="btnToggle" title="<%= DotNetZoom.rtesafe(GetLanguage("MapGoogle_zoom")) %>" href="javascript:ToggleFitToScreen('+ currentIndex +');"></a></li><li><a class="btnClose" title="<%= DotNetZoom.rtesafe(GetLanguage("Gal_return")) %>" href="javascript:jQuery.fancybox.close();"></a></li></ul></div><table width="100%"><tr><td><%= DotNetZoom.rtesafe(GetLanguage("F_Image")) %> ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? '   ' + title : '') + '</td><td align="right" width="25"></td></tr></table></div>';},
            'onComplete': function () { t = setTimeout(FancyBoxDirection, <%= Config.SlideshowSpeed %>); ToggleClass(); },
            'onCleanup': function () { clearTimeout(t); },
            'onClosed': function (selectedArray, selectedIndex, selectedOptions) {
                var url = document.URL;
                var newAdditionalURL = "";
                var tempArray = url.split("?");
                var baseURL = tempArray[0];
                var aditionalURL = tempArray[1];
                var temp = "";
                if (aditionalURL) {
                    var tempArray = aditionalURL.split("&");
                    for (var i in tempArray) {
                        if (tempArray[i].indexOf("currentstrip") == -1) {
                            newAdditionalURL += temp + tempArray[i];
                            temp = "&";
                        }
                    }
                };
                var currentstrip = Math.ceil((selectedIndex + 1) / <%= config.ItemsPerStrip %>);
                var rows_txt = temp + "currentstrip=" + currentstrip;
                var finalURL = baseURL + "?" + newAdditionalURL + rows_txt;
                window.location.assign(finalURL)
            }
        });
    });
</script>


<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder ID="pnlModuleContent" Runat="server" EnableViewState="false" >
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:literal id="ImageBefore" runat="server" EnableViewState="false" ></asp:literal>
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
<separatortemplate>&nbsp;/&nbsp;</separatortemplate>
</asp:datalist>&nbsp;
<asp:imagebutton id="ClearCache" runat="server" EnableViewState="false" visible="False" height="16" width="16" ImageURL="~/images/1x1.gif" style="border-width:0px;"></asp:imagebutton>
<asp:imagebutton id="SubAlbum" runat="server" EnableViewState="false" visible="False" height="16" width="16" imageurl="~/Admin/AdvFileManager/Images/NewFolder.gif" style="border-width:0px"></asp:imagebutton>
<asp:imagebutton id="UploadImage" runat="server" EnableViewState="false" visible="False" height="16" width="16" imageurl="~/Admin/AdvFileManager/Images/Upload.gif" style="border-width:0px"></asp:imagebutton>
<asp:imagebutton id="UploadReturn" runat="server" EnableViewState="false" visible="False" height="16" width="16" imageurl="~/images/save.gif" style="border-width:0px"></asp:imagebutton></td>

<td class="TTTHeader" valign="middle" style="white-space: nowrap;" align="right" height="28">
<font color="#ff0000">
<asp:Literal id="lblInfo" runat="server" EnableViewState="false"  Visible="False" />
</font>
<asp:hyperlink id="lnkAdmin" CssClass="CommandButton" runat="server" EnableViewState="false" visible="False"></asp:hyperlink>
<asp:hyperlink id="lnkManager" CssClass="CommandButton" runat="server" EnableViewState="false" visible="False"></asp:hyperlink>
<asp:hyperlink id="BrowserLink" CssClass="CommandButton" runat="server" EnableViewState="false" visible="False"></asp:hyperlink>
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
<SelectedItemStyle Font-Bold="true"  ForeColor="red">
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
<div>
<asp:hyperlink id="Thumb" title="<%# GetItemInfo(Container.DataItem) %>" rev="<%# GetItemInfo(Container.DataItem) %>" rel="<%# GetItemType(Container.DataItem )  %>" runat="server" EnableViewState="false" navigateurl="<%# GetImageURL(Container.DataItem) %>" visible="<%# Not Ctype(Container.DataItem, IGalleryObjectInfo).Thumbnail = string.empty %>">
<asp:Image ID="ImgThumb" rev="<%# GetItemInfo(Container.DataItem) %>" runat="server" AlternateText="<%# Ctype(Container.DataItem, IGalleryObjectInfo).Name %>" BorderWidth="0" imageurl="<%# GetThumbnailURL(Container.DataItem) %>"/>
</asp:hyperlink>
<div>
<asp:ImageButton id="Left" runat="server"  height="16" width="16" visible="<%# CanGoLeft(Container.DataItem) %>" CommandName="left" ImageURL="~/images/lt.gif" BorderWidth="0" BorderStyle="none" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>'>
</asp:ImageButton>
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
<asp:hyperlink id="lnkGoogleMap" runat="server" EnableViewState="false" navigateurl="<%# GetMapURL(Container.DataItem) %>" visible="<%# CanShowMap(Container.DataItem)%>">
<asp:image height="16" width="16" runat="server" id="g"  imageurl="~/images/gps/40Earth.png"></asp:image>
</asp:hyperlink>
<asp:ImageButton id="Delete" runat="server"  height="16" width="16" visible="<%# ItemAuthority(Container.DataItem) %>" CommandName="delete" ImageURL="~/images/1x1.gif" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>'  style='<%# GetImageStyle("0px -32px") %>'>
</asp:ImageButton>
<asp:ImageButton id="Right" runat="server"  height="16" width="16" visible="<%# CanGoRight(Container.DataItem)%>" CommandName="right" ImageURL="~/images/rt.gif" BorderWidth="0" BorderStyle="none" CommandArgument='<%# CType(DataBinder.Eval(Container.DataItem, "Index"), String) %>'>
</asp:ImageButton>
</div>
<div>
<%# Ctype(Container.DataItem, IGalleryObjectInfo).Title %>
<%# GetItemTitle(Container.DataItem)%>
</div>
</div>
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
<asp:datalist id="dlPager2" runat="server"  EnableViewState="false" repeatlayout="Flow" repeatdirection="Horizontal">
<SelectedItemStyle Font-Bold="true" ForeColor="red">
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
<asp:literal id="ImageAfter" runat="server" EnableViewState="false" ></asp:literal>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>