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

    Public MustInherit Class Contacts
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal

        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents grdContacts As System.Web.UI.WebControls.DataGrid
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
        ' The Page_Load event handler on this User Control is used to
        ' obtain a DataReader of contact information from the Contacts
        ' table, and then databind the results to a DataGrid
        ' server control.  It uses the DotNetZoom.ContactsDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************'

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"
            Title1.DisplayOptions = True

			
			
			' Check to see if available in Cache
			Dim TempKey as String = GetDBname & "ModuleID_" & CStr(ModuleId)
			Dim context As HttpContext = HttpContext.Current
			Dim content as system.data.DataTable = Context.Cache(TempKey)
            If content Is Nothing Then
			'	Item not in cache, get it manually    
            Dim objContacts As New ContactsDB()
			Dim objAdmin As New AdminDB()
			content = AdminDB.ConvertDataReaderToDataTable(objContacts.GetContacts(ModuleId))
            Context.Cache.Insert(TempKey, content, CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, ModuleID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
            End If

            Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)
            If settings.ContainsKey("Name") Then
                grdContacts.Columns(1).HeaderText = settings("Name").ToString
            Else
                grdContacts.Columns(1).HeaderText = GetLanguage("Name")
            End If
            If settings.ContainsKey("contact_title") Then
                grdContacts.Columns(2).HeaderText = settings("contact_title").ToString
            Else
                grdContacts.Columns(2).HeaderText = GetLanguage("contact_title")
            End If
            If settings.ContainsKey("title_visible") Then
                grdContacts.Columns(2).Visible = CType(settings("title_visible").ToString, Boolean)
            End If

            If settings.ContainsKey("contact_email") Then
                grdContacts.Columns(3).HeaderText = settings("contact_email").ToString
            Else
                grdContacts.Columns(3).HeaderText = GetLanguage("contact_email")
            End If
            If settings.ContainsKey("Email_visible") Then
                grdContacts.Columns(3).Visible = CType(settings("Email_visible").ToString, Boolean)
            End If

            If settings.ContainsKey("contact_telephone1") Then
                grdContacts.Columns(4).HeaderText = settings("contact_telephone1").ToString
            Else
                grdContacts.Columns(4).HeaderText = GetLanguage("contact_telephone")
            End If
            If settings.ContainsKey("tel1_visible") Then
                grdContacts.Columns(4).Visible = CType(settings("tel1_visible").ToString, Boolean)
            End If

            If settings.ContainsKey("contact_telephone2") Then
                grdContacts.Columns(5).HeaderText = settings("contact_telephone2").ToString
            Else
                grdContacts.Columns(5).HeaderText = GetLanguage("contact_telephone")
            End If
            If settings.ContainsKey("tel2_visible") Then
                grdContacts.Columns(5).Visible = CType(settings("tel2_visible").ToString, Boolean)
            End If

			grdContacts.DataSource = content
			grdContacts.DataBind()
			
			
        End Sub

        Public Function DisplayEmail(ByVal Email As Object, ByVal Name As Object) As String
            Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)

            If settings.ContainsKey("DisplayEmail") Then
                Select Case settings("DisplayEmail").ToString.ToLower
                    Case "1"
                        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                        Dim objSecurity As New PortalSecurity()
                        Dim crypto As String = Server.UrlEncode(objSecurity.Encrypt(Application("cryptokey"), Email))
                        'option 1 send e-mail with feedback module
                        DisplayEmail = "<a class=""Head"" title=""" + Replace(GetLanguage("clicktosend"), "{name}", Name) + """ href=""" + FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "def=feedback&sendto=" + crypto) + """>Courriel</a>"
                    Case Else
                        DisplayEmail = FormatEmail(Email, Page)
                End Select
            Else
                DisplayEmail = FormatEmail(Email, Page)
            End If

        End Function

        Public Sub grdContacts_ItemCreated(sender As Object, e As DataGridItemEventArgs) handles grdContacts.ItemCreated 
            If e.Item.ItemType = ListItemType.Header then
                 e.Item.Cells(1).Attributes.Add("Scope","col")
                 e.Item.Cells(2).Attributes.Add("Scope","col")
                 e.Item.Cells(3).Attributes.Add("Scope","col")
                 e.Item.Cells(4).Attributes.Add("Scope","col")
                 e.Item.Cells(5).Attributes.Add("Scope","col")
            End If
        End Sub

    End Class

End Namespace