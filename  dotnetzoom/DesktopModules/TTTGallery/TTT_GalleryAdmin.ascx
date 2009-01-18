<%@ Control language="vb" CodeBehind="TTT_GalleryAdmin.ascx.vb" AutoEventWireup="false" Inherits="DotNetZoom.TTT_GalleryAdmin" %>
<%@ Register TagPrefix="TTT" TagName="UsersControl" Src="../TTTForum/TTT_UsersControl.ascx"%>
<table class="TTTBorder" cellSpacing="1" cellPadding="0" width="780">
	<tbody>
		<tr>
			<td class="TTTHeader" align="left" colSpan="2" height="28"><span class="TTTHeaderText">&nbsp;<%= DotNetZoom.getlanguage("Gal_SetUp") %></span>
			</td>
		</tr>
		<tr>
			<td class="TTTAltHeader" align="left" colSpan="2" height="28">&nbsp;
				<asp:label id="lblInfo" Runat="server" ForeColor="red" CssClass="TTTNormal"></asp:label></td>
		</tr>
		<asp:placeholder id="pnlAdmin" Runat="server" Visible="False">
		<!--Panel admin settings-->
  <tr>
    <td class=TTTSubHeader width=200 height=28></td>
    <td class=TTTAltHeader align="left" height=28>&nbsp; 
<span class="TTTHeaderText"><%= DotNetZoom.getlanguage("Gal_SetUpAdmin") %></span></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ImgBaseUrl") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=RootURL cssclass="NormalTextBox" Enabled="false" width="100%" runat="server"></asp:textbox></td></tr>
  <tr>
    <td class=TTTSubHeader width=200><label title="<%= DotNetZoom.getlanguage("Gal_QuotaTip") %>">&nbsp;<%= DotNetZoom.getlanguage("Gal_Quota") %>:</label></td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=txtQuota cssclass="NormalTextBox" Enabled="false" width="75px" runat="server"></asp:textbox>&nbsp;
	 <asp:label id="lblQuota" Runat="server" CssClass="TTTNormal"></asp:label></td>
 
<asp:regularexpressionvalidator id=Regularexpressionvalidator9 CssClass="TTTNormal" runat="server" ControlToValidate="txtQuota" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_MaxFile") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=txtMaxFileSize cssclass="NormalTextBox" width="75px" runat="server"></asp:textbox><SPAN 
      class=TTTNormal><%= DotNetZoom.getlanguage("Gal_KB") %></SPAN> 
<asp:regularexpressionvalidator id=Regularexpressionvalidator8 CssClass="TTTNormal" runat="server" ControlToValidate="txtMaxFileSize" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ImgChangeSize") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:checkbox id=chkFixedSize CssClass="TTTNormal" runat="server" AutoPostBack="True"></asp:checkbox><SPAN 
      class=TTTNormal id=Span1 runat="server"><%= DotNetZoom.getlanguage("Gal_ImgRatioInfo") %></SPAN> 
  </td></tr>
<asp:placeholder id=pnlFixedSize Runat="server"><!--Start panel fixed size-->
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ImgQual") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=Quality cssclass="NormalTextBox" width="75px" runat="server"></asp:textbox><SPAN class=TTTNormal><%= DotNetZoom.getlanguage("Gal_ImgQualInfo") %></SPAN>
<asp:regularexpressionvalidator id=RegularExpressionValidator10 CssClass="TTTNormal" runat="server" ControlToValidate="Quality" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator></td></tr>


  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ImgMaxW") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=FixedWidth cssclass="NormalTextBox" width="75px" runat="server"></asp:textbox>
<asp:regularexpressionvalidator id=RegularExpressionValidator5 CssClass="TTTNormal" runat="server" ControlToValidate="FixedWidth" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ImgMaxH") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=FixedHeight cssclass="NormalTextBox" width="75px" runat="server"></asp:textbox>
<asp:regularexpressionvalidator id=RegularExpressionValidator6 CssClass="TTTNormal" runat="server" ControlToValidate="FixedHeight" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ImgKeepS") %></td>
    <td class=TTTRow align="left" height=25>
