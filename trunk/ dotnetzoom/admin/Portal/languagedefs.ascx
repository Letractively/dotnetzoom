<%@ Control Inherits="DotNetZoom.LanguageDefs" codebehind="LanguageDefs.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<div align="Center">
<asp:datalist width="100%" id="dlTabsTop" ItemStyle-Height="15" separatorstyle-cssclass="TabSeparator" runat="Server" cssclass="TabHolder" itemstyle-cssclass="TabDefault" itemstyle-wrap="False" cellpadding="0" repeatcolumns="5">
		<itemtemplate>
				<b><%# Container.DataItem.Value %></b>
		</itemtemplate>
		<separatortemplate>
			<img src="/images/1x1.gif" alt="*" border="0">
		</separatortemplate>
</asp:datalist>
<asp:datalist width="100%" id="dlTabsBottom" ItemStyle-Height="25" separatorstyle-cssclass="TabSeparator"  runat="Server" cssclass="TabHolder" selecteditemstyle-cssclass="TabSelected" itemstyle-cssclass="TabDefault" itemstyle-wrap="False" cellpadding="0" repeatcolumns="5">
		<itemtemplate>
				<b><%# Container.DataItem.Value %></b>
		</itemtemplate>
		<separatortemplate>
			<img src="/images/1x1.gif"  alt="*" border="0">
		</separatortemplate>
</asp:datalist>
<table class="TabPage" cellspacing="0" cellpadding="0" width="100%">
 <tbody>
  <tr>
   <td>
<table cellpadding="5" cellspacing="0" width="750">
<tr>
	<td height="20">
	<asp:DropDownList id="ddlModLanguage" Width="150px" AutoPostBack="True" DataValueField="language" DataTextField="SettingValue" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>
	</td>
	<td>
	<asp:Label id="lblMessage" runat="server" class="NormalRed" EnableViewState="false"></asp:Label>
	</td>
</tr>

</table>

<asp:placeHolder id="Setting1" runat="server">
<!-- Debut PlaceHolder Setting1 -->	
<div align="center">
<table cellpadding="10" cellspacing="0" width="750">
<tr>
	<td>
    <asp:Button cssclass="button" id="GenerateScript" runat="server"></asp:Button>
	</td>
</tr>
<tr>
	<td class="NormalBold" width="200" align="left">
	<%= DotNetZoom.GetLanguage("language_new")%></td>
	<td class="NormalBold"  width="550" align="left"><%= DotNetZoom.GetLanguage("UO_From")%>:&nbsp;&nbsp;<asp:DropDownList id="ddlLanguage" Width="150px" AutoPostBack="False" DataValueField="language" DataTextField="SettingValue" runat="server" CssClass="NormalTextBox" ></asp:DropDownList>
	&nbsp;&nbsp;<%= DotNetZoom.GetLanguage("to")%>:&nbsp;&nbsp;
	<asp:DropDownList id="ddltoLanguage" Width="150px" AutoPostBack="False" runat="server" DataValueField="language" DataTextField="SettingValue" CssClass="NormalTextBox" ></asp:DropDownList>
	 &nbsp;&nbsp;<asp:ImageButton id="CreateSetting" ImageURL="~/images/save.gif" Width="16px" Height="16px" runat="server" EnableViewState="true" BorderWidth="0" BorderStyle="none" CausesValidation="False"></asp:ImageButton>
	</td>
</tr>
<tr>
	<td width="200" align="left">
	<asp:label class="NormalBold" id="lblDelLang" visible="false" runat=server></asp:label>
	</td>
	<td class="NormalBold" width="550" align="left">
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList id="ddlDelLang" visible="false" Width="150px" AutoPostBack="True" runat="server" DataValueField="language" DataTextField="SettingValue" CssClass="NormalTextBox" ></asp:DropDownList>
	&nbsp;&nbsp;<asp:ImageButton id="DelLang" visible="false" ImageURL="~/images/delete.gif" Width="16px" Height="16px" runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="True"></asp:ImageButton>
	</td>
</tr>
</table>
</div>
</asp:PlaceHolder>
<asp:placeHolder id="Setting2" runat="server">
<!-- Debut PlaceHolder Setting2 -->	
<div align="center">

<table cellpadding="10" cellspacing="0" width="750">
<tr>
	<td>
	<span class="NormalBold"><%= DotNetZoom.GetLanguage("language_see")%>&nbsp;</span><asp:DropDownList id="cboLonglanguage" DataValueField="SettingName" DataTextField="SettingName" AutoPostBack="True" runat="server" cssclass="NormalTextBox" Width="200px" EnableViewState="true" Visible="True"></asp:DropDownList>
	</td>
</tr>
<tr>
	<td>
