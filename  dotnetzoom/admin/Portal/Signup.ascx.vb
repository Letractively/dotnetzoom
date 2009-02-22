'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
'

Imports System.Xml
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System
Imports System.object
Imports System.Collections
Imports System.Configuration
Imports System.Globalization
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Text.RegularExpressions

Namespace DotNetZoom

    Public Class Signup
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents Title1 As DesktopModuleTitle
        Protected WithEvents lblInstructions As System.Web.UI.WebControls.Label
		Protected WithEvents MyHtmlImage As System.Web.UI.WebControls.Image
        Protected WithEvents rowType As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents optParent As System.Web.UI.WebControls.RadioButton
        Protected WithEvents optChild As System.Web.UI.WebControls.RadioButton
        Protected WithEvents txtPortalName As System.Web.UI.WebControls.TextBox
        Protected WithEvents ValSum As System.Web.UI.WebControls.ValidationSummary
        Protected WithEvents valPortalName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
        Protected WithEvents lblAdminAccount As System.Web.UI.WebControls.Label
		Protected WithEvents lblPortalDef As System.Web.UI.WebControls.Label
        Protected WithEvents txtFirstName As System.Web.UI.WebControls.TextBox
        Protected WithEvents valFirstName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtLastName As System.Web.UI.WebControls.TextBox
        Protected WithEvents valLastName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtUsername As System.Web.UI.WebControls.TextBox
        Protected WithEvents valUsername As System.Web.UI.WebControls.RequiredFieldValidator
		Protected WithEvents RvalUsername As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
        Protected WithEvents valPassword As System.Web.UI.WebControls.RequiredFieldValidator
		Protected WithEvents RvalPassword As System.Web.UI.WebControls.RegularExpressionValidator
		
        Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
        Protected WithEvents valEmail As System.Web.UI.WebControls.RequiredFieldValidator
		Protected WithEvents RvalEmail As System.Web.UI.WebControls.RegularExpressionValidator

        Protected WithEvents cboTemplate As System.Web.UI.WebControls.DropDownList

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton

		Dim strPortalAlias As String = ""
		
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

        '*******************************************************
        '
        ' The Page_Load server event handler on this page is used
        ' to populate the role information for the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Title1.DisplayHelp = "DisplayHelp_Signup"
            Dim strFolder As String
            Dim strFileName As String
			Dim Admin As New AdminDB()
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' ensure portal signup is allowed
            If (not PortalSecurity.IsSuperUser) And portalSettings.GetHostSettings("DemoSignup") <> "Y" Then
                AccessDenied()
            End If
            lblPortalDef.Text = Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "PortalCreationInfo")
            cmdUpdate.Text = GetLanguage("S_CreatePortal")
            cmdCancel.Text = GetLanguage("annuler")
            valPortalName.ErrorMessage = GetLanguage("need_portal_name")
            valFirstName.ErrorMessage = GetLanguage("need_firstname")
            valLastName.ErrorMessage = GetLanguage("need_lastname")
            valUsername.ErrorMessage = GetLanguage("need_username")
            RvalUsername.ErrorMessage = GetLanguage("need_username_minimum")
            valPassword.ErrorMessage = GetLanguage("need_password")
            RvalPassword.ErrorMessage = GetLanguage("need_password_minimum")
            valEmail.ErrorMessage = GetLanguage("need_email")
            RvalEmail.ErrorMessage = GetLanguage("need_valid_email")
            ValSum.HeaderText = GetLanguage("error")
            If Not Page.IsPostBack Then

                If (Not PortalSecurity.IsSuperUser) And PortalSettings.GetHostSettings("DemoSignup") = "Y" Then
                    ' check if guid passed and the one in session are the same
                    If (Request.Params("guid") Is Nothing) Or (System.Web.HttpContext.Current.Session("GUID") Is Nothing) Then
                        AccessDenied()
                    Else
                        If Request.Params("guid") <> System.Web.HttpContext.Current.Session("GUID") Then
                            AccessDenied()
                        End If
                    End If
                    System.Web.HttpContext.Current.Session("GUID") = Guid.NewGuid().ToString()
                End If




                strFolder = Request.MapPath(glbTemplatesDirectory)

                If System.IO.Directory.Exists(strFolder) Then
                    Dim DirEntries As String() = System.IO.Directory.GetDirectories(strFolder)
                    For Each strFileName In DirEntries
                        Dim dir As New DirectoryInfo(strFileName)
                        cboTemplate.Items.Add(dir.Name)
                    Next
                    cboTemplate.SelectedIndex = 0
                Else
                    cboTemplate.Items.Insert(0, GetLanguage("list_none"))
                    cboTemplate.SelectedIndex = 0
                End If
                cmdUpdate.Attributes.Add("onclick", "Page_ClientValidate(); toggleBox('attendre',Page_IsValid);toggleBox('main',!Page_IsValid);")
                If Not (Request.Params("hostpage") Is Nothing) Then
                    If cboTemplate.Items.FindByText(GetLanguage("list_none")) Is Nothing Then
                        cboTemplate.Items.Insert(0, GetLanguage("list_none"))
                        cboTemplate.SelectedIndex = 0
                    End If
                    rowType.Visible = True
                    optParent.Checked = True
                    Title1.DisplayTitle = GetLanguage("S_Title")
                    lblInstructions.Text = Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "Demo_Portal_Info1")

                Else
                    rowType.Visible = False
                    Title1.DisplayTitle = GetLanguage("SD_Title")
                    lblInstructions.Text = Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "Demo_Portal_Info2")
                End If
                lblInstructions.Text = Replace(lblInstructions.Text, "{DomainName}", GetDomainName(Request))
                lblInstructions.Text = Replace(lblInstructions.Text, "{days}", PortalSettings.GetHostSettings("DemoPeriod"))
                If PortalSettings.GetHostSettings("DemoPeriod") = Nothing Then
                    lblInstructions.Text = Regex.Replace(lblInstructions.Text, "{DemoPeriod}[^{}]+{/DemoPeriod}", "", RegexOptions.IgnoreCase)
                End If
                lblInstructions.Text = Regex.Replace(lblInstructions.Text, "{DemoPeriod}", "", RegexOptions.IgnoreCase)
                lblInstructions.Text = Regex.Replace(lblInstructions.Text, "{/DemoPeriod}", "", RegexOptions.IgnoreCase)
                SetTemplateImage()

            End If
			
        End Sub

		Private Sub SetTemplateImage()
				If cboTemplate.SelectedItem.Text <> getlanguage("list_none") Then
				Dim ImageURL As STring
    			ImageUrl =  "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & glbTemplatesDirectory 
    			If Not ImageUrl.EndsWith("/") Then
          			ImageUrl += "/"
   				End If
		  		myHtmlImage.ImageUrl = ImageUrl & cboTemplate.SelectedItem.Text & "/template.jpg"
		   		myHtmlImage.AlternateText = cboTemplate.SelectedItem.Text
	   			myHtmlImage.ToolTip = cboTemplate.SelectedItem.Text
				MyHtmlImage.Visible = True
				Else
				MyHtmlImage.Visible = True
				End if	
		
		End Sub
		
		Private Sub cboTemplate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTemplate.SelectedIndexChanged
			SetTemplateImage()	
		End Sub
		
        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, ""), True)
        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
            Page.Validate()
            If Page.IsValid Then
                Dim blnChild As Boolean
                Dim strMessage As String = ""
                Dim intCounter As Integer
                Dim intPortalId As Integer
                Dim strServerPath As String
                Dim strBody As String
                Dim LogoFile As String = ""
                Dim BkFile As String = ""
                Dim strExpiryDate As String
                Dim TempDomainName As String = ""
                Dim tempGUID As String
                Dim tempPortalAlias As String
                Dim strEditRoles As String
                Dim TempAdministratorID As Integer
                Dim dr As SqlDataReader
                Dim objAdmin As New AdminDB()

                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                Dim SubDomain As Boolean = False
                If PortalSettings.GetSiteSettings(_portalSettings.PortalId)("DemoDomain") <> Nothing Then
                    If CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("DemoDomain"), String) = "Y" Then
                        SubDomain = True
                    End If
                End If


                txtPortalName.Text = LCase(txtPortalName.Text)
                txtPortalName.Text = Replace(txtPortalName.Text, "http://", "")
                tempPortalAlias = GetDomainName(Request).ToLower()

                If (Request.Params("hostpage") Is Nothing) Then
                    blnChild = True

                    ' child portal
                    For intCounter = 1 To txtPortalName.Text.Length
                        If InStr(1, "abcdefghijklmnopqrstuvwxyz0123456789-", Mid(txtPortalName.Text, intCounter, 1)) = 0 Then
                            strMessage = GetLanguage("No_accent_In_Name")
                        End If
                    Next intCounter

                    strPortalAlias = txtPortalName.Text
                Else
                    blnChild = optChild.Checked

                    If blnChild Then
                        strPortalAlias = Mid(txtPortalName.Text, InStrRev(txtPortalName.Text, "/") + 1)
                    Else
                        strPortalAlias = txtPortalName.Text
                        tempPortalAlias = txtPortalName.Text
                    End If

                    Dim strValidChars As String = "abcdefghijklmnopqrstuvwxyz0123456789-"
                    If Not blnChild Then
                        strValidChars += "./:,"
                    End If

                    For intCounter = 1 To strPortalAlias.Length
                        If InStr(1, strValidChars, Mid(strPortalAlias, intCounter, 1)) = 0 Then
                            strMessage = GetLanguage("No_accent_In_Name")
                        End If
                    Next intCounter

                End If

                ' Validate to make sur all is well

                If strPortalAlias = "" Then
                    strMessage = strMessage & GetLanguage("S_Need_Portal_Name")
                End If
                If txtFirstName.Text = "" Or txtLastName.Text = "" Then
                    strMessage = strMessage & GetLanguage("S_Need_Names")
                End If
                If txtUsername.Text.Length < 7 Then
                    strMessage = strMessage & GetLanguage("S_User_Name")

                    Dim r As New Random()
                    Dim i As Integer
                    Dim strTemp As String = ""
                    For i = 0 To (7 - txtUsername.Text.Length)
                        strTemp = strTemp & Chr(Int((26 * r.NextDouble()) + 65))
                    Next
                    txtUsername.Text = txtUsername.Text & strTemp
                End If
                If txtPassword.Text.Length < 7 Then
                    strMessage = strMessage & GetLanguage("S_Password")
                End If

                'Check to make sure it is a valid E-Mail

                If InStr(txtEmail.Text, "@") = 0 Or _
                      InStr(txtEmail.Text, ".") = 0 Or _
                      txtEmail.Text.Length < 7 Then
                    strMessage = strMessage & GetLanguage("S_EMail")
                End If

                strServerPath = GetAbsoluteServerPath(Request)

                If strMessage = "" Then
                    If Not FolderPermissions(strServerPath & "Portals\") Then
                        SendHttpException("403", "Forbidden", Request, strServerPath & "Portals\")
                        ' Response.Redirect("403-3.htm", True)
                    End If
                    If blnChild Then
                        If Not (Request.Params("hostpage") Is Nothing) Or ((Request.Params("hostpage") Is Nothing) And Not SubDomain) Then
                            If Not FolderPermissions(strServerPath) Then
                                SendHttpException("403", "Forbidden", Request, strServerPath)
                                ' Response.Redirect("/403-3.htm", True)
                            End If
                        End If
                    End If
                End If


                If strMessage = "" Then
                    If blnChild Then
                        If Not System.IO.Directory.Exists(strServerPath & strPortalAlias) Then
                            ' create the subdirectory for the new portal
                            If Not SubDomain Or Not (Request.Params("hostpage") Is Nothing) Then
                                System.IO.Directory.CreateDirectory(strServerPath & strPortalAlias)
                                ' create the subhost default.aspx file

                                Dim objStreamReader As StreamReader
                                objStreamReader = File.OpenText(strServerPath + "subhost.html")
                                Dim strHTML As String = objStreamReader.ReadToEnd
                                objStreamReader.Close()
                                strHTML = Replace(strHTML, "window.location =", "window.location = ""http://" + GetDomainName(Request).ToLower() + "/" + strPortalAlias + "/default.aspx""")
                                Dim objStream As StreamWriter
                                objStream = File.CreateText(strServerPath & strPortalAlias & "\index.html")
                                objStream.WriteLine(strHTML)
                                objStream.Close()
                            End If
                            If (Request.Params("hostpage") Is Nothing) Then
                                TempDomainName = GetDomainName(Request).ToLower()
                                If SubDomain Then
                                    If TempDomainName.IndexOf("www.") = 0 Then
                                        strPortalAlias = Replace(TempDomainName, "www", strPortalAlias)
                                    Else
                                        strPortalAlias = strPortalAlias & "." & TempDomainName
                                    End If
                                    tempPortalAlias = strPortalAlias
                                    If PortalSettings.GetPortalByAlias(strPortalAlias) <> -1 Then
                                        strMessage = GetLanguage("S_NameAlreadyUsed")
                                        strMessage = Replace(strMessage, "{PortalAlias}", strPortalAlias)
                                    End If
                                Else
                                    strPortalAlias = GetDomainName(Request) & "/" & strPortalAlias
                                End If
                            Else
                                strPortalAlias = txtPortalName.Text
                            End If
                        Else
                            strMessage = GetLanguage("S_NameAlreadyUsed1")
                        End If
                    End If
                End If

                If strMessage = "" Then

                    If PortalSettings.GetHostSettings("DemoPeriod") <> "" Then
                        strExpiryDate = formatansidate(DateAdd(DateInterval.Day, Int32.Parse(PortalSettings.GetHostSettings("DemoPeriod")), Now()).ToString("yyyy-MM-dd"))
                    Else
                        strExpiryDate = ""
                    End If


                    Dim objSecurity As New PortalSecurity()

                    Dim dblHostFee As Double = 0
                    If PortalSettings.GetHostSettings("HostFee") <> "" Then
                        Try
                            dblHostFee = Double.Parse(PortalSettings.GetHostSettings("HostFee"))
                        Catch objException As Exception
                            If InStr(1, PortalSettings.GetHostSettings("HostFee"), ".") Then
                                PortalSettings.GetHostSettings("HostFee") = Replace(PortalSettings.GetHostSettings("HostFee"), ".", ",")
                            Else
                                PortalSettings.GetHostSettings("HostFee") = Replace(PortalSettings.GetHostSettings("HostFee"), ",", ".")
                            End If
                            dblHostFee = Double.Parse(PortalSettings.GetHostSettings("HostFee"))
                        End Try
                    End If

                    Dim dblHostSpace As Double = 0
                    If (Request.Params("hostpage") Is Nothing) Then
                        dblHostSpace = 20
                    Else
                        If PortalSettings.GetHostSettings("HostSpace") <> "" Then
                            dblHostSpace = Double.Parse(PortalSettings.GetHostSettings("HostSpace"))
                        End If
                    End If

                    Dim intSiteLogHistory As Integer = -1
                    If (Request.Params("hostpage") Is Nothing) Then
                        intSiteLogHistory = 7
                    Else
                        If PortalSettings.GetHostSettings("SiteLogHistory") <> "" Then
                            intSiteLogHistory = Integer.Parse(PortalSettings.GetHostSettings("SiteLogHistory"))
                        End If
                    End If

                    ' add new portal to database
                    intPortalId = objAdmin.AddPortalInfo(GetLanguage("N"), cboTemplate.SelectedItem.Text = GetLanguage("list_none"), txtPortalName.Text, strPortalAlias.ToLower, PortalSettings.GetHostSettings("HostCurrency"), txtFirstName.Text, txtLastName.Text, txtUsername.Text, objSecurity.Encrypt(PortalSettings.GetHostSettings("EncryptionKey"), txtPassword.Text), txtEmail.Text, CheckDateSqL(strExpiryDate), dblHostFee, dblHostSpace, intSiteLogHistory, intPortalId)
                    ' AddPortalInfo(ByVal Language As String, ByVal HomePage As Boolean, ByVal PortalName As String, ByVal PortalAlias As String, Optional ByVal Currency As String = "", Optional ByVal FirstName As String = "", Optional ByVal LastName As String = "", Optional ByVal Username As String = "", Optional ByVal Password As String = "", Optional ByVal Email As String = "", Optional ByVal ExpiryDate As String = "", Optional ByVal HostFee As Double = 0, Optional ByVal HostSpace As Double = 0, Optional ByVal SiteLogHistory As Integer = -1, <SqlParameter(, , , , , ParameterDirection.Output)> Optional ByVal PortalId As Integer = -1) As Integer


                    Dim TempTemplateDir As String

                    TempTemplateDir = strServerPath
                    If cboTemplate.SelectedItem.Text <> GetLanguage("list_none") Then
                        TempTemplateDir = strServerPath & "templates\" & cboTemplate.SelectedItem.Text & "\"
                    End If

                    dr = objAdmin.GetSinglePortal(intPortalId)
                    If dr.Read Then
                        strEditRoles = dr("AdministratorRoleId").ToString & ";"
                        TempAdministratorID = dr("AdministratorID")
                        objAdmin.UpdatePortalSetting(intPortalId, "uploadroles", strEditRoles)
                        ' Add user to Forum Dbase
                        Dim dbForumUser As New ForumUserDB()
                        dbForumUser.TTTForum_UserCreateUpdateDelete(TempAdministratorID, txtUsername.Text, True, False, "", "", "", _portalSettings.TimeZone, "", "", "", "", "", "", "", False, True, False, True, True, True, 0)

                        ' create the upload directory for the new portal
                        ' need to copy all file in the template dir glbTemplatesDirectory to the portal dir
                        System.IO.Directory.CreateDirectory(strServerPath & "Portals\" & dr("GUID").ToString)
                        Dim TempPortalDir As String
                        tempGUID = dr("GUID").ToString
                        TempPortalDir = strServerPath & "Portals\" & dr("GUID").ToString & "\"
                        Dim TempFileName As String

                        ' copy all in the template dir to the new portal except template.txt and template.jpg
                        If TempTemplateDir <> strServerPath Then
                            If System.IO.Directory.Exists(TempTemplateDir) Then
                                Dim fileEntries As String() = System.IO.Directory.GetFiles(TempTemplateDir)
                                Dim strFileName As String
                                For Each strFileName In fileEntries
                                    If InStr(1, strFileName.ToLower, "template.") = 0 Then
                                        Select Case strFileName.ToLower
                                            Case "logo.gif", "logo.jpg" : LogoFile = strFileName.ToLower
                                            Case "bk.gif", "bk.jpg" : BkFile = strFileName.ToLower
                                        End Select
                                        TempFileName = strFileName.Replace(TempTemplateDir, TempPortalDir)
                                        System.IO.File.Copy(strFileName, TempFileName)
                                    End If
                                Next strFileName
                                Dim StrFolder As String
                                For Each StrFolder In System.IO.Directory.GetDirectories(TempTemplateDir)
                                    CopyFileRecursively(StrFolder, StrFolder.Replace(TempTemplateDir, TempPortalDir))
                                Next
                            End If
                        Else
                            ' only copy the logo file and the css file if the main directory
                            If Not System.IO.Directory.Exists(TempPortalDir & "\skin") Then
                                System.IO.Directory.CreateDirectory(TempPortalDir & "\skin")
                            End If
                            Dim fileEntries As String() = System.IO.Directory.GetFiles(TempTemplateDir)
                            Dim strFileName As String
                            For Each strFileName In fileEntries
                                If (InStr(1, strFileName.ToLower, ".css") <> 0) _
                                 Or (InStr(1, strFileName.ToLower, ".jpg") <> 0) _
                                 Or (InStr(1, strFileName.ToLower, ".gif") <> 0) _
                                 Or (InStr(1, strFileName.ToLower, "menu_tpl1.js") <> 0) Then
                                    If (InStr(1, strFileName.ToLower, ".jpg") <> 0) Or (InStr(1, strFileName.ToLower, ".gif") <> 0) Then
                                        TempFileName = strFileName.Replace(TempTemplateDir, TempPortalDir)
                                        Select Case strFileName.ToLower
                                            Case "logo.gif", "logo.jpg" : LogoFile = strFileName.ToLower
                                            Case "bk.gif", "bk.jpg" : BkFile = strFileName.ToLower
                                        End Select
                                    Else
                                        TempFileName = strFileName.Replace(TempTemplateDir, TempPortalDir & "\skin\")
                                    End If
                                    System.IO.File.Copy(strFileName, TempFileName)
                                End If
                            Next strFileName

                        End If
                        ' SynchronizeFile in DB
                        objAdmin.SynchronizeFiles(intPortalId, TempPortalDir)

                        If cboTemplate.SelectedItem.Text <> GetLanguage("list_none") Then
                            'Parse the template for the new portal
                            Dim objStreamReader As StreamReader
                            objStreamReader = File.OpenText(TempTemplateDir & "template.txt")
                            Dim xmlData As String = objStreamReader.ReadToEnd
                            objStreamReader.Close()

                            xmlData = Replace(xmlData, "[GUID]", "http://" & tempPortalAlias & "/portals/" & tempGUID & "/")

                            ' Parse XML to build Site	
                            PopulateSiteModule(xmlData, intPortalId, GetLanguage("N"), strEditRoles, TempAdministratorID)

                        End If
                        ' Update Language
                        ' mettre a jour les infos du portail
                        ' just need to put in the Logo
                        objAdmin.UpdatePortalInfo(intPortalId, txtPortalName.Text, "", LogoFile, "", 0, 0, PortalSettings.GetHostSettings("HostCurrency"), TempAdministratorID, CheckDateSqL(strExpiryDate), dblHostFee, dblHostSpace, "", "", "", "", "", BkFile, intSiteLogHistory, _portalSettings.TimeZone)
                        If LogoFile = "" Then
                            objAdmin.UpdatePortalSetting(intPortalId, "flash", "<span style=""font-size: 22pt"">" & strPortalAlias & "</span>")
                        End If
                        objAdmin.UpdatePortalSetting(intPortalId, "language", GetLanguage("N"))
                        objAdmin.UpdatePortalSetting(intPortalId, "languageauth", GetLanguage("N") & ";")

                    End If
                    dr.Close()
                    If (Request.Params("hostpage") Is Nothing) Then
                        strBody = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "email_new_demo_portal")
                    Else
                        strBody = objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "email_new_portal")
                    End If
                    tempGUID = Guid.NewGuid().ToString()
                    Dim objUser As New UsersDB()
                    objUser.UpdateCheckUserSecurity(TempAdministratorID, tempGUID, DateTime.Now.AddHours(24), 0)
                    Dim ValidationURL As String

                    ValidationURL = "http://" & strPortalAlias.ToLower() & "/default.aspx?showlogin=1&validate=" & tempGUID

                    strBody = Regex.Replace(strBody, "{FullName}", txtFirstName.Text & " " & txtLastName.Text, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{PortalName}", _portalSettings.PortalName, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{PortalURL}", "http://" & strPortalAlias, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{Username}", txtUsername.Text, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{Password}", txtPassword.Text, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{validationcode}", ValidationURL, RegexOptions.IgnoreCase)
                    strBody = Regex.Replace(strBody, "{HostEmail}", PortalSettings.GetHostSettings("HostEmail"), RegexOptions.IgnoreCase)
                    ' notification
                    If (Request.Params("hostpage") Is Nothing) Then
                        SendNotification(PortalSettings.GetHostSettings("HostEmail"), txtEmail.Text, PortalSettings.GetHostSettings("HostEmail"), GetLanguage("Demo_Portal"), strBody, "", "html")
                    Else
                        SendNotification(PortalSettings.GetHostSettings("HostEmail"), txtEmail.Text, PortalSettings.GetHostSettings("HostEmail"), GetLanguage("New_Portal"), strBody, "", "html")
                    End If



                    ' Redirect to this new site
                    Response.Redirect(GetPortalDomainName(strPortalAlias, Request) + "/default.aspx", True)
                Else
                    lblMessage.Text = strMessage & "<br>"
                End If
            End If
        End Sub


        Private Sub optChild_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optChild.CheckedChanged
            txtPortalName.Text = GetDomainName(Request) & "/"
        End Sub

        Private Function FolderPermissions(ByVal strServerPath As String) As Boolean
            ' verify folder permission settings
            Try
                If System.IO.Directory.Exists(strServerPath & "_verify") = False Then
                    System.IO.Directory.CreateDirectory(strServerPath & "_verify")
                End If
                If System.IO.Directory.Exists(strServerPath & "_verify") = True Then
                    System.IO.Directory.Delete(strServerPath & "_verify")
                End If

                FolderPermissions = True
            Catch ' security error
                FolderPermissions = False
            End Try

        End Function

        Public Function GetModuleSignup(ByVal ModuleDefID As Integer, ByVal ModuleTitle As String, ByVal TabId As Integer) As Integer

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim TempModuleID As Integer = -1
            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleDefID, ModuleTitle, TabId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            myConnection.Close()
            ' Return the ModuleID
            If result.Read Then
                TempModuleID = Int32.Parse(result("ModuleID").ToString)
            End If
            result.Close()
            Return TempModuleID

        End Function


        Private Sub PopulateSiteModule(ByVal xmlData As String, ByVal intPortalId As Integer, ByVal Language As String, ByVal strEditRoles As String, ByVal TAdministratorID As Integer)

            Dim xmlDoc As New XmlDocument()
            Dim nodeTab As XmlNode
            Dim nodePane As XmlNode
            Dim nodeModule As XmlNode
            Dim nodeData As XmlNode
            Dim nodeSetting As XmlNode
            Dim nodeSiteSetting As XmlNode
            Dim intTabId As Integer
            Dim dr As SqlDataReader
            Dim Admin As New AdminDB()
            Dim TempModuleID As Integer
            Dim TempDefinition As String


            xmlDoc.Load(New StringReader(xmlData))



            ' xmlDoc.Load(TempTemplateDir & "template.txt")

            ' Update Portal Settings if present
            For Each nodeSiteSetting In xmlDoc.SelectNodes("//portal/sitesettings")
                Admin.UpdatePortalSetting(intPortalId, nodeSiteSetting.Item("settingname").InnerText, nodeSiteSetting.Item("settingvalue").InnerText)
            Next


            Dim NodeToLoad As String = "//portal/tabs[@language = '" & Language & "']/tab"

            Dim elemList As XmlNodeList

            elemList = xmlDoc.SelectNodes(NodeToLoad)
            If Not elemList.Count > 0 Then
                NodeToLoad = "//portal/tabs[@language = '']/tab"
            End If


            Dim TempTabID As Integer
            intTabId = -1
            Dim intTabOrder As Integer = 1
            For Each nodeTab In xmlDoc.SelectNodes(NodeToLoad)
                TempTabID = -1
                intTabOrder += 2
                dr = Admin.GetTabByName(nodeTab.Item("name").InnerText, intPortalId)
                If dr.Read Then
                    intTabId = dr("TabId")
                Else
                    ' add tab

                    If nodeTab.Item("parent").InnerText <> "" Then
                        Dim drs As SqlDataReader = Admin.GetTabByName(nodeTab.Item("parent").InnerText, intPortalId)
                        If drs.Read Then
                            TempTabID = drs("TabId")
                        End If
                        drs.Close()
                    End If

                    intTabId = Admin.AddTab(intPortalId, nodeTab.Item("name").InnerText, True, Admin.convertstringtounicode(nodeTab.Item("name").InnerText.ToLower), "-1;", "200", "200", CBool(nodeTab.Item("visible").InnerText), False, TempTabID, "", strEditRoles, intTabId)
                    Admin.UpdateTabOrder(intTabId, nodeTab.Item("taborder").InnerText, nodeTab.Item("level").InnerText, TempTabID)

                End If
                dr.Close()

                For Each nodePane In nodeTab.SelectSingleNode("panes").ChildNodes
                    For Each nodeModule In nodePane.SelectSingleNode("modules").ChildNodes
                        ' get module definition
                        dr = Admin.GetSingleModuleDefinitionByName(GetLanguage("N"), nodeModule.Item("definition").InnerText)
                        If dr.Read Then
                            ' add module
                            TempModuleID = Admin.AddModule(intTabId, -1, nodePane.Item("name").InnerText, nodeModule.Item("title").InnerText, dr("ModuleDefId"), 0, strEditRoles, False, GetLanguage("N"))
                            TempDefinition = nodeModule.Item("definition").InnerText
                            For Each nodeSetting In nodeModule.SelectSingleNode("modulesettings").ChildNodes
                                If nodeSetting.Item("settingname").InnerText <> "" Then
                                    Admin.UpdateModuleSetting(TempModuleID, nodeSetting.Item("settingname").InnerText, nodeSetting.Item("settingvalue").InnerText)
                                End If
                            Next
                            For Each nodeData In nodeModule.SelectSingleNode("datas").ChildNodes
                                If nodeData.Item("data1").InnerText <> "" Then
                                    PopulateModule(TempModuleID, TempDefinition, nodeData.Item("data1").InnerText, nodeData.Item("data2").InnerText, nodeData.Item("data3").InnerText, nodeData.Item("data4").InnerText, nodeData.Item("data5").InnerText, nodeData.Item("data6").InnerText, TAdministratorID)
                                End If
                            Next
                        End If
                        dr.Close()
                    Next
                Next
            Next

        End Sub



        Private Sub PopulateModule(ByVal TModuleID As Integer, ByVal TDefinition As String, ByVal data1 As String, ByVal data2 As String, ByVal data3 As String, ByVal data4 As String, ByVal data5 As String, ByVal data6 As String, ByVal AdministratorID As Integer)

            Select Case TDefinition

                Case "Contacts"
                    ' Create an instance of the ContactsDB component
                    Dim contacts As New ContactsDB()
                    ' Data1 = Name  Data2 = Role Data3 = EMail Data4 = Contact1Field Data5 = Contact2Field
                    contacts.AddContact(TModuleID, AdministratorID, data1, data2, data3, data4, data5)
                Case "FAQs"
                    ' Create an instance of the FAQsDB component
                    Dim FAQs As New FAQsDB()
                    ' Data1 = QuestionField  Data2 = AnswerField
                    FAQs.AddFAQ(TModuleID, AdministratorID, data1, data2)
                Case "Hyperliens"
                    Dim links As New LinkDB()
                    ' Data1 = Title  Data2 = Link Data 3 = vieworder data4=description data5=newwindow
                    links.AddLink(TModuleID, AdministratorID, data1, data2, "", data3, data4, CType(data5, Boolean))
                Case "Calendrier"
                    Dim events As New EventsDB()
                    ' Data1 = Description  Data2 = DateTime Data 3 = Title data4=ExpiryDate data5=icone data6=alttext
                    ' AddModuleEvent(ByVal ModuleId As Integer, ByVal Description As String, ByVal DateTime As Date, ByVal Title As String, ByVal ExpireDate As String, ByVal UserName As String, ByVal Every As String, ByVal Period As String, ByVal IconFile As String, ByVal AltText as String)
                    Dim TempInteger As Integer = 0
                    Dim TempExpiryDate As String = ""
                    If data4 <> "" Then
                        If IsNumeric(data4) Then
                            TempInteger = CType(data4, Integer)
                            TempExpiryDate = formatansidate(DateTime.Now.AddDays(TempInteger).ToString("yyyy-MM-dd"))
                            TempInteger = 0
                        End If
                    End If

                    If IsNumeric(data2) Then
                        TempInteger = CType(data2, Integer)
                    End If


                    events.AddModuleEvent(TModuleID, data1, DateTime.Now.AddDays(TempInteger), data3, CheckDateSqL(TempExpiryDate), AdministratorID, "", "", data5, data6)
                Case "HTML/Texte"
                    Dim objHTML As New HtmlTextDB()
                    ' Data1 = text  Data2 = AltSummary Data 3 = AltDetail
                    data1 = Regex.Replace(data1, "{FullName}", txtFirstName.Text & " " & txtLastName.Text, RegexOptions.IgnoreCase)
                    data1 = Regex.Replace(data1, "{PortalURL}", "http://" & strPortalAlias, RegexOptions.IgnoreCase)
                    data1 = ProcessLanguage(data1)
                    objHTML.UpdateHtmlText(TModuleID, data1, data2, data3)

                Case "Image"
                    Dim objAdmin As New AdminDB()
                    objAdmin.UpdateModuleSetting(TModuleID, "src", data1)
                    objAdmin.UpdateModuleSetting(TModuleID, "alt", data2)
                    objAdmin.UpdateModuleSetting(TModuleID, "width", data3)
                    objAdmin.UpdateModuleSetting(TModuleID, "height", data4)

                Case "Babillard"
                    Dim objAnnouncements As New AnnouncementsDB()
                    ' Data1 = Title  Data2 = expireddate Data 3 = description data4=Link 
                    objAnnouncements.AddAnnouncement(TModuleID, Context.User.Identity.Name, data1, data2, data3, data4, True, "")
            End Select
        End Sub
    End Class

End Namespace