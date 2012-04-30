<%@ Control Language="vb" CodeBehind="EditLinks.ascx.vb" AutoEventWireup="false"
    Explicit="True" Inherits="DotNetZoom.EditLinks" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<Portal:Title ID="Title1" runat="server"></Portal:Title>
<asp:Literal ID="before" runat="server" EnableViewState="false"></asp:Literal>
<asp:PlaceHolder ID="pnlOptions" runat="server" Visible="False">
    <table cellspacing="0" cellpadding="2" border="0">
        <tbody>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=optControl.ClientID%>">
                        <%= DotNetZoom.getlanguage("links_label_list_type") %>:</label>
                </td>
                <td valign="bottom">
                    <asp:RadioButtonList ID="optControl" runat="server" CssClass="NormalTextBox" RepeatDirection="Horizontal">
                        <asp:ListItem Value="L"></asp:ListItem>
                        <asp:ListItem Value="D"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=optView.ClientID%>">
                        <%= DotNetZoom.getlanguage("links_label_list_type") %>:</label>
                </td>
                <td valign="bottom">
                    <asp:RadioButtonList ID="optView" runat="server" CssClass="NormalTextBox" RepeatDirection="Horizontal">
                        <asp:ListItem Value="V"></asp:ListItem>
                        <asp:ListItem Value="H"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=optInfo.ClientID%>">
                        <%= DotNetZoom.getlanguage("links_label_see_info") %>:</label>
                </td>
                <td valign="bottom">
                    <asp:RadioButtonList ID="optInfo" runat="server" CssClass="NormalTextBox" RepeatDirection="Horizontal">
                        <asp:ListItem Value="Y"></asp:ListItem>
                        <asp:ListItem Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="middle">
                    <label for="<%=txtCSS.ClientID%>">
                        <%= DotNetZoom.getlanguage("links_label_CSSClass") %>:</label>
                </td>
                <td valign="bottom">
                    <asp:TextBox ID="txtCSS" runat="server" CssClass="NormalTextBox" Width="200px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
    <p align="left">
        <asp:LinkButton class="CommandButton" ID="cmdUpdateOptions" runat="server" CausesValidation="False"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="cmdCancelOptions" runat="server" CssClass="CommandButton" CausesValidation="False"></asp:LinkButton>
    </p>
