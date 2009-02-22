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

Namespace DotNetZoom

    Public Class EditAnnouncements
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents valTitle As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents valDescription As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents cmdUpload As System.Web.UI.WebControls.HyperLink
        Protected WithEvents optExternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents txtExternal As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtExpires As System.Web.UI.WebControls.TextBox
        Protected WithEvents valExpires As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents chkSyndicate As System.Web.UI.WebControls.CheckBox

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdSyndicate As System.Web.UI.WebControls.LinkButton

        Protected WithEvents pnlAudit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblCreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents lblCreatedDate As System.Web.UI.WebControls.Label
        Protected WithEvents lblSyndicate As System.Web.UI.WebControls.Label

        Protected WithEvents lblClicks As System.Web.UI.WebControls.Label
        Protected WithEvents chkLog As System.Web.UI.WebControls.CheckBox
        Protected WithEvents grdLog As System.Web.UI.WebControls.DataGrid
        Protected WithEvents txtViewOrder As System.Web.UI.WebControls.TextBox
        Protected WithEvents optFile As System.Web.UI.WebControls.RadioButton
        Protected WithEvents cboFile As System.Web.UI.WebControls.DropDownList
        Protected WithEvents optInternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents cboInternal As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents valViewOrder As System.Web.UI.WebControls.CompareValidator

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
        ' and ItemId of the announcement to edit.
        '
        ' It then uses the DotNetZoom.AnnouncementsDB() data component
        ' to populate the page's edit controls with the annoucement details.
        '
        '****************************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Title1.DisplayHelp = "DisplayHelp_EditAnnoncement"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Determine ItemId of Announcement to Update
            If IsNumeric(Request.Params("ItemID")) Then
                itemId = Int32.Parse(Request.Params("ItemID"))
            End If

            If optExternal.Checked = False And optInternal.Checked = False And optFile.Checked = False Then
                optFile.Checked = True
            End If

            EnableControls()

            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)

                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm")) & "');")
                cmdCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtExpires)
                cmdUpdate.Text = GetLanguage("enregistrer")
                cmdCancel.Text = GetLanguage("annuler")
                cmdDelete.Text = GetLanguage("delete")
                cmdSyndicate.Text = GetLanguage("syndicate")
                valTitle.ErrorMessage = GetLanguage("need_title")
                valViewOrder.ErrorMessage = GetLanguage("need_number")
                valDescription.ErrorMessage = GetLanguage("need_description")
                cmdUpload.Text = GetLanguage("command_upload")
                cmdCalendar.Text = GetLanguage("command_calendar")

                ' load the list of files found in the upload directory
                cmdUpload.NavigateUrl = GetFullDocument() & "?tabid=" & TabId & "&def=Gestion fichiers"
                Dim FileList As ArrayList = GetFileList(_portalSettings.PortalId)
                cboFile.DataSource = FileList
                cboFile.DataBind()

                cboInternal.DataSource = GetPortalTabs(PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N")), True, True)
                cboInternal.DataBind()

                Dim ObjAdmin As New AdminDB()
                lblSyndicate.Text = "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & _portalSettings.UploadDirectory & ObjAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml"
                lblSyndicate.Visible = File.Exists(Request.MapPath(_portalSettings.UploadDirectory) & ObjAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml")
                If itemId <> -1 Then

                    ' Obtain a single row of announcement information
                    Dim objAnnouncements As New AnnouncementsDB()
                    Dim dr As SqlDataReader = objAnnouncements.GetSingleAnnouncement(itemId, ModuleId)

                    ' Load first row into DataReader
                    If dr.Read() Then
                        If InStr(1, dr("Url").ToString, "://") = 0 Then
                            If IsNumeric(dr("Url").ToString) Then
                                optExternal.Checked = False
                                optInternal.Checked = True
                                optFile.Checked = False
                                EnableControls()
                                If Not cboInternal.Items.FindByValue(dr("Url").ToString) Is Nothing Then
                                    cboInternal.Items.FindByValue(dr("Url").ToString).Selected = True
                                End If
                            Else ' file
                                optExternal.Checked = False
                                optInternal.Checked = False
                                optFile.Checked = True
                                EnableControls()
                                If Not cboFile.Items.FindByValue(dr("Url").ToString) Is Nothing Then
                                    cboFile.Items.FindByValue(dr("Url").ToString).Selected = True
                                End If
                            End If
                        Else ' external
                            optExternal.Checked = True
                            optInternal.Checked = False
                            optFile.Checked = False
                            EnableControls()
                            txtExternal.Text = dr("Url").ToString
                        End If

                        txtTitle.Text = dr("Title").ToString
                        txtDescription.Text = dr("Description").ToString
                        If Not IsDBNull(dr("ExpireDate")) Then
                            txtExpires.Text = Format(CDate(dr("ExpireDate")), "yyyy-MM-dd")
                        End If
                        chkSyndicate.Checked = dr("Syndicate")

                        txtViewOrder.Text = dr("ViewOrder").ToString()

                        lblClicks.Text = dr("Clicks").ToString
                        lblCreatedBy.Text = dr("CreatedByUser").ToString
                        lblCreatedDate.Text = CType(dr("CreatedDate"), DateTime).ToShortDateString()

                        ' Close the datareader
                        dr.Close()

                    Else ' security violation attempt to access item not related to this Module
                        dr.Close()
                        Response.Redirect(GetFullDocument() & "?tabid=" & TabId, True)
                    End If
                Else
                    cmdDelete.Visible = False
                    pnlAudit.Visible = False
                End If


            End If

        End Sub


        '****************************************************************
        '
        ' The cmdUpdate_Click event handler on this Page is used to either
        ' create or update an announcement.  It  uses the DotNetZoom.AnnouncementsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            Page.Validate()
            Dim strLink As String
			' Reset Cache
			
			ClearModuleCache(ModuleId)
            ' Only Update if the Entered Data is Valid
            If Page.IsValid = True Then

                ' Create an instance of the Announcement DB component
                Dim objAnnouncements As New AnnouncementsDB()

                If optExternal.Checked = True Then
                    strLink = AddHTTP(txtExternal.Text)
                Else
                    If optInternal.Checked = True Then
                        strLink = cboInternal.SelectedItem.Value
                    Else
                        strLink = cboFile.SelectedItem.Value
                    End If
                End If

                If itemId = -1 Then
                    objAnnouncements.AddAnnouncement(ModuleId, Context.User.Identity.Name, txtTitle.Text, CheckDateSqL(txtExpires.Text), txtDescription.Text, strLink, chkSyndicate.Checked, txtViewOrder.Text)
                Else
                    objAnnouncements.UpdateAnnouncement(itemId, Context.User.Identity.Name, txtTitle.Text, CheckDateSQL(txtExpires.Text), txtDescription.Text, strLink, chkSyndicate.Checked, txtViewOrder.Text)
                End If

                Syndicate()

                ' Redirect back to the portal home page
                Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

            End If

        End Sub


        '****************************************************************
        '
        ' The cmdDelete_Click event handler on this Page is used to delete an
        ' an announcement.  It  uses the DotNetZoom.AnnouncementsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click

			' Reset Cache
			
			ClearModuleCache(ModuleId)
		
            If itemId <> -1 Then
                Dim objAnnouncements As New AnnouncementsDB()
                objAnnouncements.DeleteAnnouncement(itemId)
            End If

            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

        End Sub


        '****************************************************************
        '
        ' The cmdCancel_Click event handler on this Page is used to cancel
        ' out of the page, and return the user back to the portal home
        ' page.
        '
        '****************************************************************

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
			' Reset Cache
			
			ClearModuleCache(ModuleId)

            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

        Private Sub EnableControls()
            If optExternal.Checked Then
                txtExternal.Enabled = True
                cboInternal.ClearSelection()
                cboInternal.Enabled = False
                cboFile.ClearSelection()
                cboFile.Enabled = False
            Else
                If optInternal.Checked Then
                    txtExternal.Text = ""
                    txtExternal.Enabled = False
                    cboInternal.Enabled = True
                    cboFile.ClearSelection()
                    cboFile.Enabled = False
                Else
                    txtExternal.Text = ""
                    txtExternal.Enabled = False
                    cboInternal.ClearSelection()
                    cboInternal.Enabled = False
                    cboFile.Enabled = True
                End If
            End If
        End Sub

        Private Sub Syndicate()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAnnouncements As New AnnouncementsDB()
			Dim objAdmin As New AdminDB()
            Dim dr As SqlDataReader = objAnnouncements.GetAnnouncements(ModuleId)
            CreateRSS(dr, "Title", "URL", "CreatedDate", "Syndicate", GetPortalDomainName(_portalSettings.PortalAlias, Request), Request.MapPath(_portalSettings.UploadDirectory) & objAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml")
            dr.Close()
			lblSyndicate.Visible = File.Exists(Request.MapPath(_portalSettings.UploadDirectory) & objAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml")
        End Sub

        Private Sub cmdSyndicate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSyndicate.Click
            Syndicate()
	     End Sub

        Private Sub chkLog_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLog.CheckedChanged

            If chkLog.Checked Then
                Dim objAdmin As New AdminDB()
                grdLog.DataSource = objAdmin.GetClicks("Announcements", itemId)
				grdLog.Columns(0).HeaderText = GetLanguage("header_date")
				grdLog.Columns(1).HeaderText = GetLanguage("header_who")
                grdLog.DataBind()

                grdLog.Visible = True
            Else
                grdLog.Visible = False
            End If

        End Sub

    End Class

End Namespace