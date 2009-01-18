Imports System.IO

Namespace DotNetZoom

    Public MustInherit Class altmenu
        Inherits System.Web.UI.UserControl

        Protected WithEvents lblBreadCrumbs As System.Web.UI.WebControls.Label

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

    ' process bread crumbs
	' modification au bread crumbs par rene boulard pour que l accueil figure toujours en premier

    Dim strBreadCrumbs as String = ""
    Dim intTab As Integer
	Dim TempId as integer
	Dim TempParentId as integer
	Dim DesktopTabs As ArrayList = portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N"))
	Dim AccueilTab As TabStripDetails = CType(DesktopTabs(0), TabStripDetails)
	
	' Modification pour mettre un menu alternatif apres les breadcrums
			if CType(_portalSettings.BreadCrumbs(0), TabStripDetails).TabId <> AccueilTab.TabId then
			TempId = CType(_portalSettings.BreadCrumbs(_portalSettings.BreadCrumbs.Count - 1), TabStripDetails).TabId
				If _portalSettings.ActiveTab.TabId = AccueilTab.TabId then
				' HighLight menu if active tab
                    strBreadCrumbs += "&nbsp;&raquo;&nbsp;<a href=""" & FormatFriendlyURL(AccueilTab.FriendlyTabName, AccueilTab.ssl, AccueilTab.ShowFriendly, AccueilTab.TabId.ToString) & """>" & "<span class=""activetab"">" & AccueilTab.TabName & "</span>" & "</a>"
				else
                    strBreadCrumbs += "&nbsp;&raquo;&nbsp;<a href=""" & FormatFriendlyURL(AccueilTab.FriendlyTabName, AccueilTab.ssl, AccueilTab.ShowFriendly, AccueilTab.TabId.ToString) & """>" & AccueilTab.TabName & "</a>"
				end if
			Else
			TempId = -1
			end if
			For intTab = 0 To _portalSettings.BreadCrumbs.Count - 1
			ObjTab = CType(_portalSettings.BreadCrumbs(intTab), TabStripDetails)
			if _portalSettings.ActiveTab.TabId = ObjTab.TabId then
                    strBreadCrumbs += "&nbsp;&raquo;&nbsp;<a href=""" & FormatFriendlyURL(objTab.FriendlyTabName, objtab.ssl, objTab.ShowFriendly, objTab.Tabid.ToString) & """>" & "<span class=""activetab"">" & objTab.TabName & "</span>" & "</a>"
			else
                    strBreadCrumbs += "&nbsp;&raquo;&nbsp;<a href=""" & FormatFriendlyURL(objtab.FriendlyTabName, objtab.ssl, objTab.ShowFriendly, objTab.Tabid.ToString) & """>" & objTab.TabName & "</a>"
			end if
			Next
						
			' Build list of tabs to be shown to user
 		    Dim i As Integer
			Dim firstTab as boolean
			FirstTab = true
	        	
			For i = 0 To DesktopTabs.Count - 1
            Dim tab As TabStripDetails = CType(DesktopTabs(i), TabStripDetails)
			If Tab.TabId = TempId then TempParentId = Tab.ParentId 
                	If tab.IsVisible Then
                    		If (PortalSecurity.IsInRoles(tab.AuthorizedRoles) = True) and (Tab.parentId = TempId ) and (Tab.TabId <> TempId) and (tab.TabId <> AccueilTab.TabId) Then
					If firstTab then
                    			FirstTab = False
					strBreadCrumbs += "&nbsp;&nbsp;|&nbsp;"
					else
					strBreadCrumbs += "|"
					End If
                        strBreadCrumbs += "&nbsp;<a href=""" & FormatFriendlyURL(tab.FriendlyTabName, tab.ssl, tab.ShowFriendly, tab.TabId.ToString) & """>" & tab.TabName & "</a>&nbsp;"
					End If
                	End If
            Next i

			If FirstTab then
			For i = 0 To DesktopTabs.Count - 1
		        Dim tab As TabStripDetails = CType(DesktopTabs(i), TabStripDetails)
                	If tab.IsVisible Then
                    	If (PortalSecurity.IsInRoles(tab.AuthorizedRoles) = True) and (Tab.parentId = TempParentId ) and (Tab.TabId <> TempId) and (tab.TabId <> AccueilTab.TabId) Then
 				If firstTab then
                    		FirstTab = False
				strBreadCrumbs += "&nbsp;&nbsp;|&nbsp;"
				else
				strBreadCrumbs += "|"
				End If
                            strBreadCrumbs += "&nbsp;<a href=""" & FormatFriendlyURL(tab.FriendlyTabName, tab.ssl, tab.ShowFriendly, tab.TabId.ToString) & """>" & tab.TabName & "</a>&nbsp;"
			End If
                	End If
            		Next i
			end if
			
			lblBreadCrumbs.Text = strBreadCrumbs

            End Sub



    End Class

End Namespace