<asp:checkbox id=chkKeepSource CssClass="TTTNormal" runat="server"></asp:checkbox></td></tr></asp:placeholder><!-- End panel fixed size-->
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ImgKeepP") %></td>
    <td class=TTTRow align="left" height=25>
<asp:checkbox id=chkPrivate CssClass="TTTNormal" runat="server" AutoPostBack="True"></asp:checkbox></td></tr>
<asp:placeholder id=pnlPrivate Runat="server">
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_Prop") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=txtOwner cssclass="NormalTextBox" width="180px" runat="server"></asp:textbox>&nbsp; 
<asp:Button id=btnEditOwner runat="server" cssclass="button" CommandName="edit"></asp:Button>
<asp:textbox id=txtOwnerID Visible="False" cssclass="NormalTextBox" width="75px" runat="server"></asp:textbox></td></tr>
<asp:placeholder id=pnlSelectOwner Runat="server" Visible="False"><!--Start select owner-->
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_SelectProp") %>:</td>
    <td>
<TTT:USERSCONTROL id=ctlUsers runat="server" ShowEditButton="True" ShowEmail="True" ShowFullName="True" ShowUserName="True" Type="2"></TTT:USERSCONTROL></td></tr></asp:placeholder><!--End select owner--></asp:placeholder>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_CacheS") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:checkbox id=BuildCacheOnStart CssClass="TTTNormal" runat="server" Checked="True"></asp:checkbox></td></tr>
		</asp:placeholder>
		<!--End panel admin settings-->
		<asp:placeholder id="pnlUser" Runat="server">
		<!--End panel user settings-->
  <tr>
    <td class=TTTSubHeader width=200 height=28></td>
    <td class=TTTAltHeader align="left" height=28>&nbsp; 
<span Class="TTTHeaderText"><%= DotNetZoom.getlanguage("Gal_UserParam") %></span></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_Title") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=GalleryTitle cssclass="NormalTextBox" width="100%" runat="server"></asp:textbox></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_Desc") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=Description cssclass="NormalTextBox" width="100%" runat="server"></asp:textbox></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_TxtOp") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:dropdownlist id=ddlDisplayOption CssClass="NormalTextBox" runat="server" Width="180px"></asp:dropdownlist></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_IntF") %>&nbsp;</td>
    <td class=TTTRow align="left" height=25>
<asp:checkbox id=chkForumIntegrate CssClass="TTTNormal" runat="server" AutoPostBack="True"></asp:checkbox></td></tr>
<asp:placeholder id=pnlForumSelect Runat="server" Visible="False">
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_SelF") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:dropdownlist id=ddlForumGroup CssClass="NormalTextBox" runat="server" AutoPostBack="True" Width="180px" DataValueField="ForumGroupID" DataTextField="Name"></asp:dropdownlist></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_IntFF") %>:</td>
    <td class=TTTRow align="left">
      <table id=Table1 cellSpacing=0 cellPadding=1 border=0 runat="server">
        <tr>
          <td class=TTTRow valign=top align="left" width=180>
<asp:dropdownlist id=ddlForum CssClass="NormalTextBox" runat="server" Width="100%" DataValueField="ForumID" DataTextField="Name"></asp:dropdownlist></td>
          <td valign="middle" align="center" rowSpan=2>
<asp:ImageButton id=btnAdd runat="server" ImageURL="~/images/rt.gif" CommandName="add" BorderWidth="0" BorderStyle="none"></asp:ImageButton><br><br>
<asp:ImageButton id=btnRemove runat="server" ImageURL="~/images/lt.gif" CommandName="remove" BorderWidth="0" BorderStyle="none"></asp:ImageButton></td>
          <td class=TTTRow valign=top align="left" width=180 rowSpan=2>&nbsp; 
<asp:ListBox id=lstIntegrate CssClass="NormalTextBox" runat="server" Width="100%" DataValueField="ForumID" DataTextField="Integration" Rows="3"></asp:ListBox></td></tr>
        <tr>
          <td class=TTTRow align="left" width=180>
