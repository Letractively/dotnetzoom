<%@ Control Language="vb" Inherits="DotNetZoom.EditHtml" codebehind="EditHtml.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="Editor"  Namespace="dotnetzoom" Assembly="DotNetZoom" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="2" cellpadding="2" border="0">
    <tbody>
        <tr valign="top">
             <td colspan="2" valign="middle">
                <asp:RadioButtonList visible="false" id="optView" Runat="server" CssClass="NormalTextBox" RepeatDirection="Horizontal" AutoPostBack="True">
                    <asp:ListItem Value="B">Texte</asp:ListItem>
                    <asp:ListItem Value="R">Html</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="2">
                <asp:placeholder id="pnlBasicTextBox" Runat="server" Visible="False">
                <asp:TextBox id="txtDesktopHTML" runat="server" CssClass="NormalTextBox" textmode="multiline" rows="25" width="750" columns="75"></asp:TextBox>
                </asp:placeholder>
                <asp:placeholder id="pnlRichTextBox" Runat="server" Visible="False">
				<editor:FCKeditor id="FCKeditor1" BasePath="~/FCKeditor/" runat="server"></editor:FCKeditor>
                </asp:placeholder>
            </td>
        </tr>
        <tr>
            <td class="Head" colspan="2"><hr><br>
			<%= DotNetZoom.GetLanguage("ForSearchHTML") %>
             </td>
		</tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtAlternateSummary.ClientID%>"><%= DotNetZoom.GetLanguage("SearchSummary") %>
                :</label> 
            </td>
            <td>
                <asp:TextBox id="txtAlternateSummary" runat="server" CssClass="NormalTextBox" textmode="multiline" rows="3" width="600" columns="75"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtAlternateDetails.ClientID%>"><%= DotNetZoom.GetLanguage("AlternateDetailSummary") %>:</label> 
            </td>
            <td>
                <asp:TextBox id="txtAlternateDetails" runat="server" CssClass="NormalTextBox" textmode="multiline" rows="5" width="600" columns="75"></asp:TextBox>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" runat="server" ></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdCancel" runat="server"  CausesValidation="False"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdPreview" runat="server"  CausesValidation="False"></asp:LinkButton>
    &nbsp; 
</p>
<table cellspacing="0" cellpadding="0" width="750">
    <tbody>
        <tr valign="top">
            <td>
                <asp:Label id="lblPreview" runat="server" cssclass="Normal"></asp:Label></td>
        </tr>
    </tbody>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>