Imports System.Web
Imports System.Data.SqlClient

Namespace DotNetZoom

    Public MustInherit Class UsersOnlineModulesmall
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents lblGuestCountsmall As System.Web.UI.WebControls.Literal
        Protected WithEvents lblMemberCountsmall As System.Web.UI.WebControls.Literal
        Protected WithEvents lblTotalCountsmall As System.Web.UI.WebControls.Literal
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder


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
            'Put user code to initialize the page here
                Dim objUsersOnline As UsersOnlineDB = New UsersOnlineDB
                Dim GuestCount As Integer = 0
                Dim MemberCount As Integer = 0
                Dim UserCount As Integer = 0
                Dim NewToday As Integer = 0
                Dim NewYesterday As Integer = 0
                Dim LatestUsername As String = ""
                Dim LatestUserID As Integer = 0
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                objUsersOnline.GetUserCounts(_portalSettings.PortalId, GuestCount, MemberCount, UserCount, NewToday, NewYesterday, LatestUsername, LatestUserID)
                lblGuestCountsmall.Text = GuestCount
                lblMemberCountsmall.Text = MemberCount
                lblTotalCountsmall.Text = GuestCount + MemberCount
         
        End Sub

   End Class

End Namespace