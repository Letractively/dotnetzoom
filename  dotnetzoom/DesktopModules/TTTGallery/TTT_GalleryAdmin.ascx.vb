'=======================================================================================
' TTTGALLERY MODULE FOR DOTNETNUKE (1.0.10)
'=======================================================================================
' Original version by:              DAVID BARRETT http://www.davidbarrett.net
' A modified version for DNN by:    KENNYRICE
' This version for TTTCompany       http://www.tttcompany.com
' Author:                           TAM TRAN MINH   (tamttt - tam@tttcompany.com)
' Slideshow component:              FREEK VAN OORT
' Flash Player component:           TYLER JENSEN
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
'========================================================================================
Option Strict On

Imports System.IO
Imports System
Imports System.Web
Imports Microsoft.VisualBasic
Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom
    Public Class TTT_GalleryAdmin
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents ctlUsers As TTT_UsersControl
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
		Protected WithEvents lblQuota As System.Web.UI.WebControls.Label
        Protected WithEvents RootURL As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtQuota As System.Web.UI.WebControls.TextBox
        Protected WithEvents Regularexpressionvalidator9 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents txtMaxFileSize As System.Web.UI.WebControls.TextBox
        Protected WithEvents Regularexpressionvalidator8 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents chkFixedSize As System.Web.UI.WebControls.CheckBox
        Protected WithEvents FixedWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents Quality As System.Web.UI.WebControls.TextBox
        Protected WithEvents RegularExpressionValidator5 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents FixedHeight As System.Web.UI.WebControls.TextBox
        Protected WithEvents RegularExpressionValidator6 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents chkKeepSource As System.Web.UI.WebControls.CheckBox
        Protected WithEvents pnlFixedSize As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents chkPrivate As System.Web.UI.WebControls.CheckBox
        Protected WithEvents txtOwner As System.Web.UI.WebControls.TextBox
        Protected WithEvents btnEditOwner As System.Web.UI.WebControls.Button
        Protected WithEvents txtOwnerID As System.Web.UI.WebControls.TextBox
        Protected WithEvents BuildCacheOnStart As System.Web.UI.WebControls.CheckBox
        Protected WithEvents pnlAdmin As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents GalleryTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents Description As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkForumIntegrate As System.Web.UI.WebControls.CheckBox
        Protected WithEvents ddlForumGroup As System.Web.UI.WebControls.DropDownList
        Protected WithEvents ddlForum As System.Web.UI.WebControls.DropDownList
        Protected WithEvents btnAdd As System.Web.UI.WebControls.ImageButton
        Protected WithEvents btnRemove As System.Web.UI.WebControls.ImageButton
        Protected WithEvents lstIntegrate As System.Web.UI.WebControls.ListBox
        Protected WithEvents ddlAlbums As System.Web.UI.WebControls.DropDownList
        Protected WithEvents pnlForumSelect As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents StripWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents RegularExpressionValidator10 As System.Web.UI.WebControls.RegularExpressionValidator

        Protected WithEvents StripHeight As System.Web.UI.WebControls.TextBox
        Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents MaxThumbWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents RegularExpressionValidator3 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents MaxThumbHeight As System.Web.UI.WebControls.TextBox
        Protected WithEvents RegularExpressionValidator4 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents CategoryValues As System.Web.UI.WebControls.TextBox
        Protected WithEvents FileExtensions As System.Web.UI.WebControls.TextBox
        Protected WithEvents MovieExtensions As System.Web.UI.WebControls.TextBox
        Protected WithEvents SlideshowSpeed As System.Web.UI.WebControls.TextBox
        Protected WithEvents RegularExpressionValidator7 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents chkPopup As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkInfoBule As System.Web.UI.WebControls.CheckBox
        Protected WithEvents pnlUser As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
        Protected WithEvents btnUpdate As System.Web.UI.WebControls.Button
        Protected WithEvents Span1 As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents pnlPrivate As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlSelectOwner As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents chkDownload As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkAvatarsGallery As System.Web.UI.WebControls.CheckBox
        Protected WithEvents ddlDisplayOption As System.Web.UI.WebControls.DropDownList
        Protected config As GalleryConfig

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

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            config = GalleryConfig.GetGalleryConfig(ModuleId)

			Regularexpressionvalidator9.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator8.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator10.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator5.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator6.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator1.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator2.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator3.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator4.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator7.ErrorMessage = GetLanguage("need_number")
			btnEditOwner.Text = GetLanguage("Gal_Select")
			btnEditOwner.ToolTip = GetLanguage("Gal_SOwner")
			btnCancel.Text = GetLanguage("annuler")
			btnCancel.ToolTip = GetLanguage("annuler")
			btnUpdate.Text = GetLanguage("enregistrer")
			btnUpdate.ToolTip = GetLanguage("Gal_UpdateConf")

 
            If Not Page.IsPostBack Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If

                GalleryTitle.Text = config.GalleryTitle
                Description.Text = config.GalleryDescription
                RootURL.Text = config.RootURL
                If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) Then
                    ' Super User or Admin can modify galery root directory and Quota
                    RootURL.Enabled = True
                    txtQuota.Enabled = True
                End If

                FileExtensions.Text = config.FileExtensions
                MovieExtensions.Text = config.MovieExtensions
                CategoryValues.Text = config.CategoryValues
                StripWidth.Text = config.StripWidth.ToString
                StripHeight.Text = config.StripHeight.ToString
                txtMaxFileSize.Text = config.MaxFileSize.ToString
                txtQuota.Text = config.Quota.ToString
                ' info about quota

                Dim StrFolder As String
                Dim SpaceUsed As Double
                Dim objAdmin As New AdminDB()
                StrFolder = Request.MapPath(config.RootURL)
                SpaceUsed = objAdmin.GetdirectorySpaceUsed(StrFolder)
                If SpaceUsed = 0 Then
                    SpaceUsed = objAdmin.GetFolderSizeRecursive(StrFolder)
                    objAdmin.AddDirectory(StrFolder, SpaceUsed.ToString())
                End If

                SpaceUsed = SpaceUsed / 1048576
                If config.Quota = 0 Then

                    lblQuota.Text = Replace(GetLanguage("Gal_QuotaInfo"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00"))
                Else
                    lblQuota.Text = Replace(GetLanguage("Gal_QuotaInfo1"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00"))
                    lblQuota.Text = Replace(lblQuota.Text, "{Quota}", Format(config.Quota, "#,##0.00"))
                    lblQuota.Text = Replace(lblQuota.Text, "{SpaceLeft}", Format(config.Quota - SpaceUsed, "#,##0.00"))
                End If

                lblQuota.ToolTip = GetLanguage("Gal_QuotaTip")




                MaxThumbWidth.Text = config.MaximumThumbWidth.ToString
                MaxThumbHeight.Text = config.MaximumThumbHeight.ToString
                BuildCacheOnStart.Checked = (config.BuildCacheonStart = True)

                chkFixedSize.Checked = config.IsFixedSize
                FixedWidth.Text = config.FixedWidth.ToString
                Quality.Text = config.Quality.ToString
                FixedHeight.Text = config.FixedHeight.ToString
                chkKeepSource.Checked = config.IsKeepSource
                SlideshowSpeed.Text = config.SlideshowSpeed.ToString
                chkPrivate.Checked = config.IsPrivate
                chkForumIntegrate.Checked = config.IsIntegrated
                chkPopup.Checked = config.SlideshowPopup
                chkInfoBule.Checked = config.InfoBule
                chkDownload.Checked = config.AllowDownload
                chkAvatarsGallery.Checked = config.IsAvatarsGallery
                txtOwner.Text = config.GalleryOwner.UserName
                txtOwnerID.Text = config.OwnerID.ToString

                pnlFixedSize.Visible = chkFixedSize.Checked

                pnlPrivate.Visible = chkPrivate.Checked

                pnlForumSelect.Visible = chkForumIntegrate.Checked

                If chkForumIntegrate.Checked Then
                    If BindForumGroup() Then
                        PopulateIntegration()
                    End If
                End If

                ' Display admin panel if user id admin
                If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                OrElse (PortalSecurity.IsInRoles(CType(PortalSettings.GetEditModuleSettings(ModuleId), ModuleSettings).AuthorizedEditRoles.ToString) = True) Then
                    pnlAdmin.Visible = True
                Else
                    pnlAdmin.Visible = False
                    If txtOwnerID.Text <> Context.User.Identity.Name.ToString Then
                        btnUpdate.Visible = False
                    End If
                End If

            End If

            ddlDisplayOption.DataSource = [Enum].GetValues(GetType(GalleryConfig.GalleryDisplayOption))
            ddlDisplayOption.DataBind()
            'Try
            ddlDisplayOption.Items.FindByValue(config.DisplayOption).Selected = True
            'Catch Exc As System.Exception
            'End Try
        End Sub

        Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

            lblInfo.Text = ""

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim GalleryURL As String = RootURL.Text
            Dim GalleryPath As String = Server.MapPath(GalleryURL)
			Dim Admin as new AdminDB()
			
		If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) And not PortalSecurity.IsSuperUser Then
			if InStr(1, LCase(GalleryURL), LCase(_portalSettings.UploadDirectory)) = 0 then
			If GalleryTitle.Text = string.empty then
			GalleryTitle.Text = "Album"
			end if
				RootURL.Text = _portalSettings.UploadDirectory & admin.convertstringtounicode(GalleryTitle.Text)
				lblInfo.Text = GetLanguage("Gal_ErrorDir")
				Return
			end if		
		end if
			
			
            Try
                If Not IO.Directory.Exists(GalleryPath) Then
				    If PortalSecurity.IsSuperUser Then
					' Super User can create a new gallery anywhere
                    IO.Directory.CreateDirectory(GalleryPath)
					else
					If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
					' admin of portal can only create a galley in his portal
					if InStr(1, LCase(GalleryURL), LCase(_portalSettings.UploadDirectory)) <> 0 then
					IO.Directory.CreateDirectory(GalleryPath)
					else
					lblInfo.Text = GetLanguage("Gal_ErrorDir")
					Return
					end if
					end if
					end If
                End If
            Catch Exc As System.Exception
                Dim strErr As String = Exc.ToString
                lblInfo.Text = strErr
                Return
            End Try

 
            admin.UpdateModuleSetting(ModuleId, "GalleryTitle", GalleryTitle.Text)
            admin.UpdateModuleSetting(ModuleId, "GalleryDescription", Description.Text)
            admin.UpdateModuleSetting(ModuleId, "RootURL", RootURL.Text)
            admin.UpdateModuleSetting(ModuleId, "StripWidth", StripWidth.Text)
            admin.UpdateModuleSetting(ModuleId, "StripHeight", StripHeight.Text)
            admin.UpdateModuleSetting(ModuleId, "Quota", txtQuota.Text)
            admin.UpdateModuleSetting(ModuleId, "MaxFileSize", txtMaxFileSize.Text)
            admin.UpdateModuleSetting(ModuleId, "MaxThumbWidth", MaxThumbWidth.Text)
            admin.UpdateModuleSetting(ModuleId, "MaxThumbHeight", MaxThumbHeight.Text)
            admin.UpdateModuleSetting(ModuleId, "FileExtensions", LCase(FileExtensions.Text))
            admin.UpdateModuleSetting(ModuleId, "MovieExtensions", LCase(MovieExtensions.Text))
            admin.UpdateModuleSetting(ModuleId, "CategoryValues", CategoryValues.Text)
            admin.UpdateModuleSetting(ModuleId, "BuildCacheOnStart", BuildCacheOnStart.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "IsFixedSize", chkFixedSize.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "FixedWidth", FixedWidth.Text)
            admin.UpdateModuleSetting(ModuleId, "Quality", Quality.Text)
            admin.UpdateModuleSetting(ModuleId, "FixedHeight", FixedHeight.Text)
            admin.UpdateModuleSetting(ModuleId, "IsKeepSource", chkKeepSource.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "SlideshowSpeed", SlideshowSpeed.Text)
            admin.UpdateModuleSetting(ModuleId, "IsPrivate", chkPrivate.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "IsIntegrated", chkForumIntegrate.Checked.ToString)
            Admin.UpdateModuleSetting(ModuleId, "SlideshowPopup", chkPopup.Checked.ToString)
            Admin.UpdateModuleSetting(ModuleId, "InfoBule", chkInfoBule.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "AllowDownload", chkDownload.Checked.ToString)
            ' admin.UpdateModuleSetting(ModuleId, "IsAvatarsGallery", chkAvatarsGallery.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "Owner", txtOwner.Text)
            admin.UpdateModuleSetting(ModuleId, "OwnerID", txtOwnerID.Text)
            admin.UpdateModuleSetting(ModuleId, "DisplayOption", ddlDisplayOption.SelectedItem.Value.ToString)

            If chkForumIntegrate.Checked Then
                admin.UpdateModuleSetting(ModuleId, "IntegratedForumGroup", ddlForumGroup.SelectedItem.Value.ToString)
            End If

            GalleryConfig.ResetGalleryConfig(ModuleId)

			Dim Zrequest As GalleryRequest = New GalleryRequest(ModuleId)
			Zrequest.Folder.REPopulate()
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub

        Private Function BindForumGroup() As Boolean
         
		Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
   
            Try
                Dim dbForum As New ForumDB
                ddlForumGroup.DataSource = dbForum.TTTForum_GetGroups(_portalSettings.PortalId, 0)
                ddlForumGroup.DataBind()
                If config.IntegratedForumGroup > 0 Then
                    ddlForumGroup.Items.FindByValue(ConvertString(config.IntegratedForumGroup)).Selected = True

                Else
                    ddlForumGroup.SelectedIndex = 0
                End If
                If BindForum() Then
                    ListAlbum(ddlAlbums)
                End If
            Catch Exc As System.Exception
                lblInfo.Text = GetLanguage("Gal_ErrorF")
                Return False
            End Try
            Return True

        End Function

        Private Function BindForum() As Boolean
            Try
                Dim dbForum As New ForumDB
                Dim _groupID As Integer = Int16.Parse(ddlForumGroup.Items(ddlForumGroup.SelectedIndex).Value)

                ddlForum.Items.Clear()
                ddlForum.Items.Add(New ListItem(GetLanguage("Gal_SelectF"), "0"))
                Dim dr As SqlDataReader = dbForum.TTTForum_GetForums(_groupID)
                While dr.Read
                    ddlForum.Items.Add(New ListItem(ConvertString(dr("Name")), ConvertString(dr("ForumID"))))
                End While
                dr.Close()
                ddlForum.Items.FindByText(GetLanguage("Gal_SelectF")).Selected = True
            Catch Exc As System.Exception
                lblInfo.Text = GetLanguage("Gal_ErrorF")
                Return False
            End Try
            Return True

        End Function

        Public Sub ListAlbum(ByVal ListControl As DropDownList)

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim Zconfig As GalleryConfig = GalleryConfig.GetGalleryConfig(ModuleId)
            lblInfo.Text = ""

            Dim galleryPath As String = Zconfig.RootURL
            Dim album As String

            Dim physicalPath As String = Server.MapPath(galleryPath)

            'Create album folder if was accidentally deleted to avoid error
            Try
                If Not Directory.Exists(physicalPath) Then
                    Directory.CreateDirectory(physicalPath)
                End If
            Catch Exc As System.Exception
            End Try

            Dim albums() As String = Directory.GetDirectories(physicalPath)

            ListControl.Items.Clear()
            ListControl.Items.Insert(0, New ListItem(GetLanguage("Gal_SelectA"), "-1"))

            Try
                For Each album In albums
                    Dim albumName As String = System.IO.Path.GetFileName(album)
                    If Not LCase(albumName) = LCase(GalleryConfig.DefaultSourceFolder) And Not LCase(albumName) = LCase(GalleryConfig.DefaultThumbFolder) And Not LCase(albumName) = LCase(GalleryConfig.DefaultResFolder) And Not LCase(albumName) = LCase(GalleryConfig.DefaultTempFolder) Then
                        ListControl.Items.Add(System.IO.Path.GetFileName(album))
                    End If
                Next

                ListControl.Items.FindByText(GetLanguage("Gal_SelectA")).Selected = True

            Catch Exc As System.Exception
                lblInfo.Text = Exc.ToString
            End Try

        End Sub

        Private Sub ddlForumGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlForumGroup.SelectedIndexChanged
            BindForum()
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String))

        End Sub

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click

            lblInfo.Text = ""
            If ddlForum.SelectedIndex = 0 OrElse Me.ddlAlbums.SelectedIndex = 0 Then
                lblInfo.Text = GetLanguage("Gal_ErrorSelectAF")
                Return
            End If
            Dim ZforumID As Integer = Int16.Parse(Me.ddlForum.SelectedItem.Value)
            Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)
            With forumInfo
                .IsIntegrated = True
                .IntegratedGallery = ModuleId
                .IntegratedAlbumName = ddlAlbums.SelectedItem.Text
                .UpdateForumInfo()
                .ResetForumInfo(ZforumID)
            End With
            PopulateIntegration()

        End Sub

        Private Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove.Click

            lblInfo.Text = ""
            If Me.lstIntegrate.SelectedIndex < 0 Then
                lblInfo.Text = GetLanguage("Gal_ErrorSelectL") 
                Return
            End If
            Dim ZforumID As Integer = Int16.Parse(lstIntegrate.SelectedItem.Value)
            Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(ZforumID)
            With forumInfo
                .IsIntegrated = False
                .IntegratedGallery = 0
                .IntegratedAlbumName = ""
                .UpdateForumInfo()
                .ResetForumInfo(ZforumID)
            End With
            PopulateIntegration()
        End Sub

        Private Sub PopulateIntegration()
            Dim dbGallery As New TTT_GalleryDB

            lstIntegrate.DataSource = dbGallery.TTTGallery_GetIntegratedForum(ModuleId)
            lstIntegrate.DataBind()
        End Sub

        Private Sub chkForumIntegrate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkForumIntegrate.CheckedChanged

            Me.pnlForumSelect.Visible = chkForumIntegrate.Checked
            If Me.chkForumIntegrate.Checked Then
                BindForumGroup()
                PopulateIntegration()
            End If
        End Sub

        Private Sub ctlUsers_UserSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlUsers.UserSelected
            Dim myUser As ForumUser = CType(ctlUsers.SelectedUser, ForumUser)

            txtOwner.Text = myUser.Name
            txtOwnerID.Text = myUser.UserID.ToString

            pnlSelectOwner.Visible = False

        End Sub

        Private Sub chkFixedSize_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFixedSize.CheckedChanged
            pnlFixedSize.Visible = chkFixedSize.Checked
        End Sub

        Private Sub chkPrivate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPrivate.CheckedChanged
            pnlPrivate.Visible = chkPrivate.Checked
        End Sub

        Private Sub btnEditOwner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditOwner.Click
            pnlSelectOwner.Visible = True
            ctlUsers.BindUsers()
        End Sub
    End Class
End Namespace