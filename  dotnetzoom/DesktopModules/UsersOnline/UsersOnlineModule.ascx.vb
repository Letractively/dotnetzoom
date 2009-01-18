Imports System.Web
Imports System.Data.SqlClient

Namespace DotNetZoom

    Public MustInherit Class UsersOnlineModule
        Inherits DotNetZoom.PortalModuleControl
        Protected WithEvents lblGuestCount As System.Web.UI.WebControls.Literal
        Protected WithEvents lblMemberCount As System.Web.UI.WebControls.Literal
        Protected WithEvents pnlGuestMessage As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlMemberMessage As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents hypUser As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblMembers As System.Web.UI.WebControls.Literal
        Protected WithEvents lblCount As System.Web.UI.WebControls.Literal
        Protected WithEvents hypCount As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblTotalCount As System.Web.UI.WebControls.Literal
        Protected WithEvents lblNewToday As System.Web.UI.WebControls.Literal
        Protected WithEvents lblNewYesterday As System.Web.UI.WebControls.Literal
        Protected WithEvents lblUserCount As System.Web.UI.WebControls.Literal
        Protected WithEvents lblLatestUsername As System.Web.UI.WebControls.Literal
        Protected WithEvents rptOnlineNow As System.Web.UI.WebControls.Repeater
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lnkLatestUsername As System.Web.UI.WebControls.HyperLink

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
                Dim objSession As SessionTrackerDB = New SessionTrackerDB
                Dim objUser As UsersDB = New UsersDB

                Dim GuestCount As Integer = 0
                Dim MemberCount As Integer = 0
                Dim UserCount As Integer = 0
                Dim NewToday As Integer = 0
                Dim NewYesterday As Integer = 0
                Dim LatestUsername As String = ""
                Dim LatestUserID As Integer = 0

                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                objUsersOnline.GetUserCounts(_portalSettings.PortalId, GuestCount, MemberCount, UserCount, NewToday, NewYesterday, LatestUsername, LatestUserID)

                If (LatestUsername.Length > 0) Then
                    lnkLatestUsername.Text = LatestUsername
                    lnkLatestUsername.NavigateUrl = GetUserInfoLink(LatestUserID.ToString())
                    lnkLatestUsername.ToolTip = GetUserInfoTooltip(LatestUsername)
                End If

                lblGuestCount.Text = GuestCount
                lblMemberCount.Text = MemberCount
                lblTotalCount.Text = GuestCount + MemberCount
                lblUserCount.Text = UserCount
                lblNewToday.Text = NewToday
                lblNewYesterday.Text = NewYesterday

                If Request.IsAuthenticated Then

                    Dim myReader As SqlDataReader = objUsersOnline.GetOnlineUsers(_portalSettings.PortalId)
                    rptOnlineNow.DataSource = myReader
                    rptOnlineNow.DataBind()

                    myReader.Close()
                End If
           
        End Sub

        Private Sub rptOnlineNow_ItemDataBound(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptOnlineNow.ItemDataBound
            Dim lblUserNumber As System.Web.UI.WebControls.Literal = e.Item.FindControl("lblUserNumber")

            If Not (lblUserNumber Is Nothing) Then
                Dim userNumber As String = (e.Item.ItemIndex + 1).ToString()

                If (userNumber.Length = 1) Then
                    userNumber = "0" & userNumber
                End If

                lblUserNumber.Text = userNumber
            End If
        End Sub

        Protected Function GetUserInfoLink(ByVal userID As String) As String
            Return GetFullDocument() & "?edit=control&TabID=" & TabId.ToString() + "&def=UserInfo&UserID=" + userID
        End Function

        Protected Function GetUserInfoTooltip(ByVal userName As String) As String
           Return replace(GetLanguage("UO_see_user"), "{username}", userName)
        End Function

    End Class

End Namespace