</asp:PlaceHolder>
<asp:PlaceHolder ID="pnlContent" runat="server">
    <table cellspacing="0" cellpadding="0" width="750" border="0">
        <tbody>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label for="<%=txtTitle.ClientID%>">
                        <%= DotNetZoom.getlanguage("links_title") %>:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="NormalTextBox" Width="300" Columns="30"
                        MaxLength="150"></asp:TextBox>
                    <br>
                    <asp:RequiredFieldValidator ID="valTitle" runat="server" CssClass="NormalRed" Display="Static"
                        ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label style="display: none" for="<%=optExternal.ClientID%>">
                        <%= DotNetZoom.getlanguage("select_external_link") %></label>
                    <asp:RadioButton ID="optExternal" runat="server" GroupName="LinkType" AutoPostBack="True">
                    </asp:RadioButton>
                    &nbsp;<%= DotNetZoom.getlanguage("external_link") %><label for="<%=txtExternal.ClientID%>">:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtExternal" runat="server" CssClass="NormalTextBox" Width="300"
                        Columns="30" MaxLength="150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label style="display: none" for="<%=optInternal.ClientID%>">
                        <%= DotNetZoom.getlanguage("select_internal_link") %></label>
                    <asp:RadioButton ID="optInternal" runat="server" GroupName="LinkType" AutoPostBack="True">
                    </asp:RadioButton>
                    &nbsp;<%= DotNetZoom.getlanguage("internal_link") %><label for="<%=cboInternal.ClientID%>">:</label>
                </td>
                <td>
                    <asp:DropDownList ID="cboInternal" runat="server" CssClass="NormalTextBox" Width="300"
                        DataValueField="TabId" DataTextField="TabName">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label style="display: none" for="<%=optFile.ClientID%>">
                        <%= DotNetZoom.getlanguage("select_file_link") %></label>
                    <asp:RadioButton ID="optFile" runat="server" GroupName="LinkType" AutoPostBack="True">
                    </asp:RadioButton>
                    &nbsp;<%= DotNetZoom.getlanguage("file_link") %><label for="<%=cboFiles.ClientID%>">:</label>
                </td>
                <td>
                    <asp:DropDownList ID="cboFiles" runat="server" CssClass="NormalTextBox" Width="200"
                        DataValueField="Value" DataTextField="Text">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:HyperLink ID="cmdUpload" runat="server" CssClass="CommandButton"></asp:HyperLink>
                    <asp:ImageButton ID="UploadReturn" runat="server" EnableViewState="false" Visible="False"
                        Height="16" Width="16" ImageUrl="~/images/save.gif" Style="border-width: 0px">
                    </asp:ImageButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label for="<%=txtDescription.ClientID%>">
                        <%= DotNetZoom.getlanguage("link_description") %>:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="NormalTextBox" Width="300"
                        Columns="30" MaxLength="2000" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
                        <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label for="<%=txtViewOrder.ClientID%>">
                        <%= DotNetZoom.getlanguage("link_vue_order") %>:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtViewOrder" runat="server" CssClass="NormalTextBox" Width="300"
                        Columns="30" MaxLength="3"></asp:TextBox>
                </td>
            </tr>
                        <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="SubHead" valign="top">
                    <label class="SubHead" for="<%=chkNewWindow.ClientID%>">
                        <%= DotNetZoom.getlanguage("link_new_window") %></label>
                </td>
                <td>
                    <asp:CheckBox ID="chkNewWindow" runat="server" CssClass="NormalTextBox"></asp:CheckBox>
                </td>
            </tr>
        </tbody>
    </table>
    <p align="left">
        <asp:LinkButton ID="cmdUpdate" runat="server" CssClass="CommandButton"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="cmdCancel" runat="server" CssClass="CommandButton" CausesValidation="False"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="cmdDelete" runat="server" CssClass="CommandButton" CausesValidation="False"></asp:LinkButton>
    </p>
    <hr width="500" noshade="noshade" size="1" />
    <asp:PlaceHolder ID="pnlAudit" runat="server"><span class="Normal">
        <%= DotNetZoom.GetLanguage("label_update_by") %>&nbsp;<asp:Label ID="lblCreatedBy"
            runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("label_update_the") %>&nbsp;<asp:Label
                ID="lblCreatedDate" runat="server"></asp:Label></span> </asp:PlaceHolder>
    <br>
    <table cellspacing="0" cellpadding="2" border="0">
        <tbody>
            <tr valign="top">
                <td class="SubHead">
                    <%= DotNetZoom.GetLanguage("label_clicks") %>:
                </td>
                <td>
                    <asp:Label ID="lblClicks" runat="server" CssClass="Normal"></asp:Label>
                </td>
            </tr>
            <tr valign="top">
                <td class="SubHead">
                    <label class="SubHead" for="<%=chkLog.ClientID%>">
                        <%= DotNetZoom.getlanguage("label_see_stat") %></label>
                </td>
                <td>
                    <asp:CheckBox ID="chkLog" runat="server" CssClass="NormalTextBox" AutoPostBack="True">
                    </asp:CheckBox>
                </td>
            </tr>
        </tbody>
    </table>
    <asp:DataGrid ID="grdLog" runat="server" Width="100%" GridLines="none" BorderStyle="None"
        CellSpacing="3" AutoGenerateColumns="false" EnableViewState="false">
        <Columns>
            <asp:BoundColumn DataField="DateTime" ItemStyle-CssClass="Normal" HeaderStyle-CssClass="NormalBold" />
            <asp:BoundColumn DataField="FullName" ItemStyle-CssClass="Normal" HeaderStyle-CssClass="NormalBold" />
        </Columns>
    </asp:DataGrid>
</asp:PlaceHolder>
<asp:Literal ID="after" runat="server" EnableViewState="false"></asp:Literal>