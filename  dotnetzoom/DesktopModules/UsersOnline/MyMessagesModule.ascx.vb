Imports System.Web
Imports System.Data.SqlClient

Namespace DotNetZoom

    Public MustInherit Class MyMessagesModule
        Inherits DotNetZoom.PortalModuleControl
        Protected WithEvents lblWarningMessage As System.Web.UI.WebControls.Label
        Protected WithEvents lblReadCount As System.Web.UI.WebControls.Label
        Protected WithEvents lblUnreadCount As System.Web.UI.WebControls.Label
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

            Dim objMessagesDB As MessagesDB = New MessagesDB

            Dim unreadCount As Integer = 0
            Dim readCount As Integer = 0

            objMessagesDB.GetMessageCount((New Utility).GetUserID(), unreadCount, readCount)

            lblUnreadCount.Text = unreadCount.ToString()

            If unreadCount > 0 Then
                lblUnreadCount.Style.Add("text-decoration", "blink")
                ' "text-decoration: blink"
            Else
                lblUnreadCount.Style.Clear()
            End If
            lblReadCount.Text = readCount.ToString()

        End Sub

        Protected Function GetMessageUrl() As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&amp;def=PrivateMessages"
        End Function

    End Class

End Namespace