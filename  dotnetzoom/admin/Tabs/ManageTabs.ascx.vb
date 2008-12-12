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
Imports System.Xml
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System
Imports System.object
Imports System.Collections
Imports System.Configuration
Imports System.Globalization
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Text.RegularExpressions

Namespace DotNetZoom

    Public Class ManageTabs
        Inherits DotNetZoom.PortalModuleControl

		
        Protected WithEvents chkStyleMenu As System.Web.UI.WebControls.CheckBox
        Protected WithEvents StyleRow As System.Web.UI.HtmlControls.HtmlTableRow
		
		Protected WithEvents ddlLanguage As System.Web.UI.WebControls.DropDownList		
		Protected WithEvents ltabName As System.Web.UI.WebControls.TextBox
		Protected WithEvents cmdUpdateName As System.Web.UI.WebControls.LinkButton
		Protected WithEvents rowlanguage As System.Web.UI.HtmlControls.HtmlTableRow
		
        Protected WithEvents tabName As System.Web.UI.WebControls.TextBox

    	Protected WithEvents MyHtmlImage As System.Web.UI.WebControls.Image
		Protected WithEvents txticone As System.Web.UI.WebControls.TextBox
		Protected WithEvents lnkicone As System.Web.UI.WebControls.hyperlink

        Protected WithEvents valtabName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents cboTab As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cbocss As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cboskin As System.Web.UI.WebControls.DropDownList
		Protected WithEvents editcss As System.Web.UI.WebControls.Hyperlink
		Protected WithEvents editskin As System.Web.UI.WebControls.Hyperlink
        Protected WithEvents IsVisible As System.Web.UI.WebControls.CheckBox
        Protected WithEvents DisableLink As System.Web.UI.WebControls.CheckBox
        Protected WithEvents txtLeftPaneWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtRightPaneWidth As System.Web.UI.WebControls.TextBox
		
		Protected WithEvents XMLTextBox  As System.Web.UI.WebControls.TextBox
        Protected WithEvents adminRoles As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents authRoles As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents rowTemplate As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents cboTemplate As System.Web.UI.WebControls.DropDownList
        Protected WithEvents rowTemplate1 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents cboTemplate1 As System.Web.UI.WebControls.DropDownList



        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
		Protected WithEvents cmdXML  As System.Web.UI.WebControls.LinkButton
        Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents PlaceHolder1 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents PlaceHolder2 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents cmdCancel1 As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdXML2 As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdXML3 As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdXML4 As System.Web.UI.WebControls.LinkButton
        Protected WithEvents XMLsaveBox As System.Web.UI.WebControls.TextBox
        Protected WithEvents CheckBoxList1 As System.Web.UI.WebControls.CheckBoxList

        Private strAction As String = ""

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
        ' The Page_Load server event handler on this page is used
        ' to populate a tab's layout settings on the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			lnkicone.Tooltip = GetLanguage("select_icone_tooltip")	
			lnkicone.Text = GetLanguage("select_icone")	
			valtabName.ErrorMessage = "<br>" + GetLanguage("need_tab_name")
			cmdUpdate.Text = getLanguage("enregistrer")
			cmdUpdateName.Text = getLanguage("enregistrer")
			cmdCancel.Text = getlanguage("annuler")
            cmdDelete.Text = GetLanguage("delete")


            If Not (Request.Params("action") Is Nothing) Then
                strAction = Request.Params("action")
            Else
                strAction = ""
            End If



            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) And strAction = "edit" Then
                cmdXML.Visible = True
                cmdXML.Text = GetLanguage("Generate_XML")
                Title1.DisplayOptions2 = True
                Title1.OptionsText2 = GetLanguage("Generate_XML")
                Title1.Options2URL = Request.RawUrl
                Title1.Options2IMG = "<img  src=""images/xml.gif"" alt=""xml"" style=""border-width:0px;"">"
            Else
                cmdXML.Visible = False
            End If
            IsVisible.ToolTip = GetLanguage("ts_visibleinfo")
            DisableLink.ToolTip = GetLanguage("ts_disableinfo")

            ' Verify that the current user has access to edit this module
            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = False And PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = False Then
                Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If


            Dim objTIGRA As Control = Page.FindControl("tigra")
            If (Not objTIGRA Is Nothing) Then
                StyleRow.Visible = True
            Else
                StyleRow.Visible = False
            End If



            If Page.IsPostBack = False Then
                Title1.DisplayHelp = "DisplayHelp_ManageTabs"
                PlaceHolder1.Visible = True
                PlaceHolder2.Visible = False
                ViewState("RawUrl") = Request.RawUrl

                Dim objAdmin As New AdminDB()
                Dim TempAuthLanguage As String = ""
                If PortalSettings.GetSiteSettings(_portalSettings.PortalId).ContainsKey("languageauth") Then
                    TempAuthLanguage = PortalSettings.GetSiteSettings(_portalSettings.PortalId)("languageauth")
                Else
                    TempAuthLanguage = GetLanguage("N") & ";"
                End If
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


                If ddlLanguage.Items.Count = 1 Then
                    rowlanguage.Visible = False
                Else
                    rowlanguage.Visible = True
                End If

                If Not ddlLanguage.Items.FindByText(GetLanguage("language")) Is Nothing Then
                    ddlLanguage.Items.FindByText(GetLanguage("language")).Selected = True
                Else
                    ddlLanguage.SelectedIndex = 0
                End If
                Dim TabLanguage As Hashtable
                TabLanguage = objAdmin.GetTabsName(_portalSettings.PortalId, ddlLanguage.SelectedItem.Value)
                ltabName.Text = TabLanguage(TabId)

                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm")) & "');")


                cbocss.Items.Insert(0, New ListItem(GetLanguage("list_none"), ""))
                cboskin.Items.Insert(0, New ListItem(GetLanguage("list_none"), ""))
                Dim StrFolder As String
                StrFolder = Request.MapPath(_portalSettings.UploadDirectory) & "skin"
                If System.IO.Directory.Exists(StrFolder) Then
                    Dim fileEntries As String() = System.IO.Directory.GetFiles(StrFolder)
                    Dim strFileName As String
                    For Each strFileName In fileEntries
                        Dim item As New ListItem()
                        item.Text = Mid(strFileName, InStrRev(strFileName, "\") + 1)
                        item.Value = Mid(strFileName, InStrRev(strFileName, "\") + 1)
                        If InStr(1, strFileName, ".css") Then
                            cbocss.Items.Add(item)
                        End If
                        If InStr(1, strFileName, ".skin") Then
                            cboskin.Items.Add(item)
                        End If
                    Next strFileName
                End If

                If Not cbocss.Items.FindByText(_portalSettings.ActiveTab.Css) Is Nothing Then
                    cbocss.Items.FindByText(_portalSettings.ActiveTab.Css).Selected = True
                Else
                    cbocss.Items.FindByText(GetLanguage("list_none")).Selected = True
                End If

                If Not cboskin.Items.FindByText(_portalSettings.ActiveTab.Skin) Is Nothing Then
                    cboskin.Items.FindByText(_portalSettings.ActiveTab.Skin).Selected = True
                Else
                    cbocss.Items.FindByText(GetLanguage("list_none")).Selected = True
                End If


                cboTab.DataSource = GetPortalTabs(PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N")), True)
                cboTab.DataBind()

                ' tab administrators can only manage their own tab
                If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = False Then
                    cboTab.Enabled = False
                End If

                If strAction = "" Then
                    InitializeTab()
                    cmdDelete.Visible = False
                    cboTemplate.DataSource = GetPortalTabs(PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N")), True)
                    cboTemplate.DataBind()
                    cboTemplate1.Items.Insert(0, New ListItem(GetLanguage("list_none"), ""))
                    If System.IO.Directory.Exists(Request.MapPath(_portalSettings.UploadDirectory & "skin/templates/")) Then
                        Dim fileEntries As String() = System.IO.Directory.GetFiles(Request.MapPath(_portalSettings.UploadDirectory & "skin/templates/"), "*.xml")
                        Dim strFileName As String
                        For Each strFileName In fileEntries
                            Dim item As New ListItem()
                            item.Text = Mid(strFileName, InStrRev(strFileName, "\") + 1)
                            item.Value = Mid(strFileName, InStrRev(strFileName, "\") + 1)
                            cboTemplate1.Items.Add(item)
                        Next strFileName
                     End If
                    cboTemplate1.Items.FindByText(GetLanguage("list_none")).Selected = True
                Else
                    rowTemplate.Visible = False
                    rowTemplate1.Visible = False
                    BindData()
                End If

                If Not Request.UrlReferrer Is Nothing Then
                    If InStr(Request.UrlReferrer.ToString(), "options=2") <> 0 Or InStr(Request.UrlReferrer.ToString(), "action=edit") <> 0 Then
                        ViewState("UrlReferrer") = "~" & GetDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId
                    Else
                        ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                    End If

                Else
                    ViewState("UrlReferrer") = "~" & GetDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId
                End If

                If Not (Request.Params("options") Is Nothing) And PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                    If Request.Params("options") = "2" Then
                        Options2Create()
                        ViewState("UrlReferrer") = Request.UrlReferrer.ToString().Replace("&options=2", "")
                    End If
                End If



            Else

                If txticone.Text <> "" Then
                    Dim ImageURL As String
                    ImageURL = "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & _portalSettings.UploadDirectory
                    If Not ImageURL.EndsWith("/") Then
                        ImageURL += "/"
                    End If
                    MyHtmlImage.ImageUrl = ImageURL & txticone.Text
                    MyHtmlImage.AlternateText = txticone.Text
                    MyHtmlImage.ToolTip = txticone.Text
                    MyHtmlImage.Visible = True
                Else
                    MyHtmlImage.Visible = False
                End If
                If PlaceHolder2.Visible Then
                    Title1.DisplayHelp = "DisplayHelp_ManageTabsXML"
                Else
                    Title1.DisplayHelp = "DisplayHelp_ManageTabs"
                End If


            End If

            Dim ParentID As String = Server.HtmlEncode(txticone.ClientID)
            lnkicone.NavigateUrl = "javascript:OpenNewWindow('" + TabId.ToString + "')"

            If cbocss.SelectedItem.Value <> "" Then
                editcss.NavigateUrl = "javascript:var m = window.open('admin/tabs/skinedit.aspx?L=" & GetLanguage("N") & "&file=skin/" & cbocss.SelectedItem.Value & "&TabId=" & _portalSettings.ActiveTab.TabId & "','');m.focus();"
                editcss.Text = GetLanguage("ts_editcss")
                editcss.ToolTip = GetLanguage("ts_editcssinfo")
                editcss.Visible = True
            Else
                editcss.Visible = False
            End If
            If cboskin.SelectedItem.Value <> "" Then
                editskin.NavigateUrl = "javascript:var m = window.open('admin/tabs/skinedit.aspx?L=" & GetLanguage("N") & "&file=skin/" & cboskin.SelectedItem.Value & "&TabId=" & _portalSettings.ActiveTab.TabId & "','');m.focus();"
                editskin.Visible = True
                editskin.Text = GetLanguage("ts_editskin")
                editskin.ToolTip = GetLanguage("ts_editskininfo")
            Else
                editskin.Visible = False
            End If




        End Sub


        '*******************************************************
        '
        ' The cmdCancel_Click() handler cancels operation and redirects
        ' user to admin tab of their portal.
        '
        '*******************************************************

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click, cmdCancel1.Click
            Response.Redirect(ViewState("UrlReferrer"), True)
        End Sub



        Sub InitializeTab()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Populate Tab Names, etc.
            tabName.Text = ""

            If Not cboTab.Items.FindByValue(TabId.ToString) Is Nothing Then
                   cboTab.Items.FindByValue(TabId).Selected = True
            End If

            IsVisible.Checked = True
            DisableLink.Checked = False
            txtLeftPaneWidth.Text = "200"
            txtRightPaneWidth.Text = "200"

            ' Populate checkbox list with all security roles for this portal
            ' and "check" the ones already configured for this tab
            Dim objUser As New UsersDB()
            Dim roles As SqlDataReader = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))

            ' Clear existing items in checkboxlist
            authRoles.Items.Clear()

            Dim allAuthItem As New ListItem()
            allAuthItem.Text = GetLanguage("ms_all_users")
            allAuthItem.Value = glbRoleAllUsers
            allAuthItem.Selected = True
            authRoles.Items.Add(allAuthItem)

            Dim unauthItem As New ListItem()
            unauthItem.Text = GetLanguage("ms_non_authorized")
            unauthItem.Value = glbRoleUnauthUser
            authRoles.Items.Add(unauthItem)

            Dim allAdminItem As New ListItem()
            allAdminItem.Text = GetLanguage("ms_all_users")
            allAdminItem.Value = glbRoleAllUsers
            adminRoles.Items.Add(allAdminItem)

            While roles.Read()
                Dim authItem As New ListItem()
				authItem.Text = CType(roles("RoleName"), String)
                authItem.Value = roles("RoleID").ToString()
                If authItem.Value = _portalSettings.AdministratorRoleId.ToString Then
                    authItem.Selected = True
                End If
                If InStr(1, _portalSettings.ActiveTab.AdministratorRoles.ToString, authItem.Value) Then
                    authItem.Selected = True
                End If
                authRoles.Items.Add(authItem)

                Dim adminItem As New ListItem()
				adminItem.Text = CType(roles("RoleName"), String)
                adminItem.Value = roles("RoleID").ToString()
                If adminItem.Value = _portalSettings.AdministratorRoleId.ToString Then
                    adminItem.Selected = True
                End If
                If InStr(1, _portalSettings.ActiveTab.AdministratorRoles.ToString, adminItem.Value) Then
                    adminItem.Selected = True
                End If
                adminRoles.Items.Add(adminItem)
            End While
            roles.Close()

        End Sub

        Private Sub cmdUpdateName_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdUpdateName.Click
        ' Obtain PortalSettings from Current Context
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		Dim objAdmin As New AdminDB()
		if ltabName.Text <> "" then
 		objAdmin.UpdateTabName(_portalSettings.PortalId, TabID, ltabName.Text , ddlLanguage.SelectedItem.Value)
		else
		objAdmin.DeleteTabName( TabID, ddlLanguage.SelectedItem.Value)
		end if
		ClearTabCache(TabId)
		End Sub

        Private Sub ddlLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLanguage.SelectedIndexChanged
    	    ' Obtain PortalSettings from Current Context
	        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			Dim TabLanguage As Hashtable
			TabLanguage = objAdmin.GetTabsName(_portalSettings.PortalId, ddlLanguage.SelectedItem.Value)
			ltabName.Text = TabLanguage(TabId)
		End Sub		 

        Private Sub cmdUpdate_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            Dim admin As New AdminDB()
			Dim TempTabName As String = tabName.Text
			Dim dr As SqlDataReader = admin.GetTabByID(TabId, GetLanguage("N"))
            If dr.Read Then
            TempTabName = dr("MTabName").ToString
            End If
            dr.Close()

			
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			SaveTabData(strAction)

			If tabName.Text = TempTabName or TabID <> _portalSettings.ActiveTab.TabId then
			' TabName did not change or TabAdmin menu
			Response.Redirect(ViewState("UrlReferrer"), True)
			else
			' TabNameChange see if need to redirect
            Dim objAdmin As New AdminDB()
			Response.Redirect(replace(ViewState("UrlReferrer"),_portalSettings.ActiveTab.FriendlyTabName, objAdmin.convertstringtounicode(tabName.Text)), True)
			end if
        End Sub

        Private Sub chkStyleMenu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkStyleMenu.CheckedChanged
		    Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Response.Redirect(FormatFriendlyURL(_PortalSettings.activetab.FriendlyTabName, _PortalSettings.activetab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "editmenu=1"), True)
        End Sub

        Private Sub cmdDelete_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

            objAdmin.DeleteTab(TabId)
            Dim dr As SqlDataReader = objAdmin.GetTabById(TabId, GetLanguage("N"))
            If Not dr.Read Then
                objAdmin.UpdatePortalTabOrder(portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N")), TabId, -2)
            End If
            dr.Close()
			ClearPortalCache(_portalSettings.PortalId)
            Response.Redirect(GetPortalDomainName(PortalAlias, Request), True)

        End Sub

        '*******************************************************
        '
        ' The SaveTabData helper method is used to persist the
        ' current tab settings to the database.
        '
        '*******************************************************

        Sub SaveTabData(ByVal strAction As String)

            Dim intTabId As Integer

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

            ' Construct AdministratorRoles String
            Dim strAdministratorRoles As String = ""

            Dim item As ListItem

            For Each item In adminRoles.Items
                ' admins always have access to all tabs
                If item.Selected = True Or item.Value = _portalSettings.AdministratorRoleId.ToString Or (PortalSecurity.IsInRole(_portalSettings.AdministratorRoleId.ToString) = False And InStr(1, _portalSettings.ActiveTab.AdministratorRoles.ToString, item.Value) <> 0) Then
                    strAdministratorRoles += item.Value & ";"
                End If
            Next item

            ' Construct AuthorizedRoles String
            Dim strAuthorizedRoles As String = ""

            For Each item In authRoles.Items
                ' admins always have access to all tabs
                If item.Selected = True Or item.Value = _portalSettings.AdministratorRoleId.ToString Or (PortalSecurity.IsInRole(_portalSettings.AdministratorRoleId.ToString) = False And InStr(1, _portalSettings.ActiveTab.AdministratorRoles.ToString, item.Value) <> 0) Then
                    strAuthorizedRoles += item.Value & ";"
                End If
            Next item

            Dim strIcon As String = ""
			
			strIcon = txtIcone.Text
			
            Dim admin As New AdminDB()

            If strAction = "edit" Then

                ' trap circular tab reference
                If (TabId <> Int32.Parse(cboTab.SelectedItem.Value)) And (Int32.Parse(cboTab.SelectedItem.Value) = -1 Or IsVisible.Checked = True) Then
                    admin.UpdateTab(TabId, tabName.Text, true, "", strAuthorizedRoles, txtLeftPaneWidth.Text, txtRightPaneWidth.Text, IsVisible.Checked, DisableLink.Checked, Int32.Parse(cboTab.SelectedItem.Value), strIcon, strAdministratorRoles, cbocss.SelectedItem.Value, cboskin.SelectedItem.Value)
                    admin.UpdatePortalTabOrder(portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N")), TabId, Int32.Parse(cboTab.SelectedItem.Value), , , IsVisible.Checked.ToString)
                End If

            Else ' add

                ' child tabs must be visible
                If Int32.Parse(cboTab.SelectedItem.Value) <> -1 Then
                    IsVisible.Checked = True
                End If
                intTabId = admin.AddTab(_portalSettings.PortalId, tabName.Text, true, "", strAuthorizedRoles, txtLeftPaneWidth.Text, txtRightPaneWidth.Text, IsVisible.Checked, DisableLink.Checked, Int32.Parse(cboTab.SelectedItem.Value), strIcon, strAdministratorRoles, intTabId)
				Dim DesktopTabs As ArrayList = portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N"))
                admin.UpdatePortalTabOrder(DesktopTabs, intTabId, Int32.Parse(cboTab.SelectedItem.Value), , , IsVisible.Checked.ToString)
				
                If Int32.Parse(cboTemplate.SelectedItem.Value) <> -1 Then
                    ' copy all modules to new tab
                    admin.CopyTab(Int32.Parse(cboTemplate.SelectedItem.Value), intTabId)
                End If

                If cboTemplate1.SelectedItem.Text <> GetLanguage("list_none") Then
                    ' Make Template
                    Dim objStreamReader As StreamReader
                    objStreamReader = File.OpenText(Request.MapPath(_portalSettings.UploadDirectory & "skin/templates/") & cboTemplate1.SelectedItem.Text)
                    Dim xmlData As String = objStreamReader.ReadToEnd
                    objStreamReader.Close()
                    'Do All Language
                    Dim ObjAdmin As New AdminDB
                    Dim HashL As Hashtable = ObjAdmin.GetAvailablelanguage
                    For Each deL As DictionaryEntry In HashL
                        PopulatetabModule(xmlData, intTabId, deL.Key, _portalSettings.AdministratorRoleId, True, True)
                    Next
                    PopulatetabModule(xmlData, intTabId, "", _portalSettings.AdministratorRoleId, True, True)

                End If
            End If
		ClearPortalCache(_portalSettings.PortalId)
        End Sub

        '*******************************************************
        '
        ' The BindData helper method is used to update the tab's
        ' layout panes with the current configuration information
        '
        '*******************************************************
        Sub BindData()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

            Dim tab As TabSettings = _portalSettings.ActiveTab


			' Populate Tab Names, etc.

			Dim admin As New AdminDB()
			Dim dr As SqlDataReader = admin.GetTabByID(TabId, GetLanguage("N"))
            If dr.Read Then
            tabName.Text = dr("MTabName").ToString
            End If
            dr.Close()



           
			TxtIcone.Text = tab.IconFile
			If tab.IconFile <> ""
			Dim ImageURL As STring
    		ImageUrl =  "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & _portalSettings.UploadDirectory 
    		If Not ImageUrl.EndsWith("/") Then
          		ImageUrl += "/"
   			End If
		  	myHtmlImage.ImageUrl = ImageUrl & tab.IconFile
		   	myHtmlImage.AlternateText = tab.IconFile
	   		myHtmlImage.ToolTip = tab.IconFile
			MyHtmlImage.Visible = True
			else
			MyHtmlImage.Visible = False
			end if
			
            cboTab.Items.FindByValue(tab.ParentId).Selected = True
            IsVisible.Checked = tab.IsVisible
            DisableLink.Checked = tab.DisableLink
            txtLeftPaneWidth.Text = tab.LeftPaneWidth
            txtRightPaneWidth.Text = tab.RightPaneWidth

            Dim objUser As New UsersDB()
            Dim roles As SqlDataReader = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))

            ' Populate AuthorizedRoles, AdministratorRoles
            authRoles.Items.Clear()
            adminRoles.Items.Clear()

            Dim allAuthItem As New ListItem()
            allAuthItem.Text = GetLanguage("ms_all_users")
            allAuthItem.Value = glbRoleAllUsers
            If tab.AuthorizedRoles.LastIndexOf(allAuthItem.Value & ";") > -1 Then
                allAuthItem.Selected = True
            End If
            authRoles.Items.Add(allAuthItem)

            Dim unauthItem As New ListItem()
            unauthItem.Text = GetLanguage("ms_non_authorized")
            unauthItem.Value = glbRoleUnauthUser
            If tab.AuthorizedRoles.LastIndexOf(unauthItem.Value & ";") > -1 Then
                unauthItem.Selected = True
            End If
            authRoles.Items.Add(unauthItem)


            Dim allAdminItem As New ListItem()
            allAdminItem.Text = GetLanguage("ms_all_users")
            allAdminItem.Value = glbRoleAllUsers
            If tab.AdministratorRoles.LastIndexOf(allAdminItem.Value & ";") > -1 Then
                allAdminItem.Selected = True
            End If
            adminRoles.Items.Add(allAdminItem)

            While roles.Read()
                Dim authItem As New ListItem()
				authItem.Text = CType(roles("RoleName"), String)
                authItem.Value = roles("RoleID").ToString()
                If tab.AuthorizedRoles.LastIndexOf(authItem.Value & ";") > -1 Then
                    authItem.Selected = True
                End If
                authRoles.Items.Add(authItem)

                Dim adminItem As New ListItem()
				adminItem.Text = CType(roles("RoleName"), String)
                adminItem.Value = roles("RoleID").ToString()
                If tab.AdministratorRoles.LastIndexOf(adminItem.Value & ";") > -1 Then
                    adminItem.Selected = True
                End If
                adminRoles.Items.Add(adminItem)
            End While

            roles.Close()

        End Sub

        Private Sub cmdXML2_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdXML2.Click
            Dim SiteSetting As Boolean = False
            Dim AddTabs As Boolean = False
            Dim AddModules As Boolean = False
            Dim AddInfo As Boolean = False
            Dim item As ListItem
            For Each item In CheckBoxList1.Items
                If item.Selected Then
                    Select Case item.Value
                        Case "0"
                            SiteSetting = True
                        Case "1"
                            AddTabs = True
                        Case "2"
                            AddModules = True
                        Case "3"
                            AddInfo = True
                    End Select
                End If
            Next item


            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim ObjAdmin As New AdminDB()
            Dim HashL As Hashtable = ObjAdmin.GetAvailablelanguage


            Dim i As Integer


            XMLTextBox.Text = "<?xml version=""1.0"" encoding=""utf-8""?>" & ControlChars.CrLf & "<portal>" & ControlChars.CrLf

            ' Put in the site module setting

            If PortalSettings.GetSiteSettings(_portalSettings.PortalId).Count > 0 And SiteSetting Then
                For Each de As DictionaryEntry In PortalSettings.GetSiteSettings(_portalSettings.PortalId)
                    XMLTextBox.Text += " <sitesettings>" & ControlChars.CrLf
                    XMLTextBox.Text += "   <settingname>" & de.Key & "</settingname>" & ControlChars.CrLf
                    XMLTextBox.Text += "   <settingvalue><![CDATA[" & de.Value & "]]></settingvalue>" & ControlChars.CrLf
                    XMLTextBox.Text += " </sitesettings>" & ControlChars.CrLf
                Next de
            End If


            XMLTextBox.Text += " <tabs language=''>"
            Dim DesktopTabs As ArrayList = PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N"))
            For i = 0 To DesktopTabs.Count - 1
                Dim tab As TabStripDetails = CType(DesktopTabs(i), TabStripDetails)
                If tab.IsVisible Then
                    If AddTabs Then
                        XMLTextBox.Text += MakeTabXML(tab.TabId, "")
                    ElseIf tab.TabId = _portalSettings.ActiveTab.TabId Then
                        XMLTextBox.Text += MakeTabXML(tab.TabId, "")
                    End If
                End If
            Next i
            XMLTextBox.Text += ControlChars.CrLf & " </tabs>" & ControlChars.CrLf





            For Each deL As DictionaryEntry In HashL
                XMLTextBox.Text += " <tabs language='" & deL.Key & "'>" & ControlChars.CrLf
                For i = 0 To DesktopTabs.Count - 1
                    Dim tab As TabStripDetails = CType(DesktopTabs(i), TabStripDetails)
                    If tab.IsVisible Then
                        If AddTabs Then
                            XMLTextBox.Text += MakeTabXML(tab.TabId, deL.Key)
                        ElseIf tab.TabId = _portalSettings.ActiveTab.TabId Then
                            XMLTextBox.Text += MakeTabXML(tab.TabId, deL.Key)
                        End If
                    End If
                Next i
                XMLTextBox.Text += ControlChars.CrLf & " </tabs>" & ControlChars.CrLf
            Next deL


            XMLTextBox.Text += "</portal>" & ControlChars.CrLf
        End Sub

        Private Sub cmdXML3_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdXML3.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim SiteSetting As Boolean = False
            Dim AddTabs As Boolean = False
            Dim AddModules As Boolean = False
            Dim AddInfo As Boolean = False
            Dim item As ListItem
            For Each item In CheckBoxList1.Items
                If item.Selected Then
                    Select Case item.Value
                        Case "0"
                            SiteSetting = True
                        Case "1"
                            AddTabs = True
                        Case "2"
                            AddModules = True
                        Case "3"
                            AddInfo = True
                    End Select
                End If
            Next item
            PopulateSiteModule(XMLTextBox.Text, _portalSettings.PortalId, GetLanguage("N"), _portalSettings.AdministratorRoleId, _portalSettings.AdministratorId, SiteSetting, AddTabs, AddModules, AddInfo)
            ClearPortalCache(_portalSettings.PortalId)
            ' Response.Redirect(ViewState("RawUrl"), True)
        End Sub

        Private Sub cmdXML4_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdXML4.Click
            'Save or GetXML From disk
            If XMLsaveBox.Text <> "" Then
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                If Not System.IO.Directory.Exists(Request.MapPath(_portalSettings.UploadDirectory & "skin/templates/")) Then
                    ' create the subdirectory for the xml.file
                    System.IO.Directory.CreateDirectory(Request.MapPath(_portalSettings.UploadDirectory & "skin/templates/"))
                End If

                Dim objStream As StreamWriter
                objStream = File.CreateText(Request.MapPath(_portalSettings.UploadDirectory & "skin/templates/" & XMLsaveBox.Text & ".xml"))
                objStream.WriteLine(XMLTextBox.Text)
                objStream.Close()
            End If

            Response.Redirect(ViewState("RawUrl"), True)
        End Sub


        Private Sub Options2Create()
            PlaceHolder1.Visible = False
            PlaceHolder2.Visible = True
            Title1.DisplayHelp = "DisplayHelp_ManageTabsXML"
            cmdXML2.Text = GetLanguage("Generate_XML")
            cmdXML2.Visible = False
            cmdXML3.Text = "Update with XML"
            cmdXML3.Visible = False
            XMLsaveBox.Text = tabName.Text
            cmdXML4.Text = "Save to Disk"
            cmdCancel1.Text = GetLanguage("annuler")
            CheckBoxList1.Visible = False
            CheckBoxList1.Items.Clear()
            Dim I As Integer
            For I = 0 To 3
                Dim item As New ListItem()
                Select Case I
                    Case 0
                        item.Text = "Add SiteSetting"
                    Case 1
                        item.Text = "AddTabs"
                    Case 2
                        item.Text = "AddModules"
                    Case 3
                        item.Text = "AddInfo"
                End Select
                item.Value = I.ToString()
                CheckBoxList1.Items.Add(item)
            Next I


            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim ObjAdmin As New AdminDB()
            Dim HashL As Hashtable = ObjAdmin.GetAvailablelanguage


            XMLTextBox.Text = "<?xml version=""1.0"" encoding=""utf-8""?>" & ControlChars.CrLf & "<portal>" & ControlChars.CrLf


            XMLTextBox.Text += " <tabs language=''>"
            XMLTextBox.Text += MakeTabXML(_portalSettings.ActiveTab.TabId, "")
            XMLTextBox.Text += ControlChars.CrLf & " </tabs>" & ControlChars.CrLf

            For Each deL As DictionaryEntry In HashL
                XMLTextBox.Text += " <tabs language='" & deL.Key & "'>" & ControlChars.CrLf
                XMLTextBox.Text += MakeTabXML(_portalSettings.ActiveTab.TabId, deL.Key)
                XMLTextBox.Text += ControlChars.CrLf & " </tabs>" & ControlChars.CrLf
            Next deL


            XMLTextBox.Text += "</portal>" & ControlChars.CrLf
        End Sub

        Private Sub cmdXML_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdXML.Click
            Options2Create()
            If Not Request.UrlReferrer Is Nothing Then
                ViewState("UrlReferrer") = Request.UrlReferrer.ToString()

            Else
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                ViewState("UrlReferrer") = "~" & GetDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId
            End If
        End Sub

        Private Sub PopulateSiteModule(ByVal xmlData As String, ByVal intPortalId As Integer, ByVal Language As String, ByVal strEditRoles As String, ByVal TAdministratorID As Integer, ByVal UpdateSiteSetting As Boolean, ByVal AddTabs As Boolean, ByVal AddModules As Boolean, ByVal AddInfo As Boolean)
            Dim xmlDoc As New XmlDocument()
            Dim nodeTab As XmlNode
            Dim nodePane As XmlNode
            Dim nodeModule As XmlNode
            Dim nodeData As XmlNode
            Dim nodeSetting As XmlNode
            Dim nodeSiteSetting As XmlNode
            Dim intTabId As Integer
            Dim dr As SqlDataReader
            Dim Admin As New AdminDB()
            Dim TempModuleID As Integer
            Dim TempDefinition As String
            Dim GotATab As Boolean = False


            xmlDoc.Load(New StringReader(xmlData))

            ' Update Portal Settings if present
            XMLTextBox.Text = ""
            If UpdateSiteSetting Then
                For Each nodeSiteSetting In xmlDoc.SelectNodes("//portal/sitesettings")
                    Admin.UpdatePortalSetting(intPortalId, nodeSiteSetting.Item("settingname").InnerText, nodeSiteSetting.Item("settingvalue").InnerText)
                Next
            End If


            Dim NodeToLoad As String = "//portal/tabs[@language = '" & Language & "']/tab"

            Dim elemList As XmlNodeList

            elemList = xmlDoc.SelectNodes(NodeToLoad)
            If Not elemList.Count > 0 Then
                NodeToLoad = "//portal/tabs[@language = '']/tab"
            End If


            Dim TempTabID As Integer
            intTabId = -1
            Dim intTabOrder As Integer = 1
            For Each nodeTab In xmlDoc.SelectNodes(NodeToLoad)
                TempTabID = -1
                intTabOrder += 2
                GotATab = False
                dr = Admin.GetTabByName(nodeTab.Item("name").InnerText, intPortalId)
                If dr.Read Then
                    intTabId = dr("TabId")
                    GotATab = True
                Else
                    ' add tab
                    If AddTabs Then
                        If nodeTab.Item("parent").InnerText <> "" Then
                            Dim drs As SqlDataReader = Admin.GetTabByName(nodeTab.Item("parent").InnerText, intPortalId)
                            If drs.Read Then
                                TempTabID = drs("TabId")
                            End If
                            drs.Close()
                        End If
                        intTabId = Admin.AddTab(intPortalId, nodeTab.Item("name").InnerText, True, Admin.convertstringtounicode(nodeTab.Item("name").InnerText.ToLower), "-1;", "200", "200", CBool(nodeTab.Item("visible").InnerText), False, TempTabID, "", strEditRoles, intTabId)
                        Admin.UpdateTabOrder(intTabId, nodeTab.Item("taborder").InnerText, nodeTab.Item("level").InnerText, TempTabID)
                        GotATab = True
                    End If
                End If
                    dr.Close()
                If GotATab And AddModules Then
                    For Each nodePane In nodeTab.SelectSingleNode("panes").ChildNodes
                        For Each nodeModule In nodePane.SelectSingleNode("modules").ChildNodes
                            ' get module definition
                            dr = Admin.GetSingleModuleDefinitionByName(Language, nodeModule.Item("definition").InnerText)
                            If dr.Read Then
                                ' add module
                                TempModuleID = Admin.AddModule(intTabId, -1, nodePane.Item("name").InnerText, nodeModule.Item("title").InnerText, dr("ModuleDefId"), 0, strEditRoles, False, Language)
                                TempDefinition = nodeModule.Item("definition").InnerText
                                If AddInfo Then
                                    For Each nodeSetting In nodeModule.SelectSingleNode("modulesettings").ChildNodes
                                        If nodeSetting.Item("settingname").InnerText <> "" Then
                                            Admin.UpdateModuleSetting(TempModuleID, nodeSetting.Item("settingname").InnerText, nodeSetting.Item("settingvalue").InnerText)
                                        End If
                                    Next
                                    For Each nodeData In nodeModule.SelectSingleNode("datas").ChildNodes
                                        If nodeData.Item("data1").InnerText <> "" Then
                                            If TempDefinition = "Forum" Then
                                                PopulateForum(TempModuleID, nodeData.SelectSingleNode("data1"))
                                            Else
                                                PopulateModule(TempModuleID, TempDefinition, nodeData.Item("data1").InnerText, nodeData.Item("data2").InnerText, nodeData.Item("data3").InnerText, nodeData.Item("data4").InnerText, nodeData.Item("data5").InnerText, nodeData.Item("data6").InnerText, TAdministratorID)
                                            End If

                                        End If
                                    Next
                                End If
                            End If
                            dr.Close()
                        Next
                    Next
                End If
            Next

        End Sub


        Private Sub PopulatetabModule(ByVal xmlData As String, ByVal TabId As Integer, ByVal Language As String, ByVal strEditRoles As String, ByVal AddModules As Boolean, ByVal AddInfo As Boolean)
            Dim xmlDoc As New XmlDocument()
            Dim nodeTab As XmlNode
            Dim nodePane As XmlNode
            Dim nodeModule As XmlNode
            Dim nodeData As XmlNode
            Dim nodeSetting As XmlNode
            Dim dr As SqlDataReader
            Dim Admin As New AdminDB()
            Dim TempModuleID As Integer
            Dim TempDefinition As String
            Dim xmllog As String = ""



            xmlDoc.Load(New StringReader(xmlData))

            Dim NodeToLoad As String = "//portal/tabs[@language = '" & Language & "']/tab"

            Dim elemList As XmlNodeList

            elemList = xmlDoc.SelectNodes(NodeToLoad)
            If elemList.Count > 0 Then




                For Each nodeTab In xmlDoc.SelectNodes(NodeToLoad)
                    xmllog += "1 "
                    For Each nodePane In nodeTab.SelectSingleNode("panes").ChildNodes
                        xmllog += "2 "
                        For Each nodeModule In nodePane.SelectSingleNode("modules").ChildNodes
                            ' get module definition
                            xmllog += "3 "
                            dr = Admin.GetSingleModuleDefinitionByName(Language, nodeModule.Item("definition").InnerText)
                            If dr.Read Then
                                ' add module
                                TempModuleID = Admin.AddModule(TabId, -1, nodePane.Item("name").InnerText, nodeModule.Item("title").InnerText, dr("ModuleDefId"), 0, strEditRoles, False, Language)
                                TempDefinition = nodeModule.Item("definition").InnerText
                                If AddInfo Then
                                    For Each nodeSetting In nodeModule.SelectSingleNode("modulesettings").ChildNodes
                                        If nodeSetting.Item("settingname").InnerText <> "" Then
                                            Admin.UpdateModuleSetting(TempModuleID, nodeSetting.Item("settingname").InnerText, nodeSetting.Item("settingvalue").InnerText)
                                        End If
                                    Next
                                    For Each nodeData In nodeModule.SelectSingleNode("datas").ChildNodes
                                        xmllog += "4 "
                                        If nodeData.Item("data1").InnerText <> "" Then
                                            If TempDefinition = "Forum" Then
                                                PopulateForum(TempModuleID, nodeData.SelectSingleNode("data1"))
                                            Else
                                                PopulateModule(TempModuleID, TempDefinition, nodeData.Item("data1").InnerText, nodeData.Item("data2").InnerText, nodeData.Item("data3").InnerText, nodeData.Item("data4").InnerText, nodeData.Item("data5").InnerText, nodeData.Item("data6").InnerText, TAdministratorID)
                                            End If

                                        End If
                                    Next
                                End If
                            End If
                            dr.Close()
                        Next
                    Next
                Next
            End If


            Dim objStream As StreamWriter
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            objStream = File.CreateText(Request.MapPath(_portalSettings.UploadDirectory & "skin/templates/xml.log"))
            objStream.WriteLine(xmllog)
            objStream.Close()

        End Sub



        Private Sub PopulateForum(ByVal TModuleID As Integer, ByVal NodeData As XmlNode)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim NodeUser As XmlNode
            Dim NodeForumGroup As XmlNode
            Dim NodeForum As XmlNode
            Dim NodePost As XmlNode
            Dim TempUserID As Integer
            Dim TempForumGroupID As Integer
            Dim TempForumID As Integer
            Dim TempThreadID As Integer = 0
            Dim objUser As New UsersDB()
            Dim dbForumUser As New ForumUserDB()
            Dim dbForum As New ForumDB()
            Dim drUser As SqlDataReader
            XMLTextBox.Text += "USERS" + ControlChars.CrLf
            For Each NodeUser In NodeData.SelectSingleNode("users").ChildNodes
                If NodeUser.Item("alias").InnerText <> "" Then
                    TempUserID = objUser.AddUser(_portalSettings.PortalId, NodeUser.Item("firstname").InnerText, NodeUser.Item("lastname").InnerText, "", "", "", "", "", "", "", NodeUser.Item("email").InnerText, NodeUser.Item("username").InnerText, Guid.NewGuid().ToString(), True, TempUserID)

                    If TempUserID >= 0 Then
                        dbForumUser.TTTForum_UserCreateUpdateDelete(TempUserID, NodeUser.Item("alias").InnerText, True, False, "", "", "", _portalSettings.TimeZone, "", "", "", "", "", "", "", NodeUser.Item("istrusted").InnerText, True, False, True, True, True, 0)
                    End If
                    XMLTextBox.Text += TempUserID.ToString + " " + NodeUser.Item("alias").InnerText + ControlChars.CrLf
                End If
            Next

            For Each NodeForumGroup In NodeData.SelectSingleNode("forumgroups")
                XMLTextBox.Text += "FORUMGROUP" + ControlChars.CrLf
                If NodeForumGroup.Item("name").InnerText <> "" Then
                    TempForumGroupID = -1
                    TempForumGroupID = dbForum.TTTForum_ForumGroupCreateUpdateDelete(-1, NodeForumGroup.Item("name").InnerText, _portalSettings.PortalId, TModuleID, _portalSettings.AdministratorId, 0)
                    XMLTextBox.Text += TempForumGroupID.ToString() + " " + NodeForumGroup.Item("name").InnerText + ControlChars.CrLf
                End If
                For Each NodeForum In NodeForumGroup.SelectSingleNode("forums").ChildNodes
                    XMLTextBox.Text += "        FORUM" + ControlChars.CrLf
                    If NodeForum.Item("name").InnerText <> "" Then
                        TempForumID = -1
                        TempForumID = dbForum.TTTForum_ForumCreateUpdateDelete(TempForumGroupID, _portalSettings.PortalId, TModuleID, NodeForum.Item("name").InnerText, NodeForum.Item("description").InnerText, _portalSettings.AdministratorId, NodeForum.Item("moderated").InnerText, NodeForum.Item("active").InnerText, False, 0, "", NodeForum.Item("private").InnerText, "", 0, -1)

                        XMLTextBox.Text += TempForumID.ToString() + "        FORUM -> " + NodeForum.Item("name").InnerText + ControlChars.CrLf
                    End If
                    XMLTextBox.Text += "            POSTS" + ControlChars.CrLf
                    For Each NodePost In NodeForum.SelectSingleNode("posts").ChildNodes

                        If NodePost.Item("subject").InnerText <> "" Then


                            drUser = objUser.GetSingleUserByUsername(_portalSettings.PortalId, NodePost.Item("alias").InnerText)
                            If drUser.Read() Then
                                TempUserID = drUser("UserID")
                            Else
                                TempUserID = _portalSettings.AdministratorId
                            End If
                            drUser.Close()
                            If NodePost.Item("thread").InnerText = "True" Then
                                TempThreadID = dbForum.TTTForum_AddPost(0, TempForumID, TempUserID, "", False, NodePost.Item("subject").InnerText, NodePost.Item("body").InnerText, False, "", 0)
                            Else
                                TempThreadID = dbForum.TTTForum_AddPost(TempThreadID, TempForumID, TempUserID, "", False, NodePost.Item("subject").InnerText, NodePost.Item("body").InnerText, False, "", 0)

                            End If
                            XMLTextBox.Text += TempUserID.ToString() + " " + TempThreadID.ToString() + "                POSTS -> " + NodePost.Item("thread").InnerText + " " + NodePost.Item("subject").InnerText + ControlChars.CrLf
                        End If
                    Next

                Next

            Next
        End Sub


        Private Sub PopulateModule(ByVal TModuleID As Integer, ByVal TDefinition As String, ByVal data1 As String, ByVal data2 As String, ByVal data3 As String, ByVal data4 As String, ByVal data5 As String, ByVal data6 As String, ByVal AdministratorID As Integer)

            Select Case TDefinition

                Case "Contacts"
                    ' Create an instance of the ContactsDB component
                    Dim contacts As New ContactsDB()
                    ' Data1 = Name  Data2 = Role Data3 = EMail Data4 = Contact1Field Data5 = Contact2Field
                    contacts.AddContact(TModuleID, AdministratorID, data1, data2, data3, data4, data5)
                Case "FAQs"
                    ' Create an instance of the FAQsDB component
                    Dim FAQs As New FAQsDB()
                    ' Data1 = QuestionField  Data2 = AnswerField
                    FAQs.AddFAQ(TModuleID, AdministratorID, data1, data2)
                Case "Hyperliens"
                    Dim links As New LinkDB()
                    ' Data1 = Title  Data2 = Link Data 3 = vieworder data4=description data5=newwindow
                    links.AddLink(TModuleID, AdministratorID, data1, data2, "", data3, data4, CType(data5, Boolean))
                Case "Calendrier"
                    Dim events As New EventsDB()
                    ' Data1 = Description  Data2 = DateTime Data 3 = Title data4=ExpiryDate data5=icone data6=alttext
                    ' AddModuleEvent(ByVal ModuleId As Integer, ByVal Description As String, ByVal DateTime As Date, ByVal Title As String, ByVal ExpireDate As String, ByVal UserName As String, ByVal Every As String, ByVal Period As String, ByVal IconFile As String, ByVal AltText as String)
                    Dim TempInteger As Integer = 0
                    Dim TempExpiryDate As String = ""
                    If data4 <> "" Then
                        If IsNumeric(data4) Then
                            TempInteger = CType(data4, Integer)
                            TempExpiryDate = formatansidate(DateTime.Now.AddDays(TempInteger).ToString("yyyy-MM-dd"))
                            TempInteger = 0
                        End If
                    End If

                    If IsNumeric(data2) Then
                        TempInteger = CType(data2, Integer)
                    End If


                    events.AddModuleEvent(TModuleID, data1, DateTime.Now.AddDays(TempInteger), data3, CheckDateSqL(TempExpiryDate), AdministratorID, "", "", data5, data6)
                Case "HTML/Texte"
                    Dim objHTML As New HtmlTextDB()
                    ' Data1 = text  Data2 = AltSummary Data 3 = AltDetail

                    objHTML.UpdateHtmlText(TModuleID, data1, data2, data3)

                Case "Image"
                    Dim objAdmin As New AdminDB()
                    objAdmin.UpdateModuleSetting(TModuleID, "src", data1)
                    objAdmin.UpdateModuleSetting(TModuleID, "alt", data2)
                    objAdmin.UpdateModuleSetting(TModuleID, "width", data3)
                    objAdmin.UpdateModuleSetting(TModuleID, "height", data4)

                Case "Babillard"
                    Dim objAnnouncements As New AnnouncementsDB()
                    ' Data1 = Title  Data2 = expireddate Data 3 = description data4=Link 
                    objAnnouncements.AddAnnouncement(TModuleID, Context.User.Identity.Name, data1, data2, data3, data4, True, "")
            End Select
        End Sub

    End Class

End Namespace