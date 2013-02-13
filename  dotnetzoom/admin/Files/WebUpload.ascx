<%@ Control Language="vb" codebehind="WebUpload.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.WebUpload" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>

<asp:placeHolder id="Upload" runat="server">
<table cellspacing="0" cellpadding="0" >
    <tbody>
	  <asp:placeHolder id="CanUpload" runat="server">
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
		<td class="SubHead" align="left" colspan="2"><%= DotNetZoom.GetLanguage("F_UploadInDirec") %>
        <asp:Label id="lblRootDir" runat="server" cssclass="Normal"></asp:Label></td>
        <td>
        </td>
		</tr>
        <tr><td colspan="2">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        !window.jQuery && document.write('<script src="javascript\/jquery.min.js"><\/script>');
    </script>
	<script type="text/javascript" src="/javascript/swfobject.js"></script>
	<script type="text/javascript" src="/javascript/jquery.uploadify.v2.1.4.js"></script> 
           <div class="demo">
                <asp:FileUpload ID="FileUpload1" runat="server"/>
				<br />
				<a href="#" Class="CommandButton" id="startUploadLink"><%= DotNetZoom.GetLanguage("upload")%></a>&nbsp; |&nbsp;
				<a href="#" Class="CommandButton" id="clearQueueLink"><%= DotNetZoom.GetLanguage("cmdRemove")%></a>&nbsp; |&nbsp;
                <asp:HyperLink id="cmdCancel" runat="server" Cssclass="CommandButton"></asp:Hyperlink>
                <p>
                <asp:CheckBox id="chkUnzip" autopostback=true Runat="server" CssClass="Normal" Textalign="Right"></asp:CheckBox>
                </p>
            </div>
     </td></tr>
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
        


 		</asp:placeHolder>
        <tr>
            <td align="justify" colspan="2">
                <asp:Label id="lblMessage" runat="server" cssclass="NormalRed" width="400" enableviewstate="False"></asp:Label></td>
        </tr>
    </tbody>
</table>
</asp:placeHolder>
<asp:placeHolder id="Options" runat="server">
<table align="center">
<tr>
<td colspan="2">
&nbsp;</td>
</tr>
<tr>
<td>
<asp:linkbutton id="cmdSynchronize" runat="server" Cssclass="CommandButton"></asp:linkbutton>
<br><br>
<asp:Label id="lblDiskSpace" runat="server" enableviewstate="False" cssclass="Normal"></asp:Label>
<br>
<br>
<span class="SubHead"><label for="<%=chkUploadRoles.ClientID%>"><%= DotNetZoom.GetLanguage("F_Upload_Auth") %>&nbsp;:&nbsp;</label></span>
<br>
<br>
<asp:checkboxlist id="chkUploadRoles" runat="server" width="600" Font-Size="8pt" Font-Names="Verdana,Arial" cellspacing="0" cellpadding="0" RepeatColumns="2"></asp:checkboxlist>
<br>
<asp:linkbutton cssclass="CommandButton" id="cmdUpdate" runat="server" ></asp:linkbutton>
&nbsp;&nbsp;<asp:linkbutton cssclass="CommandButton" id="cmdCancelOptions" runat="server"></asp:linkbutton>
</td>
</tr>
</table>
</asp:placeHolder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>