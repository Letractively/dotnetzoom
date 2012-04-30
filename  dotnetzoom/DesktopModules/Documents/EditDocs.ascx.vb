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

    Public Class EditDocs
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
        Protected WithEvents valName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents optInternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents optExternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents cboInternal As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpload As System.Web.UI.WebControls.HyperLink
        Protected WithEvents UploadReturn As System.Web.UI.WebControls.ImageButton
        Protected WithEvents txtExternal As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtCategory As System.Web.UI.WebControls.TextBox
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
        ' and ItemId of the document to edit.
        '
        ' It then uses the DotNetZoom.DocumentDB() data component
        ' to populate the page's edit controls with the document details.
        '
        '****************************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Title1.DisplayHelp = "DisplayHelp_EditDocs"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			valName.ErrorMessage =  "<br>" + GetLanguage("need_object_message")
			cmdUpload.Text = GetLanguage("upload")
			cmdUpdate.Text = GetLanguage("enregistrer")
			cmdCancel.Text = GetLanguage("annuler")
			cmdDelete.Text = GetLanguage("delete")
			cmdSyndicate.Text = GetLanguage("syndicate")

            ' Determine ItemId of Document to Update
            If IsNumeric(Request.Params("ItemID")) Then
                itemId = Int32.Parse(Request.Params("ItemID"))
            End If

            If optExternal.Checked = False And optInternal.Checked = False Then
                optInternal.Checked = True
            End If

            EnableControls()

            ' If the page is being requested the first time, determine if an
            ' document itemId value is specified, and if so populate page
            ' contents with the document details
            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)

                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm")) & "');")

                SetUpUpload()
 
                Dim ObjAdmin As New AdminDB()
                lblSyndicate.Text = "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & _portalSettings.UploadDirectory & ObjAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml"
                lblSyndicate.Visible = File.Exists(Request.MapPath(_portalSettings.UploadDirectory) & ObjAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml")



                If itemId <> -1 Then

                    ' Obtain a single row of document information
                    Dim documents As New DocumentDB()
                    Dim dr As SqlDataReader = documents.GetSingleDocument(itemId, ModuleId)

                    ' Load first row into Datareader
                    If dr.Read() Then
                        If InStr(1, dr("URL").ToString, "://") = 0 Then
                            optInternal.Checked = True
                            optExternal.Checked = False
                            EnableControls()
                            If cboInternal.Items.Contains(New ListItem(dr("URL").ToString)) Then
                                cboInternal.Items.FindByText(dr("URL").ToString).Selected = True
                            End If
                        Else
                            optInternal.Checked = False
                            optExternal.Checked = True
                            EnableControls()
                            txtExternal.Text = dr("URL").ToString
                        End If

                        txtName.Text = dr("Title").ToString
                        txtCategory.Text = dr("Category").ToString
                        chkSyndicate.Checked = dr("Syndicate")
                        lblClicks.Text = dr("Clicks").ToString

                        lblCreatedBy.Text = dr("CreatedByUser").ToString
                        lblCreatedDate.Text = CType(dr("CreatedDate"), DateTime).ToShortDateString()

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

        Private Sub SetUpUpload()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            UploadReturn.Visible = True
            UploadReturn.Style.Add("display", "none")
            cmdUpload.NavigateUrl = "#"
            SetUpModuleUpload(Request.MapPath(_portalSettings.UploadDirectory), PortalSettings.GetHostSettings("FileExtensions").ToString(), False)
            Dim incScript As String = String.Format("<script Language=""javascript"" SRC=""{0}""></script>", ResolveUrl("/admin/advFileManager/dialog.js"))
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "FileManager", incScript)
            Dim retScript As String = "<script language=""javascript"">" & vbCrLf & "<!--" & vbCrLf
            retScript &= "function retVal()" & vbCrLf & "{" & vbCrLf
            retScript &= vbTab & Page.ClientScript.GetPostBackEventReference(UploadReturn, String.Empty) & ";" & vbCrLf & "}" & vbCrLf
            retScript &= "--></script>"
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "FileManagerRefresh", retScript)
            Dim click As String = String.Format("openDialog('{0}', 700, 600, retVal);return false", ResolveUrl("/Admin/AdvFileManager/TAGFileUploadDialog.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)) & IIf(Not (Request.Params("hostpage") Is Nothing), "&hostpage=", ""), UploadReturn.ClientID)
            cmdUpload.Attributes.Add("onclick", click)
            Dim FileList As ArrayList = GetFileList(_portalSettings.PortalId)
            cboInternal.DataSource = FileList
            cboInternal.DataBind()
        End Sub

        Private Sub UploadReturn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles UploadReturn.Click
            SetUpUpload()
        End Sub
        '****************************************************************
        '
        ' The cmdUpdate_Click event handler on this Page is used to either
        ' create or update an document.  It  uses the DotNetZoom.DocumentDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            Page.Validate()

            Dim strDocument As String

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Only Update if Input Data is Valid
            If Page.IsValid = True Then

                If txtExternal.Text <> "" Then
                    strDocument = AddHTTP(txtExternal.Text)
                Else
                    strDocument = cboInternal.SelectedItem.Text
                End If

                ' Create an instance of the Document DB component
                Dim documents As New DocumentDB()

                If itemId = -1 Then
                    documents.AddDocument(ModuleId, txtName.Text, strDocument, Context.User.Identity.Name, txtCategory.Text, chkSyndicate.Checked)
                Else
                    documents.UpdateDocument(itemId, txtName.Text, strDocument, Context.User.Identity.Name, txtCategory.Text, chkSyndicate.Checked)
                End If

                Syndicate()

			' Reset data cashe
			
			ClearModuleCache(ModuleId)
				
                ' Redirect back to the portal home page
                HttpContext.Current.Session("UploadInfo") = Nothing
                Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

            End If

        End Sub


        '****************************************************************
        '
        ' The cmdDelete_Click event handler on this Page is used to delete an
        ' a document.  It  uses the DotNetZoom.DocumentsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************
        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click

            If itemId <> -1 Then

                Dim documents As New DocumentDB()
                documents.DeleteDocument(itemId)
			' Reset data cashe
			
			ClearModuleCache(ModuleId)

            End If

            ' Redirect back to the portal home page
            HttpContext.Current.Session("UploadInfo") = Nothing
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

            ' Redirect back to the portal home page
            HttpContext.Current.Session("UploadInfo") = Nothing
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

        End Sub

        Private Sub EnableControls()
            If optExternal.Checked Then
                cboInternal.ClearSelection()
                cboInternal.Enabled = False
                txtExternal.Enabled = True
            Else
                cboInternal.Enabled = True
                txtExternal.Text = ""
                txtExternal.Enabled = False
            End If
        End Sub

        Private Sub Syndicate()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objDocuments As New DocumentDB()
			Dim objAdmin As New AdminDB()
            Dim dr As SqlDataReader = objDocuments.GetDocuments(ModuleId, _portalSettings.PortalId)
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
                grdLog.DataSource = objAdmin.GetClicks("Documents", itemId)
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