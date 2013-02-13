<%@ Control Language="vb" codebehind="EditAnnouncements.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.EditAnnouncements" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="Portal" TagName="ItemEdit" Src="~/admin/tabs/ItemEdit.ascx" %>

<script language="javascript" type="text/javascript"  src="<%=dotnetzoom.glbpath + "controls/PopupCalendar.js"%>"></script>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>

<asp:PlaceHolder ID="paneledit" runat="server">
<table cellspacing="0" cellpadding="0" width="750">
    <tbody>
            <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtTitle.ClientID%>"><%= DotNetZoom.GetLanguage("label_title") %>:</label></td>
            <td>
                <asp:textbox id="txtTitle" runat="server" maxlength="100" Columns="30" width="390" cssclass="NormalTextBox"></asp:textbox>
                <br>
                <asp:requiredfieldvalidator id="valTitle" runat="server" CssClass="NormalRed" ControlToValidate="txtTitle" Display="Dynamic"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=txtDescription.ClientID%>"><%= DotNetZoom.GetLanguage("label_description") %>:</label></td>
            <td>
                <asp:textbox id="txtDescription" runat="server" Columns="44" width="390" CssClass="NormalTextBox" Rows="6" TextMode="Multiline"></asp:textbox>
                <br>
                <asp:requiredfieldvalidator id="valDescription" runat="server" CssClass="NormalRed" ControlToValidate="txtDescription"  Display="Dynamic"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=chkComment.ClientID%>"><%= DotNetZoom.GetLanguage("BB_AllowComments")%>:</label></td>
            <td>
                <asp:checkbox id="chkComment"  AutoPostBack="true" CssClass="NormalTextBox" Runat="server"></asp:checkbox>
            </td>
        </tr>

        <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label class="SubHead" style="DISPLAY: none" for="<%=optExternal.ClientID%>"><%= DotNetZoom.GetLanguage("label_selectlink") %></label> 
                <asp:radiobutton id="optExternal" AutoPostBack="True" GroupName="LinkType" Runat="server"></asp:radiobutton>
                <%= DotNetZoom.GetLanguage("label_link") %><label for="<%=txtExternal.ClientID%>">:</label></td>
            <td>
                <asp:textbox id="txtExternal" runat="server" maxlength="250" cssclass="NormalTextBox" Width="390"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td class="SubHead" valign="top">
                <label class="SubHead" style="DISPLAY: none" for="<%=optInternal.ClientID%>"><%= DotNetZoom.GetLanguage("label_selectIlink") %></label> 
                <asp:radiobutton id="optInternal" AutoPostBack="True" GroupName="LinkType" Runat="server"></asp:radiobutton>
                <%= DotNetZoom.GetLanguage("label_Ilink") %><label for="<%=cboInternal.ClientID%>">:</label> 
            </td>
            <td>
                <asp:dropdownlist id="cboInternal" width="390" CssClass="NormalTextBox" Runat="server" DataTextField="TabName" DataValueField="TabId"></asp:dropdownlist>
            </td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label class="SubHead" style="DISPLAY: none" for="<%=optFile.ClientID%>"><%= DotNetZoom.GetLanguage("label_SelectFile") %></label> 
                <asp:radiobutton id="optFile" AutoPostBack="True" GroupName="LinkType" Runat="server"></asp:radiobutton>
                <label for="<%=cboFile.ClientID%>"><%= DotNetZoom.GetLanguage("label_File") %>:</label> 
            </td>
            <td>
                <asp:dropdownlist id="cboFile" runat="server" CssClass="NormalTextBox" Width="200" DataTextField="Text" DataValueField="Value"></asp:dropdownlist>
                &nbsp; 
                <asp:hyperlink id="cmdUpload" CssClass="CommandButton" Runat="server"></asp:hyperlink>
                <asp:imagebutton id="UploadReturn" runat="server" EnableViewState="false" visible="False" height="16" width="16" imageurl="~/images/save.gif" style="border-width:0px"></asp:imagebutton>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtExpires.ClientID%>"><%= DotNetZoom.GetLanguage("label_expiration") %></label></td>
            <td>
                <asp:textbox id="txtExpires" runat="server" Columns="20" width="200" cssclass="NormalTextBox" Text=""></asp:textbox>
                &nbsp; 
                <asp:hyperlink id="cmdCalendar" CssClass="CommandButton" Runat="server"></asp:hyperlink>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtViewOrder.ClientID%>"><%= DotNetZoom.GetLanguage("label_vieworder") %>:</label></td>
            <td>
                <asp:textbox id="txtViewOrder" runat="server" maxlength="3" Columns="20" width="200" CssClass="NormalTextBox"></asp:textbox>
                <asp:comparevalidator id="valViewOrder" runat="server" CssClass="NormalRed" ControlToValidate="txtViewOrder"  Display="Dynamic" Type="Integer" Operator="DataTypeCheck"></asp:comparevalidator>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=chkSyndicate.ClientID%>"><%= DotNetZoom.GetLanguage("label_syndicate") %>:</label></td>
            <td>
                <asp:checkbox id="chkSyndicate" CssClass="NormalTextBox" Runat="server"></asp:checkbox>
            </td>
        </tr>
    </tbody>
