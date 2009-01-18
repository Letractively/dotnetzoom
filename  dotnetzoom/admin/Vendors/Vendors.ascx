<%@ Control Language="vb" autoeventwireup="false" Explicit="True" codebehind="Vendors.ascx.vb" Inherits="DotNetZoom.Vendors" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server" DisplayOptions="True"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<br>
<asp:datagrid id="grdVendors" runat="server"  AllowPaging="True" EnableViewState="false" AutoGenerateColumns="false" width="100%" CellPadding="4" gridlines="none" BorderStyle="None">
    <PagerStyle position="Top" horizontalalign="Center"></PagerStyle>
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="20">
            <ItemTemplate>
                <asp:HyperLink NavigateUrl='<%# FormatURL("VendorID",DataBinder.Eval(Container.DataItem,"VendorID")) %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1">
                    <asp:Image ImageURL="~/images/edit.gif" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1Image" />
                </asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="VendorName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
            <ItemTemplate>
                <asp:Label id="lblAddress" runat="server" text='<%# DisplayAddress(DataBinder.Eval(Container.DataItem, "Unit"),DataBinder.Eval(Container.DataItem, "Street"), DataBinder.Eval(Container.DataItem, "City"), DataBinder.Eval(Container.DataItem, "Region"), DataBinder.Eval(Container.DataItem, "Country"), DataBinder.Eval(Container.DataItem, "PostalCode")) %>'></asp:Label> 
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="Telephone" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn DataField="Fax" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
            <ItemTemplate>
                <asp:Label id="lblEmail" runat="server" text='<%# DisplayEmail(DataBinder.Eval(Container.DataItem, "Email")) %>'></asp:Label> 
            </ItemTemplate>
        </asp:TemplateColumn>
       <asp:TemplateColumn ItemStyle-HorizontalAlign="Center"  HeaderStyle-Cssclass="NormalBold" ItemStyle-Width="20">  
		 <ItemTemplate>
		 <span class="normal"><%# DotNetZoom.GetLanguage("*" & DataBinder.Eval(Container.DataItem,"Authorized")) %></span>
       	</ItemTemplate>
	   </asp:TemplateColumn>
        <asp:BoundColumn DataField="Banners" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
    </Columns>
</asp:datagrid>
<p align="center">
    <span class="Normal"><%# DotNetZoom.GetLanguage("Vendors_Info_List") %></span>
</p>
<p align="left">&nbsp;&nbsp;&nbsp;&nbsp;
<asp:HyperLink id="cmdEditModule" Runat="server" CssClass="CommandButton"></asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
<asp:HyperLink id="cmdEditOptions" Runat="server" CssClass="CommandButton"></asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
<asp:LinkButton id="cmdDelete" CssClass="CommandButton" Runat="server"></asp:LinkButton>
</p>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>