'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' =======================================================================================

Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports System.IO

Namespace DotNetZoom

    Public Class TTT_ForumSettings
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents lblAvatar As System.Web.UI.WebControls.Label
        Protected WithEvents cmdavatar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkavatar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents txtImageFolder As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAvatarFolder As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkUserAvatar As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkNotify As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkSenderAddress As System.Web.UI.WebControls.CheckBox
        Protected WithEvents txtAutomatedAddress As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtEmailDomain As System.Web.UI.WebControls.TextBox
        Protected WithEvents optHTMLFormat As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents txtTheardsPerPage As System.Web.UI.WebControls.TextBox
        Protected WithEvents Regularexpressionvalidator6 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents txtPostsPerPage As System.Web.UI.WebControls.TextBox
        Protected WithEvents Regularexpressionvalidator7 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents txtAvatarSize As System.Web.UI.WebControls.TextBox
        Protected WithEvents Regularexpressionvalidator3 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents txtImageHeight As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtImageWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtImageExtensions As System.Web.UI.WebControls.TextBox
        Protected WithEvents btnUpdate As System.Web.UI.WebControls.Button
        Protected WithEvents chkUserOnline As System.Web.UI.WebControls.CheckBox
        Protected WithEvents cmdUserAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkUserAdmin As System.Web.UI.WebControls.HyperLink
        Protected WithEvents Regularexpressionvalidator1 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents txtStatsUpdateInterval As System.Web.UI.WebControls.TextBox

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

        Protected WithEvents btnAvatarConfig As System.Web.UI.WebControls.ImageButton
        Protected WithEvents lblAvatarInfo As System.Web.UI.WebControls.Label
        Protected WithEvents pnlAvatarConfig As System.Web.UI.WebControls.PlaceHolder
        Dim Zconfig As ForumConfig

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

			Regularexpressionvalidator6.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator7.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator3.ErrorMessage = GetLanguage("need_number")
			Regularexpressionvalidator1.ErrorMessage = GetLanguage("need_number")
			Dim ImageFolder As String = ForumConfig.SkinImageFolder()	
			Dim objCSS As Control = page.FindControl("CSS")
			Dim objTTTCSS As Control = page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl
            If (Not objCSS Is Nothing) and (objTTTCSS Is Nothing) Then
                    ' put in the ttt.css
					objLink = New System.Web.UI.LiteralControl("TTTCSS")
					If Request.IsAuthenticated Then
					Dim UserCSS as ForumUser
					UserCSS = ForumUser.GetForumUser(Int16.Parse(Context.User.Identity.Name))
					Select Case UserCSS.Skin
					case "Jardin Floral"
                            objLink.Text = "<link href=""" & glbPath & "images/TTT/skin1/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                        Case "Stibnite"
                            objLink.Text = "<link href=""" & glbPath & "images/TTT/skin2/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                        Case "Algues bleues"
                            objLink.Text = "<link href=""" & glbPath & "images/TTT/skin3/ttt.css"" type=""text/css"" rel=""stylesheet"">"
					Case Else
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
					End Select
					else
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                    End If
					objCSS.Controls.Add(objLink)
            End If

		
 			
            lnkavatar.Text = GetLanguage("F_AdminAvatar")
			lblAvatarInfo.Text = GetLanguage("F_AvInfo")
			btnAvatarConfig.ToolTip = GetLanguage("F_AvInfo1")
			optHTMLFormat.Items.FindByValue("False").Text = GetLanguage("text")
			optHTMLFormat.Items.FindByValue("True").Text = GetLanguage("html")
			
			btnUpdate.Text = GetLanguage("enregistrer")
			btnUpdate.ToolTip = GetLanguage("enregistrer")

           If Not Page.IsPostBack Then
           txtImageFolder.Text = CType(portalSettings.GetSiteSettings(_portalSettings.PortalID)("ImageFolder"), String)
			If txtImageFolder.Text = "" then
                    txtImageFolder.Text = glbSiteDirectory() & "ttt/"
			end if

			
                If ModuleId > 0 Then
                    Zconfig = ForumConfig.GetForumConfig(ModuleId)
                    txtAvatarFolder.Text = Zconfig.AvatarFolder
                    chkUserAvatar.Checked = Zconfig.UserAvatar
                    chkNotify.Checked = Zconfig.MailNotification
                    chkSenderAddress.Checked = Zconfig.ShowSenderAddressOnEmail
                    txtAutomatedAddress.Text = Zconfig.AutomatedEmailAddress
                    txtEmailDomain.Text = Zconfig.EmailDomain
                    optHTMLFormat.Items.FindByValue(Zconfig.HTMLMailFormat.ToString).Selected = True
                    txtTheardsPerPage.Text = Convert.ToString(Zconfig.ThreadsPerPage)
                    txtPostsPerPage.Text = Convert.ToString(Zconfig.PostsPerPage)
                    txtImageHeight.Text = Convert.ToString(Zconfig.ImageSizeHeight)
                    txtImageWidth.Text = Convert.ToString(Zconfig.ImageSizeHeight)
                    txtAvatarSize.Text = Convert.ToString(Zconfig.MaxAvatarSize)
                    txtImageExtensions.Text = Convert.ToString(Zconfig.ImageExtensions)
                    chkUserOnline.Checked = Zconfig.UserOnlineIntegrate
                    txtStatsUpdateInterval.Text = Zconfig.StatsUpdateInterval.ToString
                    
                End If

                If Zconfig.AvatarModuleID = 0 Then
                    pnlAvatarConfig.Visible = True
                    lblAvatar.Visible = False
                    cmdavatar.Visible = False
                    lnkavatar.Visible = False
                    btnAvatarConfig.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm_avatar")) & "');")
                Else
                    ' Get Tab Id of Module Zconfig.AvatarModuleID
                    PopulateURL(Zconfig.AvatarModuleID)
                End If

               
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = ""
                End If

            End If
			
			
        End Sub

        Private Sub PopulateURL(ByVal GalModuleId As Integer)
            Dim admin As New AdminDB()
            Dim dr As SqlDataReader = admin.GetModule(GalModuleId)
            If dr.Read Then
                lblAvatar.Visible = True
                cmdavatar.Visible = True
                lnkavatar.Visible = True
                lnkavatar.NavigateUrl = "~/" + GetLanguage("N") + ".default.aspx" + "?tabid=" + ConvertString(dr("TabId"))
                cmdavatar.NavigateUrl = lnkavatar.NavigateUrl
            Else
                lnkavatar.NavigateUrl = ""
                cmdavatar.NavigateUrl = ""
                lblAvatar.Visible = False
                cmdavatar.Visible = False
                lnkavatar.Visible = False
            End If
            dr.Close()
            pnlAvatarConfig.Visible = False

        End Sub

        Private Sub UpdateSettings()
            Dim admin As New AdminDB()
            admin.UpdatePortalSetting(_portalSettings.PortalId, "ImageFolder", txtImageFolder.Text)
            admin.UpdateModuleSetting(ModuleId, "AvatarFolder", txtAvatarFolder.Text)
            admin.UpdateModuleSetting(ModuleId, "UserAvatar", chkUserAvatar.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "MailNotification", chkNotify.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "ShowSenderAddressOnEmail", chkSenderAddress.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "AutomatedEmailAddress", txtAutomatedAddress.Text)
            admin.UpdateModuleSetting(ModuleId, "EmailDomain", txtEmailDomain.Text)
            admin.UpdateModuleSetting(ModuleId, "HTMLMailFormat", (optHTMLFormat.SelectedItem.Value).ToString)
            admin.UpdateModuleSetting(ModuleId, "ThreadsPerPage", txtTheardsPerPage.Text)
            admin.UpdateModuleSetting(ModuleId, "PostsPerPage", txtPostsPerPage.Text)
            admin.UpdateModuleSetting(ModuleId, "ImageSizeHeight", txtImageHeight.Text)
            admin.UpdateModuleSetting(ModuleId, "ImageSizeHeight", txtImageWidth.Text)
            admin.UpdateModuleSetting(ModuleId, "MaxAvatarSize", txtAvatarSize.Text)
            admin.UpdateModuleSetting(ModuleId, "ImageExtensions", txtImageExtensions.Text)
            admin.UpdateModuleSetting(ModuleId, "UserOnlineIntegrate", chkUserOnline.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "StatsUpdateInterval", txtStatsUpdateInterval.Text)

            ForumConfig.ResetForumConfig(ModuleId)
        End Sub

        Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
            ' Update settings in the database
            UpdateSettings()
        End Sub


        Private Sub btnAvatarConfig_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAvatarConfig.Click
            Dim dbAdmin As New AdminDB()
            Dim dbForum As New ForumDB()
            ' Dim intTabId As Integer = 0
            Dim newModuleID As Integer = 0
            Dim galleryDefID As Integer = 0

            Dim dr As SqlDataReader = dbAdmin.GetModuleDefinitions(_portalSettings.PortalId, "--",  False)
            While dr.Read
                If ConvertString(dr("FriendlyName")) = "Galerie de photos" Then
                    galleryDefID = ConvertInteger(dr("ModuleDefID"))
                    Exit While
                End If
            End While
            dr.Close()

            If galleryDefID = 0 Then
				lblAvatarInfo.Text = GetLanguage("F_AvInfo2")
                Return
            End If

           Try

                Dim strAuthorizedRoles As String = _portalSettings.ActiveTab.AdministratorRoles
                If InStr(1, strAuthorizedRoles, _portalSettings.AdministratorRoleId.ToString & ";") = 0 Then
                    strAuthorizedRoles += _portalSettings.AdministratorRoleId.ToString & ";"
                End If
                Dim intTabId As Integer = -1
                intTabId = dbAdmin.AddTab(_portalSettings.PortalId, "Avatar" + ModuleId.ToString, True, "Avatar" + ModuleId.ToString, strAuthorizedRoles, "0", "0", False, False, -1, "", strAuthorizedRoles, intTabId)

                dbAdmin.UpdatePortalTabOrder(PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N")), intTabId, _portalSettings.ActiveTab.TabId, , , "False")
               
                newModuleID = dbAdmin.AddModule(intTabId, -1, "ContentPane", "Avatar", galleryDefID, 0, strAuthorizedRoles, False, "")

				Dim _Settings As HashTable = portalSettings.GetSiteSettings(_portalSettings.PortalID)
				dbadmin.UpdateModuleSetting( newModuleID, "containerTitleHeaderClass", IIf(_Settings("containerTitleHeaderClass") <> "", _Settings("containerTitleHeaderClass"), ""))
			    dbadmin.UpdateModuleSetting( newModuleID, "containerTitleCSSClass", IIf(_Settings("containerTitleCSSClass") <> "", _Settings("containerTitleCSSClass"), ""))
				dbadmin.UpdateModuleSetting( newModuleID, "container", IIf(_Settings("container") <> "", _Settings("container"), ""))
				dbadmin.UpdateModuleSetting( newModuleID, "TitleContainer", IIf(_Settings("TitleContainer") <> "", _Settings("TitleContainer"), ""))
				dbadmin.UpdateModuleSetting( newModuleID, "ModuleContainer", IIf(_Settings("ModuleContainer") <> "", _Settings("ModuleContainer"), ""))
				dbadmin.UpdateModuleSetting( newModuleID, "containerAlignment", IIf(_Settings("containerAlignment") <> "", _Settings("containerAlignment"), ""))
				dbadmin.UpdateModuleSetting( newModuleID, "containerColor", IIf(_Settings("containerColor") <> "", _Settings("containerColor"), ""))
				dbadmin.UpdateModuleSetting( newModuleID, "containerBorder", IIf(_Settings("containerBorder") <> "", _Settings("containerBorder"), ""))

                'Set init values for Avatar Gallery
                dbAdmin.UpdateModuleSetting(newModuleID, "GalleryTitle", "Avatar Gallery")
                dbAdmin.UpdateModuleSetting(newModuleID, "Description", "Avatars for TTTForum")
                dbAdmin.UpdateModuleSetting(newModuleID, "RootURL", txtAvatarFolder.Text)
                dbAdmin.UpdateModuleSetting(newModuleID, "StripWidth", "8")
                dbAdmin.UpdateModuleSetting(newModuleID, "StripHeight", "3")
                dbAdmin.UpdateModuleSetting(newModuleID, "Quota", "0")
                dbAdmin.UpdateModuleSetting(newModuleID, "MaxFileSize", "10")
                dbAdmin.UpdateModuleSetting(newModuleID, "MaxThumbWidth", "50")
                dbAdmin.UpdateModuleSetting(newModuleID, "MaxThumbHeight", "50")
                dbAdmin.UpdateModuleSetting(newModuleID, "FileExtensions", txtImageExtensions.Text)
                dbAdmin.UpdateModuleSetting(newModuleID, "MovieExtensions", "")
                dbAdmin.UpdateModuleSetting(newModuleID, "CategoryValues", "Avatar")
                dbAdmin.UpdateModuleSetting(newModuleID, "BuildCacheOnStart", "True")
                dbAdmin.UpdateModuleSetting(newModuleID, "IsFixedSize", "True")
                dbAdmin.UpdateModuleSetting(newModuleID, "FixedWidth", "50")
                dbAdmin.UpdateModuleSetting(newModuleID, "FixedHeight", "50")
				dbAdmin.UpdateModuleSetting(newModuleID, "Quality", "50")
                dbAdmin.UpdateModuleSetting(newModuleID, "IsKeepSource", "False")
                dbAdmin.UpdateModuleSetting(newModuleID, "SlideshowSpeed", "3000")
                dbAdmin.UpdateModuleSetting(newModuleID, "IsPrivate", "False")
                dbAdmin.UpdateModuleSetting(newModuleID, "IsIntegrated", "False")
                dbAdmin.UpdateModuleSetting(newModuleID, "SlideshowPopup", "False")
                dbAdmin.UpdateModuleSetting(newModuleID, "AllowDownload", "False")
                dbAdmin.UpdateModuleSetting(newModuleID, "IsAvatarsGallery", ModuleId.ToString())
                dbAdmin.UpdateModuleSetting(newModuleID, "Owner", "")
                dbAdmin.UpdateModuleSetting(newModuleID, "OwnerID", _portalSettings.AdministratorId.ToString)
                Dim userAvatarPath As String = Server.MapPath(Zconfig.AvatarFolder)
                If Not Directory.Exists(userAvatarPath) Then
                    Directory.CreateDirectory(userAvatarPath) 'reserve only
                End If
                UpdateSettings()
                PopulateURL(newModuleID)
            Catch Exc As System.Exception
                lblAvatarInfo.Text = Exc.Message
            End Try
            ClearPortalCache(_portalSettings.PortalId)
        End Sub
    End Class

End Namespace