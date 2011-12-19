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
Option Strict On
Imports DotNetZoom.TTTUtils
Namespace DotNetZoom

    Public Class TTT_ForumDetails
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents txtForumID As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtForumName As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtForumGroupName As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtForumGroupID As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtForumDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtForumCreatorName As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtForumCreatorID As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkActive As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkPrivate As System.Web.UI.WebControls.CheckBox
        Protected WithEvents authRoles As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents pnlPrivate As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents chkModerated As System.Web.UI.WebControls.CheckBox

        Protected WithEvents lnkForumModerateAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents chkIntegrated As System.Web.UI.WebControls.CheckBox
        Protected WithEvents txtGalleryID As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAlbumName As System.Web.UI.WebControls.TextBox
        Protected WithEvents btnUpdate As System.Web.UI.WebControls.Button
		Protected WithEvents btnBack As System.Web.UI.WebControls.Button

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Private ZforumID As Integer
        Protected WithEvents pnlForumModerateAdmin As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Checkbox1 As System.Web.UI.WebControls.CheckBox
        Private ZmoduleID As Integer
        Private _RolesDirty As Boolean = False

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

            If IsNumeric(Request.Params("mid")) Then
                ZmoduleID = Int32.Parse(Request.Params("mid"))
            End If


            If IsNumeric(Request.Params("forumid")) Then
                ZforumID = Int32.Parse(Request.Params("forumid"))
            End If

			btnBack.Text = GetLanguage("return")
			btnBack.Tooltip = GetLanguage("return")
			
			
            If Not Page.IsPostBack Then
                PopulateForum()
				chkActive.Text = GetLanguage("F_Active")

				If Not (Page.Request.Params("selectedindex") Is Nothing) and Page.Request.Params("selectedindex") <> "" Then
                HttpContext.Current.Session("selectedindex") = Page.Request.Params("selectedindex")
				HttpContext.Current.Session("edititemindex") = Nothing
                End If

				If Not (Page.Request.Params("edititemindex") Is Nothing) and Page.Request.Params("edititemindex") <> "" Then
                HttpContext.Current.Session("edititemindex") = Page.Request.Params("edititemindex")
				HttpContext.Current.Session("selectedindex") = Nothing
                End If
				
			

            End If

        End Sub

        Public Sub PopulateForum()

            'Dim dbForum As New ForumDB()
            Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)
            With forumInfo
                txtForumID.Text = CType(.ForumID, String)
                txtForumGroupID.Text = CType(.ForumGroupID, String)
                txtForumGroupName.Text = .ForumGroup.Name
                txtForumName.Text = .Name
                txtForumDescription.Text = .Description
                txtForumCreatorID.Text = .CreatedByUser.ToString
                txtForumCreatorName.Text = .Creator.Name
                chkActive.Checked = .IsActive
                chkModerated.Checked = .IsModerated
                chkIntegrated.Checked = .IsIntegrated
                txtGalleryID.Text = CType(.IntegratedGallery, String)
                txtAlbumName.Text = .IntegratedAlbumName
                chkPrivate.Checked = .IsPrivate
            End With

            pnlPrivate.Visible = forumInfo.IsPrivate
            If forumInfo.IsPrivate Then
                BindPrivateForum(forumInfo)
            End If

            pnlForumModerateAdmin.Visible = forumInfo.IsModerated
            btnUpdate.Text = GetLanguage("enregistrer")
		btnUpdate.ToolTip = GetLanguage("enregistrer")
		End Sub


        Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

            'Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim forumID As Integer = Int16.Parse(txtForumID.Text)
            Dim forumGroupID As Integer = Int16.Parse(txtForumGroupID.Text)
            Dim forumName As String = txtForumName.Text
            Dim description As String = txtForumDescription.Text
            Dim createdByUser As Integer = Int16.Parse(txtForumCreatorID.Text)
            Dim IsModerated As Boolean = chkModerated.Checked
            Dim IsActive As Boolean = chkActive.Checked
            Dim IsIntegrated As Boolean = chkIntegrated.Checked
            Dim IntegratedGallery As Integer = Int16.Parse(txtGalleryID.Text)
            Dim IntegratedAlbumName As String = txtAlbumName.Text
            Dim IsPrivate As Boolean = chkPrivate.Checked
            Dim AuthorizedRoles As String = ""

            Dim item As ListItem
            If IsPrivate Then 'AndAlso _RolesDirty Then
                Dim objForum As New ForumDB()
                For Each item In authRoles.Items
                    ' admins always have access to all forums
                    If item.Selected = True _
                    OrElse item.Value = _portalSettings.AdministratorRoleId.ToString _
                    OrElse (PortalSecurity.IsInRole(_portalSettings.AdministratorRoleId.ToString) = False And InStr(1, _portalSettings.ActiveTab.AdministratorRoles.ToString, item.Value) <> 0) Then
                        objForum.TTTForum_PrivateForum_CreateDeleteRoles(ZforumID, Int16.Parse(item.Value), True)
                        AuthorizedRoles += item.Value & ";" ' tobe removed
                    Else
                        objForum.TTTForum_PrivateForum_CreateDeleteRoles(ZforumID, Int16.Parse(item.Value), False)
                    End If
                Next item
            End If

            Dim dbForum As New ForumDB()
            dbForum.TTTForum_ForumCreateUpdateDelete(forumGroupID, _portalSettings.PortalId, ZmoduleID, forumName, description, createdByUser, IsModerated, IsActive, IsIntegrated, IntegratedGallery, IntegratedAlbumName, IsPrivate, AuthorizedRoles, 1, forumID)
            ForumItemInfo.ResetForumInfo(ZforumID)
            If Not HttpContext.Current.Session("edititemindex") is nothing then
			Response.Redirect(ForumAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID) + "&edititemindex=" + CType(HttpContext.Current.Session("edititemindex"), String))
			end if
            If Not HttpContext.Current.Session("selectedindex") is nothing then
			Response.Redirect(ForumModerateAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID) + "&selectedindex=" + CType(HttpContext.Current.Session("selectedindex"), String))
			end if

        End Sub

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            If Not HttpContext.Current.Session("edititemindex") is nothing then
			Response.Redirect(ForumAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID) + "&edititemindex=" + CType(HttpContext.Current.Session("edititemindex"), String))
			end if
            If Not HttpContext.Current.Session("selectedindex") is nothing then
			Response.Redirect(ForumModerateAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID) + "&selectedindex=" + CType(HttpContext.Current.Session("selectedindex"), String))
			end if
        End Sub

		
		
        Private Sub BindPrivateForum(ByVal ForumInfo As ForumItemInfo)

            Dim dbUser As New UsersDB()
			  Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim roles As SqlDataReader = dbUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))

            ' Clear existing items in checkboxlist
            authRoles.Items.Clear()

            While roles.Read()
                Dim authItem As New ListItem()
                authItem.Text = CType(roles("RoleName"), String)
                authItem.Value = roles("RoleID").ToString()

                'If authItem.Value = _portalSettings.AdministratorRoleId.ToString Then
                '    authItem.Selected = True
                'End If

                If InStr(1, ForumInfo.AuthorizedRoles, authItem.Value.ToString) > 0 Then
                    authItem.Selected = True
                End If
                authRoles.Items.Add(authItem)

            End While
            roles.Close()
        End Sub

        Private Sub chkPrivate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPrivate.CheckedChanged
            Dim objForum As New ForumDB()
            Dim item As ListItem
            Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(Int16.Parse(txtForumID.Text))

            If chkPrivate.Checked Then
                'Add admin role to private forum right away
                objForum.TTTForum_PrivateForum_CreateDeleteRoles(ZforumID, 0, True)
                BindPrivateForum(forumInfo)
            Else
                ' Delete all roles authorized this private forum
                For Each item In authRoles.Items
                    objForum.TTTForum_PrivateForum_CreateDeleteRoles(ZforumID, Int16.Parse(item.Value), False)
                Next item
            End If
            ForumItemInfo.ResetForumInfo(ZforumID)
            pnlPrivate.Visible = chkPrivate.Checked

        End Sub

        Private Sub chkModerated_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModerated.CheckedChanged

            Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)
            forumInfo.IsModerated = chkModerated.Checked
            forumInfo.UpdateForumInfo()
            ForumItemInfo.ResetForumInfo(ZforumID) 'Clear cache to make sure new info displayed

            pnlForumModerateAdmin.Visible = chkModerated.Checked

        End Sub

        Private Function ModerateLink() As String
            Dim lnkModerate As String = GetFullDocument() & "?edit=control&editpage=" & TTT_EditForum.ForumEditType.ForumModerateAdmin & "&mid=" & ZmoduleID & "&tabid=" & _portalSettings.ActiveTab.TabId & "&forumid=" & ZforumID
            Return lnkModerate
        End Function

        Private Sub authRoles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles authRoles.SelectedIndexChanged
            _RolesDirty = True
        End Sub
    End Class

End Namespace