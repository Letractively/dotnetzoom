<%@ Control codebehind="DesktopPortalBanner.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.DesktopPortalBanner" %>
<%@ import Namespace="DotNetZoom" %>
<%@ Register TagPrefix="mail" TagName="check" Src="~/controls/mailcheck.ascx" %>
<div id="TopContextMenu">
<div id="banner">
<asp:hyperlink id="hypBanner" runat="server" EnableViewState="false">
<asp:Image ID="hypBannerImage" BorderWidth="0" runat="server" EnableViewState="false"></asp:Image>
</asp:hyperlink><div id="logo"><asp:literal id="Flash" runat="server" EnableViewState="false"></asp:Literal><asp:hyperlink id="hypLogo" runat="server" EnableViewState="false" >
<asp:Image ID="hypLogoImage" cssclass="LogoImage" BorderWidth="0" runat="server" EnableViewState="false"></asp:Image>
</asp:hyperlink>
</div>
<div id="info">
    <asp:Table  Width="100%" ID="Table1"  runat="server">
    <asp:TableRow id="row1" runat="server">
   <asp:TableCell ID="Cell1" Height="30" VerticalAlign="Bottom" HorizontalAlign="Left" Wrap="false">
&nbsp;<asp:Label id="lblDate" cssclass="SelectedTab" runat="server" EnableViewState="false" >Date</asp:Label>
</asp:TableCell>
<asp:TableCell ID="editpanel" Height="30" width="100%" visible="false" VerticalAlign="Bottom" HorizontalAlign="Left" Wrap="false">
    <div class="nav">
	<ul>
	<li class="folder" style="z-index: 3"><a title="<%= DotNetZoom.GetLanguage("admin_menu")%>" href="javascript:toggleBox('adminmenu',1)" ><img src="/images/edit.gif" alt="" style="border-width:0px;"></a>
	<ul id="adminmenu">
	<li id="liadmin" runat="server" EnableViewState="false" visible="False" class="items"><asp:HyperLink id="cmdAdminMenu" runat="server" EnableViewState="false"><img  src="/images/icon_site_16px.gif" alt="*" style="border-width:0px;"> </asp:HyperLink></li>
	<li class="items"><asp:LinkButton id="cmdPreview"  Visible="False" runat="server" EnableViewState="false" BorderWidth="0" BorderStyle="none" CausesValidation="False"></asp:LinkButton></li>
	<li id="liaddtab" runat="server" EnableViewState="false" visible="False" class="items"><asp:Hyperlink id="cmdAddTab" runat="server" EnableViewState="false"><img  src="/images/add1.gif" alt="" style="border-width:0px;"> </asp:Hyperlink></li>
	<li id="liedittab" runat="server" EnableViewState="false" visible="False" class="items"><asp:Hyperlink id="cmdEditTab" runat="server" EnableViewState="false"><img  src="/images/editpage.gif" alt="*" style="border-width:0px;"> </asp:Hyperlink></li>
	<li id="lideletetab" runat="server" enableviewstate="false" visible="false" class="items"><asp:linkbutton BorderWidth="0" id="cmdDelete" runat="server"  CausesValidation="False"><img src="/images/delete.gif" alt="X" style="border-width:0px;"> </asp:linkbutton></li>
	<li class="items"><asp:LinkButton id="cmdAddModule" runat="server" EnableViewState="false"  BorderWidth="0" CausesValidation="False"><img  src="/images/add2.gif" alt="*" style="border-width:0px;"> </asp:LinkButton></li>
	<li id="tigraedit" runat="server" EnableViewState="false" visible="False" class="items"><asp:Hyperlink id="TiGraLink" runat="server" EnableViewState="true"><img  src="/images/tigra_builder.gif" alt="*" style="border-width:0px;"> </asp:Hyperlink></li>
	<li class="items"><asp:LinkButton id="cmdClearPortalCache" runat="server" EnableViewState="false"  CausesValidation="False"><img  src="/images/restore.gif" alt="*" style="border-width:0px;"> </asp:LinkButton></li>
	</ul>
	</li>
	</ul>
	</div>
