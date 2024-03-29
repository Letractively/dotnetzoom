'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by Ren� Boulard ( http://www.reneboulard.qc.ca)'
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


Imports System
Imports System.Diagnostics
Imports System.Configuration
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection

Namespace DotNetZoom

    Public Class ISyncFunction

        Dim Del As MyAsyncDelegate
        Private _SessionId As String
        Private _UserId As Integer
        Private _Request As HttpRequest
        Private _portalSettings As PortalSettings
        Private _DBConnectionString As String
        Private _PortalUserOnline As Boolean

        Public Property PortalUserOnline() As Boolean
            Get
                Return _PortalUserOnline
            End Get
            Set(ByVal Value As Boolean)
                _PortalUserOnline = Value
            End Set
        End Property

        Public Property DBConnectionString() As String
            Get
                Return _DBConnectionString
            End Get
            Set(ByVal Value As String)
                _DBConnectionString = Value
            End Set
        End Property


        Public Property SessionID() As String
            Get
                Return _SessionId
            End Get
            Set(ByVal Value As String)
                _SessionId = Value
            End Set
        End Property

        Public Property UserID() As Integer
            Get
                Return _UserId
            End Get
            Set(ByVal Value As Integer)
                _UserId = Value
            End Set
        End Property

        Public Property Request() As HttpRequest
            Get
                Return _Request
            End Get
            Set(ByVal Value As HttpRequest)
                _Request = Value
            End Set
        End Property

        Public Property PortalSetting() As PortalSettings
            Get
                Return _portalSettings
            End Get
            Set(ByVal Value As PortalSettings)
                _portalSettings = Value
            End Set
        End Property
        Sub New(ByVal SessionID As String, ByVal UserID As Integer, ByVal Request As HttpRequest, ByVal portalsetting As PortalSettings, ByVal DBConnectionString As String, ByVal PortalUserOnline As Boolean)
            _SessionId = SessionID
            _UserId = UserID
            _Request = Request
            _portalSettings = portalsetting
            _DBConnectionString = DBConnectionString
            _PortalUserOnline = PortalUserOnline
        End Sub

        Public Sub DoIt()

            Del = New MyAsyncDelegate(AddressOf MyWorker)
            Del.BeginInvoke(Nothing, Nothing)

        End Sub

        Public Delegate Sub MyAsyncDelegate()

        Private Sub AddSiteLog(ByVal PortalId As Integer, Optional ByVal UserId As Integer = -1, Optional ByVal Referrer As String = "", Optional ByVal URL As String = "", Optional ByVal UserAgent As String = "", Optional ByVal UserHostAddress As String = "", Optional ByVal UserHostName As String = "", Optional ByVal TabId As Integer = -1, Optional ByVal AffiliateId As Integer = -1)
            Dim myConnection As New SqlConnection(DBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, IIf(UserId <> -1, UserId, SqlInt16.Null), IIf(Referrer <> "", Referrer, SqlInt16.Null), IIf(URL <> "", URL, SqlInt16.Null), IIf(UserAgent <> "", UserAgent, SqlInt16.Null), IIf(UserHostAddress <> "", UserHostAddress, SqlInt16.Null), IIf(UserHostName <> "", UserHostName, SqlInt16.Null), IIf(TabId <> -1, TabId, SqlInt16.Null), IIf(AffiliateId <> -1, AffiliateId, SqlInt16.Null)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

        Private Sub UpdateSession(ByVal PortalId As Integer, ByVal SessionId As String, ByVal Location As String, ByVal TabId As Integer, ByVal IP As String, ByVal Browser As String, ByVal UserId As Integer)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(DBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, SessionId, Location, TabId, IP, Browser, IIf(UserId <> -1, UserId, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub

        Private Sub MyWorker()
            If PortalUserOnline Then
                UpdateSession(_portalSettings.PortalId, SessionID, _portalSettings.ActiveTab.TabName, _portalSettings.ActiveTab.TabId, Request.UserHostAddress, Request.Browser.Type, UserID)
            End If

            If _portalSettings.SiteLogHistory <> 0 Then
                Dim URLReferrer As String = ""
                If Not Request.UrlReferrer Is Nothing Then
                    URLReferrer = Request.UrlReferrer.ToString()
                End If
                Dim AffiliateId As Integer = -1
                If Not Request.Params("AffiliateId") Is Nothing Then
                    If IsNumeric(Request.Params("AffiliateId")) Then
                        AffiliateId = Int32.Parse(Request.QueryString("AffiliateId"))
                    End If
                End If
                AddSiteLog(_portalSettings.PortalId, UserID, URLReferrer, Request.Url.ToString(), Request.UserAgent, Request.UserHostAddress, Request.UserHostName, _portalSettings.ActiveTab.TabId, AffiliateId)
            End If


        End Sub

    End Class


    Public Class BasePage

        Inherits System.Web.UI.Page

        Public Const VIEW_STATE_FIELD_NAME As String = "__PAGEVIEWSTATE"


        Private _ModuleCommunicators As New ModuleCommunicators()
        Private _ModuleListeners As New ModuleListeners()
        Private _formatter As LosFormatter = Nothing                    'LosFormatter

        '//Determine if server side is enabled or not from HostSettings

        Public ReadOnly Property ServerSideEnabled() As Integer
            Get
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                Dim DefServerSideViewState As String = ""
                If PortalSettings.GetHostSettings.ContainsKey("ViewState") Then
                    DefServerSideViewState = PortalSettings.GetHostSettings("ViewState").ToString
                End If


                Select Case DefServerSideViewState
                    Case "M"
                        ServerSideEnabled = 1
                    Case "S"
                        ServerSideEnabled = 2
                    Case Else
                        ServerSideEnabled = 0
                End Select
            End Get
        End Property



        Public ReadOnly Property ModuleCommunicators() As ModuleCommunicators
            Get
                Return _ModuleCommunicators
            End Get
        End Property

        Public ReadOnly Property ModuleListeners() As ModuleListeners
            Get
                Return _ModuleListeners
            End Get
        End Property



        Public Sub New()
        End Sub 'New




        Protected Overrides Function LoadPageStateFromPersistenceMedium() As Object

            Select Case ServerSideEnabled

                Case 1
                    ' en memoire
                    Return LoadViewState()
                Case 2
                    ' SQL
                    Return LoadViewStateSQL()
                Case Else
                    Return MyBase.LoadPageStateFromPersistenceMedium()
            End Select
        End Function


        Protected Overrides Sub SavePageStateToPersistenceMedium(ByVal viewState As Object)

            Select Case ServerSideEnabled

                Case 1
                    ' en memoire
                    SaveViewState(viewState)
                Case 2
                    ' SQL
                    SaveViewStateSQL(viewState)
                Case Else
                    MyBase.SavePageStateToPersistenceMedium(viewState)
            End Select
        End Sub


        Private Overloads Function LoadViewStateSQL() As Object

            If (_formatter Is Nothing) Then
                _formatter = New LosFormatter
            End If

            If (Request.Form(VIEW_STATE_FIELD_NAME) Is Nothing) Then
                '//Did not see form field for viewstate, return null to try to continue (could log event...)
                Exit Function
            End If
            Dim vsKey As String = Request.Form(VIEW_STATE_FIELD_NAME)


            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Try
                myConnection.Open()

                Try
                    Dim myCommand As New SqlCommand("SqlViewStateProvider_GetViewState", myConnection)
                    Try
                        myCommand.CommandType = CommandType.StoredProcedure

                        '	Key
                        myCommand.Parameters.Add("@vsKey", SqlDbType.NVarChar, 100)
                        myCommand.Parameters("@vsKey").Value = vsKey

                        Dim _viewState As Object = myCommand.ExecuteScalar()
                        '	Deserialize the view state string into object
                        Return _formatter.Deserialize(_viewState.ToString())
                    Finally
                        myCommand.Dispose()
                    End Try
                Catch ex As Exception
                    LogMessage(HttpContext.Current.Request, "Erreur LoadViewState, " + ex.Message)
                    Response.Redirect(Request.RawUrl, True)
                End Try
            Finally
                myConnection.Dispose()
            End Try
        End Function ' LoadViewStateSQL



        Private Overloads Function LoadViewState() As Object

            Dim lRequestNumber As Long = 0

            If (_formatter Is Nothing) Then
                _formatter = New LosFormatter
            End If

            '//Check if the client has form field that stores request key
            If (Request.Form(VIEW_STATE_FIELD_NAME) Is Nothing) Then
                '//Did not see form field for viewstate, return null to try to continue (could log event...)
                Exit Function
            End If

            '//Make sure it can be converted to request number (in case of corruption)
            Try
                lRequestNumber = Convert.ToInt64(Request.Form(VIEW_STATE_FIELD_NAME))
            Catch
                '//Could not covert to request number, return null (could log event...)
                Exit Function
            End Try
            '//Get the viewstate for this page
            Dim _viewState As String
            _viewState = viewStateSvrMgr.GetViewStateSvrMgr().GetViewState(lRequestNumber)


            '//If find the viewstate on the server, convert it so ASP.Net can use it
            If (_viewState Is Nothing) Then
                Exit Function
            End If
            LoadViewState = _formatter.Deserialize(_viewState)

        End Function


        Private Overloads Sub SaveViewStateSQL(ByVal viewState As Object)

            If (_formatter Is Nothing) Then
                _formatter = New LosFormatter
            End If

            '//Save the viewstate information
            Dim _viewState As New System.Text.StringBuilder
            Dim _writer As New System.IO.StringWriter(_viewState)

            _formatter.Serialize(_writer, viewState)

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
                myCommand.Parameters.Add("@vsValue", SqlDbType.NText, _writer.ToString().Length)
                myCommand.Parameters("@vsValue").Value = _writer.ToString()

                myCommand.ExecuteNonQuery()

                myCommand.Dispose()

            Finally
                myConnection.Dispose()
            End Try



            _writer.Close()

            ClientScript.RegisterHiddenField(VIEW_STATE_FIELD_NAME, vsKey)

        End Sub ' SaveViewStateSQL

        Private Overloads Sub SaveViewState(ByVal viewState As Object)
            If (_formatter Is Nothing) Then
                _formatter = New LosFormatter
            End If

            '//Save the viewstate information
            Dim _viewState As New System.Text.StringBuilder
            Dim _writer As New System.IO.StringWriter(_viewState)

            _formatter.Serialize(_writer, viewState)


            Dim lRequestNumber As Long
            lRequestNumber = viewStateSvrMgr.GetViewStateSvrMgr().SaveViewState(_viewState.ToString())

            '//Need to register the viewstate hidden field (must be present or postback things don't 
            '// work, we use in our case to store the request number)
            ClientScript.RegisterHiddenField(VIEW_STATE_FIELD_NAME, lRequestNumber.ToString())


        End Sub


        Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
            If HttpContext.Current.Request.Browser.EcmaScriptVersion.Major > 0 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "dnzscript", "<script type=""text/javascript"" src=""" & glbPath & "javascript/dnzscript1.js""></script>")
                If Not Request.Form("scrollLeft") Is Nothing Then
                    ClientScript.RegisterHiddenField("scrollLeft", Request.Form("scrollLeft"))
                    ClientScript.RegisterHiddenField("scrollTop", Request.Form("scrollTop"))
                Else
                    ClientScript.RegisterHiddenField("scrollLeft", String.Empty)
                    ClientScript.RegisterHiddenField("scrollTop", String.Empty)
                End If
            End If
        End Sub


        Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
            Dim stringWriter As System.IO.StringWriter = New System.IO.StringWriter
            Dim htmlWriter As HtmlTextWriter = New HtmlTextWriter(stringWriter)
            MyBase.Render(htmlWriter)
            Dim html As String = stringWriter.ToString()
            Dim StartPoint As Integer
            Dim EndPoint As Integer
            Dim ViewStateInput As String = ""
            Dim FormEndStart As Integer

            Try
                html = Regex.Replace(html, "id=""\r\n__VIEWSTATE""|id=""__VIEWSTATE""|id=""__VIEWSTATEENCRYPTED""|id=""__EVENTTARGET""|id=""__EVENTARGUMENT""|id=""__LASTFOCUS""|id=""__PAGEVIEWSTATE""|id=""__EVENTVALIDATION""", "", RegexOptions.IgnoreCase)
            Catch ex As ArgumentException
                'Syntax error in the regular expression
            End Try


            StartPoint = html.IndexOf("<input type=""hidden"" name=""__VIEWSTATE""")

            ' Only do it if there is a viewstate
            If StartPoint > 0 Then
                EndPoint = html.IndexOf("/>", StartPoint) + 2
                ViewStateInput = html.Substring(StartPoint, EndPoint - StartPoint)
                html = html.Remove(StartPoint, EndPoint - StartPoint)
            End If

            ' ajout par rene boulard pour enlever les espaces blanc
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim DefRemWS As String = ""
            If PortalSettings.GetHostSettings.ContainsKey("WhiteSpace") Then
                DefRemWS = PortalSettings.GetHostSettings("WhiteSpace").ToString
            End If

            'Convert for HTML 4 Compatibility, if not solpart menu because of xml
            If Not html.IndexOf("/controls/SolpartMenu/spmenu.js") > 0 Then
                html = html.Replace(""" />", """>")
                html = html.Replace("<br />", "<br>")
            End If
            If glbPath <> "/" Then
                html = html.Replace("url('/images/", "url('" + glbPath + "images/")
                html = html.Replace("src=""/images/", "src=""" + glbPath + "images/")
                html = html.Replace("src=""../images/", "src=""" + glbPath + "images/")
            End If


            If DefRemWS <> "" Then
                Select Case DefRemWS
                    Case "E"
                        ' white space only
                        Try
                            html = Regex.Replace(html, "(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&nbsp;)\s{2,}(?=[<])", "")
                        Catch ex As ArgumentException
                            'Syntax error in the regular expression
                        End Try

                    Case "H"
                        ' html
                        Try
                            html = Regex.Replace(html, "(?<=<tr )id=""\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{2,40}\b""|(?<=<img )id=""\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{2,40}\b""|(?<=<title )id=""\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{2,40}\b""|(?<=<a )id=""\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{2,40}\b""|(?<=<td )id=""\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{2,40}\b""", "", RegexOptions.IgnoreCase)
                        Catch ex As ArgumentException
                            'Syntax error in the regular expression
                        End Try


                    Case "T"
                        ' all
                        Try
                            html = Regex.Replace(html, "(?<=<tr )id=""\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{2,40}\b""|(?<=<img )id=""\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{2,40}\b""|(?<=<title )id=""\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{2,40}\b""|(?<=<a )id=""\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{2,40}\b""|(?<=<td )id=""\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{2,40}\b""", "", RegexOptions.IgnoreCase)
                        Catch ex As ArgumentException
                            'Syntax error in the regular expression
                        End Try
                        Try
                            html = Regex.Replace(html, "(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&nbsp;)\s{2,}(?=[<])", "")
                        Catch ex As ArgumentException
                            'Syntax error in the regular expression
                        End Try
                    Case Else
                        ' nothing to do here
                End Select
            End If


            'Only do it if viewstateexist
            If StartPoint > 0 Then
                FormEndStart = html.IndexOf("</form>")
                html = html.Insert(FormEndStart, ViewStateInput)
                html = html.Replace("/></form>", "></form>")
            End If


            If html.IndexOf("return escape") > 10 Then
                ' inject script if onmouseover is there in page
                ' put in the tooltip contener
                Dim _Setting As Hashtable = PortalSettings.GetSiteSettings(_portalSettings.PortalId)
                Dim arrContainer() As String = {" ", " "}
                If _Setting("ToolTipcontainer") <> "" Then
                    arrContainer = SplitContainer(_Setting("ToolTipcontainer"), _portalSettings.UploadDirectory, IIf(_Setting("ToolTipcontainerAlignment") <> "", _Setting("ToolTipcontainerAlignment"), ""), IIf(_Setting("ToolTipcontainerColor") <> "", _Setting("ToolTipcontainerColor"), ""), IIf(_Setting("ToolTipcontainerBorder") <> "", _Setting("ToolTipcontainerBorder"), ""))
                    arrContainer(0) = "var before = '" + tooltipSafe(arrContainer(0)) + "';"
                    arrContainer(1) = "var after = '" + tooltipSafe(arrContainer(1)) + "';"
                Else
                    arrContainer(0) = "var before = '<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""><tr><td width=""13""><img border=""0"" src=""" & glbPath & "javascript/t-l.gif"" WIDTH=""13"" HEIGHT=""13""><\/td><td background=""" & glbPath & "javascript/b.gif""><\/td><td width=""13""><img border=""0"" src=""" & glbPath & "javascript/t-r.gif"" WIDTH=""13"" HEIGHT=""13""><\/td><\/tr><tr><td width=""13"" background=""" & glbPath & "javascript/b.gif""><\/td><td class=""Normal"" background=""" & glbPath & "javascript/b.gif"">';" + vbCrLf
                    arrContainer(1) = "var after = '<\/td><td width=""13"" background=""" & glbPath & "javascript/b.gif""><\/td><\/tr><tr><td width=""13""><img border=""0"" src=""" & glbPath & "javascript/b-l.gif"" WIDTH=""13"" HEIGHT=""13""><\/td><td background=""" & glbPath & "javascript/b.gif""><\/td><td width=""13""><img border=""0"" src=""" & glbPath & "javascript/b-r.gif"" WIDTH=""13"" HEIGHT=""13""><\/td><\/tr><\/table>';" + vbCrLf
                End If


                FormEndStart = html.IndexOf("</form>")
                html = html.Insert(FormEndStart + 7, vbCrLf + "<script type=""text/javascript"">" + vbCrLf + "<!--" + vbCrLf + arrContainer(0) + vbCrLf + arrContainer(1) + vbCrLf + "// -->" + vbCrLf + "</script>" + vbCrLf + "<script type=""text/javascript"" src=""" + glbPath + "javascript/wz_tooltip.js""></script>")
            End If

            If Not HttpContext.Current.Items("FOCUS") Is Nothing Then
                FormEndStart = html.IndexOf("</form>")
                html = html.Insert(FormEndStart + 7, vbCrLf + HttpContext.Current.Items("FOCUS"))
            End If

            FormEndStart = html.IndexOf("<body")
            If html.IndexOf("<script src=""http://maps.google.com/maps?") > 10 Then
                ' google map
                html = html.Insert(FormEndStart + 5, " onload=""javascript:load();javascript:SmartScroller_Scroll()"" onunload=""javascript:GUnload()""")
            Else
                html = html.Insert(FormEndStart + 5, " onload=""javascript:SmartScroller_Scroll()""")

            End If
            writer.Write(html)
        End Sub

        Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
            Dim LastError As Exception
            LastError = Server.GetLastError.GetBaseException()
            If PortalSettings.GetHostSettings("EnableErrorReporting") <> "N" Then
                Dim ErrorMessage As New StringBuilder


                If Not LastError Is Nothing Then
                    ErrorMessage.Append(LastError.Message + vbCrLf)
                    ErrorMessage.Append(LastError.StackTrace + vbCrLf)
                    ErrorMessage.Append(LastError.Source + vbCrLf)
                End If

                SendNotification(PortalSettings.GetHostSettings("HostEmail"), PortalSettings.GetHostSettings("HostEmail2"), "", "Page_Error", ErrorMessage.ToString, "")
            End If

            If File.Exists(Server.MapPath(glbPath + "erreur" + GetLanguage("N") + ".htm")) Then
                ' read script file for version
                Dim objStreamReader As StreamReader
                objStreamReader = File.OpenText(Server.MapPath(glbPath + "erreur" + GetLanguage("N") + ".htm"))
                Dim strHTML As String = objStreamReader.ReadToEnd
                objStreamReader.Close()
                Response.Write(strHTML)
            End If

            LogErrorMessage(Request, LastError)
            Context.ClearError()
            Response.End()
        End Sub


        Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
            If Page.IsPostBack = False Then
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                Dim UserId As Integer = -1
                Dim PortalUserOnline As Boolean = False
                If Request.IsAuthenticated Then
                    UserId = CType(Context.User.Identity.Name, Integer)
                End If

                If PortalSettings.GetSiteSettings(_portalSettings.PortalId)("PortalUserOnline") <> "NO" And Not Request.Browser.Crawler Then
                    PortalUserOnline = True
                End If
                If PortalUserOnline Or _portalSettings.SiteLogHistory <> 0 Then
                    Dim ISyncLog As New ISyncFunction(Session.SessionID, UserId, Request, _portalSettings, GetDBConnectionString(), PortalUserOnline)
                    ISyncLog.DoIt()
                End If
            End If
            MyBase.OnInit(e)
        End Sub


        Protected Overrides Sub OnUnload(ByVal e As System.EventArgs)
            MyBase.OnUnload(e)
        End Sub


        Public Function LoadModule(ByVal virtualPath As String) As System.Web.UI.Control
            Dim returnData As System.Web.UI.Control = MyBase.LoadControl(virtualPath)

            ' Check and see if the module implements IModuleCommunicator
            If TypeOf returnData Is IModuleCommunicator Then
                Me.Add(CType(returnData, IModuleCommunicator))
            End If
            ' Check and see if the module implements IModuleCommunicator
            If TypeOf returnData Is IModuleListener Then
                Me.Add(CType(returnData, IModuleListener))
            End If

            Return returnData
        End Function 'LoadModule


        Private Overloads Function Add(ByVal item As IModuleCommunicator) As Integer
            Dim returnData As Integer = _ModuleCommunicators.Add(item)

            Dim i As Integer
            For i = 0 To _ModuleListeners.Count - 1
                AddHandler item.ModuleCommunication, AddressOf _ModuleListeners(i).OnModuleCommunication
            Next i
            Return returnData
        End Function 'Add


        Private Overloads Function Add(ByVal item As IModuleListener) As Integer
            Dim returnData As Integer = _ModuleListeners.Add(item)

            Dim i As Integer
            For i = 0 To _ModuleCommunicators.Count - 1
                AddHandler _ModuleCommunicators(i).ModuleCommunication, AddressOf item.OnModuleCommunication
            Next i

            Return returnData
        End Function 'Add

        Private Function tooltipSafe(ByVal strText As String) As String
            'returns safe code for preloading in the tooltip container
            Dim tmpString As String
            tmpString = Trim(strText)
            tmpString = tmpString.Replace("\", "\\")
            tmpString = tmpString.Replace("'", "\'")
            tmpString = tmpString.Replace("/", "\/")
            'replace carriage returns & line feeds & tab and ff
            tmpString = Replace(tmpString, Chr(11), "\t")
            tmpString = Replace(tmpString, Chr(10), "\n")
            tmpString = Replace(tmpString, Chr(13), "\r")
            tmpString = Replace(tmpString, Chr(12), "\f")
            Return tmpString
        End Function



    End Class 'BasePage

End Namespace