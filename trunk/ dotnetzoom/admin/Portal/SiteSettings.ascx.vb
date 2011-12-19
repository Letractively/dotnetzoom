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

Imports System.IO
Imports System.Text.RegularExpressions

Namespace DotNetZoom

    Public Class SiteSettings
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents Title1 As DesktopModuleTitle
		Protected WithEvents ContainerEdit As ModuleEdit
		Protected WithEvents ContainerEdit1 As ModuleEdit
		Protected WithEvents ContainerEdit2 As ModuleEdit
		Protected WithEvents ContainerEdit3 As ModuleEdit
		Protected WithEvents ContainerEdit4 As ModuleEdit
		Protected WithEvents dlTabs As System.Web.UI.WebControls.DataList
        Protected WithEvents chkUserInfo As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents SSLCheckBox As System.Web.UI.WebControls.CheckBox
		Protected WithEvents chkContainerInfo As System.Web.UI.WebControls.CheckBox
		Protected WithEvents chkadminContainerInfo As System.Web.UI.WebControls.CheckBox
		Protected WithEvents chkeditContainerInfo As System.Web.UI.WebControls.CheckBox
		Protected WithEvents chkloginContainerInfo As System.Web.UI.WebControls.CheckBox
		Protected WithEvents chkToolTipContainerInfo As System.Web.UI.WebControls.CheckBox
		Protected WithEvents Portalcss As System.Web.UI.WebControls.HyperLink
		Protected WithEvents TTTcss As System.Web.UI.WebControls.HyperLink
		Protected WithEvents Portalskin As System.Web.UI.WebControls.HyperLink
		Protected WithEvents PortalEditskin As System.Web.UI.WebControls.HyperLink
		Protected WithEvents PortalTerms As System.Web.UI.WebControls.HyperLink
		Protected WithEvents PortalPrivacy As System.Web.UI.WebControls.HyperLink
		
		Protected WithEvents pnlDemoContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents authRoles As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents chkDemoSignup As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkDemoDomain As System.Web.UI.WebControls.RadioButtonList
		Protected WithEvents txtInstructionDemo As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtLogin As System.Web.UI.WebControls.TextBox	
		Protected WithEvents txtRegistration As System.Web.UI.WebControls.TextBox	
		Protected WithEvents txtRegisterEMail As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtSignup As System.Web.UI.WebControls.TextBox			
		Protected WithEvents ddlLanguage As System.Web.UI.WebControls.DropDownList
		Protected WithEvents ddlSiteLanguage As System.Web.UI.WebControls.DropDownList
		Protected WithEvents chkAuthLanguage As System.Web.UI.WebControls.CheckBoxList
		Protected WithEvents ddlTimeZone As System.Web.UI.WebControls.DropDownList
		Protected WithEvents txtPortalName As System.Web.UI.WebControls.TextBox
        Protected WithEvents cboLogo As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtFlash As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtKeyWords As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdGoogle As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cboBackground As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtFooterText As System.Web.UI.WebControls.TextBox
        Protected WithEvents optUserRegistration As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents optBannerAdvertising As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents cboCurrency As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cboAdministratorId As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cboProcessor As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdProcessor As System.Web.UI.WebControls.LinkButton
        Protected WithEvents txtUserId As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
        Protected WithEvents SiteTable1 As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents txtPortalAlias As System.Web.UI.WebControls.TextBox
        
        Protected WithEvents txtExpiryDate As System.Web.UI.WebControls.TextBox
        
        Protected WithEvents txtHostSpace As System.Web.UI.WebControls.TextBox
        
        Protected WithEvents txtHostFee As System.Web.UI.WebControls.TextBox
        
        Protected WithEvents txtSiteLogHistory As System.Web.UI.WebControls.TextBox
       	Protected WithEvents SiteRow6 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents SiteRow7 As System.Web.UI.HtmlControls.HtmlTableRow

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton

        Protected WithEvents Setting1 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting2 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting3 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting4 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting5 As System.Web.UI.WebControls.PlaceHolder



        Protected WithEvents chkStyleMenu As System.Web.UI.WebControls.CheckBox
        Protected WithEvents StyleRow As System.Web.UI.HtmlControls.HtmlTableRow

        Protected WithEvents grdModuleDefs As System.Web.UI.WebControls.DataGrid
        Protected WithEvents PortalRow3 As System.Web.UI.HtmlControls.HtmlTable
		Protected WithEvents PortalRow5 As System.Web.UI.HtmlControls.HtmlTable
		Protected WithEvents DemoCell As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents lblHostFee As System.Web.UI.WebControls.Label
        Protected WithEvents lblHostCurrency As System.Web.UI.WebControls.Label
        Protected WithEvents lblModuleFee As System.Web.UI.WebControls.Label
		Protected WithEvents lblTimeZone As System.Web.UI.WebControls.Label
        Protected WithEvents lblModuleCurrency As System.Web.UI.WebControls.Label
        Protected WithEvents lblTotalFee As System.Web.UI.WebControls.Label
        Protected WithEvents lblTotalCurrency As System.Web.UI.WebControls.Label
        
        Protected WithEvents PortalRow7 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents cmdRenew As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdExpiryCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents valExpiryDate As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents grdPortalsAlias As System.Web.UI.WebControls.DataGrid
        Protected WithEvents AddSetting As System.Web.UI.WebControls.ImageButton
        Protected WithEvents sslCheckBox1 As System.Web.UI.WebControls.CheckBox
        Protected WithEvents sslSubDomainBox1 As System.Web.UI.WebControls.CheckBox
        Protected WithEvents sllline As System.Web.UI.HtmlControls.HtmlTable




        Dim intPortalId As Integer = -1
		

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
        ' The Page_Load server event handler on this user control is used
        ' to populate the current site settings from the config system
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Not PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                EditDenied()
            End If

            Dim objAdmin As New AdminDB()
            valExpiryDate.ErrorMessage = "<br>" + GetLanguage("not_a_date")
            If Not (Request.QueryString("PortalID") Is Nothing) And PortalSecurity.IsSuperUser Then
                intPortalId = Int32.Parse(Request.QueryString("PortalID"))
                cmdCancel.Visible = True
            Else
                intPortalId = PortalId
                cmdCancel.Visible = False
            End If

            Dim objTIGRA As Control = Page.FindControl("tigra")
            If (Not objTIGRA Is Nothing) Then
                StyleRow.Visible = True
            Else
                StyleRow.Visible = False
            End If

            ' If this is the first visit to the page, populate the site data
            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If

                chkContainerInfo.Checked = True
                ContainerEdit.Visible = True

                ContainerEdit.ModuleId = -4
                ContainerEdit.ModuleTitle = GetLanguage("SS_SiteModuleSkin")
                ContainerEdit.TabID = _portalSettings.ActiveTab.TabId

                ContainerEdit1.ModuleId = -3
                ContainerEdit1.ModuleTitle = GetLanguage("SS_AdminModuleSkin")
                ContainerEdit1.TabID = _portalSettings.ActiveTab.TabId

                ContainerEdit2.ModuleId = -2
                ContainerEdit2.ModuleTitle = GetLanguage("SS_EditModuleSkin")
                ContainerEdit2.TabID = _portalSettings.ActiveTab.TabId

                ContainerEdit3.ModuleId = -1
                ContainerEdit3.ModuleTitle = GetLanguage("SS_LoginModuleSkin")
                ContainerEdit3.TabID = _portalSettings.ActiveTab.TabId

                ContainerEdit4.ModuleId = -5
                ContainerEdit4.ModuleTitle = GetLanguage("SS_ToolTipModuleSkin")
                ContainerEdit4.TabID = _portalSettings.ActiveTab.TabId


                chkContainerInfo.Text = GetLanguage("SS_SiteModuleSkin")
                chkContainerInfo.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("ms_edit_contener")))
                chkadminContainerInfo.Text = GetLanguage("SS_AdminModuleSkin")
                chkadminContainerInfo.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("ms_edit_contener")))
                chkeditContainerInfo.Text = GetLanguage("SS_EditModuleSkin")
                chkeditContainerInfo.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("ms_edit_contener")))
                chkloginContainerInfo.Text = GetLanguage("SS_LoginModuleSkin")
                chkloginContainerInfo.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("ms_edit_contener")))
                chkToolTipContainerInfo.Text = GetLanguage("SS_ToolTipModuleSkin")
                chkToolTipContainerInfo.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("ms_edit_contener")))



                cmdProcessor.Text = GetLanguage("SS_cmdProcessor")
                cmdGoogle.Text = GetLanguage("SS_cmdGoogle")
                cmdUpdate.Text = GetLanguage("enregistrer")
                cmdCancel.Text = GetLanguage("annuler")
                cmdDelete.Text = GetLanguage("delete")

                cmdRenew.Text = GetLanguage("cmdRenew")
                PortalTerms.ToolTip = GetLanguage("SS_Terms_Tooltip")
                PortalTerms.Text = GetLanguage("SS_Terms_Text")
                PortalPrivacy.ToolTip = GetLanguage("SS_Privacy_Tooltip")
                PortalPrivacy.Text = GetLanguage("SS_Privacy_Text")

                Portalcss.NavigateUrl = "javascript:var m = window.open('" + glbPath + "admin/tabs/skinedit.aspx?L=" & GetLanguage("N") & "&file=skin/portal.css&TabId=" & _portalSettings.ActiveTab.TabId & "', 'edit', 'width=800,height=600,left=100,top=100,resizable=1');m.focus();"
                Portalcss.ToolTip = GetLanguage("SS_PortalCSS_Tooltip")
                TTTcss.NavigateUrl = "javascript:var m = window.open('" + glbPath + "admin/tabs/skinedit.aspx?L=" & GetLanguage("N") & "&file=skin/ttt.css&TabId=" & _portalSettings.ActiveTab.TabId & "','edit', 'width=800,height=600,left=100,top=100,resizable=1');m.focus();"
                TTTcss.ToolTip = GetLanguage("SS_TTTCSS_Tooltip")
                Portalskin.NavigateUrl = "javascript:var m = window.open('" + glbPath + "admin/tabs/skinedit.aspx?L=" & GetLanguage("N") & "&file=skin/portal.skin&TabId=" & _portalSettings.ActiveTab.TabId & "','edit' , 'width=800,height=600,left=100,top=100,resizable=1');m.focus();"
                Portalskin.ToolTip = GetLanguage("SS_PortalSkin_Tooltip")
                PortalEditskin.NavigateUrl = "javascript:var m = window.open('" + glbPath + "admin/tabs/skinedit.aspx?L=" & GetLanguage("N") & "&file=skin/portaledit.skin&TabId=" & _portalSettings.ActiveTab.TabId & "','edit' , 'width=800,height=600,left=100,top=100,resizable=1');m.focus();"
                PortalEditskin.ToolTip = GetLanguage("SS_EditSkin_Tooltip")



                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm_portal")) & "');")
                cmdExpiryCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtExpiryDate)
                cmdExpiryCalendar.Text = GetLanguage("Stat_Calendar")
                ' load the list of files found in the upload directory
                Dim ImageFileList As ArrayList = GetFileList(intPortalId, glbImageFileTypes)
                cboLogo.DataSource = ImageFileList
                cboLogo.DataBind()
                cboBackground.DataSource = ImageFileList
                cboBackground.DataBind()


                Dim objUser As New UsersDB()

                ddlTimeZone.DataSource = objAdmin.GetTimeZoneCodes(GetLanguage("N"))
                ddlTimeZone.DataBind()
                cboProcessor.DataSource = objAdmin.GetProcessorCodes
                cboProcessor.DataBind()

                Dim dr As Data.SqlClient.SqlDataReader = objAdmin.GetSinglePortal(intPortalId)
                If dr.Read Then
                    If PortalSettings.GetHostSettings("chkEnableSSL").ToString = "Y" Then
                        SSLCheckBox.Checked = Boolean.Parse(dr("ssl").ToString)
                        If PortalSecurity.IsSuperUser Then
                            sllline.Visible = True
                        Else
                            sllline.Visible = False
                        End If

                    Else
                        SSLCheckBox.Checked = False
                        SSLCheckBox.Enabled = False
                        sslSubDomainBox1.Visible = False
                        sslSubDomainBox1.Checked = False
                        sslCheckBox1.Visible = False
                        sslCheckBox1.Checked = False
                    End If

                        txtPortalName.Text = dr("PortalName").ToString
                        If cboLogo.Items.Contains(New ListItem(dr("LogoFile").ToString)) Then
                            cboLogo.Items.FindByText(dr("LogoFile").ToString).Selected = True
                        End If
                        txtDescription.Text = dr("Description").ToString
                        txtKeyWords.Text = dr("KeyWords").ToString
                        If cboBackground.Items.Contains(New ListItem(dr("BackgroundFile").ToString)) Then
                            cboBackground.Items.FindByText(dr("BackgroundFile").ToString).Selected = True
                        End If
                        txtFooterText.Text = dr("FooterText").ToString

                        optUserRegistration.Items.FindByValue("0").Text = GetLanguage("optUserRegistration_0")
                        optUserRegistration.Items.FindByValue("1").Text = GetLanguage("optUserRegistration_1")
                        optUserRegistration.Items.FindByValue("2").Text = GetLanguage("optUserRegistration_2")
                        optUserRegistration.Items.FindByValue("3").Text = GetLanguage("optUserRegistration_3")

                        optBannerAdvertising.Items.FindByValue("0").Text = GetLanguage("optBannerAdvertising_0")
                        optBannerAdvertising.Items.FindByValue("1").Text = GetLanguage("optBannerAdvertising_1")
                        optBannerAdvertising.Items.FindByValue("2").Text = GetLanguage("optBannerAdvertising_2")

                        optUserRegistration.SelectedIndex = dr("UserRegistration")
                        optBannerAdvertising.SelectedIndex = dr("BannerAdvertising")

                        cboCurrency.DataSource = objAdmin.GetCurrencies(GetLanguage("N"))
                        cboCurrency.DataBind()
                        If dr("Currency").ToString = "" Then
                            cboCurrency.Items.FindByValue("USD").Selected = True
                        Else
                            cboCurrency.Items.FindByValue(dr("Currency").ToString).Selected = True
                        End If

                        Dim drUsers As Data.SqlClient.SqlDataReader = objUser.GetRoleMembership(intPortalId, GetLanguage("N"), dr("AdministratorRoleId"))
                        While drUsers.Read()
                            cboAdministratorId.Items.Add(New ListItem(drUsers("FullName"), drUsers("UserId").ToString))
                        End While
                        drUsers.Close()
                        If Not cboAdministratorId.Items.FindByValue(dr("AdministratorId")) Is Nothing Then
                            cboAdministratorId.Items.FindByValue(dr("AdministratorId")).Selected = True
                        End If

                        txtPortalAlias.Text = ""
                        If Not IsDBNull(dr("ExpiryDate")) Then
                            txtExpiryDate.Text = Format(CDate(dr("ExpiryDate")), "yyyy-MM-dd")
                        End If

                        txtHostFee.Text = Format(Val(dr("HostFee").ToString), "#,##0.00")
                        If txtHostFee.Text <> "" Then
                            lblHostFee.Text = Format(Val(txtHostFee.Text), "#,##0.00")
                        Else
                            lblHostFee.Text = "0.00"
                        End If
                        lblHostCurrency.Text = PortalSettings.GetHostSettings("HostCurrency") & " / " & GetLanguage("SS_Month")
                        txtHostSpace.Text = dr("HostSpace").ToString
                        If Not IsDBNull(dr("SiteLogHistory")) Then
                            txtSiteLogHistory.Text = dr("SiteLogHistory").ToString
                        End If


                        Try
                            ddlTimeZone.SelectedValue = dr("TimeZone")
                        Catch ex As Exception
                            ddlTimeZone.SelectedValue = 0
                        End Try

                        lblTimeZone.Text = DateTime.Now().AddMinutes(GetTimeDiff(_portalSettings.TimeZone)).ToString()

                        If Not cboProcessor.Items.FindByText(dr("PaymentProcessor").ToString) Is Nothing Then
                            cboProcessor.Items.FindByText(dr("PaymentProcessor").ToString).Selected = True
                        Else ' default
                            cboProcessor.Items.FindByText("PayPal").Selected = True
                        End If
                        txtUserId.Text = dr("ProcessorUserId").ToString
                        txtPassword.Text = dr("ProcessorPassword").ToString
                    End If
                dr.Close()

                Dim Tsettings As Hashtable = PortalSettings.GetSiteSettings(intPortalId)
                chkUserInfo.Items.Clear()
                Dim item As New ListItem()
                item.Text = "<img height=""14"" width=""17"" src=""" & glbPath & "images/1x1.gif"" Alt=""*"" style="" background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -91px;"">"
                item.Value = "SearchUser"
                If Tsettings("SearchUser") <> "NO" Then
                    item.Selected = True
                Else
                    item.Selected = False
                End If
                chkUserInfo.Items.Add(item)

                item = New ListItem()
                item.Text = "<img height=""14"" width=""17"" src=""" & glbPath & "images/1x1.gif"" Alt=""*"" style="" background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -105px;"">"
                item.Value = "UserOnline"
                If Tsettings("UserOnline") <> "NO" Then
                    item.Selected = True
                Else
                    item.Selected = False
                End If
                chkUserInfo.Items.Add(item)


                item = New ListItem()
                item.Text = "<img height=""12"" width=""18"" src=""" & glbPath & "images/1x1.gif"" Alt=""*"" style="" background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -147px;"">"
                item.Value = "UserMessage"
                If Tsettings("UserMessage") <> "NO" Then
                    item.Selected = True
                Else
                    item.Selected = False
                End If
                chkUserInfo.Items.Add(item)

                item = New ListItem()
                item.Text = "<img height=""14"" width=""17"" src=""" & glbPath & "images/1x1.gif"" Alt=""*"" style="" background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -91px;"">"
                item.Value = "PortalUserOnline"
                If Tsettings("PortalUserOnline") <> "NO" Then
                    item.Selected = True
                Else
                    item.Selected = False
                End If
                chkUserInfo.Items.Add(item)

                item = New ListItem()
                item.Text = "<img height=""16"" width=""17"" src=""" & glbPath & "images/1x1.gif"" style=""background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -243px;"" border=""0"" alt=""*"">"
                item.Value = "PMSMailNotice"
                If Tsettings("PMSMailNotice") <> "NO" Then
                    item.Selected = True
                Else
                    item.Selected = False
                End If
                chkUserInfo.Items.Add(item)



                Dim TempAuthLanguage As String = ""
                If Tsettings.ContainsKey("languageauth") Then
                    TempAuthLanguage = Tsettings("languageauth")
                Else
                    TempAuthLanguage = GetLanguage("N") & ";"
                    objAdmin.UpdatePortalSetting(intPortalId, "languageauth", TempAuthLanguage)
                End If


                ' Language to Use
                Dim HashL As Hashtable = objAdmin.GetAvailablelanguage
                For Each de As DictionaryEntry In HashL

                    Dim itemL As New ListItem()
                    itemL.Text = de.Value
                    itemL.Value = de.Key

                    Dim itemX As New ListItem()
                    itemX.Text = de.Value
                    itemX.Value = de.Key

                    Dim itemR As New ListItem()
                    itemR.Text = de.Value
                    itemR.Value = de.Key

                    If InStr(1, TempAuthLanguage, itemL.Value & ";") Then
                        ddlLanguage.Items.Add(itemX)
                        ddlSiteLanguage.Items.Add(itemL)
                    End If
                    If GetLanguage("N") = itemR.Value Then
                        itemR.Selected = True
                    Else
                        If InStr(1, TempAuthLanguage, itemR.Value & ";") Then
                            itemR.Selected = True
                        End If
                    End If
                    chkAuthLanguage.Items.Add(itemR)
                Next de


                If Tsettings.ContainsKey("language") Then
                    If Not ddlSiteLanguage.Items.FindByValue(Tsettings("language")) Is Nothing Then
                        ddlSiteLanguage.Items.FindByValue(Tsettings("language")).Selected = True
                    Else
                        ddlSiteLanguage.SelectedIndex = 0
                    End If
                Else
                    objAdmin.UpdatePortalSetting(intPortalId, "language", GetLanguage("N"))
                End If


                If Not ddlLanguage.Items.FindByText(GetLanguage("language")) Is Nothing Then
                    ddlLanguage.Items.FindByText(GetLanguage("language")).Selected = True
                Else
                    ddlLanguage.SelectedIndex = 0
                End If

                If ddlLanguage.Items.Count = 1 Then
                    ddlLanguage.Visible = False
                Else
                    ddlLanguage.Visible = True
                End If


                PortalTerms.NavigateUrl = "javascript:var m = window.open('" & FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, False, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "edit=mod&def=Terms&Language=" & ddlLanguage.SelectedItem.Value) & "','');m.focus();"
                PortalPrivacy.NavigateUrl = "javascript:var m = window.open('" & FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, False, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "edit=mod&def=Privacy&Language=" & ddlLanguage.SelectedItem.Value) & "','');m.focus();"

                If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_loginmessage") Then
                    txtLogin.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_loginmessage"), String)
                Else
                    txtLogin.Text = CType(Tsettings("loginmessage"), String)
                End If

                If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_registrationmessage") Then
                    txtRegistration.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_registrationmessage"), String)
                Else
                    txtRegistration.Text = CType(Tsettings("registrationmessage"), String)
                End If

                txtRegisterEMail.Text = CType(Tsettings("registeremail"), String)

                If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_Description") Then
                    txtDescription.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_Description"), String)
                End If

                If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_KeyWords") Then
                    txtKeyWords.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_KeyWords"), String)
                End If

                If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_FooterText") Then
                    txtFooterText.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_FooterText"), String)
                End If

                If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_PortalName") Then
                    txtPortalName.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_PortalName"), String)
                End If

                If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_signupmessage") Then
                    txtSignup.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_signupmessage"), String)
                Else
                    txtSignup.Text = CType(Tsettings("signupmessage"), String)
                End If




                txtFlash.Text = ""
                If Tsettings("flash") <> Nothing Then
                    txtFlash.Text = CType(Tsettings("flash"), String)
                End If
                txtInstructionDemo.Text = ""

                If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_DemoDirectives") Then
                    txtInstructionDemo.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_DemoDirectives"), String)
                Else
                    txtInstructionDemo.Text = CType(Tsettings("DemoDirectives"), String)
                End If

                Dim TempDomainName As String = GetDomainName(Request)

                chkDemoDomain.Items.FindByValue("N").Text = TempDomainName & "/" & GetLanguage("chkDemoDomain")
                TempDomainName = Replace(TempDomainName, "www.", "")
                chkDemoDomain.Items.FindByValue("Y").Text = GetLanguage("chkDemoDomain") & "." & TempDomainName

                If Tsettings("DemoDomain") <> Nothing Then
                    chkDemoDomain.Items.FindByValue(CType(Tsettings("DemoDomain"), String)).Selected = True
                Else
                    chkDemoDomain.SelectedIndex = 0
                End If

                If Tsettings("DemoSignup") <> Nothing Then
                    If CType(Tsettings("DemoSignup"), String) = "Y" Then
                        chkDemoSignup.Checked = True
                        SiteRow7.Visible = True
                        DemoCell.Visible = True
                        pnlDemoContent.Visible = True
                    Else
                        chkDemoSignup.Checked = False
                        SiteRow7.Visible = False
                        pnlDemoContent.Visible = False
                        DemoCell.Visible = False
                    End If
                End If


                Dim slTabs As SortedList = New SortedList
                Dim TabSelectedIndex As Integer = 0
                If Request("TabSelected") <> "" Then
                    TabSelectedIndex = Request("TabSelected") - 1
                End If

                GetTabVisible((TabSelectedIndex + 1).ToString)

                slTabs.Add("1", GetLanguage("SiteSettings1"))
                slTabs.Add("2", GetLanguage("SiteSettings2"))
                slTabs.Add("3", GetLanguage("SiteSettings3"))
                If pnlDemoContent.Visible Or PortalSecurity.IsSuperUser Then
                    slTabs.Add("4", GetLanguage("SiteSettings4"))
                Else
                    If TabSelectedIndex = 4 Then
                        TabSelectedIndex = 3
                    End If
                End If
                slTabs.Add("5", GetLanguage("SiteSettings5"))


                dlTabs.DataSource = slTabs
                dlTabs.DataBind()

                dlTabs.SelectedIndex = TabSelectedIndex



                BindData()
                InitializeDemo()

            Else
                Title1.DisplayHelp = ViewState("DisplayHelp")
            End If

            sslCheckBox1.Visible = SSLCheckBox.Checked
            sslSubDomainBox1.Visible = SSLCheckBox.Checked

        End Sub


        Private Sub BindDataAlias()

            Dim objAdmin As New AdminDB()
            If SSLCheckBox.Checked Then
                grdPortalsAlias.Columns(0).HeaderText = GetLanguage("P_sub")
                grdPortalsAlias.Columns(1).HeaderText = GetLanguage("P_ssl")
            Else
                grdPortalsAlias.Columns(0).HeaderText = ""
                grdPortalsAlias.Columns(1).HeaderText = ""
            End If
            grdPortalsAlias.Columns(2).HeaderText = GetLanguage("P_Alias")


            grdPortalsAlias.DataSource = objAdmin.GetPortalAlias(intPortalId)
            grdPortalsAlias.DataBind()

        End Sub

        Sub InitializeDemo()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Populate checkbox list with all security roles for this portal
            ' and "check" the ones already configured for this tab
            Dim objUser As New UsersDB()
            Dim roles As Data.SqlClient.SqlDataReader = objUser.GetPortalRoles(intPortalId, GetLanguage("N"))
            Dim Tsettings As Hashtable = portalSettings.GetSiteSettings(intPortalId)
			
			Dim strAuthorizedRoles As String = ""
			If Tsettings("DemoAuthRole") <> nothing then
			strAuthorizedRoles = CType(Tsettings("DemoAuthRole"), String) 
			end if
			
            ' Clear existing items in checkboxlist
            authRoles.Items.Clear()

            Dim allAuthItem As New ListItem()
            allAuthItem.Text = GetLanguage("SS_Guest") 
            allAuthItem.Value = glbRoleAllUsers
			If InStr(1, strAuthorizedRoles , glbRoleAllUsers) Then
               allAuthItem.Selected = True
            End If
            authRoles.Items.Add(allAuthItem)

            Dim unauthItem As New ListItem()
            unauthItem.Text = GetLanguage("ms_non_authorized")
            unauthItem.Value = glbRoleUnauthUser
			If InStr(1, strAuthorizedRoles , glbRoleUnauthUser) Then
               unauthItem.Selected = True
            End If
            authRoles.Items.Add(unauthItem)
			
			
			
            While roles.Read()
                Dim authItem As New ListItem()
				authItem.Text = CType(roles("RoleName"), String)
                authItem.Value = roles("RoleID").ToString()
                If authItem.Value = _portalSettings.AdministratorRoleId.ToString Then
                    authItem.Selected = True
                End If
                If InStr(1, strAuthorizedRoles , authItem.Value) Then
                    authItem.Selected = True
                End If
                authRoles.Items.Add(authItem)
            End While
            roles.Close()

        End Sub

        Sub SaveDemo()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

            ' Construct AuthorizedRoles String
            Dim strAuthorizedRoles As String = ""

            For Each item In authRoles.Items
                If item.Selected = True Or item.Value = _portalSettings.AdministratorRoleId.ToString Then
                    strAuthorizedRoles += item.Value & ";"
                End If
            Next item
            Dim admin As New AdminDB()

           admin.UpdatePortalSetting(intPortalId, "DemoAuthRole", strAuthorizedRoles)
			
         End Sub
		
		
        Private Sub BindData()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

			grdModuleDefs.Columns(2).HeaderText = GetLanguage("MS_ModuleName")
			grdModuleDefs.Columns(3).HeaderText = GetLanguage("MS_Module_Desc")
			grdModuleDefs.Columns(4).HeaderText = GetLanguage("MS_BonusTxt")
			
			
            grdModuleDefs.DataSource = objAdmin.GetPortalModuleDefinitions(intPortalId, GetLanguage("N"))
            grdModuleDefs.DataBind()

			

            If grdModuleDefs.Items.Count = 0 Then
                grdModuleDefs.Visible = False
            End If

            lblModuleFee.Text = "0.00"
            lblModuleCurrency.Text = portalSettings.GetHostSettings("HostCurrency") & " / " & GetLanguage("SS_Month")

            lblModuleFee.Text = Format(objAdmin.GetPortalModuleDefinitionFee(intPortalId), "#,##0.00")

            lblTotalFee.Text = Format(Val(lblHostFee.Text) + Val(lblModuleFee.Text), "#,##0.00")
            lblTotalCurrency.Text = portalSettings.GetHostSettings("HostCurrency") & " / " & GetLanguage("SS_Month")
			If lblTotalFee.Text = "0,00" then
			PortalRow3.visible = False
			PortalRow7.visible = False
			end if
			
			If txtExpiryDate.Text = "" then
			PortalRow5.visible = False
			End if
			
            If PortalSecurity.IsSuperUser Then
                AddSetting.ToolTip = GetLanguage("add")
                PortalRow3.Visible = True
                PortalRow5.Visible = True
                SiteTable1.Visible = True
                txtExpiryDate.Enabled = True
                cmdExpiryCalendar.Visible = True
                DemoCell.Visible = True
                SiteRow7.Visible = True
                SiteRow6.Visible = True
                pnlDemoContent.Visible = True
                If intPortalId > 0 Then
                    ' Can only delete if not base portal
                    cmdDelete.Visible = True
                End If
                cmdRenew.Visible = False
                PortalRow7.Visible = False
                txtHostFee.Enabled = True
                txtHostFee.Visible = True
                lblHostFee.Visible = False
                txtSiteLogHistory.Enabled = True
                txtHostSpace.Enabled = True
            End If


			
        End Sub

        Private Sub Update_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			ClearPortalCache(intPortalId)
		

            Dim strLogo As String
            Dim strBackground As String

            If Not cboLogo.SelectedItem Is Nothing Then
                strLogo = cboLogo.SelectedItem.Value
            End If
            If Not cboBackground.SelectedItem Is Nothing Then
                strBackground = cboBackground.SelectedItem.Value
            End If

            Dim dblHostFee As Double = 0
            If txtHostFee.Text <> "" Then
                dblHostFee = Double.Parse(txtHostFee.Text)
            End If

            Dim dblHostSpace As Double = 0
            If txtHostSpace.Text <> "" Then
                dblHostSpace = Double.Parse(txtHostSpace.Text)
            End If

            Dim intSiteLogHistory As Integer = -1
            If txtSiteLogHistory.Text <> "" Then
                intSiteLogHistory = Integer.Parse(txtSiteLogHistory.Text)
            End If

            ' update Portal info in the database
            Dim admin As New AdminDB()

            admin.UpdatePortalInfo(intPortalId, txtPortalName.Text, "", strLogo, txtFooterText.Text, optUserRegistration.SelectedIndex, optBannerAdvertising.SelectedIndex, cboCurrency.SelectedItem.Value, cboAdministratorId.SelectedItem.Value, CheckDateSqL(txtExpiryDate.Text), dblHostFee, dblHostSpace, cboProcessor.SelectedItem.Text, txtUserId.Text, txtPassword.Text, txtDescription.Text, txtKeyWords.Text, strBackground, intSiteLogHistory, ddlTimeZone.SelectedItem.Value, SSLCheckBox.Checked)

            ' SSLCheckBox

			admin.UpdatePortalSetting(intPortalId, "registeremail", txtregisteremail.text)
            admin.UpdatePortalSetting(intPortalId, ddlLanguage.SelectedItem.Value & "_loginmessage", txtLogin.Text)
            admin.UpdatePortalSetting(intPortalId, ddlLanguage.SelectedItem.Value & "_registrationmessage", txtRegistration.Text)
            admin.UpdatePortalSetting(intPortalId, ddlLanguage.SelectedItem.Value & "_signupmessage", txtSignup.Text)
            admin.UpdatePortalSetting(intPortalId, ddlLanguage.SelectedItem.Value & "_Description", txtDescription.Text)
            admin.UpdatePortalSetting(intPortalId, ddlLanguage.SelectedItem.Value & "_KeyWords", txtKeyWords.Text)
            admin.UpdatePortalSetting(intPortalId, ddlLanguage.SelectedItem.Value & "_FooterText", txtFooterText.Text)
            admin.UpdatePortalSetting(intPortalId, ddlLanguage.SelectedItem.Value & "_PortalName", txtPortalName.Text)


			
			admin.UpdatePortalSetting(intPortalId, "flash", txtflash.Text)

			admin.UpdatePortalSetting(intPortalId, "language", ddlSiteLanguage.SelectedItem.Value)

			admin.UpdatePortalSetting(intPortalId, ddlLanguage.SelectedItem.Value & "_DemoDirectives", txtInstructionDemo.Text)
			
			For Each item In chkUserInfo.Items
                If item.Selected Then
                admin.UpdatePortalSetting(intPortalId, item.value, "YES")
				else
				admin.UpdatePortalSetting(intPortalId, item.value, "NO")
                End If
            Next item

                ' Construct Language Auth 
            Dim strLanguageAuth As String = ""

                For Each itemL In chkAuthLanguage.Items
                    If itemL.Selected or itemL.Value = ddlSiteLanguage.SelectedItem.Value Then
                        strLanguageAuth = strLanguageAuth & itemL.Value & ";"
                    End If
                Next itemL
				admin.UpdatePortalSetting(intPortalId, "languageauth", strLanguageAuth)
			
			If chkDemoDomain.SelectedItem.Value = "Y" then
			admin.UpdatePortalSetting(intPortalId, "DemoDomain", "Y")
			else
			admin.UpdatePortalSetting(intPortalId, "DemoDomain", "N")
			End if
			
			If chkDemoSignup.Checked = True then
			admin.UpdatePortalSetting(intPortalId, "DemoSignup", "Y")
           	Else
			admin.UpdatePortalSetting(intPortalId, "DemoSignup", "N")
           	End If

			SaveDemo()
			
			' if language changed redirect to refresh 
			if GetLanguage("N") <> ddlSiteLanguage.SelectedItem.Value then
			Dim TabSelected As String = ""			
			If Setting1.Visible then TabSelected = "TabSelected=1"
			If Setting2.Visible then TabSelected = "TabSelected=2"
			if Setting3.Visible then TabSelected = "TabSelected=3"
			if Setting4.Visible then TabSelected = "TabSelected=4"
			if Setting5.Visible then TabSelected = "TabSelected=5"
			TabSelected += iif(Request.Params("adminpage") Is Nothing, "", "&adminpage=" & Request.Params("adminpage"))
			TabSelected += iif(Request.Params("PortalID") Is Nothing, "", "&PortalID=" & intPortalId.ToString)
			
			' Redirect back to refresh
                Response.Redirect(Replace(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, TabSelected), GetLanguage("N") & ".", ddlSiteLanguage.SelectedItem.Value & "."), True)
			end if

        End Sub

        Private Sub Delete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click
			If IntPortalID > 0 then
			' cannot delete portal if 0
			ClearPortalCache(intPortalId)
            Dim FileName As String
            Dim strServerPath As String
            Dim strPortalName As String

            strServerPath = GetAbsoluteServerPath(Request)

            Dim objAdmin As New AdminDB()

            Dim dr As SqlDataReader = objAdmin.GetSinglePortal(intPortalId)
            If dr.Read Then
                    ' delete upload directory
                    If dr("GUID").ToString <> "" Then
                        deleteFolderRecursive(strServerPath & "Portals\" & dr("GUID").ToString)
                    End If
                    ' delete child directory
                    Dim Drd As SqlDataReader = objAdmin.GetPortalAlias(intPortalId)
                    While Drd.Read
                        strPortalName = CType(Drd("PortalAlias"), String)
                        If InStr(1, strPortalName, "/") Then
                            strPortalName = Mid(strPortalName, InStrRev(strPortalName, "/") + 1)
                            If strPortalName <> "" And strPortalName.ToLower <> "portals" And System.IO.Directory.Exists(strServerPath & strPortalName) Then
                                deleteFolderRecursive(strServerPath & strPortalName)
                            End If
                        End If

                    End While
                    Drd.close()

                    ' remove database references
                    objAdmin.DeletePortalInfo(intPortalId)
                End If
                dr.Close()

                ' Redirect to another site
                If intPortalId = PortalId Then
                    Response.Redirect("~/default.aspx", True)
                Else
                    Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
                End If
            End If
        End Sub

        Private Sub deleteFolderRecursive(ByVal strRoot As String)

            If strRoot <> "" Then
                Dim strFolder As String
                Dim FileName As String
                If System.IO.Directory.Exists(strRoot) Then
                    For Each strFolder In System.IO.Directory.GetDirectories(strRoot)
                        deleteFolderRecursive(strFolder)
                    Next
                    If System.IO.Directory.Exists(strRoot) Then
                        Dim fileEntries As String() = System.IO.Directory.GetFiles(strRoot)
                        For Each FileName In fileEntries
                            System.IO.File.Delete(FileName)
                        Next FileName
                        System.IO.Directory.Delete(strRoot, True)
                    End If
                End If
            End If
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

        Private Sub cmdRenew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRenew.Click

			ClearPortalCache(intPortalId)
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim strProcessorURL As String = ""
			Dim TempString As String = txtPortalName.Text & " " & GetLanguage("SS_Renewal")
            Dim objAdmin As New AdminDB()

            Dim strAdministratorRoleId As String
            Dim dr As SqlDataReader = objAdmin.GetSinglePortal(intPortalId)
            If dr.Read Then
                strAdministratorRoleId = dr("AdministratorRoleId").ToString
            End If
            dr.Close()

            If portalSettings.GetHostSettings("PaymentProcessor") = "PayPal" or portalSettings.GetHostSettings("PaymentProcessor") = "SandBoxPayPal" Then
			
			Tempstring = replace(tempstring , "{HostFee}" ,lblHostFee.Text)
			Tempstring = replace(tempstring , "{ModuleFee}" ,lblModuleFee.Text)
			Tempstring = replace(tempstring , "{TotalFee}" ,lblTotalFee.Text)
				If portalSettings.GetHostSettings("PaymentProcessor") = "PayPal" then
                strProcessorURL = "https://www.paypal.com/xclick/business=" & HTTPPOSTEncode(portalSettings.GetHostSettings("ProcessorUserId"))
                else
				strProcessorURL = "https://www.sandbox.paypal.com/xclick/business=" & HTTPPOSTEncode(portalSettings.GetHostSettings("ProcessorUserId"))
				end if
				
				strProcessorURL = strProcessorURL & "&item_name=" & HTTPPOSTEncode(TempString)
                strProcessorURL = strProcessorURL & "&item_number=" & HTTPPOSTEncode(strAdministratorRoleId)
                strProcessorURL = strProcessorURL & "&quantity=1" ' month by month only due to premium modules
                strProcessorURL = strProcessorURL & "&custom=" & HTTPPOSTEncode(Context.User.Identity.Name)
   				lblTotalFee.Text = replace(lblTotalFee.Text, "," , ".")
                strProcessorURL = strProcessorURL & "&amount=" & HTTPPOSTEncode(lblTotalFee.Text)
                strProcessorURL = strProcessorURL & "&currency_code=" & HTTPPOSTEncode(portalSettings.GetHostSettings("HostCurrency"))

                strProcessorURL = strProcessorURL & "&return=" & HTTPPOSTEncode(Request.UrlReferrer.ToString())
                strProcessorURL = strProcessorURL & "&cancel_return=" & HTTPPOSTEncode(Request.UrlReferrer.ToString()) & "&charset=utf-8"

                strProcessorURL = strProcessorURL & "&notify_url=" & HTTPPOSTEncode(glbHTTP() & "/admin/Sales/PayPalIPN.aspx")

            End If

            ' redirect to payment processor
            If strProcessorURL <> "" Then
                Response.Redirect(strProcessorURL, True)
            End If

        End Sub



		
' début ajout pour menu

        Private Sub chkStyleMenu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkStyleMenu.CheckedChanged
		    Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "editmenu=1" & "&" & GetAdminPage()), True)
        End Sub



        Private Sub cmdGoogle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGoogle.Click

            Dim strURL As String = ""
            Dim strComments As String = ""

                strComments += txtPortalName.Text
                If txtDescription.Text <> "" Then
                    strComments += " " & txtDescription.Text
                End If
                If txtKeyWords.Text <> "" Then
                    strComments += " " & txtKeyWords.Text
                End If

            strURL += "http://www.google.com/addurl?q=" & HTTPPOSTEncode(AddHTTP(GetDomainName(Request)))
            strURL += "&dq=" & HTTPPOSTEncode(strComments)
            strURL += "&submit=Add+URL"

            Response.Redirect(strURL, True)

        End Sub

        Public Sub grdModuleDefs_CancelEdit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            grdModuleDefs.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub grdModuleDefs_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) 
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

             If PortalSecurity.IsSuperUser Then
                  grdModuleDefs.EditItemIndex = e.Item.ItemIndex
                  grdModuleDefs.SelectedIndex = -1
             End If

            BindData()
        End Sub

        Public Sub grdModuleDefs_Update(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) 
            Dim chkSubscribed As CheckBox = CType(e.Item.FindControl("Checkbox2"), WebControls.CheckBox)
            Dim txtHostingFee As TextBox = CType(e.Item.FindControl("txtHostingFee"), WebControls.TextBox)

            Dim dblHostFee As Double = 0
            If txtHostingFee.Text <> "" Then
                dblHostFee = Double.Parse(txtHostingFee.Text)
            End If

            Dim objAdmin As New AdminDB()

            objAdmin.UpdatePortalModuleDefinition(intPortalId, Integer.Parse(grdModuleDefs.DataKeys(e.Item.ItemIndex).ToString()), chkSubscribed.Checked, dblHostFee)

            grdModuleDefs.EditItemIndex = -1
            BindData()
        End Sub

        Private Sub grdModuleDefs_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdModuleDefs.ItemCreated
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			
            Dim cmdEditModuleDefs As Control = e.Item.FindControl("cmdEditModuleDefs")
			
			If Not cmdEditModuleDefs Is Nothing Then
                If not PortalSecurity.IsSuperUser Then
                    CType(cmdEditModuleDefs, ImageButton).Attributes.Add("onClick", "javascript: return confirm('" & rtesafe(GetLanguage("request_confirm_premium")) & "')")
                End If
            End If

         End Sub

        Public Function FormatFee(ByVal objHostFee As Object) As String
            If Not IsDBNull(objHostFee) Then
                Return Format(objHostFee, "#,##0.00")
            Else
                Return 0
            End If
        End Function

        Public Function FormatCurrency() As String
            Return lblHostCurrency.Text
        End Function

        Private Sub cmdProcessor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcessor.Click
            Response.Redirect(AddHTTP(cboProcessor.SelectedItem.Value), True)
        End Sub

        Private Sub ddlLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLanguage.SelectedIndexChanged
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

		
                Dim Tsettings As Hashtable = portalSettings.GetSiteSettings(intPortalId)
				
				txtLogin.Text = "" 
				If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_loginmessage") then
				txtLogin.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_loginmessage"), String)
				else 
            	txtLogin.Text = CType(Tsettings("loginmessage"), String)
				end if
				txtRegistration.Text = ""
				If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_registrationmessage") then
				txtRegistration.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_registrationmessage"), String)
				else 
            	txtRegistration.Text = CType(Tsettings("registrationmessage"), String)
				end if
				txtSignup.Text = ""
				If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_signupmessage") then
				txtSignup.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_signupmessage"), String)
				else 
            	txtSignup.Text = CType(Tsettings("signupmessage"), String)
				end if

				txtInstructionDemo.Text = ""
				
				If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_DemoDirectives") then
				txtInstructionDemo.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_DemoDirectives"), String)
				else 
            	txtInstructionDemo.Text = CType(Tsettings("DemoDirectives"), String)
				end if

				If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_Description") then
				txtDescription.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_Description"), String)
				else
				txtDescription.Text = ""
				end if

				If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_KeyWords") then
				txtKeyWords.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_KeyWords"), String)
				else
				txtKeyWords.Text = ""
				end if

				If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_PortalName") then
				txtPortalName.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_PortalName"), String)
				end if
               
				If Tsettings.ContainsKey(ddlLanguage.SelectedItem.Value & "_FooterText") then
				txtFooterText.Text = CType(Tsettings(ddlLanguage.SelectedItem.Value & "_FooterText"), String)
				else
				txtFooterText.Text = ""
				end if

            PortalTerms.NavigateUrl = "javascript:var m = window.open('" & FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, False, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "edit=mod&def=Terms&Language=" & ddlLanguage.SelectedItem.Value) & "','');m.focus();"
            PortalPrivacy.NavigateUrl = "javascript:var m = window.open('" & FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, False, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "edit=mod&def=Privacy&Language=" & ddlLanguage.SelectedItem.Value) & "','');m.focus();"

	
		End Sub

        Protected Sub GetTabVisible(ByVal CommandName As String) 
			Setting1.Visible = False
			Setting2.Visible = False
			Setting3.Visible = False
			Setting4.Visible = False
			Setting5.Visible = False

		   Select CommandName
                Case "1"
                    Setting1.Visible = True
                    Title1.DisplayHelp = "DisplayHelp_SiteSettings1"
                Case "2"
                    Setting2.Visible = True
                    Title1.DisplayHelp = "DisplayHelp_SiteSettings2"
                Case "3"
                    Setting3.Visible = True
                    Title1.DisplayHelp = "DisplayHelp_SiteSettings3"
                Case "4"
                    Setting4.Visible = True
                    If PortalSecurity.IsSuperUser Then
                        ' Demo Portal
                        Title1.DisplayHelp = "DisplayHelp_SiteSettingsDemo"
                    Else
                        Title1.DisplayHelp = "DisplayHelp_SiteSettings4"
                    End If

                Case "5"
                    Setting5.Visible = True
                    If PortalSecurity.IsSuperUser Then
                        ' Set Portal Alias
                        Title1.DisplayHelp = "DisplayHelp_SiteSettingsAlias"
                    Else
                        Title1.DisplayHelp = "DisplayHelp_SiteSettings5"
                    End If
                    BindDataAlias()
            End Select
		   
		   ViewState("DisplayHelp") = Title1.DisplayHelp
        End Sub

        Private Sub AddSetting_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddSetting.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objAdmin As New AdminDB()
            objAdmin.UpdatePortalAlias(intPortalId, txtPortalAlias.Text, sslSubDomainBox1.Checked, sslCheckBox1.Checked)
            sslCheckBox1.Checked = False
            sslSubDomainBox1.Checked = False
            txtPortalAlias.Text = ""
            BindDataAlias()
        End Sub


        Private Function GetTextBox(ByVal item As DataGridItem, ByVal NameOfBox As String) As TextBox
            Return CType(item.FindControl(NameOfBox), TextBox)
        End Function

        Private Function GetCheckBox(ByVal item As DataGridItem, ByVal NameOfBox As String) As CheckBox
            Return CType(item.FindControl(NameOfBox), CheckBox)
        End Function

        Private Sub grdPortalsAlias_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdPortalsAlias.ItemCommand
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objAdmin As New AdminDB()

            Select Case e.CommandName
                Case "EditOK"
                    If e.CommandArgument = GetTextBox(e.Item, "txtRename").Text.ToLower Then
                        objAdmin.UpdatePortalAlias(intPortalId, e.CommandArgument, GetCheckBox(e.Item, "sslSubDomainBox").Checked, GetCheckBox(e.Item, "sslCheckBox").Checked)
                    Else
                        objAdmin.DeletePortalAlias(e.CommandArgument)
                        objAdmin.UpdatePortalAlias(intPortalId, GetTextBox(e.Item, "txtRename").Text.ToLower, GetCheckBox(e.Item, "sslSubDomainBox").Checked, GetCheckBox(e.Item, "sslCheckBox").Checked)
                    End If

                    BindDataAlias()
                Case "DeleteOK"
                    If e.CommandArgument <> _portalSettings.PortalAlias Then
                        objAdmin.DeletePortalAlias(e.CommandArgument)
                        BindDataAlias()
                    End If

            End Select

        End Sub

        Private Sub SSLCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SSLCheckBox.CheckedChanged
            BindDataAlias()
        End Sub

		
        Protected Sub dlTabs_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs)
    	   dlTabs.SelectedIndex = e.Item.ItemIndex
		   GetTabVisible(e.CommandName)
        End Sub

        Private Sub chkcontainerInfo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkcontainerInfo.CheckedChanged
		   chkadmincontainerInfo.Checked = False
		   chkeditcontainerInfo.Checked = False
		   chkloginContainerInfo.Checked = False
		   chkToolTipContainerInfo.Checked = False
		   ContainerEdit.Visible = chkcontainerInfo.Checked
		   ContainerEdit1.Visible = False
		   ContainerEdit2.Visible = False
		   ContainerEdit3.Visible = False
		   ContainerEdit4.Visible = False
        End Sub

        Private Sub chkadmincontainerInfo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkadmincontainerInfo.CheckedChanged
		   chkcontainerInfo.Checked = False
		   chkeditcontainerInfo.Checked = False
		   chkloginContainerInfo.Checked = False
		   chkToolTipContainerInfo.Checked = False
		   ContainerEdit.Visible = False
		   ContainerEdit1.Visible = chkadmincontainerInfo.Checked
		   ContainerEdit2.Visible = False
		   ContainerEdit3.Visible = False
		   ContainerEdit4.Visible = False

        End Sub

        Private Sub chkeditcontainerInfo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkeditcontainerInfo.CheckedChanged
		   chkadmincontainerInfo.Checked = False
		   chkcontainerInfo.Checked = False
		   chkloginContainerInfo.Checked = False
		   chkToolTipContainerInfo.Checked = False
		   ContainerEdit.Visible = False
		   ContainerEdit1.Visible = False
		   ContainerEdit2.Visible = chkeditcontainerInfo.Checked
		   ContainerEdit3.Visible = False
		   ContainerEdit4.Visible = False
        End Sub
		

        Private Sub chkloginContainerInfo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkloginContainerInfo.CheckedChanged
		   chkadmincontainerInfo.Checked = False
		   chkcontainerInfo.Checked = False
		   chkeditcontainerInfo.Checked = False
		   chkToolTipContainerInfo.Checked = False
		   ContainerEdit.Visible = False
		   ContainerEdit1.Visible = False
		   ContainerEdit2.Visible = False
		   ContainerEdit3.Visible = chkloginContainerInfo.Checked
		   ContainerEdit4.Visible = False
        End Sub

        Private Sub chkToolTipContainerInfo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkToolTipContainerInfo.CheckedChanged
		   chkadmincontainerInfo.Checked = False
		   chkcontainerInfo.Checked = False
		   chkeditcontainerInfo.Checked = False
		   chkloginContainerInfo.Checked = False
		   ContainerEdit.Visible = False
		   ContainerEdit1.Visible = False
		   ContainerEdit2.Visible = False
		   ContainerEdit3.Visible = False
		   ContainerEdit4.Visible = chkToolTipContainerInfo.Checked
        End Sub		
    End Class

End Namespace