</asp:TableCell>
<asp:TableCell ID="cell2" Height="30" VerticalAlign="Bottom" HorizontalAlign="right" Wrap="false">
<asp:Hyperlink Visible="False" id="LanguageLink" runat="server" EnableViewState="false">
<asp:Image EnableViewState="false" ID="Image1" ImageURL="~/images/icon_language_16px.gif" BorderWidth="0" AlternateText="*" runat="server" /></asp:Hyperlink>
&nbsp;<mail:check id="check" runat="server" EnableViewState="false"></mail:check>
<asp:hyperlink id="hypUser" runat="server" EnableViewState="false" CssClass="SelectedTab">User</asp:hyperlink>
&nbsp;<asp:literal id="hypHelp" runat="server" EnableViewState="false">Help</asp:literal>
<asp:linkButton cssclass="OtherTabs" visible="false" id="cmdRegister"  CausesValidation="False" runat="server" ></asp:linkButton>
<asp:Label id="lblSeparator" cssclass="OtherTabs" runat="server" EnableViewState="false" >&nbsp;|&nbsp;</asp:Label>
<asp:linkButton cssclass="OtherTabs" visible="false"  CausesValidation="False" id="cmdLogin" runat="server" ></asp:linkButton>
&nbsp;<asp:linkButton cssclass="OtherTabs" visible="false" id="cmdLogOff" runat="server" ></asp:linkButton>
&nbsp;&nbsp;&nbsp;
<asp:DropDownList id="ddlLanguage" EnableViewState="true" visible="false" AutoPostBack="True" DataValueField="language" DataTextField="SettingValue" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>
</asp:TableCell>
</asp:TableRow>
</asp:Table>
</div>
</div>
<asp:PlaceHolder id="pnlmoduleinfo" EnableViewState="false" Runat="Server" Visible="False">
<div id="signin" style="z-index: 4">
	<table class="headertitle" width="100%" cellspacing="0" cellpadding="3" border="0">
    	<tbody>
	   	<tr>
	   	<td align="center" valign="middle">
	    <span class="ItemTitle"><%= DotNetZoom.GetLanguage("admin_add_module")%></span>
	   	</td>
	   	<td align="right" valign="middle"><a title="<%= DotNetZoom.GetLanguage("admin_menu_hide")%>" href="javascript:toggleBox('signin',0)" >
		<img height="14" width="14" border="0" src="/images/1x1.gif" Alt="ca" title="<%= DotNetZoom.GetLanguage("admin_menu_hide")%>" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -333px;"></a>
	   	</td>
	   	</tr>
		</tbody>
	</table>
<div class="ModuleInfo" style="overflow: auto; vertical-align: top; height: 400px;">
<asp:datagrid id="grdDefinitions" runat="server"  EnableViewState="false" ShowHeader="False" AutoGenerateColumns="false" cellspacing="1" CellPadding="1" gridlines="none" Border="0px">
    <Columns>
	     <asp:TemplateColumn ItemStyle-CssClass="OtherTabs" ItemStyle-Width="20">
            <ItemTemplate>
                <asp:HyperLink EnableViewState="false" style="CURSOR: help" NavigateUrl='<%# FormatModuleURL( "mdi", DataBinder.Eval(Container.DataItem,"ModuleDefID")) %>' Visible="true" runat="server" ID="Hyperlink1">
                    <asp:Image EnableViewState="false" height="12" width="12" onmouseover='<%# ReturnToolTip( DataBinder.Eval(Container.DataItem,"Description"))%>' style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -362px;" ImageURL="~/images/1x1.gif" AlternateText='<%# DotNetZoom.GetLanguage("module_info") %>'  tooltip='<%# DotNetZoom.GetLanguage("module_info") &  DataBinder.Eval(Container.DataItem,"FriendlyName") %>' Visible="true" runat="server" ID="Hyperlink1Image" />
                </asp:HyperLink>
            </ItemTemplate>
		 </asp:TemplateColumn>
		 <asp:TemplateColumn ItemStyle-CssClass="OtherTabs">
			<ItemTemplate>
			    <asp:HyperLink EnableViewState="false" tooltip='<%# DotNetZoom.GetLanguage("add") & " " &  DataBinder.Eval(Container.DataItem,"FriendlyName") %>' NavigateUrl='<%# FormatModuleURL( "mda", DataBinder.Eval(Container.DataItem,"ModuleDefID")) %>' Visible="true" runat="server" ID="Hyperlink2">
 				<%# DataBinder.Eval(Container.DataItem,"FriendlyName") %>
				</asp:HyperLink>
            </ItemTemplate>
		</asp:TemplateColumn>
    </Columns>
</asp:datagrid>
</div>
</div>
</asp:placeholder>
<asp:PlaceHolder EnableViewState="false" id="pnlmodulehelp" Runat="Server" Visible="False">
<div class="admin" id="admin" style="z-index: 4">
<table class="headertitle" width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
<tr>
<td align="right" width="350px">
<a title="<%= DotNetZoom.GetLanguage("admin_hide_info")%>" href="javascript:toggleBox('admin',0)" >
<img height="14" width="14" border="0" src="/images/1x1.gif" Alt="ca"  title="<%= DotNetZoom.GetLanguage("admin_hide_info")%>" style=" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -333px;"></a>
</td>
</tr>
<tr>
<td align="left" width="350px">
<asp:Label id="lblmodulehelp" CssClass="Normal" runat="server" EnableViewState="false" ></asp:Label>
</td>
</tr>
</table>
</div>
</asp:placeholder>
</div>
<asp:Literal ID="contextmenu" runat="server" Visible="false" EnableViewState="false"></asp:Literal>