<asp:dropdownlist id=ddlAlbums CssClass="NormalTextBox" runat="server" Width="100%"></asp:dropdownlist></td></tr></table></td></tr></asp:placeholder>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ThumbsW") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=StripWidth cssclass="NormalTextBox" width="75px" runat="server" Columns="26"></asp:textbox>
<asp:regularexpressionvalidator id=RegularExpressionValidator1 CssClass="TTTNormal" runat="server" ControlToValidate="StripWidth" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ThumbsH") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=StripHeight cssclass="NormalTextBox" width="75px" runat="server" Columns="26"></asp:textbox>
<asp:regularexpressionvalidator id=RegularExpressionValidator2 CssClass="TTTNormal" runat="server"  ControlToValidate="StripHeight" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_MThumbsW") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=MaxThumbWidth cssclass="NormalTextBox" width="75px" runat="server" Columns="26"></asp:textbox>
<asp:regularexpressionvalidator id=RegularExpressionValidator3 CssClass="TTTNormal" runat="server" ControlToValidate="MaxThumbWidth" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_MThumbsH") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=MaxThumbHeight cssclass="NormalTextBox" width="75px" runat="server" Columns="26"></asp:textbox>
<asp:regularexpressionvalidator id=RegularExpressionValidator4 CssClass="TTTNormal" runat="server" ControlToValidate="MaxThumbHeight" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_CatV") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=CategoryValues cssclass="NormalTextBox" width="100%" runat="server" Columns="26"></asp:textbox></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ExtVF") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=FileExtensions cssclass="NormalTextBox" width="100%" runat="server" Columns="26"></asp:textbox></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_ExtM") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=MovieExtensions cssclass="NormalTextBox" width="100%" runat="server" Columns="26"></asp:textbox></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_DiaS") %>:</td>
    <td class=TTTRow align="left" height=25>
<asp:textbox id=SlideshowSpeed cssclass="NormalTextBox" width="75px" runat="server" Columns="26"></asp:textbox>
<asp:regularexpressionvalidator id=RegularExpressionValidator7 CssClass="TTTNormal" runat="server" ControlToValidate="SlideShowSpeed" ValidationExpression="[0-9]{1,}"></asp:regularexpressionvalidator></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_PopDia") %></td>
    <td class=TTTRow align="left" height=25>
<asp:checkbox id=chkPopup CssClass="TTTNormal" runat="server"></asp:checkbox></td></tr>
  <tr>
    <td class=TTTSubHeader width=200>&nbsp;<%= DotNetZoom.getlanguage("Gal_InfoBule") %></td>
    <td class=TTTRow align="left" height=25>
<asp:checkbox id=Chkinfobule CssClass="TTTNormal" runat="server"></asp:checkbox></td></tr>

		</asp:placeholder> 
		<!--End panel user settings-->
		<tr>
			<td class="TTTSubHeader" width="200">&nbsp;<%= DotNetZoom.getlanguage("Gal_AllowD") %></td>
			<td class="TTTRow" align="left" height="25">
				<asp:checkbox id="chkDownload" CssClass="TTTNormal" runat="server"></asp:checkbox></td>
		</tr>
		<tr>
			<td class="TTTSubHeader" width="200">&nbsp;<%= DotNetZoom.getlanguage("Gal_AgAL") %></td>
			<td class="TTTRow" align="left" height="25">
				<asp:checkbox id="chkAvatarsGallery" CssClass="TTTNormal" runat="server"></asp:checkbox></td>
		</tr>
		<tr>
			<td class="TTTSubHeader" width="200"></td>
			<td class="TTTAltHeader" align="left">&nbsp;
				<asp:button id="btnCancel" CssClass="button" runat="server" CommandName="cancel"></asp:button>&nbsp;
				<asp:button id="btnUpdate" CssClass="button" runat="server" CommandName="update"></asp:button>&nbsp;
			</td>
		</tr>
	</tbody></table>