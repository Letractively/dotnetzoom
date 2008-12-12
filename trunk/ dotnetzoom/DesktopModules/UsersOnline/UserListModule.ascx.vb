Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient

Namespace DotNetZoom

    Public MustInherit Class UserListModule
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents drpGroupFilter As System.Web.UI.WebControls.DropDownList
        Protected WithEvents drpSortOrder As System.Web.UI.WebControls.DropDownList
        Protected WithEvents drpSortDirection As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lnkSearch As System.Web.UI.WebControls.LinkButton
        Protected WithEvents rptUsers As System.Web.UI.WebControls.Repeater
        Protected WithEvents lblPageNumber As System.Web.UI.WebControls.Label
        Protected WithEvents lblPageCount As System.Web.UI.WebControls.Label
        Protected WithEvents lnkFirstPage As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkPrevPage As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkNextPage As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkLastPage As System.Web.UI.WebControls.LinkButton
        Protected WithEvents pnlPaging As System.Web.UI.WebControls.PlaceHolder
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
			If Request.Params("edit") <> "" Then
			Title1.DisplayTitle = getlanguage("UserListModule")
			end if
			Title1.DisplayHelp = "DisplayHelp_UserListModule"
			drpGroupFilter.Items.FindByValue("All Users").Text = GetLanguage("UO_AllUsers")
			drpGroupFilter.Items.FindByValue("Online Users Only").Text = GetLanguage("UO_OnlineOnly")
			drpSortOrder.Items.FindByValue("Username").Text = GetLanguage("UO_UserName")
			drpSortOrder.Items.FindByValue("FirstName").Text = GetLanguage("UO_Name")
			drpSortDirection.Items.FindByValue("ASC").Text = GetLanguage("UO_ASC")
			drpSortDirection.Items.FindByValue("DESC").Text = GetLanguage("UO_DESC")
			lnkSearch.Text = GetLanguage("search_search")
			lnkFirstPage.Text = GetLanguage("UO_FirstPage")
			lnkPrevPage.Text = GetLanguage("UO_PrevPage")
			lnkNextPage.Text = GetLanguage("UO_NextPage")
			lnkLastPage.Text = GetLanguage("UO_LastPage")
            If Not IsPostBack Then
                If Request.IsAuthenticated Then
                    drpGroupFilter.Items.Add(New ListItem(GetLanguage("UO_OnlyBody")))
                End If
            End If

        End Sub

        Private Sub BindList()
            Dim objUsers As UsersOnlineDB = New UsersOnlineDB

            Dim pageSize As Integer = 10
            Dim pageNumber As Integer = 1
            Dim pageCount As Integer = 1
            Dim loggedInUserID As Integer = -1

            If Not (ViewState.Item("pageNumber") Is Nothing) Then
                pageNumber = ViewState.Item("pageNumber")
            Else
                ViewState.Item("pageNumber") = 1
            End If

            If (Request.IsAuthenticated) Then
                loggedInUserID = (New Utility).GetUserID()
            End If

            Dim sortOrder As String = drpSortOrder.SelectedItem.Value
            Dim sortDirection As String = drpSortDirection.SelectedItem.Value

            ViewState.Item("groupFilter") = drpGroupFilter.SelectedItem.Text
            ViewState.Item("sortOrder") = sortOrder
            ViewState.Item("sortDirection") = sortDirection

            Dim myReader As SqlDataReader
 		  Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If (drpGroupFilter.SelectedItem.Text = GetLanguage("UO_OnlineOnly")) Then
                myReader = objUsers.GetUsersOnlineOnly(_portalSettings.PortalId, pageNumber, pageSize, loggedInUserID)
            Else
                If (drpGroupFilter.SelectedItem.Text = GetLanguage("UO_OnlyBody")) Then
                    myReader = objUsers.GetUsersBuddiesOnly(_portalSettings.PortalId, pageNumber, pageSize, loggedInUserID)
                Else
                    myReader = objUsers.GetUsers(_portalSettings.PortalId, loggedInUserID, pageNumber, pageSize, sortOrder, sortDirection)
                End If
            End If

            If (myReader.Read()) Then
                pageCount = (myReader(0) \ 10) + 1
                ViewState.Item("pageCount") = pageCount
                If (pageCount > 0) Then
                    myReader.NextResult()

                    rptUsers.DataSource = myReader
                    rptUsers.DataBind()

                    lblPageNumber.Text = pageNumber
                    lblPageCount.Text = pageCount
                End If
            End If

            pnlPaging.Visible = True

            myReader.Close()
        End Sub

        Private Sub lnkSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSearch.Click
            ViewState.Item("pageNumber") = 1
            BindList()
        End Sub

        Protected Sub Page_Changed(ByVal sender As System.Object, ByVal e As CommandEventArgs)

            Select Case (e.CommandName)
                Case "FirstPage"
                    ViewState.Item("pageNumber") = "1"
                Case "PrevPage"
                    If (ViewState.Item("pageNumber") > 1) Then
                        ViewState.Item("pageNumber") = ViewState.Item("pageNumber") - 1
                    End If
                Case "NextPage"
                    If (ViewState.Item("pageCount") > ViewState.Item("pageNumber")) Then
                        ViewState.Item("pageNumber") = ViewState.Item("pageNumber") + 1
                    End If
                Case "LastPage"
                    ViewState.Item("pageNumber") = ViewState.Item("pageCount")
            End Select

            If (ViewState.Item("groupFilter") <> drpGroupFilter.SelectedItem.Text Or ViewState.Item("sortOrder") <> drpSortOrder.SelectedItem.Value Or ViewState.Item("sortDirection") <> drpSortDirection.SelectedItem.Value) Then
                ViewState.Item("pageNumber") = 1
            End If

            BindList()

        End Sub

        Protected Function GetUserInfoLink(ByVal userID As String) As String
            Return "~" & GetDocument() & "?edit=control&TabID=" & TabId.ToString() + "&def=UserInfo&UserID=" + userID
        End Function

        Protected Function GetUserInfoTooltip(ByVal userName As String) As String
            Return replace(GetLanguage("UO_see_user"), "{username}", userName)
        End Function

        Protected Function GetOnlineStatus(ByVal sessionID As String) As String
            If (sessionID <> "") Then
                Return GetLanguage("yes") 
            Else
                Return GetLanguage("no") 
            End If
        End Function

        Protected Function GetOnlineColor(ByVal sessionID As String) As String
            If (sessionID <> "") Then
                Return "green"
            Else
                Return "red"
            End If
        End Function

        Protected Function GetBuddyStatus(ByVal buddyID As String) As String
            If (buddyID <> "") Then
                Return GetLanguage("yes") 
            Else
                Return GetLanguage("no") 
            End If
        End Function

        Protected Function GetBuddyColor(ByVal buddyID As String) As String
            If (buddyID <> "") Then
                Return "green"
            Else
                Return "red"
            End If
        End Function

'        Protected Function GetTableSizeUnit() As Unit
'            If Request.Url.ToString().ToLower().IndexOf("default.aspx") = -1 Then
'                Return Unit.Pixel(750)
'            Else
'                Return Unit.Percentage(100)
'            End If
'        End Function
    End Class

End Namespace