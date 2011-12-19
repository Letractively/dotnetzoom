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
Option Strict On

Imports System
Imports System.Text
Imports System.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.Security
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public Class ForumBaseObject
#Region "ForumBaseObject"

        Private _ForumControl As ForumControl

        Public Sub New(ByVal ForumControl As ForumControl)
            _ForumControl = ForumControl
        End Sub 'New

        Public Overridable Sub CreateChildControls()
        End Sub 'CreateChildControls

        Public Overridable Sub OnPreRender()
        End Sub 'OnPreRender

        Public Overridable Sub Render(ByVal writer As HtmlTextWriter)
        End Sub 'Render


        Protected Sub RenderTableBegin(ByVal writer As HtmlTextWriter, ByVal cellSpacing As Integer, ByVal cellPadding As Integer) ' Begin table in which we will render object
            If (_ForumControl.BorderStyle <> BorderStyle.None) Then
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "TTTBorder")
                writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, cellSpacing.ToString())
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, cellPadding.ToString())
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%")
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "ForumContent")
                writer.RenderBeginTag(HtmlTextWriterTag.Table)
            End If
        End Sub 'RenderTableBegin

        Protected Sub RenderTableEnd(ByVal writer As HtmlTextWriter)
            writer.RenderEndTag()
        End Sub ' End table in which object was rendered

        Public ReadOnly Property Page() As Page
            Get
                Return _ForumControl.Page
            End Get
        End Property

        Public ReadOnly Property ForumControl() As ForumControl
            Get
                Return _ForumControl
            End Get
        End Property

        Public ReadOnly Property Controls() As ControlCollection
            Get
                Return _ForumControl.Controls
            End Get
        End Property

        Public ReadOnly Property LoggedOnUserID() As Integer
            Get
                Dim context As HttpContext = HttpContext.Current
                If Not context.User.Identity.Name = String.Empty Then
                    Return Int32.Parse(context.User.Identity.Name)
                Else
                    Return -1
                End If
            End Get
        End Property

#End Region
    End Class 'ForumBaseObject


    Public Class ForumControl
#Region "ForumControl"

        Inherits WebControl

        Implements INamingContainer 'ToDo: Add Implements Clauses for implementation methods of these interface(s)
        Private _initialised As Boolean = False
        Private _ForumBaseObject As ForumBaseObject

        Private _portalID As Integer
        Private ZmoduleID As Integer
        Private Zconfig As ForumConfig
        Private _scope As String
        Private _title As String
        Private _isFlatView As Boolean

        Public Sub New()
        End Sub 'New

        Protected Overridable Sub Initialise()
            _initialised = True
        End Sub 'Initialise

        Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
            Initialise()
        End Sub 'OnLoad

        Protected Overrides Sub EnsureChildControls()
            If Not _initialised Then
                Initialise()
            End If
            MyBase.EnsureChildControls()
        End Sub 'EnsureChildControls

        Protected Overridable Sub CreateObject()
        End Sub 'CreateObject

        Protected Overrides Sub CreateChildControls()
            CreateObject()
            If Not (_ForumBaseObject Is Nothing) Then
                _ForumBaseObject.CreateChildControls()
            End If
        End Sub 'CreateChildControls

        Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
            If Not (_ForumBaseObject Is Nothing) Then
                _ForumBaseObject.OnPreRender()
            End If
        End Sub 'OnPreRender

        Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
            If Not (_ForumBaseObject Is Nothing) Then
                _ForumBaseObject.Render(writer)
            End If
        End Sub 'Render 

        Public Property PortalID() As Integer
            Get
                Return _portalID
            End Get
            Set(ByVal Value As Integer)
                _portalID = Value
            End Set
        End Property

        Public Property ModuleID() As Integer
            Get
                Return ZmoduleID
            End Get
            Set(ByVal Value As Integer)
                ZmoduleID = Value
            End Set
        End Property

        Public Property Scope() As String
            Get
                Return _scope
            End Get
            Set(ByVal Value As String)
                _scope = Value
            End Set
        End Property

        Public Property Config() As ForumConfig
            Get
                Return Zconfig
            End Get
            Set(ByVal Value As ForumConfig)
                Zconfig = Value
            End Set
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal Value As String)
                _title = Value
            End Set
        End Property

        Public Property IsFlatView() As Boolean
            Get
                Return _isFlatView
            End Get
            Set(ByVal Value As Boolean)
                _isFlatView = Value
            End Set
        End Property

        Protected Property ForumBaseObject() As ForumBaseObject
            Get
                Return _ForumBaseObject
            End Get
            Set(ByVal Value As ForumBaseObject)
                _ForumBaseObject = Value
            End Set
        End Property

