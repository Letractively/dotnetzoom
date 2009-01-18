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
			Dim objAdmin As New DotNetZoom.AdminDB()
			Dim Language As String = GetLanguage("N")
			Dim TempString As String
			
			If Request.Params("edit") = "mod" and PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) then
				If Not Request.Params("Language") is Nothing then
				Language = Request.Params("Language")
				end if
				If objAdmin.GetSinglelonglanguageSettings(Language, "PortalPrivacy", _PortalSettings.PortalID) = "" then
				TempString = objAdmin.GetSinglelonglanguageSettings(Language, "PortalPrivacy")
				If TempString <> "" then
				objAdmin.UpdatelonglanguageSetting(Language, "PortalPrivacy" , TempString, _PortalSettings.PortalID)
				FCKeditor1.value = TempString
				else
				objAdmin.UpdatelonglanguageSetting(Language, "PortalPrivacy" , lblPrivacy.Text, _PortalSettings.PortalID)
				FCKeditor1.value = lblPrivacy.Text
				end if
				else
				FCKeditor1.value = objAdmin.GetSinglelonglanguageSettings(Language, "PortalPrivacy", _PortalSettings.PortalID)
				end if
            	' Store URL Referrer to return to portal
              	If Not Request.UrlReferrer Is Nothing Then
              	  ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
             	Else
             	    ViewState("UrlReferrer") = ""
            	End If
				Dim _language As HashTable = HttpContext.Current.Items("Language")
				cmdUpdate.Text = _Language("enregistrer")
				cmdUpdate.CommandArgument = Language
				cmdCancel.Text = _Language("return")
				cmdCancel.Attributes.Add("onClick", "javascript:window.close();")
				pnlRichTextBox.Visible = True
				SetFckEditor()
			else
				If objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalPrivacy", _PortalSettings.PortalID) = "" then
				TempString = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalPrivacy")
				If TempString <> "" then
				objAdmin.UpdatelonglanguageSetting(GetLanguage("N"), "PortalPrivacy" , TempString, _PortalSettings.PortalID)
				lblPrivacy.Text = TempString
				else
				objAdmin.UpdatelonglanguageSetting(GetLanguage("N"), "PortalPrivacy" , lblPrivacy.Text, _PortalSettings.PortalID)
				end if
				else
				lblPrivacy.Text = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalPrivacy", _PortalSettings.PortalID)
				end if
				Privacy.Visible = True
				lblPrivacy.Text = ProcessLanguage(lblPrivacy.Text, Page)
           end if
		end if
        End Sub
		
        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New DotNetZoom.AdminDB()
			objAdmin.UpdatelonglanguageSetting(CType(sender, LinkButton).CommandArgument, "PortalPrivacy" , FCKeditor1.value, _PortalSettings.PortalID)
			SetFckEditor()
        End Sub

		Private Sub SetFckEditor()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' FCKeditor1.LinkBrowser = true
			FCKeditor1.width = unit.pixel(700)
			FCKeditor1.Height = unit.pixel(500)
			if GetLanguage("fckeditor_language") <> "auto"
			FCKeditor1.DefaultLanguage = GetLanguage("fckeditor_language")
			FCKeditor1.AutoDetectLanguage = False
			end if
            FCKeditor1.LinkBrowserURL = glbPath & "admin/AdvFileManager/filemanager.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
            FCKeditor1.ImageBrowserURL = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
            Session("FCKeditor:UserFilesPath") = _portalSettings.UploadDirectory
				' set the css for the editor if it exist
				If Directory.Exists(Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/")) then
				FCKeditor1.SkinPath =  _portalSettings.UploadDirectory & "skin/fckeditor/"
				FCKeditor1.EditorAreaCSS= _portalSettings.UploadDirectory & "skin/fckeditor/fck_editorarea.css"
				FCKeditor1.StylesXmlPath =  _portalSettings.UploadDirectory & "skin/fckeditor/fckstyles.xml"
                ' FCKeditor1.TemplatesXmlPath	= _portalSettings.UploadDirectory & "skin/fckeditor/fcktemplates.xml" 
				End If

		end sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub
		
		
    End Class

End Namespace