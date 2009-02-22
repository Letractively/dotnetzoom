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

Namespace DotNetZoom

    Public Class TTT_ForumSubscribe
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents lstGroup As System.Web.UI.WebControls.DataList
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
        Protected WithEvents lstSubscription As System.Web.UI.WebControls.ListBox
        Protected WithEvents btnBack As System.Web.UI.WebControls.Button
		Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		Private itemIndex As Integer = -1

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


						
            Dim ImageFolder As String = ForumConfig.SkinImageFolder()

            If IsNumeric(Request.Params("ItemIndex")) Then
                itemIndex = Int32.Parse(Request.Params("ItemIndex"))
            End If

            If Not Page.IsPostBack Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If

			btnBack.Text = GetLanguage("return")
			btnBack.Tooltip = GetLanguage("return")
                GetSubscription()
                BindList()

                If itemIndex <> -1 Then
                    lstGroup.SelectedIndex = itemIndex
                    'BindList()
                End If

                If ModuleId > 0 Then
                    ForumConfig.ResetForumConfig(ModuleId)
                End If

                

            End If
			
        End Sub

        Sub BindList()
            Dim forumGroup As New ForumDB()
            lstGroup.DataSource = forumGroup.TTTForum_GetGroups(_portalSettings.PortalId, ModuleId)
            lstGroup.DataBind()
        End Sub

        Function GetForums() As SqlDataReader

            Dim dbForum As New ForumDB()
            Dim groupKey As String = Convert.ToString(lstGroup.DataKeys(lstGroup.SelectedIndex))
            Dim groupID As Integer = Int16.Parse(groupKey)
            Dim dr As SqlDataReader = dbForum.TTTForum_GetForums(groupID)
            Return dr

        End Function

        Private Sub lstGroup_Select(ByVal Sender As System.Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles lstGroup.ItemCommand

            ' Determine the command of the button 
            Dim cmd As String = CType(e.CommandSource, ImageButton).CommandName
            Dim argument As String = CType(e.CommandSource, ImageButton).CommandArgument
            Dim groupID As Integer = Int32.Parse(argument)
            Dim dbForum As New ForumDB()
            Dim groupInfo As ForumGroupInfo = ForumGroupInfo.GetGroupInfo(groupID)

            Select Case cmd.ToLower

                Case "collapse"
                    lstGroup.SelectedIndex = -1
                    BindList()

                Case "expand"
                    lstGroup.SelectedIndex = e.Item.ItemIndex
                    BindList()

            End Select

            BindList()

        End Sub


#Region "Subscribe Forum"

        Public Sub AddSubscribe_Click(ByVal Sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

            Dim cmd As String = CType(Sender, ImageButton).CommandArgument
            Dim forumID As Integer = Int16.Parse(cmd)
            Dim userID As Integer = Int16.Parse(Context.User.Identity.Name)
            Dim dbForum As New ForumDB()
            dbForum.TTTForum_TrackingForumCreateDelete(forumID, userID, True)
            GetSubscription()
            BindList()
        End Sub

        Public Sub RemoveSubscribe_Click(ByVal Sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

            Dim cmd As String = CType(Sender, ImageButton).CommandArgument
            Dim forumID As Integer = Int16.Parse(cmd)
            Dim userID As Integer = Int16.Parse(Context.User.Identity.Name)
            Dim dbForum As New ForumDB()
            dbForum.TTTForum_TrackingForumCreateDelete(forumID, userID, False)
            GetSubscription()
            BindList()
        End Sub

        Private Sub GetSubscription()

            Dim dbForum As New ForumDB()
            lstSubscription.DataSource = dbForum.TTTForum_TrackingForumGet(Int16.Parse(Context.User.Identity.Name))
            lstSubscription.DataBind()

        End Sub

        Public Function CanAdd(ByVal ForumID As String) As Boolean

            Return (lstSubscription.Items.FindByValue(ForumID) Is Nothing)

        End Function

        Public Function CanRemove(ByVal ForumID As String) As Boolean

            Return (Not lstSubscription.Items.FindByValue(ForumID) Is Nothing)

        End Function

        Public Function CanView(ByVal AuthorizedRoles As String) As Boolean
            If Len(AuthorizedRoles) = 0 Then
                Return True
            Else
                Return PortalSecurity.IsInRoles(AuthorizedRoles)
            End If
        End Function


        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub

#End Region



    End Class

End Namespace