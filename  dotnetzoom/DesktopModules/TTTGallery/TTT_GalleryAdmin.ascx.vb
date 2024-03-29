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
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by Ren� Boulard ( http://www.reneboulard.qc.ca)'
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
        Protected WithEvents txtgoogleAPI As System.Web.UI.WebControls.TextBox
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
        Protected WithEvents CheckboxGPS As System.Web.UI.WebControls.CheckBox
        Protected WithEvents CheckboxIndex As System.Web.UI.WebControls.CheckBox
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
        Protected WithEvents chkAuthRoles As System.Web.UI.WebControls.CheckBoxList
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
            RegularExpressionValidator10.ErrorMessage = GetLanguage("need_number")
            RegularExpressionValidator5.ErrorMessage = GetLanguage("need_number")
            RegularExpressionValidator6.ErrorMessage = GetLanguage("need_number")
            RegularExpressionValidator1.ErrorMessage = GetLanguage("need_number")
            RegularExpressionValidator2.ErrorMessage = GetLanguage("need_number")
            RegularExpressionValidator3.ErrorMessage = GetLanguage("need_number")
            RegularExpressionValidator4.ErrorMessage = GetLanguage("need_number")
            RegularExpressionValidator7.ErrorMessage = GetLanguage("need_number")
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
                txtgoogleAPI.Text = config.GoogleAPI

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

                CheckboxIndex.Checked = (config.CheckboxIndex = True)
                CheckboxGPS.Checked = (config.CheckboxGPS = True)


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
                chkAuthRoles.Items.Clear()
                Dim allItems As New ListItem()
                allItems.Text = GetLanguage("ms_all_users")
                allItems.Value = glbRoleAllUsers


                Dim unauthItems As New ListItem()
                unauthItems.Text = GetLanguage("ms_non_authorized")
                unauthItems.Value = glbRoleUnauthUser

                If InStr(1, config.DownloadRoles, allItems.Value & ";") > 0 Then
                    allItems.Selected = True
                End If
                chkAuthRoles.Items.Add(allItems)

                If InStr(1, config.DownloadRoles, unauthItems.Value & ";") > 0 Then
                    unauthItems.Selected = True
                End If
                chkAuthRoles.Items.Add(unauthItems)

                Dim objUser As New UsersDB()

                Dim ViewRoles As SqlDataReader = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))
                While ViewRoles.Read()

                    Dim item As New ListItem()
                    item.Text = CType(ViewRoles("RoleName"), String)
                    item.Value = ViewRoles("RoleID").ToString()

                    If InStr(1, config.DownloadRoles, item.Value & ";") > 0 Then
                        item.Selected = True
                    End If

                    chkAuthRoles.Items.Add(item)

                End While

                ViewRoles.Close()


                chkAvatarsGallery.Checked = config.IsAvatarsGallery


                Dim TGalleryUser As GalleryUser = New GalleryUser(config.OwnerID)
                txtOwner.Text = TGalleryUser.FullName
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

            Dim NeedToRepopulate As Boolean = False
            lblInfo.Text = ""

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim GalleryURL As String = RootURL.Text
            Dim GalleryPath As String = Server.MapPath(GalleryURL)
            Dim Admin As New AdminDB()

            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) And Not PortalSecurity.IsSuperUser Then
                If InStr(1, LCase(GalleryURL), LCase(_portalSettings.UploadDirectory)) = 0 Then
                    If GalleryTitle.Text = String.Empty Then
                        GalleryTitle.Text = "Album"
                    End If
                    If RootURL.Text.ToLower <> config.RootURL.ToLower Then
                        ' only webmestre can change directory outside portal
                        RootURL.Text = _portalSettings.UploadDirectory & Admin.convertstringtounicode(GalleryTitle.Text)
                        lblInfo.Text = GetLanguage("Gal_ErrorDir")
                        Return
                    End If
                End If
            End If


            Try
                If Not IO.Directory.Exists(GalleryPath) Then
                    If PortalSecurity.IsSuperUser Then
                        ' Super User can create a new gallery anywhere
                        IO.Directory.CreateDirectory(GalleryPath)
                        NeedToRepopulate = True
                    Else
                        If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                            ' admin of portal can only create a galley in his portal
                            If InStr(1, LCase(GalleryURL), LCase(_portalSettings.UploadDirectory)) <> 0 Then
                                IO.Directory.CreateDirectory(GalleryPath)
                                NeedToRepopulate = True
                            Else
                                lblInfo.Text = GetLanguage("Gal_ErrorDir")
                                Return
                            End If
                        End If
                    End If
                End If
            Catch Exc As System.Exception
                Dim strErr As String = Exc.ToString
                lblInfo.Text = strErr
                Return
            End Try

            Admin.UpdateModuleSetting(ModuleId, "GoogleAPI", txtgoogleAPI.Text)
            Admin.UpdateModuleSetting(ModuleId, "GalleryTitle", GalleryTitle.Text)
            Admin.UpdateModuleSetting(ModuleId, "GalleryDescription", Description.Text)

            Admin.UpdateModuleSetting(ModuleId, "RootURL", RootURL.Text)

            Admin.UpdateModuleSetting(ModuleId, "StripWidth", StripWidth.Text)
            Admin.UpdateModuleSetting(ModuleId, "StripHeight", StripHeight.Text)
            If IsNumeric(txtQuota.Text) Then
                Admin.UpdateModuleSetting(ModuleId, "Quota", txtQuota.Text)
            Else
                Admin.UpdateModuleSetting(ModuleId, "Quota", "0")
            End If
            Admin.UpdateModuleSetting(ModuleId, "MaxFileSize", txtMaxFileSize.Text)
            Admin.UpdateModuleSetting(ModuleId, "MaxThumbWidth", MaxThumbWidth.Text)
            Admin.UpdateModuleSetting(ModuleId, "MaxThumbHeight", MaxThumbHeight.Text)
            Admin.UpdateModuleSetting(ModuleId, "FileExtensions", LCase(FileExtensions.Text))
            Admin.UpdateModuleSetting(ModuleId, "MovieExtensions", LCase(MovieExtensions.Text))
            Admin.UpdateModuleSetting(ModuleId, "CategoryValues", CategoryValues.Text)
            Admin.UpdateModuleSetting(ModuleId, "BuildCacheOnStart", BuildCacheOnStart.Checked.ToString)

            Admin.UpdateModuleSetting(ModuleId, "CheckboxIndex", CheckboxIndex.Checked.ToString)
            Admin.UpdateModuleSetting(ModuleId, "CheckboxGPS", CheckboxGPS.Checked.ToString)


            Admin.UpdateModuleSetting(ModuleId, "IsFixedSize", chkFixedSize.Checked.ToString)
            Admin.UpdateModuleSetting(ModuleId, "FixedWidth", FixedWidth.Text)
            Admin.UpdateModuleSetting(ModuleId, "Quality", Quality.Text)
            Admin.UpdateModuleSetting(ModuleId, "FixedHeight", FixedHeight.Text)
            Admin.UpdateModuleSetting(ModuleId, "IsKeepSource", chkKeepSource.Checked.ToString)
            Admin.UpdateModuleSetting(ModuleId, "SlideshowSpeed", SlideshowSpeed.Text)
            Admin.UpdateModuleSetting(ModuleId, "IsPrivate", chkPrivate.Checked.ToString)
            Admin.UpdateModuleSetting(ModuleId, "IsIntegrated", chkForumIntegrate.Checked.ToString)
            Admin.UpdateModuleSetting(ModuleId, "SlideshowPopup", chkPopup.Checked.ToString)
            Admin.UpdateModuleSetting(ModuleId, "InfoBule", chkInfoBule.Checked.ToString)
            Admin.UpdateModuleSetting(ModuleId, "AllowDownload", chkDownload.Checked.ToString)
            ' admin.UpdateModuleSetting(ModuleId, "IsAvatarsGallery", chkAvatarsGallery.Checked.ToString)
            Admin.UpdateModuleSetting(ModuleId, "Owner", txtOwner.Text)
            Admin.UpdateModuleSetting(ModuleId, "OwnerID", txtOwnerID.Text)
            Admin.UpdateModuleSetting(ModuleId, "DisplayOption", ddlDisplayOption.SelectedItem.Value.ToString)

            If chkForumIntegrate.Checked Then
                Admin.UpdateModuleSetting(ModuleId, "IntegratedForumGroup", ddlForumGroup.SelectedItem.Value.ToString)
            End If


            Dim item As ListItem

            ' Construct Authorized Download Roles 
            Dim viewRoles As String = ""
            For Each item In chkAuthRoles.Items
                If item.Selected Then
                    viewRoles = viewRoles & item.Value & ";"
                End If
            Next item

            Admin.UpdateModuleSetting(ModuleId, "DownloadRoles", viewRoles)


            Dim Zrequest As GalleryRequest = New GalleryRequest(ModuleId)
            If NeedToRepopulate Then Zrequest.Folder.REPopulate()
            GalleryConfig.ResetGalleryConfig(ModuleId)
            Zrequest.Folder.Reset()
            ClearModuleCache(ModuleId)

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
            lblInfo.Text = ""

            Dim galleryPath As String = config.RootURL
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

            txtOwner.Text = myUser.FullName
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