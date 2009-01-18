<%@ Control Inherits="DotNetZoom.Tabs" codebehind="Tabs.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<table align="center" cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr valign="top">
            <td>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr valign="top">
                            <td>
                                <asp:listbox id="lstTabs" runat="server" rows="20" DataTextField="TabName" DataValueField="TabId" CssClass="NormalTextBox" width="400px"></asp:listbox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td height="100%">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td valign="top">
                                                <asp:imagebutton id="cmdUp" runat="server" CommandName="up" ImageURL="~/images/up.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:imagebutton id="cmdDown" runat="server" CommandName="down" ImageURL="~/images/dn.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:imagebutton id="cmdLeft" runat="server" CommandName="left" ImageURL="~/images/lt.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:imagebutton id="cmdRight" runat="server" CommandName="right" ImageURL="~/images/rt.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td valign="bottom">
                                                <asp:imagebutton id="cmdEdit" runat="server" ImageURL="~/images/edit.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="bottom">
                                                <asp:imagebutton id="cmdView" runat="server" ImageURL="~/images/view.gif" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>