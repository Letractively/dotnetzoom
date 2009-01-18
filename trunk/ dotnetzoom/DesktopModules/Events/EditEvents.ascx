<%@ Control Language="vb" codebehind="EditEvents.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditEvents" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<script language="javascript" type="text/javascript"  src="<%=dotnetzoom.glbpath + "controls/PopupCalendar.js"%>"></script>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<script language="JavaScript" type="text/javascript">
		function OpenNewWindow(tabid)
			{
				var m = window.open('<%=dotnetzoom.glbpath%>admin/tabs/icone.aspx?L=<%= DotNetZoom.GetLanguage("N") %>&tabid=' + tabid , 'icone', 'width=800,height=600,left=100,top=100');
				m.focus();
			}
		function SetUrl(idParentValue)
			{
			  
			  document.getElementById('edit_txticone').value = idParentValue;
			  __doPostBack('edit$cmdView','');
			}
</script>
<asp:placeholder id="pnlOptions" Runat="server" Visible="False">
    <table cellspacing="0" cellpadding="2" border="0">
        <tbody>
            <tr>
                <td class="SubHead" valign="middle"><%= DotNetZoom.GetLanguage("events_format")%>:</td>
                <td valign="bottom">
                    <asp:RadioButtonList id="optView" Runat="server" RepeatDirection="Horizontal" CssClass="NormalTextBox">
                        <asp:ListItem Value="L"></asp:ListItem>
                        <asp:ListItem Value="C"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr valign="top">
                <td class="SubHead">
                    <label for="<%=txtWidth.ClientID%>"><%= DotNetZoom.GetLanguage("calendar_cel_width")%>:</label></td>
                <td>
                    <asp:textbox id="txtWidth" runat="server" CssClass="NormalTextBox" Columns="5"></asp:textbox>
                </td>
            </tr>
            <tr valign="top">
                <td class="SubHead">
                    <label for="<%=txtHeight.ClientID%>"><%= DotNetZoom.GetLanguage("calendar_cel_height")%>:</label></td>
                <td>
                    <asp:textbox id="txtHeight" runat="server" CssClass="NormalTextBox" Columns="5"></asp:textbox>
                </td>
            </tr>
        </tbody>
    </table>
    <p align="left">
        <asp:LinkButton class="CommandButton" id="cmdUpdateOptions" runat="server" CausesValidation="False" ></asp:LinkButton>
        &nbsp;
        <asp:LinkButton id="cmdCancelOptions" runat="server" CssClass="CommandButton" CausesValidation="False" ></asp:LinkButton>
    </p>
