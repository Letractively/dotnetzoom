Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Namespace DotNetZoom

    Public Class index

       Inherits DotNetZoom.BasePage

		Private moduleId As Integer = -1
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

#End Region


 
      Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.init
            '
            ' CODEGEN: This call is required by the ASP.NET Web Form Designer.
            '
			
            InitializeComponent()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            'Try
            'try to catch error
            'Dim o As [Object] = Request.QueryString
            'Dim parameters As System.Collections.Specialized.NameValueCollection = Request.Params
            'Catch ex As Exception
            'LogMessage(Request, "Erreur in NameValueCollection, page_Init default.aspx")
            'End Try

            If Request.IsAuthenticated = False And Request.QueryString("showlogin") = "1" Then
                'Send back to Login Page
                Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "def=Login"), True)
            End If

            LinkClick()

            Dim CheckSSL As Boolean = False

            If Not Page.IsPostBack And PortalSecurity.IsSuperUser Then
                Session("LanguageTable") = Nothing
            End If

            Dim context As HttpContext = HttpContext.Current
            Dim strPageHTML As String
            Dim SkinFileName As String
            Dim TempKey As String

            ' if portal page not in memory get it
            If IsAdminTab() Or Not (Request.Params("edit") Is Nothing) Or Not (Request.Params("def") Is Nothing) Then
                TempKey = GetDBname() & "PAGESKINEDIT_" & CStr(_portalSettings.PortalId) & GetLanguage("N")
                strPageHTML = context.Cache(TempKey)
                If strPageHTML Is Nothing Then
                    '	Item not in cache, get it manually
                    SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/portaledit.skin")
                    If Not File.Exists(SkinFileName) Then
                        SkinFileName = Request.MapPath(glbPath + "hostedit.skin")
                    End If
                    strPageHTML = context.Cache(SkinFileName)
                    ' if page not in memory get it
                    If strPageHTML Is Nothing Then
                        Dim objStreamReader As StreamReader
                        objStreamReader = File.OpenText(SkinFileName)
                        strPageHTML = objStreamReader.ReadToEnd
                        objStreamReader.Close()
                        Dim dep As New System.Web.Caching.CacheDependency(SkinFileName)
                        Cache.Insert(SkinFileName, strPageHTML, dep)
                        ' end read edit page
                    End If
                    ' convert all tags to lowercase
                    strPageHTML = ConvertTAGlowercase(strPageHTML)
                    context.Cache.Insert(TempKey, strPageHTML, CDp(_portalSettings.PortalId), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.Normal, Nothing)
                End If
                ' end edit page
            Else
                ' get standard page
                TempKey = GetDBname() & "PAGESKIN_" & CStr(_portalSettings.ActiveTab.TabId) & GetLanguage("N")
                strPageHTML = context.Cache(TempKey)
                If strPageHTML Is Nothing Then
                    '	Item not in cache, get it manually    
                    SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/" & _portalSettings.ActiveTab.Skin)
                    If _portalSettings.ActiveTab.Skin = "" Or Not File.Exists(SkinFileName) Then
                        SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/portal.skin")
                        If Not File.Exists(SkinFileName) Then
                            SkinFileName = Request.MapPath(glbPath + "host.skin")
                        End If
                    End If
                    strPageHTML = context.Cache(SkinFileName)
                    '  page not in memory get it
                    If strPageHTML Is Nothing Then
                        Dim objStreamReader As StreamReader
                        objStreamReader = File.OpenText(SkinFileName)
                        strPageHTML = objStreamReader.ReadToEnd
                        objStreamReader.Close()
                        Dim dep As New System.Web.Caching.CacheDependency(SkinFileName)
                        Cache.Insert(SkinFileName, strPageHTML, dep)
                    End If
                    ' convert all tags to lowercase
                    strPageHTML = ConvertTAGlowercase(strPageHTML)
                    context.Cache.Insert(TempKey, strPageHTML, CDp(_portalSettings.PortalId), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.Normal, Nothing)
                End If

            End If

            Dim strTitle As String
            If PortalSettings.GetSiteSettings(_portalSettings.PortalId).ContainsKey(GetLanguage("N") & "_PortalName") Then
                strTitle = PortalSettings.GetSiteSettings(_portalSettings.PortalId)(GetLanguage("N") & "_PortalName")
            Else
                strTitle = _portalSettings.PortalName
            End If
            Dim tab As TabStripDetails
            For Each tab In _portalSettings.BreadCrumbs
                strTitle += " &gt; " & tab.TabName
            Next
            If PortalSettings.GetHostSettings.ContainsKey("DisablePageTitleVersion") = True Then
                If PortalSettings.GetHostSettings("DisablePageTitleVersion") = "N" Then
                    strTitle += " ( DNZ " & _portalSettings.Version & " " & GetLanguage("language") & ")"
                End If
            Else
                strTitle += " ( DNZ " & _portalSettings.Version & " " & GetLanguage("language") & ")"
            End If
            strPageHTML = strPageHTML.Insert(strPageHTML.IndexOf("</title>"), strTitle)
            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Then
                strPageHTML = Regex.Replace(strPageHTML, "{isadmin}", "", RegexOptions.IgnoreCase)
                strPageHTML = Regex.Replace(strPageHTML, "{/isadmin}", "", RegexOptions.IgnoreCase)
                ' info just pour admin
            Else
                strPageHTML = Regex.Replace(strPageHTML, "{isadmin}[^{}]+{/isadmin}", "", RegexOptions.IgnoreCase)
            End If

            strPageHTML = Regex.Replace(strPageHTML, "{datetime}", ProcessLanguage("{date}"), RegexOptions.IgnoreCase)


            If Request.Browser.Browser.ToUpper().IndexOf("IE") >= 0 Then
                ' put in behavior in body 
                ' behavior:url("/csshover.htc")
                If Request.Browser.MajorVersion() < 7 Then
                    strPageHTML = Regex.Replace(strPageHTML, "<body", "<body style=""behavior:url(/csshover.htc)"" ", RegexOptions.IgnoreCase)
                End If
            End If


            Dim form As New HtmlForm()
            form.ID = "Form1"
            Dim ContentPane As New PlaceHolder()
            ContentPane.ID = "contentpane"
            Dim leftPaneSpacer As New PlaceHolder()
            leftPaneSpacer.EnableViewState = False
            leftPaneSpacer.ID = "leftpanespacer"
            Dim rightPaneSpacer As New PlaceHolder()
            rightPaneSpacer.EnableViewState = False
            rightPaneSpacer.ID = "rightpanespacer"
            Dim leftPane As New PlaceHolder()
            leftPane.ID = "leftpane"
            Dim rightPane As New PlaceHolder()
            rightPane.ID = "rightpane"
            Dim TopPane As New PlaceHolder()
            TopPane.ID = "toppane"
            TopPane.Visible = False
            Dim BottomPane As New PlaceHolder()
            BottomPane.ID = "bottompane"
            BottomPane.Visible = False
            Dim leftline As New PlaceHolder()
            leftline.EnableViewState = False
            leftline.ID = "leftline"
            Dim rightline As New PlaceHolder()
            rightline.EnableViewState = False
            rightline.ID = "rightline"
            Dim CSSPane As New PlaceHolder()
            CSSPane.EnableViewState = False
            CSSPane.ID = "CSS"


            ' put in the portal.css
            strPageHTML = AddCSSControlToPage(CSSPane, strPageHTML)

            Dim objLink As System.Web.UI.LiteralControl
            objLink = New System.Web.UI.LiteralControl("PORTALCSS")
            objLink.EnableViewState = False
            objLink.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"
            CSSPane.Controls.Add(objLink)
            'See if a CSS file for this tab
            If IsAdminTab() Or Not Request.Params("edit") Is Nothing Or Not (Request.Params("def") Is Nothing) Then
                SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/portaledit.css")
                If File.Exists(SkinFileName) Then
                    objLink = New System.Web.UI.LiteralControl("EDITCSS")
                    objLink.EnableViewState = False
                    objLink.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portaledit.css""  type=""text/css"" rel=""stylesheet"">"
                    CSSPane.Controls.Add(objLink)
                End If
            Else
                If _portalSettings.ActiveTab.Css <> "" Then
                    objLink = New System.Web.UI.LiteralControl("TABCSS")
                    objLink.EnableViewState = False
                    objLink.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/" & _portalSettings.ActiveTab.Css & """" & " type=""text/css"" rel=""stylesheet"">"
                    CSSPane.Controls.Add(objLink)
                End If
            End If


            ' addform to page			
            strPageHTML = Addform(form, strPageHTML)
            ' add control to form

            Dim myValues As [String]() = {"{banner}", "{leftline}", "{leftspacer}", "{toppane}", "{leftpane}", "{contentpane}", "{rightspacer}", "{rightline}", "{rightpane}", "{bottompane}", "{footer}", "{tigramenu}", "{solpartmenu}", "{altmenu}", "{mailcheck}", "{tabmenu}"}
            Dim myKeys(15) As Integer

            Dim i As Integer
            For i = myKeys.GetLowerBound(0) To myKeys.GetUpperBound(0)
                myKeys.SetValue(strPageHTML.IndexOf(myValues(i)), i)
            Next i
            Array.Sort(myKeys, myValues)

            For i = myValues.GetLowerBound(0) To myValues.GetUpperBound(0)

                Select Case myValues(i)
                    Case "{banner}"
                        Dim BannerControl1 As Control = CType(LoadControl("~/controls/desktopportalbanner.ascx"), Control)
                        BannerControl1.ID = "banner"
                        strPageHTML = AddControlToPage(form, BannerControl1, strPageHTML, "{banner}")
                    Case "{leftspacer}"
                        strPageHTML = Addplaceholder(form, leftPaneSpacer, strPageHTML, "{leftspacer}")
                        If _portalSettings.ActiveTab.LeftPaneWidth <> "" Then
                            objLink = New System.Web.UI.LiteralControl("leftspacerliteral")
                            objLink.EnableViewState = False
                            objLink.Text = "<img  src=""" & glbPath & "images/1x1.gif"" alt=""-"" style=""border-width:0px;height:1px;width:"
                            objLink.Text = objLink.Text & _portalSettings.ActiveTab.LeftPaneWidth & "px;"" >"
                            leftPaneSpacer.Controls.Add(objLink)
                        End If
                    Case "{leftline}"
                        strPageHTML = Addplaceholder(form, leftline, strPageHTML, "{leftline}")
                        objLink = New System.Web.UI.LiteralControl("leftlineliteral")
                        objLink.EnableViewState = False
                        objLink.Text = "<td class=""line""></td>"
                        leftline.Controls.Add(objLink)
                    Case "{rightline}"
                        strPageHTML = Addplaceholder(form, rightline, strPageHTML, "{rightline}")
                        objLink = New System.Web.UI.LiteralControl("rightlineliteral")
                        objLink.EnableViewState = False
                        objLink.Text = "<td class=""line""></td>"
                        rightline.Controls.Add(objLink)
                    Case "{toppane}"
                        strPageHTML = Addplaceholder(form, TopPane, strPageHTML, "{toppane}")
                    Case "{leftpane}"
                        strPageHTML = Addplaceholder(form, leftPane, strPageHTML, "{leftpane}")
                    Case "{contentpane}"
                        strPageHTML = Addplaceholder(form, ContentPane, strPageHTML, "{contentpane}")
                    Case "{rightspacer}"
                        strPageHTML = Addplaceholder(form, rightPaneSpacer, strPageHTML, "{rightspacer}")
                        If _portalSettings.ActiveTab.RightPaneWidth <> "" Then
                            objLink = New System.Web.UI.LiteralControl("rightspacerliteral")
                            objLink.EnableViewState = False
                            objLink.Text = "<img  src=""" & glbPath & "images/1x1.gif"" alt=""-"" style=""border-width:0px;height:1px;width:"
                            objLink.Text = objLink.Text & _portalSettings.ActiveTab.RightPaneWidth & "px;"" >"
                            rightPaneSpacer.Controls.Add(objLink)
                        End If
                    Case "{rightpane}"
                        strPageHTML = Addplaceholder(form, rightPane, strPageHTML, "{rightpane}")
                    Case "{bottompane}"
                        strPageHTML = Addplaceholder(form, BottomPane, strPageHTML, "{bottompane}")
                    Case "{footer}"
                        Dim footerControl1 As Control = CType(LoadControl("~/controls/DesktopPortalFooter.ascx"), Control)
                        footerControl1.ID = "footer"
                        strPageHTML = AddControlToPage(form, footerControl1, strPageHTML, "{footer}")
                    Case "{tigramenu}"
                        Dim TigraControl1 As Control
                        If Request.Params("editmenu") Is Nothing Or Not PortalSecurity.IsInRole(_portalSettings.AdministratorRoleId.ToString) Then
                            TigraControl1 = CType(LoadControl("~/controls/tigramenu.ascx"), Control)
                        Else
                            TigraControl1 = CType(LoadControl("~/admin/tabs/menuedit.ascx"), Control)
                        End If
                        TigraControl1.ID = "tigra"
                        strPageHTML = AddControlToPage(form, TigraControl1, strPageHTML, "{tigramenu}")
                    Case "{solpartmenu}"
                        Dim SolpartControl1 As Control = CType(LoadControl("~/controls/solpart.ascx"), Control)
                        SolpartControl1.ID = "solpart"
                        strPageHTML = AddControlToPage(form, SolpartControl1, strPageHTML, "{solpartmenu}")
                    Case "{altmenu}"
                        Dim altmenuControl1 As Control = CType(LoadControl("~/controls/altmenu.ascx"), Control)
                        altmenuControl1.ID = "AltMenu"
                        strPageHTML = AddControlToPage(form, altmenuControl1, strPageHTML, "{altmenu}")
                    Case "{mailcheck}"
                        Dim MailCheckControl As Control = CType(LoadControl("~/controls/mailcheck.ascx"), Control)
                        MailCheckControl.ID = "MailCheck"
                        strPageHTML = AddControlToPage(form, MailCheckControl, strPageHTML, "{mailcheck}")
                    Case "{tabmenu}"
                        Dim TabMenuControl As Control = CType(LoadControl("~/controls/tabmenu.ascx"), Control)
                        TabMenuControl.ID = "tabmenu"
                        strPageHTML = AddControlToPage(form, TabMenuControl, strPageHTML, "{tabmenu}")

                End Select
            Next i

            form.Controls.Add(New LiteralControl(strPageHTML))

            leftPane.Visible = False
            rightPane.Visible = False
            leftline.Visible = False
            rightline.Visible = False

            ' if first time loading this page then reload to avoid caching
            If Request.Params("tabId") Is Nothing Then
                Response.Expires = -1
            End If

            ' get module container
            Dim objAdmin As New AdminDB()

            Dim strContainer As String
            If PortalSettings.GetSiteSettings(_portalSettings.PortalId)("container") <> "" Then
                strContainer = PortalSettings.GetSiteSettings(_portalSettings.PortalId)("container")
            Else
                strContainer = "[MODULE]"
            End If
            Dim EditContainer As String = "<div id=""EditTable"" align=""center""><table width=""750"" cellspacing=""0"" cellpadding=""0"" border=""0"" ><tr valign=""top""><td valign=""top"" align=""left"">[MODULE]</td></tr></table></div>"
            Dim AdminContainer As String = "<div id=""AdminTable"" align=""center""><table width=""750"" cellspacing=""0"" cellpadding=""0"" border=""0"" ><tr valign=""top""><td valign=""top"" align=""left"">[MODULE]</td></tr></table></div>"

            If PortalSettings.GetSiteSettings(_portalSettings.PortalId).ContainsKey("editcontainer") Then
                EditContainer = "<div id=""EditTable"">" & PortalSettings.GetSiteSettings(_portalSettings.PortalId)("editcontainer") & "</div>"
            End If
            If PortalSettings.GetSiteSettings(_portalSettings.PortalId).ContainsKey("admincontainer") Then
                AdminContainer = "<div id=""AdminTable"">" & PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainer") & "</div>"
            End If

            Dim _moduleSettings As New ModuleSettings()

            If Not Request.Params("edit") Is Nothing Or Not (Request.Params("def") Is Nothing) Then
                ' Determine ModuleId of Portal Module
                If IsNumeric(Request.Params("mid")) Then
                    moduleId = Int32.Parse(Request.Params("mid"))
                End If

                ' initialize security
                _moduleSettings.Secure = True
                _moduleSettings.IsAdminModule = False

                ' load module settings based on moduleid ( instance of desktopmodule )
                If moduleId <> -1 Then
                    _moduleSettings = PortalSettings.GetEditModuleSettings(moduleId)
                End If

                ' load module definition by name
                If Not (Request.Params("def") Is Nothing) Then
                    Dim DefFriendlyName As String = Request.Params("def")
                    DefFriendlyName = LCase(DefFriendlyName)
                    Dim dr As SqlDataReader = objAdmin.GetSingleModuleDefinitionByName(GetLanguage("N"), DefFriendlyName)
                    If dr.Read Then
                        If Boolean.Parse(dr("defedit").ToString) Then
                            CheckSSL = Boolean.Parse(dr("ssl").ToString) And _portalSettings.SSL
                            _moduleSettings.ModuleId = moduleId
                            _moduleSettings.ModuleDefId = Int32.Parse(dr("ModuleDefID").ToString)
                            _moduleSettings.TabId = _portalSettings.ActiveTab.TabId
                            _moduleSettings.PaneName = "Edit"
                            _moduleSettings.ModuleTitle = dr("ModuleTitle").ToString
                            _moduleSettings.IsAdminModule = IsAdminTab()
                            _moduleSettings.AuthorizedEditRoles = ""
                            _moduleSettings.EditSrc = dr("EditSrc").ToString
                            _moduleSettings.Secure = dr("Secure")
                            _moduleSettings.EditModuleIcon = dr("EditModuleIcon").ToString
                            _moduleSettings.ShowTitle = True
                            _moduleSettings.Personalize = 2
                            _moduleSettings.FriendlyName = dr("FriendlyName").ToString
                        End If
                    End If
                    dr.Close()



                Else
                    ' load module definition id
                    If IsAdminTab() Then
                        If IsNumeric(Request.Params("adminpage")) Then
                            Dim Intdefid As Integer = Int32.Parse(Request.QueryString("adminpage"))
                            Dim dr As SqlDataReader = objAdmin.GetAdminModuleDefinition(GetLanguage("N"), Intdefid)
                            If dr.Read Then
                                CheckSSL = Boolean.Parse(dr("ssl").ToString) And _portalSettings.SSL
                                _moduleSettings.ModuleId = 0
                                _moduleSettings.Secure = True
                                _moduleSettings.PaneName = "Edit"
                                _moduleSettings.TabId = _portalSettings.ActiveTab.TabId
                                _moduleSettings.DesktopSrc = dr("DesktopSrc")
                                _moduleSettings.ModuleTitle = dr("ModuleTitle")
                                _moduleSettings.IsAdminModule = True
                                _moduleSettings.AuthorizedEditRoles = ""
                                _moduleSettings.EditSrc = IIf(IsDBNull(dr("EditSrc")), "", dr("EditSrc"))
                                _moduleSettings.EditModuleIcon = dr("EditModuleIcon")
                                _moduleSettings.ShowTitle = True
                                _moduleSettings.Personalize = 2
                                _moduleSettings.FriendlyName = dr("FriendLyName")
                            End If
                            dr.Close()
                        End If
                    End If
                End If ' def or defid

                ' Verify that the current user has access to edit this module
                If _moduleSettings.Secure Then
                    If PortalSecurity.IsInRole(_portalSettings.AdministratorRoleId.ToString) = False And PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = False Then
                        If (moduleId = -1 Or PortalSecurity.HasEditPermissions(moduleId) = False) Then
                            EditDenied()
                        End If
                    End If
                End If

                ' load user control
                If _moduleSettings.EditSrc <> "" Then
                    Try
                        Dim objModule As PortalModuleControl = CType(Me.LoadModule(_moduleSettings.EditSrc), PortalModuleControl)
                        objModule.ModuleConfiguration = _moduleSettings
                        objModule.ID = "edit"
                        If IsAdminTab() Then
                            AddModule(ContentPane, objModule, AdminContainer, _portalSettings.UploadDirectory, IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerAlignment") <> "", _
                             PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerAlignment"), ""), _
                           IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerColor") <> "", _
                          PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerColor"), ""), _
                          IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerBorder") <> "", _
                           PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerBorder"), ""))
                        Else
                            AddModule(ContentPane, objModule, EditContainer, _portalSettings.UploadDirectory, IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("editcontainerAlignment") <> "", _
                            PortalSettings.GetSiteSettings(_portalSettings.PortalId)("editcontainerAlignment"), ""), _
                             IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("editcontainerColor") <> "", _
                            PortalSettings.GetSiteSettings(_portalSettings.PortalId)("editcontainerColor"), ""), _
                            IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("editcontainerBorder") <> "", _
                             PortalSettings.GetSiteSettings(_portalSettings.PortalId)("editcontainerBorder"), ""))
                        End If
                    Catch objException As Exception
                        ' error loading user control - the file may have been deleted or moved
                        If InStr(1, Request.Url.ToString.ToLower, "localhost") Then
                            Throw objException
                        Else
                            LogMessage(HttpContext.Current.Request, "Erreur LoadModule, " + _moduleSettings.EditSrc + " " + objException.Message)
                            ContentPane.Controls.Add(New LiteralControl("<span class=""NormalRed"">Error Loading " & _moduleSettings.EditSrc & "</span>"))
                        End If
                    End Try
                End If
                ' End Edit page
            Else

                ' See if AdminPage
                If IsAdminTab() Then
                    If IsNumeric(Request.Params("adminpage")) Then
                        Dim IntAdminPage As Integer = Int32.Parse(Request.QueryString("adminpage"))
                        Dim dr As SqlDataReader = objAdmin.GetAdminModuleDefinition(GetLanguage("N"), IntAdminPage)
                        If dr.Read Then
                            CheckSSL = Boolean.Parse(dr("ssl").ToString) And _portalSettings.SSL
                            _moduleSettings.ModuleId = 0
                            _moduleSettings.Secure = True
                            _moduleSettings.PaneName = "Edit"
                            _moduleSettings.TabId = _portalSettings.ActiveTab.TabId
                            _moduleSettings.DesktopSrc = dr("DesktopSrc")
                            _moduleSettings.ModuleTitle = dr("ModuleTitle")
                            _moduleSettings.IsAdminModule = True
                            _moduleSettings.AuthorizedEditRoles = ""
                            _moduleSettings.EditSrc = IIf(IsDBNull(dr("EditSrc")), "", dr("EditSrc"))
                            _moduleSettings.EditModuleIcon = dr("EditModuleIcon")
                            _moduleSettings.ShowTitle = True
                            _moduleSettings.Personalize = 2
                            _moduleSettings.FriendlyName = dr("FriendLyName")
                            Dim AdminobjModule As PortalModuleControl = CType(Me.LoadModule(_moduleSettings.DesktopSrc), PortalModuleControl)
                            AdminobjModule.ModuleConfiguration = _moduleSettings
                            AdminobjModule.ID = "admin"
                            AddModule(ContentPane, AdminobjModule, AdminContainer, _portalSettings.UploadDirectory, IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerAlignment") <> "", _
                              PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerAlignment"), ""), _
                               IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerColor") <> "", _
                              PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerColor"), ""), _
                              IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerBorder") <> "", _
                               PortalSettings.GetSiteSettings(_portalSettings.PortalId)("admincontainerBorder"), ""))
                        End If
                        dr.Close()
                    End If
                Else

                    ' Normal page
                    CheckSSL = _portalSettings.ActiveTab.ssl And _portalSettings.SSL
                    ' ensure that the user has access to the current page
                    If PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AuthorizedRoles) = False Then
                        Dim objModule As PortalModuleControl = CType(Me.LoadModule("~/Admin/Security/AccessDenied.ascx"), PortalModuleControl)
                        objModule.ID = "denied"
                        AddModule(ContentPane, objModule, strContainer, _portalSettings.UploadDirectory)
                    End If



                    ' check portal expiry date
                    If _portalSettings.ExpiryDate <> "" Then
                        If CDate(_portalSettings.ExpiryDate) < Now() Then
                            ' Dynamically inject an expiry message into the Content pane if the portal license has expired
                            Dim objModule As PortalModuleControl = CType(Me.LoadModule("~/Admin/Security/Expired.ascx"), PortalModuleControl)
                            objModule.ID = "expired"
                            AddModule(ContentPane, objModule, strContainer, _portalSettings.UploadDirectory, "center")
                        End If
                    End If


                    ' Dynamically Populate the Left, Center and Right pane sections of the portal page
                    If _portalSettings.ActiveTab.Modules.Count > 0 Then

                        ' Loop through each entry in the configuration system for this tab
                        Dim ModuleOrderLeft As Integer = 0
                        Dim ModuleOrderCenter As Integer = 0
                        Dim ModuleOrderRight As Integer = 0
                        Dim ModuleOrderTop As Integer = 0
                        Dim ModuleOrderBottom As Integer = 0
                        Dim Setting As Hashtable
                        For Each _moduleSettings In _portalSettings.ActiveTab.Modules
                            Setting = PortalSettings.GetModuleSettings(_moduleSettings.ModuleId)
                            Select Case _moduleSettings.PaneName
                                Case "ContentPane"
                                    ModuleOrderCenter = _moduleSettings.ModuleId
                                Case "LeftPane"
                                    ModuleOrderLeft = _moduleSettings.ModuleId
                                Case "RightPane"
                                    ModuleOrderRight = _moduleSettings.ModuleId
                                Case "TopPane"
                                    ModuleOrderTop = _moduleSettings.ModuleId
                                Case "BottomPane"
                                    ModuleOrderBottom = _moduleSettings.ModuleId
                            End Select


                            ' Check language here


                            If (_moduleSettings.Language = GetLanguage("N") Or _moduleSettings.Language = "") And PortalSecurity.IsInRoles(IIf(_moduleSettings.AuthorizedViewRoles <> "", _moduleSettings.AuthorizedViewRoles, _portalSettings.ActiveTab.AuthorizedRoles)) Then
                                Dim parent As Control = Page.FindControl(_moduleSettings.PaneName)
                                If Not parent Is Nothing Then
                                    ' If no caching is specified, create the user control instance and dynamically
                                    ' inject it into the page.  Otherwise, create a cached module instance that
                                    ' may or may not optionally inject the module into the tree
                                    Try
                                        ' need to do a check here for the panel admin
                                        If ((_moduleSettings.CacheTime > 0) And (Not PortalSecurity.IsInRoles(_moduleSettings.AuthorizedEditRoles))) Then
                                            ' Cash module
                                            Dim objModule As New CachedPortalModuleControl()
                                            objModule.ModuleConfiguration = _moduleSettings
                                            objModule.ID = "m" & _moduleSettings.ModuleId
                                            AddModule(parent, objModule, IIf(Setting("container") <> "", Setting("container"), strContainer), _portalSettings.UploadDirectory, IIf(Setting("containerAlignment") <> "", Setting("containerAlignment"), ""), IIf(Setting("containerColor") <> "", Setting("containerColor"), ""), IIf(Setting("containerBorder") <> "", Setting("containerBorder"), ""))
                                        Else
                                            Dim objModule As PortalModuleControl = CType(Me.LoadModule(_moduleSettings.DesktopSrc), PortalModuleControl)
                                            objModule.ModuleConfiguration = _moduleSettings
                                            objModule.ID = "m" & _moduleSettings.ModuleId
                                            AddModule(parent, objModule, IIf(Setting("container") <> "", Setting("container"), strContainer), _portalSettings.UploadDirectory, IIf(Setting("containerAlignment") <> "", Setting("containerAlignment"), ""), IIf(Setting("containerColor") <> "", Setting("containerColor"), ""), IIf(Setting("containerBorder") <> "", Setting("containerBorder"), ""))
                                        End If

                                    Catch objException As Exception

                                        ' error loading user control - the file may have been deleted or moved
                                        If InStr(1, Request.Url.ToString.ToLower, "localhost") Then
                                            Throw objException
                                        Else
                                            LogMessage(HttpContext.Current.Request, "Erreur LoadModule, " + _moduleSettings.DesktopSrc + " " + objException.Message)
                                            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Or PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True Then
                                                parent.Controls.Add(New LiteralControl("<span class=""NormalRed"">Error Loading " & _moduleSettings.DesktopSrc & "</span>"))
                                            End If
                                        End If
                                    End Try
                                End If
                            Else
                                ' does not have view role check to see if Classified 
                                If _moduleSettings.FriendlyName.ToLowerInvariant = "classified" Then
                                    If Not (Request.Params("cp") Is Nothing) And Not (Request.Params("cpxy") Is Nothing) And Not (Request.Params("ttzp") Is Nothing) Then
                                        If Request.Params("cp") = "3" Then
                                            ' Link from Classified E-Mail So sendhim the login link
                                            Session("URLTOAD") = Request.RawUrl
                                            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "def=Login"), True)
                                        End If
                                    End If
                                End If
                            End If

                        Next _moduleSettings
                        context.Items.Add("ModuleOrderLeft", ModuleOrderLeft)
                        context.Items.Add("ModuleOrderCenter", ModuleOrderCenter)
                        context.Items.Add("ModuleOrderRight", ModuleOrderRight)
                        If TopPane.Visible Then
                            context.Items.Add("ModuleOrderTop", ModuleOrderTop)
                        Else
                            context.Items.Add("ModuleOrderTop", -1)
                        End If
                        If BottomPane.Visible Then
                            context.Items.Add("ModuleOrderBottom", ModuleOrderBottom)
                        Else
                            context.Items.Add("ModuleOrderBottom", -1)
                        End If
                    End If
                    ' End Dynamically Populate the Left, Center and Right pane sections of the portal page
                End If
                ' End normal page	


            End If





            rightPaneSpacer.Visible = rightPane.Visible
            leftPaneSpacer.Visible = leftPane.Visible
            leftline.Visible = leftPane.Visible
            rightline.Visible = rightPane.Visible
            ' Warning message if required
            CheckSecureSSL(Page, CheckSSL)

        End Sub
		

 
		Private function Addform(ByVal objtoadd As HTMLForm, ByVal strtxtpage as string) As String
		Dim arrHTML As Array
		If strtxtpage.IndexOf("<form>") <> -1 then
		arrHTML = Split(strtxtpage, "<form>")
		Controls.Add(New LiteralControl(arrHTML(0)))
		Controls.Add(objtoadd)
		arrHTML = Split(arrHTML(1), "</form>")
		Controls.Add(New LiteralControl(arrHTML(1)))
		Return arrHTML(0)
		else
		Controls.Add(objtoadd)
		Return strtxtpage
		end if
		
		End function 

 
 		Private function AddControlToPage(ByVal FormToAdd As HTMLForm, ByVal objtoadd As Control, ByVal strtxtpage As String, ByVal StrToAdd As String) as String
            Dim arrContainer As Array
			If strtxtpage.IndexOf(StrToAdd) <> -1 then 
            arrContainer = Split(strtxtpage, StrToAdd)
            FormToAdd.Controls.Add(New LiteralControl(arrContainer(0)))
            FormToAdd.Controls.Add(objtoadd)
            objtoadd.Visible = True
			Return arrContainer(1)
			Else
			Return strtxtpage
			end if
 		End Function

		
 		Private function AddCSSControlToPage(ByVal objtoadd As Control, ByVal strtxtpage As String) as String
            Dim arrContainer As Array
			If strtxtpage.IndexOf("</head>") <> -1 then 
            arrContainer = Split(strtxtpage, "</head>")
            Controls.Add(New LiteralControl(arrContainer(0)))
            Controls.Add(objtoadd)
            objtoadd.Visible = True
			Return "</head>" & arrContainer(1)
			Else
			Return strtxtpage
			end if
 		End Function
	
		Private function ConvertTAGlowercase(ByVal strtxtpage As String) as String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim options As RegexOptions = RegexOptions.IgnoreCase
			'Put in language Meta
			strtxtpage = Regex.Replace(strtxtpage, "{languagecode}" , GetLanguage("N") , options)
            strtxtpage = Regex.Replace(strtxtpage, "<head>", "<head>", options)
            strtxtpage = Regex.Replace(strtxtpage, "</head>", "</head>", options)
            strtxtpage = Regex.Replace(strtxtpage, "<form[^>]*>", "<form>", options)
            strtxtpage = Regex.Replace(strtxtpage, "</form[^><]*>", "</form>", options)
            strtxtpage = Regex.Replace(strtxtpage, "<body", "<body", options)
            strtxtpage = Regex.Replace(strtxtpage, "</title>", "</title>", options)
			dim TempString as String = ""       	
            TempString = Replace(AssemblyCopyright, "YYYY", Year(Now).ToString)
			TempString = "<a class=""Normal"" href=""http://www.DotNetZoom.com"" style=""font-size:9px;"">" + TempString + "</a>"
			strtxtpage = Regex.Replace(strtxtpage, "{hypcopyright}", tempstring, options)
			TempString = _portalSettings.UploadDirectory & "favicon.ico"
			strtxtpage = Regex.Replace(strtxtpage, "{favicon}", tempstring, options)
            
			If portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey(GetLanguage("N") & "_Description") then
			TempString = portalSettings.GetSiteSettings(_portalSettings.PortalID)(GetLanguage("N") & "_Description")
			else
            TempString = _portalSettings.Description
			end if
			strtxtpage = Regex.Replace(strtxtpage, "{description}", tempstring, options)
			If portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey(GetLanguage("N") & "_KeyWords") then
			TempString = portalSettings.GetSiteSettings(_portalSettings.PortalID)(GetLanguage("N") & "_KeyWords") & IIf(_portalSettings.KeyWords <> "", ",", "") & "DotNetZoom"
			else
            tempstring = _portalSettings.KeyWords & IIf(_portalSettings.KeyWords <> "", ",", "") & "DotNetZoom"
			end if
			strtxtpage = Regex.Replace(strtxtpage, "{keyword}", tempstring, options)
			TempString = "Copyright (c) 2004-" & Year(Now).ToString & " by DotNetZoom ( " & AssemblyProduct & " )"
			strtxtpage = Regex.Replace(strtxtpage, "{copyright}", tempstring, options)
            TempString = "DotNetZoom " & _portalSettings.Version
			strtxtpage = Regex.Replace(strtxtpage, "{generator}", tempstring, options)
            If _portalSettings.BackgroundFile <> "" Then
                TempString = "<body background=""" & _portalSettings.UploadDirectory & _portalSettings.BackgroundFile & """"
                strtxtpage = Regex.Replace(strtxtpage, "<body", TempString, options)
            End If
            strtxtpage = Regex.Replace(strtxtpage, "{portaldir}", _portalSettings.UploadDirectory, options)
			Return strtxtpage
		end Function		
		
		
        Private function Addplaceholder(ByVal FormToAdd As HTMLForm, ByVal objtoadd As PlaceHolder, ByVal strtxtpage As String, ByVal StrToAdd As String) as String

            Dim arrContainer As Array
			If strtxtpage.IndexOf(StrToAdd) <> -1 then 
            arrContainer = Split(strtxtpage, StrToAdd)
            FormToAdd.Controls.Add(New LiteralControl(arrContainer(0)))
            FormToAdd.Controls.Add(objtoadd)
            objtoadd.Visible = True
			Return arrContainer(1)
			Else
			Return strtxtpage
			end if
        End function		
		
		
        Private Sub AddModule(ByVal objPane As placeholder, ByVal objModule As Control, ByVal strContainer As String, ByVal strUploadDirectory As String, Optional ByVal strAlignment As String = "", Optional ByVal strColor As String = "", Optional ByVal strBorder As String = "")

            Dim arrContainer As Array
			If strContainer.IndexOf("[MODULE]") = -1 then strContainer = "[MODULE]" 
            strContainer = Replace(strContainer, "[ALIGN]", IIf(strAlignment <> "", " align=""" & strAlignment & """", ""))
            strContainer = Replace(strContainer, "[COLOR]", IIf(strColor <> "", " bgcolor=""" & strColor & """", ""))
            strContainer = Replace(strContainer, "[BORDER]", IIf(strBorder <> "", " border=""" & strBorder & """", ""))
            strContainer = "<div id=""" & objModule.ID.ToString & """>" & strContainer & "</div>"
            arrContainer = Split(strContainer, "[MODULE]")
            objPane.Controls.Add(New LiteralControl(arrContainer(0)))
            objPane.Controls.Add(objModule)
            objPane.Controls.Add(New LiteralControl(arrContainer(1)))
            objPane.Visible = True

        End Sub

        Private Sub LinkClick()
            If Not HttpContext.Current.Items("linkclick") Is Nothing Then
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                If Not Request.Params("link") Is Nothing And Not Request.Params("table") Is Nothing And Not Request.Params("field") Is Nothing And Not Request.Params("id") Is Nothing Then
                    If IsNumeric(Request.Params("id")) And _
                    Request.Params("field") = "ItemID" And _
                    (Request.Params("table") = "Announcements" Or _
                      Request.Params("table") = "Documents" Or _
                      Request.Params("table") = "Links") Then
                        Dim strLink As String = Request.Params("link").ToString


                        Dim UserId As Integer = -1
                        If Request.IsAuthenticated Then
                            UserId = CType(Context.User.Identity.Name, Integer)
                        End If

                        ' update clicks
                        Dim objAdmin As New AdminDB()
                        objAdmin.UpdateClicks(Request.Params("table").ToString, Request.Params("field").ToString, Integer.Parse(Request.Params("id")), UserId)

                        ' format file link
                        If InStr(1, strLink, "/") = 0 Then
                            strLink = _portalSettings.UploadDirectory & strLink
                        End If


                        If Not Request.Params("contenttype") Is Nothing Then
                            ' verify file extension for request
                            Dim strExtension As String = Replace(System.IO.Path.GetExtension(Request.Params("link").ToString()), ".", "")
                            If InStr(1, "," & PortalSettings.GetHostSettings("FileExtensions").ToString.ToUpper, "," & strExtension.ToUpper) <> 0 Then
                                ' force download dialog
                                Response.Clear()

                                Response.AppendHeader("content-disposition", "attachment; filename=" + Request.Params("link").ToString)

                                Select Case strExtension.ToLower()
                                    Case "kmz"
                                        Response.ContentType = "Application/vnd.google-earth.kmz"
                                    Case "kml"
                                        Response.ContentType = "application/vnd.google-earth.kml+xml"
                                    Case "gpx"
                                        Response.ContentType = "application/gpx"
                                End Select
                                Response.WriteFile(strLink)
                                Response.End()
                            End If
                        Else ' redirect
                            Response.Redirect(strLink, True)
                        End If
                    End If

                ElseIf Not Request.QueryString("BannerClickId") Is Nothing Then
                    Dim strURL As String

                    Dim objVendor As New VendorsDB()

                    Dim dr As SqlDataReader = objVendor.GetBannerClickThrough(CInt(Request.QueryString("BannerClickId")))
                    If dr.Read() Then
                        If Not IsDBNull(dr("URL")) Then
                            strURL = AddHTTP(dr("URL").ToString)
                        Else
                            strURL = GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&VendorId=" & dr("VendorId").ToString & "&banner=1&def=Fournisseurs"
                        End If
                    Else
                        If Not Request.UrlReferrer Is Nothing Then
                            strURL = Request.UrlReferrer.ToString
                        Else
                            strURL = GetFullDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId
                        End If
                    End If
                    dr.Close()

                    Response.Redirect(strURL, True)
                ElseIf Not Request.QueryString("VendorId") Is Nothing Then
                    Dim strURL As String = ""

                    Dim objVendor As New VendorsDB()

                    Dim dr As SqlDataReader = objVendor.GetVendorClickThrough(CInt(Request.QueryString("VendorId")))
                    If dr.Read() Then
                        Select Case Request.QueryString("link")
                            Case "logo", "name", "url"
                                strURL = AddHTTP(dr("Website").ToString)
                            Case "map"
                                ' http://maps.google.com/maps?f=q&hl=fr&q=1280+Rue+St-Marc,+Montr%C3%A9al,+QC,+Canada&ie=UTF8&z=15&ll=45.493683,-73.580117&spn=0.01462,0.043259&om=1
                                strURL = BuildGoogleURL(dr)
                            Case "directions"
                                strURL = BuildGoogleURL(dr)
                                If Request.IsAuthenticated Then
                                    Dim objUser As New UsersDB()
                                    Dim drUser As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, Int32.Parse(Context.User.Identity.Name))
                                    If drUser.Read Then
                                        ' http://maps.google.com/maps?f=d&hl=fr&saddr=299+Rue+Marco,+Beauport,+QC,+Canada&daddr=6820+place+de+la+paix,+quebec&ie=UTF8&sll=37.0625,-95.677068&sspn=33.847644,59.765625&z=13&om=1		
                                        strURL = Replace(strURL, "f=q", "f=d")
                                        strURL = Replace(strURL, "&q=", "&daddr=")
                                        strURL += BuildDestinationURL(drUser)
                                    End If
                                    drUser.Close()
                                End If
                            Case "feedback"
                                strURL = GetFullDocument() & "?tabid=" & Request.Params("tabid") & "&mid=" & Request.Params("mid") & "&VendorId=" & Request.Params("VendorId") & "&search=" & Request.Params("search") & "&def=Vendor Feedback"
                        End Select
                    Else
                        strURL = Request.UrlReferrer.ToString
                    End If
                    dr.Close()

                    Response.Redirect(strURL, True)
                End If
            End If

        End Sub




        Private Function BuildDestinationURL(ByVal dr As SqlDataReader) As String
            ' http://maps.google.com/maps?f=q&hl=fr&q=1280+Rue+St-Marc,+Montr%C3%A9al,+QC,+Canada&ie=UTF8&z=15&ll=45.493683,-73.580117&spn=0.01462,0.043259&om=1

            Dim strURL As String
            Dim GotOne As Boolean = False
            strURL = "&saddr="
            If dr("Street").ToString <> "" Then
                strURL += System.Web.HttpUtility.UrlEncode(dr("Street").ToString)
                GotOne = True
            End If
            If dr("City").ToString <> "" Then
                If GotOne Then strURL += ",+"
                strURL += System.Web.HttpUtility.UrlEncode(dr("City").ToString)
                GotOne = True
            End If
            If dr("Region").ToString <> GetLanguage("list_none") And dr("Region").ToString <> "" Then
                If GotOne Then strURL += "+"
                strURL += System.Web.HttpUtility.UrlEncode(dr("Region").ToString)
                GotOne = True
            End If
            If dr("Country").ToString <> GetLanguage("list_none") And dr("Country").ToString <> "" Then
                If GotOne Then strURL += ",+"
                strURL += System.Web.HttpUtility.UrlEncode(dr("Country").ToString)
                GotOne = True
            End If
            If dr("PostalCode").ToString <> "" Then
                If GotOne Then strURL += ",+"
                strURL += System.Web.HttpUtility.UrlEncode(dr("PostalCode").ToString)
                GotOne = True
            End If
            BuildDestinationURL = strURL
        End Function

        Private Function BuildGoogleURL(ByVal dr As SqlDataReader) As String
            ' http://maps.google.com/maps?f=q&hl=fr&q=1280+Rue+St-Marc,+Montr%C3%A9al,+QC,+Canada&ie=UTF8&z=15&ll=45.493683,-73.580117&spn=0.01462,0.043259&om=1

            Dim strURL As String
            Dim GotOne As Boolean = False
            strURL = "http://maps.google.com/maps?f=q&hl=" + Getlanguage("N") + "&q="
            If dr("Street").ToString <> "" Then
                strURL += System.Web.HttpUtility.UrlEncode(dr("Street").ToString)
                GotOne = True
            End If
            If dr("City").ToString <> "" Then
                If GotOne Then strURL += ",+"
                strURL += System.Web.HttpUtility.UrlEncode(dr("City").ToString)
                GotOne = True
            End If
            If dr("Region").ToString <> GetLanguage("list_none") And dr("Region").ToString <> "" Then
                If GotOne Then strURL += "+"
                strURL += System.Web.HttpUtility.UrlEncode(dr("Region").ToString)
                GotOne = True
            End If
            If dr("Country").ToString <> GetLanguage("list_none") And dr("Country").ToString <> "" Then
                If GotOne Then strURL += ",+"
                strURL += System.Web.HttpUtility.UrlEncode(dr("Country").ToString)
                GotOne = True
            End If
            If dr("PostalCode").ToString <> "" Then
                If GotOne Then strURL += ",+"
                strURL += System.Web.HttpUtility.UrlEncode(dr("PostalCode").ToString)
                GotOne = True
            End If
            BuildGoogleURL = strURL & "&ie=UTF8&z=15"
        End Function



        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Page.IsPostBack = False Then
                Session.Contents.Remove("FCKeditor:UserFilesPath")
            End If
            HttpContext.Current.RewritePath(HttpContext.Current.Items("RequestDocument"), String.Empty, HttpContext.Current.Request.QueryString.ToString())

        End Sub

    End Class

End Namespace