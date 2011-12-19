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
Imports System.Text

Namespace DotNetZoom
    Public Class TTT_GalleryViewer
        Inherits DotNetZoom.BasePage
        Protected WithEvents Title1 As System.Web.UI.WebControls.Label
        Protected WithEvents Image As System.Web.UI.WebControls.Image
        Protected WithEvents MovePrevious As System.Web.UI.WebControls.HyperLink
        Protected WithEvents MoveNext As System.Web.UI.WebControls.HyperLink
        Protected WithEvents ReturnTo As System.Web.UI.WebControls.HyperLink
        Protected WithEvents FocusTo As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdAlbum As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
        Protected WithEvents Album As System.Web.UI.WebControls.Label
        Protected WithEvents info As System.Web.UI.WebControls.Image
        Protected WithEvents table As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents img As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents Edit As System.Web.UI.WebControls.ImageButton
        Protected WithEvents Delete As System.Web.UI.WebControls.ImageButton
        Protected WithEvents pnledit As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtWaterMark As System.Web.UI.WebControls.TextBox
        Protected WithEvents imgFileIcon As System.Web.UI.WebControls.Image
        Protected WithEvents gpsIconImage As System.Web.UI.WebControls.DataList
        Protected WithEvents UpdateButton As System.Web.UI.WebControls.Button
        Protected WithEvents ddCategories As System.Web.UI.WebControls.DropDownList


        Protected Zrequest As GalleryViewerRequest
        Protected Zconfig As GalleryConfig
        Protected ModuleID As Integer

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
            GalleryConfig.SetSkinCSS(Me.Page)
            MovePrevious.ImageUrl = glbPath & "images/lt.gif"
            MovePrevious.ToolTip = GetLanguage("Gal_Prev")
            MoveNext.ImageUrl = glbPath & "images/rt.gif"
            MoveNext.ToolTip = GetLanguage("Gal_Next")
            ReturnTo.ImageUrl = glbPath & "images/cancel.gif"
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim _ModuleSettings As New ModuleSettings
            ModuleID = Int32.Parse(HttpContext.Current.Request("mid"))
            Dim isinrole As Boolean = False
            Dim Editrole As Boolean = False
            For Each _ModuleSettings In _portalSettings.ActiveTab.Modules
                If _ModuleSettings.ModuleId = ModuleID Then
                    If PortalSecurity.IsInRoles(CStr(IIf(_ModuleSettings.AuthorizedViewRoles <> "", _ModuleSettings.AuthorizedViewRoles, _portalSettings.ActiveTab.AuthorizedRoles))) Then
                        isinrole = True
                    End If
                    If PortalSecurity.IsInRoles(CStr(IIf(_ModuleSettings.AuthorizedEditRoles <> "", _ModuleSettings.AuthorizedEditRoles, _portalSettings.ActiveTab.AdministratorRoles))) Then
                        Editrole = True
                    End If
                End If
            Next
            If Not isinrole Then
                RegisterBADip(Request.UserHostAddress)
                Dim Admin As New AdminDB()
                Title1.Text = ProcessLanguage(Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "AccessDeniedInfo"), Page)
                ReturnTo.NavigateUrl = GetFullDocument()
                ReturnTo.Attributes.Add("onClick", "return Cancel(this)")
            Else

                Edit.Visible = Editrole
                Delete.Visible = Editrole
                If Not Page.IsPostBack Then
                    If Not Session("reset") Is Nothing Then
                        Zconfig = GalleryConfig.GetGalleryConfig(ModuleID)
                        Zrequest = New GalleryViewerRequest(ModuleID)
                        ResetGal()
                    End If
                End If
                Zconfig = GalleryConfig.GetGalleryConfig(ModuleID)
                Zrequest = New GalleryViewerRequest(ModuleID)
                If Not Zconfig.RootFolder.IsPopulated Then
                    Zrequest.Folder.LogEvent("Config not populated -> PostBack : " + Page.IsPostBack.ToString + vbCrLf)
                    'Response.Redirect(Request.RawUrl)
                End If

                If Not Zrequest.Folder.IsPopulated Then
                    ' Zrequest.Folder.Populate()
                    ' Server.Transfer("~/DesktopModules/tttGallery/TTT_cache.aspx")
                    Zrequest.Folder.LogEvent("Folder not populated -> PostBack : " + Page.IsPostBack.ToString + vbCrLf)
                    'Response.Redirect(Request.RawUrl)
                End If

                Dim sb As New StringBuilder()

                ' Set the backward nav
                sb.Append("TTT_viewer.aspx?L=" & GetLanguage("N") & "&path=")
                sb.Append(Zrequest.Path)
                sb.Append("&currentitem=")
                sb.Append(CStr(Zrequest.PreviousItem))
                sb.Append("&mid=")
                sb.Append(ModuleID.ToString)
                sb.Append("&tabid=")
                sb.Append(_portalSettings.ActiveTab.TabId)

                MovePrevious.NavigateUrl = sb.ToString
                sb.Length = 0

                ' Set forward nav
                sb.Append("TTT_viewer.aspx?L=" & GetLanguage("N") & "&path=")
                sb.Append(Zrequest.Path)
                sb.Append("&tabid=")
                sb.Append(_portalSettings.ActiveTab.TabId)
                sb.Append("&currentitem=")
                sb.Append(CStr(Zrequest.NextItem))
                sb.Append("&mid=")
                sb.Append(ModuleID.ToString)
                MoveNext.NavigateUrl = sb.ToString
                sb.Length = 0


                If Zrequest.GalleryConfig.CheckboxGPS Then
                    ReturnTo.NavigateUrl = "javascript:window.top.close();window.top.opener.focus();"
                    ReturnTo.ToolTip = GetLanguage("Gal_MapReturn")
                    FocusTo.ImageUrl = glbPath & "images/up.gif"
                    FocusTo.ToolTip = GetLanguage("Gal_MapFocus")
                    FocusTo.NavigateUrl = "javascript:window.top.opener.focus();window.top.opener.location.href = 'javascript:GV_Center_On_Marker(\'" + Zrequest.CurrentItem.Name + "\', {zoom: 15, open_info_window : true});'"
                    FocusTo.Visible = True
                Else
                    ReturnTo.ToolTip = GetLanguage("Gal_return")
                    sb.Length = 0

                    sb.Append(GetFullDocument() & "?tabid=")
                    sb.Append(_portalSettings.ActiveTab.TabId)
                    sb.Append("&path=")
                    sb.Append(Zrequest.Path)
                    sb.Append("&currentstrip=" & Zrequest.currentstrip)
                    ReturnTo.NavigateUrl = sb.ToString
                    ReturnTo.Attributes.Add("onClick", "return Cancel(this)")
                End If

                ' Image properties
                If Zrequest.Folder.Title <> "" Then
                    Album.Text = Zrequest.Folder.Title
                Else
                    Album.Text = Zrequest.Folder.Name
                End If

                Dim config As GalleryConfig = GalleryConfig.GetGalleryConfig(ModuleID)



                If config.InfoBule Then
                    ' Image properties
                    Dim TempTooltip As String
                    If File.Exists(Server.MapPath(Zrequest.CurrentItem.URL)) Then
                        Dim Exif As New ExifWorks(Server.MapPath(Zrequest.CurrentItem.URL))
                        TempTooltip = Exif.ToString()
                        If TempTooltip <> "" Then
                            info.Visible = True
                            info.Attributes.Add("onmouseover", ReturnToolTip(TempTooltip, "200"))
                        End If
                        Exif.Dispose()
                        If Zrequest.CurrentItem.Description <> "" Then
                            Image.Attributes.Add("onmouseover", ReturnToolTip("<pre>" & Zrequest.CurrentItem.Description & "</pre>"))
                        End If
                    End If
                End If



                Title1.Text = Zrequest.CurrentItem.Title.Replace(vbCrLf, "<br>")

                lblInfo.Text = Replace(GetLanguage("Gal_ImgInfo"), "{imgnum}", Zrequest.CurrentItemNumber.ToString())
                lblInfo.Text = Replace(lblInfo.Text, "{albumnum}", Zrequest.Folder.BrowsableItems.Count.ToString())
                lblInfo.Text = Replace(lblInfo.Text, "{imgname}", Zrequest.CurrentItem.Name)
                lblInfo.Text = Replace(lblInfo.Text, "{imgsize}", Math.Ceiling(Zrequest.CurrentItem.Size / 1024).ToString)
                If Zrequest.GalleryConfig.CheckboxGPS Then
                    lblInfo.ToolTip = GetLanguage("Gal_MapFocus")
                    lblInfo.Text = "<a href=""javascript:window.top.opener.focus();window.top.opener.location.href = 'javascript:GV_Center_On_Marker(\'" + Zrequest.CurrentItem.Name + "\', {zoom: 15, open_info_window : true});'"">" + lblInfo.Text + "</a>"
                End If
                End If

                If Not Page.IsPostBack Then
                'Image.ImageUrl = CryptoUrl(Zrequest.CurrentItem.URL, config.IsPrivate)
                img.Height = CStr(Zrequest.GalleryConfig.FixedHeight + 4)
                img.Width = CStr(Zrequest.GalleryConfig.FixedWidth + 4)
                Image.ImageUrl = Zrequest.CurrentItem.URL
                If Zrequest.CurrentItem.Width <> "0" Then
                    CalculatePhotoSize()
                End If


                End If
        End Sub

        Private Sub CalculatePhotoSize()
            Dim lWidth As Integer = CInt(Zrequest.CurrentItem.Width)
            Dim lHeight As Integer = CInt(Zrequest.CurrentItem.Height)
            Dim newWidth As Integer = lWidth
            Dim newHeight As Integer = lHeight
            Dim sRatio As Double

            If (lWidth > Zrequest.GalleryConfig.FixedWidth Or lHeight > Zrequest.GalleryConfig.FixedHeight) Or (lWidth < Zrequest.GalleryConfig.FixedWidth And lHeight < Zrequest.GalleryConfig.FixedHeight) Then
                sRatio = (lHeight / lWidth)
                If sRatio > 1 Then ' Bounded by height
                    newWidth = CShort(Zrequest.GalleryConfig.FixedWidth / sRatio)
                    newHeight = Zrequest.GalleryConfig.FixedHeight
                Else 'Bounded by width
                    newWidth = Zrequest.GalleryConfig.FixedWidth
                    newHeight = CShort(Zrequest.GalleryConfig.FixedHeight * sRatio)
                End If
            End If
            Image.Width = Unit.Pixel(CInt(newWidth))
            Image.Height = Unit.Pixel(CInt(newHeight))

        End Sub

        Private Function CheckEdit() As Boolean
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim _ModuleSettings As New ModuleSettings
            Dim ModuleID As Integer = Int32.Parse(HttpContext.Current.Request("mid"))
            Dim isinrole As Boolean = False
            For Each _ModuleSettings In _portalSettings.ActiveTab.Modules
                If _ModuleSettings.ModuleId = ModuleID Then
                    If PortalSecurity.IsInRoles(CStr(IIf(_ModuleSettings.AuthorizedEditRoles <> "", _ModuleSettings.AuthorizedEditRoles, _portalSettings.ActiveTab.AdministratorRoles))) Then
                        isinrole = True
                    End If
                End If
            Next
            If Not isinrole Then
                RegisterBADip(Request.UserHostAddress)
                Dim Admin As New AdminDB()
                Title1.Text = ProcessLanguage(Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "AccessDeniedInfo"), Page)
                ReturnTo.NavigateUrl = GetFullDocument()
                ReturnTo.Attributes.Add("onClick", "return Cancel(this)")

            Else


                Zconfig = GalleryConfig.GetGalleryConfig(ModuleID)
                Zrequest = New GalleryViewerRequest(ModuleID)
                If Not Zconfig.RootFolder.IsPopulated Then
                    Zrequest.Folder.LogEvent("Config not populated -> PostBack : " + Page.IsPostBack.ToString + vbCrLf)
                    'Response.Redirect(Request.RawUrl)
                End If

                If Not Zrequest.Folder.IsPopulated Then
                    ' Zrequest.Folder.Populate()
                    ' Server.Transfer("~/DesktopModules/tttGallery/TTT_cache.aspx")
                    Zrequest.Folder.LogEvent("Folder not populated -> PostBack : " + Page.IsPostBack.ToString + vbCrLf)
                    'Response.Redirect(Request.RawUrl)
                End If
            End If
            Return isinrole
        End Function

        Private Sub Edit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Edit.Click
            If CheckEdit() Then
                SetUpEdit()
            End If
        End Sub

        Private Sub ResetGal()
            GalleryConfig.ResetGalleryConfig(ModuleID)
            Zrequest.Folder.Reset()
            ' so remove the from cache
            ClearModuleCache(ModuleID)
            Session("reset") = Nothing
        End Sub

        Private Sub Delete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Delete.Click
            If CheckEdit() Then
                Dim Item As IGalleryObjectInfo = CType(Zrequest.CurrentItem, IGalleryObjectInfo)
                UpdateFolderSize(Zconfig, Zrequest.Folder.DeleteChild(Item))
                ResetGal()
                ' Session("reset") = Zrequest.Path
                Response.Redirect(MoveNext.NavigateUrl)
            End If
        End Sub

        Private Sub SetUpEdit()
            pnledit.Visible = True
            table.Visible = True
            img.Height = CStr(CInt(img.Height) / 2)

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

                If InStr(1, Zrequest.CurrentItem.Categories, catString) > 0 Then
                    catItem.Selected = True
                End If
                'list category for current item
                ddCategories.Items.Add(catItem)
            Next

            If Not ddCategories.Items.FindByValue(Zrequest.CurrentItem.Categories) Is Nothing Then
                ddCategories.Items.FindByValue(Zrequest.CurrentItem.Categories).Selected = True
            End If

            txtTitle.Text = Zrequest.CurrentItem.Title
            txtDescription.Text = Zrequest.CurrentItem.Description

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

            strExtension = ""
            If InStr(1, Zrequest.CurrentItem.Name, ".") <> 0 Then
                strExtension = Mid(Zrequest.CurrentItem.Name, InStrRev(Zrequest.CurrentItem.Name, ".") + 1)
            End If


            Select Case strExtension.ToLower()
                Case "jpg", "jpeg", "tif", "png"
                    Dim Exif As New ExifWorks(Server.MapPath(Zrequest.CurrentItem.URL))
                    txtWaterMark.Text = Exif.UserComment
                    If txtWaterMark.Text <> "" Then
                        txtWaterMark.Enabled = False
                    End If
                    Exif.Dispose()
                Case Else
                    txtWaterMark.Enabled = False
            End Select

            gpsIconImage.DataSource = slImages
            gpsIconImage.DataBind()
            gpsIconImage.Visible = Zconfig.CheckboxGPS
            Dim metaData As New GalleryXML(Zrequest.Folder.Path)
            imgFileIcon.ImageUrl = Zrequest.CurrentItem.GPSIcon
            imgFileIcon.Visible = Zconfig.CheckboxGPS
            UpdateButton.Text = GetLanguage("enregistrer")
            UpdateButton.ToolTip = GetLanguage("Gal_UpdateTip")

        End Sub

        Private Sub GPSIconImage_ItemCommand(ByVal Sender As System.Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles gpsIconImage.ItemCommand
            If CheckEdit() Then
                imgFileIcon.ImageUrl = CStr(e.CommandArgument)
                Dim directory As String = Zrequest.Folder.Path
                Dim name As String = Zrequest.CurrentItem.Name
                Dim mImage As System.Drawing.Image
                mImage = System.Drawing.Image.FromFile(Request.MapPath(imgFileIcon.ImageUrl))
                GalleryXML.Savegpsicon(directory, name, imgFileIcon.ImageUrl, "[" + mImage.Width.ToString + "," + mImage.Height.ToString + "]")
                imgFileIcon.Width = CType(mImage.Width.ToString, Unit)
                imgFileIcon.Height = CType(mImage.Height.ToString, Unit)
                mImage.Dispose()
                Session("reset") = Zrequest.Path
            End If
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

        Private Sub UpdateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateButton.Click

            Dim directory As String = Zrequest.Folder.Path
            Dim metaData As New GalleryXML(directory)
            Dim What As IGalleryObjectInfo = metaData.CompleteInfo(Zrequest.Currentitem.name)
            What.Title = txtTitle.Text
            What.Description = txtDescription.Text
            What.Categories = ddCategories.SelectedItem.Value

            Dim strExtension As String = ""
            If InStr(1, What.Name, ".") <> 0 Then
                strExtension = Mid(What.Name, InStrRev(What.Name, ".") + 1)
            End If


            Select Case strExtension.ToLower()
                Case "jpg", "jpeg", "tif", "png"
                    Dim fileName As String
                    fileName = Server.MapPath(Zrequest.CurrentItem.URL)
                    Try
                        Dim Exif As New ExifWorks(fileName)
                        If What.Latitude = "" Then
                            What.Latitude = Exif.Latitude(CType(3, ExifWorks.LatLongFormat))
                            What.Longitude = Exif.Longitude(CType(3, ExifWorks.LatLongFormat))
                        End If
                        If What.Latitude = "" Then
                            What.Latitude = "0"
                            What.Longitude = "0"
                        End If
                        Dim TGalleryUser As GalleryUser = New GalleryUser(Zrequest.CurrentItem.OwnerID)
                        If Exif.UserComment <> txtWaterMark.Text Or Exif.Artist <> TGalleryUser.UserName Or Exif.Title <> txtTitle.Text Or Exif.Copyright <> GetDomainName(Request) Or Exif.Description <> txtDescription.Text Then
                            Exif.Artist = TGalleryUser.UserName
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
                                Image.ImageUrl = Zrequest.CurrentItem.URL + "?" + DateTime.Now.ToString("yyyyMMddHHmmss")
                            End If
                            BMP.Save(fileName & ".tmp")
                            BMP.Dispose()
                            Exif.Dispose()
                            System.IO.File.Delete(fileName)
                            System.IO.File.Move(fileName & ".tmp", fileName)
                        Else
                            Exif.Dispose()
                        End If


                    Catch ex As Exception

                    End Try

            End Select

            ' Put Sort in the XML
            GalleryXML.SaveGalleryData(directory, What)
            Session("reset") = Zrequest.Path

            'GalleryConfig.ResetGalleryConfig(ModuleID)
            'Zrequest.Folder.Reset()
            'ClearModuleCache(ModuleID)
            pnledit.Visible = False
            table.Visible = True
        End Sub


    End Class

End Namespace