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

    Public Class EditFAQs
        Inherits DotNetZoom.PortalModuleControl
		
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Protected WithEvents QuestionField As System.Web.UI.WebControls.TextBox
        Protected WithEvents AnswerField As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents pnlAudit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents CreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedDate As System.Web.UI.WebControls.Label

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

        '*******************************************************
        '
        ' The Page_Load server event handler on this page is used
        ' to populate the role information for the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Title1.DisplayHelp = "DisplayHelp_EditFAQ"
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			cmdUpdate.Text = GetLanguage("enregistrer")
			cmdCancel.Text = GetLanguage("annuler")
			cmdDelete.Text = GetLanguage("delete")		

            ' Determine ItemId of Contacts to Update
            If IsNumeric(Request.Params("ItemID")) Then
                ItemId = Int32.Parse(Request.Params("ItemID"))
            End If

            ' If this is the first visit to the page, bind the role data to the datalist
            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If

                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm")) & "');")

                If ItemId <> -1 Then

                    ' Obtain a single row of FAQ information
                    Dim FAQs As New FAQsDB()
                    Dim dr As SqlDataReader = FAQs.GetSingleFAQ(ItemId, ModuleId)

                    ' Read first row from database
                    If dr.Read() Then
                        QuestionField.Text = Server.HtmlDecode(Replace(CType(dr("Question"), String), "<br>", vbCrLf))
                        AnswerField.Text = Server.HtmlDecode(Replace(CType(dr("Answer"), String), "<br>", vbCrLf))
                        CreatedBy.Text = dr("CreatedByUser").ToString
                        CreatedDate.Text = CType(dr("CreatedDate"), DateTime).ToShortDateString()

                        ' Close datareader
                        dr.Close()
                    Else ' security violation attempt to access item not related to this Module
                        dr.Close()
                        Response.Redirect(GetFullDocument() & "?tabid=" & TabId, True)
                    End If

                Else
                    cmdDelete.Visible = False
                    pnlAudit.Visible = False
                End If

            End If
        End Sub

        '****************************************************************
        '
        ' The cmdUpdate_Click event handler on this Page is used to either
        ' create or update a FAQ.  It  uses the DotNetZoom.FAQsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs)
            Page.Validate()

            ' Only Update if Entered data is Valid
            If Page.IsValid = True Then

                ' Create an instance of the FAQsDB component
                Dim FAQs As New FAQsDB()

                If ItemId = -1 Then
                    ' Add the FAQ within the FAQs table
                    FAQs.AddFAQ(ModuleId, Context.User.Identity.Name, Replace(Server.HtmlEncode(QuestionField.Text), vbCrLf, "<br>"), Replace(Server.HtmlEncode(AnswerField.Text), vbCrLf, "<br>"))
                Else
                    ' Update the FAQ within the FAQs table
                    FAQs.UpdateFAQ(ItemId, Context.User.Identity.Name, Replace(Server.HtmlEncode(QuestionField.Text), vbCrLf, "<br>"), Replace(Server.HtmlEncode(AnswerField.Text), vbCrLf, "<br>"))
                End If
				' Reset data cashe
				
				ClearModuleCache(ModuleId)

                ' Redirect back to the portal home page
                Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

            End If

        End Sub


        '****************************************************************
        '
        ' The cmdDelete_Click event handler on this Page is used to delete an
        ' a FAQ.  It  uses the DotNetZoom.FAQsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Sub cmdDelete_Click(ByVal sender As Object, ByVal e As EventArgs)

            ' Only attempt to delete the item if it is an existing item
            ' (new items will have "ItemId" of -1)
            If ItemId <> -1 Then

                Dim FAQs As New FAQsDB()
                FAQs.DeleteFAQ(ItemId)

            End If
			' Reset data cashe
			
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
        '****************************************************************

        Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

    End Class

End Namespace