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

Imports System.Security
Imports System.Security.Principal
Imports System.Web.Security
Imports System.IO
Imports System.XML
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Data.SqlTypes
Imports System.Reflection
Imports System.Web.Configuration
Imports System.Collections.Specialized



Namespace DotNetZoom

   Public Class TOPGlobal

        Inherits System.Web.HttpApplication



        Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)

            If Application("SetContext") = "Maintenance" And Request.Params("Maintenance") Is Nothing Then
                HttpContext.Current.RewritePath(glbPath() + "admin/Portal/update.aspx", String.Empty, HttpContext.Current.Request.QueryString.ToString())
            Else



                If Not Application("error403") Is Nothing Then
                    Dim TempError As String = Application("error403")
                    Application("error403") = Nothing
                    SendHttpException("403", "Forbidden", Request, TempError)
                End If


                If Not Application(Request.UserHostAddress) Is Nothing Then
                    If CType(Application(Request.UserHostAddress), DateTime) > DateTime.Now() Then
                        Response.StatusCode = "403"
                        Response.StatusDescription = "Forbidden"
                        Response.Write("<html><head><title>Forbidden</title></head><body bgcolor=white text=black>Forbidden : " & Request.UserHostAddress & " IP address rejected</body></html>")
                        Response.End()
                    Else
                        Application(Request.UserHostAddress) = Nothing
                    End If
                End If


                Dim context As HttpContext = HttpContext.Current
                Dim StrExtension As String = ""

                Dim filePath As String = context.Request.FilePath
                If InStrRev(filePath, "/controls/crypto.ashx") > 0 Then
                    Dim CryptoText As String = Left(filePath, InStrRev(filePath, "/controls/crypto.ashx") - 1)
                    CryptoText = Right(CryptoText, CryptoText.Length - glbPath.Length)
                    Dim objSecurity As New DotNetZoom.PortalSecurity()
                    Dim dnPath As String
                    dnPath = objSecurity.DecryptRijndael(context.Application("cryptokey"), CryptoText)
                    Try
                        Dim dnFile As System.IO.FileInfo = New System.IO.FileInfo(context.Request.MapPath(dnPath))
                        'Get If-Modified-Since header from request.
                        Dim sIfModifiedSince As String = context.Request.Headers("If-Modified-Since")
                        Dim IfModifiedSince As Date = DateTime.Now()
                        'Convert the header To date.
                        If sIfModifiedSince <> String.Empty Then
                            IfModifiedSince = DateTime.Parse(sIfModifiedSince)
                        End If

                        'Get a time when the file was last modified.
                        Dim LastModified As Date = dnFile.LastWriteTime

                        'round the date To seconds.
                        LastModified = New Date(LastModified.Year, LastModified.Month, LastModified.Day, _
                        LastModified.Hour, LastModified.Minute, LastModified.Second)
                        If LastModified <> IfModifiedSince Then '200 OK - file was modified.
                            StrExtension = Replace(System.IO.Path.GetExtension(dnFile.Name), ".", "")
                            ' Only send a file if proper extension
                            If InStr(1, "," & DotNetZoom.PortalSettings.GetHostSettings("FileExtensions").ToString.ToUpper, "," & StrExtension.ToUpper) <> 0 Then
                                context.Response.AddHeader("Content-Length", dnFile.Length.ToString)
                                Dim strContentType As String = "application/octet-stream"
                                Select Case StrExtension.ToLower()
                                    Case "txt" : strContentType = "text/plain"
                                    Case "htm", "html" : strContentType = "text/html"
                                    Case "rtf" : strContentType = "text/richtext"
                                    Case "jpg", "jpeg" : strContentType = "image/jpeg"
                                    Case "gif" : strContentType = "image/gif"
                                    Case "bmp" : strContentType = "image/bmp"
                                    Case "png" : strContentType = "image/png"
                                    Case "mpg", "mpeg" : strContentType = "video/mpeg"
                                    Case "avi" : strContentType = "video/avi"
                                    Case "wmv" : strContentType = "video/x-ms-wmv"
                                    Case "pdf" : strContentType = "application/pdf"
                                    Case "doc", "dot" : strContentType = "application/msword"
                                    Case "csv", "xls", "xlt" : strContentType = "application/x-msexcel"
                                    Case "flv" : strContentType = "video/x-flv"
                                End Select
                                context.Response.ContentType = strContentType
                                context.Response.Cache.SetLastModified(dnFile.LastWriteTimeUtc)
                                context.Response.Cache.SetExpires(DateTime.Now.AddDays(365))
                                context.Response.WriteFile(dnPath)
                                context.Response.StatusCode = 200
                                context.Response.End()
                            Else
                                context.Response.StatusCode = "404"
                                context.Response.StatusDescription = "Not Found"
                                context.Response.End()
                            End If
                        Else '304 - file is Not modified.
                            context.Response.StatusCode = "304"
                            context.Response.StatusDescription = "Not Modified"
                            context.Response.End()
                        End If
                    Catch

                    End Try

                End If



                If Not Application("throttle") Is Nothing Then
                    Dim Trootle As Integer = CType(context.Cache(Request.UserHostAddress), Integer)
                    Dim TSpan As Integer = CType(Application("throttle"), Integer)
                    If Trootle = Nothing Then
                        context.Cache.Insert(Request.UserHostAddress, 1, Nothing, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(TSpan), Caching.CacheItemPriority.Low, Nothing)
                    Else
                        Trootle = Trootle + 1
                        context.Cache.Insert(Request.UserHostAddress, Trootle, Nothing, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(TSpan), Caching.CacheItemPriority.Low, Nothing)
                        If Trootle > 10 Then
                            Response.StatusCode = "503"
                            Response.StatusDescription = "Service Unavailable"
                            Response.Write("<html><head><title>Service Unavailable</title></head><body bgcolor=white text=black>Wait</body></html>")
                            Response.End()
                        End If
                    End If
                End If  'End Throttle


                If (Request.Path.IndexOf(Chr(92)) >= 0 Or _
                      System.IO.Path.GetFullPath(Request.PhysicalPath) <> Request.PhysicalPath) Then
                    SendHttpException("404", "Not Found", Request)
                End If


                Dim StrPhysicalPath As String = context.Request.PhysicalPath
                If InStr(1, StrPhysicalPath, ".") Then
                    StrExtension = Mid(StrPhysicalPath, InStrRev(StrPhysicalPath, ".")).ToLower
                End If

                If StrExtension <> ".axd" Then

                    HttpContext.Current.Items("DBName") = Nothing
                    HttpContext.Current.Items("ConnectionString") = Nothing

                    Dim tabId As Integer = 0
                    Dim DomainName As String = Nothing
                    Dim PortalAlias As String = Nothing
                    Dim PortalNumber As Integer = -1

                    If Not (Request.Params("crypto") Is Nothing) Then
                        ' check if crypto
                        Dim objSecurity As New DotNetZoom.PortalSecurity()
                        Dim cryptoout As String = objSecurity.Decrypt(Application("cryptokey"), Request.Params("crypto"))
                        HttpContext.Current.RewritePath(Request.Path.ToString(), String.Empty, cryptoout)
                    Else
                        If Not (Request.Params("linkclick") Is Nothing) Then
                            Dim objSecurity As New DotNetZoom.PortalSecurity()
                            Dim cryptoout As String = objSecurity.Decrypt(Application("cryptokey"), Request.Params("linkclick"))
                            context.Items.Add("linkclick", Request.Params("linkclick"))
                            HttpContext.Current.RewritePath(Request.Path.ToString(), String.Empty, cryptoout)
                        End If
                    End If

                    ' get tabId from querystring
                    If Not (Request.Params("tabid") Is Nothing) Then
                        If Request.Params("tabid") <> "" Then
                            If IsNumeric(Request.Params("tabid")) Then
                                tabId = Int32.Parse(Request.Params("tabid"))
                                ' LogMessage(context.Request, "tabid : " + tabId.ToString)
                            Else
                                Response.End()
                            End If
                        End If
                    End If

                    ' check params
                    If Not Request.Params("mid") Is Nothing Then
                        If Not IsNumeric(Request.Params("mid")) Then
                            Response.End()
                        End If
                    End If


                    If Not Request.Params("p") Is Nothing Then
                        DomainName = Request.Params("p")

                    Else
                        ' parse the Request URL into a Domain Name token
                        DomainName = GetDomainName(Request)
                    End If


                    ' alias parameter can be used to switch portals
                    If Not (Request.Params("alias") Is Nothing) Then
                        If PortalSettings.GetPortalByAlias(Request.Params("alias")) <> -1 Then
                            If InStr(1, Request.Params("alias"), DomainName, CompareMethod.Text) = 0 Then
                                Response.Redirect(GetPortalDomainName(Request.Params("alias"), Request), True)
                            Else
                                PortalAlias = Request.Params("alias")
                            End If
                        End If
                    End If

                    ' tabId uniquely identifies a Portal
                    If PortalAlias Is Nothing Then
                        If tabId <> 0 Then
                            PortalAlias = PortalSettings.GetPortalByTab(tabId, DomainName)
                        End If
                    End If

                    ' else use the domainname
                    If PortalAlias Is Nothing Then
                        PortalAlias = DomainName
                        tabId = 0 ' load the default tab
                    End If

                    ' LogMessage(context.Request, "PortalAlias : " + PortalAlias)
                    ' validate the alias
                    PortalNumber = PortalSettings.GetPortalByAlias(PortalAlias)

                    ' LogMessage(context.Request, "PortalNumber : " + PortalNumber.ToString)

                    If PortalNumber <> -1 Then
                        ' see if file name is not a tab
                        Dim StringFileName As String = Mid(Request.Path, InStrRev(Request.Path, "/") + 1).ToLower


                        Dim TempLanguageCode As String
                        TempLanguageCode = Replace(StringFileName, ".aspx", "")
                        If InStr(1, TempLanguageCode, ".") <> 0 Then
                            TempLanguageCode = Left(TempLanguageCode, InStrRev(TempLanguageCode, ".") - 1)
                        Else
                            If Not Request.Params("L") Is Nothing Then
                                TempLanguageCode = Request.Params("L")
                            Else
                                TempLanguageCode = ""
                            End If
                        End If



                        ' see if there is not a language in the file name
                        ' Get Default Language for Portal
                        Dim objAdmin As New AdminDB()
                        Dim LanguageHash As New Hashtable
                        Dim TempLanguage As String

                        Dim tsettings As Hashtable = PortalSettings.GetSiteSettings(PortalNumber)

                        Dim TempAuthLanguage As String = ""
                        If tsettings.ContainsKey("languageauth") Then
                            TempAuthLanguage = tsettings("languageauth")
                        End If


                        If tsettings("language") <> Nothing Then
                            ' use portal default language
                            TempLanguage = FindUserLanguage(tsettings("language"), TempLanguageCode, TempAuthLanguage)
                        Else
                            ' use default language
                            TempLanguage = FindUserLanguage("fr", TempLanguageCode, TempAuthLanguage)
                        End If
                        LanguageHash = objAdmin.GetlanguageSettings(TempLanguage)
                        If Not LanguageHash.ContainsKey("N") Then
                            LanguageHash.Add("N", TempLanguage)
                        End If
                        Response.AddHeader("Content-Language", TempLanguage)

                        ' Put in fckeditor language

                        If Not LanguageHash.ContainsKey("fckeditor_language") Then
                            If InStr(1, "ar;bg;bs;ca;cs;da;de;el;en;en-au;en-uk;eo;es;et;eu;fa;fi;fo;fr;gl;he;hi;hr;hu;it;ja;ko;lt;lv;mn;ms;nl;no;pl;pt;pt-br;ro;ru;sk;sl;sr;sr-latn;sv;th;tr;uk;vi;zh;zh-cn;", TempLanguage & ";") Then
                                LanguageHash.Add("fckeditor_language", TempLanguage)
                            Else
                                LanguageHash.Add("fckeditor_language", "auto")
                            End If
                        End If


                        ' put language setting in memory

                        context.Items.Add("Language", LanguageHash)

                        ' put url in memory
                        context.Items.Add("RequestURL", Request.Url.ToString())
                        context.Items.Add("RequestDocument", Request.Path.ToString())
                        ' end language				

                        If TempLanguageCode = TempLanguage Then
                            StringFileName = Replace(StringFileName, TempLanguageCode & ".", "")
                        End If

                        If StringFileName <> "default.aspx" Then
                            StringFileName = Replace(StringFileName, ".aspx", "")


                            If StringFileName <> "" Then
                                Dim result As Integer = objAdmin.GetTabByFriendLyName(StringFileName, PortalNumber)
                                If result > -1 Then
                                    tabId = result
                                    Dim TempQuerystring As String = context.Request.QueryString.ToString()
                                    If Request.Params("tabid") Is Nothing Then
                                        If TempQuerystring = "" Then
                                            TempQuerystring = "tabid=" & tabId.ToString
                                        Else
                                            TempQuerystring = "tabid=" & tabId.ToString & "&" & TempQuerystring
                                        End If
                                    End If
                                    HttpContext.Current.RewritePath(glbPath() + "default.aspx", String.Empty, TempQuerystring)
                                Else
                                    'not a tab in data base
                                    'see if file exist otherwise send 404
                                    If Not IO.File.Exists(Server.MapPath(Request.Path)) Then
                                        SendHttpException("404", "Not Found", Request)
                                    End If

                                End If
                            End If
                        Else
                            HttpContext.Current.RewritePath(glbPath() + "default.aspx", String.Empty, HttpContext.Current.Request.QueryString.ToString())

                        End If
                        ' Cashe Portal Setting 

                        Dim TempKey As String = GetDBname() & "Portal" & PortalNumber & DomainName & "_Tab" & tabId & "_ " & TempLanguage


                        Dim _settings As PortalSettings = CType(context.Cache(TempKey), PortalSettings)

                        If _settings Is Nothing Then
                            ' If this object has not been instantiated yet, we need to grab it
                            _settings = New PortalSettings(tabId, PortalAlias, Request.ApplicationPath, TempLanguage)
                            context.Cache.Insert(TempKey, _settings, CDp(PortalNumber, tabId), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), Caching.CacheItemPriority.Normal, Nothing)
                        End If
                        context.Items.Add("PortalSettings", _settings)
                    Else
                        ' alias does not exist in database
                        SendHttpException("404", "Not Found", Request)

                    End If ' End PortalNumber <> -1 
                End If
            End If
        End Sub



        Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)

            If Application("SetContext") = "Maintenance" And Request.Params("Maintenance") Is Nothing Then
                Exit Sub
            End If


            Dim StrPhysicalPath As String = Context.Request.PhysicalPath
            Dim StrExtension As String = ""
            If InStr(1, StrPhysicalPath, ".") Then
                StrExtension = Mid(StrPhysicalPath, InStrRev(StrPhysicalPath, ".")).ToLower
            End If

            If StrExtension <> ".axd" Then
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                If Request.IsAuthenticated = True Then

                    If Not Request.Cookies("portalid") Is Nothing Then
                        Dim PortalCookie As FormsAuthenticationTicket = FormsAuthentication.Decrypt(Context.Request.Cookies("portalid").Value)
                        ' check if user has switched portals
                        If _portalSettings.PortalId <> Int32.Parse(PortalCookie.UserData) Then
                            ' expire cookies if portal has changed

                            Response.Cookies("portalid").Value = Nothing
                            Response.Cookies("portalid").Path = "/"
                            Response.Cookies("portalid").Expires = DateTime.Now.AddYears(-30)

                            Response.Cookies("portalroles").Value = Nothing
                            Response.Cookies("portalroles").Path = "/"
                            Response.Cookies("portalroles").Expires = DateTime.Now.AddYears(-30)

                            ' check if user is valid for new portal
                            Dim objUser As New UsersDB()
                            Dim dr As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, Int32.Parse(Context.User.Identity.Name))
                            If Not dr.Read Then
                                ' log user out
                                FormsAuthentication.SignOut()
                                DotNetZoom.LogMessage(Context.Request, "SignOut : check if user is valid for new portal")
                                ' Redirect browser back to home page
                                Response.Redirect(Request.RawUrl, True)
                                Exit Sub
                            End If

                            dr.Close()

                        End If
                    End If
                End If

                If Request.IsAuthenticated = True Then

                    Dim arrPortalRoles() As String

                    ' get UserId based on authentication method ( from web.config )
                    Dim intUserId As Integer = -1
                    Dim objUser As New UsersDB()
                    Select Case User.Identity.AuthenticationType
                        Case "Forms"
                            If IsNumeric(Context.User.Identity.Name) Then
                                intUserId = Int32.Parse(Context.User.Identity.Name)
                            End If
                        Case "Windows"
                            Dim dr As SqlDataReader = objUser.GetSingleUserByUsername(_portalSettings.PortalId, Context.User.Identity.Name)
                            If dr.Read() Then
                                intUserId = dr("UserId")
                            End If
                            dr.Close()
                    End Select

                    ' authenticate user and set last login ( this is necessary for users who have a permanent Auth cookie set )
                    If Not objUser.UpdateUserLogin(intUserId, _portalSettings.PortalId) Then
                        ' Log User Off from Cookie Authentication System
                        LogOffUser()
                        DotNetZoom.LogMessage(Context.Request, "SignOut : Fail UpdateUserLogin")

                    Else ' valid Auth cookie

                        ' create cookies if they do not exist yet for this session.
                        If Request.Cookies("portalroles") Is Nothing Then

                            ' keep cookies in sync
                            Dim CurrentDateTime As Date = DateTime.Now

                            ' create a cookie authentication ticket ( version, user name, issue time, expires every hour, don't persist cookie, roles )
                            Dim PortalTicket As New FormsAuthenticationTicket(1, intUserId.ToString, CurrentDateTime, CurrentDateTime.AddHours(1), False, _portalSettings.PortalId.ToString)
                            ' encrypt the ticket
                            Dim strPortal As String = FormsAuthentication.Encrypt(PortalTicket)
                            ' send portal cookie to client
                            Response.Cookies("portalid").Value = strPortal
                            Response.Cookies("portalid").Path = "/"
                            Response.Cookies("portalid").Expires = CurrentDateTime.AddMinutes(1)

                            ' get roles from UserRoles table
                            arrPortalRoles = objUser.GetRolesByUser(intUserId, _portalSettings.PortalId)
                            ' create a string to persist the roles
                            Dim strPortalRoles As String = Join(arrPortalRoles, New Char() {";"c})

                            ' create a cookie authentication ticket ( version, user name, issue time, expires every hour, don't persist cookie, roles )
                            Dim RolesTicket As New FormsAuthenticationTicket(1, intUserId.ToString, CurrentDateTime, CurrentDateTime.AddHours(1), False, strPortalRoles)
                            ' encrypt the ticket
                            Dim strRoles As String = FormsAuthentication.Encrypt(RolesTicket)
                            ' send roles cookie to client
                            Response.Cookies("portalroles").Value = strRoles
                            Response.Cookies("portalroles").Path = "/"
                            Response.Cookies("portalroles").Expires = CurrentDateTime.AddMinutes(1)

                        Else
                            If Request.Cookies("portalroles").Value <> "" Then

                                ' get roles from roles cookie
                                Dim RoleTicket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(Context.Request.Cookies("portalroles").Value)

                                ' convert the string representation of the role data into a string array
                                arrPortalRoles = Split(RoleTicket.UserData, New Char() {";"c})

                            End If

                        End If

                        ' add our own custom principal to the request containing the roles in the auth ticket
                        Dim objGenericIdentity As Principal.GenericIdentity = New Principal.GenericIdentity(intUserId.ToString)
                        Context.User = New GenericPrincipal(objGenericIdentity, arrPortalRoles)
                        Dim strUserRoles As String = Join(arrPortalRoles, New Char() {";"c})
                        Context.Items.Add("portalRole", strUserRoles)

                    End If
                End If

            End If

        End Sub

        Sub Application_End(ByVal Sender As Object, ByVal E As EventArgs)
            cleanLogs(Server.MapPath("~/database/"))
        End Sub

        Sub Application_Start(ByVal Sender As Object, ByVal E As EventArgs)
            ' Get the appSettings key,value pairs collection. 
            Dim appSettings As NameValueCollection = _
            WebConfigurationManager.AppSettings

            ' Get the collection enumerator. 
            Dim appSettingsEnum As IEnumerator = _
            appSettings.GetEnumerator()

            ' Loop through the collection and  

            Dim i As Integer = 0
            Application("logmessage") = False
            While appSettingsEnum.MoveNext()
                Dim key As String = appSettings.AllKeys(i)
                Select Case key
                    Case "logmessage" : Application("logmessage") = CType(appSettings("logmessage"), Boolean)
                    Case "throttle" : Application("throttle") = appSettings("throttle")
                    Case "cryptokey" : Application("cryptokey") = appSettings("cryptokey")
                    Case "SetContext" : Application("SetContext") = appSettings("SetContext")
                End Select
                i += 1
            End While

            If Application("cryptokey") Is Nothing Then
                Application("cryptokey") = Membership.GeneratePassword(16, 7)
                WriteSetting("web", "cryptokey", Application("cryptokey"))
            End If
            If WebConfigurationManager.AppSettings("ConnectionString") = "SERVER=sqlexpress;Database=dotnetzoom;User ID=sa;Password=password;Trusted_Connection=False;" Then
                ' Site was not set up yet send Startup
                ' Set up Maintenance 
                Application("SetContext") = "Maintenance"
            Else
                ' Set up Maintenance if the Version need to update database
                If DotNetZoom.PortalSettings.GetVersion(WebConfigurationManager.AppSettings("connectionString")) < ApplicationVersion Then
                    Application("SetContext") = "Maintenance"
                End If
            End If
            'Clear all Cache on StartUp just in Case
            For Each de As DictionaryEntry In HttpContext.Current.Cache
                HttpContext.Current.Cache.Remove(de.Key.ToString())
            Next de
        End Sub

        Public Sub WriteSetting(ByVal InFile As String, ByVal key As String, ByVal value As String)
            Dim doc As XmlDocument = loadConfigDocument(InFile)
            Dim node As XmlNode = doc.SelectSingleNode("//appSettings")
            If node Is Nothing Then
                Throw New InvalidOperationException("appSettings section not found in config file.")
            End If
            Try
                Dim elem As XmlElement = CType(node.SelectSingleNode(String.Format("//add[@key='{0}']", key)), XmlElement)
                If Not (elem Is Nothing) Then
                    elem.SetAttribute("value", value)
                Else
                    elem = doc.CreateElement("add")
                    elem.SetAttribute("key", key)
                    elem.SetAttribute("value", value)
                    node.AppendChild(elem)
                End If
                doc.Save(getConfigFilePath(InFile))
            Catch
                SendHttpException("403", "Forbidden", Request, getConfigFilePath(InFile))

            End Try
        End Sub

        Private Function loadConfigDocument(ByVal InFile As String) As XmlDocument
            Dim doc As XmlDocument = Nothing
            Try
                doc = New XmlDocument
                doc.Load(getConfigFilePath(InFile))
                Return doc
            Catch e As System.IO.FileNotFoundException
                Throw New Exception("No configuration file found.", e)
            End Try
        End Function

        Private Function getConfigFilePath(ByVal InFile As String) As String
            Return System.Web.HttpContext.Current.Server.MapPath(InFile & ".config")
        End Function


        Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
            Dim LastError As Exception
            LastError = Server.GetLastError.GetBaseException()
            If PortalSettings.GetVersion > -1 Then
                If PortalSettings.GetHostSettings("EnableErrorReporting") <> "N" Then
                    Dim ErrorMessage As String = String.Empty


                    If Not LastError Is Nothing Then
                        ErrorMessage += (LastError.Message + vbCrLf)
                        ErrorMessage += (LastError.StackTrace + vbCrLf)
                        ErrorMessage += (LastError.Source + vbCrLf)
                    End If

                    SendNotification(PortalSettings.GetHostSettings("HostEmail"), PortalSettings.GetHostSettings("HostEmail2"), "", "Page_Error", ErrorMessage.ToString, "")
                End If

                If File.Exists(Server.MapPath("/erreur" + GetLanguage("N") + ".htm")) Then
                    ' read script file for version
                    Dim objStreamReader As StreamReader
                    objStreamReader = File.OpenText(Server.MapPath("/erreur" + GetLanguage("N") + ".htm"))
                    Dim strHTML As String = objStreamReader.ReadToEnd
                    objStreamReader.Close()
                    Response.Write(strHTML)
                End If
                LogErrorMessage(Request, LastError)
            End If
            Server.ClearError()
            Response.End()
        End Sub



    End Class
End Namespace