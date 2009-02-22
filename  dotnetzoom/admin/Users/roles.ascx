<%@ Control Inherits="DotNetZoom.Roles" codebehind="Roles.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:datagrid id="grdRoles" runat="server" gridlines="none" BorderStyle="None" CellPadding="4" CellSpacing="0" Width="100%" AutoGenerateColumns="false" EnableViewState="false">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="20">
            <ItemTemplate>
                <asp:HyperLink ToolTip='<%# DotNetZoom.GetLanguage("modifier")%>' NavigateUrl='<%# EditURL("RoleID",DataBinder.Eval(Container.DataItem,"RoleID")) %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1">
                    <asp:Image ImageURL="~/images/edit.gif" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1Image" />
                </asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="20">
            <ItemTemplate>
                <asp:HyperLink ToolTip='<%# DotNetZoom.GetLanguage("modifier")%>' NavigateUrl='<%# FormatURL(DataBinder.Eval(Container.DataItem,"RoleID")) %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink2">
                    <asp:Image ImageURL="~/images/icon_users_16px.gif" AlternateText='<%# DotNetZoom.GetLanguage("modifier")%>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1Image2" />
                </asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="RoleName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn DataField="Description" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
	    <asp:BoundColumn DataField="ServiceFee" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" DataFormatString="{0:#,##0.00}" />
		<asp:BoundColumn DataField="BillingPeriod" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
		<asp:BoundColumn DataField="BillingFrequency" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
 		<asp:BoundColumn DataField="TrialFee" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" DataFormatString="{0:#,##0.00}" />
        <asp:BoundColumn DataField="TrialPeriod" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn DataField="TrialFrequency" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
       <asp:TemplateColumn ItemStyle-HorizontalAlign="Center"  HeaderStyle-Cssclass="NormalBold" ItemStyle-Width="20">  
		 <ItemTemplate>
		 <span class="normal"><%# DotNetZoom.GetLanguage("*" & DataBinder.Eval(Container.DataItem,"IsPublic")) %></span>
       	</ItemTemplate>
	   </asp:TemplateColumn>
       <asp:TemplateColumn ItemStyle-HorizontalAlign="Center"  HeaderStyle-Cssclass="NormalBold" ItemStyle-Width="20">  
		 <ItemTemplate>
		 <span class="normal"><%# DotNetZoom.GetLanguage("*" & DataBinder.Eval(Container.DataItem,"AutoAssignment")) %></span>
       	</ItemTemplate>
	   </asp:TemplateColumn>
    </Columns>
</asp:datagrid>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>