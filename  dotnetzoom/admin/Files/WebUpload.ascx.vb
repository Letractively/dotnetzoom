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

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Core
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml.Serialization
Imports System.Web.Script.Serialization
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection
Imports System.Web.Configuration


Namespace DotNetZoom

    Public Class WebUpload
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents cmdSynchronize As System.Web.UI.WebControls.LinkButton
        Protected WithEvents chkUploadRoles As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents options As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents Upload As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents cmdCancelOptions As System.Web.UI.WebControls.LinkButton

        Protected WithEvents CanUpload As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents chkUnzip As System.Web.UI.WebControls.CheckBox
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
        Protected WithEvents lblRootDir As System.Web.UI.WebControls.Label
        Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Private obj As New UploadInfo()
        Private Zrequest As GalleryRequest
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()

            If Not IsNothing(Session("UploadInfo")) And Request.IsAuthenticated Then
                obj = Session("UploadInfo")
            Else
                AccessDenied()
            End If


            If Not PortalSecurity.IsSuperUser And obj.IsHost Then
                AccessDenied()
            End If

            Dim SpaceUsed As Double
            Dim strFolder As String

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

            If Not (Request.Params("hostpage") Is Nothing) Then
                strFolder = Request.MapPath(glbSiteDirectory)
            Else
                strFolder = Request.MapPath(_portalSettings.UploadDirectory)
            End If

            SpaceUsed = objAdmin.GetdirectorySpaceUsed(strFolder)
            SpaceUsed = SpaceUsed / 1048576

            If (((SpaceUsed) <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Or (Not (Request.Params("hostpage") Is Nothing)) Then
            Else
                CanUpload.Visible = False
                lblMessage.Text = GetLanguage("Gal_PortalQuota2")
            End If


            If obj.IsGall Then
                If Not CheckQuota() Then
                    CanUpload.Visible = False
                End If
            End If


        End Sub

#End Region

        '*******************************************************
        '
        ' The Page_Load server event handler on this page is used
        ' to populate the role information for the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objCSS As Control = Page.FindControl("CSS")
            Dim objuploadifyCSS As Control = Page.FindControl("uploadifyCSS")
            Dim objLink As System.Web.UI.LiteralControl


            If (Not objCSS Is Nothing) And (objuploadifyCSS Is Nothing) Then
                ' put in the xxx.css
                objLink = New System.Web.UI.LiteralControl("uploadifyCSS")
                objLink.Text = "<link href=""" & glbPath() & "admin/files/uploadify.css"" rel=""stylesheet"" type=""text/css"" />"
                objCSS.Controls.Add(objLink)
            End If

            Title1.DisplayHelp = "DisplayHelp_WebUpload"
            cmdCancelOptions.Text = GetLanguage("annuler")
            cmdSynchronize.Text = GetLanguage("F_cmdSynchronize")
            cmdUpdate.Text = GetLanguage("F_Update")


            Dim objAdmin As New AdminDB()

            Dim tmpUploadRoles As String = ""
            If Not CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("uploadroles"), String) Is Nothing Then
                tmpUploadRoles = CType(PortalSettings.GetSiteSettings(_portalSettings.PortalId)("uploadroles"), String)
            End If


            If Request.IsAuthenticated = False Then
                EditDenied()
            End If

            If PortalSecurity.IsInRoles(tmpUploadRoles) = False Then
                CanUpload.Visible = False
                lblMessage.Text = ProcessLanguage(objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "Security_CannotUpload"), Page)
            End If



            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If
                Session("UrlReferrer") = ViewState("UrlReferrer")
                chkUnzip.Checked = False

                If Request.Params("options") <> "" Then

                    If Not PortalSecurity.IsInRole(_portalSettings.AdministratorRoleId.ToString) Or Not IsAdminTab() Then
                        'Autorisation
                        EditDenied()
                    End If
                    options.Visible = True
                    Upload.Visible = False
                    chkUploadRoles.Items.Clear()
                    Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)
                    Dim UploadRoles As String = ""
                    If Not CType(settings("uploadroles"), String) Is Nothing Then
                        UploadRoles = CType(settings("uploadroles"), String)
                    End If

                    Dim objUser As New UsersDB()

                    Dim dr As SqlDataReader = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))
                    While dr.Read()
                        Dim item As New ListItem()
                        item.Text = CType(dr("RoleName"), String)
                        item.Value = dr("RoleID").ToString()
                        If InStr(1, UploadRoles, item.Value & ";") Or item.Value = _portalSettings.AdministratorRoleId.ToString Then
                            item.Selected = True
                        End If

                        chkUploadRoles.Items.Add(item)

                    End While

                    dr.Close()
                Else
                    Upload.Visible = True
                    options.Visible = False
                End If

                chkUnzip.Text = GetLanguage("F_UnzipFile")
                cmdCancel.Text = GetLanguage("return")
                If Session("RootDir") <> "" Then
                    lblRootDir.Text = Session("RootDir") & Session("RelativeDir")
                    lblRootDir.Text = Replace(lblRootDir.Text, GetAbsoluteServerPath(Request), "")
                    lblRootDir.Text = "/" + Replace(lblRootDir.Text, "\", "/")
                End If
            End If
            cmdCancel.NavigateUrl = CType(Session("UrlReferrer"), String)
        End Sub
        Private Function CheckQuota() As Boolean
            ' SendToLog("CheckQuota", Context)
            CheckQuota = True
            Zrequest = New GalleryRequest(obj.ModuleID)
            Dim StrFolder As String
            Dim SpaceUsed As Double
            Dim objAdmin As New AdminDB()
            StrFolder = HttpContext.Current.Request.MapPath(Zrequest.GalleryConfig.RootURL)
            SpaceUsed = objAdmin.GetdirectorySpaceUsed(StrFolder) / 1048576
            If Zrequest.GalleryConfig.Quota <> 0 Then
                lblMessage.Text = "<p>" & GetLanguage("Gal_QuotaInfo1") + "</p>"
                lblMessage.Text = Replace(lblMessage.Text, "{Quota}", Format(Zrequest.GalleryConfig.Quota, "#,##0.00"))
                lblMessage.Text = Replace(lblMessage.Text, "{SpaceUsed}", Format(SpaceUsed, "#,##0.00"))
                lblMessage.Text = Replace(lblMessage.Text, "{SpaceLeft}", Format(Zrequest.GalleryConfig.Quota - SpaceUsed, "#,##0.00"))
                If ((SpaceUsed) >= Zrequest.GalleryConfig.Quota) Then
                    lblMessage.Text += "<p>" & GetLanguage("Gal_QuotaInfo2") + "</p>"
                    Return False
                End If
            Else
                lblMessage.Text = "<p>" + Replace(GetLanguage("Gal_QuotaInfo"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00")) + "</p>"
            End If
        End Function
        Public Function SetMaxSize() As String
            'SendToLog("SetMaxSize", Context)
            ' System.Configuration.ConfigurationManager()
            Dim instance As Object = ConfigurationManager.GetSection("system.web/httpRuntime")
            'Dim instance As Object = Config.GetSection("system.web/httpRuntime")
            instance = CType(instance, HttpRuntimeSection)
            Dim MaxSize As Integer = instance.MaxRequestLength * 1024
            If obj.IsGall Then
                If MaxSize > Zrequest.GalleryConfig.MaxFileSize * 1024 Then
                    MaxSize = Zrequest.GalleryConfig.MaxFileSize * 1024
                End If
            End If
            Return MaxSize.ToString
        End Function
        Public Function GetFileMaxSize() As String
            'SendToLog("GetFileMaxSize", Context)
            'Dim Config As System.Configuration.Configuration = WebConfigurationManager.OpenWebConfiguration("~")
            Dim instance As Object = ConfigurationManager.GetSection("system.web/httpRuntime")
            instance = CType(instance, HttpRuntimeSection)
            Dim MaxSize As Integer = instance.MaxRequestLength
            If obj.IsGall Then
                If MaxSize > Zrequest.GalleryConfig.MaxFileSize Then
                    MaxSize = Zrequest.GalleryConfig.MaxFileSize
                End If
            End If
            Dim DmaxSize As Double
            DmaxSize = MaxSize / 1024
            Return GetLanguage("Gal_MaxFileSize").Replace("{MaxFileSize}", Format(DmaxSize, "#,##0.00"))
        End Function

        Public Function GetFileType() As String
            'SendToLog("GetFileType", Context)
            Dim TempExtString As String = ""
            Dim TempExt() As String
            Dim i As Integer
            TempExt = Split(obj.Extension, ",")
            For i = 0 To TempExt.Length - 1
                TempExtString += "*." + TempExt(i)
                If i < TempExt.Length - 1 Then TempExtString += ";"
            Next
            Return TempExtString
        End Function

        Public Function GetMulti() As String
            'SendToLog("GetMulti", Context)
            Return obj.MultiFile.ToString.ToLower
        End Function

        Public Function GetQueueLimit() As String
            'SendToLog("GetQueueLimit", Context)
            Return obj.MaxFile.ToString
        End Function

        Public Function GetMaxQueue() As String
            'SendToLog("GetMaxQueue", Context)
            Return GetLanguage("MaxQueue").Replace("{MaxQueue}", GetQueueLimit)
        End Function

 
 

        Public Function GetPathCrypTo() As String
            'SendToLog("GetPathCrypto", Context)
            ' chkUnzip.AutoPostBack = obj.CUnzip
            ' chkUnzip.Enabled = obj.CUnzip
            ' chkUnzip.Checked = obj.Unzip


            Dim builder As New StringBuilder()
            Dim s As New XmlSerializer(obj.[GetType]())
            Using writer As New StringWriter(builder)
                s.Serialize(writer, obj)
            End Using
            Dim myInfo As String = builder.ToString()
            Dim serializer As New JavaScriptSerializer()
            Dim Json As String = serializer.Serialize(obj)



            Dim vsKey As String = ""
            vsKey = Guid.NewGuid().ToString()
            '	Store the view state into database
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Try
                myConnection.Open()

                Dim myCommand As New SqlCommand("SqlViewStateProvider_SaveViewState", myConnection)

                myCommand.CommandType = CommandType.StoredProcedure

                '	Key
                myCommand.Parameters.Add("@vsKey", SqlDbType.NVarChar, 100)
                myCommand.Parameters("@vsKey").Value = vsKey

                '	Serialized ViewState
                myCommand.Parameters.Add("@vsValue", SqlDbType.NText, Json.Length)
                myCommand.Parameters("@vsValue").Value = Json

                myCommand.ExecuteNonQuery()

                myCommand.Dispose()

            Finally
                myConnection.Dispose()
            End Try

            Return vsKey
        End Function

        Private Sub chkUnzip_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUnzip.CheckedChanged
            If obj.CUnzip Then
                obj.Unzip = chkUnzip.Checked
            End If
            Session("UploadInfo") = obj
        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

            Dim admin As New AdminDB()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim item As ListItem

            ' Construct Authorized View Roles
            Dim UploadRoles As String = ""
            For Each item In chkUploadRoles.Items
                If item.Selected Then
                    UploadRoles = UploadRoles & item.Value & ";"
                End If
            Next item
            If UploadRoles <> "" Then
                If InStr(1, UploadRoles, _portalSettings.AdministratorRoleId.ToString & ";") = 0 Then
                    UploadRoles += _portalSettings.AdministratorRoleId.ToString & ";"
                End If
            End If

            admin.UpdatePortalSetting(_portalSettings.PortalId, "uploadroles", UploadRoles)

        End Sub

        Private Sub cmdSynchronize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSynchronize.Click
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

            If Not (Request.Params("hostpage") Is Nothing) Then
                objAdmin.SynchronizeFiles(-1, Request.MapPath(glbSiteDirectory))
            Else
                objAdmin.SynchronizeFiles(_portalSettings.PortalId, Request.MapPath(_portalSettings.UploadDirectory))
            End If

        End Sub


        Private Sub cmdCancelOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelOptions.Click
            Session("UploadInfo") = Nothing
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub


    End Class

End Namespace