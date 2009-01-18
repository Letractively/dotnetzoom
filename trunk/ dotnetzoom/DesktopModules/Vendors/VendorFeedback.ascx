<%@ Control Language="vb" codebehind="VendorFeedback.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.VendorFeedback" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:Label id="lblMessage" runat="server" cssclass="Normal"></asp:Label><asp:Label id="lblVendor" runat="server" cssclass="Head"></asp:Label>
<br>
<asp:placeholder id="pnlFeedback" Visible="False" Runat="server">
    <br>
    <table cellspacing="2" cellpadding="0" width="500">
        <tbody>
            <tr valign="top">
                <td class="SubHead" width="100">
                    <label for="<%=cboValue.ClientID%>">Experience:</label>&nbsp;</td>
                <td align="left" width="320">
                    <asp:DropDownList id="cboValue" Runat="server" CssClass="NormalTextBox">
                        <asp:ListItem Value="1"></asp:ListItem>
                        <asp:ListItem Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr valign="top">
                <td class="SubHead" width="100">
                    <label for="<%=txtComment.ClientID%>">Commentaires:</label>&nbsp;</td>
                <td align="left" width="320">
                    <asp:TextBox id="txtComment" runat="server" cssclass="NormalTextBox" columns="59" width="500" Rows="15" TextMode="Multiline"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
    <p align="left">
        <asp:LinkButton cssclass="CommandButton" id="cmdUpdate" runat="server" Text="Enregistrer" ></asp:LinkButton>
        &nbsp; 
        <asp:LinkButton cssclass="CommandButton" id="cmdCancel" runat="server" Text="Annuler"  CausesValidation="False"></asp:LinkButton>
        &nbsp; 
        <asp:linkbutton cssclass="CommandButton" id="cmdDelete" runat="server" Text="Supprimer"  CausesValidation="False"></asp:linkbutton>
    </p>
</asp:placeholder>
<br>
<asp:DataList id="lstFeedback" runat="server" RepeatColumns="1" Width="100%">
    <ItemTemplate>
        <table border="0" cellspacing="2" cellpadding="0" width="100%">
            <tr>
                <td valign="top" align="left" style="white-space: nowrap;">
                    <asp:Label id="lblValue" runat="server"></asp:Label> 
                </td>
                <td valign="top" align="left" style="white-space: nowrap;">
                    <asp:Label id="lblDate" runat="server" cssclass="Normal"></asp:Label> 
                </td>
                <td valign="top" align="left" style="white-space: nowrap;">
                    <asp:Label id="lblUser" runat="server" cssclass="Normal"></asp:Label> 
                </td>
                <td valign="top" width="100%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label id="lblComment" runat="server" cssclass="Normal"></asp:Label> 
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:DataList>
<p align="left">
    <asp:LinkButton cssclass="CommandButton" id="cmdFeedback" runat="server"  CausesValidation="False"></asp:LinkButton>
    &nbsp;&nbsp; 
    <asp:LinkButton cssclass="CommandButton" id="cmdBack" runat="server"  CausesValidation="False"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>