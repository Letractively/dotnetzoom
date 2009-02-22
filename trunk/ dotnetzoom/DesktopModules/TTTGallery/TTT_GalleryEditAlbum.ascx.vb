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
Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports System.Text

Namespace DotNetZoom
    Public Class TTT_GalleryEditAlbum
        Inherits DotNetZoom.PortalModuleControl

        ' Obtain PortalSettings from Current Context   
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

		Protected WithEvents ctlUsers As TTT_UsersControl
		Protected WithEvents pnlSelectOwner As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents btnEditOwner As System.Web.UI.WebControls.Button
		Protected WithEvents btnAddFile As System.Web.UI.WebControls.Button
        Protected WithEvents btnAddFolder As System.Web.UI.WebControls.Button
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
        Protected WithEvents txtPath As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtOwner As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents lstCategories As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents pnlAlbumDetails As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents grdFile As System.Web.UI.WebControls.DataGrid
        Protected WithEvents grdDir As System.Web.UI.WebControls.DataGrid
        Protected WithEvents pnlContentFile As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlContentDir As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents CancelButton As System.Web.UI.WebControls.Button
        Protected WithEvents UpdateButton As System.Web.UI.WebControls.Button
        Protected WithEvents pnlAlbumInfo As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblAlbumTitle As System.Web.UI.WebControls.Label
        Protected WithEvents txtNewAlbum As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAlbumTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAlbumDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents lstCategories2 As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents btnFolderSave As System.Web.UI.WebControls.Button
        Protected WithEvents btnFolderClose As System.Web.UI.WebControls.Button
        Protected WithEvents pnlAddFolder As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlAdd As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlAdd1 As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblfileTitle As System.Web.UI.WebControls.Label
        Protected WithEvents lblFileType As System.Web.UI.WebControls.Label
        Protected WithEvents txtFileDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents lstCategories3 As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents btnAdd As System.Web.UI.WebControls.ImageButton
        Protected WithEvents lstFiles As System.Web.UI.WebControls.ListBox
        Protected WithEvents btnFileClose As System.Web.UI.WebControls.Button
        Protected WithEvents pnlAddFile As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents htmlUploadFile As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents txtOwnerID As System.Web.UI.WebControls.TextBox
        Protected WithEvents dlFolders As System.Web.UI.WebControls.DataList
        Protected WithEvents grdUpload As System.Web.UI.WebControls.DataGrid
        Protected WithEvents txtFileTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblFileInfo As System.Web.UI.WebControls.Label
        Protected WithEvents btnFileUpload As System.Web.UI.WebControls.Button

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
			btnEditOwner.Text = GetLanguage("Gal_Select")
			btnFolderSave.Text = GetLanguage("enregistrer")
			btnFolderSave.ToolTip = GetLanguage("Gal_SaveA")
			btnFolderClose.Text = GetLanguage("annuler")
			btnFolderClose.ToolTip = GetLanguage("Gal_Cancel")
			btnFileClose.Text = GetLanguage("annuler")
            btnFileClose.ToolTip = GetLanguage("annuler")
			btnFileUpload.Text = GetLanguage("upload")
			btnFileUpload.Tooltip = GetLanguage("upload")
			btnAddFolder.Text = GetLanguage("Gal_AddFolder")
			btnAddFolder.Tooltip = GetLanguage("Gal_AddFolderTip")
			btnAddFile.Text = GetLanguage("Gal_AddFile")
			btnAddFile.Tooltip = GetLanguage("Gal_AddFileTip")
			CancelButton.Text = GetLanguage("annuler")
			UpdateButton.Text = GetLanguage("enregistrer")
			CancelButton.ToolTip = GetLanguage("Gal_CancelTip")
			UpdateButton.ToolTip = GetLanguage("Gal_UpdateTip")
			btnAdd.ToolTip = GetLanguage("Gal_btnAddTip")
			btnAdd.AlternateText = GetLanguage("Gal_btnAddAlt")
			
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

            Dim _path As String = ""
            If Not Request.Params("path") Is Nothing Then
                _path = Request.Params("path")
            End If

            btnAdd.Attributes.Add("onclick", "toggleBox('" + btnAdd.ClientID + "',0);toggleBox('rotation',1)")


            Zrequest = New GalleryRequest(ZmoduleID)
            Zconfig = Zrequest.GalleryConfig
            Zfolder = Zrequest.Folder

            If Zconfig.IsValidPath Then
                If Not Zrequest.Folder.IsPopulated Then
                    Zrequest.Folder.Populate()
                End If
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
                ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
            End If
            If Not Page.IsPostBack AndAlso Zconfig.IsValidPath Then


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
                pnlAdd.Visible = True
                btnFileUpload.Visible = False

                If Zfolder.Parent Is Nothing Then
                    lblHeader.Text = GetLanguage("Gal_ModG")
                    UpdateButton.Visible = False
                Else
                    lblHeader.Text = GetLanguage("Gal_ModA")
                End If

                If Not Request.Params("action") Is Nothing Then
                    SetUpAction(Request.Params("action"))
                End If


            End If

        End Sub

        Private Function Authorize() As Boolean
            ' Authorized gallery
            If ZuserID > 0 Then
                If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                   OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                   OrElse (Zconfig.OwnerID = ZuserID) Then
                    ZGalleryAuthority = True
                Else
                    ZGalleryAuthority = False
                End If
            End If

            'BRT: only allow administrators or owners to edit Title, Description and Categories
            txtTitle.Enabled = ZGalleryAuthority
            txtDescription.Enabled = ZGalleryAuthority
            lstCategories.Enabled = ZGalleryAuthority
            'BRT: end

            If Zrequest.GalleryConfig.IsPrivate AndAlso Not ZGalleryAuthority Then
                Me.lblRefuse.Text = GetLanguage("Gal_Refuse")
                Me.pnlMain.Visible = False
                Me.UpdateButton.Visible = False
                Me.pnlRefuse.Visible = True
                Return False
            Else
                Me.pnlMain.Visible = True
                Me.UpdateButton.Visible = True
                Me.pnlRefuse.Visible = False
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
                txtOwner.Text = .Owner.UserName
                txtOwnerID.Text = .OwnerID.ToString
                txtTitle.Text = .Title
                txtDescription.Text = .Description
            End With

            BindChildItems()

        End Sub

        Private Sub BindCategories()

            Dim catList As ArrayList = Zconfig.Categories
            Dim catString As String

            ' Clear existing items in checkboxlist
            lstCategories.Items.Clear()

            For Each catString In catList

                Dim catItem As New ListItem()
                catItem.Value = catString
                catItem.Selected = False

                If InStr(1, LCase(Zfolder.Categories), LCase(catString)) > 0 Then
                    catItem.Selected = True
                End If
                'list category for current item
                lstCategories.Items.Add(catItem)

            Next

            lstCategories2.DataSource = Zconfig.Categories
            lstCategories2.DataBind()

            lstCategories3.DataSource = Zconfig.Categories
            lstCategories3.DataBind()

        End Sub

        Private Sub BindChildItems()


            grdFile.DataSource = Zrequest.FileItems
            grdFile.PageSize = (Zrequest.FileItems.Count + 1)
            btnAddFile.Visible = True


            grdDir.DataSource = Zrequest.SubAlbumItems

            grdDir.PageSize = grdDir.PageSize + (Zrequest.SubAlbumItems.Count + 1)

            btnAddFolder.Visible = True

            If Zrequest.SubAlbumItems.Count > 0 Then
                pnlContentDir.Visible = True
            Else
                pnlContentDir.Visible = False
            End If

            If Zrequest.FileItems.Count > 0 Then
                pnlContentFile.Visible = True
            Else
                pnlContentFile.Visible = False
            End If

            grdDir.DataBind()
            grdFile.DataBind()

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
                Dim albumTitle As String = Me.txtAlbumTitle.Text
                Dim albumDescription As String = txtAlbumDescription.Text
                Dim categories As String = GetCategories(lstCategories2)
                lblInfo.Text = strFolderInfo
                lblInfo.Text += Zfolder.CreateChild(albumName, albumTitle, albumDescription, categories, ZuserID)

                pnlAlbumDetails.Visible = True


                pnlAddFolder.Visible = False
                pnlAdd1.Visible = True
                pnlAdd.Visible = True

                GalleryConfig.ResetGalleryConfig(ZmoduleID)
                Zrequest = New GalleryRequest(ZmoduleID)
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
                End If
                If Not Request.Params("action") Is Nothing Then
                    Response.Redirect(CType(ViewState("UrlReferrer"), String))
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
                    If SpaceUsed = 0 Then
                        SpaceUsed = objAdmin.GetFolderSizeRecursive(strFolder)
                        objAdmin.AddDirectory(strFolder, SpaceUsed.ToString())
                    End If
                    ' CheckPortal Quota
                    If ((SpaceUsed / 1048576) >= _portalSettings.HostSpace) And (Not (Request.Params("hostpage") Is Nothing)) Then
                        lblFileInfo.Text = GetLanguage("Gal_NoSpaceLeft")
                        Return False
                    End If
                End If

                If ZRequest.GalleryConfig.Quota <> 0 Then
                    strFolder = HttpContext.Current.Request.MapPath(ZRequest.GalleryConfig.RootURL)
                    SpaceUsed = objAdmin.GetDirectorySpaceUsed(strFolder)
                    If SpaceUsed = 0 Then
                        SpaceUsed = objAdmin.GetFolderSizeRecursive(strFolder)
                        objAdmin.AddDirectory(strFolder, SpaceUsed.ToString())
                    End If


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


        Private Sub UpdateFolderSize(Optional ByVal FileSize As Double = 0)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim StrFolder As String
            Dim objAdmin As New AdminDB()
            If FileSize = 0 Then
                strFolder = Request.MapPath(_portalSettings.UploadDirectory)
                objAdmin.AddDirectory(strFolder, objAdmin.GetFolderSizeRecursive(strFolder).ToString())
                strFolder = Request.MapPath(Zconfig.RootURL)
                objAdmin.AddDirectory(strFolder, objAdmin.GetFolderSizeRecursive(strFolder).ToString())
            Else
                strFolder = Request.MapPath(_portalSettings.UploadDirectory)
                objAdmin.AddDirectory(strFolder, (objAdmin.GetDirectorySpaceUsed(strFolder) + FileSize).ToString())
                strFolder = Request.MapPath(Zconfig.RootURL)
                objAdmin.AddDirectory(strFolder, (objAdmin.GetDirectorySpaceUsed(strFolder) + FileSize).ToString())
            End If

        End Sub

        Private Sub grdDir_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDir.ItemCommand

            Dim itemIndex As Integer = Int16.Parse((CType(e.CommandSource, ImageButton).CommandArgument))
            'Dim name As String = (CType(e.CommandSource, ImageButton).CommandArgument)
            Dim selItem As IGalleryObjectInfo = CType(Zfolder.List.Item(itemIndex), IGalleryObjectInfo)


            Select Case (CType(e.CommandSource, ImageButton).CommandName)

                Case "delete"
                    lblInfo.Text = strFolderInfo
                    UpdateFolderSize(Zfolder.DeleteChild(selItem))
                    GalleryConfig.ResetGalleryConfig(ZmoduleID)
                    Zrequest = New GalleryRequest(ZmoduleID)
                    BindData()

                Case "edit"
                    Dim url As String = ""

                    If (Not selItem Is Nothing) AndAlso (selItem.IsFolder) Then
                        url = GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditAlbum & "&tabid=" & TabId & "&mid=" & ZmoduleID.ToString & "&path=" & CType(selItem, IGalleryObjectInfo).URL
                    ElseIf (Not selItem Is Nothing) AndAlso (Not selItem.IsFolder) Then
                        url = GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditFile & "&tabid=" & TabId & "&mid=" & ZmoduleID.ToString & "&path=" & CType(Zfolder, IGalleryObjectInfo).URL & "&editindex=" & selItem.Index.ToString
                    End If

                    If Len(url) > 0 Then
                        Response.Redirect(url)
                    Else
                        lblInfo.Text = GetLanguage("Gal_FileOE")
                    End If

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
                    UpdateFolderSize(Zfolder.DeleteChild(selItem))
                    GalleryConfig.ResetGalleryConfig(ZmoduleID)
                    Zrequest = New GalleryRequest(ZmoduleID)

                    BindData()

                Case "edit"
                    Dim url As String = ""

                    If (Not selItem Is Nothing) AndAlso (selItem.IsFolder) Then
                        url = GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditAlbum & "&tabid=" & TabId & "&mid=" & ZmoduleID.ToString & "&path=" & CType(selItem, IGalleryObjectInfo).URL
                    ElseIf (Not selItem Is Nothing) AndAlso (Not selItem.IsFolder) Then
                        url = GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditFile & "&tabid=" & TabId & "&mid=" & ZmoduleID.ToString & "&path=" & CType(Zfolder, IGalleryObjectInfo).URL & "&editindex=" & selItem.Index.ToString
                    End If

                    If Len(url) > 0 Then
                        Response.Redirect(url)
                    Else
                        lblInfo.Text = GetLanguage("Gal_FileOE")
                    End If

            End Select

        End Sub

        Private Sub grdFile_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdFile.ItemCreated
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





        Private Sub dlStrip_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)
            Dim cmdDelete As Control = e.Item.FindControl("delete")
            If Not cmdDelete Is Nothing Then
                CType(cmdDelete, ImageButton).Attributes.Add("onClick", "javascript: return confirm('" & rtesafe(GetLanguage("request_confirm")) & "')")
            End If
        End Sub

        Private Sub UpdateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateButton.Click

            If Zfolder.Parent Is Nothing Then ' GALLERY: No need to change, as admin or owner could use gallery admin for this task
                Dim admin As New AdminDB()
                admin.UpdateModuleSetting(ZmoduleID, "GalleryTitle", txtTitle.Text)
                admin.UpdateModuleSetting(ZmoduleID, "Description", txtDescription.Text)
                admin.UpdateModuleSetting(ZmoduleID, "Owner", txtOwner.Text)
                admin.UpdateModuleSetting(ZmoduleID, "OwnerID", txtOwnerID.Text)
                admin.UpdateModuleSetting(ZmoduleID, "Description", txtDescription.Text)
            Else ' ALBUM: Save changes into metadata.xml
                Dim directory As String = Zrequest.Folder.Parent.Path
                Dim name As String = Zrequest.Folder.Name
                Dim ownerID As Integer = Int16.Parse(txtOwnerID.Text)
                Dim title As String = txtTitle.Text
                Dim description As String = txtDescription.Text
                Dim categories As String = GetCategories(lstCategories)

                GalleryXML.SaveMetaData(directory, name, title, description, categories, ownerID, "0", "0")
                GalleryConfig.ResetGalleryConfig(ZmoduleID)
            End If

            Response.Redirect(CType(ViewState("UrlReferrer"), String))

        End Sub

        Private Function GetCategories(ByVal List As CheckBoxList) As String
            Dim catItem As ListItem
            Dim catString As String = ""

            For Each catItem In List.Items
                If catItem.Selected Then
                    catString += catItem.Value & ";"
                End If
            Next

            If Len(catString) > 0 Then
                Return catString.TrimEnd(";"c)
            Else
                Return ""
            End If

        End Function

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click



            If CheckQuota() Then
                'Retreive file upload collection
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
                    .Categories = GetCategories(lstCategories3)
                    .OwnerID = ZuserID
                End With

                txtFileTitle.Text = ""
                txtFileDescription.Text = ""
                lstCategories.ClearSelection()

                ' Check file valid size & type
                Dim validationInfo As String = UploadFile.ValidationInfo(ZmoduleID)
                If Len(validationInfo) > 0 Then
                    lblFileInfo.Text = validationInfo
                    Return
                End If

                'Check file exists
                If (Not ZuploadCollection.FileExists(UploadFile.Name)) Then
                    ZuploadCollection.Add(UploadFile)

                    Dim uploadPath As String
                    If UploadFile.Type = IGalleryObjectInfo.ItemType.Zip Then
                        uploadPath = BuildPath(New String(1) {Zrequest.Folder.Path, Zrequest.GalleryConfig.TempFolder}, "\", False, False)
                    Else
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
                        UpdateFolderSize(htmlUploadFile.PostedFile.ContentLength)
                    Catch Exc As System.Exception
                        lblFileInfo.Text = "<br> 1 - " + Exc.Message
                        Return
                    End Try
                Else
                    lblFileInfo.Text = GetLanguage("Gal_FileOEN")
                    Return
                End If

                ' Reset value & bind to grid
                grdUpload.DataSource = ZuploadCollection
                grdUpload.DataBind()
                btnFileUpload.Visible = True
                btnFileUpload.Attributes.Add("onclick", "toggleBox('" + btnFileUpload.ClientID + "',0);toggleBox('rotation1',1)")
            End If
        End Sub

        Private Sub grdUpload_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUpload.ItemCommand

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
                        lblFileInfo.Text = "<br> 2 - " + Exc.Message
                        Return
                    End Try
                    ZuploadCollection.RemoveAt(itemIndex)
                    grdUpload.DataSource = ZuploadCollection
                    grdUpload.DataBind()
                Case "edit"

            End Select
        End Sub

        Private Sub btnFileUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFileUpload.Click

            Dim StrFolder As String

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objAdmin As New AdminDB()

            strFolder = Request.MapPath(_portalSettings.UploadDirectory)

            Me.lblFileInfo.Text = ""

            ZuploadCollection = GalleryUploadCollection.GetList(Zfolder, ZmoduleID)

            ' Update Directory Size
            UpdateFolderSize(ZuploadCollection.Upload(StrFolder))

            If Len(ZuploadCollection.ErrMessage) > 0 Then
                lblFileInfo.Text = ZuploadCollection.ErrMessage
            Else
                If Not Request.Params("action") Is Nothing Then
                    ZuploadCollection.ResetList(Zfolder, ZmoduleID)
                    Zrequest.GalleryConfig.ResetGalleryConfig(ZmoduleID)
                    Response.Redirect(CType(ViewState("UrlReferrer"), String))
                End If

            End If

            ZuploadCollection.ResetList(Zfolder, ZmoduleID)
            grdUpload.DataBind() ' Clear old file list
            btnFileUpload.Visible = False
            Zrequest.GalleryConfig.ResetGalleryConfig(ZmoduleID)
            Zrequest = New GalleryRequest(ZmoduleID)

            BindData()


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
            Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub

        Private Sub btnAddFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddFile.Click
			
 				lblInfo.Text = strFolderInfo
				
				If CheckQuota() then
	            pnlAddFile.Visible = True
				pnlAdd.Visible = False
				pnlAdd1.Visible = False
				btnFileUpload.visible = False
				pnlAddFolder.Visible = False
				pnlAlbumDetails.Visible = False
				pnlContentdir.Visible = false
				if Zrequest.FileItems.Count > 0 then
				pnlContentFile.Visible = true
				else
				pnlContentFile.Visible = false
				end if
				Else
				lblInfo.Text = lblFileInfo.Text
				End if
				
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
            pnlContentDir.Visible = False
            pnlContentFile.Visible = False
            pnlAddFile.Visible = False


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

        Private Sub btnAddFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddFolder.Click
            'BRT: clear for next add
            txtNewAlbum.Text = ""
            txtAlbumTitle.Text = ""
            txtAlbumDescription.Text = ""
            'BRT: end
            pnlAddFolder.Visible = True
			pnlAdd1.Visible = False
			pnlAdd.Visible = False
			pnlAddFile.Visible = False
			pnlAlbumDetails.Visible = False
			pnlContentFile.Visible = false
			
			if Zrequest.SubAlbumItems.Count >  0 then
 			pnlContentDir.Visible = true
			else
			pnlContentDir.Visible = false
			end if

			
        End Sub

        Private Sub btnFileClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFileClose.Click
            If Not Request.Params("action") Is Nothing Then
                Response.Redirect(CType(ViewState("UrlReferrer"), String))
            Else

                pnlAddFile.Visible = False
                pnlAlbumDetails.Visible = True
                pnlAdd.Visible = True
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
                End If
            End If
        End Sub

        Private Sub btnFolderClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFolderClose.Click
            If Not Request.Params("action") Is Nothing Then
                Response.Redirect(CType(ViewState("UrlReferrer"), String))
            Else
 

                pnlAddFolder.Visible = False
                pnlAlbumDetails.Visible = True
                pnlAdd1.Visible = True
                pnlAdd.Visible = True

                If Zrequest.SubAlbumItems.Count > 0 Then
                    pnlContentDir.Visible = True
                Else
                    pnlContentDir.Visible = False
                End If

                If Zrequest.FileItems.Count > 0 Then
                    pnlContentFile.Visible = True
                Else
                    pnlContentFile.Visible = False
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

        Private Sub btnEditOwner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditOwner.Click
            pnlSelectOwner.Visible = True
            ctlUsers.BindUsers()
        End Sub

        Private Sub ctlUsers_UserSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlUsers.UserSelected
            Dim myUser As ForumUser = CType(ctlUsers.SelectedUser, ForumUser)

            txtOwner.Text = myUser.Name
            txtOwnerID.Text = myUser.UserID.ToString

            pnlSelectOwner.Visible = False

        End Sub
		
		
    End Class
End Namespace