<asp:ImageButton id="DelLongLang" visible="false" ImageURL="~/images/delete.gif" Width="16px" Height="16px" runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="True"></asp:ImageButton>
<asp:TextBox MaxLength="50" id="SettingToSave" runat="server" Width="360px">&nbsp;&nbsp;</asp:TextBox><asp:ImageButton id="SaveLongSetting" ImageURL="~/images/save.gif" Width="16px" Height="16px" runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
<br><asp:TextBox id="txtLongLanguage" runat="server" Width="750px" height="300px" TextMode="MultiLine"></asp:TextBox>
</td>
</tr>
</table>
</div>
</asp:PlaceHolder>
<asp:placeHolder id="Setting3" runat="server">
<!-- Debut PlaceHolder Setting3 -->
<div align="center">

<table cellpadding="10" cellspacing="0" width="750">
<tr>
	<td>
<span class="NormalBold"><%= DotNetZoom.GetLanguage("language_see")%>&nbsp;</span><asp:DropDownList id="cbolanguage" AutoPostBack="True" runat="server" cssclass="NormalTextBox" Width="200px" EnableViewState="true" Visible="True"></asp:DropDownList>
</td>
</tr>
</table>
<table cellspacing="0" cellpadding="4" border="0" runat="server" id="tableAddItem">
	<tr>
<td class="NormalBold"><%= DotNetZoom.GetLanguage("language_add")%></td>
	</tr><tr>
<td class="NormalBold"><%= DotNetZoom.GetLanguage("language_param")%></td><td width="200"><asp:TextBox MaxLength="50" id="txtSettingName" runat="server" Width="190px"></asp:TextBox></td>
	</tr><tr>
<td class="NormalBold"><%= DotNetZoom.GetLanguage("language_context")%></td>
<td width="50"><asp:TextBox MaxLength="200" id="txtContextValue" runat="server" Width="190px"></asp:TextBox></td>
	</tr><tr>
<td class="NormalBold"><%= DotNetZoom.GetLanguage("language_value")%></td>
<td width="500"><asp:TextBox MaxLength="256" id="txtSettingValue" runat="server" Width="460px" height="50px" TextMode="MultiLine"></asp:TextBox>&nbsp;
<asp:ImageButton id="AddSetting" ImageURL="~/images/save.gif" Width="16px" Height="16px" runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
</td></tr></table>
<asp:datagrid id="gdrLanguage" runat="server"  EnableViewState="true" AutoGenerateColumns="false" cellspacing="4" CellPadding="4" BorderStyle="None" gridlines="none">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="100" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <span onmouseover="<%# DotNetZoom.ReturnToolTip("Context: " & DataBinder.Eval(Container, "DataItem.Context") & "<br>Param: " & DataBinder.Eval(Container, "DataItem.SettingName") & "<br>Value: " & DataBinder.Eval(Container, "DataItem.SettingValue") )  %>"><%# DataBinder.Eval(Container, "DataItem.SettingName") %></span>
        </ItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="20" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
		<asp:image id="image" runat="server" visible='<%# not DataBinder.Eval(Container, "DataItem.New") %>' AlternateText="new" ImageURL="~/images/node.gif"></asp:image>
		<asp:imagebutton id="cmdEdit" runat="server" causesvalidation="false" commandname="Edit" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' ImageURL="~/images/edit.gif"></asp:imagebutton>
		</ItemTemplate>
		<EditItemTemplate>
        <asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("delete") & " -> " & DataBinder.Eval(Container, "DataItem.SettingValue") %>' id="imgdelete" ImageURL="~/images/delete.gif" CommandName="Delete" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SettingName") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        </EditItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn ItemStyle-Width="450" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
		<%# DataBinder.Eval(Container, "DataItem.SettingValue") %>
         </ItemTemplate>
		<EditItemTemplate>
        <asp:TextBox id="txtRename" runat="server" MaxLength="256" Width="440px" height="50px" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container, "DataItem.SettingValue") %>'></asp:TextBox>
		</EditItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="40" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <EditItemTemplate>
		<asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("enregistrer") & " -> " & DataBinder.Eval(Container, "DataItem.SettingValue") %>' id="imgEditOK" ImageURL="~/images/save.gif" CommandName="EditOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SettingName") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        <asp:imagebutton id="cmdCancel" runat="server" causesvalidation="false" commandname="Cancel" tooltip='<%# DotNetZoom.GetLanguage("annuler") %>' AlternateText='<%# DotNetZoom.GetLanguage("annuler") %>' ImageURL="~/images/cancel.gif"></asp:imagebutton>
		</EditItemTemplate>
		</asp:TemplateColumn>
    </Columns>
