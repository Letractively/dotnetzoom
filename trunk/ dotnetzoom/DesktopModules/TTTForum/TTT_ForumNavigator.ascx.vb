'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
' =======================================================================================
Option Strict Off

Imports System
Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public MustInherit Class TTT_ForumNavigator
        Inherits System.Web.UI.UserControl
	

        Protected WithEvents lblGroupName As System.Web.UI.WebControls.Label
        Protected WithEvents btnGroupEdit As System.Web.UI.WebControls.ImageButton
        Protected WithEvents btnGroupDelete As System.Web.UI.WebControls.ImageButton
        Protected WithEvents btnGroupUp As System.Web.UI.WebControls.ImageButton
        Protected WithEvents btnGroupDown As System.Web.UI.WebControls.ImageButton
        Protected WithEvents lstForum As System.Web.UI.WebControls.DataList
        Protected WithEvents txtNewForum As System.Web.UI.WebControls.TextBox
        Protected WithEvents btnNewForum As System.Web.UI.WebControls.LinkButton
        Protected WithEvents btnExpand As System.Web.UI.WebControls.ImageButton
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents btnGroupEdit2 As System.Web.UI.WebControls.ImageButton
        Protected WithEvents btnGroupDelete2 As System.Web.UI.WebControls.ImageButton
        Protected WithEvents btnGroupUp2 As System.Web.UI.WebControls.ImageButton
        Protected WithEvents btnGroupDown2 As System.Web.UI.WebControls.ImageButton
        Protected WithEvents txtNewGroupName As System.Web.UI.WebControls.TextBox
        Protected WithEvents btnAddGroup As System.Web.UI.WebControls.ImageButton
        Protected WithEvents Table4 As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents tblForumAdd As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents tblGroup As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents lstGroup As System.Web.UI.WebControls.DataList
        Protected WithEvents tblMail As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents tblGroupSelect As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents cmdTest As System.Web.UI.WebControls.ImageButton
        Protected WithEvents cmdForumSetting As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkForumSetting As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdModerateAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkModerateAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdHome As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkHome As System.Web.UI.WebControls.HyperLink

        Private _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Protected ZmoduleID As Integer
        Protected ZgroupCanEdit As Boolean
        Protected ZselectedForum As ForumItemInfo
        Protected ZreadOnly As Boolean
		Protected ZEditPost As Boolean
        Protected ZuserID As Integer
        Protected Zconfig As ForumConfig

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

        Event ForumSelected As EventHandler

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ZuserID = Int16.Parse(Context.User.Identity.Name)

            If IsNumeric(Request.Params("mid")) Then
                ZmoduleID = Int32.Parse(Page.Request.Params("mid"))
                Zconfig = ForumConfig.GetForumConfig(ModuleId)
            End If

            ZgroupCanEdit = (lstGroup.SelectedIndex = -1) AndAlso (lstGroup.EditItemIndex = -1) AndAlso (ZreadOnly = False)

            If Not Page.IsPostBack Then
                If IsNumeric(Page.Request.Params("selectedindex")) AndAlso (ZreadOnly = True) Then
                    lstGroup.SelectedIndex = Int32.Parse(Page.Request.Params("selectedindex"))
                End If
                If IsNumeric(Page.Request.Params("edititemindex")) AndAlso (ZreadOnly = False) Then
                    lstGroup.EditItemIndex = Int32.Parse(Page.Request.Params("edititemindex"))
                End If
				BindList()
            End If
        End Sub

        Private Sub BindList()
            Dim forumGroup As ForumGroupInfoCollection = New ForumGroupInfoCollection(_portalSettings.PortalId, ZmoduleID)
            lstGroup.DataSource = forumGroup

            'turn addgroup function on when no group exists
            If forumGroup.Count = 0 AndAlso Not ZreadOnly Then
                ZgroupCanEdit = True
            End If

            'allow add group in edit mode only
            lstGroup.ShowFooter = ZgroupCanEdit AndAlso Not ZreadOnly
            lstGroup.DataBind()
            

        End Sub

        Private Sub lstGroup_ItemCommand(ByVal Sender As System.Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles lstGroup.ItemCommand

            ' Determine the command of the button 
            Dim cmd As String = e.CommandName
            Dim argument As String = e.CommandArgument
            Dim groupID As Integer = Int32.Parse(argument)
            Dim dbForum As New ForumDB()
            Dim groupInfo As ForumGroupInfo = ForumGroupInfo.GetGroupInfo(groupID)
			
            Select Case cmd.ToLower
                Case "collapse"
                    lstGroup.SelectedIndex = -1
					If ZreadOnly = False then
                    ZgroupCanEdit = True
					else
					 ZgroupCanEdit = False
					end if
                Case "expand"
                    lstGroup.SelectedIndex = e.Item.ItemIndex
					session("selectedindex") = e.Item.ItemIndex.ToString()
					ZgroupCanEdit = False
                Case "edit"
                    lstGroup.EditItemIndex = e.Item.ItemIndex
					HttpContext.Current.Session("edititemindex") = e.Item.ItemIndex.ToString()
                    ZgroupCanEdit = False
                Case "delete"
                    dbForum.TTTForum_ForumGroupCreateUpdateDelete(groupID, groupInfo.Name, _portalSettings.PortalId, ZmoduleID, groupInfo.CreatedByUser, 2)
                Case "up"
                    dbForum.TTTForum_UpdateForumGroupSortOrder(groupID, True)
                Case "down"
                    dbForum.TTTForum_UpdateForumGroupSortOrder(groupID, False)
                Case "add"
                    AddGroup()
                Case "update"
                    UpdateGroup(groupID)
                Case "cancel"
                    lstGroup.EditItemIndex = -1
                 	If ZreadOnly = False then
                    ZgroupCanEdit = True
					else
					 ZgroupCanEdit = False
					end if

            End Select

            BindList()

        End Sub

        Private Sub AddGroup()

            Dim dbForum As New ForumDB()
            Dim lstFooter As DataListItem
            Dim control As DataListItem
            For Each control In lstGroup.Controls
                If control.ItemType = ListItemType.Footer Then
                    lstFooter = control
                    Exit For
                End If
            Next
            'lstFooter = lstGroup.Controls.Item(2)
            Dim groupName As String = DirectCast(lstFooter.FindControl("txtNewGroupName"), TextBox).Text
            Dim createdByUser As Integer = ZuserID

            If Len(groupName) > 0 Then
                dbForum.TTTForum_ForumGroupCreateUpdateDelete(-1, groupName, _portalSettings.PortalId, ZmoduleID, createdByUser, 0)
            End If

        End Sub

        Private Sub UpdateGroup(ByVal GroupID As Integer)
            Dim dbForum As New ForumDB()
            Dim lstGroupItem As DataListItem = lstGroup.Items(lstGroup.EditItemIndex)
            Dim groupName As String = DirectCast(lstGroupItem.FindControl("txtGroupName"), TextBox).Text
            Dim createdByUser As Integer = ZuserID
            dbForum.TTTForum_ForumGroupCreateUpdateDelete(GroupID, groupName, _portalSettings.PortalId, ZmoduleID, createdByUser, 1)

        End Sub

        Private Sub CancelGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            lstGroup.ShowFooter = False
        End Sub

        Public Function BindForum() As ForumItemCollection

            Dim groupID As Integer
            If ZreadOnly Then
                groupID = Int16.Parse(lstGroup.DataKeys.Item(lstGroup.SelectedIndex).ToString)
            Else
                groupID = Int16.Parse(lstGroup.DataKeys.Item(lstGroup.EditItemIndex).ToString)
            End If

            Dim forumCollection As ForumItemCollection = New ForumItemCollection(groupID)

            Return forumCollection

        End Function

        Public Sub EditForum_Click(ByVal Sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
            Dim forumID As Integer = Int32.Parse(CType(Sender, ImageButton).CommandArgument)
            Dim strForumAdmin As String = GetFullDocument() & "?edit=control&editpage=" & TTT_EditForum.ForumEditType.ForumAdmin & "&mid=" & ZmoduleID & "&tabid=" & _portalSettings.ActiveTab.TabId & "&forumid=" & forumID
            If Not HttpContext.Current.Session("edititemindex") is nothing then
			Response.Redirect(strForumAdmin + "&edititemindex=" + CType(HttpContext.Current.Session("edititemindex"), String))
			end if
        End Sub

        Public Sub DeleteForum_Click(ByVal Sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

            Dim forumID As Integer = Int32.Parse(CType(Sender, ImageButton).CommandArgument)

            Dim dbForum As New ForumDB()
            Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(forumID)
            Dim forumGroupID As Integer = forumInfo.ForumGroupID

            dbForum.TTTForum_ForumCreateUpdateDelete(forumGroupID, _portalSettings.PortalId, ZmoduleID, "", "", 0, False, True, False, 0, "", False, "", 2, forumID)
            BindList()
        End Sub

        Public Sub ForumUp_Click(ByVal Sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

            Dim forumID As Integer = Int32.Parse(CType(Sender, ImageButton).CommandArgument)

            Dim dbForum As New ForumDB()
            Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(forumID)
            Dim forumGroupID As Integer = forumInfo.ForumGroupID

            dbForum.TTTForum_UpdateForumSortOrder(forumGroupID, forumID, True)
            BindList()

        End Sub

        Public Sub ForumDown_Click(ByVal Sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

            Dim forumID As Integer = Int32.Parse(CType(Sender, ImageButton).CommandArgument)

            Dim dbForum As New ForumDB()
            Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(forumID)
            Dim forumGroupID As Integer = forumInfo.ForumGroupID

            dbForum.TTTForum_UpdateForumSortOrder(forumGroupID, forumID, False)
            BindList()

        End Sub

        Public Sub ForumSelect_Click(ByVal Sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

            Dim _selID As Integer = Int16.Parse(CType(Sender, ImageButton).CommandArgument)
            ZselectedForum = ForumItemInfo.GetForumInfo(_selID)
            RaiseEvent ForumSelected(Me, e)

        End Sub



        Public Sub AddForum_Click(ByVal Sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

            Dim GroupID As Integer = Int16.Parse(lstGroup.DataKeys.Item(lstGroup.EditItemIndex).ToString)
            Dim listItem As DataListItem = DirectCast(lstGroup.Items(lstGroup.EditItemIndex), DataListItem)
            Dim forumList As DataList = DirectCast(listItem.FindControl("lstForum"), DataList)
            Dim lstFooter As DataListItem
            Dim control As DataListItem
            For Each control In forumList.Controls
                If control.ItemType = ListItemType.Footer Then
                    lstFooter = control
                    Exit For
                End If
            Next
            Dim tbForumName As TextBox
            tbForumName = DirectCast(lstFooter.FindControl("txtNewForum"), TextBox)
            Dim newForumName As String = tbForumName.Text
            Dim createdByUser As Integer = ZuserID


            If Len(newForumName) > 0 Then
                Dim dbForum As New ForumDB()
                Dim newForumID As Integer = dbForum.TTTForum_ForumCreateUpdateDelete(GroupID, _portalSettings.PortalId, ZmoduleID, newForumName, "", createdByUser, False, True, False, 0, "", False, "", 0, -1)

                BindList()
            End If
        End Sub

        Public Function GroupCanEdit() As Boolean
            Return ZgroupCanEdit

        End Function

        Public Function GetEditCommand() As String
            If ZreadOnly Then
                Return "expand"
            Else
                Return "edit"
            End If
        End Function

        Public Function FormatUser(ByVal UserID As Object) As String
            If Not IsDBNull(UserID) Then
                Dim user As ForumUser = ForumUser.GetForumUser(CType(UserID, Integer))
                Return user.Name
            Else
                Return "Anonymous"
            End If
        End Function

 
        Public Property IsReadOnly() As Boolean
            Get
                Return ZreadOnly
            End Get
            Set(ByVal Value As Boolean)
                ZreadOnly = Value
            End Set
        End Property
		
        Public Property EditPost() As Boolean
            Get
                Return ZEditPost
            End Get
            Set(ByVal Value As Boolean)
                ZEditPost = Value
            End Set
        End Property


        Public ReadOnly Property SelectedForum() As ForumItemInfo
            Get
                Return ZselectedForum
            End Get
        End Property

		Public Function GetEditText(ByVal IsModerated As boolean) As String
		if EditPost then
		Return GetLanguage("cmdNewTopic")
		else
		if IsModerated then
		Return GetLanguage("F_SelectModerated")
		else
		Return GetLanguage("F_SelectAdmin")
		end if
		end if
		end Function
		
        Public Function GetEditStyle(ByVal IsModerated As Boolean) As String
            If EditPost Then
                Return "border-width:0px; background: url('" & Zconfig.ImageFolder() & "forum.gif') no-repeat; background-position: 0px -128px;"
            Else
                If IsModerated Then
                    Return "border-width:0px; background: url('" & Zconfig.ImageFolder() & "forum.gif') no-repeat; background-position: 0px -176px;"
                Else
                    Return "border-width:0px; background: url('" & Zconfig.ImageFolder() & "forum.gif') no-repeat; background-position: 0px -128px;"
                End If
            End If
        End Function

        Public Function GetImgStyle(ByVal img As String) As String
            Select Case img
                Case "edit"
                    Return "border-width:0px; background: url('" & Zconfig.ImageFolder() & "forum.gif') no-repeat; background-position: 0px -128px;"
                Case "delete"
                    Return "border-width:0px; background: url('" & Zconfig.ImageFolder() & "forum.gif') no-repeat; background-position: 0px -32px;"
                Case "add"
                    Return "border-width:0px; background: url('" & Zconfig.ImageFolder() & "forum.gif') no-repeat; background-position: 0px -16px;"
                Case Else
                    Return "border-width:0px; background: url('" & Zconfig.ImageFolder() & "forum.gif') no-repeat; background-position: 0px 0px;"
            End Select
 
        End Function

		
    End Class

End Namespace