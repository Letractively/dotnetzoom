'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2008
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

    Public MustInherit Class EditBanners
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents optSource As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents cboType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtCount As System.Web.UI.WebControls.TextBox
		Protected WithEvents valCount As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton

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

            Title1.DisplayHelp = "DisplayHelp_EditBanners"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			
            If Not Page.IsPostBack Then

                ' Obtain banner information from the Banners table and bind to the list control
                Dim objVendor As New VendorsDB()

								Dim Item As New ListItem()
				
				Item.Value = 1
				Item.Text = GetLanguage("Vendor_Banner1")
 				cboType.Items.Add(item)
				Item = New ListItem
				Item.Value = 2
				Item.Text = GetLanguage("Vendor_Banner2")
 				cboType.Items.Add(item)
				Item = New ListItem
				Item.Value = 3
				Item.Text = GetLanguage("Vendor_Banner3")
 				cboType.Items.Add(item)
				Item = New ListItem
				Item.Value = 4
				Item.Text = GetLanguage("Vendor_Banner4")
 				cboType.Items.Add(item)
				Item = New ListItem
				Item.Value = 5
				Item.Text = GetLanguage("Vendor_Banner5")
 				cboType.Items.Add(item)

			optSource.Items.FindByValue("G").Text = getlanguage("banners_host")
			optSource.Items.FindByValue("L").Text = getlanguage("banners_portal")
			valCount.ErrorMessage = getlanguage("need_number")
			cmdUpdate.Text = getLanguage("enregistrer")
			cmdCancel.Text = getlanguage("annuler")
                If ModuleId > 0 Then

                    ' Get settings from the database
                    Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)

                    If Not optSource.Items.FindByValue(CType(settings("bannersource"), String)) Is Nothing Then
                        optSource.Items.FindByValue(CType(settings("bannersource"), String)).Selected = True
                    End If
                    If Not cboType.Items.FindByValue(CType(settings("bannertype"), String)) Is Nothing Then
                        cboType.Items.FindByValue(CType(settings("bannertype"), String)).Selected = True
                    End If
                    txtCount.Text = CType(settings("bannercount"), String)

                End If


                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = GetFullDocument() & "?tabid=" & TabId

            End If

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            ' Update settings in the database
            Dim admin As New AdminDB()

            If Not optSource.SelectedItem Is Nothing Then
                admin.UpdateModuleSetting(ModuleId, "bannersource", optSource.SelectedItem.Value)
            End If
            If Not cboType.SelectedItem Is Nothing Then
                admin.UpdateModuleSetting(ModuleId, "bannertype", cboType.SelectedItem.Value)
            End If
            admin.UpdateModuleSetting(ModuleId, "bannercount", txtCount.Text)

            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

    End Class

End Namespace