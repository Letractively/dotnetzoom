<%@ Control Inherits="DotNetZoom.ModuleDefs" codebehind="ModuleDefs.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:datagrid id="grdDefinitions" runat="server"  EnableViewState="false" AutoGenerateColumns="false" cellspacing="4" CellPadding="4" BorderStyle="None" gridlines="none">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="20">
            <ItemTemplate>
                <asp:HyperLink NavigateUrl='<%# EditURL("defid",DataBinder.Eval(Container.DataItem,"ModuleDefID")) %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1">
                    <asp:Image ImageURL="~/images/edit.gif" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' ToolTip='<%# DotNetZoom.GetLanguage("modifier") %>' Visible="<%# IsEditable %>" runat="server" ID="Hyperlink1Image" />
                </asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="FriendlyName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
        <asp:BoundColumn DataField="Description" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
       <asp:TemplateColumn ItemStyle-HorizontalAlign="Center"  HeaderStyle-Cssclass="NormalBold" ItemStyle-Width="20">  
		 <ItemTemplate>
		 <span class="normal"><%# DotNetZoom.GetLanguage("*" & DataBinder.Eval(Container.DataItem,"IsPremium")) %></span>
       	</ItemTemplate>
	   </asp:TemplateColumn>
    </Columns>
</asp:datagrid>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>