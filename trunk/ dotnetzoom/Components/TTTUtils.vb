'=======================================================================================
' TTTUTILS FOR DOTNETNUKE
'=======================================================================================
' Created by TAM TRAN MINH
' for using in TTT CORPORATION intranet & website
' http://www.tttcompany.com
' tam@tttcompany.com
'=======================================================================================
Option Strict On

Imports System
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.Mail
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Collections.Specialized

Namespace DotNetZoom

    Public Class TTTUtils

        Public Shared Function ConvertString(ByVal value As Object) As String
            If value Is DBNull.Value Then
                Return ""
            Else
                Return Convert.ToString(value)
            End If
        End Function

        Public Shared Function ConvertDateTime(ByVal value As Object) As DateTime
            If value Is DBNull.Value Then
                Return #1/1/2000#
            Else
                Return Convert.ToDateTime(value)
            End If
        End Function

        Public Shared Function ConvertInteger(ByVal value As Object) As Integer
            If value Is DBNull.Value Then
                Return 0
            Else
                Return Convert.ToInt16(value)
            End If
        End Function

        Public Shared Function ConvertBoolean(ByVal value As Object) As Boolean
            If value Is DBNull.Value Then
                Return False
            Else
                Return Convert.ToBoolean(value)
            End If
        End Function

        ' Provides more functionality than the path object static functions
        Public Shared Function BuildPath(ByVal Input() As String, ByVal Delimiter As String, ByVal StripInitial As Boolean, ByVal StripFinal As Boolean) As String
            Dim output As StringBuilder = New StringBuilder()

            output.Append(Join(Input, Delimiter))
            output.Replace(Delimiter & Delimiter, Delimiter)

            If StripInitial Then
                If Left(output.ToString(), Len(Delimiter)) = Delimiter Then
                    output.Remove(0, Len(Delimiter))
                End If
            End If

            If StripFinal Then
                If Right(output.ToString, Len(Delimiter)) = Delimiter Then
                    output.Remove(output.Length - Len(Delimiter), Len(Delimiter))
                End If
            End If

            Return output.ToString()

        End Function


        Public Shared Function SendForumMail(ByVal PostID As Integer, ByVal URL As String, ByVal EmailType As ForumEmail.ForumEmailType) As String

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim forumMail As New ForumEmail()
            With forumMail
                .PostID = PostID
                .EmailType = EmailType
                .UrlContentBase = URL
                .Generate()
            End With

            If Not Len(portalSettings.GetHostSettings("SMTPServer")) = 0 Then
                SmtpMail.SmtpServer = CType(portalSettings.GetHostSettings("SMTPServer"), String)
            End If

			' external SMTP server
           If Not Len(portalSettings.GetHostSettings("SMTPServer")) = 0 Then
                SmtpMail.SmtpServer = CType(portalSettings.GetHostSettings("SMTPServer"), String)
				If Not Len(portalSettings.GetHostSettings("SMTPServerUser")) = 0 then 
				forumMail.Fields("http://schemas.microsoft.com/cdo/configuration/smtpserver") = Ctype(portalSettings.GetHostSettings("SMTPServer"), string)
			    forumMail.Fields("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = 25
			    forumMail.Fields("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2
			    forumMail.Fields("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1
			    forumMail.Fields("http://schemas.microsoft.com/cdo/configuration/sendusername") = Ctype(portalSettings.GetHostSettings("SMTPServerUser"), String)
			    forumMail.Fields("http://schemas.microsoft.com/cdo/configuration/sendpassword") = Ctype(portalSettings.GetHostSettings("SMTPServerPassword"), String)
				end if 			
            End If

			
			
			
			
			
		 forumMail.Body = Replace(forumMail.Body, "src=""/", "src=""http://" + HttpContext.Current.Request.ServerVariables("HTTP_HOST") + "/")
	
           If Not forumMail.Bcc = String.Empty Then
                Try
                    SmtpMail.Send(forumMail)
                Catch exc As System.Exception
                    Return exc.Message
                End Try
            End If

            Return ""

        End Function

        Public Shared Function GetLastPostInfo(ByVal LastPostedDate As DateTime, ByVal LastPostedAuthor As String, ByVal Pinned As Boolean) As String
            Dim strInfo As String

            If Pinned Then
                strInfo = "<b>" & GetLanguage("F_Stiky") & "</b>"
            Else
                If LastPostedDate.Date = Today Then
                    strInfo = "<b>" & GetLanguage("F_Today") & "</b>"
                    strInfo += " @ "
                    strInfo += LastPostedDate.ToLongTimeString
                Else
                    strInfo = LastPostedDate.ToString()
                End If
            End If
            strInfo += "<br>"

            Return strInfo

        End Function 'GetLastPostInfo

        Private Overloads Shared Function CreateHashtableFromQueryString(ByVal page As Page) As Hashtable
            Dim ht As New Hashtable()

            Dim query As String
            For Each query In page.Request.QueryString
                If Len(query) > 0 Then
                    ht.Add(query, page.Request.QueryString(query))
                End If
            Next query
            Return ht
        End Function 'CreateHashtableFromQueryString


        Private Shared Function CreateQueryString(ByVal current As Hashtable, ByVal add As Hashtable, ByVal remove As Hashtable) As String
            Dim myEnumerator As IDictionaryEnumerator = add.GetEnumerator()
            While myEnumerator.MoveNext()
                If current.ContainsKey(myEnumerator.Key) Then
                    current.Remove(myEnumerator.Key)
                End If
                current.Add(myEnumerator.Key, myEnumerator.Value)
            End While

            myEnumerator = remove.GetEnumerator()
            While myEnumerator.MoveNext()
                Dim removeKey As String = CStr(myEnumerator.Key)

                If current.ContainsKey(removeKey) Then
                    Dim removeValue As String = CStr(myEnumerator.Value)

                    If CStr(current(removeKey)) = removeValue OrElse removeValue = String.Empty Then
                        current.Remove(removeKey)
                    End If
                End If
            End While
            Dim count As Integer = 0
            Dim sb As New StringBuilder()
            myEnumerator = current.GetEnumerator()
            While myEnumerator.MoveNext()
                If count = 0 Then
                    sb.Append("?")
                Else
                    sb.Append("&")
                End If
                sb.Append(myEnumerator.Key)
                sb.Append("=")
                sb.Append(myEnumerator.Value)
                count += 1
            End While

            Return sb.ToString()
        End Function 'CreateQueryString

        Private Overloads Shared Function CreateHashtableFromQueryString(ByVal query As String) As Hashtable
            Dim ht As New Hashtable()

            Dim startIndex As Integer = 0
            While startIndex >= 0
                Dim oldStartIndex As Integer = startIndex
                Dim equalIndex As Integer = query.IndexOf("=", startIndex)
                startIndex = query.IndexOf("&", startIndex)
                If startIndex >= 0 Then
                    startIndex += 1
                End If
                If equalIndex >= 0 Then
                    Dim lengthValue As Integer = 0
                    If startIndex >= 0 Then
                        lengthValue = startIndex - equalIndex - 2
                    Else
                        lengthValue = query.Length - equalIndex - 1
                    End If
                    Dim key As String = query.Substring(oldStartIndex, equalIndex - oldStartIndex)
                    Dim val As String = query.Substring(equalIndex + 1, lengthValue)

                    ht.Add(key, val)
                End If
            End While

            Return ht
        End Function 'CreateHashtableFromQueryString

        Public Shared Function GetURL(ByVal baseURL As String, ByVal page As Page, ByVal add As String, ByVal remove As String) As String
                    

            Dim currentQueries As Hashtable = CreateHashtableFromQueryString(page)
            Dim addQueries As Hashtable = CreateHashtableFromQueryString(add)
            Dim removeQueries As Hashtable = CreateHashtableFromQueryString(remove)

            Dim newQueryString As String = CreateQueryString(currentQueries, addQueries, removeQueries)

            Return baseURL + newQueryString
        End Function 'GetURL

        Public Shared Function GetCaseInsensitiveSearch(ByVal search As String) As String
            Dim result As String = String.Empty

            Dim index As Integer

            For index = 0 To search.Length - 1
                Dim character As Char = search.Chars(index)
                Dim characterLower As Char = Char.ToLower(character)
                Dim characterUpper As Char = Char.ToUpper(character)

                If characterUpper = characterLower Then
                    result = result + character
                Else
                    result = result + "[" + characterLower + characterUpper + "]"
                End If

            Next index
            Return result
        End Function 'GetCaseInsensitiveSearch


        
        Public Shared Function ReplaceCaseInsensitive(ByVal text As String, ByVal oldValue As String, ByVal newValue As String) As String
            oldValue = GetCaseInsensitiveSearch(oldValue)

            Return Regex.Replace([text], oldValue, newValue)

        End Function 'ReplaceCaseInsensitive

        Public Shared Function IsValidExtension(ByVal FileName As String, ByVal AcceptedExtensions As String) As Boolean
            Dim _fileTypes As New ArrayList()
            Dim extension As String

            For Each extension In Split(AcceptedExtensions, ";", , CompareMethod.Text)
                _fileTypes.Add(LCase(extension))
            Next

            Dim fileExtension As String = Mid(FileName, InStrRev(FileName, ".")).ToLower
            If Not _fileTypes.Contains(fileExtension) Then
                Return False
            End If

            Return True

        End Function

        Public Shared Function JSEncode(ByVal Source As String) As String
            Source = ReplaceCaseInsensitive(Source, "'", "^")
            Return Source
        End Function

        Public Shared Function JSDecode(ByVal Source As String) As String
            Source = ReplaceCaseInsensitive(Source, "^", "'")
            Return Source
        End Function

#Region "Links"

        Public Shared Function ForumHomeLink(ByVal TabId As Integer) As String
 			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabId.ToString, "forumpage=" & TTT_ForumDispatch.ForumDesktopType.ForumMain)
        End Function

        Public Shared Function ForumSearchLink(ByVal TabId As Integer, ByVal ModuleID As Integer) As String
            ' search

            Dim sb As New StringBuilder()
            sb.Append("forumpage=")
            sb.Append(TTT_ForumDispatch.ForumDesktopType.ForumSearch)
            sb.Append("&mid=")
            sb.Append(ModuleID)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabId.ToString, sb.ToString)

        End Function

        Public Shared Function ForumModerateLink(ByVal TabId As Integer) As String
            ' moderate
             Dim sb As New StringBuilder()
             sb.Append("scope=moderateforum")
             sb.Append("&forumpage=")
             sb.Append(TTT_ForumDispatch.ForumDesktopType.ForumMain)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabId.ToString, sb.ToString)
        End Function

        Public Shared Function ForumUserProfileLink(ByVal TabId As Integer, ByVal UserID As Integer) As String
            ' profile
            Dim sb As New StringBuilder()
            sb.Append("forumpage=")
            sb.Append(TTT_ForumDispatch.ForumDesktopType.ForumProfile)
            sb.Append("&userid=")
            sb.Append(UserID.ToString)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabId.ToString, sb.ToString)
        End Function

        Public Shared Function ForumSubscribeLink(ByVal TabId As Integer, ByVal ModuleID As Integer, ByVal UserID As Integer) As String
            ' subscribe
            Dim sb As New StringBuilder()
            sb.Append("forumpage=")
            sb.Append(TTT_ForumDispatch.ForumDesktopType.ForumSubscribe)
            sb.Append("&mid=")
            sb.Append(ModuleID.ToString)
            sb.Append("&userid=")
            sb.Append(UserID.ToString)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabId.ToString, sb.ToString)
        End Function

        Public Shared Function ForumNewThreadLink(ByVal TabID As Integer, ByVal ModuleID As Integer, ByVal ForumID As Integer) As String
            ' new thread
            Dim sb As New StringBuilder()
            sb.Append("mid=")
            sb.Append(ModuleID.ToString)
            sb.Append("&forumid=")
            sb.Append(ForumID.ToString)
            sb.Append("&scope=thread&action=new")
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabID.ToString, sb.ToString)
        End Function

        Public Shared Function ForumSettingLink(ByVal TabID As Integer, ByVal ModuleID As Integer) As String
            ' admin
            Dim sb As New StringBuilder()
            sb.Append("edit=forum&editpage=")
            sb.Append(TTT_EditForum.ForumEditType.GlobalSettings)
            sb.Append("&mid=")
            sb.Append(ModuleID.ToString)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabID.ToString, sb.ToString)
        End Function

        ' without forumid
        Public Shared Function ForumAdminLink(ByVal TabID As Integer, ByVal moduleID As Integer) As String
            'forum admin without forumid
            Dim sb As New StringBuilder()
            sb.Append("edit=admin&editpage=")
            sb.Append(TTT_EditForum.ForumEditType.ForumAdmin)
            sb.Append("&mid=")
            sb.Append(moduleID.ToString)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabID.ToString, sb.ToString)
        End Function

        ' with forumid
        Public Shared Function ForumAdminLink(ByVal TabID As Integer, ByVal moduleID As Integer, ByVal ForumID As Integer) As String
            Dim sb As New StringBuilder()
            sb.Append("edit=admin&editpage=")
            sb.Append(TTT_EditForum.ForumEditType.ForumAdmin)
            sb.Append("&mid=")
            sb.Append(moduleID.ToString)
           sb.Append("&forumid=")
            sb.Append(ForumID.ToString)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabID.ToString, sb.ToString)
        End Function

        Public Shared Function ForumUserAdminLink(ByVal TabID As Integer, ByVal ModuleID As Integer) As String
            ' moderate admin
            Dim sb As New StringBuilder()
            sb.Append("edit=user&editpage=")
            sb.Append(TTT_EditForum.ForumEditType.ForumUserAdmin)
            sb.Append("&mid=")
            sb.Append(ModuleID.ToString)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabID.ToString, sb.ToString)
        End Function

        Public Shared Function ForumUserAdminLink(ByVal TabID As Integer, ByVal ModuleID As Integer, ByVal UserID As Integer) As String
            ' moderate admin
            Dim sb As New StringBuilder()
            sb.Append("edit=user&editpage=")
            sb.Append(TTT_EditForum.ForumEditType.ForumUserAdmin)
            sb.Append("&mid=")
            sb.Append(ModuleID.ToString)
            sb.Append("&userid=")
            sb.Append(UserID)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabID.ToString, sb.ToString)
        End Function

        Public Shared Function ForumModerateAdminLink(ByVal TabID As Integer, ByVal ModuleID As Integer) As String
            ' moderate admin without forumid
            Dim sb As New StringBuilder()
            sb.Append("edit=moderate&editpage=")
            sb.Append(TTT_EditForum.ForumEditType.ForumModerateAdmin)
            sb.Append("&mid=")
            sb.Append(ModuleID.ToString)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabID.ToString, sb.ToString)
        End Function

        Public Shared Function ForumModerateAdminLink(ByVal TabID As Integer, ByVal ModuleID As Integer, ByVal ForumID As Integer) As String
            ' moderate admin with forumid
            Dim sb As New StringBuilder()
            sb.Append("edit=moderate&editpage=")
            sb.Append(TTT_EditForum.ForumEditType.ForumModerateAdmin)
            sb.Append("&mid=")
            sb.Append(ModuleID.ToString)
            sb.Append("&forumid=")
            sb.Append(ForumID.ToString)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabID.ToString, sb.ToString)
        End Function

        Public Shared Function ForumPMSLink(ByVal TabID As Integer) As String
            ' private message
            Dim sb As New StringBuilder()
            sb.Append("forumpage=")
            sb.Append(TTT_ForumDispatch.ForumDesktopType.ForumPrivateMessage)
            ' sb.Append("&def=UsersPMS")
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabID.ToString, sb.ToString)
        End Function

        Public Shared Function ForumPMSComposeLink(ByVal TabID As Integer, ByVal UserID As Integer) As String
            Dim sb As New StringBuilder()
            sb.Append("forumpage=")
            sb.Append(TTT_ForumDispatch.ForumDesktopType.ForumPrivateMessage)
            sb.Append("&pmstabid=3")
            sb.Append("&userid=")
            sb.Append(UserID.ToString)
			Dim _PortalSetting As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Return FormatFriendlyURL(_PortalSetting.ActiveTab.FriendlyTabName, _PortalSetting.ActiveTab.ssl, _PortalSetting.ActiveTab.ShowFriendly, TabID.ToString, sb.ToString)
        End Function

        Public Shared Function MSNLink(ByVal value As String) As String
            Return ""
        End Function

        Public Shared Function YahooLink(ByVal value As String) As String
            Dim strTarget As String = "http://edit.yahoo.com/config/send_webmesg?.target=" & value & "&.src=pg"
            Return strTarget
        End Function

        Public Shared Function AIMLink(ByVal value As String) As String
            Dim strTarget As String = "aim:goim?screenname=" & value & "&message=Allo+est+tu+la?"
            Return strTarget
        End Function

        Public Shared Function ICQLink(ByVal value As String) As String
            Dim strTarget As String = "http://wwp.icq.com/scripts/search.dll?to=" & value
            Return strTarget
        End Function

        Public Shared Function GalleryAdminLink(ByVal TabID As Integer, ByVal ModuleID As Integer) As String
            Return GetFullDocument() & "?edit=control&editpage=" & TTT_EditGallery.GalleryEditType.GalleryAdmin & "&mid=" & ModuleID.ToString & "&tabid=" & TabID.ToString
        End Function

