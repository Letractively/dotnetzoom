
Namespace DotNetZoom

	Public MustInherit Class TAGFileManagerModule
		Inherits DotNetZoom.PortalModuleControl
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
		' The Page_Load server event handler on this user control is used
		' to populate the current roles settings from the configuration system
		'
		'*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Title1.EditText = GetLanguage("upload")
            Title1.EditIMG = "<img  src=""admin/advfileManager/images/upload.gif"" alt=""*"" style=""border-width:0px;"">"
            Title1.DisplayHelp = "DisplayHelp_TAGFileManagerModule"
        End Sub

	End Class

End Namespace