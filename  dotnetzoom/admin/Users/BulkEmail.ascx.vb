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


    Public MustInherit Class BulkEmail
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents cboRoles As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtMessage As System.Web.UI.WebControls.TextBox
        Protected WithEvents cboAttachment As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpload As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnSend As System.Web.UI.WebControls.LinkButton
		Protected WithEvents btnSendAnother As System.Web.UI.WebControls.LinkButton
		Protected WithEvents btnReturn As System.Web.UI.WebControls.LinkButton
		Protected WithEvents TableSendTo As System.Web.UI.HtmlControls.HtmlTable
		Protected WithEvents TableMessage As System.Web.UI.HtmlControls.HtmlTable
		Protected WithEvents SendTo As System.Web.UI.WebControls.Literal
		
        Protected WithEvents pnlBasicTextBox As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlRichTextBox As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents FCKeditor1 As FredCK.FCKeditorV2.FCKEditor
        Protected WithEvents optView As System.Web.UI.WebControls.RadioButtonList
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Not PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If
            Title1.DisplayHelp = "DisplayHelp_BulkEmail"

            If Not Page.IsPostBack Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    btnReturn.Visible = False
                End If

                TableMessage.Visible = True
                TableSendTo.Visible = False
                optView.Items.FindByValue("B").Text = GetLanguage("text")
                optView.Items.FindByValue("R").Text = GetLanguage("html")
                cmdUpload.Text = GetLanguage("upload")
                btnSend.Text = GetLanguage("Mail_Send")
                btnSendAnother.Text = GetLanguage("Mail_Send_Again")
                btnReturn.Text = GetLanguage("return")

                Dim objUser As New UsersDB()
                Dim dr As SqlDataReader = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))
                cboRoles.DataSource = dr
                cboRoles.DataBind()
                dr.Close()
                cboRoles.Items.Insert(0, New ListItem(GetLanguage("list_none"), "-1"))
                cmdUpload.NavigateUrl = GetFullDocument() & "?edit=control&tabid=" & TabId & "&def=Gestion fichiers"

                Dim FileList As ArrayList
                FileList = GetFileList(_portalSettings.PortalId)
                cboAttachment.DataSource = FileList
                cboAttachment.DataBind()
                SetFckEditor()





            Else
                If optView.SelectedItem.Value = "B" Then
                    optView.SelectedItem.Value = "B"
                    pnlBasicTextBox.Visible = True
                    pnlRichTextBox.Visible = False
                Else
                    pnlBasicTextBox.Visible = False
                    pnlRichTextBox.Visible = True
                End If
            End If
        End Sub

		Private Sub SetFckEditor()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Session("FCKeditor:UserFilesPath") = _portalSettings.UploadDirectory

				FCKeditor1.width = unit.pixel(700)
				FCKeditor1.Height = unit.pixel(500)
				if GetLanguage("fckeditor_language") <> "auto"
				FCKeditor1.DefaultLanguage = GetLanguage("fckeditor_language")
				FCKeditor1.AutoDetectLanguage = False
				end if
            	FCKeditor1.LinkBrowserURL = glbPath & "admin/AdvFileManager/filemanager.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
            	FCKeditor1.ImageBrowserURL = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
				' set the css for the editor if it exist
				If Directory.Exists(Request.MapPath(_portalSettings.UploadDirectory & "skin/fckeditor/")) then
				FCKeditor1.SkinPath =  _portalSettings.UploadDirectory & "skin/fckeditor/"
				FCKeditor1.EditorAreaCSS= _portalSettings.UploadDirectory & "skin/fckeditor/fck_editorarea.css"
				FCKeditor1.StylesXmlPath =  _portalSettings.UploadDirectory & "skin/fckeditor/fckstyles.xml"
                ' FCKeditor1.TemplatesXmlPath	= _portalSettings.UploadDirectory & "skin/fckeditor/fcktemplates.xml" 
				End If

        End Sub
		
        Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
		Response.Redirect(CStr(ViewState("UrlReferrer")), True)
		end sub

        Private Sub btnSendAnother_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendAnother.Click
	    TableMessage.Visible = True
		TableSendTo.Visible = False
		SetFckEditor()
		end sub

		
        Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
            Dim strBody As String
			SendTo.Text = ""
			TableMessage.Visible = False
			TableSendTo.Visible = True

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' send to role membership
            If cboRoles.SelectedIndex > 0 Then
                Dim objUser As New UsersDB()
                Dim dr As SqlDataReader = objUser.GetRoleMembership(_portalSettings.PortalId, GetLanguage("N"), Integer.Parse(cboRoles.SelectedItem.Value))
                While dr.Read()

                    Dim sMimeType As String
                    If optView.SelectedItem.Value = "B" Then
                        sMimeType = "text"
                        strBody = dr("FullName") & "," & vbCrLf & vbCrLf
                        strBody = strBody & txtMessage.Text & vbCrLf & vbCrLf
                        strBody = strBody & _portalSettings.PortalName
                    Else
                        sMimeType = "html"
                        strBody = dr("FullName") & ",<p>"
						strBody = strBody & FCKeditor1.value & "<p>" 
                        strBody = strBody & _portalSettings.PortalName
                    End If
                SendTo.Text += "<br>" + dr("FullName") + " " + dr("Email").ToString + " "  +  SendNotification(_portalSettings.Email, dr("Email").ToString, "", txtSubject.Text, strBody, IIf(cboAttachment.SelectedItem.Value <> "", Request.MapPath(_portalSettings.UploadDirectory) & cboAttachment.SelectedItem.Value, ""), sMimeType)
                End While
                dr.Close()
            cboRoles.SelectedIndex = 0
			End If
			
            ' send to email distribution list
            If txtEmail.Text <> "" Then
                Dim arrEmail As Array = Split(txtEmail.Text, ";")
                Dim strEmail As String
                For Each strEmail In arrEmail

                    Dim sMimeType As String
                    If optView.SelectedItem.Value = "B" Then
                       sMimeType = "text"
                       strBody = strEmail & "," & vbCrLf & vbCrLf
                       strBody = strBody & txtMessage.Text & vbCrLf & vbCrLf
					   strBody = strBody & _portalSettings.PortalName
                    Else
                        sMimeType = "html"
                        strBody = strEmail & ",<p>"
						strBody = strBody & FCKeditor1.value & "<p>" 
                        strBody = strBody & _portalSettings.PortalName
                    End If
                SendTo.Text += "<br>" + strEmail + " " +  SendNotification(_portalSettings.Email, strEmail, "", txtSubject.Text, strBody, IIf(cboAttachment.SelectedItem.Value <> "", Request.MapPath(_portalSettings.UploadDirectory) & cboAttachment.SelectedItem.Value, ""), sMimeType)
                Next
			txtEmail.Text = ""	
            End If
        End Sub

        Private Sub optView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optView.SelectedIndexChanged
            If optView.SelectedItem.Value = "B" Then
			    txtMessage.Text = FCKeditor1.value
            Else
			    FCKeditor1.value = txtMessage.Text
				SetFckEditor()
            End If
        End Sub
    End Class

End Namespace