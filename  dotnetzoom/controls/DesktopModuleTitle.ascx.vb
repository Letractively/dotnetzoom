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
Imports System.Text
Imports Solpart
Namespace DotNetZoom

    Public MustInherit Class DesktopModuleTitle
        Inherits System.Web.UI.UserControl
		Protected WithEvents Titlebefore As System.Web.UI.WebControls.Literal
		Protected WithEvents Titleafter As System.Web.UI.WebControls.Literal
        Protected WithEvents pnlModuleTitle As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents pnlAdminTitle As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents pnlHelp As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents cmdEditModule As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEditModuleImage As System.Web.UI.WebControls.Image
		Protected WithEvents cmdEditModuleImage1 As System.Web.UI.WebControls.Image
        Protected WithEvents cmdModuleUp As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdModuleDown As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdModuleTop As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmddelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdModuleBottom As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdModuleLeft As System.Web.UI.WebControls.LinkButton
		Protected WithEvents cmdModuleRefresh As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdModuleRight As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblModuleTitle As System.Web.UI.WebControls.Label
		Protected WithEvents lblEditModuleTitle As System.Web.UI.WebControls.Label
        Protected WithEvents cmdEditContent As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEditOptions As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEditContent1 As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEditOptions1 As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEditOptions2 As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEditOptions4 As System.Web.UI.WebControls.HyperLink

        Protected WithEvents cmdDisplayModule As System.Web.UI.WebControls.LinkButton
        Protected WithEvents CellEdit As System.Web.UI.HtmlControls.HtmlTableCell
		Protected WithEvents cellhelp As System.Web.UI.HtmlControls.HtmlTableCell
		Protected WithEvents help1 As System.Web.UI.WebControls.HyperLink
		
		Protected WithEvents TableTitle As System.Web.UI.WebControls.Literal
		Protected WithEvents TableTitle1 As System.Web.UI.WebControls.Literal
		
        Protected WithEvents CellOptions As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents CellOptions2 As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents cAdmin As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents cmdAdmin As System.Web.UI.WebControls.Literal
        Protected WithEvents rowAdmin1 As System.Web.UI.HtmlControls.HtmlTableCell
		Protected WithEvents rowDisplay As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents lblModuleContent As System.Web.UI.WebControls.Label
		Protected WithEvents modifier As System.Web.UI.HtmlControls.HtmlGenericControl
		Protected WithEvents modifierC As System.Web.UI.HtmlControls.HtmlGenericControl
		Protected WithEvents param As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents param2 As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents haut As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents delete As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents top As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents bottom As System.Web.UI.HtmlControls.HtmlGenericControl
		Protected WithEvents bas As System.Web.UI.HtmlControls.HtmlGenericControl
		Protected WithEvents gauche As System.Web.UI.HtmlControls.HtmlGenericControl
		Protected WithEvents droite As System.Web.UI.HtmlControls.HtmlGenericControl
		Protected WithEvents purger As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents help As System.Web.UI.WebControls.HyperLink


		
	   Protected WithEvents ctlMenu As SolpartWebControls.Solpartmenu
		

        Public DisplayTitle As [String] = Nothing
		Public DisplayHelp As [String] = Nothing
        Public EditText As [String] = Nothing
        Public EditURL As [String] = Nothing
        Public OptionsURL As [String] = Nothing
        Public EditIMG As [String] = Nothing
        Public TitleVisible As [Boolean] = True
        Public DisplayOptions As [Boolean] = False
        Public DisplayOptions2 As [Boolean] = False
        Public OptionsText2 As [String] = Nothing
        Public OptionsText As [String] = Nothing
        Public Options2URL As [String] = Nothing
        Public Options2IMG As [String] = Nothing


        Private tabId As Integer = 0
        Public styleplusminus As String = Nothing
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
			Dim _language As HashTable = HttpContext.Current.Items("Language")
			ctlMenu.Visible = False
			
			
            tabId = _portalSettings.ActiveTab.TabId
			
            ' Obtain reference to parent portal module
            Dim portalModule As PortalModuleControl = CType(Me.Parent, PortalModuleControl)
			Dim _Setting as Hashtable
			

			lblModuleTitle.Text = ""

       ' Display the Module and Content Edit button
       If Not IsNothing(portalModule.ModuleConfiguration) Then

                If Not IsAdminTab() And (Request.Params("edit") Is Nothing) And (Request.Params("def") Is Nothing) And portalModule.ModuleId > 0 Then
                    'Normal Module

                    pnlModuleTitle.Visible = True
                    If Not IsNothing(DisplayTitle) Then
                        ' Display Custom Title
                        lblModuleTitle.Text = DisplayTitle
                    Else
                        lblModuleTitle.Text = portalModule.ModuleConfiguration.ModuleTitle
                    End If
                    lblModuleTitle.ID = ""
                    TableTitle.ID = ""

                    _Setting = PortalSettings.GetModuleSettings(portalModule.ModuleId)
                    If _Setting("containerTitleHeaderClass") <> "" Then
                        TableTitle.Text = _Setting("containerTitleHeaderClass")
                    Else
                        TableTitle.Text = "headertitle"
                    End If

                    If _Setting("containerTitleCSSClass") <> "" Then
                        lblModuleTitle.CssClass = _Setting("containerTitleCSSClass")
                    Else
                        lblModuleTitle.CssClass = "Head"
                    End If
                    If _Setting("TitleContainer") <> "" Then
                        Dim arrContainer As Array = SplitContainer(_Setting("TitleContainer"), _portalSettings.UploadDirectory, IIf(_Setting("containerAlignment") <> "", _Setting("containerAlignment"), ""), IIf(_Setting("containerColor") <> "", _Setting("containerColor"), ""), IIf(_Setting("containerBorder") <> "", _Setting("containerBorder"), ""))
                        Titlebefore.Text = arrContainer(0)
                        Titlebefore.Visible = True
                        Titleafter.Text = arrContainer(1)
                        Titleafter.Visible = True
                    End If


                    ' check if the Module Title is hidden
                    If portalModule.ModuleConfiguration.ShowTitle = False Then
                        pnlModuleTitle.Visible = False
                    End If
                    ' check if Personalization is allowed
                    If portalModule.ModuleConfiguration.Personalize = 2 Then
                        cmdDisplayModule.Enabled = False
                        rowDisplay.Visible = False
                    End If



                    If portalModule.ModuleConfiguration.IconFile <> "" Then
                        cmdEditModuleImage.ImageUrl = _portalSettings.UploadDirectory & portalModule.ModuleConfiguration.IconFile
                        cmdEditModuleImage.Visible = True
                        cmdEditModuleImage.ToolTip = lblModuleTitle.Text
                        cmdEditModuleImage.AlternateText = lblModuleTitle.Text
                    End If

                    Dim blnPreview As Boolean = False
                    If Not Request.Cookies("_Tab_Admin_Preview" & _portalSettings.PortalId.ToString) Is Nothing Then
                        blnPreview = Boolean.Parse(Request.Cookies("_Tab_Admin_Preview" & _portalSettings.PortalId.ToString).Value)
                    End If

                    If (blnPreview = False And (PortalSecurity.IsInRoles(portalModule.ModuleConfiguration.AuthorizedEditRoles) = True _
                        Or PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True)) Then

                        If Not IsNothing(EditText) Then
                            pnlModuleTitle.Visible = True
                            If IsNothing(EditIMG) Then
                                cmdEditContent.Text = "<img  src=""" & glbPath & "images/edit.gif"" alt=""*"" style=""border-width:0px;""> " & EditText
                            Else
                                cmdEditContent.Text = EditIMG & " " & EditText
                            End If


                            modifierC.Visible = True
                            rowAdmin1.Visible = True
                            cmdEditContent.ToolTip = EditText
                            If Not IsNothing(EditURL) Then
                                cmdEditContent.NavigateUrl = EditURL & IIf(InStr(1, EditURL, "?") <> 0, "&", "?") & "tabid=" & tabId & "&mid=" + portalModule.ModuleId.ToString()
                            Else
                                cmdEditContent.NavigateUrl = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, HttpContext.Current.Request.IsSecureConnection, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "edit=control" & IIf(Request.Params("adminpage") Is Nothing, "&mid=" & portalModule.ModuleId.ToString, "&adminpage=" & Request.Params("adminpage")))
                            End If

                            If DisplayOptions Then

                                cmdEditOptions.Text = cmdEditOptions.Text & GetLanguage("paramêtres")
                                If OptionsText = Nothing Then
                                    cmdEditOptions.ToolTip = GetLanguage("title_ShowParam")
                                Else
                                    cmdEditOptions.ToolTip = OptionsText
                                End If

                                cmdEditOptions.Visible = True
                                param.Visible = True
                                rowAdmin1.Visible = True
                                If Not IsNothing(OptionsURL) Then
                                    cmdEditOptions.NavigateUrl = OptionsURL & IIf(InStr(1, OptionsURL, "?") <> 0, "&", "?") & "tabid=" & tabId & "&mid=" + portalModule.ModuleId.ToString()
                                Else
                                    cmdEditOptions.NavigateUrl = cmdEditContent.NavigateUrl & "&options=1"
                                End If


                            End If

                            If DisplayOptions2 Then

                                cmdEditOptions4.Text = cmdEditOptions4.Text & GetLanguage("paramêtres")
                                If OptionsText2 = Nothing Then
                                    cmdEditOptions4.ToolTip = GetLanguage("title_ShowParam")
                                Else
                                    cmdEditOptions4.ToolTip = OptionsText2
                                End If

                                cmdEditOptions4.Visible = True
                                param2.Visible = True
                                rowAdmin1.Visible = True
                                If Not IsNothing(Options2URL) Then
                                    cmdEditOptions4.NavigateUrl = Options2URL & IIf(InStr(1, Options2URL, "?") <> 0, "&", "?") & "tabid=" & tabId & "&mid=" + portalModule.ModuleId.ToString()
                                Else
                                    cmdEditOptions4.NavigateUrl = cmdEditContent.NavigateUrl & "&options=2"
                                End If


                            End If



                        End If
                    End If

                    If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Or PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True Then
                        If blnPreview = False Then
                            pnlModuleTitle.Visible = True
                            rowAdmin1.Visible = True
                            cmdEditModule.NavigateUrl = GetFullDocument() & "?tabid=" & tabId & "&mid=" & portalModule.ModuleId.ToString() & "&def=Module"
                            cmdEditModule.ToolTip = GetLanguage("title_ParamModif")
                            cmdEditModule.Visible = True
                            cmdEditModule.Text = cmdEditModule.Text & GetLanguage("modifier")
                            modifier.Visible = True

                            delete.Visible = True
                            cmddelete.Visible = True
                            cmddelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm")) & "');")
                            cmddelete.ToolTip = GetLanguage("delete")
                            cmddelete.Text = cmddelete.Text & GetLanguage("delete")



                            Dim TempModuleOrder As Integer = 1

                            If portalModule.ModuleConfiguration.ModuleOrder > 3 Or _
                               portalModule.ModuleConfiguration.ModuleOrder > 0 And _
                               portalModule.ModuleConfiguration.Language = "" Then
                                cmdModuleUp.ToolTip = GetLanguage("title_UpToolTip")
                                cmdModuleUp.CommandArgument = portalModule.ModuleConfiguration.PaneName
                                cmdModuleUp.CommandName = "up"
                                cmdModuleUp.Text = cmdModuleUp.Text & GetLanguage("haut")
                                cmdModuleUp.Visible = True
                                haut.Visible = True
                            End If


                            Select Case portalModule.ModuleConfiguration.PaneName
                                Case "ContentPane"
                                    TempModuleOrder = HttpContext.Current.Items("ModuleOrderCenter")
                                Case "LeftPane"
                                    TempModuleOrder = HttpContext.Current.Items("ModuleOrderLeft")
                                Case "RightPane"
                                    TempModuleOrder = HttpContext.Current.Items("ModuleOrderRight")
                                Case "TopPane"
                                    TempModuleOrder = HttpContext.Current.Items("ModuleOrderTop")
                                Case "BottomPane"
                                    TempModuleOrder = HttpContext.Current.Items("ModuleOrderBottom")
                            End Select

                            If (portalModule.ModuleConfiguration.ModuleOrder <> 0) And _
                                (portalModule.ModuleConfiguration.ModuleId <> TempModuleOrder) Then
                                cmdModuleDown.ToolTip = GetLanguage("title_DownToolTip")
                                cmdModuleDown.CommandArgument = portalModule.ModuleConfiguration.PaneName
                                cmdModuleDown.CommandName = "down"
                                cmdModuleDown.Text = cmdModuleDown.Text & GetLanguage("bas")
                                cmdModuleDown.Visible = True
                                bas.Visible = True
                            End If

                            If LCase(portalModule.ModuleConfiguration.PaneName) = "contentpane" Or _
                               LCase(portalModule.ModuleConfiguration.PaneName) = "rightpane" Then
                                cmdModuleLeft.ToolTip = GetLanguage("title_LeftToolTip")
                                cmdModuleLeft.CommandArgument = portalModule.ModuleConfiguration.PaneName
                                cmdModuleLeft.CommandName = "left"
                                cmdModuleLeft.Text = cmdModuleLeft.Text & GetLanguage("gauche")
                                cmdModuleLeft.Visible = True
                                gauche.Visible = True
                            End If

                            If LCase(portalModule.ModuleConfiguration.PaneName) = "leftpane" Or _
                               LCase(portalModule.ModuleConfiguration.PaneName) = "contentpane" Then
                                cmdModuleRight.ToolTip = GetLanguage("title_RightToolTip")
                                cmdModuleRight.CommandArgument = portalModule.ModuleConfiguration.PaneName
                                cmdModuleRight.CommandName = "right"
                                cmdModuleRight.Text = cmdModuleRight.Text & GetLanguage("droite")
                                cmdModuleRight.Visible = True
                                droite.Visible = True
                            End If


                            If ((LCase(portalModule.ModuleConfiguration.PaneName) = "contentpane" Or _
                                LCase(portalModule.ModuleConfiguration.PaneName) = "leftpane" Or _
                                LCase(portalModule.ModuleConfiguration.PaneName) = "rightpane") And _
                               HttpContext.Current.Items("ModuleOrderTop") <> -1) Or _
                               LCase(portalModule.ModuleConfiguration.PaneName) = "bottompane" Then
                                cmdModuleTop.ToolTip = GetLanguage("title_TopToolTip")
                                cmdModuleTop.CommandArgument = portalModule.ModuleConfiguration.PaneName
                                cmdModuleTop.CommandName = "top"
                                cmdModuleTop.Text = cmdModuleTop.Text & GetLanguage("top")
                                cmdModuleTop.Visible = True
                                top.Visible = True
                            End If


                            If ((LCase(portalModule.ModuleConfiguration.PaneName) = "contentpane" Or _
                                LCase(portalModule.ModuleConfiguration.PaneName) = "leftpane" Or _
                                LCase(portalModule.ModuleConfiguration.PaneName) = "rightpane") And _
                               HttpContext.Current.Items("ModuleOrderBottom") <> -1) Or _
                               LCase(portalModule.ModuleConfiguration.PaneName) = "toppane" Then
                                cmdModuleBottom.ToolTip = GetLanguage("title_bottomToolTip")
                                cmdModuleBottom.CommandArgument = portalModule.ModuleConfiguration.PaneName
                                cmdModuleBottom.CommandName = "bottom"
                                cmdModuleBottom.Text = cmdModuleBottom.Text & GetLanguage("bottom")
                                cmdModuleBottom.Visible = True
                                bottom.Visible = True
                            End If



                            ' Check to see if a cash is on

                            Dim TempKey As String = GetDBname() & "M_" & CStr(portalModule.ModuleConfiguration.ModuleId)
                            Dim context As HttpContext = HttpContext.Current
                            cmdModuleRefresh.ToolTip = ""
                            cmdModuleRefresh.Text = cmdModuleRefresh.Text & GetLanguage("actualiser")

                            If Not context.Cache(TempKey) Is Nothing Then

                                cmdModuleRefresh.ToolTip = GetLanguage("title_CacheToolTip")
                                cmdModuleRefresh.Visible = True
                                purger.Visible = True
                            End If
                            TempKey = GetDBname() & "_public" & CStr(portalModule.ModuleConfiguration.ModuleId)

                            If Not context.Cache(TempKey) Is Nothing Then
                                If cmdModuleRefresh.ToolTip = "" Then
                                    cmdModuleRefresh.ToolTip = GetLanguage("title_CacheModToolTip")
                                Else
                                    cmdModuleRefresh.ToolTip = GetLanguage("title_CacheModToolTip0")
                                End If
                                cmdModuleRefresh.Visible = True
                                purger.Visible = True
                            End If
                        End If
                    End If

                    If rowAdmin1.Visible And Not Page.FindControl("solpart") Is Nothing Then
                        ' generate solpart action menu
                        ' Only Show one menu
                        ctlMenu.Visible = True
                        ctlMenu.SystemImagesPath = glbSiteDirectory()
                        ctlMenu.IconImagesPath = glbSiteDirectory()
                        ctlMenu.RootArrow = True
                        ctlMenu.RootArrowImage = "action.gif"
                        ctlMenu.SystemScriptPath = glbPath() & "controls/SolpartMenu/"
                        Dim objMenuItem As Solpart.WebControls.SPMenuItemNode
                        objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, "", ""))
                        objMenuItem.ItemStyle = "background:transparent; height: 20px;"
                        objMenuItem.ImageStyle = "background:transparent; height: 20px;"
                        If cmdEditModule.Visible Then objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 1, "&nbsp;" & cmdEditModule.Text & "&nbsp;&nbsp;&nbsp;", cmdEditModule.ResolveClientUrl(cmdEditModule.NavigateUrl)))
                        If cmdEditContent.Visible Then objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 2, "&nbsp;" & cmdEditContent.Text & "&nbsp;&nbsp;&nbsp;", cmdEditContent.ResolveClientUrl(cmdEditContent.NavigateUrl)))
                        If cmdEditOptions.Visible Then objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 3, "&nbsp;" & cmdEditOptions.Text & "&nbsp;&nbsp;&nbsp;", cmdEditOptions.ResolveClientUrl(cmdEditOptions.NavigateUrl)))
                        If cmdEditOptions4.Visible Then objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 3, "&nbsp;" & cmdEditOptions4.Text & "&nbsp;&nbsp;&nbsp;", cmdEditOptions4.ResolveClientUrl(cmdEditOptions4.NavigateUrl)))

                        If cmdModuleTop.Visible Then
                            objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 4, "&nbsp;" & cmdModuleTop.Text & "&nbsp;&nbsp;&nbsp;", GetClientScriptURL("4")))
                            objMenuItem.RunAtServer = True
                        End If

                        If cmdModuleUp.Visible Then
                            objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 5, "&nbsp;" & cmdModuleUp.Text & "&nbsp;&nbsp;&nbsp;", GetClientScriptURL("5")))
                            objMenuItem.RunAtServer = True
                        End If
                        If cmdModuleDown.Visible Then
                            objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 6, "&nbsp;" & cmdModuleDown.Text & "&nbsp;&nbsp;&nbsp;", GetClientScriptURL("6")))
                            objMenuItem.RunAtServer = True
                        End If
                        If cmdModuleLeft.Visible Then
                            objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 7, "&nbsp;" & cmdModuleLeft.Text & "&nbsp;&nbsp;&nbsp;", GetClientScriptURL("7")))
                            objMenuItem.RunAtServer = True
                        End If
                        If cmdModuleRight.Visible Then
                            objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 8, "&nbsp;" & cmdModuleRight.Text & "&nbsp;&nbsp;&nbsp;", GetClientScriptURL("8")))
                            objMenuItem.RunAtServer = True
                        End If
                        If cmdModuleBottom.Visible Then
                            objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 9, "&nbsp;" & cmdModuleBottom.Text & "&nbsp;&nbsp;&nbsp;", GetClientScriptURL("9")))
                            objMenuItem.RunAtServer = True
                        End If

                        If cmdModuleRefresh.Visible Then
                            objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 10, "&nbsp;" & cmdModuleRefresh.Text & "&nbsp;&nbsp;&nbsp;", GetClientScriptURL("10")))
                            objMenuItem.RunAtServer = True
                        End If

                        If cmddelete.Visible Then
                            objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(0, 11, "&nbsp;" & cmddelete.Text & "&nbsp;&nbsp;&nbsp;", GetClientScriptURL("11")))
                            objMenuItem.RunAtServer = True
                        End If

                    Else
                        ctlMenu.Visible = False
                    End If  'Solpart Action  

                    If cmdDisplayModule.Enabled Then
                        Dim pnlModuleContent As PlaceHolder = Parent.FindControl("pnlModuleContent")
                        If Not pnlModuleContent Is Nothing Then
                            Dim objModuleVisible As HttpCookie = Request.Cookies("_Module" & portalModule.ModuleId.ToString & "_Visible")

                            If Not objModuleVisible Is Nothing Then
                                If objModuleVisible.Value = "true" Then
                                    pnlModuleContent.Visible = True
                                    styleplusminus = "background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -377px;"
                                    cmdDisplayModule.ToolTip = GetLanguage("title_Dec")
                                Else
                                    pnlModuleContent.Visible = False
                                    styleplusminus = "background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -407px;"
                                    cmdDisplayModule.ToolTip = GetLanguage("title_Inc")

                                End If
                            Else
                                If portalModule.ModuleConfiguration.Personalize = 1 Then
                                    pnlModuleContent.Visible = False
                                    styleplusminus = "background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -407px;"

                                    cmdDisplayModule.ToolTip = GetLanguage("title_Inc")

                                Else
                                    pnlModuleContent.Visible = True
                                    styleplusminus = "background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -377px;"
                                    cmdDisplayModule.ToolTip = GetLanguage("title_Dec")

                                End If
                            End If
                            cmdDisplayModule.Visible = True
                            rowDisplay.Visible = True
                        End If
                    End If
                Else

                    If IsAdminTab() Or (Not Request.Params("def") Is Nothing) Or (Not Request.Params("edit") Is Nothing) Then
                        ' Edit or AdminModule
                        pnlAdminTitle.Visible = True
                        pnlModuleTitle.Visible = False
                        If Not IsNothing(DisplayTitle) Then
                            ' Display Custom Title
                            lblEditModuleTitle.Text = DisplayTitle
                        Else
                            lblEditModuleTitle.Text = portalModule.ModuleConfiguration.ModuleTitle
                        End If
                        lblEditModuleTitle.CssClass = "Head"
                        lblEditModuleTitle.ID = ""
                        TableTitle1.ID = ""
                        TableTitle1.Text = "headertitle"


                        If portalModule.ModuleConfiguration.EditModuleIcon <> "" Then
                            cmdEditModuleImage1.ImageUrl = glbPath & "images/" & portalModule.ModuleConfiguration.EditModuleIcon
                            cmdEditModuleImage1.ToolTip = lblEditModuleTitle.Text
                            cmdEditModuleImage1.AlternateText = lblEditModuleTitle.Text
                            cmdEditModuleImage1.Visible = True
                            cmdEditModuleImage1.ID = ""
                        End If
                        If PortalSecurity.IsInRoles(portalModule.ModuleConfiguration.AuthorizedEditRoles) = True _
                            Or PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True _
                        Or (portalModule.ModuleConfiguration.PaneName = "Edit") Then

                            If Not IsNothing(EditText) Then
                                CellEdit.Visible = True
                                cmdEditContent1.ToolTip = EditText
                                cmdEditContent1.Attributes.Add("onmouseover", ReturnToolTip(EditText))
                                If Not IsNothing(EditIMG) Then
                                    cmdEditContent1.Text = EditIMG
                                End If
                                If Not IsNothing(EditURL) Then
                                    If portalModule.ModuleId > -1 Then
                                        cmdEditContent1.NavigateUrl = EditURL & IIf(InStr(1, EditURL, "?") <> 0, "&", "?") & "tabid=" & tabId & "&mid=" + portalModule.ModuleId.ToString()
                                    Else
                                        cmdEditContent1.NavigateUrl = EditURL
                                    End If
                                Else
                                    cmdEditContent1.NavigateUrl = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, HttpContext.Current.Request.IsSecureConnection, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "edit=control" & IIf(Request.Params("adminpage") Is Nothing, "&mid=" & portalModule.ModuleId.ToString, "&adminpage=" & Request.Params("adminpage")) & IIf(Not (Request.Params("hostpage") Is Nothing), "&hostpage=", ""))
                                End If
                                If DisplayOptions Then
                                    cmdEditOptions1.NavigateUrl = cmdEditContent1.NavigateUrl & "&options=1"
                                    If OptionsText = Nothing Then
                                        cmdEditOptions1.ToolTip = GetLanguage("title_ShowParam")
                                        cmdEditOptions1.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("title_ShowParam")))
                                    Else
                                        cmdEditOptions1.ToolTip = OptionsText
                                        cmdEditOptions1.Attributes.Add("onmouseover", ReturnToolTip(OptionsText))
                                    End If
                                    cmdEditOptions1.Visible = True
                                    CellOptions.Visible = True
                                End If
                            End If

                            If DisplayOptions2 Then
                                Options2URL = Options2URL.Replace("&options=1", "")
                                If InStr(1, Options2URL, "options=2") <> 0 Then
                                    cmdEditOptions2.NavigateUrl = Options2URL
                                Else
                                    cmdEditOptions2.NavigateUrl = cmdEditContent1.NavigateUrl + "&options=2"
                                End If

                                If Options2IMG = Nothing Then
                                    cmdEditOptions2.Text = "<img  src=""" & glbPath & "images/view.gif"" alt=""*"" style=""border-width:0px;"">"
                                Else
                                    cmdEditOptions2.Text = Options2IMG
                                End If
                                If OptionsText2 = Nothing Then
                                    cmdEditOptions2.ToolTip = GetLanguage("title_ShowParam")
                                    cmdEditOptions2.Attributes.Add("onmouseover", ReturnToolTip(GetLanguage("title_ShowParam")))
                                Else
                                    cmdEditOptions2.ToolTip = OptionsText2
                                    cmdEditOptions2.Attributes.Add("onmouseover", ReturnToolTip(OptionsText2))
                                End If

                                cmdEditOptions2.Visible = True
                                CellOptions2.Visible = True
                                rowAdmin1.Visible = True
                            End If


                        End If
                        _Setting = PortalSettings.GetSiteSettings(_portalSettings.PortalId)
                        If IsAdminTab() Then

                            GetMenu()


                            If _Setting("admincontainerTitleHeaderClass") <> "" Then
                                TableTitle1.Text = _Setting("admincontainerTitleHeaderClass")
                            End If


                            If _Setting("admincontainerTitleCSSClass") <> "" Then
                                lblEditModuleTitle.CssClass = _Setting("admincontainerTitleCSSClass")
                            End If

                            If _Setting("adminTitleContainer") <> "" Then
                                Dim arrContainer As Array = SplitContainer(_Setting("adminTitleContainer"), _portalSettings.UploadDirectory, IIf(_Setting("admincontainerAlignment") <> "", _Setting("admincontainerAlignment"), ""), IIf(_Setting("admincontainerColor") <> "", _Setting("admincontainerColor"), ""), IIf(_Setting("admincontainerBorder") <> "", _Setting("admincontainerBorder"), ""))
                                Titlebefore.Text = arrContainer(0)
                                Titlebefore.Visible = True
                                Titleafter.Text = arrContainer(1)
                                Titleafter.Visible = True
                            End If
                        Else

                            If _Setting("editcontainerTitleHeaderClass") <> "" Then
                                TableTitle1.Text = _Setting("editcontainerTitleHeaderClass")
                            End If

                            If _Setting("editcontainerTitleCSSClass") <> "" Then
                                lblEditModuleTitle.CssClass = _Setting("editcontainerTitleCSSClass")
                            End If
                            If _Setting("editTitleContainer") <> "" Then
                                Dim arrContainer As Array = SplitContainer(_Setting("editTitleContainer"), _portalSettings.UploadDirectory, IIf(_Setting("editcontainerAlignment") <> "", _Setting("editcontainerAlignment"), ""), IIf(_Setting("editcontainerColor") <> "", _Setting("editcontainerColor"), ""), IIf(_Setting("editcontainerBorder") <> "", _Setting("editcontainerBorder"), ""))
                                Titlebefore.Text = arrContainer(0)
                                Titlebefore.Visible = True
                                Titleafter.Text = arrContainer(1)
                                Titleafter.Visible = True
                            End If
                        End If
                    End If
                End If

            End If

            If Not TitleVisible Then
                pnlModuleTitle.Visible = False
                pnlAdminTitle.Visible = False
            End If


            modifier.ID = ""
            modifierC.ID = ""
            param.ID = ""
            haut.ID = ""
            bas.ID = ""
            top.ID = ""
            bottom.ID = ""
            gauche.ID = ""
            droite.ID = ""
            purger.ID = ""
            rowAdmin1.ID = ""
            rowDisplay.ID = ""

        End Sub




        Private Sub cmdDisplayModule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDisplayModule.Click

            ' Obtain reference to parent portal module
            Dim portalModule As PortalModuleControl = CType(Me.Parent, PortalModuleControl)

            Dim pnlModuleContent As PlaceHolder = FindControlRecursive(sender, "pnlModuleContent")

            If Not pnlModuleContent Is Nothing Then
                Dim objModuleVisible As HttpCookie = New HttpCookie("_Module" & portalModule.ModuleId.ToString & "_Visible")

                If pnlModuleContent.Visible = True Then
                    pnlModuleContent.Visible = False
                    styleplusminus = "background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -407px;"

                    cmdDisplayModule.ToolTip = GetLanguage("title_Inc")

                    objModuleVisible.Value = "false"
                Else
                    pnlModuleContent.Visible = True
                    styleplusminus = "background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -377px;"

                    cmdDisplayModule.ToolTip = GetLanguage("title_Dec")

                    objModuleVisible.Value = "true"
                End If

                objModuleVisible.Expires = DateTime.MaxValue ' never expires
                Response.AppendCookie(objModuleVisible)
            End If
        End Sub

        Private Sub ModuleResetCache()
            Dim portalModule As PortalModuleControl = CType(Me.Parent, PortalModuleControl)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' so remove the from cache
            ClearModuleCache(portalModule.ModuleId)
            ' Redirect to the same page to pick up changes
            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, ""), True)

        End Sub


        Private Sub ModuleResetCache_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModuleRefresh.Click
            ModuleResetCache()
        End Sub

        Private Sub deleteModule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmddelete.Click
            DeleteModule()
        End Sub

        Private Sub DeleteModule()
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objAdmin As New AdminDB()
            Dim portalModule As PortalModuleControl = CType(Me.Parent, PortalModuleControl)
            objAdmin.DeleteModule(portalModule.ModuleConfiguration.ModuleId)
            objAdmin.UpdateTabModuleOrder(tabId)
            ClearTabCache(tabId)
            ' Redirect to the same page to pick up changes
            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, ""), True)
        End Sub


        Private Sub ModuleLeftRight(ByVal CommandName As String)

            Dim PaneName As String = "ContentPane"
            ' Obtain reference to parent portal module
            Dim portalModule As PortalModuleControl = CType(Me.Parent, PortalModuleControl)

            Select Case CommandName.ToLower
                Case "top"
                    Select Case portalModule.ModuleConfiguration.PaneName.ToLower
                        Case "bottompane"
                            PaneName = "ContentPane"
                        Case Else
                            PaneName = "TopPane"
                    End Select


                Case "bottom"
                    Select Case portalModule.ModuleConfiguration.PaneName.ToLower
                        Case "toppane"
                            PaneName = "ContentPane"
                        Case Else
                            PaneName = "BottomPane"
                    End Select
                Case "left"
                    Select Case portalModule.ModuleConfiguration.PaneName.ToLower
                        Case "contentpane"
                            PaneName = "LeftPane"
                        Case "rightpane"
                            PaneName = "ContentPane"
                    End Select
                Case "right"
                    Select Case portalModule.ModuleConfiguration.PaneName.ToLower
                        Case "leftpane"
                            PaneName = "ContentPane"
                        Case "contentpane"
                            PaneName = "RightPane"
                    End Select
            End Select

            Dim objAdmin As New AdminDB()

            objAdmin.UpdateModuleOrder(portalModule.ModuleConfiguration.ModuleId, portalModule.ModuleConfiguration.ModuleOrder - 1, PaneName)
            objAdmin.UpdateTabModuleOrder(tabId)

            ' Redirect to the same page to pick up changes
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' reset the tab 
            ClearTabCache(_portalSettings.ActiveTab.TabId)

            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, ""), True)






        End Sub


        Private Sub ModuleLeftRight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModuleLeft.Click, cmdModuleRight.Click, cmdModuleTop.Click, cmdModuleBottom.Click
            ModuleLeftRight(CType(sender, LinkButton).CommandName.ToLower)
        End Sub

        Private Sub ModuleUpDown(ByVal CommandName As String)

            ' Obtain reference to parent portal module
            Dim portalModule As PortalModuleControl = CType(Me.Parent, PortalModuleControl)

            Dim objAdmin As New AdminDB()
            If portalModule.ModuleConfiguration.Language = "" Then
                Select Case CommandName
                    Case "up"
                        If portalModule.ModuleConfiguration.ModuleOrder = 1 Then
                            objAdmin.UpdateModuleOrder(portalModule.ModuleConfiguration.ModuleId, portalModule.ModuleConfiguration.ModuleOrder - 4, portalModule.ModuleConfiguration.PaneName)
                        Else
                            objAdmin.UpdateModuleOrder(portalModule.ModuleConfiguration.ModuleId, portalModule.ModuleConfiguration.ModuleOrder - 2, portalModule.ModuleConfiguration.PaneName)
                        End If
                    Case "down"
                        If portalModule.ModuleConfiguration.ModuleOrder = -3 Then
                            objAdmin.UpdateModuleOrder(portalModule.ModuleConfiguration.ModuleId, portalModule.ModuleConfiguration.ModuleOrder + 4, portalModule.ModuleConfiguration.PaneName)
                        Else
                            objAdmin.UpdateModuleOrder(portalModule.ModuleConfiguration.ModuleId, portalModule.ModuleConfiguration.ModuleOrder + 2, portalModule.ModuleConfiguration.PaneName)
                        End If
                End Select
            Else
                Select Case CommandName
                    Case "up"
                        objAdmin.UpdateModuleOrder(portalModule.ModuleConfiguration.ModuleId, portalModule.ModuleConfiguration.ModuleOrder - 3, portalModule.ModuleConfiguration.PaneName)
                    Case "down"
                        objAdmin.UpdateModuleOrder(portalModule.ModuleConfiguration.ModuleId, portalModule.ModuleConfiguration.ModuleOrder + 3, portalModule.ModuleConfiguration.PaneName)
                End Select
                objAdmin.UpdateTabModuleOrder(tabId)
            End If

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' reset the tab cashe
            ClearTabCache(_portalSettings.ActiveTab.TabId)

            ' Redirect to the same page to pick up changes
            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, ""), True)

        End Sub

        Private Sub ModuleUpDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModuleUp.Click, cmdModuleDown.Click
            ModuleUpDown(CType(sender, LinkButton).CommandName.ToLower)
        End Sub

        Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)

            If ctlMenu.Visible Then rowAdmin1.Visible = False

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Not IsNothing(DisplayHelp) Then
                If IsAdminTab() Or (Not Request.Params("def") Is Nothing) Or (Not Request.Params("edit") Is Nothing) Then
                    help1.ToolTip = GetLanguage("title_ShowInfo")
                    help1.Visible = True
                    help1.NavigateUrl = "javascript:var m = window.open('" + glbHTTP() + "admin/tabs/help.aspx?help=" & DisplayHelp & "&TabId=" & _portalSettings.ActiveTab.TabId & "&L=" & GetLanguage("N") & "', 'help', 'width=640,height=400,left=100,top=100,titlebar=0,scrollbars=1,menubar=0,status=0,location=0,resizable=1');m.focus();"
                    cellhelp.Visible = True
                Else
                    help.ToolTip = GetLanguage("title_ShowInfo")
                    help.Visible = True
                    help.NavigateUrl = "javascript:var m = window.open('" + glbHTTP() + "admin/tabs/help.aspx?help=" & DisplayHelp & "&TabId=" & _portalSettings.ActiveTab.TabId & "&L=" & GetLanguage("N") & "', 'help', 'width=640,height=400,left=100,top=100,titlebar=0,scrollbars=1,menubar=0,status=0,location=0,resizable=1');m.focus();"
                    pnlHelp.Visible = True
                    pnlModuleTitle.Visible = True
                End If
            Else
                help.Visible = False
                pnlHelp.Visible = False
                help1.Visible = False
                cellhelp.Visible = False
            End If


            ' Obtain reference to parent portal module
            Dim portalModule As PortalModuleControl = CType(Me.Parent, PortalModuleControl)


            If Not IsNothing(portalModule.ModuleConfiguration) Then

                Dim _Setting As Hashtable
                Dim pnlafter As System.Web.UI.WebControls.Literal = Parent.FindControl("after")
                Dim pnlbefore As System.Web.UI.WebControls.Literal = Parent.FindControl("before")
                If Not pnlafter Is Nothing And Not pnlbefore Is Nothing Then
                    ' Inject Module wrapper here		 
                    If portalModule.ModuleConfiguration.PaneName <> "Edit" Then
                        _Setting = PortalSettings.GetModuleSettings(portalModule.ModuleId)
                        If _Setting("ModuleContainer") <> "" Then
                            Dim arrContainer As Array = SplitContainer(CType(_Setting("ModuleContainer"), String), _portalSettings.UploadDirectory, IIf(_Setting("containerAlignment") <> "", _Setting("containerAlignment"), ""), IIf(_Setting("containerColor") <> "", _Setting("containerColor"), ""), IIf(_Setting("containerBorder") <> "", _Setting("containerBorder"), ""))
                            pnlbefore.Text = arrContainer(0)
                            pnlbefore.Visible = True
                            pnlafter.Text = arrContainer(1)
                            pnlafter.Visible = True
                        End If
                    Else
                        _Setting = PortalSettings.GetSiteSettings(_portalSettings.PortalId)
                        If IsAdminTab() Then
                            If _Setting("adminModuleContainer") <> "" Then
                                Dim arrContainer As Array = SplitContainer(_Setting("adminModuleContainer"), _portalSettings.UploadDirectory, IIf(_Setting("admincontainerAlignment") <> "", _Setting("admincontainerAlignment"), ""), IIf(_Setting("admincontainerColor") <> "", _Setting("admincontainerColor"), ""), IIf(_Setting("admincontainerBorder") <> "", _Setting("admincontainerBorder"), ""))
                                pnlbefore.Text = arrContainer(0)
                                pnlbefore.Visible = True
                                pnlafter.Text = arrContainer(1)
                                pnlafter.Visible = True
                            End If
                        Else
                            If _Setting("editModuleContainer") <> "" Then
                                Dim arrContainer As Array = SplitContainer(_Setting("editModuleContainer"), _portalSettings.UploadDirectory, IIf(_Setting("editcontainerAlignment") <> "", _Setting("editcontainerAlignment"), ""), IIf(_Setting("editcontainerColor") <> "", _Setting("editcontainerColor"), ""), IIf(_Setting("editcontainerBorder") <> "", _Setting("editcontainerBorder"), ""))
                                pnlbefore.Text = arrContainer(0)
                                pnlbefore.Visible = True
                                pnlafter.Text = arrContainer(1)
                                pnlafter.Visible = True
                            End If
                        End If
                    End If
                End If
            End If

            MyBase.OnPreRender(e)

        End Sub

        Private Sub ctlMenu_MenuClick(ByVal ID As String) Handles ctlMenu.MenuClick
            Select Case ID
                Case 4
                    ModuleLeftRight("top")
                Case 5
                    ModuleUpDown("up")
                Case 6
                    ModuleUpDown("down")
                Case 7
                    ModuleLeftRight("left")
                Case 8
                    ModuleLeftRight("right")
                Case 9
                    ModuleLeftRight("bottom")
                Case 10
                    ModuleResetCache()
                Case 11
                    DeleteModule()
            End Select
        End Sub

        Private Function GetClientScriptURL(ByVal Action As String) As String
            Return Page.GetPostBackClientEvent(ctlMenu, Action)
        End Function

        Private Sub GetMenu()
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Then
                cmdAdmin.Visible = True
                cAdmin.Visible = True
                Dim TempKey As String = GetDBname() & GetLanguage("N") & _portalSettings.AdministratorRoleId.ToString & "adm" & "_portalSettings.Activetab.TabID" & PortalSecurity.IsSuperUser.ToString
                Dim context As HttpContext = HttpContext.Current
                Dim content As String = context.Cache(TempKey)
                If content Is Nothing Then
                    cmdAdmin.Text = BuildAdminMenu(PortalSecurity.IsSuperUser)
                End If
                If content <> "" Then
                    context.Cache.Insert(TempKey, content, CDp(_portalSettings.PortalId, _portalSettings.ActiveTab.TabId), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.Normal, Nothing)
                End If
                If Not content Is Nothing Then
                    cmdAdmin.Text = content.ToString()
                End If
            Else
                cmdAdmin.Visible = False
                cmdAdmin.Text = ""
                cAdmin.Visible = False
            End If
        End Sub
        Private Function BuildAdminMenu(ByVal Host As Boolean) As String
            ' put admin menu in
            ' show menu admin 
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim TempHost As StringBuilder = New System.Text.StringBuilder()
            Dim TempAdmin As StringBuilder = New System.Text.StringBuilder()

            Dim objAdmin As New AdminDB()
            Dim IsSSL As Boolean

            Dim result As SqlDataReader
            result = objAdmin.GetAdminModuleDefinitions(GetLanguage("N"))
            While result.Read
                ' idadmin or ishost
                IsSSL = Boolean.Parse(result("ssl").ToString) And _portalSettings.SSL
                If result("isadmin") Then
                    TempAdmin.Append("<a href=""" & FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, IsSSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "adminpage=" & result("ModuleDefID").ToString, "", "&amp;") & """ onmouseover=""this.T_WIDTH=210;return escape('" & RTESafe(result("Description").ToString) & "')"">")
                    TempAdmin.Append("<img  src=""" & glbPath & "images/")
                    TempAdmin.Append(result("AdminTabIcon").ToString)
                    TempAdmin.Append(""" alt=""" & result("FriendlyName") & """ style=""border-width:0px;"" /></a>")
                    TempAdmin.Append("&nbsp;")
                End If
                If Host Then
                    If result("ishost") Then
                        TempHost.Append("<a href=""" & FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, IsSSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "adminpage=" & result("ModuleDefID").ToString, "", "&amp;") & IIf(result("isadmin"), "&amp;hostpage=" & result("ModuleDefID"), "") & """ onmouseover=""this.T_WIDTH=210;return escape('" & RTESafe(result("Description").ToString) & "')"">")
                        TempHost.Append("<img  src=""" & glbPath & "images/")
                        TempHost.Append(result("AdminTabIcon").ToString)
                        TempHost.Append(""" alt=""" & result("FriendlyName") & """ style=""border-width:0px;"" /></a>")
                        TempHost.Append("&nbsp;")
                    End If
                End If
            End While
            result.Close()
            If Host Then
                Return TempAdmin.ToString & "|&nbsp;" & TempHost.ToString
            Else
                Return TempAdmin.ToString
            End If

        End Function



    End Class


	
	
End Namespace