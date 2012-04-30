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
	Public Class TAGFileUploadDialog
		Inherits System.Web.UI.Page
        Protected WithEvents CanUpload As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Literal
        Protected WithEvents chkUnzip As System.Web.UI.WebControls.CheckBox
        Protected WithEvents StyleSheet As System.Web.UI.WebControls.Literal
        Protected WithEvents InfoText As System.Web.UI.WebControls.Literal

        Private Zrequest As GalleryRequest
        Private obj As New UploadInfo()

#Region " Web Form Designer Generated Code "

		'This call is required by the Web Form Designer.
		<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

		End Sub

		Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
			'CODEGEN: This method call is required by the Web Form Designer
			'Do not modify it using the code editor.
			InitializeComponent()

            ' SendToLog("StartIni", Context)
            If Not IsNothing(Session("UploadInfo")) And Request.IsAuthenticated Then
                obj = Session("UploadInfo")
            Else
                HttpContext.Current.Response.StatusCode = 403
                HttpContext.Current.Response.StatusDescription = "Forbidden"
                HttpContext.Current.Response.End()
            End If

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim SpaceUsed As Double

            If Not PortalSecurity.IsSuperUser And obj.IsHost Then
                HttpContext.Current.Response.StatusCode = 403
                HttpContext.Current.Response.StatusDescription = "Forbidden"
                HttpContext.Current.Response.End()
            End If


            Dim objAdmin As New AdminDB()
            Dim strFolder As String = ""
            If PortalSecurity.IsSuperUser And Not (Request.Params("hostpage") Is Nothing) Then
                strFolder = Request.MapPath(glbSiteDirectory)
            Else
                strFolder = Request.MapPath(_portalSettings.UploadDirectory)
            End If


            SpaceUsed = objAdmin.GetdirectorySpaceUsed(strFolder)
            SpaceUsed = SpaceUsed / 1048576


            If (Request.Params("hostpage") Is Nothing) Then
                If _portalSettings.HostSpace = 0 Then
                    lblMessage.Text = "<p>" + Replace(GetLanguage("Gal_PortalQuota"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00")) + "</p>"
                Else
                    lblMessage.Text = "<p>" + Replace(GetLanguage("Gal_PortalQuota1"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00")) + "</p>"
                    lblMessage.Text = Replace(lblMessage.Text, "{Quota}", Format(_portalSettings.HostSpace, "#,##0.00"))
                    lblMessage.Text = Replace(lblMessage.Text, "{SpaceLeft}", Format(_portalSettings.HostSpace - SpaceUsed, "#,##0.00"))
                End If
            Else
                lblMessage.Text = "<p>" + Replace(GetLanguage("Gal_PortalQuota"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00")) + "</p>"
            End If


            If (((SpaceUsed) <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Or (Not (Request.Params("hostpage") Is Nothing)) Then
            Else
                CanUpload.Visible = False
                lblMessage.Text += "<p>" + GetLanguage("Gal_PortalQuota2") + "</p><p><a href=""javascript:handleOK();"" class=""CommandButton"">" + GetLanguage("return") + "</a></p>"
            End If

            If obj.IsGall Then
                If Not CheckQuota() Then
                    CanUpload.Visible = False
                    lblMessage.Text += "<p><a href=""javascript:handleOK();"" class=""CommandButton"">" + GetLanguage("return") + "</a></p>"
                End If
            End If


            If Session("message") <> "" Then
                lblMessage.Text += "<br>" + Session("message")
                Session("message") = ""
            End If
        End Sub

#End Region

		Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ' SendToLog("Page_load", Context)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"

            If Page.IsPostBack = False Then
                Dim ObjAdmin As New AdminDB
                InfoText.Text = ObjAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "DisplayHelp_TAGFileUploadDialog")
                chkUnzip.AutoPostBack = obj.CUnzip
                chkUnzip.Enabled = obj.CUnzip
                chkUnzip.Checked = obj.Unzip
                chkUnzip.Text = "<label for=""" & chkUnzip.ClientID & """>" & GetLanguage("UnZipFile") & "</label>"
            End If

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

        Private Sub SendToLog(ByVal LogIt As String, ByVal Context As HttpContext)
            Dim objStream As StreamWriter
            objStream = File.AppendText(Context.Server.MapPath(glbPath + "database/upload.log"))
            objStream.WriteLine("UploadDI : " + DateTime.Now.ToString("yyyy\-MM\-dd HH\:mm\:ss") + vbCrLf + LogIt)
            objStream.Close()
        End Sub


        Private Sub chkUnzip_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUnzip.CheckedChanged
            If obj.CUnzip Then
                obj.Unzip = chkUnzip.Checked
            End If
            Session("UploadInfo") = obj
        End Sub


     



    End Class
End Namespace