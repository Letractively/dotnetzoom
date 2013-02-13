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
        Protected WithEvents cmdUpdate1 As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel1 As System.Web.UI.WebControls.LinkButton


        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents pnlAudit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents CreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedDate As System.Web.UI.WebControls.Label
        Protected WithEvents AdminPanel As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents MainPanel As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents chkCapcha As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents chkname As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkrole As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkemail As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkphone1 As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkphone2 As System.Web.UI.WebControls.CheckBox

        Protected WithEvents txtname As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtrole As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtemail As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtphone1 As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtphone2 As System.Web.UI.WebControls.TextBox


        Protected WithEvents tr0 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents tr1 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents tr2 As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents tr3 As System.Web.UI.HtmlControls.HtmlTableRow


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
            Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)

            If Request.Params("options") <> "" Then
                Title1.DisplayHelp = "DisplayHelp_AdminContacts"
                AdminPanel.Visible = True
                MainPanel.Visible = False
                chkCapcha.ToolTip = GetLanguage("Contact_CapchaTip")
                If Page.IsPostBack = False Then

                    Dim Capcha As String = "1"
                    If settings.ContainsKey("DisplayEmail") Then
                        Capcha = settings("DisplayEmail").ToString.ToLower
                    End If



                    Dim item1 As New ListItem()
                    item1.Text = GetLanguage("Contact_Capcha1")
                    item1.Value = "1"

                    If item1.Value = Capcha Then
                        item1.Selected = True
                    End If

                    chkCapcha.Items.Add(item1)

                    Dim item2 As New ListItem()
                    item2.Text = GetLanguage("Contact_Capcha2")
                    item2.Value = "2"

                    If item2.Value = Capcha Then
                        item2.Selected = True
                    End If

                    chkCapcha.Items.Add(item2)

                    If settings.ContainsKey("Name") Then
                        txtname.Text = settings("Name").ToString
                    Else
                        txtname.Text = GetLanguage("Name")
                    End If
                    If settings.ContainsKey("contact_title") Then
                        txtrole.Text = settings("contact_title").ToString
                    Else
                        txtrole.Text = GetLanguage("contact_title")
                    End If
                    If settings.ContainsKey("title_visible") Then
                        chkrole.Checked = CType(settings("title_visible").ToString, Boolean)
                    Else
                        chkrole.Checked = True
                    End If

                    If settings.ContainsKey("contact_email") Then
                        txtemail.Text = settings("contact_email").ToString
                    Else
                        txtemail.Text = GetLanguage("contact_email")
                    End If
                    If settings.ContainsKey("Email_visible") Then
                        chkemail.Checked = CType(settings("Email_visible").ToString, Boolean)
                    Else
                        chkemail.Checked = True
                    End If

                    If settings.ContainsKey("contact_telephone1") Then
                        txtphone1.Text = settings("contact_telephone1").ToString
                    Else
                        txtphone1.Text = GetLanguage("contact_telephone")
                    End If
                    If settings.ContainsKey("tel1_visible") Then
                        chkphone1.Checked = CType(settings("tel1_visible").ToString, Boolean)
                    Else
                        chkphone1.Checked = True
                    End If

                    If settings.ContainsKey("contact_telephone2") Then
                        txtphone2.Text = settings("contact_telephone2").ToString
                    Else
                        txtphone2.Text = GetLanguage("contact_telephone")
                    End If
                    If settings.ContainsKey("tel2_visible") Then
                        chkphone2.Checked = CType(settings("tel2_visible").ToString, Boolean)
                    Else
                        chkphone2.Checked = True
                    End If
                End If
            Else
                Title1.DisplayHelp = "DisplayHelp_EditContacts"
                AdminPanel.Visible = False
                MainPanel.Visible = True
                If settings.ContainsKey("title_visible") Then
                    tr0.Visible = CType(settings("title_visible").ToString, Boolean)
                End If

                If settings.ContainsKey("Email_visible") Then
                    tr1.Visible = CType(settings("Email_visible").ToString, Boolean)
                End If

                If settings.ContainsKey("tel1_visible") Then
                    tr2.Visible = CType(settings("tel1_visible").ToString, Boolean)
                End If

                If settings.ContainsKey("tel2_visible") Then
                    tr3.Visible = CType(settings("tel2_visible").ToString, Boolean)
                End If

            End If


            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Requiredfieldvalidator1.ErrorMessage = "<br>" + GetLanguage("need_a_valide_name")
            cmdUpdate.Text = GetLanguage("enregistrer")
            cmdCancel.Text = GetLanguage("annuler")
            cmdUpdate1.Text = GetLanguage("enregistrer")
            cmdCancel1.Text = GetLanguage("annuler")

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


        Public Function GetName(ByVal Setting As String) As String
            Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)
            If settings.ContainsKey(Setting) Then
                Return settings(Setting).ToString
            Else
                Return GetLanguage(Setting)
            End If
        End Function



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

        Private Sub cmdUpdate1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate1.Click
            Dim ObjAdmin As New AdminDB
            ObjAdmin.UpdateModuleSetting(ModuleId, "DisplayEmail", chkCapcha.SelectedValue)

            ObjAdmin.UpdateModuleSetting(ModuleId, "Name", txtname.Text)
            ObjAdmin.UpdateModuleSetting(ModuleId, "contact_title", txtrole.Text)
            ObjAdmin.UpdateModuleSetting(ModuleId, "title_visible", chkrole.Checked.ToString)

            ObjAdmin.UpdateModuleSetting(ModuleId, "contact_email", txtemail.Text)
            ObjAdmin.UpdateModuleSetting(ModuleId, "Email_visible", chkemail.Checked.ToString)

            ObjAdmin.UpdateModuleSetting(ModuleId, "contact_telephone1", txtphone1.Text)
            ObjAdmin.UpdateModuleSetting(ModuleId, "tel1_visible", chkphone1.Checked.ToString)

            ObjAdmin.UpdateModuleSetting(ModuleId, "contact_telephone2", txtphone2.Text)
            ObjAdmin.UpdateModuleSetting(ModuleId, "tel2_visible", chkphone2.Checked.ToString)


            ClearModuleCache(ModuleId)
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)

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

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click, cmdCancel1.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub


    End Class

End Namespace