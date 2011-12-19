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

Imports System.Text
Imports DotNetZoom
Imports DotNetZoom.TTTUtils


Namespace DotNetZoom
    Public Class TTT_Gallery
        Inherits DotNetZoom.PortalModuleControl

        ' Obtain PortalSettings from Current Context
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents dlFolders As System.Web.UI.WebControls.DataList


        Protected WithEvents ClearCache As System.Web.UI.WebControls.ImageButton
        Protected WithEvents SubAlbum As System.Web.UI.WebControls.ImageButton
        Protected WithEvents UploadImage As System.Web.UI.WebControls.ImageButton
        Protected WithEvents Stats As System.Web.UI.WebControls.Literal
        Protected WithEvents dlPager As System.Web.UI.WebControls.DataList
        Protected WithEvents dlStrip As System.Web.UI.WebControls.DataList
        Protected WithEvents dlPager2 As System.Web.UI.WebControls.DataList
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Literal
        Protected WithEvents BrowserLink As System.Web.UI.WebControls.HyperLink

        Protected WithEvents SlideshowLink As System.Web.UI.WebControls.HyperLink

        Protected WithEvents lnkAdmin As System.Web.UI.WebControls.HyperLink

        Protected WithEvents lnkManager As System.Web.UI.WebControls.HyperLink

        Protected WithEvents lblPageInfo2 As System.Web.UI.WebControls.Literal
        Protected WithEvents lblPageInfo As System.Web.UI.WebControls.Literal
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Private mGalleryAuthority As Boolean = False
        Private mGalleryEdit As Boolean = False
        Private mAllowDownload As Boolean = False
        Private mForumIntegrate As Boolean = False
        Private ZuserID As Integer = 0
        Private Zrequest As GalleryRequest
        Private config As GalleryConfig
	    Private bCanDiscuss As Boolean = False
        Private bCanView As Boolean = False

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
            Title1.EditText = GetLanguage("editer")
            ClearCache.ToolTip = GetLanguage("Gal_Clear")
            ClearCache.Style.Add("background", "url('" & ForumConfig.DefaultImageFolder() & "forum.gif" & "') no-repeat")
            ClearCache.Style.Add("background-position", "0px -256px")
            SubAlbum.ToolTip = GetLanguage("Gal_AddFolderTip")
            UploadImage.ToolTip = GetLanguage("Gal_AddFileTip")
			lnkAdmin.Text =  getlanguage("Gal_SetUp")

			lnkManager.Text =  getlanguage("Gal_SetUp1")

			BrowserLink.Text =  getlanguage("Gal_BrowserLink")

			SlideshowLink.Text =  getlanguage("Gal_showLink")

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            System.Web.HttpContext.Current.Session("ReturnPath") = Request.RawUrl
            Zrequest = New GalleryRequest(ModuleId)
            config = GalleryConfig.GetGalleryConfig(ModuleId)





            If Request.IsAuthenticated Then
                ZuserID = Int16.Parse(Context.User.Identity.Name)
            End If

            GenerateConfig()

            If Not config.IsValidPath Then

                lblInfo.Text = GetLanguage("Gal_NoConfig")

                If mGalleryAuthority Then
                    lblInfo.Text += GetLanguage("Gal_Click")
                    CreateAdminLink()
                End If

                lblInfo.Visible = True
                lblInfo.ID = ""
                Return
            End If

            ' Only bind data, etc. if we've got a good filepath for media

            If config.IsValidPath Then

                ' Reset if needto
                If Not Session("reset") Is Nothing Then
                    If Zrequest.Path = CStr(Session("reset")) Then
                        Dim sb As New StringBuilder()
                        sb.Append(GetRootURL)
                        sb.Append("&path=")
                        sb.Append(Zrequest.Path)
                        sb.Append("&currentstrip=")
                        sb.Append(Zrequest.CurrentStrip.ToString)


                        GalleryConfig.ResetGalleryConfig(ModuleId)
                        Zrequest.Folder.Reset()
                        ClearModuleCache(ModuleId)

                        Zrequest = New GalleryRequest(ModuleId)
                        config = GalleryConfig.GetGalleryConfig(ModuleId)

                        Session("reset") = Nothing
                        lblInfo.Text = "<script language=""javascript"">" & vbCrLf & "<!--" & vbCrLf
                        lblInfo.Text += "window.location.reload()" & vbCrLf
                        lblInfo.Text += "--></script>"
                        lblInfo.Visible = True
                        Return
                        'Response.Redirect(sb.ToString)
                    End If
                End If
                If Not config.RootFolder.IsPopulated Then
                    Zrequest.Folder.LogEvent("Config not populated -> PostBack : " + Page.IsPostBack.ToString + vbCrLf)
                    'Response.Redirect(glbPath & "DesktopModules/TTTGallery/TTT_cache.aspx" & HttpContext.Current.Request.Url.Query & "&mid=" & ModuleId.ToString)
                End If

                If Not Zrequest.Folder.IsPopulated Then
                    Zrequest.Folder.LogEvent("Folder not populated -> PostBack : " + Page.IsPostBack.ToString + vbCrLf)
                    'Response.Redirect(glbPath & "DesktopModules/TTTGallery/TTT_cache.aspx" & HttpContext.Current.Request.Url.Query & "&mid=" & ModuleId.ToString)
                End If

                CreateLink()

                dlStrip.RepeatColumns = config.StripWidth

                If Zrequest.CurrentItems.Count > 0 Then

                    dlPager.DataSource = Zrequest.PagerItems
                    dlPager2.DataSource = Zrequest.PagerItems


                    If Zrequest.CurrentStrip > 1 Then
                        dlPager.SelectedIndex = Zrequest.CurrentStrip
                        dlPager2.SelectedIndex = Zrequest.CurrentStrip
                    Else
                        dlPager.SelectedIndex = 0
                        dlPager2.SelectedIndex = 0
                    End If

                    dlPager.SelectedIndex = Zrequest.SelectedIndex
                    dlPager2.SelectedIndex = Zrequest.SelectedIndex


                    dlPager.DataBind()
                    dlPager2.DataBind()

                    lblPageInfo.Text = GetLanguage("Gal_Page")
                    lblPageInfo2.Text = GetLanguage("Gal_Page")
                    lblPageInfo.ID = ""


                    dlStrip.ItemStyle.Width = New WebControls.Unit(Int(100 / config.StripWidth), UnitType.Percentage)
                    dlStrip.DataSource = Zrequest.CurrentItems
                    dlStrip.DataBind()
                    Stats.Text = GetStats()
                    Stats.ID = ""
                Else
                    lblPageInfo2.Text = GetLanguage("Gal_NoFile")

                End If
                lblPageInfo2.ID = ""
                dlFolders.DataSource = Zrequest.FolderPaths
                dlFolders.DataBind()
                dlFolders.ID = ""

            End If
        End Sub

        ' Generate all config value once for better performance
        Private Sub GenerateConfig()
		Dim delim as String = "/"
            Dim tempRootURL As String = config.RootURL
		temprootURL = TempRootURL.Trim(delim.ToCharArray()) & "/"
		tempRootURL += Zrequest.Path
		temprootURL = TempRootURL.Trim(delim.ToCharArray()) & "/_res"
            ' Authorized gallery
            If ZuserID > 0 Then
            System.Web.HttpContext.Current.Session("UploadPath") = tempRootURL
                If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                   OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                   OrElse (config.OwnerID = ZuserID) Then
                    mGalleryAuthority = True
                    mGalleryEdit = True
                Else
                    mGalleryAuthority = False
                    mGalleryEdit = False
                End If

                If Not config.IsPrivate AndAlso (PortalSecurity.IsInRoles(CType(PortalSettings.GetEditModuleSettings(ModuleId), ModuleSettings).AuthorizedEditRoles.ToString) = True) Then
                    mGalleryEdit = True
                End If

                Try
                    If Zrequest.Folder.OwnerID = ZuserID Then
                        mGalleryEdit = True
                    End If
                Catch ex As Exception

                End Try
            End If

            ' Allow download file?
            mAllowDownload = config.AllowDownload

            ' Integrate with forum?
            mForumIntegrate = config.IsIntegrated

            ' Folder contains any image
            If Zrequest.FirstImageIndex > -1 Then
                bCanView = True
            End If

        End Sub

		
        Protected Function GalleryAuthority() As Boolean
            Return mGalleryAuthority
        End Function

        Protected Function GalleryEdit() As Boolean
            Return mGalleryEdit
        End Function

        'Modification par René Boulard 2004-05-08

        Protected Function ViewStateAllow() As Boolean
            If mGalleryAuthority Or config.AllowDownload Or mGalleryEdit Then
                Return True
            Else
                Return False
            End If
        End Function


        Protected Function ItemAuthority(ByVal DataItem As Object) As Boolean

            If GalleryEdit() _
            OrElse (CType(DataItem, IGalleryObjectInfo).OwnerID = ZuserID AndAlso ZuserID > 0) Then
                Return True
            Else
                Return False
            End If

        End Function


        Protected Function CanGoLeft(ByVal DataItem As Object) As Boolean

            If CType(DataItem, IGalleryObjectInfo).Index > 0 And GalleryEdit() Then

                If CType(DataItem, IGalleryObjectInfo).IsFolder <> CType(Zrequest.Folder.List.Item(CType(DataItem, IGalleryObjectInfo).Index - 1), IGalleryObjectInfo).IsFolder Then
                    Return False
                Else
                    Return True
                End If


            Else
                Return False
            End If

        End Function

        Protected Function CanGoRight(ByVal DataItem As Object) As Boolean

            If CType(DataItem, IGalleryObjectInfo).Index + 1 < Zrequest.Folder.List.Count And GalleryEdit() Then
                If CType(DataItem, IGalleryObjectInfo).IsFolder <> CType(Zrequest.Folder.List.Item(CType(DataItem, IGalleryObjectInfo).Index + 1), IGalleryObjectInfo).IsFolder Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If

        End Function


        ' modifier par rene boulard pour modifier Icon

        Protected Function ItemIconAuthority(ByVal DataItem As Object) As Boolean

            If GalleryAuthority() _
            OrElse (CType(DataItem, IGalleryObjectInfo).OwnerID = ZuserID AndAlso ZuserID > 0) Then

                If (CType(DataItem, IGalleryObjectInfo).IsFolder) _
                OrElse (CType(DataItem, IGalleryObjectInfo).Type = IGalleryObjectInfo.ItemType.Flash) _
                OrElse (CType(DataItem, IGalleryObjectInfo).Type = IGalleryObjectInfo.ItemType.Movie) Then
                    Return True
                Else
                    Return False
                End If

            Else
                Return False
            End If

        End Function



        Private Sub CreateAdminLink()


            lnkAdmin.NavigateUrl = GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryAdmin & "&mid=" & ModuleId & "&tabid=" & TabId

            lnkAdmin.Visible = True


            lnkAdmin.ID = ""
        End Sub

        Private Sub CreateManagerLink()


            lnkManager.NavigateUrl = GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditAlbum & "&mid=" & ModuleId & "&tabid=" & TabId & "&path=" & Zrequest.Folder.GalleryHierarchy

            lnkManager.Visible = True

            lnkManager.ID = ""

            ' Turn admin button on/off
            ClearCache.Visible = True
            SubAlbum.Visible = True
            UploadImage.Visible = True


        End Sub

        Private Sub CreateLink()

            If mGalleryAuthority Then
                CreateAdminLink()
            End If

            If mGalleryAuthority OrElse mGalleryEdit Then
                CreateManagerLink()
            End If

            'Try ' Error handling suggested by Katse to prevent exception after deleting the last image in album
            If Zrequest.Folder.IsBrowsable() AndAlso Zrequest.Folder.BrowsableItems.Count > 0 Then


                BrowserLink.NavigateUrl = GetFolderViewerURL(Zrequest.Folder)

                BrowserLink.Visible = True


                BrowserLink.ID = ""


                SlideshowLink.NavigateUrl = GetSlideshowURL(Zrequest.Folder)

                SlideshowLink.Visible = True

                SlideshowLink.ID = ""

            End If

        End Sub

        Protected Function CanDownload(ByVal DataItem As Object) As Boolean
            If Not CType(DataItem, IGalleryObjectInfo).IsFolder _
            AndAlso config.AllowDownload Then
                Return True
            Else
                Return False
            End If
        End Function

        Protected Function CanDiscuss() As Boolean
            Return bCanDiscuss
        End Function

        Protected Function CanView() As Boolean 'for container folder
            Return bCanView
        End Function

        Protected Function CanView(ByVal DataItem As Object) As Boolean 'for item
            If CType(DataItem, IGalleryObjectInfo).IsFolder Then
                If CType(DataItem, GalleryFolder).BrowsableItems.Count > 0 Then
                    Return True
                End If
            End If

        End Function

        Protected Function CanClearCache(ByVal dataItem As Object) As Boolean
            Return CType(dataItem, IGalleryObjectInfo).IsFolder AndAlso mGalleryAuthority

        End Function

        Protected Function GetItemInfo(ByVal DataItem As Object) As String
            Dim Item As IGalleryObjectInfo = CType(DataItem, IGalleryObjectInfo)
            Dim sb As New StringBuilder()


            If Item.IsFolder Then
                Dim GalleryItem As GalleryFolder = CType(DataItem, GalleryFolder)
                'If Not GalleryItem.IsPopulated Then
                'GalleryItem.Populate()
                'End If
                If GalleryItem.Size > 0 Then
                    Return "(" & GalleryItem.Size.ToString & " " & GetLanguage("Gal_Items") & ")"
                End If
            ElseIf Not Item.IsFolder Then

                If config.DisplayOption = GalleryConfig.GalleryDisplayOption.Description.ToString _
                OrElse config.DisplayOption = GalleryConfig.GalleryDisplayOption.FileInfoAndDescription.ToString Then
                    sb.Append(Replace(Item.Description, vbCrLf, "<br>"))
                End If

                If sb.Length > 0 Then
                    sb.Append("<br>")
                End If

                If config.DisplayOption = GalleryConfig.GalleryDisplayOption.FileInfo.ToString _
                OrElse config.DisplayOption = GalleryConfig.GalleryDisplayOption.FileInfoAndDescription.ToString Then
                    sb.Append(Item.Name)
                    sb.Append(" ")
                    sb.Append(Math.Ceiling(Item.Size / 1024).ToString)
                    sb.Append(" ")
                    sb.Append(GetLanguage("Gal_KB"))
                End If

                Return sb.ToString

            End If

        End Function

        Protected Function GetImageToolTip(ByVal DataItem As Object) As String

            If config.InfoBule Then
                If CType(DataItem, IGalleryObjectInfo).IsFolder Then
                    Dim item As GalleryFolder = CType(DataItem, GalleryFolder)
                    Return ReturnToolTip(item.Description)
                Else
                    Dim item As GalleryFile = CType(DataItem, GalleryFile)
                    Dim TempString As String = "<table width='100%' cellspacing='1' cellpadding='0'>"
                    TempString = TempString & "<tr><td align='center'>"

                    Select Case item.Type
                        Case IGalleryObjectInfo.ItemType.Image
                            ' Return ReturnGalleryToolTip(item.url, item.width, item.height)
                            TempString += GetLanguage("F_Image") + " " & item.Width.ToString & "px X " & item.Height & "px"
                        Case IGalleryObjectInfo.ItemType.Flash
                            TempString += GetLanguage("Gal_FFlash")
                        Case IGalleryObjectInfo.ItemType.Movie
                            TempString += GetLanguage("Gal_FFilm")
                        Case Else
                            Return ""
                    End Select

                    TempString += " " + Math.Ceiling(item.Size / 1024).ToString() + " " + GetLanguage("Gal_KB") + "</td></tr>"

                    If item.Description <> "" Then
                        TempString += "<tr><td align='left'>" & item.Description & "</td></tr>"
                    End If
                    TempString += "</table>"
                    Return ReturnToolTip(TempString, "200", "true")

                End If
            Else
                Return ""
            End If
        End Function

        Protected Function GetThumbnailURL(ByVal DataItem As Object) As String
            Dim Item As IGalleryObjectInfo = CType(DataItem, IGalleryObjectInfo)
            Return Item.Thumbnail
            ' Return CryptoUrl(Item.Thumbnail, config.IsPrivate)
        End Function

        Protected Function GetBrowserURL(ByVal DataItem As Object) As String
            If CType(DataItem, IGalleryObjectInfo).IsFolder Then
                Return GetAlbumURL(DataItem)
            Else
                Dim item As GalleryFile = CType(DataItem, GalleryFile)
                Select Case item.Type
                    Case IGalleryObjectInfo.ItemType.Image
                        Return GetItemURL(DataItem)
                    Case IGalleryObjectInfo.ItemType.Flash
                        Return Me.GetFlashPlayerURL(DataItem)
                    Case IGalleryObjectInfo.ItemType.Movie
                        Return GetMediaPlayerURL(DataItem)
                End Select
            End If
        End Function

        Private Function GetRootURL() As String
            Return GetFullDocument() & "?" & "tabid=" & _portalSettings.ActiveTab.TabId

        End Function

        Public Function GetFolderURL(ByVal DataItem As Object) As String
            ' DataItem is a folderdetail object
            Dim sb As New StringBuilder()
            sb.Append(GetRootURL())
            sb.Append("&path=")
            sb.Append(CType(DataItem, FolderDetail).URL)

            Return sb.ToString

        End Function

        Public Function GetPagerURL(ByVal DataItem As Object) As String
            Dim sb As New StringBuilder()

            sb.Append(GetRootURL)
            sb.Append("&path=")
            sb.Append(Zrequest.Path)
            sb.Append("&currentstrip=")
            sb.Append(CType(DataItem, PagerDetail).Strip.ToString)

            Return sb.ToString

        End Function

        Protected Function GetAdminURL() As String
            Dim sb As New StringBuilder()
            sb.Append(GetFullDocument() & "?edit=control&editpage=")
            sb.Append(TTT_EditGallery.GalleryEditType.GalleryAdmin)
            sb.Append("&mid=")
            sb.Append(ModuleId.ToString)
            sb.Append("&tabid=")
            sb.Append(TabId.ToString)

            Return sb.ToString

        End Function

        Protected Function GetFolderViewerURL(ByVal DataItem As Object) As String
            Return GetURL(TTT_GalleryDispatch.GalleryDesktopType.GalleryBrowser, Zrequest.Folder.GalleryHierarchy, False, "0", "")
            ' Return GetURL(TTT_GalleryDispatch.GalleryDesktopType.GalleryBrowser, Zrequest.Folder.GalleryHierarchy, False, "0", "")
        End Function

        Protected Function GetAlbumURL(ByVal DataItem As Object) As String
            Dim sb As New StringBuilder()
            sb.Append(GetRootURL)
            sb.Append("&path=")
            sb.Append(CType(DataItem, IGalleryObjectInfo).URL)
            Return sb.ToString
        End Function

        Protected Function GetItemURL(ByVal DataItem As Object) As String
            ' modification 2004-07-31 pour ordonner la liste des images
            Dim viewIndex As Integer = Zrequest.Folder.BrowsableItems.IndexOf(CType(DataItem, IGalleryObjectInfo).Index)

            Return GetURL(TTT_GalleryDispatch.GalleryDesktopType.GalleryBrowser, Zrequest.Folder.GalleryHierarchy, False, viewIndex.ToString, "")
            ' Return GetURL(TTT_GalleryDispatch.GalleryDesktopType.GalleryBrowser, Zrequest.Folder.GalleryHierarchy, False, viewIndex.ToString, "")
        End Function

        ' ajout par rene boulard 2004-04-23 pour modifier icon dans le res repertoire

        Protected Function GetEditIconURL(ByVal DataItem As Object) As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim sb As New StringBuilder()
            sb.Append(glbPath & "DesktopModules/TTTGallery/magicfile.aspx")
            sb.Append("?name=")
            sb.Append(IO.Path.GetFileNameWithoutExtension(CType(DataItem, IGalleryObjectInfo).Name))
            sb.Append("&tabid=" & CStr(_portalSettings.ActiveTab.TabId))
            sb.Append("&path=")
            sb.Append(Zrequest.Path)
            sb.Append("&mid=")
            sb.Append(ModuleId)

            sb.Append("&L=" & GetLanguage("N"))
            Return sb.ToString
        End Function




        Protected Function GetEditURL(ByVal DataItem As Object) As String

            Dim sb As New StringBuilder()
            If CType(DataItem, IGalleryObjectInfo).IsFolder Then
                sb.Append(GetFullDocument() & "?edit=control&editpage=")
                sb.Append(TTT_EditGallery.GalleryEditType.GalleryEditAlbum)
                sb.Append("&mid=")
                sb.Append(ModuleId.ToString)
                sb.Append("&tabid=")
                sb.Append(TabId.ToString)
                sb.Append("&path=")
                sb.Append(CType(DataItem, IGalleryObjectInfo).URL)
            Else
                sb.Append(GetFullDocument() & "?edit=control&editpage=")
                sb.Append(TTT_EditGallery.GalleryEditType.GalleryEditFile)
                sb.Append("&tabid=")
                sb.Append(TabId.ToString)
                sb.Append("&mid=")
                sb.Append(ModuleId.ToString)
                sb.Append("&path=")
                sb.Append(CType(Zrequest.Folder, IGalleryObjectInfo).URL)
                sb.Append("&editindex=")
                sb.Append(CType(DataItem, IGalleryObjectInfo).Index.ToString)
            End If
            Return sb.ToString
        End Function

        Protected Function GetMediaPlayerURL(ByVal DataItem As Object) As String
            Return GetURL(TTT_GalleryDispatch.GalleryDesktopType.GalleryMediaPlayer, CType(DataItem, GalleryFile).URL, True, "", CType(DataItem, GalleryFile).Name)
        End Function

        Protected Function GetFlashPlayerURL(ByVal DataItem As Object) As String
            Return GetURL(TTT_GalleryDispatch.GalleryDesktopType.GalleryFlashPlayer, CType(DataItem, GalleryFile).URL, True, "", CType(DataItem, GalleryFile).Name)
        End Function

        Protected Function GetSlideshowURL(ByVal DataItem As Object) As String
            Return GetURL(TTT_GalleryDispatch.GalleryDesktopType.GallerySlideshow, CType(DataItem, IGalleryObjectInfo).URL, False, "", "")
        End Function

        Private Function GetURL( _
            ByVal GalleryPage As TTT_GalleryDispatch.GalleryDesktopType, _
            ByVal Path As String, _
            ByVal IncludeHost As Boolean, _
            Optional ByVal CurrentItem As String = "", _
            Optional ByVal Media As String = "") As String

            Dim sb As New StringBuilder()

            If IncludeHost Then
                ' Path = CryptoUrl(Path, config.IsPrivate)
            End If


            If config.SlideshowPopup Then
                sb.Append("javascript:DestroyWnd;CreateWnd('" & glbPath & "DesktopModules/TTTGallery/")
                Select Case GalleryPage
                    Case TTT_GalleryDispatch.GalleryDesktopType.GalleryBrowser
                        sb.Append("TTT_Viewer.aspx?L=" & GetLanguage("N") & "&path=")
                    Case TTT_GalleryDispatch.GalleryDesktopType.GalleryFlashPlayer
                        sb.Append("TTT_FlashPlayer.aspx?L=" & GetLanguage("N") & "&path=")
                    Case TTT_GalleryDispatch.GalleryDesktopType.GalleryMediaPlayer
                        sb.Append("TTT_MediaPlayer.aspx?L=" & GetLanguage("N") & "&path=")
                    Case TTT_GalleryDispatch.GalleryDesktopType.GallerySlideshow
                        sb.Append("TTT_SlideShow.aspx?L=" & GetLanguage("N") & "&path=")
                End Select
                sb.Append(Path)
                If Len(CurrentItem) > 0 Then
                    sb.Append("&currentitem=")
                    sb.Append(CurrentItem)
                End If
                If Len(Media) > 0 Then
                    sb.Append("&media=")
                    sb.Append(Media)
                End If
                sb.Append("&tabid=")
                sb.Append(TabId)
                sb.Append("&mid=")
                sb.Append(ModuleId.ToString)
                sb.Append("', ")
                sb.Append(CStr(config.FixedWidth + 20))
                sb.Append(", ")
                sb.Append(CStr(config.FixedHeight + 125))
                sb.Append(", true);")

            Else
                sb.Append(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "gallerypage=" & GalleryPage & "&mid=" & ModuleId))
                sb.Append("&path=")
                If IncludeHost Then
                    sb.Append(AddHTTP(Request.ServerVariables("HTTP_HOST")))
                End If
                sb.Append(Path)
                If Len(CurrentItem) > 0 Then
                    sb.Append("&currentitem=")
                    sb.Append(CurrentItem)
                End If
                If Len(Media) > 0 Then
                    sb.Append("&media=")
                    sb.Append(Media)
                End If
            End If

            Return sb.ToString

        End Function

        Public Function SetImage(ByVal ImageData As String) As String
            Return "<img height=""16"" width=""16"" src=""/images/1x1.gif"" Alt=""*"" style=""border-width:0px; background: url('" & ForumConfig.DefaultImageFolder & "forum.gif') no-repeat; background-position:" & ImageData & ";"">"
        End Function

        Public Function GetImageStyle(ByVal ImageData As String) As String
            Return "border-width:0px; background: url('" & ForumConfig.DefaultImageFolder & "forum.gif') no-repeat; background-position: " & ImageData & ";"
        End Function


        Public Function GetForumURL(ByVal DataItem As Object) As String

            bCanDiscuss = False
            Dim url As String = ""

            If config.IsIntegrated AndAlso CType(DataItem, IGalleryObjectInfo).IsFolder Then
                Dim albumName As String = CType(DataItem, GalleryFolder).Name
                Dim forumID As Integer = CType(DataItem, GalleryFolder).IntegratedForumID
                Dim forumTabID As Integer = CType(DataItem, GalleryFolder).IntegratedForumTabID

                If forumID > 0 AndAlso forumTabID > 0 Then
                    url = "~/" & GetLanguage("N") & ".default.aspx" & "?" & "tabid=" & forumTabID.ToString & "&forumid=" & forumID & "&scope=thread"
                    bCanDiscuss = True
                End If
                Return url
            End If

        End Function


        Public Function GetStats() As String
            ' Présentation {start} - {end} de {total} item(s) total
            Dim TempString As String
            TempString = Replace(GetLanguage("Gal_InfoPre"), "{start}", Zrequest.StartItem.ToString)
            TempString = Replace(TempString, "{end}", Zrequest.EndItem.ToString)
            TempString = Replace(TempString, "{total}", Zrequest.Folder.List.Count.ToString)
            Return TempString

        End Function



        Private Sub SubAlbum_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SubAlbum.Click
            Session("UrlReferrer") = Request.RawUrl
            Response.Redirect(GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditAlbum & "&mid=" & ModuleId & "&tabid=" & TabId & "&path=" & Zrequest.Folder.GalleryHierarchy & "&action=addfolder")
        End Sub

        Private Sub UploadImage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles UploadImage.Click
            Session("UrlReferrer") = Request.RawUrl
            Response.Redirect(GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditAlbum & "&mid=" & ModuleId & "&tabid=" & TabId & "&path=" & Zrequest.Folder.GalleryHierarchy & "&action=addfile")
        End Sub

        Private Sub ClearCache_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ClearCache.Click
            'Zrequest.Folder.Clear()

            Zrequest.Folder.REPopulate(config.CheckboxGPS)
            GalleryConfig.ResetGalleryConfig(ModuleId)
            Zrequest.Folder.Reset()
            ClearModuleCache(ModuleId)

            UpdateFolderSize(config)
            Response.Redirect(Request.UrlReferrer.ToString)
        End Sub



        Private Sub dlStrip_Commands(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlStrip.ItemCommand

            ' Determine the command of the button (either "left" or "right")
            Dim command As String = CType(e.CommandSource, ImageButton).CommandName
            Dim itemIndex As Integer = e.Item.ItemIndex
            Select Case command
                Case "right", "left"

                    Dim TempSort As String
                    Dim TempIndex As Integer
                    itemIndex = config.ItemsPerStrip * (Zrequest.CurrentStrip - 1) + e.Item.ItemIndex
                    Dim Item As IGalleryObjectInfo = CType(Zrequest.Folder.List.Item(itemIndex), IGalleryObjectInfo)
                    Dim SaveItem As IGalleryObjectInfo
                    Dim MoveTo As IGalleryObjectInfo

                    If command = "right" Then
                        MoveTo = CType(Zrequest.Folder.List.Item(itemIndex + 1), IGalleryObjectInfo)
                        TempSort = (itemIndex + 1).ToString("d8")
                        TempIndex = itemIndex + 1
                    Else
                        MoveTo = CType(Zrequest.Folder.List.Item(itemIndex - 1), IGalleryObjectInfo)
                        TempSort = (itemIndex - 1).ToString("d8")
                        TempIndex = itemIndex - 1
                    End If


                    If Item.Sort <> MoveTo.Sort Then
                        SaveItem = MoveTo
                        TempSort = MoveTo.Sort
                        SaveItem.Sort = Item.Sort
                        GalleryXML.SaveGalleryData(Zrequest.Folder.Path, SaveItem)
                        'GalleryXML.SaveMetaData(Item.Sort, Zrequest.Folder.Path, MoveTo.Name, MoveTo.Title, MoveTo.Description, MoveTo.Categories, MoveTo.OwnerID, MoveTo.Width, MoveTo.Height, metaData.latitude(MoveTo.Name), metaData.Longitude(MoveTo.Name), metaData.gpsicon(MoveTo.Name), metaData.gpsiconsize(MoveTo.Name), metaData.Link(MoveTo.Name))
                        Item.Sort = TempSort
                        GalleryXML.SaveGalleryData(Zrequest.Folder.Path, Item)
                        'GalleryXML.SaveMetaData(MoveTo.Sort, Zrequest.Folder.Path, Item.Name, Item.Title, Item.Description, Item.Categories, Item.OwnerID, Item.Width, Item.Height, metaData.latitude(Item.Name), metaData.Longitude(Item.Name), metaData.gpsicon(Item.Name), metaData.gpsiconsize(Item.Name), metaData.Link(Item.Name))
                    Else
                        ' sort is same
                        SaveItem = MoveTo
                        SaveItem.Sort = itemIndex.ToString("d8")
                        GalleryXML.SaveGalleryData(Zrequest.Folder.Path, SaveItem)

                        'GalleryXML.SaveMetaData(itemIndex.ToString("d8"), Zrequest.Folder.Path, MoveTo.Name, MoveTo.Title, MoveTo.Description, MoveTo.Categories, MoveTo.OwnerID, MoveTo.Width, MoveTo.Height, metaData.latitude(MoveTo.Name), metaData.Longitude(MoveTo.Name), metaData.gpsicon(MoveTo.Name), metaData.gpsiconsize(MoveTo.Name), metaData.Link(MoveTo.Name))
                        Item.Sort = TempSort
                        GalleryXML.SaveGalleryData(Zrequest.Folder.Path, Item)

                        'GalleryXML.SaveMetaData(TempSort, Zrequest.Folder.Path, Item.Name, Item.Title, Item.Description, Item.Categories, Item.OwnerID, Item.Width, Item.Height, metaData.latitude(Item.Name), metaData.Longitude(Item.Name), metaData.gpsicon(Item.Name), metaData.gpsiconsize(Item.Name), metaData.Link(Item.Name))
                    End If


                    Dim sb As New StringBuilder()
                    sb.Append(GetRootURL)
                    sb.Append("&path=")
                    sb.Append(Zrequest.Path)
                    sb.Append("&currentstrip=")
                    sb.Append(System.Math.Ceiling((TempIndex + 1) / config.ItemsPerStrip).ToString)


                    GalleryConfig.ResetGalleryConfig(ModuleId)
                    Zrequest.Folder.Reset()
                    ClearModuleCache(ModuleId)
                    Response.Redirect(sb.ToString)
                Case "edit"
                    DownloadFile(itemIndex)
            End Select

        End Sub

        Private Sub dlStrip_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlStrip.DeleteCommand
            Dim ItemIndex As Integer
            ItemIndex = config.ItemsPerStrip * (Zrequest.CurrentStrip - 1) + e.Item.ItemIndex
            Dim Item As IGalleryObjectInfo = CType(Zrequest.Folder.List.Item(ItemIndex), IGalleryObjectInfo)
            UpdateFolderSize(config, Zrequest.Folder.DeleteChild(Item))

            GalleryConfig.ResetGalleryConfig(ModuleId)
            Zrequest.Folder.Reset()
            ClearModuleCache(ModuleId)
            Response.Redirect(Request.UrlReferrer.ToString)
        End Sub


        Private Sub dlStrip_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlStrip.ItemCreated
            Dim TmpControl As Control

            TmpControl = Nothing
            TmpControl = e.Item.FindControl("Left")
            If Not TmpControl Is Nothing Then
                CType(TmpControl, ImageButton).ToolTip = GetLanguage("gauche")
            End If
            TmpControl = Nothing
            TmpControl = e.Item.FindControl("Right")
            If Not TmpControl Is Nothing Then
                CType(TmpControl, ImageButton).ToolTip = GetLanguage("droite")
            End If
            TmpControl = Nothing
            TmpControl = e.Item.FindControl("delete")
            If Not TmpControl Is Nothing Then
                CType(TmpControl, ImageButton).ToolTip = GetLanguage("delete")
                CType(TmpControl, ImageButton).Attributes.Add("onClick", "javascript: return confirm('" & RTESafe(GetLanguage("request_confirm")) & "')")
            End If
            TmpControl = Nothing
            TmpControl = e.Item.FindControl("btnDownload")
            If Not TmpControl Is Nothing Then
                CType(TmpControl, ImageButton).ToolTip = GetLanguage("download")
            End If
            TmpControl = Nothing
            TmpControl = e.Item.FindControl("lnkSlideshow")
            If Not TmpControl Is Nothing Then
                CType(TmpControl, HyperLink).ToolTip = GetLanguage("Gal_lnkshow")
            End If
            TmpControl = Nothing
            TmpControl = e.Item.FindControl("lnkDiscussion")
            If Not TmpControl Is Nothing Then
                CType(TmpControl, HyperLink).ToolTip = GetLanguage("Gal_Dis")
            End If
            TmpControl = Nothing
            TmpControl = e.Item.FindControl("lnkEditRes")
            If Not TmpControl Is Nothing Then
                CType(TmpControl, HyperLink).ToolTip = GetLanguage("Gal_EditRes")
            End If
            TmpControl = Nothing
            TmpControl = e.Item.FindControl("lnkEdit")
            If Not TmpControl Is Nothing Then
                CType(TmpControl, HyperLink).ToolTip = GetLanguage("Gal_Edit")
            End If

        End Sub

        Private Sub DownloadFile(ByVal ItemIndex As Integer)


            Dim file As GalleryFile = CType(Zrequest.CurrentItems.Item(ItemIndex), GalleryFile)
            Dim fileName As String = System.Web.HttpUtility.UrlEncode(file.Name)
            Dim filePath As String = file.Path

            Dim UserId As Integer = -1
            Dim URLReferrer As String = ""
            If Not Request.UrlReferrer Is Nothing Then
                URLReferrer = Request.UrlReferrer.ToString()
            End If

            Response.AppendHeader("content-disposition", "attachment; filename=" & fileName)
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(filePath)
            Response.End()

        End Sub




    End Class
End Namespace