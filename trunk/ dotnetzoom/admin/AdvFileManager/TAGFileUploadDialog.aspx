<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TAGFileUploadDialog.aspx.vb" Inherits="DotNetZoom.TAGFileUploadDialog" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<title><%= DotNetZoom.GetLanguage("upload") %> <%= Session("RelativeDir") %></title>
		<SCRIPT type="text/javascript" language="JavaScript"><!--
			var Nav4 = ((navigator.appName == "Netscape") && (parseInt(navigator.appVersion) >= 4));

			// Close the dialog
			function closeme() {
				window.close();
			}

			// Handle click of OK button
			function handleOK() {
				if (opener && !opener.closed) {
					opener.dialogWin.returnFunc();
				} else {
					alert("<%= DotNetZoom.RTESafe(DotNetZoom.GetLanguage("File_Exploreur_Warning")) %>");
				}
				closeme();
				return false;
			}

			// Handle click of Cancel button
			function handleCancel() {
				closeme();
				return false;
			}
			
			// Handle onload
			function block() {
				if (opener) {
					opener.blockEvents();
				}
			}

			// Handle onunload
			function unblock() {
				if (opener) {
					opener.unblockEvents();
				}
			}
		--></SCRIPT>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
	</head>
	<body onload="block()" onunload="unblock()">
 		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
		<span class="Normal"><%= DotNetZoom.GetLanguage("F_Directory") %> : &nbsp;&nbsp;<%= Replace(Session("RelativeDir"), "\", "/") & "/" %></span><br>
         <asp:Label id="lblMessage" runat="server" style="font: xx-small" width="400" enableviewstate="False"></asp:Label>
		  <asp:placeHolder id="CanUpload" runat="server">
			<asp:Table id="Table1" runat="server">
				<asp:TableRow>
					<asp:TableCell>
						<asp:Label runat="server" ID="Label1" Font-Bold="True"></asp:Label>
					</asp:TableCell>
					<asp:TableCell>
						<INPUT id="htmlUploadFile" type="file" name="loFile" runat="server" size="60"></asp:TableCell>
				</asp:TableRow>
				<asp:TableRow>
					<asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
					<asp:CheckBox id="chkUnzip" Runat="server" CssClass="Normal" TextAlign="Left"></asp:CheckBox>
					<asp:Button runat="server" ID="btnUpload"></asp:Button>
					<asp:Button runat="server" ID="btnCancel"></asp:Button>
					</asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			</asp:placeHolder>
		</form>
	</body>
</html>