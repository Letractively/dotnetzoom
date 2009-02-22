<%@ Control Language="VB" Inherits="DotNetZoom.PortalModuleControl"%>
<%@ Import Namespace="DotNetZoom" %>


<script runat="server">
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    </script>
		<SCRIPT type="text/javascript" language="JavaScript"><!--
	var answer = confirm("<%= DotNetZoom.RTESafe(DotNetZoom.GetLanguage("nossl")) %>")
	if (answer){
	} else
	{
	window.location = "<%= Replace(HttpContext.Current.Items("RequestURL"), "https://", "http://") %>";
	}
		--></SCRIPT>
