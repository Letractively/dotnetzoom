Namespace DotNetZoom

    Public MustInherit Class solpartmenu
	       Inherits DotNetZoom.PortalModuleControl

		   Protected WithEvents ctlMenu As SolpartWebControls.SolpartMenu

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
            ' Build list of tabs to be shown to user
            Dim authorizedTabs As New ArrayList()
            Dim addedTabs As Integer = 0
			Dim DesktopTabs As ArrayList = portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N"))
            Dim i As Integer
            For i = 0 To DesktopTabs.Count - 1
                Dim tab As TabStripDetails = CType(DesktopTabs(i), TabStripDetails)
                If tab.IsVisible Then
                    If PortalSecurity.IsInRoles(tab.AuthorizedRoles) = True Then
                        authorizedTabs.Add(tab)
                        addedTabs += 1
                    End If
                End If
            Next i

            ' generate dynamic menu
            ctlMenu.SystemImagesPath = glbSiteDirectory()
            ctlMenu.IconImagesPath = _portalSettings.UploadDirectory
            ctlMenu.ArrowImage = "breadcrumb.gif"
            ctlMenu.RootArrow = True
            ctlMenu.RootArrowImage = "menu_down.gif"
            ctlMenu.SystemScriptPath = IIf(Request.ApplicationPath = "/", "", Request.ApplicationPath) & "/controls/SolpartMenu/"

            Dim objTab As TabStripDetails
            Dim objMenuItem As Solpart.WebControls.SPMenuItemNode

            For Each objTab In authorizedTabs
                If objTab.ParentId = -1 Then ' root menu
                    If objTab.DisableLink Then
                        objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(objTab.TabId, objTab.TabName, ""))
                    Else
                        objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(objTab.TabId, objTab.TabName, FormatFriendlyURL(objTab.FriendlyTabName, objTab.ssl, objTab.ShowFriendly, objTab.TabId.ToString)))
                    End If

                    'add image next to selected item
                    If objTab.TabId = CType(_portalSettings.BreadCrumbs(0), TabStripDetails).TabId Then
                        objMenuItem.LeftHTML = "<img alt=""»"" border=""0"" src=""" & ctlMenu.SystemImagesPath & "breadcrumb.gif"">"
                    End If
                    'Has Children now handled by rootmenuarrow
                Else
                    Try
                        If objTab.DisableLink Then
                            objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(objTab.ParentId, objTab.TabId, "&nbsp;" & objTab.TabName, ""))
                        Else
                            objMenuItem = New Solpart.WebControls.SPMenuItemNode(ctlMenu.AddMenuItem(objTab.ParentId, objTab.TabId, "&nbsp;" & objTab.TabName, FormatFriendlyURL(objTab.FriendlyTabName, objTab.ssl, objTab.ShowFriendly, objTab.TabId.ToString)))
                        End If
                    Catch
                        ' throws exception if the parent tab has not been loaded ( may be related to user role security not allowing access to a parent tab )
                        objMenuItem = Nothing
                    End Try
                End If

                ' menu icon
                If Not objMenuItem Is Nothing Then
                        If objTab.IconFile <> "" Then
                            objMenuItem.Image = objTab.IconFile
                        End If
                End If
            Next
	 End Sub
    End Class

End Namespace