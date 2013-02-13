'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by Ren� Boulard ( http://www.reneboulard.qc.ca)'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
'
Imports System.IO
Imports System.Net

Namespace DotNetZoom

    Public MustInherit Class Announcements

        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents AnnData As Announcement
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents before As System.Web.UI.WebControls.Literal
        Protected WithEvents after As System.Web.UI.WebControls.Literal
        Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents ajax As System.Web.UI.HtmlControls.HtmlGenericControl


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            JQueryScript(Me.Page)
            AnnData.ModuleID = ModuleId
            AnnData.TabId = TabId
            AnnData.ModuleTitle = ModuleConfiguration.ModuleTitle
            AnnData.IsEditable = IsEditable
            AnnData.AjaxID = ajax.ClientID
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim Tsettings As Hashtable = PortalSettings.GetSiteSettings(_portalSettings.PortalId)
            If Tsettings.ContainsKey("PrivateKey") Then
                If CType(Tsettings("PrivateKey"), String) <> "" Then
                    AnnData.Captcha = Not Request.IsAuthenticated
                End If
            End If
            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"
            If IsNumeric(Request.Params("add")) Then
                Title1.DisplayHelp = "DisplayHelp_ReCapchat_" + (Not Request.IsAuthenticated).ToString
                Title1.DisplayTitle = GetLanguage("AddComment")
            Else
                Title1.DisplayHelp = Nothing
                Title1.DisplayTitle = Nothing
            End If

        End Sub
    End Class

End Namespace