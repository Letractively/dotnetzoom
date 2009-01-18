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
            Dim CheckSSL As Boolean = False

            If Not Page.IsPostBack And PortalSecurity.IsSuperUser Then
                Session("LanguageTable") = Nothing
            End If
			' Add a event Handler for 'Init'.
            ' redirect to a specific tab based on name
            If Request.Params("tabname") <> "" Then
                Dim strURL As String = ""

                Dim admin As New AdminDB()
                Dim result As SqlDataReader = admin.GetTabByName(Request.Params("TabName"), CType(HttpContext.Current.Items("PortalSettings"), PortalSettings).PortalId)
                If result.Read() Then
                    strURL = GetFullDocument() & "?tabid=" & result("TabId")
                End If
                result.Close()

                If strURL <> "" Then
                    Dim intParam As Integer
                    For intParam = 0 To Request.QueryString.Count - 1
                        Select Case Request.QueryString.Keys(intParam).ToLower()
                            Case "tabid", "tabname"
                            Case Else
                                strURL += "&" + Request.QueryString.Keys(intParam) + "=" + Request.QueryString(intParam)
                        End Select
                    Next

                    Response.Redirect(strURL, True)

                End If
            End If
             ' Obtain PortalSettings from Current Context
			Dim context As HttpContext = HttpContext.Current
			Dim strPageHTML As String
			Dim SkinFileName As String
			Dim TempKey as String
				' if portal page not in memory get it
				If IsAdminTab() or Not (Request.Params("edit") Is Nothing) then
					TempKey = GetDBname & "PAGESKINEDIT_" & CStr(_portalSettings.PortalID) & GetLanguage("N")
					strPageHTML = Context.Cache(TempKey)
            			If strPageHTML Is Nothing Then
						'	Item not in cache, get it manually
						SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/portaledit.skin")
							If Not File.Exists(SkinFileName) then
							SkinFileName = Request.MapPath("hostedit.skin")
							end if
						strPageHTML = Context.Cache(SkinFileName)
							' if page not in memory get it
							If strPageHTML Is Nothing Then
							Dim objStreamReader As StreamReader
            				objStreamReader = File.OpenText(SkinFileName)
            				strPageHTML = objStreamReader.ReadToEnd
            				objStreamReader.Close()
							Dim dep As New System.Web.Caching.CacheDependency(SkinFileName)
	    					Cache.Insert(SkinFileName, strPageHTML, dep)
							' end read edit page
							end if
						' convert all tags to lowercase
						strPageHTML = ConvertTAGlowercase(strPageHTML)
						Context.Cache.Insert(TempKey, strPageHTML, CDp(_portalSettings.PortalID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
						end if			
						' end edit page
				else
					' get standard page
					TempKey = GetDBname & "PAGESKIN_" & CStr(_portalSettings.ActiveTab.TabID) & GetLanguage("N")
					strPageHTML = Context.Cache(TempKey)
        			    If strPageHTML Is Nothing Then
						'	Item not in cache, get it manually    
						SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/" & _portalSettings.ActiveTab.skin)
							If _portalSettings.ActiveTab.skin = "" or Not File.Exists(SkinFileName) then
							SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/portal.skin")
								If Not File.Exists(SkinFileName) then
								SkinFileName = Request.MapPath("host.skin")
								end if
							end if
						strPageHTML = Context.Cache(SkinFileName)
							'  page not in memory get it
							If strPageHTML Is Nothing Then
							Dim objStreamReader As StreamReader
            				objStreamReader = File.OpenText(SkinFileName)
            				strPageHTML = objStreamReader.ReadToEnd
            				objStreamReader.Close()
							Dim dep As New System.Web.Caching.CacheDependency(SkinFileName)
	      					Cache.Insert(SkinFileName, strPageHTML, dep)
							end if
						' convert all tags to lowercase
						strPageHTML = ConvertTAGlowercase(strPageHTML)
						Context.Cache.Insert(TempKey, strPageHTML, CDp(_portalSettings.PortalID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
					end if

			end if
            
			Dim strTitle As String 
			if portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey(GetLanguage("N") & "_PortalName") then
			strTitle = portalSettings.GetSiteSettings(_portalSettings.PortalID)(GetLanguage("N") & "_PortalName")
			else		
            strTitle = _portalSettings.PortalName
			end if
            Dim tab As TabStripDetails
            For Each tab In _portalSettings.BreadCrumbs
                strTitle += " &gt; " & tab.TabName
            Next
            If portalSettings.GetHostSettings.ContainsKey("DisablePageTitleVersion") = True Then
                If portalSettings.GetHostSettings("DisablePageTitleVersion") = "N" Then
                   strTitle += " ( DNZ " & _portalSettings.Version & " " & GetLanguage("language")& ")"
                End If
            Else
                 strTitle += " ( DNZ " & _portalSettings.Version & " " & GetLanguage("language")& ")"
            End If
    		strPageHTML = strPageHTML.Insert(strPageHTML.IndexOf("</title>") , strTitle)
			If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = true then
			strPageHTML = Regex.Replace(strPageHTML, "{isadmin}" , "", RegexOptions.IgnoreCase)
			strPageHTML = Regex.Replace(strPageHTML, "{/isadmin}" , "", RegexOptions.IgnoreCase)
			' info just pour admin
			else
			strPageHTML = Regex.Replace(strPageHTML, "{isadmin}[^{}]+{/isadmin}" , "", RegexOptions.IgnoreCase)
			end if

			strPageHTML = Regex.Replace(strPageHTML, "{datetime}" , ProcessLanguage("{date}"), RegexOptions.IgnoreCase)


			If Request.Browser.Browser.ToUpper().IndexOf("IE") >= 0 Then
			' put in behavior in body 
			' behavior:url("/csshover.htc")
			if Request.Browser.MajorVersion() < 7 then
            strPageHTML = Regex.Replace(strPageHTML, "<body", "<body style=""behavior:url(/csshover.htc)"" " , RegexOptions.IgnoreCase)
			end if
			End If
			

			Dim form As New HtmlForm()
			form.id = "Form1"
			Dim ContentPane as new Placeholder()
			contentpane.id = "contentpane"
            Dim leftPaneSpacer As New PlaceHolder()
            leftPaneSpacer.EnableViewState = False
			leftpaneSpacer.id = "leftpanespacer"
            Dim rightPaneSpacer As New PlaceHolder()
            rightPaneSpacer.EnableViewState = False
			rightpaneSpacer.id = "rightpanespacer"
			Dim leftPane as new Placeholder()
			leftpane.id = "leftpane"
			Dim rightPane as new Placeholder()
            rightPane.ID = "rightpane"
            Dim TopPane As New PlaceHolder()
            TopPane.ID = "toppane"
            TopPane.Visible = False
            Dim BottomPane As New PlaceHolder()
            BottomPane.ID = "bottompane"
            BottomPane.Visible = False
            Dim leftline As New PlaceHolder()
            leftline.EnableViewState = False
			leftline.id = "leftline"
            Dim rightline As New PlaceHolder()
            rightline.EnableViewState = False
			rightline.id = "rightline"
            Dim CSSPane As New PlaceHolder()
            CSSPane.EnableViewState = False
			csspane.id = "CSS"
			
			
           ' put in the portal.css
			strPageHTML = AddCSSControlToPage( csspane, strPageHTML )
			
            Dim objLink As System.Web.UI.LiteralControl
            objLink = New System.Web.UI.LiteralControl("PORTALCSS")
            objLink.EnableViewState = False
			objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"
			CSSPane.Controls.Add(objLink)
		   'See if a CSS file for this tab
		    If IsAdminTab() or  Request.Params("edit") <> "" then
			SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/portaledit.css")
			If File.Exists(SkinFileName) then
                    objLink = New System.Web.UI.LiteralControl("EDITCSS")
                    objLink.EnableViewState = False
			objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portaledit.css""  type=""text/css"" rel=""stylesheet"">"
			CSSPane.Controls.Add(objLink)
			end if
			else
			If _portalSettings.ActiveTab.css <> "" then
			objLink = New System.Web.UI.LiteralControl("TABCSS")
                    objLink.EnableViewState = False
                    objLink.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/" & _portalSettings.ActiveTab.Css & """" & " type=""text/css"" rel=""stylesheet"">"
			CSSPane.Controls.Add(objLink)
			end if
			end if

			
			' addform to page			
	    	  strPageHTML = AddForm( Form, strPageHTML )
			' add control to form
		
            Dim myValues As [String]() = {"{banner}", "{leftline}", "{leftspacer}", "{toppane}", "{leftpane}", "{contentpane}", "{rightspacer}", "{rightline}", "{rightpane}", "{bottompane}", "{footer}", "{tigramenu}", "{solpartmenu}", "{altmenu}", "{mailcheck}", "{tabmenu}"}
            Dim myKeys(15) As Integer
	
			  Dim i As Integer
               For i = myKeys.GetLowerBound(0) To myKeys.GetUpperBound(0)
			   myKeys.SetValue(strPageHTML.IndexOf(myValues(i)), i)
			   next i
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
			leftline.visible = false
			rightline.visible = false
						
            ' if first time loading this page then reload to avoid caching
            If Request.Params("tabId") Is Nothing Then
                Response.Expires = -1
            End If

            ' get module container
			Dim objAdmin As New AdminDB()
			
            Dim strContainer As String 
			If portalSettings.GetSiteSettings(_portalSettings.PortalID)("container") <> "" then
			strContainer = portalSettings.GetSiteSettings(_portalSettings.PortalID)("container")
			Else
			strContainer = "[MODULE]"
			end if
			Dim EditContainer As String =  "<div id=""EditTable"" align=""center""><table width=""750"" cellspacing=""0"" cellpadding=""0"" border=""0"" ><tr valign=""top""><td valign=""top"" align=""left"">[MODULE]</td></tr></table></div>"
			Dim AdminContainer As String = "<div id=""AdminTable"" align=""center""><table width=""750"" cellspacing=""0"" cellpadding=""0"" border=""0"" ><tr valign=""top""><td valign=""top"" align=""left"">[MODULE]</td></tr></table></div>"
			Dim LoginContainer As String = "<div id=""signin"">[MODULE]</div>"
			
			if portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey("logincontainer") then
                LoginContainer = "<div id=""signin"" style=""z-index: 4"">" & PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainer") & "</div>"
			end if

			if portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey("editcontainer") then
			EditContainer = "<div id=""EditTable"">" & portalSettings.GetSiteSettings(_portalSettings.PortalID)("editcontainer") & "</div>"
			end if
			if portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey("admincontainer") then
			AdminContainer = "<div id=""AdminTable"">" & portalSettings.GetSiteSettings(_portalSettings.PortalID)("admincontainer")  & "</div>"
			end if

			Dim _moduleSettings As New ModuleSettings()
			
			If Request.Params("edit") <> "" Then
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
                            Response.Redirect(GetFullDocument() & "?edit=" & _portalSettings.ActiveTab.TabId & "&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Edit Access Denied", True)
                            End If
                        End If
                    End If

                    ' load user control
                    If _moduleSettings.EditSrc <> "" Then
                        Try
                            Dim objModule As PortalModuleControl = CType(Me.LoadModule(_moduleSettings.EditSrc), PortalModuleControl)
                            objModule.ModuleConfiguration = _moduleSettings

                            ' show login module if the client is not yet authenticated
                            If Request.IsAuthenticated = False And Request.Params("def") = "PrivateMessages" Then
                                Dim SignInobjModule As PortalModuleControl = CType(Me.LoadModule("~/Admin/Security/SignIn.ascx"), PortalModuleControl)
                                SignInobjModule.ID = "SignIn"
                                ' No Module
                                AddModule(ContentPane, SignInobjModule, LoginContainer, _portalSettings.UploadDirectory, IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerAlignment") <> "", _
                                   PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerAlignment"), ""), _
                                    IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerColor") <> "", _
                                   PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerColor"), ""), _
                                   IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerBorder") <> "", _
                                    PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerBorder"), ""))
                            End If
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
                                If PortalSettings.GetHostSettings("EnableErrorReporting") <> "N" Then
                                    SendNotification(PortalSettings.GetHostSettings("HostEmail"), PortalSettings.GetHostSettings("HostEmail"), "", "ERROR LOADING MODULE", objException.ToString(), "")
                                End If
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
                        Dim blnShowLogin As Boolean = False
                    CheckSSL = _portalSettings.ActiveTab.ssl And _portalSettings.SSL
                        ' ensure that the user has access to the current page
                        If PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AuthorizedRoles) = False Then
                            If Request.IsAuthenticated = False Then
                                blnShowLogin = True
                            Else
                                Dim objModule As PortalModuleControl = CType(Me.LoadModule("~/Admin/Security/AccessDenied.ascx"), PortalModuleControl)
                                objModule.ID = "denied"
                                AddModule(ContentPane, objModule, strContainer, _portalSettings.UploadDirectory)
                            End If
                        End If

                        ' show login module if the client is not yet authenticated and they requested to login
                        If Request.IsAuthenticated = False And Request.QueryString("showlogin") = "1" Then
                            blnShowLogin = True
                        End If

                        ' show login module
                    If blnShowLogin Then
                        CheckSSL = _portalSettings.SSL
                        Dim objModule As PortalModuleControl = CType(Me.LoadModule("~/Admin/Security/SignIn.ascx"), PortalModuleControl)
                        objModule.ID = "SignIn"
                        AddModule(ContentPane, objModule, LoginContainer, _portalSettings.UploadDirectory, IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerAlignment") <> "", _
                          PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerAlignment"), ""), _
                           IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerColor") <> "", _
                          PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerColor"), ""), _
                          IIf(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerBorder") <> "", _
                           PortalSettings.GetSiteSettings(_portalSettings.PortalId)("logincontainerBorder"), ""))
                    End If

                        ' check portal expiry date
                        Dim blnExpired As Boolean = False
                        If _portalSettings.ExpiryDate <> "" Then
                            If CDate(_portalSettings.ExpiryDate) < Now() Then
                                blnExpired = True
                            End If
                        End If

                        ' Dynamically inject an expiry message into the Content pane if the portal license has expired
                        If blnExpired = True Then
                            Dim objModule As PortalModuleControl = CType(Me.LoadModule("~/Admin/Security/Expired.ascx"), PortalModuleControl)
                            objModule.ID = "expired"
                            AddModule(ContentPane, objModule, strContainer, _portalSettings.UploadDirectory, "center")
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
                                                If PortalSettings.GetHostSettings("EnableErrorReporting") <> "N" Then
                                                    SendNotification(PortalSettings.GetHostSettings("HostEmail"), PortalSettings.GetHostSettings("HostEmail"), "", "ERROR LOADING MODULE", objException.ToString(), "")
                                                End If
                                                If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Or PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True Then
                                                    parent.Controls.Add(New LiteralControl("<span class=""NormalRed"">Error Loading " & _moduleSettings.DesktopSrc & "</span>"))
                                                End If
                                            End If
                                        End Try
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

                Dim UserId As Integer = -1
                If Request.IsAuthenticated Then
                    UserId = CType(context.User.Identity.Name, Integer)
                End If

            ' Redirect to SSL if required
            CheckSecureSSL(HttpContext.Current.Request, CheckSSL)
                ' log visit to site and not a postBack
                If _portalSettings.SiteLogHistory <> 0 And Not Page.IsPostBack And Not Request.UserHostAddress = "192.168.1.1" Then
                    Dim URLReferrer As String = ""
                    If Not Request.UrlReferrer Is Nothing Then
                        URLReferrer = Request.UrlReferrer.ToString()
                    End If
                    Dim AffiliateId As Integer = -1
                    If Not Request.Params("AffiliateId") Is Nothing Then
                        If IsNumeric(Request.Params("AffiliateId")) Then
                            AffiliateId = Int32.Parse(Request.QueryString("AffiliateId"))
                        End If
                    End If
                    objAdmin.AddSiteLog(_portalSettings.PortalId, UserId, URLReferrer, Request.Url.ToString(), Request.UserAgent, Request.UserHostAddress, Request.UserHostName, _portalSettings.ActiveTab.TabId, AffiliateId)
                End If

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
            TempString = Replace(System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).LegalCopyright.ToString, "YYYY", Year(Now).ToString)
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
			TempString = "Copyright (c) 2004-" & Year(Now).ToString & " by DotNetZoom ( " & System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).ProductName.ToString & " )"
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
            arrContainer = Split(strContainer, "[MODULE]")
            objPane.Controls.Add(New LiteralControl(arrContainer(0)))
            objPane.Controls.Add(objModule)
            objPane.Controls.Add(New LiteralControl(arrContainer(1)))
            objPane.Visible = True

        End Sub



        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If Page.IsPostBack = False Then
                Session.Contents.Remove("FCKeditor:UserFilesPath")
            End If
            HttpContext.Current.RewritePath(HttpContext.Current.Items("RequestDocument"), String.Empty, HttpContext.Current.Request.QueryString.ToString())

        End Sub

    End Class

End Namespace