</asp:placeholder>
<asp:placeholder id="pnlContent" Runat="server">
    <table cellspacing="0" cellpadding="0" width="750">
        <tbody>
            <tr valign="top">
                <td class="SubHead">
                    <label for="<%=txtTitle.ClientID%>"><%= DotNetZoom.GetLanguage("label_title")%>:</label></td>
                <td>
                    <asp:textbox id="txtTitle" runat="server" Columns="30" cssclass="NormalTextBox" width="390" maxlength="150"></asp:textbox>
                    <br>
                    <asp:requiredfieldvalidator id="valTitle" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtTitle"></asp:requiredfieldvalidator>
                </td>
            </tr>
            <tr valign="top">
                <td class="SubHead">
                    <label for="<%=txtDescription.ClientID%>"><%= DotNetZoom.GetLanguage("label_description")%>:</label></td>
                <td>
                    <asp:textbox id="txtDescription" runat="server" CssClass="NormalTextBox" Columns="44" width="390" TextMode="Multiline" Rows="6"></asp:textbox>
                    <br>
                    <asp:requiredfieldvalidator id="valDescription" runat="server" CssClass="NormalRed" Display="Dynamic"  ControlToValidate="txtDescription"></asp:requiredfieldvalidator>
                </td>
            </tr>
        <tr>
            <td class="SubHead" style="white-space: nowrap;">
            <label title="<%= DotNetZoom.GetLanguage("select_icone_tooltip")%>" for="<%=txtIcone.ClientID%>"><%= DotNetZoom.GetLanguage("label_icone")%>:</label> 
			</td>
            <td colspan="2">
	         <asp:textbox id="txticone" runat="server" cssclass="NormalTextBox" width="300" MaxLength="200"></asp:textbox>
             &nbsp; 
			<asp:hyperlink id="lnkicone" CssClass="CommandButton" runat="server" EnableViewState="false"></asp:hyperlink>
		   &nbsp;&nbsp;<asp:Image Id="MyHtmlImage" runat="server" Visible="false" EnableViewState="false"></asp:Image>
		   </td>
        </tr>
            <tr>
                <td class="SubHead">
                    <p>
                        <label for="<%=txtAlt.ClientID%>"><%= DotNetZoom.GetLanguage("label_alt")%>:</label>
                    </p>
                </td>
                <td>
                    <asp:TextBox id="txtAlt" runat="server" Columns="50" cssclass="NormalTextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator id="valAltText" runat="server" CssClass="NormalRed" Display="Dynamic"  ControlToValidate="txtAlt"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="SubHead">
                    <label for="<%=txtEvery.ClientID%>"><%= DotNetZoom.GetLanguage("events_interval")%>:</label></td>
                <td>
                    <asp:textbox id="txtEvery" runat="server" Columns="3" cssclass="NormalTextBox" maxlength="3"></asp:textbox>
                    &nbsp; <label style="DISPLAY: none" for="<%=cboPeriod.ClientID%>"><%= DotNetZoom.GetLanguage("label_period")%></label>
                    <asp:DropDownList id="cboPeriod" Runat="server" cssclass="NormalTextBox">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="D"></asp:ListItem>
                        <asp:ListItem Value="W"></asp:ListItem>
                        <asp:ListItem Value="M"></asp:ListItem>
                        <asp:ListItem Value="Y"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="Normal"></td>
            </tr>
            <tr>
                <td class="SubHead">
                    <label for="<%=txtStartDate.ClientID%>"><%= DotNetZoom.GetLanguage("start_date") %>:</label>
                </td>
                <td>
                    <asp:textbox id="txtStartDate" runat="server" Columns="20" cssclass="NormalTextBox" maxlength="20"></asp:textbox>
                    &nbsp;
                    <asp:HyperLink id="cmdStartCalendar" Runat="server" CssClass="CommandButton"></asp:HyperLink>
                    <asp:requiredfieldvalidator id="valStartDate" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtStartDate"></asp:requiredfieldvalidator>
                    <asp:CompareValidator id="valStartDate2" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtStartDate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="SubHead">
                    <label for="<%=txtTime.ClientID%>"><%= DotNetZoom.GetLanguage("start_hour") %>:</label>
                </td>
                <td>
                    <asp:textbox id="txtTime" runat="server" Columns="8" cssclass="NormalTextBox" maxlength="8"></asp:textbox>
                </td>
            </tr>
            <tr>
                <td class="SubHead">
                    <label for="<%=txtExpiryDate.ClientID%>"><%= DotNetZoom.GetLanguage("end_date") %>:</label>
                </td>
                <td>
                    <asp:textbox id="txtExpiryDate" runat="server" Columns="20" cssclass="NormalTextBox" MaxLength="20"></asp:textbox>
                    &nbsp;
                    <asp:HyperLink id="cmdExpiryCalendar" Runat="server" CssClass="CommandButton"></asp:HyperLink>
                    <asp:CompareValidator id="valExpiryDate" runat="server" CssClass="NormalRed" Display="Dynamic" ControlToValidate="txtExpiryDate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
        </tbody>
    </table>
    <p align="left">
        <asp:linkbutton class="CommandButton" id="cmdUpdate" runat="server" ></asp:linkbutton>
        &nbsp;
        <asp:linkbutton class="CommandButton" id="cmdCancel" runat="server" CausesValidation="False" ></asp:linkbutton>
        &nbsp;
        <asp:linkbutton class="CommandButton" id="cmdDelete" runat="server" CausesValidation="False" ></asp:linkbutton>
    </p>
    <hr width="500" noshade="noshade" size="1" />
    <asp:placeholder id="pnlAudit" Runat="server">
	    <span class="Normal"><%= DotNetZoom.GetLanguage("label_update_by") %>&nbsp;<asp:Label id="CreatedBy" runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("label_update_the") %>&nbsp;<asp:Label id="CreatedDate" runat="server"></asp:Label>
        </span>
    </asp:placeholder>
</asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>