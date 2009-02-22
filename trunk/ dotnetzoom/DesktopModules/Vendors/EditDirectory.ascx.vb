'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
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

Namespace DotNetZoom

    Public MustInherit Class EditDirectory
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents optSource As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents chkFeedback As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkSignup As System.Web.UI.WebControls.CheckBox

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

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

        '*******************************************************
        '
        ' The Page_Load event handler on this User Control is used to
        ' obtain a DataReader of banner information from the Banners
        ' table, and then databind the results to a templated DataList
        ' server control.  It uses the DotNetZoom.BannerDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************'

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Title1.DisplayHelp = "DisplayHelp_EditDirectory"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			optSource.Items.FindByValue("G").Text = getlanguage("banners_host")
			optSource.Items.FindByValue("L").Text = getlanguage("banners_portal")
			cmdUpdate.Text = getLanguage("enregistrer")
			cmdCancel.Text = getlanguage("annuler")

            If Not Page.IsPostBack Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If
                If ModuleId > 0 Then

                    ' Get settings from the database
                    Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)

                    If Not optSource.Items.FindByValue(CType(settings("directorysource"), String)) Is Nothing Then
                        optSource.Items.FindByValue(CType(settings("directorysource"), String)).Selected = True
                    End If
                    If CType(settings("directoryfeedback"), String) <> "" Then
                        chkFeedback.Checked = CType(settings("directoryfeedback"), Boolean)
                    End If
                    If CType(settings("directorysignup"), String) <> "" Then
                        chkSignup.Checked = CType(settings("directorysignup"), Boolean)
                    End If

                End If

            End If

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            ' Update settings in the database
            Dim admin As New AdminDB()

            If Not optSource.SelectedItem Is Nothing Then
                admin.UpdateModuleSetting(ModuleId, "directorysource", optSource.SelectedItem.Value)
            End If
            admin.UpdateModuleSetting(ModuleId, "directoryfeedback", chkFeedback.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "directorysignup", chkSignup.Checked.ToString)
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

    End Class

End Namespace