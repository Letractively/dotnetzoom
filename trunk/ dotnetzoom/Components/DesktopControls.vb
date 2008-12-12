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
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.IO


Namespace DotNetZoom

    '*********************************************************************
    '
    ' PortalModuleControl Class
    '
    ' The PortalModuleControl class defines a custom base class inherited by all
    ' desktop portal modules within the Portal.
    ' 
    ' The PortalModuleControl class defines portal specific properties
    ' that are used by the portal framework to correctly display portal modules
    '
    '*********************************************************************

    Public Class PortalModuleControl
        Inherits UserControl

        ' Private field variables
        Private _isEditable As Integer = 0
        Private _moduleConfiguration As ModuleSettings
        Private _settings As Hashtable

        ' Public property accessors
        Public ReadOnly Property ModuleId() As Integer

            Get
                Return CInt(_moduleConfiguration.ModuleId)
            End Get

        End Property

        Public ReadOnly Property TabId() As Integer

            Get
                Return CInt(_moduleConfiguration.TabId)
            End Get

        End Property

        Public ReadOnly Property PortalId() As Integer

            Get
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                Return _portalSettings.PortalId
            End Get

        End Property

         Public ReadOnly Property PortalAlias() As String

            Get
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                Return _portalSettings.PortalAlias
            End Get

        End Property

        Public ReadOnly Property IsEditable() As Boolean

            Get

                ' Perform tri-state switch check to avoid having to perform a security
                ' role lookup on every property access (instead caching the result)
                If _isEditable = 0 Then

                    ' Obtain PortalSettings from Current Context
                    Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                    Dim blnPreview As Boolean = False
                    If Not Request.Cookies("_Tab_Admin_Preview" & PortalId.ToString) Is Nothing Then
                        blnPreview = Boolean.Parse(Request.Cookies("_Tab_Admin_Preview" & PortalId.ToString).Value)
                    End If
                    If IsAdminTab() Then
                        blnPreview = False
                    End If

                    If blnPreview = False And (PortalSecurity.IsInRoles(_moduleConfiguration.AuthorizedEditRoles) = True Or PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles) = True) Then
                        _isEditable = 1
                    Else
                        _isEditable = 2
                    End If
                End If

                Return _isEditable = 1
            End Get

        End Property

        Public ReadOnly Property EditURL(ByVal strKeyName As String, ByVal strKeyValue As String) As String

            Get
                Return "~" & GetDocument() & "?edit=control&tabid=" & TabId.ToString & IIf(Request.Params("adminpage") Is Nothing, "&mid=" & ModuleId.ToString, "&adminpage=" & Request.Params("adminpage")) & "&" & strKeyName & "=" & strKeyValue
            End Get

        End Property


        Public Property ModuleConfiguration() As ModuleSettings

            Get
                Return _moduleConfiguration
            End Get
            Set(ByVal Value As ModuleSettings)
                _moduleConfiguration = Value
            End Set

        End Property


        Public ReadOnly Property Settings() As Hashtable

            Get

                If _settings Is Nothing Then

                    _settings = PortalSettings.GetModuleSettings(ModuleId)
                End If

                Return _settings
            End Get

        End Property

	        Protected Overrides Sub Render(ByVal output As HtmlTextWriter)

             Dim _cachedOutput As String
             Dim tempWriter As StringWriter = New StringWriter()
             MyBase.Render(New HtmlTextWriter(tempWriter))
             _cachedOutput = tempWriter.ToString()
			
			 If Not IsNothing(_moduleConfiguration) And Not Request.Params("h") is Nothing then
			 Dim strColor As String = ""
 			 If Not Request.Params("m") is Nothing then
			 if Request.Params("m").ToString = ModuleId.ToString then 
			 _cachedOutput = "<div style=""border: medium dotted red"">" + _cachedOutput + "</div>"
			 strColor = "#FF0000"
			 else
			 strColor = "#00FF7F"
			 end if
			 else
			 strColor = "#FFFF00"
			 end if
                Dim Word As String = Request.Params("h").ToString
                Dim HTMLWord As String
			 Dim stringpattern as string 
			 stringpattern = "(" + Word + ")(?=[^>]*<)"
			 _cachedOutput = Regex.Replace(_cachedOutput, stringpattern, "<b style=""background: " + strColor + "; color: black"">" & word & "</b>", RegexOptions.IgnoreCase)
			 HTMLWord = Server.HTMLEncode(Word)
			 If HTMLWord <> Word then
			 stringpattern = "(" + HTMLWord + ")(?=[^>]*<)"
			 _cachedOutput = Regex.Replace(_cachedOutput, stringpattern, "<b style=""background: " + strColor + "; color: black"">" & word & "</b>", RegexOptions.IgnoreCase)
			 end if
			 end if
		
            ' Output the user control's content
             output.Write(_cachedOutput)
			
        End Sub	

    End Class
    _

    '*********************************************************************
    '
    ' CachedPortalModuleControl Class
    '
    ' The CachedPortalModuleControl class is a custom server control that
    ' the Portal framework uses to optionally enable output caching of 
    ' individual portal module's content.
    '
    ' If a CacheTime value greater than 0 seconds is specified within the 
    ' Portal.Config configuration file, then the CachePortalModuleControl
    ' will automatically capture the output of the Portal Module User Control
    ' it wraps.  It will then store this captured output within the ASP.NET
    ' Cache API.  On subsequent requests (either by the same browser -- or
    ' by other browsers visiting the same portal page), the CachedPortalModuleControl
    ' will attempt to resolve the cached output out of the cache.
    '
    ' Note: In the event that previously cached output can't be found in the
    ' ASP.NET Cache, the CachedPortalModuleControl will automatically instatiate
    ' the appropriate portal module user control and place it within the
    ' portal page.
    '
    '*********************************************************************

    Public Class CachedPortalModuleControl
        Inherits UserControl

        ' Private field variables
        Private _moduleConfiguration As ModuleSettings
        Private _cachedOutput As String = ""
        Private _portalId As Integer = 0

        ' Public property accessors
        Public Property ModuleConfiguration() As ModuleSettings

            Get
                Return _moduleConfiguration
            End Get
            Set(ByVal Value As ModuleSettings)
                _moduleConfiguration = Value
            End Set

        End Property


        Public ReadOnly Property ModuleId() As Integer

            Get
                Return _moduleConfiguration.ModuleId
            End Get

        End Property


        Public ReadOnly Property TabId() As Integer

            Get
                Return _moduleConfiguration.TabId
            End Get

        End Property

        Public ReadOnly Property PortalId() As Integer
            Get
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                Return _portalSettings.PortalId
            End Get
        End Property

        Public ReadOnly Property PortalAlias() As String

            Get
                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                Return _portalSettings.PortalAlias
            End Get

        End Property

        '*********************************************************************
        '
        ' CacheKey Property
        '
        ' The CacheKey property is used to calculate a "unique" cache key
        ' entry to be used to store/retrieve the portal module's content
        ' from the ASP.NET Cache.
        '
        '*********************************************************************

        Public ReadOnly Property CacheKey() As String

            Get
                Return GetDBname  & "_public" & CStr(_moduleConfiguration.ModuleId) 
            End Get

        End Property

	

        '*********************************************************************
        '
        ' CreateChildControls Method
        '
        ' The CreateChildControls method is called when the ASP.NET Page Framework
        ' determines that it is time to instantiate a server control.
        ' 
        ' The CachedPortalModuleControl control overrides this method and attempts
        ' to resolve any previously cached output of the portal module from the 
        ' ASP.NET cache.  
        '
        ' If it doesn't find cached output from a previous request, then the
        ' CachedPortalModuleControl will instantiate and add the portal module's
        ' User Control instance into the page tree.
        '
        '*********************************************************************

        Protected Overrides Sub CreateChildControls()
			' Attempt to resolve previously cached content from the ASP.NET Cache
			 if not Page.IsPostBack Then
                _cachedOutput = CStr(Context.Cache(CacheKey))
				else
				_cachedOutput = nothing
             End If

            ' If no cached content is found, then instantiate and add the portal
            ' module user control into the portal's page server control tree
            If _cachedOutput Is Nothing Then
                MyBase.CreateChildControls()
                Dim [module] As PortalModuleControl = CType(Page.LoadControl(_moduleConfiguration.DesktopSrc), PortalModuleControl)
                [module].ModuleConfiguration = Me.ModuleConfiguration
                Me.Controls.Add([module])
            End If

        End Sub


        '*********************************************************************
        '
        ' Render Method
        '
        ' The Render method is called when the ASP.NET Page Framework
        ' determines that it is time to render content into the page output stream.
        ' 
        ' The CachedPortalModuleControl control overrides this method and captures
        ' the output generated by the portal module user control.  It then 
        ' adds this content into the ASP.NET Cache for future requests.
        '
        '*********************************************************************

        Protected Overrides Sub Render(ByVal output As HtmlTextWriter)

             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If _cachedOutput Is Nothing Then
                Dim tempWriter As StringWriter = New StringWriter()
                MyBase.Render(New HtmlTextWriter(tempWriter))
                _cachedOutput = tempWriter.ToString()
				if not Page.IsPostBack then
				Context.Cache.Insert(CacheKey, _cachedOutput, CDp( _PortalSettings.PortalID, _PortalSettings.activeTab.TabID, _moduleConfiguration.ModuleId), DateTime.Now.AddSeconds(_moduleConfiguration.CacheTime), TimeSpan.Zero, Caching.CacheItemPriority.Normal, nothing)
				end if
			End If

			
			 If Not IsNothing(_moduleConfiguration) And Not Request.Params("h") is Nothing then
			 If Not Request.Params("m") is Nothing then
			 if Request.Params("m").ToString <> ModuleId.ToString then exit sub
			 end if
			 Dim Word As string
			 Dim HTMLWord As String 
			 Dim stringpattern as string 
			 Dim iColor As integer = 0
			 Dim strColor As String = ""
			 for Each word in Request.Params("h").Split(":"c)
			 if word <> "" and Not word Is Nothing then
			 stringpattern = "(" + word + ")(?=[^>]*<)"
			 IColor += 1
			 Select Case IColor 
			 case 1
			 strColor = "#FF0000"
			 case 2
			 strColor = "#00FF7F"
			 case 3
			 strColor = "#FF6347"
			 case 4
			 strColor = "#FFFF00"
			 case 5
			 strColor = "#9ACD32"
			 end select
			 _cachedOutput = Regex.Replace(_cachedOutput, stringpattern, "<b style=""background: " + strColor + "; color: black"">" & word & "</b>", RegexOptions.IgnoreCase)
			 HTMLWord = Server.HTMLEncode(Word)
			 If HTMLWord <> Word then
			 stringpattern = "(" + HTMLWord + ")(?=[^>]*<)"
			 _cachedOutput = Regex.Replace(_cachedOutput, stringpattern, "<b style=""background: " + strColor + "; color: black"">" & word & "</b>", RegexOptions.IgnoreCase)
			 end if
			 end if
			 next
			 end if
			
            ' Output the user control's content
            output.Write(_cachedOutput)

        End Sub

    End Class

End Namespace