'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2008
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
Imports System
Imports System.Configuration
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Mail
Imports System.Web
Imports System.web.caching
Imports System.Web.HttpUtility
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml




Namespace DotNetZoom

    '*********************************************************************
    '
    ' Globals Module
    ' This module contains global utility functions, constants, and enumerations.
    '
    '*********************************************************************

    Public Module Globals

        Public Const glbRoleAllUsers As String = "-1"
        Public Const glbRoleSuperUser As String = "-2"
        Public Const glbRoleUnauthUser As String = "-3"

        Public Const glbImageFileTypes As String = "jpg,jpeg,jpe,gif,bmp,png"

        Public ReadOnly Property glbSiteDirectory() As String
            Get
                Return IIf(HttpContext.Current.Request.ApplicationPath = "/", "", HttpContext.Current.Request.ApplicationPath) + "/images/"
            End Get
        End Property

        Public ReadOnly Property glbPath() As String
            Get
                Return IIf(HttpContext.Current.Request.ApplicationPath = "/", "", HttpContext.Current.Request.ApplicationPath) & "/"
            End Get
        End Property

        Public ReadOnly Property glbTemplatesDirectory() As String
            Get
                Return IIf(HttpContext.Current.Request.ApplicationPath = "/", "", HttpContext.Current.Request.ApplicationPath) + "/templates/"
            End Get
        End Property



       Public function SplitContainer( ByVal strContainer As String, ByVal strUploadDirectory As String, Optional ByVal strAlignment As String = "", Optional ByVal strColor As String = "", Optional ByVal strBorder As String = "") as array
			If strContainer.IndexOf("[MODULE]") = -1 then strContainer = "[MODULE]" 
            strContainer = Replace(strContainer, "[ALIGN]", IIf(strAlignment <> "", " align=""" & strAlignment & """", ""))
            strContainer = Replace(strContainer, "[COLOR]", IIf(strColor <> "", " bgcolor=""" & strColor & """", ""))
            strContainer = Replace(strContainer, "[BORDER]", IIf(strBorder <> "", " border=""" & strBorder & """", ""))
            Return Split(strContainer, "[MODULE]")
       End function		 
		 
		 
		
		Public Sub RegisterBADip(ByVal BadIP as String)
		Dim TempTime As integer = 1
		If HttpContext.Current.Cache(BadIP) Is Nothing Then
        HttpContext.Current.Cache.Insert(BadIP, TempTime, Nothing, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), Caching.CacheItemPriority.normal, nothing)
		else
		TempTime = HttpContext.Current.Cache(BadIP) + 1
		HttpContext.Current.Cache(BadIP) = TempTime
		End If
		If tempTime > 10 then 
		HttpContext.Current.Application(BadIP) = DateTime.Now.AddHours(1)
		Dim StrBody As New StringBuilder()
		Dim UserId As Integer = -1
        If HttpContext.Current.Request.IsAuthenticated Then
        UserId = CType(HttpContext.Current.User.Identity.Name, Integer)
	    End If
		Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		try
		if HttpContext.Current.Request.UrlReferrer Is Nothing then
		strBody.AppendFormat(GetLanguage("Bad_IPTXT"), BadIP, "*" , HttpContext.Current.Request.Url.ToString(), HttpContext.Current.Request.UserAgent, UserID.ToString())
		else
		strBody.AppendFormat(GetLanguage("Bad_IPTXT"), BadIP, HttpContext.Current.Request.UrlReferrer.ToString() , HttpContext.Current.Request.Url.ToString(), HttpContext.Current.Request.UserAgent, UserID.ToString())
        end if
		SendNotification(portalSettings.GetHostSettings("HostEmail"), _portalSettings.Email, portalSettings.GetHostSettings("HostEmail"), GetLanguage("Bad_IP"), strBody.ToString)
    	Catch objException As Exception
        SendNotification(portalSettings.GetHostSettings("HostEmail"), _portalSettings.Email, portalSettings.GetHostSettings("HostEmail"), GetLanguage("Bad_IP"), BadIP )
		End Try
		HttpContext.Current.Response.redirect("/", true)
		end if
		end sub
		
		
		Public Sub ClearHostCache()
		HttpContext.Current.Cache.Remove(GetDBName)
		end sub

		Public Sub ClearPortalCache(ByVal PortalId As integer)
		HttpContext.Current.Cache.Remove(GetDBname & "P_" & PortalID.ToString)
		end sub

		Public Sub ClearTabCache(ByVal TabId As integer)
		HttpContext.Current.Cache.Remove(GetDBname & "T_" & TabId.ToString)
		end sub
		
		Public Sub ClearModuleCache(ByVal ModuleId As Integer)
		HttpContext.Current.Cache.Remove(GetDBname & "M_" & ModuleID.ToString)
		end sub
		
		
		Public Function CDp( ByVal PortalId As Integer, Optional ByVal TabId As Integer = 0,Optional ByVal ModuleId As Integer = 0) as CacheDependency
	    Dim dependencyKey(3) As String
		Dim context As HttpContext = HttpContext.Current
  	    dependencyKey(0) = GetDBname
		dependencyKey(1) = GetDBname & "P_" & PortalID.ToString
		dependencyKey(2) = GetDBname & "T_" & TabID.ToString
		dependencyKey(3) = GetDBname & "M_" & ModuleID.ToString
		If Context.Cache(dependencyKey(0)) is nothing then
		Context.Cache.Insert(dependencyKey(0), portalsettings.GetVersion(), Nothing)
		end if

		If Context.Cache(dependencyKey(1)) is nothing then
		Context.Cache.Insert(dependencyKey(1), DateTime.Now(), Nothing)
		end if
		If Context.Cache(dependencyKey(2)) is nothing then
		Context.Cache.Insert(dependencyKey(2), DateTime.Now(), Nothing)
		end if
		If Context.Cache(dependencyKey(3)) is nothing then
		Context.Cache.Insert(dependencyKey(3), DateTime.Now(), Nothing)
		end if	
	
		Dim dependency As new CacheDependency(Nothing, dependencyKey)
		Return Dependency
		end function	

        ' returns the absolute server path to the root ( ie. D:\Inetpub\wwwroot\directory\ )
        Public Function GetAbsoluteServerPath(ByVal Request As HttpRequest) As String
            Dim strServerPath As String

            strServerPath = Request.MapPath(Request.ApplicationPath)
            If Not strServerPath.EndsWith("\") Then
                strServerPath += "\"
            End If

            GetAbsoluteServerPath = strServerPath
        End Function


        Public Sub CheckSecureSSL(ByVal Request As HttpRequest, ByVal ToSecure As Boolean)
            If ToSecure Then
                If Request.Params("def") = "Signup" _
                Or Request.Params("def") = "Register" _
                Or Request.Params("def") = "Gestion usagers" _
                Or Request.Params("adminpage") = "14" _
                Or Request.Params("adminpage") = "15" _
                Or Request.Params("adminpage") = "19" _
                Or Request.Params("adminpage") = "63" _
                Or Request.Params("adminpage") = "72" _
                Or Request.Params("showlogin") <> "" Then
                Else
                    ToSecure = False
                End If
                If Not Request.IsSecureConnection And ToSecure Then
                    HttpContext.Current.Response.Redirect(Replace(Request.Url.ToString(), "http://", "https://"), True)
                ElseIf Request.IsSecureConnection And Not ToSecure Then
                    HttpContext.Current.Response.Redirect(Replace(Request.Url.ToString(), "https://", "http://"), True)
                End If
            End If
        End Sub


        ' returns the domain name of the current request ( ie. www.domain.com or 207.132.12.123 or www.domain.com/directory if subhost )
        Public Function GetDomainName(ByVal Request As HttpRequest) As String
            Dim DomainName As String = ""
            Dim URL() As String
            Dim intURL As Integer

            URL = Split(Request.Url.ToString(), "/")
            For intURL = 2 To URL.GetUpperBound(0)
                Select Case URL(intURL).ToLower
                    Case "admin", "controls", "desktopmodules", "fckeditor"
                        Exit For
                    Case Else
                        ' check if filename
                        If InStr(1, URL(intURL), ".aspx") = 0 Then
                            DomainName += IIf(DomainName <> "", "/", "") & URL(intURL)
                        Else
                            Exit For
                        End If
                End Select
            Next intURL

            GetDomainName = DomainName
        End Function

        ' sends a simple email
        Public Function SendNotification(ByVal strFrom As String, ByVal strTo As String, ByVal strBcc As String, ByVal strSubject As String, ByVal strBody As String, Optional ByVal strAttachment As String = "", Optional ByVal strBodyType As String = "") As String

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim mail As New MailMessage()
			
			strBody = Replace(strBody, "src=""/", "src=""http://" + HttpContext.Current.Request.ServerVariables("HTTP_HOST") + "/")
			
            mail.From = strFrom
            mail.To = strTo
            If strBcc <> "" Then
                mail.Bcc = strBcc
            End If
            mail.Subject = strSubject
            mail.Body = strBody

            'here we check if we want to format the email as html or plain text.
            If strBodyType <> "" Then
                Select Case LCase(strBodyType)
                    Case "html"
                        mail.BodyFormat = MailFormat.Html
                    Case "text"
                        mail.BodyFormat = MailFormat.Text
                End Select
            End If

            If strAttachment <> "" Then
                mail.Attachments.Add(New MailAttachment(strAttachment))
            End If

            ' external SMTP server
            If portalSettings.GetHostSettings("SMTPServer") <> "" Then
                SmtpMail.SmtpServer = portalSettings.GetHostSettings("SMTPServer")
				If portalSettings.GetHostSettings("SMTPServerUser") <> "" then 
				Mail.Fields("http://schemas.microsoft.com/cdo/configuration/smtpserver") = portalSettings.GetHostSettings("SMTPServer")
			    Mail.Fields("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = 25
			    Mail.Fields("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2
			    Mail.Fields("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1
			    Mail.Fields("http://schemas.microsoft.com/cdo/configuration/sendusername") = portalSettings.GetHostSettings("SMTPServerUser")
			    Mail.Fields("http://schemas.microsoft.com/cdo/configuration/sendpassword") = portalSettings.GetHostSettings("SMTPServerPassword")
				end if 			
            End If

			
            Try
                SmtpMail.Send(mail)
            Catch objException As Exception
                ' mail configuration problem
                SendNotification = objException.Message
            End Try

        End Function

        ' encodes a URL for posting to an external site
        Public Function HTTPPOSTEncode(ByVal strPost As String) As String
            strPost = Replace(strPost, "\", "")
            strPost = System.Web.HttpUtility.UrlEncode(strPost)
            strPost = Replace(strPost, "%2f", "/")
            HTTPPOSTEncode = strPost
        End Function

        ' retrieves the domain name of the portal ( ie. http://www.domain.com " )
        Public Function GetPortalDomainName(ByVal strPortalAlias As String, Optional ByVal Request As HttpRequest = Nothing, Optional ByVal blnAddHTTP As Boolean = True) As String

            Dim strDomainName As String = ""

            Dim strURL As String = ""
            Dim arrPortalAlias() As String
            Dim intAlias As Integer

            If Not Request Is Nothing Then
                strURL = GetDomainName(Request)
            End If

            arrPortalAlias = Split(strPortalAlias, ",")
            For intAlias = 0 To arrPortalAlias.Length - 1
                If arrPortalAlias(intAlias) = strURL Then
                    strDomainName = arrPortalAlias(intAlias)
                End If
            Next
            If strDomainName = "" Then
                strDomainName = arrPortalAlias(0)
            End If

            If blnAddHTTP Then
                strDomainName = AddHTTP(strDomainName)
            End If

            GetPortalDomainName = strDomainName

        End Function

        ' adds HTTP to URL if no other protocol specified
        Public Function AddHTTP(ByVal strURL As String) As String
            If strURL <> "" Then
                If InStr(1, strURL, "://") = 0 And InStr(1, strURL, "~") = 0 And InStr(1, strURL, "\\") = 0 Then
                    strURL = "http://" & strURL
                End If
            End If
            Return strURL
        End Function

        ' adds HTTPS to URL if no other protocol specified
        Public Function AddHTTPS(ByVal strURL As String) As String
            If strURL <> "" Then
                If InStr(1, strURL, "://") = 0 And InStr(1, strURL, "~") = 0 And InStr(1, strURL, "\\") = 0 Then
                    strURL = "https://" & strURL
                End If
            End If
            Return strURL
        End Function


        ' convert datareader to dataset
        Public Function ConvertDataReaderToDataSet(ByVal reader As SqlClient.SqlDataReader) As DataSet

            Dim dataSet As DataSet = New DataSet()

            Dim schemaTable As DataTable = reader.GetSchemaTable()

            Dim dataTable As DataTable = New DataTable()

            Dim intCounter As Integer

            For intCounter = 0 To schemaTable.Rows.Count - 1
                Dim dataRow As DataRow = schemaTable.Rows(intCounter)
                Dim columnName As String = CType(dataRow("ColumnName"), String)
                Dim column As DataColumn = New DataColumn(columnName, CType(dataRow("DataType"), Type))
                dataTable.Columns.Add(column)
            Next

            dataSet.Tables.Add(dataTable)

            While reader.Read()
                Dim dataRow As DataRow = dataTable.NewRow()

                For intCounter = 0 To reader.FieldCount - 1
                    dataRow(intCounter) = reader.GetValue(intCounter)
                Next

                dataTable.Rows.Add(dataRow)
            End While
            reader.Close()

            Return dataSet

        End Function

        ' convert datareader to crosstab dataset
        Public Function BuildCrossTabDataSet(ByVal DataSetName As String, ByVal result As SqlDataReader, ByVal FixedColumns As String, ByVal VariableColumns As String, ByVal KeyColumn As String, ByVal FieldColumn As String, ByVal FieldTypeColumn As String, ByVal StringValueColumn As String, ByVal NumericValueColumn As String) As DataSet

            Dim arrFixedColumns As String()
            Dim arrVariableColumns As String()
            Dim arrField As String()
            Dim FieldType As String
            Dim intColumn As Integer
            Dim intKeyColumn As Integer

            ' create dataset
            Dim crosstab As New DataSet(DataSetName)
            crosstab.Namespace = "NetFrameWork"

            ' create table
            Dim tab As New DataTable(DataSetName)

            ' split fixed columns
            arrFixedColumns = FixedColumns.Split(",".ToCharArray())

            ' add fixed columns to table
            For intColumn = LBound(arrFixedColumns) To UBound(arrFixedColumns)
                arrField = arrFixedColumns(intColumn).Split("|".ToCharArray())
                Dim col As New DataColumn(arrField(0), System.Type.GetType("System." & arrField(1)))
                tab.Columns.Add(col)
            Next intColumn

            ' split variable columns
            If VariableColumns <> "" Then
                arrVariableColumns = VariableColumns.Split(",".ToCharArray())

                ' add varible columns to table
                For intColumn = LBound(arrVariableColumns) To UBound(arrVariableColumns)
                    arrField = arrVariableColumns(intColumn).Split("|".ToCharArray())
                    Dim col As New DataColumn(arrField(0), System.Type.GetType("System." & arrField(1)))
                    col.AllowDBNull = True
                    tab.Columns.Add(col)
                Next intColumn
            End If

            ' add table to dataset
            crosstab.Tables.Add(tab)

            ' add rows to table
            intKeyColumn = -1
            Dim row As DataRow
            While result.Read()
                ' loop using KeyColumn as control break
                If result(KeyColumn) <> intKeyColumn Then
                    ' add row
                    If intKeyColumn <> -1 Then
                        tab.Rows.Add(row)
                    End If

                    ' create new row
                    row = tab.NewRow()

                    ' assign fixed column values
                    For intColumn = LBound(arrFixedColumns) To UBound(arrFixedColumns)
                        arrField = arrFixedColumns(intColumn).Split("|".ToCharArray())
                        row(arrField(0)) = result(arrField(0))
                    Next intColumn

                    ' initialize variable column values
                    If VariableColumns <> "" Then
                        For intColumn = LBound(arrVariableColumns) To UBound(arrVariableColumns)
                            arrField = arrVariableColumns(intColumn).Split("|".ToCharArray())
                            Select Case arrField(1)
                                Case "Decimal"
                                    row(arrField(0)) = 0
                                Case "String"
                                    row(arrField(0)) = ""
                            End Select
                        Next intColumn
                    End If

                    intKeyColumn = result(KeyColumn)
                End If

                ' assign pivot column value
                If FieldTypeColumn <> "" Then
                    FieldType = result(FieldTypeColumn)
                Else
                    FieldType = "String"
                End If
                Select Case FieldType
                    Case "Decimal" ' decimal
                        row(result(FieldColumn)) = result(NumericValueColumn)
                    Case "String" ' string
                        Try
                            row(result(FieldColumn)) = result(StringValueColumn)
                        Catch exception As System.ArgumentException
                            row(result(FieldColumn)) = Replace(result(StringValueColumn), ".", ",")
                        End Try
                End Select
            End While

            result.Close()

            ' add row
            If intKeyColumn <> -1 Then
                tab.Rows.Add(row)
            End If

            ' finalize dataset
            crosstab.AcceptChanges()

            ' return the dataset
            Return crosstab

        End Function

        Public Class FileItem
            Private _Value As String
            Private _Text As String

            Public Sub New(ByVal Value As String, ByVal Text As String)
                _Value = Value
                _Text = Text
            End Sub

            Public ReadOnly Property Value() As String
                Get
                    Return _Value
                End Get
            End Property

            Public ReadOnly Property Text() As String
                Get
                    Return _Text
                End Get
            End Property

        End Class

        ' get list of files from folder matching criteria
        Public Function GetFileList(Optional ByVal PortalId As Integer = -1, Optional ByVal strExtensions As String = "", Optional ByVal NoneSpecified As Boolean = True) As ArrayList
            Dim arrFileList As New ArrayList()

            If NoneSpecified Then
                arrFileList.Add(New FileItem("", getlanguage("list_none")))
            End If

            Dim objAdmin As New AdminDB()

            Dim dr As SqlDataReader = objAdmin.GetFiles(PortalId)
            While dr.Read()
                If InStr(1, strExtensions, dr("Extension")) <> 0 Or strExtensions = "" Then
                    arrFileList.Add(New FileItem(dr("FileName").ToString, dr("FileName").ToString))
                End If
            End While
            dr.Close()

            GetFileList = arrFileList
        End Function

        ' format an address on a single line ( ie. Unit, Street, City, Region, Country, PostalCode )
        Public Function FormatAddress(ByVal Unit As Object, ByVal Street As Object, ByVal City As Object, ByVal Region As Object, ByVal Country As Object, ByVal PostalCode As Object) As String

            Dim strAddress As String = ""

            If Not IsDBNull(Unit) Then
                If Trim(Unit.ToString()) <> "" Then
                    strAddress += ", " & Unit.ToString
                End If
            End If
            If Not IsDBNull(Street) Then
                If Trim(Street.ToString()) <> "" Then
                    strAddress += ", " & Street.ToString
                End If
            End If
            If Not IsDBNull(City) Then
                If Trim(City.ToString()) <> "" Then
                    strAddress += ", " & City.ToString
                End If
            End If
            If Not IsDBNull(Region) Then
                If Trim(Region.ToString()) <> "" Then
                    strAddress += ", " & Region.ToString
                End If
            End If
            If Not IsDBNull(Country) Then
                If Trim(Country.ToString()) <> "" Then
                    strAddress += ", " & Country.ToString
                End If
            End If
            If Not IsDBNull(PostalCode) Then
                If Trim(PostalCode.ToString()) <> "" Then
                    strAddress += ", " & PostalCode.ToString
                End If
            End If
            If Trim(strAddress) <> "" Then
                strAddress = Mid(strAddress, 3)
            End If

            FormatAddress = strAddress

        End Function

        ' format an email address including link
        Public Function FormatEmail(ByVal Email As Object, ByVal Page As Page, Optional ByVal Name As String = "", Optional ByVal Subject As String = "" ) As String

            If Not IsDBNull(Email) Then
                If Trim(Email.ToString()) <> "" Then
                    If InStr(1, Email.ToString(), "@") Then
						Dim arrEmail As Array
        				arrEmail = Split(Email.ToString(), "@")
						FormatEmail =  "<script type=""text/javascript"" language=""Javascript"">protect(""" + arrEMail(0) + """, """ + arrEMail(1) + """, """ + iif(Name = "", replace(Email.ToString(),"@" , "<!-- nospan -->&" & "#64;"  & "<!-- spamnot -->"), Name) + """, """ + replace(urlEncode(Subject), "'", "%27") + """);</script>"

                    Else
                        FormatEmail = Email.ToString()
                    End If
                End If
            End If
        End Function

        ' format a domain name including link
        Public Function FormatWebsite(ByVal Website As Object) As String

            If Not IsDBNull(Website) Then
                If Trim(Website.ToString()) <> "" Then
                    If InStr(1, Website.ToString(), ".") Then
                        FormatWebsite = "<a href=""" & IIf(InStr(1, Website.ToString(), "://"), "", "http://") & Website.ToString() & """>" & Website.ToString() & "</a>"
                    Else
                        FormatWebsite = Website.ToString()
                    End If
                End If
            End If

        End Function

        ' returns an arraylist of tabitems for a portal
        Public Function GetPortalTabs(ByVal objDesktopTabs As ArrayList, Optional ByVal blnNoneSpecified As Boolean = False, Optional ByVal blnHidden As Boolean = False) As ArrayList

            Dim arrPortalTabs As ArrayList = New ArrayList()
            Dim objPortalTab As TabItem
            Dim objTab As TabStripDetails

            If blnNoneSpecified Then
                objPortalTab = New TabItem()
                objPortalTab.TabId = -1
                objPortalTab.TabName = getlanguage("list_none")
                objPortalTab.TabOrder = 0
                objPortalTab.ParentId = -2
                arrPortalTabs.Add(objPortalTab)
            End If

            Dim intCounter As Integer
            Dim strIndent As String

            For Each objTab In objDesktopTabs
                If (objTab.IsVisible = True Or blnHidden = True) Then
                    strIndent = ""
                    For intCounter = 1 To objTab.Level
                        strIndent += "..."
                    Next

                    objPortalTab = New TabItem()
                    objPortalTab.TabId = objTab.TabId
                    objPortalTab.TabName = strIndent & objTab.TabName
                    objPortalTab.TabOrder = objTab.TabOrder
                    objPortalTab.ParentId = objTab.ParentId
                    arrPortalTabs.add(objPortalTab)
                End If
            Next

            Return arrPortalTabs

        End Function

        public Function formatansidate(ByVal DateExp As String) as String
                     Dim Resulttodo as String = left(DateExp, 10)
                         Formatansidate = Resulttodo
        End Function


        Public Function CheckDateSqL(ByVal strDate As String) As String
            strDate = strDate.Replace("/", "")
            strDate = strDate.Replace("-", "")
            Return strDate
        End Function

		' ajout par Rene Boulard pour avoir le differenciel heure entre serveur et utilisateur
       Public Function GetTimeDiff(ByVal UserTime As Integer) As Integer
       Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		Dim TempTimeZone As integer

		If (UserTime = -99) or (UserTime < -720) or (UserTime > 840) then
		If portalSettings.GetHostSettings.ContainsKey("TimeZone") Then
		UserTime = portalSettings.GetHostSettings("TimeZone")
		end if
		end if
	
		If portalSettings.GetHostSettings.ContainsKey("TimeZone") Then
		TempTimeZone = portalSettings.GetHostSettings("TimeZone")
		else
		TempTimeZone = 0
		Usertime = 0
		end if

        Return UserTime - TempTimeZone
        End Function

		
		

        ' returns a SQL Server compatible date
        Public Function GetMediumDate(ByVal strDate As String) As String

            If strDate <> "" Then
                Dim datDate As Date = CDate(strDate)
                Dim strYear As String = Year(datDate)
                Dim strMonth As String = Monthname( Month( datDate ), True )
                Dim strDay As String = Day(datDate)
                strDate = strDay & "-" & strMonth & "-" & strYear
            End If

            Return strDate

        End Function

        ' returns a boolean value whether the tab is an admin tab
        Public Function IsAdminTab() As Boolean
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		Return Not (HttpContext.Current.Request.Params("adminpage") Is Nothing) and PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True
        End Function


        Public Function MakeTabXML(ByVal TabId As Integer, ByVal TabLanguage As String) As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim _NewPortalsettings As PortalSettings = New PortalSettings(TabId, _portalSettings.PortalAlias, HttpContext.Current.Request.ApplicationPath, TabLanguage)
            Dim _moduleSettings As ModuleSettings
            Dim ContentPane As String = ""
            Dim LeftPane As String = ""
            Dim RightPane As String = ""
            Dim TopPane As String = ""
            Dim BottomPane As String = ""
            Dim ResultString As String = ""
            Dim Dr As SqlDataReader


            ResultString = ControlChars.CrLf & "  <tab>" & ControlChars.CrLf
            ResultString += "  <name>" & _NewPortalsettings.ActiveTab.TabName & "</name>" & ControlChars.CrLf

            Dim Admin As New AdminDB()
            Dr = Admin.GetTabById(_NewPortalsettings.ActiveTab.ParentId, TabLanguage)
            If Dr.Read Then
                ResultString += "  <parent>" & Dr("TabName") & "</parent>" & ControlChars.CrLf
            Else
                ResultString += "  <parent></parent>" & ControlChars.CrLf
            End If
            Dr.Close()
            ResultString += "  <level>" & _NewPortalsettings.ActiveTab.Level.ToString() & "</level>" & ControlChars.CrLf
            ResultString += "  <taborder>" & _NewPortalsettings.ActiveTab.TabOrder.ToString() & "</taborder>" & ControlChars.CrLf
            ResultString += "  <visible>True</visible>" & ControlChars.CrLf
            ResultString += "   <panes>" & ControlChars.CrLf
            For Each _moduleSettings In _NewPortalsettings.ActiveTab.Modules
                If TabLanguage = _moduleSettings.Language Then
                    'If TabLanguage = _moduleSettings.Language Or _moduleSettings.Language = "" Then

                    Select Case _moduleSettings.PaneName.ToLower()
                        Case "leftpane"
                            LeftPane += CreateXLM(_moduleSettings.ModuleId)
                        Case "contentpane"
                            ContentPane += CreateXLM(_moduleSettings.ModuleId)
                        Case "rightpane"
                            RightPane += CreateXLM(_moduleSettings.ModuleId)
                        Case "toppane"
                            TopPane += CreateXLM(_moduleSettings.ModuleId)
                        Case "bottompane"
                            BottomPane += CreateXLM(_moduleSettings.ModuleId)

                    End Select
                End If
            Next

            If LeftPane <> "" Then
                ResultString += "   <pane>" & ControlChars.CrLf
                ResultString += "   <name>LeftPane</name>" & ControlChars.CrLf
                ResultString += "    <modules>" & ControlChars.CrLf
                ResultString += LeftPane & "    </modules>" & ControlChars.CrLf
                ResultString += "   </pane>" & ControlChars.CrLf
            End If

            If ContentPane <> "" Then
                ResultString += "   <pane>" & ControlChars.CrLf
                ResultString += "   <name>ContentPane</name>" & ControlChars.CrLf
                ResultString += "    <modules>" & ControlChars.CrLf
                ResultString += ContentPane & "    </modules>" & ControlChars.CrLf
                ResultString += "   </pane>" & ControlChars.CrLf
            End If


            If RightPane <> "" Then
                ResultString += "   <pane>" & ControlChars.CrLf
                ResultString += "   <name>RightPane</name>" & ControlChars.CrLf
                ResultString += "    <modules>" & ControlChars.CrLf
                ResultString += RightPane & "    </modules>" & ControlChars.CrLf
                ResultString += "   </pane>" & ControlChars.CrLf
            End If


            ResultString += "  </panes>" & ControlChars.CrLf
            ResultString += " </tab>"

            Return ResultString
        End Function
		
		
		
        Public Function CreateXLM(ByVal TModuleID As Integer) As String

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim _moduleSettings As ModuleSettings = PortalSettings.GetEditModuleSettings(TModuleID)
            Dim TempString As String
            Dim Dr As SqlDataReader
            TempString = "      <module>" & ControlChars.CrLf
            TempString += "     <title>" & _moduleSettings.ModuleTitle & "</title>" & ControlChars.CrLf
            TempString += "     <definition>" & _moduleSettings.FriendlyName & "</definition>" & ControlChars.CrLf
            TempString += "      <modulesettings>" & ControlChars.CrLf


            Dim settings As Hashtable = PortalSettings.GetModuleSettings(TModuleID)
            If settings.Count > 0 Then
                For Each de As DictionaryEntry In settings
                    TempString += "      <modulesetting>" & ControlChars.CrLf
                    TempString += "       <settingname>" & de.Key & "</settingname>" & ControlChars.CrLf
                    TempString += "       <settingvalue><![CDATA[" & de.Value & "]]></settingvalue>" & ControlChars.CrLf
                    TempString += "       </modulesetting>" & ControlChars.CrLf
                Next de
            Else
                TempString += "      <modulesetting>" & ControlChars.CrLf
                TempString += "       <settingname></settingname>" & ControlChars.CrLf
                TempString += "       <settingvalue></settingvalue>" & ControlChars.CrLf
                TempString += "       </modulesetting>" & ControlChars.CrLf
            End If

            TempString += "      </modulesettings>" & ControlChars.CrLf
            TempString += "        <datas>" & ControlChars.CrLf



            Select Case _moduleSettings.FriendlyName


                Case "Calendrier"
                    Dim events As New EventsDB()
                    ' Data1 = Description  Data2 = DateTime Data 3 = Title data4=ExpiryDate data5=icone data6=alttext
                    ' AddModuleEvent(ByVal ModuleId As Integer, ByVal Description As String, ByVal DateTime As Date, ByVal Title As String, ByVal ExpireDate As String, ByVal UserName As String, ByVal Every As String, ByVal Period As String, ByVal IconFile As String, ByVal AltText as String)
                    ' ts.Days
                    Dim ts As TimeSpan
                    Dim ts1 As TimeSpan
                    Dr = events.GetModuleEvents(TModuleID)
                    Dim X As Integer = 0
                    While Dr.Read
                        X = X + 1
                        ts = System.DateTime.op_Subtraction(Dr("DateTime"), DateTime.Now())
                        If Not IsDBNull(Dr("ExpireDate")) Then
                            ts1 = System.DateTime.op_Subtraction(Dr("ExpireDate"), DateTime.Now())
                        End If
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1><![CDATA[" & Dr("Description") & "]]></data1><data2>" & ts.Days.ToString & "</data2><data3><![CDATA[" & Dr("Title") & "]]></data3><data4>" & IIf(IsDBNull(Dr("ExpireDate")), "", ts1.Days.ToString) & "</data4><data5><![CDATA[" & Dr("IconFile") & "]]></data5><data6><![CDATA[" & Dr("altText") & "]]></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End While
                    If X = 0 Then
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1></data1><data2></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End If
                    Dr.Close()


                Case "Contacts"
                    ' Create an instance of the ContactsDB component
                    Dim contacts As New ContactsDB()
                    ' Data1 = Name  Data2 = Role Data3 = EMail Data4 = Contact1 Data5 = Contact2
                    Dr = contacts.GetContacts(TModuleID)
                    Dim X As Integer = 0
                    While Dr.Read
                        X = X + 1
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1><![CDATA[" & Dr("Name") & "]]></data1><data2><![CDATA[" & Dr("Role") & "]]></data2><data3><![CDATA[" & Dr("Email") & "]]></data3><data4><![CDATA[" & Dr("Contact1") & "]]></data4><data5><![CDATA[" & Dr("Contact2") & "]]></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End While
                    If X = 0 Then
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1></data1><data2></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End If
                    Dr.Close()


                Case "FAQs"
                    ' Create an instance of the FAQsDB component
                    Dim FAQs As New FAQsDB()
                    ' Data1 = QuestionField  Data2 = AnswerField
                    Dr = FAQs.GetFAQs(TModuleID)

                    Dim X As Integer = 0
                    While Dr.Read
                        X = X + 1
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1><![CDATA[" & Dr("Question") & "]]></data1><data2><![CDATA[" & Dr("Answer") & "]]></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End While
                    If X = 0 Then
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1></data1><data2></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End If
                    Dr.Close()

                Case "Hyperliens"
                    Dim links As New LinkDB()
                    ' Data1 = Title  Data2 = Link Data 3 = vieworder data4=description data5=newwindow
                    Dr = links.GetLinks(TModuleID)
                    Dim X As Integer = 0
                    While Dr.Read
                        X = X + 1
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1>" & Dr("Title") & "</data1><data2><![CDATA[" & Dr("Url") & "]]></data2><data3>" & IIf(IsDBNull(Dr("ViewOrder")), "", Dr("ViewOrder")) & "</data3><data4><![CDATA[" & Dr("Description") & "]]></data4><data5>" & Dr("NewWindow").ToString() & "</data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End While

                    If X = 0 Then
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1></data1><data2></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End If
                    Dr.Close()

                Case "HTML/Texte"
                    Dim objHTML As New HtmlTextDB()
                    ' Data1 = text  Data2 = AltSummary Data 3 = AltDetail
                    ' <![CDATA[ ]]>
                    Dr = objHTML.GetHtmlText(TModuleID)
                    If Dr.Read() Then
                        Dim Content As String
                        Content = HtmlDecode(CType(Dr("DesktopHtml"), String))
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1><![CDATA[" & Content & "]]></data1><data2></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    Else
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1></data1><data2></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End If
                    ' Close the datareader
                    Dr.Close()



                Case "Image"
                    TempString += "         <data>" & ControlChars.CrLf & "           <data1></data1><data2></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf

                Case "Forum"
                    TempString += "         <data>" & ControlChars.CrLf
                    TempString += "         <data1>" & ControlChars.CrLf
                    ' 0 TTTForum_GetUsers PortalID Filter
                    ' 1 get TTTForum_GetGroups portalid moduleid
                    ' 2 get TTTForum_GetForums groupid
                    ' 3 get TTTForum_GetThreads ForumID	PageSize PageIndex filter
                    ' 4 get TTTForum_GetPosts 	ThreadID ThreadPage PostsPerPage FlatView Filter
                    Dim dbForum As New ForumDB
                    TempString += "         <users>" & ControlChars.CrLf
                    ' getForum Users
                    Dim dbForumUser As New ForumUserDB()
                    Dr = dbForumUser.TTTForum_GetUsers(_portalSettings.PortalId, "")
                    While Dr.Read
                        TempString += "            <user>" & ControlChars.CrLf
                        TempString += "              <username><![CDATA[" & Dr("UserName").ToString() + "]]></username>" & ControlChars.CrLf
                        TempString += "              <alias><![CDATA[" & Dr("Alias").ToString() + "]]></alias>" & ControlChars.CrLf
                        TempString += "              <firstname><![CDATA[" & Dr("FirstName").ToString() + "]]></firstname>" & ControlChars.CrLf
                        TempString += "              <lastname><![CDATA[" & Dr("LastName").ToString() + "]]></lastname>" & ControlChars.CrLf
                        TempString += "              <email><![CDATA[" & Dr("Email").ToString() + "]]></email>" & ControlChars.CrLf
                        TempString += "              <istrusted>" & Dr("IsTrusted").ToString() + "</istrusted>" & ControlChars.CrLf
                        TempString += "            </user>" & ControlChars.CrLf
                    End While
                    Dr.Close()
                    TempString += "         </users>" & ControlChars.CrLf
                    TempString += "         <forumgroups>" & ControlChars.CrLf
                    Dr = dbForum.TTTForum_GetGroups(_portalSettings.PortalId, TModuleID)
                    While Dr.Read
                        TempString += "         <forumgroup>" & ControlChars.CrLf
                        TempString += "            <name><![CDATA[" & Dr("Name").ToString() + "]]></name>" & ControlChars.CrLf
                        '  Get Forum here
                        TempString += "                <forums>" & ControlChars.CrLf
                        Dim DrForum As SqlDataReader = dbForum.TTTForum_GetForums(Int32.Parse(Dr("ForumGroupId")))
                        While DrForum.Read
                            TempString += "                     <forum>" & ControlChars.CrLf
                            TempString += "                         <name><![CDATA[" & DrForum("Name").ToString() + "]]></name>" & ControlChars.CrLf
                            TempString += "                         <description><![CDATA[" & DrForum("Description").ToString() + "]]></description>" & ControlChars.CrLf
                            TempString += "                         <active><![CDATA[" & DrForum("IsActive").ToString() + "]]></active>" & ControlChars.CrLf
                            TempString += "                         <moderated><![CDATA[" & DrForum("IsModerated").ToString() + "]]></moderated>" & ControlChars.CrLf
                            TempString += "                         <private><![CDATA[" & DrForum("IsPrivate").ToString() + "]]></private>" & ControlChars.CrLf
                            ' Get Thread here

                            Dim DrThreads As SqlDataReader = dbForum.TTTForum_GetThreads(Int32.Parse(DrForum("ForumId")), 99999, 0, "")
                            TempString += "                                         <posts>" & ControlChars.CrLf
                            While DrThreads.Read

                                Dim DrPosts As SqlDataReader = dbForum.TTTForum_GetPosts(Int32.Parse(DrThreads("ThreadId")), 0, 99999, True, "")
                                While DrPosts.Read
                                    TempString += "                                             <post>" & ControlChars.CrLf
                                    TempString += "                                                 <thread>" + (DrPosts("ParentPostID") = 0).ToString() + "</thread>" & ControlChars.CrLf
                                    TempString += "                                                 <subject><![CDATA[" & DrPosts("Subject").ToString() + "]]></subject>" & ControlChars.CrLf
                                    TempString += "                                                 <alias><![CDATA[" & DrPosts("Alias").ToString() + "]]></alias>" & ControlChars.CrLf
                                    TempString += "                                                 <body><![CDATA[" & DrPosts("Body").ToString() + "]]></body>" & ControlChars.CrLf
                                    TempString += "                                             </post>" & ControlChars.CrLf
                                End While
                                ' Close the datareader
                                DrPosts.Close()

                            End While
                            TempString += "                                         </posts>" & ControlChars.CrLf
                            ' Close the datareader
                            DrThreads.Close()
                            TempString += "                     </forum>" & ControlChars.CrLf
                        End While
                        ' Close the datareader
                        DrForum.Close()
                        TempString += "                </forums>" & ControlChars.CrLf
                        TempString += "         </forumgroup>" & ControlChars.CrLf
                    End While
                    TempString += "         </forumgroups>" & ControlChars.CrLf
                    ' Close the datareader
                    Dr.Close()
                    TempString += "          </data1><data2></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf
                    TempString += "          </data>" & ControlChars.CrLf
                Case "Babillard"
                    Dim objAnnouncements As New AnnouncementsDB()
                    ' Data1 = Title  Data2 = expireddate Data 3 = description data4=Link 
                    Dr = objAnnouncements.GetAnnouncements(TModuleID)
                    Dim TempDate As String
                    Dim X As Integer = 0
                    While Dr.Read
                        If IsDBNull(Dr("ExpireDate")) Then
                            TempDate = ""
                        Else
                            TempDate = Format(CDate(Dr("ExpireDate")), "yyyyMMdd")
                        End If
                        X = X + 1
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1>" & Dr("Title") & "</data1><data2>" & TempDate & "</data2><data3><![CDATA[" & Dr("Description") & "]]></data3><data4><![CDATA[" & Dr("URL") & "]]></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End While
                    If X = 0 Then
                        TempString += "         <data>" & ControlChars.CrLf & "           <data1></data1><data2></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
                    End If
                    ' Close the datareader
                    Dr.Close()
                Case Else
                    TempString += "         <data>" & ControlChars.CrLf & "           <data1></data1><data2></data2><data3></data3><data4></data4><data5></data5><data6></data6>" & ControlChars.CrLf & "          </data>" & ControlChars.CrLf
            End Select


            TempString += "        </datas>" & ControlChars.CrLf
            TempString += "      </module>" & ControlChars.CrLf
            Return TempString

        End Function

        ' creates RRS files
        Public Sub CreateRSS(ByVal dr As SqlDataReader, ByVal TitleField As String, ByVal URLField As String, ByVal CreatedDateField As String, ByVal SyndicateField As String, ByVal DomainName As String, ByVal FileName As String)

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' create RSS file
            Dim strRSS As String = ""

            Dim strRelativePath As String = DomainName & Replace(Mid(FileName, InStr(1, FileName, "\Portals")), "\", "/")
            strRelativePath = Left(strRelativePath, InStrRev(strRelativePath, "/"))

            While dr.Read()
                If dr(SyndicateField) Then
                    strRSS += "      <item>" & ControlChars.CrLf
                    strRSS += "         <title>" & dr(TitleField).ToString & "</title>" & ControlChars.CrLf
                    If InStr(1, dr("URL").ToString, "://") = 0 Then
                        If IsNumeric(dr("URL").ToString) Then
                            strRSS += "         <link>" & DomainName & "/default.aspx?tabid=" & dr(URLField).ToString & "</link>" & ControlChars.CrLf
                        Else
						    If dr("URL").ToString <> "" then
                            strRSS += "         <link>" & strRelativePath & dr(URLField).ToString & "</link>" & ControlChars.CrLf
							else
							strRSS += "         <link>" & DomainName & "</link>" & ControlChars.CrLf
							end if
                        End If
                    Else
                        strRSS += "         <link>" & HTMLEncode(dr(URLField).ToString) & "</link>" & ControlChars.CrLf
                    End If
                    strRSS += "         <description>" & _portalSettings.PortalName & " " & GetMediumDate(dr(CreatedDateField).ToString) & "</description>" & ControlChars.CrLf
                    strRSS += "     </item>" & ControlChars.CrLf
                End If
            End While
            dr.Close()

            If strRSS <> "" Then
                strRSS = "<?xml version=""1.0"" encoding=""utf-8""?>" & ControlChars.CrLf & _
                    "<rss version=""2.0"" xmlns:wfw=""http://wellformedweb.org/CommentAPI/"" xmlns:slash=""http://purl.org/rss/1.0/modules/slash/"">" & ControlChars.CrLf & _
                    "  <channel>" & ControlChars.CrLf & _
                    "     <title>" & _portalSettings.PortalName & "</title>" & ControlChars.CrLf & _
                    "     <link>" & DomainName & "</link>" & ControlChars.CrLf & _
                    "     <description>" & _portalSettings.PortalName & "</description>" & ControlChars.CrLf & _
                    "     <language>" & GetLanguage("N") & "</language>" & ControlChars.CrLf & _
                    "     <copyright>" & _portalSettings.FooterText & "</copyright>" & ControlChars.CrLf & _
                    "     <webMaster>" & _portalSettings.Email & "</webMaster>" & ControlChars.CrLf & _
                    strRSS & _
                    "   </channel>" & ControlChars.CrLf & _
                    "</rss>"

                Dim objStream As StreamWriter
                objStream = File.CreateText(FileName)
                objStream.WriteLine(strRSS)
                objStream.Close()
            Else
                If File.Exists(FileName) Then
                    File.Delete(FileName)
                End If
            End If

        End Sub

        ' creates ForumRSS files
        Public Sub CreateForumRSS(ByVal dr As SqlDataReader, ByVal TitleField As String, ByVal DescriptionField As String, ByVal URLField As String, ByVal CreatedDateField As String, ByVal ForumName As String, ByVal FileName As String)

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' create RSS file
            Dim strRSS As String = ""

            While dr.Read()
                    strRSS += "      <item>" & ControlChars.CrLf
                    strRSS += "         <title><![CDATA["  & GetMediumDate(dr(CreatedDateField).ToString) & " " & GetLanguage("F_Auteur") & " : " & dr(DescriptionField).ToString & " " & GetLanguage("F_Object") & " : " & dr(TitleField).ToString & "]]></title>" & ControlChars.CrLf
                    strRSS += "         <link>" & HTMLEncode(dr(URLField).ToString) & "</link>" & ControlChars.CrLf
                    strRSS += "         <description><![CDATA[" & HtmlDecode(dr("Body").ToString) & "]]></description>" & ControlChars.CrLf
                    strRSS += "     </item>" & ControlChars.CrLf
            End While
            dr.Close()

            If strRSS <> "" Then
                strRSS = "<?xml version=""1.0"" encoding=""utf-8""?>" & ControlChars.CrLf & _
                    "<rss version=""2.0"" xmlns:wfw=""http://wellformedweb.org/CommentAPI/"" xmlns:slash=""http://purl.org/rss/1.0/modules/slash/"">" & ControlChars.CrLf & _
                    "  <channel>" & ControlChars.CrLf & _
                    "     <title>" & _portalSettings.PortalName & "</title>" & ControlChars.CrLf & _
                    "     <link>" & GetPortalDomainName(_portalSettings.PortalAlias, Request) &  "</link>" & ControlChars.CrLf & _
                    "     <description>" & _portalSettings.PortalName & " " & ForumName & "</description>" & ControlChars.CrLf & _
                    "     <language>" & GetLanguage("N") & "</language>" & ControlChars.CrLf & _
                    "     <copyright>" & _portalSettings.FooterText & "</copyright>" & ControlChars.CrLf & _
                    "     <webMaster>" & _portalSettings.Email & "</webMaster>" & ControlChars.CrLf & _
                    strRSS & _
                    "   </channel>" & ControlChars.CrLf & _
                    "</rss>"

                Dim objStream As StreamWriter
                objStream = File.CreateText(FileName)
                objStream.WriteLine(strRSS)
                objStream.Close()
            Else
                If File.Exists(FileName) Then
                    File.Delete(FileName)
                End If
            End If

        End Sub		
		


        ' returns the database connection string
        Public Function GetDBConnectionString() As String

			if HTTPContext.Current.Items("ConnectionString") is nothing then
                Dim DomainName As String = GetDomainName(HttpContext.Current.Request).ToLower()
                Dim URL() As String
                Dim intURL As Integer
                URL = Split(DomainName, ".")
			  ' strip the www or subdomain
	   			if URL.GetUpperBound(0) > 0 then
	   				For intURL = 0 To (URL.GetUpperBound(0))
	   				If DomainName.Length > 5 then
                            If Not ConfigurationSettings.AppSettings(DomainName) Is Nothing Then
                                HttpContext.Current.Items("ConnectionString") = ConfigurationSettings.AppSettings(DomainName)
                                Exit For
                            End If
	   				DomainName =  replace(DomainName,  url(intURL) & "." , "")
	   				end if
       		 		Next intURL
	   			end if
		
				if HTTPContext.Current.Items("ConnectionString") is nothing then
                    HttpContext.Current.Items("ConnectionString") = ConfigurationSettings.AppSettings("connectionString")
	    	 	end if
			End If
		Return HTTPContext.Current.Items("ConnectionString")
        End Function

        Public Function GetDBname() As String
		if HTTPContext.Current.Items("DBName") is nothing then
			Dim TempName As String = GetDBConnectionString.tolower()
			Dim ResultString As String = "Portal"
			Try
 			Dim RegexObj As New Regex("database=\b[A-Z0-9_a-z][A-Z0-9_a-z][A-Z0-9_a-z]{1,40}\b;", RegexOptions.IgnoreCase)
			ResultString = RegexObj.Match(TempName).Value
 			ResultString = Replace(ResultString, "database=", "")
			ResultString = Replace(ResultString, ";", "")
			Catch ex As ArgumentException
			'Syntax error in the regular expression
			End Try
			HTTPContext.Current.Items("DBName") = ResultString
		 End if	
			Return HTTPContext.Current.Items("DBName")
        End Function

		
		Public Function ReturnGalleryToolTip(ByVal Url As String, optional ByVal Width As String = "", optional ByVal Height As String = "") As String
		url = replace(url, "~/", "")
		If InStr(1, Url.ToLower, ".jpg") <> 0 or InStr(1, Url.ToLower, ".gif") <> 0 or InStr(1, Url.ToLower, ".bmp") <> 0 or InStr(1, Url.ToLower, ".png") <> 0 or InStr(1, Url.ToLower, ".tif") <> 0 then
		Dim TempWidth as integer = 0
		Dim TempHeight as integer = 0
		Dim SRatio As double 
		Dim OptionalString as String = ""
		If Width <> "" and Height <> "" then
		TempWidth = Ctype(Width, integer)
		TempHeight = CType(Height, integer)
	     If Not (TempWidth <= 250 AndAlso TempHeight <= 250) Then
                    sRatio = (TempHeight / TempWidth)
                    If sRatio > 1 Then ' Bounded by height
					    TempWidth = CShort(250 / sRatio)
                        TempHeight = 250
                    Else 'Bounded by width
                        TempWidth = 250
                        TempHeight = CShort(250 * sRatio)
                    End If
 		End If
		OptionalString = " Width=" & TempWidth.ToString & " Height=" & TempHeight.ToString & " "
		end if
		Return ReturnToolTip("<img src='" & Url & "' " & OptionalString & " alt='*' border=0>", TempWidth.ToString(), "Oui")
		else
		Return ReturnToolTip(Url)
		end if
        End Function

		
		
	Public ReadOnly Property ReturnToolTip(ByVal strKeyValue As String, optional ByVal Width As String = "", optional ByVal Sticky As String = "") As String
            Get
			if strKeyValue <> "" then
			Dim ToolWidth As String = Width
			If ToolWidth = ""  then
			Select Case strKeyValue.length
			case 0 
			ToolWidth = "10"
			case  1 to 30  
			ToolWidth = (strKeyValue.length * 7)
			case  31 to 50  
			ToolWidth = "210"
			case   51 to 200  
			ToolWidth = "250"
			case   201 to 500     
			ToolWidth = "400"
			case   501 to 2000    
			ToolWidth = "500"
			case else
			ToolWidth = "600"
			end select
			end if
			If Sticky = "" then
			   Return "this.T_WIDTH="& ToolWidth & ";return escape('" & RTESafe(strKeyValue) & "')"
			else
			Return "this.T_STICKY=true;this.T_WIDTH="& ToolWidth & ";return escape('" & RTESafe(strKeyValue) & "')"
			end if
 		   Return "this.T_WIDTH="& ToolWidth & ";return escape('" & RTESafe(strKeyValue) & "')"
            end if
			End Get
	End Property
		
	
		Public function IsNotAlpha(ByVal strToCheck As String)	as Boolean
		Dim FoundMatch As Boolean
		Try
			FoundMatch = Regex.IsMatch(strToCheck, "[^a-zA-Z0-9 -_]")
		Catch ex As ArgumentException
		'Syntax error in the regular expression
		End Try	
		IsNotAlpha = FoundMatch
		End function	

		Public function RTESafe(ByVal strText As String) as string
			'returns safe code for preloading in the RTE
			dim tmpString as string
			tmpString = trim(strText)
 			tmpString = tmpString.replace("\'", "[ESCAPE]")
 			tmpString = tmpString.replace("\", "\\")
			tmpString = tmpString.replace("'", "\'")
			tmpString = tmpString.replace("""", "¨")
			'replace carriage returns & line feeds & tab and ff
			tmpString = replace(tmpString, chr(11), "\t")
			tmpString = replace(tmpString, chr(10), "\n")
			tmpString = replace(tmpString, chr(13), "\r")
			tmpString = replace(tmpString, chr(12), "\f")
			RTESafe = tmpString
		end function				

		
        Function GetDocument() As String
		    Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim TempURL As String = "/"
            If _portalSettings.activetab.ShowFriendly And _portalSettings.activetab.FriendlyTabName <> "" Then
                TempURL += GetLanguage("N") & "." & _portalSettings.activetab.FriendlyTabName & ".aspx"
            Else
                TempURL += GetLanguage("N") & ".default.aspx"
            End If
	
            Return TempURL
        End Function 

        Function GetFullDocument() As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim TempURL As String = HttpContext.Current.Request.ApplicationPath
            If Not TempURL.EndsWith("/") Then
                TempURL += "/"
            End If
            If _portalSettings.ActiveTab.ShowFriendly And _portalSettings.ActiveTab.FriendlyTabName <> "" Then
                TempURL += GetLanguage("N") & "." & _portalSettings.ActiveTab.FriendlyTabName & ".aspx"
            Else
                TempURL += GetLanguage("N") & ".default.aspx"
            End If

            Return TempURL
        End Function
		
        Function FormatFriendlyURL(ByVal TabName As String, ByVal UseTabName As Boolean, ByVal TabId As String, Optional ByVal Options As String = "", Optional ByVal strLangCode As String = "", Optional ByVal amp As String = "&") As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim ServerPath As String
            If strLangCode = "" Then
                strLangCode = GetLanguage("N")
            End If
            ServerPath = IIf(HttpContext.Current.Request.ApplicationPath = "/", "", HttpContext.Current.Request.ApplicationPath)
            If Not ServerPath.EndsWith("/") Then
                ServerPath += "/"
            End If
            If UseTabName Then
                If Options <> "" Then
                    If _portalSettings.PortalChild Then
                        Return ServerPath & strLangCode & "." & TabName & ".aspx?tabid=" & TabId & amp & Options
                    Else
                        Return ServerPath & strLangCode & "." & TabName & ".aspx" & "?" & Options
                    End If
                Else
                    If _portalSettings.PortalChild Then
                        Return ServerPath & strLangCode & "." & TabName & ".aspx?tabid=" & TabId
                    Else
                        Return ServerPath & strLangCode & "." & TabName & ".aspx"
                    End If
                End If
            Else
                If Options <> "" Then
                    Return ServerPath & strLangCode & ".default.aspx?tabid=" & TabId & amp & Options
                Else
                    Return ServerPath & strLangCode & ".default.aspx?tabid=" & TabId
                End If
            End If
        End Function

		Public function TrueFalse(ByVal WhatIs As Boolean) As String
		If WhatIs then
		Return GetLanguage("True")
		else
		Return GetLanguage("False")
		end if
		end Function
		
		Public function SafeRTE(ByVal strText As String) as string
			'returns safe code for preloading in the RTE
			dim tmpString as string
			tmpString = trim(strText)
			tmpString = tmpString.replace("\'", "'")
			tmpString = tmpString.replace("[ESCAPE]", "\'")	
	
			'replace carriage returns & line feeds & tab and ff
			tmpString = replace(tmpString, "\t" , chr(11))
			tmpString = replace(tmpString, "\n" , chr(10))
			tmpString = replace(tmpString,  "\r", chr(13))
			tmpString = replace(tmpString, "\f" , chr(12))
			SafeRTE = tmpString
		end function				
		
        ' uses recursion to search the control hierarchy for a specific control based on controlname
        Public Function FindControlRecursive(ByVal objControl As Control, ByVal strControlName As String) As Control
            If objControl.Parent Is Nothing Then
                Return Nothing
            Else
                If Not objControl.Parent.FindControl(strControlName) Is Nothing Then
                    Return objControl.Parent.FindControl(strControlName)
                Else
                    Return FindControlRecursive(objControl.Parent, strControlName)
                End If
            End If
        End Function

		Public Function DisplayCountrycode(ByVal hostIPAddress As String) As String
				Dim _CountryLookup As DotNetNuke.CountryLookup
				Dim context As HttpContext = HttpContext.Current
				'Check to see if we are using the Cached
				'version of the GeoIPData file
				If Context.Cache.Get("GeoIPData") Is Nothing Then
   					Context.Cache.Insert("GeoIPData", DotNetNuke.CountryLookup.FileToMemory(Context.Server.MapPath("bin/GeoIP.dat")), New CacheDependency(Context.Server.MapPath("bin/GeoIP.dat")))
				End If
				_CountryLookup = New DotNetNuke.CountryLookup(CType(Context.Cache.Get("GeoIPData"), System.IO.MemoryStream))
 
				Dim _UserCountryCode As String = _CountryLookup.LookupCountryCode(hostIPAddress)
				Return _UserCountryCode 
		End Function

		Public Function DisplayCountryName(ByVal hostIPAddress As String) As String
				Dim _CountryLookup As DotNetNuke.CountryLookup
				Dim context As HttpContext = HttpContext.Current
				'Check to see if we are using the Cached
				'version of the GeoIPData file
				If Context.Cache.Get("GeoIPData") Is Nothing Then
   					Context.Cache.Insert("GeoIPData", DotNetNuke.CountryLookup.FileToMemory(Context.Server.MapPath("bin/GeoIP.dat")), New CacheDependency(Context.Server.MapPath("bin/GeoIP.dat")))
				End If
				_CountryLookup = New DotNetNuke.CountryLookup(CType(Context.Cache.Get("GeoIPData"), System.IO.MemoryStream))
 
				Dim _UserCountryName As String = _CountryLookup.LookupCountryName(hostIPAddress)
				Return _UserCountryName 
		End Function

		Public Function IPConvert(IPAddress As Object) As Object

	    Dim x       As Integer
    	Dim Pos     As Integer
    	Dim PrevPos As Integer
    	Dim Num     As Integer

	    If IsNumeric(IPAddress) Then
    	    IPConvert = "0.0.0.0"
        	For x = 1 To 4
            	Num = Int(IPAddress / 256 ^ (4 - x))
            	IPAddress = IPAddress - (Num * 256 ^ (4 - x))
            		If Num > 255 Then
                		IPConvert = "0.0.0.0"
                	Exit Function
            		End If

            	If x = 1 Then
                	IPConvert = Num
            	Else
        	        IPConvert = IPConvert & "." & Num
    	        End If
	        Next
	    ElseIf UBound(Split(IPAddress, ".")) = 3 Then
	'   	     On Error Resume Next
    	    	For x = 1 To 4
        	    Pos = InStr(PrevPos + 1, IPAddress, ".", 1)
            	If x = 4 Then Pos = Len(IPAddress) + 1
				If IsNumeric(Mid(IPAddress, PrevPos + 1, Pos - PrevPos - 1)) Then
            	Num = Int(Mid(IPAddress, PrevPos + 1, Pos - PrevPos - 1))
            	If Num > 255 Then
                	IPConvert = "0"
                	Exit Function
            	End If
            PrevPos = Pos
            IPConvert = ((Num Mod 256) * (256 ^ (4 - x))) + IPConvert
			else
            IPConvert = "0"
            Exit Function
			end if
    	    Next
	    End If

	End Function

	Public Sub CopyFileRecursively(ByVal strRoot As String, ByVal ToStrRoot As String)
		    If strRoot <> "" Then
                Dim strFolder As String
                If System.IO.Directory.Exists(strRoot) Then
					If Not System.IO.Directory.Exists(TostrRoot) Then
		                System.IO.Directory.CreateDirectory(TostrRoot)
					End IF
                Dim fileEntries As String() = System.IO.Directory.GetFiles(strRoot)
				Dim strFileName As String
                For Each strFileName In fileEntries
				If not System.IO.File.Exists(strFileName.replace(strRoot, ToStrRoot)) then
				' Only copy if file does not exist
				System.IO.File.Copy(strFileName, strFileName.replace(strRoot, ToStrRoot))
				End If
                Next strFileName
 
                For Each strFolder In System.IO.Directory.GetDirectories(strRoot)
				' StrFolder - StrRoot + ToStrRoot
                CopyFileRecursively(strFolder, strFolder.replace(strRoot, ToStrRoot))
                Next
               End If
            End If
     End Sub

	Public Function ProcessLanguage(ByVal Item As String, Optional ByVal Page As Page = Nothing) As String
	If Item <> "" then
 	Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
	item = Regex.Replace(item, "{PortalName}" , _portalSettings.PortalName, RegexOptions.IgnoreCase)
	item = Regex.Replace(item, "{Year}" , Year(Now()).Tostring, RegexOptions.IgnoreCase)
	If Not Page is Nothing then
	Dim arrEmail As Array
	If _portalSettings.Email.IndexOf("@") > 1 then 
        arrEmail = Split(_portalSettings.Email, "@")
		item = Regex.Replace( item, "{AdministratorEmail}" ,  "<script type=""text/javascript"" language=""Javascript"">protect(""" + arrEMail(0) + """, """ + arrEMail(1) + """, """ + _portalSettings.PortalName + """);</script>", RegexOptions.IgnoreCase)

	End if

	If portalSettings.GetHostSettings("HostEmail").IndexOf("@") > 1 then 
        arrEmail = Split(portalSettings.GetHostSettings("HostEmail"), "@")
		item = Regex.Replace( item, "{HostEmail}" ,  "<script type=""text/javascript"" language=""Javascript"">protect(""" + arrEMail(0) + """, """ + arrEMail(1) + """, """ + portalSettings.GetHostSettings("HostTitle").ToString + """);</script>", RegexOptions.IgnoreCase)

	End if
	else
	item = Regex.Replace(item, "{HostEmail}" , portalSettings.GetHostSettings("HostEmail"), RegexOptions.IgnoreCase)
	item = Regex.Replace(item, "{AdministratorEmail}" , _portalSettings.Email, RegexOptions.IgnoreCase)
	End if

	
	
	if (Regex.IsMatch(item, "{Date}", RegexOptions.IgnoreCase) = true) then
	if GetLanguage("culturecode") <> "" then
	System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(GetLanguage("culturecode"), False)
	end if
	Dim myDTFI As System.Globalization.DateTimeFormatInfo = New System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, False).DateTimeFormat
	item = Regex.Replace(item, "{Date}" , Format(Now().AddMinutes(GetTimeDiff(_portalSettings.TimeZone)),  myDTFI.LongDatePattern), RegexOptions.IgnoreCase)
    End if
	item = Regex.Replace(item, "{httplogin}" , GetPortalDomainName(PortalAlias, Request) & GetDocument() & "?tabid=" & TabId & "&showlogin=1", RegexOptions.IgnoreCase)
	item = Regex.Replace(item, "{httpregister}" , GetPortalDomainName(PortalAlias) & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Register", RegexOptions.IgnoreCase)
	end if
	Return item
	end function
	 
	 
	 Public Function GetLanguage(ByVal Item As String) As String
	 Dim _language As HashTable = HttpContext.Current.Items("Language")
	 Dim TempString As STring
	    If _language.ContainsKey(item) Then
            TempString = _language(item)
        Else
		    if Item = "culturecode" then
			TempString = "fr-ca"
			else
           	TempString = "-" + Item + "-"
			Dim Admin as new AdminDB()
			Admin.UpdatelanguageContext(GetLanguage("N"), Item, Item, "New")
			if portalSettings.GetHostSettings("EnableErrorReporting") <> "N" and _language.ContainsKey("N") then
			Dim TempMessage As String
			If _language.ContainsKey("LanguageERROR") then
			TempMessage = String.Format(_language("LanguageERROR"), Item, _language("N"))
			else 
			TempMessage = "The following word - " & Item & " - is missing from the language table " & _language("N")
			end if
			SendNotification(portalSettings.GetHostSettings("HostEmail"), portalSettings.GetHostSettings("HostEmail"), "", Item, TempMessage , "")
			end if
			end if
        End If

	try	 
	 If PortalSecurity.IsSuperUser then
	 	Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		     Dim Zpagelanguage As HashTable
    		 If not HttpContext.Current.Session("LanguageTable") is nothing then
				Zpagelanguage = HttpContext.Current.Session("LanguageTable")
			 else
       			Zpagelanguage = New Hashtable()
			 end if
			 If Zpagelanguage(item) is nothing and not item = "language" and not item = "N" and not item = "culturecode" then
	         Zpagelanguage.Add(Item, TempString)
			 end if
			 HttpContext.Current.Session("LanguageTable") =  Zpagelanguage
	end if
    Catch objException As Exception
    
	end try
	   	Return TempString
	   
	 end function
	
	Public Function GetAdminPage() As String
	Return iif(HttpContext.Current.Request.Params("adminpage") Is Nothing, "", "adminpage=" & HttpContext.Current.Request.Params("adminpage")) & iif(HttpContext.Current.Request.Params("hostpage") Is Nothing, "", "&hostpage=" & HttpContext.Current.Request.Params("hostpage"))
	end function
	 
	 Public Function FindUserLanguage(ByVal DefaultLanguage As String, ByVal ProposedLanguage As String, ByVal AuthLanguage As String) As String
		'find language by elimination
		
			Dim Strlanguage As String= ""
			Dim LanguageHsh As New Hashtable()
			Dim objAdmin As New AdminDB()
			Dim TempKey as String = GetDBname & "Alanguage"
			Dim context As HttpContext = HttpContext.Current
			Dim _Alanguage as Hashtable
            _Alanguage = CType(Context.Cache(TempKey), Hashtable)

             If _Alanguage Is Nothing Then
            ' If this object has not been instantiated yet, we need to grab it
            _Alanguage = New Hashtable()
			end if
			' see if the proposed language can be used
				If ProposedLanguage <> "" then
                strlanguage = ProposedLanguage.ToLower
				if InStr(1, AuthLanguage, Strlanguage & ";") or AuthLanguage = "" then
					If _Alanguage(strlanguage) = "OK" then
					return ProposedLanguage
					end if
					If _Alanguage(strlanguage) is nothing then
						LanguageHsh = objAdmin.GetlanguageSettings(strlanguage)
						If LanguageHsh.count > 0 then
							_Alanguage.Add(strlanguage, "OK")
							Context.Cache.insert(TempKey, _Alanguage, Nothing)
							Return strLanguage
						else
						_Alanguage.Add(strlanguage, "NO")
	 					Context.Cache.insert(TempKey, _Alanguage, Nothing)
						end if
					end if
				end if
				end if			
			
            ' The language was set in the querystring so use it first
            If Not (Context.Request.Params("language") Is Nothing) Then
				If Context.Request.Params("language") <> "" then
                strlanguage = Context.Request.Params("language").ToLower
				if InStr(1, AuthLanguage, Strlanguage & ";") or AuthLanguage = "" then
					If _Alanguage(strlanguage) = "OK" then
					return strlanguage
					end if
					If _Alanguage(strlanguage) is nothing then
						LanguageHsh = objAdmin.GetlanguageSettings(strlanguage)
						If LanguageHsh.count > 0 then
							_Alanguage.Add(strlanguage, "OK")
							Context.Cache.insert(TempKey, _Alanguage, Nothing)
							Return strLanguage
						else
						_Alanguage.Add(strlanguage, "NO")
	 					Context.Cache.insert(TempKey, _Alanguage, Nothing)
						end if
					end if
				end if
				end if
            End If

            Dim userLang As String()
			userLang = HttpContext.Current.Request.UserLanguages
            If Not userLang Is Nothing Then
                Dim Count As integer
                For count = 0 To userLang.GetUpperBound(0)
                    If InStr(1, userLang(count), ";") Then
                        Strlanguage = Left(userLang(count), InStrRev(userLang(count), ";") - 1).ToLower
                    Else
                        Strlanguage = userLang(count).Tolower
                    End If
                    If InStr(1, AuthLanguage, Strlanguage & ";") Or AuthLanguage = "" Then
                        If _Alanguage(Strlanguage) = "OK" Then
                            Return Strlanguage
                        End If
                        If _Alanguage(Strlanguage) Is Nothing Then
                            LanguageHsh = objAdmin.GetlanguageSettings(Strlanguage)
                            If LanguageHsh.Count > 0 Then
                                _Alanguage.Add(Strlanguage, "OK")
                                context.Cache.Insert(TempKey, _Alanguage, Nothing)
                                Return Strlanguage
                            Else
                                _Alanguage.Add(Strlanguage, "NO")
                                context.Cache.Insert(TempKey, _Alanguage, Nothing)
                            End If
                        End If
                    End If
                    If InStr(1, Strlanguage, "-") Then
                        Strlanguage = Left(userLang(count), InStrRev(userLang(count), "-") - 1).ToLower
                        If InStr(1, AuthLanguage, Strlanguage & ";") Or AuthLanguage = "" Then
                            If _Alanguage(Strlanguage) = "OK" Then
                                Return Strlanguage
                            End If
                            If _Alanguage(Strlanguage) Is Nothing Then
                                LanguageHsh = objAdmin.GetlanguageSettings(Strlanguage)
                                If LanguageHsh.Count > 0 Then
                                    _Alanguage.Add(Strlanguage, "OK")
                                    context.Cache.Insert(TempKey, _Alanguage, Nothing)
                                    Return Strlanguage
                                Else
                                    _Alanguage.Add(Strlanguage, "NO")
                                    context.Cache.Insert(TempKey, _Alanguage, Nothing)
                                End If
                            End If
                        End If
                    End If
                Next count
            End If
			Return  DefaultLanguage
	end function

        Private Function IsInTabRoles(ByVal roles As String) As Boolean

            Dim role As String
            For Each role In roles.Split(New Char() {";"c})
                If role <> "" And Not role Is Nothing And _
                    (role = glbRoleUnauthUser Or _
                    role = glbRoleAllUsers) Then
                    Return True
                End If
            Next role

            Return False

        End Function


        Private Function Make404Menu(ByVal Request As HttpRequest) As String
            Dim DomainName As String = GetDomainName(Request)
            Dim PortalNumber As Integer = PortalSettings.GetPortalByAlias(DomainName)

            If PortalNumber = -1 And InStr(1, DomainName, "/") <> 0 Then
                DomainName = Left(DomainName, InStr(DomainName, "/") - 1)
                PortalNumber = PortalSettings.GetPortalByAlias(DomainName)
            End If


            Dim TempString As StringBuilder = New System.Text.StringBuilder()
            TempString.Append("<p align=""left""><b><font color=""#003399"" size=""5"">" & DomainName & "</font></b></p>")

            If PortalNumber <> -1 Then

                If HttpContext.Current.Items("Language") Is Nothing Then
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
                        TempLanguage = FindUserLanguage(tsettings("language"), "", TempAuthLanguage)
                    Else
                        ' use default language
                        TempLanguage = FindUserLanguage("fr", "", TempAuthLanguage)
                    End If
                    LanguageHash = objAdmin.GetlanguageSettings(TempLanguage)
                    If Not LanguageHash.ContainsKey("N") Then
                        LanguageHash.Add("N", TempLanguage)
                    End If


                    ' put language setting in memory

                    HttpContext.Current.Items.Add("Language", LanguageHash)

                    ' end language	
                End If

                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                If _portalSettings Is Nothing Then
                    _portalSettings = New PortalSettings(0, DomainName, Request.ApplicationPath, GetLanguage("N"))
                    HttpContext.Current.Items.Add("PortalSettings", _portalSettings)
                End If



                ' Build list of tabs to be shown to user
                Dim authorizedTabs As New ArrayList()
                Dim DesktopTabs As ArrayList = PortalSettings.Getportaltabs(PortalNumber, GetLanguage("N"))
                Dim i As Integer
                For i = 0 To DesktopTabs.Count - 1
                    Dim tab As TabStripDetails = CType(DesktopTabs(i), TabStripDetails)
                    If tab.IsVisible Then
                        If IsInTabRoles(tab.AuthorizedRoles) = True Then
                            authorizedTabs.Add(tab)
                        End If
                    End If
                Next i


                Dim objTab As TabStripDetails
                Dim NextTab As TabStripDetails
                Dim LastLevel As Integer = -1
                Dim NextLevel As Integer = 0
                Dim TabLevel As Integer = 0
                Dim DifLevel As Integer = 0
                Dim Vbtabint As Integer = 0
                Dim IntTab As Integer = 0
                Dim IntMenu As Integer = 0

                ' For Each objTab In authorizedTabs
                TempString.Append("<table width=""750"" cellpadding=""0"" cellspacing=""0"" border=""0"">" & vbCrLf & "<tr><td>" & vbCrLf)
                For IntTab = 0 To authorizedTabs.Count - 1
                    objTab = CType(authorizedTabs(IntTab), TabStripDetails)


                    If objTab.Level > LastLevel Or objTab.Level = 0 Then
                        If objTab.Level > LastLevel Then
                            TempString.Append(vbCrLf)
                        End If
                        For Vbtabint = 0 To objTab.Level
                            TempString.Append(vbTab)
                        Next

                        If objTab.Level = 0 Then
                            NextLevel = 0
                            IntMenu = IntMenu + 1
                            'TempString.Append("<td>" & vbCrLf & vbTab)
                        End If
                        If objTab.Level = 0 Or objTab.Level = 1 Then
                            TempString.Append("<ul>" & vbCrLf)
                        Else
                            TempString.Append("<ul>" & vbCrLf)
                            NextLevel = NextLevel + 1
                        End If
                    End If


                    For Vbtabint = 0 To objTab.Level + 1
                        TempString.Append(vbTab)
                    Next




                    If IntTab + 1 <= authorizedTabs.Count - 1 Then
                        NextTab = CType(authorizedTabs(IntTab + 1), TabStripDetails)
                    Else
                        NextTab = CType(authorizedTabs(authorizedTabs.Count - 1), TabStripDetails)
                    End If



                    If objTab.DisableLink Then
                        TempString.Append("<li><a href="""">")
                    Else
                        TempString.Append("<li><a href=""" & FormatFriendlyURL(objTab.FriendlyTabName, objTab.ShowFriendly, objTab.TabId.ToString) & """>")
                    End If


                    TempString.Append(objTab.TabName & "</a>")

                    LastLevel = objTab.Level


                    If LastLevel >= NextTab.Level Then
                        TempString.Append("</li>" & vbCrLf)
                    End If


                    If LastLevel > NextTab.Level Then
                        For DifLevel = 1 To LastLevel - NextTab.Level
                            For Vbtabint = 0 To (LastLevel - DifLevel) + 1
                                TempString.Append(vbTab)
                            Next
                            TempString.Append("</ul>" & vbCrLf)
                            For Vbtabint = 0 To (LastLevel - DifLevel) + 1
                                TempString.Append(vbTab)
                            Next
                            TempString.Append("</li>" & vbCrLf)
                        Next
                    End If

                    If NextTab.Level = 0 Then
                        TempString.Append(vbTab & "</ul>" & vbCrLf)
                        'TempString.Append(vbTab & "</td>" & vbCrLf)
                    End If


                Next

                If LastLevel > 0 Then
                    For DifLevel = 0 To LastLevel
                        For Vbtabint = 0 To (LastLevel - DifLevel)
                            TempString.Append(vbTab)
                        Next
                        TempString.Append("</ul>" & vbCrLf)
                    Next
                    ' TempString.Append("</td>" & vbCrLf)
                End If
                TempString.Append("</td></tr>" & vbCrLf & "</table>" & vbCrLf)
                TempString.Append("<table width=""750"" cellpadding=""0"" cellspacing=""0"" border=""0"">" & vbCrLf & "<tr><td align=""center"">" & vbCrLf)
                If PortalSettings.GetSiteSettings(_portalSettings.PortalId).ContainsKey(GetLanguage("N") & "_FooterText") Then
                    If PortalSettings.GetSiteSettings(_portalSettings.PortalId)(GetLanguage("N") & "_FooterText") <> "" Then
                        TempString.Append(PortalSettings.GetSiteSettings(_portalSettings.PortalId)(GetLanguage("N") & "_FooterText"))
                    End If
                Else
                    If _portalSettings.FooterText <> "" Then
                        TempString.Append(_portalSettings.FooterText)

                    Else
                        TempString.Append("Copyright (c) " & Year(Now()) & " " & _portalSettings.PortalName)
                    End If
                End If
                TempString.Append("</td></tr>" & vbCrLf & "</table>" & vbCrLf)
                TempString.Append("<table  width=""750"" cellpadding=""0"" cellspacing=""0"" border=""0"">" & vbCrLf & "<tr><td align=""center"">" & vbCrLf)

                TempString.Append("<a href=""" & AddHTTP(PortalSettings.GetHostSettings("HostURL")) & """>" & Replace(GetLanguage("hypHost"), "{hostname}", PortalSettings.GetHostSettings("HostTitle")) & "</a>")
                TempString.Append("</td>" & vbCrLf & "<td align=""center"">" & vbCrLf)

                TempString.Append("<a href=""" & GetDocument() & "?edit=control&amp;tabid=" & _portalSettings.ActiveTab.TabId & "&amp;def=Terms"">")
                TempString.Append(GetLanguage("hypTerms") & "</a>")
                TempString.Append("</td>" & vbCrLf & "<td align=""center"">" & vbCrLf)

                TempString.Append("<a href=""" & GetDocument() & "?edit=control&amp;tabid=" & _portalSettings.ActiveTab.TabId & "&amp;def=Privacy"">")
                TempString.Append(GetLanguage("hypPrivacy") & "</a>")
                TempString.Append("</td></tr>" & vbCrLf & "</table>" & vbCrLf)



            End If

            Return TempString.ToString()

        End Function


        Public Sub SendHttpException(ByVal Errornb As String, ByVal Errortxt As String, ByVal Request As HttpRequest, Optional ByVal ErrorMessage As String = "")
            If Errornb = "404" And ErrorMessage = "" Then
                ErrorMessage = Make404Menu(Request)
            End If
            Dim strHTML As String = GetXMLmessage(Errornb, Request)
            If Not ErrorMessage <> "" Then
                HttpContext.Current.Response.StatusCode = Errornb
                HttpContext.Current.Response.StatusDescription = Errortxt
            End If
            strHTML = strHTML.Replace("[errormessage]", ErrorMessage)
            HttpContext.Current.Response.Write(strHTML)
            HttpContext.Current.Response.End()
        End Sub


        Public Function GetXMLmessage(ByVal Key As String, ByVal Request As HttpRequest) As String
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(Request.MapPath("/dnz.config"))
            Dim elem As XmlElement = Nothing
            Dim Language As String = ""
            Dim userLang As String()

            userLang = HttpContext.Current.Request.UserLanguages
            If Not userLang Is Nothing Then
                Dim Count As Integer
                For Count = 0 To userLang.GetUpperBound(0)
                    If InStr(1, userLang(Count), ";") Then
                        Language = Left(userLang(Count), InStrRev(userLang(Count), ";") - 1).ToLower
                    Else
                        Language = userLang(Count).ToLower
                    End If
                    ' CheckLanguageinXML
                    If elem Is Nothing Then
                        elem = CType(xmlDoc.SelectSingleNode("//settings[@language = '" & Language & "']/setting[@key='" & Key & "']"), XmlElement)
                    End If
                    If InStr(1, Language, "-") Then
                        Language = Left(userLang(Count), InStrRev(userLang(Count), "-") - 1).ToLower
                        ' CheckLanguageInXML
                        If elem Is Nothing Then
                            elem = CType(xmlDoc.SelectSingleNode("//settings[@language = '" & Language & "']/setting[@key='" & Key & "']"), XmlElement)
                        End If
                    End If
                    Language = ""
                Next Count
            End If




            If elem Is Nothing Then
                elem = CType(xmlDoc.SelectSingleNode("//settings[@language = '']/setting[@key='" & Key & "']"), XmlElement)
                If elem Is Nothing Then
                    GetXMLmessage = ""
                Else
                    GetXMLmessage = elem.InnerText
                End If
            Else
                GetXMLmessage = elem.InnerText
            End If

        End Function

        'set focus to any control
        Public Sub SetFormFocus(ByVal control As Control)
            If Not control.Page Is Nothing Then
                If control.Page.Request.Browser.JavaScript = True Then
                    ' Create JavaScript
                    Dim sb As New System.Text.StringBuilder()
                    sb.Append("<script language='JavaScript' type='text/javascript'>")
                    sb.Append("<!--")
                    sb.Append(ControlChars.Lf)
                    sb.Append(" document.")
                    ' Find the Form
                    Dim objParent As control = control.Parent
                    While Not TypeOf objParent Is System.Web.UI.HtmlControls.HtmlForm
                        objParent = objParent.Parent
                    End While
                    sb.Append(objParent.ClientID)
                    sb.Append(".")
                    sb.Append(control.UniqueID)
                    sb.Append(".focus();")
                    sb.Append(ControlChars.Lf)
                    sb.Append("// -->")
                    sb.Append(ControlChars.Lf)
                    sb.Append("</SCRIPT>")

                    HttpContext.Current.Items("FOCUS") = sb.ToString()
                End If
            End If
        End Sub


    End Module

End Namespace