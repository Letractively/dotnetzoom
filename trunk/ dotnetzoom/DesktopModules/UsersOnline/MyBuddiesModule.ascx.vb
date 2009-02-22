Imports System.Web
Imports System.Data.SqlClient

Namespace DotNetZoom

    Public MustInherit Class MyBuddiesModule
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents lblWarningMessage As System.Web.UI.WebControls.Label
        Protected WithEvents rptOnlineBuddies As System.Web.UI.WebControls.Repeater
        Protected WithEvents rptOfflineBuddies As System.Web.UI.WebControls.Repeater
        Protected WithEvents lstOnlineBuddies As System.Web.UI.WebControls.DataList
        Protected WithEvents lstOfflineBuddies As System.Web.UI.WebControls.DataList
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
            If Request.Params("def") <> "" Then
                Title1.DisplayTitle = GetLanguage("MyBuddiesModule")
            End If
            If (Request.IsAuthenticated) Then
				Title1.DisplayHelp = "DisplayHelp_MyBuddiesModule"
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                Dim objBuddies As BuddiesDB = New BuddiesDB

                Dim myTable As DataTable = objBuddies.GetBuddiesListDataTable(_portalSettings.PortalId, Int32.Parse(Page.User.Identity.Name))

                Dim myOnlineView As DataView = New DataView(myTable)
                myOnlineView.RowFilter = "SessionID is not null"

                lstOnlineBuddies.DataSource = myOnlineView
                lstOnlineBuddies.DataBind()

                Dim myOfflineView As DataView = New DataView(myTable)
                myOfflineView.RowFilter = "SessionID is null"

                lstOfflineBuddies.DataSource = myOfflineView
                lstOfflineBuddies.DataBind()

                If (lstOnlineBuddies.Items.Count = 0 And lstOfflineBuddies.Items.Count = 0) Then
                    lstOfflineBuddies.Visible = False
                    lstOnlineBuddies.Visible = False

                    lblWarningMessage.Text = GetLanguage("UO_no_pref")
                End If
            Else
                lblWarningMessage.Text = GetLanguage("UO_no_connect")
				Title1.DisplayHelp = "DisplayHelp_NeedToLogin"
            End If
        End Sub

        Private Sub lstOnlineBuddies_ItemDataBound(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles lstOnlineBuddies.ItemDataBound
            Dim lblUserNumber As System.Web.UI.WebControls.Label = e.Item.FindControl("lblUserNumber")

            If Not (lblUserNumber Is Nothing) Then
                Dim userNumber As String = (e.Item.ItemIndex + 1).ToString()

                If (userNumber.Length = 1) Then
                    userNumber = "0" & userNumber
                End If

                lblUserNumber.Text = userNumber
            End If
        End Sub

        Private Sub lstOfflineBuddies_ItemDataBound(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles lstOfflineBuddies.ItemDataBound
            Dim lblUserNumber As System.Web.UI.WebControls.Label = e.Item.FindControl("lblUserNumber")

            If Not (lblUserNumber Is Nothing) Then
                Dim userNumber As String = (e.Item.ItemIndex + 1).ToString()

                If (userNumber.Length = 1) Then
                    userNumber = "0" & userNumber
                End If

                lblUserNumber.Text = userNumber
            End If
        End Sub

        Protected Function GetUserInfoLink(ByVal userID As String) As String
            Return GetFullDocument() & "?TabID=" & TabId.ToString() + "&def=UserInfo&UserID=" + userID
        End Function

        Protected Function GetUserInfoTooltip(ByVal userName As String) As String
            Return replace(GetLanguage("UO_see_user"), "{username}", userName)
        End Function

    End Class

End Namespace