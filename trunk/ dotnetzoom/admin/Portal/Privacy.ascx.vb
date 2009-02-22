Imports System.IO

Namespace DotNetZoom

    Public Class Privacy
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents lblPrivacy As System.Web.UI.WebControls.Label
        Protected WithEvents pnlRichTextBox As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Privacy As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents FCKeditor1 As FredCK.FCKeditorV2.FCKEditor
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
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
        ' The Page_Load server event handler on this page is used
        ' to populate the role information for the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

   		Title1.DisplayTitle = getlanguage("privacy_declaration")
		Title1.DisplayHelp = "DisplayHelp_Privacy"
            If Page.IsPostBack = False Then
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If
                Dim objAdmin As New DotNetZoom.AdminDB()
                Dim Language As String = GetLanguage("N")
                Dim TempString As String

                If Request.Params("edit") = "mod" And PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                    If Not Request.Params("Language") Is Nothing Then
                        Language = Request.Params("Language")
                    End If
                    If objAdmin.GetSinglelonglanguageSettings(Language, "PortalPrivacy", _portalSettings.PortalId) = "" Then
                        TempString = objAdmin.GetSinglelonglanguageSettings(Language, "PortalPrivacy")
                        If TempString <> "" Then
                            objAdmin.UpdatelonglanguageSetting(Language, "PortalPrivacy", TempString, _portalSettings.PortalId)
                            FCKeditor1.Value = TempString
                        Else
                            objAdmin.UpdatelonglanguageSetting(Language, "PortalPrivacy", lblPrivacy.Text, _portalSettings.PortalId)
                            FCKeditor1.Value = lblPrivacy.Text
                        End If
                    Else
                        FCKeditor1.Value = objAdmin.GetSinglelonglanguageSettings(Language, "PortalPrivacy", _portalSettings.PortalId)
                    End If
                    Dim _language As Hashtable = HttpContext.Current.Items("Language")
                    cmdUpdate.Text = _language("enregistrer")
                    cmdUpdate.CommandArgument = Language
                    cmdCancel.Text = _language("return")
                    cmdCancel.Attributes.Add("onClick", "javascript:window.close();")
                    pnlRichTextBox.Visible = True
                    SetFckEditor()
                Else
                    If objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalPrivacy", _portalSettings.PortalId) = "" Then
                        TempString = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalPrivacy")
                        If TempString <> "" Then
                            objAdmin.UpdatelonglanguageSetting(GetLanguage("N"), "PortalPrivacy", TempString, _portalSettings.PortalId)
                            lblPrivacy.Text = TempString
                        Else
                            objAdmin.UpdatelonglanguageSetting(GetLanguage("N"), "PortalPrivacy", lblPrivacy.Text, _portalSettings.PortalId)
                        End If
                    Else
                        lblPrivacy.Text = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalPrivacy", _portalSettings.PortalId)
                    End If
                    Privacy.Visible = True
                    lblPrivacy.Text = ProcessLanguage(lblPrivacy.Text, Page)
                End If
            End If
        End Sub
		
        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New DotNetZoom.AdminDB()
			objAdmin.UpdatelonglanguageSetting(CType(sender, LinkButton).CommandArgument, "PortalPrivacy" , FCKeditor1.value, _PortalSettings.PortalID)
            SetFckEditor()
        End Sub

        Private Sub SetFckEditor()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            FCKeditor1.Width = Unit.Pixel(700)
            FCKeditor1.Height = Unit.Pixel(500)
            SetEditor(FCKeditor1)
            FCKeditor1.LinkBrowserURL = glbPath & "admin/AdvFileManager/filemanager.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
            FCKeditor1.ImageBrowserURL = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
            Session("FCKeditor:UserFilesPath") = _portalSettings.UploadDirectory
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub
		
		
    End Class

End Namespace