'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'=======================================================================================
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by:
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' =======================================================================================


Imports System.IO
Imports System
Imports System.Web
Imports System.Web.UI
Imports Microsoft.VisualBasic
Imports DotNetZoom.TTTUtils


Namespace DotNetZoom

    Public Class TTT_ForumUserProfile
        Inherits PortalModuleControl

        Dim ZuserID As Integer
        Protected WithEvents lnkavatar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
		Protected WithEvents lblTimeZone As System.Web.UI.WebControls.Label
        Protected WithEvents txtUserID As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtUserName As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkRichText As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkAvatar As System.Web.UI.WebControls.CheckBox
        Protected WithEvents ddlAvatar As System.Web.UI.WebControls.DropDownList
        Protected WithEvents btnUploadAvatar As System.Web.UI.WebControls.Button
        Protected WithEvents pnlAvatarUpload As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents pnlAvatarDefault As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtAvatar As System.Web.UI.WebControls.TextBox
        Protected WithEvents imgAvatar As System.Web.UI.WebControls.Image
        Protected WithEvents ddlSkin As System.Web.UI.WebControls.DropDownList
        Protected WithEvents chkMemberList As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkOnlineStatus As System.Web.UI.WebControls.CheckBox
        Protected WithEvents txtSignature As System.Web.UI.WebControls.TextBox
        Protected WithEvents ddlTimeZone As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtOccupation As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtInterests As System.Web.UI.WebControls.TextBox
        Protected WithEvents pnlUserProfile As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents txtAlias As System.Web.UI.WebControls.TextBox
        Protected WithEvents lnkPMS As System.Web.UI.WebControls.HyperLink
        Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
        Protected WithEvents lnkEmail As System.Web.UI.WebControls.HyperLink
        Protected WithEvents pnlEmail As System.Web.UI.WebControls.PlaceHolder

		Protected WithEvents RowURL As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents txtURL As System.Web.UI.WebControls.TextBox
        Protected WithEvents lnkWWW As System.Web.UI.WebControls.HyperLink
        Protected WithEvents txtMSN As System.Web.UI.WebControls.TextBox
		Protected WithEvents RowMSN As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents lnkMSN As System.Web.UI.WebControls.HyperLink
        Protected WithEvents txtYahoo As System.Web.UI.WebControls.TextBox
		Protected WithEvents RowYahoo As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents lnkYahoo As System.Web.UI.WebControls.HyperLink
        Protected WithEvents txtAIM As System.Web.UI.WebControls.TextBox
		Protected WithEvents RowAIM As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents lnkAIM As System.Web.UI.WebControls.HyperLink
        Protected WithEvents txtICQ As System.Web.UI.WebControls.TextBox
		Protected WithEvents RowICQ As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents lnkICQ As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblStatistic As System.Web.UI.WebControls.Label
        Protected WithEvents pnlPublicInfo As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents CancelButton As System.Web.UI.WebControls.Button
        Protected WithEvents UpdateButton As System.Web.UI.WebControls.Button
        Protected WithEvents htmlAvatar As System.Web.UI.HtmlControls.HtmlInputFile

        Dim _isAuthenticated As Boolean
        Protected WithEvents txtFullName As System.Web.UI.WebControls.TextBox
        Protected WithEvents pnlUserAvatar As System.Web.UI.WebControls.PlaceHolder
        Public Shared ZoldAlias As String

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
			
			Dim objCSS As Control = page.FindControl("CSS")
			Dim objTTTCSS As Control = page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
  			Dim ImageFolder As String = ForumConfig.SkinImageFolder()
            If (Not objCSS Is Nothing) Then
                    ' put in the ttt.css
					If Not ObjTTTCSS Is Nothing then
					objCSS.Controls.Remove(objTTTCSS)
					End If
					objLink = New System.Web.UI.LiteralControl("TTTCSS")
					objLink.EnableViewState = False
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


            If IsNumeric(Request.Params("userid")) Then
                ZuserID = Int32.Parse(Request.Params("userid"))
            Else
                ZuserID = 0
            End If
			
            _isAuthenticated = Request.IsAuthenticated

            Dim Zconfig As ForumConfig = ForumConfig.GetForumConfig(ModuleId)
            Dim Zuser As ForumUser = ForumUser.GetForumUser(ZuserID)

            Me.imgAvatar.Visible = (Len(Zuser.Avatar) > 0)

            lnkavatar.ToolTip = GetLanguage("select_avatar_tooltip")
            lnkavatar.Text = GetLanguage("select_avatar")

            Dim ParentID As String = Server.HtmlEncode(txtAvatar.ClientID)
            lnkavatar.NavigateUrl = "javascript:OpeniconeWindow('" + TabId.ToString + "','" + ParentID + "')"


            If Not Page.IsPostBack Then
				btnUploadAvatar.Text = GetLanguage("upload")
				btnUploadAvatar.Tooltip = GetLanguage("upload")
				UpdateButton.Text = GetLanguage("enregistrer")
				UpdateButton.ToolTip = GetLanguage("enregistrer")
				CancelButton.Text = GetLanguage("annuler")	
				CancelButton.Tooltip = GetLanguage("annuler")
                'Dim dbUser As New ForumUserDB()
                ' Clear cache to make sure that new info will be displayed
                ForumUser.ResetForumUser(ZuserID)
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = ""
                End If
                'Add to check duplicated alias
                ZoldAlias = Zuser.Alias

                txtAlias.Text = Zuser.Alias
                lnkPMS.Visible = (_isAuthenticated)
                lnkPMS.NavigateUrl = ForumPMSComposeLink(_portalSettings.ActiveTab.TabId, Zuser.UserID)
				lnkPMS.Tooltip = GetLanguage("F_SendEMailTo") + " " + Zuser.Alias
				lnkPMS.Text = GetLanguage("F_SendPMS")
                '<tam:note value=public Info>
                txtURL.Text = Zuser.URL
                lnkWWW.Visible = (Len(Zuser.URL) > 0)
                lnkWWW.NavigateUrl = AddHTTP(Zuser.URL)
				lnkWWW.Tooltip = GetLanguage("F_VisitUserSite") + " " + Zuser.URL
				lnkWWW.Text = GetLanguage("F_VisitWWW")
                txtEmail.Text = Zuser.Email
                lnkEmail.Visible = (Len(Zuser.Email) > 0 AndAlso _isAuthenticated)
                lnkEmail.NavigateUrl = "mailto:" & Zuser.Email
				lnkEmail.Tooltip = GetLanguage("F_SendEMailToUser") + " "  + Zuser.Alias
                lnkEmail.Text = GetLanguage("F_SendEMail") 
				txtMSN.Text = Zuser.MSN
                lnkMSN.Visible = (Len(Zuser.MSN) > 0 AndAlso _isAuthenticated)
                lnkMSN.NavigateUrl = MSNLink(Zuser.MSN)
				lnkMSN.Text = getlanguage("FZuserMSN")

                txtYahoo.Text = Zuser.Yahoo
                lnkYahoo.Visible = (Len(Zuser.Yahoo) > 0 AndAlso _isAuthenticated)
                lnkYahoo.NavigateUrl = YahooLink(Zuser.Yahoo)
				lnkYahoo.Text = getlanguage("FZuserYAHOO")
                txtAIM.Text = Zuser.AIM
                lnkAIM.Visible = (Len(Zuser.AIM) > 0 AndAlso _isAuthenticated)
                lnkAIM.NavigateUrl = AIMLink(Zuser.AIM)
				lnkAIM.Text = getlanguage("FZuserAIM")
                txtICQ.Text = Zuser.ICQ
                lnkICQ.Visible = (Len(Zuser.ICQ) > 0 AndAlso _isAuthenticated)
                lnkICQ.NavigateUrl = ICQLink(Zuser.ICQ)
				lnkICQ.Text = getlanguage("FZuserICQ")
				lblStatistic.Text = replace(GetLanguage("F_Contributed"), "{username}", Zuser.Alias) 
				lblStatistic.Text = replace(lblStatistic.Text, "{numpost}", Zuser.PostCount.ToString) 
			
				
                Dim LoggonedUserID As Integer
                If _isAuthenticated Then
                    LoggonedUserID = ConvertInteger(Context.User.Identity.Name)
					Dim Currentuser As ForumUser = ForumUser.GetForumUser(LoggonedUserID)
					Dim TempDateTime as DateTime
					TempDateTime = Zuser.LastActivity.AddMinutes(GetTimeDiff(Currentuser.TimeZone))
					lblStatistic.Text = replace(lblStatistic.Text, "{datelast}", TempDateTime.ToString()) 
                Else
                    LoggonedUserID = -1
					lblStatistic.Text = replace(lblStatistic.Text, "{datelast}", Zuser.LastActivity.ToLongDateString) 
	            End If

                '<tam:note value=User Profile>
                If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True) _
                OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                OrElse ZuserID = LoggonedUserID Then
                    pnlUserProfile.Visible = True
                    UpdateButton.Visible = True
					ddlSkin.Items.Clear()
					Dim TempDomainName As String = HttpContext.Current.Request.Url.Host.ToLower()
						If TempDomainName.indexof("www.") = 0 then
						TempDomainName = Replace(TempDomainName, "www.", "")
						end if
					ddlSkin.Items.Add(TempDomainName)
					ddlSkin.Items.Add("Portail")
					ddlSkin.Items.Add("Jardin Floral")
					ddlSkin.Items.Add("Stibnite")
					ddlSkin.Items.Add("Algues bleues")
					Dim objAdmin As New AdminDB()
					ddlTimeZone.DataSource =  objAdmin.GetTimeZoneCodes(GetLanguage("N"))
					ddlTimeZone.DataBind()
					txtUserID.Text = ConvertString(Zuser.UserID)
                    txtUserName.Text = Zuser.Name
                    txtFullName.Text = Zuser.FullName
					If Zconfig.UserAvatar Then
                        pnlUserAvatar.Visible = True
                        chkAvatar.Checked = Zuser.UserAvatar
                        pnlAvatarUpload.Visible = chkAvatar.Checked
                        pnlAvatarDefault.Visible = (Not chkAvatar.Checked)
                        If Len(Zuser.Avatar) > 0 Then
                            txtAvatar.Text = Zuser.Avatar
                            imgAvatar.ImageUrl = Zuser.Avatar
                        End If
                        PopulateAvatar(Zuser.Avatar, Zuser.UserAvatar) 'ddlAvatar
                    End If
                    chkMemberList.Checked = Zuser.EnableDisplayInMemberList
                    chkOnlineStatus.Checked = Zuser.EnableOnlineStatus
					Try
 		       		ddlTimeZone.SelectedValue = Zuser.TimeZone
			        Catch ex As Exception
       			    ddlTimeZone.SelectedValue = 0     
			        End Try
					Try
					If Len(Zuser.Skin) > 0 then
					ddlSkin.Items.FindByText(Zuser.Skin).Selected = True
					else
					ddlSkin.selectedindex = 0		
					end If
					lblTimeZone.Text = " " & GetLanguage("F_LocalTime") & " : " & DateTime.Now.AddMinutes(GetTimeDiff(Zuser.TimeZone)).ToString("t") 
					Catch Exc As System.Exception
                    End Try

					
					
                    txtSignature.Text = Zuser.Signature
                    txtOccupation.Text = Zuser.Occupation
                    txtInterests.Text = Zuser.Interests
                Else
				
					If _isAuthenticated then
                    Me.pnlEmail.Visible = Zuser.EnableDisplayInMemberList
					else
					Me.pnlEmail.Visible = false
					end If
                    txtAlias.Enabled = False
                    txtEmail.Enabled = False
                    txtURL.Enabled = False
					If TxtURL.Text = "" then RowURL.Visible = False
                    txtMSN.Enabled = False
					If TxtMSN.Text = "" then RowMSN.Visible = False
                    txtYahoo.Enabled = False
					If TxtYahoo.Text = "" then RowYahoo.Visible = False
                    txtAIM.Enabled = False
					If TxtAIM.Text = "" then RowAIM.Visible = False
                    txtICQ.Enabled = False
					If TxtICQ.Text = "" then RowICQ.Visible = False

                    pnlUserProfile.Visible = False
                    UpdateButton.Visible = False
                End If
            End If
        End Sub


		
		
        Private Sub PopulateAvatar(ByVal Avatar As String, ByVal UserAvatar As Boolean)
            Dim ZforumConfig As ForumConfig = ForumConfig.GetForumConfig(ModuleId)
            Dim avatarURL As String = ZforumConfig.AvatarFolder 
            Dim physicalPath As String = Server.MapPath(avatarURL)
            

            If Len(avatarURL) = 0 Then
                lblInfo.Text = GetLanguage("F_NoConfig")
                Return
            End If

            'Populate avatar folder to get file list
            Try
                Dim avatars() As String
                Dim file As String
                If Directory.Exists(physicalPath) Then
                    avatars = Directory.GetFiles(physicalPath)
                    Session("ForumThumbPath") = avatarURL
                Else
                    lblInfo.Text = GetLanguage("F_NoAvrRep")
                    Session("ForumThumbPath") = Nothing
                    chkAvatar.Enabled = False
                    ddlAvatar.Enabled = False
                    Return
                End If

                ddlAvatar.Items.Clear()
                ddlAvatar.Items.Insert(0, New ListItem(GetLanguage("F_SelectAvatar"), "-1"))

                For Each file In avatars
                    Dim fileName As String = Path.GetFileName(file)
                    If IsValidExtension(fileName, ZforumConfig.ImageExtensions) Then
                        ddlAvatar.Items.Add(fileName)
                    End If
                Next

                If Len(Avatar) > 0 AndAlso Not UserAvatar Then
                    ddlAvatar.Items.FindByText(Path.GetFileName(Avatar)).Selected = True
                Else
                    ddlAvatar.Items.FindByText(GetLanguage("F_SelectAvatar")).Selected = True
                End If

            Catch Exc As System.Exception
                lblInfo.Text = Exc.ToString
                Return
            End Try


        End Sub

        Private Sub ddlAvatar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAvatar.SelectedIndexChanged
            If ddlAvatar.SelectedIndex = -1 Then Return
            Dim avatar As String = ddlAvatar.SelectedItem.ToString
            Dim ZforumConfig As ForumConfig = ForumConfig.GetForumConfig(ModuleId)

            Dim avatarFolderURL As String = ZforumConfig.AvatarFolder
            Dim avatarURL As String = avatarFolderURL & avatar

			If InStr(1 , avatarURL , GetLanguage("F_SelectAvatar") ) > 0 then
				imgAvatar.ImageUrl = ""
				imgAvatar.Visible = False
            	txtAvatar.Text = ""
			else
				imgAvatar.ImageUrl = avatarURL
            	txtAvatar.Text = avatarURL
			End if

        End Sub

        Private Sub ddlTimeZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTimeZone.SelectedIndexChanged
		lblTimeZone.Text = " " & GetLanguage("F_LocalTime") & " : " & DateTime.Now.AddMinutes(GetTimeDiff(ddlTimeZone.SelectedItem.Value)).ToString("t") 
        End Sub		

        Private Sub ddlSkin_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSkin.SelectedIndexChanged
           
            If Not AliasIsValid() Then ' check to make sure alias is valid
                lblInfo.Text = GetLanguage("F_AliasUsed")
                txtAlias.Text = ZoldAlias
                Return
            End If

            Dim Zuser As ForumUser = ForumUser.GetForumUser(ZuserID)
            Try
                With Zuser
                    .Alias = txtAlias.Text
                    .UseRichText = chkRichText.Checked
                    .UserAvatar = chkAvatar.Checked
					If InStr(1 , txtAvatar.Text , GetLanguage("F_SelectAvatar") ) > 0 then
					.Avatar = ""
					else
                    .Avatar = txtAvatar.Text
					End if
                   
                    .URL = txtURL.Text
					.Skin = ddlSkin.SelectedItem.Text
				 	.TimeZone = ddlTimeZone.SelectedItem.Value
                    .Signature = txtSignature.Text
                    .Occupation = txtOccupation.Text
                    .Interests = txtInterests.Text
                    .MSN = txtMSN.Text
                    .Yahoo = txtYahoo.Text
                    .AIM = txtAIM.Text
                    .ICQ = txtICQ.Text
                    .EnableDisplayInMemberList = chkMemberList.Checked
                    .EnableOnlineStatus = chkOnlineStatus.Checked
                    .UpdateForumUser()
                End With

                ZoldAlias = txtAlias.Text ' refresh old alias value if update successful
				
            Catch Exc As System.Exception
                lblInfo.Text = Exc.ToString
				Return
            End Try
		ForumUser.ResetForumUser(ZuserID)
		Response.Redirect(Request.UrlReferrer.ToString())
        

		End Sub		
		
		
        Private Sub UpdateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateButton.Click
            
            If Not AliasIsValid() Then ' check to make sure alias is valid
                lblInfo.Text = GetLanguage("F_AliasUsed")
                txtAlias.Text = ZoldAlias
                Return
            End If

            Dim Zuser As ForumUser = ForumUser.GetForumUser(ZuserID)
            Try
                With Zuser
                    .Alias = txtAlias.Text
                    .UseRichText = chkRichText.Checked
                    .UserAvatar = chkAvatar.Checked
					If InStr(1 , txtAvatar.Text , GetLanguage("F_SelectAvatar") ) > 0 then
					.Avatar = ""
					else
                    .Avatar = txtAvatar.Text
					End if
                   .URL = txtURL.Text
					.Skin = ddlSkin.SelectedItem.Text
					.TimeZone = ddlTimeZone.SelectedItem.Value
                    .Signature = txtSignature.Text
                    .Occupation = txtOccupation.Text
                    .Interests = txtInterests.Text
                    .MSN = txtMSN.Text
                    .Yahoo = txtYahoo.Text
                    .AIM = txtAIM.Text
                    .ICQ = txtICQ.Text
                    .EnableDisplayInMemberList = chkMemberList.Checked
                    .EnableOnlineStatus = chkOnlineStatus.Checked
                    .UpdateForumUser()
                End With

                ZoldAlias = txtAlias.Text ' refresh old alias value if update successful

            Catch Exc As System.Exception
                lblInfo.Text = Exc.ToString
				Return
            End Try
    		ForumUser.ResetForumUser(ZuserID)
		Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub

        Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub

        Private Sub chkAvatar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAvatar.CheckedChanged
            pnlAvatarUpload.Visible = chkAvatar.Checked
            pnlAvatarDefault.Visible = Not chkAvatar.Checked
        End Sub

        Private Sub btnUploadAvatar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadAvatar.Click

			Dim SpaceUsed As Double
			Dim StrFolder As String
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim strExtension As String 
            Dim objAdmin As New AdminDB()
            strFolder = Request.MapPath(_portalSettings.UploadDirectory)
			SpaceUsed = objAdmin.GetDirectorySpaceUsed(strFolder)
			If SpaceUsed = 0 then
			SpaceUsed = objAdmin.GetFolderSizeRecursive(strFolder)
			objAdmin.AddDirectory( strFolder, SpaceUsed.tostring() )
			End If



            Dim Zconfig As ForumConfig = ForumConfig.GetForumConfig(ModuleId)
            

            Dim fileCheck As TTTUtils.ValidateFile = New ValidateFile()
            Dim myString As String = htmlAvatar.PostedFile.FileName
            With fileCheck
                .InputFile = htmlAvatar
                .AcceptedExtensions = Zconfig.ImageExtensions
                .AcceptedMaxSize = Zconfig.MaxAvatarSize
            End With

            If fileCheck.Valid Then
                Dim userAvatarPath As String = Server.MapPath(Zconfig.AvatarFolder + "users/")
                Dim userAvatarURL As String = Zconfig.AvatarFolder + "users/"
				Dim ZgalleryConfig As galleryConfig = galleryConfig.GetGalleryConfig(Zconfig.AvatarModuleID)
                Try
                    If Not Directory.Exists(userAvatarPath) Then
                        Directory.CreateDirectory(userAvatarPath) 'reserve only
                    End If

                    Dim fileNamePath As String = htmlAvatar.PostedFile.FileName.ToLower()
                    Dim fileName As String = Path.GetFileName(fileNamePath)
                    Dim strFileNamePath As String = Path.Combine(userAvatarPath, fileName)

                    If File.Exists(strFileNamePath) Then ' Check if same file name exists
                        lblInfo.Text = GetLanguage("F_FileNameUsed") 
                        Return
                    End If
					
   					If ((SpaceUsed / 1048576 <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Then
                    htmlAvatar.PostedFile.SaveAs(strFileNamePath + ".tmp")
					strExtension = Mid(fileName, InStrRev(fileName, ".")).ToLower
					ResizeImage(strFileNamePath + ".tmp", strFileNamePath, ZgalleryConfig.FixedWidth, ZgalleryConfig.FixedHeight, strExtension, ZgalleryConfig.quality)
					File.Delete(strFileNamePath + ".tmp")

					
					
					objAdmin.AddDirectory( strFolder, (SpaceUsed + filelen(strFileNamePath)).ToString())
                    '<tam:note change to save full image url />
                        txtAvatar.Text = userAvatarURL & fileName
                        Me.imgAvatar.ImageUrl = userAvatarURL & fileName
					Else
					lblInfo.Text = GetLanguage("F_NoSpaceLeft")  
					end If
                Catch Exc As System.Exception
                    lblInfo.Text = Exc.Message
                End Try

            Else
                lblInfo.Text = fileCheck.ErrorMessage
            End If

        End Sub

        Private Function AliasIsValid() As Boolean
            If Not LCase(txtAlias.Text) = LCase(ZoldAlias) Then
                Dim dbForumUser As New ForumUserDB()
				Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                Dim dr As SqlDataReader = dbForumUser.TTTForum_GetUsers(_portalSettings.PortalId, "")
                While dr.Read
                    If LCase(txtAlias.Text) = LCase(ConvertString(dr("Alias"))) OrElse LCase(txtAlias.Text) = LCase(ConvertString(dr("UserName"))) Then
                        If Not ConvertInteger(dr("UserID")) = ZuserID Then
                            Return False
                        End If
                    End If
                End While
                dr.Close()
            End If
            Return True
        End Function


    End Class

End Namespace