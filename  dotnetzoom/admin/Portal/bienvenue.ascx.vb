
Namespace DotNetZoom

    Public Class Bienvenue
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents lblbienvenue As System.Web.UI.WebControls.Label
		Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
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
   			Title1.DisplayTitle = getlanguage("registration_ok")
			Title1.DisplayHelp = "DisplayHelp_Welcome"
			cmdCancel.Text = getlanguage("return")

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim StrMessage as string 
			Dim Admin As New AdminDB()
             Select Case _portalSettings.UserRegistration
                        	               	

              Case 1 ' private
              StrMessage =  Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "Welcome_Accepted_Private")
              Case 2 ' public
              StrMessage =  Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "Welcome_Accepted_public")
              Case 3 ' verified
              StrMessage =  Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "Welcome_Accepted_Verified")
              End Select
			  StrMessage = ProcessLanguage(StrMessage, page)
              lblbienvenue.Text = Replace(lblbienvenue.Text, "{BienvenuePortail}", StrMessage)
            
        End Sub

		Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
           ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, ""), True)
        End Sub

		
    End Class

End Namespace