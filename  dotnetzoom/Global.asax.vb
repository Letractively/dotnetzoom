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
Imports System.Configuration
Imports System.Collections.Specialized



Namespace DotNetZoom

   Public Class TOPGlobal

        Inherits System.Web.HttpApplication


        Private Sub UpdateTheVersion()
            ' Reset the application and start over
            Dim strWebConfig As String
            Dim objStreamReader As StreamReader
            objStreamReader = File.OpenText(Server.MapPath("web.config"))
            strWebConfig = objStreamReader.ReadToEnd
            objStreamReader.Close()
            Try
                Dim objStream As StreamWriter
                objStream = File.CreateText(Server.MapPath("web.config"))
                objStream.WriteLine(strWebConfig)
                objStream.Close()
            Catch
				SendHttpException("403", "Forbidden", Request, Server.MapPath("web.config"))
            End Try
        End Sub

        Private Sub SendStartupOk()
            Application("GUID") = Nothing
            Application("dostartup") = Nothing
            Dim strHTML As String = GetXMLmessage("startupok", Request)
            Response.Write(strHTML)
            UpdateTheVersion()
            Response.End()
        End Sub



        Private Sub SendStartup(ByVal ServerName As String, ByVal Database As String, ByVal ConnectionType As String, ByVal UserName As String, ByVal Password As String, ByVal tempGUID As String, ByVal Message As String)
            Dim strHTML As String = GetXMLmessage("startup", Request)
            strHTML = Replace(strHTML, "[Message]", Message)
            strHTML = Replace(strHTML, "[ServerName]", ServerName)
            strHTML = Replace(strHTML, "[Database]", Database)
            strHTML = Replace(strHTML, "[UserName]", UserName)
            strHTML = Replace(strHTML, "[Password]", Password)
            If ConnectionType = "Windows" Then
                strHTML = Replace(strHTML, "[Windows]", "checked")
                strHTML = Replace(strHTML, "[Sql]", "")
            Else
                strHTML = Replace(strHTML, "[Windows]", "")
                strHTML = Replace(strHTML, "[Sql]", "checked")
            End If
            If tempGUID <> "" Then
                strHTML = Replace(strHTML, "[GUID]", tempGUID)
            Else
                tempGUID = Guid.NewGuid().ToString()
                strHTML = Replace(strHTML, "[GUID]", tempGUID)
                Application("GUID") = tempGUID
            End If
            Response.Write(strHTML)
            Response.End()
        End Sub

        Sub dostartup()

            Dim objAdmin As New DotNetZoom.AdminDB()
            ' automatic upgrade
            Dim intBuild As Integer = DotNetZoom.PortalSettings.GetVersion
            Dim TempKey As String = GetDBname()
            Dim context As HttpContext = HttpContext.Current

            ' for each build version up to the current version, perform the necessary upgrades
            If intBuild > -2 Then
	            While intBuild < ApplicationVersion
                    intBuild += 1
                    ' verify script has not already been run
                    context.Cache.Remove(TempKey)

                    If Not DotNetZoom.PortalSettings.FindVersion(intBuild) Then

                        ' database upgrade
                        If File.Exists(Server.MapPath("Database\1.0." & intBuild.ToString & ".sql")) Then
                            ' read script file for version
                            Dim objStreamReader As StreamReader
                            objStreamReader = File.OpenText(Server.MapPath("Database\1.0." & intBuild.ToString & ".sql"))
                            Dim strScript As String = objStreamReader.ReadToEnd
                            objStreamReader.Close()

                            ' execute SQL installation script
                            Dim strSQLExceptions As String = objAdmin.ExecuteSQLScript(strScript)

                            ' delete script file once executed
                            Try

                                File.Delete(Server.MapPath("Database\1.0." & intBuild.ToString & ".sql"))

                            Catch
                                ' could not delete the script file
                                Application("error403") += "<br>" & Server.MapPath("Database\1.0." & intBuild.ToString & ".sql")
                            End Try

                            ' log the results
                            Try
                                Dim objStream As StreamWriter
                                objStream = File.CreateText(Server.MapPath("Database\1.0." & intBuild.ToString & ".log"))
                                objStream.WriteLine(strSQLExceptions)
                                objStream.Close()
                            Catch
                                ' does not have permission to create the log file
                                ' Response.Redirect("403-3.htm", True)
                                Application("error403") += "<br>" & Server.MapPath("Database\1.0." & intBuild.ToString & ".log")

                            End Try
                        Else
                            ' script file does not exist for version ( this is mandatory for every version )
                            Try
                                Dim objStream As StreamWriter
                                objStream = File.CreateText(Server.MapPath("Database\1.0." & intBuild.ToString & ".log"))
                                objStream.WriteLine("Upgrade Error. Could Not Locate " & Server.MapPath("Database\1.0." & intBuild.ToString & ".sql") & " Database Upgrade Script.")
                                objStream.Close()
                            Catch
                                ' does not have permission to create the log file
                                Application("error403") += "<br>" & Server.MapPath("Database\1.0." & intBuild.ToString & ".log")
                            End Try
                            Exit Sub
                        End If
                        DotNetZoom.PortalSettings.UpdateVersion(intBuild)
                    End If

                End While
            End If
            Application("DoValidation") = "ok"
        End Sub

        Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)

            If Not Application("error403") Is Nothing Then
                Dim TempError As String = Application("error403")
                Application("error403") = Nothing
                SendHttpException("403", "Forbidden", Request, TempError)
            End If
            If Application("DoValidation") = "Startup" Then
                dostartup()
                If Not Application("error403") Is Nothing Then
                    Application("dostartup") = Nothing
                    Application("GUID") = Nothing
                    Application("DoValidation") = Nothing
                    Dim TempError As String = Application("error403")
                    Application("error403") = Nothing
            	    SendHttpException("403", "Forbidden", Request, TempError)
                End If
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
           Dim StrExtension As String = ""
            If InStr(1, StrPhysicalPath, ".") Then
               StrExtension = Mid(StrPhysicalPath, InStrRev(StrPhysicalPath, ".")).ToLower
           End If

           If StrExtension <> ".axd" Then

                If Application("dostartup") = "ok" Then
                    ' Site was not set up yet send Startup
                    If Not Application("GUID") Is Nothing Then
                        ' DataBase not set up

                        If Application("GUID") = Request.Form("GUID") Then
                            ' Build Connection String
                            Dim ConnectionString As String = "SERVER=" + Request.Form("ServerName") + ";Database=" + Request.Form("DataBase") + ";"
                            Dim ServerConnectionString As String = "SERVER=" + Request.Form("ServerName") + ";"
                            If Request.Form("Connection") = "Windows" Then
                                ConnectionString += "Trusted_Connection=True;"
                                ServerConnectionString += "Trusted_Connection=True;"
                            Else
                                ConnectionString += "User ID=" + Request.Form("UserName") + ";Password=" + Request.Form("Password") + ";Trusted_Connection=False;"
                                ServerConnectionString += "User ID=" + Request.Form("UserName") + ";Password=" + Request.Form("Password") + ";Trusted_Connection=False;"
                            End If
                            If CanConnecttoSql(ServerConnectionString) And Not CanConnectDataBase(ConnectionString, Request.Form("DataBase")) Then
                                If CreateDataBase(ServerConnectionString, Request.Form("DataBase")) Then
                                    WriteSetting("web", "connectionString", ConnectionString)
                                    SendStartupOk()
                                    Exit Sub
                                End If
                            End If
                            If CanConnectDataBase(ConnectionString, Request.Form("DataBase")) Then
                                WriteSetting("web", "connectionString", ConnectionString)
                                SendStartupOk()
                                Exit Sub
                            Else
                                Dim Message As String
                                If CanConnecttoSql(ServerConnectionString) Then
                                    Message = "Message3"
                                Else
                                    Message = "Message2"
                                End If
                                SendStartup(Request.Form("ServerName"), Request.Form("DataBase"), Request.Form("Connection"), Request.Form("UserName"), Request.Form("Password"), Request.Form("GUID"), Message)
                                Exit Sub
                            End If
                        End If
                    Else
                        'data base not setup yet
                        SendStartup(System.Environment.MachineName & "\sqlexpress", "dotnetzoom", "Windows", "", "", "", "Message1")
                    End If
                End If


                HttpContext.Current.Items("DBName") = Nothing
                HttpContext.Current.Items("ConnectionString") = Nothing

                Dim tabId As Integer = 0
                Dim DomainName As String = Nothing
                Dim PortalAlias As String = Nothing
                Dim PortalNumber As Integer = -1

                ' check once to see if the upgrade worked
                If Application("DoValidation") = "ok" Then
                    Dim DatabaseVersion As Integer = PortalSettings.GetVersion
                    
					   
                    If DatabaseVersion <> ApplicationVersion Then
                        ' the database version is not synchronized with the assembly version
                        Dim TempKey As String = GetDBname()
                        context.Cache.Remove(TempKey)

                        Dim strMessage As String = ""
                        Dim strSql As String = ""
                        If DatabaseVersion = -2 Then
                            ' error1 [DBname]
                            strMessage = Replace(GetXMLmessage("error1", Request), "[DBname]", GetDBname())
                        Else
                            Dim intVersion As Integer
                            For intVersion = (DatabaseVersion + 1) To ApplicationVersion
                                If Not File.Exists(Server.MapPath("Database\1.0." & intVersion.ToString & ".sql")) Then
                                    strMessage += "<br>1.0." & intVersion.ToString & ".sql"
                                Else
                                    strSql += "<br>1.0." & intVersion.ToString & ".sql"
                                End If
                            Next
                            If strSql <> "" Then
                                'error 2 
                                strSql = GetXMLmessage("error2", Request) + strSql + "<br><br>"
                            End If
                            If strMessage <> "" Then
                                strMessage = GetXMLmessage("error3", Request) + strMessage + "<br><br>"
                            End If

                        End If


                        Dim strHTML As String = GetXMLmessage("500", Request)
                        strHTML = Replace(strHTML, "[ASSEMBLYVERSION]", "1.0." & ApplicationVersion.ToString)
                        strHTML = Replace(strHTML, "[DATABASEVERSION]", "1.0." & DatabaseVersion.ToString)
                        strHTML = Replace(strHTML, "[MESSAGE]", strSql & strMessage)
                        Response.Write(strHTML)
                        If strMessage = "" Then
                            UpdateTheVersion()
                        End If
                        Response.End()

                    Else
                        Application("DoValidation") = Nothing
                    End If
                End If


                ' check if linkclick
                If Not (Request.Params("linkclick") Is Nothing) Then
                    Dim objSecurity As New DotNetZoom.PortalSecurity()
                    Dim cryptoout As String = objSecurity.Decrypt(Application("cryptokey"), Request.Params("linkclick"))
                   context.Items.Add("linkclick", Request.Params("linkclick"))
                    HttpContext.Current.RewritePath(Request.Path.ToString(), String.Empty, cryptoout)
                End If

                ' get tabId from querystring
                If Not (Request.Params("tabid") Is Nothing) Then
                    If Request.Params("tabid") <> "" Then
                        If IsNumeric(Request.Params("tabid")) Then
                            tabId = Int32.Parse(Request.Params("tabid"))
                       Else
                            Response.End()
                        End If
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

                ' validate the alias
                PortalNumber = PortalSettings.GetPortalByAlias(PortalAlias)

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
                       ' See if the Portal Directory Exist  if not create it
                        If Not System.IO.Directory.Exists(Request.MapPath(_settings.UploadDirectory)) Then
                            'Create the new directory and put in files
                             Try
 	                             System.IO.Directory.CreateDirectory(Request.MapPath(_settings.UploadDirectory))
                                 Dim fileEntries As String() = System.IO.Directory.GetFiles(GetAbsoluteServerPath(Request) + "templates\base\")
                                 Dim TempFileName As String
                                 Dim strFileName As String
                                 For Each strFileName In fileEntries
                                     If InStr(1, strFileName.ToLower, "template.") = 0 Then
                                         TempFileName = strFileName.Replace(GetAbsoluteServerPath(Request) + "templates\base\", Request.MapPath(_settings.UploadDirectory))
                                         System.IO.File.Copy(strFileName, TempFileName)
                                     End If
                                 Next strFileName
                                 Dim StrFolder As String
                                 For Each StrFolder In System.IO.Directory.GetDirectories(GetAbsoluteServerPath(Request) + "templates\base\")
                                     CopyFileRecursively(StrFolder, StrFolder.Replace(GetAbsoluteServerPath(Request) + "templates\base\", Request.MapPath(_settings.UploadDirectory)))
                                 Next
                             Catch
                                ClearHostCache()
            					 SendHttpException("403", "Forbidden", Request, Request.MapPath(_settings.UploadDirectory))
                             End Try
                         End If

                    End If
                   context.Items.Add("PortalSettings", _settings)
                Else
                    ' alias does not exist in database
            		 SendHttpException("404", "Not Found", Request)

                End If ' End PortalNumber <> -1 
           End If
        End Sub



        Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)


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
                                ' Redirect browser back to home page
                                Response.Redirect(Request.RawUrl, True)
                                Exit Sub
                            End If

                            dr.close()

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


                    End If
                End If

            End If
        End Sub

        Sub Application_End(ByVal Sender As Object, ByVal E As EventArgs)
            Try
                Dim objStream As StreamWriter
                objStream = File.AppendText(Application("log"))
                objStream.WriteLine("Application End : " + DateTime.Now.ToString("yyyy\-MM\-dd HH\:mm\:ss"))
                objStream.Close()
            Catch
            End Try

        End Sub

        Sub Application_Start(ByVal Sender As Object, ByVal E As EventArgs)
            Application("log") = Server.MapPath("/Startup.log")
            Application("throttle") = ConfigurationSettings.AppSettings("throttle")
            Application("cryptokey") = ConfigurationSettings.AppSettings("cryptokey")
            If Application("cryptokey") Is Nothing Then
                Application("cryptokey") = Membership.GeneratePassword(16, 7)
            End If
            If ConfigurationSettings.AppSettings("ConnectionString") = "SERVER=sqlexpress;Database=dotnetzoom;User ID=sa;Password=password;Trusted_Connection=False;" Then
                ' Site was not set up yet send Startup
                ' Check if can write to disk
                Application("dostartup") = "ok"
                Application("DoValidation") = "ok"

                Try
                    Dim objStream As StreamWriter
                    objStream = File.AppendText(Server.MapPath("/Startup.log"))
                    objStream.WriteLine("Application Init : " + DateTime.Now.ToString("yyyy\-MM\-dd HH\:mm\:ss"))
                    objStream.Close()
                Catch
                    ' does not have permission to create file on root
                    Application("error403") = Server.MapPath("") & "<br>"
                End Try


            Else
                Application("DoValidation") = "Startup"
                Try
                    Dim objStream As StreamWriter
                    objStream = File.AppendText(Server.MapPath("/Startup.log"))
                    objStream.WriteLine("Application Start : " + DateTime.Now.ToString("yyyy\-MM\-dd HH\:mm\:ss"))
                    objStream.Close()
                Catch
                End Try

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

        Private Function CreateDataBase(ByVal ConnectionString As String, ByVal DataBase As String)
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConnectionString)
            ' check if Can Connect to database
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {}, CommandType.Text, "USE MASTER CREATE DATABASE " & DataBase)
            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch
                Return False
            End Try
            Return True
        End Function

        Private Function CanConnecttoSql(ByVal ConnectionString As String) As Boolean
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConnectionString)
            ' check if Can Connect to database
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {}, CommandType.Text, "select * from sys.databases")
            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch
                Return False
            End Try
            Return True
        End Function

        Private Function CanConnectDataBase(ByVal ConnectionString As String, ByVal DataBase As String) As Boolean
            Dim Success As Boolean = False
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConnectionString)
            ' check if Can Connect to database
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {}, CommandType.Text, "select name from sys.databases where name = N'" & DataBase & "'")
            Try
                myConnection.Open()
                Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                If result.Read() Then Success = True
                result.Close()
            Catch
                Return False
            End Try
            Return Success
        End Function

        Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)

            If PortalSettings.GetHostSettings("EnableErrorReporting") <> "N" Then
                Dim LastError As Exception
                Dim ErrorMessage As String

                LastError = Server.GetLastError()

                If Not LastError Is Nothing Then
                    ErrorMessage += (LastError.Message + vbCrLf)
                    ErrorMessage += (LastError.StackTrace + vbCrLf)
                    ErrorMessage += (LastError.Source + vbCrLf)
                End If

                ErrorMessage += BuildErrorMessage(Request)

                SendNotification(PortalSettings.GetHostSettings("HostEmail"), PortalSettings.GetHostSettings("HostEmail2"), "", "Page_Error", ErrorMessage.ToString, "")
                If File.Exists(Server.MapPath("erreur" + GetLanguage("N") + ".htm")) Then
                    ' read script file for version
                    Dim objStreamReader As StreamReader
                    objStreamReader = File.OpenText(Server.MapPath("erreur" + GetLanguage("N") + ".htm"))
                    Dim strHTML As String = objStreamReader.ReadToEnd
                    objStreamReader.Close()
                    Response.Write(strHTML)
                End If
                Server.ClearError()
                Response.End()
            End If

        End Sub



    End Class
End Namespace