</asp:datagrid>
</div>	
</asp:PlaceHolder>
<asp:placeHolder id="Setting4" runat="server">
<!-- Debut PlaceHolder Setting4 -->
<div align="center">

<br><span class="ItemTitle"><%= dotnetzoom.GetLanguage("LanguageSettings4")%></span><br>
<asp:datagrid id="gdrCountryCode" runat="server"  EnableViewState="true" AutoGenerateColumns="false" cellspacing="4" CellPadding="4" BorderStyle="None" gridlines="none">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="20" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <%# DataBinder.Eval(Container, "DataItem.Code") %>
        </ItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="550" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <asp:TextBox id="txtRename" runat="server" MaxLength="100" Width="460px" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:TextBox>
		&nbsp;<asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("enregistrer") & " -> " & DataBinder.Eval(Container, "DataItem.Description") %>' id="imgEditOK" ImageURL="~/images/save.gif" CommandName="EditOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Code") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        </ItemTemplate>
		</asp:TemplateColumn>
    </Columns>
</asp:datagrid>
</div>	
</asp:PlaceHolder>
<asp:placeHolder id="Setting5" runat="server">
<!-- Debut PlaceHolder Setting5 -->
<div align="center">
<asp:datagrid id="gdrAdmin" runat="server"  EnableViewState="true" AutoGenerateColumns="false" cellspacing="4" CellPadding="4" BorderStyle="None" gridlines="none">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="200" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <asp:TextBox id="txtFriendLyName" runat="server" MaxLength="128" Width="180px" Text='<%# DataBinder.Eval(Container, "DataItem.FriendLyName") %>'></asp:TextBox>
        </ItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="450" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <asp:TextBox id="txtDescription" runat="server" Width="460px" height="60px" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:TextBox>
        </ItemTemplate>
		</asp:TemplateColumn>
		 <asp:TemplateColumn ItemStyle-Width="20" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
		&nbsp;<asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("enregistrer") & " -> " & DataBinder.Eval(Container, "DataItem.FriendLyName") %>' id="imgEditOK" ImageURL="~/images/save.gif" CommandName="EditOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ModuleDefID") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        </ItemTemplate>
		</asp:TemplateColumn>
    </Columns>
</asp:datagrid>
</div>	
</asp:PlaceHolder>
<asp:placeHolder id="Setting6" runat="server">
<!-- Debut PlaceHolder Setting6 -->
<div align="center">
<br><span class="ItemTitle"><%= dotnetzoom.GetLanguage("LanguageSettings6")%></span><br>
<asp:datagrid id="gdrCurrencyCode" runat="server"  EnableViewState="true" AutoGenerateColumns="false" cellspacing="4" CellPadding="4" BorderStyle="None" gridlines="none">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="20" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <%# DataBinder.Eval(Container, "DataItem.Code") %>
        </ItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="550" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <asp:TextBox id="txtRename" runat="server" MaxLength="100" Width="460px" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:TextBox>
		&nbsp;<asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("enregistrer") & " -> " & DataBinder.Eval(Container, "DataItem.Description") %>' id="imgEditOK" ImageURL="~/images/save.gif" CommandName="EditOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Code") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        </ItemTemplate>
		</asp:TemplateColumn>
    </Columns>
</asp:datagrid>
</div>	
</asp:PlaceHolder>
<asp:placeHolder id="Setting7" runat="server">
<!-- Debut PlaceHolder Setting7 -->
<div align="center">
<br><span class="ItemTitle"><%= dotnetzoom.GetLanguage("LanguageSettings7")%></span><br>
<asp:datagrid id="gdrCodeFrequency" runat="server"  EnableViewState="true" AutoGenerateColumns="false" cellspacing="4" CellPadding="4"  BorderStyle="None" gridlines="none">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="20" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <%# DataBinder.Eval(Container, "DataItem.Code")%>
        </ItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="550" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <asp:TextBox id="txtRename" runat="server" MaxLength="50" Width="460px" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:TextBox>
		&nbsp;<asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("enregistrer") & " -> " & DataBinder.Eval(Container, "DataItem.Description") %>' id="imgEditOK" ImageURL="~/images/save.gif" CommandName="EditOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Code") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        </ItemTemplate>
		</asp:TemplateColumn>
    </Columns>
