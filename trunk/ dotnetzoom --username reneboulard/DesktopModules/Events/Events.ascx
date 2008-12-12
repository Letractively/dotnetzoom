<%@ Control Language="vb" Inherits="DotNetZoom.Events" codebehind="Events.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" DisplayOptions="True" runat="server"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:datalist id="lstEvents" runat="server" CellPadding="4" EnableViewState="false">
<ItemTemplate>
<table cellpadding="2" cellspacing="0" border="0"><tr>
<td id="colIcon" runat="server" valign="top" align="center" rowspan="3" width='<%# DataBinder.Eval(Container.DataItem,"MaxWidth") %>'>
<asp:Image ID="imgIcon" AlternateText='<%# DataBinder.Eval(Container.DataItem,"AltText") %>' runat="server" ImageUrl='<%# FormatImage(DataBinder.Eval(Container.DataItem,"IconFile")) %>' Visible='<%# FormatImage(DataBinder.Eval(Container.DataItem,"IconFile")) <> "" %>'></asp:Image>
</td><td>
<asp:HyperLink id="editLink" NavigateUrl='<%# EditURL("ItemID",DataBinder.Eval(Container.DataItem,"ItemID")) %>' Visible="<%# IsEditable %>" runat="server">
<asp:Image id="editLinkImage" ImageUrl="~/images/edit.gif" Visible="<%# IsEditable %>" AlternateText="Modifier" runat="server" />
</asp:HyperLink>
<asp:Label id="lblTitle" runat="server" cssclass="ItemTitle" text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Label>
</td></tr><tr><td>
<asp:Label id="lblDateTime" runat="server" cssclass="ItemTitle" text='<%# FormatDateTime(DataBinder.Eval(Container.DataItem,"DateTime")) %>'></asp:Label>
</td></tr><tr><td>
<asp:Label id="lblDescription" runat="server" cssclass="Normal" text='<%# DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
</td></tr></table><br>
</ItemTemplate>
</asp:datalist>
<asp:calendar id="calEvents" runat="server" SelectionMode="None" CssClass="Normal" BorderWidth="1">
<DayHeaderStyle backcolor="#EEEEEE" cssclass="NormalBold" bordercolor="black" borderwidth="1"></DayHeaderStyle>
<DayStyle cssclass="Normal" bordercolor="black" height="12%" borderwidth="1" verticalalign="Top"></DayStyle>
<OtherMonthDayStyle forecolor="#FFFFFF" bordercolor="black" borderwidth="1"></OtherMonthDayStyle>
<TitleStyle font-bold="True" bordercolor="black" borderwidth="1"></TitleStyle>
<NextPrevStyle cssclass="NormalBold"></NextPrevStyle>
</asp:calendar>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>