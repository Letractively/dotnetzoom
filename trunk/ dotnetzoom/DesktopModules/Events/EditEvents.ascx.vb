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

    Public Class EditEvents
        Inherits DotNetZoom.PortalModuleControl

        ' module options
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents pnlOptions As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents optView As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents txtWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHeight As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdUpdateOptions As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancelOptions As System.Web.UI.WebControls.LinkButton

        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents valTitle As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents valDescription As System.Web.UI.WebControls.RequiredFieldValidator

    	Protected WithEvents MyHtmlImage As System.Web.UI.WebControls.Image
		Protected WithEvents txticone As System.Web.UI.WebControls.TextBox
		Protected WithEvents lnkicone As System.Web.UI.WebControls.hyperlink

        Protected WithEvents txtAlt As System.Web.UI.WebControls.TextBox
        Protected WithEvents valAltText As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtEvery As System.Web.UI.WebControls.TextBox
        Protected WithEvents cboPeriod As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtStartDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtTime As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtExpiryDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents valExpiryDate As System.Web.UI.WebControls.CompareValidator

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton

        Protected WithEvents pnlAudit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents CreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedDate As System.Web.UI.WebControls.Label
        Protected WithEvents pnlContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents cmdStartCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents valStartDate2 As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents cmdExpiryCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents valStartDate As System.Web.UI.WebControls.RequiredFieldValidator

        Private itemId As Integer = -1

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

        '****************************************************************
        '
        ' The Page_Load event on this Page is used to obtain the ModuleId
        ' and ItemId of the event to edit.
        '
        ' It then uses the DotNetZoom.EventsDB() data component
        ' to populate the page's edit controls with the event details.
        '
        '****************************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Title1.DisplayHelp = "DisplayHelp_EditEvents"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Determine ItemId of Events to Update
            If IsNumeric(Request.Params("ItemID")) Then
                itemId = Int32.Parse(Request.Params("ItemID"))
            End If
			valTitle.ErrorMessage =  "<br>" + GetLanguage("need_object_message")
			valDescription.ErrorMessage =  "<br>" + GetLanguage("need_valid_description")
			valAltText.ErrorMessage =  "<br>" + GetLanguage("need_alt")
			valExpiryDate.ErrorMessage =  "<br>" + GetLanguage("not_a_date")
			valStartDate.ErrorMessage =  "<br>" + GetLanguage("need_start_date")
			valStartDate2.ErrorMessage =  "<br>" + GetLanguage("not_a_date")
			cmdUpdate.Text = GetLanguage("enregistrer")
			cmdCancel.Text = GetLanguage("annuler")
			cmdDelete.Text = GetLanguage("delete")		
			cmdUpdateOptions.Text = GetLanguage("enregistrer")
			cmdCancelOptions.Text = GetLanguage("annuler")
			cmdStartCalendar.Text = GetLanguage("calendar")
			cmdExpiryCalendar.Text = GetLanguage("calendar")			
			optView.Items.FindByValue("L").Text = GetLanguage("events_list")		
			optView.Items.FindByValue("C").Text = GetLanguage("calendar")
			cboPeriod.Items.FindByValue("D").Text = GetLanguage("events_day")		
			cboPeriod.Items.FindByValue("W").Text = GetLanguage("events_week")	
			cboPeriod.Items.FindByValue("M").Text = GetLanguage("events_month")		
			cboPeriod.Items.FindByValue("Y").Text = GetLanguage("events_year")		
 
			
					
			lnkicone.Tooltip = GetLanguage("select_icone_tooltip")	
			lnkicone.Text = GetLanguage("select_icone")	
			
            ' If the page is being requested the first time, determine if an
            ' event itemId value is specified, and if so populate page
            ' contents with the event details
            If Page.IsPostBack = False Then

                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm")) & "');")
                cmdStartCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtStartDate)
                cmdExpiryCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtExpiryDate)

                ' module options
				If not request.params("options") is nothing then
				pnlOptions.visible = True
				pnlContent.visible = False
				end if
                If CType(Settings("eventview"), String) <> "" Then
                    optView.Items.FindByValue(CType(Settings("eventview"), String)).Selected = True
                Else
                    optView.SelectedIndex = 1 ' calendar
                End If
                txtWidth.Text = CType(Settings("eventcalendarcellwidth"), String)
                txtHeight.Text = CType(Settings("eventcalendarcellheight"), String)

                If itemId <> -1 Then

                    ' Obtain a single row of event information
                    Dim events As New EventsDB()
                    Dim dr As SqlDataReader = events.GetSingleModuleEvent(itemId, ModuleId)

                    ' Read first row from database
                    If dr.Read() Then
                        txtTitle.Text = dr("Title").ToString
                        txtDescription.Text = dr("Description").ToString
                        
						TxtIcone.Text = dr("IconFile").ToString
                        
                        txtAlt.Text = dr("AltText").ToString
                        txtEvery.Text = dr("Every").ToString
                        If txtEvery.Text = "1" Then
                            txtEvery.Text = ""
                        End If
                        If dr("Period").ToString <> "" Then
                            cboPeriod.Items.FindByValue(dr("Period").ToString).Selected = True
                        Else
                            cboPeriod.Items(0).Selected = True
                        End If
                        txtStartDate.Text = Format(CDate(dr("DateTime")), "yyyy-MM-dd")
                        txtTime.Text = Format(CDate(dr("DateTime")), "hh:mm")
                        If txtTime.Text = "12:00" Then
                            txtTime.Text = ""
                        End If
						If Not IsDBNull(dr("ExpireDate")) Then
                        txtExpiryDate.Text = Format(CDate(dr("ExpireDate")), "yyyy-MM-dd")
						end if

                        CreatedBy.Text = dr("CreatedByUser").ToString
                        CreatedDate.Text = CType(dr("CreatedDate"), DateTime).ToShortDateString()
                        dr.Close()
                    Else ' security violation attempt to access item not related to this Module
                        dr.Close()
                        Response.Redirect(GetFullDocument() & "?tabid=" & TabId, True)
                    End If
                Else
                    cmdDelete.Visible = False
                    pnlAudit.Visible = False
                End If

                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = ViewState("UrlReferrer") = GetFullDocument() & "?tabid=" & TabId
                End If
                If InStr(1, ViewState("UrlReferrer"), "VisibleDate=") Then
                    ViewState("UrlReferrer") = Left(ViewState("UrlReferrer"), InStr(1, ViewState("UrlReferrer"), "VisibleDate=") - 2)
                End If

                If Not Request.Params("VisibleDate") Is Nothing Then
				  If InStr(1, ViewState("UrlReferrer"), "?tabid=") then
                    ViewState("UrlReferrer") += "&VisibleDate=" & Request.Params("VisibleDate")
					else
					ViewState("UrlReferrer") += "?VisibleDate=" & Request.Params("VisibleDate")
					end if
                End If

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
			valAltText.Visible  = True
			else
			MyHtmlImage.Visible = False
			valAltText.Visible = False
			end if
	
			

            Dim ParentID As String = Server.HtmlEncode(txticone.ClientID)
            lnkicone.NavigateUrl = "javascript:OpenNewWindow('" + tabID.ToString + "')"

        End Sub


        '****************************************************************
        '
        ' The cmdUpdate_Click event handler on this Page is used to either
        ' create or update an event.  It uses the DotNetZoom.EventsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            Dim strDateTime As String
            Dim strIconFile As String

            ' Only Update if the Entered Data is Valid
            If Page.IsValid = True Then

                strDateTime = txtStartDate.Text
                If txtTime.Text <> "" Then
                    strDateTime += " " & txtTime.Text
                End If

                    strIconFile = TxtIcone.Text
 
                ' Create an instance of the Event DB component
                Dim events As New EventsDB()

                If itemId = -1 Then
                    ' Add the event within the Events table
                    events.AddModuleEvent(ModuleId, txtDescription.Text, strDateTime, txtTitle.Text, CheckDateSql(txtExpiryDate.Text), Context.User.Identity.Name, IIf(txtEvery.Text = "", "1", txtEvery.Text), cboPeriod.SelectedItem.Value, strIconFile, txtAlt.Text)
                Else
                    ' Update the event within the Events table
                    events.UpdateModuleEvent(itemId, txtDescription.Text, strDateTime, txtTitle.Text, CheckDateSql(txtExpiryDate.Text), Context.User.Identity.Name, IIf(txtEvery.Text = "", "1", txtEvery.Text), cboPeriod.SelectedItem.Value, strIconFile, txtAlt.Text)
                End If
				' Reset data cashe
				
				ClearModuleCache(ModuleId)

                ' Redirect back to the portal home page
                Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

            End If

        End Sub


        '****************************************************************
        '
        ' The cmdDelete_Click event handler on this Page is used to delete an
        ' an event.  It  uses the DotNetZoom.EventsDB() data component to
        ' encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click

            Dim events As New EventsDB()
            events.DeleteModuleEvent(itemId)
			' Reset data cashe
			
			ClearModuleCache(ModuleId)

            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

        End Sub


        '****************************************************************
        '
        ' The cmdCancel_Click event handler on this Page is used to cancel
        ' out of the page, and return the user back to the portal home
        ' page.
        '
        '****************************************************************'

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

        Private Sub cmdUpdateOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateOptions.Click

            Dim objAdmin As New AdminDB()

            objAdmin.UpdateModuleSetting(ModuleId, "eventview", optView.SelectedItem.Value)
            objAdmin.UpdateModuleSetting(ModuleId, "eventcalendarcellwidth", txtWidth.Text)
            objAdmin.UpdateModuleSetting(ModuleId, "eventcalendarcellheight", txtHeight.Text)
            ' Redirect back to the portal home page
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

        Private Sub cmdCancelOptions_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancelOptions.Click
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

    End Class

End Namespace