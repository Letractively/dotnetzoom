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
        Private Zconfig As GalleryConfig
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
   			Title1.EditText = getlanguage("modifier")
			ClearCache.ToolTip = getlanguage("Gal_Clear")
            SubAlbum.ToolTip = GetLanguage("Gal_AddFolderTip")
            UploadImage.ToolTip = GetLanguage("Gal_AddFileTip")
			lnkAdmin.Text =  getlanguage("Gal_SetUp")

			lnkManager.Text =  getlanguage("Gal_SetUp1")

			BrowserLink.Text =  getlanguage("Gal_BrowserLink")

			SlideshowLink.Text =  getlanguage("Gal_showLink")

			
			Dim objCSS As Control = page.FindControl("CSS")
			Dim objTTTCSS As Control = page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If (Not objCSS Is Nothing) and (objTTTCSS Is Nothing) Then
                    ' put in the ttt.css
					objLink = New System.Web.UI.LiteralControl("TTTCSS")
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                    objCSS.Controls.Add(objLink)
            End If

			If Not Request.Params("reset") Is Nothing Then
			GalleryConfig.ResetGalleryConfig(ModuleID)
			end if

			
			System.Web.HttpContext.Current.Session("ReturnPath") = Request.RawUrl
            Zrequest = New GalleryRequest(ModuleId)
            Zconfig = Zrequest.GalleryConfig



			
            If Request.IsAuthenticated Then
                ZuserID = Int16.Parse(Context.User.Identity.Name)
            End If

            GenerateConfig()

            If Not Zconfig.IsValidPath Then

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
			
			if Zconfig.IsValidPath Then
                If Not Zrequest.Folder.IsPopulated Then
                    Response.Redirect(glbPath & "DesktopModules/TTTGallery/TTT_cache.aspx" & HttpContext.Current.Request.Url.Query & "&mid=" & ModuleId.ToString) '& "&tabid=" & _portalSettings.ActiveTab.TabId)
                End If

                CreateLink()

                dlStrip.RepeatColumns = Zconfig.StripWidth

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

                    dlPager.DataBind()
                    dlPager2.DataBind()

                    lblPageInfo.Text = GetLanguage("Gal_Page")
                    lblPageInfo2.Text = GetLanguage("Gal_Page")
                    lblPageInfo.ID = ""
                    

                    dlStrip.ItemStyle.Width = New WebControls.Unit(Int(100 / Zconfig.StripWidth), UnitType.Percentage)
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
				dlFolders.id = ""

                ' Turn admin button on/off
                ClearCache.Visible = GalleryAuthority()
                SubAlbum.Visible = ClearCache.Visible
                UploadImage.Visible = ClearCache.Visible

           End If
        End Sub

        ' Generate all config value once for better performance
        Private Sub GenerateConfig()
		Dim delim as String = "/"
        Dim tempRootURL as string = Zconfig.RootURL
		temprootURL = TempRootURL.Trim(delim.ToCharArray()) & "/"
		tempRootURL += Zrequest.Path
		temprootURL = TempRootURL.Trim(delim.ToCharArray()) & "/_res"
            ' Authorized gallery
            If ZuserID > 0 Then
            System.Web.HttpContext.Current.Session("UploadPath") = tempRootURL
                If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                   OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                   OrElse (Zconfig.OwnerID = ZuserID) Then
                    mGalleryAuthority = True
                    mGalleryEdit = True
                Else
                    mGalleryAuthority = False
                    mGalleryEdit = False
                End If
            End If

            If Not Zconfig.IsPrivate AndAlso (PortalSecurity.IsInRoles(CType(portalSettings.GetEditModuleSettings(ModuleId), ModuleSettings).AuthorizedEditRoles.ToString) = True) Then
                mGalleryEdit = True

            End If

            ' Allow download file?
            mAllowDownload = Zconfig.AllowDownload

            ' Integrate with forum?
            mForumIntegrate = Zconfig.IsIntegrated

            ' Folder contains any image
            If Zrequest.FirstImageIndex > -1 Then
                bCanView = True
            End If

        End Sub

		
        Protected Function GalleryAuthority() As Boolean
            Return mGalleryAuthority
        End Function

		'Modification par René Boulard 2004-05-08
		
		Protected Function ViewStateAllow() As Boolean
            If mGalleryAuthority or Zconfig.AllowDownload Then
                Return True
            Else
                Return False
            End If
        End Function
		
		
        Protected Function ItemAuthority(ByVal DataItem As Object) As Boolean

            If GalleryAuthority() _
            OrElse (CType(DataItem, IGalleryObjectInfo).OwnerID = ZuserID AndAlso ZuserID > 0) Then
                Return True
            Else
                Return False
            End If

        End Function

		
        ' modifier par rene boulard pour modifier Icon
 
        Protected Function ItemIconAuthority(ByVal DataItem As Object) As Boolean

            If GalleryAuthority() _
            OrElse (CType(DataItem, IGalleryObjectInfo).OwnerID = ZuserID AndAlso ZuserID > 0) then
			
			If (CType(DataItem, IGalleryObjectInfo).IsFolder ) _
			OrElse (CType(DataItem, IGalleryObjectInfo).Type = IGalleryObjectInfo.ItemType.Flash) _
			OrElse (CType(DataItem, IGalleryObjectInfo).Type = IGalleryObjectInfo.ItemType.Movie) Then
			    Return True
				else
				Return False
				end If	

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
            AndAlso Zconfig.AllowDownload Then
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

            If Item.IsFolder AndAlso Item.Size > 0 Then
                Return "(" & Item.Size.ToString & " " & GetLanguage("Gal_Items") & ")"
            ElseIf Not Item.IsFolder Then

                If Zconfig.DisplayOption = Zconfig.GalleryDisplayOption.Description.ToString _
                OrElse Zconfig.DisplayOption = Zconfig.GalleryDisplayOption.FileInfoAndDescription.ToString Then
                    sb.Append(Replace(Item.Description, vbCrLf, "<br>"))
                End If

                If sb.Length > 0 Then
                    sb.Append("<br>")
                End If

                If Zconfig.DisplayOption = Zconfig.GalleryDisplayOption.FileInfo.ToString _
                OrElse Zconfig.DisplayOption = Zconfig.GalleryDisplayOption.FileInfoAndDescription.ToString Then
                    sb.Append(Item.Name)
                    sb.Append(" ")
                    sb.Append(Math.Ceiling(Item.Size / 1024).ToString)
                    sb.Append(" ")
                    Sb.Append(GetLanguage("Gal_KB"))
                End If

                Return sb.ToString

            End If

        End Function

        Protected Function GetImageToolTip(ByVal DataItem As Object) As String
            Dim config As GalleryConfig = GalleryConfig.GetGalleryConfig(ModuleId)
            If config.InfoBule Then
                If CType(DataItem, IGalleryObjectInfo).IsFolder Then
                    Return ReturnToolTip(CType(DataItem, IGalleryObjectInfo).URL)
                Else
                    Dim item As GalleryFile = CType(DataItem, GalleryFile)
                    Select Case item.Type
                        Case IGalleryObjectInfo.ItemType.Image
                            ' Return ReturnGalleryToolTip(item.url, item.width, item.height)
                            Dim TempString As String = ""
                            TempString += GetLanguage("Gal_Name") & " : " & item.Name
                            If item.Name <> item.Title Then
                                TempString += "<br>" & GetLanguage("Gal_TitleI") & " : " & item.Title
                            End If
                            If item.Description <> "" Then
                                TempString += "<br>" & GetLanguage("Gal_Desc") & " : " & item.Description
                            End If
                            Return ReturnToolTip(TempString & "<br>" & GetLanguage("Gal_Dim") & " : " & item.Width.ToString & "px X " & item.Height & "px", "200", "true")
                        Case IGalleryObjectInfo.ItemType.Flash
                            Return ReturnToolTip(GetLanguage("Gal_FFlash") & " : " & item.Name & "<br>" & item.Description)
                        Case IGalleryObjectInfo.ItemType.Movie
                            Return ReturnToolTip(GetLanguage("Gal_FFilm") & " : " & item.Name & "<br>" & item.Description)
                        Case Else
                            Return ""
                    End Select

                End If
            Else
                Return ""
            End If
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
            Return GetFullDocument() & "?" & "&tabid=" & _portalSettings.ActiveTab.TabId

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
            Dim viewIndex As Integer = Zrequest.Folder.BrowsableItems.indexOf(CType(DataItem, IGalleryObjectInfo).Index)

            Return GetURL(TTT_GalleryDispatch.GalleryDesktopType.GalleryBrowser, Zrequest.Folder.GalleryHierarchy, False, viewIndex.ToString, "")
        End Function

        ' ajout par rene boulard 2004-04-23 pour modifier icon dans le res repertoire

        Protected Function GetEditIconURL(ByVal DataItem As Object) As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim sb As New StringBuilder()
            sb.Append(glbPath & "DesktopModules/TTTGallery/magicfile.aspx")
            sb.Append("?name=")
            sb.Append(IO.Path.GetFileNameWithoutExtension(CType(DataItem, IGalleryObjectInfo).Name))
            sb.Append("&tabid=" & CStr(_portalSettings.ActiveTab.TabID))
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


            If Zconfig.SlideshowPopup Then
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
                    sb.Append("Media")
                End If
                sb.Append("&tabid=")
                sb.Append(TabId)
                sb.Append("&mid=")
                sb.Append(ModuleId.ToString)
                sb.Append("', ")
                sb.Append(CStr(Zconfig.FixedWidth + 50))
                sb.Append(", ")
                sb.Append(CStr(Zconfig.FixedHeight + 50))
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

        Public Function GetForumURL(ByVal DataItem As Object) As String

            bCanDiscuss = False
            Dim url As String = ""

            If Zconfig.IsIntegrated AndAlso CType(DataItem, IGalleryObjectInfo).IsFolder Then
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
            Response.Redirect(GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditAlbum & "&mid=" & ModuleId & "&tabid=" & TabId & "&path=" & Zrequest.Folder.GalleryHierarchy & "&action=addfolder")
        End Sub

        Private Sub UploadImage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles UploadImage.Click
            Response.Redirect(GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryEditAlbum & "&mid=" & ModuleId & "&tabid=" & TabId & "&path=" & Zrequest.Folder.GalleryHierarchy & "&action=addfile")
        End Sub

        Private Sub ClearCache_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ClearCache.Click
            'Zrequest.Folder.Clear()
            Zrequest.Folder.REPopulate()
            GalleryConfig.ResetGalleryConfig(ModuleId)
            ' Reset the File Quota
            Dim StrFolder As String
            Dim SpaceUsed As Double
            Dim objAdmin As New AdminDB()
            StrFolder = Request.MapPath(Zconfig.RootURL)
            SpaceUsed = objAdmin.GetFolderSizeRecursive(StrFolder)
            objAdmin.AddDirectory(StrFolder, SpaceUsed.ToString())
            Response.Redirect(Request.UrlReferrer.ToString)
        End Sub

        Private Sub dlStrip_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlStrip.EditCommand

            Dim itemIndex As Integer = e.Item.ItemIndex
            DownloadFile(itemIndex)

        End Sub

        Private Sub dlStrip_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlStrip.DeleteCommand
            Dim ItemIndex As Integer
            ItemIndex = Zconfig.ItemsPerStrip * (Zrequest.CurrentStrip - 1) + e.Item.ItemIndex

            Dim Item As IGalleryObjectInfo = CType(Zrequest.Folder.List.Item(ItemIndex), IGalleryObjectInfo)

            Try
                If Item.IsFolder Then
                    Dim _galleryFolder As GalleryFolder = CType(Zrequest.Folder.List.Item(ItemIndex), GalleryFolder)
                    Dim folderPath As String = _galleryFolder.Path
                    System.IO.Directory.Delete(folderPath, True)


                    ' ne pas oublier d'effacer tous les fichiers  
                    ' System.IO.File.Delete(folderPath + _res + foldername + .gif ou .jpg
                    If Item.Thumbnail <> glbPath & "images/TTT/TTT_folder.gif" Then
                        System.IO.File.Delete(Server.MapPath(Item.Thumbnail))
                    End If
                    ' Clear integrated info of forum
                    Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(_galleryFolder.IntegratedForumID)
                    With forumInfo
                        .IsIntegrated = False
                        .IntegratedAlbumName = ""
                        .IntegratedGallery = 0
                        .UpdateForumInfo()
                        .ResetForumInfo(_galleryFolder.IntegratedForumID)
                    End With
                Else

                    Dim _galleryFile As GalleryFile = CType(Zrequest.Folder.List.Item(ItemIndex), GalleryFile)
                    Dim filePath As String = _galleryFile.Path
                    Dim thumbURL As String = _galleryFile.ThumbNail
                    Dim thumbPath As String = Server.MapPath(thumbURL)
                    System.IO.File.Delete(filePath)
                    If (_galleryFile.ThumbNail <> glbPath & "images/TTT/TTT_MediaPlayer.gif") And (_galleryFile.ThumbNail <> glbPath & "images/TTT/TTT_Flash.gif") Then
                        System.IO.File.Delete(thumbPath)
                    End If
                End If
            Catch Exc As System.Exception
            End Try




            GalleryConfig.ResetGalleryConfig(ModuleId)
            ' Reset the File Quota
            Dim StrFolder As String
            Dim SpaceUsed As Double
            Dim objAdmin As New AdminDB()
            StrFolder = Request.MapPath(Zconfig.RootURL)
            SpaceUsed = objAdmin.GetFolderSizeRecursive(StrFolder)
            objAdmin.AddDirectory(StrFolder, SpaceUsed.ToString())
            Response.Redirect(Request.UrlReferrer.ToString)

        End Sub


        Private Sub dlStrip_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlStrip.ItemCreated
            Dim TmpControl As Control
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

            'Response.Redirect(file.URL, True)
            Response.AppendHeader("content-disposition", "attachment; filename=" & fileName)
            Response.ContentType = "application/octet-stream"
            'Response.ContentType = file.Type.ToString
            Response.WriteFile(filePath)
            Response.End()

        End Sub




    End Class
End Namespace