<%@ Control Language="vb" codebehind="EditDirectory.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditDirectory" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750" >
    <tbody>
        <tr valign="bottom">
            <td class="SubHead" valign="middle">
                <%= DotNetZoom.getlanguage("directory_from") %>:&nbsp; 
            </td>
            <td valign="top">
                <asp:RadioButtonList id="optSource" runat="server" CssClass="NormalTextBox" RepeatDirection="Horizontal">
                    <asp:ListItem Value="G">Host</asp:ListItem>
                    <asp:ListItem Value="L">Site</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr valign="bottom">
            <td class="SubHead">
                <label class="SubHead" for="<%=chkFeedback.ClientID%>"><%= DotNetZoom.getlanguage("label_see_comments") %></label>&nbsp; 
            </td>
            <td>
                <asp:CheckBox id="chkFeedback" CssClass="NormalTextBox" Runat="server"></asp:CheckBox>
            </td>
        </tr>
        <tr valign="bottom">
            <td class="SubHead">
                <label class="SubHead" for="<%=chkSignup.ClientID%>"><%= DotNetZoom.getlanguage("allow_vendors_register") %></label>&nbsp; 
            </td>
            <td>
                <asp:CheckBox id="chkSignup" CssClass="NormalTextBox" Runat="server"></asp:CheckBox>
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