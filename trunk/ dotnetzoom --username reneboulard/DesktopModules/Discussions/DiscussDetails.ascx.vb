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

Namespace DotNetZoom

    Public Class DiscussDetails
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents cmdReply As System.Web.UI.WebControls.LinkButton
        Protected WithEvents TitleField As System.Web.UI.WebControls.TextBox
        Protected WithEvents BodyField As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents EditPanel As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Subject As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedByUser As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedDate As System.Web.UI.WebControls.Label
        Protected WithEvents Body As System.Web.UI.WebControls.Label
        Protected WithEvents cmdCancel2 As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdEdit As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents tblOriginalMessage As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents rowOriginalMessage As System.Web.UI.HtmlControls.HtmlTableRow
		Protected WithEvents pnlOptions As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents pnlContent As System.Web.UI.WebControls.PlaceHolder
	
		
        Private itemId As Integer = -1
		Protected WithEvents valTitleField As System.Web.UI.WebControls.RequiredFieldValidator
		Protected WithEvents valBodyField As System.Web.UI.WebControls.RequiredFieldValidator
        Private itemIndex As Integer = -1

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

            Dim strURLReferrer As String
			Title1.DisplayHelp = "DisplayHelp_DiscussDetails"
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			valTitleField.ErrorMessage =  "<br>" + GetLanguage("need_object_message")
			valBodyField.ErrorMessage =  "<br>" + GetLanguage("need_body_message")
			cmdCancel2.Text = getlanguage("annuler")
			cmdReply.Text = getlanguage("Reply")
			cmdEdit.Text = getlanguage("modifier")
			cmdDelete.Text = getlanguage("delete")
            If IsNumeric(Request.Params("ItemIndex")) Then
                itemIndex = Int32.Parse(Request.Params("ItemIndex"))
            End If

            If IsNumeric(Request.Params("ItemID")) Then
                itemId = Int32.Parse(Request.Params("ItemID"))
            Else
                EditPanel.Visible = True
                cmdReply.Visible = False
                cmdUpdate.Text = GetLanguage("enregistrer")
            End If

            ' Populate message contents if this is the first visit to the page
            If Page.IsPostBack = False Then
			   ' module options
				If not request.params("options") is nothing then
				pnlOptions.visible = True
				pnlContent.visible = False
				end if

				cmdUpdate.Text = getLanguage("enregistrer")
				cmdCancel.Text = getlanguage("annuler")

                If Not PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                    cmdEdit.Visible = False
                    cmdDelete.Visible = False
                End If

                If itemId <> -1 Then
                    ' Obtain the selected item from the Discussion table
                    Dim discuss As New DiscussionDB()
                    Dim dr As SqlDataReader = discuss.GetSingleMessage(itemId, ModuleId)

                    ' Load row from database
                    If dr.Read() Then
                        Dim objSecurity As New PortalSecurity()
                        Subject.Text = objSecurity.InputFilter(CType(dr("Title"), String), PortalSecurity.FilterFlag.MultiLine)
                        Body.Text = objSecurity.InputFilter(CType(dr("Body"), String), PortalSecurity.FilterFlag.MultiLine)
                        CreatedByUser.Text = dr("CreatedByUser").ToString
						If CreatedByUser.Text = "Anonymous Anonymous" then
						CreatedByUser.Text = GetLanguage("label_anonymous")
						end if
                        CreatedDate.Text = String.Format("{0:d}", dr("CreatedDate"))
                        TitleField.Text = ReTitle(CType(dr("Title"), String))
                        BodyField.Text = CType(dr("Body"), String)
                        dr.Close()
                    Else ' security violation attempt to access item not related to this Module
                        dr.Close()
                        Response.Redirect("~" & GetDocument() & "?tabid=" & TabId, True)
                    End If
                Else
                    tblOriginalMessage.Visible = False
                End If

                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                strURLReferrer = Request.UrlReferrer.ToString()
                    If InStr(1, strURLReferrer, "ItemIndex=") <> 0 Then
                        strURLReferrer = Left(strURLReferrer, InStr(1, strURLReferrer, "ItemIndex=") - 2)
                    End If

                    If InStr(1, strURLReferrer, "?") = 0 Then
                        strURLReferrer = strURLReferrer & IIf(itemIndex <> -1, "?ItemIndex=" & itemIndex, "")
                    Else
                        strURLReferrer = strURLReferrer & IIf(itemIndex <> -1, "&ItemIndex=" & itemIndex, "")
                    End If
                ViewState("UrlReferrer") = strURLReferrer
                Else
                ViewState("UrlReferrer") = ""
                End If

            End If

            If PortalSecurity.HasEditPermissions(ModuleId) = False Then
                 If itemId = -1 Then
                    Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Register", True)
                 Else
                    cmdReply.Visible = False
                 End If
            End If
        End Sub

        Private Sub cmdReply_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdReply.Click

            BodyField.Text = ""
            EditPanel.Visible = True
            rowOriginalMessage.Visible = True
            cmdReply.Visible = False
            cmdCancel2.Visible = False
            cmdEdit.Visible = False
            cmdDelete.Visible = False
			cmdUpdate.Text = getLanguage("enregistrer")
        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            ' Create new discussion database component
            Dim discuss As New DiscussionDB()
			Dim TempUser As String = "1"
            If Request.IsAuthenticated Then
                TempUser = CType(Context.User.Identity.Name, String)
            End If

            If cmdUpdate.Text = getLanguage("update") Then
                discuss.UpdateMessage(itemId, TempUser, TitleField.Text, BodyField.Text)
            Else
                discuss.AddMessage(ModuleId, itemId, TempUser, TitleField.Text, BodyField.Text)
            End If

			' Reset data cashe
			
			ClearModuleCache(ModuleId)

			
            Response.Redirect(ViewState("UrlReferrer"), True)

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click, cmdCancel2.Click
            Response.Redirect(ViewState("UrlReferrer"), True)
        End Sub

        Function ReTitle(ByVal title As String) As String

            If title.Length > 0 And title.IndexOf(getLanguage("new_message_re"), 0) = -1 Then
                title = getLanguage("new_message_re") & title
            End If

            Return title

        End Function

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
            ' Create new discussion database component
            Dim discuss As New DiscussionDB()
            discuss.DeleteMessage(itemId)
			' Reset data cashe
			
			ClearModuleCache(ModuleId)

            Response.Redirect(ViewState("UrlReferrer"), True)
        End Sub

        Private Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

            EditPanel.Visible = True
            rowOriginalMessage.Visible = True
            cmdReply.Visible = False
            cmdCancel2.Visible = False
            cmdEdit.Visible = False
            cmdDelete.Visible = False
			cmdUpdate.Text = getLanguage("update")
            
        End Sub

    End Class

End Namespace