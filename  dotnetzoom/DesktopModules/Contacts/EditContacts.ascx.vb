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

    Public Class EditContacts
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents NameField As System.Web.UI.WebControls.TextBox
		Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents RoleField As System.Web.UI.WebControls.TextBox
        Protected WithEvents EmailField As System.Web.UI.WebControls.TextBox
        Protected WithEvents Contact1Field As System.Web.UI.WebControls.TextBox
        Protected WithEvents Contact2Field As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents pnlAudit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents CreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedDate As System.Web.UI.WebControls.Label

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
        ' and ItemId of the contact to edit.
        '
        ' It then uses the DotNetZoom.ContactsDB() data component
        ' to populate the page's edit controls with the contact details.
        '
        '****************************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Title1.DisplayHelp = "DisplayHelp_EditContacts"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Requiredfieldvalidator1.ErrorMessage =  "<br>" + GetLanguage("need_a_valide_name")
			cmdUpdate.Text = GetLanguage("enregistrer")
			cmdCancel.Text = GetLanguage("annuler")
			cmdDelete.Text = GetLanguage("delete")		
            ' Determine ItemId of Contacts to Update
            If IsNumeric(Request.Params("ItemID")) Then
                itemId = Int32.Parse(Request.Params("ItemID"))
            End If

            ' If the page is being requested the first time, determine if an
            ' contact itemId value is specified, and if so populate page
            ' contents with the contact details
            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If
                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm")) & "');")

                If itemId <> -1 Then
                    ' Obtain a single row of contact information
                    Dim contacts As New ContactsDB()
                    Dim dr As SqlDataReader = contacts.GetSingleContact(itemId, ModuleId)

                    ' Read first row from database
                    If dr.Read() Then
                        NameField.Text = CType(dr("Name"), String)
                        RoleField.Text = CType(dr("Role"), String)
                        EmailField.Text = CType(dr("Email"), String)
                        Contact1Field.Text = CType(dr("Contact1"), String)
                        Contact2Field.Text = CType(dr("Contact2"), String)
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
        ' create or update a contact.  It  uses the DotNetZoom.ContactsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            Page.Validate()

            ' Only Update if Entered data is Valid
            If Page.IsValid = True Then

                ' Create an instance of the ContactsDB component
                Dim contacts As New ContactsDB()

                If itemId = -1 Then
                    ' Add the contact within the contacts table
                    contacts.AddContact(ModuleId, Context.User.Identity.Name, NameField.Text, RoleField.Text, EmailField.Text, Contact1Field.Text, Contact2Field.Text)
                Else
                    ' Update the contact within the contacts table
                    contacts.UpdateContact(itemId, Context.User.Identity.Name, NameField.Text, RoleField.Text, EmailField.Text, Contact1Field.Text, Contact2Field.Text)
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
        ' a contact.  It  uses the DotNetZoom.ContactsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click

            If itemId <> -1 Then

                Dim contacts As New ContactsDB()
                contacts.DeleteContact(itemId)
				' Reset data cashe
				
				ClearModuleCache(ModuleId)
				
            End If

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

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub


    End Class

End Namespace