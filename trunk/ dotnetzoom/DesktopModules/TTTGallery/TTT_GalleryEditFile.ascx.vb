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
' by René Boulard ( http://www.reneboulard.qc.ca)'
'========================================================================================
Option Strict On

Imports System.IO
Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports System.Text

Namespace DotNetZoom
    Public Class TTT_GalleryEditFile
        Inherits DotNetZoom.PortalModuleControl

        
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Dim ZmoduleID As Integer
        Protected Zrequest As GalleryRequest
        Protected Zeditindex As Integer
        Protected Zconfig As GalleryConfig
        Protected Zfolder As GalleryFolder
        Protected ZFile As GalleryFile

		Protected WithEvents ctlUsers As TTT_UsersControl
		Protected WithEvents pnlSelectOwner As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents btnEditOwner As System.Web.UI.WebControls.Button
        Protected WithEvents pnlFileDetails As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
        Protected WithEvents txtPath As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtSortOrder As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtOwner As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtWaterMark As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents ddCategories As System.Web.UI.WebControls.DropDownList
        Protected WithEvents CancelButton As System.Web.UI.WebControls.Button
        Protected WithEvents imgFile As System.Web.UI.WebControls.Image
        Protected WithEvents imgFileIcon As System.Web.UI.WebControls.Image
        Protected WithEvents gpsIconImage As System.Web.UI.WebControls.DataList
        Protected WithEvents div1 As System.Web.UI.HtmlControls.HtmlGenericControl

        Protected WithEvents txtOwnerID As System.Web.UI.WebControls.TextBox
        Protected WithEvents dlFolders As System.Web.UI.WebControls.DataList
        Protected WithEvents UpdateButton As System.Web.UI.WebControls.Button
        Protected WithEvents MagicFile As System.Web.UI.WebControls.HyperLink

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
			CancelButton.Text = GetLanguage("annuler")
			UpdateButton.Text = GetLanguage("enregistrer")
			CancelButton.ToolTip = GetLanguage("Gal_CancelTip")
			UpdateButton.ToolTip = GetLanguage("Gal_UpdateTip")

            If IsNumeric(Request.Params("mid")) Then
                ZmoduleID = Int32.Parse(Request.Params("mid"))
            End If

            If IsNumeric(Request.Params("editindex")) Then
                Zeditindex = Int32.Parse(Request.Params("editindex"))
            End If

            Zrequest = New GalleryRequest(ZmoduleID)
            Zconfig = Zrequest.GalleryConfig
            Zfolder = Zrequest.Folder
            ZFile = CType(Zrequest.Folder.List.Item(Zeditindex), GalleryFile)

            If Not Page.IsPostBack Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If
            End If
            If Not Page.IsPostBack AndAlso Zconfig.IsValidPath Then

                If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                            OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                OrElse (PortalSecurity.IsInRoles(CType(PortalSettings.GetEditModuleSettings(ZmoduleID), ModuleSettings).AuthorizedEditRoles.ToString) = True) Then
                    btnEditOwner.Visible = True
                Else
                    btnEditOwner.Visible = False
                End If


                'BindData(Zrequest)
                BindData()


            End If
            If Not ZFile.Type = IGalleryObjectInfo.ItemType.Image Then
                MagicFile.NavigateUrl = GetEditIconURL()
                Dim delim As String = "/"
                Dim tempRootURL As String = Zconfig.RootURL
                tempRootURL = tempRootURL.Trim(delim.ToCharArray()) & "/"
                tempRootURL += Zrequest.Path
                tempRootURL = tempRootURL.Trim(delim.ToCharArray()) & "/_res"
                ' Authorized gallery
                System.Web.HttpContext.Current.Session("UploadPath") = tempRootURL
                System.Web.HttpContext.Current.Session("ReturnPath") = ViewState("UrlReferrer")

            End If

        End Sub

        Private Function GetEditIconURL() As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim sb As New StringBuilder()
            sb.Append(glbPath & "DesktopModules/TTTGallery/magicfile.aspx")
            sb.Append("?name=")
            sb.Append(IO.Path.GetFileNameWithoutExtension(ZFile.Name))
            sb.Append("&tabid=" & CStr(_portalSettings.ActiveTab.TabId))
            sb.Append("&path=")
            sb.Append(Zrequest.Path)
            sb.Append("&mid=")
            sb.Append(ModuleId)
            sb.Append("&L=" & GetLanguage("N"))
            Return sb.ToString
        End Function

        'Private Sub BindData(ByVal Request As GalleryRequest)
        Private Sub BindData()


            dlFolders.DataSource = Zrequest.FolderPaths
            dlFolders.DataBind()

            With ZFile
                txtPath.Text = .URL
                txtName.Text = .Name
                Dim TGalleryUser As GalleryUser = New GalleryUser(.OwnerID)
                txtOwner.Text = TGalleryUser.UserName
                txtOwnerID.Text = .OwnerID.ToString
                txtTitle.Text = .Title
                txtSortOrder.Text = .Sort
                txtDescription.Text = .Description
                imgFile.ImageUrl = .ThumbNail
            End With

            BindCategories()

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

            imgFileIcon.ImageUrl = ZFile.GPSIcon
            imgFileIcon.Visible = Zconfig.CheckboxGPS
            div1.Visible = Zconfig.CheckboxGPS
            strExtension = ""
            If InStr(1, ZFile.Name, ".") <> 0 Then
                strExtension = Mid(ZFile.Name, InStrRev(ZFile.Name, ".") + 1)
            End If


            Select Case strExtension.ToLower()
                Case "jpg", "jpeg", "tif", "png"
                    Dim Exif As New ExifWorks(Server.MapPath(ZFile.URL))
                    txtWaterMark.Text = Exif.UserComment
                    If txtWaterMark.Text <> "" Then
                        txtWaterMark.Enabled = False
                    End If
                    Exif.Dispose()
                Case Else
                    txtWaterMark.Enabled = False
            End Select
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

                If InStr(1, ZFile.Categories, catString) > 0 Then
                    catItem.Selected = True
                End If
                'list category for current item
                ddCategories.Items.Add(catItem)
            Next

            If Not ddCategories.Items.FindByValue(ZFile.Categories) Is Nothing Then
                ddCategories.Items.FindByValue(ZFile.Categories).Selected = True
            End If

        End Sub

        Private Sub UpdateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateButton.Click

            Dim directory As String = Zrequest.Folder.Path
            Dim metaData As New GalleryXML(directory)
            Dim What As IGalleryObjectInfo = metaData.CompleteInfo(ZFile.name)


            What.Title = txtTitle.Text
            What.Description = txtDescription.Text
            What.Categories = ddCategories.SelectedItem.Value
            What.OwnerID = Int16.Parse(Me.txtOwnerID.Text)
            What.Sort = txtSortOrder.Text
            What.gpsIcon = imgFileIcon.ImageUrl
            Dim strExtension As String = ""
            If InStr(1, What.Name, ".") <> 0 Then
                strExtension = Mid(What.Name, InStrRev(What.Name, ".") + 1)
            End If


            Select Case strExtension.ToLower()
                Case "jpg", "jpeg", "tif", "png"
                    Dim fileName As String
                    fileName = Server.MapPath(ZFile.URL)
                    Dim Exif As New ExifWorks(fileName)
                    If What.Latitude = "" Then
                        What.Latitude = Exif.Latitude(CType(3, ExifWorks.LatLongFormat))
                        What.Longitude = Exif.Longitude(CType(3, ExifWorks.LatLongFormat))
                    End If
                    If What.Latitude = "" Then
                        What.Latitude = "0"
                        What.Longitude = "0"
                    End If
                    If Exif.UserComment <> txtWaterMark.Text Or Exif.Artist <> txtOwner.Text Or Exif.Title <> txtTitle.Text Or Exif.Copyright <> GetDomainName(Request) Or Exif.Description <> txtDescription.Text Then
                        Exif.Artist = txtOwner.Text
                        Exif.Copyright = GetDomainName(Request)
                        Exif.Description = txtDescription.Text
                        Exif.Title = txtTitle.Text
                        Exif.UserComment = txtWaterMark.Text
                        Dim BMP As System.Drawing.Bitmap = Exif.GetBitmap()
                        If txtWaterMark.Enabled Then
                            BMP = watermrk(BMP, txtWaterMark.Text)
                            Dim tBMP As System.Drawing.Bitmap = Exif.GetBitmap()
                            Dim PropItm As Drawing.Imaging.PropertyItem
                            For Each PropItm In tBMP.PropertyItems
                                BMP.SetPropertyItem(PropItm)
                            Next
                            tBMP.Dispose()
                        End If
                        BMP.Save(fileName & ".tmp")
                        BMP.Dispose()
                        Exif.Dispose()
                        System.IO.File.Delete(fileName)
                        System.IO.File.Move(fileName & ".tmp", fileName)

                    Else
                        Exif.Dispose()
                    End If


            End Select

            ' put a watermark on file


            ' Put Sort in the XML
            GalleryXML.SaveGalleryData(directory, What)


            GalleryConfig.ResetGalleryConfig(ZmoduleID)
            Zfolder.Reset()
            ClearModuleCache(ZmoduleID)

            Response.Redirect(CType(ViewState("UrlReferrer"), String))

        End Sub


        Private Function GetRootURL() As String
            ' Return GetFullDocument() & "?" & "&tabid=" & portalSettings.GetEditModuleSettings(ZmoduleID).TabId
            ' to return to prévious album
            Dim sb As New StringBuilder()
            sb.Append(getFulldocument())
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

        Private Sub btnEditOwner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditOwner.Click
            pnlSelectOwner.Visible = True
            ctlUsers.BindUsers()
        End Sub

        Private Sub GPSIconImage_ItemCommand(ByVal Sender As System.Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles gpsIconImage.ItemCommand
            imgFileIcon.ImageUrl = CStr(e.CommandArgument)
            Dim directory As String = Zrequest.Folder.Path
            Dim name As String = ZFile.Name
            Dim mImage As System.Drawing.Image
            mImage = System.Drawing.Image.FromFile(Request.MapPath(imgFileIcon.ImageUrl))
            GalleryXML.Savegpsicon(directory, name, imgFileIcon.ImageUrl, "[" + mImage.Width.ToString + "," + mImage.Height.ToString + "]")
            imgFileIcon.Width = CType(mImage.Width.ToString, Unit)
            imgFileIcon.Height = CType(mImage.Height.ToString, Unit)
            mImage.Dispose()

        End Sub

        Private Sub ctlUsers_UserSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlUsers.UserSelected
            Dim myUser As ForumUser = CType(ctlUsers.SelectedUser, ForumUser)

            txtOwner.Text = myUser.Name
            txtOwnerID.Text = myUser.UserID.ToString

            pnlSelectOwner.Visible = False

        End Sub
		
		
		
    End Class
End Namespace