'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2008
' by Ren� Boulard ( http://www.reneboulard.qc.ca)'
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
' MODIFIED 2004 06 06 TO REMOVED SOLPARTMENU

Imports System.IO
Imports System.Text
Imports System.Data.SqlClient


Namespace DotNetZoom

    Public MustInherit Class DesktopPortalBanner
        Inherits System.Web.UI.UserControl

		Protected WithEvents grdDefinitions As System.Web.UI.WebControls.DataGrid		
        Protected WithEvents cmdPreview As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdAddModule As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton

        Protected WithEvents ddlLanguage As System.Web.UI.WebControls.DropDownList

        Protected WithEvents cmdClearPortalCache As System.Web.UI.WebControls.LinkButton
        Protected WithEvents tigraedit As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents liedittab As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents liaddtab As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents liadmin As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents lideletetab As System.Web.UI.HtmlControls.HtmlGenericControl



        Protected WithEvents editpanel As System.Web.UI.WebControls.TableCell
		Protected WithEvents pnlmoduleinfo As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents pnlmodulehelp As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblmodulehelp As System.Web.UI.WebControls.Label
        Protected WithEvents cmdAdminMenu As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEditTab As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdAddTab As System.Web.UI.WebControls.HyperLink
		Protected WithEvents TigraLink As System.Web.UI.WebControls.HyperLink
		Protected WithEvents LanguageLink As System.Web.UI.WebControls.HyperLink
        Protected WithEvents hypLogo As System.Web.UI.WebControls.HyperLink
        Protected WithEvents hypLogoImage As System.Web.UI.WebControls.Image
        Protected WithEvents hypBanner As System.Web.UI.WebControls.HyperLink
        Protected WithEvents hypBannerImage As System.Web.UI.WebControls.Image
        Protected WithEvents lblDate As System.Web.UI.WebControls.Label
        Protected WithEvents hypUser As System.Web.UI.WebControls.HyperLink
        Protected WithEvents hypHelp As System.Web.UI.WebControls.literal
        Protected WithEvents lblSeparator As System.Web.UI.WebControls.Label
        Protected WithEvents hypLogin As System.Web.UI.WebControls.HyperLink
		Protected WithEvents Flash As System.Web.UI.WebControls.Literal

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
			Dim objAdmin As New AdminDB()


            Dim objTIGRA As Control = Page.FindControl("tigra")
            If (Not objTIGRA Is Nothing) Then
                TigraLink.Visible = True
                tigraedit.Visible = True
                TigraLink.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("SS_Label_Tigra"), "100"))
                TigraLink.NavigateUrl = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "editmenu=1")
                TigraLink.Text = TigraLink.Text & GetLanguage("SS_Tigra_Edit")
            Else
                TigraLink.Visible = False
                tigraedit.Visible = False
            End If


            Dim TempAuthLanguage As String = ""
            If PortalSettings.GetSiteSettings(_portalSettings.PortalId).ContainsKey("languageauth") Then
                TempAuthLanguage = PortalSettings.GetSiteSettings(_portalSettings.PortalId)("languageauth")
            Else
                TempAuthLanguage = GetLanguage("N") & ";"
            End If
            ' Language to Use
            If Not Page.IsPostBack Then
                Dim HashL As Hashtable = objAdmin.GetAvailablelanguage
                For Each de As DictionaryEntry In HashL
                    Dim itemL As New ListItem()
                    itemL.Text = de.Value
                    itemL.Value = de.Key
                    If InStr(1, TempAuthLanguage, itemL.Value & ";") Then
                        ddlLanguage.Items.Add(itemL)
                    End If
                Next de

                If Not ddlLanguage.Items.FindByText(GetLanguage("language")) Is Nothing Then
                    ddlLanguage.Items.FindByText(GetLanguage("language")).Selected = True
                Else
                    ddlLanguage.SelectedIndex = 0
                End If
            End If

            If ddlLanguage.Items.Count > 1 Then ddlLanguage.Visible = True


            If PortalSecurity.IsSuperUser Then
                LanguageLink.Visible = True
                LanguageLink.ToolTip = GetLanguage("Language_Edit_Param")
                LanguageLink.NavigateUrl = "javascript:var m = window.open('controls/languageedit.aspx?tabid=" & _portalSettings.ActiveTab.TabId.ToString & "&L=" & GetLanguage("N") & "' , 'languageedit', 'width=800,height=300,left=100,top=100,resizable=1');m.focus();"
            End If




            Dim ServerPath As String
            Dim BannerId As Integer

            ' format the Request.ApplicationPath
            ServerPath = Request.ApplicationPath
            If Not ServerPath.EndsWith("/") Then
                ServerPath += "/"
            End If

            ' dynamically populate the portal settings



            Flash.Text = CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("flash"), String)
            If Flash.Text = "" Then
                Flash.Visible = False
            Else
                Flash.Visible = True
            End If


            If _portalSettings.LogoFile <> "" Then
                hypLogoImage.ImageUrl = _portalSettings.UploadDirectory & _portalSettings.LogoFile
            Else
                hypLogoImage.Visible = False
            End If
            hypLogo.ToolTip = _portalSettings.PortalName
            hypLogoImage.AlternateText = _portalSettings.PortalName
            hypLogo.NavigateUrl = GetPortalDomainName(_portalSettings.PortalAlias, Request)
            hypHelp.Visible = False
            lblSeparator.Visible = False
            If _portalSettings.UserRegistration <> 0 Then


                If _portalSettings.SSL Then
                    hypHelp.Text = "<a class=""OtherTabs"" href=""" & AddHTTPS(GetDomainName(Request)) & GetFullDocument() & "?edit=control&amp;tabid=" & _portalSettings.ActiveTab.TabId & "&amp;def=Register""" & ">" & GetLanguage("banner_register") & "</a>&nbsp;|&nbsp;"
                Else
                    hypHelp.Text = "<a class=""OtherTabs"" href=""" & GetFullDocument() & "?edit=control&amp;tabid=" & _portalSettings.ActiveTab.TabId & "&amp;def=Register""" & ">" & GetLanguage("banner_register") & "</a>&nbsp;|&nbsp;"
                End If
                hypHelp.Visible = True
                lblSeparator.Visible = True
            End If
            hypLogin.Text = GetLanguage("login")
            hypLogin.NavigateUrl = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "showlogin=1")
            If _portalSettings.SSL Then
                hypLogin.NavigateUrl = AddHTTPS(GetDomainName(Request)) + hypLogin.NavigateUrl
            End If
            lblDate.Text = ProcessLanguage("{date}")
            Dim objUser As New UsersDB()
            Dim dr As SqlDataReader

            hypUser.Text = ""
            ' Check for services in cache

            Dim TempKey As String = GetDBname() & "Services_" & CStr(_portalSettings.PortalId)
            Dim context As HttpContext = HttpContext.Current
            If context.Cache(TempKey) Is Nothing Then
                '	Item not in cache, get it manually   
                dr = objUser.GetServices(_portalSettings.PortalId, GetLanguage("N"))
                If dr.Read Then
                    context.Cache.Insert(TempKey, "oui", CDp(_portalSettings.PortalId), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.Normal, Nothing)
                Else
                    context.Cache.Insert(TempKey, "non", CDp(_portalSettings.PortalId), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.Normal, Nothing)
                End If
                dr.Close()
            End If

            If context.Cache(TempKey) = "oui" Then
                hypUser.Text = GetLanguage("Membership_Serv") + " | "
                hypUser.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_services")))
                hypUser.NavigateUrl = "~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Register&services=1"
            End If



            ' banner advertising
            If _portalSettings.BannerAdvertising <> 0 Then

                Dim objVendor As New VendorsDB()
                Dim result As SqlDataReader
                Select Case _portalSettings.BannerAdvertising
                    Case 1 ' local
                        result = objVendor.FindBanners(_portalSettings.PortalId, , _portalSettings.PortalId)
                    Case Else 'Global
                        result = objVendor.FindBanners(_portalSettings.PortalId)
                End Select
                If result.Read() Then
                    BannerId = result("BannerId")
                    hypBanner.Visible = True
                    hypBannerImage.Visible = True
                    hypBannerImage.Width = Unit.Pixel(468)
                    hypBannerImage.Height = Unit.Pixel(68)
                    Dim Tdr As SqlDataReader
                    Select Case _portalSettings.BannerAdvertising
                        Case 1 ' local
                            Tdr = objAdmin.GetSingleFile(CStr(result("ImageFile")), _portalSettings.PortalId)
                            hypBannerImage.ImageUrl = _portalSettings.UploadDirectory & CStr(result("ImageFile"))
                        Case Else ' global
                            Tdr = objAdmin.GetSingleFile(CStr(result("ImageFile")))
                            hypBannerImage.ImageUrl = glbSiteDirectory & CStr(result("ImageFile"))
                    End Select
                    hypBanner.ToolTip = CStr(result("BannerName"))
                    hypBannerImage.AlternateText = CStr(result("BannerName"))
                    If Tdr.Read() Then
                        hypBannerImage.Width = Unit.Pixel(Tdr("Width"))
                        hypBannerImage.Height = Unit.Pixel(Tdr("Height"))
                    End If
                    Tdr.Close()
                Else ' no banners defined
                    BannerId = -1
                    hypBannerImage.ImageUrl = "~/images/nobanner.gif"
                    hypBannerImage.AlternateText = GetLanguage("NoBanner")
                    hypBannerImage.Width = Unit.Pixel(468)
                    hypBannerImage.Height = Unit.Pixel(60)
                End If
                result.Close()

                hypBanner.NavigateUrl = ServerPath & "DesktopModules/Banners/BannerClickThrough.aspx?BannerId=" & CStr(BannerId)
            Else
                hypBanner.Visible = False
                hypBannerImage.Visible = False
                BannerId = -1
            End If

            ' If user logged in, customize welcome message
            If Request.IsAuthenticated = True Then
                hypUser.Text = "<img height=""14"" width=""15"" border=""0"" src=""images/1x1.gif"" Alt=""mod"" style="" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -347px;"">"
                ' dr("FullName")
                hypUser.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_profile")))

                If PortalSecurity.IsSuperUser Then
                    hypUser.NavigateUrl = "~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&UserId=" & _portalSettings.SuperUserId & "&def=Gestion usagers"
                Else
                    hypUser.NavigateUrl = "~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Register"
                End If

                If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                    hypHelp.Text = "<span onmouseover=""" & ReturnToolTip(GetLanguage("help")) & """>" & FormatEmail(PortalSettings.GetHostSettings("HostEmail"), Page, "<img height='12' width='12' border='0' src='images/1x1.gif' Alt='?' style='background: url(/images/uostrip.gif) no-repeat; background-position: 0px -362px;'>", _portalSettings.PortalName & " " & GetLanguage("need_help")) & "</span>"
                    hypHelp.Visible = True
                    lblSeparator.Visible = True
                Else ' registered user
                    hypHelp.Text = "<span onmouseover=""" & ReturnToolTip(GetLanguage("help")) & """>" & FormatEmail(_portalSettings.Email, Page, "<img height='12' width='12' border='0' src='images/1x1.gif' Alt='?' style='background: url(/images/uostrip.gif) no-repeat; background-position: 0px -362px;'>", GetLanguage("need_help")) & "</span>"
                    hypHelp.Visible = True
                    lblSeparator.Visible = True
                End If

                hypLogin.Text = "<img height=""14"" width=""14"" border=""0"" src=""images/1x1.gif""  title="""" onmouseover=""" & ReturnToolTip(GetLanguage("exit")) & """ border=""0"" Alt=""ca"" style="" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -333px;"">"
                hypLogin.NavigateUrl = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "logoff=ok")
            End If

            If _portalSettings.SSL Then
                hypUser.NavigateUrl = Replace(hypUser.NavigateUrl, "~", AddHTTPS(GetDomainName(Request)))
            End If


            ' To Show Admin Menu			
            If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Or PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) Then
                editpanel.Visible = True
                If IsAdminTab() Or (Not Request.Params("edit") Is Nothing) Then
                    editpanel.Visible = False
                Else
                    If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                        cmdAdminMenu.NavigateUrl = "~" & GetDocument() & "?adminpage=" & GetAdminModuleID() & "&tabid=" & _portalSettings.ActiveTab.TabId
                        cmdAdminMenu.Text = cmdAdminMenu.Text + GetLanguage("admin_txt")
                        cmdAdminMenu.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_menu"), "100"))
                        liadmin.Visible = True
                        cmdEditTab.NavigateUrl = "~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Onglets&action=edit"
                        cmdEditTab.Text = cmdEditTab.Text + GetLanguage("admin_tab_edit")
                        cmdEditTab.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_edit_tab"), "100"))
                        liedittab.Visible = True
                        cmdAddTab.NavigateUrl = "~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Onglets"
                        cmdAddTab.Text = cmdAddTab.Text + GetLanguage("admin_tab_add")
                        cmdAddTab.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_add_tab"), "100"))
                        liaddtab.Visible = True
                        cmdDelete.Text = cmdDelete.Text + GetLanguage("admin_delete_tab")
                        cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm")) & "');")
                        cmdDelete.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_tab_delete"), "100"))
                        lideletetab.Visible = True
                    End If

                    cmdAddModule.Text = cmdAddModule.Text + GetLanguage("admin_m_add")

                    cmdAddModule.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_add_module"), "120"))

                    cmdClearPortalCache.Text = cmdClearPortalCache.Text + GetLanguage("admin_caches_x")
                    cmdClearPortalCache.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_clear_caches"), "130"))

                    If Not Request.Cookies("_Tab_Admin_Preview" & _portalSettings.PortalId.ToString) Is Nothing Then
                        If Boolean.Parse(Request.Cookies("_Tab_Admin_Preview" & _portalSettings.PortalId.ToString).Value) = False Then
                            cmdPreview.Text = "<img  src=""/images/minus3.gif"" alt=""-"" style=""border-width:0px;""> " + GetLanguage("admin_option_hide")
                            cmdPreview.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_hide_option")))
                        Else
                            cmdPreview.Text = "<img  src=""/images/plus3.gif"" alt=""+"" style=""border-width:0px;""> " + GetLanguage("admin_option_show")
                            cmdPreview.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_show_option")))
                        End If
                    Else
                        cmdPreview.Text = "<img  src=""/images/minus3.gif"" alt=""-"" style=""border-width:0px;""> " + GetLanguage("admin_option_hide")
                        cmdPreview.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("admin_hide_option")))
                    End If
                End If

                    cmdPreview.Visible = True
                    pnlmodulehelp.Visible = False
                    pnlmoduleinfo.Visible = False
                    If Not Page.IsPostBack Then
                        ' Show Help if asked for	
                        If Not Request.Params("mdi") Is Nothing Then
                            If IsNumeric(Request.Params("mdi")) Then
                                ShowinfoModule()
                                lblmodulehelp.Text = GetLanguage("error")
                                Dim dri As SqlDataReader = objAdmin.GetSingleModuleDefinition(GetLanguage("N"), Int32.Parse(Request.QueryString("mdi")))
                                If dri.Read Then
                                    lblmodulehelp.Text = "<b>" & GetLanguage("module_info") & " " & dri("FriendlyName") & "</b>"

                                    Dim TempString As String = dri("Description").ToString
                                    If TempString <> "" Then
                                        lblmodulehelp.Text += "<BLOCKQUOTE>" & TempString & "</BLOCKQUOTE>"
                                    Else
                                        lblmodulehelp.Text += "<BLOCKQUOTE>" & GetLanguage("no_module_info") & "</BLOCKQUOTE>"
                                    End If
                                    pnlmodulehelp.Visible = True
                                End If
                                dri.Close()
                            End If
                        End If

                        If Not Request.Params("mda") Is Nothing Then
                            ' add module
                            If IsNumeric(Request.Params("mda")) Then
                                Dim strEditRoles As String = _portalSettings.ActiveTab.AdministratorRoles
                                If InStr(1, strEditRoles, _portalSettings.AdministratorRoleId.ToString & ";") = 0 Then
                                    strEditRoles += _portalSettings.AdministratorRoleId.ToString & ";"
                                End If

                                ' save to database
                                Dim dra As SqlDataReader = objAdmin.GetSingleModuleDefinition(GetLanguage("N"), Int32.Parse(Request.QueryString("mda")))
                                Dim TempString As String = ""
                                If dra.Read Then
                                    If dra("FriendlyName").ToString <> "" Then
                                        TempString = dra("FriendlyName").ToString
                                    End If
                                End If
                                dra.Close()


                                Dim TempModuleID As Integer = objAdmin.AddModule(_portalSettings.ActiveTab.TabId, -3, "ContentPane", TempString, Request.Params("mda"), 0, strEditRoles, False, GetLanguage("N"))
                                ' Put In the default Container
                                Dim _Settings As Hashtable = PortalSettings.GetSiteSettings(_portalSettings.PortalId)

                                objAdmin.UpdateModuleSetting(TempModuleID, "containerTitleHeaderClass", IIf(_Settings("containerTitleHeaderClass") <> "", _Settings("containerTitleHeaderClass"), ""))
                                objAdmin.UpdateModuleSetting(TempModuleID, "containerTitleCSSClass", IIf(_Settings("containerTitleCSSClass") <> "", _Settings("containerTitleCSSClass"), ""))
                                objAdmin.UpdateModuleSetting(TempModuleID, "container", IIf(_Settings("container") <> "", _Settings("container"), ""))
                                objAdmin.UpdateModuleSetting(TempModuleID, "TitleContainer", IIf(_Settings("TitleContainer") <> "", _Settings("TitleContainer"), ""))
                                objAdmin.UpdateModuleSetting(TempModuleID, "ModuleContainer", IIf(_Settings("ModuleContainer") <> "", _Settings("ModuleContainer"), ""))
                                objAdmin.UpdateModuleSetting(TempModuleID, "containerAlignment", IIf(_Settings("containerAlignment") <> "", _Settings("containerAlignment"), ""))
                                objAdmin.UpdateModuleSetting(TempModuleID, "containerColor", IIf(_Settings("containerColor") <> "", _Settings("containerColor"), ""))
                                objAdmin.UpdateModuleSetting(TempModuleID, "containerBorder", IIf(_Settings("containerBorder") <> "", _Settings("containerBorder"), ""))
                                objAdmin.UpdateTabModuleOrder(_portalSettings.ActiveTab.TabId)
                                ' reset the tab cashe also
                                ClearTabCache(_portalSettings.ActiveTab.TabId)
                                ' Redirect to edit the module settings
                                ' http://localhost/fr.accueil.aspx?edit=control&tabid=1&mid=339&def=Module
                                Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "edit=control&mid=" & TempModuleID.ToString & "&def=Module"), True)

                            End If

                        End If
                        ' only show panel module if not postback
                    End If

            End If




            ' Modified by ren� boulard, turn off the ID tag

            hypLogo.ID = ""
            hypLogoImage.ID = ""
            hypBanner.ID = ""
            hypBannerImage.ID = ""
            lblDate.ID = ""
            hypUser.ID = ""

            lblSeparator.ID = ""
            hypLogin.ID = ""



        End Sub
			

        Private Function GetAdminModuleID() As String
            If Application("admin") Is Nothing Then
                Dim objAdmin As New AdminDB()
                Dim dr As SqlDataReader
                dr = objAdmin.GetSingleModuleDefinitionByName(GetLanguage("N"), "Admin")
                If dr.Read Then
                    Application("admin") = dr("ModuleDefID").ToString
                Else
                    Application("admin") = "87"
                End If
                dr.Close()
            End If
            Return Application("admin")
        End Function

        Private Sub ShowinfoModule()

            pnlmoduleinfo.Visible = True
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objAdmin As New AdminDB()
            grdDefinitions.DataSource = objAdmin.GetModuleDefinitions(_portalSettings.PortalId, GetLanguage("N"), False)
            grdDefinitions.DataBind()

        End Sub

			

            Function FormatURL() As String

                Dim ServerPath As String

                ServerPath = IIf(request.ApplicationPath = "/", "", request.ApplicationPath)
                If Not ServerPath.EndsWith("/") Then
                    ServerPath += "/"
                End If

                Return ServerPath & GetLanguage("N") & ".default.aspx"

            End Function

			
		Function FormatModuleURL(ByVal strKeyName As String, ByVal strKeyValue As String) As String
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)
            FormatModuleURL = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, strKeyName & "=" & strKeyValue)


        End Function
			
			
        Private Sub cmdPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPreview.Click

            ' Obtain portalId from Current Context
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)
            Dim objPreview As HttpCookie

            If Request.Cookies("_Tab_Admin_Preview" & _portalSettings.PortalId.ToString) Is Nothing Then
                objPreview = New HttpCookie("_Tab_Admin_Preview" & _portalSettings.PortalId.ToString)
                objPreview.Expires = DateTime.MaxValue ' never expires
                objPreview.Value = False
                Response.AppendCookie(objPreview)
            End If
            objPreview = Request.Cookies("_Tab_Admin_Preview" & _portalSettings.PortalId.ToString)
            objPreview.Value = Not Boolean.Parse(Request.Cookies("_Tab_Admin_Preview" & _portalSettings.PortalId.ToString).Value)
            Response.SetCookie(objPreview)
            If Not Request.UrlReferrer.ToString Is Nothing Then
                Response.Redirect(Request.UrlReferrer.ToString)
            Else
                Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, ""), True)
            End If

        End Sub
			
        Private Sub cmdAddModule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddModule.Click
            ShowinfoModule()
        End Sub
		
        Private Sub cmdClearPortalCache_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClearPortalCache.Click
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If PortalSecurity.IsSuperUser Then
                ClearHostCache()
            Else
                ClearPortalCache(_portalSettings.PortalId)
            End If
            ' Redirect to the same page to pick up changes
            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, ""), True)
        End Sub

		Private Sub ddlLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLanguage.SelectedIndexChanged
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		Dim strURL As String = ""              
        Dim intParam As Integer
        For intParam = 0 To Request.QueryString.Count - 1
        Select Case Request.QueryString.Keys(intParam).ToLower()
        Case "tabid", "tabname"
              Case Else
			  if strURL = "" then
			  strURL = Request.QueryString.Keys(intParam) + "=" + Request.QueryString(intParam)
			  else
              strURL += "&" + Request.QueryString.Keys(intParam) + "=" + Request.QueryString(intParam)
			  end if
        End Select
        Next
            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, strURL, ddlLanguage.SelectedItem.Value), True)
		end sub

        Private Sub cmdDelete_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

            objAdmin.DeleteTab(_portalSettings.ActiveTab.TabId)
            Dim dr As SqlDataReader = objAdmin.GetTabById(_portalSettings.ActiveTab.TabId, GetLanguage("N"))
            If Not dr.Read Then
                objAdmin.UpdatePortalTabOrder(PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N")), _portalSettings.ActiveTab.TabId, -2)
            End If
            dr.Close()
            ClearPortalCache(_portalSettings.PortalId)
            Response.Redirect(GetPortalDomainName(_portalSettings.PortalAlias, Request), True)
        End Sub

		
    End Class

End Namespace