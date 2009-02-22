'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' =======================================================================================
Option Strict On

Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public MustInherit Class TTT_ModerateDetails

        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents lstModerators As System.Web.UI.WebControls.DataList
        Protected WithEvents ctlUsers As TTT_UsersControl

        Private _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Private ZforumID As Integer
        Private ZmoduleID As Integer
        Private _IsModerator As Boolean

        Private ZforumModerators As ForumModeratorCollection

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
        '*******************************************************
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If IsNumeric(Request.Params("forumid")) Then
                ZforumID = Int16.Parse(Request.Params("forumid"))
            End If

            If IsNumeric(Request.Params("mid")) Then
                ZmoduleID = Int16.Parse(Request.Params("mid"))
            End If

            If Not Page.IsPostBack Then

                ' Get moderators list
                If ZforumID > 0 Then
                    BindModerators(ZforumID)
                End If

            End If

        End Sub

        Public Sub RemoveUser_Click(ByVal Sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

            Dim userID As Integer = Int16.Parse(CType(Sender, ImageButton).CommandArgument)
            Dim dbForum As New ForumDB()
            dbForum.TTTForum_Moderate_UserCreateUpdateDelete(ZforumID, userID, True, 2)

            'Refresh
            Response.Redirect(Request.Url.ToString)

        End Sub
	
        Private Sub lstModerators_ItemCommand(ByVal Source As System.Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles lstModerators.ItemCommand

            ' Determine the command of the button 
            Dim cmd As String = CType(e.CommandSource, ImageButton).CommandName
            Dim UserID As Integer = Int32.Parse(CType(e.CommandSource, ImageButton).CommandArgument)

            Select Case cmd.ToLower

                Case "collapse"
                    lstModerators.SelectedIndex = -1
                    BindModerators(ZforumID)

                Case "expand"
                    lstModerators.SelectedIndex = e.Item.ItemIndex
                    BindModerators(ZforumID)

            End Select
        End Sub

        Private Sub BindModerators(ByVal ForumID As Integer)

            ZforumModerators = New ForumModeratorCollection(ForumID)
            lstModerators.DataSource = ZforumModerators
            lstModerators.DataBind()
        End Sub

        Public Function IsNotEmpty(ByVal Value As Object) As Boolean

            Return Len(CType(Value, String)) > 0

        End Function

        Public Function FormatUserURL(ByVal strKeyValue As String) As String
            Dim strUserAdmin As String = TTTUtils.ForumUserAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID, Int16.Parse(strKeyValue))
            Return strUserAdmin
        End Function

        Public Function DisplayAddress(ByVal Unit As Object, ByVal Street As Object, ByVal City As Object, ByVal Region As Object, ByVal Country As Object, ByVal PostalCode As Object) As String
            DisplayAddress = FormatAddress("", Street, City, Region, "", "")
        End Function

        Public Function DisplayEmail(ByVal Email As Object) As String
            DisplayEmail = FormatEmail(Email, page)
        End Function

        Public Function DisplayWebsite(ByVal URL As Object) As String
            DisplayWebsite = FormatWebsite(URL)
        End Function

        Private Sub ctlUsers_UserSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlUsers.UserSelected
            Dim ZuserID As Integer = CType(ctlUsers.SelectedUser, ForumUser).UserID
            Dim dbForum As New ForumDB()
            dbForum.TTTForum_Moderate_UserCreateUpdateDelete(ZforumID, ZuserID, True, 0)

            ' Refresh
            Response.Redirect(Request.Url.ToString)
        End Sub
    End Class

End Namespace