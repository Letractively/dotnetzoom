'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)
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

    Public MustInherit Class Document
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal

		Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents D As System.Web.UI.WebControls.DataGrid
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
		Protected WithEvents rssLink As System.Web.UI.WebControls.HyperLink

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
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			

            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"

			Dim objAdmin As New AdminDB()
			' Check to see if available in Cache
			Dim TempKey as String = GetDBname & "ModuleID_" & CStr(ModuleId)
			Dim context As HttpContext = HttpContext.Current
			Dim content as system.data.DataTable = Context.Cache(TempKey)
            If content Is Nothing Then
			'	Item not in cache, get it manually    
            Dim documents As New DocumentDB()

			content = AdminDB.ConvertDataReaderToDataTable(documents.GetDocuments(ModuleId, PortalId))
            
			Context.Cache.Insert(TempKey, content, CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, ModuleID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
  			End If
			D.Columns(1).HeaderText = GetLanguage("header_title")
			D.Columns(2).HeaderText = GetLanguage("header_who")
			D.Columns(3).HeaderText = GetLanguage("header_what")
			D.Columns(4).HeaderText = GetLanguage("header_when")
			D.Columns(5).HeaderText = GetLanguage("header_size")

			D.DataSource = content
            D.DataBind()
	
            if File.Exists(Request.MapPath(_portalSettings.UploadDirectory) & objAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml")
                rssLink.NavigateUrl = _portalSettings.UploadDirectory & objAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml"
			rssLink.visible = True
			rssLink.ToolTip = GetLanguage("channel_syndicate") & " " & File.getlastwritetime(Request.MapPath(_portalSettings.UploadDirectory) & objAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml").ToString()
			end if
			

        End Sub


        Function FormatURL(ByVal Link As String, ByVal ID As Integer) As String

            Dim objSecurity As New PortalSecurity()
            Dim crypto As String = "tabid=" & TabId & "&table=Documents&field=ItemID&id=" & ID.ToString & "&link=" & Server.UrlEncode(Link)
            Return glbPath & GetLanguage("N") & ".default.aspx?linkclick=" + Server.UrlEncode(objSecurity.Encrypt(Application("cryptokey"), crypto))


            ' Return glbPath() & "admin/Portal/LinkClick.aspx?tabid=" & TabId & "&table=Documents&field=ItemID&id=" & ID.ToString & "&link=" & Server.UrlEncode(Link)

        End Function


        Function FormatSize(ByVal Size As Object) As String

            If Not IsDBNull(Size) Then
                FormatSize = Format(Size / 1000, "#,##0.00")
            Else
                FormatSize = "Null"
            End If

        End Function

        Public Sub D_ItemCreated(sender As Object, e As DataGridItemEventArgs) handles D.ItemCreated 
            If e.Item.ItemType = ListItemType.Header then
                 e.Item.Cells(1).Attributes.Add("Scope","col")
                 e.Item.Cells(2).Attributes.Add("Scope","col")
                 e.Item.Cells(3).Attributes.Add("Scope","col")
                 e.Item.Cells(4).Attributes.Add("Scope","col")
                 e.Item.Cells(5).Attributes.Add("Scope","col")
            End If
        End Sub

        Public Sub D_Edit(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles D.EditCommand

            Dim ItemId As Integer = Integer.Parse(D.DataKeys(e.Item.ItemIndex).ToString())

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objDocuments As New DocumentDB()
            Dim objAdmin As New AdminDB()

            Dim strLink As String = ""

            Dim dr As SqlDataReader = objDocuments.GetSingleDocument(ItemId, ModuleId)
            If dr.Read() Then

                Dim objSecurity As New PortalSecurity()
                Dim crypto As String = "tabid=" & TabId & "&table=Documents&field=ItemID&id=" & ItemId.ToString & "&link=" & Server.UrlEncode(dr("URL").ToString)
                strLink += glbPath & GetLanguage("N") & ".default.aspx?linkclick=" + Server.UrlEncode(objSecurity.Encrypt(Application("cryptokey"), crypto))
                ' strLink += glbPath() & "admin/Portal/LinkClick.aspx?tabid=" & TabId & "&table=Documents&field=ItemID&id=" & ItemId.ToString & "&link=" & Server.UrlEncode(dr("URL").ToString)
            End If
            dr.Close()

            If strLink <> "" Then
                Response.Redirect(strLink, True)
            End If
        End Sub

    End Class

End Namespace