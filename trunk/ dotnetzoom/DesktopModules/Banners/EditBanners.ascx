<%@ Control Language="vb" codebehind="EditBanners.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditBanners" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="2" cellpadding="0" width="750">
    <tbody>
        <tr valign="bottom">
            <td class="SubHead">
                <%= DotNetZoom.getlanguage("label_banner_source") %>&nbsp;:&nbsp; 
            </td>
            <td>
                <asp:RadioButtonList id="optSource" runat="server" RepeatDirection="Horizontal" CssClass="NormalBold">
                    <asp:ListItem Value="G">Host</asp:ListItem>
                    <asp:ListItem Value="L">Site</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr valign="bottom">
            <td class="SubHead">
                <label for="<%=cboType.ClientID%>"><%= DotNetZoom.getlanguage("label_banner_type") %>:</label>&nbsp; 
            </td>
            <td>
                <asp:DropDownList id="cboType" CssClass="NormalTextBox" DataValueField="BannerTypeId" DataTextField="BannerTypeName" Width="250px" Runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr valign="bottom">
            <td class="SubHead">
                <label for="<%=txtCount.ClientID%>"><%= DotNetZoom.getlanguage("label_banner_order") %>:</label>&nbsp; 
            </td>
            <td>
                <asp:TextBox id="txtCount" CssClass="NormalTextBox" Width="250px" Runat="server" Columns="30"></asp:TextBox>
                <asp:RegularExpressionValidator id="valCount" runat="server" Display="Dynamic" ValidationExpression="^[1-9]+[0]*$" ControlToValidate="txtCount"></asp:RegularExpressionValidator>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" runat="server"  ></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdCancel" runat="server"   CausesValidation="False"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>