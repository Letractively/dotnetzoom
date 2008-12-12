<%@ Control Language="vb" codebehind="EditIFrame.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditIFrame" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750" border="0">
    <tbody>
        <tr valign="top">
            <td class="SubHead" width="144">
                <label for="<%=txtSrc.ClientID%>"><%= DotNetZoom.GetLanguage("iframe_source")%></label></td>
            <td>
                <asp:TextBox id="txtSrc" runat="server" Columns="50" cssclass="NormalTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtWidth.ClientID%>"><%= DotNetZoom.GetLanguage("iframe_width")%>:</label></td>
            <td>
                <asp:TextBox id="txtWidth" runat="server" Columns="50" cssclass="NormalTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtHeight.ClientID%>"><%= DotNetZoom.GetLanguage("iframe_height")%>:</label></td>
            <td>
                <asp:TextBox id="txtHeight" runat="server" Columns="50" cssclass="NormalTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtTitle.ClientID%>"><%= DotNetZoom.GetLanguage("iframe_title")%>:</label></td>
            <td>
                <asp:TextBox id="txtTitle" runat="server" Columns="50" cssclass="NormalTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=cboScrolling.ClientID%>"><%= DotNetZoom.GetLanguage("iframe_scroll")%>:</label></td>
            <td>
                <asp:DropDownList id="cboScrolling" CssClass="NormalTextBox" Runat="server">
                        <asp:ListItem Value="auto"></asp:ListItem>
                        <asp:ListItem Value="no"></asp:ListItem>
                        <asp:ListItem Value="yes"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=cboBorder.ClientID%>"><%= DotNetZoom.GetLanguage("iframe_border")%>:</label></td>
            <td>
                <asp:DropDownList id="cboBorder" CssClass="NormalTextBox" Runat="server">
                        <asp:ListItem Value="no"></asp:ListItem>
                        <asp:ListItem Value="yes"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:LinkButton id="cmdUpdate" runat="server" cssclass="CommandButton"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton id="cmdCancel" runat="server" cssclass="CommandButton" CausesValidation="False"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>