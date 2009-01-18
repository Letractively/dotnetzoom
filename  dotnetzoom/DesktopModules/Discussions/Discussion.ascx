<%@ Control Language="vb" Inherits="DotNetZoom.Discussion" codebehind="Discussion.ascx.vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server" DisplayOptions="False"></portal:title>
<asp:PlaceHolder id="pnlModuleContent" Runat="server" EnableViewState="true">
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal> 
    <asp:DataList id="lstDiscussions"  runat="server"  ItemStyle-Cssclass="Normal" DataKeyField="Parent" CellPadding="4" >
	   <ItemTemplate>
            <asp:ImageButton id="btnSelect" ToolTip='<%# Nodetext(Cint(DataBinder.Eval(Container.DataItem, "ChildCount"))) %>' AlternateText='<%# Nodetext(Cint(DataBinder.Eval(Container.DataItem, "ChildCount"))) %>' ImageURL="~/images/1x1.gif" CommandName="select" runat="server" height="12" width="12" style='<%# NodeStyle(Cint(DataBinder.Eval(Container.DataItem, "ChildCount"))) %>' />&nbsp;
            
            <asp:hyperlink Text='<%# FormatMultiLine(DataBinder.Eval(Container.DataItem, "Title")) %>' NavigateUrl='<%# EditUrl("ItemID",DataBinder.Eval(Container.DataItem, "ItemId")) & "&itemindex=" & lstDiscussions.Items.Count %>' runat="server" />
            , <%# DotNetZoom.GetLanguage("message_de") %>&nbsp;<%# FormatUser(DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>,
            <%# DotNetZoom.GetLanguage("message_le")%> <%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %>
        </ItemTemplate>
        <SelectedItemTemplate>
            <asp:ImageButton id="btnCollapse" ToolTip='<%# NodetextFermer(Cint(DataBinder.Eval(Container.DataItem, "ChildCount"))) %>' AlternateText='<%# NodetextFermer(Cint(DataBinder.Eval(Container.DataItem, "ChildCount"))) %>' ImageURL="~/images/1x1.gif" height="12" width="12" Style='<%# NodeStylefermer(Cint(DataBinder.Eval(Container.DataItem, "ChildCount"))) %>' CommandName="collapse" runat="server" BorderWidth="0" BorderStyle="none"/>&nbsp;
            <asp:hyperlink Text='<%# FormatMultiLine(DataBinder.Eval(Container.DataItem, "Title")) %>' NavigateUrl='<%# EditUrl("ItemID",DataBinder.Eval(Container.DataItem, "ItemId")) & "&itemindex=" & lstDiscussions.Items.Count %>' runat="server" />
            , <%# DotNetZoom.GetLanguage("message_de") %>&nbsp;<%# FormatUser(DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>,
            <%# DotNetZoom.GetLanguage("message_le")%> <%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %><br />
			
			<%# FormatMultiLine(DataBinder.Eval(Container.DataItem,"Body")) %>
		
			<hr size="1">
            <asp:DataList id="DetailList" ItemStyle-Cssclass="Normal" datasource="<%# GetThreadMessages() %>" runat="server">
				<ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "Indent") %>
                    
                    <asp:hyperlink Text='<%# FormatMultiLine(DataBinder.Eval(Container.DataItem, "Title")) %>' NavigateUrl='<%# EditUrl("ItemID",DataBinder.Eval(Container.DataItem, "ItemId")) & "&itemindex=" & lstDiscussions.Items.Count %>' runat="server" />
                    , <%# DotNetZoom.GetLanguage("message_de") %>&nbsp;<%# FormatUser(DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>,
                    <%# DotNetZoom.GetLanguage("message_le")%> <%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %><br />
					<%# FormatMultiLine(DataBinder.Eval(Container.DataItem,"Body")) %><hr noshade size="1">
                </ItemTemplate>			
            </asp:DataList>
        </SelectedItemTemplate>
    </asp:DataList>
<asp:Button cssclass="button" id="cmdAdd" runat="server" CausesValidation="False"></asp:Button>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>
</asp:PlaceHolder>