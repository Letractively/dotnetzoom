Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient

Namespace DotNetZoom

    Public MustInherit Class UserInfoModule
        Inherits DotNetZoom.PortalModuleControl
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Protected WithEvents lblUsername As System.Web.UI.WebControls.Label
        Protected WithEvents lblFullName As System.Web.UI.WebControls.Label
        Protected WithEvents lblJoined As System.Web.UI.WebControls.Label
        Protected WithEvents lblLastVisited As System.Web.UI.WebControls.Label
        Protected WithEvents lblOnlineStatus As System.Web.UI.WebControls.Label
        Protected WithEvents lblBuddyList As System.Web.UI.WebControls.Label
        Protected WithEvents lblNoBuddiesFound As System.Web.UI.WebControls.Label

        Protected WithEvents pnlProfileOptions As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents rowEditProfile As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents pnlOtherUser As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lnkAddRemoveBuddiesList As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblBuddyStatus As System.Web.UI.WebControls.Label
        Protected WithEvents lnkBack As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkSendPrivateMessage As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lstBuddies As System.Web.UI.WebControls.DataList


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

			Title1.DisplayHelp = "DisplayHelp_UserInfoModule"
			Dim userID As Integer = -1
			lnkAddRemoveBuddiesList.Text = GetLanguage("UO_AddList")
			lnkSendPrivateMessage.Text = GetLanguage("UO_SendMessage")
            If Not IsNumeric(Request.Params("UserId")) Then
                Response.Write(GetLanguage("UO_BadUserID"))
                Return
            End If

            userID = Int32.Parse(Request.Params("UserID"))

            BindProfile(userID)
            BindBuddies(userID)

            If (Page.User.Identity.IsAuthenticated) Then
                rowEditProfile.Visible = IsProfileEditable()

                If ((New Utility).GetUserID() = userID) Then
                    pnlOtherUser.Visible = False
                End If
            Else
                pnlProfileOptions.Visible = False
            End If

            If (Page.IsPostBack = False) Then
                If Request.Params("def") <> "" Then
                    Title1.DisplayTitle = GetLanguage("UserInfoModule")
                End If
            End If

        End Sub

        Private Sub BindProfile(ByVal userID As Integer)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objUsersOnline As UsersOnlineDB = New UsersOnlineDB

            Dim loggedInUserID As Integer = -1

            If (Request.IsAuthenticated) Then
                loggedInUserID = (New Utility).GetUserID()
            End If

            Dim myReader As SqlDataReader = objUsersOnline.GetSingleUser(_portalSettings.PortalId, userID, loggedInUserID)

            If (myReader.Read()) Then
                Title1.DisplayTitle = GetLanguage("UO_InfoUserName") & ": " + myReader("Username")

                lblUsername.Text = myReader("Username")
                lblBuddyList.Text = Replace(GetLanguage("UO_ListUserName"), "{username}" , myReader("Username")) & " :"
                lblFullName.Text = myReader("FirstName") & " " & myReader("LastName")

                If Not (myReader("CreatedDate") Is DBNull.Value) Then
                    lblJoined.Text = DateTime.Parse(myReader("CreatedDate")).ToLongDateString()
                Else
                    lblJoined.Text = GetLanguage("UO_SinceEver") 
                End If

                If Not (myReader("LastLoginDate") Is DBNull.Value) Then
                    lblLastVisited.Text = DateTime.Parse(myReader("LastLoginDate")).ToLongDateString()
                Else
                    lblLastVisited.Text = GetLanguage("UO_NeverConnected") 
                End If


                If Not (myReader("SessionID") Is DBNull.Value) Then
                    lblOnlineStatus.Text = GetLanguage("yes") 
                    lblOnlineStatus.ForeColor = System.Drawing.Color.Green
                Else
                    lblOnlineStatus.Text = GetLanguage("no") 
                    lblOnlineStatus.ForeColor = System.Drawing.Color.Red
                End If

                If Not (myReader("ItemID") Is DBNull.Value) Then
                    lnkAddRemoveBuddiesList.Text = GetLanguage("UO_RemovePU") 
                    lnkAddRemoveBuddiesList.CommandArgument = "Remove"
                    lblBuddyStatus.Text = GetLanguage("yes")
                    lblBuddyStatus.ForeColor = System.Drawing.Color.Green
                Else
                    lnkAddRemoveBuddiesList.Text = GetLanguage("UO_AddToList") 
                    lnkAddRemoveBuddiesList.CommandArgument = "Add"
                    lblBuddyStatus.Text = GetLanguage("no")
                    lblBuddyStatus.ForeColor = System.Drawing.Color.Red
                End If



            End If

            myReader.Close()
        End Sub

        Private Sub BindBuddies(ByVal userID As Integer)

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objBuddies As BuddiesDB = New BuddiesDB

            Dim myReader As SqlDataReader = objBuddies.GetBuddiesList(_portalSettings.PortalId, userID)

            lstBuddies.DataSource = myReader
            lstBuddies.DataBind()

            If (lstBuddies.Items.Count = 0) Then
                lblNoBuddiesFound.Visible = True
                lstBuddies.Visible = False
            End If

            myReader.Close()

        End Sub

        Protected Function GetEditProfileLink() As String

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Or PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) Then
                If ((New Utility).GetUserID() = Request.Params("UserID")) Then
                    Return FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "def=Register")
                Else
                    Return FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "def=Gestion%20usagers&UserID=" & Request.Params("UserID"))
                End If
            Else
                Return FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "def=Register")
            End If

        End Function

        Protected Function IsProfileEditable() As Boolean

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Or PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) Then
                Return True
            Else
                If (Request.Params("UserID") = (New Utility).GetUserID()) Then
                    Return True
                Else
                    Return False
                End If
            End If

        End Function

        Protected Function GetUserInfoLink(ByVal userID As String) As String
            Return GetFullDocument() & "?TabID=" & TabId.ToString() + "&def=UserInfo&UserID=" + userID
        End Function

        Protected Function GetUserInfoTooltip(ByVal userName As String) As String
            Return replace(GetLanguage("UO_see_user"), "{username}", userName)
        End Function


        Protected Sub lnkAddRemoveBuddiesList_Command(ByVal sender As Object, ByVal e As CommandEventArgs) Handles lnkAddRemoveBuddiesList.Command
            Dim objBuddies As BuddiesDB = New BuddiesDB
            If IsNumeric(Request.Params("UserID")) Then
                If (e.CommandArgument = "Remove") Then
                    objBuddies.DeleteBuddy((New Utility).GetUserID(), Int32.Parse(Request.Params("UserID")))
                Else
                    objBuddies.AddBuddy((New Utility).GetUserID(), Int32.Parse(Request.Params("UserID")))
                End If

                BindProfile(Int32.Parse(Request.Params("UserID")))
            End If
        End Sub

        Private Sub lnkSendPrivateMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSendPrivateMessage.Click
            Response.Redirect(GetFullDocument() & "?tabid=" & Me.TabId & "&def=PrivateMessages&pmsTabID=3&UserID=" & Request("UserID"))
        End Sub
    End Class

End Namespace