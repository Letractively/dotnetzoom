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
        Protected WithEvents txtOwner As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents lstCategories As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents CancelButton As System.Web.UI.WebControls.Button
        Protected WithEvents imgFile As System.Web.UI.WebControls.Image
        Protected WithEvents txtOwnerID As System.Web.UI.WebControls.TextBox
        Protected WithEvents dlFolders As System.Web.UI.WebControls.DataList
        Protected WithEvents UpdateButton As System.Web.UI.WebControls.Button

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

            If Not Zfolder.IsPopulated Then
                Zrequest.Folder.Populate()
            End If
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

                If Not Zfolder.IsPopulated Then
                    Response.Redirect(glbPath & "DesktopModules/TTTGallery/TTT_cache.aspx" & HttpContext.Current.Request.Url.Query & "&mid=" & ZmoduleID & "&tabid=" & TabId)
                End If

                'BindData(Zrequest)
                BindData()


            End If

        End Sub

        'Private Sub BindData(ByVal Request As GalleryRequest)
        Private Sub BindData()

            dlFolders.DataSource = Zrequest.FolderPaths
            dlFolders.DataBind()

            With ZFile
                txtPath.Text = .URL
                txtName.Text = .Name
                txtOwner.Text = .Owner.UserName
                txtOwnerID.Text = .OwnerID.ToString
                txtTitle.Text = .Title
                txtDescription.Text = .Description
                imgFile.ImageUrl = .ThumbNail
            End With

            BindCategories()

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

                If InStr(1, ZFile.Categories, catString) > 0 Then
                    catItem.Selected = True
                End If
                'list category for current item
                lstCategories.Items.Add(catItem)

            Next

        End Sub

        Private Sub UpdateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateButton.Click

            Dim directory As String = Zrequest.Folder.Path
            Dim name As String = ZFile.Name
            Dim ownerID As Integer = Int16.Parse(Me.txtOwnerID.Text)
            Dim title As String = txtTitle.Text
            Dim description As String = txtDescription.Text
            Dim categories As String = GetCategories(lstCategories)
            Dim lWidth As String = ZFile.Width
            Dim lHeight As String = ZFile.Height
            Dim strExtension As String = ""
            If InStr(1, name, ".") <> 0 Then
                strExtension = Mid(Name, InStrRev(Name, ".") + 1)
            End If

            Select Case strExtension.ToLower()
                Case "jpg", "jpeg", "tif", "png"
                    Dim fileName As String
                    FileName = Server.MapPath(ZFile.URL)
                    Dim Exif As New ExifWorks(FileName)
                    Exif.Artist = txtOwner.Text
                    Exif.Copyright = GetDomainName(Request)
                    Exif.Description = txtDescription.Text
                    Exif.Title = txtTitle.Text
                    Dim BMP As System.Drawing.Bitmap = Exif.GetBitmap()
                    BMP.Save(FileName & ".tmp")
                    BMP.Dispose()
                    Exif.Dispose()
                    System.IO.File.Delete(FileName)
                    System.IO.File.Move(FileName & ".tmp", FileName)
            End Select



            GalleryXML.SaveMetaData(directory, name, title, description, categories, ownerID, LWidth, Lheight)
            GalleryConfig.ResetGalleryConfig(ZmoduleID)

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

        Private Sub ctlUsers_UserSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlUsers.UserSelected
            Dim myUser As ForumUser = CType(ctlUsers.SelectedUser, ForumUser)

            txtOwner.Text = myUser.Name
            txtOwnerID.Text = myUser.UserID.ToString

            pnlSelectOwner.Visible = False

        End Sub
		
		
		
    End Class
End Namespace