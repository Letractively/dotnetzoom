<%@ Page Language="vb" AutoEventWireup="false"  EnableViewState="true" Codebehind="TTT_Viewer.aspx.vb" Inherits="DotNetZoom.TTT_GalleryViewer" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<title><%= DotNetZoom.GetLanguage("Viewer_Image") %></title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
		<asp:placeholder id="CSS" runat="server"></asp:placeholder>
<script type="text/javascript">
                 
  
    function Cancel(mylink) {
        window.top.close();
        window.top.opener.focus();
        window.top.opener.location.href = mylink.href;
    }
    

</script>

	</head>
	<body>
		<form id="Form1" method="post" runat="server">
        		<asp:placeholder id="pnledit"  EnableViewState="true" Visible="False" Runat="server">
					<table cellSpacing="1" cellPadding="0" width="100%" border="0">
                    <tr>
                    <td class="TTTSubHeader" width="500">
                    
                    <table cellSpacing="1" cellPadding="0" width="100%" border="0">
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_TitleI") %>:</td>
							<td class="TTTRow" width="370" align="left">
								<asp:TextBox id="txtTitle" runat="server" cssclass="NormalTextBox" Width="100%"></asp:TextBox></td>
						</tr>
						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.GetLanguage("WaterMark")%></td>
									<td class="TTTRow" width="370" align="left">
									<asp:TextBox id="txtWaterMark" runat="server" Width="50%" cssclass="NormalTextBox"></asp:TextBox></td>
						</tr>

						<tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Desc") %>:</td>
							<td class="TTTRow" width="370" align="left">
								<asp:TextBox id="txtDescription" TextMode="MultiLine" runat="server" cssclass="NormalTextBox" Width="100%"></asp:TextBox></td>
						</tr>
                        <tr>
							<td class="TTTSubHeader" width="120">&nbsp;<%= DotNetZoom.getlanguage("Gal_Cat") %>:</td>
							<td class="TTTRow" width="370" align="left">
								<asp:DropDownList ID="ddCategories" runat=server></asp:DropDownList></td>
						</tr>
				
					</table>
                    </td>
                    <td class="TTTSubHeader" width="250">
                       <table>
                       <tr><td Align="center" valign="middle" width="100">
                            <asp:Image id="ImgFileIcon" AlternateText="gpsicon" EnableViewState="True" Runat="server"></asp:Image>
                            </td><td Align="center" valign="middle" width="150">
                            <div id="Div1" style="padding:0px; height:97px;  width:49px; :auto; overflow-x:hidden; ">
                            <asp:datalist id="gpsIconImage" runat="server" EnableViewState="True" cssclass="NormalBold"  RepeatDirection="Vertical" cellpadding="0" width="100%">
                            <ItemStyle BorderWidth="0" horizontalalign="Center" verticalalign="Middle"></ItemStyle>
                            <ItemTemplate>
                            <asp:ImageButton ID="ImgThumb" runat="server" EnableViewState="true" Width="24" Height="24" AlternateText="<%# CType(Container.DataItem, DotNetZoom.FolderDetail).Name %>" BorderWidth="0"  CommandArgument="<%# CType(Container.DataItem, DotNetZoom.FolderDetail).url %>" imageurl="<%# CType(Container.DataItem, DotNetZoom.FolderDetail).url %>"/>
                            </ItemTemplate>
                            </asp:datalist>
                            </div>
                        </td></tr>
                        </table>
                     </td>
                </tr>
    		<tr valign="middle">
			<td class="TTTAltHeader" align="left" colspan="3">
				&nbsp;
				<asp:button id="UpdateButton" cssclass="button" runat="server" CommandName="back"></asp:button>
				&nbsp;
			</td>
		</tr>
</table>
</asp:placeholder>

			<table Runat="server" id="table" class="TTTBorder" cellSpacing="0" cellPadding="0" align="center">
				<tr>
                    <td class="TTTHeader" valign="middle" align="left" height="28">
                    <asp:imagebutton id="Delete" runat="server" EnableViewState="false" visible="True" height="16" width="16" ImageURL="~/images/1x1.gif" style="height:16px;width:16px;border-width:0px; background: url('/images/ttt/forum.gif') no-repeat; background-position: 0px -32px;"></asp:imagebutton>
                    <asp:imagebutton id="Edit" runat="server" EnableViewState="false" visible="True" height="16" width="16" imageurl="~/images/1x1.gif" style="border-width:0px; background: url('/images/ttt/forum.gif') no-repeat; background-position:0px -128px;"></asp:imagebutton>
                    </td>
					<td class="TTTHeader" valign="middle" align="center" height="28"><asp:label id="Album" CssClass="TTTHeaderText" Runat="server"></asp:label></td>
                    <td class="TTTHeader" valign="middle" align="right" height="28">
                    <asp:Image runat=server Width="16" Height="16" BorderWidth="0" ImageUrl="~/images/info.gif" ID="info" Visible=false />
                    <asp:hyperlink id="focusto" Visible=false runat="server" ><%= DotNetZoom.GetLanguage("return")%></asp:hyperlink>
                    <asp:hyperlink id="ReturnTO" runat="server" ><%= DotNetZoom.GetLanguage("return")%></asp:hyperlink>
                    </td>
				</tr>
				<tr>
					<td colspan="3">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="TTTAltHeader" align="left">&nbsp;
									<asp:hyperlink id="MovePrevious" runat="server" ><%= DotNetZoom.getlanguage("Gal_Prev") %></asp:hyperlink></td>
								<td class="TTTAltHeader" align="center"" valign="middle"><asp:label id="Title1" runat="server" cssclass="TTTNormalBold"></asp:label></td>
								<td class="TTTAltHeader" align="right"><asp:hyperlink id="MoveNext" runat="server"><%= DotNetZoom.getlanguage("Gal_Next") %></asp:hyperlink>&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" id="img" enableviewstate="true" Runat="server" align="center" valign="middle" class="TTTRow">
						<asp:image id="Image" enableviewstate="true" runat="server" BorderStyle="Outset" BorderWidth="2px" BorderColor="#D1D7DC"></asp:image>
					</td>
				</tr>
				<tr>
					<td colspan="3" align="center" class="TTTRow" valign="middle" height="22">
						<asp:label id="lblInfo" cssclass="TTTRow" runat="server"></asp:label>
					</td>
                    </tr>
			</table>
		</form>
 	</body>
</html>