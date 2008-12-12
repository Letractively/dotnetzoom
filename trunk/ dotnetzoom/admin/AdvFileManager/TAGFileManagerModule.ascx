<%@ Control Inherits="DotNetZoom.TAGFileManagerModule" codebehind="TAGFileManagerModule.ascx.vb" Language="vb" autoeventwireup="false" Explicit="True" %>
<%@ Register TagPrefix="TAG" TagName="TAGAdvFileManager" Src="TAGAdvFileManager.ascx" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<TAG:TAGAdvFileManager id="TAGAdvFileManager1" runat="server"></TAG:TAGAdvFileManager>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>