Imports System.IO
Imports System.Threading
Namespace DotNetZoom

    Public MustInherit Class LanguageDefs
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents dlTabsBottom As System.Web.UI.WebControls.DataList
		Protected WithEvents dlTabsTop As System.Web.UI.WebControls.DataList
		Protected WithEvents ddlModLanguage As System.Web.UI.WebControls.DropDownList
		Protected WithEvents ddlLanguage As System.Web.UI.WebControls.DropDownList
		Protected WithEvents ddltoLanguage As System.Web.UI.WebControls.DropDownList
		Protected WithEvents tableAddItem As System.Web.UI.HtmlControls.HtmlTable
		Protected WithEvents cbolanguage As System.Web.UI.WebControls.DropDownList
		Protected WithEvents cboLongLanguage As System.Web.UI.WebControls.DropDownList
        Protected WithEvents gdrLanguage As System.Web.UI.WebControls.DataGrid
		Protected WithEvents gdrAdmin As System.Web.UI.WebControls.DataGrid
		Protected WithEvents gdrTimeZone As System.Web.UI.WebControls.DataGrid
		Protected WithEvents gdrCountryCode As System.Web.UI.WebControls.DataGrid
		Protected WithEvents cboCountry As System.Web.UI.WebControls.DropDownList
		Protected WithEvents gdrRegionCode As System.Web.UI.WebControls.DataGrid
		Protected WithEvents gdrCurrencyCode As System.Web.UI.WebControls.DataGrid
		Protected WithEvents gdrLogCode As System.Web.UI.WebControls.DataGrid
		Protected WithEvents gdrCodeFrequency As System.Web.UI.WebControls.DataGrid
		Protected WithEvents txtSettingName As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtSettingValue As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtRegionCode As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtRegionValue As System.Web.UI.WebControls.TextBox
		Protected WithEvents SaveRegionCode As System.Web.UI.WebControls.ImageButton
		
		Protected WithEvents txtContextValue As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtLongLanguage As System.Web.UI.WebControls.TextBox
		Protected WithEvents CreateSetting As System.Web.UI.WebControls.ImageButton
		Protected WithEvents AddSetting As System.Web.UI.WebControls.ImageButton
		Protected WithEvents GenerateScript As System.Web.UI.WebControls.Button
		Protected WithEvents SaveLongSetting As System.Web.UI.WebControls.ImageButton
		Protected WithEvents SettingToSave As System.Web.UI.WebControls.TextBox
		Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
		Protected WithEvents lblDelLang As System.Web.UI.WebControls.Label
		Protected WithEvents ddlDelLang As System.Web.UI.WebControls.DropDownList
		Protected WithEvents DelLang As System.Web.UI.WebControls.ImageButton
		Protected WithEvents DelLongLang As System.Web.UI.WebControls.ImageButton
        Protected WithEvents Setting1 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting2 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting3 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting4 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting5 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting6 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting7 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting8 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting9 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Setting10 As System.Web.UI.WebControls.PlaceHolder

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
        ' The Page_Load server event handler on this user control is used
        ' to populate the current defs settings from the configuration system
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' Verify that the current user has access to this page
            If Not PortalSecurity.IsSuperUser Then
                EditDenied()
            End If

            If Session("language") Is Nothing Then
                ' in case the session expired
                Session("language") = GetLanguage("N")
            End If
            Dim TabSelectedIndex As Integer = 0

            If Page.IsPostBack = False Then


                If Request("TabSelected") <> "" Then
                    TabSelectedIndex = Request("TabSelected")
                End If
                GetTabVisible((TabSelectedIndex).ToString)

                ddlModLanguage = Setddl(ddlModLanguage)
                If Not ddlModLanguage.Items.FindByValue(Session("language")) Is Nothing Then
                    ddlModLanguage.Items.FindByValue(Session("language")).Selected = True
                Else
                    ddlModLanguage.SelectedIndex = 0
                End If
                lblMessage.Text = ""
                Session("DataTable") = Nothing

                Dim slTabs As SortedList = New SortedList

                slTabs.Add("0", GetSubLink("0", GetLanguage("LanguageSettings1")))
                slTabs.Add("1", GetSubLink("1", GetLanguage("LanguageSettings2")))
                slTabs.Add("2", GetSubLink("2", GetLanguage("LanguageSettings3")))
                slTabs.Add("3", GetSubLink("3", GetLanguage("LanguageSettings4")))
                slTabs.Add("4", GetSubLink("4", GetLanguage("LanguageSettings5")))
                If TabSelectedIndex > 4 Then
                    dlTabsTop.DataSource = slTabs
                    dlTabsTop.DataBind()
                Else
                    dlTabsBottom.DataSource = slTabs
                    dlTabsBottom.DataBind()
                    dlTabsBottom.SelectedIndex = TabSelectedIndex
                End If

                slTabs = New SortedList
                slTabs.Add("0", GetSubLink("5", GetLanguage("LanguageSettings6")))
                slTabs.Add("1", GetSubLink("6", GetLanguage("LanguageSettings7")))
                slTabs.Add("2", GetSubLink("7", GetLanguage("LanguageSettings8")))
                slTabs.Add("3", GetSubLink("8", GetLanguage("LanguageSettings9")))
                slTabs.Add("4", GetSubLink("9", GetLanguage("LanguageSettings10")))

                If TabSelectedIndex > 4 Then
                    dlTabsBottom.DataSource = slTabs
                    dlTabsBottom.DataBind()
                    dlTabsBottom.SelectedIndex = TabSelectedIndex - 5
                Else
                    dlTabsTop.DataSource = slTabs
                    dlTabsTop.DataBind()
                End If
            Else
                Title1.DisplayHelp = ViewState("title")
            End If

        End Sub


        Private Sub GetTabVisible( ByVal CommandName As String)
			Setting1.Visible = False
			Setting2.Visible = False
			Setting3.Visible = False
			Setting4.Visible = False
			Setting5.Visible = False
			Setting6.Visible = False
			Setting7.Visible = False
			Setting8.Visible = False
			Setting9.Visible = False
			Setting10.Visible = False
			
		   Title1.DisplayHelp = "DisplayHelp_Language" & CommandName
		   ViewState("title") = Title1.DisplayHelp	
		   Select CommandName
		   Case "0"
		   Setting1.Visible = True
		   
		   DataBind1()
		   Case "1"
		   Setting2.Visible = True
		   
		   DataBind2()
		   Case "2"
		   Setting3.Visible = True
		   
		   DataBind3()
		   Case "3"
		   Setting4.Visible = True
		   
		   DataBind4()
		   Case "4"
		   Setting5.Visible = True
		   
		   DataBind5()
		   Case "5"
		   Setting6.Visible = True
		   
		   DataBind6()
		   Case "6"
		   Setting7.Visible = True
		   
		   DataBind7()
		   Case "7"
		   Setting8.Visible = True
		   
		   DataBind8()
		   Case "8"
		   Setting9.Visible = True
		  
		   DataBind9()
		   Case "9"
		   Setting10.Visible = True
		
		   DataBind10()
		   end select
		end sub

		Private Sub DataBind1()
		CreateSetting.ToolTip = GetLanguage("language_clicktocreate")
		GenerateScript.ToolTip = GetLanguage("language_clicktogenerate") & Session("language")
		GenerateScript.Text = GetLanguage("language_create") & Session("language")
		Dim objAdmin As New AdminDB()
		lblDelLang.Text = GetLanguage("language_del")
		lblDelLang.Visible = True
		ddlDelLang.Visible = True
		DelLang.Visible = True

            ddlDelLang = Setddl(ddlDelLang)

   		ddlDelLang.SelectedIndex = 0     
		DelLang.ToolTip = GetLanguage("language_clicktodel") & " " & ddlDelLang.SelectedItem.Value
			
		if ddlDelLang.Items.Count = 1 then
			lblDelLang.Visible = False
			ddlDelLang.Visible = False
			DelLang.Visible = False
		end if
			
            ddlLanguage = Setddl(ddlLanguage)
       	If Not ddlLanguage.Items.FindByValue(Session("language")) Is Nothing then
			ddlLanguage.Items.FindByValue(Session("language")).Selected = True
		else 
    		ddlLanguage.SelectedIndex = 0     
	    End If
            Dim dtl As New Data.DataTable
            Dim dr As Data.DataRow
            dtl.Columns.Add(New Data.DataColumn("language", GetType(String)))
            dtl.Columns.Add(New Data.DataColumn("SettingValue", GetType(String)))

	    Dim ci As System.Globalization.CultureInfo



   		For Each ci In  System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.NeutralCultures)
		 If ci.Name <> "" and  ddlLanguage.Items.FindByValue(ci.Name) is nothing then
                    dr = dtl.NewRow()
		    dr("language") = ci.Name
		    dr("SettingValue") = ci.DisplayName
		    dtl.Rows.Add(dr)
		 end if
		Next ci
	    For Each ci In  System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures)
		 If ci.Name <> "" and  ddlLanguage.Items.FindByValue(ci.Name) is nothing then
		 	dr = dtl.NewRow()
			dr("language") = ci.Name
			dr("SettingValue") = ci.DisplayName
			dtl.Rows.Add(dr)
		 end if
    	Next ci
            Dim dvl As New Data.DataView(dtl)
        dvl.Sort = "SettingValue"

		ddltoLanguage.DataSource = dvl
		ddltoLanguage.DataBind()
		end sub
		
		
		Private Sub DataBind2()
		If Not Page.IsPostBack then
		SaveLongSetting.ToolTip = GetLanguage("enregistrer")
		end if
		DelLongLang.Visible = False
		Dim objAdmin As New AdminDB()
		cboLonglanguage.Items.Clear()
		cboLonglanguage.DataSource = objAdmin.GetlonglanguageSettings(Session("language"))
		cboLonglanguage.DataBind()
		cboLonglanguage.Items.Add(GetLanguage("list_none"))
		cboLonglanguage.Items.FindByText(GetLanguage("list_none")).Selected = True
		end sub

    Private Sub DelLongLang_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles DelLongLang.Click
		lblMessage.Text = Sender.CommandName + " " + GetLanguage("delete")
		Dim TempScript as String
		Dim objAdmin As New AdminDB()
		TempScript = "DELETE FROM longlanguageSettings WHERE language = '" & Session("language") & "' and portalId is null and SettingName = '" & MakeSQLFriendly(Sender.CommandName) & "'" &   vbCrLf
		lblMessage.Text += " " & objAdmin.ExecuteSQLScript(TempScript)
        txtLongLanguage.Text = ""
		SettingToSave.Text = ""			
    	DataBind2()
	end Sub
	
		Private Sub DataBind3()
		AddSetting.ToolTip = GetLanguage("language_clicktoadd")
		tableAddItem.Visible = False
		BindData(GetLanguage("list_none"))
		end sub

		
		Private Sub DataBind4()
		Dim objAdmin As New AdminDB()

		gdrCountryCode.DataSource =  objAdmin.GetCountryCodes(Session("language"))
		gdrCountryCode.DataBind()

		end sub
	
		Private Sub gdrCountryCode_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gdrCountryCode.ItemCommand
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			
			Select Case e.CommandName
				Case "EditOK"
		        ObjAdmin.UpdateCountryCodes(Session("language"), e.CommandArgument, GetTextBox(e.Item, "txtRename").Text)
			    lblMessage.Text = GetTextBox(e.Item, "txtRename").Text + " " + GetLanguage("enregistrer")
				DataBind4()
				End Select

   		End Sub
	
		
		Private Sub DataBind5()
		Dim objAdmin As New AdminDB()
	    gdrAdmin.Columns(0).HeaderText = GetLanguage("MS_ModuleName")
		gdrAdmin.Columns(1).HeaderText = GetLanguage("MS_Module_Desc")
        gdrAdmin.DataSource =  ObjAdmin.GetAdminModuleDefinitions(Session("language"))
		gdrAdmin.DataBind()	
		end sub

		Private Sub DataBind6()
		Dim objAdmin As New AdminDB()
		gdrCurrencyCode.DataSource =  objAdmin.GetCurrencies(Session("language"))
		gdrCurrencyCode.DataBind()
		end sub
	
		Private Sub gdrCurrencyCode_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gdrCurrencyCode.ItemCommand
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			
			Select Case e.CommandName
				Case "EditOK"
		        ObjAdmin.UpdateCurrencies(Session("language"), e.CommandArgument, GetTextBox(e.Item, "txtRename").Text)
			    lblMessage.Text = GetTextBox(e.Item, "txtRename").Text + " " + GetLanguage("enregistrer")
				DataBind6()
				End Select

   		End Sub

		Private Sub DataBind7()
		Dim objAdmin As New AdminDB()
		gdrCodeFrequency.DataSource =  objAdmin.GetBillingFrequencyCodes(Session("language"))
		gdrCodeFrequency.DataBind()
		end sub
		
		Private Sub gdrCodeFrequency_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gdrCodeFrequency.ItemCommand
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			
			Select Case e.CommandName
				Case "EditOK"
		        ObjAdmin.UpdateBillingFrequencyCodes(Session("language"), e.CommandArgument, GetTextBox(e.Item, "txtRename").Text)
			    lblMessage.Text = GetTextBox(e.Item, "txtRename").Text + " " + GetLanguage("enregistrer")
				DataBind7()
				End Select

   		End Sub

		Private Sub DataBind8()
		Dim objAdmin As New AdminDB()
		gdrLogCode.DataSource =  objAdmin.GetSiteLogReports(Session("language"))
		gdrLogCode.DataBind()
		end sub
		
		Private Sub gdrLogCode_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gdrLogCode.ItemCommand
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			
			Select Case e.CommandName
				Case "EditOK"
		        ObjAdmin.UpdateSiteLogReports(Session("language"), e.CommandArgument, GetTextBox(e.Item, "txtRename").Text)
			    lblMessage.Text = GetTextBox(e.Item, "txtRename").Text + " " + GetLanguage("enregistrer")
				DataBind8()
				End Select
   		End Sub
		
		Private Sub DataBind9()
		Dim objAdmin As New AdminDB()
		Dim TempCountryCode as String = "CA"
		If Not Page.IsPostBack then
        cboCountry.DataSource = objAdmin.GetCountryCodes(Session("language"))
        cboCountry.DataBind()
		SaveRegionCode.AlternateText = GetLanguage("enregistrer")
		SaveRegionCode.ToolTip = GetLanguage("enregistrer")
		SaveRegionCode.CommandName = "newcode"
		txtRegionValue.Text = ""
		txtRegionCode.Text = ""
        If Not cboCountry.Items.FindByValue(DisplayCountryCode(Request.UserHostAddress)) Is Nothing Then
               TempCountryCode = DisplayCountryCode(Request.UserHostAddress)
			   cboCountry.ClearSelection()
               cboCountry.Items.FindByValue(TempCountryCode).Selected = True
		Else
			   cboCountry.ClearSelection()
               cboCountry.Items.FindByValue(TempCountryCode).Selected = True
        End If
		else
		TempCountryCode = cboCountry.SelectedItem.Value
		end if
		SaveRegionCode.CommandArgument = TempCountryCode
		gdrRegionCode.DataSource =  objAdmin.GetRegionCodes(TempCountryCode, Session("language"))
		gdrRegionCode.DataBind()
		end sub

        Private Sub cboCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCountry.SelectedIndexChanged
        DataBind9()
        End Sub
		
    Private Sub SaveRegionCode_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SaveRegionCode.Click
		If txtRegionValue.Text <> "" and txtRegionCode.Text <> "" then
		Dim objAdmin As New AdminDB()
	    ObjAdmin.UpdateRegionCodes(Session("language"), sender.CommandArgument, txtRegionCode.Text, txtRegionValue.Text)
		lblMessage.Text = txtRegionCode.Text + " " + txtRegionValue.Text + " " + GetLanguage("enregistrer")
		DataBind9()
		end if
		txtRegionValue.Text = ""
		txtRegionCode.Text = ""
	end sub		

	      Private Sub gdr_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gdrRegionCode.ItemCreated, gdrLanguage.ItemCreated
			
            Dim imgdelete As Control = e.Item.FindControl("imgdelete")
			
			If Not imgdelete Is Nothing Then
                    CType(imgdelete, ImageButton).Attributes.Add("onClick", "javascript: return confirm('" & rtesafe(GetLanguage("request_confirm")) & "')")
            End If

         End Sub
	
		Private Sub gdrRegionCode_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gdrRegionCode.ItemCommand
			Dim objAdmin As New AdminDB()
			gdrRegionCode.EditItemIndex = -1
            gdrRegionCode.SelectedIndex = e.Item.ItemIndex
			Select Case e.CommandName
				Case "EditOK"
				If GetTextBox(e.Item, "txtRename").Text <> "" then
	    		ObjAdmin.UpdateRegionCodes(Session("language"), cboCountry.SelectedItem.Value, e.CommandArgument, GetTextBox(e.Item, "txtRename").Text)
				lblMessage.Text = e.CommandArgument + " " + GetTextBox(e.Item, "txtRename").Text + " " + GetLanguage("enregistrer")
				end if
				Case "Edit"
				gdrRegionCode.EditItemIndex = e.Item.ItemIndex
                gdrRegionCode.SelectedIndex = -1
				Case "delete"
				lblMessage.Text = e.CommandArgument + " " + GetTextBox(e.Item, "txtRename").Text + " " + GetLanguage("delete")
				Dim TempScript as String
				TempScript = "DELETE FROM CodeRegion WHERE language = '" & Session("language") & "' and country = '" & cboCountry.SelectedItem.Value _
			             & "' and code = '" & e.CommandArgument &"'" &   vbCrLf
				lblMessage.Text += " " & objAdmin.ExecuteSQLScript(TempScript)			
			end select
			DataBind9()

   		End Sub
		

		
		Private Sub DataBind10()
		Dim objAdmin As New AdminDB()
		gdrTimeZone.DataSource =  objAdmin.GetTimeZoneCodes(Session("language"))
		gdrTimeZone.DataBind()
		end sub

		Private Sub gdrTimeZone_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gdrTimeZone.ItemCommand
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			
			Select Case e.CommandName
				Case "EditOK"
		        ObjAdmin.UpdateTimeZoneCodes(Session("language"), e.CommandArgument, GetTextBox(e.Item, "txtRename").Text)
			    lblMessage.Text = GetTextBox(e.Item, "txtRename").Text + " " + GetLanguage("enregistrer")
				DataBind10()
				End Select

   		End Sub
		
		
		
		
		Private Sub Binddata(ByVal ToBind As String)

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim dt As New Data.DataTable()
            Dim dr As Data.DataRow
	    Dim objAdmin As New AdminDB()
		If not Session("DataTable") is nothing then
		dt = Session("DataTable")
		else
                dt.Columns.Add(New Data.DataColumn("SettingName", GetType(String)))
                dt.Columns.Add(New Data.DataColumn("SettingValue", GetType(String)))
                dt.Columns.Add(New Data.DataColumn("Context", GetType(String)))
                dt.Columns.Add(New Data.DataColumn("New", GetType(Boolean)))
                Dim Result As Data.SqlClient.SqlDataReader = objAdmin.GetlanguageContext(Session("language"))

		cbolanguage.Items.Clear()
		cbolanguage.Items.Add(GetLanguage("list_none"))
		cbolanguage.Items.Add("<"& GetLanguage("all") & ">")
        Dim BindAll As Boolean
		If ToBind = "<"& GetLanguage("all") & ">" then
		BindAll = True
		Else
		BindAll = False
		end if 

        While result.Read()
		If BindAll or result.GetString(2) = ToBind then
		dr = dt.NewRow()
        dr(0) =  result.GetString(0)
		dr(1) =  result.GetString(1)
		dr(2) =  result.GetString(2)
		dr(3) =  result.GetBoolean(3)
		dt.Rows.Add(dr)
		end if
		If Result.GetString(2) <> "" and cbolanguage.Items.FindByText(result.GetString(2)) Is Nothing Then
		' add to list
		cbolanguage.Items.Add(result.GetString(2))
		end if
        End While  
		result.Close()
		If ToBind <> "" and Not cbolanguage.Items.FindByText(ToBind) Is Nothing Then
           cbolanguage.Items.FindByText(ToBind).Selected = True
		else
		   cbolanguage.Items.FindByText("<"& GetLanguage("all") & ">").Selected = True
        End If          
		gdrLanguage.Columns(0).HeaderText = GetLanguage("language_param")
		gdrLanguage.Columns(1).HeaderText = GetLanguage("language_change")
		Session("DataTable") = dt
		end if
        Dim dv As New DataView(dt)
        dv.Sort = "SettingValue, Context"
        gdrLanguage.DataSource = dv
        gdrLanguage.DataBind()
		if ToBind = GetLanguage("list_none") then
		gdrLanguage.Visible = false
		tableAddItem.visible = False	
		end if
		End Sub		

   		
		Private Sub gdrAdmin_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gdrAdmin.ItemCommand
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			
			Select Case e.CommandName
				Case "EditOK"
		        ObjAdmin.UpdateAdminModuleDefinitions(Session("language"), e.CommandArgument, GetTextBox(e.Item, "txtFriendLyName").Text, GetTextBox(e.Item, "txtDescription").Text)
			    lblMessage.Text = GetTextBox(e.Item, "txtFriendLyName").Text + " " + GetLanguage("enregistrer")
				DataBind5()
				End Select

   		End Sub
		
   Private Sub gdrLanguage_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gdrLanguage.ItemCommand
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			Dim TempScript As String
			gdrLanguage.EditItemIndex = -1
            gdrLanguage.SelectedIndex = e.Item.ItemIndex

			Select Case e.CommandName
			    Case "Edit"
				gdrLanguage.EditItemIndex = e.Item.ItemIndex
            	gdrLanguage.SelectedIndex = -1
				Case "EditOK"
		        ObjAdmin.UpdatelanguageSetting(Session("language"), e.CommandArgument, GetTextBox(e.Item, "txtRename").Text)
			    lblMessage.Text = e.CommandArgument + " " + GetLanguage("enregistrer")
	    		' clear Table to pick up changes
				Session("DataTable") = nothing
				Case "Delete"
				TempScript = "delete from languageSettings where language = '" & Session("language") & "' and SettingName = '" &  MakeSQLFriendly(e.CommandArgument) & "'"
			    lblMessage.Text = e.CommandArgument + " " + GetLanguage("delete")
			    lblMessage.Text += " " & objAdmin.ExecuteSQLScript(TempScript)
	    		' clear Table to pick up changes
				Session("DataTable") = nothing
			End Select
			BindData(cbolanguage.SelectedItem.Value)
   End Sub

	Private Function GetTextBox(ByVal item As DataGridItem, ByVal NameOfBox As String) As TextBox
	    	Return CType(item.FindControl(NameOfBox), TextBox)
	End Function
	
	
    Private Sub AddSetting_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddSetting.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			If txtSettingName.Text <> "" and txtSettingValue.Text <> "" then
		    ObjAdmin.UpdatelanguageContext(Session("language"), txtSettingName.Text, txtSettingValue.Text, txtContextValue.Text)
			lblMessage.Text = txtSettingName.Text + " " + GetLanguage("enregistrer")
			else
			lblMessage.Text = ""
			End if
			' clear Table to pick up changes
			Session("DataTable") = nothing
			BindData(cbolanguage.SelectedItem.Value)
			txtSettingName.Text = ""
			txtSettingValue.Text = ""
	End Sub

    Private Sub SaveLongSetting_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SaveLongSetting.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			If SettingToSave.Text <> "" and txtLongLanguage.Text <> "" then
		    ObjAdmin.UpdatelonglanguageSetting(Session("language"), SettingToSave.Text , txtLongLanguage.Text)
			lblMessage.Text = SettingToSave.Text + " " + GetLanguage("enregistrer")
			else
			lblMessage.Text = ""
			Exit Sub
			End if
			If SettingToSave.Text <> cboLongLanguage.selectedItem.Value then
			cboLonglanguage.Items.Clear()
			cboLonglanguage.DataSource = objAdmin.GetlonglanguageSettings(Session("language"))
			cboLonglanguage.DataBind()
			cboLonglanguage.Items.Add(GetLanguage("list_none"))
			cboLonglanguage.Items.FindByText(SettingToSave.Text).Selected = True
			End If
			txtLongLanguage.Text = ""
			SettingToSave.Text = ""
	End Sub

    Private Sub CreateSetting_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CreateSetting.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			Dim TempScript As String			
            ClearHostCache()
			If ddltoLanguage.SelectedItem.Value <> ddlLanguage.SelectedItem.Value then
			Dim TempLanguageCode As String = ddltoLanguage.SelectedItem.Value
			' need to check if culture code OK
			If TempLanguageCode.Length < 5 then
			    Dim ci As System.Globalization.CultureInfo
			    For Each ci In System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures)
				If ci.Name <> "" and  InStr(1, ci.Name, TempLanguageCode) then
					TempLanguageCode = ci.Name
				end if
      			Next ci			
			end if
			
						
			
                TempScript = "exec AddNewLanguage '" & ddlLanguage.SelectedItem.Value & "', '" & ddltoLanguage.SelectedItem.Value & "', '" & ddltoLanguage.SelectedItem.Text & "', '" & TempLanguageCode & "', 'utf-8', ''"
			lblMessage.Text = GetLanguage("UO_From") + " " + ddlLanguage.SelectedItem.Text + " " + GetLanguage("to") +  " " + ddltoLanguage.SelectedItem.Text + " " + GetLanguage("enregistrer")
			lblMessage.Text += " " & objAdmin.ExecuteSQLScript(TempScript)
			
			Session("language") = ddltoLanguage.SelectedItem.Value
			ddlLanguage.Items.Clear()
			ddlDelLang.Items.Clear()
                ddlDelLang = Setddl(ddlDelLang)

    		ddlDelLang.SelectedIndex = 0     
			DelLang.ToolTip = GetLanguage("language_clicktodel") & " " & ddlDelLang.SelectedItem.Text
                ddlLanguage = Setddl(ddlLanguage)
        	If Not ddlLanguage.Items.FindByValue(Session("language")) Is Nothing then
			ddlLanguage.Items.FindByValue(Session("language")).Selected = True
			else 
    		ddlLanguage.SelectedIndex = 0     
		    End If
			if ddlDelLang.Items.Count = 1 then
			lblDelLang.Visible = False
			ddlDelLang.Visible = False
			DelLang.Visible = False
			end if
			End if
			ddlModLanguage.Items.Clear()
            ddlModLanguage = Setddl(ddlModLanguage)

       		If Not ddlModLanguage.Items.FindByValue(Session("language")) Is Nothing then
			  ddlModLanguage.Items.FindByValue(Session("language")).Selected = True
			else 
    		   ddlModLanguage.SelectedIndex = 0     
	       End If

			
	End Sub
    

    Private Sub GenerateScript_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GenerateScript.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
	        Dim Result As SqlDataReader = objAdmin.GetlanguageContext(Session("language"))
			Dim LongResult As SqlDataReader = objAdmin.GetlonglanguageSettings(Session("language"))
			Try
            Dim objStream As StreamWriter
            objStream = File.CreateText(Server.MapPath("Database\language_" & Session("language") & ".sql"))


			Dim TempLanguageCode As String = Session("language")
			Dim TempLanguageName As String = ""
			' need to check if culture code OK
			If TempLanguageCode.Length < 5 then
			    Dim ci As System.Globalization.CultureInfo
			    For Each ci In System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures)
				If ci.Name <> "" and  InStr(1, ci.Name, TempLanguageCode) then
					TempLanguageCode = ci.Name
					TempLanguageName = ci.DisplayName
				end if
      			Next ci			
			end if
			
						
 			objStream.WriteLine("----------------------------------------------------")
 			objStream.WriteLine("-- Update")
 			objStream.WriteLine("----------------------------------------------------")
			objStream.WriteLine("if not exists ( select * from language where language = '" & Session("language") & "' )")
 			objStream.WriteLine("begin")
 			objStream.WriteLine("insert language (") 
 			objStream.WriteLine("       Language,")
 			objStream.WriteLine("   Description,")
 			objStream.WriteLine("   CultureCode,")
 			objStream.WriteLine("   Encoding, ")
			objStream.WriteLine("   HomePage, ")
			objStream.WriteLine("   FriendlyHomePage, ")
			objStream.WriteLine("   AdminRole, ")
			objStream.WriteLine("   AdminRoleDesc, ")
			objStream.WriteLine("   UserRole, ")
			objStream.WriteLine("   UserRoleDesc ")
 			objStream.WriteLine(" )")
 			objStream.WriteLine(" values (")
 			objStream.WriteLine("   '" & Session("language") & "',")
 			objStream.WriteLine("   '" & TempLanguageName & "',")
 			objStream.WriteLine("   '" & TempLanguageCode & "',")
                objStream.WriteLine("   'utf-8', ")

			' _portalSettings.AdministratorRoleId.ToString
			
			Dim DesktopTabs As ArrayList = portalSettings.Getportaltabs(_PortalSettings.PortalID, Session("language"))
			Dim AccueilTab As TabStripDetails = CType(DesktopTabs(0), TabStripDetails)
		
			objStream.WriteLine("   '" & AccueilTab.TabName	 & "', ")
			objStream.WriteLine("   '" &   AccueilTab.FriendlyTabName  & "', ")

			' _portalSettings.AdministratorRoleId.ToString
			Dim objUser As New UsersDB()
			Dim Dr As SqlDataReader = objUser.GetSingleRole(_portalSettings.AdministratorRoleId, Session("language"))
			If dr.Read Then
			objStream.WriteLine("   '" & Dr("RoleName").ToString & "', ")
			objStream.WriteLine("   '" & Dr("Description").ToString & "', ")
			End if
			Dr.Close()

			' _portalSettings.RegisteredRoleId
			Dr = objUser.GetSingleRole(_portalSettings.RegisteredRoleId, Session("language"))
			If dr.Read Then
			objStream.WriteLine("   '" & Dr("RoleName").ToString & "', ")
			objStream.WriteLine("   '" & Dr("Description").ToString & "' ")
			end if
			Dr.Close()
			
 			objStream.WriteLine(" )")
 			objStream.WriteLine(" end")
 			objStream.WriteLine("else")
 			objStream.WriteLine("begin")
 			objStream.WriteLine(" update language")
 			objStream.WriteLine(" set ")
 			objStream.WriteLine(" Language = '" & Session("language") & "',")
 			objStream.WriteLine(" Description = '" & TempLanguageName & "',")
 			objStream.WriteLine(" CultureCode = '" & TempLanguageCode & "',")
                objStream.WriteLine(" Encoding = 'utf-8', ")

			objStream.WriteLine("   HomePage = '" & AccueilTab.TabName	 & "', ")
			objStream.WriteLine("   FriendlyHomePage = '" &   AccueilTab.FriendlyTabName  & "', ")

			' _portalSettings.AdministratorRoleId.ToString
			 Dr = objUser.GetSingleRole(_portalSettings.AdministratorRoleId, Session("language"))
			If dr.Read Then
			objStream.WriteLine("   AdminRole = '" & dr("RoleName").ToString & "', ")
			objStream.WriteLine("   AdminRoleDesc = '" & dr("Description").ToString & "', ")
			end if
			Dr.Close()

			' _portalSettings.RegisteredRoleId
			Dr = objUser.GetSingleRole(_portalSettings.RegisteredRoleId, Session("language"))
			If dr.Read Then
			objStream.WriteLine("   UserRole = '" & dr("RoleName").ToString & "', ")
			objStream.WriteLine("   UserRoleDesc = '" & dr("Description").ToString & "' ")
			end if
			Dr.Close()

 			objStream.WriteLine(" where language = '" & Session("language") & "'")
 			objStream.WriteLine(" end")





			objStream.WriteLine("GO")
			
			
			
			

	        While result.Read()
	        objStream.WriteLine("updatelanguagecontext '" & Session("language") & "','" & MakeSQLFriendly(result.GetString(0)) & "','" & MakeSQLFriendly(result.GetString(1)) & "', '" & MakeSQLFriendly(result.GetString(2)) & "'")
			objStream.WriteLine("GO")
        	End While 
			result.Close()

	        While Longresult.Read()
	        objStream.WriteLine("UpdatelonglanguageSetting '" & Session("language") & "','" & MakeSQLFriendly(Longresult.GetString(0)) & "','" & MakeSQLFriendly(Longresult.GetString(1)) & "', null ")
			objStream.WriteLine("GO")
        	End While 
			Longresult.Close() 

			result =  objAdmin.GetCountryCodes(Session("language"))
			While result.Read()
	        objStream.WriteLine("UpdateCountryCodes '" & Session("language") & "','" & MakeSQLFriendly(result.GetString(0)) & "','" & MakeSQLFriendly(result.GetString(1))& "'")
			objStream.WriteLine("GO")
					Longresult = objAdmin.GetRegionCodes(result.GetString(0), Session("language"))
					While Longresult.Read()
	        		objStream.WriteLine("UpdateRegionCodes '" & Session("language") & "','" & MakeSQLFriendly(result.GetString(0)) & "','" & MakeSQLFriendly(Longresult.GetString(0))& "','" & MakeSQLFriendly(Longresult.GetString(1))& "'")
					objStream.WriteLine("GO")
					End While
					Longresult.Close()
        	End While 
			result.Close() 

			result = ObjAdmin.GetAdminModuleDefinitions(Session("language"))
                While Result.Read()
                    ' objAdmin.UpdateAdminModuleDefinitions(               Session("language"),         e.CommandArgument,                                     GetTextBox(e.Item, "txtFriendLyName").Text,    GetTextBox(e.Item, "txtDescription").Text)
                    objStream.WriteLine("UpdateAdminModuleDefinitions '" & Session("language") & "','" & Int32.Parse(Result("ModuleDefID")).ToString & "','" & MakeSQLFriendly(Result("FriendlyName")) & "','" & MakeSQLFriendly(Result("Description")) & "'")
                    objStream.WriteLine("GO")
                End While
			result.Close() 


			result = objAdmin.GetCurrencies(Session("language"))
			While result.Read()
	        objStream.WriteLine("UpdateCurrencies '" & Session("language") & "','" & MakeSQLFriendly(result.GetString(0)) & "','" & MakeSQLFriendly(result.GetString(1))& "'")
			objStream.WriteLine("GO")
        	End While 
			result.Close() 
	
 			result = objAdmin.GetBillingFrequencyCodes(Session("language"))
			While result.Read()
	        objStream.WriteLine("UpdateBillingFrequencyCodes '" & Session("language") & "','" & MakeSQLFriendly(result.GetString(0)) & "','" & MakeSQLFriendly(result.GetString(2))& "'")
			objStream.WriteLine("GO")
        	End While 
			result.Close() 
			
			result = objAdmin.GetSiteLogReports(Session("language"))
			While result.Read()
	        objStream.WriteLine("UpdateSiteLogReports '" & Session("language") & "','" & Int32.Parse(result(0)).ToString & "','" & MakeSQLFriendly(result.GetString(1))& "'")
			objStream.WriteLine("GO")
        	End While 
			result.Close() 

			result = objAdmin.GetTimeZoneCodes(Session("language"))
			While result.Read()
	        objStream.WriteLine("UpdateTimeZoneCodes '" & Session("language") & "','" & Int32.Parse(result(0)).ToString & "','" & MakeSQLFriendly(result.GetString(1))& "'")
			objStream.WriteLine("GO")
        	End While 
			result.Close() 
		
			
			objStream.WriteLine("SET QUOTED_IDENTIFIER OFF") 
			objStream.WriteLine("GO")
			objStream.WriteLine("SET ANSI_NULLS ON")
            objStream.Close()
			lblMessage.Text = GetLanguage("ScriptGenerated") & " " & Server.MapPath("Database\language_" & Session("language") & ".sql")

             Catch
             lblMessage.Text = GetLanguage("WriteError")
             End Try

	End Sub	
    Private Function MakeSQLFriendly(ByVal Item as String) As String
	Return item.replace("'", "''")
	end function
	
	Private Sub cbolanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbolanguage.SelectedIndexChanged
	'Clear Table to pick up changes
	Session("DataTable") = nothing
	gdrLanguage.Visible = True
	tableAddItem.visible = True
	BindData(cbolanguage.SelectedItem.Value)
	end sub

	
	Private Sub ddlModLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlModLanguage.SelectedIndexChanged
	Session("language") = ddlModLanguage.SelectedItem.Value
	Dim TabSelectedIndex As Integer = 0
    If Request("TabSelected") <> "" Then
	   TabSelectedIndex = Request("TabSelected")
	end if
    GetTabVisible( (TabSelectedIndex).ToString)
	end sub	
	
	
	Private Sub cboLonglanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLonglanguage.SelectedIndexChanged
	if GetLanguage("list_none") <> cboLongLanguage.selectedItem.Value then
	Dim objAdmin As New AdminDB()
	txtLongLanguage.Text = 	objAdmin.GetSinglelonglanguageSettings(Session("language"), cboLongLanguage.selectedItem.Value)
	SettingToSave.Text = cboLongLanguage.selectedItem.Value
	DelLongLang.ToolTip = GetLanguage("delete") & " " & cboLongLanguage.selectedItem.Value
	DelLongLang.Visible = True
	DelLongLang.AlternateText = GetLanguage("delete")
	DelLongLang.CommandName = cboLongLanguage.selectedItem.Value
	else
	txtLongLanguage.Text = ""
	SettingToSave.Text = ""
	DelLongLang.Visible = False
	end if
	end sub

	Private Sub ddlDelLang_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDelLang.SelectedIndexChanged
	DelLang.ToolTip = GetLanguage("language_clicktodel") & " " & ddlLanguage.SelectedItem.Value
	end sub


    Private Sub DelLang_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles DelLang.Click
            ClearHostCache()
            If ddlDelLang.SelectedItem.Value <> "fr" Then
                Dim TempScript As String

                TempScript = "DELETE FROM language WHERE ([language] = '" & ddlDelLang.SelectedItem.Value & "')" & vbCrLf
                lblMessage.Text = ddlDelLang.SelectedItem.Text & " " & GetLanguage("delete")
                Dim objAdmin As New AdminDB()
                lblMessage.Text += " " & objAdmin.ExecuteSQLScript(TempScript)
                ddlDelLang = Setddl(ddlDelLang)
                ddlDelLang.SelectedIndex = 0
                DelLang.ToolTip = GetLanguage("language_clicktodel") & " " & ddlDelLang.SelectedItem.Text
                ddlLanguage = Setddl(ddlLanguage)
                If Not ddlLanguage.Items.FindByValue(Session("language")) Is Nothing Then
                    ddlLanguage.Items.FindByValue(Session("language")).Selected = True
                Else
                    ddlLanguage.SelectedIndex = 0
                End If
                If ddlDelLang.Items.Count = 1 Then
                    lblDelLang.Visible = False
                    ddlDelLang.Visible = False
                    DelLang.Visible = False
                End If
            End If
	end sub

        Private Function Setddl(ByVal ddltoset As DropDownList) As DropDownList
            Dim objAdmin As New AdminDB()
            Dim HashL As Hashtable = objAdmin.GetAvailablelanguage
            ddltoset.Items.Clear()
            For Each de As DictionaryEntry In HashL
                Dim itemL As New ListItem()
                itemL.Text = de.Value
                itemL.Value = de.Key
                ddltoset.Items.Add(itemL)
            Next de
            Return ddltoset
        End Function

        Protected Function GetSubLink(ByVal TabSelected As String, ByVal TabName As String) As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return "<a href=""" & GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId _
           & "&amp;" & GetAdminPage() & "&amp;TabSelected=" & TabSelected _
     & """><b>" & TabName & "</b></a>"
        End Function

    End Class

End Namespace