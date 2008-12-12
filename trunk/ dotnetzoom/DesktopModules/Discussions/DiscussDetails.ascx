<%@ Control Language="vb" codebehind="DiscussDetails.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.DiscussDetails" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:placeholder id="pnlOptions" Runat="server" Visible="False">
Panel Options
</asp:placeholder>
<asp:placeholder id="pnlContent" Runat="server">
<asp:placeholder id="EditPanel" runat="server" Visible="false">
    <table cellspacing="0" cellpadding="4" width="750" border="0">
        <tbody>
            <tr valign="top">
                <td class="SubHead">
                    <label for="<%=TitleField.ClientID%>"><%= DotNetZoom.getlanguage("label_title") %>:</label> 
                </td>
                <td rowspan="4">
                    &nbsp; 
                </td>
                <td width=*>
                    <asp:TextBox id="TitleField" runat="server" maxlength="100" columns="40" width="500" cssclass="NormalTextBox"></asp:TextBox>
                    <br>
                    <asp:RequiredFieldValidator id="valTitleField" Runat="server" CssClass="NormalRed" ControlToValidate="TitleField" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr valign="top">
                <td class="SubHead">
                    <label for="<%=BodyField.ClientID%>"><%= DotNetZoom.getlanguage("label_message_body") %>:</label> 
                </td>
                <td width=*>
                    <asp:TextBox id="BodyField" runat="server" columns="59" width="500" cssclass="NormalTextBox" Rows="15" TextMode="Multiline"></asp:TextBox>
                    <br>
                    <asp:RequiredFieldValidator id="valBodyField" Runat="server" CssClass="NormalRed" ControlToValidate="BodyField" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr valign="top">
                <td>
                    &nbsp; 
                </td>
                <td>
                    <asp:LinkButton class="CommandButton" id="cmdUpdate" runat="server" ></asp:LinkButton>
                    &nbsp; 
                    <asp:LinkButton class="CommandButton" id="cmdCancel" runat="server" CausesValidation="False"></asp:LinkButton>
                </td>
            </tr>
        </tbody>
    </table>
</asp:placeholder>
<table id="tblOriginalMessage" cellspacing="0" cellpadding="4" width="750"  border="0" runat="server">
    <tbody>
        <tr id="rowOriginalMessage" valign="top" runat="server" visible="false">
            <td>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr valign="top">
            <td align="left">
                <table cellspacing="0" cellpadding="4" width="600" border="0">
                    <tbody>
                        <tr>
                            <td class="SubHead" valign="top">
                                <%= DotNetZoom.getlanguage("label_message_object") %>:</td>
                            <td>
                                <asp:Label id="Subject" runat="server" cssclass="Normal" width="500"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="SubHead" valign="top">
                                <%= DotNetZoom.getlanguage("label_message_user") %>:</td>
                            <td>
                                <asp:Label id="CreatedByUser" runat="server" cssclass="Normal" width="500"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="SubHead" valign="top">
                                <%= DotNetZoom.getlanguage("label_message_date") %>:</td>
                            <td>
                                <asp:Label id="CreatedDate" runat="server" cssclass="Normal" width="500"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="SubHead" valign="top">
                                <%= DotNetZoom.getlanguage("label_message_body") %>:</td>
                            <td align="left" width="100%" >
                                <asp:Label id="Body" runat="server" cssclass="Normal" width="500"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:LinkButton class="CommandButton" id="cmdCancel2" runat="server" Text="Annuler" CausesValidation="False"></asp:LinkButton>
                                &nbsp; 
                                <asp:LinkButton class="CommandButton" id="cmdReply" runat="server" Text="Reply" CausesValidation="False"></asp:LinkButton>
                                &nbsp; 
                                <asp:LinkButton class="CommandButton" id="cmdEdit" runat="server" Text="Modifier" CausesValidation="False"></asp:LinkButton>
                                &nbsp; 
                                <asp:LinkButton class="CommandButton" id="cmdDelete" runat="server" Text="Supprimer" CausesValidation="False"></asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
</asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>