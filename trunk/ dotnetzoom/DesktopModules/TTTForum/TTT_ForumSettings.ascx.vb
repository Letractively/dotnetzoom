'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
' =======================================================================================

Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports System.IO

Namespace DotNetZoom

    Public Class TTT_ForumSettings
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents O1 As DotNetZoom.OpenClose
        Protected WithEvents O2 As DotNetZoom.OpenClose
        Protected WithEvents O3 As DotNetZoom.OpenClose
        Protected WithEvents O4 As DotNetZoom.OpenClose
        Protected WithEvents O5 As DotNetZoom.OpenClose


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
        Protected WithEvents btnImage As System.Web.UI.WebControls.Button
        Protected WithEvents chkUserOnline As System.Web.UI.WebControls.CheckBox
        Protected WithEvents Regularexpressionvalidator1 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents txtStatsUpdateInterval As System.Web.UI.WebControls.TextBox

        Protected WithEvents textbox1 As System.Web.UI.WebControls.TextBox
        Protected WithEvents textbox2 As System.Web.UI.WebControls.TextBox
        Protected WithEvents textbox3 As System.Web.UI.WebControls.TextBox
        Protected WithEvents textbox4 As System.Web.UI.WebControls.TextBox
        Protected WithEvents textbox5 As System.Web.UI.WebControls.TextBox
        Protected WithEvents textbox6 As System.Web.UI.WebControls.TextBox
        Protected WithEvents textbox7 As System.Web.UI.WebControls.TextBox


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

 			
            lnkavatar.Text = GetLanguage("F_AdminAvatar")
			lblAvatarInfo.Text = GetLanguage("F_AvInfo")
			btnAvatarConfig.ToolTip = GetLanguage("F_AvInfo1")
			optHTMLFormat.Items.FindByValue("False").Text = GetLanguage("text")
			optHTMLFormat.Items.FindByValue("True").Text = GetLanguage("html")
			
			btnUpdate.Text = GetLanguage("enregistrer")
			btnUpdate.ToolTip = GetLanguage("enregistrer")

            btnImage.Text = GetLanguage("visualiser")
            btnImage.ToolTip = GetLanguage("visualiser")


            If Not Page.IsPostBack Then
                O1.What = GetLanguage("F_ImgSettings")
                O2.What = GetLanguage("F_EMailYes")
                O3.What = GetLanguage("F_ForumFormat")
                O4.What = GetLanguage("F_UOInt")
                O5.What = GetLanguage("F_GalHeight")

                txtImageFolder.Text = ForumConfig.DefaultImageFolder()
                If InStr(1, txtImageFolder.Text.ToLower(), _portalSettings.UploadDirectory.ToLower()) Then
                    btnImage.Visible = True
                Else
                    btnImage.Visible = False
                End If

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
                    Dim _settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)
                    textbox1.Text = GetValue(_settings(GetLanguage("N") + "_NewMessageTXT"), GetLanguage("Forum_NewMessageTXT"))
                    textbox2.Text = GetValue(_settings(GetLanguage("N") + "_ModMessageTXT"), GetLanguage("Forum_ModMessageTXT"))
                    textbox3.Text = GetValue(_settings(GetLanguage("N") + "_ReplayMessageTXT"), GetLanguage("Forum_ReplayMessageTXT"))
                    textbox4.Text = GetValue(_settings(GetLanguage("N") + "_BodyMessageTXT"), GetLanguage("Forum_BodyMessageTXT"))
                    textbox5.Text = GetValue(_settings(GetLanguage("N") + "_Moderated_messageTXT"), GetLanguage("Forum_Moderated_messageTXT"))
                    textbox6.Text = GetValue(_settings(GetLanguage("N") + "_Moderated_approvedTXT"), GetLanguage("Forum_Moderated_approvedTXT"))
                    textbox7.Text = GetValue(_settings(GetLanguage("N") + "_Moderated_refusedTXT"), GetLanguage("Forum_Moderated_refusedTXT"))


                End If

                If Zconfig.AvatarModuleID = 0 Then
                    pnlAvatarConfig.Visible = True
                    lblAvatar.Visible = False
                    cmdavatar.Visible = False
                    lnkavatar.Visible = False
                    btnAvatarConfig.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm_avatar")) & "');")
                Else
                    ' Get Tab Id of Module Zconfig.AvatarModuleID
                    PopulateURL(Zconfig.AvatarModuleID)
                End If



            End If
			
			
        End Sub

        Private Function GetValue(ByVal Input As Object, ByVal DefaultValue As String) As String
            ' Used to determine if a valid input is provided, if not, return default value

            If Input Is Nothing Then
                Return DefaultValue
            Else
                Return CStr(Input)
            End If

        End Function


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
            ' Check if ImageFolder exist
            If CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("ImageFolder"), String) <> txtImageFolder.Text Then
                If Not txtImageFolder.Text.EndsWith("/") Then
                    txtImageFolder.Text += "/"
                End If

                If InStr(1, txtImageFolder.Text.ToLower(), _portalSettings.UploadDirectory.ToLower()) Then
                    btnImage.Visible = True
                Else
                    btnImage.Visible = False
                End If

                If Not System.IO.Directory.Exists(Request.MapPath(txtImageFolder.Text)) And InStr(1, txtImageFolder.Text.ToLower(), _portalSettings.UploadDirectory.ToLower()) Then
                    'Create the new directory and put in files on if in Portal
                    Try
                        System.IO.Directory.CreateDirectory(Request.MapPath(txtImageFolder.Text))
                        Dim fileEntries As String() = System.IO.Directory.GetFiles(GetAbsoluteServerPath(Request) + "images\ttt\")
                        Dim TempFileName As String
                        Dim strFileName As String
                        For Each strFileName In fileEntries
                            TempFileName = strFileName.Replace(GetAbsoluteServerPath(Request) + "images\ttt\", Request.MapPath(txtImageFolder.Text))
                            System.IO.File.Copy(strFileName, TempFileName)
                        Next strFileName
                    Catch
                        SendHttpException("403", "Forbidden", Request, Request.MapPath(txtImageFolder.Text))
                    End Try
                End If
                If System.IO.Directory.Exists(Request.MapPath(txtImageFolder.Text)) Then
                    admin.UpdatePortalSetting(_portalSettings.PortalId, "ImageFolder", txtImageFolder.Text)
                End If
            End If


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

            If textbox1.Text <> GetLanguage("Forum_NewMessageTXT") Then
                admin.UpdateModuleSetting(ModuleId, GetLanguage("N") + "_NewMessageTXT", textbox1.Text)
            End If
            If textbox2.Text <> GetLanguage("Forum_ModMessageTXT") Then
                admin.UpdateModuleSetting(ModuleId, GetLanguage("N") + "_ModMessageTXT", textbox2.Text)
            End If
            If textbox3.Text <> GetLanguage("Forum_ReplayMessageTXT") Then
                admin.UpdateModuleSetting(ModuleId, GetLanguage("N") + "_ReplayMessageTXT", textbox3.Text)
            End If
            If textbox4.Text <> GetLanguage("Forum_BodyMessageTXT") Then
                admin.UpdateModuleSetting(ModuleId, GetLanguage("N") + "_BodyMessageTXT", textbox4.Text)
            End If
            If textbox5.Text <> GetLanguage("Forum_Moderated_messageTXT") Then
                admin.UpdateModuleSetting(ModuleId, GetLanguage("N") + "_Moderated_messageTXT", textbox5.Text)
            End If
            If textbox6.Text <> GetLanguage("Forum_Moderated_approvedTXT") Then
                admin.UpdateModuleSetting(ModuleId, GetLanguage("N") + "_Moderated_approvedTXT", textbox6.Text)
            End If
            If textbox7.Text <> GetLanguage("Forum_Moderated_refusedTXT") Then
                admin.UpdateModuleSetting(ModuleId, GetLanguage("N") + "_Moderated_refusedTXT", textbox7.Text)
            End If

            ForumConfig.ResetForumConfig(ModuleId)
        End Sub

        Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
            ' Update settings in the database
            UpdateSettings()
        End Sub


        Private Sub btnImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImage.Click
            ' redirect do see files
            Dim TempURL As String = glbPath & "DesktopModules/TTTGallery/fckimage.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)
            Session("FCKeditor:UserFilesPath") = CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("ImageFolder"), String)
            Response.Redirect(TempURL, True)
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