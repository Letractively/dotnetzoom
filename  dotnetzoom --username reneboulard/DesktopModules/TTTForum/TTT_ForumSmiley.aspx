<%@ Page Language="vb" autoeventwireup="false" codebehind="TTT_ForumSmiley.aspx.vb" Inherits="TTT_ForumSmiley" %>
<%@ import Namespace="DotNetZoom" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <title><%= DotNetZoom.GetLanguage("F_SelectIcon") %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript"> 
    <script type="text/javascript" language="javascript">
			function CloseWindow()
			{
				self.close();
			}
			</script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table id="tbStrip" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
            <tbody>
                <tr>
                    <td class="TTTHeader" align="center" width="100%" height="28">
                        <span class="TTTHeaderText"><%= DotNetZoom.GetLanguage("F_SelectIcon") %></span></td>
                </tr>
                <tr>
                    <td class="TTTRow">
                        <asp:datalist id="dlStrip" runat="server" width="100%" CellSpacing="1" cellpadding="1" RepeatDirection="Horizontal" DataKeyField="Name">
                            <ItemStyle horizontalalign="Center" verticalalign="Bottom"></ItemStyle>
                            <ItemTemplate>
                                <table id="tblImage" cellpadding="0" cellspacing="1" border="0" runat="server">
                                    <tr>
                                        <td class="TTTRow" align="center" valign="top" width="100%">
                                            <asp:imagebutton id="Smiley" ImageUrl="<%# Ctype(Container.DataItem, GalleryFile).URL %>" visible="<%# Not Ctype(Container.DataItem, GalleryFile).IsFolder %>" CommandArgument="<%# Ctype(Container.DataItem, GalleryFile).Title %>" AlternateText="<%# Ctype(Container.DataItem, GalleryFile).Title %>" Runat="Server" BorderWidth="0" BorderStyle="none"></asp:imagebutton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TTTRow" align="center" valign="top">
                                            <asp:Label id="Decription" runat="server" cssclass="TTTNormal"> <%# Ctype(Container.DataItem, GalleryFile).Description %> </asp:Label> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:datalist>
                    </td>
                </tr>
                <tr>
                    <td class="TTTAltHeader" align="center" width="100%">
                        <asp:Label id="lblInfo" runat="server" cssclass="TTTNormal" forecolor="red"></asp:Label></td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>