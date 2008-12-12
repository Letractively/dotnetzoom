<%@ Control codebehind="DesktopPortalFooter.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.DesktopPortalFooter" %>
<%@ import Namespace="DotNetZoom" %>
<div id="footer">
<asp:Label id="lblFooter" Visible="False" runat="server" EnableViewState="false" cssclass="SelectedTab"></asp:Label>
<div class="a2"><asp:hyperlink id="hypHost" CssClass="OtherTabs" runat="server" EnableViewState="false"></asp:hyperlink>
&nbsp;&nbsp;&nbsp;
<asp:hyperlink id="hypTerms" CssClass="OtherTabs" runat="server" EnableViewState="false"></asp:hyperlink>
&nbsp;&nbsp;&nbsp;
<asp:hyperlink id="hypPrivacy" CssClass="OtherTabs" runat="server" EnableViewState="false"></asp:hyperlink>
</div>
</div>