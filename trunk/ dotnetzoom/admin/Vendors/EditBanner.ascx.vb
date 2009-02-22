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

    Public Class EditBanner
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents txtBannerName As System.Web.UI.WebControls.TextBox
        Protected WithEvents valBannerName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents cboBannerType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cboImage As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpload As System.Web.UI.WebControls.HyperLink
        Protected WithEvents txtURL As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtImpressions As System.Web.UI.WebControls.TextBox
        Protected WithEvents valImpressions As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents compareImpressions As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents txtCPM As System.Web.UI.WebControls.TextBox
        Protected WithEvents valCPM As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents compareCPM As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents txtStartDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtEndDate As System.Web.UI.WebControls.TextBox

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton

        Protected WithEvents pnlAudit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblCreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents lblCreatedDate As System.Web.UI.WebControls.Label
        Protected WithEvents lblViews As System.Web.UI.WebControls.Label
        Protected WithEvents lblClickThroughs As System.Web.UI.WebControls.Label
        Protected WithEvents chkLog As System.Web.UI.WebControls.CheckBox
        Protected WithEvents grdLog As System.Web.UI.WebControls.DataGrid
        Protected WithEvents cmdStartCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEndCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents valStartDate As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents valEndDate As System.Web.UI.WebControls.CompareValidator
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

		
        Private VendorId As Integer = -1
        Private BannerId As Integer = -1

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
			Title1.DisplayHelp = "DisplayHelp_EditBaners"
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			valBannerName.ErrorMessage = GetLanguage("need_banner_name")
			valImpressions.ErrorMessage = GetLanguage("need_exposition_number")
			compareImpressions.ErrorMessage = GetLanguage("need_exposition_number")
			valCPM.ErrorMessage = GetLanguage("need_CPM_number")
			compareCPM.ErrorMessage = GetLanguage("need_CPM_number")
			valStartDate.ErrorMessage = "<br>" + GetLanguage("not_a_date")
			valEndDate.ErrorMessage = "<br>" + GetLanguage("not_a_date")
			cmdUpload.Text = Getlanguage("upload")
			cmdEndCalendar.Text = Getlanguage("calendar")
			cmdStartCalendar.Text = Getlanguage("calendar")
			cmdUpdate.Text = Getlanguage("enregistrer")
			cmdCancel.Text = Getlanguage("annuler")
			cmdDelete.Text = Getlanguage("delete")
            If IsNumeric(Request.Params("VendorId")) Then
                VendorId = Int32.Parse(Request.Params("VendorId"))
            End If

            If IsNumeric(Request.Params("BannerId")) Then
                BannerId = Int32.Parse(Request.Params("BannerId"))
            End If

            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If

                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm")) & "');")
                cmdStartCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtStartDate)
                cmdEndCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtEndDate)

                Dim objVendor As New VendorsDB()

                ' Get the banner types from the database
 				
				Dim Item As New ListItem()
				
				Item.Value = 1
				Item.Text = GetLanguage("Vendor_Banner1")
 				cboBannerType.Items.Add(item)
				Item = New ListItem
				Item.Value = 2
				Item.Text = GetLanguage("Vendor_Banner2")
 				cboBannerType.Items.Add(item)
				Item = New ListItem
				Item.Value = 3
				Item.Text = GetLanguage("Vendor_Banner3")
 				cboBannerType.Items.Add(item)
				Item = New ListItem
				Item.Value = 4
				Item.Text = GetLanguage("Vendor_Banner4")
 				cboBannerType.Items.Add(item)
				Item = New ListItem
				Item.Value = 5
				Item.Text = GetLanguage("Vendor_Banner5")
 				cboBannerType.Items.Add(item)
				
				
				
                ' load the list of files found in the upload directory
                cmdUpload.NavigateUrl = GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&def=Gestion fichiers" & IIf(Not (Request.Params("hostpage") Is Nothing), "&hostpage=", "")
                Dim FileList As ArrayList
                If Not (Request.Params("hostpage") Is Nothing) Then
                    FileList = GetFileList(, glbImageFileTypes, False)
                Else
                    FileList = GetFileList(_portalSettings.PortalId, glbImageFileTypes, False)
                End If
                cboImage.DataSource = FileList
                cboImage.DataBind()

                If BannerId <> -1 Then

                    ' Obtain a single row of banner information
                    Dim dr As SqlDataReader = objVendor.GetSingleBanner(BannerId, VendorId)

                    If dr.Read() Then
                        txtBannerName.Text = CType(dr("BannerName"), String)
                        cboBannerType.Items.FindByValue(CType(dr("BannerTypeId"), String)).Selected = True
                        If cboImage.Items.Contains(New ListItem(CType(dr("ImageFile").ToString, String))) Then
                            cboImage.Items.FindByText(CType(dr("ImageFile"), String)).Selected = True
                        End If
                        If Not IsDBNull(dr("URL")) Then
                            txtURL.Text = CType(dr("URL"), String)
                        End If
                        txtImpressions.Text = CType(dr("Impressions"), String)
                        txtCPM.Text = CType(dr("CPM"), String)
                        If Not IsDBNull(dr("StartDate")) Then
                            txtStartDate.Text = Format(CDate(dr("StartDate")), "yyyy-MM-dd")
                        End If
                        If Not IsDBNull(dr("EndDate")) Then
                            txtEndDate.Text = Format(CDate(dr("EndDate")), "yyyy-MM-dd")
                        End If
                        lblCreatedBy.Text = dr("CreatedByUser").ToString
                        lblCreatedDate.Text = CType(dr("CreatedDate"), DateTime).ToShortDateString()
                        lblViews.Text = dr("Views").ToString
                        lblClickThroughs.Text = dr("ClickThroughs").ToString

                        dr.Close()
                    Else ' security violation attempt to access item not related to this Module
                        dr.Close()
                        Response.Redirect(GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&" & GetAdminPage() & "&VendorId=" & VendorId & "&def=Fournisseurs", True)
                    End If

                    chkLog.Checked = False
                    grdLog.Visible = False
                Else
                    txtImpressions.Text = "0"
                    txtCPM.Text = "0"

                    cmdDelete.Visible = False
                    pnlAudit.Visible = False
                End If

                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&" & GetAdminPage() & "&VendorId=" & VendorId & "&def=Fournisseurs"

            End If

        End Sub


        '****************************************************************
        '
        ' The cmdUpdate_Click event handler on this Page is used to either
        ' create or update a banner.  It uses the DotNetZoom.BannerDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Page.Validate()

            ' Only Update if the Entered Data is val
            If Page.IsValid = True Then

                ' Create an instance of the Banner DB component
                Dim objVendor As New VendorsDB()

                If BannerId = -1 Then
                    ' Add the banner within the Banners table
                    objVendor.AddBanner(txtBannerName.Text, VendorId, cboImage.SelectedItem.Text, txtURL.Text, Integer.Parse(txtImpressions.Text), Double.Parse(txtCPM.Text), CheckDateSql(txtStartDate.Text), CheckDateSql(txtEndDate.Text), Context.User.Identity.Name, cboBannerType.SelectedItem.Value)
                Else
                    ' Update the banner within the Banners table
                    objVendor.UpdateBanner(BannerId, txtBannerName.Text, cboImage.SelectedItem.Text, txtURL.Text, Integer.Parse(txtImpressions.Text), Double.Parse(txtCPM.Text), CheckDateSql(txtStartDate.Text), CheckDateSql(txtEndDate.Text), Context.User.Identity.Name, cboBannerType.SelectedItem.Value)
                End If

                ' Redirect back to the portal home page
                Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

            End If

        End Sub


        '****************************************************************
        '
        ' The cmdDelete_Click event handler on this Page is used to delete an
        ' a banner.  It  uses the DotNetZoom.BannerDB() data component to
        ' encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click

            If BannerId <> -1 Then
                Dim objVendor As New VendorsDB()
                objVendor.DeleteBanner(BannerId)

                ' Redirect back to the portal home page
                Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
            End If

        End Sub


        '****************************************************************
        '
        ' The cmdCancel_Click event handler on this Page is used to cancel
        ' out of the page, and return the user back to the portal home
        ' page.
        '
        '****************************************************************'

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

        Private Sub chkLog_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLog.CheckedChanged

            If chkLog.Checked Then
                grdLog.Visible = True
            Else
                grdLog.Visible = False
            End If

            BindData()
        End Sub

        Private Sub BindData()
            Dim objVendor As New VendorsDB()

            grdLog.DataSource = objVendor.GetBannerLog(BannerId)
            grdLog.DataBind()
			
			grdLog.Columns(0).HeaderText = GetLanguage("F_PostDate")
			grdLog.Columns(1).HeaderText = GetLanguage("Vendor_Banner_Imp")
        End Sub

    End Class

End Namespace