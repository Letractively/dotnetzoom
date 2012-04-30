Imports System.Text
Imports System.IO

Namespace DotNetZoom

    Public MustInherit Class ModuleEdit
        Inherits System.Web.UI.UserControl
		
		Public ModuleTitle As String = Nothing
		Public ModuleId As Integer
		Public TabID  As Integer = Nothing
		Public ReturnURL As String = Nothing
        Public ItemContainer As Boolean = False
	
		Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
		Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
		Protected WithEvents rowContainer As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents rowCSS As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents GlobalDefault As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents Globalplaceholder As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtCSSClass As System.Web.UI.WebControls.TextBox
		Protected WithEvents TitleHeaderClass As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtContainer As System.Web.UI.WebControls.TextBox
		Protected WithEvents TitleContener As System.Web.UI.WebControls.TextBox
		Protected WithEvents ModuleContener As System.Web.UI.WebControls.TextBox
		Protected WithEvents lnkcolor As System.Web.UI.WebControls.hyperlink
		Protected withevents lnkcontainer As System.Web.UI.WebControls.HyperLink
		Protected WithEvents optContainer As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents cboAlign As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtColor As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtBorder As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblPreview As System.Web.UI.WebControls.literal
	    Protected WithEvents cmdEdit As System.Web.UI.WebControls.LinkButton
		Protected WithEvents cmdsee As System.Web.UI.WebControls.LinkButton
		Protected WithEvents chkGlobal As System.Web.UI.WebControls.CheckBox
		Protected WithEvents chkDefault As System.Web.UI.WebControls.CheckBox
				
		
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

       ' Obtain PortalSettings from Current Context
		Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Page.IsPostBack = False Then
                ViewState("ModuleId") = ModuleId.ToString
                ViewState("ModuleTitle") = ModuleTitle
                ViewState("TabID") = TabID.ToString
				ViewState("ReturnURL") = ReturnURL
				RowContainer.Visible = False
				lnkcontainer.Visible = RowContainer.Visible
				cmdSee.Visible = RowContainer.Visible
				chkGlobal.Checked = False
				chkDefault.Checked = False
				BindData()
			else
			  ModuleID = ViewState("ModuleId")
              ModuleTitle = ViewState("ModuleTitle")
              TabID = ViewState("TabID") 
			  ReturnURL = ViewState("ReturnURL")

			' PostBack see if a value passed back
			Dim PassBack As String = request.form("PassBack")
			If PassBack <> "" then
                    If File.Exists(Server.MapPath(glbPath + "images/containers/" & PassBack & "/Container.txt")) Then
                        Dim objStreamReader As System.IO.StreamReader
                        Dim ContainerText As String
                        objStreamReader = System.IO.File.OpenText(Server.MapPath(glbPath + "images/containers/" & PassBack & "/Container.txt"))
                        ContainerText = objStreamReader.ReadToEnd
                        Select Case optContainer.SelectedItem.Value
                            Case "A"
                                TitleContener.Text = Replace(ContainerText, "[PATH]", glbPath() & "images/containers/" & PassBack)
                            Case "B"
                                txtContainer.Text = Replace(ContainerText, "[PATH]", glbPath() & "images/containers/" & PassBack)
                            Case "C"
                                ModuleContener.Text = Replace(ContainerText, "[PATH]", glbPath() & "images/containers/" & PassBack)
                        End Select
                        objStreamReader.Close()
                    End If
			end if
		end if
		
		
		cmdUpdate.Text = getLanguage("enregistrer")
		cmdCancel.Text = getlanguage("annuler")
		cmdDelete.Text = getlanguage("delete")
        cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm")) & "');")
		cmdEdit.Text = getLanguage("ms_edit_contener")
	   	lnkcontainer.Text = GetLanguage("ms_SelectModuleSkin")
	   	lnkcontainer.NavigateUrl = "javascript:OpenNewContainerWindow('" + TabID.ToString + "')"
		lnkcolor.Tooltip = GetLanguage("ms_select_color")	
		lnkcolor.Text = GetLanguage("ms_select_color")	
		lnkcolor.NavigateUrl = "javascript:OpenColorWindow('" + TabID.ToString + "')"
		cmdSee.Text = getLanguage("Cmd_Reload")
		cboAlign.Items.FindByValue("left").Text = GetLanguage("ms_left")
		cboAlign.Items.FindByValue("center").Text = GetLanguage("ms_center")
		cboAlign.Items.FindByValue("right").Text = GetLanguage("ms_right")
			
	    optContainer.Items.FindByValue("A").Text = GetLanguage("ModuleTitle")
		optContainer.Items.FindByValue("B").Text = GetLanguage("ModuleWrapper")
	    optContainer.Items.FindByValue("C").Text = GetLanguage("ModuleOnly")
		
		
		
		
		If PortalSecurity.IsInRole(_portalSettings.AdministratorRoleId.ToString) or IsAdminTab() Then
		If ModuleID <> -5 then
		Globalplaceholder.Visible = IsAdminTab()
		GlobalDefault.Visible = PortalSecurity.IsInRole(_portalSettings.AdministratorRoleId.ToString)
		chkGlobal.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("ms_contener_global_info")))
	    chkDefault.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("ms_contener_default_info")))
		end if
		end if

        End Sub

		Private Sub SetPreview()
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim strContainer As String = txtContainer.Text
			Dim TitleContainer As String = TitleContener.Text
			Dim ModuleContainer As String = ModuleContener.Text

			if ModuleID <> - 5 then
            If strContainer = "" Then
                strContainer = portalSettings.GetSiteSettings(_portalSettings.PortalID)("container")
            End If
			If strContainer.IndexOf("[MODULE]") = -1 then strContainer = "[MODULE]" 
			If TitleContainer.IndexOf("[MODULE]") = -1 then TitleContainer = "[MODULE]" 
			If ModuleContainer.IndexOf("[MODULE]") = -1 then ModuleContainer = "[MODULE]" 
            TitleContainer = Replace(TitleContainer, "[ALIGN]", IIf(cboAlign.SelectedItem.Value <> "", " align=""" & cboAlign.SelectedItem.Value & """", ""))
            TitleContainer = Replace(TitleContainer, "[COLOR]", IIf(txtColor.Text <> "", " bgcolor=""" & txtColor.Text & """", ""))
            TitleContainer = Replace(TitleContainer, "[BORDER]", IIf(txtBorder.Text <> "", " border=""" & txtBorder.Text & """", ""))

			TitleContainer = Replace(TitleContainer, "[MODULE]" , "<table width=""100%"" class=""" & iif(TitleHeaderClass.Text <> "", TitleHeaderClass.Text, "headertitle") & """><tr><td><span class=""" & iif(txtCSSClass.Text <> "", txtCSSClass.Text, "Head") & """>" & ModuleTitle & "</span></td></tr></table>")

									
									
									
            ModuleContainer = Replace(ModuleContainer, "[ALIGN]", IIf(cboAlign.SelectedItem.Value <> "", " align=""" & cboAlign.SelectedItem.Value & """", ""))
            ModuleContainer = Replace(ModuleContainer, "[COLOR]", IIf(txtColor.Text <> "", " bgcolor=""" & txtColor.Text & """", ""))
            ModuleContainer = Replace(ModuleContainer, "[BORDER]", IIf(txtBorder.Text <> "", " border=""" & txtBorder.Text & """", ""))
                ModuleContainer = Replace(ModuleContainer, "[MODULE]", "<p>&nbsp;</p><h1>[MODULE]</h1><p>&nbsp;</p>")
			End if
            
			strContainer = Replace(strContainer, "[ALIGN]", IIf(cboAlign.SelectedItem.Value <> "", " align=""" & cboAlign.SelectedItem.Value & """", ""))
            strContainer = Replace(strContainer, "[COLOR]", IIf(txtColor.Text <> "", " bgcolor=""" & txtColor.Text & """", ""))
            strContainer = Replace(strContainer, "[BORDER]", IIf(txtBorder.Text <> "", " border=""" & txtBorder.Text & """", ""))
            strContainer = Replace(strContainer, "[MODULE]", TitleContainer & ModuleContainer)
            lblPreview.Text = strContainer
		end sub
		
        Private Sub cmdsee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsee.Click
				SetPreview()
        End Sub
		
	     Private Sub optContainer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optContainer.SelectedIndexChanged
         txtContainer.Visible = "False"
		 ModuleContener.Visible = "False"
		 TitleContener.Visible = "False"
		 Select Case optContainer.SelectedItem.Value
		 Case "A"
		TitleContener.Visible = "True"
		 Case "B"
		txtContainer.Visible = "True"
		 Case "C"
		ModuleContener.Visible = "True"
		 end select
        End Sub	

		Private Function GetADD() As String
		Select Case ModuleID
		Case -1
		Return "login"
		Case -2
		Return "edit"
		Case -3
		Return "admin"
		Case -4
		Return ""
		Case -5
		Return "ToolTip"
		Case Else
		Return ""
		End Select
		end Function

        Sub BindData()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
	   		Dim _Setting as Hashtable 
			Dim TempADD As STring = GetADD()
			If ModuleId > 0 then
			_Setting = portalSettings.GetModuleSettings(ModuleID)
			else
			_Setting = portalSettings.GetSiteSettings(_portalSettings.PortalID)
			end if
			
			If ModuleID <> -5 then
			If _Setting(TempADD +  "containerTitleHeaderClass") <> "" then
		 	TitleHeaderClass.Text = _Setting(TempADD + "containerTitleHeaderClass")
		 	else
		 	TitleHeaderClass.Text = ""
		 	end if  			

			If _Setting(TempADD +  "containerTitleCSSClass") <> "" then
		 	txtCSSClass.Text = _Setting(TempADD + "containerTitleCSSClass")
		 	else
		 	txtCSSClass.Text = ""
		 	end if 
			
			If _Setting(TempADD +  "TitleContainer") <> "" then
		 	TitleContener.Text = _Setting(TempADD +  "TitleContainer")
		 	else
		 	TitleContener.Text = ""
		 	end if
		    If _Setting(TempADD +  "ModuleContainer") <> "" then
		    ModuleContener.Text = _Setting(TempADD +  "ModuleContainer")
		    else
		    ModuleContener.Text = ""
		    end if
			else
			ModuleContener.Text = "[TOOLTIP]"
			rowCSS.Visible = False 
			GlobalDefault.Visible = False				
			end if
			
			If _Setting(TempADD +  "container") <> "" then
		 	txtContainer.Text = _Setting(TempADD + "container")
		 	else
		 	txtContainer.Text = "[MODULE]"
		 	end if  

	
				
			If _Setting(TempADD + "containerColor") <> "" Then
			txtColor.Text = _Setting(TempADD + "containerColor")
			Else
			txtColor.Text = ""
			End if

			If _Setting(TempADD + "containerBorder") <> "" Then
			txtBorder.Text = _Setting(TempADD + "containerBorder")
			Else
			txtBorder.Text = ""
			End if

			If _Setting(TempADD + "containerAlignment") <> "" Then
			cboAlign.Items.FindByValue(_Setting(TempADD + "containerAlignment")).Selected = True
			End if
			
			SetPreview()
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
		cmdEdit.Visible = True
		rowContainer.Visible = False
		lnkcontainer.Visible = RowContainer.Visible
		cmdSee.Visible = RowContainer.Visible
		chkGlobal.Checked = False
		chkDefault.Checked = False
        End Sub

		Private Sub cmdUpdate_Click(ByVal Sender As Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
   			Dim Admin As New AdminDB()
            Dim strMessage As String = ""
			Dim TempADD As STring = GetADD()
            If txtContainer.Text <> "" Then
                If txtContainer.Text <> portalSettings.GetSiteSettings(_portalSettings.PortalID)("container") Then
                    If InStr(1, txtContainer.Text, "[MODULE]") = 0 Then
                        strMessage = "<br>" & getlanguage("ms_contener_error1")
                    End If
                    If (UBound(Split(txtContainer.Text.ToLower, "<table")) <> UBound(Split(txtContainer.Text.ToLower, "</table"))) Or _
                       (UBound(Split(txtContainer.Text.ToLower, "<tr")) <> UBound(Split(txtContainer.Text.ToLower, "</tr"))) Or _
                       (UBound(Split(txtContainer.Text.ToLower, "<td")) <> UBound(Split(txtContainer.Text.ToLower, "</td"))) Or _
                       (UBound(Split(txtContainer.Text.ToLower, """")) Mod 2 <> 0) Or _
                       (UBound(Split(txtContainer.Text.ToLower, "<")) <> UBound(Split(txtContainer.Text.ToLower, ">"))) Then
                        strMessage = strMessage & "<br>" & getlanguage("ms_contener_error2") 
                    End If
                End If
            End If
            If strMessage <> "" Then
                lblMessage.Text = strMessage + "<br>"
				lblMessage.Visible = True
	        Else
			lblMessage.Visible = False
			If ModuleID > 0 then
			
			admin.UpdateModuleSetting( ModuleID, "containerTitleHeaderClass", TitleHeaderClass.Text)
			admin.UpdateModuleSetting( ModuleID, "containerTitleCSSClass", txtCSSClass.Text)
			admin.UpdateModuleSetting( ModuleID, "container", txtContainer.Text)
			admin.UpdateModuleSetting( ModuleID, "TitleContainer", TitleContener.Text)
			admin.UpdateModuleSetting( ModuleID, "ModuleContainer", ModuleContener.Text)
			admin.UpdateModuleSetting( ModuleID, "containerAlignment", cboAlign.SelectedItem.Value)
			admin.UpdateModuleSetting( ModuleID, "containerColor", txtColor.Text)
			admin.UpdateModuleSetting( ModuleID, "containerBorder", txtBorder.Text)
			Else
			If ModuleID = -5 then
           		admin.UpdatePortalSetting(_portalSettings.PortalID, "ToolTipcontainer", txtContainer.Text)
				admin.UpdatePortalSetting(_portalSettings.PortalID, "ToolTipcontainerAlignment", cboAlign.SelectedItem.Value)
				admin.UpdatePortalSetting(_portalSettings.PortalID, "ToolTipcontainerColor", txtColor.Text)
				admin.UpdatePortalSetting(_portalSettings.PortalID, "ToolTipcontainerBorder", txtBorder.Text)
			else
			admin.UpdatePortalSetting(_portalSettings.PortalID,  TempADD + "containerTitleHeaderClass", TitleHeaderClass.Text)
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "containerTitleCSSClass", txtCSSClass.Text)
			admin.UpdatePortalSetting(_portalSettings.PortalID,  TempADD + "container", txtContainer.Text)
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "TitleContainer", TitleContener.Text)
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "ModuleContainer", ModuleContener.Text)
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "containerAlignment", cboAlign.SelectedItem.Value)
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "containerColor", txtColor.Text)
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "containerBorder", txtBorder.Text)
            end if
			end if
				If chkDefault.Checked Then
					admin.UpdatePortalSetting(_portalSettings.PortalID, "containerTitleHeaderClass", TitleHeaderClass.Text)
					admin.UpdatePortalSetting(_portalSettings.PortalID, "containerTitleCSSClass", txtCSSClass.Text)
               		admin.UpdatePortalSetting(_portalSettings.PortalID, "container", txtContainer.Text)
					admin.UpdatePortalSetting(_portalSettings.PortalID, "TitleContainer", TitleContener.Text)
					admin.UpdatePortalSetting(_portalSettings.PortalID, "ModuleContainer", ModuleContener.Text)
					admin.UpdatePortalSetting(_portalSettings.PortalID, "containerAlignment", cboAlign.SelectedItem.Value)
					admin.UpdatePortalSetting(_portalSettings.PortalID, "containerColor", txtColor.Text)
					admin.UpdatePortalSetting(_portalSettings.PortalID, "containerBorder", txtBorder.Text)
            	End If
            	If chkGlobal.Checked Then
               		admin.UpdatePortalModules(_portalSettings.PortalId)
					ClearPortalCache(_portalSettings.PortalID)
            	End If
			ClearTabCache(TabID)	
			End If
		BindData()		
		cmdEdit.Visible = True
		rowContainer.Visible = False
		lnkcontainer.Visible = RowContainer.Visible
		cmdSee.Visible = RowContainer.Visible
		chkGlobal.Checked = False
		chkDefault.Checked = False
		End Sub

        Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        ' Obtain PortalSettings from Current Context
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

		Dim Admin As New AdminDB()
		Dim TempADD As STring = GetADD()
			If ModuleID > 0 then
			admin.UpdateModuleSetting( ModuleID, "container", "[MODULE]")
			admin.UpdateModuleSetting( ModuleID, "TitleContainer", "[MODULE]")
			admin.UpdateModuleSetting( ModuleID, "ModuleContainer", "[MODULE]")
			admin.UpdateModuleSetting( ModuleID, "containerAlignment", "")
			admin.UpdateModuleSetting( ModuleID, "containerColor", "")
			admin.UpdateModuleSetting( ModuleID, "containerBorder", "")
			admin.UpdateModuleSetting( ModuleID, "containerTitleHeaderClass", "")
			admin.UpdateModuleSetting( ModuleID, "containerTitleCSSClass", "")

			Else
			If ModuleID = -5 then
           		admin.UpdatePortalSetting(_portalSettings.PortalID, "ToolTipcontainer", "")
				admin.UpdatePortalSetting(_portalSettings.PortalID, "ToolTipcontainerAlignment", "")
				admin.UpdatePortalSetting(_portalSettings.PortalID, "ToolTipcontainerColor", "")
				admin.UpdatePortalSetting(_portalSettings.PortalID, "ToolTipcontainerBorder", "")
			else
			admin.UpdatePortalSetting(_portalSettings.PortalID,  TempADD + "containerTitleCSSClass", "")
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "containerTitleHeaderClass", "")
			admin.UpdatePortalSetting(_portalSettings.PortalID,  TempADD + "container", "[MODULE]")
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "TitleContainer", "[MODULE]")
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "ModuleContainer", "[MODULE]")
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "containerAlignment", "")
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "containerColor", "")
			admin.UpdatePortalSetting(_portalSettings.PortalID, TempADD + "containerBorder", "")
            end if
			end if		
		ClearTabCache(TabID)
		BindData()
		cmdEdit.Visible = True
		rowContainer.Visible = False
		lnkcontainer.Visible = RowContainer.Visible
		cmdSee.Visible = RowContainer.Visible
		chkGlobal.Checked = False
		chkDefault.Checked = False
		End Sub

        Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
		cmdEdit.Visible = False
		rowContainer.Visible = True
		lnkcontainer.Visible = RowContainer.Visible
		cmdSee.Visible = RowContainer.Visible
        End Sub
		
		Public Function GettxtcolorID() As STring
		Return TxtColor.ClientID.ToString
		end Function

		Public Function GetPostBack() As STring
		Return Replace(cmdSee.ClientID, "_", "$") 
		End Function		
    End Class
	
End Namespace