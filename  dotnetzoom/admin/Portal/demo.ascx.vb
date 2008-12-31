Imports System.IO


Namespace DotNetZoom

    Public Class demo
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents lbldemo As System.Web.UI.WebControls.Literal

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
   			Title1.DisplayTitle = getlanguage("create_portal")
			Title1.DisplayHelp = "DisplayHelp_Demo"
			Dim lblpreview As String
		    Dim lblGUID As String

		    Dim strFolder As String
            Dim strFileName As String
			Dim Admin As New AdminDB()			
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim strAuthorizedRoles As String = ""
			Dim AllowDemo As String = "N"
			If  portalSettings.GetSiteSettings(_portalSettings.PortalID)("DemoSignup") <> nothing then
			AllowDemo = CType( portalSettings.GetSiteSettings(_portalSettings.PortalID)("DemoSignup"), String) 
			end if
			If  portalSettings.GetSiteSettings(_portalSettings.PortalID)("DemoAuthRole") <> nothing then
			strAuthorizedRoles = CType( portalSettings.GetSiteSettings(_portalSettings.PortalID)("DemoAuthRole"), String) 
			end if
            ' ensure portal signup is allowed

            If  (portalSettings.GetHostSettings("DemoSignup") <> "Y") or (AllowDemo = "N") or ( not PortalSecurity.IsInRoles(strAuthorizedRoles))Then
                Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Access Denied", True)
            End If
				lblGUID = Guid.NewGuid().ToString()
			    System.Web.HttpContext.Current.Session("GUID") = lblGUID
				Dim TempString As String = Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "Demo_Portal_Info")
				If TempString <> "" then
				lbldemo.Text = TempString
				end if
				If  portalSettings.GetSiteSettings(_portalSettings.PortalID)(GetLanguage("N") & "_DemoDirectives") <> nothing then
				lbldemo.Text = Replace(lbldemo.Text, "{Directives}" , CType( portalSettings.GetSiteSettings(_portalSettings.PortalID)(GetLanguage("N") & "_DemoDirectives"), String))
				else
				If  portalSettings.GetSiteSettings(_portalSettings.PortalID)("DemoDirectives") <> nothing 
				lbldemo.Text = Replace(lbldemo.Text, "{Directives}" , CType( portalSettings.GetSiteSettings(_portalSettings.PortalID)("DemoDirectives"), String))
				Else
				lbldemo.Text = Replace(lbldemo.Text, "{Directives}" , GetLanguage("Demo_Create_Portal"))
				end if
				end if
                lbldemo.Text = Replace(lbldemo.Text, "{PortalName}", _portalSettings.PortalName)
                lbldemo.Text = ProcessLanguage(lbldemo.Text, Page)
            lbldemo.Text = Replace(lbldemo.Text, "{DomainName}", GetDomainName(Request))
            lbldemo.Text = Replace(lbldemo.Text, "{language}", GetLanguage("N"))
                lbldemo.Text = Replace(lbldemo.Text, "{space}", "20 meg")
                lbldemo.Text = Replace(lbldemo.Text, "{days}" , portalSettings.GetHostSettings("DemoPeriod"))
		
                lbldemo.Text = Replace(lbldemo.Text, "{lblGUID}" , lblGUID)    



				lblpreview = ""
                strFolder = Request.MapPath(glbTemplatesDirectory)
		        If System.IO.Directory.Exists(strFolder) Then
                    Dim DirEntries As String() = System.IO.Directory.GetDirectories(strFolder)
					Dim lWidth As Integer
		            Dim lHeight As Integer
            		Dim mImage As System.Drawing.Image
		            Dim strServerPath As String = GetAbsoluteServerPath(Request)

					
 					
					
                    For Each strFileName In DirEntries
					Dim dir As New DirectoryInfo(strFileName)
                	If System.IO.File.Exists(strServerPath & "templates\" & dir.name & "\template.jpg") Then
                	mImage = System.Drawing.Image.FromFile(strServerPath & "templates\" & dir.name & "\template.jpg")
                	lWidth = mImage.Width
                	lHeight = mImage.Height
					lblpreview = lblpreview & "<a href=""" & "templates/" & dir.name & "/template.jpg" & """ onmouseover=""" & ReturnGalleryToolTip("/templates/" & dir.name & "/template.jpg", lwidth, lheight) & """ title=""Thème de création d'un site web"" target=""_blank"">" & dir.name & "</a>&nbsp;"
					end if
        			Next
                End If
				lbldemo.Text = Replace(lbldemo.Text, "{lblpreview}" , lblpreview)    
        End Sub

    End Class

End Namespace