<%@ Control Language="vb" codebehind="EditRss.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditRss" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750" border="0">
 <tbody>
  <tr valign="top">
   <td Colspan="4" class="Head"><%= DotNetZoom.getlanguage("rss_news_source") %>:
   </td>
  </tr>
  <tr>
  <td>&nbsp;
  </td>
  </tr>	
  <tr>
	<td>
       <label class="SubHead" for= "<%=optXMLInternal.ClientID%>"><%= DotNetZoom.getlanguage("rss_internal") %> </label><label class="SubHead" for="<%=cboXML.ClientID%>">:</label>
	</td>
	<td> 
       <asp:RadioButton id="optXMLInternal" AutoPostBack="True" GroupName="XMLType" Runat="server"></asp:RadioButton>
	</td>
	<td> 
       <asp:dropdownlist id="cboXML" runat="server" DataTextField="Text" DataValueField="Value" Width="390" CssClass="NormalTextBox"></asp:dropdownlist>
	</td>
	<td> 
       <asp:HyperLink id="cmdUpload1" Runat="server" CssClass="CommandButton"></asp:HyperLink>
    </td>
  </tr>
  <tr valign="top">
    <td>
      <label class="SubHead" for= "<%=optXMLExternal.ClientID%>"><%= DotNetZoom.getlanguage("rss_external") %> </label><label class="SubHead" for="<%=txtXML.ClientID%>">:</label>
	 </td>
	<td> 
       <asp:RadioButton id="optXMLExternal" AutoPostBack="True" GroupName="XMLType" Runat="server"></asp:RadioButton>
     	</td>
	<td> 
      <asp:TextBox id="txtXML" runat="server" CssClass="NormalTextBox" maxlength="150" Columns="30" width="390"></asp:TextBox>
   </td>
 </tr>
   <tr>
  <td Colspan="4">&nbsp;<hr>
  </td>
  </tr>	
 <tr valign="top">
   <td Colspan="4" class="Head"><%= DotNetZoom.getlanguage("rss_CSS_source") %>: 
   </td>
 </tr>
    <tr>
  <td>&nbsp;
  </td>
  </tr>	
 <tr>
	<td>
   <label class="SubHead" for= "<%=optXSLInternal.ClientID%>"><%= DotNetZoom.getlanguage("rss_css_internal") %> </label><label class="SubHead" for="<%=cboXSL.ClientID%>">:</label>
	</td>
	<td> 
    <asp:RadioButton id="optXSLInternal" AutoPostBack="True" GroupName="XSLType" Runat="server"></asp:RadioButton>
	</td>
	<td> 
    <asp:dropdownlist id="cboXSL" runat="server" DataTextField="Text" DataValueField="Value" Width="390" CssClass="NormalTextBox"></asp:dropdownlist>
	</td>
	<td> 
    <asp:HyperLink id="cmdUpload2" Runat="server" CssClass="CommandButton"></asp:HyperLink>
    </td>
 </tr>
 <tr valign="top">
    <td>
    <label class="SubHead" for= "<%=optXSLExternal.ClientID%>"><%= DotNetZoom.getlanguage("rss_css_external") %> :</label>
  	</td>
	<td> 
    <asp:RadioButton id="optXSLExternal" AutoPostBack="True" GroupName="XSLType" Runat="server"></asp:RadioButton>
  	</td>
	<td> 
    <asp:TextBox id="txtXSL" runat="server" CssClass="NormalTextBox" maxlength="150" Columns="30" width="390"></asp:TextBox>
    </td>
 </tr>
    <tr>
  <td Colspan="4">&nbsp;<hr>
  </td>
  </tr>	
 <tr valign="top">
     <td Colspan="4" class="Head"><%= DotNetZoom.getlanguage("rss_security") %>
     </td>
  </tr>
     <tr>
  <td>&nbsp;
  </td>
  </tr>	
  <tr>
  <td>&nbsp;
  </td>	
  <td>&nbsp;
  </td>	
    <td>
      <asp:Label id="lblSecurity" runat="server" cssclass="Subhead" name="lblSecurity">
	  <%= DotNetZoom.getlanguage("rss_security_account") %></asp:Label>&nbsp; 
   </td>
  </tr>
  <tr>
  <td>&nbsp;
  </td>	
   <td>
	<span class="Subhead"><%= DotNetZoom.getlanguage("rss_account_info") %>:</span>
	</td>
	<td> 
    <asp:TextBox id="txtAccount" Runat="server" CssClass="NormalTextBox" TextMode="SingleLine" Enabled="False"></asp:TextBox>
    </td>
  </tr>
	<tr>
  <td>&nbsp;
  </td>	

	 <td>	
		<span class="Subhead"><%= DotNetZoom.getlanguage("rss_account_password") %>:</span>
	</td>
	<td> 
	<asp:TextBox id="txtPassword" Runat="server" CssClass="NormalTextBox" TextMode="Password" Enabled="False"></asp:TextBox>
    </td>
    </tr>
	   <tr>
  <td>&nbsp;
  </td>
  </tr>	
  <tr valign="top">
    <td Colspan="4" class="Normal">
        <br>
        <br>
    	<%= DotNetZoom.getlanguage("rss_general_info") %>
    </td>
   </tr>
 </tbody>
</table>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" runat="server" ></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdCancel" runat="server"  CausesValidation="False"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>