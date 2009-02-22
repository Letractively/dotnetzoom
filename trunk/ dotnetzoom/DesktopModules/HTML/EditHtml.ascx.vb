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

    Public Class EditHtml
        Inherits DotNetZoom.PortalModuleControl
		
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Protected WithEvents optView As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents pnlBasicTextBox As System.Web.UI.WebControls.PlaceHolder

        Protected WithEvents pnlRichTextBox As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtDesktopHTML As System.Web.UI.WebControls.TextBox

        Protected WithEvents FCKeditor1 As FredCK.FCKeditorV2.FCKEditor
			
        Protected WithEvents txtAlternateSummary As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAlternateDetails As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdPreview As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblPreview As System.Web.UI.WebControls.Label
		

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
        ' of the xml module to edit.
        '
        ' It then uses the DotNetZoom.HtmlTextDB() data component
        ' to populate the page's edit controls with the text details.
        '
        '****************************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
           Title1.DisplayHelp = "DisplayHelp_EditHTML"
		   ' Obtain PortalSettings from Current Context
			cmdUpdate.Text = GetLanguage("enregistrer")
			cmdCancel.Text = GetLanguage("annuler")
			cmdPreview.Text = GetLanguage("visualiser")
			optView.Items.FindByValue("B").Text = GetLanguage("text")
			optView.Items.FindByValue("R").Text = GetLanguage("html")
            ' Get settings from the database 
            Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)

            Dim DesktopView As String = CType(settings("desktopview"), String)
            Dim LastDesktopView As String = DesktopView


		
			optView.Visible = True
            If optView.SelectedIndex <> -1 Then 
                DesktopView = optView.SelectedItem.Value
                Dim admin As New AdminDB()
                admin.UpdateModuleSetting(ModuleId, "desktopview", DesktopView)
            End If

            If DesktopView <> "" Then
                optView.Items.FindByValue(DesktopView).Selected = True
            Else
                optView.SelectedIndex = 0
            End If

            If optView.SelectedItem.Value = "B" Then
				optView.SelectedItem.Value = "B"
                pnlBasicTextBox.Visible = True
                pnlRichTextBox.Visible = False
			End If

            If optView.SelectedItem.Value = "R" Then
				optView.SelectedItem.Value = "R"
                pnlBasicTextBox.Visible = False
				lblPreview.Text = ""
				cmdPreview.Visible = False
				pnlRichTextBox.Visible = True
				SetFckEditor()
            End If
        
		If Page.IsPostBack = False Then
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If

                ' Obtain a single row of text information
                Dim objHTML As New HtmlTextDB()
                Dim dr As SqlDataReader = objHTML.GetHtmlText(ModuleId)

                If dr.Read() Then
					
                    txtDesktopHTML.Text = Server.HtmlDecode(CType(dr("DesktopHtml"), String))
                    FCKeditor1.value = Server.HtmlDecode(CType(dr("DesktopHtml"), String))
                    txtAlternateSummary.Text = Server.HtmlDecode(CType(dr("AlternateSummary"), String))
                    txtAlternateDetails.Text = Server.HtmlDecode(CType(dr("AlternateDetails"), String))
                Else
					FCKeditor1.value = GetLAnguage("HTML_ADDtxt")
                    txtDesktopHTML.Text = GetLAnguage("HTML_ADDtxt")
                    txtAlternateSummary.Text = ""
                    txtAlternateDetails.Text = ""
                End If

                dr.Close()

            Else
                If (LastDesktopView <> optView.SelectedItem.Value) And (Not LastDesktopView Is Nothing) Then
                    If optView.SelectedItem.Value = "B" Then
                        cmdPreview.Visible = True
                        txtDesktopHTML.Text = FCKeditor1.Value
                    Else
                        cmdPreview.Visible = False
                        FCKeditor1.Value = txtDesktopHTML.Text
                    End If
                End If
			End If
        End Sub


		Private Sub SetFckEditor()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objAdmin As New AdminDB()
            Dim tmpUploadRoles As String = ""
            If Not CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("uploadroles"), String) Is Nothing Then
                tmpUploadRoles = CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("uploadroles"), String)
            End If
            If PortalSecurity.IsInRoles(tmpUploadRoles) Then
                Session("FCKeditor:UserFilesPath") = _portalSettings.UploadDirectory
                FCKeditor1.LinkBrowserURL = glbPath & "admin/AdvFileManager/filemanager.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
                FCKeditor1.ImageBrowserURL = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
            Else
                Session("FCKeditor:UserFilesPath") = Nothing
                FCKeditor1.LinkBrowserURL = ""
                FCKeditor1.ImageBrowserURL = ""
            End If
            FCKeditor1.Width = Unit.Pixel(700)
            FCKeditor1.Height = Unit.Pixel(500)
            SetEditor(FCKeditor1)
        End Sub
		

        '****************************************************************
        '
        ' The cmdUpdate_Click event handler on this Page is used to save
        ' the text changes to the database.
        '
        '****************************************************************

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            Dim objHTML As New HtmlTextDB()

            Dim strDesktopHTML As String
            If pnlBasicTextBox.Visible Then
               strDesktopHTML = txtDesktopHTML.Text
            Else
                strDesktopHTML = Server.HtmlEncode(FCKeditor1.Value)
            End If

			If StrDeskTopHTML <> "" then
			strDesktopHTML = ConvertColor(strDesktopHTML)
			else
			StrDeskTopHTML = " "
			end if

            ' Update the text within the HtmlText table
            objHTML.UpdateHtmlText(ModuleId, strDesktopHTML, Server.HtmlEncode(txtAlternateSummary.Text), Server.HtmlEncode(txtAlternateDetails.Text))

			
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

        Private Sub cmdPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPreview.Click

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If pnlBasicTextBox.Visible Then
                lblPreview.Text = txtDesktopHTML.Text
            Else
                lblPreview.Text = ""
			End IF

        End Sub


		
        Public Shared Function ConvertColor(ByVal Colorvalue As String) As String

            'Convert for HTML 4 Compatibility
            Colorvalue = Colorvalue.Replace(""" />", """>")
            Colorvalue = Colorvalue.Replace("<br />", "<br>")
            Colorvalue = Colorvalue.Replace(""" /&gt;", """&gt;")
            Colorvalue = Colorvalue.Replace("&lt;br /&gt;", "&lt;br&gt;")




            ' convert color string
            Dim StartPoint As Integer

            StartPoint = 1

            While (StartPoint <> 0) And (StartPoint < Colorvalue.Length)
                StartPoint = Colorvalue.IndexOf("color=#", StartPoint)
                If StartPoint > 0 Then
                    ' validate if ok
                    If IsHex(Colorvalue.Substring(StartPoint + 6, 7)) Then
                        Colorvalue = Colorvalue.Insert(StartPoint + 6, """")
                        Colorvalue = Colorvalue.Insert(StartPoint + 14, """")
                    End If
                    StartPoint = StartPoint + 12
                Else
                    Exit While
                End If
            End While
            Return Colorvalue
        End Function

		Public Shared Function IsHex(ByVal Hex As String) As Boolean
        Dim TempInt as integer
			Try
			TempInt = Convert.ToInt32(Hex.Substring(1, 2), 16)
			TempInt = Convert.ToInt32(Hex.Substring(3, 2), 16)
			TempInt = Convert.ToInt32(Hex.Substring(5, 2), 16)
            Catch objException As Exception
			Return False 
 			end Try
			Return True
		End Function
		
		
    End Class

End Namespace