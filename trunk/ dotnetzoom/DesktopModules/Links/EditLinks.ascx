<%@ Control Language="vb" codebehind="EditLinks.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditLinks" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:placeholder id="pnlOptions" Runat="server" Visible="False">
    <table cellspacing="0" cellpadding="2"  border="0">
        <tbody>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=optControl.ClientID%>"><%= DotNetZoom.getlanguage("links_label_list_type") %>:</label>
                </td>
                <td valign="bottom">
                    <asp:RadioButtonList id="optControl" Runat="server" CssClass="NormalTextBox" RepeatDirection="Horizontal">
                        <asp:ListItem Value="L"></asp:ListItem>
                        <asp:ListItem Value="D"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=optView.ClientID%>"><%= DotNetZoom.getlanguage("links_label_list_type") %>:</label>
                </td>
                <td valign="bottom">
                    <asp:RadioButtonList id="optView" Runat="server" CssClass="NormalTextBox" RepeatDirection="Horizontal">
                        <asp:ListItem Value="V"></asp:ListItem>
                        <asp:ListItem Value="H"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=optInfo.ClientID%>"><%= DotNetZoom.getlanguage("links_label_see_info") %>:</label>
                </td>
                <td valign="bottom">
                    <asp:RadioButtonList id="optInfo" Runat="server" CssClass="NormalTextBox" RepeatDirection="Horizontal">
                        <asp:ListItem Value="Y"></asp:ListItem>
                        <asp:ListItem Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=txtCSS.ClientID%>"><%= DotNetZoom.getlanguage("links_label_CSSClass") %>:</label>
                </td>
                <td valign="bottom">
     			<asp:textbox id="txtCSS" runat="server" cssclass="NormalTextBox" Width="200px" MaxLength="50"></asp:textbox>
                </td>
            </tr>			
		
        </tbody>
    </table>
    <p align="left">
        <asp:LinkButton class="CommandButton" id="cmdUpdateOptions" runat="server"  CausesValidation="False"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton id="cmdCancelOptions" runat="server" CssClass="CommandButton"  CausesValidation="False"></asp:LinkButton>
    </p>
</asp:placeholder>
<asp:placeholder id="pnlContent" Runat="server">
    <table cellspacing="0" cellpadding="0" width="750"  border="0">
        <tbody>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label for="<%=txtTitle.ClientID%>"><%= DotNetZoom.getlanguage("links_title") %>:</label>
                </td>
                <td>
                    <asp:TextBox id="txtTitle" runat="server" CssClass="NormalTextBox" width="300" Columns="30" maxlength="150"></asp:TextBox>
                    <br>
                    <asp:RequiredFieldValidator id="valTitle" runat="server" CssClass="NormalRed" Display="Static" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label style="DISPLAY: none" for="<%=optExternal.ClientID%>"><%= DotNetZoom.getlanguage("select_external_link") %></label>
                    <asp:RadioButton id="optExternal" Runat="server" GroupName="LinkType" AutoPostBack="True"></asp:RadioButton>
                    &nbsp;<%= DotNetZoom.getlanguage("external_link") %><label for="<%=txtExternal.ClientID%>">:</label>
                </td>
                <td>
                    <asp:TextBox id="txtExternal" runat="server" CssClass="NormalTextBox" width="300" Columns="30" maxlength="150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label style="DISPLAY: none" for="<%=optInternal.ClientID%>"><%= DotNetZoom.getlanguage("select_internal_link") %></label>
                    <asp:RadioButton id="optInternal" Runat="server" GroupName="LinkType" AutoPostBack="True"></asp:RadioButton>
                    &nbsp;<%= DotNetZoom.getlanguage("internal_link") %><label for="<%=cboInternal.ClientID%>">:</label>
                </td>
                <td>
                    <asp:DropDownList id="cboInternal" Runat="server" CssClass="NormalTextBox" width="300" DataValueField="TabId" DataTextField="TabName"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label style="DISPLAY: none" for="<%=optFile.ClientID%>"><%= DotNetZoom.getlanguage("select_file_link") %></label>
                    <asp:RadioButton id="optFile" Runat="server" GroupName="LinkType" AutoPostBack="True"></asp:RadioButton>
                    &nbsp;<%= DotNetZoom.getlanguage("file_link") %><label for="<%=cboFiles.ClientID%>">:</label>
                </td>
                <td>
                    <asp:DropDownList id="cboFiles" Runat="server" CssClass="NormalTextBox" width="200" DataValueField="Value" DataTextField="Text"></asp:DropDownList>
                    &nbsp;
                    <asp:HyperLink id="cmdUpload" Runat="server" CssClass="CommandButton"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label for="<%=txtDescription.ClientID%>"><%= DotNetZoom.getlanguage("link_description") %>:</label>
                </td>
                <td>
                    <asp:TextBox id="txtDescription" runat="server" CssClass="NormalTextBox" width="300" Columns="30" maxlength="2000" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label for="<%=txtViewOrder.ClientID%>"><%= DotNetZoom.getlanguage("link_vue_order") %>:</label>
                </td>
                <td>
                    <asp:TextBox id="txtViewOrder" runat="server" CssClass="NormalTextBox" width="300" Columns="30" maxlength="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label class="SubHead" for="<%=chkNewWindow.ClientID%>"><%= DotNetZoom.getlanguage("link_new_window") %></label>
                </td>
                <td>
                    <asp:CheckBox id="chkNewWindow" Runat="server" CssClass="NormalTextBox"></asp:CheckBox>
                </td>
            </tr>
        </tbody>
    </table>
    <p align="left">
        <asp:LinkButton id="cmdUpdate" runat="server" CssClass="CommandButton" ></asp:LinkButton>
        &nbsp;
        <asp:LinkButton id="cmdCancel" runat="server" CssClass="CommandButton"  CausesValidation="False"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton id="cmdDelete" runat="server" CssClass="CommandButton"  CausesValidation="False"></asp:LinkButton>
    </p>
    <hr width="500" noshade="noshade" size="1" />
    <asp:placeholder id="pnlAudit" Runat="server">
    <span class="Normal"><%= DotNetZoom.GetLanguage("label_update_by") %>&nbsp;<asp:Label id="lblCreatedBy" runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("label_update_the") %>&nbsp;<asp:Label id="lblCreatedDate" runat="server"></asp:Label></span>
    </asp:placeholder>
    <br>
    <table cellspacing="0" cellpadding="2"  border="0">
        <tbody>
            <tr valign="top">
                <td class="SubHead">
                    <%= DotNetZoom.GetLanguage("label_clicks") %>:</td>
                <td>
                    <asp:Label id="lblClicks" runat="server" cssclass="Normal"></asp:Label></td>
            </tr>
            <tr valign="top">
                <td class="SubHead">
                    <label class="SubHead" for="<%=chkLog.ClientID%>"><%= DotNetZoom.getlanguage("label_see_stat") %></label></td>
                <td>
                    <asp:Checkbox id="chkLog" runat="server" CssClass="NormalTextBox" AutoPostBack="True"></asp:Checkbox>
                </td>
            </tr>
        </tbody>
    </table>
    <asp:datagrid id="grdLog" runat="server" Width="100%"  gridlines="none" BorderStyle="None" CellSpacing="3" AutoGenerateColumns="false" EnableViewState="false" >
        <Columns>
            <asp:BoundColumn DataField="DateTime" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
            <asp:BoundColumn DataField="FullName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        </Columns>
    </asp:datagrid>
</asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>