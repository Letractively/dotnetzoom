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
Imports System.IO
Namespace DotNetZoom

    Public Class ModuleSettingsPage
        Inherits DotNetZoom.PortalModuleControl


    	Protected WithEvents MyHtmlImage As System.Web.UI.WebControls.Image
		Protected WithEvents txticone As System.Web.UI.WebControls.TextBox
		Protected WithEvents lnkicone As System.Web.UI.WebControls.hyperlink
		

		Protected WithEvents ddlLanguage As System.Web.UI.WebControls.DropDownList	
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkShowTitle As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkAllTabs As System.Web.UI.WebControls.CheckBox
        Protected WithEvents cboPersonalize As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtCacheTime As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkAuthViewRoles As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents chkAuthEditRoles As System.Web.UI.WebControls.CheckBoxList
       
        Protected WithEvents cboTab As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
		
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
		Protected WithEvents ContainerEdit As DotNetZoom.ModuleEdit
		
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
        ' to populate the module settings on the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Title1.DisplayHelp = "DisplayHelp_ModuleSettings"
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			lnkicone.Tooltip = GetLanguage("select_icone_tooltip")	
			lnkicone.Text = GetLanguage("select_icone")	

			cmdUpdate.Text = getLanguage("enregistrer")
			cmdCancel.Text = getlanguage("annuler")
			cmdDelete.Text = getlanguage("delete")
			cboPersonalize.Items.FindByValue("0").Text = GetLanguage("ms_open")
			cboPersonalize.Items.FindByValue("1").Text = GetLanguage("ms_close")
			cboPersonalize.Items.FindByValue("2").Text = GetLanguage("ms_none")

			
			
            ' Verify that the current user has access to edit this module
            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = False And PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = False Then
                Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If

            If Page.IsPostBack = False Then
			    
                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm")) & "');")


                cboTab.DataSource = GetPortalTabs(portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N")), , True)
                cboTab.DataBind()

                ' tab administrators can only manage their own tab
                If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = False Then
                    cboTab.Enabled = False
                End If

                
					Dim objAdmin As New AdminDB()
					Dim TempAuthLanguage As String = ""
					If portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey("languageauth") then
					TempAuthLanguage = portalSettings.GetSiteSettings(_portalSettings.PortalID)("languageauth")
					else
					TempAuthLanguage = GetLanguage("N") & ";"
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

                ddlLanguage.Items.Add("<" & GetLanguage("all") & ">")
                ddlLanguage.Items.FindByText("<" & GetLanguage("all") & ">").Value = ""
				
				
                If ModuleId <> -1 Then
                    BindData()
					ContainerEdit.ModuleID = ModuleId
					ContainerEdit.ModuleTitle = txtTitle.Text
					ContainerEdit.TabID = TabID
                Else
                    chkShowTitle.Checked = True
                    cboPersonalize.SelectedIndex = 2 ' none
                    chkAllTabs.Checked = False
                    cmdDelete.Visible = False
 		       		If Not ddlLanguage.Items.FindByText(GetLanguage("language")) Is Nothing then
					ddlLanguage.Items.FindByText(GetLanguage("language")).Selected = True
					else 
       				ddlLanguage.SelectedIndex = 0     
			    	End If
                End If

                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = "~" & GetDocument() & "?tabid=" & TabId
			End If

			If TxtIcone.Text <> ""
			Dim ImageURL As STring
    		ImageUrl =  "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & _portalSettings.UploadDirectory 
    		If Not ImageUrl.EndsWith("/") Then
          		ImageUrl += "/"
   			End If
		  	myHtmlImage.ImageUrl = ImageUrl & TxtIcone.Text
		   	myHtmlImage.AlternateText = TxtIcone.Text
	   		myHtmlImage.ToolTip = TxtIcone.Text
			MyHtmlImage.Visible = True
			else
			MyHtmlImage.Visible = False
			end if
            lnkicone.NavigateUrl = "javascript:OpenNewWindow('" + tabID.ToString + "')"


        End Sub


        '*******************************************************
        '
        ' The ApplyChanges_Click server event handler on this page is used
        ' to save the module settings into the portal configuration system
        '
        '*******************************************************

        Private Sub ApplyChanges_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click


            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			Dim Admin As New AdminDB()

                Dim item As ListItem

                ' Construct Authorized View Roles 
                Dim viewRoles As String = ""
                For Each item In chkAuthViewRoles.Items
                    If item.Selected Then
                        viewRoles = viewRoles & item.Value & ";"
                    End If
                Next item
                If viewRoles <> "" Then
                    If InStr(1, viewRoles, _portalSettings.AdministratorRoleId.ToString & ";") = 0 Then
                        viewRoles += _portalSettings.AdministratorRoleId.ToString & ";"
                    End If
                End If

                ' Construct Authorized Edit Roles
                Dim editRoles As String = ""
                For Each item In chkAuthEditRoles.Items
                    If item.Selected = True Or item.Value = _portalSettings.AdministratorRoleId.ToString Then
                        editRoles = editRoles & item.Value & ";"
                    End If
                Next item

                Dim strIcon As String = TxtIcone.Text

                ' update module
                admin.UpdateModule(ModuleId, txtTitle.Text, "", "", "", strIcon, Int32.Parse(txtCacheTime.Text), viewRoles, editRoles, false, Int32.Parse(cboTab.SelectedItem.Value), chkAllTabs.Checked, chkShowTitle.Checked, Int32.Parse(cboPersonalize.SelectedItem.Value), "", ddlLanguage.SelectedItem.Value)

                ' update tab module order ( in case module was moved to another tab )
                admin.UpdateTabModuleOrder(TabId)
				If TabId <> cboTab.SelectedItem.Value then
				ClearTabCache(cboTab.SelectedItem.Value)
				end if
				ClearTabCache(TabId)
				' Redirect to the same page to pick up changes

                ' Navigate back to admin page
                Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)

        End Sub


        '*******************************************************
        '
        ' The BindData helper method is used to populate a asp:datalist
        ' server control with the current "edit access" permissions
        ' set within the portal configuration system
        '
        '*******************************************************

        Sub BindData()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()
            Dim objUser As New UsersDB()

            Dim ViewRoles As SqlDataReader = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))

            ' Clear existing items in checkboxlist
            chkAuthViewRoles.Items.Clear()

            Dim allViewItems As New ListItem()
            allViewItems.Text = GetLanguage("ms_all_users")
            allViewItems.Value = glbRoleAllUsers

            Dim unauthViewItems As New ListItem()
            unauthViewItems.Text = GetLanguage("ms_non_authorized")
            unauthViewItems.Value = glbRoleUnauthUser

            Dim EditRoles As SqlDataReader = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))

            ' Clear existing items in checkboxlist
            chkAuthEditRoles.Items.Clear()

            Dim allEditItems As New ListItem()
            allEditItems.Text = GetLanguage("ms_all_users")
            allEditItems.Value = glbRoleAllUsers

            Dim result As SqlDataReader = objAdmin.GetModule(ModuleId)
            If result.Read() Then

				If Not ddlLanguage.Items.FindByValue(result("Language").ToString) Is Nothing then
				ddlLanguage.Items.FindByValue(result("Language").ToString).Selected = True
				else 
    			ddlLanguage.Items.FindByText("<"& GetLanguage("all") & ">").Selected = True   
				End If
			

			
                txtTitle.Text = result("ModuleTitle").ToString
				TxtIcone.Text = result("IconFile").ToString
                chkShowTitle.Checked = result("ShowTitle")
                chkAllTabs.Checked = result("AllTabs")
                txtCacheTime.Text = result("CacheTime")
                cboPersonalize.SelectedIndex = result("Personalize")
                cboTab.Items.FindByValue(result("TabId").ToString).Selected = True

                If InStr(1, IIf(IsDBNull(result("AuthorizedViewRoles")), "", result("AuthorizedViewRoles")), allViewItems.Value & ";") Then
                    allViewItems.Selected = True
                End If

                chkAuthViewRoles.Items.Add(allViewItems)
                If InStr(1, IIf(IsDBNull(result("AuthorizedViewRoles")), "", result("AuthorizedViewRoles")), unauthViewItems.Value & ";") Then
                    unauthViewItems.Selected = True
                End If
                chkAuthViewRoles.Items.Add(unauthViewItems)

                While ViewRoles.Read()

                    Dim item As New ListItem()
					item.Text = CType(ViewRoles("RoleName"), String)
                    item.Value = ViewRoles("RoleID").ToString()

                    If InStr(1, IIf(IsDBNull(result("AuthorizedViewRoles")), "", result("AuthorizedViewRoles")), item.Value & ";") Then
                        item.Selected = True
                    End If

                    chkAuthViewRoles.Items.Add(item)

                End While

                ViewRoles.Close()

                If InStr(1, IIf(IsDBNull(result("AuthorizedEditRoles")), "", result("AuthorizedEditRoles")), allEditItems.Value & ";") Then
                    allEditItems.Selected = True
                End If

                chkAuthEditRoles.Items.Add(allEditItems)
				
                While EditRoles.Read()

                    Dim item As New ListItem()
					item.Text = CType(EditRoles("RoleName"), String)
                    item.Value = EditRoles("RoleID").ToString()

                    If InStr(1, IIf(IsDBNull(result("AuthorizedEditRoles")), "", result("AuthorizedEditRoles")), item.Value & ";") Then
                        item.Selected = True
                    End If

                    chkAuthEditRoles.Items.Add(item)

                End While

                EditRoles.Close()

            End If
            result.Close()
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)
        End Sub

        Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

            Dim objAdmin As New AdminDB()
			objAdmin.DeleteModule(ModuleId)
            objAdmin.UpdateTabModuleOrder(TabId)
			ClearTabCache(TabId)

            Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)

        End Sub



		
    End Class

End Namespace