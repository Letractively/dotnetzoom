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

    Public Class Terms
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents lblTerms As System.Web.UI.WebControls.Label
        Protected WithEvents pnlRichTextBox As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Terms As System.Web.UI.WebControls.PlaceHolder
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
   		Title1.DisplayTitle = getlanguage("title_terms")
		Title1.DisplayHelp = "DisplayHelp_Terms"

            ' Obtain PortalSettings from Current Context
		If Page.IsPostBack = False Then

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New DotNetZoom.AdminDB()
			Dim Language As String = GetLanguage("N")

			If Request.Params("edit") = "mod" and PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) then
			If Not Request.Params("Language") is Nothing then
			Language = Request.Params("Language")
			end if

			If objAdmin.GetSinglelonglanguageSettings(Language, "PortalTerms" , _PortalSettings.PortalID) = "" then
				Dim TempString As String = objAdmin.GetSinglelonglanguageSettings(Language, "PortalTerms")
				If TempString <> "" then
				objAdmin.UpdatelonglanguageSetting(Language, "PortalTerms" , TempString, _PortalSettings.PortalID)
				FCKeditor1.value = TempString
				else
				objAdmin.UpdatelonglanguageSetting(Language, "PortalTerms" , lblTerms.Text , _PortalSettings.PortalID)
				FCKeditor1.value = lblTerms.Text
				end if
			else
			FCKeditor1.value = objAdmin.GetSinglelonglanguageSettings(Language, "PortalTerms" , _PortalSettings.PortalID)
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
			If objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalTerms" , _PortalSettings.PortalID) = "" then
				Dim TempString As String = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalTerms")
				If TempString <> "" then
				objAdmin.UpdatelonglanguageSetting(GetLanguage("N"), "PortalTerms" , TempString, _PortalSettings.PortalID)
				lblTerms.Text = TempString
				else
				objAdmin.UpdatelonglanguageSetting(GetLanguage("N"), "PortalTerms" , lblTerms.Text , _PortalSettings.PortalID)
				end if
			else
			lblTerms.Text = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalTerms" , _PortalSettings.PortalID)
			end if
				Terms.Visible = True
				lblTerms.Text = ProcessLanguage(lblTerms.Text, Page)
           end if
		end if
        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New DotNetZoom.AdminDB()
			objAdmin.UpdatelonglanguageSetting(CType(sender, LinkButton).CommandArgument, "PortalTerms" , FCKeditor1.value, _PortalSettings.PortalID)
			SetFckEditor()
        End Sub

	
		Private Sub SetFckEditor()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            FCKeditor1.Width = Unit.Pixel(700)
			FCKeditor1.Height = unit.pixel(500)
			if GetLanguage("fckeditor_language") <> "auto"
			FCKeditor1.DefaultLanguage = GetLanguage("fckeditor_language")
			FCKeditor1.AutoDetectLanguage = False
			end if
            FCKeditor1.LinkBrowserURL = glbPath & "admin/AdvFileManager/filemanager.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
            FCKeditor1.ImageBrowserURL = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
            Session("FCKeditor:UserFilesPath") = _portalSettings.UploadDirectory
			' set the css for the editor if it exist
				' set the css for the editor if it exist
				If Directory.Exists(Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/")) then
				FCKeditor1.SkinPath =  _portalSettings.UploadDirectory & "skin/fckeditor/"
				FCKeditor1.EditorAreaCSS= _portalSettings.UploadDirectory & "skin/fckeditor/fck_editorarea.css"
				FCKeditor1.StylesXmlPath =  _portalSettings.UploadDirectory & "skin/fckeditor/fckstyles.xml"
            End If

		end sub
		
        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

    End Class

End Namespace