#End Region
    End Class 'ForumControl

    Public Class ForumObject
#Region "ForumObject"

        Inherits ForumBaseObject

        Public Sub New(ByVal forum As Forum)
            MyBase.New(forum)
        End Sub 'New


        Protected ReadOnly Property Forum() As Forum
            Get
                Return CType(ForumControl, Forum)
            End Get
        End Property

        Protected ReadOnly Property ModuleID() As Integer
            Get
                Return ModuleID
            End Get
        End Property

        Protected ReadOnly Property PortalID() As Integer
            Get
                Return PortalID
            End Get
        End Property

        Protected ReadOnly Property Config() As ForumConfig
            Get
                Return Config
            End Get
        End Property

#End Region
    End Class 'ForumObject


    Public Class Forum
#Region "Forum"

        Inherits DotNetZoom.ForumControl
        'Implements INamingContainer 'ToDo: Add Implements Clauses for implementation methods of these interface(s)

        Public Sub New()

        End Sub 'New

        Protected Overrides Sub CreateObject()
			Dim _postID As Integer = 0
			Dim _threadID As Integer = 0
			Dim ZforumID As Integer = 0
			Dim _groupID As Integer = 0 
			Dim _action As String = ""
			Dim _scope As String = ""
            
			try
			
			_postID   = Convert.ToInt32(Page.Request.QueryString("postid"))
			_threadID = Convert.ToInt32(Page.Request.QueryString("threadid"))
			 ZforumID  = Convert.ToInt32(Page.Request.QueryString("forumid"))
             _groupID = Convert.ToInt32(Page.Request.QueryString("groupid"))
             _action  = Page.Request.QueryString("action")
             _scope   = Page.Request.QueryString("scope")
			 Catch Exc As System.Exception
		     If PortalSettings.GetHostSettings("EnableErrorReporting").ToString <> "N" Then
                  ' SendNotification(PortalSettings.GetHostSettings("HostEmail").toString(), PortalSettings.GetHostSettings("HostEmail").ToString(), "", "Public Class ForumBaseObject - CreateObject()", ControlChars.CrLf + ControlChars.CrLf + Exc.ToString)
             End If

 			End Try
			
            Dim _name As String = ""

            Select Case _scope
                Case ""
                    If _action = "search" Then
                        Search()
                    Else
                        ForumBaseObject = New ForumGroup(Me, PortalID, ModuleID)
                    End If
                Case "forum"
                    If _action = "search" Then
                        ForumBaseObject = New ForumGroup(Me, PortalID, ModuleID)
                    ElseIf _action = "admin" Then
                        ForumBaseObject = New ForumGroup(Me, PortalID, ModuleID)
                    Else
                        ForumBaseObject = New ForumGroup(Me, PortalID, ModuleID)
                    End If

                Case "forum"
                    If _action = "search" Then
                        Search()
                    Else
                        ForumBaseObject = New ForumItems(Me, PortalID)
                    End If
                Case "thread"
                    If _action = "search" Then
                        Search()
                    Else
                        Dim threadsPage As Integer = Convert.ToInt32(Page.Request.QueryString("threadspage"))
                        If threadsPage > 0 Then
                            threadsPage = threadsPage - 1
                        End If
                        ForumBaseObject = New ForumThreads(Me, ZforumID, threadsPage)
                    End If
                Case "post"
                    If _action = "search" Then
                        Search()
                    Else
                        Dim threadPage As Integer = Convert.ToInt32(Page.Request.QueryString("threadpage"))
                        If threadPage > 0 Then
                            threadPage = threadPage - 1
                        End If
                        ForumBaseObject = New ForumThread(Me, ZforumID, _threadID, _postID, threadPage)
                    End If
                Case "moderateforum"
                    ForumBaseObject = New ForumModerate(Me)
                Case "moderatepost"
                    'Dim ModeratePage As Integer = Convert.ToInt32(Page.Request.QueryString("moderatepage"))
                    Dim ModeratePage As Integer = 0
                    ForumBaseObject = New ForumPostsModerate(Me, ZforumID, _postID, ModeratePage)
            End Select

        End Sub

        Private Sub Search()

            Dim searchPage As Integer = Convert.ToInt32(Page.Request.QueryString("searchpage"))

            Dim ZforumID As String = Page.Request.QueryString("forumsid")
			
			Dim IntforumID As String = Page.Request.QueryString("forumid")
			If Not (IntforumID Is Nothing) AndAlso IntforumID <> String.Empty Then
			if ZforumID <> String.Empty then
			ZforumID += ":" + IntforumID
			else
			ZforumID = IntforumID
			end if
			end if
			
			if ZforumID = String.Empty then ZforumID = "0"
			
            If searchPage > 0 Then
                searchPage = searchPage - 1
            End If

            Dim searchTerms As String = Page.Request.QueryString("searchterms")
            If Not (searchTerms Is Nothing) AndAlso searchTerms <> String.Empty Then
                searchTerms = searchTerms.Replace(":amp:", "&")
                searchTerms = searchTerms.Replace("'", "''")
            End If

			Dim searchObject As String = Page.Request.QueryString("searchobject")
            If Not (searchobject Is Nothing) AndAlso searchobject <> String.Empty Then
                searchobject = searchobject.Replace(":amp:", "&")
                searchobject = searchobject.Replace("'", "''")
            End If

			
			
            Dim UserAlias As String = Page.Request.QueryString("useralias")
            If Not (UserAlias Is Nothing) AndAlso UserAlias <> String.Empty Then
                UserAlias = UserAlias.Replace(":amp:", "&")
            End If

            Dim StartDate As String = Page.Request.QueryString("startdate")

            Dim EndDate As String = Page.Request.QueryString("enddate")

            ForumBaseObject = New ForumSearch(Me, ZforumID, searchPage, searchobject ,searchTerms, UserAlias, StartDate, EndDate)

        End Sub

        Protected Overrides Sub Initialise()

            MyBase.Initialise()

        End Sub

        Protected Overrides Sub CreateChildControls()
            'SetupCookies()
            MyBase.CreateChildControls()
        End Sub

