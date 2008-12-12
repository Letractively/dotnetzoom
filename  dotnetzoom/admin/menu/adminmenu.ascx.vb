Imports System.Data.SqlClient
Imports System.Data
Imports System.text
Namespace DotNetZoom

    Public MustInherit Class adminmenu
		Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents lblAdminMenu As System.Web.UI.WebControls.Literal
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
        ' The Page_Load server event handler on this page is used
        ' to populate the role information for the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Title1.DisplayHelp = "DisplayHelp_AdminMenu"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Verify that the current user has access to edit this module
            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = False Then
                Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Access Denied", True)
            End If
		   lblAdminMenu.text = GetMenu(PortalSecurity.IsSuperUser, TabID)
		   lblAdminMenu.visible = true
        End Sub
    Private Function GetMenu(ByVal Host As Boolean, ByVal TabID As Integer) As String

            ' Grab reference to the applicationstate object
			' Need to change Forum Cashing
			Dim TempKey as String = GetDBName & TabID.ToString &  "AdminMenu_" & GetLanguage("N") & "_" & Host.ToString
			Dim context As HttpContext = HttpContext.Current
			Dim Menu As String = CType(Context.Cache(TempKey), String)
			
            If Menu Is Nothing Then
            Menu = MakeAdminMenu(Host)
			Context.Cache.Insert(TempKey, Menu, DotNetZoom.CDp(-1), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), Caching.CacheItemPriority.low, nothing)
            End If
            Return Menu
  End Function	
  
  Private Function MakeAdminMenu(ByVal Host As Boolean) As String

			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)


			Dim TempAdmin As StringBuilder = New System.Text.StringBuilder()
			Dim TempHost As StringBuilder = New System.Text.StringBuilder()
			If Host then
			TempHost.append("<tr><td colspan=""3""><hr><span class=""Head"">" & GetLanguage("admin_StartHost") & "</span><hr></td></tr>")
			TempAdmin.append("<tr><td colspan=""3""><hr><span class=""Head"">" & GetLanguage("admin_StartPortal") & "</span><hr></td></tr>")
			end if

			Dim objAdmin As New DotNetZoom.AdminDB()

			Dim result As SqlDataReader
			result = objAdmin.GetAdminModuleDefinitions(GetLanguage("N"))
			While Result.Read
			' idadmin or ishost
			if result("isadmin") and result("AdminOrder") <> 1 then
			TempAdmin.append("<tr><td>")
			TempAdmin.append("<a href=""" & FormatFriendlyURL(_PortalSettings.activetab.FriendlyTabName, _PortalSettings.activetab.ShowFriendly, _PortalSettings.activetab.Tabid.ToString, "adminpage=" & result("ModuleDefID").ToString) & """ title=""" & result("FriendlyName").ToString & """>")
                    TempAdmin.Append("<img  src=""images/")
			TempAdmin.Append(result("EditModuleIcon").ToString)
                    TempAdmin.Append(""" alt=""" & result("FriendlyName") & """ title=""" & result("FriendlyName") & """ style=""border-width:0px;"" /></a></td><td style=""white-space: nowrap;"">")
			TempAdmin.append("<a href=""" & FormatFriendlyURL(_PortalSettings.activetab.FriendlyTabName, _PortalSettings.activetab.ShowFriendly, _PortalSettings.activetab.Tabid.ToString, "adminpage=" & result("ModuleDefID").ToString) & """ title=""" & result("FriendlyName") & """>" & result("FriendlyName") & "</a>")
			TempAdmin.append("</td><td><p class=""normal"">" & result("Description") & "</p></td></tr>")
			end if
			If Host then
			if result("ishost") then
			TempHost.append("<tr><td>")
			TempHost.append("<a href=""" & FormatFriendlyURL(_PortalSettings.activetab.FriendlyTabName, _PortalSettings.activetab.ShowFriendly, _PortalSettings.activetab.Tabid.ToString, "adminpage=" & result("ModuleDefID").ToString) & iif(result("isadmin"), "&hostpage=" & result("ModuleDefID"),"") & """ title=""" & result("FriendlyName").ToString & """>")
                        TempHost.Append("<img  src=""images/")
			TempHost.Append(result("EditModuleIcon").ToString)
                        TempHost.Append(""" alt=""" & result("FriendlyName") & """ title=""" & result("FriendlyName") & """ style=""border-width:0px;"" /></a></td><td style=""white-space: nowrap;"">")
			TempHost.append("<a href=""" & FormatFriendlyURL(_PortalSettings.activetab.FriendlyTabName, _PortalSettings.activetab.ShowFriendly, _PortalSettings.activetab.Tabid.ToString, "adminpage=" & result("ModuleDefID").ToString) & iif(result("isadmin"), "&hostpage=" & result("ModuleDefID"),"") &  """ title=""" & result("FriendlyName") & """>" & result("FriendlyName") & "</a>")
			TempHost.append("</td><td><p class=""normal"">" & result("Description") & "</p></td></tr>")
			end if
			end if
			End While
			result.Close()
			Return "<table cellspacing=""3"" cellpadding=""3""  border=""0""><tbody>" & TempAdmin.ToString & TempHost.ToString & "</tbody></table>"
  End Function
End Class

	
End Namespace