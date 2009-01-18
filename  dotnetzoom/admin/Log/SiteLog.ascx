<%@ Control Inherits="DotNetZoom.SiteLog" codebehind="SiteLog.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<script language="javascript" type="text/javascript"  src="<%=dotnetzoom.glbpath + "controls/PopupCalendar.js"%>"></script>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<div align="center">
    <table cellspacing="2" cellpadding="2"  border="0">
        <tbody>
            <tr valign="top">
                <td class="NormalBold" valign="top" align="center">
                    <label for="<%=cboReportType.ClientID%>"><%= DotNetZoom.GetLanguage("Stat_ToGet") %>:</label> 
                </td>
                <td class="NormalBold" align="left">
                    <asp:DropDownList id="cboReportType" CssClass="NormalTextBox" DataTextField="Description" DataValueField="Code" Runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="NormalBold" align="left">
                    <label for="<%= txtStartDate.ClientID%>"><%= DotNetZoom.GetLanguage("Stat_StartDate") %>:</label> 
                </td>
                <td class="NormalBold" align="left">
                    <asp:TextBox id="txtStartDate" runat="server" CssClass="NormalTextBox" Columns="20" width="120"></asp:TextBox>
                    &nbsp; 
                    <asp:HyperLink id="cmdStartCalendar" CssClass="CommandButton" Runat="server"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td class="NormalBold" align="left">
                    <label for="<%=txtEndDate.ClientID%>"><%= DotNetZoom.GetLanguage("Stat_EndDate") %>:</label> 
                </td>
                <td class="NormalBold" align="left">
                    <asp:TextBox id="txtEndDate" runat="server" CssClass="NormalTextBox" Columns="20" width="120"></asp:TextBox>
                    &nbsp; 
                    <asp:HyperLink id="cmdEndCalendar" CssClass="CommandButton" Runat="server"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td class="NormalBold" valign="top" align="center" colspan="2">
                    <asp:LinkButton id="cmdDisplay" runat="server" Text="Display" cssclass="CommandButton"></asp:LinkButton>
                    &nbsp;&nbsp; 
                    <asp:LinkButton id="cmdCancel" runat="server" CssClass="CommandButton"></asp:LinkButton>
                </td>
            </tr>
        </tbody>
    </table>
    <br>
    <asp:datagrid id="grdLog" Runat="server" EnableViewState="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Normal" HeaderStyle-CssClass="NormalBold" AutoGenerateColumns="true" CellSpacing="4" CellPadding="4" Borderwidth="0" gridlines="none" ></asp:datagrid>
    <br>
    <asp:Label id="lblMessage" runat="server" cssclass="NormalBold" enableviewstate="False"></asp:Label>
</div>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>