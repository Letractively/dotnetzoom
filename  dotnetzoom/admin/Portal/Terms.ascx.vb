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

    Public Class Terms
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents lblTerms As System.Web.UI.WebControls.Label
        Protected WithEvents pnlRichTextBox As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Terms As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents FCKeditor1 As DotNetZoom.FCKEditor
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

                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If

                Dim objAdmin As New DotNetZoom.AdminDB()
                Dim Language As String = GetLanguage("N")

                If Request.Params("edit") = "mod" And PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                    If Not Request.Params("Language") Is Nothing Then
                        Language = Request.Params("Language")
                    End If

                    If objAdmin.GetSinglelonglanguageSettings(Language, "PortalTerms", _portalSettings.PortalId) = "" Then
                        Dim TempString As String = objAdmin.GetSinglelonglanguageSettings(Language, "PortalTerms")
                        If TempString <> "" Then
                            objAdmin.UpdatelonglanguageSetting(Language, "PortalTerms", TempString, _portalSettings.PortalId)
                            FCKeditor1.Value = TempString
                        Else
                            objAdmin.UpdatelonglanguageSetting(Language, "PortalTerms", lblTerms.Text, _portalSettings.PortalId)
                            FCKeditor1.Value = lblTerms.Text
                        End If
                    Else
                        FCKeditor1.Value = objAdmin.GetSinglelonglanguageSettings(Language, "PortalTerms", _portalSettings.PortalId)
                    End If
                    Dim _language As Hashtable = HttpContext.Current.Items("Language")
                    cmdUpdate.Text = _language("enregistrer")
                    cmdUpdate.CommandArgument = Language
                    cmdCancel.Text = _language("return")
                    cmdCancel.Attributes.Add("onClick", "javascript:window.close();")

                    pnlRichTextBox.Visible = True
                    SetFckEditor()
                Else
                    If objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalTerms", _portalSettings.PortalId) = "" Then
                        Dim TempString As String = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalTerms")
                        If TempString <> "" Then
                            objAdmin.UpdatelonglanguageSetting(GetLanguage("N"), "PortalTerms", TempString, _portalSettings.PortalId)
                            lblTerms.Text = TempString
                        Else
                            objAdmin.UpdatelonglanguageSetting(GetLanguage("N"), "PortalTerms", lblTerms.Text, _portalSettings.PortalId)
                        End If
                    Else
                        lblTerms.Text = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalTerms", _portalSettings.PortalId)
                    End If
                    Terms.Visible = True
                    lblTerms.Text = ProcessLanguage(lblTerms.Text, Page)
                End If
            End If
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