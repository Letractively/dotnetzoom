<%@ Control Inherits="DotNetZoom.Users" codebehind="Users.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<br>
<asp:datagrid id="grdUsers" runat="server" AllowPaging="True" EnableViewState="false" AutoGenerateColumns="false" width="100%" CellPadding="4" gridlines="none" BorderStyle="None">
    <PagerStyle position="Top" horizontalalign="Center"></PagerStyle>
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="20">
            <ItemTemplate>
                <asp:HyperLink ToolTip='<%# DotNetZoom.GetLanguage("modifier") %>' NavigateUrl='<%# FormatURL("UserID",DataBinder.Eval(Container.DataItem,"UserId")) %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1">
                    <asp:Image ImageURL="~/images/edit.gif" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1Image" />
                </asp:HyperLink>
            </ItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="20">
            <ItemTemplate>
                <asp:HyperLink ToolTip='<%# DotNetZoom.GetLanguage("modifier") %>' NavigateUrl='<%# FormatURLrole("UserID",DataBinder.Eval(Container.DataItem,"UserId")) %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink2">
                    <asp:Image ImageURL="~/images/icon_securityroles_16px.gif" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1Image2" />
                </asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="FullName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn DataField="UserName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
            <ItemTemplate>
                <asp:Label id="lblAddress" runat="server" text='<%# DisplayAddress(DataBinder.Eval(Container.DataItem, "Unit"),DataBinder.Eval(Container.DataItem, "Street"), DataBinder.Eval(Container.DataItem, "City"), DataBinder.Eval(Container.DataItem, "Region"), DataBinder.Eval(Container.DataItem, "Country"), DataBinder.Eval(Container.DataItem, "PostalCode")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="Telephone" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
            <ItemTemplate>
                <asp:Label id="lblEmail" runat="server" text='<%# DisplayEmail(DataBinder.Eval(Container.DataItem, "Email")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
            <ItemTemplate>
                <asp:Label id="lblLastLogin" runat="server" text='<%# DisplayLastLogin(DataBinder.Eval(Container.DataItem, "LastLoginDate")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
       <asp:TemplateColumn ItemStyle-HorizontalAlign="Center"  HeaderStyle-Cssclass="NormalBold" ItemStyle-Width="20">  
		 <ItemTemplate>
		 <span class="normal"><%# DotNetZoom.GetLanguage("*" & DataBinder.Eval(Container.DataItem,"Authorized")) %></span>
       	</ItemTemplate>
	   </asp:TemplateColumn>
    </Columns>
</asp:datagrid>
<p align="center">
    <asp:LinkButton id="cmdDelete" CssClass="CommandButton" Runat="server"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>