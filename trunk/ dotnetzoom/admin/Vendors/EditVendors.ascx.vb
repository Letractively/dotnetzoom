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
    Public Class EditVendors
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents Title1 As DesktopModuleTitle

        ' module options
        Protected WithEvents txtInstructions As System.Web.UI.WebControls.TextBox
        Protected WithEvents cboRoles As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtMessage As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdCancelOptions As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdUpdateOptions As System.Web.UI.WebControls.LinkButton
		Protected WithEvents pnlOptions As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents pnlContent As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents ClassificationsEdit As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents lblInstructions As System.Web.UI.WebControls.Label
        Protected WithEvents txtVendorName As System.Web.UI.WebControls.TextBox
        Protected WithEvents valVendorName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtFirstName As System.Web.UI.WebControls.TextBox
        Protected WithEvents valFirstName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtLastName As System.Web.UI.WebControls.TextBox
        Protected WithEvents valLastName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Address1 As Address
        Protected WithEvents txtFax As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
		Protected WithEvents valEmail As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtWebsite As System.Web.UI.WebControls.TextBox
        Protected WithEvents rowVendor1 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents cboLogo As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpload As System.Web.UI.WebControls.HyperLink
        Protected WithEvents rowVendor2 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents chkAuthorized As System.Web.UI.WebControls.CheckBox
        Protected WithEvents colVendor As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents txtKeyWords As System.Web.UI.WebControls.TextBox

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
		Protected WithEvents cmdAddName As System.Web.UI.WebControls.LinkButton
		Protected WithEvents txtAddName As System.Web.UI.WebControls.TextBox
		
        Protected WithEvents pnlAudit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblCreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents lblCreatedDate As System.Web.UI.WebControls.Label
        Protected WithEvents lblViews As System.Web.UI.WebControls.Label
        Protected WithEvents lblClickThroughs As System.Web.UI.WebControls.Label
        Protected WithEvents chkLog As System.Web.UI.WebControls.CheckBox
        Protected WithEvents grdLog As System.Web.UI.WebControls.DataGrid

        Protected WithEvents rowBanner1 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents rowBanner2 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents cmdAddBanner As System.Web.UI.WebControls.LinkButton
        Protected WithEvents grdBanners As System.Web.UI.WebControls.DataGrid
        Protected WithEvents cmdBack As System.Web.UI.WebControls.LinkButton
		Protected WithEvents ddlLanguage As System.Web.UI.WebControls.DropDownList
		Protected WithEvents grdClassifications As System.Web.UI.WebControls.DataGrid
		Protected WithEvents grdEditClassifications As System.Web.UI.WebControls.DataGrid
		
        Public VendorID As Integer = -1

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

		Title1.DisplayHelp = "DisplayHelp_EditVendors"
        ' Obtain PortalSettings from Current Context
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		
		valVendorName.ErrorMessage = "<br>" + GetLanguage("need_vendor_name")
		valFirstName.ErrorMessage =  "<br>" + GetLanguage("need_firstname")
		valLastName.ErrorMessage =  "<br>" + GetLanguage("need_lastname")
		valEmail.ErrorMessage =  "<br>" + GetLanguage("need_email")
		cmdUpdateOptions.Text = Getlanguage("enregistrer")
		cmdCancelOptions.Text = Getlanguage("annuler")
		cmdUpload.Text = GetLanguage("upload")
		
		cmdCancel.Text = GetLanguage("annuler")
		cmdDelete.Text = GetLanguage("delete")
		cmdAddBanner.Text = GetLanguage("Vendor_Banner_Add")
		cmdBack.Text = GetLanguage("return")
		cmdAddName.Text = GetLanguage("Vendors_AddName")
		If request.params("options") <> "" then
		If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = true then
		pnlOptions.Visible = True
		pnlContent.Visible = False
		If PortalSecurity.IsSuperUser Then
		ClassificationsEdit.Visible = true
		end if
		else
    	Response.Redirect("~" & GetDocument() & "?edit="  & _portalSettings.ActiveTab.TabId &    "&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Edit Access Denied", True)
 		end if
		else
		pnlOptions.Visible = false
		pnlContent.Visible = True
		end if
		

		
        Dim objAdmin As New AdminDB()
		
		If Page.IsPostBack = False Then
		Dim TempAuthLanguage As String = ""
		If portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey("languageauth") then
		TempAuthLanguage = portalSettings.GetSiteSettings(_portalSettings.PortalID)("languageauth")
		else
		TempAuthLanguage = GetLanguage("N") & ";"
		objadmin.UpdatePortalSetting(_PortalSettings.PortalId, "languageauth", TempAuthLanguage)
		end if 

                ' Language to Use
                Dim HashL As Hashtable = objAdmin.GetAvailablelanguage
                For Each de As DictionaryEntry In HashL
                    Dim itemL As New ListItem()
                    itemL.Text = de.Value
                    itemL.Value = de.Key
                    If InStr(1, TempAuthLanguage, itemL.Value & ";") Then
                        ddlLanguage.Items.Add(itemL)
                    End If
                Next de


	 	If Not ddlLanguage.Items.FindByText(GetLanguage("language")) Is Nothing then
			ddlLanguage.Items.FindByText(GetLanguage("language")).Selected = True
		else 
       		ddlLanguage.SelectedIndex = 0     
		End If
				
		If ddlLanguage.Items.Count = 1 then
		   ddlLanguage.visible = False
		else
		   ddlLanguage.visible = True
		end if
		end if
		' end language
         Dim blnBanner As Boolean = False
         Dim blnSignup As Boolean = False

         Dim dr As SqlDataReader


            If IsNumeric(Request.Params("VendorID")) Then
                VendorID = Int32.Parse(Request.Params("VendorID"))
            End If

            If Not Request.Params("def") Is Nothing And VendorID = -1 Then
                blnSignup = True
            End If

            If Not Request.Params("banner") Is Nothing Then
                blnBanner = True
            End If
           
            If Page.IsPostBack = False Then
				ViewState("UrlReferrer") = "~" & GetDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId
                Address1.ModuleId = -2
                Address1.StartTabIndex = 4

                Dim objVendor As New VendorsDB()

                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm")) & "');")

                ' load the list of files found in the upload directory
                cmdUpload.NavigateUrl = "~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Gestion fichiers" & IIf(Not (Request.Params("hostpage") Is Nothing), "&hostpage=" , "")
                Dim FileList As ArrayList
                If Not (Request.Params("hostpage") Is Nothing) Then
                    FileList = GetFileList(, glbImageFileTypes)
                Else
                    FileList = GetFileList(_portalSettings.PortalId, glbImageFileTypes)
                End If
                cboLogo.DataSource = FileList
                cboLogo.DataBind()

				grdClassifications.DataSource =  objVendor.GetVendorClassifications(VendorID)
				grdClassifications.DataBind()
				
				grdEditClassifications.DataSource =  objVendor.GetVendorClassifications(-1)
				grdEditClassifications.DataBind() 
				
                If VendorID <> -1 Then

                    dr = objVendor.GetSingleVendor(VendorID)
                    If dr.Read() Then
                        txtVendorName.Text = dr("VendorName").ToString
                        txtFirstName.Text = dr("FirstName").ToString
                        txtLastName.Text = dr("LastName").ToString
                        If cboLogo.Items.Contains(New ListItem(CType(dr("LogoFile").ToString, String))) Then
                            cboLogo.Items.FindByText(CType(dr("LogoFile"), String)).Selected = True
                        End If
                        Address1.Unit = dr("Unit").ToString
                        Address1.Street = dr("Street").ToString
                        Address1.City = dr("City").ToString
                        Address1.Region = dr("Region").ToString
                        Address1.Country = dr("Country").ToString
                        Address1.Postal = dr("PostalCode").ToString
                        Address1.Telephone = dr("Telephone").ToString
                        txtFax.Text = dr("Fax").ToString
                        txtEmail.Text = dr("Email").ToString
                        txtWebsite.Text = dr("Website").ToString
                        chkAuthorized.Checked = dr("Authorized")
                        txtKeyWords.Text = dr("KeyWords").ToString

                        lblCreatedBy.Text = dr("CreatedByUser").ToString
                        lblCreatedDate.Text = CType(dr("CreatedDate"), DateTime).ToShortDateString()
                        lblViews.Text = dr("Views").ToString
                        lblClickThroughs.Text = dr("ClickThroughs").ToString
                    End If
					dr.Close()
                    chkLog.Checked = False
                    grdLog.Visible = False

                    BindData()
                Else
                    chkAuthorized.Checked = True
                    pnlAudit.Visible = False
                    cmdDelete.Visible = False
                    rowBanner1.Visible = False
                    rowBanner2.Visible = False
                End If

                
                txtInstructions.Text = CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)(ddlLanguage.SelectedItem.Value & "_instructions"), String)
                Dim objUser As New UsersDB()
                cboRoles.DataSource = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))
                cboRoles.DataBind()
                cboRoles.Items.Insert(0, New ListItem(getlanguage("list_none"), ""))
                If CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)("roleid"), String) <> "" Then
                    cboRoles.Items.FindByValue(CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)("roleid"), String)).Selected = True
                End If
                txtMessage.Text = CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)(ddlLanguage.SelectedItem.Value & "_message"), String)

                If blnSignup = True Or blnBanner = True Then
                    rowVendor1.Visible = False
                    rowVendor2.Visible = False
                    colVendor.Visible = False
                    cmdDelete.Visible = False
                    pnlAudit.Visible = False
                    rowBanner1.Visible = False
                    rowBanner2.Visible = False

                    If blnBanner = True Then
                        cmdUpdate.Visible = False
                    Else
                        Title1.DisplayTitle = GetLanguage("Vendor_Register")
                        If txtInstructions.Text <> "" Then
                            lblInstructions.Text = txtInstructions.Text
                            lblInstructions.Visible = True
                        End If
                        cmdUpdate.Text = GetLanguage("banner_register") 
                    End If

                    If Not Request.UrlReferrer Is Nothing Then
                       ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                    Else
                        ViewState("UrlReferrer") = ""
                    End If
                Else
                ViewState("UrlReferrer") = "~" & GetDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId &  "&" & GetAdminPage()& "&filter=" & Request.Params("filter")
                cmdUpdate.Text = GetLanguage("enregistrer")
				End If
            End If

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            Dim intPortalID As Integer
            Dim strLogoFile As String = ""

            If Page.IsValid Then

                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                If Not (Request.Params("hostpage") Is Nothing) Then
                    intPortalID = -1
                Else
                    intPortalID = PortalId
                End If

                If Not cboLogo.SelectedItem Is Nothing Then
                    If cboLogo.SelectedIndex <> 0 Then
                        strLogoFile = cboLogo.SelectedItem.Value
                    End If
                End If

                Dim objVendor As New VendorsDB()

                If VendorID = -1 Then
                    VendorID = objVendor.AddVendor(intPortalID, txtVendorName.Text, Address1.Unit, Address1.Street, Address1.City, Address1.Region, Address1.Country, Address1.Postal, Address1.Telephone, txtFax.Text, txtEmail.Text, txtWebsite.Text, txtFirstName.Text, txtLastName.Text, Context.User.Identity.Name, strLogoFile, txtKeyWords.Text, chkAuthorized.Checked.ToString, VendorID)
                    Viewstate("UrlReferrer") = Replace(CType(Viewstate("UrlReferrer"), String), "&filter=", "&filter=" & Left(txtVendorName.Text, 1))
                Else
                    objVendor.UpdateVendor(VendorID, txtVendorName.Text, Address1.Unit, Address1.Street, Address1.City, Address1.Region, Address1.Country, Address1.Postal, Address1.Telephone, txtFax.Text, txtEmail.Text, txtWebsite.Text, txtFirstName.Text, txtLastName.Text, Context.User.Identity.Name, strLogoFile, txtKeyWords.Text, chkAuthorized.Checked.ToString)
                End If

                ' update vendor classifications
                objVendor.DeleteVendorClassifications(VendorID)

				
			    Dim ItemCollection As DataGridItemCollection
    	        Dim CurrentItem As DataGridItem

	            ItemCollection = grdClassifications.Items
				Dim ClsValue As Integer
        	    For Each CurrentItem In ItemCollection
            	    If CurrentItem.ItemType = ListItemType.AlternatingItem Or CurrentItem.ItemType = ListItemType.Item Then
                	    Dim chkClassificationName As HtmlInputCheckBox
                    	chkClassificationName = CType(CurrentItem.Cells(0).Controls(1), HtmlInputCheckBox)
						ClsValue = Int32.Parse(chkClassificationName.Value)
                    		If chkClassificationName.Checked Then
								objVendor.AddVendorClassification(VendorID, ClsValue)
                    		End If
                	End If
            	Next

				
				
				
				
                If cmdUpdate.Text = GetLanguage("banner_register") Then
                    Dim strBody As String = ""
                    strBody = strBody & GetLanguage("label_message_date") & ": " & ProcessLanguage("{date}") & vbCrLf & vbCrLf
                    strBody = strBody & GetLanguage("Vendor_Name") & ": " & txtVendorName.Text & vbCrLf
                    strBody = strBody & GetLanguage("S_F_Name") & ": " & txtFirstName.Text & vbCrLf
                    strBody = strBody & GetLanguage("S_F_LastName") & ": " & txtLastName.Text & vbCrLf & vbCrLf
                    strBody = strBody & GetLanguage("address_app") & ": " & Address1.Unit & vbCrLf
                    strBody = strBody & GetLanguage("address_street") & ": " & Address1.Street & vbCrLf
                    strBody = strBody & GetLanguage("address_city") & ": " & Address1.City & vbCrLf
                    strBody = strBody & GetLanguage("address_region") & ": " & Address1.Region & vbCrLf
                    strBody = strBody & GetLanguage("address_country") & ": " & Address1.Country & vbCrLf
                    strBody = strBody & GetLanguage("address_postal_code") & ": " & Address1.Postal & vbCrLf
                    strBody = strBody & GetLanguage("address_telephone") & ": " & Address1.Telephone & vbCrLf & vbCrLf
                    strBody = strBody & GetLanguage("address_fax") & ": " & txtFax.Text & vbCrLf
                    strBody = strBody & GetLanguage("address_Email") & ": " & txtEmail.Text & vbCrLf
                    strBody = strBody & GetLanguage("address_WebSite") & ": " & txtWebsite.Text & vbCrLf

                    SendNotification(txtEmail.Text, _portalSettings.Email, "", _portalSettings.PortalName & " " & GetLanguage("Vendor_request") , strBody)

                    strBody = txtFirstName.Text & " " & txtLastName.Text & "," & vbCrLf & vbCrLf
                    strBody = strBody & GetLanguage("Vendor_ThankYou") & vbCrLf & vbCrLf
                    strBody = strBody & txtMessage.Text & vbCrLf & vbCrLf
                    strBody = strBody & _portalSettings.PortalName

                    SendNotification(_portalSettings.Email, txtEmail.Text, "", " " & GetLanguage("Vendor_request") & " " & _portalSettings.PortalName , strBody)

                    If cboRoles.SelectedItem.Value <> "" Then
                        Response.Redirect("~/admin/Sales/PayPalSubscription.aspx?tabid=" & _portalSettings.ActiveTab.TabId & "&roleid=" & cboRoles.SelectedItem.Value, True)
                    Else
                        Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)
                    End If
                Else
                    Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)
                End If

            End If

        End Sub

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
            If VendorID <> -1 Then
                Dim objVendor As New VendorsDB()
                objVendor.DeleteVendor(VendorID)
            End If

            Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)
        End Sub

        Private Sub cmdAddBanner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddBanner.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&" & GetAdminPage() & "&VendorId=" & VendorID & "&def=Banner", True)
        End Sub

        Private Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
            Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)
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


			grdLog.Columns(0).HeaderText = GetLanguage("Vendor_Header_Search")
			grdLog.Columns(1).HeaderText = GetLanguage("Vendor_Header_Request")
			grdLog.Columns(2).HeaderText = GetLanguage("Vendor_Header_LastRequest")

            grdLog.DataSource = objVendor.GetVendorLog(VendorID)
            grdLog.DataBind()

			grdBanners.Columns(1).HeaderText = GetLanguage("Vendor_Banner1")
			grdBanners.Columns(2).HeaderText = GetLanguage("Vender_Type")
			grdBanners.Columns(3).HeaderText = GetLanguage("Vender_URL")
			grdBanners.Columns(4).HeaderText = GetLanguage("Vendor_Banner_Imp")
			grdBanners.Columns(5).HeaderText = GetLanguage("Vendor_Banner_CPM")
			grdBanners.Columns(6).HeaderText = GetLanguage("Vender_View")
			grdBanners.Columns(7).HeaderText = GetLanguage("Vender_Clicks")
			grdBanners.Columns(8).HeaderText = GetLanguage("Vender_Start")
			grdBanners.Columns(9).HeaderText = GetLanguage("Vender_End")
			
            grdBanners.DataSource = objVendor.GetBanners(VendorID)
            grdBanners.DataBind()
			
			
        End Sub

        Public Function FormatURL(ByVal strKeyName As String, ByVal strKeyValue As String) As String
            FormatURL = EditURL(strKeyName, strKeyValue) & "&VendorId=" & VendorID & IIf(Not (Request.Params("hostpage") Is Nothing), "&hostpage=" , "") & "&def=Banner"
        End Function

        Private Sub cmdUpdateOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateOptions.Click

            Dim objAdmin As New AdminDB()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            objAdmin.UpdatePortalSetting(_portalSettings.PortalId, ddlLanguage.SelectedItem.Value & "_instructions", txtInstructions.Text)
            objAdmin.UpdatePortalSetting(_portalSettings.PortalId, "roleid", cboRoles.SelectedItem.Value)
            objAdmin.UpdatePortalSetting(_portalSettings.PortalId, ddlLanguage.SelectedItem.Value & "_message", txtMessage.Text)

        End Sub

        Private Sub cmdCancelOptions_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancelOptions.Click
            ' Redirect back to the portal home page
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

        Private Sub ddlLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLanguage.SelectedIndexChanged
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
				Dim ObjAdmin As New AdminDB()
		        
                txtInstructions.Text = CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)(ddlLanguage.SelectedItem.Value & "_instructions"), String)
				txtMessage.Text = CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)(ddlLanguage.SelectedItem.Value & "_message"), String)
		End Sub		

		Public Function GetBannerName(ByVal BannerID As String) As String
		Return GetLanguage("Vendor_Banner" & BannerID)
		end function

		Public Sub grdEditClassifications_ItemCommand(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles grdEditClassifications.ItemCommand
            Dim objVendor As New VendorsDB()

			objVendor.DeleteVendorClassification(Integer.Parse(grdEditClassifications.DataKeys(e.Item.ItemIndex).ToString()))
			grdEditClassifications.DataSource =  objVendor.GetVendorClassifications(-1)
			grdEditClassifications.DataBind() 
           
        End Sub

		Private Sub cmdAddName_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdAddName.Click

			If txtAddName.Text <> "" then
			Dim objVendor As New VendorsDB()
			objVendor.AddVendorClassificationName(txtAddName.Text)
			grdEditClassifications.DataSource =  objVendor.GetVendorClassifications(-1)
			grdEditClassifications.DataBind() 
			txtAddName.Text = ""
			end if

        End Sub

		
    End Class

End Namespace