</table>
<p align="left">
    <asp:linkbutton id="cmdUpdate" runat="server" CssClass="CommandButton" ></asp:linkbutton>
    &nbsp; 
    <asp:linkbutton id="cmdCancel" runat="server" CssClass="CommandButton"  CausesValidation="False"></asp:linkbutton>
    &nbsp; 
    <asp:linkbutton id="cmdDelete" runat="server" CssClass="CommandButton"  CausesValidation="False"></asp:linkbutton>
    &nbsp; 
    <asp:linkbutton id="cmdSyndicate" runat="server" CssClass="CommandButton"  CausesValidation="False"></asp:linkbutton>
</p>
<hr noshade="noshade" size="1" />
<asp:placeholder id="pnlAudit" Runat="server">
    <span class="Normal"><%= DotNetZoom.GetLanguage("label_update_by") %>&nbsp;<asp:Label id="lblCreatedBy" runat="server"></asp:Label>&nbsp;<%= DotNetZoom.GetLanguage("label_update_the") %>&nbsp;<asp:Label id="lblCreatedDate" runat="server"></asp:Label>
    </span>
    <br>
</asp:placeholder>
<br>
<span class="SubHead"><%= DotNetZoom.GetLanguage("channel_syndicate") %> :</span>&nbsp;<asp:Label id="lblSyndicate" runat="server" cssclass="SubHead"></asp:Label>
<br>
<br>
<table cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr valign="top">
            <td class="SubHead"><%= DotNetZoom.GetLanguage("label_clicks") %>:</td>
            <td>
                <asp:Label id="lblClicks" runat="server" cssclass="Normal"></asp:Label></td>
        </tr>
        <tr valign="top">
            <td class="SubHead">
                <label for="<%=chkLog.ClientID%>"><%= DotNetZoom.GetLanguage("label_see_stat") %></label></td>
            <td>
                <asp:checkbox id="chkLog" runat="server" CssClass="NormalTextBox" AutoPostBack="True"></asp:checkbox>
            </td>
        </tr>
    </tbody>
</table>
<asp:datagrid id="grdLog" runat="server" Width="100%" EnableViewState="false" AutoGenerateColumns="false" CellSpacing="3" gridlines="none" Borderwidth="0">
    <Columns>
        <asp:BoundColumn  DataField="DateTime" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn  DataField="FullName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
    </Columns>
</asp:datagrid>



</asp:PlaceHolder>
<asp:PlaceHolder ID="paneloption" runat="server">
<table cellspacing="0" cellpadding="0" width="750">
    <tbody>
            <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=chkAnonymous.ClientID%>"><%= DotNetZoom.GetLanguage("BB_AllowAnonyme")%>:</label></td>
            <td>
                <asp:checkbox id="chkAnonymous"  AutoPostBack="true" CssClass="NormalTextBox" Runat="server"></asp:checkbox>
            </td>
        </tr>
        <tr>
            <td class="SubHead">
                <label for="<%=txtPager.ClientID%>"><%= DotNetZoom.GetLanguage("BB_NumItems")%>:</label></td>
            <td>
                <asp:textbox id="txtPager"  AutoPostBack="true" runat="server"  MaxLength="3" width="20" CssClass="NormalTextBox" TextMode="SingleLine"></asp:textbox>
            </td>
        </tr>
                    <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="SubHead" valign="top" width="300">
            <%= DotNetZoom.GetLanguage("ms_contener")%>:
			</td>
            <td>
                <table cellspacing="0" cellpadding="4" border="0" width="400">
                    <tbody>
					<tr>
					<td colspan="3">
					<portal:ItemEdit id="ContainerEdit" runat="server"></portal:ItemEdit>
					</td>
					</tr>
                    </tbody>
                </table>
            </td>
        </tr>
                    <tr valign="top">
            <td colspan="2">
                &nbsp;</td>
        </tr>

</tbody>
</table>

<p align="left">
    <asp:hyperlink id="retour" runat="server" CssClass="CommandButton" ></asp:hyperlink>
</p>
</asp:PlaceHolder>

<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>