<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TAGFileUploadDialog.aspx.vb"
    Inherits="DotNetZoom.TAGFileUploadDialog" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <title>
        <%= DotNetZoom.GetLanguage("upload") %>
        <%= Session("RelativeDir") %></title>
    <asp:Literal ID="StyleSheet" runat="server"></asp:Literal>
    <link href="/admin/files/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        !window.jQuery && document.write('<script src="\/javascript\/jquery.min.js"><\/script>');
    </script>
    <script type="text/javascript" src="/javascript/swfobject.js"></script>
    <script type="text/javascript" src="/javascript/jquery.uploadify.v2.1.4.js"></script>
    <script type="text/javascript" language="JavaScript"><!--
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
		--></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="Content-Script-Type" content="text/javascript">
</head>
<body onload="block()" onunload="unblock()">
    <form id="Form1" method="post" enctype="multipart/form-data" runat="server">
    <div class="master">
    <div class="main">
            <p>
                <%= DotNetZoom.GetLanguage("F_Directory") %>
                : &nbsp;&nbsp;<%= Replace(Session("RelativeDir"), "\", "/") & "/" %></p>
            <asp:literal ID="lblMessage" runat="server" EnableViewState="False"></asp:Literal>
      </div>      
    <asp:PlaceHolder ID="CanUpload" runat="server">
        <div class="main">
           <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
           <div class="main">
                <a href="#" class="CommandButton" id="startUploadLink">
                    <%= DotNetZoom.GetLanguage("upload")%></a>&nbsp; |&nbsp; <a href="#" class="CommandButton"
                        id="clearQueueLink">
                        <%= DotNetZoom.GetLanguage("cmdRemove")%></a>&nbsp; |&nbsp; <a href="javascript:handleOK();"
                            class="CommandButton">
                            <%= DotNetZoom.GetLanguage("return")%></a>&nbsp;
            <p>
                <asp:CheckBox ID="chkUnzip" runat="server" CssClass="Normal" TextAlign="Left"></asp:CheckBox>
            </p>
            <asp:Literal ID="InfoText" runat="server"></asp:Literal>
        </div>
        <script type="text/javascript">
    oO = [];
    oO.queue = '<%=DotNetZoom.RTESafe(GetMaxQueue)%>';
    oO.c = '<%=DotNetZoom.RTESafe(DotNetZoom.GetLanguage("OK"))%>';
    oO.r = '<%=DotNetZoom.RTESafe(DotNetZoom.GetLanguage("ReplaceFile"))%> ';
    oO.e = ' <%=DotNetZoom.RTESafe(DotNetZoom.GetLanguage("error"))%>';
    oO.fs = ' <%=DotNetZoom.RTESafe(GetFileMaxSize)%>';

    $(document).ready(function () {
        $("#<%=FileUpload1.ClientID%>").uploadify({
            'uploader': '/javascript/uploadify.swf',
            'script': '/admin/files/UploadVB.ashx<%= GetTabId %>',
            'checkScript' : '/admin/files/CheckVB.ashx<%= GetTabId %>',
            'cancelImg': '/images/cancel.gif',
            'folder': '<%=GetPathCrypTo%>',
            'queueSizeLimit': <%=GetQueueLimit%>,
            'buttonText': '<%=DotNetZoom.RTESafe(DotNetZoom.GetLanguage("label_SelectFile"))%>',
            'fileExt': '<%=GetFileType%>',
            'fileDesc': '<%=GetFileType%>',
            'sizeLimit': '<%=SetMaxSize%>',
            'multi': '<%=GetMulti%>'
        });

        $("#startUploadLink").click(function () {
            $('#<%=FileUpload1.ClientID%>').uploadifyUpload();
            return false;
        });

        $("#clearQueueLink").click(function () {
            $("#<%=FileUpload1.ClientID%>").uploadifyClearQueue();
            return false;
        });
    });

        </script>
</div>
    </asp:PlaceHolder>
    </form>
</body>
</html>
