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

    Public Class EditLinks
        Inherits DotNetZoom.PortalModuleControl
		
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        ' module options
        Protected WithEvents optControl As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents optView As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents optInfo As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents cmdCancelOptions As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdUpdateOptions As System.Web.UI.WebControls.LinkButton
		Protected WithEvents txtCSS As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents valTitle As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents optExternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents txtExternal As System.Web.UI.WebControls.TextBox
        Protected WithEvents optInternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents cboInternal As System.Web.UI.WebControls.DropDownList
        Protected WithEvents optFile As System.Web.UI.WebControls.RadioButton
        Protected WithEvents cboFiles As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpload As System.Web.UI.WebControls.HyperLink
        Protected WithEvents UploadReturn As System.Web.UI.WebControls.ImageButton
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtViewOrder As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkNewWindow As System.Web.UI.WebControls.CheckBox

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton

        Protected WithEvents pnlAudit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblCreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents lblCreatedDate As System.Web.UI.WebControls.Label

        Protected WithEvents lblClicks As System.Web.UI.WebControls.Label
        Protected WithEvents chkLog As System.Web.UI.WebControls.CheckBox
        Protected WithEvents grdLog As System.Web.UI.WebControls.DataGrid
		Protected WithEvents pnlOptions As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents pnlContent As System.Web.UI.WebControls.PlaceHolder

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
        ' The Page_Load event on this Page is used to obtain the
        ' ItemId of the link to edit.
        '
        ' It then uses the DotNetZoom.LinkDB() data component
        ' to populate the page's edit controls with the links details.
        '
        '****************************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Title1.DisplayHelp = "DisplayHelp_EditLinks"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			valTitle.ErrorMessage = GetLanguage("need_title")
			optControl.Items.FindByValue("L").Text = GetLanguage("links_options_list")
			optControl.Items.FindByValue("D").Text = GetLanguage("links_options_dropdown")
			optView.Items.FindByValue("V").Text = GetLanguage("links_options_vertical")
			optView.Items.FindByValue("H").Text = GetLanguage("links_options_horizontal")
			optInfo.Items.FindByValue("Y").Text = GetLanguage("links_options_yes")
			optInfo.Items.FindByValue("N").Text = GetLanguage("links_options_no")
			cmdUpdateOptions.Text = GetLanguage("enregistrer")
			cmdCancelOptions.Text = GetLanguage("annuler")
			cmdUpload.Text  = GetLanguage("upload")
			cmdUpdate.Text = getLanguage("enregistrer")
			cmdCancel.Text = getlanguage("annuler")
			cmdDelete.Text = GetLanguage("delete")

            ' Determine ItemId of Link to Update
            If IsNumeric(Request.Params("ItemID")) Then
                itemId = Int32.Parse(Request.Params("ItemID"))
            End If

            If optExternal.Checked = False And optInternal.Checked = False And optFile.Checked = False Then
                optExternal.Checked = True
            End If

            EnableControls()
            SetUpUpload(Not Page.IsPostBack)
            ' If the page is being requested the first time, determine if an
            ' link itemId value is specified, and if so populate page
            ' contents with the link details
            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                ' module options
                If Not Request.Params("options") Is Nothing Then
                    pnlOptions.Visible = True
                    pnlContent.Visible = False
                End If


                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm")) & "');")

                If CType(Settings("linkcontrol"), String) <> "" Then
                    optControl.Items.FindByValue(CType(Settings("linkcontrol"), String)).Selected = True
                Else
                    optControl.SelectedIndex = 0 ' list
                End If

                If CType(Settings("linkClass"), String) <> "" Then
                    txtCSS.Text = CType(Settings("linkClass"), String)
                Else
                    txtCSS.Text = "Normal"
                End If

                If CType(Settings("linkview"), String) <> "" Then
                    optView.Items.FindByValue(CType(Settings("linkview"), String)).Selected = True
                Else
                    optView.SelectedIndex = 0 ' vertical
                End If
                If CType(Settings("displayinfo"), String) <> "" Then
                    optInfo.Items.FindByValue(CType(Settings("displayinfo"), String)).Selected = True
                Else
                    optInfo.SelectedIndex = 0 ' vertical
                End If

                cboInternal.DataSource = GetPortalTabs(PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N")), True, True)
                cboInternal.DataBind()


                If itemId <> -1 Then

                    ' Obtain a single row of link information
                    Dim links As New LinkDB()
                    Dim dr As SqlDataReader = links.GetSingleLink(itemId, ModuleId)

                    ' Read in first row from database
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
                                If Not cboFiles.Items.FindByValue(dr("Url").ToString) Is Nothing Then
                                    cboFiles.Items.FindByValue(dr("Url").ToString).Selected = True
                                End If
                            End If
                        Else ' external
                            optExternal.Checked = True
                            optInternal.Checked = False
                            optFile.Checked = False
                            EnableControls()
                            txtExternal.Text = dr("Url").ToString
                        End If

                        txtTitle.Text = CStr(dr("Title"))
                        txtDescription.Text = CStr(dr("Description"))
                        If txtDescription.Text = "" Then
                            txtDescription.Text = CStr(dr("Title"))
                        End If
                        txtViewOrder.Text = dr("ViewOrder").ToString()
                        chkNewWindow.Checked = CType(dr("NewWindow"), Boolean)
                        lblClicks.Text = dr("Clicks").ToString

                        lblCreatedBy.Text = dr("CreatedByUser").ToString
                        lblCreatedDate.Text = CType(dr("CreatedDate"), DateTime).ToShortDateString()

                        ' Close datareader
                        dr.Close()
                    Else ' security violation attempt to access item not related to this Module
                        dr.Close()
                        HttpContext.Current.Session("UploadInfo") = Nothing
                        Response.Redirect(GetFullDocument() & "?tabid=" & TabId, True)
                    End If
                Else
                    cmdDelete.Visible = False
                    pnlAudit.Visible = False
                End If


            End If

        End Sub

        Private Sub SetUpUpload(ByVal SetFile As Boolean)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            UploadReturn.Visible = True
            UploadReturn.Style.Add("display", "none")
            cmdUpload.NavigateUrl = "#"
            SetUpModuleUpload(Request.MapPath(_portalSettings.UploadDirectory), glbImageFileTypes + ",zip," + glbGoogleEarthTypes, False)
            Dim incScript As String = String.Format("<script Language=""javascript"" SRC=""{0}""></script>", ResolveUrl("/admin/advFileManager/dialog.js"))
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "FileManager", incScript)
            Dim retScript As String = "<script language=""javascript"">" & vbCrLf & "<!--" & vbCrLf
            retScript &= "function retVal()" & vbCrLf & "{" & vbCrLf
            retScript &= vbTab & Page.ClientScript.GetPostBackEventReference(UploadReturn, String.Empty) & ";" & vbCrLf & "}" & vbCrLf
            retScript &= "--></script>"
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "FileManagerRefresh", retScript)
            Dim click As String = String.Format("openDialog('{0}', 700, 700, retVal);return false", ResolveUrl("/Admin/AdvFileManager/TAGFileUploadDialog.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)) & IIf(Not (Request.Params("hostpage") Is Nothing), "&hostpage=", ""), UploadReturn.ClientID)
            cmdUpload.Attributes.Add("onclick", click)
            If SetFile Then
                ' load the list of files found in the upload directory
                Dim FileList As ArrayList = GetFileList(_portalSettings.PortalId)
                cboFiles.DataSource = FileList
                cboFiles.DataBind()
            End If
        End Sub

        Private Sub UploadReturn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles UploadReturn.Click
            SetUpUpload(True)
        End Sub
        '****************************************************************
        '
        ' The cmdUpdate_Click event handler on this Page is used to either
        ' create or update a link.  It  uses the DotNetZoom.LinkDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            Page.Validate()
            Dim strLink As String

            If optExternal.Checked = True Then
                strLink = AddHTTP(txtExternal.Text)
            Else
                If optInternal.Checked = True Then
                    strLink = cboInternal.SelectedItem.Value
                Else
                    strLink = cboFiles.SelectedItem.Value
                End If
            End If

            If Page.IsValid = True And strLink <> "" Then

                ' Create an instance of the Link DB component
                Dim links As New LinkDB()
				if txtDescription.Text = "" then
				txtDescription.Text = txtTitle.Text
				end if
                If itemId = -1 Then
                    ' Add the link within the Links table
                    links.AddLink(ModuleId, Context.User.Identity.Name, txtTitle.Text, strLink, "", txtViewOrder.Text, txtDescription.Text, chkNewWindow.Checked)
                Else
                    ' Update the link within the Links table
                    links.UpdateLink(itemId, Context.User.Identity.Name, txtTitle.Text, strLink, "", txtViewOrder.Text, txtDescription.Text, chkNewWindow.Checked)
                End If
				' Reset data cashe
				
				ClearModuleCache(ModuleId)

                ' Redirect back to the portal home page
                HttpContext.Current.Session("UploadInfo") = Nothing
                Response.Redirect(CStr(ViewState("UrlReferrer")), True)
            End If

        End Sub


        '****************************************************************
        '
        ' The cmdDelete_Click event handler on this Page is used to delete
        ' a link.  It  uses the DotNetZoom.LinksDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click

            If itemId <> -1 Then

                Dim links As New LinkDB()
                links.DeleteLink(itemId)

            End If
				' Reset data cashe
				
				ClearModuleCache(ModuleId)

            ' Redirect back to the portal home page
            HttpContext.Current.Session("UploadInfo") = Nothing
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub


        '****************************************************************
        '
        ' The cmdCancel_Click event handler on this Page is used to cancel
        ' out of the page, and return the user back to the portal home
        ' page.
        '
        '****************************************************************'

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            HttpContext.Current.Session("UploadInfo") = Nothing
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

        Private Sub EnableControls()
            If optExternal.Checked Then
                txtExternal.Enabled = True
                cboInternal.ClearSelection()
                cboInternal.Enabled = False
                cboFiles.ClearSelection()
                cboFiles.Enabled = False
            Else
                If optInternal.Checked Then
                    txtExternal.Text = ""
                    txtExternal.Enabled = False
                    cboInternal.Enabled = True
                    cboFiles.ClearSelection()
                    cboFiles.Enabled = False
                Else
                    txtExternal.Text = ""
                    txtExternal.Enabled = False
                    cboInternal.ClearSelection()
                    cboInternal.Enabled = False
                    cboFiles.Enabled = True
                End If
            End If
        End Sub

        Private Sub cmdUpdateOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateOptions.Click

            Dim objAdmin As New AdminDB()

            objAdmin.UpdateModuleSetting(ModuleId, "linkcontrol", optControl.SelectedItem.Value)
            objAdmin.UpdateModuleSetting(ModuleId, "linkview", optView.SelectedItem.Value)
            objAdmin.UpdateModuleSetting(ModuleId, "displayinfo", optInfo.SelectedItem.Value)
			objAdmin.UpdateModuleSetting(ModuleId, "linkClass" , txtCSS.Text)
            ' Redirect back to the portal home page
            HttpContext.Current.Session("UploadInfo") = Nothing
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

        Private Sub cmdCancelOptions_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancelOptions.Click
            HttpContext.Current.Session("UploadInfo") = Nothing
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

        Private Sub chkLog_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLog.CheckedChanged

            If chkLog.Checked Then
                Dim objAdmin As New AdminDB()
				grdLog.Columns(0).HeaderText = GetLanguage("header_date")
				grdLog.Columns(1).HeaderText = GetLanguage("header_who")
                grdLog.DataSource = objAdmin.GetClicks("Links", itemId)
                grdLog.DataBind()

                grdLog.Visible = True
            Else
                grdLog.Visible = False
            End If

        End Sub
	
    End Class

End Namespace