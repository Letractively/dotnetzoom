<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TTT_Viewer.aspx.vb" Inherits="DotNetZoom.TTT_GalleryViewer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<title><%= DotNetZoom.GetLanguage("Viewer_Image") %></title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
		<asp:placeholder id="CSS" runat="server"></asp:placeholder>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table class="TTTBorder" cellSpacing="1" cellPadding="0" align="center">
				<tr>
					<td class="TTTHeader" valign="middle" height="28"><asp:label id="Album" CssClass="TTTHeaderText" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="TTTAltHeader" align="left">&nbsp;
									<asp:hyperlink id="MovePrevious" runat="server" ><%= DotNetZoom.getlanguage("Gal_Prev") %></asp:hyperlink></td>
								<td class="TTTAltHeader" valign="middle"><asp:label id="Title1" runat="server" cssclass="TTTNormalBold"></asp:label></td>
								<td class="TTTAltHeader" align="right"><asp:hyperlink id="MoveNext" runat="server"><%= DotNetZoom.getlanguage("Gal_Next") %></asp:hyperlink>&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" class="TTTRow" valign="middle" height="22">
						<asp:label id="Description" cssclass="TTTRow" runat="server"></asp:label>
					</td>
				</tr>
				<tr>
					<td align="center" valign="middle" class="TTTRow">
						<asp:image id="Image" runat="server" BorderStyle="Outset" BorderWidth="2px" BorderColor="#D1D7DC"></asp:image>
					</td>
				</tr>
				<tr>
					<td align="center" class="TTTRow" valign="middle" height="22">
						<asp:label id="lblInfo" cssclass="TTTRow" runat="server"></asp:label>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>