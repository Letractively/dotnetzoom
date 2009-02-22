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

    Public MustInherit Class Announcements

 		Inherits DotNetZoom.PortalModuleControl
		
		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected WithEvents lstAnnouncements As System.Web.UI.WebControls.DataList
		Protected WithEvents rssLink As System.Web.UI.WebControls.HyperLink
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
            ' Obtain announcement information from Announcements table
            ' and bind to the datalist control
			' Check to see if available in Cache
			
            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"
			Dim objAdmin As New AdminDB()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			
			
			
			
			
			Dim TKey as String = GetDBname & "ModuleID_" & CStr(ModuleId)
			Dim context As HttpContext = HttpContext.Current
			Dim content as system.data.DataTable 
			Content = CType(Context.Cache(TKey), system.data.DataTable)
            If content Is Nothing Then
			'	Item not in cache, get it manually    
			Dim announcements As New AnnouncementsDB()
			content = AdminDB.ConvertDataReaderToDataTable(announcements.GetAnnouncements(ModuleID))
            Context.Cache.Insert(TKey, content, CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, ModuleID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
  			End If
			lstAnnouncements.DataSource = content
            lstAnnouncements.DataBind()
			lstAnnouncements.ID = ""

	
            If File.Exists(Request.MapPath(_portalSettings.UploadDirectory) & objAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml") Then
                rssLink.NavigateUrl = _portalSettings.UploadDirectory & objAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml"
                rssLink.Visible = True
                rssLink.ToolTip = GetLanguage("channel_syndicate") & " " & File.GetLastWriteTime(Request.MapPath(_portalSettings.UploadDirectory) & objAdmin.convertstringtounicode(ModuleConfiguration.ModuleTitle) & ".xml").ToString()
            End If

			
        End Sub

        Public Function FormatURL(ByVal Link As String, ByVal ID As Integer) As String

            If InStr(1, Link, "://") = 0 Then
                If IsNumeric(Link) Then ' internal tab link
                    Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                    Link = _portalSettings.HTTP & "/" & GetLanguage("N") & ".default.aspx?tabid=" & Link
                End If
            End If
            Dim objSecurity As New PortalSecurity()
            Dim crypto As String = "tabid=" & TabId & "&table=Announcements&field=ItemID&id=" & ID.ToString & "&link=" & Server.UrlEncode(Link)
            Return glbPath & GetLanguage("N") & ".default.aspx?linkclick=" + Server.UrlEncode(objSecurity.Encrypt(Application("cryptokey"), crypto))

        End Function

        Public Function FormatDate(ByVal objDate As Date) As String

            Return FormatAnsiDate(objDate.ToString("yyyy-MM-dd"))

        End Function

    End Class

End Namespace