#End Region
    End Class 'Forum




    Public Class ForumText
#Region "ForumText"

        Private _text As String
        Private ZforumConfig As ForumConfig
        Private Class Helper

            Public Sub New(ByVal open As Boolean, ByVal index As Integer, ByVal level As Integer)
                _open = open
                _index = index
                _level = level
            End Sub 'New

            Private _open As Boolean
            Private _valid As Boolean = False
            Private _index As Integer
            Private _level As Integer

            Public ReadOnly Property Open() As Boolean
                Get
                    Return _open
                End Get
            End Property

            Public ReadOnly Property Level() As Integer
                Get
                    Return _level
                End Get
            End Property

            Public ReadOnly Property Index() As Integer
                Get
                    Return _index
                End Get
            End Property

            Public Property Valid() As Boolean
                Get
                    Return _valid
                End Get
                Set(ByVal Value As Boolean)
                    _valid = Value
                End Set
            End Property
        End Class 'Helper

        Public Sub New(ByVal [text] As String, ByVal ForumConfig As ForumConfig)
            _text = [text]
            ZforumConfig = ForumConfig
        End Sub 'New

        Public Sub FormatMultiLine()
            _text = _text.Replace(ControlChars.Cr + ControlChars.Lf, ControlChars.Lf)
            _text = _text.Replace(ControlChars.Cr, ControlChars.Lf)
            '_text = _text.Replace(ControlChars.Lf, "<br>")
            'Remove unwanted <br>
            _text = _text.Replace("<br><P>", "<P>")
            _text = _text.Replace("<P><br>", "<P>")
            _text = _text.Replace("<table><br>", "<table>")
            _text = _text.Replace("<br></table>", "</table>")
            _text = _text.Replace("<td><br>", "<td>")
            _text = _text.Replace("<tr><br>", "<tr>")
            _text = _text.Replace("</td><br>", "</td>")
            _text = _text.Replace("</tr><br>", "</tr>")
        End Sub 'FormatMultiLine

        Public Sub FormatDisableHtml()
            ' From Code Project forums...
            _text = ReplaceCaseInsensitive(_text, "<script", "&lt;script")
            _text = ReplaceCaseInsensitive(_text, "</script", "&lt;/script")
            _text = ReplaceCaseInsensitive(_text, "<input", "&lt;input")
            _text = ReplaceCaseInsensitive(_text, "</input", "&lt;/input")
            _text = ReplaceCaseInsensitive(_text, "<object", "&lt;object")
            _text = ReplaceCaseInsensitive(_text, "</object", "&lt;/object")
            _text = ReplaceCaseInsensitive(_text, "<applet", "&lt;applet")
            _text = ReplaceCaseInsensitive(_text, "</applet", "&lt;/applet")
            _text = ReplaceCaseInsensitive(_text, "<form", "&lt;form")
            _text = ReplaceCaseInsensitive(_text, "</form", "&lt;/form")
            _text = ReplaceCaseInsensitive(_text, "<table", "&lt;table")
            _text = ReplaceCaseInsensitive(_text, "</table", "&lt;/table")
            _text = ReplaceCaseInsensitive(_text, "<tr", "&lt;tr")
            _text = ReplaceCaseInsensitive(_text, "</tr", "&lt;/tr")
            _text = ReplaceCaseInsensitive(_text, "<td", "&lt;td")
            _text = ReplaceCaseInsensitive(_text, "</td", "&lt;/td")
            _text = ReplaceCaseInsensitive(_text, "select", "&lt;select")
            _text = ReplaceCaseInsensitive(_text, "</select", "&lt;/select")
            _text = ReplaceCaseInsensitive(_text, "<option", "&lt;option")
            _text = ReplaceCaseInsensitive(_text, "</option", "&lt;/option")
            _text = ReplaceCaseInsensitive(_text, "<iframe", "&lt;iframe")
            _text = ReplaceCaseInsensitive(_text, "</iframe", "&lt;/iframe")
        End Sub 'FormatDisableHtml

        Public Sub FormatHtml()
            _text = ReplaceCaseInsensitive(_text, "&lt;script", "<script")
            _text = ReplaceCaseInsensitive(_text, "&lt;/script", "</script")
            _text = ReplaceCaseInsensitive(_text, "&lt;input", "<input")
            _text = ReplaceCaseInsensitive(_text, "&lt;/input", "</input")
            _text = ReplaceCaseInsensitive(_text, "&lt;object", "<object")
            _text = ReplaceCaseInsensitive(_text, "&lt;/object", "</object")
            _text = ReplaceCaseInsensitive(_text, "&lt;applet", "<applet")
            _text = ReplaceCaseInsensitive(_text, "&lt;/applet", "</applet")
            _text = ReplaceCaseInsensitive(_text, "&lt;form", "<form")
            _text = ReplaceCaseInsensitive(_text, "&lt;/form", "</form")
            _text = ReplaceCaseInsensitive(_text, "&lt;table", "<table")
            _text = ReplaceCaseInsensitive(_text, "&lt;/table", "</table")
            _text = ReplaceCaseInsensitive(_text, "&lt;tr", "<tr")
            _text = ReplaceCaseInsensitive(_text, "&lt;/tr", "</tr")
            _text = ReplaceCaseInsensitive(_text, "&lt;td", "<td")
            _text = ReplaceCaseInsensitive(_text, "&lt;/td", "</td")
            _text = ReplaceCaseInsensitive(_text, "&lt;select", "select")
            _text = ReplaceCaseInsensitive(_text, "&lt;/select", "</select")
            _text = ReplaceCaseInsensitive(_text, "&lt;option", "<option")
            _text = ReplaceCaseInsensitive(_text, "&lt;/option", "</option")
            _text = ReplaceCaseInsensitive(_text, "&lt;iframe", "<iframe")
            _text = ReplaceCaseInsensitive(_text, "&lt;/iframe", "</iframe")
            _text = ReplaceCaseInsensitive(_text, "script&gt;", "script>")
            _text = ReplaceCaseInsensitive(_text, "script&gt;", "script>")
            _text = ReplaceCaseInsensitive(_text, "input&gt;", "input>")
            _text = ReplaceCaseInsensitive(_text, "input&gt;", "input>")
            _text = ReplaceCaseInsensitive(_text, "object&gt;", "object>")
            _text = ReplaceCaseInsensitive(_text, "object&gt;", "object>")
            _text = ReplaceCaseInsensitive(_text, "applet&gt;", "applet>")
            _text = ReplaceCaseInsensitive(_text, "applet&gt;", "applet>")
            _text = ReplaceCaseInsensitive(_text, "form&gt;", "form>")
            _text = ReplaceCaseInsensitive(_text, "form&gt;", "form>")
            _text = ReplaceCaseInsensitive(_text, "table&gt;", "table>")
            _text = ReplaceCaseInsensitive(_text, "table&gt;", "table>")
            _text = ReplaceCaseInsensitive(_text, "tr&gt;", "tr>")
            _text = ReplaceCaseInsensitive(_text, "tr&gt;", "tr>")
            _text = ReplaceCaseInsensitive(_text, "td&gt;", "td>")
            _text = ReplaceCaseInsensitive(_text, "td&gt;", "td>")
            _text = ReplaceCaseInsensitive(_text, "select&gt;", "select>")
            _text = ReplaceCaseInsensitive(_text, "select&gt;", "select>")
            _text = ReplaceCaseInsensitive(_text, "option&gt;", "option>")
            _text = ReplaceCaseInsensitive(_text, "option&gt;", "option>")
            _text = ReplaceCaseInsensitive(_text, "iframe&gt;", "iframe>")
            _text = ReplaceCaseInsensitive(_text, "iframe&gt;", "iframe>")
        End Sub

        Public Sub FormatPlainText()
            ' From Code Project forums...
            _text = ReplaceCaseInsensitive(_text, "<br>", vbCrLf)
            _text = ReplaceCaseInsensitive(_text, "<a href=", "")
            _text = ReplaceCaseInsensitive(_text, "</a>", "")
            _text = ReplaceCaseInsensitive(_text, "<b>", "")
            _text = ReplaceCaseInsensitive(_text, "</b>", "")
            _text = ReplaceCaseInsensitive(_text, "<i>", "")
            _text = ReplaceCaseInsensitive(_text, "</i>", "")
            _text = ReplaceCaseInsensitive(_text, """", "'")
            _text = ReplaceCaseInsensitive(_text, "/>", "")
            _text = ReplaceCaseInsensitive(_text, "&lt;", "<")
            _text = ReplaceCaseInsensitive(_text, "&gt;", ">")
            _text = ReplaceCaseInsensitive(_text, "<p>", vbCrLf)
            _text = ReplaceCaseInsensitive(_text, "</p>", vbCrLf)
        End Sub 'FormatDisableHtml

        Private Sub FormatSmiley(Optional ByVal AvatarURL As String = "")
            Dim objSmiley As ForumSmilies = ForumSmilies.GetSmileys(ZforumConfig.AvatarModuleID)
            Dim intCount As Integer

            For intCount = 0 To objSmiley.smileyItems.Count - 1
                Dim smiley As GalleryFile = CType(objSmiley.smileyItems.Item(intCount), GalleryFile)
                If Len(AvatarURL) > 0 Then
                    _text = _text.Replace(smiley.Title, String.Format("<img src=""{0}"" align=absmiddle border=0 \>", AvatarURL + smiley.URL))
                Else
                    _text = _text.Replace(smiley.Title, String.Format("<img src=""{0}"" align=absmiddle border=0 \>", smiley.URL))
                End If

            Next
        End Sub

        Private Sub UnformatSmiley(Optional ByVal AvatarURL As String = "")

            Dim objSmiley As ForumSmilies = ForumSmilies.GetSmileys(ZforumConfig.AvatarModuleID)
            Dim intCount As Integer

            For intCount = 0 To objSmiley.smileyItems.Count - 1
                Dim smiley As GalleryFile = CType(objSmiley.smileyItems.Item(intCount), GalleryFile)
                If Len(AvatarURL) > 0 Then
                    Dim imgURL As String = String.Format("<img src=""{0}"" align=absmiddle border=0 \>", AvatarURL + smiley.Name)
                    _text = _text.Replace(imgURL, smiley.Title)
                Else
                    Dim imgURL As String = String.Format("<img src=""{0}"" align=absmiddle border=0 \>", smiley.URL)
                    _text = _text.Replace(imgURL, smiley.Title)
                End If
            Next

        End Sub


        Private Sub ProcessOpenClose(ByVal array As ArrayList)
            Dim index As Integer = 0
            Dim level As Integer = 0
            Dim indexQuoteOpen As Integer = 0
            Dim indexQuoteClose As Integer = 0
            Dim previousQuoteOpen As Boolean = False

            While indexQuoteOpen >= 0 OrElse indexQuoteClose >= 0

                indexQuoteOpen = _text.IndexOf("[QUOTE]", index)
                indexQuoteClose = _text.IndexOf("[/QUOTE]", index)

                If indexQuoteOpen >= 0 OrElse indexQuoteClose >= 0 Then
                    If indexQuoteOpen >= 0 AndAlso (indexQuoteOpen < indexQuoteClose OrElse indexQuoteClose = -1) Then
                        If index > 0 AndAlso previousQuoteOpen Then
                            level += 1
                        End If
                        array.Add(New Helper(True, indexQuoteOpen, level))
                        index = indexQuoteOpen + 7
                        previousQuoteOpen = True
                    ElseIf indexQuoteClose >= 0 AndAlso (indexQuoteClose < indexQuoteOpen OrElse indexQuoteOpen = -1) Then
                        If index > 0 AndAlso Not previousQuoteOpen Then
                            level -= 1
                        End If
                        array.Add(New Helper(False, indexQuoteClose, level))
                        index = indexQuoteClose + 8
                        previousQuoteOpen = False
                    End If
                End If
            End While
        End Sub 'ProcessOpenClose



        Private Sub ValidateFindClose(ByVal array As ArrayList, ByVal index As Integer)
            Dim helperOpen As Helper = CType(array(index), Helper)

            index += 1
            Dim foundClose As Boolean = False

            While Not foundClose AndAlso index < array.Count
                Dim helperClose As Helper = CType(array(index), Helper)

                If helperClose.Level = helperOpen.Level AndAlso Not helperClose.Valid Then
                    helperOpen.Valid = True
                    helperClose.Valid = True
                    foundClose = True
                End If
                index += 1
            End While
        End Sub 'ValidateFindClose


        Private Sub ValidateOpenClose(ByVal array As ArrayList)
            Dim index As Integer
            For index = 0 To array.Count - 1
                Dim helper As helper = CType(array(index), helper)
                If helper.Open Then
                    ValidateFindClose(array, index)
                End If
            Next index
        End Sub 'ValidateOpenClose

        Private Sub FormatQuoteOpen(ByVal sb As StringBuilder)
            'sb.Append("<table border=""0"" cellpadding=""10"" cellspacing=""0"" width=""100%"">")
            'sb.Append("<tr class=""Normal""><td>[")
            'sb.Append("[")
            sb.Append("<blockquote>")
        End Sub 'FormatQuoteOpen


        Private Sub FormatQuoteClose(ByVal sb As StringBuilder)
            sb.Append("</blockquote>")
			'sb.Append("]")
            'sb.Append("]</td>")
            'sb.Append("</tr>")
            'sb.Append("</table>")
        End Sub 'FormatQuoteClose


        Private Sub FormatStripQuotes()
            Dim array As New ArrayList
            ProcessOpenClose(array)
            ValidateOpenClose(array)

            Dim sb As New StringBuilder

            Dim startIndex As Integer = 0
            Dim index As Integer = 0

            While index < array.Count
                Dim helper As helper = CType(array(index), helper)

                If helper.Valid Then
                    ' helper.Open will be true, meaning there must be a corresponding
                    ' closing helper item at the same level as helper.Open
                    Dim found As Boolean = False
                    Dim level As Integer = helper.Level
                    index += 1
                    Dim helperClose As helper = Nothing
                    While index < array.Count AndAlso Not found
                        helperClose = CType(array(index), helper)
                        If helperClose.Valid AndAlso Not helperClose.Open AndAlso helperClose.Level = level Then
                            found = True
                        End If
                        index += 1
                    End While
                    If found Then
                        Dim length As Integer = helper.Index - startIndex
                        If length > 0 Then
                            sb.Append(_text.Substring(startIndex, length))
                        End If
                        startIndex = helperClose.Index + 8
                    End If
                Else
                    index += 1
                End If
            End While

            If _text.Length - startIndex > 0 Then
                sb.Append(_text.Substring(startIndex, _text.Length - startIndex))
            End If
            _text = sb.ToString()
        End Sub 'FormatStripQuotes


        Private Sub FormatQuote()
            Dim array As New ArrayList
            ProcessOpenClose(array)
            ValidateOpenClose(array)

            Dim sb As New StringBuilder

            Dim startIndex As Integer = 0
            Dim index As Integer
            For index = 0 To array.Count - 1
                Dim helper As helper = CType(array(index), helper)

                If helper.Valid Then
                    Dim length As Integer = helper.Index - startIndex
                    If length > 0 Then
                        sb.Append(_text.Substring(startIndex, length))
                    End If
                    If helper.Open Then
                        FormatQuoteOpen(sb)
                        startIndex = helper.Index + 7
                    Else
                        FormatQuoteClose(sb)
                        startIndex = helper.Index + 8
                    End If
                End If
            Next index

            If _text.Length - startIndex > 0 Then
                sb.Append(_text.Substring(startIndex, _text.Length - startIndex))
            End If
            _text = sb.ToString()
        End Sub 'FormatQuote


        'Public Function ProcessSingleLine(ByVal images As String) As String
        Public Function ProcessSingleLine() As String
            FormatSmiley()
            FormatDisableHtml()
            Return _text
        End Function 'ProcessSingleLine

        Public Function Process() As String
            FormatSmiley()
            FormatMultiLine()
            FormatDisableHtml()
            FormatQuote()
            Return _text
        End Function 'Process

        
        Public Function Process(ByVal avatarURL As String) As String

            FormatSmiley(avatarURL)
            FormatMultiLine()
            FormatDisableHtml()
            FormatQuote()
            Return _text
        End Function 'Process

        Public Function ProcessHtml() As String
            FormatSmiley()
            FormatHtml()
            FormatMultiLine()
            FormatQuote()
            Return _text
        End Function 'Process

        Public Function ProcessNoSmiley() As String
            FormatMultiLine()
            FormatDisableHtml()
            FormatQuote()
            Return _text
        End Function 'Process

        Public Function ProcessQuoteBody(ByVal parentPoster As String, ByVal RichText As Boolean) As String
            FormatStripQuotes()
            
            If RichText Then
                Dim strQuote As String = parentPoster + " " + GetLanguage("Quote_Wrote") + " :<br><blockquote>"
                strQuote += _text + "</blockquote>"
                _text = strQuote
            Else
			    FormatPlainText()
                Dim strQuote As String = parentPoster & " " & GetLanguage("Quote_Wrote") & " :" & vbCrLf & " [QUOTE]" + vbCrLf
                strQuote += _text + vbCrLf + "[/QUOTE]" + vbCrLf
                _text = strQuote
            End If
            Return _text
        End Function 'ProcessQuoteBody 

        Public Property [Text]() As String
            Get
                Return _text
            End Get
            Set(ByVal Value As String)
                _text = Value
            End Set
        End Property
#End Region
    End Class 'ForumText

End Namespace