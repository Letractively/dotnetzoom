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
Imports System.Xml
Imports System.Linq
Imports System.Xml.Linq
Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports System.Text
Imports ICSharpCode.SharpZipLib.Zip

Namespace DotNetZoom
    Public Class TTT_GalleryEditAlbum
        Inherits DotNetZoom.PortalModuleControl

        ' Obtain PortalSettings from Current Context   
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

        Protected WithEvents OpenClose1 As DotNetZoom.OpenClose
        Protected WithEvents OpenClose2 As DotNetZoom.OpenClose
        Protected WithEvents OpenClose3 As DotNetZoom.OpenClose
        Protected WithEvents OpenClose4 As DotNetZoom.OpenClose
        Protected WithEvents OpenClose5 As DotNetZoom.OpenClose
        Protected WithEvents OpenClose6 As DotNetZoom.OpenClose
        Protected WithEvents OpenClose7 As DotNetZoom.OpenClose
        Protected WithEvents UploadGPXFile As System.Web.UI.HtmlControls.HtmlInputFile



		Protected WithEvents ctlUsers As TTT_UsersControl
		Protected WithEvents pnlSelectOwner As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlMapOption As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents FolderImage As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtWaterMark As System.Web.UI.WebControls.TextBox

        Protected WithEvents ClearCache As System.Web.UI.WebControls.ImageButton
        Protected WithEvents SubAlbum As System.Web.UI.WebControls.ImageButton
        Protected WithEvents UploadImage2 As System.Web.UI.WebControls.ImageButton
        Protected WithEvents btngpsEditall As System.Web.UI.WebControls.ImageButton

        Protected WithEvents btnEditOwner As System.Web.UI.WebControls.Button
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
        Protected WithEvents txtPath As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtOwner As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents ddCategories As System.Web.UI.WebControls.DropDownList
        Protected WithEvents pnlAlbumDetails As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents grdFile As System.Web.UI.WebControls.DataGrid
        Protected WithEvents grdDir As System.Web.UI.WebControls.DataGrid
        Protected WithEvents pnlContentFile As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlContentDir As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents CancelButton As System.Web.UI.WebControls.Button
        Protected WithEvents UpdateButton As System.Web.UI.WebControls.Button
        Protected WithEvents EditMapOptions As System.Web.UI.WebControls.Button
        Protected WithEvents MakeTrack As System.Web.UI.WebControls.Button
        Protected WithEvents pnlAlbumInfo As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblAlbumTitle As System.Web.UI.WebControls.Label
        Protected WithEvents txtNewAlbum As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAlbumTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents Latitude As System.Web.UI.WebControls.TextBox
        Protected WithEvents Longitude As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAlbumDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents ddCategories2 As System.Web.UI.WebControls.DropDownList
        Protected WithEvents btnFolderSave As System.Web.UI.WebControls.Button
        Protected WithEvents btnFolderClose As System.Web.UI.WebControls.Button
        Protected WithEvents pnlAddFolder As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlAdd As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlAdd1 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblfileTitle As System.Web.UI.WebControls.Label
        Protected WithEvents lblFileType As System.Web.UI.WebControls.Label
        Protected WithEvents txtFileDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents ddCategories3 As System.Web.UI.WebControls.DropDownList
        Protected WithEvents btnAdd As System.Web.UI.WebControls.ImageButton
        Protected WithEvents lstFiles As System.Web.UI.WebControls.ListBox
        Protected WithEvents btnFileClose As System.Web.UI.WebControls.Button
        Protected WithEvents pnlAddFile As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents htmlUploadFile As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents UploadFile As System.Web.UI.HtmlControls.HtmlInputFile

        Protected WithEvents txtOwnerID As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtSortOrder As System.Web.UI.WebControls.TextBox
        Protected WithEvents dlFolders As System.Web.UI.WebControls.DataList
        Protected WithEvents grdUpload As System.Web.UI.WebControls.DataGrid
        Protected WithEvents txtFileTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblFileInfo As System.Web.UI.WebControls.Label
        Protected WithEvents btnFileUpload As System.Web.UI.WebControls.Button

        Protected WithEvents txtquality As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtSizeH As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtSizeL As System.Web.UI.WebControls.TextBox

        Protected WithEvents UploadImage As System.Web.UI.WebControls.Button
        Protected WithEvents imgFile As System.Web.UI.WebControls.Image
        Protected WithEvents imgFileIcon As System.Web.UI.WebControls.Image
        Protected WithEvents gpsIconImage As System.Web.UI.WebControls.DataList
        Protected WithEvents div1 As System.Web.UI.HtmlControls.HtmlGenericControl


        Protected ZuploadCollection As GalleryUploadCollection
        Protected Zrequest As GalleryRequest
        Protected Zconfig As GalleryConfig
        Protected Zfolder As GalleryFolder
        Protected ZmoduleID As Integer
		Protected ZuserID As Integer = 0
        Protected ZGalleryAuthority As Boolean = False
        Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
        Protected WithEvents lblRefuse As System.Web.UI.WebControls.Label
        Protected WithEvents pnlRefuse As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlMain As System.Web.UI.WebControls.PlaceHolder
        Dim strFolderInfo As String


        Protected WithEvents options_width As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_height As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_full_screen As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_center As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_zoom As System.Web.UI.WebControls.DropDownList
        Protected WithEvents options_map_opacity As System.Web.UI.WebControls.DropDownList
        Protected WithEvents options_map_type As System.Web.UI.WebControls.DropDownList

        Protected WithEvents options_doubleclick_zoom As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_mousewheel_zoom As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_centering_options As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_zoom_control As System.Web.UI.WebControls.DropDownList
        Protected WithEvents options_scale_control As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_center_coordinates As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_crosshair_hidden As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_map_opacity_control As System.Web.UI.WebControls.CheckBox




        Protected WithEvents options_map_type_control_style As System.Web.UI.WebControls.DropDownList
        Protected WithEvents options_map_type_control_filter As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_map_type_control_excluded As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents options_legend_options_legend As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_legend_options_position As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_legend_options_draggable As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_legend_options_collapsible As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_measurement_tools As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_tracklist_options_tracklist As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_tracklist_options_position As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_tracklist_options_max_width As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_tracklist_options_max_height As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_tracklist_options_desc As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_tracklist_options_zoom_links As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_tracklist_options_tooltips As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_tracklist_options_draggable As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_tracklist_options_collapsible As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_default_marker As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_shadows As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_marker_link_target As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_info_window_width As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_thumbnail_width As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_photo_size As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_hide_labels As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_label_offset As System.Web.UI.WebControls.TextBox
        Protected WithEvents options_label_centered As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_driving_directions As System.Web.UI.WebControls.CheckBox
        Protected WithEvents options_garmin_icon_set As System.Web.UI.WebControls.TextBox
        Protected WithEvents trk_info As System.Web.UI.WebControls.TextBox
        Protected WithEvents Draw_Marker As System.Web.UI.WebControls.TextBox

        Protected WithEvents txtColor As System.Web.UI.WebControls.TextBox
        Protected WithEvents lnkcolor As System.Web.UI.WebControls.HyperLink
        Protected WithEvents ddlTimeZone As System.Web.UI.WebControls.DropDownList
        Protected WithEvents ddlheure As System.Web.UI.WebControls.DropDownList
        Protected WithEvents ddljours As System.Web.UI.WebControls.DropDownList

        Protected WithEvents txtNewAlbumValidator As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents lblTimeZone As System.Web.UI.WebControls.Label

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
            'Put user code to initialize the page here

            lnkcolor.Tooltip = GetLanguage("ms_select_color")
            lnkcolor.Text = GetLanguage("ms_select_color")
            lnkcolor.NavigateUrl = "javascript:OpenColorWindow('" + TabID.ToString + "')"


            ClearCache.ToolTip = GetLanguage("Gal_Clear")
            ClearCache.Style.Add("background", "url('" & ForumConfig.DefaultImageFolder() & "forum.gif" & "') no-repeat")
            ClearCache.Style.Add("background-position", "0px -256px")
            SubAlbum.ToolTip = GetLanguage("Gal_AddFolderTip")
            UploadImage2.ToolTip = GetLanguage("Gal_AddFileTip")


            btnEditOwner.Text = GetLanguage("Gal_Select")
			btnFolderSave.Text = GetLanguage("enregistrer")
			btnFolderSave.ToolTip = GetLanguage("Gal_SaveA")
			btnFolderClose.Text = GetLanguage("annuler")
			btnFolderClose.ToolTip = GetLanguage("Gal_Cancel")
			btnFileClose.Text = GetLanguage("annuler")
            btnFileClose.ToolTip = GetLanguage("annuler")
			btnFileUpload.Text = GetLanguage("upload")
            btnFileUpload.ToolTip = GetLanguage("upload")
            UploadImage.Text = GetLanguage("upload")
            UploadImage.ToolTip = GetLanguage("upload")
            CancelButton.Text = GetLanguage("annuler")
            UpdateButton.Text = GetLanguage("enregistrer")
            EditMapOptions.Text = GetLanguage("Gal_SetUpMap")
            MakeTrack.Text = GetLanguage("Gal_MakeTrack")
            MakeTrack.ToolTip = GetLanguage("Gal_MakeTrack")
            EditMapOptions.ToolTip = GetLanguage("Gal_SetUpMap")
			CancelButton.ToolTip = GetLanguage("Gal_CancelTip")
			UpdateButton.ToolTip = GetLanguage("Gal_UpdateTip")
			btnAdd.ToolTip = GetLanguage("Gal_btnAddTip")
            btnAdd.AlternateText = GetLanguage("Gal_btnAddAlt")
            txtNewAlbum.ToolTip = GetLanguage("Gal_Name_Valid")
            txtNewAlbumValidator.ErrorMessage = GetLanguage("Gal_Name_Not_Valid")
			grdUpload.Columns(1).HeaderText = GetLanguage("Gal_Name")
			grdUpload.Columns(2).HeaderText = GetLanguage("Gal_TitleI")
			grdUpload.Columns(3).HeaderText = GetLanguage("Gal_Desc")
			grdUpload.Columns(4).HeaderText = GetLanguage("Gal_Cat")
			grdDir.Columns(1).HeaderText = GetLanguage("Gal_Album")
			grdDir.Columns(2).HeaderText = GetLanguage("Gal_Prop")
			grdDir.Columns(3).HeaderText = GetLanguage("Gal_TitleI")
			grdDir.Columns(4).HeaderText = GetLanguage("Gal_Cat")
			grdDir.Columns(5).HeaderText = GetLanguage("Gal_Desc")

			grdFile.Columns(1).HeaderText = GetLanguage("Gal_File")
			grdFile.Columns(2).HeaderText = GetLanguage("Gal_Prop")
			grdFile.Columns(3).HeaderText = GetLanguage("Gal_TitleI")
			grdFile.Columns(4).HeaderText = GetLanguage("Gal_Cat")
            grdFile.Columns(5).HeaderText = GetLanguage("Gal_Desc")
 
			
            If Request.IsAuthenticated Then
                ZuserID = Int16.Parse(Context.User.Identity.Name)
            End If

            If IsNumeric(Request.Params("mid")) Then
                ZmoduleID = Int32.Parse(Request.Params("mid"))
            End If

            ' Dim _path As String = ""
            ' If Not Request.Params("path") Is Nothing Then
            '     _path = Request.Params("path")
            ' End If

            btnAdd.Attributes.Add("onclick", "toggleBox('" + btnAdd.ClientID + "',0);toggleBox('rotation',1)")

            Zconfig = GalleryConfig.GetGalleryConfig(ZmoduleID)
            Zrequest = New GalleryRequest(ZmoduleID)
            Zfolder = Zrequest.Folder

            If Zconfig.IsValidPath Then
                CheckPopulate()
            Else
                'New so go to set up
                Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "edit=control&editpage=1&mid=" & ZmoduleID.ToString), True)
            End If




            With Zfolder
                lblAlbumTitle.Text = Replace(GetLanguage("Gal_AddF"), "{folderURL}", Zfolder.URL & "/")
                lblfileTitle.Text = Replace(GetLanguage("Gal_AddFF"), "{folderURL}", Zfolder.URL & "/")
            End With

            Authorize()
            BindCategories()

            If Not Page.IsPostBack Then
                txtColor.Text = "#59ACFF"
                OpenClose1.What = GetLanguage("Gal_MapSettings1")
                OpenClose2.What = GetLanguage("Gal_MapSettings2")
                OpenClose3.What = GetLanguage("Gal_MapSettings3")
                OpenClose4.What = GetLanguage("Gal_MapSettings4")
                OpenClose5.What = GetLanguage("Gal_MapSettings5")
                OpenClose6.What = GetLanguage("Gal_MapSettings6")
                OpenClose7.What = GetLanguage("Gal_MapSettings7")


                pnlMapOption.Visible = False
                txtquality.Text = Zconfig.Quality.ToString
                txtSizeH.Text = Zconfig.MaximumThumbHeight.ToString
                txtSizeL.Text = Zconfig.MaximumThumbWidth.ToString
                If Not Zfolder.Parent Is Nothing Then
                    FolderImage.Visible = True
                End If


                If Not Session("UrlReferrer") Is Nothing Then
                    ViewState("UrlReferrer") = Session("UrlReferrer")
                Else
                    If Not Request.UrlReferrer Is Nothing Then
                        ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                    Else
                        ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                    End If
                End If


                BindData()
                BuildStringInfo()
                ' if Admin then can edit owner

                If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                            OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                OrElse (PortalSecurity.IsInRoles(CType(PortalSettings.GetEditModuleSettings(ZmoduleID), ModuleSettings).AuthorizedEditRoles.ToString) = True) Then
                    btnEditOwner.Visible = True
                Else
                    btnEditOwner.Visible = False
                End If


                lblInfo.Text = strFolderInfo
                pnlAdd1.Visible = True

                btnFileUpload.Visible = False

                If Zfolder.Parent Is Nothing Then
                    lblHeader.Text = GetLanguage("Gal_ModG")
                Else
                    lblHeader.Text = GetLanguage("Gal_ModA")
                End If

                If Not Request.Params("action") Is Nothing Then
                    SetUpAction(Request.Params("action"))
                End If
                EditMapOptions.Visible = Zconfig.CheckboxGPS
                MakeTrack.Visible = Zconfig.CheckboxGPS
                If Zconfig.CheckboxGPS Then
                    grdDir.Columns(8).Visible = False
                    If Request.Params("mapoptions") <> "" Then
                        pnlMapOption.Visible = True
                        UploadImage2.Visible = False
                        ClearCache.Visible = False
                        SubAlbum.Visible = False
                        pnlAddFile.Visible = False
                        pnlAdd.Visible = False
                        pnlAdd1.Visible = True
                        btnFileUpload.Visible = False
                        pnlAddFolder.Visible = False
                        pnlAlbumDetails.Visible = True
                        pnlContentDir.Visible = False
                        pnlContentFile.Visible = False
                        PopulateMapOptions()
                    End If
                End If




            End If

        End Sub

        Private Sub CheckPopulate()
            If Not Zconfig.RootFolder.IsPopulated Then
            End If

            If Not Zrequest.Folder.IsPopulated Then
            End If

        End Sub

        Private Function Authorize() As Boolean
            ' Authorized gallery
            If ZuserID > 0 Then
                If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                   OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                   OrElse (Zconfig.OwnerID = ZuserID) OrElse Zfolder.OwnerID = ZuserID Then
                    ZGalleryAuthority = True
                Else
                    ZGalleryAuthority = False
                End If
            End If

            'BRT: only allow administrators or owners to edit Title, Description and Categories
            txtTitle.Enabled = ZGalleryAuthority
            txtDescription.Enabled = ZGalleryAuthority
            ddCategories.Enabled = ZGalleryAuthority
            txtSortOrder.Enabled = ZGalleryAuthority
            Latitude.Enabled = ZGalleryAuthority
            Longitude.Enabled = ZGalleryAuthority
            'BRT: end


            If Zrequest.GalleryConfig.IsPrivate AndAlso Not ZGalleryAuthority Then
                Me.lblRefuse.Text = GetLanguage("Gal_Refuse")
                Me.pnlMain.Visible = False
                Me.UpdateButton.Visible = False
                Me.EditMapOptions.Visible = False
                UploadImage2.Visible = False
                ClearCache.Visible = False
                SubAlbum.Visible = False
                Me.pnlRefuse.Visible = True
                Return False
            Else
                Me.pnlMain.Visible = True
                Me.UpdateButton.Visible = True
                Me.pnlRefuse.Visible = False
                UploadImage2.Visible = ZGalleryAuthority
                ClearCache.Visible = ZGalleryAuthority
                SubAlbum.Visible = ZGalleryAuthority
                Return True
            End If

            Return True 'for public gallery

        End Function


        Private Sub BindData()

            dlFolders.DataSource = Zrequest.FolderPaths
            dlFolders.DataBind()

            With Zfolder
                txtPath.Text = Zfolder.URL
                txtName.Text = .Name
                Dim TGalleryUser As GalleryUser = New GalleryUser(.OwnerID)
                txtOwner.Text = TGalleryUser.UserName
                txtOwnerID.Text = .OwnerID.ToString
                txtTitle.Text = .Title
				txtSortOrder.Text = .Sort
                txtDescription.Text = .Description
                If Not Session("reload") Is Nothing Then
                    Session("reload") = Nothing
                    imgFile.ImageUrl = .ThumbNail + "?" + DateTime.Now.ToString("yyyyMMddHHmmss")
                Else
                    imgFile.ImageUrl = .ThumbNail
                End If

            End With
            Dim objAdmin As New AdminDB()
            ddlTimeZone.DataSource = objAdmin.GetTimeZoneCodes(GetLanguage("N"))
            ddlTimeZone.DataBind()
            Dim temptemdate As DateTime
            temptemdate = DateTime.Now
            lblTimeZone.Text = temptemdate.ToUniversalTime().ToString
            'DateTime.Now().AddMinutes(GetTimeDiff(_portalSettings.TimeZone)).ToString()
            Try
                ddlTimeZone.SelectedValue = _portalSettings.TimeZone.ToString
            Catch ex As Exception
                ddlTimeZone.SelectedValue = "0"
            End Try



            If Not Zfolder.Parent Is Nothing Then 
                Dim metaData As New GalleryXML(Zrequest.Folder.Parent.Path)
                Latitude.Text = metaData.latitude(Zrequest.Folder.Name)
                Longitude.Text = metaData.Longitude(Zrequest.Folder.Name)
                ' GPS ICON gpsIconImage
                Dim items() As String
                Dim item As String
                Dim slImage As FolderDetail
                items = System.IO.Directory.GetFiles(Request.MapPath("/images/gps/"))
                Dim slImages As New ArrayList()
                Dim strExtension As String

                For Each item In items
                    strExtension = IO.Path.GetExtension(item)
                    'Check Valid Type here
                    If Zconfig.IsValidImageType(strExtension) Then
                        slImage = New FolderDetail()
                        slImage.Name = IO.Path.GetFileName(item)
                        slImage.URL = "/images/gps/" + IO.Path.GetFileName(item)
                        slImages.Add(slImage)
                    End If
                Next
                gpsIconImage.DataSource = slImages
                gpsIconImage.DataBind()
                gpsIconImage.Visible = Zconfig.CheckboxGPS
                imgFileIcon.ImageUrl = metaData.gpsicon(Zrequest.Folder.Name)
                imgFileIcon.Visible = Zconfig.CheckboxGPS
                div1.Visible = Zconfig.CheckboxGPS
            End If
            BindChildItems()

        End Sub


        Private Sub BindMapControl_Excluded(WhatToBind As String)

            Dim I As Integer
            For I = 1 To options_map_type_control_excluded.Items.Count
                options_map_type_control_excluded.Items(I - 1).Selected = True
                If InStr(WhatToBind, options_map_type_control_excluded.Items(I - 1).Value) > 0 Then
                    options_map_type_control_excluded.Items(I - 1).Selected = False
                End If
            Next

        End Sub


        Private Sub BindCategories()

            Dim catList As ArrayList = Zconfig.Categories
            Dim catString As String

            ' Clear existing items in checkboxlist
            ddCategories.Items.Clear()
            Dim EmptyItem As New ListItem
            EmptyItem.Value = ""
            ddCategories.Items.Add(EmptyItem)
            For Each catString In catList

                Dim catItem As New ListItem()
                catItem.Value = catString
                catItem.Selected = False


                If InStr(1, Zfolder.Categories, catString) > 0 Then
                    catItem.Selected = True
                End If

                'list category for current item
                ddCategories.Items.Add(catItem)

            Next

            If Not ddCategories.Items.FindByValue(Zfolder.Categories) Is Nothing Then
                ddCategories.Items.FindByValue(Zfolder.Categories).Selected = True
            End If

            ddCategories2.DataSource = ddCategories.Items
            ddCategories2.DataBind()

            ddCategories3.DataSource = ddCategories.Items
            ddCategories3.DataBind()

        End Sub

        Private Sub BindChildItems()

            grdFile.Columns(8).Visible = False
            grdFile.Columns(9).Visible = False
            btngpsEditall.Visible = False
            pnlAdd.Visible = False


            grdFile.DataSource = Zrequest.FileItems
            grdFile.PageSize = (Zrequest.FileItems.Count + 1)

            grdDir.DataSource = Zrequest.SubAlbumItems

            grdDir.PageSize = grdDir.PageSize + (Zrequest.SubAlbumItems.Count + 1)



            If Zrequest.SubAlbumItems.Count > 0 Then
                pnlContentDir.Visible = True
            Else
                pnlContentDir.Visible = False
            End If

            If Zrequest.FileItems.Count > 0 Then
                pnlContentFile.Visible = True

            Else
                pnlContentFile.Visible = False
                pnlAdd.Visible = False
            End If

            grdDir.DataBind()
            grdFile.DataBind()
            If Zconfig.CheckboxGPS Then
                grdFile.Columns(8).Visible = pnlAdd.Visible
                grdFile.Columns(9).Visible = pnlAdd.Visible
            Else
                pnlAdd.Visible = False
                grdFile.Columns(8).Visible = False
                grdFile.Columns(9).Visible = False
            End If


        End Sub

        Private Sub BuildStringInfo()

            lblFileType.Text = Replace(GetLanguage("Gal_FileInfo"), "{fileext}", Replace(Zrequest.GalleryConfig.FileExtensions, ";", " "))
            lblFileType.Text = Replace(lblFileType.Text, "{movieext}", Replace(Zrequest.GalleryConfig.MovieExtensions, ";", " "))
            lblFileType.Text = Replace(lblFileType.Text, "{MaxFileSize}", Zrequest.GalleryConfig.MaxFileSize.ToString)
            strFolderInfo = Replace(GetLanguage("Gal_FileInfo1"), "{FileItemsCount}", Zrequest.FileItems.Count.ToString)
            strFolderInfo = Replace(strFolderInfo, "{SubAlbumItems}", Zrequest.SubAlbumItems.Count.ToString)

        End Sub

        Private Sub BindUpload()
            Me.grdUpload.DataSource = Nothing
        End Sub


        Private Sub btnFolderSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFolderSave.Click

            Dim Zconfig As GalleryConfig = Zrequest.GalleryConfig

            If Not Len(txtNewAlbum.Text) = 0 Then
                Dim albumName As String = txtNewAlbum.Text
                'Check name compatibility
                Dim albumTitle As String = Me.txtAlbumTitle.Text
                Dim albumDescription As String = txtAlbumDescription.Text
                Dim categories As String = ddCategories2.SelectedItem.Value
                lblInfo.Text = strFolderInfo
                lblInfo.Text += Zfolder.CreateChild(albumName, albumTitle, albumDescription, categories, ZuserID)

                UploadImage2.Visible = ZGalleryAuthority
                ClearCache.Visible = ZGalleryAuthority
                SubAlbum.Visible = ZGalleryAuthority

                pnlAlbumDetails.Visible = True

                pnlAddFolder.Visible = False
                pnlAdd1.Visible = True


                Zrequest.Folder.Reset()
                GalleryConfig.ResetGalleryConfig(ZmoduleID)
                ClearModuleCache(ZmoduleID)
                If Not Request.Params("action") Is Nothing Then
                    Session("UrlReferrer") = Nothing
                    Response.Redirect(CType(ViewState("UrlReferrer"), String))
                End If
                Zconfig = New GalleryConfig(ZmoduleID)
                Zrequest = New GalleryRequest(ZmoduleID)
                Zfolder = Zrequest.Folder
                CheckPopulate()
                BindData()


                If Zrequest.SubAlbumItems.Count > 0 Then
                    pnlContentDir.Visible = True
                Else
                    pnlContentDir.Visible = False
                End If

                If Zrequest.FileItems.Count > 0 Then
                    pnlContentFile.Visible = True

                Else
                    pnlContentFile.Visible = False
                    pnlAdd.Visible = False
                End If
            End If

        End Sub

        Private Function CheckQuota() As Boolean
            lblFileInfo.Text = ""
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' only do if Quota
            CheckQuota = True
            If _portalSettings.HostSpace <> 0 Or ZRequest.GalleryConfig.Quota <> 0 Then
                Dim StrFolder As String
                Dim SpaceUsed As Double

                Dim objAdmin As New AdminDB()

                If _portalSettings.HostSpace <> 0 Then
                    strFolder = Request.MapPath(_portalSettings.UploadDirectory)
                    SpaceUsed = objAdmin.GetDirectorySpaceUsed(strFolder)
                    ' CheckPortal Quota
                    If ((SpaceUsed / 1048576) >= _portalSettings.HostSpace) And (Not (Request.Params("hostpage") Is Nothing)) Then
                        lblFileInfo.Text = GetLanguage("Gal_NoSpaceLeft")
                        Return False
                    End If
                End If

                If ZRequest.GalleryConfig.Quota <> 0 Then
                    strFolder = HttpContext.Current.Request.MapPath(ZRequest.GalleryConfig.RootURL)
                    SpaceUsed = objAdmin.GetDirectorySpaceUsed(strFolder)


                    If ((SpaceUsed / 1048576) >= ZRequest.GalleryConfig.Quota) Then
                        SpaceUsed = SpaceUsed / 1048576
                        lblFileInfo.Text += "<br>" & GetLanguage("Gal_QuotaInfo2") & "<br>" & GetLanguage("Gal_QuotaInfo1")
                        lblFileInfo.Text = replace(lblFileInfo.Text, "{Quota}", Format(ZRequest.GalleryConfig.Quota, "#,##0.00"))
                        lblFileInfo.Text = replace(lblFileInfo.Text, "{SpaceUsed}", Format(SpaceUsed, "#,##0.00"))
                        lblFileInfo.Text = replace(lblFileInfo.Text, "{SpaceLeft}", Format(ZRequest.GalleryConfig.Quota - SpaceUsed, "#,##0.00"))
                        Return False
                    End If
                End If
            End If
        End Function


        
        Private Sub grdDir_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDir.ItemCommand

            Dim itemIndex As Integer = Int16.Parse((CType(e.CommandSource, ImageButton).CommandArgument))
            'Dim name As String = (CType(e.CommandSource, ImageButton).CommandArgument)
            Dim selItem As IGalleryObjectInfo = CType(Zfolder.List.Item(itemIndex), IGalleryObjectInfo)


            Select Case (CType(e.CommandSource, ImageButton).CommandName)

                Case "delete"
                    lblInfo.Text = strFolderInfo
                    UpdateFolderSize(Zrequest.GalleryConfig, Zfolder.DeleteChild(selItem))

                    Zrequest = New GalleryRequest(ZmoduleID)

                    Zrequest.Folder.Reset()
                    If Not Zfolder.Parent Is Nothing Then
                        Zrequest.Folder.Parent.Reset()
                    End If
                    GalleryConfig.ResetGalleryConfig(ZmoduleID)
                    ClearModuleCache(ZmoduleID)
                    Zconfig = New GalleryConfig(ZmoduleID)
                    Zrequest = New GalleryRequest(ZmoduleID)
                    Zfolder = Zrequest.Folder

                    CheckPopulate()
                    BindData()

                Case "edit"
                    Dim url As String = ""

                    If (Not selItem Is Nothing) AndAlso (selItem.IsFolder) Then
                        url = GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditAlbum & "&tabid=" & TabId & "&mid=" & ZmoduleID.ToString & "&path=" & CType(selItem, IGalleryObjectInfo).URL
                    ElseIf (Not selItem Is Nothing) AndAlso (Not selItem.IsFolder) Then
                        url = GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditFile & "&tabid=" & TabId & "&mid=" & ZmoduleID.ToString & "&path=" & CType(Zfolder, IGalleryObjectInfo).URL & "&editindex=" & selItem.Index.ToString
                    End If

                    If Len(url) > 0 Then
                        Session("UrlReferrer") = ViewState("UrlReferrer").ToString
                        Response.Redirect(url)
                    Else
                        lblInfo.Text = GetLanguage("Gal_FileOE")
                    End If


                Case "gps"

                    Response.Redirect("http://www.gpsvisualizer.com/draw")


            End Select

        End Sub

        Private Sub grdDir_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdDir.ItemCreated
            Dim cmdDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
            If Not cmdDelete Is Nothing Then

                Dim createdItem As IGalleryObjectInfo = CType(e.Item.DataItem, IGalleryObjectInfo)
                Dim confirmScript As String = ""

                If (Not createdItem Is Nothing) AndAlso (createdItem.IsFolder) Then
                    confirmScript = "javascript: return confirm('" & rtesafe(GetLanguage("request_confirm_erasealbum")) & "')"
                    confirmScript = confirmScript.replace("{items}", createdItem.Size.ToString)
                ElseIf (Not createdItem Is Nothing) AndAlso (Not createdItem.IsFolder) Then
                    confirmScript = "javascript: return confirm('" & rtesafe(GetLanguage("request_confirm")) & "')"
                End If

                cmdDelete.Attributes.Add("onClick", confirmScript)

            End If
        End Sub

        Private Sub grdFile_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdFile.ItemCommand

            Dim itemIndex As Integer = Int16.Parse((CType(e.CommandSource, ImageButton).CommandArgument))
            'Dim name As String = (CType(e.CommandSource, ImageButton).CommandArgument)
            Dim selItem As IGalleryObjectInfo = CType(Zfolder.List.Item(itemIndex), IGalleryObjectInfo)


            Select Case (CType(e.CommandSource, ImageButton).CommandName)

                Case "delete"
                    lblInfo.Text = strFolderInfo
                    UpdateFolderSize(Zrequest.GalleryConfig, Zfolder.DeleteChild(selItem))
                    Zrequest.Folder.Reset()
                    GalleryConfig.ResetGalleryConfig(ZmoduleID)
                    ClearModuleCache(ZmoduleID)
                    Zconfig = New GalleryConfig(ZmoduleID)
                    Zrequest = New GalleryRequest(ZmoduleID)
                    Zfolder = Zrequest.Folder

                    CheckPopulate()
                    BindData()

                Case "edit"
                    Dim url As String = ""

                    If (Not selItem Is Nothing) AndAlso (selItem.IsFolder) Then
                        url = GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditAlbum & "&tabid=" & TabId & "&mid=" & ZmoduleID.ToString & "&path=" & CType(selItem, IGalleryObjectInfo).URL
                    ElseIf (Not selItem Is Nothing) AndAlso (Not selItem.IsFolder) Then
                        url = GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditFile & "&tabid=" & TabId & "&mid=" & ZmoduleID.ToString & "&path=" & CType(Zfolder, IGalleryObjectInfo).URL & "&editindex=" & selItem.Index.ToString
                    End If

                    If Len(url) > 0 Then
                        Session("UrlReferrer") = ViewState("UrlReferrer").ToString
                        Response.Redirect(url)
                    Else
                        lblInfo.Text = GetLanguage("Gal_FileOE")
                    End If

                Case "gps"
                    If (Not selItem Is Nothing) AndAlso (selItem.IsFolder) Then
                        ' is folder
                    ElseIf (Not selItem Is Nothing) AndAlso (Not selItem.IsFolder) Then
                        ' is file
                        Dim name As String = CType(selItem, GalleryFile).Name

                        Dim strExtension As String = ""
                        If InStr(1, name, ".") <> 0 Then
                            strExtension = Mid(name, InStrRev(name, ".") + 1)
                        End If

                        Select Case strExtension.ToLower()
                            Case "jpg", "jpeg", "tif", "png"
                                Dim objStreamReader As StreamReader
                                Dim strScript As String = ""
                                Dim strFileNamePath As String = Zfolder.Path
 
                                Dim fileName As String
                                fileName = Server.MapPath(CType(selItem, GalleryFile).URL)
                                Dim Exif As New ExifWorks(fileName)

                                Dim items() As String
                                Dim item As String

                                items = System.IO.Directory.GetFiles(Zrequest.Folder.Path, "*.gpx")

                                Dim SaveExif As Boolean = False
                                Dim TempLatLong As LatLong = Nothing
                                Dim TempDate As Date
                                TempDate = Exif.DateTimeOriginal

                                If TempDate > DateTime.MinValue Then
                                    TempDate = TempDate.AddDays(ConvertInteger(ddljours.SelectedValue))
                                    TempDate = TempDate.AddHours(ConvertInteger(ddlheure.SelectedValue))


                                    For Each item In items
                                        If TempLatLong Is Nothing Then
                                            objStreamReader = File.OpenText(item)
                                            strScript = objStreamReader.ReadToEnd
                                            objStreamReader.Close()
                                            TempLatLong = GetLatLongFromGPX(strScript, TempDate, ConvertInteger(ddlTimeZone.SelectedValue))
                                        End If

                                    Next
                                End If

                                'set latlong if found one

                                If Not TempLatLong Is Nothing Then
                                    If TempLatLong.Latitude <> "" Then
                                        Dim directory As String = Zrequest.Folder.Path
                                        ' Put Sort in the XML
                                        

                                        selItem.Latitude = TempLatLong.Latitude
                                        selItem.Longitude = TempLatLong.Longitude
                                        selItem.Sort = TempDate.ToString("yyyy\-MM\-dd HH\:mm\:ss")
                                        GalleryXML.SaveGalleryData(directory, selItem)

                                        'GalleryXML.SaveMetaData(TempDate.ToString, directory, name, metaData.Title(name), metaData.Description(name), metaData.Categories(name), metaData.OwnerID(name), metaData.Width(name), metaData.height(name), TempLatLong.Latitude, TempLatLong.Longitude, metaData.gpsicon(name), metaData.gpsiconsize(name), metaData.Link(name))
                                        SaveExif = True
                                        'GalleryConfig.ResetGalleryConfig(ZmoduleID)
                                        'Zrequest = New GalleryRequest(ZmoduleID)
                                        'Zrequest.Folder.Reset()
                                        'ClearModuleCache(ZmoduleID)
                                    End If
                                End If



                                If TempDate <> Exif.DateTimeOriginal And SaveExif Then
                                    Exif.DateTimeOriginal = TempDate
                                    Exif.DateTimeDigitized = TempDate
                                    Exif.DateTimeLastModified = TempDate
                                    Dim BMP As System.Drawing.Bitmap = Exif.GetBitmap()
                                    BMP.Save(fileName & ".tmp")
                                    BMP.Dispose()
                                    Exif.Dispose()
                                    System.IO.File.Delete(fileName)
                                    System.IO.File.Move(fileName & ".tmp", fileName)
                                Else
                                    Exif.Dispose()
                                End If




                                BindChildItems()





                        End Select

                    End If



            End Select

        End Sub



        Private Sub GPSIconImage_ItemCommand(ByVal Sender As System.Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles gpsIconImage.ItemCommand
            imgFileIcon.ImageUrl = CStr(e.CommandArgument)
            If Not Zfolder.Parent Is Nothing Then
                Dim directory As String = Zrequest.Folder.Parent.Path
                Dim name As String = Zrequest.Folder.Name
                Dim ownerID As Integer = Int16.Parse(txtOwnerID.Text)
                Dim mImage As System.Drawing.Image
                mImage = System.Drawing.Image.FromFile(Request.MapPath(imgFileIcon.ImageUrl))
                GalleryXML.Savegpsicon(directory, name, imgFileIcon.ImageUrl, "[" + mImage.Width.ToString + "," + mImage.Height.tostring + "]")
                imgFileIcon.Width = CType(mImage.Width.ToString, Unit)
                imgFileIcon.Height = CType(mImage.Height.ToString, Unit)
                mImage.Dispose()
                BindChildItems()
            End If
        End Sub




        Private Sub grdFile_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdFile.ItemCreated
            Dim cmdDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)

            If Not cmdDelete Is Nothing Then

                Dim createdItem As IGalleryObjectInfo = CType(e.Item.DataItem, IGalleryObjectInfo)
                Dim confirmScript As String = ""

                If (Not createdItem Is Nothing) AndAlso (createdItem.IsFolder) Then
                    confirmScript = "javascript: return confirm('" & RTESafe(GetLanguage("request_confirm_erasealbum")) & "')"
                    confirmScript = confirmScript.Replace("{items}", createdItem.Size.ToString)
                ElseIf (Not createdItem Is Nothing) AndAlso (Not createdItem.IsFolder) Then
                    confirmScript = "javascript: return confirm('" & RTESafe(GetLanguage("request_confirm")) & "')"
                End If

                cmdDelete.Attributes.Add("onClick", confirmScript)

            End If

            Dim cmdLatLong As ImageButton = CType(e.Item.FindControl("btngpsEdit"), ImageButton)
            Dim gpscheckbox As HtmlInputCheckBox = CType(e.Item.FindControl("Checkbox1"), HtmlInputCheckBox)
            If Not cmdLatLong Is Nothing And Not gpscheckbox Is Nothing Then
                Dim createdItem As IGalleryObjectInfo = CType(e.Item.DataItem, IGalleryObjectInfo)
                If Not createdItem Is Nothing Then
                    Dim metaData As New GalleryXML(Zrequest.Folder.Path)
                    cmdLatLong.Visible = (metaData.latitude(createdItem.Name) = "0" Or metaData.latitude(createdItem.Name) = "")
                    gpscheckbox.Visible = cmdLatLong.Visible
                    If cmdLatLong.Visible = True Then
                        btngpsEditall.Visible = True
                        pnlAdd.Visible = True
                    End If
                End If
            End If
        End Sub





        Private Sub dlStrip_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)
            Dim cmdDelete As Control = e.Item.FindControl("delete")
            If Not cmdDelete Is Nothing Then
                CType(cmdDelete, ImageButton).Attributes.Add("onClick", "javascript: return confirm('" & rtesafe(GetLanguage("request_confirm")) & "')")
            End If
        End Sub


        Private Sub EditMapOptions_Click(ByVal Sender As Object, ByVal e As System.EventArgs) Handles EditMapOptions.Click
            Session("UrlReferrer") = ViewState("UrlReferrer").ToString
            Response.Redirect(GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditAlbum & "&mapoptions=1&mid=" & ModuleId & "&tabid=" & TabId & "&path=" & Zrequest.Folder.GalleryHierarchy, True)
        End Sub


        Private Sub MakeTrack_Click(ByVal Sender As Object, ByVal e As System.EventArgs) Handles MakeTrack.Click
            If Not (UploadGPXFile.PostedFile.FileName.Trim() = "") Then
                Dim strScript As String
                Try
                    Dim UploadFileDestination As String = Zfolder.Path
                    Dim delim As String = "\"
                    UploadFileDestination = UploadFileDestination.Trim(delim.ToCharArray()) & "\"
                    Dim UploadFileName As String = ""
                    UploadFileName = UploadGPXFile.PostedFile.FileName.ToLower()
                    UploadFileName = UploadFileName.Substring(UploadFileName.LastIndexOf("\") + 1)

                    Dim ext As String = LCase(UploadFileName.Substring(UploadFileName.LastIndexOf(".") + 1, UploadFileName.Length - UploadFileName.LastIndexOf(".") - 1))






                    UploadFileDestination = Replace(UploadFileDestination, "/", "\")

                    UploadGPXFile.PostedFile.SaveAs(UploadFileDestination + UploadFileName)

                    Dim objStreamReader As StreamReader
                    objStreamReader = File.OpenText(UploadFileDestination + UploadFileName)
                    strScript = objStreamReader.ReadToEnd
                    objStreamReader.Close()



                    MakeTrackFromGPX(strScript)

                Catch ex As Exception
                End Try

            End If
            OpenClose6.Show = True
            OpenClose7.Show = True
        End Sub



        Private Sub UpdateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateButton.Click


            If Zfolder.Parent Is Nothing Then ' GALLERY: No need to change, as admin or owner could use gallery admin for this task
                Dim admin As New AdminDB()
                admin.UpdateModuleSetting(ZmoduleID, "GalleryTitle", txtTitle.Text)
                admin.UpdateModuleSetting(ZmoduleID, "GalleryDescription", txtDescription.Text)
                admin.UpdateModuleSetting(ZmoduleID, "Owner", txtOwner.Text)
                admin.UpdateModuleSetting(ZmoduleID, "OwnerID", txtOwnerID.Text)
                admin.UpdateModuleSetting(ZmoduleID, "Description", txtDescription.Text)
            Else ' ALBUM: Save changes into metadata.xml
                Dim directory As String = Zrequest.Folder.Parent.Path
                Dim name As String = Zrequest.Folder.Name
                Dim ownerID As Integer = Int16.Parse(txtOwnerID.Text)
                Dim categories As String = ddCategories.SelectedItem.Value
                ' Put Sort in the XML
                Dim metaData As New GalleryXML(directory)
                Dim What As IGalleryObjectInfo = metaData.CompleteInfo(name)
                What.Title = txtTitle.Text
                What.Description = txtDescription.Text
                What.Categories = categories
                What.OwnerID = ownerID
                What.Width = "0"
                What.Height = "0"
                What.Sort = txtSortOrder.Text
                What.Latitude = Latitude.Text
                What.Longitude = Longitude.Text
                What.Gpsicon = imgFileIcon.ImageUrl
                What.IsFolder = True
                What.Size = Zrequest.Folder.Size

                GalleryXML.SaveGalleryData(directory, What)

                'GalleryXML.SaveMetaData(txtSortOrder.Text, directory, name, txtTitle.Text, txtDescription.Text, categories, ownerID, "0", "0", Latitude.Text, Longitude.Text, imgFileIcon.ImageUrl, metaData.gpsiconsize(name), "")
                Zrequest.Folder.Parent.Reset()
            End If

            If Zconfig.CheckboxGPS Then
                If Request.Params("mapoptions") <> "" Then
                    GalleryXML.SaveMapMetaData(Zrequest.Folder.Path, _
                                                   options_width.Text, _
                                                   options_height.Text, _
                                                   options_full_screen.Checked.ToString, _
                                                   options_center.Text, _
                                                   options_zoom.Text, _
                                                   options_map_opacity.Text, _
                                                   options_map_type.Text, _
                                                   options_doubleclick_zoom.Checked.ToString, _
                                                   options_mousewheel_zoom.Checked.ToString, _
                                                   options_centering_options.Text, _
                                                   options_zoom_control.Text, _
                                                   options_scale_control.Checked.ToString, _
                                                   options_center_coordinates.Checked.ToString, _
                                                   options_crosshair_hidden.Checked.ToString, _
                                                   options_map_opacity_control.Checked.ToString, _
                                                   options_map_type_control_style.Text, _
                                                   options_map_type_control_filter.Checked.ToString, _
                                                   GetMapcontrol_excluded(options_map_type_control_excluded), _
                                                   options_legend_options_legend.Checked.ToString, _
                                                   options_legend_options_position.Text, _
                                                   options_legend_options_draggable.Checked.ToString, _
                                                   options_legend_options_collapsible.Checked.ToString, _
                                                   options_measurement_tools.Text, _
                                                   options_tracklist_options_tracklist.Checked.ToString, _
                                                   options_tracklist_options_position.Text, _
                                                   options_tracklist_options_max_width.Text, _
                                                   options_tracklist_options_max_height.Text, _
                                                   options_tracklist_options_desc.Checked.ToString, _
                                                   options_tracklist_options_zoom_links.Checked.ToString, _
                                                   options_tracklist_options_tooltips.Checked.ToString, _
                                                   options_tracklist_options_draggable.Checked.ToString, _
                                                   options_tracklist_options_collapsible.Checked.ToString, _
                                                   options_default_marker.Text, _
                                                   options_shadows.Checked.ToString, _
                                                   options_marker_link_target.Text, _
                                                   options_info_window_width.Text, _
                                                   options_thumbnail_width.Text, _
                                                   options_photo_size.Text, _
                                                   options_hide_labels.Checked.ToString, _
                                                   options_label_offset.Text, _
                                                   options_label_centered.Checked.ToString, _
                                                   options_driving_directions.Checked.ToString, _
                                                   options_garmin_icon_set.Text)

                    GalleryXML.SaveMapTrackList(Zrequest.Folder.Path, "1", trk_info.Text)
                    GalleryXML.SaveMapMarkers(Zrequest.Folder.Path, "1", Draw_Marker.Text)
                End If
            End If

            Zrequest.Folder.Reset()
            GalleryConfig.ResetGalleryConfig(ZmoduleID)
            ClearModuleCache(ZmoduleID)
            SendBackToAlbum()

        End Sub

        Private Sub SendBackToAlbum()

            Session("UrlReferrer") = Nothing
            Dim TempUrl As String
            TempUrl = GetFullDocument() & "?" & "tabid=" & _portalSettings.ActiveTab.TabId.ToString + "&path=" + Zfolder.GalleryHierarchy
            Response.Redirect(TempUrl)

        End Sub

        Private Function GetMapcontrol_excluded(ByVal List As CheckBoxList) As String
            Dim catItem As ListItem
            Dim catString As String = "["

            For Each catItem In List.Items
                If Not catItem.Selected Then
                    catString += catItem.Value & ","
                End If
            Next

            If Len(catString) > 1 Then
                Return catString.TrimEnd(","c) + "]"
            Else
                Return "[]"
            End If

        End Function


        Private Function Unzip(ByVal ZipEntry As ZipEntry, ByVal InputStream As ZipInputStream, ByVal TempDir As String) As String
            Dim strFileName As String
            Dim strFileNamePath As String

            Try
                strFileName = Path.GetFileName(ZipEntry.Name)

                If strFileName <> "" Then
                    strFileNamePath = Path.Combine(TempDir, strFileName)

                    If File.Exists(strFileNamePath) Then
                        ' _SpaceUsed -= New FileInfo(strFileNamePath).Length
                        File.Delete(strFileNamePath)
                    End If

                    Dim objFileStream As FileStream = File.Create(strFileNamePath)
                    Dim intSize As Integer = 2048
                    Dim arrData(2048) As Byte

                    intSize = InputStream.Read(arrData, 0, arrData.Length)
                    While intSize > 0
                        objFileStream.Write(arrData, 0, intSize)
                        intSize = InputStream.Read(arrData, 0, arrData.Length)
                    End While

                    objFileStream.Close()
                    ' _SpaceUsed += New FileInfo(strFileNamePath).Length
                    Return strFileName

                End If

            Catch Exc As System.Exception
                lblFileInfo.Text += "<br>" + Exc.Message
            End Try

        End Function


        Private Sub UnzipUploadedFile(FileToUnzip As String, TempDir As String)
            Dim objZipInputStream As New ZipInputStream(File.OpenRead(FileToUnzip))
            Dim objZipEntry As ZipEntry
            Dim ZipKeepSource As String = ""
            objZipEntry = objZipInputStream.GetNextEntry
            ZipKeepSource = BuildPath(New String(1) {Zrequest.Folder.Path, Zconfig.SourceFolder}, "\", False, False)

            Try
                If Not Directory.Exists(ZipKeepSource) Then
                    Directory.CreateDirectory(ZipKeepSource)
                End If
            Catch Exc As System.Exception
                lblFileInfo.Text += "<br>" + Exc.Message
            End Try



            While Not objZipEntry Is Nothing

                Try 'Save unzip file
                    Dim unzipFile As String = Unzip(objZipEntry, objZipInputStream, TempDir).ToLower()

                    Dim strExtension As String = ""
                    Dim AlbumFilePath As String = ""
                    Dim UnzipFilePath As String = Path.Combine(TempDir, unzipFile)
                    If InStr(1, unzipFile, ".") > 0 Then
                        strExtension = Path.GetExtension(unzipFile)
                    End If

                    'Check Valid Type here


                    If Zconfig.IsValidImageType(strExtension) Then
                        AlbumFilePath = Path.Combine(ZipKeepSource, unzipFile)
                    ElseIf Zconfig.IsValidMovieType(strExtension) _
                    OrElse Zconfig.IsValidFlashType(strExtension) Then
                        AlbumFilePath = Path.Combine(Zrequest.Folder.Path, unzipFile)
                    End If
                    If AlbumFilePath <> "" And Not File.Exists(AlbumFilePath) Then
                        File.Copy(UnzipFilePath, AlbumFilePath)
                    End If
                    'Delete unzip file
                    Try
                        File.Delete(UnzipFilePath)
                    Catch Exc As System.Exception
                        lblFileInfo.Text += "<br>" + Exc.Message

                    End Try

                    ' Put the file in collection
                    Dim UploadFile As New GalleryUploadFile
                    Dim TempUnzipFile As New System.IO.FileInfo(AlbumFilePath)

                    With UploadFile
                        .Name = Path.GetFileName(AlbumFilePath.ToLower)
                        .FileName = TempUnzipFile.FullName
                        .uploadFilePath = TempUnzipFile.FullName
                        .ContentType = TempUnzipFile.Extension
                        .WaterMark = txtWaterMark.Text
                        .ContentLength = CInt(TempUnzipFile.Length)
                        .ModuleID = ZmoduleID
                        .Title = txtFileTitle.Text
                        .Description = txtFileDescription.Text
                        .Categories = ddCategories3.SelectedItem.Value
                        .OwnerID = ZuserID
                    End With

                    ' Check file valid size & type
                    Dim validationInfo As String = UploadFile.ValidationInfo(ZmoduleID)
                    If Len(validationInfo) > 0 Then
                        lblFileInfo.Text += validationInfo
                        Return
                    End If

                    'Check file exists
                    If (Not ZuploadCollection.FileExists(UploadFile.Name)) Then
                        Dim uploadPath As String
                        ZuploadCollection.Add(UploadFile)
                    Else
                        lblFileInfo.Text += "<br>" + UploadFile.Name + " " + GetLanguage("Gal_FileOEN")

                    End If

                    ' end the file in collection



                Catch Exc As System.Exception
                    lblFileInfo.Text += "<br>" + Exc.Message
                End Try

                objZipEntry = objZipInputStream.GetNextEntry

            End While

            'Delete zip file
            Try
                File.Delete(FileToUnzip)
            Catch Exc As System.Exception
                lblFileInfo.Text += "<br>" + Exc.Message
            End Try

        End Sub






        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click



            If CheckQuota() Then
                'Retreive file upload collection
                SaveInfoinCollection()
                'UnZip Now
                ZuploadCollection = GalleryUploadCollection.GetList(Zfolder, ZmoduleID)
                Dim UploadFile As New GalleryUploadFile

                With UploadFile
                    .Name = Path.GetFileName(htmlUploadFile.PostedFile.FileName.ToLower)
                    .FileName = htmlUploadFile.PostedFile.FileName.ToLower
                    .ContentType = htmlUploadFile.PostedFile.ContentType
                    .ContentLength = htmlUploadFile.PostedFile.ContentLength
                    .ModuleID = ZmoduleID
                    .Title = txtFileTitle.Text
                    .Description = txtFileDescription.Text
                    .Categories = ddCategories3.SelectedItem.Value
                    .OwnerID = ZuserID
                    .WaterMark = txtWaterMark.Text
                End With

                txtFileTitle.Text = ""
                txtFileDescription.Text = ""
                ddCategories.ClearSelection()

                ' Check file valid size & type
                Dim validationInfo As String = UploadFile.ValidationInfo(ZmoduleID)
                If Len(validationInfo) > 0 Then
                    lblFileInfo.Text = validationInfo
                    Return
                End If
                Dim uploadPath As String
                'Check file exists
                If (Not ZuploadCollection.FileExists(UploadFile.Name)) Then


 

                    If UploadFile.Type = IGalleryObjectInfo.ItemType.Zip Then
                        uploadPath = BuildPath(New String(1) {Zrequest.Folder.Path, Zrequest.GalleryConfig.TempFolder}, "\", False, False)
                    Else
                        ZuploadCollection.Add(UploadFile)
                        If Not Zrequest.GalleryConfig.IsFixedSize Or UploadFile.Type = IGalleryObjectInfo.ItemType.Flash Or UploadFile.Type = IGalleryObjectInfo.ItemType.Movie Then
                            uploadPath = Zrequest.Folder.Path
                        Else
                            uploadPath = BuildPath(New String(1) {Zrequest.Folder.Path, Zrequest.GalleryConfig.SourceFolder}, "\", False, False)
                        End If
                    End If
                    UploadFile.uploadFilePath = Path.Combine(uploadPath, UploadFile.Name)

                    Try
                        If Not Directory.Exists(uploadPath) Then
                            Directory.CreateDirectory(uploadPath)
                        End If
                        htmlUploadFile.PostedFile.SaveAs(UploadFile.uploadFilePath)
                        ' Add file size to Quota
                        UpdateFolderSize(Zrequest.GalleryConfig, htmlUploadFile.PostedFile.ContentLength)
                    Catch Exc As System.Exception
                        lblFileInfo.Text += "<br>" + Exc.Message
                        Return
                    End Try
                Else
                    lblFileInfo.Text = GetLanguage("Gal_FileOEN")
                    Return
                End If

                If UploadFile.Type = IGalleryObjectInfo.ItemType.Zip Then
                    UnzipUploadedFile(UploadFile.uploadFilePath, uploadPath)
                End If
                ' Reset value & bind to grid
                grdUpload.DataSource = ZuploadCollection
                grdUpload.DataBind()
                btnFileUpload.Visible = True
                btnFileUpload.Attributes.Add("onclick", "toggleBox('" + btnFileUpload.ClientID + "',0);toggleBox('rotation1',1)")
            End If
        End Sub

        Private Sub grdUpload_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUpload.ItemCommand
            SaveInfoinCollection()
            Dim itemIndex As Integer = e.Item.ItemIndex
            ZuploadCollection = GalleryUploadCollection.GetList(Zfolder, ZmoduleID)
            Dim uploadFile As GalleryUploadFile

            Select Case (CType(e.CommandSource, ImageButton).CommandName)
                Case "delete"
                    uploadFile = CType(ZuploadCollection.Item(itemIndex), GalleryUploadFile)
                    Dim uploadPath As String
                    If uploadFile.Type = IGalleryObjectInfo.ItemType.Zip Then
                        UploadPath = BuildPath(New String(1) {Zrequest.Folder.Path, ZRequest.GalleryConfig.TempFolder}, "\", False, False)
                    Else
                        If Not ZRequest.GalleryConfig.IsFixedSize Or uploadFile.Type = IGalleryObjectInfo.ItemType.Flash Or uploadFile.Type = IGalleryObjectInfo.ItemType.Movie Then
                            uploadPath = Zrequest.Folder.Path
                        Else
                            UploadPath = BuildPath(New String(1) {Zrequest.Folder.Path, ZRequest.GalleryConfig.SourceFolder}, "\", False, False)
                        End If
                    End If
                    Try
                        File.Delete(uploadFile.uploadFilePath)
                    Catch Exc As System.Exception
                        lblFileInfo.Text = "<br>" + Exc.Message
                        Return
                    End Try
                    ZuploadCollection.RemoveAt(itemIndex)
                    grdUpload.DataSource = ZuploadCollection
                    grdUpload.DataBind()
                Case "save"
                    Me.lblFileInfo.Text = ""
                    SaveInfoinCollection()
                    uploadFile = CType(ZuploadCollection.Item(itemIndex), GalleryUploadFile)
                    ' Update Directory Size
                    UpdateFolderSize(Zrequest.GalleryConfig, ZuploadCollection.Upload(uploadFile.uploadFilePath))
                    If Len(ZuploadCollection.ErrMessage) > 0 Then
                        lblFileInfo.Text = ZuploadCollection.ErrMessage
                    Else
                        grdUpload.DataSource = ZuploadCollection
                        grdUpload.DataBind()
                    End If



            End Select
        End Sub

        Private Sub SaveInfoinCollection()
            If grdUpload.Items.Count > 0 Then
                ZuploadCollection = GalleryUploadCollection.GetList(Zfolder, ZmoduleID)
                ' Dim item As DataGridItem
                Dim I As Integer
                For I = grdUpload.Items.Count To 1 Step -1
                    Dim uploadFile As GalleryUploadFile
                    uploadFile = CType(ZuploadCollection.Item(I - 1), GalleryUploadFile)
                    uploadFile.Description = GetTextBox(grdUpload.Items(I - 1), "Description").Text
                    uploadFile.Title = GetTextBox(grdUpload.Items(I - 1), "Title").Text
                Next
            End If
        End Sub


        Private Function GetTextBox(ByVal item As DataGridItem, ByVal NameOfBox As String) As TextBox
            Return CType(item.FindControl(NameOfBox), TextBox)
        End Function


        Private Sub btnFileUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFileUpload.Click

            SaveInfoinCollection()
            Me.lblFileInfo.Text = ""
            ZuploadCollection = GalleryUploadCollection.GetList(Zfolder, ZmoduleID)

            ' Update Directory Size
            UpdateFolderSize(Zrequest.GalleryConfig, ZuploadCollection.Upload(""))
			If Len(ZuploadCollection.ErrMessage) > 0 Then
                lblFileInfo.Text = ZuploadCollection.ErrMessage
            Else
                GalleryUploadCollection.ResetList(Zfolder, ZmoduleID)
                Zrequest = New GalleryRequest(ZmoduleID)
                Zrequest.Folder.Reset()
                GalleryConfig.ResetGalleryConfig(ZmoduleID)
                ClearModuleCache(ZmoduleID)
                If Not Request.Params("action") Is Nothing Then
                    Session("UrlReferrer") = Nothing
                    Response.Redirect(CType(ViewState("UrlReferrer"), String))
                Else
                    grdUpload.DataBind() ' Clear old file list
                    btnFileUpload.Visible = False
                    Zconfig = New GalleryConfig(ZmoduleID)
                    Zrequest = New GalleryRequest(ZmoduleID)
                    Zfolder = Zrequest.Folder

                    CheckPopulate()
                    BindData()
                End If

            End If
            UploadImage2.Visible = ZGalleryAuthority
            ClearCache.Visible = ZGalleryAuthority
            SubAlbum.Visible = ZGalleryAuthority
            pnlAddFile.Visible = False
            pnlAlbumDetails.Visible = True
            pnlAdd.Visible = False
            pnlAdd1.Visible = True
            If Zrequest.SubAlbumItems.Count > 0 Then
                pnlContentDir.Visible = True
            Else
                pnlContentDir.Visible = False
            End If

            If Zrequest.FileItems.Count > 0 Then
                pnlContentFile.Visible = True
            Else
                pnlContentFile.Visible = False
                pnlAdd.Visible = False
            End If

        End Sub

        Private Function GetRootURL() As String
            ' Return GetFullDocument() & "?" & "&tabid=" & portalSettings.GetEditModuleSettings(ZmoduleID).TabId
            ' to return to prévious album
            Dim sb As New StringBuilder()
            sb.Append(GetFullDocument())
            sb.Append("?edit=control&editpage=")
            sb.Append(TTT_EditGallery.GalleryEditType.GalleryEditAlbum)
            sb.Append("&mid=")
            sb.Append(ZmoduleID.ToString)
            sb.Append("&tabid=")
            sb.Append(TabId.ToString)
            Return Sb.ToString()

        End Function

        Public Function GetFolderURL(ByVal DataItem As Object) As String

            Return GetRootURL() & _
            "&path=" & CType(DataItem, FolderDetail).URL

        End Function

        Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelButton.Click
            SendBackToAlbum()
        End Sub


        Private Sub UploadImage2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles UploadImage2.Click

            lblInfo.Text = strFolderInfo

            If CheckQuota() Then
                UploadImage2.Visible = False
                ClearCache.Visible = False
                SubAlbum.Visible = False
                pnlAddFile.Visible = True
                pnlAdd1.Visible = False
                btnFileUpload.Visible = False
                pnlAddFolder.Visible = False
                pnlAlbumDetails.Visible = False
                pnlMapOption.Visible = False
                pnlContentDir.Visible = False
                pnlContentFile.Visible = False
                pnlAdd.Visible = False
            Else
                lblInfo.Text = lblFileInfo.Text
            End If

        End Sub

        Private Sub SetUpAction(ByVal Action As String)
            txtNewAlbum.Text = ""
            txtAlbumTitle.Text = ""
            txtAlbumDescription.Text = ""
            pnlAdd.Visible = False
            pnlAdd1.Visible = False
            btnFileUpload.Visible = False
            pnlAddFolder.Visible = False
            pnlAlbumDetails.Visible = False
            pnlMapOption.Visible = False
            pnlContentDir.Visible = False
            pnlContentFile.Visible = False
            pnlAddFile.Visible = False
            UploadImage2.Visible = False
            ClearCache.Visible = False
            SubAlbum.Visible = False


            Select Case Action
                Case "addfile"
                    lblInfo.Text = strFolderInfo

                    If CheckQuota() Then
                        pnlAddFile.Visible = True
                    Else
                        lblInfo.Text = lblFileInfo.Text
                    End If

                Case "addfolder"
                    pnlAddFolder.Visible = True
            End Select
        End Sub

        Private Sub SubAlbum_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SubAlbum.Click
            'BRT: clear for next add
            txtNewAlbum.Text = ""
            txtAlbumTitle.Text = ""
            txtAlbumDescription.Text = ""
            'BRT: end
            pnlAddFolder.Visible = True
            pnlAdd1.Visible = False
            pnlAddFile.Visible = False
            pnlAlbumDetails.Visible = False
            pnlMapOption.Visible = False
            pnlContentFile.Visible = False
            pnlAdd.Visible = False
            UploadImage2.Visible = False
            ClearCache.Visible = False
            SubAlbum.Visible = False


            If Zrequest.SubAlbumItems.Count > 0 Then
                pnlContentDir.Visible = True
            Else
                pnlContentDir.Visible = False
            End If


        End Sub

 

        Private Sub btnFileClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFileClose.Click
            If Not Request.Params("action") Is Nothing Then
                Session("UrlReferrer") = Nothing
                Response.Redirect(CType(ViewState("UrlReferrer"), String))
            Else

                UploadImage2.Visible = ZGalleryAuthority
                ClearCache.Visible = ZGalleryAuthority
                SubAlbum.Visible = ZGalleryAuthority
                ' Should go back to raw url

                pnlAddFile.Visible = False
                pnlAlbumDetails.Visible = True
                pnlMapOption.Visible = False
                pnlAdd1.Visible = True
                If Zrequest.SubAlbumItems.Count > 0 Then
                    pnlContentDir.Visible = True
                Else
                    pnlContentDir.Visible = False
                End If

                If Zrequest.FileItems.Count > 0 Then
                    pnlContentFile.Visible = True
                Else
                    pnlContentFile.Visible = False
                    pnlAdd.Visible = False
                End If
            End If
        End Sub

        Private Sub btnFolderClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFolderClose.Click
            If Not Request.Params("action") Is Nothing Then
                Session("UrlReferrer") = Nothing
                Response.Redirect(CType(ViewState("UrlReferrer"), String))
            Else

                UploadImage2.Visible = ZGalleryAuthority
                ClearCache.Visible = ZGalleryAuthority
                SubAlbum.Visible = ZGalleryAuthority

                pnlAddFolder.Visible = False
                pnlAlbumDetails.Visible = True
                pnlMapOption.Visible = False
                pnlAdd1.Visible = True


                If Zrequest.SubAlbumItems.Count > 0 Then
                    pnlContentDir.Visible = True
                Else
                    pnlContentDir.Visible = False
                End If

                If Zrequest.FileItems.Count > 0 Then
                    pnlContentFile.Visible = True

                Else
                    pnlContentFile.Visible = False
                    pnlAdd.Visible = False
                End If
            End If

        End Sub

        Protected Function CanEdit(ByVal DataItem As Object) As Boolean
            If CType(DataItem, IGalleryObjectInfo).OwnerID = ZuserID _
            OrElse ZGalleryAuthority Then
                Return True
            Else
                Return False
            End If
        End Function

        Protected Function GpsEdit() As Boolean
            Return Zconfig.CheckboxGPS
        End Function


        Private Sub btnEditOwner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditOwner.Click
            pnlSelectOwner.Visible = True
            ctlUsers.BindUsers()
        End Sub

        Private Function IsValidFileType(ByVal FileName As String) As Boolean
            Dim ext As String = FileName.Substring(FileName.LastIndexOf(".") + 1, FileName.Length - FileName.LastIndexOf(".") - 1)
            Dim I As Integer
            Dim AcceptedFileTypes As String = "jpg gif"
            i = AcceptedFileTypes.lastIndexOf(ext)
            If i > -1 Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Sub UploadImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UploadImage.Click
            Page.Validate()
            If CheckQuota() Then
                'Retreive file upload collection
                Dim StrFolder As String
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                If Not Zfolder.Parent Is Nothing Then

                    If Not (UploadFile.PostedFile.FileName.Trim() = "") Then
                        If IsValidFileType(UploadFile.PostedFile.FileName.ToLower()) Then
                            Try
                                Dim FileName As String = Zfolder.Name.ToLower()
                                Dim delim As String = "\"
                                Dim UploadFileDestination As String = Zfolder.Parent.ResFolderPath
                                UploadFileDestination = UploadFileDestination.Trim(delim.ToCharArray()) & "\"
                                Dim UploadFileName As String = ""
                                UploadFileName = UploadFile.PostedFile.FileName.ToLower()
                                UploadFileName = UploadFileName.Substring(UploadFileName.LastIndexOf("\") + 1)

                                Dim ext As String = LCase(UploadFileName.Substring(UploadFileName.LastIndexOf(".") + 1, UploadFileName.Length - UploadFileName.LastIndexOf(".") - 1))





                                UploadFileDestination += "\"
                                UploadFileDestination = Replace(UploadFileDestination, "/", "\")


                                If System.IO.File.Exists(UploadFileDestination + FileName + ".jpg") Then
                                    System.IO.File.Delete(UploadFileDestination + FileName + ".jpg")
                                End If


                                If System.IO.File.Exists(UploadFileDestination + FileName + ".gif") Then
                                    System.IO.File.Delete(UploadFileDestination + FileName + ".gif")
                                End If



                                If ext = "jpg" Then
                                    UploadFile.PostedFile.SaveAs(UploadFileDestination + "temp" + ".jpg")
                                    ResizeImage(UploadFileDestination + "temp" + ".jpg", UploadFileDestination + FileName + ".jpg", CInt(txtSizeL.Text), CInt(txtSizeH.Text), ext, CInt(txtquality.Text))
                                    System.IO.File.Delete(UploadFileDestination + "temp" + ".jpg")
                                End If

                                If ext = "gif" Then
                                    UploadFile.PostedFile.SaveAs(UploadFileDestination + "temp" + ".gif")
                                    ResizeImage(UploadFileDestination + "temp" + ".gif", UploadFileDestination + FileName + ".gif", CInt(txtSizeL.Text), CInt(txtSizeH.Text), ext, CInt(txtquality.Text))
                                    System.IO.File.Delete(UploadFileDestination + "temp" + ".gif")
                                End If
                                Session("reload") = "true"
                                Zrequest.Folder.Parent.Reset()
                                Zrequest.Folder.Reset()
                                ClearModuleCache(ZmoduleID)
                                Session("UrlReferrer") = ViewState("UrlReferrer").ToString
                                Response.Redirect(Request.RawUrl)

                            Catch ex As Exception
                            End Try


                        End If

                    End If
                End If
            End If

        End Sub

        Private Sub ClearCache_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ClearCache.Click
            Zrequest.Folder.Reset()
            Zrequest.Folder.REPopulate(Zconfig.CheckboxGPS)
            ClearModuleCache(ModuleId)
            Session("UrlReferrer") = ViewState("UrlReferrer").ToString
            Response.Redirect(Request.UrlReferrer.ToString)
        End Sub

        Private Sub btngpsEditall_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btngpsEditall.Click
            Dim ItemCollection As DataGridItemCollection
            Dim CurrentItem As DataGridItem

            ItemCollection = grdFile.Items
            txtDescription.Text = ""
            For Each CurrentItem In ItemCollection

                Dim chkDeleteMessage As HtmlInputCheckBox
                chkDeleteMessage = CType(CurrentItem.Cells(9).Controls(1), HtmlInputCheckBox)
                If chkDeleteMessage.Checked Then
                    Dim selItem As IGalleryObjectInfo = CType(Zfolder.List.Item(CInt(chkDeleteMessage.Value)), IGalleryObjectInfo)

                    If (Not selItem Is Nothing) AndAlso (Not selItem.IsFolder) Then
                        ' is file
                        Dim name As String = CType(selItem, GalleryFile).Name

                        Dim strExtension As String = ""
                        If InStr(1, name, ".") <> 0 Then
                            strExtension = Mid(name, InStrRev(name, ".") + 1)
                        End If

                        Select strExtension.ToLower()
                            Case "jpg", "jpeg", "tif", "png"
                                Dim objStreamReader As StreamReader
                                Dim strScript As String = ""
                                Dim strFileNamePath As String = Zfolder.Path

                                Dim fileName As String
                                fileName = Server.MapPath(CType(selItem, GalleryFile).URL)
                                Dim Exif As New ExifWorks(fileName)

                                Dim items() As String
                                Dim item As String

                                items = System.IO.Directory.GetFiles(Zrequest.Folder.Path, "*.gpx")

                                Dim SaveExif As Boolean = False
                                Dim TempLatLong As LatLong = Nothing
                                Dim TempDate As Date
                                TempDate = Exif.DateTimeOriginal
                                If TempDate > DateTime.MinValue Then
                                    TempDate = TempDate.AddDays(ConvertInteger(ddljours.SelectedValue))
                                    TempDate = TempDate.AddHours(ConvertInteger(ddlheure.SelectedValue))

                                    For Each item In items
                                        If TempLatLong Is Nothing Then
                                            objStreamReader = File.OpenText(item)
                                            strScript = objStreamReader.ReadToEnd
                                            objStreamReader.Close()
                                            TempLatLong = GetLatLongFromGPX(strScript, TempDate, ConvertInteger(ddlTimeZone.SelectedValue))
                                        End If
                                    Next

                                    'set latlong if found one

                                    If Not TempLatLong Is Nothing Then
                                        If TempLatLong.Latitude <> "" Then
                                            Dim directory As String = Zrequest.Folder.Path
                                            ' Put Sort in the XML

                                            selItem.Latitude = TempLatLong.Latitude
                                            selItem.Longitude = TempLatLong.Longitude
                                            selItem.Sort = TempDate.ToString("yyyy\-MM\-dd HH\:mm\:ss")
                                            GalleryXML.SaveGalleryData(directory, selItem)
                                            SaveExif = True
                                        End If
                                    End If
                                End If

                                If TempDate <> Exif.DateTimeOriginal And SaveExif Then
                                    Exif.DateTimeOriginal = TempDate
                                    Dim BMP As System.Drawing.Bitmap = Exif.GetBitmap()
                                    BMP.Save(fileName & ".tmp")
                                    BMP.Dispose()
                                    Exif.Dispose()
                                    System.IO.File.Delete(fileName)
                                    System.IO.File.Move(fileName & ".tmp", fileName)
                                Else
                                    Exif.Dispose()
                                End If

                        End Select
                    End If
                End If
            Next
            BindChildItems()
        End Sub



        Private Sub ctlUsers_UserSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlUsers.UserSelected
            Dim myUser As ForumUser = CType(ctlUsers.SelectedUser, ForumUser)

            txtOwner.Text = myUser.Name
            txtOwnerID.Text = myUser.UserID.ToString

            pnlSelectOwner.Visible = False

        End Sub

        Private Sub PopulateMapOptions()
            If Zconfig.CheckboxGPS Then

                EditMapOptions.Visible = False
                MakeTrack.Visible = True
                Dim metaData As New GalleryXML(Zrequest.Folder.Path)

                options_width.Text = metaData.Map_Width  ' width of the map, in pixels
                options_height.Text = metaData.Map_Height  ' height of the map, in pixels"
                options_full_screen.Checked = CBool(metaData.Map_FullScreen)  ' true|false: should the map fill the entire page (or frame)?"
                options_center.Text = metaData.map_center ' "[46.493553, -73.6715815]"  ' [latitude,longitude] - be sure to keep the square brackets"

                options_zoom.SelectedItem.Selected = False
                options_zoom.Items.FindByValue(metaData.Map_Zoom).Selected = True



                options_map_opacity.SelectedItem.Selected = False
                options_map_opacity.Items.FindByValue(metaData.Map_Opacity).Selected = True


                options_map_type.SelectedItem.Selected = False
                options_map_type.Items.FindByValue(metaData.Map_Type).Selected = True

                options_doubleclick_zoom.Checked = CBool(metaData.Map_ZoomClick)  ' true|false: zoom in when mouse is double-clicked?"
                options_mousewheel_zoom.Checked = CBool(metaData.map_zoommouse) ' true|false; or 'reverse' for down=in and up=out"
                options_centering_options.Text = metaData.map_centering '"{ 'open_info_window': true, 'partial_match': true, 'center_key': 'center', 'default_zoom': null}" ' URL-based centering (e.g., ?center=name_of_marker&zoom=14)"

                ' widgets on the map:
                options_zoom_control.SelectedItem.Selected = False
                options_zoom_control.Items.FindByValue(metaData.Map_ZoomControl).Selected = True


                options_scale_control.Checked = CBool(metaData.Map_ScaleControl) ' true|false"
                options_center_coordinates.Checked = CBool(metaData.map_centercoord)  ' true|false: show a "center coordinates" box and crosshair?
                options_crosshair_hidden.Checked = CBool(metaData.map_crosshair)  ' true|false: hide the crosshair initially?"
                options_map_opacity_control.Checked = CBool(metaData.map_opacityctrl)  ' true|false"

                ' widget to change the background map"
                options_map_type_control_style.SelectedItem.Selected = False
                options_map_type_control_style.Items.FindByValue(metaData.map_typectrl).Selected = True

                ' options_map_type_control_style.Text = "'none'"  ' 'menu'|'list'|'none'|'google'"
                options_map_type_control_filter.Checked = CBool(metaData.map_typefltr)  ' true|false: when map loads, are irrelevant maps ignored?"

                BindMapControl_Excluded(metaData.Map_TypeExcl)


                ' options for a floating legend box (id="legend"), which can contain anything
                options_legend_options_legend.Checked = CBool(metaData.map_legendon)  ' true|false: enable or disable the legend altogether"
                options_legend_options_position.Text = metaData.map_legendpos '"['G_ANCHOR_TOP_LEFT', 70, 6]"  ' [Google anchor name, relative x, relative y]"
                options_legend_options_draggable.Checked = CBool(metaData.map_legenddrag)  ' true|false: can it be moved around the screen?"
                options_legend_options_collapsible.Checked = CBool(metaData.map_legendcoll)  ' true|false: can it be collapsed by double-clicking its top bar?"
                options_measurement_tools.Text = metaData.map_tools '"{ visible: false, distance_color: '', area_color: '', position: [] }"

                ' track-related options:
                ' options for a floating list of the tracks visible on the map"
                options_tracklist_options_tracklist.Checked = CBool(metaData.map_trackliston)  ' true|false: enable or disable the tracklist altogether"
                options_tracklist_options_position.Text = metaData.map_tracklistpos '"['G_ANCHOR_TOP_RIGHT', 6, 32]"  ' [Google anchor name, relative x, relative y]"
                options_tracklist_options_max_width.Text = metaData.map_tracklistmwidth '"180" ' maximum width of the tracklist, in pixels"
                options_tracklist_options_max_height.Text = metaData.map_tracklistmheight ' "610" ' maximum height of the tracklist, in pixels; if the list is longer, scrollbars will appear"
                options_tracklist_options_desc.Checked = CBool(metaData.map_tracklistdesc)  ' true|false: should tracks' descriptions be shown in the list"
                options_tracklist_options_zoom_links.Checked = CBool(metaData.map_tracklistzoom)  ' true|false: should each item include a small icon that will zoom to that track?"
                options_tracklist_options_tooltips.Checked = CBool(metaData.map_tracklisttool)  ' true|false: should the name of the track appear on the map when you mouse over the name in the list?"
                options_tracklist_options_draggable.Checked = CBool(metaData.map_tracklistdrag)  ' true|false: can it be moved around the screen?"
                options_tracklist_options_collapsible.Checked = CBool(metaData.map_tracklistcoll)  ' true|false: can it be collapsed by double-clicking its top bar?"

                ' marker-related options:
                options_default_marker.Text = metaData.map_marker '  "{ color: 'red', icon: 'googlemini' }" ' icon can be a URL, but be sure to also include size:[w,h] and optionally anchor:[x,y]"
                options_shadows.Checked = CBool(metaData.map_shadows) ' true|false: do the standard markers have "shadows" behind them?
                options_marker_link_target.Text = metaData.map_linktgt '"'_blank'" ' the name of the window or frame into which markers' URLs will load"
                options_info_window_width.Text = metaData.map_infowidth ' "0"  ' in pixels, the width of the markers' pop-up info "bubbles" (can be overridden by 'window_width' in individual markers)
                options_thumbnail_width.Text = metaData.map_thumbnailwidth ' "0"  ' in pixels, the width of the markers' thumbnails (can be overridden by 'thumbnail_width' in individual markers)"
                options_photo_size.Text = metaData.map_photosize '"[0, 0]"  ' in pixels, the size of the photos in info windows (can be overridden by 'photo_width' or 'photo_size' in individual markers)"
                options_hide_labels.Checked = CBool(metaData.map_labelshide)  ' true|false: hide labels when map first loads?"
                options_label_offset.Text = metaData.map_labeloff '"[0, 0]"  ' [x,y]: shift all markers' labels (positive numbers are right and down)"
                options_label_centered.Checked = CBool(metaData.map_labelcenter)  ' true|false: center labels with respect to their markers?  (label_left is also a valid option.)"
                options_driving_directions.Checked = CBool(metaData.map_dd)  ' put a small "driving directions" form in each marker's pop-up window? (override with dd:true or dd:false in a marker's options)
                options_garmin_icon_set.Text = metaData.map_iconset ' "'gpsmap'" ' 'gpsmap' are the small 16x16 icons; change it to '24x24' for larger icons"

                trk_info.Text = metaData.Map_TrackList("1")
                Draw_Marker.Text = metaData.Map_Markers("1")
            End If
        End Sub




        Private Sub MakeTrackFromGPX(ByVal xmlData As String)
            Dim XmlDoc As New XmlDocument()
            xmlDoc.Load(New StringReader(xmlData))

            'Instantiate an XmlDocument object. 
            Dim GPSInfoNode As System.Xml.XmlNode

            'Instantiate an XmlNamespaceManager object. 
            Dim xmlnsManager As New System.Xml.XmlNamespaceManager(xmldoc.NameTable)

            'Add the namespaces used in the XmlNamespaceManager. 
            Dim root As XmlNode = xmlDoc.DocumentElement
            xmlnsManager.AddNamespace("nsgpx", root.Attributes("xmlns").InnerText())
            Dim GPSInfo As System.Xml.XmlNodeList

            'Execute the XPath query using the SelectNodes method of the XmlDocument. 
            'Supply the XmlNamespaceManager as the nsmgr parameter. 
            'The matching nodes will be returned as an XmlNodeList. 
            'Use an XmlNode object to iterate through the returned XmlNodeList. 

            GPSInfo = xmlDoc.SelectNodes("//nsgpx:gpx/nsgpx:trk/nsgpx:trkseg/nsgpx:trkpt", xmlnsManager)
            Dim I As Integer
            ' Choisir couleur pour la track
            Dim Color As String
            If txtColor.Text <> "" Then
                Color = txtColor.Text
            Else
                Color = "#59ACFF"
            End If

            Dim Track As Integer = 1

            If trk_info.Text <> "" Then
                While InStr(trk_info.Text, "t = " + Track.ToString + ";") > 0
                    Track += 1
                End While
            End If

            If GPSInfo.Count > 0 Then
                Dim TempTrack As String
                Dim TempTrack1 As String
                Dim TempTrack2 As String
                TempTrack = vbLf + "//  Track " + Track.ToString + vbLf
                TempTrack += "t = " + Track.ToString + "; trk_info[t] = [];" + vbLf

                root = XmlDoc.SelectSingleNode("//nsgpx:gpx/nsgpx:trk", xmlnsManager)


                If InStr(root.InnerXml, "</name>") > 0 Then
                    TempTrack += "trk_info[t]['name'] = '" + root.Item("name").InnerText + "';" + vbLf
                Else
                    TempTrack += "trk_info[t]['name'] = '" + txtTitle.Text + "';" + vbLf
                End If
                If InStr(root.InnerXml, "</desc>") > 0 Then
                    TempTrack += "trk_info[t]['desc'] = '" + root.Item("desc").InnerText + "';" + vbLf
                Else
                    TempTrack += "trk_info[t]['desc'] = '" + txtDescription.Text + "';" + vbLf
                End If


                TempTrack += "trk_info[t]['clickable'] = true;" + vbLf
                TempTrack += "trk_info[t]['color'] = '" + Color + "'; trk_info[t]['width'] = 6; trk_info[t]['opacity'] = 0.60;" + vbLf
                TempTrack += "trk_info[t]['outline_color'] = '#000000'; trk_info[t]['outline_width'] = 0; trk_info[t]['fill_color'] = '" + Color + "'; trk_info[t]['fill_opacity'] = 0;" + vbLf
                TempTrack += "trk_segments[t] = [];" + vbLf
                TempTrack += "trk_segments[t].push({ points:[ "
                ' [46.501293,-73.659836],[46.500984,-73.659691] ] }); 

                I = 0
                TempTrack1 = ""
                For Each GPSInfoNode In GPSInfo
                    I += 1
                    TempTrack1 += "[" + GPSInfoNode.Attributes("lat").InnerText()
                    TempTrack1 += "," + GPSInfoNode.Attributes.GetNamedItem("lon").InnerText() + "]"
                    If GPSInfo.Count > I Then
                        TempTrack1 += ","
                    End If
                Next

                TempTrack2 = "] });" + vbLf
                TempTrack2 += "GV_Draw_Track(t);" + vbLf
                TempTrack2 += "t = " + Track.ToString + "; GV_Add_Track_to_Tracklist({bullet:'- ',name:trk_info[t]['name'],desc:trk_info[t]['desc'],color:trk_info[t]['color'],number:t});" + vbLf

                If Not InStr(trk_info.Text, TempTrack1) > 0 Then
                    trk_info.Text += TempTrack + TempTrack1 + TempTrack2 + vbLf
                End If


            End If


            GPSInfo = xmlDoc.SelectNodes("//nsgpx:gpx/nsgpx:wpt", xmlnsManager)

            '  GV_Draw_Marker({lat:46.4024017897516,lon:-73.6854231087874, name: 'dsc00455.jpg', desc: '', shortdesc: 'dsc00455.jpg', icon: '/images/gps/24camera.png', url: '.jpg', thumbnail: '/Portals/afb8348f-f401-4ecb-b128-5d3d6502402b/Album12/_thumbs/dsc00455.jpg', thumbnail_width: 100, photo: '/Portals/afb8348f-f401-4ecb-b128-5d3d6502402b/Album12/dsc00455.jpg', photo_size: [500,375], window_width: 520, folder: 'dsc00455.jpg', scale: 1.5, opacity: 0.7, dd: false});
            ' GV_Draw_Marker({lat:46.521654,lon:46.521654, name:018,, icon:CrossingCrossing});

            Dim TempMarker As String
            For Each GPSInfoNode In GPSInfo
                TempMarker = "GV_Draw_Marker({lat:" + GPSInfoNode.Attributes("lat").InnerText() + ",lon:" + GPSInfoNode.Attributes("lon").InnerText()
                If InStr(GPSInfoNode.InnerXml, "</name>") > 0 Then
                    TempMarker += ", name:'" + GPSInfoNode.Item("name").InnerText + "'"
                End If
                If InStr(GPSInfoNode.InnerXml, "</desc>") > 0 Then
                    TempMarker += ", desc:'" + GPSInfoNode.Item("desc").InnerText + "'"
                End If
                If InStr(GPSInfoNode.InnerXml, "</sym>") > 0 Then
                    TempMarker += ", icon:'" + GPSInfoNode.Item("sym").InnerText + "'"
                End If

                TempMarker += "});"

                If Not InStr(Draw_Marker.Text, TempMarker) > 0 Then
                    Draw_Marker.Text += TempMarker + vbLf
                End If

            Next
        End Sub

        Private Function GetLatLongFromGPX(ByVal xmlData As String, ByVal what As Date, Optional ByVal timeZone As Integer = -1) As LatLong
            Dim XmlDoc As New XmlDocument()
            XmlDoc.Load(New StringReader(xmlData))
            Dim tempLat As String
            Dim tempLong As String
            Dim TempDate As Date = Now()
            Dim TempLatLong As New LatLong
            TempLatLong.Latitude = ""
            TempLatLong.Longitude = ""
            If timeZone = -1 Then
                timeZone = _portalSettings.TimeZone
            End If
            'Instantiate an XmlDocument object. 
            Dim GPSInfoNode As System.Xml.XmlNode

            'Instantiate an XmlNamespaceManager object. 
            Dim xmlnsManager As New System.Xml.XmlNamespaceManager(XmlDoc.NameTable)

            'Add the namespaces used in the XmlNamespaceManager. 
            Dim root As XmlNode = XmlDoc.DocumentElement
            xmlnsManager.AddNamespace("nsgpx", root.Attributes("xmlns").InnerText())
            Dim GPSInfo As System.Xml.XmlNodeList

            GPSInfo = XmlDoc.SelectNodes("//nsgpx:gpx/nsgpx:trk/nsgpx:trkseg/nsgpx:trkpt", xmlnsManager)
            Dim GotALatLong As Boolean = False
            Dim TotalResult As String = ""
            If GPSInfo.Count > 0 Then


                For Each GPSInfoNode In GPSInfo
                    tempLat = GPSInfoNode.Attributes("lat").InnerText()
                    tempLong = GPSInfoNode.Attributes("lon").InnerText()
                    ' 2011-07-29 06:37:21   - from exif
                    ' 2011-11-19T14:19:22Z  - from gpx
                    TempDate = DateTime.ParseExact(GPSInfoNode.Item("time").InnerText, "yyyy\-MM\-dd\THH\:mm\:ss\Z", Nothing).AddMinutes(timeZone)
                    If TempDate < what Then
                        GotALatLong = True
                    End If

                    If GotALatLong And TempDate >= what Then
                        TempLatLong.Latitude = tempLat
                        TempLatLong.Longitude = tempLong
                        Return TempLatLong
                    End If


                Next
            End If
            Return Nothing

        End Function
    End Class
End Namespace