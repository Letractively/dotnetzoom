<%@ Control Language="vb" AutoEventWireup="false" Codebehind="UserInfoModule.ascx.vb" Inherits="DotNetZoom.UserInfoModule" TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx"%>
<portal:title id="Title1" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<img height=1 src="images/1x1.gif"  alt="*" width=750><br>
<table cellspacing=0 cellpadding=4 width="100%">
  <tr class=Normal>
    <td>
      <table cellspacing=0 cellpadding=0 width="100%">
        <tr>
          <td valign=top align=center width=150><img 
            src="Images/uoProfile.jpg"> <asp:placeholder id=pnlProfileOptions Runat="server">
            <table cellspacing=0 cellpadding=2 width="100%">
              <tr id=rowEditProfile runat="server">
                <td class=Normal colspan=2>[ <a 
                  href="<%= GetEditProfileLink() %>"><%= DotNetZoom.GetLanguage("UO_ModInfo")%></a> ] 
              </td></tr><asp:placeholder id=pnlOtherUser Runat="server">
              <tr>
                <td class=Normal colspan=2>[ <asp:LinkButton id=lnkAddRemoveBuddiesList runat="server"></asp:LinkButton>&nbsp;]</td></tr>
              <tr>
                <td class=Normal colspan=2>[ <asp:linkbutton id=lnkSendPrivateMessage runat="server"></asp:linkbutton>&nbsp;]</td></tr></asp:placeholder></table></asp:placeholder></td>
          <td valign=top align=left>
            <table cellspacing=0 cellpadding=2 width="100%">
              <tr>
                <td class=NormalBold width=100><%= DotNetZoom.GetLanguage("UO_UserName")%>:</td>
                <td class=Normal><asp:label id=lblUsername Runat="server"></asp:label></td></tr>
              <tr>
                <td class=NormalBold width=100><%= DotNetZoom.GetLanguage("UO_Name")%>:</td>
                <td class=Normal><asp:label id=lblFullName Runat="server"></asp:label></td></tr>
              <tr>
                <td class=NormalBold width=100><%= DotNetZoom.GetLanguage("UO_Since")%>:</td>
                <td class=Normal><asp:label id=lblJoined Runat="server"></asp:label></td></tr>
              <tr>
                <td class=NormalBold width=100><%= DotNetZoom.GetLanguage("UO_LastOn")%>:</td>
                <td class=Normal><asp:label id=lblLastVisited Runat="server"></asp:label></td></tr>
              <tr>
                <td class=NormalBold width=100><%= DotNetZoom.GetLanguage("UO_OnLineNow")%>:</td>
                <td class=Normal><asp:label id=lblOnlineStatus Runat="server" Font-Bold="true"></asp:label></td></tr>
              <tr>
                <td class=NormalBold width=100><%= DotNetZoom.GetLanguage("UO_Body")%>:</td>
                <td class=Normal><asp:label id=lblBuddyStatus Runat="server" Font-Bold="true"></asp:label></td></tr></table>
            <hr>

            <table cellspacing=0 cellpadding=2 width="100%">
              <tr>
                <td align=center width=150><img src="Images/uoBuddies.jpg"></td>
                <td valign=top><span class=NormalBold><asp:Label id=lblBuddyList Runat="server"></asp:Label></span><asp:DataList id=lstBuddies Runat="server" cssclass="normal" Height="100%" RepeatColumns="2" ItemStyle-Width="125">
												<itemtemplate>
													[
													<asp:HyperLink ID="lnkUsername" Runat="server" NavigateUrl='<%# GetUserInfoLink(DataBinder.Eval(Container.DataItem, "BuddyID")) %>' Tooltip='<%# GetUserInfoTooltip(DataBinder.Eval(Container.DataItem, "Username")) %>'>
														<%# DataBinder.Eval(Container.DataItem, "UserName") %>
													</asp:HyperLink>
													]
												</ItemTemplate>
											</asp:DataList>
			<asp:Label id=lblNoBuddiesFound Runat="server" CssClass="Normal" Visible="False"> <%= DotNetZoom.GetLanguage("UO_NoBody")%></asp:Label></td></tr>
              <tr></tr></table></td></tr></table>
      <hr>
      [ <a href="javascript:history.go(-1)"><%= DotNetZoom.GetLanguage("return")%></a>&nbsp;] 
    </td></tr></table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
	</asp:PlaceHolder>