</asp:datagrid>
</div>	
</asp:PlaceHolder>
<asp:placeHolder id="Setting8" runat="server">
<!-- Debut PlaceHolder Setting8 -->
<div align="center">
<br><span class="ItemTitle"><%= dotnetzoom.GetLanguage("LanguageSettings8")%></span><br>
<asp:datagrid id="gdrLogCode" runat="server"  EnableViewState="true" AutoGenerateColumns="false" cellspacing="4" CellPadding="4" BorderStyle="None" gridlines="none">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="20" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <%# DataBinder.Eval(Container, "DataItem.Code") %>
        </ItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="550" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <asp:TextBox id="txtRename" runat="server" MaxLength="50" Width="460px" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:TextBox>
		&nbsp;<asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("enregistrer") & " -> " & DataBinder.Eval(Container, "DataItem.Description") %>' id="imgEditOK" ImageURL="~/images/save.gif" CommandName="EditOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Code") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        </ItemTemplate>
		</asp:TemplateColumn>
    </Columns>
</asp:datagrid>
</div>	
</asp:PlaceHolder>
<asp:placeHolder id="Setting9" runat="server">
<!-- Debut PlaceHolder Setting9 -->
<div align="center">
<br><span class="ItemTitle"><%= dotnetzoom.GetLanguage("LanguageSettings9")%></span><br>
<asp:DropDownList id="cboCountry" DataValueField="Code" DataTextField="Description" AutoPostBack="True" runat="server" cssclass="NormalTextBox" Width="200px" EnableViewState="true" Visible="True"></asp:DropDownList>
&nbsp;<asp:TextBox id="txtRegionCode" runat="server" MaxLength="2" Width="20px"></asp:TextBox>
&nbsp;<asp:TextBox id="txtRegionValue" runat="server" MaxLength="100" Width="460px"></asp:TextBox>
&nbsp;<asp:ImageButton id="SaveRegionCode" ImageURL="~/images/save.gif" Width="16px" Height="16px" runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
<br>
<asp:datagrid id="gdrRegionCode" runat="server"  EnableViewState="true" AutoGenerateColumns="false" cellspacing="4" CellPadding="4" BorderStyle="None" gridlines="none">
    <Columns>
	    <asp:TemplateColumn ItemStyle-Width="20" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <asp:imagebutton id="cmdEdit" runat="server" causesvalidation="false" commandname="Edit" AlternateText='<%# DotNetZoom.GetLanguage("modifier") %>' ImageURL="~/images/edit.gif"></asp:imagebutton>
		</ItemTemplate>
		<EditItemTemplate>
        <asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("delete") & " -> " & DataBinder.Eval(Container, "DataItem.Description") %>' id="imgdelete" ImageURL="~/images/delete.gif" CommandName='delete' Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Code") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        </EditItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="20" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <%# DataBinder.Eval(Container, "DataItem.Code") %>
        </ItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="450" ItemStyle-CssClass="NormalBold" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
		<%# DataBinder.Eval(Container, "DataItem.Description") %>
        </ItemTemplate>
		<EditItemTemplate>
        <asp:TextBox id="txtRename" runat="server" MaxLength="100" Width="400px" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:TextBox>
		</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn ItemStyle-Width="40" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <EditItemTemplate>
		<asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("enregistrer") & " -> " & DataBinder.Eval(Container, "DataItem.Description") %>' id="imgEditOK" ImageURL="~/images/save.gif" CommandName="EditOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Code") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        <asp:imagebutton id="cmdCancel" runat="server" causesvalidation="false" commandname="Cancel" tooltip='<%# DotNetZoom.GetLanguage("annuler") %>' AlternateText='<%# DotNetZoom.GetLanguage("annuler") %>' ImageURL="~/images/cancel.gif"></asp:imagebutton>
        </EditItemTemplate>
		</asp:TemplateColumn>

    </Columns>
</asp:datagrid>
</div>	
</asp:PlaceHolder>
<asp:placeHolder id="Setting10" runat="server">
<!-- Debut PlaceHolder Setting10 -->
<div align="center">
<br><span class="ItemTitle"><%= dotnetzoom.GetLanguage("LanguageSettings10")%></span><br>
<asp:datagrid id="gdrTimeZone" runat="server"  EnableViewState="true" AutoGenerateColumns="false" cellspacing="4" CellPadding="4" BorderStyle="None" gridlines="none">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="20" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <%# DataBinder.Eval(Container, "DataItem.Zone")/60 %>
        </ItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="550" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <asp:TextBox id="txtRename" runat="server" MaxLength="100" Width="460px" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:TextBox>
		&nbsp;<asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("enregistrer") & " -> " & DataBinder.Eval(Container, "DataItem.Description") %>' id="imgEditOK" ImageURL="~/images/save.gif" CommandName="EditOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Zone") %>' runat="server" EnableViewState="true" BorderWidth="0" CausesValidation="False"></asp:ImageButton>
        </ItemTemplate>
		</asp:TemplateColumn>
    </Columns>
</asp:datagrid>
</div>	
</asp:PlaceHolder>

</td>
</tr>
</tbody>
</table>
</div>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>