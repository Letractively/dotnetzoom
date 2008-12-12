<%@ Control Language="vb" codebehind="EditFAQs.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditFAQs" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table cellspacing="0" cellpadding="0" width="750" border="0">
    <tbody>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=QuestionField.ClientID%>"><%= DotNetZoom.GetLanguage("label_question")%>:</label> 
            </td>
            <td>
                <asp:TextBox id="QuestionField" runat="server" cssclass="NormalTextBox" TextMode="Multiline" width="405" Rows="7"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=AnswerField.ClientID%>"><%= DotNetZoom.GetLanguage("label_answer")%>:</label> 
            </td>
            <td>
                <asp:TextBox id="AnswerField" runat="server" cssclass="NormalTextBox" TextMode="Multiline" width="405" Rows="15"></asp:TextBox>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" onclick="cmdUpdate_Click" runat="server" ></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdCancel" onclick="cmdCancel_Click" runat="server"  CausesValidation="False"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdDelete" onclick="cmdDelete_Click" runat="server"  CausesValidation="False"></asp:LinkButton>
</p>
<hr width="500" noshade="noshade" size="1" />
<asp:placeholder id="pnlAudit" Runat="server">
    <span class="Normal"><%= DotNetZoom.GetLanguage("label_update_by") %>&nbsp;<asp:Label id="CreatedBy" runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("label_update_the") %>&nbsp;<asp:Label id="CreatedDate" runat="server"></asp:Label></span>
</asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>