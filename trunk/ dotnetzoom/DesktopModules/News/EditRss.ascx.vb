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

    Public Class EditRss
        Inherits DotNetZoom.PortalModuleControl
		
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Protected WithEvents optXMLInternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents cboXML As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpload1 As System.Web.UI.WebControls.HyperLink
        Protected WithEvents UploadReturn As System.Web.UI.WebControls.ImageButton
        Protected WithEvents optXMLExternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents txtXML As System.Web.UI.WebControls.TextBox

        Protected WithEvents optXSLInternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents cboXSL As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpload2 As System.Web.UI.WebControls.HyperLink
        Protected WithEvents optXSLExternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents txtXSL As System.Web.UI.WebControls.TextBox

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton

        Protected WithEvents txtAccount As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblSecurity As System.Web.UI.WebControls.Label

        Private ItemId As Integer = -1

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

            Title1.DisplayHelp = "DisplayHelp_EditRSS"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			cmdUpload1.Text = GetLanguage("upload")
			cmdUpload2.Text = GetLanguage("upload")
			cmdUpdate.Text = getLanguage("enregistrer")
			cmdCancel.Text = getlanguage("annuler")

            If optXMLExternal.Checked = False And optXMLInternal.Checked = False Then
                optXMLInternal.Checked = True
            End If
            If optXSLExternal.Checked = False And optXSLInternal.Checked = False Then
                optXSLInternal.Checked = True
            End If

            EnableControls()
            SetUpUpload(Not Page.IsPostBack)
            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)



                If ModuleId > 0 Then

                    ' Get settings from the database
                    Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)

                    If InStr(1, CType(settings("xmlsrc"), String), "://") = 0 Then
                        optXMLInternal.Checked = True
                        optXMLExternal.Checked = False
                        EnableControls()
                        If Not cboXML.Items.FindByText(CType(settings("xmlsrc"), String)) Is Nothing Then
                            cboXML.Items.FindByText(CType(settings("xmlsrc"), String)).Selected = True
                        End If
                    Else
                        optXMLInternal.Checked = False
                        optXMLExternal.Checked = True
                        EnableControls()
                        txtAccount.Text = CType(settings("account"), String)
                        txtPassword.Text = CType(settings("password"), String)
                        txtXML.Text = CType(settings("xmlsrc"), String)
                    End If

                    If InStr(1, CType(settings("xslsrc"), String), "://") = 0 Then
                        optXSLInternal.Checked = True
                        optXSLExternal.Checked = False
                        EnableControls()
                        If Not cboXSL.Items.FindByText(CType(settings("xslsrc"), String)) Is Nothing Then
                            cboXSL.Items.FindByText(CType(settings("xslsrc"), String)).Selected = True
                        End If
                    Else
                        optXSLInternal.Checked = False
                        optXSLExternal.Checked = True
                        EnableControls()
                        txtAccount.Text = CType(settings("account"), String)
                        txtPassword.Text = CType(settings("password"), String)
                        txtXSL.Text = CType(settings("xslsrc"), String)
                    End If

                End If


            End If

        End Sub

        Private Sub SetUpUpload(ByVal SetFile As Boolean)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            UploadReturn.Visible = True
            UploadReturn.Style.Add("display", "none")
            cmdUpload1.NavigateUrl = "#"
            cmdUpload2.NavigateUrl = "#"
            SetUpModuleUpload(Request.MapPath(_portalSettings.UploadDirectory), "rss,xml,xsl,zip", False, True, "rss,xml,xsl")
            Dim incScript As String = String.Format("<script Language=""javascript"" SRC=""{0}""></script>", ResolveUrl("/admin/advFileManager/dialog.js"))
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "FileManager", incScript)
            Dim retScript As String = "<script language=""javascript"">" & vbCrLf & "<!--" & vbCrLf
            retScript &= "function retVal()" & vbCrLf & "{" & vbCrLf
            retScript &= vbTab & Page.ClientScript.GetPostBackEventReference(UploadReturn, String.Empty) & ";" & vbCrLf & "}" & vbCrLf
            retScript &= "--></script>"
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "FileManagerRefresh", retScript)
            Dim click As String = String.Format("openDialog('{0}', 700, 600, retVal);return false", ResolveUrl("/Admin/AdvFileManager/TAGFileUploadDialog.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)) & IIf(Not (Request.Params("hostpage") Is Nothing), "&hostpage=", ""), UploadReturn.ClientID)
            cmdUpload1.Attributes.Add("onclick", click)
            cmdUpload2.Attributes.Add("onclick", click)
            If SetFile Then
                ' load the list of files found in the upload directory
                Dim FileList1 As ArrayList = GetFileList(_portalSettings.PortalId, "rss,xml")
                cboXML.DataSource = FileList1
                cboXML.DataBind()
                Dim FileList2 As ArrayList = GetFileList(_portalSettings.PortalId, "xsl")
                cboXSL.DataSource = FileList2
                cboXSL.DataBind()
            End If
        End Sub

        Private Sub UploadReturn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles UploadReturn.Click
            SetUpUpload(True)
        End Sub
        Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

            ' Update settings in the database
            Dim admin As New AdminDB()

            Dim strXML As String

            If txtXML.Text <> "" Then
                strXML = AddHTTP(txtXML.Text)
            Else
                strXML = cboXML.SelectedItem.Value
            End If

            Dim strXSL As String

            If txtXSL.Text <> "" Then
                strXSL = AddHTTP(txtXSL.Text)
            Else
                strXSL = cboXSL.SelectedItem.Value
            End If

            admin.UpdateModuleSetting(ModuleId, "xmlsrc", strXML)
            admin.UpdateModuleSetting(ModuleId, "xslsrc", strXSL)
            admin.UpdateModuleSetting(ModuleId, "account", txtAccount.Text)
            admin.UpdateModuleSetting(ModuleId, "password", txtPassword.Text)
            ' Redirect back to the portal home page
            HttpContext.Current.Session("UploadInfo") = Nothing
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            HttpContext.Current.Session("UploadInfo") = Nothing
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

        Private Sub EnableControls()
            Dim blnAccount As Boolean = False
            If optXMLExternal.Checked Then
                cboXML.ClearSelection()
                cboXML.Enabled = False
                txtXML.Enabled = True
                blnAccount = True
            Else
                cboXML.Enabled = True
                txtXML.Text = ""
                txtXML.Enabled = False
            End If

            If optXSLExternal.Checked Then
                cboXSL.ClearSelection()
                cboXSL.Enabled = False
                txtXSL.Enabled = True
                blnAccount = True

            Else
                cboXSL.Enabled = True
                txtXSL.Text = ""
                txtXSL.Enabled = False
            End If
            If blnAccount Then
                txtAccount.Enabled = True
                txtPassword.Enabled = True
                lblSecurity.Enabled = True
            Else
                txtAccount.Enabled = False
                txtPassword.Enabled = False
                lblSecurity.Enabled = False
            End If
        End Sub

    End Class

End Namespace