#End Region

#Region "ValidateUploadFile"

        Public Class ValidateFile

            Inherits BaseValidator
            Implements INamingContainer 'ToDo: Add Implements Clauses for implementation methods of these interface(s)

            Private _inputFile As HtmlInputFile
            Private _maxFileSize As Integer
            Private _maxHeight As Integer = 0 '0 will not check
            Private _maxWidth As Integer = 0 '0 will not check
            Private _fileExtension As String
            Private _fileExtensions As String
            Private _fileTypes As New ArrayList()

            Public Sub New()
            End Sub

            Protected Overrides Function EvaluateIsValid() As Boolean
                If Not (_inputFile Is Nothing) _
                AndAlso Not (_inputFile.Size = 0) _
                AndAlso Not (_inputFile.PostedFile Is Nothing) _
                AndAlso Not (_inputFile.PostedFile.FileName Is Nothing) _
                AndAlso Not (_inputFile.PostedFile.FileName = String.Empty) _
                AndAlso Not (_inputFile.PostedFile.ContentLength = 0) Then

                    Dim extension As String
                    For Each extension In Split(_fileExtensions, ";", , CompareMethod.Text)
                        _fileTypes.Add(LCase(extension))
                    Next


                    Dim fileName As String = _inputFile.PostedFile.FileName.ToLower()
                    _fileExtension = Mid(fileName, InStrRev(fileName, ".")).ToLower
                    If Not _fileTypes.Contains(_fileExtension) Then
                        ErrorMessage = Replace(GetLanguage("F_Bad_Ext"), "{fileext}", _fileExtensions)
                        Return False
                    End If

                    Dim fileSize As Integer = _inputFile.Size
                    If fileSize > _maxFileSize * 1024 Then
                        ErrorMessage = Replace(GetLanguage("F_ToLarge"), "{maxFileSize}" , _maxFileSize.ToString())
                        Return False
                    End If


                    If Not _maxHeight = 0 AndAlso Not _maxWidth = 0 Then 'Only check if heigth/width have value <> 0
                        Dim dimensionsCorrect As Boolean = False

                        Try
                            Dim image As System.Drawing.Image = System.Drawing.Image.FromStream(_inputFile.PostedFile.InputStream)
                            dimensionsCorrect = image.Width > 0 AndAlso image.Width <= _maxWidth AndAlso (image.Height > 0 AndAlso image.Height <= _maxHeight)
                        Catch
                        End Try

                        If Not dimensionsCorrect Then
                            ErrorMessage = Replace(GetLanguage("F_MaxImage"), "{maxHeight}", _maxHeight.ToString)
							ErrorMessage = Replace(ErrorMessage, "{maxWidth}",  _maxWidth.ToString)
                            Return False
                        End If
                    End If

                End If

                Return True
            End Function 'EvaluateIsValid

            Protected Function IsValidType() As Boolean

            End Function

            Public Property InputFile() As HtmlInputFile
                Get
                    Return _inputFile
                End Get
                Set(ByVal Value As HtmlInputFile)
                    _inputFile = Value
                End Set
            End Property

            Public Property AcceptedMaxSize() As Integer
                Get
                    Return _maxFileSize
                End Get
                Set(ByVal Value As Integer)
                    _maxFileSize = Value
                End Set
            End Property

            Public Property AcceptedMaxHeight() As Integer
                Get
                    Return _maxHeight
                End Get
                Set(ByVal Value As Integer)
                    _maxHeight = Value
                End Set
            End Property

            Public Property AcceptedMaxWidth() As Integer
                Get
                    Return _maxWidth
                End Get
                Set(ByVal Value As Integer)
                    _maxWidth = Value
                End Set
            End Property

            Public Property AcceptedExtensions() As String
                Get
                    Return _fileExtensions
                End Get
                Set(ByVal Value As String)
                    _fileExtensions = Value
                End Set
            End Property

            Public ReadOnly Property FileExtension() As String
                Get
                    Return _fileExtension
                End Get
            End Property

            Public ReadOnly Property Valid() As Boolean
                Get
                    Return EvaluateIsValid()
                End Get
            End Property

        End Class 'ValidateFile

#End Region

    End Class

End Namespace